<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" CodeFile="RelaxationSubmit.aspx.cs" Inherits="ExternalUser_RelaxationApplication_RelaxationSubmit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type = "text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>Application Detail</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />

    <asp:Panel ID="pnlContents" runat="server">
    <table align="center" class="SubFormWOBG" width="100%">
        <tr>
              <td style="width: 200px">
                <table width="100%">
                    <tr>
                        <td>
                            <div class="middle">
                                <div class="block_left_inner">
                                    <div id="information" class="cont_left" style="display: block">
                                        <ul class="progressbar">
                                            <li class="visited">Location Details</li>
                                            <li class="visited">Communication Address</li>
                                            <li class="visited">Attachment</li>
                                            <%--   <li >Online Payment</li>--%>
                                            <li class="active">Final Submit</li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table class="SubFormWOBG" width="100%" style="line-height: 25px">
                    <tr>
                        <td colspan="5">
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                 Submit application  
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">
                            <b>(1). </b>
                        </td>
                        <td colspan="4">
                            <b>Final Details of application</b>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        

                        <td>(i) Name of Industry
                        </td>
                        <td>
                            <asp:Label ID="lblNameOfIndustry" runat="server" Text="Label"></asp:Label>
                        </td>

                        <td style="width: 35%">(ii) UID Number 
                        </td>
                        <td>
                            <asp:Label ID="lblUIDNumber" runat="server" Text="Label"></asp:Label>
                        </td>

                    </tr>
                    <tr>
                        <td></td>
                        <td>(iii) State
                        </td>
                        <td>
                            <asp:Label ID="lblState" runat="server" Text="Label"></asp:Label>
                        </td>

                        <td>(iv) District
                        </td>
                        <td>
                            <asp:Label ID="lblDistrict" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>(v) Sub District
                        </td>
                        <td>
                            <asp:Label ID="lblSubDistrict" runat="server" Text="Label"></asp:Label>
                        </td>

                        <td>(vi) Village/Town
                        </td>
                        <td>
                            <asp:Label ID="lblVillageTown" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <%--<td>(vii) Area Type Category
                        </td>
                        <td>
                            <asp:Label ID="lblAreaTypeCatagory" runat="server" Text="Label"></asp:Label>
                        </td>--%>

                        <td>(vii) Whether industry is MSME
                        </td>
                        <td>
                            <asp:Label ID="lblMSME" runat="server" Text="Label"></asp:Label>
                        </td>

                         <td>(viii) Whether the project falls in Wetland Area
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblWetlandarea" runat="server" Text="Label"></asp:Label>
                        </td>

                    </tr>
                      


                     <tr>
                        <td></td>
                        

                        <td>(ix)  Pay Amount
                        </td>
                        <td>
                            <asp:Label ID="lblAmount" runat="server" Text="" ></asp:Label>
                        </td>

                         <td> 
                             (x)  Bharat Kosh Transaction Ref. No
                        </td>
                        <td colspan="3">
                             <asp:Label ID="lblTransactionRefNo" runat="server" Text=""></asp:Label>
                        </td>

                    </tr>

                      <tr>
                        <td></td>
                        

                        <td>(ix)  Bharat Transaction Dated
                        </td>
                        <td>
                            <asp:Label ID="lblBharatTransDated" runat="server" Text="" ></asp:Label>
                        </td>

                         <td> 
                             
                        </td>
                        <td colspan="3">
                              
                        </td>

                    </tr>


                    
                </table>
            </td>
        </tr>
    </table>
        <div  style="margin-bottom: 50px;margin-top: 10px;font-size: 15px;">

   The Registration Certificate does not guarantee the issuance of NOC by the Authority. The
applicant shall be required to submit complete NOC Application along with required documents
/ reports before 30.09.2022 to obtain the NOC, failing which Environmental Compensation shall
be levied as per the CGWA Guidelines dated 24.09.2020.
        </div>

    </asp:Panel>
     <div style="text-align:right;"> <asp:Button ID="btnPrint" runat="server" Visible="false"  Text="Print" OnClientClick = "return PrintPanel();" />

    </div>
   <div style="text-align:center;">

        <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('Please Save as Draft before moving to Previous Page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="FinalSubmit"
                                OnClick="btnSubmit_Click" OnClientClick="return confirm('Are you Sure you want to submit the application ?')" />

    <table>

        <tr>
                        <td>
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblIndustialApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblFinalMsg" runat="server"></asp:Label>
                            <asp:Label ID="lblAppStop" Visible="false" runat="server" Enabled="False" Font-Bold="true" ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" >
                           
                        </td>
                    </tr>

    </table>
    </div>
</asp:Content>

 

