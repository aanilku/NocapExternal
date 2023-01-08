<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="ProposedUtiizationOfPumpedWater.aspx.cs"
    Inherits="ExternalUser_Mining_ProposedUtiizationOfPumpedWater" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function GetData() {
            var txtDomesticUseInMinesDesc = document.getElementById("<%= txtDomesticUseInMinesDesc.ClientID %>")
            var txtDomesticUseInMinesDesc_array = document.getElementById("lbltxtDomesticUseInMinesDesc").value.split(' ');
            document.getElementById('lbltxtDomesticUseInMinesDesc').value = '( ' + parseInt(txtDomesticUseInMinesDesc_array[1] - txtDomesticUseInMinesDesc.value.length) + ' Character Left )';


            var txtWaterSuplyDesc = document.getElementById("<%= txtWaterSuplyDesc.ClientID %>")
            var txtWaterSuplyDesc_array = document.getElementById("lbltxtWaterSuplyDesc").value.split(' ');
            document.getElementById('lbltxtWaterSuplyDesc').value = '( ' + parseInt(txtWaterSuplyDesc_array[1] - txtWaterSuplyDesc.value.length) + ' Character Left )';

            var txtAgricultureDesc = document.getElementById("<%= txtAgricultureDesc.ClientID %>")
            var txtAgricultureDesc_array = document.getElementById("lbltxtAgricultureDesc").value.split(' ');
            document.getElementById('lbltxtAgricultureDesc').value = '( ' + parseInt(txtAgricultureDesc_array[1] - txtAgricultureDesc.value.length) + ' Character Left )';


            var txtGBDevelopmentDesc = document.getElementById("<%= txtGBDevelopmentDesc.ClientID %>")
            var txtGBDevelopmentDesc_array = document.getElementById("lbltxtGBDevelopmentDesc").value.split(' ');
            document.getElementById('lbltxtGBDevelopmentDesc').value = '( ' + parseInt(txtGBDevelopmentDesc_array[1] - txtGBDevelopmentDesc.value.length) + ' Character Left )';


            var txtSuppervisionOfDustDesc = document.getElementById("<%= txtSuppervisionOfDustDesc.ClientID %>")
            var txtSuppervisionOfDustDesc_array = document.getElementById("lbltxtSuppervisionOfDustDesc").value.split(' ');
            document.getElementById('lbltxtSuppervisionOfDustDesc').value = '( ' + parseInt(txtSuppervisionOfDustDesc_array[1] - txtSuppervisionOfDustDesc.value.length) + ' Character Left )';

            var txtRechargeDesc = document.getElementById("<%= txtRechargeDesc.ClientID %>")
            var txtRechargeDesc_array = document.getElementById("lbltxtRechargeDesc").value.split(' ');
            document.getElementById('lbltxtRechargeDesc').value = '( ' + parseInt(txtRechargeDesc_array[1] - txtRechargeDesc.value.length) + ' Character Left )';

            var txtPumpedWaterAnyOtherItem = document.getElementById("<%= txtPumpedWaterAnyOtherItem.ClientID %>")
            var txtPumpedWaterAnyOtherItem_array = document.getElementById("lbltxtPumpedWaterAnyOtherItem").value.split(' ');
            document.getElementById('lbltxtPumpedWaterAnyOtherItem').value = '( ' + parseInt(txtPumpedWaterAnyOtherItem_array[1] - txtPumpedWaterAnyOtherItem.value.length) + ' Character Left )';

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
                                            <li class="active">Utilization of pumped water</li>
                                            <li>Monitoring of groundwater regime</li>
                                            <li>Groundwater Abstraction Structure- Existing</li>
                                            <li>Groundwater Abstraction Structure- Proposed</li>
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
                        <td colspan="5">
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                MINING USE: Proposed Utilization of Pumped Water
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="5">
                            (<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-
                            Upload Attachments in <strong style="color: Red">Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">
                            <b>(16).</b>
                        </td>
                        <td colspan="4">
                            <b>Proposed Utilization of Pumped Water (m<sup>3</sup>/year)</b> :</td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 20%">
                        </td>
                        <td style="width: 110px">
                        </td>
                        <td style="width: 20%">
                            <b>Value</b>
                        </td>
                        <td>
                            <b>Description</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 20%">
                            a). Domestic Use in Mines <span class="Coumpulsory">*</span>
                        </td>
                        <td style="width: 110px">
                            <asp:DropDownList ID="ddlDomesticUseInMines" runat="server" Width="100px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlDomesticUseInMines_SelectedIndexChanged">
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtDomesticUseInMines" runat="server" MaxLength="12" Width="98%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="revtxtDomesticUseInMines" runat="server" ControlToValidate="txtDomesticUseInMines"
                                Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="ProUtiPumpedWater"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revatxtDomesticUseInMines" runat="server" Display="Dynamic" ControlToValidate="txtDomesticUseInMines"
                                ForeColor="Red" ValidationGroup="ProUtiPumpedWater"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDomesticUseInMinesDesc" runat="server" Enabled="false" TextMode="MultiLine"
                                Height="50px" Width="98%" onkeyup="CountCharacter(this, this.form.lbltxtDomesticUseInMinesDesc, 100);"
                                onkeydown="CountCharacter(this, this.form.lbltxtDomesticUseInMinesDesc, 100);"></asp:TextBox><br />
                            <input type="text" id="lbltxtDomesticUseInMinesDesc" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtDomesticUseInMinesDesc" size="2" maxlength="2" value="( 100 Character Left )"
                                readonly="readonly" />
                            <asp:RegularExpressionValidator ID="revtxtDomesticUseInMinesDesc" runat="server"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtDomesticUseInMinesDesc"
                                ValidationGroup="ProUtiPumpedWater"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtDomesticUseInMinesDesc" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="ProUtiPumpedWater" ControlToValidate="txtDomesticUseInMinesDesc"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            b). Water Supply <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlWatersupply" runat="server" Width="100px" OnSelectedIndexChanged="ddlWatersupply_SelectedIndexChanged"
                                AutoPostBack="true">
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWaterSuply" runat="server" MaxLength="12" Width="98%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="revtxtWaterSuply" runat="server" ControlToValidate="txtWaterSuply"
                                Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="ProUtiPumpedWater"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revatxtWaterSuply" runat="server" ControlToValidate="txtWaterSuply"
                                ForeColor="Red" ValidationGroup="ProUtiPumpedWater"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWaterSuplyDesc" runat="server" Enabled="false" TextMode="MultiLine"
                                Height="50px" Width="98%" onkeyup="CountCharacter(this, this.form.lbltxtWaterSuplyDesc, 100);"
                                onkeydown="CountCharacter(this, this.form.lbltxtWaterSuplyDesc, 100);"></asp:TextBox>
                            <input type="text" id="lbltxtWaterSuplyDesc" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtWaterSuplyDesc" size="2" maxlength="2" value="( 100 Character Left )"
                                readonly="readonly" /><br />
                            <asp:RegularExpressionValidator ID="revtxtWaterSuplyDesc" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtWaterSuplyDesc" ValidationGroup="ProUtiPumpedWater"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtWaterSuplyDesc" runat="server" Display="Dynamic"
                                ForeColor="Red" ValidationGroup="ProUtiPumpedWater" ControlToValidate="txtWaterSuplyDesc"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            c). Agriculture <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAgriculture" runat="server" Width="100px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlAgriculture_SelectedIndexChanged">
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAgriculture" runat="server" MaxLength="12" Width="98%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="revddlAgriculture" runat="server" ControlToValidate="txtAgriculture"
                                Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="ProUtiPumpedWater"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtAgriculture" runat="server" ControlToValidate="txtAgriculture"
                                ForeColor="Red" ValidationGroup="ProUtiPumpedWater"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAgricultureDesc" runat="server" Enabled="false" TextMode="MultiLine"
                                Height="50px" Width="98%" onkeyup="CountCharacter(this, this.form.lbltxtAgricultureDesc, 100);"
                                onkeydown="CountCharacter(this, this.form.lbltxtAgricultureDesc, 100);"></asp:TextBox>
                            <input type="text" id="lbltxtAgricultureDesc" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtAgricultureDesc" size="2" maxlength="2" value="( 100 Character Left )"
                                readonly="readonly" /><br />
                            <asp:RegularExpressionValidator ID="revtxtAgricultureDesc" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtAgricultureDesc" ValidationGroup="ProUtiPumpedWater"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtAgricultureDesc" runat="server" Display="Dynamic"
                                ForeColor="Red" ValidationGroup="ProUtiPumpedWater" ControlToValidate="txtAgricultureDesc"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            d). Green Belt Development <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlGBDevelopment" runat="server" Width="100px" OnSelectedIndexChanged="ddlGBDevelopment_SelectedIndexChanged"
                                AutoPostBack="true">
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGBDevelopment" runat="server" MaxLength="12" Width="98%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="revtxtGBDevelopment" runat="server" ControlToValidate="txtGBDevelopment"
                                Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="ProUtiPumpedWater"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revatxtGBDevelopment" runat="server" ControlToValidate="txtGBDevelopment"
                                ForeColor="Red" ValidationGroup="ProUtiPumpedWater"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGBDevelopmentDesc" runat="server" Enabled="false" TextMode="MultiLine"
                                Height="50px" Width="98%" onkeyup="CountCharacter(this, this.form.lbltxtGBDevelopmentDesc, 100);"
                                onkeydown="CountCharacter(this, this.form.lbltxtGBDevelopmentDesc, 100);"></asp:TextBox>
                            <input type="text" id="lbltxtGBDevelopmentDesc" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtGBDevelopmentDesc" size="2" maxlength="2" value="( 100 Character Left )"
                                readonly="readonly" /><br />
                            <asp:RegularExpressionValidator ID="revtxtGBDevelopmentDesc" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtGBDevelopmentDesc" ValidationGroup="ProUtiPumpedWater"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtGBDevelopmentDesc" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="ProUtiPumpedWater" ControlToValidate="txtGBDevelopmentDesc"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            e). Suppression of Dust <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSuppresionOFDust" runat="server" AutoPostBack="true" Width="100px"
                                OnSelectedIndexChanged="ddlSuppresionOFDust_SelectedIndexChanged" Style="height: 22px">
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSuppervisionOfDust" runat="server" MaxLength="12" Width="98%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="revtxtSuppervisionOfDust" runat="server" ControlToValidate="txtSuppervisionOfDust"
                                Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="ProUtiPumpedWater"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revatxtSuppervisionOfDust" runat="server" ControlToValidate="txtSuppervisionOfDust"
                                ForeColor="Red" ValidationGroup="ProUtiPumpedWater"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSuppervisionOfDustDesc" runat="server" Enabled="false" TextMode="MultiLine"
                                Height="50px" Width="98%" onkeyup="CountCharacter(this, this.form.lbltxtSuppervisionOfDustDesc, 100);"
                                onkeydown="CountCharacter(this, this.form.lbltxtSuppervisionOfDustDesc, 100);"></asp:TextBox>
                            <input type="text" id="lbltxtSuppervisionOfDustDesc" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtSuppervisionOfDustDesc" size="2" maxlength="2" value="( 100 Character Left )"
                                readonly="readonly" /><br />
                            <asp:RegularExpressionValidator ID="revtxtSuppervisionOfDustDesc" runat="server"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtSuppervisionOfDustDesc"
                                ValidationGroup="ProUtiPumpedWater"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtSuppervisionOfDustDesc" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="ProUtiPumpedWater" ControlToValidate="txtSuppervisionOfDustDesc"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            f). Recharge <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRecharge" runat="server" Width="100px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlRecharge_SelectedIndexChanged">
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRecharge" runat="server" MaxLength="12" Width="98%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtRecharge" runat="server" ControlToValidate="txtRecharge"
                                Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="ProUtiPumpedWater"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtRecharge" runat="server" ControlToValidate="txtRecharge"
                                ForeColor="Red" ValidationGroup="ProUtiPumpedWater"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRechargeDesc" runat="server" Enabled="false" TextMode="MultiLine"
                                Height="50px" Width="98%" onkeyup="CountCharacter(this, this.form.lbltxtRechargeDesc, 100);"
                                onkeydown="CountCharacter(this, this.form.lbltxtRechargeDesc, 100);"></asp:TextBox>
                            <input type="text" id="lbltxtRechargeDesc" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtRechargeDesc" size="2" maxlength="2" value="( 100 Character Left )"
                                readonly="readonly" /><br />
                            <asp:RegularExpressionValidator ID="revtxtRechargeDesc" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtRechargeDesc" ValidationGroup="ProUtiPumpedWater"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtRechargeDesc" runat="server" Display="Dynamic"
                                ForeColor="Red" ValidationGroup="ProUtiPumpedWater" ControlToValidate="txtRechargeDesc"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            g). Any Other Item&nbsp;
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtPumpedWaterAnyOtherItem" runat="server" TextMode="MultiLine"
                                onkeyup="CountCharacter(this, this.form.lbltxtPumpedWaterAnyOtherItem, 100);"
                                onkeydown="CountCharacter(this, this.form.lbltxtPumpedWaterAnyOtherItem, 100);"
                                Height="52px" Width="98%" MaxLength="500"></asp:TextBox>
                            <input type="text" id="lbltxtPumpedWaterAnyOtherItem" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtPumpedWaterAnyOtherItem" size="2" maxlength="2" value="( 100 Character Left )"
                                readonly="readonly" /><br />
                            <asp:RegularExpressionValidator ID="revtxtPumpedWaterAnyOtherItem" runat="server"
                                ValidationGroup="ProUtiPumpedWater" Display="Dynamic" ForeColor="Red" ControlToValidate="txtPumpedWaterAnyOtherItem"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtPumpedWaterAnyOtherItem" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="ProUtiPumpedWater" ControlToValidate="txtPumpedWaterAnyOtherItem"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblMiningApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="5">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('Please Save as Draft before moving to Previous Page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" ValidationGroup="ProUtiPumpedWater"
                                OnClick="btnSaveAsDraft_Click" />
                            <asp:Button ID="btnNext" runat="server" Text="Next >>" OnClick="btnNext_Click" Style="width: 69px"
                                ValidationGroup="ProUtiPumpedWater" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
