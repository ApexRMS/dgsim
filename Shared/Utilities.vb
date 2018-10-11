'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographics of wildlife populations.
'
' Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
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

    Function ApplicationMessageBox(text As [String], buttons As MessageBoxButtons) As DialogResult
        Return MessageBox.Show(text, Application.ProductName, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, DirectCast(0, MessageBoxOptions))
    End Function

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

    Public Function FormatNullableInt(ByVal value As Nullable(Of Integer)) As String

        If (value.HasValue) Then
            Return CStr(value.Value)
        Else
            Return "NULL"
        End If
    End Function

    Public Function FormatNullableDouble(ByVal value As Nullable(Of Double)) As String

        If (value.HasValue) Then
            Return value.Value.ToString("N4", CultureInfo.InvariantCulture)
        Else
            Return "NULL"
        End If

    End Function

    Public Function FormatNullableSexAsString(ByVal value As Nullable(Of Sex)) As String

        Dim sx As String = "NULL"

        If (value.HasValue) Then

            If (value.Value = Sex.Female) Then
                sx = "Female"
            Else
                sx = "Male"
            End If

        End If

        Return sx

    End Function

    Public Function GetDatasheetValue(ByVal project As Project, ByVal datasheetName As String, ByVal value As Nullable(Of Integer)) As String

        If (value.HasValue) Then

            Dim ds As DataSheet = project.GetDataSheet(datasheetName)
            Return ds.ValidationTable.GetDisplayName(value.Value)

        Else
            Return "NULL"
        End If

    End Function

    Public Function GetCommonFormattedExceptionData(
        ByVal ex As Exception,
        ByVal dataSheet As DataSheet,
        ByVal stratumId As Nullable(Of Integer),
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal ageClassId As Nullable(Of Integer))

        Dim st As String = GetDatasheetValue(dataSheet.Project, STRATUM_DATASHEET_NAME, stratumId)
        Dim it As String = FormatNullableInt(iteration)
        Dim ts As String = FormatNullableInt(timestep)
        Dim ac As String = GetDatasheetValue(dataSheet.Project, AGE_CLASS_DATASHEET_NAME, ageClassId)

        Dim m As String = Nothing

        m = m & dataSheet.DisplayName & vbCrLf
        m = m & ex.Message & vbCrLf
        m = m & "Stratum: " & st & vbCrLf
        m = m & "Iteration: " & it & vbCrLf
        m = m & "Timestep: " & ts & vbCrLf
        m = m & "Age Class: " & ac

        Return m

    End Function

End Module
