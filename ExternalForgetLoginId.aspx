<%@ Page Title="NOCAP- External User Login" Language="C#" AutoEventWireup="true"
    CodeFile="ExternalForgetLoginId.aspx.cs" Inherits="ExternalForgetLoginId" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password</title>
    <style type="text/css">
        #style1
        {
            width: 70%;
        }
        
        #style2
        {
            width: 30%;
        }
        
        #style3
        {
            width: 100%;
        }
        #btnChangePassword, input[type="submit"]
        {
            border-radius: 5px 5px 5px 5px;
            background-color: #094E7F;
            color: White;
            font-weight: bold;
        }
        .login_header
        {
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
    </style>
    <link href="css/LoginStyle.css" rel="stylesheet" type="text/css" />
    <link href="css/table.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-3.6.0.min.js" type="text/javascript"></script>
    <script src="Scripts/MD5-login.js" type="text/javascript"></script>
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
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
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
                        Department of Water Resources, River Development and Ganga Rejuvenation</div>
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
    <center>
        <%--<asp:HiddenField ID="hdnOldResultValue" runat="server" />--%>
        <asp:HiddenField ID="hdnNewResultValue" runat="server" />
        <asp:HiddenField ID="hdnConfResultValue" runat="server" />
        <div style="width: 500px; height: 100px; text-align: left;">
            <asp:ValidationSummary ID="ValidationSummary1" ForeColor="Red" ValidationGroup="ForgetLoginId"
                runat="server" />
        </div>
        <div class="login_header" style="width: 32%; margin-left: 0px;">
            <table width="100%">
                <tr>
                    <td align="left">
                        Forgot User Name - Applicant User
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
                    <td align="left" style="padding-left: 50px; direction: ltr;">
                        Mobile Number:<span class="Coumpulsory">*</span>
                    </td>
                    <td align="right" style="padding-right: 50px">
                        <asp:TextBox ID="txtMobileNo" runat="server" ToolTip="Enter User Name" MaxLength="10"
                            Width="163px" Style="margin-left: 0px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMobileNo"
                            Display="None" ValidationGroup="ForgetLoginId" ErrorMessage="Mobile Number Required"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revtxtMobileNo" runat="server" ControlToValidate="txtMobileNo"
                            Display="None" ValidationGroup="ForgetLoginId"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 50px; direction: ltr;">
                        Date Of Birth:<span class="Coumpulsory">*</span>(dd/mm/yyyy)
                    </td>
                    <td>
                        <asp:TextBox ID="txtDOB" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtDOB_CalendarExtender" runat="server" Enabled="True"
                            TargetControlID="txtDOB" PopupButtonID="imgbtnCalendar" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                        <asp:ImageButton ID="imgbtnCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ForeColor="Red"
                            ValidationGroup="ForgetLoginId" Display="None" ControlToValidate="txtDOB" ErrorMessage="Please Select Date of Birth"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator33" runat="server"
                            ControlToValidate="txtDOB" Display="None" ForeColor="Red" ErrorMessage="Invalid Date Format"
                            ValidationExpression="^(?:(?:(?:0?[1-9]|1\d|2[0-8])\/(?:0?[1-9]|1[0-2]))\/(?:(?:1[6-9]|[2-9]\d)\d{2}))$|^(?:(?:(?:31\/0?[13578]|1[02])|(?:(?:29|30)\/(?:0?[1,3-9]|1[0-2])))\/(?:(?:1[6-9]|[2-9]\d)\d{2}))$|^(?:29\/0?2\/(?:(?:(?:1[6-9]|[2-9]\d)(?:0[48]|[2468][048]|[13579][26]))))$"
                            ValidationGroup="ForgetLoginId"></asp:RegularExpressionValidator>
                        <asp:RangeValidator ID="rvDOB" runat="server" Type="Date" Display="None" ValidationGroup="ForgetLoginId"
                            ForeColor="Red" ControlToValidate="txtDOB" ErrorMessage="DOB should be grater than current date."></asp:RangeValidator>
                    </td>
                </tr>
                <tr style="height: 10px;">
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right" style="padding-right: 10px;">
                        <asp:Button ID="btnForgetLoginId" runat="server" Text="Send User Name" Height="28px"
                            ValidationGroup="ForgetLoginId" Width="135px" OnClick="btnForgetLoginId_Click" />
                        <asp:Button ID="btnGoHome" runat="server" Text="Go To Login" Height="28px" Width="100px"
                            OnClick="btnGoHome_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </center>
    </form>
</body>
</html>
