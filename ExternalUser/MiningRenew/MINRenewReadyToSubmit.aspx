<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="MINRenewReadyToSubmit.aspx.cs" Inherits="ExternalUser_MiningRenew_MINRenewReadyToSubmit" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table align="center" class="SubFormWOBG" width="100%">
        <tr>
            <td style="width: 200px">
                <table width="100%">
                    <tr>
                        <td>
                            <div class="middle">
                                <div class="block_left_inner">
                                    <div id="information" class="cont_left" style="display: block">
                                        <ul class="progressbar">
                                            <li class="visited">Location Details</li>
                                            <li class="visited">Communication Address</li>
                                            <li class="visited">Existing NOC Details</li>
                                            <li class="visited">Land Use Details</li>
                                            <li class="visited">Dewatering Existing Structure</li>
                                            <li class="visited">Dewatering Additional Structure</li>
                                            <li class="visited">Utilization of pumped water</li>
                                            <li class="visited">Monitoring of groundwater regime</li>
                                            <li class="visited">Groundwater Abstraction Structure- Existing</li>
                                            <li class="visited">Groundwater Abstraction Structure- Additional</li>
                                            <li class="visited">Compliance Conditions in the NOC</li>
                                            <li class="visited">Compliance Conditions in the NOC - Other</li>
                                            <li class="visited">Other Details</li>

                                            <li class="visited">Attachment</li>
                                            <li class="active">Ready To Submit</li>
                                            <li>Final Submit</li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table class="SubFormWOBG" width="100%" style="line-height: 25px">
                    <tr>
                        <td>
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                MINING Renew USE: Ready To Submit
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>Application Preview</td>
                    </tr>
                    <tr>
                        <td>
                            <div>

                                <center style="background-color: White;">
                                    <rsweb:ReportViewer ID="RvResult" runat="server" Width="100%" Height="100%" AsyncRendering="false"
                                        SizeToReportContent="true" ShowPrintButton="true" ShowRefreshButton="true">
                                    </rsweb:ReportViewer>
                                </center>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblMiningApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblFinalMsg" runat="server" Font-Bold="true"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('If you have not saved data, Your data will be lost before moving to other page ?');" OnClick="btnPrev_Click" />

                            <asp:Button ID="btnReadyToSubmitSubmit" runat="server" Text="Ready To Submit" OnClick="btnReadyToSubmitSubmit_Click" />
                        </td>
                    </tr>
                </table>
            </td>

        </tr>
    </table>
</asp:Content>

