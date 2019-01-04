Imports System.Data
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Partial Class personal
    Inherits System.Web.UI.Page

    Private conn As New SqlConnection(ConfigurationManager.AppSettings("conn0"))

#Region "Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Redirect("Default.aspx")
        Exit Sub

        If Session("kisino") = 0 Or Session("kisino") Is Nothing Then
            Response.Redirect("Login.aspx", True)
            Exit Sub
        End If

        Session("PageIs") = ""
        If Not Page.IsPostBack Then
            fakulte_getir()
            bolum_getir()
            bilgi_getir()
            'If Session("type") = "Student" Then
            '        o_panel1.Visible = True
            '        o_panel2.Visible = False
            '        ogr_load()
            '        'CType(Master.FindControl("dersmenu"), TreeView).Visible = False
            '    ElseIf Session("type") = "Personel" Then
            '        o_panel1.Visible = False
            '        o_panel2.Visible = True
            '        per_load()
            '    End If
        End If
        If o_rdb_moff.Checked = True Then
            o_div_selection.Visible = False
        ElseIf o_rdb_mon.Checked = True Then
            o_div_selection.Visible = True
        End If
        o_lbl_adsoyad.Text = Session("adsoyad")
    End Sub
#End Region
#Region "Fakülte ve Bölüm getir"
    Protected Sub o_ddl_fak_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_ddl_fak.DataBound
        If Session("bolum") = 0 Then
            Dim litem As New ListItem("Lütfen Seçiniz", -1)
            o_ddl_fak.Items.Insert(0, litem)
            o_ddl_bol.Enabled = False
        End If
        o_ddl_fak.SelectedValue = Left(Session("bolum"), 2)
    End Sub

    Protected Sub o_ddl_fak_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_ddl_fak.SelectedIndexChanged
        If o_ddl_fak.SelectedValue <> -1 Then
            bolum_getir()
            o_ddl_bol.Enabled = True
        End If
    End Sub

    Protected Sub o_ddl_bol_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_ddl_bol.DataBound
        If Session("bolum") = 0 Then
            Dim litem As New ListItem("Lütfen Seçiniz", -1)
            o_ddl_bol.Items.Insert(0, litem)
        End If
        o_ddl_bol.SelectedValue = Session("bolum")
    End Sub

    Private Sub fakulte_getir()
        Try
            Dim dt As New DataTable
            Dim command As New SqlCommand("stp_FAKULTE_GETIR", conn)
            command.CommandType = CommandType.StoredProcedure

            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)
            If conn.State <> ConnectionState.Open Then conn.Open()
            Adapter.Fill(dt)
            If conn.State = ConnectionState.Open Then conn.Close()

            If dt.Rows.Count > 0 Then
                o_ddl_fak.DataSource = dt
                o_ddl_fak.DataValueField = "FAK"
                o_ddl_fak.DataTextField = "FAKULTE"
                o_ddl_fak.DataBind()
            Else
                o_ddl_fak.DataSource = Nothing
                o_ddl_fak.DataBind()
            End If

        Catch ex As Exception
            'lblUyari.Visible = True
            'lblUyari.Text = "Ýþlem sýrasýnda hata oluþtu. Hata kodu : 300106"
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub bolum_getir()
        Try
            Dim dt As New DataTable
            Dim command As New SqlCommand("stp_BOLUM_GETIR", conn)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("FAKULTE_NO", Left(Session("bolum"), 2))
            End With

            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)
            If conn.State <> ConnectionState.Open Then conn.Open()
            Adapter.Fill(dt)
            If conn.State = ConnectionState.Open Then conn.Close()

            If dt.Rows.Count > 0 Then
                o_ddl_bol.DataSource = dt
                o_ddl_bol.DataValueField = "BIRIM"
                o_ddl_bol.DataTextField = "BOLUM"
                o_ddl_bol.DataBind()
            Else
                o_ddl_bol.DataSource = Nothing
                o_ddl_bol.DataBind()
            End If

        Catch ex As Exception
            'lblUyari.Visible = True
            'lblUyari.Text = "Ýþlem sýrasýnda hata oluþtu. Hata kodu : 300106"
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub
#End Region
#Region "Bilgi getir ve güncelle"
    Protected Sub imgb_update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgb_update.Click
        Try
            Dim msgState As Integer = 1
            If o_rdb_moff.Checked = True Then msgState = 0
            If o_rdb_opt1.Checked = True Then msgState = 1
            If o_rdb_opt2.Checked = True Then msgState = 2
            If o_rdb_opt3.Checked = True Then msgState = 3
            If o_rdb_opt4.Checked = True Then msgState = 4
            If o_rdb_opt5.Checked = True Then msgState = 5

            'If Not Regex.IsMatch(o_txt_mail.Text, "^([a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4})", RegexOptions.IgnoreCase) = True Then
            '    o_lbl_warning.Visible = True
            '    o_lbl_warning.Text = "Yazdýðýnýz e-posta hatalý"
            '    Exit Sub
            'End If

            'If Not Regex.IsMatch(o_txt_tel.Text, "[0-9 ]+\-[0-9 ]+") = True Then
            '    o_lbl_warning.Visible = True
            '    o_lbl_warning.Text = "Yazdýðýnýz telefon numarasý hatalý"
            '    Exit Sub
            'End If

            Dim command As New SqlCommand("stp_BILGI_GUNCELLE", conn)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("KISI_ID", CType(Session("id"), Long))
                .AddWithValue("BOLUM", o_ddl_bol.SelectedValue)
                .AddWithValue("FAKULTE", o_ddl_fak.SelectedValue)
                .AddWithValue("EPOSTA", Regex.Match(o_txt_mail.Text, "^([a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4})", RegexOptions.IgnoreCase).Value)
                .AddWithValue("TEL", Regex.Match(o_txt_tel.Text, "[0-9 ]+\-[0-9 ]+").Value)
                .AddWithValue("MESAJALMA", msgState)
                .AddWithValue("LOGINTYPE", CType(Session("type"), String))
            End With

            'Dim returnValue As SqlParameter
            'returnValue = New SqlParameter("RETURNVALUE", SqlDbType.Int)
            'returnValue.Direction = ParameterDirection.ReturnValue
            'command.Parameters.Add(returnValue)

            If conn.State <> ConnectionState.Open Then conn.Open()
            command.ExecuteNonQuery()
            If conn.State = ConnectionState.Open Then conn.Close()

        Catch ex As Exception

        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub bilgi_getir()
        Try
            Dim command As New SqlCommand("stp_BILGI_GETIR", conn)
            command.CommandType = Data.CommandType.StoredProcedure
            Dim mesajalma As Integer = 1

            With command.Parameters
                .AddWithValue("KISI_ID", CType(Session("id"), Long))
                .AddWithValue("LOGINTYPE", CType(Session("type"), String))
            End With

            Dim reader As SqlDataReader
            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            reader = command.ExecuteReader

            If reader.HasRows = True Then
                reader.Read()
                'o_ddl_fak.SelectedValue = reader("FAKULTE")
                'o_ddl_bol.SelectedValue = reader("BOLUM")
                o_txt_tel.Text = reader("TEL")
                o_txt_mail.Text = reader("EPOSTA")
                mesajalma = reader("MESAJALMA")
            End If
            reader.Close()

            If mesajalma <> 0 Then
                o_rdb_mon.Checked = True
            End If
            DirectCast(o_div_selection.FindControl("o_rdb_opt" & mesajalma), CheckBox).Checked = True
        Catch ex As Exception

        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try
    End Sub
#End Region
End Class
