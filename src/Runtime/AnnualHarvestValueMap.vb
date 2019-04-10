'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'**********************************************************************************************

Imports SyncroSim.Common

Class AnnualHarvestValueMap

    Private m_Map As New MultiLevelKeyMap1(Of SortedKeyMap3(Of AnnualHarvestValue))

    Public Sub Initialize(ByVal items As AnnualHarvestValueCollection, ByVal runControl As RunControl)

        Dim id As Integer = 0

        For Each item As AnnualHarvestValue In items

            Dim m As SortedKeyMap3(Of AnnualHarvestValue) = Me.m_Map.GetItemExact(item.StratumId)

            If (m Is Nothing) Then

                m = New SortedKeyMap3(Of AnnualHarvestValue)(SearchMode.ExactPrevNext)
                Me.m_Map.AddItem(item.StratumId, m)

            End If

            If (item.Iteration.HasValue) Then

                item.ReSample()
                m.AddItem(item.Iteration, item.Timestep, id, item)

                id += 1

            Else

                For Iteration As Integer = runControl.MinimumIteration To runControl.MaximumIteration

                    Dim NewItem As New AnnualHarvestValue(
                        item.Project,
                        item.StratumId,
                        Iteration,
                        item.Timestep,
                        item.AgeClassId,
                        item.Sex,
                        item.Mean,
                        item.DistributionType,
                        item.DistributionSD,
                        item.DistributionMin,
                        item.DistributionMax,
                        item.DistributionProvider)

                    NewItem.Initialize()
                    NewItem.ReSample()
                    m.AddItem(Iteration, item.Timestep, id, NewItem)

                    id += 1

                Next

            End If

        Next

    End Sub

    Public Function GetItems(
        ByVal stratumId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As SortedList(Of Integer, AnnualHarvestValue)

        Dim m As SortedKeyMap1(Of AnnualHarvestValue) = Me.GetFinalMap(stratumId, iteration, timestep)

        If (m Is Nothing OrElse m.Map.Count = 0) Then
            Return Nothing
        End If

        Return m.Map.Items

    End Function

    Private Function GetFinalMap(
        ByVal stratumId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As SortedKeyMap1(Of AnnualHarvestValue)

        Dim m1 As SortedKeyMap3(Of AnnualHarvestValue) = Me.m_Map.GetItem(stratumId)

        If (m1 Is Nothing OrElse m1.Map.Count = 0) Then
            Return Nothing
        End If

        Dim m2 As SortedKeyMap2(Of AnnualHarvestValue) = m1.Map.GetItem(iteration)

        If (m2 Is Nothing OrElse m2.Map.Count = 0) Then
            Return Nothing
        End If

        Dim m3 As SortedKeyMap1(Of AnnualHarvestValue) = m2.Map.GetItem(timestep)

        If (m3 Is Nothing OrElse m3.Map.Count = 0) Then
            Return Nothing
        End If

        Return m3

    End Function

End Class
