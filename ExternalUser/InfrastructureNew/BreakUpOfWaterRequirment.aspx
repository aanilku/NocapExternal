<%@ Page Title="NOCAP-Infrastructure Application-New" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="BreakUpOfWaterRequirment.aspx.cs"
    Inherits="ExternalUser_InfrastructureNew_BreakUpOfWaterRequirment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../../Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function cal() {
            var ExistingReqTotalDay = "0", ProposedReqTotalDay = "0", TotalRequirementDayIND = "0", TotalRequirementDayRed = "0", TotalRequirementDayCom = "0", TotalRequirementDayGD = "0", TotalRequirementDayOth = "0", TotalRequirementDayCA="0";
            if (document.getElementById('<%=txtIndActExistRequirement.ClientID%>').value != "") {
                ExistingReqTotalDay = Number(ExistingReqTotalDay) + Number(document.getElementById('<%=txtIndActExistRequirement.ClientID%>').value);
                TotalRequirementDayIND = Number(TotalRequirementDayIND) + Number(document.getElementById('<%=txtIndActExistRequirement.ClientID%>').value);
            }
            if (document.getElementById('<%=txtResidDomExistRequirement.ClientID%>').value != "") {
                ExistingReqTotalDay = Number(ExistingReqTotalDay) + Number(document.getElementById('<%=txtResidDomExistRequirement.ClientID%>').value);
                TotalRequirementDayRed = Number(TotalRequirementDayRed) + Number(document.getElementById('<%=txtResidDomExistRequirement.ClientID%>').value);
            }
            if (document.getElementById('<%=txtCOMExistRequirement.ClientID%>').value != "") {
                ExistingReqTotalDay = Number(ExistingReqTotalDay) + Number(document.getElementById('<%=txtCOMExistRequirement.ClientID%>').value);
                TotalRequirementDayCom = Number(TotalRequirementDayCom) + Number(document.getElementById('<%=txtCOMExistRequirement.ClientID%>').value);
            }
            if (document.getElementById('<%=txtGreenDevelEnviMaintExistRequirement.ClientID%>').value != "") {
                ExistingReqTotalDay = Number(ExistingReqTotalDay) + Number(document.getElementById('<%=txtGreenDevelEnviMaintExistRequirement.ClientID%>').value);
                TotalRequirementDayGD = Number(TotalRequirementDayGD) + Number(document.getElementById('<%=txtGreenDevelEnviMaintExistRequirement.ClientID%>').value);
            }
        

             if (document.getElementById('<%=txtConstActExist.ClientID%>').value != "") {
                ExistingReqTotalDay = Number(ExistingReqTotalDay) + Number(document.getElementById('<%=txtConstActExist.ClientID%>').value);
             
                 TotalRequirementDayCA = Number(TotalRequirementDayCA) + Number(document.getElementById('<%=txtConstActExist.ClientID%>').value);
             
             }
           
            if (document.getElementById('<%=txtOtherUseExistRequirement.ClientID%>').value != "") {
                ExistingReqTotalDay = Number(ExistingReqTotalDay) + Number(document.getElementById('<%=txtOtherUseExistRequirement.ClientID%>').value);
                TotalRequirementDayOth = Number(TotalRequirementDayOth) + Number(document.getElementById('<%=txtOtherUseExistRequirement.ClientID%>').value);
            }

            if (document.getElementById('<%=txtIndActProposedRequirement.ClientID%>').value != "") {
                ProposedReqTotalDay = Number(ProposedReqTotalDay) + Number(document.getElementById('<%=txtIndActProposedRequirement.ClientID%>').value);
                TotalRequirementDayIND = Number(TotalRequirementDayIND) + Number(document.getElementById('<%=txtIndActProposedRequirement.ClientID%>').value);
            }
            if (document.getElementById('<%=txtResidDomProposedRequirement.ClientID%>').value != "") {
                ProposedReqTotalDay = Number(ProposedReqTotalDay) + Number(document.getElementById('<%=txtResidDomProposedRequirement.ClientID%>').value);
                TotalRequirementDayRed = Number(TotalRequirementDayRed) + Number(document.getElementById('<%=txtResidDomProposedRequirement.ClientID%>').value);
            }
            if (document.getElementById('<%=txtCOMProposedRequirement.ClientID%>').value != "") {
                ProposedReqTotalDay = Number(ProposedReqTotalDay) + Number(document.getElementById('<%=txtCOMProposedRequirement.ClientID%>').value);
                TotalRequirementDayCom = Number(TotalRequirementDayCom) + Number(document.getElementById('<%=txtCOMProposedRequirement.ClientID%>').value);
            }
            if (document.getElementById('<%=txtGreenDevelEnviMaintProposedRequirement.ClientID%>').value != "") {
                ProposedReqTotalDay = Number(ProposedReqTotalDay) + Number(document.getElementById('<%=txtGreenDevelEnviMaintProposedRequirement.ClientID%>').value);
                TotalRequirementDayGD = Number(TotalRequirementDayGD) + Number(document.getElementById('<%=txtGreenDevelEnviMaintProposedRequirement.ClientID%>').value);
            }

             if (document.getElementById('<%=txtConstActProposed.ClientID%>').value != "") {
                ProposedReqTotalDay = Number(ProposedReqTotalDay) + Number(document.getElementById('<%=txtConstActProposed.ClientID%>').value);
                TotalRequirementDayCA = Number(TotalRequirementDayCA) + Number(document.getElementById('<%=txtConstActProposed.ClientID%>').value);
            }
            if (document.getElementById('<%=txtOtherUseProposedRequirement.ClientID%>').value != "") {
                ProposedReqTotalDay = Number(ProposedReqTotalDay) + Number(document.getElementById('<%=txtOtherUseProposedRequirement.ClientID%>').value);
                TotalRequirementDayOth = Number(TotalRequirementDayOth) + Number(document.getElementById('<%=txtOtherUseProposedRequirement.ClientID%>').value);
            }

            document.getElementById('<%=txtGrandTotalExistRequirement.ClientID%>').value = Number(ExistingReqTotalDay).toFixed(2);
            document.getElementById('<%=txtGrandTotalProposedRequirement.ClientID%>').value = Number(ProposedReqTotalDay).toFixed(2);

            document.getElementById('<%=txtIndActTotalRequirement.ClientID%>').value = Number(TotalRequirementDayIND).toFixed(2);
            document.getElementById('<%=txtResidDomTotalRequirement.ClientID%>').value = Number(TotalRequirementDayRed).toFixed(2);
            document.getElementById('<%=txtCOMTotalRequirement.ClientID%>').value = Number(TotalRequirementDayCom).toFixed(2);
            document.getElementById('<%=txtGreenDevelEnviMaintTotalRequirement.ClientID%>').value = Number(TotalRequirementDayGD).toFixed(2);
        
             document.getElementById('<%=txtConstActTotal.ClientID%>').value = Number(TotalRequirementDayCA).toFixed(2);

            document.getElementById('<%=txtOtherUseTotalRequirement.ClientID%>').value = Number(TotalRequirementDayOth).toFixed(2);

            document.getElementById('<%=txtGrandTotalTotalRequirement.ClientID%>').value = Number(Number(TotalRequirementDayIND) + Number(TotalRequirementDayRed) + Number(TotalRequirementDayCom) + Number(TotalRequirementDayGD) + Number(TotalRequirementDayCA) + Number(TotalRequirementDayOth)).toFixed(2);
            var ddlETPSTPProposed = document.getElementById('<%=ddlETPSTPProposed.ClientID %>');
            var vSkillText = ddlETPSTPProposed.options[ddlETPSTPProposed.selectedIndex].innerHTML;
            alert
            var TotalAnnualSum = "0";
            if (document.getElementById('<%=txtIndActNoOfOperationalDaysInYear.ClientID%>').value != "") {
                document.getElementById('<%=txtIndActAnnualRequirement.ClientID%>').value = Number(Number(TotalRequirementDayIND) * Number(document.getElementById('<%=txtIndActNoOfOperationalDaysInYear.ClientID%>').value)).toFixed(2);
                TotalAnnualSum = Number(TotalAnnualSum) + Number(document.getElementById('<%=txtIndActAnnualRequirement.ClientID%>').value);
                if (vSkillText == "Yes") {
                    document.getElementById('<%=txtQuantityReuseIndustrialActivityNoOfDay.ClientID%>').value = document.getElementById('<%=txtIndActNoOfOperationalDaysInYear.ClientID%>').value;
                }
            }
            if (document.getElementById('<%=txtResidDomNoOfOperationalDaysInYear.ClientID%>').value != "") {
                document.getElementById('<%=txtResidDomAnnualRequirement.ClientID%>').value = Number(Number(TotalRequirementDayRed) * Number(document.getElementById('<%=txtResidDomNoOfOperationalDaysInYear.ClientID%>').value)).toFixed(2);
                TotalAnnualSum = Number(TotalAnnualSum) + Number(document.getElementById('<%=txtResidDomAnnualRequirement.ClientID%>').value);
            }
            if (document.getElementById('<%=txtCOMNoOfOperationalDaysInYear.ClientID%>').value != "") {
                document.getElementById('<%=txtCOMAnnualRequirement.ClientID%>').value = Number(Number(TotalRequirementDayCom) * Number(document.getElementById('<%=txtCOMNoOfOperationalDaysInYear.ClientID%>').value)).toFixed(2);
                TotalAnnualSum = Number(TotalAnnualSum) + Number(document.getElementById('<%=txtCOMAnnualRequirement.ClientID%>').value);
                if (vSkillText == "Yes") {
                    document.getElementById('<%=txtQuantityReuseCommercialActivityNoOfDay.ClientID%>').value = document.getElementById('<%=txtCOMNoOfOperationalDaysInYear.ClientID%>').value;
                }
            }
            if (document.getElementById('<%=txtGreenDevelEnviMaintNoOfOperationalDaysInYear.ClientID%>').value != "") {
                document.getElementById('<%=txtGreenDevelEnviMaintAnnualRequirement.ClientID%>').value = Number(Number(TotalRequirementDayGD) * Number(document.getElementById('<%=txtGreenDevelEnviMaintNoOfOperationalDaysInYear.ClientID%>').value)).toFixed(2);
                TotalAnnualSum = Number(TotalAnnualSum) + Number(document.getElementById('<%=txtGreenDevelEnviMaintAnnualRequirement.ClientID%>').value);
                if (vSkillText == "Yes") {
                    document.getElementById('<%=txtQuantityReuseGreenBeltDevelNoOfDay.ClientID%>').value = document.getElementById('<%=txtGreenDevelEnviMaintNoOfOperationalDaysInYear.ClientID%>').value;
                }
            }
              if (document.getElementById('<%=txtConstActNoOfOperationalDaysInYear.ClientID%>').value != "") {
                document.getElementById('<%=txtConstActAnnual.ClientID%>').value = Number(Number(TotalRequirementDayCA) * Number(document.getElementById('<%=txtConstActNoOfOperationalDaysInYear.ClientID%>').value)).toFixed(2);
                TotalAnnualSum = Number(TotalAnnualSum) + Number(document.getElementById('<%=txtConstActAnnual.ClientID%>').value);
                if (vSkillText == "Yes") {
                    document.getElementById('<%=txtQuantityReuseGreenBeltDevelNoOfDay.ClientID%>').value = document.getElementById('<%=txtConstActNoOfOperationalDaysInYear.ClientID%>').value;
                }
            }


            if (document.getElementById('<%=txtOtherUseNoOfOperationalDaysInYear.ClientID%>').value != "") {
                document.getElementById('<%=txtOtherUseAnnualRequirement.ClientID%>').value = Number(Number(TotalRequirementDayOth) * Number(document.getElementById('<%=txtOtherUseNoOfOperationalDaysInYear.ClientID%>').value)).toFixed(2);
                TotalAnnualSum = Number(TotalAnnualSum) + Number(document.getElementById('<%=txtOtherUseAnnualRequirement.ClientID%>').value);
                if (vSkillText == "Yes") {
                    document.getElementById('<%=txtOtherUsesNoOfDay.ClientID%>').value = document.getElementById('<%=txtOtherUseNoOfOperationalDaysInYear.ClientID%>').value;
                }
            }
            document.getElementById('<%=txtGrandTotalAnnualRequirement.ClientID%>').value = Number(TotalAnnualSum).toFixed(2);

            CalculateSumTreatedWaterUtilizedInDay();

        }



        function WaterRequirementMessage() {
            var txtWaterRequirementExisting = document.getElementById('<%= hdnWaterReqExistingTotal.ClientID %>').value;
            var txtBreakupOfWaterExisting = "0";

            if (document.getElementById('<%= txtIndActExistRequirement.ClientID %>').value != "") {
                txtBreakupOfWaterExisting = Number(txtBreakupOfWaterExisting) + Number(document.getElementById('<%= txtIndActExistRequirement.ClientID %>').value);
            }
            if (document.getElementById('<%= txtResidDomExistRequirement.ClientID %>').value != "") {
                txtBreakupOfWaterExisting = Number(txtBreakupOfWaterExisting) + Number(document.getElementById('<%= txtResidDomExistRequirement.ClientID %>').value);
            }
            if (document.getElementById('<%= txtCOMExistRequirement.ClientID %>').value != "") {
                txtBreakupOfWaterExisting = Number(txtBreakupOfWaterExisting) + Number(document.getElementById('<%= txtCOMExistRequirement.ClientID %>').value);
            }
            if (document.getElementById('<%= txtGreenDevelEnviMaintExistRequirement.ClientID %>').value != "") {
                txtBreakupOfWaterExisting = Number(txtBreakupOfWaterExisting) + Number(document.getElementById('<%= txtGreenDevelEnviMaintExistRequirement.ClientID %>').value);
            }
             if (document.getElementById('<%= txtConstActExist.ClientID %>').value != "") {
                 txtBreakupOfWaterExisting = Number(txtBreakupOfWaterExisting) + Number(document.getElementById('<%= txtConstActExist.ClientID %>').value);
            }
            if (document.getElementById('<%= txtOtherUseExistRequirement.ClientID %>').value != "") {
                txtBreakupOfWaterExisting = Number(txtBreakupOfWaterExisting) + Number(document.getElementById('<%= txtOtherUseExistRequirement.ClientID %>').value);
            }
            if (txtWaterRequirementExisting != txtBreakupOfWaterExisting) {
                alert("Please Modify Breakup of Water Requirement and Usage Existing 3(ii)- It should equal to Total Water Requirement Existing Section 3(i).");
            }


            var txtWaterRequirementProposed = document.getElementById('<%= hdnWaterReqProposedTotal.ClientID %>').value;
            var txtBreakupOfWaterProposed = "0";
            if (document.getElementById('<%= txtIndActProposedRequirement.ClientID %>').value != "") {
                txtBreakupOfWaterProposed = Number(txtBreakupOfWaterProposed) + Number(document.getElementById('<%= txtIndActProposedRequirement.ClientID %>').value);
            }
            if (document.getElementById('<%= txtResidDomProposedRequirement.ClientID %>').value != "") {
                txtBreakupOfWaterProposed = Number(txtBreakupOfWaterProposed) + Number(document.getElementById('<%= txtResidDomProposedRequirement.ClientID %>').value);
            }

            if (document.getElementById('<%= txtCOMProposedRequirement.ClientID %>').value != "") {
                txtBreakupOfWaterProposed = Number(txtBreakupOfWaterProposed) + Number(document.getElementById('<%= txtCOMProposedRequirement.ClientID %>').value);
            }

            if (document.getElementById('<%= txtGreenDevelEnviMaintProposedRequirement.ClientID %>').value != "") {
                txtBreakupOfWaterProposed = Number(txtBreakupOfWaterProposed) + Number(document.getElementById('<%= txtGreenDevelEnviMaintProposedRequirement.ClientID %>').value);
            }
             if (document.getElementById('<%= txtConstActProposed.ClientID %>').value != "") {
                txtBreakupOfWaterProposed = Number(txtBreakupOfWaterProposed) + Number(document.getElementById('<%= txtConstActProposed.ClientID %>').value);
            }

            if (document.getElementById('<%= txtOtherUseProposedRequirement.ClientID %>').value != "") {
                txtBreakupOfWaterProposed = Number(txtBreakupOfWaterProposed) + Number(document.getElementById('<%= txtOtherUseProposedRequirement.ClientID %>').value);
            }

            if (txtWaterRequirementProposed != txtBreakupOfWaterProposed) {
                alert("Please Modify Breakup of Water Requirement and Usage Proposed 3(ii)- It should equal to Total Water Requrement Proposed Section 3(i)");
            }








            var txtQuantityTreatedWaterAvailable = document.getElementById('<%= hdnRecycledWaterUsageTotal.ClientID %>').value;
            var ExistingReqTotalDay = "0";
            if (document.getElementById('<%=txtQuantityReuseCommercialActivityInDay.ClientID%>').value != "") {
                ExistingReqTotalDay = Number(ExistingReqTotalDay) + Number(document.getElementById('<%=txtQuantityReuseCommercialActivityInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtQuantityReuseIndustrialActivityInDay.ClientID%>').value != "") {
                ExistingReqTotalDay = Number(ExistingReqTotalDay) + Number(document.getElementById('<%=txtQuantityReuseIndustrialActivityInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtQuantityReuseGreenBeltDevelInDay.ClientID%>').value != "") {
                ExistingReqTotalDay = Number(ExistingReqTotalDay) + Number(document.getElementById('<%=txtQuantityReuseGreenBeltDevelInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtOtherUsesInDay.ClientID%>').value != "") {
                ExistingReqTotalDay = Number(ExistingReqTotalDay) + Number(document.getElementById('<%=txtOtherUsesInDay.ClientID%>').value);
            }
            if (txtQuantityTreatedWaterAvailable != ExistingReqTotalDay) {
                alert("Please Check Reuse water Quantity (i,ii, III,IV) above ; Total should be eqaul to 'Quantity of Treated Water utilised Section 3(i)-d'.");
            }
        }


        function CalculateSumTreatedWaterUtilizedInDay() {
            var SumTreatedWaterDay = "0", SumTreatedWaterYear = "0";
            if (document.getElementById('<%=txtQuantityReuseCommercialActivityInDay.ClientID%>').value != "") {
                SumTreatedWaterDay = Number(SumTreatedWaterDay) + Number(document.getElementById('<%=txtQuantityReuseCommercialActivityInDay.ClientID%>').value);
                if (document.getElementById('<%=txtQuantityReuseCommercialActivityNoOfDay.ClientID%>').value != "") {
                    SumTreatedWaterYear = Number(SumTreatedWaterYear) + (Number(document.getElementById('<%=txtQuantityReuseCommercialActivityInDay.ClientID%>').value) * Number(document.getElementById('<%=txtQuantityReuseCommercialActivityNoOfDay.ClientID%>').value));
                    document.getElementById('<%=txtQuantityReuseCommercialActivityInYear.ClientID%>').value = Number(Number(document.getElementById('<%=txtQuantityReuseCommercialActivityInDay.ClientID%>').value) * Number(document.getElementById('<%=txtQuantityReuseCommercialActivityNoOfDay.ClientID%>').value)).toFixed(2);
                }
            }

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
    <asp:HiddenField ID="hdnWaterReqExistingTotal" runat="server" Value="" />
    <asp:HiddenField ID="hdnWaterReqProposedTotal" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdnRecycledWaterUsageTotal" runat="server"></asp:HiddenField>
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
                                            <li class="visited">Water Requirement Details</li>
                                            <li class="visited">De-Watering Existing Structure</li>
                                            <li class="visited">De-Watering Proposed Structure</li>
                                            <li class="visited">Groundwater Abstraction Structure-Existing</li>
                                            <li class="visited">Groundwater Abstraction Structure-Proposed</li>
                                            <li class="active">Breakup of Water Requirment</li>
                                            <li>Water Supply Detail</li>
                                            <%--<li>Groundwater Abstraction Structure-Existing</li>
                                            <li>Groundwater Abstraction Structure-Proposed</li>--%>
                                            <li>Other Details</li>
                                           <%-- <li>Self Declaration</li>--%>
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
                        <td colspan="2">
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                INFRASTRUCTURE USE: 3 (ii) Breack Up of Water 
                                Requirment and Usage
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="2">(<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="style8">$</span>)-Upload
                            Attachments in <strong style="color: Red">Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <b>3(ii) Breakup of Water Requirement and Usage :</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="100%" class="SubFormWOBG">
                                <tr>
                                    <th style="width: 20%">
                                        <b>Activity</b>
                                    </th>
                                    <th style="width: 17%">
                                        <b>Existing Requirement (m&sup3/day)</b>
                                    </th>
                                    <th style="width: 17%">
                                        <b>Proposed Requirement (m&sup3/day)</b>
                                    </th>
                                    <th>
                                        <b>Total Requirement (m&sup3/day)</b>
                                    </th>
                                    <th style="width: 17%">
                                        <b>No. of Operational Days in a Year</b>
                                    </th>
                                    <th style="width: 17%">
                                        <b>Annual Requirement (m&sup3/year)</b>
                                    </th>
                                </tr>
                                <tr>
                                    <td>Residential / Domestic
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResidDomExistRequirement" runat="server" Width="97%" MaxLength="9"
                                            onblur="cal()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtResidDomExistRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtResidDomExistRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtResidDomExist" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtResidDomExistRequirement"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResidDomProposedRequirement" runat="server" Width="97%" MaxLength="9"
                                            onblur="cal()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtResidDomProposedRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtResidDomProposedRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtResidDomProposed" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtResidDomProposedRequirement"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResidDomTotalRequirement" runat="server" Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResidDomNoOfOperationalDaysInYear" runat="server" Width="97%"
                                            onblur="cal()" MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ForeColor="Red"
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
                                        <asp:TextBox ID="txtResidDomAnnualRequirement" Width="97%" runat="server" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Commercial Activity
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCOMExistRequirement" runat="server" Width="97%" MaxLength="9"
                                            onblur="cal()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtCOMExistRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtCOMExistRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtCOMExist" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtCOMExistRequirement" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCOMProposedRequirement" runat="server" Width="97%" MaxLength="9"
                                            onblur="cal()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtCOMProposedRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtCOMProposedRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtCOMProposed" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtCOMProposedRequirement"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCOMTotalRequirement" runat="server" Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCOMNoOfOperationalDaysInYear" runat="server" onblur="cal()" Width="97%"
                                            MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtCOMNoOfOperationalDaysInYear" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtCOMDaysInYear" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtCOMNoOfOperationalDaysInYear"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="RangeValidator3" runat="server" ForeColor="Red" MinimumValue="0"
                                            MaximumValue="365" ErrorMessage="Value should be between 0 and 365" ValidationGroup="WaterRequirementDetails"
                                            Display="Dynamic" ControlToValidate="txtCOMNoOfOperationalDaysInYear" Type="Integer"></asp:RangeValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCOMAnnualRequirement" runat="server" Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trIndActExistRequirement" runat="server" visible="false">
                                    <td>Industrial Activity
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIndActExistRequirement" runat="server" MaxLength="9" onblur="cal()"
                                            Width="97%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtIndActExistRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtIndActExistRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtIndActExist" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtIndActExistRequirement"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIndActProposedRequirement" runat="server" MaxLength="9" onblur="cal()"
                                            Width="97%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtIndActProposedRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtIndActProposedRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtIndActProposed" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtIndActProposedRequirement" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIndActTotalRequirement" runat="server" Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIndActNoOfOperationalDaysInYear" runat="server" onblur="cal()"
                                            Width="97%" MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtIndActNoOfOperationalDaysInYear" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtIndActDaysInYear" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtIndActNoOfOperationalDaysInYear"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ForeColor="Red" MinimumValue="0"
                                            MaximumValue="365" ErrorMessage="Value should be between 0 and 365" ValidationGroup="WaterRequirementDetails"
                                            Display="Dynamic" ControlToValidate="txtIndActNoOfOperationalDaysInYear" Type="Integer"></asp:RangeValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIndActAnnualRequirement" Width="97%" runat="server" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Greenbelt Development / Environment Maintenance
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGreenDevelEnviMaintExistRequirement" runat="server" MaxLength="9"
                                            onblur="cal()" Width="97%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtGreenDevelEnviMaintExistRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtGreenDevelEnviMaintExistRequirement"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtGreenDevelExist" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtGreenDevelEnviMaintExistRequirement"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGreenDevelEnviMaintProposedRequirement" runat="server" MaxLength="9"
                                            onblur="cal()" Width="97%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtGreenDevelEnviMaintProposedRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtGreenDevelEnviMaintProposedRequirement"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtGreenDevelProposed" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtGreenDevelEnviMaintProposedRequirement"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGreenDevelEnviMaintTotalRequirement" runat="server" Enabled="false"
                                            Width="97%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGreenDevelEnviMaintNoOfOperationalDaysInYear" runat="server"
                                            onblur="cal()" Width="97%" MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtGreenDevelEnviMaintNoOfOperationalDaysInYear"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtGreenDevelDaysInYear" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtGreenDevelEnviMaintNoOfOperationalDaysInYear"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="RangeValidator4" runat="server" ForeColor="Red" MinimumValue="0"
                                            MaximumValue="365" ErrorMessage="Value should be between 0 and 365" ValidationGroup="WaterRequirementDetails"
                                            Display="Dynamic" ControlToValidate="txtGreenDevelEnviMaintNoOfOperationalDaysInYear"
                                            Type="Integer"></asp:RangeValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGreenDevelEnviMaintAnnualRequirement" runat="server" Enabled="false"
                                            Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Construction Activity</td>
                                    <td>
                                        <asp:TextBox ID="txtConstActExist" runat="server" MaxLength="9"
                                            onblur="cal()" Width="97%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtConstAct" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtConstActExist"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtConstAct" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtConstActExist"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtConstActProposed" runat="server" MaxLength="9"
                                            onblur="cal()" Width="97%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtConstActProposed" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtConstActProposed"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtConstActProposed" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtConstActProposed"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtConstActTotal" runat="server" Enabled="false"
                                            Width="97%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtConstActNoOfOperationalDaysInYear" runat="server"
                                            onblur="cal()" Width="97%" MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtConstActNoOfOperationalDaysInYear"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtConstActNoOfOperationalDaysInYear" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtConstActNoOfOperationalDaysInYear"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="RangeValidator7" runat="server" ForeColor="Red" MinimumValue="0"
                                            MaximumValue="365" ErrorMessage="Value should be between 0 and 365" ValidationGroup="WaterRequirementDetails"
                                            Display="Dynamic" ControlToValidate="txtGreenDevelEnviMaintNoOfOperationalDaysInYear"
                                            Type="Integer"></asp:RangeValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtConstActAnnual" runat="server" Enabled="false"
                                            Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Other Use
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUseExistRequirement" runat="server" Width="97%" MaxLength="9"
                                            onblur="cal()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtOtherUseExistRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtOtherUseExistRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtOtherUseExist" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtOtherUseExistRequirement"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUseProposedRequirement" onblur="cal()" runat="server" Width="97%" MaxLength="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtOtherUseProposedRequirement" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtOtherUseProposedRequirement" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtOtherUseProposed" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtOtherUseProposedRequirement"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUseTotalRequirement" runat="server" Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUseNoOfOperationalDaysInYear" runat="server" Width="97%" MaxLength="3"
                                            onblur="cal()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtOtherUseNoOfOperationalDaysInYear" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtOtherUseDaysInYear" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtOtherUseNoOfOperationalDaysInYear"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="RangeValidator5" runat="server" ForeColor="Red" MinimumValue="0"
                                            MaximumValue="365" ErrorMessage="Value should be between 0 and 365" ValidationGroup="WaterRequirementDetails"
                                            Display="Dynamic" ControlToValidate="txtOtherUseNoOfOperationalDaysInYear" Type="Integer"></asp:RangeValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUseAnnualRequirement" Width="97%" runat="server" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Grand Total</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGrandTotalExistRequirement" runat="server" Width="97%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGrandTotalProposedRequirement" runat="server" Width="97%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGrandTotalTotalRequirement" runat="server" Width="97%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:TextBox ID="txtGrandTotalAnnualRequirement" Width="97%" runat="server" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>3(iii) Whether ETP/STP Proposed, if so furnish following details</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlETPSTPProposed" runat="server" OnSelectedIndexChanged="ddlETPSTPProposed_SelectedIndexChanged"
                                AutoPostBack="true">
                                <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                <asp:ListItem Value="No" Text="No"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table cellpadding="0" cellspacing="0" class="SubFormWOBG" width="100%">
                                <tr>
                                    <th style="width: 30%">Breakup of Recycled Water Usage
                                    </th>
                                    <th style="width: 23%">(m<sup>3</sup>/day)
                                    </th>
                                    <th style="width: 23%">(Days)
                                    </th>
                                    <th>(m<sup>3</sup>/year)
                                    </th>
                                </tr>
                                <tr>
                                    <td>a) Total Waste Water Generated :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalwasteWaterGeneratedInDay" runat="server" MaxLength="9" Width="97%"
                                            onblur="CalculateSumTreatedWaterUtilizedInDay()"> </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtTotalwasteWaterGeneratedInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtTotalwasteWaterGeneratedInDay"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtTotalwasteWaterInDay" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtTotalwasteWaterGeneratedInDay" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalwasteWaterGeneratedNoOfDay" runat="server" MaxLength="3"
                                            Width="97%" onblur="CalculateSumTreatedWaterUtilizedInDay()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtTotalwasteWaterGeneratedNoOfDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtTotalwasteWaterGeneratedNoOfDay"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator6" runat="server" ForeColor="Red" MinimumValue="0"
                                            MaximumValue="365" ErrorMessage="Value should be between 0 and 365" ValidationGroup="WaterRequirementDetails"
                                            Display="Dynamic" ControlToValidate="txtTotalwasteWaterGeneratedNoOfDay" Type="Integer"></asp:RangeValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalwasteWaterGeneratedInYear" runat="server" Width="97%"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>b) Quantity of Treated Water Available :
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtQuantityTreatedWaterAvailable" runat="server" MaxLength="9" Width="100px"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">i). Reuse in Flushing Activity :<%--i). Reuse in Commercial Activity :--%>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQuantityReuseCommercialActivityInDay" runat="server" MaxLength="9"
                                            Width="97%" onblur="CalculateSumTreatedWaterUtilizedInDay()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtQuantityReuseCommercialActivityInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtQuantityReuseCommercialActivityInDay"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtQuantityReuseCommInDay" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtQuantityReuseCommercialActivityInDay"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQuantityReuseCommercialActivityNoOfDay" runat="server" Width="97%"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQuantityReuseCommercialActivityInYear" runat="server" Enabled="false"
                                            Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">ii). Reuse in Industrial Activity :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQuantityReuseIndustrialActivityInDay" runat="server" MaxLength="9"
                                            Width="97%" onblur="CalculateSumTreatedWaterUtilizedInDay()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtQuantityReuseIndustrialActivityInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtQuantityReuseIndustrialActivityInDay"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtQuantityReuseIndActInDay" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtQuantityReuseIndustrialActivityInDay"
                                            ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
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
                                    <td style="padding-left: 20px">iii) Reuse in Greenbelt Development :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQuantityReuseGreenBeltDevelInDay" runat="server" MaxLength="9"
                                            Width="97%" onblur="CalculateSumTreatedWaterUtilizedInDay()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtQuantityReuseGreenBeltDevelInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtQuantityReuseGreenBeltDevelInDay"
                                            ValidationGroup="WaterRequirementDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtQtyReuseGBDevInDay" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtQuantityReuseGreenBeltDevelInDay" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
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
                                    <td style="padding-left: 20px">iv) Other Uses (Please specify) :<%--iv) Other Uses :--%>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUsesInDay" runat="server" MaxLength="9" Width="97%" onblur="CalculateSumTreatedWaterUtilizedInDay()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtOtherUsesInDay" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtOtherUsesInDay" ValidationGroup="WaterRequirementDetails"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtOtherUsesInDay" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtOtherUsesInDay" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUsesNoOfDay" runat="server" MaxLength="9" Width="97%"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUsesInYear" runat="server" Enabled="false" Width="97%"></asp:TextBox>
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
                    <%--<tr>
                        <td colspan="2">
                            <table width="100%" class="SubFormWOBG">
                                <tr>
                                    <td  >
                                        Qty of  Treated Water Available
                                    </td>
                                    <th>
                                        <b>m&sup3/Day</b>
                                    </th>
                                    <th>
                                        <b>Annual Requirement (m&sup3/Year)</b>
                                    </th>
                                </tr>
                                <tr>
                                    <td  >
                                        a) Industrial Activity
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIndActivityPerDay" runat="server"  
                                            MaxLength="6" onblur="cal()" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Value Required"  ControlToValidate="txtIndActivityPerDay" ValidationGroup="WaterRequirementDetails"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtIndActivityPerDay" ValidationExpression="^\d{0,6}(\.\d{0,2})?$"
                                            ErrorMessage="Invalid Value" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIndActivityPerYear" runat="server" 
                                            MaxLength="6"  onblur="cal()" ></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Value Required"  ControlToValidate="txtIndActivityPerYear" ValidationGroup="WaterRequirementDetails"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtIndActivityPerYear" ValidationExpression="^\d{0,6}(\.\d{0,2})?$"
                                            ErrorMessage="Invalid Value" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="(m3/Day should be smaller than or equal to m3/Year)" ForeColor="Red" Display="Dynamic" ValidationGroup="WaterRequirementDetails" ControlToCompare="txtIndActivityPerDay" ControlToValidate="txtIndActivityPerYear" Operator="GreaterThanEqual" Type="Double"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        b) Commercial Activity
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCommercialActivityPerDay" runat="server" 
                                            MaxLength="6" onblur="cal()" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Value Required"  ControlToValidate="txtCommercialActivityPerDay" ValidationGroup="WaterRequirementDetails"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtCommercialActivityPerDay" ValidationExpression="^\d{0,6}(\.\d{0,2})?$"
                                            ErrorMessage="Invalid Value" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCommercialActivityPerYear" runat="server" 
                                            MaxLength="6" onblur="cal()" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Value Required"  ControlToValidate="txtCommercialActivityPerYear" ValidationGroup="WaterRequirementDetails"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtCommercialActivityPerYear" ValidationExpression="^\d{0,6}(\.\d{0,2})?$"
                                            ErrorMessage="Invalid Value" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="(m3/Day should be smaller than or equal to m3/Year)" ForeColor="Red" Display="Dynamic" ValidationGroup="WaterRequirementDetails" ControlToCompare="txtCommercialActivityPerDay" ControlToValidate="txtCommercialActivityPerYear" Operator="GreaterThanEqual" Type="Double"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        c) Green Belt Development
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGreenBeltDevelPerDay" runat="server" 
                                            MaxLength="6" onblur="cal()" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Value Required"  ControlToValidate="txtGreenBeltDevelPerDay" ValidationGroup="WaterRequirementDetails"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtGreenBeltDevelPerDay" ValidationExpression="^\d{0,6}(\.\d{0,2})?$"
                                            ErrorMessage="Invalid Value" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGreenBeltDevelPerYear" runat="server" 
                                            MaxLength="6" onblur="cal()" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Value Required"  ControlToValidate="txtGreenBeltDevelPerDay" ValidationGroup="WaterRequirementDetails"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtGreenBeltDevelPerYear" ValidationExpression="^\d{0,6}(\.\d{0,2})?$"
                                            ErrorMessage="Invalid Value" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="(m3/Day should be smaller than or equal to m3/Year)" ForeColor="Red" Display="Dynamic" ValidationGroup="WaterRequirementDetails" ControlToCompare="txtGreenBeltDevelPerDay" ControlToValidate="txtGreenBeltDevelPerYear" Operator="GreaterThanEqual" Type="Double"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        d) Domestic
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDomesticPerDay" runat="server" 
                                            MaxLength="6" onblur="cal()" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Value Required"  ControlToValidate="txtDomesticPerDay" ValidationGroup="WaterRequirementDetails"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtDomesticPerDay" ValidationExpression="^\d{0,6}(\.\d{0,2})?$"
                                            ErrorMessage="Invalid Value" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDomesticPerYear" runat="server" 
                                            MaxLength="6" onblur="cal()" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Value Required"  ControlToValidate="txtDomesticPerYear" ValidationGroup="WaterRequirementDetails"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" Display="Dynamic"
                                            ForeColor="Red" ControlToValidate="txtDomesticPerYear" ValidationExpression="^\d{0,6}(\.\d{0,2})?$"
                                            ErrorMessage="Invalid Value" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="(m3/Day should be smaller than or equal to m3/Year)" ForeColor="Red" Display="Dynamic" ValidationGroup="WaterRequirementDetails" ControlToCompare="txtDomesticPerDay" ControlToValidate="txtDomesticPerYear" Operator="GreaterThanEqual" Type="Double"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        e) Other Use
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUsePerDay" runat="server" 
                                            MaxLength="6" onblur="cal()" ></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Value Required"  ControlToValidate="txtOtherUsePerDay" ValidationGroup="WaterRequirementDetails"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtOtherUsePerDay" ValidationExpression="^\d{0,6}(\.\d{0,2})?$"
                                            ErrorMessage="Invalid Value" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUsePerYear" runat="server" 
                                            onblur="cal()" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Value Required"  ControlToValidate="txtOtherUsePerYear" ValidationGroup="WaterRequirementDetails"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtOtherUsePerYear" ValidationExpression="^\d{0,6}(\.\d{0,2})?$"
                                            ErrorMessage="Invalid Value" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                                        <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="(m3/Day should be smaller than or equal to m3/Year)" ForeColor="Red" Display="Dynamic" ValidationGroup="WaterRequirementDetails" ControlToCompare="txtOtherUsePerDay" ControlToValidate="txtOtherUsePerYear" Operator="GreaterThanEqual" Type="Double"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Grand Total</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalPerDay" runat="server"    Enabled="false"></asp:TextBox>
                                    </td>
                                    <td  >
                                        <asp:TextBox ID="txtTotalPerYear"   runat="server"  Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="2">
                            <table width="100%">
                                <tr>
                                    <td style="width: 30%">Net Ground Water Requirement :<span class="Coumpulsory">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNetGroundWaterRequirement" runat="server" Enabled="false"></asp:TextBox>
                                        (m<sup>3</sup>/day)
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="font-weight: bold; color: Red">( <span style="font-weight: bold; color: Red;">(3(i)(a) Ground Water Requirement Existing
                                            (m³/Day) + 3(i)(a) Ground Water Requirement Proposed (m³/Day))</span> )
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
                        <td style="text-align: center" colspan="2">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('Please Save as Draft before moving to Previous Page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" ValidationGroup="WaterRequirementDetails"
                                OnClientClick="WaterRequirementMessage()" OnClick="btnSaveAsDraft_Click" />
                            <asp:Button ID="txtNext" runat="server" Text="Next >>" ValidationGroup="WaterRequirementDetails"
                                OnClientClick="WaterRequirementMessage()" OnClick="txtNext_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
