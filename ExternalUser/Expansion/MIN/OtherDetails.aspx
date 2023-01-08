<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="OtherDetails.aspx.cs"
    Inherits="ExternalUser_Expansion_MIN_OtherDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function GetData() {
          <%--  var txtGroundwaterAvailabilityDetails = document.getElementById("<%= txtGroundwaterAvailabilityDetails.ClientID %>")
            var txtGroundwaterAvailabilityDetails_array = document.getElementById("lbltxtGroundwaterAvailabilityDetails").value.split(' ');
            document.getElementById('lbltxtGroundwaterAvailabilityDetails').value = '( ' + parseInt(txtGroundwaterAvailabilityDetails_array[1] - txtGroundwaterAvailabilityDetails.value.length) + ' Character Left )';

            var txtRrainwaterDetails = document.getElementById("<%= txtRrainwaterDetails.ClientID %>")
            var txtRrainwaterDetails_array = document.getElementById("lbltxtRrainwaterDetails").value.split(' ');
            document.getElementById('lbltxtRrainwaterDetails').value = '( ' + parseInt(txtRrainwaterDetails_array[1] - txtRrainwaterDetails.value.length) + ' Character Left )';--%>

            var txtEarlierAppliedGWClearDesc = document.getElementById("<%= txtEarlierAppliedGWClearDesc.ClientID %>")
            var txtEarlierAppliedGWClearDesc_array = document.getElementById("lbltxtEarlierAppliedGWClearDesc").value.split(' ');
            document.getElementById('lbltxtEarlierAppliedGWClearDesc').value = '( ' + parseInt(txtEarlierAppliedGWClearDesc_array[1] - txtEarlierAppliedGWClearDesc.value.length) + ' Character Left )';

        }
        function CountCharacter(controlId, countControlId, maxCharlimit) {
            var charLeft = maxCharlimit - controlId.value.length;
            countControlId.value = '( ' + parseInt(maxCharlimit - controlId.value.length) + ' Character Left )';
            if (charLeft < 0) {
                countControlId.style.color = "Red";
                //msgCharacterleftId.style.color = "Red";
                //Bracket1.style.color = "Red";
                //Bracket2.style.color = "Red";
            }
            else {
                countControlId.style.color = "Black";
                //msgCharacterleftId.style.color = "Black";
                //Bracket1.style.color = "Black";
                //Bracket2.style.color = "Black";
            }
        }
    </script>
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
                                            <li class="active">Other Details</li>
                                        
                                            <li>Attachment</li>
                                                  <li>Ready To Submit</li>
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
                                MINING EXPANSION USE: Other Details
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="3">
                            (<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-
                            Upload Attachments in <strong style="color: Red">Attachment</strong> Section
                        </td>
                    </tr>
                   <%-- <tr>
                        <td style="width: 20px">
                            (19).
                        </td>
                        <td colspan="2">
                            Groundwater Availability Report ( Please Enclose a Comprehensive Report on Groundwater
                            Condition / Groundwater Quality in and Around 5Km of the Area) Map showing location
                            of groundwater regime monitoring wells, flow chart showing details of water requirment
                            and recycle water use and gainfull of pumped water- (<span class="Coumpulsory">$</span>)
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            Details:
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtGroundwaterAvailabilityDetails" runat="server" onkeyup="CountCharacter(this, this.form.lbltxtGroundwaterAvailabilityDetails, 500);"
                                onkeydown="CountCharacter(this, this.form.lbltxtGroundwaterAvailabilityDetails, 500);"
                                MaxLength="500" TextMode="MultiLine" Width="99%" Height="50px"></asp:TextBox><br />
                            <input type="text" id="lbltxtGroundwaterAvailabilityDetails" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtGroundwaterAvailabilityDetails" size="2" maxlength="2" value="( 500 Character Left )"
                                readonly="readonly" />
                            <asp:RegularExpressionValidator ID="revtxtGroundwaterAvailabilityDetails" runat="server"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtGroundwaterAvailabilityDetails"
                                ValidationGroup="OtherDetails"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtGroundwaterAvailabilityDetails" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="OtherDetails" ControlToValidate="txtGroundwaterAvailabilityDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            (20).
                        </td>
                        <td colspan="2">
                            Details of Rrainwater Harvesting and Artificial Recharge Measures Proposed / Implemented.
                            If Ground Water Recharge outside the
                            <br />
                            &nbsp;Industrial Unit Premises, then provide NOC from the Concerned Authority / Agency
                            if Already implemented, details may be furnished. (Attach Rainwater Harvesting /Artificial
                            Recharge Proposal)- (<span class="Coumpulsory">$</span>)
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            Details:
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtRrainwaterDetails" runat="server" MaxLength="500" onkeyup="CountCharacter(this, this.form.lbltxtRrainwaterDetails, 500);"
                                onkeydown="CountCharacter(this, this.form.lbltxtRrainwaterDetails, 500);" TextMode="MultiLine"
                                Width="99%" Height="50px"></asp:TextBox><br />
                            <input type="text" id="lbltxtRrainwaterDetails" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtRrainwaterDetails" size="2" maxlength="2" value="( 500 Character Left )"
                                readonly="readonly" />
                            <asp:RegularExpressionValidator ID="revtxtRrainwaterDetails" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtRrainwaterDetails" ValidationGroup="OtherDetails"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtRrainwaterDetails" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="OtherDetails" ControlToValidate="txtRrainwaterDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            (21).
                        </td>
                        <td colspan="2">
                            TOR/EC/Approval letter from statutory bodies viz Ministry of Environment & Forest
                            (MoEF) or State Pollution Control Board (SPCB) or State Level Expert Appraisal Committee
                            (SEAC) or State Level Environment Impact Assessment Authority (SLEIAA)
                            // Copy of Referral Letter seeking NOC from CGWA from Central Pollution Control Board
                            / State Pollution Control Board / Bureau of Indian Standards / Ministry of Environment
                            and Forests / Other Central / State Agencies shall be Annexed.//
                            - <span class="Coumpulsory">*</span>(<span class="Coumpulsory">$</span>)
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            Letter Number:
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtReferralLetterNo" runat="server" onkeyup="CountCharacter(this, this.form.lbltxtReferralLetterNo, 100);"
                                onkeydown="CountCharacter(this, this.form.lbltxtReferralLetterNo, 100);" MaxLength="100"
                                Width="99%"></asp:TextBox><br />
                            <input type="text" id="lbltxtReferralLetterNo" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtReferralLetterNo" size="2" maxlength="2" value="( 100 Character Left )"
                                readonly="readonly" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="OtherDetails" ControlToValidate="txtReferralLetterNo">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtReferralLetterNo" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtReferralLetterNo" ValidationGroup="OtherDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>--%>
                    <%--    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            <asp:GridView ID="gvMINRefLetter" runat="server" AutoGenerateColumns="False" Width="200px"
                                ShowHeaderWhenEmpty="true" CssClass="SubFormWOBG" OnRowDataBound="gvMINRefLetter_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Referral Letter Type Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="ReferralLetterTypeCode" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ReferralLetterTypeCode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Attached Referral Letter">
                                        <ItemTemplate>
                                            <asp:Label ID="ReferralLetterTypeName" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No Records exsist in MiningNewReferralLetter
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            Have you Applied Earlier for the Same Purpose with&nbsp; CGWA / State Ground Water
                            Authority:
                        </td>
                        <td style="width: 20px">
                            <asp:CheckBox ID="chkboxEarlierAppliedGWClear" runat="server" AutoPostBack="True"
                                OnCheckedChanged="chkboxEarlierAppliedGWClear_CheckedChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            If Yes, so Details thereof with Status:
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEarlierAppliedGWClearDesc" runat="server" MaxLength="500" Height="50px"
                                onkeyup="CountCharacter(this, this.form.lbltxtEarlierAppliedGWClearDesc, 500);"
                                onkeydown="CountCharacter(this, this.form.lbltxtEarlierAppliedGWClearDesc, 500);"
                                TextMode="MultiLine" Width="99%"></asp:TextBox><br />
                            <input type="text" id="lbltxtEarlierAppliedGWClearDesc" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtEarlierAppliedGWClearDesc" size="2" maxlength="2" value="( 500 Character Left )"
                                readonly="readonly" />
                            <asp:RegularExpressionValidator ID="revtxtEarlierAppliedGWClearDesc" runat="server"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtEarlierAppliedGWClearDesc"
                                ValidationGroup="OtherDetails"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtEarlierAppliedGWClearDesc" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="OtherDetails" ControlToValidate="txtEarlierAppliedGWClearDesc"></asp:RegularExpressionValidator>
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
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" ValidationGroup="OtherDetails"
                                OnClick="btnSaveAsDraft_Click" />
                            <asp:Button ID="txtNext" runat="server" Text="Next >>" ValidationGroup="OtherDetails"
                                OnClick="txtNext_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
