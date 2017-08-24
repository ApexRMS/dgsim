'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core

Partial Class DGSimTransformer

    Private m_OffspringPerFemaleBirthJDay As Integer
    Private m_AnnualHarvestSpecification As AnnualHarvestSpecification
    Private m_AnnualHarvestPopFilterGender As Nullable(Of Gender)
    Private m_AnnualHarvestPopFilterMinAge As Integer
    Private m_AnnualHarvestPopFilterMaxAge As Integer

    ''' <summary>
    ''' Initializes the Offspring per Female Birth Julian Day
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeOffspringPerFemaleBirthJDay()

        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(OFFSPRING_PER_FEMALE_OPTION_DATASHEET_NAME)
        Dim dr As DataRow = ds.GetDataRow()

        If (dr IsNot Nothing AndAlso dr(OFFSPRING_PER_FEMALE_VALUE_BIRTH_JDAY_COLUMN_NAME) IsNot DBNull.Value) Then
            Me.m_OffspringPerFemaleBirthJDay = CInt(dr(OFFSPRING_PER_FEMALE_VALUE_BIRTH_JDAY_COLUMN_NAME))
        Else
            Me.m_OffspringPerFemaleBirthJDay = Me.m_RunControl.StartJulianDay
        End If

    End Sub

    ''' <summary>
    ''' Initializes Annual Harvest Variables
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeAnnualHarvestVariables()

        Dim dr As DataRow = Me.ResultScenario.GetDataSheet(ANNUAL_HARVEST_OPTION_DATASHEET_NAME).GetDataRow()

        Me.m_AnnualHarvestSpecification = AnnualHarvestSpecification.AbsoluteNumber
        Me.m_AnnualHarvestPopFilterGender = Nothing
        Me.m_AnnualHarvestPopFilterMinAge = Integer.MinValue
        Me.m_AnnualHarvestPopFilterMaxAge = Integer.MaxValue

        If (dr IsNot Nothing) Then

            Debug.Assert(dr(ANNUAL_HARVEST_SPECIFICATION_COLUMN_NAME) IsNot DBNull.Value)

            If (dr(ANNUAL_HARVEST_SPECIFICATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Me.m_AnnualHarvestSpecification = CType(dr(ANNUAL_HARVEST_SPECIFICATION_COLUMN_NAME), AnnualHarvestSpecification)
            End If

            If (dr(ANNUAL_HARVEST_POP_FILTER_GENDER_COLUMN_NAME) IsNot DBNull.Value) Then
                Me.m_AnnualHarvestPopFilterGender = CType(dr(ANNUAL_HARVEST_POP_FILTER_GENDER_COLUMN_NAME), Gender)
            End If

            If (dr(ANNUAL_HARVEST_POP_FILTER_MIN_AGE_COLUMN_NAME) IsNot DBNull.Value) Then
                Me.m_AnnualHarvestPopFilterMinAge = CInt(dr(ANNUAL_HARVEST_POP_FILTER_MIN_AGE_COLUMN_NAME))
            End If

            If (dr(ANNUAL_HARVEST_POP_FILTER_MAX_AGE_COLUMN_NAME) IsNot DBNull.Value) Then
                Me.m_AnnualHarvestPopFilterMaxAge = CInt(dr(ANNUAL_HARVEST_POP_FILTER_MAX_AGE_COLUMN_NAME))
            End If

            If (Me.m_AnnualHarvestPopFilterMinAge > Me.m_AnnualHarvestPopFilterMaxAge) Then
                ThrowArgumentException("The annual harvest population filter minimum age is greater than the maximum age.")
            End If

        End If

    End Sub

    ''' <summary>
    ''' Validates the model
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateModel()

        If (Me.Project.GetDataSheet(STRATUM_DATASHEET_NAME).GetData().Rows.Count = 0) Then
            ThrowArgumentException("You must define at least one stratum.")
        End If

        If (Me.Project.GetDataSheet(AGE_CLASS_DATASHEET_NAME).GetData().Rows.Count = 0) Then
            ThrowArgumentException("You must define at least one age class.")
        End If

    End Sub

    ''' <summary>
    ''' Initializes the output data tables
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeOutputDataTables()

        Dim dspop As DataSheet = Me.ResultScenario.GetDataSheet(OUTPUT_POPULATION_SIZE_DATASHEET_NAME)
        Me.m_OutputPopSizeDataTable = dspop.GetData()

        Dim dsharv As DataSheet = Me.ResultScenario.GetDataSheet(OUTPUT_HARVEST_DATASHEET_NAME)
        Me.m_OutputHarvestDataTable = dsharv.GetData()

        Dim dsbirths As DataSheet = Me.ResultScenario.GetDataSheet(OUTPUT_BIRTHS_DATASHEET_NAME)
        Me.m_OutputBirthsDataTable = dsbirths.GetData()

        Dim dsmort As DataSheet = Me.ResultScenario.GetDataSheet(OUTPUT_MORTALITY_DATASHEET_NAME)
        Me.m_OutputMortalityDataTable = dsmort.GetData()

        Dim dsdist As DataSheet = Me.ResultScenario.GetDataSheet(OUTPUT_POSTERIOR_DIST_VALUE_DATASHEET_NAME)
        Me.m_OutputPosteriorDistDataTable = dsdist.GetData()

        Debug.Assert(Me.m_OutputPopSizeDataTable.Rows.Count = 0)
        Debug.Assert(Me.m_OutputHarvestDataTable.Rows.Count = 0)
        Debug.Assert(Me.m_OutputBirthsDataTable.Rows.Count = 0)
        Debug.Assert(Me.m_OutputMortalityDataTable.Rows.Count = 0)
        Debug.Assert(Me.m_OutputPosteriorDistDataTable.Rows.Count = 0)

    End Sub

End Class
