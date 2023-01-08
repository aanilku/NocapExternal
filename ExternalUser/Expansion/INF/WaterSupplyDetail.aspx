<%@ Page Title="NOCAP-Infrastructure Application-Expansion" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="WaterSupplyDetail.aspx.cs"
    Inherits="ExternalUser_Expansion_INF_WaterSupplyDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function GetData() {
            var txtGroundwaterAvailabilityDetails = document.getElementById("<%= txtGroundwaterAvailabilityDetails.ClientID %>")
            var txtGroundwaterAvailabilityDetails_array = document.getElementById("lbltxtGroundwaterAvailabilityDetails").value.split(' ');
            document.getElementById('lbltxtGroundwaterAvailabilityDetails').value = '( ' + parseInt(txtGroundwaterAvailabilityDetails_array[1] - txtGroundwaterAvailabilityDetails.value.length) + ' Character Left )';

        }

        function CountCharacter(controlId, countControlId, maxCharlimit) {
            var charLeft = maxCharlimit - controlId.value.length;
            countControlId.value = '( ' + parseInt(maxCharlimit - controlId.value.length) + ' Character Left )';
            if (charLeft < 0) {
                countControlId.style.color = "Red";
                //msgCharacterleftId.style.color = "Red";
                //Bracket1.style.color = "Red";
                //Bracket2.style.color = "Red";
            }
            else {
                countControlId.style.color = "Black";
                //msgCharacterleftId.style.color = "Black";
                //Bracket1.style.color = "Black";
                //Bracket2.style.color = "Black";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
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
                                            <li class="visited">De-Watering Existing Structure</li>
                                            <li class="visited">De-Watering Proposed Structure</li>
                                            <li class="visited">Groundwater Abstraction Structure-Existing</li>
                                            <li class="visited">Groundwater Abstraction Structure-Proposed</li>
                                            <li class="visited">Breakup of Water Requirment</li>
                                            <li class="active">Water Supply Detail</li>
                                            <%--<li>Groundwater Abstraction Structure-Existing</li>
                                            <li>Groundwater Abstraction Structure-Proposed</li>--%>
                                            <li>Other Details</li>
                                       <%--     <li>Self Declaration</li>--%>
                                            <li>Attachment</li>
                                              <li>Ready To Submit</li>
                                            <li>Final Submit</li>
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
                                INFRASTRUCTURE EXPANSION USE- Water Supply Details
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="3">
                            (<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-Upload
                            Attachments in <strong style="color: Red">Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20px">
                            (4).
                        </td>
                        <td>
                            Whether Local Goverment Water Supply Network Exists in the Area, if yes, provide
                            details. (<span class="Coumpulsory">$</span>)
                        </td>
                        <td style="width:200px">
                            <asp:DropDownList ID="ddlGovtWaterSupplyExists" runat="server" AutoPostBack="true"
                                Width="200px" OnSelectedIndexChanged="ddlGovtWaterSupplyExists_SelectedIndexChanged">
                                <asp:ListItem Value="Yes" Text="Yes"> </asp:ListItem>
                                <asp:ListItem Value="No" Text="No"> </asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            Details :
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtGroundwaterAvailabilityDetails" runat="server" Height="50px"
                                onkeyup="CountCharacter(this, this.form.lbltxtGroundwaterAvailabilityDetails, 500);"
                                onkeydown="CountCharacter(this, this.form.lbltxtGroundwaterAvailabilityDetails, 500);"
                                MaxLength="500" TextMode="MultiLine" Width="99%"></asp:TextBox>
                            <input type="text" id="lbltxtGroundwaterAvailabilityDetails" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtGroundwaterAvailabilityDetails" size="2" maxlength="2" value="( 500 Character Left )"
                                readonly="readonly" />
                            <asp:RegularExpressionValidator ID="revtxtGroundwaterAvailabilityDetails" runat="server"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtGroundwaterAvailabilityDetails"
                                ValidationGroup="OtherDetails"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtGroundwaterAvailabilityDetails" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="GASProposed"
                                ControlToValidate="txtGroundwaterAvailabilityDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr id="trWaterSupplyActionStatus" runat="server" visible="false">
                        <td>
                            (5).
                        </td>
                        <td>
                            (i) Water Supply Application Status
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlWaterSupplyActionStatus" runat="server" Width="200px">
                                <asp:ListItem Text="Applied"> </asp:ListItem>
                                <asp:ListItem Text="Obtained"> </asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trWaterSupplyAgency" runat="server" visible="false">
                        <td>
                        </td>
                        <td>
                            (ii) Water Supply Agency
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlWaterSupplyAgency" runat="server" Width="200px">
                                <asp:ListItem Text="Goverment"> </asp:ListItem>
                                <asp:ListItem Text="Private"> </asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trWaterSupplyCommited" runat="server" visible="false">
                        <td>
                        </td>
                        <td>
                            a) Whether Water Supply 
                            Committed (<span class="Coumpulsory">$</span>):
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlWaterSupplyCommited" runat="server" Width="200px">
                                <asp:ListItem Value="Yes" Text="Yes"> </asp:ListItem>
                                <asp:ListItem Value="No" Text="No"> </asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trWaterSupplyDenied" runat="server" visible="false">
                        <td>
                        </td>
                        <td>
                            b) Whether Water Supply 
                            Denied (<span class="Coumpulsory">$</span>):
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlWaterSupplyDenied" runat="server" Width="200px">
                                <asp:ListItem Value="Yes" Text="Yes"> </asp:ListItem>
                                <asp:ListItem Value="No" Text="No"> </asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblInfrastructureApplicationCodeFrom" Visible="false" runat="server"
                                Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="3">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('Please Save as Draft before moving to Previous Page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" ValidationGroup="OtherDetails"
                                OnClick="btnSaveAsDraft_Click" />
                            <asp:Button ID="txtNext" runat="server" Text="Next >>" ValidationGroup="OtherDetails"
                                OnClick="txtNext_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
