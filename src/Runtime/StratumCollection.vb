'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'**********************************************************************************************

Imports System.Collections.ObjectModel

Class StratumCollection
    Inherits KeyedCollection(Of Integer, Stratum)

    Protected Overrides Function GetKeyForItem(ByVal item As Stratum) As Integer
        Return item.Id
    End Function

End Class
