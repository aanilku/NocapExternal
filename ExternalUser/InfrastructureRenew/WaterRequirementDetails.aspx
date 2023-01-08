<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    AutoEventWireup="true" CodeFile="WaterRequirementDetails.aspx.cs" Inherits="ExternalUser_InfrastructureRenew_WaterRequirementDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function SumTWR() {
            var txtGroundWaterRequirementTotal = "";
            var totalExisting = "0";
            var totalAdditional = "0";

            if (document.getElementById('<%= txtGroundWaterRequirementExist.ClientID %>').value != "") {
                totalExisting = Number(totalExisting) + Number(document.getElementById('<%= txtGroundWaterRequirementExist.ClientID %>').value);
                if (document.getElementById('<%= txtGroundWaterRequirementAddit.ClientID %>').value != "") {
                    totalAdditional = Number(totalAdditional) + Number(document.getElementById('<%= txtGroundWaterRequirementAddit.ClientID %>').value);
                    txtGroundWaterRequirementTotal = Number(document.getElementById('<%= txtGroundWaterRequirementExist.ClientID %>').value) + Number(document.getElementById('<%= txtGroundWaterRequirementAddit.ClientID %>').value);
                }
                else {
                    txtGroundWaterRequirementTotal = Number(document.getElementById('<%= txtGroundWaterRequirementExist.ClientID %>').value);
                }
            }
            else {
                totalAdditional = Number(totalAdditional) + Number(document.getElementById('<%= txtGroundWaterRequirementAddit.ClientID %>').value);
                txtGroundWaterRequirementTotal = Number(document.getElementById('<%= txtGroundWaterRequirementAddit.ClientID %>').value);
            }
            document.getElementById('<%= txtGroundWaterRequirementTotal.ClientID %>').value = txtGroundWaterRequirementTotal.toFixed(2);

            var txtSurfaceWaterRequirementTotal = "";
            if (document.getElementById('<%= txtSurfaceWaterRequirementExist.ClientID %>').value != "") {
                totalExisting = Number(totalExisting) + Number(document.getElementById('<%= txtSurfaceWaterRequirementExist.ClientID %>').value);
                if (document.getElementById('<%= txtSurfaceWaterRequirementAddit.ClientID %>').value != "") {
                    totalAdditional = Number(totalAdditional) + Number(document.getElementById('<%= txtSurfaceWaterRequirementAddit.ClientID %>').value);
                    txtSurfaceWaterRequirementTotal = Number(document.getElementById('<%= txtSurfaceWaterRequirementExist.ClientID %>').value) + Number(document.getElementById('<%= txtSurfaceWaterRequirementAddit.ClientID %>').value);
                }
                else {
                    txtSurfaceWaterRequirementTotal = Number(document.getElementById('<%= txtSurfaceWaterRequirementExist.ClientID %>').value);
                }
            }
            else {
                totalAdditional = Number(totalAdditional) + Number(document.getElementById('<%= txtSurfaceWaterRequirementAddit.ClientID %>').value);
                txtSurfaceWaterRequirementTotal = Number(document.getElementById('<%= txtSurfaceWaterRequirementAddit.ClientID %>').value);
            }
            document.getElementById('<%= txtSurfaceWaterRequirementTotal.ClientID %>').value = Number(txtSurfaceWaterRequirementTotal).toFixed(2);

            var txtAdditionalExistingWaterSupplyTotal = "";
            if (document.getElementById('<%= txtAdditionalExistingWaterSupplyExist.ClientID %>').value != "") {
                totalExisting = Number(totalExisting) + Number(document.getElementById('<%= txtAdditionalExistingWaterSupplyExist.ClientID %>').value);
                if (document.getElementById('<%= txtAdditionalExistingWaterSupplyAddit.ClientID %>').value != "") {
                    totalAdditional = Number(totalAdditional) + Number(document.getElementById('<%= txtAdditionalExistingWaterSupplyAddit.ClientID %>').value);
                    txtAdditionalExistingWaterSupplyTotal = Number(document.getElementById('<%= txtAdditionalExistingWaterSupplyExist.ClientID %>').value) + Number(document.getElementById('<%= txtAdditionalExistingWaterSupplyAddit.ClientID %>').value);
                }
                else {
                    txtAdditionalExistingWaterSupplyTotal = Number(document.getElementById('<%= txtAdditionalExistingWaterSupplyExist.ClientID %>').value);
                }
            }
            else {
                totalAdditional = Number(totalAdditional) + Number(document.getElementById('<%= txtAdditionalExistingWaterSupplyAddit.ClientID %>').value);
                txtAdditionalExistingWaterSupplyTotal = Number(document.getElementById('<%= txtAdditionalExistingWaterSupplyAddit.ClientID %>').value);
            }
            document.getElementById('<%= txtAdditionalExistingWaterSupplyTotal.ClientID %>').value = Number(txtAdditionalExistingWaterSupplyTotal).toFixed(2);

            var txtRecyWaterUsageTotal = "", txtTotalAdditional = "", txtTotalExisting = "";
            if (document.getElementById('<%= txtRecyWaterUsageExist.ClientID %>').value != "") {
                txtTotalExisting = Number(document.getElementById('<%= txtRecyWaterUsageExist.ClientID %>').value);
                if (document.getElementById('<%= txtRecyWaterUsageAddit.ClientID %>').value != "") {
                    txtTotalAdditional = Number(document.getElementById('<%= txtRecyWaterUsageAddit.ClientID %>').value);
                    txtRecyWaterUsageTotal = Number(document.getElementById('<%= txtRecyWaterUsageExist.ClientID %>').value) + Number(document.getElementById('<%= txtRecyWaterUsageAddit.ClientID %>').value);
                }
                else {
                    txtRecyWaterUsageTotal = Number(document.getElementById('<%= txtRecyWaterUsageExist.ClientID %>').value);
                }
            }
            else {
                txtTotalAdditional = Number(document.getElementById('<%= txtRecyWaterUsageAddit.ClientID %>').value);
                txtRecyWaterUsageTotal = Number(document.getElementById('<%= txtRecyWaterUsageAddit.ClientID %>').value);
            }
            document.getElementById('<%= txtRecyWaterUsageTotal.ClientID %>').value = Number(txtRecyWaterUsageTotal).toFixed(2);
            document.getElementById('<%= totalExisting.ClientID %>').value = Number(totalExisting).toFixed(2);
            document.getElementById('<%= totalAdditional.ClientID %>').value = Number(totalAdditional).toFixed(2);
            var GTotal = Number(Number(totalExisting) + Number(totalAdditional)).toFixed(2);
            if (GTotal != "0.00") {
                document.getElementById('<%= GTotal.ClientID %>').value = Number(GTotal).toFixed(2);
            }

            var FinalSumExisting = Number(Number(totalExisting) + Number(txtTotalExisting)).toFixed(2);
            var FinalSumAdditional = Number(Number(totalAdditional) + Number(txtTotalAdditional)).toFixed(2);
            if (FinalSumExisting != "0.00") {
                document.getElementById('<%= txtExistTotal.ClientID %>').value = Number(FinalSumExisting).toFixed(2);
            }
            if (FinalSumAdditional != "0.00") {
                document.getElementById('<%= txtAdditTotal.ClientID %>').value = Number(FinalSumAdditional).toFixed(2);
            }

            var FinaltxtGTotal = Number(FinalSumExisting) + Number(FinalSumAdditional);
            if (FinaltxtGTotal != "0.00") {
                document.getElementById('<%= txtGTotal.ClientID %>').value = Number(FinaltxtGTotal).toFixed(2);
            }
        }



        function WaterRequirementMessage() {
            var txtWaterRequirementExisting = "0";
            if (document.getElementById('<%= txtGroundWaterRequirementExist.ClientID %>').value != "") {
                txtWaterRequirementExisting = Number(txtWaterRequirementExisting) + Number(document.getElementById('<%= txtGroundWaterRequirementExist.ClientID %>').value);
            }
            if (document.getElementById('<%= txtSurfaceWaterRequirementExist.ClientID %>').value != "") {
                txtWaterRequirementExisting = Number(txtWaterRequirementExisting) + Number(document.getElementById('<%= txtSurfaceWaterRequirementExist.ClientID %>').value);
            }
            if (document.getElementById('<%= txtAdditionalExistingWaterSupplyExist.ClientID %>').value != "") {
                txtWaterRequirementExisting = Number(txtWaterRequirementExisting) + Number(document.getElementById('<%= txtAdditionalExistingWaterSupplyExist.ClientID %>').value);
            }
            if (document.getElementById('<%= txtRecyWaterUsageExist.ClientID %>').value != "") {
                txtWaterRequirementExisting = Number(txtWaterRequirementExisting) + Number(document.getElementById('<%= txtRecyWaterUsageExist.ClientID %>').value);
            }

            var txtBreakupOfWater = "0";
            if (document.getElementById('<%= txtCommeUseExistRequirement.ClientID %>').value != "") {
                txtBreakupOfWater = Number(txtBreakupOfWater) + Number(document.getElementById('<%= txtCommeUseExistRequirement.ClientID %>').value);
            }
            if (document.getElementById('<%= txtResidUseExistRequirement.ClientID %>').value != "") {
                txtBreakupOfWater = Number(txtBreakupOfWater) + Number(document.getElementById('<%= txtResidUseExistRequirement.ClientID %>').value);
            }
            if (document.getElementById('<%= txtGreenDevelEnviMaintExistRequirement.ClientID %>').value != "") {
                txtBreakupOfWater = Number(txtBreakupOfWater) + Number(document.getElementById('<%= txtGreenDevelEnviMaintExistRequirement.ClientID %>').value);
            }
            if (document.getElementById('<%= txtFlushReqExist.ClientID %>').value != "") {
                txtBreakupOfWater = Number(txtBreakupOfWater) + Number(document.getElementById('<%= txtFlushReqExist.ClientID %>').value);
            }
            if (txtWaterRequirementExisting != txtBreakupOfWater) {
                alert("Please Modify Breakup of Water Requirement and Usage Existing 2(ii)- It should equal to Total Water Requirement Existing Section 2(i)");
            }


            var txtWaterRequirementAdditional = "0";
            if (document.getElementById('<%= txtGroundWaterRequirementAddit.ClientID %>').value != "") {
                txtWaterRequirementAdditional = Number(txtWaterRequirementAdditional) + Number(document.getElementById('<%= txtGroundWaterRequirementAddit.ClientID %>').value);
            }
            if (document.getElementById('<%= txtSurfaceWaterRequirementAddit.ClientID %>').value != "") {
                txtWaterRequirementAdditional = Number(txtWaterRequirementAdditional) + Number(document.getElementById('<%= txtSurfaceWaterRequirementAddit.ClientID %>').value);
            }
            if (document.getElementById('<%= txtAdditionalExistingWaterSupplyAddit.ClientID %>').value != "") {
                txtWaterRequirementAdditional = Number(txtWaterRequirementAdditional) + Number(document.getElementById('<%= txtAdditionalExistingWaterSupplyAddit.ClientID %>').value);
            }
            if (document.getElementById('<%= txtRecyWaterUsageAddit.ClientID %>').value != "") {
                txtWaterRequirementAdditional = Number(txtWaterRequirementAdditional) + Number(document.getElementById('<%= txtRecyWaterUsageAddit.ClientID %>').value);
            }

            var txtBreakupOfWaterAdditional = "0";
            if (document.getElementById('<%= txtCommeUseAdditRequirement.ClientID %>').value != "") {
                txtBreakupOfWaterAdditional = Number(txtBreakupOfWaterAdditional) + Number(document.getElementById('<%= txtCommeUseAdditRequirement.ClientID %>').value);
            }
            if (document.getElementById('<%= txtResidUseAdditRequirement.ClientID %>').value != "") {
                txtBreakupOfWaterAdditional = Number(txtBreakupOfWaterAdditional) + Number(document.getElementById('<%= txtResidUseAdditRequirement.ClientID %>').value);
            }
            if (document.getElementById('<%= txtGreenDevelEnviMaintAdditionalRequirement.ClientID %>').value != "") {
                txtBreakupOfWaterAdditional = Number(txtBreakupOfWaterAdditional) + Number(document.getElementById('<%= txtGreenDevelEnviMaintAdditionalRequirement.ClientID %>').value);
            }
            if (document.getElementById('<%= txtFlushReqAddit.ClientID %>').value != "") {
                txtBreakupOfWaterAdditional = Number(txtBreakupOfWaterAdditional) + Number(document.getElementById('<%= txtFlushReqAddit.ClientID %>').value);
            }
            if (txtWaterRequirementAdditional != txtBreakupOfWaterAdditional) {
                alert("Please Modify Breakup of Water Requirement and Usage Additional 2(ii)- It should equal to Total Water Requrement Additional Section 2(i)");
            }


        }

        function cal1() {


            var txtCommeUseExistRequirement = document.getElementById('<%= txtCommeUseExistRequirement.ClientID %>').value;
            if (txtCommeUseExistRequirement != null && txtCommeUseExistRequirement != "") {
                var txtCommeUseExistRequirement = document.getElementById('<%= txtCommeUseExistRequirement.ClientID %>').value;
            }
            else {
                document.getElementById('<%= txtCommeUseExistRequirement.ClientID %>').value = 0;
            }
            var txtCommeUseAdditRequirement = document.getElementById('<%= txtCommeUseAdditRequirement.ClientID %>').value;
            if (txtCommeUseAdditRequirement != null && txtCommeUseExistRequirement != "") {
                var txtCommeUseAdditRequirement = document.getElementById('<%= txtCommeUseAdditRequirement.ClientID %>').value;
            }
            else {
                document.getElementById('<%= txtCommeUseAdditRequirement.ClientID %>').value = 0;
            }
            var txtCommeUseTotalRequirement = Number(txtCommeUseExistRequirement) + Number(txtCommeUseAdditRequirement);
            document.getElementById('<%= txtCommeUseTotalRequirement.ClientID %>').value = txtCommeUseTotalRequirement.toFixed(2);
            var txtCommeUseNoOfOperationalDaysInYear = document.getElementById('<%= txtCommeUseNoOfOperationalDaysInYear.ClientID %>').value;
            var txtResidUseNoOfOperationalDaysInYear = document.getElementById('<%= txtResidUseNoOfOperationalDaysInYear.ClientID %>').value;
            var txtGreenDevelEnviMaintNoOfOperationalDaysInYear = document.getElementById('<%= txtGreenDevelEnviMaintNoOfOperationalDaysInYear.ClientID %>').value;
            var txtFlushReqNoOfOperationalDaysInYear = document.getElementById('<%= txtFlushReqNoOfOperationalDaysInYear.ClientID %>').value;
            var txtCommeUseAnnualRequirement = document.getElementById('<%= txtCommeUseAnnualRequirement.ClientID %>').value;
            var txtResidUseAnnualRequirement = document.getElementById('<%= txtResidUseAnnualRequirement.ClientID %>').value;
            var txtGreenDevelEnviMaintAnnualRequirement = document.getElementById('<%= txtGreenDevelEnviMaintAnnualRequirement.ClientID %>').value;
            var txtFlushReqAnnual = document.getElementById('<%= txtFlushReqAnnual.ClientID %>').value;
            var txtResidUseExistRequirement = document.getElementById('<%= txtResidUseExistRequirement.ClientID %>').value;
            var txtResidUseAdditRequirement = document.getElementById('<%= txtResidUseAdditRequirement.ClientID %>').value;
            var txtResidUseTotalRequirement = Number(txtResidUseExistRequirement) + Number(txtResidUseAdditRequirement);
            document.getElementById('<%= txtResidUseTotalRequirement.ClientID %>').value = txtResidUseTotalRequirement.toFixed(2);
            var txtGreenDevelEnviMaintExistRequirement = document.getElementById('<%= txtGreenDevelEnviMaintExistRequirement.ClientID %>').value;
            var txtGreenDevelEnviMaintAdditionalRequirement = document.getElementById('<%= txtGreenDevelEnviMaintAdditionalRequirement.ClientID %>').value;
            var txtGreenDevelEnviMaintTotalRequirement = Number(txtGreenDevelEnviMaintExistRequirement) + Number(txtGreenDevelEnviMaintAdditionalRequirement);
            document.getElementById('<%= txtGreenDevelEnviMaintTotalRequirement.ClientID %>').value = txtGreenDevelEnviMaintTotalRequirement.toFixed(2);
            var txtFlushReqExist = document.getElementById('<%= txtFlushReqExist.ClientID %>').value;
            var txtFlushReqAddit = document.getElementById('<%= txtFlushReqAddit.ClientID %>').value;
            var txtFlushReqTotal = Number(txtFlushReqExist) + Number(txtFlushReqAddit);
            document.getElementById('<%= txtFlushReqTotal.ClientID %>').value = txtFlushReqTotal.toFixed(2);
            var txtGrandTotalExistRequirement = Number(txtCommeUseExistRequirement) + Number(txtResidUseExistRequirement) + Number(txtGreenDevelEnviMaintExistRequirement) + Number(txtFlushReqExist);
            document.getElementById('<%= txtGrandTotalExistRequirement.ClientID %>').value = txtGrandTotalExistRequirement.toFixed(2);
            var txtGrandTotalAdditionalRequirement = Number(txtCommeUseAdditRequirement) + Number(txtResidUseAdditRequirement) + Number(txtGreenDevelEnviMaintAdditionalRequirement) + Number(txtFlushReqAddit);
            document.getElementById('<%= txtGrandTotalAdditionalRequirement.ClientID %>').value = txtGrandTotalAdditionalRequirement.toFixed(2);
            var txtGrandTotalTotalRequirement = Number(txtCommeUseTotalRequirement) + Number(txtResidUseTotalRequirement) + Number(txtGreenDevelEnviMaintTotalRequirement) + Number(txtFlushReqTotal);
            document.getElementById('<%= txtGrandTotalTotalRequirement.ClientID %>').value = txtGrandTotalTotalRequirement.toFixed(2);
            // var txtGrandTotalNoOfOperationalDaysInYear = Number(txtCommeUseNoOfOperationalDaysInYear) + Number(txtResidUseNoOfOperationalDaysInYear) + Number(txtGreenDevelEnviMaintNoOfOperationalDaysInYear) + Number(txtFlushReqNoOfOperationalDaysInYear); 
            // document.getElementById('<%= txtGrandTotalNoOfOperationalDaysInYear.ClientID %>').value = txtGrandTotalNoOfOperationalDaysInYear.toFixed(2);
            var txtGrandTotalAnnualRequirement = Number(txtCommeUseAnnualRequirement) + Number(txtResidUseAnnualRequirement) + Number(txtGreenDevelEnviMaintAnnualRequirement) + Number(txtFlushReqAnnual);



            //-------Annual----------

            var txtCommeUseAnnualRequirement = Number(txtCommeUseTotalRequirement) * Number(txtCommeUseNoOfOperationalDaysInYear);
            var txtResidUseAnnualRequirement = Number(txtResidUseTotalRequirement) * Number(txtResidUseNoOfOperationalDaysInYear);
            var txtGreenDevelEnviMaintAnnualRequirement = Number(txtGreenDevelEnviMaintTotalRequirement) * Number(txtGreenDevelEnviMaintNoOfOperationalDaysInYear);
            var txtFlushReqAnnual = Number(txtFlushReqTotal) * Number(txtFlushReqNoOfOperationalDaysInYear);
            document.getElementById('<%= txtCommeUseAnnualRequirement.ClientID %>').value = txtCommeUseAnnualRequirement.toFixed(2);
            document.getElementById('<%= txtResidUseAnnualRequirement.ClientID %>').value = txtResidUseAnnualRequirement.toFixed(2);
            document.getElementById('<%= txtGreenDevelEnviMaintAnnualRequirement.ClientID %>').value = txtGreenDevelEnviMaintAnnualRequirement.toFixed(2);
            document.getElementById('<%= txtFlushReqAnnual.ClientID %>').value = txtFlushReqAnnual.toFixed(2);
            //alert(txtCommeUseAnnualRequirement + "," + txtResidUseAnnualRequirement + "," + txtGreenDevelEnviMaintAnnualRequirement + "," + txtFlushReqAnnual);

            txtGrandTotalAnnualRequirement = parseFloat(txtCommeUseAnnualRequirement) + parseFloat(txtResidUseAnnualRequirement) + parseFloat(txtGreenDevelEnviMaintAnnualRequirement) + parseFloat(txtFlushReqAnnual);
            document.getElementById('<%= txtGrandTotalAnnualRequirement.ClientID %>').value = parseFloat(txtGrandTotalAnnualRequirement).toFixed(2);


        }


        function cal() {

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <table align="center" class="SubFormWOBG" width="100%">
        <tr>
            <td style="width: 200px">
                <table align="left" width="100%">
                    <tr>
                        <td>
                            <div class="middle">
                                <div class="block_left_inner">
                                    <div id="information" class="cont_left" style="display: block">
                                        <ul class="progressbar">
                                            <li class="visited">Location Details</li>
                                            <li class="visited">Communication Address</li>
                                            <li class="visited">Existing NOC Details</li>
                                            <li class="active">Water Requirement Details</li>
                                            <li>Recycled Water Usage</li>
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
                <table align="right" class="SubFormWOBG" style="line-height: 25px" width="100%">
                    <tr>
                        <td colspan="2">
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                RENEW - INFRASTRUCTURE USE: 2. Water Requirement Details
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
                        <td colspan="2" style="text-align: right">
                            (<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-
                            Upload Attachments in <strong>Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">
                            (2).
                        </td>
                        <td>
                            <b>Details of Water Requirement (Fresh/Saline/Brackish and Recycled Water Usage):
                                <%--<br />--%>
                                <%--(Please Enclose Water Flow Chart of Activities and Requirement of Water at each
                            Stage) --%>
                                <%--(<span class="Coumpulsory">$</span>)--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <b>(i) Total Water Requirement (a+b+c+d) (m<sup>3</sup>/day)</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0" class="SubFormWOBG" width="100%">
                                <tr>
                                    <th>
                                        &nbsp;
                                    </th>
                                    <th style="width: 20%">
                                        Existing
                                    </th>
                                    <th style="width: 20%">
                                        Additional
                                    </th>
                                    <th style="width: 20%">
                                        Total
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <b>Water Requirement Details (Fresh Water) (m<sup>3</sup>/day)</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        a) Ground Water Requirement (m&sup3/day): <span class="Coumpulsory">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGroundWaterRequirementExist" runat="server" MaxLength="9" Width="97%"
                                            onblur="SumTWR();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtGroundWaterRequirementExist" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtGroundWaterRequirementExist"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtGroundWaterRequirementExist" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtGroundWaterRequirementExist"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGroundWaterRequirementAddit" runat="server" MaxLength="9" Width="97%"
                                            onblur="SumTWR();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtGroundWaterRequirementAddit" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtGroundWaterRequirementAddit"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtGroundWaterRequirementAddit" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtGroundWaterRequirementAddit"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGroundWaterRequirementTotal" runat="server" Width="97%" Enabled="false"></asp:TextBox>
                                        <br />
                                        <br />
                                        <asp:TextBox ID="txtGroundWaterRequirementYear" runat="server" Width="97%"></asp:TextBox>
                                        (m3/year)
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtGroundWaterRequirementYear"
                                            ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtGroundWaterRequirementYear" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtGroundWaterRequirementYear"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        b) Surface Water Available (Canal, River, Ponds etc.) (m&sup3/day): <span class="Coumpulsory">
                                            *</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSurfaceWaterRequirementExist" runat="server" MaxLength="9" Width="97%"
                                            onblur="SumTWR();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtSurfaceWaterRequirementExist" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtSurfaceWaterRequirementExist"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtSurfaceWaterRequirementExist" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtSurfaceWaterRequirementExist"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSurfaceWaterRequirementAddit" runat="server" MaxLength="9" Width="97%"
                                            onblur="SumTWR();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtSurfaceWaterRequirementAddit" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtSurfaceWaterRequirementAddit"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtSurfaceWaterRequirementAddit" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtSurfaceWaterRequirementAddit"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSurfaceWaterRequirementTotal" runat="server" Enabled="false"
                                            Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        c) Water Supply from any Agency (m&sup3/day): <span class="Coumpulsory">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAdditionalExistingWaterSupplyExist" runat="server" MaxLength="9"
                                            onblur="SumTWR();" Width="97%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtAdditionalExistingWaterSupplyExist" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtAdditionalExistingWaterSupplyExist"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtAdditionalExistingWaterSupplyExist" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtAdditionalExistingWaterSupplyExist"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAdditionalExistingWaterSupplyAddit" runat="server" MaxLength="9"
                                            onblur="SumTWR();" Width="97%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtAdditionalExistingWaterSupplyAddit" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtAdditionalExistingWaterSupplyAddit"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtAdditionalExistingWaterSupplyAddit" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtAdditionalExistingWaterSupplyAddit"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAdditionalExistingWaterSupplyTotal" runat="server" Enabled="false"
                                            Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Total Fresh Water Requirement : (a+b+c) (m<sup>3</sup>/day)</b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="totalExisting" runat="server" Text="" Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="totalAdditional" runat="server" Text="" Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="GTotal" runat="server" Text="" Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        d). Recycled Water Usage (m<sup>3</sup>/day):
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRecyWaterUsageExist" runat="server" MaxLength="9" Width="97%"
                                            onblur="SumTWR();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtRecyWaterUsageExist" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtRecyWaterUsageExist" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtRecyWaterUsageExist" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtRecyWaterUsageExist" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRecyWaterUsageAddit" runat="server" MaxLength="9" Width="97%"
                                            onblur="SumTWR();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtRecyWaterUsageAddit" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtRecyWaterUsageAddit" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtRecyWaterUsageAddit" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtRecyWaterUsageAddit" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRecyWaterUsageTotal" runat="server" Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Total Water Requirement (a+b+c+d) (m<sup>3</sup>/day): </b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExistTotal" runat="server" Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAdditTotal" runat="server" Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGTotal" runat="server" Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <b>(ii) Breakup of Water Requirement and Usage:</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0" class="SubFormWOBG" width="100%">
                                <tr>
                                    <th>
                                        <b>Activity</b>
                                    </th>
                                    <th>
                                        <b>Existing Requirement (m<sup>3</sup>/day)</b>
                                    </th>
                                    <th>
                                        <b>Additional Requirement (m<sup>3</sup>/day)</b>
                                    </th>
                                    <th>
                                        <b>Total Requirement (m<sup>3</sup>/day)</b>
                                    </th>
                                    <th>
                                        <b>No. of Operational Days in a Year</b>
                                    </th>
                                    <th>
                                        <b>Annual Requirement (m<sup>3</sup>/year)</b>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        Commercial Use
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCommeUseExistRequirement" runat="server" Width="95%" onblur="cal1()"
                                            MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtCommeUseExistRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtCommeUseExistRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtCommeUseExist" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtCommeUseExistRequirement" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                        <br />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCommeUseAdditRequirement" runat="server" Width="95%" onblur="cal1()"
                                            MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtCommeUseAdditRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtCommeUseAdditRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtCommeUseAddit" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtCommeUseAdditRequirement" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCommeUseTotalRequirement" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCommeUseNoOfOperationalDaysInYear" runat="server" Width="95%"
                                            onblur="cal1()" MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtCommeUseNoOfOperationalDaysInYear" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtCommeUseNoOfOperationalDaysInYear"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtCommeUseNoOfDaysInYear" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtCommeUseNoOfOperationalDaysInYear"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ForeColor="Red" MinimumValue="0"
                                            MaximumValue="365" ErrorMessage="Value should be between 0 and 365" ValidationGroup="WaterRequirementDetails"
                                            Display="Dynamic" ControlToValidate="txtCommeUseNoOfOperationalDaysInYear" Type="Integer"></asp:RangeValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCommeUseAnnualRequirement" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Residential Use
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResidUseExistRequirement" runat="server" Width="95%" onblur="cal1()"
                                            MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtResidUseExistRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtResidUseExistRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtResidUseExist" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtResidUseExistRequirement" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResidUseAdditRequirement" runat="server" Width="95%" onblur="cal1()"
                                            MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtResidUseAdditRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtResidUseAdditRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtResidUseAddit" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtResidUseAdditRequirement" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResidUseTotalRequirement" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResidUseNoOfOperationalDaysInYear" runat="server" Width="95%"
                                            onblur="cal1()" MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtResidUseNoOfOperationalDaysInYear" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtResidUseNoOfOperationalDaysInYear"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtResidUseNoOfDaysInYear" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtResidUseNoOfOperationalDaysInYear"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ForeColor="Red" MinimumValue="0"
                                            MaximumValue="365" ErrorMessage="Value should be between 0 and 365" ValidationGroup="WaterRequirementDetails"
                                            Display="Dynamic" ControlToValidate="txtResidUseNoOfOperationalDaysInYear" Type="Integer"></asp:RangeValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResidUseAnnualRequirement" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Greenbelt Development / Environment Maintenance
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGreenDevelEnviMaintExistRequirement" runat="server" Width="95%"
                                            onblur="cal1()" MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtGreenDevelEnviMaintExistRequirement" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtGreenDevelEnviMaintExistRequirement"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtGreenDevelExist" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtGreenDevelEnviMaintExistRequirement" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGreenDevelEnviMaintAdditionalRequirement" runat="server" Width="95%"
                                            onblur="cal1()" MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtGreenDevelEnviMaintAdditionalRequirement" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtGreenDevelEnviMaintAdditionalRequirement"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtGreenDevelAdditional" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtGreenDevelEnviMaintAdditionalRequirement"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGreenDevelEnviMaintTotalRequirement" runat="server" Width="95%"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGreenDevelEnviMaintNoOfOperationalDaysInYear" runat="server"
                                            onblur="cal1()" Width="95%" MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtGreenDevelEnviMaintNoOfOperationalDaysInYear"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtGreenDevelDaysInYear" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtGreenDevelEnviMaintNoOfOperationalDaysInYear"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="RangeValidator3" runat="server" ForeColor="Red" MinimumValue="0"
                                            MaximumValue="365" ErrorMessage="Value should be between 0 and 365" ValidationGroup="WaterRequirementDetails"
                                            Display="Dynamic" ControlToValidate="txtGreenDevelEnviMaintNoOfOperationalDaysInYear"
                                            Type="Integer"></asp:RangeValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGreenDevelEnviMaintAnnualRequirement" runat="server" Width="95%"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Flushing Req.
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFlushReqExist" runat="server" Width="95%" onblur="cal1()" MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtFlushReqExist" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtFlushReqExist" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtFlushReqExist" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtFlushReqExist" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFlushReqAddit" runat="server" Width="95%" onblur="cal1()" MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtFlushReqAddit" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtFlushReqAddit" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtFlushReqAddit" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtFlushReqAddit" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFlushReqTotal" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFlushReqNoOfOperationalDaysInYear" runat="server" Width="95%"
                                            MaxLength="3" onblur="cal1()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtFlushReqNoOfOperationalDaysInYear" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtFlushReqNoOfOperationalDaysInYear"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtFlushReqNoOfDaysInYear" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtFlushReqNoOfOperationalDaysInYear"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="RangeValidator4" runat="server" ForeColor="Red" MinimumValue="0"
                                            MaximumValue="365" ErrorMessage="Value should be between 0 and 365" ValidationGroup="WaterRequirementDetails"
                                            Display="Dynamic" ControlToValidate="txtFlushReqNoOfOperationalDaysInYear" Type="Integer"></asp:RangeValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFlushReqAnnual" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Grand Total</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGrandTotalExistRequirement" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGrandTotalAdditionalRequirement" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGrandTotalTotalRequirement" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGrandTotalNoOfOperationalDaysInYear" runat="server" Width="95%"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGrandTotalAnnualRequirement" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblInfrastructureApplicationCodeFrom" Visible="false" runat="server"
                                Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('Please Save as Draft before moving to Previous Page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" OnClientClick="WaterRequirementMessage()"
                                ValidationGroup="WaterRequirementDetails" OnClick="btnSaveAsDraft_Click" />
                            <asp:Button ID="txtNext" runat="server" Text="Next >>" ValidationGroup="WaterRequirementDetails"
                                OnClientClick="WaterRequirementMessage()" OnClick="txtNext_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
