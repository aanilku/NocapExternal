<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="ComplianceConditionNOC.aspx.cs"
    Inherits="ExternalUser_IndustrialRenew_ComplianceConditionNOC" %>

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
                                            <li class="visited">Existing NOC Details</li>
                                            <li class="visited">Water Requirement Details</li>
                                            <li class="visited">Recycled Water Usage</li>
                                            <li class="visited">Groundwater Abstraction Structure- Existing</li>
                                            <li class="visited">Groundwater Abstraction Structure- Additional</li>
                                            <li class="active">Compliance Conditions in the NOC</li>
                                            <li >Compliance Conditions in the NOC - Other</li>
                                            <li >Other Details</li>
                                           <%-- <li>Self Declaration</li>--%>
                                            <li >Attachment</li>
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
                                RENEW - INDUSTRIAL USE: 4. Compliance to the Conditions prescribed in the NOC
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormProjName">
                            <b>Project Name:&nbsp;
                                <asp:Label ID="lblHeadingProjName" runat="server"></asp:Label></b>
                        </td>
                    </tr>
                    <%-- <tr align="center">
                        <td colspan="2" >
                          <center>
                           <table>
                           <tr>
                           <td>
                         Select  Compliance Type:
                           </td>
                             <td>
                                 <asp:DropDownList ID="ddlComplianceType" runat="server" Width="255px">
                                 </asp:DropDownList>
                           </td>
                           </tr>
                            <tr>
                           <td valign="top">
                          Status of Compliance:
                           </td>
                             <td>
                           <asp:TextBox ID="txtStatusofCompliance" runat="server" TextMode="MultiLine" Width="250px" Height="50px"></asp:TextBox>
                           </td>
                           </tr>
                            <tr>
                           <td colspan="2" align="center">

                           </td>
                           </tr>

                            <tr>
                           <td align="center">
                               <asp:Label ID="lblMsgComConAdd" runat="server" ></asp:Label>
                             </td>
                            <td align="center">
                           <asp:Button ID="btnComConAdd" runat="server" Text="Add" 
                                   onclick="btnComConAdd_Click"   />
                           </td>
                             
                           </tr>
                           </table>
                           </center>
                         
                         </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <b>a). Compliance to the Conditions prescribed in the NOC</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvCompCondNOC" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                align="center" DataKeyNames="ComplianceConditionCode" Width="85%" ShowHeaderWhenEmpty="True"
                                OnRowDataBound="gvCompCondNOC_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <span>
                                                <%#Container.DataItemIndex + 1 %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Condition give in NOC" HeaderStyle-Width="50%" ItemStyle-Width="50%"
                                        ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompCondCode" runat="server" Visible="false" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ComplianceConditionCode")) %>'></asp:Label>
                                            <asp:Label ID="lblCompCondDesc" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ComplianceConditionDescription")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Compliance Conditions Applicable" HeaderStyle-Width="8%"
                                        ItemStyle-Width="8%" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlCompCondApplicable" runat="server">
                                                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                <asp:ListItem Text="Not Applicable" Value="A"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status of Compliance" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtStatusOfCompliance" runat="server" TextMode="MultiLine" Width="310px"
                                                Height="50px"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="revtxtStatusOfCompliance" runat="server" Display="Dynamic"
                                                ForeColor="Red" ControlToValidate="txtStatusOfCompliance" ValidationGroup="Compliance"></asp:RegularExpressionValidator>
                                            <asp:RegularExpressionValidator ID="revLengthtxtStatusOfCompliance" runat="server"
                                                Display="Dynamic" ForeColor="Red" ValidationGroup="Compliance" ControlToValidate="txtStatusOfCompliance"></asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <p>
                                        No record found.</p>
                                </EmptyDataTemplate>
                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                    PageButtonCount="4" />
                            </asp:GridView>
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
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('If you have not saved data, Your data will be lost before moving to other page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" ValidationGroup="Compliance"
                                OnClick="btnSaveAsDraft_Click" />
                            <asp:Button ID="txtNext" runat="server" Text="Next >>" ValidationGroup="Compliance"
                                OnClick="txtNext_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
