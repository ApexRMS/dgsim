'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Class InitialPopulationSize

    Private m_Mean As Integer
    Private m_Min As Integer
    Private m_Max As Integer
    Private m_SD As Double

    Public Sub New(
        ByVal mean As Integer,
        ByVal min As Integer,
        ByVal max As Integer,
        ByVal sd As Double)

        Me.m_Mean = mean
        Me.m_Min = min
        Me.m_Max = max
        Me.m_SD = sd

    End Sub

    Public ReadOnly Property Mean As Integer
        Get
            Return Me.m_Mean
        End Get
    End Property

    Public ReadOnly Property Min As Integer
        Get
            Return Me.m_Min
        End Get
    End Property

    Public ReadOnly Property Max As Integer
        Get
            Return Me.m_Max
        End Get
    End Property

    Public ReadOnly Property SD As Double
        Get
            Return Me.m_SD
        End Get
    End Property

End Class
