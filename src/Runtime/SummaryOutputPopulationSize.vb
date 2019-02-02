'*********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Class SummaryOutputPopulationSize

    Private m_StratumId As Integer
    Private m_Sex As Sex
    Private m_AgeClassId As Integer
    Private m_Population As Double

    Public Sub New(
        ByVal stratumId As Integer,
        ByVal sex As Sex,
        ByVal ageClassId As Integer,
        ByVal population As Double)

        Me.m_StratumId = stratumId
        Me.m_Sex = sex
        Me.m_AgeClassId = ageClassId
        Me.m_Population = population

    End Sub

    Public ReadOnly Property StratumId As Integer
        Get
            Return Me.m_StratumId
        End Get
    End Property

    Public ReadOnly Property Sex As Sex
        Get
            Return Me.m_Sex
        End Get
    End Property

    Public ReadOnly Property AgeClassId As Integer
        Get
            Return Me.m_AgeClassId
        End Get
    End Property

    Public Property Population As Double
        Get
            Return Me.m_Population
        End Get
        Set(value As Double)
            Me.m_Population = value
        End Set
    End Property

End Class
