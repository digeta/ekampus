Imports System.Data.SqlClient

Partial Class Manage_users
    Inherits System.Web.UI.Page

  Private conn As New SqlConnection(ConfigurationManager.AppSettings("conn0"))
    Private returnValue As SqlParameter

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        dsGrid.ConnectionString = ConfigurationManager.AppSettings("conn0")
        dsDetails.ConnectionString = ConfigurationManager.AppSettings("conn0")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("kisino") = 2314 And Not Session("type") = "JediMaster" Then
            Session.Clear()
            Response.Redirect("Login.aspx")
            Exit Sub
        End If
        Session("PageIs") = ""
    End Sub

    Protected Sub Buton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim command As New SqlCommand("stp_IPMAC_EKLE", conn)
            command.CommandType = Data.CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("KISI_NO", CType(Session("kisino"), Long))   'Kurumsicil
            End With

            returnValue = New SqlParameter("RETURNVALUE", Data.SqlDbType.Int)
            returnValue.Direction = Data.ParameterDirection.ReturnValue
            command.Parameters.Add(returnValue)

            conn.Open()
            command.ExecuteNonQuery()

            lblUyari.Visible = True
            If returnValue.Value = 101 Then
                lblUyari.Text = "Seçilmiþ insana ayrý yetkilendirme kaydý yapýldý ama yetkilerin hepsi 0 deðiþtirin lütfen"
            Else
                lblUyari.Text = "Yetki veremedik"
            End If
        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = ex.Message
        Finally
            conn.Close()
        End Try
    End Sub
End Class
