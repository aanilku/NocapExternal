<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" CodeFile="INDExpansionOnlinePayment.aspx.cs" Inherits="ExternalUser_Expansion_IND_INDExpansionOnlinePayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <table align="center" class="SubFormWOBG" width="100%">
        <tr>
            <td style="width: 200px">
                <table width="100%">
                    <tr>
                        <td>
                            <div class="middle">
                                <div class="block_left_inner">
                                    <div id="information" class="cont_left" style="display: block">
                                        <ul class="progressbar">
                                            <li class="visited">Location Details</li>
                                            <li class="visited">Communication Address</li>
                                            <li class="visited">Land Use Details</li>
                                            <li class="visited">Water Requirement Details</li>
                                            <li class="visited">Recycled Water Usage</li>
                                            <li class="visited">Groundwater Abstraction Structure- Existing</li>
                                            <li class="visited">Groundwater Abstraction Structure- Proposed</li>
                                            <li class="visited">Other Details</li>
                                            <%--       <li class="visited">Self Declaration</li>--%>
                                            <li class="visited">Attachment</li>
                                           <li >Ready To Submit</li>
                                            <li>Final Submit</li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table class="SubFormWOBG" width="100%" style="line-height: 25px">
                    <tr>
                        <td colspan="2">
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                INDUSTRIAL EXPANSION USE: 1. Payment
                            </div>
                        </td>
                    </tr>
                    <%--  <tr>
                        <td style="text-align: right" colspan="2">(<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-
                            Upload Attachments in <strong>Attachment</strong> Section
                        </td>
                    </tr>--%>




                    <tr>
                        <td>Payment Mode:
                            <br />
                           <asp:Label runat="server" Text="(Once Transaction is initiated, it can not be changed)" Font-Bold="true"></asp:Label> 
                        
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
                                        <asp:Label runat="server" ID="lblFeeAmout"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Ground Water Charge 
                                        <asp:Label runat="server" ID="lblChargeType"></asp:Label>
                                        <span class='Coumpulsory'>*</span>:
                                  
                                    </td>

                                    <td>
                                        <asp:TextBox runat="server" ID="txtGWCharge"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvOnlinePayment" runat="server"
                                            Display="Dynamic" ControlToValidate="txtGWCharge"
                                            ForeColor="Red" ValidationGroup="OnlinePayment">Required</asp:RequiredFieldValidator>
                                        <br />
                                        <asp:RegularExpressionValidator runat="server" ForeColor="Red" Display="Dynamic"
                                            ControlToValidate="txtGWCharge" ErrorMessage="Invalid Value. Format should be XXXXXXXXX.XX"
                                            ValidationGroup="OnlinePayment" ValidationExpression="^(\d*\.)?\d+$"></asp:RegularExpressionValidator>



                                    </td>
                                </tr>
                                <tr>
                                    <td>Ground Water Charge Arear
                                  
                                    </td>

                                    <td>
                                        <asp:TextBox runat="server" ID="txtGWChargeArear" Height="22px"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator runat="server" ForeColor="Red" Display="Dynamic"
                                            ControlToValidate="txtGWChargeArear" ErrorMessage="Invalid Value. Format should be XXXXXXXXX.XX"
                                            ValidationGroup="OnlinePayment" ValidationExpression="^(\d*\.)?\d+$"></asp:RegularExpressionValidator>


                                    </td>
                                </tr>
                                <tr runat="server" id="rowPenalty" visible="false">
                                    <td>Penalty
                                        <asp:Label runat="server" ID="lblPenaltyType"></asp:Label>:                                  
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblPenaltyAmount"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Pay:</td>
                                    <td>
                                        <asp:Button runat="server" ID="PayBtn" Text="Pay" OnClick="PayBtn_Click" ValidationGroup="OnlinePayment" />

                                    </td>
                                </tr>
                            </table>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="2">
                            <table class="SubFormWOBG" width="100%" runat="server" id="tblOfflinePayment">
                                <tr>
                                    <td>Application Proccessing Fee:                                  
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblOfflineFeeAmout"></asp:Label>
                                    </td>
                                    <td>NEFT/RTGS:<asp:RadioButtonList runat="server" ID="rdbtnAppFee"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList></td>
                                    <td>
                                        <asp:Button runat="server" ID="btnAppFee" Text="Pay"  OnClick="btnAppFee_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table class="SubFormWOBG" width="100%">
                                            <tr>

                                                <td>Ground Water Charge 
                                        <asp:Label runat="server" ID="lblOfflineChargeType"></asp:Label>
                                                    <span class='Coumpulsory'>*</span>:
                                  
                                                </td>

                                                <td>
                                                    <asp:TextBox runat="server" ID="txtOffGWCharge"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvOfflinePayment" runat="server"
                                                        Display="Dynamic" ControlToValidate="txtOffGWCharge"
                                                        ForeColor="Red" ValidationGroup="OnlinePayment">Required</asp:RequiredFieldValidator>
                                                    <br />
                                                    <asp:RegularExpressionValidator runat="server" ForeColor="Red" Display="Dynamic"
                                                        ControlToValidate="txtOffGWCharge" ErrorMessage="Invalid Value. Format should be XXXXXXXXX.XX"
                                                        ValidationGroup="OnlinePayment" ValidationExpression="^(\d*\.)?\d+$"></asp:RegularExpressionValidator>



                                                </td>



                                            </tr>
                                            <tr>
                                                <td>Ground Water Charge Arear
                                  
                                                </td>

                                                <td>
                                                    <asp:TextBox runat="server" ID="txtOffLineGWChargeArear" Height="22px"></asp:TextBox>
                                                    <br />
                                                    <asp:RegularExpressionValidator runat="server" ForeColor="Red" Display="Dynamic"
                                                        ControlToValidate="txtOffLineGWChargeArear" ErrorMessage="Invalid Value. Format should be XXXXXXXXX.XX"
                                                        ValidationGroup="OnlinePayment" ValidationExpression="^(\d*\.)?\d+$"></asp:RegularExpressionValidator>


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
                                        <asp:Button runat="server" ID="btnCharge" Text="Pay" ValidationGroup="OnlinePayment" OnClick="btnCharge_Click" />
                                    </td>
                                </tr>
                                <tr runat="server" id="rowOfflinePenalty" visible="false">
                                    <td>Penalty
                                        <asp:Label runat="server" ID="lblOfflinePenaltyType"></asp:Label>:                                  
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblOfflinePenaltyAmount"></asp:Label>
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
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblIndustialApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="2">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev"
                                OnClientClick="return confirm('If you have not saved data, Your data will be lost before moving to other page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnNext" runat="server" Text="Next >>" OnClick="btnNext_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

