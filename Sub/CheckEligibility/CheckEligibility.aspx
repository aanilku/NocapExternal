<%@ Page Title="" Language="C#" MasterPageFile="~/Sub/ApplicantRegi/ApplicantRegistrationMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="CheckEligibility.aspx.cs"
    Inherits="Sub_CheckEligibility_CheckEligibility" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Scripts/ClientSideDateValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table class="SubFormWOBG" width="100%">
        <tr>
            <th colspan="2" style="height: 20px">
                <div class="div_IndAppHeading" style="padding-left: 10px; font-size: 18px;">
                    Check Application Eligibility
                </div>
            </th>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel2" GroupingText="<span style='font-weight:bold;font-size:17px'>Application Information</span>"
                    runat="server">
                    <table width="100%" class="SubFormWOBG">
                        <tr>
                            <td style="width: 50%">
                                Application Type:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlApplicationType" runat="server" Width="250px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlApplicationType_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                    Display="Dynamic" ValidationGroup="CheckEligibility" InitialValue="" ControlToValidate="ddlApplicationType">Required</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                       <tr>
                            <td>
                                Application Purpose:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlApplicationPurpose" runat="server" Width="250px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlApplicationPurpose_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" InitialValue=""
                                    Display="Dynamic" ControlToValidate="ddlApplicationPurpose" ForeColor="Red" ValidationGroup="CheckEligibility">Required</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Application Type Category/ Type of Application:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlApplicationTypeCategory" runat="server" Width="250px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlApplicationTypeCategory_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                    Display="Dynamic" ValidationGroup="CheckEligibility" InitialValue="" ControlToValidate="ddlApplicationTypeCategory">Required</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel5" GroupingText="<span style='font-weight:bold;font-size:17px'>Water Quality</span>"
                    runat="server">
                    <table width="100%" class="SubFormWOBG">
                        <tr>
                            <td style="width: 50%">
                                Water Quality Type :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlWaterQualityType" runat="server" Width="250px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red"
                                    Display="Dynamic" ValidationGroup="CheckEligibility" InitialValue="" ControlToValidate="ddlWaterQualityType">Required</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel runat="server" ID="UtilizationFor" GroupingText="<span style='font-weight:bold;font-size:17px'>Ground Water Utilization</span>">
                    <table width="100%" class="SubFormWOBG">
                        <tr>
                            <td style="width: 50%">
                                Whether Ground Water Utilization for: <span class="Coumpulsory">*</span>
                            </td>
                            <td style="width: 50%">
                                <asp:RadioButtonList ID="rbtnWhetherGroundWaterUtilization" runat="server" align="left"
                                    Style="font-size: 12px" AutoPostBack="true" RepeatDirection="Vertical" OnSelectedIndexChanged="rbtnWhetherGroundWaterUtilization_SelectedIndexChanged">
                                    <asp:ListItem Value="NewIndustry">New Industry</asp:ListItem>
                                    <asp:ListItem Value="ExistingIndustry">Existing Industry</asp:ListItem>
                                    <asp:ListItem Value="ExpansionProgramExistingIndustry">Expansion Program of Existing Industry</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ForeColor="Red"
                                    InitialValue="" Display="Dynamic" ValidationGroup="CheckEligibility" ControlToValidate="rbtnWhetherGroundWaterUtilization">Required</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat="server" id="RowNOCObtainedForExistIND" visible="false">
                            <td>
                                Whether NOC Obtained for Existing Usage of Groundwater :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlNOCObtainedForExistIND" runat="server" AutoPostBack="true"
                                    Width="250px" OnSelectedIndexChanged="ddlNOCObtainedForExistIND_SelectedIndexChanged">
                                    <asp:ListItem Value="">-----Select-----</asp:ListItem>
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlNOCObtainedForExistIND" runat="server" InitialValue=" "
                                    Display="Dynamic" ControlToValidate="ddlNOCObtainedForExistIND" ForeColor="Red"
                                    ValidationGroup="CheckEligibility">Required</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat="server" id="RowDateOfCommencement" visible="false">
                            <td>
                                Date of Commencement :
                            </td>
                            <td>
                                <asp:TextBox ID="txtDateOfCommencement" Width="153px" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="txtDOB_CalendarExtender" runat="server" Enabled="True"
                                    TargetControlID="txtDateOfCommencement" PopupButtonID="imgbtnCalendar" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                                <asp:ImageButton ID="imgbtnCalendar" runat="server" ImageUrl="~/Images/calendar.png"
                                    OnClick="imgbtnCalendar_Click" />
                                <asp:RequiredFieldValidator ID="rfvtxtDateOfCommencement" runat="server" Enabled="false"
                                    Display="Dynamic" ControlToValidate="txtDateOfCommencement" ForeColor="Red" ValidationGroup="CheckEligibility">Required</asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="CstmVtxtDateOfCommencement" runat="server" ClientValidationFunction="ValidateDateOnClient"
                                    OnServerValidate="ValidateDate" ControlToValidate="txtDateOfCommencement" ErrorMessage="Invalid Date."
                                    ForeColor="Red" ValidationGroup="CheckEligibility" />
                                <%--  <asp:RegularExpressionValidator ID="revtxtDateOfCommencement" runat="server" ValidationGroup="CheckEligibility"
                                    ForeColor="Red" ControlToValidate="txtDateOfCommencement" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                                <asp:RangeValidator ID="rvtxtDateOfCommencement" runat="server" Type="Date" Display="Dynamic"
                                    ValidationGroup="CheckEligibility" ForeColor="Red" MinimumValue="01/01/1900"
                                    ControlToValidate="txtDateOfCommencement" ErrorMessage="Date of Commencement should be grater than 01/01/1900 and less than or equal to current date."></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr runat="server" id="RowDateOfExpansion" visible="false">
                            <td>
                                Date of Expansion :
                            </td>
                            <td>
                                <asp:TextBox ID="txtDateOfExpansion" Width="153px" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="txtDateOfExpansion_CalendarExtender" runat="server" Enabled="True"
                                    TargetControlID="txtDateOfExpansion" PopupButtonID="imgbtnCalendar_DateOfExpansion"
                                    Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                                <asp:ImageButton ID="imgbtnCalendar_DateOfExpansion" runat="server" ImageUrl="~/Images/calendar.png" />
                                <asp:RequiredFieldValidator ID="rfvtxtDateOfExpansion" runat="server" Enabled="false"
                                    Display="Dynamic" ControlToValidate="txtDateOfExpansion" ForeColor="Red" ValidationGroup="CheckEligibility">Required</asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="CstmVtxtDateOfExpansion" runat="server" ClientValidationFunction="ValidateDateOnClient"
                                    ControlToValidate="txtDateOfExpansion" ErrorMessage="Invalid Date." ForeColor="Red"
                                    ValidationGroup="CheckEligibility" />
                                <%-- <asp:RegularExpressionValidator ID="revtxtDateOfExpansion" runat="server" ValidationGroup="CheckEligibility"
                                    ForeColor="Red" ControlToValidate="txtDateOfExpansion" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                                <asp:RangeValidator ID="rvtxtDateOfExpansion" runat="server" Type="Date" Display="Dynamic"
                                    ValidationGroup="CheckEligibility" ForeColor="Red" MinimumValue="01/01/1900"
                                    ControlToValidate="txtDateOfExpansion" ErrorMessage="Date of Expansion should be grater than 01/01/1900 and less than or equal to current date."></asp:RangeValidator>
                                <asp:CompareValidator ID="cvtxtDateOfExpansion" runat="server" Type="Date" ControlToValidate="txtDateOfExpansion"
                                    ControlToCompare="txtDateOfCommencement" ForeColor="Red" ErrorMessage="Date Of Expansion should be greater than Date of Commencement"
                                    Display="Dynamic" ValidationGroup="CheckEligibility" Operator="GreaterThan"></asp:CompareValidator>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel3" GroupingText="<span style='font-weight:bold;font-size:17px'>Location Detail</span>"
                    runat="server">
                    <table width="100%" class="SubFormWOBG">
                        <tr>
                            <td style="width: 50%">
                                State: <span class="Coumpulsory">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" Width="250px"
                                    OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue=""
                                    Display="Dynamic" ControlToValidate="ddlState" ForeColor="Red" ValidationGroup="CheckEligibility">Required</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                District: <span class="Coumpulsory">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="True" Width="250px"
                                    OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue=" "
                                    Display="Dynamic" ControlToValidate="ddlDistrict" ForeColor="Red" ValidationGroup="CheckEligibility">Required</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Assessment unit: <span class="Coumpulsory">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSubDistrict" runat="server"  Width="250px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue=" "
                                    Display="Dynamic" ControlToValidate="ddlSubDistrict" ForeColor="Red" ValidationGroup="CheckEligibility">Required</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Button ID="btnCheck" runat="server" Text="Check Eligibility" ValidationGroup="CheckEligibility"
                    OnClick="btnCheck_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Reset" CausesValidation="false" OnClick="btnCancel_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel1" GroupingText="<span style='font-weight:bold;font-size:17px'>Eligibility Result</span>"
                    runat="server">
                    <table width="100%" class="SubFormWOBG">
                        <tr>
                            <td style="width: 70%">
                                Whether regulated by State Govt:
                            </td>
                            <td>
                                <asp:Label ID="lblStateGroundWaterAuthority" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblStateGroundWaterAuthorityAddress" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="Panel4" runat="server">
                        <table width="100%" class="SubFormWOBG">
                            <tr>
                                <td style="width: 70%">
                                    Area Type Category :
                                </td>
                                <td>
                                    <asp:Label ID="lblAreaTypeCategory" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Categorization of Assessment Units:
                                </td>
                                <td>
                                    <asp:Label ID="lblCategorizationofAssessmentUnits" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Water Based / Water Intensive Industry :
                                </td>
                                <td>
                                    <asp:Label ID="lblWaterBased" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Water Quality Type :
                                </td>
                                <td>
                                    <asp:Label ID="lblWaterQualityType" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <fieldset>
                                        <legend><span style='font-size: 15px'>Ground Water Utilization</span></legend>
                                        <table width="100%" style="line-height: 20px; font-size: 12px" class="SubFormWOBG">
                                            <tr>
                                                <td style="width: 70%">
                                                    Whether Ground Water Utilization for:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblGroundWaterUtilizationFor" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="RowNOCObtainedForExistINDRe" visible="false">
                                                <td style="width: 70%">
                                                    Whether NOC Obtained for Existing Usage of Groundwater :
                                                </td>
                                                <td>
                                                    <asp:Label ID="ddlNOCObtainedForExistINDRe" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="RowDateOfCommencementRe" visible="false">
                                                <td style="width: 70%">
                                                    Date of Commencement :
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtDateOfCommencementRe" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="RowDateOfExpansionRe" visible="false">
                                                <td style="width: 70%">
                                                    Date of Expansion :
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtDateOfExpansionRe" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="RowDateOfNotificationRe" visible="false">
                                                <td style="width: 70%">
                                                    Date of Notification :
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtDateOfNotificationRe" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 15px">
                                    <b>Eligibility for applying for NOC<span style="color: Red"><sup>*</sup></span>:</b>
                                </td>
                                <td style="font-size: 15px">
                                    <asp:Label ID="lblFinal" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblReason" runat="server" Font-Italic="True" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    
                </asp:Panel>
                <asp:Panel ID="Panel6" runat="server" Visible="false">
                        <table width="100%" class="SubFormWOBG">
                            <tr>
                                <td colspan="2">
                                    <a style="text-decoration: none; font-size: 14px; color: Red;" href="../../LandingPage/LatestUpdate/MessageCGWA19Nov2018.pdf#ZOOM=100"
                                        target="_blank">
                                        <p>
                                            See also:</p>
                                        <br />NOC for groundwater extraction shall not be granted to existing/ new industries/
                                    infrastructure/ mining projects except for drinking/domestic use and/or green belts
                                    in Over-exploited and Critical blocks in Segment B. Phase-I of Ganga Basin from
                                    Haridwar to Unnao with immediate effect
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
            </td>
        </tr>
       
    </table>
</asp:Content>
