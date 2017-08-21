'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports SyncroSim.Core.Forms
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class AnnualHarvestDataFeedView

    Protected Overrides Sub InitializeView()

        MyBase.InitializeView()

        Dim v As DataFeedView = Me.Session.CreateMultiRowDataFeedView(Me.Scenario, Me.ControllingScenario)
        Me.PanelValues.Controls.Add(v)

    End Sub

    Public Overrides Sub LoadDataFeed(ByVal dataFeed As DataFeed)

        MyBase.LoadDataFeed(dataFeed)

        Me.SetComboBoxBinding(Me.ComboBoxSpecifyHarvestAs, ANNUAL_HARVEST_OPTION_DATASHEET_NAME, ANNUAL_HARVEST_SPECIFICATION_COLUMN_NAME)
        Me.SetComboBoxBinding(Me.ComboBoxPopGender, ANNUAL_HARVEST_OPTION_DATASHEET_NAME, ANNUAL_HARVEST_POP_FILTER_GENDER_COLUMN_NAME)
        Me.SetTextBoxBinding(Me.TextBoxPopFilterMinAge, ANNUAL_HARVEST_OPTION_DATASHEET_NAME, ANNUAL_HARVEST_POP_FILTER_MIN_AGE_COLUMN_NAME)
        Me.SetTextBoxBinding(Me.TextBoxPopFilterMaxAge, ANNUAL_HARVEST_OPTION_DATASHEET_NAME, ANNUAL_HARVEST_POP_FILTER_MAX_AGE_COLUMN_NAME)

        Dim v As DataFeedView = CType(Me.PanelValues.Controls(0), DataFeedView)
        v.LoadDataFeed(dataFeed, ANNUAL_HARVEST_VALUE_DATASHEET_NAME)

        Me.RefreshBoundControls()

    End Sub

    ''' <summary>
    ''' Overrides EnableView
    ''' </summary>
    ''' <param name="enable"></param>
    ''' <remarks>
    ''' We need to do our own enabling or the data feed views will be 
    ''' completely disabled instead of just grayed.
    ''' </remarks>
    Public Overrides Sub EnableView(enable As Boolean)

        Me.ComboBoxSpecifyHarvestAs.Enabled = enable
        Me.ComboBoxPopGender.Enabled = enable
        Me.TextBoxPopFilterMinAge.Enabled = enable
        Me.TextBoxPopFilterMaxAge.Enabled = enable
        Me.GroupBoxPopFilters.Enabled = enable
        Me.LabelSpecifyHarvestAs.Enabled = enable
        Me.LabelPopFilterSex.Enabled = enable
        Me.LabelPopFilterMinAge.Enabled = enable
        Me.LabelPopFilterMaxAge.Enabled = enable
        Me.ButtonClearAll.Enabled = enable

        If (Me.PanelValues.Controls.Count > 0) Then

            Dim v As DataFeedView = CType(Me.PanelValues.Controls(0), DataFeedView)
            v.EnableView(enable)

        End If

        Me.EnableControls()

    End Sub

    Protected Overrides Sub OnBoundComboBoxSelectedValueChanged(comboBox As System.Windows.Forms.ComboBox, columnName As String)

        MyBase.OnBoundComboBoxSelectedValueChanged(comboBox, columnName)
        Me.EnableControls()

    End Sub

    Private Sub EnableControls()

        If (Me.ShouldEnableView()) Then

            Dim FiltersEnabled As Boolean = Me.EnablePopulationFilters()

            Me.GroupBoxPopFilters.Enabled = FiltersEnabled
            Me.ComboBoxPopGender.Enabled = FiltersEnabled
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

    Private Sub ButtonClearAll_Click(sender As System.Object, e As System.EventArgs) Handles ButtonClearAll.Click

        Me.DataFeed.GetDataSheet(ANNUAL_HARVEST_OPTION_DATASHEET_NAME).ClearData()
        Me.EnableControls()

    End Sub

End Class
