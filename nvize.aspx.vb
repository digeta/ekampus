Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class nvize
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

        'If Session("VIZE_ONAY") = True Then
        '    notKaydet.Visible = False
        '    notOnay.Visible = False
        '    notKaydet2.Visible = False
        '    notOnay2.Visible = False
        'End If

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

                        Dim dersSayi As Integer = 10
                        vizeOrt = 0

                        vize1 = DirectCast(grid_notliste.Rows(i).FindControl("txtV1"), TextBox).Text.Trim
                        If vize1 = String.Empty Or IsNumeric(vize1) = False Then
                            vize1 = -1
                            dersSayi -= 1
                        Else
                            vizeOrt += vize1
                        End If

                        vize2 = DirectCast(grid_notliste.Rows(i).FindControl("txtV2"), TextBox).Text.Trim
                        If vize2 = String.Empty Or IsNumeric(vize2) = False Or ddlVizeSay.SelectedValue < 2 Then
                            vize2 = -1
                            dersSayi -= 1
                        Else
                            vizeOrt += vize2
                        End If

                        vize3 = DirectCast(grid_notliste.Rows(i).FindControl("txtV3"), TextBox).Text.Trim
                        If vize3 = String.Empty Or IsNumeric(vize3) = False Or ddlVizeSay.SelectedValue < 3 Then
                            vize3 = -1
                            dersSayi -= 1
                        Else
                            vizeOrt += vize3
                        End If

                        vize4 = DirectCast(grid_notliste.Rows(i).FindControl("txtV4"), TextBox).Text.Trim
                        If vize4 = String.Empty Or IsNumeric(vize4) = False Or ddlVizeSay.SelectedValue < 4 Then
                            vize4 = -1
                            dersSayi -= 1
                        Else
                            vizeOrt += vize4
                        End If

                        vize5 = DirectCast(grid_notliste.Rows(i).FindControl("txtV5"), TextBox).Text.Trim
                        If vize5 = String.Empty Or IsNumeric(vize5) = False Or ddlVizeSay.SelectedValue < 5 Then
                            vize5 = -1
                            dersSayi -= 1
                        Else
                            vizeOrt += vize5
                        End If

                        vize6 = DirectCast(grid_notliste.Rows(i).FindControl("txtV6"), TextBox).Text.Trim
                        If vize6 = String.Empty Or IsNumeric(vize6) = False Or ddlVizeSay.SelectedValue < 6 Then
                            vize6 = -1
                            dersSayi -= 1
                        Else
                            vizeOrt += vize6
                        End If

                        vize7 = DirectCast(grid_notliste.Rows(i).FindControl("txtV7"), TextBox).Text.Trim
                        If vize7 = String.Empty Or IsNumeric(vize7) = False Or ddlVizeSay.SelectedValue < 7 Then
                            vize7 = -1
                            dersSayi -= 1
                        Else
                            vizeOrt += vize7
                        End If

                        vize8 = DirectCast(grid_notliste.Rows(i).FindControl("txtV8"), TextBox).Text.Trim
                        If vize8 = String.Empty Or IsNumeric(vize8) = False Or ddlVizeSay.SelectedValue < 8 Then
                            vize8 = -1
                            dersSayi -= 1
                        Else
                            vizeOrt += vize8
                        End If

                        vize9 = DirectCast(grid_notliste.Rows(i).FindControl("txtV9"), TextBox).Text.Trim
                        If vize9 = String.Empty Or IsNumeric(vize9) = False Or ddlVizeSay.SelectedValue < 9 Then
                            vize9 = -1
                            dersSayi -= 1
                        Else
                            vizeOrt += vize9
                        End If

                        vize10 = DirectCast(grid_notliste.Rows(i).FindControl("txtV10"), TextBox).Text.Trim
                        If vize10 = String.Empty Or IsNumeric(vize10) = False Or ddlVizeSay.SelectedValue < 10 Then
                            vize10 = -1
                            dersSayi -= 1
                        Else
                            vizeOrt += vize10
                        End If

                        If dersSayi = 0 Then
                            atananId = 0
                        ElseIf dersSayi > 0 Then
                            atananId = Session("ATANAN_ID")
                        End If

                        vizeOrt = Math.Round(SafeDivide(vizeOrt, dersSayi), MidpointRounding.AwayFromZero)

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
                            vizeler2.Append(vize2)
                            vizeler3.Append(vize3)
                            vizeler4.Append(vize4)
                            vizeler5.Append(vize5)
                            vizeler6.Append(vize6)
                            vizeler7.Append(vize7)
                            vizeler8.Append(vize8)
                            vizeler9.Append(vize9)
                            vizeler10.Append(vize10)
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
                            dt.Rows(i)("V2") = vize2
                            dt.Rows(i)("V3") = vize3
                            dt.Rows(i)("V4") = vize4
                            dt.Rows(i)("V5") = vize5
                            dt.Rows(i)("V6") = vize6
                            dt.Rows(i)("V7") = vize7
                            dt.Rows(i)("V8") = vize8
                            dt.Rows(i)("V9") = vize9
                            dt.Rows(i)("V10") = vize10
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
                                  vizedurumlar.ToString, bn_degerlist.ToString, ddlVizeSay.SelectedValue)
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

    Public Function vizeKontrol(ByVal num As Integer) As String
        If grid_notliste.DataKeys(grid_notliste.Rows.Count)("V" & num) = -1 Then
            Return ""
        Else
            Return Eval("V" & num)
        End If
    End Function

    Public Function onayKontrol(ByVal num As Integer) As String
        'If Session("VIZE_ONAY") = True Then
        'Return True
        'Else
        Return False
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

    Protected Sub ddlVizeSay_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlVizeSay.SelectedIndexChanged
        ddlVizeSay_degisen = 1
        ddlVizeSay2.SelectedValue = ddlVizeSay.SelectedValue
        notToparla(False)
    End Sub

    Protected Sub ddlVizeSay2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlVizeSay2.SelectedIndexChanged
        ddlVizeSay_degisen = 2
        ddlVizeSay.SelectedValue = ddlVizeSay2.SelectedValue
        notToparla(False)
    End Sub

    Protected Sub grid_notliste_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grid_notliste.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Page.IsPostBack = False Then
                ddlVizeSay.SelectedValue = grid_notliste.DataKeys(0)("VIZE_SAYISI")
                ddlVizeSay2.SelectedValue = grid_notliste.DataKeys(0)("VIZE_SAYISI")
            End If

            Dim vizeSay As Integer = 0
            If ddlVizeSay_degisen = 1 Then
                vizeSay = ddlVizeSay.SelectedValue
            Else
                vizeSay = ddlVizeSay2.SelectedValue
            End If

            If vizeSay > 1 Then
                For i As Integer = 2 To vizeSay
                    CType(e.Row.Cells(3 + i).FindControl("txtV" & i), TextBox).Enabled = True
                    CType(e.Row.Cells(3 + i).FindControl("txtV" & i), TextBox).BackColor = Drawing.Color.White
                Next
            End If
        End If
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



