<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="De-WateringProposedStructure.aspx.cs"
    Inherits="ExternalUser_Mining_De_WateringProposedStructure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
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
                                            <li class="visited">Dewatering Existing Structure</li>
                                            <li class="active">Dewatering Proposed Structure</li>
                                            <li>Utilization of pumped water</li>
                                            <li>Monitoring of groundwater regime</li>
                                            <li>Groundwater Abstraction Structure- Existing</li>
                                            <li>Groundwater Abstraction Structure- Proposed</li>
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
                                MINING USE: De-Watering Proposed Structure Detail</div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="3">
                            (<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-Upload
                            Attachments in <strong style="color: Red">Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">
                        </td>
                        <td colspan="2">
                            <b>(b). De-Watering Proposed Structure</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 40%">
                            Number of Proposed Structures: <span class="Coumpulsory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNoOfProposedStructure" runat="server" MaxLength="3" Width="200px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtNoOfProposedStructure"
                                Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="deWateringPrposedStructDetail"></asp:RequiredFieldValidator><br />
                            <asp:RegularExpressionValidator ID="revtxtNoOfProposedStructure" runat="server" ForeColor="Red"
                                ControlToValidate="txtNoOfProposedStructure" Display="Dynamic" ValidationGroup="deWateringPrposedStructDetail"></asp:RegularExpressionValidator>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" Type="Integer" ControlToValidate="txtNoOfProposedStructure"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="deWateringPrposedStructDetail"
                                ErrorMessage="Value can't be more than 1000" MaximumValue="1000" MinimumValue="0"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            <fieldset>
                                <legend><b>Details:</b></legend>
                                <table class="SubFormWOBG" width="100%" style="line-height: 25px">
                                    <tr>
                                        <td style="width: 41%">
                                            Type of Structure: <span class="Coumpulsory">*</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTypeOfStructure" runat="server" Width="200px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                Display="Dynamic" ValidationGroup="deWateringPrposedStruct" InitialValue=" "
                                                ControlToValidate="ddlTypeOfStructure">Required</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Year of Construction:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtYearOfConstruction" runat="server" MaxLength="4" Width="200px"></asp:TextBox><br />
                                            <asp:RangeValidator ID="RngValYearOfConstruction" runat="server" ControlToValidate="txtYearOfConstruction"
                                                Display="Dynamic" Type="Integer" MinimumValue="1900" MaximumValue="3000" ForeColor="Red"
                                                ErrorMessage="Invalid Year" ValidationGroup="deWateringPrposedStruct"></asp:RangeValidator>
                                            <asp:RegularExpressionValidator ID="revtxtYearOfConstruction" runat="server" ForeColor="Red"
                                                Display="Dynamic" ControlToValidate="txtYearOfConstruction" ValidationGroup="deWateringPrposedStruct"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Depth (Meter):
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDepthMeter" runat="server" MaxLength="7" Width="200px"></asp:TextBox><br />
                                            <asp:RangeValidator ID="RangeValidator2" runat="server" Type="Double" ControlToValidate="txtDepthMeter"
                                                Display="Dynamic" ForeColor="Red" ValidationGroup="deWateringPrposedStruct" ErrorMessage="Value Range 0.01 to 1000"
                                                MaximumValue="1000.00" MinimumValue="0.01"></asp:RangeValidator>
                                            <asp:RegularExpressionValidator ID="revtxtDepthMeter" runat="server" Display="Dynamic"
                                                ForeColor="Red" ControlToValidate="txtDepthMeter" ValidationGroup="deWateringPrposedStruct"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Diameter (mm):
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDiameterMM" runat="server" MaxLength="6" Width="200px"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="revtxtDiameterMM" runat="server" Display="Dynamic"
                                                ForeColor="Red" ControlToValidate="txtDiameterMM" ValidationExpression="([0-9])*"
                                                ErrorMessage="Invalid Value" ValidationGroup="deWateringPrposedStruct"></asp:RegularExpressionValidator>
                                            <asp:RangeValidator ID="RangeValidator3" runat="server" Display="Dynamic" Type="Integer"
                                                ControlToValidate="txtDiameterMM" ForeColor="Red" ValidationGroup="deWateringPrposedStruct"
                                                ErrorMessage="Value can't be more than 999999" MaximumValue="999999" MinimumValue="0"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Depth to Water Level (Meters below Ground Level):
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDepthWaterBelowGroundLevel" runat="server" MaxLength="7" Width="200px"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="revtxtDepthWaterBelowGroundLevel" runat="server"
                                                ForeColor="Red" Display="Dynamic" ControlToValidate="txtDepthWaterBelowGroundLevel"
                                                ValidationGroup="deWateringPrposedStruct"></asp:RegularExpressionValidator>
                                            <asp:RangeValidator ID="RangeValidator4" runat="server" Display="Dynamic" Type="Double"
                                                ControlToValidate="txtDepthWaterBelowGroundLevel" ForeColor="Red" ValidationGroup="deWateringPrposedStruct"
                                                ErrorMessage="Value Range 0.01 to 1000" MaximumValue="1000.00" MinimumValue="0.01"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Discharge (m&sup3/Hour):
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDischarge" runat="server" MaxLength="7" Width="200px"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="revtxtDischarge" runat="server" Display="Dynamic"
                                                ForeColor="Red" ControlToValidate="txtDischarge" ValidationGroup="deWateringPrposedStruct"></asp:RegularExpressionValidator>
                                            <asp:RangeValidator ID="RangeValidator5" runat="server" Display="Dynamic" Type="Double"
                                                ControlToValidate="txtDischarge" ForeColor="Red" ValidationGroup="deWateringPrposedStruct"
                                                ErrorMessage="Value Range 0.01 to 1000" MaximumValue="1000.00" MinimumValue="0.01"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Operational Hours / Day:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOperationalHoursDay" runat="server" MaxLength="2" Width="200px"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="revtxtOperationalHoursDay" runat="server" Display="Dynamic"
                                                ForeColor="Red" ControlToValidate="txtOperationalHoursDay" ValidationGroup="deWateringPrposedStruct"></asp:RegularExpressionValidator>
                                            <asp:RangeValidator ID="RangeValidator6" runat="server" Display="Dynamic" Type="Integer"
                                                ControlToValidate="txtOperationalHoursDay" ForeColor="Red" ValidationGroup="deWateringPrposedStruct"
                                                ErrorMessage="Value can't be more than 24" MaximumValue="24" MinimumValue="0"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Operational Days / Year:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOperationalDaysYear" runat="server" MaxLength="3" Width="200px"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="revtxtOperationalDaysYear" runat="server" Display="Dynamic"
                                                ForeColor="Red" ControlToValidate="txtOperationalDaysYear" ValidationGroup="deWateringPrposedStruct"></asp:RegularExpressionValidator>
                                            <asp:RangeValidator ID="RangeValidator7" runat="server" Display="Dynamic" Type="Integer"
                                                ControlToValidate="txtOperationalDaysYear" ForeColor="Red" ValidationGroup="deWateringPrposedStruct"
                                                ErrorMessage="Value can't be more than 365" MaximumValue="365" MinimumValue="0"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Mode of Lift:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlModeOfLift" runat="server" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Horse Power of Pump:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtHorsePowerPump" runat="server" MaxLength="6" Width="200px"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="revtxtHorsePowerPump" runat="server" ForeColor="Red"
                                                ControlToValidate="txtHorsePowerPump" Display="Dynamic" ValidationGroup="deWateringPrposedStruct"></asp:RegularExpressionValidator>
                                            <asp:RangeValidator ID="RangeValidator8" runat="server" Display="Dynamic" Type="Double"
                                                ControlToValidate="txtHorsePowerPump" ForeColor="Red" ValidationGroup="deWateringPrposedStruct"
                                                ErrorMessage="Value can't be more than 999.99" MaximumValue="999.99" MinimumValue="0.01"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Whether fitted with Water Meter:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlWhetherFittedWithWaterMeter" runat="server" Width="200px">
                                                <asp:ListItem>Yes</asp:ListItem>
                                                <asp:ListItem>No</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Whether Permission / Registered with CGWA:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlWhetherPermissionRegisteredWithCGWA" runat="server" Width="200px"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlWhetherPermissionRegisteredWithCGWA_SelectedIndexChanged">
                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                <asp:ListItem Value="No">No</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            If so Details thereof:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWhetherPermissionRegisteredWithCGWADet" runat="server" MaxLength="100"
                                                onkeyup="CountCharacter(this, this.form.ActionCommentRemCount, 100);" onkeydown="CountCharacter(this, this.form.ActionCommentRemCount, 100);"
                                                TextMode="MultiLine" Width="300px" Height="50px"></asp:TextBox><br />
                                            <input type="text" id="ActionCommentRemCount" tabindex="-1" style="border-width: 0px;
                                                width: 100px; font-size: 10px; text-align: left; margin-left: 210px; background-color: transparent"
                                                name="ActionCommentRemCount" size="2" maxlength="2" value="( 100 Character Left )"
                                                readonly="readonly" /><br />
                                            <asp:RegularExpressionValidator ID="revtxtWhetherPermissionRegisteredWithCGWADet"
                                                runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtWhetherPermissionRegisteredWithCGWADet"
                                                ValidationGroup="deWateringPrposedStruct"></asp:RegularExpressionValidator>
                                            <asp:RegularExpressionValidator ID="revLengthtxtWhetherPermissionRegisteredWithCGWADet"
                                                runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="deWateringPrposedStruct"
                                                ControlToValidate="txtWhetherPermissionRegisteredWithCGWADet"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAdd" runat="server" Text="Add In List" ValidationGroup="deWateringPrposedStruct"
                                                OnClick="btnAdd_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                                            <asp:Label ID="lblMiningApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvdeWateringPrposed" runat="server" AutoGenerateColumns="false"
                    DataKeyNames="ApplicationCode" Caption="<th colspan='15'><b>Detail of De-Watering Proposed Structure</b></th>"
                    CssClass="SubFormWOBG" OnRowCommand="gvdeWateringPrposed_RowCommand" OnRowDataBound="gvdeWateringPrposed_RowDataBound"
                    ShowHeaderWhenEmpty="True">
                    <Columns>
                        <asp:TemplateField HeaderText="SNo.">
                            <ItemTemplate>
                                <span>
                                    <%#Container.DataItemIndex + 1 %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Serial Number" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="SerialNumber" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("SerialNumber")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type of Structure Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="TypeOfAbstractionStructureCode" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TypeOfAbstractionStructureCode")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type of Structure Name">
                            <ItemTemplate>
                                <asp:Label ID="TypeOfAbstractionStructureName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Year of Construction">
                            <ItemTemplate>
                                <asp:Label ID="YearOfConstruction" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("YearOfConstruction")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Depth (Meter)">
                            <ItemTemplate>
                                <asp:Label ID="DepthExist" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("DepthExist")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Diameter (mm)">
                            <ItemTemplate>
                                <asp:Label ID="Diameter" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Diameter")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Depth to Water level (Meters below Ground Level)">
                            <ItemTemplate>
                                <asp:Label ID="DepthBelowWaterLevel" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("DepthBelowWaterLevel")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Discharge (m3/Hour)">
                            <ItemTemplate>
                                <asp:Label ID="Discharge" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Discharge")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Operational Hours/Day">
                            <ItemTemplate>
                                <asp:Label ID="OperationalHousrDay" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("OperationalHousrDay")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Operational Days/Year">
                            <ItemTemplate>
                                <asp:Label ID="OperationalDaysYear" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("OperationalDaysYear")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mode of lift Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="LiftingDeviceCode" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("LiftingDeviceCode")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mode of Lift Name">
                            <ItemTemplate>
                                <asp:Label ID="LiftingDeviceName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Horse Power of Pump">
                            <ItemTemplate>
                                <asp:Label ID="PowerOfPump" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("PowerOfPump")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Whether fitted with Water Meter">
                            <ItemTemplate>
                                <asp:Label ID="WaterFittedWithWaterMeterOrNot" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("WaterFittedWithWaterMeterOrNot")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Whether Permission/Registered with CGWA">
                            <ItemTemplate>
                                <asp:Label ID="WhetherPermissionRegisteredWithCGWA" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("WhetherPermissionRegisteredWithCGWA"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="If so Details Thereof">
                            <ItemTemplate>
                                <asp:Label ID="WhetherPermissionRegisteredWithCGWADetails" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("WhetherPermissionRegisteredWithCGWADetails")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnDelete" runat="server" Height="20px" ImageUrl="~/Images/delete.jpg"
                                    Width="20px" OnClientClick="return confirm('Are you sure you want to delete?');"
                                    CommandName="DeleteName" CommandArgument='<%#Container.DataItemIndex + 1 %>'
                                    ToolTip="Delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Records exist in De-Watering Proposed Structure.
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="text-align: center">
                <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('Please Save as Draft before moving to Previous Page ?');"
                    OnClick="btnPrev_Click" />
                <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" ValidationGroup="deWateringPrposedStructDetail"
                    OnClick="btnSaveAsDraft_Click" />
                <asp:Button ID="txtNext" runat="server" Text="Next >>" ValidationGroup="deWateringPrposedStructDetail"
                    OnClick="txtNext_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
