<%@ Page Title="NOCAP-Infrastructure Application-Expansion" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="Attachment.aspx.cs"
    Inherits="ExternalUser_Expansion_INF_Attachment" %>

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
    <script language="javascript" type="text/javascript">




        function WetlandAreaShowWait() {
            if (document.getElementById('<%= txtWetlandArea.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadWetlandArea.ClientID %>').value > 0) {
                var x = document.getElementById('<%= UpdateProgressWetlandArea.ClientID %>')
                x.style.display = 'inline';

            }
        }
        function CertiNonAvaShowWait() {
            if (document.getElementById('<%= txtCertiNonAva.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadCertiNonAva.ClientID %>').value > 0) {
                var x = document.getElementById('<%= UpdateProgressCertiNonAva.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function AffidavitShowWait() {
            if (document.getElementById('<%= txtAffidavit.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadAffidavit.ClientID %>').value > 0) {
                var x = document.getElementById('<%= UpdateProgressAffidavit.ClientID %>')
                x.style.display = 'inline';

            }
        }
        function ImpactReportShowWait() {
            if (document.getElementById('<%= txtImpactReport.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadImpactReport.ClientID %>').value > 0) {
                var x = document.getElementById('<%= UpdateProgressImpactReport.ClientID %>')
                x.style.display = 'inline';

            }
        }
        function CompletionCertiShowWait() {
            if (document.getElementById('<%= txtCompletionCerti.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadCompletionCerti.ClientID %>').value > 0) {
                var x = document.getElementById('<%= UpdateProgressCompletionCerti.ClientID %>')
                x.style.display = 'inline';

            }
        }
        function InstSTPShowWait() {
            if (document.getElementById('<%= txtInstSTP.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadInstSTP.ClientID %>').value > 0) {
                var x = document.getElementById('<%= UpdateProgressInstSTP.ClientID %>')
                x.style.display = 'inline';

            }
        }
        function PenaltyShowWait() {
            if (document.getElementById('<%= txtPenalty.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadPenalty.ClientID %>').value > 0) {
                var x = document.getElementById('<%= UpdateProgressPenalty.ClientID %>')
                x.style.display = 'inline';

            }
        }






        function showWait() {
            if (document.getElementById('<%= txtSitePlanAttachment.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadSitePlan.ClientID %>').value > 0) {
                //       
                var x = document.getElementById('<%= UpdateProgressSitePlan.ClientID %>')

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
            if (document.getElementById('<%= txtApplicationSignatureSeal.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadAplicationSignatureSeal.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressAplicationSignatureSeal.ClientID %>')
                x.style.display = 'inline';
            }
        }

        function showCertifiedRevenueSketch() {
            if (document.getElementById('<%= txtLocationMapAttachment.ClientID %>').value.length > 0 && document.getElementById('<%= txtFileUploadLocationMap.ClientID %>').value > 0) {

                var x = document.getElementById('<%= UpdateProgressCertifiedRevenueSketch.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showDocumentsofOwnership() {
            if (document.getElementById('<%= txtDocumentsofOwnership.ClientID %>').value.length > 0 && document.getElementById('<%= txtFileUploadDocumentsofOwnership.ClientID %>').value > 0) {

                var x = document.getElementById('<%= UpdateProgressDocumentsofOwnership.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showSourceofAvailability() {
            if (document.getElementById('<%= txtSourceofAvailabilityofSurfaceWater.ClientID %>').value.length > 0 && document.getElementById('<%= txtFileUploadSourceofAvailabilityofSurfaceWater.ClientID %>').value > 0) {

                var x = document.getElementById('<%= UpdateProgressSourceofAvailabilityofSurfaceWater.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showWaterRequirement() {
            if (document.getElementById('<%= txtWaterRequirement.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadWaterRequirement.ClientID %>').value > 0) {

                var x = document.getElementById('<%= UpdateProgressWaterRequirement.ClientID %>')
                x.style.display = 'inline';
            }
        }

        function showGroundwaterAvailability() {
            if (document.getElementById('<%= txtgvGroundwaterAvailability.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadGroundwaterAvailability.ClientID %>').value > 0) {

                var x = document.getElementById('<%= UpdateProgressGroundwaterAvailability.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showRainwaterHarvesting() {
            if (document.getElementById('<%= txtRainwaterHarvesting.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadRainwaterHarvesting.ClientID %>').value > 0) {
                var x = document.getElementById('<%= UpdateProgressRainwaterHarvesting.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showUndertaking() {
            if (document.getElementById('<%= txtUndertaking.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadUndertaking.ClientID %>').value > 0) {

                var x = document.getElementById('<%= UpdateProgressUndertaking.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showReferralLetter() {
            if (document.getElementById('<%= txtReferralLetter.ClientID %>').value.length && document.getElementById('<%= FileUploadReferralLetter.ClientID %>').value > 0 && document.getElementById('<%= txtSitePlanAttachment.ClientID %>').value > 0) {

                var x = document.getElementById('<%= UpdateProgressReferralLetter.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showApprovalLetter() {
            if (document.getElementById('<%= txtApprovalLetterName.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadApprovalLetter.ClientID %>').value > 0) {
                //       
                var x = document.getElementById('<%= UpdateProgress1.ClientID %>')
                //        alert(x);

                x.style.display = 'inline';

            }
        }

        function ShowGroundwaterquality() {
            if (document.getElementById('<%= txtGroundwaterquality.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadGroundwaterquality.ClientID %>').value > 0) {
                var x = document.getElementById('<%= UpdateProgressGroundwaterquality.ClientID %>')
                x.style.display = 'inline';
            }
        }

        function ExtraShowWait() {
            if (document.getElementById('<%= txtExtraAttachment.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadExtra.ClientID %>').value > 0) {
                var x = document.getElementById('<%= UpdateProgressExtra.ClientID %>')
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
                                            <li class="visited">De-Watering Existing Structure</li>
                                            <li class="visited">De-Watering Proposed Structure</li>
                                            <li class="visited">Groundwater Abstraction Structure-Existing</li>
                                            <li class="visited">Groundwater Abstraction Structure-Proposed</li>
                                            <li class="visited">Breakup of Water Requirment</li>
                                            <li class="visited">Water Supply Detail</li>
                                            <%--<li class="visited">Groundwater Abstraction Structure-Existing</li>
                                            <li class="visited">Groundwater Abstraction Structure-Proposed</li>--%>
                                            <li class="visited">Other Details</li>
                                            <%--  <li class="visited">Self Declaration</li>--%>
                                            <li class="active">Attachment</li>
                                            <li>Online Payment</li>
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
                                INFRASTRUCTURE EXPANSION USE: Attachment
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
                        <td style="text-align: right">(<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-Upload
                            Attachments in <strong style="color: Red">Attachment</strong> Section<br />
                            Maximum Number of Attachments Allowed- 5<br />
                            Maximum Size of each Attachment Allowed- 5MB<br />
                            Allowed File Type for Attachment- txt, doc, docx, jpg, jpeg, pdf
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
                                <tr runat="server">
                                    <td valign="top">A.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td valign="top">
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblWaterBalanceFlowChart2"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblWaterBalanceFlowChart" CssClass="Clicked" runat="server" Text="Water Requirement"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Water requirement computed as per National Building Code, 2016 : (Refer: 3)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtWaterRequirement" runat="server" MaxLength="50"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtWaterRequirement" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGWaterRequirement"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtWaterRequirement"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGWaterRequirement"></asp:RequiredFieldValidator>
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
                                                    <asp:GridView ID="gvWaterRequirement" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                                        ShowHeaderWhenEmpty="true" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%" OnRowDeleting="gvWaterRequirement_RowDeleting">
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
                                                                    <asp:LinkButton runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'
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
                                                            No Records Exist in Water Balance Flow Chart.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    <asp:Label ID="lblMessageWaterRequirement" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td valign="top">B.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td valign="top">
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td valign="top" colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblAffidavit2"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblAffidavit" CssClass="Clicked" runat="server" Text="Affidavit"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Affidavit on non judicial stamp paper of Rs. 10/- regarding non availability of water from any other source in case water is required for construction in safe and semi critical areas:"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAffidavit" runat="server" MaxLength="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAffidavit"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGAffidavitAttachment"> </asp:RequiredFieldValidator>
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
                                                <td>
                                                    <asp:GridView ID="gvAffidavit" runat="server" AutoGenerateColumns="False"
                                                        CssClass="SubFormWOBG" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="Affidavit_RowDeleting">
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
                                                            No Records Exist in Affidavit Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        &nbsp;<asp:Label ID="lblMessageAffidavit" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">C.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td valign="top">
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblSourceWaterAvailability2"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblSourceWaterAvailability" CssClass="Clicked" runat="server" Text="Source Water Availability/Non-availability Certificate Construction Purpose" Width="432px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">

                                                                <asp:Label runat="server" Text="Certificate from a government agency regarding non availability of treated sewage water for construction within 10 km radius of the site in critical and over-exploited areas"></asp:Label>
                                                                <%-- <asp:Label runat="server" Text="Source Water Availability/Non-availability Certificate : (Previous:Source of Availability
                                                of Surface Water): (Refer: 1 (vi))"></asp:Label>--%>
                                                            </td>
                                                        </tr>

                                                        <tr>

                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSourceofAvailabilityofSurfaceWater" runat="server" MaxLength="50"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtSourceofAvailabilityofSurfaceWater" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGSurfaceWater"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtSourceofAvailabilityofSurfaceWater"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGSurfaceWater"></asp:RequiredFieldValidator>
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
                                                                            OnClick="lbtnUplodSourceofAvailabilityofSurfaceWaterFile_Click" OnClientClick="javascript:showSourceofAvailability();"
                                                                            ValidationGroup="VGSurfaceWater" />
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
                                                <td colspan="3">
                                                    <asp:GridView ID="gvSourceofAvailabilityofSurfaceWater" runat="server" CssClass="SubFormWOBG"
                                                        ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%" OnRowDeleting="gvSourceofAvailabilityofSurfaceWater_RowDeleting">
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
                                                                    <asp:LinkButton runat="server"
                                                                        CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'
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
                                                    &nbsp;<asp:Label ID="lblMessageSourceofAvailabilityofSurfaceWater" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td valign="top">D.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td valign="top">
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td valign="top" colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblCertiNonAva2"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblCertiNonAva" CssClass="Clicked" runat="server" Text="Certificate of non-availability of water"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Certificate of non-availability of water from local government water supply agency in respect of all categories of assessments units for commercial use (Except Annex. B & C):"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCertiNonAva" runat="server" MaxLength="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCertiNonAva"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGCertiNonAvaAttachment"> </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator runat="server"
                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtCertiNonAva" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGCertiNonAvaAttachment"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelCertiNonAva" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadCertiNonAva" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadCertiNonAva" runat="server" />
                                                                        <asp:Button ID="btnUploadCertiNonAva" runat="server" Text="Upload" OnClick="btnUploadCertiNonAva_Click"
                                                                            ValidationGroup="VGCertiNonAvaAttachment" OnClientClick="javascript:CertiNonAvaShowWait();" />
                                                                        <asp:UpdateProgress ID="UpdateProgressCertiNonAva" runat="server" AssociatedUpdatePanelID="UpdatePanelCertiNonAva">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblCertiNonAvaWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                    <asp:GridView ID="gvCertiNonAva" runat="server" AutoGenerateColumns="False"
                                                        CssClass="SubFormWOBG" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvCertiNonAva_RowDeleting">
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
                                                            No Records Exist in Certificate Non-Availability Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        &nbsp;<asp:Label ID="lblMessageCertiNonAva" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">E.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td valign="top">
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblRainWaterHarv2"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblRainWaterHarv" CssClass="Clicked" runat="server" Text="Rain Water Harvesting/ Recharge"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Proposal for rain water harvesting/ recharge within the premises as per Model Building Bye Laws:
                                                (Refer: 8)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRainwaterHarvesting" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtRainwaterHarvesting" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGRainWaterHarvesting"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtRainwaterHarvesting"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGRainWaterHarvesting"></asp:RequiredFieldValidator>
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
                                                    <asp:GridView ID="gvRainwaterHarvesting" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                                        ShowHeaderWhenEmpty="true" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%" OnRowDeleting="gvRainwaterHarvesting_RowDeleting">
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
                                                                    <asp:LinkButton runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'
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
                                                            No Records Exist in Rainwater Harvesting / Artificial Recharge Measures.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageRainwaterHarvesting" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" id="rowIAR">
                                    <td valign="top">F.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td valign="top">
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td valign="top" colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblImpactReport2"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblImpactReport" CssClass="Clicked" runat="server" Text="Impact Assessment Report by Accredited Consultant in case of Dewatering"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Impact Assessment Report by Accredited Consultant in case of Dewatering:"></asp:Label>
                                                                <br />
                                                                Note: OTP verification is compulsory before uploading attachment. OTP will be send to selected accredited consultant through sms amd email. Please get&nbsp; it from accredited consultant and verify OTP.
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Consultant Name:
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlConsultant" runat="server">
                                                                </asp:DropDownList>
                                                                <span>
                                                                    <asp:RequiredFieldValidator ID="rfvConsultant" runat="server" ForeColor="Red" ValidationGroup="VGImpactReportOCSVerify" ControlToValidate="ddlConsultant" ErrorMessage="Consultant  Required"></asp:RequiredFieldValidator>
                                                                    <br />
                                                                    <asp:RequiredFieldValidator ID="rfvConsultantSendOtp" runat="server" ForeColor="Red" ValidationGroup="SendOTP" ControlToValidate="ddlConsultant" ErrorMessage="Consultant Required"></asp:RequiredFieldValidator>
                                                                </span>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                     <asp:Button ID="btnSendOTP" runat="server" Text="Send OTP" OnClick="btnSendOTP_Click" ValidationGroup="SendOTP" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Enter OTP:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtImpactReportOCSOTP" runat="server" MaxLength="8" Width="200px"></asp:TextBox>

                                                                <asp:RegularExpressionValidator ID="revtxtImpactReportOCSOTP" Display="Dynamic" runat="server" ValidationGroup="VGImpactReportOCSVerify" ValidationExpression="^(0|[1-9][0-9]*)$" ForeColor="Red" ControlToValidate="txtImpactReportOCSOTP" ErrorMessage="only numeric value"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ValidationGroup="VGImpactReportOCSVerify" ForeColor="Red" ControlToValidate="txtImpactReportOCSOTP" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>OTP Verified:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                                                                <asp:Label ID="lblOTPVerified" Font-Bold="true" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">

                                                                <asp:Button ID="btnOTPVerify" runat="server" Text="OTP Verify" ValidationGroup="VGImpactReportOCSVerify" OnClick="btnOTPVerify_Click" />
                                                                <asp:Label ID="lblOTPMsg" runat="server" Text=""></asp:Label>
                                                            </td>

                                                        </tr>

                                                        <tr>
                                                            <td colspan="2">
                                                                <table class="SubFormWOBG" width="100%" runat="server" id="tableIAR" visible="false">
                                                                    <tr>
                                                                        <td>Attachment Name :
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtImpactReport" runat="server" MaxLength="50"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtImpactReport"
                                                                                Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGImpactReportAttachment"> </asp:RequiredFieldValidator>
                                                                            <asp:RegularExpressionValidator runat="server"
                                                                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtImpactReport" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                                ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGImpactReportAttachment"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Select Attachment File :
                                                                        </td>
                                                                        <td>
                                                                            <asp:UpdatePanel ID="UpdatePanelImpactReport" runat="server">
                                                                                <Triggers>
                                                                                    <asp:PostBackTrigger ControlID="btnUploadImpactReport" />
                                                                                </Triggers>
                                                                                <ContentTemplate>
                                                                                    <asp:FileUpload ID="FileUploadImpactReport" runat="server" />
                                                                                    <asp:Button ID="btnUploadImpactReport" runat="server" Text="Upload"
                                                                                        OnClick="btnUploadImpactReport_Click" ValidationGroup="VGImpactReportAttachment" OnClientClick="javascript:ImpactReportShowWait();" />
                                                                                    <asp:UpdateProgress ID="UpdateProgressImpactReport" runat="server" AssociatedUpdatePanelID="UpdatePanelImpactReport">
                                                                                        <ProgressTemplate>
                                                                                            <asp:Label ID="lblImpactReportWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                    <asp:GridView ID="gvImpactReport" runat="server" AutoGenerateColumns="False"
                                                        CssClass="SubFormWOBG" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvImpactReport_RowDeleting">
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
                                                            No Records Exist in Impact Report Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        &nbsp;<asp:Label ID="lblMessageImpactReport" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">G.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td valign="top">
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblGroundwaterquality2"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblGroundwaterquality" CssClass="Clicked" runat="server" Text="Ground Water Quality"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Ground water quality data of existing bore well/ tube well/ dug well from any NABL accredited laboratory or Govt. approved laboratory:"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtGroundwaterquality" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator runat="server"
                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtGroundwaterquality" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGGroundwaterquality"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtGroundwaterquality"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGGroundwaterquality"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>


                                                        <tr>

                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelGroundwaterquality" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadGroundwaterquality" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadGroundwaterquality" runat="server" />
                                                                        <asp:Button ID="btnUploadGroundwaterquality" runat="server" Text="Upload"
                                                                            OnClientClick="javascript:ShowGroundwaterquality();"
                                                                            ValidationGroup="VGGroundwaterquality" OnClick="btnUploadGroundwaterquality_Click" />
                                                                        <asp:UpdateProgress ID="UpdateProgressGroundwaterquality" runat="server"
                                                                            AssociatedUpdatePanelID="UpdatePanelGroundwaterquality">
                                                                            <ProgressTemplate>
                                                                                <asp:Label runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                    <asp:GridView ID="gvGroundwaterquality" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                                        ShowHeaderWhenEmpty="true" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%" OnRowDeleting="gvGroundwaterquality_RowDeleting">
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
                                                                    <asp:LinkButton runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'
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
                                                            No Records Exist in Ground Water Quality Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageGroundwaterquality" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td valign="top">H.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td valign="top">
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td valign="top" colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblCompletionCerti2"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblCompletionCerti" CssClass="Clicked" runat="server" Text="Completion certificate from the concerned agency"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Completion certificate from the concerned agency for infrastructure projects requiring water for
commercial use:"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCompletionCerti" runat="server" MaxLength="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCompletionCerti"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGCompletionCertiAttachment"> </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator runat="server"
                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtCompletionCerti" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGCompletionCertiAttachment"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelCompletionCerti" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadCompletionCerti" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadCompletionCerti" runat="server" />
                                                                        <asp:Button ID="btnUploadCompletionCerti" runat="server" Text="Upload" OnClick="btnUploadCompletionCerti_Click"
                                                                            ValidationGroup="VGWetlandAreaAttachment" OnClientClick="javascript:CompletionCertiShowWait();" />
                                                                        <asp:UpdateProgress ID="UpdateProgressCompletionCerti" runat="server" AssociatedUpdatePanelID="UpdatePanelCompletionCerti">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblCompletionCertiWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                    <asp:GridView ID="gvCompletionCerti" runat="server" AutoGenerateColumns="False"
                                                        CssClass="SubFormWOBG" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvCompletionCerti_RowDeleting">
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
                                                            No Records Exist in Completion Certificate Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        &nbsp;<asp:Label ID="lblMessageCompletionCerti" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td valign="top">I.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td valign="top">
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td valign="top" colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblInstSTP2"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblInstSTP" CssClass="Clicked" runat="server" Text="Installation of STP (For New Projects)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Installation of STP (For New Projects):"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtInstSTP" runat="server" MaxLength="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtInstSTP"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGInstSTPAttachment"> </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator runat="server"
                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtInstSTP" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGInstSTPAttachment"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelInstSTP" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadInstSTP" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadInstSTP" runat="server" />
                                                                        <asp:Button ID="btnUploadInstSTP" runat="server" Text="Upload" OnClick="btnUploadInstSTP_Click"
                                                                            ValidationGroup="VGInstSTPAttachment" OnClientClick="javascript:InstSTPShowWait();" />
                                                                        <asp:UpdateProgress ID="UpdateProgressInstSTP" runat="server" AssociatedUpdatePanelID="UpdatePanelInstSTP">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblInstSTPWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                    <asp:GridView ID="gvInstSTP" runat="server" AutoGenerateColumns="False"
                                                        CssClass="SubFormWOBG" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvInstSTP_RowDeleting">
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
                                                            No Records Exist in Instalation of STP Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        &nbsp;<asp:Label ID="lblMessageInstSTP" runat="server"></asp:Label>
                                    </td>
                                </tr>

                                <tr runat="server">
                                    <td valign="top">J.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td valign="top">
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td valign="top" colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblWetlandArea2"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblWetlandArea" CssClass="Clicked" runat="server" Text="Approval from Wetland Authority"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Approval from Wetland Authority (in case of project area falling in Wetland zone):"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtWetlandArea" runat="server" MaxLength="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtWetlandArea"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGWetlandAreaAttachment"> </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator runat="server"
                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtWetlandArea" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGWetlandAreaAttachment"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelWetlandArea" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadWetlandArea" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadWetlandArea" runat="server" />
                                                                        <asp:Button ID="btnUploadWetlandArea" runat="server" Text="Upload" OnClick="btnUploadWetlandArea_Click"
                                                                            ValidationGroup="VGWetlandAreaAttachment" OnClientClick="javascript:WetlandAreaShowWait();" />
                                                                        <asp:UpdateProgress ID="UpdateProgressWetlandArea" runat="server" AssociatedUpdatePanelID="UpdatePanelWetlandArea">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblWetlandAreaWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                    <asp:GridView ID="gvWetlandArea" runat="server" AutoGenerateColumns="False"
                                                        CssClass="SubFormWOBG" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvWetlandArea_RowDeleting">
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
                                                            No Records Exist in Wetland Area Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        &nbsp;<asp:Label ID="lblMessageWetlanArea" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td valign="top">K.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">

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
                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBharatKoshReciept"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGBharatKoshRecieptAttachment"> </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator runat="server" Display="Dynamic" ForeColor="Red"
                                                                    ControlToValidate="txtBharatKoshReciept" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
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
                                                        CssClass="SubFormWOBG" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                            No Records Exist in BharatKosh Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageBharatKoshReciept" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">K.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblSigneddoc2"> <span class="Coumpulsory">*</span> </asp:Label>
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
                                                                <asp:Label runat="server" Text="Step1:Signature and Seal document can be obtain from Preview option in New-Save As Draft on Applicant Home Page"></asp:Label>
                                                                &nbsp; or&nbsp;                                                                                                                      
                                                                <asp:LinkButton ID="lbtnPrint" runat="server" PostBackUrl="~/ExternalUser/InfrastructureNew/Reports/INFSADReportViewer.aspx">click here</asp:LinkButton>
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
                                                                    ValidationGroup="VGApplicationSignatureSealAttachment"> </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator runat="server" Display="Dynamic" ForeColor="Red"
                                                                    ControlToValidate="txtApplicationSignatureSeal" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGApplicationSignatureSealAttachment"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelAplicationSignatureSeal" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadAplicationSignatureSeal" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadAplicationSignatureSeal" runat="server" />
                                                                        <asp:Button ID="btnUploadAplicationSignatureSeal" runat="server" Text="Upload"
                                                                            OnClick="btnUploadAplicationSignatureSeal_Click"
                                                                            ValidationGroup="VGApplicationSignatureSealAttachment"
                                                                            OnClientClick="javascript:AplicationSignatureSealShowWait();" />
                                                                        <asp:UpdateProgress ID="UpdateProgressAplicationSignatureSeal" runat="server"
                                                                            AssociatedUpdatePanelID="UpdatePanelAplicationSignatureSeal">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblAplicationSignatureSealWait" runat="server" BackColor="#507CD1"
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
                                                    <asp:GridView ID="gvAplicationSignatureSeal" runat="server" AutoGenerateColumns="False"
                                                        CssClass="SubFormWOBG" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvAplicationSignatureSeal_RowDeleting">
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
                                                            No Records Exist in Application Seal and Signature Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageAplicationSignatureSeal" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td valign="top">L.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td valign="top">
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td valign="top" colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblPenalty2"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblPenalty" CssClass="Clicked" runat="server" Text="Penalty"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Penalty:"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPenalty" runat="server" MaxLength="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPenalty"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGPenaltyAttachment"> </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator runat="server"
                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtPenalty" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGPenaltyAttachment"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelPenalty" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadPenalty" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadPenalty" runat="server" />
                                                                        <asp:Button ID="btnUploadPenalty" runat="server" Text="Upload" OnClick="btnUploadPenalty_Click"
                                                                            ValidationGroup="VGPenaltyAttachment" OnClientClick="javascript:PenaltyShowWait();" />
                                                                        <asp:UpdateProgress ID="UpdateProgressPenalty" runat="server" AssociatedUpdatePanelID="UpdatePanelPenalty">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblPenaltyWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                    <asp:GridView ID="gvPenalty" runat="server" AutoGenerateColumns="False"
                                                        CssClass="SubFormWOBG" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvPenalty_RowDeleting">
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
                                                            No Records Exist in Wetland Area Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        &nbsp;<asp:Label ID="lblMessagePenalty" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">L.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblExtraAttachment2"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblExtraAttachment" CssClass="Clicked" runat="server" Text="Extra Attachment"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Extra Attachment :"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtExtraAttachment" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtExtraAttachment" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGExtraAttachment"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtExtraAttachment"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGExtraAttachment"></asp:RequiredFieldValidator>
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
                                                                            OnClientClick="javascript:ExtraShowWait();" ValidationGroup="VGExtraAttachment" />
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
                                                    <asp:GridView ID="gvExtra" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                                        ShowHeaderWhenEmpty="true" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%" OnRowDeleting="gvExtra_RowDeleting">
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
                                                                    <asp:LinkButton runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'
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
                                    <td valign="top">N.
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
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtProofOfOwnershipOfLand" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGProofOfOwnershipOfLand"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtProofOfOwnershipOfLand"
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
                                                        CssClass="SubFormWOBG" AutoGenerateColumns="False" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                                    <asp:LinkButton runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'
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
                                    <td valign="top">O.
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
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" Display="Dynamic"
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
                                                        CssClass="SubFormWOBG" AutoGenerateColumns="False" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                                    <asp:LinkButton runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'
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
                                                                <asp:Label runat="server" Visible="false" ID="lblHydrogeologicalReport2"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblHydrogeologicalReport" CssClass="Clicked" runat="server" Text="Hydrogeological Report"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Hydrogeological Report (Previous:Groundwater Availability): (Refer: 7)
                                                ">&nbsp;Max File Size - 20MB</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtgvGroundwaterAvailability" runat="server" MaxLength="50"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtgvGroundwaterAvailability" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGGroundwaterAvailability"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtgvGroundwaterAvailability"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGGroundwaterAvailability"></asp:RequiredFieldValidator>
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
                                                                            OnClientClick="javascript:showGroundwaterAvailability();" ValidationGroup="VGGroundwaterAvailability" />
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
                                                    <asp:GridView ID="gvGroundwaterAvailability" runat="server" CssClass="SubFormWOBG"
                                                        ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%" OnRowDeleting="gvGroundwaterAvailability_RowDeleting">
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
                                                                    <asp:LinkButton runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'
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
                                                            No Records Exist in Groundwater Availability.
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
                                                                <asp:Label runat="server" Visible="false" ID="lblAuthorizationLetter2"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblAuthorizationLetter" CssClass="Clicked" runat="server" Text="Authorization Letter (Previous:Authorization)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Authorization Letter (Previous:Authorization) :"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtUndertaking" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtUndertaking" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGUndertaking"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtUndertaking"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGUndertaking"></asp:RequiredFieldValidator>
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
                                                    <asp:GridView ID="gvUndertaking" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                                        ShowHeaderWhenEmpty="true" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%" OnRowDeleting="gvUndertaking_RowDeleting">
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
                                                                    <asp:LinkButton runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'
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
                                                            No Records Exist in Authorization Letter.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageUndertaking" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td valign="top">5.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblConsentApproval2"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblConsentApproval" CssClass="Clicked" runat="server" Text="Consent/ Approval of Government Agency"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Consent/ Approval of Government Agency (Previous:Copy of Referral Letter seeking
                                                NOC from CGWA): (Refer: 10)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Referral Letter Type :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlReferralLetter" runat="server" Width="200px" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlReferralLetter_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlReferralLetter"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGReferralLetter"
                                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtReferralLetter" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtReferralLetter" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGReferralLetter"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtReferralLetter"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGReferralLetter"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelReferralLetter" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadReferralLetter" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadReferralLetter" runat="server" />
                                                                        <asp:Button ID="btnUploadReferralLetter" runat="server" Text="Upload" OnClick="btnUplodReferralLetter_Click"
                                                                            OnClientClick="javascript:showReferralLetter();" ValidationGroup="VGReferralLetter" />
                                                                        <asp:UpdateProgress ID="UpdateProgressReferralLetter" runat="server" AssociatedUpdatePanelID="UpdatePanelReferralLetter">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblReferralLetterWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                    <asp:GridView ID="gvReferralLetterAttachment" runat="server" CssClass="SubFormWOBG"
                                                        ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%" OnRowDeleting="gvReferralLetterAttachment_RowDeleting">
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
                                                                    <asp:LinkButton runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'
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
                                                            No Records Exist in Consent/ Approval of Government Agency.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageReferralLetter" runat="server"></asp:Label>
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
                                                                <asp:Label runat="server" Visible="false" ID="lblSitePlan2"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblSitePlan" CssClass="Clicked" runat="server" Text="Site Plan"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Site Plan : (Refer: 1 (ii))"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSitePlanAttachment" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtSitePlanAttachment" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGSitePlan"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSitePlanAttachment"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGSitePlan"></asp:RequiredFieldValidator>
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
                                                                        <asp:Button ID="btnUploadSitePlan" runat="server" Text="Upload" OnClientClick="javascript:showWait();"
                                                                            OnClick="btnUploadSitePlan_Click" ValidationGroup="VGSitePlan" />
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
                                                        ShowHeaderWhenEmpty="true" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%" OnRowDeleting="gvSitePlan_RowDeleting">
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
                                                                    <asp:LinkButton runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'
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
                                                            No Records Exist in Site Plan.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageSitePlan" runat="server"></asp:Label>
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
                                                                <asp:Label runat="server" Visible="false" ID="lblLocationMap2"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblLocationMap" CssClass="Clicked" runat="server" Text="Location Map"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Location Map : (Refer: 1 (ii))"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLocationMapAttachment" runat="server" MaxLength="50"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtLocationMapAttachment" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGLocationMap"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLocationMapAttachment"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGLocationMap"></asp:RequiredFieldValidator>
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
                                                                        <asp:FileUpload ID="txtFileUploadLocationMap" runat="server" />
                                                                        <asp:Button ID="btnUploadCertifiedRevenueSketch" runat="server" Text="Upload" OnClientClick="javascript:showCertifiedRevenueSketch();"
                                                                            OnClick="btnUploadCertifiedRevenueSketch_Click" ValidationGroup="VGLocationMap" />
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
                                                    <asp:GridView ID="gvLocationMap" runat="server" CssClass="SubFormWOBG" ShowHeaderWhenEmpty="true"
                                                        AutoGenerateColumns="False" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%" OnRowDeleting="gvCertifiedRevenueSketch_RowDeleting">
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
                                                                    <asp:LinkButton runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'
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
                                                            No Records Exist in Location Map.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageLocationMap" runat="server"></asp:Label>
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
                                                                <asp:Label runat="server" Visible="false" ID="lblDocumentsOwnership2"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblDocumentsOwnership" CssClass="Clicked" runat="server" Text="Documents of Ownership / Lease"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Documents of Ownership / Lease : (Refer: 1 (v))"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDocumentsofOwnership" runat="server" MaxLength="50"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtDocumentsofOwnership" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGDocumentOfOwnerShip"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDocumentsofOwnership"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGDocumentOfOwnerShip"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelDocumentsofOwnership" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUplodDocumentsofOwnership" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="txtFileUploadDocumentsofOwnership" runat="server" />
                                                                        <asp:Button ID="btnUplodDocumentsofOwnership" runat="server" Text="Upload" OnClick="btnUplodDocumentsofOwnership_Click"
                                                                            OnClientClick="javascript:showDocumentsofOwnership();" ValidationGroup="VGDocumentOfOwnerShip" />
                                                                        <asp:UpdateProgress ID="UpdateProgressDocumentsofOwnership" runat="server" AssociatedUpdatePanelID="UpdatePanelDocumentsofOwnership">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblDocumentsofOwnershipWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                    <asp:GridView ID="gvDocumentsofOwnership" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                                        ShowHeaderWhenEmpty="true" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%" OnRowDeleting="gvDocumentsofOwnership_RowDeleting">
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
                                                                    <asp:LinkButton runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'
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
                                                            No Records Exist in Documents of Ownership / Lease.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    <asp:Label ID="lblMessageDocumentsofOwnership" runat="server"></asp:Label>
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
                                                                <asp:Label runat="server" Visible="false" ID="lblApprovalLetterofStateGovernmentAgency2"> <span class="Coumpulsory">*</span> </asp:Label>
                                                                <asp:Label ID="lblApprovalLetterofStateGovernmentAgency" CssClass="Clicked" runat="server" Text="Approval Letter of State Government Agency"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Approval Letter of State Government Agency : (Refer 9)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">Attachment Name :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtApprovalLetterName" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtApprovalLetterName" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGApprovalLetterName"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtApprovalLetterName"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGApprovalLetterName"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadApporvalLetter" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadApprovalLetter" runat="server" />
                                                                        <asp:Button ID="btnUploadApporvalLetter" runat="server" Text="Upload" OnClick="btnUploadApporvalLetter_Click"
                                                                            OnClientClick="javascript:showApprovalLetter();" ValidationGroup="VGApprovalLetterName" />
                                                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelExtra">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblApprovalLetterWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                    <asp:GridView ID="gvApprovalLetterOfStateGovtAgency" runat="server" ShowHeaderWhenEmpty="true"
                                                        CssClass="SubFormWOBG" AutoGenerateColumns="False" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%" OnRowDeleting="gvApprovalLetterOfStateGovtAgency_RowDeleting">
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
                                                                    <asp:LinkButton runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCode")) + "," + System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'
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
                                                            No Records Exist in Approval Letter of State Government Agency.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblApprovalLetterOfStateGovtAgency" runat="server"></asp:Label>
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
                            <asp:Button ID="txtSubmit" runat="server" Text="Next >>" ValidationGroup="CommunicationAddress"
                                OnClick="txtSubmit_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:MultiView ID="MainView" runat="server">
                                <asp:View ID="GroundWaterQuality" runat="server">
                                    <table class="SubFormWOBG" width="100%">
                                    </table>
                                </asp:View>
                            </asp:MultiView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>





    </table>
</asp:Content>
