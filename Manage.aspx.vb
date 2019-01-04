Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Partial Class Manage
    Inherits System.Web.UI.Page

   Private conn As New SqlConnection(ConfigurationManager.AppSettings("conn0"))

    Protected Sub o_btn_do_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_btn_do.Click
        o_grid_result.SelectedIndex = -1
        o_dtl_user.ChangeMode(DetailsViewMode.ReadOnly)
        sorgu_olustur()
    End Sub

    Private Sub sorgu_olustur()
        Dim bimonay As Integer = o_ddl_bimonay.SelectedValue
        Dim pcturu As Integer = o_ddl_pctur.SelectedValue
        Dim ipaddr As String = o_txt_ip.Text
        Dim macaddr As String = o_txt_mac.Text
        'Dim vlan As Integer = o_ddl_vlan.SelectedValue
        Dim kisino As Long = IIf(o_txt_kisi.Text = "", 0, o_txt_kisi.Text)
        Dim adsoyad As String = o_txt_adsoyad.Text
        Dim lab As Integer = o_ddl_lab.SelectedValue
        Dim isaktif As Integer = o_ddl_aktif.SelectedValue

        Dim ad() As String = adsoyad.Split(" ")

        Dim sartsayi As Integer = 0
        Dim sorguSart As New ArrayList

        If o_chk_bimonay.Checked = True Then sorguSart.Add("BIM_ONAY = " & bimonay & "#")
        If o_chk_pctur.Checked = True Then sorguSart.Add("PCTUR = " & pcturu & "#")
        If o_chk_ip.Checked = True Then sorguSart.Add("IP = '" & ipaddr & "'#")
        If o_chk_mac.Checked = True Then sorguSart.Add("MAC = '" & macaddr & "'#")
        'If o_chk_vlan.Checked = True Then sorguSart.Add("VLAN_ID = " & vlan & "#")
        If o_chk_kisi.Checked = True Then sorguSart.Add("KISI_NO = " & kisino & "#")
        If o_chk_lab.Checked = True Then sorguSart.Add("LAB = " & lab & "#")
        If o_chk_aktif.Checked = True Then sorguSart.Add("AKTIF = " & isaktif & "#")
        If o_chk_adsoyad.Checked = True And adsoyad <> "" Then
            If ad.Length > 1 Then
                sorguSart.Add("AD LIKE '%" & ad(0) & "%' AND SOYAD LIKE '%" & ad(1) & "%'#")
            ElseIf ad.Length = 1 Then
                sorguSart.Add("(AD LIKE '%" & ad(0) & "%' OR SOYAD LIKE '%" & ad(0) & "%')#")
            End If
        End If

        If sorguSart.Count > 0 Then
            Dim sb As New StringBuilder
            sb.Append("SELECT * FROM IPMAC WHERE ")
            If sorguSart.Count > 1 Then
                For i As Integer = 0 To sorguSart.Count - 2
                    Dim sart As String = Regex.Replace(CType(sorguSart(i), String), "#", " AND ")
                    sb.Append(sart)
                Next
                sb.Append(Regex.Replace(CType(sorguSart(sorguSart.Count - 1), String), "#", ""))
            ElseIf sorguSart.Count = 1 Then
                sb.Append(Regex.Replace(CType(sorguSart(0), String), "#", ""))
            End If
            'Response.Write(sb.ToString)
            ViewState("sqlstr") = sb.ToString
            dsGrid.SelectCommand = sb.ToString
            'Else
            'Response.Write("NO~thing selected")
        End If
    End Sub

    Private Sub vlanlar()
        Try
            Dim command As New SqlCommand("stp_VLANS", conn)
            command.CommandType = Data.CommandType.StoredProcedure

            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)
            Dim dt As New Data.DataTable()
            Adapter.Fill(dt)

            o_ddl_vlan.DataSource = dt
            o_ddl_vlan.DataTextField = "VLAN_ADI"
            o_ddl_vlan.DataValueField = "VLAN_NO"
            o_ddl_vlan.DataBind()
        Catch ex As Exception
            'lblUyari.Visible = True
            'lblUyari.Text = "Ýþlem sýrasýnda hata oluþtu. Hata kodu : 300101"
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub labs()
        Try
            Dim command As New SqlCommand("stp_LABS", conn)
            command.CommandType = Data.CommandType.StoredProcedure

            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)
            Dim dt As New Data.DataTable()
            Adapter.Fill(dt)

            o_ddl_lab.DataSource = dt
            o_ddl_lab.DataTextField = "LAB_AD"
            o_ddl_lab.DataValueField = "ID"
            o_ddl_lab.DataBind()
        Catch ex As Exception
            'lblUyari.Visible = True
            'lblUyari.Text = "Ýþlem sýrasýnda hata oluþtu. Hata kodu : 300101"
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        dsGrid.ConnectionString = ConfigurationManager.AppSettings("conn0")
        dsDetails.ConnectionString = ConfigurationManager.AppSettings("conn0")
        dsDetailsServer.ConnectionString = ConfigurationManager.AppSettings("conn0")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("kisino") = 0 Then
            Server.Transfer("Login.aspx")
        End If

        If Session("kisino") = 2314 And Session("type") = "JediMaster" Then
            If Page.IsPostBack = False Then
                vlanlar()
                labs()
            End If
            dsGrid.SelectCommand = ViewState("sqlstr")
        Else
            Session.Clear()
            Response.Redirect("Login.aspx")
            Exit Sub
        End If
        Session("PageIs") = ""
    End Sub

    Protected Sub o_btn_reset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_btn_reset.Click
        o_grid_result.SelectedIndex = -1
        o_dtl_user.ChangeMode(DetailsViewMode.ReadOnly)
        o_chk_bimonay.Checked = False
        o_chk_pctur.Checked = False
        o_chk_ip.Checked = False
        o_chk_mac.Checked = False
        o_chk_vlan.Checked = False
        o_chk_kisi.Checked = False
        o_chk_adsoyad.Checked = False
        o_chk_lab.Checked = False
        o_chk_aktif.Checked = False
    End Sub

    Protected Sub dsGrid_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles dsGrid.Selecting
        e.Command.CommandText = ViewState("sqlstr")
    End Sub
End Class
