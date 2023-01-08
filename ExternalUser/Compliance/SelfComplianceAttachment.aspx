<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="SelfComplianceAttachment.aspx.cs"
    Inherits="ExternalUser_Compliance_SelfComplianceAttachment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>File Upload</title>
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
            float: left;
            display: block; /*background: url("../Images/SelectedButton.png") no-repeat right top;*/
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table class="SubFormWOBG" width="100%" style="line-height: 25px">
        <tr>
            <td>
                <div class="div_IndAppHeading" style="padding-left: 10px">
                    Self Compliance: Attachment
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Button ID="Tab1" BorderStyle="None" runat="server" Text="Photographs" CssClass="Initial"
                                            OnClick="Tab1_Click" Width="100%" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Tab2" BorderStyle="None" runat="server" Text="Self Compliance" CssClass="Initial"
                                            OnClick="Tab2_Click" Width="100%" />
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
                <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table width="100%" class="SubFormWOBG" cellspacing="0px">
                            <tr>
                                <td colspan="3" style="text-align: right">(<span class="style8">*</span>)- Mandatory Fields, (<span class="style8">$</span>)-
                                    Upload Attachments in <strong>Attachment</strong> Section<br />
                                    Maximum Number of Attachment Allowed- 4<br />
                                    Maximum Size of each Attachment Allowed- 500KB<br />
                                    Allowed file type for Attachment-&nbsp; doc, docx, jpg, jpeg, pdf
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30px">(1).
                                </td>
                                <td colspan="2">Photographs
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td valign="top">Name <span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPhotographs" runat="server" MaxLength="25"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtNameAndAddressOfReffDept" runat="server" ForeColor="Red"
                                        Display="Dynamic" ValidationGroup="VGPhotographs" ControlToValidate="txtPhotographs">Required</asp:RequiredFieldValidator>
                                    <%-- <asp:RegularExpressionValidator ID="revtxtNameAndAddressOfReffDept" runat="server"
                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtPhotographs" ValidationGroup="VGPhotographs"></asp:RegularExpressionValidator>--%>
                                    <asp:RegularExpressionValidator ID="rfvtxtPhotographs" runat="server" Display="Dynamic"
                                        ForeColor="Red" ControlToValidate="txtPhotographs" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                        ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGPhotographs"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>Select Attachment file <span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUploadPhotographs" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnUploadPhotographs" runat="server" Text="Upload" ValidationGroup="VGPhotographs"
                                        OnClick="btnUploadPhotographs_Click" Style="width: 60px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:GridView ID="gvPhotographs" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                        Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                        ShowHeaderWhenEmpty="true" OnRowCommand="gvPhotographs_RowCommand" OnRowDeleting="gvPhotographs_RowDeleting">
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
                                                    <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="File Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View File">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                        OnCommand="lbtnViewFile_Click">View</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No Records exist.
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <asp:Label ID="lblMessagePhotographs" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <table width="100%" class="SubFormWOBG" cellspacing="0px">
                            <tr>
                                <td colspan="3" style="text-align: right">(<span class="style8">*</span>)- Mandatory Fields, (<span class="style8">$</span>)-
                                    Upload Attachments in <strong>Attachment</strong> Section<br />
                                    Maximum Number of Attachment Allowed- 1<br />
                                    Maximum Size of each Attachment Allowed- 10MB<br />
                                    Allowed file type for Attachment-&nbsp; doc, docx, pdf
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30px">(2).
                                </td>
                                <td colspan="2">Self Compliance Attachment
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td valign="top">Name <span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSelfComplianceAttachment" runat="server" MaxLength="25"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                        Display="Dynamic" ValidationGroup="SelfCompliance" ControlToValidate="txtSelfComplianceAttachment">Required</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                                        ForeColor="Red" ControlToValidate="txtSelfComplianceAttachment" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                        ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>Select Attachment file <span class="Coumpulsory">*</span>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUploadSelfComplianceAttachment" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnUploadSelfComplianceAttachment" runat="server" Text="Upload" ValidationGroup="SelfCompliance"
                                        OnClick="btnUploadSelfComplianceAttachment_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:GridView ID="gvSelfComplianceAttachment" runat="server" AutoGenerateColumns="False"
                                        CssClass="SubFormWOBG" Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                        ShowHeaderWhenEmpty="true" OnRowCommand="gvSelfComplianceAttachment_RowCommand"
                                        OnRowDeleting="gvSelfComplianceAttachment_RowDeleting">
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
                                                    <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="File Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View File">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnViewSelfComplianceAttachmentFile" runat="server" CommandName="ViewFile"
                                                        OnCommand="lbtnViewSelfComplianceAttachmentFile_Click" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'>View</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No Records exist.
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    &nbsp;<asp:Label ID="lblMessageSelfCompliance" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                <asp:Label ID="lblApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                <asp:Label ID="lblApplicationRenewCode" Visible="false" runat="server" Enabled="False"></asp:Label>


                <asp:Label ID="lblFinalMsg" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:CheckBox runat="server"  />I have reviewed and accepted that all the information provided by me is correct.

                <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('If you have not saved data, Your data will be lost before moving to other page ?');"
                    OnClick="btnPrev_Click" />
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
