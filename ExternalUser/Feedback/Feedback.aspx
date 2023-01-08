<%@ Page Title="NOCAP-Feedback" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    AutoEventWireup="true" CodeFile="Feedback.aspx.cs" Inherits="ExternalUser_Feedback_Feedback"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function GetData() {
            var txtFeedbackDescription = document.getElementById("<%= txtFeedbackDescription.ClientID %>")
            var txtFeedbackDescription_array = document.getElementById("FeedbackDescriptionRemCount").value.split(' ');
            document.getElementById('FeedbackDescriptionRemCount').value = '( ' + parseInt(txtFeedbackDescription_array[1] - txtFeedbackDescription.value.length) + ' Character Left )';
        }

        function CountCharacter(controlId, countControlId, maxCharlimit) {
            var charLeft = maxCharlimit - controlId.value.length;
            countControlId.value = '( ' + parseInt(maxCharlimit - controlId.value.length) + ' Character Left )';
            if (charLeft < 0) {
                countControlId.style.color = "Red";
            }
            else {
                countControlId.style.color = "Black";
            }
        }
    </script>
    <style type="text/css">
        .Star
        {
            background-image: url('../../Images/Star.gif');
            height: 17px;
            width: 17px;
        }
        .WaitingStar
        {
            background-image: url('../../Images/WaitingStar.gif');
            height: 17px;
            width: 17px;
        }
        .FilledStar
        {
            background-image: url('../../Images/FilledStar.gif');
            height: 17px;
            width: 17px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <table align="center" class="SubFormWOBG" style="line-height: 25px" width="100%">
        <tr>
            <td colspan="4">
                <div class="div_AreaType" style="padding-left: 10px; font-size: 18px; height: 25px;
                    text-align: center">
                    <b>Feedback Form</b>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; color: Red">
                (For any grievance / clarifications / query etc, may contact the concerned offices.
                Details of contacts are available in
                <asp:HyperLink ID="hlinkContactUs" runat="server" NavigateUrl="~/LandingPage/ContactUs.htm"
                    Target="_blank">Contact Us</asp:HyperLink>
                &nbsp;Page.)
            </td>
        </tr>
        <tr>
            <td width="15%">
                Name:
            </td>
            <td width="35%">
                <asp:Label ID="lblUserName" runat="server"></asp:Label>
            </td>
            <td width="15%">
                Login Name:
            </td>
            <td width="35%">
                <asp:Label ID="lblLoginName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Mobile Number:
            </td>
            <td>
                <asp:Label ID="lblMobileNumber" runat="server"></asp:Label>
            </td>
            <td>
                Email ID:
            </td>
            <td>
                <asp:Label ID="lblEmailID" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Application Number:
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlApplicationNumber" runat="server" Width="250px" ValidationGroup="Feedback"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlApplicationNumber_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                    InitialValue="" ForeColor="Red" ValidationGroup="Feedback" ControlToValidate="ddlApplicationNumber"
                    ErrorMessage="Required"> </asp:RequiredFieldValidator>
                &nbsp;&nbsp;
                <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                &nbsp;&nbsp;
                <asp:Label ID="lblApplicationStatus" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Feedback:
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtFeedbackDescription" TextMode="MultiLine" Columns="75" Rows="8"
                    onkeyup="CountCharacter(this, this.form.FeedbackDescriptionRemCount, 1000);"
                    MaxLength="1000" onkeydown="CountCharacter(this, this.form.FeedbackDescriptionRemCount, 1000);"
                    runat="server" ValidationGroup="Feedback"></asp:TextBox><br />
                <input type="text" id="FeedbackDescriptionRemCount" tabindex="-1" style="border-width: 0px;
                    width: 110px; font-size: 10px; margin-left: 510px; text-align: left; background-color: transparent"
                    name="FeedbackDescriptionRemCount" size="2" maxlength="2" value="( 1000 Character Left )"
                    readonly="readonly" /><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                    ForeColor="Red" ValidationGroup="Feedback" ControlToValidate="txtFeedbackDescription"
                    ErrorMessage="Feedback - Feedback Description required"> </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxtFeedbackDescription" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtFeedbackDescription" ValidationGroup="Feedback"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="revLengthtxtFeedbackDescription" runat="server"
                    Display="Dynamic" ForeColor="Red" ValidationGroup="Feedback" ControlToValidate="txtFeedbackDescription"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                Rate NOCAP:
            </td>
            <td colspan="3">
                <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </cc1:ToolkitScriptManager>
                <cc1:Rating ID="NOCAPRating" AutoPostBack="true" runat="server" StarCssClass="Star"
                    WaitingStarCssClass="FilledStar" EmptyStarCssClass="Star" FilledStarCssClass="FilledStar">
                </cc1:Rating>
                <br />
                <asp:Label ID="lblRatingStatus" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align:right">
                <asp:LinkButton ID="lnkbtnGivenFeedback" runat="server" OnClick="lnkbtnGivenFeedback_Click">Given Feedback</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="Feedback" Text="Submit"
                    OnClick="btnSubmit_Click" />
                &nbsp;
                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClientClick="return confirm('If you have not saved data, Your data will be lost after Reset.');"
                    OnClick="btnReset_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
