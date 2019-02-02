'*********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Class RunControl

    Private m_MinimumIteration As Integer
    Private m_MaximumIteration As Integer
    Private m_MinimumTimestep As Integer
    Private m_MaximumTimestep As Integer
    Private m_StartJulianDay As Integer

    Public Sub New(
        ByVal minimumIteration As Integer,
        ByVal maximumIteration As Integer,
        ByVal minimumTimestep As Integer,
        ByVal maximumTimestep As Integer,
        ByVal startJulianDay As Integer)

        Me.m_MinimumIteration = minimumIteration
        Me.m_MaximumIteration = maximumIteration
        Me.m_MinimumTimestep = minimumTimestep
        Me.m_MaximumTimestep = maximumTimestep
        Me.m_StartJulianDay = startJulianDay

    End Sub

    Public ReadOnly Property MinimumIteration As Integer
        Get
            Return Me.m_MinimumIteration
        End Get
    End Property

    Public ReadOnly Property MaximumIteration As Integer
        Get
            Return Me.m_MaximumIteration
        End Get
    End Property

    Public ReadOnly Property MinimumTimestep As Integer
        Get
            Return Me.m_MinimumTimestep
        End Get
    End Property

    Public ReadOnly Property MaximumTimestep As Integer
        Get
            Return Me.m_MaximumTimestep
        End Get
    End Property

    Public ReadOnly Property StartJulianDay As Integer
        Get
            Return Me.m_StartJulianDay
        End Get
    End Property

End Class
