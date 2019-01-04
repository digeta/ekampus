Imports System.Data
Imports System.Data.SqlClient

Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Private _dosyaID As Integer
    Private conn As New SqlConnection(ConfigurationManager.AppSettings("conn0"))
    Private sslDurum As Boolean = ConfigurationManager.AppSettings("sslDurum")

#Region "Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If sslDurum = True Then
                If Not Me.Request.IsSecureConnection Then
                    Dim str As String = HttpUtility.UrlEncode(Me.Request.ServerVariables.Item("SERVER_NAME"))
                    Me.Response.Redirect(("https://" & str))
                End If
            End If

            Dim bannerNo As Integer = 0
            Response.Buffer = True
            Response.Expires = -1
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1D)
            Response.AddHeader("pragma", "no-cache")
            Response.AddHeader("cache-control", "private")
            Response.CacheControl = "no-cache"
            If Session("ipno") Is Nothing Then Session("ipno") = Trim(CType(Context.Request.ServerVariables("REMOTE_ADDR"), String))
            If Session("ipno") <> Trim(CType(Context.Request.ServerVariables("REMOTE_ADDR"), String)) Then Server.Transfer("Login.aspx")
            'Session("ipno") = "10.10.2.15"
            'SetCookie("key", "value")
            Page.Title = "E-Kampüs"

            If Session("adsoyad") <> "" Then o_lbl_greet.Text = Session("unvan") & " " & Session("adsoyad") & ", Hoþgeldiniz"

            If Not Page.IsPostBack And Session("kisino") > 0 Then
                o_div_menu.Visible = True
                populate_topmenu()

                If Session("type") = "Student" Then
                    If Session("PageIs") = "Lessons" Or Session("PageIs") = "ViewSwf" Then
                        dersmenu.Visible = True
                        PopulateMenu()
                    End If
                    'bannerNo = 1
                ElseIf Session("type") = "Personel" Then
                    If Session("perbilgiID") = 0 Then

                    End If
                    'bannerNo = 2
                ElseIf Session("type") = "Alien" Then
                    'bannerNo = 3
                ElseIf Session("yetki") <> "denied" Then
                    'bannerNo = 4
                ElseIf Session("type") = "JediMaster" Then
                    'bannerNo = 4
                End If
                'banner.ImageUrl = "~/images/bnnr" & bannerNo & ".jpg"
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region
#Region "Üst menü oluþturma"
    Private Sub populate_topmenu()
        Dim menuItems As New ArrayList
        Session("cihazislem") = False

        Dim vlan_id() As String = CType(Session("ipno"), String).Split(".")
        ViewState("vlan") = vlan_id(1)
        Dim vlan_id2 As Integer = CType(vlan_id(2), Integer)
        If vlan_id(0) = "10" Then
            topmenu.Width = 100%
        End If

        Try
            Dim command As New SqlCommand("stp_VLANS", conn)
            command.CommandType = Data.CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("VLAN_NO", CType(vlan_id(1), Integer))
            End With

            Dim reader As SqlDataReader
            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            reader = command.ExecuteReader

            If reader.HasRows = True Then
                reader.Read()
                Session("cihazislem") = reader("AKTIF")
            End If
            reader.Close()
        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = ex.Message
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try

        'If Session("type") = "Personel" Then
        '    Try
        '        Dim command As New SqlCommand("stp_MAT_ACCESS", conn)
        '        command.CommandType = Data.CommandType.StoredProcedure

        '        With command.Parameters
        '            .AddWithValue("BIRIM_NO", Session("bolum"))
        '        End With

        '        Dim reader As SqlDataReader
        '        If conn.State <> Data.ConnectionState.Open Then conn.Open()
        '        reader = command.ExecuteReader

        '        If reader.HasRows = True Then
        '            reader.Read()
        '            Session("materyalislem") = reader("AKTIF")
        '        End If
        '        reader.Close()
        '    Catch ex As Exception
        '        lblUyari.Visible = True
        '        lblUyari.Text = ex.Message
        '    Finally
        '        If conn.State = Data.ConnectionState.Open Then conn.Close()
        '    End Try
        'End If

        Try
            With menuItems
                If Session("type") = "Student" Then
                    .Add(New MenuItem("Ayarlar", "", "images/settings.png"))
                    .Add(New MenuItem("Ders materyalleri", "Lessons", "images/materials.png"))
                    .Add(New MenuItem("Notlar", "Notlar", "images/transcript.png"))
                    .Add(New MenuItem("Mesajlaþma", "Messages", "images/messages.png"))
                    '.Add(New MenuItem("Destek", "", "images/support.png"))
                    .Add(New MenuItem("Çýkýþ", "Exit", "images/exit.png"))
                End If

                If Session("type") = "Personel" Then
                    .Add(New MenuItem("Ayarlar", "", "images/settings.png"))
                    .Add(New MenuItem("Ders materyalleri", "Materials", "images/materials.png"))
                    .Add(New MenuItem("Dosya yükleme", "Upload", "images/upload.png"))
                    '.Add(New MenuItem("Mesajlaþma", "Messages", "images/messages.png"))
                    .Add(New MenuItem("Vize Not Giriþleri", "Derslerim", "images/notislem.png"))
                    '.Add(New MenuItem("Genel Sýnav Ýþlemleri", "Derslerim_f", "images/notislem.png"))
                    .Add(New MenuItem("Dökümler", "Rapor_snfliste", "images/document.png"))
                    '.Add(New MenuItem("Öðrenci notu görme", "Rapor_ogrliste", "images/notlist.png"))
                    '.Add(New MenuItem("Destek", "", "images/support.png"))
                    .Add(New MenuItem("Çýkýþ", "Exit", "images/exit.png"))
                End If

                If Session("type") = "Alien" Then
                    .Add(New MenuItem("Ayarlar", "", "images/settings.png"))
                    '.Add(New MenuItem("Mesajlaþma", "Messages", "images/messages.png"))
                    '.Add(New MenuItem("Destek", "", "images/support.png"))
                    .Add(New MenuItem("Çýkýþ", "Exit", "images/exit.png"))
                End If

                'Burada menüdeki item sayýlarýna dikkat!!!
                If Session("type") <> "JediMaster" And Session("type") <> "Peradmin" Then
                    'If vlan_id(0) = "10" And Session("cihazislem") = True Then
                    If Session("cihazislem") = True Then
                        .Insert(menuItems.Count - 1, New MenuItem("Cihaz tanýmlama", "Cihaz", "images/device.png"))
                        '.Insert(menuItems.Count - 1, New MenuItem("Cihaz iþlemleri", "Cihazlist", "images/devices.png"))
                    End If
                End If

                If Session("type") = "JediMaster" Then
                    .Add(New MenuItem("Sistem yönetimi", "Manage", "images/manage.png"))
                    .Add(New MenuItem("Kullanýcý yönetimi", "Manage_users", "images/users.png"))
                    .Add(New MenuItem("VLAN yönetimi", "Vlans", "images/vlans.png"))
                    .Add(New MenuItem("Çýkýþ", "Exit", "images/exit.png"))
                End If

                If Session("type") = "Peradmin" Then
                    .Add(New MenuItem("Materyal onaylama", "Management", "images/materials.png"))
                    '.Add(New MenuItem("Katalog", "Catalogue", "images/catalogue.png"))
                    .Add(New MenuItem("Çýkýþ", "Exit", "images/exit.png"))
                End If

                If Session("type") <> "JediMaster" And Session("type") <> "Peradmin" Then
                    If Session("pctur") = "labpc" And Session("yetki_oturumac") = True Then
                        .Insert(menuItems.Count - 1, New MenuItem("Ýnternet oturumu", "Authentication", "images/web.png"))
                    End If
                End If
            End With

            For i As Integer = 0 To menuItems.Count - 1
                topmenu.Items.Add(menuItems(i))
            Next

            If Session("type") <> "JediMaster" And Session("type") <> "Peradmin" Then
                Dim menuChildNames() As String = {0, "Kiþisel Bilgiler", 0, "Þifre deðiþtirme"}
                Dim menuChildValues() As String = {"", "Pass"}
                Dim menuChildImages() As String = {"images/personal.png", "images/pass.png"}

                Dim j As Integer = 1
                For i As Integer = 1 To menuChildNames.Length Step 2
                    Dim childItem As New MenuItem(menuChildNames(i), menuChildValues(i - j), menuChildImages(i - j))
                    topmenu.Items(menuChildNames(i - 1)).ChildItems.Add(childItem)
                    j += 1
                Next
            End If
        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "Üst menü yaratýlamadý"
        End Try
    End Sub
#End Region
#Region "Aðaç menü oluþturma"
    Protected Sub dersmenu_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dersmenu.SelectedNodeChanged
        If Mid(dersmenu.SelectedValue, 1, 4) = "DOS#" Then
            _dosyaID = Mid(dersmenu.SelectedValue, 5, dersmenu.SelectedValue.Length - 4)
            getThatFileName()
        End If
    End Sub

    Private Sub PopulateMenu()
        Try
            Dim ds As DataSet = GetDataSetForMenu()
            dersmenu.Nodes.Clear()

            For Each parentRow As DataRow In ds.Tables("DERSLER").Rows

                If Not CType(parentRow("derskod"), String).Trim = "" Then
                    Dim node As New TreeNode(CType(parentRow("derskod"), String) & " - " & CType(parentRow("dersadi"), String).Trim)

                    For Each childRow As DataRow In parentRow.GetChildRows("Children")
                        Dim childNode As New TreeNode(LCase(Mid(CType(childRow("DOSYA_ADI"), String), 1, 17)))
                        childNode.Value = "DOS#" & childRow("DOSYA_ID")

                        node.ChildNodes.Add(childNode)
                    Next
                    dersmenu.Nodes.Add(node)
                End If
            Next

            If Not Page.IsPostBack Then
                dersmenu.CollapseAll()
            End If
        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "Aðaç menü yaratýlamadý"
        End Try
        'Session("TreeMenu") = "Exist"
    End Sub

    Private Function GetDataSetForMenu() As DataSet
        Try
            Dim ds As New DataSet()
            'Try
            Dim command As New SqlCommand("stp_DERS_ALINAN", conn)
            command.CommandType = Data.CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("FAKNO", CType(Session("kisino"), Long))
            End With

            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)
            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            Adapter.Fill(ds, "DERSLER")
            If conn.State = Data.ConnectionState.Open Then conn.Close()

            'Catch ex As Exception
            '    lblUyari.Visible = True
            '    lblUyari.Text = "Ýþlem sýrasýnda hata oluþtu."
            'Finally
            '    If conn.State = Data.ConnectionState.Open Then conn.Close()
            'End Try
            'If Session("term") = "Güz Dönemi" Then
            '    _donem = "GUZ"
            'ElseIf Session("term") = "Bahar Dönemi" Then
            '    _donem = "BAH"
            'End If
            '_letter = LCase(Mid(_donem, 1, 1))

            '_sqlStr = "SELECT derskod, dersadi,(SELECT COUNT(proje1.dbo.DOSYALAR.ID) AS EXPR1" _
            '              & " FROM proje1.dbo.DOSYALAR INNER JOIN" _
            '              & " proje1.dbo.DERSLER ON proje1.dbo.DOSYALAR.ID = proje1.dbo.DERSLER.DOSYAID" _
            '              & " WHERE (proje1.dbo.DERSLER.DERSKOD = KARNE2.dbo." & "d" & Session("yýl") & _letter & ".derskod COLLATE Turkish_CS_AI) AND" _
            '              & " (proje1.dbo.DOSYALAR.YAYIN > 0) AND (proje1.dbo.DERSLER.BIRIM='" & Session("bolum") & "') and (proje1.dbo.DERSLER.ONAY > 0) AND" _
            '              & " (proje1.dbo.DOSYALAR.SIL = 0) AND (proje1.dbo.DOSYALAR.SWF = 3 OR proje1.dbo.DOSYALAR.SWF = 4) ) AS" _
            '              & " derssayi FROM KARNE2.dbo." & "d" & Session("yýl") & _letter & " WHERE" _
            '              & " danonay=1 AND fakno = '" & Session("kisino") & "' AND birim ='" & Session("bolum") & "' ORDER BY derskod"

            'Try
            Dim command2 As New SqlCommand("stp_DERS_MATERYAL", conn)
            command2.CommandType = Data.CommandType.StoredProcedure

            With command2.Parameters
                .AddWithValue("FAKNO", CType(Session("kisino"), Long))
            End With

            Dim Adapter2 As SqlDataAdapter = New SqlDataAdapter(command2)
            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            Adapter2.Fill(ds, "DOSYALAR")
            If conn.State = Data.ConnectionState.Open Then conn.Close()

            'Catch ex As Exception
            '    lblUyari.Visible = True
            '    lblUyari.Text = "Ýþlem sýrasýnda hata oluþtu."
            'Finally
            '    If conn.State = Data.ConnectionState.Open Then conn.Close()
            'End Try

            'Dim da, da2 As SqlDataAdapter
            'Dim ds As New DataSet()
            'da = New SqlDataAdapter(_sqlStr, _conn.ConnectionString)
            'da.Fill(ds, "Dersler")

            'da2 = New SqlDataAdapter(_sqlStr2, _conn.ConnectionString)
            'da2.Fill(ds, "Dosyalar")

            ds.Relations.Add("Children", ds.Tables("DERSLER").Columns("derskod"), ds.Tables("DOSYALAR").Columns("DERSKOD"), False)
            'da.Dispose()
            'da2.dispose()
            Return ds
            'ds.Dispose()
        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "Ýþlem sýrasýnda hata oluþtu."
            Return Nothing
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try
    End Function

    Private Sub getThatFileName()
        Try
            Dim command As New SqlCommand("SELECT SICIL,AD FROM DOSYALAR WHERE ID=" & CType(_dosyaID, String), conn)
            command.CommandType = Data.CommandType.Text

            'With command.Parameters
            '    .AddWithValue("LOGINTYPE", loginType)
            '    .AddWithValue("KISINO", kisino)
            '    .AddWithValue("PASSWD", passwd)
            'End With

            Dim reader As SqlDataReader
            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            reader = command.ExecuteReader

            If reader.HasRows = True Then
                While reader.Read()
                    Dim _ext As String = CType(reader("AD"), String).Trim.Remove(0, CType(reader("AD"), String).Trim.LastIndexOf("."))
                    If _ext.ToLower = ".zip" Or _ext.ToLower = ".rar" Then
                        Dim _filePath As String = "E:\dosyalar\Upload\" & CType(reader("SICIL"), String).Trim & "\" & CType(reader("AD"), String).Trim
                        Dim _fileStream As New IO.FileStream(_filePath, IO.FileMode.Open)
                        Dim _bytes(_fileStream.Length) As Byte

                        _fileStream.Read(_bytes, 0, _fileStream.Length)
                        _fileStream.Close()
                        Response.ContentType = "application/octet-stream"
                        Response.AddHeader("Content-disposition", "attachment; filename=" & CType(reader("AD"), String).Trim)
                        Response.BinaryWrite(_bytes)
                        Response.End()
                    Else
                        Session("dosyaadi") = "E:\dosyalar\UploadSWF\" & CType(reader("SICIL"), String).Trim & "\" & (CType(reader("AD"), String).Trim.Remove(CType(reader("AD"), String).Trim.LastIndexOf("."))) & ".SWF"
                        Response.Redirect("Viewswf.aspx")
                    End If
                End While
            End If
            reader.Close()
        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "Materyal bilgileri getirmede hata oluþtu."
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try
    End Sub
#End Region
#Region "Cookies"
    'SetCookie - key & value only
    Public Shared Sub SetCookie(ByVal key As String, ByVal value As String)
        Try
            'Encode Part
            key = HttpContext.Current.Server.UrlEncode(key)
            value = HttpContext.Current.Server.UrlEncode(value)

            Dim cookie As HttpCookie
            cookie = New HttpCookie(key, value)
            SetCookie(cookie)
        Catch ex As Exception
        End Try
    End Sub
    'SetCookie - overloaded with expires parameter
    Public Shared Sub SetCookie(ByVal key As String, ByVal value As String, ByVal expires As Date)
        Try
            'Encode Parts
            key = HttpContext.Current.Server.UrlEncode(key)
            value = HttpContext.Current.Server.UrlEncode(value)

            Dim cookie As HttpCookie
            cookie = New HttpCookie(key, value)
            cookie.Expires = expires
            SetCookie(cookie)
        Catch ex As Exception
        End Try
    End Sub

    'SetCookie - HttpCookie only
    'final step to set the cookie to the response clause
    Public Shared Sub SetCookie(ByVal cookie As HttpCookie)
        HttpContext.Current.Response.Cookies.Set(cookie)
    End Sub
#End Region

    Protected Sub topmenu_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles topmenu.MenuItemClick
        Try
            If Not e.Item.Value = "" Then
                If e.Item.Value = "Exit" Then
                    Session.Clear()
                    Response.Redirect("Default.aspx")
                    Exit Sub
                End If
                Response.Redirect(e.Item.Value & ".aspx")
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class