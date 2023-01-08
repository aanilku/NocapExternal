<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"   MaintainScrollPositionOnPostback="true"
AutoEventWireup="true" CodeFile="OtherDetails.aspx.cs" Inherits="ExternalUser_MiningRenew_OtherDetails"
  Theme="Skin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function GetData() {
        var txtGainfulUtilazatoinOfPumpWaterDetails = document.getElementById("<%= txtGainfulUtilazatoinOfPumpWaterDetails.ClientID %>")
        var txtGainfulUtilazatoinOfPumpWaterDetails_array = document.getElementById("lbltxtGainfulUtilazatoinOfPumpWaterDetails").value.split(' ');
        document.getElementById('lbltxtGainfulUtilazatoinOfPumpWaterDetails').value = '( ' + parseInt(txtGainfulUtilazatoinOfPumpWaterDetails_array[1] - txtGainfulUtilazatoinOfPumpWaterDetails.value.length) + ' Character Left )';

        var txtRrainwaterDetails = document.getElementById("<%= txtRrainwaterDetails.ClientID %>")
        var txtRrainwaterDetails_array = document.getElementById("lbltxtRrainwaterDetails").value.split(' ');
        document.getElementById('lbltxtRrainwaterDetails').value = '( ' + parseInt(txtRrainwaterDetails_array[1] - txtRrainwaterDetails.value.length) + ' Character Left )';


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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                                            <li class="visited">Existing NOC Details</li>
                                            <li class="visited">Water Requirement Details</li>
                                            <li class="visited">Recycled Water Usage</li>
                                            <li class="visited">Groundwater Abstraction Structure- Existing</li>
                                            <li class="visited">Groundwater Abstraction Structure- Additional</li>
                                            <li class="visited">Compliance Conditions in the NOC</li>
                                            <li class="visited">Compliance Conditions in the NOC - Other</li>
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
                                RENEW - MINING USE: Other Details
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
                        <td style="text-align: right" colspan="3">
                            (<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-
                            Upload Attachments in <strong>Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">
                            19).
                        </td>
                        <td colspan="2">
                            Gainful utilaization of pump water.<br />
                            Details :
                            
                        </td>
                    </tr>
                 <%--   <tr>
                        <td style="width: 20px">
                        </td>
                        <td colspan="2">
                            Details :
                        </td>
                    </tr>--%>
                    <tr>
                        <td style="width: 20px">
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtGainfulUtilazatoinOfPumpWaterDetails" runat="server" onkeyup="CountCharacter(this, this.form.lbltxtGainfulUtilazatoinOfPumpWaterDetails, 2000);"
                                onkeydown="CountCharacter(this, this.form.lbltxtGainfulUtilazatoinOfPumpWaterDetails, 2000);"
                                MaxLength="2000" TextMode="MultiLine" Width="99%" Height="50px"></asp:TextBox><br />
                            <input type="text" id="lbltxtGainfulUtilazatoinOfPumpWaterDetails" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtGainfulUtilazatoinOfPumpWaterDetails" size="2" maxlength="2" value="( 2000 Character Left )"
                                readonly="readonly" />
                            <asp:RegularExpressionValidator ID="revtxtGainfulUtilazatoinOfPumpWaterDetails" runat="server"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtGainfulUtilazatoinOfPumpWaterDetails"
                                ValidationGroup="OtherDetails"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtGainfulUtilazatoinOfPumpWaterDetails" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="OtherDetails" ControlToValidate="txtGainfulUtilazatoinOfPumpWaterDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">
                            20).
                        </td>
                        <td colspan="2">
                            Details of Rrainwater Harvesting and Artificial Recharge Measures for Groundwater
                            Recharge in the Area.- (<span class="Coumpulsory">$</span>)<br />
                            Details :
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtRrainwaterDetails" runat="server" onkeyup="CountCharacter(this, this.form.lbltxtRrainwaterDetails, 500);"
                                onkeydown="CountCharacter(this, this.form.lbltxtRrainwaterDetails, 500);" MaxLength="500"
                                TextMode="MultiLine" Width="99%" Height="50px"></asp:TextBox><br />
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
                    <%--<tr>
                        <td>
                            7).
                        </td>
                        <td>
                            Have 
                            You Applied Earlier for Groundwater Clearance from CGWA / State
                            Government Agency:
                        </td>
                        <td style="width:20px">
                            <asp:CheckBox ID="chkboxEarlierAppliedGWClear" runat="server" 
                                AutoPostBack="True" 
                                oncheckedchanged="chkboxEarlierAppliedGWClear_CheckedChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="2">
                            If Yes, so Details thereof with Status:
                        </td>
                    </tr>--%>
                    <%--<tr>
                        <td></td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEarlierAppliedGWClearDesc" runat="server" onkeyup="CountCharacter(this, this.form.lbltxtEarlierAppliedGWClearDesc, 500);"
                                onkeydown="CountCharacter(this, this.form.lbltxtEarlierAppliedGWClearDesc, 500);"
                                MaxLength="500" TextMode="MultiLine" Width="99%" Height="50px"></asp:TextBox>
                            <input type="text" id="lbltxtEarlierAppliedGWClearDesc" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtEarlierAppliedGWClearDesc" size="2" maxlength="2" value="( 500 Character Left )"
                                readonly="readonly" />
                            <asp:RegularExpressionValidator ID="revtxtEarlierAppliedGWClearDesc" runat="server"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtEarlierAppliedGWClearDesc"
                                ValidationGroup="OtherDetails"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtEarlierAppliedGWClearDesc" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="OtherDetails"
                                ControlToValidate="txtEarlierAppliedGWClearDesc"></asp:RegularExpressionValidator>
                        </td>
                    </tr>--%>
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

