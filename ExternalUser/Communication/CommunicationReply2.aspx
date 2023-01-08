<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" CodeFile="CommunicationReply2.aspx.cs" Inherits="ExternalUser_Communication_CommunicationReply2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../../Scripts/jquery-3.6.0.min.js"></script>
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
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="AppCode"
        OnRowDataBound="GridView1_OnRowDataBound" HeaderStyle-BackColor="#A52A2A" HeaderStyle-ForeColor="White">
        <Columns>
            <asp:TemplateField ItemStyle-Width="20px">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnAddReply" runat="server"
                        CommandName="AppCode"
                        CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("AppCode") + "," + Eval("CommunicatReqSNumber")) %>'>Add Reply

                    </asp:LinkButton>
                    <asp:HiddenField runat="server" ID="hdnAttachment" Value='<%# System.Web.HttpUtility.HtmlEncode(Eval("AppCode")  + "," + Eval("CommunicatReqSNumber")+ "," + Eval("VerificationSN"))%>' />
                    <a href="JavaScript:shrinkandgrow('div<%# Eval("AppCode") %>');" >
                        <img alt="Details" id="imgdiv<%# Eval("AppCode") %>" style="display:none" src="grow.png" />
                    </a>
                    <div id="div<%# Eval("AppCode") %>" style="display: inline;">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false"
                            DataKeyNames="AppCoe"
                            HeaderStyle-BackColor="#FFA500" HeaderStyle-ForeColor="White">
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

                            </Columns>
                        </asp:GridView>
                    </div>
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
        </Columns>
    </asp:GridView>
</asp:Content>

