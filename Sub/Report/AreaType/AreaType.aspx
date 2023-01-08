<%@ Page Title="" Language="C#" MasterPageFile="~/Sub/SubReportMaster.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true"
    CodeFile="AreaType.aspx.cs" Inherits="Sub_Report_AreaType_AreaType" %>

<%@ Register Src="~/AscxControl/ExternalUserLeftMenu.ascx" TagName="ExternalUserLeftMenu"
    TagPrefix="uc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <table align="center" cellpadding="0" cellspacing="0" class="SubFormWOBG" width="100%">
        <tr>
            <td style="width:200px">
                <uc1:ExternalUserLeftMenu ID="ExternalUserLeftMenu2" runat="server" />
            </td>
            <td>
                <div>
                    <table width="100%" class="SubFormWOBG" style="line-height:25px">
                        <tr>
                            <th colspan="4">
                                <div class="div_IndAppHeading" style="padding-left:10px;font-size:18px;">
                                    Area Type
                                </div>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                State Name:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" Width="153px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                District Name:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                    AutoPostBack="True"  Width="153px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Sub-District Name:
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlSubDistrict" runat="server" AutoPostBack="True" 
                                    Width="153px" onselectedindexchanged="ddlSubDistrict_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <%--<td>
                                Area Type:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAreaType" runat="server" AutoPostBack="True" 
                                    Width="153px" OnSelectedIndexChanged="ddlAreaType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>--%>
                            <td >
                                Area Type Category:
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlAreaTypeCategory" runat="server" 
                                    Width="153px" AutoPostBack="True" onselectedindexchanged="ddlAreaTypeCategory_SelectedIndexChanged">
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
                        <tr>
                            <td colspan="4">
                                <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release"></asp:ScriptManager>
                                <center>
                                    <rsweb:ReportViewer ID="RvResult" runat="server" Width="100%"  AsyncRendering="false"
                                        SizeToReportContent="true" ShowPrintButton="true" ShowRefreshButton="true">
                                    </rsweb:ReportViewer>
                                </center>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
