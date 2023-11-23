<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InitialPopulationDataFeedView
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
        Me.PanelInitialPopulationSize = New System.Windows.Forms.Panel()
        Me.LabelInitialPopSize = New System.Windows.Forms.Label()
        Me.LabelInitialPopDist = New System.Windows.Forms.Label()
        Me.PanelInitialPopulationDistribution = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'PanelInitialPopulationSize
        '
        Me.PanelInitialPopulationSize.Location = New System.Drawing.Point(8, 38)
        Me.PanelInitialPopulationSize.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelInitialPopulationSize.Name = "PanelInitialPopulationSize"
        Me.PanelInitialPopulationSize.Size = New System.Drawing.Size(288, 137)
        Me.PanelInitialPopulationSize.TabIndex = 1
        '
        'LabelInitialPopSize
        '
        Me.LabelInitialPopSize.AutoSize = True
        Me.LabelInitialPopSize.Location = New System.Drawing.Point(7, 10)
        Me.LabelInitialPopSize.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelInitialPopSize.Name = "LabelInitialPopSize"
        Me.LabelInitialPopSize.Size = New System.Drawing.Size(143, 17)
        Me.LabelInitialPopSize.TabIndex = 0
        Me.LabelInitialPopSize.Text = "Initial population size:"
        '
        'LabelInitialPopDist
        '
        Me.LabelInitialPopDist.AutoSize = True
        Me.LabelInitialPopDist.Location = New System.Drawing.Point(7, 199)
        Me.LabelInitialPopDist.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelInitialPopDist.Name = "LabelInitialPopDist"
        Me.LabelInitialPopDist.Size = New System.Drawing.Size(187, 17)
        Me.LabelInitialPopDist.TabIndex = 2
        Me.LabelInitialPopDist.Text = "Initial population distribution:"
        '
        'PanelInitialPopulationDistribution
        '
        Me.PanelInitialPopulationDistribution.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelInitialPopulationDistribution.Location = New System.Drawing.Point(8, 224)
        Me.PanelInitialPopulationDistribution.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelInitialPopulationDistribution.Name = "PanelInitialPopulationDistribution"
        Me.PanelInitialPopulationDistribution.Size = New System.Drawing.Size(936, 443)
        Me.PanelInitialPopulationDistribution.TabIndex = 3
        '
        'InitialPopulationDataFeedView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.LabelInitialPopDist)
        Me.Controls.Add(Me.LabelInitialPopSize)
        Me.Controls.Add(Me.PanelInitialPopulationDistribution)
        Me.Controls.Add(Me.PanelInitialPopulationSize)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "InitialPopulationDataFeedView"
        Me.Size = New System.Drawing.Size(951, 673)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PanelInitialPopulationSize As System.Windows.Forms.Panel
    Friend WithEvents LabelInitialPopSize As System.Windows.Forms.Label
    Friend WithEvents LabelInitialPopDist As System.Windows.Forms.Label
    Friend WithEvents PanelInitialPopulationDistribution As System.Windows.Forms.Panel

End Class
