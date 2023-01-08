<%@ Page Title="" Language="C#" MasterPageFile="~/Sub/SubReportMaster.master" 
    AutoEventWireup="true" CodeFile="GWChargesCalculation.aspx.cs" Inherits="Sub_Report_GW_Charges_Calculation_GWChargesCalculation" %>

<%@ Register Src="~/AscxControl/ExternalUserLeftMenu.ascx" TagName="ExternalUserLeftMenu"
    TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script type="text/javascript">
        function SetDecimalFormateFrom() {

            if (document.getElementById('<%= txtQty.ClientID %>').value != "") {
                var str = document.getElementById('<%= txtQty.ClientID %>').value.toString();
                if (str.indexOf(".") == -1) {
                    document.getElementById('<%= txtQty.ClientID %>').value = Number(document.getElementById('<%= txtQty.ClientID %>').value) + ".00";
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <table align="center" cellpadding="0" cellspacing="0" class="SubFormWOBG" width="100%">
        <tr>
            <td style="width: 200px">
                <uc1:ExternalUserLeftMenu ID="ExternalUserLeftMenu2" runat="server" />
            </td>
            <td>
                <div>
                    <table width="100%" class="SubFormWOBG" style="line-height: 25px">
                        <tr>
                            <th colspan="2">
                                <div class="div_IndAppHeading" style="padding-left: 10px; font-size: 18px;">
                                    Know Your Ground Water Abstraction / Restoration Charges
                                </div>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel2" GroupingText="<span style='font-weight:bold;font-size:17px'>Application Information</span>"
                                    runat="server">
                                    <table width="100%" class="SubFormWOBG">
                                        <tr>
                                            <td style="width: 50%">Application Type: <span class="Coumpulsory">*</span></td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlApplicationType" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddlApplicationType_SelectedIndexChanged"> 
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Domestic" Value="1"></asp:ListItem>
                                            <%--        <asp:ListItem Text="Packaged Drinking Water" Value="2"></asp:ListItem>--%>
                                                    <asp:ListItem Text="Industry" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Infrastructure" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Mining" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="BulkWater" Value="5"></asp:ListItem>

                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                    InitialValue="0"
                                                    Display="Dynamic" ControlToValidate="ddlApplicationType" ForeColor="Red"
                                                    ValidationGroup="KnowYourGW">Required</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="RowAppTypeCate">
                                            <td>Application Type Category:</td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlApplicationTypeCat" Width="250px" OnSelectedIndexChanged="ddlApplicationTypeCat_SelectedIndexChanged"></asp:DropDownList></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel3" GroupingText="<span style='font-weight:bold;font-size:17px'>Location Detail</span>"
                                    runat="server">
                                    <table width="100%" class="SubFormWOBG">
                                        <tr>
                                            <td style="width: 50%">State Name: <span class="Coumpulsory">*</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlState" runat="server" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue=""
                                                    Display="Dynamic" ControlToValidate="ddlState" ForeColor="Red"
                                                    ValidationGroup="KnowYourGW">Required</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td>District Name: <span class="Coumpulsory">*</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDistrict" runat="server" Width="250px" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" InitialValue=""
                                                    Display="Dynamic" ControlToValidate="ddlDistrict" ForeColor="Red"
                                                    ValidationGroup="KnowYourGW">Required</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Sub-District Name: <span class="Coumpulsory">*</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSubDistrict" runat="server" AutoPostBack="True"
                                                    Width="250px" OnSelectedIndexChanged="ddlSubDistrict_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" InitialValue=""
                                                    Display="Dynamic" ControlToValidate="ddlSubDistrict" ForeColor="Red"
                                                    ValidationGroup="KnowYourGW">Required</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <asp:Panel GroupingText="<span style='font-weight:bold;font-size:17px'>Quantity Detail</span>"
                                    runat="server">
                                    <table width="100%" class="SubFormWOBG">
                                        <tr>
                                            <td style="width: 50%">Number of operational days in one year:<span class="Coumpulsory">*</span></td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtDays"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ForeColor="Red" ErrorMessage="Required"
                                                    Display="Dynamic" ControlToValidate="txtDays" ValidationGroup="KnowYourGW"> 

                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator runat="server" ValidationGroup="KnowYourGW"
                                                    ForeColor="Red" ControlToValidate="txtDays" ValidationExpression="^\d+$"
                                                    ErrorMessage="Please Enter Numeric value Only."></asp:RegularExpressionValidator></td>
                                        </tr>
                                        <tr>

                                            <%--<td>Quantity(cum/day):<span class="Coumpulsory">*</span></td>--%>
                                            <td>Quantity<span class="Coumpulsory">*</span><asp:Label ID="lblCum" Width="153px" runat="server" Font-Bold="true"></asp:Label></td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtQty" onblur="SetDecimalFormateFrom();"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ForeColor="Red" ErrorMessage="Required"
                                                    Display="Dynamic" ControlToValidate="txtQty" ValidationGroup="KnowYourGW"> 

                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator runat="server" ForeColor="Red" Display="Dynamic"
                                                    ControlToValidate="txtQty" ErrorMessage="Invalid Value. Format should be XXXXXXXXX.XX"
                                                    ValidationGroup="KnowYourGW" ValidationExpression="^(\d*\.)?\d+$"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label>
                                <asp:Label ID="lblResult" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                <asp:Label runat="server" ID="lblAreaTypeCatCode" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center">
                                <asp:Button ID="btnShowRecord" runat="server" Text="Submit" OnClick="btnShowRecord_Click"
                                    ValidationGroup="KnowYourGW" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            
        </tr>
    </table>
</asp:Content>

