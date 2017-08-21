'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core

Class AnnualHarvestValue
    Inherits ModelItemBase

    Private m_Gender As Nullable(Of Gender)

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
        ByVal gender As Nullable(Of Gender))

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

        Me.m_Gender = gender

    End Sub

    Public ReadOnly Property Gender As Nullable(Of Gender)
        Get
            Return Me.m_Gender
        End Get
    End Property

End Class
