'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'**********************************************************************************************

Class AgeClassRange

    Private m_AgeClassId As Integer
    Private m_MaximumAge As Integer

    Public Sub New(ByVal ageClassId As Integer, ByVal maximumAge As Integer)

        Me.m_AgeClassId = ageClassId
        Me.m_MaximumAge = maximumAge

    End Sub

    Public ReadOnly Property AgeClassId As Integer
        Get
            Return Me.m_AgeClassId
        End Get
    End Property

    Public ReadOnly Property MaximumAge As Integer
        Get
            Return Me.m_MaximumAge
        End Get
    End Property

End Class

