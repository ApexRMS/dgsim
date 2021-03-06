﻿'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'**********************************************************************************************

Imports SyncroSim.Common

Partial Class DGSimTransformer

    Private Sub AddPopSizeOutputToCollection(ByVal cohort As AgeSexCohort, ByVal stratum As Stratum)

        Dim AgeClassId As Integer = GetAgeClassIdFromAge(cohort.Age)
        Dim key As New ThreeIntegerLookupKey(stratum.Id, cohort.Sex, AgeClassId)

        If (Me.m_SummaryPopSizeOutput.Contains(key)) Then

            Dim o As SummaryOutputPopulationSize = Me.m_SummaryPopSizeOutput(key)
            o.Population += cohort.NumIndividuals

        Else

            Dim o As New SummaryOutputPopulationSize(stratum.Id, cohort.Sex, AgeClassId, cohort.NumIndividuals)
            Me.m_SummaryPopSizeOutput.Add(o)

        End If

    End Sub

    Private Sub ProcessSummaryPopSizeOutputData(iteration As Integer, timestep As Integer)

        For Each o As SummaryOutputPopulationSize In Me.m_SummaryPopSizeOutput

            Dim NewRow As DataRow = Me.m_OutputPopSizeDataTable.NewRow

            NewRow(DATASHEET_STRATUM_ID_COLUMN_NAME) = o.StratumId
            NewRow(DATASHEET_ITERATION_COLUMN_NAME) = iteration
            NewRow(DATASHEET_TIMESTEP_COLUMN_NAME) = timestep
            NewRow(DATASHEET_SEX_COLUMN_NAME) = CInt(o.Sex)
            NewRow(DATASHEET_AGE_CLASS_ID_COLUMN_NAME) = o.AgeClassId
            NewRow(OUTPUT_POPULATION_SIZE_POPULATION_COLUMN_NAME) = o.Population

            Me.m_OutputPopSizeDataTable.Rows.Add(NewRow)

        Next

        Me.m_SummaryPopSizeOutput.Clear()

    End Sub

    Private Sub AddHarvestOutputToCollection(ByVal cohort As AgeSexCohort, ByVal stratum As Stratum, ByVal harvestAmount As Double)

        Dim AgeClassId As Integer = GetAgeClassIdFromAge(cohort.Age)
        Dim key As New ThreeIntegerLookupKey(stratum.Id, cohort.Sex, AgeClassId)

        If (Me.m_SummaryHarvestOutput.Contains(key)) Then

            Dim o As SummaryOutputHarvest = Me.m_SummaryHarvestOutput(key)
            o.Harvest += harvestAmount

        Else

            Dim o As New SummaryOutputHarvest(stratum.Id, cohort.Sex, AgeClassId, harvestAmount)
            Me.m_SummaryHarvestOutput.Add(o)

        End If

    End Sub

    Private Sub ProcessSummaryHarvestOutputData(iteration As Integer, timestep As Integer)

        For Each o As SummaryOutputHarvest In Me.m_SummaryHarvestOutput

            Dim NewRow As DataRow = Me.m_OutputHarvestDataTable.NewRow

            NewRow(DATASHEET_STRATUM_ID_COLUMN_NAME) = o.StratumId
            NewRow(DATASHEET_ITERATION_COLUMN_NAME) = iteration
            NewRow(DATASHEET_TIMESTEP_COLUMN_NAME) = timestep
            NewRow(DATASHEET_SEX_COLUMN_NAME) = CInt(o.Sex)
            NewRow(DATASHEET_AGE_CLASS_ID_COLUMN_NAME) = o.AgeClassId
            NewRow(OUTPUT_HARVEST_HARVEST_COLUMN_NAME) = o.Harvest

            Me.m_OutputHarvestDataTable.Rows.Add(NewRow)

        Next

        Me.m_SummaryHarvestOutput.Clear()

    End Sub

    Private Sub AddRecruitsToOutputToCollection(ByVal cohort As AgeSexCohort, ByVal stratum As Stratum, ByVal numRecruits As Double, offspringSex As Sex)

        Dim MotherAgeClassId As Integer = GetAgeClassIdFromAge(cohort.Age)
        Dim key As New ThreeIntegerLookupKey(stratum.Id, MotherAgeClassId, offspringSex)

        If (Me.m_SummaryRecruitsOutput.Contains(key)) Then

            Dim o As SummaryOutputRecruits = Me.m_SummaryRecruitsOutput(key)
            o.Recruits += numRecruits

        Else

            Dim o As New SummaryOutputRecruits(stratum.Id, MotherAgeClassId, offspringSex, numRecruits)
            Me.m_SummaryRecruitsOutput.Add(o)

        End If

    End Sub

    Private Sub ProcessSummaryRecruitsOutputData(iteration As Integer, timestep As Integer)

        For Each o As SummaryOutputRecruits In Me.m_SummaryRecruitsOutput

            Dim NewRow As DataRow = Me.m_OutputRecruitsDataTable.NewRow

            NewRow(DATASHEET_STRATUM_ID_COLUMN_NAME) = o.StratumId
            NewRow(DATASHEET_ITERATION_COLUMN_NAME) = iteration
            NewRow(DATASHEET_TIMESTEP_COLUMN_NAME) = timestep
            NewRow(DATASHEET_MOTHER_AGECLASS_ID_COLUMN_NAME) = o.MotherAgeClassId
            NewRow(DATASHEET_OFFSPRING_SEX_COLUMN_NAME) = CInt(o.OffspringSex)
            NewRow(OUTPUT_RECRUITS_COLUMN_NAME) = o.Recruits

            Me.m_OutputRecruitsDataTable.Rows.Add(NewRow)

        Next

        Me.m_SummaryRecruitsOutput.Clear()

    End Sub

    Private Sub AddMortalityOutputToCollection(ByVal cohort As AgeSexCohort, ByVal stratum As Stratum, ByVal mortalityAmount As Double)

        Dim AgeClassId As Integer = GetAgeClassIdFromAge(cohort.Age)
        Dim key As New ThreeIntegerLookupKey(stratum.Id, cohort.Sex, AgeClassId)

        If (Me.m_SummaryMortalityOutput.Contains(key)) Then

            Dim o As SummaryOutputMortality = Me.m_SummaryMortalityOutput(key)
            o.Mortality += mortalityAmount

        Else

            Dim o As New SummaryOutputMortality(stratum.Id, cohort.Sex, AgeClassId, mortalityAmount)
            Me.m_SummaryMortalityOutput.Add(o)

        End If

    End Sub

    Private Sub ProcessSummaryMortalityOutputData(iteration As Integer, timestep As Integer)

        For Each o As SummaryOutputMortality In Me.m_SummaryMortalityOutput

            Dim NewRow As DataRow = Me.m_OutputMortalityDataTable.NewRow

            NewRow(DATASHEET_STRATUM_ID_COLUMN_NAME) = o.StratumId
            NewRow(DATASHEET_ITERATION_COLUMN_NAME) = iteration
            NewRow(DATASHEET_TIMESTEP_COLUMN_NAME) = timestep
            NewRow(DATASHEET_SEX_COLUMN_NAME) = CInt(o.Sex)
            NewRow(DATASHEET_AGE_CLASS_ID_COLUMN_NAME) = o.AgeClassId
            NewRow(OUTPUT_MORTALITY_MORTALITY_COLUMN_NAME) = o.Mortality

            Me.m_OutputMortalityDataTable.Rows.Add(NewRow)

        Next

        Me.m_SummaryMortalityOutput.Clear()

    End Sub

    Private Sub ProcessPosteriorDistributionOutput(
        ByVal stratumId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal hasCensusData As Boolean,
        ByVal isFiltered As Boolean)

        Me.ProcessPosteriorAnnualHarvest(stratumId, iteration, timestep, hasCensusData, isFiltered)
        Me.ProcessPosteriorMortalityRate(stratumId, iteration, timestep, hasCensusData, isFiltered)
        Me.ProcessPosteriorOffspringPerFemale(stratumId, iteration, timestep, hasCensusData, isFiltered)

    End Sub

    Private Sub ProcessPosteriorAnnualHarvest(
        ByVal stratumId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal hasCensusData As Boolean,
        ByVal isFiltered As Boolean)

        Dim recs As SortedList(Of Integer, AnnualHarvestValue) =
            Me.m_AnnualHarvestValueMap.GetItems(stratumId, iteration, timestep)

        If (recs IsNot Nothing) Then

            Debug.Assert(recs.Count > 0)

            For Each r As AnnualHarvestValue In recs.Values

                Dim NewRow As DataRow = Me.m_OutputPosteriorDistDataTable.NewRow

                NewRow(DATASHEET_STRATUM_ID_COLUMN_NAME) = stratumId
                NewRow(DATASHEET_ITERATION_COLUMN_NAME) = iteration
                NewRow(DATASHEET_TIMESTEP_COLUMN_NAME) = timestep
                NewRow(DATASHEET_HAS_CENSUS_DATA_COLUMN_NAME) = CInt(hasCensusData)
                NewRow(DATASHEET_JULIAN_DAY_COLUMN_NAME) = DBNull.Value
                NewRow(DATASHEET_SEX_COLUMN_NAME) = GetNullableDatabaseValue(r.Sex)
                NewRow(DATASHEET_AGE_CLASS_ID_COLUMN_NAME) = GetNullableDatabaseValue(r.AgeClassId)
                NewRow(OUTPUT_POSTERIOR_DIST_VALUE_DATASHEET_VARIABLE_COLUMN_NAME) = CInt(PosteriorDistribution.Harvest)
                NewRow(OUTPUT_POSTERIOR_DIST_VALUE_DATASHEET_ISFILTERED_COLUMN_NAME) = CInt(isFiltered)
                NewRow(OUTPUT_POSTERIOR_DIST_VALUE_DATASHEET_VALUE_COLUMN_NAME) = CInt(r.CurrentValue)

                Me.m_OutputPosteriorDistDataTable.Rows.Add(NewRow)

            Next

        End If

    End Sub

    Private Sub ProcessPosteriorMortalityRate(
        ByVal stratumId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal hasCensusData As Boolean,
        ByVal isFiltered As Boolean)

        Me.ProcessMortRateBySex(stratumId, Sex.Male, iteration, timestep, hasCensusData, isFiltered)
        Me.ProcessMortRateBySex(stratumId, Sex.Female, iteration, timestep, hasCensusData, isFiltered)

    End Sub

    Private Sub ProcessMortRateBySex(
        ByVal stratumId As Integer,
        ByVal sex As Sex,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal hasCensusData As Boolean,
        ByVal isFiltered As Boolean)

        For Each a As AgeClass In Me.m_AgeClasses

            Dim recs As SortedList(Of Integer, AnnualizedMortalityRate) =
                  Me.m_AnnualizedMortalityRateMap.GetItems(stratumId, sex, a.Id, iteration, timestep)

            If (recs IsNot Nothing) Then

                Debug.Assert(recs.Count > 0)

                For Each r As AnnualizedMortalityRate In recs.Values
                    WriteAnnualizedMortalityRecord(r, stratumId, iteration, timestep, a.Id, sex, hasCensusData, isFiltered)
                Next

            End If

        Next

    End Sub

    Private Sub WriteAnnualizedMortalityRecord(
        ByVal r As AnnualizedMortalityRate,
        ByVal stratumId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal a As Integer,
        ByVal sex As Sex,
        ByVal hasCensusData As Boolean,
        ByVal isFiltered As Boolean)

        Dim NewRow As DataRow = Me.m_OutputPosteriorDistDataTable.NewRow

        NewRow(DATASHEET_STRATUM_ID_COLUMN_NAME) = stratumId
        NewRow(DATASHEET_ITERATION_COLUMN_NAME) = iteration
        NewRow(DATASHEET_TIMESTEP_COLUMN_NAME) = timestep
        NewRow(DATASHEET_HAS_CENSUS_DATA_COLUMN_NAME) = CInt(hasCensusData)
        NewRow(DATASHEET_JULIAN_DAY_COLUMN_NAME) = GetNullableDatabaseValue(r.JulianDay)
        NewRow(DATASHEET_SEX_COLUMN_NAME) = sex
        NewRow(DATASHEET_AGE_CLASS_ID_COLUMN_NAME) = a
        NewRow(OUTPUT_POSTERIOR_DIST_VALUE_DATASHEET_VARIABLE_COLUMN_NAME) = CInt(PosteriorDistribution.Mortality)
        NewRow(OUTPUT_POSTERIOR_DIST_VALUE_DATASHEET_ISFILTERED_COLUMN_NAME) = CInt(isFiltered)
        NewRow(OUTPUT_POSTERIOR_DIST_VALUE_DATASHEET_VALUE_COLUMN_NAME) = r.CurrentValue

        Me.m_OutputPosteriorDistDataTable.Rows.Add(NewRow)

    End Sub

    Private Sub ProcessPosteriorOffspringPerFemale(
        ByVal stratumId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal hasCensusData As Boolean,
        ByVal isFiltered As Boolean)

        For Each a As AgeClass In Me.m_AgeClasses

            Dim r As OffspringPerFemaleValue = Me.m_OffspringPerFemaleValueMap.GetItem(stratumId, a.Id, iteration, timestep)

            If (r IsNot Nothing) Then

                Dim NewRow As DataRow = Me.m_OutputPosteriorDistDataTable.NewRow

                NewRow(DATASHEET_STRATUM_ID_COLUMN_NAME) = stratumId
                NewRow(DATASHEET_ITERATION_COLUMN_NAME) = iteration
                NewRow(DATASHEET_TIMESTEP_COLUMN_NAME) = timestep
                NewRow(DATASHEET_HAS_CENSUS_DATA_COLUMN_NAME) = CInt(hasCensusData)
                NewRow(DATASHEET_JULIAN_DAY_COLUMN_NAME) = GetNullableDatabaseValue(r.CountJulianDay)
                NewRow(DATASHEET_SEX_COLUMN_NAME) = CInt(Sex.Female)
                NewRow(DATASHEET_AGE_CLASS_ID_COLUMN_NAME) = GetNullableDatabaseValue(r.AgeClassId)
                NewRow(OUTPUT_POSTERIOR_DIST_VALUE_DATASHEET_VARIABLE_COLUMN_NAME) = CInt(PosteriorDistribution.Offspring)
                NewRow(OUTPUT_POSTERIOR_DIST_VALUE_DATASHEET_ISFILTERED_COLUMN_NAME) = CInt(isFiltered)
                NewRow(OUTPUT_POSTERIOR_DIST_VALUE_DATASHEET_VALUE_COLUMN_NAME) = r.CurrentValue

                Me.m_OutputPosteriorDistDataTable.Rows.Add(NewRow)

            End If

        Next

    End Sub

End Class
