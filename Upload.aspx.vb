Imports System.Data
Imports System.Data.SqlClient

Public Class Upload2
    Inherits System.Web.UI.Page

    Private conn As New SqlConnection(ConfigurationManager.AppSettings("conn0"))

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("kisino") = 0 And Session("kisino") = 0 Then
            Server.Transfer("Login.aspx")
        End If
        If Page.IsPostBack = False Then
            Session("yukleme_tamam") = False
            dosyalar_getir()
        End If
        'If Session("yukleme_tamam") Then
        'Panelflsh.Visible = False
        'Panelozet.Visible = True
        'Else
        'Panelflsh.Visible = True
        'Panelozet.Visible = True
        'End If
        Dim jscript As String
        jscript = "<script language=JavaScript> function UploadComplete(){" + String.Format("__doPostBack('{0}','');", LinkButton1.ClientID.Replace("_", "$"))
        jscript = jscript + "};</script>"
        ClientScript.RegisterClientScriptBlock(GetType(Page), "FileCompleteUpload", jscript)
    End Sub

    Protected Function GetFlashVars() As String
        Return "?" + Server.UrlEncode(Request.QueryString.ToString())
    End Function

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As EventArgs)
        Session("yukleme_tamam") = True
        dosyalar_getir()
        'Page_Load(Page, System.EventArgs.Empty)
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        HttpContext.Current.Response.Write(" ")
    End Sub

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

    Protected Sub imgb_del_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim imgButton As ImageButton = CType(sender, ImageButton)
            Dim grow As GridViewRow = CType(imgButton.Parent.Parent, GridViewRow)
            Dim rowIndex As Integer = grow.RowIndex

            Dim command As New SqlCommand("stp_DOSYA_SIL", conn)
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

    Protected Sub imgb_view_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
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
End Class
