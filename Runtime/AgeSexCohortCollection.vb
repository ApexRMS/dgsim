'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Common
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
