<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    AutoEventWireup="true" CodeFile="ViewPayment.aspx.cs" Inherits="ExternalUser_Payment_ViewPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <table align="center" class="SubFormWOBG" style="line-height: 25px" width="90%">
        <tr>
            <td colspan="2">
                <div class="div_IndAppHeading" style="padding-left: 10px; font-size: 18px; height: 25px">
                    <b>
                        <asp:Label runat="server" ID="lblAppName"></asp:Label>:Payment</b>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                Application Type :
            </td>
            <td>
                <asp:Label runat="server" ID="lblAppType"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Application Purpose :
            </td>
            <td>
                <asp:Label runat="server" ID="lblAppPurpose"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Application Status :
            </td>
            <td>
                <asp:Label runat="server" ID="lblAppStatus"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Payment Required Amount :
            </td>
            <td>
                <asp:Label runat="server" ID="lblPaymentReqAmt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Submitted Type :
            </td>
            <td>
                <asp:Label runat="server" ID="lblSubmittedType"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Submitted Date :
            </td>
            <td>
                <asp:Label runat="server" ID="lblSubmittedDate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Payment Mode:
            </td>
            <td>
                <asp:Label runat="server" ID="lblPaymentMode"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Payment Status
            </td>
            <td>
                <asp:Label runat="server" ID="lblPaymentStatus"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
