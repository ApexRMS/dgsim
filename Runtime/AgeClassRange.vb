'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

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

