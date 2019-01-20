'*********************************************************************************************
' DG-Sim: A SyncroSim Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.StochasticTime

Class DistributionBase

    Private m_Mean As Nullable(Of Double)
    Private m_DistributionType As Nullable(Of Integer)
    Private m_DistributionSD As Nullable(Of Double)
    Private m_DistributionMin As Nullable(Of Double)
    Private m_DistributionMax As Nullable(Of Double)
    Private m_CurrentValue As Nullable(Of Double)
    Private m_DistributionProvider As DistributionProvider

    Public ReadOnly Property Mean As Double?
        Get
            Return m_Mean
        End Get
    End Property

    Public ReadOnly Property DistributionType As Integer?
        Get
            Return m_DistributionType
        End Get
    End Property

    Public ReadOnly Property DistributionSD As Double?
        Get
            Return m_DistributionSD
        End Get
    End Property

    Public ReadOnly Property DistributionMin As Double?
        Get
            Return m_DistributionMin
        End Get
    End Property

    Public ReadOnly Property DistributionMax As Double?
        Get
            Return m_DistributionMax
        End Get
    End Property

    Public ReadOnly Property DistributionProvider As DistributionProvider
        Get
            Return m_DistributionProvider
        End Get
    End Property

    Public ReadOnly Property CurrentValue As Double?
        Get
            Return m_CurrentValue
        End Get
    End Property

    Public Sub New(
        ByVal mean As Nullable(Of Double),
        ByVal distributionType As Nullable(Of Double),
        ByVal distributionSD As Nullable(Of Double),
        ByVal distributionMin As Nullable(Of Double),
        ByVal distributionMax As Nullable(Of Double),
        ByVal distributionProvider As DistributionProvider)

        Me.m_Mean = mean
        Me.m_DistributionType = distributionType
        Me.m_DistributionSD = distributionSD
        Me.m_DistributionMin = distributionMin
        Me.m_DistributionMax = distributionMax
        Me.m_DistributionProvider = distributionProvider

    End Sub

    Public Sub Initialize()

        Me.m_DistributionProvider.Validate(Me.m_DistributionType, Me.m_Mean, Me.m_DistributionSD, Me.m_DistributionMin, Me.m_DistributionMax)

        If (Me.m_DistributionType.HasValue) Then
            Me.m_CurrentValue = Me.m_DistributionProvider.Sample(Me.m_DistributionType, Me.Mean, Me.m_DistributionSD, Me.m_DistributionMin, Me.m_DistributionMax, -1, -1)
        Else
            Me.m_CurrentValue = Me.Mean
        End If

    End Sub

    Public Function ReSample() As Double

        If (Me.m_DistributionType.HasValue) Then
            Me.m_CurrentValue = Me.m_DistributionProvider.Sample(Me.m_DistributionType, Me.Mean, Me.m_DistributionSD, Me.m_DistributionMin, Me.m_DistributionMax, -1, -1)
        End If

        Debug.Assert(Me.m_CurrentValue.HasValue)
        Return Me.m_CurrentValue

    End Function

End Class
