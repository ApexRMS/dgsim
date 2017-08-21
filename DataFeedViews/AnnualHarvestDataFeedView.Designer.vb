<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AnnualHarvestDataFeedView
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
        Me.PanelValues = New System.Windows.Forms.Panel()
        Me.LabelSpecifyHarvestAs = New System.Windows.Forms.Label()
        Me.ComboBoxSpecifyHarvestAs = New System.Windows.Forms.ComboBox()
        Me.ComboBoxPopGender = New System.Windows.Forms.ComboBox()
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
        'PanelValues
        '
        Me.PanelValues.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelValues.Location = New System.Drawing.Point(3, 188)
        Me.PanelValues.Name = "PanelValues"
        Me.PanelValues.Size = New System.Drawing.Size(689, 335)
        Me.PanelValues.TabIndex = 4
        '
        'LabelSpecifyHarvestAs
        '
        Me.LabelSpecifyHarvestAs.AutoSize = True
        Me.LabelSpecifyHarvestAs.Location = New System.Drawing.Point(50, 13)
        Me.LabelSpecifyHarvestAs.Name = "LabelSpecifyHarvestAs"
        Me.LabelSpecifyHarvestAs.Size = New System.Drawing.Size(97, 13)
        Me.LabelSpecifyHarvestAs.TabIndex = 0
        Me.LabelSpecifyHarvestAs.Text = "Specify harvest as:"
        '
        'ComboBoxSpecifyHarvestAs
        '
        Me.ComboBoxSpecifyHarvestAs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxSpecifyHarvestAs.FormattingEnabled = True
        Me.ComboBoxSpecifyHarvestAs.Location = New System.Drawing.Point(161, 10)
        Me.ComboBoxSpecifyHarvestAs.Name = "ComboBoxSpecifyHarvestAs"
        Me.ComboBoxSpecifyHarvestAs.Size = New System.Drawing.Size(167, 21)
        Me.ComboBoxSpecifyHarvestAs.TabIndex = 1
        '
        'ComboBoxPopGender
        '
        Me.ComboBoxPopGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxPopGender.FormattingEnabled = True
        Me.ComboBoxPopGender.Location = New System.Drawing.Point(146, 22)
        Me.ComboBoxPopGender.Name = "ComboBoxPopGender"
        Me.ComboBoxPopGender.Size = New System.Drawing.Size(167, 21)
        Me.ComboBoxPopGender.TabIndex = 1
        '
        'TextBoxPopFilterMaxAge
        '
        Me.TextBoxPopFilterMaxAge.Location = New System.Drawing.Point(146, 73)
        Me.TextBoxPopFilterMaxAge.Name = "TextBoxPopFilterMaxAge"
        Me.TextBoxPopFilterMaxAge.Size = New System.Drawing.Size(167, 20)
        Me.TextBoxPopFilterMaxAge.TabIndex = 5
        Me.TextBoxPopFilterMaxAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxPopFilterMinAge
        '
        Me.TextBoxPopFilterMinAge.Location = New System.Drawing.Point(146, 48)
        Me.TextBoxPopFilterMinAge.Name = "TextBoxPopFilterMinAge"
        Me.TextBoxPopFilterMinAge.Size = New System.Drawing.Size(167, 20)
        Me.TextBoxPopFilterMinAge.TabIndex = 3
        Me.TextBoxPopFilterMinAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabelPopFilterMaxAge
        '
        Me.LabelPopFilterMaxAge.AutoSize = True
        Me.LabelPopFilterMaxAge.Location = New System.Drawing.Point(60, 76)
        Me.LabelPopFilterMaxAge.Name = "LabelPopFilterMaxAge"
        Me.LabelPopFilterMaxAge.Size = New System.Drawing.Size(75, 13)
        Me.LabelPopFilterMaxAge.TabIndex = 4
        Me.LabelPopFilterMaxAge.Text = "Maximum age:"
        '
        'LabelPopFilterMinAge
        '
        Me.LabelPopFilterMinAge.AutoSize = True
        Me.LabelPopFilterMinAge.Location = New System.Drawing.Point(63, 50)
        Me.LabelPopFilterMinAge.Name = "LabelPopFilterMinAge"
        Me.LabelPopFilterMinAge.Size = New System.Drawing.Size(72, 13)
        Me.LabelPopFilterMinAge.TabIndex = 2
        Me.LabelPopFilterMinAge.Text = "Minimum age:"
        '
        'LabelPopFilterSex
        '
        Me.LabelPopFilterSex.AutoSize = True
        Me.LabelPopFilterSex.Location = New System.Drawing.Point(107, 24)
        Me.LabelPopFilterSex.Name = "LabelPopFilterSex"
        Me.LabelPopFilterSex.Size = New System.Drawing.Size(28, 13)
        Me.LabelPopFilterSex.TabIndex = 0
        Me.LabelPopFilterSex.Text = "Sex:"
        '
        'ButtonClearAll
        '
        Me.ButtonClearAll.Location = New System.Drawing.Point(161, 159)
        Me.ButtonClearAll.Name = "ButtonClearAll"
        Me.ButtonClearAll.Size = New System.Drawing.Size(167, 23)
        Me.ButtonClearAll.TabIndex = 3
        Me.ButtonClearAll.Text = "Clear All"
        Me.ButtonClearAll.UseVisualStyleBackColor = True
        '
        'GroupBoxPopFilters
        '
        Me.GroupBoxPopFilters.Controls.Add(Me.TextBoxPopFilterMaxAge)
        Me.GroupBoxPopFilters.Controls.Add(Me.TextBoxPopFilterMinAge)
        Me.GroupBoxPopFilters.Controls.Add(Me.LabelPopFilterMaxAge)
        Me.GroupBoxPopFilters.Controls.Add(Me.LabelPopFilterMinAge)
        Me.GroupBoxPopFilters.Controls.Add(Me.LabelPopFilterSex)
        Me.GroupBoxPopFilters.Controls.Add(Me.ComboBoxPopGender)
        Me.GroupBoxPopFilters.Location = New System.Drawing.Point(15, 46)
        Me.GroupBoxPopFilters.Name = "GroupBoxPopFilters"
        Me.GroupBoxPopFilters.Size = New System.Drawing.Size(332, 107)
        Me.GroupBoxPopFilters.TabIndex = 2
        Me.GroupBoxPopFilters.TabStop = False
        Me.GroupBoxPopFilters.Text = "Filter population by:"
        '
        'AnnualHarvestDataFeedView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ButtonClearAll)
        Me.Controls.Add(Me.GroupBoxPopFilters)
        Me.Controls.Add(Me.ComboBoxSpecifyHarvestAs)
        Me.Controls.Add(Me.LabelSpecifyHarvestAs)
        Me.Controls.Add(Me.PanelValues)
        Me.Name = "AnnualHarvestDataFeedView"
        Me.Size = New System.Drawing.Size(695, 526)
        Me.GroupBoxPopFilters.ResumeLayout(False)
        Me.GroupBoxPopFilters.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PanelValues As System.Windows.Forms.Panel
    Friend WithEvents LabelSpecifyHarvestAs As System.Windows.Forms.Label
    Friend WithEvents ComboBoxSpecifyHarvestAs As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxPopGender As System.Windows.Forms.ComboBox
    Friend WithEvents TextBoxPopFilterMaxAge As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxPopFilterMinAge As System.Windows.Forms.TextBox
    Friend WithEvents LabelPopFilterMaxAge As System.Windows.Forms.Label
    Friend WithEvents LabelPopFilterMinAge As System.Windows.Forms.Label
    Friend WithEvents LabelPopFilterSex As System.Windows.Forms.Label
    Friend WithEvents ButtonClearAll As System.Windows.Forms.Button
    Friend WithEvents GroupBoxPopFilters As System.Windows.Forms.GroupBox

End Class
