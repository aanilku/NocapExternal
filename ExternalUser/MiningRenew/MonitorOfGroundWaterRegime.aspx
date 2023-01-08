<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="MonitorOfGroundWaterRegime.aspx.cs"
    Inherits="ExternalUser_MiningRenew_MonitorOfGroundWaterRegime" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function GetData() {
            var txtMonitoGWRegimeDetails = document.getElementById("<%= txtMonitoGWRegimeDetails.ClientID %>")
            var txtMonitoGWRegimeDetails_array = document.getElementById("lbltxtMonitoGWRegimeDetails").value.split(' ');
            document.getElementById('lbltxtMonitoGWRegimeDetails').value = '( ' + parseInt(txtMonitoGWRegimeDetails_array[1] - txtMonitoGWRegimeDetails.value.length) + ' Character Left )';

            var txtNoOfWellsPiezometers = document.getElementById("<%= txtNoOfWellsPiezometers.ClientID %>")
            var txtNoOfWellsPiezometers_array = document.getElementById("lbltxtNoOfWellsPiezometers").value.split(' ');
            document.getElementById('lbltxtNoOfWellsPiezometers').value = '( ' + parseInt(txtNoOfWellsPiezometers_array[1] - txtNoOfWellsPiezometers.value.length) + ' Character Left )';

            var txtGWLevelofObservationwellsPiezometer = document.getElementById("<%= txtGWLevelofObservationwellsPiezometer.ClientID %>")
            var txtGWLevelofObservationwellsPiezometer_array = document.getElementById("lbltxtGWLevelofObservationwellsPiezometer").value.split(' ');
            document.getElementById('lbltxtGWLevelofObservationwellsPiezometer').value = '( ' + parseInt(txtGWLevelofObservationwellsPiezometer_array[1] - txtGWLevelofObservationwellsPiezometer.value.length) + ' Character Left )';

            var txtWSSurrounding = document.getElementById("<%= txtWSSurrounding.ClientID %>")
            var txtWSSurrounding_array = document.getElementById("lbltxtWSSurrounding").value.split(' ');
            document.getElementById('lbltxtWSSurrounding').value = '( ' + parseInt(txtWSSurrounding_array[1] - txtWSSurrounding.value.length) + ' Character Left )';

            var txtAnyOtherItem = document.getElementById("<%= txtAnyOtherItem.ClientID %>")
            var txtAnyOtherItem_array = document.getElementById("lbltxtAnyOtherItem").value.split(' ');
            document.getElementById('lbltxtAnyOtherItem').value = '( ' + parseInt(txtAnyOtherItem_array[1] - txtAnyOtherItem.value.length) + ' Character Left )';

            var txtDetailChangeGWRegim = document.getElementById("<%= txtDetailChangeGWRegim.ClientID %>")
            var txtDetailChangeGWRegim_array = document.getElementById("lbltxtDetailChangeGWRegim").value.split(' ');
            document.getElementById('lbltxtDetailChangeGWRegim').value = '( ' + parseInt(txtDetailChangeGWRegim_array[1] - txtDetailChangeGWRegim.value.length) + ' Character Left )';

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
                                            <li class="visited">Existing NOC Details</li>
                                            <li class="visited">Land Use Details</li>
                                            <li class="visited">Dewatering Existing Structure</li>
                                            <li class="visited">Dewatering Additional Structure</li>
                                            <li class="visited">Utilization of pumped water</li>
                                            <li class="active">Monitoring of groundwater regime</li>
                                            <li>Groundwater Abstraction Structure- Existing</li>
                                            <li>Groundwater Abstraction Structure- Additional</li>
                                            <li>Compliance Conditions in the NOC</li>
                                            <li>Compliance Conditions in the NOC - Other</li>
                                            <li>Other Details</li>
                                        
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
                                RENEW - MINING USE: Monitoring of Ground Water Regime
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="FormProjName">
                            <b>Project Name:&nbsp;
                                <asp:Label ID="lblHeadingProjName" runat="server"></asp:Label></b>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right" colspan="3">
                            (<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-
                            Upload Attachments in <strong style="color: Red">Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">
                            (15).
                        </td>
                        <td colspan="2">
                            <b>Monitoring of Ground Water Regime (Attach Map</b><strong>(</strong><span class="Coumpulsory"><strong>$</span>))
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 50%">
                            a). Location Details of the Wells / Piezometers (Latitute, Longitude, Reduced Level)
                            <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMonitoGWRegimeDetails" runat="server" TextMode="MultiLine" Height="45px"
                                onkeyup="CountCharacter(this, this.form.lbltxtMonitoGWRegimeDetails, 500);" onkeydown="CountCharacter(this, this.form.lbltxtMonitoGWRegimeDetails, 500);"
                                Width="100%" MaxLength="500"></asp:TextBox><br />
                            <input type="text" id="lbltxtMonitoGWRegimeDetails" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtMonitoGWRegimeDetails" size="2" maxlength="2" value="( 500 Character Left )"
                                readonly="readonly" />
                            <asp:RegularExpressionValidator ID="revtxtMonitoGWRegimeDetails" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtMonitoGWRegimeDetails" ValidationGroup="MonitorgroudnwaterRegime"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtMonitoGWRegimeDetails" ValidationGroup="MonitorgroudnwaterRegime"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtMonitoGWRegimeDetails" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="MonitorgroudnwaterRegime"
                                ControlToValidate="txtMonitoGWRegimeDetails"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            b). Number of Wells / Piezometers monitored at present <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNoOfWellsPiezometers" runat="server" TextMode="MultiLine" Height="45px"
                                onkeyup="CountCharacter(this, this.form.lbltxtNoOfWellsPiezometers, 500);" onkeydown="CountCharacter(this, this.form.lbltxtNoOfWellsPiezometers, 500);"
                                Width="100%" MaxLength="500"></asp:TextBox><br />
                            <input type="text" id="lbltxtNoOfWellsPiezometers" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtNoOfWellsPiezometers" size="2" maxlength="2" value="( 500 Character Left )"
                                readonly="readonly" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtNoOfWellsPiezometers" ValidationGroup="MonitorgroudnwaterRegime"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtNoOfWellsPiezometers" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtNoOfWellsPiezometers" ValidationGroup="MonitorgroudnwaterRegime"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtNoOfWellsPiezometers" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="MonitorgroudnwaterRegime"
                                ControlToValidate="txtNoOfWellsPiezometers"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            c). Attach Details of GW Level of Observation Wells / Piezometers( At Least for
                            One Year )(<span class="Coumpulsory">$</span>) <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGWLevelofObservationwellsPiezometer" runat="server" TextMode="MultiLine"
                                onkeyup="CountCharacter(this, this.form.lbltxtGWLevelofObservationwellsPiezometer, 100);"
                                onkeydown="CountCharacter(this, this.form.lbltxtGWLevelofObservationwellsPiezometer, 100);"
                                Height="45px" Width="100%" MaxLength="500"></asp:TextBox><br />
                            <input type="text" id="lbltxtGWLevelofObservationwellsPiezometer" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtGWLevelofObservationwellsPiezometer" size="2" maxlength="2" value="( 100 Character Left )"
                                readonly="readonly" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtGWLevelofObservationwellsPiezometer"
                                ValidationGroup="MonitorgroudnwaterRegime" ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtGWLevelofObservationwellsPiezometer" runat="server"
                                Display="Dynamic" ForeColor="Red" ControlToValidate="txtGWLevelofObservationwellsPiezometer"
                                ValidationGroup="MonitorgroudnwaterRegime"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtGWLevelofObservationwellsPiezometer"
                                runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="MonitorgroudnwaterRegime"
                                ControlToValidate="txtGWLevelofObservationwellsPiezometer"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            d). General Quality of Groundwater in the Area &amp; Surroundings <span class="Coumpulsory">*</span>(<span
                                class="Coumpulsory">$</span>)
                        </td>
                        <td>
                            <asp:TextBox ID="txtWSSurrounding" runat="server" TextMode="MultiLine" Height="45px"
                                onkeyup="CountCharacter(this, this.form.lbltxtWSSurrounding, 100);" onkeydown="CountCharacter(this, this.form.lbltxtWSSurrounding, 100);"
                                Width="100%" MaxLength="500"></asp:TextBox><br />
                            <input type="text" id="lbltxtWSSurrounding" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtWSSurrounding" size="2" maxlength="2" value="( 100 Character Left )"
                                readonly="readonly" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtWSSurrounding" ValidationGroup="MonitorgroudnwaterRegime"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtWSSurrounding" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtWSSurrounding" ValidationGroup="MonitorgroudnwaterRegime"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtWSSurrounding" runat="server" Display="Dynamic"
                                ForeColor="Red" ValidationGroup="MonitorgroudnwaterRegime" ControlToValidate="txtWSSurrounding"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            e). Any Other Item <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAnyOtherItem" runat="server" TextMode="MultiLine" Height="45px"
                                onkeyup="CountCharacter(this, this.form.lbltxtAnyOtherItem, 100);" onkeydown="CountCharacter(this, this.form.lbltxtAnyOtherItem, 100);"
                                Width="100%" MaxLength="500"></asp:TextBox>
                            <input type="text" id="lbltxtAnyOtherItem" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtAnyOtherItem" size="2" maxlength="2" value="( 100 Character Left )"
                                readonly="readonly" /><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtAnyOtherItem" ValidationGroup="MonitorgroudnwaterRegime"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtAnyOtherItem" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtAnyOtherItem" ValidationGroup="MonitorgroudnwaterRegime"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtAnyOtherItem" runat="server" Display="Dynamic"
                                ForeColor="Red" ValidationGroup="MonitorgroudnwaterRegime" ControlToValidate="txtAnyOtherItem"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            (16).
                        </td>
                        <td colspan="2">
                            <b>Give the details of change in groundwater regime and quality after execution of the
                                project (Attach detailed report with Map showing GW flow direction</b><strong>(</strong><span
                                    class="Coumpulsory"><strong>$</span>))<span class="Coumpulsory"> *</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtDetailChangeGWRegim" runat="server" TextMode="MultiLine" Height="45px"
                                onkeyup="CountCharacter(this, this.form.lbltxtDetailChangeGWRegim, 500);" onkeydown="CountCharacter(this, this.form.lbltxtDetailChangeGWRegim, 500);"
                                Width="100%" MaxLength="500"></asp:TextBox>
                            <input type="text" id="lbltxtDetailChangeGWRegim" tabindex="-1" style="border-width: 0px;
                                width: 100px; font-size: 10px; text-align: left; float: right; background-color: transparent"
                                name="lbltxtDetailChangeGWRegim" size="2" maxlength="2" value="( 500 Character Left )"
                                readonly="readonly" /><br />
                            <asp:RequiredFieldValidator ID="rfvtxtDetailChangeGWRegim" runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtDetailChangeGWRegim" ValidationGroup="MonitorgroudnwaterRegime"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtDetailChangeGWRegim" runat="server" Display="Dynamic"
                                ForeColor="Red" ControlToValidate="txtDetailChangeGWRegim" ValidationGroup="MonitorgroudnwaterRegime"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revLengthtxtDetailChangeGWRegim" runat="server"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="MonitorgroudnwaterRegime"
                                ControlToValidate="txtDetailChangeGWRegim"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblMiningApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="3">
                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('If you have not saved data, Your data will be lost before moving to other page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" ValidationGroup="MonitorgroudnwaterRegime"
                                OnClick="btnSaveAsDraft_Click" />
                            <asp:Button ID="btnNext" runat="server" Text="Next >>" ValidationGroup="MonitorgroudnwaterRegime"
                                OnClick="btnNext_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
