'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographics of wildlife populations.
'
' Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core

Class AnnualHarvestValue
    Inherits ModelItemBase

    Private m_Sex As Nullable(Of Sex)

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
        ByVal sex As Nullable(Of Sex))

        MyBase.New(
            project,
            stratumId,
            iteration,
            timestep,
            ageClassId,
            distributionMean,
            distributionSD,
            distributionMinimum,
            distributionMaximum,
            randomGenerator)

        Me.m_Sex = sex

    End Sub

    Public ReadOnly Property Sex As Nullable(Of Sex)
        Get
            Return Me.m_Sex
        End Get
    End Property

End Class
