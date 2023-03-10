using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Threading;

public partial class ExternalUser_Compliance_SelfComplianceB : System.Web.UI.Page
{
    string strPageName = "SelfCompliance";
    string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                ValidationExepInit();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;

                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");

                    if (placeHolder != null)
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblApplicationCodeFrom");
                        if (SourceLabel != null && SourceLabel.Text != "")
                        {
                            lblApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                        }

                        Label SourceLabelPreviousPage = (Label)placeHolder.FindControl("lblApplicationRenewCode");
                        if (SourceLabelPreviousPage != null && SourceLabelPreviousPage.Text != "")
                        {
                            lblApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabelPreviousPage.Text);
                        }
                        lblApplicationCode.Text = lblApplicationCodeFrom.Text;
                        //txtComplianceSubmitDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
                        else
                        {
                            for (int i = 0; i < chklistTypeOfARStruct.Items.Count; i++)
                            {

                                chklistTypeOfARStruct.Items[i].Selected = false;
                            }
                        }
                        if (lblApplicationCodeFrom.Text.Trim() != "")
                        {
                            PopulateData(Convert.ToInt64(lblApplicationCodeFrom.Text), sender, e);

                            //ddlRecycleReuse_SelectedIndexChanged(sender, e);
                            // ddlRWHArtificialRecharge_SelectedIndexChanged(sender, e);
                            ddlPresentwithdrawal_SelectedIndexChanged(sender, e);
                            ddlNameOfAgency_SelectedIndexChanged(sender, e);
                            ddlSTPETP_SelectedIndexChanged(sender, e);
                            ddlAnnualCali_SelectedIndexChanged(sender, e);

                            ddlGeoPhotoRechargeStruc_SelectedIndexChanged(sender, e);
                            ddlgeophotopiewell_SelectedIndexChanged(sender, e);
                            ddlGroundWaterMonitoringData_SelectedIndexChanged(sender, e);
                            ddlSiteInsp_SelectedIndexChanged(sender, e);
                            ddlgeophotostruct_SelectedIndexChanged(sender, e);
                            ddlGeoPhotoFittedWM_SelectedIndexChanged(sender, e);
                            ddlGWQReport_SelectedIndexChanged(sender, e);
                            ddlMineSeepage_SelectedIndexChanged(sender, e);
                            ddlgeophotoSTPETP_SelectedIndexChanged(sender, e);
                            ddlRainwaterharvesting_SelectedIndexChanged(sender, e);
                            ddlAnyVariation_SelectedIndexChanged(sender, e);
                            BindGridView(gvSiteInspection, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblSiteInspectionCount);


                            BindGridView(gvgeophotostruct, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblgeophotostructCount);
                            BindGridView(gvGeoPhotowellfitted, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblGeoPhotowellfittedCount);
                            BindGridView(gvWQRSubmitted, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblWQRSubmittedCount);
                            BindGridView(gvRainwaterharvesting, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblgvRainwaterharvestingCount);
                            BindGridView(gvMineseepage, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblMineseepageCount);
                            BindGridView(gvGroundwaterMonitoring, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblGroundwaterMonitoringCount);
                            BindGridView(gvSTPETP, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblSTPETPCount);
                            BindGridView(gvGroundWaterMonitoringData, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblGroundWaterMonitoringDataCount);
                            BindGridView(gvMineseepage, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblMineseepageCount);
                            BindGridView(gvNOC, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblMessageNOCCount);
                            BindGridView(gvAnnualCalibration, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblAnnualCalibrationCount);



                        }
                    }
                }
            }


        }
        catch (Exception ex)
        {
            lblModeFrom.Text = "";
            lblPageTitleFrom.Text = "";
            lblApplicationCodeFrom.Text = "";
        }

    }

    #region Private
    private void BindGridView(GridView gv, long lngA_ApplicationCode, ref Label AttCount)
    {
        NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(lngA_ApplicationCode);
        NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_SelfComplianceAttachment = null;

        if (gv.ID == "gvSiteInspection")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetCopySiteInspectionAttachmentList();
        else if (gv.ID == "gvgeophotostruct")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetGroundwaterAbstractionDataAttachmentList();
        else if (gv.ID == "gvGeoPhotowellfitted")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetWellFittedWithWaterFlowMeterAttachmentList();
        else if (gv.ID == "gvWQRSubmitted")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetGroundWaterQualityAttachmentList();
        else if (gv.ID == "gvRainwaterharvesting")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetGeoRainWaterHarvRechAttachmentList();

        else if (gv.ID == "gvGroundwaterMonitoring")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetGroundwaterMonitoringAttachmentList();
        else if (gv.ID == "gvSTPETP")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetPhotoETPSTPAttachmentList();
        else if (gv.ID == "gvGroundWaterMonitoringData")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetGroundWaterMonitoringDataAttachmentList();
        else if (gv.ID == "gvMineseepage")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetMiningSeepageAttachmentList();
        else if (gv.ID == "gvNOC")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetNOCAttachmentList();
        else if (gv.ID == "gvAnnualCalibration")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetAnnualCalibrationAttachmentList();




        gv.DataSource = arr_SelfComplianceAttachment;
        gv.DataBind();
        AttCount.Text = arr_SelfComplianceAttachment.Length.ToString();
    }
  
    private int UpdateData(long lngA_ApplicationCode, object sender, EventArgs e)
    {
        try
        {
            strActionName = "UpdateSelfCompliance";
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(lngA_ApplicationCode);

            if (lblMessageNOCCount.Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('NOC should be attached');", true);
                return 0;
            }


            if (ddlSiteInsp.SelectedValue.ToString() == "1")
            {
                if (lblSiteInspectionCount.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('SiteInspection should be attached');", true);
                    return 0;
                }

            }
            if (ddlgeophotostruct.SelectedValue.ToString() == "1")
            {
                if (lblgeophotostructCount.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Geotagged photograogh  of withdrawal structures should be attached');", true);
                    return 0;
                }

            }
            if (ddlGeoPhotoFittedWM.SelectedValue.ToString() == "1")
            {
                if (lblGeoPhotowellfittedCount.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Geotagged Photograph of well fitted with water meter attached should be attached');", true);
                    return 0;
                }
            }
            if (ddlGWQReport.SelectedValue.ToString() == "1")
            {
                if (lblWQRSubmittedCount.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Ground Water Quality should be attached');", true);
                    return 0;
                }

            }
            if (ddlGeoPhotoRechargeStruc.SelectedValue.ToString() == "1")
            {
                if (lblgvRainwaterharvestingCount.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Geotagged photograph  of recharge structures should be attached');", true);
                    return 0;
                }

            }
            if (ddlgeophotoSTPETP.SelectedValue.ToString() == "1")
            {
                if (lblSTPETPCount.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Geotagged photograph of SEWAGE TREATMENT PLANT/EFFLUENT TREATMENT PLANT should be attached');", true);
                    return 0;
                }

            }
            if (ddlGroundWaterMonitoringData.SelectedValue.ToString() == "1")
            {
                if (lblGroundWaterMonitoringDataCount.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Ground Water Monitoring Data should be attached');", true);
                    return 0;
                }

            }
            if (ddlMineSeepage.SelectedValue.ToString() == "1")
            {
                if (lblMineseepageCount.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Mine seepage quality report attached (in case of mining projects) should be attached');", true);
                    return 0;
                }

            }
            if (ddlAnnualCali.SelectedValue.ToString() == "1")
            {
                if (lblAnnualCalibrationCount.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Annual calibration of water meter by Govt agencies should be attached');", true);
                    return 0;
                }

            }
            if (ddlPresentwithdrawal.SelectedValue != "" && !NOCAPExternalUtility.IsNumeric(ddlPresentwithdrawal.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Allows only Numeric');", true);
                return 0;
            }

            if (ddlNameOfAgency.SelectedIndex > 0)
            {
                obj_SelfCompliance.InspectionAgencyCode = Convert.ToInt32(ddlNameOfAgency.SelectedValue.ToString());
                obj_SelfCompliance.AnyOtherAgency = txtOtherAgency.Text.Trim();
                if (!NOCAPExternalUtility.IsValidDate(txtDateOfInsp1.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Invalid Date');", true);
                    return 0;
                }
                else
                    obj_SelfCompliance.DateOfInspection = Convert.ToDateTime(txtDateOfInsp1.Text.Trim());

                switch (ddlSiteInsp.SelectedValue.ToString())
                {
                    case "1":
                        obj_SelfCompliance.InspectionReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                        break;
                    case "0":
                        obj_SelfCompliance.InspectionReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                        break;
                    case "":
                        obj_SelfCompliance.InspectionReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                        break;
                    default:
                        obj_SelfCompliance.InspectionReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                        break;
                }
            }
            switch (ddlPresentwithdrawal.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.PresentGroundWaterReq = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.PresentGroundWaterReq = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.PresentGroundWaterReq = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.PresentGroundWaterReq = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }

            //if (!NOCAPExternalUtility.IsValidDate(txtComplianceSubmitDt.Text.Trim()))
            //{
            //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Invalid Date');", true);
            //    return 0;
            //}
            //else
            //{
            //    obj_SelfCompliance.ComplianceSubmitDate = Convert.ToDateTime(txtComplianceSubmitDt.Text);
            //}            

            if (txtPresentwithdrawalInDay.Text.Trim() != "")

                obj_SelfCompliance.PresentGroundWaterReqInDay = Convert.ToDecimal(txtPresentwithdrawalInDay.Text.Trim());
            if (txtPresentwithdrawalInYear.Text.Trim() != "")

                obj_SelfCompliance.PresentGroundWaterReqInYear = Convert.ToDecimal(txtPresentwithdrawalInYear.Text.Trim());
            if (txtDewPresentwithdrawalInDay.Text.Trim() != "")
                obj_SelfCompliance.PreGroundWaterDewReqInDay = Convert.ToDecimal(txtDewPresentwithdrawalInDay.Text.Trim());
            if (txtDewPresentwithdrawalInYear.Text.Trim() != "")
                obj_SelfCompliance.PreGroundWaterDewReqInYear = Convert.ToDecimal(txtDewPresentwithdrawalInYear.Text.Trim());


            if (txtselfPresentwithdrawalInDay.Text.Trim() != "")
                obj_SelfCompliance.PreSelfsentGroundWaterReqInDay = Convert.ToDecimal(txtselfPresentwithdrawalInDay.Text.Trim());
            if (txtselfPresentwithdrawalInYear.Text.Trim() != "")
                obj_SelfCompliance.PreSelfsentGroundWaterReqInYear = Convert.ToDecimal(txtselfPresentwithdrawalInYear.Text.Trim());
            if (txtselfDewPresentwithdrawalInDay.Text.Trim() != "")
                obj_SelfCompliance.PreSelfGroundWaterDewReqInDay = Convert.ToDecimal(txtselfDewPresentwithdrawalInDay.Text.Trim());
            if (txtselfDewPresentwithdrawalInYear.Text.Trim() != "")
                obj_SelfCompliance.PreSelfGroundWaterDewReqInYear = Convert.ToDecimal(txtselfDewPresentwithdrawalInYear.Text.Trim());

            switch (ddlAnyVariation.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.PreGroundWaterAnyVari = NOCAP.BLL.Common.CommonEnum.VariationInQuantum.MoreThanPermitted;
                    break;
                case "2":
                    obj_SelfCompliance.PreGroundWaterAnyVari = NOCAP.BLL.Common.CommonEnum.VariationInQuantum.LessThanPermitted;
                    break;
                case "3":
                    obj_SelfCompliance.PreGroundWaterAnyVari = NOCAP.BLL.Common.CommonEnum.VariationInQuantum.NoVariation;
                    break;
                case "":
                    obj_SelfCompliance.PreGroundWaterAnyVari = NOCAP.BLL.Common.CommonEnum.VariationInQuantum.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.PreGroundWaterAnyVari = NOCAP.BLL.Common.CommonEnum.VariationInQuantum.NotDefined;
                    break;
            }
            if (txtQtyVariDay.Text.Trim() != "")
                obj_SelfCompliance.PreGroundWaterAnyVariReqInDay = Convert.ToDecimal(txtQtyVariDay.Text.Trim());
            else
                obj_SelfCompliance.PreGroundWaterAnyVariReqInDay = 0;
            if (txtQtyVariYear.Text.Trim() != "")
                obj_SelfCompliance.PreGroundWaterAnyVariReqInYear = Convert.ToDecimal(txtQtyVariYear.Text.Trim());
            else obj_SelfCompliance.PreGroundWaterAnyVariReqInYear = 0;
            switch (ddlAbstractionTW.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.AbstrDataSubmittedTW = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.AbstrDataSubmittedTW = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.AbstrDataSubmittedTW = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.AbstrDataSubmittedTW = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            if (txtAbstraStructExistingAsperNOC.Text.Trim() != "")
                obj_SelfCompliance.AbstraStructExistingAsPerNOC = Convert.ToInt32(txtAbstraStructExistingAsperNOC.Text.Trim());
            if (txtAbstraStructExisting.Text.Trim() != "")

                obj_SelfCompliance.AbstraStructExisting = Convert.ToInt32(txtAbstraStructExisting.Text.Trim());
            if (txtAbstraStructProposedAsperNOC.Text.Trim() != "")

                obj_SelfCompliance.AbstraStructProposedAsPerNOC = Convert.ToInt32(txtAbstraStructProposedAsperNOC.Text.Trim());
            if (txtAbstraStructProposed.Text.Trim() != "")

                obj_SelfCompliance.AbstraStructProposed = Convert.ToInt32(txtAbstraStructProposed.Text.Trim());
            if (txtNoOfFunt.Text.Trim() != "")

                obj_SelfCompliance.FuncAbstStruct = Convert.ToInt32(txtNoOfFunt.Text.Trim());
            switch (ddlgeophotostruct.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.StructPhoto = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.StructPhoto = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.StructPhoto = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.StructPhoto = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            switch (AbstStructWM.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.AbstStructFittedWithWM = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.AbstStructFittedWithWM = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.AbstStructFittedWithWM = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.AbstStructFittedWithWM = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            obj_SelfCompliance.MeterTypeCode = Convert.ToInt32(ddlMeterType.SelectedValue.ToString());
            //switch (ddlTeleInstalled.SelectedValue.ToString())
            //{
            //    case "1":
            //        obj_SelfCompliance.TelemInstalled = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
            //        break;
            //    case "0":
            //        obj_SelfCompliance.TelemInstalled = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
            //        break;
            //    case "":
            //        obj_SelfCompliance.TelemInstalled = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
            //        break;
            //    default:
            //        obj_SelfCompliance.TelemInstalled = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
            //        break;
            //}
            if (txtFunctMeter.Text.Trim() != "")
                obj_SelfCompliance.NumberOfFunMeter = Convert.ToInt32(txtFunctMeter.Text.Trim());

            switch (ddlAnnualCali.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.AnnuCalibOfWM = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.AnnuCalibOfWM = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.AnnuCalibOfWM = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.AnnuCalibOfWM = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            switch (ddlGeoPhotoFittedWM.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.PhotoWellFittedWithWM = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.PhotoWellFittedWithWM = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.PhotoWellFittedWithWM = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.PhotoWellFittedWithWM = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }


            switch (ddlGWQReport.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.GWQuality = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.GWQuality = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.GWQuality = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.GWQuality = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            switch (ddlMineSeepage.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.MineSeepageQuality = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.MineSeepageQuality = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.MineSeepageQuality = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.MineSeepageQuality = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            switch (ddlWaterSampple.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.WaterSampleAnalyzed = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.WaterSampleAnalyzed = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.WaterSampleAnalyzed = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.WaterSampleAnalyzed = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            switch (ddlWQRSubmitted.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.GWReportWithinTime = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.GWReportWithinTime = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.GWReportWithinTime = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.GWReportWithinTime = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            switch (ddlRainwaterharvesting.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.RainWaterHarv = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;

                    List<NOCAP.BLL.Master.SelfCompTypeOfARStructureDetail> list = new List<NOCAP.BLL.Master.SelfCompTypeOfARStructureDetail>();
                    for (int i = 0; i < chklistTypeOfARStruct.Items.Count; i++)
                    {
                        NOCAP.BLL.Master.SelfCompTypeOfARStructureDetail obj = null;

                        if (chklistTypeOfARStruct.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            obj = new NOCAP.BLL.Master.SelfCompTypeOfARStructureDetail();
                            obj.TypeOfARStructCode = Convert.ToInt32(chklistTypeOfARStruct.Items[i].Value);
                            list.Add(obj);
                        }
                    }
                    NOCAP.BLL.Master.SelfCompTypeOfARStructureDetail[] arr = new NOCAP.BLL.Master.SelfCompTypeOfARStructureDetail[list.Count];
                    list.CopyTo(arr);
                    obj_SelfCompliance.SelfCompTypeOfARStructureDetailCollection = arr;
                    break;
                case "0":
                    obj_SelfCompliance.RainWaterHarv = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    obj_SelfCompliance.SelfCompTypeOfARStructureDetailCollection = null;
                    break;
                case "":
                    obj_SelfCompliance.RainWaterHarv = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    obj_SelfCompliance.SelfCompTypeOfARStructureDetailCollection = null;
                    break;
                default:
                    obj_SelfCompliance.RainWaterHarv = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    obj_SelfCompliance.SelfCompTypeOfARStructureDetailCollection = null;
                    break;
            }

            if (txtNoOfStruct.Text.Trim() != "")
                obj_SelfCompliance.NumberOfStruct = Convert.ToInt32(txtNoOfStruct.Text.Trim());

            switch (ddlWithinOutSidePremises.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.WithOutPremises = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.WithOutPremises = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.WithOutPremises = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.WithOutPremises = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            if (txtQuantOfRecharge.Text.Trim() != "")
                obj_SelfCompliance.QuantOfRecharge = Convert.ToDecimal(txtQuantOfRecharge.Text.Trim());

            switch (ddlGeoPhotoRechargeStruc.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.PhotoRechargeStruct = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.PhotoRechargeStruct = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.PhotoRechargeStruct = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.PhotoRechargeStruct = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            if (txtNOPiezo.Text.Trim() != "")
                obj_SelfCompliance.NoOfPiezo = Convert.ToInt32(txtNOPiezo.Text.Trim());

            if (txtNOPiezoDWLR.Text.Trim() != "")
                obj_SelfCompliance.NoOfPiezoDWLR = Convert.ToInt32(txtNOPiezoDWLR.Text.Trim());

            //if (txtNOPiezoTele.Text.Trim() != "")
            //    obj_SelfCompliance.NoOfPiezoTelem = Convert.ToInt32(txtNOPiezoTele.Text.Trim());
            if (txtpieDWLRTelem.Text.Trim() != "")
                obj_SelfCompliance.NoOfPiezoDWLRTelem = Convert.ToInt32(txtpieDWLRTelem.Text.Trim());

            if (txtselfNOPiezo.Text.Trim() != "")
                obj_SelfCompliance.NoOfSelfPiezo = Convert.ToInt32(txtselfNOPiezo.Text.Trim());

            if (txtselfNOPiezoDWLR.Text.Trim() != "")
                obj_SelfCompliance.NoOfSelfPiezoDWLR = Convert.ToInt32(txtselfNOPiezoDWLR.Text.Trim());

            //if (txtselfNOPiezoTele.Text.Trim() != "")
            //    obj_SelfCompliance.NoOfSelfPiezoTelem = Convert.ToInt32(txtselfNOPiezoTele.Text.Trim());
            if (txtselfpieDWLRTelem.Text.Trim() != "")
                obj_SelfCompliance.NoOfSelfPiezoDWLRTelem = Convert.ToInt32(txtselfpieDWLRTelem.Text.Trim());

            if (txtNoOversWell.Text.Trim() != "")
                obj_SelfCompliance.NoOfObserwell = Convert.ToInt32(txtNoOversWell.Text.Trim());

            if (NoFunctionalPiewell.Text.Trim() != "")
                obj_SelfCompliance.NoOfFunctPiezo = Convert.ToInt32(NoFunctionalPiewell.Text.Trim());

            switch (ddlgeophotopiewell.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.GeoPiezoAWLRTelem = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.GeoPiezoAWLRTelem = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.GeoPiezoAWLRTelem = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.GeoPiezoAWLRTelem = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            switch (ddlMoniDataSubmitted.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.MoniDataSubmitted = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.MoniDataSubmitted = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.MoniDataSubmitted = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.MoniDataSubmitted = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            //switch (ddlpieDWLRTelem.SelectedValue.ToString())
            //{
            //    case "1":
            //        obj_SelfCompliance.PiezometerDWLRTelemetry = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
            //        break;
            //    case "0":
            //        obj_SelfCompliance.PiezometerDWLRTelemetry = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
            //        break;
            //    case "":
            //        obj_SelfCompliance.PiezometerDWLRTelemetry = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
            //        break;
            //    default:
            //        obj_SelfCompliance.PiezometerDWLRTelemetry = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
            //        break;
            //}
            switch (ddlGroundWaterMonitoringData.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.GroundWaterMonitoringData = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.GroundWaterMonitoringData = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.GroundWaterMonitoringData = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.GroundWaterMonitoringData = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }

            switch (ddlSTPETP.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.STPETP = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.STPETP = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.STPETP = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.STPETP = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            if (txtNoSTPETP.Text.Trim() != "")
                obj_SelfCompliance.NoOfSTPETP = Convert.ToInt32(txtNoSTPETP.Text.Trim());
            if (txtCapSTPETP.Text.Trim() != "")
                obj_SelfCompliance.CapOfSTPETP = Convert.ToDecimal(txtCapSTPETP.Text.Trim());
            if (txtQuantumTWW.Text.Trim() != "")
                obj_SelfCompliance.QuanttreatWasteWater = Convert.ToDecimal(txtQuantumTWW.Text.Trim());
            switch (ddlgeophotoSTPETP.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.GeoPhotoOfSTP = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.GeoPhotoOfSTP = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.GeoPhotoOfSTP = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.GeoPhotoOfSTP = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            if (txtIndProcess.Text.Trim() != "")
                obj_SelfCompliance.QuanttreatWasteWaterIND = Convert.ToDecimal(txtIndProcess.Text.Trim());
            if (txtGreenbelt.Text.Trim() != "")
                obj_SelfCompliance.QuanttreatWasteWaterINDGreen = Convert.ToDecimal(txtGreenbelt.Text.Trim());
            if (txtOtherUse.Text.Trim() != "")
                obj_SelfCompliance.QuanttreatWasteWaterINDOther = Convert.ToDecimal(txtOtherUse.Text.Trim());

            obj_SelfCompliance.ComplianceSubmitDate = DateTime.Now;
            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_SelfCompliance.ModifiedByExUC = obj_externalUser.ExternalUserCode;
            if (obj_SelfCompliance.Update() == 1)
            {
                strStatus = "Update Success";
                lblMessage.Text = obj_SelfCompliance.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                strStatus = "Update Failed";
                lblMessage.Text = obj_SelfCompliance.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            return 0;
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

    private void ValidationExepInit()
    {
        //revtxtPresentwithdrawalInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        //revtxtPresentwithdrawalInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        //revtxtPresentwithdrawalInYear.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        //revtxtPresentwithdrawalInYear.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        revtxtselfPresentwithdrawalInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtselfPresentwithdrawalInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtselfDewPresentwithdrawalInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtselfDewPresentwithdrawalInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");


        //revtxtDewPresentwithdrawalInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        //revtxtDewPresentwithdrawalInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        //revtxtDewPresentwithdrawalInYear.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        //revtxtDewPresentwithdrawalInYear.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");
        revtxtselfPresentwithdrawalInYear.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtselfPresentwithdrawalInYear.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");
        revtxtselfDewPresentwithdrawalInYear.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtselfDewPresentwithdrawalInYear.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");






        revtxtQuantOfRecharge.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtQuantOfRecharge.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");
        revtxtQuantumTWW.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtQuantumTWW.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");
        revtxtCapSTPETP.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtCapSTPETP.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        revtxtIndProcess.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtIndProcess.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");
        revtxtGreenbelt.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtGreenbelt.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        revtxtOtherUse.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtOtherUse.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");



        revtxtQtyVariDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtQtyVariDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtQtyVariYear.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtQtyVariYear.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        revtxtAbstraStructExistingAsperNOC.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtAbstraStructExistingAsperNOC.ErrorMessage = ValidationUtility.txtValForNumericMsg;
        revtxtAbstraStructExisting.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtAbstraStructExisting.ErrorMessage = ValidationUtility.txtValForNumericMsg;
        revtxtAbstraStructProposedAsperNOC.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtAbstraStructProposedAsperNOC.ErrorMessage = ValidationUtility.txtValForNumericMsg;
        revtxtAbstraStructProposed.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtAbstraStructProposed.ErrorMessage = ValidationUtility.txtValForNumericMsg;
        revtxtNoOfFunt.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtNoOfFunt.ErrorMessage = ValidationUtility.txtValForNumericMsg;
        revtxtFunctMeter.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtFunctMeter.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtNoOfStruct.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtNoOfStruct.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtNOPiezo.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtNOPiezo.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtNOPiezoDWLR.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtNOPiezoDWLR.ErrorMessage = ValidationUtility.txtValForNumericMsg;


        //revtxtNOPiezoTele.ValidationExpression = ValidationUtility.txtValForNumeric;
        //revtxtNOPiezoTele.ErrorMessage = ValidationUtility.txtValForNumericMsg;


        revtxtpieDWLRTelem.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtpieDWLRTelem.ErrorMessage = ValidationUtility.txtValForNumericMsg;



        revtxtselfNOPiezo.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtselfNOPiezo.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtselfNOPiezoDWLR.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtselfNOPiezoDWLR.ErrorMessage = ValidationUtility.txtValForNumericMsg;


        //revtxtselfNOPiezoTele.ValidationExpression = ValidationUtility.txtValForNumeric;
        //revtxtselfNOPiezoTele.ErrorMessage = ValidationUtility.txtValForNumericMsg;


        revtxtselfpieDWLRTelem.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtselfpieDWLRTelem.ErrorMessage = ValidationUtility.txtValForNumericMsg;


        revtxtNoOversWell.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtNoOversWell.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revNoFunctionalPiewell.ValidationExpression = ValidationUtility.txtValForNumeric;
        revNoFunctionalPiewell.ErrorMessage = ValidationUtility.txtValForNumericMsg;
        revtxtNoSTPETP.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtNoSTPETP.ErrorMessage = ValidationUtility.txtValForNumericMsg;















        revtxtOtherAgency.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtOtherAgency.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;
    }
    private void PopulateData(long lngA_ApplicationCode, object sender, EventArgs e)
    {
        try
        {
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(lngA_ApplicationCode);
            if (obj_selfCompliance.ApplicationCode != 0)
            {
                if (obj_selfCompliance.InspectionAgencyCode > 0 && obj_selfCompliance.InspectionAgencyCode != null)
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
                NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_IndustrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter(obj_selfCompliance.ApplicationCode);
                NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter obj_IndustrialRenewIssusedLetter = new NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter(obj_selfCompliance.ApplicationCode);
                NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_InfrastructureNewIssusedLetter = new NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter(obj_selfCompliance.ApplicationCode);
                NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter obj_InfrastructureRenewIssusedLetter = new NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter(obj_selfCompliance.ApplicationCode);
                NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter obj_MiningNewIssusedLetter = new NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter(obj_selfCompliance.ApplicationCode);
                NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter obj_MiningRenewIssusedLetter = new NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter(obj_selfCompliance.ApplicationCode);

                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_selfCompliance.ApplicationCode);
                NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(obj_selfCompliance.ApplicationCode);
                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_selfCompliance.ApplicationCode);
                NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(obj_selfCompliance.ApplicationCode);
                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_selfCompliance.ApplicationCode);
                NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(obj_selfCompliance.ApplicationCode);
                if (obj_IndustrialNewIssusedLetter != null && obj_IndustrialNewIssusedLetter.INDAppNo != "" && obj_IndustrialNewIssusedLetter.INDAppNo != null)
                {
                    txtPresentwithdrawalInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialNewIssusedLetter.GroundWaterAbsRecommToApprPerDay));
                    txtPresentwithdrawalInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialNewIssusedLetter.GroundWaterAbsRecommToApprPerYear));
                    txtDewPresentwithdrawalInDay.Text = "0";
                    txtDewPresentwithdrawalInYear.Text = "0";

                    NOCAP.BLL.Industrial.New.NOCProforma.IndustrialNewNOCProformaExistGWAS obj_industrialNewNOCProformaExistGWAS = new NOCAP.BLL.Industrial.New.NOCProforma.IndustrialNewNOCProformaExistGWAS();
                    obj_industrialNewNOCProformaExistGWAS.GetAll();
                    NOCAP.BLL.Industrial.New.NOCProforma.IndustrialNewNOCProformaExistGWAS[] arr_IndustrialNewNOCProformaExistGWAS = obj_industrialNewNOCProformaExistGWAS.IndustrialNewNOCProformaExistGWASCollection;
                    if (arr_IndustrialNewNOCProformaExistGWAS.Length > 0)
                        txtAbstraStructExistingAsperNOC.Text = HttpUtility.HtmlEncode(arr_IndustrialNewNOCProformaExistGWAS.Where(x => x.ApplicationCode == obj_IndustrialNewIssusedLetter.INDAppCode).Where(x => x.SerialNumber == obj_IndustrialNewApplication.NOCProformaSN).Select(x => x.TypeOfAbstStructNo).Sum());
                    else
                        txtAbstraStructExistingAsperNOC.Text = "0";


                    NOCAP.BLL.Industrial.New.NOCProforma.IndustrialNewNOCProformaProGWAS obj_industrialNewNOCProformaProGWAS = new NOCAP.BLL.Industrial.New.NOCProforma.IndustrialNewNOCProformaProGWAS();
                    obj_industrialNewNOCProformaProGWAS.GetAll();
                    NOCAP.BLL.Industrial.New.NOCProforma.IndustrialNewNOCProformaProGWAS[] arr_IndustrialNewNOCProformaProGWAS = obj_industrialNewNOCProformaProGWAS.IndustrialNewNOCProformaProGWASCollection;
                    if (arr_IndustrialNewNOCProformaProGWAS.Length > 0)
                        txtAbstraStructProposedAsperNOC.Text = HttpUtility.HtmlEncode(arr_IndustrialNewNOCProformaProGWAS.Where(x => x.ApplicationCode == obj_IndustrialNewIssusedLetter.INDAppCode).Where(x => x.SerialNumber == obj_IndustrialNewApplication.NOCProformaSN).Select(x => x.TypeOfAbstStructNo).Sum());
                    else
                        txtAbstraStructProposedAsperNOC.Text = "0";
                }
                else if (obj_IndustrialRenewIssusedLetter != null && obj_IndustrialRenewIssusedLetter.INDAppNo != "" && obj_IndustrialRenewIssusedLetter.INDAppNo != null)
                {
                    txtPresentwithdrawalInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialRenewIssusedLetter.GroundWaterAbsRecommToApprPerDay));
                    txtPresentwithdrawalInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialRenewIssusedLetter.GroundWaterAbsRecommToApprPerYear));
                    txtDewPresentwithdrawalInDay.Text = "0";
                    txtDewPresentwithdrawalInYear.Text = "0";

                    NOCAP.BLL.Industrial.Renew.NOCProforma.IndustrialRenewNOCProformaExistGWAS obj_industrialRenewNOCProformaExistGWAS = new NOCAP.BLL.Industrial.Renew.NOCProforma.IndustrialRenewNOCProformaExistGWAS();
                    obj_industrialRenewNOCProformaExistGWAS.GetAll();
                    NOCAP.BLL.Industrial.Renew.NOCProforma.IndustrialRenewNOCProformaExistGWAS[] arr_IndustrialRenewNOCProformaExistGWAS = obj_industrialRenewNOCProformaExistGWAS.IndustrialRenewNOCProformaExistGWASCollection;
                    if (arr_IndustrialRenewNOCProformaExistGWAS.Length > 0)
                        txtAbstraStructExistingAsperNOC.Text = HttpUtility.HtmlEncode(arr_IndustrialRenewNOCProformaExistGWAS.Where(x => x.ApplicationCode == obj_IndustrialRenewIssusedLetter.RenewApplicationCode).Where(x => x.SerialNumber == obj_IndustrialRenewApplication.NOCProformaSN).Select(x => x.TypeOfAbstStructNo).Sum());
                    else
                        txtAbstraStructExistingAsperNOC.Text = "0";


                    NOCAP.BLL.Industrial.Renew.NOCProforma.IndustrialRenewNOCProformaProGWAS obj_industrialRenewNOCProformaProGWAS = new NOCAP.BLL.Industrial.Renew.NOCProforma.IndustrialRenewNOCProformaProGWAS();
                    obj_industrialRenewNOCProformaProGWAS.GetAll();
                    NOCAP.BLL.Industrial.Renew.NOCProforma.IndustrialRenewNOCProformaProGWAS[] arr_IndustrialRenewNOCProformaProGWAS = obj_industrialRenewNOCProformaProGWAS.IndustrialRenewNOCProformaProGWASCollection;
                    if (arr_IndustrialRenewNOCProformaProGWAS.Length > 0)
                        txtAbstraStructProposedAsperNOC.Text = HttpUtility.HtmlEncode(arr_IndustrialRenewNOCProformaProGWAS.Where(x => x.ApplicationCode == obj_IndustrialRenewIssusedLetter.RenewApplicationCode).Where(x => x.SerialNumber == obj_IndustrialRenewApplication.NOCProformaSN).Select(x => x.TypeOfAbstStructNo).Sum());
                    else
                        txtAbstraStructProposedAsperNOC.Text = "0";

                }
                else if (obj_InfrastructureNewIssusedLetter != null && obj_InfrastructureNewIssusedLetter.INFAppNo != "" && obj_InfrastructureNewIssusedLetter.INFAppNo != null)
                {
                    txtPresentwithdrawalInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_InfrastructureNewIssusedLetter.GroundWaterAbsRecommToApprPerDay));
                    txtPresentwithdrawalInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_InfrastructureNewIssusedLetter.GroundWaterAbsRecommToApprPerYear));
                    txtDewPresentwithdrawalInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_InfrastructureNewIssusedLetter.GroundWaterAbsRecommToApprDewaterPerDay));
                    txtDewPresentwithdrawalInYear.Text = "0";
                    NOCAP.BLL.Infrastructure.New.NOCProforma.InfrastructureNewNOCProformaExistGWAS obj_InfrastructureNewNOCProformaExistGWAS = new NOCAP.BLL.Infrastructure.New.NOCProforma.InfrastructureNewNOCProformaExistGWAS();
                    obj_InfrastructureNewNOCProformaExistGWAS.GetAll();
                    NOCAP.BLL.Infrastructure.New.NOCProforma.InfrastructureNewNOCProformaExistGWAS[] arr_InfrastructureNewNOCProformaExistGWAS = obj_InfrastructureNewNOCProformaExistGWAS.InfrastructureNewNOCProformaExistGWASCollection;
                    if (arr_InfrastructureNewNOCProformaExistGWAS.Length > 0)
                        txtAbstraStructExistingAsperNOC.Text = HttpUtility.HtmlEncode(arr_InfrastructureNewNOCProformaExistGWAS.Where(x => x.ApplicationCode == obj_InfrastructureNewIssusedLetter.INFAppCode).Where(x => x.SerialNumber == obj_InfrastructureNewApplication.NOCProformaSN).Select(x => x.TypeOfAbstStructNo).Sum());
                    else
                        txtAbstraStructExistingAsperNOC.Text = "0";


                    NOCAP.BLL.Infrastructure.New.NOCProforma.InfrastructureNewNOCProformaProGWAS obj_InfrastructureNewNOCProformaProGWAS = new NOCAP.BLL.Infrastructure.New.NOCProforma.InfrastructureNewNOCProformaProGWAS();
                    obj_InfrastructureNewNOCProformaProGWAS.GetAll();
                    NOCAP.BLL.Infrastructure.New.NOCProforma.InfrastructureNewNOCProformaProGWAS[] arr_InfrastructureNewNOCProformaProGWAS = obj_InfrastructureNewNOCProformaProGWAS.InfrastructureNewNOCProformaProGWASCollection;
                    if (arr_InfrastructureNewNOCProformaProGWAS.Length > 0)
                        txtAbstraStructProposedAsperNOC.Text = HttpUtility.HtmlEncode(arr_InfrastructureNewNOCProformaProGWAS.Where(x => x.ApplicationCode == obj_InfrastructureNewIssusedLetter.INFAppCode).Where(x => x.SerialNumber == obj_InfrastructureNewApplication.NOCProformaSN).Select(x => x.TypeOfAbstStructNo).Sum());
                    else
                        txtAbstraStructProposedAsperNOC.Text = "0";
                }
                else if (obj_InfrastructureRenewIssusedLetter != null && obj_InfrastructureRenewIssusedLetter.INFAppNo != "" && obj_InfrastructureRenewIssusedLetter.INFAppNo != null)
                {
                    txtPresentwithdrawalInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_InfrastructureRenewIssusedLetter.GroundWaterAbsRecommToApprPerDay));
                    txtPresentwithdrawalInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_InfrastructureRenewIssusedLetter.GroundWaterAbsRecommToApprPerYear));
                    txtDewPresentwithdrawalInDay.Text = "0";
                    txtDewPresentwithdrawalInYear.Text = "0";
                    NOCAP.BLL.Infrastructure.Renew.NOCProforma.InfrastructureRenewNOCProformaExistGWAS obj_InfrastructureRenewNOCProformaExistGWAS = new NOCAP.BLL.Infrastructure.Renew.NOCProforma.InfrastructureRenewNOCProformaExistGWAS();
                    obj_InfrastructureRenewNOCProformaExistGWAS.GetAll();
                    NOCAP.BLL.Infrastructure.Renew.NOCProforma.InfrastructureRenewNOCProformaExistGWAS[] arr_InfrastructureRenewNOCProformaExistGWAS = obj_InfrastructureRenewNOCProformaExistGWAS.InfrastructureRenewNOCProformaExistGWASCollection;
                    if (arr_InfrastructureRenewNOCProformaExistGWAS.Length > 0)
                        txtAbstraStructExistingAsperNOC.Text = HttpUtility.HtmlEncode(arr_InfrastructureRenewNOCProformaExistGWAS.Where(x => x.ApplicationCode == obj_InfrastructureRenewIssusedLetter.RenewApplicationCode).Where(x => x.SerialNumber == obj_InfrastructureRenewApplication.NOCProformaSN).Select(x => x.TypeOfAbstStructNo).Sum());
                    else
                        txtAbstraStructExistingAsperNOC.Text = "0";


                    NOCAP.BLL.Infrastructure.Renew.NOCProforma.InfrastructureRenewNOCProformaProGWAS obj_InfrastructureRenewNOCProformaProGWAS = new NOCAP.BLL.Infrastructure.Renew.NOCProforma.InfrastructureRenewNOCProformaProGWAS();
                    obj_InfrastructureRenewNOCProformaProGWAS.GetAll();
                    NOCAP.BLL.Infrastructure.Renew.NOCProforma.InfrastructureRenewNOCProformaProGWAS[] arr_InfrastructureRenewNOCProformaProGWAS = obj_InfrastructureRenewNOCProformaProGWAS.InfrastructureRenewNOCProformaProGWASCollection;
                    if (arr_InfrastructureRenewNOCProformaProGWAS.Length > 0)
                        txtAbstraStructProposedAsperNOC.Text = HttpUtility.HtmlEncode(arr_InfrastructureRenewNOCProformaProGWAS.Where(x => x.ApplicationCode == obj_InfrastructureRenewIssusedLetter.RenewApplicationCode).Where(x => x.SerialNumber == obj_InfrastructureRenewApplication.NOCProformaSN).Select(x => x.TypeOfAbstStructNo).Sum());
                    else
                        txtAbstraStructProposedAsperNOC.Text = "0";
                }
                else if (obj_MiningNewIssusedLetter != null && obj_MiningNewIssusedLetter.MINAppNo != "" && obj_MiningNewIssusedLetter.MINAppNo != null)
                {
                    txtPresentwithdrawalInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewIssusedLetter.GroundWaterAbsRecommToApprPerDay));
                    txtPresentwithdrawalInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewIssusedLetter.GroundWaterAbsRecommToApprPerYear));
                    txtDewPresentwithdrawalInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewIssusedLetter.GroundWaterAbsRecommToApprSeepDewaterPerDay));
                    txtDewPresentwithdrawalInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewIssusedLetter.GroundWaterSeepDewYearAppr));
                    NOCAP.BLL.Mining.New.NOCProforma.MiningNewNOCProformaExistGWAS obj_MiningNewNOCProformaExistGWAS = new NOCAP.BLL.Mining.New.NOCProforma.MiningNewNOCProformaExistGWAS();
                    obj_MiningNewNOCProformaExistGWAS.GetAll();
                    NOCAP.BLL.Mining.New.NOCProforma.MiningNewNOCProformaExistGWAS[] arr_MiningNewNOCProformaExistGWAS = obj_MiningNewNOCProformaExistGWAS.MiningNewNOCProformaExistGWASCollection;
                    if (arr_MiningNewNOCProformaExistGWAS.Length > 0)
                        txtAbstraStructExistingAsperNOC.Text = HttpUtility.HtmlEncode(arr_MiningNewNOCProformaExistGWAS.Where(x => x.ApplicationCode == obj_MiningNewIssusedLetter.MINAppCode).Where(x => x.SerialNumber == obj_MiningNewApplication.NOCProformaSN).Select(x => x.TypeOfAbstStructNo).Sum());
                    else
                        txtAbstraStructExistingAsperNOC.Text = "0";


                    NOCAP.BLL.Mining.New.NOCProforma.MiningNewNOCProformaProGWAS obj_MiningNewNOCProformaProGWAS = new NOCAP.BLL.Mining.New.NOCProforma.MiningNewNOCProformaProGWAS();
                    obj_MiningNewNOCProformaProGWAS.GetAll();
                    NOCAP.BLL.Mining.New.NOCProforma.MiningNewNOCProformaProGWAS[] arr_MiningNewNOCProformaProGWAS = obj_MiningNewNOCProformaProGWAS.MiningNewNOCProformaProGWASCollection;
                    if (arr_MiningNewNOCProformaProGWAS.Length > 0)
                        txtAbstraStructProposedAsperNOC.Text = HttpUtility.HtmlEncode(arr_MiningNewNOCProformaProGWAS.Where(x => x.ApplicationCode == obj_MiningNewIssusedLetter.MINAppCode).Where(x => x.SerialNumber == obj_MiningNewApplication.NOCProformaSN).Select(x => x.TypeOfAbstStructNo).Sum());
                    else
                        txtAbstraStructProposedAsperNOC.Text = "0";
                }
                else if (obj_MiningRenewIssusedLetter != null && obj_MiningRenewIssusedLetter.MINAppNo != "" && obj_MiningRenewIssusedLetter.MINAppNo != null)
                {
                    txtPresentwithdrawalInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewIssusedLetter.GroundWaterAbsRecommToApprPerDay));
                    txtPresentwithdrawalInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewIssusedLetter.GroundWaterAbsRecommToApprPerYear));
                    txtDewPresentwithdrawalInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewIssusedLetter.GroundWaterAbsRecommToApprSeepDewaterPerDay));
                    txtDewPresentwithdrawalInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewIssusedLetter.GroundWaterSeepDewYearAppr));
                    NOCAP.BLL.Mining.Renew.NOCProforma.MiningRenewNOCProformaExistGWAS obj_MiningRenewNOCProformaExistGWAS = new NOCAP.BLL.Mining.Renew.NOCProforma.MiningRenewNOCProformaExistGWAS();
                    obj_MiningRenewNOCProformaExistGWAS.GetAll();
                    NOCAP.BLL.Mining.Renew.NOCProforma.MiningRenewNOCProformaExistGWAS[] arr_MiningRenewNOCProformaExistGWAS = obj_MiningRenewNOCProformaExistGWAS.MiningRenewNOCProformaExistGWASCollection;
                    if (arr_MiningRenewNOCProformaExistGWAS.Length > 0)
                        txtAbstraStructExistingAsperNOC.Text = HttpUtility.HtmlEncode(arr_MiningRenewNOCProformaExistGWAS.Where(x => x.ApplicationCode == obj_MiningRenewIssusedLetter.RenewApplicationCode).Where(x => x.SerialNumber == obj_MiningRenewApplication.NOCProformaSN).Select(x => x.TypeOfAbstStructNo).Sum());
                    else
                        txtAbstraStructExistingAsperNOC.Text = "0";


                    NOCAP.BLL.Mining.Renew.NOCProforma.MiningRenewNOCProformaProGWAS obj_MiningRenewNOCProformaProGWAS = new NOCAP.BLL.Mining.Renew.NOCProforma.MiningRenewNOCProformaProGWAS();
                    obj_MiningRenewNOCProformaProGWAS.GetAll();
                    NOCAP.BLL.Mining.Renew.NOCProforma.MiningRenewNOCProformaProGWAS[] arr_MiningRenewNOCProformaProGWAS = obj_MiningRenewNOCProformaProGWAS.MiningRenewNOCProformaProGWASCollection;
                    if (arr_MiningRenewNOCProformaProGWAS.Length > 0)
                        txtAbstraStructProposedAsperNOC.Text = HttpUtility.HtmlEncode(arr_MiningRenewNOCProformaProGWAS.Where(x => x.ApplicationCode == obj_MiningRenewIssusedLetter.RenewApplicationCode).Where(x => x.SerialNumber == obj_MiningRenewApplication.NOCProformaSN).Select(x => x.TypeOfAbstStructNo).Sum());
                    else
                        txtAbstraStructProposedAsperNOC.Text = "0";
                }


                txtselfPresentwithdrawalInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.PreSelfsentGroundWaterReqInDay));
                txtselfPresentwithdrawalInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.PreSelfsentGroundWaterReqInYear));
                txtselfDewPresentwithdrawalInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.PreSelfGroundWaterDewReqInDay));
                txtselfDewPresentwithdrawalInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.PreSelfGroundWaterDewReqInYear));


                switch (obj_selfCompliance.PreGroundWaterAnyVari)
                {
                    case NOCAP.BLL.Common.CommonEnum.VariationInQuantum.MoreThanPermitted:
                        ddlAnyVariation.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.VariationInQuantum.LessThanPermitted:
                        ddlAnyVariation.SelectedValue = "2";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.VariationInQuantum.NoVariation:
                        txtQtyVariDay.Enabled = false;
                        txtQtyVariYear.Enabled = false;
                        ddlAnyVariation.SelectedValue = "3";
                        break;

                    case NOCAP.BLL.Common.CommonEnum.VariationInQuantum.NotDefined:
                        ddlAnyVariation.SelectedValue = "";
                        txtQtyVariDay.Enabled = false;
                        txtQtyVariYear.Enabled = false;
                        break;
                    default:
                        ddlAnyVariation.SelectedValue = "";
                        txtQtyVariDay.Enabled = false;
                        txtQtyVariYear.Enabled = false;
                        break;

                }
                txtQtyVariDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.PreGroundWaterAnyVariReqInDay));
                txtQtyVariYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.PreGroundWaterAnyVariReqInYear));

                switch (obj_selfCompliance.AbstrDataSubmittedTW)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlAbstractionTW.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlAbstractionTW.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlAbstractionTW.SelectedValue = "";
                        break;
                    default:
                        ddlAbstractionTW.SelectedValue = "";
                        break;

                }





                txtAbstraStructExisting.Text = HttpUtility.HtmlEncode(obj_selfCompliance.AbstraStructExisting);

                txtAbstraStructProposed.Text = HttpUtility.HtmlEncode(obj_selfCompliance.AbstraStructProposed);
                txtNoOfFunt.Text = HttpUtility.HtmlEncode(obj_selfCompliance.FuncAbstStruct);


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

                //switch (obj_selfCompliance.TelemInstalled)
                //{
                //    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                //        ddlTeleInstalled.SelectedValue = "1";
                //        break;
                //    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                //        ddlTeleInstalled.SelectedValue = "0";
                //        break;
                //    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                //        ddlTeleInstalled.SelectedValue = "";
                //        break;
                //    default:
                //        ddlTeleInstalled.SelectedValue = "";
                //        break;
                //}
                txtFunctMeter.Text = HttpUtility.HtmlEncode(obj_selfCompliance.NumberOfFunMeter);

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
                switch (obj_selfCompliance.GWReportWithinTime)
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
                // ddltypestr.SelectedValue = HttpUtility.HtmlEncode(obj_selfCompliance.TypeOfAbstStructCode);
                for (int j = 0; j < chklistTypeOfARStruct.Items.Count; j++)
                {
                    chklistTypeOfARStruct.Items[j].Selected = false;
                }

                NOCAP.BLL.Master.SelfCompTypeOfARStructureDetail obj_SelfCompTypeOfARStructureDetail = new NOCAP.BLL.Master.SelfCompTypeOfARStructureDetail();
                obj_SelfCompTypeOfARStructureDetail.ApplicationCode = obj_selfCompliance.ApplicationCode;
                obj_SelfCompTypeOfARStructureDetail.GetAll();


                NOCAP.BLL.Master.SelfCompTypeOfARStructureDetail[] arr_SelfCompTypeOfARStructureDetail = obj_SelfCompTypeOfARStructureDetail.SelfCompTypeOfARStructureDetailCollection;

                if (arr_SelfCompTypeOfARStructureDetail != null)
                {
                    if (arr_SelfCompTypeOfARStructureDetail.Length > 0)
                    {
                        for (int i = 0; i < arr_SelfCompTypeOfARStructureDetail.Length; i++)
                        {
                            for (int j = 0; j < chklistTypeOfARStruct.Items.Count; j++)
                            {
                                if (Convert.ToInt32(chklistTypeOfARStruct.Items[j].Value) == arr_SelfCompTypeOfARStructureDetail[i].TypeOfARStructCode)

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


                //NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_IndustrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter(Convert.ToInt64(lblApplicationCodeFrom.Text.Trim()));
                //NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter obj_IndustrialRenewIssusedLetter = new NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter(Convert.ToInt64(lblApplicationCodeFrom.Text.Trim()));
                //NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_InfrastructureNewIssusedLetter = new NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter(Convert.ToInt64(lblApplicationCodeFrom.Text.Trim()));
                //NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter obj_InfrastructureRenewIssusedLetter = new NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter(Convert.ToInt64(lblApplicationCodeFrom.Text.Trim()));
                //NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter obj_MiningNewIssusedLetter = new NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter(Convert.ToInt64(lblApplicationCodeFrom.Text.Trim()));
                //NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter obj_MiningRenewIssusedLetter = new NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter(Convert.ToInt64(lblApplicationCodeFrom.Text.Trim()));

                NOCAP.BLL.Master.MonitoringMech obj_MonitoringMech = new NOCAP.BLL.Master.MonitoringMech();
                obj_MonitoringMech.GetAllMM();

                NOCAP.BLL.Master.MonitoringMech[] arr = obj_MonitoringMech.MonitoringMechCollection;
                if (obj_IndustrialNewIssusedLetter != null)
                {
                    if (obj_IndustrialNewIssusedLetter.GeneralConditionContent == "")
                    {
                        txtNOPiezo.Enabled = true;
                        txtNOPiezoDWLR.Enabled = true;
                        //txtNOPiezoTele.Enabled = true;
                        txtpieDWLRTelem.Enabled = true;
                        txtNOPiezo.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezo));
                        txtNOPiezoDWLR.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoDWLR));
                        // txtNOPiezoTele.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoTelem));
                        txtpieDWLRTelem.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoDWLRTelem));
                    }
                    else
                    {
                        arr = arr.Where(a => a.QuantumGWwithdraMoreThan < obj_IndustrialNewIssusedLetter.GroundWaterAbsRecommToApprPerDay && a.QuantumGWwithdraUpto >= obj_IndustrialNewIssusedLetter.GroundWaterAbsRecommToApprPerDay).ToArray();
                        if (arr != null && arr.Length > 0)
                        {
                            txtNOPiezo.Enabled = false;
                            txtNOPiezoDWLR.Enabled = false;
                            //txtNOPiezoTele.Enabled = false;
                            txtpieDWLRTelem.Enabled = false;
                            txtNOPiezo.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfManual));
                            txtNOPiezoDWLR.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfDWLR));
                            // txtNOPiezoTele.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfManual));
                            txtpieDWLRTelem.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfDWLRwithTelemetry));
                        }
                    }
                }
                else if (obj_IndustrialRenewIssusedLetter != null)
                {
                    if (obj_IndustrialRenewIssusedLetter.GeneralConditionContent == "")
                    {
                        txtNOPiezo.Enabled = true;
                        txtNOPiezoDWLR.Enabled = true;
                        //txtNOPiezoTele.Enabled = true;
                        txtpieDWLRTelem.Enabled = true;
                        txtNOPiezo.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezo));
                        txtNOPiezoDWLR.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoDWLR));
                        // txtNOPiezoTele.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoTelem));
                        txtpieDWLRTelem.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoDWLRTelem));
                    }
                    else
                    {
                        arr = arr.Where(a => a.QuantumGWwithdraMoreThan < obj_IndustrialRenewIssusedLetter.GroundWaterAbsRecommToApprPerDay && a.QuantumGWwithdraUpto >= obj_IndustrialRenewIssusedLetter.GroundWaterAbsRecommToApprPerDay).ToArray();
                        if (arr != null && arr.Length > 0)
                        {
                            txtNOPiezo.Enabled = false;
                            txtNOPiezoDWLR.Enabled = false;
                            //txtNOPiezoTele.Enabled = false;
                            txtpieDWLRTelem.Enabled = false;
                            txtNOPiezo.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfManual));
                            txtNOPiezoDWLR.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfDWLR));
                            // txtNOPiezoTele.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfManual));
                            txtpieDWLRTelem.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfDWLRwithTelemetry));
                        }
                    }
                }
                else if (obj_InfrastructureNewIssusedLetter != null)
                {
                    if (obj_InfrastructureNewIssusedLetter.GeneralConditionContent == "")
                    {
                        txtNOPiezo.Enabled = true;
                        txtNOPiezoDWLR.Enabled = true;
                        //txtNOPiezoTele.Enabled = true;
                        txtpieDWLRTelem.Enabled = true;
                        txtNOPiezo.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezo));
                        txtNOPiezoDWLR.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoDWLR));
                        //  txtNOPiezoTele.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoTelem));
                        txtpieDWLRTelem.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoDWLRTelem));
                    }
                    else
                    {
                        arr = arr.Where(a => a.QuantumGWwithdraMoreThan < obj_InfrastructureNewIssusedLetter.GroundWaterAbsRecommToApprPerDay + obj_InfrastructureNewIssusedLetter.GroundWaterAbsRecommToApprDewaterPerDay && a.QuantumGWwithdraUpto >= obj_InfrastructureNewIssusedLetter.GroundWaterAbsRecommToApprPerDay + obj_InfrastructureNewIssusedLetter.GroundWaterAbsRecommToApprDewaterPerDay).ToArray();
                        if (arr != null && arr.Length > 0)
                        {
                            txtNOPiezo.Enabled = false;
                            txtNOPiezoDWLR.Enabled = false;
                            //txtNOPiezoTele.Enabled = false;
                            txtpieDWLRTelem.Enabled = false;
                            txtNOPiezo.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfManual));
                            txtNOPiezoDWLR.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfDWLR));
                            // txtNOPiezoTele.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfManual));
                            txtpieDWLRTelem.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfDWLRwithTelemetry));
                        }
                    }
                }
                else if (obj_InfrastructureRenewIssusedLetter != null)
                {
                    if (obj_InfrastructureRenewIssusedLetter.GeneralConditionContent == "")
                    {
                        txtNOPiezo.Enabled = true;
                        txtNOPiezoDWLR.Enabled = true;
                        //txtNOPiezoTele.Enabled = true;
                        txtpieDWLRTelem.Enabled = true;
                        txtNOPiezo.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezo));
                        txtNOPiezoDWLR.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoDWLR));
                        // txtNOPiezoTele.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoTelem));
                        txtpieDWLRTelem.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoDWLRTelem));
                    }
                    else
                    {
                        arr = arr.Where(a => a.QuantumGWwithdraMoreThan < obj_InfrastructureRenewIssusedLetter.GroundWaterAbsRecommToApprPerDay && a.QuantumGWwithdraUpto >= obj_InfrastructureRenewIssusedLetter.GroundWaterAbsRecommToApprPerDay).ToArray();
                        if (arr != null && arr.Length > 0)
                        {
                            txtNOPiezo.Enabled = false;
                            txtNOPiezoDWLR.Enabled = false;
                            //txtNOPiezoTele.Enabled = false;
                            txtpieDWLRTelem.Enabled = false;
                            txtNOPiezo.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfManual));
                            txtNOPiezoDWLR.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfDWLR));
                            //txtNOPiezoTele.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfManual));
                            txtpieDWLRTelem.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfDWLRwithTelemetry));
                        }
                    }
                }
                else if (obj_MiningNewIssusedLetter != null)
                {
                    if (obj_MiningNewIssusedLetter.GeneralConditionContent == "")
                    {
                        txtNOPiezo.Enabled = true;
                        txtNOPiezoDWLR.Enabled = true;
                        //txtNOPiezoTele.Enabled = true;
                        txtpieDWLRTelem.Enabled = true;
                        txtNOPiezo.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezo));
                        txtNOPiezoDWLR.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoDWLR));
                        // txtNOPiezoTele.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoTelem));
                        txtpieDWLRTelem.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoDWLRTelem));
                    }
                    else
                    {
                        arr = arr.Where(a => a.QuantumGWwithdraMoreThan < obj_MiningNewIssusedLetter.GroundWaterAbsRecommToApprPerDay && a.QuantumGWwithdraUpto >= obj_MiningNewIssusedLetter.GroundWaterAbsRecommToApprSeepDewaterPerDay).ToArray();
                        if (arr != null && arr.Length > 0)
                        {
                            txtNOPiezo.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfManual));
                            txtNOPiezoDWLR.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfDWLR));
                            //txtNOPiezoTele.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfManual));
                            txtpieDWLRTelem.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfDWLRwithTelemetry));
                        }
                        txtNOPiezo.Enabled = false;
                        txtNOPiezoDWLR.Enabled = false;
                        //txtNOPiezoTele.Enabled = false;
                        txtpieDWLRTelem.Enabled = false;


                    }
                }
                else if (obj_MiningRenewIssusedLetter != null)
                {
                    if (obj_MiningRenewIssusedLetter.GeneralConditionContent == "")
                    {
                        txtNOPiezo.Enabled = true;
                        txtNOPiezoDWLR.Enabled = true;
                        //txtNOPiezoTele.Enabled = true;
                        txtpieDWLRTelem.Enabled = true;
                        txtNOPiezo.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezo));
                        txtNOPiezoDWLR.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoDWLR));
                        // txtNOPiezoTele.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoTelem));
                        txtpieDWLRTelem.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfPiezoDWLRTelem));
                    }
                    else
                    {
                        arr = arr.Where(a => a.QuantumGWwithdraMoreThan < obj_MiningRenewIssusedLetter.GroundWaterAbsRecommToApprPerDay + obj_MiningRenewIssusedLetter.GroundWaterAbsRecommToApprSeepDewaterPerDay && a.QuantumGWwithdraUpto >= obj_MiningRenewIssusedLetter.GroundWaterAbsRecommToApprPerDay + obj_MiningRenewIssusedLetter.GroundWaterAbsRecommToApprSeepDewaterPerDay).ToArray();
                        if (arr != null && arr.Length > 0)
                        {
                            txtNOPiezo.Enabled = false;
                            txtNOPiezoDWLR.Enabled = false;
                            //txtNOPiezoTele.Enabled = false;
                            txtpieDWLRTelem.Enabled = false;
                            txtNOPiezo.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfManual));
                            txtNOPiezoDWLR.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfDWLR));
                            // txtNOPiezoTele.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfManual));
                            txtpieDWLRTelem.Text = HttpUtility.HtmlEncode(Convert.ToInt32(arr[0].MonNumberOfDWLRwithTelemetry));
                        }
                    }
                }




                txtselfNOPiezo.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfSelfPiezo));
                txtselfNOPiezoDWLR.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfSelfPiezoDWLR));
                //txtselfNOPiezoTele.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfSelfPiezoTelem));
                txtselfpieDWLRTelem.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfSelfPiezoDWLRTelem));








                txtNoOversWell.Text = HttpUtility.HtmlEncode(Convert.ToInt32(obj_selfCompliance.NoOfObserwell));
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
                //switch (obj_selfCompliance.PiezometerDWLRTelemetry)
                //{
                //    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                //        ddlpieDWLRTelem.SelectedValue = "1";
                //        break;
                //    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                //        ddlpieDWLRTelem.SelectedValue = "0";
                //        break;
                //    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                //        ddlpieDWLRTelem.SelectedValue = "";
                //        break;
                //    default:
                //        ddlpieDWLRTelem.SelectedValue = "";
                //        break;
                //}
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
                    txtCapSTPETP.Text = HttpUtility.HtmlEncode(Convert.ToDecimal(obj_selfCompliance.CapOfSTPETP));
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




            }

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    #endregion

    #region Button Click
    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
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
                try
                {
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                    if (UpdateData(Convert.ToInt64(lblApplicationCodeFrom.Text), sender, e) == 1)
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Record Updated Successfully');", true);
                    PopulateData(Convert.ToInt64(lblApplicationCodeFrom.Text), sender, e);
                }
                catch (Exception)
                {
                    Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
                }
            }
        }
    }



    protected void btnNext_Click(object sender, EventArgs e)
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
                try
                {
                    int status = 0;
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                    status = UpdateData(Convert.ToInt64(lblApplicationCodeFrom.Text), sender, e);
                    if (status == 1)
                    {
                        Server.Transfer("~/ExternalUser/Compliance/SelfCompliance.aspx");
                    }
                }
                catch (ThreadAbortException)
                {

                }
                catch (Exception ex)
                {
                    Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
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
            Server.Transfer("SelfComplianceA.aspx");

        }
    }
    #endregion


    #region Attachment
    protected void btnUploadNOC_Click(object sender, EventArgs e)
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

                strActionName = "UploadNOCCopy";
                NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_selfComplianceAttachment;
                arr_selfComplianceAttachment = obj_selfCompliance.GetNOCAttachmentList();

                //if (arr_selfComplianceAttachment.Count() < 4)
                //{
                try
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));

                    //List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();

                    byte[] buffer = new byte[1];

                    if (FileUploadNOC.HasFile)
                    {

                        str_ext = System.IO.Path.GetExtension(FileUploadNOC.PostedFile.FileName).ToLower();

                        str_fname = FileUploadNOC.FileName;

                        if (str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                        {

                            if (NOCAPExternalUtility.IsValidFile(FileUploadNOC.PostedFile))
                            {
                                if (FileUploadNOC.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                {
                                    obj_selfComplianceAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadNOC.PostedFile);
                                    obj_selfComplianceAttachment.ContentType = FileUploadNOC.PostedFile.ContentType;
                                    obj_selfComplianceAttachment.AttachmentPath = FileUploadNOC.FileName;
                                    obj_selfComplianceAttachment.FileExtension = str_ext;
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageNOC.Text = "File can not upload. It has more than 5 MB size";
                                    lblMessageNOC.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageNOC.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageNOC.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageNOC.Text = "Not a valid file!!..Select an other file!!";
                            lblMessageNOC.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                    }
                    else
                    {
                        lblMessageNOC.Text = "Please select a file..!!";
                        lblMessageNOC.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    obj_selfComplianceAttachment.ApplicationCode = obj_SelfCompliance.ApplicationCode;
                    obj_selfComplianceAttachment.AttachmentCode = obj_SelfCompliance.NOCAttCode;
                    obj_selfComplianceAttachment.AttachmentName = "NOC";

                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    obj_selfComplianceAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;


                    if (obj_selfComplianceAttachment.Add() == 1)
                    {
                        strStatus = "File Upload Success";
                        lblMessageNOC.Text = "File Upload Success";
                        lblMessageNOC.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        lblMessageNOC.Text = obj_selfComplianceAttachment.CustumMessage;
                        lblMessageNOC.ForeColor = System.Drawing.Color.Red;

                    }




                    lblSiteInspection.Text = "";
                    lblMessagegeophotostruct.Text = "";
                    lblGeoPhotowellfitted.Text = "";
                    lblWQRSubmitted.Text = "";
                    lblMineseepage.Text = "";
                    lblgvRainwaterharvesting.Text = "";
                    lblGroundwaterMonitoring.Text = "";
                    lblGroundWaterMonitoringData.Text = "";
                    lblSTPETP.Text = "";
                    lblAnnualCalibration.Text = "";
                    BindGridView(gvNOC, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblMessageNOCCount);

                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
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
                //}
                //else
                //{
                //    lblMessagePhotographs.Text = "Maximum number of files to be uploaded is 5";
                //    lblMessagePhotographs.ForeColor = System.Drawing.Color.Red;
                //}
            }
        }
    }
    protected void btnUploadSiteInspection_Click(object sender, EventArgs e)
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

                strActionName = "UploadSiteInspection";
                NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_selfComplianceAttachment;
                arr_selfComplianceAttachment = obj_selfCompliance.GetCopySiteInspectionAttachmentList();


                try
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));

                    byte[] buffer = new byte[1];

                    if (FileUploadSiteInspection.HasFile)
                    {

                        str_ext = System.IO.Path.GetExtension(FileUploadSiteInspection.PostedFile.FileName).ToLower();

                        str_fname = FileUploadSiteInspection.FileName;

                        if (str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                        {

                            if (NOCAPExternalUtility.IsValidFile(FileUploadSiteInspection.PostedFile))
                            {
                                if (FileUploadSiteInspection.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                {
                                    obj_selfComplianceAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadSiteInspection.PostedFile);
                                    obj_selfComplianceAttachment.ContentType = FileUploadSiteInspection.PostedFile.ContentType;
                                    obj_selfComplianceAttachment.AttachmentPath = FileUploadSiteInspection.FileName;
                                    obj_selfComplianceAttachment.FileExtension = str_ext;
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblSiteInspection.Text = "File can not upload. It has more than 5 MB size";
                                    lblSiteInspection.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblSiteInspection.Text = "Not a valid file!!..Select an other file!!";
                                lblSiteInspection.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblSiteInspection.Text = "Not a valid file!!..Select an other file!!";
                            lblSiteInspection.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                    }
                    else
                    {
                        lblSiteInspection.Text = "Please select a file..!!";
                        lblSiteInspection.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    obj_selfComplianceAttachment.ApplicationCode = obj_SelfCompliance.ApplicationCode;
                    obj_selfComplianceAttachment.AttachmentCode = obj_SelfCompliance.CopySiteInspectionAttCode;
                    obj_selfComplianceAttachment.AttachmentName = "SiteInspection";

                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    obj_selfComplianceAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;


                    if (obj_selfComplianceAttachment.Add() == 1)
                    {
                        strStatus = "File Upload Success";
                        lblSiteInspection.Text = "File Upload Success";
                        lblSiteInspection.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        lblSiteInspection.Text = obj_selfComplianceAttachment.CustumMessage;
                        lblSiteInspection.ForeColor = System.Drawing.Color.Red;

                    }


                    lblMessageNOC.Text = "";

                    lblMessagegeophotostruct.Text = "";
                    lblGeoPhotowellfitted.Text = "";
                    lblWQRSubmitted.Text = "";
                    lblMineseepage.Text = "";
                    lblgvRainwaterharvesting.Text = "";
                    lblGroundwaterMonitoring.Text = "";
                    lblGroundWaterMonitoringData.Text = "";
                    lblSTPETP.Text = "";
                    lblAnnualCalibration.Text = "";
                    BindGridView(gvSiteInspection, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblSiteInspectionCount);

                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
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
    protected void btnUploadgeophotostruct_Click(object sender, EventArgs e)
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

                strActionName = "Uploadgvgeophotostruct";
                NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_selfComplianceAttachment;
                arr_selfComplianceAttachment = obj_selfCompliance.GetGroundwaterAbstractionDataAttachmentList();


                try
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));

                    byte[] buffer = new byte[1];

                    if (FileUploadgeophotostruct.HasFile)
                    {

                        str_ext = System.IO.Path.GetExtension(FileUploadgeophotostruct.PostedFile.FileName).ToLower();

                        str_fname = FileUploadgeophotostruct.FileName;

                        if (str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                        {

                            if (NOCAPExternalUtility.IsValidFile(FileUploadgeophotostruct.PostedFile))
                            {
                                if (FileUploadgeophotostruct.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                {
                                    obj_selfComplianceAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadgeophotostruct.PostedFile);
                                    obj_selfComplianceAttachment.ContentType = FileUploadgeophotostruct.PostedFile.ContentType;
                                    obj_selfComplianceAttachment.AttachmentPath = FileUploadgeophotostruct.FileName;
                                    obj_selfComplianceAttachment.FileExtension = str_ext;
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessagegeophotostruct.Text = "File can not upload. It has more than 5 MB size";
                                    lblMessagegeophotostruct.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessagegeophotostruct.Text = "Not a valid file!!..Select an other file!!";
                                lblMessagegeophotostruct.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessagegeophotostruct.Text = "Not a valid file!!..Select an other file!!";
                            lblMessagegeophotostruct.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                    }
                    else
                    {
                        lblMessagegeophotostruct.Text = "Please select a file..!!";
                        lblMessagegeophotostruct.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    obj_selfComplianceAttachment.ApplicationCode = obj_SelfCompliance.ApplicationCode;
                    obj_selfComplianceAttachment.AttachmentCode = obj_SelfCompliance.GroundwaterAbstractionDataAttCode;
                    obj_selfComplianceAttachment.AttachmentName = "geophotostruct";

                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    obj_selfComplianceAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;


                    if (obj_selfComplianceAttachment.Add() == 1)
                    {
                        strStatus = "File Upload Success";
                        lblMessagegeophotostruct.Text = "File Upload Success";
                        lblMessagegeophotostruct.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        lblMessagegeophotostruct.Text = obj_selfComplianceAttachment.CustumMessage;
                        lblMessagegeophotostruct.ForeColor = System.Drawing.Color.Red;

                    }

                    lblMessageNOC.Text = "";
                    lblSiteInspection.Text = "";

                    lblGeoPhotowellfitted.Text = "";
                    lblWQRSubmitted.Text = "";
                    lblMineseepage.Text = "";
                    lblgvRainwaterharvesting.Text = "";
                    lblGroundwaterMonitoring.Text = "";
                    lblGroundWaterMonitoringData.Text = "";
                    lblSTPETP.Text = "";
                    lblAnnualCalibration.Text = "";
                    BindGridView(gvgeophotostruct, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblgeophotostructCount);

                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
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
    protected void btnUploadGeoPhotowellfitted_Click(object sender, EventArgs e)
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

                strActionName = "UploadGeoPhotowellfitted";
                NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_selfComplianceAttachment;
                arr_selfComplianceAttachment = obj_selfCompliance.GetWellFittedWithWaterFlowMeterAttachmentList();


                try
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));

                    byte[] buffer = new byte[1];

                    if (FileUploadGeoPhotowellfitted.HasFile)
                    {

                        str_ext = System.IO.Path.GetExtension(FileUploadGeoPhotowellfitted.PostedFile.FileName).ToLower();

                        str_fname = FileUploadGeoPhotowellfitted.FileName;

                        if (str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                        {

                            if (NOCAPExternalUtility.IsValidFile(FileUploadGeoPhotowellfitted.PostedFile))
                            {
                                if (FileUploadGeoPhotowellfitted.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                {
                                    obj_selfComplianceAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadGeoPhotowellfitted.PostedFile);
                                    obj_selfComplianceAttachment.ContentType = FileUploadGeoPhotowellfitted.PostedFile.ContentType;
                                    obj_selfComplianceAttachment.AttachmentPath = FileUploadGeoPhotowellfitted.FileName;
                                    obj_selfComplianceAttachment.FileExtension = str_ext;
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblGeoPhotowellfitted.Text = "File can not upload. It has more than 5 MB size";
                                    lblGeoPhotowellfitted.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblGeoPhotowellfitted.Text = "Not a valid file!!..Select an other file!!";
                                lblGeoPhotowellfitted.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblGeoPhotowellfitted.Text = "Not a valid file!!..Select an other file!!";
                            lblGeoPhotowellfitted.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                    }
                    else
                    {
                        lblGeoPhotowellfitted.Text = "Please select a file..!!";
                        lblGeoPhotowellfitted.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    obj_selfComplianceAttachment.ApplicationCode = obj_SelfCompliance.ApplicationCode;
                    obj_selfComplianceAttachment.AttachmentCode = obj_SelfCompliance.WellFittedWithWaterFlowMeterAttCode;
                    obj_selfComplianceAttachment.AttachmentName = "GeoPhotowellfitted";

                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    obj_selfComplianceAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;


                    if (obj_selfComplianceAttachment.Add() == 1)
                    {
                        strStatus = "File Upload Success";
                        lblGeoPhotowellfitted.Text = "File Upload Success";
                        lblGeoPhotowellfitted.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        lblGeoPhotowellfitted.Text = obj_selfComplianceAttachment.CustumMessage;
                        lblGeoPhotowellfitted.ForeColor = System.Drawing.Color.Red;

                    }

                    lblMessageNOC.Text = "";
                    lblSiteInspection.Text = "";
                    lblMessagegeophotostruct.Text = "";

                    lblWQRSubmitted.Text = "";
                    lblMineseepage.Text = "";
                    lblgvRainwaterharvesting.Text = "";
                    lblGroundwaterMonitoring.Text = "";
                    lblGroundWaterMonitoringData.Text = "";
                    lblSTPETP.Text = "";
                    lblAnnualCalibration.Text = "";
                    BindGridView(gvGeoPhotowellfitted, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblGeoPhotowellfittedCount);

                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
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
    protected void btnUploadWQRSubmitted_Click(object sender, EventArgs e)
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

                strActionName = "UploadWQRSubmitted";
                NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_selfComplianceAttachment;
                arr_selfComplianceAttachment = obj_selfCompliance.GetGroundWaterQualityAttachmentList();


                try
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));

                    byte[] buffer = new byte[1];

                    if (FileUploadWQRSubmitted.HasFile)
                    {

                        str_ext = System.IO.Path.GetExtension(FileUploadWQRSubmitted.PostedFile.FileName).ToLower();

                        str_fname = FileUploadWQRSubmitted.FileName;

                        if (str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                        {

                            if (NOCAPExternalUtility.IsValidFile(FileUploadWQRSubmitted.PostedFile))
                            {
                                if (FileUploadWQRSubmitted.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                {
                                    obj_selfComplianceAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadWQRSubmitted.PostedFile);
                                    obj_selfComplianceAttachment.ContentType = FileUploadWQRSubmitted.PostedFile.ContentType;
                                    obj_selfComplianceAttachment.AttachmentPath = FileUploadWQRSubmitted.FileName;
                                    obj_selfComplianceAttachment.FileExtension = str_ext;
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblWQRSubmitted.Text = "File can not upload. It has more than 5 MB size";
                                    lblWQRSubmitted.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblWQRSubmitted.Text = "Not a valid file!!..Select an other file!!";
                                lblWQRSubmitted.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblWQRSubmitted.Text = "Not a valid file!!..Select an other file!!";
                            lblWQRSubmitted.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                    }
                    else
                    {
                        lblWQRSubmitted.Text = "Please select a file..!!";
                        lblWQRSubmitted.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    obj_selfComplianceAttachment.ApplicationCode = obj_SelfCompliance.ApplicationCode;
                    obj_selfComplianceAttachment.AttachmentCode = obj_SelfCompliance.GroundWaterQualityAttCode;
                    obj_selfComplianceAttachment.AttachmentName = "GeoPhotowellfitted";

                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    obj_selfComplianceAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;


                    if (obj_selfComplianceAttachment.Add() == 1)
                    {
                        strStatus = "File Upload Success";
                        lblWQRSubmitted.Text = "File Upload Success";
                        lblWQRSubmitted.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        lblWQRSubmitted.Text = obj_selfComplianceAttachment.CustumMessage;
                        lblWQRSubmitted.ForeColor = System.Drawing.Color.Red;

                    }
                    lblMessageNOC.Text = "";
                    lblSiteInspection.Text = "";
                    lblMessagegeophotostruct.Text = "";
                    lblGeoPhotowellfitted.Text = "";

                    lblMineseepage.Text = "";
                    lblgvRainwaterharvesting.Text = "";
                    lblGroundwaterMonitoring.Text = "";
                    lblGroundWaterMonitoringData.Text = "";
                    lblSTPETP.Text = "";
                    lblAnnualCalibration.Text = "";
                    BindGridView(gvWQRSubmitted, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblWQRSubmittedCount);

                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
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
    protected void btnUploadRainwaterharvesting_Click(object sender, EventArgs e)
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

                strActionName = "UploadRainwaterharvesting";
                NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_selfComplianceAttachment;
                arr_selfComplianceAttachment = obj_selfCompliance.GetGeoRainWaterHarvRechAttachmentList();


                try
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));

                    byte[] buffer = new byte[1];

                    if (FileUploadRainwaterharvesting.HasFile)
                    {

                        str_ext = System.IO.Path.GetExtension(FileUploadRainwaterharvesting.PostedFile.FileName).ToLower();

                        str_fname = FileUploadRainwaterharvesting.FileName;

                        if (str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                        {

                            if (NOCAPExternalUtility.IsValidFile(FileUploadRainwaterharvesting.PostedFile))
                            {
                                if (FileUploadRainwaterharvesting.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                {
                                    obj_selfComplianceAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadRainwaterharvesting.PostedFile);
                                    obj_selfComplianceAttachment.ContentType = FileUploadRainwaterharvesting.PostedFile.ContentType;
                                    obj_selfComplianceAttachment.AttachmentPath = FileUploadRainwaterharvesting.FileName;
                                    obj_selfComplianceAttachment.FileExtension = str_ext;
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblgvRainwaterharvesting.Text = "File can not upload. It has more than 5 MB size";
                                    lblgvRainwaterharvesting.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblgvRainwaterharvesting.Text = "Not a valid file!!..Select an other file!!";
                                lblgvRainwaterharvesting.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblgvRainwaterharvesting.Text = "Not a valid file!!..Select an other file!!";
                            lblgvRainwaterharvesting.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                    }
                    else
                    {
                        lblgvRainwaterharvesting.Text = "Please select a file..!!";
                        lblgvRainwaterharvesting.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    obj_selfComplianceAttachment.ApplicationCode = obj_SelfCompliance.ApplicationCode;
                    obj_selfComplianceAttachment.AttachmentCode = obj_SelfCompliance.GeoRainWaterHarvRechAttCode;
                    obj_selfComplianceAttachment.AttachmentName = "gvRainwaterharvesting";

                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    obj_selfComplianceAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;


                    if (obj_selfComplianceAttachment.Add() == 1)
                    {
                        strStatus = "File Upload Success";
                        lblgvRainwaterharvesting.Text = "File Upload Success";
                        lblgvRainwaterharvesting.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        lblgvRainwaterharvesting.Text = obj_selfComplianceAttachment.CustumMessage;
                        lblgvRainwaterharvesting.ForeColor = System.Drawing.Color.Red;

                    }
                    lblMessageNOC.Text = "";
                    lblSiteInspection.Text = "";
                    lblMessagegeophotostruct.Text = "";
                    lblGeoPhotowellfitted.Text = "";
                    lblWQRSubmitted.Text = "";
                    lblMineseepage.Text = "";

                    lblGroundwaterMonitoring.Text = "";
                    lblGroundWaterMonitoringData.Text = "";
                    lblSTPETP.Text = "";
                    lblAnnualCalibration.Text = "";
                    BindGridView(gvRainwaterharvesting, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblgvRainwaterharvestingCount);

                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
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
    protected void btnUploadGroundwaterMonitoring_Click(object sender, EventArgs e)
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

                strActionName = "UploadRainwaterharvesting";
                NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_selfComplianceAttachment;
                arr_selfComplianceAttachment = obj_selfCompliance.GetGroundwaterMonitoringAttachmentList();


                try
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));

                    byte[] buffer = new byte[1];

                    if (FileUploadGroundwaterMonitoring.HasFile)
                    {

                        str_ext = System.IO.Path.GetExtension(FileUploadGroundwaterMonitoring.PostedFile.FileName).ToLower();

                        str_fname = FileUploadGroundwaterMonitoring.FileName;

                        if (str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                        {

                            if (NOCAPExternalUtility.IsValidFile(FileUploadGroundwaterMonitoring.PostedFile))
                            {
                                if (FileUploadGroundwaterMonitoring.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                {
                                    obj_selfComplianceAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadGroundwaterMonitoring.PostedFile);
                                    obj_selfComplianceAttachment.ContentType = FileUploadGroundwaterMonitoring.PostedFile.ContentType;
                                    obj_selfComplianceAttachment.AttachmentPath = FileUploadGroundwaterMonitoring.FileName;
                                    obj_selfComplianceAttachment.FileExtension = str_ext;
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblGroundwaterMonitoring.Text = "File can not upload. It has more than 5 MB size";
                                    lblGroundwaterMonitoring.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblGroundwaterMonitoring.Text = "Not a valid file!!..Select an other file!!";
                                lblGroundwaterMonitoring.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblGroundwaterMonitoring.Text = "Not a valid file!!..Select an other file!!";
                            lblGroundwaterMonitoring.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                    }
                    else
                    {
                        lblGroundwaterMonitoring.Text = "Please select a file..!!";
                        lblGroundwaterMonitoring.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    obj_selfComplianceAttachment.ApplicationCode = obj_SelfCompliance.ApplicationCode;
                    obj_selfComplianceAttachment.AttachmentCode = obj_SelfCompliance.GroundwaterMonitoringAttCode;
                    obj_selfComplianceAttachment.AttachmentName = "GroundwaterMonitoring";

                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    obj_selfComplianceAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;


                    if (obj_selfComplianceAttachment.Add() == 1)
                    {
                        strStatus = "File Upload Success";
                        lblGroundwaterMonitoring.Text = "File Upload Success";
                        lblGroundwaterMonitoring.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        lblGroundwaterMonitoring.Text = obj_selfComplianceAttachment.CustumMessage;
                        lblGroundwaterMonitoring.ForeColor = System.Drawing.Color.Red;

                    }
                    lblMessageNOC.Text = "";
                    lblSiteInspection.Text = "";
                    lblMessagegeophotostruct.Text = "";
                    lblGeoPhotowellfitted.Text = "";
                    lblWQRSubmitted.Text = "";
                    lblMineseepage.Text = "";
                    lblgvRainwaterharvesting.Text = "";

                    lblGroundWaterMonitoringData.Text = "";
                    lblSTPETP.Text = "";
                    lblAnnualCalibration.Text = "";
                    BindGridView(gvGroundwaterMonitoring, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblGroundwaterMonitoring);

                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
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
    protected void btnUploadSTPETP_Click(object sender, EventArgs e)
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

                strActionName = "UploadSTPETP";
                NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_selfComplianceAttachment;
                arr_selfComplianceAttachment = obj_selfCompliance.GetPhotoETPSTPAttachmentList();


                try
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));

                    byte[] buffer = new byte[1];

                    if (FileUploadSTPETP.HasFile)
                    {

                        str_ext = System.IO.Path.GetExtension(FileUploadSTPETP.PostedFile.FileName).ToLower();

                        str_fname = FileUploadSTPETP.FileName;

                        if (str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                        {

                            if (NOCAPExternalUtility.IsValidFile(FileUploadSTPETP.PostedFile))
                            {
                                if (FileUploadSTPETP.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                {
                                    obj_selfComplianceAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadSTPETP.PostedFile);
                                    obj_selfComplianceAttachment.ContentType = FileUploadSTPETP.PostedFile.ContentType;
                                    obj_selfComplianceAttachment.AttachmentPath = FileUploadSTPETP.FileName;
                                    obj_selfComplianceAttachment.FileExtension = str_ext;
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblSTPETP.Text = "File can not upload. It has more than 5 MB size";
                                    lblSTPETP.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblSTPETP.Text = "Not a valid file!!..Select an other file!!";
                                lblSTPETP.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblSTPETP.Text = "Not a valid file!!..Select an other file!!";
                            lblSTPETP.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                    }
                    else
                    {
                        lblSTPETP.Text = "Please select a file..!!";
                        lblSTPETP.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    obj_selfComplianceAttachment.ApplicationCode = obj_SelfCompliance.ApplicationCode;
                    obj_selfComplianceAttachment.AttachmentCode = obj_SelfCompliance.PhotoETPSTPAttCode;
                    obj_selfComplianceAttachment.AttachmentName = "lblSTPETP";

                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    obj_selfComplianceAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;


                    if (obj_selfComplianceAttachment.Add() == 1)
                    {
                        strStatus = "File Upload Success";
                        lblSTPETP.Text = "File Upload Success";
                        lblSTPETP.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        lblSTPETP.Text = obj_selfComplianceAttachment.CustumMessage;
                        lblSTPETP.ForeColor = System.Drawing.Color.Red;

                    }
                    lblMessageNOC.Text = "";
                    lblSiteInspection.Text = "";
                    lblMessagegeophotostruct.Text = "";
                    lblGeoPhotowellfitted.Text = "";
                    lblWQRSubmitted.Text = "";
                    lblMineseepage.Text = "";
                    lblgvRainwaterharvesting.Text = "";
                    lblGroundwaterMonitoring.Text = "";
                    lblGroundWaterMonitoringData.Text = "";

                    BindGridView(gvSTPETP, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblSTPETPCount);

                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
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
    protected void btnUploadGroundWaterMonitoringData_Click(object sender, EventArgs e)
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

                strActionName = "UploadSTPETP";
                NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_selfComplianceAttachment;
                arr_selfComplianceAttachment = obj_selfCompliance.GetGroundWaterMonitoringDataAttachmentList();


                try
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));

                    byte[] buffer = new byte[1];

                    if (FileUploadGroundWaterMonitoringData.HasFile)
                    {

                        str_ext = System.IO.Path.GetExtension(FileUploadGroundWaterMonitoringData.PostedFile.FileName).ToLower();

                        str_fname = FileUploadGroundWaterMonitoringData.FileName;

                        if (str_ext == ".xlsx" || str_ext == ".xls" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                        {

                            if (NOCAPExternalUtility.IsValidFileWithExcel(FileUploadGroundWaterMonitoringData.PostedFile))
                            {
                                if (FileUploadGroundWaterMonitoringData.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                {
                                    obj_selfComplianceAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadGroundWaterMonitoringData.PostedFile);
                                    obj_selfComplianceAttachment.ContentType = FileUploadGroundWaterMonitoringData.PostedFile.ContentType;
                                    obj_selfComplianceAttachment.AttachmentPath = FileUploadGroundWaterMonitoringData.FileName;
                                    obj_selfComplianceAttachment.FileExtension = str_ext;
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblGroundWaterMonitoringData.Text = "File can not upload. It has more than 5 MB size";
                                    lblGroundWaterMonitoringData.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblGroundWaterMonitoringData.Text = "Not a valid file!!..Select an other file!!";
                                lblGroundWaterMonitoringData.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblGroundWaterMonitoringData.Text = "Not a valid file!!..Select an other file!!";
                            lblGroundWaterMonitoringData.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                    }
                    else
                    {
                        lblGroundWaterMonitoringData.Text = "Please select a file..!!";
                        lblGroundWaterMonitoringData.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    obj_selfComplianceAttachment.ApplicationCode = obj_SelfCompliance.ApplicationCode;
                    obj_selfComplianceAttachment.AttachmentCode = obj_SelfCompliance.GroundWaterMonitoringDataAttCode;
                    obj_selfComplianceAttachment.AttachmentName = "GroundWaterMonitoringData";

                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    obj_selfComplianceAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;


                    if (obj_selfComplianceAttachment.Add() == 1)
                    {
                        strStatus = "File Upload Success";
                        lblGroundWaterMonitoringData.Text = "File Upload Success";
                        lblGroundWaterMonitoringData.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        lblGroundWaterMonitoringData.Text = obj_selfComplianceAttachment.CustumMessage;
                        lblGroundWaterMonitoringData.ForeColor = System.Drawing.Color.Red;

                    }
                    lblMessageNOC.Text = "";
                    lblSiteInspection.Text = "";
                    lblMessagegeophotostruct.Text = "";
                    lblGeoPhotowellfitted.Text = "";
                    lblWQRSubmitted.Text = "";
                    lblMineseepage.Text = "";
                    lblgvRainwaterharvesting.Text = "";
                    lblGroundwaterMonitoring.Text = "";

                    lblSTPETP.Text = "";
                    lblAnnualCalibration.Text = "";
                    BindGridView(gvGroundWaterMonitoringData, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblGroundWaterMonitoringDataCount);

                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
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
    protected void btnUploadMineseepage_Click(object sender, EventArgs e)
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

                strActionName = "UploadSTPETP";
                NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_selfComplianceAttachment;
                arr_selfComplianceAttachment = obj_selfCompliance.GetMiningSeepageAttachmentList();


                try
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));

                    byte[] buffer = new byte[1];

                    if (FileUploadMineseepage.HasFile)
                    {

                        str_ext = System.IO.Path.GetExtension(FileUploadMineseepage.PostedFile.FileName).ToLower();

                        str_fname = FileUploadMineseepage.FileName;

                        if (str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                        {

                            if (NOCAPExternalUtility.IsValidFile(FileUploadMineseepage.PostedFile))
                            {
                                if (FileUploadMineseepage.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                {
                                    obj_selfComplianceAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadMineseepage.PostedFile);
                                    obj_selfComplianceAttachment.ContentType = FileUploadMineseepage.PostedFile.ContentType;
                                    obj_selfComplianceAttachment.AttachmentPath = FileUploadMineseepage.FileName;
                                    obj_selfComplianceAttachment.FileExtension = str_ext;
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMineseepage.Text = "File can not upload. It has more than 5 MB size";
                                    lblMineseepage.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMineseepage.Text = "Not a valid file!!..Select an other file!!";
                                lblMineseepage.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMineseepage.Text = "Not a valid file!!..Select an other file!!";
                            lblMineseepage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                    }
                    else
                    {
                        lblMineseepage.Text = "Please select a file..!!";
                        lblMineseepage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    obj_selfComplianceAttachment.ApplicationCode = obj_SelfCompliance.ApplicationCode;
                    obj_selfComplianceAttachment.AttachmentCode = obj_SelfCompliance.MiningSeepageAttCode;
                    obj_selfComplianceAttachment.AttachmentName = "Mineseepage";

                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    obj_selfComplianceAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;


                    if (obj_selfComplianceAttachment.Add() == 1)
                    {
                        strStatus = "File Upload Success";
                        lblMineseepage.Text = "File Upload Success";
                        lblMineseepage.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        lblMineseepage.Text = obj_selfComplianceAttachment.CustumMessage;
                        lblMineseepage.ForeColor = System.Drawing.Color.Red;

                    }
                    lblMessageNOC.Text = "";
                    lblSiteInspection.Text = "";
                    lblMessagegeophotostruct.Text = "";
                    lblGeoPhotowellfitted.Text = "";
                    lblWQRSubmitted.Text = "";

                    lblgvRainwaterharvesting.Text = "";
                    lblGroundwaterMonitoring.Text = "";
                    lblGroundWaterMonitoringData.Text = "";
                    lblSTPETP.Text = "";
                    lblAnnualCalibration.Text = "";
                    BindGridView(gvMineseepage, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblMineseepageCount);

                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
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

    protected void btnFileUploadAnnualCalibration_Click(object sender, EventArgs e)
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

                strActionName = "UploadSTPETP";
                NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_selfComplianceAttachment;
                arr_selfComplianceAttachment = obj_selfCompliance.GetAnnualCalibrationAttachmentList();


                try
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));

                    byte[] buffer = new byte[1];

                    if (FileUploadAnnualCalibration.HasFile)
                    {

                        str_ext = System.IO.Path.GetExtension(FileUploadAnnualCalibration.PostedFile.FileName).ToLower();

                        str_fname = FileUploadAnnualCalibration.FileName;

                        if (str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                        {

                            if (NOCAPExternalUtility.IsValidFile(FileUploadAnnualCalibration.PostedFile))
                            {
                                if (FileUploadAnnualCalibration.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                {
                                    obj_selfComplianceAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadAnnualCalibration.PostedFile);
                                    obj_selfComplianceAttachment.ContentType = FileUploadAnnualCalibration.PostedFile.ContentType;
                                    obj_selfComplianceAttachment.AttachmentPath = FileUploadAnnualCalibration.FileName;
                                    obj_selfComplianceAttachment.FileExtension = str_ext;
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblAnnualCalibration.Text = "File can not upload. It has more than 5 MB size";
                                    lblAnnualCalibration.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblAnnualCalibration.Text = "Not a valid file!!..Select an other file!!";
                                lblAnnualCalibration.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblAnnualCalibration.Text = "Not a valid file!!..Select an other file!!";
                            lblAnnualCalibration.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                    }
                    else
                    {
                        lblAnnualCalibration.Text = "Please select a file..!!";
                        lblAnnualCalibration.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    obj_selfComplianceAttachment.ApplicationCode = obj_SelfCompliance.ApplicationCode;
                    obj_selfComplianceAttachment.AttachmentCode = obj_SelfCompliance.AnnualCalibrationAttCode;
                    obj_selfComplianceAttachment.AttachmentName = "AnnualCalibration";

                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    obj_selfComplianceAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;


                    if (obj_selfComplianceAttachment.Add() == 1)
                    {
                        strStatus = "File Upload Success";
                        lblAnnualCalibration.Text = "File Upload Success";
                        lblAnnualCalibration.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        lblAnnualCalibration.Text = obj_selfComplianceAttachment.CustumMessage;
                        lblAnnualCalibration.ForeColor = System.Drawing.Color.Red;

                    }
                    lblMessageNOC.Text = "";
                    lblSiteInspection.Text = "";
                    lblMessagegeophotostruct.Text = "";
                    lblGeoPhotowellfitted.Text = "";
                    lblWQRSubmitted.Text = "";

                    lblgvRainwaterharvesting.Text = "";
                    lblGroundwaterMonitoring.Text = "";
                    lblGroundWaterMonitoringData.Text = "";
                    lblSTPETP.Text = "";
                    BindGridView(gvAnnualCalibration, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblAnnualCalibrationCount);

                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
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



    #endregion

    #region RowDeleting
    protected void gvNOC_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "Delete Self Compliance Attachment";
                long lng_ApplicationCode = Convert.ToInt32(gvNOC.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvNOC.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvNOC.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_SelfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment(lng_ApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);


                if (obj_SelfComplianceAttachment.Delete() == 1)
                {
                    strStatus = "File Deleted Success";
                    lblMessageNOC.Text = obj_SelfComplianceAttachment.CustumMessage;
                    lblMessageNOC.ForeColor = System.Drawing.Color.Red;

                    BindGridView(gvNOC, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblMessageNOCCount);
                }
                else
                {
                    strStatus = "File Deleted Failed";
                    lblMessageNOC.Text = obj_SelfComplianceAttachment.CustumMessage;
                    lblMessageNOC.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
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
    protected void gvSiteInspection_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "File Delete Penalty";
                if (DeleteAttchment((GridView)sender, e, lblSiteInspection, strActionName) == 1)
                    BindGridView(gvSiteInspection, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblSiteInspectionCount);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }
    protected void gvgeophotostruct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "File Delete Penalty";
                if (DeleteAttchment((GridView)sender, e, lblMessagegeophotostruct, strActionName) == 1)
                    BindGridView(gvgeophotostruct, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblgeophotostructCount);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }
    protected void gvGeoPhotowellfitted_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "File Delete Penalty";
                if (DeleteAttchment((GridView)sender, e, lblGeoPhotowellfitted, strActionName) == 1)
                    BindGridView(gvGeoPhotowellfitted, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblGeoPhotowellfittedCount);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }
    protected void gvWQRSubmitted_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "File Delete Penalty";
                if (DeleteAttchment((GridView)sender, e, lblWQRSubmitted, strActionName) == 1)
                    BindGridView(gvWQRSubmitted, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblWQRSubmittedCount);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }
    protected void gvRainwaterharvesting_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "File Delete Penalty";
                if (DeleteAttchment((GridView)sender, e, lblgvRainwaterharvesting, strActionName) == 1)
                    BindGridView(gvRainwaterharvesting, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblgvRainwaterharvestingCount);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }

    protected void gvGroundwaterMonitoring_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "File Delete Penalty";
                if (DeleteAttchment((GridView)sender, e, lblGroundwaterMonitoring, strActionName) == 1)
                    BindGridView(gvGroundwaterMonitoring, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblGroundwaterMonitoringCount);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }

    protected void gvSTPETP_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "File Delete Penalty";
                if (DeleteAttchment((GridView)sender, e, lblSTPETP, strActionName) == 1)
                    BindGridView(gvSTPETP, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblSTPETPCount);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }

    protected void gvGroundWaterMonitoringData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "File Delete Penalty";
                if (DeleteAttchment((GridView)sender, e, lblGroundWaterMonitoringData, strActionName) == 1)
                    BindGridView(gvGroundWaterMonitoringData, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblGroundWaterMonitoringDataCount);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }

    protected void gvMineseepage_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "File Delete Penalty";
                if (DeleteAttchment((GridView)sender, e, lblMineseepage, strActionName) == 1)
                    BindGridView(gvMineseepage, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblMineseepageCount);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }

    protected void gvAnnualCalibration_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "File Delete Penalty";
                if (DeleteAttchment((GridView)sender, e, lblAnnualCalibration, strActionName) == 1)
                    BindGridView(gvAnnualCalibration, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblAnnualCalibrationCount);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }


    private int DeleteAttchment(GridView gv, GridViewDeleteEventArgs e, Label lblMessage, string str_ActionName)
    {
        try
        {
            strActionName = "Delete Self gvgeophotostruct Attachment";
            long lng_ApplicationCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["ApplicationCode"]);
            int int_AttachmentCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["AttachmentCode"]);
            int int_AttachmentCodeSerialNumber = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
            NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_SelfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment(lng_ApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);

            if (obj_SelfComplianceAttachment.Delete() == 1)
            {
                lblMessage.Text = obj_SelfComplianceAttachment.CustumMessage;
                strStatus = "File Delete Success";
                return 1;
            }
            else
            {
                lblMessage.Text = obj_SelfComplianceAttachment.CustumMessage;
                strStatus = "File Delete Failed";
                return 0;
            }
        }
        catch (Exception ex)
        { return 0; }
        finally
        {
            ActionTrail obj_ExtActionTrail = new ActionTrail();
            if (Session["ExternalUserCode"] != null)
            {
                obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
                obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
                obj_ExtActionTrail.ActionPerformed = strPageName + "-" + str_ActionName;
                obj_ExtActionTrail.Status = strStatus;
                if (obj_ExtActionTrail != null)
                    ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
            }
        }
    }

    #endregion

    #region ViewFile_Click

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

                if (e.CommandArgument != null)
                {

                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_industrialNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.SelfcompDownloadFiles(lng_industrialNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

                }

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
                //lblMessageReferralLetter.Text = ex.Message;
            }
            finally
            {
                Response.End();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
            }
        }
    }


    #endregion


    #region SelectionIndexchange
    //protected void ddlPiezometer_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    if (ddlPiezometer.SelectedValue.ToString() == "1")
    //        lnkAddPiezometerDetail.Visible = true;
    //    else
    //        lnkAddPiezometerDetail.Visible = false;

    //}
    protected void ddlPresentwithdrawal_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPresentwithdrawal.SelectedValue == "0")
            {
                //txtPresentwithdrawalInDay.Text = string.Empty;
                //txtPresentwithdrawalInDay.Enabled = false;
                //rftxtPresentwithdrawalInDay.Enabled = false;
                //revtxtPresentwithdrawalInDay.Enabled = false;

                //txtPresentwithdrawalInYear.Text = string.Empty;
                //txtPresentwithdrawalInYear.Enabled = false;
                //rftxtPresentwithdrawalInYear.Enabled = false;
                //revtxtPresentwithdrawalInYear.Enabled = false;

                //txtDewPresentwithdrawalInDay.Text = string.Empty;
                //txtDewPresentwithdrawalInDay.Enabled = false;
                //revtxtDewPresentwithdrawalInDay.Enabled = false;

                //txtDewPresentwithdrawalInYear.Text = string.Empty;
                //txtDewPresentwithdrawalInYear.Enabled = false;
                //revtxtDewPresentwithdrawalInYear.Enabled = false;

                ddlAnyVariation.SelectedIndex = 0;
                ddlAnyVariation.Enabled = false;
                txtQtyVariDay.Text = string.Empty;
                txtQtyVariDay.Enabled = false;
                revtxtQtyVariDay.Enabled = false;

                txtQtyVariYear.Text = string.Empty;
                txtQtyVariYear.Enabled = false;
                revtxtQtyVariYear.Enabled = false;
                ddlAbstractionTW.SelectedIndex = 0;
                ddlAbstractionTW.Enabled = false;

                rfvtxtQtyVariDay.Enabled = false;
                rfvtxtQtyVariYear.Enabled = false;
                // rfvtxtDewPresentwithdrawalInDay.Enabled = false;
                // rfvtxtDewPresentwithdrawalInYear.Enabled = false;
                rfvddlAnyVariation.Enabled = false;
                rfvddlAbstractionTW.Enabled = false;
            }
            else
            {
                // txtPresentwithdrawalInDay.Enabled = true;
                // revtxtPresentwithdrawalInDay.Enabled = true;

                // txtPresentwithdrawalInYear.Enabled = true;
                // revtxtPresentwithdrawalInYear.Enabled = true;
                // rftxtPresentwithdrawalInDay.Enabled = true;
                // rftxtPresentwithdrawalInYear.Enabled = true;

                //txtDewPresentwithdrawalInDay.Enabled = true;
                //revtxtDewPresentwithdrawalInDay.Enabled = true;


                // txtDewPresentwithdrawalInYear.Enabled = true;
                // revtxtDewPresentwithdrawalInYear.Enabled = true;

                // ddlAnyVariation.SelectedIndex = 0;
                ddlAnyVariation.Enabled = true;

                txtQtyVariDay.Enabled = true;
                revtxtQtyVariDay.Enabled = true;


                txtQtyVariYear.Enabled = true;
                revtxtQtyVariYear.Enabled = true;
                // ddlAbstractionTW.SelectedIndex = 0;
                ddlAbstractionTW.Enabled = true;
                rfvtxtQtyVariDay.Enabled = true;
                rfvtxtQtyVariYear.Enabled = true;
                //    rfvtxtDewPresentwithdrawalInDay.Enabled = true;
                // rfvtxtDewPresentwithdrawalInYear.Enabled = true;
                rfvddlAnyVariation.Enabled = true;
                rfvddlAbstractionTW.Enabled = true;


            }
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
    }
    protected void ddlRWHArtificialRecharge_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlRWHArtificialRecharge.SelectedValue == "0")
            //{
            //    txtRWHArtificialRechargeNo.Text = string.Empty;
            //    txtRWHArtificialRechargeNo.Enabled = false;
            //    revtxtRWHArtificialRechargeNo.Enabled = false;

            //    txtRWHArtificialRechargeCapacity.Text = string.Empty;
            //    txtRWHArtificialRechargeCapacity.Enabled = false;
            //    revtxtRWHArtificialRechargeCapacity.Enabled = false;
            //}
            //else
            //{
            //    txtRWHArtificialRechargeNo.Enabled = true;
            //    revtxtRWHArtificialRechargeNo.Enabled = true;

            //    txtRWHArtificialRechargeCapacity.Enabled = true;
            //    revtxtRWHArtificialRechargeCapacity.Enabled = true;
            //}
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
    }
    protected void ddlRecycleReuse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //    if (ddlRecycleReuse.SelectedValue == "0")
            //    {
            //        txtRecycleReuseInDay.Text = string.Empty;
            //        txtRecycleReuseInDay.Enabled = false;
            //        revtxtRecycleReuseInDay.Enabled = false;

            //        txtRecycleReuseInYear.Text = string.Empty;
            //        txtRecycleReuseInYear.Enabled = false;
            //        revtxtRecycleReuseInYear.Enabled = false;
            //    }
            //    else
            //    {
            //        txtRecycleReuseInDay.Enabled = true;
            //        revtxtRecycleReuseInDay.Enabled = true;

            //        txtRecycleReuseInYear.Enabled = true;
            //        revtxtRecycleReuseInYear.Enabled = true;
            //    }
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
    }

    protected void ddlNameOfAgency_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNameOfAgency.SelectedValue.ToString() != "")
        {
            rfvtxtDateOfInsp1.Enabled = true;
            rfvddlSiteInsp.Enabled = true;
            txtDateOfInsp1.Enabled = true;
            ImgBtnDateOfInsp1.Enabled = true;
            ddlSiteInsp.Enabled = true;
            if (ddlNameOfAgency.SelectedItem.ToString() == "Any Other")
            {
                // txtOtherAgency.Text = string.Empty;
                txtOtherAgency.Enabled = true;
                rfvOtherAgency.Enabled = true;

            }
            else
            {
                txtOtherAgency.Text = string.Empty;
                txtOtherAgency.Enabled = false;
                rfvOtherAgency.Enabled = false;
            }
        }
        else
        {
            txtDateOfInsp1.Text = string.Empty;
            rfvtxtDateOfInsp1.Enabled = false;
            rfvddlSiteInsp.Enabled = false;
            txtOtherAgency.Text = string.Empty;
            txtOtherAgency.Enabled = false;
            rfvOtherAgency.Enabled = false;
            txtDateOfInsp1.Enabled = false;
            ImgBtnDateOfInsp1.Enabled = false;
            ddlSiteInsp.Enabled = false;
        }
    }
    protected void ddlRainwaterharvesting_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlRainwaterharvesting.SelectedValue.ToString() == "1")
        {
            CustomValidator1.Enabled = true;
            chklistTypeOfARStruct.Enabled = true;
            txtNoOfStruct.Enabled = true;
            ddlWithinOutSidePremises.Enabled = true;
            txtQuantOfRecharge.Enabled = true;
            ddlGeoPhotoRechargeStruc.Enabled = true;
            FileUploadRainwaterharvesting.Enabled = true;
            btnUploadRainwaterharvesting.Enabled = true;
            gvRainwaterharvesting.Enabled = true;
            // rfvddltypestr.Enabled = true;
            rfvtxtNoOfStruct.Enabled = true;
            rfvddlWithinOutSidePremises.Enabled = true;
            rfvtxtQuantOfRecharge.Enabled = true;
            rfvddlGeoPhotoRechargeStruc.Enabled = true;
            revtxtNoOfStruct.Enabled = true;
            revtxtNoOfStruct.ValidationGroup = "SelfCompliance";
            revtxtQuantOfRecharge.ValidationGroup = "SelfCompliance";
            revtxtQuantOfRecharge.Enabled = true;
        }
        else
        {
            for (int j = 0; j < chklistTypeOfARStruct.Items.Count; j++)
            {
                chklistTypeOfARStruct.Items[j].Selected = false;
            }
            CustomValidator1.Enabled = false;
            chklistTypeOfARStruct.Enabled = false;

            txtNoOfStruct.Enabled = false;
            txtNoOfStruct.Text = string.Empty;
            ddlWithinOutSidePremises.Enabled = false;
            ddlWithinOutSidePremises.SelectedIndex = 0;
            txtQuantOfRecharge.Enabled = false;
            txtQuantOfRecharge.Text = string.Empty;
            ddlGeoPhotoRechargeStruc.Enabled = false;
            ddlGeoPhotoRechargeStruc.SelectedIndex = 0;
            FileUploadRainwaterharvesting.Enabled = false;
            btnUploadRainwaterharvesting.Enabled = false;
            gvRainwaterharvesting.Enabled = false;
            gvRainwaterharvesting.DataSource = null;
            gvRainwaterharvesting.DataBind();
            // rfvddltypestr.Enabled = false;
            rfvtxtNoOfStruct.Enabled = false;
            rfvddlWithinOutSidePremises.Enabled = false;
            rfvtxtQuantOfRecharge.Enabled = false;
            rfvddlGeoPhotoRechargeStruc.Enabled = false;
            revtxtNoOfStruct.Enabled = false;
            revtxtNoOfStruct.ValidationGroup = "";
            revtxtQuantOfRecharge.ValidationGroup = "";
            revtxtQuantOfRecharge.Enabled = false;
        }
    }
    protected void ddlSTPETP_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSTPETP.SelectedValue.ToString() == "1")
        {
            txtNoSTPETP.Enabled = true;
            txtCapSTPETP.Enabled = true;
            txtQuantumTWW.Enabled = true;
            ddlgeophotoSTPETP.Enabled = true;

            txtIndProcess.Enabled = true;
            txtGreenbelt.Enabled = true;
            txtOtherUse.Enabled = true;
        }
        else
        {
            txtNoSTPETP.Enabled = false;
            txtCapSTPETP.Enabled = false;
            txtQuantumTWW.Enabled = false;
            txtNoSTPETP.Text = string.Empty;
            txtCapSTPETP.Text = string.Empty;
            txtQuantumTWW.Text = string.Empty;
            ddlgeophotoSTPETP.Enabled = false;
            ddlgeophotoSTPETP.SelectedIndex = 0;
            txtIndProcess.Enabled = false;
            txtGreenbelt.Enabled = false;
            txtOtherUse.Enabled = false;
            txtIndProcess.Text = string.Empty;
            txtGreenbelt.Text = string.Empty;
            txtOtherUse.Text = string.Empty;
        }
    }

    protected void ddlAnnualCali_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAnnualCali.SelectedValue.ToString() == "1")
        {
            FileUploadAnnualCalibration.Enabled = true;
            btnFileUploadAnnualCalibration.Enabled = true;
            gvAnnualCalibration.Enabled = true;
        }
        else
        {
            FileUploadAnnualCalibration.Enabled = false;
            btnFileUploadAnnualCalibration.Enabled = false;
            gvAnnualCalibration.Enabled = false;
        }
    }
    protected void ddlGeoPhotoRechargeStruc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGeoPhotoRechargeStruc.SelectedValue.ToString() == "1")
        {
            FileUploadRainwaterharvesting.Enabled = true;
            btnUploadRainwaterharvesting.Enabled = true;
            gvRainwaterharvesting.Enabled = true;
        }
        else
        {
            FileUploadRainwaterharvesting.Enabled = false;
            btnUploadRainwaterharvesting.Enabled = false;
            gvRainwaterharvesting.Enabled = false;
        }
    }
    protected void ddlgeophotopiewell_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlgeophotopiewell.SelectedValue.ToString() == "1")
        {
            FileUploadGroundwaterMonitoring.Enabled = true;
            btnUploadGroundwaterMonitoring.Enabled = true;
            gvGroundwaterMonitoring.Enabled = true;
        }
        else
        {
            FileUploadGroundwaterMonitoring.Enabled = false;
            btnUploadGroundwaterMonitoring.Enabled = false;
            gvGroundwaterMonitoring.Enabled = false;
        }
    }

    protected void ddlGroundWaterMonitoringData_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGroundWaterMonitoringData.SelectedValue.ToString() == "1")
        {
            FileUploadGroundWaterMonitoringData.Enabled = true;
            btnUploadGroundWaterMonitoringData.Enabled = true;
            gvGroundWaterMonitoringData.Enabled = true;
        }
        else
        {
            FileUploadGroundWaterMonitoringData.Enabled = false;
            btnUploadGroundWaterMonitoringData.Enabled = false;
            gvGroundWaterMonitoringData.Enabled = false;
        }
    }
    protected void ddlSiteInsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSiteInsp.SelectedValue.ToString() == "1")
        {
            FileUploadSiteInspection.Enabled = true;
            btnUploadSiteInspection.Enabled = true;
            gvSiteInspection.Enabled = true;
        }
        else
        {
            FileUploadSiteInspection.Enabled = false;
            btnUploadSiteInspection.Enabled = false;
            gvSiteInspection.Enabled = false;
        }
    }
    protected void ddlgeophotostruct_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlgeophotostruct.SelectedValue.ToString() == "1")
        {
            FileUploadgeophotostruct.Enabled = true;
            btnUploadgeophotostruct.Enabled = true;
            gvgeophotostruct.Enabled = true;
        }
        else
        {
            FileUploadgeophotostruct.Enabled = false;
            btnUploadgeophotostruct.Enabled = false;
            gvgeophotostruct.Enabled = false;
        }
    }

    protected void ddlGeoPhotoFittedWM_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlGeoPhotoFittedWM.SelectedValue.ToString() == "1")
        {
            FileUploadGeoPhotowellfitted.Enabled = true;
            btnUploadGeoPhotowellfitted.Enabled = true;
            gvGeoPhotowellfitted.Enabled = true;
        }
        else
        {
            FileUploadGeoPhotowellfitted.Enabled = false;
            btnUploadGeoPhotowellfitted.Enabled = false;
            gvGeoPhotowellfitted.Enabled = false;
        }
    }
    protected void ddlGWQReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGWQReport.SelectedValue.ToString() == "1")
        {
            FileUploadWQRSubmitted.Enabled = true;
            btnUploadWQRSubmitted.Enabled = true;
            gvWQRSubmitted.Enabled = true;
        }
        else
        {
            FileUploadWQRSubmitted.Enabled = false;
            btnUploadWQRSubmitted.Enabled = false;
            gvWQRSubmitted.Enabled = false;
        }
    }

    protected void ddlMineSeepage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMineSeepage.SelectedValue.ToString() == "1")
        {
            FileUploadMineseepage.Enabled = true;
            btnUploadMineseepage.Enabled = true;
            gvMineseepage.Enabled = true;
        }
        else
        {
            FileUploadMineseepage.Enabled = false;
            btnUploadMineseepage.Enabled = false;
            gvMineseepage.Enabled = false;
        }
    }
    protected void ddlgeophotoSTPETP_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlgeophotoSTPETP.SelectedValue.ToString() == "1")
        {
            FileUploadSTPETP.Enabled = true;
            btnUploadSTPETP.Enabled = true;
            gvSTPETP.Enabled = true;
        }
        else
        {
            FileUploadSTPETP.Enabled = false;
            btnUploadSTPETP.Enabled = false;
            gvSTPETP.Enabled = false;
        }
    }
    protected void ddlAnyVariation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAnyVariation.SelectedValue.ToString() == "3" || ddlAnyVariation.SelectedValue.ToString() == "")
        {
            rfvtxtQtyVariDay.Enabled = false;
            rfvtxtQtyVariYear.Enabled = false;
            txtQtyVariDay.Enabled = false;
            txtQtyVariDay.Text = string.Empty;
            txtQtyVariYear.Enabled = false;
            txtQtyVariYear.Text = string.Empty;


        }
        else
        {
            rfvtxtQtyVariDay.Enabled = true;
            rfvtxtQtyVariYear.Enabled = true;


        }
    }
    #endregion
    
    protected void lnkAddPiezometerDetail_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;

                Server.Transfer("~/ExternalUser/Piezometer/PiezometerDetail.aspx");


            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
            }
        }
    }



    protected void lnkTelemetryDetail_Click(object sender, EventArgs e)
    {


        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;           
                Server.Transfer("~/ExternalUser/Telemetry/TelemetryUserLoginDetail.aspx");

            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;

            }
        }
    }

    
}

