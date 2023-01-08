<%@ Page Title="NOCAP- External User Login" Language="C#" AutoEventWireup="true"
    CodeFile="ExternalLogin.aspx.cs" Inherits="ExternalLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        
        .style2
        {
            width: 30%;
        }
        .style3
        {
            width: 100%;
        }
    </style>
    <link type="text/css" rel="Stylesheet" href="css/LoginStyle.css" />
    <script type="text/javascript" src="Scripts/jquery-3.6.0.min.js"></script>
    <script type="text/javascript" src="Scripts/WaterMark.min.js"></script>
    <script src="Scripts/MD5-login.js" type="text/javascript"></script>
     <script src="Scripts/Sha512.js" type="text/javascript"></script>
    <script type="text/javascript">
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
        $(function () {
            $("[id*=txtUserName], [id*=txtUserPassword],[id*=txtCaptchaCode]").WaterMark();
        });

    </script>
    <script type="text/javascript">

        function combineMD5Sha512() {
            md5auth();
            sha512auth();
            clearTxtPwd();
        }
        function sha512auth() {
            var seed = '<%= Session["rno"]%>';
            //alert(seed);
            var password = document.getElementById('<%= txtUserPassword.ClientID %>').value;
            if (password != "") {
                var hash = sha512(seed + sha512(password));
             
                document.getElementById('<%= hdnResultValuesha.ClientID %>').value = hash;
                password = "";
                //clearTxtPwd();
                return true;
            }
            else {
                clearTxtPwd();
                alert('Please enter valid user id/password');
                return false;
            }
        }

        function md5auth() {
            var seed = '<%= Session["rno"]%>';
            //alert(seed);
            var password = document.getElementById('<%= txtUserPassword.ClientID %>').value;
            if (password != "") {
                var hash = MD5(seed + MD5(password));
                document.getElementById('<%= hdnResultValue.ClientID %>').value = hash;
                password = "";
                //clearTxtPwd();
                return true;
            }
            else {
                clearTxtPwd();
                alert('Please enter valid user id/password');
                return false;
            }
        }
        function clearTxtPwd() {
            document.getElementById('<%= txtUserPassword.ClientID %>').value = "";

        }
        function callButtonEvent(objTextBox, objBtnID) {
            if (window.event.keyCode == 13) {
                document.getElementById(objBtnID).focus();
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <asp:HiddenField ID="hdnResultValue" Value="0" runat="server" />
         <asp:HiddenField ID="hdnResultValuesha" Value="0" runat="server" />
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
                            <strong>Application for Issue of NOC to Abstract Ground Water (NOCAP)</strong></div>
                    </div>
                </td>
                <td align="right">
                    <div class="div_header_right">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/logocgwb.png" />
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <%--<div class="div-title">
                Applications for the Issue of NOC to Abstract Ground Water (NOCAP)</div>
        </div>--%>
        <br />
        <br />
        <br />
        <table align="center" cellpadding="0" cellspacing="0" class="style2">
            <tr>
                <td>
                    <div>
                        <table align="center" cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td colspan="2">
                                    <div class="login_header" style="width: 100%; padding-left: 0px">
                                        <table class="style3">
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp; Applicant Login
                                                </td>
                                                <td align="center">
                                                    <a runat="server" id="lnkGoHome" href="~/">
                                                        <asp:Image ID="Image3" BorderWidth="0" runat="server" ImageUrl="~/Images/ico_home.png" />
                                                    </a>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <div class="formbody_login">
                            <table align="center" cellpadding="0" cellspacing="0" class="style1">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp; User Name:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUserName" runat="server" ToolTip="Enter User Name" MaxLength="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp; Password:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUserPassword" runat="server" TextMode="Password" ToolTip="Enter Password"
                                            MaxLength="300"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp; Captcha Code:
                                    </td>
                                    <td>
                                        <img src="Handler.ashx" />&nbsp;
                                        <asp:ImageButton ID="imgBtnCaptchaRefresh" runat="server" Height="24px" ImageUrl="~/Images/refresh.png"
                                            Width="29px" OnClick="imgBtnCaptchaRefresh_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp; Enter Code:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCaptchaCode" runat="server" ToolTip="Enter Code" MaxLength="6"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="btnLogin" runat="server" ValidationGroup="ExternalLogin" Text="LogIn"
                                            Height="37px" Width="72px" OnClick="btnLogin_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:LinkButton ID="lbnNewYserRegi" CssClass="Web_link" runat="server" OnClick="lbnNewYserRegi_Click">New user Register</asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="lbtnForgetLoginId" CssClass="Web_link" runat="server" OnClick="lbtnForgetLoginId_Click">Forgot User Name?</asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="lbtnForgetPassword" CssClass="Web_link" runat="server"
                                            OnClick="lbtnForgetPassword_Click">Forgot Password?</asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="LinkButton3" CssClass="Web_link" runat="server" OnClick="LinkButton3_Click">Help</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
    </form>
</body>
</html>
