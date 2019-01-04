<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Authentication.aspx.vb" Inherits="Auth" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="o_cph_main" Runat="Server">
<asp:Label ID="o_lbl_oturum" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label><br />
<fieldset id="field_session" runat="server" style="width: 440px">    
    <asp:DropDownList ID="o_ddl_session" runat="server">
    </asp:DropDownList><asp:Button ID="o_btn_logon" runat="server" />
    <asp:Button ID="o_btn_logoff" runat="server" Text="Oturum kapat" Visible="False" />
</fieldset>
    <br />
    <asp:Label ID="lblUyari" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
</asp:Content>

