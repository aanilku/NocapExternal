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

public partial class ExternalUser_Expansion_INF_Attachment : System.Web.UI.Page
{
    string strPageName = "INFAtachment";
    string strActionName = "";
    string strStatus = "";
    long lngInfSubmitAppCode;
    decimal GroundWaterRequirement0KLD = 0;
    decimal GroundWaterRequirement10KLD = 10;
    decimal GroundWaterRequirement20KLD = 20;
    decimal GroundWaterRequirement100KLD = 100;
    public long InfSubmitAppCode
    {
        get
        {
            return lngInfSubmitAppCode;
        }
        set
        {
            lngInfSubmitAppCode = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblInfrastructureApplicationCodeFrom");
                        if (SourceLabel != null) { lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {

                    NOCAPExternalUtility.FillDropDownConsultant(ref ddlConsultant);
                    OTPVerifyEnavleDesable();


                    lblApplicationCode.Text = lblInfrastructureApplicationCodeFrom.Text;
                    BindWaterRequirementAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindAffidavitAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindInfrastructureNewApplicationAttachmentSourceofAvailabilityofSurfaceWaterDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindCertiNonAvaAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindRainwaterHarvestingAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindImpactReportAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindGroundWaterQualityAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindCompletionCertiAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindInstSTPAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindWetlandAreaAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindBharatKoshRecieptAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindSignedDocAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindPenaltyAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindExtraAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindProofOfOwnershipAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindProofofownershipLeaseoftankerAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

                    // BindInfrastructureNewApplicationAttachmentSitePlanDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    // BindInfrastructureNewApplicationAttachmentDocumentsofLocationMapDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    // BindInfrastructureNewApplicationAttachmentDocumentsofOwnershipAndLeasesofOwnershipDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    // BindInfrastructureNewApplicationAttachmentApprovalLetterOfStateGovtAgencyDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    //  BindInfrastructureNewApplicationAttachmentGroundwaterAvailabilityDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    //BindInfrastructureNewApplicationAttachmentUndertakingDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    //   BindInfrastructureNewApplicationAttachmentRefferalLetterDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

                }
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.NameOfInfrastructure);
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


    void OTPVerifyEnavleDesable()
    {

        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewSADApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
        if (obj_infrastructureNewSADApplication != null && obj_infrastructureNewSADApplication.InfrastructureNewApplicationCode > 0)
        {
            if (obj_infrastructureNewSADApplication.ImpactAssOCSOTPVerified == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.ImpactAssOCSOTPVerifiedYesNo.Yes)
            {
                ddlConsultant.ClearSelection();
                ddlConsultant.Items.FindByValue(Convert.ToString(obj_infrastructureNewSADApplication.ImpactAssOCSOTPVerifiedByCC)).Selected = true;
                ddlConsultant.Enabled = false;
                txtImpactReportOCSOTP.Enabled = false;
                btnOTPVerify.Enabled = false;
                lblOTPVerified.Text = Convert.ToString(obj_infrastructureNewSADApplication.ImpactAssOCSOTPVerified);
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
    private void BindGridView(GridView gv, long lngA_ApplicationCode, ref decimal decTotalGroundWaterRequirement, ref int AttCount)
    {
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList = null;

        if (gv.ID == "gvWaterRequirement")
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetWaterRequrementAttachmentList();
        else if (gv.ID == "gvAffidavit")
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetAffidavitNonAvaAttachmentList();
        else if (gv.ID == "gvSourceofAvailabilityofSurfaceWater")
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetSourceofAvailabilityofSurfaceWaterAttachmentList();
        else if (gv.ID == "gvCertiNonAva")
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetCertiNonAvaAttachmentList();
        else if (gv.ID == "gvRainwaterHarvesting")
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetRainwaterHarvestingAttachmentList(obj_infrastructureNewApplication);
        else if (gv.ID == "gvImpactReport")
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetImpactAssOCSAttachmentList();
        else if (gv.ID == "gvGroundwaterquality")
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetGroundwaterqualityAttachmentList();
        else if (gv.ID == "gvCompletionCerti")
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetCompletionCertiAttachmentList();
        else if (gv.ID == "gvInstSTP")
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetInstallationSTPAttachmentList();
        else if (gv.ID == "gvWetlandArea")
        {
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetWetlandAreaAttachmentList();
            switch (obj_infrastructureNewApplication.WetLandArea)
            {
                case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.WetLandAreaYesNo.Yes:
                    lblWetlandArea2.Visible = true;
                    break;
                case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.WetLandAreaYesNo.No:
                    lblWetlandArea2.Visible = false;
                    break;
                case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.WetLandAreaYesNo.NotDefine:
                    lblWetlandArea2.Visible = false;
                    break;
            }
        }
        else if (gv.ID == "gvBharatKoshReciept")
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetBharatKoshRecieptAttachmentList();
        else if (gv.ID == "gvAplicationSignatureSeal")
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetSignedDocAttachmentList();
        else if (gv.ID == "gvPenalty")
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetPenaltyAttachmentList();
        else if (gv.ID == "gvExtra")
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetExtraAttachmentList();
        else if (gv.ID == "gvProofOfOwnershipOfLand")
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetProofOwnershipLandAttachmentList();
        else if (gv.ID == "gvProofofownershipLeaseoftanker")
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetProofOwnershipTankerAttachmentList();
        gv.DataSource = arr_infrastructureNewApplicationAttachmentList;
        gv.DataBind();
        AttCount = arr_infrastructureNewApplicationAttachmentList.Length;
        decTotalGroundWaterRequirement = Convert.ToDecimal(obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement + (obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));

    }

    #region Private Function
    private void clearMessage()
    {
        lblMessageWaterRequirement.Text = "";
        txtWaterRequirement.Text = "";

        lblMessageAffidavit.Text = "";
        txtAffidavit.Text = "";

        lblMessageSourceofAvailabilityofSurfaceWater.Text = "";
        txtSourceofAvailabilityofSurfaceWater.Text = "";

        lblMessageCertiNonAva.Text = "";
        txtCertiNonAva.Text = "";

        lblMessageRainwaterHarvesting.Text = "";
        txtRainwaterHarvesting.Text = "";

        lblMessageImpactReport.Text = "";
        txtImpactReport.Text = "";

        lblMessageGroundwaterquality.Text = "";
        txtGroundwaterquality.Text = "";

        lblMessageCompletionCerti.Text = "";
        txtCompletionCerti.Text = "";

        lblMessageInstSTP.Text = "";
        txtInstSTP.Text = "";

        lblMessageWetlanArea.Text = "";
        txtWetlandArea.Text = "";

        lblMessageBharatKoshReciept.Text = "";
        txtBharatKoshReciept.Text = "";

        lblMessageAplicationSignatureSeal.Text = "";
        txtApplicationSignatureSeal.Text = "";

        lblMessagePenalty.Text = "";
        txtPenalty.Text = "";

        lblMessageExtra.Text = "";
        txtExtraAttachment.Text = "";





        lblMessageReferralLetter.Text = "";
        txtReferralLetter.Text = "";


        lblMessageGroundwaterAvailability.Text = "";
        txtgvGroundwaterAvailability.Text = "";
        lblMessageUndertaking.Text = "";
        txtUndertaking.Text = "";
        lblMessageSitePlan.Text = "";
        txtSitePlanAttachment.Text = "";

        lblMessageDocumentsofOwnership.Text = "";
        txtDocumentsofOwnership.Text = "";

    }
    private int AreaTypeCatCode(long lngA_ApplicationCode)
    {
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
        NOCAP.BLL.Master.SubDistrict obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(Convert.ToInt32(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode), Convert.ToInt32(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode), Convert.ToInt32(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode));
        NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_subDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_infrastructureNewApplication.ApplySubDistrictAreaCategoryKey);
        obj_subDistrictAreaTypeCategoryHistory = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory();
        string AreaTypeCatCode = obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode.ToString();
        NOCAP.BLL.Master.AreaTypeCategory obj_AreaTypeCategory = new NOCAP.BLL.Master.AreaTypeCategory(obj_subDistrictAreaTypeCategoryHistory.AreaTypeCode, obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode);
        return obj_AreaTypeCategory.AreaTypeCategoryCode;
    }
    private void BindWaterRequirementAttachmentDetails(long lngA_ApplicationCode)
    {
        try
        {

            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvWaterRequirement, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblWaterBalanceFlowChart.Text = HttpUtility.HtmlEncode("Ground Water Requirement (" + AttCount.ToString() + ")");

            #region Ground Water Requirement

            if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                lblWaterBalanceFlowChart2.Visible = true;
            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement20KLD))
                lblWaterBalanceFlowChart2.Visible = true;
            else if (decTotalGroundWaterRequirement > GroundWaterRequirement20KLD)
                lblWaterBalanceFlowChart2.Visible = true;
            else
                lblWaterBalanceFlowChart2.Visible = false;

            #endregion
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindAffidavitAttachmentDetails(long lngA_ApplicationCode)
    {
        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvAffidavit, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblAffidavit.Text = HttpUtility.HtmlEncode("Affidavit (" + AttCount.ToString() + ")");
            int int_areatypecatcode = AreaTypeCatCode(lngA_ApplicationCode);
            //if (lblAffidavit2.Visible)
            //{
            if (int_areatypecatcode == 2 || int_areatypecatcode == 3)
            {
                if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                    lblAffidavit2.Visible = true;
                else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement20KLD))
                    lblAffidavit2.Visible = true;
                else if (decTotalGroundWaterRequirement > GroundWaterRequirement20KLD)
                    lblAffidavit2.Visible = true;
            }
            else
            { lblAffidavit2.Visible = false; }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindInfrastructureNewApplicationAttachmentSourceofAvailabilityofSurfaceWaterDetails(long lngA_ApplicationCode)
    {
        try
        {

            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvSourceofAvailabilityofSurfaceWater, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblSourceWaterAvailability.Text = HttpUtility.HtmlEncode("Source Water Availability/Non-availability Certificate for Construction Purpose (" + AttCount.ToString() + ")");

            #region Source Water Availability/Non-availability Certificate
            int int_areatypecatcode = AreaTypeCatCode(lngA_ApplicationCode);
            //if (lblAffidavit2.Visible)
            //{
            if (int_areatypecatcode == 4 || int_areatypecatcode == 5)
            {
                if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                    lblSourceWaterAvailability2.Visible = true;
                else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement20KLD))
                    lblSourceWaterAvailability2.Visible = true;
                else if (decTotalGroundWaterRequirement > GroundWaterRequirement20KLD)
                    lblSourceWaterAvailability2.Visible = true;
                else
                    lblSourceWaterAvailability2.Visible = false;

            }
            else
            { lblSourceWaterAvailability2.Visible = false; }

            #endregion

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindCertiNonAvaAttachmentDetails(long lngA_ApplicationCode)
    {
        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvCertiNonAva, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblCertiNonAva.Text = HttpUtility.HtmlEncode("Certificate of non-availability of water (" + AttCount.ToString() + ")");
            if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                lblCertiNonAva2.Visible = false;
            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement20KLD))
                lblCertiNonAva2.Visible = false;
            else if (decTotalGroundWaterRequirement > GroundWaterRequirement20KLD)
                lblCertiNonAva2.Visible = false;

        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindRainwaterHarvestingAttachmentDetails(long lngA_ApplicationCode)
    {
        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvRainwaterHarvesting, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);

            #region Rain Water Harvesting/ Recharge 

            if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                lblRainWaterHarv2.Visible = false;
            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement20KLD))
                lblRainWaterHarv2.Visible = false;
            else if (decTotalGroundWaterRequirement > GroundWaterRequirement20KLD)
                lblRainWaterHarv2.Visible = false;
            else
                lblRainWaterHarv2.Visible = false;
            lblRainWaterHarv.Text = HttpUtility.HtmlEncode("Rain Water Harvesting/ Recharge (" + AttCount.ToString() + ")");
            #endregion
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindImpactReportAttachmentDetails(long lngA_ApplicationCode)
    {
        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvImpactReport, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblImpactReport.Text = HttpUtility.HtmlEncode("Impact Assessment Report by Accredited Consultant in case of Dewatering (" + AttCount.ToString() + ")");


            if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
            {
                lblImpactReport2.Visible = false;
                rowIAR.Visible = false;
            }
            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement20KLD))
            {
                lblImpactReport2.Visible = false;
                rowIAR.Visible = false;
            }
            else if (decTotalGroundWaterRequirement > GroundWaterRequirement20KLD)
            {
                lblImpactReport2.Visible = false;
                rowIAR.Visible = false;
            }
            else if (decTotalGroundWaterRequirement > GroundWaterRequirement100KLD)
            {

                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewSADApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
                if (obj_infrastructureNewSADApplication.WaterQuality != NOCAP.BLL.Common.CommonEnum.WaterQualityOption.RannKuchch)
                {
                    lblImpactReport2.Visible = true;
                    rowIAR.Visible = true;
                }
                else
                {
                    lblImpactReport2.Visible = false;
                    rowIAR.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindGroundWaterQualityAttachmentDetails(long lngA_ApplicationCode)
    {

        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvGroundwaterquality, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblGroundwaterquality.Text = HttpUtility.HtmlEncode("Ground Water Quality (" + AttCount.ToString() + ")");

            if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                lblGroundwaterquality2.Visible = true;
            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement20KLD))
                lblGroundwaterquality2.Visible = true;
            else if (decTotalGroundWaterRequirement > GroundWaterRequirement20KLD)
                lblGroundwaterquality2.Visible = true;
            else
                lblGroundwaterquality2.Visible = false;

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindCompletionCertiAttachmentDetails(long lngA_ApplicationCode)
    {
        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvCompletionCerti, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblCompletionCerti.Text = HttpUtility.HtmlEncode("Completion certificate from the concerned agency (" + AttCount.ToString() + ")");


            if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                lblCompletionCerti2.Visible = false;
            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement20KLD))
                lblCompletionCerti2.Visible = false;
            else if (decTotalGroundWaterRequirement > GroundWaterRequirement20KLD)
                lblCompletionCerti2.Visible = false;


        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindInstSTPAttachmentDetails(long lngA_ApplicationCode)
    {
        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvInstSTP, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblInstSTP.Text = HttpUtility.HtmlEncode("Installation of STP (For New Projects) (" + AttCount.ToString() + ")");


            if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                lblInstSTP2.Visible = false;
            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement20KLD))
                lblInstSTP2.Visible = false;
            else if (decTotalGroundWaterRequirement > GroundWaterRequirement20KLD)
                lblInstSTP2.Visible = false;


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
            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvWetlandArea, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblWetlandArea.Text = HttpUtility.HtmlEncode("Approval from Wetland Authority (" + AttCount.ToString() + ")");

            if (lblWetlandArea2.Visible)
            {
                if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                    lblWetlandArea2.Visible = true;
                else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement20KLD))
                    lblWetlandArea2.Visible = true;
                else if (decTotalGroundWaterRequirement > GroundWaterRequirement20KLD)
                    lblWetlandArea2.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindBharatKoshRecieptAttachmentDetails(long lngA_ApplicationCode)
    {
        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvBharatKoshReciept, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblBharatKosh.Text = HttpUtility.HtmlEncode("Bharat Kosh Reciept Attachment (" + AttCount.ToString() + ")");

        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindSignedDocAttachmentDetails(long lngA_ApplicationCode)
    {
        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvAplicationSignatureSeal, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblSigneddoc.Text = HttpUtility.HtmlEncode("Aplication with Signature and Seal Attachment (" + AttCount.ToString() + ")");


            if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                lblSigneddoc2.Visible = true;
            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement20KLD))
                lblSigneddoc2.Visible = true;
            else if (decTotalGroundWaterRequirement > GroundWaterRequirement20KLD)
                lblSigneddoc2.Visible = true;


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
            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvPenalty, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblPenalty.Text = HttpUtility.HtmlEncode("Penalty (" + AttCount.ToString() + ")");

            if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                lblPenalty2.Visible = false;
            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement20KLD))
                lblPenalty2.Visible = false;
            else if (decTotalGroundWaterRequirement > GroundWaterRequirement20KLD)
                lblPenalty2.Visible = false;

        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindExtraAttachmentDetails(long lngA_ApplicationCode)
    {

        try
        {


            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvExtra, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblExtraAttachment.Text = HttpUtility.HtmlEncode("Extra Attachment (" + AttCount.ToString() + ")");

            if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                lblExtraAttachment2.Visible = false;
            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement20KLD))
                lblExtraAttachment2.Visible = false;
            else if (decTotalGroundWaterRequirement > GroundWaterRequirement20KLD)
                lblExtraAttachment2.Visible = false;

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindProofOfOwnershipAttachmentDetails(long lngA_ApplicationCode)
    {

        try
        {

            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Master.ApplicationTypeCategory obj2 = new NOCAP.BLL.Master.ApplicationTypeCategory(obj.ApplicationTypeCode, obj.ApplicationTypeCategoryCode);

            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvProofOfOwnershipOfLand, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblProofofownershipoflandofsize200sqmormore.Text = HttpUtility.HtmlEncode("Proof Of Ownership of Land Attachment (" + AttCount.ToString() + ")");

            if (obj2.ApplicationTypeCategoryDesc == "Bulk Water Suppliers")
                lblProofofownershipofland.Visible = true;
            else
                lblProofofownershipofland.Visible = false;

            //if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
            //    lblProofofownershipofland.Visible = false;
            //else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement20KLD))
            //    lblProofofownershipofland.Visible = false;
            //else if (decTotalGroundWaterRequirement > GroundWaterRequirement20KLD)
            //    lblProofofownershipofland.Visible = false;

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void BindProofofownershipLeaseoftankerAttachmentDetails(long lngA_ApplicationCode)
    {

        try
        {

            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Master.ApplicationTypeCategory obj2 = new NOCAP.BLL.Master.ApplicationTypeCategory(obj.ApplicationTypeCode, obj.ApplicationTypeCategoryCode);

            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvProofofownershipLeaseoftanker, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblProofofownershipLeaseoftanker.Text = HttpUtility.HtmlEncode("Proof of Ownership / lease of Tanker Attachment (" + AttCount.ToString() + ")");
            if (obj2.ApplicationTypeCategoryDesc == "Bulk Water Suppliers")
                lblProofofownershipLease.Visible = true;
            else
                lblProofofownershipLease.Visible = false;
            //if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
            //    lblExtraAttachment2.Visible = false;
            //else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement20KLD))
            //    lblExtraAttachment2.Visible = false;
            //else if (decTotalGroundWaterRequirement > GroundWaterRequirement20KLD)
            //    lblExtraAttachment2.Visible = false;

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }










    private void BindInfrastructureNewApplicationAttachmentSitePlanDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetSitePlanAttachmentList();
            gvSitePlan.DataSource = arr_infrastructureNewApplicationAttachmentList;
            gvSitePlan.DataBind();
            decimal decTotalGroundWaterRequirement = Convert.ToDecimal(obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement + (obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));

            #region Site Plan

            //if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
            //    Tab1.Text = HttpUtility.HtmlEncode("*Site Plan (" + arr_infrastructureNewApplicationAttachmentList.Length.ToString() + ")");
            //else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement20KLD))
            //    Tab1.Text = HttpUtility.HtmlEncode("*Site Plan (" + arr_infrastructureNewApplicationAttachmentList.Length.ToString() + ")");
            //else if (decTotalGroundWaterRequirement > GroundWaterRequirement20KLD)
            //    Tab1.Text = HttpUtility.HtmlEncode("*Site Plan (" + arr_infrastructureNewApplicationAttachmentList.Length.ToString() + ")");
            //else
            //    Tab1.Text = HttpUtility.HtmlEncode("Site Plan (" + arr_infrastructureNewApplicationAttachmentList.Length.ToString() + ")");
            #endregion
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindInfrastructureNewApplicationAttachmentDocumentsofLocationMapDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            //List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_infrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetInfrastructureNewLocationMapAttachmentList();
            gvLocationMap.DataSource = arr_infrastructureNewApplicationAttachmentList;
            gvLocationMap.DataBind();
            decimal decTotalGroundWaterRequirement = Convert.ToDecimal(obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement + (obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));

            #region Location Map

            //if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
            //    Tab2.Text = HttpUtility.HtmlEncode("*Location Map (" + arr_infrastructureNewApplicationAttachmentList.Length.ToString() + ")");
            //else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement20KLD))
            //    Tab2.Text = HttpUtility.HtmlEncode("*Location Map (" + arr_infrastructureNewApplicationAttachmentList.Length.ToString() + ")");
            //else if (decTotalGroundWaterRequirement > GroundWaterRequirement20KLD)
            //    Tab2.Text = HttpUtility.HtmlEncode("*Location Map (" + arr_infrastructureNewApplicationAttachmentList.Length.ToString() + ")");
            //else
            //    Tab2.Text = HttpUtility.HtmlEncode("Location Map (" + arr_infrastructureNewApplicationAttachmentList.Length.ToString() + ")");
            #endregion

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindInfrastructureNewApplicationAttachmentGroundwaterAvailabilityDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            //List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_infrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetGroundwaterAvailabilityAttachmentList(obj_infrastructureNewApplication);
            gvGroundwaterAvailability.DataSource = arr_infrastructureNewApplicationAttachmentList;
            gvGroundwaterAvailability.DataBind();
            decimal decTotalGroundWaterRequirement = Convert.ToDecimal(obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement + (obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));

            #region Hydrogeological Report
            if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                lblHydrogeologicalReport2.Visible = false;
            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement20KLD))
                lblHydrogeologicalReport2.Visible = false;
            else if (decTotalGroundWaterRequirement > GroundWaterRequirement20KLD)
                lblHydrogeologicalReport2.Visible = true;
            else
                lblHydrogeologicalReport2.Visible = false;
            lblHydrogeologicalReport.Text = HttpUtility.HtmlEncode("Hydrogeological Report (" + arr_infrastructureNewApplicationAttachmentList.Length.ToString() + ")");
            #endregion

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindInfrastructureNewApplicationAttachmentUndertakingDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetUndertakingAttachmentList(obj_infrastructureNewApplication);//Argument
            gvUndertaking.DataSource = arr_infrastructureNewApplicationAttachmentList;
            gvUndertaking.DataBind();
            decimal decTotalGroundWaterRequirement = Convert.ToDecimal(obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement + (obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));

            #region Authorization Letter

            if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                lblAuthorizationLetter2.Visible = true;
            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement20KLD))
                lblAuthorizationLetter2.Visible = true;

            else if (decTotalGroundWaterRequirement > GroundWaterRequirement20KLD)
                lblAuthorizationLetter2.Visible = true;

            else
                lblAuthorizationLetter2.Visible = false;
            lblAuthorizationLetter.Text = HttpUtility.HtmlEncode("Authorization Letter (" + arr_infrastructureNewApplicationAttachmentList.Length.ToString() + ")");
            #endregion


        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindInfrastructureNewApplicationAttachmentRefferalLetterDetails(long lngA_ApplicationCode, int int_ReferralLetterAttachCode = 0)
    {
        try
        {
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
            arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetCopyOfReferralLetterAttachmentList(obj_infrastructureNewApplication, int_ReferralLetterAttachCode);//Argument
            gvReferralLetterAttachment.DataSource = arr_infrastructureNewApplicationAttachmentList;
            gvReferralLetterAttachment.DataBind();
            decimal decTotalGroundWaterRequirement = Convert.ToDecimal(obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement + (obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));

            #region Consent/ Approval of Government Agency

            if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                lblConsentApproval2.Visible = true;

            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement20KLD))
                lblConsentApproval2.Visible = true;
            else if (decTotalGroundWaterRequirement > GroundWaterRequirement20KLD)
                lblConsentApproval2.Visible = true;
            else
                lblConsentApproval2.Visible = false;
            lblConsentApproval.Text = HttpUtility.HtmlEncode("*Consent/ Approval of Government Agency (" + AttachmentsCountReferalLetter() + ")");
            #endregion

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
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

    private int AttachmentsCountReferalLetter()
    {
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
        int Count = 0;
        string SelectedValue = ddlReferralLetter.SelectedValue;
        for (int i = 1; i < ddlReferralLetter.Items.Count; i++)
        {
            ddlReferralLetter.SelectedIndex = i;
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter obj_InfrastructureNewReferralLetter = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text), Convert.ToInt32(ddlReferralLetter.SelectedValue));
            Count = Count + obj_InfrastructureNewApplication.GetCopyOfReferralLetterAttachmentList(obj_InfrastructureNewApplication, obj_InfrastructureNewReferralLetter.ReferralLetterAttachCode, NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment.SortingFieldForAttachment.NoSorting).Length;
        }
        ddlReferralLetter.SelectedValue = SelectedValue;
        return Count;
    }


    #endregion

    #region ViewFile_Click
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
                    long lng_infrastructureNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.INFSADAppDownloadFiles(lng_infrastructureNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

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
    protected void lbtnViewReferralLetterFile_Click(object sender, CommandEventArgs e)
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
                    long lng_InfrastructureNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    //NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_InfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment(lng_InfrastructureNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
                    //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                    //string str_fullPath = str_startPathFromConfig + obj_InfrastructureNewApplicationAttachment.AttachmentPath;

                    //Response.ContentType = ContentType;
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(str_fullPath));
                    //Response.WriteFile(str_fullPath);
                    NOCAPExternalUtility.INFSADAppDownloadFiles(lng_InfrastructureNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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

    #region RowDeleting




    protected void Affidavit_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete CertiNonAva";
                if (DeleteAttchment((GridView)sender, e, lblMessageAffidavit, strActionName) == 1)
                    BindAffidavitAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }

    protected void gvImpactReport_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete CertiNonAva";
                if (DeleteAttchment((GridView)sender, e, lblMessageImpactReport, strActionName) == 1)
                    BindImpactReportAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }
    protected void gvCertiNonAva_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete CertiNonAva";
                if (DeleteAttchment((GridView)sender, e, lblMessageCertiNonAva, strActionName) == 1)
                    BindCertiNonAvaAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }

    protected void gvCompletionCerti_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Completion Certificate";
                if (DeleteAttchment((GridView)sender, e, lblMessageCompletionCerti, strActionName) == 1)
                    BindCompletionCertiAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }
    protected void gvInstSTP_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                if (DeleteAttchment((GridView)sender, e, lblMessageInstSTP, strActionName) == 1)
                    BindInstSTPAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
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
                if (DeleteAttchment((GridView)sender, e, lblMessageWetlanArea, strActionName) == 1)
                    BindWetlandAreaAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
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
                strActionName = "File Delete CertiNonAva";
                if (DeleteAttchment((GridView)sender, e, lblMessagePenalty, strActionName) == 1)
                    BindPenaltyAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }


    protected void gvExtra_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "Delete File Extra";
                if (DeleteAttchment((GridView)sender, e, lblMessageExtra, strActionName) == 1)
                    BindExtraAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

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
    protected void gvReferralLetterAttachment_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "Deleted File Referral Latter";
                long lng_InfrastructureNewApplicationCode = Convert.ToInt32(gvReferralLetterAttachment.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvReferralLetterAttachment.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvReferralLetterAttachment.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_InfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment(lng_InfrastructureNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);


                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_InfrastructureNewApplicationAttachment.AttachmentPath;

                ///////////////////////


                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_InfrastructureNewApplicationAttachmentList;

                arr_InfrastructureNewApplicationAttachmentList = obj_InfrastructureNewApplication.GetCopyOfReferralLetterAttachmentList(obj_InfrastructureNewApplication, int_AttachmentCode, NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment.SortingFieldForAttachment.NoSorting);

                //-----------------------

                if (obj_InfrastructureNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Deleted Success";
                    lblMessageReferralLetter.Text = obj_InfrastructureNewApplicationAttachment.CustumMessage;
                    lblMessageReferralLetter.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}

                    if (arr_InfrastructureNewApplicationAttachmentList.Count() == 1)
                    {
                        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter obj_InfrastructureNewReferralLetterForDelete = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter(lng_InfrastructureNewApplicationCode, Convert.ToInt32(ddlReferralLetter.SelectedValue));
                        obj_InfrastructureNewReferralLetterForDelete.Delete();
                    }

                    int int_ReferralLetterCode = Convert.ToInt32(ddlReferralLetter.SelectedValue);
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter obj_InfrastructureNewReferralLetter = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text), int_ReferralLetterCode);
                    BindInfrastructureNewApplicationAttachmentRefferalLetterDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text), obj_InfrastructureNewReferralLetter.ReferralLetterAttachCode);
                }
                else
                {
                    strStatus = "File Deleting Failed";
                    lblMessageReferralLetter.Text = obj_InfrastructureNewApplicationAttachment.CustumMessage;
                    lblMessageReferralLetter.ForeColor = System.Drawing.Color.Red;
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
    protected void gvUndertaking_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "Delete File Autherization Letter";
                long lng_InfrastructureNewApplicationCode = Convert.ToInt32(gvUndertaking.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvUndertaking.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvUndertaking.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_InfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment(lng_InfrastructureNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);

                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_InfrastructureNewApplicationAttachment.AttachmentPath;

                if (obj_InfrastructureNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Deleted Success";
                    lblMessageUndertaking.Text = obj_InfrastructureNewApplicationAttachment.CustumMessage;
                    lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    BindInfrastructureNewApplicationAttachmentUndertakingDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Deleting Failed";
                    lblMessageUndertaking.Text = obj_InfrastructureNewApplicationAttachment.CustumMessage;
                    lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
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
    protected void gvRainwaterHarvesting_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "Delete File Rain Water Harvesting/ Recharge";
                if (DeleteAttchment((GridView)sender, e, lblMessageRainwaterHarvesting, strActionName) == 1)
                    BindRainwaterHarvestingAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }
    protected void gvGroundwaterAvailability_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "Delete File Ground Water Availability";
                long lng_InfrastructureNewApplicationCode = Convert.ToInt32(gvGroundwaterAvailability.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvGroundwaterAvailability.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvGroundwaterAvailability.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_InfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment(lng_InfrastructureNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_InfrastructureNewApplicationAttachment.AttachmentPath;


                if (obj_InfrastructureNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Deleted Success";
                    lblMessageGroundwaterAvailability.Text = obj_InfrastructureNewApplicationAttachment.CustumMessage;
                    lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    BindInfrastructureNewApplicationAttachmentGroundwaterAvailabilityDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Deleting Failed";
                    lblMessageGroundwaterAvailability.Text = obj_InfrastructureNewApplicationAttachment.CustumMessage;
                    lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
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

    protected void gvSitePlan_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "Delete File Site Plan with Location Map";
                long lng_InfrastructureNewApplicationCode = Convert.ToInt32(gvSitePlan.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvSitePlan.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvSitePlan.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_InfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment(lng_InfrastructureNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_InfrastructureNewApplicationAttachment.AttachmentPath;
                if (obj_InfrastructureNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Deleted Success";
                    lblMessageSitePlan.Text = obj_InfrastructureNewApplicationAttachment.CustumMessage;
                    lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    BindInfrastructureNewApplicationAttachmentSitePlanDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Deleting Failed";
                    lblMessageSitePlan.Text = obj_InfrastructureNewApplicationAttachment.CustumMessage;
                    lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
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

    protected void gvCertifiedRevenueSketch_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Certificate of Revenue Sketch";
                long lng_InfrastructureNewApplicationCode = Convert.ToInt32(gvLocationMap.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvLocationMap.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvLocationMap.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_InfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment(lng_InfrastructureNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_InfrastructureNewApplicationAttachment.AttachmentPath;


                if (obj_InfrastructureNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Deleted Success";
                    lblMessageLocationMap.Text = obj_InfrastructureNewApplicationAttachment.CustumMessage;
                    lblMessageLocationMap.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}

                    BindInfrastructureNewApplicationAttachmentDocumentsofLocationMapDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Deleting Failed";
                    lblMessageLocationMap.Text = obj_InfrastructureNewApplicationAttachment.CustumMessage;
                    lblMessageLocationMap.ForeColor = System.Drawing.Color.Red;

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

    protected void gvDocumentsofOwnership_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Document of Ownership";
                long lng_InfrastructureNewApplicationCode = Convert.ToInt32(gvDocumentsofOwnership.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvDocumentsofOwnership.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvDocumentsofOwnership.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_InfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment(lng_InfrastructureNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_InfrastructureNewApplicationAttachment.AttachmentPath;


                if (obj_InfrastructureNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Deleted Success";
                    lblMessageDocumentsofOwnership.Text = obj_InfrastructureNewApplicationAttachment.CustumMessage;
                    lblMessageDocumentsofOwnership.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    // BindInfrastructureNewApplicationAttachmentDocumentsofOwnershipAndLeasesofOwnershipDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Deleting Failed";
                    lblMessageDocumentsofOwnership.Text = obj_InfrastructureNewApplicationAttachment.CustumMessage;
                    lblMessageDocumentsofOwnership.ForeColor = System.Drawing.Color.Red;

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

    protected void gvSourceofAvailabilityofSurfaceWater_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Source Water Availability/Non-availability Certificate";
                long lng_InfrastructureNewApplicationCode = Convert.ToInt32(gvSourceofAvailabilityofSurfaceWater.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvSourceofAvailabilityofSurfaceWater.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvSourceofAvailabilityofSurfaceWater.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_InfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment(lng_InfrastructureNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_InfrastructureNewApplicationAttachment.AttachmentPath;


                if (obj_InfrastructureNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Deleting Success";
                    lblMessageSourceofAvailabilityofSurfaceWater.Text = obj_InfrastructureNewApplicationAttachment.CustumMessage;
                    lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    BindInfrastructureNewApplicationAttachmentSourceofAvailabilityofSurfaceWaterDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Deleting Failed";
                    lblMessageSourceofAvailabilityofSurfaceWater.Text = obj_InfrastructureNewApplicationAttachment.CustumMessage;
                    lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;

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

    protected void gvWaterRequirement_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "Delete File Water Balance Flow Chart";
                if (DeleteAttchment((GridView)sender, e, lblMessageWaterRequirement, strActionName) == 1)
                    BindWaterRequirementAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }
    protected void gvApprovalLetterOfStateGovtAgency_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "Delete Approval Letter of State";
                long lng_InfrastructureNewApplicationCode = Convert.ToInt32(gvApprovalLetterOfStateGovtAgency.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvApprovalLetterOfStateGovtAgency.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvApprovalLetterOfStateGovtAgency.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_InfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment(lng_InfrastructureNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_InfrastructureNewApplicationAttachment.AttachmentPath;


                if (obj_InfrastructureNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Deleted Success";
                    lblMessageGroundwaterAvailability.Text = obj_InfrastructureNewApplicationAttachment.CustumMessage;
                    lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    //  BindInfrastructureNewApplicationAttachmentApprovalLetterOfStateGovtAgencyDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Deleting Failed";
                    lblApprovalLetterOfStateGovtAgency.Text = obj_InfrastructureNewApplicationAttachment.CustumMessage;
                    lblApprovalLetterOfStateGovtAgency.ForeColor = System.Drawing.Color.Red;
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
    protected void gvGroundwaterquality_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "Delete File Ground Water Quality";
                if (DeleteAttchment((GridView)sender, e, lblMessageGroundwaterquality, strActionName) == 1)
                    BindGroundWaterQualityAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
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
                if (DeleteAttchment((GridView)sender, e, lblMessageBharatKoshReciept, strActionName) == 1)
                    BindBharatKoshRecieptAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

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
                if (DeleteAttchment((GridView)sender, e, lblMessageAplicationSignatureSeal, strActionName) == 1)
                    BindSignedDocAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

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

    protected void gvProofOfOwnershipOfLand_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                if (DeleteAttchment((GridView)sender, e, lblProofofownershipoflandofsize200sqm, strActionName) == 1)
                    BindProofOfOwnershipAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

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

    protected void gvProofofownershipLeaseoftanker_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Proof of ownership/Lease of tanker";
                if (DeleteAttchment((GridView)sender, e, lblProofofownershipLeaseTanker, strActionName) == 1)
                    BindProofofownershipLeaseoftankerAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

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
    private int DeleteAttchment(GridView gv, GridViewDeleteEventArgs e, Label lblMessage, string str_ActionName)
    {
        try
        {
            long lng_InfrastructureNewApplicationCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["ApplicationCode"]);
            int int_AttachmentCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["AttachmentCode"]);
            int int_AttachmentCodeSerialNumber = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
            lblMessage.ForeColor = System.Drawing.Color.Red;
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_infrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment(lng_InfrastructureNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
            clearMessage();
            if (obj_infrastructureNewApplicationAttachment.Delete() == 1)
            {
                lblMessage.Text = obj_infrastructureNewApplicationAttachment.CustumMessage;
                strStatus = "File Delete Success";
                return 1;
            }
            else
            {
                lblMessage.Text = obj_infrastructureNewApplicationAttachment.CustumMessage;
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

    #region Active Upload Button Click
    protected void btnUplodWaterRequirement_Click(object sender, EventArgs e)
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
                strActionName = "UploadWaterRequirement";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
                arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplicationForNoLimit.GetWaterRequrementAttachmentList();

                if (arr_infrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileName = "";
                    string str_newFileNameWithPath = "";


                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_infrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();

                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadWaterRequirement.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadWaterRequirement.PostedFile.FileName).ToLower();
                            str_fname = FileUploadWaterRequirement.FileName;

                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadWaterRequirement.PostedFile))
                                {
                                    if (FileUploadWaterRequirement.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {

                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadWaterRequirement.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = FileUploadWaterRequirement.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = FileUploadWaterRequirement.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageWaterRequirement.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageWaterRequirement.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageWaterRequirement.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }

                        else
                        {
                            lblMessageWaterRequirement.Text = "Please select a file..!!";
                            lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
                            return;
                        }



                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_infrastructureNewApplication.ApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.WaterRequrementAttachCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtWaterRequirement.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageWaterRequirement.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                            lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
                        }

                        BindWaterRequirementAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageWaterRequirement.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                        lblMessageWaterRequirement.ForeColor = Color.Green;
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
                    lblMessageWaterRequirement.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void btnUplodRainwaterHarvesting_Click(object sender, EventArgs e)
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
                strActionName = "UploadRainWaterHarvesting";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
                arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplicationForNoLimit.GetRainwaterHarvestingAttachmentList(obj_infrastructureNewApplicationForNoLimit);//Argument

                if (arr_infrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileName = "";
                    string str_newFileNameWithPath = "";


                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_infrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();

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

                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadRainwaterHarvesting.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = FileUploadRainwaterHarvesting.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = FileUploadRainwaterHarvesting.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageRainwaterHarvesting.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageRainwaterHarvesting.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageRainwaterHarvesting.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }

                        else
                        {
                            lblMessageRainwaterHarvesting.Text = "Please select a file..!!";
                            lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_infrastructureNewApplication.ApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_infrastructureNewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeAttachCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtRainwaterHarvesting.Text;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageRainwaterHarvesting.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                            lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                        }


                        BindRainwaterHarvestingAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageRainwaterHarvesting.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                        lblMessageRainwaterHarvesting.ForeColor = Color.Green;

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
                    lblMessageRainwaterHarvesting.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUploadGroundwaterquality_Click(object sender, EventArgs e)
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
                strActionName = "Upload Ground Water Quality";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
                arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplicationForNoLimit.GetGroundwaterqualityAttachmentList();
                if (arr_infrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileName = "";
                    string str_newFileNameWithPath = "";


                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_infrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();

                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadGroundwaterquality.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadGroundwaterquality.PostedFile.FileName).ToLower();
                            str_fname = FileUploadGroundwaterquality.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadGroundwaterquality.PostedFile))
                                {
                                    if (FileUploadGroundwaterquality.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadGroundwaterquality.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = FileUploadGroundwaterquality.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = FileUploadGroundwaterquality.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageGroundwaterquality.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageGroundwaterquality.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageGroundwaterquality.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageGroundwaterquality.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageGroundwaterquality.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageGroundwaterquality.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }

                        else
                        {
                            lblMessageGroundwaterquality.Text = "Please select a file..!!";
                            lblMessageGroundwaterquality.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_infrastructureNewApplication.ApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_infrastructureNewApplication.InfraStructureNewGroundwaterqualityAttachmentCode;

                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtGroundwaterquality.Text; ;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageGroundwaterquality.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                            lblMessageGroundwaterquality.ForeColor = System.Drawing.Color.Red;
                        }


                        BindGroundWaterQualityAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageGroundwaterquality.ForeColor = Color.Green;
                        lblMessageGroundwaterquality.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;

                    }
                    catch (Exception)
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
                        Response.Redirect("~/ExternalErrorPage.aspx", false);
                    }
                }
                else
                {
                    lblMessageGroundwaterquality.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageGroundwaterquality.ForeColor = System.Drawing.Color.Red;
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
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_InfrastructureNewApplicationAttachmentList;
                arr_InfrastructureNewApplicationAttachmentList = obj_InfrastructureNewApplicationForNoLimit.GetBharatKoshRecieptAttachmentList();

                if (arr_InfrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_InfrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();
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
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadBharatKoshReciept.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = FileUploadBharatKoshReciept.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = FileUploadBharatKoshReciept.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;
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
                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_InfrastructureNewApplication.InfrastructureNewApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_InfrastructureNewApplication.InfraStructureNewBharatKoshRecieptAttachmentCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtBharatKoshReciept.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageBharatKoshReciept.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                            lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;

                        }

                        lblMessageBharatKoshReciept.ForeColor = Color.Green;
                        lblMessageBharatKoshReciept.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                        txtBharatKoshReciept.Text = "";
                        BindBharatKoshRecieptAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

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
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_InfrastructureNewApplicationAttachmentList;
                arr_InfrastructureNewApplicationAttachmentList = obj_InfrastructureNewApplicationForNoLimit.GetSignedDocAttachmentList();

                if (arr_InfrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_InfrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();
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
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadAplicationSignatureSeal.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = FileUploadAplicationSignatureSeal.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = FileUploadAplicationSignatureSeal.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;
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
                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_InfrastructureNewApplication.InfrastructureNewApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_InfrastructureNewApplication.SignedDocAttCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtApplicationSignatureSeal.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageAplicationSignatureSeal.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                            lblMessageAplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;

                        }

                        lblMessageAplicationSignatureSeal.ForeColor = Color.Green;
                        lblMessageAplicationSignatureSeal.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                        txtApplicationSignatureSeal.Text = "";
                        BindSignedDocAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

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
    protected void btnUploadExtra_Click(object sender, EventArgs e)
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
                strActionName = "Upload Extra";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
                arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplicationForNoLimit.GetExtraAttachmentList();
                if (arr_infrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileName = "";
                    string str_newFileNameWithPath = "";
                    //HttpPostedFile MyFile;
                    //HttpFileCollection MyFileCollection;
                    //int int_FileLen;
                    //System.IO.Stream MyStream;
                    //string str_restPath = "";

                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_infrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();
                    // NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_infrastructureNewApplicationAttachmentBlank = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
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
                                        //MyFileCollection = Request.Files;
                                        //MyFile = FileUploadExtra.PostedFile;
                                        //int_FileLen = MyFile.ContentLength;
                                        //buffer = new byte[int_FileLen];

                                        //// Initialize the stream.
                                        //MyStream = MyFile.InputStream;
                                        //int Status = MyStream.Read(buffer, 0, int_FileLen);

                                        //str_newFileName = "InfAppCode" + Convert.ToString(obj_infrastructureNewApplication.ApplicationCode) + "AttCode" + Convert.ToString(obj_infrastructureNewApplication.InfraStructureNewExtraAttachmentCode) + "_" + str_fname;

                                        //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                                        //str_restPath = Convert.ToString(obj_infrastructureNewApplication.ApplicationCode) + Path.DirectorySeparatorChar.ToString() + str_newFileName;

                                        //string str_infrastructureNewApplicationSubDirectory = str_startPathFromConfig + Convert.ToString(obj_infrastructureNewApplication.ApplicationCode);
                                        //if (!(Directory.Exists(str_infrastructureNewApplicationSubDirectory)))
                                        //{
                                        //    DirectoryInfo di = Directory.CreateDirectory(str_infrastructureNewApplicationSubDirectory);
                                        //}
                                        //str_newFileNameWithPath = str_startPathFromConfig + str_restPath;

                                        //File.WriteAllBytes(str_newFileNameWithPath, buffer);
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadExtra.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = FileUploadExtra.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = FileUploadExtra.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageExtra.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageExtra.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageExtra.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }

                        else
                        {
                            lblMessageExtra.Text = "Please select a file..!!";
                            lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_infrastructureNewApplication.ApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_infrastructureNewApplication.InfraStructureNewExtraAttachmentCode;

                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtExtraAttachment.Text; ;
                        // obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_restPath;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageExtra.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                            lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                        }


                        BindExtraAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageExtra.ForeColor = Color.Green;
                        lblMessageExtra.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;

                    }
                    catch (Exception)
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
                        Response.Redirect("~/ExternalErrorPage.aspx", false);
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

    protected void btnUploadProofOfOwnershipOfLand_Click(object sender, EventArgs e)
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
                strActionName = "ProofOfOwnershipOfLand";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
                arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplicationForNoLimit.GetProofOwnershipLandAttachmentList();

                if (arr_infrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileName = "";
                    string str_newFileNameWithPath = "";

                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_infrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();

                    byte[] buffer = new byte[1];
                    try
                    {


                        if (FileUploadProofOfOwnershipOfLand.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadProofOfOwnershipOfLand.PostedFile.FileName).ToLower();
                            str_fname = FileUploadProofOfOwnershipOfLand.FileName;

                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadProofOfOwnershipOfLand.PostedFile))
                                {
                                    if (FileUploadProofOfOwnershipOfLand.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadProofOfOwnershipOfLand.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = FileUploadProofOfOwnershipOfLand.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = FileUploadProofOfOwnershipOfLand.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;

                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageUndertaking.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageUndertaking.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageUndertaking.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }

                        else
                        {
                            lblMessageUndertaking.Text = "Please select a file..!!";
                            lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_infrastructureNewApplication.ApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_infrastructureNewApplication.ProofOwnershipLandAttCode;//obj_infrastructureNewApplication.Undertaking.UndertakingAttachCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtProofOfOwnershipOfLand.Text;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageUndertaking.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                            lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        lblProofofownershipoflandofsize200sqm.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                        lblProofofownershipoflandofsize200sqm.ForeColor = Color.Green;
                        txtProofOfOwnershipOfLand.Text = "";
                        BindProofOfOwnershipAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

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
                    lblMessageUndertaking.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void btnUploadProofofownershipLeaseoftanker_Click(object sender, EventArgs e)
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
                strActionName = "ProofofownershipLeaseoftanker";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
                arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplicationForNoLimit.GetProofOwnershipTankerAttachmentList();

                if (arr_infrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileName = "";
                    string str_newFileNameWithPath = "";
                    //HttpPostedFile MyFile;
                    //HttpFileCollection MyFileCollection;
                    //int int_FileLen;
                    //System.IO.Stream MyStream;
                    //string str_restPath = "";

                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_infrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();
                    // NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_infrastructureNewApplicationAttachmentBlank = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();

                    byte[] buffer = new byte[1];
                    try
                    {


                        if (FileUploadProofofownershipLeaseoftanker.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadProofofownershipLeaseoftanker.PostedFile.FileName).ToLower();
                            str_fname = FileUploadProofofownershipLeaseoftanker.FileName;

                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadProofofownershipLeaseoftanker.PostedFile))
                                {
                                    if (FileUploadProofofownershipLeaseoftanker.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {

                                        //MyFileCollection = Request.Files;
                                        ////MyFile = MyFileCollection[0];
                                        //MyFile = FileUploadUndertaking.PostedFile;
                                        //int_FileLen = MyFile.ContentLength;
                                        //buffer = new byte[int_FileLen];

                                        //// Initialize the stream.
                                        //MyStream = MyFile.InputStream;
                                        //int Status = MyStream.Read(buffer, 0, int_FileLen);

                                        //str_newFileName = "InfAppCode" + Convert.ToString(obj_infrastructureNewApplication.ApplicationCode) + "AttCode" + Convert.ToString(obj_infrastructureNewApplication.Undertaking.UndertakingAttachCode) + "_" + str_fname;

                                        //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                                        //str_restPath = Convert.ToString(obj_infrastructureNewApplication.ApplicationCode) + Path.DirectorySeparatorChar.ToString() + str_newFileName;
                                        //string str_ifrastructureNewApplicationSubDirectory = str_startPathFromConfig + Convert.ToString(obj_infrastructureNewApplication.ApplicationCode);
                                        //if (!(Directory.Exists(str_ifrastructureNewApplicationSubDirectory)))
                                        //{
                                        //    DirectoryInfo di = Directory.CreateDirectory(str_ifrastructureNewApplicationSubDirectory);
                                        //}
                                        //str_newFileNameWithPath = str_startPathFromConfig + str_restPath;
                                        //File.WriteAllBytes(str_newFileNameWithPath, buffer);
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadProofofownershipLeaseoftanker.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = FileUploadProofofownershipLeaseoftanker.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = FileUploadProofofownershipLeaseoftanker.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;

                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblProofofownershipLeaseTanker.Text = "File can not upload. It has more than 5 MB size";
                                        lblProofofownershipLeaseTanker.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblProofofownershipLeaseTanker.Text = "Not a valid file!!..Select an other file!!";
                                    lblProofofownershipLeaseTanker.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblProofofownershipLeaseTanker.Text = "Not a valid file!!..Select an other file!!";
                                lblProofofownershipLeaseTanker.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }

                        else
                        {
                            lblProofofownershipLeaseTanker.Text = "Please select a file..!!";
                            lblProofofownershipLeaseTanker.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_infrastructureNewApplication.ApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_infrastructureNewApplication.ProofOwnershipTankerAttCode;//obj_infrastructureNewApplication.Undertaking.UndertakingAttachCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtProofofownershipLeaseoftanker.Text;
                        //  obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_restPath;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblProofofownershipLeaseTanker.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                            lblProofofownershipLeaseTanker.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        lblProofofownershipLeaseTanker.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                        lblProofofownershipLeaseTanker.ForeColor = Color.Green;
                        txtProofofownershipLeaseoftanker.Text = "";
                        BindProofofownershipLeaseoftankerAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

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
                    lblMessageUndertaking.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    #endregion

    #region Upload Button Click
    protected void btnUploadCertiNonAva_Click(object sender, EventArgs e)
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
                strActionName = "File Wetland";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_InfrastructureNewApplicationAttachmentList;
                arr_InfrastructureNewApplicationAttachmentList = obj_InfrastructureNewApplicationForNoLimit.GetCertiNonAvaAttachmentList();

                if (arr_InfrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_InfrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadCertiNonAva.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadCertiNonAva.PostedFile.FileName).ToLower();
                            str_fname = FileUploadCertiNonAva.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadCertiNonAva.PostedFile))
                                {
                                    if (FileUploadCertiNonAva.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadCertiNonAva.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = FileUploadCertiNonAva.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = FileUploadCertiNonAva.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageCertiNonAva.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageCertiNonAva.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageCertiNonAva.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageCertiNonAva.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageCertiNonAva.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageCertiNonAva.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageCertiNonAva.Text = "Please select a file..!!";
                            lblMessageCertiNonAva.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_InfrastructureNewApplication.InfrastructureNewApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_InfrastructureNewApplication.CertiNonAvaAttCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtCertiNonAva.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            lblMessageCertiNonAva.ForeColor = Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageCertiNonAva.ForeColor = System.Drawing.Color.Red;
                        }

                        BindCertiNonAvaAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageCertiNonAva.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                        lblMessageCertiNonAva.ForeColor = Color.Green;
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
                    lblMessageWetlanArea.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageWetlanArea.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUploadAffidavit_Click(object sender, EventArgs e)
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
                strActionName = "File Affidavit";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_InfrastructureNewApplicationAttachmentList;
                arr_InfrastructureNewApplicationAttachmentList = obj_InfrastructureNewApplicationForNoLimit.GetAffidavitNonAvaAttachmentList();

                if (arr_InfrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_InfrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadAffidavit.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadAffidavit.PostedFile.FileName).ToLower();
                            str_fname = FileUploadAffidavit.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadAffidavit.PostedFile))
                                {
                                    if (FileUploadAffidavit.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadAffidavit.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = FileUploadAffidavit.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = FileUploadAffidavit.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageAffidavit.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageAffidavit.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageAffidavit.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageAffidavit.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageAffidavit.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageAffidavit.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageAffidavit.Text = "Please select a file..!!";
                            lblMessageAffidavit.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_InfrastructureNewApplication.InfrastructureNewApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_InfrastructureNewApplication.AffidavitNonAvaAttCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtAffidavit.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            lblMessageAffidavit.ForeColor = Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageAffidavit.ForeColor = System.Drawing.Color.Red;
                        }

                        BindAffidavitAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageAffidavit.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                        lblMessageAffidavit.ForeColor = Color.Green;
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
                    lblMessageAffidavit.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageAffidavit.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUploadImpactReport_Click(object sender, EventArgs e)
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
                strActionName = "File Impact";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_InfrastructureNewApplicationAttachmentList;
                arr_InfrastructureNewApplicationAttachmentList = obj_InfrastructureNewApplicationForNoLimit.GetAffidavitNonAvaAttachmentList();

                if (obj_InfrastructureNewApplicationForNoLimit.ImpactAssOCSOTPVerified == NOCAP.BLL.Common.CommonApplication.ImpactAssOCSOTPVerifiedYesNo.Yes)
                {
                    if (arr_InfrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                    {
                        string str_fname;
                        string str_ext;
                        string str_newFileNameWithPath = "";
                        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_InfrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();
                        byte[] buffer = new byte[1];
                        try
                        {
                            if (FileUploadImpactReport.HasFile)
                            {
                                str_ext = System.IO.Path.GetExtension(FileUploadImpactReport.PostedFile.FileName).ToLower();
                                str_fname = FileUploadImpactReport.FileName;
                                if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                                {
                                    if (NOCAPExternalUtility.IsValidFile(FileUploadImpactReport.PostedFile))
                                    {
                                        if (FileUploadImpactReport.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                        {
                                            obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadImpactReport.PostedFile);
                                            obj_insertInfrastructureNewApplicationAttachment.ContentType = FileUploadImpactReport.PostedFile.ContentType;
                                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = FileUploadImpactReport.FileName;
                                            obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;
                                        }
                                        else
                                        {
                                            strStatus = "File Upload Failed";
                                            lblMessageImpactReport.Text = "File can not upload. It has more than 5 MB size";
                                            lblMessageImpactReport.ForeColor = System.Drawing.Color.Red;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageImpactReport.Text = "Not a valid file!!..Select an other file!!";
                                        lblMessageImpactReport.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageImpactReport.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageImpactReport.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }

                            }
                            else
                            {
                                lblMessageImpactReport.Text = "Please select a file..!!";
                                lblMessageImpactReport.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                            obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_InfrastructureNewApplication.InfrastructureNewApplicationCode;
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_InfrastructureNewApplication.ImpactAssOCSAttCode;
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtImpactReport.Text;
                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                            obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                            if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                            {
                                strStatus = "File Upload Success";
                                obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                                lblMessageImpactReport.ForeColor = Color.Green;
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageImpactReport.ForeColor = System.Drawing.Color.Red;
                            }

                            BindImpactReportAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                            clearMessage();
                            lblMessageImpactReport.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                            lblMessageImpactReport.ForeColor = Color.Green;
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
                        lblMessageImpactReport.Text = "Maximum number of files to be uploaded is 5";
                        lblMessageImpactReport.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMessageImpactReport.Text = "Please Verified Consultant OTP";
                    lblMessageImpactReport.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUploadCompletionCerti_Click(object sender, EventArgs e)
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
                strActionName = "File Completion Certificate";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_InfrastructureNewApplicationAttachmentList;
                arr_InfrastructureNewApplicationAttachmentList = obj_InfrastructureNewApplicationForNoLimit.GetCompletionCertiAttachmentList();

                if (arr_InfrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_InfrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadCompletionCerti.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadCompletionCerti.PostedFile.FileName).ToLower();
                            str_fname = FileUploadCompletionCerti.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadCompletionCerti.PostedFile))
                                {
                                    if (FileUploadCompletionCerti.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadCompletionCerti.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = FileUploadCompletionCerti.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = FileUploadCompletionCerti.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageCompletionCerti.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageCompletionCerti.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageCompletionCerti.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageCompletionCerti.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageCompletionCerti.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageCompletionCerti.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageCompletionCerti.Text = "Please select a file..!!";
                            lblMessageCompletionCerti.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_InfrastructureNewApplication.InfrastructureNewApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_InfrastructureNewApplication.CompletionCertiAttCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtCompletionCerti.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            lblMessageCompletionCerti.ForeColor = Color.Green;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";

                            lblMessageCompletionCerti.ForeColor = System.Drawing.Color.Red;
                        }

                        BindWetlandAreaAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageCompletionCerti.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                        lblMessageCompletionCerti.ForeColor = Color.Green;
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
                    lblMessageCompletionCerti.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageCompletionCerti.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUploadInstSTP_Click(object sender, EventArgs e)
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
                strActionName = "File Wetland";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_InfrastructureNewApplicationAttachmentList;
                arr_InfrastructureNewApplicationAttachmentList = obj_InfrastructureNewApplicationForNoLimit.GetWetlandAreaAttachmentList();

                if (arr_InfrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_InfrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadInstSTP.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadInstSTP.PostedFile.FileName).ToLower();
                            str_fname = FileUploadInstSTP.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadInstSTP.PostedFile))
                                {
                                    if (FileUploadInstSTP.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadInstSTP.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = FileUploadInstSTP.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = FileUploadInstSTP.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageInstSTP.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageInstSTP.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageInstSTP.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageInstSTP.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageInstSTP.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageInstSTP.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageInstSTP.Text = "Please select a file..!!";
                            lblMessageInstSTP.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_InfrastructureNewApplication.InfrastructureNewApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_InfrastructureNewApplication.InstallationSTPAttCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtInstSTP.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            lblMessageInstSTP.ForeColor = Color.Green;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";

                            lblMessageInstSTP.ForeColor = System.Drawing.Color.Red;
                        }

                        BindInstSTPAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageInstSTP.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                        lblMessageInstSTP.ForeColor = Color.Green;
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
                    lblMessageInstSTP.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageInstSTP.ForeColor = System.Drawing.Color.Red;
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
                strActionName = "File Wetland";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_InfrastructureNewApplicationAttachmentList;
                arr_InfrastructureNewApplicationAttachmentList = obj_InfrastructureNewApplicationForNoLimit.GetWetlandAreaAttachmentList();

                if (arr_InfrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_InfrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();
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
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadWetlandArea.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = FileUploadWetlandArea.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = FileUploadWetlandArea.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;
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
                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_InfrastructureNewApplication.InfrastructureNewApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_InfrastructureNewApplication.WetlandAreaAttCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtWetlandArea.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            lblMessageWetlanArea.ForeColor = Color.Green;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";

                            lblMessageWetlanArea.ForeColor = System.Drawing.Color.Red;
                        }

                        BindWetlandAreaAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageWetlanArea.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                        lblMessageWetlanArea.ForeColor = Color.Green;
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
                    lblMessageWetlanArea.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageWetlanArea.ForeColor = System.Drawing.Color.Red;
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
                strActionName = "File Penalty";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_InfrastructureNewApplicationAttachmentList;
                arr_InfrastructureNewApplicationAttachmentList = obj_InfrastructureNewApplicationForNoLimit.GetWetlandAreaAttachmentList();

                if (arr_InfrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_InfrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();
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
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadPenalty.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = FileUploadPenalty.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = FileUploadPenalty.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;
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
                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_InfrastructureNewApplication.InfrastructureNewApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_InfrastructureNewApplication.PenaltyAttCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtPenalty.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            lblMessagePenalty.ForeColor = Color.Green;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";

                            lblMessagePenalty.ForeColor = System.Drawing.Color.Red;
                        }

                        BindWetlandAreaAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessagePenalty.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                        lblMessagePenalty.ForeColor = Color.Green;
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
                    lblMessagePenalty.Text = "Maximum number of files to be uploaded is 5";
                    lblMessagePenalty.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUploadSitePlan_Click(object sender, EventArgs e)
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
                strActionName = "UploadSitePlan";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
                arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplicationForNoLimit.GetSitePlanAttachmentList();
                if (arr_infrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_infrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();

                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadSitePlan.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadSitePlan.PostedFile.FileName).ToLower();
                            str_fname = FileUploadSitePlan.FileName;
                            //str_path = txtFileUpload.PostedFile.FileName;
                            //str_ext = System.IO.Path.GetExtension(str_fname);
                            //str_newFileName = str_newFileName + str_ext;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadSitePlan.PostedFile))
                                {
                                    if (FileUploadSitePlan.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadSitePlan.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = FileUploadSitePlan.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = FileUploadSitePlan.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Updoad Failed";
                                        lblMessageSitePlan.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Updoad Failed";
                                    lblMessageSitePlan.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }

                            }
                            else
                            {
                                strStatus = "File Updoad Failed";
                                lblMessageSitePlan.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageSitePlan.Text = "Please select a file..!!";
                            lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                            return;
                        }


                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_infrastructureNewApplication.ApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_infrastructureNewApplication.ProposedLocation.ProposedLocationSitePlanAttachCode;

                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtSitePlanAttachment.Text; ;


                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Updoad Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;



                        }
                        else
                        {
                            strStatus = "File Updoad Failed";
                            lblMessageSitePlan.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                            lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;

                        }

                        lblMessageSitePlan.ForeColor = Color.Green;
                        lblMessageSitePlan.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                        txtSitePlanAttachment.Text = "";
                        BindInfrastructureNewApplicationAttachmentSitePlanDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        lblMessageLocationMap.Text = "";
                        lblMessageDocumentsofOwnership.Text = "";
                        lblMessageGroundwaterAvailability.Text = "";
                        lblMessageRainwaterHarvesting.Text = "";
                        lblMessageSourceofAvailabilityofSurfaceWater.Text = "";
                        lblMessageUndertaking.Text = "";
                        lblMessageWaterRequirement.Text = "";



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
                    lblMessageSitePlan.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void btnUploadApporvalLetter_Click(object sender, EventArgs e)
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
                strActionName = "Upload Approva Letter";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
                arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplicationForNoLimit.GetInfrastructureNewApprovalLetterFromStateGovtAgencyList();
                if (arr_infrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileName = "";
                    string str_newFileNameWithPath = "";
                    //HttpPostedFile MyFile;
                    //HttpFileCollection MyFileCollection;
                    //int int_FileLen;
                    //System.IO.Stream MyStream;
                    //string str_restPath = "";

                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_infrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();
                    //NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_infrastructureNewApplicationAttachmentBlank = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();

                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadApprovalLetter.HasFile)
                        {

                            str_ext = System.IO.Path.GetExtension(FileUploadApprovalLetter.PostedFile.FileName).ToLower();

                            str_fname = FileUploadApprovalLetter.FileName;
                            //str_path = txtFileUpload.PostedFile.FileName;
                            //str_ext = System.IO.Path.GetExtension(str_fname);
                            //str_newFileName = str_newFileName + str_ext;

                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadApprovalLetter.PostedFile))
                                {
                                    if (FileUploadApprovalLetter.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        //MyFileCollection = Request.Files;
                                        //// MyFile = MyFileCollection[0];
                                        //MyFile = FileUploadApprovalLetter.PostedFile;
                                        //int_FileLen = MyFile.ContentLength;
                                        //buffer = new byte[int_FileLen];

                                        //// Initialize the stream.
                                        //MyStream = MyFile.InputStream;
                                        //int Status = MyStream.Read(buffer, 0, int_FileLen);

                                        //str_newFileName = "InfAppCode" + Convert.ToString(obj_infrastructureNewApplication.ApplicationCode) + "AttCode" + Convert.ToString(obj_infrastructureNewApplication.ApprovalLetterofStateGovtAgency.ApprovalLetterAttachCode) + "_" + str_fname;

                                        //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                                        //str_restPath = Convert.ToString(obj_infrastructureNewApplication.ApplicationCode) + Path.DirectorySeparatorChar.ToString() + str_newFileName;
                                        //string str_infrastructureNewApplicationSubDirectory = str_startPathFromConfig + Convert.ToString(obj_infrastructureNewApplication.ApplicationCode);
                                        //if (!(Directory.Exists(str_infrastructureNewApplicationSubDirectory)))
                                        //{
                                        //    DirectoryInfo di = Directory.CreateDirectory(str_infrastructureNewApplicationSubDirectory);
                                        //}

                                        //str_newFileNameWithPath = str_startPathFromConfig + str_restPath;
                                        //File.WriteAllBytes(str_newFileNameWithPath, buffer);
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadApprovalLetter.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = FileUploadApprovalLetter.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = FileUploadApprovalLetter.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;

                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblApprovalLetterOfStateGovtAgency.Text = "File can not upload. It has more than 5 MB size";
                                        lblApprovalLetterOfStateGovtAgency.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblApprovalLetterOfStateGovtAgency.Text = "Not a valid file!!..Select an other file!!";
                                    lblApprovalLetterOfStateGovtAgency.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblApprovalLetterOfStateGovtAgency.Text = "Not a valid file!!..Select an other file!!";
                                lblApprovalLetterOfStateGovtAgency.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblApprovalLetterOfStateGovtAgency.Text = "Please select a file..!!";
                            lblApprovalLetterOfStateGovtAgency.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_infrastructureNewApplication.ApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_infrastructureNewApplication.ApprovalLetterofStateGovtAgency.ApprovalLetterAttachCode;

                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtApprovalLetterName.Text;
                        //obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_restPath;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageLocationMap.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                            lblMessageLocationMap.ForeColor = System.Drawing.Color.Red;
                        }

                        lblMessageLocationMap.ForeColor = Color.Green;
                        lblMessageLocationMap.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                        //txtCertifiedRevenueSketchAttachment.Text = "";
                        // BindInfrastructureNewApplicationAttachmentApprovalLetterOfStateGovtAgencyDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    }
                    catch (Exception)
                    {
                        lblApprovalLetterOfStateGovtAgency.ForeColor = Color.Green;
                        lblApprovalLetterOfStateGovtAgency.Text = "Error in page";
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
                    lblApprovalLetterOfStateGovtAgency.Text = "Maximum number of files to be uploaded is 5";
                    lblApprovalLetterOfStateGovtAgency.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void btnUploadCertifiedRevenueSketch_Click(object sender, EventArgs e)
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
                strActionName = "Upload Certificate Revenue Sketch";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
                arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplicationForNoLimit.GetInfrastructureNewLocationMapAttachmentList();
                if (arr_infrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileName = "";
                    string str_newFileNameWithPath = "";
                    //HttpPostedFile MyFile;
                    //HttpFileCollection MyFileCollection;
                    //int int_FileLen;
                    //System.IO.Stream MyStream;
                    //string str_restPath = "";

                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_infrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();
                    //NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_infrastructureNewApplicationAttachmentBlank = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();

                    byte[] buffer = new byte[1];
                    try
                    {
                        if (txtFileUploadLocationMap.HasFile)
                        {

                            str_ext = System.IO.Path.GetExtension(txtFileUploadLocationMap.PostedFile.FileName).ToLower();

                            str_fname = txtFileUploadLocationMap.FileName;
                            //str_path = txtFileUpload.PostedFile.FileName;
                            //str_ext = System.IO.Path.GetExtension(str_fname);
                            //str_newFileName = str_newFileName + str_ext;

                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(txtFileUploadLocationMap.PostedFile))
                                {
                                    if (txtFileUploadLocationMap.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        //MyFileCollection = Request.Files;
                                        //// MyFile = MyFileCollection[0];
                                        //MyFile = txtFileUploadLocationMap.PostedFile;
                                        //int_FileLen = MyFile.ContentLength;
                                        //buffer = new byte[int_FileLen];

                                        //// Initialize the stream.
                                        //MyStream = MyFile.InputStream;
                                        //int Status = MyStream.Read(buffer, 0, int_FileLen);

                                        //str_newFileName = "InfAppCode" + Convert.ToString(obj_infrastructureNewApplication.ApplicationCode) + "AttCode" + Convert.ToString(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationMapAttachCode) + "_" + str_fname;

                                        //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                                        //str_restPath = Convert.ToString(obj_infrastructureNewApplication.ApplicationCode) + Path.DirectorySeparatorChar.ToString() + str_newFileName;
                                        //string str_infrastructureNewApplicationSubDirectory = str_startPathFromConfig + Convert.ToString(obj_infrastructureNewApplication.ApplicationCode);
                                        //if (!(Directory.Exists(str_infrastructureNewApplicationSubDirectory)))
                                        //{
                                        //    DirectoryInfo di = Directory.CreateDirectory(str_infrastructureNewApplicationSubDirectory);
                                        //}

                                        //str_newFileNameWithPath = str_startPathFromConfig + str_restPath;
                                        //File.WriteAllBytes(str_newFileNameWithPath, buffer);

                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(txtFileUploadLocationMap.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = txtFileUploadLocationMap.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = txtFileUploadLocationMap.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageLocationMap.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageLocationMap.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageLocationMap.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageLocationMap.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageLocationMap.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageLocationMap.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageLocationMap.Text = "Please select a file..!!";
                            lblMessageLocationMap.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_infrastructureNewApplication.ApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_infrastructureNewApplication.ProposedLocation.ProposedLocationMapAttachCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtLocationMapAttachment.Text;
                        //obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_restPath;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageLocationMap.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                            lblMessageLocationMap.ForeColor = System.Drawing.Color.Red;
                        }

                        lblMessageLocationMap.ForeColor = Color.Green;
                        lblMessageLocationMap.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                        //txtCertifiedRevenueSketchAttachment.Text = "";
                        BindInfrastructureNewApplicationAttachmentDocumentsofLocationMapDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
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
                    lblMessageLocationMap.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageLocationMap.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void btnUplodDocumentsofOwnership_Click(object sender, EventArgs e)
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
                strActionName = "UploadDocumentOfOwnerShip";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
                arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplicationForNoLimit.GetDocumentsofOwnershipAndLeasesofOwnershipAttachmentList();

                if (arr_infrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileName = "";
                    string str_newFileNameWithPath = "";
                    //HttpPostedFile MyFile;
                    //HttpFileCollection MyFileCollection;
                    //int int_FileLen;
                    //System.IO.Stream MyStream;
                    //string str_restPath = "";

                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_infrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();
                    //NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_infrastructureNewApplicationAttachmentBlank = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (txtFileUploadDocumentsofOwnership.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(txtFileUploadDocumentsofOwnership.PostedFile.FileName).ToLower();
                            str_fname = txtFileUploadDocumentsofOwnership.FileName;

                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(txtFileUploadDocumentsofOwnership.PostedFile))
                                {
                                    if (txtFileUploadDocumentsofOwnership.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {

                                        //MyFileCollection = Request.Files;
                                        ////MyFile = MyFileCollection[0];
                                        //MyFile = txtFileUploadDocumentsofOwnership.PostedFile;
                                        //int_FileLen = MyFile.ContentLength;
                                        //buffer = new byte[int_FileLen];

                                        //// Initialize the stream.
                                        //MyStream = MyFile.InputStream;
                                        //int Status = MyStream.Read(buffer, 0, int_FileLen);

                                        //str_newFileName = "InfAppCode" + Convert.ToString(obj_infrastructureNewApplication.ApplicationCode) + "AttCode" + Convert.ToString(obj_infrastructureNewApplication.LandUseDetailOfExistingProposed.LandUseTypeOwnershipLeaseAttachCode) + "_" + str_fname;

                                        //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                                        //str_restPath = Convert.ToString(obj_infrastructureNewApplication.ApplicationCode) + Path.DirectorySeparatorChar.ToString() + str_newFileName;
                                        //string str_infrastructureNewApplicationSubDirectory = str_startPathFromConfig + Convert.ToString(obj_infrastructureNewApplication.ApplicationCode);
                                        //if (!(Directory.Exists(str_infrastructureNewApplicationSubDirectory)))
                                        //{
                                        //    DirectoryInfo di = Directory.CreateDirectory(str_infrastructureNewApplicationSubDirectory);
                                        //}
                                        //str_newFileNameWithPath = str_startPathFromConfig + str_restPath;
                                        //File.WriteAllBytes(str_newFileNameWithPath, buffer);
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(txtFileUploadDocumentsofOwnership.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = txtFileUploadDocumentsofOwnership.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = txtFileUploadDocumentsofOwnership.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageDocumentsofOwnership.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageDocumentsofOwnership.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageDocumentsofOwnership.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageDocumentsofOwnership.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageDocumentsofOwnership.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageDocumentsofOwnership.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageDocumentsofOwnership.Text = "Please select a file..!!";
                            lblMessageDocumentsofOwnership.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_infrastructureNewApplication.ApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_infrastructureNewApplication.LandUseDetailOfExistingProposed.LandUseTypeOwnershipLeaseAttachCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtDocumentsofOwnership.Text;
                        // obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_restPath;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageDocumentsofOwnership.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                            lblMessageDocumentsofOwnership.ForeColor = System.Drawing.Color.Red;
                        }

                        lblMessageDocumentsofOwnership.ForeColor = Color.Green;
                        lblMessageDocumentsofOwnership.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                        txtDocumentsofOwnership.Text = "";
                        // BindInfrastructureNewApplicationAttachmentDocumentsofOwnershipAndLeasesofOwnershipDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
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
                    lblMessageDocumentsofOwnership.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageDocumentsofOwnership.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }


    protected void lbtnUplodSourceofAvailabilityofSurfaceWaterFile_Click(object sender, EventArgs e)
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
                strActionName = "UploadSourceOfAvailabilityOfSurfaceWater";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
                arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplicationForNoLimit.GetSourceofAvailabilityofSurfaceWaterAttachmentList();

                if (arr_infrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileName = "";
                    string str_newFileNameWithPath = "";
                    //HttpPostedFile MyFile;
                    //HttpFileCollection MyFileCollection;
                    //int int_FileLen;
                    //System.IO.Stream MyStream;
                    //string str_restPath = "";

                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_infrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();
                    // NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_infrastructureNewApplicationAttachmentBlank = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
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
                                        //MyFileCollection = Request.Files;
                                        ////MyFile = MyFileCollection[0];
                                        //MyFile = txtFileUploadSourceofAvailabilityofSurfaceWater.PostedFile;
                                        //int_FileLen = MyFile.ContentLength;
                                        //buffer = new byte[int_FileLen];

                                        //// Initialize the stream.
                                        //MyStream = MyFile.InputStream;
                                        //int Status = MyStream.Read(buffer, 0, int_FileLen);

                                        //str_newFileName = "InfAppCode" + Convert.ToString(obj_infrastructureNewApplication.ApplicationCode) + "AttCode" + Convert.ToString(obj_infrastructureNewApplication.SourceOfAvailabilityOfSurfaceWaterForInfrastructureUse.SourceOfAvalabilityOfSurfaceWaterAttachCode) + "_" + str_fname;

                                        //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                                        //str_restPath = Convert.ToString(obj_infrastructureNewApplication.ApplicationCode) + Path.DirectorySeparatorChar.ToString() + str_newFileName;
                                        //string str_infrastructureNewApplicationSubDirectory = str_startPathFromConfig + Convert.ToString(obj_infrastructureNewApplication.ApplicationCode);
                                        //if (!(Directory.Exists(str_infrastructureNewApplicationSubDirectory)))
                                        //{
                                        //    DirectoryInfo di = Directory.CreateDirectory(str_infrastructureNewApplicationSubDirectory);
                                        //}
                                        //str_newFileNameWithPath = str_startPathFromConfig + str_restPath;
                                        //File.WriteAllBytes(str_newFileNameWithPath, buffer);
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(txtFileUploadSourceofAvailabilityofSurfaceWater.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = txtFileUploadSourceofAvailabilityofSurfaceWater.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = txtFileUploadSourceofAvailabilityofSurfaceWater.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;

                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageSourceofAvailabilityofSurfaceWater.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageSourceofAvailabilityofSurfaceWater.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageSourceofAvailabilityofSurfaceWater.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageSourceofAvailabilityofSurfaceWater.Text = "Please select a file..!!";
                            lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;
                            return;
                        }





                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_infrastructureNewApplication.ApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_infrastructureNewApplication.SourceOfAvailabilityOfSurfaceWaterForInfrastructureUse.SourceOfAvalabilityOfSurfaceWaterAttachCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtSourceofAvailabilityofSurfaceWater.Text;
                        // obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_restPath;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageSourceofAvailabilityofSurfaceWater.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                            lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;
                        }

                        lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = Color.Green;
                        lblMessageSourceofAvailabilityofSurfaceWater.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                        txtSourceofAvailabilityofSurfaceWater.Text = "";
                        BindInfrastructureNewApplicationAttachmentSourceofAvailabilityofSurfaceWaterDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

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
                    lblMessageSourceofAvailabilityofSurfaceWater.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }


    protected void btnUplodGroundwaterAvailability_Click(object sender, EventArgs e)
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
                strActionName = "UploadGroundWaterAvailability";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
                arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplicationForNoLimit.GetGroundwaterAvailabilityAttachmentList(obj_infrastructureNewApplicationForNoLimit);//Argument

                if (arr_infrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileName = "";
                    string str_newFileNameWithPath = "";
                    //HttpPostedFile MyFile;
                    //HttpFileCollection MyFileCollection;
                    //int int_FileLen;
                    //System.IO.Stream MyStream;
                    //string str_restPath = "";

                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_infrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();
                    // NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_infrastructureNewApplicationAttachmentBlank = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
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



                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadGroundwaterAvailability.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = FileUploadGroundwaterAvailability.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = FileUploadGroundwaterAvailability.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageGroundwaterAvailability.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageGroundwaterAvailability.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageGroundwaterAvailability.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }


                        else
                        {
                            lblMessageGroundwaterAvailability.Text = "Please select a file..!!";
                            lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_infrastructureNewApplication.ApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_infrastructureNewApplication.GroundWaterAvailability.GroundWaterAvailabilityAttachCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtgvGroundwaterAvailability.Text;
                        //obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_restPath;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageGroundwaterAvailability.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                            lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                        }

                        lblMessageGroundwaterAvailability.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                        lblMessageGroundwaterAvailability.ForeColor = Color.Green;
                        txtgvGroundwaterAvailability.Text = "";
                        BindInfrastructureNewApplicationAttachmentGroundwaterAvailabilityDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
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
                    lblMessageGroundwaterAvailability.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }


    protected void btnUplodUndertaking_Click(object sender, EventArgs e)
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
                strActionName = "UploadUnderTaking";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
                arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplicationForNoLimit.GetUndertakingAttachmentList(obj_infrastructureNewApplicationForNoLimit);//Argument

                if (arr_infrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileName = "";
                    string str_newFileNameWithPath = "";
                    //HttpPostedFile MyFile;
                    //HttpFileCollection MyFileCollection;
                    //int int_FileLen;
                    //System.IO.Stream MyStream;
                    //string str_restPath = "";

                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_infrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();
                    // NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_infrastructureNewApplicationAttachmentBlank = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();

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

                                        //MyFileCollection = Request.Files;
                                        ////MyFile = MyFileCollection[0];
                                        //MyFile = FileUploadUndertaking.PostedFile;
                                        //int_FileLen = MyFile.ContentLength;
                                        //buffer = new byte[int_FileLen];

                                        //// Initialize the stream.
                                        //MyStream = MyFile.InputStream;
                                        //int Status = MyStream.Read(buffer, 0, int_FileLen);

                                        //str_newFileName = "InfAppCode" + Convert.ToString(obj_infrastructureNewApplication.ApplicationCode) + "AttCode" + Convert.ToString(obj_infrastructureNewApplication.Undertaking.UndertakingAttachCode) + "_" + str_fname;

                                        //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                                        //str_restPath = Convert.ToString(obj_infrastructureNewApplication.ApplicationCode) + Path.DirectorySeparatorChar.ToString() + str_newFileName;
                                        //string str_ifrastructureNewApplicationSubDirectory = str_startPathFromConfig + Convert.ToString(obj_infrastructureNewApplication.ApplicationCode);
                                        //if (!(Directory.Exists(str_ifrastructureNewApplicationSubDirectory)))
                                        //{
                                        //    DirectoryInfo di = Directory.CreateDirectory(str_ifrastructureNewApplicationSubDirectory);
                                        //}
                                        //str_newFileNameWithPath = str_startPathFromConfig + str_restPath;
                                        //File.WriteAllBytes(str_newFileNameWithPath, buffer);
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadUndertaking.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = FileUploadUndertaking.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = FileUploadUndertaking.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;

                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageUndertaking.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageUndertaking.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageUndertaking.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }

                        else
                        {
                            lblMessageUndertaking.Text = "Please select a file..!!";
                            lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_infrastructureNewApplication.ApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_infrastructureNewApplication.Undertaking.UndertakingAttachCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtUndertaking.Text;
                        //  obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_restPath;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageUndertaking.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                            lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        lblMessageUndertaking.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
                        lblMessageUndertaking.ForeColor = Color.Green;
                        txtUndertaking.Text = "";
                        BindInfrastructureNewApplicationAttachmentUndertakingDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

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
                    lblMessageUndertaking.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void btnUplodReferralLetter_Click(object sender, EventArgs e)
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
                strActionName = "Upload Consent/ Approval of Government Agency";
                int int_CountAttForRefLetter = 0;
                int int_ReferralLetterCode = Convert.ToInt32(ddlReferralLetter.SelectedValue);
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter obj_infrastructureNewReferralLetterk = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text), int_ReferralLetterCode);
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
                if (obj_infrastructureNewReferralLetterk.ReferralLetterAttachCode == 0)
                {
                    int_CountAttForRefLetter = 0;
                }
                else
                {
                    arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplicationForNoLimit.GetCopyOfReferralLetterAttachmentList(obj_infrastructureNewApplicationForNoLimit, obj_infrastructureNewReferralLetterk.ReferralLetterAttachCode, NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment.SortingFieldForAttachment.NoSorting);
                    int_CountAttForRefLetter = arr_infrastructureNewApplicationAttachmentList.Count();
                }
                if (int_CountAttForRefLetter < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileName = "";
                    string str_newFileNameWithPath = "";
                    //HttpPostedFile MyFile;
                    //HttpFileCollection MyFileCollection;
                    //int int_FileLen;
                    //System.IO.Stream MyStream;
                    //string str_restPath = "";


                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter obj_infrastructureNewReferralLetter = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text), Convert.ToInt32(ddlReferralLetter.SelectedValue));
                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    int int_ReferralLetterAttachCode;
                    try
                    {

                        int_ReferralLetterAttachCode = obj_infrastructureNewReferralLetter.ReferralLetterAttachCode;
                        if (obj_infrastructureNewReferralLetter.ReferralLetterAttachCode == 0)
                        {
                            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter obj_infrastructureNewReferralLetterForAdd = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter();
                            obj_infrastructureNewReferralLetterForAdd.ApplicationCode = Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text);
                            obj_infrastructureNewReferralLetterForAdd.ReferralLetterTypeCode = Convert.ToInt32(ddlReferralLetter.SelectedValue);
                            obj_infrastructureNewReferralLetterForAdd.CreatedByExUC = obj_externalUser.ExternalUserCode;
                            if (obj_infrastructureNewReferralLetterForAdd.Add() != 1)
                            {
                                lblMessageReferralLetter.Text = obj_infrastructureNewReferralLetterForAdd.CustumMessage;
                                lblMessageReferralLetter.ForeColor = Color.Green;
                                return;
                            }
                            int_ReferralLetterAttachCode = obj_infrastructureNewReferralLetterForAdd.ReferralLetterAttachCode;
                        }

                        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachmentAferAdd = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplicationAfterAdd = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_infrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();

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

                                        //MyFileCollection = Request.Files;
                                        ////MyFile = MyFileCollection[0];
                                        //MyFile = FileUploadReferralLetter.PostedFile;
                                        //int_FileLen = MyFile.ContentLength;
                                        //buffer = new byte[int_FileLen];

                                        //// Initialize the stream.
                                        //MyStream = MyFile.InputStream;
                                        //int Status = MyStream.Read(buffer, 0, int_FileLen);

                                        //str_newFileName = "InfAppCode" + Convert.ToString(obj_infrastructureNewApplication.ApplicationCode) + "AttCode" + Convert.ToString(int_ReferralLetterAttachCode) + "_" + str_fname;
                                        //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                                        //str_restPath = Convert.ToString(obj_infrastructureNewApplication.ApplicationCode) + Path.DirectorySeparatorChar.ToString() + str_newFileName;
                                        //string str_infrastructureNewApplicationSubDirectory = str_startPathFromConfig + Convert.ToString(obj_infrastructureNewApplication.ApplicationCode);
                                        //if (!(Directory.Exists(str_infrastructureNewApplicationSubDirectory)))
                                        //{
                                        //    DirectoryInfo di = Directory.CreateDirectory(str_infrastructureNewApplicationSubDirectory);
                                        //}
                                        //str_newFileNameWithPath = str_startPathFromConfig + str_restPath;
                                        //File.WriteAllBytes(str_newFileNameWithPath, buffer);
                                        obj_insertInfrastructureNewApplicationAttachmentAferAdd.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadReferralLetter.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachmentAferAdd.ContentType = FileUploadReferralLetter.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachmentAferAdd.AttachmentPath = FileUploadReferralLetter.FileName;
                                        obj_insertInfrastructureNewApplicationAttachmentAferAdd.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload failed";
                                        lblMessageReferralLetter.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageReferralLetter.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload failed";
                                    lblMessageReferralLetter.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageReferralLetter.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload failed";
                                lblMessageReferralLetter.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageReferralLetter.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }

                        else
                        {
                            lblMessageReferralLetter.Text = "Please select a file..!!";
                            lblMessageReferralLetter.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertInfrastructureNewApplicationAttachmentAferAdd.ApplicationCode = obj_infrastructureNewApplication.ApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachmentAferAdd.AttachmentCode = int_ReferralLetterAttachCode;

                        obj_insertInfrastructureNewApplicationAttachmentAferAdd.AttachmentName = txtReferralLetter.Text;
                        // obj_insertInfrastructureNewApplicationAttachmentAferAdd.AttachmentPath = str_restPath;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUserAfterAdd = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachmentAferAdd.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureNewApplicationAttachmentAferAdd.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachmentAferAdd.AttachmentPath = str_newFileNameWithPath;

                        }
                        else
                        {
                            strStatus = "File Upload failed";
                            lblMessageReferralLetter.Text = obj_insertInfrastructureNewApplicationAttachmentAferAdd.CustumMessage;
                            lblMessageReferralLetter.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        lblMessageReferralLetter.Text = obj_insertInfrastructureNewApplicationAttachmentAferAdd.CustumMessage;
                        lblMessageReferralLetter.ForeColor = Color.Green;
                        obj_infrastructureNewReferralLetter = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text), Convert.ToInt32(ddlReferralLetter.SelectedValue));
                        BindInfrastructureNewApplicationAttachmentRefferalLetterDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text), obj_infrastructureNewReferralLetter.ReferralLetterAttachCode);
                        txtReferralLetter.Text = "";
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
                    lblMessageReferralLetter.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageReferralLetter.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }







    #endregion

    #region Button Click
    protected void btnPrev_Click(object sender, EventArgs e)
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
                Server.Transfer("OtherDetails.aspx");
            }
        }
    }
    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {

    }
    protected void txtSubmit_Click(object sender, EventArgs e)
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
                clearMessage();
                strActionName = "SubmitApplication";
                long lng_submittedApplicationCode = 0;
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));

                try
                {
                    string ErrorMessage = string.Empty;

                    if (lblWaterBalanceFlowChart2.Visible)
                    {
                        if (obj_InfrastructureNewApplication.GetWaterRequrementAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Water Requirementart" : ErrorMessage + ",Water Requirementart"; }
                    }
                    if (lblAffidavit2.Visible)
                    {
                        if (obj_InfrastructureNewApplication.GetAffidavitNonAvaAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Affidavit" : ErrorMessage + ",Affidavit"; }
                    }
                    if (lblSourceWaterAvailability2.Visible)
                    {
                        if (obj_InfrastructureNewApplication.GetSourceofAvailabilityofSurfaceWaterAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Source Water Availability/Non-availability Certificate" : ErrorMessage + ",Source Water Availability/Non-availability Certificate"; }
                    }

                    if (lblCertiNonAva2.Visible)
                    {
                        if (obj_InfrastructureNewApplication.GetCertiNonAvaAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Certificate of non-availability of water" : ErrorMessage + ",Certificate of non-availability of water"; }
                    }
                    if (lblRainWaterHarv2.Visible)
                    {
                        if (obj_InfrastructureNewApplication.GetRainwaterHarvestingAttachmentList(obj_InfrastructureNewApplication).Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Rain Water Harvesting/ Recharge" : ErrorMessage + ",Rain Water Harvesting/ Recharge"; }
                    }
                    if (lblImpactReport2.Visible)
                    {
                        if (obj_InfrastructureNewApplication.GetImpactAssOCSAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Impact Assessment Report by Accredited Consultant in case of Dewatering" : ErrorMessage + ",Impact Assessment Report by Accredited Consultant in case of Dewatering"; }
                    }
                    if (lblGroundwaterquality2.Visible)
                    {
                        if (obj_InfrastructureNewApplication.GetGroundwaterqualityAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Ground Water Quality" : ErrorMessage + ",Ground Water Quality"; }
                    }
                    if (lblCompletionCerti2.Visible)
                    {
                        if (obj_InfrastructureNewApplication.GetCompletionCertiAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Completion certificate from the concerned agency" : ErrorMessage + ",Completion certificate from the concerned agency"; }
                    }
                    if (lblInstSTP2.Visible)
                    {
                        if (obj_InfrastructureNewApplication.GetInstallationSTPAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Installation of STP (For New Projects)" : ErrorMessage + ",Installation of STP (For New Projects)"; }
                    }
                    if (lblWetlandArea2.Visible)
                    {
                        if (obj_InfrastructureNewApplication.GetWetlandAreaAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Approval from Wetland Authority" : ErrorMessage + ",Approval from Wetland Authority"; }
                    }

                    if (lblSigneddoc2.Visible)
                    {
                        if (obj_InfrastructureNewApplication.GetSignedDocAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Aplication with Signature and Seal" : ErrorMessage + ",Aplication with Signature and Seal"; }
                    }
                    if (lblPenalty2.Visible)
                    {
                        if (obj_InfrastructureNewApplication.GetPenaltyAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Penalty Attachment" : ErrorMessage + ",Penalty Attachment"; }
                    }

                    if (lblExtraAttachment2.Visible)
                    {
                        if (obj_InfrastructureNewApplication.GetExtraAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Extra Attachment" : ErrorMessage + ",Extra Attachment"; }
                    }
                    if (lblProofofownershipofland.Visible)
                    {
                        if (obj_InfrastructureNewApplication.GetProofOwnershipLandAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Proof of Ownership of land Attachment" : ErrorMessage + ",Proof of Ownership of land Attachment"; }
                    }
                    if (lblProofofownershipLease.Visible)
                    {
                        if (obj_InfrastructureNewApplication.GetProofOwnershipTankerAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Proof of ownership/lease of tanker Attachment" : ErrorMessage + ",Proof of ownership/Lease of tanker"; }
                    }




                    //if (lblHydrogeologicalReport2.Visible)
                    //{
                    //    if (obj_InfrastructureNewApplication.GetGroundwaterAvailabilityAttachmentList(obj_InfrastructureNewApplication).Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Hydrogeological Report" : ErrorMessage + ",Hydrogeological Report"; }
                    //}


                    //if (lblAuthorizationLetter2.Visible)
                    //{
                    //    if (obj_InfrastructureNewApplication.GetUndertakingAttachmentList(obj_InfrastructureNewApplication).Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Authorization Letter" : ErrorMessage + ",Authorization Letter"; }
                    //}

                    //if (lblConsentApproval2.Visible)
                    //{

                    //    if (AttachmentsCountReferalLetter() < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Consent/ Approval of Government Agency" : ErrorMessage + ",Consent/ Approval of Government Agency"; }
                    //}


                    if (!string.IsNullOrEmpty(ErrorMessage))
                    {
                        lblMessage.Text = ErrorMessage + " Attachments are Mandatory.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    lngInfSubmitAppCode = lng_submittedApplicationCode;
                    //Server.Transfer("Submit.aspx");
                    Server.Transfer("INFNewReadyToSubmit.aspx");

                }

                catch (ThreadAbortException)
                {


                }
                catch (Exception)
                {
                    //lblFinalMsg.Text = ex.Message;

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
                    obj_InfrastructureNewApplication.Dispose();
                }
            }
        }
    }
    #endregion

    #region Selectoin index Changed
    protected void gvExtra_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvApprovalLetterOfStateGovtAgency_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlReferralLetter_SelectedIndexChanged(object sender, EventArgs e)
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
                int int_ReferralLetterCode = Convert.ToInt32(ddlReferralLetter.SelectedValue);
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter obj_infrastructureNewReferralLetter = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text), int_ReferralLetterCode);
                BindInfrastructureNewApplicationAttachmentRefferalLetterDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text), obj_infrastructureNewReferralLetter.ReferralLetterAttachCode);
                lblMessageReferralLetter.Text = "";
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    #endregion


    protected void btnOTPVerify_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["ConsultantOTP"]) == txtImpactReportOCSOTP.Text)
        {
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewSADApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToUInt32(lblApplicationCode.Text));
            if (obj_infrastructureNewSADApplication.InfrastructureNewApplicationCode > 0)
            {
                obj_infrastructureNewSADApplication.ImpactAssOCSOTPVerified = NOCAP.BLL.Common.CommonApplication.ImpactAssOCSOTPVerifiedYesNo.Yes;
                obj_infrastructureNewSADApplication.ImpactAssOCSOTPVerifiedByCC = Convert.ToInt32(ddlConsultant.SelectedValue);
                obj_infrastructureNewSADApplication.ModifiedByExUC = Convert.ToInt64(Session["ExternalUserCode"]); ;
                int int_result = obj_infrastructureNewSADApplication.SetImpactAssOCSOTPVerified();
                if (int_result == 1)
                {
                    ddlConsultant.SelectedIndex = -1;
                    txtImpactReportOCSOTP.Text = "";
                    lblMessageImpactReport.Text = "OTP Verifired successfully.";
                    lblMessageImpactReport.ForeColor = Color.Green;
                    OTPVerifyEnavleDesable();
                    tableIAR.Visible = true;
                    return;
                }
                else
                {
                    lblMessageImpactReport.Text = "OTP Verification failed.";
                    lblMessageImpactReport.ForeColor = Color.Red;
                    tableIAR.Visible = false;
                    return;
                }

            }
            else
            {
                lblMessageImpactReport.Text = "Error on page.";
                lblMessageImpactReport.ForeColor = Color.Red;
                tableIAR.Visible = false;
                return;
            }
        }
        else
        {
            lblMessageImpactReport.Text = "Invalid OTP, Please try again";
            lblMessageImpactReport.ForeColor = Color.Red;
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

                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewSADApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt64(lblApplicationCode.Text));
                if (obj_infrastructureNewSADApplication.InfrastructureNewApplicationCode > 0)
                {

                    if (SMSUtility.IsSendSMSEnable())
                    {
                        OTPMessage = "Do not share OTP with anyone. OTP for submission of IAR / CHR to CGWA in respect of your application name : " + obj_infrastructureNewSADApplication.NameOfInfrastructure + " is :" + OTP;
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

                        string str_StateName = new NOCAP.BLL.Master.State(obj_infrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
                        string str_DistrictName = new NOCAP.BLL.Master.District(obj_infrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_infrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName;
                        string str_SubDistrictName = new NOCAP.BLL.Master.SubDistrict(obj_infrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_infrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_infrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;

                        if (obj_infrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown == NOCAP.BLL.Common.Address.VillageOrTownOption.Town)
                        {
                            str_TownName = "Town Name : ";
                            str_TownName = str_TownName + new NOCAP.BLL.Master.Town(obj_infrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_infrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_infrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_infrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.TownCode).TownName;
                            str_TownName = str_TownName + ", ";
                        }
                        if (obj_infrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown == NOCAP.BLL.Common.Address.VillageOrTownOption.Village)
                        {
                            str_VillageName = "Village Name : ";
                            str_VillageName = str_VillageName + new NOCAP.BLL.Master.Village(obj_infrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_infrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_infrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_infrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.VillageCode).VillageName;
                            str_VillageName = str_VillageName + ", ";
                        }
                        //for Address



                        string strBody = "<table style='max-width: 600px;margin: auto;width: 100%;border: 1px solid #f9f9f9;'><tr><td style='padding:30px;' colspan='3'><p>Dear <span>" + obj_ConsultantDetail.ConsultantName + "</span></p><p>Your One Time Password (OTP) for submission of Impact Assessment Report / Comprehensive Hydrogeological Report to CGWA in respect of Application for <span style='font-style: italic;'> " + obj_infrastructureNewSADApplication.NameOfInfrastructure + ", " + str_VillageName + str_TownName + " Sub District : " + str_SubDistrictName + ", District : " + str_DistrictName + ", State : " + str_StateName + " </span> is mentioned below.</p><p>Please enter this OTP in the required field to proceed further.</p></td></tr><tr style='background-color: #1d65a3;'><td style='color: #fff;text-align: center; padding: 5px 0px;' colspan='3'>DETAILS</td></tr><tr><td style='padding-left: 30px;'><strong>One Time Password (OTP)</strong></td><td style='padding-right: 30px;' colspan='2'><strong>" + OTP + "</strong></td></tr><tr><td  colspan='3' style='padding:30px;'><p>The above mentioned OTP is valid for 15 minutes.</p><p>Sincerely, <br /><strong>CGWA, New Delhi</strong></p></td></tr></table>";


                        //string strBody = "<p>Sir/Madam,</p><p> </br></br>Do not share OTP with anyone. OTP for submission of IAR / CHR to CGWA in respect of your application name : " + obj_infrastructureNewSADApplication.NameOfInfrastructure + " is <span style='color: blue;'> " + OTP + "</span> </p><p><br />  This is an auto-generated email.&nbsp; Do not reply to this email.<br />  </p>";
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