'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'**********************************************************************************************

Imports System.Reflection
Imports System.Globalization
Imports SyncroSim.Core.Forms

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class HarvestReport
    Inherits ExportTransformer

    Protected Overrides Sub Export(location As String, exportType As ExportType)

        Dim query As String = Me.CreateReportQuery()
        Dim columns As ExportColumnCollection = CreateColumnCollection()

        If (exportType = ExportType.ExcelFile) Then
            Me.ExcelExport(location, columns, query, "Harvest")
        Else
            Me.CSVExport(location, columns, query)
            InformationMessageBox("Data saved to '{0}'.", location)
        End If

    End Sub

    Private Shared Function CreateColumnCollection() As ExportColumnCollection

        Dim c As New ExportColumnCollection()

        c.Add(New ExportColumn("ScenarioID", "Scenario ID"))
        c.Add(New ExportColumn("ScenarioName", "Scenario"))
        c.Add(New ExportColumn("Iteration", "Iteration"))
        c.Add(New ExportColumn("Timestep", "Year"))
        c.Add(New ExportColumn("StratumName", "Stratum"))
        c.Add(New ExportColumn("Sex", "Sex"))
        c.Add(New ExportColumn("AgeClassName", "Age Class"))
        c.Add(New ExportColumn("Harvest", "Harvest"))

        c("Harvest").DecimalPlaces = 0
        c("Harvest").Alignment = Core.ColumnAlignment.Right

        Return c

    End Function

    Private Function CreateReportQuery() As String

        Dim Query As String = String.Format(CultureInfo.InvariantCulture,
            "SELECT " &
            "dgsim__OutputHarvest.ScenarioID, " &
            "system__Scenario.Name AS ScenarioName, " &
            "dgsim__OutputHarvest.Iteration, " &
            "dgsim__OutputHarvest.Timestep, " &
            "dgsim__Stratum.Name AS StratumName, " &
            "CASE WHEN dgsim__OutputHarvest.Sex=0 THEN 'Male' ELSE 'Female' END AS Sex, " &
            "dgsim__AgeClass.Name AS AgeClassName, " &
            "dgsim__OutputHarvest.Harvest " &
            "FROM dgsim__OutputHarvest " &
            "INNER JOIN system__Scenario ON system__Scenario.ScenarioID = dgsim__OutputHarvest.ScenarioID " &
            "INNER JOIN dgsim__Stratum ON dgsim__OutputHarvest.StratumID = dgsim__Stratum.StratumID " &
            "INNER JOIN dgsim__AgeClass ON dgsim__OutputHarvest.AgeClassID = dgsim__AgeClass.AgeClassID " &
            "WHERE dgsim__OutputHarvest.ScenarioID IN ({0}) " &
            "ORDER BY " &
            "dgsim__OutputHarvest.ScenarioID, " &
            "Iteration, " &
            "Timestep, " &
            "StratumName, " &
            "Sex, " &
            "AgeClassName",
            Me.CreateActiveResultScenarioFilter())

        Return Query

    End Function

End Class
