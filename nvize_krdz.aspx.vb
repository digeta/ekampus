﻿Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class nvize_krdz
    Inherits System.Web.UI.Page

    Private conn As New SqlConnection(ConfigurationManager.AppSettings("conn1"))
    Private ddlVizeSay_degisen As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("kisino") = 0 Or Session("kisino") Is Nothing Then
            Response.Redirect("Login.aspx", True)
            Exit Sub
        End If

        If Session("parola_zorla") = True Then
            Response.Redirect("Pass.aspx")
            Exit Sub
        End If

        'Response.Redirect("Default.aspx")
        'Exit Sub

        lblUyari.Visible = False
        lblUyari.Text = ""

        lblDersKodu.Text = CType(Session("DERSKOD"), String) & " - " & CType(Session("DERSAD"), String)

        If Page.IsPostBack = False Then
            notlarGetir()
        End If

        notKaydet.Attributes.Add("onclick", "alert('Notları Onayla butonunu kullanarak notları onaylamadığınız sürece" & _
                                 " notlar öğrenciler tarafından görüntülenemeyecektir!');")

        notKaydet2.Attributes.Add("onclick", "alert('Notları Onayla butonunu kullanarak notları onaylamadığınız sürece" & _
                                 " notlar öğrenciler tarafından görüntülenemeyecektir!');")
    End Sub

    Private Sub notlarGetir()
        Try
            Dim command As New SqlCommand("stp_YNS_VIZE_NOTLAR_GETIR", conn)
            command.CommandType = Data.CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("ATANAN_ID", Session("ATANAN_ID"))
                .AddWithValue("YIL", Session("YIL"))
                .AddWithValue("DONEM", Session("DONEM"))
                .AddWithValue("KATID", Session("KATID"))
            End With

            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)

            Dim dt As New DataTable

            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            Adapter.Fill(dt)
            If conn.State = Data.ConnectionState.Open Then conn.Close()

            Session("DT_NOTLAR") = dt
            grid_notliste.DataSource = dt
            grid_notliste.DataBind()

            'If Not dt.Rows.Count > 0 Then divDersler.Visible = False Else divDersler.Visible = True
        Catch ex As Exception
            lblUyari.Visible = True
            'lblUyari.Text = "Bir hata oluştu.Bilgi İşlem' e başvurunuz."
            lblUyari.Text = ex.Message
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub notToparla(ByVal kayit As Boolean)
        Dim rowCount As Integer = grid_notliste.Rows.Count

        If rowCount > 0 Then
            Try
                Dim dt As New DataTable
                dt = CType(Session("DT_NOTLAR"), DataTable)

                Dim idler As New StringBuilder

                Dim vizeler1 As New StringBuilder
                Dim vizeler2 As New StringBuilder
                Dim vizeler3 As New StringBuilder
                Dim vizeler4 As New StringBuilder
                Dim vizeler5 As New StringBuilder
                Dim vizeler6 As New StringBuilder
                Dim vizeler7 As New StringBuilder
                Dim vizeler8 As New StringBuilder
                Dim vizeler9 As New StringBuilder
                Dim vizeler10 As New StringBuilder

                Dim devamlar As New StringBuilder
                Dim vizedurumlar As New StringBuilder
                Dim atananIdler As New StringBuilder
                Dim vizeOrtlar As New StringBuilder
                Dim bn_degerlist As New StringBuilder

                Dim id As Long = 0

                Dim vize1 As String = ""
                Dim vize2 As String = ""
                Dim vize3 As String = ""
                Dim vize4 As String = ""
                Dim vize5 As String = ""
                Dim vize6 As String = ""
                Dim vize7 As String = ""
                Dim vize8 As String = ""
                Dim vize9 As String = ""
                Dim vize10 As String = ""

                'Dim vize_not As String = ""
                Dim devam As Boolean = False
                Dim vizedurum As Boolean = False
                Dim vizeOrt As Integer = 0
                Dim atananId As Integer = Session("ATANAN_ID")
                Dim bn_deger As Integer = 0

                For i As Integer = 0 To rowCount - 1
                    If idler.Length > 0 Then
                        idler.Append(",")
                        atananIdler.Append(",")
                        devamlar.Append(",")
                        vizedurumlar.Append(",")
                        vizeOrtlar.Append(",")
                        bn_degerlist.Append(",")

                        vizeler1.Append(",")
                        vizeler2.Append(",")
                        vizeler3.Append(",")
                        vizeler4.Append(",")
                        vizeler5.Append(",")
                        vizeler6.Append(",")
                        vizeler7.Append(",")
                        vizeler8.Append(",")
                        vizeler9.Append(",")
                        vizeler10.Append(",")
                    End If


                    id = DirectCast(grid_notliste.DataKeys(i)("ID"), Long)
                    bn_deger = CType(grid_notliste.DataKeys(i)("BN_DEGERLENDIR"), Integer)

                    devam = DirectCast(grid_notliste.Rows(i).FindControl("ddlDevam"), DropDownList).SelectedValue
                    vizedurum = DirectCast(grid_notliste.Rows(i).FindControl("ddlAraSinav"), DropDownList).SelectedValue

                    If vizedurum = False Then
                        atananId = Session("ATANAN_ID")
                        vizeOrt = 0
                    End If

                    If devam = False Then
                        atananId = Session("ATANAN_ID")
                        vizeOrt = 0
                    End If

                    If devam = True And vizedurum = True Then
                        vize1 = DirectCast(grid_notliste.Rows(i).FindControl("ddlV"), DropDownList).SelectedValue
                        If vize1 = -1 Then
                            atananId = 0
                        Else
                            vizeOrt = vize1
                            atananId = Session("ATANAN_ID")
                        End If
                    End If


                    If kayit = True Then
                        idler.Append(id)
                        devamlar.Append(devam)
                        vizedurumlar.Append(vizedurum)
                        vizeOrtlar.Append(vizeOrt)
                        atananIdler.Append(atananId)
                        bn_degerlist.Append(bn_deger)

                        If devam = True And vizedurum = True Then
                            vizeler1.Append(vize1)
                            vizeler2.Append(-1)
                            vizeler3.Append(-1)
                            vizeler4.Append(-1)
                            vizeler5.Append(-1)
                            vizeler6.Append(-1)
                            vizeler7.Append(-1)
                            vizeler8.Append(-1)
                            vizeler9.Append(-1)
                            vizeler10.Append(-1)
                        Else
                            vizeler1.Append("-1")
                            vizeler2.Append("-1")
                            vizeler3.Append("-1")
                            vizeler4.Append("-1")
                            vizeler5.Append("-1")
                            vizeler6.Append("-1")
                            vizeler7.Append("-1")
                            vizeler8.Append("-1")
                            vizeler9.Append("-1")
                            vizeler10.Append("-1")
                        End If
                    End If

                    If kayit = False Then
                        If devam = True And vizedurum = True Then
                            dt.Rows(i)("V1") = vize1
                            dt.Rows(i)("V2") = -1
                            dt.Rows(i)("V3") = -1
                            dt.Rows(i)("V4") = -1
                            dt.Rows(i)("V5") = -1
                            dt.Rows(i)("V6") = -1
                            dt.Rows(i)("V7") = -1
                            dt.Rows(i)("V8") = -1
                            dt.Rows(i)("V9") = -1
                            dt.Rows(i)("V10") = -1
                        End If

                        dt.Rows(i)("DEVAM_DURUM") = devam
                        dt.Rows(i)("VIZE_DURUM") = vizedurum
                    End If
                Next

                Session("DT_NOTLAR") = dt
                grid_notliste.DataSource = dt
                grid_notliste.DataBind()

                If kayit = True Then
                    notlarKaydet(atananIdler.ToString, idler.ToString, vizeOrtlar.ToString, devamlar.ToString, _
                                  vizeler1.ToString, _
                                  vizeler2.ToString, _
                                  vizeler3.ToString, _
                                  vizeler4.ToString, _
                                  vizeler5.ToString, _
                                  vizeler6.ToString, _
                                  vizeler7.ToString, _
                                  vizeler8.ToString, _
                                  vizeler9.ToString, _
                                  vizeler10.ToString, _
                                  vizedurumlar.ToString, bn_degerlist.ToString, 1)
                End If

            Catch ex As InvalidCastException
                lblUyari.Visible = True
                lblUyari.Text = "Not alanlarına sadece rakam girmelisiniz."
            Catch ex As Exception
                lblUyari.Visible = True
                lblUyari.Text = "Bir hata oluştu.Bilgi İşlem' e başvurunuz."
                'lblUyari.Text = ex.Message
            End Try
        End If
    End Sub

    Public Function SafeDivide(ByVal dbl1 As Double, ByVal dbl2 As Double) As Double
        If (dbl1 = 0) Or (dbl2 = 0) Then Return 0 Else Return dbl1 / dbl2
    End Function

    Private Sub notlarKaydet(ByVal atananIdler As String, ByVal idler As String, ByVal vizeOrtlar As String, ByVal devamlar As String, _
                             ByVal vizeler1 As String, _
                             ByVal vizeler2 As String, _
                             ByVal vizeler3 As String, _
                             ByVal vizeler4 As String, _
                             ByVal vizeler5 As String, _
                             ByVal vizeler6 As String, _
                             ByVal vizeler7 As String, _
                             ByVal vizeler8 As String, _
                             ByVal vizeler9 As String, _
                             ByVal vizeler10 As String, _
                             ByVal vizedurumlar As String, ByVal bn_degerlist As String, ByVal vizeSay As Integer)

        Session("KAYIT_OK") = False

        Try
            Dim command As New SqlCommand("stp_YNS_VIZE_NOTLAR_GIR", conn)
            command.CommandType = Data.CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("ATANAN_IDLER", atananIdler)
                .AddWithValue("IDLER", idler)
                .AddWithValue("VIZELER1", vizeler1)
                .AddWithValue("VIZELER2", vizeler2)
                .AddWithValue("VIZELER3", vizeler3)
                .AddWithValue("VIZELER4", vizeler4)
                .AddWithValue("VIZELER5", vizeler5)
                .AddWithValue("VIZELER6", vizeler6)
                .AddWithValue("VIZELER7", vizeler7)
                .AddWithValue("VIZELER8", vizeler8)
                .AddWithValue("VIZELER9", vizeler9)
                .AddWithValue("VIZELER10", vizeler10)
                .AddWithValue("DEVAMLAR", devamlar)
                .AddWithValue("VIZEDURUMLAR", vizedurumlar)
                .AddWithValue("VIZE_ORTLAR", vizeOrtlar)
                .AddWithValue("BN_DEGERLIST", bn_degerlist)
                .AddWithValue("VIZE_SAYISI", vizeSay)
                .AddWithValue("KISINO", CType(Session("kisino"), Long))
                .AddWithValue("KATID", Session("KATID"))
                .AddWithValue("IP", Session("ipno"))
            End With

            Dim returnValue As SqlParameter
            returnValue = New SqlParameter("RETURNVALUE", Data.SqlDbType.Int)
            returnValue.Direction = Data.ParameterDirection.ReturnValue
            command.Parameters.Add(returnValue)


            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            command.ExecuteNonQuery()
            If conn.State = Data.ConnectionState.Open Then conn.Close()


            If returnValue.Value = 100 Then
                lblUyari.Visible = True
                lblUyari.Text = "Notlar başarıyla kaydedildi."
                Session("KAYIT_OK") = True
            Else
                lblUyari.Visible = True
                lblUyari.Text = "Hata oluştu. Notlar kaydedilemedi!"
            End If

        Catch ex As SqlException
            lblUyari.Visible = True
            lblUyari.Text = "Hata oluştu. Notlar kaydedilemedi!"
        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "Bir hata oluştu.Bilgi İşlem' e başvurunuz."
            'lblUyari.Text = ex.Message
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
            If Session("KAYIT_OK") = True And Session("ONAYVAR") = True Then
                notOnayla()
            End If
            notlarGetir()
        End Try
    End Sub

    Public Function vizeKontrol() As Integer
        'If grid_notliste.DataKeys(grid_notliste.Rows.Count)("V1") = -1 Then
        'Return ""
        'Else
        Return Eval("V1")
        'End If
    End Function

    Protected Sub imgb_notKaydet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles notKaydet.Click, notKaydet2.Click
        'If Session("VIZE_ONAY") = False Then
        notToparla(True)
        'Else
        'lblUyari.Visible = True
        'lblUyari.Text = "Daha önce notlara onay verme işlemi yapılmış.Bu yüzden kayıtları değiştiremezsiniz.</br>Bilgi İşlem ile iletişime geçiniz."
        'notlarGetir()
        'End If
    End Sub

    Protected Sub notOnay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles notOnay.Click, notOnay2.Click
        divNotOnay1.Visible = True
        divNotOnay2.Visible = True
    End Sub

    Private Sub notOnayla()
        Try
            Dim command As New SqlCommand("stp_YNS_NOT_ONAY", conn)
            command.CommandType = Data.CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("ATANAN_ID", Session("ATANAN_ID"))
                .AddWithValue("NOTTYPE", "Vize")
            End With

            Dim returnValue As SqlParameter
            returnValue = New SqlParameter("RETURNVALUE", Data.SqlDbType.Int)
            returnValue.Direction = Data.ParameterDirection.ReturnValue
            command.Parameters.Add(returnValue)

            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            command.ExecuteNonQuery()
            If conn.State = Data.ConnectionState.Open Then conn.Close()

            If returnValue.Value = 100 Then
                lblUyari.Visible = True
                lblUyari.Text = "Notlar Onaylandı."
                divNotOnay1.Visible = False
                divNotOnay2.Visible = False
                notlarGetir()
                Session("VIZE_ONAY") = True
            Else
                lblUyari.Visible = True
                lblUyari.Text = "Hata oluştu. Notlar Onaylanamadı!"
                divNotOnay1.Visible = False
                divNotOnay2.Visible = False
                notlarGetir()
                Session("VIZE_ONAY") = False
            End If

        Catch ex As SqlException
            lblUyari.Visible = True
            lblUyari.Text = "Onay verme işlemi sırasında bir hata oluştu."
        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "Bir hata oluştu.Bilgi İşlem' e başvurunuz."
            'lblUyari.Text = ex.Message
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
            Session("ONAYVAR") = False
        End Try
    End Sub

    Protected Sub btnOnayYok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOnayYok.Click, btnOnayYok2.Click
        divNotOnay1.Visible = False
        divNotOnay2.Visible = False
        Session("ONAYVAR") = False
    End Sub

    Protected Sub btnOnayVar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOnayVar.Click, btnOnayVar2.Click
        'If Session("VIZE_ONAY") = False Then
        Session("ONAYVAR") = True
        notToparla(True)
        'Else
        'lblUyari.Visible = True
        'lblUyari.Text = "Daha önce notlara onay verme işlemi yapılmış.Bu yüzden kayıtları değiştiremezsiniz.</br>Bilgi İşlem ile iletişime geçiniz."
        'notlarGetir()
        'End If
    End Sub
End Class
