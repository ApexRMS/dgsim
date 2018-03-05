'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Class InitialPopulationDistribution

    Private m_StratumId As Nullable(Of Integer)
    Private m_Sex As Nullable(Of Gender)
    Private m_AgeMin As Integer
    Private m_AgeMax As Integer
    Private m_RelativeAmount As Double

    Public Sub New(
        ByVal stratumId As Nullable(Of Integer),
        ByVal sex As Nullable(Of Gender),
        ByVal ageMin As Integer,
        ByVal ageMax As Integer,
        ByVal relativeAmount As Double)

        Me.m_StratumId = stratumId
        Me.m_Sex = sex
        Me.m_AgeMin = ageMin
        Me.m_AgeMax = ageMax
        Me.m_RelativeAmount = relativeAmount

    End Sub

    Public ReadOnly Property StratumId As Nullable(Of Integer)
        Get
            Return Me.m_StratumId
        End Get
    End Property

    Public ReadOnly Property Sex As Nullable(Of Gender)
        Get
            Return Me.m_Sex
        End Get
    End Property

    Public ReadOnly Property AgeMin As Integer
        Get
            Return Me.m_AgeMin
        End Get
    End Property

    Public ReadOnly Property AgeMax As Integer
        Get
            Return Me.m_AgeMax
        End Get
    End Property

    Public ReadOnly Property RelativeAmount As Double
        Get
            Return Me.m_RelativeAmount
        End Get
    End Property

End Class


