<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="Attachment.aspx.cs"
    Inherits="ExternalUser_MiningRenew_Attachment" %>

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
    <script language="javascript" type="text/javascript">
        function showGWFlowDirectionMap() {
            if (document.getElementById('<%= txtGWFlowDirectionMap.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadGWFlowDirectionMap.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressGWFlowDirectionMap.ClientID %>')
                x.style.display = 'inline';
            }
        }

        function showGWLevelObservation() {
            if (document.getElementById('<%= txtGWLevelObservation.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadGWLevelObservation.ClientID %>').value.length > 0) {

                var x = document.getElementById('<%= UpdateProgressGWLevelObservation.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showGQofGWInArea() {
            if (document.getElementById('<%= txtGQofGWInArea.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadGQofGWInArea.ClientID %>').value.length > 0) {

                var x = document.getElementById('<%= UpdateProgressGQofGWInArea.ClientID %>')
                x.style.display = 'inline';

            }
        }

        function showWaitChangesInTopography() {
            if (document.getElementById('<%= txtChangesInTopographyAttachment.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadChangesInTopography.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressChangesInTopography.ClientID %>')
                x.style.display = 'inline';
            }
        }

        function showChangesInDrainage() {
            if (document.getElementById('<%= txtChangesInDrainage.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadChangesInDrainage.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressChangesInDrainage.ClientID %>')
                x.style.display = 'inline';
            }
        }

        function ExtraShowWait() {
            if (document.getElementById('<%= txtExtraAttachment.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadExtra.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressExtra.ClientID %>')
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
        function BharatKoshRecieptShowWait() {
            if (document.getElementById('<%= txtBharatKoshReciept.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadBharatKoshReciept.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressBharatKoshReciept.ClientID %>')
                x.style.display = 'inline';
            }
        }
        function ApplicationSignatureSealShowWait() {
            if (document.getElementById('<%= txtApplicationSignatureSeal.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadApplicationSignatureSeal.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressApplicationSignatureSeal.ClientID %>')
                x.style.display = 'inline';
            }
        }

        function AffidavitShowWait() {
            if (document.getElementById('<%= txtAffidavit.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadAffidavit.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressAffidavit.ClientID %>')
                x.style.display = 'inline';
            }
        }
        function WaterAuditReportShowWait() {
            if (document.getElementById('<%= txtWaterAuditReport.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadWaterAuditReport.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressWaterAuditReport.ClientID %>')
                x.style.display = 'inline';
            }
        }
        function showSourceofAvailability() {
            if (document.getElementById('<%= txtSourceofAvailabilityofSurfaceWater.ClientID %>').value.length > 0 && document.getElementById('<%= txtFileUploadSourceofAvailabilityofSurfaceWater.ClientID %>').value.length > 0) {
                  var x = document.getElementById('<%= UpdateProgressSourceofAvailabilityofSurfaceWater.ClientID %>')
                x.style.display = 'inline';
            }
        }
        function ShowWaterQualityMinSeep() {
            if (document.getElementById('<%= txtWaterQualityMinSeep.ClientID %>').value.length > 0 && document.getElementById('<%= FileUploadWaterQualityMinSeep.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressWaterQualityMinSeep.ClientID %>')
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
                                RENEW - MINING USE: Attachment
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td class="FormProjName">
                                        <b>Project Name:&nbsp;
                                            <asp:Label ID="lblHeadingProjName" runat="server"></asp:Label></b>
                                    </td>
                                </tr>
                            </table>
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
                                                                <asp:Label runat="server" Visible="false" ID="lblGroundwaterFlowDirectionMap2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblGroundwaterFlowDirectionMap" CssClass="Clicked" runat="server" Text="Groundwater Flow Direction Map"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Groundwater Flow Direction Map : (Refer: 11-C)"></asp:Label>
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
                                                        DataKeyNames="MiningRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvGWFlowDirectionMap_RowDeleting">
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
                                                                    <asp:LinkButton ID="lbtnViewshowGWFlowDirectionMapFile" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                                <asp:Label runat="server" Visible="false" ID="lblGWLevelofObservation2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblGWLevelofObservation" CssClass="Clicked" runat="server" Text="GW Level of Observation Wells / Piezometer"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="GW Level of Observation Wells / Piezometer : (Refer: 15-(c))"></asp:Label>
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
                                                        DataKeyNames="MiningRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvGWLevelObservation_RowDeleting">
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
                                                                    <asp:LinkButton ID="lbtnViewshowGWLevelObservationFile" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                                <asp:Label runat="server" Visible="false" ID="lblGeneralQuality2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblGeneralQuality" CssClass="Clicked" runat="server" Text="General Quality of Ground Water in the Area"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="General Quality of Ground Water in the Area : (Refer: 17- (d))"></asp:Label>
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
                                                                        <asp:Button ID="btnUplodGQofGWInArea" runat="server" Text="Upload" OnClientClick="javascript:showGQofGWInArea();"
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
                                                        DataKeyNames="MiningRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvGQofGWInArea_RowDeleting">
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
                                                                    <asp:LinkButton ID="lbtnViewshowGQofGWInAreaFile" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                                <asp:Label runat="server" Visible="false" ID="lblChangesTopography2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblChangesTopography" CssClass="Clicked" runat="server" Text="Changes in Topography"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Changes in Topography: (Refer: 8)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtChangesInTopographyAttachment" runat="server" MaxLength="50"
                                                                    Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtChangesInTopographyAttachment"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGSitePlan"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtChangesInTopographyAttachment"
                                                                    ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*" ErrorMessage="Allow only Alphanumeric and Characters . , _ / -"
                                                                    ValidationGroup="VGSitePlan"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelChangesInTopography" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadChangesInTopography" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadChangesInTopography" runat="server" />
                                                                        <asp:Button ID="btnUploadChangesInTopography" runat="server" Text="Upload" OnClientClick="javascript:showWaitChangesInTopography();"
                                                                            OnClick="btnUploadChangesInTopography_Click" ValidationGroup="VGSitePlan" />
                                                                        <asp:UpdateProgress ID="UpdateProgressChangesInTopography" runat="server" AssociatedUpdatePanelID="UpdatePanelChangesInTopography">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblChangesInTopographyWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                    <asp:GridView ID="gvChangesInTopography" runat="server" AutoGenerateColumns="False"
                                                        CssClass="SubFormWOBG" Width="100%" DataKeyNames="MiningRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" OnRowDeleting="gvChangesInTopography_RowDeleting">
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
                                                                    <asp:LinkButton ID="lbtnViewFile" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                            No Records exist in Changes in Topography.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    <asp:Label ID="lblMessageChangesInTopography" runat="server"></asp:Label>
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
                                                                <asp:Label runat="server" Visible="false" ID="lblChangesDrainage2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblChangesDrainage" CssClass="Clicked" runat="server" Text="Changes in Drainage"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Changes in Drainage: (Refer: 9)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtChangesInDrainage" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtChangesInDrainage"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGApprovedMiningPlan"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtChangesInDrainage" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGApprovedMiningPlan"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelChangesInDrainage" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadChangesInDrainage" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadChangesInDrainage" runat="server" />
                                                                        <asp:Button ID="btnUploadChangesInDrainage" runat="server" OnClientClick="javascript:showChangesInDrainage();"
                                                                            Text="Upload" OnClick="btnUploadChangesInDrainage_Click" ValidationGroup="VGApprovedMiningPlan" />
                                                                        <asp:UpdateProgress ID="UpdateProgressChangesInDrainage" runat="server" AssociatedUpdatePanelID="UpdatePanelChangesInDrainage">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblChangesInDrainageWait" runat="server" BackColor="#507CD1" Font-Bold="True"
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
                                                    <asp:GridView ID="gvChangesInDrainage" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                                        ShowHeaderWhenEmpty="true" DataKeyNames="MiningRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%" OnRowDeleting="gvChangesInDrainage_RowDeleting">
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
                                                                    <asp:LinkButton ID="lbtnViewChangesInDrainage" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                        OnCommand="lbtnChangesInDrainage_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Records exist in Changes In Drainage
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    &nbsp;<asp:Label ID="lblMessageChangesInDrainage" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
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
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblReasonforNotApplying2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblReasonforNotApplying" CssClass="Clicked" runat="server" Text="Reason for Not Applying for Renewal before Expiring NOC"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Reason for Not Applying for Renewal before Expiring NOC : (Refer: 5)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtReasonForNotApplyingBefore" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtReasonForNotApplyingBefore"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGDocumentofOwnership"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtReasonForNotApplyingBefore"
                                                                    ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*" ErrorMessage="Allow only Alphanumeric and Characters . , _ / -"
                                                                    ValidationGroup="VGDocumentofOwnership"></asp:RegularExpressionValidator>
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
                                                        CssClass="SubFormWOBG" DataKeyNames="MiningRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                                    <asp:LinkButton ID="lbtnViewReasonForNotApplyingBeforeFile" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                                <asp:Label runat="server" Visible="false" ID="lblExistingNOC2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblExistingNOC" CssClass="Clicked" runat="server" Text="Existing NOC"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Existing NOC : (Refer: 5)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtExistingNOC" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtExistingNOC"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGExistingNOC"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                                                                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtExistingNOC" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
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
                                                        AutoGenerateColumns="False" DataKeyNames="MiningRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                                    <asp:LinkButton ID="lbtnViewExistingNOCFile" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                    <td valign="top">G.
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
                                                                <asp:Label runat="server" Text="Compliance Condition NOC: (Refer: 17-(a))"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:GridView ID="gvMINRenewComplianceConditionNOC" runat="server" AutoGenerateColumns="False"
                                                                    CssClass="SubFormWOBG" ShowHeaderWhenEmpty="true" Width="100%" OnRowDataBound="gvMINRenewComplianceConditionNOC_RowDataBound"
                                                                    OnRowCommand="gvMINRenewComplianceConditionNOC_RowCommand">
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
                                                                                <asp:Label ID="lblMessageMINCompCondAttachment" runat="server" Text=""></asp:Label>
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
                                                                                                <asp:LinkButton ID="lnkMiningCompCondNOCAttachmentView" OnCommand="lnkMiningCompCondNOCAttachmentView_Click"
                                                                                                    runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                                                    CommandName="View">View</asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                                                    CommandName="DeleteFile" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <EmptyDataTemplate>
                                                                                        No Records Exist in Compliance Condition NOC Attachment.
                                                                                    </EmptyDataTemplate>
                                                                                </asp:GridView>
                                                                                <asp:Label ID="lblMessageMINCompCondNOCAttachmentDelete" runat="server" Text="">
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
                                    <td valign="top">9.
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
                                                                <asp:Label ID="lblComplianceReportOther" CssClass="Clicked" runat="server" Text="Compliance Report-Other"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Compliance Condition NOC - Other: (Refer: 17-(b))"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:GridView ID="gvMINRenewComplianceConditionNOCOther" runat="server" AutoGenerateColumns="False"
                                                                    CssClass="SubFormWOBG" ShowHeaderWhenEmpty="true" Width="100%" OnRowCommand="gvMINRenewComplianceConditionNOCOther_RowCommand">
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
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtAttachmentNameCompCondNOCOther"
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
                                                                                <asp:Label ID="lblMessageMINCompCondAttachmentOther" runat="server" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Attachments">
                                                                            <ItemTemplate>
                                                                                <asp:GridView ID="gvCompCondNOCAttachmentOther" runat="server" AutoGenerateColumns="False"
                                                                                    CssClass="SubFormWOBG" ShowHeaderWhenEmpty="true" Width="100%" OnRowCommand="gvCompCondNOCAttachmentOther_RowCommand">
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
                                                                                                <asp:LinkButton ID="lnkMiningCompCondNOCAttachmentViewOther" OnCommand="lnkMiningCompCondNOCAttachmentViewOther_Click"
                                                                                                    runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                                                    CommandName="View">View</asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                                                                    CommandName="DeleteFile" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <EmptyDataTemplate>
                                                                                        No Records Exist in Compliance Condition NOC - Other Attachment.
                                                                                    </EmptyDataTemplate>
                                                                                </asp:GridView>
                                                                                <asp:Label ID="lblMessageMINCompCondNOCAttachmentOtherDelete" runat="server" Text="">
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
                                                        DataKeyNames="MiningRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDeleting="gvExtra_RowDeleting">
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
                                                                    <asp:LinkButton ID="lbtnExtraViewFile" runat="server" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                    <td valign="top">I.
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
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBharatKoshReciept"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGBharatKoshRecieptAttachment">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
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
                                                        CssClass="SubFormWOBG" DataKeyNames="MiningRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                                    <asp:LinkButton ID="lbtnBharatKoshRecieptViewFile" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                    <td valign="top">I.
                                    </td>
                                    <td colspan="4" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label runat="server" Visible="false" ID="lblSignDoc2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblSignDoc" CssClass="Clicked" runat="server" Text="Aplication with Signature and Seal"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label ID="Label9" runat="server" Text="Scanned copy of signature and seal document:"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label runat="server" Text="Step1:Signature and Seal document can be obtain from Preview option in Renew-Save As Draft on Applicant Home Page."></asp:Label>
                                                                &nbsp; or&nbsp;                                                                                                                        
                                                                <asp:LinkButton ID="lbtnPrint" runat="server" PostBackUrl="~/ExternalUser/MiningRenew/Reports/MINRenewSADReportViewer.aspx">click here</asp:LinkButton>
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
                                                                            OnClientClick="javascript:ApplicationSignatureSealShowWait();" />
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
                                                        CssClass="SubFormWOBG" DataKeyNames="MiningRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        ShowHeaderWhenEmpty="true" Width="100%"
                                                        OnRowDeleting="gvAplicationSignatureSeal_RowDeleting">
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
                                                                    <asp:LinkButton ID="lbtnAplicationSignatureSealViewFile" runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                                        CssClass="SubFormWOBG" DataKeyNames="MiningRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                                    <asp:LinkButton runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                    <td valign="top">K.
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
                                                        CssClass="SubFormWOBG" DataKeyNames="MiningRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                                    <asp:LinkButton runat="server" CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                    <td valign="top">L.
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
                                                                                <asp:Label runat="server" BackColor="#507CD1"
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
                                                        DataKeyNames="MiningRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
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
                                                                        CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                    <td valign="top">M.
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table class="SubFormWOBG" width="100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:HiddenField runat="server" ID="hdnWaterQualityMinSeep" />
                                                                <asp:Label runat="server" Visible="false" ID="lblWaterQualityMinSeep2">
                                        <span class="Coumpulsory">*</span>
                                                                </asp:Label>
                                                                <asp:Label ID="lblWaterQualityMinSeep" runat="server" CssClass="Clicked" Text="Water Quality Report Of Mine Seepage/Discharge"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label ID="Label6" runat="server" Text="Water Quality Report Of Mine Seepage/Discharge:"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment Name:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtWaterQualityMinSeep" runat="server" MaxLength="50"
                                                                    Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtWaterQualityMinSeep"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGWaterQualityMinSeep"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator runat="server" Display="Dynamic"
                                                                    ForeColor="Red" ControlToValidate="txtWaterQualityMinSeep" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                                                    ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGWaterQualityMinSeep"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Select Attachment File :
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanelWaterQualityMinSeep" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUploadWaterQualityMinSeep" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileUploadWaterQualityMinSeep" runat="server" />
                                                                        <asp:Button ID="btnUploadWaterQualityMinSeep" runat="server" Text="Upload"
                                                                            ValidationGroup="VGWaterQualityMinSeep"
                                                                            OnClick="btnUplodWaterQualityMinSeep_Click"
                                                                            OnClientClick="javascript:ShowWaterQualityMinSeep();" />
                                                                        <asp:UpdateProgress ID="UpdateProgressWaterQualityMinSeep" runat="server"
                                                                            AssociatedUpdatePanelID="UpdatePanelWaterQualityMinSeep">
                                                                            <ProgressTemplate>
                                                                                <asp:Label runat="server" BackColor="#507CD1"
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
                                                    <asp:GridView ID="gvWaterQualityMinSeep" runat="server" ShowHeaderWhenEmpty="true"
                                                        CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                                        DataKeyNames="MiningRenewApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                                        Width="100%"
                                                        OnRowDeleting="gvWaterQualityMinSeep_RowDeleting">
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
                                                                        CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("MiningRenewApplicationCode") + "," + Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                        &nbsp;<asp:Label ID="lblMessageWaterQualityMinSeep" runat="server"></asp:Label>
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
                                        <asp:Button ID="txtSubmit" runat="server" Text="Next >>" ValidationGroup="CommunicationAddress"
                                            OnClick="txtSubmit_Click" />
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
