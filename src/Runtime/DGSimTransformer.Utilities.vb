'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'**********************************************************************************************

Imports SyncroSim.Common

Partial Class DGSimTransformer

    ''' <summary>
    ''' Calculates the mortality over a specific time period
    ''' </summary>
    ''' <param name="rate"></param>
    ''' <param name="numDays"></param>
    ''' <param name="mortalityAdjustmentFactor"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Formula: {10^[(Log10(rate)/number of days]}^365
    ''' </remarks>
    Friend Shared Function CalculateMortality(
        ByVal rate As Double,
        ByVal numDays As Integer,
        ByVal mortalityAdjustmentFactor As Double) As Double

        Debug.Assert(numDays <> 0)

        Dim rt As Double = rate + mortalityAdjustmentFactor

        If (rt < 0.0) Then
            rt = 0.0
        ElseIf (rt > 1.0) Then
            rt = 1.0
        End If

        Dim d0 As Double = Math.Log((1 - rt), 10)
        Dim d1 As Double = d0 / 365
        Dim d2 As Double = Math.Pow(10, d1)
        Dim d3 As Double = Math.Pow(d2, numDays)

        Return (1 - d3)

    End Function

    Private Shared Function CalculateMortality(
        ByVal amr As AnnualizedMortalityRate,
        ByVal numDays As Integer,
        ByVal mortalityAdjustmentFactor As Double) As Double

        If (numDays = 0) Then
            Return 0.0
        End If

        Return CalculateMortality(
            amr.CurrentValue,
            numDays,
            mortalityAdjustmentFactor)

    End Function

    Private Shared Function CalculateOffspringPerFemale(
        ByVal opf As OffspringPerFemaleValue,
        ByVal fecundityAdjustmentFactor As Double) As Double

        Dim v As Double = (opf.CurrentValue + fecundityAdjustmentFactor)

        If (v < 0.0) Then
            v = 0.0
        ElseIf (v > 1.0) Then
            v = 1.0
        End If

        Return v

    End Function

    ''' <summary>
    ''' Calculates the offspring relative count day for the specified criteria
    ''' </summary>
    ''' <param name="stratumId"></param>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <param name="ageClassId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CalculateOffspringRelativeCountDay(
        ByVal stratumId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal ageClassId As Integer) As Integer

        Dim opf = Me.m_OffspringPerFemaleValueMap.GetItem(stratumId, ageClassId, iteration, timestep)

        If (opf Is Nothing) Then
            Return 1
        End If

        If (opf.RelativeJulianDay.HasValue) Then

            Return opf.RelativeJulianDay.Value

        ElseIf (opf.CountJulianDay.HasValue) Then

            If (opf.CountJulianDay >= Me.m_RunControl.StartJulianDay) Then
                opf.RelativeJulianDay = opf.CountJulianDay - Me.m_RunControl.StartJulianDay
            Else
                opf.RelativeJulianDay = 365 - (Me.m_RunControl.StartJulianDay - opf.CountJulianDay)
            End If

            Return opf.RelativeJulianDay.Value

        Else
            Return 1
        End If

    End Function

    Private Function GetPopulationSize(
        ByVal stratum As Stratum,
        ByVal ageClassId As Nullable(Of Integer),
        ByVal sex As Nullable(Of Sex)) As Double

        If (Not ageClassId.HasValue And Not sex.HasValue) Then
            Return CalculateStratumPopulationSize(stratum)
        Else

            Dim PopSize As Double = 0

            For Each cohort As AgeSexCohort In stratum.AgeSexCohorts

                Dim id As Integer = GetAgeClassIdFromAge(cohort.Age)

                If ((Not ageClassId.HasValue) OrElse (id = ageClassId)) Then

                    If ((Not sex.HasValue) OrElse (cohort.Sex = sex.Value)) Then
                        PopSize += cohort.NumIndividuals
                    End If

                End If

            Next

            Return PopSize

        End If

    End Function

    Private Shared Function GetPopulationSizeByAgeRangeAndSex(
        ByVal stratum As Stratum,
        ByVal ageMin As Integer,
        ByVal ageMax As Integer,
        ByVal sex As Nullable(Of Sex)) As Double

        Dim PopSize As Double = 0

        For Each cohort As AgeSexCohort In stratum.AgeSexCohorts

            If (cohort.Age >= ageMin And cohort.Age <= ageMax) Then

                If ((Not sex.HasValue) OrElse (cohort.Sex = sex.Value)) Then
                    PopSize += cohort.NumIndividuals
                End If

            End If

        Next

        Return PopSize

    End Function

    Private Shared Sub ResetAnnualHarvest(ByVal stratum As Stratum)

        For Each cohort As AgeSexCohort In stratum.AgeSexCohorts
            cohort.AnnualHarvest = 0
        Next

    End Sub

    Private Sub DetermineAnnualHarvest(
        ByVal stratum As Stratum,
        ByVal iteration As Integer,
        ByVal timestep As Integer)

        ResetAnnualHarvest(stratum)

        Dim recs As SortedList(Of Integer, AnnualHarvestValue) =
            Me.m_AnnualHarvestValueMap.GetItems(stratum.Id, iteration, timestep)

        If (recs Is Nothing) Then
            Return
        End If

        For Each ah As AnnualHarvestValue In recs.Values

            For Each cohort As AgeSexCohort In stratum.AgeSexCohorts

                Dim id As Integer = GetAgeClassIdFromAge(cohort.Age)

                If ((Not ah.AgeClassId.HasValue) OrElse (id = ah.AgeClassId.Value)) Then

                    If ((Not ah.Sex.HasValue) OrElse (cohort.Sex = ah.Sex.Value)) Then

                        Dim pSize As Double = GetPopulationSize(stratum, ah.AgeClassId, ah.Sex)
                        Dim InputHarvestAmount As Double = 0.0

                        If (pSize > 0.0) Then

                            If (Me.m_AnnualHarvestSpecification = AnnualHarvestSpecification.PercentageOfCohort) Then

                                pSize = 1.0
                                InputHarvestAmount = ah.CurrentValue / 100.0

                            ElseIf (Me.m_AnnualHarvestSpecification = AnnualHarvestSpecification.AbsoluteNumber) Then
                                InputHarvestAmount = ah.CurrentValue
                            Else

                                Dim popsize As Double = GetPopulationSizeByAgeRangeAndSex(
                                    stratum,
                                    Me.m_AnnualHarvestPopFilterMinAge,
                                    Me.m_AnnualHarvestPopFilterMaxAge,
                                    Me.m_AnnualHarvestPopFilterSex)

                                InputHarvestAmount = (ah.CurrentValue / 100.0) * popsize

                            End If

                        End If

                        Dim d As Double = 0.0

                        If pSize > 0.0 Then
                            d = (InputHarvestAmount * cohort.NumIndividuals / pSize)
                        End If

                        cohort.AnnualHarvest = d

                    End If

                End If

            Next

        Next

    End Sub

    Private Function GetAgeClassIdFromAge(ByVal age As Integer) As Integer

        For Each acr As AgeClassRange In Me.m_AgeClassRanges

            If (age <= acr.MaximumAge) Then
                Return acr.AgeClassId
            End If

        Next

        Return Me.m_AgeClassRanges.Last.AgeClassId

    End Function

    ''' <summary>
    ''' Gets the sum of the relative amount for the initial population distribution
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDistSumOfRelativeAmount() As Double

        Dim val As Double = 0.0
        Debug.Assert(Me.m_InitialPopulationDistributions.Count > 0)

        For Each ipd As InitialPopulationDistribution In Me.m_InitialPopulationDistributions
            val += ipd.RelativeAmount
        Next

        Return val

    End Function

    Private Sub AddAgeCohort(
        ByVal age As Integer,
        ByVal sex As Sex,
        ByVal numIndArg As Double,
        ByVal stratumId As Nullable(Of Integer))

        Debug.Assert(numIndArg >= 0)

        If (numIndArg = 0) Then
            Return
        End If

        Dim key As New TwoIntegerLookupKey(age, CInt(sex))
        Dim ids As New List(Of Integer)

        If (stratumId.HasValue) Then
            ids.Add(stratumId.Value)
        Else

            For Each s As Stratum In Me.m_Strata
                ids.Add(s.Id)
            Next

        End If

        For Each id As Integer In ids

            Dim Stratum As Stratum = Me.m_Strata(id)

            If (Stratum.AgeSexCohorts.Contains(key)) Then
                Stratum.AgeSexCohorts(key).NumIndividuals += numIndArg
            Else
                Stratum.AgeSexCohorts.Add(New AgeSexCohort(age, age, sex, numIndArg))
            End If

        Next

    End Sub

    Private Shared Function CalculateStratumPopulationSize(ByVal stratum As Stratum) As Double


        Dim Total As Double = 0

        For Each Cohort As AgeSexCohort In stratum.AgeSexCohorts
            Total += Cohort.NumIndividuals
        Next

        Return Total

    End Function

    Private Function IsOutsideCensusDataRange(ByVal stratum As Stratum, ByVal timestep As Integer) As Boolean

        Dim cd As CensusData = Me.m_CensusDataMap.GetItem(stratum.Id, timestep)

        If (cd IsNot Nothing) Then

            Dim TotalPopSize As Double = CalculateStratumPopulationSize(stratum)
            Dim M2FRatio As Double = CalculateM2FRatio(stratum.AgeSexCohorts)

            If (TotalPopSize < cd.MinimumPopulation Or TotalPopSize > cd.MaximumPopulation) Then
                Return True
            End If

            Dim mincomp As Double = 0.0
            Dim maxcomp As Double = Double.MaxValue

            If (cd.MinimumM2FRatio.HasValue) Then
                mincomp = cd.MinimumM2FRatio.Value
            End If

            If (cd.MaximumM2FRatio.HasValue) Then
                maxcomp = cd.MaximumM2FRatio.Value
            End If

            If (M2FRatio < mincomp Or M2FRatio > maxcomp) Then
                Return True
            End If

        End If

        Return False

    End Function

    Private Shared Function CalculatePopulationBySex(ByVal cohorts As AgeSexCohortCollection, ByVal sex As Sex) As Integer

        Dim t As Double = 0

        For Each c As AgeSexCohort In cohorts

            If (c.Age > 0) Then

                If (c.Sex = sex) Then
                    t += c.NumIndividuals
                End If

            End If

        Next

        Return CInt(t)

    End Function

    Private Shared Function CalculateM2FRatio(ByVal cohorts As AgeSexCohortCollection) As Double

        Dim TotalMale As Double = 0
        Dim TotalFemale As Double = 0

        For Each c As AgeSexCohort In cohorts

            If (c.Age > 0) Then

                If (c.Sex = Sex.Male) Then
                    TotalMale += c.NumIndividuals
                Else
                    TotalFemale += c.NumIndividuals
                End If

            End If

        Next

        If (TotalFemale = 0) Then

            Debug.Assert(False)
            Return TotalMale

        Else
            Return TotalMale / TotalFemale
        End If

    End Function

    Private Shared Function AnyAgeZeroIndividuals(ByVal cohorts As AgeSexCohortCollection) As Boolean

        For Each c As AgeSexCohort In cohorts

            If (c.Age = 0 And c.NumIndividuals > 0) Then
                Return True
            End If

        Next

        Return False

    End Function

    Private Shared Sub ValidateAnnualHarvestValue(ByVal ah As AnnualHarvestValue)

        Dim err As Boolean = False

        If (ah.Mean > 100.0) Then
            err = True
        ElseIf (ah.DistributionMin.HasValue AndAlso ah.DistributionMin.Value > 100.0) Then
            err = True
        ElseIf (ah.DistributionMax.HasValue AndAlso ah.DistributionMax.Value > 100.0) Then
            err = True
        ElseIf (ah.DistributionSD.HasValue AndAlso ah.DistributionSD.Value > 100.0) Then
            err = True
        End If

        If (err) Then
            Throw New InvalidOperationException(My.Resources.DGSIM_ERROR_ANNUAL_HARVEST_VALUES)
        End If

    End Sub

End Class
