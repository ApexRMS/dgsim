﻿'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'**********************************************************************************************

Imports SyncroSim.Common
Imports System.Collections.ObjectModel

Friend Class SummaryOutputPopulationSizeCollection
    Inherits KeyedCollection(Of ThreeIntegerLookupKey, SummaryOutputPopulationSize)

    Public Sub New()
        MyBase.New(New ThreeIntegerLookupKeyEqualityComparer)
    End Sub

    Protected Overrides Function GetKeyForItem(ByVal item As SummaryOutputPopulationSize) As ThreeIntegerLookupKey
        Return New ThreeIntegerLookupKey(item.StratumId, CInt(item.Sex), item.AgeClassId)
    End Function

End Class
