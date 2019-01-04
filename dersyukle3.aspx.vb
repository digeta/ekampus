
Partial Class dersyukle3
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("kisino") = 0 Then
            Server.Transfer("Login.aspx")
        End If
        'Set the appropriate ContentType.
        Try
            Response.ContentType = "Application"
            'Get the physical path to the file.
            Dim FilePath As String = MapPath("uyari.swf")
            If Session("dosyaadi") IsNot Nothing Then
                If Session("dosyaadi") <> "" Then
                    Dim f As New IO.FileInfo(MapPath("ederswm1.swf"))
                    If f.Exists Then FilePath = (MapPath("ederswm1.swf"))
                End If
            End If
            'Write the file directly to the HTTP output stream.

            Response.WriteFile(FilePath)
            Response.End()
        Catch ex As Exception

        End Try
    End Sub
End Class
