<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="Submit.aspx.cs"
    Inherits="ExternalUser_InfrastructureRenew_Submit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Namespace="MyControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                            <li class="visited">Water Requirement Details</li>
                                            <li class="visited">Recycled Water Usage</li>
                                            <li class="visited">Groundwater Abstraction Structure- Existing</li>
                                            <li class="visited">Groundwater Abstraction Structure- Additional</li>
                                            <li class="visited">Compliance Conditions in the NOC</li>
                                            <li class="visited">Compliance Conditions in the NOC - Other</li>
                                            <li class="visited">Other Details</li>
                                            <li class="visited">Self Declaration</li>
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
                                RENEW - INFRASTRUCTURE USE: Final Submit
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">
                            <b>(1). </b>
                        </td>
                        <td colspan="2">
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
                    </tr>
                    <tr>
                        <td></td>
                        <td>(ii) Name of Infrastructure
                        </td>
                        <td>
                            <asp:Label ID="lblNameOfInfrastructure" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>(iii) Details of Existing NOC :
                        </td>
                        <td>
                            <asp:Label ID="lblINFExistingNOCNumber" runat="server" Text="Label"></asp:Label><br />
                            <asp:Label ID="lblINFNOCValidity" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>(iv) Applied For Renewal :
                        </td>
                        <td>
                            <asp:Label ID="lblAppliedForRenewal" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>(v) State
                        </td>
                        <td>
                            <asp:Label ID="lblState" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>(vi) District
                        </td>
                        <td>
                            <asp:Label ID="lblDistrict" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>(vii) Sub District
                        </td>
                        <td>
                            <asp:Label ID="lblSubDistrict" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>(viii) Village/Town
                        </td>
                        <td>
                            <asp:Label ID="lblVillageTown" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>(ix) Apply Area Type
                        </td>
                        <td>Area Type -
                            <asp:Label ID="lblAreaType" runat="server" Text="Label" Font-Bold="true"></asp:Label><br />
                            Area Type Category -
                            <asp:Label ID="lblAreaTypeCatagory" runat="server" Text="Label" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>(x) Present Area Type
                        </td>
                        <td>Area Type -
                            <asp:Label ID="lblpresAreaType" runat="server" Text="Label" Font-Bold="true"></asp:Label><br />
                            Area Type Category -
                            <asp:Label ID="lblpresAreaTypeCatagory" runat="server" Text="Label" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">
                            <b>(2). </b>
                        </td>
                        <td colspan="2">
                            <b>Self Declaration</b>
                        </td>
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
                        <td colspan="3">
                            <b class="Coumpulsory">Note :</b>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3" class="Coumpulsory">
                            <asp:Label ID="lblProcessingFeeNonRefund" runat="server" Text=" a)  The Processing Fee is Non-Refundable. Applicant should 
                            ensure &quot;Check Eligibility&quot; and &quot;Documents Required&quot; before Submitting Application Online."
                                class="Coumpulsory"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td colspan="3" class="Coumpulsory">
                            <asp:Label CssClass="Coumpulsory" ID="lblSubmittedApplication" runat="server" Text="b)Scanned copy of signature and seal document should be attached at presribed place before submission of application.<br />Note:Signature and seal document can be obtain from Preview option in New-Save As Draft on Applicant Home Page."></asp:Label>
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
                        <td colspan="3">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblInfrastructureApplicationCodeFrom" Visible="false" runat="server"
                                Enabled="False"></asp:Label>
                            <asp:Label ID="lblFinalMsg" runat="server"></asp:Label>
                            <asp:Label ID="lblAppStop" Visible="false" runat="server" Enabled="False" Font-Bold="true" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="3">
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
