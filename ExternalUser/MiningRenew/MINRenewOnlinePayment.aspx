<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" CodeFile="MINRenewOnlinePayment.aspx.cs" Inherits="ExternalUser_MiningRenew_MINRenewOnlinePayment" %>


<asp:content id="Content1" contentplaceholderid="head" runat="Server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="Server">
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
                                            <li class="visited">Existing NOC Details</li>
                                            <li class="visited">Land Use Details</li>
                                            <li class="visited">Dewatering Existing Structure</li>
                                            <li class="visited">Dewatering Additional Structure</li>
                                            <li class="visited">Utilization of pumped water</li>
                                            <li class="visited">Monitoring of groundwater regime</li>
                                            <li class="visited">Groundwater Abstraction Structure- Existing</li>
                                            <li class="visited">Groundwater Abstraction Structure- Additional</li>
                                            <li class="visited">Compliance Conditions in the NOC</li>
                                            <li class="visited">Compliance Conditions in the NOC - Other</li>
                                            <li class="visited">Other Details</li>
                                          
                                            <li class="visited">Attachment</li>
                                              <li class="active">Ready To Submit</li>
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
                                Mininmg USE: 1. Payment
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
                        
                        </td>

                        <td>
                            <asp:RadioButtonList runat="server" ID="rdBtnPayMode" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdBtnPayMode_SelectedIndexChanged">
                                <%--       <asp:ListItem Value="0" Text="OffLine"></asp:ListItem>
                                <asp:ListItem Value="1" Text="OnLine" Selected="True"></asp:ListItem>--%>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table class="SubFormWOBG" width="100%" runat="server" id="tblOnlinePayment">
                                <tr>
                                    <td>NEFT/RTGS:
                        
                                    </td>

                                    <td>
                                        <asp:RadioButtonList runat="server" ID="rdBtnNEFTRTGS" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="No" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
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
                                <tr runat="server" id="rowPenalty">
                                    <td>Penalty
                                        <asp:Label runat="server" ID="lblPenaltyType"></asp:Label>:                                  
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblPenaltyAmount"></asp:Label>
                                    </td>
                                </tr>


                                <%-- <tr>
                                    <td>Total Amount:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblTotalAmt"></asp:Label>
                                    </td>
                                </tr>--%>
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
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblMiningApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                              <asp:Label ID="lblValidityEndDate" runat="server" Text="Label" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="2">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev"
                                OnClientClick="return confirm('If you have not saved data, Your data will be lost before moving to other page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnNext" runat="server" Text="Next >>"  OnClick="btnNext_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:content>

