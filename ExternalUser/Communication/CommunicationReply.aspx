<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" CodeFile="CommunicationReply.aspx.cs" Inherits="ExternalUser_Communication_CommunicationReply" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 400px;
            height: 350px;
        }



        .round1 {
            border: 1px solid;
            border-radius: 3px;
        }

        .style1 {
            width: 100%;
        }

        .style2 {
            width: 30%;
        }

        .style3 {
            width: 100%;
        }

        .auto-style3 {
            height: 29px;
        }

        .auto-style4 {
            width: 60%;
            height: 29px;
        }
    </style>
    <link type="text/css" rel="Stylesheet" href="../../css/LoginStyle.css" />
    <link type="text/css" rel="Stylesheet" href="../../css/CommonStyle.css" />
    <script type="text/javascript" src="../../Scripts/jquery-3.6.0.min.js"></script>

    <script type="text/javascript" src="../../Scripts/WaterMark.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $("[id*=txtReplyDescription], [id*=txtAttachmentDesc]").WaterMark();
        });

        function SetInline() {
            if (document.getElementById('<%= txtAttachmentDesc.ClientID %>').value.length > 0) {
                var x = document.getElementById('<%= UpdateProgressFileUpload.ClientID %>')
                x.style.display = 'inline';
            }
        }

    </script>

    <script type="text/javascript">
        function shrinkandgrow(input) {
            var displayIcon = "img" + input;
            if ($("#" + displayIcon).attr("src") == "grow.png") {
                $("#" + displayIcon).closest("tr")
			    .after("<tr><td></td><td colspan = '100%'>" + $("#" + input)
			    .html() + "</td></tr>");
                $("#" + displayIcon).attr("src", "shrink.png");
            } else {
                $("#" + displayIcon).closest("tr").next().remove();
                $("#" + displayIcon).attr("src", "grow.png");
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <AjaxControlkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </AjaxControlkit:ToolkitScriptManager>
    <table align="center" class="SubFormWOBG" width="100%" style="line-height: 20px">
        <tr>
            <td colspan="2">
                <div class="div_IndAppHeading" style="padding-left: 10px; text-align: center; font-size: 20px">
                    Communication Reply
                </div>
            </td>
        </tr>
        <tr>
            <td>Application Type:
            </td>
            <td>
                <asp:Label runat="server" ID="lblAppType"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Application Name:
            </td>
            <td>
                <asp:Label runat="server" ID="lblAppName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Application Purpose:
            </td>
            <td>
                <asp:Label runat="server" ID="lblAppPurpose"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Application Number:
            </td>
            <td>
                <asp:Label runat="server" ID="lblApplicationNumber"></asp:Label>

            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label runat="server" Text="Verification Communication Detail" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView Font-Bold="True" runat="server" ID="grdviewCommRequestVeri"
                    AutoGenerateColumns="False"
                    DataKeyNames="AppCode" CssClass="SubFormWOBG" Width="100%"
                    ShowHeaderWhenEmpty="True" OnRowDataBound="grdviewCommRequestVeri_RowDataBound"
                    OnRowCommand="grdviewCommRequestVeri_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnAddReplyVeri" runat="server" ForeColor="Green"
                                    CommandName="AppCode"
                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("AppCode") + "," + Eval("CommunicatReqSNumber")) %>'>Add Reply
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Communication Date">
                            <ItemTemplate>
                                <asp:Label ID="lblCommDate" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreateModifyByUser.CreatedOnByUC", "{0:dd/MM/yyyy}"))%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="lblCommDesc" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CommunicatedText"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Attachment">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hdnAttachment" Value='<%# System.Web.HttpUtility.HtmlEncode(Eval("AppCode")  + "," + Eval("CommunicatReqSNumber")+ "," + Eval("VerificationSN"))%>' />
                                <asp:GridView runat="server" BorderStyle="None" ShowHeader="false"
                                    ID="grdCommReqAttachmentVeri" AutoGenerateColumns="False"
                                    Visible="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnReqAttachmentVeri"
                                                    OnCommand="lbtnReqAttachmentVeri_Command" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'
                                                    CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("AppCode") + "," + Eval("CommunicatReqSNumber") + "," + Eval("AttachmentCode")+ "," + Eval("AttachmentCodeSNum"))%>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:LinkButton runat="server" ID="btnlNoFileAttachmentVeri" Text="No File Attached" Visible="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Closing Remark">
                            <ItemTemplate>
                                <asp:Label ID="lblCommCosingRemark" runat="server"
                                    Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ClosingRemark"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Is Close">
                            <ItemTemplate>
                                <asp:Label ID="lblCommIsCosingVeri" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("IsClose")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Requested By User">
                            <ItemTemplate>
                                <asp:Label ID="lblFromUser" Visible="false" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("FromIntUserCode"))%>'
                                    Style="font-weight: bold;"></asp:Label>
                                <asp:Label ID="lblCommRequestedUser" runat="server" Text=""></asp:Label>
                                <tr>
                                    <td></td>
                                    <td colspan="100%">
                                        <%--  <div style="display: inline;">--%>
                                        <asp:GridView ID="grdviewCommReplyVeri" Width="100%"
                                            OnRowDataBound="grdviewCommReplyVeri_RowDataBound"
                                            runat="server" AutoGenerateColumns="false"
                                            DataKeyNames="AppCode,CommunicatReqSNumber,CommunicatReplySNumber" CssClass="SubFormWOBG">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Reply Date" HeaderStyle-Width="21%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReplyDate" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreateModifySubmitByExtIntUser.CreatedOnByUC", "{0:dd/MM/yyyy}"))%>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reply Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReplyDesc" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CommunicatedText"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reply Attachment">
                                                    <ItemTemplate>
                                                        <asp:HiddenField runat="server" ID="hdnReplyAttachment" Value='<%# System.Web.HttpUtility.HtmlEncode(Eval("AppCode") +","+Eval("CommunicatReplySNumber") + "," + Eval("CommunicatReqSNumber"))%>' />
                                                        <asp:GridView runat="server" BorderStyle="None" ShowHeader="false"
                                                            ID="grdCommRepAttachmentVeri" AutoGenerateColumns="False"
                                                            Visible="false">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtnReplyAttachmentVeri" OnCommand="lbtnReplyAttachmentVeri_Command"
                                                                            runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'
                                                                            CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("AppCode") + "," + Eval("CommunicatReplySN") + "," + Eval("AttachmentCode")+ "," + Eval("AttachmentCodeSNum"))%>'></asp:LinkButton>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:LinkButton runat="server" ID="btnReplyNoFileAttachmentVeri" Text="No File Attached" Visible="false"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reply By User" HeaderStyle-Width="16%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFromExUser" Visible="false" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("FromExtUserCode"))%>'
                                                            Style="font-weight: bold;"></asp:Label>
                                                        <asp:Label ID="lblCommReplyUser" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <%--  </div>--%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label runat="server" Text="Evaluation Communication Detail" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView Font-Bold="true" runat="server" ID="grdviewCommRequestEva" Width="100%"
                    OnRowDataBound="grdviewCommRequestEva_RowDataBound"
                    OnRowCommand="grdviewCommRequestEva_RowCommand"
                    AutoGenerateColumns="false" DataKeyNames="AppCode" CssClass="SubFormWOBG"
                    ShowHeaderWhenEmpty="True">
                    <Columns>
                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnAddReplyEva" runat="server" ForeColor="Green"
                                    CommandName="AppCode"
                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("AppCode") + "," + Eval("CommunicatReqSNumber")) %>'>Add Reply
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Communication Date">
                            <ItemTemplate>
                                <asp:Label ID="lblCommDate" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreateModifyByUser.CreatedOnByUC", "{0:dd/MM/yyyy}"))%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="lblCommDesc" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CommunicatedText"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Attachment">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hdnAttachment" Value='<%# System.Web.HttpUtility.HtmlEncode(Eval("AppCode")  + "," + Eval("CommunicatReqSNumber")+ "," + Eval("EvaluationSN"))%>' />
                                <asp:GridView runat="server" BorderStyle="None" ShowHeader="false"
                                    ID="grdCommReqAttachmentEva" AutoGenerateColumns="False"
                                    Visible="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnAttachmentEva"
                                                    OnCommand="lbtnAttachmentEva_Command"
                                                    runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'
                                                    CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("AppCode") + "," + Eval("CommunicatReqSNumber") + "," + Eval("AttachmentCode")+ "," + Eval("AttachmentCodeSNum"))%>'></asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:LinkButton runat="server" ID="btnlNoFileAttachmentEva" Text="No File Attached" Visible="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Closing Remark">
                            <ItemTemplate>
                                <asp:Label ID="lblCommCosingRemark" runat="server"
                                    Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ClosingRemark"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Is Close">
                            <ItemTemplate>

                                <asp:Label ID="lblCommIsCosingEva" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("IsClose")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Requested By User">
                            <ItemTemplate>
                                <asp:Label ID="lblFromUser" Visible="false" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("FromIntUserCode"))%>'
                                    Style="font-weight: bold;"></asp:Label>
                                <asp:Label ID="lblCommRequestedUser" runat="server" Text=""></asp:Label>

                                <tr>
                                    <td></td>
                                    <td colspan="100%">
                                        <asp:GridView ID="grdviewCommReplyEva" Width="100%"
                                            OnRowDataBound="grdviewCommReplyEva_RowDataBound"
                                            runat="server" AutoGenerateColumns="false"
                                            DataKeyNames="AppCode,CommunicatReqSNumber,CommunicatReplySNumber" CssClass="SubFormWOBG">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Reply Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReplyDate" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreateModifySubmitByExtIntUser.CreatedOnByUC", "{0:dd/MM/yyyy}"))%>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reply Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReplyDesc" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CommunicatedText"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reply Attachment">
                                                    <ItemTemplate>
                                                        <asp:HiddenField runat="server" ID="hdnReplyAttachment" Value='<%# System.Web.HttpUtility.HtmlEncode(Eval("AppCode") +","+Eval("CommunicatReplySNumber") + "," + Eval("CommunicatReqSNumber"))%>' />
                                                        <asp:GridView runat="server" BorderStyle="None" ShowHeader="false"
                                                            ID="grdCommRepAttachmentEva" AutoGenerateColumns="False"
                                                            Visible="false">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtnReplyAttachmentEva"
                                                                            OnCommand="lbtnReplyAttachmentEva_Command"
                                                                            runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'
                                                                            CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("AppCode") + "," + Eval("CommunicatReplySN") + "," + Eval("AttachmentCode")+ "," + Eval("AttachmentCodeSNum"))%>'></asp:LinkButton>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:LinkButton runat="server" ID="btnReplyNoFileAttachmentEva" Text="No File Attached" Visible="false"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reply By User">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFromExUser" Visible="false" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("FromExtUserCode"))%>'
                                                            Style="font-weight: bold;"></asp:Label>
                                                        <asp:Label ID="lblCommReplyUser" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label runat="server" Text="Screening Committee Communication Detail" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView Font-Bold="True" runat="server" ID="grdviewCommRequestScree"
                    AutoGenerateColumns="False"
                    DataKeyNames="AppCode" CssClass="SubFormWOBG" Width="100%"
                    ShowHeaderWhenEmpty="True"
                    OnRowDataBound="grdviewCommRequestScree_RowDataBound"
                    OnRowCommand="grdviewCommRequestScree_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnAddReplyScree" runat="server" ForeColor="Green"
                                    CommandName="AppCode"
                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("AppCode") + "," + Eval("CommunicatReqSNumber")) %>'>Add Reply
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Communication Date">
                            <ItemTemplate>
                                <asp:Label ID="lblCommDate" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreateModifyByUser.CreatedOnByUC", "{0:dd/MM/yyyy}"))%>'>
                                </asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="lblCommDesc" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CommunicatedText"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Attachment">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hdnAttachment" Value='<%# System.Web.HttpUtility.HtmlEncode(Eval("AppCode")  + "," + Eval("CommunicatReqSNumber")+ "," + Eval("ScreningCommitteeSN"))%>' />
                                <asp:GridView runat="server" BorderStyle="None" ShowHeader="false"
                                    ID="grdCommReqAttachmentScree" AutoGenerateColumns="False"
                                    Visible="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnReqAttachmentScree"
                                                    OnCommand="lbtnReqAttachmentScree_Command" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'
                                                    CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("AppCode") + "," + Eval("CommunicatReqSNumber") + "," + Eval("AttachmentCode")+ "," + Eval("AttachmentCodeSNum"))%>'></asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:LinkButton runat="server" ID="btnlNoFileAttachmentVeri" Text="No File Attached" Visible="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Closing Remark">
                            <ItemTemplate>
                                <asp:Label ID="lblCommCosingRemark" runat="server"
                                    Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ClosingRemark"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Is Close">
                            <ItemTemplate>
                                <asp:Label ID="lblCommIsCosingScree" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("IsClose")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Requested By User">
                            <ItemTemplate>
                                <asp:Label ID="lblFromUser" Visible="false" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("FromIntUserCode"))%>'
                                    Style="font-weight: bold;"></asp:Label>
                                <asp:Label ID="lblCommRequestedUser" runat="server" Text=""></asp:Label>

                                <tr>
                                    <td></td>
                                    <td colspan="100%">
                                        <%--  <div style="display: inline;">--%>
                                        <asp:GridView ID="grdviewCommReplyScree" Width="100%"
                                            OnRowDataBound="grdviewCommReplyScree_RowDataBound"
                                            runat="server" AutoGenerateColumns="false"
                                            DataKeyNames="AppCode,CommunicatReqSNumber,CommunicatReplySNumber" CssClass="SubFormWOBG">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Reply Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReplyDate" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CreateModifySubmitByExtIntUser.CreatedOnByUC", "{0:dd/MM/yyyy}"))%>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reply Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReplyDesc" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("CommunicatedText"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reply Attachment">
                                                    <ItemTemplate>
                                                        <asp:HiddenField runat="server" ID="hdnReplyAttachment" Value='<%# System.Web.HttpUtility.HtmlEncode(Eval("AppCode") +","+Eval("CommunicatReplySNumber") + "," + Eval("CommunicatReqSNumber"))%>' />
                                                        <asp:GridView runat="server" BorderStyle="None" ShowHeader="false"
                                                            ID="grdCommRepAttachmentScree" AutoGenerateColumns="False"
                                                            Visible="false">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtnReplyAttachmentScree" OnCommand="lbtnReplyAttachmentScree_Command"
                                                                            runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName"))%>'
                                                                            CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("AppCode") + "," + Eval("CommunicatReplySN") + "," + Eval("AttachmentCode")+ "," + Eval("AttachmentCodeSNum"))%>'></asp:LinkButton>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:LinkButton runat="server" ID="btnReplyNoFileAttachmentScree" Text="No File Attached" Visible="false"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reply Reply User">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFromExUser" Visible="false" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("FromExtUserCode"))%>'
                                                            Style="font-weight: bold;"></asp:Label>
                                                        <asp:Label ID="lblCommReplyUser" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <%--  </div>--%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </td>
        </tr>
        <tr>

            <td style="text-align: right" colspan="2">
                <a runat="server" href="~/ExternalUser/ApplicantHome.aspx">Go Back</a>
            </td>

        </tr>

        <tr>
            <td colspan="2">
                <asp:LinkButton runat="server" ID="lnkbutton"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <%--    Style="display: none"--%>
                <asp:Panel runat="server" ID="Panel1" class="style2">
                    <table align="center" cellpadding="0" cellspacing="0" class="style2">
                        <tr>
                            <td>
                                <div>
                                    <table align="center" cellpadding="0" cellspacing="0" class="style1">
                                        <tr>
                                            <td colspan="2">
                                                <div class="login_header" style="width: 80%; padding-left: 0px">
                                                    <table class="style3">
                                                        <tr>
                                                            <td>&nbsp;&nbsp;
                                                                <asp:Label runat="server" Text="Add Reply"
                                                                    CssClass="lbl"></asp:Label>
                                                            </td>
                                                            <td align="right">

                                                                <asp:Button runat="server" Text="x" Font-Size="Large" ToolTip="Close" ID="BtnCrosClose" ForeColor="WhiteSmoke" BorderStyle="None" OnClick="BtnCrosClose_Click" BackColor="Transparent" />
                                                                <%-- <asp:ImageButton runat="server" ID="ImgBtnClose" ImageUrl="~/Images/delete.jpg" OnClick="ImgBtnClose_Click" />
                                                                   
                                                                --%> 
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="formbody_modelpopup" style="width: 80%">
                                        <table align="center" cellpadding="0" cellspacing="0" class="style1">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="width: 70%">
                                                    <span class="Coumpulsory">*</span>
                                                    <asp:Label runat="server" Text=" All Fields are Compulsary"
                                                        CssClass="lblCompulsary"></asp:Label>

                                                    <br />
                                                    <asp:Label runat="server" CssClass="lblCompulsary" Text="Maximum Size of each Attachment Allowed- 5MB"></asp:Label>
                                                    <br />
                                                    <asp:Label runat="server" CssClass="lblCompulsary" Text=" Allowed file type for Attachment- txt, doc, docx, jpg, jpeg, pdf"></asp:Label>

                                                </td>
                                            </tr>
                                            <tr style="height: 50px">
                                                <td>&nbsp;&nbsp; <span class="Coumpulsory">*</span>
                                                    <asp:Label runat="server" CssClass="lbl" Text=" Reply Description:"></asp:Label>
                                                </td>
                                                <td style="width: 60%">
                                                    <asp:TextBox Width="50%" Height="50px" runat="server"
                                                        ID="txtReplyDescription" ToolTip="Communication Details"
                                                        ValidationGroup="ReplyDes" TextMode="MultiLine" CssClass="round1"></asp:TextBox>
                                                    <br />
                                                    <asp:RequiredFieldValidator EnableClientScript="true" ID="RequiredFieldValidator4"
                                                        runat="server" ErrorMessage="Required"
                                                        ForeColor="Red" Display="Dynamic" ControlToValidate="txtReplyDescription"
                                                        ValidationGroup="ReplyDes"></asp:RequiredFieldValidator>
                                                    <%-- <asp:RegularExpressionValidator ID="revtxtActionComment" runat="server" Display="Dynamic"
                                                        ForeColor="Red" ControlToValidate="txtReplyDescription" ValidationGroup="ReplyDes"></asp:RegularExpressionValidator>
                                                    <asp:RegularExpressionValidator ID="revtxtActionCommentLength" runat="server" Display="Dynamic"
                                                        ForeColor="Red" ControlToValidate="txtReplyDescription" ValidationGroup="ReplyDes"></asp:RegularExpressionValidator>
                                                    --%>
                                                </td>
                                            </tr>

                                            <%-- <tr>
                                                <td colspan="2">

                                                    <asp:Panel runat="server" ID="Panel2" class="style2" GroupingText="File Upload">--%>


                                            <tr>
                                                <td>&nbsp;&nbsp; &nbsp;&nbsp; 
                                                    <asp:Label runat="server" CssClass="lbl" Text="Attachment File Name:"></asp:Label>
                                                </td>
                                                <td style="width: 60%">
                                                    <asp:TextBox ID="txtAttachmentDesc" Width="50%" runat="server"
                                                        CssClass="round1" ToolTip="Attachment File Name"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style3">&nbsp;&nbsp; 
                                                    <asp:Label runat="server" CssClass="lbl" Text=" Upload File:"></asp:Label>
                                                </td>
                                                <td class="auto-style4">

                                                    <asp:FileUpload ID="FileUploadCommunicatioReply" runat="server" />


                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                                <td style="width: 60%">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                                <td style="width: 60%">
                                                    <table>

                                                        <tr>
                                                            <td align="left">
                                                                <asp:UpdatePanel ID="UpdatePanelFileUpload" runat="server">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnSave" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:Button runat="server" Height="37px" OnClientClick="javascript:SetInline();"
                                                                            ValidationGroup="ReplyDes" CssClass="ButtonStyle"
                                                                            Width="72px" ID="btnSave" Text="Submit"
                                                                            OnClick="btnSave_Click" />
                                                                        <asp:UpdateProgress ID="UpdateProgressFileUpload" runat="server"
                                                                            AssociatedUpdatePanelID="UpdatePanelFileUpload">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="lblFileUploadWait" runat="server" BackColor="#507CD1" Font-Bold="True"
                                                                                    ForeColor="White" Text="Please wait ... Uploading file"></asp:Label>
                                                                            </ProgressTemplate>
                                                                        </asp:UpdateProgress>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Button runat="server" Height="37px"
                                                                    CssClass="ButtonStyle"
                                                                    ID="btnClose" Width="72px"
                                                                    Text="Close" />

                                                            </td>

                                                        </tr>
                                                    </table>
                                                </td>


                                            </tr>

                                            <%--<tr>
                                                <td>&nbsp;
                                                </td>
                                                <td style="width: 60%">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;&nbsp; &nbsp;&nbsp; 
                                                    <asp:Label runat="server" CssClass="lbl" Text="Attachment File Name 2:"></asp:Label>
                                                </td>
                                                <td style="width: 60%">
                                                    <asp:TextBox ID="txtAttachmentDesc2" Width="50%" runat="server"
                                                        CssClass="round1" ToolTip="Attachment File Name 2"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;&nbsp; 
                                                    <asp:Label runat="server" CssClass="lbl" Text="Upload File 2:"></asp:Label>
                                                </td>
                                                <td style="width: 60%">
                                                    <asp:FileUpload ID="FileUploadReferralLetter2" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                                <td style="width: 60%">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;&nbsp; &nbsp;&nbsp; 
                                                    <asp:Label runat="server" CssClass="lbl" Text="Attachment File Name 3:"></asp:Label>
                                                </td>
                                                <td style="width: 60%">
                                                    <asp:TextBox ID="txtAttachmentDesc3" Width="50%" runat="server"
                                                        CssClass="round1" ToolTip="Attachment File Name 3"></asp:TextBox>
                                                </td>
                                            </tr>
                                              <tr>
                                                <td>&nbsp;&nbsp; 
                                                    <asp:Label runat="server" CssClass="lbl" Text="Upload File 3:"></asp:Label>
                                                </td>
                                                <td style="width: 60%">

                                                    <asp:FileUpload ID="FileUploadReferralLetter3" runat="server" />

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                                <td style="width: 60%">&nbsp;
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>&nbsp;&nbsp; &nbsp;&nbsp; 
                                                    <asp:Label runat="server" CssClass="lbl" Text="Attachment File Name 4:"></asp:Label>
                                                </td>
                                                <td style="width: 60%">
                                                    <asp:TextBox ID="txtAttachmentDesc4" Width="50%" runat="server"
                                                        CssClass="round1" ToolTip="Attachment File Name 4"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;&nbsp; 
                                                    <asp:Label runat="server" CssClass="lbl" Text="Upload File 4:"></asp:Label>
                                                </td>
                                                <td style="width: 60%">

                                                    <asp:FileUpload ID="FileUploadReferralLetter4" runat="server" />


                                                </td>
                                            </tr>
                                              <tr>
                                                <td>&nbsp;
                                                </td>
                                                <td style="width: 60%">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;&nbsp; &nbsp;&nbsp; 
                                                    <asp:Label runat="server" CssClass="lbl" Text="Attachment File Name 5:"></asp:Label>
                                                </td>
                                                <td style="width: 60%">
                                                    <asp:TextBox ID="txtAttachmentDesc5" Width="50%" runat="server"
                                                        CssClass="round1" ToolTip="Attachment File Name 5"></asp:TextBox>
                                                </td>
                                            </tr>
                                        <tr>
                                                <td>&nbsp;&nbsp; 
                                                    <asp:Label runat="server" CssClass="lbl" Text="Upload File 5:"></asp:Label>
                                                </td>
                                                <td style="width: 60%">

                                                    <asp:FileUpload ID="FileUploadReferralLetter5" runat="server" />


                                                </td>
                                            </tr>--%>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>

        <tr>
            <td colspan="2">
                <AjaxControlkit:ModalPopupExtender ID="ModalPopupExtenderLogin" runat="server"
                    TargetControlID="lnkbutton"
                    PopupControlID="Panel1"
                    BackgroundCssClass="modalBackground"
                    DropShadow="true"
                    CancelControlID="btnClose" />
            </td>
        </tr>
        <%-- <tr>
            <div id="myModal" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h4 class="modal-title">Article for C# Corner</h4>
                        </div>
                        <div class="modal-body" style="overflow-y: scroll; max-height: 85%; margin-top: 50px; margin-bottom: 50px;">
                            <asp:Label ID="Label1" runat="server" ClientIDMode="Static"></asp:Label>
                            <asp:GridView ID="GridView2" runat="server"></asp:GridView>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </tr>--%>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblIntUserCode" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblCommunicationReqSNumber" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                <asp:Label ID="lblPageTitleFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                <asp:Label ID="lblApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

