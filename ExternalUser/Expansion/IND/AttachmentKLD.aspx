<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true"
    MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true"
    CodeFile="AttachmentKLD.aspx.cs" Inherits="ExternalUser_Expansion_IND_AttachmentKLD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>File Upload</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js"></script>
    <style type="text/css">
        /* Custom style */
        .accordion-button::after {
            background-image: url("data:image/svg+xml,%3csvg viewBox='0 0 16 16' fill='%23333' xmlns='http://www.w3.org/2000/svg'%3e%3cpath fill-rule='evenodd' d='M8 0a1 1 0 0 1 1 1v6h6a1 1 0 1 1 0 2H9v6a1 1 0 1 1-2 0V9H1a1 1 0 0 1 0-2h6V1a1 1 0 0 1 1-1z' clip-rule='evenodd'/%3e%3c/svg%3e");
            transform: scale(.7) !important;
        }

        .accordion-button:not(.collapsed)::after {
            background-image: url("data:image/svg+xml,%3csvg viewBox='0 0 16 16' fill='%23333' xmlns='http://www.w3.org/2000/svg'%3e%3cpath fill-rule='evenodd' d='M0 8a1 1 0 0 1 1-1h14a1 1 0 1 1 0 2H1a1 1 0 0 1-1-1z' clip-rule='evenodd'/%3e%3c/svg%3e");
        }

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <script language="javascript" type="text/javascript">


        function showWait() {
            if (document.getElementById('<%= txtSitePlanAttachment.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadSitePlan.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressSitePlan.ClientID %>')
                x.style.display = 'inline';
            }
        }
        function showCertifiedRevenueSketch() {
            if (document.getElementById('<%= txtCertifiedRevenueSketchAttachment.ClientID %>').value.length > 0 && document.getElementById('<%= txtFileUploadCertifiedRevenueSketch.ClientID %>').value.length > 0) {

                var x = document.getElementById('<%= UpdateProgressCertifiedRevenueSketch.ClientID %>')
                x.style.display = 'inline';

            }
        }
        function showDocumentsofOwnership() {
            if (document.getElementById('<%= txtDocumentsofOwnership.ClientID %>').value.length > 0 && document.getElementById('<%= txtFileUploadDocumentsofOwnership.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressDocumentsofOwnership.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showSourceofAvailability() {
            if (document.getElementById('<%= txtSourceofAvailabilityofSurfaceWater.ClientID %>').value.length > 0 && document.getElementById('<%= txtFileUploadSourceofAvailabilityofSurfaceWater.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressSourceofAvailabilityofSurfaceWater.ClientID %>')
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

        function showReferralLetter() {
            if (document.getElementById('<%= txtReferralLetter.ClientID %>').value.length > 0 && document.getElementById('<%= ddlReferralLetter.ClientID %>').value > 0 && document.getElementById('<%= FileUploadReferralLetter.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressReferralLetter.ClientID %>')
                x.style.display = 'inline';
            }
        }

        function ExtraShowWait() {
            if (document.getElementById('<%= txtExtraAttachment.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadExtra.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressExtra.ClientID %>')
                x.style.display = 'inline';
            }
        }

   <%--    
        function AbstRestChargeShowWait() {
            if (document.getElementById('<%= txtAbstRestCharge.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadAbstRestCharge.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressAbstRestCharge.ClientID %>')
                x.style.display = 'inline';
            }
        }
        function RestChargeShowWait() {
            if (document.getElementById('<%= txtRestCharge.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadRestCharge.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressRestCharge.ClientID %>')
                x.style.display = 'inline';
            }
        }--%>

        function MSMEShowWait() {
            if (document.getElementById('<%= txtMSME.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadMSME.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressMSME.ClientID %>')
                x.style.display = 'inline';
            }
        }
        function WetlandAreaShowWait() {
            if (document.getElementById('<%= txtWetlandArea.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadWetlandArea.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressWetlandArea.ClientID %>')
                x.style.display = 'inline';
            }
        }
        function AffidavitNonAvaShowWait() {
            if (document.getElementById('<%= txtAffidavitNonAva.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadAffidavitNonAva.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressAffidavitNonAva.ClientID %>')
                x.style.display = 'inline';
            }
        }
       <%-- function ShowImpactReportOCS() {
            if (document.getElementById('<%= txtImpactReportOCS.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadImpactReportOCS.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressImpactReportOCS.ClientID %>')
                x.style.display = 'inline';
            }
        }--%>
        function ShowAffidavitOtherThanMSMEWait() {
            if (document.getElementById('<%= txtAffidavitOtherThanMSME.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadAffidavitOtherThanMSME.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressAffidavitOtherThanMSME.ClientID %>')
                x.style.display = 'inline';
            }
        }

        function SigneddocShowWait() {
            if (document.getElementById('<%= txtSigneddoc.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadSigneddoc.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressSigneddoc.ClientID %>')
                x.style.display = 'inline';
            }
        }

        function NonPollutingShowWait() {
            if (document.getElementById('<%=txtNonPollutingAttachment.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadNonPolluting.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressNonPolluting.ClientID %>')
                x.style.display = 'inline';
            }
        }
       
        function ConsentShowWait() {
            if (document.getElementById('<%=txtConsent.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadConsent.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressConsent.ClientID %>')
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
                                            <li class="visited">Groundwater Abstraction Structure</li>
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
                    <%-- <tr>
                        <td>
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                INDUSTRIAL EXPANSION USE: Attachment
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormProjName">
                            <b>Project Name:&nbsp;
                                <asp:Label ID="lblHeadingProjName" runat="server"></asp:Label></b>
                        </td>
                    </tr>--%>
                    <tr>
                        <td style="text-align: right">(<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-
                            Upload Attachments in <strong>Attachment</strong> Section<br />
                            Maximum Number of Attachment Allowed- 5<br />
                            Maximum Size of each Attachment Allowed- 5MB<br />
                            Allowed file type for Attachment- txt, doc, docx, jpg, jpeg, pdf
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div class="m-6">
                                <div class="accordion" id="myAccordion">
                                    <div class="accordion-item">
                                        <h2 class="accordion-header" id="headingOne">
                                            <button type="button" class="accordion-button"
                                                data-bs-toggle="collapse"
                                                data-bs-target="#collapseOne">
                                                1. Attachment</button>
                                        </h2>
                                        <div id="collapseOne" class="accordion-collapse collapse show"
                                            data-bs-parent="#myAccordion">
                                            <div class="card-body">
                                                <table class="SubFormWOBG" width="100%">
                                                    <tr>
                                                        <th align="center">SNo.
                                                        </th>
                                                        <th align="center" colspan="4">Attachment Name/ Upload File
                                                        </th>

                                                    </tr>


                                                    <tr>
                                                        <td valign="top">A.
                                                        </td>
                                                        <td colspan="4">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table class="SubFormWOBG" width="100%">
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:Label runat="server" Visible="false" ID="lblAffidavitNonAva2">
                                        <span class="Coumpulsory">*</span>
                                                                                    </asp:Label>
                                                                                    <asp:Label ID="lblAffidavitNonAva" runat="server" CssClass="Clicked" Text="Affidavit regarding Non-availability of water supply from local government agencies"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:Label runat="server" Text="Affidavit on non judicial stamp paper of Rs. 10/- regarding non availability of water supply from local government agencies"></asp:Label>
                                                                                    &nbsp;(&lt;10 KLD)</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Attachment Name:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtAffidavitNonAva" runat="server" MaxLength="50"
                                                                                        Width="200px"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAffidavitNonAva"
                                                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGAffidavitNonAva"></asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator runat="server" Display="Dynamic"
                                                                                        ForeColor="Red" ControlToValidate="txtAffidavitNonAva" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                                        ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGAffidavitNonAva"></asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Select Attachment File :
                                                                                </td>
                                                                                <td>
                                                                                    <asp:UpdatePanel ID="UpdatePanelAffidavitNonAva" runat="server">
                                                                                        <Triggers>
                                                                                            <asp:PostBackTrigger ControlID="btnUploadAffidavitNonAva" />
                                                                                        </Triggers>
                                                                                        <ContentTemplate>
                                                                                            <asp:FileUpload ID="FileUploadAffidavitNonAva" runat="server" />
                                                                                            <asp:Button ID="btnUploadAffidavitNonAva" runat="server" Text="Upload"
                                                                                                ValidationGroup="VGAffidavitNonAva"
                                                                                                OnClick="btnAffidavitNonAva_Click"
                                                                                                OnClientClick="javascript:AffidavitNonAvaShowWait();" />
                                                                                            <asp:UpdateProgress ID="UpdateProgressAffidavitNonAva" runat="server"
                                                                                                AssociatedUpdatePanelID="UpdatePanelAffidavitNonAva">
                                                                                                <ProgressTemplate>
                                                                                                    <asp:Label ID="lblAffidavitNonAvaWait" runat="server" BackColor="#507CD1"
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
                                                                        <asp:GridView ID="gvAffidavitNonAva" runat="server" ShowHeaderWhenEmpty="true"
                                                                            CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                                                            DataKeyNames="IndustrialNewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                                            Width="100%"
                                                                            OnRowDeleting="gvAffidavitNonAva_RowDeleting">
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
                                                                                            CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                                                No Records Exist in Affidavit Non-availability of water.
                                                                            </EmptyDataTemplate>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            &nbsp;<asp:Label ID="lblmessageAffidavitNonAva" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">B.
                                                        </td>
                                                        <td colspan="4">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table class="SubFormWOBG" width="100%">
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:Label runat="server" Visible="false" ID="lblSourceWaterAvailability2">
                                        <span class="Coumpulsory">*</span>
                                                                                    </asp:Label>
                                                                                    <asp:Label ID="lblSourceWaterAvailability" runat="server" CssClass="Clicked" Text="Source Water Availability/Non-availability Certificate"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:Label ID="Label2" runat="server" Text="Certificate regarding non/ partial availability of fresh water/ treated waste water supply from the local government water supply agency:(Refer: 1 (vii))"></asp:Label>
                                                                                    &nbsp;(&gt;10 KLD)</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Attachment Name:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtSourceofAvailabilityofSurfaceWater" runat="server" MaxLength="50"
                                                                                        Width="200px"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSourceofAvailabilityofSurfaceWater"
                                                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGSourceOfAvailabilityOfSurface"></asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic"
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
                                                                            DataKeyNames="IndustrialNewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                                                            CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                    <tr>
                                                        <td valign="top">C.
                                                        </td>
                                                        <td colspan="4" valign="top">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table class="SubFormWOBG" width="100%">
                                                                            <tr>
                                                                                <td valign="top" colspan="2">
                                                                                    <asp:Label runat="server" Visible="false" ID="lblGroundWaterQualityReport2">
                                        <span class="Coumpulsory">*</span>
                                                                                    </asp:Label>
                                                                                    <asp:Label ID="lblGroundWaterQualityReport" CssClass="Clicked" runat="server" Text="Ground Water Quality Report"></asp:Label>
                                                                                    <%--Non-Polluting Effluent Attachment:--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:Label ID="Label5" runat="server" Text="Ground water quality data of existing bore well/ tube well/ dug well from any NABL accredited laboratory or Govt. approved laboratory (in case of existing projects applying for NOC): (Refer: 3(a))"></asp:Label>
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
                                                                                    <asp:UpdatePanel ID="UpdatePanelNonPolluting" runat="server">
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
                                                                                    </asp:UpdatePanel>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="gvNonPolluting" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                                                            DataKeyNames="IndustrialNewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                                            ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvNonPolluting_RowDeleting">
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
                                                                                        <asp:LinkButton runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                                                No Records Exist in Ground Water Quality Report.
                                                                            </EmptyDataTemplate>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            &nbsp;<asp:Label ID="lblMessageNonPolluting" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" visible="false">
                                                        <td valign="top">D.
                                                        </td>
                                                        <td colspan="4" valign="top">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table class="SubFormWOBG" width="100%">
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:Label runat="server" Visible="false" ID="lblRainWaterHarvesting2">
                                        <span class="Coumpulsory">*</span>
                                                                                    </asp:Label>
                                                                                    <asp:Label ID="lblRainWaterHarvesting" CssClass="Clicked" runat="server" Text="Rain Water Harvesting/Artificial Recharge proposal"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:Label ID="Label3" runat="server" Text="Proposal for rain water harvesting/ recharge within the premises as per Model Building Bye Laws: (Refer: 5)"></asp:Label>
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
                                                                    <td>
                                                                        <asp:GridView ID="gvRainwaterHarvesting" runat="server" AutoGenerateColumns="False"
                                                                            CssClass="SubFormWOBG" DataKeyNames="IndustrialNewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                                                        <asp:LinkButton runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                                                No Records Exist in Rain Water Harvesting/Artificial Recharge proposal.
                                                                            </EmptyDataTemplate>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            &nbsp;<asp:Label ID="lblMessageRainwaterHarvesting" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                  
                                                    <tr runat="server" visible="false">
                                                        <td valign="top">D.
                                                        </td>
                                                        <td colspan="4" valign="top">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table class="SubFormWOBG" width="100%">
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:Label runat="server" Visible="false" ID="lblConsentApproval2">
                                        <span class="Coumpulsory">*</span>
                                                                                    </asp:Label>
                                                                                    <asp:Label ID="lblConsentApproval" runat="server" CssClass="Clicked" Text="Consent/ Approval of Government Agency"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:Label ID="Label1" runat="server" Text="Consent to Establish in case of Over Exploited Category: (Refer: 6)"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Referral Letter type :
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlReferralLetter" runat="server" Width="200px" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="ddlReferralLetter_SelectedIndexChanged">
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
                                                                                    <asp:UpdatePanel ID="UpdatePanelReferralLetter" runat="server">
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
                                                                                    </asp:UpdatePanel>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="gvReferralLetterAttachment" runat="server" ShowHeaderWhenEmpty="true"
                                                                            CssClass="SubFormWOBG" AutoGenerateColumns="False" DataKeyNames="IndustrialNewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                                            Width="100%" OnRowDeleting="gvReferralLetterAttachment_RowDeleting">
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
                                                                                        <asp:LinkButton ID="lbtnViewReferralLetterFile" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                                            OnCommand="lbtnViewReferralLetterFile_Click">View</asp:LinkButton>
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
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            &nbsp;<asp:Label ID="lblMessageReferralLetter" runat="server"></asp:Label>
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
                                                                                    <asp:Label runat="server" Visible="false" ID="lblConsent2">
                                        <span class="Coumpulsory">*</span>
                                                                                    </asp:Label>
                                                                                    <asp:Label ID="lblConsent" runat="server" CssClass="Clicked" Text="Consent to Establish in case of Over Exploited Category"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:Label runat="server" Text="Consent to Establish in case of Over Exploited Category: (Refer: 6)"></asp:Label>
                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td>Attachment Name:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtConsent" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtConsent"
                                                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGConsent"></asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator runat="server" Display="Dynamic"
                                                                                        ForeColor="Red" ControlToValidate="txtReferralLetter" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                                        ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGConsent"></asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Select Attachment File :
                                                                                </td>
                                                                                <td>
                                                                                    <asp:UpdatePanel ID="UpdatePanelConsent" runat="server">
                                                                                        <Triggers>
                                                                                            <asp:PostBackTrigger ControlID="btnUploadConsent" />
                                                                                        </Triggers>
                                                                                        <ContentTemplate>
                                                                                            <asp:FileUpload ID="FileUploadConsent" runat="server" />
                                                                                            <asp:Button ID="btnUploadConsent" runat="server" Text="Upload" OnClick="btnUploadConsent_Click"
                                                                                                OnClientClick="javascript:ConsentShowWait();" ValidationGroup="VGConsent" />
                                                                                            <asp:UpdateProgress ID="UpdateProgressConsent" runat="server" AssociatedUpdatePanelID="UpdatePanelConsent">
                                                                                                <ProgressTemplate>
                                                                                                    <asp:Label ID="lblConsentWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                                        <asp:GridView ID="gvConsent" runat="server" ShowHeaderWhenEmpty="true"
                                                                            CssClass="SubFormWOBG" AutoGenerateColumns="False" DataKeyNames="IndustrialNewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                                            Width="100%" OnRowDeleting="gvConsent_RowDeleting">
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
                                                                                        <asp:LinkButton runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            &nbsp;<asp:Label ID="lblMessageConsent" runat="server"></asp:Label>
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
                                                                                <td valign="top" colspan="2">
                                                                                    <asp:Label runat="server" Visible="false" ID="lblAuditReport2">
                                        <span class="Coumpulsory">*</span>
                                                                                    </asp:Label>
                                                                                    <asp:Label ID="lblAuditReport" CssClass="Clicked" runat="server" Text="Water audit report by cerified auditors of NPC/CII/FICCI(in case of renewal only)"></asp:Label>
                                                                                </td>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label runat="server" Text="Water audit reports by certified auditors of NPC/CII/FICCI ( in case of renewal Only)"></asp:Label></td>
                                                                                </tr>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            &nbsp;<asp:Label ID="lblMessageAuditReport" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server">
                                                        <td valign="top">E.
                                                        </td>
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
                                                                                <td colspan="3">
                                                                                    <asp:Label runat="server" Text="MSME certificate in case of MSME:"></asp:Label>
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
                                                                            CssClass="SubFormWOBG" DataKeyNames="IndustrialNewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                                                        <asp:LinkButton runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                        </td>
                                                    </tr>
                                                    <tr runat="server">
                                                        <td valign="top">F.
                                                        </td>
                                                        <td colspan="4" valign="top">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table class="SubFormWOBG" width="100%">
                                                                            <tr>
                                                                                <td valign="top" colspan="2">
                                                                                    <asp:Label runat="server" Visible="false" ID="lblAffidavitOtherThanMSME2">
                                        <span class="Coumpulsory">*</span>
                                                                                    </asp:Label>
                                                                                    <asp:Label ID="lblAffidavitOtherThanMSME" CssClass="Clicked" runat="server" Text="Affidavit in case of drinking/domestic/green belt (OE areas) for industries other than MSME"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:Label runat="server" Text="Affidavit in case of drinking/domestic/green belt (OE areas) for industries other than MSME:"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Attachment Name :
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtAffidavitOtherThanMSME" runat="server" MaxLength="50"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAffidavitOtherThanMSME"
                                                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGAffidavitOtherThanMSME">
                                                                                    </asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator runat="server"
                                                                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtAffidavitOtherThanMSME" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                                        ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGAffidavitOtherThanMSME"></asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Select Attachment File :
                                                                                </td>
                                                                                <td>
                                                                                    <asp:UpdatePanel ID="UpdatePanelAffidavitOtherThanMSME" runat="server">
                                                                                        <Triggers>
                                                                                            <asp:PostBackTrigger ControlID="btnUploadAffidavitOtherThanMSME" />
                                                                                        </Triggers>
                                                                                        <ContentTemplate>
                                                                                            <asp:FileUpload ID="FileUploadAffidavitOtherThanMSME" runat="server" />
                                                                                            <asp:Button ID="btnUploadAffidavitOtherThanMSME" runat="server" Text="Upload"
                                                                                                OnClick="btnUploadAffidavitOtherThanMSME_Click"
                                                                                                ValidationGroup="VGAffidavitOtherThanMSME" OnClientClick="javascript:ShowAffidavitOtherThanMSMEWait();" />
                                                                                            <asp:UpdateProgress ID="UpdateProgressAffidavitOtherThanMSME" runat="server" AssociatedUpdatePanelID="UpdatePanelAffidavitOtherThanMSME">
                                                                                                <ProgressTemplate>
                                                                                                    <asp:Label ID="lblAffidavitOtherThanMSMEWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                                        <asp:GridView ID="gvAffidavitOtherThanMSME" runat="server" AutoGenerateColumns="False"
                                                                            CssClass="SubFormWOBG" DataKeyNames="IndustrialNewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                                            ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvAffidavitOtherThanMSME_RowDeleting">
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
                                                                                        <asp:LinkButton runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                            &nbsp;<asp:Label runat="server" ID="lblMessageAffidavitOtherThanMSME"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server">
                                                        <td valign="top">G.
                                                        </td>
                                                        <td colspan="4" valign="top">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table class="SubFormWOBG" width="100%">
                                                                            <tr>
                                                                                <td valign="top" colspan="2">
                                                                                    <asp:Label runat="server" Visible="false" ID="lblWetlandArea2">
                                        <span class="Coumpulsory">*</span>
                                                                                    </asp:Label>
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
                                                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGWetlandAreaAttachment">
                                                                                    </asp:RequiredFieldValidator>
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
                                                                            CssClass="SubFormWOBG" DataKeyNames="IndustrialNewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                                                        <asp:LinkButton runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                    
                                                
                                                    <tr runat="server">
                                                        <td valign="top">H.
                                                        </td>
                                                        <td colspan="4" valign="top">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table class="SubFormWOBG" width="100%">
                                                                            <tr>
                                                                                <td valign="top" colspan="2">
                                                                                    <asp:Label runat="server" Visible="false" ID="lblSigneddoc2">
                                                                   <span class="Coumpulsory">*</span>
                                                                                    </asp:Label>
                                                                                    <asp:Label ID="lblSigneddoc" CssClass="Clicked" runat="server" Text="Aplication with Signature and Seal"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:Label ID="Label9" runat="server" Text="Scanned copy of signature and seal document:"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:Label runat="server" Text="Step1:Signature and Seal document can be obtain from Preview option in New-Save As Draft on Applicant Home Page"></asp:Label>
                                                                                    &nbsp; or&nbsp;                                                                                                                        
                                                                <asp:LinkButton ID="lbtnPrint" runat="server" PostBackUrl="~/ExternalUser/IndustrialNew/Reports/INDSADReportViewer.aspx">click here</asp:LinkButton>
                                                                                    &nbsp; &nbsp;<br />
                                                                                    Step2:Scanned copy of page after signature and seal on printed page should be attached here before submission of application.
                                              

                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td>Attachment Name :
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtSigneddoc" runat="server" MaxLength="50"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtSigneddoc"
                                                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGSigneddocAttachment">
                                                                                    </asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                                                                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtSigneddoc" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                                        ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGSigneddocAttachment"></asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Select Attachment File :
                                                                                </td>
                                                                                <td>
                                                                                    <asp:UpdatePanel ID="UpdatePanelSigneddoc" runat="server">
                                                                                        <Triggers>
                                                                                            <asp:PostBackTrigger ControlID="btnUploadSigneddoc" />
                                                                                        </Triggers>
                                                                                        <ContentTemplate>
                                                                                            <asp:FileUpload ID="FileUploadSigneddoc" runat="server" />
                                                                                            <asp:Button ID="btnUploadSigneddoc" runat="server" Text="Upload" OnClick="btnUploadSigneddoc_Click"
                                                                                                ValidationGroup="VGSigneddocAttachment" OnClientClick="javascript:SigneddocShowWait();" />
                                                                                            <asp:UpdateProgress ID="UpdateProgressSigneddoc" runat="server" AssociatedUpdatePanelID="UpdatePanelSigneddoc">
                                                                                                <ProgressTemplate>
                                                                                                    <asp:Label ID="lblSigneddocWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                                        <asp:GridView ID="gvSigneddoc" runat="server" AutoGenerateColumns="False"
                                                                            CssClass="SubFormWOBG" DataKeyNames="IndustrialNewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                                            ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvSigneddoc_RowDeleting">
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
                                                                                        <asp:LinkButton runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                                                No Records Exist in Sign and Signature Attachment.
                                                                            </EmptyDataTemplate>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>

                                                            </table>
                                                            &nbsp;<asp:Label ID="lblMessageSigneddoc" runat="server"></asp:Label>
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
                                                                                <td valign="top" colspan="2">
                                                                                    <asp:Label runat="server" Visible="false" ID="lblExtraAttachment2">
                                        <span class="Coumpulsory">*</span>
                                                                                    </asp:Label>
                                                                                    <asp:Label ID="lblExtraAttachment" CssClass="Clicked" runat="server" Text="Extra Attachment"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:Label ID="Label7" runat="server" Text="Extra Attachment:"></asp:Label>
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
                                                                                                    <%--<img alt="progress" src="../../Images/loading.gif"/>--%>
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
                                                                        <asp:GridView ID="gvExtra" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                                                            DataKeyNames="IndustrialNewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                                                        <asp:LinkButton runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            &nbsp;<asp:Label ID="lblMessageExtra" runat="server"></asp:Label>
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
                                                                                <td valign="top" colspan="2">
                                                                                    <asp:Label runat="server" Visible="false" ID="lblHydrogeologicalReport2">
                                        <span class="Coumpulsory">*</span>
                                                                                    </asp:Label>
                                                                                    <asp:Label ID="lblHydrogeologicalReport" CssClass="Clicked" runat="server" Text="Hydrogeological Report"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:Label ID="Label4" runat="server" Text="Hydrogeological Report (Previous:Groundwater Availability): (Refer: 4)"></asp:Label>
                                                                                    <%--Groundwater Availability Report : (Refer: 4)--%>
                                                                &nbsp;Max File Size - 20MB
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
                                                                    <td>
                                                                        <asp:GridView ID="gvGroundwaterAvailability" runat="server" ShowHeaderWhenEmpty="true"
                                                                            AutoGenerateColumns="False" DataKeyNames="IndustrialNewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                                                        <asp:LinkButton runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                                                No Records Exist in Hydrogeological Report.
                                                                            </EmptyDataTemplate>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            &nbsp;<asp:Label ID="lblMessageGroundwaterAvailability" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" visible="false">
                                                        <td valign="top">6.
                                                        </td>
                                                        <td colspan="4" valign="top">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table class="SubFormWOBG" width="100%">
                                                                            <tr>
                                                                                <td valign="top" colspan="2">
                                                                                    <asp:Label runat="server" Visible="false" ID="lblAuthorizationLetter2">
                                        <span class="Coumpulsory">*</span>
                                                                                    </asp:Label>
                                                                                    <asp:Label ID="lblAuthorizationLetter" CssClass="Clicked" runat="server" Text="Authorization Letter"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:Label ID="Label6" runat="server" Text="Authorization Letter (Previous:Authorization):"></asp:Label>
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
                                                                    <td>
                                                                        <asp:GridView ID="gvUndertaking" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                                                            DataKeyNames="IndustrialNewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                                                        <asp:LinkButton runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            &nbsp;<asp:Label ID="lblMessageUndertaking" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" visible="false">
                                                        <td valign="top" style="border: solid .1px">10.
                                                        </td>
                                                        <td valign="top" style="border: solid .1px">
                                                            <asp:Label runat="server" Visible="false" ID="lblSitePlanwithLocationMap2">
                                        <span class="Coumpulsory">*</span>
                                                            </asp:Label>
                                                            <asp:Label ID="lblSitePlanwithLocationMap" CssClass="Clicked" runat="server" Text="Site Plan with Location Map (Previous:Site Plan) : (Refer: 1 (ii)):"></asp:Label>
                                                        </td>
                                                        <td colspan="3" valign="top" style="border: solid .1px">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>Attachment Name :
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtSitePlanAttachment" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtSitePlanAttachment"
                                                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGSitePlan"></asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
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
                                                                    <td>
                                                                        <asp:GridView ID="gvSitePlan" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                                                            DataKeyNames="IndustrialNewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                                                        <asp:LinkButton runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:Label ID="lblMessageSitePlan" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr1" runat="server" visible="false">
                                                        <td valign="top" style="border: solid .1px">11.
                                                        </td>
                                                        <td valign="top" style="border: solid .1px">
                                                            <asp:Label ID="lblCertifiedRevenueSketch" CssClass="Clicked" runat="server" Text="Certified Revenue Sketch : (Refer: 1 (ii)):"></asp:Label>
                                                        </td>
                                                        <td colspan="3" valign="top" style="border: solid .1px">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <table>
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
                                                                    <td>
                                                                        <asp:GridView ID="gvCertifiedRevenueSketch" runat="server" ShowHeaderWhenEmpty="true"
                                                                            CssClass="SubFormWOBG" AutoGenerateColumns="False" DataKeyNames="IndustrialNewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                                                        <asp:LinkButton runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                                                No Records Exist in Certified Revenue Sketch.
                                                                            </EmptyDataTemplate>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:Label ID="lblMessageCertifiedRevenueSketch" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr2" runat="server" visible="false">
                                                        <td valign="top" style="border: solid .1px">12.
                                                        </td>
                                                        <td valign="top" style="border: solid .1px">
                                                            <asp:Label ID="lblDocumentsofOwnership" CssClass="Clicked" runat="server" Text="Documents of Ownership / Lease : (Refer: 1 (v)):"></asp:Label>
                                                        </td>
                                                        <td colspan="3" valign="top" style="border: solid .1px">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td valign="top">Attachment Name:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDocumentsofOwnership" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDocumentsofOwnership"
                                                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGDocumentofOwnership"></asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                                                                                        ForeColor="Red" ControlToValidate="txtDocumentsofOwnership" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                                        ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGDocumentofOwnership"></asp:RegularExpressionValidator>
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
                                                                                                OnClientClick="javascript:showDocumentsofOwnership();" ValidationGroup="VGDocumentofOwnership" />
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
                                                                    <td>
                                                                        <asp:GridView ID="gvDocumentsofOwnership" runat="server" AutoGenerateColumns="False"
                                                                            CssClass="SubFormWOBG" DataKeyNames="IndustrialNewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                                            ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvDocumentsofOwnership_RowDeleting">
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
                                                                                        <asp:LinkButton runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            &nbsp;<asp:Label ID="lblMessageDocumentsofOwnership" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr3" runat="server" visible="false">
                                                        <td valign="top" style="border: solid .1px">13.
                                                        </td>
                                                        <td valign="top" style="border: solid .1px">
                                                            <asp:Label ID="lblWaterBalanceFlowChart" CssClass="Clicked" runat="server" Text="Water Balance Flow Chart (Previous:Water Requirement): (Refer: 2):"></asp:Label>
                                                        </td>
                                                        <td colspan="3" valign="top" style="border: solid .1px">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <table>
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
                                                                    <td>
                                                                        <asp:GridView ID="gvWaterRequirement" runat="server" AutoGenerateColumns="False"
                                                                            CssClass="SubFormWOBG" DataKeyNames="IndustrialNewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                                                        <asp:LinkButton runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("IndustrialNewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            &nbsp;<asp:Label ID="lblMessageWaterRequirement" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--here--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblIndustialApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblFinalMsg" runat="server"></asp:Label>
                            <asp:Label ID="lblApplicationCode" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblApplicationCodeForPayment" runat="server" Visible="false"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev"
                                OnClientClick="return confirm('If you have not saved data, Your data will be lost before moving to other page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" ValidationGroup="CommunicationAddress" />
                            <asp:Button ID="btnSubmit" runat="server" Text="Next >>" ValidationGroup="CommunicationAddress"
                                OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>
