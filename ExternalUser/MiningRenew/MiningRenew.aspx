<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="MiningRenew.aspx.cs"
    Inherits="ExternalUser_MiningRenew_MiningRenew" %>

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
                                            <li>Existing NOC Details</li>
                                            <li>Land Use Details</li>
                                            <li>Dewatering Existing Structure</li>
                                            <li>Dewatering Additional Structure</li>
                                            <li>Utilization of pumped water</li>
                                            <li>Monitoring of groundwater regime</li>
                                            <li>Groundwater Abstraction Structure- Existing</li>
                                            <li>Groundwater Abstraction Structure- Additional</li>
                                            <li>Compliance Conditions in the NOC</li>
                                            <li>Compliance Conditions in the NOC - Other</li>
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
                                RENEW - MINING USE: General Information- Location Details
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="3">(<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-Upload
                            Attachments in <strong style="color: Red">Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">(1).
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
                        <td style="width: 40%">Application Type Category / Type of Application: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlApplicationTypeCategory" runat="server" Width="200px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>(2).
                        </td>
                        <td>Name of Mine / Project: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:Label ID="lblNameOfMining" runat="server" MaxLength="100" Width="99%"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>(3).
                        </td>
                        <td colspan="2">Location Details of the Mining Unit.
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Address Line 1: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddressLine1" runat="server" MaxLength="100" Width="99%" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Address Line 2:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddressLine2" runat="server" MaxLength="100" Width="99%" Enabled="false"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Address Line 3:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddressLine3" runat="server" MaxLength="100" Width="99%" Enabled="false"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>State: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" Enabled="false"
                                Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>District: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDistrict" runat="server" Enabled="false" AutoPostBack="True"
                                Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Sub-District: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSubDistrict" runat="server" AutoPostBack="True" Width="200px"
                                Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Village / Town: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTownOrVillage" runat="server" Width="200px" Enabled="false"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="lblVillage" runat="server" Text="Village: <span class='Coumpulsory'>*</span>"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlVillage" runat="server" Width="200px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="lblTown" runat="server" Text="Town: <span class='Coumpulsory'>*</span>"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTown" runat="server" Width="200px" Enabled="false">
                            </asp:DropDownList>
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

                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Purpose of Renewal: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rbtnWhetherGroundWaterUtilization" runat="server" align="left"
                                RepeatDirection="Vertical">
                                <asp:ListItem Value="ExistingGW">Existing Ground Water</asp:ListItem>
                                <asp:ListItem Value="ExistingGWwithAdditionalGWRequirment">Existing with Additional Ground Water Requirment</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ForeColor="Red"
                                InitialValue="" Display="Dynamic" ValidationGroup="LocationDetails" ControlToValidate="rbtnWhetherGroundWaterUtilization">Required</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblPageTitleFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblModeFrom" runat="server" Visible="false" Enabled="False"></asp:Label>
                            <asp:Label ID="lblMiningApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="3">
                            <asp:Button ID="Check" runat="server" OnClick="Check_Click" Text="Check" Visible="false" />
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" OnClick="btnSaveAsDraft_Click"
                                ValidationGroup="LocationDetails" />
                            <asp:Button ID="btnNext" runat="server" Text="Next >>" OnClick="btnNext_Click" ValidationGroup="LocationDetails" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
