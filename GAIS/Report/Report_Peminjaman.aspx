<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report_Peminjaman.aspx.cs" Inherits="GAIS.Report.Report_Peminjaman" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer id="ReportViewer1" style="width:100%;height:450px;" Visible="false" runat="server"></rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
