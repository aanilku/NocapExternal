<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubDistrictAreaTypeCategoryHistoryDetails.aspx.cs" Inherits="Sub_SubDistrictAreaTypeCategoryHistory_SubDistrictAreaTypeCategoryHistoryDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
            
        .style3
        {
            width: 23%;
            border-style: solid;
            border-width: 1px;
        }
        .style4
        {
            width: 23%;
        }
        .style5
        {
            width: 213px;
        }
        .style6
        {
            width: 318px;
        }
    
        .style7
        {
            width: 291px;
        }
    
    </style>
</head>

<body>
    <form id="form1" runat="server">
    <div>
    

    <asp:HiddenField ID="hidCSRF" runat="server" value=""/>
    <table align="center" class="style4">
        <tr>
            <td>
                <div class="div_AreaType">
                    <strong>Sub District Area Type Category History</strong></div>
            </td>
        </tr>
    </table>
    <br />
    <table align="center" cellpadding="0" cellspacing="0" class="style3">
        <tr>
            <td class="style5">
                &nbsp;
            </td>
            <td class="style7">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style8" align="center">
                State</td>
            <td class="style7" align="center">
                <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" 
                    Width="65%" 
                    onselectedindexchanged="ddlState_SelectedIndexChanged">
                </asp:DropDownList>

                <br />
                <br />

            </td>
        </tr>
        <tr>
            <td class="style8" align="center">
                District&nbsp;</td>
            <td class="style7" align="center">
                <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="True" 
                    Width="65%" onselectedindexchanged="ddlDistrict_SelectedIndexChanged">
                </asp:DropDownList><br /><br />

            </td>
        </tr>
        <tr>
            <td class="style9">
                &nbsp;
            </td>
            <td class="style7">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style9">
                &nbsp;
            </td>
            <td class="style7">
                
            </td>
        </tr>
        <tr>
            <td class="style9">
                &nbsp;
            </td>
            <td class="style7">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                &nbsp; &nbsp;
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
   <table align="center" cellpadding="0" cellspacing="0" class="style4">
        <tr>
            <td align="center">
               
                    <asp:GridView ID="gvSubDistrictAreaTypeCatHist" runat="server" 
                    AutoGenerateColumns="False" AllowSorting ="True" AllowPaging="True" PageSize="5"
                    onsorting="gvSubDistrictAreaTypeCatHist_Sorting"
                    DataKeyNames="StateCode,DistrictCode,SubDistrictCode" Width="690px" 
                        Height="118px" onrowdatabound="gvSubDistrictAreaTypeCatHist_RowDataBound" 
                       
                        >
                   
                            
                                          
                    <Columns>

                                
                        <asp:TemplateField HeaderText="Sub District "  SortExpression="SubDistrictName">
                        <ItemTemplate>
                        
                                <asp:Label ID="lblSubDistrictName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("SubDistrictName")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Area Type">
                            <ItemTemplate>
                                <asp:Label ID="lblAreaTypeName" runat="server" Text=""></asp:Label>
                              
                            </ItemTemplate>
                            
                        </asp:TemplateField>                        




                        <asp:TemplateField HeaderText="Area Type Category">
                            <ItemTemplate>
                                <asp:Label ID="lblAreaTypeCatName" runat="server" Text=""></asp:Label>
                              
                            </ItemTemplate>
                            
                        </asp:TemplateField>                        


                         <asp:TemplateField HeaderText="Notification Date">
                            <ItemTemplate>
                                <asp:Label ID="lblnotificationDate" runat="server" Text=""> </asp:Label>
                              
                            </ItemTemplate>
                            
                        </asp:TemplateField>                        




                         <asp:TemplateField HeaderText="Start Date">
                             
                            <ItemTemplate>
                                <asp:Label ID="lblStartDate" runat="server" Text=""></asp:Label>
                              
                            </ItemTemplate>
                            
                        </asp:TemplateField>                        
                        
                    </Columns>
                    <PagerSettings FirstPageText="First" LastPageText="Last" 
                        Mode="NumericFirstLast" PageButtonCount="4" />
               
                </asp:GridView>
                
                <asp:Label ID="lblSortField" runat="server" Visible="False"></asp:Label>
                
                 </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>


    </div>
    </form>
</body>
</html>
