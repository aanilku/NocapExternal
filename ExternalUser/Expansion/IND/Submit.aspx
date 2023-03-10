<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="Submit.aspx.cs"
    Inherits="ExternalUser_Expansion_IND_Submit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Namespace="MyControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Scripts/ClientSideDateValidation.js" type="text/javascript"></script>

    <script src="../../Scripts/Calendar/jquery-min-Calendar.js" type="text/javascript"></script>
    <script src="../../Scripts/Calendar/jquery-ui.min-Calendar.js" type="text/javascript"></script>
    <link href="../../Styles/Calendar/jquery-ui-Calendar.css" rel="stylesheet" type="text/css" />


    <style type="text/css">
        .ui-datepicker {
            font-size: 8pt !important;
        }
    </style>
    <%-- <script type="text/javascript">
        function OnClientLoad(editor, args) {
            setTimeout(function () {
                editor.set_mode(4);
            }, 100);
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
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
                                            <li class="visited">Land Use Details</li>
                                            <li class="visited">Water Requirement Details</li>
                                            <li class="visited">Recycled Water Usage</li>
                                            <li class="visited">Groundwater Abstraction Structure- Existing</li>
                                            <li class="visited">Groundwater Abstraction Structure- Proposed</li>
                                            <li class="visited">Other Details</li>
                                            <%--             <li class="visited">Self Declaration</li>--%>
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
                        <td colspan="5">
                            <div class="div_IndAppHeading" style="padding-left: 10px">
                                INDUSTRIAL EXPANSION USE: Final Submit and Self Declaration
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
                        <td style="width: 35%">(i) Net Ground Water Requirement (m<sup>3</sup>/day)
                        </td>
                        <td>
                            <asp:Label ID="lblNetGroundWaterRequirement" runat="server" Text="Label"></asp:Label>
                        </td>

                        <td>(ii) Name of Industry
                        </td>
                        <td>
                            <asp:Label ID="lblNameOfIndustry" runat="server" Text="Label"></asp:Label>
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
                        <td>(vii) Area Type Category
                        </td>
                        <td>
                            <asp:Label ID="lblAreaTypeCatagory" runat="server" Text="Label" Font-Bold="true"></asp:Label>
                        </td>

                        <td>(viii) Whether industry is MSME
                        </td>
                        <td>
                            <asp:Label ID="lblMSME" runat="server" Text="Label" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>(ix) Whether the project falls in Wetland Area
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblWetlandarea" runat="server" Text="Label" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">
                            <b>(2). </b>
                        </td>
                        <td colspan="4">
                            <b>Self Declaration</b>
                        </td>
                    </tr>
                    <%-- <tr>
                        <td style="width: 20px">
                            <b></b>
                        </td>
                        <td colspan="4">
                           
                        </td>
                    </tr>--%>
                    <tr>
                        <td></td>
                        <td colspan="4">

                            <cc1:CustomEditor ID="GeneralConditionCustomEditor" ActiveMode="Preview" NoScript="true"
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
                        <td colspan="4">
                            <b class="Coumpulsory">Note :</b>
                        </td>

                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="4" class="Coumpulsory">
                            <asp:Label ID="lblProcessingFeeNonRefund" runat="server" Text=" a)  The Processing Fee is Non-Refundable. Applicant should 
                            ensure &quot;Check Eligibility&quot; and &quot;Documents Required&quot; before Submitting Application Online."
                                class="Coumpulsory"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td colspan="4" class="Coumpulsory">
                            <asp:Label CssClass="Coumpulsory" ID="lblSubmittedApplication" runat="server" Text="b) Scanned copy of signature and seal document should be attached at presribed place before submission of application.<br /> Note:Signature and seal document can be obtain from Preview option in New-Save As Draft on Applicant Home Page."></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="4" class="Coumpulsory">
                            <asp:Label CssClass="Coumpulsory" ID="Label2" runat="server" Text="c) Charges of Application should be paid before submission of application.<br /> Note:Payment option for charges can be obtain from Payment Detail(MakePayment) option in New-Save As Draft on Applicant Home Page."></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="4" class="Coumpulsory">
                            <asp:Label CssClass="Coumpulsory" ID="Label3" runat="server" Text="d) Once Payment is initiated,application detial can not be modify.<br /> Note:Please ensure the application is complete in all respect before the payment initiated"></asp:Label>

                        </td>
                    </tr>

                    <tr>
                        <td colspan="5">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblIndustialApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblFinalMsg" runat="server"></asp:Label>
                            <asp:Label ID="lblAppStop" Visible="false" runat="server" Enabled="False" Font-Bold="true" ForeColor="Red"></asp:Label>
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="5">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('Please Save as Draft before moving to Previous Page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="FinalSubmit"
                                OnClick="btnSubmit_Click" OnClientClick="return confirm('Are you Sure you want to submit the application ?')" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
