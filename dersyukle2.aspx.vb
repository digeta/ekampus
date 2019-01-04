
Partial Class dersyukle2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("kisino") = 0 Then
            Server.Transfer("Login.aspx")
        End If
        Session("PageIs") = ""

        Try
            Response.ContentType = "Application"
            Dim FilePath As String = MapPath("uyari.swf")
            If Session("dosyaadi") IsNot Nothing Then
                If Session("dosyaadi") <> "" Then
                    Dim f As New IO.FileInfo(Session("dosyaadi"))
                    If f.Exists Then FilePath = (Session("dosyaadi"))
                End If
            End If
            Response.WriteFile(FilePath)
            Response.End()
        Catch ex As Exception

        End Try
    End Sub
End Class
