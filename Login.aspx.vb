Imports System.IO
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.Xml

Partial Class _Default
    Inherits System.Web.UI.Page

    Private conn As New SqlConnection(ConfigurationManager.AppSettings("conn1"))
    Private loginErrorText As String
    Private dataErrorText As String
    Private bakimVarText As String
    Private Shared bakimVar As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("kisino") <> 0 Or Session("kisino") IsNot Nothing Then
            Response.Redirect("Default.aspx", True)
            Exit Sub
        End If

        Session("yetki") = "denied"
        If Page.IsPostBack = False Then
            duyuruOku()
            captcha()
        End If

        Try
            If (Today.Month >= 9 And Today.Month <= 12) Then
                Session("term") = "Güz Dönemi"
                Session("term2") = Today.Year & "/" & Today.Year + 1 'Doðrusu bu
                Session("yýl") = Today.Year + 1
            ElseIf Today.Month = 1 Or Today.Month = 2 Then
                Session("term") = "Güz Dönemi"
                Session("term2") = Today.Year - 1 & "/" & Today.Year
                Session("yýl") = Today.Year
            ElseIf (Today.Month < 9 And Today.Month >= 3) Then
                Session("term") = "Bahar Dönemi"
                Session("term2") = Today.Year - 1 & "/" & Today.Year
                Session("yýl") = Today.Year
            End If
        Catch ex As Exception
        End Try

        loginErrorText = "<script language=""JavaScript"">alert('Giriþ baþarýsýz.\n\nLütfen bilgilerinizi kontrol edip yeniden deneyiniz.');</script>"
        dataErrorText = "<script language=""JavaScript"">alert('Bir hata oluþtu.\n\nLütfen Bilgi Ýþlem ile iletiþime geçiniz.');</script>"
        bakimVarText = "<script language=""JavaScript"">alert('Þuan bakým çalýþmalarý yapýlmaktadýr.\n\nBu nedenle giriþ yapamamaktasýnýz.');</script>"
    End Sub

    Protected Sub imgbLogin_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbLogin.Click
        If txtCaptcha.Text = CType(Session("CaptchaImageText"), String) Then
            'Dim kullanici As String = txtKullanici.Text
            'Dim parola As String = txtParola.Text

            loginCheck()
        Else
            loginError()
        End If
    End Sub

#Region "Login Kontrol ve Yönlendirme"
    Private Sub loginCheck()
        Try
            Dim uid As String = txtKullanici.Text
            Dim pwd As String = txtParola.Text
            Dim login_type As String = ""
            Dim md5 As New mdV

            If Mid(uid, 1, 6) = "Master" Or Mid(uid, 1, 5) = "Admin" Then
                'If Not Regex.IsMatch(uid, "^[0-9a-zA-Z]+$") Or Not Regex.IsMatch(pwd, "^[0-9a-zA-Z]+$") Then
                If Not Regex.IsMatch(uid, "^[0-9a-zA-Z]+$") Then
                    loginError()
                    Exit Sub
                Else
                    If Mid(uid, 1, 6) = "Master" Then
                        uid = Mid(uid, 7, uid.Length - 6)
                        login_type = "JediMaster"
                    ElseIf Mid(uid, 1, 5) = "Admin" Then
                        uid = Mid(uid, 6, uid.Length - 5)
                        login_type = "Peradmin" 'Materyal onaylar
                    End If
                End If
            Else
                'If Not Regex.IsMatch(uid, "^[0-9]+$") Or Not Regex.IsMatch(pwd, "^[0-9]+$") Then
                If Not Regex.IsMatch(uid, "^[0-9]+$") Then
                    loginError()
                    Exit Sub
                Else
                    If uid.Length < 8 Then
                        login_type = "Personel"
                        pwd = md5.getMd5Hash(pwd)
                    ElseIf uid.Length = 13 Then
                        login_type = "Student"
                        pwd = md5.getMd5Hash(pwd)
                    ElseIf uid.Length = 10 Then
                        login_type = "Alien"
                    End If
                End If
            End If

            Login(uid, pwd, login_type)

        Catch ex As Exception
            dataError()
        End Try
    End Sub
#End Region
#Region "Login"
    Private Sub Login(ByVal kisino As Long, ByVal passwd As String, ByVal loginType As String)
        If bakimVar = True Then
            bakimVarMsj()
        Else
            Try
                Dim command As New SqlCommand("stp_LOGIN", conn)
                command.CommandType = Data.CommandType.StoredProcedure

                With command.Parameters
                    .AddWithValue("LOGINTYPE", loginType)
                    .AddWithValue("KISINO", kisino)
                    .AddWithValue("PASSWD", passwd)
                End With

                Dim reader As SqlDataReader
                If conn.State <> Data.ConnectionState.Open Then conn.Open()
                reader = command.ExecuteReader

                Dim isYetkili As Boolean = False

                If reader.HasRows = True Then
                    reader.Read()
                    Session("id") = reader("KISI_ID")
                    Session("adsoyad") = reader("AD") + " " + reader("SOYAD")
                    Session("kisino") = kisino
                    'Session("kisino") = 1234
                    Session("type") = loginType
                    Session("parola") = reader("PAROLA")
                    Session("parola_zorla") = reader("PAROLA_ZORLA")

                    If loginType = "Personel" Then
                        Session("perbilgiID") = reader("PERBILGI_ID")
                        Session("unvan") = reader("UNVAN")
                        Session("bolum") = reader("PERBOLUM")
                    End If

                    If loginType = "Student" Then
                        Session("fakulte") = Mid(reader("BIRIM"), 1, 2)
                        Session("bolum") = reader("BIRIM")
                    End If

                    If loginType = "Alien" Then
                        Session("birim") = Mid(kisino, 5, 3)
                    Else
                        Session("birim") = reader("BIRIM")
                    End If

                    Session("yetki_pc") = reader("Y_PCEKLE")
                    Session("yetki_hareketli") = reader("Y_HAREKETLI")
                    Session("yetki_sunucu") = reader("Y_SUNUCUEKLE")
                    Session("yetki_lab") = reader("Y_LABEKLE")
                    Session("yetki_misafir") = reader("Y_MISAFIREKLE")
                    Session("yetki_digercihaz") = reader("Y_DIGEREKLE")
                    Session("yetki_oturumac") = reader("Y_OTURUMACMA")

                    Session("yetki_pcsay") = IIf(Session("yetki_pc") = True, reader("Y_PCSAYISI"), 0)
                    Session("yetki_mobilpcsay") = IIf(Session("yetki_hareketli") = True, reader("Y_MOBILPCSAYISI"), 0)
                    Session("yetki_sunucusay") = IIf(Session("yetki_sunucu") = True, reader("Y_SUNUCUSAYISI"), 0)
                    Session("yetki_labpcsay") = IIf(Session("yetki_lab") = True, reader("Y_LABPCSAYISI"), 0)
                    Session("yetki_misafirpcsay") = IIf(Session("yetki_misafir") = True, reader("Y_MISAFIRPCSAYISI"), 0)
                    Session("yetki_digercihazsay") = IIf(Session("yetki_digercihaz") = True, reader("Y_DIGERSAYISI"), 0)

                    reader.Close()

                    Dim _ticket As New FormsAuthenticationTicket(1, Session("adsoyad"), Now, Now.AddMinutes(10), False, Session("type"), FormsAuthentication.FormsCookiePath)
                    Dim _cookie As New HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(_ticket))
                    _cookie.Expires = _ticket.Expiration
                    Response.Cookies.Add(_cookie)

                    Dim _returnUrl As String = Request.QueryString("ReturnUrl")
                    If _returnUrl Is Nothing Then _returnUrl = "Default.aspx"

                    Response.Redirect(_returnUrl)
                    'Response.Redirect("Default.aspx")
                    'Server.Transfer("Default.aspx")
                Else
                    loginError()
                End If

            Catch ex As Exception
                dataError()
            Finally
                If conn.State = Data.ConnectionState.Open Then conn.Close()
            End Try
        End If
    End Sub
#End Region
#Region "Duyuru ve Hata Mesajlarý"
    Public Function duyuruOku() As String
        Try
            Dim xmlDoc As New XmlDocument
            xmlDoc.Load(Server.MapPath("~/App_Data/ayarlar.xml"))

            Dim xmlRoot As XmlNode = xmlDoc.DocumentElement
            Dim nodeCount As Integer = xmlRoot.ChildNodes.Count

            Dim sb As New StringBuilder

            bakimVar = False
            divBakim.Visible = False

            For i As Integer = 0 To nodeCount - 1
                Dim node As XmlNode = xmlRoot.ChildNodes(i)
                If node.Name = "bakim" Or node.Name = "gelecekBakim" Then
                    If node.Attributes("durum").Value = True Then
                        divBakim.Visible = True
                        bakimVar = True
                        Session("BAKIMDURUM") = node.InnerXml
                    End If
                Else
                    If node.Attributes("gizli").Value <> True Then sb.Append(node.InnerXml)
                End If
            Next

            Return sb.ToString
        Catch ex As Exception
            Return "<div style=""text-align: center;""><span style=""color: #FF0000;"">Duyuru gösteriminde hata!</span></div>"
        End Try
    End Function

    Public Shared Function bakimVarmi() As Boolean
        If bakimVar = True Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub loginError()
        ClientScript.RegisterStartupScript(GetType(Page), "LoginFailed", loginErrorText)
        txtCaptcha.Text = ""
        captcha()
    End Sub

    Private Sub dataError()
        ClientScript.RegisterStartupScript(GetType(Page), "LoginFailed", dataErrorText)
        txtCaptcha.Text = ""
        captcha()
    End Sub

    Private Sub bakimVarMsj()
        ClientScript.RegisterStartupScript(GetType(Page), "LoginFailed", bakimVarText)
        txtCaptcha.Text = ""
        captcha()
    End Sub
#End Region
#Region "Captcha"
    Private Sub captcha()
        Try
            'Session("CaptchaImageText") = Mid(Path.GetRandomFileName, 1, 6)
            Dim sb As New StringBuilder
            Dim random As New Random
            For i As Integer = 0 To 5
                sb.Append(random.Next(0, 9))
            Next
            Session("CaptchaImageText") = sb.ToString
        Catch ex As Exception
        End Try
    End Sub
#End Region
End Class
