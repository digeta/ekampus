<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Rapor_snfliste.aspx.vb" Inherits="Rapor_snfliste" %>

<asp:Content ID="Content1" ContentPlaceHolderID="o_cph_lefttop" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="o_cph_main" Runat="Server">
   <div class="div_derslerContainer">
        <fieldset class="derslerFieldset">
            <legend style="color: Black;">Atanmış olduğunuz dersler</legend>
            <div class="div_warning">
                <div class="div_innerwarn">
                    <asp:Label ID="lblUyari" runat="server" Font-Bold="True" ForeColor="#990000" EnableViewState="False"></asp:Label>
                </div>
            </div>
            <div class="div_warning">
                <div style="float: left">
                    <asp:DropDownList ID="ddlDonem" runat="server" AutoPostBack="True" 
                        Visible="False">
                        <asp:ListItem Value="0">Lütfen Önce Dönem Seçiniz</asp:ListItem>
                        <asp:ListItem Value="1">Güz</asp:ListItem>
                        <asp:ListItem Value="2">Bahar</asp:ListItem>
                        <asp:ListItem Value="3">Yaz</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>            
            <div class="dersler">
                <asp:GridView HorizontalAlign="Center" ID="gridDersler" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="None" HeaderStyle-CssClass="GridHeader"
                    
                    DataKeyNames="KATID,HOCASAY,ATANAN_ID,BIRIM,DERSKOD,DERSAD,VIZE_ONAY,FINAL_ONAY,BUT_ONAY,KRD,BOLUM_AD,OGRSAY,BAGILKODU">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:TemplateField HeaderText="Ders Kodu">
                            <ItemTemplate>
                                <asp:Label ID="lblDerskod" runat="server" Text='<%# Bind ("DERSKOD") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" HorizontalAlign="Left" />
                            <ItemStyle Wrap="False" Width="70px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ders Adı">
                            <ItemTemplate>
                                <asp:Label ID="lblDersad" runat="server" Text='<%# Bind ("DERSAD") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" HorizontalAlign="Left" />
                            <ItemStyle Wrap="true" Width="200px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fakülte">
                            <ItemTemplate>
                                <asp:Label ID="lblFakulte" runat="server" Text='<%# Bind ("FAKULTE_AD") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" HorizontalAlign="Left" />
                            <ItemStyle Wrap="False" Width="90px" />
                        </asp:TemplateField>                        
                        <asp:TemplateField HeaderText="Bölüm">
                            <ItemTemplate>
                                <asp:Label ID="lblBolum" runat="server" Text='<%# Bind ("BOLUM_AD") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" HorizontalAlign="Left" />
                            <ItemStyle Wrap="False" Width="220px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Döküm">
                            <ItemTemplate>
                                <div>
                                    <div>
                                        <asp:Button ID="btnVize" runat="server" Text="Arasınav"
                                            Width="70px" onclick="btnVize_Click"/>
                                    </div>
                                </div>                                
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Döküm">
                            <ItemTemplate>
                                <div>
                                    <div>
                                        <asp:Button ID="btnFinal" runat="server" Text="Genel Sınav"
                                            Width="80px" onclick="btnFinal_Click"/>
                                    </div>
                                </div>                                
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Döküm">
                            <ItemTemplate>
                                <div>
                                    <div>
                                        <asp:Button ID="btnBut" runat="server" Text="Büt."
                                            Width="50px" onclick="btnBut_Click"/>
                                    </div>
                                </div>                                
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Döküm">
                            <ItemTemplate>
                                <div>
                                    <div>
                                        <asp:Button ID="btnHepsi" runat="server" Text="Tümü"
                                            Width="50px" onclick="btnHepsi_Click"/>
                                    </div>
                                </div>                                
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"/>
                        </asp:TemplateField>                        
                    </Columns>
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" CssClass="GridHeader" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            </div>
        </fieldset>
    </div>
    <div style="clear: both;">
    </div>    
</asp:Content>

