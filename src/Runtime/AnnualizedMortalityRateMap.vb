'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'**********************************************************************************************

Imports SyncroSim.Apex

Class AnnualizedMortalityRateMap

    Private m_Map As New MultiLevelKeyMap3(Of SortedKeyMap3(Of AnnualizedMortalityRate))

    Public Sub Initialize(ByVal items As AnnualizedMortalityRateCollection, ByVal runControl As RunControl)

        For Each item As AnnualizedMortalityRate In items

            Dim m As SortedKeyMap3(Of AnnualizedMortalityRate) =
                Me.m_Map.GetItemExact(item.StratumId, item.Sex, item.AgeClassId)

            If (m Is Nothing) Then

                m = New SortedKeyMap3(Of AnnualizedMortalityRate)(SearchMode.ExactPrevNext)
                Me.m_Map.AddItem(item.StratumId, item.Sex, item.AgeClassId, m)

            End If

            Dim ts As Nullable(Of Integer) = item.Timestep

            If (item.Timestep.HasValue) Then

                If (item.JulianDay.HasValue) Then

                    If (item.JulianDay.Value < runControl.StartJulianDay) Then
                        ts -= 1
                    End If

                End If

            End If

            If (item.Iteration.HasValue) Then

                item.ReSample()

                If (item.JulianDay.HasValue) Then
                    m.AddItem(item.Iteration, ts, item.RelativeJulianDay, item)
                Else
                    m.AddItem(item.Iteration, ts, item.JulianDay, item)
                End If

            Else

                For Iteration As Integer = runControl.MinimumIteration To runControl.MaximumIteration

                    Dim NewItem As New AnnualizedMortalityRate(
                        item.Project,
                        item.StratumId,
                        Iteration,
                        item.Timestep,
                        item.AgeClassId,
                        item.JulianDay,
                        item.Sex,
                        item.Mean,
                        item.DistributionType,
                        item.DistributionSD,
                        item.DistributionMin,
                        item.DistributionMax,
                        item.DistributionProvider)

                    NewItem.Initialize()
                    NewItem.ReSample()
                    NewItem.RelativeJulianDay = item.RelativeJulianDay

                    If (NewItem.JulianDay.HasValue) Then
                        m.AddItem(Iteration, ts, NewItem.RelativeJulianDay, NewItem)
                    Else
                        m.AddItem(Iteration, ts, NewItem.JulianDay, NewItem)
                    End If

                Next

            End If

        Next

    End Sub

    Public Function GetItems(
        ByVal stratumId As Integer,
        ByVal sex As Sex,
        ByVal ageClassId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As SortedList(Of Integer, AnnualizedMortalityRate)

        Dim m As SortedKeyMap1(Of AnnualizedMortalityRate) =
            Me.GetFinalMap(stratumId, sex, ageClassId, iteration, timestep)

        If (m Is Nothing OrElse m.Map.Count = 0) Then
            Return Nothing
        End If

        Return m.Map.Items

    End Function

    Private Function GetFinalMap(
        ByVal stratumId As Integer,
        ByVal sex As Sex,
        ByVal ageClassId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As SortedKeyMap1(Of AnnualizedMortalityRate)

        Dim m1 As SortedKeyMap3(Of AnnualizedMortalityRate) =
            Me.m_Map.GetItem(stratumId, sex, ageClassId)

        If (m1 Is Nothing OrElse m1.Map.Count = 0) Then
            Return Nothing
        End If

        Dim m2 As SortedKeyMap2(Of AnnualizedMortalityRate) = m1.Map.GetItem(iteration)

        If (m2 Is Nothing OrElse m2.Map.Count = 0) Then
            Return Nothing
        End If

        Dim m3 As SortedKeyMap1(Of AnnualizedMortalityRate) = m2.Map.GetItem(timestep)

        If (m3 Is Nothing OrElse m3.Map.Count = 0) Then
            Return Nothing
        End If

        Return m3

    End Function

End Class
