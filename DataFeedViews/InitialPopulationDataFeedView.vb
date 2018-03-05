'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports SyncroSim.Core.Forms
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class InitialPopulationDataFeedView

    Protected Overrides Sub InitializeView()

        MyBase.InitializeView()

        Dim SizeView As SingleRowDataFeedView = Me.Session.CreateSingleRowDataFeedView(Me.Scenario, Me.ControllingScenario)
        Me.PanelInitialPopulationSize.Controls.Add(SizeView)

        Dim DistView As DataFeedView = Me.Session.CreateMultiRowDataFeedView(Me.Scenario, Me.ControllingScenario)
        Me.PanelInitialPopulationDistribution.Controls.Add(DistView)

        'The single row view has four rows and no space to have more or less.  To make this look
        'good we only want to show the cell borders.  If we also show the border it will conflict 
        'with the top and bottom rows.

        SizeView.ShowBorder = False
        SizeView.PaintGridCellBorders = True

    End Sub

    Public Overrides Sub LoadDataFeed(ByVal dataFeed As DataFeed)

        MyBase.LoadDataFeed(dataFeed)

        Dim SizeView As DataFeedView = CType(Me.PanelInitialPopulationSize.Controls(0), DataFeedView)
        SizeView.LoadDataFeed(dataFeed, INITIAL_POPULATION_SIZE_DATASHEET_NAME)

        Dim DistView As DataFeedView = CType(Me.PanelInitialPopulationDistribution.Controls(0), DataFeedView)
        DistView.LoadDataFeed(dataFeed, INITIAL_POPULATION_DISTRIBUTION_DATASHEET_NAME)

    End Sub

    Public Overrides Sub EnableView(enable As Boolean)

        If (Me.PanelInitialPopulationSize.Controls.Count > 0) Then
            Dim v As DataFeedView = CType(Me.PanelInitialPopulationSize.Controls(0), DataFeedView)
            v.EnableView(enable)
        End If

        If (Me.PanelInitialPopulationDistribution.Controls.Count > 0) Then
            Dim v As DataFeedView = CType(Me.PanelInitialPopulationDistribution.Controls(0), DataFeedView)
            v.EnableView(enable)
        End If

        Me.LabelInitialPopSize.Enabled = enable
        Me.LabelInitialPopDist.Enabled = enable

    End Sub

End Class
