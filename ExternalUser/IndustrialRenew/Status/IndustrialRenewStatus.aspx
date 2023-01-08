<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    AutoEventWireup="true" CodeFile="IndustrialRenewStatus.aspx.cs" Inherits="ExternalUser_IndustrialRenew_Status_IndustrialRenewStatus" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ PreviousPageType VirtualPath="../../ApplicantHome.aspx" %>--%>
<%@ Register Src="../../../AscxControl/ExternalUserLeftMenu.ascx" TagName="ExternalUserLeftMenu"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .ModalPopupBG
        {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
        .PresentBackPopup
        {
            min-width: 500px;
            min-height: 250px;
            background: white;
        }
        .PopupBody
        {
        }
        .Initial
        {
            display: block;
            padding: 4px 18px 4px 18px;
            float: left; /* background: url("../Images/InitialImage.png") no-repeat right top;*/
            background-color: #EAEEF2;
            color: Black;
            font-weight: bold;
        }
        .Initial:hover
        {
            color: White; /* background: url("../Images/SelectedButton.png") no-repeat right top;*/
            background-color: #094E7F;
        }
        .Clicked
        {
            float: left;
            display: block; /*background: url("../Images/SelectedButton.png") no-repeat right top;*/
            background-color: #094E7F;
            padding: 4px 18px 4px 18px;
            color: Black;
            font-weight: bold;
            color: White;
        }
        .SubmittedApp
        {
            font-family: Arial;
            font-size: 14px;
            height: 20px;
            background-color: #094E7F;
            color: White;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
       <asp:HiddenField ID="hidCSRFs" runat="server" Value="" />
    <div style="display: none">
        <asp:Button ID="Button1" runat="server" />
    </div>
    <asp:ModalPopupExtender ID="ModalPopupExtender" runat="server" CancelControlID="btnCancel"
        TargetControlID="Button1" PopupControlID="Panel1" PopupDragHandleControlID="PopupHeader"
        Drag="true" BackgroundCssClass="ModalPopupBG">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" Style="display: none" runat="server">
        <div class="PresentBackPopup">
            <div class="PopupHeader" id="PopupHeader">
            </div>
            <div style="margin-left: 28px;">
                <p style="height: 1px;">
                </p>
                <b>Presentation Gist</b>
                <p>
                </p>
                <asp:TextBox ID="txtReqPresentGistComm" runat="server" TextMode="MultiLine" Rows="10"
                    Enabled="false" Columns="60"></asp:TextBox>
                <br />
                <p>
                </p>
                <div style="margin-left: 170px;">
                    <input id="btnCancel" type="button" value="Close" /></div>
            </div>
        </div>
    </asp:Panel>
    <table align="center" class="SubFormWOBG" style="width: 100%; line-height: 25px">
        <tr>
            <td style="width: 200px">
                <uc1:externaluserleftmenu id="ExternalUserLeftMenu1" runat="server" />
            </td>
            <td style="vertical-align: top">
                <table width="100%" class="SubFormWOBG">
                    <tr>
                        <td colspan="2">
                            <div class="div_IndAppHeading" style="width: 100%; height: 23px; line-height: 22px;
                                padding-left: 10px">
                                <asp:Label ID="Label3" runat="server" ForeColor="White" Font-Bold="True" Text="Application Status"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr style="visibility: hidden;">
                        <td style="width: 150px">
                            Application Code :
                        </td>
                        <td style="width: 550px">
                            <asp:Label ID="lblApplicationCode" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblApplicationRenewCode" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Application No :</strong>
                        </td>
                        <td>
                            <asp:Label ID="lblApplicationNo" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Receive Date :</strong>
                        </td>
                        <td>
                            <asp:Label ID="lblReceiveDate" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Name of Industry :</strong>
                        </td>
                        <td>
                            <asp:Label ID="lblIndustoryName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: 700">
                            Application Processing Fee :
                        </td>
                        <td>
                            <asp:Label ID="lblProcessingFee" runat="server"></asp:Label>
                            &nbsp;<asp:Label ID="lblProcessingFeeSubmitted" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: 700">Ground Water Abstraction/Restoration Charge :
                        </td>
                        <td>
                            <table width="100%" class="SubFormWOBG">
                                <tr>
                                    <td> Ground Water Quality Approved</td>
                                    <td><asp:Label ID="lblWaterQualityTypeApproved" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Ground Water Charge Required:</td>
                                    <td><asp:Label ID="lblWaterChargeReqFinally" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Charge:</td>
                                    <td>
                                        <asp:Label ID="lblAbsRestCharge" runat="server"></asp:Label>
                                        &nbsp;<asp:Label ID="lblAbsRestChargeSubmitted" runat="server"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td>Arear:</td>
                                    <td>
                                        <asp:Label ID="lblAbsRestArear" runat="server"></asp:Label>
                                        &nbsp;<asp:Label ID="lblAbsRestArearSubmitted" runat="server"></asp:Label></td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                    <tr runat="server" id="RowFinalStatus">
                        <td style="font-weight: 700">
                            Final Status
                        </td>
                        <td>
                            <asp:Label ID="lblLatesStatus" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="RowCurrentStage">
                        <td style="font-weight: 700">
                            Current Stage :
                        </td>
                        <td>
                            <asp:Label ID="lblCurrentStage" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="RowCurrentStatus">
                        <td>
                            <strong>Current Status :</strong>
                        </td>
                        <td>
                            <%--<asp:Label ID="lblCurrentStatus" runat="server" Text=""></asp:Label><asp:Label ID="lblFinalStatus"
                                runat="server" Text=""></asp:Label>--%>
                            <asp:Label ID="lblLatesStatus1" runat="server"></asp:Label>
                            <asp:Label ID="lblPresentReq" Font-Bold="true" Visible="false" ForeColor="Red" runat="server"
                                Text="<br />Presentation Required"></asp:Label>
                            <asp:Label ID="lblPresentDateSche" Font-Bold="true" ForeColor="Red" runat="server"
                                Text=""></asp:Label>
                            <asp:Label ID="lblPresentDateTimeInHours" Font-Bold="true" ForeColor="Red" runat="server"
                                Text=""></asp:Label>
                            <asp:Label ID="lblPresentAddress" runat="server" Visible="false" Font-Bold="true"
                                ForeColor="Red" Text="<br />Address :<br/>"></asp:Label>
                            <asp:TextBox TextMode="MultiLine" ID="txtPresentAddress" Visible="false" Width="550px"
                                Height="90px" Enabled="false" runat="server"></asp:TextBox>
                            <asp:Label ID="lblReqPresentGist" runat="server" Font-Bold="true" ForeColor="Red"
                                Visible="false" Text="<br />Presentation Gist :<br />"></asp:Label>
                            <asp:TextBox TextMode="MultiLine" ID="txtReqPresentGist" Visible="false" Width="550px"
                                Height="90px" Enabled="false" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <%--<tr>
                                    <td>Current User :</td>
                                    <td><asp:Label ID="lblCurrentUser" runat="server" Text=""></asp:Label></td>
                                </tr>--%>
                    <tr runat="server" id="RowAddress">
                        <td style="vertical-align: top">
                            <strong>Address : </strong>
                        </td>
                        <td>
                            <%--<asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>--%><asp:TextBox
                                TextMode="MultiLine" ID="txtAddress" Width="550px" Height="90px" Enabled="false"
                                runat="server"></asp:TextBox>
                            <%--<asp:Label ID="lblState" runat="server" Text=""></asp:Label>
                                    
                                    <br />
                                    <asp:Label ID="lblDistrict" runat="server" Text=""></asp:Label>
                                    <br />
                                    <asp:Label ID="lblSubDistrict" runat="server" Text=""></asp:Label>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblSerialNo" runat="server" Text="" Visible="false" Enabled="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvMainGrid" runat="server" AutoGenerateColumns="false" CssClass="SubFormWOBG"
                                DataKeyNames="ApplicationCode" Width="100%" ShowHeader="false" OnRowDataBound="gvMainGrid_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo" ItemStyle-Width="80px" HeaderStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnReferBackSN" runat="server" Value='<%#System.Web.HttpUtility.HtmlEncode(Eval("ReferBackSN")) %>' />
                                            <asp:HiddenField ID="hdnApplicationCode" runat="server" Value='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>' />
                                            <asp:Label ID="Label2" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ReferBackHeader")) %>'
                                                Font-Size="15px" Font-Bold="True"></asp:Label>
                                            <asp:GridView ID="grdVerificationIndustrialApplicationStatus" runat="server" ShowHeaderWhenEmpty="true"
                                                PageSize="8" AllowPaging="true" Width="100%" AutoGenerateColumns="false" CssClass="SubFormWOBG"
                                                Caption='<center><div class="SubmittedApp"><b>Application Verification</b></div></center>'
                                                DataKeyNames="ApplicationCode,SerialNumber" OnRowDataBound="grdVerificationIndustrialApplicationStatus_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Receive Date" ItemStyle-Width="80px" HeaderStyle-Width="80px">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnApplicationCode" runat="server" Value='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode")) %>' />
                                                            <asp:HiddenField ID="hdnSerialNumberVeri" runat="server" Value='<%#System.Web.HttpUtility.HtmlEncode(Eval("SerialNumber")) %>' />
                                                            <%#System.Web.HttpUtility.HtmlEncode(Eval("ReceiveDate", "{0:d}"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From User Name" ItemStyle-Width="110px" HeaderStyle-Width="110px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFromUser" Font-Bold="true" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("FromUserCode"))%>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblFromUserRegionOrDistrictOrHqName" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="To User Name" ItemStyle-Width="90px" HeaderStyle-Width="90px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblToUser" runat="server" Font-Bold="true" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ToUserCode"))%>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblToUserRegionOrDistrictOrHqName" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Forwarded User Name" ItemStyle-Width="90px" HeaderStyle-Width="90px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblForwardedUser" runat="server" Font-Bold="true" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ForwardedUserCode"))%>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblForwardedUserRegionOrDistrictOrHqName" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action Date" ItemStyle-Width="90px" HeaderStyle-Width="90px">
                                                        <ItemTemplate>
                                                            <%#System.Web.HttpUtility.HtmlEncode(Eval("ActionDate", "{0:d}"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action Internal Status" ItemStyle-Width="90px" HeaderStyle-Width="90px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblActionInternalStatus" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ActionInternalStatusCode"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action Comment" ItemStyle-Width="90px" HeaderStyle-Width="90px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblActionComment" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ActionComment"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Copy of Application Received On" ItemStyle-Width="80px"
                                                        HeaderStyle-Width="80px">
                                                        <ItemTemplate>
                                                            <%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationReceivedOn","{0:d}"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No Record for this Stage.
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                            <br />
                                            <asp:GridView ID="grdAppProcessingIndustrialApplicationStatus" runat="server" Width="100%"
                                                OnRowCommand="grdAppProcessingIndustrialApplicationStatus_RowCommand" AutoGenerateColumns="false"
                                                CssClass="SubFormWOBG" ShowHeaderWhenEmpty="true" DataKeyNames="ApplicationCode,SerialNumber"
                                                Caption='<center><div class="SubmittedApp"><b>Application Processing</b></div></center>'
                                                OnRowDataBound="grdAppProcessingIndustrialApplicationStatus_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Receive Date" ItemStyle-Width="80px" HeaderStyle-Width="80px">
                                                        <ItemTemplate>
                                                            <%#System.Web.HttpUtility.HtmlEncode(Eval("ReceiveDate", "{0:d}"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From User Name" ItemStyle-Width="100px" HeaderStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFromUser" Font-Bold="true" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("FromUserCode"))%>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblFromUserRegionOrDistrictOrHqName" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="To User Name" ItemStyle-Width="80px" HeaderStyle-Width="80px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblToUser" runat="server" Font-Bold="true" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ToUserCode"))%>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblToUserRegionOrDistrictOrHqName" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Forwarded User Name" ItemStyle-Width="90px" HeaderStyle-Width="90px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblForwardedUser" runat="server" Font-Bold="true" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ForwardedUserCode"))%>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblForwardedUserRegionOrDistrictOrHqName" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action Date" ItemStyle-Width="80px" HeaderStyle-Width="80px">
                                                        <ItemTemplate>
                                                            <%#System.Web.HttpUtility.HtmlEncode(Eval("ActionDate", "{0:d}"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action Internal Status" ItemStyle-Width="90px" HeaderStyle-Width="90px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblActionInternalStatus" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ActionInternalStatusCode"))%>'></asp:Label>
                                                            <asp:LinkButton ID="lnkbtPresentation" Visible="false" runat="server" Text="Called for Presentation"
                                                                CommandName="Presentation" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("PresentationCalledSerialNumber")) %>'></asp:LinkButton>
                                                            <asp:Label ID="lblPresentPreviousDt" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action Comment" ItemStyle-Width="90px" HeaderStyle-Width="90px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblActionComment" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ActionComment"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ground Water Recom Per Day" ItemStyle-Width="80px"
                                                        HeaderStyle-Width="80px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGroundWaterRecommendedPerDay" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("GroundWaterRecommendedPerDay"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ground Water Recom Annual" ItemStyle-Width="80px"
                                                        HeaderStyle-Width="80px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGroundWaterRecommendedAnnually" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("GroundWaterRecommendedAnnually"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No Record for this Stage.
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                            <br />
                                            <asp:GridView ID="grdNOCProcessingIndustrialApplicationStatus" runat="server" Width="100%"
                                                AutoGenerateColumns="false" CssClass="SubFormWOBG" ShowHeaderWhenEmpty="true"
                                                DataKeyNames="ApplicationCode,SerialNumber" Caption='<center><div class="SubmittedApp"><b>NOC Processing</b></div></center>'
                                                OnRowDataBound="grdNOCProcessingIndustrialApplicationStatus_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Receive Date" ItemStyle-Width="80px" HeaderStyle-Width="80px">
                                                        <ItemTemplate>
                                                            <%#System.Web.HttpUtility.HtmlEncode(Eval("ReceiveDate", "{0:d}"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From User Name" ItemStyle-Width="100px" HeaderStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFromUser" runat="server" Font-Bold="true" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("FromUserCode"))%>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblFromUserRegionOrDistrictOrHqName" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="To User Name" ItemStyle-Width="80px" HeaderStyle-Width="80px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblToUser" runat="server" Font-Bold="true" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ToUserCode"))%>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblToUserRegionOrDistrictOrHqName" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Forwarded User Name" ItemStyle-Width="90px" HeaderStyle-Width="90px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblForwardedUser" runat="server" Font-Bold="true" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ForwardedUserCode"))%>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblForwardedUserRegionOrDistrictOrHqName" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action Date" ItemStyle-Width="80px" HeaderStyle-Width="80px">
                                                        <ItemTemplate>
                                                            <%#System.Web.HttpUtility.HtmlEncode(Eval("ActionDate", "{0:d}"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action Internal Status" ItemStyle-Width="90px" HeaderStyle-Width="90px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblActionInternalStatus" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ActionInternalStatusCode"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action Comment" ItemStyle-Width="90px" HeaderStyle-Width="90px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblActionComment" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ActionComment"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No Record for this Stage.
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                            <br />
                                            <asp:GridView ID="grdDisbursmentProcessingIndustrialApplicationStatus" runat="server"
                                                Width="100%" AutoGenerateColumns="false" CssClass="SubFormWOBG" ShowHeaderWhenEmpty="true"
                                                DataKeyNames="ApplicationCode,SerialNumber" Caption='<center><div class="SubmittedApp"><b>NOC Disbursement</b></div></center>'
                                                OnRowDataBound="grdDisbursmentProcessingIndustrialApplicationStatus_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Receive Date" ItemStyle-Width="80px" HeaderStyle-Width="80px">
                                                        <ItemTemplate>
                                                            <%#System.Web.HttpUtility.HtmlEncode(Eval("ReceiveDate", "{0:d}"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From User Name" ItemStyle-Width="100px" HeaderStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFromUser" runat="server" Font-Bold="true" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("FromUserCode"))%>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblFromUserRegionOrDistrictOrHqName" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="To User Name" ItemStyle-Width="80px" HeaderStyle-Width="80px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblToUser" runat="server" Font-Bold="true" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ToUserCode"))%>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblToUserRegionOrDistrictOrHqName" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Forwarded User Name" ItemStyle-Width="90px" HeaderStyle-Width="90px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblForwardedUser" runat="server" Font-Bold="true" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ForwardedUserCode"))%>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblForwardedUserRegionOrDistrictOrHqName" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action Date" ItemStyle-Width="80px" HeaderStyle-Width="80px">
                                                        <ItemTemplate>
                                                            <%#System.Web.HttpUtility.HtmlEncode(Eval("ActionDate", "{0:d}"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action Internal Status" ItemStyle-Width="90px" HeaderStyle-Width="90px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblActionInternalStatus" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ActionInternalStatusCode"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action Comment" ItemStyle-Width="90px" HeaderStyle-Width="90px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblActionComment" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("ActionComment"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No Record for this Stage.
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="2">
                           <%-- <a id="A1" runat="server" href="~/ExternalUser/ApplicantHome.aspx">Go Back</a>--%>
                            <asp:LinkButton runat="server" Text="Go Back" ID="btnGoBack" 
                                onclick="btnGoBack_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
