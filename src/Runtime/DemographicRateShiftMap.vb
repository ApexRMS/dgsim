'*********************************************************************************************
' DG-Sim: A SyncroSim Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Common

Class DemographicRateShiftMap

    Private m_map As New MultiLevelKeyMap3(Of DemographicRateShift)

    Public Sub Initialize(ByVal items As DemographicRateShiftCollection)

        For Each item As DemographicRateShift In items
            Me.m_map.AddItem(item.Iteration, item.Timestep, item.AgeClassId, item)
        Next

    End Sub

    Public Function GetFecundityAdjustment(ByVal iteration As Nullable(Of Integer), ByVal timestep As Nullable(Of Integer), ByVal ageClassId As Integer) As Double

        Dim shift As DemographicRateShift = Me.m_map.GetItem(iteration, timestep, ageClassId)

        If (shift IsNot Nothing) Then
            Return shift.Fecundity
        Else
            Return 0.0
        End If

    End Function

    Public Function GetMortalityAdjustment(ByVal iteration As Nullable(Of Integer), ByVal timestep As Nullable(Of Integer), ByVal ageClassId As Integer) As Double

        Dim shift As DemographicRateShift = Me.m_map.GetItem(iteration, timestep, ageClassId)

        If (shift IsNot Nothing) Then
            Return shift.Mortality
        Else
            Return 0.0
        End If

    End Function

End Class
