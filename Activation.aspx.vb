Imports System.Security.Cryptography
Imports System.Data.SqlClient

Partial Class Activation
    Inherits System.Web.UI.Page

    Private conn As New SqlConnection(ConfigurationManager.AppSettings("conn0"))

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Redirect("Default.aspx")
        Exit Sub

        'Dim pkey As Integer = Request.QueryString("pkey")
        'Dim userid As Integer = Request.QueryString("uid")
        'If IsNumeric(pkey) = True And IsNumeric(userid) = True Then
        'If doActivation(pkey, userid) = 100 Then

        'End If
        'End If
    End Sub

    Private Function doActivation(ByVal pkey As Integer, ByVal userid As Integer) As Integer
        Try
            Dim command As New SqlCommand("stp_DO_ACTIVATION", conn)
            command.CommandType = Data.CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("PKEY", pkey)
                .AddWithValue("USERID", userid)
                .AddWithValue("REQUEST_IP", Session("ipno"))
            End With

            Dim returnValue As SqlParameter
            returnValue = New SqlParameter("RETURNVALUE", Data.SqlDbType.Int)
            returnValue.Direction = Data.ParameterDirection.ReturnValue
            command.Parameters.Add(returnValue)

            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            command.ExecuteNonQuery()
            If conn.State = Data.ConnectionState.Open Then conn.Close()

            If returnValue.Value = 100 Then
                Return 100
            Else
                Return 0
            End If

        Catch ex As SqlException
        Catch ex As Exception
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try
    End Function
End Class
