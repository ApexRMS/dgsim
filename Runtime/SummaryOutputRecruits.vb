'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Class SummaryOutputRecruits

    Private m_StratumId As Integer
    Private m_MotherAgeClassId As Integer
    Private m_OffspringSex As Gender
    Private m_Recruits As Double

    Public Sub New(
        ByVal stratumId As Integer,
        ByVal motherAgeClassId As Integer,
        ByVal offspringSex As Gender,
        ByVal recruits As Double)

        Me.m_StratumId = stratumId
        Me.m_MotherAgeClassId = motherAgeClassId
        Me.m_OffspringSex = offspringSex
        Me.m_Recruits = recruits

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

    Public Property Recruits As Double
        Get
            Return Me.m_Recruits
        End Get
        Set(value As Double)
            Me.m_Recruits = value
        End Set
    End Property

End Class
