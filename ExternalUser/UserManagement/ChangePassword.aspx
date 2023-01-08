<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ExternalUser_UserManagement_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        function check() {
            if (document.getElementById("<%=txtCurrentPassword.ClientID %>").value == "")//|| !document.getElementById("txtNewPassword").value || !document.getElementById("txtCurrentPassword").value)
            {
                alert("Please enter your current password ");
                return (false);
            }
            else
                if (document.getElementById("<%=txtNewPassword.ClientID %>").value == "")//|| !document.getElementById("txtNewPassword").value || !document.getElementById("txtCurrentPassword").value)
                {
                    alert("Please enter your New password ");
                    return (false);
                }
                else
                    if (document.getElementById("<%=txtConfirmPassword.ClientID %>").value == "")//|| !document.getElementById("txtNewPassword").value || !document.getElementById("txtCurrentPassword").value)
                    {
                        alert("Please Re-enter your New password ");
                        return (false);
                    }

                    else
                        if (document.getElementById("<%=txtNewPassword.ClientID %>").value != document.getElementById("<%=txtConfirmPassword.ClientID %>").value) {
                            alert("New Password and Re-Enter New Password is not Same");
                            return (false);
                        }
            return (true);
        }

    </script>
    <link type="text/css" rel="Stylesheet" href="../../css/LoginStyle.css" />
    <script src="../../Scripts/jquery-3.6.0.min.js" type="text/javascript"></script>
    <script src="../../Scripts/MD5-login.js" type="text/javascript"></script>
    <script src="../../Scripts/Sha512.js" type="text/javascript"></script>
    <script type="text/javascript">
        function combineMD5Sha512(seed) {
            md5auth(seed);
            sha512auth(seed);
            ClearControl();
        }
        function md5auth(seed) {

            var password = document.getElementById('<%= txtCurrentPassword.ClientID %>').value;

            if (password != "") {
                var NewPwd = document.getElementById('<%= txtNewPassword.ClientID %>').value;
                var ConfPwd = document.getElementById('<%= txtConfirmPassword.ClientID %>').value;
                if (NewPwd != "" && ConfPwd != "") {
                    var reg = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}/;
                    var chkpasswords = reg.test(NewPwd);

                    if (chkpasswords) {
                        if (NewPwd == ConfPwd) {
                            var seed1 = seed;
                            var hash = MD5(seed1 + MD5(password));
                            document.getElementById('<%= hdnOldResultValue.ClientID %>').value = hash;
                             password = "";

                             var hashNew1 = sha512(NewPwd);
                             var hashNew = (hashNew1 + seed1);

                             document.getElementById('<%= hdnNewResultValue.ClientID %>').value = hashNew;
                            NewPwd = "";

                            var hashConf1 = sha512(ConfPwd);
                            var hashConf = (hashConf1 + seed1);
                            document.getElementById('<%= hdnConfResultValue.ClientID %>').value = hashConf;
                            ConfPwd = "";
                             //ClearControl();
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
            else {
                ClearControl();
                alert('Please enter valid password');
                return false;
            }
        }

        function sha512auth(seed) {

            var password = document.getElementById('<%= txtCurrentPassword.ClientID %>').value;

            if (password != "") {
                var NewPwd = document.getElementById('<%= txtNewPassword.ClientID %>').value;
                var ConfPwd = document.getElementById('<%= txtConfirmPassword.ClientID %>').value;
                if (NewPwd != "" && ConfPwd != "") {
                    var reg = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}/;
                    var chkpasswords = reg.test(NewPwd);

                    if (chkpasswords) {
                        if (NewPwd == ConfPwd) {
                            var seed1 = seed;
                            var hash = sha512(seed1 + sha512(password));
                            document.getElementById('<%= hdnBeforeOldResultValuesha512.ClientID %>').value = hash;
                            password = "";

                            var hashNew1 = sha512(NewPwd);
                            var hashNew = (hashNew1 + seed1);

                            document.getElementById('<%= hdnNewResultValue.ClientID %>').value = hashNew;
                            NewPwd = "";

                            var hashConf1 = sha512(ConfPwd);
                            var hashConf = (hashConf1 + seed1);
                            document.getElementById('<%= hdnConfResultValue.ClientID %>').value = hashConf;
                            ConfPwd = "";
                            //ClearControl();
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
            else {
                ClearControl();
                alert('Please enter valid password');
                return false;
            }
        }


        function ClearControl() {

            document.getElementById('<%= txtCurrentPassword.ClientID %>').value = "";
            document.getElementById('<%= txtNewPassword.ClientID %>').value = "";
            document.getElementById('<%= txtConfirmPassword.ClientID %>').value = "";

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnOldResultValue" runat="server" />
    <asp:HiddenField ID="hdnNewResultValue" runat="server" />
    <asp:HiddenField ID="hdnConfResultValue" runat="server" />
    <asp:HiddenField ID="hdnBeforeOldResultValuesha512" runat="server" />

    <table align="center" class="SubFormWOBG" width="500px" style="line-height: 35px; margin-top: 50px">
        <tr>
            <th colspan="2" style="height: 50px">
                <div class="div_IndAppHeading" style="font-size: 18px; height: 100%; padding-top: 8px">
                    Change Password - External User <a id="lnkHome" runat="server" href="~/ExternalUser/ApplicantHome.aspx">
                        <asp:Image ID="Image3" BorderWidth="0" runat="server" ImageUrl="~/Images/ico_home.png" /></a>
                </div>
            </th>
        </tr>
        <tr>
            <td colspan="2" style="text-align: right">Fields marked with asterisk (<span class="mandatory">*</span>) are mandatory
            </td>
        </tr>
        <tr>
            <td>Current Password <span class="Coumpulsory">*</span>
            </td>
            <td style="padding-top: 10px; padding-left: 10px">
                <asp:TextBox ID="txtCurrentPassword" runat="server" TextMode="Password" ToolTip="Enter Current Password"
                    Width="163px" Style="margin-left: 0px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>New Password <span class="Coumpulsory">*</span>
            </td>
            <td style="padding-top: 10px; padding-left: 10px">
                <asp:TextBox ID="txtNewPassword" runat="server" onkeyup="passwordStrength(this.value)" TextMode="Password" MaxLength="50" Width="149px"></asp:TextBox>
                <a href="javascript:;" onclick="window.open('../../Sub/ApplicantRegi/PasswordPolicy.htm','myWin', 'scrollbars=no,width=500,height=150');"
                    title="Password Policy">Password Policy</a>
                <div id="passwordDescription"></div>
                <div id="passwordStrength" class="strength0"></div>
               <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtNewPassword" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$"
                    ErrorMessage="New Password should have 8 characters or more with an Alphabet, Number and Special Character($,@,%,&,*,!) each"></asp:RegularExpressionValidator>
                
--%>


                <%--    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" ToolTip="Enter New Password"
                    Width="163px" Style="margin-left: 0px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="vallen_txtNewPassword" runat="server" ControlToValidate="txtNewPassword"
                    Display="Dynamic" ErrorMessage="New Password should have 8 characters or more with an Alphabet, Number and Special Character($,@,%,&,*,!) each"
                    ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$"
                    ForeColor="Red" Text="*" />--%>
            </td>
        </tr>
        <tr>
            <td>Re-Enter New Password <span class="Coumpulsory">*</span>
            </td>
            <td style="padding-top: 10px; padding-left: 10px">
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" ToolTip="Reneter New Password"
                    Width="163px" Style="margin-left: 0px"></asp:TextBox>
                <asp:CompareValidator ID="valc_txtConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                    Display="Dynamic" ControlToCompare="txtNewPassword" Type="String" Operator="Equal"
                    ErrorMessage="Invalid password. Please confirm new password again!" ForeColor="Red">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label><br />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" Height="28px"
                    Width="135px" OnClick="btnChangePassword_Click" />
                <asp:Button ID="btnGoHome" runat="server" Text="Go To Home" Enabled="false" Height="28px" Width="100px"
                    OnClick="btnGoHome_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
