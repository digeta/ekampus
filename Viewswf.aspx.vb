Imports System.Data
Imports System.Data.SqlClient

Partial Class Viewswf
    Inherits System.Web.UI.Page


    Private _conn As New dConn(0)
    Private _sqlStr, _sqlStr2, _donem As String
    Private ds, ds2 As DataSet
    Private da As SqlDataAdapter
    Private dataRelation As DataRelation
    Private menu As Menu
    Private _dosyaID As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("kisino") = 0 Or Session("kisino") Is Nothing Then
            Response.Redirect("Login.aspx", True)
            Exit Sub
        End If

        If Session("dosyaadi") IsNot Nothing Then
            If Session("dosyaadi") <> "" Then
                'KySwf1.MovieName = Session("dosyaadi") + "?POPUP_ENABLED=False"
            End If
        End If
        If Session("type") = "Student" Then
            Try
                Session("PageIs") = "ViewSwf"
                'PopulateMenu()
                'AddHandler menu.MenuItemClick, AddressOf onItemClick
                ' o_lbl_term.Text = Session("term")
                ' o_lbl_term2.Text = Session("term2")
            Catch _excp As Exception
            End Try
        End If
    End Sub

    Private Sub onItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs)
        If Mid(e.Item.Value, 1, 4) = "DOS#" Then
            _dosyaID = Mid(e.Item.Value, 5, e.Item.Value.Length - 4)
            getThatFileName()
        End If
    End Sub

    'Protected Sub dersmenu_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dersmenu.SelectedNodeChanged
    '    dersmenu.SelectedNode.Expand()
    '    If Mid(dersmenu.SelectedValue, 1, 4) = "DOS#" Then
    '        _dosyaID = Mid(dersmenu.SelectedValue, 5, dersmenu.SelectedValue.Length - 4)
    '        getThatFileName()
    '    End If
    'End Sub

    'Private Sub PopulateMenu()
    '    Dim ds As DataSet = GetDataSetForMenu()
    '    dersmenu.Nodes.Clear()

    '    For Each parentRow As DataRow In ds.Tables("Dersler").Rows
    '        Dim node As New TreeNode(CType(parentRow("derskod"), String))

    '        For Each childRow As DataRow In parentRow.GetChildRows("Children")
    '            Dim childNode As New TreeNode(CType(childRow("ADI"), String))
    '            childNode.Value = "DOS#" & childRow("ID")
    '            node.ChildNodes.Add(childNode)
    '        Next
    '        dersmenu.Nodes.Add(node)
    '    Next
    '    dersmenu.CollapseAll()
    'End Sub

    Private Function GetDataSetForMenu() As DataSet
        Try
            If Session("term") = "Güz Dönemi" Then
                _donem = "GUZ"
            ElseIf Session("term") = "Bahar Dönemi" Then
                _donem = "BAH"
            End If
            _sqlStr = "SELECT derskod, dersadi,(SELECT     COUNT(proje1.dbo.DOSYALAR.ID) AS EXPR1" _
                   + " FROM proje1.dbo.DOSYALAR INNER JOIN" _
                   + " proje1.dbo.DERSLER ON proje1.dbo.DOSYALAR.ID = proje1.dbo.DERSLER.DOSYAID" _
                   + " WHERE (proje1.dbo.DERSLER.DERSKOD = KARNE2.dbo.karneler.derskod COLLATE Turkish_CS_AI) AND" _
                   + " (proje1.dbo.DOSYALAR.YAYIN > 0) AND (proje1.dbo.DERSLER.BIRIM='" + Session("bolum") + "') and (proje1.dbo.DERSLER.ONAY > 0) AND" _
                   + " (proje1.dbo.DOSYALAR.SIL = 0) AND (proje1.dbo.DOSYALAR.SWF = 3 OR proje1.dbo.DOSYALAR.SWF = 4) ) AS" _
                   + " derssayi FROM KARNE2.dbo.karneler WHERE" _
                   + " fakno = '" + Session("kisino") + "' AND birim ='" + Session("bolum") + "' AND alyil='" + Session("term2") + "' AND donem='" + _donem + "' ORDER BY derskod"

            _sqlStr2 = "SELECT proje1.dbo.DERSLER.BIRIM,proje1.dbo.DERSLER.DERSKOD,ltrim(rtrim(proje1.dbo.DOSYALAR.AD)) as ADI, proje1.dbo.DOSYALAR.SICIL," _
            + " (proje1.dbo.PERSONEL.AD + ' ' + proje1.dbo.PERSONEL.SOYAD) as YAYINLAYAN, proje1.dbo.DOSYALAR.ID," _
            + " proje1.dbo.DOSYALAR.ACIKLAMA, proje1.dbo.DOSYALAR.TARIH" _
            + " FROM proje1.dbo.DERSLER INNER JOIN" _
            + " proje1.dbo.DOSYALAR ON proje1.dbo.DERSLER.DOSYAID = proje1.dbo.DOSYALAR.ID INNER JOIN" _
            + " proje1.dbo.PERSONEL ON proje1.dbo.DOSYALAR.SICIL = proje1.dbo.PERSONEL.KURUMSICIL" _
            + " WHERE     proje1.dbo.DERSLER.DERSKOD COLLATE Turkish_CS_AI IN" _
            + " (SELECT derskod FROM KARNE2.dbo.karneler WHERE  fakno = '" + Session("kisino") + "' AND birim ='" + Session("bolum") + "' AND alyil='" + Session("term2") + "' AND donem='" + _donem + "')" _
            + " AND (proje1.dbo.DOSYALAR.YAYIN > 0) AND (proje1.dbo.DERSLER.BIRIM='" + Session("bolum") + "') AND (proje1.dbo.DERSLER.ONAY > 0) AND (proje1.dbo.DOSYALAR.SIL = 0) AND" _
            + " (proje1.dbo.DOSYALAR.SWF = 3 OR proje1.dbo.DOSYALAR.SWF = 4) ORDER BY DERSKOD"

            Dim ds As New DataSet()
            da = New SqlDataAdapter(_sqlStr, _conn.ConnectionString)
            da.Fill(ds, "Dersler")
            da.Dispose()
            da = New SqlDataAdapter(_sqlStr2, _conn.ConnectionString)
            da.Fill(ds, "Dosyalar")
            da.Dispose()
            ds.Relations.Add("Children", ds.Tables("Dersler").Columns("derskod"), ds.Tables("Dosyalar").Columns("DERSKOD"), False)
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub getThatFileName()
        With _conn
            'Dim _fileName As String = o_grid_mats.Rows(o_grid_mats.SelectedIndex).Cells(1).Text.Trim()
            Try
                '_sqlStr = "SELECT SICIL,AD FROM DOSYALAR WHERE ID=" + CType(o_grid_mats.SelectedValue, String)
                _sqlStr = "SELECT SICIL,AD FROM DOSYALAR WHERE ID=" + CType(_dosyaID, String)
                .SelectQuery(_sqlStr)
                If ._sqlReader.HasRows = True Then
                    While ._sqlReader.Read

                        'Dim _ext As String = Mid(_fileName, _fileName.LastIndexOf(".") + 1, _fileName.Length - _fileName.LastIndexOf("."))
                        Dim _ext As String = CType(._sqlReader("AD"), String).Trim.Remove(0, CType(._sqlReader("AD"), String).Trim.LastIndexOf("."))
                        If _ext.ToLower = ".zip" Or _ext.ToLower = ".rar" Then
                            Dim _filePath As String = "E:\dosyalar\Upload\" & CType(._sqlReader("SICIL"), String).Trim & "\" & CType(._sqlReader("AD"), String).Trim
                            Dim _fileStream As New IO.FileStream(_filePath, IO.FileMode.Open)
                            Dim _bytes(_fileStream.Length) As Byte

                            _fileStream.Read(_bytes, 0, _fileStream.Length)
                            _fileStream.Close()
                            Response.ContentType = "application/octet-stream"
                            Response.AddHeader("Content-disposition", "attachment; filename=" & CType(._sqlReader("AD"), String).Trim)
                            Response.BinaryWrite(_bytes)
                            Response.End()
                        Else
                            'Session("dosyaadi") = Trim(._sqlReader("SWFYOL")).Remove(0, Trim(._sqlReader("SWFYOL")).IndexOf("UploadSWF"))
                            Session("dosyaadi") = "E:\dosyalar\UploadSWF\" & CType(._sqlReader("SICIL"), String).Trim & "\" & (CType(._sqlReader("AD"), String).Trim.Remove(CType(._sqlReader("AD"), String).Trim.LastIndexOf("."))) & ".SWF"
                            Response.Redirect("Viewswf.aspx")
                        End If
                    End While
                Else
                    Session("dosyaadi") = ""
                    'Server.Transfer("Viewswf.aspx")
                    Response.Redirect("Viewswf.aspx")
                End If
            Catch _ioExcp As IO.IOException
                'o_lbl_notice.Text = "Dosya bulunamýyor"
                'o_lbl_notice.Text += _ioExcp.Message
            Catch _excp As Exception
                'o_lbl_notice.Text = "Bir hata oluþtu"
            Finally
                _conn.Close()
            End Try
        End With
    End Sub
End Class
