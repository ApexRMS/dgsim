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

    Protected Overrides Sub OnAfterUpdate(store As DataStore)

        MyBase.OnAfterUpdate(store)

#If DEBUG Then

        'Verify that all expected indexes exist after the update because it Is easy to forget to recreate them after 
        'adding a column to an existing table (which requires the table to be recreated if you want to preserve column order.)

        ASSERT_INDEX_EXISTS(store, "dgsim_OutputPopulationSize")
        ASSERT_INDEX_EXISTS(store, "dgsim_OutputHarvest")
        ASSERT_INDEX_EXISTS(store, "dgsim_OutputRecruits")
        ASSERT_INDEX_EXISTS(store, "dgsim_OutputMortality")
        ASSERT_INDEX_EXISTS(store, "dgsim_OutputPosteriorDistributionValue")

#End If

    End Sub

#If DEBUG Then

    Private Shared Sub ASSERT_INDEX_EXISTS(ByVal store As DataStore, ByVal tableName As String)

        If (store.TableExists(tableName)) Then

            Dim IndexName As String = tableName + "_Index"
            Dim Query As String = String.Format("SELECT COUNT(name) FROM sqlite_master WHERE type = 'index' AND name = '{0}'", IndexName)
            Debug.Assert(CInt(store.ExecuteScalar(Query)) = 1)

        End If

    End Sub

#End If

End Class
