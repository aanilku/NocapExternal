<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" CodeFile="StatusOnlinePayment.aspx.cs" Inherits="ExternalUser_StatusOnlinePayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../css/OnLinePayment//bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/OnLinePayment/font-awesome.min.css" />
    <link href="../css/OnLinePayment/PaymentOnlineSuccess.css" rel="stylesheet" />
    <%--  <script type="text/javascript" src="../Scripts/jquery-3.6.0.min.js"></script>--%>
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '100%'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../Images/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../Images/plus.png");
            $(this).closest("tr").next().remove();
        });
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
    <asp:HiddenField ID="hidCSRFs" runat="server" Value="" />
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
                        <td style="text-align: left">Application Name: </td>

                        <td colspan="4" style="text-align: left">
                            <asp:Label runat="server" ID="lblAppName"></asp:Label></td>
                    </tr>
                    <tr>

                        <td style="text-align: left">Application Code: </td>

                        <td colspan="4" style="text-align: left">
                            <asp:Label runat="server" ID="Label1"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:CheckBox ID="chkBoxUndertaking" runat="server" Checked="true" Enabled="false" />
                        </td>
                        <td colspan="4" style="text-align: left"><span>I certify that, the amount has not been debited from my account .</span></td>

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
                            <asp:GridView ID="gvSADPayment" runat="server" ShowHeaderWhenEmpty="true"
                                AutoGenerateColumns="False"
                                Width="100%" DataKeyNames="ApplicationCode,OrderPaymentCode" CssClass="SubFormWOBG"
                                OnRowDataBound="gvSADPayment_RowDataBound"
                                OnRowEditing="gvSADPayment_RowEditing"
                                OnRowUpdating="gvSADPayment_RowUpdating" OnPageIndexChanging="gvSADPayment_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="15px">
                                        <ItemTemplate>
                                            <img alt="" style="cursor: pointer" src="../Images/plus.png" />
                                            <asp:Panel ID="pnlSADPaymentDetails" runat="server" Style="display: none">
                                                <table>
                                                    <tr>
                                                        <td></td>
                                                        <td colspan="100%">

                                                            <asp:GridView runat="server" CssClass="SubFormWOBG" ID="gvSADPaymentDetails" ShowHeaderWhenEmpty="true"
                                                                AutoGenerateColumns="False" Width="100%">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                                        <ItemTemplate>
                                                                            <%# Convert.ToInt32(Container.DataItemIndex) + 1%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Transaction Type">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblPaymentTypeDecription" runat="server" Font-Bold="true" Text='<%#  System.Web.HttpUtility.HtmlEncode(Eval("PaymentTypeDecription")).ToUpper() %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAmount" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AmountValue")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Arrear Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblArrearAmount" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ArearAmount")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    There is no record exists.
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>


                                                        </td>
                                                    </tr>

                                                </table>
                                            </asp:Panel>


                                        </ItemTemplate>

                                        <ItemStyle Width="15px"></ItemStyle>
                                    </asp:TemplateField>
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
                                        <EditItemTemplate>
                                            <asp:Label ID="lblRemark" runat="server" Text="Remark:"></asp:Label>
                                            <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Remarks")) %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="gvrfvtxtRemark" ControlToValidate="txtRemark" runat="server" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Terminate Transaction">
                                        <EditItemTemplate>

                                            <asp:ImageButton ID="imgbtnTerminate" runat="server" Height="20px" ImageUrl="~/Images/update.jpg"
                                                Width="20px" CommandName="Update" ToolTip="Terminate" /></td>
                                                   

                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnEdit" runat="server" Height="20px" ImageUrl="~/Images/Edit.jpg"
                                                Enabled='<%# System.Web.HttpUtility.HtmlEncode(Eval("FinalPaymentStatus")).ToUpper() == "PENDING" ? true : false%>'
                                                Width="20px" CommandName="Edit" ToolTip="Edit" CausesValidation="false" />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Refresh Transaction">



                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnRefresh" runat="server" Height="20px"
                                                ImageUrl="~/Images/refresh.png"
                                                CommandArgument='<%#Eval("OrderPaymentCodeForNTRP")%>'
                                                CausesValidation="false" Width="20px" CommandName="Refresh" ToolTip="Refresh" OnClick="imgbtnRefresh_Click" />


                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField>
                                        <ItemTemplate>
                                            <table>
                                                <tr>

                                                    <td>
                                                        <asp:Button ID="btnRemovePenalty" runat="server" Text="Remove"
                                                            CommandArgument='<%#Eval("Remark") + "," + Eval("PenaltyRecFinally") + "," + Eval("AppCode")+","+ Eval("PenaltySN") %>'
                                                            Enabled='<%# Eval("PenaltyRecFinally").ToString() == "No" ? true : false %>'
                                                            OnClick="btnRemovePenalty_Click" />
                                                        <asp:ImageButton ID="imgbtnRefresh" runat="server" Height="20px"
                                                            ImageUrl="~/Images/refresh.png"
                                                            CausesValidation="false" Width="20px" CommandName="Refresh" ToolTip="Refresh" />



                                                    </td>
                                                </tr>
                                            </table>





                                        </ItemTemplate>
                                    </asp:TemplateField>--%>


                                    <%--<asp:TemplateField HeaderText="Details">
                                        <ItemTemplate>
                                            <asp:GridView runat="server" CssClass="SubFormWOBG" ID="gvSADPaymentDetails" ShowHeaderWhenEmpty="true"
                                                AutoGenerateColumns="False" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%# Convert.ToInt32(Container.DataItemIndex) + 1%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Transaction Type">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblPaymentTypeDecription" runat="server" Font-Bold="true" Text='<%#  System.Web.HttpUtility.HtmlEncode(Eval("PaymentTypeDecription")).ToUpper() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AmountValue")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Arrear Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblArrearAmount" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ArearAmount")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <EmptyDataTemplate>
                                                    There is no record exists.
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                                <EmptyDataTemplate>
                                    There is no record exists.
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%; align-content: center" class="SubFormWOBG">
                    <tr>
                        <td style="text-align: left; font-weight: bold" class="auto-style2">Transaction History</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvSADPaymentHist" runat="server" ShowHeaderWhenEmpty="true"
                                AutoGenerateColumns="False"
                                Width="100%" DataKeyNames="ApplicationCode,OrderPaymentCode" CssClass="SubFormWOBG"
                                OnRowDataBound="gvSADPaymentHist_RowDataBound">
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




                                    <asp:TemplateField HeaderText="Details">
                                        <ItemTemplate>
                                            <asp:GridView runat="server" CssClass="SubFormWOBG" ID="gvSADPaymentDetailsHist" ShowHeaderWhenEmpty="true"
                                                AutoGenerateColumns="False" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%# Convert.ToInt32(Container.DataItemIndex) + 1%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Transaction Type">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblPaymentTypeDecription" runat="server" Font-Bold="true" Text='<%#  System.Web.HttpUtility.HtmlEncode(Eval("PaymentTypeDecription")).ToUpper() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AmountValue")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Arrear Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblArrearAmount" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ArearAmount")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <EmptyDataTemplate>
                                                    There is no record exists.
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                                <EmptyDataTemplate>
                                    There is no record exists.
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%; align-content: center" class="SubFormWOBG">
                    <tr>
                        <td style="text-align: left; font-weight: bold" class="auto-style2">Offline Application Fee History</td>
                    </tr>


                    <tr>
                        <td>
                            <asp:GridView ID="gvApplicationFee" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="false"
                                ShowHeaderWhenEmpty="true" AllowSorting="true" AllowPaging="true" PageSize="5"
                                DataKeyNames="ApplicationCode,SN"
                                OnSorting="gvApplicationFee_Sorting">

                                <Columns>

                                    <asp:TemplateField HeaderText="Application Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppCode" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Serial Number" SortExpression="SN">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSN" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("SN")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Processing Fee Pay Mode">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProFeePayMode" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProcessingFeePayMode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bharatkosh Transaction Ref No.">

                                        <ItemTemplate>
                                            <asp:Label ID="lblBhaKoshTranNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("BharatTransReferanceNumber")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bharat Kosh Transaction Dated">

                                        <ItemTemplate>
                                            <asp:Label ID="lblBharatKoshTransactionDated" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("BharatTransDated","{0:dd/MM/yyyy}")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bharat Kosh Pay Status">

                                        <ItemTemplate>
                                            <asp:Label ID="lblBharatKoshPayStatus" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("BharatPayStatus")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Application Fee Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApplicationFeeAmount" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Amount")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemark" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Remark")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CreatedOn" SortExpression="CreatedOn">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreatedOn")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View Uploaded File">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentName" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttName")) %>'></asp:Label>
                                            <asp:LinkButton ID="lblViewUploadedLinkApplicationFee" OnCommand="ViewFile" runat="server"
                                                CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode"))+ "," + System.Web.HttpUtility.HtmlEncode(Eval("SN")) %>'>View</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No Record Exists.
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>

                </table>
            </td>
        </tr>

        <tr>
            <td>
                <table style="width: 100%; align-content: center" class="SubFormWOBG">
                    <tr>
                        <td style="text-align: left; font-weight: bold" class="auto-style2">Offline Penalty Fee History</td>
                    </tr>


                    <tr>
                        <td>
                            <asp:GridView ID="gvPenalty" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="false"
                                ShowHeaderWhenEmpty="true" AllowSorting="true" AllowPaging="true" PageSize="5"
                                align="center" DataKeyNames="ApplicationCode,PenaltySN,SN"
                                OnSorting="gvPenalty_Sorting">


                                <Columns>

                                    <asp:TemplateField HeaderText="Application Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppCode" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Serial Number" SortExpression="SN">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSN" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("SN")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Processing Fee Pay Mode">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProFeePayMode" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProcessingFeePayMode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bharatkosh Transaction Ref No.">

                                        <ItemTemplate>
                                            <asp:Label ID="lblBhaKoshTranNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("BharatTransReferanceNumber")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bharat Kosh Transaction Dated">

                                        <ItemTemplate>
                                            <asp:Label ID="lblBharatKoshTransactionDated" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("BharatTransDated","{0:dd/MM/yyyy}")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bharat Kosh Pay Status">

                                        <ItemTemplate>
                                            <asp:Label ID="lblBharatKoshPayStatus" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("BharatPayStatus")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Penalty Charges Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPenaltyAmount" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Amount")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemark" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Remark")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CreatedOn" SortExpression="CreatedOn">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreatedOn")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View Uploaded File">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentName" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttName")) %>'></asp:Label>
                                            <asp:LinkButton ID="lblViewUploadedLinkPenalty" OnCommand="DownloadOrViewFilePenaltyCorrection" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode"))+ ","+ System.Web.HttpUtility.HtmlEncode(Eval("PenaltySN"))+ "," + System.Web.HttpUtility.HtmlEncode(Eval("SN")) %>'>View</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No Record Exists.
                                </EmptyDataTemplate>
                            </asp:GridView>

                        </td>
                    </tr>

                </table>
            </td>
        </tr>

        <tr>
            <td>
                <table style="width: 100%; align-content: center" class="SubFormWOBG">
                    <tr>
                        <td style="text-align: left; font-weight: bold" class="auto-style2">Offline Abstraction Charge History</td>
                    </tr>


                    <tr>
                        <td>
                            <asp:GridView ID="gvGWCharges" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="false"
                                ShowHeaderWhenEmpty="true" AllowSorting="true" AllowPaging="true" PageSize="5"
                                align="center" DataKeyNames="ApplicationCode,SN"
                                OnSorting="gvGWCharges_Sorting">


                                <Columns>

                                    <asp:TemplateField HeaderText="Application Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppCode" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Serial Number" SortExpression="SN">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSN" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("SN")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Processing Fee Pay Mode">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProFeePayMode" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProcessingFeePayMode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bharatkosh Transaction Ref No.">

                                        <ItemTemplate>
                                            <asp:Label ID="lblBhaKoshTranNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("BharatTransReferanceNumber")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bharat Kosh Transaction Dated">

                                        <ItemTemplate>
                                            <asp:Label ID="lblBharatKoshTransactionDated" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("BharatTransDated","{0:dd/MM/yyyy}")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bharat Kosh Pay Status">

                                        <ItemTemplate>
                                            <asp:Label ID="lblBharatKoshPayStatus" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("BharatPayStatus")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="G.W Charges Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGWChargesAmount" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Amount")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemark" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Remark")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CreatedOn" SortExpression="CreatedOn">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreatedOn")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View Uploaded File">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentName" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttName")) %>'></asp:Label>
                                            <asp:LinkButton ID="lblViewUploadedLinkGWCharges" OnCommand="DownloadOrViewFileGWCharges" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode"))+ "," + System.Web.HttpUtility.HtmlEncode(Eval("SN")) %>'>View</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <EmptyDataTemplate>
                                    No Record Exists.
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                        <asp:Label ID="lblSortFieldPenalty" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSortFieldGWCharges" runat="server" Visible="False"></asp:Label>
                    </tr>

                </table>
            </td>
        </tr>



        <tr>
            <td colspan="3">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>

            </td>
        </tr>
    </table>
</asp:Content>
