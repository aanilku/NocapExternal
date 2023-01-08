<%@ Page Title="NOCAP-Applicant Home" Language="C#" MaintainScrollPositionOnPostback="true"
    MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true"
    CodeFile="ApplicantHome.aspx.cs" Inherits="ExternalUser_ApplicantHome" Theme="Skin" %>

<%@ Register Src="../AscxControl/ExternalUserLeftMenu.ascx" TagName="ExternalUserLeftMenu"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/table.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Initial {
            display: block;
            padding: 4px 10px 4px 10px;
            float: left; /* background: url("../Images/InitialImage.png") no-repeat right top;*/ /*background:-o-linear-gradient(bottom, #6C7A89 5%, #8995A1 100%);	background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #6C7A89), color-stop(1, #8995A1) );
	            background:-moz-linear-gradient( center top, #6C7A89 5%, #8995A1 100% );
	            filter:progid:DXImageTransform.Microsoft.gradient(startColorstr="#6C7A89", endColorstr="#8995A1");	background: -o-linear-gradient(top,#6C7A89,8995A1);*/
            background-color: #CFE3FA;
            color: Black;
            font-weight: bold;
        }

            .Initial:hover {
                color: White; /* background: url("../Images/SelectedButton.png") no-repeat right top;*/
                background-color: #094E7F;
                cursor: hand;
            }

        .Clicked {
            float: left;
            display: block; /*background: url("../Images/SelectedButton.png") no-repeat right top;*/
            background-color: #094E7F;
            padding: 4px 18px 4px 18px;
            color: Black;
            font-weight: bold;
            color: White;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRFs" runat="server" Value="" />
    <%--<table align="center" cellpadding="0" cellspacing="0" style="width:1100px;">--%>
    <table align="center" class="SubFormWOBG" width="100%" style="line-height: 25px">
        <tr>
            <td style="width: 200px">
                <table width="100%">
                    <tr>
                        <td>
                            <uc1:ExternalUserLeftMenu ID="ExternalUserLeftMenu1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table width="100%" style="line-height: 20px">
                    <tr>
                        <td>
                            <asp:Button Text="Industrial" BorderStyle="None" ID="btnIndustrialTab" CssClass="Initial"
                                runat="server" OnClick="btnIndustrialTab_Click" Style="margin-right: 5px" Width="130px" />
                            <asp:Button Text="Infrastructure" BorderStyle="None" ID="btnInfrastructureTab" CssClass="Initial"
                                runat="server" OnClick="btnInfrastructureTab_Click" Style="margin-right: 5px" Visible="true"
                                Width="130px" />
                            <asp:Button Text="Mining" BorderStyle="None" ID="btnMiningTab" CssClass="Initial" Visible="true"
                                runat="server" OnClick="btnMiningTab_Click" Style="margin-right: 5px" Width="130px" />
                            <asp:Button Text="Domestic" BorderStyle="None" ID="btnDomestic" Visible="false" CssClass="Initial"
                                runat="server" OnClick="btnDomestic_Click" Width="130px" />
                            <asp:MultiView ID="MainView" runat="server">
                                <asp:View ID="View1" runat="server">
                                    <table class="SubFormWOBG" width="100%">
                                        <tr>
                                            <td style="font-size: 17px; font-weight: bold">Industrial
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="div_IndAppHeading" style="padding-left: 10px">
                                                    <b>New- Save As Draft (Number of Save as Draft Application Allowed at a time :
                                                        <asp:Label ID="lblMsgForInd" runat="server" Text=""></asp:Label>)
                                                        <asp:Label ID="lblIndSADAppCount" runat="server" Text=""></asp:Label>
                                                        <br />
                                                        (Validity of Save as Draft Application :
                                                        <asp:Label ID="lblSadValidityInd" runat="server" Text=""></asp:Label><span style="margin-left: 3px;">Month(s)</span>)
                                                    </b>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvIndNewSaveAsDraft" ShowHeaderWhenEmpty="true" runat="server"
                                                    CssClass="SubFormWOBG" Width="100%" AutoGenerateColumns="False"
                                                    OnRowDataBound="gvIndNewSaveAsDraft_RowDataBound"
                                                    DataKeyNames="IndustrialNewApplicationCode"
                                                    OnRowCommand="gvIndNewSaveAsDraft_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%# Convert.ToInt32(Container.DataItemIndex) + 1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Application Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name of Industry">
                                                            <ItemTemplate>
                                                                <asp:Label ID="NameOfIndustry" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("NameOfIndustry")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Signature and Seal">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnPrint" runat="server" PostBackUrl="IndustrialNew/Reports/INDSADReportViewer.aspx"
                                                                    CommandName="ApplicationCode" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode")) %>'>Preview</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Created Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCreatedDateInd" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreatedOnByExUC", "{0:dd MMM yyyy}"))%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnEdit" runat="server"
                                                                    OnCommand="lbtnEdit_Click"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode")) %>'>Edit</asp:LinkButton>
                                                                <%-- PostBackUrl="IndustrialNew/IndustrialNew.aspx"--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Detail">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="lnkbtnMakePayment" runat="server"
                                                                    OnClientClick="return confirm('Once Payment is initiated,application detial can not be modify.Are you sure to proceed for payment ?')"
                                                                    PostBackUrl="Payment.aspx"
                                                                    Text="MakePayment" CommandName="MakePayment"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProFeeOrderPaymentCode")+","+Eval("IndustrialNewApplicationCode"))%>' />
                                                                <asp:Label runat="server" Text="/"></asp:Label>
                                                                <asp:LinkButton ID="lnkPaymentStatus" runat="server"
                                                                    PostBackUrl="StatusOnlinePayment.aspx"
                                                                    Text="View" CommandName="OrderPaymentCode"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProFeeOrderPaymentCode")+","+Eval("IndustrialNewApplicationCode"))%>' />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Submit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnSubmit" runat="server" OnCommand="lbtnSubmit_Click" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode")) %>'
                                                                    PostBackUrl="IndustrialNew/Submit.aspx">Submit</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ready To Submit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTransactionStatus" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ReadyToSubmit")).ToUpper() %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        There is No Save As Draft Application.
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr id="trRenH" runat="server" visible="true">
                                            <td>
                                                <div class="div_IndAppHeading" style="padding-left: 10px">
                                                    <b>Renew- Save As Draft
                                                        <%--(Number of Save as Draft Application Allowed at a time :
                                                        <asp:Label ID="lblMsgForIndRenewSAD" runat="server" Text=""></asp:Label>)
                                                        <asp:Label ID="lblIndRenewSADAppCount" runat="server" Text=""></asp:Label>
                                                        <br />
                                                        (Validity of Save as Draft Application :
                                                        <asp:Label ID="lblSadValidityIndRenew" runat="server" Text=""></asp:Label><span style="margin-left: 3px;">Month(s)</span>)--%>
                                                    </b>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr id="trRenD" runat="server" visible="true">
                                            <td>
                                                <asp:GridView ID="gvIndRenewSaveAsDraft" ShowHeaderWhenEmpty="true" runat="server"
                                                    CssClass="SubFormWOBG" Width="100%" AutoGenerateColumns="False"
                                                    DataKeyNames="IndustrialRenewApplicationCode"
                                                    OnRowDataBound="gvIndRenewSaveAsDraft_RowDataBound"
                                                    OnRowCommand="gvIndRenewSaveAsDraft_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%# Convert.ToInt32(Container.DataItemIndex) + 1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Application Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialRenewApplicationCode")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name of Industry">
                                                            <ItemTemplate>
                                                                <asp:Label ID="NameOfIndustry" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("NameOfIndustry")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Application Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApplicationNumber" runat="server"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblOldApplicationNumber" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Existing NOC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNOCNumber" runat="server"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblIssueLetterStartDate" runat="server"></asp:Label>
                                                                <asp:Label ID="lblIssueLetterEndDate" runat="server"></asp:Label>
                                                                <asp:Label ID="lblOldNOCNumber" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Renewal">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRenewalCount" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Signature and Seal">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnPrint" runat="server" PostBackUrl="IndustrialRenew/Reports/INDSADRenewReportViewer.aspx"
                                                                    CommandName="ApplicationCode" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialRenewApplicationCode")) %>'>Preview</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Created Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCreatedDateInd" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreatedOnByExUC", "{0:dd MMM yyyy}"))%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnEdit" runat="server" OnCommand="lbtnEditIndRenewSaveAsDraft_Click"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialRenewApplicationCode")) %>'
                                                                    PostBackUrl="IndustrialRenew/IndustrialRenew.aspx">Edit</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Detail">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="lnkbtnMakePayment" runat="server" OnClientClick="return confirm('Once Payment is initiated,application detial can not be modify.Are you sure to proceed for payment ?')"
                                                                    PostBackUrl="Payment.aspx"
                                                                    Text="MakePayment" CommandName="MakePayment"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProFeeOrderPaymentCode")+","+Eval("IndustrialRenewApplicationCode"))%>' />
                                                                <asp:Label runat="server" Text="/"></asp:Label>
                                                                <asp:LinkButton ID="indrenlnkPaymentStatus" runat="server" PostBackUrl="StatusOnlinePayment.aspx"
                                                                    Text="View" CommandName="OrderPaymentCode"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProFeeOrderPaymentCode")+","+Eval("IndustrialRenewApplicationCode"))%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Submit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnSubmit" runat="server" OnCommand="lbtnSubmit_Click" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialRenewApplicationCode")) %>'
                                                                    PostBackUrl="IndustrialRenew/Submit.aspx">Submit</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ready To Submit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTransactionStatus" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ReadyToSubmit")).ToUpper() %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        There is No Save As Draft Application.
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>

                                        <tr id="trExpansnH" runat="server" visible="true">
                                            <td>
                                                <div class="div_IndAppHeading" style="padding-left: 10px">
                                                    <b>Expansion- Save As Draft
                                                        <%--(Number of Save as Draft Application Allowed at a time :
                                                        <asp:Label ID="lblMsgForIndExpansion" runat="server" Text=""></asp:Label>)
                                                        <asp:Label ID="lblIndExpansionAppCount" runat="server" Text=""></asp:Label>
                                                        <br />
                                                        (Validity of Save as Draft Application :
                                                        <asp:Label ID="lblSadValidityIndExpansion" runat="server" Text=""></asp:Label><span style="margin-left: 3px;">Month(s)</span>)--%>
                                                    </b>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr id="trExpansnD" runat="server" visible="true">
                                            <td>
                                                <asp:GridView ID="gvIndExpansionSaveAsDraft" ShowHeaderWhenEmpty="true" runat="server"
                                                    CssClass="SubFormWOBG" Width="100%" AutoGenerateColumns="False"
                                                    DataKeyNames="IndustrialNewApplicationCode"
                                                    OnRowDataBound="gvIndExpansionSaveAsDraft_RowDataBound"
                                                    OnRowCommand="gvIndExpansionSaveAsDraft_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%# Convert.ToInt32(Container.DataItemIndex) + 1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Application Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name of Industry">
                                                            <ItemTemplate>
                                                                <asp:Label ID="NameOfIndustry" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("NameOfIndustry")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Application Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApplicationNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationNumber")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Existing NOC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNOCNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("NOCNumber")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Signature and Seal">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnPrint" runat="server" PostBackUrl="IndustrialNew/Reports/INDSADReportViewer.aspx"
                                                                    CommandName="ApplicationCode" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode")) %>'>Preview</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Created Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCreatedDateInd" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreatedOnByExUC", "{0:dd MMM yyyy}"))%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnEdit" runat="server"
                                                                    OnCommand="lbtnINDExpansionEdit_Click"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode")) %>'>Edit</asp:LinkButton>
                                                                <%--   PostBackUrl="IndustrialNew/IndustrialNew.aspx"--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                      <%--  <asp:TemplateField HeaderText="Payment Detail">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="lnkbtnMakePayment" runat="server"
                                                                    OnClientClick="return confirm('Once Payment is initiated,application detial can not be modify.Are you sure to proceed for payment ?')"
                                                                    PostBackUrl="Payment.aspx"
                                                                    Text="MakePayment" CommandName="MakePayment"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProFeeOrderPaymentCode")+","+Eval("IndustrialNewApplicationCode"))%>' />
                                                                <asp:Label runat="server" Text="/"></asp:Label>
                                                                <asp:LinkButton ID="lnkPaymentStatus" runat="server"
                                                                    PostBackUrl="StatusOnlinePayment.aspx"
                                                                    Text="View" CommandName="OrderPaymentCode"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProFeeOrderPaymentCode")+","+Eval("IndustrialNewApplicationCode"))%>' />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Submit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnSubmit" runat="server" OnCommand="lbtnSubmit_Click" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode")) %>'
                                                                    PostBackUrl="~/Expansion/IND/Submit.aspx">Submit</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ready To Submit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTransactionStatus" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ReadyToSubmit")).ToUpper() %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        There is No Save As Draft Application.
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <div class="div_IndAppHeading" style="padding-left: 10px">
                                                    <b>Submitted :
                                                        <asp:Label ID="lblIndAppCount" runat="server" Text=""></asp:Label></b>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvIndNewSubmitted" ShowHeaderWhenEmpty="true" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" DataKeyNames="IndustrialNewApplicationCode" CssClass="SubFormWOBG"
                                                    OnRowDataBound="gvIndNewSubmitted_RowDataBound" OnRowCommand="gvIndNewSubmitted_RowCommand">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%# Convert.ToInt32(Container.DataItemIndex) + 1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name of Industry">
                                                            <ItemTemplate>
                                                                <asp:Label ID="NameOfIndustry" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("NameOfIndustry")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Application Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="IndustrialNewApplicationNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationNumber")) %>'></asp:Label>
                                                                <asp:LinkButton ID="lbtnCommunicationRequired" runat="server" ForeColor="Red"
                                                                    PostBackUrl="Communication/CommunicationReply.aspx"
                                                                    CommandName="ApplicationCode"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode")) %>'>Communication Required</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLatestAppStatusCode" runat="server"></asp:Label>
                                                                <asp:Label ID="lblLatestPresentReq" runat="server"></asp:Label>
                                                                <asp:LinkButton ID="lbtnEdit" runat="server" OnCommand="lbtnViewStatus_Click"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode")) %>'>View</asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton2" runat="server" OnCommand="lbtnAppNameChangeStatus_Click" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode")) %>'>AppNameChangeStatus</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnPrint" runat="server" PostBackUrl="IndustrialNew/Reports/INDReportViewer.aspx"
                                                                    CommandName="ApplicationCode" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode")) %>'>Print</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Make Payment">
                                                            <ItemTemplate>

                                                                <asp:LinkButton  runat="server" OnCommand="lbtnMakePayment_Click"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode")) %>'>Make Payment</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Digital Signed Letter">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnDownload" runat="server" Text="Download" Visible="false"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode")) %>'
                                                                    OnClick="lbtnDownload_Click">Download</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Scan Letter">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnScanDownload" runat="server" Text="Scan Letter" Visible="false"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode")) %>'
                                                                    OnClick="lbtnScanDownload_Click">Scan Letter</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="NOC-Number" ItemStyle-Width="100px" HeaderStyle-Width="150px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkNOCNumber" runat="server" CommandName="NOCNumber" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode"))%>'
                                                                    Enabled="false" />
                                                                <asp:LinkButton ID="lnkCompliance" runat="server" OnCommand="lnkCompliance_Click"
                                                                    Visible="false" Text="Self Compliance" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode"))%>' />
                                                                <br />
                                                                <asp:LinkButton ID="lnkInspection" runat="server" OnCommand="lnkInspection_Click"
                                                                    Visible="false" Text="Self Inspection"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode"))%>' />
                                                                <br />
                                                                <asp:LinkButton ID="lnkPayAbstractionCharges" runat="server" OnCommand="lnkPayAbstractionCharges_Click"
                                                                    Visible="true" Text="Ground Water Charge"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode"))%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Apply Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApplyType" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("SubmittedType")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Piezometer Detail" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkAddPiezometerDetail" runat="server"
                                                                    PostBackUrl="Piezometer/PiezometerDetail.aspx" Text="Add" CommandName="PiezometerDetail"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode")) %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Telemetry Detail" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTelemetryDetail" runat="server" Text="Add"
                                                                    CommandName="TelemetryDetail" PostBackUrl="Telemetry/TelemetryUserLoginDetail.aspx"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode")) %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Renewal" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkRenewDetail" runat="server" CommandName="RenewDetail" OnCommand="lbtnRenewal_Click"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode")) %>' />
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
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    <table class="SubFormWOBG" width="100%" style="line-height: 25px">
                                        <tr>
                                            <td style="font-size: 17px; font-weight: bold">Infrastructure
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="div_IndAppHeading" style="padding-left: 10px">
                                                    <b>New - Save As Draft (Number of Save as Draft Application Allowed at a time :
                                                        <asp:Label ID="lblMsgForInf" runat="server" Text=""></asp:Label>)
                                                        <asp:Label ID="lblInfSADAppCount" runat="server" Text=""></asp:Label>
                                                        <br />
                                                        (Validity of Save as Draft Application :
                                                        <asp:Label ID="lblSadValidityInf" runat="server" Text=""></asp:Label><span style="margin-left: 3px;">Month(s)</span>)
                                                    </b>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvInfNewSaveAsDraft" runat="server" ShowHeaderWhenEmpty="true"
                                                    AutoGenerateColumns="False" CssClass="SubFormWOBG" Width="100%"
                                                    DataKeyNames="ApplicationCode"
                                                    OnRowDataBound="gvInfNewSaveAsDraft_RowDataBound"
                                                    OnRowCommand="gvInfNewSaveAsDraft_RowCommand">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%# Convert.ToInt32(Container.DataItemIndex) + 1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Application Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name of Infrastructure">
                                                            <ItemTemplate>
                                                                <asp:Label ID="NameOfInfrastructure" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("NameOfInfrastructure")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Signature and Seal">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnPrint" runat="server" PostBackUrl="InfrastructureNew/Reports/INFSADReportViewer.aspx"
                                                                    CommandName="ApplicationCode" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'>Preview</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Created Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCreatedDateInf" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreatedOnByExUC", "{0:dd MMM yyyy}"))%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnEdit" runat="server" OnCommand="lbtnEditInfrastructure_Click"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'
                                                                    PostBackUrl="InfrastructureNew/InfrastructureNew.aspx">Edit</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Detail">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="lnkbtnMakePayment" runat="server" OnClientClick="return confirm('Once Payment is initiated,application detial can not be modify.Are you sure to proceed for payment ?')"
                                                                    PostBackUrl="Payment.aspx"
                                                                    Text="MakePayment" CommandName="MakePayment"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProFeeOrderPaymentCode")+","+Eval("ApplicationCode"))%>' />
                                                                <asp:Label runat="server" Text="/"></asp:Label>
                                                                <asp:LinkButton ID="inflnkPaymentStatus" runat="server" PostBackUrl="StatusOnlinePayment.aspx"
                                                                    Text="View" CommandName="OrderPaymentCode"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProFeeOrderPaymentCode")+","+Eval("ApplicationCode"))%>' />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Submit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnSubmit" runat="server" OnCommand="lbtnSubmit_Click"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'
                                                                    PostBackUrl="InfrastructureNew/Submit.aspx">Submit</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ready To Submit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTransactionStatus" runat="server"
                                                                    Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ReadyToSubmit")).ToUpper() %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        There is No Save As Draft Application.
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr id="tr1" runat="server" visible="true">
                                            <td>
                                                <div class="div_IndAppHeading" style="padding-left: 10px">
                                                    <b>Renew- Save As Draft
                                               
                                                    </b>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr id="tr2" runat="server" visible="true">
                                            <td>
                                                <asp:GridView ID="gvInfRenewSaveAsDraft" ShowHeaderWhenEmpty="true" runat="server"
                                                    CssClass="SubFormWOBG" Width="100%" AutoGenerateColumns="False" DataKeyNames="InfrastructureRenewApplicationCode"
                                                    OnRowDataBound="gvInfRenewSaveAsDraft_RowDataBound"
                                                    OnRowCommand="gvInfRenewSaveAsDraft_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%# Convert.ToInt32(Container.DataItemIndex) + 1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Application Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name of Infrastructure">
                                                            <ItemTemplate>
                                                                <asp:Label ID="NameOfInfrastructure" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("NameOfInfrastructure")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Application Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApplicationNumber" runat="server"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblOldApplicationNumber" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Existing NOC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNOCNumber" runat="server"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblIssueLetterStartDate" runat="server"></asp:Label>
                                                                <asp:Label ID="lblIssueLetterEndDate" runat="server"></asp:Label>
                                                                <asp:Label ID="lblOldNOCNumber" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Renewal">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRenewalCount" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Signature and Seal">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnPrint" runat="server" PostBackUrl="InfrastructureRenew/Reports/INFRenewSADReportViewer.aspx"
                                                                    CommandName="ApplicationCode" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode")) %>'>Preview</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Created Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCreatedDateInd" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreatedOnByExUC", "{0:dd MMM yyyy}"))%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnEdit" runat="server" OnCommand="lbtnEditInfRenewSaveAsDraft_Click"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode")) %>'
                                                                    PostBackUrl="InfrastructureRenew/InfrastructureRenew.aspx">Edit</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Detail">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="lnkbtnMakePayment" runat="server" OnClientClick="return confirm('Once Payment is initiated,application detial can not be modify.Are you sure to proceed for payment ?')"
                                                                    PostBackUrl="Payment.aspx"
                                                                    Text="MakePayment" CommandName="MakePayment"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProFeeOrderPaymentCode")+","+Eval("InfrastructureRenewApplicationCode"))%>' />
                                                                <asp:Label runat="server" Text="/"></asp:Label>
                                                                <asp:LinkButton ID="infrenlnkPaymentStatus" runat="server" PostBackUrl="StatusOnlinePayment.aspx"
                                                                    Text="View" CommandName="OrderPaymentCode"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProFeeOrderPaymentCode")+","+Eval("InfrastructureRenewApplicationCode"))%>' />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Submit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnSubmit" runat="server" OnCommand="lbtnSubmit_Click"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode")) %>'
                                                                    PostBackUrl="InfrastructureRenew/Submit.aspx">Submit</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ready To Submit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTransactionStatus" runat="server"
                                                                    Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ReadyToSubmit")).ToUpper() %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        There is No Save As Draft Application.
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>                                       


                                        <tr id="trInfExpansnH" runat="server" visible="true">
                                            <td>
                                                <div class="div_IndAppHeading" style="padding-left: 10px">
                                                    <b>Expansion- Save As Draft
                                                        <asp:Label ID="lblMsgForInFExpansion" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="lblInFExpansionAppCount" runat="server" Text=""></asp:Label>
                                                        <br />                                                  
                                                        <asp:Label ID="lblSadValidityInFExpansion" runat="server" Text=""></asp:Label>
                                                      
                                                    </b>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr id="trInfExpansnD" runat="server" visible="true">
                                            <td>
                                                <asp:GridView ID="gvInFExpansionSaveAsDraft" ShowHeaderWhenEmpty="true" runat="server"
                                                    CssClass="SubFormWOBG" Width="100%" AutoGenerateColumns="False"
                                                    DataKeyNames="ApplicationCode"
                                                    OnRowDataBound="gvInFExpansionSaveAsDraft_RowDataBound"
                                                    OnRowCommand="gvInFExpansionSaveAsDraft_RowCommand">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%# Convert.ToInt32(Container.DataItemIndex) + 1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Application Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name of Infrastructure">
                                                            <ItemTemplate>
                                                                <asp:Label ID="NameOfInfrastructure" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("NameOfInfrastructure")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Application Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApplicationNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureExpansionApplicationNumber")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Existing NOC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNOCNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("NOCNumber")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Signature and Seal">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnPrint" runat="server" PostBackUrl="InfrastructureNew/Reports/INFSADReportViewer.aspx"
                                                                    CommandName="ApplicationCode" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'>Preview</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Created Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCreatedDateInf" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreatedOnByExUC", "{0:dd MMM yyyy}"))%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnEdit" runat="server" OnCommand="lbtnEditInfrastructure_Click"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'
                                                                    PostBackUrl="Expansion/INF/InfrastructureExpansion.aspx">Edit</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Detail">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="lnkbtnMakePayment" runat="server" OnClientClick="return confirm('Once Payment is initiated,application detial can not be modify.Are you sure to proceed for payment ?')"
                                                                    PostBackUrl="Payment.aspx"
                                                                    Text="MakePayment" CommandName="MakePayment"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProFeeOrderPaymentCode")+","+Eval("ApplicationCode"))%>' />
                                                                <asp:Label runat="server" Text="/"></asp:Label>
                                                                <asp:LinkButton ID="inflnkPaymentStatus" runat="server" PostBackUrl="StatusOnlinePayment.aspx"
                                                                    Text="View" CommandName="OrderPaymentCode"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProFeeOrderPaymentCode")+","+Eval("ApplicationCode"))%>' />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Submit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnSubmit" runat="server" OnCommand="lbtnSubmit_Click"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'
                                                                    PostBackUrl="InfrastructureNew/Submit.aspx">Submit</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ready To Submit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTransactionStatus" runat="server"
                                                                    Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ReadyToSubmit")).ToUpper() %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        There is No Save As Draft Application.
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <div class="div_IndAppHeading" style="padding-left: 10px">
                                                    <b>New - Submitted :
                                                        <asp:Label ID="lblInfAppCount" runat="server" Text=""></asp:Label>
                                                    </b>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvInfNewSubmitted" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                                    Width="100%" DataKeyNames="ApplicationCode" CssClass="SubFormWOBG" OnRowDataBound="gvInfNewSubmitted_RowDataBound"
                                                    OnRowCommand="gvInfNewSubmitted_RowCommand">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%# Convert.ToInt32(Container.DataItemIndex) + 1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name of Infrastructure">
                                                            <ItemTemplate>
                                                                <asp:Label ID="NameOfInfrastructure" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("NameOfInfrastructure")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Application Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="InfrastructureNewApplicationNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureNewApplicationNumber")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLatestAppStatusCode1" runat="server"></asp:Label>
                                                                <asp:Label ID="lblLatestPresentReq1" runat="server"></asp:Label>
                                                                <asp:LinkButton ID="lbtnEdit" runat="server" OnCommand="lbtnViewInfrastructureStatus_Click"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'>View</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnPrint" runat="server" PostBackUrl="InfrastructureNew/Reports/INFReportViewer.aspx"
                                                                    CommandName="ApplicationCode" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'>Print</asp:LinkButton>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Make Payment">
                                                            <ItemTemplate>

                                                                <asp:LinkButton  runat="server" OnCommand="lbtnMakePayment_Click"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'>Make Payment</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Digital Signed Letter">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnInfDownload" runat="server" Text="Download" Visible="false"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'
                                                                    OnClick="lbtnInfDownload_Click">Download</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Scan Letter">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnScanInfDownload" runat="server" Text="Scan Letter" Visible="false"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'
                                                                    OnClick="lbtnScanInfDownload_Click">Scan Letter</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="NOC-Number" ItemStyle-Width="100px" HeaderStyle-Width="150px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkNOCNumber" runat="server" CommandName="NOCNumber" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode"))%>'
                                                                    Enabled="false" />
                                                                <asp:LinkButton ID="lnkCompliance" runat="server" OnCommand="lnkCompliance_Click"
                                                                    Visible="false" Text="Self Compliance"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode"))%>' />
                                                                <br />
                                                                <asp:LinkButton ID="lnkInspection" runat="server" OnCommand="lnkInspection_Click"
                                                                    Visible="false" Text="Self Inspection"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode"))%>' />
                                                                <br />
                                                                <asp:LinkButton ID="lnkPayRestCharges" runat="server" OnCommand="lnkPayRestCharges_Click"
                                                                    Visible="false" Text="Pay Restoration Charge"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode"))%>' />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Apply Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApplyType" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("SubmittedType")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Piezometer Detail" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkAddPiezometerDetail" runat="server" Text="Add" CommandName="PiezometerDetail"
                                                                    PostBackUrl="Piezometer/PiezometerDetail.aspx"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Telemetry Detail" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkInfTelemetryDetail" runat="server" Text="Add" CommandName="TelemetryDetail"
                                                                    PostBackUrl="Telemetry/TelemetryUserLoginDetail.aspx"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Renewal" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkInfRenewDetail" runat="server" CommandName="RenewDetail" OnCommand="lnkInfRenewDetail_Click"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>' />
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
                                </asp:View>
                                <asp:View ID="vMining" runat="server">
                                    <table class="SubFormWOBG" width="100%" style="line-height: 25px">
                                        <tr>
                                            <td style="font-size: 17px; font-weight: bold">Mining
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="div_IndAppHeading" style="padding-left: 10px">
                                                    <b>New - Save As Draft (Number of Save as Draft Application Allowed at a time :
                                                        <asp:Label ID="lblMsgForMin" runat="server" Text=""></asp:Label>)
                                                        <asp:Label ID="lblMinSADAppCount" runat="server" Text=""></asp:Label>
                                                        <br />
                                                        (Validity of Save as Draft Application :
                                                        <asp:Label ID="lblSADValidityMin" runat="server" Text=""></asp:Label><span style="margin-left: 3px;">Month(s)</span>)
                                                    </b>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvMINNewSaveAsDraft" runat="server" ShowHeaderWhenEmpty="true"
                                                    Width="100%" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                                    DataKeyNames="ApplicationCode"
                                                    OnRowCommand="gvMINNewSaveAsDraft_RowCommand"
                                                    OnRowDataBound="gvMINNewSaveAsDraft_RowDataBound">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%# Convert.ToInt32(Container.DataItemIndex) + 1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Application Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name of Mining">
                                                            <ItemTemplate>
                                                                <asp:Label ID="NameOfMining" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("NameOfMining")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Signature and Seal">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnPrint" runat="server" PostBackUrl="MiningNew/Reports/MINSADReportViewer.aspx"
                                                                    CommandName="ApplicationCode" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'>Preview</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Created Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCreatedDateMin" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreatedOnByExUC", "{0:dd MMM yyyy}"))%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnEdit" runat="server" OnCommand="lbtnMiningEdit_Click" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'
                                                                    PostBackUrl="MiningNew/MiningNew.aspx">Edit</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Detail">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="lnkbtnMakePayment" runat="server" OnClientClick="return confirm('Once Payment is initiated,application detial can not be modify.Are you sure to proceed for payment ?')"
                                                                    PostBackUrl="Payment.aspx"
                                                                    Text="MakePayment" CommandName="MakePayment"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProFeeOrderPaymentCode")+","+Eval("ApplicationCode"))%>' />
                                                                <asp:Label runat="server" Text="/"></asp:Label>
                                                                <asp:LinkButton ID="minlnkPaymentStatus" runat="server" PostBackUrl="StatusOnlinePayment.aspx"
                                                                    Text="View" CommandName="OrderPaymentCode"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProFeeOrderPaymentCode")+","+Eval("ApplicationCode"))%>' />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Submit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnMinSubmit" runat="server" OnCommand="lbtnSubmit_Click" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'
                                                                    PostBackUrl="MiningNew/Submit.aspx">Submit</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ready To Submit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTransactionStatus" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ReadyToSubmit")).ToUpper() %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        There is No Save As Draft Application.
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>

                                        <tr id="tr3" runat="server" visible="true">
                                            <td>
                                                <div class="div_IndAppHeading" style="padding-left: 10px">
                                                    <b>Renew- Save As Draft
                                                      
                                                    </b>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr id="tr4" runat="server" visible="true">
                                            <td>
                                                <asp:GridView ID="gvMinRenewSaveAsDraft" ShowHeaderWhenEmpty="true" runat="server"
                                                    CssClass="SubFormWOBG" Width="100%" AutoGenerateColumns="False"
                                                    DataKeyNames="MiningRenewApplicationCode"
                                                    OnRowDataBound="gvMinRenewSaveAsDraft_RowDataBound"
                                                    OnRowCommand="gvMinRenewSaveAsDraft_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%# Convert.ToInt32(Container.DataItemIndex) + 1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Application Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name of Mining">
                                                            <ItemTemplate>
                                                                <asp:Label ID="NameOfMining" runat="server" Text=""></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Application Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApplicationNumber" runat="server"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblOldApplicationNumber" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Existing NOC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNOCNumber" runat="server"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblIssueLetterStartDate" runat="server"></asp:Label>
                                                                <asp:Label ID="lblIssueLetterEndDate" runat="server"></asp:Label>
                                                                <asp:Label ID="lblOldNOCNumber" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Renewal">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRenewalCount" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Signature and Seal">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnPrint" runat="server" PostBackUrl="MiningRenew/Reports/MINRenewSADReportViewer.aspx"
                                                                    CommandName="ApplicationCode" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode")) %>'>Preview</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Created Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCreatedDateInd" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreatedOnByExUC", "{0:dd MMM yyyy}"))%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnEdit" runat="server" OnCommand="lbtnEditMinRenewSaveAsDraft_Click"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode")) %>'
                                                                    PostBackUrl="MiningRenew/MiningRenew.aspx">Edit</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Detail">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="lnkbtnMakePayment" runat="server" OnClientClick="return confirm('Once Payment is initiated,application detial can not be modify.Are you sure to proceed for payment ?')"
                                                                    PostBackUrl="Payment.aspx"
                                                                    Text="MakePayment" CommandName="MakePayment"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProFeeOrderPaymentCode")+","+Eval("MiningRenewApplicationCode"))%>' />
                                                                <asp:Label runat="server" Text="/"></asp:Label>
                                                                <asp:LinkButton ID="minrenlnkPaymentStatus" runat="server" PostBackUrl="StatusOnlinePayment.aspx"
                                                                    Text="View" CommandName="OrderPaymentCode"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProFeeOrderPaymentCode")+","+Eval("MiningRenewApplicationCode"))%>' />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Submit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnMinSubmit" runat="server"
                                                                    OnCommand="lbtnSubmit_Click" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode")) %>'
                                                                    PostBackUrl="MiningRenew/Submit.aspx">Submit</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ready To Submit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTransactionStatus" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ReadyToSubmit")).ToUpper() %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        There is No Save As Draft Application.
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <div class="div_IndAppHeading" style="padding-left: 10px">
                                                    <b>Expansion - Save As Draft 
                                                      <asp:Label ID="lblMsgForMinExpansion" runat="server" Text=""></asp:Label>)
                                                         <asp:Label ID="lblMinExpansionAppCount" runat="server" Text=""></asp:Label>
                                                        <br />                                                       
                                                         <asp:Label ID="lblExpansionSADValidityMin" runat="server" Text=""></asp:Label>
                                                    </b>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvMINExpansionSaveAsDraft" runat="server" ShowHeaderWhenEmpty="true"
                                                    Width="100%" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                                    DataKeyNames="ApplicationCode"
                                                    OnRowCommand="gvMINExpansionSaveAsDraft_RowCommand"
                                                    OnRowDataBound="gvMINExpansionSaveAsDraft_RowDataBound">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%# Convert.ToInt32(Container.DataItemIndex) + 1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Application Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name of Mining">
                                                            <ItemTemplate>
                                                                <asp:Label ID="NameOfMining" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("NameOfMining")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Application Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApplicationNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("MiningExpansionApplicationNumber")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Existing NOC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNOCNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("NOCNumber")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Signature and Seal">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnPrint" runat="server" PostBackUrl="MiningNew/Reports/MINSADReportViewer.aspx"
                                                                    CommandName="ApplicationCode" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'>Preview</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Created Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCreatedDateMin" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreatedOnByExUC", "{0:dd MMM yyyy}"))%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnEdit" runat="server" OnCommand="lbtnMiningEdit_Click" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'
                                                                    PostBackUrl="Expansion/INF/MiningExpansion.aspx">Edit</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Detail">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="lnkbtnMakePayment" runat="server" OnClientClick="return confirm('Once Payment is initiated,application detial can not be modify.Are you sure to proceed for payment ?')"
                                                                    PostBackUrl="Payment.aspx"
                                                                    Text="MakePayment" CommandName="MakePayment"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProFeeOrderPaymentCode")+","+Eval("ApplicationCode"))%>' />
                                                                <asp:Label runat="server" Text="/"></asp:Label>
                                                                <asp:LinkButton ID="minlnkPaymentStatus" runat="server" PostBackUrl="StatusOnlinePayment.aspx"
                                                                    Text="View" CommandName="OrderPaymentCode"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProFeeOrderPaymentCode")+","+Eval("ApplicationCode"))%>' />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Submit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnMinSubmit" runat="server" OnCommand="lbtnSubmit_Click" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'
                                                                    PostBackUrl="MiningNew/Submit.aspx">Submit</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ready To Submit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTransactionStatus" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ReadyToSubmit")).ToUpper() %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        There is No Save As Draft Application.
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td>
                                                <div class="div_IndAppHeading" style="padding-left: 10px">
                                                    <b>New - Submitted :
                                                        <asp:Label ID="lblMinAppCount" runat="server" Text=""></asp:Label>
                                                    </b>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvMinNewSubmitted" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                                    Width="100%" DataKeyNames="ApplicationCode" CssClass="SubFormWOBG" OnRowDataBound="gvMinNewSubmitted_RowDataBound"
                                                    OnRowCommand="gvMinNewSubmitted_RowCommand">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%# Convert.ToInt32(Container.DataItemIndex) + 1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name of Mining">
                                                            <ItemTemplate>
                                                                <asp:Label ID="NameOfMining" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("NameOfMining")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Application Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="MiningNewApplicationNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("MiningNewApplicationNumber")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLatestAppStatusCode3" runat="server"></asp:Label>
                                                                <asp:Label ID="lblLatestPresentReq3" runat="server"></asp:Label>
                                                                <asp:LinkButton ID="lbtnEdit" runat="server" OnCommand="lbtnViewMiningStatus_Click"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'>View</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnPrint" runat="server" PostBackUrl="MiningNew/Reports/MINReportViewer.aspx"
                                                                    CommandName="ApplicationCode" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'>Print</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--  <asp:TemplateField HeaderText="Make Payment">
                                                            <ItemTemplate>

                                                                <asp:LinkButton  runat="server" OnCommand="lbtnMakePayment_Click"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'>Make Payment</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Digital Signed Letter">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnMinDownload" runat="server" Text="Download" Visible="false"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'
                                                                    OnClick="lbtnMinDownload_Click">Download</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Scan Letter">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnScanMinDownload" runat="server" Text="Scan Letter" Visible="false"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>'
                                                                    OnClick="lbtnScanMinDownload_Click">Scan Letter</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="NOC-Number" ItemStyle-Width="100px" HeaderStyle-Width="150px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkNOCNumber" runat="server" CommandName="NOCNumber" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode"))%>'
                                                                    Enabled="false" />
                                                                <asp:LinkButton ID="lnkCompliance" runat="server" OnCommand="lnkCompliance_Click"
                                                                    Visible="false" Text="Self Compliance"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode"))%>' />
                                                                <br />
                                                                <asp:LinkButton ID="lnkInspection" runat="server" OnCommand="lnkInspection_Click"
                                                                    Visible="false" Text="Self Inspection"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode"))%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Apply Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApplyType" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("SubmittedType")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Piezometer Detail" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkAddPiezometerDetail" runat="server" Text="Add" CommandName="PiezometerDetail"
                                                                    PostBackUrl="Piezometer/PiezometerDetail.aspx"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Telemetry Detail" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" Text="Add" CommandName="TelemetryDetail"
                                                                    PostBackUrl="Telemetry/TelemetryUserLoginDetail.aspx"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Renewal" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkMinRenewDetail" runat="server" CommandName="RenewDetail" OnCommand="lnkMinRenewDetail_Click"
                                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>' />
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
                                </asp:View>
                                <asp:View ID="vDomestic" runat="server">
                                    <table class="SubFormWOBG" width="100%" style="line-height: 25px">
                                        <tr>
                                            <td style="font-size: 17px; font-weight: bold">Domestic View
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                            </asp:MultiView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPageTitle" runat="server" Enabled="False" Visible="False"></asp:Label>
                            <asp:Label ID="lblMode" runat="server" Enabled="False" Visible="False"></asp:Label>
                            <asp:Label ID="lblApplicationCode" runat="server" Enabled="False" Visible="False"></asp:Label>
                            <asp:Label ID="lblOrderPaymentCode" runat="server" Enabled="False" Visible="False"></asp:Label>
                            <asp:Label ID="lblApplicationCodeForPayment" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
