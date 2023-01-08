using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Drawing;
using System.Threading;
using System.Configuration;
using System.IO;


public partial class ExternalUser_SelfInspection_SelfInspectionSumit : System.Web.UI.Page
{
    string strPageName = "SelfInspectionAttachment";
    string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;


            if (PreviousPage != null)
            {
                Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                if (placeHolder != null)
                {
                    Label SourceLabel = (Label)placeHolder.FindControl("lblApplicationCodeFrom");
                    if (SourceLabel != null)
                    {
                        lblApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                    }
                    if (NOCAPExternalUtility.FillDropDownMeterType(ref ddlMeterType) != 1)
                    {
                        lblMessage.Text = "Problem in Meter Type";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    if (NOCAPExternalUtility.FillDropDownNameOfAgency(ref ddlNameOfAgency) != 1)
                    {
                        lblMessage.Text = "Problem in Name Of Agency";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    if (NOCAPExternalUtility.FillCheckBoxListTypeOfARStructure(ref chklistTypeOfARStruct) != 1)
                    {
                        lblMessage.Text = "Problem in Type of Structure population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;

                    }
                    if (lblApplicationCodeFrom.Text.Trim() != "")
                    {
                        GetINDINFMINDetails(Convert.ToInt64(lblApplicationCodeFrom.Text));
                        PopulateData(Convert.ToInt64(lblApplicationCodeFrom.Text), sender, e);
                        BindGridView(gvNOC, Convert.ToInt64(lblApplicationCodeFrom.Text));
                        BindGridView(gvSiteInspection, Convert.ToInt64(lblApplicationCodeFrom.Text));
                        BindGridView(gvgeophotostruct, Convert.ToInt64(lblApplicationCodeFrom.Text));
                        BindGridView(gvGeoPhotowellfitted, Convert.ToInt64(lblApplicationCodeFrom.Text));
                        BindGridView(gvWQRSubmitted, Convert.ToInt64(lblApplicationCodeFrom.Text));
                        BindGridView(gvMineseepage, Convert.ToInt64(lblApplicationCodeFrom.Text));
                        BindGridView(gvRainwaterharvesting, Convert.ToInt64(lblApplicationCodeFrom.Text));
                        BindGridView(gvGroundwaterMonitoring, Convert.ToInt64(lblApplicationCodeFrom.Text));
                        BindGridView(gvGroundWaterMonitoringData, Convert.ToInt64(lblApplicationCodeFrom.Text));
                        BindGridView(gvSTPETP, Convert.ToInt64(lblApplicationCodeFrom.Text));
                        BindGridView(gvWaterAuditInspection, Convert.ToInt64(lblApplicationCodeFrom.Text));
                        BindGridView(gvIAR, Convert.ToInt64(lblApplicationCodeFrom.Text));
                        BindGridView(gvExtra, Convert.ToInt64(lblApplicationCodeFrom.Text));

                        BindGridView(gvAnnualCalibration, Convert.ToInt64(lblApplicationCodeFrom.Text));






                    }

                }

            }
        }
    }
    #region Private Method
    private void PopulateData(long lngA_ApplicationCode, object sender, EventArgs e)
    {
        try
        {
            NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfCompliance = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(lngA_ApplicationCode);

            if (obj_selfCompliance.ApplicationCode != 0)
            {
                txtNOCNo.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.NOCNo));
                txtNOCStartDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_selfCompliance.ValidityStartDate).ToString("dd/MM/yyyy"));
                txtNOCEndDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_selfCompliance.ValidityEndDate).ToString("dd/MM/yyyy"));


                txtQtyAbstractionPerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.GroundWaterAbsDayAppr));


                txtQtyDewateringPerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.GroundWaterDewDayAppr));
                txtQtyAbstractionPerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.GroundWaterAbsYearAppr));
                if (obj_selfCompliance.GroundWaterDewYearAppr != null)
                    txtQtyDewateringPerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.GroundWaterDewYearAppr));







                if (obj_selfCompliance.InspectionAgencyCode > 0)
                {
                    ddlNameOfAgency.SelectedValue = Convert.ToString((int)obj_selfCompliance.InspectionAgencyCode);
                    txtOtherAgency.Text = HttpUtility.HtmlEncode(obj_selfCompliance.AnyOtherAgency);
                    txtDateOfInsp1.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_selfCompliance.DateOfInspection).ToString("dd/MM/yyyy"));

                    switch (obj_selfCompliance.InspectionReport)
                    {
                        case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                            ddlSiteInsp.SelectedValue = "1";
                            break;
                        case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                            ddlSiteInsp.SelectedValue = "0";
                            break;
                        case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                            ddlSiteInsp.SelectedValue = "";
                            break;
                        default:
                            ddlSiteInsp.SelectedValue = "";
                            break;
                    }

                }
             
            
               
                switch (obj_selfCompliance.PresentGroundWaterReq)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlPresentwithdrawal.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlPresentwithdrawal.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlPresentwithdrawal.SelectedValue = "";
                        break;
                    default:
                        ddlPresentwithdrawal.SelectedValue = "";
                        break;

                }
                txtPresentwithdrawalInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.PresentGroundWaterReqInDay));
                txtPresentwithdrawalInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.PresentGroundWaterReqInYear));
                txtDewPresentwithdrawalInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.PreGroundWaterDewReqInDay));
                txtDewPresentwithdrawalInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.PreGroundWaterDewReqInYear));
                switch (obj_selfCompliance.PreGroundWaterAnyVari)
                {
                    case NOCAP.BLL.Common.CommonEnum.VariationInQuantum.MoreThanPermitted:
                        ddlAnyVariation.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.VariationInQuantum.LessThanPermitted:
                        ddlAnyVariation.SelectedValue = "2";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.VariationInQuantum.NoVariation:
                        ddlAnyVariation.SelectedValue = "3";
                        break;

                    case NOCAP.BLL.Common.CommonEnum.VariationInQuantum.NotDefined:
                        ddlAnyVariation.SelectedValue = "";
                        break;
                    default:
                        ddlAnyVariation.SelectedValue = "";
                        break;

                }
                txtQtyVariDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.PreGroundWaterAnyVariReqInDay));
                txtQtyVariYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.PreGroundWaterAnyVariReqInYear));

                txtAbstraStructExistingAsperNOC.Text = HttpUtility.HtmlEncode(obj_selfCompliance.AbstraStructExistingAsPerNOC);
                txtAbstraStructExisting.Text = HttpUtility.HtmlEncode(obj_selfCompliance.AbstraStructExisting);
                txtAbstraStructProposedAsperNOC.Text = HttpUtility.HtmlEncode(obj_selfCompliance.AbstraStructProposedAsPerNOC);
                txtAbstraStructProposed.Text = HttpUtility.HtmlEncode(obj_selfCompliance.AbstraStructProposed);
                txtNoOfFunt.Text = HttpUtility.HtmlEncode(obj_selfCompliance.FuncAbstStruct);
                txtselfPresentwithdrawalInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.PreSelfsentGroundWaterReqInDay));
                txtselfPresentwithdrawalInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.PreSelfsentGroundWaterReqInYear));
                txtselfDewPresentwithdrawalInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.PreSelfGroundWaterDewReqInDay));
                txtselfDewPresentwithdrawalInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.PreSelfGroundWaterDewReqInYear));

                if (txtselfPresentwithdrawalInDay.Text.Trim() != "")
                    obj_selfCompliance.PreSelfsentGroundWaterReqInDay = Convert.ToDecimal(txtselfPresentwithdrawalInDay.Text.Trim());
                if (txtselfPresentwithdrawalInYear.Text.Trim() != "")
                    obj_selfCompliance.PreSelfsentGroundWaterReqInYear = Convert.ToDecimal(txtselfPresentwithdrawalInYear.Text.Trim());
                if (txtselfDewPresentwithdrawalInDay.Text.Trim() != "")
                    obj_selfCompliance.PreSelfGroundWaterDewReqInDay = Convert.ToDecimal(txtselfDewPresentwithdrawalInDay.Text.Trim());
                if (txtselfDewPresentwithdrawalInYear.Text.Trim() != "")
                    obj_selfCompliance.PreSelfGroundWaterDewReqInYear = Convert.ToDecimal(txtselfDewPresentwithdrawalInYear.Text.Trim());

                switch (obj_selfCompliance.StructPhoto)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlgeophotostruct.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlgeophotostruct.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlgeophotostruct.SelectedValue = "";
                        break;
                    default:
                        ddlgeophotostruct.SelectedValue = "";
                        break;

                }

                switch (obj_selfCompliance.AbstStructFittedWithWM)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        AbstStructWM.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        AbstStructWM.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        AbstStructWM.SelectedValue = "";
                        break;
                    default:
                        AbstStructWM.SelectedValue = "";
                        break;

                }

                ddlMeterType.SelectedValue = HttpUtility.HtmlEncode(obj_selfCompliance.MeterTypeCode);

                switch (obj_selfCompliance.TelemInstalled)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlTeleInstalled.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlTeleInstalled.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlTeleInstalled.SelectedValue = "";
                        break;
                    default:
                        ddlTeleInstalled.SelectedValue = "";
                        break;
                }
                txtFunctMeter.Text = HttpUtility.HtmlEncode(obj_selfCompliance.NumberOfFunMeter);
                switch (obj_selfCompliance.RainWaterHarv)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlRainwaterharvesting.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlRainwaterharvesting.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlRainwaterharvesting.SelectedValue = "";
                        break;
                    default:
                        ddlRainwaterharvesting.SelectedValue = "";
                        break;
                }
                switch (obj_selfCompliance.AnnuCalibOfWM)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlAnnualCali.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlAnnualCali.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlAnnualCali.SelectedValue = "";
                        break;
                    default:
                        ddlAnnualCali.SelectedValue = "";
                        break;
                }
                switch (obj_selfCompliance.PhotoWellFittedWithWM)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlGeoPhotoFittedWM.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlGeoPhotoFittedWM.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlGeoPhotoFittedWM.SelectedValue = "";
                        break;
                    default:
                        ddlGeoPhotoFittedWM.SelectedValue = "";
                        break;
                }
                switch (obj_selfCompliance.GWQuality)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlGWQReport.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlGWQReport.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlGWQReport.SelectedValue = "";
                        break;
                    default:
                        ddlGWQReport.SelectedValue = "";
                        break;
                }

                switch (obj_selfCompliance.MineSeepageQuality)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlMineSeepage.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlMineSeepage.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlMineSeepage.SelectedValue = "";
                        break;
                    default:
                        ddlMineSeepage.SelectedValue = "";
                        break;
                }
                switch (obj_selfCompliance.WaterSampleAnalyzed)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlWaterSampple.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlWaterSampple.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlWaterSampple.SelectedValue = "";
                        break;
                    default:
                        ddlWaterSampple.SelectedValue = "";
                        break;
                }
                switch (obj_selfCompliance.WaterSampleAnalyzed)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlWQRSubmitted.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlWQRSubmitted.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlWQRSubmitted.SelectedValue = "";
                        break;
                    default:
                        ddlWQRSubmitted.SelectedValue = "";
                        break;
                }
                //for (int j = 0; j < chklistTypeOfARStruct.Items.Count; j++)
                //{
                //    chklistTypeOfARStruct.Items[j].Selected = false;
                //}

                NOCAP.BLL.Master.SelfInspTypeOfARStructureDetail obj_SelfInspTypeOfARStructureDetail = new NOCAP.BLL.Master.SelfInspTypeOfARStructureDetail();
                obj_SelfInspTypeOfARStructureDetail.ApplicationCode = obj_selfCompliance.ApplicationCode;
                obj_SelfInspTypeOfARStructureDetail.GetAll();


                NOCAP.BLL.Master.SelfInspTypeOfARStructureDetail[] arr_SelfInspTypeOfARStructureDetail = obj_SelfInspTypeOfARStructureDetail.SelfInspTypeOfARStructureDetailCollection;

                if (arr_SelfInspTypeOfARStructureDetail != null)
                {
                    if (arr_SelfInspTypeOfARStructureDetail.Length > 0)
                    {
                        for (int i = 0; i < arr_SelfInspTypeOfARStructureDetail.Length; i++)
                        {
                            for (int j = 0; j < chklistTypeOfARStruct.Items.Count; j++)
                            {
                                if (Convert.ToInt32(chklistTypeOfARStruct.Items[j].Value) == arr_SelfInspTypeOfARStructureDetail[i].TypeOfARStructCode)

                                    chklistTypeOfARStruct.Items[j].Selected = true;


                            }
                        }

                    }
                }

                txtNoOfStruct.Text = HttpUtility.HtmlEncode(obj_selfCompliance.NumberOfStruct);

                switch (obj_selfCompliance.WithOutPremises)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlWithinOutSidePremises.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlWithinOutSidePremises.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlWithinOutSidePremises.SelectedValue = "";
                        break;
                    default:
                        ddlWithinOutSidePremises.SelectedValue = "";
                        break;
                }
                txtQuantOfRecharge.Text = HttpUtility.HtmlEncode(obj_selfCompliance.QuantOfRecharge);
                switch (obj_selfCompliance.PhotoRechargeStruct)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlGeoPhotoRechargeStruc.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlGeoPhotoRechargeStruc.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlGeoPhotoRechargeStruc.SelectedValue = "";
                        break;
                    default:
                        ddlGeoPhotoRechargeStruc.SelectedValue = "";
                        break;
                }
                txtNOPiezo.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezo));
                txtNOPiezoDWLR.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoDWLR));
                txtpieDWLRTelem.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoDWLRTelem));


                txtselfNOPiezo.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfSelfPiezo));
                txtselfNOPiezoDWLR.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfSelfPiezoDWLR));
                //txtselfNOPiezoTele.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfSelfPiezoTelem));
                txtselfpieDWLRTelem.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfSelfPiezoDWLRTelem));



                //txtNOPiezoTele.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoTelem));
                //txtNoOversWell.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfObserwell));
                NoFunctionalPiewell.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfFunctPiezo));

                switch (obj_selfCompliance.GeoPiezoAWLRTelem)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlgeophotopiewell.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlgeophotopiewell.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlgeophotopiewell.SelectedValue = "";
                        break;
                    default:
                        ddlgeophotopiewell.SelectedValue = "";
                        break;
                }
                switch (obj_selfCompliance.MoniDataSubmitted)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlMoniDataSubmitted.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlMoniDataSubmitted.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlMoniDataSubmitted.SelectedValue = "";
                        break;
                    default:
                        ddlMoniDataSubmitted.SelectedValue = "";
                        break;
                }
                switch (obj_selfCompliance.PiezometerDWLRTelemetry)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlpieDWLRTelem.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlpieDWLRTelem.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlpieDWLRTelem.SelectedValue = "";
                        break;
                    default:
                        ddlpieDWLRTelem.SelectedValue = "";
                        break;
                }
                switch (obj_selfCompliance.GroundWaterMonitoringData)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlGroundWaterMonitoringData.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlGroundWaterMonitoringData.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlGroundWaterMonitoringData.SelectedValue = "";
                        break;
                    default:
                        ddlGroundWaterMonitoringData.SelectedValue = "";
                        break;
                }
                switch (obj_selfCompliance.STPETP)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlSTPETP.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlSTPETP.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlSTPETP.SelectedValue = "";
                        break;
                    default:
                        ddlSTPETP.SelectedValue = "";
                        break;
                }
                if (obj_selfCompliance.NoOfSTPETP != null)
                    txtNoSTPETP.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfSTPETP));
                if (obj_selfCompliance.CapOfSTPETP != null)
                    txtCapSTPETP.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.CapOfSTPETP));
                if (obj_selfCompliance.QuanttreatWasteWater != null)
                    txtQuantumTWW.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.QuanttreatWasteWater));
                switch (obj_selfCompliance.GeoPhotoOfSTP)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlgeophotoSTPETP.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlgeophotoSTPETP.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlgeophotoSTPETP.SelectedValue = "";
                        break;
                    default:
                        ddlgeophotoSTPETP.SelectedValue = "";
                        break;
                }
                if (obj_selfCompliance.QuanttreatWasteWaterIND != null)
                    txtIndProcess.Text = HttpUtility.HtmlEncode(Convert.ToDecimal(obj_selfCompliance.QuanttreatWasteWaterIND));
                if (obj_selfCompliance.QuanttreatWasteWaterINDGreen != null)
                    txtGreenbelt.Text = HttpUtility.HtmlEncode(Convert.ToDecimal(obj_selfCompliance.QuanttreatWasteWaterINDGreen));
                if (obj_selfCompliance.QuanttreatWasteWaterINDOther != null)
                    txtOtherUse.Text = HttpUtility.HtmlEncode(Convert.ToDecimal(obj_selfCompliance.QuanttreatWasteWaterINDOther));
                switch (obj_selfCompliance.SubSCWithinTimeFrame)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlComplianceReportWSTF.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlComplianceReportWSTF.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlComplianceReportWSTF.SelectedValue = "";
                        break;
                    default:
                        ddlComplianceReportWSTF.SelectedValue = "";
                        break;
                }
                switch (obj_selfCompliance.WaterAuditInspectionApplicable)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        IFddlWaterAuditInspection.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        IFddlWaterAuditInspection.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        IFddlWaterAuditInspection.SelectedValue = "";
                        break;
                    default:
                        IFddlWaterAuditInspection.SelectedValue = "";
                        break;
                }
                switch (obj_selfCompliance.WaterAuditInspection)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlWateAuditinspection.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlWateAuditinspection.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlWateAuditinspection.SelectedValue = "";
                        break;
                    default:
                        ddlWateAuditinspection.SelectedValue = "";
                        break;
                }
                switch (obj_selfCompliance.WaterAuditInspectionAsPerNOC)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlWateAuditinspectionNOC.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlWateAuditinspectionNOC.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlWateAuditinspectionNOC.SelectedValue = "";
                        break;
                    default:
                        ddlWateAuditinspectionNOC.SelectedValue = "";
                        break;
                }
                txtAuditAgency.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.AuditAgency));
                if (obj_selfCompliance.DateOfInspectionAudit != null)

                    txtDateOfInsp.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_selfCompliance.DateOfInspectionAudit).ToString("dd/MM/yyyy"));
                switch (obj_selfCompliance.WaterAuditReport)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlWaterauditreportattached.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlWaterauditreportattached.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlWaterauditreportattached.SelectedValue = "";
                        break;
                    default:
                        ddlWaterauditreportattached.SelectedValue = "";
                        break;
                }


                switch (obj_selfCompliance.ImpactAssementReportApplicable)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlImpactassessmentreport.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlImpactassessmentreport.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlImpactassessmentreport.SelectedValue = "";
                        break;
                    default:
                        ddlImpactassessmentreport.SelectedValue = "";
                        break;
                }
                switch (obj_selfCompliance.ImpactAssementReport)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlRequirement.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlRequirement.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlRequirement.SelectedValue = "";
                        break;
                    default:
                        ddlRequirement.SelectedValue = "";
                        break;
                }
                switch (obj_selfCompliance.ImpactAssementReportSubmitted)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlSubmittedNOC.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlSubmittedNOC.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlSubmittedNOC.SelectedValue = "";
                        break;
                    default:
                        ddlSubmittedNOC.SelectedValue = "";
                        break;
                }
                switch (obj_selfCompliance.ImpactAssementAIRReport)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlCopyIAR.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlCopyIAR.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlCopyIAR.SelectedValue = "";
                        break;
                    default:
                        ddlCopyIAR.SelectedValue = "";
                        break;
                }
                switch (obj_selfCompliance.AnyViolationNOCCondi)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlAnyViolation.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlAnyViolation.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlAnyViolation.SelectedValue = "";
                        break;
                    default:
                        ddlAnyViolation.SelectedValue = "";
                        break;
                }
                txtAnyViolation.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.AnyViolationNOCCondiDesc));
                switch (obj_selfCompliance.AnyOtherInspection)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlAnyothercompliance.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlAnyothercompliance.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlAnyothercompliance.SelectedValue = "";
                        break;
                    default:
                        ddlAnyothercompliance.SelectedValue = "";
                        break;
                }
                switch (obj_selfCompliance.Undertaking)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        chkUndertaking.Checked = true;
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        chkUndertaking.Checked = false;
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        chkUndertaking.Checked = false;
                        break;
                    default:
                        chkUndertaking.Checked = false;
                        break;
                }
                
                txtAnyothercompliance.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.AnyOtherInspection));

                txtRemarks.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.Remarks));


            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    private void GetINDINFMINDetails(long ApplicationCode)
    {

        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication();


        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication();
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication();
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication();



        NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_industrialNewApplication, out obj_infrastructureNewApplication, out obj_miningNewApplication, out obj_industrialRenewApplication, out obj_infrastructureRenewApplication, out obj_miningRenewApplication, ApplicationCode);

        if (obj_industrialNewApplication != null)
        {
            GetIndustrialDetails(ApplicationCode);
            BindNewINDIssuedLetterDetails(ApplicationCode);
        }
        else if (obj_infrastructureNewApplication != null)
        {
            GetInfrastructureDetails(ApplicationCode);
            BindNewINFIssuedLetterDetails(ApplicationCode);
        }
        else if (obj_miningNewApplication != null)
        {
            GetMininingDetails(ApplicationCode);
            BindNewMINIssuedLetterDetails(ApplicationCode);
        }
        else if (obj_industrialRenewApplication != null)
        {
            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(ApplicationCode);
            if (obj_IndustrialRenewApplication != null)
                GetIndustrialRenewDetails(obj_IndustrialRenewApplication.IndustrialRenewApplicationCode);
            BindRenewINDIssuedLetterDetails(ApplicationCode);

        }
        else if (obj_infrastructureRenewApplication != null)
        {
            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(ApplicationCode);
            if (obj_InfrastructureRenewApplication != null)
                GetInfrastructureRenewDetails(obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode);
            BindRenewINFIssuedLetterDetails(ApplicationCode);
        }
        else if (obj_miningRenewApplication != null)
        {
            NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(ApplicationCode);
            if (obj_MiningRenewApplication != null)
                GetMininingRenewDetails(obj_MiningRenewApplication.MiningRenewApplicationCode);
            BindRenewMINIssuedLetterDetails(ApplicationCode);
        }
        else
        {
            lblMessage.Text = " Application Code does not exist.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Visible = true;
        }


    }
    private void GetIndustrialRenewDetails(long ApplicationCode)
    {
        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(ApplicationCode);
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_industrialRenewApplication.FirstApplicationCode);

        NOCAP.BLL.Master.ApplicationTypeCategory obj_ApplicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_industrialNewApplication.ApplicationTypeCode, obj_industrialNewApplication.ApplicationTypeCategoryCode);
        NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
        NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);

        NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter obj_IndustrialRenewIssusedLetter = obj_industrialRenewApplication.GetIssuedLetter();

        try
        {

            if (obj_industrialRenewApplication != null)
            {


                lblApplicationCode.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplication.IndustrialRenewApplicationCode);
                lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.IndustrialNewApplicationNumber);
                lblAppliedFor.Text = HttpUtility.HtmlEncode("Renew (") + HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(obj_industrialRenewApplication.LinkDepth)) + ")";


                lblTypeOfProject.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.ApplicationType(obj_industrialNewApplication.ApplicationTypeCode).ApplicationTypeDescription);

                lblNameOfIndustry.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.NameOfIndustry);



            }

        }
        catch (Exception)
        {
            Response.Redirect("~/InternalErrorPage.aspx", false);
        }
        finally
        {
            obj_industrialNewApplication = null;
            obj_ApplicationTypeCategory = null;
            obj_District = null;
            obj_SubDistrict = null;
            obj_Town = null;
            obj_Village = null;
        }
    }
    private void GetInfrastructureRenewDetails(long ApplicationCode)
    {
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(ApplicationCode);
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_InfrastructureRenewApplication.FirstApplicationCode);
        NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_InfrastructureNewApplication.CommunicationAddress.StateCode, obj_InfrastructureNewApplication.CommunicationAddress.DistrictCode);
        NOCAP.BLL.Master.District obj_DistrictPro = new NOCAP.BLL.Master.District(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode);
        NOCAP.BLL.Master.ApplicationTypeCategory obj_ApplicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_InfrastructureNewApplication.ApplicationTypeCode, obj_InfrastructureNewApplication.ApplicationTypeCategoryCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.CommunicationAddress.StateCode, obj_InfrastructureNewApplication.CommunicationAddress.DistrictCode, obj_InfrastructureNewApplication.CommunicationAddress.SubDistrictCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrictPro = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
        NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);

        if (obj_InfrastructureRenewApplication != null)
        {

            try
            {
                lblApplicationCode.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode);
                lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber);
                lblAppliedFor.Text = HttpUtility.HtmlEncode("Renew (") + HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(obj_InfrastructureRenewApplication.LinkDepth)) + ")";
                lblTypeOfProject.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.ApplicationType(obj_InfrastructureNewApplication.ApplicationTypeCode).ApplicationTypeDescription);
                lblNameOfIndustry.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.NameOfInfrastructure);
            }
            catch (Exception)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);
            }
            finally
            {
                obj_InfrastructureNewApplication = null;
                obj_ApplicationTypeCategory = null;
                obj_District = null;
                obj_SubDistrict = null;
                obj_Town = null;
                obj_Village = null;
            }
        }
    }
    private void GetMininingRenewDetails(long ApplicationCode)
    {
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(ApplicationCode);
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_MiningRenewApplication.FirstApplicationCode);
        NOCAP.BLL.Master.ApplicationTypeCategory obj_ApplicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_MiningNewApplication.ApplicationTypeCode, obj_MiningNewApplication.ApplicationTypeCategoryCode);
        NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_MiningNewApplication.CommunicationAddress.StateCode, obj_MiningNewApplication.CommunicationAddress.DistrictCode);
        NOCAP.BLL.Master.District obj_DistrictPro = new NOCAP.BLL.Master.District(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.CommunicationAddress.StateCode, obj_MiningNewApplication.CommunicationAddress.DistrictCode, obj_MiningNewApplication.CommunicationAddress.SubDistrictCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrictPro = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
        NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);

        if (obj_MiningNewApplication != null)
        {

            try
            {
                lblApplicationCode.Text = HttpUtility.HtmlEncode(obj_MiningRenewApplication.MiningRenewApplicationCode);
                lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.MiningNewApplicationNumber);
                lblAppliedFor.Text = HttpUtility.HtmlEncode("Renew (") + HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(obj_MiningRenewApplication.LinkDepth)) + ")";
                lblTypeOfProject.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.ApplicationType(obj_MiningNewApplication.ApplicationTypeCode).ApplicationTypeDescription);

                lblNameOfIndustry.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.NameOfMining);
            }
            catch (Exception)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);
            }
            finally
            {
                obj_MiningNewApplication = null;
                obj_ApplicationTypeCategory = null;
                obj_District = null;
                obj_SubDistrict = null;
                obj_Town = null;
                obj_Village = null;
            }
        }
    }
    private void GetIndustrialDetails(long ApplicationCode)
    {
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(ApplicationCode);
        NOCAP.BLL.Master.ApplicationTypeCategory obj_ApplicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_industrialNewApplication.ApplicationTypeCode, obj_industrialNewApplication.ApplicationTypeCategoryCode);
        NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
        NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);

        NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_IndustrialNewIssusedLetter = obj_industrialNewApplication.GetIssuedLetter();

        if (obj_industrialNewApplication != null)
        {

            try
            {
                lblApplicationCode.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.IndustrialNewApplicationCode);
                lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.IndustrialNewApplicationNumber);
                lblAppliedFor.Text = HttpUtility.HtmlEncode("Fresh");
                lblTypeOfProject.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.ApplicationType(obj_industrialNewApplication.ApplicationTypeCode).ApplicationTypeDescription);

                lblNameOfIndustry.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.NameOfIndustry);


            }
            catch (Exception)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);
            }
            finally
            {
                obj_industrialNewApplication = null;
                obj_ApplicationTypeCategory = null;
                obj_District = null;
                obj_SubDistrict = null;
                obj_Town = null;
                obj_Village = null;
            }
        }
    }
    private void BindRenewINDIssuedLetterDetails(long ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(ApplicationCode);
            NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter obj_IndustrialRenewIssusedLetter = obj_industrialRenewApplication.GetIssuedLetter();
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplicationFirst = obj_industrialRenewApplication.GetFirstIndustrialApplication();

        }
        catch (Exception)
        {
            Response.Redirect("~/InternalErrorPage.aspx", false);
        }
    }
    private void GetInfrastructureDetails(long ApplicationCode)
    {
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(ApplicationCode);
        NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_InfrastructureNewApplication.CommunicationAddress.StateCode, obj_InfrastructureNewApplication.CommunicationAddress.DistrictCode);
        NOCAP.BLL.Master.District obj_DistrictPro = new NOCAP.BLL.Master.District(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode);
        NOCAP.BLL.Master.ApplicationTypeCategory obj_ApplicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_InfrastructureNewApplication.ApplicationTypeCode, obj_InfrastructureNewApplication.ApplicationTypeCategoryCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.CommunicationAddress.StateCode, obj_InfrastructureNewApplication.CommunicationAddress.DistrictCode, obj_InfrastructureNewApplication.CommunicationAddress.SubDistrictCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrictPro = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
        NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);

        if (obj_InfrastructureNewApplication != null)
        {

            try
            {
                lblApplicationCode.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.InfrastructureNewApplicationCode);
                lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber);
                lblAppliedFor.Text = HttpUtility.HtmlEncode("Fresh");
                lblTypeOfProject.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.ApplicationType(obj_InfrastructureNewApplication.ApplicationTypeCode).ApplicationTypeDescription);

                lblNameOfIndustry.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.NameOfInfrastructure);

            }
            catch (Exception)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);
            }
            finally
            {
                obj_InfrastructureNewApplication = null;
                obj_ApplicationTypeCategory = null;
                obj_District = null;
                obj_SubDistrict = null;
                obj_Town = null;
                obj_Village = null;
            }
        }
    }
    private void GetMininingDetails(long ApplicationCode)
    {
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(ApplicationCode);
        NOCAP.BLL.Master.ApplicationTypeCategory obj_ApplicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_MiningNewApplication.ApplicationTypeCode, obj_MiningNewApplication.ApplicationTypeCategoryCode);
        NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_MiningNewApplication.CommunicationAddress.StateCode, obj_MiningNewApplication.CommunicationAddress.DistrictCode);
        NOCAP.BLL.Master.District obj_DistrictPro = new NOCAP.BLL.Master.District(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.CommunicationAddress.StateCode, obj_MiningNewApplication.CommunicationAddress.DistrictCode, obj_MiningNewApplication.CommunicationAddress.SubDistrictCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrictPro = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
        NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);

        if (obj_MiningNewApplication != null)
        {

            try
            {
                lblApplicationCode.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.ApplicationCode);
                lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.MiningNewApplicationNumber);
                lblAppliedFor.Text = HttpUtility.HtmlEncode("Fresh");
                lblTypeOfProject.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.ApplicationType(obj_MiningNewApplication.ApplicationTypeCode).ApplicationTypeDescription);

                lblNameOfIndustry.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.NameOfMining);

            }
            catch (Exception)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);
            }
            finally
            {
                obj_MiningNewApplication = null;
                obj_ApplicationTypeCategory = null;
                obj_District = null;
                obj_SubDistrict = null;
                obj_Town = null;
                obj_Village = null;
            }
        }
    }
    private void BindNewINDIssuedLetterDetails(long ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(ApplicationCode);
            NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_IndustrialNewIssusedLetter = obj_industrialNewApplication.GetIssuedLetter();
            if (obj_IndustrialNewIssusedLetter != null && obj_IndustrialNewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey > 0)
            {
                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_IndustrialNewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);

            }
        }
        catch (Exception)
        {
            Response.Redirect("~/InternalErrorPage.aspx", false);
        }
    }
    private void BindNewINFIssuedLetterDetails(long ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(ApplicationCode);
            NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_InfrastructureNewIssusedLetter = obj_infrastructureNewApplication.GetIssuedLetter();
            if (obj_InfrastructureNewIssusedLetter != null && obj_InfrastructureNewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey > 0)
            {
                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);

            }
        }
        catch (Exception)
        {
            Response.Redirect("~/InternalErrorPage.aspx", false);
        }
    }
    private void BindRenewINFIssuedLetterDetails(long ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(ApplicationCode);

            NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter obj_InfrastructureRenewIssusedLetter = obj_infrastructureRenewApplication.GetIssuedLetter();
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplicationFirst = obj_infrastructureRenewApplication.GetFirstInfrastructureApplication();
            if (obj_InfrastructureRenewIssusedLetter != null && obj_InfrastructureRenewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey > 0)
            {
                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_InfrastructureNewApplicationFirst.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplicationFirst.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplicationFirst.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureRenewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);

            }

        }
        catch (Exception)
        {
            Response.Redirect("~/InternalErrorPage.aspx", false);
        }
    }

    private void BindNewMINIssuedLetterDetails(long ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(ApplicationCode);
            NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter obj_MiningNewIssusedLetter = obj_miningNewApplication.GetIssuedLetter();
            if (obj_MiningNewIssusedLetter != null && obj_MiningNewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey > 0)
            {
                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);

            }
        }
        catch (Exception)
        {
            Response.Redirect("~/InternalErrorPage.aspx", false);
        }
    }
    private void BindRenewMINIssuedLetterDetails(long ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(ApplicationCode);
            NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter obj_MiningRenewIssusedLetter = obj_miningRenewApplication.GetIssuedLetter();
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplicationFirst = obj_miningRenewApplication.GetFirstMiningApplication();

            if (obj_MiningRenewIssusedLetter != null && obj_MiningRenewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey > 0)
            {
                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_MiningNewApplicationFirst.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplicationFirst.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplicationFirst.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningRenewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);

            }

        }
        catch (Exception)
        {
            Response.Redirect("~/InternalErrorPage.aspx", false);
        }
    }
    #endregion
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
                strActionName = "Submit Application";
                try
                {
                    NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_SelfCompliance = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(Convert.ToInt64(lblApplicationCodeFrom.Text));
                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    if (obj_SelfCompliance.ApplicationCode != 0)
                    {

                        if (obj_SelfCompliance.SubmitApplication(obj_SelfCompliance.ApplicationCode, null, obj_externalUser.ExternalUserCode) == 1)
                        {
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Now your record is ready to take action');", true);
                            Server.Transfer("~/ExternalUser/ApplicantHome.aspx");
                        }
                        else
                        {
                            strStatus = "Application Submit Failed";
                            lblFinalMsg.Text = obj_SelfCompliance.CustumMessage;
                            lblFinalMsg.ForeColor = System.Drawing.Color.Red;

                            //display error
                        }

                    }


                }
                catch (ThreadAbortException)
                {


                }
                catch (Exception)
                {
                    Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
                }
                finally
                {
                    ActionTrail obj_ExtActionTrail = new ActionTrail();
                    if (Session["ExternalUserCode"] != null)
                    {
                        obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
                        obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
                        obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
                        obj_ExtActionTrail.Status = strStatus;
                        if (obj_ExtActionTrail != null)
                            ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
                    }
                }
            }
        }
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            Server.Transfer("SelfInspectionC.aspx");

        }
    }

    protected void ViewFile(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment();

                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_applicationCode = Convert.ToInt64(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment obj_selfComplianceAttachmentB = obj_selfComplianceAttachment.DownloadFile(lng_applicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

                    if (obj_selfComplianceAttachmentB != null)
                    {
                        byte[] bytes = obj_selfComplianceAttachmentB.AttachmentFile;
                        HttpContext.Current.Response.Buffer = true;
                        HttpContext.Current.Response.Charset = "";
                        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        HttpContext.Current.Response.ContentType = HttpUtility.HtmlEncode(obj_selfComplianceAttachmentB.ContentType);
                        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "SelfComp_" + Convert.ToString(lng_applicationCode) + "_" + Convert.ToString(int_attachmentCode) + "_" + Convert.ToString(int_attachmentCodeSerialNumber) + obj_selfComplianceAttachmentB.FileExtension);
                        HttpContext.Current.Response.BinaryWrite(bytes);
                        HttpContext.Current.Response.Flush();


                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
            }
        }
    }

    private void BindGridView(GridView gv, long lngA_ApplicationCode)
    {
        NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfCompliance = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(lngA_ApplicationCode);
        NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment[] arr_SelfComplianceAttachment = null;

        if (gv.ID == "gvNOC")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetNOCAttachmentList();
        else if (gv.ID == "gvSiteInspection")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetCopySiteInspectionAttachmentList();
        else if (gv.ID == "gvgeophotostruct")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetGroundwaterAbstractionDataAttachmentList();
        else if (gv.ID == "gvGeoPhotowellfitted")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetWellFittedWithWaterFlowMeterAttachmentList();
        else if (gv.ID == "gvWQRSubmitted")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetGroundWaterQualityAttachmentList();
        else if (gv.ID == "gvRainwaterharvesting")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetGeoRainWaterHarvRechAttachmentList();
        else if (gv.ID == "gvMineseepage")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetMiningSeepageAttachmentList();
        else if (gv.ID == "gvGroundwaterMonitoring")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetGroundwaterMonitoringAttachmentList();
        else if (gv.ID == "gvSTPETP")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetPhotoETPSTPAttachmentList();
        else if (gv.ID == "gvGroundWaterMonitoringData")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetGroundWaterMonitoringDataAttachmentList();
        else if (gv.ID == "gvMineseepage")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetMiningSeepageAttachmentList();

        else if (gv.ID == "gvWaterAuditInspection")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetWaterAuditAttachmentList();
        else if (gv.ID == "gvIAR")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetIARModelingAttachmentList();
        else if (gv.ID == "gvExtra")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetExtraAttachmentList();
        else if (gv.ID == "gvAnnualCalibration")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetAnnualCalibrationAttachmentList();
        gv.DataSource = arr_SelfComplianceAttachment;
        gv.DataBind();

    }

}