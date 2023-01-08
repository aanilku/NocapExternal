<%@ Page Title="NOCAP- Industrial Application for Renewal" Language="C#" MaintainScrollPositionOnPostback="true"
    MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true"
    CodeFile="RecycledWaterUsage.aspx.cs" Inherits="ExternalUser_IndustrialRenew_RecycledWaterUsage" %>

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

            var SumIndustrialActivity = 0;
            if (document.getElementById('<%=txtAvaIndustrialActivityExistInDay.ClientID%>').value != "") {
                SumIndustrialActivity = Number(SumIndustrialActivity) + Number(document.getElementById('<%=txtAvaIndustrialActivityExistInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaIndustrialActivityAdditAvailInDay.ClientID%>').value != "") {
                SumIndustrialActivity = Number(SumIndustrialActivity) + Number(document.getElementById('<%=txtAvaIndustrialActivityAdditAvailInDay.ClientID%>').value);
            }
            document.getElementById('<%= txtAvaIndustrialActivityTotalInDay.ClientID%>').value = SumIndustrialActivity;


            var SumDomestic = 0;
            if (document.getElementById('<%=txtAvaDomesticExistInDay.ClientID%>').value != "") {
                SumDomestic = Number(SumDomestic) + Number(document.getElementById('<%=txtAvaDomesticExistInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaDomesticAdditAvailInDay.ClientID%>').value != "") {
                SumDomestic = Number(SumDomestic) + Number(document.getElementById('<%=txtAvaDomesticAdditAvailInDay.ClientID%>').value);
            }
            document.getElementById('<%= txtAvaDomesticTotalInDay.ClientID%>').value = SumDomestic;


            var SumDevelopment = 0;
            if (document.getElementById('<%=txtAvaGreenbeltDevelopmentExistInDay.ClientID%>').value != "") {
                SumDevelopment = Number(SumDevelopment) + Number(document.getElementById('<%=txtAvaGreenbeltDevelopmentExistInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaGreenbeltDevelopmentAdditAvailInDay.ClientID%>').value != "") {
                SumDevelopment = Number(SumDevelopment) + Number(document.getElementById('<%=txtAvaGreenbeltDevelopmentAdditAvailInDay.ClientID%>').value);
            }
            document.getElementById('<%= txtAvaGreenbeltDevelopmentTotalInDay.ClientID%>').value = SumDevelopment;


            var SumOtherUse = 0;
            if (document.getElementById('<%=txtAvaOtherUseExistInDay.ClientID%>').value != "") {
                SumOtherUse = Number(SumOtherUse) + Number(document.getElementById('<%=txtAvaOtherUseExistInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaOtherUseAdditAvailInDay.ClientID%>').value != "") {
                SumOtherUse = Number(SumOtherUse) + Number(document.getElementById('<%=txtAvaOtherUseAdditAvailInDay.ClientID%>').value);
            }
            document.getElementById('<%= txtAvaOtherUseTotalInDay.ClientID%>').value = SumOtherUse;



            var SumActivExistTotal = 0;
            if (document.getElementById('<%=txtAvaIndustrialActivityExistInDay.ClientID%>').value != "") {
                SumActivExistTotal = Number(SumActivExistTotal) + Number(document.getElementById('<%=txtAvaIndustrialActivityExistInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaDomesticExistInDay.ClientID%>').value != "") {
                SumActivExistTotal = Number(SumActivExistTotal) + Number(document.getElementById('<%=txtAvaDomesticExistInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaGreenbeltDevelopmentExistInDay.ClientID%>').value != "") {
                SumActivExistTotal = Number(SumActivExistTotal) + Number(document.getElementById('<%=txtAvaGreenbeltDevelopmentExistInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaOtherUseExistInDay.ClientID%>').value != "") {
                SumActivExistTotal = Number(SumActivExistTotal) + Number(document.getElementById('<%=txtAvaOtherUseExistInDay.ClientID%>').value);
            }
            document.getElementById('<%= txtRecycleUsageActivityExitTotal.ClientID%>').value = SumActivExistTotal;


            var SumActivAdditTotal = 0;
            if (document.getElementById('<%=txtAvaIndustrialActivityAdditAvailInDay.ClientID%>').value != "") {
                SumActivAdditTotal = Number(SumActivAdditTotal) + Number(document.getElementById('<%=txtAvaIndustrialActivityAdditAvailInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaDomesticAdditAvailInDay.ClientID%>').value != "") {
                SumActivAdditTotal = Number(SumActivAdditTotal) + Number(document.getElementById('<%=txtAvaDomesticAdditAvailInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaGreenbeltDevelopmentAdditAvailInDay.ClientID%>').value != "") {
                SumActivAdditTotal = Number(SumActivAdditTotal) + Number(document.getElementById('<%=txtAvaGreenbeltDevelopmentAdditAvailInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaOtherUseAdditAvailInDay.ClientID%>').value != "") {
                SumActivAdditTotal = Number(SumActivAdditTotal) + Number(document.getElementById('<%=txtAvaOtherUseAdditAvailInDay.ClientID%>').value);
            }
            document.getElementById('<%= txtRecycleUsageActivityAdditTotal.ClientID%>').value = SumActivAdditTotal;


            var SumActivUseAvailTotal = 0;
            if (document.getElementById('<%=txtAvaIndustrialActivityTotalInDay.ClientID%>').value != "") {
                SumActivUseAvailTotal = Number(SumActivUseAvailTotal) + Number(document.getElementById('<%=txtAvaIndustrialActivityTotalInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaDomesticTotalInDay.ClientID%>').value != "") {
                SumActivUseAvailTotal = Number(SumActivUseAvailTotal) + Number(document.getElementById('<%=txtAvaDomesticTotalInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaGreenbeltDevelopmentTotalInDay.ClientID%>').value != "") {
                SumActivUseAvailTotal = Number(SumActivUseAvailTotal) + Number(document.getElementById('<%=txtAvaGreenbeltDevelopmentTotalInDay.ClientID%>').value);
            }
            if (document.getElementById('<%=txtAvaOtherUseTotalInDay.ClientID%>').value != "") {
                SumActivUseAvailTotal = Number(SumActivUseAvailTotal) + Number(document.getElementById('<%=txtAvaOtherUseTotalInDay.ClientID%>').value);
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
    <%--  <script type="text/javascript">
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

        function TreatedWaterCheck() {
            var txtQuantityTreatedWaterAvailable = "0", txtTotalTreatedWaterUtilizedInDay = "0";
            if (document.getElementById('<%= txtQuantityTreatedWaterAvailable.ClientID %>').value != "") {
                txtQuantityTreatedWaterAvailable = Number(txtQuantityTreatedWaterAvailable) + Number(document.getElementById('<%= txtQuantityTreatedWaterAvailable.ClientID %>').value);
            }
            if (document.getElementById('<%= txtTotalTreatedWaterUtilizedInDay.ClientID %>').value != "") {
                txtTotalTreatedWaterUtilizedInDay = Number(txtTotalTreatedWaterUtilizedInDay) + Number(document.getElementById('<%= txtTotalTreatedWaterUtilizedInDay.ClientID %>').value);
            }
            if (txtQuantityTreatedWaterAvailable != "0" && txtTotalTreatedWaterUtilizedInDay != "0") {
                if (txtQuantityTreatedWaterAvailable != txtTotalTreatedWaterUtilizedInDay) {
                    alert("Please Check Reuse water Quantity (i,ii, III) above ; Total should be eqaul to 'Quantity of Treated Water utilised Section 2(i)-d'.");
                }
            }

        }


        function CalculateSumTreatedWaterUtilizedInYear() {
            var Val1; var Val2; var Val3; var Total;

            if (document.getElementById('<%=txtQuantityReuseIndustrialActivityInYear.ClientID%>').value.length <= 0) {
                Val1 = 0;
            }
            else {
                if (isNaN(document.getElementById('<%=txtQuantityReuseIndustrialActivityInYear.ClientID%>').value)) {
                    Val1 = 0;
                }
                else {
                    Val1 = parseFloat(document.getElementById('<%=txtQuantityReuseIndustrialActivityInYear.ClientID%>').value);
                }
            }
            if (document.getElementById('<%=txtQuantityReuseGreenBeltDevelInYear.ClientID%>').value.length <= 0) {
                Val2 = 0;
            }
            else {
                if (isNaN(document.getElementById('<%=txtQuantityReuseGreenBeltDevelInYear.ClientID%>').value)) {
                    Val2 = 0;
                }
                else {
                    Val2 = parseFloat(document.getElementById('<%=txtQuantityReuseGreenBeltDevelInYear.ClientID %>').value);
                }
            }
            if (document.getElementById('<%=txtOtherUsesInYear.ClientID%>').value.length <= 0) {
                Val3 = 0;
            }
            else {
                if (isNaN(document.getElementById('<%=txtOtherUsesInYear.ClientID%>').value)) {
                    Val3 = 0;
                }
                else {
                    Val3 = parseFloat(document.getElementById('<%=txtOtherUsesInYear.ClientID %>').value);
                }
            }

            Total = Val1 + Val2 + Val3;

            document.getElementById('<%=txtTotalTreatedWaterUtilizedInYear.ClientID %>').value = Total.toFixed(2);
        }
    </script>--%>
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
                                            <li >Groundwater Abstraction Structure- Existing</li>
                                            <li >Groundwater Abstraction Structure- Additional</li>
                                            <li >Compliance Conditions in the NOC</li>
                                            <li >Compliance Conditions in the NOC - Other</li>
                                            <li >Other Details</li>
                                           <%-- <li class="visited">Self Declaration</li>--%>
                                            <li >Attachment</li>                                            
                                            <li >Ready To Submit</li>
                                            <li >Final Submit</li>
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
                                RENEW - INDUSTRIAL USE: 2. Water Requirement Details - Recycled Water Usage
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
                        <td style="text-align: right" colspan="4">
                            (<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)
                            - Upload Attachments in <strong>Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <b>(iii) Details of Water Availability from ETP/STP for Recycle/Reuse Usage:</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
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
                                        No Of Day
                                    </th>
                                    <th>
                                        (m<sup>3</sup>/year)
                                    </th>
                                    <th>
                                        (m<sup>3</sup>/day)
                                    </th>
                                    <th>
                                        No Of Day
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
                                    <td style="width: 30%; padding-left: 20px">
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
                                        <asp:TextBox ID="txtEfflSewGeneratedETPSTPExistInYear" runat="server" Width="97%"
                                            Enabled="false" onblur="CalSumRecycleDayYear();"></asp:TextBox>
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
                                        <asp:TextBox ID="txtEfflSewGeneratedETPSTPAdditInYear" runat="server" Width="97%"
                                            Enabled="false" onblur="CalSumRecycleDayYear();"></asp:TextBox>
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
                                            Enabled="false" onblur="CalSumRecycleDayYear();"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEfflAvailableTreatedforUsageAdditInDay" MaxLength="9" runat="server"
                                            Width="97%" onblur="CalSumRecycleDayYear();"></asp:TextBox>
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
                                        <asp:TextBox ID="txtEfflAvailableTreatedforUsageAdditInYear" runat="server" Width="97%"
                                            Enabled="false" onblur="CalSumRecycleDayYear();"></asp:TextBox>
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
                                        <asp:TextBox ID="txtEfflDischargeAfterTreatmentExistInYear" runat="server" Width="97%"
                                            Enabled="false" onblur="CalSumRecycleDayYear();"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEfflDischargeAfterTreatmentAdditInDay" runat="server" Text=""
                                            MaxLength="9" Width="97%" onblur="CalSumRecycleDayYear();"></asp:TextBox>
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
                                        <asp:TextBox ID="txtEfflDischargeAfterTreatmentAdditInYear" runat="server" Width="97%"
                                            Enabled="false" onblur="CalSumRecycleDayYear();"></asp:TextBox>
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
                        <td colspan="4">
                            <b>Available treated effluent usage: Total quantity same as 2 i (d) and 2 iii (b) above</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
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
                                        Industrial Activity:
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtAvaIndustrialActivityExistInDay" runat="server" MaxLength="9"
                                            Width="97%" onblur="CalSumUsageActivityDayYear();"> </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtAvaIndustrialActivityExistInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtAvaIndustrialActivityExistInDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtAvaIndustrialActivityExistInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtAvaIndustrialActivityExistInDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtAvaIndustrialActivityAdditAvailInDay" runat="server" MaxLength="9"
                                            onblur="CalSumUsageActivityDayYear();" Width="97%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtAvaIndustrialActivityAdditAvailInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtAvaIndustrialActivityAdditAvailInDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtAvaIndustrialActivityAdditAvailInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtAvaIndustrialActivityAdditAvailInDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAvaIndustrialActivityTotalInDay" runat="server" Width="97%" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Domestic:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAvaDomesticExistInDay" runat="server" MaxLength="9" onblur="CalSumUsageActivityDayYear();"
                                            Width="97%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtAvaDomesticExistInDay" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtAvaDomesticExistInDay" ValidationGroup="RecycledWaterUsage"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtAvaDomesticExistInDay" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtAvaDomesticExistInDay" ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAvaDomesticAdditAvailInDay" runat="server" Width="97%" MaxLength="9"
                                            onblur="CalSumUsageActivityDayYear();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtAvaDomesticAdditAvailInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtAvaDomesticAdditAvailInDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtAvaDomesticAdditAvailInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtAvaDomesticAdditAvailInDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAvaDomesticTotalInDay" runat="server" Enabled="false" Width="97%"></asp:TextBox>
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
                                        <asp:TextBox ID="txtAvaGreenbeltDevelopmentAdditAvailInDay" MaxLength="9" runat="server"
                                            Width="97%" onblur="CalSumUsageActivityDayYear();"></asp:TextBox>
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
                                        Other use:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAvaOtherUseExistInDay" runat="server" MaxLength="9" Width="97%"
                                            onblur="CalSumUsageActivityDayYear();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtAvaOtherUseExistInDay" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtAvaOtherUseExistInDay" ValidationGroup="RecycledWaterUsage"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtAvaOtherUseExistInDay" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtAvaOtherUseExistInDay" ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAvaOtherUseAdditAvailInDay" MaxLength="9" runat="server" Width="97%"
                                            onblur="CalSumUsageActivityDayYear();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtAvaOtherUseAdditAvailInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtAvaOtherUseAdditAvailInDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtAvaOtherUseAdditAvailInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtAvaOtherUseAdditAvailInDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAvaOtherUseTotalInDay" runat="server" Text="" Enabled="false"
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
                        <td colspan="4">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblIndustialApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
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
