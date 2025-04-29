'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'**********************************************************************************************

Imports SyncroSim.Core
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Partial Class DGSimUpdates
    Inherits DotNetUpdateProvider

    <Update(0.101, "This is the final update to move from v2 to v3")>
    Public Shared Sub Update_0_101(ByVal store As DataStore)

        RenameTablesWithPrefix(store, "DGSim_", "dgsim_")

        store.ExecuteNonQuery("DROP INDEX IF EXISTS DGSim_OutputPopulationSize_Index")
        store.ExecuteNonQuery("DROP INDEX IF EXISTS DGSim_OutputHarvest_Index")
        store.ExecuteNonQuery("DROP INDEX IF EXISTS DGSim_OutputRecruits_Index")
        store.ExecuteNonQuery("DROP INDEX IF EXISTS DGSim_OutputMortality_Index")
        store.ExecuteNonQuery("DROP INDEX IF EXISTS DGSim_OutputPosteriorDistributionValue_Index")

        CreateIndex(store, "dgsim_OutputPopulationSize", New String() {"ScenarioID", "Iteration", "Timestep", "StratumID", "Sex", "AgeClassID"})
        CreateIndex(store, "dgsim_OutputHarvest", New String() {"ScenarioID", "Iteration", "Timestep", "StratumID", "Sex", "AgeClassID"})
        CreateIndex(store, "dgsim_OutputRecruits", New String() {"ScenarioID", "Iteration", "Timestep", "StratumID", "MotherAgeClassID", "OffspringSex"})
        CreateIndex(store, "dgsim_OutputMortality", New String() {"ScenarioID", "Iteration", "Timestep", "StratumID", "Sex", "AgeClassID"})
        CreateIndex(store, "dgsim_OutputPosteriorDistributionValue", New String() {"ScenarioID", "Iteration", "Timestep", "StratumID", "Sex", "AgeClassID"})

        store.ExecuteNonQuery("UPDATE core_Chart SET Criteria = REPLACE(Criteria, 'DGSim_', 'dgsim_')")
        store.ExecuteNonQuery("UPDATE core_Chart SET Criteria = REPLACE(Criteria, 'Population', 'dgsim_Population')")
        store.ExecuteNonQuery("UPDATE core_Chart SET Criteria = REPLACE(Criteria, 'Harvest', 'dgsim_Harvest')")
        store.ExecuteNonQuery("UPDATE core_Chart SET Criteria = REPLACE(Criteria, 'Recruits', 'dgsim_Recruits')")
        store.ExecuteNonQuery("UPDATE core_Chart SET Criteria = REPLACE(Criteria, 'Mortality', 'dgsim_Mortality')")

    End Sub



End Class
