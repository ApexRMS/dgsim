'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'**********************************************************************************************

Imports SyncroSim.Core
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class AnnualHarvestOptionDataFeedView

    Public Overrides Sub LoadDataFeed(dataFeed As DataFeed)

        MyBase.LoadDataFeed(dataFeed)

        Me.SetComboBoxBinding(Me.ComboBoxSpecifyHarvestAs, ANNUAL_HARVEST_SPECIFICATION_COLUMN_NAME)
        Me.SetComboBoxBinding(Me.ComboBoxPopSex, ANNUAL_HARVEST_POP_FILTER_SEX_COLUMN_NAME)
        Me.SetTextBoxBinding(Me.TextBoxPopFilterMinAge, ANNUAL_HARVEST_POP_FILTER_MIN_AGE_COLUMN_NAME)
        Me.SetTextBoxBinding(Me.TextBoxPopFilterMaxAge, ANNUAL_HARVEST_POP_FILTER_MAX_AGE_COLUMN_NAME)

        Me.RefreshBoundControls()

    End Sub

    Public Overrides Sub EnableView(enable As Boolean)

        Me.ComboBoxSpecifyHarvestAs.Enabled = enable
        Me.ComboBoxPopSex.Enabled = enable
        Me.TextBoxPopFilterMinAge.Enabled = enable
        Me.TextBoxPopFilterMaxAge.Enabled = enable
        Me.GroupBoxPopFilters.Enabled = enable
        Me.LabelSpecifyHarvestAs.Enabled = enable
        Me.LabelPopFilterSex.Enabled = enable
        Me.LabelPopFilterMinAge.Enabled = enable
        Me.LabelPopFilterMaxAge.Enabled = enable
        Me.ButtonClearAll.Enabled = enable

        Me.EnableControls()

    End Sub

    Protected Overrides Sub OnBoundComboBoxSelectedValueChanged(comboBox As Windows.Forms.ComboBox, columnName As String)

        MyBase.OnBoundComboBoxSelectedValueChanged(comboBox, columnName)
        Me.EnableControls()

    End Sub

    Private Sub EnableControls()

        If (Me.ShouldEnableView()) Then

            Dim FiltersEnabled As Boolean = Me.EnablePopulationFilters()

            Me.GroupBoxPopFilters.Enabled = FiltersEnabled
            Me.ComboBoxPopSex.Enabled = FiltersEnabled
            Me.TextBoxPopFilterMinAge.Enabled = FiltersEnabled
            Me.TextBoxPopFilterMaxAge.Enabled = FiltersEnabled

        End If

    End Sub

    Private Function EnablePopulationFilters() As Boolean

        If (Me.ComboBoxSpecifyHarvestAs.SelectedIndex = -1) Then
            Return False
        End If

        Dim v As AnnualHarvestSpecification = CType(Me.ComboBoxSpecifyHarvestAs.SelectedValue, AnnualHarvestSpecification)
        Return (v = AnnualHarvestSpecification.PercentageOfPopulation)

    End Function

    Private Sub ButtonClearAll_Click(sender As Object, e As EventArgs) Handles ButtonClearAll.Click

        Me.DataFeed.GetDataSheet(ANNUAL_HARVEST_OPTION_DATASHEET_NAME).ClearData()
        Me.EnableControls()

    End Sub

End Class
