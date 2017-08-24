'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Class SummaryOutputBirths

    Private m_StratumId As Integer
    Private m_MotherAgeClassId As Integer
    Private m_OffspringSex As Gender
    Private m_Births As Integer

    Public Sub New(
        ByVal stratumId As Integer,
        ByVal motherAgeClassId As Integer,
        ByVal offspringSex As Gender,
        ByVal births As Integer)

        Me.m_StratumId = stratumId
        Me.m_MotherAgeClassId = motherAgeClassId
        Me.m_OffspringSex = offspringSex
        Me.m_Births = births

    End Sub

    Public ReadOnly Property StratumId As Integer
        Get
            Return Me.m_StratumId
        End Get
    End Property

    Public ReadOnly Property MotherAgeClassId As Integer
        Get
            Return Me.m_MotherAgeClassId
        End Get
    End Property

    Public ReadOnly Property OffspringSex As Gender
        Get
            Return Me.m_OffspringSex
        End Get
    End Property

    Public Property Births As Integer
        Get
            Return Me.m_Births
        End Get
        Set(value As Integer)
            Me.m_Births = value
        End Set
    End Property

End Class
