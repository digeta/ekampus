<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="Pass.aspx.vb" Inherits="pass" Title="Untitled Page" Async="true" AsyncTimeout="1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="o_cph_main" runat="Server">
    <ajaxToolkit:PasswordStrength ID="PasswordStrength1" runat="server" PrefixText="Zorluk Derecesi: "
        CalculationWeightings="50;15;15;20" MinimumNumericCharacters="2" TargetControlID="txtNewPwd"
        TextStrengthDescriptions="Çok Zayýf;Zayýf;Orta;Ýyi;Mükemmel" PreferredPasswordLength="8"
        Enabled="true" MinimumSymbolCharacters="1" RequiresUpperAndLowerCaseCharacters="false"
        TextStrengthDescriptionStyles="ps1;ps2;ps3;ps4;ps5"
        HelpHandlePosition="RightSide">
    </ajaxToolkit:PasswordStrength>

    <asp:Button ID="Button1" runat="server" Text="Gonderive" Visible="False" />
    <asp:Button ID="Button2" runat="server" Text="Ýptaledive" Visible="False" />
    <asp:Label ID="lblMailStatus" runat="server" Visible="False"></asp:Label>
    
                    <div class="parola">
                    <div style="text-align: center;"><asp:Label ID="lblUyari" runat="server" Font-Bold="True" ForeColor="#C00000" Visible="false"></asp:Label></div>
                    <fieldset class="parolaFieldset">
                        <legend>
                            Parola Deðiþtirme</legend>
                        <div style="padding: 5px;">
                        <div style="float: left; padding-right: 3px;">
                        <div style="height: 30px;"><span>Eski parolanýz: </span></div>
                        <div style="height: 30px;"><span>Yeni parolanýz: </span></div>
                        <div style="height: 30px;"><span>Yeni parolanýz (tekrar): </span></div>
                        </div>
                        <div style="float: left;">
                        <div style="height: 30px;">
                            <asp:TextBox ID="txtOldPwd" runat="server" 
                                TextMode="Password" Width="150px" MaxLength="15"></asp:TextBox></div>
                        <div style="height: 30px;"><asp:TextBox ID="txtNewPwd" runat="server" 
                                TextMode="Password" Width="150px"></asp:TextBox></div>
                        <div style="height: 30px;">
                            <asp:TextBox ID="txtNewPwd2" runat="server" 
                                TextMode="Password" Width="150px" MaxLength="15"></asp:TextBox></div>
                        <div style="height: 30px;">
                        <div style="float: right; height: 30px;"><asp:ImageButton ID="imgbUpdate" runat="server" 
                                ImageUrl="~/images/btnUpdate.png" ></asp:ImageButton>
                        </div>
                        </div>
                        </div>
                        <div style="clear: both;"></div>
                        </div>                        
                        </fieldset>
                        </div>
</asp:Content>
