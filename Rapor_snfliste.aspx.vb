Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Reporting
Imports Telerik.Reporting.Drawing
Imports Telerik.Reporting.Processing


Partial Class Rapor_snfliste
    Inherits System.Web.UI.Page

    Private conn As New SqlConnection(ConfigurationManager.AppSettings("conn1"))

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("kisino") = 0 Or Session("kisino") Is Nothing Then
            Response.Redirect("Login.aspx", True)
            Exit Sub
        End If

        'Session("kisino") = 3204
        'Session("adsoyad") = ""

        'If Session("parola_zorla") = True Then
        '    Response.Redirect("Pass.aspx")
        '    Exit Sub
        'End If

        'Session("kisino") = 30683

        'lblUyari.Visible = True
        'lblUyari.Text = "<a href=""http://server.karaelmas.edu.tr/ekamp_test/Login.aspx"">Staj not giriş ve döküm işlemleri için buraya tıklayın, bu sayfada yeniden giriş yapmanız gerekecektir.</a>"
        If Page.IsPostBack = False Then
            derslerGetir()
        End If
    End Sub

    Private Sub derslerGetir()
        Try
            Dim command As New SqlCommand("stp_YNS_ATANAN_DERSLER", conn)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("ATANANSICIL", Session("kisino"))
            End With

            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)

            Dim dt As New DataTable

            If conn.State <> ConnectionState.Open Then conn.Open()
            Adapter.Fill(dt)
            If conn.State = ConnectionState.Open Then conn.Close()

            Session("DT_NOTLAR") = dt
            gridDersler.DataSource = dt
            gridDersler.DataBind()

            'If Not dt.Rows.Count > 0 Then divDersler.Visible = False Else divDersler.Visible = True
        Catch ex As Exception
            lblUyari.Visible = True
            lblUyari.Text = "Bir hata oluştu.Bilgi İşlem' e başvurunuz."
            'lblUyari.Text = ex.Message
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Sub ExportToPDF(ByVal reportToExport As Telerik.Reporting.Report)
        Try
            Dim reportProcessor As New ReportProcessor()
            Dim result As RenderingResult = reportProcessor.RenderReport("PDF", reportToExport, Nothing)

            'Dim fileName As String = result.DocumentName + ".pdf"
            Response.Clear()
            Response.ContentType = result.MimeType
            Response.Cache.SetCacheability(HttpCacheability.Private)
            Response.Expires = -1
            Response.Buffer = True
            'Response.AddHeader("Content-Disposition", String.Format("{0};FileName=""{1}""", "attachment", fileName))
            Dim rptFileName As String = Session("DERS_BIRIM") & "_" & Session("DERSKOD") & "_SnfListesi" & ".pdf"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & rptFileName)
            Response.BinaryWrite(result.DocumentBytes)
            'Response.End()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub rapor_yukle(ByVal sinavtip As String)
        'Response.Redirect("vizeRapor.aspx")
        Dim rpt As New vizeRpr
        ExportToPDF(rpt)
    End Sub

    Private Sub yonlendir(ByVal rowIndex As Integer, ByVal sinavtip As String)
        Session("KATID") = gridDersler.DataKeys(rowIndex)("KATID")
        Session("ATANAN_ID") = gridDersler.DataKeys(rowIndex)("ATANAN_ID")
        Session("DERSKOD") = gridDersler.DataKeys(rowIndex)("DERSKOD")
        Session("DERSAD") = gridDersler.DataKeys(rowIndex)("DERSAD")
        Session("VIZE_ONAY") = gridDersler.DataKeys(rowIndex)("VIZE_ONAY")
        Session("FINAL_ONAY") = gridDersler.DataKeys(rowIndex)("FINAL_ONAY")
        Session("BUT_ONAY") = gridDersler.DataKeys(rowIndex)("BUT_ONAY")
        Session("DERS_BIRIM") = gridDersler.DataKeys(rowIndex)("BIRIM")
        Session("BOLUMAD") = gridDersler.DataKeys(rowIndex)("BOLUM_AD")

        rapor_yukle(sinavtip)
    End Sub

    Protected Sub btnVize_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As Button = CType(sender, Button)
        Dim grow As GridViewRow = CType(button.Parent.Parent, GridViewRow)
        Dim rowIndex As Integer = grow.RowIndex

        yonlendir(rowIndex, "Vize")
    End Sub

    Protected Sub btnFinal_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As Button = CType(sender, Button)
        Dim grow As GridViewRow = CType(button.Parent.Parent, GridViewRow)
        Dim rowIndex As Integer = grow.RowIndex

        yonlendir(rowIndex, "Final")
    End Sub

    Protected Sub btnBut_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As Button = CType(sender, Button)
        Dim grow As GridViewRow = CType(button.Parent.Parent, GridViewRow)
        Dim rowIndex As Integer = grow.RowIndex

        yonlendir(rowIndex, "But")
    End Sub

    Protected Sub btnHepsi_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As Button = CType(sender, Button)
        Dim grow As GridViewRow = CType(button.Parent.Parent, GridViewRow)
        Dim rowIndex As Integer = grow.RowIndex

        yonlendir(rowIndex, "Hepsi")
    End Sub

    Protected Sub ddlDonem_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDonem.SelectedIndexChanged
        derslerGetir()
    End Sub
End Class