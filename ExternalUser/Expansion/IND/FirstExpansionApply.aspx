<%@ Page Title="NOCAP-Industrial Application Expansion" Language="C#" MaintainScrollPositionOnPostback="true"
    MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true"
    CodeFile="FirstExpansionApply.aspx.cs" Inherits="ExternalUser_Expansion_IND_FirstExpansionApply" %>

<%--<%@ PreviousPageType VirtualPath="~/ExternalUser/ApplicantHome.aspx" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Scripts/ClientSideDateValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table align="center" class="SubFormWOBG" width="100%">
        <tr>
            
            <td>
                <table class="SubFormWOBG" width="100%" style="line-height: 25px">
                      <tr>
                            <td colspan="3">
                                <div class="div_IndAppHeading" style="padding-left: 10px">
                                    INDUSTRIAL EXPANSION USE: 1. General Information- Location Details
                                </div>
                            </td>
                        </tr>
                    <tr>
                        <td>Application No: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlApplicatonNumber" runat="server" AutoPostBack="true"
                                Width="200px" OnSelectedIndexChanged="ddlApplicatonNumber_SelectedIndexChanged">
                            </asp:DropDownList>
                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue=""
                    Display="Dynamic" ControlToValidate="ddlApplicatonNumber" ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>

                    <tr>

                        <td>Application Number<span class="Coumpulsory"></span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationNo" runat="server" MaxLength="100" Width="51%" Enabled="false"></asp:TextBox>

                        </td>
                    </tr>

                    <tr>

                        <td>Name Of Project<span class="Coumpulsory"></span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationName" runat="server" MaxLength="100" Width="51%" Enabled="false"></asp:TextBox>

                        </td>
                    </tr>

                    <tr>

                        <td>NOC Number<span class="Coumpulsory"></span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNocNo" runat="server" MaxLength="100" Width="51%" Enabled="false"></asp:TextBox>

                        </td>
                    </tr>
                     <tr>
                            <td colspan="3">
                                 <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                <asp:Label ID="lblPageTitleFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                                <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                                <asp:Label ID="lblIndustialApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            </td>
                        </tr>
                     <tr>
                        
                            <td colspan="3" style="text-align: center">                                
                                <asp:Button ID="BtnShowDetails" runat="server" Text="Apply" OnClick="btnShowDetails_Click" ValidationGroup=""
                                    Style="height: 26px" />
                            </td>
                        </tr>

                </table>

            </td>
        </tr>
    </table>
</asp:Content>
