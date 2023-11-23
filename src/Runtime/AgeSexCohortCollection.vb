'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'**********************************************************************************************

Imports SyncroSim.Apex
Imports System.Collections.ObjectModel

Class AgeSexCohortCollection
    Inherits KeyedCollection(Of TwoIntegerLookupKey, AgeSexCohort)

    Public Sub New()
        MyBase.New(New TwoIntegerLookupKeyEqualityComparer)
    End Sub

    Protected Overrides Function GetKeyForItem(ByVal item As AgeSexCohort) As TwoIntegerLookupKey
        Return New TwoIntegerLookupKey(item.InitialAge, item.Sex)
    End Function

End Class
