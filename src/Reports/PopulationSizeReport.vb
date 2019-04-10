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
            "DGSim_OutputPopulationSize.ScenarioID, " &
            "SSim_Scenario.Name AS ScenarioName, " &
            "DGSim_OutputPopulationSize.Iteration, " &
            "DGSim_OutputPopulationSize.Timestep, " &
            "DGSim_Stratum.Name AS StratumName, " &
            "CASE WHEN DGSim_OutputPopulationSize.Sex=0 THEN 'Male' ELSE 'Female' END AS Sex, " &
            "DGSim_AgeClass.Name AS AgeClassName, " &
            "DGSim_OutputPopulationSize.Population " &
            "FROM DGSim_OutputPopulationSize " &
            "INNER JOIN SSim_Scenario ON SSim_Scenario.ScenarioID = DGSim_OutputPopulationSize.ScenarioID " &
            "INNER JOIN DGSim_Stratum ON DGSim_OutputPopulationSize.StratumID = DGSim_Stratum.StratumID " &
            "INNER JOIN DGSim_AgeClass ON DGSim_OutputPopulationSize.AgeClassID = DGSim_AgeClass.AgeClassID " &
            "WHERE DGSim_OutputPopulationSize.ScenarioID IN ({0}) " &
            "ORDER BY " &
            "DGSim_OutputPopulationSize.ScenarioID, " &
            "Iteration, " &
            "Timestep, " &
            "StratumName, " &
            "Sex, " &
            "AgeClassName",
            Me.CreateActiveResultScenarioFilter())

        Return Query

    End Function

End Class
