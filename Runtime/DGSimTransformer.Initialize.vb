'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographics of wildlife populations.
'
' Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports SyncroSim.StochasticTime

Partial Class DGSimTransformer

    Private m_OffspringPerFemaleBirthJDay As Integer
    Private m_AnnualHarvestSpecification As AnnualHarvestSpecification
    Private m_AnnualHarvestPopFilterSex As Nullable(Of Sex)
    Private m_AnnualHarvestPopFilterMinAge As Integer
    Private m_AnnualHarvestPopFilterMaxAge As Integer

    Private Sub InitializeModel()

        Me.TimestepUnits = My.Resources.DGSIM_TIMESTEP
        Me.m_RandomGenerator = New RandomGenerator()
        Me.m_DistributionProvider = New DistributionProvider(Me.ResultScenario, Me.m_RandomGenerator)

    End Sub

    Private Sub InitializeOffspringPerFemaleBirthJDay()

        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(OFFSPRING_PER_FEMALE_OPTION_DATASHEET_NAME)
        Dim dr As DataRow = ds.GetDataRow()

        If (dr IsNot Nothing AndAlso dr(OFFSPRING_PER_FEMALE_VALUE_BIRTH_JDAY_COLUMN_NAME) IsNot DBNull.Value) Then
            Me.m_OffspringPerFemaleBirthJDay = CInt(dr(OFFSPRING_PER_FEMALE_VALUE_BIRTH_JDAY_COLUMN_NAME))
        Else
            Me.m_OffspringPerFemaleBirthJDay = Me.m_RunControl.StartJulianDay
        End If

    End Sub

    Private Sub InitializeAnnualHarvestVariables()

        Dim dr As DataRow = Me.ResultScenario.GetDataSheet(ANNUAL_HARVEST_OPTION_DATASHEET_NAME).GetDataRow()

        Me.m_AnnualHarvestSpecification = AnnualHarvestSpecification.AbsoluteNumber
        Me.m_AnnualHarvestPopFilterSex = Nothing
        Me.m_AnnualHarvestPopFilterMinAge = Integer.MinValue
        Me.m_AnnualHarvestPopFilterMaxAge = Integer.MaxValue

        If (dr IsNot Nothing) Then

            Debug.Assert(dr(ANNUAL_HARVEST_SPECIFICATION_COLUMN_NAME) IsNot DBNull.Value)

            If (dr(ANNUAL_HARVEST_SPECIFICATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Me.m_AnnualHarvestSpecification = CType(dr(ANNUAL_HARVEST_SPECIFICATION_COLUMN_NAME), AnnualHarvestSpecification)
            End If

            If (dr(ANNUAL_HARVEST_POP_FILTER_SEX_COLUMN_NAME) IsNot DBNull.Value) Then
                Me.m_AnnualHarvestPopFilterSex = CType(dr(ANNUAL_HARVEST_POP_FILTER_SEX_COLUMN_NAME), Sex)
            End If

            If (dr(ANNUAL_HARVEST_POP_FILTER_MIN_AGE_COLUMN_NAME) IsNot DBNull.Value) Then
                Me.m_AnnualHarvestPopFilterMinAge = CInt(dr(ANNUAL_HARVEST_POP_FILTER_MIN_AGE_COLUMN_NAME))
            End If

            If (dr(ANNUAL_HARVEST_POP_FILTER_MAX_AGE_COLUMN_NAME) IsNot DBNull.Value) Then
                Me.m_AnnualHarvestPopFilterMaxAge = CInt(dr(ANNUAL_HARVEST_POP_FILTER_MAX_AGE_COLUMN_NAME))
            End If

            If (Me.m_AnnualHarvestPopFilterMinAge > Me.m_AnnualHarvestPopFilterMaxAge) Then
                ThrowArgumentException(My.Resources.DGSIM_ERROR_ANNUAL_HARVEST_POP_AGE)
            End If

        End If

    End Sub

    Private Sub InitializeOutputDataTables()

        Dim dspop As DataSheet = Me.ResultScenario.GetDataSheet(OUTPUT_POPULATION_SIZE_DATASHEET_NAME)
        Me.m_OutputPopSizeDataTable = dspop.GetData()

        Dim dsharv As DataSheet = Me.ResultScenario.GetDataSheet(OUTPUT_HARVEST_DATASHEET_NAME)
        Me.m_OutputHarvestDataTable = dsharv.GetData()

        Dim dsrecs As DataSheet = Me.ResultScenario.GetDataSheet(OUTPUT_RECRUITS_DATASHEET_NAME)
        Me.m_OutputRecruitsDataTable = dsrecs.GetData()

        Dim dsmort As DataSheet = Me.ResultScenario.GetDataSheet(OUTPUT_MORTALITY_DATASHEET_NAME)
        Me.m_OutputMortalityDataTable = dsmort.GetData()

        Dim dsdist As DataSheet = Me.ResultScenario.GetDataSheet(OUTPUT_POSTERIOR_DIST_VALUE_DATASHEET_NAME)
        Me.m_OutputPosteriorDistDataTable = dsdist.GetData()

        Debug.Assert(Me.m_OutputPopSizeDataTable.Rows.Count = 0)
        Debug.Assert(Me.m_OutputHarvestDataTable.Rows.Count = 0)
        Debug.Assert(Me.m_OutputRecruitsDataTable.Rows.Count = 0)
        Debug.Assert(Me.m_OutputMortalityDataTable.Rows.Count = 0)
        Debug.Assert(Me.m_OutputPosteriorDistDataTable.Rows.Count = 0)

    End Sub

End Class
