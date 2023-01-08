<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    CodeFile="PayAppSumbit.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="ExternalUser_PayAppSumbit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Scripts/ClientSideDateValidation.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 217px;
        }

        .auto-style2 {
            width: 672px;
        }

        .auto-style3 {
            /*	margin:0px;padding:0px;
	min-width:50%;*/
            width: 213%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table align="center" class="SubFormWOBG" width="100%">
        <tr>
            <td colspan="2">
                <div class="div_IndAppHeading" style="padding-left: 10px; text-align: center">
                    Make Payment
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">Application Type :
            </td>
            <td style="border-left: 0px solid red; border-right: 0px solid red; width: 15%">
                <asp:DropDownList ID="ddlApplicationType" runat="server" Width="200px" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlApplicationType_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">Application Name: </td>

            <td style="text-align: left">
                <asp:DropDownList runat="server" ID="ddlApplicationNumber" Width="200px" Enabled="false" OnSelectedIndexChanged="ddlApplicationNumber_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

            </td>
        </tr>



        <tr>
            <td>Application Type:</td>
            <td>
                <asp:Label runat="server" ID="lblAppType"></asp:Label></td>
        </tr>
        <tr>
            <td>Application Purpose:</td>
            <td>
                <asp:Label runat="server" ID="lblAppPur"></asp:Label></td>
        </tr>
        <tr>
            <td>Application Code:</td>
            <td>
                <asp:Label runat="server" ID="lblAppCode"></asp:Label></td>
        </tr>
        <tr>
            <td>Application Number:</td>
            <td>
                <asp:Label runat="server" ID="lblAppNo"></asp:Label></td>
        </tr>
        <tr>
            <td>Application Name:</td>
            <td>
                <asp:Label runat="server" ID="lblAppName"></asp:Label></td>
        </tr>
        <tr>
            <td class="auto-style2">Payment Mode:
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
        <asp:Panel runat="server" ID="pnlPaymentDetail" Visible="true">

            <tr>
                <td colspan="2">

                    <table class="SubFormWOBG" width="100%" runat="server" id="tblOfflinePayment">
                        <tr>
                            <td>
                                <table class="SubFormWOBG" width="100%">
                                    <tr>
                                        <td>Ground Water Charge 
                                         <asp:Label runat="server" ID="lblOfflineChargeType"></asp:Label>
                                            <span class='Coumpulsory'>*</span>:
                                        </td>
                                        <td>




                                            <asp:TextBox runat="server" ID="txtGWCharge">

                                            </asp:TextBox>Charge
                                              
                                      
                                        <br />
                                            <asp:RequiredFieldValidator ID="rfvGWCharge" runat="server" ForeColor="Red" Enabled="false"
                                                Display="Dynamic" ValidationGroup="Payment" ControlToValidate="txtGWCharge">Required</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ForeColor="Red" Display="Dynamic"
                                                ControlToValidate="txtGWCharge" ErrorMessage="Invalid Value. Format should be XXXXXXXXX"
                                                ValidationGroup="Payment"
                                                ValidationExpression="^(\d*\.)?\d+$"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:TextBox runat="server" ID="txtGWChargeArear" Height="22px">

                                            </asp:TextBox>Arrear
                                                     <br />
                                            <asp:RequiredFieldValidator ID="rfvGWChargeArear" runat="server" ForeColor="Red" Enabled="false"
                                                Display="Dynamic" ValidationGroup="Payment" ControlToValidate="txtGWChargeArear">Required</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ForeColor="Red" Display="Dynamic"
                                                ControlToValidate="txtGWChargeArear"
                                                ErrorMessage="Invalid Value. Format should be XXXXXXXXX"
                                                ValidationGroup="Payment" ValidationExpression="^(\d*\.)?\d+$">
                                            </asp:RegularExpressionValidator>



                                        </td>
                                        <td>
                                            <table class="SubFormWOBG" width="100%" runat="server"
                                                id="singleCharge">
                                                <tr>
                                                    <td>NEFT/RTGS:
                        
                                               
                                                    <asp:RadioButtonList runat="server" ID="rdbtnCharge"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList></td>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnCharge" Text="Pay Charge"
                                                            ValidationGroup="Payment" OnClick="btnCharge_Click" /></td>
                                                </tr>
                                            </table>


                                        </td>
                                    </tr>
                                    <tr runat="server" id="rowPenalty">
                                        <td>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <b>LIST OF PENALITIES</b>
                                                        <br />
                                                        <br />
                                                        <asp:GridView ID="gvPenalty" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                                                            AutoGenerateColumns="false" EmptyDataText="No records."
                                                            Width="100%"
                                                            DataKeyNames="PenaltyCode">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="5px">
                                                                    <ItemTemplate>
                                                                        <span>
                                                                            <%#Container.DataItemIndex + 1 %>
                                                                        </span>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PenaltyCode" ItemStyle-Width="15px" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPenaltyCode" runat="server" Text='<%# Eval("PenaltyCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Penalty Desc" ItemStyle-Width="350px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPenaltyDesc" runat="server" Text='<%# Eval("PenaltyDesc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rate" ItemStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>

                                                </tr>

                                            </table>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPenaltyAmount" runat="server" Font-Bold="True" Style="color: Red; text-align: right; font-weight: bold; margin-left: 0PX; margin-right: 0px;">

                                            </asp:TextBox>Total For Penalty
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvPenaltyAmount" runat="server" ForeColor="Red" Enabled="false"
                                                Display="Dynamic" ValidationGroup="Payment" ControlToValidate="txtPenaltyAmount">Required</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ForeColor="Red" Display="Dynamic"
                                                ControlToValidate="txtPenaltyAmount" ErrorMessage="Invalid Value. Format should be XXXXXXXXX"
                                                ValidationGroup="Payment"
                                                ValidationExpression="^(\d*\.)?\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <table class="SubFormWOBG" width="100%"
                                                runat="server" id="singlePenalty">
                                                <tr>
                                                    <td>NEFT/RTGS:
                        
                                  
                                        <asp:RadioButtonList runat="server" ID="rdbtnPenalty"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnPenalty" ValidationGroup="Payment" Text="Pay Penalty" OnClick="btnPenalty_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="rowModification">
                                        <td><b>LIST OF MODIFICATION/CORRECTION</b>
                                        <br />
                                            <br />
                                            <asp:GridView ID="gvCorrection" runat="server"
                                                AutoGenerateColumns="false" EmptyDataText="No records Found."
                                                DataKeyNames="CorrectionChargeCode"
                                                Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="5px">
                                                        <ItemTemplate>
                                                            <span>
                                                                <%#Container.DataItemIndex + 1 %>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CorrectionChargeCode" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCorrectionChargeCode" runat="server" Text='<%# Eval("CorrectionChargeCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CorrectionChargeDesc" ItemStyle-Width="300px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCorrectionChargeDesc" runat="server" Text='<%# Eval("CorrectionChargeDesc") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate" ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LalCorr" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView></td>
                                        <td>
                                            <asp:TextBox ID="txtCorrAmount" runat="server" Font-Bold="True" Style="color: Red; text-align: right; font-weight: bold; margin-left: 0PX; margin-right: 0px;">

                                            </asp:TextBox>Total For Correction
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvCorrAmount" runat="server" ForeColor="Red" Enabled="false"
                                                Display="Dynamic" ValidationGroup="Payment" ControlToValidate="txtCorrAmount">Required</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ForeColor="Red" Display="Dynamic"
                                                ControlToValidate="txtCorrAmount" ErrorMessage="Invalid Value. Format should be XXXXXXXXX"
                                                ValidationGroup="Payment"
                                                ValidationExpression="^(\d*\.)?\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <table class="SubFormWOBG" width="100%"
                                                runat="server" id="singleCorrection">


                                                <tr>
                                                    <td>NEFT/RTGS:
                        
                                  
                                                    <asp:RadioButtonList runat="server" ID="rdbtnCorrection"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnCorrection" ValidationGroup="Payment" Text="Pay Correction" OnClick="btnCorrection_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="rowEC">
                                        <td>Environmental Compensation(EC)</td>
                                        <td>
                                            <asp:TextBox ID="txtEC" runat="server" Font-Bold="True" Style="color: Red; text-align: right; font-weight: bold; margin-left: 0PX; margin-right: 0px;">

                                            </asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvEC" runat="server" ForeColor="Red" Enabled="false"
                                                Display="Dynamic" ValidationGroup="Payment" ControlToValidate="txtEC">Required</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ForeColor="Red" Display="Dynamic"
                                                ControlToValidate="txtEC" ErrorMessage="Invalid Value. Format should be XXXXXXXXX"
                                                ValidationGroup="Payment"
                                                ValidationExpression="^(\d*\.)?\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <table class="SubFormWOBG" width="100%"
                                                runat="server" id="singleEC">


                                                <tr>
                                                    <td>NEFT/RTGS:
                        
                                  
                                                    <asp:RadioButtonList runat="server" ID="rdbtnEC"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnEC" ValidationGroup="Payment" Text="Pay EC" OnClick="btnEC_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
                        <tr>

                            <td style="text-align: center" colspan="3">
                                <asp:Button runat="server" ID="PayBtn" Text="Pay All" OnClick="PayBtn_Click" ValidationGroup="Payment" />

                            </td>
                        </tr>



                    </table>
                </td>
            </tr>

        </asp:Panel>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
            </td>
        </tr>

    </table>
</asp:Content>
