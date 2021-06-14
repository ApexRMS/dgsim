'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'**********************************************************************************************

Class Stratum

    Private m_Id As Integer
    Private m_AgeSexCohorts As New AgeSexCohortCollection

    Public Sub New(ByVal id As Integer)
        Me.m_Id = id
    End Sub

    Public ReadOnly Property Id As Integer
        Get
            Return Me.m_Id
        End Get
    End Property

    Public ReadOnly Property AgeSexCohorts As AgeSexCohortCollection
        Get
            Return Me.m_AgeSexCohorts
        End Get
    End Property

End Class


