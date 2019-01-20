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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RunControlDataFeedView))
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
        resources.ApplyResources(Me.TableLayoutPanelMain, "TableLayoutPanelMain")
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelStartJulianDay, 0, 3)
        Me.TableLayoutPanelMain.Controls.Add(Me.ButtonClearAll, 1, 4)
        Me.TableLayoutPanelMain.Controls.Add(Me.TextBoxEndTimestep, 1, 1)
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelStartTimestep, 0, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.TextBoxStartTimestep, 1, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelEndTimestep, 0, 1)
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelTotalIterations, 0, 2)
        Me.TableLayoutPanelMain.Controls.Add(Me.TextBoxTotalIterations, 1, 2)
        Me.TableLayoutPanelMain.Controls.Add(Me.TextBoxStartJulianDay, 1, 3)
        Me.TableLayoutPanelMain.Name = "TableLayoutPanelMain"
        '
        'LabelStartJulianDay
        '
        resources.ApplyResources(Me.LabelStartJulianDay, "LabelStartJulianDay")
        Me.LabelStartJulianDay.Name = "LabelStartJulianDay"
        '
        'ButtonClearAll
        '
        resources.ApplyResources(Me.ButtonClearAll, "ButtonClearAll")
        Me.ButtonClearAll.Name = "ButtonClearAll"
        Me.ButtonClearAll.UseVisualStyleBackColor = True
        '
        'TextBoxEndTimestep
        '
        resources.ApplyResources(Me.TextBoxEndTimestep, "TextBoxEndTimestep")
        Me.TextBoxEndTimestep.Name = "TextBoxEndTimestep"
        '
        'LabelStartTimestep
        '
        resources.ApplyResources(Me.LabelStartTimestep, "LabelStartTimestep")
        Me.LabelStartTimestep.Name = "LabelStartTimestep"
        '
        'TextBoxStartTimestep
        '
        resources.ApplyResources(Me.TextBoxStartTimestep, "TextBoxStartTimestep")
        Me.TextBoxStartTimestep.Name = "TextBoxStartTimestep"
        '
        'LabelEndTimestep
        '
        resources.ApplyResources(Me.LabelEndTimestep, "LabelEndTimestep")
        Me.LabelEndTimestep.Name = "LabelEndTimestep"
        '
        'LabelTotalIterations
        '
        resources.ApplyResources(Me.LabelTotalIterations, "LabelTotalIterations")
        Me.LabelTotalIterations.Name = "LabelTotalIterations"
        '
        'TextBoxTotalIterations
        '
        resources.ApplyResources(Me.TextBoxTotalIterations, "TextBoxTotalIterations")
        Me.TextBoxTotalIterations.Name = "TextBoxTotalIterations"
        '
        'TextBoxStartJulianDay
        '
        resources.ApplyResources(Me.TextBoxStartJulianDay, "TextBoxStartJulianDay")
        Me.TextBoxStartJulianDay.Name = "TextBoxStartJulianDay"
        '
        'RunControlDataFeedView
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanelMain)
        Me.Name = "RunControlDataFeedView"
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
