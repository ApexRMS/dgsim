﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AnnualHarvestOptionDataFeedView
    Inherits SyncroSim.Core.Forms.DataFeedView

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AnnualHarvestOptionDataFeedView))
        Me.LabelSpecifyHarvestAs = New System.Windows.Forms.Label()
        Me.ComboBoxSpecifyHarvestAs = New System.Windows.Forms.ComboBox()
        Me.ComboBoxPopSex = New System.Windows.Forms.ComboBox()
        Me.TextBoxPopFilterMaxAge = New System.Windows.Forms.TextBox()
        Me.TextBoxPopFilterMinAge = New System.Windows.Forms.TextBox()
        Me.LabelPopFilterMaxAge = New System.Windows.Forms.Label()
        Me.LabelPopFilterMinAge = New System.Windows.Forms.Label()
        Me.LabelPopFilterSex = New System.Windows.Forms.Label()
        Me.ButtonClearAll = New System.Windows.Forms.Button()
        Me.GroupBoxPopFilters = New System.Windows.Forms.GroupBox()
        Me.GroupBoxPopFilters.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelSpecifyHarvestAs
        '
        resources.ApplyResources(Me.LabelSpecifyHarvestAs, "LabelSpecifyHarvestAs")
        Me.LabelSpecifyHarvestAs.Name = "LabelSpecifyHarvestAs"
        '
        'ComboBoxSpecifyHarvestAs
        '
        Me.ComboBoxSpecifyHarvestAs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxSpecifyHarvestAs.FormattingEnabled = True
        resources.ApplyResources(Me.ComboBoxSpecifyHarvestAs, "ComboBoxSpecifyHarvestAs")
        Me.ComboBoxSpecifyHarvestAs.Name = "ComboBoxSpecifyHarvestAs"
        '
        'ComboBoxPopSex
        '
        Me.ComboBoxPopSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxPopSex.FormattingEnabled = True
        resources.ApplyResources(Me.ComboBoxPopSex, "ComboBoxPopSex")
        Me.ComboBoxPopSex.Name = "ComboBoxPopSex"
        '
        'TextBoxPopFilterMaxAge
        '
        resources.ApplyResources(Me.TextBoxPopFilterMaxAge, "TextBoxPopFilterMaxAge")
        Me.TextBoxPopFilterMaxAge.Name = "TextBoxPopFilterMaxAge"
        '
        'TextBoxPopFilterMinAge
        '
        resources.ApplyResources(Me.TextBoxPopFilterMinAge, "TextBoxPopFilterMinAge")
        Me.TextBoxPopFilterMinAge.Name = "TextBoxPopFilterMinAge"
        '
        'LabelPopFilterMaxAge
        '
        resources.ApplyResources(Me.LabelPopFilterMaxAge, "LabelPopFilterMaxAge")
        Me.LabelPopFilterMaxAge.Name = "LabelPopFilterMaxAge"
        '
        'LabelPopFilterMinAge
        '
        resources.ApplyResources(Me.LabelPopFilterMinAge, "LabelPopFilterMinAge")
        Me.LabelPopFilterMinAge.Name = "LabelPopFilterMinAge"
        '
        'LabelPopFilterSex
        '
        resources.ApplyResources(Me.LabelPopFilterSex, "LabelPopFilterSex")
        Me.LabelPopFilterSex.Name = "LabelPopFilterSex"
        '
        'ButtonClearAll
        '
        resources.ApplyResources(Me.ButtonClearAll, "ButtonClearAll")
        Me.ButtonClearAll.Name = "ButtonClearAll"
        Me.ButtonClearAll.UseVisualStyleBackColor = True
        '
        'GroupBoxPopFilters
        '
        Me.GroupBoxPopFilters.Controls.Add(Me.TextBoxPopFilterMaxAge)
        Me.GroupBoxPopFilters.Controls.Add(Me.TextBoxPopFilterMinAge)
        Me.GroupBoxPopFilters.Controls.Add(Me.LabelPopFilterMaxAge)
        Me.GroupBoxPopFilters.Controls.Add(Me.LabelPopFilterMinAge)
        Me.GroupBoxPopFilters.Controls.Add(Me.LabelPopFilterSex)
        Me.GroupBoxPopFilters.Controls.Add(Me.ComboBoxPopSex)
        resources.ApplyResources(Me.GroupBoxPopFilters, "GroupBoxPopFilters")
        Me.GroupBoxPopFilters.Name = "GroupBoxPopFilters"
        Me.GroupBoxPopFilters.TabStop = False
        '
        'AnnualHarvestOptionDataFeedView
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ButtonClearAll)
        Me.Controls.Add(Me.GroupBoxPopFilters)
        Me.Controls.Add(Me.ComboBoxSpecifyHarvestAs)
        Me.Controls.Add(Me.LabelSpecifyHarvestAs)
        Me.Name = "AnnualHarvestOptionDataFeedView"
        Me.GroupBoxPopFilters.ResumeLayout(False)
        Me.GroupBoxPopFilters.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelSpecifyHarvestAs As System.Windows.Forms.Label
    Friend WithEvents ComboBoxSpecifyHarvestAs As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxPopSex As System.Windows.Forms.ComboBox
    Friend WithEvents TextBoxPopFilterMaxAge As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxPopFilterMinAge As System.Windows.Forms.TextBox
    Friend WithEvents LabelPopFilterMaxAge As System.Windows.Forms.Label
    Friend WithEvents LabelPopFilterMinAge As System.Windows.Forms.Label
    Friend WithEvents LabelPopFilterSex As System.Windows.Forms.Label
    Friend WithEvents ButtonClearAll As System.Windows.Forms.Button
    Friend WithEvents GroupBoxPopFilters As System.Windows.Forms.GroupBox

End Class
