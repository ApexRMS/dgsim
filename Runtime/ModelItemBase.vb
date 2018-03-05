'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports System.Globalization

Class ModelItemBase

    Private m_Project As Project
    Private m_StratumId As Nullable(Of Integer)
    Private m_Iteration As Nullable(Of Integer)
    Private m_Timestep As Nullable(Of Integer)
    Private m_AgeClassId As Nullable(Of Integer)
    Private m_DistributionMean As Double
    Private m_DistributionSD As Nullable(Of Double)
    Private m_DistributionMinimum As Nullable(Of Double)
    Private m_DistributionMaximum As Nullable(Of Double)
    Private m_RandomGenerator As RandomGenerator
    Private m_HasDistribution As Boolean
    Private m_MinWasNull As Boolean
    Private m_CurrentValue As Double

    Public Sub New(
        ByVal project As Project,
        ByVal stratumId As Nullable(Of Integer),
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal ageClassId As Nullable(Of Integer),
        ByVal distributionMean As Double,
        ByVal distributionSD As Nullable(Of Double),
        ByVal distributionMinimum As Nullable(Of Double),
        ByVal distributionMaximum As Nullable(Of Double),
        ByVal randomGenerator As RandomGenerator)

        Me.m_Project = project
        Me.m_StratumId = stratumId
        Me.m_Iteration = iteration
        Me.m_Timestep = timestep
        Me.m_AgeClassId = ageClassId
        Me.m_DistributionMean = distributionMean
        Me.m_DistributionSD = distributionSD
        Me.m_DistributionMinimum = DistributionMinimum
        Me.m_DistributionMaximum = DistributionMaximum
        Me.m_RandomGenerator = RandomGenerator
        Me.m_HasDistribution = (Me.m_DistributionSD.HasValue)

    End Sub

    Public Sub Initialize()

        If (Me.m_HasDistribution) Then
            Me.InitializeDistribution()
        Else
            Me.InitializeNoDistribution()
        End If

    End Sub

    Public ReadOnly Property Project As Project
        Get
            Return Me.m_Project
        End Get
    End Property

    Public ReadOnly Property StratumId As Nullable(Of Integer)
        Get
            Return Me.m_StratumId
        End Get
    End Property

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

    Public ReadOnly Property DistributionMean As Double
        Get
            Return Me.m_DistributionMean
        End Get
    End Property

    Public ReadOnly Property DistributionSD As Nullable(Of Double)
        Get
            Return Me.m_DistributionSD
        End Get
    End Property

    Public ReadOnly Property DistributionMinimum As Nullable(Of Double)
        Get
            Return Me.m_DistributionMinimum
        End Get
    End Property

    Public ReadOnly Property DistributionMaximum As Nullable(Of Double)
        Get
            Return Me.m_DistributionMaximum
        End Get
    End Property

    Public ReadOnly Property RandomGenerator As RandomGenerator
        Get
            Return Me.m_RandomGenerator
        End Get
    End Property

    Public ReadOnly Property CurrentValue As Double
        Get
            Return Me.m_CurrentValue
        End Get
    End Property

    Public Function ReSample() As Double

        If (Me.m_HasDistribution) Then

            Me.m_CurrentValue = Me.m_RandomGenerator.GetRandomBeta(
                Me.m_DistributionMean,
                Me.m_DistributionSD.Value,
                Me.m_DistributionMinimum.Value,
                Me.m_DistributionMaximum.Value)

        End If

        Return Me.m_CurrentValue

    End Function

    Private Sub InitializeDistribution()

        Debug.Assert(Me.m_HasDistribution)
        Debug.Assert(Me.m_DistributionSD.HasValue)
        Debug.Assert(Me.m_DistributionMaximum.HasValue)

        If (Not DistributionMinimum.HasValue) Then

            Me.m_MinWasNull = True
            Me.m_DistributionMinimum = 0.0

        End If

        If (Not Me.m_DistributionMaximum.HasValue) Then
            Me.ThrowException("Cannot get random Beta value.  A maximum value is required.  See below for details:")
        End If

        If (Not Me.m_RandomGenerator.CanGetRandomBeta(
            Me.m_DistributionMean,
            Me.m_DistributionSD.Value,
            Me.m_DistributionMinimum.Value,
            Me.m_DistributionMaximum.Value)) Then

            Me.ThrowException("Cannot get random Beta value.  The standard deviation is too large given the specified mean, min and max.  See below for details:")

        End If

    End Sub

    Private Sub InitializeNoDistribution()

        Debug.Assert(Not Me.m_HasDistribution)
        Me.m_CurrentValue = Me.m_DistributionMean

    End Sub

    Private Sub ThrowException(ByVal message As String)

        Dim st As String = "NULL"
        Dim it As String = "NULL"
        Dim ts As String = "NULL"
        Dim ac As String = "NULL"
        Dim sd As String = "NULL"
        Dim mn As String = "NULL"
        Dim mx As String = "NULL"

        If (Me.m_StratumId.HasValue) Then

            Dim ds As DataSheet = Me.m_Project.GetDataSheet(STRATUM_DATASHEET_NAME)
            st = ds.ValidationTable.GetDisplayName(StratumId.Value)

        End If

        If (Me.m_Iteration.HasValue) Then
            it = CStr(Me.m_Iteration.Value)
        End If

        If (Me.m_Timestep.HasValue) Then
            ts = CStr(Me.m_Timestep.Value)
        End If

        If (Me.m_AgeClassId.HasValue) Then

            Dim ds As DataSheet = Me.m_Project.GetDataSheet(AGE_CLASS_DATASHEET_NAME)
            ac = ds.ValidationTable.GetDisplayName(AgeClassId.Value)

        End If

        If (Me.m_DistributionSD.HasValue) Then
            sd = Me.m_DistributionSD.Value.ToString("N4", CultureInfo.InvariantCulture)
        End If

        If (Me.m_DistributionMinimum.HasValue And (Not Me.m_MinWasNull)) Then
            mn = Me.m_DistributionMinimum.Value.ToString("N4", CultureInfo.InvariantCulture)
        End If

        If (Me.m_DistributionMaximum.HasValue) Then
            mx = Me.m_DistributionMaximum.Value.ToString("N4", CultureInfo.InvariantCulture)
        End If

        Dim m As String = message & vbCrLf & vbCrLf
        m = m & "Stratum: " & st & vbCrLf
        m = m & "Iteration: " & it & vbCrLf
        m = m & "Timestep: " & ts & vbCrLf
        m = m & "Age Class: " & ac & vbCrLf
        m = m & "Mean: " & CStr(Me.m_DistributionMean) & vbCrLf
        m = m & "Standard Deviation: " & sd & vbCrLf
        m = m & "Distribution Minimum: " & mn & vbCrLf
        m = m & "Distribution Maximum: " & mx

        ThrowArgumentException(m)

    End Sub

End Class
