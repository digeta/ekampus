Imports System.Data.SqlClient

Partial Class Main
    Inherits System.Web.UI.Page

    Private conn As New SqlConnection(ConfigurationManager.AppSettings("conn0"))

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("kisino") = 0 Or Session("kisino") Is Nothing Then
            Response.Redirect("Login.aspx", True)
            Exit Sub
        End If
        '#########################################################

        'Try
        If Session("type") = "Student" Then
            lblBilgi.Visible = False
        ElseIf Session("type") = "Personel" Then
            'lblBilgi.Visible = True
            'lblBilgi.Text = "<a href=""http://server.karaelmas.edu.tr/ogrenciliste/"">Sýnýf ve yoklama listesi dökümleri için buraya týklayýnýz</a>"
        End If
        Session("PageIs") = ""
        '    If Session("type") = "Personel" And Session("perbilgiID") = 0 Then Server.Transfer("Personal.aspx")
        '    Exit Sub
        'Catch ex As Exception
        '    lblUyari.Visible = True
        '    lblUyari.Text = "Ýþlem sýrasýnda hata oluþtu. Hata kodu : 500101"
        'Finally
        '    _conn.Close()
        'End Try

        Dim vlan_id() As String = CType(Session("ipno"), String).Split(".")
        ViewState("vlan") = vlan_id(1)
        Dim vlan_id2 As Integer = CType(vlan_id(2), Integer)

        If vlan_id(0) <> "10" Then
            Exit Sub
        Else
            Try
                Dim command As New SqlCommand("stp_PCTUR_GETIR", conn)
                command.CommandType = Data.CommandType.StoredProcedure
                command.Parameters.AddWithValue("IP", CType(Session("ipno"), String))
                Dim reader As SqlDataReader

                If conn.State <> Data.ConnectionState.Open Then conn.Open()
                reader = command.ExecuteReader

                Dim pctur As Integer
                Session("pctur") = ""
                While reader.Read
                    pctur = reader("PCTUR")
                    If pctur = 2 Then
                        Session("pctur") = "labpc"
                    End If
                End While
                reader.Close()

                Dim command2 As New SqlCommand("stp_OTURUM_DEGISTIR", conn)
                command2.CommandType = Data.CommandType.StoredProcedure
                command2.Parameters.AddWithValue("IP", CType(Session("ipno"), String))
                command2.Parameters.AddWithValue("KISI_NO", CType(Session("kisino"), Long))
                command2.ExecuteNonQuery()
            Catch ex As Exception
                'lblUyari.Visible = True
                'lblUyari.Text = "Ýþlem sýrasýnda hata oluþtu. Hata kodu : 500102"
            Finally
                If conn.State = Data.ConnectionState.Open Then conn.Close()
            End Try
        End If
    End Sub
End Class
'Sayfa hata kodu : 500 & Hatakodu
'101 - Load hatasý
'102 - PC tür getirme hatasý
