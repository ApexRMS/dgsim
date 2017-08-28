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
            "DGSim_OutputHarvest.ScenarioID, " &
            "SSim_Scenario.Name AS ScenarioName, " &
            "DGSim_OutputHarvest.Iteration, " &
            "DGSim_OutputHarvest.Timestep, " &
            "DGSim_Stratum.Name AS StratumName, " &
            "CASE WHEN DGSim_OutputHarvest.Sex=0 THEN 'Male' ELSE 'Female' END AS Sex, " &
            "DGSim_AgeClass.Name AS AgeClassName, " &
            "DGSim_OutputHarvest.Harvest " &
            "FROM DGSim_OutputHarvest " &
            "INNER JOIN SSim_Scenario ON SSim_Scenario.ScenarioID = DGSim_OutputHarvest.ScenarioID " &
            "INNER JOIN DGSim_Stratum ON DGSim_OutputHarvest.StratumID = DGSim_Stratum.StratumID " &
            "INNER JOIN DGSim_AgeClass ON DGSim_OutputHarvest.AgeClassID = DGSim_AgeClass.AgeClassID " &
            "WHERE DGSim_OutputHarvest.ScenarioID IN ({0}) " &
            "ORDER BY " &
            "DGSim_OutputHarvest.ScenarioID, " &
            "Iteration, " &
            "Timestep, " &
            "StratumName, " &
            "Sex, " &
            "AgeClassName",
            Me.CreateActiveResultScenarioFilter())

        Return Query

    End Function

End Class
