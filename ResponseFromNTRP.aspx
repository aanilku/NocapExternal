<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/NTRPResponseMaster.master" AutoEventWireup="true" CodeFile="ResponseFromNTRP.aspx.cs" Inherits="ExternalUser_ResponseFromNTRP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../css/OnLinePayment//bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/OnLinePayment/font-awesome.min.css" />
    <link href="../css/OnLinePayment/PaymentOnlineSuccess.css" rel="stylesheet" />
    <link type="text/css" rel="Stylesheet" href="css/LoginStyle.css" />
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
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <table style="width: 100%; align-content: center" class="SubFormWOBG">


        <tr>
            <td colspan="5">
                <div class="div_IndAppHeading" style="text-align: center">
                    Payment Status
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align: left" class="auto-style2">Application Name </td>
            <td>:</td>
            <td style="text-align: left" class="auto-style1">
                <asp:Label runat="server" ID="lblAppName"></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align: left" class="auto-style2">Application Number </td>
            <td>:</td>
            <td style="text-align: left" class="auto-style1">
                <asp:Label runat="server" ID="lblAppNo"></asp:Label></td>
        </tr>

        <tr>
            <td style="text-align: left" class="auto-style2">Amount</td>
            <td>:</td>
            <td style="text-align: left" class="auto-style1">
                <asp:Label runat="server" ID="lblAmount"></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align: left" class="auto-style2">Transaction Id</td>
            <td>:</td>
            <td style="text-align: left" class="auto-style1">
                <asp:Label runat="server" ID="lblReferNo"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left" class="auto-style2">Transaction Date</td>
            <td>:</td>
            <td style="text-align: left" class="auto-style1">
                <asp:Label runat="server" ID="lblTime"></asp:Label>
            </td>

        </tr>
        <tr>
            <td style="text-align: left" class="auto-style2">Transaction Status</td>
            <td>:</td>
            <td style="text-align: left" class="auto-style1">
                <asp:Label runat="server" ID="lblTransStatus" Font-Bold="true" Font-Size="Large"></asp:Label>
            </td>

        </tr>
        <tr>
            <td colspan="3">Note: Please keep these information for future reference </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                <asp:Label ID="lblApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                <asp:Label ID="lblOnlineOrderCode" Visible="false" runat="server" Enabled="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center" colspan="3">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnNext" runat="server" Text="Home" OnClick="btnNext_Click" />
                        </td>
                       
                    </tr>
                </table>



            </td>
        </tr>


    </table>
    <%-- <div class="container">
        <div id="dvSubmiitPayment" class="row" runat="server" visible="false">
            <div class="col-md-6 mx-auto mt-5">
                <div class="payment" style="height: 600px!important;">
                    <div class="payment_header1">
                        <div class="check"><i class="fa fa-check" aria-hidden="true"></i></div>
                    </div>
                    <div class="content">
                        <h2 style="color: forestgreen;" id="hPaymentSuccess" runat="server" visible="false">Payment Successful !</h2>
                        <h6 id="time" runat="server">Transaction Id :
                                    <asp:Label runat="server" ID="lblReferNo"></asp:Label><br />
                            On 
                                    <asp:Label ID="lblTime" runat="server"></asp:Label>
                        </h6>

                        <table align="center" runat="server" id="Table1">
                            <tr>
                                <td style="text-align: left" class="auto-style2">Application Name </td>
                                <td>:</td>
                                <td style="text-align: left" class="auto-style1">
                                    <asp:Label runat="server" ID="lblAppName"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="text-align: left" class="auto-style2">Application Number </td>
                                <td>:</td>
                                <td style="text-align: left" class="auto-style1">
                                    <asp:Label runat="server" ID="lblAppNo"></asp:Label></td>
                            </tr>

                            <tr>
                                <td style="text-align: left" class="auto-style2">Amount</td>
                                <td>:</td>
                                <td style="text-align: left" class="auto-style1">
                                    <asp:Label runat="server" ID="lblAmount"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="text-align: left" class="auto-style2">Transaction Status</td>
                                <td>:</td>
                                <td style="text-align: left" class="auto-style1">
                                    <asp:Label runat="server" ID="lblTransStatus" Font-Bold="true" Font-Size="Large"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td colspan="3">Note: Please keep these information for future reference </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                    <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                                    <asp:Label ID="lblApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                                    <asp:Label ID="lblOnlineOrderCode" Visible="false" runat="server" Enabled="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center" colspan="3">

                                    <asp:Button ID="btnNext" runat="server" Text="Next >>" OnClick="btnNext_Click" />
                                </td>
                            </tr>
                        </table>

                        <h1 style="color: red;" id="hPaymentFail" runat="server" visible="false">Payment Fail !</h1>
                        <h1 style="color: red;" id="hSTATUSUNKNOWN" runat="server" visible="false">STATUS UNKNOWN/ABORT !</h1>
                        <h1 style="color: red;" id="hPENDING" runat="server" visible="false">PENDING !</h1>
                        <h1 style="color: red;" id="hEXPIRED" runat="server" visible="false">EXPIRED !</h1>
                    </div>

                </div>
            </div>
        </div>
    </div>--%>
</asp:Content>
