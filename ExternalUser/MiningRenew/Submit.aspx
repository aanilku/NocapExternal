<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="Submit.aspx.cs"
    Inherits="ExternalUser_MiningRenew_Submit" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Namespace="MyControls" TagPrefix="cc1" %>
<asp:content id="Content1" contentplaceholderid="head" runat="Server">
      <script src="../../Scripts/ClientSideDateValidation.js" type="text/javascript"></script>
    <script src="../../Scripts/Calendar/jquery-min-Calendar.js" type="text/javascript"></script>
    <script src="../../Scripts/Calendar/jquery-ui.min-Calendar.js" type="text/javascript"></script>
    <link href="../../Styles/Calendar/jquery-ui-Calendar.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("[id*=txtBharatKoshDated]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd/mm/yy',
                minDate: new Date('01/01/2015'),
                maxDate: new Date(),
                changeMonth: true,
                changeYear: true,
                showOn: 'both',
                buttonImage: '../../images/calendar.png'
            });
        });

       <%-- function ConfirmApproval(objMsg) {
            if (document.getElementById('<%= txtBharatKoshRefferenceNo.ClientID %>').value == "") {
                alert("Bharat Kosh transaction refference number  is mandatory.");
                return true;
            }
            if (document.getElementById('<%= txtBharatKoshDated.ClientID %>').value == "") {
                alert("Bharat Kosh transaction date is mandatory.");
                return true;
            }

        }--%>

    </script>
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" CombineScripts="false" runat="server">
    </asp:ToolkitScriptManager>
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
                                            <li class="visited">Existing NOC Details</li>
                                            <li class="visited">Land Use Details</li>
                                            <li class="visited">Dewatering Existing Structure</li>
                                            <li class="visited">Dewatering Additional Structure</li>
                                            <li class="visited">Utilization of pumped water</li>
                                            <li class="visited">Monitoring of groundwater regime</li>
                                            <li class="visited">Groundwater Abstraction Structure- Existing</li>
                                            <li class="visited">Groundwater Abstraction Structure- Additional</li>
                                            <li class="visited">Compliance Conditions in the NOC</li>
                                            <li class="visited">Compliance Conditions in the NOC - Other</li>
                                             <li class="visited">Other Details</li>
                                        
                                            <li class="visited">Attachment</li>
                                              <li class="visited">Ready To Submit</li>
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
                        <td colspan="3">
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                RENEW - MINING USE: Final Submit
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">
                            (1).
                        </td>
                        <td colspan="2">
                            <b>Final Details of Application</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 50%">
                            (i) Ground Water Required through Abstract Structure (m<sup>3</sup>/day)
                        </td>
                        <td>
                            <asp:Label ID="lblWaterReqThroughAbsStruc" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            (ii) Ground Water Required through Dewatering / Mining Seeping (m<sup>3</sup>/day)
                        </td>
                        <td>
                            <asp:Label ID="lblGWrequiredMiningSeeping" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            (iii) Name of Mine / Project
                        </td>
                        <td>
                            <asp:Label ID="lblNameOfMining" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            (iv) Details of Existing NOC :
                        </td>
                        <td>
                            <asp:Label ID="lblMINExistingNOCNumber" runat="server" Text="Label"></asp:Label><br />
                            <asp:Label ID="lblMINNOCValidity" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            (v) Applied For Renewal :
                        </td>
                        <td>
                            <asp:Label ID="lblAppliedForRenewal" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            (vi) State
                        </td>
                        <td>
                            <asp:Label ID="lblState" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            (vii) District
                        </td>
                        <td>
                            <asp:Label ID="lblDistrict" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            (viii) Sub District
                        </td>
                        <td>
                            <asp:Label ID="lblSubDistrict" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            (ix) Village/Town
                        </td>
                        <td>
                            <asp:Label ID="lblVillageTown" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>                   
                    <tr>
                        <td>
                        </td>
                        <td>
                            (x) Apply Area Type
                        </td>
                        <td>
                            Area Type -
                            <asp:Label ID="lblAreaType" runat="server" Text="Label" Font-Bold="true"></asp:Label><br />
                            Area Type Category -
                            <asp:Label ID="lblAreaTypeCatagory" runat="server" Text="Label" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            (xi) Present Area Type
                        </td>
                        <td>
                            Area Type -
                            <asp:Label ID="lblpresAreaType" runat="server" Text="Label" Font-Bold="true"></asp:Label><br />
                            Area Type Category -
                            <asp:Label ID="lblpresAreaTypeCatagory" runat="server" Text="Label" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>(2.)</td><td colspan="2"><b>Self Declaration</b></td>

                    </tr>

                    <tr>
                        <td></td>
                        <td colspan="4">

                            <cc1:customeditor ID="GeneralConditionCustomEditor" ActiveMode="Preview" NoScript="true"
                                runat="server" Height="400px" />


                        </td>
                    </tr>
                     <tr>
                        <td style="width: 20px">
                            <b></b>
                        </td>
                        <td colspan="4">
                            <asp:CheckBox runat="server" ID="chkNOC" Text="NOC Condition-I have read and understood, I agreed." Font-Bold="true" Checked="true" />
                        </td>
                    </tr>
                     
                
                       <tr>
                           <td></td>
                        <td colspan="2">
                            <b class="Coumpulsory">Note :</b>
                            <table class="SubFormWOBG" width="97%" style="line-height: 25px; margin-left: 3%;">
                            
                                <tr>
                                    <td colspan="3" class="Coumpulsory">
                                        <asp:Label ID="lblProcessingFeeNonRefund" runat="server" Text=" a)  The Processing Fee is Non-Refundable. Applicant should 
                            ensure &quot;Check Eligibility&quot; and &quot;Documents Required&quot; before Submitting Application Online."
                                            class="Coumpulsory"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblSecondNumber" runat="server" Text="b)" CssClass="Coumpulsory"></asp:Label>
                                        <asp:Label ID="lblFee" runat="server" Text="" class="Coumpulsory"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                     <td colspan="3" class="Coumpulsory">
                            <asp:Label CssClass="Coumpulsory" ID="lblSubmittedApplication" runat="server" Text="c)  
                               Scanned copy of signature and seal document should be attached at presribed place before submission of application.
                                <br />
                                Note:Signature and seal document can be obtain from Preview option in New-Save As Draft on Applicant Home Page.
                                "></asp:Label>
                           
                        </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblMiningApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblFinalMsg" runat="server"></asp:Label>
                            <asp:Label ID="lblAppStop" Visible="false" runat="server" Enabled="False" Font-Bold="true" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="3">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('If you have not saved data, Your data will be lost before moving to other page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="FinalSubmit"
                                OnClientClick="return confirm('Are you sure you want to submit the application ?')"
                                OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:content>
