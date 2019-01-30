'*********************************************************************************************
' DG-Sim: A SyncroSim Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core

Partial Class DGSimTransformer

    Private m_Strata As New StratumCollection
    Private m_AgeClasses As New AgeClassCollection
    Private m_RunControl As RunControl
    Private m_InitialPopulationSize As InitialPopulationSize
    Private m_InitialPopulationDistributions As New InitialPopulationDistributionCollection
    Private m_AgeClassRanges As New AgeClassRangeCollection
    Private m_OffspringPerFemaleValues As New OffspringPerFemaleValueCollection
    Private m_AnnualizedMortalityRates As New AnnualizedMortalityRateCollection
    Private m_AnnualHarvestValues As New AnnualHarvestValueCollection
    Private m_DemographicRateShifts As New DemographicRateShiftCollection
    Private m_CensusData As New CensusDataCollection
    Private m_SummaryPopSizeOutput As New SummaryOutputPopulationSizeCollection()
    Private m_SummaryHarvestOutput As New SummaryOutputHarvestCollection()
    Private m_SummaryRecruitsOutput As New SummaryOutputRecruitsCollection()
    Private m_SummaryMortalityOutput As New SummaryOutputMortalityCollection()

    Private Sub InitializeCollections()

        Me.InitializeStratumCollection()
        Me.InitializeAgeClassCollection()
        Me.InitializeInitialPopulationSize()
        Me.InitializeInitialPopulationDistribution()
        Me.InitializeAgeClassRanges()
        Me.InitializeOffspringPerFemaleValues()
        Me.InitializeAnnualizedMortalityRate()
        Me.InitializeAnnualHarvestValues()
        Me.InitializeDemographicRateShifts()
        Me.InitializeCensusData()

    End Sub

    Private Sub InitializeStratumCollection()

        Debug.Assert(Me.m_Strata.Count = 0)
        Dim ds As DataSheet = Me.Project.GetDataSheet(STRATUM_DATASHEET_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim id As Integer = CInt(dr(ds.PrimaryKeyColumn.Name))
            Me.m_Strata.Add(New Stratum(id))

        Next

    End Sub

    Private Sub InitializeAgeClassCollection()

        Debug.Assert(Me.m_AgeClasses.Count = 0)
        Dim ds As DataSheet = Me.Project.GetDataSheet(AGE_CLASS_DATASHEET_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim id As Integer = CInt(dr(ds.PrimaryKeyColumn.Name))
            Me.m_AgeClasses.Add(New AgeClass(id))

        Next

    End Sub

    Private Sub InitializeRunControl()

        Debug.Assert(Me.m_RunControl Is Nothing)

        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(RUN_CONTROL_DATASHEET_NAME)
        Dim dr As DataRow = ds.GetDataRow()

        Me.m_RunControl = New RunControl(
            CInt(dr("MinimumIteration")),
            CInt(dr("MaximumIteration")),
            CInt(dr("MinimumTimestep")),
            CInt(dr("MaximumTimestep")),
            CInt(dr("StartJulianDay")))

        Me.MinimumIteration = Me.m_RunControl.MinimumIteration
        Me.MaximumIteration = Me.m_RunControl.MaximumIteration
        Me.MinimumTimestep = Me.m_RunControl.MinimumTimestep
        Me.MaximumTimestep = Me.m_RunControl.MaximumTimestep

    End Sub

    Private Sub InitializeInitialPopulationSize()

        Debug.Assert(Me.m_InitialPopulationSize Is Nothing)

        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(INITIAL_POPULATION_SIZE_DATASHEET_NAME)
        Dim dr As DataRow = ds.GetDataRow()

        Try

            Me.m_InitialPopulationSize = New InitialPopulationSize(
                GetNullableDouble(dr, DATASHEET_MEAN_COLUMN_NAME),
                GetNullableInt(dr, DATASHEET_DISTRIBUTION_TYPE_COLUMN_NAME),
                GetNullableDouble(dr, DATASHEET_DISTRIBUTION_SD_COLUMN_NAME),
                GetNullableDouble(dr, DATASHEET_DISTRIBUTION_MIN_COLUMN_NAME),
                GetNullableDouble(dr, DATASHEET_DISTRIBUTION_MAX_COLUMN_NAME),
                Me.m_DistributionProvider)

        Catch ex As Exception
            Throw New ArgumentException(ds.DisplayName & vbCrLf & ex.Message)
        End Try

    End Sub

    Private Sub InitializeInitialPopulationDistribution()

        Debug.Assert(Me.m_InitialPopulationDistributions.Count = 0)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(INITIAL_POPULATION_DISTRIBUTION_DATASHEET_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Me.m_InitialPopulationDistributions.Add(New InitialPopulationDistribution(
                 GetNullableInt(dr, DATASHEET_STRATUM_ID_COLUMN_NAME),
                 CType(GetNullableInt(dr, DATASHEET_SEX_COLUMN_NAME), Nullable(Of Sex)),
                 CInt(dr(DATASHEET_MIN_AGE_COLUMN_NAME)),
                 CInt(dr(DATASHEET_MAX_AGE_COLUMN_NAME)),
                 CDbl(dr(INITIAL_POPULATION_DISTRIBUTION_DATASHEET_RELATIVE_AMOUNT_COLUMN_NAME))))

        Next

    End Sub

    Private Sub InitializeAgeClassRanges()

        Debug.Assert(Me.m_AgeClassRanges.Count = 0)

        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(AGE_CLASS_RANGE_DATASHEET_NAME)
        Dim dv As New DataView(ds.GetData(), Nothing, DATASHEET_MAX_AGE_COLUMN_NAME & " ASC", DataViewRowState.CurrentRows)

        For Each drv As DataRowView In dv

            Dim dr As DataRow = drv.Row
            Dim id As Integer = CInt(dr(DATASHEET_AGE_CLASS_ID_COLUMN_NAME))
            Dim max As Integer = CInt(dr(DATASHEET_MAX_AGE_COLUMN_NAME))

            Me.m_AgeClassRanges.Add(New AgeClassRange(id, max))

        Next

    End Sub

    Private Sub InitializeOffspringPerFemaleValues()

        Debug.Assert(Me.m_OffspringPerFemaleValues.Count = 0)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(OFFSPRING_PER_FEMALE_VALUE_DATASHEET_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim Item As OffspringPerFemaleValue = Nothing

            Try

                Item = New OffspringPerFemaleValue(
                    Me.Project,
                    GetNullableInt(dr, DATASHEET_STRATUM_ID_COLUMN_NAME),
                    GetNullableInt(dr, DATASHEET_ITERATION_COLUMN_NAME),
                    GetNullableInt(dr, DATASHEET_TIMESTEP_COLUMN_NAME),
                    GetNullableInt(dr, DATASHEET_AGE_CLASS_ID_COLUMN_NAME),
                    GetNullableInt(dr, OFFSPRING_PER_FEMALE_COUNT_JULIAN_DAY_COLUMN_NAME),
                    GetNullableDouble(dr, DATASHEET_MEAN_COLUMN_NAME),
                    GetNullableInt(dr, DATASHEET_DISTRIBUTION_TYPE_COLUMN_NAME),
                    GetNullableDouble(dr, DATASHEET_DISTRIBUTION_SD_COLUMN_NAME),
                    GetNullableDouble(dr, DATASHEET_DISTRIBUTION_MIN_COLUMN_NAME),
                    GetNullableDouble(dr, DATASHEET_DISTRIBUTION_MAX_COLUMN_NAME),
                    Me.m_DistributionProvider)

                Item.Initialize()
                Me.m_OffspringPerFemaleValues.Add(Item)

            Catch ex As ArgumentException

                If (Item Is Nothing) Then
                    Throw
                End If

                Throw New ArgumentException(
                    GetCommonFormattedExceptionData(ex, ds, Item.StratumId, Item.Iteration, Item.Timestep, Item.AgeClassId) & vbCrLf &
                    "Count Julian Day: " & FormatNullableInt(Item.CountJulianDay))

            End Try

        Next

        For Each opfv As OffspringPerFemaleValue In Me.m_OffspringPerFemaleValues

            If (opfv.CountJulianDay.HasValue) Then
                opfv.RelativeJulianDay = GetRelativeJulianDay(opfv.CountJulianDay.Value, Me.m_RunControl.StartJulianDay)
            End If

        Next

    End Sub

    Private Sub InitializeAnnualizedMortalityRate()

        Debug.Assert(Me.m_AnnualizedMortalityRates.Count = 0)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(ANNUALIZED_MORTALITY_RATE_DATASHEET_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim Item As AnnualizedMortalityRate = Nothing

            Try

                Item = New AnnualizedMortalityRate(
                    Me.Project,
                    GetNullableInt(dr, DATASHEET_STRATUM_ID_COLUMN_NAME),
                    GetNullableInt(dr, DATASHEET_ITERATION_COLUMN_NAME),
                    GetNullableInt(dr, DATASHEET_TIMESTEP_COLUMN_NAME),
                    GetNullableInt(dr, DATASHEET_AGE_CLASS_ID_COLUMN_NAME),
                    GetNullableInt(dr, DATASHEET_JULIAN_DAY_COLUMN_NAME),
                    CType(GetNullableInt(dr, DATASHEET_SEX_COLUMN_NAME), Nullable(Of Sex)),
                    GetNullableDouble(dr, DATASHEET_MEAN_COLUMN_NAME),
                    GetNullableInt(dr, DATASHEET_DISTRIBUTION_TYPE_COLUMN_NAME),
                    GetNullableDouble(dr, DATASHEET_DISTRIBUTION_SD_COLUMN_NAME),
                    GetNullableDouble(dr, DATASHEET_DISTRIBUTION_MIN_COLUMN_NAME),
                    GetNullableDouble(dr, DATASHEET_DISTRIBUTION_MAX_COLUMN_NAME),
                    Me.m_DistributionProvider)

                Item.Initialize()
                Me.m_AnnualizedMortalityRates.Add(Item)

            Catch ex As ArgumentException

                If (Item Is Nothing) Then
                    Throw
                End If

                Throw New ArgumentException(
                    GetCommonFormattedExceptionData(ex, ds, Item.StratumId, Item.Iteration, Item.Timestep, Item.AgeClassId) & vbCrLf &
                    "Julian Day: " & FormatNullableInt(Item.JulianDay) & vbCrLf &
                    "Sex: " & FormatNullableSexAsString(Item.Sex))

            End Try

        Next

        For Each amr As AnnualizedMortalityRate In Me.m_AnnualizedMortalityRates

            If (amr.JulianDay.HasValue) Then
                amr.RelativeJulianDay = GetRelativeJulianDay(amr.JulianDay.Value, Me.m_RunControl.StartJulianDay)
            End If

        Next

    End Sub

    Private Sub InitializeAnnualHarvestValues()

        Debug.Assert(Me.m_AnnualHarvestValues.Count = 0)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(ANNUAL_HARVEST_VALUE_DATASHEET_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim Item As AnnualHarvestValue = Nothing

            Try

                Item = New AnnualHarvestValue(
                    Me.Project,
                    GetNullableInt(dr, DATASHEET_STRATUM_ID_COLUMN_NAME),
                    GetNullableInt(dr, DATASHEET_ITERATION_COLUMN_NAME),
                    GetNullableInt(dr, DATASHEET_TIMESTEP_COLUMN_NAME),
                    GetNullableInt(dr, DATASHEET_AGE_CLASS_ID_COLUMN_NAME),
                    CType(GetNullableInt(dr, DATASHEET_SEX_COLUMN_NAME), Nullable(Of Sex)),
                    GetNullableDouble(dr, DATASHEET_MEAN_COLUMN_NAME),
                    GetNullableInt(dr, DATASHEET_DISTRIBUTION_TYPE_COLUMN_NAME),
                    GetNullableDouble(dr, DATASHEET_DISTRIBUTION_SD_COLUMN_NAME),
                    GetNullableDouble(dr, DATASHEET_DISTRIBUTION_MIN_COLUMN_NAME),
                    GetNullableDouble(dr, DATASHEET_DISTRIBUTION_MAX_COLUMN_NAME),
                    Me.m_DistributionProvider)

                If (Me.m_AnnualHarvestSpecification = AnnualHarvestSpecification.PercentageOfCohort Or
                    Me.m_AnnualHarvestSpecification = AnnualHarvestSpecification.PercentageOfPopulation) Then

                    ValidateAnnualHarvestValue(Item)

                End If

                Item.Initialize()
                Me.m_AnnualHarvestValues.Add(Item)

            Catch ex As ArgumentException

                If (Item Is Nothing) Then
                    Throw
                End If

                Throw New ArgumentException(
                    GetCommonFormattedExceptionData(ex, ds, Item.StratumId, Item.Iteration, Item.Timestep, Item.AgeClassId) & vbCrLf &
                    "Sex: " & FormatNullableSexAsString(Item.Sex))

            End Try

        Next

    End Sub

    Private Sub InitializeDemographicRateShifts()

        Debug.Assert(Me.m_DemographicRateShifts.Count = 0)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DEMOGRAPHIC_RATE_SHIFT_DATASHEET_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Me.m_DemographicRateShifts.Add(New DemographicRateShift(
                GetNullableInt(dr, DATASHEET_ITERATION_COLUMN_NAME),
                GetNullableInt(dr, DATASHEET_TIMESTEP_COLUMN_NAME),
                GetNullableInt(dr, DATASHEET_AGE_CLASS_ID_COLUMN_NAME),
                GetNullableDouble(dr, DEMOGRAPHIC_RATE_SHIFT_FECUNDITY_COLUMN_NAME),
                GetNullableDouble(dr, DEMOGRAPHIC_RATE_SHIFT_MORTALITY_COLUMN_NAME)))

        Next

    End Sub

    Private Sub InitializeCensusData()

        Debug.Assert(Me.m_CensusData.Count = 0)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(CENSUS_DATASHEET_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Me.m_CensusData.Add(New CensusData(
                CInt(dr(DATASHEET_STRATUM_ID_COLUMN_NAME)),
                CInt(dr(DATASHEET_TIMESTEP_COLUMN_NAME)),
                CInt(dr(CENSUS_DATASHEET_MIN_POP_COLUMN_NAME)),
                CInt(dr(CENSUS_DATASHEET_MAX_POP_COLUMN_NAME)),
                GetNullableDouble(dr, CENSUS_DATASHEET_MIN_M2F_RATIO_COLUMN_NAME),
                GetNullableDouble(dr, CENSUS_DATASHEET_MAX_M2F_RATIO_COLUMN_NAME)))

        Next

    End Sub

End Class

