'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographics of wildlife populations.
'
' Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.StochasticTime

Class InitialPopulationSize
    Inherits DistributionBase

    Public Sub New(
        ByVal mean As Nullable(Of Double),
        ByVal distributionType As Nullable(Of Double),
        ByVal distributionSD As Nullable(Of Double),
        ByVal distributionMin As Nullable(Of Double),
        ByVal distributionMax As Nullable(Of Double),
        ByVal distributionProvider As DistributionProvider)

        MyBase.New(mean, distributionType, distributionSD, distributionMin, distributionMax, distributionProvider)

    End Sub

End Class
