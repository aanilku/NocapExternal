<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" CodeFile="PayOnBharatKosh.aspx.cs"
    Inherits="PayOnBharatKosh" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <table class="SubFormWOBG" style="line-height: 25px; width: 100%">
        <tr>
            <td colspan="2">
                <div class="div_IndAppHeading" style="padding-left: 10px; text-align: center">
                    Online Payment
                </div>
            </td>
        </tr>
        <tr>
            <td><strong>Application Code:
            </strong>
            </td>

            <td>
                <asp:Label runat="server" ID="lblApplicationCode"></asp:Label>
            </td>
        </tr>
        <tr>
            <td><strong>Application Name:
            </strong>
            </td>

            <td>
                <asp:Label runat="server" ID="lblProjectName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td><strong>Application Type:
            </strong>
            </td>

            <td>
                <asp:Label runat="server" ID="lblApplicationType"></asp:Label>
            </td>
        </tr>
        <tr>
            <td><strong>Application Purpose:
            </strong>
            </td>

            <td>
                <asp:Label runat="server" ID="lblAppPurpose"></asp:Label>
            </td>
        </tr>
        <tr>
            <td><strong>Application Number:
            </strong>
            </td>

            <td>
                <asp:Label runat="server" ID="lnkApplicationNumber"></asp:Label>
            </td>
        </tr>
        <tr>
            <td><strong>NOC Number:
            </strong>
            </td>

            <td>
                <asp:Label runat="server" ID="lblNOCNumber"></asp:Label>
            </td>
        </tr>
        <tr>
            <td><strong>NEFT/RTGS:
            </strong>
            </td>

            <td>
                <asp:RadioButtonList runat="server" ID="rdBtnNEFTRTGS" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                    <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td><strong>Payment Type:
            </strong>
            </td>

            <td>
                <asp:CheckBox runat="server" AutoPostBack="true" ID="chbPaymentType" OnCheckedChanged="chbPaymentType_CheckedChanged" Text="None" />
                <asp:CheckBoxList runat="server" ID="chblistPaymentType" RepeatDirection="Vertical" TextAlign="Right" 
                    AutoPostBack="true" OnSelectedIndexChanged="chblistPaymentType_SelectedIndexChanged"
                    ></asp:CheckBoxList>
                <%--    <asp:RequiredFieldValidator runat="server" ControlToValidate="chblistPaymentType"
                    InitialValue=""
                     ErrorMessage="Please select Payment" ValidationGroup="OnlinePayment"></asp:RequiredFieldValidator>--%>

            </td>
        </tr>
        <tr>
            <td><strong>Ground Water Restoration Charge:
            </strong>
            </td>

            <td>
                <asp:Label runat="server" ID="lblGWCharge"></asp:Label>

            </td>
        </tr>
        <tr>
            <td><strong>Ground Water Abstraction Charge:
            </strong>
            </td>

            <td>
                <asp:Label runat="server" ID="lblGWAbsCharge"></asp:Label>

            </td>
        </tr>

        <tr>
            <td><strong>Total Amount:
            </strong></td>
            <td>
                <asp:Label runat="server" ID="lblTotalAmt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Pay:</td>
            <td>
                <asp:Button runat="server" ID="PayBtn" Text="Pay" OnClick="PayBtn_Click" Enabled="false" />
                <asp:Button runat="server" Text="Cancel" ID="btnCancel" OnClick="btnCancel_Click" Visible="false" />

            </td>
        </tr>
       

    </table>
    <asp:HiddenField runat="server" ID="hdnBase64String" />
    <asp:Label ID="lblMessage" runat="server"></asp:Label>
    <asp:Label ID="lblPageTitleFrom" Visible="false" runat="server" Enabled="False"></asp:Label>

    <asp:Label ID="lblApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>

</asp:Content>

