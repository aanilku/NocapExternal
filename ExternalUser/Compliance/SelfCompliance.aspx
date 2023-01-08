<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true"
    MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    AutoEventWireup="true" CodeFile="SelfCompliance.aspx.cs" Inherits="ExternalUser_Compliance_SelfCompliance"  %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
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
                            <asp:Button ID="Tab2" BorderStyle="None" runat="server" Text="(B)" CssClass="Initial"
                                Width="100%" Enabled="false" />
                        </td>
                        <td style="width: 17%">
                            <asp:Button ID="Button1" BorderStyle="None" runat="server" Text="(C)"
                                CssClass="Clicked" Width="100%" Enabled="false" />
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
                <asp:Label runat="server" Text="(viii)" Font-Bold="true"></asp:Label></td>
            <td colspan="2">
                <asp:Label runat="server" Text="Submission of Self Compliance report online within stipulated time frame:" Font-Bold="true"></asp:Label>
            </td>
            <td colspan="4">
                <asp:DropDownList ID="ddlComplianceReportWSTF" runat="server" Width="100px">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList></td>

            </tr><tr>
            <td>
                <asp:Label runat="server" Text="(ix)" Font-Bold="true"></asp:Label></td>
            <td colspan="2">
                <asp:Label runat="server" Text="Details of Water Audit Inspection (if applicable):" Font-Bold="true"></asp:Label>
            </td>
            <td colspan="4">
                <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="IFWaterAuditInspection_SelectedIndexChanged"
                    runat="server" ID="IFddlWaterAuditInspection">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2">Water Audit inspection</td>
            <td>
                <asp:DropDownList ID="ddlWateAuditinspection" runat="server" Width="100px">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlWateAuditinspection" Display="Dynamic" runat="server"
                    ControlToValidate="ddlWateAuditinspection" InitialValue=""
                    ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>
            </td>


            <td colspan="2">Water Audit inspection carried out as per NOC
            </td>
            <td>
                <asp:DropDownList ID="ddlWateAuditinspectionNOC" runat="server" Width="100px">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvddlWateAuditinspectionNOC" Display="Dynamic" runat="server"
                    ControlToValidate="ddlWateAuditinspectionNOC" InitialValue=""
                    ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

            </td>

        </tr>
        <tr>
            <td></td>
            <td colspan="2">Name of agency by which water audit carried out</td>
            <td colspan="4">
                <asp:TextBox runat="server" ID="txtAuditAgency" Width="699px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtAuditAgency" Display="Dynamic" runat="server"
                    ControlToValidate="txtAuditAgency" InitialValue=""
                    ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>Date of inspection</td>
            <td colspan="2">
                <asp:TextBox ID="txtDateOfInsp" MaxLength="10" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtDateOfInsp" Display="Dynamic" runat="server"
                    ControlToValidate="txtDateOfInsp" InitialValue=""
                    ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>
                <asp:ImageButton ID="ImgBtnDateOfInsp" runat="server" ImageUrl="~/Images/calendar.png"
                    CausesValidation="false" />
                <asp:CalendarExtender runat="server" ID="calDateOfInsp" Enabled="false"
                    TargetControlID="txtDateOfInsp" PopupButtonID="ImgBtnDateOfInsp"
                    Format="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>

            <td>Water audit report attached
            </td>
            <td>
                <asp:DropDownList ID="ddlWaterauditreportattached" runat="server" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlWaterauditreportattached_SelectedIndexChanged">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlWaterauditreportattached" Display="Dynamic" runat="server"
                    ControlToValidate="ddlWaterauditreportattached" InitialValue=""
                    ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUploadWaterAuditInspection" runat="server" />
                            <asp:Button ID="btnUploadWaterAuditInspection" runat="server" Text="Upload" ValidationGroup="SelfCompliance"
                                OnClick="btnUploadWaterAuditInspection_Click" Style="width: 60px" />


                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvWaterAuditInspection" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                ShowHeaderWhenEmpty="true"
                                OnRowDeleting="gvWaterAuditInspection_RowDeleting">
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
                            <asp:Label ID="lblWaterAuditInspection" runat="server"></asp:Label>

                            <asp:Label ID="lblWaterAuditInspectionCount" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Text="(x)" Font-Bold="true"></asp:Label></td>
            <td colspan="2">
                <asp:Label runat="server" Text="Impact assessment report/Comprehensive Hydro geological Report /Modeling report (if applicable): " Font-Bold="true"></asp:Label>
            </td>
            <td colspan="4">
                <asp:DropDownList runat="server" ID="ddlImpactassessmentreport" AutoPostBack="true" OnSelectedIndexChanged="ddlImpactassessmentreport_SelectedIndexChanged">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td></td>
            <td>Requirement
            </td>
            <td colspan="2">
                <asp:DropDownList ID="ddlRequirement" runat="server" Width="100px">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlRequirement" Display="Dynamic" runat="server"
                    ControlToValidate="ddlRequirement" InitialValue=""
                    ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator></td>


            <td>Submitted as per NOC requirement
            </td>
            <td colspan="2">
                <asp:DropDownList ID="ddlSubmittedNOC" runat="server" Width="100px">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlSubmittedNOC" Display="Dynamic" runat="server"
                    ControlToValidate="ddlSubmittedNOC" InitialValue=""
                    ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>
            </td>

        </tr>
        <tr>
            <td></td>
            <td colspan="2">Copy of IMPACT ASSESSMENT REPORT/modeling report/Hydrogeological report attached: 
            </td>
            <td>
                <asp:DropDownList ID="ddlCopyIAR" runat="server" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlCopyIAR_SelectedIndexChanged">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlCopyIAR" Display="Dynamic" runat="server"
                    ControlToValidate="ddlCopyIAR" InitialValue=""
                    ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

            </td>
            <td colspan="3">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUploadIAR" runat="server" />
                            <asp:Button ID="btnUploadIAR" runat="server" Text="Upload" ValidationGroup="SelfCompliance"
                                OnClick="btnUploadIAR_Click" Style="width: 60px" />


                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvIAR" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                ShowHeaderWhenEmpty="true"
                                OnRowDeleting="gvIAR_RowDeleting">
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
                            <asp:Label ID="lblIAR" runat="server"></asp:Label>

                            <asp:Label ID="lblIARCount" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>

            </td>

        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Text="(xi)" Font-Bold="true"></asp:Label></td>
            <td colspan="2">
                <asp:Label runat="server" Text="Any Violation of NOC conditions to be reported (If any):" Font-Bold="true"></asp:Label>
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td valign="top">
                            <asp:DropDownList ID="ddlAnyViolation" runat="server" Width="100px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlAnyViolation_SelectedIndexChanged">
                                <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:DropDownList>

                        </td>
                        <td valign="top">
                            <asp:TextBox ID="txtAnyViolation" runat="server" TextMode="MultiLine" Rows="4"
                                Columns="25"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtAnyViolation" Display="Dynamic" runat="server"
                                ControlToValidate="txtAnyViolation" InitialValue=""
                                ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtAnyViolation" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtAnyViolation" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </td>

            <td>
                <asp:Label runat="server" Text="(xii)" Font-Bold="true"></asp:Label>

            </td>
            <td>
                <asp:Label runat="server" Text="Any other compliances as per NOC condition (If any):" Font-Bold="true"></asp:Label>
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td valign="top">
                            <asp:DropDownList ID="ddlAnyothercompliance" runat="server" Width="100px"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlAnyothercompliance_SelectedIndexChanged">
                                <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:DropDownList>

                        </td>
                        <td valign="top">
                            <asp:TextBox ID="txtAnyothercompliance" runat="server" TextMode="MultiLine" Rows="4"
                                Columns="25"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revtxtAnyothercompliance" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtAnyothercompliance" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfvtxtAnyothercompliance" Display="Dynamic" runat="server"
                                ControlToValidate="txtAnyothercompliance" InitialValue=""
                                ForeColor="Red" ValidationGroup="SelfCompliance">Required</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label runat="server" Text="(xiii)" Font-Bold="true"></asp:Label>
            </td>
            <td>Extra Attachment
            </td>
            <td valign="top">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUploadExtra" runat="server" />
                            <asp:Button ID="btnUploadExra" runat="server" Text="Upload" ValidationGroup="SelfCompliance"
                                OnClick="btnUploadExra_Click" Style="width: 60px" />


                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvExtra" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                ShowHeaderWhenEmpty="true"
                                OnRowDeleting="gvExtra_RowDeleting">
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
                            <asp:Label ID="lblExtra" runat="server"></asp:Label>

                            <asp:Label ID="lblExtraCount" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>

            <td><b>(xiiiv)</b>
            </td>
            <td>Remarks
            </td>
            <td valign="top" colspan="2">
                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="4" Columns="25"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revtxtRemarks" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtRemarks" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
            </td>
        </tr>

        <tr>
            <td colspan="7">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                <asp:Label ID="lblPageTitleFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                <asp:Label ID="lblApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
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
