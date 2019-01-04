<%@ Page Language="VB" AutoEventWireup="false" CodeFile="vizeRapor.aspx.vb" Inherits="vizeRapor" %>


<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=4.0.10.423, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
	<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
		<Scripts>
			<%--Needed for JavaScript IntelliSense in VS2010--%>
			<%--For VS2008 replace RadScriptManager with ScriptManager--%>
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
		</Scripts>
	</telerik:RadScriptManager>    
        <telerik:ReportViewer ID="ReportViewer1" runat="server" Height="800px" 
            ReportBookID="ReportBookControl1" Width="780px">
        </telerik:ReportViewer>
        <telerik:ReportBookControl ID="ReportBookControl1" runat="server">
            <Reports>
                <telerik:ReportInfo Report="vizeRpr, App_Code.tnwjfask, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null">
                </telerik:ReportInfo>
            </Reports>
        </telerik:ReportBookControl>
    
    </div>
    </form>
</body>
</html>
