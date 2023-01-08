<%@ Page Title="NOCAP- New User Registration" Language="C#" MasterPageFile="~/Sub/ApplicantRegi/ApplicantRegistrationMaster.master"
    AutoEventWireup="true" ValidateRequest="false" EnableEventValidation="false"
    MaintainScrollPositionOnPostback="true" CodeFile="ApplicantRegistration.aspx.cs"
    Inherits="Sub_ApplicantRegi_ApplicantRegistration" %>

<%--<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../Scripts/Sha512.js" type="text/javascript"></script>

    <link href="../../css/PasswordStrength.css" rel="stylesheet" type="text/css" />
    <link href="../../css/table.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function passwordStrength(password) {
            var desc = new Array();
            desc[0] = "Very Weak";
            desc[1] = "Weak";
            desc[2] = "Better";
            desc[3] = "Medium";
            desc[4] = "Strong";
            desc[5] = "Strongest";

            var score = 0;
            if (password.length > 6) score++;
            //if password has both lower and uppercase characters give 1 point	
            if ((password.match(/[a-z]/)) && (password.match(/[A-Z]/))) score++;
            //if password has at least one number give 1 point
            if (password.match(/\d+/)) score++;
            //if password has at least one special caracther give 1 point
            if (password.match(/.[!,@,#,$,%,^,&,*,?,_,~,-,(,)]/)) score++;
            //if password bigger than 12 give another 1 point
            if (password.length > 12) score++;
            document.getElementById("passwordDescription").innerHTML = desc[score];
            document.getElementById("passwordStrength").className = "strength" + score;
        }
    </script>
    <script language="javascript" type="text/javascript">
        function SHA512auth() {
            var seed = '<%= Session["rno"]%>';
            if (document.getElementById('<%= txtPassword.ClientID %>') != null) {
                var password = document.getElementById('<%= txtPassword.ClientID %>').value;
                var ConfPwd = document.getElementById('<%= txtReEnterPassword.ClientID %>').value;
                if (password != "" && ConfPwd != "") {
                    var reg = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}/;
                    var chkpasswords = reg.test(password);
                    if (chkpasswords) {
                        if (password == ConfPwd) {
                            // var seed1 = seed;
                            var hash1 = sha512(password);
                            var hash = (hash1 + seed);
                            document.getElementById('<%= hdnPassResultValue.ClientID %>').value = hash;
                            password = "";
                            var hashNew = sha512(ConfPwd);
                            document.getElementById('<%= hdnConfResultValue.ClientID %>').value = hashNew;
                            ConfPwd = "";
                            ClearControl();
                            return true;
                        }
                        else {
                            ClearControl();
                            alert("Invalid password. Please confirm new password again!");
                            return false;
                        }
                    }
                    else {
                        ClearControl();
                        alert("New Password should have 8 characters or more with an Alphabet, Number and one of these Special Characters $ @ % & * !");

                        return false;
                    }
                }
                else {
                    ClearControl();
                    alert('Please enter valid Password and Confirm Password!');

                    return false;
                }

            }
            ClearControl();
        }


        function ClearControl() {
            if (document.getElementById('<%= txtPassword.ClientID %>') != null && document.getElementById('<%= txtReEnterPassword.ClientID %>') != null) {
                document.getElementById('<%= txtPassword.ClientID %>').value = "";
                document.getElementById('<%= txtReEnterPassword.ClientID %>').value = "";
            }
        }
    </script>

    <script type="text/javascript">

        //For Checking valid date at Client Side

        function ValidateDateOnClient(sender, args) {

            var dateString = document.getElementById(sender.controltovalidate).value;
            var regex = /(((0|1)[1-9]|1[0]|2[0]|2[1-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$/;

            if (regex.test(dateString)) {
                var parts = dateString.split("/");
                var dt = new Date(parts[1] + "/" + parts[0] + "/" + parts[2]);

                args.IsValid = (dt.getDate() == parts[0] && dt.getMonth() + 1 == parts[1] && dt.getFullYear() == parts[2]);


            }

            else {
                args.IsValid = false;


            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnPassResultValue" runat="server" />
    <asp:HiddenField ID="hdnConfResultValue" runat="server" />

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table class="SubFormWOBG" width="100%" style="line-height: 25px">
        <tr>
            <td colspan="2">
                <div class="div_IndAppHeading" style="padding-left: 10px; font-size: 18px;">
                    User Registration
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: right; font-style: italic">Fields marked with asterisk (<span class="Coumpulsory">*</span>) are Coumpulsory<br />
                Attachment size should be less then or equal 300KB
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="ApplicantInfo" runat="server">
                        <table width="100%" class="SubFormWOBG">
                            <tr>
                                <td>Title:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTitle" runat="server" Width="153px">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem>Mr</asp:ListItem>
                                        <asp:ListItem>Miss</asp:ListItem>
                                        <asp:ListItem>Mrs</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                        Display="Dynamic" ValidationGroup="UserRegistrationValGroup" InitialValue="" ControlToValidate="ddlTitle">Title Required</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>First Name:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="30"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                        ValidationGroup="UserRegistrationValGroup" ControlToValidate="txtFirstName" Display="Dynamic">Please enter First Name</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ControlToValidate="txtFirstName" ID="RegularExpressionValidator1" ValidationGroup="UserRegistrationValGroup"
                                        ValidationExpression="^[\s\S]{0,30}$" runat="server" ForeColor="Red" ErrorMessage="First Name cannot exceed 30 Characters"
                                        Display="Dynamic"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator25" runat="server" ValidationGroup="UserRegistrationValGroup"
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
                                        ForeColor="Red" ControlToValidate="txtLastName" ValidationExpression="^[\s\S]{0,30}$" ValidationGroup="UserRegistrationValGroup"
                                        ErrorMessage="Last Name cannot exceed 30 Characters"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator26" runat="server" ValidationGroup="UserRegistrationValGroup"
                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z]*"
                                        ErrorMessage="Invalid Characters"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>User Name:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUserName" runat="server" MaxLength="30"></asp:TextBox>
                                    <asp:LinkButton ID="lbtnCheckUserNameAvail" runat="server" OnClick="lbtnCheckUserNameAvail_Click">Check Availability</asp:LinkButton>
                                    <asp:Label ID="lblCheckUserNameAvailMsg" runat="server" ForeColor="Red"></asp:Label>
                                    <asp:RequiredFieldValidator ID="reqUserName" runat="server" ForeColor="Red" ValidationGroup="UserRegistrationValGroup"
                                        Display="Dynamic" ControlToValidate="txtUserName">User Name Required</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic" ValidationGroup="UserRegistrationValGroup"
                                        ForeColor="Red" ControlToValidate="txtUserName" ValidationExpression="^[\s\S]{0,30}$"
                                        ErrorMessage="User Name cannot exceed 30 Characters"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator35" runat="server" ValidationGroup="UserRegistrationValGroup"
                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtUserName" ValidationExpression="[A-Za-z0-9^]*"
                                        ErrorMessage="Invalid Characters"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Email Address:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Red"
                                        Display="Dynamic" ValidationGroup="UserRegistrationValGroup" ControlToValidate="txtEmail">Please enter Email</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="regEmail" ControlToValidate="txtEmail" Text="Invalid email"
                                        Display="Dynamic" ForeColor="Red" ValidationGroup="UserRegistrationValGroup"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="Dynamic"
                                        ForeColor="Red" ControlToValidate="txtEmail" ValidationExpression="^[\s\S]{0,50}$" ValidationGroup="UserRegistrationValGroup"
                                        ErrorMessage="Email cannot exceed 50 Characters"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Confirm Email:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtConfirmEmail" runat="server" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ForeColor="Red"
                                        Display="Dynamic" ValidationGroup="UserRegistrationValGroup" ControlToValidate="txtConfirmEmail">Please enter Confirm Email</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="compConfirmEmail" runat="server" ForeColor="Red" ControlToCompare="txtEmail"
                                        Display="Dynamic" ValidationGroup="UserRegistrationValGroup" ControlToValidate="txtConfirmEmail"
                                        ErrorMessage="Email does not match"></asp:CompareValidator>
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
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic" ValidationGroup="UserRegistrationValGroup"
                                        ForeColor="Red" ControlToValidate="txtAlternateEmail" ValidationExpression="^[\s\S]{0,50}$"
                                        ErrorMessage="Alternate Email cannot exceed 50 Characters"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Phone Number:(with STD code)
                                </td>
                                <td>+<asp:TextBox ID="txtCountryCode" runat="server" Width="34px" MaxLength="2"></asp:TextBox>&nbsp;(ISD)
                                    <asp:TextBox ID="txtStdCode" runat="server" Width="34px" MaxLength="4"></asp:TextBox>&nbsp;(STD)
                                    <asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="10"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server"
                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtCountryCode" ValidationExpression="^[\s\S]{0,2}$" ValidationGroup="UserRegistrationValGroup"
                                        ErrorMessage="Country Code cannot exceed 2 Characters"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server"
                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtStdCode" ValidationExpression="^[\s\S]{0,4}$" ValidationGroup="UserRegistrationValGroup"
                                        ErrorMessage="Please enter Std Code 4 Characters"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator22" runat="server"
                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtPhoneNumber" ValidationExpression="^[\s\S]{0,10}$" ValidationGroup="UserRegistrationValGroup"
                                        ErrorMessage="Phone Number cannot exceed 10 Characters"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator23" runat="server"
                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtCountryCode" ValidationExpression="^[0-9]+$" ValidationGroup="UserRegistrationValGroup"
                                        ErrorMessage="Please enter numeric values"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator24" runat="server"
                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtStdCode" ValidationExpression="^[0-9]+$" ValidationGroup="UserRegistrationValGroup"
                                        ErrorMessage="Please enter numeric values"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtPhoneNumber" ValidationExpression="^[0-9]+$" ValidationGroup="UserRegistrationValGroup"
                                        ErrorMessage="Please enter numeric values"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator28" runat="server"
                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtCountryCode" ValidationExpression="^[1-9]\d*$" ValidationGroup="UserRegistrationValGroup"
                                        ErrorMessage="Invalid Value in Country Code"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator29" runat="server"
                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtStdCode" ValidationExpression="^[1-9]\d*$" ValidationGroup="UserRegistrationValGroup"
                                        ErrorMessage="Invalid Value in Std Code"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator30" runat="server"
                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtPhoneNumber" ValidationExpression="^[1-9]\d*$" ValidationGroup="UserRegistrationValGroup"
                                        ErrorMessage="Invalid Value in Phone Number"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;Mobile Number:<span class="Coumpulsory">*</span>
                                </td>
                                <td>+<asp:TextBox ID="txtMobileCountryCode" runat="server" Width="34px" MaxLength="2" Enabled="false">91</asp:TextBox>(ISD)
                                    <asp:TextBox ID="txtMobileNumber" runat="server" MaxLength="10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ForeColor="Red"
                                        Display="Dynamic" ValidationGroup="UserRegistrationValGroup" ControlToValidate="txtMobileCountryCode">Please enter Country Code,</asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ForeColor="Red"
                                        Display="Dynamic" ValidationGroup="UserRegistrationValGroup" ControlToValidate="txtMobileNumber">Please enter Mobile Number</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" Display="Dynamic"
                                        ForeColor="Red" ControlToValidate="txtMobileCountryCode" ValidationExpression="^[\s\S]{0,2}$" ValidationGroup="UserRegistrationValGroup"
                                        ErrorMessage="Country Code of Mobile cannot exceed 2 Characters"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" Display="Dynamic" ValidationGroup="UserRegistrationValGroup"
                                        ForeColor="Red" ControlToValidate="txtMobileNumber" ValidationExpression="^[0-9]{10}$"
                                        ErrorMessage="Mobile Number is of 10 Digits Only"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ValidationGroup="UserRegistrationValGroup"
                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtMobileCountryCode" ValidationExpression="^[0-9]+$"
                                        ErrorMessage="Please enter numeric values"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server" ValidationGroup="UserRegistrationValGroup"
                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtMobileNumber" ValidationExpression="^[0-9]+$"
                                        ErrorMessage="Please enter numeric values"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator31" runat="server" ValidationGroup="UserRegistrationValGroup"
                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtMobileCountryCode" ValidationExpression="^[1-9]\d*$"
                                        ErrorMessage="Invalid Value in CountryCode"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator32" runat="server" ValidationGroup="UserRegistrationValGroup"
                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtMobileNumber" ValidationExpression="^\d{10}$"
                                        ErrorMessage="Invalid Value in Mobile Number"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Address Line 1:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAddressLine1" runat="server" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ForeColor="Red"
                                        Display="Dynamic" ValidationGroup="UserRegistrationValGroup" ControlToValidate="txtAddressLine1">Please enter Address Line 1</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" Display="Dynamic" ValidationGroup="UserRegistrationValGroup"
                                        ForeColor="Red" ControlToValidate="txtAddressLine1" ValidationExpression="^[\s\S]{0,100}$"
                                        ErrorMessage="Address Line 1 cannot exceed 100 Characters"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Address Line 2:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAddressLine2" runat="server" MaxLength="100"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ValidationGroup="UserRegistrationValGroup"
                                        ForeColor="Red" Display="Dynamic" ControlToValidate="txtAddressLine2" ValidationExpression="^[\s\S]{0,100}$"
                                        ErrorMessage="Address Line 2 cannot exceed 100 Characters"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Address Line 3:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAddressLine3" runat="server" MaxLength="100"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ValidationGroup="UserRegistrationValGroup"
                                        ForeColor="Red" Display="Dynamic" ControlToValidate="txtAddressLine3" ValidationExpression="^[\s\S]{0,100}$"
                                        ErrorMessage="Address Line 3 cannot exceed 100 Characters"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>State:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlState" runat="server" Width="153px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue=""
                                        Display="Dynamic" ForeColor="Red" ValidationGroup="UserRegistrationValGroup"
                                        ControlToValidate="ddlState">Required</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>District:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDistrict" runat="server" Width="153px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue=""
                                        Display="Dynamic" ForeColor="Red" ValidationGroup="UserRegistrationValGroup"
                                        ControlToValidate="ddlDistrict">Required</asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
                                </td>
                            </tr>
                            <tr>
                                <td>Sub-District
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSubDistrict" runat="server" Width="153px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Pin Code:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPinCode" runat="server" MaxLength="6"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ForeColor="Red"
                                        Display="Dynamic" ValidationGroup="UserRegistrationValGroup" ControlToValidate="txtPinCode">Please enter Pin Code</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ValidationGroup="UserRegistrationValGroup"
                                        ForeColor="Red" Display="Dynamic" ControlToValidate="txtPinCode" ValidationExpression="^[\s\S]{0,6}$"
                                        ErrorMessage="Pin Code cannot exceed 6 Characters"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator27" runat="server" ValidationGroup="UserRegistrationValGroup"
                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtPinCode" ValidationExpression="^[1-9][\d]{5}$"
                                        ErrorMessage="Invalid Value"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Date Of Birth:<span class="Coumpulsory">*</span>(dd/mm/yyyy)
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDOB" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtDOB_CalendarExtender" runat="server" Enabled="True"
                                        TargetControlID="txtDOB" PopupButtonID="imgbtnCalendar" Format="dd/MM/yyyy">
                                    </asp:CalendarExtender>
                                    <asp:ImageButton ID="imgbtnCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ForeColor="Red"
                                        Display="Dynamic" ValidationGroup="UserRegistrationValGroup" ControlToValidate="txtDOB">Please Enter Date of Birth</asp:RequiredFieldValidator>

                                    <asp:CustomValidator ID="CstmVtxtDOB" runat="server" Display="Dynamic"
                                        ClientValidationFunction="ValidateDateOnClient" OnServerValidate="ValidateDate" ControlToValidate="txtDOB"
                                        ErrorMessage="Invalid Date." ForeColor="Red" ValidationGroup="UserRegistrationValGroup" />


                                    <%--
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator33" runat="server" ValidationGroup="UserRegistrationValGroup"
                                        ForeColor="Red" ErrorMessage="Invalid Date Format" ValidationExpression="^(?:(?:(?:0?[1-9]|1\d|2[0-8])\/(?:0?[1-9]|1[0-2]))\/(?:(?:1[6-9]|[2-9]\d)\d{2}))$|^(?:(?:(?:31\/0?[13578]|1[02])|(?:(?:29|30)\/(?:0?[1,3-9]|1[0-2])))\/(?:(?:1[6-9]|[2-9]\d)\d{2}))$|^(?:29\/0?2\/(?:(?:(?:1[6-9]|[2-9]\d)(?:0[48]|[2468][048]|[13579][26]))))$"
                                        ControlToValidate="txtDOB" Display="Dynamic"></asp:RegularExpressionValidator>--%>

                                    <asp:RangeValidator ID="rvtxtDOB" runat="server" Type="Date" Display="Dynamic"
                                        ValidationGroup="UserRegistrationValGroup" ForeColor="Red" MinimumValue="01/01/1900"
                                        ControlToValidate="txtDOB" ErrorMessage=" Date of Birth should be grater than 01/01/1900 and less than or equal to current date."></asp:RangeValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="Coumpulsory">Gender:<span class="Coumpulsory">*</span>
                                </td>
                                <td class="Coumpulsory">
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
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ValidationGroup="UserRegistrationValGroup"
                                        ForeColor="Red" Display="Dynamic" ControlToValidate="txtUID" ValidationExpression="^[\s\S]{12,12}$"
                                        ErrorMessage="UID should have only 12 Characters"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator34" runat="server" ValidationGroup="UserRegistrationValGroup"
                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtUID" ValidationExpression="^[0-9]+$"
                                        ErrorMessage="Please enter numeric values"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>ID Proof Type:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlIDProofType" runat="server" Width="153px">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" InitialValue=""
                                        Display="Dynamic" ForeColor="Red" ValidationGroup="UserRegistrationValGroup"
                                        ControlToValidate="ddlIDProofType">Please select ID Proof Type</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>ID Proof Unique No.:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtIDProofUniqueNo" runat="server" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ForeColor="Red"
                                        Display="Dynamic" ValidationGroup="UserRegistrationValGroup" ControlToValidate="txtIDProofUniqueNo">Please enter ID Proof Unique No</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ValidationGroup="UserRegistrationValGroup"
                                        ForeColor="Red" Display="Dynamic" ControlToValidate="txtIDProofUniqueNo" ValidationExpression="^[\s\S]{0,50}$"
                                        ErrorMessage="ID Proof Unique No cannot exceed 50 Characters"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Attach ID Proof <span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUploadIDProof" runat="server" />
                                    <span style="font-style: italic; color: Red; padding-left: 10px">( only jpg/pdf files allowed. )</span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="lblUploadIDProofMessage" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <asp:Button ID="btnProceed" runat="server" Text="Proceed" CausesValidation="true"
                                        ValidationGroup="UserRegistrationValGroup" OnClick="btnProceed_Click1" />
                                    <%--<asp:Button ID="btnProfilereset" runat="server" Text="Reset" OnClick="btnReset_Click" />--%>
                                    <asp:Button ID="btnCancelProfile" runat="server" Text="Cancel" OnClick="btnReset_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="OTPPanel" runat="server">
                        <table width="100%" class="SubFormWOBG">
                            <tr>
                                <th colspan="2">Filled Details
                                </th>
                            </tr>
                            <tr>
                                <td>Title:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblddlTitle" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>First Name:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:Label ID="lbltxtFirstName" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Last Name:
                                </td>
                                <td>
                                    <asp:Label ID="lbltxtLastName" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>User Name:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:Label ID="lbltxtUserName" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Email Address:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:Label ID="lbltxtEmail" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Alternate Email:
                                </td>
                                <td>
                                    <asp:Label ID="lbltxtAlternateEmail" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Phone Number:(with STD code)
                                </td>
                                <td>
                                    <asp:Label ID="lbltxtPhoneNumber" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;Mobile Number:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:Label ID="lbltxtMobileNumber" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Address Line 1:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:Label ID="lbltxtAddressLine1" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Address Line 2:
                                </td>
                                <td>
                                    <asp:Label ID="lbltxtAddressLine2" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Address Line 3:
                                </td>
                                <td>
                                    <asp:Label ID="lbltxtAddressLine3" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>State:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblddlState" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>District:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblddlDistrict" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Sub-District
                                </td>
                                <td>
                                    <asp:Label ID="lblddlSubDistrict" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Pin Code:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:Label ID="lbltxtPinCode" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Date Of Birth:<span class="Coumpulsory">*</span>(dd/mm/yyyy)
                                </td>
                                <td>
                                    <asp:Label ID="lbltxtDOB" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Coumpulsory">Gender:<span class="Coumpulsory">*</span>
                                </td>
                                <td class="Coumpulsory">
                                    <asp:Label ID="lblddlGender" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>UID:
                                </td>
                                <td>
                                    <asp:Label ID="lbltxtUID" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>ID Proof Type:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblddlIDProofType" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>ID Proof Unique No.:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:Label ID="lbltxtIDProofUniqueNo" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Attached ID Proof
                                </td>
                                <td>
                                    <asp:Label ID="lblFileUploadIDProof" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th colspan="2">Please Enter the Following Details
                                </th>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <img src="../../Handler.ashx" />&nbsp;<%--<asp:LinkButton ID="lnkbtnRefresh" runat="server" OnClick="lnkbtnRefresh_Click">Refresh Captcha Image</asp:LinkButton>--%><asp:ImageButton ID="imgBtnCaptchaRefresh" runat="server" Height="24px" ImageUrl="~/Images/refresh.png"
                                        Width="29px" OnClick="imgBtnCaptchaRefresh_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>Enter Captcha Code:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCaptcha" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqCaptcha" runat="server" ForeColor="Red" ValidationGroup="UserRegistrationValGroup"
                                        Display="Dynamic" ControlToValidate="txtCaptcha">Captcha Required</asp:RequiredFieldValidator>
                                    <asp:Label ID="Label1" runat="server" Font-Names="Arial"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Enter OTP (Sent to your Mobile No)</td>
                                <td>
                                    <asp:TextBox ID="txtOTP" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredOTP" runat="server" ForeColor="Red" ValidationGroup="UserRegistrationValGroup"
                                        Display="Dynamic" ControlToValidate="txtOTP">Enter OTP</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorOTP" runat="server" Display="Dynamic" ValidationGroup="UserRegistrationValGroup"
                                        ForeColor="Red" ControlToValidate="txtOTP" ValidationExpression="^[0-9]{6}$"
                                        ErrorMessage="Enter 6 Digit OTP"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Password:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="50" Width="149px"
                                        onkeyup="passwordStrength(this.value)"></asp:TextBox>
                                    <a href="javascript:;" onclick="window.open('PasswordPolicy.htm','myWin', 'scrollbars=no,width=500,height=150');"
                                        title="Password Policy">Password Policy</a>
                                    <div id="passwordDescription"></div>
                                    <div id="passwordStrength" class="strength0"></div>
                                    <%-- <asp:RegularExpressionValidator display="Dynamic"  errormessage="Password must be 8-10 characters long with at least one numeric,</br>one upper case character and one special character." forecolor="Red" 
                                         ValidationExpression="(?=^.{8,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+}{":;'?/>.<,])(?!.*\s).*$" controltovalidate="txtPasswordWithSpecialCharacter" runat="server">
                                    --%>
                                   <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                        Display="Dynamic"
                                        ForeColor="Red" ControlToValidate="txtPassword"
                                     ErrorMessage="New Password should have 8 characters or more with an Alphabet, Number and Special Character($,@,%,&,*,!) each"
                                        ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$"
                                                        ></asp:RegularExpressionValidator>--%>

                                    <%--ValidationExpression="^[\s\S]{0,50}$"--%>
                                </td>
                            </tr>
                            <tr>
                                <td>Re- Enter Password:<span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtReEnterPassword" runat="server" TextMode="Password" MaxLength="50"
                                        Width="149px"></asp:TextBox>
                                    <asp:CompareValidator ID="comp" runat="server" ForeColor="Red" ControlToValidate="txtReEnterPassword"
                                        Display="Dynamic" ControlToCompare="txtPassword" ErrorMessage="Password does not match"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblEnterVerificationCodeMessage" runat="server" Font-Names="Arial"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CausesValidation="true"
                                        ValidationGroup="UserRegistrationValGroup" OnClick="btnSubmit_Click" />
                                    <%--<asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />--%>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnReset_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
    </table>
</asp:Content>
