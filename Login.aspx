<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Login.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="o_cph_main">
    <div>
    <div class="loginContainer">
        <div style="float: left;">
            <fieldset class="loginFieldset">
                <legend style="color: Black;">Giriş Bilgileri</legend>
                <div class="loginTable">
                    <div class="loginTable_left">
                        <div class="loginEleman">
                            <asp:Label ID="lblKullanici" runat="server" Text="Kullanıcı Adı : "></asp:Label>
                        </div>
                        <div class="loginEleman">
                            <asp:Label ID="lblParola" runat="server" Text="Parola : "></asp:Label>
                        </div>
                        <div class="loginEleman2">
                            <asp:Label ID="lblCaptcha" runat="server" Text="Güvenlik kodu : "></asp:Label>
                        </div>
                    </div>
                    <div class="loginTable_right">
                        <div class="DropDowns">
                            <asp:TextBox ID="txtKullanici" runat="server" Width="156px" MaxLength="13"></asp:TextBox>
                        </div>
                        <div class="DropDowns">
                            <asp:TextBox ID="txtParola" runat="server" Width="156px" TextMode="Password" 
                                MaxLength="15"></asp:TextBox>
                        </div>
                        <div class="captcha">
                            <asp:Image ID="img_captcha" runat="server" ImageUrl="~/JpegImage.aspx" />
                        </div>
                        <div class="DropDowns">
                            <asp:TextBox ID="txtCaptcha" runat="server" Width="156px" 
                                AutoCompleteType="None" MaxLength="6"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="loginButtonContainer">
                    <div class="loginButton">
                        <asp:ImageButton ID="imgbLogin" runat="server" ImageUrl="~/images/login.png" /></div>
                </div>
            </fieldset>
        </div>
        <div style="float: left; padding-left: 5px;">
            <fieldset class="loginFieldset">
                <legend style="color: #800000; font-weight: bold; font-size: 12px;">eKampüs' e Hoşgeldiniz</legend>
                <div class="loginBilgi">
                    <%= duyuruOku()%>
                </div>
            </fieldset>
        </div>
        <div style="clear: both;">
        </div>
        <div id="divBakim" class="bakim" runat="server">
            <div style="float: left; width: 48px; height: 48px; background-image: url('images/Grid/important.png')">
            </div>
            <div style="float: left; height: 38px; padding-top: 10px; padding-left: 5px;">
                <span>
                    <%=Session("BAKIMDURUM")%></span></div>
                    <div style="clear: both;"></div>
        </div>
    </div>
    </div>
</asp:Content>
