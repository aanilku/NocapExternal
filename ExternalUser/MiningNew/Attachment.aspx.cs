using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Drawing;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Threading;
using NOCAP.BLL.Master;
using NOCAP.BLL.Mining.New.SADApplication;

public partial class ExternalUser_Mining_Attachment : System.Web.UI.Page
{
    string strPageName = "MINAttachment";
    string strActionName = "";
    string strStatus = "";
    long lngMinSubmitAppCode;
    decimal GroundWaterRequirement0KLD = 0;
    decimal GroundWaterRequirement10KLD = 10;
    decimal GroundWaterRequirement100KLD = 100;
    public long MinSubmitAppCode
    {
        get
        {
            return lngMinSubmitAppCode;
        }
        set
        {
            lngMinSubmitAppCode = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //lblMessage.Text = "";
        if (!IsPostBack)
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;


            if (NOCAPExternalUtility.FillDropDownReferralLetterType(ref ddlReferralLetter) != 1) { lblMessageReferralLetter.Text = "Problem in State Population!"; }
            else { ddlReferralLetter.Items[0].Value = "0"; }
            try
            {
                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblModeFrom");
                        if (SourceLabel != null) { lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); }
                    }
                }
                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblMiningApplicationCodeFrom");
                        if (SourceLabel != null) { lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    lblApplicationCode.Text = lblMiningApplicationCodeFrom.Text;


                    // Start new code for OTP Verify
                    NOCAPExternalUtility.FillDropDownConsultant(ref ddlConsultant);
                    OTPVerifyEnavleDesable();
                    // End new code for OTP Verify



                    // BindMiningNewApplicationAttachmentSitePlanDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningNewApplicationAttachmentTORECApprovalLetterDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningNewApplicationAttachmentApproveMiningPlanDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    //   BindMiningNewApplicationAttachmentTopoSketchDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    //   BindMiningNewApplicationAttachmentOwnerShipOfLandDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    //  BindMiningNewApplicationAttachmentAvailabilityOfSurfaceWaterDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningNewApplicationAttachmentGroundWaterFlowDirectionDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    // BindMiningNewApplicationAttachmentProposedUtilizationOfPumpedWaterDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningNewApplicationAttachmentGroundWaterRegimeMapDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    //  BindMiningNewApplicationAttachmentGroundWaterObservationWellsDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningNewApplicationAttachmentGQualityOfGroundWaterInAreaDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningNewApplicationAttachmentGroundWaterAvailabilityDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningNewApplicationAttachmentRainWaterharvestingDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningNewApplicationAttachmentUndertalkingDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    //  BindMiningNewApplicationAttachmentRefferalLetterDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningNewApplicationAttachmentExtraAttachmentsDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningNewApplicationAttachmentBharatKoshRecieptDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningNewApplicationAttachmentSignedDocDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindWetlandAreaAttachmentDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindPenaltyAttachmentDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
            }
        }
    }

    #region Private Function
    //private void BindMiningNewApplicationAttachmentSitePlanDetails(long lngA_ApplicationCode)
    //{
    //    try
    //    {
    //        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);            
    //        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
    //        arr_MiningNewApplicationAttachmentList = obj_MiningNewApplication.GetSitePlanAttachmentList();            
    //        Tab16.Text = "*Site Plan (" + arr_MiningNewApplicationAttachmentList.Length.ToString() + ")";
    //        gvSitePlan.DataSource = arr_MiningNewApplicationAttachmentList;
    //        gvSitePlan.DataBind();
    //    }
    //    catch (Exception)
    //    {
    //        Response.Redirect("~/ExternalErrorPage.aspx", false);
    //    }
    //}
    private void BindMiningNewApplicationAttachmentTORECApprovalLetterDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
            arr_MiningNewApplicationAttachmentList = obj_MiningNewApplication.GetTORECApprovalLetterAttachmentList();
            decimal decTotalGroundWaterRequirement = Convert.ToDecimal((obj_MiningNewApplication.GWREquiredThroughAbstractStructure == null ? 0 : obj_MiningNewApplication.GWREquiredThroughAbstractStructure) + (obj_MiningNewApplication.GWRequiredThroughMiningSeeping == null ? 0 : obj_MiningNewApplication.GWRequiredThroughMiningSeeping));

            if (decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD)
                lblTORApprovalLetter2.Visible = true;

            else if (decTotalGroundWaterRequirement > GroundWaterRequirement10KLD)
                lblTORApprovalLetter2.Visible = true;
            else
                lblTORApprovalLetter2.Visible = false;
            lblTORApprovalLetter.Text = HttpUtility.HtmlEncode("TOR/EC/Approval Letter (" + arr_MiningNewApplicationAttachmentList.Length.ToString() + ")");
            gvTORECApprovalLetter.DataSource = arr_MiningNewApplicationAttachmentList;
            gvTORECApprovalLetter.DataBind();

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindMiningNewApplicationAttachmentApproveMiningPlanDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
            arr_MiningNewApplicationAttachmentList = obj_MiningNewApplication.GetApproveMiningPlanAttachmentList();
            decimal decTotalGroundWaterRequirement = Convert.ToDecimal((obj_MiningNewApplication.GWREquiredThroughAbstractStructure == null ? 0 : obj_MiningNewApplication.GWREquiredThroughAbstractStructure) + (obj_MiningNewApplication.GWRequiredThroughMiningSeeping == null ? 0 : obj_MiningNewApplication.GWRequiredThroughMiningSeeping));

            if (decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD)
                lblApprovedMiningPlan2.Visible = true;

            else if (decTotalGroundWaterRequirement > GroundWaterRequirement10KLD)
                lblApprovedMiningPlan2.Visible = true;
            else
                lblApprovedMiningPlan2.Visible = false;
            lblApprovedMiningPlan.Text = HttpUtility.HtmlEncode("Approved Mining Plan (" + arr_MiningNewApplicationAttachmentList.Length.ToString() + ")");
            gvApprovedMiningPlan.DataSource = arr_MiningNewApplicationAttachmentList;
            gvApprovedMiningPlan.DataBind();

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    //private void BindMiningNewApplicationAttachmentTopoSketchDetails(long lngA_ApplicationCode)
    //{
    //    try
    //    {
    //        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
    //        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
    //        arr_MiningNewApplicationAttachmentList = obj_MiningNewApplication.GetTopoSketchAttachmentList();
    //        Tab3.Text = HttpUtility.HtmlEncode("Toposketch (" + arr_MiningNewApplicationAttachmentList.Length.ToString() + ")");
    //        gvToposketch.DataSource = arr_MiningNewApplicationAttachmentList;
    //        gvToposketch.DataBind();

    //    }
    //    catch (Exception)
    //    {
    //        Response.Redirect("~/ExternalErrorPage.aspx", false);
    //    }
    //}

    //private void BindMiningNewApplicationAttachmentOwnerShipOfLandDetails(long lngA_ApplicationCode)
    //{
    //    try
    //    {
    //        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
    //        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
    //        arr_MiningNewApplicationAttachmentList = obj_MiningNewApplication.GetOwnerShipOfLandAttachmentList();
    //        Tab4.Text = "Ownership of Land (" + arr_MiningNewApplicationAttachmentList.Length.ToString() + ")";
    //        gvDocumentsofOwnership.DataSource = arr_MiningNewApplicationAttachmentList;
    //        gvDocumentsofOwnership.DataBind();
    //    }
    //    catch (Exception)
    //    {
    //        Response.Redirect("~/ExternalErrorPage.aspx", false);
    //    }
    //}

    //private void BindMiningNewApplicationAttachmentAvailabilityOfSurfaceWaterDetails(long lngA_ApplicationCode)
    //{
    //    try
    //    {
    //        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
    //        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
    //        arr_MiningNewApplicationAttachmentList = obj_MiningNewApplication.GetSurfaceWaterAvailabilityAttachmentList();
    //        Tab5.Text = "Source of Availability of Surface Water (" + arr_MiningNewApplicationAttachmentList.Length.ToString() + ")";
    //        gvSourceofAvailabilityofSurfaceWater.DataSource = arr_MiningNewApplicationAttachmentList;
    //        gvSourceofAvailabilityofSurfaceWater.DataBind();
    //    }
    //    catch (Exception)
    //    {
    //        Response.Redirect("~/ExternalErrorPage.aspx", false);
    //    }
    //}
    private void BindMiningNewApplicationAttachmentGroundWaterFlowDirectionDetails(long lngA_ApplicationCode)
    {
        //try
        //{
        //    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
        //    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
        //    arr_MiningNewApplicationAttachmentList = obj_MiningNewApplication.GetGroundWaterflowDirectionAttachmentList();
        //    decimal decTotalGroundWaterRequirement = Convert.ToDecimal((obj_MiningNewApplication.GWREquiredThroughAbstractStructure == null ? 0 : obj_MiningNewApplication.GWREquiredThroughAbstractStructure) + (obj_MiningNewApplication.GWRequiredThroughMiningSeeping == null ? 0 : obj_MiningNewApplication.GWRequiredThroughMiningSeeping));
        //    if (decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD)
        //        Tab6.Text = HttpUtility.HtmlEncode("Groundwater Flow Direction Map (" + arr_MiningNewApplicationAttachmentList.Length.ToString() + ")");
        //    else if (decTotalGroundWaterRequirement > GroundWaterRequirement10KLD)
        //        Tab6.Text = HttpUtility.HtmlEncode("*Groundwater Flow Direction Map (" + arr_MiningNewApplicationAttachmentList.Length.ToString() + ")");
        //    else
        //        Tab6.Text = HttpUtility.HtmlEncode("Groundwater Flow Direction Map (" + arr_MiningNewApplicationAttachmentList.Length.ToString() + ")");
        //    gvGWFlowDirectionMap.DataSource = arr_MiningNewApplicationAttachmentList;
        //    gvGWFlowDirectionMap.DataBind();
        //}
        //catch (Exception)
        //{
        //    Response.Redirect("~/ExternalErrorPage.aspx", false);
        //}
    }

    //private void BindMiningNewApplicationAttachmentProposedUtilizationOfPumpedWaterDetails(long lngA_ApplicationCode)
    //{
    //    try
    //    {
    //        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
    //        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
    //        arr_MiningNewApplicationAttachmentList = obj_MiningNewApplication.GetProposedUtilizationPumpedWaterAttachmentList();
    //        Tab7.Text = HttpUtility.HtmlEncode("*Proposed Utilization of Pumped Water (" + arr_MiningNewApplicationAttachmentList.Length.ToString() + ")");
    //        gvProUtiPumpedWater.DataSource = arr_MiningNewApplicationAttachmentList;
    //        gvProUtiPumpedWater.DataBind();
    //    }
    //    catch (Exception)
    //    {
    //        Response.Redirect("~/ExternalErrorPage.aspx", false);
    //    }
    //}
    private void BindMiningNewApplicationAttachmentGroundWaterRegimeMapDetails(long lngA_ApplicationCode)
    {
        //try
        //{
        //    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
        //    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
        //    arr_MiningNewApplicationAttachmentList = obj_MiningNewApplication.GetMonitorGroundWaterRegimeMapAttachmentList();
        //    decimal decTotalGroundWaterRequirement = Convert.ToDecimal((obj_MiningNewApplication.GWREquiredThroughAbstractStructure == null ? 0 : obj_MiningNewApplication.GWREquiredThroughAbstractStructure) + (obj_MiningNewApplication.GWRequiredThroughMiningSeeping == null ? 0 : obj_MiningNewApplication.GWRequiredThroughMiningSeeping));
        //    if (decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD)
        //        Tab8.Text = HttpUtility.HtmlEncode("Ground Water Regime Map (" + arr_MiningNewApplicationAttachmentList.Length.ToString() + ")");
        //    else if (decTotalGroundWaterRequirement > GroundWaterRequirement10KLD)
        //        Tab8.Text = HttpUtility.HtmlEncode("*Ground Water Regime Map (" + arr_MiningNewApplicationAttachmentList.Length.ToString() + ")");
        //    else
        //        Tab8.Text = HttpUtility.HtmlEncode("Ground Water Regime Map (" + arr_MiningNewApplicationAttachmentList.Length.ToString() + ")");
        //    gvMonitorGWRegime.DataSource = arr_MiningNewApplicationAttachmentList;
        //    gvMonitorGWRegime.DataBind();
        //}
        //catch (Exception)
        //{
        //    Response.Redirect("~/ExternalErrorPage.aspx", false);
        //}
    }

    //private void BindMiningNewApplicationAttachmentGroundWaterObservationWellsDetails(long lngA_ApplicationCode)
    //{
    //    try
    //    {
    //        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
    //        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
    //        arr_MiningNewApplicationAttachmentList = obj_MiningNewApplication.GetMonitorGroundWaterRegimeObservationWellAttachmentList();

    //        Tab9.Text =HttpUtility.HtmlEncode( "GW Level of Observation Wells/Piezometer (" + arr_MiningNewApplicationAttachmentList.Length.ToString() + ")");
    //        gvGWLevelObservation.DataSource = arr_MiningNewApplicationAttachmentList;
    //        gvGWLevelObservation.DataBind();
    //    }
    //    catch (Exception)
    //    {
    //        Response.Redirect("~/ExternalErrorPage.aspx", false);
    //    }
    //}
    private void BindMiningNewApplicationAttachmentGQualityOfGroundWaterInAreaDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
            arr_MiningNewApplicationAttachmentList = obj_MiningNewApplication.GetMonitorGroundWaterRegimeGQsuroudingsAttachmentList();
            decimal decTotalGroundWaterRequirement = Convert.ToDecimal((obj_MiningNewApplication.GWREquiredThroughAbstractStructure == null ? 0 : obj_MiningNewApplication.GWREquiredThroughAbstractStructure) + (obj_MiningNewApplication.GWRequiredThroughMiningSeeping == null ? 0 : obj_MiningNewApplication.GWRequiredThroughMiningSeeping));
            if (decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD)
                lblGeneralQualityGW2.Visible = true;

            else if (decTotalGroundWaterRequirement > GroundWaterRequirement10KLD)
                lblGeneralQualityGW2.Visible = true;
            else
                lblGeneralQualityGW2.Visible = false;
            lblGeneralQualityGW.Text = HttpUtility.HtmlEncode("General Quality of GW in Area (" + arr_MiningNewApplicationAttachmentList.Length.ToString() + ")");
            gvGQofGWInArea.DataSource = arr_MiningNewApplicationAttachmentList;
            gvGQofGWInArea.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void BindMiningNewApplicationAttachmentGroundWaterAvailabilityDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
            arr_MiningNewApplicationAttachmentList = obj_MiningNewApplication.GetGroundwaterAvailabilityAttachmentList();
            decimal decTotalGroundWaterRequirement = Convert.ToDecimal((obj_MiningNewApplication.GWREquiredThroughAbstractStructure == null ? 0 : obj_MiningNewApplication.GWREquiredThroughAbstractStructure) + (obj_MiningNewApplication.GWRequiredThroughMiningSeeping == null ? 0 : obj_MiningNewApplication.GWRequiredThroughMiningSeeping));

            if (decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD)
            {
                lblHydrogeologicalReport2.Visible = false;
                rowIAR.Visible = false;
            }
            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement100KLD))
            {
                lblHydrogeologicalReport2.Visible = false;
                rowIAR.Visible = false;
            }
            else
            {
                
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_miningNewSADApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
                if (obj_miningNewSADApplication.WaterQuality != NOCAP.BLL.Common.CommonEnum.WaterQualityOption.RannKuchch)
                {
                    lblHydrogeologicalReport2.Visible = true;
                    rowIAR.Visible = true;
                }
                else
                {
                    lblHydrogeologicalReport2.Visible = false;
                    rowIAR.Visible = false;
                }
            }

            lblHydrogeologicalReport.Text = HttpUtility.HtmlEncode("Comprehensive Report (" + arr_MiningNewApplicationAttachmentList.Length.ToString() + ")");
            gvGroundwaterAvailability.DataSource = arr_MiningNewApplicationAttachmentList;
            gvGroundwaterAvailability.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindMiningNewApplicationAttachmentRainWaterharvestingDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
            arr_MiningNewApplicationAttachmentList = obj_MiningNewApplication.GetRainwaterHarvestingAttachmentList();
            decimal decTotalGroundWaterRequirement = Convert.ToDecimal((obj_MiningNewApplication.GWREquiredThroughAbstractStructure == null ? 0 : obj_MiningNewApplication.GWREquiredThroughAbstractStructure) + (obj_MiningNewApplication.GWRequiredThroughMiningSeeping == null ? 0 : obj_MiningNewApplication.GWRequiredThroughMiningSeeping));
            if (decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD)
                lblRainWaterHarvesting2.Visible = true;

            else if (decTotalGroundWaterRequirement > GroundWaterRequirement10KLD)
                lblRainWaterHarvesting2.Visible = true;
            else
                lblRainWaterHarvesting2.Visible = false;
            lblRainWaterHarvesting.Text = HttpUtility.HtmlEncode("Rain Water Harvesting/Artificial Recharge proposal (" + arr_MiningNewApplicationAttachmentList.Length.ToString() + ")");
            gvRainwaterHarvesting.DataSource = arr_MiningNewApplicationAttachmentList;
            gvRainwaterHarvesting.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindMiningNewApplicationAttachmentUndertalkingDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
            arr_MiningNewApplicationAttachmentList = obj_MiningNewApplication.GetUndertakingAttachmentList();
            decimal decTotalGroundWaterRequirement = Convert.ToDecimal((obj_MiningNewApplication.GWREquiredThroughAbstractStructure == null ? 0 : obj_MiningNewApplication.GWREquiredThroughAbstractStructure) + (obj_MiningNewApplication.GWRequiredThroughMiningSeeping == null ? 0 : obj_MiningNewApplication.GWRequiredThroughMiningSeeping));
            if (decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD)
                lblAuthorizationLetter2.Visible = true;

            else if (decTotalGroundWaterRequirement > GroundWaterRequirement10KLD)
                lblAuthorizationLetter2.Visible = true;
            else
                lblAuthorizationLetter2.Visible = false;
            lblAuthorizationLetter.Text = HttpUtility.HtmlEncode("Authorization Letter (" + arr_MiningNewApplicationAttachmentList.Length.ToString() + ")");
            gvUndertaking.DataSource = arr_MiningNewApplicationAttachmentList;
            gvUndertaking.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    //private void BindMiningNewApplicationAttachmentRefferalLetterDetails(long lngA_ApplicationCode, int int_ReferralLetterAttachCode = 0)
    //{
    //    try
    //    {
    //        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MIningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
    //        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
    //        arr_MiningNewApplicationAttachmentList = obj_MIningNewApplication.GetCopyOfReferralLetterAttachmentList(int_ReferralLetterAttachCode, NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment.SortingFieldForAttachment.NoSorting);
    //        gvReferralLetterAttachment.DataSource = arr_MiningNewApplicationAttachmentList;
    //        gvReferralLetterAttachment.DataBind();

    //        Tab14.Text = HttpUtility.HtmlEncode("*Referal Letter (" + AttachmentsCountOfReferalLetter() + ")");
    //    }
    //    catch (Exception)
    //    {
    //        Response.Redirect("~/ExternalErrorPage.aspx", false);
    //    }
    //}
    private void BindMiningNewApplicationAttachmentExtraAttachmentsDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
            arr_MiningNewApplicationAttachmentList = obj_MiningNewApplication.GetExtraAttachmentList();
            lblExtraAttachment2.Visible = false;
            lblExtraAttachment.Text = HttpUtility.HtmlEncode("Extra Attachment (" + arr_MiningNewApplicationAttachmentList.Length.ToString() + ")");
            gvExtra.DataSource = arr_MiningNewApplicationAttachmentList;
            gvExtra.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void clearMessage()
    {
        lblMessageApprovedMiningPlan.Text = HttpUtility.HtmlEncode("");
        txtApprovedMiningPlan.Text = HttpUtility.HtmlEncode("");
        lblMessageRainwaterHarvesting.Text = HttpUtility.HtmlEncode("");
        txtRainwaterHarvesting.Text = HttpUtility.HtmlEncode("");
        lblMessageGroundwaterAvailability.Text = HttpUtility.HtmlEncode("");
        txtgvGroundwaterAvailability.Text = HttpUtility.HtmlEncode("");
        lblMessageWetlanArea.Text = HttpUtility.HtmlEncode("");
        txtWetlandArea.Text = HttpUtility.HtmlEncode("");
        lblMessageBharatKoshReciept.Text = HttpUtility.HtmlEncode("");
        txtBharatKoshReciept.Text = HttpUtility.HtmlEncode("");
        lblMessageAplicationSignatureSeal.Text = HttpUtility.HtmlEncode("");
        txtApplicationSignatureSeal.Text = HttpUtility.HtmlEncode("");

        lblMessageExtra.Text = HttpUtility.HtmlEncode("");
        txtExtraAttachment.Text = HttpUtility.HtmlEncode("");





        lblMessageTORECApprovalLetter.Text = HttpUtility.HtmlEncode("");
        lblMessageSitePlan.Text = HttpUtility.HtmlEncode("");

        lblMessageToposketch.Text = HttpUtility.HtmlEncode("");
        lblMessageDocumentsofOwnership.Text = HttpUtility.HtmlEncode("");
        lblMessageSourceofAvailabilityofSurfaceWater.Text = HttpUtility.HtmlEncode("");
        lblMessageGWFlowDirectionMap.Text = HttpUtility.HtmlEncode("");
        lblMessageProUtiPumpedWater.Text = HttpUtility.HtmlEncode("");
        lblMessageMonitorGWRegime.Text = HttpUtility.HtmlEncode("");
        lblMessageGWLevelObservation.Text = HttpUtility.HtmlEncode("");
        lblMessageGQofGWInArea.Text = HttpUtility.HtmlEncode("");
        lblMessageGroundwaterAvailability.Text = HttpUtility.HtmlEncode("");

        lblMessageUndertaking.Text = HttpUtility.HtmlEncode("");
        lblMessageReferralLetter.Text = HttpUtility.HtmlEncode("");


    }
    private void ClearAttachmentFileName()
    {
        txtTORECApprovalLetterAttachment.Text = HttpUtility.HtmlEncode("");
        txtSitePlanAttachment.Text = HttpUtility.HtmlEncode("");
        txtApprovedMiningPlan.Text = HttpUtility.HtmlEncode("");
        txtToposketch.Text = HttpUtility.HtmlEncode("");
        txtDocumentsofOwnership.Text = HttpUtility.HtmlEncode("");
        txtSourceofAvailabilityofSurfaceWater.Text = HttpUtility.HtmlEncode("");
        txtGWFlowDirectionMap.Text = HttpUtility.HtmlEncode("");
        txtProUtiPumpedWater.Text = HttpUtility.HtmlEncode("");
        txtMonitorGWRegime.Text = HttpUtility.HtmlEncode("");
        txtGWLevelObservation.Text = HttpUtility.HtmlEncode("");
        txtGQofGWInArea.Text = HttpUtility.HtmlEncode("");
        txtgvGroundwaterAvailability.Text = HttpUtility.HtmlEncode("");
        txtRainwaterHarvesting.Text = HttpUtility.HtmlEncode("");
        txtUndertaking.Text = HttpUtility.HtmlEncode("");
        txtReferralLetter.Text = HttpUtility.HtmlEncode("");
        txtExtraAttachment.Text = HttpUtility.HtmlEncode("");



    }
    //private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
    //{
    //    // Get the subdirectories for the specified directory.
    //    DirectoryInfo dir = new DirectoryInfo(sourceDirName);
    //    DirectoryInfo[] dirs = dir.GetDirectories();

    //    if (!dir.Exists)
    //    {
    //        throw new DirectoryNotFoundException(
    //            "Source directory does not exist or could not be found: "
    //            + sourceDirName);
    //    }

    //    // If the destination directory doesn't exist, create it. 
    //    if (!Directory.Exists(destDirName))
    //    {
    //        Directory.CreateDirectory(destDirName);
    //    }

    //    // Get the files in the directory and copy them to the new location.
    //    FileInfo[] files = dir.GetFiles();
    //    foreach (FileInfo file in files)
    //    {
    //        string temppath = Path.Combine(destDirName, file.Name);
    //        file.CopyTo(temppath, false);
    //    }

    //    // If copying subdirectories, copy them and their contents to new location. 
    //    if (copySubDirs)
    //    {
    //        foreach (DirectoryInfo subdir in dirs)
    //        {
    //            string temppath = Path.Combine(destDirName, subdir.Name);
    //            DirectoryCopy(subdir.FullName, temppath, copySubDirs);
    //        }
    //    }
    //}
    private int AttachmentNumberLimit()
    {
        try
        {
            NOCAP.BLL.Common.AttachmentLimit obj_attachmentNoLimit = new NOCAP.BLL.Common.AttachmentLimit();
            int AttachmentNumber = obj_attachmentNoLimit.NumberOfAttachment;
            return AttachmentNumber;
        }
        catch
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
            return 0;
        }
        finally
        {
        }
    }
    
    private int AttachmentSizeLimitForHydro()
    {
        try
        {
            NOCAP.BLL.Common.AttachmentLimit obj_attachmentLimit = new NOCAP.BLL.Common.AttachmentLimit();
            int AttachmentSize = 1048576 * 20;
            return AttachmentSize;
        }
        catch
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
            return 0;
        }
        finally
        {
        }
    }
    //private int AttachmentsCountOfReferalLetter()
    //{
    //    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
    //    int Count = 0;
    //    string SelectedValue = ddlReferralLetter.SelectedValue;
    //    for (int i = 1; i < ddlReferralLetter.Items.Count; i++)
    //    {
    //        ddlReferralLetter.SelectedIndex = i;
    //        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADReferralLetter obj_MiningNewReferralLetter = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADReferralLetter(Convert.ToInt32(lblMiningApplicationCodeFrom.Text), Convert.ToInt32(ddlReferralLetter.SelectedValue));
    //        Count = Count + obj_MiningNewApplication.GetCopyOfReferralLetterAttachmentList(obj_MiningNewReferralLetter.ReferralLetterAttachCode, NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment.SortingFieldForAttachment.NoSorting).Length;
    //    }
    //    ddlReferralLetter.SelectedValue = SelectedValue;
    //    return Count;
    //}
    private void BindMiningNewApplicationAttachmentBharatKoshRecieptDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_miningNewApplicationAttachmentList;
            arr_miningNewApplicationAttachmentList = obj_miningNewApplication.GetBharatKoshRecieptAttachmentList();
            gvBharatKoshReciept.DataSource = arr_miningNewApplicationAttachmentList;
            gvBharatKoshReciept.DataBind();

            lblBharatKosh.Text = HttpUtility.HtmlEncode("Bharat Kosh Reciept Attachment (" + arr_miningNewApplicationAttachmentList.Length.ToString() + ")");
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindWetlandAreaAttachmentDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_miningNewApplicationAttachmentList;
            arr_miningNewApplicationAttachmentList = obj_miningNewApplication.GetWetlandAreaAttachmentList();
            gvWetlandArea.DataSource = arr_miningNewApplicationAttachmentList;
            gvWetlandArea.DataBind();
            switch (obj_miningNewApplication.WetLandArea)
            {
                case NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WetLandAreaYesNo.Yes:
                    lblWetlandArea2.Visible = true;
                    break;
                case NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WetLandAreaYesNo.No:
                    lblWetlandArea2.Visible = false;
                    break;
                case NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WetLandAreaYesNo.NotDefine:
                    lblWetlandArea2.Visible = false;
                    break;
            }

            lblWetlandArea.Text = HttpUtility.HtmlEncode("Approval from Wetland Authority (" + arr_miningNewApplicationAttachmentList.Length.ToString() + ")");
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindPenaltyAttachmentDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_miningNewApplicationAttachmentList;
            arr_miningNewApplicationAttachmentList = obj_miningNewApplication.GetPenaltyAttachmentList();
            gvPenalty.DataSource = arr_miningNewApplicationAttachmentList;
            gvPenalty.DataBind();
            lblPenalty2.Visible = false;
            lblPenalty.Text = HttpUtility.HtmlEncode("Penalty (" + arr_miningNewApplicationAttachmentList.Length.ToString() + ")");
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void BindMiningNewApplicationAttachmentSignedDocDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_miningNewApplicationAttachmentList;
            arr_miningNewApplicationAttachmentList = obj_miningNewApplication.GetSignedDocAttachmentList();
            gvApplicationSignatureSeal.DataSource = arr_miningNewApplicationAttachmentList;
            gvApplicationSignatureSeal.DataBind();
            lblSigneddoc2.Visible = true;
            lblSigneddoc.Text = HttpUtility.HtmlEncode("Aplication with Signature and Seal Attachment (" + arr_miningNewApplicationAttachmentList.Length.ToString() + ")");
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    #endregion

    protected void ViewFile(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
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
                    long lng_miningNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINSADAppDownloadFiles(lng_miningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
            }
        }
    }

    #region Button Click
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        strActionName = "Submit";

        long lng_submittedApplicationCode = 0;
        clearMessage();
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {

                string ErrorMessage = string.Empty;
                if (lblApprovedMiningPlan2.Visible)
                {
                    if (obj_MiningNewApplication.GetApproveMiningPlanAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Approved Mining Plan" : ErrorMessage + ", Approved Mining Plan"; }
                }
                if (lblRainWaterHarvesting2.Visible)
                {
                    if (obj_MiningNewApplication.GetRainwaterHarvestingAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Rain Water Harvesting/Artificial Recharge proposal" : ErrorMessage + ",Rain Water Harvesting/Artificial Recharge proposal"; }
                }
                if (lblHydrogeologicalReport2.Visible)
                {
                    if (obj_MiningNewApplication.GetGroundwaterAvailabilityAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Comprehensive Report" : ErrorMessage + ",Comprehensive Report"; }
                }

                if (lblWetlandArea2.Visible)
                {
                    if (obj_MiningNewApplication.GetWetlandAreaAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Approval from Wetland Authority" : ErrorMessage + ",Approval from Wetland Authority"; }
                }

                if (lblSigneddoc2.Visible)
                {
                    if (obj_MiningNewApplication.GetSignedDocAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Aplication with Signature and Seal" : ErrorMessage + ",Aplication with Signature and Seal"; }
                }
                if (lblPenalty2.Visible)
                {
                    if (obj_MiningNewApplication.GetPenaltyAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Penalty" : ErrorMessage + ",Penalty"; }
                }






                if (lblTORApprovalLetter2.Visible)
                {
                    if (obj_MiningNewApplication.GetTORECApprovalLetterAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "TOR/EC/Approval Letter" : ErrorMessage + ",TOR/EC/Approval Letter"; }
                }

                if (lblGeneralQualityGW2.Visible)
                {
                    if (obj_MiningNewApplication.GetMonitorGroundWaterRegimeGQsuroudingsAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "General Quality of GW in Area" : ErrorMessage + ",General Quality of GW in Area"; }
                }

                if (lblMonitoringofGroundwater2.Visible)
                {
                    if (obj_MiningNewApplication.GetMonitorGroundWaterRegimeGQsuroudingsAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Monitoring of Groundwater Regime Map" : ErrorMessage + ",Monitoring of Groundwater Regime Map"; }
                }


                if (lblAuthorizationLetter2.Visible)
                {
                    if (obj_MiningNewApplication.GetUndertakingAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Authorization Letter" : ErrorMessage + ",Authorization Letter"; }
                }

                if (!string.IsNullOrEmpty(ErrorMessage))
                {
                    lblMessage.Text = HttpUtility.HtmlEncode(ErrorMessage + " Attachments are Mandatory.");
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                lngMinSubmitAppCode = lng_submittedApplicationCode;
                //Server.Transfer("Submit.aspx");
                Server.Transfer("MINNewReadyToSubmit.aspx");
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception)
            {
                strStatus = "Record Save Failed !";
                //lblFinalMsg.Text = HttpUtility.HtmlEncode(ex.Message);
            }
            finally
            {
                obj_MiningNewApplication.Dispose();
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
    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {

    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            else
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                //Server.Transfer("~/ExternalUser/MiningNew/SelfDeclaration.aspx");
                Server.Transfer("~/ExternalUser/MiningNew/OtherDetails.aspx");
            }
        }
    }
    #endregion

    #region RowDeleting
    protected void gvSitePlan_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            strActionName = "DeleteSitePlan";
            try
            {
                long lng_MiningNewApplicationCode = Convert.ToInt32(gvSitePlan.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvSitePlan.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvSitePlan.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_MiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment(lng_MiningNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningNewApplicationAttachment.AttachmentPath;
                if (obj_MiningNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    clearMessage();
                    lblMessageSitePlan.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                    // BindMiningNewApplicationAttachmentSitePlanDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    ClearAttachmentFileName();

                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageSitePlan.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void gvApprovedMiningPlan_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "DeleteApprovedMiningPlan";
                long lng_MiningNewApplicationCode = Convert.ToInt32(gvApprovedMiningPlan.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvApprovedMiningPlan.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvApprovedMiningPlan.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_MiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment(lng_MiningNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningNewApplicationAttachment.AttachmentPath;
                if (obj_MiningNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    clearMessage();
                    lblMessageApprovedMiningPlan.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageApprovedMiningPlan.ForeColor = System.Drawing.Color.Red;
                    BindMiningNewApplicationAttachmentApproveMiningPlanDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    ClearAttachmentFileName();
                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageApprovedMiningPlan.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageApprovedMiningPlan.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void gvToposketch_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "DeleteToposketch";
                long lng_MiningNewApplicationCode = Convert.ToInt32(gvToposketch.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvToposketch.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvToposketch.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_MiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment(lng_MiningNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningNewApplicationAttachment.AttachmentPath;
                if (obj_MiningNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    clearMessage();
                    lblMessageToposketch.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageToposketch.ForeColor = System.Drawing.Color.Red;
                    //  BindMiningNewApplicationAttachmentTopoSketchDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    ClearAttachmentFileName();
                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageToposketch.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageToposketch.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void gvDocumentsofOwnership_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "DeleteDocumentsofOwnership";
                long lng_MiningNewApplicationCode = Convert.ToInt32(gvDocumentsofOwnership.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvDocumentsofOwnership.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvDocumentsofOwnership.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_MiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment(lng_MiningNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningNewApplicationAttachment.AttachmentPath;
                if (obj_MiningNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Upload Successfully !";
                    clearMessage();
                    lblMessageDocumentsofOwnership.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageDocumentsofOwnership.ForeColor = System.Drawing.Color.Red;
                    //  BindMiningNewApplicationAttachmentOwnerShipOfLandDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    ClearAttachmentFileName();
                }
                else
                {
                    strStatus = "File Upload Failed !";
                    lblMessageDocumentsofOwnership.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageDocumentsofOwnership.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                strStatus = "File Upload Failed !";
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
    protected void gvSourceofAvailabilityofSurfaceWater_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "DeleteSourceofAvailabilityofSurfaceWater";
                long lng_MiningNewApplicationCode = Convert.ToInt32(gvSourceofAvailabilityofSurfaceWater.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvSourceofAvailabilityofSurfaceWater.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvSourceofAvailabilityofSurfaceWater.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_MiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment(lng_MiningNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningNewApplicationAttachment.AttachmentPath;
                if (obj_MiningNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    clearMessage();
                    lblMessageSourceofAvailabilityofSurfaceWater.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;
                    // BindMiningNewApplicationAttachmentAvailabilityOfSurfaceWaterDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    ClearAttachmentFileName();
                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageSourceofAvailabilityofSurfaceWater.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void gvGWFlowDirectionMap_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "DeleteGWFlowDirectionMap";
                long lng_MiningNewApplicationCode = Convert.ToInt32(gvGWFlowDirectionMap.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvGWFlowDirectionMap.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvGWFlowDirectionMap.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_MiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment(lng_MiningNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningNewApplicationAttachment.AttachmentPath;
                if (obj_MiningNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    clearMessage();
                    lblMessageGWFlowDirectionMap.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageGWFlowDirectionMap.ForeColor = System.Drawing.Color.Red;
                    BindMiningNewApplicationAttachmentGroundWaterFlowDirectionDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    ClearAttachmentFileName();
                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageGWFlowDirectionMap.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageGWFlowDirectionMap.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void gvProUtiPumpedWater_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "DeleteProUtiPumpedWater";
                long lng_MiningNewApplicationCode = Convert.ToInt32(gvProUtiPumpedWater.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvProUtiPumpedWater.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvProUtiPumpedWater.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_MiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment(lng_MiningNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningNewApplicationAttachment.AttachmentPath;
                if (obj_MiningNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    clearMessage();
                    lblMessageProUtiPumpedWater.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageProUtiPumpedWater.ForeColor = System.Drawing.Color.Red;
                    //  BindMiningNewApplicationAttachmentProposedUtilizationOfPumpedWaterDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    ClearAttachmentFileName();
                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageProUtiPumpedWater.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageProUtiPumpedWater.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void gvMonitorGWRegime_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "DeleteMonitorGWRegime";
                long lng_MiningNewApplicationCode = Convert.ToInt32(gvMonitorGWRegime.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvMonitorGWRegime.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvMonitorGWRegime.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_MiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment(lng_MiningNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningNewApplicationAttachment.AttachmentPath;
                if (obj_MiningNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    clearMessage();
                    lblMessageMonitorGWRegime.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageMonitorGWRegime.ForeColor = System.Drawing.Color.Red;
                    BindMiningNewApplicationAttachmentGroundWaterRegimeMapDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    ClearAttachmentFileName();
                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageMonitorGWRegime.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageMonitorGWRegime.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void gvGWLevelObservation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "DeleteGWLevelObservation";
                long lng_MiningNewApplicationCode = Convert.ToInt32(gvGWLevelObservation.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvGWLevelObservation.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvGWLevelObservation.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_MiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment(lng_MiningNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningNewApplicationAttachment.AttachmentPath;
                if (obj_MiningNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    clearMessage();
                    lblMessageGWLevelObservation.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageGWLevelObservation.ForeColor = System.Drawing.Color.Red;
                    // BindMiningNewApplicationAttachmentGroundWaterObservationWellsDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    ClearAttachmentFileName();
                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageGWLevelObservation.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageGWLevelObservation.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void gvGQofGWInArea_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "DeleteGQofGWInArea";
                long lng_MiningNewApplicationCode = Convert.ToInt32(gvGQofGWInArea.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvGQofGWInArea.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvGQofGWInArea.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_MiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment(lng_MiningNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningNewApplicationAttachment.AttachmentPath;
                if (obj_MiningNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    clearMessage();
                    lblMessageGQofGWInArea.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageGQofGWInArea.ForeColor = System.Drawing.Color.Red;
                    BindMiningNewApplicationAttachmentGQualityOfGroundWaterInAreaDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    ClearAttachmentFileName();
                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageGQofGWInArea.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageGQofGWInArea.ForeColor = System.Drawing.Color.Red;

                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void gvGroundwaterAvailability_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "DeleteGroundwaterAvailability";
                long lng_MiningNewApplicationCode = Convert.ToInt32(gvGroundwaterAvailability.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvGroundwaterAvailability.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvGroundwaterAvailability.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_MiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment(lng_MiningNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningNewApplicationAttachment.AttachmentPath;
                if (obj_MiningNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    clearMessage();
                    lblMessageGroundwaterAvailability.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                    BindMiningNewApplicationAttachmentGroundWaterAvailabilityDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    ClearAttachmentFileName();
                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageGroundwaterAvailability.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void gvRainwaterHarvesting_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "DeleteRainwaterHarvesting";
                long lng_MiningNewApplicationCode = Convert.ToInt32(gvRainwaterHarvesting.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvRainwaterHarvesting.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvRainwaterHarvesting.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_MiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment(lng_MiningNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningNewApplicationAttachment.AttachmentPath;
                if (obj_MiningNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    clearMessage();
                    lblMessageRainwaterHarvesting.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                    BindMiningNewApplicationAttachmentRainWaterharvestingDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    ClearAttachmentFileName();
                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageRainwaterHarvesting.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void gvUndertaking_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "DeleteUndertaking";
                long lng_MiningNewApplicationCode = Convert.ToInt32(gvUndertaking.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvUndertaking.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvUndertaking.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_MiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment(lng_MiningNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningNewApplicationAttachment.AttachmentPath;
                if (obj_MiningNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    clearMessage();
                    lblMessageUndertaking.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                    BindMiningNewApplicationAttachmentUndertalkingDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    ClearAttachmentFileName();
                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageUndertaking.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void gvReferralLetterAttachment_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "DeleteReferralLetterAttachment";
                long lng_MiningNewApplicationCode = Convert.ToInt32(gvReferralLetterAttachment.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvReferralLetterAttachment.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvReferralLetterAttachment.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_MiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment(lng_MiningNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningNewApplicationAttachment.AttachmentPath;
                if (obj_MiningNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    clearMessage();
                    lblMessageReferralLetter.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageReferralLetter.ForeColor = System.Drawing.Color.Red;
                    //  BindMiningNewApplicationAttachmentRefferalLetterDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    ClearAttachmentFileName();
                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageReferralLetter.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageReferralLetter.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void gvExtra_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "DeleteExtraAttachment";
                long lng_MiningNewApplicationCode = Convert.ToInt32(gvExtra.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvExtra.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvExtra.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_MiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment(lng_MiningNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningNewApplicationAttachment.AttachmentPath;
                if (obj_MiningNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    clearMessage();
                    lblMessageExtra.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                    BindMiningNewApplicationAttachmentExtraAttachmentsDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    ClearAttachmentFileName();
                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageExtra.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void gvTORECApprovalLetter_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "DeleteTORECApprovalLetterAttachment";
                long lng_MiningNewApplicationCode = Convert.ToInt32(gvTORECApprovalLetter.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvTORECApprovalLetter.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvTORECApprovalLetter.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_MiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment(lng_MiningNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningNewApplicationAttachment.AttachmentPath;
                if (obj_MiningNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    clearMessage();
                    lblMessageTORECApprovalLetter.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageTORECApprovalLetter.ForeColor = System.Drawing.Color.Red;
                    BindMiningNewApplicationAttachmentTORECApprovalLetterDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    ClearAttachmentFileName();
                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageTORECApprovalLetter.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationAttachment.CustumMessage);
                    lblMessageTORECApprovalLetter.ForeColor = System.Drawing.Color.Red;

                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void BharatKoshReciept_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "File Delete Bharat Kosh Reciept";
                long lng_ApplicationCode = Convert.ToInt32(gvBharatKoshReciept.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvBharatKoshReciept.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvBharatKoshReciept.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_miningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment(lng_ApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                if (obj_miningNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Success";
                    lblMessageBharatKoshReciept.Text = obj_miningNewApplicationAttachment.CustumMessage;
                    lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
                    BindMiningNewApplicationAttachmentBharatKoshRecieptDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed";
                    lblMessageBharatKoshReciept.Text = obj_miningNewApplicationAttachment.CustumMessage;
                    lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;

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
    protected void gvWetlandArea_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "File Delete Wetland Area";
                long lng_ApplicationCode = Convert.ToInt32(gvWetlandArea.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvWetlandArea.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvWetlandArea.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_miningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment(lng_ApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                if (obj_miningNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Success";
                    lblMessageWetlanArea.Text = obj_miningNewApplicationAttachment.CustumMessage;
                    lblMessageWetlanArea.ForeColor = System.Drawing.Color.Red;
                    BindWetlandAreaAttachmentDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed";
                    lblMessageWetlanArea.Text = obj_miningNewApplicationAttachment.CustumMessage;
                    lblMessageWetlanArea.ForeColor = System.Drawing.Color.Red;

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
    protected void gvPenalty_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "File Delete Penalty";
                long lng_ApplicationCode = Convert.ToInt32(gvPenalty.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvPenalty.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvPenalty.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_miningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment(lng_ApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                if (obj_miningNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Success";
                    lblMessagePenalty.Text = obj_miningNewApplicationAttachment.CustumMessage;
                    lblMessagePenalty.ForeColor = System.Drawing.Color.Red;
                    BindPenaltyAttachmentDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed";
                    lblMessagePenalty.Text = obj_miningNewApplicationAttachment.CustumMessage;
                    lblMessagePenalty.ForeColor = System.Drawing.Color.Red;

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

    protected void gvAplicationSignatureSeal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "File Delete Aplication with Signature and Seal";
                long lng_MiningNewApplicationCode = Convert.ToInt32(gvApplicationSignatureSeal.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvApplicationSignatureSeal.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvApplicationSignatureSeal.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_miningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment(lng_MiningNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                if (obj_miningNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Success";
                    lblMessageAplicationSignatureSeal.Text = obj_miningNewApplicationAttachment.CustumMessage;
                    lblMessageAplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;
                    BindMiningNewApplicationAttachmentSignedDocDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed";
                    lblMessageAplicationSignatureSeal.Text = obj_miningNewApplicationAttachment.CustumMessage;
                    lblMessageAplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;

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
    #endregion

    #region Button Upload Click
    protected void btnUploadTORECApprovalLetter_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "UploadTORECApprovalLetter";
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationForNoLimit = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
                arr_MiningNewApplicationAttachmentList = obj_MiningNewApplicationForNoLimit.GetTORECApprovalLetterAttachmentList();
                if (arr_MiningNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_insertMiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment> lst_MiningNewApplicationAttachmentList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadTORECApprovalLetter.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadTORECApprovalLetter.PostedFile.FileName).ToLower();
                            str_fname = FileUploadTORECApprovalLetter.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadTORECApprovalLetter.PostedFile))
                                {
                                    if (FileUploadTORECApprovalLetter.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadTORECApprovalLetter.PostedFile);
                                        obj_insertMiningNewApplicationAttachment.ContentType = FileUploadTORECApprovalLetter.PostedFile.ContentType;
                                        obj_insertMiningNewApplicationAttachment.AttachmentPath = FileUploadTORECApprovalLetter.FileName;
                                        obj_insertMiningNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageTORECApprovalLetter.Text = HttpUtility.HtmlEncode("File can not upload. It has more than 5 MB size");
                                        lblMessageTORECApprovalLetter.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageTORECApprovalLetter.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                    lblMessageTORECApprovalLetter.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }

                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageTORECApprovalLetter.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                lblMessageTORECApprovalLetter.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageTORECApprovalLetter.Text = HttpUtility.HtmlEncode("Please select a file..!!");
                            lblMessageTORECApprovalLetter.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningNewApplicationAttachment.ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentCode = obj_MiningNewApplication.MiningNewTorEcApprovalLetterAttachmentCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentName = txtTORECApprovalLetterAttachment.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertMiningNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageTORECApprovalLetter.Text = HttpUtility.HtmlEncode("File Upload Success");
                            lblMessageTORECApprovalLetter.ForeColor = System.Drawing.Color.Green;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageTORECApprovalLetter.Text = HttpUtility.HtmlEncode("File Upload Failed");
                            lblMessageTORECApprovalLetter.ForeColor = System.Drawing.Color.Red;

                        }

                        clearMessage();
                        lblMessageTORECApprovalLetter.ForeColor = Color.Green;
                        lblMessageTORECApprovalLetter.Text = HttpUtility.HtmlEncode(obj_insertMiningNewApplicationAttachment.CustumMessage);
                        BindMiningNewApplicationAttachmentTORECApprovalLetterDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                        ClearAttachmentFileName();

                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";
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
                else
                {
                    lblMessageTORECApprovalLetter.Text = HttpUtility.HtmlEncode("Maximum number of files to be uploaded is 5");
                    lblMessageTORECApprovalLetter.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

    }
    protected void btnUploadSitePlan_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "UploadSitePlan";
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationForNoLimit = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
                arr_MiningNewApplicationAttachmentList = obj_MiningNewApplicationForNoLimit.GetSitePlanAttachmentList();
                if (arr_MiningNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_insertMiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment> lst_MiningNewApplicationAttachmentList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadSitePlan.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadSitePlan.PostedFile.FileName).ToLower();
                            str_fname = FileUploadSitePlan.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadSitePlan.PostedFile))
                                {
                                    if (FileUploadSitePlan.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadSitePlan.PostedFile);
                                        obj_insertMiningNewApplicationAttachment.ContentType = FileUploadSitePlan.PostedFile.ContentType;
                                        obj_insertMiningNewApplicationAttachment.AttachmentPath = FileUploadSitePlan.FileName;
                                        obj_insertMiningNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageSitePlan.Text = HttpUtility.HtmlEncode("File can not upload. It has more than 5 MB size");
                                        lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageSitePlan.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                    lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }

                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageSitePlan.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageSitePlan.Text = HttpUtility.HtmlEncode("Please select a file..!!");
                            lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningNewApplicationAttachment.ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentCode = obj_MiningNewApplication.ProposedLocation.ProposedLocationSitePlanAttachCode;

                        obj_insertMiningNewApplicationAttachment.AttachmentName = txtSitePlanAttachment.Text; ;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;


                        if (obj_insertMiningNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageSitePlan.Text = HttpUtility.HtmlEncode("File Upload Success");
                            lblMessageSitePlan.ForeColor = System.Drawing.Color.Green;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageSitePlan.Text = HttpUtility.HtmlEncode("File Upload Failed");
                            lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;

                        }

                        clearMessage();
                        lblMessageSitePlan.ForeColor = Color.Green;
                        lblMessageSitePlan.Text = HttpUtility.HtmlEncode(obj_insertMiningNewApplicationAttachment.CustumMessage);

                        //BindMiningNewApplicationAttachmentSitePlanDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                        ClearAttachmentFileName();
                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";

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
                else
                {
                    lblMessageSitePlan.Text = HttpUtility.HtmlEncode("Maximum number of files to be uploaded is 5");
                    lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUploadApprovedMiningPlan_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "UploadApprovedMiningPlan";
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationForNoLimit = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
                arr_MiningNewApplicationAttachmentList = obj_MiningNewApplicationForNoLimit.GetApproveMiningPlanAttachmentList();
                if (arr_MiningNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_insertMiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment> lst_MiningNewApplicationAttachmentList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (txtFileUploadApprovedMiningPlan.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(txtFileUploadApprovedMiningPlan.PostedFile.FileName).ToLower();
                            str_fname = txtFileUploadApprovedMiningPlan.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(txtFileUploadApprovedMiningPlan.PostedFile))
                                {
                                    if (txtFileUploadApprovedMiningPlan.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(txtFileUploadApprovedMiningPlan.PostedFile);
                                        obj_insertMiningNewApplicationAttachment.ContentType = txtFileUploadApprovedMiningPlan.PostedFile.ContentType;
                                        obj_insertMiningNewApplicationAttachment.AttachmentPath = txtFileUploadApprovedMiningPlan.FileName;
                                        obj_insertMiningNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageApprovedMiningPlan.Text = HttpUtility.HtmlEncode("File can not upload. It has more than 5 MB size");
                                        lblMessageApprovedMiningPlan.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageApprovedMiningPlan.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                    lblMessageApprovedMiningPlan.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageApprovedMiningPlan.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                lblMessageApprovedMiningPlan.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageApprovedMiningPlan.Text = HttpUtility.HtmlEncode("Please select a file..!!");
                            lblMessageApprovedMiningPlan.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningNewApplicationAttachment.ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentCode = obj_MiningNewApplication.ProposedLocation.ProposedLocationApproveMiningPlan;

                        obj_insertMiningNewApplicationAttachment.AttachmentName = txtApprovedMiningPlan.Text;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageApprovedMiningPlan.Text = HttpUtility.HtmlEncode("File Upload Success");
                            lblMessageApprovedMiningPlan.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageApprovedMiningPlan.Text = HttpUtility.HtmlEncode("File Upload Failed");
                            lblMessageApprovedMiningPlan.ForeColor = System.Drawing.Color.Red;
                        }
                        clearMessage();
                        lblMessageApprovedMiningPlan.ForeColor = Color.Green;
                        lblMessageApprovedMiningPlan.Text = HttpUtility.HtmlEncode(obj_insertMiningNewApplicationAttachment.CustumMessage);

                        BindMiningNewApplicationAttachmentApproveMiningPlanDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                        ClearAttachmentFileName();
                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";
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
                else
                {
                    lblMessageApprovedMiningPlan.Text = HttpUtility.HtmlEncode("Maximum number of files to be uploaded is 5");
                    lblMessageApprovedMiningPlan.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUploadToposketch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "UploadToposketch";
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationForNoLimit = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
                arr_MiningNewApplicationAttachmentList = obj_MiningNewApplicationForNoLimit.GetTopoSketchAttachmentList();
                if (arr_MiningNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_insertMiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment> lst_MiningNewApplicationAttachmentList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (txtFileUploadToposketch.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(txtFileUploadToposketch.PostedFile.FileName).ToLower();
                            str_fname = txtFileUploadToposketch.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(txtFileUploadToposketch.PostedFile))
                                {
                                    if (txtFileUploadToposketch.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(txtFileUploadToposketch.PostedFile);
                                        obj_insertMiningNewApplicationAttachment.ContentType = txtFileUploadToposketch.PostedFile.ContentType;
                                        obj_insertMiningNewApplicationAttachment.AttachmentPath = txtFileUploadToposketch.FileName;
                                        obj_insertMiningNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageToposketch.Text = HttpUtility.HtmlEncode("File can not upload. It has more than 5 MB size");
                                        lblMessageToposketch.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageToposketch.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                    lblMessageToposketch.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageToposketch.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                lblMessageToposketch.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageToposketch.Text = HttpUtility.HtmlEncode("Please select a file..!!");
                            lblMessageToposketch.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningNewApplicationAttachment.ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentCode = obj_MiningNewApplication.ProposedLocation.ProposedLocationTopoSketch;

                        obj_insertMiningNewApplicationAttachment.AttachmentName = txtToposketch.Text; ;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageToposketch.Text = HttpUtility.HtmlEncode("File Upload Success");
                            lblMessageToposketch.ForeColor = System.Drawing.Color.Green;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageToposketch.Text = HttpUtility.HtmlEncode("File Upload Failed");
                            lblMessageToposketch.ForeColor = System.Drawing.Color.Red;

                        }
                        clearMessage();
                        lblMessageToposketch.ForeColor = Color.Green;
                        lblMessageToposketch.Text = HttpUtility.HtmlEncode(obj_insertMiningNewApplicationAttachment.CustumMessage);

                        // BindMiningNewApplicationAttachmentTopoSketchDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                        ClearAttachmentFileName();
                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";
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
                else
                {
                    lblMessageToposketch.Text = HttpUtility.HtmlEncode("Maximum number of files to be uploaded is 5");
                    lblMessageToposketch.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUplodDocumentsofOwnership_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "UploadDocumentsofOwnership";
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationForNoLimit = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
                arr_MiningNewApplicationAttachmentList = obj_MiningNewApplicationForNoLimit.GetOwnerShipOfLandAttachmentList();
                if (arr_MiningNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_insertMiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment> lst_MiningNewApplicationAttachmentList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadDocumentsofOwnership.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadDocumentsofOwnership.PostedFile.FileName).ToLower();
                            str_fname = FileUploadDocumentsofOwnership.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadDocumentsofOwnership.PostedFile))
                                {
                                    if (FileUploadDocumentsofOwnership.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadDocumentsofOwnership.PostedFile);
                                        obj_insertMiningNewApplicationAttachment.ContentType = FileUploadDocumentsofOwnership.PostedFile.ContentType;
                                        obj_insertMiningNewApplicationAttachment.AttachmentPath = FileUploadDocumentsofOwnership.FileName;
                                        obj_insertMiningNewApplicationAttachment.FileExtension = str_ext;

                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageDocumentsofOwnership.Text = HttpUtility.HtmlEncode("File can not upload. It has more than 5 MB size");
                                        lblMessageDocumentsofOwnership.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageDocumentsofOwnership.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                    lblMessageDocumentsofOwnership.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageDocumentsofOwnership.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                lblMessageDocumentsofOwnership.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageDocumentsofOwnership.Text = HttpUtility.HtmlEncode("Please select a file..!!");
                            lblMessageDocumentsofOwnership.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningNewApplicationAttachment.ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentCode = obj_MiningNewApplication.LandUseDetailOfExistingProposed.LandUseTypeOwnershipLeaseAttachCode;

                        obj_insertMiningNewApplicationAttachment.AttachmentName = txtDocumentsofOwnership.Text; ;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageDocumentsofOwnership.Text = HttpUtility.HtmlEncode("File Upload Success");
                            lblMessageDocumentsofOwnership.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageDocumentsofOwnership.Text = HttpUtility.HtmlEncode("File Upload Failed");
                            lblMessageDocumentsofOwnership.ForeColor = System.Drawing.Color.Red;

                        }
                        clearMessage();
                        lblMessageDocumentsofOwnership.ForeColor = Color.Green;
                        lblMessageDocumentsofOwnership.Text = HttpUtility.HtmlEncode(obj_insertMiningNewApplicationAttachment.CustumMessage);

                        //  BindMiningNewApplicationAttachmentOwnerShipOfLandDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                        ClearAttachmentFileName();
                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";
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
                else
                {
                    lblMessageDocumentsofOwnership.Text = HttpUtility.HtmlEncode("Maximum number of files to be uploaded is 5");
                    lblMessageDocumentsofOwnership.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUplodSourceofAvailabilityofSurfaceWater_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "UploadSourceofAvailabilityofSurfaceWater";
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationForNoLimit = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
                arr_MiningNewApplicationAttachmentList = obj_MiningNewApplicationForNoLimit.GetSurfaceWaterAvailabilityAttachmentList();
                if (arr_MiningNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_insertMiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment> lst_MiningNewApplicationAttachmentList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (txtFileUploadSourceofAvailabilityofSurfaceWater.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(txtFileUploadSourceofAvailabilityofSurfaceWater.PostedFile.FileName).ToLower();
                            str_fname = txtFileUploadSourceofAvailabilityofSurfaceWater.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(txtFileUploadSourceofAvailabilityofSurfaceWater.PostedFile))
                                {
                                    if (txtFileUploadSourceofAvailabilityofSurfaceWater.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(txtFileUploadSourceofAvailabilityofSurfaceWater.PostedFile);
                                        obj_insertMiningNewApplicationAttachment.ContentType = txtFileUploadSourceofAvailabilityofSurfaceWater.PostedFile.ContentType;
                                        obj_insertMiningNewApplicationAttachment.AttachmentPath = txtFileUploadSourceofAvailabilityofSurfaceWater.FileName;
                                        obj_insertMiningNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageSourceofAvailabilityofSurfaceWater.Text = HttpUtility.HtmlEncode("File can not upload. It has more than 5 MB size");
                                        lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageSourceofAvailabilityofSurfaceWater.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                    lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageSourceofAvailabilityofSurfaceWater.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageSourceofAvailabilityofSurfaceWater.Text = HttpUtility.HtmlEncode("Please select a file..!!");
                            lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningNewApplicationAttachment.ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentCode = obj_MiningNewApplication.SourceOfAvailabilityOfSurfaceWater.SourceOfAvalabilityOfSurfaceWaterAttachCode;

                        obj_insertMiningNewApplicationAttachment.AttachmentName = txtSourceofAvailabilityofSurfaceWater.Text; ;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageSourceofAvailabilityofSurfaceWater.Text = HttpUtility.HtmlEncode("File Upload Success");
                            lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageSourceofAvailabilityofSurfaceWater.Text = HttpUtility.HtmlEncode("File Upload Failed");
                            lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;

                        }
                        clearMessage();
                        lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = Color.Green;
                        lblMessageSourceofAvailabilityofSurfaceWater.Text = HttpUtility.HtmlEncode(obj_insertMiningNewApplicationAttachment.CustumMessage);

                        //  BindMiningNewApplicationAttachmentAvailabilityOfSurfaceWaterDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                        ClearAttachmentFileName();
                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";
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
                else
                {
                    lblMessageSourceofAvailabilityofSurfaceWater.Text = HttpUtility.HtmlEncode("Maximum number of files to be uploaded is 5");
                    lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUplodGWFlowDirectionMap_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "UploadGWFlowDirectionMap";
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationForNoLimit = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
                arr_MiningNewApplicationAttachmentList = obj_MiningNewApplicationForNoLimit.GetGroundWaterflowDirectionAttachmentList();
                if (arr_MiningNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_insertMiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment> lst_MiningNewApplicationAttachmentList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadGWFlowDirectionMap.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadGWFlowDirectionMap.PostedFile.FileName).ToLower();
                            str_fname = FileUploadGWFlowDirectionMap.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadGWFlowDirectionMap.PostedFile))
                                {
                                    if (FileUploadGWFlowDirectionMap.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadGWFlowDirectionMap.PostedFile);
                                        obj_insertMiningNewApplicationAttachment.ContentType = FileUploadGWFlowDirectionMap.PostedFile.ContentType;
                                        obj_insertMiningNewApplicationAttachment.AttachmentPath = FileUploadGWFlowDirectionMap.FileName;
                                        obj_insertMiningNewApplicationAttachment.FileExtension = str_ext;

                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageGWFlowDirectionMap.Text = HttpUtility.HtmlEncode("File can not upload. It has more than 5 MB size");
                                        lblMessageGWFlowDirectionMap.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageGWFlowDirectionMap.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                    lblMessageGWFlowDirectionMap.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageGWFlowDirectionMap.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                lblMessageGWFlowDirectionMap.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageGWFlowDirectionMap.Text = HttpUtility.HtmlEncode("Please select a file..!!");
                            lblMessageGWFlowDirectionMap.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningNewApplicationAttachment.ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentCode = obj_MiningNewApplication.MiningNewSADDewatering.GWFlowAttachCode;

                        obj_insertMiningNewApplicationAttachment.AttachmentName = txtGWFlowDirectionMap.Text; ;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageGWFlowDirectionMap.Text = HttpUtility.HtmlEncode("File Upload Success");
                            lblMessageGWFlowDirectionMap.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageGWFlowDirectionMap.Text = HttpUtility.HtmlEncode("File Upload Failed");
                            lblMessageGWFlowDirectionMap.ForeColor = System.Drawing.Color.Red;

                        }
                        clearMessage();
                        lblMessageGWFlowDirectionMap.ForeColor = Color.Green;
                        lblMessageGWFlowDirectionMap.Text = HttpUtility.HtmlEncode(obj_insertMiningNewApplicationAttachment.CustumMessage);
                        BindMiningNewApplicationAttachmentGroundWaterFlowDirectionDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                        ClearAttachmentFileName();
                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";
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
                else
                {
                    lblMessageGWFlowDirectionMap.Text = HttpUtility.HtmlEncode("Maximum number of files to be uploaded is 5");
                    lblMessageGWFlowDirectionMap.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUploadProUtiPumpedWater_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "UploadProUtiPumpedWater";
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationForNoLimit = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
                arr_MiningNewApplicationAttachmentList = obj_MiningNewApplicationForNoLimit.GetProposedUtilizationPumpedWaterAttachmentList();
                if (arr_MiningNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_insertMiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment> lst_MiningNewApplicationAttachmentList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadProUtiPumpedWater.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadProUtiPumpedWater.PostedFile.FileName).ToLower();
                            str_fname = FileUploadProUtiPumpedWater.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadProUtiPumpedWater.PostedFile))
                                {
                                    if (FileUploadProUtiPumpedWater.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadProUtiPumpedWater.PostedFile);
                                        obj_insertMiningNewApplicationAttachment.ContentType = FileUploadProUtiPumpedWater.PostedFile.ContentType;
                                        obj_insertMiningNewApplicationAttachment.AttachmentPath = FileUploadProUtiPumpedWater.FileName;
                                        obj_insertMiningNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageProUtiPumpedWater.Text = HttpUtility.HtmlEncode("File can not upload. It has more than 5 MB size");
                                        lblMessageProUtiPumpedWater.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageProUtiPumpedWater.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                    lblMessageProUtiPumpedWater.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageProUtiPumpedWater.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                lblMessageProUtiPumpedWater.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageProUtiPumpedWater.Text = HttpUtility.HtmlEncode("Please select a file..!!");
                            lblMessageProUtiPumpedWater.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningNewApplicationAttachment.ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentCode = obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumpWaterAttachmentCode;

                        obj_insertMiningNewApplicationAttachment.AttachmentName = txtProUtiPumpedWater.Text; ;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageProUtiPumpedWater.Text = HttpUtility.HtmlEncode("File Upload Success");
                            lblMessageProUtiPumpedWater.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageProUtiPumpedWater.Text = HttpUtility.HtmlEncode("File Upload Failed");
                            lblMessageProUtiPumpedWater.ForeColor = System.Drawing.Color.Red;

                        }
                        clearMessage();
                        lblMessageProUtiPumpedWater.ForeColor = Color.Green;
                        lblMessageProUtiPumpedWater.Text = HttpUtility.HtmlEncode(obj_insertMiningNewApplicationAttachment.CustumMessage);

                        // BindMiningNewApplicationAttachmentProposedUtilizationOfPumpedWaterDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                        ClearAttachmentFileName();

                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";
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
                else
                {
                    lblMessageProUtiPumpedWater.Text = HttpUtility.HtmlEncode("Maximum number of files to be uploaded is 5");
                    lblMessageProUtiPumpedWater.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUplodMonitorGWRegime_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "UploadMonitorGWRegime";
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationForNoLimit = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
                arr_MiningNewApplicationAttachmentList = obj_MiningNewApplicationForNoLimit.GetMonitorGroundWaterRegimeMapAttachmentList();
                if (arr_MiningNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_insertMiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment> lst_MiningNewApplicationAttachmentList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadMonitorGWRegime.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadMonitorGWRegime.PostedFile.FileName).ToLower();
                            str_fname = FileUploadMonitorGWRegime.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadMonitorGWRegime.PostedFile))
                                {
                                    if (FileUploadMonitorGWRegime.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadMonitorGWRegime.PostedFile);
                                        obj_insertMiningNewApplicationAttachment.ContentType = FileUploadMonitorGWRegime.PostedFile.ContentType;
                                        obj_insertMiningNewApplicationAttachment.AttachmentPath = FileUploadMonitorGWRegime.FileName;
                                        obj_insertMiningNewApplicationAttachment.FileExtension = str_ext;

                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageMonitorGWRegime.Text = HttpUtility.HtmlEncode("File can not upload. It has more than 5 MB size");
                                        lblMessageMonitorGWRegime.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageMonitorGWRegime.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                    lblMessageMonitorGWRegime.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageMonitorGWRegime.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                lblMessageMonitorGWRegime.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageMonitorGWRegime.Text = HttpUtility.HtmlEncode("Please select a file..!!");
                            lblMessageMonitorGWRegime.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningNewApplicationAttachment.ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentCode = obj_MiningNewApplication.MiningNewMonitoringGWRegime.MonitoringGWRegimeMapAttachCode;

                        obj_insertMiningNewApplicationAttachment.AttachmentName = txtMonitorGWRegime.Text; ;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageMonitorGWRegime.Text = HttpUtility.HtmlEncode("File Upload Success");
                            lblMessageMonitorGWRegime.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageMonitorGWRegime.Text = HttpUtility.HtmlEncode("File Upload Failed");
                            lblMessageMonitorGWRegime.ForeColor = System.Drawing.Color.Red;

                        }
                        clearMessage();
                        lblMessageMonitorGWRegime.ForeColor = Color.Green;
                        lblMessageMonitorGWRegime.Text = HttpUtility.HtmlEncode(obj_insertMiningNewApplicationAttachment.CustumMessage);

                        BindMiningNewApplicationAttachmentGroundWaterRegimeMapDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                        ClearAttachmentFileName();
                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";
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
                else
                {
                    lblMessageMonitorGWRegime.Text = HttpUtility.HtmlEncode("Maximum number of files to be uploaded is 5");
                    lblMessageMonitorGWRegime.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUplodGWLevelObservation_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "UploadGWLevelObservation";
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationForNoLimit = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
                arr_MiningNewApplicationAttachmentList = obj_MiningNewApplicationForNoLimit.GetMonitorGroundWaterRegimeObservationWellAttachmentList();
                if (arr_MiningNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_insertMiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment> lst_MiningNewApplicationAttachmentList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadGWLevelObservation.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadGWLevelObservation.PostedFile.FileName).ToLower();
                            str_fname = FileUploadGWLevelObservation.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadGWLevelObservation.PostedFile))
                                {
                                    if (FileUploadGWLevelObservation.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadGWLevelObservation.PostedFile);
                                        obj_insertMiningNewApplicationAttachment.ContentType = FileUploadGWLevelObservation.PostedFile.ContentType;
                                        obj_insertMiningNewApplicationAttachment.AttachmentPath = FileUploadGWLevelObservation.FileName;
                                        obj_insertMiningNewApplicationAttachment.FileExtension = str_ext;

                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageGWLevelObservation.Text = HttpUtility.HtmlEncode("File can not upload. It has more than 5 MB size");
                                        lblMessageGWLevelObservation.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageGWLevelObservation.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                    lblMessageGWLevelObservation.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageGWLevelObservation.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                lblMessageGWLevelObservation.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageGWLevelObservation.Text = HttpUtility.HtmlEncode("Please select a file..!!");
                            lblMessageGWLevelObservation.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningNewApplicationAttachment.ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentCode = obj_MiningNewApplication.MiningNewMonitoringGWRegime.MonitoringGWDetailOfLevelOfObservationWellsAttachCode;

                        obj_insertMiningNewApplicationAttachment.AttachmentName = txtGWLevelObservation.Text; ;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageGWLevelObservation.Text = HttpUtility.HtmlEncode("File Upload Success");
                            lblMessageGWLevelObservation.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageGWLevelObservation.Text = HttpUtility.HtmlEncode("File Upload Failed");
                            lblMessageGWLevelObservation.ForeColor = System.Drawing.Color.Red;

                        }
                        clearMessage();
                        lblMessageGWLevelObservation.ForeColor = Color.Green;
                        lblMessageGWLevelObservation.Text = HttpUtility.HtmlEncode(obj_insertMiningNewApplicationAttachment.CustumMessage);

                        // BindMiningNewApplicationAttachmentGroundWaterObservationWellsDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                        ClearAttachmentFileName();
                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";
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
                else
                {
                    lblMessageGWLevelObservation.Text = HttpUtility.HtmlEncode("Maximum number of files to be uploaded is 5");
                    lblMessageGWLevelObservation.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUplodGQofGWInArea_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "UplodGQofGWInArea";
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationForNoLimit = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
                arr_MiningNewApplicationAttachmentList = obj_MiningNewApplicationForNoLimit.GetMonitorGroundWaterRegimeGQsuroudingsAttachmentList();
                if (arr_MiningNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_insertMiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment> lst_MiningNewApplicationAttachmentList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadGQofGWInArea.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadGQofGWInArea.PostedFile.FileName).ToLower();
                            str_fname = FileUploadGQofGWInArea.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadGQofGWInArea.PostedFile))
                                {
                                    if (FileUploadGQofGWInArea.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadGQofGWInArea.PostedFile);
                                        obj_insertMiningNewApplicationAttachment.ContentType = FileUploadGQofGWInArea.PostedFile.ContentType;
                                        obj_insertMiningNewApplicationAttachment.AttachmentPath = FileUploadGQofGWInArea.FileName;
                                        obj_insertMiningNewApplicationAttachment.FileExtension = str_ext;

                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageGQofGWInArea.Text = HttpUtility.HtmlEncode("File can not upload. It has more than 5 MB size");
                                        lblMessageGQofGWInArea.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageGQofGWInArea.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                    lblMessageGQofGWInArea.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageGQofGWInArea.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                lblMessageGQofGWInArea.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageGQofGWInArea.Text = HttpUtility.HtmlEncode("Please select a file..!!");
                            lblMessageGQofGWInArea.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningNewApplicationAttachment.ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentCode = obj_MiningNewApplication.MiningNewMonitoringGWRegime.MonitoringGWGeneralQualityOfGWInSurroundingAttachCode;

                        obj_insertMiningNewApplicationAttachment.AttachmentName = txtGQofGWInArea.Text; ;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageGQofGWInArea.Text = HttpUtility.HtmlEncode("File Upload Success");
                            lblMessageGQofGWInArea.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageGQofGWInArea.Text = HttpUtility.HtmlEncode("File Upload Failed");
                            lblMessageGQofGWInArea.ForeColor = System.Drawing.Color.Red;

                        }
                        clearMessage();
                        lblMessageGQofGWInArea.ForeColor = Color.Green;
                        lblMessageGQofGWInArea.Text = HttpUtility.HtmlEncode(obj_insertMiningNewApplicationAttachment.CustumMessage);
                        BindMiningNewApplicationAttachmentGQualityOfGroundWaterInAreaDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                        ClearAttachmentFileName();
                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";
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
                else
                {
                    lblMessageGQofGWInArea.Text = HttpUtility.HtmlEncode("Maximum number of files to be uploaded is 5");
                    lblMessageGQofGWInArea.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUploadGroundwaterAvailability_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "UploadGroundwaterAvailability";

                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationForNoLimit = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
                arr_MiningNewApplicationAttachmentList = obj_MiningNewApplicationForNoLimit.GetGroundwaterAvailabilityAttachmentList();

                if (obj_MiningNewApplicationForNoLimit.ImpactAssOCSOTPVerified == NOCAP.BLL.Common.CommonApplication.ImpactAssOCSOTPVerifiedYesNo.Yes)
                {

                    if (arr_MiningNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                    {
                        string str_fname;
                        string str_ext;
                        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_insertMiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
                        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                        List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment> lst_MiningNewApplicationAttachmentList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment>();
                        byte[] buffer = new byte[1];
                        try
                        {
                            if (FileUploadGroundwaterAvailability.HasFile)
                            {
                                str_ext = System.IO.Path.GetExtension(FileUploadGroundwaterAvailability.PostedFile.FileName).ToLower();
                                str_fname = FileUploadGroundwaterAvailability.FileName;
                                if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                                {
                                    if (NOCAPExternalUtility.IsValidFile(FileUploadGroundwaterAvailability.PostedFile))
                                    {
                                        if (FileUploadGroundwaterAvailability.PostedFile.ContentLength < AttachmentSizeLimitForHydro())
                                        {
                                            obj_insertMiningNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadGroundwaterAvailability.PostedFile);
                                            obj_insertMiningNewApplicationAttachment.ContentType = FileUploadGroundwaterAvailability.PostedFile.ContentType;
                                            obj_insertMiningNewApplicationAttachment.AttachmentPath = FileUploadGroundwaterAvailability.FileName;
                                            obj_insertMiningNewApplicationAttachment.FileExtension = str_ext;

                                        }
                                        else
                                        {
                                            strStatus = "File Upload Failed !";
                                            lblMessageGroundwaterAvailability.Text = HttpUtility.HtmlEncode("File can not upload. It has more than 5 MB size");
                                            lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageGroundwaterAvailability.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                        lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageGroundwaterAvailability.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                    lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                lblMessageGroundwaterAvailability.Text = HttpUtility.HtmlEncode("Please select a file..!!");
                                lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                            obj_insertMiningNewApplicationAttachment.ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                            obj_insertMiningNewApplicationAttachment.AttachmentCode = obj_MiningNewApplication.GroundWaterAvailability.GroundWaterAvailabilityAttachCode;

                            obj_insertMiningNewApplicationAttachment.AttachmentName = txtgvGroundwaterAvailability.Text; ;

                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                            obj_insertMiningNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                            if (obj_insertMiningNewApplicationAttachment.Add() == 1)
                            {
                                strStatus = "File Upload Success";
                                lblMessageGroundwaterAvailability.Text = HttpUtility.HtmlEncode("File Upload Success");
                                lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageGroundwaterAvailability.Text = HttpUtility.HtmlEncode("File Upload Failed");
                                lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;

                            }
                            clearMessage();
                            lblMessageGroundwaterAvailability.ForeColor = Color.Green;
                            lblMessageGroundwaterAvailability.Text = HttpUtility.HtmlEncode(obj_insertMiningNewApplicationAttachment.CustumMessage);

                            BindMiningNewApplicationAttachmentGroundWaterAvailabilityDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                            ClearAttachmentFileName();
                        }
                        catch (Exception)
                        {
                            strStatus = "File Upload Failed !";
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
                    else
                    {
                        lblMessageGroundwaterAvailability.Text = HttpUtility.HtmlEncode("Maximum number of files to be uploaded is 5");
                        lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMessageGroundwaterAvailability.Text = "Please Verified Consultant OTP";
                    lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUploadRainwaterHarvesting_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "UploadRainwaterHarvesting";
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationForNoLimit = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
                arr_MiningNewApplicationAttachmentList = obj_MiningNewApplicationForNoLimit.GetRainwaterHarvestingAttachmentList();
                if (arr_MiningNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_insertMiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment> lst_MiningNewApplicationAttachmentList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadRainwaterHarvesting.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadRainwaterHarvesting.PostedFile.FileName).ToLower();
                            str_fname = FileUploadRainwaterHarvesting.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadRainwaterHarvesting.PostedFile))
                                {
                                    if (FileUploadRainwaterHarvesting.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadRainwaterHarvesting.PostedFile);
                                        obj_insertMiningNewApplicationAttachment.ContentType = FileUploadRainwaterHarvesting.PostedFile.ContentType;
                                        obj_insertMiningNewApplicationAttachment.AttachmentPath = FileUploadRainwaterHarvesting.FileName;
                                        obj_insertMiningNewApplicationAttachment.FileExtension = str_ext;

                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageRainwaterHarvesting.Text = HttpUtility.HtmlEncode("File can not upload. It has more than 5 MB size");
                                        lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageRainwaterHarvesting.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                    lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }

                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageRainwaterHarvesting.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageRainwaterHarvesting.Text = HttpUtility.HtmlEncode("Please select a file..!!");
                            lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningNewApplicationAttachment.ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentCode = obj_MiningNewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeAttachCode;

                        obj_insertMiningNewApplicationAttachment.AttachmentName = txtRainwaterHarvesting.Text; ;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageRainwaterHarvesting.Text = HttpUtility.HtmlEncode("File Upload Success");
                            lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageRainwaterHarvesting.Text = HttpUtility.HtmlEncode("File Upload Failed");
                            lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;

                        }
                        clearMessage();
                        lblMessageRainwaterHarvesting.ForeColor = Color.Green;
                        lblMessageRainwaterHarvesting.Text = HttpUtility.HtmlEncode(obj_insertMiningNewApplicationAttachment.CustumMessage);

                        BindMiningNewApplicationAttachmentRainWaterharvestingDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                        ClearAttachmentFileName();
                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";
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
                else
                {
                    lblMessageRainwaterHarvesting.Text = HttpUtility.HtmlEncode("Maximum number of files to be uploaded is 5");
                    lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUplodUndertaking_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "UploadUndertaking";
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationForNoLimit = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
                arr_MiningNewApplicationAttachmentList = obj_MiningNewApplicationForNoLimit.GetUndertakingAttachmentList();
                if (arr_MiningNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_insertMiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment> lst_MiningNewApplicationAttachmentList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadUndertaking.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadUndertaking.PostedFile.FileName).ToLower();
                            str_fname = FileUploadUndertaking.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadUndertaking.PostedFile))
                                {
                                    if (FileUploadUndertaking.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadUndertaking.PostedFile);
                                        obj_insertMiningNewApplicationAttachment.ContentType = FileUploadUndertaking.PostedFile.ContentType;
                                        obj_insertMiningNewApplicationAttachment.AttachmentPath = FileUploadUndertaking.FileName;
                                        obj_insertMiningNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageUndertaking.Text = HttpUtility.HtmlEncode("File can not upload. It has more than 5 MB size");
                                        lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageUndertaking.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                    lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }

                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageUndertaking.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageUndertaking.Text = HttpUtility.HtmlEncode("Please select a file..!!");
                            lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningNewApplicationAttachment.ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentCode = obj_MiningNewApplication.Undertaking.UndertakingAttachCode;

                        obj_insertMiningNewApplicationAttachment.AttachmentName = txtUndertaking.Text; ;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageUndertaking.Text = HttpUtility.HtmlEncode("File Upload Success");
                            lblMessageUndertaking.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageUndertaking.Text = HttpUtility.HtmlEncode("File Upload Failed");
                            lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;

                        }
                        clearMessage();
                        lblMessageUndertaking.ForeColor = Color.Green;
                        lblMessageUndertaking.Text = HttpUtility.HtmlEncode(obj_insertMiningNewApplicationAttachment.CustumMessage);

                        BindMiningNewApplicationAttachmentUndertalkingDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                        ClearAttachmentFileName();
                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";
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
                else
                {
                    lblMessageUndertaking.Text = HttpUtility.HtmlEncode("Maximum number of files to be uploaded is 5");
                    lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUploadReferralLetter_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "UploadReferralLetter";
                int int_CountAttForRefLetter = 0;
                int int_ReferralLetterCode = Convert.ToInt32(ddlReferralLetter.SelectedValue);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADReferralLetter obj_MiningNewReferralLetterk = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADReferralLetter(Convert.ToInt32(lblMiningApplicationCodeFrom.Text), int_ReferralLetterCode);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationForNoLimit = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
                if (obj_MiningNewReferralLetterk.ReferralLetterAttachCode == 0)
                    int_CountAttForRefLetter = 0;
                else
                {
                    arr_MiningNewApplicationAttachmentList = obj_MiningNewApplicationForNoLimit.GetCopyOfReferralLetterAttachmentList(obj_MiningNewReferralLetterk.ReferralLetterAttachCode, NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment.SortingFieldForAttachment.NoSorting);
                    int_CountAttForRefLetter = arr_MiningNewApplicationAttachmentList.Count();
                }
                if (int_CountAttForRefLetter < AttachmentNumberLimit())
                {

                    string str_fname;
                    string str_ext;
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADReferralLetter obj_MiningNewReferralLetter = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADReferralLetter(Convert.ToInt32(lblMiningApplicationCodeFrom.Text), Convert.ToInt32(ddlReferralLetter.SelectedValue));
                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    int int_ReferralLetterAttachCode;
                    try
                    {

                        int_ReferralLetterAttachCode = obj_MiningNewReferralLetter.ReferralLetterAttachCode;
                        if (obj_MiningNewReferralLetter.ReferralLetterAttachCode == 0)
                        {
                            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADReferralLetter obj_MiningNewReferralLetterForAdd = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADReferralLetter();
                            obj_MiningNewReferralLetterForAdd.ApplicationCode = Convert.ToInt32(lblMiningApplicationCodeFrom.Text);
                            obj_MiningNewReferralLetterForAdd.ReferralLetterTypeCode = Convert.ToInt32(ddlReferralLetter.SelectedValue);
                            obj_MiningNewReferralLetterForAdd.CreatedByExUC = obj_externalUser.ExternalUserCode;
                            if (obj_MiningNewReferralLetterForAdd.Add() != 1)
                            {

                                lblMessageReferralLetter.Text = obj_MiningNewReferralLetterForAdd.CustumMessage;
                                lblMessageReferralLetter.ForeColor = Color.Green;
                                return;
                            }

                            int_ReferralLetterAttachCode = obj_MiningNewReferralLetterForAdd.ReferralLetterAttachCode;
                        }

                        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_insertMiningNewApplicationAttachmentAferAdd = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
                        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationAfterAdd = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                        List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment> lst_MiningNewApplicationAttachmentList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment>();
                        byte[] buffer = new byte[1];

                        if (FileUploadReferralLetter.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadReferralLetter.PostedFile.FileName).ToLower();
                            str_fname = FileUploadReferralLetter.FileName;

                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {

                                if (NOCAPExternalUtility.IsValidFile(FileUploadReferralLetter.PostedFile))
                                {

                                    if (FileUploadReferralLetter.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningNewApplicationAttachmentAferAdd.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadReferralLetter.PostedFile);
                                        obj_insertMiningNewApplicationAttachmentAferAdd.ContentType = FileUploadReferralLetter.PostedFile.ContentType;
                                        obj_insertMiningNewApplicationAttachmentAferAdd.AttachmentPath = FileUploadReferralLetter.FileName;
                                        obj_insertMiningNewApplicationAttachmentAferAdd.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageReferralLetter.Text = HttpUtility.HtmlEncode("File can not upload. It has more than 5 MB size");
                                        lblMessageReferralLetter.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageExtra.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                    lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }

                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageReferralLetter.Text = HttpUtility.HtmlEncode("Not a valid file!!..Select an other file!!");
                                lblMessageReferralLetter.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageReferralLetter.Text = HttpUtility.HtmlEncode("Please select a file..!!");
                            lblMessageReferralLetter.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningNewApplicationAttachmentAferAdd.ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                        obj_insertMiningNewApplicationAttachmentAferAdd.AttachmentCode = int_ReferralLetterAttachCode;

                        obj_insertMiningNewApplicationAttachmentAferAdd.AttachmentName = txtReferralLetter.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUserAfterAdd = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningNewApplicationAttachmentAferAdd.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningNewApplicationAttachmentAferAdd.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageReferralLetter.Text = HttpUtility.HtmlEncode("File Upload Success");
                            lblMessageReferralLetter.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageReferralLetter.Text = HttpUtility.HtmlEncode("File Upload Failed");
                            lblMessageReferralLetter.ForeColor = System.Drawing.Color.Red;

                        }
                        clearMessage();
                        lblMessageReferralLetter.Text = HttpUtility.HtmlEncode(obj_insertMiningNewApplicationAttachmentAferAdd.CustumMessage);
                        lblMessageReferralLetter.ForeColor = Color.Green;
                        obj_MiningNewReferralLetter = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADReferralLetter(Convert.ToInt32(lblMiningApplicationCodeFrom.Text), Convert.ToInt32(ddlReferralLetter.SelectedValue));
                        // BindMiningNewApplicationAttachmentRefferalLetterDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text), obj_MiningNewReferralLetter.ReferralLetterAttachCode);
                        ClearAttachmentFileName();

                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";
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
                else
                {
                    lblMessageReferralLetter.Text = HttpUtility.HtmlEncode("Maximum number of files to be uploaded is 5");
                    lblMessageReferralLetter.ForeColor = System.Drawing.Color.Red;
                }

            }

        }
    }
    protected void btnUploadExtra_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "UploadExtraAttachment";
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationForNoLimit = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
                arr_MiningNewApplicationAttachmentList = obj_MiningNewApplicationForNoLimit.GetExtraAttachmentList();
                if (arr_MiningNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_insertMiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment> lst_MiningNewApplicationAttachmentList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadExtra.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadExtra.PostedFile.FileName).ToLower();
                            str_fname = FileUploadExtra.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadExtra.PostedFile))
                                {
                                    if (FileUploadExtra.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadExtra.PostedFile);
                                        obj_insertMiningNewApplicationAttachment.ContentType = FileUploadExtra.PostedFile.ContentType;
                                        obj_insertMiningNewApplicationAttachment.AttachmentPath = FileUploadExtra.FileName;
                                        obj_insertMiningNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageExtra.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageExtra.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }

                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageExtra.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageExtra.Text = HttpUtility.HtmlEncode("Please select a file..!!");
                            lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningNewApplicationAttachment.ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentCode = obj_MiningNewApplication.MiningNewExtraAttachmentCode;

                        obj_insertMiningNewApplicationAttachment.AttachmentName = txtExtraAttachment.Text; ;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageExtra.Text = "File Upload Success";
                            lblMessageExtra.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageExtra.Text = "File Upload Failed";
                            lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                        }
                        clearMessage();
                        lblMessageExtra.ForeColor = Color.Green;
                        lblMessageExtra.Text = HttpUtility.HtmlEncode(obj_insertMiningNewApplicationAttachment.CustumMessage);

                        BindMiningNewApplicationAttachmentExtraAttachmentsDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                        ClearAttachmentFileName();
                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";
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
                else
                {
                    lblMessageExtra.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUploadBharatKoshReciept_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "File Upload Bharat Kosh Reciept";
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationForNoLimit = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
                arr_MiningNewApplicationAttachmentList = obj_MiningNewApplicationForNoLimit.GetBharatKoshRecieptAttachmentList();

                if (arr_MiningNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_insertMiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment> lst_MiningNewApplicationAttachmentList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadBharatKoshReciept.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadBharatKoshReciept.PostedFile.FileName).ToLower();
                            str_fname = FileUploadBharatKoshReciept.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadBharatKoshReciept.PostedFile))
                                {
                                    if (FileUploadBharatKoshReciept.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadBharatKoshReciept.PostedFile);
                                        obj_insertMiningNewApplicationAttachment.ContentType = FileUploadBharatKoshReciept.PostedFile.ContentType;
                                        obj_insertMiningNewApplicationAttachment.AttachmentPath = FileUploadBharatKoshReciept.FileName;
                                        obj_insertMiningNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageBharatKoshReciept.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageBharatKoshReciept.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageBharatKoshReciept.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageBharatKoshReciept.Text = "Please select a file..!!";
                            lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertMiningNewApplicationAttachment.ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentCode = obj_MiningNewApplication.MiningNewBharatKoshRecieptAttachmentCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentName = txtBharatKoshReciept.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertMiningNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertMiningNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageBharatKoshReciept.Text = obj_insertMiningNewApplicationAttachment.CustumMessage;
                            lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;

                        }

                        lblMessageBharatKoshReciept.ForeColor = Color.Green;
                        lblMessageBharatKoshReciept.Text = obj_insertMiningNewApplicationAttachment.CustumMessage;
                        txtBharatKoshReciept.Text = "";
                        BindMiningNewApplicationAttachmentBharatKoshRecieptDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                        lblMessageExtra.Text = "";
                        lblMessageGroundwaterAvailability.Text = "";
                        lblMessageRainwaterHarvesting.Text = "";
                        lblMessageSourceofAvailabilityofSurfaceWater.Text = "";
                        lblMessageUndertaking.Text = "";
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
                else
                {
                    lblMessageExtra.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUploadWetlandArea_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "File Upload Wetland Area";
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationForNoLimit = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
                arr_MiningNewApplicationAttachmentList = obj_MiningNewApplicationForNoLimit.GetWetlandAreaAttachmentList();

                if (arr_MiningNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_insertMiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment> lst_MiningNewApplicationAttachmentList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadWetlandArea.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadWetlandArea.PostedFile.FileName).ToLower();
                            str_fname = FileUploadWetlandArea.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadWetlandArea.PostedFile))
                                {
                                    if (FileUploadWetlandArea.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadWetlandArea.PostedFile);
                                        obj_insertMiningNewApplicationAttachment.ContentType = FileUploadWetlandArea.PostedFile.ContentType;
                                        obj_insertMiningNewApplicationAttachment.AttachmentPath = FileUploadWetlandArea.FileName;
                                        obj_insertMiningNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageWetlanArea.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageWetlanArea.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageWetlanArea.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageWetlanArea.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageWetlanArea.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageWetlanArea.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageWetlanArea.Text = "Please select a file..!!";
                            lblMessageWetlanArea.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertMiningNewApplicationAttachment.ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentCode = obj_MiningNewApplication.WetlandAreaAttCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentName = txtWetlandArea.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertMiningNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertMiningNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageWetlanArea.Text = obj_insertMiningNewApplicationAttachment.CustumMessage;
                            lblMessageWetlanArea.ForeColor = System.Drawing.Color.Red;

                        }


                        BindWetlandAreaAttachmentDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageWetlanArea.ForeColor = Color.Green;
                        lblMessageWetlanArea.Text = obj_insertMiningNewApplicationAttachment.CustumMessage;


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
                else
                {
                    lblMessageExtra.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUploadPenalty_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "File Upload Wetland Area";
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationForNoLimit = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
                arr_MiningNewApplicationAttachmentList = obj_MiningNewApplicationForNoLimit.GetPenaltyAttachmentList();

                if (arr_MiningNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_insertMiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment> lst_MiningNewApplicationAttachmentList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadPenalty.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadPenalty.PostedFile.FileName).ToLower();
                            str_fname = FileUploadPenalty.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadPenalty.PostedFile))
                                {
                                    if (FileUploadPenalty.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadPenalty.PostedFile);
                                        obj_insertMiningNewApplicationAttachment.ContentType = FileUploadPenalty.PostedFile.ContentType;
                                        obj_insertMiningNewApplicationAttachment.AttachmentPath = FileUploadPenalty.FileName;
                                        obj_insertMiningNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessagePenalty.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessagePenalty.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessagePenalty.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessagePenalty.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessagePenalty.Text = "Not a valid file!!..Select an other file!!";
                                lblMessagePenalty.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessagePenalty.Text = "Please select a file..!!";
                            lblMessagePenalty.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertMiningNewApplicationAttachment.ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentCode = obj_MiningNewApplication.PenaltyAttCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentName = txtPenalty.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertMiningNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertMiningNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessagePenalty.Text = obj_insertMiningNewApplicationAttachment.CustumMessage;
                            lblMessagePenalty.ForeColor = System.Drawing.Color.Red;
                        }
                        BindPenaltyAttachmentDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessagePenalty.ForeColor = Color.Green;
                        lblMessagePenalty.Text = obj_insertMiningNewApplicationAttachment.CustumMessage;


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
                else
                {
                    lblMessageExtra.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void btnUploadAplicationSignatureSeal_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "File Upload Aplication with Signature and Seal";
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplicationForNoLimit = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
                arr_MiningNewApplicationAttachmentList = obj_MiningNewApplicationForNoLimit.GetSignedDocAttachmentList();

                if (arr_MiningNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment obj_insertMiningNewApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment> lst_MiningNewApplicationAttachmentList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadAplicationSignatureSeal.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadAplicationSignatureSeal.PostedFile.FileName).ToLower();
                            str_fname = FileUploadAplicationSignatureSeal.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadAplicationSignatureSeal.PostedFile))
                                {
                                    if (FileUploadAplicationSignatureSeal.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadAplicationSignatureSeal.PostedFile);
                                        obj_insertMiningNewApplicationAttachment.ContentType = FileUploadAplicationSignatureSeal.PostedFile.ContentType;
                                        obj_insertMiningNewApplicationAttachment.AttachmentPath = FileUploadAplicationSignatureSeal.FileName;
                                        obj_insertMiningNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageAplicationSignatureSeal.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageAplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageAplicationSignatureSeal.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageAplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageAplicationSignatureSeal.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageAplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageAplicationSignatureSeal.Text = "Please select a file..!!";
                            lblMessageAplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertMiningNewApplicationAttachment.ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentCode = obj_MiningNewApplication.SignedDocAttCode;
                        obj_insertMiningNewApplicationAttachment.AttachmentName = txtApplicationSignatureSeal.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertMiningNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageAplicationSignatureSeal.Text = obj_insertMiningNewApplicationAttachment.CustumMessage;
                            lblMessageAplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;

                        }

                        lblMessageAplicationSignatureSeal.ForeColor = Color.Green;
                        lblMessageAplicationSignatureSeal.Text = obj_insertMiningNewApplicationAttachment.CustumMessage;
                        txtApplicationSignatureSeal.Text = "";
                        BindMiningNewApplicationAttachmentSignedDocDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                        lblMessageExtra.Text = "";
                        lblMessageGroundwaterAvailability.Text = "";
                        lblMessageRainwaterHarvesting.Text = "";
                        lblMessageSourceofAvailabilityofSurfaceWater.Text = "";
                        lblMessageUndertaking.Text = "";
                        lblMessageBharatKoshReciept.Text = "";
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
                else
                {
                    lblMessageExtra.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    #endregion

    #region Selection Changed
    protected void ddlReferralLetter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                int int_ReferralLetterCode = Convert.ToInt32(ddlReferralLetter.SelectedValue);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADReferralLetter obj_MiningNewReferralLetter = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADReferralLetter(Convert.ToInt32(lblMiningApplicationCodeFrom.Text), int_ReferralLetterCode);
                // BindMiningNewApplicationAttachmentRefferalLetterDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text), obj_MiningNewReferralLetter.ReferralLetterAttachCode);
                lblMessageReferralLetter.Text = "";
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    #endregion

    #region ViewFile

    protected void lbtnViewFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {
            try
            {
                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_MiningNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINSADAppDownloadFiles(lng_MiningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;
            }
        }
    }
    protected void lbtnLocationMapViewFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {
            try
            {
                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_MiningNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINSADAppDownloadFiles(lng_MiningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }
    protected void lbtnTopoSketchViewFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {
            try
            {
                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_MiningNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINSADAppDownloadFiles(lng_MiningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }
    protected void lbtnOwnershipViewFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {
            try
            {
                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_MiningNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINSADAppDownloadFiles(lng_MiningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }
    protected void lbtnSurfaceWaterViewFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {
            try
            {
                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_MiningNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINSADAppDownloadFiles(lng_MiningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }
    protected void lbtnFlowDirectionViewFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {
            try
            {
                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_MiningNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINSADAppDownloadFiles(lng_MiningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }
    protected void lbtnPumpedWaterViewFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {
            try
            {
                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_MiningNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINSADAppDownloadFiles(lng_MiningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }
    protected void lbtnRegimeMapViewFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_MiningNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINSADAppDownloadFiles(lng_MiningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }
    protected void lbtnObservationWellsViewFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {
            try
            {
                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_MiningNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINSADAppDownloadFiles(lng_MiningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }
    protected void lbtnGeneralQualityViewFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {
            try
            {
                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_MiningNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINSADAppDownloadFiles(lng_MiningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }
    protected void lbtnGroundWaterAvailabilityViewFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {
            try
            {
                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_MiningNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINSADAppDownloadFiles(lng_MiningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }
    protected void lbtnRainwaterHarvestingViewFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {
            try
            {
                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_MiningNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINSADAppDownloadFiles(lng_MiningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }
    protected void lbtnUnderTakingViewFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {
            try
            {
                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_MiningNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINSADAppDownloadFiles(lng_MiningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }
    protected void lbtnRefferalLetterViewFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {
            try
            {
                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_MiningNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINSADAppDownloadFiles(lng_MiningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }
    protected void lbtnExtraAttViewFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {
            try
            {
                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_MiningNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINSADAppDownloadFiles(lng_MiningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }
    protected void lbtnTORECApprovalLetterAttViewFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {
            try
            {
                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_MiningNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINSADAppDownloadFiles(lng_MiningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }
    protected void lbtnBharatKoshRecieptViewFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
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
                    long lng_miningNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINSADAppDownloadFiles(lng_miningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

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
                Session["CSRF"] = hidCSRF.Value;
            }
        }
    }

    protected void lbtnAplicationSignatureSealViewFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
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
                    long lng_miningNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINSADAppDownloadFiles(lng_miningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

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
                Session["CSRF"] = hidCSRF.Value;
            }
        }
    }

    #endregion

    void OTPVerifyEnavleDesable()
    {

        MiningNewSADApplication obj_MiningNewSADApplication = new MiningNewSADApplication(Convert.ToInt64(lblApplicationCode.Text));
        if (obj_MiningNewSADApplication != null && obj_MiningNewSADApplication.ApplicationCode > 0)
        {
            if (obj_MiningNewSADApplication.ImpactAssOCSOTPVerified == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.ImpactAssOCSOTPVerifiedYesNo.Yes)
            {
                ddlConsultant.ClearSelection();
                ddlConsultant.Items.FindByValue(Convert.ToString(obj_MiningNewSADApplication.ImpactAssOCSOTPVerifiedByCC)).Selected = true;
                ddlConsultant.Enabled = false;
                txtImpactReportOCSOTP.Enabled = false;
                btnOTPVerify.Enabled = false;
                lblOTPVerified.Text = Convert.ToString(obj_MiningNewSADApplication.ImpactAssOCSOTPVerified);
                btnSendOTP.Enabled = false;
            }
            else
            {
                ddlConsultant.Enabled = true;
                txtImpactReportOCSOTP.Enabled = true;
                btnOTPVerify.Enabled = true;
                lblOTPVerified.Text = Convert.ToString(NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.ImpactAssOCSOTPVerifiedYesNo.No);
                btnSendOTP.Enabled = true;
            }
        }

    }

    protected void btnOTPVerify_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["ConsultantOTP"]) == txtImpactReportOCSOTP.Text)
        {
            MiningNewSADApplication obj_MiningNewSADApplication = new MiningNewSADApplication(Convert.ToInt64(lblApplicationCode.Text));
            if (obj_MiningNewSADApplication.ApplicationCode > 0)
            {
                obj_MiningNewSADApplication.ImpactAssOCSOTPVerified = NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.ImpactAssOCSOTPVerifiedYesNo.Yes;
                obj_MiningNewSADApplication.ImpactAssOCSOTPVerifiedByCC = Convert.ToInt32(ddlConsultant.SelectedValue);
                obj_MiningNewSADApplication.ModifiedByExUC = Convert.ToInt64(Session["ExternalUserCode"]); ;
                int int_result = obj_MiningNewSADApplication.SetImpactAssOCSOTPVerified();
                if (int_result == 1)
                {
                    ddlConsultant.SelectedIndex = -1;
                    txtImpactReportOCSOTP.Text = "";
                    lblMessageGroundwaterAvailability.Text = "OTP Verifired successfully.";
                    lblMessageGroundwaterAvailability.ForeColor = Color.Green;
                    OTPVerifyEnavleDesable();
                    tableIAR.Visible = true;
                    return;
                }
                else
                {
                    lblMessageGroundwaterAvailability.Text = "OTP Verification failed.";
                    lblMessageGroundwaterAvailability.ForeColor = Color.Red;
                    tableIAR.Visible = false;
                    return;
                }

            }
            else
            {
                lblMessageGroundwaterAvailability.Text = "Error on page.";
                lblMessageGroundwaterAvailability.ForeColor = Color.Red;
                tableIAR.Visible = false;
                return;
            }
        }
        else
        {
            lblMessageGroundwaterAvailability.Text = "Invalid OTP, Please try again";
            lblMessageGroundwaterAvailability.ForeColor = Color.Red;
            tableIAR.Visible = false;
            return;
        }

    }

    protected void btnSendOTP_Click(object sender, EventArgs e)
    {
        string EmailServerName = "";
        string strSMSUserName = "";

        if (ddlConsultant.SelectedIndex > 0)
        {
            ConsultantDetail obj_ConsultantDetail = new ConsultantDetail(Convert.ToInt32(ddlConsultant.SelectedValue));
            if (obj_ConsultantDetail != null && obj_ConsultantDetail.ConsultantCode > 0)
            {
                string OTPMessage = "";
                string OTP = "";
                OTP = NOCAPExternalUtility.GetRandomNumber();
                Session["ConsultantOTP"] = OTP;

                if (!NOCAPExternalUtility.IsNumeric(obj_ConsultantDetail.MobileNumber))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('Invalid Mobile Number !!');", true);
                    return;
                }

                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_miningNewSADApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt64(lblApplicationCode.Text));
                if (obj_miningNewSADApplication.ApplicationCode > 0)
                {

                    if (SMSUtility.IsSendSMSEnable())
                    {
                        OTPMessage = "Do not share OTP with anyone. OTP for submission of IAR / CHR to CGWA in respect of your application name : " + obj_miningNewSADApplication.NameOfMining + " is :" + OTP;
                        string msgRes = SMSUtility.sendOTPtoMobile(OTPMessage, obj_ConsultantDetail.MobileNumber, "1007165035970164831", out strSMSUserName);
                        //string msgRes = "Platform accepted";
                        // lblOTPMsg.Text = OTPMessage;
                        if (msgRes.Trim() == "Platform accepted")
                        {
                            // SaveSMSAlert();
                            ClientScript.RegisterStartupScript(this.GetType(), "SMSAlert", "alert('One time Password(OTP) has been Sent to your Mobile No, Enter OTP to Complete Your verification Process');", true);

                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "SMSAlert", "alert('Error in sending OTP in mobile number');", true);
                        }

                    }
                    if (EmailUtility.IsSendEmailEnable())
                    {

                        //for Address 
                        string str_TownName = "";
                        string str_VillageName = "";

                        string str_StateName = new NOCAP.BLL.Master.State(obj_miningNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
                        string str_DistrictName = new NOCAP.BLL.Master.District(obj_miningNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_miningNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName;
                        string str_SubDistrictName = new NOCAP.BLL.Master.SubDistrict(obj_miningNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_miningNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_miningNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;

                        if (obj_miningNewSADApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown == NOCAP.BLL.Common.Address.VillageOrTownOption.Town)
                        {
                            str_TownName = "Town Name : ";
                            str_TownName = str_TownName + new NOCAP.BLL.Master.Town(obj_miningNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_miningNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_miningNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_miningNewSADApplication.ProposedLocation.ProposedLocationAddress.TownCode).TownName;
                            str_TownName = str_TownName + ", ";
                        }
                        if (obj_miningNewSADApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown == NOCAP.BLL.Common.Address.VillageOrTownOption.Village)
                        {
                            str_VillageName = "Village Name : ";
                            str_VillageName = str_VillageName + new NOCAP.BLL.Master.Village(obj_miningNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_miningNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_miningNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_miningNewSADApplication.ProposedLocation.ProposedLocationAddress.VillageCode).VillageName;
                            str_VillageName = str_VillageName + ", ";
                        }
                        //for Address


                        string strBody = "<table style='max-width: 600px;margin: auto;width: 100%;border: 1px solid #f9f9f9;'><tr><td style='padding:30px;' colspan='3'><p>Dear <span>" + obj_ConsultantDetail.ConsultantName + "</span></p><p>Your One Time Password (OTP) for submission of Impact Assessment Report / Comprehensive Hydrogeological Report to CGWA in respect of Application for <span style='font-style: italic;'> " + obj_miningNewSADApplication.NameOfMining + ", " + str_VillageName + str_TownName + " Sub District : " + str_SubDistrictName + ", District : " + str_DistrictName + ", State : " + str_StateName + " </span> is mentioned below.</p><p>Please enter this OTP in the required field to proceed further.</p></td></tr><tr style='background-color: #1d65a3;'><td style='color: #fff;text-align: center; padding: 5px 0px;' colspan='3'>DETAILS</td></tr><tr><td style='padding-left: 30px;'><strong>One Time Password (OTP)</strong></td><td style='padding-right: 30px;' colspan='2'><strong>" + OTP + "</strong></td></tr><tr><td  colspan='3' style='padding:30px;'><p>The above mentioned OTP is valid for 15 minutes.</p><p>Sincerely, <br /><strong>CGWA, New Delhi</strong></p></td></tr></table>";


                        // string strBody = "<p>Sir/Madam,</p><p> </br></br>Do not share OTP with anyone. OTP for submission of IAR / CHR to CGWA in respect of your application name : " + obj_miningNewSADApplication.NameOfMining + " is <span style='color: blue;'> " + OTP + "</span> </p><p><br />  This is an auto-generated email.&nbsp; Do not reply to this email.<br />  </p>";
                        bool boolResult = EmailUtility.SendMail(out EmailServerName, StrTo: obj_ConsultantDetail.EmailID, StrSubject: "CGWA, New Delhi - OTP for submission of IAR/ CHR", StrBody: strBody);

                        if (boolResult == true)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "EmailAlert", "alert('One time Password(OTP) has been Sent to your email , Enter OTP to Complete Your verification Process');", true);

                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "EmailAlert", "alert('Error in sending OTP in your email');", true);
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error on page');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error on page');", true);
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Please Select Consultant');", true);
        }

    }

}