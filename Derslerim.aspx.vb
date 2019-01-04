Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class Derslerim_vize
    Inherits System.Web.UI.Page

    Private conn As New SqlConnection(ConfigurationManager.AppSettings("conn1"))

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("kisino") = 0 Or Session("kisino") Is Nothing Then
            Response.Redirect("Login.aspx", True)
            Exit Sub
        End If

        If Session("parola_zorla") = True Then
            Response.Redirect("Pass.aspx")
            Exit Sub
        End If

        'Session("kisino") = 2108

        'Response.Redirect("Default.aspx")
        'Exit Sub

        'lblUyari.Visible = True
        'lblUyari.Text = "<a href=""http://server.karaelmas.edu.tr/ekamp_test/Login.aspx"">Staj not giriş ve döküm işlemleri için buraya tıklayın, bu sayfada yeniden giriş yapmanız gerekecektir.</a>"

        If Page.IsPostBack = False Then
            derslerGetir()
        End If
    End Sub

    Private Sub derslerGetir()
        Try
            Dim command As New SqlCommand("stp_YNS_ATANAN_DERSLER", conn)
            command.CommandType = Data.CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("ATANANSICIL", Session("kisino"))
            End With

            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)

            Dim dt As New DataTable

            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            Adapter.Fill(dt)
            If conn.State = Data.ConnectionState.Open Then conn.Close()

            Session("DT_NOTLAR") = dt
            grid_dersler.DataSource = dt
            grid_dersler.DataBind()

            'If Not dt.Rows.Count > 0 Then divDersler.Visible = False Else divDersler.Visible = True
        Catch ex As Exception
            lblUyari.Visible = True
            'lblUyari.Text = "Bir hata oluştu.Bilgi İşlem' e başvurunuz."
            lblUyari.Text = ex.Message
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Protected Sub btnVize_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As Button = CType(sender, Button)
        Dim grow As GridViewRow = CType(button.Parent.Parent, GridViewRow)
        Dim rowIndex As Integer = grow.RowIndex

        Session("KATID") = grid_dersler.DataKeys(rowIndex)("KATID")
        Session("ATANAN_ID") = grid_dersler.DataKeys(rowIndex)("ATANAN_ID")
        Session("DERSKOD") = grid_dersler.DataKeys(rowIndex)("DERSKOD")
        Session("DERSAD") = grid_dersler.DataKeys(rowIndex)("DERSAD")
        Session("VIZE_ONAY") = grid_dersler.DataKeys(rowIndex)("VIZE_ONAY")
        Session("DERS_BIRIM") = grid_dersler.DataKeys(rowIndex)("BIRIM")
        Session("BAGILKODU") = grid_dersler.DataKeys(rowIndex)("BAGILKODU")
        Session("DONEM") = grid_dersler.DataKeys(rowIndex)("DONEM")
        Session("YIL") = grid_dersler.DataKeys(rowIndex)("YIL")

        If grid_dersler.DataKeys(rowIndex)("KRD") > 0 Then
            Response.Redirect("nvize.aspx")
        Else
            Response.Redirect("nvize_krdz.aspx")
        End If
    End Sub

    Protected Sub grid_dersler_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grid_dersler.RowCreated
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    CType(e.Row.FindControl("btnVize"), Button).Attributes.Add("onclick", "alert('Notları Onayla butonunu kullanarak notları onaylamadığınız sürece" & _
        '                             " notlar öğrenciler tarafından görüntülenemeyecektir!\n" & _
        '                             " Ayrıca notlar onaylandıktan sonra düzeltmeniz mümkün olmayacaktır.');")
        'End If
    End Sub

    Protected Sub grid_dersler_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grid_dersler.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hocasay As Integer = grid_dersler.DataKeys(e.Row.RowIndex)("HOCASAY")
            If hocasay > 1 Then
                CType(e.Row.FindControl("btnVize"), Button).Attributes.Add("onclick", "alert('DİKKAT!\n\nSeçilen ders " & hocasay & _
                                         " farklı ders sorumlusu tarafından verilmektedir.\n" & _
                                         "Lütfen sadece dersinizi almış olan öğrencilere not girişi yaptığınızdan emin olunuz.\n\n" & _
                                         "İlk kez not girişi yaptığınız öğrenciler daha sonra sadece sizin listenizde gözükecektir.\n" & _
                                         "Dersinizi almayan öğrencilerin not alanlarını boş bırakınız.');")
            End If
        End If
    End Sub
End Class
