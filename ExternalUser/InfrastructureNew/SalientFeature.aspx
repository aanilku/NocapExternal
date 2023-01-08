<%@ Page Title="NOCAP-Infrastructure Application-New" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="SalientFeature.aspx.cs" Inherits="ExternalUser_InfrastructureNew_SalientFeature" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <script language="javascript" type="text/javascript">


        function MSMEShowWait() {
            if (document.getElementById('<%= txtMSME.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadMSME.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressMSME.ClientID %>')
                x.style.display = 'inline';
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                                           <%-- <li class="visited">De-Watering Existing Structure</li>
                                            <li class="visited">De-Watering Proposed Structure</li>
                                            <li class="visited">Breakup of Water Requirment</li>--%>
                                            <li class="active">Final Submit-Exemption Letter</li>
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
                                INFRASTRUCTURE USE: 1. General Information- Exemption Letter
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblExemptionMessage" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:Label ID="lblMessage" runat="server" Style="text-align: left"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblInfrastructureApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server">
                        <%--<td valign="top">G.
                                    </td>--%>
                        <td colspan="4" valign="top">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <table class="SubFormWOBG" width="100%">
                                            <tr>
                                                <td valign="top" colspan="2">
                                                    <asp:Label runat="server" Visible="false" ID="lblMSME2">
                                        <span class="Coumpulsory">*</span>
                                                    </asp:Label>
                                                    <asp:Label ID="lblMSME" CssClass="Clicked" runat="server" Text="MSME certificate in case of MSME"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>Attachment Name :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMSME" runat="server" MaxLength="50"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtMSME"
                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGMSMEAttachment">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtMSME" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                        ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGMSMEAttachment"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Select Attachment File :
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanelMSME" runat="server">
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnUploadMSME" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:FileUpload ID="FileUploadMSME" runat="server" />
                                                            <asp:Button ID="btnUploadMSME" runat="server" Text="Upload" OnClick="btnUploadMSME_Click"
                                                                ValidationGroup="VGMSMEAttachment" OnClientClick="javascript:MSMEShowWait();" />
                                                            <asp:UpdateProgress ID="UpdateProgressMSME" runat="server" AssociatedUpdatePanelID="UpdatePanelMSME">
                                                                <ProgressTemplate>
                                                                    <asp:Label ID="lblMSMEWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                        <asp:GridView ID="gvMSME" runat="server" AutoGenerateColumns="False"
                                            CssClass="SubFormWOBG" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                            ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvMSME_RowDeleting">
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
                                                        <asp:Label ID="lblAttachmentName" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="File Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFileName" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View File">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                No Records Exist in MSME Attachment.
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                            &nbsp;<asp:Label ID="lblMSMEMessage" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblMessageExtra" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('If you have not saved data, Your data will be lost before moving to other page ?');"
                                OnClick="btnPrev_Click" Style="width: 67px" />
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"
                                Text="Submit" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

