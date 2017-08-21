<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OffspringPerFemaleDataFeedView
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
        Me.TextBoxBirthJDay = New System.Windows.Forms.TextBox()
        Me.LabelBirthJDay = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'PanelValues
        '
        Me.PanelValues.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelValues.Location = New System.Drawing.Point(3, 34)
        Me.PanelValues.Name = "PanelValues"
        Me.PanelValues.Size = New System.Drawing.Size(572, 355)
        Me.PanelValues.TabIndex = 2
        '
        'TextBoxBirthJDay
        '
        Me.TextBoxBirthJDay.Location = New System.Drawing.Point(88, 5)
        Me.TextBoxBirthJDay.Name = "TextBoxBirthJDay"
        Me.TextBoxBirthJDay.Size = New System.Drawing.Size(48, 20)
        Me.TextBoxBirthJDay.TabIndex = 1
        '
        'LabelBirthJDay
        '
        Me.LabelBirthJDay.AutoSize = True
        Me.LabelBirthJDay.Location = New System.Drawing.Point(4, 8)
        Me.LabelBirthJDay.Name = "LabelBirthJDay"
        Me.LabelBirthJDay.Size = New System.Drawing.Size(78, 13)
        Me.LabelBirthJDay.TabIndex = 0
        Me.LabelBirthJDay.Text = "Birth julian day:"
        '
        'OffspringPerFemaleDataFeedView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LabelBirthJDay)
        Me.Controls.Add(Me.TextBoxBirthJDay)
        Me.Controls.Add(Me.PanelValues)
        Me.Name = "OffspringPerFemaleDataFeedView"
        Me.Size = New System.Drawing.Size(578, 392)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PanelValues As System.Windows.Forms.Panel
    Friend WithEvents TextBoxBirthJDay As System.Windows.Forms.TextBox
    Friend WithEvents LabelBirthJDay As System.Windows.Forms.Label

End Class
