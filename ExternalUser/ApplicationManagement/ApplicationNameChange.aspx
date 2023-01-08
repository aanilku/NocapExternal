<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" CodeFile="ApplicationNameChange.aspx.cs" Inherits="ExternalUser_ApplicationManagement_ApplicationNameChange" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../../Scripts/ClientSideDateValidation.js" type="text/javascript"></script>
    <script type="text/javascript">
        function CountCharacter(controlId, countControlId, maxCharlimit) {
            var charLeft = maxCharlimit - controlId.value.length;
            countControlId.value = '( ' + parseInt(maxCharlimit - controlId.value.length) + ' Character Left )';
            if (charLeft < 0) {
                countControlId.style.color = "Red";

            }
            else {
                countControlId.style.color = "Black";

            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRFs" runat="server" Value="" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table align="center" class="SubFormWOBG" width="100%">
        <tr>
            <td>
                <table class="SubFormWOBG" width="100%" style="line-height: 25px">
                    <tr>

                        <td style="width: 20px">
                            <b>(1).</b>
                        </td>
                        <td colspan="2">
                            <b>Application Name Change
                                <br />
                            </b>
                        </td>

                    </tr>
                    <tr>
                        <td></td>
                        <td>Application Type: <span class="Coumpulsory">*</span>
                            <td>
                                <asp:DropDownList ID="ddlApplicationType" runat="server" AutoPostBack="True"
                                    Width="200px" OnSelectedIndexChanged="ddlApplicationType_SelectedIndexChanged">
                                </asp:DropDownList>

                            </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Application No: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlApplicatonNumber" runat="server" AutoPostBack="true"
                                Width="200px" OnSelectedIndexChanged="ddlApplicatonNumber_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue=""
                                Display="Dynamic" ControlToValidate="ddlApplicatonNumber" ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Existing Name<span class="Coumpulsory"></span>
                        </td>
                        <td>
                            <asp:TextBox ID="TXTExistingName" runat="server" MaxLength="100" Width="51%" Enabled="false"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>New Name<span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewName" runat="server" MaxLength="100" Width="51%"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Correction Category: <span class="Coumpulsory">*</span></td>
                        <td>
                            <asp:DropDownList ID="ddlCorrecationCharge" runat="server" AutoPostBack="True"
                                Width="200px" OnSelectedIndexChanged="ddlCorrecationCharge_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue=""
                                Display="Dynamic" ControlToValidate="ddlCorrecationCharge" ForeColor="Red" ValidationGroup="LocationDetails">Required</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Correction Rate<span class="Coumpulsory"></span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCorrecationRate" runat="server" MaxLength="100" Width="51%" Enabled="false"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Reason To Change:<span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtReasonToChange" runat="server" MaxLength="100"
                                TextMode="MultiLine" onkeyup="CountCharacter(this, this.form.ActionCommentRemCount, 100);"
                                onkeydown="CountCharacter(this, this.form.ActionCommentRemCount, 100);" Height="77px" Width="374px"></asp:TextBox>
                            <br />
                            <input type="text" id="ActionCommentRemCount" tabindex="-1" style="border-width: 0px; width: 100px; font-size: 10px; text-align: left; margin-left: 110px; background-color: transparent"
                                name="ActionCommentRemCount" size="2" maxlength="2" value="( 100 Character Left )"
                                readonly="readonly" /><br />

                        </td>
                    </tr>


                    <tr>
                        <td></td>
                        <td>Upload Documents: <span class="Coumpulsory">*</span>
                        </td>

                        <td>
                            <asp:FileUpload ID="FileUploadRecommenedAttachment" runat="server" Width="280px" />
                            <span style="color: Blue">
                                <br />
                                (Max Size 20 MB, Allowed file type for Attachment-doc,docx,jpg,jpeg,pdf)</span>
                            <asp:Label ID="lblFileUploadMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>

                        <td colspan="3">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">

                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="LocationDetails"
                                Style="height: 26px" />
                        </td>
                    </tr>

                    <%-- <tr><td colspan="3"> 
                        <asp:GridView ID="gvApplicationNameChange" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
                    HorizontalAlign="Center" Width="100%" 
                    DataKeyNames="AppCode,SN" ShowHeaderWhenEmpty="true">                     
                    <Columns>
                       
                        <asp:TemplateField HeaderText="Sr.No.">
                            <ItemTemplate>
                                <span>
                                    <%#Container.DataItemIndex + 1 %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                      
                        <asp:TemplateField HeaderText="Project Name">
                            <ItemTemplate>
                                <asp:Label ID="lblProjectName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ProjectName")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="Previous Project Name">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("PreviousProjectName")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ReasonToChange">
                            <ItemTemplate>
                                <asp:Label ID="lblReasonToChange" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ReasonToChange")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AttName">
                            <ItemTemplate>
                                <asp:Label ID="lblAttName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttName")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Creation Date">
                            <ItemTemplate>
                                <asp:Label ID="lblCreationDate" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreatedOnByExUC", "{0:dd/MM/yyyy}")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View File">
                            <ItemTemplate>                                
                                <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("AppCode") + "," + Eval("SN"))%>'                                   
                                   OnCommand="gvNameChange_RowCommand">View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                    </Columns>
                    <EmptyDataTemplate>
                        No Records exist.
                    </EmptyDataTemplate>
                </asp:GridView></td></tr>--%>
                </table>
            </td>

        </tr>
    </table>
</asp:Content>


