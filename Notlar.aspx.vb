Imports System.Data.SqlClient
Imports System.Data


Partial Class Notlar
    Inherits System.Web.UI.Page
    Private _conn, _conn2, _conn3, _connup1, _connup2 As New dConn(2)
    Private _crypt As New cryptool

    Private _sb As New StringBuilder
    Private _sqlstr, _sqlstr2, _sqlstr3, _sqlstr_up, _id, _acik2, _ad, _soyad As String
    Private conn As New SqlConnection(ConfigurationManager.AppSettings("conn1"))

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("kisino") = 0 Or Session("kisino") Is Nothing Then
            Response.Redirect("Login.aspx", True)
            Exit Sub
        End If

        'Session("kisino") = 2009010610060

        Session("PageIs") = ""
        If Not Page.IsPostBack Then
            Try
                'gridNotliste.DataSource = notlarGetir(2004010612074)
                gridNotliste.DataSource = notlarGetir(Session("kisino"), ddlYil.SelectedValue, ddlDonem.SelectedValue)
                gridNotliste.DataBind()

                'ViewState("donem") = 0
                'ViewState("kredi_toplam") = ""
                'ViewState("ag_kredi_toplam") = ""
                'ViewState("ag_ens_kredi_toplam") = ""
                'If Session("type") = "Student" Then
                '    o_panel1.Visible = True
                '    ogr_load()
                'Else
                '    o_panel1.Visible = False
                'End If
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Function notlarGetir(ByVal fakno As Long, ByVal yil As Integer, ByVal donem As Integer) As DataTable
        Try
            Dim command As New SqlCommand("stp_YNS_OGRENCI_NOT_GOR", conn)
            command.CommandType = Data.CommandType.StoredProcedure

            With command.Parameters
                .AddWithValue("FAKNO", fakno)
                .AddWithValue("YIL", yil)
                .AddWithValue("DONEM", donem)
            End With

            Dim Adapter As SqlDataAdapter = New SqlDataAdapter(command)

            Dim dt As New DataTable

            If conn.State <> Data.ConnectionState.Open Then conn.Open()
            Adapter.Fill(dt)
            If conn.State = Data.ConnectionState.Open Then conn.Close()

            'Session("DT_NOTLAR") = dt
            'gridNotliste.DataSource = dt
            'gridNotliste.DataBind()
            Return dt
            'If Not dt.Rows.Count > 0 Then divDersler.Visible = False Else divDersler.Visible = True
        Catch ex As Exception
            'lblUyari2.Visible = True
            'lblUyari2.Text = "Bir hata oluþtu.Bilgi Ýþlem' e baþvurunuz."
            'lblUyari2.Text = ex.Message
            Return Nothing
        Finally
            If conn.State = Data.ConnectionState.Open Then conn.Close()
        End Try
    End Function

    Public Function vizeNot() As String
        Try
            If gridNotliste.DataKeys(gridNotliste.Rows.Count)("VIZE_ONAY") = False Then
                Return ""
            Else
                If gridNotliste.DataKeys(gridNotliste.Rows.Count)("KRD") > 0 Then
                    Return Eval("VIZE_ORT")
                Else
                    Dim notu As Integer = gridNotliste.DataKeys(gridNotliste.Rows.Count)("VIZE_ORT")
                    Dim bn_deger As Integer = gridNotliste.DataKeys(gridNotliste.Rows.Count)("BN_DEGERLENDIR")

                    If bn_deger = 0 Then
                        If notu = 0 Then
                            Return "K"
                        ElseIf notu > 60 Then
                            Return "G"
                        End If
                    ElseIf bn_deger = 1 Then
                        If notu = 0 Then
                            Return "YZ"
                        ElseIf notu > 60 Then
                            Return "YT"
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function finalNot() As String
        Try
            If gridNotliste.DataKeys(gridNotliste.Rows.Count)("BN_DEGERLENDIR") = 1 And gridNotliste.DataKeys(gridNotliste.Rows.Count)("BAGILKODU") <> 3 Then
                Return ""
            Else
                If gridNotliste.DataKeys(gridNotliste.Rows.Count)("FINAL_ONAY") = False Then
                    Return ""
                Else
                    Return Eval("FINAL_NOT")
                End If
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function butNot() As String
        Try
            If gridNotliste.DataKeys(gridNotliste.Rows.Count)("BN_DEGERLENDIR") = 1 And gridNotliste.DataKeys(gridNotliste.Rows.Count)("BAGILKODU") <> 3 Then
                Return ""
            Else
                If gridNotliste.DataKeys(gridNotliste.Rows.Count)("BUT_ONAY") = False Then
                    Return ""
                Else
                    Return Eval("BUT_NOT")
                End If
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function hbn() As String
        Try
            If gridNotliste.DataKeys(gridNotliste.Rows.Count)("BN_DEGERLENDIR") = 1 And gridNotliste.DataKeys(gridNotliste.Rows.Count)("BAGILKODU") <> 3 Then
                Return ""
            Else
                If gridNotliste.DataKeys(gridNotliste.Rows.Count)("FINAL_ONAY") = False And gridNotliste.DataKeys(gridNotliste.Rows.Count)("BUT_ONAY") = False Then
                    Return ""
                Else
                    Return Eval("HBN")
                End If
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function bno() As String
        Try
            If gridNotliste.DataKeys(gridNotliste.Rows.Count)("FINAL_ONAY") = False And gridNotliste.DataKeys(gridNotliste.Rows.Count)("BUT_ONAY") = False Then
                Return ""
            Else
                Return Eval("BNO")
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Protected Function renkver(ByVal strbasari As String, ByVal strdenk As String) As String
        Try
            If (Trim(strbasari) = "K" Or Trim(strbasari) = "F1" Or Trim(strbasari) = "F2" Or Trim(strbasari) = "F3") Then
                If Trim(strdenk) = "" Then
                    ViewState("muaf") = "hayir"
                    Return "#FFDDD7"
                Else
                    ViewState("muaf") = "evet"
                    Return "#eeeeD7"
                End If
                'Return "<tr  bgcolor='#e8e8e8' align='center'><td colspan='7'>" + (Trim(stralyil) + " - " + Trim(strdonem)) + "</td></tr>"
            Else
                ViewState("muaf") = "hayir"
                Return "#DDFFD7"
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Protected Function Grupla_eski(ByVal stralyil As String, ByVal strdonem As String) As String
        Try
            If ViewState("yildonem") <> "" Then
                If (Trim(stralyil) + Trim(strdonem)) <> ViewState("yildonem") Then
                    ViewState("yildonem") = (Trim(stralyil) + Trim(strdonem))
                    ViewState("donem") = Val(ViewState("donem")) + 1
                    Return "<tr  bgcolor='#e8e8e8' align='left'><td colspan='8'><table style='font-size:smaller;'  width='100%'><tr><td align='left'>" + (Trim(stralyil) + " - " + Trim(strdonem) + "</td><td width=''100%></td><td align='right'>   " + ViewState("donem").ToString + ". Dönem  ") + "</td></tr></table></td></tr>"
                Else
                    Return ""
                End If
            Else
                ViewState("donem") = 1
                ViewState("yildonem") = (Trim(stralyil) + Trim(strdonem))
                Return "<tr  bgcolor='#e8e8e8' align='left'><td colspan='8'><table style='font-size:smaller;'  width='100%'><tr><td align='left'>" + (Trim(stralyil) + " - " + Trim(strdonem) + "</td><td width=''100%></td><td align='right'>   " + ViewState("donem").ToString + ". Dönem  ") + "</td></tr></table></td></tr>"
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Protected Function Grupla(ByVal strdonem As String) As String
        Try
            If ViewState("yildonem") <> "" Then
                If (Trim(strdonem)) <> ViewState("yildonem") Then
                    ViewState("yildonem") = (Trim(strdonem))
                    ViewState("donem") = Val(ViewState("donem")) + 1
                    Return "<tr  bgcolor='#e8e8e8' align='left'><td colspan='8'><table style='font-size:smaller;'  width='100%'><tr><td align='left'>" + (Trim(strdonem) + "</td><td width=''100%></td><td align='right'>   " + ViewState("donem").ToString + ". Dönem  ") + "</td></tr></table></td></tr>"
                Else
                    Return ""
                End If
            Else
                ViewState("donem") = 1
                ViewState("yildonem") = (Trim(strdonem))
                Return "<tr  bgcolor='#e8e8e8' align='left'><td colspan='8'><table style='font-size:smaller;'  width='100%'><tr><td align='left'>" + (Trim(strdonem) + "</td><td width=''100%></td><td align='right'>   " + ViewState("donem").ToString + ". Dönem  ") + "</td></tr></table></td></tr>"
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Protected Function ekle_krd(ByVal param1 As Integer, ByVal kredi As String) As String
        Try
            Dim a, b, c As Double
            If ViewState("muaf") = "hayir" Then
                kredi = Replace(kredi, ".", ",")
                If param1 = 1 Then

                    If ViewState("kredi_toplam") <> "" Then
                        ViewState("kredi_toplam") = (CType("0" + ViewState("kredi_toplam"), Double) + CType(kredi, Double)).ToString
                    Else
                        ViewState("kredi_toplam") = CType(kredi, Double).ToString
                    End If
                    a = ViewState("kredi_toplam")
                ElseIf param1 = 2 Then
                    If ViewState("ag_kredi_toplam") <> "" Then
                        ViewState("ag_kredi_toplam") = (CType("0" + ViewState("ag_kredi_toplam"), Double) + CType(kredi, Double)).ToString
                    Else
                        ViewState("ag_kredi_toplam") = CType(kredi, Double).ToString
                    End If
                    b = ViewState("ag_kredi_toplam")


                ElseIf param1 = 3 Then
                    If ViewState("ag_ens_kredi_toplam") <> "" Then
                        ViewState("ag_ens_kredi_toplam") = (CType("0" + ViewState("ag_ens_kredi_toplam"), Double) + CType(kredi, Double)).ToString
                    Else
                        ViewState("ag_ens_kredi_toplam") = CType(kredi, Double).ToString
                    End If
                    c = ViewState("ag_ens_kredi_toplam")

                End If
            End If
            Return ""
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function convertDataReaderToDataSet(ByVal reader As SqlDataReader) As DataSet
        Try
            Dim dataSet As DataSet = New DataSet
            Dim dataRow As DataRow
            Dim columnName As String
            Dim column As DataColumn
            Dim schemaTable As DataTable
            Dim dataTable As DataTable

            Do
                ' Create new data table
                schemaTable = reader.GetSchemaTable
                dataTable = New DataTable
                If Not IsDBNull(schemaTable) Then
                    ' A query returning records was executed
                    Dim i As Integer
                    For i = 0 To schemaTable.Rows.Count - 1
                        dataRow = schemaTable.Rows(i)
                        ' Create a column name that is unique in the data table
                        columnName = dataRow("ColumnName")
                        'Add the column definition to the data table

                        column = New DataColumn(columnName, CType(dataRow("DataType"), Type))
                        dataTable.Columns.Add(column)
                    Next
                    dataSet.Tables.Add(dataTable)

                    'Fill the data table we just created
                    While reader.Read()
                        dataRow = dataTable.NewRow()
                        For i = 0 To reader.FieldCount - 1
                            dataRow(i) = reader(i)
                        Next
                        dataTable.Rows.Add(dataRow)
                    End While
                Else
                    'No records were returned
                    column = New DataColumn("RowsAffected")
                    dataTable.Columns.Add(column)
                    dataSet.Tables.Add(dataTable)
                    dataRow = dataTable.NewRow()
                    dataRow(0) = reader.RecordsAffected
                    dataTable.Rows.Add(dataRow)
                End If
            Loop While reader.NextResult()
            Return dataSet
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Protected Sub ddlDonem_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDonem.SelectedIndexChanged
        gridNotliste.DataSource = notlarGetir(Session("kisino"), ddlYil.SelectedValue, ddlDonem.SelectedValue)
        gridNotliste.DataBind()
    End Sub
End Class
