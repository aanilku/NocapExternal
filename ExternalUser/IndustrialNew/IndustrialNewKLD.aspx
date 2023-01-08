<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="IndustrialNewKLD.aspx.cs" Inherits="ExternalUser_IndustrialNew_IndustrialNewKLD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/bootstrap.min.css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js"></script>
    <style type="text/css">
        /* Custom style */
        .accordion-button::after {
            background-image: url("data:image/svg+xml,%3csvg viewBox='0 0 16 16' fill='%23333' xmlns='http://www.w3.org/2000/svg'%3e%3cpath fill-rule='evenodd' d='M8 0a1 1 0 0 1 1 1v6h6a1 1 0 1 1 0 2H9v6a1 1 0 1 1-2 0V9H1a1 1 0 0 1 0-2h6V1a1 1 0 0 1 1-1z' clip-rule='evenodd'/%3e%3c/svg%3e");
            transform: scale(.7) !important;
        }

        .accordion-button:not(.collapsed)::after {
            background-image: url("data:image/svg+xml,%3csvg viewBox='0 0 16 16' fill='%23333' xmlns='http://www.w3.org/2000/svg'%3e%3cpath fill-rule='evenodd' d='M0 8a1 1 0 0 1 1-1h14a1 1 0 1 1 0 2H1a1 1 0 0 1-1-1z' clip-rule='evenodd'/%3e%3c/svg%3e");
        }
    </style>
    <script type="text/javascript" src="../../Scripts/jquery-3.6.0.min.js"></script>

    <script type="text/javascript" language="javascript">
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


        }



        $(document).ready(function () {


            switchPane();
            var indexValue = localStorage.getItem('index');
            var indexVal = parseInt(indexValue);
            // alert(indexVal + 1);
            $(`div#myAccordion .accordion-item:nth-child(${indexVal + 1})`).find(".accordion-collapse").addClass("show");
            $(`div#myAccordion .accordion-item:nth-child(${indexVal + 1})`).siblings().find(".accordion-collapse").removeClass("show");

            $(`div#myAccordion .accordion-item:nth-child(${indexVal + 1})`).find(".accordion-button").removeClass("collapsed");
            $(`div#myAccordion .accordion-item:nth-child(${indexVal + 1})`).siblings().find(".accordion-button").addClass("collapsed");
            $('#ddlCommState').change(function () {
                localStorage.setItem('select', $('#ddlCommState option:selected').attr("value"));
            });
            var select = localStorage.getItem('#ddlCommState');
            console.log('select: ', JSON.parse(select));
            var stateName = localStorage.getItem('select');
            $(`#ddlCommState option[value=${stateName}]`).attr('selected', 'selected');
            console.log(indexValue);
            console.log(stateName);

        });

        function switchPane() {
            $('.accordion-item').click(function () {
                localStorage.setItem('index', $(this).index());
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfAccordionIndex" runat="server" />
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
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
                                            <li class="active">Location Details</li>
                                            <li>Groundwater Abstraction Structure</li>
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
                        <td colspan="3" style="text-align: right">(<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-Upload
                            Attachments in <strong>Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div class="m-6">
                                <div class="accordion" id="myAccordion">
                                    <%--Location detail--%>
                                    <div class="accordion-item">
                                        <h2 class="accordion-header" id="headingOne">
                                            <button type="button" class="accordion-button"
                                                data-bs-toggle="collapse"
                                                data-bs-target="#collapseOne">
                                                1. Location details</button>
                                        </h2>
                                        <div id="collapseOne" class="accordion-collapse collapse show"
                                            data-bs-parent="#myAccordion">
                                            <div class="card-body">

                                                <table width="100%">



                                                    <tr>

                                                        <td style="text-align: right; width: 10%">Water Quality Type : <span class="Coumpulsory">*</span>
                                                        </td>
                                                        <td style="width: auto">
                                                            <asp:DropDownList ID="ddlWaterQualityType" runat="server" Width="200px">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red"
                                                                Display="Dynamic" ValidationGroup="LocationDetails" InitialValue="" ControlToValidate="ddlWaterQualityType">Required</asp:RequiredFieldValidator>
                                                        </td>


                                                        <td style="text-align: right; width: 30%">Application Type Category / Type of Application: <span class="Coumpulsory">*</span>
                                                        </td>
                                                        <td style="width: auto">
                                                            <asp:DropDownList ID="ddlApplicationTypeCategory" runat="server" Width="200px">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                                Display="Dynamic" ValidationGroup="LocationDetails" InitialValue="" ControlToValidate="ddlApplicationTypeCategory">Required</asp:RequiredFieldValidator>

                                                        </td>
                                                    </tr>

                                                    <tr>

                                                        <td>(i) Name of Industry: <span class="Coumpulsory">*</span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtNameOfIndustry" runat="server" MaxLength="100" Width="100%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                                ValidationGroup="LocationDetails" ControlToValidate="txtNameOfIndustry" Display="Dynamic">Required</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="revtxtNameOfIndustry" runat="server" ValidationGroup="LocationDetails"
                                                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtNameOfIndustry"></asp:RegularExpressionValidator>
                                                        </td>



                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">(ii) Location Details of the Industrial Unit- (Attach Approved Site Plan with Location
                              Map) <span class="Coumpulsory">*</span>

                                                        </td>
                                                    </tr>
                                                    <tr>

                                                        <td>Address Line 1: <span class="Coumpulsory">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAddressLine1" runat="server" MaxLength="100" Width="99%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ForeColor="Red"
                                                                Display="Dynamic" ValidationGroup="LocationDetails" ControlToValidate="txtAddressLine1">Required</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="revtxtAddressLine1" runat="server" Display="Dynamic"
                                                                ForeColor="Red" ControlToValidate="txtAddressLine1" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                        </td>


                                                        <td>Address Line 2:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAddressLine2" runat="server" MaxLength="100" Width="99%"></asp:TextBox><br />
                                                            <asp:RegularExpressionValidator ID="revtxtAddressLine2" runat="server" Display="Dynamic"
                                                                ForeColor="Red" ControlToValidate="txtAddressLine2" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>

                                                        <td>Address Line 3:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAddressLine3" runat="server" MaxLength="100" Width="99%"></asp:TextBox><br />
                                                            <asp:RegularExpressionValidator ID="revtxtAddressLine3" runat="server" Display="Dynamic"
                                                                ForeColor="Red" ControlToValidate="txtAddressLine3" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                        </td>


                                                        <td>State: <span class="Coumpulsory">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                                Width="200px">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue=""
                                                                Display="Dynamic" ControlToValidate="ddlState" ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>

                                                        <td>District: <span class="Coumpulsory">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                                AutoPostBack="True" Width="200px">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue=" "
                                                                Display="Dynamic" ControlToValidate="ddlDistrict" ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                                                        </td>


                                                        <td>Sub-District: <span class="Coumpulsory">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlSubDistrict" runat="server" AutoPostBack="True" Width="200px"
                                                                OnSelectedIndexChanged="ddlSubDistrict_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue=" "
                                                                Display="Dynamic" ControlToValidate="ddlSubDistrict" ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>

                                                        <td>Village / Town: <span class="Coumpulsory">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlTownOrVillage" runat="server" Width="200px" OnSelectedIndexChanged="ddlTownOrVillage_SelectedIndexChanged"
                                                                AutoPostBack="True">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" InitialValue=""
                                                                Display="Dynamic" ControlToValidate="ddlTownOrVillage" ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblVillage" runat="server" Text="Village:<span class='Coumpulsory'>*</span>"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlVillage" runat="server" Width="200px">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="reqValiVillage" runat="server" InitialValue=" " Display="Dynamic"
                                                                ControlToValidate="ddlVillage" ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <asp:Label ID="lblTown" runat="server" Text="Town: <span class='Coumpulsory'>*</span>"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlTown" runat="server" Width="200px">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="reqValiTown" runat="server" InitialValue=" " Display="Dynamic"
                                                                ControlToValidate="ddlTown" ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>

                                                        <td>Latitude <span class='Coumpulsory'>*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProLat" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtProLat" runat="server" Display="Dynamic" ControlToValidate="txtProLat"
                                                                ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="revtxtProLat" runat="server" ForeColor="Red"
                                                                Display="Dynamic" ControlToValidate="txtProLat" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                            <asp:RangeValidator ID="rvtxtProLat" runat="server" Display="Dynamic" ErrorMessage="Value should be between 8.066700 and 37.100000" ValidationGroup="LocationDetails"
                                                                Type="Double" MinimumValue="8.066700" MaximumValue="37.100000" ForeColor="Red" ControlToValidate="txtProLat"></asp:RangeValidator>
                                                        </td>


                                                        <td>Longitude <span class='Coumpulsory'>*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProLong" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtProLong" runat="server" Display="Dynamic" ControlToValidate="txtProLong"
                                                                ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="revtxtProLong" runat="server" ForeColor="Red"
                                                                Display="Dynamic" ControlToValidate="txtProLong" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                            <asp:RangeValidator ID="rvtxtProLong" runat="server" Display="Dynamic" ErrorMessage="Value should be between 68.116600 and 97.416700" ValidationGroup="LocationDetails"
                                                                Type="Double" MinimumValue="68.116600" MaximumValue="97.416700" ForeColor="Red" ControlToValidate="txtProLong"></asp:RangeValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>

                                                        <td style="width: 35%">Whether industry is MSME: <span class="Coumpulsory">*</span>(<span class="Coumpulsory">$</span>)
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ddlMSME" Width="200px">
                                                                <asp:ListItem Value="">-----Select-----</asp:ListItem>
                                                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator runat="server" ForeColor="Red"
                                                                Display="Dynamic" ValidationGroup="LocationDetails" InitialValue=""
                                                                ControlToValidate="ddlMSME">Required</asp:RequiredFieldValidator>

                                                        </td>


                                                        <td style="width: 35%">MSME Type</td>
                                                        <td>
                                                            <asp:DropDownList ID="ddMSMEType" Width="200px" runat="server">
                                                            </asp:DropDownList>

                                                        </td>
                                                    </tr>
                                                    <tr>

                                                        <td style="width: 35%">Whether the project falls in Wetland Area: <span class="Coumpulsory">*</span>(<span class="Coumpulsory">$</span>)
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ddlWetlandArea" Width="200px">
                                                                <asp:ListItem Value="">-----Select-----</asp:ListItem>
                                                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator runat="server" ForeColor="Red"
                                                                Display="Dynamic" ValidationGroup="LocationDetails" InitialValue=""
                                                                ControlToValidate="ddlWetlandArea">Required</asp:RequiredFieldValidator>

                                                        </td>


                                                        <td>Whether Ground Water Utilization for: <span class="Coumpulsory">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButtonList ID="rbtnWhetherGroundWaterUtilization" runat="server" align="left"
                                                                AutoPostBack="true" RepeatDirection="Vertical"
                                                                OnSelectedIndexChanged="rbtnWhetherGroundWaterUtilization_SelectedIndexChanged">
                                                                <asp:ListItem Value="NewIndustry">New Industry</asp:ListItem>
                                                                <asp:ListItem Value="ExistingIndustry">Existing Industry</asp:ListItem>
                                                                <asp:ListItem Value="ExpansionProgramExistingIndustry">Expansion Program of Existing Industry</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ForeColor="Red"
                                                                InitialValue="" Display="Dynamic" ValidationGroup="LocationDetails" ControlToValidate="rbtnWhetherGroundWaterUtilization">Required</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="RowNOCObtainedForExistIND" visible="false">

                                                        <td>Whether NOC Obtained for Existing Usage of Groundwater :
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlNOCObtainedForExistIND" runat="server" Width="200px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlNOCObtainedForExistIND_SelectedIndexChanged">
                                                                <asp:ListItem Value="">-----Select-----</asp:ListItem>
                                                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                <asp:ListItem Value="N">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlNOCObtainedForExistIND" runat="server" InitialValue=" "
                                                                Display="Dynamic" ControlToValidate="ddlNOCObtainedForExistIND" ForeColor="Red"
                                                                ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr runat="server" id="RowDateOfCommencement" visible="false">

                                                        <td>Date of Commencement :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDateOfCommencement" Width="200px" runat="server"></asp:TextBox>
                                                            <asp:CalendarExtender ID="txtDOB_CalendarExtender" runat="server" Enabled="True"
                                                                TargetControlID="txtDateOfCommencement" PopupButtonID="imgbtnCalendar" Format="dd/MM/yyyy">
                                                            </asp:CalendarExtender>
                                                            <asp:ImageButton ID="imgbtnCalendar" runat="server" ImageUrl="~/Images/calendar.png"
                                                                OnClick="imgbtnCalendar_Click" />
                                                            <asp:RequiredFieldValidator ID="rfvtxtDateOfCommencement" runat="server" Enabled="false"
                                                                Display="Dynamic" ControlToValidate="txtDateOfCommencement" ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                                                            <asp:CustomValidator ID="CstmVtxtDateOfCommencement" runat="server" ClientValidationFunction="ValidateDateOnClient"
                                                                OnServerValidate="ValidateDate" ControlToValidate="txtDateOfCommencement" ErrorMessage="Invalid Date."
                                                                ForeColor="Red" ValidationGroup="LocationDetails" Display="Dynamic" />
                                                            <asp:RangeValidator ID="rvtxtDateOfCommencement" runat="server" Type="Date" Display="Dynamic"
                                                                ValidationGroup="LocationDetails" ForeColor="Red" MinimumValue="01/01/1900" ControlToValidate="txtDateOfCommencement"
                                                                ErrorMessage="Date of Commencement should be grater than 01/01/1900 and less than or equal to current date."></asp:RangeValidator>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr runat="server" id="RowDateOfExpansion" visible="false">

                                                        <td>Date of Expansion :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDateOfExpansion" Width="200px" runat="server"></asp:TextBox>
                                                            <asp:CalendarExtender ID="txtDateOfExpansion_CalendarExtender" runat="server" Enabled="True"
                                                                TargetControlID="txtDateOfExpansion" PopupButtonID="imgbtnCalendar_DateOfExpansion"
                                                                Format="dd/MM/yyyy">
                                                            </asp:CalendarExtender>
                                                            <asp:ImageButton ID="imgbtnCalendar_DateOfExpansion" runat="server" ImageUrl="~/Images/calendar.png" />
                                                            <asp:RequiredFieldValidator ID="rfvtxtDateOfExpansion" runat="server" Enabled="false"
                                                                Display="Dynamic" ControlToValidate="txtDateOfExpansion" ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                                                            <asp:CustomValidator ID="CstmVtxtDateOfExpansion" runat="server" Display="Dynamic"
                                                                OnServerValidate="ValidateDate" ClientValidationFunction="ValidateDateOnClient"
                                                                ControlToValidate="txtDateOfExpansion" ErrorMessage="Invalid Date." ForeColor="Red"
                                                                ValidationGroup="LocationDetails" />
                                                            <asp:RangeValidator ID="rvtxtDateOfExpansion" runat="server" Type="Date" Display="Dynamic"
                                                                ValidationGroup="LocationDetails" ForeColor="Red" MinimumValue="01/01/1900" ControlToValidate="txtDateOfExpansion"
                                                                ErrorMessage="Date of Expansion should be grater than 01/01/1900 and less than or equal to current date."></asp:RangeValidator>
                                                            <asp:CompareValidator ID="cvtxtDateOfExpansion" runat="server" Type="Date" ControlToValidate="txtDateOfExpansion"
                                                                ControlToCompare="txtDateOfCommencement" ForeColor="Red" ErrorMessage="Date Of Expansion should be greater than Date of Commencement"
                                                                Display="Dynamic" ValidationGroup="LocationDetails" Operator="GreaterThan"></asp:CompareValidator>
                                                        </td>

                                                        <td></td>
                                                        <td></td>
                                                    </tr>

                                                </table>

                                            </div>
                                        </div>
                                    </div>
                                    <%--Communication Adrress--%>
                                    <div class="accordion-item">
                                        <h2 class="accordion-header" id="headingTwo">
                                            <button type="button" class="accordion-button collapsed"
                                                data-bs-toggle="collapse"
                                                data-bs-target="#collapseTwo">
                                                2. Communication Address</button>
                                        </h2>
                                        <div id="collapseTwo" class="accordion-collapse collapse"
                                            data-bs-parent="#myAccordion">
                                            <div class="card-body">

                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 35%">Address Line 1: <span class="Coumpulsory">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCommAddress1" runat="server" MaxLength="100" Width="100%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ForeColor="Red"
                                                                Display="Dynamic" ValidationGroup="LocationDetails" ControlToValidate="txtCommAddress1">Required</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="revtxtCommAddressLine1" runat="server" Display="Dynamic"
                                                                ForeColor="Red" ControlToValidate="txtCommAddress1" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>Address Line 2:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCommAddress2" runat="server" MaxLength="100" Width="100%"></asp:TextBox><br />
                                                            <asp:RegularExpressionValidator ID="revtxtCommAddressLine2" runat="server" Display="Dynamic"
                                                                ForeColor="Red" ControlToValidate="txtCommAddress2" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>Address Line 3:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCommAddress3" runat="server" MaxLength="100" Width="100%"></asp:TextBox><br />
                                                            <asp:RegularExpressionValidator ID="revtxtCommAddressLine3" runat="server" Display="Dynamic"
                                                                ForeColor="Red" ControlToValidate="txtCommAddress3" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>State: <span class="Coumpulsory">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlCommState" runat="server" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlCommState_SelectedIndexChanged"
                                                                Width="200px">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator runat="server" InitialValue=" "
                                                                Display="Dynamic" ControlToValidate="ddlCommState" ForeColor="Red"
                                                                ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>District: <span class="Coumpulsory">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlCommDist" runat="server"
                                                                OnSelectedIndexChanged="ddlCommDist_SelectedIndexChanged"
                                                                AutoPostBack="True" Width="200px">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator runat="server" InitialValue=" "
                                                                Display="Dynamic" ControlToValidate="ddlCommDist" ForeColor="Red"
                                                                ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>Sub-District:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlCommSubDist" runat="server" Width="200px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>Pincode: <span class="Coumpulsory">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPinCode" runat="server" MaxLength="6" Width="100%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ForeColor="Red"
                                                                Display="Dynamic" ValidationGroup="LocationDetails"
                                                                ControlToValidate="txtPinCode">Required</asp:RequiredFieldValidator><br />
                                                            <asp:RegularExpressionValidator ID="revtxtPinCode" runat="server" ForeColor="Red"
                                                                Display="Dynamic" ControlToValidate="txtPinCode" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>Phone Number with Area Code:
                                                        </td>
                                                        <td>+<asp:TextBox ID="txtCountryCode" runat="server" Width="34px" MaxLength="2" Text="91"
                                                            Enabled="false"></asp:TextBox>(ISD)
                            <asp:TextBox ID="txtStdCode" runat="server" Width="34px" MaxLength="4"></asp:TextBox>(STD)
                            <asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="10" Width="100%"></asp:TextBox><br />
                                                            <asp:RegularExpressionValidator ID="revtxtStdCode" runat="server"
                                                                ValidationGroup="LocationDetails"
                                                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtStdCode"></asp:RegularExpressionValidator>
                                                            <asp:RegularExpressionValidator ID="revtxtPhoneNumber" runat="server"
                                                                ValidationGroup="LocationDetails"
                                                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtPhoneNumber"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>Mobile Number : <span class="Coumpulsory">*</span>
                                                        </td>
                                                        <td>+<asp:TextBox ID="txtMobileCountryCode" runat="server" Width="34px" MaxLength="2"
                                                            Text="91" Enabled="false"></asp:TextBox>(ISD)
                            <asp:TextBox ID="txtMobileNumber" runat="server" MaxLength="10" Width="100%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ForeColor="Red"
                                                                Display="Dynamic" ValidationGroup="LocationDetails" ControlToValidate="txtMobileNumber"
                                                                ErrorMessage="Required"></asp:RequiredFieldValidator><br />
                                                            <asp:RegularExpressionValidator ID="revtxtMobileNumber" runat="server"
                                                                ValidationGroup="LocationDetails"
                                                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtMobileNumber"></asp:RegularExpressionValidator>
                                                        </td>

                                                        <td>Fax Number:
                                                        </td>
                                                        <td>+<asp:TextBox ID="txtCountryCodeFax" runat="server" Width="34px" MaxLength="2" Text="91"
                                                            Enabled="false"></asp:TextBox>(ISD)
                            <asp:TextBox ID="txtStdCodeFax" runat="server" Width="34px" MaxLength="4"></asp:TextBox>(STD)
                            <asp:TextBox ID="txtFaxNumber" runat="server" MaxLength="10" Width="100%"></asp:TextBox><br />
                                                            <asp:RegularExpressionValidator ID="revtxtStdCodeFax" runat="server"
                                                                ValidationGroup="LocationDetails"
                                                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtStdCodeFax"></asp:RegularExpressionValidator>
                                                            <asp:RegularExpressionValidator ID="revtxtFaxNumber" runat="server" ValidationGroup="LocationDetails"
                                                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtFaxNumber"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>E-Mail: <span class="Coumpulsory">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="100%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ForeColor="Red"
                                                                Display="Dynamic" ValidationGroup="LocationDetails" ControlToValidate="txtEmail">Required</asp:RequiredFieldValidator><br />
                                                            <asp:RegularExpressionValidator ID="revtxtEmail" ControlToValidate="txtEmail" Display="Dynamic"
                                                                ForeColor="Red" ValidationGroup="LocationDetails" runat="server" />
                                                        </td>
                                                        <td></td>
                                                        <td></td>

                                                    </tr>
                                                </table>

                                            </div>
                                        </div>
                                    </div>

                                    <%--     Water Requirement Details--%>
                                    <div class="accordion-item">
                                        <h2 class="accordion-header" id="headingThree">
                                            <button type="button" class="accordion-button collapsed" data-bs-toggle="collapse"
                                                data-bs-target="#collapseThree">
                                                3. Water Requirement Details
                                            </button>
                                        </h2>
                                        <div id="collapseThree" class="accordion-collapse collapse" data-bs-parent="#myAccordion">
                                            <div class="card-body">
                                                <table width="100%">
                                                    <tr>

                                                        <td>
                                                            <b>(i) Total Water Requirement (a+b+c+d) (m<sup>3</sup>/day)</b>
                                                        </td>
                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <table width="100%">

                                                                <tr>
                                                                    <th>&nbsp;
                                                                    </th>
                                                                    <th style="width: 20%">Existing
                                                                    </th>
                                                                    <th style="width: 20%">Proposed
                                                                    </th>
                                                                    <th style="width: 20%">Total
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>a) Ground Water Requirement (m&sup3/day): <span class="Coumpulsory">*</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtGroundWaterRequirementExist" runat="server" MaxLength="9" Width="97%"
                                                                            onblur="SumTWR();"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvtxtGroundWaterRequirementExist" runat="server" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtGroundWaterRequirementExist"
                                                                            ValidationGroup="LocationDetails"
                                                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revtxtGroundWaterRequirementExist" runat="server"
                                                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtGroundWaterRequirementExist"
                                                                            ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                                        <asp:RangeValidator ID="rvGroundWaterRequirementExist" runat="server" ControlToValidate="txtGroundWaterRequirementExist"
                                                                            ValidationGroup="LocationDetails" Type="Double" MaximumValue="100.00"
                                                                            Display="Dynamic" ForeColor="Red" ErrorMessage="Value can't be more than 100"></asp:RangeValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtGroundWaterRequirementPro" runat="server" MaxLength="9" Width="97%"
                                                                            onblur="SumTWR();"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvtxtGroundWaterRequirementPro" runat="server" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtGroundWaterRequirementPro" ValidationGroup="LocationDetails"
                                                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revtxtGroundWaterRequirementPro" runat="server"
                                                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtGroundWaterRequirementPro"
                                                                            ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                                        <asp:RangeValidator ID="rvGroundWaterRequirementPro" runat="server" ControlToValidate="txtGroundWaterRequirementPro"
                                                                            ValidationGroup="LocationDetails" Type="Double" MaximumValue="100.00"
                                                                            Display="Dynamic" ForeColor="Red" ErrorMessage="Value can't be more than 100"></asp:RangeValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtGroundWaterRequirementTotal" runat="server" Width="97%" Enabled="false"></asp:TextBox>

                                                                    </td>

                                                                </tr>
                                                                <tr>
                                                                    <td>b) Surface Water Available (Canal, River, Ponds etc.) (m&sup3/day): <span class="Coumpulsory">*</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtSurfaceWaterRequirementExist" runat="server" MaxLength="9" Width="97%"
                                                                            onblur="SumTWR();"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvtxtSurfaceWaterRequirementExist" runat="server" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtSurfaceWaterRequirementExist" ValidationGroup="LocationDetails"
                                                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revtxtSurfaceWaterRequirementExist" runat="server"
                                                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtSurfaceWaterRequirementExist"
                                                                            ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtSurfaceWaterRequirementPro" runat="server" MaxLength="9" Width="97%"
                                                                            onblur="SumTWR();"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvtxtSurfaceWaterRequirementPro" runat="server" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtSurfaceWaterRequirementPro" ValidationGroup="LocationDetails"
                                                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revtxtSurfaceWaterRequirementPro" runat="server"
                                                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtSurfaceWaterRequirementPro"
                                                                            ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
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
                                                                            Display="Dynamic" ControlToValidate="txtProposedExistingWaterSupplyExist" ValidationGroup="LocationDetails"
                                                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revtxtProposedExistingWaterSupplyExist" runat="server"
                                                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtProposedExistingWaterSupplyExist"
                                                                            ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtProposedExistingWaterSupplyPro" runat="server" MaxLength="9"
                                                                            onblur="SumTWR();" Width="97%"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvtxtProposedExistingWaterSupplyPro" runat="server" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtProposedExistingWaterSupplyPro" ValidationGroup="LocationDetails"
                                                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revtxtProposedExistingWaterSupplyPro" runat="server"
                                                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtProposedExistingWaterSupplyPro"
                                                                            ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
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
                                                                            Display="Dynamic" ControlToValidate="txtRecyWaterUsageExist" ValidationGroup="LocationDetails"
                                                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revtxtRecyWaterUsageExist" runat="server" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtRecyWaterUsageExist" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtRecyWaterUsagePro" runat="server" MaxLength="9" Width="97%" onblur="SumTWR();"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvtxtRecyWaterUsagePro" runat="server" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtRecyWaterUsagePro" ValidationGroup="LocationDetails"
                                                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revtxtRecyWaterUsagePro" runat="server" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtRecyWaterUsagePro" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
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

                                                        <td>
                                                            <b>(ii) Breakup of Water Requirement and Usage:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>

                                                        <td colspan="3">
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
                                                                        <asp:TextBox ID="txtIndActExistRequirement" runat="server" Width="95%" onblur="cal1();"
                                                                            MaxLength="9"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvtxtIndActExistRequirement" runat="server" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtIndActExistRequirement" ValidationGroup="LocationDetails"
                                                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revtxtIndActExist" runat="server" Display="Dynamic"
                                                                            ForeColor="Red" ControlToValidate="txtIndActExistRequirement" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                                        <br />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIndActProposedRequirement" runat="server" Width="95%" onblur="cal1();"
                                                                            MaxLength="9"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvtxtIndActProposedRequirement" runat="server" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtIndActProposedRequirement" ValidationGroup="LocationDetails"
                                                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revtxtIndActProposed" runat="server" Display="Dynamic"
                                                                            ForeColor="Red" ControlToValidate="txtIndActProposedRequirement" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIndActTotalRequirement" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIndActNoOfOperationalDaysInYear"
                                                                            runat="server" Width="95%" onblur="cal1();"
                                                                            MaxLength="3"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtIndActNoOfOperationalDaysInYear" ValidationGroup="LocationDetails"
                                                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revtxtIndActDaysInYear" runat="server" Display="Dynamic"
                                                                            ForeColor="Red" ControlToValidate="txtIndActNoOfOperationalDaysInYear" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ForeColor="Red" MinimumValue="0"
                                                                            MaximumValue="365" ErrorMessage="Value should be between 0 and 365" ValidationGroup="LocationDetails"
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
                                                                        <asp:TextBox ID="txtResidDomExistRequirement" runat="server" Width="95%" onblur="cal1();"
                                                                            MaxLength="9"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvtxtResidDomExistRequirement" runat="server" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtResidDomExistRequirement" ValidationGroup="LocationDetails"
                                                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revtxtResidDomExist" runat="server"
                                                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtResidDomExistRequirement"
                                                                            ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtResidDomProposedRequirement" runat="server" Width="95%" onblur="cal1();"
                                                                            MaxLength="9"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvtxtResidDomProposedRequirement" runat="server" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtResidDomProposedRequirement" ValidationGroup="LocationDetails"
                                                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revtxtResidDomProposed" runat="server"
                                                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtResidDomProposedRequirement"
                                                                            ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtResidDomTotalRequirement" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtResidDomNoOfOperationalDaysInYear" runat="server" Width="95%"
                                                                            onblur="cal1();" MaxLength="3"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtResidDomNoOfOperationalDaysInYear" ValidationGroup="LocationDetails"
                                                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revtxtResidDomDaysInYear" runat="server"
                                                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtResidDomNoOfOperationalDaysInYear"
                                                                            ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ForeColor="Red" MinimumValue="0"
                                                                            MaximumValue="365" ErrorMessage="Value should be between 0 and 365" ValidationGroup="LocationDetails"
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
                                                                            onblur="cal1();" MaxLength="9"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvtxtGreenDevelEnviMaintExistRequirement" runat="server" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtGreenDevelEnviMaintExistRequirement"
                                                                            ValidationGroup="LocationDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revtxtGreenDevelExist" runat="server"
                                                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtGreenDevelEnviMaintExistRequirement"
                                                                            ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtGreenDevelEnviMaintProposedRequirement" runat="server" Width="95%"
                                                                            onblur="cal1();" MaxLength="9"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvtxtGreenDevelEnviMaintProposedRequirement" runat="server" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtGreenDevelEnviMaintProposedRequirement"
                                                                            ValidationGroup="LocationDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revtxtGreenDevelProposed" runat="server"
                                                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtGreenDevelEnviMaintProposedRequirement"
                                                                            ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtGreenDevelEnviMaintTotalRequirement" runat="server" Width="95%"
                                                                            Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtGreenDevelEnviMaintNoOfOperationalDaysInYear" runat="server"
                                                                            onblur="cal1();" Width="95%" MaxLength="3"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtGreenDevelEnviMaintNoOfOperationalDaysInYear"
                                                                            ValidationGroup="LocationDetails" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revtxtGreenDevelDaysInYear" runat="server"
                                                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtGreenDevelEnviMaintNoOfOperationalDaysInYear"
                                                                            ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                                        <asp:RangeValidator ID="RangeValidator3" runat="server" ForeColor="Red" MinimumValue="0"
                                                                            MaximumValue="365" ErrorMessage="Value should be between 0 and 365" ValidationGroup="LocationDetails"
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
                                                                        <asp:TextBox ID="txtOtherUseExistRequirement" runat="server" Width="95%" onblur="cal1();"
                                                                            MaxLength="9"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvtxtOtherUseExistRequirement" runat="server" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtOtherUseExistRequirement" ValidationGroup="LocationDetails"
                                                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revtxtOtherUseExist" runat="server"
                                                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtOtherUseExistRequirement"
                                                                            ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOtherUseProposedRequirement" runat="server" Width="95%" onblur="cal1();" MaxLength="9"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvtxtOtherUseProposedRequirement" runat="server" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtOtherUseProposedRequirement" ValidationGroup="LocationDetails"
                                                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revtxtOtherUseProposed" runat="server"
                                                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtOtherUseProposedRequirement"
                                                                            ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOtherUseTotalRequirement" runat="server" Width="95%" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOtherUseNoOfOperationalDaysInYear" runat="server" Width="95%" MaxLength="3"
                                                                            onblur="cal1();"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Red"
                                                                            Display="Dynamic" ControlToValidate="txtOtherUseNoOfOperationalDaysInYear" ValidationGroup="LocationDetails"
                                                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revtxtOtherUseDaysInYear" runat="server"
                                                                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtOtherUseNoOfOperationalDaysInYear"
                                                                            ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                                                                        <asp:RangeValidator ID="RangeValidator4" runat="server" ForeColor="Red" MinimumValue="0"
                                                                            MaximumValue="365" ErrorMessage="Value should be between 0 and 365" ValidationGroup="LocationDetails"
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
                                                </table>


                                            </div>
                                        </div>
                                    </div>





                                </div>
                            </div>

                        </td>
                    </tr>



                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblPageTitleFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblIndustialApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">

                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" OnClick="btnSaveAsDraft_Click"
                                ValidationGroup="LocationDetails" />
                            <asp:Button ID="btnNext" runat="server" Text="Next >>" OnClick="btnNext_Click"
                                ValidationGroup="LocationDetails"
                                Style="height: 26px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

