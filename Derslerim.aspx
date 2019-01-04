<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Derslerim.aspx.vb" Inherits="Derslerim_vize" %>

<asp:Content ID="Content3" ContentPlaceHolderID="o_cph_main" Runat="Server">
    <div class="div_derslerContainer">
        <fieldset class="derslerFieldset">
            <legend style="color: Black;">Atanmış olduğunuz dersler</legend>
            <div class="div_warning">
                <div class="div_innerwarn">
                    <asp:Label ID="lblUyari" runat="server" Font-Bold="True" ForeColor="#990000" EnableViewState="False"></asp:Label>
                </div>
            </div>
            <div class="dersler">
                <asp:GridView HorizontalAlign="Center" ID="grid_dersler" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="None" HeaderStyle-CssClass="GridHeader"
                    DataKeyNames="KATID,HOCASAY,ATANAN_ID,BIRIM,DERSKOD,DERSAD,VIZE_ONAY,FINAL_ONAY,BUT_ONAY,KRD,DONEM,YIL">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:TemplateField HeaderText="Ders Kodu">
                            <ItemTemplate>
                                <asp:Label ID="lblDerskod" runat="server" Text='<%# Bind ("DERSKOD") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" HorizontalAlign="Left" />
                            <ItemStyle Wrap="False" Width="80px" />
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
                        <asp:TemplateField HeaderText="Not Girişleri">
                            <ItemTemplate>
                                <div>
                                    <div>
                                        <asp:Button ID="btnVize" runat="server" Text='<%# Bind ("OGRSAY") %>' 
                                            Width="100px" onclick="btnVize_Click"/>
                                    </div>
                                </div>                                
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <div>
                                    <div>
                                        <asp:Button ID="btnListe" runat="server" Visible="false"/>
                                    </div>
                                </div>
                            </ItemTemplate>
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

