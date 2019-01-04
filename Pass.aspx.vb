Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Net
Imports System.Net.Mail

Partial Class pass
    Inherits System.Web.UI.Page

    Private mailSent As Boolean
    Private md5 As New mdV

    'Private smtp As New SmtpClient("posta.karaelmas.edu.tr", 25)
    Private smtp As New SmtpClient("smtp.gmail.com", 587)
    'Private smtp As New SmtpClient("smtp.live.com", 587)
    Private conn As New SqlConnection(ConfigurationManager.AppSettings("conn1"))

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("kisino") = 0 Or Session("kisino") Is Nothing Then
            Response.Redirect("Login.aspx", True)
            Exit Sub
        End If

        If Session("parola_zorla") = True Then
            lblUyari.Visible = True
            lblUyari.Text = "Ýþlemlerinize devam etmek için parolanýzý deðiþtirmelisiniz."
        End If

        Session("PageIs") = ""
        'Server.Transfer("Default.aspx")
        'Exit Sub
        'AddOnPreRenderCompleteAsync(AddressOf myBeginEventHandler, AddressOf myEndEventHandler)

        'Dim task As PageAsyncTask = New PageAsyncTask(AddressOf MailSendBegin, MailSendEnd, MailSendTimeout, Nothing)
        ' RegisterAsyncTask(task)
        'If Session("kisino") = 0 Then
        '    Server.Transfer("Login.aspx")
        'End If

        If Not Page.IsPostBack Then
            'txtNewPwd.Attributes.Add("onkeyup", "javascript:dogrula('" + PasswordStrength1.ClientID + "','" + imgbUpdate.ClientID + "','" + lblUyari.ClientID + "');")
            'imgbUpdate.Attributes.Add("disabled", "'true'")
        End If
    End Sub

    'Private Function MailSendBegin(ByVal sender As Object, ByVal e As EventArgs, ByVal cb As AsyncCallback, ByVal state As Object) As IAsyncResult
    'End Function

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'lblMailStatus.Text = postala()
        postala()
    End Sub

    Private Function postala() As Integer
        Try
            Dim pkey As String = generatePkey()
            If setPkey(pkey, 1, "semih.temel@gmail.com", Session("ipno")) = 100 Then

                lblMailStatus.Text = ""
                'Dim kimden As MailAddress = New MailAddress("semih.temel@karaelmas.edu.tr", "E-Kampüs", Encoding.UTF8)
                Dim kimden As MailAddress = New MailAddress("semih.temel@gmail.com", "E-Kampüs", Encoding.UTF8)
                'Dim kimden As MailAddress = New MailAddress("stm103@hotmail.com", "E-Kampüs", Encoding.UTF8)
                Dim kime As MailAddress = New MailAddress("semih.temel@gmail.com")

                Dim msg As MailMessage = New MailMessage(kimden, kime)
                'msg.Body = "Þu aþaðýda gördüðünüz linke týklayarak iþleminizi tamamlayýn adamý hasta etmeyin " & _
                '"https://ekampus.karaelmas.edu.tr/activation.aspx?pkey=12v4fg3fgg854"
                Dim msgBodyHtml As String = "deneme maili , <br> <a href=http://10.1.16.9/ekamp_test/activation.aspx?pkey=" & pkey & "&uid= " & Session("id") & ">buraya týklayýn</a>"
                Dim msgBodyText As String = "Tarayýcýnýzýn Html desteði yoktur, aþaðýdaki aktivasyon kodunu..."
                'msg.Body = "deneme maili , <br> <a href=""https://ekampus.karaelmas.edu.tr/activation.aspx?pkey=12v4fg3fgg854"">buraya týklayýn</a>"

                msg.BodyEncoding = Encoding.UTF8
                msg.Subject = "E-Kampüs : Þifre deðiþim iþlemleri"
                msg.SubjectEncoding = Encoding.UTF8

                Dim plainView As AlternateView = AlternateView.CreateAlternateViewFromString(System.Text.RegularExpressions.Regex.Replace(msgBodyText, "<(.|\n)*?>", String.Empty), Nothing, "text/plain")
                Dim htmlView As AlternateView = AlternateView.CreateAlternateViewFromString(msgBodyHtml, Nothing, "text/html")

                msg.AlternateViews.Add(plainView)
                msg.AlternateViews.Add(htmlView)
                'msg.Headers.Add("Disposition-Notification-To", "semih.temel@karaelmas.edu.tr")
                msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess Or DeliveryNotificationOptions.OnFailure

                AddHandler smtp.SendCompleted, AddressOf SendCompleted



                'smtp.ClientCertificates.Add(New System.Security.Cryptography.X509Certificates.TrustStatus
                Dim token As String = "testmaili"
                smtp.UseDefaultCredentials = False
                smtp.EnableSsl = True
                'smtp.Credentials = New System.Net.NetworkCredential("stm103@hotmail.com", "less4pnr2@#")
                smtp.Credentials = New System.Net.NetworkCredential("semih.temel", "roch4pnr2@#")
                'smtp.Credentials = New System.Net.NetworkCredential("semih.temel@karaelmas.edu.tr", "gest4pnr2")

                'For i As Integer = 0 To msg.Headers.Count - 1
                '    lblMailStatus.Text = msg.Headers(i) & "< /br>"
                'Next

                smtp.SendAsync(msg, token)
                'smtp.Send(msg)
                'msg.To.Add("semih.temel@karaelmas.edu.tr")
                'msg.Subject = "Deneme"
                'msg.Body = "Ses birki"
                'msg.IsBodyHtml = False
                'smtp.DeliveryMethod = SmtpDeliveryMethod.Network
                'smtp.UseDefaultCredentials = False
                'smtp.EnableSsl = True
                'smtp.Timeout = 1
                'smtp.Credentials = New System.Net.NetworkCredential("semih.temel@karaelmas.edu.tr", "gest4pnr2")
                'smtp.Send(msg)
                'If msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess Then
                '    lblMailStatus.Text = "olda"
                'End If
            End If
        Catch ex As SmtpException
            lblMailStatus.Text = ex.Message
        End Try
    End Function

    Private Function generatePkey() As String
        Try
            Dim sb As New StringBuilder
            Dim random As New Random

            For i As Integer = 0 To 5
                sb.Append(random.Next(0, 9))
            Next

            Return sb.ToString
        Catch ex As Exception
        End Try
    End Function

    Private Function setPkey(ByVal pkey As String, ByVal userid As String, ByVal email As String, ByVal requestIP As String) As Integer
        Try
            Dim command As New SqlCommand("stp_AKTIVASYON_TALEP", conn)
            command.CommandType = Data.CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("PKEY", pkey)
                .AddWithValue("USERID", userid)
                .AddWithValue("EMAIL", email)
                .AddWithValue("REQUEST_IP", requestIP)
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

    Private Sub MailStatus(ByVal sender As Object, ByVal e As MailMessageEventArgs)

    End Sub

    Private Sub SendCompleted(ByVal sender As Object, ByVal e As AsyncCompletedEventArgs)
        Dim taskId As String = e.UserState

        If e.Cancelled = True Then
            lblMailStatus.Text = "Mail gönderimi iptal edildi."
        End If

        If e.Error IsNot Nothing Then
            lblMailStatus.Text = e.Error.Message
        Else
            lblMailStatus.Text = "Mail Gönderildi."
        End If
        mailSent = True
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        smtp.SendAsyncCancel()
    End Sub

    Protected Sub parolaDegis(ByVal newPwd As String)
        Try
            Dim command As New SqlCommand("stp_PAROLA_DEGIS", conn)
            command.CommandType = Data.CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("IP", Session("ipno"))
                .AddWithValue("LOGINTYPE", Session("type"))
                .AddWithValue("NEW_PASSWD", newPwd)
                .AddWithValue("USERNO", Session("kisino"))
            End With

            Dim returnValue As SqlParameter
            returnValue = New SqlParameter("RETURNVALUE", Data.SqlDbType.Int)
            returnValue.Direction = Data.ParameterDirection.ReturnValue
            command.Parameters.Add(returnValue)

            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            command.ExecuteNonQuery()
            If conn.State = Data.ConnectionState.Open Then conn.Close()

            If returnValue.Value = 100 Then
                lblUyari.Visible = True
                lblUyari.Text = "Parolanýz baþarýyla deðiþtirildi."
                Session("parola_zorla") = False
            Else
                lblUyari.Visible = True
                lblUyari.Text = "Parolanýz deðiþtirilemedi!"
            End If

        Catch ex As SqlException
            lblUyari.Visible = True
            lblUyari.Text = "Bir hata oluþtu.Sunucu baðlantýsý kurulamadý.Yeniden deneyiniz."
        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "Bir hata oluþtu.Bilgi Ýþlem' e baþvurunuz."
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Protected Sub imgbUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbUpdate.Click
        Try
            If txtNewPwd.Text IsNot String.Empty And txtNewPwd2.Text IsNot String.Empty Then
                If Session("STRIKE") < 4 Then
                    Dim oldPwd As String = md5.getMd5Hash(txtOldPwd.Text)

                    If Session("parola") = oldPwd Then
                        If txtNewPwd.Text <> txtNewPwd2.Text Then
                            lblUyari.Visible = True
                            lblUyari.Text = "Yeni parolalar birbiriyle uyuþmuyor."
                        Else
                            parolaDegis(md5.getMd5Hash(txtNewPwd.Text))
                        End If
                    Else
                        lblUyari.Visible = True
                        lblUyari.Text = "Eski parolanýz yanlýþ."
                        Session("STRIKE") += 1
                    End If
                Else
                    Response.Redirect("Logout.aspx")
                End If
            Else
                lblUyari.Visible = True
                lblUyari.Text = "Yeni parola alanlarýný boþ býraktýnýz."
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class
