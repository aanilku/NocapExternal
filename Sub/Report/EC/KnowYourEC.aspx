<%@ Page Title="" Language="C#" MasterPageFile="~/Sub/SubReportMaster.master" AutoEventWireup="true"
    CodeFile="KnowYourEC.aspx.cs" Inherits="Sub_Report_EC_KnowYourEC" %>

<%@ Register Src="~/AscxControl/ExternalUserLeftMenu.ascx" TagName="ExternalUserLeftMenu"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../../Styles/Calendar/jquery-ui-Calendar.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/Calendar/jquery-min-Calendar.js" type="text/javascript"></script>
    <script src="../../../Scripts/Calendar/jquery-ui.min-Calendar.js" type="text/javascript"></script>
    <script type="text/javascript">
        function SetDecimalFormateFrom() {

            if (document.getElementById('<%= txtQty.ClientID %>').value != "") {
                var str = document.getElementById('<%= txtQty.ClientID %>').value.toString();
                if (str.indexOf(".") == -1) {
                    document.getElementById('<%= txtQty.ClientID %>').value = Number(document.getElementById('<%= txtQty.ClientID %>').value) + ".00";
                }
            }
        }
        $(function () {
            $("[id*=ContentPlaceHolder1_txtGWWillegalFrom]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd/mm/yy',
                minDate: new Date('01/01/2015'),
                maxDate: new Date(),
                changeMonth: true,
                changeYear: true,
                showOn: 'both',
                buttonImage: '../../../Images/calendar.png'
            });
        });
        $(function () {
            $("[id*=ContentPlaceHolder1_txtGWWillegalTo]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd/mm/yy',
                minDate: new Date('01/01/2015'),
                maxDate: new Date(),
                changeMonth: true,
                changeYear: true,
                showOn: 'both',
                buttonImage: '../../../Images/calendar.png'
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                    Know Your Environmental Compensation (EC)
                                </div>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel2" GroupingText="<span style='font-weight:bold;font-size:17px'>Application Information</span>"
                                    runat="server">
                                    <table width="100%" class="SubFormWOBG">
                                        <tr>
                                            <td>Application Type: <span class="Coumpulsory">*</span></td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlApplicationType" AutoPostBack="true" OnSelectedIndexChanged="ddlApplicationType_SelectedIndexChanged">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Domestic" Value="1"></asp:ListItem>
                                                    <%--        <asp:ListItem Text="Packaged Drinking Water" Value="2"></asp:ListItem>--%>
                                                    <asp:ListItem Text="Industry" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Infrastructure" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Mining" Value="4"></asp:ListItem>

                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                    InitialValue="0"
                                                    Display="Dynamic" ControlToValidate="ddlApplicationType" ForeColor="Red"
                                                    ValidationGroup="KnowYourEC">Required</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="RowAppTypeCate">
                                            <td>Application Type Category:</td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlApplicationTypeCat" OnSelectedIndexChanged="ddlApplicationTypeCat_SelectedIndexChanged"></asp:DropDownList></td>
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
                                            <td>State Name: <span class="Coumpulsory">*</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" Width="153px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue=""
                                                    Display="Dynamic" ControlToValidate="ddlState" ForeColor="Red"
                                                    ValidationGroup="KnowYourEC">Required</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td>District Name: <span class="Coumpulsory">*</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                    AutoPostBack="True" Width="153px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" InitialValue=""
                                                    Display="Dynamic" ControlToValidate="ddlDistrict" ForeColor="Red"
                                                    ValidationGroup="KnowYourEC">Required</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Sub-District Name: <span class="Coumpulsory">*</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSubDistrict" runat="server" AutoPostBack="True"
                                                    Width="153px" OnSelectedIndexChanged="ddlSubDistrict_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" InitialValue=""
                                                    Display="Dynamic" ControlToValidate="ddlSubDistrict" ForeColor="Red"
                                                    ValidationGroup="KnowYourEC">Required</asp:RequiredFieldValidator>
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

                                            <%--<td style="width: 20%">

                                               Is Applicable?  <br /> <asp:CheckBox runat="server" ID="ECCheckBox" AutoPostBack="true"
                                                    OnCheckedChanged="ECCheckBox_CheckedChanged" />

                                            </td>--%>
                                            <td style="width: 40%">ILLEGAL GW ABSTRACTION (FROM)<br />
                                                <asp:TextBox ID="txtGWWillegalFrom" runat="server" MaxLength="10"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvGWWillegalFrom" runat="server" ErrorMessage="Required"
                                                    ForeColor="Red" ControlToValidate="txtGWWillegalFrom" ValidationGroup="KnowYourEC"
                                                    Display="Dynamic"></asp:RequiredFieldValidator>

                                            </td>
                                            <td style="width: 40%">(TO)
                                                <br />
                                                <asp:TextBox ID="txtGWWillegalTo" runat="server" MaxLength="10"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvGWWInvalidto" runat="server" ErrorMessage="Required"
                                                    ForeColor="Red" ControlToValidate="txtGWWillegalTo" ValidationGroup="KnowYourEC"
                                                    Display="Dynamic"></asp:RequiredFieldValidator>

                                            </td>
                                        </tr>
                                        <tr>

                                            <td>Quantity(cum/day):<span class="Coumpulsory">*</span></td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtQty" onblur="SetDecimalFormateFrom();"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ForeColor="Red" ErrorMessage="Required"
                                                    Display="Dynamic" ControlToValidate="txtQty" ValidationGroup="KnowYourEC"> 

                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator runat="server" ForeColor="Red" Display="Dynamic"
                                                    ControlToValidate="txtQty" ErrorMessage="Invalid Value. Format should be XXXXXXXXX.XX"
                                                    ValidationGroup="KnowYourEC" ValidationExpression="^(\d*\.)?\d+$"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>



                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label>
                                <asp:Label runat="server" ID="lblAreaTypeCatCode" Visible="false"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center">
                                <asp:Button ID="btnShowRecord" runat="server" Text="Submit" OnClick="btnShowRecord_Click"
                                    ValidationGroup="KnowYourEC" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

