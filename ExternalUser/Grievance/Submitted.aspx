<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" CodeFile="Submitted.aspx.cs" Inherits="ExternalUser_Grievance_Submitted" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../../Scripts/jquery-1.8.3.min.js"></script> 
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '100%'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../../../Images/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../../../Images/plus.png");
            $(this).closest("tr").next().remove();
        });
    </script>

    <style type="text/css">
        .auto-style1 {
            width: 399px;
        }
    </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblGrievanceCode" runat="server" Enabled="False" Visible="False"></asp:Label>
    <asp:HiddenField ID="hidCSRFs" runat="server" Value="" />

    <table class="SubFormWOBG" width="100%">
        <tr>
            <td style="font-size: 17px; font-weight: bold" colspan="2">

                <div style="text-align: right; height: 50px; margin-top: 10px;">
                    <asp:LinkButton runat="server" Font-Bold="true" PostBackUrl="~/ExternalUser/Grievance/SubmitGrievance.aspx">Apply Fresh Grievance</asp:LinkButton>
                </div>
            </td>
        </tr>

        <tr>
            <td colspan="2">
                <div class="div_IndAppHeading" style="padding-left: 10px">
                    Submitted Grievance
                </div>
            </td>
        </tr>
    </table>
    <div>
        <asp:GridView ID="gvGrievance" runat="server" AutoGenerateColumns="false" CssClass="Grid" OnRowDataBound="OnRowDataBound"
            DataKeyNames="GrievanceCode" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="SN" ItemStyle-Width="5px">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-Width="100px" DataField="GrievanceNumber" HeaderText="Grievance Number">
                    <ItemStyle Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="GrievanceTypeDesc" HeaderText="Grievance Type" ItemStyle-Width="100" />
                <asp:BoundField DataField="Quantum" HeaderText="Quantum" ItemStyle-Width="100" />
                <asp:BoundField DataField="ToUserName" HeaderText="To User Name" ItemStyle-Width="100" />
                <asp:BoundField DataField="Remark" HeaderText="QUERY/GRIEVANCE" ItemStyle-Width="100" />

                 <asp:BoundField DataField="OfficerRemark" HeaderText="Officer Remark" ItemStyle-Width="100" />
                 


                <asp:TemplateField ItemStyle-Width="50px" HeaderText="File Closed">
                <ItemTemplate>
                    <asp:Label ID="lblFileClosed" runat="server" Text='<%# (Convert.ToString(Eval("FileClosed"))) %>'>
                    </asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:GridView ID="gvShowReGrievance" runat="server" AutoGenerateColumns="false"
                            CssClass="ChildGrid"
                            Width="100%" ShowFooter="true">
                            <Columns>
                                <asp:TemplateField HeaderText="SN" ItemStyle-Width="5px">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Remark" HeaderText="QUERY/GRIEVANCE" ItemStyle-Width="100" />
                                <asp:BoundField DataField="ToUserName" HeaderText="To User Name" ItemStyle-Width="100" />
                                 <asp:BoundField DataField="FileClosed" HeaderText="File Closed" ItemStyle-Width="100" /> 

                                  <asp:BoundField DataField="OfficerQuery" HeaderText="Officer Remark" ItemStyle-Width="100" />


                                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Appeal">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAppealDepth" runat="server" Text='<%# (Convert.ToInt32(Eval("Depth"))) +"st Appeal" %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <asp:Button ID="lbtnEdit" Text="Add Appeal" OnCommand="lbtnEdit_Click2" runat="server"
                            CommandArgument='<%#System.Web.HttpUtility.HtmlEncode(Eval("GrievanceCode")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>                 
            </Columns>
            <EmptyDataTemplate>
                There Is No Appeal Record
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>

