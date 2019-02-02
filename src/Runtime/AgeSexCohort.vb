'*********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Class AgeSexCohort

    Private m_Age As Integer
    Private m_InitialAge As Integer
    Private m_Sex As Sex
    Private m_NumIndividuals As Double
    Private m_AnnualHarvest As Double

    Public Sub New(
        ByVal age As Integer,
        ByVal initialAge As Integer,
        ByVal sex As Sex,
        ByVal numIndividuals As Double)

        'Age can be -1 if cohort is for next years calves
        Debug.Assert(age >= -1)
        Debug.Assert(numIndividuals > 0)

        Me.m_Age = age
        Me.m_InitialAge = initialAge
        Me.m_Sex = sex
        Me.m_NumIndividuals = numIndividuals

    End Sub

    Public Property Age As Integer
        Get
            Return Me.m_Age
        End Get
        Set(value As Integer)
            Me.m_Age = value
        End Set
    End Property

    Public ReadOnly Property InitialAge As Integer
        Get
            Return Me.m_InitialAge
        End Get
    End Property

    Public ReadOnly Property Sex As Sex
        Get
            Return Me.m_Sex
        End Get
    End Property

    Public Property NumIndividuals As Double
        Get
            Return Me.m_NumIndividuals
        End Get
        Set(value As Double)
            Me.m_NumIndividuals = value
        End Set
    End Property

    Public Property AnnualHarvest As Double
        Get
            Return Me.m_AnnualHarvest
        End Get
        Set(value As Double)
            Me.m_AnnualHarvest = value
        End Set
    End Property

End Class
