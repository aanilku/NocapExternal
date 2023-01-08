<%@ Page Title="" Language="C#" MasterPageFile="~/Sub/ApplicantRegi/ApplicantRegistrationMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="GWAbstractionCalculation.aspx.cs" 
    Inherits="Sub_GWAbstractionCharges_GWAbstractionCalculation" %>

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
                    Ground Water Abstraction / Restoration Charges Calculation
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
                        
                        
                                              
                    </table>
                </asp:Panel>
            </td>
        </tr>

        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel1" GroupingText="<span style='font-weight:bold;font-size:17px'>Domestic Water Detail</span>"
                    runat="server">
                    <table width="100%" class="SubFormWOBG">
                        <tr>
                            <td style="width: 50%">
                                Domestic Purpose:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlApplicationPurpose" runat="server" Width="250px" 
                                    AutoPostBack="true"> <%--OnSelectedIndexChanged="ddlApplicationPurpose_SelectedIndexChanged"--%>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" InitialValue=""
                                    Display="Dynamic" ControlToValidate="ddlApplicationPurpose" ForeColor="Red" ValidationGroup="CheckEligibility">Required</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table></asp:Panel></td>
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
                <asp:Panel ID="Panel4" GroupingText="<span style='font-weight:bold;font-size:17px'>Application Information</span>"
                    runat="server">
                    <table width="100%" class="SubFormWOBG">
                        <tr>
                            <td style="width: 50%">
                                Ground Water Requirement (m&sup3/day): <span class="Coumpulsory">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGroundWaterRequirementExist" runat="server" MaxLength="9" Width="97%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtGroundWaterRequirementExist" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtGroundWaterRequirementExist" ValidationGroup="WaterRequirementDetails"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revtxtGroundWaterRequirementExist" runat="server"
                                ForeColor="Red" Display="Dynamic" ControlToValidate="txtGroundWaterRequirementExist"
                                ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                           </td>
                        </tr>  
                        <tr>
                            <td>
                                Total Number of Days: <span class="Coumpulsory">
                                   *</span>
                             </td>
                             <td>
                                <asp:TextBox ID="txtSurfaceWaterRequirementExist" runat="server" MaxLength="9" Width="97%"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvtxtSurfaceWaterRequirementExist" runat="server" ForeColor="Red"
                                 Display="Dynamic" ControlToValidate="txtSurfaceWaterRequirementExist" ValidationGroup="WaterRequirementDetails"
                                 ErrorMessage="Required"></asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ID="revtxtSurfaceWaterRequirementExist" runat="server"
                                 ForeColor="Red" Display="Dynamic" ControlToValidate="txtSurfaceWaterRequirementExist"
                                 ValidationGroup="WaterRequirementDetails"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblAreaTypeCategory" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblCategorizationofAssessmentUnits" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Button ID="btnCheck" runat="server" Text="Check Amount" ValidationGroup="CheckEligibility"
                    OnClick="btnCheck_Click" />
                <%--<asp:Button ID="btnCancel" runat="server" Text="Reset" CausesValidation="false" OnClick="btnCancel_Click" />--%>
            </td>
        </tr>
    </table>
</asp:Content>


