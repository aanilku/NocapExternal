<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="ApplicationNameChangeStatus.aspx.cs"
    Inherits="ExternalUser_ApplicationManagement_ApplicationNameChangeStatus" Theme="Skin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ PreviousPageType VirtualPath="../../ApplicantHome.aspx" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src='<%= Page.ResolveUrl("~/Scripts/jquery-1.8.3.min.js")%>'></script>

    


    <style type="text/css">
        .ModalPopupBG {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }

        .PresentBackPopup {
            min-width: 500px;
            min-height: 250px;
            background: white;
        }

        .PopupBody {
        }

        .Initial {
            display: block;
            padding: 4px 18px 4px 18px;
            float: left; /* background: url("../Images/InitialImage.png") no-repeat right top;*/
            background-color: #EAEEF2;
            color: Black;
            font-weight: bold;
        }

            .Initial:hover {
                color: White; /* background: url("../Images/SelectedButton.png") no-repeat right top;*/
                background-color: #094E7F;
            }

        .Clicked {
            float: left;
            display: block; /*background: url("../Images/SelectedButton.png") no-repeat right top;*/
            background-color: #094E7F;
            padding: 4px 18px 4px 18px;
            color: Black;
            font-weight: bold;
            color: White;
        }

        .SubmittedApp {
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
   
   
    <table align="center" class="SubFormWOBG" style="width: 100%; line-height: 25px">
        <tr>
            
            <td style="vertical-align: top">
                <table width="100%" class="SubFormWOBG">
                    <tr>
                        <td colspan="2">
                            <div class="div_IndAppHeading" style="width: 100%; height: 23px; line-height: 22px; padding-left: 10px">
                                <asp:Label ID="Label3" runat="server" ForeColor="White" Font-Bold="True" Text="Application Status"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr style="visibility: hidden;">
                        <td style="width: 150px">Application Code :
                        </td>
                        <td style="width: 550px">
                            <asp:Label ID="lblApplicationCode" runat="server" Text=""></asp:Label>
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
                            <strong>Previous Project Name :</strong>
                        </td>
                        <td>
                            <asp:Label ID="lblPrevProjectName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <strong>New Project Name :</strong>
                        </td>
                        <td>
                            <asp:Label ID="lblNewProjectName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    
                    
        </tr>

         <tr>

            <td colspan="2" style="width: 400px; vertical-align: top">
                <b>Applicaton Name Change NOC</b></td>

        </tr>
        <tr>
            <td colspan="2">
                <div id="dvGrid" style="padding: 10px; width: 100%">
                   <asp:GridView ID="gvNameChangeBLL" runat="server" CssClass="SubFormWOBG" AutoGenerateColumns="False"
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
                       
                      
                    </Columns>
                    <EmptyDataTemplate>
                        No Records exist.
                    </EmptyDataTemplate>
                </asp:GridView>


                </div>
            </td>
        </tr>
        
          <tr runat="server" id="RowFinalStatus">
            <td style="font-weight: 700">Final Status
            </td>
            <td>
                <asp:Label ID="lblLatesStatus" runat="server"></asp:Label>
            </td>
        </tr>
        <tr runat="server" id="RowCurrentStage">
            <td style="font-weight: 700">Current Stage :
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
         <tr>
            <td style="text-align: right" colspan="2">
                <a runat="server" href="~/ExternalUser/ApplicantHome.aspx">Go Back</a>
            </td>
        </tr>
    </table>
  
</asp:Content>
