
Partial Class Logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Clear()
        'Response.Redirect("Giris.aspx")
        Response.Redirect("Login.aspx", True)
        Exit Sub
    End Sub
End Class
