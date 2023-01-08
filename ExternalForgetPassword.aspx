<%@ Page Title="NOCAP- External User Login" Language="C#" AutoEventWireup="true"
    CodeFile="ExternalForgetPassword.aspx.cs" Inherits="ExternalLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password</title>
    <style type="text/css">
        #style1 {
            width: 70%;
        }

        #style2 {
            width: 30%;
        }

        #style3 {
            width: 100%;
        }

        #btnChangePassword, input[type="submit"] {
            border-radius: 5px 5px 5px 5px;
            background-color: #094E7F;
            color: White;
            font-weight: bold;
        }

        .login_header {
            /*background: url(../TestImages/header_bg.png) no-repeat top;*/
            background-color: #094E7F;
            height: 60px;
            width: 266px;
            line-height: 60px;
            padding-left: 20px;
            border-radius: 4px 4px 0px 0px;
            color: #fff;
            font-weight: bold;
            margin-left: 467px;
        }

        .style3 {
            height: 30px;
        }
    </style>
    <link type="text/css" rel="Stylesheet" href="css/LoginStyle.css" />
    <link href="css/PasswordStrength.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-3.6.0.min.js" type="text/javascript"></script>
    <script src="Scripts/MD5-login.js" type="text/javascript"></script>
    <script src="Scripts/Sha512.js" type="text/javascript"></script>
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
        function preventBack() {
            window.history.forward();
        }
        setTimeout("preventBack()", 0);
        window.onunload = function () {
            null
        };
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }

        window.onload = function () {
            document.onkeydown = function (event) {
                //return (e.which || e.keyCode) != 116;
                switch (event.keyCode) {
                    case 116: //F5 button
                        event.returnValue = false;
                        event.keyCode = 0;
                        return false;
                    case 82: //R button
                        if (event.ctrlKey) {
                            event.returnValue = false;
                            event.keyCode = 0;
                            return false;
                        }
                }
            };
        }
    </script>
    <script type="text/javascript">
        function sha512auth(seed) {

            var txtUserName = document.getElementById('<%= txtUserName.ClientID %>');
            if (txtUserName.value != "") {
                var txtOPT = document.getElementById('<%= txtOPT.ClientID %>');
                if (txtOPT.value != "") {
                    var NewPwd = document.getElementById('<%= txtNewPassword.ClientID %>').value;
                    var ConfPwd = document.getElementById('<%= txtConfirmPassword.ClientID %>').value;
                    if (NewPwd != "" && ConfPwd != "") {
                        var reg = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}/;
                        var chkpasswords = reg.test(NewPwd);
                        if (chkpasswords) {
                            if (NewPwd == ConfPwd) {
                                var seed1 = seed;

                                var hashNew1 = sha512(NewPwd);
                                var hashNew = (hashNew1 + seed1);

                                document.getElementById('<%= hdnNewResultValue.ClientID %>').value = hashNew;
                                NewPwd = "";

                                var hashConf1 = sha512(ConfPwd);
                                var hashConf = (hashConf1 + seed1);
                                document.getElementById('<%= hdnConfResultValue.ClientID %>').value = hashConf;
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
                 else {
                     ClearControl();
                     alert("Please enter valid OTP");
                     return false;

                 }
             }
             else {
                 ClearControl();
                 alert("Please enter valid User Name");
                 return false;

             }
         }
         function md5auth(seed) {

             var txtUserName = document.getElementById('<%= txtUserName.ClientID %>');
            if (txtUserName.value != "") {
                var txtOPT = document.getElementById('<%= txtOPT.ClientID %>');
                if (txtOPT.value != "") {
                    var NewPwd = document.getElementById('<%= txtNewPassword.ClientID %>').value;
                    var ConfPwd = document.getElementById('<%= txtConfirmPassword.ClientID %>').value;
                    if (NewPwd != "" && ConfPwd != "") {
                        var reg = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}/;
                        var chkpasswords = reg.test(NewPwd);
                        if (chkpasswords) {
                            if (NewPwd == ConfPwd) {
                                var seed1 = seed;

                                var hashNew1 = MD5(NewPwd);
                                var hashNew = (hashNew1 + seed1);

                                document.getElementById('<%= hdnNewResultValue.ClientID %>').value = hashNew;
                                NewPwd = "";

                                var hashConf1 = MD5(ConfPwd);
                                var hashConf = (hashConf1 + seed1);
                                document.getElementById('<%= hdnConfResultValue.ClientID %>').value = hashConf;
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
                 else {
                     ClearControl();
                     alert("Please enter valid OTP");
                     return false;

                 }
             }
             else {
                 ClearControl();
                 alert("Please enter valid User Name");
                 return false;

             }
         }



         function checkUserName() {
             var txtUserName = document.getElementById('<%= txtUserName.ClientID %>');
            if (txtUserName.value == "") {
                ClearControl();
                alert("Please enter valid User Name");
                return false;
            }
            else {
                return true;
            }
            ClearControl();
        }


        function ClearControl() {
            document.getElementById('<%= txtNewPassword.ClientID %>').value = "";
            document.getElementById('<%= txtConfirmPassword.ClientID %>').value = "";

        }

    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
        <table align="center" cellpadding="0" cellspacing="0" class="style1" bgcolor="#F0F0F0">
            <tr>
                <td>
                    <div class="div_header">
                        <div class="div_header_left">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/satyamevjayate_Logo.png"
                                Height="110px" Width="81px" />
                        </div>
                    </div>
                </td>
                <td>
                    <div class="logo_txt">
                        <div class="div_header_line1">
                            Government of India<br />
                            Ministry of Jal Shakti<br />
                            Department of Water Resources, River Development and Ganga Rejuvenation
                        </div>
                        <div class="div_header_line2">
                            Central Ground Water Authority (CGWA)
                        </div>
                        <br />
                        <div class="div_header_line3">
                            <strong>Application for Issue of NOC to Abstract Ground Water (NOCAP)</strong>
                        </div>
                    </div>
                </td>
                <td align="right">
                    <div class="div_header_right">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/logocgwb.png" />
                    </div>
                </td>
            </tr>
        </table>
        <center>
            <%--<asp:HiddenField ID="hdnOldResultValue" runat="server" />--%>
            <asp:HiddenField ID="hdnNewResultValue" runat="server" />
            <asp:HiddenField ID="hdnConfResultValue" runat="server" />
            <center>
                <div style="width: 500px; height: 100px;">
                    <asp:ValidationSummary ID="ValidationSummary1" ForeColor="Red" ValidationGroup="ResetPassword"
                        runat="server" />
                </div>
            </center>
            <div class="login_header" style="width: 32%; margin-left: 0px;">
                <table width="100%">
                    <tr>
                        <td align="left">Reset Password - Applicant User
                        </td>
                        <td>
                            <a id="lnkHome" runat="server" href="~/">
                                <asp:Image ID="Image3" BorderWidth="0" runat="server" ImageUrl="~/Images/ico_home.png"
                                    Height="16px" Width="20px" /></a>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="formbody_ForgetPassword" style="width: 34%; margin-top: 8px;">
                <table align="center" width="90%">
                    <tr>
                        <td colspan="2" style="height: 20px">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-left: 50px">User Name
                        </td>
                        <td align="right" style="padding-right: 50px">
                            <asp:TextBox ID="txtUserName" runat="server" ToolTip="Enter User Name" Width="163px"
                                Style="margin-left: 0px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-left: 50px">Enter OTP
                        </td>
                        <td align="right" style="padding-right: 50px">
                            <asp:TextBox ID="txtOPT" runat="server" Width="163px" MaxLength="6"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^[0-9]{6}$"
                                ControlToValidate="txtOPT" Display="None" ValidationGroup="ResetPassword" ErrorMessage="OTP Allow Only 6 Digit Numeric Values"></asp:RegularExpressionValidator>
                            <asp:LinkButton ID="lnkSendOTP" runat="server" OnClientClick="javascript:return checkUserName();"
                                OnClick="lnkSendOTP_Click">Send OTP</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 50px; text-align: left">New Password
                        </td>
                        <td align="right" style="padding-right: 50px">
                            <asp:TextBox ID="txtNewPassword" MaxLength="50" runat="server" TextMode="Password"
                                ToolTip="Enter New Password"
                                Width="163px" Style="margin-left: 0px"
                                 onkeyup="passwordStrength(this.value)"></asp:TextBox>
                            <a href="javascript:;" onclick="window.open('Sub/ApplicantRegi/PasswordPolicy.htm','myWin', 'scrollbars=no,width=500,height=150');"
                                title="Password Policy">Password Policy</a>
                            <div id="passwordDescription"></div>
                            <div id="passwordStrength" class="strength0"></div>
                          <%--  <asp:RegularExpressionValidator runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtNewPassword"     ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$"
                                ErrorMessage="New Password should have 8 characters or more with an Alphabet, Number and Special Character($,@,%,&,*,!) each"></asp:RegularExpressionValidator>--%>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="Left" style="width: 175px; padding-left: 50px">Re-Enter New Password
                        </td>
                        <td align="right" style="padding-right: 50px">
                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" ToolTip="Reneter New Password"
                                Width="163px" Style="margin-left: 0px"></asp:TextBox>
                            <asp:CompareValidator ID="valc_txtConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                                ValidationGroup="ResetPassword" Display="None" ControlToCompare="txtNewPassword"
                                Type="String" Operator="Equal" ErrorMessage="Invalid password. Please confirm new password again!"></asp:CompareValidator>
                        </td>
                    </tr>
                    <%--  <tr>
                                    <td >  
                                    <br />  
                                    </td>
                                    <td>
                                        
                                    </td>
                                </tr>--%>
                    <tr style="height: 10px;">
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" Height="28px"
                                ValidationGroup="ResetPassword" Width="135px" OnClick="btnResetPassword_Click" />
                            <asp:Button ID="btnGoHome" runat="server" Text="Go To Home" Height="28px" Width="100px"
                                OnClick="btnGoHome_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="style3">
                            <asp:RegularExpressionValidator ID="vallen_txtNewPassword" runat="server" ControlToValidate="txtNewPassword"
                                ValidationGroup="ResetPassword" Display="None" ErrorMessage="New Password should have 8 characters or more with an Alphabet, Number and Special Character($,@,%,&,*,!) each"
                                ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$" />
                            <%-- 
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor="Red" ValidationExpression="^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$" ControlToValidate="txtNewPassword" 
                                            ErrorMessage=" password length should be minimum 8 characters, at least one special character, one upper case, one lower case and one numeric character required"></asp:RegularExpressionValidator>--%>
                            <br />
                        </td>
                        <td class="style3"></td>
                    </tr>
                </table>
            </div>
        </center>
    </form>
</body>
</html>
