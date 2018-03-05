'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Class AgeClass

    Private m_Id As Integer

    Public Sub New(ByVal id As Integer)
        Me.m_Id = id
    End Sub

    Public ReadOnly Property Id As Integer
        Get
            Return Me.m_Id
        End Get
    End Property

End Class