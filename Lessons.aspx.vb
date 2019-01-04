Imports System.Xml
Imports System.Data
Imports System.Data.SqlClient

Partial Class Lessons
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("kisino") = 0 Or Session("kisino") Is Nothing Then
            Response.Redirect("Login.aspx", True)
            Exit Sub
        End If
        'PopulateMenu()
        Try
            Session("PageIs") = "Lessons"
            'AddHandler menu.MenuItemClick, AddressOf onItemClick
        Catch _excp As Exception
        End Try
    End Sub
End Class
