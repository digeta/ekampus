
Partial Class wmdata
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("kisino") = 0 Then
            Server.Transfer("Login.aspx")
        End If
        If Session("kisino") <> 0 Then Response.Write("&content1=" + Session("kisino").ToString + "&content2=" + Session("adsoyad").ToString + "&content3=1")
        If Session("kisino") <> 0 Then Response.Write("&content1=" + Session("kisino").ToString + "&content2=" + Session("adsoyad").ToString + "&content3=1")


    End Sub
End Class
