<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    AutoEventWireup="true" CodeFile="MINForExemLetter.aspx.cs" Inherits="ExternalUser_MiningNew_Reports_MINForExemLetter" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        #container
        {
            height: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center style="background-color: White;">
            <rsweb:ReportViewer ID="RvResult" runat="server" Width="100%" Height="100%" AsyncRendering="false"
                Visible="false" SizeToReportContent="true" ShowPrintButton="true" ShowRefreshButton="true">
            </rsweb:ReportViewer>
        </center>
    </div>
    <center>
        <asp:Label ID="lblMsg" runat="server" ForeColor="Green" Text="Your Data has been Submited Successfully !"></asp:Label>
        <br />
        <br />
        <div style="background-color: #094E7F; width: 180px; height: 23px; line-height: 22px;
            font-size: 12px; font-weight: bold;">
            <asp:LinkButton ID="lnkGenratePDF" runat="server" ForeColor="White" Width="180" Text="Download Exemption Letter"
                OnClick="lnkGenratePDF_Click"></asp:LinkButton>
        </div>
    </center>
</asp:Content>
