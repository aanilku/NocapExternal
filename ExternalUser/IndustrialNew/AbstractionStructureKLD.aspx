<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true"
    CodeFile="AbstractionStructureKLD.aspx.cs" MaintainScrollPositionOnPostback="true"
     Inherits="ExternalUser_IndustrialNew_AbstractionStructureKLD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js"></script>
    <style type="text/css">
        /* Custom style */
        .accordion-button::after {
            background-image: url("data:image/svg+xml,%3csvg viewBox='0 0 16 16' fill='%23333' xmlns='http://www.w3.org/2000/svg'%3e%3cpath fill-rule='evenodd' d='M8 0a1 1 0 0 1 1 1v6h6a1 1 0 1 1 0 2H9v6a1 1 0 1 1-2 0V9H1a1 1 0 0 1 0-2h6V1a1 1 0 0 1 1-1z' clip-rule='evenodd'/%3e%3c/svg%3e");
            transform: scale(.7) !important;
        }

        .accordion-button:not(.collapsed)::after {
            background-image: url("data:image/svg+xml,%3csvg viewBox='0 0 16 16' fill='%23333' xmlns='http://www.w3.org/2000/svg'%3e%3cpath fill-rule='evenodd' d='M0 8a1 1 0 0 1 1-1h14a1 1 0 1 1 0 2H1a1 1 0 0 1-1-1z' clip-rule='evenodd'/%3e%3c/svg%3e");
        }
    </style>
    <script type="text/javascript" language="javascript">
        function CountCharacter(controlId, countControlId, maxCharlimit) {
            var charLeft = maxCharlimit - controlId.value.length;
            countControlId.value = '( ' + parseInt(maxCharlimit - controlId.value.length) + ' Character Left )';
            if (charLeft < 0) {
                countControlId.style.color = "Red";

            }
            else {
                countControlId.style.color = "Black";

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
                                            <li class="active">Groundwater Abstraction Structure</li>
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
                        <td colspan="3" style="text-align: right">(<span class="Coumpulsory">*</span>)- Mandatory Fields, (<span class="Coumpulsory">$</span>)-Upload
                            Attachments in <strong>Attachment</strong> Section
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div class="m-6">
                                <div class="accordion" id="myAccordion">
                                    <div class="accordion-item">
                                        <h2 class="accordion-header" id="headingOne">
                                            <button type="button" class="accordion-button"
                                                data-bs-toggle="collapse"
                                                data-bs-target="#collapseOne">
                                                1. Groundwater Abstraction Structure- Existing</button>
                                        </h2>
                                        <div id="collapseOne" class="accordion-collapse collapse show"
                                            data-bs-parent="#myAccordion">
                                            <div class="card-body">
                                                <table>
                                                    <tr>
                                                        <td style="width: 35%">Number of Existing Structures: <span class="Coumpulsory">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNumbeOfExistingGAS" runat="server" MaxLength="3" Width="200px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtNumbeOfExistingGAS"
                                                                Display="Dynamic" ErrorMessage="Required" ForeColor="Red" 
                                                                ValidationGroup="GS1"></asp:RequiredFieldValidator><br />
                                                            <asp:RegularExpressionValidator ID="revtxtNumbeOfExistingGAS" runat="server" ForeColor="Red"
                                                                ControlToValidate="txtNumbeOfExistingGAS" Display="Dynamic" ValidationGroup="GS1"></asp:RegularExpressionValidator>
                                                            <asp:RangeValidator ID="RangeValidator1" runat="server" Type="Integer" ControlToValidate="txtNumbeOfExistingGAS"
                                                                Display="Dynamic" ForeColor="Red" ValidationGroup="GS1" ErrorMessage="Value can't be more than 100"
                                                                MaximumValue="100" MinimumValue="0"></asp:RangeValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <fieldset>
                                                                <legend><b>Details:</b></legend>
                                                                <table class="SubFormWOBG" width="100%" style="line-height: 20px">
                                                                    <tr>
                                                                        <td style="width: 35%">Type of Structure: <span class="Coumpulsory">*</span>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlTypeOfStructure" runat="server" Width="200px">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator runat="server" ForeColor="Red"
                                                                                Display="Dynamic" ValidationGroup="GASExisting" InitialValue=" " ControlToValidate="ddlTypeOfStructure">Required</asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Year of Construction:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtYearOfConstruction" runat="server" MaxLength="4" Width="200px"></asp:TextBox><br />
                                                                            <asp:RangeValidator ID="RngValYearOfConstruction"
                                                                                 runat="server" ControlToValidate="txtYearOfConstruction"
                                                                                Display="Dynamic" Type="Integer" MinimumValue="1900" 
                                                                                ForeColor="Red" ErrorMessage="Invalid Year"
                                                                                ValidationGroup="GASExisting"></asp:RangeValidator>
                                                                            <asp:RegularExpressionValidator ID="revtxtYearOfConstruction" runat="server" ForeColor="Red" Display="Dynamic"
                                                                                ControlToValidate="txtYearOfConstruction" ValidationGroup="GASExisting"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Depth (Meter):
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtDepthMeter" runat="server" MaxLength="7" Width="200px"></asp:TextBox><br />
                                                                            <asp:RangeValidator ID="RangeValidator2" runat="server" Type="Double" ControlToValidate="txtDepthMeter"
                                                                                Display="Dynamic" ForeColor="Red" ValidationGroup="GASExisting" ErrorMessage="Value Range 0.01 to 1000"
                                                                                MaximumValue="1000.00" MinimumValue="0.01"></asp:RangeValidator>
                                                                            <asp:RegularExpressionValidator ID="revtxtDepthMeter" runat="server" Display="Dynamic"
                                                                                ForeColor="Red" ControlToValidate="txtDepthMeter" ValidationGroup="GASExisting"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Diameter (mm):
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtDiameterMM" runat="server" MaxLength="6" Width="200px"></asp:TextBox><br />
                                                                            <asp:RegularExpressionValidator ID="revtxtDiameterMM" runat="server" Display="Dynamic"
                                                                                ForeColor="Red" ControlToValidate="txtDiameterMM" ValidationGroup="GASExisting"></asp:RegularExpressionValidator>
                                                                            <asp:RangeValidator ID="RangeValidator3" runat="server" Display="Dynamic" Type="Integer"
                                                                                ControlToValidate="txtDiameterMM" ForeColor="Red" ValidationGroup="GASExisting"
                                                                                ErrorMessage="Value can't be more than 999999" MaximumValue="999999" MinimumValue="0"></asp:RangeValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Depth to Water Level (Meters below Ground Level):
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtDepthWaterBelowGroundLevel" runat="server" MaxLength="7" Width="200px"></asp:TextBox><br />
                                                                            <asp:RegularExpressionValidator ID="revtxtDepthWaterBelowGroundLevel" runat="server"
                                                                                ForeColor="Red" Display="Dynamic" ControlToValidate="txtDepthWaterBelowGroundLevel"
                                                                                ValidationGroup="GASExisting"></asp:RegularExpressionValidator>
                                                                            <asp:RangeValidator ID="RangeValidator4" runat="server" Display="Dynamic" Type="Double"
                                                                                ControlToValidate="txtDepthWaterBelowGroundLevel" ForeColor="Red" ValidationGroup="GASExisting"
                                                                                ErrorMessage="Value Range 0.01 to 1000" MaximumValue="1000.00" MinimumValue="0.01"></asp:RangeValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Discharge (m<sup>3</sup>/Hour):
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtDischarge" runat="server" MaxLength="7" Width="200px"></asp:TextBox><br />
                                                                            <asp:RegularExpressionValidator ID="revtxtDischarge" runat="server" Display="Dynamic"
                                                                                ForeColor="Red" ControlToValidate="txtDischarge" ValidationGroup="GASExisting"></asp:RegularExpressionValidator>
                                                                            <asp:RangeValidator ID="RangeValidator5" runat="server" Display="Dynamic" Type="Double"
                                                                                ControlToValidate="txtDischarge" ForeColor="Red" ValidationGroup="GASExisting"
                                                                                ErrorMessage="Value Range 0.01 to 1000" MaximumValue="1000.00" MinimumValue="0.01"></asp:RangeValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Operational Hours / Day:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtOperationalHoursDay" runat="server" MaxLength="2" Width="200px"></asp:TextBox><br />
                                                                            <asp:RegularExpressionValidator ID="revtxtOperationalHoursDay" runat="server" Display="Dynamic"
                                                                                ForeColor="Red" ControlToValidate="txtOperationalHoursDay" ValidationGroup="GASExisting"></asp:RegularExpressionValidator>
                                                                            <asp:RangeValidator ID="RangeValidator6" runat="server" Display="Dynamic" Type="Integer"
                                                                                ControlToValidate="txtOperationalHoursDay" ForeColor="Red" ValidationGroup="GASExisting"
                                                                                ErrorMessage="Value can't be more than 24" MaximumValue="24" MinimumValue="0"></asp:RangeValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Operational Days / Year:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtOperationalDaysYear" runat="server" MaxLength="3" Width="200px"></asp:TextBox><br />
                                                                            <asp:RegularExpressionValidator ID="revtxtOperationalDaysYear" runat="server" Display="Dynamic"
                                                                                ForeColor="Red" ControlToValidate="txtOperationalDaysYear" ValidationGroup="GASExisting"></asp:RegularExpressionValidator>
                                                                            <asp:RangeValidator ID="RangeValidator7" runat="server" Display="Dynamic" Type="Integer"
                                                                                ControlToValidate="txtOperationalDaysYear" ForeColor="Red" ValidationGroup="GASExisting"
                                                                                ErrorMessage="Value can't be more than 365" MaximumValue="365" MinimumValue="0"></asp:RangeValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Mode of Lift:
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlModeOfLift" runat="server" Width="200px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Horse Power of Pump:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtHorsePowerPump" runat="server" MaxLength="6" Width="200px"></asp:TextBox><br />
                                                                            <asp:RegularExpressionValidator ID="revtxtHorsePowerPump" runat="server" ForeColor="Red"
                                                                                ControlToValidate="txtHorsePowerPump" Display="Dynamic" ValidationGroup="GASExisting"></asp:RegularExpressionValidator>
                                                                            <asp:RangeValidator ID="RangeValidator8" runat="server" Display="Dynamic" Type="Double"
                                                                                ControlToValidate="txtHorsePowerPump" ForeColor="Red" ValidationGroup="GASExisting"
                                                                                ErrorMessage="Value can't be more than 999.99" MaximumValue="999.99" MinimumValue="0.01"></asp:RangeValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Whether Fitted with Water Meter:
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlWhetherFittedWithWaterMeter" runat="server" Width="200px">
                                                                                <asp:ListItem>Yes</asp:ListItem>
                                                                                <asp:ListItem>No</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Whether Permission / Registered with CGWA:
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
                                                                        <td>If so Details thereof:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtWhetherPermissionRegisteredWithCGWADet" runat="server" MaxLength="100"
                                                                                onkeyup="CountCharacter(this, this.form.ActionCommentRemCount, 100);" onkeydown="CountCharacter(this, this.form.ActionCommentRemCount, 100);"
                                                                                TextMode="MultiLine" Width="300px" Height="50px"></asp:TextBox><br />
                                                                            <input type="text" id="ActionCommentRemCount" tabindex="-1" style="border-width: 0px; width: 100px; font-size: 10px; text-align: left; margin-left: 210px; background-color: transparent"
                                                                                name="ActionCommentRemCount" size="2" maxlength="2" value="( 100 Character Left )"
                                                                                readonly="readonly" /><br />
                                                                            <asp:RegularExpressionValidator ID="revtxtWhetherPermissionRegisteredWithCGWADet"
                                                                                runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtWhetherPermissionRegisteredWithCGWADet"
                                                                                ValidationGroup="GASExisting"></asp:RegularExpressionValidator>
                                                                            <asp:RegularExpressionValidator ID="revLengthtxtWhetherPermissionRegisteredWithCGWADet"
                                                                                runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="GASExisting"
                                                                                ControlToValidate="txtWhetherPermissionRegisteredWithCGWADet"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center" colspan="2">
                                                                            <asp:Button ID="btnAdd" runat="server" Text="Add In List"
                                                                                OnClick="btnAdd_Click"
                                                                                ValidationGroup="GASExisting" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </fieldset>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:GridView ID="gvGASExisting" runat="server" AutoGenerateColumns="false" CssClass="SubFormWOBG"
                                                                Caption="<th colspan='15'><b>Detail of Structures</b></th>" DataKeyNames="IndustrialNewApplicationCode"
                                                                ShowHeaderWhenEmpty="true" OnRowDataBound="gvGASExisting_RowDataBound" OnRowCommand="gvGASExisting_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="SNo.">
                                                                        <ItemTemplate>
                                                                            <span>
                                                                                <%#Container.DataItemIndex + 1 %></span>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Serial Number" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="SerialNumber" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("SerialNumber")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Type of Structure Code" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="TypeOfAbstractionStructureCode" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("TypeOfAbstractionStructureCode")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Type of Structure Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="TypeOfAbstractionStructureName" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Year of Construction">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="YearOfConstruction" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("YearOfConstruction")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Depth (Meter)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="DepthExist" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("DepthExist")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Diameter (mm)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Diameter" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("Diameter")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Depth to Water Level (Meters below Ground Level)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="DepthBelowWaterLevel" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("DepthBelowWaterLevel")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Discharge (m3/Hour)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Discharge" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("Discharge")) %>'></asp:Label>
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
                                                                    <asp:TemplateField HeaderText="Mode of Lift Code" Visible="false">
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
                                                                            <asp:Label ID="WhetherPermissionRegisteredWithCGWA" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("WhetherPermissionRegisteredWithCGWA")) %>'></asp:Label>
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
                                                                    No Records exist in Groundwater Abstraction Structure- Existing.
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="accordion-item">
                                        <h2 class="accordion-header" id="headingTwo">
                                            <button type="button" class="accordion-button collapsed"
                                                data-bs-toggle="collapse"
                                                data-bs-target="#collapseTwo">
                                                2. Groundwater Abstraction Structure- Proposed</button>
                                        </h2>
                                        <div id="collapseTwo" class="accordion-collapse collapse"
                                            data-bs-parent="#myAccordion">
                                            <div class="card-body">
                                                <table>
                                                    <tr>
                                                        <td style="width: 35%">Number of Proposed Structures: <span class="Coumpulsory">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNumbeOfProposedGAS" runat="server" MaxLength="3" Width="200px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNumbeOfProposedGAS"
                                                                Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="GS1"></asp:RequiredFieldValidator><br />
                                                            <asp:RegularExpressionValidator ID="revtxtNumbeOfProposedGAS" runat="server" ForeColor="Red" Display="Dynamic"
                                                                ControlToValidate="txtNumbeOfProposedGAS" ValidationGroup="GS1"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <fieldset>
                                                                <legend><b>Details:</b></legend>
                                                                <table class="SubFormWOBG" width="100%" style="line-height: 20px">
                                                                    <tr>
                                                                        <td style="width: 35%">Type of Structure: <span class="Coumpulsory">*</span>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlTypeOfStructureP" runat="server" Width="200px">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator runat="server"
                                                                                ForeColor="Red"
                                                                                Display="Dynamic" ValidationGroup="GS"
                                                                                InitialValue=" " ControlToValidate="ddlTypeOfStructureP">Required</asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Year of Construction:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtYearOfConstructionP" runat="server" MaxLength="4" Width="200px"></asp:TextBox><br />
                                                                            <asp:RangeValidator id="RngValYearOfConstructionP"
                                                                                runat="server" ControlToValidate="txtYearOfConstructionP"
                                                                                Type="Integer" MaximumValue="2050" Display="Dynamic" 
                                                                                ForeColor="Red" ErrorMessage="Invalid Year"
                                                                                ValidationGroup="GS"></asp:RangeValidator>
                                                                            <asp:RegularExpressionValidator ID="revtxtYearOfConstructionP" runat="server" Display="Dynamic"
                                                                                ForeColor="Red" ControlToValidate="txtYearOfConstructionP"
                                                                                ValidationGroup="GS"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Depth (Meter):
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtDepthMeterP" runat="server" MaxLength="7" Width="200px"></asp:TextBox><br />
                                                                            <asp:RangeValidator runat="server" Type="Double" ControlToValidate="txtDepthMeterP"
                                                                                Display="Dynamic" ForeColor="Red" ValidationGroup="GS" ErrorMessage="Value Range 0.01 to 1000"
                                                                                MaximumValue="1000.00" MinimumValue="0.01"></asp:RangeValidator>
                                                                            <asp:RegularExpressionValidator ID="revtxtDepthMeterP" runat="server" Display="Dynamic"
                                                                                ForeColor="Red" ControlToValidate="txtDepthMeterP" ValidationGroup="GS"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Diameter (mm):
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtDiameterMMP" runat="server" MaxLength="6" Width="200px"></asp:TextBox><br />
                                                                            <asp:RegularExpressionValidator
                                                                                ID="revtxtDiameterMMP" runat="server" Display="Dynamic"
                                                                                ForeColor="Red" ControlToValidate="txtDiameterMMP" ValidationGroup="GS"></asp:RegularExpressionValidator>
                                                                            <asp:RangeValidator runat="server" Display="Dynamic" Type="Integer"
                                                                                ControlToValidate="txtDiameterMMP" ForeColor="Red" ValidationGroup="GS"
                                                                                ErrorMessage="Value can't be more than 999999" MaximumValue="999999" MinimumValue="0"></asp:RangeValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Depth to Water Level (Meters below Ground Level):
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtDepthWaterBelowGroundLevelP" runat="server" MaxLength="7"
                                                                                Width="200px"></asp:TextBox><br />
                                                                            <asp:RegularExpressionValidator ID="revtxtDepthWaterBelowGroundLevelP" runat="server"
                                                                                ForeColor="Red" Display="Dynamic" ControlToValidate="txtDepthWaterBelowGroundLevelP"
                                                                                ValidationGroup="GS"></asp:RegularExpressionValidator>
                                                                            <asp:RangeValidator runat="server" Display="Dynamic" Type="Double"
                                                                                ControlToValidate="txtDepthWaterBelowGroundLevelP" ForeColor="Red" ValidationGroup="GS"
                                                                                ErrorMessage="Value Range 0.01 to 1000" MaximumValue="1000.00" MinimumValue="0.01"></asp:RangeValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Discharge (m<sup>3</sup>/Hour):
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtDischargeP" runat="server" MaxLength="7" Width="200px"></asp:TextBox><br />
                                                                            <asp:RegularExpressionValidator ID="revtxtDischargeP" runat="server" Display="Dynamic"
                                                                                ForeColor="Red" ControlToValidate="txtDischargeP" ValidationGroup="GS"></asp:RegularExpressionValidator>
                                                                            <asp:RangeValidator runat="server" Display="Dynamic" Type="Double"
                                                                                ControlToValidate="txtDischargeP" ForeColor="Red" ValidationGroup="GS"
                                                                                ErrorMessage="Value Range 0.01 to 1000" MaximumValue="1000.00" MinimumValue="0.01"></asp:RangeValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Operational Hours / Day:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtOperationalHoursDayP" runat="server" MaxLength="2"
                                                                                Width="200px"></asp:TextBox><br />
                                                                            <asp:RegularExpressionValidator ID="revtxtOperationalHoursDayP" runat="server" Display="Dynamic"
                                                                                ForeColor="Red" ControlToValidate="txtOperationalHoursDayP" ValidationGroup="GS"></asp:RegularExpressionValidator>
                                                                            <asp:RangeValidator runat="server" Display="Dynamic" Type="Integer"
                                                                                ControlToValidate="txtOperationalHoursDayP" ForeColor="Red" ValidationGroup="GS"
                                                                                ErrorMessage="Value can't be more than 24" MaximumValue="24" MinimumValue="0"></asp:RangeValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Operational Days / Year:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtOperationalDaysYearP" runat="server" MaxLength="3"
                                                                                Width="200px"></asp:TextBox><br />
                                                                            <asp:RegularExpressionValidator ID="revtxtOperationalDaysYearP" runat="server" Display="Dynamic"
                                                                                ForeColor="Red" ControlToValidate="txtOperationalDaysYearP" ValidationGroup="GS"></asp:RegularExpressionValidator>
                                                                            <asp:RangeValidator runat="server" Display="Dynamic" Type="Integer"
                                                                                ControlToValidate="txtOperationalDaysYearP" ForeColor="Red" ValidationGroup="GS"
                                                                                ErrorMessage="Value can't be more than 365" MaximumValue="365" MinimumValue="0"></asp:RangeValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Mode of Lift:
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlModeOfLiftP" runat="server" Width="200px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Horse Power of Pump:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtHorsePowerPumpP" runat="server" MaxLength="6" Width="200px"></asp:TextBox><br />
                                                                            <asp:RegularExpressionValidator ID="revtxtHorsePowerPumpP" runat="server" ForeColor="Red"
                                                                                ControlToValidate="txtHorsePowerPumpP" Display="Dynamic" ValidationGroup="GS"></asp:RegularExpressionValidator>
                                                                            <asp:RangeValidator runat="server" Display="Dynamic" Type="Double"
                                                                                ControlToValidate="txtHorsePowerPumpP" ForeColor="Red" ValidationGroup="GS"
                                                                                ErrorMessage="Value can't be more than 999.99" MaximumValue="999.99" MinimumValue="0.01"></asp:RangeValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Whether 
                                    Fitted with Water Meter:
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlWhetherFittedWithWaterMeterP" runat="server" Width="200px">
                                                                                <asp:ListItem>Yes</asp:ListItem>
                                                                                <asp:ListItem>No</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Whether Permission / Registered with CGWA:
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlWhetherPermissionRegisteredWithCGWAP" runat="server" Width="200px"
                                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlWhetherPermissionRegisteredWithCGWAP_SelectedIndexChanged">
                                                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                                <asp:ListItem Value="No">No</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>If so Details thereof:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtWhetherPermissionRegisteredWithCGWADetP" runat="server" MaxLength="100"
                                                                                onkeyup="CountCharacter(this, this.form.ActionCommentRemCountP, 100);" onkeydown="CountCharacter(this, this.form.ActionCommentRemCountP, 100);"
                                                                                TextMode="MultiLine" Width="300px" Height="50px"></asp:TextBox><br />
                                                                            <input type="text" id="ActionCommentRemCountP" tabindex="-1" style="border-width: 0px; width: 100px; font-size: 10px; text-align: left; margin-left: 210px; background-color: transparent"
                                                                                name="ActionCommentRemCountP" size="2" maxlength="2" value="( 100 Character Left )"
                                                                                readonly="readonly" /><br />
                                                                            <asp:RegularExpressionValidator ID="revtxtWhetherPermissionRegisteredWithCGWADetP"
                                                                                runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtWhetherPermissionRegisteredWithCGWADetP"
                                                                                ValidationGroup="GS"></asp:RegularExpressionValidator>
                                                                            <asp:RegularExpressionValidator ID="revLengthtxtWhetherPermissionRegisteredWithCGWADetP"
                                                                                runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="GS"
                                                                                ControlToValidate="txtWhetherPermissionRegisteredWithCGWADetP"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center" colspan="2">
                                                                            <asp:Button ID="btnAddP" runat="server" Text="Add In List" OnClick="btnAddP_Click"
                                                                                ValidationGroup="GS" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </fieldset>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:GridView ID="gvGASProposed" runat="server" AutoGenerateColumns="false" CssClass="SubFormWOBG"
                                                                Caption="<th colspan='15'><b>Detail of Structures</b></th>" DataKeyNames="IndustrialNewApplicationCode"
                                                                ShowHeaderWhenEmpty="true" OnRowDataBound="gvGASProposed_RowDataBound" OnRowCommand="gvGASProposed_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="SNo.">
                                                                        <ItemTemplate>
                                                                            <span>
                                                                                <%#Container.DataItemIndex + 1 %></span>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Serial Number" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="SerialNumber" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("SerialNumber")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Type of Structure Code" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="TypeOfAbstractionStructureCode" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("TypeOfAbstractionStructureCode")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Type of Structure Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="TypeOfAbstractionStructureName" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Year of Construction">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="YearOfConstruction" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("YearOfConstruction")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Depth (Meter)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="DepthExist" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("DepthExist")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Diameter (mm)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Diameter" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Diameter")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Depth to Water Level (Meters below Ground Level)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="DepthBelowWaterLevel" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("DepthBelowWaterLevel")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Discharge (m3/Hour)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Discharge" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("Discharge")) %>'></asp:Label>
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
                                                                    <asp:TemplateField HeaderText="Mode of Lift Code" Visible="false">
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
                                                                            <asp:Label ID="WhetherPermissionRegisteredWithCGWA" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("WhetherPermissionRegisteredWithCGWA")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="If so Details Thereof">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="WhetherPermissionRegisteredWithCGWADetails" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("WhetherPermissionRegisteredWithCGWADetails")) %>'></asp:Label>
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
                                                                    No Records exist in Groundwater Abstraction Structure- Proposed.
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                            <asp:Label ID="lblIndustialApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">

                            <asp:Button ID="btnPrev" runat="server" Text="<< Prev" OnClientClick="return confirm('Please Save as Draft before moving to Previous Page ?');"
                                OnClick="btnPrev_Click" />
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" ValidationGroup="GS1"
                                OnClick="btnSaveAsDraft_Click" />
                            <asp:Button ID="txtNext" runat="server" Text="Next >>" ValidationGroup="GS1"
                                OnClick="txtNext_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

