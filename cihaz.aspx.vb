Imports System.Data
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.IO
Imports System.Text.RegularExpressions

Partial Class cihaz
    Inherits System.Web.UI.Page

    Private konsol As New Process
    Private conn As New SqlConnection(ConfigurationManager.AppSettings("conn0"))
    Private returnValue As SqlParameter
    Private yetkilist(5) As Integer

#Region "Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '##############Kullan�c� giri� kontrol####################
        If Session("kisino") = 0 Or Session("kisino") Is Nothing Then
            Response.Redirect("Login.aspx", True)
            Exit Sub
        End If

        Session("PageIs") = ""

        If Session("cihazislem") = False Then
            Response.Write("Default.aspx")
            Exit Sub
        End If
        '#########################################################
        o_lbl_yetki.Text = ""
        lblUyari.Text = ""
        o_lbl_ipbilgi.Text = "�uan ki IP numaran�z : " & Session("ipno")

        Dim vlan_id() As String = CType(Session("ipno"), String).Split(".")
        ViewState("vlan") = vlan_id(1)
        Dim vlan_id2 As Integer = CType(vlan_id(2), Integer)

        If Page.IsPostBack = False Then
            Dim mac As String
            If Session("macAdresi") IsNot Nothing Then
                mac = Session("macAdresi")
            Else
                mac = ""
            End If

            If mac.Length < 1 Then macAdres_getir(False)

            If Session("macAdresi") = "INVALIDMAC" Then
                lblUyari.Visible = True
                lblUyari.Text = "Bir hata olu�tu, sisteme yeniden giri� yapmay� deneyin. Hata kodu: 300103"
                Exit Sub
            Else
                o_div_pc.Visible = True
                pcleri_getir()
                pcleri_say()
                'misafir_getir()
            End If

            populate_pcturlist()
            iplistesi_olustur()
            dis_iplistesi()
            cihaz_iplistesi_olustur()
            labs()
        End If

        yetkileri_listele()

        If vlan_id2 = 1 Or (vlan_id2 <> 2 And (vlan_id2 < 16 Or vlan_id2 > 26)) Then
            o_div_pc.Visible = False
        End If

        defaultControls()
    End Sub
#End Region
#Region "Kay�tl� bilgisayarlar"
    Private Sub pcleri_getir()
        Try
            If CType(Session("macAdresi"), String).Length < 18 Then
                Dim command As New SqlCommand("stp_IPMAC_GETIR", conn)
                command.CommandType = Data.CommandType.StoredProcedure

                With command.Parameters
                    .AddWithValue("KISI_NO", CType(Session("kisino"), Long))   'Kurumsicil
                    '.AddWithValue("MAC", Session("macAdresi"))
                End With

                Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)
                Dim dt As New DataTable()
                If conn.State <> Data.ConnectionState.Open Then conn.Open()
                Adapter.Fill(dt)
                If conn.State = Data.ConnectionState.Open Then conn.Close()

                o_grid_macs.DataSource = dt
                o_grid_macs.DataBind()
            End If
        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "��lem s�ras�nda hata olu�tu. Hata kodu : 300101"
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub pcleri_say()
        Try
            Dim command As New SqlCommand("stp_AKTIFPCLER", conn)
            command.CommandType = Data.CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("KISINO", CType(Session("kisino"), Long))   'Kurumsicil
            End With

            Dim reader As SqlDataReader
            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            reader = command.ExecuteReader

            If reader.HasRows = True Then
                reader.Read()
                Session("pcsay") = Session("yetki_pcsay") - reader("PCLER")
                Session("mobilpcsay") = Session("yetki_mobilpcsay") - reader("MOBILPC")
                Session("labpcsay") = Session("yetki_labpcsay") - reader("LABPC")
                Session("sunucusay") = Session("yetki_sunucusay") - reader("SUNUCULAR")
                Session("misafirpcsay") = Session("yetki_misafirpcsay") - reader("MISAFIRPC")
                Session("digercihazsay") = Session("yetki_digercihazsay") - reader("DIGERCIHAZ")
            Else
                Session("pcsay") = Session("yetki_pcsay")
                Session("mobilpcsay") = Session("yetki_mobilpcsay")
                Session("labpcsay") = Session("yetki_labpcsay")
                Session("sunucusay") = Session("yetki_sunucusay")
                Session("misafirpcsay") = Session("yetki_misafirpcsay")
                Session("digercihazsay") = Session("yetki_digercihazsay")
            End If
            reader.Close()

            yetkilist(0) = Session("pcsay")
            yetkilist(1) = Session("mobilpcsay")
            yetkilist(2) = Session("labpcsay")
            yetkilist(3) = Session("sunucusay")
            yetkilist(4) = Session("misafirpcsay")
            yetkilist(5) = Session("digercihazsay")
        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "��lem s�ras�nda hata olu�tu. Hata kodu : 300102"
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub misafir_getir()
        Try
            If CType(Session("macAdresi"), String).Length < 18 Then
                Dim command As New SqlCommand("stp_IPMAC_MSFGETIR", conn)
                command.CommandType = Data.CommandType.StoredProcedure

                With command.Parameters
                    .AddWithValue("KISI_NO", CType(Session("kisino"), Long))   'Kurumsicil
                    '.AddWithValue("MAC", Session("macAdresi"))
                End With

                Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)
                Dim dt As New DataTable()
                If conn.State <> Data.ConnectionState.Open Then conn.Open()
                Adapter.Fill(dt)
                If conn.State = Data.ConnectionState.Open Then conn.Close()

                o_grid_msf.DataSource = dt
                o_grid_msf.DataBind()
            End If
        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "��lem s�ras�nda hata olu�tu. Hata kodu : 300101"
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try
    End Sub
#End Region
#Region "Yetkileri listele"
    Protected Sub o_ddl_pctur_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_ddl_pctur.SelectedIndexChanged
        'defaultControls()
    End Sub

    Private Sub yetkileri_listele()
        o_lbl_yetki.Text = ""
        If Session("pcsay") > 0 Then o_lbl_yetki.Text = Session("pcsay") & " ki�isel bilgisayar ekleme yetkiniz var.<br>"
        If Session("mobilpcsay") > 0 Then o_lbl_yetki.Text &= Session("mobilpcsay") & " hareketli bilgisayar ekleme yetkiniz var.<br>"
        If Session("labpcsay") > 0 Then o_lbl_yetki.Text &= Session("labpcsay") & " laboratuar bilgisayar� ekleme yetkiniz var.<br>"
        If Session("sunucusay") > 0 Then o_lbl_yetki.Text &= Session("sunucusay") & " sunucu ekleme yetkiniz var.<br>"
        If Session("misafirpcsay") > 0 Then o_lbl_yetki.Text &= Session("misafirpcsay") & " misafir bilgisayar� ekleme yetkiniz var.<br>"
        If Session("digercihazsay") > 0 Then o_lbl_yetki.Text &= Session("digercihazsay") & " di�er cihaz ekleme yetkiniz var.<br>"
    End Sub
#End Region
#Region "PC T�r� Listesi"
    Private Sub populate_pcturlist()
        o_ddl_pctur.Items.Clear()
        Dim litem As ListItem
        Dim litem0 As New ListItem("L�tfen se�iniz", -1)
        o_ddl_pctur.Items.Add(litem0)

        If Session("pcsay") > 0 Then
            litem = New ListItem("Sabit kullan�c�", 0)
            o_ddl_pctur.Items.Add(litem)
        End If
        If Session("mobilpcsay") > 0 Then
            litem = New ListItem("Hareketli kullan�c�", 1)
            o_ddl_pctur.Items.Add(litem)
        End If
        If Session("labpcsay") > 0 Then
            litem = New ListItem("Ortak kullan�m", 2)
            o_ddl_pctur.Items.Add(litem)
        End If
        If Session("sunucusay") > 0 Then
            litem = New ListItem("Sunucu", 3)
            o_ddl_pctur.Items.Add(litem)
        End If
        If Session("misafirpcsay") > 0 Then
            litem = New ListItem("Misafir", 4)
            o_ddl_pctur.Items.Add(litem)
        End If
        If Session("digercihazsay") > 0 Then
            litem = New ListItem("Di�er cihazlar", 5)
            o_ddl_pctur.Items.Add(litem)
        End If
        If o_ddl_pctur.Items.Count <= 1 Then
            o_div_pc.Visible = False
        End If

        o_ddl_pctur.SelectedIndex = -1
    End Sub
#End Region
#Region "MAC adresi tespiti"
    Private Sub macAdres_getir(ByVal manuelMAC As Boolean)
        Try
            Dim sistemKlasor As String = Environment.SystemDirectory
            Dim girdi As StreamWriter
            Dim cikti As StreamReader
            Dim girdiVerisi As String

            Session("manuelMAC") = "INVALIDMAC"

            If manuelMAC = False Then
                girdiVerisi = sistemKlasor & _
                "\ping.exe -n 1 -w 10 " & CType(Session("ipno"), String) & " | " & sistemKlasor & "\arp.exe -a " & CType(Session("ipno"), String)
                '"\ping.exe -n 1 -w 10 10.1.19.12 | arp -a 10.1.19.12"
            Else
                girdiVerisi = sistemKlasor & _
                "\ping.exe -n 1 -w 10 " & CType(Session("manuelIP"), String) & " | " & sistemKlasor & "\arp.exe -a " & CType(Session("manuelIP"), String)
            End If

            With konsol.StartInfo
                .FileName = sistemKlasor & "\cmd.exe"
                .UseShellExecute = False
                .RedirectStandardError = True
                .RedirectStandardInput = True
                .RedirectStandardOutput = True
            End With

            konsol.Start()
            girdi = konsol.StandardInput
            girdi.WriteLine(girdiVerisi & vbCrLf)
            girdi.Close()
            cikti = konsol.StandardOutput

            Dim veri As New StringBuilder
            Dim mac As String = ""

            For i As Integer = 0 To 9
                veri.Append(cikti.ReadLine)
            Next
            mac = Regex.Match(veri.ToString, "([0-9a-fA-F]{2}[:-]){5}[0-9a-fA-F]{2}").Value
            cikti.Close()

            If mac = "" Or mac.Length <> 17 Then
                Session("macAdresi") = "INVALIDMAC"
                Exit Sub
            End If

            Dim veriMac As New StringBuilder
            For Each c As Char In mac
                If c = "-" Then
                    veriMac.Append(":")
                Else
                    veriMac.Append(c)
                End If
            Next

            If manuelMAC = True Then
                Session("manuelMAC") = veriMac.ToString
            Else
                Session("macAdresi") = veriMac.ToString
            End If

        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "��lem s�ras�nda hata olu�tu. Hata kodu : 300103"
        End Try
    End Sub
#End Region
#Region "Varsay�lan kontroller"
    Private Sub defaultControls()
        Try
            o_div_onepc.Visible = False
            o_div_server.Visible = False
            o_div_lab.Visible = False
            o_div_guest.Visible = False
            o_div_toc.Visible = False
            o_btn_svmac.Enabled = False

            If Not o_ddl_pctur.SelectedValue < 0 Then
                o_div_toc.Visible = True
            End If

            If o_ddl_pctur.SelectedValue = 4 Then
                If o_chk_toc.Checked = True And o_chk_msftoc.Checked = True Then
                    o_btn_svmac.Enabled = True
                End If
            Else
                If o_chk_toc.Checked = True Then
                    o_btn_svmac.Enabled = True
                End If
            End If

            ''###########################################
            If o_ddl_pctur.SelectedValue = 0 Then
                o_div_onepc.Visible = True
                o_lbl_ipler.Visible = False
                o_ddl_ipler.Visible = False
                o_ddl_iplerdis.Visible = False

                If o_rdb_pc2.Checked = True Then
                    o_lbl_ip.Visible = True
                    o_txt_ip.Visible = True
                    o_btn_getmac.Visible = True
                    o_lbl_result.Visible = True
                Else
                    o_lbl_ip.Visible = False
                    o_txt_ip.Visible = False
                    o_btn_getmac.Visible = False
                    o_lbl_result.Visible = False
                    o_txt_ip.Text = ""
                    o_lbl_result.Text = ""
                    Session("manuelMAC") = ""
                End If

            ElseIf o_ddl_pctur.SelectedValue = 2 Then
                o_div_lab.Visible = True

            ElseIf o_ddl_pctur.SelectedValue = 3 Then
                o_div_onepc.Visible = True
                o_div_server.Visible = True
                o_lbl_ipler.Visible = True
                o_ddl_ipler.Visible = True
                o_ddl_iplerdis.Visible = True
                o_ddl_cihazip.Visible = False
                o_lbl_ipler.Text = "Sunucu IP (i� / d��) :"

                If o_rdb_domain1.Checked = True Then
                    o_lbl_domainexp.Visible = True
                    o_lbl_domain.Visible = True
                    o_txt_domain.Visible = True
                Else
                    o_lbl_domainexp.Visible = False
                    o_lbl_domain.Visible = False
                    o_txt_domain.Visible = False
                End If

                If o_rdb_pc2.Checked = True Then
                    o_lbl_ip.Visible = True
                    o_txt_ip.Visible = True
                    o_btn_getmac.Visible = True
                    o_lbl_result.Visible = True
                Else
                    o_lbl_ip.Visible = False
                    o_txt_ip.Visible = False
                    o_btn_getmac.Visible = False
                    o_lbl_result.Visible = False
                    o_txt_ip.Text = ""
                    o_lbl_result.Text = ""
                    Session("manuelMAC") = ""
                End If

            ElseIf o_ddl_pctur.SelectedValue = 4 Then
                o_div_guest.Visible = True
                o_div_onepc.Visible = True
                o_lbl_ipler.Visible = False
                o_ddl_ipler.Visible = False
                o_ddl_iplerdis.Visible = False
                o_ddl_cihazip.Visible = False
                o_chk_msftoc.Visible = True

                If o_rdb_pc2.Checked = True Then
                    o_lbl_ip.Visible = True
                    o_txt_ip.Visible = True
                    o_btn_getmac.Visible = True
                    o_lbl_result.Visible = True
                Else
                    o_lbl_ip.Visible = False
                    o_txt_ip.Visible = False
                    o_btn_getmac.Visible = False
                    o_lbl_result.Visible = False
                    o_txt_ip.Text = ""
                    o_lbl_result.Text = ""
                    Session("manuelMAC") = ""
                End If

            ElseIf o_ddl_pctur.SelectedValue = 5 Then
                o_div_onepc.Visible = True
                o_ddl_cihazip.Visible = True
                o_lbl_ipler.Text = "Cihaza atanacak IP : "
                o_lbl_ipler.Visible = True
                o_ddl_ipler.Visible = False
                o_ddl_iplerdis.Visible = False
                o_rdb_pc1.Visible = False
                o_rdb_pc2.Visible = False
                o_lbl_ip.Visible = True
                o_txt_ip.Visible = True
                o_btn_getmac.Visible = True
                o_lbl_result.Visible = True
            End If
            '##########################################

        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "��lem s�ras�nda hata olu�tu. Hata kodu : 300105"
        End Try
    End Sub
#End Region
#Region "Sunucular i�in IP listesi"
    Private Sub iplistesi_olustur()
        Try
            o_ddl_ipler.Items.Clear()
            Dim vlan_id() As String = CType(Session("ipno"), String).Split(".")
            Dim dt As New DataTable
            Dim dts As New DataTable
            'Dim view As Data.DataView
            Dim ipler As New ArrayList
            Dim ip As String
            Dim var As Boolean

            Dim command As New SqlCommand("stp_SUNUCU_GETIR", conn)
            command.CommandType = Data.CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("IPLIKE", "10." & ViewState("vlan") & ".")
            End With

            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)
            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            Adapter.Fill(dt)
            If conn.State = Data.ConnectionState.Open Then conn.Close()
            'View = dsServer.Select(DataSourceSelectArguments.Empty)
            'dt = View.ToTable

            For i As Integer = 0 To 15
                ip = "10." & vlan_id(1) & ".2." & 1 + i
                var = False
                If dt.Rows.Count > 0 Then
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If dt.Rows(j)(0) = ip Then
                            var = True
                        End If
                    Next
                End If
                If Not var Then ipler.Add(ip)
            Next

            'dts.Columns.Add("IP", GetType(String))
            Dim litem As ListItem
            For i As Integer = 0 To ipler.Count - 1
                'dts.Rows.Add(New Object() {ipler(i)})
                litem = New ListItem(ipler(i), ipler(i))
                o_ddl_ipler.Items.Add(litem)
            Next

        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "��lem s�ras�nda hata olu�tu. Hata kodu : 300106"
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub dis_iplistesi()
        Try
            Dim dt As New Data.DataSet
            Dim command As New SqlCommand("stp_SUNUCUDISIP_GETIR", conn)
            command.CommandType = Data.CommandType.StoredProcedure

            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)
            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            Adapter.Fill(dt)
            If conn.State = Data.ConnectionState.Open Then conn.Close()

            o_ddl_iplerdis.DataSource = dt
            o_ddl_iplerdis.DataValueField = "IP"
            o_ddl_iplerdis.DataBind()

        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "��lem s�ras�nda hata olu�tu. Hata kodu : 300106"
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try
    End Sub
#End Region
#Region "Cihazlar i�in IP listesi"
    Private Sub cihaz_iplistesi_olustur()
        Try
            o_ddl_cihazip.Items.Clear()
            Dim vlan_id() As String = CType(Session("ipno"), String).Split(".")
            Dim dt As New DataTable
            Dim dts As New DataTable
            'Dim view As Data.DataView
            Dim ipler As New ArrayList
            Dim ip As String
            Dim var As Boolean

            Dim command As New SqlCommand("stp_CIHAZ_GETIR", conn)
            command.CommandType = Data.CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("IPLIKE", "10." & ViewState("vlan") & ".")
            End With

            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)
            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            Adapter.Fill(dt)
            If conn.State = Data.ConnectionState.Open Then conn.Close()
            'View = dsServer.Select(DataSourceSelectArguments.Empty)
            'dt = View.ToTable

            For i As Integer = 0 To 250
                ip = "10." & vlan_id(1) & ".4." & 1 + i
                var = False
                If dt.Rows.Count > 0 Then
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If dt.Rows(j)(0) = ip Then
                            var = True
                        End If
                    Next
                End If
                If Not var Then ipler.Add(ip)
            Next

            'dts.Columns.Add("IP", GetType(String))
            Dim litem As ListItem
            For i As Integer = 0 To ipler.Count - 1
                'dts.Rows.Add(New Object() {ipler(i)})
                litem = New ListItem(ipler(i), ipler(i))
                o_ddl_cihazip.Items.Add(litem)
            Next

        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "��lem s�ras�nda hata olu�tu. Hata kodu : 300106"
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try
    End Sub
#End Region

#Region "Yeni bilgisayar kayd�"
    Protected Sub o_btn_svmac_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_btn_svmac.Click
        If o_ddl_pctur.SelectedValue = -1 Then
            lblUyari.Visible = True
            lblUyari.Text = "Bir bilgisayar t�r� se�iniz"
            Exit Sub
        End If

        Dim returned As String = alanKontrol()

        If returned = "OK@$REGEX" Then
            Try
                Dim ipAdresi As String = ""
                Dim dis_ipadresi As String = ""
                Dim mac As String = ""

                If o_ddl_pctur.SelectedValue = 3 Then
                    ipAdresi = o_ddl_ipler.SelectedValue
                    dis_ipadresi = o_ddl_iplerdis.SelectedValue

                    If o_rdb_pc1.Checked = True Then
                        mac = Session("macAdresi")
                    Else
                        mac = Session("manuelMAC")
                    End If
                Else
                    If o_ddl_pctur.SelectedValue = 5 Then
                        ipAdresi = o_ddl_cihazip.SelectedValue
                        mac = Session("manuelMAC")
                    Else
                        If o_rdb_pc1.Checked = True Then
                            ipAdresi = CType(Session("ipno"), String)
                            mac = Session("macAdresi")
                        Else
                            ipAdresi = o_txt_ip.Text
                            mac = Session("manuelMAC")
                        End If
                    End If
                End If

                If mac.Length = 17 Then
                    Dim adsoyad() As String = DirectCast(Session("adsoyad"), String).Split(" ")
                    Dim vlanID() As String = ipAdresi.Split(".")
                    Dim result As Integer = 0
                    Dim result2 As Integer = 0

                    Dim command As New SqlCommand("stp_IPMAC_EKLE", conn)
                    command.CommandType = Data.CommandType.StoredProcedure

                    Dim bimonay As Boolean = True
                    Dim fwekle As Boolean = True
                    Dim tunekle As Boolean = False

                    Select Case o_ddl_pctur.SelectedValue
                        Case 2
                            fwekle = False
                        Case 3
                            bimonay = False
                            tunekle = True
                        Case 5
                            fwekle = False
                            tunekle = True
                    End Select

                    With command.Parameters
                        .AddWithValue("KISI_ID", Session("id"))  'Personel tablosundaki ID
                        .AddWithValue("KISI_NO", CType(Session("kisino"), Long))   'Kurumsicil
                        .AddWithValue("IP", ipAdresi)
                        .AddWithValue("DIS_IP", dis_ipadresi)
                        .AddWithValue("MAC", mac)
                        .AddWithValue("PCTUR", o_ddl_pctur.SelectedValue)
                        .AddWithValue("LAB", IIf(o_ddl_pctur.SelectedValue = 2, o_ddl_lab.SelectedValue, 0))
                        .AddWithValue("PCNO", IIf(o_ddl_pctur.SelectedValue = 2, o_txt_pcno.Text, 0))
                        Dim ad As String = ""
                        For i As Integer = 0 To adsoyad.Length - 2
                            ad = ad & adsoyad(i) & " "
                        Next
                        .AddWithValue("AD", ad)
                        .AddWithValue("SOYAD", adsoyad(adsoyad.Length - 1))
                        .AddWithValue("VLAN_ID", vlanID(1))
                        .AddWithValue("BIM_ONAY", bimonay)
                        .AddWithValue("WEB", o_chk_web.Checked)
                        .AddWithValue("SWEB", o_chk_sweb.Checked)
                        .AddWithValue("REMOTE_DESKTOP", o_chk_remote.Checked)
                        .AddWithValue("SSH", o_chk_ssh.Checked)
                        .AddWithValue("DIGER", IIf(o_txt_others.Text = "", " ", o_txt_others.Text))
                        .AddWithValue("DOMAIN", IIf(o_rdb_domain1.Checked = True, o_txt_domain.Text, ""))
                        .AddWithValue("DOMAIN_ISTEK", o_rdb_domain1.Checked)
                        .AddWithValue("ACIKLAMA", IIf(o_txt_aciklama.Text = "", " ", o_txt_aciklama.Text))
                        .AddWithValue("FW_EKLE", fwekle)
                        .AddWithValue("TUN_EKLE", tunekle)
                        .AddWithValue("MSFTC", o_txt_msfkimlik.Text)
                        .AddWithValue("MSFAD", o_txt_msfad.Text)
                        .AddWithValue("MSFSOYAD", o_txt_msfsoyad.Text)
                        .AddWithValue("MSFSURE", o_ddl_time.SelectedValue)
                    End With

                    returnValue = New SqlParameter("RETURNVALUE", Data.SqlDbType.Int)
                    returnValue.Direction = Data.ParameterDirection.ReturnValue
                    command.Parameters.Add(returnValue)

                    If conn.State <> Data.ConnectionState.Open Then conn.Open()
                    command.ExecuteNonQuery()
                    If conn.State = Data.ConnectionState.Open Then conn.Close()

                    lblUyari.Visible = True
                    If returnValue.Value = 100 Then
                        lblUyari.Text = "<img src=""images/img_cihaz/success.png"">" & "Bilgisayar�n�z / cihaz�n�z ba�ar�yla kaydedildi"
                    ElseIf returnValue.Value = 900 Then
                        lblUyari.Text = "<img src=""images/img_cihaz/warning.png"">" & "Kaydetmeye �al��t���n�z bilgisayar / cihaz daha �nce kaydedilmi�"
                    End If

                    If returnValue.Value < 200 Then pcleri_getir()
                    If returnValue.Value < 200 Then pcleri_say()
                    If returnValue.Value < 200 Then
                        populate_pcturlist()
                    End If
                End If
            Catch ex As Exception
                lblUyari.Visible = True
                lblUyari.Text = "��lem s�ras�nda hata olu�tu. Hata kodu : 300104"
                'lblUyari.Text = ex.Message
            Finally
                If conn.State = Data.ConnectionState.Open Then conn.Close()
                cleanse()
                yetkileri_listele()
                defaultControls()
                iplistesi_olustur()
                dis_iplistesi()
                cihaz_iplistesi_olustur()
            End Try
        Else
            lblUyari.Visible = True
            'lblUyari.Text = "��lem s�ras�nda hata olu�tu. Hata kodu : 300110"
            lblUyari.Text = returned
        End If
    End Sub
#End Region
#Region "Alan kontrolleri"
    Private Function alanKontrol() As String
        Try
            If o_ddl_pctur.SelectedValue = 4 Then
                If Not Regex.IsMatch(o_txt_msfad.Text.Trim & o_txt_msfsoyad.Text.Trim, "^[a-zA-Z������������]+$") Then Return "Ad veya soyad� hatal� girdiniz"
            End If

            If o_ddl_pctur.SelectedValue = 3 And o_txt_others.Text <> "" Then
                If Not Regex.IsMatch(o_txt_others.Text, "^(([a-zA-Z ]+[:][1-9][0-9]{1,4})+([\,])*)*$") Then Return "Di�er servisler alanlar�n� hatal� girdiniz"
            End If

            If o_rdb_domain1.Checked = True Then
                If o_txt_domain.Text.Contains(" ") Then Return "Girdi�iniz domain �n eki hatal�"
                If Not Regex.IsMatch(o_txt_domain.Text.Trim, "^[0-9a-z]+$") Then Return "Girdi�iniz domain �n eki hatal�"
            End If

            If o_ddl_pctur.SelectedValue = 3 And o_txt_aciklama.Text <> "" Then
                If Not Regex.IsMatch(o_txt_aciklama.Text, "^[a-zA-Z������������ \,\.\:]+$") Then Return "A��klama alan�ndaki bilgileriniz hatal�"
            End If

            If o_ddl_pctur.SelectedValue = 2 Then
                'If Not Regex.IsMatch(o_txt_pcno.Text.Trim, "^[1-9][0-9]{1,2}$") Then Return "Bilgisayar numaras�n� hatal� girdiniz"
                If Not Regex.IsMatch(o_txt_pcno.Text.Trim, "^[1-9][0-9]*$") Then Return "Bilgisayar numaras�n� hatal� girdiniz"
            End If

            Return "OK@$REGEX"
        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "��lem s�ras�nda hata olu�tu. Hata kodu : 300107"
            Return "!OK@$REGEX"
        End Try
    End Function
#End Region
#Region "Ba�ka bilgisayar MAC adres tespiti"
    Protected Sub o_btn_getmac_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_btn_getmac.Click
        If o_txt_ip.Text = "" Then
            lblUyari.Visible = True
            lblUyari.Text = "IP adresini bo� b�rakt�n�z."
            Exit Sub
        End If

        Session("manuelIP") = o_txt_ip.Text
        macAdres_getir(True)
        If Session("manuelMAC") <> "INVALIDMAC" Then
            o_lbl_result.Text = Session("manuelMAC")
        Else
            o_lbl_result.Text = "��lem ba�ar�s�z.Belirtti�iniz bilgisayara eri�ilemiyor. Hata kodu : 300103"
        End If
    End Sub
#End Region
#Region "Laboratuarlar"
    Private Sub labs()
        Try
            Dim command As New SqlCommand("stp_LABS", conn)
            command.CommandType = CommandType.StoredProcedure

            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)
            Dim dt As New DataTable()
            If conn.State <> ConnectionState.Open Then conn.Open()
            Adapter.Fill(dt)
            If conn.State = ConnectionState.Open Then conn.Close()

            o_ddl_lab.DataSource = dt
            o_ddl_lab.DataTextField = "LAB_AD"
            o_ddl_lab.DataValueField = "ID"
            o_ddl_lab.DataBind()
        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "��lem s�ras�nda hata olu�tu. Hata kodu : 300112"
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try
    End Sub
#End Region

#Region "Gridview Databound"
    Protected Sub o_grid_macs_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_grid_macs.DataBound
        Try
            Dim gridRowCount As Integer = o_grid_macs.Rows.Count
            For i As Integer = 0 To gridRowCount - 1

                If o_grid_macs.Rows(i).Cells(2).Text = 0 Then
                    o_grid_macs.Rows(i).Cells(2).Text = "Sabit kullan�c�"
                    CType(o_grid_macs.Rows(i).FindControl("img_pctur"), Image).ImageUrl = "images/img_cihaz/private.gif"

                ElseIf o_grid_macs.Rows(i).Cells(2).Text = 1 Then
                    o_grid_macs.Rows(i).Cells(2).Text = "Hareketli kullan�c�"
                    CType(o_grid_macs.Rows(i).FindControl("img_pctur"), Image).ImageUrl = "images/img_cihaz/mobile.png"

                ElseIf o_grid_macs.Rows(i).Cells(2).Text = 2 Then
                    o_grid_macs.Rows(i).Cells(2).Text = "Ortak kullan�m"
                    CType(o_grid_macs.Rows(i).FindControl("img_pctur"), Image).ImageUrl = "images/img_cihaz/public.gif"

                ElseIf o_grid_macs.Rows(i).Cells(2).Text = 3 Then
                    o_grid_macs.Rows(i).Cells(2).Text = "Sunucu"
                    CType(o_grid_macs.Rows(i).FindControl("img_pctur"), Image).ImageUrl = "images/img_cihaz/server.png"

                ElseIf o_grid_macs.Rows(i).Cells(2).Text = 4 Then
                    o_grid_macs.Rows(i).Cells(2).Text = "Misafir"
                    CType(o_grid_macs.Rows(i).FindControl("img_pctur"), Image).ImageUrl = "images/img_cihaz/quest.gif"
                ElseIf o_grid_macs.Rows(i).Cells(2).Text = 5 Then
                    o_grid_macs.Rows(i).Cells(2).Text = "Di�er cihazlar"
                    CType(o_grid_macs.Rows(i).FindControl("img_pctur"), Image).ImageUrl = "images/img_cihaz/others.png"
                End If

                If o_grid_macs.DataKeys(i)(1) = "False" Then
                    CType(o_grid_macs.Rows(i).FindControl("img_aktif"), ImageButton).ImageUrl = "images/img_cihaz/red.gif"
                    CType(o_grid_macs.Rows(i).FindControl("img_aktif"), ImageButton).AlternateText = "Bu k�s�mdan aktifle�tirdi�iniz cihaz B�M onay�n� bekler"
                Else
                    CType(o_grid_macs.Rows(i).FindControl("img_aktif"), ImageButton).ImageUrl = "images/img_cihaz/onay.gif"
                End If

                If o_grid_macs.DataKeys(i)(2) = "False" Then
                    CType(o_grid_macs.Rows(i).FindControl("img_onay"), Image).ImageUrl = "images/img_cihaz/red.gif"
                Else
                    CType(o_grid_macs.Rows(i).FindControl("img_onay"), Image).ImageUrl = "images/img_cihaz/onay.gif"
                End If

                If o_grid_macs.DataKeys(i)(3) = Session("macAdresi") Then
                    CType(o_grid_macs.Rows(i).FindControl("img_thispc"), Image).Visible = True
                    'o_grid_macs.Rows(i).BackColor = Drawing.ColorTranslator.FromHtml("#e1e9fc")
                    'o_grid_macs.Rows(i).BackColor = Drawing.Color.DarkSlateGray
                    'If o_grid_macs.DataKeys(i)(1) = "True" And o_grid_macs.DataKeys(i)(4) = 0 Then
                    '    Session("yetki_pc") = False
                    'ElseIf o_grid_macs.DataKeys(i)(1) = "True" And o_grid_macs.DataKeys(i)(4) = 1 Then
                    '    Session("yetki_hareketli") = False
                    'ElseIf o_grid_macs.DataKeys(i)(1) = "True" And o_grid_macs.DataKeys(i)(4) = 2 Then
                    '    Session("yetki_lab") = False
                    'ElseIf o_grid_macs.DataKeys(i)(1) = "True" And o_grid_macs.DataKeys(i)(4) = 3 Then
                    '    Session("yetki_sunucu") = False
                    'ElseIf o_grid_macs.DataKeys(i)(1) = "True" And o_grid_macs.DataKeys(i)(4) = 4 Then
                    '    Session("yetki_misafir") = False
                    'End If
                End If
            Next

            If o_lbl_yetki.Text = "" Then o_lbl_info.Visible = False
        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "��lem s�ras�nda hata olu�tu. Hata kodu : 300108"
        End Try
    End Sub

    Protected Sub o_grid_msf_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_grid_msf.DataBound
        Try
            Dim gridRowCount As Integer = o_grid_msf.Rows.Count
            For i As Integer = 0 To gridRowCount - 1
                If o_grid_msf.DataKeys(i)(1) = "False" Then
                    CType(o_grid_msf.Rows(i).FindControl("img_aktif"), ImageButton).ImageUrl = "images/img_cihaz/red.gif"
                Else
                    CType(o_grid_msf.Rows(i).FindControl("img_aktif"), ImageButton).ImageUrl = "images/img_cihaz/onay.gif"
                End If
            Next
        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "��lem s�ras�nda hata olu�tu. Hata kodu : 300108"
        End Try
    End Sub
#End Region
#Region "Gridview Aktif butonu "
    Protected Sub img_aktif_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        lblUyari.Visible = True
        Try
            Dim imgButton As ImageButton = CType(sender, ImageButton)
            Dim grow As GridViewRow = CType(imgButton.Parent.Parent, GridViewRow)
            Dim rowIndex As Integer = grow.RowIndex
            'Dim aktif As Boolean

            If o_grid_macs.DataKeys(rowIndex)(4) = 4 Or o_grid_macs.DataKeys(rowIndex)(4) = 0 Then
                'If o_grid_macs.DataKeys(rowIndex)(1) = "False" Then
                'aktif = True
                'ElseIf o_grid_macs.DataKeys(rowIndex)(1) = "True" Then
                'aktif = False
                'End If

                '    If aktif = True Then
                '        pcleri_say()
                '        If Not yetkilist(o_grid_macs.DataKeys(rowIndex)(4)) >(0 T�en
                '            lblUyari.Visible = True
   �            '            lblUyari.Text = "Yetkilerinizde belirlenenden fazla say�da bil�isayar�n veya cihaz�n kayd�n� aktiv edemezsiniz."
 "          (   '            Exit Sub
    (           '        End If
                '    End If

                Dim command As New SqlCommand("stp_IPMAC_DEGISTIR", conn)
                command.CommandType = Data.CommandType.StoredProcedure

                With command.Parameters
                    .AddWithValue("ID", o_grid_macs.DataKeys(rowIndex)(0))
                    .AddWithValue("AKTIF", False)
                    .AddWithValue("CURRENT_IP", CType(Session("ipno"), String))
                    .AddWithValue("CURRENT_MAC", CType(Session("macAdresi"), String))
                    .AddWithValue("KISI_NO", CType(Session("kisino"), Long))
                End With

                returnValue = New SqlParameter("RETURNVALUE", Data.SqlDbType.Int)
                returnValue.Direction = Data.ParameterDirection.ReturnValue
                command.Parameters.Add(returnValue)

                If conn.State <> Data.ConnectionState.Open Then conn.Open()
                command.ExecuteNonQuery()
                If conn.State = Data.ConnectionState.Open Then conn.Close()

                lblUyari.Visible = True
                If returnValue.Value = 902 Or returnValue.Value = 904 Then
                    lblUyari.Text = "<img src=""images/img_cihaz/time.png"">" & "Yeni yapt���n�z "" kay�t / kay�t aktif etme / kay�t pasif etme "" i�lemleri aras�nda belli bir s�re ge�mesi gerekmektedir"
                    '    ElseIf returnValue.Value = 911 Then
                    '        lblUyari.Text = "<img src=""images/img_cihaz/warning.png"">" & "Aktif hale getirmeye �al��t���n�z bilgisayar veya cihaz i�in aktif durumda bir kay�t zaten var.<br>"
                    '        lblUyari.Text &= "E�er bu bilgisayar� veya cihaz�, ba�ka bir bilgisayar veya cihaz t�r�nde kaydetmek istiyorsan�z �nceki kayd� pasif hale getirmelisiniz."
                    '    ElseIf returnValue.Value = 913 Then
                    '        lblUyari.Text = "<img src=""images/img_cihaz/restrict.png"">" & "Sunucu olarak kaydedilmi� bilgisayarlar buradan aktif edilemez.<br>"
                    '        lblUyari.Text &= "Bu sunucuyu aktif hale getirmek i�in Bilgi ��lem ile irtibata ge�iniz."
                    '    ElseIf returnValue.Value = 999 Then
                    '        lblUyari.Text = "<img src=""images/img_cihaz/unmatch.png"">" & "Bu bilgisayar sizin ad�n�za kaydedilmi�tir ancak �uan kulland���n�z bilgisayar de�ildir.<br>"
                    '        lblUyari.Text &= "Bu bilgisayar�n kayd�n�, ancak ayn� bilgisayar� kullanarak pasif veya aktif hale getirebilirsiniz"

                ElseIf returnValue.Value = 101 Then
                    lblUyari.Text = "<img src=""images/img_cihaz/warning.png"">" & "Se�ti�iniz bilgisayar�n kayd� pasif hale getirilmi�tir, internet eri�imi bir s�re sonra kapanacak"
                ElseIf returnValue.Value = 103 Then
                    lblUyari.Text = "<img src=""images/img_cihaz/warning.png"">" & "Se�ti�iniz sunucunun kayd� pasif hale getirilmi�tir, internet eri�imi bir s�re sonra kapanacak"
                ElseIf returnValue.Value = 105 Then
                    lblUyari.Text = "<img src=""images/img_cihaz/warning.png"">" & "Se�ti�iniz cihaz�n kayd� pasif hale getirilmi�tir, internet eri�imi bir s�re sonra kapanacak"

                ElseIf returnValue.Value = 111 Then
                    lblUyari.Text = "<img src=""images/img_cihaz/warning.png"">" & "Se�ti�iniz bilgisayar�n kayd� aktif hale getirilmi�tir, internet eri�imi bir s�re sonra a��lacak"
                ElseIf returnValue.Value = 115 Then
                    lblUyari.Text = "<img src=""images/img_cihaz/warning.png"">" & "Se�ti�iniz cihaz�n kayd� aktif hale getirilmi�tir, internet eri�imi bir s�re sonra a��lacak"
                End If

                If returnValue.Value < 200 Then pcleri_getir()
                If returnValue.Value < 200 Then pcleri_say()
                If returnValue.Value < 200 Then
                    populate_pcturlist()
                End If
            Else
                lblUyari.Text = "Aktif halde kay�tl� bulunan cihaz�n�z� veya bilgisayar�n�z� pasif hale getirmek i�in, <br> Bilgi ��lemi aray�n�z."
            End If
        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "��lem s�ras�nda hata olu�tu. Hata kodu : 300109"
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
            yetkileri_listele()
            defaultControls()
            iplistesi_olustur()
            dis_iplistesi()
            cihaz_iplistesi_olustur()
        End Try
    End Sub
#End Region
#Region "Temizle"
    Private Sub cleanse()
        Session("manuelMAC") = ""

        o_txt_ip.Text = ""
        o_lbl_result.Text = ""
        o_txt_others.Text = ""
        o_txt_aciklama.Text = ""
        o_txt_domain.Text = ""
        o_txt_pcno.Text = ""
        o_txt_msfad.Text = ""
        o_txt_msfsoyad.Text = ""

        o_chk_web.Checked = False
        o_chk_sweb.Checked = False
        o_chk_remote.Checked = False
        o_chk_ssh.Checked = False
        o_rdb_pc1.Checked = True
        o_rdb_pc2.Checked = False
        o_rdb_domain2.Checked = True
        o_chk_toc.Checked = False
        o_chk_msftoc.Checked = False
        o_btn_svmac.Enabled = False
        o_ddl_lab.SelectedIndex = -1
        o_ddl_time.SelectedIndex = -1
    End Sub
#End Region
End Class
'Sayfa hata kodu : 300 & Hatakodu
'101 - Kay�tl� pcleri getirmede hata (KISINO ve MAC e g�re)
'102 - Kay�tl� ve aktif pcleri getirmede hata (KISINO ya g�re)
'103 - MAC adresi bulmada hata
'104 - Yeni bilgisayar kayd�nda hata
'105 - Varsay�lan kontrollerde hata
'106 - Sunucular i�in IP listesi olu�turmada hata
'107 - Alan kontrollerinde hata
'108 - Gridview databound hatas�
'109 - Kay�tl� PC aktif/pasif yapma hatas�
'110 - Ge�ersiz karakter kullan�m�
'111 - Ge�ersiz IP
'112 - Labs gelemedi
