<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="Management.aspx.vb" Inherits="Management" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="o_cph_main" runat="Server">
    <div id="o_div_materyal" style="height: auto">
        <div id="o_div_bolumsec" style="margin: 0 auto;" runat="server" visible="true">
            <table>
                <tr>
                    <td>
                        <strong><span style="font-size: 9pt; color: darkorange; font-family: Arial; text-align: left;">
                            Fakülte Seçiniz :</span></strong></td>
                    <td style="font-size: 12pt; font-family: Times New Roman">
                        <asp:DropDownList ID="o_ddl_fak" runat="server" Width="340px" AutoPostBack="True">
                        </asp:DropDownList></td>
                </tr>
                <tr style="font-size: 12pt; font-family: Times New Roman">
                    <td>
                        <strong><span style="font-size: 9pt; color: darkorange; font-family: Arial; text-align: left;">
                            Bölüm Seçiniz :</span></strong></td>
                    <td>
                        <asp:DropDownList ID="o_ddl_bol" runat="server" Width="340px" AutoPostBack="True"
                            Enabled="False">
                        </asp:DropDownList></td>
                </tr>
                <tr style="font-size: 12pt; font-family: Times New Roman">
                    <td>
                        <strong><span style="font-size: 9pt; color: darkorange; font-family: Arial"></span></strong>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <div id="o_div_warnings" style="margin: 10px auto 5px auto;">
            <asp:Label ID="o_lbl_title" runat="server" Text="Onay bekleyen materyaller" Font-Bold="True"
                ForeColor="DarkRed" Visible="False"></asp:Label>&nbsp;</div>
        <div id="o_div_grid" style="margin-bottom: 10px;">
            <asp:GridView ID="o_grid_mats" runat="server" AutoGenerateColumns="False" CellPadding="4"
                Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" GridLines="None" ShowFooter="True"
                DataKeyNames="DOSID,DERSID,DOSAD">
                <PagerSettings FirstPageImageUrl="~/images/img_cihaz/first_page.png" LastPageImageUrl="~/images/img_cihaz/last_page.png"
                    Mode="NextPreviousFirstLast" NextPageImageUrl="~/images/img_cihaz/next_page.png"
                    PreviousPageImageUrl="~/images/img_cihaz/previous_page.png" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <FooterStyle BackColor="Transparent" CssClass="DerslerGridFooter" Font-Bold="True"
                    ForeColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Dosya adý" SortExpression="AD">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("AD") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="imgb_onayver" runat="server" ImageUrl="~/images/onayver.png" OnClick="imgb_onayver_Click" />
                        </FooterTemplate>
                        <ItemStyle Width="156px" />
                        <HeaderStyle Wrap="False" />
                        <ItemTemplate>
                            <asp:Label ID="o_lbl_dosad" runat="server" Text='<%# Bind("DOSAD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ders kodu" SortExpression="DERSKOD">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("AD") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemTemplate>
                            <asp:Label ID="o_lbl_dkod" runat="server" Text='<%# Bind("DERSKOD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="YUKLEYEN" HeaderText="Y&#252;kleyen" SortExpression="YUKLEYEN" />
                    <asp:BoundField DataField="ACIKLAMA" HeaderText="A&#231;ýklama" SortExpression="ACIKLAMA" />
                    <asp:BoundField DataField="TARIH" HeaderText="Y&#252;k. Tarih" SortExpression="TARIH">
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Flash" SortExpression="SWF">
                        <HeaderStyle Wrap="False" />
                        <ItemTemplate>
                            <asp:Label ID="o_lbl_swf" runat="server" Text='<%# Bind("SWF") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vir&#252;s tarama" SortExpression="AV">
                        <HeaderStyle Wrap="False" />
                        <ItemTemplate>
                            <asp:Label ID="o_lbl_av" runat="server" Text='<%# Bind("AV") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="G&#246;r&#252;nt&#252;le">
                        <ItemTemplate>
                            <asp:ImageButton ID="img_view" runat="server" ImageUrl="~/images/view.png" OnClick="img_view_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Onayla">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgb_tekonayver" runat="server" ImageUrl="~/images/img_cihaz/Onay.gif" OnClick="img_tekonayver_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="o_chk_dosya" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="Transparent" CssClass="GridFooter" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="Transparent" CssClass="DerslerGridHeader" Font-Bold="True"
                    ForeColor="White" />
            </asp:GridView>
        </div>
        <div id="o_div_warnings2" style="margin: 10px auto 5px auto;">
            <asp:Label ID="o_lbl_title2" runat="server" Font-Bold="True" ForeColor="DarkOrange" Visible="False">Onaylanmýþ materyaller</asp:Label><asp:GridView ID="o_grid_mats2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" GridLines="None" ShowFooter="True"
                DataKeyNames="DOSID,DERSID,DOSAD">
                <PagerSettings FirstPageImageUrl="~/images/img_cihaz/first_page.png" LastPageImageUrl="~/images/img_cihaz/last_page.png"
                    Mode="NextPreviousFirstLast" NextPageImageUrl="~/images/img_cihaz/next_page.png"
                    PreviousPageImageUrl="~/images/img_cihaz/previous_page.png" />
                <FooterStyle BackColor="Transparent" CssClass="AtananGridFooter" Font-Bold="True"
                    ForeColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Dosya adý" SortExpression="AD">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("AD") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="imgb_onaysil" runat="server" ImageUrl="~/images/onaysil.png" OnClick="img_onaysil_Click" />
                        </FooterTemplate>
                        <ItemStyle Width="156px" />
                        <HeaderStyle Wrap="False" />
                        <ItemTemplate>
                            <asp:Label ID="o_lbl_dosad" runat="server" Text='<%# Bind("DOSAD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ders kodu" SortExpression="DERSKOD">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("AD") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemTemplate>
                            <asp:Label ID="o_lbl_dkod" runat="server" Text='<%# Bind("DERSKOD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="YUKLEYEN" HeaderText="Y&#252;kleyen" SortExpression="YUKLEYEN" />
                    <asp:BoundField DataField="ACIKLAMA" HeaderText="A&#231;ýklama" SortExpression="ACIKLAMA" />
                    <asp:BoundField DataField="TARIH" HeaderText="Y&#252;k. Tarih" SortExpression="TARIH">
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Flash" SortExpression="SWF">
                        <HeaderStyle Wrap="False" />
                        <ItemTemplate>
                            <asp:Label ID="o_lbl_swf" runat="server" Text='<%# Bind("SWF") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vir&#252;s tarama" SortExpression="AV">
                        <HeaderStyle Wrap="False" />
                        <ItemTemplate>
                            <asp:Label ID="o_lbl_av" runat="server" Text='<%# Bind("AV") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="G&#246;r&#252;nt&#252;le">
                        <ItemTemplate>
                            <asp:ImageButton ID="img_view2" runat="server" ImageUrl="~/images/view.png" OnClick="img_view2_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Onay kaldýr">
                        <HeaderStyle Wrap="False" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgb_tekonaysil" runat="server" ImageUrl="~/images/img_cihaz/Red.gif" OnClick="img_tekonaysil_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="o_chk_dosya" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="Transparent" CssClass="GridFooter" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="Transparent" CssClass="AtananGridHeader" Font-Bold="True"
                    ForeColor="White" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </div>
        <div id="o_div_grid2" style="margin-bottom: 10px;">
            &nbsp;</div>
    </div>
    <div style="clear: both; height: 0;">
    </div>
</asp:Content>
