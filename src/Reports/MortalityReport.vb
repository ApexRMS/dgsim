﻿'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'**********************************************************************************************

Imports System.Reflection
Imports System.Globalization
Imports SyncroSim.Core.Forms

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class MortalityReport
    Inherits ExportTransformer

    Protected Overrides Sub Export(location As String, exportType As ExportType)

        Dim query As String = Me.CreateReportQuery()
        Dim columns As ExportColumnCollection = CreateColumnCollection()

        If (exportType = ExportType.ExcelFile) Then
            Me.ExcelExport(location, columns, query, "Mortality")
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
        c.Add(New ExportColumn("Mortality", "Mortality"))

        c("Mortality").DecimalPlaces = 0
        c("Mortality").Alignment = Core.ColumnAlignment.Right

        Return c

    End Function

    Private Function CreateReportQuery() As String

        Dim Query As String = String.Format(CultureInfo.InvariantCulture,
            "SELECT " &
            "dgsim_OutputMortality.ScenarioID, " &
            "core_Scenario.Name AS ScenarioName, " &
            "dgsim_OutputMortality.Iteration, " &
            "dgsim_OutputMortality.Timestep, " &
            "dgsim_Stratum.Name AS StratumName, " &
            "CASE WHEN dgsim_OutputMortality.Sex=0 THEN 'Male' ELSE 'Female' END AS Sex, " &
            "dgsim_AgeClass.Name AS AgeClassName, " &
            "dgsim_OutputMortality.Mortality " &
            "FROM dgsim_OutputMortality " &
            "INNER JOIN core_Scenario ON core_Scenario.ScenarioID = dgsim_OutputMortality.ScenarioID " &
            "INNER JOIN dgsim_Stratum ON dgsim_OutputMortality.StratumID = dgsim_Stratum.StratumID " &
            "INNER JOIN dgsim_AgeClass ON dgsim_OutputMortality.AgeClassID = dgsim_AgeClass.AgeClassID " &
            "WHERE dgsim_OutputMortality.ScenarioID IN ({0}) " &
            "ORDER BY " &
            "dgsim_OutputMortality.ScenarioID, " &
            "Iteration, " &
            "Timestep, " &
            "StratumName, " &
            "Sex, " &
            "AgeClassName",
            Me.CreateActiveResultScenarioFilter())

        Return Query

    End Function

End Class
