Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Text

Partial Class Materials
    Inherits System.Web.UI.Page

    Private conn As New SqlConnection(ConfigurationManager.AppSettings("conn0"))

#Region "Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("kisino") = 0 Or Session("kisino") Is Nothing Then
            Response.Redirect("Login.aspx", True)
            Exit Sub
        End If
        Session("PageIs") = ""

        If Page.IsPostBack = False Then
            dosyalar_getir()
            fakulte_getir()
        End If
    End Sub
#End Region
#Region "Dosyalar"
    Private Sub dosyalar_getir()
        Try
            Dim command As New SqlCommand("stp_DOSYALAR", conn)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("SICIL_NO", CType(Session("kisino"), Long))   'Kurumsicil
            End With

            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)
            Dim dt As New DataTable()
            If conn.State <> ConnectionState.Open Then conn.Open()
            Adapter.Fill(dt)
            If conn.State = ConnectionState.Open Then conn.Close()

            o_grid_files.DataSource = dt
            o_grid_files.DataBind()
        Catch ex As Exception
            'lblUyari.Visible = True
            'lblUyari.Text = "Ýþlem sýrasýnda hata oluþtu. Hata kodu : 300101"
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub
#End Region
#Region "Fakülte, Bölüm ve ders getir"
    Protected Sub o_ddl_fak_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_ddl_fak.DataBound
        Dim litem As New ListItem("Lütfen Seçiniz", -1)
        o_ddl_fak.Items.Insert(0, litem)
    End Sub

    Protected Sub o_ddl_fak_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_ddl_fak.SelectedIndexChanged
        If o_ddl_fak.SelectedValue <> -1 Then
            bolum_getir()
            o_ddl_bol.Enabled = True
        End If
    End Sub

    Protected Sub o_ddl_bol_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_ddl_bol.DataBound
        Dim litem As New ListItem("Lütfen Seçiniz", -1)
        o_ddl_bol.Items.Insert(0, litem)
    End Sub

    Protected Sub o_ddl_bol_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_ddl_bol.SelectedIndexChanged
        ders_getir()
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
                .AddWithValue("FAKULTE_NO", o_ddl_fak.SelectedValue)
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

    Private Sub ders_getir()
        Try
            Dim dt As New DataTable
            Dim command As New SqlCommand("stp_DOSYA_BOLUM_DERSLER", conn)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("BIRIM", o_ddl_bol.SelectedValue)
            End With

            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)
            If conn.State <> ConnectionState.Open Then conn.Open()
            Adapter.Fill(dt)
            If conn.State = ConnectionState.Open Then conn.Close()

            If dt.Rows.Count > 0 Then
                o_grid_dersler.DataSource = dt
                o_grid_dersler.DataBind()
                td1_top.Visible = True
                td1_bottom.Visible = True
            Else
                o_grid_dersler.DataSource = Nothing
                o_grid_dersler.DataBind()
                td1_top.Visible = False
                td1_bottom.Visible = False
            End If

        Catch ex As Exception
            'lblUyari.Visible = True
            'lblUyari.Text = "Ýþlem sýrasýnda hata oluþtu. Hata kodu : 300106"
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub
#End Region
#Region "Ýliþkiler ve yayýn"
    Private Sub iliski_getir()
        Try
            Dim command As New SqlCommand("stp_DOSYA_ILISKI_GETIR", conn)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("DOSYA_ID", Session("dosyaID"))
            End With

            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)
            Dim dt As New DataTable()
            If conn.State <> ConnectionState.Open Then conn.Open()
            Adapter.Fill(dt)
            If conn.State = ConnectionState.Open Then conn.Close()

            If dt.Rows.Count > 0 Then
                o_grid_iliskiler.DataSource = dt
                o_grid_iliskiler.DataBind()
                td2_top.Visible = True
                td2_bottom.Visible = True
            Else
                o_grid_iliskiler.DataSource = Nothing
                o_grid_iliskiler.DataBind()
                td2_top.Visible = False
                td2_bottom.Visible = False
            End If
        Catch ex As Exception
            'lblUyari.Visible = True
            'lblUyari.Text = "Ýþlem sýrasýnda hata oluþtu. Hata kodu : 300102"
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Protected Sub imgb_addselection_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgb_addselection.Click
        Try
            Dim rowCount As Integer = o_grid_dersler.Rows.Count
            Dim derskodlar As New StringBuilder
            Dim dersadlar As New StringBuilder
            'Dim derskodlar As New Generic.List(Of String)
            'Dim birimler As New Generic.List(Of String)
            'Dim dersadlar As New Generic.List(Of String)

            For i As Integer = 0 To rowCount - 1
                If CType(o_grid_dersler.Rows(i).FindControl("o_chk_dosya"), CheckBox).Checked = True Then
                    If derskodlar.Length > 0 Then
                        derskodlar.Append(",")
                        dersadlar.Append(",")
                    End If
                    derskodlar.Append(DirectCast(o_grid_dersler.DataKeys(i)(0), String).Trim)
                    dersadlar.Append(DirectCast(o_grid_dersler.DataKeys(i)(1), String).Trim)
                End If
            Next

            Dim command As New SqlCommand("stp_DOSYA_COK_ILISKI_EKLE", conn)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("DOSYA_ID", Session("dosyaID"))
                .AddWithValue("DERSKODLAR", derskodlar.ToString)
                .AddWithValue("DERSADLAR", dersadlar.ToString)
                .AddWithValue("BIRIM", o_ddl_bol.SelectedValue)
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
            iliski_getir()
            dosyalar_getir()
        End Try
    End Sub

    Protected Sub imgb_remselection_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgb_remselection.Click
        Try
            Dim rowCount As Integer = o_grid_iliskiler.Rows.Count
            Dim derskodlar As New StringBuilder
            Dim birimler As New StringBuilder

            For i As Integer = 0 To rowCount - 1
                If CType(o_grid_iliskiler.Rows(i).FindControl("o_chk_dosya"), CheckBox).Checked = True Then
                    If derskodlar.Length > 0 Then
                        derskodlar.Append(",")
                        birimler.Append(",")
                    End If
                    derskodlar.Append(DirectCast(o_grid_iliskiler.DataKeys(i)(0), String).Trim)
                    birimler.Append(DirectCast(o_grid_iliskiler.DataKeys(i)(1), String).Trim)
                End If
            Next

            Dim command As New SqlCommand("stp_DOSYA_COK_ILISKI_SIL", conn)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("DOSYA_ID", Session("dosyaID"))
                .AddWithValue("DERSKODLAR", derskodlar.ToString)
                .AddWithValue("BIRIMLER", birimler.ToString)
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
            iliski_getir()
            dosyalar_getir()
        End Try
    End Sub
#End Region
#Region "Grid butonlarý"
    Protected Sub img_edit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim imgButton As ImageButton = CType(sender, ImageButton)
            Dim grow As GridViewRow = CType(imgButton.Parent.Parent, GridViewRow)
            Dim rowIndex As Integer = grow.RowIndex
            Session("dosyaID") = o_grid_files.DataKeys(rowIndex)(0)
            'Session("dosyaAD") = o_grid_files.DataKeys(rowIndex)(1)
            o_lbl_title2.Visible = True
            o_lbl_title2.Text = "Þu an seçili bulunan dosya adý: <font color=""#000000"">" & o_grid_files.DataKeys(rowIndex)(1) & "<font>"
        Catch ex As Exception

        Finally
            iliski_getir()
        End Try
    End Sub

    Protected Sub img_view_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim imgButton As ImageButton = CType(sender, ImageButton)
            Dim grow As GridViewRow = CType(imgButton.Parent.Parent, GridViewRow)
            Dim rowIndex As Integer = grow.RowIndex

            Dim command As New SqlCommand("stp_DOSYA_GOR", conn)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("DOSYA_ID", o_grid_files.DataKeys(rowIndex)(0))
            End With

            Dim reader As SqlDataReader
            If conn.State <> ConnectionState.Open Then conn.Open()
            reader = command.ExecuteReader

            If reader.HasRows = True Then
                reader.Read()
                If reader("SWF") = 4 Then
                    Session("dosyaadi") = "E:\dosyalar\UploadSWF\" _
                    & CType(reader("SICIL"), String).Trim & "\" _
                    & (CType(reader("AD"), String).Trim.Remove(CType(reader("AD"), String).Trim.LastIndexOf("."))) & ".SWF"
                Else
                    Dim _filePath As String = "E:\dosyalar\Upload\" & CType(reader("SICIL"), String).Trim & "\" & CType(reader("AD"), String).Trim
                    Dim _fileStream As New IO.FileStream(_filePath, IO.FileMode.Open)
                    Dim _bytes(_fileStream.Length) As Byte

                    _fileStream.Read(_bytes, 0, _fileStream.Length)
                    _fileStream.Close()
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("Content-disposition", "attachment; filename=" & CType(reader("AD"), String).Trim)
                    Response.BinaryWrite(_bytes)
                    Response.End()
                End If
            End If
                If conn.State = ConnectionState.Open Then conn.Close()
                'Response.Write(Session("dosyaadi"))
        Catch ex As Exception
            Session("dosyaadi") = ""
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
        'Server.Transfer("Viewswf.aspx")
        Response.Redirect("Viewswf.aspx")
    End Sub

    Protected Sub img_publish_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim imgButton As ImageButton = CType(sender, ImageButton)
            Dim grow As GridViewRow = CType(imgButton.Parent.Parent, GridViewRow)
            Dim rowIndex As Integer = grow.RowIndex

            Dim command As New SqlCommand("stp_DOSYA_TEK_YAYINLA", conn)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("DOSYA_ID", o_grid_files.DataKeys(rowIndex)(0))
            End With

            If conn.State <> ConnectionState.Open Then conn.Open()
            command.ExecuteNonQuery()
            If conn.State = ConnectionState.Open Then conn.Close()
        Catch ex As Exception

        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
            dosyalar_getir()
        End Try
    End Sub

    Protected Sub imgb_rempublish_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim rowCount As Integer = o_grid_files.Rows.Count
            Dim dosyaidler As New StringBuilder

            For i As Integer = 0 To rowCount - 1
                If CType(o_grid_files.Rows(i).FindControl("o_chk_dosya"), CheckBox).Checked = True Then
                    If dosyaidler.Length > 0 Then
                        dosyaidler.Append(",")
                    End If
                    dosyaidler.Append(o_grid_files.DataKeys(i)(0))
                End If
            Next

            Dim command As New SqlCommand("stp_DOSYA_COK_YAYIN_SIL", conn)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("DOSYA_IDLER", dosyaidler.ToString)
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
            dosyalar_getir()
        End Try
    End Sub
#End Region
#Region "Gridview Databound"
    Protected Sub o_grid_files_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_grid_files.DataBound
        Try
            Dim gridRowCount As Integer = o_grid_files.Rows.Count
            For i As Integer = 0 To gridRowCount - 1
                If o_grid_files.DataKeys(i)(2) = "Evet" Then
                    CType(o_grid_files.Rows(i).FindControl("img_filetype"), Image).ImageUrl = "images/document.png"
                    CType(o_grid_files.Rows(i).FindControl("img_filetype"), Image).AlternateText = "Görüntülenebilir döküman"
                Else
                    CType(o_grid_files.Rows(i).FindControl("img_filetype"), Image).ImageUrl = "images/file.png"
                    CType(o_grid_files.Rows(i).FindControl("img_filetype"), Image).AlternateText = "Ýndirilebilir dosya"
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
#End Region
End Class
