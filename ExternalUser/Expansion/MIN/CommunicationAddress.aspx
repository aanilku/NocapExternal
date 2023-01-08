<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="CommunicationAddress.aspx.cs"
    Inherits="ExternalUser_Expansion_MIN_CommunicationAddress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                                            <li class="active">Communication Address</li>
                                            <li>Land Use Details</li>
                                            <li>Dewatering Existing Structure</li>
                                            <li>Dewatering Proposed Structure</li>
                                            <li>Utilization of pumped water</li>
                                            <li>Monitoring of groundwater regime</li>
                                            <li>Groundwater Abstraction Structure- Existing</li>
                                            <li>Groundwater Abstraction Structure- Proposed</li>
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
                        <td colspan="3">
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                MINING EXPANSION USE : Communication Address
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="3">
                            (<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-Upload
                            Attachments in <strong style="color: Red">Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">
                            (4).
                        </td>
                        <td colspan="2">
                            <b>Communication Address</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 35%">
                            Address Line 1: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddressLine1" runat="server" MaxLength="100" Width="99%" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="CommunicationAddress" ControlToValidate="txtAddressLine1">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtAddressLine1" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtAddressLine1"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            Address Line 2:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddressLine2" runat="server" MaxLength="100" Width="99%" Enabled="false"></asp:TextBox><br />
                            <asp:RegularExpressionValidator ID="revtxtAddressLine2" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtAddressLine2" ValidationGroup="CommunicationAddress"></asp:RegularExpressionValidator>
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
                            <asp:RegularExpressionValidator ID="revtxtAddressLine3" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtAddressLine3" ValidationGroup="CommunicationAddress"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            State: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                Width="200px" Enabled="false">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue=" "
                                Display="Dynamic" ControlToValidate="ddlState" ForeColor="Red" ValidationGroup="CommunicationAddress"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            District: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                AutoPostBack="True" Width="200px" Enabled="false">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue=" "
                                Display="Dynamic" ControlToValidate="ddlDistrict" ForeColor="Red" ValidationGroup="CommunicationAddress"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            Sub-District:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSubDistrict" runat="server" Width="200px" Enabled="false">
                            </asp:DropDownList>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            Pincode: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPinCode" runat="server" MaxLength="6" Width="200px" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="CommunicationAddress" ControlToValidate="txtPinCode">Required</asp:RequiredFieldValidator><br />
                            <asp:RegularExpressionValidator ID="revtxtPinCode" runat="server" ValidationGroup="CommunicationAddress"
                                ForeColor="Red" Display="Dynamic" ControlToValidate="txtPinCode"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            Phone Number with Area Code:
                        </td>
                        <td>
                            +<asp:TextBox ID="txtCountryCode" runat="server" Width="34px" MaxLength="2" Text="91"
                                Enabled="false"></asp:TextBox>&nbsp;(ISD)
                            <asp:TextBox ID="txtStdCode" runat="server" Width="34px" MaxLength="4" Enabled="false"></asp:TextBox>&nbsp;(STD)
                            <asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="10" Width="200px" Enabled="false"></asp:TextBox><br />
                            <asp:RegularExpressionValidator ID="revtxtStdCode" runat="server" ValidationGroup="CommunicationAddress"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtStdCode"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revtxtPhoneNumber" runat="server" ValidationGroup="CommunicationAddress"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtPhoneNumber"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            Mobile Number: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            +<asp:TextBox ID="txtMobileCountryCode" runat="server" Width="34px" MaxLength="2"
                                Text="91" Enabled="false"></asp:TextBox>&nbsp;(ISD)
                            <asp:TextBox ID="txtMobileNumber" runat="server" MaxLength="10" Width="200px" Enabled="false"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="CommunicationAddress" ControlToValidate="txtMobileNumber"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtMobileNumber" runat="server" ValidationGroup="CommunicationAddress"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtMobileNumber"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            Fax Number:
                        </td>
                        <td>
                            +<asp:TextBox ID="txtCountryCodeFax" runat="server" Width="34px" MaxLength="2" Text="91"
                                Enabled="false"></asp:TextBox>&nbsp;(ISD)
                            <asp:TextBox ID="txtStdCodeFax" runat="server" Width="34px" MaxLength="4" Enabled="false"></asp:TextBox>&nbsp;(STD)
                            <asp:TextBox ID="txtFaxNumber" runat="server" MaxLength="10" Width="200px" Enabled="false"></asp:TextBox><br />
                            <asp:RegularExpressionValidator ID="revtxtStdCodeFax" runat="server" ValidationGroup="CommunicationAddress"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtStdCodeFax"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revtxtFaxNumber" runat="server" ValidationGroup="CommunicationAddress"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtFaxNumber"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            E-Mail: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="200px" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Red"
                                Display="Dynamic" ValidationGroup="CommunicationAddress" ControlToValidate="txtEmail">Required</asp:RequiredFieldValidator><br />
                            <asp:RegularExpressionValidator ID="revtxtEmail" runat="server" Display="Dynamic"
                                ValidationGroup="CommunicationAddress" ForeColor="Red" ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
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
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" ValidationGroup="CommunicationAddress"
                                OnClick="btnSaveAsDraft_Click" />
                            <asp:Button ID="txtNext" runat="server" Text="Next >>" ValidationGroup="CommunicationAddress"
                                OnClick="txtNext_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
