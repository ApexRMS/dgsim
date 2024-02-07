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
Class RecruitsReport
    Inherits ExportTransformer

    Protected Overrides Sub Export(location As String, exportType As ExportType)

        Dim query As String = Me.CreateReportQuery()
        Dim columns As ExportColumnCollection = CreateColumnCollection()

        If (exportType = ExportType.ExcelFile) Then
            Me.ExportToExcel(location, columns, query, "Recruits")
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
            "dgsim_OutputRecruits.ScenarioID, " &
            "core_Scenario.Name AS ScenarioName, " &
            "dgsim_OutputRecruits.Iteration, " &
            "dgsim_OutputRecruits.Timestep, " &
            "dgsim_Stratum.Name AS StratumName, " &
            "CASE WHEN dgsim_OutputRecruits.OffspringSex=0 THEN 'Male' ELSE 'Female' END AS OffspringSex, " &
            "dgsim_AgeClass.Name AS MotherAgeClassName, " &
            "dgsim_OutputRecruits.Recruits " &
            "FROM dgsim_OutputRecruits " &
            "INNER JOIN core_Scenario ON core_Scenario.ScenarioID = dgsim_OutputRecruits.ScenarioID " &
            "INNER JOIN dgsim_Stratum ON dgsim_OutputRecruits.StratumID = dgsim_Stratum.StratumID " &
            "INNER JOIN dgsim_AgeClass ON dgsim_OutputRecruits.MotherAgeClassID = dgsim_AgeClass.AgeClassID " &
            "WHERE dgsim_OutputRecruits.ScenarioID IN ({0}) " &
            "ORDER BY " &
            "dgsim_OutputRecruits.ScenarioID, " &
            "Iteration, " &
            "Timestep, " &
            "StratumName, " &
            "MotherAgeClassName, " &
            "OffspringSex",
            Me.ExportCreateActiveResultScenarioFilter())

        Return Query

    End Function

End Class
