<%@ Page Title="" Language="C#" MasterPageFile="~/Sub/ApplicantRegistrationMaster.master"
    AutoEventWireup="true" CodeFile="DistrictOffLocation.aspx.cs" Inherits="Sub_DistrictOfficeLocator_DistrictOffLocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" value=""/>
    <table align="center" class="SubFormWOBG" style="line-height:25px" width="100%">
        <tr>
            <th colspan="2">
                <div class="div_IndAppHeading" style="padding-left:10px;font-size:18px;">
                    District Office Locator
                </div>
            </th>
        </tr>
        <tr>
            <td>
                State:
            </td>
            <td>
                <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="True" Width="153px"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" InitialValue=" "
                    Display="Dynamic" ControlToValidate="ddlState" ForeColor="Red" ValidationGroup="DistrictOfficeLocator">Please select State</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                District:
            </td>
            <td>
                <asp:DropDownList ID="ddlDistrict" runat="server" Width="153px" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" InitialValue=" "
                    Display="Dynamic" ControlToValidate="ddlDistrict" ForeColor="Red" ValidationGroup="DistrictOfficeLocator">Please select District</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Address:
            </td>
            <td>
                <asp:TextBox ID="txtAddress" runat="server" Width="372px" Height="185px" TextMode="MultiLine"
                    BorderStyle="Outset" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                District Commitee:
            </td>
            <td>
                <asp:TextBox ID="txtDistrictCommitee" border-style="none" runat="server" ReadOnly="True" BorderWidth="0px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
