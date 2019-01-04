Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports Telerik.Reporting
Imports Telerik.Reporting.Drawing

Public Class vizeRpr
    Inherits Telerik.Reporting.Report
    Friend WithEvents DetailSection1 As Telerik.Reporting.DetailSection
    Friend WithEvents textBox7 As Telerik.Reporting.TextBox
    Friend WithEvents textBox8 As Telerik.Reporting.TextBox
    Friend WithEvents textBox6 As Telerik.Reporting.TextBox
    Friend WithEvents textBox23 As Telerik.Reporting.TextBox
    Friend WithEvents textBox24 As Telerik.Reporting.TextBox
    Friend WithEvents PageHeaderSection1 As Telerik.Reporting.PageHeaderSection
    Friend WithEvents TextBox22 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox17 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox16 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox15 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox12 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox11 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox10 As Telerik.Reporting.TextBox
    Friend WithEvents HtmlTextBox2 As Telerik.Reporting.HtmlTextBox
    Friend WithEvents HtmlTextBox1 As Telerik.Reporting.HtmlTextBox
    Friend WithEvents TextBox4 As Telerik.Reporting.TextBox
    Friend WithEvents textBox1 As Telerik.Reporting.TextBox
    Friend WithEvents textBox5 As Telerik.Reporting.TextBox
    Friend WithEvents htmlTextBox3 As Telerik.Reporting.HtmlTextBox
    Friend WithEvents textBox9 As Telerik.Reporting.TextBox
    Friend WithEvents PageFooterSection1 As Telerik.Reporting.PageFooterSection
    Friend WithEvents textBox13 As Telerik.Reporting.TextBox
    Friend WithEvents textBox14 As Telerik.Reporting.TextBox
    Private WithEvents pictureBox1 As Telerik.Reporting.PictureBox
    Friend WithEvents textBox18 As Telerik.Reporting.TextBox
    Friend WithEvents textBox2 As Telerik.Reporting.TextBox
    Friend WithEvents shape1 As Telerik.Reporting.Shape
    Friend WithEvents Shape2 As Telerik.Reporting.Shape
    Friend WithEvents textBox3 As Telerik.Reporting.TextBox
    Friend WithEvents textBox19 As Telerik.Reporting.TextBox
    Friend WithEvents textBox20 As Telerik.Reporting.TextBox

    Private conn As New SqlConnection(ConfigurationManager.AppSettings("conn1"))

    Public Sub New()
        InitializeComponent()
        Me.DataSource = Nothing
        textBox14.Value = System.Web.HttpContext.Current.Session("adsoyad")
        If System.Web.HttpContext.Current.Session("VIZE_ONAY") = 0 Then
            textBox20.Value = "Notlar için onay verilmemiþ!"
        End If
    End Sub

    Private Sub vizeRpr_NeedDataSource(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.NeedDataSource
        Try
            Dim command As New SqlCommand("stp_YNS_SINIF_LISTE_HOCA", conn)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("ATANAN_ID", System.Web.HttpContext.Current.Session("ATANAN_ID"))
                .AddWithValue("KATID", System.Web.HttpContext.Current.Session("KATID"))
            End With

            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)

            Dim dt As New DataTable

            If conn.State <> ConnectionState.Open Then conn.Open()
            Adapter.Fill(dt)
            If conn.State = ConnectionState.Open Then conn.Close()

            'dt.Columns.Add("SORUMLU", GetType(SqlTypes.SqlString))
            'dt.Columns.Add("ONAY", GetType(SqlTypes.SqlBoolean))

            'For i As Integer = 0 To dt.Rows.Count - 1
            'dt.Rows(i)("SORUMLU") = System.Web.HttpContext.Current.Session("adsoyad")
            'dt.Rows(i)("ONAY") = System.Web.HttpContext.Current.Session("VIZE_ONAY")
            'Next

            'dt.Columns.Add("BOLUMAD", GetType(SqlTypes.SqlString))

            'For i As Integer = 0 To dt.Rows.Count - 1
            '    dt.Rows(i)("BOLUMAD") = System.Web.HttpContext.Current.Session("BOLUMAD")
            'Next
            CType(sender, Telerik.Reporting.Processing.Report).DataSource = dt
        Catch ex As Exception
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub



    'NOTE: The following procedure is required by the telerik Reporting Designer
    'It can be modified using the telerik Reporting Designer.  
    'Do not modify it using the code editor.
    Private Sub InitializeComponent()
        Me.DetailSection1 = New Telerik.Reporting.DetailSection
        Me.textBox7 = New Telerik.Reporting.TextBox
        Me.textBox8 = New Telerik.Reporting.TextBox
        Me.textBox6 = New Telerik.Reporting.TextBox
        Me.textBox23 = New Telerik.Reporting.TextBox
        Me.textBox24 = New Telerik.Reporting.TextBox
        Me.PageFooterSection1 = New Telerik.Reporting.PageFooterSection
        Me.PageHeaderSection1 = New Telerik.Reporting.PageHeaderSection
        Me.TextBox22 = New Telerik.Reporting.TextBox
        Me.TextBox17 = New Telerik.Reporting.TextBox
        Me.TextBox16 = New Telerik.Reporting.TextBox
        Me.TextBox15 = New Telerik.Reporting.TextBox
        Me.TextBox12 = New Telerik.Reporting.TextBox
        Me.TextBox11 = New Telerik.Reporting.TextBox
        Me.TextBox10 = New Telerik.Reporting.TextBox
        Me.HtmlTextBox2 = New Telerik.Reporting.HtmlTextBox
        Me.HtmlTextBox1 = New Telerik.Reporting.HtmlTextBox
        Me.TextBox4 = New Telerik.Reporting.TextBox
        Me.textBox1 = New Telerik.Reporting.TextBox
        Me.textBox5 = New Telerik.Reporting.TextBox
        Me.htmlTextBox3 = New Telerik.Reporting.HtmlTextBox
        Me.textBox9 = New Telerik.Reporting.TextBox
        Me.textBox13 = New Telerik.Reporting.TextBox
        Me.textBox14 = New Telerik.Reporting.TextBox
        Me.pictureBox1 = New Telerik.Reporting.PictureBox
        Me.textBox18 = New Telerik.Reporting.TextBox
        Me.textBox2 = New Telerik.Reporting.TextBox
        Me.shape1 = New Telerik.Reporting.Shape
        Me.Shape2 = New Telerik.Reporting.Shape
        Me.textBox3 = New Telerik.Reporting.TextBox
        Me.textBox19 = New Telerik.Reporting.TextBox
        Me.textBox20 = New Telerik.Reporting.TextBox
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'DetailSection1
        '
        Me.DetailSection1.Height = New Telerik.Reporting.Drawing.Unit(0.599999725818634, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.DetailSection1.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.textBox7, Me.textBox8, Me.textBox6, Me.textBox23, Me.textBox24})
        Me.DetailSection1.KeepTogether = False
        Me.DetailSection1.Name = "DetailSection1"
        Me.DetailSection1.PageBreak = Telerik.Reporting.PageBreak.None
        '
        'textBox7
        '
        Me.textBox7.Format = "{0}"
        Me.textBox7.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(3.1499991416931152, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(-0.00000040372211174144468, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox7.Name = "textBox7"
        Me.textBox7.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(6.0498013496398926, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.39999988675117493, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox7.Value = "{=Fields.OGRENCI}"
        '
        'textBox8
        '
        Me.textBox8.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.30661383271217346, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(-0.00000040372211174144468, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox8.Name = "textBox8"
        Me.textBox8.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(2.7999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.39999988675117493, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox8.Value = "{=Fields.FAKNO}"
        '
        'textBox6
        '
        Me.textBox6.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(9.1999998092651367, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(-0.00000040372211174144468, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox6.Name = "textBox6"
        Me.textBox6.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(1.9232808351516724, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.39999988675117493, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox6.Value = "{=iif(Fields.DEVAM_DURUM = 0,""Devam yok"",""Devam var"")}"
        '
        'textBox23
        '
        Me.textBox23.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(11.399999618530273, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(-0.00000040372211174144468, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox23.Name = "textBox23"
        Me.textBox23.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(1.9000002145767212, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.39999988675117493, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox23.Value = "{=iif(Fields.VIZE_DURUM = 0,""Girmedi"",""Girdi"")}"
        '
        'textBox24
        '
        Me.textBox24.Format = "{0:C0}"
        Me.textBox24.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(13.300000190734863, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(-0.00000040372211174144468, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox24.Name = "textBox24"
        Me.textBox24.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(1.8000003099441528, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.39999988675117493, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.textBox24.Value = "{=Fields.VIZE_ORT}"
        '
        'PageFooterSection1
        '
        Me.PageFooterSection1.Height = New Telerik.Reporting.Drawing.Unit(0.13229165971279144, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.PageFooterSection1.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.shape1})
        Me.PageFooterSection1.Name = "PageFooterSection1"
        Me.PageFooterSection1.Style.Visible = False
        '
        'PageHeaderSection1
        '
        Me.PageHeaderSection1.Height = New Telerik.Reporting.Drawing.Unit(9.3000001907348633, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.PageHeaderSection1.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.TextBox22, Me.TextBox17, Me.TextBox16, Me.TextBox15, Me.TextBox12, Me.TextBox11, Me.TextBox10, Me.HtmlTextBox2, Me.HtmlTextBox1, Me.TextBox4, Me.textBox1, Me.textBox5, Me.htmlTextBox3, Me.textBox9, Me.textBox13, Me.textBox14, Me.pictureBox1, Me.textBox18, Me.textBox2, Me.Shape2, Me.textBox3, Me.textBox19, Me.textBox20})
        Me.PageHeaderSection1.Name = "PageHeaderSection1"
        '
        'TextBox22
        '
        Me.TextBox22.CanGrow = True
        Me.TextBox22.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(5.6000003814697266, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.79999995231628418, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox22.Name = "TextBox22"
        Me.TextBox22.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(8.7999992370605469, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.4000999927520752, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox22.Style.Font.Bold = True
        Me.TextBox22.Style.Font.Name = "Verdana"
        Me.TextBox22.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.TextBox22.TextWrap = False
        Me.TextBox22.Value = "= ""Döküm Tarihi : "" + Now()"
        '
        'TextBox17
        '
        Me.TextBox17.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(5.7968254089355469, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(5.3000001907348633, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox17.Name = "TextBox17"
        Me.TextBox17.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(8.1999998092651367, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.39999988675117493, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox17.Value = "{=Fields.BOLUMAD}"
        '
        'TextBox16
        '
        Me.TextBox16.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(5.7968254089355469, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(4.7973537445068359, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox16.Name = "TextBox16"
        Me.TextBox16.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(8.1999998092651367, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.39999988675117493, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox16.Value = "{=Fields.DERSAD}"
        '
        'TextBox15
        '
        Me.TextBox15.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(5.7968254089355469, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(4.2947072982788086, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox15.Name = "TextBox15"
        Me.TextBox15.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(8.1999998092651367, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.39999988675117493, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox15.Value = "{=Fields.DERSKOD}"
        '
        'TextBox12
        '
        Me.TextBox12.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(3.2835977077484131, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(5.3000001907348633, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox12.Name = "TextBox12"
        Me.TextBox12.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(2.3999996185302734, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.39999988675117493, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox12.Style.Font.Bold = True
        Me.TextBox12.Style.Font.Name = "Verdana"
        Me.TextBox12.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox12.Style.Font.Underline = False
        Me.TextBox12.Value = "Bölümü :"
        '
        'TextBox11
        '
        Me.TextBox11.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(3.2835977077484131, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(4.7973537445068359, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox11.Name = "TextBox11"
        Me.TextBox11.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(2.3999996185302734, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.39999988675117493, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox11.Style.Font.Bold = True
        Me.TextBox11.Style.Font.Name = "Verdana"
        Me.TextBox11.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox11.Style.Font.Underline = False
        Me.TextBox11.Value = "Ders adý :"
        '
        'TextBox10
        '
        Me.TextBox10.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(3.2835977077484131, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(4.2947072982788086, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(2.3999996185302734, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.39999988675117493, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox10.Style.Font.Bold = True
        Me.TextBox10.Style.Font.Name = "Verdana"
        Me.TextBox10.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox10.Style.Font.Underline = False
        Me.TextBox10.Value = "Ders kodu :"
        '
        'HtmlTextBox2
        '
        Me.HtmlTextBox2.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(5.2150788307189941, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(2.5989422798156738, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.HtmlTextBox2.Name = "HtmlTextBox2"
        Me.HtmlTextBox2.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(6.9232807159423828, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.50105744600296021, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.HtmlTextBox2.Style.Font.Bold = True
        Me.HtmlTextBox2.Style.Font.Name = "Verdana"
        Me.HtmlTextBox2.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.HtmlTextBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.HtmlTextBox2.Value = "= Fields.YIL + ""/"" + (YIL+1)"
        '
        'HtmlTextBox1
        '
        Me.HtmlTextBox1.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(4.9767193794250488, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(1.990476131439209, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.HtmlTextBox1.Name = "HtmlTextBox1"
        Me.HtmlTextBox1.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.3999991416931152, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.60000020265579224, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.HtmlTextBox1.Style.Font.Bold = True
        Me.HtmlTextBox1.Style.Font.Name = "Verdana"
        Me.HtmlTextBox1.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.HtmlTextBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.HtmlTextBox1.Value = "ZONGULDAK KARAELMAS ÜNÝVERSÝTESÝ"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(9.1999998092651367, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(7.6000003814697266, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(1.6232806444168091, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.90010017156600952, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox4.Style.Font.Bold = True
        Me.TextBox4.Style.Font.Name = "Verdana"
        Me.TextBox4.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.TextBox4.Value = "Devam Durum"
        '
        'textBox1
        '
        Me.textBox1.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(11.399999618530273, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(7.6000003814697266, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox1.Name = "textBox1"
        Me.textBox1.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(1.5765191316604614, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.90010017156600952, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox1.Style.Font.Bold = True
        Me.textBox1.Style.Font.Name = "Verdana"
        Me.textBox1.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.textBox1.Value = "Sýnav Durum"
        '
        'textBox5
        '
        Me.textBox5.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(13.299999237060547, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(7.6000003814697266, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox5.Name = "textBox5"
        Me.textBox5.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(2.1999983787536621, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.90010017156600952, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox5.Style.Font.Bold = True
        Me.textBox5.Style.Font.Name = "Verdana"
        Me.textBox5.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.textBox5.Value = "Sýnav Ortalamasý"
        '
        'htmlTextBox3
        '
        Me.htmlTextBox3.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(5.1767191886901855, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(3.0999999046325684, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.htmlTextBox3.Name = "htmlTextBox3"
        Me.htmlTextBox3.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.50105744600296021, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.htmlTextBox3.Style.Font.Bold = True
        Me.htmlTextBox3.Style.Font.Name = "Verdana"
        Me.htmlTextBox3.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.htmlTextBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.htmlTextBox3.Value = "=iif(Fields.DONEM = 1,""Güz Dönemi"",(iif(Fields.DONEM = 2, ""Bahar"",(iif(Fields.DON" & _
            "EM = 3, ""Yaz"","""")))))+"" Ara Sýnav Not&nbsp;Dökümü"""
        '
        'textBox9
        '
        Me.textBox9.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(3.2835977077484131, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(5.7999997138977051, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox9.Name = "textBox9"
        Me.textBox9.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(3.3000001907348633, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.39999988675117493, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox9.Style.Font.Bold = True
        Me.textBox9.Style.Font.Name = "Verdana"
        Me.textBox9.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.textBox9.Style.Font.Underline = False
        Me.textBox9.Value = "Ders Sorumlusu :"
        '
        'textBox13
        '
        Me.textBox13.CanGrow = True
        Me.textBox13.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(11.799999237060547, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.30000004172325134, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox13.Name = "TextBox22"
        Me.textBox13.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(2.59999942779541, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.4000999927520752, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox13.Style.Font.Bold = True
        Me.textBox13.Style.Font.Name = "Verdana"
        Me.textBox13.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.textBox13.TextWrap = False
        Me.textBox13.Value = "= ""Sayfa : "" + PageNumber + ""/"" + PageCount"
        '
        'textBox14
        '
        Me.textBox14.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(6.7000002861022949, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(5.8000001907348633, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox14.Name = "textBox14"
        Me.textBox14.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(8.1999998092651367, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.39999988675117493, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox14.Value = ""
        '
        'pictureBox1
        '
        Me.pictureBox1.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.800000011920929, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(2.7000000476837158, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(2.7000000476837158, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.Stretch
        '
        'textBox18
        '
        Me.textBox18.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(7.6000003814697266, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox18.Name = "TextBox4"
        Me.textBox18.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(1.6232806444168091, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.90010017156600952, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox18.Style.Font.Bold = True
        Me.textBox18.Style.Font.Name = "Verdana"
        Me.textBox18.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.textBox18.Value = "Öðrenci No"
        '
        'textBox2
        '
        Me.textBox2.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(3.1499989032745361, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(7.6000003814697266, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox2.Name = "TextBox4"
        Me.textBox2.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(1.6232806444168091, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.90010017156600952, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox2.Style.Font.Bold = True
        Me.textBox2.Style.Font.Name = "Verdana"
        Me.textBox2.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.textBox2.Value = "Adý" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Soyadý"
        '
        'shape1
        '
        Me.shape1.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.18520833551883698, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.shape1.Name = "shape1"
        Me.shape1.ShapeType = New Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW)
        Me.shape1.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(15.199999809265137, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.13227513432502747, Telerik.Reporting.Drawing.UnitType.Cm))
        '
        'Shape2
        '
        Me.Shape2.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.18520833551883698, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(8.6000003814697266, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.Shape2.Name = "Shape2"
        Me.Shape2.ShapeType = New Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW)
        Me.Shape2.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(15.199999809265137, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.13227513432502747, Telerik.Reporting.Drawing.UnitType.Cm))
        '
        'textBox3
        '
        Me.textBox3.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(6.7000002861022949, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(6.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox3.Name = "textBox14"
        Me.textBox3.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(8.1999998092651367, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.39999988675117493, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox3.Value = "= Count(Fields.ID) + "" Kiþi"""
        '
        'textBox19
        '
        Me.textBox19.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(3.2868752479553223, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(6.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox19.Name = "textBox9"
        Me.textBox19.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(3.3000001907348633, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.39999988675117493, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox19.Style.Font.Bold = True
        Me.textBox19.Style.Font.Name = "Verdana"
        Me.textBox19.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.textBox19.Style.Font.Underline = False
        Me.textBox19.Value = "Sýnýf Mevcudu :"
        '
        'textBox20
        '
        Me.textBox20.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(3.9000000953674316, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(6.7999997138977051, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox20.Multiline = False
        Me.textBox20.Name = "textBox14"
        Me.textBox20.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(8.1999998092651367, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.39999988675117493, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.textBox20.Style.Color = System.Drawing.Color.Red
        Me.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.textBox20.TextWrap = False
        Me.textBox20.Value = ""
        '
        'vizeRpr
        '
        Me.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.PageHeaderSection1, Me.DetailSection1, Me.PageFooterSection1})
        Me.PageSettings.Landscape = False
        Me.PageSettings.Margins.Bottom = New Telerik.Reporting.Drawing.Unit(2.5396826267242432, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.PageSettings.Margins.Left = New Telerik.Reporting.Drawing.Unit(2.5396826267242432, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.PageSettings.Margins.Right = New Telerik.Reporting.Drawing.Unit(2.5396826267242432, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.PageSettings.Margins.Top = New Telerik.Reporting.Drawing.Unit(2.5396826267242432, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.Style.BackgroundColor = System.Drawing.Color.White
        Me.Width = New Telerik.Reporting.Drawing.Unit(15.920634269714355, Telerik.Reporting.Drawing.UnitType.Cm)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Private Sub pictureBox1_ItemDataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles pictureBox1.ItemDataBinding
        Try
            Dim pic As Telerik.Reporting.Processing.PictureBox = CType(sender, Telerik.Reporting.Processing.PictureBox)
            Dim imgSrc As String = HttpContext.Current.Server.MapPath("~/images/zkulogo.png")
            Dim img As Image = Image.FromFile(imgSrc)
            pic.Image = img
        Catch ex As Exception
        End Try
    End Sub
End Class
