Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Namespace Captcha
    Partial Public Class JpegImage
        Inherits System.Web.UI.Page

        Private ci As CaptchaImage

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                ' Create a CAPTCHA image using the text stored in the Session object. 
                ci = New CaptchaImage(Session("CaptchaImageText").ToString(), 161, 40, "Arial")

                ' Change the response headers to output a JPEG image. 
                Response.Clear()
                Response.ContentType = "image/jpeg"

                ' Write the image to the response stream in JPEG format. 
                ci.Image.Save(Response.OutputStream, ImageFormat.Jpeg)

                ' Dispose of the CAPTCHA image object. 
                ci.Dispose()
            Catch ex As Exception
            End Try
        End Sub

#Region "Web Form Designer generated code"
        Protected Overloads Overrides Sub OnInit(ByVal e As EventArgs)
            ' 
            ' CODEGEN: This call is required by the ASP.NET Web Form Designer. 
            ' 
            InitializeComponent()
            MyBase.OnInit(e)
        End Sub

        ''' <summary> 
        ''' Required method for Designer support - do not modify 
        ''' the contents of this method with the code editor. 
        ''' </summary> 
        Private Sub InitializeComponent()
            AddHandler Me.Load, AddressOf Page_Load
        End Sub
#End Region

    End Class
End Namespace