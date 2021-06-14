'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'**********************************************************************************************

Class MigrationEvent

    Private m_NumIndividuals As Integer
    Private m_ToStratum As Stratum

    Public Sub New(
        ByVal numIndividuals As Integer,
        ByVal toStratum As Stratum)

        Debug.Assert(numIndividuals > 0)

        Me.m_NumIndividuals = numIndividuals
        Me.m_ToStratum = toStratum

    End Sub

    Friend ReadOnly Property NumIndividuals As Integer
        Get
            Return Me.m_NumIndividuals
        End Get
    End Property

    Friend ReadOnly Property ToStratum As Stratum
        Get
            Return Me.m_ToStratum
        End Get
    End Property

End Class
