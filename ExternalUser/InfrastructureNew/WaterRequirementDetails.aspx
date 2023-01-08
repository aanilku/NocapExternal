<%@ Page Title="NOCAP-Infrastructure Application-New" Language="C#" MaintainScrollPositionOnPostback="true"
    MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true"
    CodeFile="WaterRequirementDetails.aspx.cs" Inherits="ExternalUser_IndustrialNew_WaterRequirementDetails" %>

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
                                            <li class="active">Water Requirement Details</li>
                                            <li>De-Watering Existing Structure</li>
                                            <li>De-Watering Proposed Structure</li>
                                            <li>Groundwater Abstraction Structure-Existing</li>
                                            <li>Groundwater Abstraction Structure-Proposed</li>
                                            <li>Breakup of Water Requirment</li>
                                            <li>Water Supply Detail</li>
                                            <li>Other Details</li>
                                          <%--  <li>Self Declaration</li>--%>
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
                                INFRASTRUCTURE USE: 2. Infrastructure Unit &amp; Water Requirement Details Part-1
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="3">
                            (<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-Upload
                            Attachments in <strong style="color: Red">Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">
                            (2).
                        </td>
                        <td colspan="2">
                            <b>Total Number and Type of :</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 35%">
                            a) Dwelling Units <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDwellingUnits" runat="server" MaxLength="200" Width="99%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtDweelunit" runat="server"
                                ForeColor="Red" Display="Dynamic" ControlToValidate="txtDwellingUnits" ValidationGroup="WaterRequirementDetails"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtDwellingUnits" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtDwellingUnits" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="width: 35%">
                            Population: 
                        </td>
                        <td>
                            <asp:TextBox ID="txtDwellingPopulation" runat="server" MaxLength="20" Width="99%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFVtxtDwellingPopulation" runat="server"
                                ForeColor="Red" Display="Dynamic" ControlToValidate="txtDwellingPopulation" ValidationGroup="WaterRequirementDetails"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtDwellingPopulation" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtDwellingPopulation" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            b) Commercial Units <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCommercialUnits" runat="server" MaxLength="200" 
                                Width="99%"></asp:TextBox>    
                            <asp:RequiredFieldValidator ID="rfvtxtCommercialUnits" runat="server"
                                ForeColor="Red" Display="Dynamic" ControlToValidate="txtCommercialUnits" ValidationGroup="WaterRequirementDetails"
                                ErrorMessage="Required"></asp:RequiredFieldValidator><br />                        
                            <asp:RegularExpressionValidator ID="revtxtCommercialUnits" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtCommercialUnits" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="width: 35%">
                            Population: 
                        </td>
                        <td>
                            <asp:TextBox ID="txtCommercialPopulation" runat="server" MaxLength="20" Width="99%"></asp:TextBox> 
                            <asp:RequiredFieldValidator ID="rfvtxtCommercialPopulation" runat="server"
                                ForeColor="Red" Display="Dynamic" ControlToValidate="txtCommercialPopulation" ValidationGroup="WaterRequirementDetails"
                                ErrorMessage="Required"></asp:RequiredFieldValidator><br />                           
                            <asp:RegularExpressionValidator ID="revtxtCommercialPopulation" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtCommercialPopulation" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            c) Industrial Units <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIndustrialUnits" runat="server" MaxLength="200" 
                                Width="99%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtIndustrialUnits" runat="server"
                                ForeColor="Red" Display="Dynamic" ControlToValidate="txtIndustrialUnits" ValidationGroup="WaterRequirementDetails"
                                ErrorMessage="Required"></asp:RequiredFieldValidator><br />
                            <asp:RegularExpressionValidator ID="revtxtIndustrialUnits" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtIndustrialUnits" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            d) Others<span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOtherUnits" runat="server" MaxLength="200" Width="99%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtOtherUnits" runat="server"
                                ForeColor="Red" Display="Dynamic" ControlToValidate="txtOtherUnits" ValidationGroup="WaterRequirementDetails"
                                ErrorMessage="Required"></asp:RequiredFieldValidator><br />
                            <asp:RegularExpressionValidator ID="revtxtOtherUnits" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtOtherUnits" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            e) Populations Benefited<span class="Coumpulsory">*</span><br />(In case of Government water supply agencies)                        </td>
                        <td>
                            <asp:TextBox ID="txtPopulation" runat="server" MaxLength="20" Width="99%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtPopulation" runat="server"
                                ForeColor="Red" Display="Dynamic" ControlToValidate="txtPopulation" ValidationGroup="WaterRequirementDetails"
                                ErrorMessage="Required"></asp:RequiredFieldValidator><br />
                            <asp:RegularExpressionValidator ID="revtxtPopulation" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtPopulation" ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            (3).
                        </td>
                        <td colspan="2">
                            <b>Details of Water Requirement (Fresh/Saline/Brackish and Recycled Water Usage):</b><br />
                            <%--<span id="spanBalFlowChart" runat="server">(Please Enclose Water 
                            Balance Flow Chart of Activities and Requirement of Water at 
                            Each
                            Stage)</span>
                            Stage)(<span class="Coumpulsory">$</span>)--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            <b>3(i). Total Water Requirement (a+b+c+d) (m<sup>3</sup>/day)</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            <table cellpadding="0" cellspacing="0" class="SubFormWOBG" width="100%">
                                <tr>
                                    <th style="width: 40%">
                                    </th>
                                    <th style="width: 20%">
                                        Existing
                                    </th>
                                    <th style="width: 20%">
                                        Proposed
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
                                    <td style="width: 40%">
                                        a) Ground Water Requirement (m&sup3/day): <span class="Coumpulsory">*</span>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtGroundWaterRequirementExist" runat="server" MaxLength="9" Width="97%"
                                            onblur="SumTWR();"></asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="rfvtxtGroundWaterRequirementExist" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtGroundWaterRequirementExist" ValidationGroup="WaterRequirementDetails"
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
                                    <td>
                                        b) Surface Water Available (Canal, River, Ponds etc.) (m&sup3/day): <span class="Coumpulsory">
                                            *</span>
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
                                    <td>
                                        c) Water Supply from Any Agency (m&sup3/day): <span class="Coumpulsory">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtProposedExistingWaterSupplyExist" runat="server" MaxLength="9"
                                            onblur="SumTWR();" Width="97%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtProposedExistingWaterSupplyExist" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtProposedExistingWaterSupplyExist" ValidationGroup="WaterRequirementDetails"
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
                    <%-- <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            <b>(i) Water Requirement Details</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            (a) Ground Water Requirement (m&sup3/Day): <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGroundWaterRequirement" runat="server" MaxLength="9"  onblur="cal();"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtGroundWaterRequirement" ValidationGroup="WaterRequirementDetails"
                                ErrorMessage="Please enter Ground Water Requirement"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator60" 
                                runat="server" ForeColor="Red"
                                ControlToValidate="txtGroundWaterRequirement" 
                                ValidationExpression="^\d{0,6}(\.\d{0,2})?$" ErrorMessage="Invalid Value or Max 2 Scale"
                                ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            (b) Surface Water Available (m&sup3/Day): <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSurfaceWaterRequirement" runat="server" MaxLength="9" onblur="cal();"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtSurfaceWaterRequirement" ValidationGroup="WaterRequirementDetails"
                                ErrorMessage="Please enter Surface Water Requirement"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator59" 
                                runat="server" ForeColor="Red"
                                ControlToValidate="txtSurfaceWaterRequirement" 
                                ValidationExpression="^\d{0,6}(\.\d{0,2})?$" ErrorMessage="Invalid Value or Max 2 Scale"
                                ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            (c) Proposed/Existing Water Supply from Any Agency(m&sup3/Day): <span class="Coumpulsory">*</span>
                        </td>
                        <td style="margin-left: 40px">
                            <asp:TextBox ID="txtProposedExistingWaterSupply" runat="server" MaxLength="9"  onblur="cal();"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtProposedExistingWaterSupply" ValidationGroup="WaterRequirementDetails"
                                ErrorMessage="Please enter Proposed/Existing Water Supply"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ForeColor="Red"
                                ControlToValidate="txtProposedExistingWaterSupply" ValidationExpression="^\d{0,6}(\.\d{0,2})?$" ErrorMessage="Invalid Value or Max 2 Scale"
                                ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            Total Water Requirement (m&sup3/Day):(a+b+c)
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalWaterRequirement" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblInfrastructureApplicationCodeFrom" Visible="false" runat="server"
                                Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="3">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('Please Save as Draft before moving to Previous Page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" ValidationGroup="WaterRequirementDetails"
                                OnClick="btnSaveAsDraft_Click" />
                            <asp:Button ID="txtNext" runat="server" Text="Next >>" ValidationGroup="WaterRequirementDetails"
                                OnClick="txtNext_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
