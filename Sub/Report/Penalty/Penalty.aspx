<%@ Page Title="" Language="C#" MasterPageFile="~/Sub/SubReportMaster.master" AutoEventWireup="true"
    CodeFile="Penalty.aspx.cs" Inherits="Sub_Report_Penalty_Penalty" %>

<%@ Register Src="~/AscxControl/ExternalUserLeftMenu.ascx" TagName="ExternalUserLeftMenu"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                    <table align="center" class="SubFormWOBG" style="line-height: 25px" width="75%">
                        <tr>
                            <td colspan="2">
                                <div class="div_IndAppHeading" style="padding-left: 10px; font-size: 18px;">
                                    Know Your Penalty
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMesgI" runat="server" Text="(I). Penalty provision for non Compliance of  NOC conditions" Font-Bold="True"></asp:Label>
                                <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvPenalty" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                    DataKeyNames="PenaltyCode"
                                    align="center" AllowPaging="true" AllowSorting="true"
                                    OnPageIndexChanging="gvPenalty_PageIndexChanging"
                                    PageSize="25">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PenaltyCode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAreaTypeCode" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("PenaltyCode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Penalty Description">

                                            <ItemTemplate>
                                                <asp:Label ID="lblPenaltyDesc" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("PenaltyDesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">

                                            <ItemTemplate>
                                                <asp:Label ID="lblRate" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Rate")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="lblSortField" runat="server" Visible="false"></asp:Label>
                                <br />
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2">
                            <asp:Label ID="lblMsgeII" runat="server" Text="(II). Proposed Charges for correction/Modification in the existing issued NOC" Font-Bold="True"></asp:Label>
                        </td></tr>
                            <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvCorrectionCharge" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                    DataKeyNames="CorrectionChargeCode"
                                    align="center" AllowPaging="true" AllowSorting="true"
                                    OnPageIndexChanging="gvCorrectionCharge_PageIndexChanging"
                                    PageSize="25">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PenaltyCode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAreaTypeCode" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CorrectionChargeCode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Penalty Description">

                                            <ItemTemplate>
                                                <asp:Label ID="lblPenaltyDesc" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CorrectionChargeDesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">

                                            <ItemTemplate>
                                                <asp:Label ID="lblRate" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Rate")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                </asp:GridView>
                                <%--<table class="SubFormWOBG" cellspacing="0" rules="all" align="center" border="1" id="ContentPlaceHolder1_gvPenalty" style="border-collapse: collapse;">
                                <tr>
                                    <th scope="col">Sr No</th>
                                    <th scope="col">Penalty Description</th>
                                    <th scope="col">Rate</th>
                                </tr>
                                <tr>
                                    <td>1
                                    </td>
                                    <td>
                                        <span id="ContentPlaceHolder1_gvPenalty_lblPenaltyDesc_0">Change in recharge quantum</span>
                                    </td>
                                    <td>
                                        <span id="ContentPlaceHolder1_gvPenalty_lblRate_0">10000.00</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>2
                                    </td>
                                    <td>
                                        <span id="ContentPlaceHolder1_gvPenalty_lblPenaltyDesc_1">Change in User ID.</span>
                                    </td>
                                    <td>
                                        <span id="ContentPlaceHolder1_gvPenalty_lblRate_1">5000.00</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>3
                                    </td>
                                    <td>
                                        <span id="ContentPlaceHolder1_gvPenalty_lblPenaltyDesc_2">Change in firm Name</span>
                                    </td>
                                    <td>
                                        <span id="ContentPlaceHolder1_gvPenalty_lblRate_2">5000.00</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>4
                                    </td>
                                    <td>
                                        <span id="ContentPlaceHolder1_gvPenalty_lblPenaltyDesc_3">Extension of NOC</span>
                                    </td>
                                    <td>
                                        <span id="ContentPlaceHolder1_gvPenalty_lblRate_3">5000.00</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>5
                                    </td>
                                    <td>
                                        <span id="ContentPlaceHolder1_gvPenalty_lblPenaltyDesc_4">Issuance of duplicate NOC</span>
                                    </td>
                                    <td>
                                        <span id="ContentPlaceHolder1_gvPenalty_lblRate_4">5000.00</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>6
                                    </td>
                                    <td>
                                        <span id="ContentPlaceHolder1_gvPenalty_lblPenaltyDesc_5">Issuance of corrigendum to  NOC</span>
                                    </td>
                                    <td>
                                        <span id="ContentPlaceHolder1_gvPenalty_lblRate_5">5000.00</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>7
                                    </td>
                                    <td>
                                        <span id="ContentPlaceHolder1_gvPenalty_lblPenaltyDesc_6">Any other items/corrections etc</span>
                                    </td>
                                    <td>
                                        <span id="ContentPlaceHolder1_gvPenalty_lblRate_6">5000.00</span>
                                    </td>
                                </tr>
                                
                                
                            </table>--%>
                            </td>

                        </tr>
                    </table>
                </div>
            </td>

        </tr>

       
    </table>
    
</asp:Content>

