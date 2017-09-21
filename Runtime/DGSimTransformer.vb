'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports SyncroSim.Common
Imports SyncroSim.StochasticTime
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class DGSimTransformer
    Inherits StochasticTimeTransformer

    Private m_OutputPopSizeDataTable As DataTable
    Private m_OutputHarvestDataTable As DataTable
    Private m_OutputRecruitsDataTable As DataTable
    Private m_OutputMortalityDataTable As DataTable
    Private m_OutputPosteriorDistDataTable As DataTable
    Private m_RandomGenerator As New RandomGenerator()

    ''' <summary>
    ''' Overrides Configure
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Configure()

        MyBase.Configure()

        Me.ValidateModel()
        Me.NormalizeData()

        Me.TimestepUnits = My.Resources.DGSIM_TIMESTEP

    End Sub

    ''' <summary>
    ''' Overrides Transform
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Transform()

        Me.InitializeRunControl()
        Me.InitializeOffspringPerFemaleBirthJDay()
        Me.InitializeAnnualHarvestVariables()
        Me.InitializeCollections()
        Me.InitializeOutputDataTables()
        Me.CreateMaps()

        MyBase.Transform()

    End Sub

    ''' <summary>
    ''' Overrides OnIteration
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnIteration(iteration As Integer)

        MyBase.OnIteration(iteration)
        Me.InitializeAgeSexCohortCollection()

    End Sub

    ''' <summary>
    ''' Overrides OnTimestep
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnTimestep(iteration As Integer, timestep As Integer)

        MyBase.OnTimestep(iteration, timestep)

        Me.SimulateTimestep(iteration, timestep)

        Me.ProcessSummaryPopSizeOutputData(iteration, timestep)
        Me.ProcessSummaryHarvestOutputData(iteration, timestep)
        Me.ProcessSummaryRecruitsOutputData(iteration, timestep)
        Me.ProcessSummaryMortalityOutputData(iteration, timestep)

    End Sub

    ''' <summary>
    ''' Runs the model for the specified iteration and timestep
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <remarks></remarks>
    Private Sub SimulateTimestep(ByVal iteration As Integer, ByVal timestep As Integer)

        For Each Stratum As Stratum In Me.m_Strata
            Me.SimulateTimestep(Stratum, iteration, timestep)
        Next

    End Sub

    ''' <summary>
    ''' Runs the model for the specified startum, iteration, and timestep
    ''' </summary>
    ''' <param name="stratum"></param>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <remarks></remarks>
    Private Sub SimulateTimestep(ByVal stratum As Stratum, ByVal iteration As Integer, ByVal timestep As Integer)

        Dim NumMaleOffspring As Double = 0
        Dim NumFemaleOffspring As Double = 0
        Dim cdata As CensusData = Me.m_CensusDataMap.GetItem(stratum.Id, timestep)
        Dim HasCensusData As Boolean = (cdata IsNot Nothing)
        Dim HasAgeSexCohorts As Boolean = (stratum.AgeSexCohorts.Count > 0)
        Dim RelAge As Integer = (Me.m_RunControl.MinimumTimestep - timestep)
        Dim MaleCalfMortality As Double = 0.0
        Dim FemaleCalfMortality As Double = 0.0

        If (Me.m_OffspringPerFemaleBirthJDay < Me.m_RunControl.StartJulianDay) Then

            'Don't age the cohorts on the very first timestep as assume ages are specified for the census date already
            If (timestep <> Me.m_RunControl.MinimumTimestep) Then

                For Each Cohort As AgeSexCohort In stratum.AgeSexCohorts
                    Cohort.Age += 1
                Next

            End If

        End If

        Dim IsOutsideCensusRange As Boolean = Me.IsOutsideCensusDataRange(stratum, timestep)

        For Each Cohort As AgeSexCohort In stratum.AgeSexCohorts
            Me.AddPopSizeOutputToCollection(Cohort, stratum)
        Next

        If (HasAgeSexCohorts) Then

            Me.ProcessPosteriorDistributionOutput(
                stratum.Id, iteration, timestep, HasCensusData, IsOutsideCensusRange)

        End If

        If (IsOutsideCensusRange) Then
            Me.AdjustPopulationForCensus(stratum, cdata)
        End If

        If (Not HasAgeSexCohorts) Then
            Return
        End If

        If (Me.m_OffspringPerFemaleBirthJDay >= Me.m_RunControl.StartJulianDay) Then

            For Each Cohort As AgeSexCohort In stratum.AgeSexCohorts
                Cohort.Age += 1
            Next

        End If

        DetermineAnnualHarvest(stratum, iteration, timestep)

        For Each Cohort As AgeSexCohort In stratum.AgeSexCohorts

            If (Cohort.Sex = Gender.Female) Then

                NumMaleOffspring += Me.CalculateNumOffspring(Cohort, Gender.Male, stratum, iteration, timestep, MaleCalfMortality)
                NumFemaleOffspring += Me.CalculateNumOffspring(Cohort, Gender.Female, stratum, iteration, timestep, FemaleCalfMortality)

            End If

            ' Calculate mortality from census to birthday first
            Dim AgeClassId As Integer = GetAgeClassIdFromAge(Cohort.Age)
            Dim TimePeriodMortality As Double = Me.CalculateTimePeriodMortality(stratum, iteration, timestep, Cohort.Sex, AgeClassId, 0, (GetRelativeJulianDay(Me.m_OffspringPerFemaleBirthJDay, Me.m_RunControl.StartJulianDay) - 1))
            Dim TotalMortality As Double = TimePeriodMortality * Cohort.NumIndividuals
            Dim NumIndividuals As Double = Cohort.NumIndividuals * (1 - TimePeriodMortality)

            'Update the annual harvest collection
            If (Cohort.AnnualHarvest <= NumIndividuals) Then
                Me.AddHarvestOutputToCollection(Cohort, stratum, Cohort.AnnualHarvest)
            Else
                Me.AddHarvestOutputToCollection(Cohort, stratum, NumIndividuals)
            End If

            'Change number of individuals
            NumIndividuals -= Cohort.AnnualHarvest

            If NumIndividuals < 0.0 Then
                NumIndividuals = 0.0
            End If

            ' Now calculate mortality from birthday to census
            AgeClassId = GetAgeClassIdFromAge(Cohort.Age + 1)
            TimePeriodMortality = Me.CalculateTimePeriodMortality(stratum, iteration, timestep, Cohort.Sex, AgeClassId, GetRelativeJulianDay(Me.m_OffspringPerFemaleBirthJDay, Me.m_RunControl.StartJulianDay), 364)
            TotalMortality += (TimePeriodMortality * NumIndividuals)

            'Update the mortality collection
            Me.AddMortalityOutputToCollection(Cohort, stratum, TotalMortality)

            NumIndividuals = NumIndividuals * (1 - TimePeriodMortality)
            Debug.Assert(NumIndividuals >= 0.0)
            Cohort.NumIndividuals = NumIndividuals

        Next

        Dim NewCohortAge As Integer = 0
        'When adding chorts that are born next year (before the census) specify the age as -1 so that they will be recorded as calves in the next census (aging happens before census).
        If (Me.m_OffspringPerFemaleBirthJDay < Me.m_RunControl.StartJulianDay) Then
            NewCohortAge = -1
        End If

        If (NumMaleOffspring > 0) Then
            Dim c As New AgeSexCohort(NewCohortAge, RelAge - 1, Gender.Male, NumMaleOffspring)
            Me.AddMortalityOutputToCollection(c, stratum, MaleCalfMortality)
            stratum.AgeSexCohorts.Add(c)

        End If

        If (NumFemaleOffspring > 0) Then

            Dim c As New AgeSexCohort(NewCohortAge, RelAge - 1, Gender.Female, NumFemaleOffspring)
            Me.AddMortalityOutputToCollection(c, stratum, FemaleCalfMortality)
            stratum.AgeSexCohorts.Add(c)

        End If

    End Sub

    ''' <summary>
    ''' Adjusts the population for the specified stratum using the specified census data
    ''' </summary>
    ''' <param name="stratum"></param>
    ''' <param name="cdata"></param>
    ''' <remarks></remarks>
    Private Sub AdjustPopulationForCensus(ByVal stratum As Stratum, ByVal cdata As CensusData)

        Dim TargetM2FRatio As Double = CalculateM2FRatio(stratum.AgeSexCohorts)
        Dim TargetPopulation As Integer = CalculateStratumPopulationSize(stratum)
        Dim CurrentM2FRatio As Double = TargetM2FRatio

        If (cdata.MinimumM2FRatio.HasValue And cdata.MaximumM2FRatio.HasValue) Then

            If (TargetM2FRatio < cdata.MinimumM2FRatio.Value Or TargetM2FRatio > cdata.MaximumM2FRatio.Value) Then

                TargetM2FRatio = Me.m_RandomGenerator.GetNextDouble()
                TargetM2FRatio *= (cdata.MaximumM2FRatio.Value - cdata.MinimumM2FRatio.Value)
                TargetM2FRatio += cdata.MinimumM2FRatio.Value

            End If

        End If

        If (TargetPopulation < cdata.MinimumPopulation Or TargetPopulation > cdata.MaximumPopulation) Then
            TargetPopulation = Me.m_RandomGenerator.GetNextInteger(cdata.MinimumPopulation, cdata.MaximumPopulation)
        End If

        Dim TotalMales As Integer = CalculatePopulationBySex(stratum.AgeSexCohorts, Gender.Male)
        Dim TotalFemales As Integer = CalculatePopulationBySex(stratum.AgeSexCohorts, Gender.Female)
        Dim MaleMultiplier As Double = TargetM2FRatio * (1 / CurrentM2FRatio) * (TargetPopulation / (TotalMales * TargetM2FRatio * (1 / CurrentM2FRatio) + TotalFemales))
        Dim FemaleMultiplier As Double = (TargetPopulation / (TotalMales * TargetM2FRatio * (1 / CurrentM2FRatio) + TotalFemales))

        For Each c As AgeSexCohort In stratum.AgeSexCohorts

            If (c.Sex = Gender.Male) Then

                Dim d As Double = c.NumIndividuals * MaleMultiplier
                c.NumIndividuals = d

            Else

                Dim d As Double = c.NumIndividuals * FemaleMultiplier
                c.NumIndividuals = d

            End If

        Next

    End Sub

    ''' <summary>
    ''' Initializes the Age/Sex cohort collection
    ''' </summary>
    ''' <remarks>This is done for each iteration</remarks>
    Private Sub InitializeAgeSexCohortCollection()

        Dim PopSize As Integer =
            CInt(DGSimGetRandomBeta(
                CDbl(Me.m_InitialPopulationSize.Mean),
                Me.m_InitialPopulationSize.SD,
                CDbl(Me.m_InitialPopulationSize.Min),
                CDbl(Me.m_InitialPopulationSize.Max),
                Me.m_RandomGenerator,
                "Core Model"))

        Dim SumRel As Double = Me.GetDistSumOfRelativeAmount()

        For Each Stratum As Stratum In Me.m_Strata
            Stratum.AgeSexCohorts.Clear()
        Next

        For Each ipd As InitialPopulationDistribution In Me.m_InitialPopulationDistributions

            Dim NumIndividuals As Integer = CInt(PopSize * ipd.RelativeAmount / SumRel)
            Dim NumIndDiv1 As Integer = CInt((NumIndividuals / (ipd.AgeMax - ipd.AgeMin + 1)))
            Dim NumIndDiv2 As Integer = CInt(NumIndDiv1 / 2)

            For Age As Integer = ipd.AgeMin To ipd.AgeMax

                If (ipd.Sex.HasValue) Then
                    Me.AddAgeCohort(Age, ipd.Sex.Value, NumIndDiv1, ipd.StratumId)
                Else

                    Me.AddAgeCohort(Age, Gender.Male, NumIndDiv2, ipd.StratumId)
                    Me.AddAgeCohort(Age, Gender.Female, NumIndDiv2, ipd.StratumId)

                End If

            Next

        Next

    End Sub

    Private Function CalculateNumOffspring(
        ByVal cohort As AgeSexCohort,
        ByVal offspringSex As Gender,
        ByVal stratum As Stratum,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByRef calfMortality As Double) As Double


        Dim AgeClassId As Integer = GetAgeClassIdFromAge(cohort.Age)
        Dim RelativeCountDay As Integer = Me.CalculateOffspringRelativeCountDay(stratum.Id, iteration, timestep, AgeClassId)
        Dim Mortality As Double = Me.CalculateTimePeriodMortality(stratum, iteration, timestep, Gender.Female, AgeClassId, 1, RelativeCountDay)
        Dim FecundityAdjustment As Double = Me.m_DemographicRateShiftMap.GetFecundityAdjustment(iteration, timestep, AgeClassId)
        Dim OffspringAgeClassId As Integer = GetAgeClassIdFromAge(0)

        Debug.Assert(Mortality >= 0.0 And Mortality <= 1.0)

        Dim opf As OffspringPerFemaleValue = Me.m_OffspringPerFemaleValueMap.GetItem(stratum.Id, AgeClassId, iteration, timestep)

        If (opf IsNot Nothing) Then

            'DEVTODO: remove the scaling of the harvest from this equation and check that numbers make sense.
            Dim d1 As Double = (cohort.NumIndividuals * (1 - Mortality)) - (cohort.AnnualHarvest)

            If d1 < 0.0 Then
                d1 = 0.0
            End If

            Dim d2 As Double = CalculateOffspringPerFemale(opf, FecundityAdjustment) * 0.5

            Me.AddRecruitsToOutputToCollection(cohort, stratum, (d1 * d2), offspringSex)

            Dim d3 As Double = Me.CalculateTimePeriodMortality(stratum, iteration, timestep, offspringSex, OffspringAgeClassId, RelativeCountDay, 365)
            Dim d4 As Double = (d1 * d2 * (1 - d3))

            calfMortality += (d3 * d1 * d2)

            Return d4

        Else
            Return 0
        End If

    End Function

    Private Function CalculateTimePeriodMortality(
        ByVal stratum As Stratum,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal sex As Gender,
        ByVal ageClassId As Integer,
        ByVal relativeStartDay As Integer,
        ByVal relativeEndDay As Integer) As Double

        If (relativeStartDay >= relativeEndDay) Then
            Return 0.0
        End If

        Dim Rates As SortedList(Of Integer, AnnualizedMortalityRate) =
            Me.m_AnnualizedMortalityRateMap.GetItems(stratum.Id, sex, ageClassId, iteration, timestep)

        Dim MortalityAdjustment As Double = Me.m_DemographicRateShiftMap.GetMortalityAdjustment(iteration, timestep, ageClassId)

        If (Not AtLeastOneNonWildcardRate(Rates)) Then
            Return CalculateTimePeriodMortalityWildCard(Rates, (relativeEndDay - relativeStartDay + 1), MortalityAdjustment)
        Else
            Return CalculateTimePeriodMortalityNoWildCard(relativeStartDay, relativeEndDay, Rates, MortalityAdjustment)
        End If

    End Function

    Private Shared Function CalculateTimePeriodMortalityWildCard(
        ByVal rates As SortedList(Of Integer, AnnualizedMortalityRate),
        ByVal relativeEndDay As Integer,
        ByVal mortalityAdjustmentFactor As Double) As Double

        Dim amr As AnnualizedMortalityRate = GetWildcardRate(rates)

        If (amr Is Nothing) Then
            Return 0.0
        Else
            Return CalculateMortality(amr, relativeEndDay, mortalityAdjustmentFactor)
        End If

    End Function

    Private Shared Function CalculateTimePeriodMortalityNoWildCard(
        ByVal relativeStartDay As Integer,
        ByVal relativeEndDay As Integer,
        ByVal Rates As SortedList(Of Integer, AnnualizedMortalityRate),
        ByVal mortalityAdjustmentFactor As Double) As Double

        Dim StartDay As Integer = relativeStartDay
        Dim EndDay As Integer = relativeEndDay
        Dim CumulativeSurvival As Double = 1.0
        Dim NumRatesUsed As Integer = 0

        For Each k As Integer In Rates.Keys

            If (k = SortedKeyMapBase.WildcardKey) Then
                Continue For
            End If

            Dim Rate As AnnualizedMortalityRate = Rates(k)
            Debug.Assert(Rate.RelativeJulianDay.HasValue)

            NumRatesUsed += 1

            If NumRatesUsed < Rates.Count Then

                If (relativeEndDay > (Rates.Values(NumRatesUsed).RelativeJulianDay.Value - 1)) Then
                    EndDay = Rates.Values(NumRatesUsed).RelativeJulianDay.Value - 1
                Else
                    EndDay = relativeEndDay
                End If

            Else
                EndDay = relativeEndDay
            End If

            If (Rate.RelativeJulianDay.Value < relativeEndDay) Then

                If (Rate.RelativeJulianDay.Value >= StartDay) Then

                    Dim NumDays As Integer = EndDay - StartDay + 1
                    Dim Mortality As Double = CalculateMortality(Rate, NumDays, mortalityAdjustmentFactor)

                    CumulativeSurvival *= (1 - Mortality)

                ElseIf (NumRatesUsed = Rates.Count) Then

                    Dim NumDays As Integer = EndDay - StartDay + 1
                    Dim Mortality As Double = CalculateMortality(Rate, NumDays, mortalityAdjustmentFactor)

                    CumulativeSurvival *= (1 - Mortality)

                ElseIf (Rates.Values.Count > NumRatesUsed) Then

                    If (Rates.Values(NumRatesUsed).RelativeJulianDay.Value > StartDay) Then

                        Dim NumDays As Integer = EndDay - StartDay
                        Dim Mortality As Double = CalculateMortality(Rate, NumDays, mortalityAdjustmentFactor)

                        CumulativeSurvival *= (1 - Mortality)

                    End If

                End If

                StartDay = EndDay + 1

            Else

                Dim NumDays As Integer = (EndDay - StartDay)

                If NumDays > 0 Then

                    Dim Mortality As Double = CalculateMortality(Rate, NumDays, mortalityAdjustmentFactor)
                    CumulativeSurvival *= (1 - Mortality)

                End If

                Exit For

            End If

        Next

        Return (1 - CumulativeSurvival)

    End Function

    Private Sub AddStartDayAgeZeroCohorts(
        ByVal stratum As Stratum,
        ByVal iteration As Integer,
        ByVal timestep As Integer)

        Dim MaleOffspring As Double = 0
        Dim FemaleOffspring As Double = 0
        Dim TimestepToUse As Integer = timestep

        For Each Cohort In stratum.AgeSexCohorts

            If (Cohort.Sex <> Gender.Female) Then
                Continue For
            End If

            Dim AgeClassId As Integer = GetAgeClassIdFromAge(Cohort.Age)
            Dim opf As OffspringPerFemaleValue = Me.m_OffspringPerFemaleValueMap.GetItem(stratum.Id, AgeClassId, iteration, timestep)

            If (opf IsNot Nothing) Then

                Dim FecundityAdjustment As Double = Me.m_DemographicRateShiftMap.GetFecundityAdjustment(iteration, timestep, AgeClassId)
                Dim NumOffspringPerFemale = CalculateOffspringPerFemale(opf, FecundityAdjustment)
                Dim OffspringAgeClassId As Integer = GetAgeClassIdFromAge(0)
                Dim RelativeEndDay As Integer = CalculateOffspringRelativeCountDay(stratum.Id, iteration, timestep, AgeClassId)
                Dim RelativeStartDay As Integer = 0

                ' If the calf per cow count day is between birth and census 
                ' calculate the number of calves surviving from count to census
                ' Otherwise its the number of days from census to count
                If ((opf.CountJulianDay >= Me.m_OffspringPerFemaleBirthJDay) And (opf.CountJulianDay < Me.m_RunControl.StartJulianDay)) Then
                    RelativeStartDay = RelativeEndDay
                    ' 365 day year but zero indexed
                    RelativeEndDay = 364
                    ' Because data is before the census we need the previous years rate
                    TimestepToUse = timestep - 1
                End If

                Dim SurvivalToCountDayM As Double = (1 - (CalculateTimePeriodMortality(
                    stratum, iteration, TimestepToUse, Gender.Male, OffspringAgeClassId, RelativeStartDay, RelativeEndDay)))

                Dim SurvivalToCountDayF As Double = (1 - (CalculateTimePeriodMortality(
                    stratum, iteration, TimestepToUse, Gender.Female, OffspringAgeClassId, RelativeStartDay, RelativeEndDay)))

                Dim dm As Double = 0.0
                Dim df As Double = 0.0

                ' If the count day is between birth and census
                If ((opf.CountJulianDay >= Me.m_OffspringPerFemaleBirthJDay) And (opf.CountJulianDay < Me.m_RunControl.StartJulianDay)) Then

                    ' Calculate the number of mothers at the count day
                    ' TODO: So far we are not including harvest mortality of mothers as part of this calculation
                    Dim MotherMortalitySinceCount As Double = CalculateTimePeriodMortality(stratum, iteration, TimestepToUse, Gender.Female, AgeClassId, RelativeStartDay, RelativeEndDay)

                    If MotherMortalitySinceCount < 1.0 Then

                        ' Count day is before census so divide by survival rate
                        Dim NumberOfMothers As Double = Cohort.NumIndividuals / (1.0 - MotherMortalitySinceCount)

                        ' Calculate the number of ofspring
                        ' Count day is before the census to need to multiply by survival rate
                        dm = (0.5 * NumOffspringPerFemale * NumberOfMothers) * SurvivalToCountDayM
                        df = (0.5 * NumOffspringPerFemale * NumberOfMothers) * SurvivalToCountDayF

                    End If

                Else

                    ' Calculate the number of mothers at the count day
                    ' Count day is after census so multiply by survival rate
                    ' TODO: So far we are not including harvest mortality of mothers as part of this calculation
                    Dim MotherMortalityToCount As Double = CalculateTimePeriodMortality(stratum, iteration, TimestepToUse, Gender.Female, AgeClassId, RelativeStartDay, RelativeEndDay)
                    Dim NumberOfMothers As Double = Cohort.NumIndividuals * (1 - MotherMortalityToCount)

                    ' Calculate the number of ofspring
                    ' Count day is after the census to need to divide by survival rate
                    If SurvivalToCountDayM > 0.0 Then
                        dm = (0.5 * NumOffspringPerFemale * NumberOfMothers) / SurvivalToCountDayM
                    End If

                    If SurvivalToCountDayF > 0.0 Then
                        df = (0.5 * NumOffspringPerFemale * NumberOfMothers) / SurvivalToCountDayF
                    End If

                End If

                MaleOffspring += dm
                FemaleOffspring += df

            End If

        Next

        Dim RelAge As Integer = (Me.m_RunControl.MinimumTimestep - timestep)

        If (MaleOffspring > 0) Then
            stratum.AgeSexCohorts.Add(New AgeSexCohort(0, RelAge - 1, Gender.Male, MaleOffspring))
        End If

        If (FemaleOffspring > 0) Then
            stratum.AgeSexCohorts.Add(New AgeSexCohort(0, RelAge - 1, Gender.Female, FemaleOffspring))
        End If

    End Sub

    Private Shared Sub RemoveStartDayAgeZeroCohorts(ByVal stratum As Stratum, ByVal RelAge As Integer)

        Dim mkey As New TwoIntegerLookupKey(RelAge - 1, Gender.Male)
        Dim fkey As New TwoIntegerLookupKey(RelAge - 1, Gender.Female)

        stratum.AgeSexCohorts.Remove(mkey)
        stratum.AgeSexCohorts.Remove(fkey)

    End Sub

    ''' <summary>
    ''' Determines if the specified list of rates contains at least one non-wildcard rate
    ''' </summary>
    ''' <param name="rates"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function AtLeastOneNonWildcardRate(ByVal rates As SortedList(Of Integer, AnnualizedMortalityRate)) As Boolean

        If (rates Is Nothing) Then
            Return False
        End If

        For Each k As Integer In rates.Keys

            If (k <> SortedKeyMapBase.WildcardKey) Then
                Return True
            End If

        Next

        Return False

    End Function

    ''' <summary>
    ''' Gets the Wildcard rate if one exists
    ''' </summary>
    ''' <param name="rates"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetWildcardRate(ByVal rates As SortedList(Of Integer, AnnualizedMortalityRate)) As AnnualizedMortalityRate

        If (rates Is Nothing) Then
            Return Nothing
        End If

        For Each k As Integer In rates.Keys

            If (k = SortedKeyMapBase.WildcardKey) Then
                Return rates(k)
            End If

        Next

        Return Nothing

    End Function

End Class
