﻿'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'**********************************************************************************************

Imports SyncroSim.Common
Imports System.Collections.ObjectModel

Friend Class SummaryOutputMortalityCollection
    Inherits KeyedCollection(Of ThreeIntegerLookupKey, SummaryOutputMortality)

    Public Sub New()
        MyBase.New(New ThreeIntegerLookupKeyEqualityComparer)
    End Sub

    Protected Overrides Function GetKeyForItem(ByVal item As SummaryOutputMortality) As ThreeIntegerLookupKey
        Return New ThreeIntegerLookupKey(item.StratumId, CInt(item.Sex), item.AgeClassId)
    End Function

End Class
