'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Reflection
Imports System.Globalization
Imports SyncroSim.Core.Forms

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class BirthsReport
    Inherits ExportTransformer

    Protected Overrides Sub Export(location As String, exportType As ExportType)

        Dim query As String = Me.CreateReportQuery()
        Dim columns As ExportColumnCollection = CreateColumnCollection()

        If (exportType = ExportType.ExcelFile) Then
            Me.ExcelExport(location, columns, query, "Births")
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
        c.Add(New ExportColumn("MotherAgeClassName", "Mother Age Class"))
        c.Add(New ExportColumn("OffspringSex", "Offspring Sex"))
        c.Add(New ExportColumn("Births", "Births"))

        c("Births").Alignment = Core.ColumnAlignment.Right

        Return c

    End Function

    Private Function CreateReportQuery() As String

        Dim Query As String = String.Format(CultureInfo.InvariantCulture,
            "SELECT " &
            "DGSim_OutputBirths.ScenarioID, " &
            "SSim_Scenario.Name AS ScenarioName, " &
            "DGSim_OutputBirths.Iteration, " &
            "DGSim_OutputBirths.Timestep, " &
            "DGSim_Stratum.Name AS StratumName, " &
            "CASE WHEN DGSim_OutputBirths.OffspringSex=0 THEN 'Male' ELSE 'Female' END AS OffspringSex, " &
            "DGSim_AgeClass.Name AS MotherAgeClassName, " &
            "DGSim_OutputBirths.Births " &
            "FROM DGSim_OutputBirths " &
            "INNER JOIN SSim_Scenario ON SSim_Scenario.ScenarioID = DGSim_OutputBirths.ScenarioID " &
            "INNER JOIN DGSim_Stratum ON DGSim_OutputBirths.StratumID = DGSim_Stratum.StratumID " &
            "INNER JOIN DGSim_AgeClass ON DGSim_OutputBirths.MotherAgeClassID = DGSim_AgeClass.AgeClassID " &
            "WHERE DGSim_OutputBirths.ScenarioID IN ({0}) " &
            "ORDER BY " &
            "DGSim_OutputBirths.ScenarioID, " &
            "Iteration, " &
            "Timestep, " &
            "StratumName, " &
            "MotherAgeClassName, " &
            "OffspringSex",
            Me.CreateActiveResultScenarioFilter())

        Return Query

    End Function

End Class
