<%@ Page Title="NOCAP-Industrial Application Expansion" Language="C#" MaintainScrollPositionOnPostback="true"
    MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true"
    CodeFile="WaterRequirementDetails.aspx.cs" Inherits="ExternalUser_Expansion_IND_WaterRequirementDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function SumTWR() {
            var txtGroundWaterRequirementTotal = "";
            var totalExisting = "0";
            var totalProposed = "0";
            if (document.getElementById('<%= txtGroundWaterRequirementExist.ClientID %>').value != "") {
                totalExisting = Number(totalExisting) + Number(document.getElementById('<%= txtGroundWaterRequirementExist.ClientID %>').value);
                if (document.getElementById('<%= txtGroundWaterRequirementPro.ClientID %>').value != "") {
                    totalProposed = Number(totalProposed) + Number(document.getElementById('<%= txtGroundWaterRequirementPro.ClientID %>').value);
                    txtGroundWaterRequirementTotal = Number(document.getElementById('<%= txtGroundWaterRequirementExist.ClientID %>').value) + Number(document.getElementById('<%= txtGroundWaterRequirementPro.ClientID %>').value);
                }
                else {
                    txtGroundWaterRequirementTotal = Number(document.getElementById('<%= txtGroundWaterRequirementExist.ClientID %>').value);
                }
            }
            else {
                totalProposed = Number(totalProposed) + Number(document.getElementById('<%= txtGroundWaterRequirementPro.ClientID %>').value);
                txtGroundWaterRequirementTotal = Number(document.getElementById('<%= txtGroundWaterRequirementPro.ClientID %>').value);
            }
            document.getElementById('<%= txtGroundWaterRequirementTotal.ClientID %>').value = txtGroundWaterRequirementTotal.toFixed(2);


            var txtSurfaceWaterRequirementTotal = "";
            if (document.getElementById('<%= txtSurfaceWaterRequirementExist.ClientID %>').value != "") {
                totalExisting = Number(totalExisting) + Number(document.getElementById('<%= txtSurfaceWaterRequirementExist.ClientID %>').value);
                if (document.getElementById('<%= txtSurfaceWaterRequirementPro.ClientID %>').value != "") {
                    totalProposed = Number(totalProposed) + Number(document.getElementById('<%= txtSurfaceWaterRequirementPro.ClientID %>').value);
                    txtSurfaceWaterRequirementTotal = Number(document.getElementById('<%= txtSurfaceWaterRequirementExist.ClientID %>').value) + Number(document.getElementById('<%= txtSurfaceWaterRequirementPro.ClientID %>').value);
                }
                else {
                    txtSurfaceWaterRequirementTotal = Number(document.getElementById('<%= txtSurfaceWaterRequirementExist.ClientID %>').value);
                }
            }
            else {
                totalProposed = Number(totalProposed) + Number(document.getElementById('<%= txtSurfaceWaterRequirementPro.ClientID %>').value);
                txtSurfaceWaterRequirementTotal = Number(document.getElementById('<%= txtSurfaceWaterRequirementPro.ClientID %>').value);
            }
            document.getElementById('<%= txtSurfaceWaterRequirementTotal.ClientID %>').value = Number(txtSurfaceWaterRequirementTotal).toFixed(2);

            var txtProposedExistingWaterSupplyTotal = "";
            if (document.getElementById('<%= txtProposedExistingWaterSupplyExist.ClientID %>').value != "") {
                totalExisting = Number(totalExisting) + Number(document.getElementById('<%= txtProposedExistingWaterSupplyExist.ClientID %>').value);
                if (document.getElementById('<%= txtProposedExistingWaterSupplyPro.ClientID %>').value != "") {
                    totalProposed = Number(totalProposed) + Number(document.getElementById('<%= txtProposedExistingWaterSupplyPro.ClientID %>').value);
                    txtProposedExistingWaterSupplyTotal = Number(document.getElementById('<%= txtProposedExistingWaterSupplyExist.ClientID %>').value) + Number(document.getElementById('<%= txtProposedExistingWaterSupplyPro.ClientID %>').value);
                }
                else {
                    txtProposedExistingWaterSupplyTotal = Number(document.getElementById('<%= txtProposedExistingWaterSupplyExist.ClientID %>').value);
                }
            }
            else {
                totalProposed = Number(totalProposed) + Number(document.getElementById('<%= txtProposedExistingWaterSupplyPro.ClientID %>').value);
                txtProposedExistingWaterSupplyTotal = Number(document.getElementById('<%= txtProposedExistingWaterSupplyPro.ClientID %>').value);
            }
            document.getElementById('<%= txtProposedExistingWaterSupplyTotal.ClientID %>').value = Number(txtProposedExistingWaterSupplyTotal).toFixed(2);

            var txtRecyWaterUsageTotal = "", txtTotalProposed = "", txtTotalExisting = "";
            if (document.getElementById('<%= txtRecyWaterUsageExist.ClientID %>').value != "") {
                txtTotalExisting = Number(document.getElementById('<%= txtRecyWaterUsageExist.ClientID %>').value);
                if (document.getElementById('<%= txtRecyWaterUsagePro.ClientID %>').value != "") {
                    txtTotalProposed = Number(document.getElementById('<%= txtRecyWaterUsagePro.ClientID %>').value);
                    txtRecyWaterUsageTotal = Number(document.getElementById('<%= txtRecyWaterUsageExist.ClientID %>').value) + Number(document.getElementById('<%= txtRecyWaterUsagePro.ClientID %>').value);
                }
                else {
                    txtRecyWaterUsageTotal = Number(document.getElementById('<%= txtRecyWaterUsageExist.ClientID %>').value);
                }
            }
            else {
                txtTotalProposed = Number(document.getElementById('<%= txtRecyWaterUsagePro.ClientID %>').value);
                txtRecyWaterUsageTotal = Number(document.getElementById('<%= txtRecyWaterUsagePro.ClientID %>').value);
            }
            document.getElementById('<%= txtRecyWaterUsageTotal.ClientID %>').value = Number(txtRecyWaterUsageTotal).toFixed(2);
            document.getElementById('<%= totalExisting.ClientID %>').value = Number(totalExisting).toFixed(2);
            document.getElementById('<%= totalProposed.ClientID %>').value = Number(totalProposed).toFixed(2);
            var GTotal = Number(Number(totalExisting) + Number(totalProposed)).toFixed(2);
            if (GTotal != "0.00") {
                document.getElementById('<%= GTotal.ClientID %>').value = Number(GTotal).toFixed(2);
            }

            var FinalSumExisting = Number(Number(totalExisting) + Number(txtTotalExisting)).toFixed(2);
            var FinalSumProposed = Number(Number(totalProposed) + Number(txtTotalProposed)).toFixed(2);
            if (FinalSumExisting != "0.00") {
                document.getElementById('<%= txtExistTotal.ClientID %>').value = Number(FinalSumExisting).toFixed(2);
            }
            if (FinalSumProposed != "0.00") {
                document.getElementById('<%= txtProTotal.ClientID %>').value = Number(FinalSumProposed).toFixed(2);
            }

            var FinaltxtGTotal = Number(FinalSumExisting) + Number(FinalSumProposed);
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
            if (document.getElementById('<%= txtProposedExistingWaterSupplyExist.ClientID %>').value != "") {
                txtWaterRequirementExisting = Number(txtWaterRequirementExisting) + Number(document.getElementById('<%= txtProposedExistingWaterSupplyExist.ClientID %>').value);
            }
            if (document.getElementById('<%= txtRecyWaterUsageExist.ClientID %>').value != "") {
                txtWaterRequirementExisting = Number(txtWaterRequirementExisting) + Number(document.getElementById('<%= txtRecyWaterUsageExist.ClientID %>').value);
            }

            var txtBreakupOfWater = "0";
            if (document.getElementById('<%= txtIndActExistRequirement.ClientID %>').value != "") {
                txtBreakupOfWater = Number(txtBreakupOfWater) + Number(document.getElementById('<%= txtIndActExistRequirement.ClientID %>').value);
            }
            if (document.getElementById('<%= txtResidDomExistRequirement.ClientID %>').value != "") {
                txtBreakupOfWater = Number(txtBreakupOfWater) + Number(document.getElementById('<%= txtResidDomExistRequirement.ClientID %>').value);
            }
            if (document.getElementById('<%= txtGreenDevelEnviMaintExistRequirement.ClientID %>').value != "") {
                txtBreakupOfWater = Number(txtBreakupOfWater) + Number(document.getElementById('<%= txtGreenDevelEnviMaintExistRequirement.ClientID %>').value);
            }
            if (document.getElementById('<%= txtOtherUseExistRequirement.ClientID %>').value != "") {
                txtBreakupOfWater = Number(txtBreakupOfWater) + Number(document.getElementById('<%= txtOtherUseExistRequirement.ClientID %>').value);
            }
            if (txtWaterRequirementExisting != txtBreakupOfWater) {
                alert("Please Modify Breakup of Water Requirement and Usage Existing 2(ii)- It should equal to Total Water Requirement Existing Section 2(i)");
            }


            var txtWaterRequirementProposed = "0";
            if (document.getElementById('<%= txtGroundWaterRequirementPro.ClientID %>').value != "") {
                txtWaterRequirementProposed = Number(txtWaterRequirementProposed) + Number(document.getElementById('<%= txtGroundWaterRequirementPro.ClientID %>').value);
            }
            if (document.getElementById('<%= txtSurfaceWaterRequirementPro.ClientID %>').value != "") {
                txtWaterRequirementProposed = Number(txtWaterRequirementProposed) + Number(document.getElementById('<%= txtSurfaceWaterRequirementPro.ClientID %>').value);
            }
            if (document.getElementById('<%= txtProposedExistingWaterSupplyPro.ClientID %>').value != "") {
                txtWaterRequirementProposed = Number(txtWaterRequirementProposed) + Number(document.getElementById('<%= txtProposedExistingWaterSupplyPro.ClientID %>').value);
            }
            if (document.getElementById('<%= txtRecyWaterUsagePro.ClientID %>').value != "") {
                txtWaterRequirementProposed = Number(txtWaterRequirementProposed) + Number(document.getElementById('<%= txtRecyWaterUsagePro.ClientID %>').value);
            }

            var txtBreakupOfWaterProposed = "0";
            if (document.getElementById('<%= txtIndActProposedRequirement.ClientID %>').value != "") {
                txtBreakupOfWaterProposed = Number(txtBreakupOfWaterProposed) + Number(document.getElementById('<%= txtIndActProposedRequirement.ClientID %>').value);
            }
            if (document.getElementById('<%= txtResidDomProposedRequirement.ClientID %>').value != "") {
                txtBreakupOfWaterProposed = Number(txtBreakupOfWaterProposed) + Number(document.getElementById('<%= txtResidDomProposedRequirement.ClientID %>').value);
            }
            if (document.getElementById('<%= txtGreenDevelEnviMaintProposedRequirement.ClientID %>').value != "") {
                txtBreakupOfWaterProposed = Number(txtBreakupOfWaterProposed) + Number(document.getElementById('<%= txtGreenDevelEnviMaintProposedRequirement.ClientID %>').value);
            }
            if (document.getElementById('<%= txtOtherUseProposedRequirement.ClientID %>').value != "") {
                txtBreakupOfWaterProposed = Number(txtBreakupOfWaterProposed) + Number(document.getElementById('<%= txtOtherUseProposedRequirement.ClientID %>').value);
            }
            if (txtWaterRequirementProposed != txtBreakupOfWaterProposed) {
                alert("Please Modify Breakup of Water Requirement and Usage Proposed 2(ii)- It should equal to Total Water Requrement Proposed Section 2(i)");
            }


        }

        function cal1() {


            var txtIndActExistRequirement = document.getElementById('<%= txtIndActExistRequirement.ClientID %>').value;
            if (txtIndActExistRequirement != null && txtIndActExistRequirement != "") {
                var txtIndActExistRequirement = document.getElementById('<%= txtIndActExistRequirement.ClientID %>').value;
            }
            else {
                document.getElementById('<%= txtIndActExistRequirement.ClientID %>').value = 0;
            }
            var txtIndActProposedRequirement = document.getElementById('<%= txtIndActProposedRequirement.ClientID %>').value;
            if (txtIndActProposedRequirement != null && txtIndActExistRequirement != "") {
                var txtIndActProposedRequirement = document.getElementById('<%= txtIndActProposedRequirement.ClientID %>').value;
            }
            else {
                document.getElementById('<%= txtIndActProposedRequirement.ClientID %>').value = 0;
            }
            var txtIndActTotalRequirement = Number(txtIndActExistRequirement) + Number(txtIndActProposedRequirement);
            document.getElementById('<%= txtIndActTotalRequirement.ClientID %>').value = txtIndActTotalRequirement.toFixed(2);
            var txtIndActNoOfOperationalDaysInYear = document.getElementById('<%= txtIndActNoOfOperationalDaysInYear.ClientID %>').value;
            var txtResidDomNoOfOperationalDaysInYear = document.getElementById('<%= txtResidDomNoOfOperationalDaysInYear.ClientID %>').value;
            var txtGreenDevelEnviMaintNoOfOperationalDaysInYear = document.getElementById('<%= txtGreenDevelEnviMaintNoOfOperationalDaysInYear.ClientID %>').value;
            var txtOtherUseNoOfOperationalDaysInYear = document.getElementById('<%= txtOtherUseNoOfOperationalDaysInYear.ClientID %>').value;
            var txtIndActAnnualRequirement = document.getElementById('<%= txtIndActAnnualRequirement.ClientID %>').value;
            var txtResidDomAnnualRequirement = document.getElementById('<%= txtResidDomAnnualRequirement.ClientID %>').value;
            var txtGreenDevelEnviMaintAnnualRequirement = document.getElementById('<%= txtGreenDevelEnviMaintAnnualRequirement.ClientID %>').value;
            var txtOtherUseAnnualRequirement = document.getElementById('<%= txtOtherUseAnnualRequirement.ClientID %>').value;
            var txtResidDomExistRequirement = document.getElementById('<%= txtResidDomExistRequirement.ClientID %>').value;
            var txtResidDomProposedRequirement = document.getElementById('<%= txtResidDomProposedRequirement.ClientID %>').value;
            var txtResidDomTotalRequirement = Number(txtResidDomExistRequirement) + Number(txtResidDomProposedRequirement);
            document.getElementById('<%= txtResidDomTotalRequirement.ClientID %>').value = txtResidDomTotalRequirement.toFixed(2);
            var txtGreenDevelEnviMaintExistRequirement = document.getElementById('<%= txtGreenDevelEnviMaintExistRequirement.ClientID %>').value;
            var txtGreenDevelEnviMaintProposedRequirement = document.getElementById('<%= txtGreenDevelEnviMaintProposedRequirement.ClientID %>').value;
            var txtGreenDevelEnviMaintTotalRequirement = Number(txtGreenDevelEnviMaintExistRequirement) + Number(txtGreenDevelEnviMaintProposedRequirement);
            document.getElementById('<%= txtGreenDevelEnviMaintTotalRequirement.ClientID %>').value = txtGreenDevelEnviMaintTotalRequirement.toFixed(2);
            var txtOtherUseExistRequirement = document.getElementById('<%= txtOtherUseExistRequirement.ClientID %>').value;
            var txtOtherUseProposedRequirement = document.getElementById('<%= txtOtherUseProposedRequirement.ClientID %>').value;
            var txtOtherUseTotalRequirement = Number(txtOtherUseExistRequirement) + Number(txtOtherUseProposedRequirement);
            document.getElementById('<%= txtOtherUseTotalRequirement.ClientID %>').value = txtOtherUseTotalRequirement.toFixed(2);
            var txtGrandTotalExistRequirement = Number(txtIndActExistRequirement) + Number(txtResidDomExistRequirement) + Number(txtGreenDevelEnviMaintExistRequirement) + Number(txtOtherUseExistRequirement);
            document.getElementById('<%= txtGrandTotalExistRequirement.ClientID %>').value = txtGrandTotalExistRequirement.toFixed(2);
            var txtGrandTotalProposedRequirement = Number(txtIndActProposedRequirement) + Number(txtResidDomProposedRequirement) + Number(txtGreenDevelEnviMaintProposedRequirement) + Number(txtOtherUseProposedRequirement);
            document.getElementById('<%= txtGrandTotalProposedRequirement.ClientID %>').value = txtGrandTotalProposedRequirement.toFixed(2);
            var txtGrandTotalTotalRequirement = Number(txtIndActTotalRequirement) + Number(txtResidDomTotalRequirement) + Number(txtGreenDevelEnviMaintTotalRequirement) + Number(txtOtherUseTotalRequirement);
            document.getElementById('<%= txtGrandTotalTotalRequirement.ClientID %>').value = txtGrandTotalTotalRequirement.toFixed(2);
            // var txtGrandTotalNoOfOperationalDaysInYear = Number(txtIndActNoOfOperationalDaysInYear) + Number(txtResidDomNoOfOperationalDaysInYear) + Number(txtGreenDevelEnviMaintNoOfOperationalDaysInYear) + Number(txtOtherUseNoOfOperationalDaysInYear); 
            // document.getElementById('<%= txtGrandTotalNoOfOperationalDaysInYear.ClientID %>').value = txtGrandTotalNoOfOperationalDaysInYear.toFixed(2);
            var txtGrandTotalAnnualRequirement = Number(txtIndActAnnualRequirement) + Number(txtResidDomAnnualRequirement) + Number(txtGreenDevelEnviMaintAnnualRequirement) + Number(txtOtherUseAnnualRequirement);



            //-------Annual----------

            var txtIndActAnnualRequirement = Number(txtIndActTotalRequirement) * Number(txtIndActNoOfOperationalDaysInYear);
            var txtResidDomAnnualRequirement = Number(txtResidDomTotalRequirement) * Number(txtResidDomNoOfOperationalDaysInYear);
            var txtGreenDevelEnviMaintAnnualRequirement = Number(txtGreenDevelEnviMaintTotalRequirement) * Number(txtGreenDevelEnviMaintNoOfOperationalDaysInYear);
            var txtOtherUseAnnualRequirement = Number(txtOtherUseTotalRequirement) * Number(txtOtherUseNoOfOperationalDaysInYear);
            document.getElementById('<%= txtIndActAnnualRequirement.ClientID %>').value = txtIndActAnnualRequirement.toFixed(2);
            document.getElementById('<%= txtResidDomAnnualRequirement.ClientID %>').value = txtResidDomAnnualRequirement.toFixed(2);
            document.getElementById('<%= txtGreenDevelEnviMaintAnnualRequirement.ClientID %>').value = txtGreenDevelEnviMaintAnnualRequirement.toFixed(2);
            document.getElementById('<%= txtOtherUseAnnualRequirement.ClientID %>').value = txtOtherUseAnnualRequirement.toFixed(2);
            //alert(txtIndActAnnualRequirement + "," + txtResidDomAnnualRequirement + "," + txtGreenDevelEnviMaintAnnualRequirement + "," + txtOtherUseAnnualRequirement);

            txtGrandTotalAnnualRequirement = parseFloat(txtIndActAnnualRequirement) + parseFloat(txtResidDomAnnualRequirement) + parseFloat(txtGreenDevelEnviMaintAnnualRequirement) + parseFloat(txtOtherUseAnnualRequirement);
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
                                            <li class="visited">Land Use Details</li>
                                            <li class="active">Water Requirement Details</li>
                                            <li>Recycled Water Usage</li>
                                            <li>Groundwater Abstraction Structure- Existing</li>
                                            <li>Groundwater Abstraction Structure- Proposed</li>
                                            <li>Other Details</li>
                                            <%-- <li>Self Declaration</li>--%>
                                            <li>Attachment</li>
                                            <%--                       <li >Online Payment</li>--%>
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
                                INDUSTRIAL EXPANSION USE: 2. Water Requirement Details
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: right">(<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-
                            Upload Attachments in <strong>Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">(2).
                        </td>
                        <td>
                            <b>Details of Water Requirement (Fresh/Saline/Brackish and Recycled Water Usage):<br />
                            </b><%--(Please Enclose Water Balance Flow Chart of Activities and Requirement of Water at each
                             Stage)--%>
                            <%-- Stage) (<span class="Coumpulsory">$</span>)--%>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>(i) Total Water Requirement (a+b+c+d) (m<sup>3</sup>/day)</b>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <table cellpadding="0" cellspacing="0" class="SubFormWOBG" width="100%">
                                <tr>
                                    <th>&nbsp;
                                    </th>
                                    <th style="width: 20%">Existing (m3/day)
                                    </th>
                                    <th style="width: 20%">Proposed (m3/day)
                                    </th>
                                    <th style="width: 20%">Total (m3/day)
                                    </th>

                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <b>Water Requirement Details (Fresh Water) (m<sup>3</sup>/day)</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>a) Ground Water Requirement (m&sup3/day): <span class="Coumpulsory">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGroundWaterRequirementExist" runat="server" MaxLength="9" Width="97%"
                                            onblur="SumTWR();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtGroundWaterRequirementExist" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtGroundWaterRequirementExist"
                                            ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtGroundWaterRequirementExist" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtGroundWaterRequirementExist"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGroundWaterRequirementPro" runat="server" MaxLength="9" Width="97%"
                                            onblur="SumTWR();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtGroundWaterRequirementPro" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtGroundWaterRequirementPro" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtGroundWaterRequirementPro" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtGroundWaterRequirementPro"
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
                                    <td>b) Surface Water Available (Canal, River, Ponds etc.) (m&sup3/day): <span class="Coumpulsory">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSurfaceWaterRequirementExist" runat="server" MaxLength="9" Width="97%"
                                            onblur="SumTWR();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtSurfaceWaterRequirementExist" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtSurfaceWaterRequirementExist" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtSurfaceWaterRequirementExist" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtSurfaceWaterRequirementExist"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSurfaceWaterRequirementPro" runat="server" MaxLength="9" Width="97%"
                                            onblur="SumTWR();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtSurfaceWaterRequirementPro" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtSurfaceWaterRequirementPro" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtSurfaceWaterRequirementPro" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtSurfaceWaterRequirementPro"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSurfaceWaterRequirementTotal" runat="server" Enabled="false"
                                            Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>c) Water Supply from any Agency (m&sup3/day): <span class="Coumpulsory">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtProposedExistingWaterSupplyExist" runat="server" MaxLength="9"
                                            onblur="SumTWR();" Width="97%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtProposedExistingWaterSupplyExist" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtProposedExistingWaterSupplyExist"
                                            ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtProposedExistingWaterSupplyExist" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtProposedExistingWaterSupplyExist"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtProposedExistingWaterSupplyPro" runat="server" MaxLength="9"
                                            onblur="SumTWR();" Width="97%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtProposedExistingWaterSupplyPro" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtProposedExistingWaterSupplyPro" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtProposedExistingWaterSupplyPro" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtProposedExistingWaterSupplyPro"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtProposedExistingWaterSupplyTotal" runat="server" Enabled="false"
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
                                        <asp:TextBox ID="totalProposed" runat="server" Text="" Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="GTotal" runat="server" Text="" Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>d). Recycled Water Usage (m<sup>3</sup>/day):
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
                                        <asp:TextBox ID="txtRecyWaterUsagePro" runat="server" MaxLength="9" Width="97%" onblur="SumTWR();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtRecyWaterUsagePro" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtRecyWaterUsagePro" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtRecyWaterUsagePro" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtRecyWaterUsagePro" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
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
                                        <asp:TextBox ID="txtProTotal" runat="server" Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGTotal" runat="server" Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>(ii) Breakup of Water Requirement and Usage:</b>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
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
                                        <b>Proposed Requirement (m<sup>3</sup>/day)</b>
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
                                    <td>Industrial Activity
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIndActExistRequirement" runat="server" Width="95%" onblur="cal1()"
                                            MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtIndActExistRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtIndActExistRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtIndActExist" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtIndActExistRequirement" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                        <br />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIndActProposedRequirement" runat="server" Width="95%" onblur="cal1()"
                                            MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtIndActProposedRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtIndActProposedRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtIndActProposed" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtIndActProposedRequirement" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIndActTotalRequirement" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIndActNoOfOperationalDaysInYear" runat="server" Width="95%"
                                            onblur="cal1()"
                                            MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtIndActNoOfOperationalDaysInYear" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtIndActDaysInYear" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtIndActNoOfOperationalDaysInYear" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ForeColor="Red" MinimumValue="0"
                                            MaximumValue="365" ErrorMessage="Value should be between 0 and 365" ValidationGroup="WaterRequirementDetails"
                                            Display="Dynamic" ControlToValidate="txtIndActNoOfOperationalDaysInYear" Type="Integer"></asp:RangeValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIndActAnnualRequirement" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Residential / Domestic
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResidDomExistRequirement" runat="server" Width="95%" onblur="cal1()"
                                            MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtResidDomExistRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtResidDomExistRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtResidDomExist" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtResidDomExistRequirement"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResidDomProposedRequirement" runat="server" Width="95%" onblur="cal1()"
                                            MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtResidDomProposedRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtResidDomProposedRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtResidDomProposed" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtResidDomProposedRequirement"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResidDomTotalRequirement" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResidDomNoOfOperationalDaysInYear" runat="server" Width="95%"
                                            onblur="cal1()" MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtResidDomNoOfOperationalDaysInYear" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtResidDomDaysInYear" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtResidDomNoOfOperationalDaysInYear"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ForeColor="Red" MinimumValue="0"
                                            MaximumValue="365" ErrorMessage="Value should be between 0 and 365" ValidationGroup="WaterRequirementDetails"
                                            Display="Dynamic" ControlToValidate="txtResidDomNoOfOperationalDaysInYear" Type="Integer"></asp:RangeValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResidDomAnnualRequirement" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Greenbelt Development / Environment Maintenance
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGreenDevelEnviMaintExistRequirement" runat="server" Width="95%"
                                            onblur="cal1()" MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtGreenDevelEnviMaintExistRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtGreenDevelEnviMaintExistRequirement"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtGreenDevelExist" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtGreenDevelEnviMaintExistRequirement"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGreenDevelEnviMaintProposedRequirement" runat="server" Width="95%"
                                            onblur="cal1()" MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtGreenDevelEnviMaintProposedRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtGreenDevelEnviMaintProposedRequirement"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtGreenDevelProposed" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtGreenDevelEnviMaintProposedRequirement"
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
                                        <asp:RegularExpressionValidator ID="revtxtGreenDevelDaysInYear" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtGreenDevelEnviMaintNoOfOperationalDaysInYear"
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
                                    <td>Other Use
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUseExistRequirement" runat="server" Width="95%" onblur="cal1()"
                                            MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtOtherUseExistRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtOtherUseExistRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtOtherUseExist" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtOtherUseExistRequirement"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUseProposedRequirement" runat="server" Width="95%" onblur="cal1()" MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtOtherUseProposedRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtOtherUseProposedRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtOtherUseProposed" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtOtherUseProposedRequirement"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUseTotalRequirement" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUseNoOfOperationalDaysInYear" runat="server" Width="95%" MaxLength="3"
                                            onblur="cal1()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtOtherUseNoOfOperationalDaysInYear" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtOtherUseDaysInYear" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtOtherUseNoOfOperationalDaysInYear"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="RangeValidator4" runat="server" ForeColor="Red" MinimumValue="0"
                                            MaximumValue="365" ErrorMessage="Value should be between 0 and 365" ValidationGroup="WaterRequirementDetails"
                                            Display="Dynamic" ControlToValidate="txtOtherUseNoOfOperationalDaysInYear" Type="Integer"></asp:RangeValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUseAnnualRequirement" runat="server" Width="95%" Enabled="false"></asp:TextBox>
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
                                        <asp:TextBox ID="txtGrandTotalProposedRequirement" runat="server" Width="95%" Enabled="false"></asp:TextBox>
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
                            <asp:Label ID="lblIndustialApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
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
