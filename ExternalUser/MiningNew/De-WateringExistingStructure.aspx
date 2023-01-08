<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="De-WateringExistingStructure.aspx.cs"
    Inherits="ExternalUser_Mining_De_WateringExistingStructure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
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
        function CalculateSumGroundWaterReq() {
            var SumTreatedWaterDay = "0", SumTreatedWaterYear = "0";
            if (document.getElementById('<%=txtGWRequirementAbstractStruct.ClientID%>').value != "") {
                SumTreatedWaterDay = Number(SumTreatedWaterDay) + Number(document.getElementById('<%=txtGWRequirementAbstractStruct.ClientID%>').value);
            }
            if (document.getElementById('<%=txtGWrequiredMiningSeeping.ClientID%>').value != "") {
                SumTreatedWaterDay = Number(SumTreatedWaterDay) + Number(document.getElementById('<%=txtGWrequiredMiningSeeping.ClientID%>').value);
            }

            if (document.getElementById('<%=txtGWrequiredMiningSeepingYear.ClientID%>').value != "") {
                SumTreatedWaterYear = Number(SumTreatedWaterYear) + Number(document.getElementById('<%=txtGWrequiredMiningSeepingYear.ClientID%>').value);
            }
            if (document.getElementById('<%=txtGWRequirementAbstractStructYear.ClientID%>').value != "") {
                SumTreatedWaterYear = Number(SumTreatedWaterYear) + Number(document.getElementById('<%=txtGWRequirementAbstractStructYear.ClientID%>').value);
            }
            document.getElementById('<%=txtDayTotal.ClientID%>').value = Number(SumTreatedWaterDay).toFixed(2);
            document.getElementById('<%=txtYearTotal.ClientID%>').value = Number(SumTreatedWaterYear).toFixed(2);
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
                                            <li class="active">Dewatering Existing Structure</li>
                                            <li>Dewatering Proposed Structure</li>
                                            <li>Utilization of pumped water</li>
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
                        <td colspan="4">
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                MINING USE: De-Watering Requirment and Existing Structure Detail
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="4">
                            (<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-Upload
                            Attachments in <strong style="color: Red">Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">
                            (13).
                        </td>
                        <td style="width: 40%">
                            Whether the Groundwater Table will be Intersected by Activity :
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlDewateringRequired" runat="server" AutoPostBack="true" Width="200px"
                                OnSelectedIndexChanged="ddlDewateringRequired_SelectedIndexChanged">
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            a). At What Depth (m bgl)
                        </td>
                        <th>
                            <b>Pre-monsoon</b>
                        </th>
                        <th>
                            <b>Post-monsoon</b>
                        </th>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            Minimum (m bgl)
                        </td>
                        <td>
                            <asp:TextBox ID="txtMinimumPremansoon" runat="server" Width="99%" MaxLength="9"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtMinimumDepth" runat="server"
                                ControlToValidate="txtMinimumPremansoon" Display="Dynamic" ErrorMessage="Required"
                                ForeColor="Red" ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtMinimumPremansoon" runat="server" ControlToValidate="txtMinimumPremansoon"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMinimunPostMansoon" runat="server" Width="99%" MaxLength="9"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMinimunPostMansoon"
                                Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtMinimunPostMansoon" runat="server" ControlToValidate="txtMinimunPostMansoon"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            Maximum (m bgl)
                        </td>
                        <td>
                            <asp:TextBox ID="txtMaximumPreMansoon" runat="server" Width="99%" MaxLength="9"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtMaximumPreMansoon" runat="server" ControlToValidate="txtMaximumPreMansoon"
                                Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtMaximumPreMansoon" runat="server" ControlToValidate="txtMaximumPreMansoon"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMaximunPostMansoon" runat="server" Width="99%" MaxLength="9"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtMaximunPostMansoon" runat="server" ControlToValidate="txtMaximunPostMansoon"
                                Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtMaximunPostMansoon" runat="server" ControlToValidate="txtMaximunPostMansoon"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            b). Maximum Depth Proposed to Dewater (m bgl)
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtmaximumDeptProposed" runat="server" Width="200px" MaxLength="9"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtmaximumDeptProposed" runat="server" ControlToValidate="txtmaximumDeptProposed"
                                Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RequiredFieldValidator><br />
                            <asp:RegularExpressionValidator ID="revtxtmaximumDeptProposed" runat="server" Display="Dynamic"
                                ForeColor="Red" ValidationGroup="deWatringRequirementDetailsSubmit" ControlToValidate="txtmaximumDeptProposed"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            c). Groundwater Flow Direction (Attach Map)($)
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtGWFlowDirection" runat="server" Width="200px" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtGWFlowDirection" runat="server" ControlToValidate="txtGWFlowDirection"
                                Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RequiredFieldValidator><br />
                            <asp:RegularExpressionValidator ID="revtxtGWFlowDirection" runat="server" Display="Dynamic"
                                ControlToValidate="txtGWFlowDirection" ForeColor="Red" ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            d) Any Other Information
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtOtherInformation" runat="server" Width="200px" MaxLength="500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtOtherInformation" runat="server" ControlToValidate="txtOtherInformation"
                                Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RequiredFieldValidator><br />
                            <asp:RegularExpressionValidator ID="revtxtOtherInformation" Display="Dynamic" runat="server"
                                ControlToValidate="txtOtherInformation" ForeColor="Red" ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            (14).
                        </td>
                        <td>
                            <strong>Total Water Requirment(Fresh/Saline/Brackish Water Usage) for various Purpose to be Mentioned</strong>
                        </td>
                        <th>
                            (m<sup>3</sup>/day)
                        </th>
                        <th>
                            (m<sup>3</sup>/year)
                        </th>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            Ground Water Required through Abstract Structure
                        </td>
                        <td>
                            <asp:TextBox ID="txtGWRequirementAbstractStruct" runat="server" Width="99%" MaxLength="9"
                                onblur="CalculateSumGroundWaterReq()" Style="margin-bottom: 0px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtGWRequirementAbstractStruct" runat="server"
                                ControlToValidate="txtGWRequirementAbstractStruct" Display="Dynamic" ErrorMessage="Required"
                                ForeColor="Red" ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtGWRequirementAbstractStruct" runat="server"
                                Display="Dynamic" ControlToValidate="txtGWRequirementAbstractStruct" ForeColor="Red"
                                ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGWRequirementAbstractStructYear" runat="server" Width="99%"
                                onblur="CalculateSumGroundWaterReq()" MaxLength="12"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="revtxtGWRequirementAbstractStructYear" runat="server"
                                ControlToValidate="txtGWRequirementAbstractStructYear" Display="Dynamic" ErrorMessage="Required"
                                ForeColor="Red" ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revatxtGWRequirementAbstractStructYear" runat="server"
                                Display="Dynamic" ControlToValidate="txtGWRequirementAbstractStructYear" ForeColor="Red"
                                ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            Ground Water Abstracted on account of Dewatering / Mining Seepage
                        </td>
                        <td>
                            <asp:TextBox ID="txtGWrequiredMiningSeeping" runat="server" Width="99%" MaxLength="9"
                                onblur="CalculateSumGroundWaterReq()"></asp:TextBox><br />
                            <%--<asp:RequiredFieldValidator ID="rfvtxtGWrequiredMiningSeeping" runat="server" ControlToValidate="txtGWrequiredMiningSeeping"
                                Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RequiredFieldValidator>--%>
                            <asp:RegularExpressionValidator ID="revtxtGWrequiredMiningSeeping" runat="server"
                                Display="Dynamic" ControlToValidate="txtGWrequiredMiningSeeping" ForeColor="Red"
                                ValidationGroup="deWatringRequirementDetailsSubmit"> </asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGWrequiredMiningSeepingYear" runat="server" Width="99%" MaxLength="12"
                                onblur="CalculateSumGroundWaterReq()"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtGWrequiredMiningSeepingYear" runat="server"
                                ControlToValidate="txtGWrequiredMiningSeepingYear" Display="Dynamic" ErrorMessage="Required"
                                ForeColor="Red" ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtGWrequiredMiningSeepingYear" runat="server"
                                Display="Dynamic" ControlToValidate="txtGWrequiredMiningSeepingYear" ForeColor="Red"
                                ValidationGroup="deWatringRequirementDetailsSubmit"> </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <b>Total Ground Water Withdrawal :</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDayTotal" runat="server" Width="99%" Enabled="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtYearTotal" runat="server" Width="99%" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            (15).
                        </td>
                        <td colspan="3">
                            <b>Details of De-Watering Structure</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="3">
                            <b>(a). De-Watering Existing Structure</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">
                        </td>
                        <td>
                            Number of Existing Structures: <span class="Coumpulsory">*</span>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtNumberOfExistingDewaterStruct" runat="server" MaxLength="3" Width="200px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtNumberOfExistingDewaterStruct" runat="server"
                                ControlToValidate="txtNumberOfExistingDewaterStruct" Display="Dynamic" ErrorMessage="Required"
                                ForeColor="Red" ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RequiredFieldValidator><br />
                            <asp:RegularExpressionValidator ID="revtxtNumberOfExistingDewaterStruct" runat="server"
                                ForeColor="Red" ControlToValidate="txtNumberOfExistingDewaterStruct" Display="Dynamic"
                                ValidationGroup="deWatringRequirementDetailsSubmit"></asp:RegularExpressionValidator>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" Type="Integer" ControlToValidate="txtNumberOfExistingDewaterStruct"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="deWatringRequirementDetailsSubmit"
                                ErrorMessage="Value can't be more than 100" MaximumValue="100" MinimumValue="0"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3">
                            <fieldset>
                                <legend><b>Details:</b></legend>
                                <table class="SubFormWOBG" width="100%" style="line-height: 25px">
                                    <tr>
                                        <td style="width: 40%">
                                            Type of Structure: <span class="Coumpulsory">*</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTypeOfStructure" runat="server" Width="200px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                Display="Dynamic" InitialValue=" " ValidationGroup="deWatringRequirementDetails"
                                                ControlToValidate="ddlTypeOfStructure">Required</asp:RequiredFieldValidator>
                                            <!-- ValidationGroup="deWatringRequirementDetails"-->
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Year of Construction:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtYearOfConstruction" runat="server" MaxLength="4" Width="200px"></asp:TextBox><br />
                                            <asp:RangeValidator ID="RngValYearOfConstruction" runat="server" ControlToValidate="txtYearOfConstruction"
                                                Display="Dynamic" Type="Integer" MinimumValue="1900" MaximumValue="3000" ForeColor="Red"
                                                ErrorMessage="Invalid Year" ValidationGroup="deWatringRequirementDetails"></asp:RangeValidator>
                                            <asp:RegularExpressionValidator ID="revtxtYearOfConstruction" runat="server" ForeColor="Red"
                                                Display="Dynamic" ControlToValidate="txtYearOfConstruction" ValidationGroup="deWatringRequirementDetails"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Depth (Meter):
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDepthMeter" runat="server" MaxLength="7" Width="200px"></asp:TextBox><br />
                                            <asp:RangeValidator ID="RangeValidator2" runat="server" Type="Double" ControlToValidate="txtDepthMeter"
                                                Display="Dynamic" ForeColor="Red" ValidationGroup="deWatringRequirementDetails"
                                                ErrorMessage="Value Range 0.01 to 1000" MaximumValue="1000.00" MinimumValue="0.01"></asp:RangeValidator>
                                            <asp:RegularExpressionValidator ID="revtxtDepthMeter" runat="server" Display="Dynamic"
                                                ForeColor="Red" ControlToValidate="txtDepthMeter" ValidationGroup="deWatringRequirementDetails"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Diameter (mm):
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDiameterMM" runat="server" MaxLength="6" Width="200px"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="revtxtDiameterMM" runat="server" Display="Dynamic"
                                                ForeColor="Red" ControlToValidate="txtDiameterMM" ValidationGroup="deWatringRequirementDetails"></asp:RegularExpressionValidator>
                                            <asp:RangeValidator ID="RangeValidator3" runat="server" Display="Dynamic" Type="Integer"
                                                ControlToValidate="txtDiameterMM" ForeColor="Red" ValidationGroup="deWatringRequirementDetails"
                                                ErrorMessage="Value can't be more than 999999" MaximumValue="999999" MinimumValue="0"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Depth to Water Level (Meters below Ground Level):
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDepthWaterBelowGroundLevel" runat="server" MaxLength="7" Width="200px"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="revtxtDepthWaterBelowGroundLevel" runat="server"
                                                ForeColor="Red" Display="Dynamic" ControlToValidate="txtDepthWaterBelowGroundLevel"
                                                ValidationGroup="deWatringRequirementDetails"></asp:RegularExpressionValidator>
                                            <asp:RangeValidator ID="RangeValidator4" runat="server" Display="Dynamic" Type="Double"
                                                ControlToValidate="txtDepthWaterBelowGroundLevel" ForeColor="Red" ValidationGroup="deWatringRequirementDetails"
                                                ErrorMessage="Value Range 0.01 to 1000" MaximumValue="1000.00" MinimumValue="0.01"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Discharge (m&sup3/Hour):
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDischarge" runat="server" MaxLength="7" Width="200px"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="revtxtDischarge" runat="server" Display="Dynamic"
                                                ForeColor="Red" ControlToValidate="txtDischarge" ValidationGroup="deWatringRequirementDetails"></asp:RegularExpressionValidator>
                                            <asp:RangeValidator ID="RangeValidator5" runat="server" Display="Dynamic" Type="Double"
                                                ControlToValidate="txtDischarge" ForeColor="Red" ValidationGroup="deWatringRequirementDetails"
                                                ErrorMessage="Value Range 0.01 to 1000" MaximumValue="1000.00" MinimumValue="0.01"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Operational Hours/Day:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOperationalHoursDay" runat="server" MaxLength="2" Width="200px"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="revtxtOperationalHoursDay" runat="server" Display="Dynamic"
                                                ForeColor="Red" ControlToValidate="txtOperationalHoursDay" ValidationGroup="deWatringRequirementDetails"></asp:RegularExpressionValidator>
                                            <asp:RangeValidator ID="RangeValidator6" runat="server" Display="Dynamic" Type="Integer"
                                                ControlToValidate="txtOperationalHoursDay" ForeColor="Red" ValidationGroup="deWatringRequirementDetails"
                                                ErrorMessage="Value can't be more than 24" MaximumValue="24" MinimumValue="0"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Operational Days/Year:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOperationalDaysYear" runat="server" MaxLength="3" Width="200px"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="revtxtOperationalDaysYear" runat="server" Display="Dynamic"
                                                ForeColor="Red" ControlToValidate="txtOperationalDaysYear" ValidationGroup="deWatringRequirementDetails"></asp:RegularExpressionValidator>
                                            <asp:RangeValidator ID="RangeValidator7" runat="server" Display="Dynamic" Type="Integer"
                                                ControlToValidate="txtOperationalDaysYear" ForeColor="Red" ValidationGroup="deWatringRequirementDetails"
                                                ErrorMessage="Value can't be more than 365" MaximumValue="365" MinimumValue="0"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Mode of Lift:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlModeOfLift" runat="server" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Horse Power of Pump:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtHorsePowerPump" runat="server" MaxLength="6" Width="200px"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="revtxtHorsePowerPump" runat="server" ForeColor="Red"
                                                ControlToValidate="txtHorsePowerPump" Display="Dynamic" ValidationGroup="deWatringRequirementDetails"></asp:RegularExpressionValidator>
                                            <asp:RangeValidator ID="RangeValidator8" runat="server" Display="Dynamic" Type="Double"
                                                ControlToValidate="txtHorsePowerPump" ForeColor="Red" ValidationGroup="deWatringRequirementDetails"
                                                ErrorMessage="Value can't be more than 999.99" MaximumValue="999.99" MinimumValue="0.01"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Whether Fitted with Water Meter:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlWhetherFittedWithWaterMeter" runat="server" Width="200px">
                                                <asp:ListItem>Yes</asp:ListItem>
                                                <asp:ListItem>No</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Whether Permission / Registered with CGWA:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlWhetherPermissionRegisteredWithCGWA" runat="server" Width="200px"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlWhetherPermissionRegisteredWithCGWA_SelectedIndexChanged">
                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                <asp:ListItem Value="No">No</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            If so Details thereof:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWhetherPermissionRegisteredWithCGWADet" runat="server" MaxLength="100"
                                                onkeyup="CountCharacter(this, this.form.ActionCommentRemCount, 100);" onkeydown="CountCharacter(this, this.form.ActionCommentRemCount, 100);"
                                                TextMode="MultiLine" Width="300px" Height="50px"></asp:TextBox><br />
                                            <input type="text" id="ActionCommentRemCount" tabindex="-1" style="border-width: 0px;
                                                width: 100px; font-size: 10px; text-align: left; margin-left: 210px; background-color: transparent"
                                                name="ActionCommentRemCount" size="2" maxlength="2" value="( 100 Character Left )"
                                                readonly="readonly" /><br />
                                            <asp:RegularExpressionValidator ID="revtxtWhetherPermissionRegisteredWithCGWADet"
                                                runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtWhetherPermissionRegisteredWithCGWADet"
                                                ValidationGroup="deWatringRequirementDetails"></asp:RegularExpressionValidator>
                                            <asp:RegularExpressionValidator ID="revLengthtxtWhetherPermissionRegisteredWithCGWADet"
                                                runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="deWatringRequirementDetails"
                                                ControlToValidate="txtWhetherPermissionRegisteredWithCGWADet"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td></td>
                                        <td align="center" colspan="2">
                                            <asp:Button ID="btnAdd" runat="server" Text="Add In List" ValidationGroup="deWatringRequirementDetails"
                                                OnClick="btnAdd_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                                            <asp:Label ID="lblMiningApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvdeWateringExisting" Width="100%" runat="server" Caption="<th colspan='15'><b>Detail of De-Watering Existing Structure</b></th>"
                    AutoGenerateColumns="false" DataKeyNames="ApplicationCode" CssClass="SubFormWOBG"
                    OnRowCommand="gvdeWateringExisting_RowCommand" OnRowDataBound="gvdeWateringExisting_RowDataBound"
                    ShowHeaderWhenEmpty="True">
                    <Columns>
                        <asp:TemplateField HeaderText="SNo.">
                            <ItemTemplate>
                                <span>
                                    <%#Container.DataItemIndex + 1 %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Serial Number" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="SerialNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("SerialNumber")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type of Structure Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="TypeOfAbstractionStructureCode" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TypeOfAbstractionStructureCode")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type of Structure Name">
                            <ItemTemplate>
                                <asp:Label ID="TypeOfAbstractionStructureName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Year of Construction">
                            <ItemTemplate>
                                <asp:Label ID="YearOfConstruction" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("YearOfConstruction")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Depth (Meter)">
                            <ItemTemplate>
                                <asp:Label ID="DepthExist" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("DepthExist")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Diameter (mm)">
                            <ItemTemplate>
                                <asp:Label ID="Diameter" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Diameter")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Depth to Water level (Meters below Ground Level)">
                            <ItemTemplate>
                                <asp:Label ID="DepthBelowWaterLevel" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("DepthBelowWaterLevel")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Discharge (m3/Hour)">
                            <ItemTemplate>
                                <asp:Label ID="Discharge" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Discharge")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Operational Hours/Day">
                            <ItemTemplate>
                                <asp:Label ID="OperationalHousrDay" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("OperationalHousrDay")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Operational Days/Year">
                            <ItemTemplate>
                                <asp:Label ID="OperationalDaysYear" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("OperationalDaysYear")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mode of lift Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="LiftingDeviceCode" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("LiftingDeviceCode")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mode of lift Name">
                            <ItemTemplate>
                                <asp:Label ID="LiftingDeviceName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Horse Power of Pump">
                            <ItemTemplate>
                                <asp:Label ID="PowerOfPump" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("PowerOfPump")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Whether fitted with Water Meter">
                            <ItemTemplate>
                                <asp:Label ID="WaterFittedWithWaterMeterOrNot" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("WaterFittedWithWaterMeterOrNot")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Whether Permission/Registered with CGWA">
                            <ItemTemplate>
                                <asp:Label ID="WhetherPermissionRegisteredWithCGWA" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("WhetherPermissionRegisteredWithCGWA")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="If so Details Thereof">
                            <ItemTemplate>
                                <asp:Label ID="WhetherPermissionRegisteredWithCGWADetails" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("WhetherPermissionRegisteredWithCGWADetails")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnDelete" runat="server" Height="20px" ImageUrl="~/Images/delete.jpg"
                                    Width="20px" OnClientClick="return confirm('Are you sure you want to delete?');"
                                    CommandName="DeleteName" CommandArgument='<%#Container.DataItemIndex + 1 %>'
                                    ToolTip="Delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Records exist in De-Watering Existing Structure.
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="text-align: center">
                <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('Please Save as Draft before moving to Previous Page ?');"
                    OnClick="btnPrev_Click" />
                <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" ValidationGroup="deWatringRequirementDetailsSubmit"
                    OnClick="btnSaveAsDraft_Click" />
                <asp:Button ID="txtNext" runat="server" Text="Next >>" ValidationGroup="deWatringRequirementDetailsSubmit"
                    OnClick="txtNext_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
