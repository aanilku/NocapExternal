<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="RequestForNOCLinkApply.aspx.cs"
    Inherits="ExternalUser_RequestForNOCLink_RequestForNOCLinkApply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../css/table.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <table class="SubFormWOBG" width="100%" style="line-height: 25px">
        <tr>
            <td colspan="3">
                <div class="div_IndAppHeading" style="padding-left: 10px; text-align:center">
                    Enroll Old NOC- Apply
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: right">
                (<span class="Coumpulsory">*</span>)- Mandatory Fields
            </td>
        </tr>
        <tr>
            <td style="width: 20px">
                <b>(i).</b>
            </td>
            <td colspan="2">
                <b>Already Issued NOC Information:</b>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="width: 36%">
                NOC No. : <span class="Coumpulsory">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtRequesteNOCNo" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtRequesteNOCNo" runat="server" ForeColor="Red"
                    Display="Dynamic" ValidationGroup="RequestForNOCLink" ControlToValidate="txtRequesteNOCNo">Required</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxtRequesteNOCNo" runat="server" ForeColor="Red"
                    Display="Dynamic" ControlToValidate="txtRequesteNOCNo" ValidationExpression="^([0-9]|[A-Za-z]|[(]|[)]|[/]|[-])*"
                    ErrorMessage="Invalid Characters"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="width: 35%">
                Project / Industry Name in NOC Issued : <span class="Coumpulsory">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtRequestedNOCIssueName" runat="server" MaxLength="100" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtRequestedNOCIssueName" runat="server" ForeColor="Red"
                    Display="Dynamic" ValidationGroup="RequestForNOCLink" ControlToValidate="txtRequestedNOCIssueName">Required</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxtRequestedNOCIssueName" runat="server" ForeColor="Red"
                    Display="Dynamic" ControlToValidate="txtRequestedNOCIssueName" ValidationGroup="RequestForNOCLink"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                Project / Industry Address in NOC Issued (Same as given in Issued NOC): <span class="Coumpulsory">
                    *</span>
            </td>
            <td>
                <asp:TextBox ID="txtRequestedNOCIssueAddressTyped" runat="server" TextMode="MultiLine"
                    MaxLength="300" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtRequestedNOCIssueAddressTyped" runat="server"
                    ForeColor="Red" ValidationGroup="RequestForNOCLink" ControlToValidate="txtRequestedNOCIssueAddressTyped"
                    Display="Dynamic">Required</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxtRequestedNOCIssueAddressTyped" runat="server"
                    ValidationGroup="RequestForNOCLink" Display="Dynamic" ForeColor="Red" ControlToValidate="txtRequestedNOCIssueAddressTyped"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                Description: <span class="Coumpulsory">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtRequestedDescription" runat="server" MaxLength="300" TextMode="MultiLine"
                    Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtRequestedDescription" runat="server" ForeColor="Red"
                    Display="Dynamic" ValidationGroup="RequestForNOCLink" ControlToValidate="txtRequestedDescription">Required</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxtRequestedDescription" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtRequestedDescription" ValidationGroup="RequestForNOCLink"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                <b>(ii).</b>
            </td>
            <td colspan="2">
                <b>Present Project / Industry Location:</b>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                State: <span class="Coumpulsory">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlPresLocState" runat="server" AutoPostBack="True" Width="200px"
                    OnSelectedIndexChanged="ddlPresLocState_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlPresLocState" runat="server" InitialValue=""
                    Display="Dynamic" ControlToValidate="ddlPresLocState" ForeColor="Red" ValidationGroup="RequestForNOCLink">Required</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                District: <span class="Coumpulsory">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlPresLocDistrict" runat="server" AutoPostBack="True" Width="200px"
                    OnSelectedIndexChanged="ddlPresLocDistrict_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlPresLocDistrict" runat="server" InitialValue=" "
                    Display="Dynamic" ControlToValidate="ddlPresLocDistrict" ForeColor="Red" ValidationGroup="RequestForNOCLink">Required</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                Sub-District: <span class="Coumpulsory">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlPresLocSubDistrict" runat="server" AutoPostBack="True" Width="200px"
                    OnSelectedIndexChanged="ddlPresLocSubDistrict_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlPresLocSubDistrict" runat="server" InitialValue=" "
                    Display="Dynamic" ControlToValidate="ddlPresLocSubDistrict" ForeColor="Red" ValidationGroup="RequestForNOCLink">Required</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                Village / Town: <span class="Coumpulsory">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlPresLocTownOrVillage" runat="server" Width="200px" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlPresLocTownOrVillage_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" InitialValue=""
                    Display="Dynamic" ControlToValidate="ddlPresLocTownOrVillage" ForeColor="Red"
                    ValidationGroup="RequestForNOCLink">Required</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="lblVillage" runat="server" Text="Village:<span class='Coumpulsory'>*</span>"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlPresLocVillage" runat="server" Width="200px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqValiPresLocVillage" runat="server" InitialValue=" "
                    Display="Dynamic" ControlToValidate="ddlPresLocVillage" ForeColor="Red" ValidationGroup="RequestForNOCLink">Required</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="lblTown" runat="server" Text="Town: <span class='Coumpulsory'>*</span>"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlPresLocTown" runat="server" Width="200px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqValiPresLocTown" runat="server" InitialValue=" "
                    Display="Dynamic" ControlToValidate="ddlPresLocTown" ForeColor="Red" ValidationGroup="RequestForNOCLink">Required</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <b>(iii).</b>
            </td>
            <td colspan="2">
                <b>Present Communication Address:</b>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="width: 35%">
                Address Line 1: <span class="Coumpulsory">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtCommAddress1" runat="server" MaxLength="100" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtCommAddress1" runat="server" ForeColor="Red"
                    Display="Dynamic" ValidationGroup="RequestForNOCLink" ControlToValidate="txtCommAddress1">Required</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxtCommAddress1" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtCommAddress1" ValidationGroup="RequestForNOCLink"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                Address Line 2:
            </td>
            <td>
                <asp:TextBox ID="txtCommAddress2" runat="server" MaxLength="100" Width="99%"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revtxtCommAddress2" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtCommAddress2" ValidationGroup="RequestForNOCLink"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                Address Line 3:
            </td>
            <td>
                <asp:TextBox ID="txtCommAddress3" runat="server" MaxLength="100" Width="99%"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revtxtCommAddress3" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtCommAddress3" ValidationGroup="RequestForNOCLink"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                State: <span class="Coumpulsory">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlCommState" runat="server" AutoPostBack="True" Width="200px"
                    OnSelectedIndexChanged="ddlCommState_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlCommState" runat="server" InitialValue=" "
                    Display="Dynamic" ControlToValidate="ddlCommState" ForeColor="Red" ValidationGroup="RequestForNOCLink">Required</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                District: <span class="Coumpulsory">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlCommDistrict" runat="server" AutoPostBack="True" Width="200px"
                    OnSelectedIndexChanged="ddlCommDistrict_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlCommDistrict" runat="server" InitialValue=" "
                    Display="Dynamic" ControlToValidate="ddlCommDistrict" ForeColor="Red" ValidationGroup="RequestForNOCLink">Required</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                Sub-District:
            </td>
            <td>
                <asp:DropDownList ID="ddlCommSubDistrict" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                Pincode: <span class="Coumpulsory">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtPinCode" runat="server" MaxLength="6" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtPinCode" runat="server" ForeColor="Red" Display="Dynamic"
                    ValidationGroup="RequestForNOCLink" ControlToValidate="txtPinCode">Required</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxtPinCode" runat="server" ForeColor="Red"
                    Display="Dynamic" ControlToValidate="txtPinCode" ValidationGroup="RequestForNOCLink"></asp:RegularExpressionValidator>
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
                    Enabled="false"></asp:TextBox>(ISD)
                <asp:TextBox ID="txtStdCode" runat="server" Width="34px" MaxLength="4"></asp:TextBox>(STD)
                <asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="10" Width="200px"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revtxtStdCode" runat="server" ValidationGroup="RequestForNOCLink"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtStdCode"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="revtxtPhoneNumber" runat="server" ValidationGroup="RequestForNOCLink"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtPhoneNumber"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                Mobile Number : <span class="Coumpulsory">*</span>
            </td>
            <td>
                +<asp:TextBox ID="txtMobileCountryCode" runat="server" Width="34px" MaxLength="2"
                    Text="91" Enabled="false"></asp:TextBox>(ISD)
                <asp:TextBox ID="txtMobileNumber" runat="server" MaxLength="10" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtMobileNumber" runat="server" ForeColor="Red"
                    Display="Dynamic" ValidationGroup="RequestForNOCLink" ControlToValidate="txtMobileNumber"
                    ErrorMessage="Required"></asp:RequiredFieldValidator><br />
                <asp:RegularExpressionValidator ID="revtxtMobileNumber" runat="server" ValidationGroup="RequestForNOCLink"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtMobileNumber"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                E-Mail: <span class="Coumpulsory">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtEmail" runat="server" ForeColor="Red" Display="Dynamic"
                    ValidationGroup="RequestForNOCLink" ControlToValidate="txtEmail">Required</asp:RequiredFieldValidator><br />
                <asp:RegularExpressionValidator ID="revtxtEmail" ControlToValidate="txtEmail" Display="Dynamic"
                    ForeColor="Red" ValidationGroup="RequestForNOCLink" runat="server" />
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
                    Enabled="false"></asp:TextBox>(ISD)
                <asp:TextBox ID="txtStdCodeFax" runat="server" Width="34px" MaxLength="4"></asp:TextBox>(STD)
                <asp:TextBox ID="txtFaxNumber" runat="server" MaxLength="10" Width="200px"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revtxtStdCodeFax" runat="server" ValidationGroup="RequestForNOCLink"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtStdCodeFax"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="revtxtFaxNumber" runat="server" ValidationGroup="RequestForNOCLink"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtFaxNumber"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                NOC Attachment:
            </td>
            <td>
                <asp:FileUpload ID="FileUploadNOCAttachment" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center">
                <asp:Button ID="btnSubmit" runat="server" CausesValidation="true" ValidationGroup="RequestForNOCLink"
                    Text="Submit" Style="height: 26px" OnClick="btnSubmit_Click" />
                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
