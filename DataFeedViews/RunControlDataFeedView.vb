'*********************************************************************************************
' DG-Sim: A SyncroSim Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class RunControlDataFeedView

    Public Overrides Sub LoadDataFeed(dataFeed As Core.DataFeed)

        MyBase.LoadDataFeed(dataFeed)

        Me.SetTextBoxBinding(Me.TextBoxStartTimestep, "MinimumTimestep")
        Me.SetTextBoxBinding(Me.TextBoxEndTimestep, "MaximumTimestep")
        Me.SetTextBoxBinding(Me.TextBoxTotalIterations, "MaximumIteration")
        Me.SetTextBoxBinding(Me.TextBoxStartJulianDay, "StartJulianDay")

        Me.RefreshBoundControls()
        Me.AddStandardCommands()

    End Sub

    Private Sub ButtonClearAll_Click(sender As System.Object, e As System.EventArgs) Handles ButtonClearAll.Click

        Me.ResetBoundControls()
        Me.ClearDataSheets()

    End Sub

End Class
