<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="MinSubmitSuccess.aspx.cs"
    Inherits="ExternalUser_MiningNew_MinSubmitSuccess" %>

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
                                            <li class="visited">Dewatering Existing Structure</li>
                                            <li class="visited">Dewatering Proposed Structure</li>
                                            <li class="visited">Utilization of pumped water</li>
                                            <li class="visited">Monitoring of groundwater regime</li>
                                            <li class="visited">Groundwater Abstraction Structure- Existing</li>
                                            <li class="visited">Groundwater Abstraction Structure- Proposed</li>
                                            <li class="visited">Other Details</li>
                                
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
                                MINING USE : SUCCESSFUL SUBMISSION
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="2">
                            <asp:LinkButton ID="lbtnPrint" runat="server" OnClick="lbtnPrint_Click" PostBackUrl="~/ExternalUser/MiningNew/Reports/MINReportViewer.aspx">Print Application</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="msgSubmit" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px">
                            <b>Application Number :</b>
                        </td>
                        <td>
                            <asp:Label ID="lblAppNo" runat="server" Text="Label" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Name of Industry :</b>
                        </td>
                        <td>
                            <asp:Label ID="lblNameofIndustry" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Submitted Date :</b>
                        </td>
                        <td>
                            <asp:Label ID="lblSubmitDate" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblRefMsg" runat="server" Text="Label" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>
                    <%--  <tr>
                        <td colspan="2">
                            <asp:Label ID="lblFinalMsg" runat="server" Font-Bold="true" Font-Underline="true"
                                Text="This e-application will be processed only after receipt of printed form duly signed by the applicant along 
                            with all relevant enclosures at the Regional Director within seven (7) days of uploading completed application online. Please send your applicaiton to Given Address below. 
                            " ForeColor="Black"></asp:Label>
                        </td>
                    </tr>--%>
                 
                    <tr>
                        <td colspan="2">
                            Your application has been submited to office:
                            <br />
                            <asp:Label ID="lblRegionalOfficeAddress" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <%--<tr>
                        <td colspan="2">
                            <b class="Coumpulsory">Note:- </b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblProcessingFeeNonRefund" runat="server" Text=" a)
                               The Processing Fee is Non-Refundable. Applicant should 
                            ensure &quot;Check Eligibility&quot; and &quot;Documents Required&quot; before Submitting Application Online."
                                class="Coumpulsory"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblSecondNumber" runat="server" Text="b)" CssClass="Coumpulsory"></asp:Label>
                            <asp:Label ID="lblFee" runat="server" Text="" CssClass="Coumpulsory"></asp:Label>
                        </td>
                    </tr>

                      <tr>
                        <td colspan="2" class="Coumpulsory">

                            <asp:Label CssClass="Coumpulsory" ID="lblSubmittedApplication" runat="server" Text="c)    Submitted Application will not be Processed till the Print Out of the Signed
                            Complete Application is Submitted to Regional Office."></asp:Label>
                            
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblApplicationCode" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblSentMailExternalUserStatus" runat="server" ForeColor="Red"></asp:Label><br />
                            <asp:Label ID="lblSentMailCommunicationalStatus" runat="server" ForeColor="Red"></asp:Label><br />
                            <asp:Label ID="lblSentSMSExternalUserStatus" runat="server" ForeColor="Red"></asp:Label><br />
                            <asp:Label ID="lblSentSMSCommunicationalStatus" runat="server" ForeColor="Red"></asp:Label><br />
                            <asp:Label ID="lblSendMsg" runat="server" Visible="false" Text="SMS not send to : "
                                ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblSMSNotSend" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                         <br />
                            <asp:Label ID="lblPushLicenseMessage" runat="server"></asp:Label>
                              <br />
                            <asp:Label ID="lblPushLicenseStatusMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <
        </tr>
    </table>
</asp:Content>
