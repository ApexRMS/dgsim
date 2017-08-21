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
Class OffspringPerFemaleDataFeedView

    Protected Overrides Sub InitializeView()

        MyBase.InitializeView()

        Dim v As DataFeedView = Me.Session.CreateMultiRowDataFeedView(Me.Scenario, Me.ControllingScenario)
        Me.PanelValues.Controls.Add(v)

    End Sub

    Public Overrides Sub LoadDataFeed(ByVal dataFeed As DataFeed)

        MyBase.LoadDataFeed(dataFeed)

        Me.SetTextBoxBinding(
            Me.TextBoxBirthJDay,
            OFFSPRING_PER_FEMALE_OPTION_DATASHEET_NAME,
            OFFSPRING_PER_FEMALE_VALUE_BIRTH_JDAY_COLUMN_NAME)

        Dim v As DataFeedView = CType(Me.PanelValues.Controls(0), DataFeedView)
        v.LoadDataFeed(dataFeed, OFFSPRING_PER_FEMALE_VALUE_DATASHEET_NAME)

        Me.RefreshBoundControls()

    End Sub

    Public Overrides Sub EnableView(enable As Boolean)

        Me.TextBoxBirthJDay.Enabled = enable

        If (Me.PanelValues.Controls.Count > 0) Then
            Dim v As DataFeedView = CType(Me.PanelValues.Controls(0), DataFeedView)
            v.EnableView(enable)
        End If

    End Sub

End Class
