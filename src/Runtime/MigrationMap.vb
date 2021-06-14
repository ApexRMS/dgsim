'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'**********************************************************************************************

Imports SyncroSim.Common

Class MigrationMap

    Private m_Map As New MultiLevelKeyMap3(Of SortedKeyMap2(Of List(Of Migration)))
    Private m_Lists As New List(Of List(Of Migration))

    Public Sub Initialize(ByVal items As MigrationCollection, ByVal runControl As RunControl)

        For Each item As Migration In items

            Dim m1 As SortedKeyMap2(Of List(Of Migration)) =
                Me.m_Map.GetItemExact(item.FromStratumId, item.AgeClassId, item.Sex)

            If (m1 Is Nothing) Then

                m1 = New SortedKeyMap2(Of List(Of Migration))(SearchMode.ExactPrevNext)
                Me.m_Map.AddItem(item.FromStratumId, item.AgeClassId, item.Sex, m1)

            End If

            Dim l As List(Of Migration) = m1.GetItemExact(item.Iteration, item.Timestep)

            If (l Is Nothing) Then

                l = New List(Of Migration)

                Me.m_Lists.Add(l)
                m1.AddItem(item.Iteration, item.Timestep, l)

            End If

            item.ReSample()
            l.Add(item)

        Next

    End Sub

    Public Function Normalize()

        Dim AtLeastOne As Boolean = False

        For Each MigList As List(Of Migration) In Me.m_Lists

            Dim Total As Double = 0

            For Each Mig As Migration In MigList
                Total += Mig.CurrentValue
            Next

            If (Total > 1.0) Then

                For Each Mig As Migration In MigList
                    Mig.SetCurrentValue(Mig.CurrentValue / Total)
                Next

                AtLeastOne = True

            End If

        Next

        Return AtLeastOne

    End Function

    Public Function GetMigrations(
        ByVal fromStratumId As Integer,
        ByVal ageClassId As Integer,
        ByVal sex As Sex,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As List(Of Migration)

        Dim m1 As SortedKeyMap2(Of List(Of Migration)) = Me.m_Map.GetItem(fromStratumId, ageClassId, sex)

        If (m1 Is Nothing OrElse m1.Map.Count = 0) Then
            Return Nothing
        End If

        Dim m2 As SortedKeyMap1(Of List(Of Migration)) = m1.Map.GetItem(iteration)

        If (m2 Is Nothing OrElse m2.Map.Count = 0) Then
            Return Nothing
        End If

        Dim l As List(Of Migration) = m2.Map.GetItem(timestep)

#If DEBUG Then

        If (l IsNot Nothing) Then
            Debug.Assert(l.Count > 0)
        End If
#End If

        Return l

    End Function

End Class
