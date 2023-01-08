<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" CodeFile="ViewAllOnlinePayment.aspx.cs" Inherits="ExternalUser_ViewAllOnlinePayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../css/OnLinePayment//bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/OnLinePayment/font-awesome.min.css" />
    <link href="../css/OnLinePayment/PaymentOnlineSuccess.css" rel="stylesheet" />

    <%--  <script type="text/javascript" src="../Scripts/jquery-3.6.0.min.js"></script>--%>
    <%--<script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>--%>
    <script type="text/javascript">
        //$(document).ready(function () {
        //    $(".plusImg").click(function () {
        //        //alert('dfgfdgh')
        //        $(this).attr("src", "../Images/minus.png");
        //        $(".pcont").toggle();
        //        $(this).attr("class", "minusImg");
        //    });
        //    $(".minusImg").click(function () {
        //        //alert('dfgfdgh')
        //        $(this).attr("src", "../Images/plus.png");
        //        $(".pcont").toggle();
        //        $(this).attr("class", "plusImg");
        //    });
        //});

        //$("[src*=minus]").click("click", function () {
        //    $(this).attr("src", "../Images/plus.png");
        //    $(this).closest("tr").next().remove();
        //});


        //$("[src*=plus]").live("click", function () {
        //    $(this).closest("tr").after("<tr><td></td><td colspan = '100%'>" + $(this).next().html() + "</td></tr>")
        //    $(this).attr("src", "../Images/minus.png");
        //});
        //$("[src*=minus]").live("click", function () {
        //    $(this).attr("src", "../Images/plus.png");
        //    $(this).closest("tr").next().remove();
        //});
    </script>
    <style type="text/css">
        body {
            background: #f2f2f2;
        }

        .payment {
            border: 1px solid #f2f2f2;
            height: 500px;
            border-radius: 20px;
            background: #fff;
        }

        .payment_header {
            background: deepskyblue;
            padding: 20px;
            border-radius: 20px 20px 0px 0px;
        }

        .check {
            margin: 0px auto;
            width: 50px;
            height: 50px;
            border-radius: 100%;
            background: #fff;
            text-align: center;
        }

            .check i {
                vertical-align: middle;
                line-height: 50px;
                font-size: 30px;
            }

        .content {
            text-align: center;
        }

            .content h1 {
                font-size: 50px;
                padding-top: 25px;
            }

            .content a {
                width: 200px;
                height: 35px;
                color: #fff;
                border-radius: 20px;
                padding: 10px 50px;
                background: deepskyblue;
                transition: all ease-in-out 0.3s;
            }

                .content a:hover {
                    text-decoration: none;
                    background: #000;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:hiddenfield id="hidCSRF" runat="server" value="" />
    <table style="width: 100%; align-content: center" class="SubFormWOBG">
        <tr>

            <td>
                <table style="width: 100%; align-content: center" class="SubFormWOBG">


                    <tr>
                        <td colspan="5">
                            <div class="div_IndAppHeading" style="text-align: center">
                                Payment Status
                            </div>


                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">Application Type :
                        </td>
                        <td style="border-left: 0px solid red; border-right: 0px solid red; width: 15%">
                            <asp:dropdownlist id="ddlApplicationType" runat="server" width="200px" autopostback="True"
                                onselectedindexchanged="ddlApplicationType_SelectedIndexChanged">
                </asp:dropdownlist>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">Application Name: </td>

                        <td colspan="4" style="text-align: left">
                            <asp:dropdownlist runat="server" width="200px" id="ddlApplicationNumber" OnSelectedIndexChanged="ddlApplicationNumber_SelectedIndexChanged"></asp:dropdownlist>

                        </td>
                    </tr>


                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%; align-content: center" class="SubFormWOBG">
                    <tr>
                        <td style="text-align: left; font-weight: bold" class="auto-style2">Curent Transaction</td>
                    </tr>

                    <tr>
                        <td>
                            <asp:gridview id="gvPayment" runat="server" showheaderwhenempty="true"
                                autogeneratecolumns="False"
                                width="100%" datakeynames="ApplicationCode,OrderPaymentCode" cssclass="SubFormWOBG"
                                onrowdatabound="gvPayment_RowDataBound"
                                onpageindexchanging="gvPayment_PageIndexChanging"
                                onrowcommand="gvPayment_RowCommand">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sr. No.">
                                        <ItemTemplate>
                                            <%# Convert.ToInt32(Container.DataItemIndex) + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateOnBy" runat="server" Text='<%# Convert.ToDateTime(System.Web.HttpUtility.HtmlEncode(Eval("CreatedOnByExUC"))).ToShortDateString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactionID" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TransactionRefNo")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Mode">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaymentMode" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("PaymentMode").ToString()=="S"?"Single":"Combined") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TotalAmount")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactionStatus" runat="server" Font-Bold="true" Text='<%#  System.Web.HttpUtility.HtmlEncode(Eval("FinalPaymentStatus")).ToUpper() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemark" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Remarks"))%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Detail">
                                        <ItemTemplate>


                                            <asp:LinkButton ID="lnkPaymentStatus" runat="server"
                                                PostBackUrl="ViewOnlinePayment.aspx"
                                                Text="View" CommandName="OrderPaymentCode"
                                                CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode"))%>' />

                                        </ItemTemplate>
                                    </asp:TemplateField>




                                </Columns>
                                <EmptyDataTemplate>
                                    There is no record exists.
                                </EmptyDataTemplate>
                            </asp:gridview>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>







        <tr>
            <td colspan="3">
                <asp:label id="lblAppCode" runat="server" enabled="False" visible="False"></asp:label>

                <asp:label id="lblMessage" runat="server"></asp:label>

            </td>
        </tr>
    </table>
</asp:Content>
