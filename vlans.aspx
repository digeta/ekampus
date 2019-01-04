<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="vlans.aspx.vb" Inherits="vlans" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="o_cph_main" Runat="Server">
    <table id="main">
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:GridView ID="o_grid_vlans" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    DataKeyNames="ID" DataSourceID="dsVlan" ForeColor="#333333" GridLines="None" AllowSorting="True">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image ID="img_pctur" runat="server" ImageUrl="~/images/img_cihaz/Laptop.gif" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="VLAN_NO" HeaderText="VLAN No." SortExpression="VLAN_NO" />
                        <asp:BoundField DataField="VLAN_ADI" HeaderText="VLAN Adý" SortExpression="VLAN_ADI" />
                        <asp:BoundField DataField="DHCP_IP" HeaderText="DHCP IP" SortExpression="DHCP_IP" />
                        <asp:TemplateField HeaderText="Aktif" SortExpression="AKTIF">
                            <EditItemTemplate>
                                <asp:DropDownList ID="o_ddl_aktif" runat="server" SelectedValue='<%# Bind("AKTIF") %>'>
                                    <asp:ListItem>False</asp:ListItem>
                                    <asp:ListItem>True</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("AKTIF") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    </Columns>
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <EditRowStyle BackColor="#999999" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Label ID="o_lbl_notice" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px; text-align: left">
            </td>
            <td style="width: 100px; text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="2" rowspan="9" style="text-align: left">
                <fieldset id="field_new" runat="server" style="width: 328px" visible="true">
                    <legend><strong><span style="font-size: 11pt; color: #000066">Yeni vlan ekleme</span></strong></legend>
                    <table id="inside">
                        <tr>
                            <td style="width: 100px">
                                <asp:Label ID="o_lbl_vlan" runat="server" Font-Bold="True" Text="VLAN No. :" Width="152px"></asp:Label></td>
                            <td style="width: 100px">
                                <asp:TextBox ID="o_txt_vlan" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                <strong>
                                    <asp:Label ID="o_lbl_birim" runat="server" Font-Bold="True" Text="VLAN Adý :" Width="136px"></asp:Label></strong></td>
                            <td style="width: 100px">
                                <asp:TextBox ID="o_txt_birim" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                            </td>
                            <td style="width: 100px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                            </td>
                            <td style="width: 100px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                            </td>
                            <td style="width: 100px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                            </td>
                            <td style="width: 100px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                            </td>
                            <td style="width: 100px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                            </td>
                            <td style="width: 100px; text-align: right">
                                &nbsp;
                                <asp:Button ID="o_btn_svvlan" runat="server" Text="Ekle" /></td>
                        </tr>
                    </table>
                    <asp:SqlDataSource ID="dsVlan" runat="server" ConnectionString="Data Source=10.1.16.41;Initial Catalog=proje1;Persist Security Info=True;User ID=ezkuderskul;Password=alskdjfhg"
                        DeleteCommand="DELETE FROM VLAN_LIST WHERE ID=@ID" InsertCommand="IF NOT EXISTS (SELECT 1 FROM VLAN_LIST WHERE VLAN_NO =@VLAN_NO)&#13;&#10;INSERT INTO VLAN_LIST (VLAN_NO,VLAN_ADI) VALUES (@VLAN_NO,@VLAN_ADI)"
                        SelectCommand="SELECT * FROM VLAN_LIST" UpdateCommand="UPDATE VLAN_LIST SET VLAN_NO=@VLAN_NO , VLAN_ADI=@VLAN_ADI , DHCP_IP=@DHCP_IP , AKTIF=@AKTIF WHERE ID=@ID">
                    </asp:SqlDataSource>
                </fieldset>
            </td>
        </tr>
        <tr>
        </tr>
        <tr>
        </tr>
        <tr>
        </tr>
        <tr>
        </tr>
        <tr>
        </tr>
        <tr>
        </tr>
        <tr>
        </tr>
        <tr>
        </tr>
    </table>
</asp:Content>


