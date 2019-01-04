Imports System.Data.SqlClient

Partial Class Auth
    Inherits System.Web.UI.Page

    Private conn As New SqlConnection(ConfigurationManager.AppSettings("conn0"))
    Private returnValue As SqlParameter

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '##############Kullanýcý giriþ kontrol####################
        If Session("kisino") = 0 Or Session("kisino") Is Nothing Then
            Response.Redirect("Login.aspx", True)
            Exit Sub
        End If

        If Session("pctur") <> "labpc" Then
            Response.Redirect("Default.aspx")
        End If
        Session("PageIs") = ""
        '#########################################################
        get_sessions()
        If Page.IsPostBack = False Then
            populate_dropdown()
        End If
    End Sub
#Region "Oturum açma"
    Protected Sub o_btn_logon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_btn_logon.Click
        Try
            Dim adsoyad() As String = DirectCast(Session("adsoyad"), String).Split(" ")
            Dim command As New SqlCommand("stp_OTURUM_EKLE", conn)
            command.CommandType = Data.CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("IP", CType(Session("ipno"), String))
                .AddWithValue("KISI_ID", CType(Session("id"), Integer))
                .AddWithValue("KISI_NO", CType(Session("kisino"), Long))
                .AddWithValue("AD", adsoyad(0))
                .AddWithValue("SOYAD", adsoyad(1))
                .AddWithValue("SURE", o_ddl_session.SelectedValue)
            End With

            returnValue = New SqlParameter("RETURNVALUE", Data.SqlDbType.Int)
            returnValue.Direction = Data.ParameterDirection.ReturnValue
            command.Parameters.Add(returnValue)

            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            command.ExecuteNonQuery()
            If conn.State = Data.ConnectionState.Open Then conn.Close()

            lblUyari.Visible = True
            If returnValue.Value = 20 Then
                lblUyari.Text = "Þuan önceki oturumu kapatma iþleminiz devam ediyor, kýsa bir süre sonra yeniden deneyin."
            Else
                get_sessions()
            End If

        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "Ýþlem sýrasýnda hata oluþtu. Hata kodu : 100101"
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try
    End Sub
#End Region
#Region "Oturumlar"
    Private Sub get_sessions()
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
        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "Ýþlem sýrasýnda hata oluþtu. Hata kodu : 100102"
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
            If Session("pctur") <> "labpc" Then field_session.Visible = False : o_lbl_oturum.Text = "Bulunduðunuz bilgisayarýn eriþimi yönetici tarafýndan engellenmiþtir."
        End Try

        Try
            Dim command As New SqlCommand("stp_OTURUM_GETIR", conn)
            command.CommandType = Data.CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("IP", CType(Session("ipno"), String))
            End With
            Dim reader As SqlDataReader
            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            reader = command.ExecuteReader

            o_lbl_oturum.Visible = True
            If reader.HasRows = True Then
                While reader.Read
                    Dim kisino As Long = reader("O_KISI_NO")
                    Dim lab As String = reader("O_LAB")
                    Dim pcno As Integer = reader("O_PCNO")
                    Dim adsoyad As String = reader("O_ADSOYAD")
                    Dim kalansure As Integer = reader("O_KALAN")
                    ViewState("kalansure") = kalansure
                    o_lbl_oturum.Text = "Þuan " & lab & " laboratuarýnda " & pcno & " numaralý bilgisayarda bulunuyorsunuz <br />" _
                    & " bu bilgisayar " & adsoyad & " adýna " & kalansure & " dakika süresince ayrýlmýþtýr.<br />" _
                    & " Oturumunuzu uzatmak veya sonlandýrmak için aþaðýdaki butonlarý kullanýnýz."
                    o_btn_logon.Text = "Oturum süresi deðiþtir"
                    o_btn_logoff.Visible = True
                End While
            Else
                o_lbl_oturum.Text = "Þuan bu bilgisayar internete çýkamaz, internete çýkmak için oturum açmanýz gerekmektedir."
                o_btn_logon.Text = "Oturum aç"
            End If
        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "Ýþlem sýrasýnda hata oluþtu. Hata kodu : 100103"
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try
    End Sub
#End Region
#Region "Drop down _ Oturum süreleri"
    Private Sub populate_dropdown()
        For i As Integer = 10 To 60 Step 10
            Dim litem As New ListItem(i & " dakika", i)
            o_ddl_session.Items.Add(litem)
        Next

        If Session("type") = "Personel" Then
            Dim litem As New ListItem("2 Saat", 120)
            Dim litem2 As New ListItem("5 Saat", 300)
            o_ddl_session.Items.Add(litem)
            o_ddl_session.Items.Add(litem2)
        End If
    End Sub
#End Region
#Region "Oturum sonlandýrma"
    Protected Sub o_btn_logoff_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_btn_logoff.Click
        Try
            Dim command As New SqlCommand("stp_OTURUM_SONLANDIR", conn)
            command.CommandType = Data.CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("IP", CType(Session("ipno"), String))
                .AddWithValue("KISI_NO", CType(Session("kisino"), Long))
            End With

            returnValue = New SqlParameter("RETURNVALUE", Data.SqlDbType.Int)
            returnValue.Direction = Data.ParameterDirection.ReturnValue
            command.Parameters.Add(returnValue)

            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            command.ExecuteNonQuery()
            If conn.State = Data.ConnectionState.Open Then conn.Close()

            lblUyari.Visible = True
            If returnValue.Value = 20 Then
                lblUyari.Text = "Þuan önceki oturumu açma iþleminiz devam ediyor, kýsa bir süre sonra yeniden deneyin."
            Else
                field_session.Visible = False
                o_lbl_oturum.Text = "Oturumunuz sonlandýrýlmýþtýr."
            End If

        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "Ýþlem sýrasýnda hata oluþtu. Hata kodu : 100104"
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try
    End Sub
#End Region
End Class
'Sayfa hata kodu : 100 & Hatakodu
'101 - Oturum açmada hata
'102 - PC türleri getirmede hata
'103 - Oturum durumu getirmede hata
'104 - Oturum sonlandýrmada hata