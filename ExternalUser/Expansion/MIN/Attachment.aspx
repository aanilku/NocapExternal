<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="Attachment.aspx.cs"
    Inherits="ExternalUser_Expansion_MIN_Attachment" %>

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
        function showWait() {
            if (document.getElementById('<%= txtSitePlanAttachment.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadSitePlan.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressSitePlan.ClientID %>')
                x.style.display = 'inline';
            }
        }

        function showApprovedMiningPlan() {
            if (document.getElementById('<%= txtApprovedMiningPlan.ClientID %>').value.length > 0 && document.getElementById('<%= txtFileUploadApprovedMiningPlan.ClientID %>').value.length > 0) {

                var x = document.getElementById('<%= UpdateProgressApprovedMiningPlan.ClientID %>')
                x.style.display = 'inline';

            }
        }
        function WetlandAreaShowWait() {
            if (document.getElementById('<%= txtWetlandArea.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadWetlandArea.ClientID %>').value.length > 0) {

                var x = document.getElementById('<%= UpdateProgressWetlandArea.ClientID %>')
                x.style.display = 'inline';

            }
        }
        function PenaltyShowWait() {
            if (document.getElementById('<%= txtPenalty.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadPenalty.ClientID %>').value.length > 0) {

                var x = document.getElementById('<%= UpdateProgressPenalty.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showToposketch() {
            if (document.getElementById('<%= txtToposketch.ClientID %>').value.length > 0 && document.getElementById('<%= txtFileUploadToposketch.ClientID %>').value.length > 0) {

                var x = document.getElementById('<%= UpdateProgressToposketch.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showDocumentsofOwnership() {
            if (document.getElementById('<%= txtDocumentsofOwnership.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadDocumentsofOwnership.ClientID %>').value.length > 0) {

                var x = document.getElementById('<%= UpdateProgressDocumentsofOwnership.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showSourceofAvailability() {
            if (document.getElementById('<%= txtFileUploadSourceofAvailabilityofSurfaceWater.ClientID %>').value.length > 0 && document.getElementById('<%= txtFileUploadSourceofAvailabilityofSurfaceWater.ClientID %>').value.length > 0) {

                var x = document.getElementById('<%= UpdateProgressSourceofAvailabilityofSurfaceWater.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showProUtiPumpedWater() {
            if (document.getElementById('<%= txtProUtiPumpedWater.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadProUtiPumpedWater.ClientID %>').value.length > 0) {

                var x = document.getElementById('<%= UpdateProgressProUtiPumpedWater.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showGQofGWInArea() {
            if (document.getElementById('<%= txtGQofGWInArea.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadGQofGWInArea.ClientID %>').value.length > 0) {

                var x = document.getElementById('<%= UpdateProgressGQofGWInArea.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showGWLevelObservation() {
            if (document.getElementById('<%= txtGWLevelObservation.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadGWLevelObservation.ClientID %>').value.length > 0) {

                var x = document.getElementById('<%= UpdateProgressGWLevelObservation.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showGQofGW() {
            if (document.getElementById('<%= txtGQofGWInArea.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadGQofGWInArea.ClientID %>').value.length > 0) {

                var x = document.getElementById('<%= UpdateProgressGQofGWInArea.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showMonitorGWRegime() {
            if (document.getElementById('<%= txtMonitorGWRegime.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadMonitorGWRegime.ClientID %>').value.length > 0) {

                var x = document.getElementById('<%= UpdateProgressMonitorGWRegime.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showGWFlowDirectionMap() {
            if (document.getElementById('<%= txtGWFlowDirectionMap.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadGWFlowDirectionMap.ClientID %>').value.length > 0) {

                var x = document.getElementById('<%= UpdateProgressGWFlowDirectionMap.ClientID %>')
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
            if (document.getElementById('<%= ddlReferralLetter.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadReferralLetter.ClientID %>').value.length > 0 && document.getElementById('<%= txtReferralLetter.ClientID %>').value > 0) {
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

        function TORECApprovalLetterShowWait() {
            if (document.getElementById('<%= txtTORECApprovalLetterAttachment.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadTORECApprovalLetter.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressTORECApprovalLetter.ClientID %>')
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
                                            <li class="visited">Dewatering Existing Structure</li>
                                            <li class="visited">Dewatering Proposed Structure</li>
                                            <li class="visited">Utilization of pumped water</li>
                                            <li class="visited">Monitoring of groundwater regime</li>
                                            <li class="visited">Groundwater Abstraction Structure- Existing</li>
                                            <li class="visited">Groundwater Abstraction Structure- Proposed</li>
                                            <li class="visited">Other Details</li>
                                            <%-- <li class="visited">Self Declaration</li>--%>
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
                                MINING EXPANSION USE: Attachment
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right">(<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-
                            Upload Attachments in <strong>Attachment</strong> Section<br />
                            Maximum Number of Attachment Allowed- 5<br />
                            Maximum Size of each Attachment Allowed- 5MB<br />
                            Allowed file type for Attachment- txt, doc, docx, jpg, jpeg, pdf
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
                                                                <asp:Label runat="server" Visible="false" ID="lblApprovedMiningPlan2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblApprovedMiningPlan" CssClass="Clicked" runat="server" Text="Approved Mining Plan"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Approved Mining Plan : (Refer:3)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtApprovedMiningPlan" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtApprovedMiningPlan"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGApprovedMiningPlan"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtApprovedMiningPlan" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGApprovedMiningPlan"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelApprovedMiningPlan" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadApprovedMiningPlan" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="txtFileUploadApprovedMiningPlan" runat="server" />
                                                                        <asp:Button ID="btnUploadApprovedMiningPlan" runat="server" OnClientClick="javascript:showApprovedMiningPlan();"
                                                                            Text="Upload" OnClick="btnUploadApprovedMiningPlan_Click" ValidationGroup="VGApprovedMiningPlan" />
                                                                        <asp:UpdateProgress ID="UpdateProgressApprovedMiningPlan" runat="server" AssociatedUpdatePanelID="UpdatePanelApprovedMiningPlan">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblApprovedMiningPlanWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                    <asp:GridView ID="gvApprovedMiningPlan" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                                        ShowHeaderWhenEmpty="true" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%" OnRowDeleting="gvApprovedMiningPlan_RowDeleting">
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
                                                                    <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="File Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View File">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnViewApprovedMiningPlan" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnLocationMapViewFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records exist in Approved Mining Plan
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageApprovedMiningPlan" runat="server"></asp:Label>
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
                                                                <asp:Label runat="server" Visible="false" ID="lblRainWaterHarvesting2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblRainWaterHarvesting" CssClass="Clicked" runat="server" Text="Rain Water Harvesting/Artificial Recharge proposaL"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Rain Water Harvesting/Artificial Recharge proposal (Previous:Rainwater Harvesting) : (Refer: 20)"></asp:Label>-&nbsp; Max File Size - 20MB
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td valign="top">Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRainwaterHarvesting" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtRainwaterHarvesting"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGRainWaterHarvesting"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtRainwaterHarvesting"
                                                                    ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*" ErrorMessage="Allow only Alphanumeric and Characters . , _ / -"
                                                                    ValidationGroup="VGRainWaterHarvesting"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelRainwaterHarvesting" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadRainwaterHarvesting" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadRainwaterHarvesting" runat="server" />
                                                                        <asp:Button ID="btnUploadRainwaterHarvesting" runat="server" Text="Upload" OnClientClick="javascript:showRainwaterHarvesting();"
                                                                            OnClick="btnUploadRainwaterHarvesting_Click" ValidationGroup="VGRainWaterHarvesting" />
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
                                                        DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber" ShowHeaderWhenEmpty="true"
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
                                                                    <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="File Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View File">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnViewRainwaterHarvestingFile" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnRainwaterHarvestingViewFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records exist in Rain Water Harvesting/Artificial Recharge proposal.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageRainwaterHarvesting" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" id="rowIAR">
                                    <td valign="top">C.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Visible="false" ID="lblHydrogeologicalReport2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblHydrogeologicalReport" CssClass="Clicked" runat="server" Text="Comprehensive Report"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">

                                                                <asp:Label runat="server" Text="Comprehensive report on groundwater conditions in core and buffer zones of the mine by Accedited Consultants"></asp:Label>
                                                                <%--<asp:Label runat="server" Text="Hydrogeological Report (Previous:Groundwater Availability): (Refer: 19)"></asp:Label>--%>-&nbsp; Max File Size - 20MB
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
                                                                        <td valign="top">Attachment Name:
                                                                        </td>
                                                                        <td colspan="1">
                                                                            <asp:TextBox ID="txtgvGroundwaterAvailability" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtgvGroundwaterAvailability"
                                                                                Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGGroundWaterAvailability"></asp:RequiredFieldValidator>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                                                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtgvGroundwaterAvailability"
                                                                                ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*" ErrorMessage="Allow only Alphanumeric and Characters . , _ / -"
                                                                                ValidationGroup="VGGroundWaterAvailability"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>

                                                                        <td>Select Attachment File :
                                                                        </td>
                                                                        <td>
                                                                            <asp:UpdatePanel ID="UpdatePanelGroundwaterAvailability" runat="server">
                                                                                <Triggers>
                                                                                    <asp:PostBackTrigger ControlID="btnUploadGroundwaterAvailability" />
                                                                                </Triggers>
                                                                                <ContentTemplate>
                                                                                    <asp:FileUpload ID="FileUploadGroundwaterAvailability" runat="server" />
                                                                                    <asp:Button ID="btnUploadGroundwaterAvailability" runat="server" Text="Upload" OnClientClick="javascript:showGroundwaterAvailability();"
                                                                                        OnClick="btnUploadGroundwaterAvailability_Click" ValidationGroup="VGGroundWaterAvailability" />
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
                                                                    <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="File Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View File">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnViewGroundwaterAvailabilityFile" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnGroundWaterAvailabilityViewFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records exist in Hydrogeological Report.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageGroundwaterAvailability" runat="server"></asp:Label>
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
                                    <td valign="top">E.
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
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtBharatKoshReciept"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGBharatKoshRecieptAttachment">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
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
                                                                    <asp:LinkButton ID="lbtnBharatKoshRecieptViewFile" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                            No Records Exist in Bharat Kosh Reciept Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageBharatKoshReciept" runat="server"></asp:Label>
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
                                                                <asp:Label runat="server" Text="Step1:Signature and Seal document can be obtain from Preview option in New-Save As Draft on Applicant Home Page."></asp:Label>
                                                                &nbsp; or&nbsp;                                                                                                                     
                                                                <asp:LinkButton ID="lbtnPrint" runat="server" PostBackUrl="~/ExternalUser/MiningNew/Reports/MINSADReportViewer.aspx">click here</asp:LinkButton>
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
                                                    <asp:GridView ID="gvApplicationSignatureSeal" runat="server" AutoGenerateColumns="False"
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
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Attachment Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="File Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View File">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnAplicationSignatureSealViewFile" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnAplicationSignatureSealViewFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records Exist in Aplication with Signature and  Seal Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageAplicationSignatureSeal" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td valign="top">F.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td valign="top" colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblPenalty2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblPenalty" CssClass="Clicked" runat="server" Text="Penalty Attachment"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Penalty Attachment:"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPenalty" runat="server" MaxLength="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPenalty"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGPenalty"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtPenalty" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGPenalty"></asp:RegularExpressionValidator>
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
                                                                            ValidationGroup="VGPenalty" OnClientClick="javascript:PenaltyShowWait();" />
                                                                        <asp:UpdateProgress ID="UpdateProgressPenalty" runat="server" AssociatedUpdatePanelID="UpdatePanelPenalty">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblPenaltyWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                    <asp:GridView ID="gvPenalty" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                                        DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                            No Records Exist in Penalty Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        &nbsp;<asp:Label ID="lblMessagePenalty" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">F.
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
                                                            <td valign="top">Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtExtraAttachment" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtExtraAttachment"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGExtraAttachment"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtExtraAttachment" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
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

                                                                        <asp:Button ID="btnUploadExtra" runat="server" Text="Upload" OnClientClick="javascript:ExtraShowWait();"
                                                                            OnClick="btnUploadExtra_Click" ValidationGroup="VGExtraAttachment" />
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
                                                        DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber" ShowHeaderWhenEmpty="true"
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
                                                                    <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="File Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View File">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnExtraViewFile" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnExtraAttViewFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records exist in Extra Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageExtra" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td valign="top">1.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblTORApprovalLetter2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblTORApprovalLetter" CssClass="Clicked" runat="server" Text="TOR/EC/Approval Letter"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="TOR/EC/Approval Letter :"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td valign="top">Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTORECApprovalLetterAttachment" runat="server" MaxLength="50"
                                                                    Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtTORECApprovalLetterAttachment"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGTORECApprovalLetterAttachment"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtTORECApprovalLetterAttachment"
                                                                    ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*" ErrorMessage="Allow only Alphanumeric and Characters . , _ / -"
                                                                    ValidationGroup="VGTORECApprovalLetterAttachment"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelTORECApprovalLetter" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadTORECApprovalLetter" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadTORECApprovalLetter" runat="server" />
                                                                        <asp:Button ID="btnUploadTORECApprovalLetter" runat="server" Text="Upload" OnClientClick="javascript:TORECApprovalLetterShowWait();"
                                                                            ValidationGroup="VGTORECApprovalLetterAttachment" OnClick="btnUploadTORECApprovalLetter_Click" />
                                                                        <asp:UpdateProgress ID="UpdateProgressTORECApprovalLetter" runat="server" AssociatedUpdatePanelID="UpdatePanelExtra">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblTORECApprovalLetterWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                    <asp:GridView ID="gvTORECApprovalLetter" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                                        DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber" ShowHeaderWhenEmpty="true"
                                                        Width="100%" OnRowDeleting="gvTORECApprovalLetter_RowDeleting">
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
                                                                    <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="File Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View File">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnTORECApprovalLetterViewFile" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnTORECApprovalLetterAttViewFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records exist in TOR/EC/Approval Letter Attachment.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageTORECApprovalLetter" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td valign="top">3.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblGeneralQualityGW2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblGeneralQualityGW" CssClass="Clicked" runat="server" Text="General Quality of GW in Area"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="General Quality of Ground Water in the Area : (Refer: 17-f)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtGQofGWInArea" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtGQofGWInArea"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGGQOfGWInArea"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtGQofGWInArea" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGGQOfGWInArea"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelGQofGWInArea" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUplodGQofGWInArea" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadGQofGWInArea" runat="server" />
                                                                        <asp:Button ID="btnUplodGQofGWInArea" runat="server" Text="Upload" OnClientClick="javascript:showGQofGW();"
                                                                            OnClick="btnUplodGQofGWInArea_Click" ValidationGroup="VGGQOfGWInArea" />
                                                                        <asp:UpdateProgress ID="UpdateProgressGQofGWInArea" runat="server" AssociatedUpdatePanelID="UpdatePanelGQofGWInArea">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblshowGQofGWInAreaWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                    <asp:GridView ID="gvGQofGWInArea" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                                        DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber" ShowHeaderWhenEmpty="true"
                                                        Width="100%" OnRowDeleting="gvGQofGWInArea_RowDeleting">
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
                                                                    <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="File Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View File">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnViewshowGQofGWInAreaFile" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnGeneralQualityViewFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records exist in General Quality of Ground Water in the Area.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    <asp:Label ID="lblMessageGQofGWInArea" runat="server"></asp:Label>
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
                                                                <asp:Label runat="server" Visible="false" ID="lblAuthorizationLetter2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblAuthorizationLetter" CssClass="Clicked" runat="server" Text="Authorization Letter"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Authorization Letter (Previous:Authorization):"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td valign="top">Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtUndertaking" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtUndertaking"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGUndertaking"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtUndertaking" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGUndertaking"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelUndertaking" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUplodUndertaking" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadUndertaking" runat="server" />
                                                                        <asp:Button ID="btnUplodUndertaking" runat="server" Text="Upload" OnClientClick="javascript:showUndertaking();"
                                                                            OnClick="btnUplodUndertaking_Click" ValidationGroup="VGUndertaking" />
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
                                                        DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber" ShowHeaderWhenEmpty="true"
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
                                                                    <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="File Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View File">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnViewUndertakingFile" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnUnderTakingViewFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records exist in Authorization Letter.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageUndertaking" runat="server"></asp:Label>
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
                                                                <asp:Label runat="server" Text="Site Plan : (Refer 3)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSitePlanAttachment" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSitePlanAttachment"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGSitePlan"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
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
                                                    <asp:GridView ID="gvSitePlan" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                                        Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" OnRowDeleting="gvSitePlan_RowDeleting">
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
                                                                    <asp:LinkButton ID="lbtnViewFile" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                                <asp:Label runat="server" Visible="false" ID="lblToposketch2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblToposketch" CssClass="Clicked" runat="server" Text="Toposketch of Surroundings 10 km Radius Outside"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Toposketch of Surroundings 10 km Radius Outside : (Refer: 3)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtToposketch" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToposketch"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGTopoSketch"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtToposketch" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGTopoSketch"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelTopoSketch" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadToposketch" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="txtFileUploadToposketch" runat="server" />
                                                                        <asp:Button ID="btnUploadToposketch" runat="server" Text="Upload" OnClientClick="javascript:showToposketch();"
                                                                            OnClick="btnUploadToposketch_Click" ValidationGroup="VGTopoSketch" />
                                                                        <asp:UpdateProgress ID="UpdateProgressToposketch" runat="server" AssociatedUpdatePanelID="UpdatePanelToposketch">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblToposketchWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                    <asp:GridView ID="gvToposketch" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                                        DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber" ShowHeaderWhenEmpty="true"
                                                        Width="100%" OnRowDeleting="gvToposketch_RowDeleting">
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
                                                                    <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="File Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View File">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnViewToposketchFile" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnTopoSketchViewFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records exist in Toposketch of Surroundings 10 km Radius Outside
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    <asp:Label ID="lblMessageToposketch" runat="server"></asp:Label>
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
                                                                <asp:Label runat="server" Visible="false" ID="lblDocumentofOwnership2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblDocumentofOwnership" CssClass="Clicked" runat="server" Text="Document of Ownership of the land"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Document of Ownership of the land : (Refer-7)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDocumentsofOwnership" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDocumentsofOwnership"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGDocumentOfOwnership"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtDocumentsofOwnership" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGDocumentOfOwnership"></asp:RegularExpressionValidator>
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
                                                                        <asp:FileUpload ID="FileUploadDocumentsofOwnership" runat="server" />
                                                                        <asp:Button ID="btnUplodDocumentsofOwnership" runat="server" Text="Upload" OnClientClick="javascript:showDocumentsofOwnership();"
                                                                            OnClick="btnUplodDocumentsofOwnership_Click" ValidationGroup="VGDocumentOfOwnership" />
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
                                                        DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber" ShowHeaderWhenEmpty="true"
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
                                                                    <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="File Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View File">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnViewDocumentsofOwnershipFile" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnOwnershipViewFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records exist in Document of Ownership of the land.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    <asp:Label ID="lblMessageDocumentsofOwnership" runat="server"></asp:Label>
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
                                                                <asp:Label runat="server" Visible="false" ID="lblSourceofAvailability2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblSourceofAvailability" CssClass="Clicked" runat="server" Text="Source of Availability of Surface Water"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Source of Availability of Surface Water : (Refer-10)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSourceofAvailabilityofSurfaceWater" runat="server" MaxLength="50"
                                                                    Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSourceofAvailabilityofSurfaceWater"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGSourceOfAvailabilityOfSurfaceWater"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtSourceofAvailabilityofSurfaceWater" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGSourceOfAvailabilityOfSurfaceWater"></asp:RegularExpressionValidator>
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
                                                                            OnClientClick="javascript:showSourceofAvailability();" OnClick="btnUplodSourceofAvailabilityofSurfaceWater_Click"
                                                                            ValidationGroup="VGSourceOfAvailabilityOfSurfaceWater" />
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
                                                        AutoGenerateColumns="False" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%" ShowHeaderWhenEmpty="true" OnRowDeleting="gvSourceofAvailabilityofSurfaceWater_RowDeleting">
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
                                                                    <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="File Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View File">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnViewSourceofAvailabilityofSurfaceWaterFile" runat="server"
                                                                        CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnSurfaceWaterViewFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records exist in Source of Availability of Surface Water.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageSourceofAvailabilityofSurfaceWater" runat="server"></asp:Label>
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
                                                                <asp:Label runat="server" Visible="false" ID="lblGroundwaterFlowDirectionMap2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblGroundwaterFlowDirectionMap" CssClass="Clicked" runat="server" Text="Groundwater Flow Direction Map"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Groundwater Flow Direction Map : (Refer: 13-C)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td valign="top">Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtGWFlowDirectionMap" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtGWFlowDirectionMap"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGFlowFirectionMap"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtGWFlowDirectionMap" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGFlowFirectionMap"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelGWFlowDirectionMap" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUplodGWFlowDirectionMap" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadGWFlowDirectionMap" runat="server" />
                                                                        <asp:Button ID="btnUplodGWFlowDirectionMap" runat="server" Text="Upload" OnClientClick="javascript:showGWFlowDirectionMap();"
                                                                            OnClick="btnUplodGWFlowDirectionMap_Click" ValidationGroup="VGFlowFirectionMap" />
                                                                        <asp:UpdateProgress ID="UpdateProgressGWFlowDirectionMap" runat="server" AssociatedUpdatePanelID="UpdatePanelGWFlowDirectionMap">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblshowGWFlowDirectionMapWait" runat="server" BackColor="#507CD1"
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
                                                    <asp:GridView ID="gvGWFlowDirectionMap" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                                        DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber" ShowHeaderWhenEmpty="true"
                                                        Width="100%" OnRowDeleting="gvGWFlowDirectionMap_RowDeleting">
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
                                                                    <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="File Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View File">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnViewshowGWFlowDirectionMapFile" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnFlowDirectionViewFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records exist in GroundWater flow Direction Map.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageGWFlowDirectionMap" runat="server"></asp:Label>
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
                                                                <asp:Label runat="server" Visible="false" ID="lblProposedUtilization2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblProposedUtilization" CssClass="Clicked" runat="server" Text="Proposed Utilization of Pumped Water"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Proposed Utilization of Pumped Water : (Refer: 16)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtProUtiPumpedWater" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtProUtiPumpedWater"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGProUtiPumpedWater"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtProUtiPumpedWater" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGProUtiPumpedWater"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelProUtiPumpedWater" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadProUtiPumpedWater" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadProUtiPumpedWater" runat="server" />
                                                                        <asp:Button ID="btnUploadProUtiPumpedWater" runat="server" Text="Upload" OnClientClick="javascript:showProUtiPumpedWater();"
                                                                            OnClick="btnUploadProUtiPumpedWater_Click" ValidationGroup="VGProUtiPumpedWater" />
                                                                        <asp:UpdateProgress ID="UpdateProgressProUtiPumpedWater" runat="server" AssociatedUpdatePanelID="UpdatePanelProUtiPumpedWater">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblshowProUtiPumpedWaterWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                    <asp:GridView ID="gvProUtiPumpedWater" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                                        DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber" ShowHeaderWhenEmpty="true"
                                                        Width="100%" OnRowDeleting="gvProUtiPumpedWater_RowDeleting">
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
                                                                    <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="File Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View File">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnViewshowProUtiPumpedWaterFile" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnPumpedWaterViewFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records exist in Proposed Utilization of Pumped Water.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    <asp:Label ID="lblMessageProUtiPumpedWater" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td valign="top">16.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblMonitoringofGroundwater2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblMonitoringofGroundwater" CssClass="Clicked" runat="server" Text="Monitoring of Groundwater Regime Map"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Monitoring of Groundwater Regime Map : (Refer: 17)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMonitorGWRegime" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtMonitorGWRegime"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGMonitorGWRegime"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtMonitorGWRegime" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGMonitorGWRegime"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelMonitorGWRegime" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUplodMonitorGWRegime" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadMonitorGWRegime" runat="server" />
                                                                        <asp:Button ID="btnUplodMonitorGWRegime" runat="server" Text="Upload" OnClientClick="javascript:showMonitorGWRegime();"
                                                                            OnClick="btnUplodMonitorGWRegime_Click" ValidationGroup="VGMonitorGWRegime" />
                                                                        <asp:UpdateProgress ID="UpdateProgressMonitorGWRegime" runat="server" AssociatedUpdatePanelID="UpdatePanelMonitorGWRegime">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblshowMonitorGWRegimeWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                    <asp:GridView ID="gvMonitorGWRegime" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                                        DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber" ShowHeaderWhenEmpty="true"
                                                        Width="100%" OnRowDeleting="gvMonitorGWRegime_RowDeleting">
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
                                                                    <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="File Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View File">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnViewshowMonitorGWRegimeFile" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnRegimeMapViewFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records exist in Monitoring of Groundwater Regime Map.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageMonitorGWRegime" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td valign="top">17.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblGWLevelofObservation2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblGWLevelofObservation" CssClass="Clicked" runat="server" Text="GW Level of Observation Wells / Piezometer"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="GW Level of Observation Wells / Piezometer : (Refer: 17-C)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtGWLevelObservation" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtGWLevelObservation"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGGVLevelObservation"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtGWLevelObservation" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGGVLevelObservation"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelGWLevelObservation" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUplodGWLevelObservation" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadGWLevelObservation" runat="server" />
                                                                        <asp:Button ID="btnUplodGWLevelObservation" runat="server" Text="Upload" OnClientClick="javascript:showGWLevelObservation();"
                                                                            OnClick="btnUplodGWLevelObservation_Click" ValidationGroup="VGGVLevelObservation" />
                                                                        <asp:UpdateProgress ID="UpdateProgressGWLevelObservation" runat="server" AssociatedUpdatePanelID="UpdatePanelGWLevelObservation">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblshowGWLevelObservationWait" runat="server" BackColor="#507CD1"
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
                                                    <asp:GridView ID="gvGWLevelObservation" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                                        DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber" ShowHeaderWhenEmpty="true"
                                                        Width="100%" OnRowDeleting="gvGWLevelObservation_RowDeleting">
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
                                                                    <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="File Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View File">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnViewshowGWLevelObservationFile" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnObservationWellsViewFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records exist in GW Level of Observation Wells / Piezometer.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    <asp:Label ID="lblMessageGWLevelObservation" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td valign="top">18.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblCopyofReferral2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblCopyofReferral" CssClass="Clicked" runat="server" Text="Copy of Referral Letter Seeking NOC"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Copy of Referral Letter Seeking NOC : (Refer: 21)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">Referral Letter type :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlReferralLetter" runat="server" Width="200px" OnSelectedIndexChanged="ddlReferralLetter_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlReferralLetter"
                                                                    InitialValue="0" Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGReferralLetter"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtReferralLetter" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtReferralLetter"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGReferralLetter"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtReferralLetter" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGReferralLetter"></asp:RegularExpressionValidator>
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
                                                                        <asp:Button ID="btnUploadReferralLetter" runat="server" Text="Upload" OnClientClick="javascript:showReferralLetter();"
                                                                            OnClick="btnUploadReferralLetter_Click" ValidationGroup="VGReferralLetter" />
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
                                                                    <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="File Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View File">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnViewReferralLetterFile" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnRefferalLetterViewFile_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records exist in Copy of Refferal Letter Seeking NOC.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageReferralLetter" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                        <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                                        <asp:Label ID="lblMiningApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                                        <asp:Label ID="lblApplicationCode" Visible="false" runat="server" Enabled="False"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: center">
                                        <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('If you have not saved data, Your data will be lost before moving to other page ?');"
                                            OnClick="btnPrev_Click" />
                                        <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" ValidationGroup="CommunicationAddress"
                                            OnClick="btnSaveAsDraft_Click" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Next >>" ValidationGroup="CommunicationAddress"
                                            OnClick="btnSubmit_Click" />
                                        <asp:Label ID="lblFinalMsg" runat="server"></asp:Label>
                                    </td>
                                </tr>

                            </table>
                        </td>

                    </tr>


                </table>
            </td>
        </tr>
    </table>
</asp:Content>
