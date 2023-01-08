<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" CodeFile="SelfInspectionA.aspx.cs" Inherits="ExternalUser_SelfInspection_SelfInspectionA" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .Initial {
            display: block;
            padding: 4px 10px 4px 10px;
            float: left;
            background-color: #CFE3FA;
            color: Black;
            font-weight: bold;
        }


        .Clicked {
            float: left;
            display: block;
            background-color: #094E7F;
            padding: 4px 18px 4px 18px;
            color: Black;
            font-weight: bold;
            color: White;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table align="center" class="SubFormWOBG" width="100%" style="line-height: 20px">
        <tr>
            <td colspan="6">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="6">
                            <center>
                                <div style="background-color: #094E7F; width: 100%; text-align: center">
                                    <asp:Label ID="lblHeading" runat="server" ForeColor="White" Font-Bold="True" Font-Size="Medium" Text=" Self Inspection"></asp:Label>
                                </div>
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 17%">
                            <asp:Button ID="Tab1" BorderStyle="None" runat="server" Text="(A)"
                                CssClass="Clicked" Width="100%" Enabled="false" />
                        </td>

                        <td style="width: 17%">
                            <asp:Button ID="Tab2" BorderStyle="None" runat="server" Text="(B)" CssClass="Initial"
                                Width="100%" Enabled="false" />
                        </td>
                        <td style="width: 17%">
                            <asp:Button ID="Button1" BorderStyle="None" runat="server" Text="(C)"
                                CssClass="Initial" Width="100%" Enabled="false" />
                        </td>
                        <td style="width: 17%">
                            <asp:Button ID="Button2" BorderStyle="None" runat="server" Text="(D)"
                                CssClass="Initial" Width="100%" Enabled="false" />
                        </td>

                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>1
            </td>
            <td>
                <asp:Label runat="server" Text="Name of Applicant / Project" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNameOfIndustry" runat="server">Label</asp:Label>
            </td>
            <td>2</td>
            <td>
                <asp:Label runat="server" Text="Application Code" Font-Bold="true"></asp:Label>

            </td>
            <td>
                <asp:Label ID="lblApplicationCode" runat="server">Label</asp:Label>
            </td>

        </tr>
        <tr>
            <td>3</td>
            <td>
                <asp:Label runat="server" Text="Application Number" Font-Bold="true"></asp:Label></td>
            <td>
                <asp:Label ID="lblApplicationNo" runat="server">Label</asp:Label>
            </td>
            <td>4</td>
            <td>
                <asp:Label runat="server" Text="Applied For" Font-Bold="true"></asp:Label></td>
            <td colspan="2">
                <asp:Label ID="lblAppliedFor" runat="server">Label</asp:Label>
            </td>

        </tr>
        <tr>
            <td>5</td>
            <td>
                <asp:Label runat="server" Text="Type Of Project" Font-Bold="true"></asp:Label></td>
            <td>
                <asp:Label ID="lblTypeOfProject" runat="server">Label</asp:Label>
            </td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>6</td>
            <td style="width: inherit">(i)
                <asp:Label runat="server" Text="NOC Details" Font-Bold="true"></asp:Label>

            </td>
            <td colspan="4" style="text-align: left">
                <table width="100%" class="SubFormWOBG" align="left">
                    <tr>
                        <td>(a)</td>
                        <td>NOC No</td>
                        <td colspan="2">
                            <asp:TextBox runat="server" ID="txtNOCNo" Enabled="false" Width="225px"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" Display="Dynamic"
                                ControlToValidate="txtNOCNo" ErrorMessage="Required." ForeColor="Red"
                                ValidationGroup="SelfCompliance"></asp:RequiredFieldValidator>
                            <%--<br />
                            <asp:RegularExpressionValidator ID="revtxtNocNumber" runat="server" ForeColor="Red"
                                ValidationGroup="SelfCompliance" Display="Dynamic" ControlToValidate="txtNOCNo"></asp:RegularExpressionValidator>
                        --%>
                        </td>
                    </tr>
                    <tr>
                        <td>(b)</td>
                        <td colspan="2">Validity</td>

                    </tr>
                    <tr>
                        <td></td>
                        <td>From:<br />
                            <asp:TextBox ID="txtNOCStartDate" MaxLength="10" runat="server" Enabled="false" Width="225px"></asp:TextBox>
                            <asp:ImageButton ID="ImgBtnNOCStartDate" runat="server" ImageUrl="~/Images/calendar.png"
                                CausesValidation="false" />
                            <asp:CalendarExtender runat="server" Enabled="True"
                                TargetControlID="txtNOCStartDate" PopupButtonID="ImgBtnNOCStartDate"
                                Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                            <asp:RequiredFieldValidator runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtNOCStartDate" ValidationGroup="SelfCompliance"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>

                        </td>
                        <td>To:<br />
                            <asp:TextBox ID="txtNOCEndDate" MaxLength="10" runat="server" Enabled="false" Width="225px"></asp:TextBox>
                            <asp:ImageButton ID="ImgBtnNOCEndDate" runat="server" ImageUrl="~/Images/calendar.png"
                                CausesValidation="false" />
                            <asp:CalendarExtender runat="server" Enabled="True"
                                TargetControlID="txtNOCEndDate" PopupButtonID="ImgBtnNOCEndDate"
                                Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                            <asp:RequiredFieldValidator runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtNOCEndDate" ValidationGroup="SelfCompliance"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>(c)</td>
                        <td colspan="3">Daily Groundwater quantum (m3/d): </td>

                    </tr>
                    <tr>
                        <td></td>
                        <td>(i) Abstraction:<br />
                            <asp:TextBox runat="server" ID="txtQtyAbstractionPerDay" Enabled="false" Width="225px"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtQtyAbstractionPerDay" ValidationGroup="SelfCompliance"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtGroundWaterRequirementExist" runat="server"
                                ForeColor="Red" Display="Dynamic" ControlToValidate="txtQtyAbstractionPerDay"
                                ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>


                        </td>
                        <td>(ii) Dewatering:<br />
                            <asp:TextBox runat="server" ID="txtQtyDewateringPerDay" Enabled="false" Width="225px"></asp:TextBox>
                            
                            <asp:RegularExpressionValidator ID="revtxtQtyDewateringPerDay" runat="server"
                                ForeColor="Red" Display="Dynamic" ControlToValidate="txtQtyDewateringPerDay"
                                ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>(d)</td>
                        <td colspan="3">Annual Groundwater quantum (m3/y): </td>

                    </tr>
                    <tr>
                        <td></td>
                        <td>(i) Abstraction:<br />
                            <asp:TextBox runat="server" ID="txtQtyAbstractionPerYear" Enabled="false" Width="225px"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ForeColor="Red"
                                Display="Dynamic" ControlToValidate="txtQtyAbstractionPerYear" ValidationGroup="SelfCompliance"
                                ErrorMessage="Required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtQtyAbstractionPerYear" runat="server"
                                ForeColor="Red" Display="Dynamic" ControlToValidate="txtQtyAbstractionPerYear"
                                ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>


                        </td>
                        <td>(ii) Dewatering:<br />
                            <asp:TextBox runat="server" ID="txtQtyDewateringPerYear" Enabled="false" Width="225px"></asp:TextBox>
                            
                            <asp:RegularExpressionValidator ID="revtxtQtyDewateringPerYear" runat="server"
                                ForeColor="Red" Display="Dynamic" ControlToValidate="txtQtyDewateringPerYear"
                                ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>


        
        <tr>
            <td colspan="6">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                <asp:Label ID="lblPageTitleFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                <asp:Label ID="lblApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                <asp:Label runat="server" ID="lblNOCCount" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6" style="text-align: center">
                <asp:Button ID="btnSaveAsDraft" ValidationGroup="SelfCompliance" runat="server" Text="Save As Draft"
                    OnClick="btnSaveAsDraft_Click" Style="height: 26px" />
                <asp:Button ID="btnNext" ValidationGroup="SelfCompliance" runat="server" Text="Next >>"
                    OnClick="btnNext_Click" Style="height: 26px" />
            </td>
        </tr>
    </table>
</asp:Content>
