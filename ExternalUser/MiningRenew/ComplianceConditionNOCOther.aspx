<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="ComplianceConditionNOCOther.aspx.cs"
    Inherits="ExternalUser_MiningRenew_ComplianceConditionNOCOther" %>

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
                                            <li class="visited">Land Use Details</li>
                                            <li class="visited">Dewatering Existing Structure</li>
                                            <li class="visited">Dewatering Additional Structure</li>
                                            <li class="visited">Utilization of pumped water</li>
                                            <li class="visited">Monitoring of groundwater regime</li>
                                            <li class="visited">Groundwater Abstraction Structure- Existing</li>
                                            <li class="visited">Groundwater Abstraction Structure- Additional</li>
                                            <li class="visited">Compliance Conditions in the NOC</li>
                                            <li class="active">Compliance Conditions in the NOC - Other</li>
                                            <li>Other Details</li>
                                         
                                            <li>Attachment</li>
                                              <li>Ready To Submit</li>
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
                        <td>
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                RENEW - MINING USE: Compliance to the Conditions prescribed in the NOC - Other
                            </div>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <center>
                                <table class="SubFormWOBG" width="100%" style="line-height: 25px">
                                    <tr>
                                        <td colspan="2" class="FormProjName">
                                            <b>Project Name:&nbsp;
                                                <asp:Label ID="lblHeadingProjName" runat="server"></asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <b>18(b). Compliance to the Conditions prescribed in the NOC - Other</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            Compliance Type:<br />
                                            (Any Specific Condition not listed in "Compliance Conditions in the NOC")
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtComplianceConditionEnterOther" runat="server" TextMode="MultiLine"
                                                Width="310px" Height="50px"> </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtComplianceConditionEnterOther" runat="server"
                                                ForeColor="Red" Display="Dynamic" ValidationGroup="CompCondNOCOther" ControlToValidate="txtComplianceConditionEnterOther">Required</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revtxtComplianceConditionEnterOther" runat="server"
                                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtComplianceConditionEnterOther"
                                                ValidationGroup="CompCondNOCOther"></asp:RegularExpressionValidator>
                                            <asp:RegularExpressionValidator ID="revLengthtxtComplianceConditionEnterOther" runat="server"
                                                Display="Dynamic" ForeColor="Red" ValidationGroup="CompCondNOCOther" ControlToValidate="txtComplianceConditionEnterOther"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            Status of Compliance:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtStatusofComplianceOther" runat="server" TextMode="MultiLine"
                                                Width="310px" Height="50px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtStatusofComplianceOther" runat="server" ForeColor="Red"
                                                Display="Dynamic" ValidationGroup="CompCondNOCOther" ControlToValidate="txtStatusofComplianceOther">Required</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revtxtStatusofComplianceOther" runat="server"
                                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtStatusofComplianceOther"
                                                ValidationGroup="CompCondNOCOther"></asp:RegularExpressionValidator>
                                            <asp:RegularExpressionValidator ID="revLengthtxtStatusofComplianceOther" runat="server"
                                                Display="Dynamic" ForeColor="Red" ValidationGroup="CompCondNOCOther" ControlToValidate="txtStatusofComplianceOther"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblMessageAdd" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Button ID="btnComConAddOther" runat="server" Text="Add" ValidationGroup="CompCondNOCOther"
                                                OnClick="btnComConAddOther_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:GridView ID="gvCompCondNOCOther" runat="server"  CssClass="SubFormWOBG"
                                AutoGenerateColumns="False" align="center" DataKeyNames="MiningRenewApplicationCode,ComplianceConditionNOCSerialNumber"
                                Width="85%" OnRowUpdating="gvCompCondNOCOther_RowUpdating" OnRowEditing="gvCompCondNOCOther_RowEditing"
                                OnRowCancelingEdit="gvCompCondNOCOther_RowCancelingEdit" OnRowDeleting="gvCompCondNOCOther_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <span>
                                                <%#Container.DataItemIndex + 1 %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Condition give in NOC" HeaderStyle-Width="50%" ItemStyle-Width="50%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblComplianceConditionEnter" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ComplianceConditionEnter"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtComplianceConditionEnter" runat="server" TextMode="MultiLine"
                                                Width="310px" Height="50px"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvtxtComplianceConditionEnter" runat="server" ForeColor="Red"
                                                Display="Dynamic" ValidationGroup='<%#System.Web.HttpUtility.HtmlEncode(Eval("ComplianceConditionNOCSerialNumber"))%>'
                                                ControlToValidate="txtComplianceConditionEnter">Required</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revtxtComplianceConditionEnter" runat="server"
                                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtComplianceConditionEnter"
                                                ValidationGroup='<%#System.Web.HttpUtility.HtmlEncode(Eval("ComplianceConditionNOCSerialNumber"))%>'></asp:RegularExpressionValidator>
                                            <asp:RegularExpressionValidator ID="revLengthgrdtxtComplianceConditionEnter" runat="server"
                                                Display="Dynamic" ForeColor="Red" ValidationGroup='<%#System.Web.HttpUtility.HtmlEncode(Eval("ComplianceConditionNOCSerialNumber"))%>'
                                                ControlToValidate="txtComplianceConditionEnter"></asp:RegularExpressionValidator>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status of Compliance" SortExpression="ComplianceConditionDescription">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatusOfComplianceOther" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("StatusOfCompliance")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtStatusOfComplianceOther" runat="server" TextMode="MultiLine"
                                                Width="310px" Height="50px"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvtxtStatusofComplianceOther" runat="server" ForeColor="Red"
                                                Display="Dynamic" ValidationGroup='<%#System.Web.HttpUtility.HtmlEncode(Eval("ComplianceConditionNOCSerialNumber"))%>'
                                                ControlToValidate="txtStatusOfComplianceOther">Required</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revtxtStatusofComplianceOther" runat="server"
                                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtStatusOfComplianceOther"
                                                ValidationGroup='<%#System.Web.HttpUtility.HtmlEncode(Eval("ComplianceConditionNOCSerialNumber"))%>'></asp:RegularExpressionValidator>
                                            <asp:RegularExpressionValidator ID="revLengthgrdtxtStatusOfComplianceOther" runat="server"
                                                Display="Dynamic" ForeColor="Red" ValidationGroup='<%#System.Web.HttpUtility.HtmlEncode(Eval("ComplianceConditionNOCSerialNumber"))%>'
                                                ControlToValidate="txtStatusOfComplianceOther"></asp:RegularExpressionValidator>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnEditOther" runat="server" Height="20px" ImageUrl="~/Images/Edit.jpg"
                                                Width="20px" CommandName="Edit" ToolTip="Edit" CausesValidation="false" />
                                            <asp:ImageButton ID="imgbtnDeleteOthe" runat="server" Height="20px" ImageUrl="~/Images/delete.jpg"
                                                Width="20px" OnClientClick="return confirm('Are you sure you want to delete?');"
                                                CommandName="Delete" ToolTip="Delete" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imgbtnUpdateOthe" runat="server" Height="20px" ImageUrl="~/Images/update.jpg"
                                                Width="20px" CommandName="Update" ToolTip="Update" ValidationGroup='<%#System.Web.HttpUtility.HtmlEncode(Eval("ComplianceConditionNOCSerialNumber"))%>' />
                                            <asp:ImageButton ID="imgbtnCancelOther" runat="server" Height="20px" ImageUrl="~/Images/Cancel.jpg"
                                                CausesValidation="false" Width="20px" CommandName="Cancel" ToolTip="Cancel" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                    PageButtonCount="4" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblMiningApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('If you have not saved data, Your data will be lost before moving to other page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="txtNext" runat="server" Text="Next >>" OnClick="txtNext_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
