<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RunControlDataFeedView
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
        Me.TableLayoutPanelMain = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelStartJulianDay = New System.Windows.Forms.Label()
        Me.ButtonClearAll = New System.Windows.Forms.Button()
        Me.TextBoxEndTimestep = New System.Windows.Forms.TextBox()
        Me.LabelStartTimestep = New System.Windows.Forms.Label()
        Me.TextBoxStartTimestep = New System.Windows.Forms.TextBox()
        Me.LabelEndTimestep = New System.Windows.Forms.Label()
        Me.LabelTotalIterations = New System.Windows.Forms.Label()
        Me.TextBoxTotalIterations = New System.Windows.Forms.TextBox()
        Me.TextBoxStartJulianDay = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanelMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanelMain
        '
        Me.TableLayoutPanelMain.ColumnCount = 2
        Me.TableLayoutPanelMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.23834!))
        Me.TableLayoutPanelMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.76166!))
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelStartJulianDay, 0, 3)
        Me.TableLayoutPanelMain.Controls.Add(Me.ButtonClearAll, 1, 4)
        Me.TableLayoutPanelMain.Controls.Add(Me.TextBoxEndTimestep, 1, 1)
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelStartTimestep, 0, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.TextBoxStartTimestep, 1, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelEndTimestep, 0, 1)
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelTotalIterations, 0, 2)
        Me.TableLayoutPanelMain.Controls.Add(Me.TextBoxTotalIterations, 1, 2)
        Me.TableLayoutPanelMain.Controls.Add(Me.TextBoxStartJulianDay, 1, 3)
        Me.TableLayoutPanelMain.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanelMain.Name = "TableLayoutPanelMain"
        Me.TableLayoutPanelMain.RowCount = 5
        Me.TableLayoutPanelMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanelMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanelMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanelMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanelMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanelMain.Size = New System.Drawing.Size(373, 134)
        Me.TableLayoutPanelMain.TabIndex = 9
        '
        'LabelStartJulianDay
        '
        Me.LabelStartJulianDay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelStartJulianDay.AutoSize = True
        Me.LabelStartJulianDay.Location = New System.Drawing.Point(23, 83)
        Me.LabelStartJulianDay.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.LabelStartJulianDay.Name = "LabelStartJulianDay"
        Me.LabelStartJulianDay.Size = New System.Drawing.Size(79, 13)
        Me.LabelStartJulianDay.TabIndex = 8
        Me.LabelStartJulianDay.Text = "Start julian day:"
        Me.LabelStartJulianDay.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ButtonClearAll
        '
        Me.ButtonClearAll.Location = New System.Drawing.Point(108, 107)
        Me.ButtonClearAll.Name = "ButtonClearAll"
        Me.ButtonClearAll.Size = New System.Drawing.Size(117, 23)
        Me.ButtonClearAll.TabIndex = 7
        Me.ButtonClearAll.Text = "Clear All"
        Me.ButtonClearAll.UseVisualStyleBackColor = True
        '
        'TextBoxEndTimestep
        '
        Me.TextBoxEndTimestep.Location = New System.Drawing.Point(108, 29)
        Me.TextBoxEndTimestep.Name = "TextBoxEndTimestep"
        Me.TextBoxEndTimestep.Size = New System.Drawing.Size(117, 20)
        Me.TextBoxEndTimestep.TabIndex = 3
        Me.TextBoxEndTimestep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabelStartTimestep
        '
        Me.LabelStartTimestep.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelStartTimestep.AutoSize = True
        Me.LabelStartTimestep.Location = New System.Drawing.Point(47, 5)
        Me.LabelStartTimestep.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.LabelStartTimestep.Name = "LabelStartTimestep"
        Me.LabelStartTimestep.Size = New System.Drawing.Size(55, 13)
        Me.LabelStartTimestep.TabIndex = 0
        Me.LabelStartTimestep.Text = "Start year:"
        Me.LabelStartTimestep.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBoxStartTimestep
        '
        Me.TextBoxStartTimestep.Location = New System.Drawing.Point(108, 3)
        Me.TextBoxStartTimestep.Name = "TextBoxStartTimestep"
        Me.TextBoxStartTimestep.Size = New System.Drawing.Size(117, 20)
        Me.TextBoxStartTimestep.TabIndex = 1
        Me.TextBoxStartTimestep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabelEndTimestep
        '
        Me.LabelEndTimestep.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelEndTimestep.AutoSize = True
        Me.LabelEndTimestep.Location = New System.Drawing.Point(50, 31)
        Me.LabelEndTimestep.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.LabelEndTimestep.Name = "LabelEndTimestep"
        Me.LabelEndTimestep.Size = New System.Drawing.Size(52, 13)
        Me.LabelEndTimestep.TabIndex = 2
        Me.LabelEndTimestep.Text = "End year:"
        Me.LabelEndTimestep.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LabelTotalIterations
        '
        Me.LabelTotalIterations.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelTotalIterations.AutoSize = True
        Me.LabelTotalIterations.Location = New System.Drawing.Point(23, 57)
        Me.LabelTotalIterations.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.LabelTotalIterations.Name = "LabelTotalIterations"
        Me.LabelTotalIterations.Size = New System.Drawing.Size(79, 13)
        Me.LabelTotalIterations.TabIndex = 4
        Me.LabelTotalIterations.Text = "Total iterations:"
        Me.LabelTotalIterations.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBoxTotalIterations
        '
        Me.TextBoxTotalIterations.Location = New System.Drawing.Point(108, 55)
        Me.TextBoxTotalIterations.Name = "TextBoxTotalIterations"
        Me.TextBoxTotalIterations.Size = New System.Drawing.Size(117, 20)
        Me.TextBoxTotalIterations.TabIndex = 5
        Me.TextBoxTotalIterations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxStartJulianDay
        '
        Me.TextBoxStartJulianDay.Location = New System.Drawing.Point(108, 81)
        Me.TextBoxStartJulianDay.Name = "TextBoxStartJulianDay"
        Me.TextBoxStartJulianDay.Size = New System.Drawing.Size(117, 20)
        Me.TextBoxStartJulianDay.TabIndex = 5
        Me.TextBoxStartJulianDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RunControlDataFeedView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanelMain)
        Me.Name = "RunControlDataFeedView"
        Me.Size = New System.Drawing.Size(379, 142)
        Me.TableLayoutPanelMain.ResumeLayout(False)
        Me.TableLayoutPanelMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanelMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ButtonClearAll As System.Windows.Forms.Button
    Friend WithEvents TextBoxEndTimestep As System.Windows.Forms.TextBox
    Friend WithEvents LabelStartTimestep As System.Windows.Forms.Label
    Friend WithEvents TextBoxStartTimestep As System.Windows.Forms.TextBox
    Friend WithEvents LabelEndTimestep As System.Windows.Forms.Label
    Friend WithEvents LabelTotalIterations As System.Windows.Forms.Label
    Friend WithEvents TextBoxTotalIterations As System.Windows.Forms.TextBox
    Friend WithEvents LabelStartJulianDay As System.Windows.Forms.Label
    Friend WithEvents TextBoxStartJulianDay As System.Windows.Forms.TextBox

End Class
