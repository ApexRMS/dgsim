'*********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Partial Class DGSimTransformer

    Private m_AnnualHarvestValueMap As New AnnualHarvestValueMap()
    Private m_AnnualizedMortalityRateMap As New AnnualizedMortalityRateMap()
    Private m_OffspringPerFemaleValueMap As New OffspringPerFemaleValueMap()
    Private m_DemographicRateShiftMap As New DemographicRateShiftMap()
    Private m_CensusDataMap As New CensusDataMap()

    Private Sub CreateCollectionMaps()

        Debug.Assert(Me.Project Is Me.ResultScenario.Project)

        Me.m_AnnualHarvestValueMap.Initialize(Me.m_AnnualHarvestValues, Me.m_RunControl)
        Me.m_AnnualizedMortalityRateMap.Initialize(Me.m_AnnualizedMortalityRates, Me.m_RunControl)
        Me.m_OffspringPerFemaleValueMap.Initialize(Me.m_OffspringPerFemaleValues, Me.m_RunControl)
        Me.m_DemographicRateShiftMap.Initialize(Me.m_DemographicRateShifts)
        Me.m_CensusDataMap.Initialize(Me.m_CensusData)

    End Sub

End Class

