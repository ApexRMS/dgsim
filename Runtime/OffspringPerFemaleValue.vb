'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographics of wildlife populations.
'
' Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core

Class OffspringPerFemaleValue
    Inherits ModelItemBase

    Private m_CountJulianDay As Nullable(Of Integer)
    Private m_RelativeJulianDay As Nullable(Of Integer)

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
        ByVal randomGenerator As RandomGenerator,
        ByVal countJulianDay As Nullable(Of Integer))

        MyBase.New(
            project,
            stratumId,
            iteration,
            timestep,
            ageClassId,
            distributionMean,
            DistributionSD,
            DistributionMinimum,
            DistributionMaximum,
            RandomGenerator)

        Me.m_CountJulianDay = CountJulianDay

    End Sub

    Public ReadOnly Property CountJulianDay As Nullable(Of Integer)
        Get
            Return Me.m_CountJulianDay
        End Get
    End Property

    Public Property RelativeJulianDay As Nullable(Of Integer)
        Get
            Return Me.m_RelativeJulianDay
        End Get
        Set(value As Nullable(Of Integer))
            Me.m_RelativeJulianDay = value
        End Set
    End Property

End Class



