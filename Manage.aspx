<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="Manage.aspx.vb" Inherits="Manage" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="o_cph_main" runat="Server">
    <fieldset><table id="o_tbl_secim" runat="server">
        <tr>
            <td>
                </td>
            <td>
                Bim onayý</td>
            <td>
            </td>
            <td>
                IP adresi</td>
            <td>
            </td>
            <td>
                VLAN</td>
            <td>
            </td>
            <td>
                Ad ve/veya soyad</td>
            <td>
            </td>
            <td>
                Aktif/Pasif</td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="o_chk_bimonay" runat="server" Text=" " Width="16px" /></td>
            <td>
                <asp:DropDownList ID="o_ddl_bimonay" runat="server" Width="155px">
                    <asp:ListItem Value="1">Bim onaylý</asp:ListItem>
                    <asp:ListItem Value="0">Bim onaysýz</asp:ListItem>
                </asp:DropDownList></td>
            <td>
                <asp:CheckBox ID="o_chk_ip" runat="server" Text=" " Width="16px" /></td>
            <td>
                <asp:TextBox ID="o_txt_ip" runat="server"></asp:TextBox></td>
            <td>
                <asp:CheckBox ID="o_chk_vlan" runat="server" Text=" " Width="16px" /></td>
            <td>
                <asp:DropDownList ID="o_ddl_vlan" runat="server" Width="155px">
                </asp:DropDownList></td>
            <td>
                <asp:CheckBox ID="o_chk_adsoyad" runat="server" Text=" " Width="16px" /></td>
            <td>
                <asp:TextBox ID="o_txt_adsoyad" runat="server"></asp:TextBox></td>
            <td><asp:CheckBox ID="o_chk_aktif" runat="server" Text=" " Width="16px" /></td>
            <td style="text-align: left"><asp:DropDownList ID="o_ddl_aktif" runat="server" Width="155px">
                <asp:ListItem Value="0">Pasif</asp:ListItem>
                <asp:ListItem Value="1">Aktif</asp:ListItem>
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td>
                </td>
            <td>
                Bilgisayar türü</td>
            <td>
            </td>
            <td>
                MAC adresi</td>
            <td>
            </td>
            <td>
                Sicil/Fak.no/Kurumdýsý</td>
            <td>
            </td>
            <td>
                Laboratuar</td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="o_chk_pctur" runat="server" Text=" " Width="16px" /></td>
            <td>
                <asp:DropDownList ID="o_ddl_pctur" runat="server" Width="155px">
                    <asp:ListItem Value="0">Sabit kullanýcý</asp:ListItem>
                    <asp:ListItem Value="1">Hareketli kullanýcý</asp:ListItem>
                    <asp:ListItem Value="2">Ortak kullaným</asp:ListItem>
                    <asp:ListItem Value="3">Sunucu</asp:ListItem>
                    <asp:ListItem Value="4">Misafir</asp:ListItem>
                </asp:DropDownList></td>
            <td>
                <asp:CheckBox ID="o_chk_mac" runat="server" Text=" " Width="16px" /></td>
            <td>
                <asp:TextBox ID="o_txt_mac" runat="server"></asp:TextBox></td>
            <td>
                <asp:CheckBox ID="o_chk_kisi" runat="server" Text=" " Width="16px" /></td>
            <td>
                <asp:TextBox ID="o_txt_kisi" runat="server"></asp:TextBox></td>
            <td>
                <asp:CheckBox ID="o_chk_lab" runat="server" Text=" " Width="16px" /></td>
            <td style="text-align: right">
                <asp:DropDownList ID="o_ddl_lab" runat="server" Width="155px">
                </asp:DropDownList>&nbsp;</td>
            <td>
                </td>
            <td>
                <asp:Button ID="o_btn_reset" runat="server" Text="Reset" /><asp:Button ID="o_btn_do" runat="server" Text="Search bakalým" /></td>
        </tr>
        </table></fieldset>
    &nbsp;
    <table>
        <tr>
            <td colspan="2" style="text-align: left">
    <asp:GridView ID="o_grid_result" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ID,KISI_NO"
        DataSourceID="dsGrid" EnableTheming="True" ForeColor="#333333" GridLines="Vertical">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="KISI_NO" HeaderText="Kiþi no." SortExpression="KISI_NO" />
            <asp:BoundField DataField="AD" HeaderText="Adý" SortExpression="AD" />
            <asp:BoundField DataField="SOYAD" HeaderText="Soyadý" SortExpression="SOYAD" />
            <asp:BoundField DataField="IP" HeaderText="IP" SortExpression="IP" />
            <asp:BoundField DataField="MAC" HeaderText="MAC" SortExpression="MAC" />
            <asp:BoundField DataField="PCTUR" HeaderText="Bilg. t&#252;r&#252;" SortExpression="PCTUR" />
            <asp:BoundField DataField="AKTIF" HeaderText="Aktif" SortExpression="AKTIF" />
            <asp:BoundField DataField="BIM_ONAY" HeaderText="Bim onayý" SortExpression="BIM_ONAY" />
            <asp:BoundField DataField="DATE_CREATED" HeaderText="Kayýt tarihi" SortExpression="DATE_CREATED" />
            <asp:BoundField DataField="DATE_MODIFIED" HeaderText="Deðiþtirilme tarihi" SortExpression="DATE_MODIFIED" />
        </Columns>
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <EmptyDataTemplate>
            NO~thing selected
        </EmptyDataTemplate>
        <EditRowStyle BackColor="#999999" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: left">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="dsDetailsServer" ForeColor="#333333" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="WEB" HeaderText="[Web]" />
                        <asp:BoundField DataField="SWEB" HeaderText="[Secure Web]" />
                        <asp:BoundField DataField="REMOTE_DESKTOP" HeaderText="[Remote Desktop]" />
                        <asp:BoundField DataField="SSH" HeaderText="[SSH]" />
                        <asp:BoundField DataField="DIGER" HeaderText="[Diðer]" />
                        <asp:BoundField DataField="DOMAIN" HeaderText="[Domain]" />
                        <asp:BoundField DataField="ACIKLAMA" HeaderText="[Açýklama]" />
                        <asp:BoundField DataField="TARIH_ISTEK" HeaderText="[Ýstek tarihi]" />
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <EditRowStyle BackColor="#999999" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="text-align: left" colspan="2">
                <asp:DetailsView ID="o_dtl_user" runat="server" AutoGenerateRows="False" CellPadding="4"
                    DataKeyNames="ID" DataSourceID="dsDetails" ForeColor="#333333" GridLines="None"
                    Height="50px" Width="208px">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                    <EditRowStyle BackColor="#999999" Width="208px" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <Fields>
                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="IP" HeaderText="IP" />
                        <asp:BoundField DataField="MAC" HeaderText="MAC" />
                        <asp:TemplateField HeaderText="PC t&#252;r&#252;">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("PCTUR") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("PCTUR") %>'></asp:TextBox>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("PCTUR") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Laboratuar">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("LAB_ID") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("LAB_ID") %>'></asp:TextBox>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("LAB_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PCNO" HeaderText="Lab. PC no." />
                        <asp:TemplateField HeaderText="Aktif">
                            <EditItemTemplate>
                                <asp:DropDownList ID="o_ddl_aktif" runat="server" SelectedValue='<%# Bind("AKTIF") %>'>
                                    <asp:ListItem Selected="True">False</asp:ListItem>
                                    <asp:ListItem>True</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("AKTIF") %>'></asp:TextBox>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("AKTIF") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fw ekle">
                            <EditItemTemplate>
                                <asp:DropDownList ID="o_ddl_fwekle" runat="server" SelectedValue='<%# Bind("FW_EKLE") %>'>
                                    <asp:ListItem>False</asp:ListItem>
                                    <asp:ListItem>True</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("FW_EKLE") %>'></asp:TextBox>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("FW_EKLE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fw &#231;ýkar">
                            <EditItemTemplate>
                                <asp:DropDownList ID="o_ddl_fwcikar" runat="server" SelectedValue='<%# Bind("FW_CIKAR") %>'>
                                    <asp:ListItem>False</asp:ListItem>
                                    <asp:ListItem>True</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("FW_CIKAR") %>'></asp:TextBox>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("FW_CIKAR") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="T&#252;nel ekle">
                            <EditItemTemplate>
                                <asp:DropDownList ID="o_ddl_tunekle" runat="server" SelectedValue='<%# Bind("TUN_EKLE") %>'>
                                    <asp:ListItem>False</asp:ListItem>
                                    <asp:ListItem>True</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("TUN_EKLE") %>'></asp:TextBox>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("TUN_EKLE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="T&#252;nel &#231;ýkar">
                            <EditItemTemplate>
                                <asp:DropDownList ID="o_ddl_tuncikar" runat="server" SelectedValue='<%# Bind("TUN_CIKAR") %>'>
                                    <asp:ListItem>False</asp:ListItem>
                                    <asp:ListItem>True</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("TUN_CIKAR") %>'></asp:TextBox>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("TUN_CIKAR") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bim onayý">
                            <EditItemTemplate>
                                <asp:DropDownList ID="o_ddl_state" runat="server" SelectedValue='<%# Bind("BIM_ONAY") %>'>
                                    <asp:ListItem>False</asp:ListItem>
                                    <asp:ListItem>True</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("BIM_ONAY") %>'></asp:TextBox>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("BIM_ONAY") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ACIKLAMA" HeaderText="A&#231;ýklama" />
                        <asp:CommandField ShowEditButton="True" />
                    </Fields>
                    <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:DetailsView>
                &nbsp;</td>
        </tr>
    </table>
    &nbsp;
    <asp:SqlDataSource ID="dsGrid" runat="server" UpdateCommand="&#13;&#10;"></asp:SqlDataSource>
    <asp:SqlDataSource ID="dsDetails" runat="server" SelectCommand="SELECT * FROM IPMAC WHERE ID=@ID"
        UpdateCommand="UPDATE IPMAC SET IP=@IP, MAC=@MAC, PCTUR=@PCTUR, PCNO=@PCNO, AKTIF=@AKTIF, FW_EKLE=@FW_EKLE, FW_CIKAR=@FW_CIKAR, TUN_EKLE=@TUN_EKLE, TUN_CIKAR=@TUN_CIKAR, BIM_ONAY=@BIM_ONAY, ACIKLAMA=@ACIKLAMA WHERE ID=@ID&#13;&#10;">
        <SelectParameters>
            <asp:ControlParameter ControlID="o_grid_result" Name="ID" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource><asp:SqlDataSource ID="dsDetailsServer" runat="server" UpdateCommand="&#13;&#10;" SelectCommand="SELECT * FROM SUNUCULAR WHERE IPMAC_ID=@IPMAC_ID">
        <SelectParameters>
            <asp:ControlParameter ControlID="o_grid_result" Name="IPMAC_ID" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

