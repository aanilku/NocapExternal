<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="InfrastructureRenewDetail.aspx.cs"
    Inherits="ExternalUser_InfrastructureRenew_RenewalDetail_InfrastructureRenewDetail" %>

<%@ Register Src="../../../AscxControl/ExternalUserLeftMenu.ascx" TagName="ExternalUserLeftMenu"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .SubmittedApp {
            font-family: Arial;
            font-size: 14px;
            height: 20px;
            background-color: #094E7F;
            color: White;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRFs" runat="server" Value="" />
    <table align="center" class="SubFormWOBG" style="width: 100%; line-height: 25px">
        <tr>
            <td style="width: 200px">
                <uc1:ExternalUserLeftMenu ID="ExternalUserLeftMenu1" runat="server" />
            </td>
            <td style="vertical-align: top">
                <table width="100%" class="SubFormWOBG" style="height: auto;">
                    <tr>
                        <td colspan="2">
                            <div class="div_IndAppHeading" style="width: 98%; height: 23px; line-height: 22px; padding-left: 10px">
                                <asp:Label ID="Label3" runat="server" ForeColor="White" Font-Bold="True" Text="Application Detail"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Application No :</strong>
                        </td>
                        <td>
                            <asp:Label ID="lblApplicationNo" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Name of Infrastructure :</strong>
                        </td>
                        <td>
                            <asp:Label ID="lblInfrastructureName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvMainGrid" runat="server" AutoGenerateColumns="false" CssClass="SubFormWOBG"
                                OnRowDataBound="gvMainGrid_RowDataBound" DataKeyNames="InfrastructureFirstApplicationCode"
                                Width="100%" ShowHeader="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo" ItemStyle-Width="80px" HeaderStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnLinkDepthSN" runat="server" Value='<%#System.Web.HttpUtility.HtmlEncode(Eval("LinkDepth")) %>' />
                                            <asp:HiddenField ID="hdnInfrastructureFirstApplicationCode" runat="server" Value='<%#System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureFirstApplicationCode")) %>' />
                                            <asp:Label ID="lblLinkDepthHeader" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("LinkDepthHeader")) %>'
                                                Font-Size="15px" Font-Bold="True"></asp:Label>
                                            <asp:GridView ID="grdRenewInfrastructureApplication" runat="server" ShowHeaderWhenEmpty="true"
                                                OnRowDataBound="grdRenewInfrastructureApplication_RowDataBound" DataKeyNames="InfrastructureRenewApplicationCode,LinkDepth"
                                                OnRowCommand="grdRenewInfrastructureApplication_RowCommand" PageSize="8" AllowPaging="true"
                                                Width="100%" AutoGenerateColumns="false" CssClass="SubFormWOBG">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Existing NOC">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPreviousNOCNumber" runat="server"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblPrevNOCLetterStartDate" runat="server"></asp:Label>
                                                            <asp:Label ID="lblPrevNOCLetterEndDate" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLatestAppStatusCode" runat="server"></asp:Label>
                                                            <asp:LinkButton ID="lbtnView" runat="server" OnCommand="lbtnViewStatus_Click" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode")) %>'>View</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtn_Print" runat="server" PostBackUrl="~/ExternalUser/InfrastructureRenew/Reports/INFRenewReportViewer.aspx"
                                                                CommandName="InfrastructureRenewApplicationCode" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode")) %>'>Print</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Digital Signed Letter">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDownload" runat="server" Text="Download" Visible="false"
                                                                CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode")) %>'
                                                                OnClick="lbtnDownload_Click">Download</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Scan Letter">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnScanDownload" runat="server" Text="Scan Letter" Visible="false"
                                                                CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode")) %>'
                                                                OnClick="lbtnScanDownload_Click">Scan Letter</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText=" Issued NOC" ItemStyle-Width="100px" HeaderStyle-Width="150px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkNOCNumber" runat="server" CommandName="NOCNumber" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("NOCNumber"))%>'
                                                                Enabled="false" />
                                                            <asp:LinkButton ID="lnkCompliance" runat="server" OnCommand="lnkCompliance_Click"
                                                                Text="Self Compliance" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode"))%>' />
                                                            <br />
                                                            <asp:LinkButton ID="lnkInspection" runat="server"
                                                                OnCommand="lnkInspection_Click"
                                                                Visible="false" Text="Self Inspection"
                                                                CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode"))%>' />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Apply Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblApplyType" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("SubmittedType")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Submitted Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSubmittedDate" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("SubmittedOnByExUC", "{0:dd MMM yyyy}"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No Record for this Stage.
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="2">
                            <a id="A1" runat="server" href="~/ExternalUser/ApplicantHome.aspx">Go Back</a>
                            <asp:Label ID="lblApplicationRenewCode" runat="server" Enabled="False" Visible="False"></asp:Label>
                            <asp:Label ID="lblApplicationNewCode" runat="server" Enabled="False" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
