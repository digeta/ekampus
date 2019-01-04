Imports System.Data.SqlClient

Partial Class Catalogue
    Inherits System.Web.UI.Page

    Private dbConn As New dConn(0)
    Private conn As New SqlConnection(dbConn.ConnectionString)

    Private _conn1 As New dConn(0)
    Private _conn2 As New dConn(3)

    Private _count, i As Integer
    Private _sqlStr, _id, _birimno, litem_bol, litem_fak, litem_ata, _yetkifak As String
    Private tbox As TextBox
    Private lisitem As New ListItem

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("kisino") = 0 Then
            Server.Transfer("Login.aspx")
        End If
        Session("PageIs") = ""
        If Not Session("type") = "Peradmin" Then
            Session("authorized") = "None"
            Session.Clear()
            Response.Redirect("Login.aspx")
            Exit Sub
        End If

    End Sub

#Region "Ders Seçimi ve güncellemesi"
    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        PanelEdit.Visible = True
        o_btn_add.Visible = False
        o_btn_dupdate.Visible = True
        With _conn1
            Try
                If o_dlist_term.SelectedValue = "K1" Then
                    _sqlStr = "SELECT RECID, derskod, dersadi, teo, pra, lab, krd, ata FROM KATALOGGUZ WHERE RECID =" + CType(GridView1.SelectedValue, String)
                ElseIf o_dlist_term.SelectedValue = "K2" Then
                    _sqlStr = "SELECT RECID, derskod, dersadi, teo, pra, lab, krd, ata FROM KATALOGBAH WHERE RECID =" + CType(GridView1.SelectedValue, String)
                End If
                .SelectQuery(_sqlStr)
                ._sqlReader.Read()
                o_txt_dkod.Text = ._sqlReader("derskod")
                o_txt_dadi.Text = ._sqlReader("dersadi")
                o_txt_tkr.Text = ._sqlReader("teo")
                o_txt_pkr.Text = ._sqlReader("pra")
                o_txt_lab.Text = ._sqlReader("lab")
                o_txt_krd.Text = ._sqlReader("krd")
                If ._sqlReader("ata") IsNot DBNull.Value Then
                    litem_ata = ._sqlReader("ata")
                Else
                    litem_ata = ""
                End If
            Catch _excp As Exception
            Finally
                .Close()
            End Try
        End With
    End Sub

    Protected Sub o_btn_dupdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_btn_dupdate.Click
        '_check.controlsCheck(Master.FindControl("o_cph_main"))
        Dim cph1 As New ContentPlaceHolder
        o_lbl_error.Text = ""
        o_lbl_notice.Text = ""

        cph1 = CType(Master.FindControl("o_cph_main"), ContentPlaceHolder)

        Try

            If o_dlist_term.SelectedValue = "K1" Then
                dsKatalogG.Update()
            ElseIf o_dlist_term.SelectedValue = "K2" Then
                dsKatalogB.Update()
            End If
            GridView1.SelectedIndex = -1
            PanelEdit.Visible = False

        Catch _sqlExcp As Data.SqlClient.SqlException
            Response.Write(_sqlExcp.LineNumber & "<br>")
            Response.Write(_sqlExcp.Message)
        Finally
            GridView1.DataBind()
        End Try
    End Sub

    Protected Sub o_dlist_ata_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_dlist_ata.DataBound
        If litem_ata <> "" And litem_ata <> 0 Then
            o_dlist_ata.ClearSelection()
            o_dlist_ata.Items.FindByValue(litem_ata).Selected = True
        End If
        lisitem.Text = "Atama yapýlmadý"
        lisitem.Value = 0
        o_dlist_ata.Items.Insert(0, lisitem)
    End Sub
#End Region

#Region "Fakülte,Bölüm ve Dönem seçimleri"
    Protected Sub o_dlist_term_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_dlist_term.SelectedIndexChanged
        PanelEdit.Visible = False
        GridView1.Visible = True
        GridView1.SelectedIndex = -1
        If o_dlist_term.SelectedValue = "K1" Then
            GridView1.DataSourceID = "dsKatalogG"
        ElseIf o_dlist_term.SelectedValue = "K2" Then
            GridView1.DataSourceID = "dsKatalogB"
        End If
        o_btn_new.Visible = True
    End Sub

    Protected Sub o_dlist_fak_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_dlist_fak.DataBound
        If litem_fak <> "" Then
            o_dlist_fak.ClearSelection()
            o_dlist_fak.Items.FindByValue(litem_fak).Selected = True
        End If
        lisitem.Text = "Lütfen Seçiniz"
        lisitem.Value = -1
        o_dlist_fak.Items.Insert(0, lisitem)
        With _conn1
            Try
                _sqlStr = "SELECT YETKI FROM ADMIN WHERE SICIL=" + CType(Session("kisino"), String)
                .SelectQuery(_sqlStr)
                ._sqlReader.Read()
                _yetkifak = ._sqlReader("YETKI").ToString.Replace(" ", "")
            Catch _excp As Exception
            Finally
                .Close()
            End Try
        End With

        With _conn2
            Try
                If Not _yetkifak = 0 Then
                    _count = ((_yetkifak.Length) / 2) - 1
                    Dim dplitem(_count) As ListItem
                    Dim line As Integer
                    line = i + 1
                    For i = 0 To _count
                        dplitem(i) = New ListItem
                        _sqlStr = "SELECT okulad1 FROM kisimlar WHERE LEFT(birim,2)='" + Mid(_yetkifak, line, 2) + "' GROUP BY okulad1, LEFT (birim, 2)"
                        .SelectQuery(_sqlStr)
                        ._sqlReader.Read()
                        dplitem(i).Text = ._sqlReader("okulad1")
                        dplitem(i).Value = Mid(_yetkifak, line, 2)
                        o_dlist_fak.Items.Insert(i + 1, dplitem(i))
                        line += 2
                        ._sqlReader.Close()
                    Next
                End If
            Catch _excp As Exception
            Finally
                .Close()
            End Try
        End With
    End Sub

    Protected Sub o_dlist_fak_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_dlist_fak.SelectedIndexChanged
        If o_dlist_fak.SelectedValue <> -1 Then
            o_dlist_bol.Enabled = True
            PanelEdit.Visible = False
            GridView1.Visible = False
            GridView1.SelectedIndex = -1
            o_dlist_term.SelectedIndex = -1
            o_dlist_term.Enabled = False
            o_btn_new.Visible = False
        End If
    End Sub

    Protected Sub o_dlist_bol_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_dlist_bol.DataBound
        If litem_bol <> "" Then
            o_dlist_bol.ClearSelection()
            o_dlist_bol.Items.FindByValue(litem_bol).Selected = True
        End If
        lisitem.Text = "Lütfen Seçiniz"
        lisitem.Value = -1
        o_dlist_bol.Items.Insert(0, lisitem)
    End Sub

    Protected Sub o_dlist_bol_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_dlist_bol.SelectedIndexChanged
        o_dlist_term.Enabled = True
        o_dlist_bol.Enabled = False
    End Sub
#End Region

#Region "Yeni ders ekleme"
    Protected Sub o_btn_new_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_btn_new.Click
        Try
            PanelEdit.Visible = True
            o_btn_dupdate.Visible = False
            o_btn_add.Visible = True
            GridView1.SelectedIndex = -1
            Dim tboxes As TextBox
            Dim tcont As Control
            For Each tcont In PanelEdit.Controls
                If TypeOf tcont Is TextBox Then
                    tboxes = CType(tcont, TextBox)
                    tboxes.Text = ""
                End If
            Next
            o_dlist_ata.SelectedIndex = -1
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub o_btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_btn_add.Click
        Dim cph1 As New ContentPlaceHolder
        o_lbl_error.Text = ""
        o_lbl_notice.Text = ""

        cph1 = CType(Master.FindControl("o_cph_main"), ContentPlaceHolder)

        Try

            If o_dlist_term.SelectedValue = "K1" Then
                dsKatalogG.Insert()
            ElseIf o_dlist_term.SelectedValue = "K2" Then
                dsKatalogB.Insert()
            End If
            GridView1.SelectedIndex = -1
            PanelEdit.Visible = False

        Catch _sqlExcp As Data.SqlClient.SqlException
            Response.Write(_sqlExcp.LineNumber & "<br>")
            Response.Write(_sqlExcp.Message)
        Finally
            GridView1.DataBind()
        End Try
    End Sub
#End Region
End Class
