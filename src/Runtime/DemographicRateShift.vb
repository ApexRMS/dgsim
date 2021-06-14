'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'**********************************************************************************************

Class DemographicRateShift

    Private m_Iteration As Nullable(Of Integer)
    Private m_Timestep As Nullable(Of Integer)
    Private m_AgeClassId As Nullable(Of Integer)
    Private m_Fecundity As Double
    Private m_Mortality As Double

    Public Sub New(
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal ageClassId As Nullable(Of Integer),
        ByVal fecundity As Nullable(Of Double),
        ByVal mortality As Nullable(Of Double))

        Me.m_Iteration = iteration
        Me.m_Timestep = timestep
        Me.m_AgeClassId = ageClassId

        If (fecundity.HasValue) Then
            Me.m_Fecundity = fecundity.Value
        End If

        If (mortality.HasValue) Then
            Me.m_Mortality = mortality.Value
        End If

    End Sub

    Public ReadOnly Property Iteration As Nullable(Of Integer)
        Get
            Return Me.m_Iteration
        End Get
    End Property

    Public ReadOnly Property Timestep As Nullable(Of Integer)
        Get
            Return Me.m_Timestep
        End Get
    End Property

    Public ReadOnly Property AgeClassId As Nullable(Of Integer)
        Get
            Return Me.m_AgeClassId
        End Get
    End Property

    Public ReadOnly Property Fecundity As Double
        Get
            Return Me.m_Fecundity
        End Get
    End Property

    Public ReadOnly Property Mortality As Double
        Get
            Return Me.m_Mortality
        End Get
    End Property

End Class
