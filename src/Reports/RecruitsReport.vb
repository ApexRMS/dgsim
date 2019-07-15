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
            "dgsim__OutputRecruits.ScenarioID, " &
            "system__Scenario.Name AS ScenarioName, " &
            "dgsim__OutputRecruits.Iteration, " &
            "dgsim__OutputRecruits.Timestep, " &
            "dgsim__Stratum.Name AS StratumName, " &
            "CASE WHEN dgsim__OutputRecruits.OffspringSex=0 THEN 'Male' ELSE 'Female' END AS OffspringSex, " &
            "dgsim__AgeClass.Name AS MotherAgeClassName, " &
            "dgsim__OutputRecruits.Recruits " &
            "FROM dgsim__OutputRecruits " &
            "INNER JOIN system__Scenario ON system__Scenario.ScenarioID = dgsim__OutputRecruits.ScenarioID " &
            "INNER JOIN dgsim__Stratum ON dgsim__OutputRecruits.StratumID = dgsim__Stratum.StratumID " &
            "INNER JOIN dgsim__AgeClass ON dgsim__OutputRecruits.MotherAgeClassID = dgsim__AgeClass.AgeClassID " &
            "WHERE dgsim__OutputRecruits.ScenarioID IN ({0}) " &
            "ORDER BY " &
            "dgsim__OutputRecruits.ScenarioID, " &
            "Iteration, " &
            "Timestep, " &
            "StratumName, " &
            "MotherAgeClassName, " &
            "OffspringSex",
            Me.CreateActiveResultScenarioFilter())

        Return Query

    End Function

End Class
