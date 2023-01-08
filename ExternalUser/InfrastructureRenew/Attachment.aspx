<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="Attachment.aspx.cs"
    Inherits="ExternalUser_InfrastructureRenew_Attachment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>File Upload</title>
    <style type="text/css">
        .Initial {
            display: block;
            padding: 4px 10px 4px 10px;
            float: left;
            background-color: #CFE3FA;
            color: Black;
            font-weight: bold;
        }

            .Initial:hover {
                color: White;
                background-color: #094E7F;
                cursor: hand;
            }

        .Clicked {
            float: left;
            display: block;
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
    <script language="javascript" type="text/javascript">
        function showWait() {
            if (document.getElementById('<%= txtSitePlanAttachment.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadSitePlan.ClientID %>').value.length > 0) {
                //       
                var x = document.getElementById('<%= UpdateProgressSitePlan.ClientID %>')
                //        alert(x);

                x.style.display = 'inline';

            }
        }
        function showSourceofAvailability() {
            if (document.getElementById('<%= txtSourceofAvailabilityofSurfaceWater.ClientID %>').value.length > 0 && document.getElementById('<%= txtFileUploadSourceofAvailabilityofSurfaceWater.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressSourceofAvailabilityofSurfaceWater.ClientID %>')
                x.style.display = 'inline';
            }
        }
        function WaterAuditReportShowWait() {
            if (document.getElementById('<%= txtWaterAuditReport.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadWaterAuditReport.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressWaterAuditReport.ClientID %>')
                x.style.display = 'inline';
            }
        }
        function AffidavitShowWait() {
            if (document.getElementById('<%= txtAffidavit.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadAffidavit.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressAffidavit.ClientID %>')
                x.style.display = 'inline';
            }
        }
        function showCertifiedRevenueSketch() {
            if (document.getElementById('<%= txtCertifiedRevenueSketchAttachment.ClientID %>').value.length > 0 && document.getElementById('<%= txtFileUploadCertifiedRevenueSketch.ClientID %>').value.length > 0) {

                var x = document.getElementById('<%= UpdateProgressCertifiedRevenueSketch.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showReasonForNotApplyingBefore() {
            if (document.getElementById('<%= txtReasonForNotApplyingBefore.ClientID %>').value.length > 0 && document.getElementById('<%= txtFileUploadReasonForNotApplyingBefore.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressReasonForNotApplyingBefore.ClientID %>')
                x.style.display = 'inline';
            }
        }

        function showExistingNOC() {
            if (document.getElementById('<%= txtExistingNOC.ClientID %>').value.length > 0 && document.getElementById('<%= txtFileUploadExistingNOC.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressExistingNOC.ClientID %>')
                x.style.display = 'inline';
            }
        }

        function showWaterRequirement() {
            if (document.getElementById('<%= txtWaterRequirement.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadWaterRequirement.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressWaterRequirement.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showGroundwaterAvailability() {
            if (document.getElementById('<%= txtgvGroundwaterAvailability.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadGroundwaterAvailability.ClientID %>').value.length > 0) {

                var x = document.getElementById('<%= UpdateProgressGroundwaterAvailability.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showRainwaterHarvesting() {
            if (document.getElementById('<%= txtRainwaterHarvesting.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadRainwaterHarvesting.ClientID %>').value.length > 0) {

                var x = document.getElementById('<%= UpdateProgressRainwaterHarvesting.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showUndertaking() {
            if (document.getElementById('<%= txtUndertaking.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadUndertaking.ClientID %>').value.length > 0) {

                var x = document.getElementById('<%= UpdateProgressUndertaking.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function ExtraShowWait() {
            if (document.getElementById('<%= txtExtraAttachment.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadExtra.ClientID %>').value.length > 0) {
                //       
                var x = document.getElementById('<%= UpdateProgressExtra.ClientID %>')
                //        alert(x);

                x.style.display = 'inline';

            }
        }
        function BharatKoshRecieptShowWait() {
            if (document.getElementById('<%= txtBharatKoshReciept.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadBharatKoshReciept.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressBharatKoshReciept.ClientID %>')
                x.style.display = 'inline';
            }
        }
        function AplicationSignatureSealShowWait() {
            if (document.getElementById('<%= txtApplicationSignatureSeal.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadApplicationSignatureSeal.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressApplicationSignatureSeal.ClientID %>')
                x.style.display = 'inline';
            }
        }
           function ShowProofOfOwnershipLand() {
            if (document.getElementById('<%= txtProofOfOwnershipOfLand.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadProofOfOwnershipOfLand.ClientID %>').value > 0) {
                var x = document.getElementById('<%= UpdateProgressProofOfOwnershipOfLand.ClientID %>')
                x.style.display = 'inline';
            }
        }

        function ShowProofofownershipLeaseoftanker() {
            if (document.getElementById('<%= txtProofofownershipLeaseoftanker.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadProofofownershipLeaseoftanker.ClientID %>').value > 0) {
                var x = document.getElementById('<%= UpdateProgressProofofownershipLeaseoftanker.ClientID %>')
                x.style.display = 'inline';
            }
        }
    </script>
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
                                            <li class="visited">Existing NOC Details</li>
                                            <li class="visited">Water Requirement Details</li>
                                            <li class="visited">Recycled Water Usage</li>
                                            <li class="visited">Groundwater Abstraction Structure- Existing</li>
                                            <li class="visited">Groundwater Abstraction Structure- Additional</li>
                                            <li class="visited">Compliance Conditions in the NOC</li>
                                            <li class="visited">Compliance Conditions in the NOC - Other</li>
                                            <li class="visited">Other Details</li>
                                        
                                            <li class="active">Attachment</li>
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
                                RENEW - INFRASTRUCTURE USE: Attachment
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormProjName">
                            <b>Project Name:&nbsp;
                                <asp:Label ID="lblHeadingProjName" runat="server"></asp:Label></b>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right">(<span class="Compulsory">*</span>)- Mandatory Fields, (<span class="Compulsory">$</span>)-
                            Upload Attachments in <strong>Attachment</strong> Section<br />
                            Maximum Number of Attachment Allowed- 5<br />
                            Maximum Size of each Attachment Allowed- 5MB<br />
                            Allowed file type for Attachment- doc, docx, jpg, jpeg, pdf
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="SubFormWOBG" width="100%">
                                <tr>
                                    <th align="center">SNo.
                                    </th>
                                    <th align="center">Attachment Name/ Upload File
                                    </th>
                                </tr>
                                <tr>
                                    <td valign="top">A.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:HiddenField runat="server" ID="hdnExistingNOC" />
                                                                <asp:Label runat="server" Visible="false" ID="lblExistingNOC2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblExistingNOC" CssClass="Clicked" runat="server" Text="Existing NOC"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Existing NOC: (Refer: 1 (iv))"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtExistingNOC" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtExistingNOC"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGExistingNOC"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtExistingNOC" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGExistingNOC"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelExistingNOC" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUplodExistingNOCFile" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="txtFileUploadExistingNOC" runat="server" />
                                                                        <asp:Button ID="btnUplodExistingNOCFile" runat="server" Text="Upload" ValidationGroup="VGExistingNOC"
                                                                            OnClick="btnUplodExistingNOCFile_Click" OnClientClick="javascript:showExistingNOC();" />
                                                                        <asp:UpdateProgress ID="UpdateProgressExistingNOC" runat="server" AssociatedUpdatePanelID="UpdatePanelExistingNOC">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblExistingNOCWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                <td colspan="3">
                                                    <asp:GridView ID="gvExistingNOC" runat="server" ShowHeaderWhenEmpty="true" CssClass="SubFormWOBG"
                                                        AutoGenerateColumns="False" DataKeyNames="InfrastructureRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%" OnRowDeleting="gvExistingNOC_RowDeleting">
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
                                                                    <asp:LinkButton ID="lbtnViewExistingNOCFile" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnViewExistingNOCFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records Exist in Existing NOC.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageExistingNOC" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">B.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:HiddenField runat="server" ID="hdnRainwaterHarvesting" />
                                                                <asp:Label runat="server" Visible="false" ID="lblRainwaterHarvesting2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblRainwaterHarvesting" CssClass="Clicked" runat="server" Text="Rainwater Harvesting"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Details of Rainwater Harvesting / Artificial Recharge Measures : (Refer: 6)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRainwaterHarvesting" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtRainwaterHarvesting"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGRainWaterHarvesting"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtRainwaterHarvesting" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGRainWaterHarvesting"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelRainwaterHarvesting" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUplodRainwaterHarvesting" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadRainwaterHarvesting" runat="server" />
                                                                        <asp:Button ID="btnUplodRainwaterHarvesting" runat="server" Text="Upload" OnClick="btnUplodRainwaterHarvesting_Click"
                                                                            OnClientClick="javascript:showRainwaterHarvesting();" ValidationGroup="VGRainWaterHarvesting" />
                                                                        <asp:UpdateProgress ID="UpdateProgressRainwaterHarvesting" runat="server" AssociatedUpdatePanelID="UpdatePanelRainwaterHarvesting">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblRainwaterHarvestingWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                <td colspan="3">
                                                    <asp:GridView ID="gvRainwaterHarvesting" runat="server" AutoGenerateColumns="False"
                                                        CssClass="SubFormWOBG" DataKeyNames="InfrastructureRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvRainwaterHarvesting_RowDeleting">
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
                                                                    <asp:LinkButton ID="lbtnViewRainwaterHarvestingFile" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnViewRainwaterHarvestingFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records Exist in Details of Rainwater Harvesting / Artificial Recharge Measures.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageRainwaterHarvesting" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">C.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblComplianceReport2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblComplianceReport" CssClass="Clicked" runat="server" Text="Compliance Report"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Compliance Condition NOC: (Refer: 4(a))"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:GridView ID="gvINFRenewComplianceConditionNOC" runat="server" AutoGenerateColumns="False"
                                                                    CssClass="SubFormWOBG" ShowHeaderWhenEmpty="true" Width="100%"
                                                                    OnRowDataBound="gvINFRenewComplianceConditionNOC_RowDataBound"
                                                                    OnRowCommand="gvINFRenewComplianceConditionNOC_RowCommand">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sr.No.">
                                                                            <ItemTemplate>
                                                                                <span>
                                                                                    <%#Container.DataItemIndex + 1 %>
                                                                                </span>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Conditions given in NOC" ItemStyle-Width="25%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCompCondNOCType" runat="server" Text=""></asp:Label>
                                                                                <asp:Label ID="lblCompCondNOCCode" runat="server" Visible="false" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("ComplianceConditionCode")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status of compliance" ItemStyle-Width="12%" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStatusOfCompliance" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("StatusOfCompliance")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Compliance Conditions Applicable" ItemStyle-Width="12%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblComplianceConditionsApplicable" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("ComplianceConditionsApplicable")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Remarks" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRemarks" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("Remarks")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="View File" Visible="false">
                                                                            <ItemTemplate>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Delete" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="File Upload" ItemStyle-Width="28%">
                                                                            <ItemTemplate>
                                                                                Attachment Name:
                                                                <asp:TextBox ID="txtAttachmentNameCompCondNOC" runat="server"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtAttachmentNameCompCondNOC"
                                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup='<%# System.Web.HttpUtility.HtmlEncode(Eval("ComplianceConditionCode")) %>'></asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtAttachmentNameCompCondNOC"
                                                                                    ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*" ErrorMessage="Allow only Alphanumeric and Characters . , _ / -"
                                                                                    ValidationGroup='<%# System.Web.HttpUtility.HtmlEncode(Eval("ComplianceConditionCode")) %>'></asp:RegularExpressionValidator><br />
                                                                                <asp:FileUpload ID="FileUploadCompCondNOC" runat="server" Width="180px" /><br />
                                                                                <asp:Button ID="btnUploadCompCondNOC" runat="server" Text="Upload" ValidationGroup='<%# System.Web.HttpUtility.HtmlEncode(Eval("ComplianceConditionCode")) %>'
                                                                                    CommandName="UploadFileForCompCondNOC" /><br />
                                                                                <asp:Label ID="lblMessageINFCompCondAttachment" runat="server" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Attachments">
                                                                            <ItemTemplate>
                                                                                <asp:GridView ID="gvCompCondNOCAttachment" runat="server" AutoGenerateColumns="False"
                                                                                    CssClass="SubFormWOBG" ShowHeaderWhenEmpty="true" Width="100%" OnRowCommand="gvCompCondNOCAttachment_RowCommand">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Sr.No.">
                                                                                            <ItemTemplate>
                                                                                                <span>
                                                                                                    <%#Container.DataItemIndex + 1 %>
                                                                                                </span>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Attachment Name / File Name">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblFileName" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) + " /" %>'></asp:Label><br />
                                                                                                <asp:Label ID="lblAttachmentName" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lnkInfrastructureCompCondNOCAttachmentView" OnCommand="lnkInfrastructureCompCondNOCAttachmentView_Click"
                                                                                                    runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                                                    CommandName="View">View</asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                                                    CommandName="DeleteFile" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <EmptyDataTemplate>
                                                                                        No Records Exist in Compliance Condition NOC Attachment.
                                                                                    </EmptyDataTemplate>
                                                                                </asp:GridView>
                                                                                <asp:Label ID="lblMessageINFCompCondNOCAttachmentDelete" runat="server" Text="">
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                        No Records Exist.
                                                                    </EmptyDataTemplate>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">D.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:HiddenField runat="server" ID="hdnAffidavit" />
                                                                <asp:Label runat="server" Visible="false" ID="lblAffidavit2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblAffidavit" CssClass="Clicked" runat="server" Text="Affidavit of Compliance of NOC Condition"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Affidavit of Compliance of NOC Condition:"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Attachment Name :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAffidavit" runat="server" MaxLength="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAffidavit"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGAffidavitAttachment">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator runat="server"
                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtAffidavit" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGAffidavitAttachment"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelAffidavit" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadAffidavit" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadAffidavit" runat="server" />
                                                                        <asp:Button ID="btnUploadAffidavit" runat="server" Text="Upload" OnClick="btnUploadAffidavit_Click"
                                                                            ValidationGroup="VGAffidavitAttachment" OnClientClick="javascript:AffidavitShowWait();" />
                                                                        <asp:UpdateProgress ID="UpdateProgressAffidavit" runat="server" AssociatedUpdatePanelID="UpdatePanelAffidavit">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblAffidavitWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                <td colspan="3">
                                                    <asp:GridView ID="gvAffidavit" runat="server" AutoGenerateColumns="False"
                                                        CssClass="SubFormWOBG" DataKeyNames="InfrastructureRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvAffidavit_RowDeleting">
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
                                                                    <asp:LinkButton runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                            No Records Exist in Affidavit Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageAffidavit" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">E.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:HiddenField runat="server" ID="hdnWaterAuditReport" />
                                                                <asp:Label runat="server" Visible="false" ID="lblWaterAuditReport2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblWaterAuditReport" CssClass="Clicked" runat="server" Text="Water Audit Report"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Water Audit Report:"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Attachment Name :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtWaterAuditReport" runat="server" MaxLength="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtWaterAuditReport"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGWaterAuditReportAttachment">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtWaterAuditReport" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGWaterAuditReportAttachment"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelWaterAuditReport" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadWaterAuditReport" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadWaterAuditReport" runat="server" />
                                                                        <asp:Button ID="btnUploadWaterAuditReport" runat="server" Text="Upload" OnClick="btnUploadWaterAuditReport_Click"
                                                                            ValidationGroup="VGWaterAuditReportAttachment" OnClientClick="javascript:WaterAuditReportShowWait();" />
                                                                        <asp:UpdateProgress ID="UpdateProgressWaterAuditReport" runat="server" AssociatedUpdatePanelID="UpdatePanelWaterAuditReport">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblWaterAuditReportWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                <td colspan="3">
                                                    <asp:GridView ID="gvWaterAuditReport" runat="server" AutoGenerateColumns="False"
                                                        CssClass="SubFormWOBG" DataKeyNames="InfrastructureRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvWaterAuditReport_RowDeleting">
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
                                                                    <asp:LinkButton runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                            No Records Exist in Water Audit Report Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageWaterAuditReport" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">F.
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:HiddenField runat="server" ID="hdnSourceWaterAvailability" />
                                                                <asp:Label runat="server" Visible="false" ID="lblSourceWaterAvailability2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblSourceWaterAvailability" runat="server" CssClass="Clicked" Text="Source Water Non-availability Certificate"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label ID="Label2" runat="server" Text="Source Water Non-availability Certificate:"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSourceofAvailabilityofSurfaceWater" runat="server" MaxLength="50"
                                                                    Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSourceofAvailabilityofSurfaceWater"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGSourceOfAvailabilityOfSurface"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtSourceofAvailabilityofSurfaceWater" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGSourceOfAvailabilityOfSurface"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelSourceofAvailabilityofSurfaceWater" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUplodSourceofAvailabilityofSurfaceWater" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="txtFileUploadSourceofAvailabilityofSurfaceWater" runat="server" />
                                                                        <asp:Button ID="btnUplodSourceofAvailabilityofSurfaceWater" runat="server" Text="Upload"
                                                                            ValidationGroup="VGSourceOfAvailabilityOfSurface"
                                                                            OnClick="lbtnUplodSourceofAvailabilityofSurfaceWater_Click"
                                                                            OnClientClick="javascript:showSourceofAvailability();" />
                                                                        <asp:UpdateProgress ID="UpdateProgressSourceofAvailabilityofSurfaceWater" runat="server"
                                                                            AssociatedUpdatePanelID="UpdatePanelSourceofAvailabilityofSurfaceWater">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblSourceofAvailabilityofSurfaceWaterWait" runat="server" BackColor="#507CD1"
                                                                                    Font-Bold="True" ForeColor="White" Text="Please wait ... Uploading file"></asp:Label>
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
                                                    <asp:GridView ID="gvSourceofAvailabilityofSurfaceWater" runat="server" ShowHeaderWhenEmpty="true"
                                                        CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                                        DataKeyNames="InfrastructureRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%"
                                                        OnRowDeleting="gvSourceofAvailabilityofSurfaceWater_RowDeleting">
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
                                                                    <asp:LinkButton runat="server"
                                                                        CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                            No Records Exist in Source Water Availability/Non-availability Certificate.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        &nbsp;<asp:Label ID="lblMessageSourceofAvailabilityofSurfaceWater" runat="server"></asp:Label>
                                    </td>
                                </tr>

                                <tr runat="server" visible="false">
                                    <td valign="top">G.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:HiddenField runat="server" ID="hdnBharatKosh" />
                                                                
                                                                <asp:Label ID="lblBharatKosh" CssClass="Clicked" runat="server" Text="Bharat Kosh Reciept Attachment"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Bharat Kosh Reciept Attachment (Processing Fee):"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Attachment Name :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBharatKoshReciept" runat="server" MaxLength="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtBharatKoshReciept"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGBharatKoshRecieptAttachment">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
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
                                                <td colspan="3">
                                                    <asp:GridView ID="gvBharatKoshReciept" runat="server" AutoGenerateColumns="False"
                                                        CssClass="SubFormWOBG"
                                                        DataKeyNames="InfrastructureRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="BharatKoshReciept_RowDeleting">
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
                                                                    <asp:LinkButton ID="lbtnBharatKoshRecieptViewFile" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnBharatKoshRecieptViewFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records Exist in Extra Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageBharatKoshReciept" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">G.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:HiddenField runat="server" ID="hdnSigneddoc" />
                                                                <asp:Label runat="server" Visible="false" ID="lblSigneddoc2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblSigneddoc" CssClass="Clicked" runat="server" Text="Aplication with Signature and Seal"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Scanned copy of signature and seal document:"></asp:Label>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Step1:Signature and Seal document can be obtain from Preview option in Renew-Save As Draft on Applicant Home Page."></asp:Label>
                                                                &nbsp; or&nbsp;                                                                                                                        
                                                                <asp:LinkButton ID="lbtnPrint" runat="server" PostBackUrl="~/ExternalUser/InfrastructureRenew/Reports/INFRenewSADReportViewer.aspx">click here</asp:LinkButton>
                                                                &nbsp; &nbsp;<br />
                                                                Step2:Scanned copy of page after signature and seal on printed page should be attached here before submission of application.
                                              
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Attachment Name :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtApplicationSignatureSeal" runat="server" MaxLength="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtApplicationSignatureSeal"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required"
                                                                    ValidationGroup="VGApplicationSignatureSealAttachment">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator runat="server" Display="Dynamic" ForeColor="Red"
                                                                    ControlToValidate="txtApplicationSignatureSeal" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGApplicationSignatureSealAttachment"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelApplicationSignatureSeal" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadApplicationSignatureSeal" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadApplicationSignatureSeal" runat="server" />
                                                                        <asp:Button ID="btnUploadApplicationSignatureSeal" runat="server" Text="Upload"
                                                                            OnClick="btnUploadApplicationSignatureSeal_Click"
                                                                            ValidationGroup="VGApplicationSignatureSealAttachment"
                                                                            OnClientClick="javascript:AplicationSignatureSealShowWait();" />
                                                                        <asp:UpdateProgress ID="UpdateProgressApplicationSignatureSeal" runat="server"
                                                                            AssociatedUpdatePanelID="UpdatePanelApplicationSignatureSeal">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblApplicationSignatureSealWait" runat="server" BackColor="#507CD1"
                                                                                    Font-Bold="True"
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
                                                <td colspan="3">
                                                    <asp:GridView ID="gvApplicationSignatureSeal" runat="server" AutoGenerateColumns="False"
                                                        CssClass="SubFormWOBG" DataKeyNames="InfrastructureRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvApplicationSignatureSeal_RowDeleting">
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
                                                                    <asp:LinkButton ID="lbtnAplicationSignatureSealViewFile" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnAplicationSignatureSealViewFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records Exist in Extra Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageAplicationSignatureSeal" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td valign="top">H.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblExtraAttachment2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblExtraAttachment" CssClass="Clicked" runat="server" Text="Extra Attachment"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Extra Attachment:"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtExtraAttachment" runat="server" MaxLength="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtExtraAttachment"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGExtraAttachment"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtExtraAttachment" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGExtraAttachment"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelExtra" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadExtra" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadExtra" runat="server" />

                                                                        <asp:Button ID="btnUploadExtra" runat="server" Text="Upload" OnClick="btnUploadExtra_Click"
                                                                            ValidationGroup="VGExtraAttachment" OnClientClick="javascript:ExtraShowWait();" />
                                                                        <asp:UpdateProgress ID="UpdateProgressExtra" runat="server" AssociatedUpdatePanelID="UpdatePanelExtra">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblExtraWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                <td colspan="3">
                                                    <asp:GridView ID="gvExtra" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                                        DataKeyNames="InfrastructureRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvExtra_RowDeleting">
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
                                                                    <asp:LinkButton ID="lbtnExtraViewFile" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnExtraViewFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records Exist in Extra Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageExtra" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                    <tr>
                                    <td valign="top">I.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" ID="lblProofofownershipofland"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblProofofownershipoflandofsize200sqmormore" CssClass="Clicked" runat="server" Text=" Proof of ownership of land of size 200 sq m or more"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Proof of ownership of land of size 200 sq m or more"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">Attachment Name :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtProofOfOwnershipOfLand" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtProofOfOwnershipOfLand" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGProofOfOwnershipOfLand"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtProofOfOwnershipOfLand"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGProofOfOwnershipOfLand"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelProofOfOwnershipOfLand" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadProofOfOwnershipOfLand" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadProofOfOwnershipOfLand" runat="server" />
                                                                        <asp:Button ID="btnUploadProofOfOwnershipOfLand" runat="server" Text="Upload" OnClick="btnUploadProofOfOwnershipOfLand_Click"
                                                                            OnClientClick="javascript:ShowProofOfOwnershipLand();" ValidationGroup="VGProofOfOwnershipOfLand" />
                                                                        <asp:UpdateProgress ID="UpdateProgressProofOfOwnershipOfLand" runat="server" AssociatedUpdatePanelID="UpdatePanelProofOfOwnershipOfLand">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblProofOfOwnershipOfLandWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                <td colspan="3">
                                                    <asp:GridView ID="gvProofOfOwnershipOfLand" runat="server" ShowHeaderWhenEmpty="true"
                                                        CssClass="SubFormWOBG" AutoGenerateColumns="False" DataKeyNames="InfrastructureRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%" OnRowDeleting="gvProofOfOwnershipOfLand_RowDeleting">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr.No.">
                                                                <ItemTemplate>
                                                                    <span>
                                                                        <%#Container.DataItemIndex + 1 %></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="S.No." Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAttachmentSerialNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'></asp:Label>
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
                                                                    <asp:LinkButton runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'
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
                                                            No Records Exist in Proof of ownership of land of size 200 sq m or more.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblProofofownershipoflandofsize200sqm" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td valign="top">J.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" ID="lblProofofownershipLease"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblProofofownershipLeaseoftanker" CssClass="Clicked" runat="server" Text=" Proof of ownership/Lease of tanker"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Proof of ownership/Lease of tanker"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">Attachment Name :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtProofofownershipLeaseoftanker" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtProofofownershipLeaseoftanker" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGProofofownershipLeaseoftanker"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtProofofownershipLeaseoftanker"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGProofofownershipLeaseoftanker"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelProofofownershipLeaseoftanker" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadProofofownershipLeaseoftanker" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadProofofownershipLeaseoftanker" runat="server" />
                                                                        <asp:Button ID="btnUploadProofofownershipLeaseoftanker" runat="server" Text="Upload" OnClick="btnUploadProofofownershipLeaseoftanker_Click"
                                                                            OnClientClick="javascript:ShowProofofownershipLeaseoftanker();" ValidationGroup="VGProofofownershipLeaseoftanker" />
                                                                        <asp:UpdateProgress ID="UpdateProgressProofofownershipLeaseoftanker" runat="server" AssociatedUpdatePanelID="UpdatePanelProofofownershipLeaseoftanker">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblProofofownershipLeaseoftankerWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                <td colspan="3">
                                                    <asp:GridView ID="gvProofofownershipLeaseoftanker" runat="server" ShowHeaderWhenEmpty="true"
                                                        CssClass="SubFormWOBG" AutoGenerateColumns="False" DataKeyNames="InfrastructureRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%" OnRowDeleting="gvProofofownershipLeaseoftanker_RowDeleting">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr.No.">
                                                                <ItemTemplate>
                                                                    <span>
                                                                        <%#Container.DataItemIndex + 1 %></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="S.No." Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAttachmentSerialNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'></asp:Label>
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
                                                                    <asp:LinkButton runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'
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
                                                            No Records Exist in Proof of Ownership / Lease of Tanker.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblProofofownershipLeaseTanker" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr runat="server" visible="false">
                                    <td valign="top">2.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblGroundwaterAvailability2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblGroundwaterAvailability" CssClass="Clicked" runat="server" Text="Groundwater Availability Report"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Groundwater Availability Report : (Refer: 5)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtgvGroundwaterAvailability" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtgvGroundwaterAvailability"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGGroundWaterAvailability"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtgvGroundwaterAvailability" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGGroundWaterAvailability"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelGroundwaterAvailability" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUplodGroundwaterAvailability" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadGroundwaterAvailability" runat="server" />
                                                                        <asp:Button ID="btnUplodGroundwaterAvailability" runat="server" Text="Upload" OnClick="btnUplodGroundwaterAvailability_Click"
                                                                            OnClientClick="javascript:showGroundwaterAvailability();" ValidationGroup="VGGroundWaterAvailability" />
                                                                        <asp:UpdateProgress ID="UpdateProgressGroundwaterAvailability" runat="server" AssociatedUpdatePanelID="UpdatePanelGroundwaterAvailability">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblWaterRequirementWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                <td colspan="3">
                                                    <asp:GridView ID="gvGroundwaterAvailability" runat="server" ShowHeaderWhenEmpty="true"
                                                        AutoGenerateColumns="False" DataKeyNames="InfrastructureRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        CssClass="SubFormWOBG" Width="100%" OnRowDeleting="gvGroundwaterAvailability_RowDeleting">
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
                                                                    <asp:LinkButton ID="lbtnViewGroundwaterAvailabilityFile" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnViewGroundwaterAvailabilityFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records Exist in Groundwater Availability Report.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageGroundwaterAvailability" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>


                                <tr runat="server" visible="false">
                                    <td valign="top">4.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblAuthorization2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblAuthorization" CssClass="Clicked" runat="server" Text="Authorization"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Authorization:"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtUndertaking" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtUndertaking"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGUndertaking"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtUndertaking" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGUndertaking"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelUndertaking" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUplodUndertaking" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadUndertaking" runat="server" />
                                                                        <asp:Button ID="btnUplodUndertaking" runat="server" Text="Upload" OnClick="btnUplodUndertaking_Click"
                                                                            OnClientClick="javascript:showUndertaking();" ValidationGroup="VGUndertaking" />
                                                                        <asp:UpdateProgress ID="UpdateProgressUndertaking" runat="server" AssociatedUpdatePanelID="UpdatePanelUndertaking">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblUndertakingWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                <td colspan="3">
                                                    <asp:GridView ID="gvUndertaking" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                                        DataKeyNames="InfrastructureRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvUndertaking_RowDeleting">
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
                                                                    <asp:LinkButton ID="lbtnViewUndertakingFile" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnViewUndertakingFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records Exist in Authorization.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageUndertaking" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>


                                <tr runat="server" visible="false">
                                    <td valign="top">7.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblComplianceReportOther2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblComplianceReportOther" CssClass="Clicked" runat="server" Text="Compliance Report- Other"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Compliance Condition NOC - Other: (Refer: 4(b))"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:GridView ID="gvINFRenewComplianceConditionNOCOther" runat="server" AutoGenerateColumns="False"
                                                                    CssClass="SubFormWOBG" ShowHeaderWhenEmpty="true" Width="100%" OnRowCommand="gvINFRenewComplianceConditionNOCOther_RowCommand">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sr.No.">
                                                                            <ItemTemplate>
                                                                                <span>
                                                                                    <%#Container.DataItemIndex + 1 %>
                                                                                </span>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Conditions given in NOC" ItemStyle-Width="25%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCompCondNOCOtherEnter" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("ComplianceConditionEnter")) %>'></asp:Label>
                                                                                <asp:Label ID="lblCompCondNOCOtherSerialNumber" runat="server" Visible="false" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("ComplianceConditionNOCSerialNumber")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status of compliance" ItemStyle-Width="12%" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStatusOfCompliance" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("StatusOfCompliance")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="File Upload" ItemStyle-Width="28%">
                                                                            <ItemTemplate>
                                                                                Attachment Name:
                                                                <asp:TextBox ID="txtAttachmentNameCompCondNOCOther" runat="server"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAttachmentNameCompCondNOCOther"
                                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required"
                                                                                    ValidationGroup='<%# System.Web.HttpUtility.HtmlEncode(Eval("ComplianceConditionNOCSerialNumber")+"O") %>'></asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtAttachmentNameCompCondNOCOther"
                                                                                    ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*" ErrorMessage="Allow only Alphanumeric and Characters . , _ / -"
                                                                                    ValidationGroup='<%# System.Web.HttpUtility.HtmlEncode(Eval("ComplianceConditionNOCSerialNumber")+"O") %>'></asp:RegularExpressionValidator><br />
                                                                                <asp:FileUpload ID="FileUploadCompCondNOCOther" runat="server" Width="180px" /><br />
                                                                                <asp:Button ID="btnUploadCompCondNOCOther" runat="server" Text="Upload"
                                                                                    ValidationGroup='<%# System.Web.HttpUtility.HtmlEncode(Eval("ComplianceConditionNOCSerialNumber")+"O") %>'
                                                                                    CommandName="UploadFileForCompCondNOCOther" /><br />
                                                                                <asp:Label ID="lblMessageINFCompCondAttachmentOther" runat="server" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Attachments">
                                                                            <ItemTemplate>
                                                                                <asp:GridView ID="gvCompCondNOCAttachmentOther" runat="server" AutoGenerateColumns="False"
                                                                                    CssClass="SubFormWOBG" ShowHeaderWhenEmpty="true" Width="100%"
                                                                                    OnRowCommand="gvCompCondNOCAttachmentOther_RowCommand">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Sr.No.">
                                                                                            <ItemTemplate>
                                                                                                <span>
                                                                                                    <%#Container.DataItemIndex + 1 %>
                                                                                                </span>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Attachment Name / File Name">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblFileName" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) + " /" %>'></asp:Label><br />
                                                                                                <asp:Label ID="lblAttachmentName" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lnkInfrastructureCompCondNOCAttachmentViewOther" OnCommand="lnkInfrastructureCompCondNOCAttachmentViewOther_Click"
                                                                                                    runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                                                    CommandName="View">View</asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                                                    CommandName="DeleteFile" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <EmptyDataTemplate>
                                                                                        No Records Exist in Compliance Condition NOC - Other Attachment.
                                                                                    </EmptyDataTemplate>
                                                                                </asp:GridView>
                                                                                <asp:Label ID="lblMessageINFCompCondNOCAttachmentOtherDelete" runat="server" Text="">
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                        No Records Exist.
                                                                    </EmptyDataTemplate>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr runat="server" visible="false">
                                    <td valign="top">10.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblSitePlan2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblSitePlan" CssClass="Clicked" runat="server" Text="Site Plan"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Site Plan : (Refer: 1 (ii))"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Attachment Name :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSitePlanAttachment" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSitePlanAttachment"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGSitePlan"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator42" runat="server"
                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtSitePlanAttachment" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGSitePlan"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Select Attachment File :
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
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:GridView ID="gvSitePlan" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                                        DataKeyNames="InfrastructureRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                                    <asp:LinkButton ID="lbtnViewFile" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                            No Records exist in Site Plan.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    <asp:Label ID="lblMessageSitePlan" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td valign="top">11.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblCertifiedRevenueSketch2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblCertifiedRevenueSketch" CssClass="Clicked" runat="server" Text="Certified Revenue Sketch"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Certified Revenue Sketch : (Refer: 1 (ii))"></asp:Label>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCertifiedRevenueSketchAttachment" runat="server" MaxLength="50"
                                                                    Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCertifiedRevenueSketchAttachment"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGRevenueSketchAttachment"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtCertifiedRevenueSketchAttachment" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGRevenueSketchAttachment"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelCertifiedRevenueSketch" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadCertifiedRevenueSketch" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="txtFileUploadCertifiedRevenueSketch" runat="server" />
                                                                        <asp:Button ID="btnUploadCertifiedRevenueSketch" runat="server" Text="Upload" OnClick="btnUploadCertifiedRevenueSketch_Click"
                                                                            ValidationGroup="VGRevenueSketchAttachment" OnClientClick="javascript:showCertifiedRevenueSketch();" />
                                                                        <asp:UpdateProgress ID="UpdateProgressCertifiedRevenueSketch" runat="server" AssociatedUpdatePanelID="UpdatePanelCertifiedRevenueSketch">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblCertifiedRevenueSketchWait" runat="server" BackColor="#507CD1"
                                                                                    Font-Bold="True" ForeColor="White" Text="Please wait ... Uploading file"></asp:Label>
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
                                                <td colspan="3">
                                                    <asp:GridView ID="gvCertifiedRevenueSketch" runat="server" ShowHeaderWhenEmpty="true"
                                                        CssClass="SubFormWOBG" AutoGenerateColumns="False" DataKeyNames="InfrastructureRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%" OnRowDeleting="gvCertifiedRevenueSketch_RowDeleting">
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
                                                                    <asp:LinkButton ID="lbtnViewCertifiedRevenueSketchFile" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnViewCertifiedRevenueSketchFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records Exist in Certified Revenue Sketch.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    <asp:Label ID="lblMessageCertifiedRevenueSketch" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td valign="top">12.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblReason2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblReason" CssClass="Clicked" runat="server" Text="Reason for Not Applying for Renewal before Expiring NOC"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Reason for Not Applying for Renewal before Expiring NOC : (Refer: 1 (iv))"></asp:Label>
                                                            </td>
                                                        </tr>

                                                        <tr>

                                                            <td valign="top">Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtReasonForNotApplyingBefore" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtReasonForNotApplyingBefore"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGDocumentofOwnership"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtReasonForNotApplyingBefore" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGDocumentofOwnership"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelReasonForNotApplyingBefore" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUplodReasonForNotApplyingBefore" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="txtFileUploadReasonForNotApplyingBefore" runat="server" />
                                                                        <asp:Button ID="btnUplodReasonForNotApplyingBefore" runat="server" Text="Upload"
                                                                            OnClick="btnUplodReasonForNotApplyingBefore_Click" OnClientClick="javascript:showReasonForNotApplyingBefore();"
                                                                            ValidationGroup="VGDocumentofOwnership" />
                                                                        <asp:UpdateProgress ID="UpdateProgressReasonForNotApplyingBefore" runat="server"
                                                                            AssociatedUpdatePanelID="UpdatePanelReasonForNotApplyingBefore">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblReasonForNotApplyingBeforeWait" runat="server" BackColor="#507CD1"
                                                                                    Font-Bold="True" ForeColor="White" Text="Please wait ... Uploading file"></asp:Label>
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
                                                <td colspan="3">
                                                    <asp:GridView ID="gvReasonForNotApplyingBefore" runat="server" AutoGenerateColumns="False"
                                                        CssClass="SubFormWOBG" DataKeyNames="InfrastructureRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvReasonForNotApplyingBefore_RowDeleting">
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
                                                                    <asp:LinkButton ID="lbtnViewReasonForNotApplyingBeforeFile" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnViewReasonForNotApplyingBeforeFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records Exist in Reason For Not Applying For Renewal Before.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageReasonForNotApplyingBefore" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td valign="top">13.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblEnclose2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblEnclose" CssClass="Clicked" runat="server" Text="Enclose Flow Chart of Activity and Requirement of Water : (Refer: 2)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Enclose Flow Chart of Activity and Requirement of Water : (Refer: 2)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtWaterRequirement" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtWaterRequirement"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGWaterRequirement"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtWaterRequirement" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGWaterRequirement"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelWaterRequirement" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUplodWaterRequirement" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadWaterRequirement" runat="server" />
                                                                        <asp:Button ID="btnUplodWaterRequirement" runat="server" Text="Upload" OnClick="btnUplodWaterRequirement_Click"
                                                                            OnClientClick="javascript:showWaterRequirement();" ValidationGroup="VGWaterRequirement" />
                                                                        <asp:UpdateProgress ID="UpdateProgressWaterRequirement" runat="server" AssociatedUpdatePanelID="UpdatePanelWaterRequirement">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblGroundwaterAvailabilityWait" runat="server" BackColor="#507CD1"
                                                                                    Font-Bold="True" ForeColor="White" Text="Please wait ... Uploading file"></asp:Label>
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
                                                <td colspan="3">
                                                    <asp:GridView ID="gvWaterRequirement" runat="server" AutoGenerateColumns="False"
                                                        CssClass="SubFormWOBG" DataKeyNames="InfrastructureRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvWaterRequirement_RowDeleting">
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
                                                                    <asp:LinkButton ID="lbtnViewGroundwaterAvailabilityFile" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnViewGroundwaterAvailabilityFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records Exist in Enclose flow Chart of Activity and Requirement.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageWaterRequirement" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td valign="top">14.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblReferralLetter2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblReferralLetter" CssClass="Clicked" runat="server" Text="Copy of Referral Letter Seeking NOC from CGWA"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Copy of Referral Letter Seeking NOC from CGWA: (Refer: 6)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Referral Letter type :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlReferralLetter" runat="server" Width="200px" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlReferralLetter"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGReferalLetter"
                                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtReferralLetter" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtReferralLetter"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGReferalLetter"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtReferralLetter" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGReferalLetter"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <%--<asp:UpdatePanel ID="UpdatePanelReferralLetter" runat="server">
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnUploadReferralLetter" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:FileUpload ID="FileUploadReferralLetter" runat="server" />
                                                        <asp:Button ID="btnUploadReferralLetter" runat="server" Text="Upload" OnClick="btnUplodReferralLetter_Click"
                                                            OnClientClick="javascript:showReferralLetter();" ValidationGroup="VGReferalLetter" />
                                                        <asp:UpdateProgress ID="UpdateProgressReferralLetter" runat="server" AssociatedUpdatePanelID="UpdatePanelReferralLetter">
                                                            <ProgressTemplate>
                                                                <asp:Label ID="lblReferralLetterWait" runat="server" BackColor="#507CD1" Font-Bold="True"
                                                                    ForeColor="White" Text="Please wait ... Uploading file"></asp:Label>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:GridView ID="gvReferralLetterAttachment" runat="server" ShowHeaderWhenEmpty="true"
                                                        CssClass="SubFormWOBG" AutoGenerateColumns="False" DataKeyNames="InfrastructureNewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%">
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
                                                                    <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode( Eval("AttachmentName")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="File Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFileName" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View File">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnViewReferralLetterFile" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureNewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'>View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records Exist in Copy of Refferal Letter Seeking NOC from CGWA.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageReferralLetter" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td valign="top">15.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblNonPolluting2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblNonPolluting" CssClass="Clicked" runat="server" Text="Non-Polluting Effluent Attachment"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Non-Polluting Effluent Attachment:"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Attachment Name :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNonPollutingAttachment" runat="server" MaxLength="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtNonPollutingAttachment"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGNonPollutingAttachment"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtNonPollutingAttachment"
                                                                    ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*" ErrorMessage="Allow only Alphanumeric and Characters . , _ / -"
                                                                    ValidationGroup="VGNonPollutingAttachment"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <%--<asp:UpdatePanel ID="UpdatePanelNonPolluting" runat="server">
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnNonPolluting" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:FileUpload ID="FileUploadNonPolluting" runat="server" />
                                                        <asp:Button ID="btnNonPolluting" runat="server" Text="Upload" ValidationGroup="VGNonPollutingAttachment"
                                                            OnClientClick="javascript:NonPollutingShowWait();" OnClick="btnNonPolluting_Click" />
                                                        <asp:UpdateProgress ID="UpdateProgressNonPolluting" runat="server" AssociatedUpdatePanelID="UpdatePanelNonPolluting">
                                                            <ProgressTemplate>
                                                                <asp:Label ID="lblNonPollutingWait" runat="server" BackColor="#507CD1" Font-Bold="True"
                                                                    ForeColor="White" Text="Please wait ... Uploading file"></asp:Label>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:GridView ID="gvNonPolluting" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                                        DataKeyNames="InfrastructureNewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%">
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
                                                                    <asp:LinkButton ID="lbtnNonPollutingViewFile" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("InfrastructureNewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'>View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records Exist in Non-Polluting Effluent Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageNonPolluting" runat="server"></asp:Label>
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
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblInfrastructureApplicationCodeFrom" Visible="false" runat="server"
                                Enabled="False"></asp:Label>
                            <asp:Label ID="lblFinalMsg" runat="server"></asp:Label>

                            <asp:Label ID="lblApplicationCode" Visible="false" runat="server" Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('If you have not saved data, Your data will be lost before moving to other page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" ValidationGroup="CommunicationAddress"
                                OnClick="btnSaveAsDraft_Click" />
                            <asp:Button ID="btnSubmit" runat="server" Text="Next >>" ValidationGroup="CommunicationAddress"
                                OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
