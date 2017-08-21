'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports System.Globalization
Imports System.Windows.Forms

Module Utilities

    Public Sub ThrowArgumentException(message As [String])
        ThrowArgumentException(message, New Object() {})
    End Sub

    Public Sub ThrowArgumentException(message As [String], ParamArray args As Object())
        Throw New ArgumentException(String.Format(CultureInfo.InvariantCulture, message, args))
    End Sub

    Function ApplicationMessageBox(text As [String], buttons As MessageBoxButtons, ParamArray args As Object()) As DialogResult
        Return ApplicationMessageBox([String].Format(CultureInfo.InvariantCulture, text, args), buttons)
    End Function

    Function InformationMessageBox(text As [String], ParamArray args As Object()) As DialogResult
        Return ApplicationMessageBox(text, MessageBoxButtons.OK, args)
    End Function

    Public Function GetNullableDatabaseValue(value As Nullable(Of Integer)) As Object

        If value.HasValue Then
            Return value.Value
        Else
            Return DBNull.Value
        End If

    End Function

    Public Function GetNullableInt(dr As DataRow, columnName As String) As Nullable(Of Integer)

        Dim value As Object = dr(columnName)

        If Object.ReferenceEquals(value, DBNull.Value) OrElse Object.ReferenceEquals(value, Nothing) Then
            Return Nothing
        Else
            Return Convert.ToInt32(value, CultureInfo.InvariantCulture)
        End If

    End Function

    Public Function GetNullableDouble(dr As DataRow, columnName As String) As Nullable(Of Double)

        Dim value As Object = dr(columnName)

        If Object.ReferenceEquals(value, DBNull.Value) OrElse Object.ReferenceEquals(value, Nothing) Then
            Return Nothing
        Else
            Return Convert.ToDouble(value, CultureInfo.InvariantCulture)
        End If

    End Function

    Public Function GetRelativeJulianDay(ByVal julianDay As Integer, ByVal startDay As Integer) As Integer

        Dim d As Integer = julianDay - startDay

        If (d < 0) Then
            d += 365
        End If

        Return d

    End Function

    Public Function DGSimGetRandomBeta(
        ByVal distributionMean As Double,
        ByVal distributionSD As Nullable(Of Double),
        ByVal distributionMinimum As Nullable(Of Double),
        ByVal distributionMaximum As Nullable(Of Double),
        ByVal randomGen As RandomGenerator,
        ByVal caller As String) As Double

        Try
            Return DGSimGetRandomBeta(distributionMean, distributionSD, distributionMinimum, distributionMaximum, randomGen)
        Catch ex As ArgumentException

            Dim m As String = String.Format(CultureInfo.CurrentCulture, "{0} -> {1}", caller, ex.Message)
            Throw New ArgumentException(m)

        End Try

    End Function

    Private Function DGSimGetRandomBeta(
        ByVal distributionMean As Double,
        ByVal distributionSD As Nullable(Of Double),
        ByVal distributionMinimum As Nullable(Of Double),
        ByVal distributionMaximum As Nullable(Of Double),
        ByVal randomGen As RandomGenerator) As Double

        If (Not distributionMaximum.HasValue Or
            Not distributionSD.HasValue) Then

            Return distributionMean

        End If

        Dim MinWasNull As Boolean = (Not distributionMinimum.HasValue)

        If (Not distributionMinimum.HasValue) Then
            distributionMinimum = 0.0
        End If

        Debug.Assert(distributionSD.Value > 0.0)
        Debug.Assert(distributionMinimum.Value <= distributionMaximum.Value)

        If (Not randomGen.CanGetRandomBeta(
            distributionMean,
            distributionSD.Value,
            distributionMinimum.Value,
            distributionMaximum.Value)) Then

            Dim min As String = "NULL"

            If (distributionMinimum.HasValue And (Not MinWasNull)) Then
                min = distributionMinimum.Value.ToString("N4", CultureInfo.CurrentCulture)
            End If

            Dim m As String = String.Format(CultureInfo.InvariantCulture,
                "Cannot get random Beta value.  The standard deviation is too large given the specified mean, min and max: Mean {0:N4}, SD {1:N4}, Min {2:N4}, Max {3:N4}.",
                distributionMean, distributionSD.Value, min, distributionMaximum.Value)

            ThrowArgumentException(m)

        End If

        Return randomGen.GetRandomBeta(
            distributionMean,
            distributionSD.Value,
            distributionMinimum.Value,
            distributionMaximum.Value)

    End Function

End Module
