<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    AutoEventWireup="true" CodeFile="SuccessPage.aspx.cs" Inherits="ExternalUser_UserManagement_SuccessPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="SubFormWOBG" width="100%" align="center" style="line-height: 25px">
        <tr>
            <td>
                <asp:Label ID="lblMsg" runat="server" ForeColor="Green" Text="Password Changed Successfully"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
