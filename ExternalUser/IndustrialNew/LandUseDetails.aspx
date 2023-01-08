<%@ Page Title="NOCAP-Industrial Application" Language="C#" MaintainScrollPositionOnPostback="true"
    MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true"
    CodeFile="LandUseDetails.aspx.cs" Inherits="ExternalUser_IndustrialNew_LandUseDetails"
    Theme="Skin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function GetData() {
            var txtActionComment = document.getElementById("<%= txtSalientFeaturesOfInActivity.ClientID %>")
            var txtActionComment_array = document.getElementById("ActionCommentRemCount").value.split(' ');
            document.getElementById('ActionCommentRemCount').value = '( ' + parseInt(txtActionComment_array[1] - txtActionComment.value.length) + ' Character Left )';


<%--            var txtDrainageInTheArea = document.getElementById("<%= txtDrainageInTheArea.ClientID %>")
            var txtDrainageInTheArea_array = document.getElementById("lbltxtDrainageInTheArea").value.split(' ');
            document.getElementById('lbltxtDrainageInTheArea').value = '( ' + parseInt(txtDrainageInTheArea_array[1] - txtDrainageInTheArea.value.length) + ' Character Left )';

           var txtSourceOfAvailability = document.getElementById("<%= txtSourceOfAvailability.ClientID %>")
            var txtSourceOfAvailability_array = document.getElementById("lbltxtSourceOfAvailability").value.split(' ');
            document.getElementById('lbltxtSourceOfAvailability').value = '( ' + parseInt(txtSourceOfAvailability_array[1] - txtSourceOfAvailability.value.length) + ' Character Left )';


         var txtTownshipVillageWithin2kmRadius = document.getElementById("<%= txtTownshipVillageWithin2kmRadius.ClientID %>")
            var txtTownshipVillageWithin2kmRadius_array = document.getElementById("lbltxtTownshipVillageWithin2kmRadius").value.split(' ');
            document.getElementById('lbltxtTownshipVillageWithin2kmRadius').value = '( ' + parseInt(txtTownshipVillageWithin2kmRadius_array[1] - txtTownshipVillageWithin2kmRadius.value.length) + ' Character Left )';
        --%>
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
                                            <li >Water Requirement Details</li>
                                            <li >Recycled Water Usage</li>
                                            <li >Groundwater Abstraction Structure- Existing</li>
                                            <li >Groundwater Abstraction Structure- Proposed</li>
                                            <li >Other Details</li>
                                            <%--        <li>Self Declaration</li>--%>
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
                        <td colspan="2">
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                INDUSTRIAL USE: 1. General Information- Land Use Details
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="2">(<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-
                            Upload Attachments in <strong>Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">(iv) Salient Features of the Industrial Activity:<span class="Coumpulsory">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtSalientFeaturesOfInActivity" runat="server" onkeyup="CountCharacter(this, this.form.ActionCommentRemCount, 500);"
                                onkeydown="CountCharacter(this, this.form.ActionCommentRemCount, 500);" TextMode="MultiLine"
                                Height="52px" Width="99%" MaxLength="500"></asp:TextBox>
                            <input type="text" id="ActionCommentRemCount" tabindex="-1" style="border-width: 0px; width: 110px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="ActionCommentRemCount" size="2" maxlength="2" value="( 500 Character Left )"
                                readonly="readonly" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LandUseDetails" ControlToValidate="txtSalientFeaturesOfInActivity"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtSalientFeaturesOfInActivity" runat="server"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtSalientFeaturesOfInActivity"
                                ValidationGroup="LandUseDetails"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtSalientFeaturesOfInActivity" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtSalientFeaturesOfInActivity"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <%--<b>(v) Land Use Details of the Existing / Proposed Industrial Unit Premises Ownership of the Land : Enclose
                                Documents of Ownership / Lease:&nbsp; (<span class="Coumpulsory">$</span>)</b>--%>
                            <b>(v) Land Use Details of the Existing / Proposed Industrial Unit Premises Ownership of the Land :</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvLandUseType" runat="server" Width="100%" AutoGenerateColumns="false"
                                DataKeyNames="LandUseTypeCode" ShowFooter="true" CssClass="SubFormWOBG" OnRowDataBound="gvLandUseType_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Land Use Details" ItemStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLandUseDetails" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("LandUseTypeDesc"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Existing (sq meter)" ItemStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtExisting" MaxLength="12" runat="server" onblur="getSum('E')"
                                                Style="text-align: right" Width="98%"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="revtxtExisting" runat="server" Display="Dynamic"
                                                ForeColor="Red" ControlToValidate="txtExisting"></asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtExistingTotal" runat="server" Enabled="false" Style="text-align: right"
                                                Width="98%"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Proposed (sq meter)" ItemStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtProposed" MaxLength="12" runat="server" onblur="getSum('P')"
                                                Style="text-align: right" Width="98%"></asp:TextBox>
                                            <br />
                                            <asp:RegularExpressionValidator ID="revtxtProposed" runat="server" Display="Dynamic"
                                                ForeColor="Red" ControlToValidate="txtProposed"></asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtProposedTotal" runat="server" Enabled="false" Style="text-align: right"
                                                Width="98%"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Grand Total (sq meter)" ItemStyle-Width="25%">
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
                        <td style="width: 45%">
                            (vi) Drainage in the Area (River/ Nala etc) :<span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDrainageInTheArea" runat="server" Width="98%" Height="50px" TextMode="MultiLine"
                                MaxLength="250" onkeyup="CountCharacter(this, this.form.lbltxtDrainageInTheArea, 250);"
                                onkeydown="CountCharacter(this, this.form.lbltxtDrainageInTheArea, 250);" OnTextChanged="txtDrainageInTheArea_TextChanged"></asp:TextBox>
                            <input type="text" id="lbltxtDrainageInTheArea" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtDrainageInTheArea" size="2" maxlength="2" value="( 250 Character Left )"
                                readonly="readonly" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LandUseDetails" ControlToValidate="txtDrainageInTheArea">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtDrainageInTheArea" runat="server" Display="Dynamic"
                                ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtDrainageInTheArea"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtDrainageInTheArea" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtDrainageInTheArea"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            (vii) Source of Availability of Surface Water for Industrial Use<br />
                            (Submit 
                            Water Availability / Non Availability Certificate): <span class="Coumpulsory">*</span> (<span class="Coumpulsory">$</span>)
                        </td>
                        <td>
                            <asp:TextBox ID="txtSourceOfAvailability" runat="server" TextMode="MultiLine" onkeyup="CountCharacter(this, this.form.lbltxtSourceOfAvailability, 250);"
                                onkeydown="CountCharacter(this, this.form.lbltxtSourceOfAvailability, 250);"
                                Width="98%" Height="50px" MaxLength="250"></asp:TextBox><br />
                            <input type="text" id="lbltxtSourceOfAvailability" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtSourceOfAvailability" size="2" maxlength="2" value="( 250 Character Left )"
                                readonly="readonly" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LandUseDetails" ControlToValidate="txtSourceOfAvailability">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtSourceOfAvailability" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtSourceOfAvailability" ValidationGroup="LandUseDetails"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtSourceOfAvailability" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtSourceOfAvailability"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            (viii) Average Annual Rainfall in the Area (in mm): <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAverageAnnualRainfall" runat="server" Width="200px" MaxLength="15"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LandUseDetails" ControlToValidate="txtAverageAnnualRainfall">Required</asp:RequiredFieldValidator><br />
                            <asp:RegularExpressionValidator ID="revtxtAverageAnnualRainfall" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtAverageAnnualRainfall" ValidationGroup="LandUseDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            (ix) Townships / Villages (Within 2km Radius of the Industrial Unit): <span class="Coumpulsory">
                                *</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTownshipVillageWithin2kmRadius" runat="server" onkeyup="CountCharacter(this, this.form.lbltxtTownshipVillageWithin2kmRadius, 500);"
                                onkeydown="CountCharacter(this, this.form.lbltxtTownshipVillageWithin2kmRadius, 500);"
                                TextMode="MultiLine" Width="98%" Height="50px" MaxLength="500"></asp:TextBox><br />
                            <input type="text" id="lbltxtTownshipVillageWithin2kmRadius" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtTownshipVillageWithin2kmRadius" size="2" maxlength="2" value="( 500 Character Left )"
                                readonly="readonly" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LandUseDetails" ControlToValidate="txtTownshipVillageWithin2kmRadius">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtTownshipVillageWithin2kmRadius" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtTownshipVillageWithin2kmRadius"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtTownshipVillageWithin2kmRadius" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtTownshipVillageWithin2kmRadius"
                                ></asp:RegularExpressionValidator>
                        </td>
                    </tr>--%>
                    <tr runat="server" id="Tr1" visible="false">

                        <td>(vi) Whether Ground Water Utilization for: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rbtnWhetherGroundWaterUtilization" runat="server"
                                align="left" Enabled="false" AutoPostBack="true"
                                RepeatDirection="Vertical"
                                OnSelectedIndexChanged="rbtnWhetherGroundWaterUtilization_SelectedIndexChanged">
                                <asp:ListItem Value="NewIndustry">New Industry</asp:ListItem>
                                <asp:ListItem Value="ExistingIndustry">Existing Industry</asp:ListItem>
                                <asp:ListItem Value="ExpansionProgramExistingIndustry">Expansion Program of Existing Industry</asp:ListItem>
                            </asp:RadioButtonList>

                        </td>
                    </tr>

                    <tr runat="server" id="RowNOCObtainedForExistIND" visible="false">

                        <td>Whether NOC Obtained for Existing Usage of Groundwater :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlNOCObtainedForExistIND" runat="server" Width="200px" AutoPostBack="true"
                                Enabled="false" OnSelectedIndexChanged="ddlNOCObtainedForExistIND_SelectedIndexChanged">
                                <asp:ListItem Value="">-----Select-----</asp:ListItem>
                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                <asp:ListItem Value="N">No</asp:ListItem>
                            </asp:DropDownList>

                        </td>
                    </tr>
                    <tr runat="server" id="RowDateOfCommencement" visible="false">
                        <td>Date of Commencement :
                        </td>
                        <td>
                            <asp:TextBox ID="txtDateOfCommencement" Width="200px" runat="server" Enabled="false"></asp:TextBox>
                            <br />
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
                        <td style="text-align: center" colspan="2">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('If you have not saved data, Your data will be lost before moving to other page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" OnClick="btnSaveAsDraft_Click1"
                                ValidationGroup="LandUseDetails" />
                            <asp:Button ID="btnNext" runat="server" Text="Next >>" OnClick="btnNext_Click" ValidationGroup="LandUseDetails" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
