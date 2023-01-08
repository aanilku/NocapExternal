<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    AutoEventWireup="true" CodeFile="EditProfile.aspx.cs" MaintainScrollPositionOnPostback="true"
    Inherits="ExternalUser_UserManagement_EditProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../css/table.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <table class="SubFormWOBG" width="75%" align="center" style="line-height: 25px">
        <tr>
            <td colspan="2">
                <div class="div_IndAppHeading" style="padding-left: 10px; font-size: 18px;">
                    Update Profile
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: right">
                <div>Fields marked with asterisk (<span class="mandatory">*</span>) are mandatory</div>
            </td>
        </tr>
        <tr>
            <td>Title:<span class="mandatory">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlTitle" runat="server" Width="153px">
                    <asp:ListItem>--Select--</asp:ListItem>
                    <asp:ListItem>Mr</asp:ListItem>
                    <asp:ListItem>Miss</asp:ListItem>
                    <asp:ListItem>Mrs</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                    Display="Dynamic" ValidationGroup="UserRegistrationValGroup" InitialValue="--Select--"
                    ControlToValidate="ddlTitle">Title Required</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>First Name:<span class="mandatory">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtFirstName" runat="server" MaxLength="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                    ValidationGroup="UserRegistrationValGroup" ControlToValidate="txtFirstName" Display="Dynamic">Please enter First Name</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ControlToValidate="txtFirstName" ID="RegularExpressionValidator1"
                    ValidationExpression="^[\s\S]{0,30}$" runat="server" ForeColor="Red" ErrorMessage="First Name cannot exceed 30 Characters"
                    Display="Dynamic"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator25" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtFirstName" ValidationExpression="^[a-zA-Z]*"
                    ErrorMessage="Invalid Characters"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>Last Name:
            </td>
            <td>
                <asp:TextBox ID="txtLastName" runat="server" MaxLength="30"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtLastName" ValidationExpression="^[\s\S]{0,30}$"
                    ErrorMessage="Last Name cannot exceed 30 Characters"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator26" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z]*"
                    ErrorMessage="Invalid Characters"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>User Name:<span class="mandatory">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtUserName" runat="server" MaxLength="30"></asp:TextBox>
                <asp:Label ID="lblCheckUserNameAvailMsg" runat="server" ForeColor="Red"></asp:Label>
                <asp:RequiredFieldValidator ID="reqUserName" runat="server" ForeColor="Red" ValidationGroup="UserRegistrationValGroup"
                    Display="Dynamic" ControlToValidate="txtUserName">User Name Required</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtUserName" ValidationExpression="^[\s\S]{0,30}$"
                    ErrorMessage="User Name cannot exceed 30 Characters"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator35" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtUserName" ValidationExpression="[A-Za-z0-9^]*"
                    ErrorMessage="Invalid Characters"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>Email Address:<span class="mandatory">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red"
                    Display="Dynamic" ValidationGroup="UserRegistrationValGroup" ControlToValidate="txtEmail">Required</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxtEmail" ControlToValidate="txtEmail" Display="Dynamic"
                    ForeColor="Red" ValidationGroup="UserRegistrationValGroup" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Alternate Email:
            </td>
            <td>
                <asp:TextBox ID="txtAlternateEmail" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" ControlToValidate="txtAlternateEmail"
                    Text="Invalid email" Display="Dynamic" ForeColor="Red" ValidationGroup="UserRegistrationValGroup"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtAlternateEmail" ValidationExpression="^[\s\S]{0,50}$"
                    ErrorMessage="Alternate Email cannot exceed 50 Characters"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>Phone Number : (with STD code)
            </td>
            <td>+
                <asp:TextBox ID="txtCountryCode" runat="server" Width="34px" MaxLength="2" Text="91"
                    Enabled="false"></asp:TextBox>
                <asp:TextBox ID="txtStdCode" runat="server" Width="34px" MaxLength="4"></asp:TextBox>&nbsp;(STD)
                <asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtCountryCode" ValidationExpression="^[\s\S]{0,2}$"
                    ErrorMessage="Country Code cannot exceed 2 Characters"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtStdCode" ValidationExpression="^[\s\S]{0,4}$"
                    ErrorMessage="Please enter Std Code 4 Characters"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator22" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtPhoneNumber" ValidationExpression="^[\s\S]{0,10}$"
                    ErrorMessage="Phone Number cannot exceed 10 Characters"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator23" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtCountryCode" ValidationExpression="^[0-9]+$"
                    ErrorMessage="Please enter numeric values"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator24" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtStdCode" ValidationExpression="^[0-9]+$"
                    ErrorMessage="Please enter numeric values"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtPhoneNumber" ValidationExpression="^[0-9]+$"
                    ErrorMessage="Please enter numeric values"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator28" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtCountryCode" ValidationExpression="^[1-9]\d*$"
                    ErrorMessage="Invalid Value in Country Code"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator29" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtStdCode" ValidationExpression="^[1-9]\d*$"
                    ErrorMessage="Invalid Value in Std Code"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator30" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtPhoneNumber" ValidationExpression="^[1-9]\d*$"
                    ErrorMessage="Invalid Value in Phone Number"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>Mobile Number:<span class="mandatory">*</span>
            </td>
            <td>+
                <asp:TextBox ID="txtMobileCountryCode" runat="server" Width="34px" MaxLength="2"
                    Text="91" Enabled="false"></asp:TextBox>
                &nbsp;(ISD)
                <asp:TextBox ID="txtMobileNumber" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ForeColor="Red"
                    Display="Dynamic" ValidationGroup="UserRegistrationValGroup" ControlToValidate="txtMobileNumber"
                    ErrorMessage="Required"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxtMobileNumber" runat="server" ValidationGroup="UserRegistrationValGroup"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtMobileNumber"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>Address Line 1:<span class="mandatory">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtAddressLine1" runat="server" MaxLength="100" Width="450px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ForeColor="Red"
                    Display="Dynamic" ValidationGroup="UserRegistrationValGroup" ControlToValidate="txtAddressLine1">Please enter Address Line 1</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtAddressLine1" ValidationExpression="^[\s\S]{0,100}$"
                    ErrorMessage="Address Line 1 cannot exceed 100 Characters"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>Address Line 2:
            </td>
            <td>
                <asp:TextBox ID="txtAddressLine2" runat="server" MaxLength="100" Width="450px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                    ForeColor="Red" Display="Dynamic" ControlToValidate="txtAddressLine2" ValidationExpression="^[\s\S]{0,100}$"
                    ErrorMessage="Address Line 2 cannot exceed 100 Characters"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>Address Line 3:
            </td>
            <td>
                <asp:TextBox ID="txtAddressLine3" runat="server" MaxLength="100" Width="450px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                    ForeColor="Red" Display="Dynamic" ControlToValidate="txtAddressLine3" ValidationExpression="^[\s\S]{0,100}$"
                    ErrorMessage="Address Line 3 cannot exceed 100 Characters"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>State:<span class="mandatory">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlState" runat="server" Width="155px" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue=""
                    Display="Dynamic" ForeColor="Red" ValidationGroup="UserRegistrationValGroup"
                    ControlToValidate="ddlState">Required</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>District:<span class="mandatory">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlDistrict" runat="server" Width="153px" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue=""
                    Display="Dynamic" ForeColor="Red" ValidationGroup="UserRegistrationValGroup"
                    ControlToValidate="ddlDistrict">Required</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Sub-District
            </td>
            <td>
                <asp:DropDownList ID="ddlSubDistrict" runat="server" Width="153px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Pin Code:<span class="mandatory">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtPinCode" runat="server" MaxLength="6"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ForeColor="Red"
                    Display="Dynamic" ValidationGroup="UserRegistrationValGroup" ControlToValidate="txtPinCode">Please enter Pin Code</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                    ForeColor="Red" Display="Dynamic" ControlToValidate="txtPinCode" ValidationExpression="^[\s\S]{0,6}$"
                    ErrorMessage="Pin Code cannot exceed 6 Characters"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator27" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtPinCode" ValidationExpression="^[1-9][\d]{5}$"
                    ErrorMessage="Invalid Value"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>Date Of Birth:<span class="mandatory">*</span><i>(dd/mm/yyyy)</i>
            </td>
            <td>
                <asp:TextBox ID="txtDOB" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="txtDOB_CalendarExtender" runat="server" Enabled="True"
                    TargetControlID="txtDOB" PopupButtonID="imgbtnCalendar" Format="dd/MM/yyyy">
                </asp:CalendarExtender>
                <asp:ImageButton ID="imgbtnCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ForeColor="Red"
                    Display="Dynamic" ValidationGroup="UserRegistrationValGroup" ControlToValidate="txtDOB">Please Select Date of Birth</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator33" runat="server"
                    ForeColor="Red" ErrorMessage="Invalid Date Format" ValidationExpression="^(?:(?:(?:0?[1-9]|1\d|2[0-8])\/(?:0?[1-9]|1[0-2]))\/(?:(?:1[6-9]|[2-9]\d)\d{2}))$|^(?:(?:(?:31\/0?[13578]|1[02])|(?:(?:29|30)\/(?:0?[1,3-9]|1[0-2])))\/(?:(?:1[6-9]|[2-9]\d)\d{2}))$|^(?:29\/0?2\/(?:(?:(?:1[6-9]|[2-9]\d)(?:0[48]|[2468][048]|[13579][26]))))$"
                    ControlToValidate="txtDOB" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>Gender:<span class="mandatory">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlGender" runat="server" Width="153px">
                    <asp:ListItem>--Select--</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" InitialValue=""
                    Display="Dynamic" ControlToValidate="ddlGender" ValidationGroup="UserRegistrationValGroup"
                    ForeColor="Red">Please select Gender</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>UID:
            </td>
            <td>
                <asp:TextBox ID="txtUID" runat="server" MaxLength="12"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                    ForeColor="Red" Display="Dynamic" ControlToValidate="txtUID" ValidationExpression="^[\s\S]{12,12}$"
                    ErrorMessage="UID should have only 12 Characters"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator34" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtUID" ValidationExpression="^[0-9]+$"
                    ErrorMessage="Please enter numeric values"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>ID Proof Type:<span class="mandatory">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlIDProofType" runat="server" Width="153px">
                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" InitialValue=""
                    Display="Dynamic" ForeColor="Red" ValidationGroup="UserRegistrationValGroup"
                    ControlToValidate="ddlIDProofType">Please select ID Proof Type</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>ID Proof Unique No.:<span class="mandatory">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtIDProofUniqueNo" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ForeColor="Red"
                    Display="Dynamic" ValidationGroup="UserRegistrationValGroup" ControlToValidate="txtIDProofUniqueNo">Please enter ID Proof Unique No</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                    ForeColor="Red" Display="Dynamic" ControlToValidate="txtIDProofUniqueNo" ValidationExpression="^[\s\S]{0,50}$"
                    ErrorMessage="ID Proof Unique No cannot exceed 50 Characters"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>Attach ID Proof
            </td>
            <td>
                <asp:FileUpload ID="FileUploadIDAttachment" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvIDProof" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                    Width="100%" ShowHeaderWhenEmpty="true" OnRowDataBound="gvIDProof_RowDataBound"
                    OnRowCommand="gvIDProof_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="100px">
                            <ItemTemplate>
                                <span>
                                    <%#Container.DataItemIndex + 1 %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Attachment Type">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachmentTypeCode" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("IDProofTypeCode")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Attachment Name">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("IDProofAttName")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View File" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnViewFile" runat="server" CausesValidation="false" CommandName="View"
                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ExternalUserCode"))%>'>View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Records exist in ID Proof Attachment.
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center" colspan="2">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="UserRegistrationValGroup"
                    OnClick="btnSubmit_Click" />
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
