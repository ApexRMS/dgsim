'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
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
            Me.ExportToExcel(location, columns, query, "Population Size")
        Else
            Me.ExportToCSVFile(location, columns, query)
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
            "dgsim_OutputPopulationSize.ScenarioID, " &
            "core_Scenario.Name AS ScenarioName, " &
            "dgsim_OutputPopulationSize.Iteration, " &
            "dgsim_OutputPopulationSize.Timestep, " &
            "dgsim_Stratum.Name AS StratumName, " &
            "CASE WHEN dgsim_OutputPopulationSize.Sex=0 THEN 'Male' ELSE 'Female' END AS Sex, " &
            "dgsim_AgeClass.Name AS AgeClassName, " &
            "dgsim_OutputPopulationSize.Population " &
            "FROM dgsim_OutputPopulationSize " &
            "INNER JOIN core_Scenario ON core_Scenario.ScenarioID = dgsim_OutputPopulationSize.ScenarioID " &
            "INNER JOIN dgsim_Stratum ON dgsim_OutputPopulationSize.StratumID = dgsim_Stratum.StratumID " &
            "INNER JOIN dgsim_AgeClass ON dgsim_OutputPopulationSize.AgeClassID = dgsim_AgeClass.AgeClassID " &
            "WHERE dgsim_OutputPopulationSize.ScenarioID IN ({0}) " &
            "ORDER BY " &
            "dgsim_OutputPopulationSize.ScenarioID, " &
            "Iteration, " &
            "Timestep, " &
            "StratumName, " &
            "Sex, " &
            "AgeClassName",
            Me.ExportCreateActiveResultScenarioFilter())

        Return Query

    End Function

End Class
