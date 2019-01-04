<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Catalogue.aspx.vb" Inherits="Catalogue" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="o_cph_main" Runat="Server">
    <span style="color: #5d7b9d"><span style="font-size: 9pt; font-family: Arial"></span>
    </span>
    <asp:Panel ID="Panel2" runat="server" Height="50px" Width="608px">
    <table>
        <tr>
            <td>
            </td>
            <td>
                <strong><span style="font-size: 9pt; color: #5d7b9d; font-family: Arial">&nbsp;Fakülte
                    Seçiniz :</span></strong></td>
            <td>
                <asp:DropDownList ID="o_dlist_fak" runat="server" DataSourceID="dsFak" DataTextField="okulad1"
                    DataValueField="birim1" AutoPostBack="True" Width="336px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <strong><span style="font-size: 9pt; color: #5d7b9d; font-family: Arial">&nbsp;Bölüm
                    Seçiniz :</span></strong></td>
            <td>
                <asp:DropDownList ID="o_dlist_bol" runat="server" AutoPostBack="True" DataSourceID="dsBol" DataTextField="bolumad1" DataValueField="birim" Width="336px" Enabled="False">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <strong><span style="font-size: 9pt; color: #5d7b9d; font-family: Arial">&nbsp;Dönem
                    Seçiniz :</span></strong></td>
            <td style="text-align: left">
                <asp:DropDownList ID="o_dlist_term" runat="server" AutoPostBack="True" Enabled="False">
                    <asp:ListItem Value="-1">L&#252;tfen Se&#231;iniz</asp:ListItem>
                    <asp:ListItem Value="K1">G&#252;z</asp:ListItem>
                    <asp:ListItem Value="K2">Bahar</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
            </td>
            <td style="text-align: left">
                <asp:Button ID="o_btn_new" runat="server" Text="Yeni ders ekle" Visible="False" /></td>
        </tr>
    </table>
    </asp:Panel>
    <asp:Panel ID="PanelEdit" runat="server" Height="50px" Visible="False" Width="125px">
    <table><tr>
        <td colspan="1" rowspan="1" style="width: 283px; height: 23px">
        </td>
                        <td colspan="4" rowspan="1" style="width: 283px; height: 23px; text-align: left;">
                            <asp:Label ID="o_lbl_notice" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                ForeColor="Red"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="1" rowspan="3">
                            &nbsp;</td>
                        <td colspan="4" rowspan="3" style="text-align: left" >
                            <table>
                               
    
        <tr>
            <td style="font-weight: bold; font-size: 9pt; color: white; font-family: Arial; background-color: #5d7b9d">
                Ders Kodu</td>
            <td style="font-weight: bold; font-size: 9pt; color: white; font-family: Arial; background-color: #5d7b9d">
                Ders Adý</td>
            <td style="font-weight: bold; font-size: 9pt; color: white; font-family: Arial; background-color: #5d7b9d">
                T.Kr.</td>
            <td style="font-weight: bold; font-size: 9pt; color: white; font-family: Arial; background-color: #5d7b9d">
                P.Kr.</td>
            <td style="font-weight: bold; font-size: 9pt; color: white; font-family: Arial; background-color: #5d7b9d">
                Lab.</td>
            <td style="font-weight: bold; font-size: 9pt; color: white; font-family: Arial; background-color: #5d7b9d">
                Kr.</td>
            <td colspan="2" style="font-weight: bold; font-size: 9pt; color: white; font-family: Arial;
                background-color: #5d7b9d">
                Atanan</td>
            <td colspan="1" style="font-weight: bold; font-size: 9pt; color: white; font-family: Arial;
                background-color: #5d7b9d">
            </td>
        </tr>
        <tr>
            <td rowspan="3" style="background-color: #f7f6f3">
                <asp:TextBox ID="o_txt_dkod" runat="server" MaxLength="6" Width="56px"></asp:TextBox></td>
            <td rowspan="3" style="background-color: #f7f6f3">
                <asp:TextBox ID="o_txt_dadi" runat="server" MaxLength="60" Width="160px"></asp:TextBox></td>
            <td rowspan="3" style="background-color: #f7f6f3">
                <asp:TextBox ID="o_txt_tkr" runat="server" MaxLength="2" Width="20px"></asp:TextBox></td>
            <td rowspan="3" style="background-color: #f7f6f3">
                <asp:TextBox ID="o_txt_pkr" runat="server" MaxLength="2" Width="20px"></asp:TextBox></td>
            <td rowspan="3" style="background-color: #f7f6f3">
                <asp:TextBox ID="o_txt_lab" runat="server" MaxLength="2" Width="20px"></asp:TextBox></td>
            <td rowspan="3" style="background-color: #f7f6f3">
                <asp:TextBox ID="o_txt_krd" runat="server" MaxLength="2" Width="20px"></asp:TextBox></td>
            <td colspan="2" rowspan="3" style="background-color: #f7f6f3">
                <asp:DropDownList ID="o_dlist_ata" runat="server" DataSourceID="dsPerson" DataTextField="ADSOYAD" DataValueField="KURUMSICIL" Width="250px">
                </asp:DropDownList></td>
            <td colspan="1" rowspan="3" style="background-color: #f7f6f3">
                <asp:Button ID="o_btn_add" runat="server" Text="Ekle" Visible="False" />
                <asp:Button ID="o_btn_dupdate" runat="server" Text="Güncelle" /></td>
        </tr>
        <tr>
        </tr>
        <tr>
        </tr>
        
                            </table>
                            <asp:Label ID="o_lbl_error" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                ForeColor="Red"></asp:Label></td></tr>
    </table>
    </asp:Panel>
    <asp:Panel ID="Panel1" runat="server" Height="50px" Width="125px">
        <table>
            <tr>
                <td style="width: 100px">
                    &nbsp;&nbsp;</td>
                <td style="width: 100px">
                    <br />
    <asp:GridView AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" ID="GridView1" runat="server" ShowFooter="True" DataKeyNames="RECID" Width="655px" >
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:BoundField DataField="RECID" HeaderText="RECID" SortExpression="RECID" Visible="False" />
            <asp:BoundField DataField="derskod" HeaderText="Ders Kodu" SortExpression="derskod" />
            <asp:BoundField DataField="dersadi" HeaderText="Ders Adý" SortExpression="dersadi" />
            <asp:BoundField DataField="ATANAN" HeaderText="Atanan" ReadOnly="True" SortExpression="ATANAN" />
            <asp:CommandField ButtonType="Button" SelectText="D&#252;zenle" ShowSelectButton="True" />
            <asp:CommandField ButtonType="Button" DeleteText="Sil" ShowDeleteButton="True" />
        </Columns>
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Names="Arial" Font-Size="9pt" />
        <EditRowStyle BackColor="#999999" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Arial" Font-Size="9pt" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel3" runat="server" Height="50px" Width="125px">
    <asp:SqlDataSource ID="dsFak" runat="server" ConnectionString="Data Source=10.1.16.41;Initial Catalog=KARNE2;Persist Security Info=True;User ID=ezkuderskul;Password=alskdjfhg"></asp:SqlDataSource>
    <asp:SqlDataSource ID="dsBol" runat="server" ConnectionString="Data Source=10.1.16.41;Initial Catalog=KARNE2;Persist Security Info=True;User ID=ezkuderskul;Password=alskdjfhg"
        SelectCommand="SELECT bolumad1,birim FROM kisimlar WHERE LEFT(birim,2)=@birim">
        <SelectParameters>
            <asp:ControlParameter ControlID="o_dlist_fak" Name="birim" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsPerson" runat="server" ConnectionString="Data Source=10.1.16.41;Initial Catalog=proje1;Persist Security Info=True;User ID=ezkuderskul;Password=alskdjfhg"
        SelectCommand="SELECT     KURUMSICIL, AD + ' ' + SOYAD AS ADSOYAD&#13;&#10;FROM         PERSONEL&#13;&#10;WHERE     (SIL = 0) ORDER BY ADSOYAD">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsKatalogB" runat="server" ConnectionString="Data Source=10.1.16.41;Initial Catalog=proje1;Persist Security Info=True;User ID=ezkuderskul;Password=alskdjfhg"
        UpdateCommand="UPDATE KATALOGBAH SET derskod=@derskod,dersadi=@dersadi,teo=@teo,pra=@pra,lab=@lab,krd=@krd,ata=@ata WHERE RECID=@RECID" DeleteCommand="DELETE FROM KATALOGBAH WHERE RECID=@RECID" InsertCommand="INSERT INTO KATALOGBAH (birim,derskod,dersadi,teo,pra,lab,krd,ata) VALUES (@birim,@derskod,@dersadi,@teo,@pra,@lab,@krd,@ata)" SelectCommand="SELECT     RECID, derskod, dersadi, (SELECT     AD + ' ' + SOYAD ADSOYAD FROM PERSONEL WHERE KURUMSICIL = KATALOGBAH.ata) AS ATANAN FROM KATALOGBAH WHERE birim = @birim">
        <UpdateParameters>
            <asp:ControlParameter ControlID="o_txt_dkod" Name="derskod" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_txt_dadi" Name="dersadi" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_txt_tkr" Name="teo" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_txt_pkr" Name="pra" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_txt_lab" Name="lab" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_txt_krd" Name="krd" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_dlist_ata" Name="ata" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="GridView1" Name="RECID" PropertyName="SelectedValue" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="o_dlist_bol" Name="birim" PropertyName="SelectedValue" />
        </SelectParameters>
        <InsertParameters>
            <asp:ControlParameter ControlID="o_dlist_bol" Name="birim" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="o_txt_dkod" Name="derskod" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_txt_dadi" Name="dersadi" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_txt_tkr" Name="teo" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_txt_pkr" Name="pra" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_txt_lab" Name="lab" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_txt_krd" Name="krd" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_dlist_ata" Name="ata" PropertyName="SelectedValue" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsKatalogG" runat="server" ConnectionString="Data Source=10.1.16.41;Initial Catalog=proje1;Persist Security Info=True;User ID=ezkuderskul;Password=alskdjfhg"
        UpdateCommand="UPDATE KATALOGGUZ SET derskod=@derskod,dersadi=@dersadi,teo=@teo,pra=@pra,lab=@lab,krd=@krd,ata=@ata WHERE RECID=@RECID" DeleteCommand="DELETE FROM KATALOGGUZ WHERE RECID=@RECID" InsertCommand="INSERT INTO KATALOGGUZ (birim,derskod,dersadi,teo,pra,lab,krd,ata) VALUES (@birim,@derskod,@dersadi,@teo,@pra,@lab,@krd,@ata)" SelectCommand="SELECT     RECID, derskod, dersadi, (SELECT     AD + ' ' + SOYAD ADSOYAD FROM PERSONEL WHERE KURUMSICIL = KATALOGGUZ.ata) AS ATANAN FROM KATALOGGUZ WHERE birim = @birim">
        <UpdateParameters>
            <asp:ControlParameter ControlID="o_txt_dkod" Name="derskod" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_txt_dadi" Name="dersadi" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_txt_tkr" Name="teo" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_txt_pkr" Name="pra" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_txt_lab" Name="lab" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_txt_krd" Name="krd" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_dlist_ata" Name="ata" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="GridView1" Name="RECID" PropertyName="SelectedValue" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="o_dlist_bol" Name="birim" PropertyName="SelectedValue" />
        </SelectParameters>
        <InsertParameters>
            <asp:ControlParameter ControlID="o_dlist_bol" Name="birim" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="o_txt_dkod" Name="derskod" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_txt_dadi" Name="dersadi" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_txt_tkr" Name="teo" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_txt_pkr" Name="pra" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_txt_lab" Name="lab" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_txt_krd" Name="krd" PropertyName="Text" />
            <asp:ControlParameter ControlID="o_dlist_ata" Name="ata" PropertyName="SelectedValue" />
        </InsertParameters>
    </asp:SqlDataSource>
    </asp:Panel>
</asp:Content>

