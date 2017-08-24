'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Common
Imports System.Collections.ObjectModel

Friend Class SummaryOutputBirthsCollection
    Inherits KeyedCollection(Of ThreeIntegerLookupKey, SummaryOutputBirths)

    Public Sub New()
        MyBase.New(New ThreeIntegerLookupKeyEqualityComparer)
    End Sub

    Protected Overrides Function GetKeyForItem(ByVal item As SummaryOutputBirths) As ThreeIntegerLookupKey
        Return New ThreeIntegerLookupKey(item.StratumId, item.MotherAgeClassId, CInt(item.OffspringSex))
    End Function

End Class
