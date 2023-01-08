<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExternalUserLeftMenu.ascx.cs"
    Inherits="AscxControl_ExternalUserLeftMenu" %>
<%--<link type="text/css" rel="Stylesheet" runat="server" href="~/css/ExternalUserCss.css" />--%>
<%--<table align="left" class="style3">
    <tr>
        <td>--%>
<div class="content_outer" id="main-content">
    <div class="middle_new">
        <div class="block_left">
            <div class="hd_left" style="background: #094E7F;">
                <span class="hd_link_active" id="link_information">Information</span></div>
            <div class="cont_left" id="information" style="display: block">
                <div class="cont_left_pad">
                    <ul class="bullets1">
                        <%--                                    <li><a href="#" title="">Forms</a>
                                        <ul class="bullets">
                                            <li><a href="#" title="">Domestic</a></li>
                                            <li><a href="#" title="">Industrial</a> </li>
                                            <li><a href="#" title="">Infrastructure</a> </li>
                                            <li><a href="#" title="">Mining</a> </li>
                                        </ul>
                                    </li>--%>
                        <li><a runat="server" href="~/LandingPage/Guidelines.htm" title="" target="_blank">Guidelines</a></li>
                        <li><a runat="server" href="~/LandingPage/GuidelinesonlineFilling/steps_for_online_filling_of_application-12Feb2020.pdf#ZOOM=100"
                            title="" target="_blank">Steps for Filling Online Application</a></li>
                        <%--                                <li><a href="#" target="" title="">FAQ</a></li>
                                    <li><a href="#" title="">Online Filing</a></li>--%>
                    </ul>
                </div>
            </div>
            <div class="hd_left" style="background: #094E7F;">
                <%--<span class="hd_link_default">Check List</span>--%>
                <span class="hd_link_default">Documents Required</span>
            </div>
            <div class="cont_left" style="display: block">
                <div class="cont_left_pad">
                    <ul class="bullets1">
                        <%--<li style="color: #0066CC;">Check List for Online Application </li>--%>
                        <li style="color: #0066CC;">Documents Required for Online Application </li>
                        <%--<ul class="bullets">
                                            <li><a target="_blank" id="A1" runat="server" href="~/LandingPage/CheckLists/CheckList-Ind.pdf" title="">Industrial</a></li>
                                            <li><a target="_blank" id="A2" runat="server" href="~/LandingPage/CheckLists/ChekcList-Inf.pdf" title="">Infrastructure</a></li>
                                            <li><a target="_blank" id="A3" runat="server" href="~/LandingPage/CheckLists/CheckList-Min.pdf" title="">Mining</a></li>
                                        </ul>--%>
                        <ul class="bullets">
                            <li><a target="_blank" id="A1" runat="server" href="~/LandingPage/DocRecInd.htm"
                                title="">Industrial</a></li>
                            <li><a target="_blank" id="A2" runat="server" href="~/LandingPage/DocRecInf.htm"
                                title="">Infrastructure</a></li>
                            <li><a target="_blank" id="A3" runat="server" href="~/LandingPage/DocRecMining.htm"
                                title="">Mining</a></li>
                        </ul>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="hd_left" style="background: #094E7F;">
                <span class="hd_link_default">Track Status</span></div>
            <div class="cont_left" style="display: block">
                <div class="cont_left_pad">
                    <ul class="bullets1">
                        <li style="color: #0066CC;">Application Status </li>
                        <ul class="bullets">
                            <%--                                            <li><a runat="server" href="~/ExternalUser/SearchForUpdateStatus.aspx" title="">Online</a></li>
                            --%>
                            <li><a id="A5" runat="server" href="~/ExternalUser/ApplicantHome.aspx" title="">Online</a></li>
                            <%-- <li><a href="#" title="">Mobile</a></li>--%>
                        </ul>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="hd_left" style="background: #094E7F;">
                <span class="hd_link_default">Location</span></div>
            <div class="cont_left" style="display: block">
                <div class="cont_left_pad">
                    <ul class="bullets1">
                      
                         <li><a id="A4" href="~/Sub/Report/AreaType/AreaType.aspx" runat="server" title="">Area
                            Type</a></li>
                        <li><a id="A8" href="~/Sub/Report/AreaType/SegmentBAreaType.aspx" runat="server"
                            title="">Segment-B Area Type</a></li>
                        <%--    <li><a href="#" target="" title="">Allowed Application Type</a></li>--%>
                        <!--<li><a href="~/Sub/DistrictOfficeLocator/DistrictOffLocation.aspx" runat="server" id="DistrictOffLocation" target="" title="">District office Location</a></li>-->
                        <%--<li><asp:LinkButton Text="District Office Location" ID="lbtnDisOfficeLoc" runat="server" PostBackUrl="~/Sub/DistrictOfficeLocator/DistrictOffLocation.aspx"></asp:LinkButton></li>--%>
                        <li><a href="~/Sub/RegionalOfficeLocator/RegionalOfficeLocator.aspx" runat="server"
                            id="RegionalOffLocation" target="" title="">Regional office Location</a></li>
                        <li><a runat="server" href="~/LandingPage/HQ.htm" target="_blank" title="">CGWA Headquarters</a></li>
                        <li><a id="A9" href="~/Sub/Report/EC/KnowYourEC.aspx" runat="server"
                            title="">Know Your Environmental Compensation(EC)</a></li>
                         <li><a id="A10" href="~/Sub/Report/Penalty/Penalty.aspx" runat="server"
                            title="">Know Your Penalty</a></li>
                         <li><a id="A11" href="~/Sub/Report/GWChargesCalculation/GWChargesCalculation.aspx" runat="server"
                            title="">Ground Water Abstraction/Restoration Charges</a></li>
                    </ul>
                </div>
            </div>
            <div class="hd_left" style="background: #094E7F;">
                <span class="hd_link_default">Reports</span></div>
            <div class="cont_left_pad">
                <ul class="bullets1">
                    <li><a id="A7" href="~/Sub/Report/AppliedForNOC/AppliedForNOC.aspx" runat="server"
                        title="">Applied for NOC - Online</a></li>
                    <li><a id="A6" href="~/Sub/Report/NOCIssuedLetter/NOCIssusedLetterToExtUser.aspx"
                        runat="server" title="">NOC Issued-Online</a></li>
                </ul>
            </div>
            <div class="hd_left" style="background: #094E7F;">
                <span class="hd_link_default">Contact Us</span></div>
            <div class="cont_left" style="display: block">
                <div class="cont_left_pad">
                    <ul class="bullets1">
                        <li><a runat="server" href="~/LandingPage/ContactUs.htm" target="_blank" title="">Contact</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</div>
<%-- </td>
    </tr>
</table>--%>
