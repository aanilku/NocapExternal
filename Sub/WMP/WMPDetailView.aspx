<%@ Page Title="" Language="C#" MasterPageFile="~/Sub/SubReportMaster.master" AutoEventWireup="true" CodeFile="WMPDetailView.aspx.cs" Inherits="Sub_WMP_WMPDetailView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
            window.setTimeout("document.forms[0].target='';", 500);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <table class="SubFormWOBG" style="line-height: 25px; width: 100%">
        <tr>
            <td colspan="2">
                <div class="div_IndAppHeading" style="padding-left: 10px; text-align: center">
                    Water Management Plan (WMP) - 2020
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 40%">State:</td>
            <td>
                <asp:DropDownList runat="server" ID="ddlState" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td style="width: 40%">District:</td>
            <td>
                <asp:DropDownList runat="server" ID="ddlDistrict" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>


            </td>
        </tr>
        <tr>
            <td style="width: 40%">Assement Unit:</td>
            <td>
                <asp:DropDownList runat="server" ID="ddlSubdistrict" OnSelectedIndexChanged="ddlSubdistrict_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>


            </td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp; Captcha Code:
            </td>
            <td>
                <img src="../../Handler.ashx" />&nbsp;
                                <asp:ImageButton ID="imgBtnCaptchaRefresh" runat="server" Height="24px" ImageUrl="~/Images/refresh.png"
                                    Width="29px" OnClick="imgBtnCaptchaRefresh_Click" /><br />
            </td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp; Enter Code:
            </td>
            <td>
                <asp:TextBox ID="txtCaptchaCode" runat="server" ToolTip="Enter Code"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfv" ControlToValidate="txtCaptchaCode" ValidationGroup="Captcha"
                    ErrorMessage="Please Enter Valid Captcha Code" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td><asp:Label runat="server" Text="Note:-" Font-Bold="true"></asp:Label>&nbsp;<asp:Label runat="server" ForeColor="Black" Text="State,District and Assement Unit Black Color shows-No WMP Assessment Unit"></asp:Label>
            </td>
            <td></td>
        </tr>

        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Button runat="server" ID="btnShow" Text="Show" OnClick="btnShow_Click" ValidationGroup="Captcha" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Text="Number of Assessment Unit having WMP" ForeColor="Green"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblCount" Font-Bold="true" ForeColor="Green"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvWMP" runat="server"
                    CssClass="SubFormWOBG" align="center" AutoGenerateColumns="False" AllowSorting="true"
                    DataKeyNames="StateCode,DistrictCode,SubDistrictCode,AttCodeSN"
                    EmptyDataText="No Record Found" Width="100%" AllowPaging="true" OnPageIndexChanging="gvWMP_PageIndexChanging"
                    OnRowDataBound="gvWMP_RowDataBound">
                     <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                    <PagerSettings Mode="Numeric" PageButtonCount="10" Position="TopAndBottom" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="State Name">
                            <ItemTemplate>
                                <asp:Label ID="lblStateName" runat="server"></asp:Label>
                                <asp:HiddenField ID="lblStateCode" runat="server" Value='<%#System.Web.HttpUtility.HtmlEncode(Eval("StateCode"))%>'></asp:HiddenField>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="District Name">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDistrictName"></asp:Label>
                                <asp:HiddenField runat="server" ID="lblDistrictCode" Value='<%#System.Web.HttpUtility.HtmlEncode(Eval("DistrictCode")) %>'></asp:HiddenField>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Assessment Unit">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSubDistrictName"></asp:Label>

                                <asp:HiddenField runat="server" ID="lblSubDistrictCode" Value='<%#System.Web.HttpUtility.HtmlEncode(Eval("SubDistrictCode")) %>'></asp:HiddenField>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View File">
                            <ItemTemplate>
                                <%--<asp:LinkButton runat="server"
                                    CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("StateCode") + "," + Eval("DistrictCode")+ ","+ Eval("SubDistrictCode") + "," + Eval("AttCodeSN"))%>'
                                    OnCommand="DownloadOrViewFile"
                                    OnClientClick="SetTarget();">View</asp:LinkButton>
                                <br />/--%>
                                <asp:LinkButton ID="lbtnDownloadBharatKoshRecieptViewFile" Text="Download" runat="server"
                                    CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("StateCode") + "," + Eval("DistrictCode")+ ","+ Eval("SubDistrictCode") + "," + Eval("AttCodeSN"))%>'
                                    OnCommand="DownloadOrViewFile"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblSortField" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <%-- <tr>
            <td colspan="2">
                <asp:Repeater ID="rptUsers" runat="server" OnItemDataBound="rptUsers_ItemDataBound">
                    <HeaderTemplate>
                        <table border="1">
                            <tr>
                                <th scope="col">S No. 
                                </th>
                                <th scope="col">State Name 
                                </th>
                                <th scope="col">District Name 
                                </th>
                                <th scope="col">Assessment Unit
                                </th>
                                <th scope="col">View File
                                </th>

                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="border: inherit;"><span>
                                <%#Container.ItemIndex + 1 %>
                            </span></td>
                            <td style="border: inherit;">
                                <asp:Label ID="lblStateName" runat="server"
                                    Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("StateCode")) %>'>
                   
                                </asp:Label></td>
                            <td style="border: inherit;">
                                <asp:Label ID="lblDistrictName" runat="server"
                                    Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("DistrictCode")) %>'>
                   
                                </asp:Label></td>
                            <td style="border: inherit;">
                                <asp:Label ID="lblSubDistrictName" runat="server"
                                    Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("SubDistrictCode")) %>'>
                   
                                </asp:Label></td>
                            <td style="border: inherit;">
                                <asp:LinkButton ID="lnkUser" runat="server"
                                    Text="View"
                                    CommandArgument='<%# System.Web.HttpUtility.HtmlEncode(Eval("StateCode") + "," + Eval("DistrictCode")+ ","+ Eval("SubDistrictCode") + "," + Eval("AttCodeSN"))%>'
                                    OnCommand="DownloadOrViewFile" OnClientClick="SetTarget();">
                   
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lblEmptyData"
                                    Text="There is no assessment unit" runat="server" Visible="false">
                                </asp:Label>
                            </td>
                        </tr>
                        </table>  
                    </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>--%>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>

            </td>
        </tr>

    </table>
</asp:Content>

