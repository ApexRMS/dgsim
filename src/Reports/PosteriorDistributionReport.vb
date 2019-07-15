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
Class PosteriorDistributionReport
    Inherits ExportTransformer

    Protected Overrides Sub Export(location As String, exportType As ExportType)

        Dim query As String = Me.CreateReportQuery()
        Dim columns As ExportColumnCollection = CreateColumnCollection()

        If (exportType = ExportType.ExcelFile) Then
            Me.ExcelExport(location, columns, query, "Sampled Inputs by Iteration")
        Else
            Me.CSVExport(location, columns, query)
            InformationMessageBox("Data saved to '{0}'.", location)
        End If

    End Sub

    Private Shared Function CreateColumnCollection() As ExportColumnCollection

        Dim c As New ExportColumnCollection

        c.Add(New ExportColumn("ScenarioID", "Scenario ID"))
        c.Add(New ExportColumn("ScenarioName", "Scenario"))
        c.Add(New ExportColumn("StratumName", "Stratum"))
        c.Add(New ExportColumn("Iteration", "Iteration"))
        c.Add(New ExportColumn("Timestep", "Timestep"))
        c.Add(New ExportColumn("HasCensusData", "Has Census Data"))
        c.Add(New ExportColumn("JulianDay", "Julian Day"))
        c.Add(New ExportColumn("Sex", "Sex"))
        c.Add(New ExportColumn("AgeClassName", "Age Class"))
        c.Add(New ExportColumn("VariableName", "Variable"))
        c.Add(New ExportColumn("IsFilteredName", "Filtered"))
        c.Add(New ExportColumn("Value", "Value"))

        c("Value").Alignment = Core.ColumnAlignment.Right

        Return c

    End Function

    Private Function CreateReportQuery() As String

        Dim Query As String = String.Format(CultureInfo.InvariantCulture,
            "SELECT dgsim__OutputPosteriorDistributionValue.ScenarioID, " &
            "system__Scenario.Name as ScenarioName, " &
            "dgsim__Stratum.Name as StratumName, " &
            "Iteration, " &
            "Timestep, " &
            "CASE WHEN HasCensusData=0 THEN 'No' ELSE 'Yes' END AS HasCensusData, " &
            "JulianDay, " &
            "CASE WHEN Sex=0 THEN 'Male' WHEN Sex=1 THEN 'Female' ELSE NULL END AS Sex, " &
            "dgsim__AgeClass.Name as AgeClassName, " &
            "CASE WHEN Variable=0 THEN 'Harvest' WHEN Variable=1 THEN 'Mortality' ELSE 'Offspring' END AS VariableName, " &
            "CASE WHEN IsFiltered=0 THEN 'No' ELSE 'Yes' END AS IsFilteredName, " &
            "Value " &
            "FROM(dgsim__OutputPosteriorDistributionValue) " &
            "INNER JOIN system__Scenario ON dgsim__OutputPosteriorDistributionValue.ScenarioID = system__Scenario.ScenarioID " &
            "LEFT JOIN dgsim__Stratum ON dgsim__OutputPosteriorDistributionValue.StratumID = dgsim__Stratum.StratumID " &
            "LEFT JOIN dgsim__AgeClass ON dgsim__OutputPosteriorDistributionValue.AgeClassID = dgsim__AgeClass.AgeClassID " &
            "WHERE dgsim__OutputPosteriorDistributionValue.ScenarioID IN ({0}) " &
            "ORDER BY dgsim__OutputPosteriorDistributionValue.ScenarioID, dgsim__Stratum.Name, Iteration, Timestep, JulianDay, Sex, dgsim__AgeClass.Name, IsFiltered, Variable",
            Me.CreateActiveResultScenarioFilter())

        Return Query

    End Function

End Class
