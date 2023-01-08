<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true"
    CodeFile="SelfComplianceB.aspx.cs" Inherits="ExternalUser_Compliance_SelfComplianceB" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .Initial {
            display: block;
            padding: 4px 10px 4px 10px;
            float: left;
            background-color: #CFE3FA;
            color: Black;
            font-weight: bold;
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
    <script type="text/javascript">
        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById("<%=chklistTypeOfARStruct.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }
        function NOCShowWait() {
            if (document.getElementById('<%= FileUploadNOC.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressNOC.ClientID %>')
                x.style.display = 'inline';
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table align="center" class="SubFormWOBG" width="100%" style="line-height: 20px">
        <tr>
            <td colspan="7" style="text-align: right">(<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-
                            Upload Attachments in <strong>Attachment</strong> Section<br />
                <%--     Maximum Number of Attachment Allowed- 5<br />--%>
                Maximum Size of each Attachment Allowed- 5MB<br />
                Allowed file type for Attachment- txt, doc, docx, jpg, jpeg, pdf
            </td>
        </tr>
        <tr>
            <td colspan="7">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="7">
                            <center>
                                <div style="background-color: #094E7F; width: 100%; text-align: center">
                                    <asp:Label ID="lblHeading" runat="server" ForeColor="White" Font-Bold="True" Font-Size="Medium" Text=" Self Compliance"></asp:Label>
                                </div>
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 17%">
                            <asp:Button ID="Tab1" BorderStyle="None" runat="server" Text="(A)"
                                CssClass="Initial" Width="100%" Enabled="false" />
                        </td>

                        <td style="width: 17%">
                            <asp:Button ID="Tab2" BorderStyle="None" runat="server" Text="(B)" CssClass="Clicked"
                                Width="100%" Enabled="false" />
                        </td>
                        <td style="width: 17%">
                            <asp:Button ID="Button1" BorderStyle="None" runat="server" Text="(C)"
                                CssClass="Initial" Width="100%" Enabled="false" />
                        </td>
                        <td style="width: 17%">
                            <asp:Button ID="Button2" BorderStyle="None" runat="server" Text="(D)"
                                CssClass="Initial" Width="100%" Enabled="false" />
                        </td>

                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Text="6" Font-Bold="true"></asp:Label></td>
            <td>(ii) Copy of NOC attached:</td>
            <td colspan="5">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelNOC" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnUploadNOC" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:FileUpload ID="FileUploadNOC" runat="server" />
                                    <asp:Button ID="btnUploadNOC" runat="server" Text="Upload" Style="width: 60px"
                                        ValidationGroup="VGNOC"
                                        OnClick="btnUploadNOC_Click"
                                        OnClientClick="javascript:NOCShowWait();" />
                                    <asp:UpdateProgress ID="UpdateProgressNOC" runat="server"
                                        AssociatedUpdatePanelID="UpdatePanelNOC">
                                        <ProgressTemplate>
                                            <asp:Label ID="lblNOCWait" runat="server" BackColor="#507CD1"
                                                Font-Bold="True" ForeColor="White" Text="Please wait ... Uploading file"></asp:Label>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvNOC" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                ShowHeaderWhenEmpty="true"
                                OnRowDeleting="gvNOC_RowDeleting">
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
                                    <%--    <asp:TemplateField HeaderText="Attachment Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                    No Records exist.
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:Label ID="lblMessageNOC" runat="server"></asp:Label>
                            <asp:Label ID="lblMessageNOCCount" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Text="7" Font-Bold="true"></asp:Label></td>
            <td colspan="6">
                <asp:Label runat="server" Text="Inspection details (Earlier)" Font-Bold="true"></asp:Label>
            </td>

        </tr>
        <tr>
            <td></td>
            <td>(a). Name of agency:</td>
            <td>
                <asp:DropDownList ID="ddlNameOfAgency" runat="server" Width="100px" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlNameOfAgency_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtOtherAgency" runat="server" TextMode="MultiLine" Rows="4"
                    Columns="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvOtherAgency" Display="Dynamic" runat="server"
                    ControlToValidate="txtOtherAgency" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator ID="revtxtOtherAgency" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtOtherAgency" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>

            </td>


            <td>(b). Date of Inspection:</td>
            <td colspan="2">
                <asp:TextBox ID="txtDateOfInsp1" MaxLength="10" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtDateOfInsp1" Display="Dynamic" runat="server"
                    ControlToValidate="txtDateOfInsp1" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

                <asp:ImageButton ID="ImgBtnDateOfInsp1" runat="server" ImageUrl="~/Images/calendar.png"
                    CausesValidation="false" />
                <asp:CalendarExtender runat="server" Enabled="True"
                    TargetControlID="txtDateOfInsp1" PopupButtonID="ImgBtnDateOfInsp1"
                    Format="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>

        </tr>
        <tr>
            <td></td>
            <td>(c). Copy of site inspection report
            </td>
            <td valign="top">
                <asp:DropDownList ID="ddlSiteInsp" runat="server" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlSiteInsp_SelectedIndexChanged">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlSiteInsp" Display="Dynamic" runat="server"
                    ControlToValidate="ddlSiteInsp" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

            </td>
            <td colspan="4">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUploadSiteInspection" runat="server" />
                            <asp:Button ID="btnUploadSiteInspection" runat="server" Text="Upload"
                                OnClick="btnUploadSiteInspection_Click" Style="width: 60px" />


                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvSiteInspection" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                ShowHeaderWhenEmpty="true"
                                OnRowDeleting="gvSiteInspection_RowDeleting">
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
                                    <%--    <asp:TemplateField HeaderText="Attachment Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                    No Records exist.
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:Label ID="lblSiteInspection" runat="server"></asp:Label>

                            <asp:Label ID="lblSiteInspectionCount" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>

        <tr>
            <td></td>
            <td colspan="6">
                <b>Self-Compliance of NOC Conditions:</b>
            </td>
        </tr>
        <tr>
            <td><b>(i)</b>
            </td>
            <td colspan="2">Present withdrawal of Ground Water
            </td>
            <td valign="top" colspan="4">
                <asp:DropDownList ID="ddlPresentwithdrawal" runat="server" Width="100px" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlPresentwithdrawal_SelectedIndexChanged">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlPresentwithdrawal" Display="Dynamic" runat="server"
                    ControlToValidate="ddlPresentwithdrawal" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="6">
                <table align="left" class="SubFormWOBG" width="100%">
                    <tr>
                        <td></td>
                        <td colspan="3"><b>As per NOC</b></td>
                        <td colspan="3"><b>Self Compliance</b></td>
                    </tr>
                    <tr>

                        <td>(a). Abstraction</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtPresentwithdrawalInDay" runat="server" Enabled="false"></asp:TextBox>
                            &nbsp;(m<sup>3</sup>/day)
                 <%--<asp:RequiredFieldValidator ID="rftxtPresentwithdrawalInDay" Display="Dynamic" runat="server"
                     ControlToValidate="txtPresentwithdrawalInDay" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ID="revtxtPresentwithdrawalInDay" runat="server"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtPresentwithdrawalInDay"
                                ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>--%>
                            <asp:TextBox ID="txtPresentwithdrawalInYear" runat="server" Enabled="false"></asp:TextBox>
                            &nbsp;(m<sup>3</sup>/year)
                <%--<asp:RequiredFieldValidator ID="rftxtPresentwithdrawalInYear" Display="Dynamic" runat="server"
                    ControlToValidate="txtPresentwithdrawalInYear" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ID="revtxtPresentwithdrawalInYear" runat="server"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtPresentwithdrawalInYear"
                                ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>--%>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtselfPresentwithdrawalInDay" runat="server"></asp:TextBox>
                            &nbsp;(m<sup>3</sup>/day)
                 <asp:RequiredFieldValidator ID="reftxtselfPresentwithdrawalInDay" Display="Dynamic" runat="server"
                     ControlToValidate="txtselfPresentwithdrawalInDay" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ID="revtxtselfPresentwithdrawalInDay" runat="server"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtselfPresentwithdrawalInDay"
                                ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtselfPresentwithdrawalInYear" runat="server"></asp:TextBox>
                            &nbsp;(m<sup>3</sup>/year)
                <asp:RequiredFieldValidator ID="reftxtselfPresentwithdrawalInYear" Display="Dynamic" runat="server"
                    ControlToValidate="txtselfPresentwithdrawalInYear" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ID="revtxtselfPresentwithdrawalInYear" runat="server"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtselfPresentwithdrawalInYear"
                                ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>



                        <td>(b). Dewatering</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtDewPresentwithdrawalInDay" runat="server" Enabled="false"></asp:TextBox>
                            &nbsp;(m<sup>3</sup>/day)
              <%--  <asp:RequiredFieldValidator ID="rfvtxtDewPresentwithdrawalInDay" Display="Dynamic" runat="server"
                    ControlToValidate="txtDewPresentwithdrawalInDay" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ID="revtxtDewPresentwithdrawalInDay" runat="server"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtDewPresentwithdrawalInDay"
                                ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>--%>
                            <asp:TextBox ID="txtDewPresentwithdrawalInYear" runat="server" Enabled="false"></asp:TextBox>
                            &nbsp;(m<sup>3</sup>/year)
                            <%--                <asp:RequiredFieldValidator ID="rfvtxtDewPresentwithdrawalInYear" Display="Dynamic" runat="server"
                    ControlToValidate="txtDewPresentwithdrawalInYear" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ID="revtxtDewPresentwithdrawalInYear" runat="server"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtDewPresentwithdrawalInYear"
                                ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>--%>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtselfDewPresentwithdrawalInDay" runat="server"></asp:TextBox>
                            &nbsp;(m<sup>3</sup>/day)
                <asp:RequiredFieldValidator ID="reftxtselfDewPresentwithdrawalInDay" Display="Dynamic" runat="server"
                    ControlToValidate="txtselfDewPresentwithdrawalInDay" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ID="revtxtselfDewPresentwithdrawalInDay" runat="server"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtselfDewPresentwithdrawalInDay"
                                ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtselfDewPresentwithdrawalInYear" runat="server"></asp:TextBox>
                            &nbsp;(m<sup>3</sup>/year)
                <asp:RequiredFieldValidator ID="reftxtselfDewPresentwithdrawalInYear" Display="Dynamic" runat="server"
                    ControlToValidate="txtselfDewPresentwithdrawalInYear" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ID="revtxtselfDewPresentwithdrawalInYear" runat="server"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtselfDewPresentwithdrawalInYear"
                                ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td></td>
            <td colspan="2">(c). Any variation in withdrawal (to be reported):</td>
            <td>
                <asp:DropDownList ID="ddlAnyVariation" runat="server" Width="225px" Height="16px"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlAnyVariation_SelectedIndexChanged">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="More than permitted quantum" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Less than permitted quantum" Value="2"></asp:ListItem>
                    <asp:ListItem Text="No" Value="3"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlAnyVariation" Display="Dynamic" runat="server"
                    ControlToValidate="ddlAnyVariation" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtQtyVariDay" runat="server"></asp:TextBox>
                &nbsp;(m<sup>3</sup>/day)
                <asp:RequiredFieldValidator ID="rfvtxtQtyVariDay" Display="Dynamic" runat="server"
                    ControlToValidate="txtQtyVariDay" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator ID="revtxtQtyVariDay" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtQtyVariDay"
                    ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtQtyVariYear" runat="server"></asp:TextBox>
                &nbsp;(m<sup>3</sup>/year)
                 <asp:RequiredFieldValidator ID="rfvtxtQtyVariYear" Display="Dynamic" runat="server"
                     ControlToValidate="txtQtyVariYear" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator ID="revtxtQtyVariYear" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtQtyVariYear"
                    ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
            </td>

            <td colspan="2">(d). Abstraction Data submitted for all the TUBEWELL as per NOC:</td>
            <td>
                <asp:DropDownList ID="ddlAbstractionTW" runat="server" Width="100px">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlAbstractionTW" Display="Dynamic" runat="server"
                    ControlToValidate="ddlAbstractionTW" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td><b>(ii)</b></td>
            <td colspan="6">
                <asp:Label runat="server" Text="Number of abstraction/dewatering structures" Font-Bold="true"></asp:Label>
            </td>

        </tr>
        <tr>
            <td></td>
            <td colspan="6">
                <table align="left" class="SubFormWOBG" width="100%">
                    <tr>

                        <td></td>
                        <td>
                            <asp:Label runat="server" Text="(As per NOC)" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="(Self Compliance)" Font-Bold="true"></asp:Label>

                        </td>
                    </tr>
                    <tr>

                        <td>Existing</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtAbstraStructExistingAsperNOC" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" runat="server"
                                ControlToValidate="txtAbstraStructExistingAsperNOC" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtAbstraStructExistingAsperNOC" runat="server" ForeColor="Red"
                                ControlToValidate="txtAbstraStructExistingAsperNOC" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>

                        </td>
                        <td>Existing
                <asp:TextBox runat="server" ID="txtAbstraStructExisting"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" runat="server"
                                ControlToValidate="txtAbstraStructExisting" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtAbstraStructExisting" runat="server" ForeColor="Red"
                                ControlToValidate="txtAbstraStructExisting" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>

                        </td>
                    </tr>
                    <tr>

                        <td>Proposed</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtAbstraStructProposedAsperNOC" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" runat="server"
                                ControlToValidate="txtAbstraStructProposedAsperNOC" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtAbstraStructProposedAsperNOC" runat="server" ForeColor="Red"
                                ControlToValidate="txtAbstraStructProposedAsperNOC" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>

                        </td>
                        <td>Proposed
                <asp:TextBox runat="server" ID="txtAbstraStructProposed"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" runat="server"
                                ControlToValidate="txtAbstraStructProposed" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtAbstraStructProposed" runat="server" ForeColor="Red"
                                ControlToValidate="txtAbstraStructProposed" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <%--<tr>
            <td></td>
            <td>At Present</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtAtPresent"></asp:TextBox></td>
        </tr>--%>
        <tr>
            <td></td>
            <td>Number of functional abstraction structures</td>
            <td>
                <asp:TextBox runat="server" ID="txtNoOfFunt"></asp:TextBox>
                <asp:RequiredFieldValidator Display="Dynamic" runat="server"
                    ControlToValidate="txtNoOfFunt" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxtNoOfFunt" runat="server" ForeColor="Red"
                    ControlToValidate="txtNoOfFunt" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
            </td>

            <td>Geotagged photograogh  of withdrawal structures</td>
            <td>
                <asp:DropDownList ID="ddlgeophotostruct" runat="server" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlgeophotostruct_SelectedIndexChanged">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" runat="server"
                    ControlToValidate="ddlgeophotostruct" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

            </td>
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUploadgeophotostruct" runat="server" />
                            <asp:Button ID="btnUploadgeophotostruct" runat="server" Text="Upload"
                                OnClick="btnUploadgeophotostruct_Click" Style="width: 60px" />


                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvgeophotostruct" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                ShowHeaderWhenEmpty="true"
                                OnRowDeleting="gvgeophotostruct_RowDeleting">
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
                                    <%--    <asp:TemplateField HeaderText="Attachment Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                    No Records exist.
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:Label ID="lblMessagegeophotostruct" runat="server"></asp:Label>

                            <asp:Label ID="lblgeophotostructCount" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
        <tr>
            <td><b>(iii)</b></td>
            <td colspan="6">
                <asp:Label runat="server" Text="Water meter details" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>All the abstraction structure fitted with water meter</td>
            <td colspan="2">
                <asp:DropDownList ID="AbstStructWM" runat="server" Width="100px">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" runat="server"
                    ControlToValidate="AbstStructWM" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

            </td>

            <td>Type of meter</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlMeterType" runat="server" Width="100px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" runat="server"
                    ControlToValidate="ddlMeterType" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td></td>
            <%--<td>Telemetry Installed</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlTeleInstalled" runat="server" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlTeleInstalled_SelectedIndexChanged">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" runat="server"
                    ControlToValidate="ddlTeleInstalled" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

                <br />
                <asp:LinkButton runat="server" Text="Add Telemetry Detail" Visible="false" ID="lnkTelemetryDetail" OnClick="lnkTelemetryDetail_Click"></asp:LinkButton>
            </td>--%>

            <td>Number of functional meter</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtFunctMeter"></asp:TextBox>
                <asp:RequiredFieldValidator Display="Dynamic" runat="server"
                    ControlToValidate="txtFunctMeter" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxtFunctMeter" runat="server" ForeColor="Red"
                    ControlToValidate="txtFunctMeter" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
            </td>
        </tr>
       <%-- <tr>
            <td></td>

            <td>Piezometers</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlPiezometer" runat="server" Width="100px" OnSelectedIndexChanged="ddlPiezometer_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" runat="server"
                    ControlToValidate="ddlPiezometer" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

                <br />
                <asp:LinkButton runat="server" Text="Add Piezometers Details" Visible="false" ID="lnkAddPiezometerDetail" OnClick="lnkAddPiezometerDetail_Click"></asp:LinkButton>
           
                 </td> 
        </tr>--%>
        <tr>
            <td></td>
            <td>Annual calibration of water meter by Govt agencies </td>
            <td>
                <asp:DropDownList ID="ddlAnnualCali" runat="server" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlAnnualCali_SelectedIndexChanged">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" runat="server"
                    ControlToValidate="ddlAnnualCali" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

            </td>
            <td colspan="5">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblAnnual" Text="Attatch Certificate" Font-Bold="true"></asp:Label>
                            <asp:FileUpload ID="FileUploadAnnualCalibration" runat="server" />
                            <asp:Button ID="btnFileUploadAnnualCalibration" runat="server" Text="Upload"
                                OnClick="btnFileUploadAnnualCalibration_Click" Style="width: 60px" />


                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvAnnualCalibration" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                ShowHeaderWhenEmpty="true"
                                OnRowDeleting="gvAnnualCalibration_RowDeleting">
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

                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                    No Records exist.
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:Label ID="lblAnnualCalibration" runat="server"></asp:Label>

                            <asp:Label ID="lblAnnualCalibrationCount" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>Geotagged Photograph of well fitted with water meter attached</td>
            <td>
                <asp:DropDownList ID="ddlGeoPhotoFittedWM" runat="server" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlGeoPhotoFittedWM_SelectedIndexChanged">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" runat="server"
                    ControlToValidate="ddlGeoPhotoFittedWM" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

            </td>
            <td colspan="5">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUploadGeoPhotowellfitted" runat="server" />
                            <asp:Button ID="btnUploadGeoPhotowellfitted" runat="server" Text="Upload" OnClick="btnUploadGeoPhotowellfitted_Click" Style="width: 60px" />


                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvGeoPhotowellfitted" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                ShowHeaderWhenEmpty="true"
                                OnRowDeleting="gvGeoPhotowellfitted_RowDeleting">
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
                                    <%--    <asp:TemplateField HeaderText="Attachment Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                    No Records exist.
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:Label ID="lblGeoPhotowellfitted" runat="server"></asp:Label>

                            <asp:Label ID="lblGeoPhotowellfittedCount" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <b>(iv)</b>
            </td>
            <td colspan="6">
                <asp:Label runat="server" Text="Ground Water Quality" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>Water samples have been analyzed in Govt. approved lab </td>
            <td>
                <asp:DropDownList ID="ddlWaterSampple" runat="server" Width="100px">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" runat="server"
                    ControlToValidate="ddlWaterSampple" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

            </td>

            <td>Report submitted within stipulated time frame

            </td>
            <td colspan="7">
                <asp:DropDownList ID="ddlWQRSubmitted" runat="server" Width="100px">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" runat="server"
                    ControlToValidate="ddlWQRSubmitted" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

            </td>


        </tr>
        <tr>
            <td></td>
            <td>Ground water Quality report attached</td>
            <td>
                <asp:DropDownList ID="ddlGWQReport" runat="server" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlGWQReport_SelectedIndexChanged">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" runat="server"
                    ControlToValidate="ddlGWQReport" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUploadWQRSubmitted" runat="server" />
                            <asp:Button ID="btnUploadWQRSubmitted" runat="server" Text="Upload"
                                OnClick="btnUploadWQRSubmitted_Click" Style="width: 60px" />


                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvWQRSubmitted" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                ShowHeaderWhenEmpty="true"
                                OnRowDeleting="gvWQRSubmitted_RowDeleting">
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
                                    <%--    <asp:TemplateField HeaderText="Attachment Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                    No Records exist.
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:Label ID="lblWQRSubmitted" runat="server"></asp:Label>

                            <asp:Label ID="lblWQRSubmittedCount" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>

            <td>Mine seepage quality report attached (in case of mining projects):

            </td>
            <td>
                <asp:DropDownList ID="ddlMineSeepage" runat="server" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlMineSeepage_SelectedIndexChanged">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" runat="server"
                    ControlToValidate="ddlMineSeepage" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUploadMineseepage" runat="server" />
                            <asp:Button ID="btnUploadMineseepage" runat="server" Text="Upload"
                                OnClick="btnUploadMineseepage_Click" Style="width: 60px" />


                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvMineseepage" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                ShowHeaderWhenEmpty="true"
                                OnRowDeleting="gvMineseepage_RowDeleting">
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
                                    <%--    <asp:TemplateField HeaderText="Attachment Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                    No Records exist.
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:Label ID="lblMineseepage" runat="server"></asp:Label>

                            <asp:Label ID="lblMineseepageCount" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td><b>(v)</b>

            </td>
            <td colspan="3">
                <asp:Label runat="server" Text="Details of Artificial recharge measures /Rain water harvesting implemented ( For NOC issued before 24/09/2020)" Font-Bold="true">

                </asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlRainwaterharvesting" runat="server" Width="100px" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlRainwaterharvesting_SelectedIndexChanged">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" runat="server"
                    ControlToValidate="ddlRainwaterharvesting" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>Type of structures</td>
            <td colspan="2">
                <%-- <asp:DropDownList ID="ddltypestr" runat="server" Width="100px">
                </asp:DropDownList>--%>
                <asp:CheckBoxList runat="server" ID="chklistTypeOfARStruct" RepeatDirection="Vertical">
                </asp:CheckBoxList>
                <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Please select at least one item."
                    ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList" runat="server" ValidationGroup="SelfCompliance" />
                <%-- <asp:RequiredFieldValidator ID="rfvddltypestr" Display="Dynamic" runat="server"
                    ControlToValidate="chklistTypeOfARStruct" InitialValue="" ForeColor="Red" 
                    ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>--%>

               
            </td>

            <td>No of structures</td>
            <td colspan="3">
                <asp:TextBox runat="server" ID="txtNoOfStruct"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtNoOfStruct" Display="Dynamic" runat="server"
                    ControlToValidate="txtNoOfStruct" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxtNoOfStruct" runat="server" ForeColor="Red"
                    ControlToValidate="txtNoOfStruct" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>Within premises/outside premises</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlWithinOutSidePremises" runat="server" Width="100px">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlWithinOutSidePremises" Display="Dynamic" runat="server"
                    ControlToValidate="ddlWithinOutSidePremises" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

            </td>

            <td>Quantum of recharge measures implemented by the applicant(cum/annum): </td>
            <td colspan="3">
                <asp:TextBox runat="server" ID="txtQuantOfRecharge"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtQuantOfRecharge" Display="Dynamic" runat="server"
                    ControlToValidate="txtQuantOfRecharge" InitialValue="" ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxtQuantOfRecharge" runat="server" ForeColor="Red"
                    ControlToValidate="txtQuantOfRecharge" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>Geotagged photograph  of recharge structures</td>
            <td>
                <asp:DropDownList ID="ddlGeoPhotoRechargeStruc" runat="server" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlGeoPhotoRechargeStruc_SelectedIndexChanged">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlGeoPhotoRechargeStruc" Display="Dynamic" runat="server"
                    ControlToValidate="ddlGeoPhotoRechargeStruc" InitialValue=""
                    ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

            </td>
            <td colspan="4">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUploadRainwaterharvesting" runat="server" />
                            <asp:Button ID="btnUploadRainwaterharvesting" runat="server" Text="Upload"
                                OnClick="btnUploadRainwaterharvesting_Click" Style="width: 60px" />


                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvRainwaterharvesting" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                ShowHeaderWhenEmpty="true"
                                OnRowDeleting="gvRainwaterharvesting_RowDeleting">
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
                                    <%--    <asp:TemplateField HeaderText="Attachment Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                    No Records exist.
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:Label ID="lblgvRainwaterharvesting" runat="server"></asp:Label>

                            <asp:Label ID="lblgvRainwaterharvestingCount" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td><b>(vi)</b></td>
            <td colspan="6">
                <asp:Label runat="server" Text="Groundwater monitoring details:" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="6">
                <table align="left" class="SubFormWOBG" width="100%">
                    <tr>
                        <td></td>
                        <td><b>As Per NOC</b></td>
                        <td><b>Self Compliance</b></td>
                    </tr>
                    <tr>
                        <td>No. of piezometer (observation well) installed:</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtNOPiezo" Enabled="false"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revtxtNOPiezo" runat="server" ForeColor="Red"
                                ControlToValidate="txtNOPiezo" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>


                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtselfNOPiezo"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revtxtselfNOPiezo" runat="server" ForeColor="Red"
                                ControlToValidate="txtselfNOPiezo" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>No. of piezometer with DIGITAL WATER LEVEL RECORDER:</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtNOPiezoDWLR" Enabled="false"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revtxtNOPiezoDWLR" runat="server" ForeColor="Red"
                                ControlToValidate="txtNOPiezoDWLR" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>


                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtselfNOPiezoDWLR"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revtxtselfNOPiezoDWLR" runat="server" ForeColor="Red"
                                ControlToValidate="txtselfNOPiezoDWLR" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <%-- <tr>
                        <td>No. of piezometer with Telemetry:</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtNOPiezoTele" Enabled="false"></asp:TextBox>
                            
                            <asp:RegularExpressionValidator ID="revtxtNOPiezoTele" runat="server" ForeColor="Red"
                                ControlToValidate="txtNOPiezoTele" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>

                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtselfNOPiezoTele"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revtxtselfNOPiezoTele" runat="server" ForeColor="Red"
                                ControlToValidate="txtselfNOPiezoTele" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>Piezometer with DIGITAL WATER LEVEL RECORDER & Telemetry</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtpieDWLRTelem" Enabled="false"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revtxtpieDWLRTelem" runat="server" ForeColor="Red"
                                ControlToValidate="txtpieDWLRTelem" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtselfpieDWLRTelem"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revtxtselfpieDWLRTelem" runat="server" ForeColor="Red"
                                ControlToValidate="txtselfpieDWLRTelem" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>Number of functional Piezometer/Observational well:</td>
            <td colspan="5">
                <asp:TextBox runat="server" ID="NoFunctionalPiewell"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revNoFunctionalPiewell" runat="server" ForeColor="Red"
                    ControlToValidate="NoFunctionalPiewell" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>

            </td>
        </tr>
        <tr>
            <td></td>
            <td>Geotagged photograph of Piezometers/observation well/key well fitted with DIGITAL WATER LEVEL RECORDER/Telemetry</td>
            <td>
                <asp:DropDownList ID="ddlgeophotopiewell" runat="server" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlgeophotopiewell_SelectedIndexChanged">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>

            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUploadGroundwaterMonitoring" runat="server" />
                            <asp:Button ID="btnUploadGroundwaterMonitoring" runat="server" Text="Upload"
                                OnClick="btnUploadGroundwaterMonitoring_Click" Style="width: 60px" />


                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvGroundwaterMonitoring" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                ShowHeaderWhenEmpty="true"
                                OnRowDeleting="gvGroundwaterMonitoring_RowDeleting">
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
                                    <%--    <asp:TemplateField HeaderText="Attachment Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                    No Records exist.
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:Label ID="lblGroundwaterMonitoring" runat="server"></asp:Label>

                            <asp:Label ID="lblGroundwaterMonitoringCount" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>


            <td>Monitoring data submitted as per NOC</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlMoniDataSubmitted" runat="server" Width="100px">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td></td>


            <td>No. of observation well/key well in core and buffer zone (in case of Mining project):</td>
            <td>
                <asp:TextBox runat="server" ID="txtNoOversWell"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revtxtNoOversWell" runat="server" ForeColor="Red"
                    ControlToValidate="txtNoOversWell" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>

            </td>
            <td>Ground Water Monitoring data</td>
            <td>
                <asp:DropDownList ID="ddlGroundWaterMonitoringData" runat="server" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlGroundWaterMonitoringData_SelectedIndexChanged">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList></td>
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUploadGroundWaterMonitoringData" runat="server" />
                            <asp:Button ID="btnUploadGroundWaterMonitoringData" runat="server" Text="Upload"
                                OnClick="btnUploadGroundWaterMonitoringData_Click" Style="width: 60px" />


                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvGroundWaterMonitoringData" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                ShowHeaderWhenEmpty="true"
                                OnRowDeleting="gvGroundWaterMonitoringData_RowDeleting">
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
                                    <%--    <asp:TemplateField HeaderText="Attachment Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                    No Records exist.
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:Label ID="lblGroundWaterMonitoringData" runat="server"></asp:Label>

                            <asp:Label ID="lblGroundWaterMonitoringDataCount" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td><b>(vii)</b></td>
            <td colspan="5">
                <asp:Label runat="server" Text="Details of treated wastewater (Recycle/Reuse):" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>SEWAGE TREATMENT PLANT/EFFLUENT TREATMENT PLANT installed:</td>
            <td colspan="2">
                <asp:DropDownList runat="server" ID="ddlSTPETP" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlSTPETP_SelectedIndexChanged">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList></td>

            <td>No. of SEWAGE TREATMENT PLANT/EFFLUENT TREATMENT PLANT installed:</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtNoSTPETP"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revtxtNoSTPETP" runat="server" ForeColor="Red"
                    ControlToValidate="txtNoSTPETP" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>

            </td>
        </tr>
        <tr>
            <td></td>
            <td>Capacity of SEWAGE TREATMENT PLANT/EFFLUENT TREATMENT PLANT:</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtCapSTPETP"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revtxtCapSTPETP" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtCapSTPETP"
                    ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
            </td>
            <td>Quantum of treated waste water generated (cum/y):</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtQuantumTWW"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revtxtQuantumTWW" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtQuantumTWW"
                    ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>Geotagged photograph of SEWAGE TREATMENT PLANT/EFFLUENT TREATMENT PLANT:</td>
            <td>
                <asp:DropDownList ID="ddlgeophotoSTPETP" runat="server" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlgeophotoSTPETP_SelectedIndexChanged">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList></td>
            <td colspan="4">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUploadSTPETP" runat="server" />
                            <asp:Button ID="btnUploadSTPETP" runat="server" Text="Upload"
                                OnClick="btnUploadSTPETP_Click" Style="width: 60px" />


                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvSTPETP" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                ShowHeaderWhenEmpty="true"
                                OnRowDeleting="gvSTPETP_RowDeleting">
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
                                    <%--    <asp:TemplateField HeaderText="Attachment Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
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
                                    No Records exist.
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:Label ID="lblSTPETP" runat="server"></asp:Label>

                            <asp:Label ID="lblSTPETPCount" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="6">
                <asp:Label runat="server" Text="Quantum of treated water used (cum/y):" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>Industrial process:</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtIndProcess"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revtxtIndProcess" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtIndProcess"
                    ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
            </td>

            <td>Greenbelt:</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtGreenbelt"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revtxtGreenbelt" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtGreenbelt"
                    ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>Other uses:</td>
            <td colspan="5">
                <asp:TextBox runat="server" ID="txtOtherUse"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revtxtOtherUse" runat="server"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtOtherUse"
                    ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
            </td>
        </tr>




        <tr>
            <td colspan="7">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                <asp:Label ID="lblPageTitleFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                <asp:Label ID="lblApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                <asp:Label ID="lblApplicationCode" Visible="false" runat="server" Enabled="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="7" style="text-align: center">
                <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('If you have not saved data, Your data will be lost before moving to other page ?');"
                    OnClick="btnPrev_Click" />
                <asp:Button ID="btnSaveAsDraft" ValidationGroup="SelfCompliance" runat="server" Text="Save As Draft"
                    OnClick="btnSaveAsDraft_Click" Style="height: 26px" />
                <asp:Button ID="btnNext" ValidationGroup="SelfCompliance" runat="server" Text="Next >>"
                    OnClick="btnNext_Click" Style="height: 26px" />
            </td>
        </tr>
    </table>
</asp:Content>

