<%@ Page Title="NOCAP-Infrastructure Application-Expansion" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="InfrastructureExpansion.aspx.cs"
    Inherits="ExternalUser_Expansion_INF_InfrastructureExpansion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:content id="Content1" contentplaceholderid="head" runat="Server">
    <script src="../../Scripts/ClientSideDateValidation.js" type="text/javascript"></script>
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="Server">
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
                                            <li>Communication Address</li>
                                            <li>Land Use Details</li>
                                            <li>Water Requirement Details</li>
                                            <li>De-Watering Existing Structure</li>
                                            <li>De-Watering Proposed Structure</li>
                                            <li>Groundwater Abstraction Structure-Existing</li>
                                            <li>Groundwater Abstraction Structure-Proposed</li>
                                            <li>Breakup of Water Requirment</li>
                                            <li>Water Supply Detail</li>
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
                        <td colspan="5">
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                INFRASTRUCTURE EXPANSION USE: 1. General Information- Location Details
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="5">
                            &nbsp; (<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-Upload
                            Attachments in <strong style="color: Red">Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">
                            (1).
                        </td>
                        <td colspan="4">
                            <b>General Information:</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            Water Quality Type : <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlWaterQualityType" runat="server" Width="200px" Enabled="false">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LocationDetails" InitialValue="" ControlToValidate="ddlWaterQualityType">Required</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            Application Type Category / Type of Application: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlApplicationTypeCategory" runat="server" Width="200px" Enabled="false">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorNewINFApplicationCat" runat="server"
                                ForeColor="Red" Display="Dynamic" ValidationGroup="LocationDetails" InitialValue=""
                                ControlToValidate="ddlApplicationTypeCategory">Required</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>                        
                        <td>
                            (i) Name of Infrastructure: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNameOfInfraStructure" runat="server" MaxLength="100" Width="99%" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorNameofINF" runat="server" ForeColor="Red"
                                ValidationGroup="LocationDetails" ControlToValidate="txtNameOfInfraStructure"
                                Display="Dynamic">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtNameOfInfraStructure" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtNameOfInfraStructure" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="4">
                            <strong>(ii) Location Details of the Infrastructure Unit- (Attach Approved Site 
                            Plan and Location
                               Map)</strong> <span class="Coumpulsory">*</span>
                             <%--   Map) (<span class="Coumpulsory">$</span>)</strong> <span class="Coumpulsory">*</span>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            Address Line 1: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddressLine1" runat="server" MaxLength="100" Width="99%" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LocationDetails" ControlToValidate="txtAddressLine1"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtAddressLine1" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtAddressLine1" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            Address Line 2:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddressLine2" runat="server" MaxLength="100" Width="99%" Enabled="false"></asp:TextBox><br />
                            <asp:RegularExpressionValidator ID="revtxtAddressLine2" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtAddressLine2" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>                        
                        <td>
                            Address Line 3:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddressLine3" runat="server" MaxLength="100" Width="99%" Enabled="false"></asp:TextBox><br />
                            <asp:RegularExpressionValidator ID="revtxtAddressLine3" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtAddressLine3" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>                            
                            State: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" Width="200px"
                                OnSelectedIndexChanged="ddlState_SelectedIndexChanged" Enabled="false">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue=""
                                Display="Dynamic" ControlToValidate="ddlState" ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            District: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="True" Width="200px"
                                OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" Enabled="false">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue=" "
                                Display="Dynamic" ControlToValidate="ddlDistrict" ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            Sub-District: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSubDistrict" runat="server" AutoPostBack="True" Width="200px"
                                OnSelectedIndexChanged="ddlSubDistrict_SelectedIndexChanged" Enabled="false">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue=" "
                                Display="Dynamic" ControlToValidate="ddlSubDistrict" ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            Village/Town: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTownOrVillage" runat="server" Width="200px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlTownOrVillage_SelectedIndexChanged" Enabled="false">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" InitialValue=""
                                Display="Dynamic" ControlToValidate="ddlTownOrVillage" ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>                        
                        <td>
                            <asp:Label ID="lblVillage" runat="server" Text="Village: <span class='Coumpulsory'>*</span>"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlVillage" runat="server" Width="200px" Enabled="false">
                            </asp:DropDownList >
                            <asp:RequiredFieldValidator ID="reqValiVillage" runat="server" InitialValue=" " Display="Dynamic"
                                ControlToValidate="ddlVillage" ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                        </td>                        
                        <td>
                            <asp:Label ID="lblTown" runat="server" Text="Town: <span class='Coumpulsory'>*</span>"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTown" runat="server" Width="200px" Enabled="false">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqValiTown" runat="server" InitialValue="" Display="Dynamic"
                                ControlToValidate="ddlTown" ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            Latitude 
                        </td>
                        <td>
                            <asp:TextBox ID="txtProLat" runat="server" Width="99%" Enabled="false"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvtxtProLat" runat="server" Display="Dynamic" ControlToValidate="txtProLat"
                                ForeColor="Red" ValidationGroup="LocationDetails" Enabled="false">Required</asp:RequiredFieldValidator>--%>
                            <asp:RegularExpressionValidator ID="revtxtProLat" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtProLat" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator> 
                            <asp:RangeValidator ID="rvtxtProLat" runat="server" Display="Dynamic" ErrorMessage="Value should be between 8.066700 and 37.100000" ValidationGroup="LocationDetails"
                                Type="Double" MinimumValue="8.066700" MaximumValue="37.100000" ForeColor="Red" ControlToValidate="txtProLat"></asp:RangeValidator>
                        </td>
                        <td>
                            Longitude 
                        </td>
                        <td>
                            <asp:TextBox ID="txtProLong" runat="server" Width="99%" Enabled="false"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvtxtProLong" runat="server" Display="Dynamic" ControlToValidate="txtProLong"
                                ForeColor="Red" ValidationGroup="LocationDetails" Enabled =" false">Required</asp:RequiredFieldValidator>--%>
                            <asp:RegularExpressionValidator ID="revtxtProLong" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtProLong" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                            <asp:RangeValidator ID="rvtxtProLong" runat="server" Display="Dynamic" ErrorMessage="Value should be between 68.116600 and 97.416700" ValidationGroup="LocationDetails"
                                Type="Double" MinimumValue="68.116600" MaximumValue="97.416700" ForeColor="Red" ControlToValidate="txtProLong"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                           <td></td>
                        <td style="width: 35%">Whether Project is MSE: <span class="Coumpulsory">*</span>(<span class="Coumpulsory">$</span>)
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlMSME" Width="200px" Enabled="false">
                                <asp:ListItem Value="">-----Select-----</asp:ListItem>
                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LocationDetails" InitialValue=""
                                ControlToValidate="ddlMSME">Required</asp:RequiredFieldValidator>

                        </td>
                        <td style="width: 35%">MSE Type</td>
                        <td><asp:DropDownList ID="ddMSMEType" Width="200px" runat="server" Enabled="false"> 
                            </asp:DropDownList>                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 35%">Whether project falling within 500m from the periphery of demarcated Wetland: <span class="Coumpulsory">*</span>(<span class="Coumpulsory">$</span>)
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlWetlandArea" Width="200px" Enabled="false">
                                <asp:ListItem Value="">-----Select-----</asp:ListItem>
                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LocationDetails" InitialValue=""
                                ControlToValidate="ddlWetlandArea">Required</asp:RequiredFieldValidator>

                        </td>
                        <td>
                            <%--Whether Ground Water Utilization for: <span class="Coumpulsory">*</span>--%>
                            Type of Project: <span class="Coumpulsory">*</span>
                        </td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="rbtnWhetherGroundWaterUtilization" runat="server" align="left"
                                AutoPostBack="true" RepeatDirection="Vertical" OnSelectedIndexChanged="rbtnWhetherGroundWaterUtilization_SelectedIndexChanged" Enabled="false">
                                <asp:ListItem Value="NewIndustry">New Project</asp:ListItem>
                                <asp:ListItem Value="ExistingIndustry">Existing Project</asp:ListItem>
                                <asp:ListItem Value="ExpansionProgramExistingIndustry">Expansion Program of Existing Project</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ForeColor="Red"
                                InitialValue="" Display="Dynamic" ValidationGroup="LocationDetails" ControlToValidate="rbtnWhetherGroundWaterUtilization">Required</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr runat="server" id="RowNOCObtainedForExistIND" visible="false">
                        <td>
                        </td>
                        <td>
                            Whether NOC Obtained for Existing Usage of Groundwater :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlNOCObtainedForExistIND" runat="server" Width="200px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlNOCObtainedForExistIND_SelectedIndexChanged" Enabled="false">
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
                        <td>
                        </td>
                        <td>
                            Date of Commencement :
                        </td>
                        <td>
                            <asp:TextBox ID="txtDateOfCommencement" Width="85%"  runat="server" Enabled="false"></asp:TextBox><%--Width="200px"--%>
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
                            <%--<asp:RegularExpressionValidator ID="revtxtDateOfCommencement" runat="server" ValidationGroup="LocationDetails"
                                ForeColor="Red" ControlToValidate="txtDateOfCommencement" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                            <asp:RangeValidator ID="rvtxtDateOfCommencement" runat="server" Type="Date" Display="Dynamic"
                                ValidationGroup="LocationDetails" ForeColor="Red" MinimumValue="01/01/1900" ControlToValidate="txtDateOfCommencement"
                                ErrorMessage="Date of Commencement should be grater than 01/01/1900 and less than or equal to current date."></asp:RangeValidator>
                        </td>
                        <td></td>
                        <td></td>
                        </tr>
                    <tr runat="server" id="RowDateOfExpansion" visible="false">
                        <td>
                        </td>
                        <td>
                            Date of Expansion :
                        </td>
                        <td>
                            <asp:TextBox ID="txtDateOfExpansion" Width="85%" runat="server" Enabled="false"></asp:TextBox><%--Width="200px"--%>
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
                            <%-- <asp:RegularExpressionValidator ID="revtxtDateOfExpansion" runat="server" ValidationGroup="LocationDetails"
                                    ForeColor="Red" ControlToValidate="txtDateOfExpansion" Display="Dynamic"></asp:RegularExpressionValidator>
                            --%>
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
                    <tr>
                        <td colspan="5">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblPageTitleFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblInfrastructureApplicationCodeFrom" Visible="false" runat="server"
                                Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="5">
                            <asp:Button ID="Check" runat="server" Text="Check" OnClick="Check_Click" Visible="False" />
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" ValidationGroup="LocationDetails"
                                OnClick="btnSaveAsDraft_Click" />
                            <asp:Button ID="btnNext" runat="server" Text="Next >>" ValidationGroup="LocationDetails"
                                OnClick="btnNext_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:content>
