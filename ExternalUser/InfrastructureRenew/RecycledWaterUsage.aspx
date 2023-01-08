<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    AutoEventWireup="true" CodeFile="RecycledWaterUsage.aspx.cs" Inherits="ExternalUser_InfrastructureRenew_RecycledWaterUsage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function CalMultiRecycleDayNoOfDay() {

            var txtEfflSewGeneratedETPSTPExistInDay = document.getElementById('<%=txtEfflSewGeneratedETPSTPExistInDay.ClientID%>');
            var txtEfflSewGeneratedETPSTPExistNoOfDay = document.getElementById('<%=txtEfflSewGeneratedETPSTPExistNoOfDay.ClientID%>');
            var txtEfflSewGeneratedETPSTPExistInYear = document.getElementById('<%= txtEfflSewGeneratedETPSTPExistInYear.ClientID%>');

            if (txtEfflSewGeneratedETPSTPExistNoOfDay != null && txtEfflSewGeneratedETPSTPExistNoOfDay.value != "" && txtEfflSewGeneratedETPSTPExistInDay != null && txtEfflSewGeneratedETPSTPExistInDay.value != "") {
                txtEfflSewGeneratedETPSTPExistInYear.value = Number(txtEfflSewGeneratedETPSTPExistInDay.value) * Number(txtEfflSewGeneratedETPSTPExistNoOfDay.value);

            }


            var txtEfflSewGeneratedETPSTPAdditInDay = document.getElementById('<%=txtEfflSewGeneratedETPSTPAdditInDay.ClientID%>');
            var txtEfflSewGeneratedETPSTPAdditNoOfDay = document.getElementById('<%=txtEfflSewGeneratedETPSTPAdditNoOfDay.ClientID%>');
            var txtEfflSewGeneratedETPSTPAdditInYear = document.getElementById('<%= txtEfflSewGeneratedETPSTPAdditInYear.ClientID%>');

            if (txtEfflSewGeneratedETPSTPAdditNoOfDay != null && txtEfflSewGeneratedETPSTPAdditNoOfDay.value != "" && txtEfflSewGeneratedETPSTPAdditInDay != null && txtEfflSewGeneratedETPSTPAdditInDay.value != "") {
                txtEfflSewGeneratedETPSTPAdditInYear.value = Number(txtEfflSewGeneratedETPSTPAdditInDay.value) * Number(txtEfflSewGeneratedETPSTPAdditNoOfDay.value);
            }

            var txtEfflAvailableTreatedforUsageExistInDay = document.getElementById('<%=txtEfflAvailableTreatedforUsageExistInDay.ClientID%>');
            var txtEfflAvailableTreatedforUsageExistNoOfDay = document.getElementById('<%=txtEfflAvailableTreatedforUsageExistNoOfDay.ClientID%>');
            var txtEfflAvailableTreatedforUsageExistInYear = document.getElementById('<%= txtEfflAvailableTreatedforUsageExistInYear .ClientID%>');

            if (txtEfflAvailableTreatedforUsageExistNoOfDay != null && txtEfflAvailableTreatedforUsageExistNoOfDay.value != "" && txtEfflAvailableTreatedforUsageExistInDay != null && txtEfflAvailableTreatedforUsageExistInDay.value != "") {
                txtEfflAvailableTreatedforUsageExistInYear.value = Number(txtEfflAvailableTreatedforUsageExistInDay.value) * Number(txtEfflAvailableTreatedforUsageExistNoOfDay.value);

            }

            var txtEfflAvailableTreatedforUsageAdditInDay = document.getElementById('<%=txtEfflAvailableTreatedforUsageAdditInDay.ClientID%>');
            var txtEfflAvailableTreatedforUsageAdditNoOfDay = document.getElementById('<%=txtEfflAvailableTreatedforUsageAdditNoOfDay.ClientID%>');
            var txtEfflAvailableTreatedforUsageAdditInYear = document.getElementById('<%= txtEfflAvailableTreatedforUsageAdditInYear .ClientID%>');

            if (txtEfflAvailableTreatedforUsageAdditNoOfDay != null && txtEfflAvailableTreatedforUsageAdditNoOfDay.value != "" && txtEfflAvailableTreatedforUsageAdditInDay != null && txtEfflAvailableTreatedforUsageAdditInDay.value != "") {
                txtEfflAvailableTreatedforUsageAdditInYear.value = Number(txtEfflAvailableTreatedforUsageAdditInDay.value) * Number(txtEfflAvailableTreatedforUsageAdditNoOfDay.value);

            }


            var txtEfflDischargeAfterTreatmentExistInDay = document.getElementById('<%=txtEfflDischargeAfterTreatmentExistInDay.ClientID%>');
            var txtEfflDischargeAfterTreatmentExistNoOfDay = document.getElementById('<%=txtEfflDischargeAfterTreatmentExistNoOfDay.ClientID%>');
            var txtEfflDischargeAfterTreatmentExistInYear = document.getElementById('<%= txtEfflDischargeAfterTreatmentExistInYear .ClientID%>');

            if (txtEfflDischargeAfterTreatmentExistNoOfDay != null && txtEfflDischargeAfterTreatmentExistNoOfDay.value != "" && txtEfflDischargeAfterTreatmentExistInDay != null && txtEfflDischargeAfterTreatmentExistInDay.value != "") {
                txtEfflDischargeAfterTreatmentExistInYear.value = Number(txtEfflDischargeAfterTreatmentExistInDay.value) * Number(txtEfflDischargeAfterTreatmentExistNoOfDay.value);

            }


            var txtEfflDischargeAfterTreatmentAdditInDay = document.getElementById('<%=txtEfflDischargeAfterTreatmentAdditInDay.ClientID%>');
            var txtEfflDischargeAfterTreatmentAdditNoOfDay = document.getElementById('<%=txtEfflDischargeAfterTreatmentAdditNoOfDay.ClientID%>');
            var txtEfflDischargeAfterTreatmentAdditInYear = document.getElementById('<%= txtEfflDischargeAfterTreatmentAdditInYear .ClientID%>');

            if (txtEfflDischargeAfterTreatmentAdditNoOfDay != null && txtEfflDischargeAfterTreatmentAdditNoOfDay.value != "" && txtEfflDischargeAfterTreatmentAdditInDay != null && txtEfflDischargeAfterTreatmentAdditInDay.value != "") {
                txtEfflDischargeAfterTreatmentAdditInYear.value = Number(txtEfflDischargeAfterTreatmentAdditInDay.value) * Number(txtEfflDischargeAfterTreatmentAdditNoOfDay.value);

            }

        }




        function CalSumRecycleDayYear() {

            CalMultiRecycleDayNoOfDay();

            var SumEffluentDay = 0;
            if (document.getElementById('<%=txtEfflSewGeneratedETPSTPExistInDay.ClientID%>').value != "") {
                SumEffluentDay = Number(SumEffluentDay) + Number(document.getElementById('<%=txtEfflSewGeneratedETPSTPExistInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtEfflSewGeneratedETPSTPAdditInDay.ClientID%>').value != "") {
                SumEffluentDay = Number(SumEffluentDay) + Number(document.getElementById('<%=txtEfflSewGeneratedETPSTPAdditInDay.ClientID%>').value);
            }
            document.getElementById('<%= txtEfflSewGeneratedETPSTPTotalInDay.ClientID%>').value = SumEffluentDay;


            var SumEffluentYear = 0;
            if (document.getElementById('<%=txtEfflSewGeneratedETPSTPExistInYear.ClientID%>').value != "") {
                SumEffluentYear = Number(SumEffluentYear) + Number(document.getElementById('<%=txtEfflSewGeneratedETPSTPExistInYear.ClientID%>').value);
            }
            if (document.getElementById('<%=txtEfflSewGeneratedETPSTPAdditInYear.ClientID%>').value != "") {
                SumEffluentYear = Number(SumEffluentYear) + Number(document.getElementById('<%=txtEfflSewGeneratedETPSTPAdditInYear.ClientID%>').value);
            }
            document.getElementById('<%= txtEfflSewGeneratedETPSTPTotalInYear.ClientID%>').value = SumEffluentYear;


            var SumAvailableDay = 0;
            if (document.getElementById('<%=txtEfflAvailableTreatedforUsageExistInDay.ClientID%>').value != "") {
                SumAvailableDay = Number(SumAvailableDay) + Number(document.getElementById('<%=txtEfflAvailableTreatedforUsageExistInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtEfflAvailableTreatedforUsageAdditInDay.ClientID%>').value != "") {
                SumAvailableDay = Number(SumAvailableDay) + Number(document.getElementById('<%=txtEfflAvailableTreatedforUsageAdditInDay.ClientID%>').value);
            }
            document.getElementById('<%= txtEfflAvailableTreatedforUsageTotalInDay.ClientID%>').value = SumAvailableDay;


            var SumAvailableYear = 0;
            if (document.getElementById('<%=txtEfflAvailableTreatedforUsageExistInYear.ClientID%>').value != "") {
                SumAvailableYear = Number(SumAvailableYear) + Number(document.getElementById('<%=txtEfflAvailableTreatedforUsageExistInYear.ClientID%>').value);
            }
            if (document.getElementById('<%=txtEfflAvailableTreatedforUsageAdditInYear.ClientID%>').value != "") {
                SumAvailableYear = Number(SumAvailableYear) + Number(document.getElementById('<%=txtEfflAvailableTreatedforUsageAdditInYear.ClientID%>').value);
            }
            document.getElementById('<%= txtEfflAvailableTreatedforUsageTotalInYear.ClientID%>').value = SumAvailableYear;


            var SumDischargeDay = 0;
            if (document.getElementById('<%=txtEfflDischargeAfterTreatmentExistInDay.ClientID%>').value != "") {
                SumDischargeDay = Number(SumDischargeDay) + Number(document.getElementById('<%=txtEfflDischargeAfterTreatmentExistInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtEfflDischargeAfterTreatmentAdditInDay.ClientID%>').value != "") {
                SumDischargeDay = Number(SumDischargeDay) + Number(document.getElementById('<%=txtEfflDischargeAfterTreatmentAdditInDay.ClientID%>').value);
            }
            document.getElementById('<%= txtEfflDischargeAfterTreatmentTotalInDay.ClientID%>').value = SumDischargeDay;


            var SumDischargeYear = 0;
            if (document.getElementById('<%=txtEfflDischargeAfterTreatmentExistInYear.ClientID%>').value != "") {
                SumDischargeYear = Number(SumDischargeYear) + Number(document.getElementById('<%=txtEfflDischargeAfterTreatmentExistInYear.ClientID%>').value);
            }
            if (document.getElementById('<%=txtEfflDischargeAfterTreatmentAdditInYear.ClientID%>').value != "") {
                SumDischargeYear = Number(SumDischargeYear) + Number(document.getElementById('<%=txtEfflDischargeAfterTreatmentAdditInYear.ClientID%>').value);
            }
            document.getElementById('<%= txtEfflDischargeAfterTreatmentTotalInYear.ClientID%>').value = SumDischargeYear;


        }


        function CalSumUsageActivityDayYear() {

            var SumCommercialActivity = 0;
            if (document.getElementById('<%=txtAvaCommeUseExistInDay.ClientID%>').value != "") {
                SumCommercialActivity = Number(SumCommercialActivity) + Number(document.getElementById('<%=txtAvaCommeUseExistInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaCommeUseAdditAvailInDay.ClientID%>').value != "") {
                SumCommercialActivity = Number(SumCommercialActivity) + Number(document.getElementById('<%=txtAvaCommeUseAdditAvailInDay.ClientID%>').value);
            }
            document.getElementById('<%= txtAvaCommUseTotalInDay.ClientID%>').value = SumCommercialActivity;


            var SumResidential = 0;
            if (document.getElementById('<%=txtAvaResiUseExistInDay.ClientID%>').value != "") {
                SumResidential = Number(SumResidential) + Number(document.getElementById('<%=txtAvaResiUseExistInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaResiUseAdditAvailInDay.ClientID%>').value != "") {
                SumResidential = Number(SumResidential) + Number(document.getElementById('<%=txtAvaResiUseAdditAvailInDay.ClientID%>').value);
            }
            document.getElementById('<%= txtAvaResiUseTotalInDay.ClientID%>').value = SumResidential;





            var SumDevelopment = 0;
            if (document.getElementById('<%=txtAvaGreenbeltDevelopmentExistInDay.ClientID%>').value != "") {
                SumDevelopment = Number(SumDevelopment) + Number(document.getElementById('<%=txtAvaGreenbeltDevelopmentExistInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaGreenbeltDevelopmentAdditAvailInDay.ClientID%>').value != "") {
                SumDevelopment = Number(SumDevelopment) + Number(document.getElementById('<%=txtAvaGreenbeltDevelopmentAdditAvailInDay.ClientID%>').value);
            }
            document.getElementById('<%= txtAvaGreenbeltDevelopmentTotalInDay.ClientID%>').value = SumDevelopment;


            var SumFlushReq = 0;
            if (document.getElementById('<%=txtAvaFlushReqExistInDay.ClientID%>').value != "") {
                SumFlushReq = Number(SumFlushReq) + Number(document.getElementById('<%=txtAvaFlushReqExistInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaFlushReqAdditAvailInDay.ClientID%>').value != "") {
                SumFlushReq = Number(SumFlushReq) + Number(document.getElementById('<%=txtAvaFlushReqAdditAvailInDay.ClientID%>').value);
            }
            document.getElementById('<%= txtAvaFlushReqTotalInDay.ClientID%>').value = SumFlushReq;



            var SumActivExistTotal = 0;
            if (document.getElementById('<%=txtAvaCommeUseExistInDay.ClientID%>').value != "") {
                SumActivExistTotal = Number(SumActivExistTotal) + Number(document.getElementById('<%=txtAvaCommeUseExistInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaResiUseExistInDay.ClientID%>').value != "") {
                SumActivExistTotal = Number(SumActivExistTotal) + Number(document.getElementById('<%=txtAvaResiUseExistInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaGreenbeltDevelopmentExistInDay.ClientID%>').value != "") {
                SumActivExistTotal = Number(SumActivExistTotal) + Number(document.getElementById('<%=txtAvaGreenbeltDevelopmentExistInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaFlushReqExistInDay.ClientID%>').value != "") {
                SumActivExistTotal = Number(SumActivExistTotal) + Number(document.getElementById('<%=txtAvaFlushReqExistInDay.ClientID%>').value);
            }
            document.getElementById('<%= txtRecycleUsageActivityExitTotal.ClientID%>').value = SumActivExistTotal;


            var SumActivAdditTotal = 0;
            if (document.getElementById('<%=txtAvaCommeUseAdditAvailInDay.ClientID%>').value != "") {
                SumActivAdditTotal = Number(SumActivAdditTotal) + Number(document.getElementById('<%=txtAvaCommeUseAdditAvailInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaResiUseAdditAvailInDay.ClientID%>').value != "") {
                SumActivAdditTotal = Number(SumActivAdditTotal) + Number(document.getElementById('<%=txtAvaResiUseAdditAvailInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaGreenbeltDevelopmentAdditAvailInDay.ClientID%>').value != "") {
                SumActivAdditTotal = Number(SumActivAdditTotal) + Number(document.getElementById('<%=txtAvaGreenbeltDevelopmentAdditAvailInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaFlushReqAdditAvailInDay.ClientID%>').value != "") {
                SumActivAdditTotal = Number(SumActivAdditTotal) + Number(document.getElementById('<%=txtAvaFlushReqAdditAvailInDay.ClientID%>').value);
            }
            document.getElementById('<%= txtRecycleUsageActivityAdditTotal.ClientID%>').value = SumActivAdditTotal;


            var SumActivUseAvailTotal = 0;
            if (document.getElementById('<%=txtAvaCommUseTotalInDay.ClientID%>').value != "") {
                SumActivUseAvailTotal = Number(SumActivUseAvailTotal) + Number(document.getElementById('<%=txtAvaCommUseTotalInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaResiUseTotalInDay.ClientID%>').value != "") {
                SumActivUseAvailTotal = Number(SumActivUseAvailTotal) + Number(document.getElementById('<%=txtAvaResiUseTotalInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaGreenbeltDevelopmentTotalInDay.ClientID%>').value != "") {
                SumActivUseAvailTotal = Number(SumActivUseAvailTotal) + Number(document.getElementById('<%=txtAvaGreenbeltDevelopmentTotalInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaFlushReqTotalInDay.ClientID%>').value != "") {
                SumActivUseAvailTotal = Number(SumActivUseAvailTotal) + Number(document.getElementById('<%=txtAvaFlushReqTotalInDay.ClientID%>').value);
            }
            document.getElementById('<%= txtRecycleUsageActivityTotalUseAvailTotal.ClientID%>').value = SumActivUseAvailTotal;



        }

        function WaterCheck() {

            var hidTotalRecycledWaterUsage = 0;
            var txtEfflAvailableTreatedforUsageTotalInDay = 0;
            var txtRecycleUsageActivityTotalUseAvailTotal = 0;

            if (document.getElementById('<%= hidTotalRecycledWaterUsage.ClientID %>').value != "") {
                hidTotalRecycledWaterUsage = Number(document.getElementById('<%= hidTotalRecycledWaterUsage.ClientID %>').value);
            }
            if (document.getElementById('<%= txtEfflAvailableTreatedforUsageTotalInDay.ClientID %>').value != "") {
                txtEfflAvailableTreatedforUsageTotalInDay = Number(document.getElementById('<%= txtEfflAvailableTreatedforUsageTotalInDay.ClientID %>').value);

            }
            if (document.getElementById('<%= txtRecycleUsageActivityTotalUseAvailTotal.ClientID %>').value != "") {
                txtRecycleUsageActivityTotalUseAvailTotal = Number(document.getElementById('<%= txtRecycleUsageActivityTotalUseAvailTotal.ClientID %>').value);

            }

            if (hidTotalRecycledWaterUsage != 0 && txtEfflAvailableTreatedforUsageTotalInDay != 0 && txtRecycleUsageActivityTotalUseAvailTotal != 0) {
                if ((hidTotalRecycledWaterUsage != txtEfflAvailableTreatedforUsageTotalInDay) || (hidTotalRecycledWaterUsage != txtRecycleUsageActivityTotalUseAvailTotal)) {
                    alert("Please Check Reuse water Quantity (2).(i).(d) , (2).(iii).(b) m3/day Total(Use + Availability) should be eqaul.");
                }
            }

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <asp:HiddenField ID="hidtxtGroundWaterRequirement" runat="server" Value="" />
    <asp:HiddenField ID="hidTotalRecycledWaterUsage" runat="server" Value="" />
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
                                            <li class="active">Recycled Water Usage</li>
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
                        <td>
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                RENEW - INFRASTRUCTURE USE: 2. Water Requirement Details - Recycled Water Usage
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormProjName">
                            <b>Project Name:&nbsp;
                                <asp:Label ID="lblHeadingProjName" runat="server"></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            (<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)
                            - Upload Attachments in <strong>Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>(iii) Details of Water Availability from ETP/STP for Recycle/Reuse Usage:</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" class="SubFormWOBG" width="100%">
                                <tr>
                                    <td>
                                    </td>
                                    <th colspan="3">
                                        Existing
                                    </th>
                                    <th colspan="3">
                                        Additional
                                    </th>
                                    <th colspan="2">
                                        Total
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <th>
                                        (m<sup>3</sup>/day)
                                    </th>
                                    <th>
                                        No. of Days
                                    </th>
                                    <th>
                                        (m<sup>3</sup>/year)
                                    </th>
                                    <th>
                                        (m<sup>3</sup>/day)
                                    </th>
                                    <th>
                                        No. of Days
                                    </th>
                                    <th>
                                        (m<sup>3</sup>/year)
                                    </th>
                                    <th>
                                        (m<sup>3</sup>/day)
                                    </th>
                                    <th>
                                        (m<sup>3</sup>/year)
                                    </th>
                                </tr>
                                <tr>
                                    <td style="width: 35%; padding-left: 20px">
                                        a) Effluent/Sewerage generated and treated in ETP/STP:
                                    </td>
                                    <td style="width: 10%">
                                        <asp:TextBox ID="txtEfflSewGeneratedETPSTPExistInDay" runat="server" MaxLength="9"
                                            Width="97%" onblur="CalSumRecycleDayYear();"> </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtEfflSewGeneratedETPSTPExistInDay" ValidationGroup="RecycledWaterUsage"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtEfflSewGeneratedETPSTPExistInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflSewGeneratedETPSTPExistInDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 10%;">
                                        <asp:TextBox ID="txtEfflSewGeneratedETPSTPExistNoOfDay" runat="server" Width="97%"
                                            onblur="CalSumRecycleDayYear();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtEfflSewGeneratedETPSTPExistNoOfDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflSewGeneratedETPSTPExistNoOfDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtEfflSewGeneratedETPSTPExistNoOfDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflSewGeneratedETPSTPExistNoOfDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="rvtxtEfflSewGeneratedETPSTPExistNoOfDay" runat="server" ForeColor="Red"
                                            MinimumValue="0" MaximumValue="365" ErrorMessage="Value should be between 0 and 365"
                                            ValidationGroup="RecycledWaterUsage" Display="Dynamic" ControlToValidate="txtEfflSewGeneratedETPSTPExistNoOfDay"
                                            Type="Integer"></asp:RangeValidator>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:TextBox ID="txtEfflSewGeneratedETPSTPExistInYear" runat="server" Width="97%" Enabled="false"
                                            onblur="CalSumRecycleDayYear();"></asp:TextBox>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:TextBox ID="txtEfflSewGeneratedETPSTPAdditInDay" runat="server" MaxLength="9"
                                            onblur="CalSumRecycleDayYear();" Width="97%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtEfflSewGeneratedETPSTPAdditInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflSewGeneratedETPSTPAdditInDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtEfflSewGeneratedETPSTPAdditInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflSewGeneratedETPSTPAdditInDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 10%;">
                                        <asp:TextBox ID="txtEfflSewGeneratedETPSTPAdditNoOfDay" runat="server" Width="97%"
                                            onblur="CalSumRecycleDayYear();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtEfflSewGeneratedETPSTPAdditNoOfDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflSewGeneratedETPSTPAdditNoOfDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtEfflSewGeneratedETPSTPAdditNoOfDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflSewGeneratedETPSTPAdditNoOfDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="rvtxtEfflSewGeneratedETPSTPAdditNoOfDay" runat="server" ForeColor="Red"
                                            MinimumValue="0" MaximumValue="365" ErrorMessage="Value should be between 0 and 365"
                                            ValidationGroup="RecycledWaterUsage" Display="Dynamic" ControlToValidate="txtEfflSewGeneratedETPSTPAdditNoOfDay"
                                            Type="Integer"></asp:RangeValidator>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:TextBox ID="txtEfflSewGeneratedETPSTPAdditInYear" runat="server" Width="97%" Enabled="false"
                                            onblur="CalSumRecycleDayYear();"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEfflSewGeneratedETPSTPTotalInDay" runat="server" Width="97%"
                                            Enabled="false" onblur="CalSumRecycleDayYear();"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEfflSewGeneratedETPSTPTotalInYear" runat="server" Width="97%"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        b) Available treated Effluent/Sewerage for usage:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEfflAvailableTreatedforUsageExistInDay" runat="server" MaxLength="9"
                                            Width="97%" onblur="CalSumRecycleDayYear();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtEfflAvailableTreatedforUsageExistInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflAvailableTreatedforUsageExistInDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtEfflAvailableTreatedforUsageExistInDay"
                                            runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflAvailableTreatedforUsageExistInDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 10%;">
                                        <asp:TextBox ID="txtEfflAvailableTreatedforUsageExistNoOfDay" runat="server" Width="97%"
                                            onblur="CalSumRecycleDayYear();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtEfflAvailableTreatedforUsageExistNoOfDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflAvailableTreatedforUsageExistNoOfDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtEfflAvailableTreatedforUsageExistNoOfDay"
                                            runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflAvailableTreatedforUsageExistNoOfDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="rvtxtEfflAvailableTreatedforUsageExistNoOfDay" runat="server"
                                            ForeColor="Red" MinimumValue="0" MaximumValue="365" ErrorMessage="Value should be between 0 and 365"
                                            ValidationGroup="RecycledWaterUsage" Display="Dynamic" ControlToValidate="txtEfflAvailableTreatedforUsageExistNoOfDay"
                                            Type="Integer"></asp:RangeValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEfflAvailableTreatedforUsageExistInYear" runat="server" Width="97%" 
                                            Enabled="false"
                                            onblur="CalSumRecycleDayYear();"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEfflAvailableTreatedforUsageAdditInDay" runat="server" Width="97%"
                                            onblur="CalSumRecycleDayYear();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtEfflAvailableTreatedforUsageAdditInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflAvailableTreatedforUsageAdditInDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtEfflAvailableTreatedforUsageAdditInDay"
                                            runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflAvailableTreatedforUsageAdditInDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 10%;">
                                        <asp:TextBox ID="txtEfflAvailableTreatedforUsageAdditNoOfDay" runat="server" Width="97%"
                                            onblur="CalSumRecycleDayYear();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtEfflAvailableTreatedforUsageAdditNoOfDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflAvailableTreatedforUsageAdditNoOfDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtEfflAvailableTreatedforUsageAdditNoOfDay"
                                            runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflAvailableTreatedforUsageAdditNoOfDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="evtxtEfflAvailableTreatedforUsageAdditNoOfDay" runat="server"
                                            ForeColor="Red" MinimumValue="0" MaximumValue="365" ErrorMessage="Value should be between 0 and 365"
                                            ValidationGroup="RecycledWaterUsage" Display="Dynamic" ControlToValidate="txtEfflAvailableTreatedforUsageAdditNoOfDay"
                                            Type="Integer"></asp:RangeValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEfflAvailableTreatedforUsageAdditInYear" runat="server" Width="97%" Enabled="false"
                                            onblur="CalSumRecycleDayYear();"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEfflAvailableTreatedforUsageTotalInDay" runat="server" Enabled="false"
                                            Width="97%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEfflAvailableTreatedforUsageTotalInYear" runat="server" Enabled="false"
                                            Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        c) Effluent/Sewerage discharge after treatment:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEfflDischargeAfterTreatmentExistInDay" runat="server" MaxLength="9"
                                            Width="97%" onblur="CalSumRecycleDayYear();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtEfflDischargeAfterTreatmentExistInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflDischargeAfterTreatmentExistInDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtEfflDischargeAfterTreatmentExistInDay"
                                            runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflDischargeAfterTreatmentExistInDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 10%;">
                                        <asp:TextBox ID="txtEfflDischargeAfterTreatmentExistNoOfDay" runat="server" Width="97%"
                                            onblur="CalSumRecycleDayYear();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtEfflDischargeAfterTreatmentExistNoOfDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflDischargeAfterTreatmentExistNoOfDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtEfflDischargeAfterTreatmentExistNoOfDay"
                                            runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflDischargeAfterTreatmentExistNoOfDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="rvtxtEfflDischargeAfterTreatmentExistNoOfDay" runat="server"
                                            ForeColor="Red" MinimumValue="0" MaximumValue="365" ErrorMessage="Value should be between 0 and 365"
                                            ValidationGroup="RecycledWaterUsage" Display="Dynamic" ControlToValidate="txtEfflDischargeAfterTreatmentExistNoOfDay"
                                            Type="Integer"></asp:RangeValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEfflDischargeAfterTreatmentExistInYear" runat="server" Width="97%" Enabled="false"
                                            onblur="CalSumRecycleDayYear();"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEfflDischargeAfterTreatmentAdditInDay" runat="server" Text=""
                                            Width="97%" onblur="CalSumRecycleDayYear();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtEfflDischargeAfterTreatmentAdditInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflDischargeAfterTreatmentAdditInDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtEfflDischargeAfterTreatmentAdditInDay"
                                            runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflDischargeAfterTreatmentAdditInDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 10%;">
                                        <asp:TextBox ID="txtEfflDischargeAfterTreatmentAdditNoOfDay" runat="server" Width="97%"
                                            onblur="CalSumRecycleDayYear();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtEfflDischargeAfterTreatmentAdditNoOfDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflDischargeAfterTreatmentAdditNoOfDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtEfflDischargeAfterTreatmentAdditNoOfDay"
                                            runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEfflDischargeAfterTreatmentAdditNoOfDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="rvtxtEfflDischargeAfterTreatmentAdditNoOfDay" runat="server"
                                            ForeColor="Red" MinimumValue="0" MaximumValue="365" ErrorMessage="Value should be between 0 and 365"
                                            ValidationGroup="RecycledWaterUsage" Display="Dynamic" ControlToValidate="txtEfflDischargeAfterTreatmentAdditNoOfDay"
                                            Type="Integer"></asp:RangeValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEfflDischargeAfterTreatmentAdditInYear" runat="server" Width="97%" Enabled="false"
                                            onblur="CalSumRecycleDayYear();"></asp:TextBox>
                                        
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEfflDischargeAfterTreatmentTotalInDay" runat="server" Text=""
                                            Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEfflDischargeAfterTreatmentTotalInYear" runat="server" Text=""
                                            Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Available treated effluent usage: Total quantity same as 2 i (d) and 2 iii (b) above</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" class="SubFormWOBG" width="100%">
                                <tr>
                                    <td>
                                        <b>Reuse / Recycle Usage Activity</b>
                                    </td>
                                    <th>
                                        Existing (m<sup>3</sup>/day)
                                    </th>
                                    <th>
                                        Additional A<b>vailability </b>(m<sup>3</sup>/day)
                                    </th>
                                    <th>
                                        Total Use + Availability (m<sup>3</sup>/day)
                                    </th>
                                </tr>
                                <tr>
                                    <td style="width: 35%; padding-left: 20px">
                                        Commercial Use:
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtAvaCommeUseExistInDay" runat="server" MaxLength="9" Width="97%"
                                            onblur="CalSumUsageActivityDayYear();"> </asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="rfvtxtAvaCommeUseExistInDay" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtAvaCommeUseExistInDay" ValidationGroup="RecycledWaterUsage"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtAvaCommeUseExistInDay" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtAvaCommeUseExistInDay" ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtAvaCommeUseAdditAvailInDay" runat="server" MaxLength="3" onblur="CalSumUsageActivityDayYear();"
                                            Width="97%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtAvaCommeUseAdditAvailInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtAvaCommeUseAdditAvailInDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtAvaCommeUseAdditAvailInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtAvaCommeUseAdditAvailInDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAvaCommUseTotalInDay" runat="server" Width="97%" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Residential Use:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAvaResiUseExistInDay" runat="server" MaxLength="9" onblur="CalSumUsageActivityDayYear();"
                                            Width="97%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtAvaResiUseExistInDay" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtAvaResiUseExistInDay" ValidationGroup="RecycledWaterUsage"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtAvaResiUseExistInDay" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtAvaResiUseExistInDay" ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAvaResiUseAdditAvailInDay" runat="server" Width="97%" onblur="CalSumUsageActivityDayYear();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtAvaResiUseAdditAvailInDay" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtAvaResiUseAdditAvailInDay" ValidationGroup="RecycledWaterUsage"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtAvaResiUseAdditAvailInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtAvaResiUseAdditAvailInDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAvaResiUseTotalInDay" runat="server" Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Greenbelt development:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAvaGreenbeltDevelopmentExistInDay" runat="server" MaxLength="9"
                                            Width="97%" onblur="CalSumUsageActivityDayYear();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtAvaGreenbeltDevelopmentExistInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtAvaGreenbeltDevelopmentExistInDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtAvaGreenbeltDevelopmentExistInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtAvaGreenbeltDevelopmentExistInDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAvaGreenbeltDevelopmentAdditAvailInDay" runat="server" Width="97%"
                                            onblur="CalSumUsageActivityDayYear();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtAvaGreenbeltDevelopmentAdditAvailInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtAvaGreenbeltDevelopmentAdditAvailInDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtAvaGreenbeltDevelopmentAdditAvailInDay"
                                            runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="txtAvaGreenbeltDevelopmentAdditAvailInDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAvaGreenbeltDevelopmentTotalInDay" runat="server" Enabled="false"
                                            Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Flushing Req.:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAvaFlushReqExistInDay" runat="server" MaxLength="9" Width="97%"
                                            onblur="CalSumUsageActivityDayYear();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtAvaFlushReqExistInDay" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtAvaFlushReqExistInDay" ValidationGroup="RecycledWaterUsage"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtAvaFlushReqExistInDay" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtAvaFlushReqExistInDay" ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAvaFlushReqAdditAvailInDay" runat="server" Width="97%" onblur="CalSumUsageActivityDayYear();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtAvaFlushReqAdditAvailInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtAvaFlushReqAdditAvailInDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtAvaFlushReqAdditAvailInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtAvaFlushReqAdditAvailInDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAvaFlushReqTotalInDay" runat="server" Text="" Enabled="false"
                                            Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <b>Total </b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRecycleUsageActivityExitTotal" runat="server" MaxLength="9" Enabled="false"
                                            Width="97%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRecycleUsageActivityAdditTotal" runat="server" Width="97%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRecycleUsageActivityTotalUseAvailTotal" runat="server" Text=""
                                            Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblInfrastructureApplicationCodeFrom" Visible="false" runat="server"
                                Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('Please Save as Draft before moving to Previous Page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" ValidationGroup="RecycledWaterUsage"
                                OnClick="btnSaveAsDraft_Click" />
                            <%--  OnClientClick="TreatedWaterCheck()"--%>
                            <asp:Button ID="btnNext" runat="server" Text="Next >>" ValidationGroup="RecycledWaterUsage"
                                OnClientClick="WaterCheck();" OnClick="btnNext_Click" />
                            <%--  OnClientClick="TreatedWaterCheck()"--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
