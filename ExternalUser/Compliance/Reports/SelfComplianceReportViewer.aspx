<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"  MaintainScrollPositionOnPostback="true"
    AutoEventWireup="true" CodeFile="SelfComplianceReportViewer.aspx.cs" Inherits="ExternalUser_Compliance_Reports_SelfComplianceReportViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center style="background-color: White;">
            <rsweb:ReportViewer ID="RvResult" runat="server" Width="100%" Height="100%" AsyncRendering="false"
                SizeToReportContent="true" ShowPrintButton="true" ShowRefreshButton="true">
            </rsweb:ReportViewer>
        </center>
    </div>
</asp:Content>
