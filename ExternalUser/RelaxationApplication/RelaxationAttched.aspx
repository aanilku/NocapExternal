<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RelaxationAttched.aspx.cs"
    MasterPageFile="~/ExternalUser/ExternalUserMaster.master" Inherits="ExternalUser_RelaxationAttched" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Scripts/ClientSideDateValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
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
                                            <li class="active">Attachment</li>
                                            <%--   <li >Online Payment</li>--%>
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

                        <td style="width: 20px">
                            <b>(1).</b>
                        </td>
                        <td colspan="2">
                            <b>Processing Fee<br />
                            </b>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Amount:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAmount" runat="server" MaxLength="8"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAmount"
                                ForeColor="Red" ValidationGroup="AddFee" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                            <%--     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ValidationExpression="[0-9]*" ErrorMessage="Only Numeric values are allowed"
                                ValidationGroup="AddFee" Display="Dynamic" ForeColor="Red" ControlToValidate="txtBharatKoshRefferenceNo"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Bharat Kosh Transaction Ref. No:
                        </td>
                        <td>
                            <asp:TextBox ID="txtBharatKoshRefferenceNo" runat="server" MaxLength="49"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtBharatKoshRefferenceNo" runat="server" ControlToValidate="txtBharatKoshRefferenceNo"
                                ForeColor="Red" ValidationGroup="AddFee" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                            <%-- <asp:RegularExpressionValidator ID="revtxtBharatKoshRefferenceNo" runat="server"
                                ValidationExpression="[0-9]*" ErrorMessage="Only Numeric values are allowed"
                                ValidationGroup="AddFee" Display="Dynamic" ForeColor="Red" ControlToValidate="txtBharatKoshRefferenceNo"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Bharat Kosh Transaction Dated:
                        </td>
                        <td>
                            <asp:TextBox ID="txtBharatKoshDated" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="txtBharatKoshDatedCalendarExtender" runat="server" Enabled="True"
                                TargetControlID="txtBharatKoshDated" PopupButtonID="imgbtntxtBharatKoshDated"
                                Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                            <asp:ImageButton ID="imgbtntxtBharatKoshDated" runat="server" ImageUrl="~/Images/calendar.png" />
                            <asp:RequiredFieldValidator ID="rfvtxtBharatKoshDated" runat="server" ControlToValidate="txtBharatKoshDated"
                                ForeColor="Red" ValidationGroup="AddFee" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                            &nbsp;<asp:CustomValidator ID="CVtxtBharatKoshDated" runat="server" ClientValidationFunction="ValidateDateOnClient"
                                OnServerValidate="ValidateDate" ControlToValidate="txtBharatKoshDated" ErrorMessage="Invalid Date."
                                ForeColor="Red" ValidationGroup="AddFee" Display="Dynamic" />
                            <asp:RangeValidator ID="revtxtBharatKoshDated" runat="server" ValidationGroup="AddFee"
                                ControlToValidate="txtBharatKoshDated" ErrorMessage="Date should be grater than 01/01/1990 and less than or equal to current date."
                                Display="Dynamic" ForeColor="Red" MinimumValue="01/01/1990" MaximumValue='<%# DateTime.Now.ToString("dd/MM/yyyy") %>'
                                Type="Date"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>

                        <td>Attachment Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRelaxation" runat="server" MaxLength="50"
                                Width="200px"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtRelaxation"
                                Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGSitePlan"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtRelaxation" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGAffidavitNonAva"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Upload Documents: <span class="Coumpulsory">*</span>
                        </td>

                        <td>
                            <asp:UpdatePanel ID="UpdatePanelSitePlan" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnUploadSitePlan" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:FileUpload ID="FileUploadSitePlan" runat="server" />
                                    <asp:Button ID="btnUploadSitePlan" runat="server" Text="Upload" OnClick="btnUploadSitePlan_Click"
                                        ValidationGroup="VGSitePlan" />
                                    <asp:UpdateProgress ID="UpdateProgressSitePlan" runat="server" AssociatedUpdatePanelID="UpdatePanelSitePlan">
                                        <ProgressTemplate>
                                            <asp:Label ID="lblSitePlanWait" runat="server" BackColor="#507CD1" Font-Bold="True"
                                                ForeColor="White" Text="Please wait ... Uploading file"></asp:Label>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:GridView ID="gvSitePlan" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                Width="100%" OnRowDeleting="gvSitePlan_RowDeleting" ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemTemplate>
                                            <span>
                                                <%#Container.DataItemIndex + 1 %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S.No." Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentSerialNumber" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Attachment Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachment" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                OnCommand="ViewFile">View</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No Records exist in Site Plan with Location Map.
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:Label ID="lblMessageNOCCount" runat="server" Visible="false"></asp:Label>
                        </td>

                    </tr>
                    <tr>

                        <td colspan="3">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblMessageSitePlan" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblIndustialApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('If you have not saved data, Your data will be lost before moving to other page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" ValidationGroup="AddFee"
                                OnClick="btnSaveAsDraft_Click" />
                            <asp:Button ID="txtNext" runat="server" Text="Next >>" ValidationGroup="AddFee"
                                OnClick="txtNext_Click" />
                        </td>
                    </tr>

                </table>
            </td>

        </tr>
    </table>
</asp:Content>
