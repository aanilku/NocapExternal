<%@ Page Title="NOCAP-Industrial Application" Language="C#" MaintainScrollPositionOnPostback="true"
    MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true"
    CodeFile="IndustrialNew.aspx.cs" Inherits="ExternalUser_IndustrialNew_IndustrialNew" %>

<%--<%@ PreviousPageType VirtualPath="~/ExternalUser/ApplicantHome.aspx" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Scripts/ClientSideDateValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                            <li>Recycled Water Usage</li>
                                            <li>Groundwater Abstraction Structure- Existing</li>
                                            <li>Groundwater Abstraction Structure- Proposed</li>
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
                                INDUSTRIAL USE: 1. General Information- Location Details
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: right">(<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-Upload
                            Attachments in <strong>Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">
                            <b>(1).</b>
                        </td>
                        <td colspan="2">
                            <b>General Information:</b>
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td style="width: 35%">Water Quality Type : <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlWaterQualityType" runat="server" Width="200px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LocationDetails" InitialValue="" ControlToValidate="ddlWaterQualityType">Required</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="width: 35%">Application Type Category / Type of Application: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlApplicationTypeCategory" runat="server" Width="200px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LocationDetails" InitialValue="" ControlToValidate="ddlApplicationTypeCategory">Required</asp:RequiredFieldValidator>

                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td>(i) Name of Industry: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNameOfIndustry" runat="server" MaxLength="100" Width="99%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                ValidationGroup="LocationDetails" ControlToValidate="txtNameOfIndustry" Display="Dynamic">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtNameOfIndustry" runat="server" ValidationGroup="LocationDetails"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtNameOfIndustry"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="2">
                            <%--   (ii) Location Details of the Industrial Unit- (Attach Site Plan and Certified Revenue
                            Sketch) (<span class="Coumpulsory">$</span>) <span class="Coumpulsory">*</span>--%>
                            (ii) Location Details of the Industrial Unit- (Attach Approved Site Plan with Location
                              Map) <span class="Coumpulsory">*</span>
                            <%-- Map) (<span class="Coumpulsory">$</span>) <span class="Coumpulsory">*</span>--%>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Address Line 1: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddressLine1" runat="server" MaxLength="100" Width="99%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LocationDetails" ControlToValidate="txtAddressLine1">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtAddressLine1" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtAddressLine1" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Address Line 2:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddressLine2" runat="server" MaxLength="100" Width="99%"></asp:TextBox><br />
                            <asp:RegularExpressionValidator ID="revtxtAddressLine2" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtAddressLine2" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Address Line 3:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddressLine3" runat="server" MaxLength="100" Width="99%"></asp:TextBox><br />
                            <asp:RegularExpressionValidator ID="revtxtAddressLine3" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtAddressLine3" ValidationGroup="LocationDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
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
                        <td></td>
                        <td>District: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                AutoPostBack="True" Width="200px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue=" "
                                Display="Dynamic" ControlToValidate="ddlDistrict" ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
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
                        <td></td>
                        <td>Village / Town: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTownOrVillage" runat="server" Width="200px" OnSelectedIndexChanged="ddlTownOrVillage_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" InitialValue=""
                                Display="Dynamic" ControlToValidate="ddlTownOrVillage" ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
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
                        <td></td>
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
                        <td></td>
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
                    </tr>
                    <tr>
                        <td></td>
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
                        <td></td>
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
                    </tr>
                    <tr>
                        <td></td>
                        <td style="width: 35%">MSME Type</td>
                        <td>
                            <asp:DropDownList ID="ddMSMEType" Width="200px" runat="server">
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="rfvMSMEType" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="LocationDetails" InitialValue=""
                                ControlToValidate="ddMSMEType">Required</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
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
                    </tr>
                    <tr>
                        <td></td>
                        <td>Whether Ground Water Utilization for: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rbtnWhetherGroundWaterUtilization" runat="server" align="left"
                                AutoPostBack="true" RepeatDirection="Vertical" OnSelectedIndexChanged="rbtnWhetherGroundWaterUtilization_SelectedIndexChanged">
                                <asp:ListItem Value="NewIndustry">New Industry</asp:ListItem>
                                <asp:ListItem Value="ExistingIndustry">Existing Industry</asp:ListItem>
                                <asp:ListItem Value="ExpansionProgramExistingIndustry">Expansion Program of Existing Industry</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ForeColor="Red"
                                InitialValue="" Display="Dynamic" ValidationGroup="LocationDetails" ControlToValidate="rbtnWhetherGroundWaterUtilization">Required</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr runat="server" id="RowNOCObtainedForExistIND" visible="false">
                        <td></td>
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
                    </tr>
                    <tr runat="server" id="RowDateOfCommencement" visible="false">
                        <td></td>
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
                    </tr>
                    <tr runat="server" id="RowDateOfExpansion" visible="false">
                        <td></td>
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
                            <asp:Button ID="Check" runat="server" OnClick="Check_Click" Text="Check" Visible="False" />
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" OnClick="btnSaveAsDraft_Click"
                                ValidationGroup="LocationDetails" />
                            <asp:Button ID="btnNext" runat="server" Text="Next >>" OnClick="btnNext_Click" ValidationGroup="LocationDetails"

                                Style="height: 26px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
