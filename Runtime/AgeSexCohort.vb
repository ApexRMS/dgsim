'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Class AgeSexCohort

    Private m_Age As Integer
    Private m_InitialAge As Integer
    Private m_Sex As Gender
    Private m_NumIndividuals As Integer
    Private m_AnnualHarvest As Integer

    Public Sub New(
        ByVal age As Integer,
        ByVal initialAge As Integer,
        ByVal sex As Gender,
        ByVal numIndividuals As Integer)

        Debug.Assert(age >= 0)
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

    Public ReadOnly Property Sex As Gender
        Get
            Return Me.m_Sex
        End Get
    End Property

    Public Property NumIndividuals As Integer
        Get
            Return Me.m_NumIndividuals
        End Get
        Set(value As Integer)
            Me.m_NumIndividuals = value
        End Set
    End Property

    Public Property AnnualHarvest As Integer
        Get
            Return Me.m_AnnualHarvest
        End Get
        Set(value As Integer)
            Me.m_AnnualHarvest = value
        End Set
    End Property

End Class
