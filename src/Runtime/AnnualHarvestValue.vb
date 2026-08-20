'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'**********************************************************************************************

Imports SyncroSim.Core


Class AnnualHarvestValue
    Inherits DistributionBase

    Private m_Project As Project
    Private m_StratumId As Nullable(Of Integer)
    Private m_Iteration As Nullable(Of Integer)
    Private m_Timestep As Nullable(Of Integer)
    Private m_AgeClassId As Nullable(Of Integer)
    Private m_Sex As Nullable(Of Sex)

    Public Sub New(
        ByVal project As Project,
        ByVal stratumId As Nullable(Of Integer),
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal ageClassId As Nullable(Of Integer),
        ByVal sex As Nullable(Of Sex),
        ByVal mean As Nullable(Of Double),
        ByVal distributionType As Nullable(Of Double),
        ByVal distributionSD As Nullable(Of Double),
        ByVal distributionMin As Nullable(Of Double),
        ByVal distributionMax As Nullable(Of Double),
        ByVal distributionProvider As DistributionProvider)

        MyBase.New(mean, distributionType, distributionSD, distributionMin, distributionMax, distributionProvider)

        Me.m_Project = project
        Me.m_StratumId = stratumId
        Me.m_Iteration = iteration
        Me.m_Timestep = timestep
        Me.m_AgeClassId = ageClassId
        Me.m_Sex = sex

    End Sub

    Public ReadOnly Property Project As Project
        Get
            Return m_Project
        End Get
    End Property

    Public ReadOnly Property StratumId As Integer?
        Get
            Return m_StratumId
        End Get
    End Property

    Public ReadOnly Property Iteration As Integer?
        Get
            Return m_Iteration
        End Get
    End Property

    Public ReadOnly Property Timestep As Integer?
        Get
            Return m_Timestep
        End Get
    End Property

    Public ReadOnly Property AgeClassId As Integer?
        Get
            Return m_AgeClassId
        End Get
    End Property

    Public ReadOnly Property Sex As Nullable(Of Sex)
        Get
            Return Me.m_Sex
        End Get
    End Property

    ''' <summary>
    ''' The number of non-null matching filters (AgeClassId, Sex) defined for this rule.
    ''' </summary>
    ''' <remarks>
    ''' Used to order rule application so more specific rules are applied after (and therefore take
    ''' precedence over) less specific / catch-all rules for the same cohort. StratumId is
    ''' deliberately excluded: AnnualHarvestValueMap.GetItems(stratumId, ...) resolves to either the
    ''' bucket of rows with an explicit matching StratumId or (only when none exist) the bucket of
    ''' rows with a null StratumId - never both - so every item returned by a single GetItems call
    ''' already shares the same StratumId-null-or-not status and it cannot affect relative ordering.
    ''' </remarks>
    Public ReadOnly Property Specificity As Integer
        Get
            Dim n As Integer = 0
            If (Me.m_AgeClassId.HasValue) Then n += 1
            If (Me.m_Sex.HasValue) Then n += 1
            Return n
        End Get
    End Property

End Class
