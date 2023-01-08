<%@ Page Title="NOCAP-Industrial Application" Language="C#" MaintainScrollPositionOnPostback="true"
    MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true"
    CodeFile="RecycledWaterUsage.aspx.cs" Inherits="ExternalUser_IndustrialNew_RecycledWaterUsage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <asp:HiddenField ID="hidtxtGroundWaterRequirement" runat="server" Value="" />
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
                                            <li class="active">Recycled Water Usage</li>
                                            <li >Groundwater Abstraction Structure- Existing</li>
                                            <li >Groundwater Abstraction Structure- Proposed</li>
                                            <li >Other Details</li>
                                      <%--      <li>Self Declaration</li>--%>
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
                                INDUSTRIAL USE: 2. Water Requirement Details - Recycled Water Usage
                            </div>
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
                            <b>(iii) Breakup of Recycled Water Usage:</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table cellpadding="0" cellspacing="0" class="SubFormWOBG" width="100%">
                                <tr>
                                    <td>
                                    </td>
                                    <th>
                                        (m<sup>3</sup>/day)
                                    </th>
                                    <th>
                                        (Days)
                                    </th>
                                    <th>
                                        (m<sup>3</sup>/year)
                                    </th>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        a) Total Waste Water Generated :
                                    </td>
                                    <td style="width: 23%">
                                        <asp:TextBox ID="txtTotalwasteWaterGeneratedInDay" runat="server" MaxLength="9" Width="97%"
                                            onblur="CalculateSumTreatedWaterUtilizedInDay()"> </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtTotalwasteWaterGeneratedInDay" ValidationGroup="RecycledWaterUsage"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtTotalwasteWaterInDay" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtTotalwasteWaterGeneratedInDay" ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 23%">
                                        <asp:TextBox ID="txtTotalwasteWaterGeneratedNoOfDay" runat="server" MaxLength="3"
                                            Width="97%" onblur="CalculateSumTreatedWaterUtilizedInDay()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtTotalwasteWaterGeneratedNoOfDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtTotalwasteWaterGeneratedNoOfDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator6" runat="server" ForeColor="Red" MinimumValue="0"
                                            MaximumValue="365" ErrorMessage="Value should be between 0 and 365" ValidationGroup="RecycledWaterUsage"
                                            Display="Dynamic" ControlToValidate="txtTotalwasteWaterGeneratedNoOfDay" Type="Integer"></asp:RangeValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalwasteWaterGeneratedInYear" runat="server" Width="97%" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        b). Quantity of Treated Water Available :
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtQuantityTreatedWaterAvailable" runat="server" MaxLength="9" Width="150px"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        i). Reuse in Industrial Activity :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQuantityReuseIndustrialActivityInDay" runat="server" MaxLength="9"
                                            Width="97%" onblur="CalculateSumTreatedWaterUtilizedInDay()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtQuantityReuseIndustrialActivityInDay"
                                            ValidationGroup="RecycledWaterUsage" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtQtyReuseIndustrialInDay" runat="server"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtQuantityReuseIndustrialActivityInDay"
                                            ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
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
                                    <td style="padding-left: 20px">
                                        ii) Reuse in Greenbelt Development :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQuantityReuseGreenBeltDevelInDay" runat="server" MaxLength="9"
                                            Width="97%" onblur="CalculateSumTreatedWaterUtilizedInDay()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtQuantityReuseGreenBeltDevelInDay" ValidationGroup="RecycledWaterUsage"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtQtyReuseGBDevInDay" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtQuantityReuseGreenBeltDevelInDay" ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
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
                                    <td style="padding-left: 20px">
                                        iii) Other Uses :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUsesInDay" runat="server" MaxLength="9" Width="97%" onblur="CalculateSumTreatedWaterUtilizedInDay()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtOtherUsesInDay" ValidationGroup="RecycledWaterUsage"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtOtherUsesInDay" runat="server" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtOtherUsesInDay" ValidationGroup="RecycledWaterUsage"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUsesNoOfDay" runat="server" MaxLength="9" Width="97%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOtherUsesInYear" runat="server" Enabled="false" Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        c). Total Treated Water Utilised :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalTreatedWaterUtilizedInDay" runat="server" Enabled="false"
                                            Width="97%"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalTreatedWaterUtilizedInYear" runat="server" Enabled="false"
                                            Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Net Ground Water Requirement :<span class="Coumpulsory">*</span><br />
                            <span style="font-weight: bold; color: Red;">(2(i)(a) Ground Water Requirement Existing
                                (m³/Day) + 2(i)(a) Ground Water Requirement Proposed (m³/Day))</span>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtNetGroundWaterRequirement" runat="server" Enabled="false"></asp:TextBox>(m<sup>3</sup>/day)
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
                                OnClientClick="TreatedWaterCheck()" OnClick="btnSaveAsDraft_Click" />
                            <asp:Button ID="btnNext" runat="server" Text="Next >>" ValidationGroup="RecycledWaterUsage"
                                OnClientClick="TreatedWaterCheck()" OnClick="btnNext_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
