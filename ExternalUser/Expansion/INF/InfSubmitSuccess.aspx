<%@ Page Title="NOCAP-Infrastructure Application-Expansion" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" MaintainScrollPositionOnPostback="true"
    AutoEventWireup="true" CodeFile="InfSubmitSuccess.aspx.cs" Inherits="ExternalUser_Expansion_INF_InfSubmitSuccess" %>

<%@ PreviousPageType VirtualPath="Submit.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                            <li class="visited">Land Use Details</li>
                                            <li class="visited">Water Requirement Details</li>
                                            <li class="visited">De-Watering Existing Structure</li>
                                            <li class="visited">De-Watering Proposed Structure</li>
                                            <li class="visited">Breakup of Water Requirment</li>
                                            <li class="visited">Water Supply Detail</li>
                                            <li class="visited">Groundwater Abstraction Structure-Existing</li>
                                            <li class="visited">Groundwater Abstraction Structure-Proposed</li>
                                            <li class="visited">Other Details</li>
                                            <%--  <li class="visited">Self Declaration</li>--%>
                                            <li class="visited">Attachment</li>
                                            <li class="visited">Ready To Submit</li>
                                            <li class="visited">Final Submit</li>
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
                        <td colspan="2">
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                INFRASTRUCTURE EXPANSION USE : SUCCESSFUL SUBMISSION
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: right">
                            <asp:linkbutton id="lbtnPrint" runat="server" onclick="lbtnPrint_Click" postbackurl="~/ExternalUser/InfrastructureNew/Reports/INFReportViewer.aspx">Print Application</asp:linkbutton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:label id="msgSubmit" runat="server" text="Label"></asp:label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px">
                            <b>Application Number :</b>
                        </td>
                        <td>
                            <asp:label id="lblAppNo" runat="server" text="Label" font-bold="True"
                                font-size="Medium"></asp:label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Name of Infrastructure :</b>
                        </td>
                        <td>
                            <asp:label id="lblNameofIndustry" runat="server" text="Label"></asp:label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Submitted Date :</b>
                        </td>
                        <td>
                            <asp:label id="lblSubmitDate" runat="server" text="Label"></asp:label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Net Ground Water Requirement  (m<sup>3</sup>/day):</b>
                        </td>
                        <td>
                            <asp:label id="lblNetGroundWaterRequirement" runat="server" text="Label"></asp:label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:label id="lblRefMsg" runat="server" text="Label" forecolor="Black"></asp:label>
                        </td>
                    </tr>


                    <tr>
                        <td colspan="2">Your application has been submited to office:
                               <br />
                            <asp:label id="lblRegionalOfficeAddress" runat="server" text="" font-bold="True"></asp:label>
                        </td>
                    </tr>


                    <tr>
                        <td colspan="2">
                            <asp:label id="lblApplicationCode" runat="server" text="" visible="false"></asp:label>
                            <asp:label id="lblSentMailExternalUserStatus" runat="server" forecolor="Red"></asp:label>
                            <br />
                            <asp:label id="lblSentMailCommunicationalStatus" runat="server" forecolor="Red"></asp:label>
                            <br />
                            <asp:label id="lblSentSMSExternalUserStatus" runat="server" forecolor="Red"></asp:label>
                            <br />
                            <asp:label id="lblSentSMSCommunicationalStatus" runat="server" forecolor="Red"></asp:label>
                            <br />
                            <asp:label id="lblSendMsg" runat="server" visible="false" text="SMS not send to : " forecolor="Black"></asp:label>
                            <asp:label id="lblSMSNotSend" runat="server" text="" forecolor="Red"></asp:label>
                            <br />
                            <asp:label id="lblPushLicenseMessage" runat="server"></asp:label>
                            <br />
                            <asp:label id="lblPushLicenseStatusMessage" runat="server"></asp:label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>
