'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Common

Class CensusDataMap

    Private m_map As New MultiLevelKeyMap2(Of CensusData)

    Public Sub Initialize(ByVal items As CensusDataCollection)

        For Each item As CensusData In items
            Me.m_map.AddItem(item.StratumId, item.Timestep, item)
        Next

    End Sub

    Public Function GetItem(ByVal stratumId As Integer, ByVal timestep As Integer) As CensusData
        Return Me.m_map.GetItem(stratumId, timestep)
    End Function

End Class
