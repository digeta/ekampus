<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="Materials.aspx.vb" Inherits="Materials" Title="Untitled Page" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="o_cph_main" runat="Server">
    <div id="o_div_materyal" style="margin-left: 0px; height: auto">
        <div id="o_div_warnings" style="margin: 10px auto 5px auto;">
            <asp:Label ID="o_lbl_title" runat="server" Text="Yüklemiþ olduðunuz ders materyalleri aþaðýdadýr"
                Font-Bold="True" ForeColor="DarkRed"></asp:Label>
        </div>
        <div id="o_div_warnings2" style="margin: 5px auto 10px auto;">
            <asp:Label ID="o_lbl_title2" runat="server" Font-Bold="True" ForeColor="DarkOrange" Visible="False"></asp:Label>
        </div>        
        <div id="o_div_grid" style="margin-bottom: 10px;">
            <asp:GridView ID="o_grid_files" runat="server" AutoGenerateColumns="False" CellPadding="4"
                Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" GridLines="None" ShowFooter="True"
                DataKeyNames="ID,AD,SWF">
                <PagerSettings FirstPageImageUrl="~/images/img_cihaz/first_page.png" LastPageImageUrl="~/images/img_cihaz/last_page.png"
                    Mode="NextPreviousFirstLast" NextPageImageUrl="~/images/img_cihaz/next_page.png"
                    PreviousPageImageUrl="~/images/img_cihaz/previous_page.png" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image ID="img_filetype" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dosya adý" SortExpression="AD">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("AD") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("AD") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <FooterTemplate>
                            <asp:ImageButton ID="imgb_rempublish" runat="server" ImageUrl="~/images/rem_publish.png"
                                OnClick="imgb_rempublish_Click" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DERSSAY" HeaderText="Ders sayýsý" SortExpression="DERSSAY">
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Yayýnda mý" SortExpression="YAYIN">
                        <ItemTemplate>
                            <asp:Label ID="o_lbl_yayin" runat="server" Text='<%# Bind("YAYIN") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="&#199;evrildi mi" SortExpression="SWF">
                        <ItemTemplate>
                            <asp:Label ID="o_lbl_swf" runat="server" Text='<%# Bind("SWF") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="G&#246;r&#252;nt&#252;le">
                        <ItemTemplate>
                            <asp:ImageButton ID="img_view" runat="server" ImageUrl="~/images/view.png" OnClick="img_view_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="D&#252;zenle">
                        <ItemTemplate>
                            <asp:ImageButton ID="img_edit" runat="server" ImageUrl="~/images/edit.png" OnClick="img_edit_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Yayýnla">
                        <ItemTemplate>
                            <asp:ImageButton ID="img_publish" runat="server" ImageUrl="~/images/publish.png" OnClick="img_publish_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="o_chk_dosya" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="Transparent" CssClass="MateryalGridFooter" Font-Bold="True"
                    ForeColor="White" />
                <PagerStyle BackColor="Transparent" CssClass="GridFooter" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="Transparent" CssClass="MateryalGridHeader" Font-Bold="True"
                    ForeColor="White" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </div>
        <div id="o_div_bolumsec" style="margin: 0 auto;">
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
        <div id="o_div_iliskiler" style="margin-bottom: 10px;">
            <div style="float: left;">
                <table>
                    <tr>
                        <td id="td1_top" style="background-image: url('images/img_cihaz/header1.png'); background-repeat: repeat-x;
                            height: 25px;" runat="server" visible="false">
                            <div style="width: 100px; float: left;">
                                <strong><span style="font-size: 10pt; color: #ffffff; font-family: Verdana">Ders kodu</span></strong></div>
                            <div style="width: 100px; float: left;">
                                <strong><span style="font-size: 10pt; color: #ffffff; font-family: Verdana">Ders adý</span></strong></div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="o_div_dersler" style="float: left; overflow: auto; height: 250px; width: 350px;
                                margin: 0;">
                                <asp:GridView ID="o_grid_dersler" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" GridLines="None" ShowHeader="False"
                                    DataKeyNames="DERSKOD,DERSADI">
                                    <PagerSettings FirstPageImageUrl="~/images/img_cihaz/first_page.png" LastPageImageUrl="~/images/img_cihaz/last_page.png"
                                        Mode="NextPreviousFirstLast" NextPageImageUrl="~/images/img_cihaz/next_page.png"
                                        PreviousPageImageUrl="~/images/img_cihaz/previous_page.png" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Derskod" SortExpression="DERSKOD">
                                            <FooterTemplate>
                                                <asp:ImageButton ID="imbg_add" runat="server" ImageUrl="~/images/add_selection.png" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("DERSKOD") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="90px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ders adý" SortExpression="DERSADI">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("DERSADI") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="o_chk_dosya" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="Transparent" CssClass="DerslerGridHeader" Font-Bold="True"
                                        ForeColor="White" />
                                    <PagerStyle BackColor="Transparent" CssClass="GridFooter" ForeColor="White" HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        Henüz bir bölüm seçmediniz
                                    </EmptyDataTemplate>
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="Transparent" CssClass="DerslerGridHeader" Font-Bold="True"
                                        ForeColor="White" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td id="td1_bottom" style="background-image: url('images/img_cihaz/header1.png');
                            background-repeat: repeat-x; height: 25px;" runat="server" visible="false">
                            <div style="float: right;">
                                <asp:ImageButton ID="imgb_addselection" runat="server" ImageUrl="~/images/add_selection.png" /></div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="float: left; margin-left: 30px;">
                <table>
                    <tr>
                        <td id="td2_top" style="background-image: url('images/img_cihaz/header2.png'); background-repeat: repeat-x;
                            height: 25px;" runat="server" visible="false">
                            <div style="width: 120px; float: left;">
                                <strong><span style="font-size: 10pt; color: #ffffff; font-family: Verdana">Bölüm</span></strong></div>
                            <div style="width: 100px; float: left;">
                                <strong><span style="font-size: 10pt; color: #ffffff; font-family: Verdana">Ders kodu</span></strong></div>
                            <div style="width: 100px; float: left;">
                                <strong><span style="font-size: 10pt; color: #ffffff; font-family: Verdana">Ders adý</span></strong></div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="o_div_iliski" style="float: left; overflow: auto; height: 250px; width: 400px;
                                margin: 0;">
                                <asp:GridView ID="o_grid_iliskiler" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" GridLines="None" ShowHeader="False"
                                    DataKeyNames="DERSKOD,BIRIM">
                                    <PagerSettings FirstPageImageUrl="~/images/img_cihaz/first_page.png" LastPageImageUrl="~/images/img_cihaz/last_page.png"
                                        Mode="NextPreviousFirstLast" NextPageImageUrl="~/images/img_cihaz/next_page.png"
                                        PreviousPageImageUrl="~/images/img_cihaz/previous_page.png" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="B&#246;l&#252;m">
                                            <FooterTemplate>
                                                <asp:ImageButton ID="imgb_rem" runat="server" ImageUrl="~/images/rem_selection.png" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("BIRIMAD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Derskod" SortExpression="DERSKOD">
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("DERSKOD") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="90px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ders adý" SortExpression="DERSADI">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("DERSAD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="o_chk_dosya" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="Transparent" CssClass="AtananGridHeader" Font-Bold="True"
                                        ForeColor="White" />
                                    <PagerStyle BackColor="Transparent" CssClass="GridFooter" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="Transparent" CssClass="AtananGridHeader" Font-Bold="True"
                                        ForeColor="White" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td id="td2_bottom" style="background-image: url('images/img_cihaz/header2.png');
                            background-repeat: repeat-x; height: 25px;" runat="server" visible="false">
                            <div style="float: right;">
                                <asp:ImageButton ID="imgb_remselection" runat="server" ImageUrl="~/images/rem_selection.png" /></div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div style="clear: both; height: 0;">
    </div>
</asp:Content>
