<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="MiningRenewList.aspx.cs"
    Inherits="ExternalUser_MiningRenew_MiningRenewList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRFs" runat="server" Value="" />
    <table align="center" class="SubFormWOBG" width="100%" style="line-height: 20px">
        <tr>
            <td>
                <div class="div_IndAppHeading" style="padding-left: 10px; text-align: center; font-size: 18px">
                    Mining Renew- List of Application Eligible for Renew
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
                <asp:GridView ID="gvMinRenewEligibleApplication" ShowHeaderWhenEmpty="true" runat="server"
                    AutoGenerateColumns="False" Width="100%" DataKeyNames="ApplicationCode" CssClass="SubFormWOBG"
                    OnRowDataBound="gvMinRenewEligibleApplication_RowDataBound" OnRowCommand="gvMinRenewEligibleApplication_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="50px" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Label ID="lbMinRenewSno" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name of Mining">
                            <ItemTemplate>
                                <asp:Label ID="NameOfMining" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("NameOfMining")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Application Number" ItemStyle-Width="100px" HeaderStyle-Width="150px">
                            <ItemTemplate>
                                <asp:Label ID="MiningNewApplicationNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("MiningNewApplicationNumber")) %>'></asp:Label>
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
                                <asp:LinkButton ID="lbtnRenewApply" runat="server" PostBackUrl="~/ExternalUser/MiningRenew/MiningRenew.aspx"
                                    CommandName="ApplicationCode" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'>Apply</asp:LinkButton>
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
                <asp:GridView ID="gvMinNthRenewEligibleApplication" ShowHeaderWhenEmpty="true" runat="server"
                    AutoGenerateColumns="False" Width="100%" DataKeyNames="MiningRenewApplicationCode"
                    CssClass="SubFormWOBG" OnRowCommand="gvMinNthRenewEligibleApplication_RowCommand"
                    OnRowDataBound="gvMinNthRenewEligibleApplication_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="50px" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Label ID="lbMinNthRenewSno" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name of Mining">
                            <ItemTemplate>
                                <asp:Label ID="NameOfMining" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("NameOfMining")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Application Number" ItemStyle-Width="100px" HeaderStyle-Width="150px">
                            <ItemTemplate>
                                <asp:Label ID="MiningNewApplicationNumber" runat="server" Text=""></asp:Label>
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
                                <asp:LinkButton ID="lbtnRenewApply" runat="server" PostBackUrl="~/ExternalUser/MiningRenew/MiningRenew.aspx"
                                    CommandName="ApplicationCode" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode")) %>'>Apply</asp:LinkButton>
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
                <asp:Label ID="lblMiningApplicationCodeFrom" runat="server" Enabled="False" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
