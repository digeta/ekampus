<%@ Page Language="VB" AutoEventWireup="false" CodeFile="nvize_krdz.aspx.vb" Inherits="nvize_krdz"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content3" ContentPlaceHolderID="o_cph_main" runat="Server">

    <script type="text/javascript" language="javascript">
        function onUpdating() {
            // get the update progress div
            //var updateProgressDiv = $get('updateProgressDiv');
            var updateProgressDiv = document.getElementById('updateProgressDiv');
            var divToHide1 = document.getElementById('divDersler');
            var divToHide2 = document.getElementById('divUyari');
            var btnKayit = document.getElementById('<%= notKaydet.ClientID %>');
            // make it visible
            updateProgressDiv.style.display = '';
            divToHide1.style.display = 'none';
            divToHide2.style.display = 'none';
            //btnHesap.disabled = true;
            btnKayit.disabled = true;
            //  get the gridview element        
            //var gridView = document.getElementById('<%= grid_notliste.ClientID %>');
            var gridView = document.getElementById('divNotsistem');

            // get the bounds of both the gridview and the progress div
            var gridViewBounds = Sys.UI.DomElement.getBounds(gridView);
            var updateProgressDivBounds = Sys.UI.DomElement.getBounds(updateProgressDiv);

            //	do the math to figure out where to position the element (the center of the gridview)
            //var x = gridViewBounds.x + Math.round(gridViewBounds.width / 2) - Math.round(updateProgressDivBounds.width / 2);
            //var y = gridViewBounds.y + Math.round(gridViewBounds.height / 2) - Math.round(updateProgressDivBounds.height / 2);
            //var y = 400;

            //	set the progress element to this position
            //Sys.UI.DomElement.setLocation (updateProgressDiv, x, y);        
        }

        function onUpdated() {
            // get the update progress div
            var updateProgressDiv = $get('updateProgressDiv');
            var divToHide1 = $get('divDersler');
            var divToHide2 = $get('divUyari');
            // make it invisible
            updateProgressDiv.style.display = 'none';
            divToHide1.style.display = '';
            divToHide2.style.display = '';
            var btnKayit = document.getElementById('<%= notKaydet.ClientID %>');

            //btnHesap.disabled = False;
            btnKayit.disabled = False;
        }    
    </script>

    <div id="divNotsistem" style="margin: 0 auto; width: 990px;">
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upanelExtender" BehaviorID="animation"
            runat="server" TargetControlID="updatePanel">
            <Animations>
                    <OnUpdating>
                        <Parallel duration="0">
                            <%-- place the update progress div over the gridview control --%>
                            <ScriptAction Script="onUpdating();" />  
                            <%-- disable the search button --%>                       
                            <EnableAction AnimationTarget="notKaydet" Enabled="false"/>
                            <%-- fade-out the GridView --%>
                            <FadeOut minimumOpacity=".8" />
                         </Parallel>
                    </OnUpdating>
                    <OnUpdated>
                        <Parallel duration="0">
                            <%-- fade back in the GridView --%>
                            <FadeIn minimumOpacity=".8" />
                            <%-- re-enable the search button --%>  
                            <EnableAction AnimationTarget="notKaydet" Enabled="true" />
                            <%--find the update progress div and place it over the gridview control--%>
                            <ScriptAction Script="onUpdated();" />                             
                        </Parallel> 
                    </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
        <asp:UpdatePanel ID="updatePanel" runat="server">
            <ContentTemplate>
                <div id="divUyari" style="height: 24px; text-align: center; padding-top: 3px; padding-bottom: 3px;">
                    <asp:Label ID="lblUyari" runat="server" Font-Bold="True" ForeColor="#C00000" Visible="False"></asp:Label><br />
                </div>
                <div id="updateProgressDiv" style="display: none; height: 90px; width: 630px; margin: 0 auto;
                    margin-top: 10px;">
                    <div style="width: 450px; height: 20px; margin: 0 auto; text-align: center; font-family: Verdana;
                        font-size: 15px; font-weight: bold;">
                        Lütfen Bekleyin.İşleminiz Yapılıyor.</div>
                    <div style="width: 450px; height: 70px; margin: 0 auto; text-align: center;">
                        <img alt="Lütfen Bekleyin..." src='images/ajax-loader.gif' style="border: 0px; margin: 0 auto;" /></div>
                </div>
                <div id="divDersler" class="notlar">
                    <fieldset class="notlarFieldset">
                        <legend>
                            <asp:Label ID="lblDersKodu" runat="server" ForeColor="#FF9900"></asp:Label></legend>
                        <div id="divButons1" style="margin: 0 auto; width: 450px;">
                            <div style="float: left; padding-left: 2px;">
                                <asp:Button ID="notKaydet" runat="server" Text="Notları Kaydet" />
                            </div>
                            <div style="float: left; padding-left: 2px;">
                                <asp:Button ID="notOnay" runat="server" Text="Notları Onayla" />
                            </div>
                            <div style="float: left; padding-left: 2px;">
                                <div style="float: left; padding-left: 2px; padding-top: 4px;">
                                    </div>
                                <div style="float: left; padding-left: 2px;">

                                </div>              
                            </div>
                            <div style="clear: both;"></div>
                        </div>
                        <div id="divNotOnay1" runat="server" style="margin: 0 auto; text-align: center;" visible="false">
                        <div style="margin-top: 5px;">
                            <span style="color: #FF0000;">Onay verme işlemi notlarınızın öğrenciler tarafından görüntülenebilmesini sağlar.<br />
                            Devam etmek istiyormusunuz.
                            </span>
                        </div>
                        <div style="width: 150px; padding-top: 2px;">
                            <div style="float: left;">
                                <asp:Button ID="btnOnayVar" runat="server" Text="Evet" />
                            </div>
                            <div style="float: left; padding-left: 30px;">
                                <asp:Button ID="btnOnayYok" runat="server" Text="Hayır" Font-Bold="True" />
                            </div>
                          <div style="clear: both;"></div>  
                        </div>
                        </div>
                        <div style="margin: 0 auto;">
                            <asp:GridView ID="grid_notliste" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                GridLines="None" DataKeyNames="ID,BN_DEGERLENDIR,V1,VIZE_ORT,VIZE_DURUM,DEVAM_DURUM">
                                <RowStyle BackColor="#F7F6F3" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Fakülte No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFakno" runat="server" Text='<%# Bind ("FAKNO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Öğrenci">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOgrenci" runat="server" Text='<%# Bind ("OGRENCI") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Devam durumu">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlDevam" runat="server" SelectedValue='<%# Bind ("DEVAM_DURUM") %>'>
                                                <asp:ListItem Value="True">Devam var</asp:ListItem>
                                                <asp:ListItem Value="False">Devamsız</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ara Sınav durum">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlAraSinav" runat="server" SelectedValue='<%# Bind ("VIZE_DURUM") %>'>
                                                <asp:ListItem Value="True">Girdi</asp:ListItem>
                                                <asp:ListItem Value="False">Girmedi</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Arasınav">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlV" runat="server" SelectedValue='<%# Bind("V1") %>'>
                                                <asp:ListItem Value="-1">Seçimsiz</asp:ListItem>
                                                <asp:ListItem Value="0">K-YZ</asp:ListItem>
                                                <asp:ListItem Value="100">G-YT</asp:ListItem>
                                            </asp:DropDownList>                                        
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" />
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
                        <div id="divButons2" style="margin: 0 auto; width: 450px; margin-top: 10px;">
                            <div style="float: left; padding-left: 2px;">
                                <asp:Button ID="notKaydet2" runat="server" Text="Notları Kaydet" />
                            </div>
                            <div style="float: left; padding-left: 2px;">
                                <asp:Button ID="notOnay2" runat="server" Text="Notları Onayla" />
                            </div>
                            <div style="float: left; padding-left: 2px; height: 30px;">
                                <div style="float: left; padding-left: 2px; padding-top: 4px;">
                                    </div>
                                <div style="float: left; padding-left: 2px;">

                                </div>
                            </div>
                            <div style="clear: both;"></div>
                        </div>
                        <div id="divNotOnay2" runat="server" style="margin: 0 auto; text-align: center;" visible="false">
                        <div style="margin-top: 5px;">
                            <span style="color: #FF0000;">Onay verme işlemi notlarınızın öğrenciler tarafından görüntülenebilmesini sağlar.<br />
                            Devam etmek istiyormusunuz.
                            </span>
                        </div>
                        <div style="width: 150px; padding-top: 2px;">
                            <div style="float: left;">
                                <asp:Button ID="btnOnayVar2" runat="server" Text="Evet" />
                            </div>
                            <div style="float: left; padding-left: 30px;">
                                <asp:Button ID="btnOnayYok2" runat="server" Text="Hayır" Font-Bold="True" />
                            </div>
                          <div style="clear: both;"></div>  
                        </div>
                        </div>                        
                    </fieldset>
                </div>
                <div class="BilgiMain">
                    <fieldset class="BilgiFieldset">
                        <legend style="color: Black;">Bilgilendirme</legend>
                        <div>
                            <div style="float: left; width: 48px;">
                                <div style="width: 48px; height: 48px; background-image: url('images/Grid/important.png'); margin-top: 7px;">
                                </div>
                                <div style="width: 48px; height: 48px; background-image: url('images/Grid/xxx.png'); margin-top: 7px;">
                                </div>
                                <div style="width: 48px; height: 48px; background-image: url('images/Grid/xxx.png'); margin-top: 7px;">
                                </div>
                                <div style="width: 48px; height: 48px; background-image: url('images/Grid/xxx.png'); margin-top: 7px;">
                                </div>
                            </div>
                            <div style="float: left; width: 720px;">
                            <div style="height: 40px; padding-top: 8px;">
                                Not girmek istemediğiniz alanları seçimsiz bırakabilirsiniz. Bu alanlar
                                ortalama hesaplanırken dikkate alınmayacaktır.
                            </div>
                            <div style="height: 55px;">
                                Daha önce kaydettiğiniz notu iptal etmek dolayısıyla öğrenciyi listenizden çıkarmak için <br />
                                not alanını seçimsiz bırakarak kaydetmelisiniz.
                            </div>
                            <div style="height: 47px; padding-top: 8px;">
                                <span style="text-decoration: underline; color: #FF0000;">&quot;Notları Onayla&quot;</span>ya 
                                tıklayıp işlemlerinize onayı vermediğiniz sürece, notlar öğrenciler tarafından 
                                görüntülenemeyecektir.<br />                                
                            </div>                            
                            <div style="height: 47px; padding-top: 8px;">
                                Not giriş işlemlerinde sorun yaşamamanız için notları girerken belli aralıklarla
                                <span style="text-decoration: underline; color: #FF0000;">&quot;Notları Kaydet&quot;</span>
                                e tıklamanız önerilir.
                            </div>
                            </div>
                            <div style="clear: both;"></div>                            
                            </div>
                    </fieldset>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="notKaydet" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
