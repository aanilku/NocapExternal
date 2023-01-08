<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" CodeFile="INDNewSADChargeDetails.aspx.cs" Inherits="ExternalUser_IndustrialNew_INDNewSADChargeDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
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

            txtGrandTotalAnnualRequirement = parseFloat(txtIndActAnnualRequirement) + parseFloat(txtResidDomAnnualRequirement) + parseFloat(txtGreenDevelEnviMaintAnnualRequirement) + parseFloat(txtOtherUseAnnualRequirement);
            document.getElementById('<%= txtGrandTotalAnnualRequirement.ClientID %>').value = parseFloat(txtGrandTotalAnnualRequirement).toFixed(2);
            // document.getElementById('<%= txtAnnualReq.ClientID %>').value = parseFloat(txtGrandTotalAnnualRequirement).toFixed(2);
           
            document.getElementById('<%= txtAnnualReq.ClientID %>').value = (parseFloat(txtGrandTotalAnnualRequirement) - parseFloat(document.getElementById('<%= txtTotalTreatedWaterUtilizedInYear.ClientID %>').value)).toFixed(2);
             document.getElementById('<%= txtCharge.ClientID %>').value = (parseFloat(document.getElementById('<%= txtAnnualReq.ClientID %>').value) * parseFloat(document.getElementById('<%= txtRate.ClientID %>').value)).toFixed(2);



        }
        function CalculateSumTreatedWaterUtilizedInDay() {
            var SumTreatedWaterDay = "0", SumTreatedWaterYear = "0";
            if (document.getElementById('<%=txtQuantityReuseIndustrialActivityInDay.ClientID%>').value != "") {
                SumTreatedWaterDay = Number(SumTreatedWaterDay) + Number(document.getElementById('<%=txtQuantityReuseIndustrialActivityInDay.ClientID%>').value);
                if (document.getElementById('<%=txtQuantityReuseIndustrialActivityNoOfDay.ClientID%>').value != "") {
                    SumTreatedWaterYear = Number(SumTreatedWaterYear) + (Number(document.getElementById('<%=txtQuantityReuseIndustrialActivityInDay.ClientID%>').value) * Number(document.getElementById('<%=txtQuantityReuseIndustrialActivityNoOfDay.ClientID%>').value));
                    document.getElementById('<%=txtQuantityReuseIndustrialActivityInYear.ClientID%>').value = Number(Number(document.getElementById('<%=txtQuantityReuseIndustrialActivityInDay.ClientID%>').value) * Number(document.getElementById('<%=txtQuantityReuseIndustrialActivityNoOfDay.ClientID%>').value)).toFixed(2);
                }
            }
            if (document.getElementById('<%=txtQuantityReuseGreenBeltDevelInDay.ClientID%>').value != "") {
                 SumTreatedWaterDay = Number(SumTreatedWaterDay) + Number(document.getElementById('<%=txtQuantityReuseGreenBeltDevelInDay.ClientID%>').value);
                if (document.getElementById('<%=txtQuantityReuseGreenBeltDevelInDay.ClientID%>').value != "") {
                    SumTreatedWaterYear = Number(SumTreatedWaterYear) + (Number(document.getElementById('<%=txtQuantityReuseGreenBeltDevelInDay.ClientID%>').value) * Number(document.getElementById('<%=txtQuantityReuseGreenBeltDevelNoOfDay.ClientID%>').value));
                    document.getElementById('<%=txtQuantityReuseGreenBeltDevelInYear.ClientID%>').value = Number(Number(document.getElementById('<%=txtQuantityReuseGreenBeltDevelInDay.ClientID%>').value) * Number(document.getElementById('<%=txtQuantityReuseGreenBeltDevelNoOfDay.ClientID%>').value)).toFixed(2);
                }
            }
            if (document.getElementById('<%=txtOtherUsesInDay.ClientID%>').value != "") {
                 SumTreatedWaterDay = Number(SumTreatedWaterDay) + Number(document.getElementById('<%=txtOtherUsesInDay.ClientID%>').value);
                if (document.getElementById('<%=txtOtherUsesNoOfDay.ClientID%>').value != "") {
                    SumTreatedWaterYear = Number(SumTreatedWaterYear) + (Number(document.getElementById('<%=txtOtherUsesInDay.ClientID%>').value) * Number(document.getElementById('<%=txtOtherUsesNoOfDay.ClientID%>').value));
                    document.getElementById('<%=txtOtherUsesInYear.ClientID%>').value = Number(Number(document.getElementById('<%=txtOtherUsesInDay.ClientID%>').value) * Number(document.getElementById('<%=txtOtherUsesNoOfDay.ClientID%>').value)).toFixed(2);
                }
            }
            if (SumTreatedWaterDay != "0") {
                document.getElementById('<%=txtTotalTreatedWaterUtilizedInDay.ClientID%>').value = Number(SumTreatedWaterDay).toFixed(2);
            }
            if (SumTreatedWaterYear != "0") {
                document.getElementById('<%=txtTotalTreatedWaterUtilizedInYear.ClientID%>').value = Number(SumTreatedWaterYear).toFixed(2);
            }

            var YearWiseSum = "0";
            if (document.getElementById('<%=txtTotalwasteWaterGeneratedInDay.ClientID%>').value != "") {
                if (document.getElementById('<%=txtTotalwasteWaterGeneratedNoOfDay.ClientID%>').value != "") {
                    document.getElementById('<%=txtTotalwasteWaterGeneratedInYear.ClientID%>').value = Number(Number(document.getElementById('<%=txtTotalwasteWaterGeneratedInDay.ClientID%>').value) * Number(document.getElementById('<%=txtTotalwasteWaterGeneratedNoOfDay.ClientID%>').value));
                }
            }


        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <table align="center" class="SubFormWOBG" width="100%">
        <tr>
            <td>
                <table align="right" class="SubFormWOBG" style="line-height: 25px" width="100%">
                    <tr>
                        <td colspan="6">
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                Ground Water Charge Details
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>(i) Breakup of Water Requirement and Usage:</b>
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
                                        <asp:TextBox ID="txtIndActExistRequirement" Enabled="false" runat="server"
                                            Width="95%" onblur="cal1()"
                                            MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtIndActExistRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtIndActExistRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtIndActExist" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtIndActExistRequirement" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                        <br />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIndActProposedRequirement" Enabled="false"
                                            runat="server" Width="95%" onblur="cal1()"
                                            MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtIndActProposedRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtIndActProposedRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtIndActProposed" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtIndActProposedRequirement" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIndActTotalRequirement" runat="server"
                                            Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIndActNoOfOperationalDaysInYear" Enabled="false" runat="server" Width="95%"
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
                                        <asp:TextBox ID="txtIndActAnnualRequirement"
                                            runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Residential / Domestic
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResidDomExistRequirement" runat="server"
                                            Enabled="false" Width="95%" onblur="cal1()"
                                            MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtResidDomExistRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtResidDomExistRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtResidDomExist" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtResidDomExistRequirement"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResidDomProposedRequirement" runat="server"
                                            Enabled="false" Width="95%" onblur="cal1()"
                                            MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtResidDomProposedRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtResidDomProposedRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtResidDomProposed" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtResidDomProposedRequirement"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResidDomTotalRequirement" runat="server"
                                            Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResidDomNoOfOperationalDaysInYear"
                                            Enabled="false" runat="server" Width="95%"
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
                                        <asp:TextBox ID="txtResidDomAnnualRequirement" runat="server"
                                            Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Greenbelt Development / Environment Maintenance
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGreenDevelEnviMaintExistRequirement"
                                            Enabled="false" runat="server" Width="95%"
                                            onblur="cal1()" MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtGreenDevelEnviMaintExistRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtGreenDevelEnviMaintExistRequirement"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtGreenDevelExist" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtGreenDevelEnviMaintExistRequirement"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGreenDevelEnviMaintProposedRequirement"
                                            Enabled="false" runat="server" Width="95%"
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
                                        <asp:TextBox ID="txtGreenDevelEnviMaintNoOfOperationalDaysInYear"
                                            Enabled="false" runat="server"
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
                                        <asp:TextBox ID="txtOtherUseExistRequirement" runat="server"
                                            Enabled="false" Width="95%" onblur="cal1()"
                                            MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtOtherUseExistRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtOtherUseExistRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtOtherUseExist" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtOtherUseExistRequirement"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUseProposedRequirement" runat="server"
                                            Enabled="false" Width="95%" onblur="cal1()" MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtOtherUseProposedRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtOtherUseProposedRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtOtherUseProposed" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtOtherUseProposedRequirement"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUseTotalRequirement" runat="server"
                                            Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUseNoOfOperationalDaysInYear"
                                            Enabled="false" runat="server" Width="95%" MaxLength="3"
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
                                        <asp:TextBox ID="txtOtherUseAnnualRequirement" runat="server"
                                            Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Grand Total</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGrandTotalExistRequirement" runat="server"
                                            Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGrandTotalProposedRequirement" runat="server"
                                            Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGrandTotalTotalRequirement" runat="server"
                                            Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGrandTotalNoOfOperationalDaysInYear" runat="server" Width="95%"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGrandTotalAnnualRequirement" runat="server"
                                            Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>


                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>(ii) Breakup of Recycled Water Usage:</b>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="4">
                            <table cellpadding="0" cellspacing="0" class="SubFormWOBG" width="100%">
                                <tr>
                                    <td></td>
                                    <th>(m<sup>3</sup>/day)
                                    </th>
                                    <th>No. of Operational Days in a Year
                                       
                                    </th>
                                    <th>(m<sup>3</sup>/year)
                                    </th>
                                </tr>
                                <tr>
                                    <td style="width: 30%">a) Total Waste Water Generated :
                                    </td>
                                    <td style="width: 23%">
                                        <asp:TextBox ID="txtTotalwasteWaterGeneratedInDay" runat="server"
                                            Enabled="false" MaxLength="9" Width="97%"
                                            onblur="CalculateSumTreatedWaterUtilizedInDay()"> </asp:TextBox>
                                    </td>
                                    <td style="width: 23%">
                                        <asp:TextBox ID="txtTotalwasteWaterGeneratedNoOfDay" runat="server"
                                            Enabled="false" MaxLength="3"
                                            Width="97%" onblur="CalculateSumTreatedWaterUtilizedInDay()"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalwasteWaterGeneratedInYear" runat="server"
                                            Width="97%" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>b). Quantity of Treated Water Available :
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtQuantityTreatedWaterAvailable" runat="server" MaxLength="9" Width="150px"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">i). Reuse in Industrial Activity :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQuantityReuseIndustrialActivityInDay" runat="server" MaxLength="9"
                                            Enabled="false" Width="97%" onblur="CalculateSumTreatedWaterUtilizedInDay()"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQuantityReuseIndustrialActivityNoOfDay" runat="server" Width="97%"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQuantityReuseIndustrialActivityInYear" runat="server" Enabled="false"
                                            Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">ii) Reuse in Greenbelt Development :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQuantityReuseGreenBeltDevelInDay" runat="server" MaxLength="9"
                                            Enabled="false" Width="97%" onblur="CalculateSumTreatedWaterUtilizedInDay()"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQuantityReuseGreenBeltDevelNoOfDay" runat="server" Text="" Enabled="false"
                                            Width="97%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQuantityReuseGreenBeltDevelInYear" runat="server" Text="" Enabled="false"
                                            Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">iii) Other Uses :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUsesInDay" runat="server" MaxLength="9"
                                            Enabled="false" Width="97%" onblur="CalculateSumTreatedWaterUtilizedInDay()"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUsesNoOfDay" runat="server" MaxLength="9"
                                            Width="97%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUsesInYear" runat="server"
                                            Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>c). Total Treated Water Utilised :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalTreatedWaterUtilizedInDay" runat="server" Enabled="false"
                                            Width="97%"></asp:TextBox>
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:TextBox ID="txtTotalTreatedWaterUtilizedInYear" runat="server" Enabled="false"
                                            Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <table cellpadding="0" cellspacing="0" class="SubFormWOBG" width="100%">

                                <tr>

                                    <td>
                                        <strong>Annual Requirement (m<sup>3</sup>/year) (A=i-ii)</strong>

                                    </td>


                                    <td>
                                        <asp:TextBox ID="txtAnnualReq" runat="server"
                                            Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>

                                    <td>
                                        <strong>Rate (B)</strong>

                                    </td>


                                    <td>
                                        <asp:TextBox ID="txtRate" runat="server"
                                            Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>

                                    <td>
                                        <strong>Charge (A*B)</strong>

                                    </td>


                                    <td>
                                        <asp:TextBox ID="txtCharge" runat="server"
                                            Width="95%" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
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

    </table>
</asp:Content>

