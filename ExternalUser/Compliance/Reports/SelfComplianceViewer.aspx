<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master"
     MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="SelfComplianceViewer.aspx.cs" Inherits="ExternalUser_Compliance_Reports_SelfComplianceViewer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .Initial {
            display: block;
            padding: 4px 10px 4px 10px;
            float: left; /* background: url("../Images/InitialImage.png") no-repeat right top;*/ /*background:-o-linear-gradient(bottom, #6C7A89 5%, #8995A1 100%);	background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #6C7A89), color-stop(1, #8995A1) );
	        background:-moz-linear-gradient( center top, #6C7A89 5%, #8995A1 100% );
	        filter:progid:DXImageTransform.Microsoft.gradient(startColorstr="#6C7A89", endColorstr="#8995A1");	background: -o-linear-gradient(top,#6C7A89,8995A1);*/
            background-color: #CFE3FA;
            color: Black;
            font-weight: bold;
        }

            .Initial:hover {
                color: White; /* background: url("../Images/SelectedButton.png") no-repeat right top;*/
                background-color: #094E7F;
                cursor: hand;
            }

        .Clicked {
            float: left;
            display: block; /*background: url("../Images/SelectedButton.png") no-repeat right top;*/
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table class="SubFormWOBG" width="100%" style="line-height: 25px">
        <tr>
            <td colspan="7">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="7">
                            <center>
                                <div style="background-color: #094E7F; width: 100%; text-align: center">
                                    <asp:Label ID="lblHeading" runat="server" ForeColor="White" Font-Bold="True" Font-Size="Medium" Text=" Self Compliance"></asp:Label>
                                </div>
                            </center>
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
            <td colspan="2">
                <asp:Label ID="lblNameOfIndustry" runat="server">Label</asp:Label>
            </td>

            <td>2</td>
            <td>
                <asp:Label runat="server" Text="Application Code" Font-Bold="true"></asp:Label></td>
            <td>
                <asp:Label ID="lblApplicationCode" runat="server">Label</asp:Label>
            </td>
        </tr>
        <tr>
            <td>3</td>
            <td>
                <asp:Label runat="server" Text="Application Number" Font-Bold="true"></asp:Label></td>
            <td colspan="2">
                <asp:Label ID="lblApplicationNo" runat="server">Label</asp:Label>
            </td>

            <td>4</td>
            <td>
                <asp:Label runat="server" Text="Applied For" Font-Bold="true"></asp:Label></td>
            <td>
                <asp:Label ID="lblAppliedFor" runat="server">Label</asp:Label>
            </td>
        </tr>
        <tr>
            <td>5</td>
            <td>
                <asp:Label runat="server" Text="Type Of Project" Font-Bold="true"></asp:Label></td>
            <td colspan="5">
                <asp:Label ID="lblTypeOfProject" runat="server">Label</asp:Label>
            </td>
        </tr>
        <tr>
            <td>6</td>
            <td style="width: inherit">
                <asp:Label runat="server" Text="(i) NOC Details" Font-Bold="true"></asp:Label>

            </td>
            <td colspan="5" style="text-align: left">
                <table width="100%" class="SubFormWOBG" align="left">
                    <tr>
                        <td>(a)</td>
                        <td>NOC No</td>
                        <td colspan="2">
                            <asp:TextBox runat="server" ID="txtNOCNo" Enabled="false"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td>(b)</td>
                        <td colspan="2">Validity</td>

                    </tr>
                    <tr>
                        <td></td>
                        <td>From:
                             <asp:TextBox ID="txtNOCStartDate" MaxLength="10" runat="server" Enabled="false"></asp:TextBox>

                        </td>
                        <td>To:
                            <asp:TextBox ID="txtNOCEndDate" MaxLength="10" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>(c)</td>
                        <td colspan="3">Groundwater quantum (m3/d): </td>

                    </tr>
                    <tr>
                        <td></td>
                        <td>(i) Abstraction:<asp:TextBox runat="server" ID="txtQtyAbstractionPerDay" Enabled="false"></asp:TextBox>

                        </td>
                        <td>(ii) Dewatering:
                            <asp:TextBox runat="server" ID="txtQtyDewateringPerDay" Enabled="false"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td>(d)</td>
                        <td colspan="3">Groundwater quantum (m3/y): </td>

                    </tr>
                    <tr>
                        <td></td>
                        <td>(i) Abstraction:
                            
                           <asp:TextBox runat="server" ID="txtQtyAbstractionPerYear" Enabled="false"></asp:TextBox>


                        </td>
                        <td>(ii) Dewatering:
                             <asp:TextBox runat="server" ID="txtQtyDewateringPerYear" Enabled="false"></asp:TextBox>


                        </td>
                    </tr>
                </table>

            </td>
        </tr>
        <tr>
            <td></td>
            <td>(ii) Copy of NOC attached:</td>
            <td colspan="5">
                <table width="100%">

                    <tr>
                        <td>
                            <asp:GridView ID="gvNOC" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemTemplate>
                                            <span>
                                                <%#Container.DataItemIndex + 1 %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S.No." Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentSerialNumber" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                OnCommand="ViewFile">View</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <EmptyDataTemplate>
                                    No Records exist.
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>


        <tr>
            <td>7</td>
            <td colspan="3">
                <asp:Label runat="server" Text="Inspection details (Earlier)" Font-Bold="true"></asp:Label>
            </td>

        </tr>
        <tr>
            <td></td>
            <td>(a). Name of agency:</td>
            <td>
                <asp:DropDownList ID="ddlNameOfAgency" runat="server" Width="100px" Enabled="false">
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtOtherAgency" runat="server" TextMode="MultiLine" Rows="4" Enabled="false"
                    Columns="35"></asp:TextBox>

            </td>

            <td colspan="2">(b). Date of Inspection:</td>
            <td>
                <asp:TextBox ID="txtDateOfInsp1" MaxLength="10" runat="server" Enabled="false"></asp:TextBox>


            </td>

        </tr>
        <tr>
            <td></td>
            <td>(c). Copy of site inspection report
            </td>
            <td valign="top">
                <asp:DropDownList ID="ddlSiteInsp" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td colspan="4">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="gvSiteInspection" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemTemplate>
                                            <span>
                                                <%#Container.DataItemIndex + 1 %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S.No." Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentSerialNumber" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                OnCommand="ViewFile">View</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <EmptyDataTemplate>
                                    No Records exist.
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>


            </td>


        </tr>
        <tr>
            <td></td>
            <td colspan="6">
                <b>Self-Compliance of NOC Conditions:</b>
            </td>
        </tr>
        <tr>
            <td>(i)
            </td>
            <td colspan="2">Present withdrawal of Ground Water
            </td>
            <td valign="top" colspan="4">
                <asp:DropDownList ID="ddlPresentwithdrawal" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="6">
                <table align="left" class="SubFormWOBG" width="100%">
                    <tr>
                        <td></td>
                        <td colspan="3"><b>As per NOC</b></td>
                        <td colspan="3"><b>Self Compliance</b></td>
                    </tr>
                    <tr>
                        <td>(a). Abstraction</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtPresentwithdrawalInDay" runat="server" Enabled="false"></asp:TextBox>
                            &nbsp;(m<sup>3</sup>/day)
                            <br />
                            <asp:TextBox ID="txtPresentwithdrawalInYear" runat="server" Enabled="false"></asp:TextBox>
                            &nbsp;(m<sup>3</sup>/year)
               
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtselfPresentwithdrawalInDay" runat="server" Enabled="false"></asp:TextBox>
                            &nbsp;(m<sup>3</sup>/day)
                            <br />
                            <asp:TextBox ID="txtselfPresentwithdrawalInYear" runat="server" Enabled="false"></asp:TextBox>
                            &nbsp;(m<sup>3</sup>/year)

                        </td>
                    </tr>
                    <tr>

                        <td>(b). Dewatering</td>
                        <td colspan="2">


                            <asp:TextBox ID="txtDewPresentwithdrawalInDay" runat="server" Enabled="false"></asp:TextBox>
                            &nbsp;(m<sup>3</sup>/day)<br />
                            <asp:TextBox ID="txtDewPresentwithdrawalInYear" runat="server" Enabled="false"></asp:TextBox>
                            &nbsp;(m<sup>3</sup>/year)
                
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtselfDewPresentwithdrawalInDay" runat="server" Enabled="false"></asp:TextBox>
                            &nbsp;(m<sup>3</sup>/day)
                            <br />
                            <asp:TextBox ID="txtselfDewPresentwithdrawalInYear" runat="server" Enabled="false"></asp:TextBox>
                            &nbsp;(m<sup>3</sup>/year)
                
                        </td>
                    </tr>
                </table>

            </td>

        </tr>

        <tr>
            <td></td>
            <td>(c). Any variation in withdrawal (to be reported):</td>
            <td>
                <asp:DropDownList ID="ddlAnyVariation" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="More than permitted quantum" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Less than permitted quantum" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:TextBox ID="txtQtyVariDay" runat="server" Enabled="false"></asp:TextBox>

                &nbsp;(m<sup>3</sup>/day) 
                <br />
                <asp:TextBox ID="txtQtyVariYear" runat="server" Enabled="false"></asp:TextBox>
                &nbsp;(m<sup>3</sup>/year)
                
            </td>

            <td colspan="2">(d). Abstraction Data submitted for all the TUBEWELL&nbsp; as per NOC:</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlAbstractionTW" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td>(ii)</td>
            <td colspan="6">
                <asp:Label runat="server" Text="Number of abstraction/dewatering structures" Font-Bold="true"></asp:Label>
            </td>

        </tr>
        <tr>
            <td></td>
            <td colspan="6">
                <table align="left" class="SubFormWOBG" width="100%">
                    <tr>

                        <td></td>
                        <td>
                            <asp:Label runat="server" Text="(As per NOC)" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="(Self Compliance)" Font-Bold="true"></asp:Label>

                        </td>
                    </tr>
                    <tr>

                        <td>Existing</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtAbstraStructExistingAsperNOC" Enabled="false"></asp:TextBox>

                        </td>
                        <td>Existing
                <asp:TextBox runat="server" ID="txtAbstraStructExisting" Enabled="false"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>

                        <td>Proposed</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtAbstraStructProposedAsperNOC" Enabled="false"></asp:TextBox>

                        </td>
                        <td>Proposed
                <asp:TextBox runat="server" ID="txtAbstraStructProposed" Enabled="false"></asp:TextBox>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>Number of functional abstraction structures</td>
            <td>
                <asp:TextBox runat="server" ID="txtNoOfFunt" Enabled="false"></asp:TextBox>

            </td>

            <td>Geotagged photograogh  of withdrawal structures</td>
            <td>
                <asp:DropDownList ID="ddlgeophotostruct" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>

            </td>
            <td colspan="2">
                <table width="100%">

                    <tr>
                        <td>
                            <asp:GridView ID="gvgeophotostruct" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                ShowHeaderWhenEmpty="true">

                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemTemplate>
                                            <span>
                                                <%#Container.DataItemIndex + 1 %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S.No." Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentSerialNumber" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--    <asp:TemplateField HeaderText="Attachment Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                OnCommand="ViewFile">View</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  <%--  <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                                <EmptyDataTemplate>
                                    No Records exist.
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
        <tr>
            <td>(iii)</td>
            <td colspan="6">
                <asp:Label runat="server" Text="Water meter details: (no content )" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2">All the abstraction structure fitted with water meter</td>
            <td>
                <asp:DropDownList ID="AbstStructWM" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>

            </td>

            <td>Type of meter</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlMeterType" runat="server" Width="100px" Enabled="false">
                </asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td></td>
            <td>Telemetry Installed</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlTeleInstalled" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <b>Telemetry Detail:</b>
                <br />
                <asp:GridView ID="gvTelemetry" runat="server" CssClass="SubFormWOBG" AllowPaging="true"
                    align="center" AutoGenerateColumns="False" AllowSorting="true" DataKeyNames="ApplicationCode,ApplicationSN"
                    Width="827px" OnSorting="gvTelemetry_Sorting" OnPageIndexChanging="gvTelemetry_PageIndexChanging"
                    PageSize="50">



                    <Columns>

                        <asp:TemplateField HeaderText="SN">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Telemetry Name">

                            <ItemTemplate>
                                <asp:Label ID="lblTelemetryName" runat="server" MaxLength="200" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TelemetryName")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Telemetry Url">

                            <ItemTemplate>
                                <asp:Label ID="lblTelemetryUrl" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TelemetryUrl")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Telemetry User Name">
                            <ItemTemplate>
                                <asp:Label ID="lblTelUserName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TelUserName"))%>'></asp:Label>
                            </ItemTemplate>


                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Telemetry Password">
                            <ItemTemplate>
                                <asp:Label ID="lblTelPassword" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("TelPassword"))%>'></asp:Label>
                            </ItemTemplate>


                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Records exist.
                    </EmptyDataTemplate>
                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                        PageButtonCount="4" />
                </asp:GridView>
                <asp:Label ID="lblSortField" runat="server" Visible="False"></asp:Label>


            </td>

            <td>Number of functional meter</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtFunctMeter" Enabled="false"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td></td>
            <td>Annual calibration of water meter by Govt agencies </td>
            <td>
                <asp:DropDownList ID="ddlAnnualCali" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>

            </td>
            <td colspan="5">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblAnnual" Text="Attatch Certificate" Font-Bold="true"></asp:Label>


                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvAnnualCalibration" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                                Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                                ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemTemplate>
                                            <span>
                                                <%#Container.DataItemIndex + 1 %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S.No." Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentSerialNumber" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                                OnCommand="ViewFile">View</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                                <EmptyDataTemplate>
                                    No Records exist.
                                </EmptyDataTemplate>
                            </asp:GridView>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>Geotagged Photograph of well fitted with water meter attached</td>
            <td>
                <asp:DropDownList ID="ddlGeoPhotoFittedWM" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>

            </td>
            <td colspan="4">
                <asp:GridView ID="gvGeoPhotowellfitted" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                    Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                    ShowHeaderWhenEmpty="true">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.No.">
                            <ItemTemplate>
                                <span>
                                    <%#Container.DataItemIndex + 1 %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="S.No." Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachmentSerialNumber" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="File Name">
                            <ItemTemplate>
                                <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View File">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                    OnCommand="ViewFile">View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        No Records exist.
                    </EmptyDataTemplate>
                </asp:GridView>


            </td>
        </tr>
        <tr>
            <td>(iv)</td>
            <td colspan="6">
                <asp:Label runat="server" Text="Ground Water Quality" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2">Water samples have been analyzed in Govt. approved lab </td>
            <td>
                <asp:DropDownList ID="ddlWaterSampple" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>

            </td>

            <td>Report submitted within stipulated time frame</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlWQRSubmitted" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td></td>
            <td>Ground water Quality report attached</td>
            <td>
                <asp:DropDownList ID="ddlGWQReport" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>

            </td>
            <td>
                <asp:GridView ID="gvWQRSubmitted" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                    Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                    ShowHeaderWhenEmpty="true">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.No.">
                            <ItemTemplate>
                                <span>
                                    <%#Container.DataItemIndex + 1 %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="S.No." Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachmentSerialNumber" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--    <asp:TemplateField HeaderText="Attachment Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="File Name">
                            <ItemTemplate>
                                <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View File">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                    OnCommand="ViewFile">View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        No Records exist.
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:Label ID="lblWQRSubmitted" runat="server"></asp:Label>

            </td>

            <td>Mine seepage quality report attached (in case of mining projects): </td>
            <td>
                <asp:DropDownList ID="ddlMineSeepage" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>

            </td>
            <td>
                <asp:GridView ID="gvMineseepage" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                    Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                    ShowHeaderWhenEmpty="true">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.No.">
                            <ItemTemplate>
                                <span>
                                    <%#Container.DataItemIndex + 1 %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="S.No." Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachmentSerialNumber" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--    <asp:TemplateField HeaderText="Attachment Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="File Name">
                            <ItemTemplate>
                                <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View File">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                    OnCommand="ViewFile">View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        No Records exist.
                    </EmptyDataTemplate>
                </asp:GridView>

            </td>
        </tr>

        <tr>
            <td>(v)</td>
            <td colspan="3">
                <asp:Label runat="server" Text="Details of Artificial recharge measures /Rain water harvesting implemented ( For NOC issued before 24/09/2020)" Font-Bold="true"></asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlRainwaterharvesting" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>Type of structures</td>
            <td colspan="2">
                <asp:CheckBoxList runat="server" ID="chklistTypeOfARStruct" RepeatDirection="Vertical" Enabled="false">
                </asp:CheckBoxList>

            </td>

            <td>No of structures</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtNoOfStruct" Enabled="false"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td></td>
            <td>Within premises/outside premises</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlWithinOutSidePremises" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>

            </td>

            <td>Quantum of recharge measures implemented by the applicant(cum/annum): </td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtQuantOfRecharge" Enabled="false"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td></td>
            <td>Geotagged photograph  of recharge structures</td>
            <td>
                <asp:DropDownList ID="ddlGeoPhotoRechargeStruc" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>

            </td>
            <td colspan="4">
                <asp:GridView ID="gvRainwaterharvesting" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                    Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                    ShowHeaderWhenEmpty="true">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.No.">
                            <ItemTemplate>
                                <span>
                                    <%#Container.DataItemIndex + 1 %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="S.No." Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachmentSerialNumber" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="File Name">
                            <ItemTemplate>
                                <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View File">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                    OnCommand="ViewFile">View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        No Records exist.
                    </EmptyDataTemplate>
                </asp:GridView>

            </td>
        </tr>
        <tr>
            <td>(vi)</td>
            <td colspan="3">
                <asp:Label runat="server" Text="Groundwater monitoring details:" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="6">
                <table align="left" class="SubFormWOBG" width="100%">
                    <tr>
                        <td></td>
                        <td><b>As Per NOC</b></td>
                        <td><b>Self Compliance</b></td>
                    </tr>
                    <tr>
                        <td>No. of piezometer (observation well) installed:</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtNOPiezo" Enabled="false"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revtxtNOPiezo" runat="server" ForeColor="Red"
                                ControlToValidate="txtNOPiezo" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>


                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtselfNOPiezo" Enabled="false"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revtxtselfNOPiezo" runat="server" ForeColor="Red"
                                ControlToValidate="txtselfNOPiezo" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>No. of piezometer with DIGITAL WATER LEVEL RECORDER:</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtNOPiezoDWLR" Enabled="false"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revtxtNOPiezoDWLR" runat="server" ForeColor="Red"
                                ControlToValidate="txtNOPiezoDWLR" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>


                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtselfNOPiezoDWLR" Enabled="false"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revtxtselfNOPiezoDWLR" runat="server" ForeColor="Red"
                                ControlToValidate="txtselfNOPiezoDWLR" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
                        </td>
                    </tr>

                    <tr>
                        <td>Piezometer with DIGITAL WATER LEVEL RECORDER & Telemetry</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtpieDWLRTelem" Enabled="false"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revtxtpieDWLRTelem" runat="server" ForeColor="Red"
                                ControlToValidate="txtpieDWLRTelem" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtselfpieDWLRTelem" Enabled="false"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revtxtselfpieDWLRTelem" runat="server" ForeColor="Red"
                                ControlToValidate="txtselfpieDWLRTelem" Display="Dynamic" ValidationGroup="SelfCompliance"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <%-- <tr>
            <td></td>
            <td>No. of piezometer (observation well) installed:</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtNOPiezo" Enabled="false"></asp:TextBox></td>

            <td>No. of piezometer with DIGITAL WATER LEVEL RECORDER:</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtNOPiezoDWLR" Enabled="false"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td>No. of piezometer with Telemetry:</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtNOPiezoTele" Enabled="false"></asp:TextBox></td>

            <td>No. of observation well/key well in core and buffer zone (in case of Mining project):</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtNoOversWell" Enabled="false"></asp:TextBox></td>
        </tr>--%>
        <tr>
            <td></td>
            <td>Number of functional Piezometer/Observational well:</td>
            <td colspan="5">
                <asp:TextBox runat="server" ID="NoFunctionalPiewell" Enabled="false"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td>Geotagged photograph of Piezometers/observation well/key well fitted with DIGITAL WATER LEVEL RECORDER/Telemetry</td>
            <td>
                <asp:DropDownList ID="ddlgeophotopiewell" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList></td>
            <td>
                <asp:GridView ID="gvGroundwaterMonitoring" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                    Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                    ShowHeaderWhenEmpty="true">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.No.">
                            <ItemTemplate>
                                <span>
                                    <%#Container.DataItemIndex + 1 %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="S.No." Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachmentSerialNumber" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="File Name">
                            <ItemTemplate>
                                <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View File">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                    OnCommand="ViewFile">View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        No Records exist.
                    </EmptyDataTemplate>
                </asp:GridView>

            </td>

            <td>Monitoring data submitted as per NOC</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlMoniDataSubmitted" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td></td>
            <td>Piezometer with DIGITAL WATER LEVEL RECORDER & Telemetry</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlpieDWLRTelem" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList></td>


            <td>Ground Water Monitoring data</td>
            <td>
                <asp:DropDownList ID="ddlGroundWaterMonitoringData" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList></td>


            <td>
                <asp:GridView ID="gvGroundWaterMonitoringData" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                    Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                    ShowHeaderWhenEmpty="true">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.No.">
                            <ItemTemplate>
                                <span>
                                    <%#Container.DataItemIndex + 1 %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="S.No." Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachmentSerialNumber" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--    <asp:TemplateField HeaderText="Attachment Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="File Name">
                            <ItemTemplate>
                                <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View File">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                    OnCommand="ViewFile">View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        No Records exist.
                    </EmptyDataTemplate>
                </asp:GridView>


            </td>


        </tr>
        <tr>
            <td>(vii)</td>
            <td colspan="6">
                <asp:Label runat="server" Text="Details of treated wastewater (Recycle/Reuse):" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>SEWAGE TREATMENT PLANT/EFFLUENT TREATMENT PLANT installed:</td>
            <td colspan="2">
                <asp:DropDownList runat="server" ID="ddlSTPETP" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList></td>

            <td>No. of SEWAGE TREATMENT PLANT/EFFLUENT TREATMENT PLANT installed:</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtNoSTPETP" Enabled="false"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td>Capacity of SEWAGE TREATMENT PLANT/EFFLUENT TREATMENT PLANT:</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtCapSTPETP" Enabled="false"></asp:TextBox></td>

            <td>Quantum of treated waste water generated (cum/y):</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtQuantumTWW" Enabled="false"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td>Geotagged photograph of EFFLUENT TREATMENT PLANT/SEWAGE TREATMENT PLANT:</td>
            <td>
                <asp:DropDownList ID="ddlgeophotoSTPETP" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList></td>
            <td colspan="4">
                <asp:GridView ID="gvSTPETP" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                    Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                    ShowHeaderWhenEmpty="true">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.No.">
                            <ItemTemplate>
                                <span>
                                    <%#Container.DataItemIndex + 1 %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="S.No." Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachmentSerialNumber" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="File Name">
                            <ItemTemplate>
                                <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View File">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                    OnCommand="ViewFile">View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        No Records exist.
                    </EmptyDataTemplate>
                </asp:GridView>

            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="6">
                <asp:Label runat="server" Text="Quantum of treated water used (cum/y):" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>Industrial process:</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtIndProcess" Enabled="false"></asp:TextBox></td>

            <td>Greenbelt:</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtGreenbelt" Enabled="false"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td>Other uses:</td>
            <td colspan="5">
                <asp:TextBox runat="server" ID="txtOtherUse" Enabled="false"></asp:TextBox></td>
        </tr>
        <tr>
            <td>(viii)</td>
            <td colspan="2">
                <asp:Label runat="server" Text="Submission of Self Compliance report online within stipulated time frame:" Font-Bold="true"></asp:Label>
            </td>
            <td colspan="4">
                <asp:DropDownList ID="ddlComplianceReportWSTF" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList></td>

        </tr>
        <tr>
            <td>(ix)</td>
            <td colspan="2">
                <asp:Label runat="server" Text="Details of Water Audit Inspection (if applicable):" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:DropDownList Enabled="false"
                    runat="server" ID="IFddlWaterAuditInspection">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td></td>
            <td>Water Audit inspection</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlWateAuditinspection" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>

            </td>
            <td>Water Audit inspection carried out as per NOC
            </td>
            <td colspan="2">
                <asp:DropDownList ID="ddlWateAuditinspectionNOC" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
        </tr>
        <tr>
            <td></td>
            <td>Name of agency by which water audit carried out</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtAuditAgency" Width="699px" Enabled="false"></asp:TextBox>

            </td>

            <td>Date of inspection</td>
            <td colspan="2">
                <asp:TextBox ID="txtDateOfInsp" MaxLength="10" runat="server" Enabled="false"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td></td>
            <td>Water audit report attached
            </td>
            <td>
                <asp:DropDownList ID="ddlWaterauditreportattached" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>

            </td>
            <td colspan="4">
                <asp:GridView ID="gvWaterAuditInspection" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                    Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                    ShowHeaderWhenEmpty="true">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.No.">
                            <ItemTemplate>
                                <span>
                                    <%#Container.DataItemIndex + 1 %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="S.No." Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachmentSerialNumber" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="File Name">
                            <ItemTemplate>
                                <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View File">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                    OnCommand="ViewFile">View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        No Records exist.
                    </EmptyDataTemplate>
                </asp:GridView>

            </td>

        </tr>
        <tr>
            <td>(x)</td>
            <td colspan="2">
                <asp:Label runat="server" Text="Impact assessment report/Comprehensive Hydro geological Report /Modeling report (if applicable): " Font-Bold="true"></asp:Label>
            </td>
            <td colspan="4">
                <asp:DropDownList runat="server" ID="ddlImpactassessmentreport" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td></td>
            <td>Requirement
            </td>
            <td colspan="2">
                <asp:DropDownList ID="ddlRequirement" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>


            </td>


            <td>Submitted as per NOC requirement
            </td>
            <td colspan="2">
                <asp:DropDownList ID="ddlSubmittedNOC" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>

            </td>

        </tr>
        <tr>
            <td></td>
            <td>Copy of IMPACT ASSESSMENT REPORT/modeling report/Hydrogeological report attached: 
            </td>
            <td>
                <asp:DropDownList ID="ddlCopyIAR" runat="server" Width="100px" Enabled="false">
                    <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>

            </td>
            <td colspan="4">
                <asp:GridView ID="gvIAR" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                    Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                    ShowHeaderWhenEmpty="true">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.No.">
                            <ItemTemplate>
                                <span>
                                    <%#Container.DataItemIndex + 1 %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="S.No." Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachmentSerialNumber" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="File Name">
                            <ItemTemplate>
                                <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View File">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                    OnCommand="ViewFile">View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        No Records exist.
                    </EmptyDataTemplate>
                </asp:GridView>

            </td>

        </tr>
        <tr>
            <td>(xi)</td>
            <td colspan="2">
                <asp:Label runat="server" Text="Any Violation of NOC conditions to be reported (If any):" Font-Bold="true"></asp:Label>
            </td>
            <td valign="top" colspan="4">
                <table>
                    <tr>
                        <td valign="top">
                            <asp:DropDownList ID="ddlAnyViolation" runat="server" Width="100px" Enabled="false">
                                <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:DropDownList>

                        </td>
                        <td valign="top">
                            <asp:TextBox ID="txtAnyViolation" runat="server" TextMode="MultiLine" Rows="4" Enabled="false"
                                Columns="35"></asp:TextBox>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>(xii)</td>
            <td colspan="2">
                <asp:Label runat="server" Text="Any other compliances as per NOC condition (If any):" Font-Bold="true"></asp:Label>
            </td>
            <td valign="top" colspan="4">
                <table>
                    <tr>
                        <td valign="top">
                            <asp:DropDownList ID="ddlAnyothercompliance" runat="server" Width="100px"
                                Enabled="false">
                                <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:DropDownList>

                        </td>
                        <td valign="top">
                            <asp:TextBox ID="txtAnyothercompliance" runat="server" TextMode="MultiLine" Rows="4" Enabled="false"
                                Columns="35"></asp:TextBox>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>(xiii)
            </td>
            <td>Extra Attachment
            </td>


            <td>
                <asp:GridView ID="gvExtra" runat="server" AutoGenerateColumns="False" CssClass="SubFormWOBG"
                    Width="100%" DataKeyNames="ApplicationCode,AttachmentCode,AttachmentCodeSerialNumber"
                    ShowHeaderWhenEmpty="true">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.No.">
                            <ItemTemplate>
                                <span>
                                    <%#Container.DataItemIndex + 1 %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="S.No." Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachmentSerialNumber" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("AttachmentCodeSerialNumber"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--    <asp:TemplateField HeaderText="Attachment Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachmentName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentName")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="File Name">
                            <ItemTemplate>
                                <asp:Label ID="lblFileName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("AttachmentPath")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View File">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnViewFile" runat="server" CommandName="ViewFile" CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("ApplicationCode") + ","+ Eval("AttachmentCode") + "," + Eval("AttachmentCodeSerialNumber"))%>'
                                    OnCommand="ViewFile">View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        No Records exist.
                    </EmptyDataTemplate>
                </asp:GridView>

            </td>



            <td>(xiiiv)
            </td>
            <td>Remarks
            </td>
            <td valign="top" colspan="2">
                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="4" Columns="35" Enabled="false"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td colspan="7">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                <asp:Label ID="lblModeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                <asp:Label ID="lblApplicationCodeFrom" Visible="false" runat="server" Enabled="False"></asp:Label>
                <asp:Label ID="lblApplicationRenewCode" Visible="false" runat="server" Enabled="False"></asp:Label>


                <asp:Label ID="lblFinalMsg" runat="server"></asp:Label>
            </td>
        </tr>

    </table>



</asp:Content>
