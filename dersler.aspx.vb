
Partial Class dersler
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("kisino") = 0 Then
            Server.Transfer("Login.aspx")
        End If
        Session("PageIs") = ""
        'Set the appropriate ContentType.
        Try
            Response.ContentType = "Application"
            'Get the physical path to the file.
            Dim FilePath As String = MapPath("uyari.swf")
            'Dim FilePath As String = (Session("dosyaadi"))
            'Dim FilePath2 As String = ("D:\calis\eylul\19-eylul\UploadSWF\2314\MLY393.swf")
            If Session("dosyaadi") IsNot Nothing Then
                If Session("dosyaadi") <> "" Then
                    Dim f As New IO.FileInfo(Session("dosyaadi"))
                    If f.Exists Then FilePath = (Session("dosyaadi"))
                End If
            End If
            'Write the file directly to the HTTP output stream.

            Response.WriteFile(FilePath)
            Response.End()
        Catch ex As Exception

        End Try
    End Sub
End Class
