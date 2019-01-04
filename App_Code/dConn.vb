Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System

Public Class dConn
    Private _sqlConn As New SqlConnection
    Private _sqlComm As SqlCommand
    Public _sqlReader As SqlDataReader

    Public Sub New(ByVal _server As Integer)
        If _server = 0 Then_
            _sqlConn.ConnectionString = "Data Source=***;Initial Catalog=***;User Id=meine_sql;Password=***;"
            '_sqlConn.ConnectionString = "Data Source=***;Initial Catalog=***;User Id=meine_sql;Password=**;"
        ElseIf _server = 1 Then
            _sqlConn.ConnectionString = "Data Source=***;Initial Catalog=***;User Id=***;Password=***;"
        ElseIf _server = 2 Then
            _sqlConn.ConnectionString = "Data Source=***;Initial Catalog=***;User Id=***;Password=***;"
        ElseIf _server = 3 Then
            _sqlConn.ConnectionString = "Data Source=***;Initial Catalog=***;User Id=***;Password=***;"
        End If
    End Sub

    Public Sub SelectQuery(ByVal _sqlStr As String)
        Try
            If _sqlConn.State = Data.ConnectionState.Closed Then _sqlConn.Open()
            _sqlComm = New SqlCommand(_sqlStr, _sqlConn)
            _sqlReader = _sqlComm.ExecuteReader
        Catch _sqlExcp As SqlException
        End Try
    End Sub

    Public Sub InsertQuery(ByVal _sqlStr As String)
        Try
            If _sqlConn.State = Data.ConnectionState.Closed Then _sqlConn.Open()
            _sqlComm = New SqlCommand(_sqlStr, _sqlConn)
            _sqlComm.ExecuteNonQuery()
        Catch _sqlExcp As SqlException
        End Try
    End Sub

    Public Sub UpdateAll(ByVal _sqlStr As String)
        Try
            If _sqlConn.State = Data.ConnectionState.Closed Then _sqlConn.Open()
            _sqlComm = New SqlCommand(_sqlStr, _sqlConn)
            _sqlComm.ExecuteNonQuery()
        Catch _sqlExcp As SqlException
        End Try
    End Sub

    Public Sub Close()
        If _sqlReader IsNot Nothing Then
            _sqlReader.Close()
        End If
        If _sqlComm IsNot Nothing Then
            _sqlComm.Dispose()
        End If
        If _sqlConn IsNot Nothing Then
            If _sqlConn.State <> Data.ConnectionState.Closed Then _sqlConn.Close()
            _sqlConn.Dispose()
        End If
    End Sub

    Public ReadOnly Property ConnectionString() As String
        Get
            Return _sqlConn.ConnectionString
        End Get
    End Property
End Class
