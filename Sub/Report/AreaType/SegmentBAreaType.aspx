<%@ Page Title="" Language="C#" MasterPageFile="~/Sub/SubReportMaster.master" AutoEventWireup="true" CodeFile="SegmentBAreaType.aspx.cs" 
Inherits="Sub_Report_AreaType_SegmentBAreaType" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <table width="70%" class="SubFormWOBG" align="center">
        <tr>
            <td colspan="4">
                <div class="div_AreaType" style="padding-left: 10px; font-size: 15px; height: 25px;
                    padding-top: 4px">
                    <b>List of Segment B Area Type</b>
                </div>
            </td>
        </tr>
        <tr>
            <td style="border-right:none">
                State Name:
            </td>
            <td style="border-right:none; border-left:none">
                <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                    Width="200px">
                </asp:DropDownList>
                <br />
            </td>
            <td style="border-right:none; border-left:none">
                District Name:
            </td>
            <td style="border-left:none">
                <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                    AutoPostBack="True" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align:center">
                <asp:Button ID="btnShowRecord" runat="server" Text="Show Record" OnClick="btnShowRecord_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" class="SubFormWOBG" align="center">
        <tr>
            <td>
                <center style="background-color: White;">
                    <rsweb:ReportViewer ID="RvResult" ShowPrintButton="true" runat="server" Width="100%"
                        Height="100%" AsyncRendering="false" SizeToReportContent="true">
                    </rsweb:ReportViewer>
                </center>
            </td>
        </tr>
    </table>
</asp:Content>

