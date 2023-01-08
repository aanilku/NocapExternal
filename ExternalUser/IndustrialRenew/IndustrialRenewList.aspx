<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="IndustrialRenewList.aspx.cs"
    Inherits="ExternalUser_IndustrialReNew_IndustrialRenewList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRFs" runat="server" Value="" />
    <table align="center" class="SubFormWOBG" width="100%" style="line-height: 20px">
        <tr>
            <td>
                <div class="div_IndAppHeading" style="padding-left: 10px; text-align: center; font-size: 18px">
                    Industrial Renew- List of Application Eligible for Renew
                </div>
            </td>
        </tr>
        <tr>
            <td style="font-size: 14px; font-weight: bold">
                On Click Apply it will get saved in Renew Save As Draft in "Applicant Home"
            </td>
        </tr>
        <tr>
            <td style="font-size: 14px; font-weight: bold">
                First Renewal
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvIndRenewEligibleApplication" ShowHeaderWhenEmpty="true" runat="server"
                    AutoGenerateColumns="False" Width="100%" DataKeyNames="IndustrialNewApplicationCode"
                    CssClass="SubFormWOBG" OnRowDataBound="gvIndRenewEligibleApplication_RowDataBound"
                    OnRowCommand="gvIndRenewEligibleApplication_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="50px" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Label ID="lbIndRenewSno" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name of Industry">
                            <ItemTemplate>
                                <asp:Label ID="NameOfIndustry" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("NameOfIndustry")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Application Number" ItemStyle-Width="100px" HeaderStyle-Width="150px">
                            <ItemTemplate>
                                <asp:Label ID="IndustrialNewApplicationNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationNumber")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NOC-Number" ItemStyle-Width="100px" HeaderStyle-Width="150px">
                            <ItemTemplate>
                                <asp:Label ID="lblNOCNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("NOCNumber")) %>'></asp:Label>
                                <br />
                                <asp:Label ID="lblIssueLetterStartDate" runat="server"></asp:Label>
                                <asp:Label ID="lblIssueLetterEndDate" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Renew" ItemStyle-Width="50px" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnRenewApply" runat="server" PostBackUrl="~/ExternalUser/IndustrialRenew/IndustrialRenew.aspx"
                                    CommandName="ApplicationCode" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode")) %>'>Apply</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        There is No Application Eligible for Renewal.
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="font-size: 14px; font-weight: bold">
                Renew of Renewal
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvIndNthRenewEligibleApplication" ShowHeaderWhenEmpty="true" runat="server"
                    AutoGenerateColumns="False" Width="100%" DataKeyNames="IndustrialRenewApplicationCode"
                    CssClass="SubFormWOBG" OnRowCommand="gvIndNthRenewEligibleApplication_RowCommand"
                    OnRowDataBound="gvIndNthRenewEligibleApplication_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="50px" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Label ID="lbIndNthRenewSno" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name of Industry">
                            <ItemTemplate>
                                <asp:Label ID="NameOfIndustry" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("NameOfIndustry")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Application Number" ItemStyle-Width="100px" HeaderStyle-Width="150px">
                            <ItemTemplate>
                                <asp:Label ID="IndustrialNewApplicationNumber" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NOC-Number" ItemStyle-Width="100px" HeaderStyle-Width="150px">
                            <ItemTemplate>
                                <asp:Label ID="lblNOCNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("NOCNumber")) %>'></asp:Label>
                                <br />
                                <asp:Label ID="lblIssueLetterStartDate" runat="server"></asp:Label>
                                <asp:Label ID="lblIssueLetterEndDate" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Renewed For" ItemStyle-Width="100px" HeaderStyle-Width="150px">
                            <ItemTemplate>
                                <asp:Label ID="lblLinkDepth" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("LinkDepth"))  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Renew" ItemStyle-Width="50px" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnRenewApply" runat="server" PostBackUrl="~/ExternalUser/IndustrialRenew/IndustrialRenew.aspx"
                                    CommandName="ApplicationCode" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialRenewApplicationCode")) %>'>Apply</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        There is No Application Eligible for Renewal.
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblModeFrom" runat="server" Enabled="False" Visible="False"></asp:Label>
                <asp:Label ID="lblIndustialApplicationCodeFrom" runat="server" Enabled="False" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
