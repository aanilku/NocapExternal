<%@ Page Title="NOCAP-Infrastructure Application-New" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="De-WateringProposedStructure.aspx.cs"
    Inherits="ExternalUser_InfrastructureNew_De_WateringProposedStructure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
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
                                            <li class="visited">Water Requirement Details</li>
                                            <li class="visited">De-Watering Existing Structure</li>
                                            <li class="active">De-Watering Proposed Structure</li>
                                            <li>Groundwater Abstraction Structure-Existing</li>
                                            <li>Groundwater Abstraction Structure-Proposed</li>
                                            <li>Breakup of Water Requirment</li>
                                            <li>Water Supply Detail</li>
                                            <%--<li>Groundwater Abstraction Structure-Existing</li>
                                            <li>Groundwater Abstraction Structure-Proposed</li>--%>
                                            <li>Other Details</li>
                                         <%--   <li>Self Declaration</li>--%>
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
                <div>
                    <table class="SubFormWOBG" width="100%" style="line-height: 25px">
                        <tr>
                            <td colspan="4">
                                <div class="div_IndAppHeading" style="padding-left: 10px">
                                    INFRASTRUCTURE USE: 2. Infrastructure Unit De-Watering Proposed Structure Detail</div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" colspan="4">
                                (<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-Upload
                                Attachments in <strong style="color: Red">Attachment</strong> Section
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <b>(f) Particulars (i-a) is yes (Fill details of Dewatering Proposed Structures)</b>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                                Number of Proposed Structures: <span class="Coumpulsory">*</span>
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtNoOfProposedStructure" runat="server" MaxLength="3" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtNoOfProposedStructure"
                                    Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="deWatringRequirementDetails"></asp:RequiredFieldValidator><br />
                                <asp:RegularExpressionValidator ID="revtxtNoOfProposedStructure" runat="server" ForeColor="Red"
                                    ControlToValidate="txtNoOfProposedStructure" Display="Dynamic" ValidationGroup="deWatringRequirementDetails"></asp:RegularExpressionValidator>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" Type="Integer" ControlToValidate="txtNoOfProposedStructure"
                                    Display="Dynamic" ForeColor="Red" ValidationGroup="deWatringRequirementDetails"
                                    ErrorMessage="Value Range 0 to 1000" MaximumValue="1000" MinimumValue="0"></asp:RangeValidator>
                            </td>
                            <td style="width: 25%">Type of Structure: <span class="Coumpulsory">*</span>
                            </td>
                            <td style="width: 25%">
                                <asp:DropDownList ID="ddlTypeOfStructure" runat="server" Width="200px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                    Display="Dynamic" ValidationGroup="AddDeWatringStructures" InitialValue=" " ControlToValidate="ddlTypeOfStructure">Required</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <%--<fieldset>
                                    <legend><b>Details:</b></legend>--%>
                                    <table class="SubFormWOBG" width="100%" style="line-height: 25px">
                                        <%--<tr>
                                            <td colspan="2" style="width:40%">
                                                Type of Structure: <span class="Coumpulsory">*</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTypeOfStructure" runat="server" Width="200px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                    Display="Dynamic" ValidationGroup="AddDeWatringStructures" InitialValue=" " ControlToValidate="ddlTypeOfStructure">Required</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>--%>
                                        <tr runat="server" visible="false">
                                            <td colspan="2" style="width:25%">
                                                Year of Construction:
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtYearOfConstruction" runat="server" MaxLength="4" Width="200px"></asp:TextBox><br />
                                                <asp:RangeValidator ID="RngValYearOfConstruction" runat="server" ControlToValidate="txtYearOfConstruction"
                                                    Display="Dynamic" Type="Integer" MinimumValue="1900" MaximumValue="3000" ForeColor="Red"
                                                    ErrorMessage="Invalid Year" ValidationGroup="AddDeWatringStructures"></asp:RangeValidator>
                                                <asp:RegularExpressionValidator ID="revtxtYearOfConstruction" runat="server" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="txtYearOfConstruction" ValidationGroup="AddDeWatringStructures"></asp:RegularExpressionValidator>
                                            </td>
                                           
                                        </tr>
                                        <tr runat="server" visible="false">
                                            <td colspan="2">
                                                Depth (Meter):
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtDepthMeter" runat="server" MaxLength="7" Width="200px"></asp:TextBox><br />
                                                <asp:RangeValidator ID="RangeValidator2" runat="server" Type="Double" ControlToValidate="txtDepthMeter"
                                                    Display="Dynamic" ForeColor="Red" ValidationGroup="AddDeWatringStructures" ErrorMessage="Value Range 0.01 to 1000"
                                                    MaximumValue="1000.00" MinimumValue="0.01"></asp:RangeValidator>
                                                <asp:RegularExpressionValidator ID="revtxtDepthMeter" runat="server" Display="Dynamic"
                                                    ForeColor="Red" ControlToValidate="txtDepthMeter" ValidationGroup="AddDeWatringStructures"></asp:RegularExpressionValidator>
                                            </td>
                                            
                                        </tr>
                                        <tr runat="server" visible="false">
                                            <td colspan="2">
                                                Diameter (mm):
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtDiameterMM" runat="server" MaxLength="6" Width="200px"></asp:TextBox><br />
                                                <asp:RegularExpressionValidator ID="revtxtDiameterMM" runat="server" Display="Dynamic"
                                                    ForeColor="Red" ControlToValidate="txtDiameterMM" ValidationGroup="AddDeWatringStructures"></asp:RegularExpressionValidator>
                                                <asp:RangeValidator ID="RangeValidator3" runat="server" Display="Dynamic" Type="Integer"
                                                    ControlToValidate="txtDiameterMM" ForeColor="Red" ValidationGroup="AddDeWatringStructures"
                                                    ErrorMessage="Value can't be more than 999999" MaximumValue="999999" MinimumValue="0"></asp:RangeValidator>
                                            </td>
                                            
                                        </tr>
                                        <tr id="trDepthWaterBelowGroundLevel" runat="server" visible="false">
                                            <td colspan="2">
                                                Depth to Water Level (Meters below Ground Level):
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtDepthWaterBelowGroundLevel" runat="server" MaxLength="7" Width="200px"></asp:TextBox><br />
                                                <asp:RegularExpressionValidator ID="revtxtDepthWaterBelowGroundLevel" runat="server"
                                                    ForeColor="Red" Display="Dynamic" ControlToValidate="txtDepthWaterBelowGroundLevel"
                                                    ValidationGroup="AddDeWatringStructures"></asp:RegularExpressionValidator>
                                                <asp:RangeValidator ID="RangeValidator4" runat="server" Display="Dynamic" Type="Double"
                                                    ControlToValidate="txtDepthWaterBelowGroundLevel" ForeColor="Red" ValidationGroup="AddDeWatringStructures"
                                                    ErrorMessage="Value Range 0.01 to 1000" MaximumValue="1000.00" MinimumValue="0.01"></asp:RangeValidator>
                                            </td>
                                            
                                        </tr>
                                        <tr id="trDischarge" runat="server" visible="false">
                                            <td colspan="2">
                                                Discharge (m&sup3/Hour):
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtDischarge" runat="server" MaxLength="7" Width="200px"></asp:TextBox><br />
                                                <asp:RegularExpressionValidator ID="revtxtDischarge" runat="server" Display="Dynamic"
                                                    ForeColor="Red" ControlToValidate="txtDischarge" ValidationGroup="AddDeWatringStructures"></asp:RegularExpressionValidator>
                                                <asp:RangeValidator ID="RangeValidator5" runat="server" Display="Dynamic" Type="Double"
                                                    ControlToValidate="txtDischarge" ForeColor="Red" ValidationGroup="AddDeWatringStructures"
                                                    ErrorMessage="Value Range 0.01 to 1000" MaximumValue="1000.00" MinimumValue="0.01"></asp:RangeValidator>
                                            </td>
                                            
                                        </tr>
                                        <tr id="trOperationalHoursDay" runat="server" visible="false">
                                            <td colspan="2">
                                                Operational Hours/Day:
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtOperationalHoursDay" runat="server" MaxLength="2" Width="200px"></asp:TextBox><br />
                                                <asp:RegularExpressionValidator ID="revtxtOperationalHoursDay" runat="server" Display="Dynamic"
                                                    ForeColor="Red" ControlToValidate="txtOperationalHoursDay" ValidationGroup="AddDeWatringStructures"></asp:RegularExpressionValidator>
                                                <asp:RangeValidator ID="RangeValidator6" runat="server" Display="Dynamic" Type="Integer"
                                                    ControlToValidate="txtOperationalHoursDay" ForeColor="Red" ValidationGroup="AddDeWatringStructures"
                                                    ErrorMessage="Value Range 0 to 24" MaximumValue="24" MinimumValue="0"></asp:RangeValidator>
                                            </td>
                                            
                                        </tr>
                                        <tr id="trOperationalDaysYear" runat="server" visible="false">
                                            <td colspan="2">
                                                Operational Days/Year:
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtOperationalDaysYear" runat="server" MaxLength="3" Width="200px"></asp:TextBox><br />
                                                <asp:RegularExpressionValidator ID="revtxtOperationalDaysYear" runat="server" Display="Dynamic"
                                                    ForeColor="Red" ControlToValidate="txtOperationalDaysYear" ValidationGroup="AddDeWatringStructures"></asp:RegularExpressionValidator>
                                                <asp:RangeValidator ID="RangeValidator7" runat="server" Display="Dynamic" Type="Integer"
                                                    ControlToValidate="txtOperationalDaysYear" ForeColor="Red" ValidationGroup="AddDeWatringStructures"
                                                    ErrorMessage="Value Range 0 to 365" MaximumValue="365" MinimumValue="0"></asp:RangeValidator>
                                            </td>
                                          
                                        </tr>
                                        <tr id="trModeOfLift" runat="server" visible="false">
                                            <td colspan="2">
                                                Mode of Lift:
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="ddlModeOfLift" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            
                                        </tr>
                                        <tr id="trHorsePowerPump" runat="server" visible="false">
                                            <td colspan="2">
                                                Horse Power of Pump:
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtHorsePowerPump" runat="server" MaxLength="6" Width="200px"></asp:TextBox><br />
                                                <asp:RegularExpressionValidator ID="revtxtHorsePowerPump" runat="server" ForeColor="Red"
                                                    ControlToValidate="txtHorsePowerPump" Display="Dynamic" ValidationGroup="AddDeWatringStructures"></asp:RegularExpressionValidator>
                                                <asp:RangeValidator ID="RangeValidator8" runat="server" Display="Dynamic" Type="Double"
                                                    ControlToValidate="txtHorsePowerPump" ForeColor="Red" ValidationGroup="AddDeWatringStructures"
                                                    ErrorMessage="Value Range 0.01 to 999.99" MaximumValue="999.99" MinimumValue="0.01"></asp:RangeValidator>
                                            </td>
                                            
                                        </tr>
                                        <tr id="trWhetherFittedWithWaterMeter" runat="server" visible="false">
                                            <td colspan="2">
                                                Whether fitted with Water Meter:
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="ddlWhetherFittedWithWaterMeter" runat="server" Width="200px">
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                           
                                        </tr>
                                        <tr id="trWhetherPermissionRegisteredWithCGWA" runat="server" visible="false">
                                            <td colspan="2">
                                                Whether Permission / Registered with CGWA:
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="ddlWhetherPermissionRegisteredWithCGWA" runat="server" Width="200px"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlWhetherPermissionRegisteredWithCGWA_SelectedIndexChanged">
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            
                                        </tr>
                                        <tr id="trWhetherPermissionRegisteredWithCGWADet" runat="server" visible="false">
                                            <td colspan="2">
                                                If so Details thereof:
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtWhetherPermissionRegisteredWithCGWADet" runat="server" MaxLength="100"
                                                    onkeyup="CountCharacter(this, this.form.ActionCommentRemCount, 100);" onkeydown="CountCharacter(this, this.form.ActionCommentRemCount, 100);"
                                                    TextMode="MultiLine" Width="300px" Height="50px"></asp:TextBox><br />
                                                <input type="text" id="ActionCommentRemCount" tabindex="-1" style="border-width: 0px;
                                                    width: 100px; font-size: 10px; text-align: left; margin-left: 210px; background-color: transparent"
                                                    name="ActionCommentRemCount" size="2" maxlength="2" value="( 100 Character Left )"
                                                    readonly="readonly" /><br />
                                                <asp:RegularExpressionValidator ID="revtxtWhetherPermissionRegisteredWithCGWADet"
                                                    runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtWhetherPermissionRegisteredWithCGWADet"
                                                    ValidationGroup="AddDeWatringStructures"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="revLengthtxtWhetherPermissionRegisteredWithCGWADet"
                                                    runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="AddDeWatringStructures"
                                                    ControlToValidate="txtWhetherPermissionRegisteredWithCGWADet"></asp:RegularExpressionValidator>
                                            </td>
                                           
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                                <asp:Label ID="lblModeFrom" runat="server" Visible="false" Enabled="False"></asp:Label>
                                                <asp:Label ID="lblInfrastructureApplicationCodeFrom" Visible="false" runat="server"
                                                    Enabled="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 23%">
                                            </td>
                                            <td colspan="3">
                                                <asp:Button ID="btnAdd" runat="server" Text="Add In List" ValidationGroup="AddDeWatringStructures"
                                                    OnClick="btnAdd_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                <%--</fieldset>--%>
                            </td>
                        </tr>
                    </table>
                    <%--<table class="SubFormWOBG" width="100%" style="line-height: 25px">
                        <tr>
                            <td colspan="3">
                                <div class="div_IndAppHeading" style="padding-left: 10px">
                                    INFRASTRUCTURE USE: 2. Infrastructure Unit De-Watering Proposed Structure Detail</div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" colspan="3">
                                (<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-Upload
                                Attachments in <strong style="color: Red">Attachment</strong> Section
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <b>(f) Particulars (i-a) is yes (Fill details of Dewatering Proposed Structures)</b>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 40%" colspan="2">
                                Number of Proposed Structures: <span class="Coumpulsory">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNoOfProposedStructure" runat="server" MaxLength="3" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtNoOfProposedStructure"
                                    Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="deWatringRequirementDetails"></asp:RequiredFieldValidator><br />
                                <asp:RegularExpressionValidator ID="revtxtNoOfProposedStructure" runat="server" ForeColor="Red"
                                    ControlToValidate="txtNoOfProposedStructure" Display="Dynamic" ValidationGroup="deWatringRequirementDetails"></asp:RegularExpressionValidator>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" Type="Integer" ControlToValidate="txtNoOfProposedStructure"
                                    Display="Dynamic" ForeColor="Red" ValidationGroup="deWatringRequirementDetails"
                                    ErrorMessage="Value Range 0 to 100" MaximumValue="100" MinimumValue="0"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <%--<fieldset>
                                    <legend><b>Details:</b></legend>--%
                                    <table class="SubFormWOBG" width="100%" style="line-height: 25px">
                                        <tr>
                                            <td colspan="2" style="width:40%">
                                                Type of Structure: <span class="Coumpulsory">*</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTypeOfStructure" runat="server" Width="200px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                    Display="Dynamic" ValidationGroup="AddDeWatringStructures" InitialValue=" " ControlToValidate="ddlTypeOfStructure">Required</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr runat="server" visible="false">
                                            <td colspan="2">
                                                Year of Construction:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtYearOfConstruction" runat="server" MaxLength="4" Width="200px"></asp:TextBox><br />
                                                <asp:RangeValidator ID="RngValYearOfConstruction" runat="server" ControlToValidate="txtYearOfConstruction"
                                                    Display="Dynamic" Type="Integer" MinimumValue="1900" MaximumValue="3000" ForeColor="Red"
                                                    ErrorMessage="Invalid Year" ValidationGroup="AddDeWatringStructures"></asp:RangeValidator>
                                                <asp:RegularExpressionValidator ID="revtxtYearOfConstruction" runat="server" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="txtYearOfConstruction" ValidationGroup="AddDeWatringStructures"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr runat="server" visible="false">
                                            <td colspan="2">
                                                Depth (Meter):
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDepthMeter" runat="server" MaxLength="7" Width="200px"></asp:TextBox><br />
                                                <asp:RangeValidator ID="RangeValidator2" runat="server" Type="Double" ControlToValidate="txtDepthMeter"
                                                    Display="Dynamic" ForeColor="Red" ValidationGroup="AddDeWatringStructures" ErrorMessage="Value Range 0.01 to 1000"
                                                    MaximumValue="1000.00" MinimumValue="0.01"></asp:RangeValidator>
                                                <asp:RegularExpressionValidator ID="revtxtDepthMeter" runat="server" Display="Dynamic"
                                                    ForeColor="Red" ControlToValidate="txtDepthMeter" ValidationGroup="AddDeWatringStructures"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr runat="server" visible="false">
                                            <td colspan="2">
                                                Diameter (mm):
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDiameterMM" runat="server" MaxLength="6" Width="200px"></asp:TextBox><br />
                                                <asp:RegularExpressionValidator ID="revtxtDiameterMM" runat="server" Display="Dynamic"
                                                    ForeColor="Red" ControlToValidate="txtDiameterMM" ValidationGroup="AddDeWatringStructures"></asp:RegularExpressionValidator>
                                                <asp:RangeValidator ID="RangeValidator3" runat="server" Display="Dynamic" Type="Integer"
                                                    ControlToValidate="txtDiameterMM" ForeColor="Red" ValidationGroup="AddDeWatringStructures"
                                                    ErrorMessage="Value can't be more than 999999" MaximumValue="999999" MinimumValue="0"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr id="trDepthWaterBelowGroundLevel" runat="server" visible="false">
                                            <td colspan="2">
                                                Depth to Water Level (Meters below Ground Level):
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDepthWaterBelowGroundLevel" runat="server" MaxLength="7" Width="200px"></asp:TextBox><br />
                                                <asp:RegularExpressionValidator ID="revtxtDepthWaterBelowGroundLevel" runat="server"
                                                    ForeColor="Red" Display="Dynamic" ControlToValidate="txtDepthWaterBelowGroundLevel"
                                                    ValidationGroup="AddDeWatringStructures"></asp:RegularExpressionValidator>
                                                <asp:RangeValidator ID="RangeValidator4" runat="server" Display="Dynamic" Type="Double"
                                                    ControlToValidate="txtDepthWaterBelowGroundLevel" ForeColor="Red" ValidationGroup="AddDeWatringStructures"
                                                    ErrorMessage="Value Range 0.01 to 1000" MaximumValue="1000.00" MinimumValue="0.01"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr id="trDischarge" runat="server" visible="false">
                                            <td colspan="2">
                                                Discharge (m&sup3/Hour):
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDischarge" runat="server" MaxLength="7" Width="200px"></asp:TextBox><br />
                                                <asp:RegularExpressionValidator ID="revtxtDischarge" runat="server" Display="Dynamic"
                                                    ForeColor="Red" ControlToValidate="txtDischarge" ValidationGroup="AddDeWatringStructures"></asp:RegularExpressionValidator>
                                                <asp:RangeValidator ID="RangeValidator5" runat="server" Display="Dynamic" Type="Double"
                                                    ControlToValidate="txtDischarge" ForeColor="Red" ValidationGroup="AddDeWatringStructures"
                                                    ErrorMessage="Value Range 0.01 to 1000" MaximumValue="1000.00" MinimumValue="0.01"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr id="trOperationalHoursDay" runat="server" visible="false">
                                            <td colspan="2">
                                                Operational Hours/Day:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOperationalHoursDay" runat="server" MaxLength="2" Width="200px"></asp:TextBox><br />
                                                <asp:RegularExpressionValidator ID="revtxtOperationalHoursDay" runat="server" Display="Dynamic"
                                                    ForeColor="Red" ControlToValidate="txtOperationalHoursDay" ValidationGroup="AddDeWatringStructures"></asp:RegularExpressionValidator>
                                                <asp:RangeValidator ID="RangeValidator6" runat="server" Display="Dynamic" Type="Integer"
                                                    ControlToValidate="txtOperationalHoursDay" ForeColor="Red" ValidationGroup="AddDeWatringStructures"
                                                    ErrorMessage="Value Range 0 to 24" MaximumValue="24" MinimumValue="0"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr id="trOperationalDaysYear" runat="server" visible="false">
                                            <td colspan="2">
                                                Operational Days/Year:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOperationalDaysYear" runat="server" MaxLength="3" Width="200px"></asp:TextBox><br />
                                                <asp:RegularExpressionValidator ID="revtxtOperationalDaysYear" runat="server" Display="Dynamic"
                                                    ForeColor="Red" ControlToValidate="txtOperationalDaysYear" ValidationGroup="AddDeWatringStructures"></asp:RegularExpressionValidator>
                                                <asp:RangeValidator ID="RangeValidator7" runat="server" Display="Dynamic" Type="Integer"
                                                    ControlToValidate="txtOperationalDaysYear" ForeColor="Red" ValidationGroup="AddDeWatringStructures"
                                                    ErrorMessage="Value Range 0 to 365" MaximumValue="365" MinimumValue="0"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr id="trModeOfLift" runat="server" visible="false">
                                            <td colspan="2">
                                                Mode of Lift:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlModeOfLift" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="trHorsePowerPump" runat="server" visible="false">
                                            <td colspan="2">
                                                Horse Power of Pump:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtHorsePowerPump" runat="server" MaxLength="6" Width="200px"></asp:TextBox><br />
                                                <asp:RegularExpressionValidator ID="revtxtHorsePowerPump" runat="server" ForeColor="Red"
                                                    ControlToValidate="txtHorsePowerPump" Display="Dynamic" ValidationGroup="AddDeWatringStructures"></asp:RegularExpressionValidator>
                                                <asp:RangeValidator ID="RangeValidator8" runat="server" Display="Dynamic" Type="Double"
                                                    ControlToValidate="txtHorsePowerPump" ForeColor="Red" ValidationGroup="AddDeWatringStructures"
                                                    ErrorMessage="Value Range 0.01 to 999.99" MaximumValue="999.99" MinimumValue="0.01"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr id="trWhetherFittedWithWaterMeter" runat="server" visible="false">
                                            <td colspan="2">
                                                Whether fitted with Water Meter:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlWhetherFittedWithWaterMeter" runat="server" Width="200px">
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="trWhetherPermissionRegisteredWithCGWA" runat="server" visible="false">
                                            <td colspan="2">
                                                Whether Permission / Registered with CGWA:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlWhetherPermissionRegisteredWithCGWA" runat="server" Width="200px"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlWhetherPermissionRegisteredWithCGWA_SelectedIndexChanged">
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="trWhetherPermissionRegisteredWithCGWADet" runat="server" visible="false">
                                            <td colspan="2">
                                                If so Details thereof:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtWhetherPermissionRegisteredWithCGWADet" runat="server" MaxLength="100"
                                                    onkeyup="CountCharacter(this, this.form.ActionCommentRemCount, 100);" onkeydown="CountCharacter(this, this.form.ActionCommentRemCount, 100);"
                                                    TextMode="MultiLine" Width="300px" Height="50px"></asp:TextBox><br />
                                                <input type="text" id="ActionCommentRemCount" tabindex="-1" style="border-width: 0px;
                                                    width: 100px; font-size: 10px; text-align: left; margin-left: 210px; background-color: transparent"
                                                    name="ActionCommentRemCount" size="2" maxlength="2" value="( 100 Character Left )"
                                                    readonly="readonly" /><br />
                                                <asp:RegularExpressionValidator ID="revtxtWhetherPermissionRegisteredWithCGWADet"
                                                    runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtWhetherPermissionRegisteredWithCGWADet"
                                                    ValidationGroup="AddDeWatringStructures"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="revLengthtxtWhetherPermissionRegisteredWithCGWADet"
                                                    runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="AddDeWatringStructures"
                                                    ControlToValidate="txtWhetherPermissionRegisteredWithCGWADet"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                                <asp:Label ID="lblModeFrom" runat="server" Visible="false" Enabled="False"></asp:Label>
                                                <asp:Label ID="lblInfrastructureApplicationCodeFrom" Visible="false" runat="server"
                                                    Enabled="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAdd" runat="server" Text="Add In List" ValidationGroup="AddDeWatringStructures"
                                                    OnClick="btnAdd_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                <%--</fieldset>--%
                            </td>
                        </tr>
                    </table>--%>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvdeWateringPrposed" runat="server" AutoGenerateColumns="false"
                    DataKeyNames="ApplicationCode" Caption="<th colspan='15'><b>Detail of Dewatering Proposed Structures</b></th>"
                    CssClass="SubFormWOBG" Width="100%" OnRowCommand="gvdeWateringPrposed_RowCommand" OnRowDataBound="gvdeWateringPrposed_RowDataBound"
                    ShowHeaderWhenEmpty="True" HorizontalAlign="Center">
                    <Columns>
                        <asp:TemplateField HeaderText="SNo.">
                            <ItemTemplate>
                                <span>
                                    <%#Container.DataItemIndex + 1 %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Serial Number" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="SerialNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("SerialNumber")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type of Structure Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="TypeOfAbstractionStructureCode" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TypeOfAbstractionStructureCode")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type of Structure Name">
                            <ItemTemplate>
                                <asp:Label ID="TypeOfAbstractionStructureName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Year of Construction" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="YearOfConstruction" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("YearOfConstruction")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Depth (Meter)" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="DepthExist" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("DepthExist")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Diameter (mm)" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="Diameter" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Diameter")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Depth to Water Level (Meters below Ground Level)" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="DepthBelowWaterLevel" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("DepthBelowWaterLevel")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Discharge (m3/Hour)" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="Discharge" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Discharge")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Operational Hours/Day" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="OperationalHousrDay" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("OperationalHousrDay")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Operational Days/Year" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="OperationalDaysYear" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("OperationalDaysYear")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mode of Lift Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="LiftingDeviceCode" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("LiftingDeviceCode")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mode of Lift Name" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="LiftingDeviceName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Horse Power of Pump" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="PowerOfPump" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("PowerOfPump")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Whether fitted with Water Meter" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="WaterFittedWithWaterMeterOrNot" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("WaterFittedWithWaterMeterOrNot")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Whether Permission/Registered with CGWA" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="WhetherPermissionRegisteredWithCGWA" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("WhetherPermissionRegisteredWithCGWA")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="If so Details Thereof" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="WhetherPermissionRegisteredWithCGWADetails" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("WhetherPermissionRegisteredWithCGWADetails")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnDelete" runat="server" Height="20px" ImageUrl="~/Images/delete.jpg"
                                    Width="20px" OnClientClick="return confirm('Are you sure you want to delete?');"
                                    CommandName="DeleteName" CommandArgument='<%#Container.DataItemIndex + 1 %>'
                                    ToolTip="Delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Records exist in Dewatering Proposed Structures.
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="text-align: center">
                <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('Please Save as Draft before moving to Previous Page ?');"
                    OnClick="btnPrev_Click" />
                <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" ValidationGroup="deWatringRequirementDetails"
                    OnClick="btnSaveAsDraft_Click" />
                <asp:Button ID="txtNext" runat="server" Text="Next >>" ValidationGroup="deWatringRequirementDetails"
                    OnClick="txtNext_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
