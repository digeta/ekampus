<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="Personal.aspx.vb" Inherits="personal" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="o_cph_main" runat="Server">
    <ajaxToolkit:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" BorderColor="tan"
        Color="Transparent" Corners="All" Radius="4" TargetControlID="o_div_inner">
    </ajaxToolkit:RoundedCornersExtender>
    <div id="o_div_settings" style="margin: 0 auto; width: 600px;" runat="server" visible="true">
        <div id="o_div_warning" style="margin: 0px auto; width: 350px;">
            <asp:Label ID="o_lbl_warning" runat="server" Font-Bold="True" ForeColor="DarkRed"></asp:Label>&nbsp;</div>
        <div id="o_div_inner" style="margin: 0 auto; width: 350px;" runat="server">
        <div id="Div1" style="margin: 0 auto; width: 350px;">
            <table style="width: 350px;">
                <tr>
                    <td style="width: 150px;">
                        <span style="font-size: 9pt; color: tan; font-family: Verdana"><strong>Ad ve Soyad :
                        </strong></span>
                    </td>
                    <td>
                        <asp:Label ID="o_lbl_adsoyad" runat="server" Font-Bold="True" Font-Names="Verdana"
                            Font-Size="9pt"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 150px;">
                        <span style="font-size: 9pt; color: tan; font-family: Verdana"><strong>Fakülte : </strong>
                        </span>
                    </td>
                    <td>
                        <asp:DropDownList ID="o_ddl_fak" runat="server" Width="200px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="width: 150px;">
                        <span style="font-size: 9pt; color: tan; font-family: Verdana"><strong>Bölüm : </strong>
                        </span>
                    </td>
                    <td>
                        <asp:DropDownList ID="o_ddl_bol" runat="server" Width="200px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="width: 150px;">
                        <span style="font-size: 9pt; color: tan; font-family: Verdana"><strong>Telefon : </strong>
                        </span>
                    </td>
                    <td>
                        <asp:TextBox ID="o_txt_tel" runat="server" Width="194px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 150px;">
                        <span style="font-size: 9pt; color: tan; font-family: Verdana"><strong>E-Posta : </strong>
                        </span>
                    </td>
                    <td>
                        <asp:TextBox ID="o_txt_mail" runat="server" Width="194px"></asp:TextBox></td>
                </tr>
            </table>
            </div>
            <div>
                <div style="float: left; height: 20px; margin-top: 0; margin-left: 1px;">
                    <span style="font-size: 9pt; color: tan; font-family: Verdana"><strong>Mesajlaþma ayarlarý :
                    </strong></span>
                </div>
                <div style="margin-top: 25px;">
                    <asp:RadioButton ID="o_rdb_moff" runat="server" Text="Mesaj alýmý kapalý" Font-Bold="True"
                        Font-Names="Verdana" Font-Size="8pt" ForeColor="DarkRed" AutoPostBack="True"
                        GroupName="msgset" /><br />
                </div>
                <div style="margin-top: 5px;">
                    <asp:RadioButton ID="o_rdb_mon" runat="server" Text="Mesaj alýmý açýk" Font-Bold="True"
                        Font-Names="Verdana" Font-Size="8pt" ForeColor="DarkRed" AutoPostBack="True"
                        GroupName="msgset" /></div>
                <div style="margin-top: 10px;">
                    <div id="o_div_selection" runat="server" visible="false">
                        <span style="font-size: 9pt; color: tan; font-family: Verdana"><strong>Seçenekler</strong></span>
                        <asp:RadioButton ID="o_rdb_opt1" runat="server" Text="Tüm öðrenciler ve personelden mesaj gelsin." Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" GroupName="msgsettings" /><br />
                        <asp:RadioButton ID="o_rdb_opt2" runat="server" Text="Sadece bölüm öðrencilerinden mesaj gelsin." Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" GroupName="msgsettings" /><br />
                        <asp:RadioButton ID="o_rdb_opt3" runat="server" Text="Sadece fakülte öðrencilerinden mesaj gelsin." Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" GroupName="msgsettings" /><br />
                        <asp:RadioButton ID="o_rdb_opt4" runat="server" Text="Sadece personelden mesaj gelsin." Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" GroupName="msgsettings" /><br />
                        <asp:RadioButton ID="o_rdb_opt5" runat="server" Text="Sadece öðrencilerden mesaj gelsin." Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" GroupName="msgsettings" /><br />                        
                    </div>
                </div>
            </div>
        
            <div id="o_div_innerfooter">
                <div style="float: right;">
                    <asp:ImageButton ID="imgb_update" runat="server" ImageUrl="~/images/update.png" /></div>
            </div>            
        </div>
    </div>
    <asp:DropDownList ID="o_ddl_msgset" runat="server" Visible="False">
        <asp:ListItem Value="0">T&#252;m mesajlar gelsin.</asp:ListItem>
        <asp:ListItem Value="1">Hi&#231;bir mesaj gelmesin.</asp:ListItem>
        <asp:ListItem Value="2">&#214;ðrenci mesajlarý gelmesin.</asp:ListItem>
        <asp:ListItem Value="3">Personel mesajlarý gelmesin.</asp:ListItem>
        <asp:ListItem Value="4">B&#246;l&#252;m &#246;ðrencilerinin mesajlarý gelsin.</asp:ListItem>
        <asp:ListItem Value="5">Fak&#252;lte &#246;ðrencilerinin mesajlarý gelsin.</asp:ListItem>
    </asp:DropDownList>
    <div style="clear: both; height: 0;">
    </div>
</asp:Content>
