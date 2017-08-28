'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Class SummaryOutputHarvest

    Private m_StratumId As Integer
    Private m_Sex As Gender
    Private m_AgeClassId As Integer
    Private m_Harvest As Double

    Public Sub New(
        ByVal stratumId As Integer,
        ByVal sex As Gender,
        ByVal ageClassId As Integer,
        ByVal harvest As Double)

        Me.m_StratumId = stratumId
        Me.m_Sex = sex
        Me.m_AgeClassId = ageClassId
        Me.m_Harvest = harvest

    End Sub

    Public ReadOnly Property StratumId As Integer
        Get
            Return Me.m_StratumId
        End Get
    End Property

    Public ReadOnly Property Sex As Gender
        Get
            Return Me.m_Sex
        End Get
    End Property

    Public ReadOnly Property AgeClassId As Integer
        Get
            Return Me.m_AgeClassId
        End Get
    End Property

    Public Property Harvest As Double
        Get
            Return Me.m_Harvest
        End Get
        Set(value As Double)
            Me.m_Harvest = value
        End Set
    End Property

End Class
