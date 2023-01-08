<%@ Page Title="" Language="C#" MasterPageFile="~/Sub/SubReportMaster.master" AutoEventWireup="true"
    CodeFile="NOCIssusedLetterToExtUserPage.aspx.cs" Inherits="Sub_Report_NOCIssuedLetter_NOCIssusedLetterToExtUserPage" %>

<%@ Register Src="~/AscxControl/ExternalUserLeftMenu.ascx" TagName="ExternalUserLeftMenu"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:HiddenField ID="hidCSRF" runat="server" Value="" />--%>
    <asp:HiddenField ID="hidCSRFs" runat="server" Value="" />
    <table align="center" cellpadding="0" cellspacing="0" class="SubFormWOBG" width="100%">
        <tr>
            <td style="width: 200px">
                <uc1:ExternalUserLeftMenu ID="ExternalUserLeftMenu2" runat="server" />
            </td>
            <td>
                <div>
                    <table width="100%" class="SubFormWOBG" style="line-height: 25px">
                        <tr>
                            <th colspan="6">
                                <div class="div_IndAppHeading" style="padding-left: 10px; font-size: 18px;">
                                    List of Issued NOC - Online
                                </div>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="2">&nbsp;&nbsp;Project Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtProjectType" runat="server" Style="width: 150px;"
                                    MaxLength="100"></asp:TextBox>

                                <asp:RegularExpressionValidator ID="regExpValExtUserName0" runat="server" ErrorMessage="Please Enter only Alphabets and Numeric value."
                                    ForeColor="Red" ValidationExpression="([a-z]|[A-Z]|[0-9]|[ ]|[-]|[_])*" ControlToValidate="txtProjectType"
                                    Display="Dynamic" ValidationGroup="Captcha"></asp:RegularExpressionValidator>
                            </td>

                            <td colspan="2">&nbsp;&nbsp;Application Number:
                            </td>
                            <td>
                                <asp:TextBox ID="txtApplicationNo" runat="server" Width="150px"></asp:TextBox>

                                <asp:RegularExpressionValidator ID="revtxtApplicationNoNew" runat="server" ErrorMessage="Please Enter only Alphabets,Numeric,-,/."
                                    ForeColor="Red" ValidationExpression="([a-z]|[A-Z]|[0-9]|[ ]|[-]|[/])*" ControlToValidate="txtApplicationNo"
                                    Display="Dynamic" ValidationGroup="Captcha"></asp:RegularExpressionValidator>
                            </td>


                        </tr>
                        <tr>
                            <td colspan="2">&nbsp;&nbsp;State:
                            </td>
                            <td>
                                <asp:DropDownList Width="200px" AutoPostBack="true" runat="server" ID="ddlState"
                                    OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>

                            <td colspan="2">&nbsp;&nbsp;District:
                            </td>
                            <td>
                                <asp:DropDownList Width="200px" runat="server" ID="ddlDistrict" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>


                        </tr>
                        <tr>
                            <td colspan="2">&nbsp;&nbsp;Sub-District: </td>
                            <td>
                                <asp:DropDownList Width="200px" runat="server" ID="ddlSubDistrict">
                                </asp:DropDownList>
                            </td>

                           <td style="border-right: 0px solid red;" colspan="2">Issue Letter Type :
                            </td>
                            <td style="border-left: 0px solid red;">
                                <asp:DropDownList ID="ddlIssueLetterType" runat="server" Width="150px" AutoPostBack="false">
                                </asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td colspan="2">&nbsp;&nbsp; Application Type:</td>
                            <td>
                                <asp:DropDownList ID="ddlApplicationType" runat="server" Width="150px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlApplicationType_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td colspan="2">Application Type Category:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlApplicationTypeCat" runat="server" Style="width: 150px;"
                                    AutoPostBack="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <%--<tr>
                            <td colspan="2">&nbsp;&nbsp; Applied Area Type:</td>
                            <td>
                                <asp:DropDownList ID="ddlAreaType" runat="server" Width="150px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlAreaType_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td colspan="2">Applied Area Type Category:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAreaTypeCat" runat="server" Style="width: 150px;"
                                    AutoPostBack="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">&nbsp;&nbsp; Present Area Type:</td>
                            <td>
                                <asp:DropDownList ID="ddlPresentAreaType" runat="server" Width="150px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlPresentAreaType_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td colspan="2">Present Area Type Category:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPresentAreaTypeCat" runat="server" Style="width: 150px;"
                                    AutoPostBack="false">
                                </asp:DropDownList>
                            </td>
                        </tr>--%>
                        <tr>
                            <td colspan="2">&nbsp;&nbsp; Captcha Code:
                            </td>
                            <td colspan="4">
                                <img src="../../../Handler.ashx" />&nbsp;
                                <asp:ImageButton ID="imgBtnCaptchaRefresh" runat="server" Height="24px" ImageUrl="~/Images/refresh.png"
                                    Width="29px" OnClick="imgBtnCaptchaRefresh_Click" /><br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">&nbsp;&nbsp; Enter Code:
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtCaptchaCode" runat="server" ToolTip="Enter Code"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfv" ControlToValidate="txtCaptchaCode" ValidationGroup="Captcha"
                                    ErrorMessage="Please Enter Valid Captcha Code" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="text-align: center">
                                <asp:Button ID="btnShowRecord" runat="server" Text="Show Record" OnClick="btnShowRecord_Click" ValidationGroup="Captcha" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:GridView ID="gvNOCIssuedLetter" runat="server" AutoGenerateColumns="False" Width="100%"
                                    OnRowDataBound="gvNOCIssuedLetter_RowDataBound" AllowPaging="True" PageSize="50"
                                    OnPageIndexChanging="gvNOCIssuedLetter_PageIndexChanging" EmptyDataText="There is no record">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No.">
                                            <ItemTemplate>
                                                <span>
                                                    <%#Container.DataItemIndex + 1 %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="App Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAppCode" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AppCode"))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type of Project">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTypeofProject" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("TypeofProject"))%>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Renewal">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnLinkDepth" runat="server" Value='<%# System.Web.HttpUtility.HtmlEncode(Eval("LinkDepth").ToString())%>' />
                                                <asp:Label ID="lblAppPurposecode" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectName" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("ProjectName"))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Application Number">
                                            <ItemTemplate>
                                                <asp:Label ID="lblApplicationNumber" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("ApplicationNumber"))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectAddress" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("ProjectAddress"))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date of generation of NOC">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDateofgenerationofNOC" runat="server" Text='<%# Convert.ToDateTime( Eval("DateofgenerationofNOC")).ToShortDateString()%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ground water Withdrawal m3/Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGroundwaterWithdrawal" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("GroundwaterWithdrawal"))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Validity Start Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblValidityStartDate" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Convert.ToDateTime(Eval("ValidityStartDate")).ToShortDateString())%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Validity End Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblValidityEndDate" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Convert.ToDateTime(Eval("ValidityEndDate")).ToShortDateString())%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Has Attachment" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="AttPath" runat="server" Text='<%#Eval("AttPath") %>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Has Attachment" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="ScanAttPath" runat="server" Text='<%#Eval("ScanAttPath") %>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Scan Letter">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnScanDownload" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ScanAttPath"))!=""?"Download":"" %>'
                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("AppCode")) %>' OnClick="lbtnScanDownload_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Digital Signed Letter">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDownload" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttPath"))!=""?"Download":"" %>'
                                                    CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("AppCode")) %>' OnClick="lbtnDownload_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>

                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
