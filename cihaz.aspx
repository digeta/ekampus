<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="cihaz.aspx.vb" Inherits="cihaz" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="o_cph_main" runat="Server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    <div id="o_div_cihaz" style="height: auto">
        <asp:Label ID="o_lbl_ipbilgi" runat="server" Font-Bold="True" ForeColor="DarkOrange"
            Font-Names="Verdana" Font-Size="Small"></asp:Label>&nbsp;<br />
        <asp:Label ID="o_lbl_yetki" runat="server" Font-Bold="True" ForeColor="DarkOrange"
            Font-Names="Verdana" Font-Size="Small" EnableViewState="False"></asp:Label><br />
        <asp:ImageButton ID="img_thispc" runat="server" ImageUrl="~/images/img_cihaz/star.png" />
        <asp:Label ID="o_lbl_indicator" runat="server" Font-Bold="True" Font-Names="Verdana"
            Font-Size="Small" ForeColor="DarkOrange">Simgesi þuan kullandýðýnýz bilgisayar ile eþleþen kayýtlarý simgeler.</asp:Label><br />
        <br />
        <div id="o_div_warnings" style="text-align: center">
        <asp:Label ID="lblUyari" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"
                Font-Names="Verdana" Font-Size="Small" BorderColor="DimGray" BorderStyle="Dashed" BorderWidth="2px" EnableViewState="False"></asp:Label><br />
            <br />
            <asp:Label ID="o_lbl_info" runat="server"
                    Font-Bold="True" ForeColor="#C00000" Visible="False" Font-Names="Verdana" Font-Size="Small">Ekleyeceðeniz her cihaz adýnýza kaydolacaktýr.<br>Yeni bir bilgisayar eklemek için kullaným politikasýný kabul etmelisiniz.</asp:Label>
        </div>
        <div id="o_div_grid" style="margin-bottom: 5px">
        <asp:GridView ID="o_grid_macs" runat="server" CellPadding="4" ForeColor="#333333"
            GridLines="None" AutoGenerateColumns="False" DataKeyNames="ID,AKTIF,BIM_ONAY,MAC,PCTUR"
            Font-Names="Verdana" Font-Size="Small" Width="680px" ShowFooter="True">
            <FooterStyle BackColor="Transparent" Font-Bold="True" ForeColor="White" CssClass="CihazGridFooter" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="Transparent" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="Transparent" Font-Bold="True" ForeColor="White" CssClass="CihazGridHeader" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="img_thispc" runat="server" ImageUrl="~/images/img_cihaz/star.png" Visible="False" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Image ID="img_pctur" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PCTUR" HeaderText="Bilgisayar t&#252;r&#252;" 
                    SortExpression="PCTUR" >
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="IP" HeaderText="IP adresi" SortExpression="IP" >
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="AD" HeaderText="Ad" SortExpression="AD" >
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="SOYAD" HeaderText="Soyad" SortExpression="SOYAD" >
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="LAB" HeaderText="Laboratuar" SortExpression="LAB" >
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="PCNO" HeaderText="Bilg. No." SortExpression="PCNO" >
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Aktif">
                    <ItemTemplate>
                        <asp:ImageButton ID="img_aktif" runat="server" OnClick="img_aktif_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Onay">
                    <ItemTemplate>
                        <asp:Image ID="img_onay" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings FirstPageImageUrl="~/images/img_cihaz/first_page.png" LastPageImageUrl="~/images/img_cihaz/last_page.png"
                Mode="NextPreviousFirstLast" NextPageImageUrl="~/images/img_cihaz/next_page.png"
                PreviousPageImageUrl="~/images/img_cihaz/previous_page.png" />
        </asp:GridView>
        </div>
        <div id="o_div_msfnotice" style="margin-bottom: 5px; margin-top: 15px; text-align: center;">
        <asp:Label id="o_lbl_msfnotice" runat="server" Text="Misafir olarak eklediðiniz kiþiler" ForeColor="#C00000" Font-Bold="True" Visible="false"></asp:Label>
        </div>
        <div id="o_div_msfgrid" style="margin-bottom: 5px">
        <asp:GridView ID="o_grid_msf" runat="server" CellPadding="4" ForeColor="#333333"
            GridLines="None" AutoGenerateColumns="False" DataKeyNames="ID,AKTIF,PCTUR"
            Font-Names="Verdana" Font-Size="Small" Width="680px" ShowFooter="True">
            <FooterStyle BackColor="Transparent" Font-Bold="True" ForeColor="White" CssClass="CihazGridFooter" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="Transparent" ForeColor="White" HorizontalAlign="Center" CssClass="CihazGridFooter" />
            <HeaderStyle BackColor="Transparent" Font-Bold="True" ForeColor="White" CssClass="CihazGridHeader" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="MISAFIR_AD" HeaderText="Ad" SortExpression="MISAFIR_AD" />
                <asp:BoundField DataField="MISAFIR_SOYAD" HeaderText="Soyad" SortExpression="MISAFIR_SOYAD" />
                <asp:BoundField DataField="IP" HeaderText="IP adresi" SortExpression="IP" />
                <asp:BoundField DataField="MISAFIR_SURE" HeaderText="Kalan süre" SortExpression="MISAFIR_SURE" />
                <asp:TemplateField HeaderText="Aktif">
                    <ItemTemplate>
                        <asp:ImageButton ID="img_aktif" runat="server" OnClick="img_aktif_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
        <div id="o_div_pc" runat="server" style="padding: 5px auto 5px 5px; margin: auto auto auto 10%">
            <div id="o_div_pctur" style="margin: auto auto auto 10%">
                <asp:Label ID="o_lbl_pctur" runat="server" Font-Bold="True" Text="Bilgisayar / Cihaz türü : "
                    Width="176px" Font-Names="Verdana" Font-Size="Small" ForeColor="#404040"></asp:Label><asp:DropDownList
                        ID="o_ddl_pctur" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="Small">
                    </asp:DropDownList><br />                    
                            </div>
                            <div id="o_div_onepc" runat="server" visible="false" enableviewstate="true">
                            <table>
                    <tr>
                        <td>
                            <asp:Label ID="o_lbl_ipler" runat="server" Font-Bold="True" Text="Sunucu IP (iç / dýþ) :"
                                Width="152px" Font-Names="Verdana" Font-Size="Small" ForeColor="#404040"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="o_ddl_cihazip" runat="server" AutoPostBack="True" Font-Names="Verdana"
                                Font-Size="Small" Visible="False">
                            </asp:DropDownList>
                            <asp:DropDownList ID="o_ddl_ipler" runat="server" AutoPostBack="True" Font-Names="Verdana"
                                Font-Size="Small">
                            </asp:DropDownList>
                            <asp:DropDownList ID="o_ddl_iplerdis" runat="server" AutoPostBack="True" Font-Names="Verdana"
                                Font-Size="Small">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:RadioButton ID="o_rdb_pc1" runat="server" AutoPostBack="True" Checked="True"
                                GroupName="sunucu" Text="Bu bilgisayar" Font-Names="Verdana" Font-Size="Small" />&nbsp;
                            <asp:RadioButton ID="o_rdb_pc2" runat="server" AutoPostBack="True" GroupName="sunucu"
                                Text="Baþka bilgisayar" Font-Names="Verdana" Font-Size="Small" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="o_lbl_ip" runat="server" Font-Bold="True" Text="IP adresi :" Font-Names="Verdana"
                                Font-Size="Small" Visible="False" ForeColor="#404040" Width="104px"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="o_txt_ip" runat="server" Font-Names="Verdana" Font-Size="Small"
                                Visible="False"></asp:TextBox>
                            <asp:Button ID="o_btn_getmac" runat="server" Text="MAC getir" Font-Names="Verdana"
                                Font-Size="Small" Visible="False" /></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="o_lbl_result" runat="server" Font-Bold="True" Font-Names="Verdana"
                                Font-Size="Small" Visible="False" ForeColor="#404040"></asp:Label></td>
                    </tr>
</table>
                            </div>
            <div id="o_div_server" runat="server" visible="false" enableviewstate="true">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="o_lbl_services" runat="server" Font-Bold="True" Text="Sunucudaki servisler : "
                                Width="160px" Font-Names="Verdana" Font-Size="Small" ForeColor="#404040"></asp:Label></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="o_chk_web" runat="server" Text="Web (http, 80)" Font-Names="Verdana"
                                Font-Size="Small" /></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="o_chk_sweb" runat="server" Text="Secure web (https, 443)" Width="176px" /></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="o_chk_remote" runat="server" Text="Remote desktop (3389)" Width="240px"
                                Font-Names="Verdana" Font-Size="Small" /></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="o_chk_ssh" runat="server" Text="SSH (22)" Width="128px" Font-Names="Verdana"
                                Font-Size="Small" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="o_lbl_others" runat="server" Font-Bold="True" Text="Diðer servisler :"
                                Width="136px" Font-Names="Verdana" Font-Size="Small" ForeColor="#404040"></asp:Label><br />
                            <br />
                        </td>
                        <td>
                            <asp:Label ID="o_lbl_protocols" runat="server" Text="<br>Program veya protokol adý : port numarasý<br>Örnek;<br>SQL Server:1433,Smtp:25"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="o_txt_others" runat="server" Font-Names="Verdana" Font-Size="Small"
                                Width="248px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="o_lbl_aciklama" runat="server" Font-Bold="True" Text="Ek açýklama :"
                                Width="136px" Font-Names="Verdana" Font-Size="Small" ForeColor="#404040"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="o_txt_aciklama" runat="server" TextMode="MultiLine" Width="248px"
                                Font-Names="Verdana" Font-Size="Small"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="o_lbl_domain_request" runat="server" Font-Bold="True" Font-Names="Verdana"
                                Font-Size="Small" Text="Domain istiyormusunuz" Width="176px" ForeColor="#404040"></asp:Label></td>
                        <td>
                            <asp:RadioButton ID="o_rdb_domain1" runat="server" AutoPostBack="True" Font-Names="Verdana"
                                Font-Size="Small" GroupName="domain" Text="Evet" />
                            <asp:RadioButton ID="o_rdb_domain2" runat="server" AutoPostBack="True" Checked="True"
                                Font-Names="Verdana" Font-Size="Small" GroupName="domain" Text="Hayýr" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="o_lbl_domainexp" runat="server" Font-Names="Verdana" Font-Size="Small"
                                Text="<br>Domain isteðiniz önce Bilgi Ýþlem tarafýndan incelenecektir, uygun görülürse onaylanacaktýr.<br>Aþaðýda ki alana sadece domain ön ekini yazýn.<br><br>Ör. :  fef.karaelmas.edu.tr , tam adresi için<br>sadece fef yazmalýsýnýz."
                                Width="432px" Visible="False"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="o_lbl_domain" runat="server" Font-Bold="True" Text="Domain ön eki :" Width="136px"
                                Font-Names="Verdana" Font-Size="Small" Visible="False" ForeColor="#404040"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="o_txt_domain" runat="server" Font-Names="Verdana" Font-Size="Small"
                                Visible="False"></asp:TextBox></td>
                    </tr>
                </table>
            </div>
            <div id="o_div_lab" runat="server" visible="false" enableviewstate="true">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="o_lbl_lab" runat="server" Font-Bold="True" Text="Laboratuar :" Width="176px"
                                Font-Names="Verdana" Font-Size="Small" ForeColor="#404040"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="o_ddl_lab" runat="server">
                            </asp:DropDownList></td>
                        <td>
                            Ortak kullanýlan bilgisayarýn bulunduðu laboratuar</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="o_lbl_pcno" runat="server" Font-Bold="True" Text="Bilgisayar no :"
                                Width="144px" Font-Names="Verdana" Font-Size="Small" ForeColor="#404040"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="o_txt_pcno" runat="server" Font-Names="Verdana" 
                                Font-Size="Small"></asp:TextBox>
                        </td>
                        <td>
                            Ör. : 012 (Yanlýþ) , 12 (Doðru)</td>
                    </tr>
                </table>
            </div>
            <div id="o_div_guest" runat="server" visible="false" enableviewstate="true">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="o_lbl_msfkimlik" runat="server" Font-Bold="True" Text="Misafir T.C. Kimlik :" Width="176px"
                                Font-Names="Verdana" Font-Size="Small" ForeColor="#404040"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="o_txt_msfkimlik" runat="server" Font-Names="Verdana" Font-Size="Small"></asp:TextBox></td>
                    </tr>                
                    <tr>
                        <td>
                            <asp:Label ID="o_lbl_msfad" runat="server" Font-Bold="True" Text="Misafir ad :" Width="176px"
                                Font-Names="Verdana" Font-Size="Small" ForeColor="#404040"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="o_txt_msfad" runat="server" Font-Names="Verdana" Font-Size="Small"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="o_lbl_msfsoyad" runat="server" Font-Bold="True" Text="Misafir soyad :"
                                Width="144px" Font-Names="Verdana" Font-Size="Small" ForeColor="#404040"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="o_txt_msfsoyad" runat="server" Font-Names="Verdana" Font-Size="Small"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="o_lbl_time" runat="server" Font-Bold="True" Text="Süre :" Width="144px"
                                Font-Names="Verdana" Font-Size="Small" ForeColor="#404040"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="o_ddl_time" runat="server" Font-Names="Verdana" Font-Size="Small">
                                <asp:ListItem Value="24">1 G&#252;n</asp:ListItem>
                                <asp:ListItem Value="48">2 G&#252;n</asp:ListItem>
                                <asp:ListItem Value="120">5 G&#252;n</asp:ListItem>
                                <asp:ListItem Value="168">1 Hafta</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                </table>
            </div>
            <div id="o_div_toc" runat="server" visible="false" enableviewstate="true">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://w3.karaelmas.edu.tr/images/stories/pdf/67000-ag-politika.pdf"
                    Target="_blank" Font-Names="Verdana" Font-Size="Small">(Kullaným politikasýnýn pdf formatýný indirmek için týklayýn.)<br></asp:HyperLink>
                <asp:CheckBox ID="o_chk_toc" runat="server" AutoPostBack="True" Font-Bold="True"
                    ForeColor="#C00000" Text="ZKÜ - BÝM kabul edilebilir kullaným politikasýný okudum ve kabul ediyorum."
                    Font-Names="Verdana" Font-Size="Small" /><br />
                <asp:CheckBox ID="o_chk_msftoc" runat="server" AutoPostBack="True" Font-Bold="True"
                    ForeColor="#C00000" Text="Misafirinizin tüm iþlemlerinden siz sorumlu olacaksýnýz.Kabul ediyormusunuz?"
                    Font-Names="Verdana" Font-Size="Small" Visible="False" /><br />
                <asp:Button ID="o_btn_svmac" runat="server" Text="Ekle" Font-Names="Verdana" Font-Size="Small" /></div>
        </div>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="o_btn_getmac" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div><br />
    <div style="clear: both; height: 0;">
    </div>    
</asp:Content>

