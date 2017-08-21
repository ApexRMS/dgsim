'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core

Partial Class DGSimTransformer

    ''' <summary>
    ''' Normalizes the model data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NormalizeData()

        Me.NormalizeRunControl()
        Me.NormalizeAgeClassRanges()
        Me.NormalizeInitialPopulationSize()
        Me.NormalizeInitialPopulationDistribution()

    End Sub

    ''' <summary>
    ''' Normalizes the run control values
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NormalizeRunControl()

        Const STATUS_USING_DEFAULT_MAX_TIMESTEP_WARNING As String = "The number of timesteps was not specified.  Using default."
        Const STATUS_USING_DEFAULT_MAX_ITERATIONS_WARNING As String = "The number of iterations was not specified.  Using default."
        Const STATUS_USING_DEFAULT_START_JDAY_WARNING As String = "The number of iterations was not specified.  Using default."

        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(RUN_CONTROL_DATASHEET_NAME)
        Dim dr As DataRow = ds.GetDataRow()

        If (dr Is Nothing) Then
            dr = ds.GetData().NewRow()
            ds.GetData().Rows.Add(dr)
        End If

        If (dr("MinimumIteration") Is DBNull.Value) Then
            dr("MinimumIteration") = 1
        End If

        If (dr("MaximumIteration") Is DBNull.Value) Then
            dr("MaximumIteration") = 1
            Me.RecordStatus(StatusType.Warning, STATUS_USING_DEFAULT_MAX_ITERATIONS_WARNING)
        End If

        If (dr("MinimumTimestep") Is DBNull.Value) Then
            dr("MinimumTimestep") = 1
        End If

        If (dr("MaximumTimestep") Is DBNull.Value) Then
            dr("MaximumTimestep") = 10
            Me.RecordStatus(StatusType.Warning, STATUS_USING_DEFAULT_MAX_TIMESTEP_WARNING)
        End If

        If (dr(RUN_CONTROL_DATASHEET_START_JULIAN_DAY_COLUMN_NAME) Is DBNull.Value) Then
            dr(RUN_CONTROL_DATASHEET_START_JULIAN_DAY_COLUMN_NAME) = 1
            Me.RecordStatus(StatusType.Warning, STATUS_USING_DEFAULT_START_JDAY_WARNING)
        End If

    End Sub

    ''' <summary>
    ''' Normalizes the age class ranges
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NormalizeAgeClassRanges()

        Dim dtacr As DataTable = Me.ResultScenario.GetDataSheet(AGE_CLASS_RANGE_DATASHEET_NAME).GetData()

        If (dtacr.Rows.Count = 0) Then

            Dim ds As DataSheet = Me.Project.GetDataSheet(AGE_CLASS_DATASHEET_NAME)
            Dim dtac As DataTable = ds.GetData()
            Debug.Assert(dtac.Rows.Count > 0)

            Dim val As Integer = 1

            For Each dr As DataRow In dtac.Rows

                Dim drnew As DataRow = dtacr.NewRow

                drnew(DATASHEET_AGE_CLASS_ID_COLUMN_NAME) = dr(ds.ValidationTable.ValueMember)
                drnew(DATASHEET_MAX_AGE_COLUMN_NAME) = val

                dtacr.Rows.Add(drnew)
                val += 1

            Next

            Me.RecordStatus(StatusType.Warning, DEFAULT_AGE_CLASS_RANGES_WARNING)

        End If

    End Sub

    ''' <summary>
    ''' Normalizes the initial population size
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NormalizeInitialPopulationSize()

        Const DEFAULT_MEAN As Integer = 1000
        Const DEFAULT_MIN As Integer = 900
        Const DEFAULT_MAX As Integer = 1100
        Const DEFAULT_SD As Double = 20

        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(INITIAL_POPULATION_SIZE_DATASHEET_NAME)
        Dim dr As DataRow = ds.GetDataRow()

        If (dr Is Nothing) Then

            dr = ds.GetData().NewRow()

            dr(DATASHEET_MEAN_COLUMN_NAME) = DEFAULT_MEAN
            dr(DATASHEET_MIN_COLUMN_NAME) = DEFAULT_MIN
            dr(DATASHEET_MAX_COLUMN_NAME) = DEFAULT_MAX
            dr(DATASHEET_SD_COLUMN_NAME) = DEFAULT_SD

            ds.GetData.Rows.Add(dr)
            Me.RecordStatus(StatusType.Warning, DEFAULT_INITIAL_POP_SIZE_WARNING)

        End If

    End Sub

    ''' <summary>
    ''' Normalizes the Initial Population Distribution
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NormalizeInitialPopulationDistribution()

        Dim dt As DataTable = Me.ResultScenario.GetDataSheet(INITIAL_POPULATION_DISTRIBUTION_DATASHEET_NAME).GetData()

        If (dt.Rows.Count = 0) Then

            Dim dr As DataRow = dt.NewRow

            dr(DATASHEET_MIN_AGE_COLUMN_NAME) = 1
            dr(DATASHEET_MAX_AGE_COLUMN_NAME) = 1
            dr(INITIAL_POPULATION_DISTRIBUTION_DATASHEET_RELATIVE_AMOUNT_COLUMN_NAME) = 1.0

            dt.Rows.Add(dr)
            Me.RecordStatus(StatusType.Warning, DEFAULT_INITIAL_POP_DIST_WARNING)

        End If

    End Sub

End Class
