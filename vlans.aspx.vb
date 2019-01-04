
Partial Class vlans
    Inherits System.Web.UI.Page

    Protected Sub o_btn_svvlan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles o_btn_svvlan.Click
        Try
            If o_txt_vlan.Text = "" Or o_txt_birim.Text = "" Then
                o_lbl_notice.Visible = True
                o_lbl_notice.Text = "Alanlarý boþ býraktýnýz"
                Exit Sub
            End If

            For Each control As Control In field_new.Controls
                If TypeOf control Is TextBox Then
                    Dim tbox As TextBox = DirectCast(control, TextBox)

                    If tbox.ID = "o_txt_vlan" Then
                        For Each c As Char In tbox.Text
                            If Asc(c) < 48 Or Asc(c) > 57 Then
                                o_lbl_notice.Text = "Bilgileri hatalý girdiniz"
                                o_lbl_notice.Visible = True
                                clear()
                                Exit Sub
                            End If
                        Next
                        '#############################################################
                    ElseIf tbox.ID = "o_txt_birim" Then
                        '#############################################################
                        For Each c As Char In tbox.Text
                            If Asc(c) < 48 Or (Asc(c) > 57 And Asc(c) < 65) Or (Asc(c) > 90 And Asc(c) < 97) Or Asc(c) > 122 Then
                                o_lbl_notice.Text = "Bilgileri hatalý girdiniz"
                                o_lbl_notice.Visible = True
                                clear()
                                Exit Sub
                            End If
                        Next
                        '#############################################################
                    End If
                    '------------------------------------------------
                End If
            Next

            With dsVlan.InsertParameters
                .Add("VLAN_NO", o_txt_vlan.Text)
                .Add("VLAN_ADI", o_txt_birim.Text)
            End With
            dsVlan.Insert()
            o_lbl_notice.Visible = True
            o_lbl_notice.Text = "Yeni VLAN baþarýyla eklendi"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("kisino") = 0 Then
            Server.Transfer("Login.aspx")
        End If

        If Not Session("kisino") = 2314 And Not Session("type") = "JediMaster" Then
            Session.Clear()
            Response.Redirect("Login.aspx")
            Exit Sub
        End If
        Session("PageIs") = ""
        o_lbl_notice.Text = ""
        o_lbl_notice.Visible = False
    End Sub

    Private Sub clear()
        Try
            For Each control As Control In field_new.Controls
                If TypeOf control Is TextBox Then
                    Dim tbox As TextBox = DirectCast(control, TextBox)
                    tbox.Text = ""
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
End Class
