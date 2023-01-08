﻿<%@ Page Title="NOCAP-Infrastructure Application-New" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="InfExemSubmitSuccess.aspx.cs" Inherits="ExternalUser_InfrastructureNew_InfExemSubmitSuccess" %>
<%@ PreviousPageType VirtualPath="SalientFeature.aspx" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
        .Initial
        {
            display: block;
            padding: 4px 18px 4px 18px;
            float: left; /* background: url("../Images/InitialImage.png") no-repeat right top;*/
            background-color: #EAEEF2;
            color: Black;
            font-weight: bold;
        }
        
        .Initial:hover
        {
            color: White; /* background: url("../Images/SelectedButton.png") no-repeat right top;*/
            background-color: #094E7F;
        }
        .Clicked
        {
            float: left;
            display: block; /*background: url("../Images/SelectedButton.png") no-repeat right top;*/
            background-color: #094E7F;
            padding: 4px 18px 4px 18px;
            color: Black;
            font-weight: bold;
            color: White;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <table align="center" class="SubFormWOBG" width="100%">
        <tr>
            <td style="width:200px">
                <table width="100%">
                    <tr>
                        <td>
                            <div class="middle">
                                <div class="block_left_inner">
                                    <div id="information" class="cont_left" style="display: block">
                                        <ul class="progressbar">
                                            <li class="visited">Location Details</li>
                                            <li class="visited">Communication Address</li>
                                            <li class="visited">Land Use Details</li>
                                            <li class="visited">Water Requirement Details</li>
                                            <li class="visited">De-Watering Existing Structure</li>
                                            <li class="visited">De-Watering Proposed Structure</li>
                                            <li class="visited">Breakup of Water Requirment</li>
                                            
                                            <li class="visited">Final Submit-Exemption Letter</li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table class="SubFormWOBG" width="100%" style="line-height:25px">
                    <tr>
                        <td colspan="2">
                            <div class="div_IndAppHeading" style="padding-left:10px">
                                INFRASTRUCTURE USE :
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="msgSubmit" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:200px">
                            Application Number :
                        </td>
                        <td>
                            <asp:Label ID="lblAppNo" runat="server" Text="Label" Font-Bold="True" 
                                Font-Size="Medium"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Name of Infrastructure :
                        </td>
                        <td>
                            <asp:Label ID="lblNameofIndustry" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Submitted Date :
                        </td>
                        <td>
                            <asp:Label ID="lblSubmitDate" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <fieldset>
                                <legend>Location</legend>
                                <table width="100%" class="SubFormWOBG">
                                    <tr>
                                        <td style="width:200px">
                                            State
                                        </td>
                                        <td>
                                            <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            District
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDistrict" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Sub District
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSubDistrict" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Quantity Applied for :
                        </td>
                        <td>
                            <asp:Label ID="lblQuentity" runat="server" Text=""></asp:Label>
                             <br />
                            <asp:Label ID="lblPushLicenseMessage" runat="server"></asp:Label>
                              <br />
                            <asp:Label ID="lblPushLicenseStatusMessage" runat="server"></asp:Label>
                              <br />
                            <asp:Label ID="lblPushDocumentAPIMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblRefMsg" runat="server" Text="Label" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblIndustialApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td colspan="2" style="text-align:center">
                            <asp:Button ID="btnExemLetter" runat="server" Text="Link To Exemption Letter"  Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblSentMailStatus" runat="server"  ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                            <td colspan="2">
                            <div>

                                <center style="background-color: White;">
                                    <rsweb:ReportViewer ID="RvResult" Visible="true" ShowPrintButton="true" runat="server"
                                        Width="100%" Height="100%" AsyncRendering="false" SizeToReportContent="true">
                                    </rsweb:ReportViewer>
                                </center>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
