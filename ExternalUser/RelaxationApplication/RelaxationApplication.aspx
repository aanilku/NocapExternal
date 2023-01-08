<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" CodeFile="RelaxationApplication.aspx.cs" Inherits="ExternalUser_RelaxationApplication_RelaxationApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


     <asp:HiddenField ID="hidCSRFs" runat="server" Value="" />

    <table class="SubFormWOBG" width="100%">
                                        <tr>
                                            <td style="font-size: 17px; font-weight: bold">
                                                <div class="div_IndAppHeading" style="padding-left: 10px">
                                                Industrial / Infrastructure / Mining
                                                    </div>
                                                 <div style="text-align:right;">
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ExternalUser/RelaxationApplication/IndustrialNew.aspx">Apply New</asp:HyperLink>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%--<div class="div_IndAppHeading" style="padding-left: 10px">
                                                    <b>New- Save As Draft                                                          
                                                    </b>
                                                </div>--%>
                                               
                                            </td>
                                        </tr>
         <tr>
                                            <td>
                                                <%--<div class="div_IndAppHeading" style="padding-left: 10px">
                                                    <b>New- Save As Draft                                                          
                                                    </b>
                                                </div>--%>
                                               
                                            </td>
                                        </tr>
       <tr>

           <td>



    <asp:GridView ID="gvRelaxationApplication" ShowHeaderWhenEmpty="true" runat="server"
        CssClass="SubFormWOBG" Width="100%" AutoGenerateColumns="False"
        DataKeyNames="ApplicationCode">
        <Columns>
            <asp:TemplateField HeaderText="Sr. No.">
                <ItemTemplate>
                    <%# Convert.ToInt32(Container.DataItemIndex) + 1%>
                </ItemTemplate>
            </asp:TemplateField>
                        <asp:TemplateField HeaderText="Application Type">
                <ItemTemplate>
                    <asp:Label ID="lblAppTypeDesc" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AppTypeDesc"))%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Name of Industry">
                <ItemTemplate>
                    <asp:Label ID="NameOfIndustry" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("NameOfIndustry")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <%--                     <asp:TemplateField HeaderText="Signature and Seal">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnPrint" runat="server" PostBackUrl="~/ExternalUser/IndustrialNew/Reports/INDSADReportViewer.aspx"
                                                                    CommandName="ApplicationCode" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'>Preview</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
            <%-- <asp:TemplateField HeaderText="Payment Detail">
                                                            <ItemTemplate>
                                                            <asp:HiddenField ID="hdnindPaymentStatus" runat="server" Value='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProFeeOrderPaymentCode"))%>' />
                                                        <asp:LinkButton ID="lnkPaymentStatus" runat="server" PostBackUrl="~/ExternalUser/StatusOnlinePayment.aspx"
                                                                    Enabled="false" Text="View"   CommandName="OrderPaymentCode" 
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProFeeOrderPaymentCode")+","+Eval("IndustrialNewApplicationCode"))%>' />
                                                        </ItemTemplate>
                                                        </asp:TemplateField> --%>
            <asp:TemplateField HeaderText="Created Date">
                <ItemTemplate>
                    <asp:Label ID="lblCreatedDateInd" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreatedOnByExUC", "{0:dd MMM yyyy}"))%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Submitted Date">
                <ItemTemplate>
                     <asp:Label ID="lblSubmittedOnByExUC" runat="server" Visible='<%# Eval("Submitted").ToString() == "Y" ? true : false %>' Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("SubmittedOnByExUC", "{0:dd MMM yyyy}"))%>'></asp:Label>                    
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnEdit" runat="server" Font-Bold="true" OnCommand="lbtnEdit_Click" Visible='<%# Eval("Submitted").ToString() == "Y" ? false : true %>' CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'
                        PostBackUrl="~/ExternalUser/RelaxationApplication/IndustrialNew.aspx">Edit</asp:LinkButton>
                    
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            There is No Save As Draft Application.
        </EmptyDataTemplate>
    </asp:GridView>


                <asp:Label ID="lblPageTitle" runat="server" Enabled="False" Visible="False"></asp:Label>
                            <asp:Label ID="lblMode" runat="server" Enabled="False" Visible="False"></asp:Label>
                            <asp:Label ID="lblApplicationCode" runat="server" Enabled="False" Visible="False"></asp:Label>
           </td>
       </tr>
         </table>
</asp:Content>

