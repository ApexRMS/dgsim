'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographics of wildlife populations.
'
' Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Common

Class OffspringPerFemaleValueMap

    Private m_map As New MultiLevelKeyMap2(Of SortedKeyMap2(Of OffspringPerFemaleValue))

    Public Sub Initialize(
        ByVal items As OffspringPerFemaleValueCollection,
        ByVal maxIterations As Integer)

        For Each item As OffspringPerFemaleValue In items

            Dim m As SortedKeyMap2(Of OffspringPerFemaleValue) = Me.m_map.GetItemExact(item.StratumId, item.AgeClassId)

            If (m Is Nothing) Then

                m = New SortedKeyMap2(Of OffspringPerFemaleValue)(SearchMode.ExactPrevNext)
                Me.m_map.AddItem(item.StratumId, item.AgeClassId, m)

            End If

            If (item.Iteration.HasValue) Then

                item.ReSample()
                m.AddItem(item.Iteration, item.Timestep, item)

            Else

                For Iteration As Integer = 1 To maxIterations

                    Dim NewItem As New OffspringPerFemaleValue(
                        item.Project,
                        item.StratumId,
                        Iteration,
                        item.Timestep,
                        item.AgeClassId,
                        item.DistributionMean,
                        item.DistributionSD,
                        item.DistributionMinimum,
                        item.DistributionMaximum,
                        item.RandomGenerator,
                        item.CountJulianDay)

                    NewItem.Initialize()
                    NewItem.ReSample()
                    m.AddItem(Iteration, item.Timestep, NewItem)

                Next

            End If

        Next

    End Sub

    Public Function GetItem(
        ByVal stratumId As Integer,
        ByVal ageClassId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As OffspringPerFemaleValue

        Dim m As SortedKeyMap2(Of OffspringPerFemaleValue) = Me.m_map.GetItem(stratumId, ageClassId)

        If (m Is Nothing) Then
            Return Nothing
        End If

        Return m.GetItem(iteration, timestep)

    End Function

End Class
