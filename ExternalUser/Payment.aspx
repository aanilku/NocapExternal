<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    AutoEventWireup="true" CodeFile="Payment.aspx.cs" Inherits="ExternalUser_Payment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
    <link rel="stylesheet" href="../Styles/bootstrap.min.css" media="screen" />

    <script type="text/javascript" src="../Scripts/ClientSideDateValidation.js"></script>
    <script src="../Scripts/Calendar/jquery-min-Calendar.js" type="text/javascript"></script>
    <script src="../Scripts/Calendar/jquery-ui.min-Calendar.js" type="text/javascript"></script>
    <link href="../Styles/Calendar/jquery-ui-Calendar.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("[id*=txtBhartDateAppFee]").focus();
            $("[id*=txtBharatKoshDatedPenalty]").focus();
            $("[id*=txtBharatKoshDatedGWCharges]").focus();
        });
        function WaterMarkFocus(txt, text) {
            if (txt.value == text) {
                txt.value = "";
                txt.style.color = "black";
            }
        }

        function WaterMarkBlur(txt, text) {
            if (txt.value == "") {
                txt.value = text;
                txt.style.color = "gray";
            }
        }
        $(function () {
            $("[id*=txtBharatKoshDatedGWCharges]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd/mm/yy',
                minDate: new Date('01/01/2015'),
                maxDate: new Date(),
                changeMonth: true,
                changeYear: true,
                showOn: 'both',
                buttonImage: '../images/calendar.png'
            });
            $("[id*=txtBharatKoshDatedPenalty]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd/mm/yy',
                minDate: new Date('01/01/2015'),
                maxDate: new Date(),
                changeMonth: true,
                changeYear: true,
                showOn: 'both',
                buttonImage: '../images/calendar.png'
            });
            $("[id*=txtBhartDateAppFee]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd/mm/yy',
                minDate: new Date('01/01/2015'),
                maxDate: new Date(),
                changeMonth: true,
                changeYear: true,
                showOn: 'both',
                buttonImage: '../images/calendar.png'
            });


        });
        //$(function () {
        //    $("[id*=txtBharatKoshDatedPenalty]").datepicker({
        //        showOn: 'button',
        //        buttonImageOnly: true,
        //        dateFormat: 'dd/mm/yy',
        //        minDate: new Date('01/01/2015'),
        //        maxDate: new Date(),
        //        changeMonth: true,
        //        changeYear: true,
        //        showOn: 'both',
        //        buttonImage: '../images/calendar.png'
        //    });
        //});


        function SetDecimalFormateGWChargeFinally() {

            if (document.getElementById('<%= txtGWChargeAmountFinally.ClientID %>').value != "") {
                var str = document.getElementById('<%= txtGWChargeAmountFinally.ClientID %>').value.toString();
                if (str.indexOf(".") == -1) {
                    document.getElementById('<%= txtGWChargeAmountFinally.ClientID %>').value = Number(document.getElementById('<%= txtGWChargeAmountFinally.ClientID %>').value) + ".00";
                }
            }
        }
        function SetDecimalFormateGWArearFinally() {

            if (document.getElementById('<%= txtGWArearAmountFinally.ClientID %>').value != "") {
                var str = document.getElementById('<%= txtGWArearAmountFinally.ClientID %>').value.toString();
                if (str.indexOf(".") == -1) {
                    document.getElementById('<%= txtGWArearAmountFinally.ClientID %>').value = Number(document.getElementById('<%= txtGWArearAmountFinally.ClientID %>').value) + ".00";
                }
            }
        }
        function SetDecimalFormatetxtAppFeeFinally() {

            if (document.getElementById('<%= txtAppFee.ClientID %>').value != "") {
                var str = document.getElementById('<%= txtAppFee.ClientID %>').value.toString();
                if (str.indexOf(".") == -1) {
                    document.getElementById('<%= txtAppFee.ClientID %>').value = Number(document.getElementById('<%= txtAppFee.ClientID %>').value) + ".00";
                }
            }
        }
        //Appication Fee
        function BharatKoshRecieptShowWait() {
            if (document.getElementById('<%= txtBharatKoshReciept.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadBharatKoshReciept.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressBharatKoshReciept.ClientID %>')
                x.style.display = 'inline';
            }
        }


        function AbstChargeINDRenewSADShowWait() {
            if (document.getElementById('<%= txtAbstChargeINDRenewSAD.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadAbstChargeINDRenewSAD.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressAbstChargeINDRenewSAD.ClientID %>')
                x.style.display = 'inline';
            }
        }




        //Penalty
        function PenaltyShowWaitINDNewSAD() {
            if (document.getElementById('<%= txtPenaltyINDNewSAD.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadPenaltyINDNewSAD.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressPenaltyINDNewSAD.ClientID %>')
                x.style.display = 'inline';
            }
        }

        // Double Tax Pay
       <%-- function DoubleTaxPayWait() {
            if (document.getElementById('<%= txtAttNameDoubleTax.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadDoubleTaxPay.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressDoubleTaxPay.ClientID %>')
                x.style.display = 'inline';
            }
        }--%>


    </script>
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
            display: inline-block; /*background: url("../Images/SelectedButton.png") no-repeat right top;*/
            background-color: #094E7F;
            padding: 4px 18px 4px 18px;
            color: Black;
            font-weight: bold;
            color: White;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <asp:HiddenField ID="StateCode" runat="server" Value="" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%-- <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>--%>
    <table align="center" class="SubFormWOBG" width="100%">
        <tr>
            <td>Application Type:</td>
            <td>
                <asp:Label runat="server" ID="lblAppType"></asp:Label></td>
        </tr>
        <tr>
            <td>Application Purpose:</td>
            <td>
                <asp:Label runat="server" ID="lblAppPurpose"></asp:Label></td>
        </tr>
        <tr>
            <td>Application Code:</td>
            <td>
                <asp:Label runat="server" ID="lblAppCode"></asp:Label></td>
        </tr>
        <tr>
            <td>Application Name:</td>
            <td>
                <asp:Label runat="server" ID="lblAppName"></asp:Label></td>
        </tr>
        <tr>
            <td>Have you paid Application Fee Offline:</td>
            <td>
                <asp:DropDownList runat="server" ID="ddlPaidFee" AutoPostBack="true" OnSelectedIndexChanged="ddlPaidFee_SelectedIndexChanged">
                    <asp:ListItem Text="--select--" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Direct Bharatkosh" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Via NOCAP" Value="2"></asp:ListItem>
                </asp:DropDownList></td>
        </tr>

        <tr>
            <td colspan="2">
                <asp:Panel runat="server" ID="PanelDoubleTaxPay" Visible="false">
                    <table class="SubFormWOBG" width="100%">
                        <tr>
                            <td valign="top" colspan="2">
                                <asp:Label CssClass="Clicked" runat="server" Text="Double Tax Payment"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style7">State Paid Amount:</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtStatePaidAmt" TextMode="Number" Enabled="true" Width="120px"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtStatePaidAmt"
                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGDoubleTaxPayAttachment">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style7">Paid To Agency:</td>
                            <td>
                                <asp:TextBox runat="server" ID="TxtPaidToAgancy" Enabled="true" Width="120px"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtPaidToAgancy"
                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGDoubleTaxPayAttachment">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style7">Attachment Name :&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="txtAttNameDoubleTax" runat="server" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAttNameDoubleTax"
                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGDoubleTaxPayAttachment">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server"
                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtAttNameDoubleTax" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGDoubleTaxPayAttachment">    </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style7">Select Attachment File :
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanelDoubleTaxPay" runat="server">
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnUploadDoubleTaxPay" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:FileUpload ID="FileUploadDoubleTaxPay" runat="server" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="FileUploadDoubleTaxPay"
                                            Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGDoubleTaxPayAttachment">
                                        </asp:RequiredFieldValidator>
                                        <asp:Button ID="btnUploadDoubleTaxPay" runat="server" Text="Upload" OnClick="btnUploadDoubleTaxPay_Click"
                                            ValidationGroup="VGDoubleTaxPayAttachment" OnClientClick="javascript:DoubleTaxPayWait();" />
                                        <asp:UpdateProgress ID="UpdateProgressDoubleTaxPay" runat="server" AssociatedUpdatePanelID="UpdatePanelDoubleTaxPay">
                                            <ProgressTemplate>
                                                <asp:Label ID="lblDoubleTaxPayWait" runat="server" BackColor="#507CD1" Font-Bold="True"
                                                    ForeColor="White" Text="Please wait ... Uploading file"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvDoubleTax" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="false"
                                    ShowHeaderWhenEmpty="true" AllowSorting="true" AllowPaging="true" PageSize="5"
                                    DataKeyNames="ApplicationCode,SN" Width="100%"
                                    OnRowDeleting="gvDoubleTax_RowDeleting">


                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No.">
                                            <ItemTemplate>
                                                <span>
                                                    <%#Container.DataItemIndex + 1 %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Application Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAppCode" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode"))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Serial Number" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSN" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("SN")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="State Paid Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("StatePaidAmt")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Paid To Agency">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPayToAgency" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("PaidToAgency")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CreatedOn">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreatedOn" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreatedOnByExUC")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="View Uploaded File">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAttachmentName" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttName")) %>'></asp:Label>
                                                <asp:LinkButton ID="lblViewUploadedLinkApplicationFee" OnCommand="ViewFile" runat="server"
                                                    CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode"))+ "," + System.Web.HttpUtility.HtmlEncode(Eval("SN")) %>'>View</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete"
                                                    OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Record Exists.
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblDoubleTax" runat="server"></asp:Label></td>
                        </tr>

                    </table>
                </asp:Panel>
            </td>

        </tr>

        <tr>
            <td colspan="2">
                <asp:Panel runat="server" ID="pnlOnline" Visible="false">
                    <table class="SubFormWOBG" width="100%">
                        <tr>
                            <td>Payment Mode:
                            <br />
                                <asp:Label runat="server" Text="(Once Payment is initiated, it can not be changed)" Font-Bold="true"></asp:Label>

                            </td>

                            <td>
                                <asp:RadioButtonList runat="server" ID="rdBtnPayMode" RepeatDirection="Vertical" AutoPostBack="true"
                                    OnSelectedIndexChanged="rdBtnPayMode_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Text="All Payment in One Combined Transaction (<b>NEFT/RTGS is not allowed</b>)"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Payment in Single-Single Transaction"></asp:ListItem>

                                </asp:RadioButtonList>
                            </td>
                        </tr>



                        <tr>
                            <td colspan="2">
                                <table class="SubFormWOBG" width="100%" runat="server" id="tblOnlinePayment">

                                    <tr>
                                        <td>Application Proccessing Fee:                                  
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblFeeAmout"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="rowChargeOnLine">
                                        <td>Ground Water Charge 
                                        <asp:Label runat="server" ID="lblChargeType"></asp:Label>
                                            <span class='Coumpulsory'>*</span>:
                                  
                                        </td>

                                        <td>
                                            <table class="SubFormWOBG" width="100%">
                                                <tr>
                                                    <%--  <td>Charge Minimum</td>--%>
                                                    <td>Charge</td>
                                                    <td>
                                                        <%-- <a href="../Sub/Report/GWChargesCalculation/GWChargesCalculation.aspx" target="_blank">Know Your Ground Water Charges</a>
                                                        <br />--%>
                                                        <asp:TextBox runat="server" ID="txtGWCharge" Enabled="true"></asp:TextBox>


                                                        <asp:RequiredFieldValidator ID="rfvOnlinePayment" runat="server"
                                                            Display="Dynamic" ControlToValidate="txtGWCharge"
                                                            ForeColor="Red" ValidationGroup="OnlinePayment">Required</asp:RequiredFieldValidator>
                                                        <br />
                                                        <asp:RegularExpressionValidator runat="server" ForeColor="Red" Display="Dynamic"
                                                            ControlToValidate="txtGWCharge" ErrorMessage="Invalid Value. Format should be XXXXXXXXX"
                                                            ValidationGroup="OnlinePayment" ValidationExpression="\d+"></asp:RegularExpressionValidator>



                                                        <br />
                                                        <asp:Label runat="server" ID="lblGWCharge"></asp:Label>
                                                    </td>
                                                    <%-- <td>Maximum</td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblMaxCharge"></asp:Label>

                                                    </td>--%>
                                                </tr>
                                                <tr>
                                                    <td>Arrear
                                  
                                                    </td>

                                                    <td colspan="3">
                                                        <asp:TextBox runat="server" ID="txtGWChargeArear" Height="22px"></asp:TextBox>
                                                        <br />
                                                        <asp:RegularExpressionValidator runat="server" ForeColor="Red" Display="Dynamic"
                                                            ControlToValidate="txtGWChargeArear" ErrorMessage="Invalid Value. Format should be XXXXXXXXX"
                                                            ValidationGroup="OnlinePayment" ValidationExpression="\d+"></asp:RegularExpressionValidator>


                                                    </td>
                                                </tr>
                                            </table>


                                        </td>
                                    </tr>
                                    <tr runat="server" id="rowPenalty" visible="false">
                                        <td>Penalty
                                        <asp:Label runat="server" ID="lblPenaltyType"></asp:Label>
                                            :                                  
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblPenaltyAmount" Height="22px"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button runat="server" ID="PayBtn" Text="Pay" OnClick="PayBtn_Click"
                                                ValidationGroup="OnlinePayment" />

                                        </td>
                                    </tr>


                                </table>
                            </td>

                        </tr>
                        <tr>
                            <td colspan="2">
                                <table class="SubFormWOBG" width="100%" runat="server" id="tblOfflinePayment">
                                    <tr>
                                        <td>
                                            <table class="SubFormWOBG" width="100%">
                                                <tr>
                                                    <td>Application Proccessing Fee:                                  
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="lblOfflineFeeAmout"></asp:TextBox>
                                                    </td>
                                                    <td>NEFT/RTGS:<asp:RadioButtonList runat="server" ID="rdbtnAppFee"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList></td>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnAppFee" Text="Pay"
                                                            OnClick="btnAppFee_Click" />
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="rowChargeOffLine">
                                                    <td>Ground Water Charge 
                                        <asp:Label runat="server" ID="lblOfflineChargeType"></asp:Label>
                                                        <span class='Coumpulsory'>*</span>:
                                  
                                                    </td>

                                                    <td>
                                                        <table class="SubFormWOBG" width="100%">
                                                            <tr>
                                                                <%--  <td>Charge Mininmum:</td>--%>
                                                                <td>Charge:</td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtOffGWCharge" Enabled="true"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvOfflinePayment" runat="server"
                                                                        Display="Dynamic" ControlToValidate="txtOffGWCharge"
                                                                        ForeColor="Red" ValidationGroup="OfflinePayment">Required</asp:RequiredFieldValidator>
                                                                    <br />
                                                                    <asp:RegularExpressionValidator runat="server" ForeColor="Red" Display="Dynamic"
                                                                        ControlToValidate="txtOffGWCharge" ErrorMessage="Invalid Value. Format should be XXXXXXXXX"
                                                                        ValidationGroup="OfflinePayment"
                                                                        ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                                                    <br />
                                                                    <asp:Label runat="server" ID="lblOffGWCharge"></asp:Label>
                                                                    <%--  </td>
                                                                <td>Maximum</td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblMaxOffGWCharge"></asp:Label>

                                                                </td>--%>
                                                            </tr>
                                                            <tr>
                                                                <td>Arrear:</td>
                                                                <td colspan="3">
                                                                    <asp:TextBox runat="server" ID="txtOffLineGWChargeArear" Height="22px"></asp:TextBox>
                                                                    <br />
                                                                    <asp:RegularExpressionValidator runat="server" ForeColor="Red" Display="Dynamic"
                                                                        ControlToValidate="txtOffLineGWChargeArear"
                                                                        ErrorMessage="Invalid Value. Format should be XXXXXXXXX"
                                                                        ValidationGroup="OfflinePayment" ValidationExpression="\d+"></asp:RegularExpressionValidator>

                                                                </td>
                                                            </tr>
                                                        </table>


                                                    </td>
                                                    <td>NEFT/RTGS:
                        
                                               
                                                    <asp:RadioButtonList runat="server" ID="rdbtnCharge"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    </td>

                                                    <td>
                                                        <asp:Button runat="server" ID="btnCharge" Text="Pay"
                                                            ValidationGroup="OfflinePayment" OnClick="btnCharge_Click" />
                                                    </td>


                                                </tr>

                                                <tr runat="server" id="rowOfflinePenalty" visible="false">
                                                    <td>Penalty
                                        <asp:Label runat="server" ID="lblOfflinePenaltyType"></asp:Label>
                                                        :                                  
                                                    </td>
                                                    <td>
                                                        <%--      <asp:Label runat="server" ID="lblOfflinePenaltyAmount"></asp:Label>--%>
                                                        <asp:TextBox runat="server" ID="lblOfflinePenaltyAmount"></asp:TextBox>
                                                    </td>


                                                    <td>NEFT/RTGS:
                        
                                  
                                        <asp:RadioButtonList runat="server" ID="rdbtnPenalty"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnPenalty" Text="Pay" OnClick="btnPenalty_Click" />
                                                    </td>

                                                </tr>
                                            </table>
                                        </td>
                                    </tr>


                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel runat="server" ID="pnlOffline" Visible="false">
                    <table class="SubFormWOBG" width="100%">
                        <tr>
                            <td valign="top">
                                <table class="SubFormWOBG" width="100%">

                                    <tr runat="server">
                                        <td>
                                            <table class="SubFormWOBG" width="100%">
                                                <tr>
                                                    <td>
                                                        <table class="SubFormWOBG" width="100%">
                                                            <tr>
                                                                <td valign="top" colspan="2">
                                                                    <asp:Label runat="server" ID="lblBharatKosh2">
                                       <span class="Coumpulsory">*</span>
                                                                    </asp:Label>
                                                                    <asp:Label ID="lblBharatKosh" CssClass="Clicked" runat="server" Text="Bharat Kosh Reciept (Application Fee)"></asp:Label>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td><b>Application Fee Amount:</b> </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAppFee" Enabled="false" runat="server" onblur="SetDecimalFormatetxtAppFeeFinally();"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAppFee" Display="Dynamic" ErrorMessage="Required"
                                                                        ForeColor="Red" ValidationGroup="VGBharatKoshRecieptAttachment"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator runat="server"
                                                                        ControlToValidate="txtAppFee" Display="Dynamic" ErrorMessage="Invalid Value. Format should be XXXXXXXXX.XX"
                                                                        ForeColor="Red" ValidationExpression="^(\d*\.)?\d+$" ValidationGroup="VGBharatKoshRecieptAttachment"></asp:RegularExpressionValidator>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>Bharat Kosh Transaction Ref. No:- </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtBhartRefAppFee" runat="server" MaxLength="49"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBhartRefAppFee" Display="Dynamic" ErrorMessage="Required"
                                                                        ForeColor="Red" ValidationGroup="VGBharatKoshRecieptAttachment"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtBhartRefAppFee"
                                                                        Display="Dynamic" ErrorMessage="Only Numeric values are allowed" ForeColor="Red" ValidationExpression="[0-9]*" ValidationGroup="VGBharatKoshRecieptAttachment"></asp:RegularExpressionValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Bharat Kosh Transaction Dated: </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtBhartDateAppFee" runat="server"
                                                                        onfocus="WaterMarkFocus(this,'dd/mm/yyyy')" onblur="WaterMarkBlur(this,'dd/mm/yyyy')"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtBhartDateAppFee"
                                                                        Display="Dynamic" ErrorMessage="Invalid Date Format" ForeColor="Red"
                                                                        ValidationExpression="^([0]?[0-9]|[12][0-9]|[3][01])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$"
                                                                        ValidationGroup="VGBharatKoshRecieptAttachment"></asp:RegularExpressionValidator>
                                                                    <br />
                                                                    <asp:RangeValidator ID="rngAppFee" runat="server" ControlToValidate="txtBhartDateAppFee"
                                                                        ErrorMessage="Date should be grater than 01/01/2015 and less than or equal to current date." ForeColor="Red"
                                                                        MinimumValue="01-01-2015" Type="Date"
                                                                        ValidationGroup="VGBharatKoshRecieptAttachment" />
                                                                </td>
                                                            </tr>



                                                            <tr>
                                                                <td>Attachment Name :
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtBharatKoshReciept" runat="server" MaxLength="50"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBharatKoshReciept"
                                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGBharatKoshRecieptAttachment">
                                                                    </asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtBharatKoshReciept" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                        ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGBharatKoshRecieptAttachment"></asp:RegularExpressionValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Select Attachment File :
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel ID="UpdatePanelBharatKoshReciept" runat="server">
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="btnUploadBharatKoshReciept" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            <asp:FileUpload ID="FileUploadBharatKoshReciept" runat="server" />
                                                                            <asp:Button ID="btnUploadBharatKoshReciept" runat="server" Text="Upload" OnClick="btnUploadBharatKoshReciept_Click"
                                                                                ValidationGroup="VGBharatKoshRecieptAttachment" OnClientClick="javascript:BharatKoshRecieptShowWait();" />
                                                                            <asp:UpdateProgress ID="UpdateProgressBharatKoshReciept" runat="server" AssociatedUpdatePanelID="UpdatePanelBharatKoshReciept">
                                                                                <ProgressTemplate>
                                                                                    <asp:Label ID="lblBharatKoshRecieptWait" runat="server" BackColor="#507CD1" Font-Bold="True"
                                                                                        ForeColor="White" Text="Please wait ... Uploading file"></asp:Label>
                                                                                </ProgressTemplate>
                                                                            </asp:UpdateProgress>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="gvApplicationFee" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="false"
                                                            ShowHeaderWhenEmpty="true" AllowSorting="true" AllowPaging="true" PageSize="5"
                                                            DataKeyNames="ApplicationCode,SN"
                                                            OnSorting="gvApplicationFee_Sorting"
                                                            OnRowDeleting="BharatKoshReciept_RowDeleting">


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
                                                                        <asp:LinkButton ID="lblViewUploadedLinkApplicationFee" OnCommand="DownloadOrViewFiles" runat="server"
                                                                            CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode"))+ "," + System.Web.HttpUtility.HtmlEncode(Eval("SN")) %>'>View</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete"
                                                                            OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                No Record Exists.
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>



                                                <tr>
                                                    <td>
                                                        <table class="SubFormWOBG" width="100%">
                                                            <tr>
                                                                <td valign="top" colspan="2">
                                                                    <%-- <asp:Label runat="server" Visible="false" ID="lblPenaltyINDNewSAD">
                                        <span class="Coumpulsory">*</span>
                                                                    </asp:Label>--%>
                                                                    <asp:Label CssClass="Clicked" runat="server" Text="Penalty"></asp:Label>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td><b>Penalty Amount</b></td>
                                                                <td>

                                                                    <asp:TextBox runat="server" ID="txtPenaltyFinally" onblur="SetDecimalFormatePCFinally();"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPenaltyFinally"
                                                                        ForeColor="Red" ValidationGroup="Penalty" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>

                                                                    <asp:RegularExpressionValidator runat="server" ForeColor="Red" Display="Dynamic"
                                                                        ControlToValidate="txtPenaltyFinally" ErrorMessage="Invalid Value. Format should be XXXXXXXXX.XX"
                                                                        ValidationGroup="Submit" ValidationExpression="^(\d*\.)?\d+$"></asp:RegularExpressionValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Bharat Kosh Transaction Ref. No:- </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtBharatKoshRefferenceNoPenalty" runat="server" MaxLength="49"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvtxtBharatKoshRefferenceNoPenalty" runat="server" ControlToValidate="txtBharatKoshRefferenceNoPenalty"
                                                                        ForeColor="Red" ValidationGroup="Penalty" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>

                                                                    <asp:RegularExpressionValidator ID="revtxtBharatKoshRefferenceNoPenalty" runat="server"
                                                                        ValidationExpression="[0-9]*" ErrorMessage="Only Numeric values are allowed"
                                                                        ValidationGroup="Penalty" Display="Dynamic" ForeColor="Red" ControlToValidate="txtBharatKoshRefferenceNoPenalty"></asp:RegularExpressionValidator></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Bharat Kosh Transaction Dated: </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtBharatKoshDatedPenalty" runat="server"
                                                                        onfocus="WaterMarkFocus(this,'dd/mm/yyyy')" onblur="WaterMarkBlur(this,'dd/mm/yyyy')"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator runat="server" ValidationExpression="^([0]?[0-9]|[12][0-9]|[3][01])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$"
                                                                        ErrorMessage="Invalid Date Format" ValidationGroup="Penalty" Display="Dynamic"
                                                                        ForeColor="Red" ControlToValidate="txtBharatKoshDatedPenalty"></asp:RegularExpressionValidator>
                                                                    <br />
                                                                    <asp:RangeValidator ForeColor="Red" ValidationGroup="Penalty" runat="server"
                                                                        ID="rngPenalty" ControlToValidate="txtBharatKoshDatedPenalty" Type="Date" MinimumValue="01-01-2015"
                                                                        ErrorMessage="Date should be grater than 01/01/2015 and less than or equal to current date." />
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>Attachment Name :
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPenaltyINDNewSAD" runat="server" MaxLength="50"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPenaltyINDNewSAD"
                                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="Penalty"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator runat="server" Display="Dynamic"
                                                                        ForeColor="Red" ControlToValidate="txtPenaltyINDNewSAD" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                        ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="Penalty"></asp:RegularExpressionValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Select Attachment File :
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel ID="UpdatePanelPenaltyINDNewSAD" runat="server">
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="btnUploadPenaltyINDNewSAD" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            <asp:FileUpload ID="FileUploadPenaltyINDNewSAD" runat="server" />
                                                                            <asp:Button ID="btnUploadPenaltyINDNewSAD" runat="server" Text="Upload" OnClick="btnUploadPenalty_Click"
                                                                                ValidationGroup="Penalty" OnClientClick="javascript:PenaltyShowWaitINDNewSAD();" />
                                                                            <asp:UpdateProgress ID="UpdateProgressPenaltyINDNewSAD" runat="server" AssociatedUpdatePanelID="UpdatePanelPenaltyINDNewSAD">
                                                                                <ProgressTemplate>
                                                                                    <asp:Label ID="lblPenaltyWaitINDNewSAD" runat="server" BackColor="#507CD1" Font-Bold="True"
                                                                                        ForeColor="White" Text="Please wait ... Uploading file"></asp:Label>

                                                                                </ProgressTemplate>
                                                                            </asp:UpdateProgress>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="gvPenalty" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="false"
                                                            ShowHeaderWhenEmpty="true" AllowSorting="true" AllowPaging="true" PageSize="5"
                                                            align="center" DataKeyNames="ApplicationCode,PenaltySN,SN"
                                                            OnSorting="gvPenalty_Sorting"
                                                            OnRowDeleting="gvPenalty_RowDeleting">

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
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
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
                                            <asp:Panel runat="server" ID="pnBharatkoshGWAC">
                                                <table class="SubFormWOBG" width="100%">
                                                    <tr runat="server">
                                                        <td>
                                                            <table class="SubFormWOBG" width="100%">

                                                                <tr>
                                                                    <td>
                                                                        <table class="SubFormWOBG" width="100%">
                                                                            <tr>
                                                                                <td valign="top" colspan="2">
                                                                                    <asp:Label runat="server" ID="lblAbstChargeINDRenewSAD"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                                    <asp:Label ID="lblINDRenGWC" CssClass="Clicked" runat="server" Text="Bharatkosh Reciept (Ground Water Abstraction Charges)"></asp:Label>
                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td><b>GW Charge Amount:</b>
                                                                                </td>
                                                                                <td>

                                                                                    <asp:TextBox runat="server" ID="txtGWChargeAmountFinally" onblur="SetDecimalFormateGWChargeFinally();"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator runat="server" ForeColor="Red" Display="Dynamic"
                                                                                        ControlToValidate="txtGWChargeAmountFinally" ErrorMessage="Invalid Value. Format should be XXXXXXXXX.XX"
                                                                                        ValidationGroup="GWCharges" ValidationExpression="^(\d*\.)?\d+$"></asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>GW Arear Amount:</b>
                                                                                </td>
                                                                                <td>

                                                                                    <asp:TextBox runat="server" ID="txtGWArearAmountFinally" onblur="SetDecimalFormateGWArearFinally();"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator runat="server" ForeColor="Red" Display="Dynamic"
                                                                                        ControlToValidate="txtGWArearAmountFinally" ErrorMessage="Invalid Value. Format should be XXXXXXXXX.XX"
                                                                                        ValidationGroup="GWCharges" ValidationExpression="^(\d*\.)?\d+$"></asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Bharat Kosh Transaction Ref. No:- </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtBharatKoshRefferenceNoGWCharges" runat="server" MaxLength="49"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="rfvtxtBharatKoshRefferenceNoGWCharges" runat="server" ControlToValidate="txtBharatKoshRefferenceNoGWCharges"
                                                                                        ForeColor="Red" ValidationGroup="GWCharges" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>

                                                                                    <asp:RegularExpressionValidator ID="revtxtBharatKoshRefferenceNoGWCharges" runat="server"
                                                                                        ValidationExpression="[0-9]*" ErrorMessage="Only Numeric values are allowed"
                                                                                        ValidationGroup="GWCharges" Display="Dynamic" ForeColor="Red" ControlToValidate="txtBharatKoshRefferenceNoGWCharges"></asp:RegularExpressionValidator></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Bharat Kosh Transaction Dated: </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtBharatKoshDatedGWCharges" runat="server"
                                                                                        onfocus="WaterMarkFocus(this,'dd/mm/yyyy')" onblur="WaterMarkBlur(this,'dd/mm/yyyy')"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator runat="server" ValidationExpression="^([0]?[0-9]|[12][0-9]|[3][01])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$"
                                                                                        ErrorMessage="Invalid Date Format" ValidationGroup="GWCharges" Display="Dynamic"
                                                                                        ForeColor="Red" ControlToValidate="txtBharatKoshDatedGWCharges"></asp:RegularExpressionValidator>
                                                                                    <br />
                                                                                    <asp:RangeValidator ForeColor="Red" ValidationGroup="GWCharges" runat="server"
                                                                                        ID="rngDate" ControlToValidate="txtBharatKoshDatedGWCharges" Type="Date" MinimumValue="01-01-2015"
                                                                                        ErrorMessage="Date should be grater than 01/01/2015 and less than or equal to current date." />


                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td>Attachment Name :
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtAbstChargeINDRenewSAD" runat="server" MaxLength="50"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAbstChargeINDRenewSAD"
                                                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGAbstRestChargeAttachment">
                                                                                    </asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator runat="server"
                                                                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtAbstChargeINDRenewSAD" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                                        ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGAbstRestChargeAttachment"></asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td>Select Attachment File :
                                                                                </td>
                                                                                <td>
                                                                                    <asp:UpdatePanel ID="UpdatePanelAbstChargeINDRenewSAD" runat="server">
                                                                                        <Triggers>
                                                                                            <asp:PostBackTrigger ControlID="btnUploadAbstChargeINDRenewSAD" />
                                                                                        </Triggers>
                                                                                        <ContentTemplate>
                                                                                            <asp:FileUpload ID="FileUploadAbstChargeINDRenewSAD" runat="server" />
                                                                                            <asp:Button ID="btnUploadAbstChargeINDRenewSAD" runat="server" Text="Upload" OnClick="btnUploadAbstCharge_Click"
                                                                                                ValidationGroup="VGAbstRestChargeAttachment" OnClientClick="javascript:AbstChargeINDRenewSADShowWait();" />
                                                                                            <asp:UpdateProgress ID="UpdateProgressAbstChargeINDRenewSAD" runat="server" AssociatedUpdatePanelID="UpdatePanelAbstChargeINDRenewSAD">
                                                                                                <ProgressTemplate>
                                                                                                    <asp:Label ID="lblAbstChargeINDRenewSADWait" runat="server" BackColor="#507CD1" Font-Bold="True"
                                                                                                        ForeColor="White" Text="Please wait ... Uploading file"></asp:Label>
                                                                                                </ProgressTemplate>
                                                                                            </asp:UpdateProgress>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>



                                                            </table>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="gvGWCharges" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="false"
                                                                ShowHeaderWhenEmpty="true" AllowSorting="true" AllowPaging="true" PageSize="5"
                                                                align="center" DataKeyNames="ApplicationCode,SN"
                                                                OnSorting="gvGWCharges_Sorting"
                                                                OnRowDeleting="gvGWCharges_RowDeleting">

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
                                                                    <asp:TemplateField HeaderText="Delete">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete"
                                                                                OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
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
                                            </asp:Panel>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Button Text="Next >>" runat="server" ID="btnNext" OnClick="btnNext_Click" />

                                        </td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <b>Note:IF APPLICABLE PENALTY WILL BE COMMUNICATED AFTER FINAL SCRUTINY OF APPLICATION VIA EMAIL</b></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessageBharatKoshReciept" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                <asp:Label ID="lblApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

