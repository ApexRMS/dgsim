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
Class RecruitsReport
    Inherits ExportTransformer

    Protected Overrides Sub Export(location As String, exportType As ExportType)

        Dim query As String = Me.CreateReportQuery()
        Dim columns As ExportColumnCollection = CreateColumnCollection()

        If (exportType = ExportType.ExcelFile) Then
            Me.ExcelExport(location, columns, query, "Recruits")
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
        c.Add(New ExportColumn("Recruits", "Recruits"))

        c("Recruits").DecimalPlaces = 0
        c("Recruits").Alignment = Core.ColumnAlignment.Right

        Return c

    End Function

    Private Function CreateReportQuery() As String

        Dim Query As String = String.Format(CultureInfo.InvariantCulture,
            "SELECT " &
            "DGSim_OutputRecruits.ScenarioID, " &
            "SSim_Scenario.Name AS ScenarioName, " &
            "DGSim_OutputRecruits.Iteration, " &
            "DGSim_OutputRecruits.Timestep, " &
            "DGSim_Stratum.Name AS StratumName, " &
            "CASE WHEN DGSim_OutputRecruits.OffspringSex=0 THEN 'Male' ELSE 'Female' END AS OffspringSex, " &
            "DGSim_AgeClass.Name AS MotherAgeClassName, " &
            "DGSim_OutputRecruits.Recruits " &
            "FROM DGSim_OutputRecruits " &
            "INNER JOIN SSim_Scenario ON SSim_Scenario.ScenarioID = DGSim_OutputRecruits.ScenarioID " &
            "INNER JOIN DGSim_Stratum ON DGSim_OutputRecruits.StratumID = DGSim_Stratum.StratumID " &
            "INNER JOIN DGSim_AgeClass ON DGSim_OutputRecruits.MotherAgeClassID = DGSim_AgeClass.AgeClassID " &
            "WHERE DGSim_OutputRecruits.ScenarioID IN ({0}) " &
            "ORDER BY " &
            "DGSim_OutputRecruits.ScenarioID, " &
            "Iteration, " &
            "Timestep, " &
            "StratumName, " &
            "MotherAgeClassName, " &
            "OffspringSex",
            Me.CreateActiveResultScenarioFilter())

        Return Query

    End Function

End Class
