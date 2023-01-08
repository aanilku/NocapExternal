<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" CodeFile="SubmitAppeal.aspx.cs" Inherits="ExternalUser_Grievance_SubmitAppeal" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" src="../../Scripts/jquery-3.6.0.min.js"></script>
  



    <script type="text/javascript">
         
         
        function ddlTypeofGrievanceChange() {            
            var ddlTypeofGrievance = document.getElementById("<%=ddlTypeofGrievance.ClientID%>");  
            var getvalue = ddlTypeofGrievance.options[ddlTypeofGrievance.selectedIndex].value;
            
            if(getvalue==2)
            {            
       

                document.getElementById("<%=txtNOCNumber.ClientID%>").disabled=true;
                document.getElementById("<%=txtQuantum.ClientID%>").disabled=true;                

            }
            else
            {

 

                document.getElementById("<%=txtNOCNumber.ClientID%>").disabled = false;
                document.getElementById("<%=txtQuantum.ClientID%>").disabled = false; 

            }


        }

        
    </script>
    <style type="text/css">
        .auto-style1 {
            height: 45px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
     <asp:Label ID="lblGrievanceCode" runat="server" Enabled="False" Visible="False"></asp:Label>
         <asp:HiddenField ID="hidCSRF" runat="server" Value="" />

    <table class="SubFormWOBG" width="100%" style="line-height: 25px">
        <tr>

            <td style="width:20%;">

            </td>
            <td>

           
    <table class="SubFormWOBG" width="100%" style="line-height: 25px">

        <tr>
                        <td colspan="2">
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                Submit Grievance
                            </div>
                        </td>
                    </tr>


        <tr>

            <td style="width:200px;">State: </td>
            <td>  
                 <asp:DropDownList ID="ddlState" runat="server" Enabled="false"  
                                Width="200px">
                            </asp:DropDownList>
                          <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue=""
                                Display="Dynamic" ControlToValidate="ddlState" ForeColor="Red" ValidationGroup="SubmitGrievance">Required</asp:RequiredFieldValidator>--%>

            </td>
        </tr>
        <tr>

            <td>Type of Grievance: </td>
            <td> <asp:DropDownList ID="ddlTypeofGrievance" runat="server"  Enabled="false" 
                                Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlTypeofGrievance_SelectedIndexChanged">                
                 <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                <asp:ListItem Text="Related to NOC" Value="1"></asp:ListItem>
                 <asp:ListItem Text="Other" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            </td>
        </tr>

        <tr>

            <td class="auto-style1">Have you Received NOC: </td>
            <td class="auto-style1">  

                <asp:RadioButtonList ID="rbtnHaveYouReceivedNOC" runat="server" align="left" Enabled="false"
                                  RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbtnHaveYouReceivedNOC_SelectedIndexChanged" >                                
                                <asp:ListItem Value="Y" Text="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="N" Text="No">No</asp:ListItem>
                            </asp:RadioButtonList>

            </td>
        </tr>

        <tr>

            <td>Enter NOC No: </td>
            <td>
                <asp:TextBox ID="txtNOCNumber" runat="server" Enabled="false"></asp:TextBox>

            </td>
        </tr>

        <tr>

            <td>Quantum: ( KLD m3/day)</td>
            <td> <asp:TextBox ID="txtQuantum" runat="server" Enabled="false"></asp:TextBox> </td>
        </tr>

        <tr>

            <td>Submitted To: </td>
            <td> <asp:RadioButtonList ID="rbtnSubmittedTo" runat="server" align="left" Enabled="false"
                                  RepeatDirection="Horizontal" >                                
                                <asp:ListItem Value="Region">Region</asp:ListItem>
                                <asp:ListItem Value="HQ">HQ</asp:ListItem>
                            </asp:RadioButtonList> </td>
        </tr>

        <tr>

            <td>Remark </td>
            <td> 

                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>

            <td>Appeal Remark </td>
            <td> 

                <asp:TextBox ID="txtRemarkAppeal" runat="server" TextMode="MultiLine" ></asp:TextBox>
            </td>
        </tr>

        <tr>

            <td> <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />

            &nbsp;
                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
            </td>
        </tr>

    </table>

 
     </td>
        </tr>

    </table>
</asp:Content>



