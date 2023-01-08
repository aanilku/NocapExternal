<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NameChange.aspx.cs"
     MasterPageFile="~/ExternalUser/ExternalUserMaster.master"  Inherits="ExternalUser_NameChange" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:content id="Content1" contentplaceholderid="head" runat="Server">
    <script src="../../Scripts/ClientSideDateValidation.js" type="text/javascript"></script>
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="Server">
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
                            <b>NOC And Change Name<br /></b>
                       </td>
                       <td></td>
                   </tr>
                   <tr>
                       <td></td>
                       <td>Application Type: <span class="Coumpulsory">*</span>
                      <td>
                           <asp:DropDownList ID="ddlApplicationType" runat="server" AutoPostBack="True"
                                Width="200px"  OnSelectedIndexChanged="ddlApplicationType_SelectedIndexChanged">
                            </asp:DropDownList>
                          
                      </td>
                   </tr>
                    <tr>
                        <td></td>
                        <td>Application No: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlApplicatonNumber" runat="server" AutoPostBack="true"
                                Width="200px"  OnSelectedIndexChanged="ddlApplicatonNumber_SelectedIndexChanged">
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
                            <asp:TextBox ID="txtNewName" runat="server" MaxLength="100" Width="51%" ></asp:TextBox>

                        </td>
                    </tr>
                     <tr>
                         <td></td>
                            <td>
                                If so Details thereof:<span class="Coumpulsory">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtWhetherPermissionRegisteredWithCGWADet" runat="server" MaxLength="100"
                                    TextMode="MultiLine" onkeyup="CountCharacter(this, this.form.ActionCommentRemCount, 100);"
                                    onkeydown="CountCharacter(this, this.form.ActionCommentRemCount, 100);" Height="77px" Width="374px"></asp:TextBox>
                                <br />
                                <input type="text" id="ActionCommentRemCount" tabindex="-1" style="border-width: 0px;
                                    width: 100px; font-size: 10px; text-align: left; margin-left: 110px; background-color: transparent"
                                    name="ActionCommentRemCount" size="2" maxlength="2" value="( 100 Character Left )"
                                    readonly="readonly" /><br />                               
                                
                            </td>
                        </tr>
                 
                   
                    <tr>
                        <td></td>
                        <td>Upload Documents: <span class="Coumpulsory">*</span>
                        </td>

                        <td>
                            <asp:UpdatePanel ID="UpdatePanelGWFlowDirectionMap" runat="server">
                               
                                <ContentTemplate>                                   
                                    <asp:FileUpload runat="server"></asp:FileUpload>
                                    <asp:Button runat="server" Text="Button"></asp:Button>
                                    <asp:UpdateProgress runat="server">
                                         <ProgressTemplate>
                                            <asp:Label ID="lblshowGWFlowDirectionMapWait" runat="server" BackColor="#507CD1"
                                                Font-Bold="True" ForeColor="White" Text="Please wait ... Uploading file"></asp:Label>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                   
                                </ContentTemplate>
                    </asp:UpdatePanel>
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
                  
               </table>
           </td>
          
       </tr>
   </table>
</asp:content>