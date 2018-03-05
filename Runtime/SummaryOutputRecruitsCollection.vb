'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographics of wildlife populations.
'
' Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Common
Imports System.Collections.ObjectModel

Friend Class SummaryOutputRecruitsCollection
    Inherits KeyedCollection(Of ThreeIntegerLookupKey, SummaryOutputRecruits)

    Public Sub New()
        MyBase.New(New ThreeIntegerLookupKeyEqualityComparer)
    End Sub

    Protected Overrides Function GetKeyForItem(ByVal item As SummaryOutputRecruits) As ThreeIntegerLookupKey
        Return New ThreeIntegerLookupKey(item.StratumId, item.MotherAgeClassId, CInt(item.OffspringSex))
    End Function

End Class
