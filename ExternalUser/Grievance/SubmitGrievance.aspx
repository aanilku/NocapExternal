<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" CodeFile="SubmitGrievance.aspx.cs" Inherits="ExternalUser_Grievance_SubmitGrievance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../../Scripts/jquery-3.6.0.min.js"></script>




    <script type="text/javascript">


        function ddlTypeofGrievanceChange() {
            var ddlTypeofGrievance = document.getElementById("<%=ddlTypeofGrievance.ClientID%>");
            var getvalue = ddlTypeofGrievance.options[ddlTypeofGrievance.selectedIndex].value;

            if (getvalue == 2) {


                document.getElementById("<%=txtNOCNumber.ClientID%>").disabled = true;
                document.getElementById("<%=txtQuantum.ClientID%>").disabled = true;

            }
            else {



                document.getElementById("<%=txtNOCNumber.ClientID%>").disabled = false;
                document.getElementById("<%=txtQuantum.ClientID%>").disabled = false;

            }


        }


    </script>
    <style type="text/css">
        .auto-style1 {
            height: 45px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <table class="SubFormWOBG" width="100%" style="line-height: 25px">
        <tr>

            <td style="width: 20%;"></td>
            <td>


                <table class="SubFormWOBG" width="100%" style="line-height: 25px">

                    <tr>
                        <td colspan="2">
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                Submit Grievance
                            </div>
                        </td>
                    </tr>


                    <tr>

                        <td style="width: 200px;">State: </td>
                        <td>
                            <asp:DropDownList ID="ddlState" runat="server"
                                Width="200px">
                            </asp:DropDownList>
                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue=""
                                Display="Dynamic" ControlToValidate="ddlState" ForeColor="Red" ValidationGroup="SubmitGrievance">Required</asp:RequiredFieldValidator>--%>

                        </td>
                    </tr>
                    <tr>

                        <td>Type of Grievance: </td>
                        <td>
                            <asp:DropDownList ID="ddlTypeofGrievance" runat="server"
                                Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlTypeofGrievance_SelectedIndexChanged">
                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                <asp:ListItem Text="Related to NOC" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Other" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>

                        <td class="auto-style1">Have you Received NOC: </td>
                        <td class="auto-style1">

                            <asp:RadioButtonList ID="rbtnHaveYouReceivedNOC" runat="server" align="left"
                                RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbtnHaveYouReceivedNOC_SelectedIndexChanged">
                                <asp:ListItem Value="Y" Text="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="N" Text="No">No</asp:ListItem>
                            </asp:RadioButtonList>

                        </td>
                    </tr>

                    <tr>

                        <td>Enter NOC No: </td>
                        <td>
                            <asp:TextBox ID="txtNOCNumber" runat="server"></asp:TextBox>

                        </td>
                    </tr>

                    <tr>

                        <td>Quantum: ( KLD m3/day)</td>
                        <td>
                            <asp:TextBox ID="txtQuantum" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>

                        <td>Submitted To: </td>
                        <td>
                            <asp:RadioButtonList ID="rbtnSubmittedTo" runat="server" align="left"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="Region">Region</asp:ListItem>
                                <asp:ListItem Value="HQ">HQ</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>

                  <%--  Attachment Start--%>


<%--                    <tr>

                        <td>Attachment Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRelaxation" runat="server" MaxLength="50"
                                Width="200px"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="txtRelaxation"
                                 Display="Dynamic" ForeColor="Red" ErrorMessage="Required" ValidationGroup="VGSitePlan"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator runat="server" Display="Dynamic"
                                 ForeColor="Red" ControlToValidate="txtRelaxation" ValidationExpression="([a-z]|[A-Z]|[ ]|[-]|[,]|[.]|[/]|[_]|[0-9])*"
                                 ErrorMessage="Allow only Alphanumeric and Characters . , _ / -" ValidationGroup="VGSitePlan"></asp:RegularExpressionValidator>--%>
                        <%--</td>
                    </tr>
                    <tr>

                        <td>Upload Documents: <span class="Coumpulsory">*</span>
                        </td>

                        <td>
                            <asp:UpdatePanel ID="UpdatePanelSitePlan" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnUploadSitePlan" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:FileUpload ID="FileUploadSitePlan" runat="server" />
                                    <asp:Button ID="btnUploadSitePlan" runat="server" Text="Upload" OnClick="btnUploadSitePlan_Click"
                                        ValidationGroup="VGSitePlan" OnClientClick="javascript:AffidavitNonAvaShowWait();" />
                                    <asp:UpdateProgress ID="UpdateProgressSitePlan" runat="server" AssociatedUpdatePanelID="UpdatePanelSitePlan">
                                        <ProgressTemplate>
                                            <asp:Label ID="lblSitePlanWait" runat="server" BackColor="#507CD1" Font-Bold="True"
                                                ForeColor="White" Text="Please wait ... Uploading file"></asp:Label>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>--%>

                   <%-- <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvGrievanceAtt" runat="server" ShowHeaderWhenEmpty="true"
                                CssClass="SubFormWOBG" AutoGenerateColumns="False"
                                DataKeyNames="GrievanceCode,SN"
                                Width="100%" OnRowDeleting="gvAttachments_RowDeleting">
                              
                                <Columns>
                                    <asp:TemplateField HeaderText="SN">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Attachment Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttName" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttName")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttPath" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttPath")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server"
                                                CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("GrievanceCode") + "," + Eval("SN"))%>'
                                                OnCommand="ViewFile">View</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                     <%--<asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No Records Exist in Grievance Attachments.
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>--%> 



                     <%--  Attachment End--%>
                     <tr>
                         
                        <td>Upload Documents: <span class="Coumpulsory">*</span></td>

                        <td>
                            <asp:FileUpload ID="FileUploadRecommenedAttachment" runat="server" Width="280px" />
                            <span style="color: Blue">
                                <br />
                                (Max Size 20 MB, Allowed file type for Attachment-doc,docx,jpg,jpeg,pdf)</span>
                            <asp:Label ID="lblFileUploadMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    

                    <tr>

                        <td>Remark </td>
                        <td>

                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>

                        <td>
                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />

                            &nbsp;
                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
                        </td>
                    </tr>

                </table>


            </td>
        </tr>

    </table>
</asp:Content>

