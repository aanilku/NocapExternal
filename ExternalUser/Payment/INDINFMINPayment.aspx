<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    AutoEventWireup="true" CodeFile="INDINFMINPayment.aspx.cs" Inherits="ExternalUser_Payment_INDINFMINPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <asp:Label ID="lblSortField" runat="server" Text=""></asp:Label>
    <table align="center" class="SubFormWOBG" style="line-height: 25px" width="90%">
        <tr>
            <td colspan="2">
                <div class="div_IndAppHeading" style="padding-left: 10px; font-size: 18px; height: 25px">
                    <b>Industrial/Infrastructure/Mining Payment</b>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                Application Type :
            </td>
            <td style="width: 50%">
                <asp:DropDownList ID="ddlApplicationType" runat="server" Width="200px" AutoPostBack="True">
                </asp:DropDownList>
                <%--OnSelectedIndexChanged="ddlApplicationType_SelectedIndexChanged"--%>
            </td>
        </tr>
        <%-- <tr>
            <td>
                Application Type Category:
            </td>
            <td style="width: 50%">
                <asp:DropDownList ID="ddlApplicationTypeCat" runat="server" Style="width: 200px;"
                    AutoPostBack="false" Enabled="false">
                </asp:DropDownList>
            </td>
        </tr>--%>
        <tr>
            <td>
                Application Purpose :
            </td>
            <td style="width: 50%">
                <asp:DropDownList ID="ddlApplicationPurpose" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Button runat="server" Text="Search" ID="lbtnSearch" OnClick="lbtnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div style="overflow-x: auto;">
                    <asp:GridView ID="grdPaymentList" runat="server" AllowPaging="True" ShowHeaderWhenEmpty="true"
                        Width="100%" AutoGenerateColumns="False" DataKeyNames="AppCode" CssClass="SubFormWOBG"
                        AllowSorting="True" OnRowDataBound="grdPaymentList_RowDataBound" OnPageIndexChanging="grdPaymentList_PageIndexChanging"
                        OnRowCommand="grdPaymentList_RowCommand" OnSorting="grdPaymentList_Sorting">
                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                        <PagerSettings Mode="Numeric" PageButtonCount="10" Position="TopAndBottom" />
                        <Columns>
                            <asp:TemplateField HeaderText="Project Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblIND_INF_MIN_Name" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("IND_INF_MIN_Name"))%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Application Number">
                                <ItemTemplate>
                                    <%-- <asp:LinkButton ID="AppNo" runat="server" CommandName="ApplicationNumber" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("AppCode"))%>'
                                        Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AppNo")) %>'></asp:LinkButton>--%>
                                    <asp:Label ID="AppNo" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AppNo"))%>'></asp:Label>
                                    <asp:HiddenField runat="server" ID="hdnAppCode" Value='<%# Eval("AppCode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Applied For Renewal">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="AppliedForRenewal" CommandName="Renewal" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("AppCode")+","+Eval("LinkDepth"))%>'></asp:LinkButton>
                                    <asp:HiddenField runat="server" ID="hdnRenewal" Value='<%# Eval("LinkDepth") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Application Status">
                                <ItemTemplate>
                                    <asp:Label ID="AppStatusDesc" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationStatusDescription"))%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Required Amount">
                                <ItemTemplate>
                                    <asp:Label ID="PaymentRequiredAmount" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("PaymentRequiredAmount"))%>'></asp:Label>
                                    <asp:HiddenField runat="server" ID="hdnPaymentAmountReceiveFinally" Value='<%# Eval("PaymentAmountReceiveFinally") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--   <asp:TemplateField HeaderText="Appllication Submitted Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblAppSubDate" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("SubmittedOnByExUC", "{0:dd MMM yyyy}"))%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Submitted Type">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblSubmittedType" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("SubmittedTypeName"))%>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Appllication Submitted Date">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblSubmittedOnByExUC" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("SubmittedOnByExUC","{0:dd MMM yyyy}"))%>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment">
                                <ItemTemplate>
                                    <asp:LinkButton Text="Pay" runat="server" ID="Pay" CommandName="Pay" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("AppCode")+","+Eval("LinkDepth"))%>'></asp:LinkButton>
                                    <%--<asp:Button Text="Pay2" runat="server" ID="LinkButton1" CommandName="Pay" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("AppCode")+","+Eval("LinkDepth"))%>'></asp:Button>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment View">
                                <ItemTemplate>
                                    <asp:LinkButton Text="View" runat="server" ID="View" CommandName="View" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("AppCode")+","+Eval("LinkDepth"))%>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            No Record found for this Payment !
                        </EmptyDataTemplate>
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
