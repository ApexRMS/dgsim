'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class DGSimUpdates
    Inherits UpdateProvider

    ''' <summary>
    ''' Performs the database updates for DG-Sim
    ''' </summary>
    ''' <param name="store"></param>
    ''' <param name="currentSchemaVersion"></param>
    ''' <remarks>
    ''' </remarks>
    Public Overrides Sub PerformUpdate(store As DataStore, currentSchemaVersion As Integer)

        If (currentSchemaVersion < 1) Then
            DGSIM0000001(store)
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

End Class
