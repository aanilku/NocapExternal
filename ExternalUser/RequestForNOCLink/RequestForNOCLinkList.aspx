<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    AutoEventWireup="true" CodeFile="RequestForNOCLinkList.aspx.cs" Inherits="ExternalUser_RequestForNOCLink_RequestForNOCLinkList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../css/table.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRFs" runat="server" Value="" />
    <table width="100%" style="line-height: 20px">
        <tr>
            <td>
                <table class="SubFormWOBG" width="100%">
                    <tr>
                        <td style="font-size: 17px; font-weight: bold">
                            Request For NOC Link (Enroll Old NOC)</td>
                    </tr>
                    <tr>
                        <td>
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                <b>
                                    <asp:LinkButton ID="lnkApplyNew" runat="server" Font-Size="14px" Font-Underline="true"
                                        ForeColor="White" OnClick="lnkApplyNew_Click">Click Here For Apply</asp:LinkButton></b>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvRequestForNOCLink" ShowHeaderWhenEmpty="true" runat="server"
                                AutoGenerateColumns="False" Width="100%" DataKeyNames="RequestNumber" CssClass="SubFormWOBG"
                                OnRowCommand="gvRequestForNOCLink_RowCommand" OnRowDataBound="gvRequestForNOCLink_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No.">
                                        <ItemTemplate>
                                            <%# Convert.ToInt32(Container.DataItemIndex) + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Requested NOC No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReqNOCNo" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("RequestedNOCNo")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Requested NOC Issue Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReqNOCIssueName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("RequestedNOCIssueName")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Requested NOC Issue Address Typed">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequestedNOCIssueAddressTyped" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("RequestedNOCIssueAddressTyped")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Present Project / Industry Location">
                                        <ItemTemplate>
                                            <b>State:</b>
                                            <asp:Label ID="lblPresLocState" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("PresentLocationStateName")) %>'></asp:Label><br />
                                            <b>District:</b>
                                            <asp:Label ID="lblPresLocDistrict" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("PresentLocationDistrictName")) %>'></asp:Label><br />
                                            <b>Sub District:</b>
                                            <asp:Label ID="lblPresLocSubDistrict" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("PresentLocationSubDistrictName")) %>'></asp:Label><br />
                                            <b>
                                                <asp:Label ID="lblVillageOrTownText" runat="server" Text='<%# (SetLabelVillageOrTownText(Convert.ToString(Eval("PresentLocationVillageName")))) %>'></asp:Label></b>
                                            <asp:Label ID="lblVillageOrTownValue" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("PresentLocationVillageName")) == null ? Eval("PresentLocationTownName") : Eval("PresentLocationVillageName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproved" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Approved")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatusDesc" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("StatusDescription")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NOC Attachment" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBtnViewNOCAttachment" runat="server" CommandName="ViewNOCAttachment"
                                                Visible='<%# (SetLinkButtonVisibility(Convert.ToInt32(Eval("RequestForNOCLinkAttachments.AttachmentCode")))) %>'
                                                CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("RequestNumber") + "," + Eval("RequestForNOCLinkAttachments.AttachmentCode") + "," + Eval("RequestForNOCLinkAttachments.AttachmentCodeSerialNumber")) %>'>View</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Submitted Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubmittedDate" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreatedOnByExUC","{0:dd/MM/yyyy}")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    There is No Application Submitted.
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
