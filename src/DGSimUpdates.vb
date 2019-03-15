'*********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports System.Reflection
Imports System.Globalization

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class DGSimUpdates
    Inherits UpdateProvider

    Public Overrides Sub PerformUpdate(store As DataStore, currentSchemaVersion As Integer)

        PerformUpdateInternal(store, currentSchemaVersion)

#If DEBUG Then

        'Verify that all expected indexes exist after the update because it Is easy to forget to recreate them after 
        'adding a column to an existing table (which requires the table to be recreated if you want to preserve column order.)

        ASSERT_INDEX_EXISTS(store, "DGSim_OutputPopulationSize")
        ASSERT_INDEX_EXISTS(store, "DGSim_OutputHarvest")
        ASSERT_INDEX_EXISTS(store, "DGSim_OutputRecruits")
        ASSERT_INDEX_EXISTS(store, "DGSim_OutputMortality")
        ASSERT_INDEX_EXISTS(store, "DGSim_OutputPosteriorDistributionValue")

#End If

    End Sub

#If DEBUG Then

    Private Shared Sub ASSERT_INDEX_EXISTS(ByVal store As DataStore, ByVal tableName As String)

        If (store.TableExists(tableName)) Then

            Dim IndexName As String = tableName + "_Index"
            Dim Query As String = String.Format(CultureInfo.InvariantCulture, "SELECT COUNT(name) FROM sqlite_master WHERE type = 'index' AND name = '{0}'", IndexName)
            Debug.Assert(CInt(store.ExecuteScalar(Query)) = 1)

        End If

    End Sub

#End If

    Private Shared Sub PerformUpdateInternal(store As DataStore, currentSchemaVersion As Integer)

        If (currentSchemaVersion < 1) Then
            DGSIM0000001(store)
        End If

        If (currentSchemaVersion < 2) Then
            DGSIM0000002(store)
        End If

    End Sub

    ''' <summary>
    ''' DGSIM0000001
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' Adds iteraton and timestep fields to the DGSim_DemographicRateShift table
    ''' </remarks>
    Private Shared Sub DGSIM0000001(ByVal store As DataStore)

        If (store.TableExists("DGSim_DemographicRateShift")) Then

            store.ExecuteNonQuery("ALTER TABLE DGSim_DemographicRateShift RENAME TO DGSim_DemographicRateShiftTEMP")
            store.ExecuteNonQuery("CREATE TABLE DGSim_DemographicRateShift (  DemographicRateShiftID INTEGER PRIMARY KEY AUTOINCREMENT,Iteration INTEGER,Timestep INTEGER, ScenarioID INTEGER, AgeClassID INTEGER, Fecundity DOUBLE, Mortality DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO DGSim_DemographicRateShift (ScenarioID, AgeClassID, Fecundity, Mortality) select ScenarioID, AgeClassID, Fecundity, Mortality from DGSim_DemographicRateShiftTEMP")
            store.ExecuteNonQuery("DROP TABLE DGSim_DemographicRateShiftTEMP")

        End If

    End Sub

    ''' <summary>
    ''' DGSIM0000002
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update:
    ''' 
    ''' (1.) Renames:
    ''' 
    '''      DGSim_AnnualHarvestOption.PopFilterGender ->
    '''      DGSim_AnnualHarvestOption.PopFilterSex
    ''' 
    ''' (2.) Ensures that the following tables have the correct columns to do distributions:
    ''' 
    '''      DGSim_InitialPopulationSize
    '''      DGSim_OffspringPerFemaleValue
    '''      DGSim_AnnualizedMortalityRate
    '''      DGSim_AnnualHarvestValue
    '''      
    ''' (3.) Creates and populates the STime_DistributionType table with the defaults.  Although
    '''      is this not really the job of a module, we need to do this because DG-Sim had hard-coded
    '''      Beta Distributions before this update and we need to configure these records with the Beta 
    '''      Distribution Ids from the new STime_DistributionType.
    '''      
    ''' (4.) Configures each table in #2 to have the Beta Distribution Ids from STime_DistributionType.
    '''      
    ''' </remarks>
    Private Shared Sub DGSIM0000002(ByVal store As DataStore)

        '#1 above

        If (store.TableExists("DGSim_AnnualHarvestOption")) Then

            store.ExecuteNonQuery("ALTER TABLE DGSim_AnnualHarvestOption RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE DGSim_AnnualHarvestOption(AnnualHarvestOptionID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Specification INTEGER, PopFilterSex INTEGER, PopFilterMinAge INTEGER, PopFilterMaxAge INTEGER)")
            store.ExecuteNonQuery("INSERT INTO DGSim_AnnualHarvestOption (ScenarioID, Specification, PopFilterSex, PopFilterMinAge, PopFilterMaxAge) SELECT ScenarioID, Specification, PopFilterGender, PopFilterMinAge, PopFilterMaxAge FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        '#2 above

        If (store.TableExists("DGSim_InitialPopulationSize")) Then

            store.ExecuteNonQuery("ALTER TABLE DGSim_InitialPopulationSize RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE DGSim_InitialPopulationSize (InitialPopulationSizeID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Mean INTEGER, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin INTEGER, DistributionMax INTEGER)")
            store.ExecuteNonQuery("INSERT INTO DGSim_InitialPopulationSize (ScenarioID, Mean, DistributionSD, DistributionMin, DistributionMax) SELECT ScenarioID, Mean, SD, Minimum, Maximum FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("DGSim_OffspringPerFemaleValue")) Then

            store.ExecuteNonQuery("ALTER TABLE DGSim_OffspringPerFemaleValue RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE DGSim_OffspringPerFemaleValue(OffspringPerFemaleValueID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, CountJulianDay INTEGER, StratumID INTEGER, AgeClassID INTEGER, Mean DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO DGSim_OffspringPerFemaleValue(ScenarioID, Iteration, Timestep, CountJulianDay, StratumID, AgeClassID, Mean, DistributionSD, DistributionMin, DistributionMax) SELECT ScenarioID, Iteration, Timestep, CountJulianDay, StratumID, AgeClassID, Mean, SD, Minimum, Maximum FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("DGSim_AnnualizedMortalityRate")) Then

            store.ExecuteNonQuery("ALTER TABLE DGSim_AnnualizedMortalityRate RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE DGSim_AnnualizedMortalityRate(AnnualizedMortalityRateID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, JulianDay INTEGER, StratumID INTEGER, Sex INTEGER, AgeClassID INTEGER, Mean DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO DGSim_AnnualizedMortalityRate(ScenarioID, Iteration, Timestep, JulianDay, StratumID, Sex, AgeClassID, Mean, DistributionSD, DistributionMin, DistributionMax) SELECT ScenarioID, Iteration, Timestep, JulianDay, StratumID, Sex, AgeClassID, Mean, SD, Minimum, Maximum FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("DGSim_AnnualHarvestValue")) Then

            store.ExecuteNonQuery("ALTER TABLE DGSim_AnnualHarvestValue RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE DGSim_AnnualHarvestValue(AnnualHarvestValueID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, Sex INTEGER, AgeClassID INTEGER, Mean DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO DGSim_AnnualHarvestValue(ScenarioID, Iteration, Timestep, StratumID, Sex, AgeClassID, Mean, DistributionSD, DistributionMin, DistributionMax) SELECT ScenarioID, Iteration, Timestep, StratumID, Sex, AgeClassID, Mean, SD, Minimum, Maximum FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        '#3 above

        EnsureDistributionTypes(store)

        '#4 above

        Dim Scenarios As DataTable = store.CreateDataTable("SSim_Scenario")
        Dim BetaDistIds As Dictionary(Of Integer, Integer) = CreateProjectXDistTypeIdDictionary(store)

        For Each dr In Scenarios.Rows

            Dim ProjectId As Integer = CInt(dr("ProjectID"))
            Dim ScenarioId As Integer = CInt(dr("ScenarioID"))
            Dim BetaDistId As Integer = BetaDistIds(ProjectId)
            Dim Template As String = "UPDATE {0} SET DistributionType={1} WHERE ScenarioID={2} AND DistributionSD IS NOT NULL"
            Dim Query As String = Nothing

            Query = String.Format(CultureInfo.InvariantCulture, Template, "DGSim_InitialPopulationSize", BetaDistId, ScenarioId)
            store.ExecuteNonQuery(Query)

            Query = String.Format(CultureInfo.InvariantCulture, Template, "DGSim_OffspringPerFemaleValue", BetaDistId, ScenarioId)
            store.ExecuteNonQuery(Query)

            Query = String.Format(CultureInfo.InvariantCulture, Template, "DGSim_AnnualizedMortalityRate", BetaDistId, ScenarioId)
            store.ExecuteNonQuery(Query)

            Query = String.Format(CultureInfo.InvariantCulture, Template, "DGSim_AnnualHarvestValue", BetaDistId, ScenarioId)
            store.ExecuteNonQuery(Query)

        Next

    End Sub

    Private Shared Function CreateProjectXDistTypeIdDictionary(ByVal store As DataStore) As Dictionary(Of Integer, Integer)

        Dim DistTypes As DataTable = store.CreateDataTableFromQuery("SELECT * FROM STime_DistributionType WHERE Name='Beta'", "DistTypes")
        Dim BetaDistIds As Dictionary(Of Integer, Integer) = New Dictionary(Of Integer, Integer)

        For Each dr As DataRow In DistTypes.Rows

            Dim ProjectId As Integer = CInt(dr("ProjectID"))
            Dim BetaDistTypeId As Integer = CInt(dr("DistributionTypeID"))

            BetaDistIds.Add(ProjectId, BetaDistTypeId)

        Next

        Return BetaDistIds

    End Function

    Private Shared Sub EnsureDistributionTypes(ByVal store As DataStore)

        'Note that it is possible that STime_DistributionType already exists.  If it does, 
        'it should have the correct distribution type records.

        If (store.TableExists("STime_DistributionType")) Then
            Return
        End If

        store.ExecuteNonQuery("CREATE TABLE STime_DistributionType ( 
            DistributionTypeID INTEGER PRIMARY KEY,
            ProjectID          INTEGER,
            Name               TEXT,
            Description        TEXT,
            IsInternal         INTEGER)")

        '<defaultRecords>
        '  <record columns = "Name|Description|IsInternal" values="Beta|Beta Distribution|-1"/>
        '  <record columns = "Name|Description|IsInternal" values="Normal|Normal Distribution|-1"/>
        '  <record columns = "Name|Description|IsInternal" values="Uniform|Uniform Distribution|-1"/>
        '  <record columns = "Name|Description|IsInternal" values="Uniform Integer|Uniform Integer Distribution|-1"/>
        '</defaultRecords>

        Dim Projects As DataTable = store.CreateDataTable("SSim_Project")

        For Each ProjectRow As DataRow In Projects.Rows

            Dim ProjectId As Integer = CInt(ProjectRow("ProjectID"))
            Dim BetaDistTypeId As Integer = Library.GetNextSequenceId(store)
            Dim NormalDistTypeId As Integer = Library.GetNextSequenceId(store)
            Dim UniformDistTypeId As Integer = Library.GetNextSequenceId(store)
            Dim UniformIntegerDistTypeId As Integer = Library.GetNextSequenceId(store)
            Dim Template As String = "INSERT INTO STime_DistributionType(DistributionTypeID, ProjectID, Name, Description, IsInternal) VALUES({0}, {1}, '{2}', '{3}', -1)"
            Dim Query As String = Nothing

            Query = String.Format(CultureInfo.InvariantCulture, Template, BetaDistTypeId, ProjectId, "Beta", "Beta Distribution")
            store.ExecuteNonQuery(Query)

            Query = String.Format(CultureInfo.InvariantCulture, Template, NormalDistTypeId, ProjectId, "Normal", "Normal Distribution")
            store.ExecuteNonQuery(Query)

            Query = String.Format(CultureInfo.InvariantCulture, Template, UniformDistTypeId, ProjectId, "Uniform", "Uniform Distribution")
            store.ExecuteNonQuery(Query)

            Query = String.Format(CultureInfo.InvariantCulture, Template, UniformIntegerDistTypeId, ProjectId, "Uniform Integer", "Uniform Integer Distribution")
            store.ExecuteNonQuery(Query)

        Next

    End Sub

End Class
