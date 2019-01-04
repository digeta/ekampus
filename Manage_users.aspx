<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Manage_users.aspx.vb" Inherits="Manage_users" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="o_cph_lefttop" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="o_cph_main" Runat="Server">
    <strong>
        <asp:Label ID="lblUyari" runat="server" Font-Bold="True" Font-Names="Verdana"
            Font-Size="Small" ForeColor="Red" Visible="False"></asp:Label><br />
        <br />
        Kullanýcýlar</strong><br />
    <asp:GridView ID="o_grid_result" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" CellPadding="4" DataKeyNames="KISI_NO" DataSourceID="dsGrid"
        EnableTheming="True" ForeColor="#333333" GridLines="Vertical">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:CommandField ShowEditButton="True" ShowSelectButton="True" />
            <asp:BoundField DataField="KISI_NO" HeaderText="Kiþi no." SortExpression="KISI_NO" />
            <asp:BoundField DataField="TCKIMLIK" HeaderText="T.C. Kimlik" SortExpression="TCKIMLIK" />
            <asp:BoundField DataField="AD" HeaderText="Adý" SortExpression="AD" />
            <asp:BoundField DataField="SOYAD" HeaderText="Soyadý" SortExpression="SOYAD" />
            <asp:BoundField DataField="PAROLA" HeaderText="Parola" SortExpression="PAROLA" />
            <asp:BoundField DataField="AKTIF" HeaderText="Aktif" SortExpression="AKTIF" />
            <asp:BoundField DataField="EKLEYEN" HeaderText="Ekleyen" SortExpression="EKLEYEN" />
            <asp:BoundField DataField="KAYITTARIHI" HeaderText="Kayýt tarihi" SortExpression="KAYITTARIHI" />
        </Columns>
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <EmptyDataTemplate>
            NO~thing selected
        </EmptyDataTemplate>
        <EditRowStyle BackColor="#999999" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    <br />
    <strong>Yetkiler&nbsp;</strong><asp:DetailsView ID="o_dtl_user" runat="server" AutoGenerateRows="False"
        CellPadding="4" DataKeyNames="YETKI_ID" DataSourceID="dsDetails" ForeColor="#333333"
        GridLines="None" Height="50px" Width="208px">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
        <EditRowStyle BackColor="#999999" Width="208px" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <EmptyDataTemplate>
            Bu insana ait ayrý yetkilendirme yapmamýþsýnýz, kullanýcý ortak yetki kullanýyor.<br />
            <br />
            Ayrý yetkilendirme yapmak için basýnýz, butona.<br />
            <asp:Button ID="Buton" runat="server" OnClick="Buton_Click" Text="Bu o Buton" />
        </EmptyDataTemplate>
        <Fields>
            <asp:CheckBoxField DataField="ORTAKYETKI" HeaderText="ORTAKYETKI" SortExpression="ORTAKYETKI" />
            <asp:BoundField DataField="YETKI_ID" HeaderText="YETKI_ID" InsertVisible="False"
                ReadOnly="True" SortExpression="YETKI_ID" />
            <asp:BoundField DataField="KISI_NO" HeaderText="KISI_NO" SortExpression="KISI_NO" />
            <asp:CheckBoxField DataField="Y_PCEKLE" HeaderText="Y_PCEKLE" SortExpression="Y_PCEKLE" />
            <asp:CheckBoxField DataField="Y_HAREKETLI" HeaderText="Y_HAREKETLI" SortExpression="Y_HAREKETLI" />
            <asp:CheckBoxField DataField="Y_SUNUCUEKLE" HeaderText="Y_SUNUCUEKLE" SortExpression="Y_SUNUCUEKLE" />
            <asp:CheckBoxField DataField="Y_LABEKLE" HeaderText="Y_LABEKLE" SortExpression="Y_LABEKLE" />
            <asp:CheckBoxField DataField="Y_MISAFIREKLE" HeaderText="Y_MISAFIREKLE" SortExpression="Y_MISAFIREKLE" />
            <asp:CheckBoxField DataField="Y_OTURUMACMA" HeaderText="Y_OTURUMACMA" SortExpression="Y_OTURUMACMA" />
            <asp:BoundField DataField="Y_PCSAYISI" HeaderText="Y_PCSAYISI" SortExpression="Y_PCSAYISI" />
            <asp:BoundField DataField="Y_MOBILPCSAYISI" HeaderText="Y_MOBILPCSAYISI" SortExpression="Y_MOBILPCSAYISI" />
            <asp:BoundField DataField="Y_LABPCSAYISI" HeaderText="Y_LABPCSAYISI" SortExpression="Y_LABPCSAYISI" />
            <asp:BoundField DataField="Y_SUNUCUSAYISI" HeaderText="Y_SUNUCUSAYISI" SortExpression="Y_SUNUCUSAYISI" />
            <asp:BoundField DataField="Y_MISAFIRPCSAYISI" HeaderText="Y_MISAFIRPCSAYISI" SortExpression="Y_MISAFIRPCSAYISI" />
            <asp:CheckBoxField DataField="ISYETKILI" HeaderText="ISYETKILI" SortExpression="ISYETKILI" />
            <asp:CommandField ShowEditButton="True" />
        </Fields>
        <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:DetailsView>
    <asp:SqlDataSource ID="dsGrid" runat="server" UpdateCommand="UPDATE KURUMDISI SET AKTIF=@AKTIF WHERE KISI_NO=@KISI_NO&#13;&#10;" SelectCommand="SELECT * FROM KURUMDISI">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsDetails" runat="server" DeleteCommand="DELETE FROM [YETKILER] WHERE [YETKI_ID] = @YETKI_ID"
        InsertCommand="INSERT INTO [YETKILER] ([KISI_NO], [Y_PCEKLE], [Y_HAREKETLI], [Y_SUNUCUEKLE], [Y_LABEKLE], [Y_MISAFIREKLE], [Y_OTURUMACMA], [Y_PCSAYISI], [Y_MOBILPCSAYISI], [Y_LABPCSAYISI], [Y_SUNUCUSAYISI], [Y_MISAFIRPCSAYISI], [ISYETKILI]) VALUES (@KISI_NO, @Y_PCEKLE, @Y_HAREKETLI, @Y_SUNUCUEKLE, @Y_LABEKLE, @Y_MISAFIREKLE, @Y_OTURUMACMA, @Y_PCSAYISI, @Y_MOBILPCSAYISI, @Y_LABPCSAYISI, @Y_SUNUCUSAYISI, @Y_MISAFIRPCSAYISI, @ISYETKILI)"
        ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [YETKILER] WHERE KISI_NO=@KISI_NO AND ISYETKILI=1"
        UpdateCommand="UPDATE YETKILER SET  Y_PCEKLE = @Y_PCEKLE, Y_HAREKETLI = @Y_HAREKETLI, Y_SUNUCUEKLE = @Y_SUNUCUEKLE, Y_LABEKLE = @Y_LABEKLE, Y_MISAFIREKLE = @Y_MISAFIREKLE, Y_OTURUMACMA = @Y_OTURUMACMA, Y_PCSAYISI = @Y_PCSAYISI, Y_MOBILPCSAYISI = @Y_MOBILPCSAYISI, Y_LABPCSAYISI = @Y_LABPCSAYISI, Y_SUNUCUSAYISI = @Y_SUNUCUSAYISI, Y_MISAFIRPCSAYISI = @Y_MISAFIRPCSAYISI, ISYETKILI = @ISYETKILI WHERE YETKI_ID = @YETKI_ID">
        <DeleteParameters>
            <asp:Parameter Name="YETKI_ID" Type="Int64" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="Y_PCEKLE" Type="Boolean" />
            <asp:Parameter Name="Y_HAREKETLI" Type="Boolean" />
            <asp:Parameter Name="Y_SUNUCUEKLE" Type="Boolean" />
            <asp:Parameter Name="Y_LABEKLE" Type="Boolean" />
            <asp:Parameter Name="Y_MISAFIREKLE" Type="Boolean" />
            <asp:Parameter Name="Y_OTURUMACMA" Type="Boolean" />
            <asp:Parameter Name="Y_PCSAYISI" Type="Int32" />
            <asp:Parameter Name="Y_MOBILPCSAYISI" Type="Int32" />
            <asp:Parameter Name="Y_LABPCSAYISI" Type="Int32" />
            <asp:Parameter Name="Y_SUNUCUSAYISI" Type="Int32" />
            <asp:Parameter Name="Y_MISAFIRPCSAYISI" Type="Int32" />
            <asp:Parameter Name="ISYETKILI" Type="Boolean" />
            <asp:Parameter Name="YETKI_ID" Type="Int64" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="o_grid_result" Name="KISI_NO" PropertyName="SelectedValue" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="KISI_NO" Type="Int64" />
            <asp:Parameter Name="Y_PCEKLE" Type="Boolean" />
            <asp:Parameter Name="Y_HAREKETLI" Type="Boolean" />
            <asp:Parameter Name="Y_SUNUCUEKLE" Type="Boolean" />
            <asp:Parameter Name="Y_LABEKLE" Type="Boolean" />
            <asp:Parameter Name="Y_MISAFIREKLE" Type="Boolean" />
            <asp:Parameter Name="Y_OTURUMACMA" Type="Boolean" />
            <asp:Parameter Name="Y_PCSAYISI" Type="Int32" />
            <asp:Parameter Name="Y_MOBILPCSAYISI" Type="Int32" />
            <asp:Parameter Name="Y_LABPCSAYISI" Type="Int32" />
            <asp:Parameter Name="Y_SUNUCUSAYISI" Type="Int32" />
            <asp:Parameter Name="Y_MISAFIRPCSAYISI" Type="Int32" />
            <asp:Parameter Name="ISYETKILI" Type="Boolean" />
        </InsertParameters>
    </asp:SqlDataSource>
</asp:Content>

