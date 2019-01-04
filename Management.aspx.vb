Imports System.Data
Imports System.Data.SqlClient

Partial Class Management
    Inherits System.Web.UI.Page

Private conn As New SqlConnection(ConfigurationManager.AppSettings("conn0"))

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("kisino") = 0 Or Session("kisino") Is Nothing Then
            Response.Redirect("Login.aspx", True)
            Exit Sub
        End If

        Session("PageIs") = ""

        If Not Session("type") = "Peradmin" Then
            Session("authorized") = "None"
            Session.Clear()
            Response.Redirect("Login.aspx")
            Exit Sub
        Else
            If Page.IsPostBack = False Then
                fakulteler_getir()
            End If
        End If
    End Sub
#Region "Fakülte , bölüm ve dosya getir"
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
        onay_bekleyen()
        onaylanan()
    End Sub

    Private Sub fakulteler_getir()
        Try
            Dim dt As New DataTable
            Dim command As New SqlCommand("stp_MATERYAL_YETKI", conn)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("SICILNO", CType(Session("kisino"), Long))
            End With

            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)
            If conn.State <> ConnectionState.Open Then conn.Open()
            Adapter.Fill(dt)
            If conn.State = ConnectionState.Open Then conn.Close()

            If dt.Rows.Count > 0 Then
                o_ddl_fak.DataSource = dt
                o_ddl_fak.DataValueField = "FAK"
                o_ddl_fak.DataTextField = "FAKULTE"
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
            End If

        Catch ex As Exception
            'lblUyari.Visible = True
            'lblUyari.Text = "Ýþlem sýrasýnda hata oluþtu. Hata kodu : 300106"
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub
#End Region
#Region "Grid Databound"
    Protected Sub o_grid_mats_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_grid_mats.DataBound
        Dim gridRowCount As Integer = o_grid_mats.Rows.Count
        If gridRowCount > 0 Then o_lbl_title.Visible = True
    End Sub

    Protected Sub o_grid_mats2_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_grid_mats2.DataBound
        Dim gridRowCount As Integer = o_grid_mats2.Rows.Count
        If gridRowCount > 0 Then o_lbl_title2.Visible = True
    End Sub
#End Region
#Region "Dosyalar"
    Private Sub onay_bekleyen()
        Try
            Dim command As New SqlCommand("stp_MATERYAL_ONAYBEKLEYEN", conn)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("BOLUM", o_ddl_bol.SelectedValue)
            End With

            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)
            Dim dt As New DataTable()
            If conn.State <> ConnectionState.Open Then conn.Open()
            Adapter.Fill(dt)
            If conn.State = ConnectionState.Open Then conn.Close()

            If dt.Rows.Count > 0 Then
                o_grid_mats.DataSource = dt
                o_grid_mats.DataBind()
            Else
                o_grid_mats.DataSource = Nothing
                o_grid_mats.DataBind()
            End If
        Catch ex As Exception
            'lblUyari.Visible = True
            'lblUyari.Text = "Ýþlem sýrasýnda hata oluþtu. Hata kodu : 300101"
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub onaylanan()
        Try
            Dim command As New SqlCommand("stp_MATERYAL_ONAYLANAN", conn)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("BOLUM", o_ddl_bol.SelectedValue)
            End With

            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)
            Dim dt As New DataTable()
            If conn.State <> ConnectionState.Open Then conn.Open()
            Adapter.Fill(dt)
            If conn.State = ConnectionState.Open Then conn.Close()

            If dt.Rows.Count > 0 Then
                o_grid_mats2.DataSource = dt
                o_grid_mats2.DataBind()
            Else
                o_grid_mats2.DataSource = Nothing
                o_grid_mats2.DataBind()
            End If
        Catch ex As Exception
            'lblUyari.Visible = True
            'lblUyari.Text = "Ýþlem sýrasýnda hata oluþtu. Hata kodu : 300101"
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub dosyayla_ilgilen()
        Try
            Dim command As New SqlCommand("stp_DOSYA_GOR", conn)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("DOSYA_ID", CType(Session("dosyaID"), Integer))
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
#End Region
#Region "Grid Butonlarý"
    Protected Sub img_view_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim imgButton As ImageButton = CType(sender, ImageButton)
            Dim grow As GridViewRow = CType(imgButton.Parent.Parent, GridViewRow)
            Dim rowIndex As Integer = grow.RowIndex
            Session("dosyaID") = o_grid_mats.DataKeys(rowIndex)(0)
            'Session("dosyaAD") = CType(o_grid_mats.DataKeys(rowIndex)(2), String).Trim
        Catch ex As Exception

        Finally
            dosyayla_ilgilen()
        End Try
    End Sub

    Protected Sub img_view2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim imgButton As ImageButton = CType(sender, ImageButton)
            Dim grow As GridViewRow = CType(imgButton.Parent.Parent, GridViewRow)
            Dim rowIndex As Integer = grow.RowIndex
            Session("dosyaID") = o_grid_mats2.DataKeys(rowIndex)(0)
            'Session("dosyaAD") = CType(o_grid_mats.DataKeys(rowIndex)(2), String).Trim
        Catch ex As Exception

        Finally
            dosyayla_ilgilen()
        End Try
    End Sub

    Protected Sub imgb_onayver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim rowCount As Integer = o_grid_mats.Rows.Count
            Dim dersidler As New StringBuilder

            For i As Integer = 0 To rowCount - 1
                If CType(o_grid_mats.Rows(i).FindControl("o_chk_dosya"), CheckBox).Checked = True Then
                    If dersidler.Length > 0 Then
                        dersidler.Append(",")
                    End If
                    dersidler.Append(o_grid_mats.DataKeys(i)(1))
                End If
            Next

            Dim command As New SqlCommand("stp_MATERYAL_COK_ONAYVER", conn)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("DERS_IDLER", dersidler.ToString)
            End With

            If conn.State <> ConnectionState.Open Then conn.Open()
            command.ExecuteNonQuery()
            If conn.State = ConnectionState.Open Then conn.Close()

        Catch ex As Exception

        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
            onay_bekleyen()
            onaylanan()
        End Try
    End Sub

    Protected Sub img_tekonayver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim imgButton As ImageButton = CType(sender, ImageButton)
            Dim grow As GridViewRow = CType(imgButton.Parent.Parent, GridViewRow)
            Dim rowIndex As Integer = grow.RowIndex

            Dim command As New SqlCommand("stp_MATERYAL_TEK_ONAYVER", conn)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("DERSID", o_grid_mats.DataKeys(rowIndex)(1))
            End With

            If conn.State <> ConnectionState.Open Then conn.Open()
            command.ExecuteNonQuery()
            If conn.State = ConnectionState.Open Then conn.Close()
        Catch ex As Exception

        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
            onay_bekleyen()
            onaylanan()
        End Try
    End Sub

    Protected Sub img_onaysil_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim rowCount As Integer = o_grid_mats2.Rows.Count
            Dim dersidler As New StringBuilder

            For i As Integer = 0 To rowCount - 1
                If CType(o_grid_mats2.Rows(i).FindControl("o_chk_dosya"), CheckBox).Checked = True Then
                    If dersidler.Length > 0 Then
                        dersidler.Append(",")
                    End If
                    dersidler.Append(o_grid_mats2.DataKeys(i)(1))
                End If
            Next

            Dim command As New SqlCommand("stp_MATERYAL_COK_ONAYSIL", conn)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("DERS_IDLER", dersidler.ToString)
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
            onay_bekleyen()
            onaylanan()
        End Try
    End Sub

    Protected Sub img_tekonaysil_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim imgButton As ImageButton = CType(sender, ImageButton)
            Dim grow As GridViewRow = CType(imgButton.Parent.Parent, GridViewRow)
            Dim rowIndex As Integer = grow.RowIndex

            Dim command As New SqlCommand("stp_MATERYAL_TEK_ONAYSIL", conn)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("DERSID", o_grid_mats2.DataKeys(rowIndex)(1))
            End With

            If conn.State <> ConnectionState.Open Then conn.Open()
            command.ExecuteNonQuery()
            If conn.State = ConnectionState.Open Then conn.Close()
        Catch ex As Exception

        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
            onay_bekleyen()
            onaylanan()
        End Try
    End Sub
#End Region
End Class
