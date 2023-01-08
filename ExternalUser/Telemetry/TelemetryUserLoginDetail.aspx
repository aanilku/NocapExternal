<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" CodeFile="TelemetryUserLoginDetail.aspx.cs" Inherits="ExternalUser_Telemetry_TelemetryUserLoginDetail" %>


<asp:content id="Content1" contentplaceholderid="head" runat="Server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="Server">

   <asp:HiddenField ID="hidCSRF" runat="server" Value="" />


     <table align="center" class="SubFormWOBG" style="line-height: 25px" width="90%">
        <tr>
            <td colspan="2">
                <div class="div_IndAppHeading" style="padding-left: 10px; font-size: 18px; height: 25px">
                    <b>Telemetry Login Detail</b>
                </div>
            </td>
        </tr>
          <tr>
            <td>
                Application Name:
            </td>
            <td style="width: 50%">
                <asp:Label ID="lblName" runat="server" MaxLength="200"></asp:Label>
               
            </td>
        </tr>
            <tr>
            <td>
                Application Number:
            </td>
            <td style="width: 50%">
                <asp:Label ID="lblNo" runat="server" MaxLength="200"></asp:Label>
               
            </td>
        </tr>
        <tr>
            <td>
                Telemetry Name:<span class="Coumpulsory">*</span>
            </td>
            <td style="width: 50%">
                <asp:TextBox ID="txtTelName" runat="server" MaxLength="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtTelName" ControlToValidate="txtTelName" runat="server" ValidationGroup="Save"  ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                
            </td>
        </tr>
       
        <tr>
            <td>
                Telemetry Url: <span class="Coumpulsory">*</span>
            </td>
            <td style="width: 50%">
                  <asp:TextBox ID="txtTelUrl" runat="server" MaxLength="500"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvtxtTelUrl" ControlToValidate="txtTelUrl" runat="server"  ValidationGroup="Save" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
               
            </td>
        </tr>
          <tr>
            <td>
                Telemetry User Name: <span class="Coumpulsory">*</span>
            </td>
            <td style="width: 50%">
               <asp:TextBox ID="txtTelUserName" runat="server" MaxLength="100"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvtxtTelUserName" ControlToValidate="txtTelUserName" runat="server" ValidationGroup="Save"  ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
               
            </td>
        </tr> <tr>
            <td>
                Telemetry Password:<span class="Coumpulsory">*</span>
            </td>
            <td style="width: 50%">
               <asp:TextBox ID="txtTelPassword" runat="server" MaxLength="100"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvtxtTelPassword" ControlToValidate="txtTelPassword" ValidationGroup="Save" runat="server"  ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
               
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                   <asp:Label ID="lblApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
            </td> 
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Button runat="server" Text="Save" ID="btnSave" ValidationGroup="Save" OnClick="btnSave_Click" style="height: 26px" />
                 &nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Button runat="server" Text="Clear " ID="btnClear" style="height: 26px" OnClick="btnClear_Click" />
                 &nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Button runat="server" Text="Close" ID="btnClose" style="height: 26px" OnClick="btnClose_Click"  />

            </td>
        </tr>

          <tr>
             <td align="center" colspan="2">
                <asp:GridView ID="gvTelemetry" runat="server" CssClass="SubFormWOBG" AllowPaging="true"
                    align="center" AutoGenerateColumns="False" AllowSorting="true" DataKeyNames="ApplicationCode,ApplicationSN"
                       OnRowCancelingEdit="gvTelemetry_RowCancelingEdit"
                    OnRowEditing="gvTelemetry_RowEditing" OnRowUpdating="gvTelemetry_RowUpdating"
                    Width="827px" OnSorting="gvTelemetry_Sorting" OnPageIndexChanging="gvTelemetry_PageIndexChanging"
                    PageSize="50" OnRowDeleting="gvTelemetry_RowDeleting" >
                  
                   

                    <Columns>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="20px" ImageUrl="~/Images/update.jpg"
                                    ValidationGroup="gvTelemetry" Width="20px" CommandName="Update" ToolTip="Update" />
                                <asp:ImageButton ID="imgbtnCancel" runat="server" Height="20px" ImageUrl="~/Images/Cancel.jpg"
                                    CausesValidation="false" Width="20px" CommandName="Cancel" ToolTip="Cancel" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnEdit" runat="server" Height="20px" ImageUrl="~/Images/Edit.jpg"
                                    Width="20px" CommandName="Edit" ToolTip="Edit" CausesValidation="false" />
                                <asp:ImageButton ID="imgbtnDelete" runat="server" Height="20px" ImageUrl="~/Images/delete.jpg"
                                    Width="20px"  OnClientClick="return confirm('Are you sure you want to delete?');"
                                    CommandName="Delete"
                                    ToolTip="Delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SN" >
                            <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>                                 
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Telemetry Name" >
                            <EditItemTemplate>

                                <asp:Label ID="Label2" runat="server" Text="Telemetry Name :"></asp:Label>
                                <asp:Label ID="Label1" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TelemetryName")) %>'></asp:Label>
                                <asp:TextBox ID="txtTelemetryName" MaxLength="50" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TelemetryName")) %>'></asp:TextBox>
                                <asp:Label ID="lblTelemetryName" runat="server" Visible="false"></asp:Label>

                                 <asp:RequiredFieldValidator ID="gvrfvtxtTelemetryName" ControlToValidate="txtTelemetryName" runat="server"  ValidationGroup="gvTelemetry" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
               

                             
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTelemetryName" runat="server" MaxLength="200" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TelemetryName")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Telemetry Url" >
                            <EditItemTemplate>
                                <asp:Label ID="lblTelemetryUrl" runat="server" Text="Telemetry Url :"></asp:Label>
                                <asp:Label ID="lblTelemetryUrl1" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TelemetryUrl")) %>'></asp:Label>
                                <asp:TextBox ID="txtTelemetryUrl" MaxLength="500" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TelemetryUrl")) %>'></asp:TextBox>
                               <%-- <asp:Label ID="lblTelemetryUrl" runat="server" Visible="false"></asp:Label>--%>
                            <asp:RequiredFieldValidator ID="gvrfvtxtTelUrl" ControlToValidate="txtTelemetryUrl" runat="server"  ValidationGroup="gvTelemetry" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
               

                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTelemetryUrl" runat="server"  Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TelemetryUrl")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Telemetry User Name">
                            <ItemTemplate>
                                <asp:Label ID="lblTelUserName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TelUserName"))%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblTelUserName" runat="server" Text="Telemetry User Name :"></asp:Label>
                                <asp:Label ID="lblTelUserNameValue" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TelUserName")) %>'></asp:Label>
                                   <asp:TextBox ID="txtTelUserName" MaxLength="100" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TelUserName")) %>'></asp:TextBox>
                               <asp:RequiredFieldValidator ID="gvrfvtxtTelUserName" ControlToValidate="txtTelUserName" runat="server"  ValidationGroup="gvTelemetry" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
               
                            </EditItemTemplate>
                            
                        </asp:TemplateField>
                       
                         <asp:TemplateField HeaderText="Telemetry Password">
                            <ItemTemplate>
                                <asp:Label ID="lblTelPassword" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TelPassword"))%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblTelPassword" runat="server" Text="Telemetry Password :"></asp:Label>
                                <asp:Label ID="lblTelPasswordValue" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TelPassword")) %>'></asp:Label>
                                     <asp:TextBox ID="txtTelPassword" MaxLength="100" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TelPassword")) %>'></asp:TextBox>
                              <asp:RequiredFieldValidator ID="gvrfvtxtTelPassword" ControlToValidate="txtTelPassword" runat="server"  ValidationGroup="gvTelemetry" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
               
                            </EditItemTemplate>
                            
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                        PageButtonCount="4" />
                </asp:GridView>
                <asp:Label ID="lblSortField" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
  
    </table>
</asp:content>
