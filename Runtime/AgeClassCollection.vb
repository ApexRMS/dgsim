'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Collections.ObjectModel

Class AgeClassCollection
    Inherits KeyedCollection(Of Integer, AgeClass)

    Protected Overrides Function GetKeyForItem(ByVal item As AgeClass) As Integer
        Return item.Id
    End Function

End Class
