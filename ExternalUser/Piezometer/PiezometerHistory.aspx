<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" CodeFile="PiezometerHistory.aspx.cs" Inherits="ExternalUser_Piezometer_PiezometerHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

     <script src="../../Scripts/ClientSideDateValidation.js" type="text/javascript"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
      </asp:ToolkitScriptManager>

    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />  

    <table align="center" class="SubFormWOBG" style="line-height: 25px" width="90%">


        <tr>
            <td colspan="2">
                <div class="div_IndAppHeading" style="padding-left: 10px; font-size: 18px; height: 25px">
                    <b>Piezometer Reading</b>
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
            <td>Month/Year :<span class="Coumpulsory">*</span></td>
            <td style="width: 50%">
                <asp:TextBox ID="txtDate" runat="server" MaxLength="200"></asp:TextBox>
               <%-- <asp:RequiredFieldValidator ID="rfvtxtDate" ControlToValidate="txtDate" runat="server" ValidationGroup="Save"  ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>--%>
                 <asp:ImageButton ID="ImgBtnDate" runat="server" ImageUrl="~/Images/calendar.png"
                    CausesValidation="false" /> 
                <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True"
                    TargetControlID="txtDate" PopupButtonID="ImgBtnDate"
                    Format="MM/yyyy" DefaultView="Months"> 
                </asp:CalendarExtender>

                <asp:RequiredFieldValidator ID="rfvDate" ControlToValidate="txtDate" ValidationGroup="Save" runat="server"  ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
            
               <%-- <asp:CustomValidator ID="CstmVtxtDate" runat="server" ClientValidationFunction="ValidateDateOnClient"
                                OnServerValidate="ValidateDate" ControlToValidate="txtDate" ErrorMessage="Invalid Date."
                                ForeColor="Red" ValidationGroup="Save" Display="Dynamic" />--%>
            
            
            
            
            </td>
        </tr>
                    
                  
        <tr>
            <td>
                Water Level : <span class="Coumpulsory">*</span>
            </td>
            <td style="width: 50%">
                  <asp:TextBox ID="txtWaterLevel" runat="server" MaxLength="6"></asp:TextBox>
                 &nbsp;(mbgl<sup></sup>)

                 <asp:RequiredFieldValidator ID="rfvtxtWaterLevel" ControlToValidate="txtWaterLevel" runat="server"  ValidationGroup="Save" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                         
                  <asp:RegularExpressionValidator ID="revtxtWaterLevel" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtWaterLevel" ValidationGroup="Save" SetFocusOnError="True"></asp:RegularExpressionValidator>  
                               
                
                  
            </td>
        </tr>      

        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                   <asp:Label ID="lblApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                <asp:Label ID="lblPiezoCodecur" Visible="false" runat="server" Enabled="False"></asp:Label>
            </td> 
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">              
                <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClick="btnPrev_Click" />
                 &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button runat="server" Text="Save" ID="btnSave" ValidationGroup="Save" OnClick="btnSave_Click" style="height: 26px" />
                 &nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Button  ID="btnClear" runat="server" Text="Clear" style="height: 26px" OnClick="btnClear_Click" />
                 &nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Button ID="btnClose" runat="server" Text="Close" style="height: 26px" OnClick="btnClose_Click"  />

               

            </td>
        </tr>        

           <tr>
             <td align="center" colspan="2">
                <asp:GridView ID="gvPiezometer" runat="server" CssClass="SubFormWOBG" AllowPaging="true"
                    align="center" AutoGenerateColumns="False" AllowSorting="true" DataKeyNames="ApplicationCode,PiezometerCode,SN"
                       OnRowCancelingEdit="gvPiezometer_RowCancelingEdit"
                    OnRowEditing="gvPiezometer_RowEditing" OnRowUpdating="gvPiezometer_RowUpdating"
                    Width="800px" OnSorting="gvPiezometer_Sorting" OnPageIndexChanging="gvPiezometer_PageIndexChanging"
                    PageSize="50" OnRowDataBound="gvPiezometer_RowDataBound">                  
                   

                    <Columns>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="20px" ImageUrl="~/Images/update.jpg"
                                    ValidationGroup="gvPiezometer" Width="20px" CommandName="Update" ToolTip="Update" />
                                <asp:ImageButton ID="imgbtnCancel" runat="server" Height="20px" ImageUrl="~/Images/Cancel.jpg"
                                    CausesValidation="false" Width="20px" CommandName="Cancel" ToolTip="Cancel" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnEdit" runat="server" Height="20px" ImageUrl="~/Images/Edit.jpg"
                                    Width="20px" CommandName="Edit" ToolTip="Edit" CausesValidation="false" />
                               <%-- <asp:ImageButton ID="imgbtnDelete" runat="server" Height="20px" ImageUrl="~/Images/delete.jpg"
                                    Width="20px"  OnClientClick="return confirm('Are you sure you want to delete?');"
                                    CommandName="Delete"
                                    ToolTip="Delete" />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SN" >
                            <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>                                 
                            </ItemTemplate>
                        </asp:TemplateField>   
                        <asp:TemplateField HeaderText="Month/Year" >
                            <EditItemTemplate>

                                <asp:Label ID="Label2" runat="server" Text="Month/Year :"></asp:Label>
                               <%-- <asp:Label ID="Label1" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Location")) %>'></asp:Label>--%>
                                <asp:TextBox ID="txtDate" MaxLength="50" runat="server" Enabled="false" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Date")) %>'></asp:TextBox>
                               <%-- <asp:Label ID="lblDate" runat="server" Visible="false"></asp:Label>--%>

                                 <asp:RequiredFieldValidator ID="gvrfvtxtDate" ControlToValidate="txtDate" runat="server"  ValidationGroup="gvPiezometer" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                            
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" MaxLength="200" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Date")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>  

                        <asp:TemplateField HeaderText="Water Level">
                            <ItemTemplate>
                                <asp:Label ID="lblWaterLevel" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("WaterLevel"))%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblWaterLevel" runat="server" Text="Water Level :"></asp:Label>
                                                             
                                   <asp:TextBox ID="txtWaterLevel" MaxLength="6" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("WaterLevel")) %>'></asp:TextBox>
                               <asp:RequiredFieldValidator ID="gvrfvtxtWaterLevel" ControlToValidate="txtWaterLevel" runat="server"  ValidationGroup="gvPiezometer" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
               
                                 <asp:RegularExpressionValidator ID="revtxtWaterLevel" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtWaterLevel" ValidationGroup="gvPiezometer"></asp:RegularExpressionValidator>
                                 
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


</asp:Content>

