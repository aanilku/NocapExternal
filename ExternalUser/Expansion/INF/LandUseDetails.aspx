<%@ Page Title="NOCAP-Infrastructure Application-Expansion" Language="C#" MaintainScrollPositionOnPostback="true"
    MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true"
    CodeFile="LandUseDetails.aspx.cs" Inherits="ExternalUser_Expansion_INF_LandUseDetails"
    Theme="Skin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Scripts/jquery-3.6.0.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        
        function GetData() {
            var txtSourceOfAvailability = document.getElementById("<%= txtSourceOfAvailability.ClientID %>")
            var txtSourceOfAvailability_array = document.getElementById("lbltxtSourceOfAvailability").value.split(' ');
            document.getElementById('lbltxtSourceOfAvailability').value = '( ' + parseInt(txtSourceOfAvailability_array[1] - txtSourceOfAvailability.value.length) + ' Character Left )';

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
                                            <li>Water Requirement Details</li>
                                            <li>De-Watering Existing Structure</li>
                                            <li>De-Watering Proposed Structure</li>
                                            <li>Groundwater Abstraction Structure-Existing</li>
                                            <li>Groundwater Abstraction Structure-Proposed</li>
                                            <li>Breakup of Water Requirment</li>
                                            <li>Water Supply Detail</li>
                                            <li>Other Details</li>
                                        <%--    <li>Self Declaration</li>--%>
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
                                INFRASTRUCTURE EXPANSION USE: Land Use Details
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="3">
                            (<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-Upload
                            Attachments in <strong style="color: Red">Attachment</strong> Section
                        </td>
                    </tr>
                    <tr id="trTypeOfInfrastructure" runat="server" visible="false">
                        <td style="width:20px">(iv).</td>
                        <td>
                            Type of Infrastructure :
                        </td>
                        <td>
                            <asp:Label ID="lblTypeOfInfrastructure" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trLandDetails" runat="server" visible="false">
                        <td><b>(v).</b></td>
                        <td colspan="2">
                            <b>Land Use Details of the Existing / Proposed :</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="gvLandUseType" runat="server" Width="100%" AutoGenerateColumns="false"
                                DataKeyNames="LandUseTypeCode" CssClass="SubFormWOBG" ShowFooter="true" OnRowDataBound="gvLandUseType_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Land Use Details" HeaderStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLandUseDetails" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("LandUseTypeDesc"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text="Total" Width="98%"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Existing (sq meter)" HeaderStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtExisting" class="txtExist" MaxLength="12" runat="server" onblur="getSum('E')"
                                                Style="text-align: right" Width="98%"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="revtxtExisting" runat="server" Display="Dynamic"
                                                ForeColor="Red" ControlToValidate="txtExisting"></asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtExistingTotal" runat="server" Enabled="false" Style="text-align: right"
                                                Width="98%"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Proposed (sq meter)" HeaderStyle-Width="25%">
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
                                    <asp:TemplateField HeaderText="Grand Total  (sq meter)" HeaderStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lblGrandTotalRow" runat="server" Enabled="false" Style="text-align: right"
                                                Width="98%"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="lblGrandTotal" runat="server" Enabled="false" Style="text-align: right"
                                                Width="98%"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr runat="server" visible="false">
                        <td>(iv).</td>
                        <td style="width: 45%">
                            Source of Availability of Surface Water for Infrastructure Use<br />
                            (Submit 
                            Water Availability / Non Availability Certificate): <span class="Coumpulsory">*</span> (<span class="Coumpulsory">$</span>)
                        </td>
                        <td>
                            <asp:TextBox ID="txtSourceOfAvailability" runat="server" TextMode="MultiLine" Width="98%"
                                Height="50px" onkeyup="CountCharacter(this, this.form.lbltxtSourceOfAvailability, 250);"
                                onkeydown="CountCharacter(this, this.form.lbltxtSourceOfAvailability, 250);"
                                MaxLength="250"></asp:TextBox>
                            <input type="text" id="lbltxtSourceOfAvailability" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtSourceOfAvailability" size="2" maxlength="2" value="( 250 Character Left )"
                                readonly="readonly" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LandUseDetails" ControlToValidate="txtSourceOfAvailability">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtSourceOfAvailability" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtSourceOfAvailability"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtSourceOfAvailability" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="LandUseDetails" ControlToValidate="txtSourceOfAvailability"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                   <%-- <tr>    
                        <td>(vii).</td>
                        <td>
                            Average Annual Rainfall in the Area (in mm): <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAverageAnnualRainfall" runat="server" Width="200px" MaxLength="15"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LandUseDetails" ControlToValidate="txtAverageAnnualRainfall"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtAverageAnnualRainfall" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtAverageAnnualRainfall" ValidationGroup="LandUseDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>--%>
                    <tr id="trWhetherGroundWaterUtilization" runat="server" visible="false">
                        <td>(v).</td>
                        <td>
                            Whether Ground Water Utilization for: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rbtnWhetherGroundWaterUtilization" runat="server" 
                                align="left" Enabled="false"
                                AutoPostBack="true" RepeatDirection="Vertical" 
                                onselectedindexchanged="rbtnWhetherGroundWaterUtilization_SelectedIndexChanged">
                                <asp:ListItem Value="NewIndustry">New Industry</asp:ListItem>
                                <asp:ListItem Value="ExistingIndustry">Existing Industry</asp:ListItem>
                                <asp:ListItem Value="ExpansionProgramExistingIndustry">Expansion Program of Existing Industry</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                            <tr runat="server" id="RowNOCObtainedForExistIND" visible="false">
                        <td>
                        </td>
                        <td>
                            Whether NOC Obtained for Existing Usage of Groundwater :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlNOCObtainedForExistIND" runat="server" Width="200px" AutoPostBack="true" Enabled="false"
                                OnSelectedIndexChanged="ddlNOCObtainedForExistIND_SelectedIndexChanged">
                                <asp:ListItem Value="">-----Select-----</asp:ListItem>
                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                <asp:ListItem Value="N">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr runat="server" id="RowDateOfCommencement" visible="false">
                        <td>
                        </td>
                        <td>
                            Date of Commencement :
                        </td>
                        <td>
                            <asp:TextBox ID="txtDateOfCommencement" Width="200px" runat="server" Enabled="false"></asp:TextBox>
                            <br />
                        </td>
                    </tr>
                    

                    

                    <tr id="trchkGWUtilizationUse" runat="server">
                        <td>(v).</td>
                        <td>
                            Whether Ground Water Utilization for: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:checkboxlist id ="chkGWUtilizationUse" runat="server"
                                align="left" RepeatDirection="Vertical">
                                <asp:ListItem Text="Drinking and Domestic Use" Value="1" />
                                <asp:ListItem Text="Construction Activity Use" Value="2" />
                                <asp:ListItem Text="Commercial Use" Value="3"/>
                                <asp:ListItem Text="Dewatering Use" Value="4"/>                                
                            </asp:checkboxlist>                            
                          </td>
                    </tr>                    
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
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('If you have not saved data, Your data will be lost before moving to other page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" OnClick="btnSaveAsDraft_Click1"
                                ValidationGroup="LandUseDetails"/>

                            <asp:Button ID="btnNext" runat="server" Text="Next >>" OnClick="btnNext_Click" ValidationGroup="LandUseDetails" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
