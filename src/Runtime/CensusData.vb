'*********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Class CensusData

    Private m_StratumId As Integer
    Private m_Timestep As Integer
    Private m_MinimumPopulation As Integer
    Private m_MaximumPopulation As Integer
    Private m_MinimumM2FRatio As Nullable(Of Double)
    Private m_MaximumM2FRatio As Nullable(Of Double)

    Public Sub New(
        ByVal stratumId As Integer,
        ByVal timestep As Integer,
        ByVal minimumPopulation As Integer,
        ByVal maximumPopulation As Integer,
        ByVal minimumM2FRatio As Nullable(Of Double),
        ByVal maximumM2FRatio As Nullable(Of Double))

        Me.m_StratumId = stratumId
        Me.m_Timestep = timestep
        Me.m_MinimumPopulation = minimumPopulation
        Me.m_MaximumPopulation = maximumPopulation
        Me.m_MinimumM2FRatio = minimumM2FRatio
        Me.m_MaximumM2FRatio = maximumM2FRatio

    End Sub

    Public ReadOnly Property StratumId As Integer
        Get
            Return Me.m_StratumId
        End Get
    End Property

    Public ReadOnly Property Timestep As Integer
        Get
            Return Me.m_Timestep
        End Get
    End Property

    Public ReadOnly Property MinimumPopulation As Integer
        Get
            Return Me.m_MinimumPopulation
        End Get
    End Property

    Public ReadOnly Property MaximumPopulation As Integer
        Get
            Return Me.m_MaximumPopulation
        End Get
    End Property

    Public ReadOnly Property MinimumM2FRatio As Nullable(Of Double)
        Get
            Return Me.m_MinimumM2FRatio
        End Get
    End Property

    Public ReadOnly Property MaximumM2FRatio As Nullable(Of Double)
        Get
            Return Me.m_MaximumM2FRatio
        End Get
    End Property

End Class

