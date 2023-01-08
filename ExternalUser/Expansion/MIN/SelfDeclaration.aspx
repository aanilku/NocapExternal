<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="SelfDeclaration.aspx.cs"
    Inherits="ExternalUser_Expansion_MIN_SelfDeclaration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Scripts/ClientSideDateValidation.js" type="text/javascript"></script>
    <script src="../../Scripts/Calendar/jquery-min-Calendar.js" type="text/javascript"></script>
    <script src="../../Scripts/Calendar/jquery-ui.min-Calendar.js" type="text/javascript"></script>
    <link href="../../Styles/Calendar/jquery-ui-Calendar.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("[id*=txtBharatKoshDated]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd/mm/yy',
                minDate: new Date('01/01/2015'),
                maxDate: new Date(),
                changeMonth: true,
                changeYear: true,
                showOn: 'both',
                buttonImage: '../../images/calendar.png'
            });
        });
 
        function ConfirmApproval(objMsg) {
            if (document.getElementById('<%= txtBharatKoshRefferenceNo.ClientID %>').value == "") {
                alert("Bharat Kosh transaction refference number  is mandatory.");
                return true;
            }
            if (document.getElementById('<%= txtBharatKoshDated.ClientID %>').value == "") {
                alert("Bharat Kosh transaction date is mandatory.");
                return true;
            }

        }</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
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
                                            <li class="active">Self Declaration</li>
                                            <li>Attachment</li>
                                            <li class="">Ready To Submit</li>
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
                        <td colspan="3">
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                MINING EXPANSION USE: Self Declaration
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="3">
                            (<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-Upload
                            Attachments in <strong style="color: Red">Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            <table class="SubFormWOBG" style="line-height: 25px" width="100%">
                                <tr>
                                    <td colspan="3">
                                        Processing Fee:&nbsp; <span class="Coumpulsory">* </span>(<span class="Coumpulsory">$</span>)
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        Bharat Kosh Transaction Ref. No:-
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBharatKoshRefferenceNo" runat="server" MaxLength="19"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revtxtBharatKoshRefferenceNo" runat="server"
                                            ValidationExpression="[0-9]*" ErrorMessage="Only Numeric values are allowed"
                                            ValidationGroup="SelfDeclaration" Display="Dynamic" ForeColor="Red" ControlToValidate="txtBharatKoshRefferenceNo"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        Bharat Kosh Transaction Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBharatKoshDated" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="^([0]?[0-9]|[12][0-9]|[3][01])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$"
                                            ErrorMessage="Invalid Date Format" ValidationGroup="SelfDeclaration" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtBharatKoshDated"></asp:RegularExpressionValidator>
                                        <br />
                                        <asp:RangeValidator ForeColor="Red" ValidationGroup="SelfDeclaration" runat="server"
                                            ID="rngDate" ControlToValidate="txtBharatKoshDated" Type="Date" MinimumValue="01/01/2015"
                                            ErrorMessage="Date should be grater than 01/01/2015 and less than or equal to current date." />
                                        <%--<asp:CalendarExtender ID="txtBharatKoshDatedCalendarExtender" runat="server" Enabled="True"
                                            TargetControlID="txtBharatKoshDated" PopupButtonID="imgbtntxtBharatKoshDated"
                                            Format="dd/MM/yyyy">
                                        </asp:CalendarExtender>
                                        <asp:ImageButton ID="imgbtntxtBharatKoshDated" runat="server" ImageUrl="~/Images/calendar.png" />
                                        &nbsp;<asp:CustomValidator ID="CVtxtBharatKoshDated" runat="server" OnServerValidate="ValidateDate"
                                            ControlToValidate="txtBharatKoshDated" ErrorMessage="Invalid Date." ForeColor="Red"
                                            ValidationGroup="SelfDeclaration" Display="Dynamic" ClientValidationFunction="ValidateDateOnClient" />
                                        <asp:RangeValidator ID="revtxtBharatKoshDated" runat="server" ValidationGroup="SelfDeclaration"
                                            ControlToValidate="txtBharatKoshDated" ErrorMessage="Date should be grater than 01/01/1990 and less than or equal to current date."
                                            Display="Dynamic" ForeColor="Red" MinimumValue="01/01/1990" Type="Date"></asp:RangeValidator>--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                      <%--  <td style="width: 20px">
                            <asp:CheckBox ID="chkBoxUndertaking" runat="server" />
                        </td>--%>
                        <td colspan="2">
                           <%-- It is to certify that no case related to ground water withdrawal/ contamination
                            is pending against the industry/ project/ unit as on date. Any such case filed against
                            the company/ project/ unit in respect of ground water withdrawal/ contamination
                            during the pendency of this application shall be immediately brought to the notice
                            of CGWA.<br />
                            It is to certify that the data and information furnished above are true to
                            the best of my knowledge and belief and I am aware that if any part of the data
                            / information submitted is Found to be false or misleading at any stage the application
                            will be rejected out rightly.<br /><br />
                            I hereby declare that all the mandatory documents prescribed in the application form are uploaded and no blank /irrelevant documents have been uploaded. I am also aware that any false and wrong submission /uploading of document will lead to rejection of my application.--%>
                            <%-- <asp:Label runat="server" Text="I hereby certify that the data and information furnished above are true to the best of my knowledge and belief and I am aware that if any part of the data / information submitted is found to be false or misleading at any stage, the application will be rejected outright."></asp:Label>                            
                                <br /><br />
                                <asp:Label runat="server" Text="I hereby declare that all the mandatory documents prescribed in the application form have been uploaded and no blank /irrelevant documents have been uploaded. I am also aware that any false/ wrong submission /uploading of document will lead to rejection of my application without any notice."></asp:Label>                            
                                <br /><br />                             
                                <asp:Label runat="server" Text="It is to certify that no case related to ground water withdrawal/ contamination is pending against the industry/ project/ unit as on date. Any such case filed against the company/ project/ unit in respect of ground water withdrawal/ contamination during the pendency of this application shall be immediately brought to the notice of CGWA."></asp:Label>
                                <br /><br />
                                <asp:Label runat="server" Text="I hereby undertake that in case any environmental compensation/ penalty is imposed on the firm by any  statutory authority, I shall comply with the decision of such authority."></asp:Label>                        
                            --%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            1.
                        </td>
                        <td colspan="2">
                            Application proforma is subject to modification from time to time.
                        </td>
                    </tr>
                    <tr>
                        <td>
                            2.
                        </td>
                        <td colspan="2">
                            Application will be submitted online on website http://cgwa-noc.gov.in to following office.<br />
                            <br />
                            <strong>Regional Director, Central Ground Water Board</strong><br />
                            <b>
                                <asp:Label ID="lblRegionalOffi" runat="server"></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            3.
                        </td>
                        <td colspan="2">
                            Incomplete application will be summarily rejected.
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <b class="Coumpulsory">Note :</b>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="Coumpulsory">
                            <asp:Label ID="lblProcessingFeeNonRefund" runat="server" Text=" a)  The Processing Fee is Non-Refundable. Applicant should 
                            ensure &quot;Check Eligibility&quot; and &quot;Documents Required&quot; before Submitting Application Online."
                                class="Coumpulsory"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblSecondNumber" runat="server" Text="b)" CssClass="Coumpulsory"></asp:Label>
                            <asp:Label ID="lblFee" runat="server" Text="" class="Coumpulsory"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                       <td colspan="3" class="Coumpulsory">
                            <asp:Label CssClass="Coumpulsory" ID="lblSubmittedApplication" runat="server" Text="c)  
                                 Scanned copy of signature and seal document should be attached at presribed place before submission of application.
                                <br />
                                Note:Signature and seal document can be obtain from Preview option in New-Save As Draft on Applicant Home Page.
                                "></asp:Label>
                            <%--<br />
                            <asp:Label Text="Last page of application can be obtain from Priview option in New-Save As Draft." runat="server" ></asp:Label>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblMiningApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="3">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('Please Save as Draft before moving to Previous Page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" ValidationGroup="SelfDeclaration"
                                OnClick="btnSaveAsDraft_Click" />
                            <asp:Button ID="btnNext" runat="server" Text="Next >>" ValidationGroup="SelfDeclaration"
                                OnClick="btnNext_Click" OnClientClick="return ConfirmApproval();" />
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" ValidationGroup="SelfDeclaration"
                                Text="TestSubmit" Visible="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
