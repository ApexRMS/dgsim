'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'**********************************************************************************************

Imports SyncroSim.Core

Partial Class DGSimTransformer

    Private Sub NormalizeModelData()

        Me.NormalizeRunControl()
        Me.NormalizeAgeClassRanges()
        Me.NormalizeInitialPopulationSize()
        Me.NormalizeInitialPopulationDistribution()

    End Sub

    Private Sub NormalizeRunControl()

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
            Me.RecordStatus(StatusType.Warning, My.Resources.DGSIM_WARNING_USING_DEFAULT_ITERATIONS)
        End If

        If (dr("MinimumTimestep") Is DBNull.Value) Then
            dr("MinimumTimestep") = 1
        End If

        If (dr("MaximumTimestep") Is DBNull.Value) Then
            dr("MaximumTimestep") = 10
            Me.RecordStatus(StatusType.Warning, My.Resources.DGSIM_WARNING_USING_DEFAULT_TIMESTEPS)
        End If

        If (dr(RUN_CONTROL_DATASHEET_START_JULIAN_DAY_COLUMN_NAME) Is DBNull.Value) Then
            dr(RUN_CONTROL_DATASHEET_START_JULIAN_DAY_COLUMN_NAME) = 1
            Me.RecordStatus(StatusType.Warning, My.Resources.DGSIM_WARNING_USING_DEFAULT_JDAY)
        End If

    End Sub

    Private Sub NormalizeAgeClassRanges()

        Dim dtacr As DataTable = Me.ResultScenario.GetDataSheet(AGE_CLASS_RANGE_DATASHEET_NAME).GetData()

        If (dtacr.Rows.Count = 0) Then

            Dim ds As DataSheet = Me.Project.GetDataSheet(AGE_CLASS_DATASHEET_NAME)
            Dim dtac As DataTable = ds.GetData()
            Dim val As Integer = 1

            Debug.Assert(dtac.Rows.Count > 0)

            For Each dr As DataRow In dtac.Rows

                Dim drnew As DataRow = dtacr.NewRow

                drnew(DATASHEET_AGE_CLASS_ID_COLUMN_NAME) = dr(ds.ValidationTable.ValueMember)
                drnew(DATASHEET_MAX_AGE_COLUMN_NAME) = val

                dtacr.Rows.Add(drnew)
                val += 1

            Next

            Me.RecordStatus(StatusType.Warning, My.Resources.DGSIM_WARNING_USING_DEFAULT_AGE_CLASS_RANGES)

        End If

    End Sub

    Private Sub NormalizeInitialPopulationSize()

        Const DEFAULT_MEAN As Integer = 1000

        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(INITIAL_POPULATION_SIZE_DATASHEET_NAME)
        Dim dr As DataRow = ds.GetDataRow()

        If (dr Is Nothing) Then

            dr = ds.GetData().NewRow()
            dr(DATASHEET_MEAN_COLUMN_NAME) = DEFAULT_MEAN
            ds.GetData.Rows.Add(dr)

            Me.RecordStatus(StatusType.Warning, My.Resources.DGSIM_WARNING_USING_DEFAULT_INITIAL_POP_SIZE)

        End If

    End Sub

    Private Sub NormalizeInitialPopulationDistribution()

        Dim dt As DataTable = Me.ResultScenario.GetDataSheet(INITIAL_POPULATION_DISTRIBUTION_DATASHEET_NAME).GetData()

        If (dt.Rows.Count = 0) Then

            Dim dr As DataRow = dt.NewRow
            dr(DATASHEET_MIN_AGE_COLUMN_NAME) = 1
            dr(DATASHEET_MAX_AGE_COLUMN_NAME) = 1
            dr(INITIAL_POPULATION_DISTRIBUTION_DATASHEET_RELATIVE_AMOUNT_COLUMN_NAME) = 1.0
            dt.Rows.Add(dr)

            Me.RecordStatus(StatusType.Warning, My.Resources.DGSIM_WARNING_USING_DEFAULT_INITIAL_POP_DISTRIBUTION)

        End If

    End Sub

End Class
