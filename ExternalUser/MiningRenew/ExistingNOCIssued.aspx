<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="ExistingNOCIssued.aspx.cs"
    Inherits="ExternalUser_MiningRenew_ExistingNOCIssued" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function GetData() {

            var txtReasonfornotapplyingforrenewal = document.getElementById("<%= txtReasonfornotapplyingforrenewal.ClientID %>")
            var txtReasonfornotapplyingforrenewal_array = document.getElementById("lbltxtReasonfornotapplyingforrenewal").value.split(' ');
            document.getElementById('lbltxtReasonfornotapplyingforrenewal').value = '( ' + parseInt(txtReasonfornotapplyingforrenewal_array[1] - txtReasonfornotapplyingforrenewal.value.length) + ' Character Left )';

        }
        function CountCharacter(controlId, countControlId, maxCharlimit) {
            var charLeft = maxCharlimit - controlId.value.length;
            countControlId.value = '( ' + parseInt(maxCharlimit - controlId.value.length) + ' Character Left )';
            if (charLeft < 0) {
                countControlId.style.color = "Red";

            }
            else {
                countControlId.style.color = "Black";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>--%>
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
                                            <li class="active">Existing NOC Details</li>
                                            <li>Land Use Details</li>
                                            <li>Dewatering Existing Structure</li>
                                            <li>Dewatering Additional Structure</li>
                                            <li>Utilization of pumped water</li>
                                            <li>Monitoring of groundwater regime</li>
                                            <li>Groundwater Abstraction Structure- Existing</li>
                                            <li>Groundwater Abstraction Structure- Additional</li>
                                            <li>Compliance Conditions in the NOC</li>
                                            <li>Compliance Conditions in the NOC - Other</li>
                                            <li>Other Details</li>
                                 
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
                        <td colspan="4">
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                RENEW - MINING USE: General Information - Details of Existing NOC Issued by CGWA
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="FormProjName">
                            <b>Project Name:&nbsp;
                                <asp:Label ID="lblHeadingProjName" runat="server"></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <b>(5). Details of Existing NOC Issued by CGWA</b>
                        </td>
                    </tr>
                    <tr>
                        <%--<td>
                            NOC Letter No: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNOCLetterNo" runat="server" MaxLength="50" Width="200px"></asp:TextBox><br />
                             <asp:RequiredFieldValidator ID="efvtxtNOCLetterNo" runat="server" 
                                Display="Dynamic" ControlToValidate="txtNOCLetterNo" ForeColor="Red" ValidationGroup="ExistingNOC">Required</asp:RequiredFieldValidator>  
                            
                            <asp:RegularExpressionValidator ID="revtxtNOCLetterNo" runat="server" ForeColor="Red"
                    Display="Dynamic" ControlToValidate="txtNOCLetterNo" ValidationExpression="^([0-9]|[A-Za-z]|[(]|[)]|[/]|[-])*"
                    ErrorMessage="Invalid Characters"></asp:RegularExpressionValidator>

                        </td>--%>
                        <td style="width: 200px;">
                            NOC Letter No :
                        </td>
                        <td style="width: 200px;">
                            <asp:Label ID="lblNOCLetterNo" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <%-- <td>
                            Date of Issuance: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDateofIssuance" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                         

                              <asp:CalendarExtender ID="DateofIssuanceCalendarExtender" runat="server" 
                                TargetControlID="txtDateofIssuance" PopupButtonID="imgbtnCalendar" Format="dd/MM/yyyy">
                               </asp:CalendarExtender>
                            <asp:ImageButton ID="imgbtnCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                            
                            <br />
                            <asp:RequiredFieldValidator ID="rfvtxtDateofIssuance" runat="server"  
                                Display="Dynamic" ControlToValidate="txtDateofIssuance" ForeColor="Red" ValidationGroup="ExistingNOC">Required</asp:RequiredFieldValidator>                                
                            <asp:RangeValidator ID="rvtxtDateofIssuance" runat="server" Type="Date" Display="Dynamic"
                                ValidationGroup="ExistingNOC" ForeColor="Red" MinimumValue="01/01/1900"
                                ControlToValidate="txtDateofIssuance" ErrorMessage="Date of Issuance should be grater than 01/01/1900 and less than or equal to current date."></asp:RangeValidator>
                                 

                        </td>--%>
                        <td>
                            Date of Issuance:
                        </td>
                        <td>
                            <asp:Label ID="lblDateofIssuance" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <%-- <td>
                           Validity Start Date: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtValidityStartDate" runat="server" MaxLength="50" Width="200px"></asp:TextBox>

                             <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                TargetControlID="txtValidityStartDate" PopupButtonID="imgbtnCalendarStart" Format="dd/MM/yyyy">
                               </asp:CalendarExtender>
                            <asp:ImageButton ID="imgbtnCalendarStart" runat="server" ImageUrl="~/Images/calendar.png" /><br />
                            <asp:RequiredFieldValidator ID="rfvtxtValidityStartDate" runat="server" 
                                Display="Dynamic" ControlToValidate="txtValidityStartDate" ForeColor="Red" ValidationGroup="ExistingNOC">Required</asp:RequiredFieldValidator>                                
                            <asp:RangeValidator ID="regfvtxtValidityStartDate" runat="server" Type="Date" Display="Dynamic"
                                ValidationGroup="ExistingNOC" ForeColor="Red" MinimumValue="01/01/1900"
                                ControlToValidate="txtValidityStartDate" ErrorMessage="Date of Validity Start should be grater than 01/01/1900 and less than or equal to current date."></asp:RangeValidator>
                            
                        </td>--%>
                        <td>
                            Validity Start Date:
                        </td>
                        <td>
                            <asp:Label ID="lblValidityStartDate" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <%--  <td>
                           Validity End Date: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtValidityEndDate" runat="server" MaxLength="50" Width="200px"></asp:TextBox>

                           <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                                TargetControlID="txtValidityEndDate" PopupButtonID="imgbtnCalendarEnd" Format="dd/MM/yyyy">
                               </asp:CalendarExtender>
                            <asp:ImageButton ID="imgbtnCalendarEnd" runat="server" ImageUrl="~/Images/calendar.png" /><br />
                            <asp:RequiredFieldValidator ID="rfvtxtValidityEndDate" runat="server" 
                                Display="Dynamic" ControlToValidate="txtValidityEndDate" ForeColor="Red" ValidationGroup="ExistingNOC">Required</asp:RequiredFieldValidator>                                
                            <asp:RangeValidator ID="regfvtxtValidityEndDate" runat="server" Type="Date" Display="Dynamic"
                                ValidationGroup="ExistingNOC" ForeColor="Red" MinimumValue="01/01/1900"
                                ControlToValidate="txtValidityEndDate" ErrorMessage="Date of Validity End should be grater than 01/01/1900 and less than or equal to current date."></asp:RangeValidator>
                            
                        </td>--%>
                        <td>
                            Validity End Date:
                        </td>
                        <td>
                            <asp:Label ID="lblValidityEndDate" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Reason for not applying for renewal<br />
                            before expiry of NOC validity:
                        </td>
                        <td>
                            <asp:TextBox ID="txtReasonfornotapplyingforrenewal" runat="server" MaxLength="100"
                                onkeyup="CountCharacter(this, this.form.lbltxtReasonfornotapplyingforrenewal, 100);"
                                onkeydown="CountCharacter(this, this.form.lbltxtReasonfornotapplyingforrenewal, 100);"
                                TextMode="MultiLine" Width="400px" Height="100px"></asp:TextBox><br />
                            <input type="text" id="lbltxtReasonfornotapplyingforrenewal" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; margin-left: 320px; background-color: transparent"
                                name="lbltxtReasonfornotapplyingforrenewal" size="2" maxlength="2" value="( 100 Character Left )"
                                readonly="readonly" />
                            <asp:RegularExpressionValidator ID="revtxtReasonfornotapplyingforrenewal" runat="server"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtReasonfornotapplyingforrenewal"
                                ValidationGroup="ExistingNOC"></asp:RegularExpressionValidator><br />
                            <asp:RegularExpressionValidator ID="revLengthtxtReasonfornotapplyingforrenewal" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="ExistingNOC" ControlToValidate="txtReasonfornotapplyingforrenewal"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblMiningApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="4">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('If you have not saved data, Your data will be lost before moving to other page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" ValidationGroup="ExistingNOC"
                                OnClick="btnSaveAsDraft_Click" style="width: 115px" />
                            <asp:Button ID="txtNext" runat="server" Text="Next >>" ValidationGroup="ExistingNOC"
                                OnClick="txtNext_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
