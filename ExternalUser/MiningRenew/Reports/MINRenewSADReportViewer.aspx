<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" CodeFile="MINRenewSADReportViewer.aspx.cs" Inherits="ExternalUser_MiningRenew_Reports_MINRenewSADReportViewer" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center style="background-color: White;">
            <rsweb:ReportViewer ID="RvResult" runat="server" ShowPrintButton="true" ShowRefreshButton="true"
                AsyncRendering="false" SizeToReportContent="true" Width="100%" Height="100%">
            </rsweb:ReportViewer>

             

        </center>
    </div>
</asp:Content>
