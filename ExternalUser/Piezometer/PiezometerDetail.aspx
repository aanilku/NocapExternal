<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" CodeFile="PiezometerDetail.aspx.cs" Inherits="ExternalUser_Piezometer_PiezometerDetail" %>

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
                    <b>Piezometer Detail</b>
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
            <td>Location :<span class="Coumpulsory">*</span></td>
            <td style="width: 50%">
                <asp:TextBox ID="txtLocation" runat="server" MaxLength="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtLocation" ControlToValidate="txtLocation" runat="server" ValidationGroup="Save"  ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                
                <asp:RegularExpressionValidator ID="revtxtLocation" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtLocation" ValidationGroup="Save" SetFocusOnError="True" ></asp:RegularExpressionValidator>

            </td>
        </tr>
                    
                  
        <tr>
            <td>
                Latitude: <span class="Coumpulsory">*</span>
            </td>
            <td style="width: 50%">
                  <asp:TextBox ID="txtPiezoLat" runat="server" MaxLength="11"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvtxtPiezoLat" ControlToValidate="txtPiezoLat" runat="server"  ValidationGroup="Save" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                         
                  <asp:RegularExpressionValidator ID="revtxtPiezoLat" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtPiezoLat" ValidationGroup="Save" SetFocusOnError="True" Type="Double"></asp:RegularExpressionValidator>    
            </td>
        </tr>
          <tr>
            <td>
                Longitude: <span class="Coumpulsory">*</span>
            </td>
            <td style="width: 50%">
               <asp:TextBox ID="txtPiezoLong" runat="server" MaxLength="11"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvtxtPiezoLong" ControlToValidate="txtPiezoLong" runat="server" ValidationGroup="Save"  ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                
               <asp:RegularExpressionValidator ID="revtxtPiezoLong" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtPiezoLong" ValidationGroup="Save" SetFocusOnError="True" Type="Double"></asp:RegularExpressionValidator>    
            </td>
        </tr> 
         <tr>
            <td>
                Depth Of Piezometer :<span class="Coumpulsory">*</span>
            </td>
            <td style="width: 50%">
               <asp:TextBox ID="txtDepthPiezo" runat="server" MaxLength="6"></asp:TextBox>
                 &nbsp;(mbgl<sup></sup>)
                 <asp:RequiredFieldValidator ID="rfvDepthPiezo" ControlToValidate="txtDepthPiezo" ValidationGroup="Save" runat="server"  ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
             
                <asp:RegularExpressionValidator ID="revtxtDepthPiezo" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtDepthPiezo" ValidationGroup="Save" SetFocusOnError="True" Type="Double"></asp:RegularExpressionValidator>   
            </td>
             </tr>   
        
          <tr>
            <td>
                Height Of Measuring Point ( TubeWell/Borebell) :<span class="Coumpulsory">*</span>
            </td>
            <td style="width: 50%">
               <asp:TextBox ID="txtHgtMsrnig" runat="server" MaxLength="6"></asp:TextBox>
                 &nbsp;(magl<sup></sup>)
                 <asp:RequiredFieldValidator ID="rfvHgtMsrnig" ControlToValidate="txtHgtMsrnig" ValidationGroup="Save" runat="server"  ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
             
                <asp:RegularExpressionValidator ID="revtxtHgtMsrnig" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtHgtMsrnig" ValidationGroup="Save" SetFocusOnError="True"></asp:RegularExpressionValidator>  
           
                <%--<asp:CompareValidator ID="cvtxtHgtMsrnig" Operator="GreaterThan" runat="server" CssClass="text-danger" ValidationGroup="Save" 
                     ControlToValidate="txtDepthPiezo" Display="Dynamic" ControlToCompare="txtHgtMsrnig" Type="Integer"
                     ErrorMessage="The Depth Of Piezo must be Greater/Equal the  Height Of Measuring Point ( TubeWell/Borebell)" SetFocusOnError="true"></asp:CompareValidator>--%>
                
                 <asp:CompareValidator ID="cvtxtHgtMsrnig" runat="server" ControlToValidate="txtHgtMsrnig"
                                ControlToCompare="txtDepthPiezo" ForeColor="Red" ErrorMessage="Height Of Measuring Point ( TubeWell/Borebell) should be Less than Depth Of Piezometer"
                                Display="Dynamic" ValidationGroup="Save" Operator="LessThan" Type="Double" ></asp:CompareValidator>
                
                 </td>
        </tr>

          <tr>
            <td>
                InstallDate :<span class="Coumpulsory">*</span>
            </td>
            <td style="width: 50%">
                  <asp:TextBox ID="txtInstallDate" runat="server" MaxLength="100"></asp:TextBox>
                 

                 <asp:ImageButton ID="ImgBtnInstallDate" runat="server" ImageUrl="~/Images/calendar.png"
                    CausesValidation="false" /> 
                <asp:CalendarExtender ID="txtInstallDate_CalendarExtender" runat="server" Enabled="True"
                    TargetControlID="txtInstallDate" PopupButtonID="ImgBtnInstallDate"
                    Format="dd/MM/yyyy"> 
                </asp:CalendarExtender>

                <asp:RequiredFieldValidator ID="rfvInstallDate" ControlToValidate="txtInstallDate" ValidationGroup="Save" runat="server"  ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
            
                <asp:CustomValidator ID="CstmVtxtInstallDate" runat="server" ClientValidationFunction="ValidateDateOnClient"
                                OnServerValidate="ValidateDate" ControlToValidate="txtInstallDate" ErrorMessage="Invalid Date."
                                ForeColor="Red" ValidationGroup="Save" Display="Dynamic" />
               <%-- <asp:RangeValidator ID="rvtxtInstallDate" runat="server" Type="Date" Display="Dynamic"
                                ValidationGroup="Save" ForeColor="Red" MinimumValue="01/01/1900" ControlToValidate="txtInstallDate"
                                ErrorMessage="Install Date should be grater than 01/01/1900 and less than or equal to current date."></asp:RangeValidator>--%>


            </td>
        </tr>         

      <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                   <asp:Label ID="lblApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                <asp:Label ID="lblPiezoCode" Visible="false" runat="server" Enabled="False"></asp:Label>
            </td> 
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
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
                    align="center" AutoGenerateColumns="False" AllowSorting="true" DataKeyNames="ApplicationCode,PiezometerCode"
                       OnRowCancelingEdit="gvPiezometer_RowCancelingEdit"
                    OnRowEditing="gvPiezometer_RowEditing" OnRowUpdating="gvPiezometer_RowUpdating"
                    Width="1000px" OnSorting="gvPiezometer_Sorting" OnPageIndexChanging="gvPiezometer_PageIndexChanging"
                    PageSize="50" OnRowDeleting="gvPiezometer_RowDeleting" OnRowDataBound="gvPiezometer_RowDataBound">                  
                   

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
                        <asp:TemplateField HeaderText="Piezometer Location" >
                            <EditItemTemplate>

                                <asp:Label ID="Label2" runat="server" Text="Piezometer Location :"></asp:Label>
                               <%-- <asp:Label ID="Label1" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Location")) %>'></asp:Label>--%>
                                <asp:TextBox ID="txtPiezometerLocation" MaxLength="200" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Location")) %>'></asp:TextBox>
                                <asp:Label ID="lblPiezometerLocation" runat="server" Visible="false"></asp:Label>

                                 <asp:RequiredFieldValidator ID="gvrfvtxtPiezometerLocation" ControlToValidate="txtPiezometerLocation" runat="server"  ValidationGroup="gvPiezometer" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                            
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPiezometerLocation" runat="server" MaxLength="200" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Location")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Piezometer Latitude">
                            <ItemTemplate>
                                <asp:Label ID="lblPiezometerLatitude" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Latitude"))%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblPiezometerLatitude" runat="server" Text="Piezometer Latitude :"></asp:Label>
                              <%--  <asp:Label ID="lblPiezometerLatitudeValue" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Latitude")) %>'></asp:Label>--%>
                                   <asp:TextBox ID="txtPiezometerLatitude" MaxLength="11" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Latitude")) %>'></asp:TextBox>
                               <asp:RequiredFieldValidator ID="gvrfvtxtPiezometerLatitude" ControlToValidate="txtPiezometerLatitude" runat="server"  ValidationGroup="gvPiezometer" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
               
                                 <asp:RegularExpressionValidator ID="revtxtPiezometerLatitude" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtPiezometerLatitude" ValidationGroup="gvPiezometer" Type="Double"></asp:RegularExpressionValidator>
                                 
                            </EditItemTemplate>
                            
                        </asp:TemplateField>
                       
                         <asp:TemplateField HeaderText="Piezometer Longitude">
                            <ItemTemplate>
                                <asp:Label ID="lblPiezometerLongitude" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Longitude"))%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblPiezometerLongitude" runat="server" Text="Piezometer Longitude:"></asp:Label>
                               <%-- <asp:Label ID="lblPiezometerLongitudeValue" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Longitude")) %>'></asp:Label>--%>
                                     <asp:TextBox ID="txtPiezometerLongitude" MaxLength="11" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Longitude")) %>'></asp:TextBox>
                              <asp:RequiredFieldValidator ID="gvrfvPiezometerLongitude" ControlToValidate="txtPiezometerLongitude" runat="server"  ValidationGroup="gvPiezometer" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
               
                                 <asp:RegularExpressionValidator ID="revtxtPiezometerLongitude" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtPiezometerLongitude" ValidationGroup="gvPiezometer" Type="Double"></asp:RegularExpressionValidator>
                                 
                            </EditItemTemplate>
                            
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Depth Of Piezometer">
                            <ItemTemplate>
                                <asp:Label ID="lblDepthOfPiezometer" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("DepthofPiezo"))%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblDepthOfPiezometer" runat="server" Text="Depth Of Piezometer :"></asp:Label>
                              <%-- <asp:Label ID="lblDepthOfPiezometerValue" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Latitude")) %>'></asp:Label>--%>
                                   <asp:TextBox ID="txtDepthOfPiezometer" MaxLength="6" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("DepthofPiezo")) %>'></asp:TextBox>
                               <asp:RequiredFieldValidator ID="gvrfvtxtDepthOfPiezometer" ControlToValidate="txtDepthOfPiezometer" runat="server"  ValidationGroup="gvPiezometer" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
               
                                <asp:RegularExpressionValidator ID="revtxtDepthOfPiezometer" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtDepthOfPiezometer" ValidationGroup="gvPiezometer" Type="Double"></asp:RegularExpressionValidator>
                                 

                            </EditItemTemplate>
                            
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText=" Height Of Measuring Point">
                            <ItemTemplate>
                                <asp:Label ID="lblHeightOfMeasuringPoint" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("HeightofMeasuring"))%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblHeightOfMeasuringPoint" runat="server" Text="Height Of Measuring Point :"></asp:Label>
                        <%-- <asp:Label ID="lblHeightOfMeasuringPointValue" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Latitude")) %>'></asp:Label>--%>
                                   <asp:TextBox ID="txtHeightOfMeasuringPoint" MaxLength="6" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("HeightofMeasuring")) %>'></asp:TextBox>
                               <asp:RequiredFieldValidator ID="gvrfvtxtHeightOfMeasuringPoint" ControlToValidate="txtHeightOfMeasuringPoint" runat="server"  ValidationGroup="gvPiezometer" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
               
                                 <asp:RegularExpressionValidator ID="revtxtHeightOfMeasuringPoint" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtHeightOfMeasuringPoint" ValidationGroup="gvPiezometer"></asp:RegularExpressionValidator>
                                 

                                 <asp:CompareValidator ID="cvtxtHeightOfMeasuringPoint" runat="server" ControlToValidate="txtHeightOfMeasuringPoint"
                                ControlToCompare="txtDepthOfPiezometer" ForeColor="Red" ErrorMessage="Height Of Measuring Point ( TubeWell/Borebell) should be Less than Depth Of Piezometer"
                                Display="Dynamic" ValidationGroup="gvPiezometer" Operator="LessThan" Type="Double"></asp:CompareValidator>



                            </EditItemTemplate>
                            
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText=" Install Date">
                            <ItemTemplate>
                                <asp:Label ID="lblInstallDate" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("InstallDate", "{0:dd MMM yyyy}"))%>'></asp:Label>                            
                                                           
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblInstallDate" runat="server" Text="Install Date :"></asp:Label>
                             <%-- <asp:Label ID="lblInstallDateValue" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Latitude")) %>'></asp:Label>--%>
                                   <asp:TextBox ID="txtInstallDate" MaxLength="100" runat="server" Enabled="false" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("InstallDate")) %>'></asp:TextBox>
                               <asp:RequiredFieldValidator ID="gvrfvtxtInstallDate" ControlToValidate="txtInstallDate" runat="server"  ValidationGroup="gvPiezometer" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
               
                            </EditItemTemplate>
                            
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Piezometer Detail">
                           <ItemTemplate> 
                                <asp:LinkButton ID="lnkHistory" runat="server" OnCommand="lbtnHistory_Click" 
                                 CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("PiezometerCode")) %>'>History</asp:LinkButton>
                                 </ItemTemplate>

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
 

