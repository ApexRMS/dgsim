'*********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Common

Class OffspringPerFemaleValueMap

    Private m_map As New MultiLevelKeyMap2(Of SortedKeyMap2(Of OffspringPerFemaleValue))

    Public Sub Initialize(ByVal items As OffspringPerFemaleValueCollection, ByVal runControl As RunControl)

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

                For Iteration As Integer = runControl.MinimumIteration To runControl.MaximumIteration

                    Dim NewItem As New OffspringPerFemaleValue(
                        item.Project,
                        item.StratumId,
                        Iteration,
                        item.Timestep,
                        item.AgeClassId,
                        item.CountJulianDay,
                        item.Mean,
                        item.DistributionType,
                        item.DistributionSD,
                        item.DistributionMin,
                        item.DistributionMax,
                        item.DistributionProvider)

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
