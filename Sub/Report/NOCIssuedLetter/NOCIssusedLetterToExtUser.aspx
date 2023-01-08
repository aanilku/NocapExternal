<%@ Page Title="" Language="C#" MasterPageFile="~/Sub/SubReportMaster.master" AutoEventWireup="true"
    CodeFile="NOCIssusedLetterToExtUser.aspx.cs" Inherits="Sub_NOCIssuedLetter_NOCIssusedLetterToExtUser" %>

<%@ Register Src="~/AscxControl/ExternalUserLeftMenu.ascx" TagName="ExternalUserLeftMenu"
    TagPrefix="uc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <%--  <script src="../../Scripts/ClientSideDateValidation.js" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <table align="center" cellpadding="0" cellspacing="0" class="SubFormWOBG" width="100%">
        <tr>
            <td style="width: 200px">
                <uc1:ExternalUserLeftMenu ID="ExternalUserLeftMenu2" runat="server" />
            </td>
            <td>
                <div>
                    <table width="100%" class="SubFormWOBG" style="line-height: 25px">
                        <tr>
                            <th colspan="4">
                                <div class="div_IndAppHeading" style="padding-left: 10px; font-size: 18px;">
                                    List of Issued NOC - Online
                                </div>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp; Captcha Code:
                            </td>
                            <td>
                                <img src="../../../Images/StaticCaptcha.gif" width="120px" height="50px" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp; Enter Code:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCaptchaCode" runat="server" ToolTip="Enter Code"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center">
                                <asp:Button ID="btnShowRecord" runat="server" Text="Show Record" OnClick="btnShowRecord_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
                                </asp:ScriptManager>
                                <center>
                                    <rsweb:ReportViewer ID="RvResult" runat="server" Width="100%" AsyncRendering="false"
                                        ShowExportControls="false" SizeToReportContent="true" ShowPrintButton="true"
                                        ShowRefreshButton="true">
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
