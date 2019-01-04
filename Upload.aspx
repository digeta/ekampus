<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="Upload.aspx.vb" Inherits="Upload2" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="o_cph_main" runat="Server">
    <table width="100%">
        <tr>
            <td align="center">
                <div align="center">
                    <asp:Panel ID="Panelflsh" runat="server" Height="200px" Width="100%">

                        <script type="text/javascript">
        <!--
	var MM_contentVersion = 6;
	        var plugin = (navigator.mimeTypes && navigator.mimeTypes["application/x-shockwave-flash"]) ? navigator.mimeTypes["application/x-shockwave-flash"].enabledPlugin : 0;
		if (plugin) {
				var words = navigator.plugins["Shockwave Flash"].description.split(" ");
				for (var i = 0; i < words.length; ++i)
				{
				if (isNaN(parseInt(words[i])))
				continue;
				var MM_PluginVersion = words[i]; 
				}
			var MM_FlashCanPlay = MM_PluginVersion >= MM_contentVersion;
		}
		else if (navigator.userAgent && navigator.userAgent.indexOf("MSIE")>=0 
		   && (navigator.appVersion.indexOf("Win") != -1)) {
			document.write('<scr' + 'ipt language=VBScript\> \n'); //FS hide this from IE4.5 Mac by splitting the tag
			document.write('on error resume next \n');
			document.write('MM_FlashCanPlay = ( IsObject(CreateObject("ShockwaveFlash.ShockwaveFlash." & MM_contentVersion)))\n');
			document.write('</scr' + 'ipt\> \n');
		}
		if (MM_FlashCanPlay) {
			document.write('<object codebase="https://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0"');
			document.write('width="550" height="200" id="fileUpload" align="middle" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000">');
			document.write('<param name="allowScriptAccess" value="sameDomain" /><param name="movie" value="fileUpload.swf" /><param name="quality" value="high" /><param name="wmode" value="transparent"><param name="FlashVars" value="uploadPage=Upload.axd'+<%="'"+ctype(GetFlashVars(),string)+"'"%>+'&completeFunction=UploadComplete()&dizin='+<%="'"+ctype(Session("kisino"),string)+"'"%>+'">');
			document.write('<embed src="fileUpload.swf" flashvars="uploadPage=Upload.axd'+<%="'"+ctype(GetFlashVars(),string)+"'"%>+'&completeFunction=UploadComplete()&dizin='+<%="'"+ctype(Session("kisino"),string)+"'" %>+'"'); 
			document.write('quality="high" wmode="transparent" width="550" height="200" name="fileUpload" align="middle" allowscriptaccess="sameDomain" type="application/x-shockwave-flash"');
			document.write('pluginspage="https://www.macromedia.com/go/getflashplayer" />');
			document.write(' </object>');
		}else{
			document.write('<a href="https://www.macromedia.com/go/getflashplayer" target="_blank"><img src="/images/uyari.jpg" alt="Flash Eklentisi Bulunamadý" width="600" height="205" border="0" align="top" /></a>');
		}
//-->
                        </script>

                        <noscript>
                            <a href="https://www.macromedia.com/go/getflashplayer" target="_blank">
                                <img src="/images/uyari.jpg" alt="Flash Eklentisi Bulunamadý" width="600" height="205"
                                    border="0" align="top" /></a>
                        </noscript>
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>
                    </asp:Panel>
                </div>
                <br />
                <table width="100%">
                    <tr>
                        <td align="center">
                            <br />                            
                                <asp:Label ID="Label1" runat="server" Text="" Width="552px"></asp:Label>
                                <div>
                                    <table>
                                        <tr>
                                            <td id="td1_top" style="background-image: url('images/img_cihaz/header1.png'); background-repeat: repeat-x;
                                                height: 25px;">
                                                <div style="width: 100px; float: left;">
                                                    <strong><span style="font-size: 10pt; color: #ffffff; font-family: Verdana">Dosya adý</span></strong></div>
                                                <div style="width: 100px; float: left;">
                                                    <strong><span style="font-size: 10pt; color: #ffffff; font-family: Verdana">Boyutu</span></strong></div>
                                                <div style="width: 120px; float: left;">
                                                    <strong><span style="font-size: 10pt; color: #ffffff; font-family: Verdana">Virüs taramasý</span></strong></div>
                                                <div style="width: 100px; float: left;">
                                                    <strong><span style="font-size: 10pt; color: #ffffff; font-family: Verdana">Flash mý</span></strong></div>
                                                <div style="width: 120px; float: left;">
                                                    <strong><span style="font-size: 10pt; color: #ffffff; font-family: Verdana">Yükleme tarihi</span></strong></div>
                                                <div style="width: 90px; float: left;">
                                                    <strong><span style="font-size: 10pt; color: #ffffff; font-family: Verdana">Görüntüle</span></strong></div>
                                                <div style="width: 60px; float: left;">
                                                    <strong><span style="font-size: 10pt; color: #ffffff; font-family: Verdana">Sil</span></strong></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="o_div_dersler" style="overflow: auto; height: 250px; margin: 0;">
                                                    <asp:GridView ID="o_grid_files" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                        Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" GridLines="None" ShowHeader="False"
                                                        DataKeyNames="ID">
                                                        <PagerSettings FirstPageImageUrl="~/images/img_cihaz/first_page.png" LastPageImageUrl="~/images/img_cihaz/last_page.png"
                                                            Mode="NextPreviousFirstLast" NextPageImageUrl="~/images/img_cihaz/next_page.png"
                                                            PreviousPageImageUrl="~/images/img_cihaz/previous_page.png" />
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                        <Columns>
                                                            <asp:BoundField DataField="AD" HeaderText="Dosya adý" SortExpression="AD">
                                                                <ItemStyle Width="90px" Wrap="True" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Boyutu" SortExpression="BOYUT">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("BOYUT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Vir&#252;s tarama" SortExpression="AV">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("AV") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="120px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Flash &#231;evrimi" SortExpression="SWF">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("SWF") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Y&#252;kleme tarihi" SortExpression="TARIH">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("TARIH") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="120px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="G&#246;r&#252;nt&#252;le">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgb_view" runat="server" ImageUrl="~/images/view.png" 
                                                                        onclick="imgb_view_Click" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sil">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgb_del" runat="server" 
                                                                        ImageUrl="~/images/img_cihaz/Red.gif" onclick="imgb_del_Click" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="40px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="o_chk_dosya" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="Transparent" CssClass="DerslerGridHeader" Font-Bold="True"
                                                            ForeColor="White" />
                                                        <PagerStyle BackColor="Transparent" CssClass="GridFooter" ForeColor="White" HorizontalAlign="Center" />
                                                        <EmptyDataTemplate>
                                                            Henüz bir bölüm seçmediniz
                                                        </EmptyDataTemplate>
                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="Transparent" CssClass="DerslerGridHeader" Font-Bold="True"
                                                            ForeColor="White" />
                                                        <EditRowStyle BackColor="#999999" />
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="td1_bottom" style="background-image: url('images/img_cihaz/header1.png');
                                                background-repeat: repeat-x; height: 25px;">
                                                <div style="float: right;">
                                                    <asp:ImageButton ID="imgb_addselection" runat="server" ImageUrl="~/images/rem_selection.png" /></div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                                        <asp:Label ID="o_lbl_error" runat="server" ForeColor="red" Text=""></asp:Label>
                            <asp:Label ID="o_lbl_notice" runat="server" ForeColor="red" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
