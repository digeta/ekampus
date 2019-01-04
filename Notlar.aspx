<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="Notlar.aspx.vb" Inherits="Notlar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="o_cph_main" runat="Server">
    <div class="div_derslerContainer">
        <fieldset class="derslerFieldset">
            <legend style="color: Black;">Alm�� oldu�unuz dersler</legend>
            <div class="div_warning">
            <div style="width: 500px; margin: 0 auto;">
                <div style="float: left;">
                    <asp:DropDownList ID="ddlYil" runat="server">
                        <asp:ListItem Value="0">L�tfen Y�l Se�iniz</asp:ListItem>
                        <asp:ListItem Value="2009">2009</asp:ListItem>
                        <asp:ListItem Value="2010">2010</asp:ListItem>                        
                    </asp:DropDownList>
                </div>
                <div style="float: left; margin-left: 10px;">
                    <asp:DropDownList ID="ddlDonem" runat="server" AutoPostBack="True">
                        <asp:ListItem Value="0">L�tfen D�nem Se�iniz</asp:ListItem>
                        <asp:ListItem Value="1">G�z</asp:ListItem>
                        <asp:ListItem Value="2">Bahar</asp:ListItem>
                        <asp:ListItem Value="3">Yaz</asp:ListItem>
                    </asp:DropDownList>
                </div>                
            </div>
            </div>
            <div class="dersler">
                <asp:GridView ID="gridNotliste" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    GridLines="None" DataKeyNames="ID,VIZE_ONAY,FINAL_ONAY,BUT_ONAY,FINAL_GECTI,BUT_GECTI,KRD,BN_DEGERLENDIR,BAGILKODU,VIZE_ORT,FINAL_HBN,FINAL_BNO">
                    <RowStyle BackColor="#F7F6F3" />
                    <Columns>
                        <asp:TemplateField HeaderText="Derskodu">
                            <ItemTemplate>
                                <asp:Label ID="lblDerskod" runat="server" Text='<%# Eval ("DERSKOD") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ders ad�">
                            <ItemTemplate>
                                <asp:Label ID="lblDersad" runat="server" Text='<%# Eval ("DERSAD") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="True" Width="240px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Aras�nav">
                            <ItemTemplate>
                                <asp:Label ID="lblVize" runat="server" Text='<%# vizeNot() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" Width="20px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Final">
                            <ItemTemplate>
                                <asp:Label ID="lblFinal" runat="server" Text='<%# finalNot() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" Width="20px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="B�t.">
                            <ItemTemplate>
                                <asp:Label ID="lblBut" runat="server" Text='<%# butNot() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" Width="20px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ort.">
                            <ItemTemplate>
                                <asp:Label ID="lblOrt" runat="server" Text='<%# hbn() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" Width="20px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ba�ar�">
                            <ItemTemplate>
                                <asp:Label ID="lblBno" runat="server" Text='<%# bno() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" Width="20px" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle Font-Bold="True" CssClass="GridHeader" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            </div>
    </fieldset></div>
    <div class="BilgiMain">
        <fieldset class="BilgiFieldset">
            <legend style="color: Black;">Bilgilendirme</legend>
            <div>
                <div style="float: left; width: 48px;">
                    <div style="width: 48px; height: 48px; background-image: url('images/Grid/important.png');
                        margin-top: 7px;">
                    </div>
                </div>
                <div style="float: left; width: 720px;">
                    <div style="height: 40px; padding-top: 8px;">
                        Not veya Ba�ar� alanlar� bo� olan derslerinizin notlar� girilmemi� veya onaylanmam��t�r.<br />
                        Notunuz girilmi� olsa dahi ders sorumlusu taraf�ndan onaylanmad�k�a listede kar��l���
                        bo� g�z�kecektir.
                    </div>
                </div>
                <div style="clear: both;">
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>
