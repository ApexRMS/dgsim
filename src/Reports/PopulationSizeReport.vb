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
Class PopulationSizeReport
    Inherits ExportTransformer

    Protected Overrides Sub Export(location As String, exportType As ExportType)

        Dim query As String = Me.CreateReportQuery()
        Dim columns As ExportColumnCollection = CreateColumnCollection()

        If (exportType = ExportType.ExcelFile) Then
            Me.ExcelExport(location, columns, query, "Population Size")
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
        c.Add(New ExportColumn("Population", "Population"))

        c("Population").DecimalPlaces = 0
        c("Population").Alignment = Core.ColumnAlignment.Right

        Return c

    End Function

    Private Function CreateReportQuery() As String

        Dim Query As String = String.Format(CultureInfo.InvariantCulture,
            "SELECT " &
            "dgsim__OutputPopulationSize.ScenarioID, " &
            "system__Scenario.Name AS ScenarioName, " &
            "dgsim__OutputPopulationSize.Iteration, " &
            "dgsim__OutputPopulationSize.Timestep, " &
            "dgsim__Stratum.Name AS StratumName, " &
            "CASE WHEN dgsim__OutputPopulationSize.Sex=0 THEN 'Male' ELSE 'Female' END AS Sex, " &
            "dgsim__AgeClass.Name AS AgeClassName, " &
            "dgsim__OutputPopulationSize.Population " &
            "FROM dgsim__OutputPopulationSize " &
            "INNER JOIN system__Scenario ON system__Scenario.ScenarioID = dgsim__OutputPopulationSize.ScenarioID " &
            "INNER JOIN dgsim__Stratum ON dgsim__OutputPopulationSize.StratumID = dgsim__Stratum.StratumID " &
            "INNER JOIN dgsim__AgeClass ON dgsim__OutputPopulationSize.AgeClassID = dgsim__AgeClass.AgeClassID " &
            "WHERE dgsim__OutputPopulationSize.ScenarioID IN ({0}) " &
            "ORDER BY " &
            "dgsim__OutputPopulationSize.ScenarioID, " &
            "Iteration, " &
            "Timestep, " &
            "StratumName, " &
            "Sex, " &
            "AgeClassName",
            Me.CreateActiveResultScenarioFilter())

        Return Query

    End Function

End Class
