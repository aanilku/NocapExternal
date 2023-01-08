<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" MaintainScrollPositionOnPostback="true"
    AutoEventWireup="true" CodeFile="IndSubmitSuccess.aspx.cs" Inherits="ExternalUser_Expansion_IND_IndSubmitSuccess" %>

<%@ PreviousPageType VirtualPath="Submit.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .Initial {
            display: block;
            padding: 4px 18px 4px 18px;
            float: left; /* background: url("../Images/InitialImage.png") no-repeat right top;*/
            background-color: #EAEEF2;
            color: Black;
            font-weight: bold;
        }

            .Initial:hover {
                color: White; /* background: url("../Images/SelectedButton.png") no-repeat right top;*/
                background-color: #094E7F;
            }

        .Clicked {
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
                                            <li class="visited">Recycled Water Usage</li>
                                            <li class="visited">Groundwater Abstraction Structure- Existing</li>
                                            <li class="visited">Groundwater Abstraction Structure- Proposed</li>
                                            <li class="visited">Other Details</li>
                                            <%--     <li class="visited">Self Declaration</li>--%>
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
                        <td>
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                INDUSTRIAL EXPANSION USE : SUCCESSFUL SUBMISSION
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="margin-left: 50px; text-align: right">
                            <asp:LinkButton ID="lbtnPrint" runat="server" OnClick="lbtnPrint_Click" PostBackUrl="~/ExternalUser/IndustrialNew/Reports/INDReportViewer.aspx">Print Application</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="msgSubmit" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <b>Application Number :</b>
                            <asp:Label ID="lblAppNo" runat="server" Text="Label" Font-Bold="True"
                                Font-Size="Medium"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Name of Industry :</b>
                            <asp:Label ID="lblNameofIndustry" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Submitted Date</b>:
                            <asp:Label ID="lblSubmitDate" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Net Ground Water Requirement:</b>
                            <asp:Label ID="lblNetGroundWaterRequirement" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblRefMsg" runat="server" Text="Label" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td style="padding: 10px 10px 10px 10px">Your application has been submited to office:
                              <br />
                            <asp:Label ID="lblRegionalOfficeAddress" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">

                            <asp:Label ID="lblApplicationCode" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblSentMailExternalUserStatus" runat="server" ForeColor="Red"></asp:Label><br />
                            <asp:Label ID="lblSentMailCommunicationalStatus" runat="server" ForeColor="Red"></asp:Label><br />
                            <asp:Label ID="lblSentSMSExternalUserStatus" runat="server" ForeColor="Red"></asp:Label><br />
                            <asp:Label ID="lblSentSMSCommunicationalStatus" runat="server" ForeColor="Red"></asp:Label><br />
                            <asp:Label ID="lblSendMsg" runat="server" Visible="false" Text="SMS not send to : " ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblSMSNotSend" runat="server" Text="" ForeColor="Red"></asp:Label>

                            <br />
                            <asp:Label ID="lblPushLicenseMessage" runat="server"></asp:Label>
                            <br />
                            <asp:Label ID="lblPushLicenseStatusMessage" runat="server"></asp:Label>

                        </td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
</asp:Content>
