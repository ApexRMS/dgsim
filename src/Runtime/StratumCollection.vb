'*********************************************************************************************
' DG-Sim: A SyncroSim Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Collections.ObjectModel

Class StratumCollection
    Inherits KeyedCollection(Of Integer, Stratum)

    Protected Overrides Function GetKeyForItem(ByVal item As Stratum) As Integer
        Return item.Id
    End Function

End Class
