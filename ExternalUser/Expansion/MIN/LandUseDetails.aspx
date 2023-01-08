<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" MaintainScrollPositionOnPostback="true"
    AutoEventWireup="true" CodeFile="LandUseDetails.aspx.cs" Inherits="ExternalUser_Expansion_MIN_LandUseDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function GetData() {
            var txtActionComment = document.getElementById("<%= txtSalientFeaturesOfInActivity.ClientID %>")
            var txtActionComment_array = document.getElementById("ActionCommentRemCount").value.split(' ');
            document.getElementById('ActionCommentRemCount').value = '( ' + parseInt(txtActionComment_array[1] - txtActionComment.value.length) + ' Character Left )';

            var txtRemark = document.getElementById("<%= txtLandUseDetailSurrounding.ClientID %>")
            var txtRemark_array = document.getElementById("lbltxtLandUseDetailSurrounding").value.split(' ');
            document.getElementById('lbltxtLandUseDetailSurrounding').value = '( ' + parseInt(txtRemark_array[1] - txtRemark.value.length) + ' Character Left )';

           <%-- var txtTopographyAreaRegional = document.getElementById("<%= txtTopographyAreaRegional.ClientID %>")
            var txtTopographyAreaRegional_array = document.getElementById("lbltxtTopographyAreaRegional").value.split(' ');
            document.getElementById('lbltxtTopographyAreaRegional').value = '( ' + parseInt(txtTopographyAreaRegional_array[1] - txtTopographyAreaRegional.value.length) + ' Character Left )';


            var txtTopographyProjectArea = document.getElementById("<%= txtTopographyProjectArea.ClientID %>")
            var txtTopographyProjectArea_array = document.getElementById("lbltxtTopographyProjectArea").value.split(' ');
            document.getElementById('lbltxtTopographyProjectArea').value = '( ' + parseInt(txtTopographyProjectArea_array[1] - txtTopographyProjectArea.value.length) + ' Character Left )';


            var txtDrainageAreaRegional = document.getElementById("<%= txtDrainageAreaRegional.ClientID %>")
            var txtDrainageAreaRegional_array = document.getElementById("lbltxtDrainageAreaRegional").value.split(' ');
            document.getElementById('lbltxtDrainageAreaRegional').value = '( ' + parseInt(txtDrainageAreaRegional_array[1] - txtDrainageAreaRegional.value.length) + ' Character Left )';

            var txtDrainageProjectArea = document.getElementById("<%= txtDrainageProjectArea.ClientID %>")
            var txtDrainageProjectArea_array = document.getElementById("lbltxtDrainageProjectArea").value.split(' ');
            document.getElementById('lbltxtDrainageProjectArea').value = '( ' + parseInt(txtDrainageProjectArea_array[1] - txtDrainageProjectArea.value.length) + ' Character Left )';

            var txtSourceOfAvailability = document.getElementById("<%= txtSourceOfAvailability.ClientID %>")
            var txtSourceOfAvailability_array = document.getElementById("lbltxtSourceOfAvailability").value.split(' ');
            document.getElementById('lbltxtSourceOfAvailability').value = '( ' + parseInt(txtSourceOfAvailability_array[1] - txtSourceOfAvailability.value.length) + ' Character Left )';

            var txtTownshipVillageWithin10kmRadius = document.getElementById("<%= txtTownshipVillageWithin10kmRadius.ClientID %>")
            var txtTownshipVillageWithin10kmRadius_array = document.getElementById("lbltxtTownshipVillageWithin10kmRadius").value.split(' ');
            document.getElementById('lbltxtTownshipVillageWithin10kmRadius').value = '( ' + parseInt(txtTownshipVillageWithin10kmRadius_array[1] - txtTownshipVillageWithin10kmRadius.value.length) + ' Character Left )';--%>


        }
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
    </script>
    <script type="text/javascript">
        function getSum(type) {
            var gridview = document.getElementById("<%=gvLandUseType.ClientID %>");
            var sum = 0, gridName = "", txtName = "";
            var valueText = 0;
            if (type == 'E') {
                gridName = "_txtExisting_";
                txtName = "_txtExistingTotal";
            }
            else if (type == 'P') {
                gridName = "_txtProposed_";
                txtName = "_txtProposedTotal";
            }
            for (i = 0; i < gridview.rows.length - 2; i++) {
                //alert(gridview.id);
                //alert(gridName + i);
                var Value = document.getElementById(gridview.id.toString() + gridName + i).value;
                if (Value != "") {
                    sum = parseFloat(sum) + parseFloat(Value);
                }
                if (document.getElementById(gridview.id.toString() + '_txtExisting_' + i).value != "") {
                    if (document.getElementById(gridview.id.toString() + '_txtProposed_' + i).value != "") { document.getElementById(gridview.id.toString() + '_lblGrandTotalRow_' + i).value = parseFloat(document.getElementById(gridview.id.toString() + '_txtExisting_' + i).value) + parseFloat(document.getElementById(gridview.id.toString() + '_txtProposed_' + i).value); }
                    else { document.getElementById(gridview.id.toString() + '_lblGrandTotalRow_' + i).value = document.getElementById(gridview.id.toString() + '_txtExisting_' + i).value }
                }
                else { document.getElementById(gridview.id.toString() + '_lblGrandTotalRow_' + i).value = document.getElementById(gridview.id.toString() + '_txtProposed_' + i).value }
                valueText = document.getElementById(gridview.id.toString() + '_lblGrandTotalRow_' + i).value;
                if (valueText != "") {
                    document.getElementById(gridview.id.toString() + '_lblGrandTotalRow_' + i).value = parseFloat(valueText).toFixed(2);
                }
            }
            document.getElementById(gridview.id.toString() + txtName).value = sum.toFixed(2);
            if (document.getElementById(gridview.id.toString() + '_txtExistingTotal').value != "") {
                if (document.getElementById(gridview.id.toString() + '_txtProposedTotal').value != "") { valueText = parseFloat(document.getElementById(gridview.id.toString() + '_txtExistingTotal').value) + parseFloat(document.getElementById(gridview.id.toString() + '_txtProposedTotal').value) }
                else { valueText = document.getElementById(gridview.id.toString() + '_txtExistingTotal').value; }
            }
            else { valueText = document.getElementById(gridview.id.toString() + '_txtProposedTotal').value; }
            document.getElementById(gridview.id.toString() + '_lblGrandTotal').value = parseFloat(valueText).toFixed(2);
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
                                            <li class="active">Land Use Details</li>
                                            <li>Dewatering Existing Structure</li>
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
                        <td colspan="3">
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                MINING EXPANSION USE : Land Use Details
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="3">
                            (<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-
                            Upload Attachments in <strong style="color: Red">Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold; width: 20px">
                            (5).
                        </td>
                        <td colspan="2">
                            <b>Salient Features of the Activity: </b><span class="Coumpulsory">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtSalientFeaturesOfInActivity" runat="server" onkeyup="CountCharacter(this, this.form.ActionCommentRemCount, 500);"
                                onkeydown="CountCharacter(this, this.form.ActionCommentRemCount, 500);" TextMode="MultiLine"
                                Height="52px" Width="98%" MaxLength="500"></asp:TextBox>
                            <input type="text" id="ActionCommentRemCount" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="ActionCommentRemCount" size="2" maxlength="2" value="( 500 Character Left )"
                                readonly="readonly" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LandUseDetails" ControlToValidate="txtSalientFeaturesOfInActivity">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtSalientFeaturesOfInActivity" runat="server"
                                Display="Dynamic" ValidationGroup="LandUseDetails" ForeColor="Red" ControlToValidate="txtSalientFeaturesOfInActivity"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtSalientFeaturesOfInActivity" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtSalientFeaturesOfInActivity"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">
                            (6).
                        </td>
                        <td colspan="2">
                            <b>Land Use Details of the Surroundings ( km 10 Radius – Outside):</b></td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">
                        </td>
                        <td style="width: 45%">
                            Land Use Detail of the Surroundings(km 10 radius)
                        </td>
                        <td>
                            <asp:TextBox ID="txtLandUseDetailSurrounding" runat="server" TextMode="MultiLine"
                                onkeyup="CountCharacter(this, this.form.lbltxtLandUseDetailSurrounding, 500);"
                                onkeydown="CountCharacter(this, this.form.lbltxtLandUseDetailSurrounding, 500);"
                                Width="98%" Height="50px"></asp:TextBox><br />
                            <input type="text" id="lbltxtLandUseDetailSurrounding" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtLandUseDetailSurrounding" size="2" maxlength="2" value="( 500 Character Left )"
                                readonly="readonly" />
                            <asp:RegularExpressionValidator ID="revtxtLandUseDetailSurrounding" runat="server"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtLandUseDetailSurrounding"
                                ValidationGroup="LandUseDetails"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtLandUseDetailSurrounding" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtLandUseDetailSurrounding"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">
                            (7).
                        </td>
                        <td colspan="2">
                            <b>Land Use Detail of Project Area:</b></td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">
                        </td>
                        <td colspan="2">
                            <asp:GridView ID="gvLandUseType" runat="server" Width="100%" AutoGenerateColumns="false"
                                DataKeyNames="LandUseTypeCode" CssClass="SubFormWOBG" ShowFooter="true" OnRowDataBound="gvLandUseType_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Land Use Details" HeaderStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLandUseDetails" Width="98%" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("LandUseTypeDesc"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text="Total" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Existing (sq meter)" HeaderStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtExisting" MaxLength="12" runat="server" onkeyup="javascript:getSum('E');"
                                                Style="text-align: right" onblur="getSum('E')" Width="98%"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="revtxtExisting" runat="server" ValidationGroup="LandUseDetails"
                                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtExisting"></asp:RegularExpressionValidator>
                                                  <asp:RequiredFieldValidator ID="rfvtxtExisting" runat="server" ControlToValidate="txtExisting"
                                                ValidationGroup="LandUseDetails" Display="Dynamic" ForeColor="Red">Required</asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtExistingTotal" runat="server" Enabled="false" Style="text-align: right"
                                                Width="98%"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Proposed (sq meter)" HeaderStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtProposed" MaxLength="12" runat="server" onkeyup="javascript:getSum('P');"
                                                Width="98%" Style="text-align: right"></asp:TextBox>
                                            <br />
                                            <asp:RegularExpressionValidator ID="revtxtProposed" runat="server" ValidationGroup="LandUseDetails"
                                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtProposed"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvtxtProposed" runat="server" ControlToValidate="txtProposed"
                                                ValidationGroup="LandUseDetails" Display="Dynamic" ForeColor="Red">Required</asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtProposedTotal" runat="server" Enabled="false" Style="text-align: right"
                                                Width="98%"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Grand Total (sq meter)" HeaderStyle-Width="130px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lblGrandTotalRow" runat="server" Enabled="false" Width="98%" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="lblGrandTotal" runat="server" Enabled="false" Width="98%" Style="text-align: right"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                   <%-- <tr>
                        <td style="font-weight: bold">
                            (8).
                        </td>
                        <td colspan="2">
                            <b>Topography of the Area:</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">
                        </td>
                        <td>
                            a) Regional <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTopographyAreaRegional" runat="server" Width="98%" Height="50px"
                                onkeyup="CountCharacter(this, this.form.lbltxtTopographyAreaRegional, 1000);"
                                onkeydown="CountCharacter(this, this.form.lbltxtTopographyAreaRegional, 1000);"
                                TextMode="MultiLine" MaxLength="100"></asp:TextBox><br />
                            <input type="text" id="lbltxtTopographyAreaRegional" tabindex="-1" style="border-width: 0px;
                                width: 110px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtTopographyAreaRegional" size="2" maxlength="2" value="( 1000 Character Left )"
                                readonly="readonly" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LandUseDetails" ControlToValidate="txtTopographyAreaRegional">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtTopographyAreaRegional" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtTopographyAreaRegional"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtTopographyAreaRegional" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtTopographyAreaRegional"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">
                        </td>
                        <td>
                            b) Project Area <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTopographyProjectArea" runat="server" Width="98%" Height="50px"
                                onkeyup="CountCharacter(this, this.form.lbltxtTopographyProjectArea, 1000);"
                                onkeydown="CountCharacter(this, this.form.lbltxtTopographyProjectArea, 1000);"
                                TextMode="MultiLine" MaxLength="100"></asp:TextBox><br />
                            <input type="text" id="lbltxtTopographyProjectArea" tabindex="-1" style="border-width: 0px;
                                width: 110px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtTopographyProjectArea" size="2" maxlength="2" value="( 1000 Character Left )"
                                readonly="readonly" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LandUseDetails" ControlToValidate="txtTopographyProjectArea">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtTopographyProjectArea" runat="server" Display="Dynamic"
                                ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtTopographyProjectArea"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtTopographyProjectArea" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtTopographyProjectArea"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">
                            (9).
                        </td>
                        <td colspan="2">
                            <b>Drainage in the Area (River/ Nala etc):</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">
                        </td>
                        <td>
                            a) Regional <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDrainageAreaRegional" runat="server" Width="98%" Height="50px"
                                onkeyup="CountCharacter(this, this.form.lbltxtDrainageAreaRegional, 1000);" onkeydown="CountCharacter(this, this.form.lbltxtDrainageAreaRegional, 1000);"
                                TextMode="MultiLine" MaxLength="100"></asp:TextBox><br />
                            <input type="text" id="lbltxtDrainageAreaRegional" tabindex="-1" style="border-width: 0px;
                                width: 110px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtDrainageAreaRegional" size="2" maxlength="2" value="( 1000 Character Left )"
                                readonly="readonly" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LandUseDetails" ControlToValidate="txtDrainageAreaRegional">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtDrainageAreaRegional" runat="server" Display="Dynamic"
                                ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtDrainageAreaRegional"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtDrainageAreaRegional" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtDrainageAreaRegional"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">
                        </td>
                        <td>
                            b) Project Area <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDrainageProjectArea" runat="server" Width="98%" Height="50px"
                                onkeyup="CountCharacter(this, this.form.lbltxtDrainageProjectArea, 1000);" onkeydown="CountCharacter(this, this.form.lbltxtDrainageProjectArea, 1000);"
                                TextMode="MultiLine" MaxLength="100"></asp:TextBox><br />
                            <input type="text" id="lbltxtDrainageProjectArea" tabindex="-1" style="border-width: 0px;
                                width: 110px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtDrainageProjectArea" size="2" maxlength="2" value="( 1000 Character Left )"
                                readonly="readonly" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LandUseDetails" ControlToValidate="txtDrainageProjectArea">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtDrainageProjectArea" runat="server" Display="Dynamic"
                                ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtDrainageProjectArea"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtDrainageProjectArea" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtDrainageProjectArea"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">
                            (10).
                        </td>
                        <td>
                            <b>Source of Availability of Surface Water</b>– Furnish Details: <span class="Coumpulsory">
                                *</span>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtSourceOfAvailability" runat="server" TextMode="MultiLine" Width="98%"
                                Height="50px" onkeyup="CountCharacter(this, this.form.lbltxtSourceOfAvailability, 250);"
                                onkeydown="CountCharacter(this, this.form.lbltxtSourceOfAvailability, 250);"
                                MaxLength="250"></asp:TextBox><br />
                            <input type="text" id="lbltxtSourceOfAvailability" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtSourceOfAvailability" size="2" maxlength="2" value="( 250 Character Left )"
                                readonly="readonly" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LandUseDetails" ControlToValidate="txtSourceOfAvailability">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtSourceOfAvailability" runat="server" Display="Dynamic"
                                ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtSourceOfAvailability"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtSourceOfAvailability" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtSourceOfAvailability"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">
                            (11).
                        </td>
                        <td>
                            <b>Average Annual Rainfall in the Area (in mm):</b> <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAverageAnnualRainfall" runat="server" Width="200px" MaxLength="15"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LandUseDetails" ControlToValidate="txtAverageAnnualRainfall">Required</asp:RequiredFieldValidator><br />
                            <asp:RegularExpressionValidator ID="revtxtAverageAnnualRainfall" runat="server" Display="Dynamic"
                                ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtAverageAnnualRainfall"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold">
                            (12).
                        </td>
                        <td>
                            <b>Townships / Villages within 10 km radius of the Project:</b>
                            <span class="Coumpulsory">
                                *</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTownshipVillageWithin10kmRadius" runat="server" TextMode="MultiLine"
                                onkeyup="CountCharacter(this, this.form.lbltxtTownshipVillageWithin10kmRadius, 500);"
                                onkeydown="CountCharacter(this, this.form.lbltxtTownshipVillageWithin10kmRadius, 500);"
                                Width="98%" MaxLength="500" Height="50px"></asp:TextBox><br />
                            <input type="text" id="lbltxtTownshipVillageWithin10kmRadius" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtTownshipVillageWithin10kmRadius" size="2" maxlength="2" value="( 500 Character Left )"
                                readonly="readonly" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LandUseDetails" ControlToValidate="txtTownshipVillageWithin10kmRadius">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtTownshipVillageWithin10kmRadius" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtTownshipVillageWithin10kmRadius"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtTownshipVillageWithin10kmRadius"
                                runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="LandUseDetails"
                                ControlToValidate="txtTownshipVillageWithin10kmRadius"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    --%>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblMiningApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="3">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('If you have not saved data, Your data will be lost before moving to other page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" OnClick="btnSaveAsDraft_Click1"
                                ValidationGroup="LandUseDetails" />
                            <asp:Button ID="btnNext" runat="server" Text="Next >>" ValidationGroup="LandUseDetails"
                                OnClick="btnNext_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
