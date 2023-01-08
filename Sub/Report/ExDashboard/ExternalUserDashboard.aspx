<%@ Page Title="" Language="C#" MasterPageFile="~/Sub/SubReportMaster.master" AutoEventWireup="true" CodeFile="ExternalUserDashboard.aspx.cs"
    Inherits="Sub_Report_ExDashboard_ExternalUserDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../../../css/Chart/bootstrap.min.css" rel="stylesheet" />
    <link href="../../../css/Chart/jquery-ui.css" rel="stylesheet" />
    <link href="../../../css/Chart/keen-dashboards.css" rel="stylesheet" />

    <script src="../../../Scripts/HighChart/jquery.min.js" type="text/javascript"></script>
    <script src="../../../Scripts/HighChart/jquery-1.11.1.min.js" type="text/javascript"></script>

    <script src="../../../Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../../Scripts/holder.js" type="text/javascript"></script>


    <script src="../../../Scripts/Calendar/jquery-min-Calendar.js" type="text/javascript"></script>
    <script src="../../../Scripts/Calendar/jquery-ui.min-Calendar.js" type="text/javascript"></script>
    <link href="../../../Styles/Calendar/jquery-ui-Calendar.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(function () {
            $("[id*=txtFromDate]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd/mm/yy',
                minDate: new Date('01/01/2015'),
                maxDate: new Date(),
                changeMonth: true,
                changeYear: true,
                showOn: 'both',
                buttonImage: '../../../Images/calendar.png'

            });
            $("[id*=txtToDate]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd/mm/yy',
                minDate: new Date('01/01/2015'),
                maxDate: new Date(),
                changeMonth: true,
                changeYear: true,
                showOn: 'both',
                buttonImage: '../../../Images/calendar.png'
            });
        });
    </script>

    <script type="text/javascript">
        Holder.add_theme("white", { background: "#fff", foreground: "#a7a7a7", size: 10 });
    </script>


    <script src="../../../Scripts/HighChart/highcharts.js" type="text/javascript"></script>
    <script src="../../../Scripts/HighChart/highcharts-more.js" type="text/javascript"></script>
    <script src="../../../Scripts/HighChart/highcharts-3d.js" type="text/javascript"></script>
    <script src="../../../Scripts/HighChart/exporting.js" type="text/javascript"></script>
    <script src="../../../Scripts/HighChart/drilldown.js" type="text/javascript"></script>
    <script src="../../../Scripts/HighChart/data.js" type="text/javascript"></script>
    <script src="../../../Scripts/HighChart/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../../../css/Tab.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/JqueryCustomTableSort.js" type="text/javascript"></script>



    <style type="text/css">
        .ui-dialog-buttonpane.ui-widget-content.ui-helper-clearfix {
            display: none !important;
        }

        text {
            text-decoration: none !important;
        }

        .ui-dialog.ui-widget.ui-widget-content.ui-corner-all.ui-front.ui-dialog-buttons.ui-draggable.ui-resizable {
            width: 1000px !important;
            margin: -444px 0px 0px 0px;
            position: fixed !important;
        }

        tr:nth-child(even) {
            background: rgb(222,237,250);
        }

        tr:nth-child(odd) {
            background: rgb(239,246,253);
        }


        .ui-dialog .ui-dialog-titlebar {
            background-color: #094E7F;
        }

        .ui-tabs .ui-tabs-nav li {
            /*  background-color: greenyellow; */
        }

        #tabs ul li a {
            outline: none;
        }

        th {
            background-color: rgb(190,218,246);
            text-align: center;
        }

        #popupdiv {
            background-color: rgb(239,246,253);
        }

        /* .ui-widget-content a { color: blue;} */

        .popupgrid tr th {
            background-color: #094E7F;
            color: White;
            font-size: 13px;
            height: 27px;
        }

        .popupgrid tr td {
            padding-left: 10px;
            text-align: left;
        }

            .popupgrid tr td a {
                color: #083d8d;
            }

                .popupgrid tr td a:hover {
                    text-decoration: none;
                }

        .ui-button-icon-only .ui-icon {
            background-color: white;
        }

        .popupgrid .tdAlign {
            text-align: center;
        }

        .drop {
            border-left: none;
            border-right: none;
            border-radius: 5px;
            height: 30px;
            /*background-color: peru;*/
            width: 150px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#tabs").tabs();
        });
      
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            //$("#txtFromDate").datepicker({ dateFormat: 'dd-mm-yy' });
            //$("#txtToDate").datepicker({ dateFormat: 'dd-mm-yy' });


            Highcharts.setOptions(
                    {
                        colors: ['#094e7f ', '#D92525', '#63A01F', '#FCB711', '#FD5E1E', '#6460AA', '#677300', '#CC004C', '#005391', '#8A1525', '#3C4C00', '#0E3D59']
                    });
            
            
            ChartRegionWiseNOCIssuedEx();
            ChartRegionWiseNOCIssuedExRen();

        });


       




        //Amardeep
        function ChartRegionWiseNOCIssuedEx() {
            Highcharts.chart('ChartRegionWiseNOCIssuedEx', {
                chart: {
                    type: 'column',
                    events: {
                        drilldown: function (e) {
                            if (e.point.y > 0) {
                                generateDrlStateWiseAndMoreThanOrLessThan12M(e.point.FullName, e.point.drilldown, e.point.series.RegionShortName);
                            }
                        },
                        drillup: function (e) {
                        }
                    } //events
                }, //chart
                title: {
                    align: 'center',
                    text: 'No of NOCs Issued by CGWA',
                    style: '{ "color": "#214900" }'


                } //title
                                   , xAxis: {
                                       type: 'category',
                                       title: {

                                           text: 'Region Name'
                                       },
                                       labels: {
                                           rotation: '-45'
                                       }

                                   } //xAxis
                                   ,
                yAxis: {
                    min: 0,
                    title: {
                        text: 'No of NOC issued by CGWA'
                    },
                    stackLabels: {
                        enabled: true,
                        style: {
                            fontWeight: 'bold', color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                        }
                    }
                }//yAxis
                                    , legend: { align: 'right', x: -30, verticalAlign: 'bottom', y: 25, floating: true, backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white', borderColor: '#CCC', borderWidth: 1, shadow: false
                                    },
                tooltip: {
                    headerFormat: '', pointFormat: '<b>{point.FullName}</b><br/>{series.name}: {point.y}<br/>Total: {point.stackTotal}'
                },
                plotOptions: {
                    column: {
                        borderRadius: 2,
                        stacking: 'normal',
                        dataLabels: {
                            enabled: true, color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                        }
                    }
                }
                , series: [{ name: 'Industrial', color:'<%= Str_StateWiseAppCountINDColor.ToString() %>', data: <%= Str_RegionWiseNOCIssuedExIND.ToString() %>

                },
                            {
                                name: 'Infrastructure', color:'<%= Str_StateWiseAppCountINFColor.ToString() %>', data: <%= Str_RegionWiseNOCIssuedExINF.ToString() %>
                                },
                            {
                                name: 'Mining', color:'<%= Str_StateWiseAppCountMINColor.ToString() %>',       data: <%= Str_RegionWiseNOCIssuedExMIN.ToString() %>
                                }
                ]

            }
                    
                    );

        }


        function ChartRegionWiseNOCIssuedExRen() {
            Highcharts.chart('ChartRegionWiseNOCIssuedExRen', {
                chart: {
                    type: 'column',
                    events: {
                        drilldown: function (e) {
                            if (e.point.y > 0) {
                                generateDrlStateWiseAndMoreThanOrLessThan12M(e.point.FullName, e.point.drilldown, e.point.series.RegionShortName);
                            }
                        },
                        drillup: function (e) {
                        }
                    } //events
                }, //chart
                title: {
                    align: 'center',
                    text: 'No of Applications Received Region Wise',
                    style: '{ "color": "#214900" }'


                } //title
                                   , xAxis: {
                                       type: 'category',
                                       title: {

                                           text: 'Region Name'
                                       },
                                       labels: {
                                           rotation: '-45'
                                       }

                                   } //xAxis
                                   ,
                yAxis: {
                    min: 0,
                    title: {
                        text: 'No of Application Received Region Wise'
                    },
                    stackLabels: {
                        enabled: true,
                        style: {
                            fontWeight: 'bold', color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                        }
                    }
                }//yAxis
                                    , legend: { align: 'right', x: -30, verticalAlign: 'bottom', y: 25, floating: true, backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white', borderColor: '#CCC', borderWidth: 1, shadow: false
                                    },
                tooltip: {
                    headerFormat: '', pointFormat: '<b>{point.FullName}</b><br/>{series.name}: {point.y}<br/>Total: {point.stackTotal}'
                },
                plotOptions: {
                    column: {
                        borderRadius: 2,
                        stacking: 'normal',
                        dataLabels: {
                            enabled: true, color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                        }
                    }
                }
                                            , series: [{ name: 'Industrial', color:'<%= Str_StateWiseAppCountINDColor.ToString() %>', data: <%= Str_RegionWiseNOCIssuedExINDRen.ToString() %>

                                            },
                            {
                                name: 'Infrastructure', color:'<%= Str_StateWiseAppCountINFColor.ToString() %>', data: <%= Str_RegionWiseNOCIssuedExINFRen.ToString() %>
                                },
                            {
                                name: 'Mining', color:'<%= Str_StateWiseAppCountMINColor.ToString() %>',       data: <%= Str_RegionWiseNOCIssuedExMINRen.ToString() %>
                                }]

            });

                                    }
                                    //Amardeep







                                                                     



                                                                                                                           

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" Value="" />

    <table width="100%" style="line-height: 25px; border: solid;" class="SubFormWOBG">

        <tr>
            <td colspan="4">
                <div class="div_AreaType" style="background-color: #094E7F; height: 18px; text-align: center; color: White; font-family: Verdana; font-size: 12px; padding-left: 10px; font-size: 15px; height: 25px; padding-top: 4px">
                    <b>Dashboard</b>
                </div>
            </td>
        </tr>
        <tr>
            <td>Application Type:
            </td>
            <td>
                <asp:DropDownList ID="ddlApplicationType" runat="server" Width="150px" CssClass="drop">
                </asp:DropDownList>
            </td>
            <td>AreaType Category:
            </td>
            <td>
                <asp:DropDownList ID="ddlAreatypeCatDesc" runat="server" Style="width: 150px;" CssClass="drop">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>State:
            </td>
            <td>
                <asp:DropDownList ID="ddlState" runat="server" Width="150px" CssClass="drop">
                </asp:DropDownList>
            </td>
            <td>Application Status:
            </td>
            <td>
                <asp:DropDownList ID="ddlApplicationStatus" runat="server" Width="150px" CssClass="drop">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>

            <td>Regional Office:</td>
            <td>
                <asp:DropDownList ID="ddlOffice" runat="server" Width="150px" CssClass="drop">
                </asp:DropDownList>
            </td>
            <td>Application Purpose:
            </td>
            <td align="center">
                <asp:DropDownList ID="ddlApplicationPurpose" runat="server" Style="width: 150px;" CssClass="drop">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Quantity From(<span>m3/day</span>):</td>
            <td>



                <asp:TextBox ID="txtQuantityFrom" runat="server" CssClass="drop"></asp:TextBox>

            </td>
            <td>&nbsp;Quantity To(<span>m3/day</span>):</td>
            <td>&nbsp;<asp:TextBox ID="txtQuantityTo" runat="server" CssClass="drop"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td>From Date:<asp:TextBox ID="txtFromDate" runat="server" ClientIDMode="Static" CssClass="drop"></asp:TextBox></td>
            <td>
                <asp:TextBox ID="txtToDate" runat="server" CssClass="drop" ClientIDMode="Static"> </asp:TextBox></td>
        </tr>









    </table>
    <table width="100%">
        <tr>
            <td colspan="2" align="center">
                <asp:Button runat="server" ID="btnShowChart" Width="150px" OnClick="btnShowChart_Click" Text="Show" class="btn btn-info" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnReset" Width="150px" Text="Reset" class="btn btn-info" OnClick="btnReset_Click" />
            </td>


        </tr>
    </table>
    <table width="100%" class="SubFormWOBG" style="border: solid;">

        <tr>
            <td colspan="2">
                <asp:Label runat="server" Text="Status of  Fresh Applications" Font-Underline="True" Font-Bold="True" Font-Size="Medium"></asp:Label>&nbsp;</td>
            <td colspan="2">
                <asp:Label runat="server" Font-Underline="True" Font-Bold="True" Text="Status of  Renewal Applications" Font-Size="Medium"></asp:Label>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" style="background-color: #dff0d8;">
                <asp:Label ID="Label10" runat="server" Text="Total No of Applications Received" Font-Bold="True"></asp:Label>
                <b>&nbsp;- </b>&nbsp;<asp:Label ID="lblTotalNoofapplicationreceived" runat="server" Text="Label"></asp:Label>
            </td>
            <td colspan="2" style="background-color: #dff0d8;">
                <asp:Label ID="Label11" runat="server" Text="Total No of Applications Received" Font-Bold="True"></asp:Label>
                &nbsp;-
                <asp:Label ID="lblTotalNoofapplicationreceivedRen" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="background-color: #d9edf7;">
                <asp:Label ID="Label1" runat="server" Text="In process" Font-Bold="True"></asp:Label>
                <b>&nbsp;- </b>
                &nbsp;<asp:Label ID="lblInprocess" runat="server" Text="Label"></asp:Label>
            </td>
            <td style="background-color: #d9edf7;" colspan="2">
                <asp:Label ID="Label12" runat="server" Text="In process" Font-Bold="True"></asp:Label>
                &nbsp;-
                <asp:Label ID="lblInprocessRen" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="background-color: #fcf8e3;">
                <asp:Label ID="Label3" runat="server" Text="Refer back" Font-Bold="True"></asp:Label>
                <b>&nbsp;- </b>&nbsp;<asp:Label ID="lblReferback" runat="server" Text="Label"></asp:Label>
            </td>
            <td colspan="2" style="background-color: #fcf8e3;">
                <asp:Label ID="Label2" runat="server" Text="Refer back" Font-Bold="True"></asp:Label>

                &nbsp;-
                <asp:Label ID="lblReferbackRen" runat="server" Text="Label"></asp:Label>

            </td>
        </tr>
        <tr>
            <td colspan="2" style="background-color: #f2dede;">
                <asp:Label ID="Label5" runat="server" Text="Approved" Font-Bold="True"></asp:Label>
                <b>&nbsp;- </b>
                &nbsp;<asp:Label ID="lblApproved" runat="server" Text="Label"></asp:Label>
            </td>
            <td colspan="2" style="background-color: #f2dede;">
                <asp:Label ID="Label14" runat="server" Text="Approved" Font-Bold="True"></asp:Label>
                &nbsp;-
                <asp:Label ID="lblApprovedRen" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="background-color: #dff0d8;">
                <asp:Label ID="Label7" runat="server" Text="Rejected" Font-Bold="True"></asp:Label>
                <b>&nbsp;- </b>
                &nbsp;<asp:Label ID="lblRejected" runat="server" Text="Label"></asp:Label>
            </td>
            <td colspan="2" style="background-color: #dff0d8;">
                <asp:Label ID="Label15" runat="server" Text="Rejected" Font-Bold="True"></asp:Label>
                &nbsp;-
                <asp:Label ID="lblRejectedRen" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="background-color: #d9edf7;">
                <asp:Label ID="Label16" runat="server" Text="Exempted" Font-Bold="True"></asp:Label>
                <b>&nbsp;- </b>
                &nbsp;<asp:Label ID="lblExempted" runat="server" Text="Label"></asp:Label>
            </td>
            <td colspan="2" style="background-color: #d9edf7;">
                <asp:Label ID="Label19" runat="server" Text="Exempted" Font-Bold="True"></asp:Label>
                &nbsp;-
                <asp:Label ID="lblExemptedRen" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="background-color: #fcf8e3;">
                <asp:Label ID="Label17" runat="server" Text="Cancelled" Font-Bold="True"></asp:Label>
                <b>&nbsp;- </b>
                &nbsp;<asp:Label ID="lblCancelled" runat="server" Text="Label"></asp:Label>
            </td>
            <td colspan="2" style="background-color: #fcf8e3;">
                <asp:Label ID="Label20" runat="server" Text="Cancelled" Font-Bold="True"></asp:Label>
                &nbsp;-
                <asp:Label ID="lblCancelledRen" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="background-color: #f2dede;">
                <asp:Label ID="Label18" runat="server" Text="Submitted" Font-Bold="True"></asp:Label>
                <b>&nbsp;- </b>
                &nbsp;<asp:Label ID="lblSubmitted" runat="server" Text="Label"></asp:Label>
            </td>
            <td colspan="2" style="background-color: #f2dede;">
                <asp:Label ID="Label21" runat="server" Text="Submitted" Font-Bold="True"></asp:Label>
                &nbsp;-
                <asp:Label ID="lblSubmittedRen" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>



        <tr>
            <td colspan="4" width="100%">
                <div class="row">
                    <div id="div7" class="col-sm-12">
                        <div class="chart-wrapper">
                            <div class="chart-title" style="background-color: #3D4A57; color: White; height: 75px;">
                                <img src="../../../Images/123.png" alt="Mountain View" />
                                <b>No of NOCs issued by CGWA</b>
                            </div>
                            <div class="chart-stage">
                                <div id="chart-RegionWiseNOCIssuedEx">
                                    <div id="ChartRegionWiseNOCIssuedEx">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </td>

        </tr>
        <tr>
            <td colspan="4" width="100%">
                <div class="row">
                    <div id="divRen" class="col-sm-12">
                        <div class="chart-wrapper">
                            <div class="chart-title" style="background-color: #3D4A57; color: White; height: 75px;">
                                <img src="../../../Images/123.png" alt="Mountain View" />
                                <b>No of Applications Received Region Wise </b>
                            </div>
                            <div class="chart-stage">
                                <div id="chart-RegionWiseNOCIssuedExRen">
                                    <div id="ChartRegionWiseNOCIssuedExRen">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>





    </table>
</asp:Content>

