using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Configuration;
using NOCAP;
using System.Threading;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using NOCAP.BLL.Master;
using NOCAP.BLL.Industrial.New.SADApplication;

public partial class ExternalUser_IndustrialNew_AttachmentKLD : System.Web.UI.Page
{
    string strPageName = "INDAttachment";
    string strActionName = "";
    string strStatus = "";
    long lngIndSubmitAppCode;
    public long IndSubmitAppCode
    {
        get
        {
            return lngIndSubmitAppCode;
        }
        set
        {
            lngIndSubmitAppCode = value;
        }
    }
    decimal GroundWaterRequirement0KLD = 0;
    decimal GroundWaterRequirement10KLD = 10;
    decimal GroundWaterRequirement100KLD = 100;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        if (!IsPostBack)
        {
            try
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;

                //SetDefaultView();
                if (NOCAPExternalUtility.FillDropDownReferralLetterType(ref ddlReferralLetter) != 1)
                {

                    lblMessageReferralLetter.Text = "Problem in State Population!";
                }
                else
                {
                    ddlReferralLetter.Items[0].Value = "0";

                }



                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblModeFrom");
                        if (SourceLabel != null)
                        {
                            lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                        }
                    }

                }
                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblIndustialApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                        }
                    }

                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    


                    //BindIndustrialNewApplicationAttachmentSitePlanDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    //BindIndustrialNewApplicationAttachmentCertifiedRevenueSketchDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    //BindIndustrialNewApplicationAttachmentDocumentsofOwnershipAndLeasesofOwnershipDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    //BindIndustrialNewApplicationAttachmentWaterRequirementDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    lblApplicationCode.Text = lblIndustialApplicationCodeFrom.Text;
                    lblApplicationCodeForPayment.Text = lblIndustialApplicationCodeFrom.Text;



                    // BindUndertakingAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

                    BindAffidavitNonAvaAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    BindSourceofAvailabilityofSurfaceWaterAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    BindGroundwaterAvailabilityAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    BindNonPollutingAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    BindRainwaterHarvestingAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                  
                    //  BindRefferalLetterAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    BindConsentAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    BindMSMEAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    BindAffidavitOtherThanMSMEAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    BindWetlandAreaAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    
                 
                    BindSignedDocAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    BindExtraAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                  
                }
                // NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                // lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_IndustrialNewApplication.NameOfIndustry);

            }
            catch (Exception ex)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }


    }


    #region Private Function

    //void OTPVerifyEnavleDesable()
    //{

    //    IndustrialNewSADApplication obj_industrialNewSADApplication = new IndustrialNewSADApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
    //    if (obj_industrialNewSADApplication != null && obj_industrialNewSADApplication.IndustrialNewApplicationCode > 0)
    //    {
    //        if (obj_industrialNewSADApplication.ImpactAssOCSOTPVerified == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.ImpactAssOCSOTPVerifiedYesNo.Yes)
    //        {
    //            ddlConsultant.ClearSelection();
    //            ddlConsultant.Items.FindByValue(Convert.ToString(obj_industrialNewSADApplication.ImpactAssOCSOTPVerifiedByCC)).Selected = true;
    //            ddlConsultant.Enabled = false;
    //            txtImpactReportOCSOTP.Enabled = false;
    //            btnOTPVerify.Enabled = false;
    //            lblOTPVerified.Text = Convert.ToString(obj_industrialNewSADApplication.ImpactAssOCSOTPVerified);
    //            btnSendOTP.Enabled = false;
    //        }
    //        else
    //        {
    //            ddlConsultant.Enabled = true;
    //            txtImpactReportOCSOTP.Enabled = true;
    //            btnOTPVerify.Enabled = true;
    //            lblOTPVerified.Text = Convert.ToString(NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.ImpactAssOCSOTPVerifiedYesNo.No);
    //            btnSendOTP.Enabled = true;
    //        }
    //    }

    //}


    private void BindSourceofAvailabilityofSurfaceWaterAttachment(long lngA_ApplicationCode)
    {
        try
        {

            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvSourceofAvailabilityofSurfaceWater, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);

            lblSourceWaterAvailability.Text = HttpUtility.HtmlEncode("Source Water Availability/Non-availability Certificate (" + AttCount.ToString() + ")");


            #region Source Water Availability/Non-availability Certificate

            if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                lblSourceWaterAvailability2.Visible = false;
            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement100KLD))
                lblSourceWaterAvailability2.Visible = true;
            else if (decTotalGroundWaterRequirement > GroundWaterRequirement100KLD)
                lblSourceWaterAvailability2.Visible = true;
            else
                lblSourceWaterAvailability2.Visible = false;
            #endregion

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
            //lblMessageSourceofAvailabilityofSurfaceWater.Text = ex.Message;
        }
    }

    private void BindGroundwaterAvailabilityAttachment(long lngA_ApplicationCode)
    {
        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvGroundwaterAvailability, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);

            lblHydrogeologicalReport.Text = HttpUtility.HtmlEncode("Hydrogeological Report (" + AttCount.ToString() + ")");

            #region Hydrogeological Report


            if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                lblHydrogeologicalReport2.Visible = false;
            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement100KLD))
                lblHydrogeologicalReport2.Visible = false;
            else if (decTotalGroundWaterRequirement > GroundWaterRequirement100KLD)
                lblHydrogeologicalReport2.Visible = true;
            else
                lblHydrogeologicalReport2.Visible = false;

            #endregion
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
            //lblMessageGroundwaterAvailability.Text = ex.Message;
        }
    }
    private void BindRainwaterHarvestingAttachment(long lngA_ApplicationCode)
    {
        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvRainwaterHarvesting, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);

            lblRainWaterHarvesting.Text = HttpUtility.HtmlEncode("Rain Water Harvesting/Artificial Recharge proposal (" + AttCount.ToString() + ")");

            #region Rain Water Harvesting/Artificial Recharge proposal

            if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                lblRainWaterHarvesting2.Visible = true;
            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement100KLD))
                lblRainWaterHarvesting2.Visible = true;
            else if (decTotalGroundWaterRequirement > GroundWaterRequirement100KLD)
                lblRainWaterHarvesting2.Visible = true;
            else
                lblRainWaterHarvesting2.Visible = false;

            #endregion
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);

            //  lblMessageRainwaterHarvesting.Text = ex.Message;
        }
    }
    private void BindUndertakingAttachment(long lngA_ApplicationCode)
    {
        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvUndertaking, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblAuthorizationLetter.Text = HttpUtility.HtmlEncode("Authorization Letter (" + AttCount.ToString() + ")");

            #region Authorization Letter

            if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                lblAuthorizationLetter2.Visible = true;
            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement100KLD))
                lblAuthorizationLetter2.Visible = true;
            else if (decTotalGroundWaterRequirement > GroundWaterRequirement100KLD)
                lblAuthorizationLetter2.Visible = true;
            else
                lblAuthorizationLetter2.Visible = false;

            #endregion
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
            //lblMessageUndertaking.Text = ex.Message;
        }
    }
    private void BindExtraAttachment(long lngA_ApplicationCode)
    {
        try
        {

            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvExtra, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblExtraAttachment.Text = HttpUtility.HtmlEncode("Extra Attachment (" + AttCount.ToString() + ")");


            #region Extra Attachment

            if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                lblExtraAttachment2.Visible = false;
            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement100KLD))
                lblExtraAttachment2.Visible = false;
            else if (decTotalGroundWaterRequirement > GroundWaterRequirement100KLD)
                lblExtraAttachment2.Visible = false;
            else
                lblExtraAttachment2.Visible = false;

            #endregion
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
  
    private void BindConsentAttachment(long lngA_ApplicationCode)
    {
        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvConsent, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblConsent.Text = HttpUtility.HtmlEncode("Consent to Establish in case of Over Exploited Category (" + AttCount.ToString() + ")");


            #region Consent Attachment
            int aretypecode = AreaTypeCatCode(lngA_ApplicationCode);
            if (aretypecode == 5)
            {
                if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                    lblConsent2.Visible = true;
                else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement100KLD))
                    lblConsent2.Visible = true;
                else if (decTotalGroundWaterRequirement > GroundWaterRequirement100KLD)
                    lblConsent2.Visible = true;
                else
                    lblConsent2.Visible = true;
            }
            else
                lblConsent2.Visible = false;

            #endregion
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
  
    private void BindGridView(GridView gv, long lngA_ApplicationCode, ref decimal decTotalGroundWaterRequirement, ref int AttCount)
    {
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(lngA_ApplicationCode);
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList = null;

        if (gv.ID == "gvAffidavitNonAva")
            arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetAffidavitNonAvaAttachmentList();
        else if (gv.ID == "gvSourceofAvailabilityofSurfaceWater")
            arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetSourceofAvailabilityofSurfaceWaterAttachmentList();
        else if (gv.ID == "gvNonPolluting")
            arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetNonPollutingAttachmentList();
        else if (gv.ID == "gvRainwaterHarvesting")
            arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetRainwaterHarvestingAttachmentList();
        else if (gv.ID == "gvImpactReportOCS")
            arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetImpactAssOCSAttachmentList();
        else if (gv.ID == "gvReferralLetterAttachment")
            arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetCpoyOfReferralLetterAttachmentList();
        else if (gv.ID == "gvMSME")
        {
            arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetMSMEAttachmentList();
            switch (obj_industrialNewApplication.MSME)
            {
                case NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.MSMEYesNo.Yes:
                    lblMSME2.Visible = true;
                    break;
                case NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.MSMEYesNo.No:
                    lblMSME2.Visible = false;
                    break;
            }
        }
        else if (gv.ID == "gvAffidavitOtherThanMSME")
            arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetAffidavitOtherMSMEAttachmentList();
        else if (gv.ID == "gvWetlandArea")
        {
            arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetWetlandAreaAttachmentList();
            switch (obj_industrialNewApplication.WetLandArea)
            {
                case NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.WetLandAreaYesNo.Yes:
                    lblWetlandArea2.Visible = true;
                    break;
                case NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.WetLandAreaYesNo.No:
                    lblWetlandArea2.Visible = false;
                    break;
            }
        }
        else if (gv.ID == "gvBharatKoshReciept")
            arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetBharatKoshRecieptAttachmentList();
        else if (gv.ID == "gvAbstRestCharge")
            arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetAbsRestChargeAttachmentList();
        else if (gv.ID == "gvRestCharge")
            arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetRestChargeAttachmentList();
        else if (gv.ID == "gvSigneddoc")
            arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetSignedDocAttachmentList();
        else if (gv.ID == "gvPenalty")
            arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetPenaltyAttachmentList();
        else if (gv.ID == "gvConsent")
        {
            arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetConsentAttachmentList();
        }
        else if (gv.ID == "gvExtra")
            arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetExtraAttachmentList();
        else if (gv.ID == "gvGroundwaterAvailability")
            arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetGroundwaterAvailabilityAttachmentList();

        gv.DataSource = arr_industrialNewApplicationAttachmentList;
        gv.DataBind();
        AttCount = arr_industrialNewApplicationAttachmentList.Length;
        decTotalGroundWaterRequirement = Convert.ToDecimal(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement + (obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));
    }
    private void BindMSMEAttachment(long lngA_ApplicationCode)
    {
        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvMSME, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblMSME.Text = HttpUtility.HtmlEncode("MSME certificate in case of MSME (" + AttCount.ToString() + ")");
            // NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(lngA_ApplicationCode);

            if (lblMSME2.Visible)
            {
                if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                    lblMSME2.Visible = false;
                else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement100KLD))
                    lblMSME2.Visible = true;
                else if (decTotalGroundWaterRequirement > GroundWaterRequirement100KLD)
                    lblMSME2.Visible = true;
                else
                    lblMSME2.Visible = false;

            }

        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindWetlandAreaAttachment(long lngA_ApplicationCode)
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
                else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement100KLD))
                    lblWetlandArea2.Visible = true;
                else if (decTotalGroundWaterRequirement > GroundWaterRequirement100KLD)
                    lblWetlandArea2.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
   
 
    private void BindAffidavitNonAvaAttachment(long lngA_ApplicationCode)
    {
        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvAffidavitNonAva, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblAffidavitNonAva.Text = HttpUtility.HtmlEncode("Affidavit regarding Non-availability of water supply from local government agencies (" + AttCount.ToString() + ")");

            //decimal decTotalGroundWaterRequirement = Convert.ToDecimal(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement + (obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));
            if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                lblAffidavitNonAva2.Visible = true;
            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement100KLD))
                lblAffidavitNonAva2.Visible = false;
            else if (decTotalGroundWaterRequirement > GroundWaterRequirement100KLD)
                lblAffidavitNonAva2.Visible = false;

        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

 
    private void BindAffidavitOtherThanMSMEAttachment(long lngA_ApplicationCode)
    {
        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvAffidavitOtherThanMSME, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(lngA_ApplicationCode);
            lblAffidavitOtherThanMSME.Text = HttpUtility.HtmlEncode("Affidavit in case of drinking/domestic/green belt (OE areas) (" + AttCount.ToString() + ")");
            if (AreaTypeCatCode(lngA_ApplicationCode) == 5 && obj_industrialNewApplication.MSME == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.MSMEYesNo.No)
            {
                if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                    lblAffidavitOtherThanMSME2.Visible = true;
                else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement100KLD))
                    lblAffidavitOtherThanMSME2.Visible = true;
                // lblAffidavitOtherThanMSME.Text = HttpUtility.HtmlEncode("Affidavit in case of drinking/domestic/green belt (OE areas) (" + arr_industrialNewApplicationAttachmentList.Length.ToString() + ")");
                else if (decTotalGroundWaterRequirement > GroundWaterRequirement100KLD)
                    lblAffidavitOtherThanMSME2.Visible = true;
                // lblAffidavitOtherThanMSME.Text = HttpUtility.HtmlEncode("Affidavit in case of drinking/domestic/green belt (OE areas) (" + arr_industrialNewApplicationAttachmentList.Length.ToString() + ")");
            }
            else
            {
                lblAffidavitOtherThanMSME2.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void BindSignedDocAttachment(long lngA_ApplicationCode)
    {
        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvSigneddoc, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);

            lblSigneddoc2.Visible = true;
            lblSigneddoc.Text = HttpUtility.HtmlEncode("Aplication with Signature and Seal (" + AttCount.ToString() + ")");
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private int AreaTypeCatCode(long lngA_ApplicationCode)
    {
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(lngA_ApplicationCode);
        NOCAP.BLL.Master.SubDistrict obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(Convert.ToInt32(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode), Convert.ToInt32(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode), Convert.ToInt32(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode));
        NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_subDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplication.ApplySubDistrictAreaCategoryKey);
        obj_subDistrictAreaTypeCategoryHistory = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory();
        string AreaTypeCatCode = obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode.ToString();
        NOCAP.BLL.Master.AreaTypeCategory obj_AreaTypeCategory = new NOCAP.BLL.Master.AreaTypeCategory(obj_subDistrictAreaTypeCategoryHistory.AreaTypeCode, obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode);
        return obj_AreaTypeCategory.AreaTypeCategoryCode;
    }
    private void BindRefferalLetterAttachment(long lngA_ApplicationCode, int int_ReferralLetterAttachCode = 0)
    {

        try
        {

            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvReferralLetterAttachment, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);

            #region Consent/ Approval of Government Agency


            lblConsentApproval.Text = HttpUtility.HtmlEncode("Consent/ Approval of Government Agency (" + AttachmentsCountReferalLetter() + ")");
            if (AreaTypeCatCode(lngA_ApplicationCode) == 5)
            {

                if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                    lblConsentApproval2.Visible = true;
                else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement100KLD))
                    lblConsentApproval2.Visible = true;
                else if (decTotalGroundWaterRequirement > GroundWaterRequirement100KLD)
                    lblConsentApproval2.Visible = true;
                else
                    lblConsentApproval2.Visible = false;
            }
            else
                lblConsentApproval2.Visible = false;

            #endregion


        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindNonPollutingAttachment(long lngA_ApplicationCode)
    {
        try
        {

            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvNonPolluting, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);

            lblGroundWaterQualityReport.Text = HttpUtility.HtmlEncode("Ground Water Quality Report  (" + AttCount.ToString() + ")");

            #region Ground Water Quality Report
            if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
                lblGroundWaterQualityReport2.Visible = true;
            else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement100KLD))
                lblGroundWaterQualityReport2.Visible = true;
            else if (decTotalGroundWaterRequirement > GroundWaterRequirement100KLD)
                lblGroundWaterQualityReport2.Visible = true;
            else
                lblGroundWaterQualityReport2.Visible = false;

            #endregion
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);

        }
    }

    private int AttachmentSizeLimit()
    {
        try
        {
            NOCAP.BLL.Common.AttachmentLimit obj_attachmentLimit = new NOCAP.BLL.Common.AttachmentLimit();

            int AttachmentSize = 1048576 * (obj_attachmentLimit.SizeOfEachAttachment);
            return AttachmentSize;
        }
        catch
        {
            //lblMessageSitePlan.Text = "Problem in Fetch Data";
            //lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
            return 0;
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
            //lblMessageSitePlan.Text = "Problem in Fetch Data";
            //lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
            return 0;
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
            //lblMessageSitePlan.Text = "Problem in Fetch Data";
            //lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
            return 0;
        }
        finally
        {
        }
    }

    private void clearMessage()
    {
        lblmessageAffidavitNonAva.Text = "";
        txtAffidavitNonAva.Text = "";
        lblMessageSourceofAvailabilityofSurfaceWater.Text = "";
        txtSourceofAvailabilityofSurfaceWater.Text = "";
        lblMessageNonPolluting.Text = "";
        txtNonPollutingAttachment.Text = "";
        lblMessageRainwaterHarvesting.Text = "";
        txtRainwaterHarvesting.Text = "";
       
        lblMessageReferralLetter.Text = "";
        txtReferralLetter.Text = "";
        lblMessageConsent.Text = "";
        txtConsent.Text = "";
        lblMSMEMessage.Text = "";
        txtMSME.Text = "";
        lblMessageAffidavitOtherThanMSME.Text = "";
        txtAffidavitOtherThanMSME.Text = "";
        lblMessageWetlanArea.Text = "";
        txtWetlandArea.Text = "";
        
        lblMessageSigneddoc.Text = "";
        txtSigneddoc.Text = "";
        
        lblMessageExtra.Text = "";
        txtExtraAttachment.Text = "";
        lblMessageGroundwaterAvailability.Text = "";
        txtgvGroundwaterAvailability.Text = "";
        lblMessageUndertaking.Text = "";
        txtUndertaking.Text = "";
        lblMessageSitePlan.Text = "";
        txtSitePlanAttachment.Text = "";
        lblMessageCertifiedRevenueSketch.Text = "";
        txtCertifiedRevenueSketchAttachment.Text = "";
        lblMessageDocumentsofOwnership.Text = "";
        txtDocumentsofOwnership.Text = "";
        lblMessageWaterRequirement.Text = "";
        txtWaterRequirement.Text = "";
    }

    private int AttachmentsCountReferalLetter()
    {
        int Count = 0;
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
        string SelectedValue = ddlReferralLetter.SelectedValue;
        for (int i = 1; i < ddlReferralLetter.Items.Count; i++)
        {
            ddlReferralLetter.SelectedIndex = i;
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADReferralLetter obj_industrialNewReferralLetter = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADReferralLetter(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text), Convert.ToInt32(ddlReferralLetter.SelectedValue));
            Count = Count + obj_industrialNewApplication.GetCpoyOfReferralLetterAttachmentList(obj_industrialNewReferralLetter.ReferralLetterAttachCode, NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment.SortingFieldForAttachment.NoSorting).Length;
        }
        ddlReferralLetter.SelectedValue = HttpUtility.HtmlEncode(SelectedValue);
        return Count;
    }

    #endregion

    #region Upload Button Click
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
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
                arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetSitePlanAttachmentList();

                if (arr_industrialNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    try
                    {
                        string str_fname;
                        string str_ext;
                        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_insertIndustrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment();
                        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();
                        byte[] buffer = new byte[1];
                        if (FileUploadSitePlan.HasFile)
                        {

                            str_ext = System.IO.Path.GetExtension(FileUploadSitePlan.PostedFile.FileName).ToLower();
                            str_fname = FileUploadSitePlan.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadSitePlan.PostedFile))
                                {
                                    if (FileUploadSitePlan.PostedFile.ContentLength < AttachmentSizeLimit())
                                    {
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadSitePlan.PostedFile);
                                        obj_insertIndustrialNewApplicationAttachment.ContentType = FileUploadSitePlan.PostedFile.ContentType;
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentPath = FileUploadSitePlan.FileName;
                                        obj_insertIndustrialNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageSitePlan.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageSitePlan.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }

                            }
                            else
                            {
                                strStatus = "File Upload Failed";
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


                        obj_insertIndustrialNewApplicationAttachment.IndustrialNewApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentCode = obj_industrialNewApplication.ProposedLocation.ProposedLocationSitePlanAttachCode;

                        obj_insertIndustrialNewApplicationAttachment.AttachmentName = txtSitePlanAttachment.Text;


                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;



                        if (obj_insertIndustrialNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageSitePlan.Text = obj_insertIndustrialNewApplicationAttachment.CustumMessage;
                            lblMessageSitePlan.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";

                            lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                        }
                        // BindIndustrialNewApplicationAttachmentSitePlanDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));                                                                  
                        clearMessage();
                        lblMessageSitePlan.Text = obj_insertIndustrialNewApplicationAttachment.CustumMessage;

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
    protected void lbtnUplodSourceofAvailabilityofSurfaceWater_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
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
                    strActionName = "File Upload Source of Availability of Surface Water";
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
                    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetSourceofAvailabilityofSurfaceWaterAttachmentList();

                    if (arr_industrialNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                    {
                        string str_fname;
                        string str_ext;
                        string str_newFileNameWithPath = "";
                        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_insertIndustrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment();
                        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();
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
                                        if (txtFileUploadSourceofAvailabilityofSurfaceWater.PostedFile.ContentLength < AttachmentSizeLimit())
                                        {
                                            obj_insertIndustrialNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(txtFileUploadSourceofAvailabilityofSurfaceWater.PostedFile);
                                            obj_insertIndustrialNewApplicationAttachment.ContentType = txtFileUploadSourceofAvailabilityofSurfaceWater.PostedFile.ContentType;
                                            obj_insertIndustrialNewApplicationAttachment.AttachmentPath = txtFileUploadSourceofAvailabilityofSurfaceWater.FileName;
                                            obj_insertIndustrialNewApplicationAttachment.FileExtension = str_ext;
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
                            obj_insertIndustrialNewApplicationAttachment.IndustrialNewApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                            obj_insertIndustrialNewApplicationAttachment.AttachmentCode = obj_industrialNewApplication.SourceOfAvaillabilityOfSurfaceWaterForIndustrialUse.SourceOfAvalabilityOfSurfaceWaterAttachCode;
                            obj_insertIndustrialNewApplicationAttachment.AttachmentName = txtSourceofAvailabilityofSurfaceWater.Text;
                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                            obj_insertIndustrialNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                            if (obj_insertIndustrialNewApplicationAttachment.Add() == 1)
                            {
                                strStatus = "File Upload Success";
                                obj_insertIndustrialNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                                lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = Color.Green;

                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;
                            }
                            BindSourceofAvailabilityofSurfaceWaterAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

                            clearMessage();
                            lblMessageSourceofAvailabilityofSurfaceWater.Text = obj_insertIndustrialNewApplicationAttachment.CustumMessage;
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

                strActionName = "File Upload Certificate of Revenue Sketch";
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
                arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetCertifiedRevenueSketAttachmentList();

                if (arr_industrialNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    try
                    {
                        string str_fname;
                        string str_ext;
                        string str_newFileNameWithPath = "";

                        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_insertIndustrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment();
                        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();

                        byte[] buffer = new byte[1];



                        if (txtFileUploadCertifiedRevenueSketch.HasFile)
                        {

                            str_ext = System.IO.Path.GetExtension(txtFileUploadCertifiedRevenueSketch.PostedFile.FileName).ToLower();
                            str_fname = txtFileUploadCertifiedRevenueSketch.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {

                                if (NOCAPExternalUtility.IsValidFile(txtFileUploadCertifiedRevenueSketch.PostedFile))
                                {

                                    if (txtFileUploadCertifiedRevenueSketch.PostedFile.ContentLength < AttachmentSizeLimit())
                                    {

                                        obj_insertIndustrialNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(txtFileUploadCertifiedRevenueSketch.PostedFile);
                                        obj_insertIndustrialNewApplicationAttachment.ContentType = txtFileUploadCertifiedRevenueSketch.PostedFile.ContentType;
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentPath = txtFileUploadCertifiedRevenueSketch.FileName;
                                        obj_insertIndustrialNewApplicationAttachment.FileExtension = str_ext;


                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageCertifiedRevenueSketch.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageCertifiedRevenueSketch.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageCertifiedRevenueSketch.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageCertifiedRevenueSketch.Text = "Please select a file..!!";
                            lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertIndustrialNewApplicationAttachment.IndustrialNewApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentCode = obj_industrialNewApplication.ProposedLocation.ProposedLocationCertifiedRevenueSketAttachCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentName = txtCertifiedRevenueSketchAttachment.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertIndustrialNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Failed";

                            obj_insertIndustrialNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            lblMessageCertifiedRevenueSketch.ForeColor = Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";

                            lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
                        }

                        // BindIndustrialNewApplicationAttachmentCertifiedRevenueSketchDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageCertifiedRevenueSketch.Text = obj_insertIndustrialNewApplicationAttachment.CustumMessage;
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
                    lblMessageCertifiedRevenueSketch.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
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
                strActionName = "File Upload Document of Ownership";
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
                arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetDocumentsofOwnershipAndLeasesofOwnershipAttachmentList();

                if (arr_industrialNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_insertIndustrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment();
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();
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

                                    if (txtFileUploadDocumentsofOwnership.PostedFile.ContentLength < AttachmentSizeLimit())
                                    {

                                        obj_insertIndustrialNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(txtFileUploadDocumentsofOwnership.PostedFile);
                                        obj_insertIndustrialNewApplicationAttachment.ContentType = txtFileUploadDocumentsofOwnership.PostedFile.ContentType;
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentPath = txtFileUploadDocumentsofOwnership.FileName;
                                        obj_insertIndustrialNewApplicationAttachment.FileExtension = str_ext;

                                    }
                                    else
                                    {
                                        strStatus = "File Deleting Failed";
                                        lblMessageDocumentsofOwnership.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageDocumentsofOwnership.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }

                                }
                                else
                                {
                                    strStatus = "File Deleting Failed";
                                    lblMessageDocumentsofOwnership.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageDocumentsofOwnership.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Deleting Failed";
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

                        obj_insertIndustrialNewApplicationAttachment.IndustrialNewApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentCode = obj_industrialNewApplication.LandUseDetailOfExistingProposed.LandUseTypeOwnershipLeaseAttachCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentName = txtDocumentsofOwnership.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertIndustrialNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Deleted Success";
                            obj_insertIndustrialNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;

                            lblMessageDocumentsofOwnership.ForeColor = Color.Green;
                        }
                        else
                        {
                            strStatus = "File Deleting Failed";
                            lblMessageDocumentsofOwnership.ForeColor = System.Drawing.Color.Red;
                        }

                        //BindIndustrialNewApplicationAttachmentDocumentsofOwnershipAndLeasesofOwnershipDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageDocumentsofOwnership.Text = obj_insertIndustrialNewApplicationAttachment.CustumMessage;
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
                strActionName = "File Upload Water Balance Flow Chart";
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
                arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetWaterRequrementAttachmentList();

                if (arr_industrialNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_insertIndustrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment();
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();
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

                                    if (FileUploadWaterRequirement.PostedFile.ContentLength < AttachmentSizeLimit())
                                    {

                                        obj_insertIndustrialNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadWaterRequirement.PostedFile);
                                        obj_insertIndustrialNewApplicationAttachment.ContentType = FileUploadWaterRequirement.PostedFile.ContentType;
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentPath = FileUploadWaterRequirement.FileName;
                                        obj_insertIndustrialNewApplicationAttachment.FileExtension = str_ext;


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
                        obj_insertIndustrialNewApplicationAttachment.IndustrialNewApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentCode = obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.WaterRequrementAttachCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentName = txtWaterRequirement.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertIndustrialNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertIndustrialNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            lblMessageWaterRequirement.ForeColor = Color.Green;


                        }
                        else
                        {
                            strStatus = "File Upload Failed";

                            lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
                        }

                        //BindIndustrialNewApplicationAttachmentWaterRequirementDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageWaterRequirement.Text = obj_insertIndustrialNewApplicationAttachment.CustumMessage;
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
                strActionName = "File Upload Extra";
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
                arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetExtraAttachmentList();

                if (arr_industrialNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_insertIndustrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment();
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();
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
                                    if (FileUploadExtra.PostedFile.ContentLength < AttachmentSizeLimit())
                                    {
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadExtra.PostedFile);
                                        obj_insertIndustrialNewApplicationAttachment.ContentType = FileUploadExtra.PostedFile.ContentType;
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentPath = FileUploadExtra.FileName;
                                        obj_insertIndustrialNewApplicationAttachment.FileExtension = str_ext;
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
                        obj_insertIndustrialNewApplicationAttachment.IndustrialNewApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentCode = obj_industrialNewApplication.IndustrialNewExtraAttachmentCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentName = txtExtraAttachment.Text; ;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertIndustrialNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertIndustrialNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            lblMessageExtra.ForeColor = Color.Green;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                        }

                        BindExtraAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageExtra.Text = obj_insertIndustrialNewApplicationAttachment.CustumMessage;
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
 
    protected void btnUploadConsent_Click(object sender, EventArgs e)
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
                strActionName = "File Upload Consent";
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
                arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetConsentAttachmentList();

                if (arr_industrialNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_insertIndustrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment();
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadConsent.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadConsent.PostedFile.FileName).ToLower();
                            str_fname = FileUploadConsent.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadConsent.PostedFile))
                                {
                                    if (FileUploadConsent.PostedFile.ContentLength < AttachmentSizeLimit())
                                    {
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadConsent.PostedFile);
                                        obj_insertIndustrialNewApplicationAttachment.ContentType = FileUploadConsent.PostedFile.ContentType;
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentPath = FileUploadConsent.FileName;
                                        obj_insertIndustrialNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageConsent.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageConsent.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageConsent.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageConsent.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageConsent.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageConsent.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageConsent.Text = "Please select a file..!!";
                            lblMessageConsent.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertIndustrialNewApplicationAttachment.IndustrialNewApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentCode = obj_industrialNewApplication.ConsentAttCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentName = txtConsent.Text; ;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertIndustrialNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertIndustrialNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            lblMessageConsent.ForeColor = Color.Green;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageConsent.ForeColor = System.Drawing.Color.Red;
                        }

                        BindConsentAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageConsent.Text = obj_insertIndustrialNewApplicationAttachment.CustumMessage;
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
                strActionName = "File Upload Ground Water Avaialbility";
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
                arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetGroundwaterAvailabilityAttachmentList();

                if (arr_industrialNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_insertIndustrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment();
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();
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

                                        obj_insertIndustrialNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadGroundwaterAvailability.PostedFile);
                                        obj_insertIndustrialNewApplicationAttachment.ContentType = FileUploadGroundwaterAvailability.PostedFile.ContentType;
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentPath = FileUploadGroundwaterAvailability.FileName;
                                        obj_insertIndustrialNewApplicationAttachment.FileExtension = str_ext;


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
                        obj_insertIndustrialNewApplicationAttachment.IndustrialNewApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentCode = obj_industrialNewApplication.GroundWaterAvailability.GroundWaterAvailabilityAttachCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentName = txtgvGroundwaterAvailability.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertIndustrialNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertIndustrialNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            lblMessageGroundwaterAvailability.ForeColor = Color.Green;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                        }
                        BindGroundwaterAvailabilityAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageGroundwaterAvailability.Text = obj_insertIndustrialNewApplicationAttachment.CustumMessage;

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
                if (Page.IsValid)
                {
                    strActionName = "File Upload Refferal Letter";
                    int int_CountAttForRefLetter = 0;
                    int int_ReferralLetterCode = Convert.ToInt32(ddlReferralLetter.SelectedValue);
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADReferralLetter obj_industrialNewReferralLetterk = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADReferralLetter(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text), int_ReferralLetterCode);
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
                    if (obj_industrialNewReferralLetterk.ReferralLetterAttachCode == 0)
                    {
                        int_CountAttForRefLetter = 0;
                    }
                    else
                    {
                        arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetCpoyOfReferralLetterAttachmentList(obj_industrialNewReferralLetterk.ReferralLetterAttachCode, NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment.SortingFieldForAttachment.NoSorting);
                        int_CountAttForRefLetter = arr_industrialNewApplicationAttachmentList.Count();
                    }
                    if (int_CountAttForRefLetter < AttachmentNumberLimit())
                    {

                        string str_fname;
                        string str_ext;
                        string str_newFileNameWithPath = "";
                        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADReferralLetter obj_industrialNewReferralLetter = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADReferralLetter(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text), Convert.ToInt32(ddlReferralLetter.SelectedValue));
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        int int_ReferralLetterAttachCode;
                        try
                        {

                            int_ReferralLetterAttachCode = obj_industrialNewReferralLetter.ReferralLetterAttachCode;
                            if (obj_industrialNewReferralLetter.ReferralLetterAttachCode == 0)
                            {
                                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADReferralLetter obj_industrialNewReferralLetterForAdd = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADReferralLetter();
                                obj_industrialNewReferralLetterForAdd.IndustrialNewApplicationCode = Convert.ToInt32(lblIndustialApplicationCodeFrom.Text);
                                obj_industrialNewReferralLetterForAdd.ReferralLetterTypeCode = Convert.ToInt32(ddlReferralLetter.SelectedValue);
                                obj_industrialNewReferralLetterForAdd.CreatedByExUC = obj_externalUser.ExternalUserCode;
                                if (obj_industrialNewReferralLetterForAdd.Add() != 1)
                                {
                                    lblMessageReferralLetter.Text = obj_industrialNewReferralLetterForAdd.CustumMessage;
                                    lblMessageReferralLetter.ForeColor = Color.Green;
                                    return;
                                }
                                int_ReferralLetterAttachCode = obj_industrialNewReferralLetterForAdd.ReferralLetterAttachCode;
                            }

                            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_insertIndustrialNewApplicationAttachmentAferAdd = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment();
                            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationAfterAdd = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                            List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();
                            byte[] buffer = new byte[1];
                            if (FileUploadReferralLetter.HasFile)
                            {
                                str_ext = System.IO.Path.GetExtension(FileUploadReferralLetter.PostedFile.FileName).ToLower();
                                str_fname = FileUploadReferralLetter.FileName;
                                if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                                {
                                    if (NOCAPExternalUtility.IsValidFile(FileUploadReferralLetter.PostedFile))
                                    {

                                        if (FileUploadReferralLetter.PostedFile.ContentLength < AttachmentSizeLimit())
                                        {
                                            obj_insertIndustrialNewApplicationAttachmentAferAdd.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadReferralLetter.PostedFile);
                                            obj_insertIndustrialNewApplicationAttachmentAferAdd.ContentType = FileUploadReferralLetter.PostedFile.ContentType;
                                            obj_insertIndustrialNewApplicationAttachmentAferAdd.AttachmentPath = FileUploadReferralLetter.FileName;
                                            obj_insertIndustrialNewApplicationAttachmentAferAdd.FileExtension = str_ext;
                                        }
                                        else
                                        {
                                            strStatus = "File Upload Failed";
                                            lblMessageReferralLetter.Text = "File can not upload. It has more than 5 MB size";
                                            lblMessageReferralLetter.ForeColor = System.Drawing.Color.Red;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageReferralLetter.Text = "Not a valid file!!..Select an other file!!";
                                        lblMessageReferralLetter.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
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
                            obj_insertIndustrialNewApplicationAttachmentAferAdd.IndustrialNewApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                            obj_insertIndustrialNewApplicationAttachmentAferAdd.AttachmentCode = int_ReferralLetterAttachCode;
                            obj_insertIndustrialNewApplicationAttachmentAferAdd.AttachmentName = txtReferralLetter.Text;
                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUserAfterAdd = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                            obj_insertIndustrialNewApplicationAttachmentAferAdd.CreatedByExUC = obj_externalUser.ExternalUserCode;
                            if (obj_insertIndustrialNewApplicationAttachmentAferAdd.Add() == 1)
                            {
                                strStatus = "File Upload Success";
                                obj_insertIndustrialNewApplicationAttachmentAferAdd.AttachmentPath = str_newFileNameWithPath;
                                lblMessageReferralLetter.Text = obj_insertIndustrialNewApplicationAttachmentAferAdd.CustumMessage;
                                lblMessageReferralLetter.ForeColor = Color.Green;
                            }
                            else
                            {
                                strStatus = "File Upload Failed";

                                lblMessageReferralLetter.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                            obj_industrialNewReferralLetter = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADReferralLetter(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text), Convert.ToInt32(ddlReferralLetter.SelectedValue));
                            BindRefferalLetterAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text), obj_industrialNewReferralLetter.ReferralLetterAttachCode);
                            clearMessage();
                            lblMessageReferralLetter.Text = obj_insertIndustrialNewApplicationAttachmentAferAdd.CustumMessage;
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
                strActionName = "File Upload Rain Water Harvesting";
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
                arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetRainwaterHarvestingAttachmentList();

                if (arr_industrialNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_insertIndustrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment();
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();
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

                                    if (FileUploadRainwaterHarvesting.PostedFile.ContentLength < AttachmentSizeLimit())
                                    {
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadRainwaterHarvesting.PostedFile);
                                        obj_insertIndustrialNewApplicationAttachment.ContentType = FileUploadRainwaterHarvesting.PostedFile.ContentType;
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentPath = FileUploadRainwaterHarvesting.FileName;
                                        obj_insertIndustrialNewApplicationAttachment.FileExtension = str_ext;
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
                        obj_insertIndustrialNewApplicationAttachment.IndustrialNewApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentCode = obj_industrialNewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeAttachCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentName = txtRainwaterHarvesting.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertIndustrialNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertIndustrialNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;

                            lblMessageRainwaterHarvesting.ForeColor = Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";

                            lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                        }
                        BindRainwaterHarvestingAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageRainwaterHarvesting.Text = obj_insertIndustrialNewApplicationAttachment.CustumMessage;
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
                if (Page.IsValid)
                {
                    strActionName = "File Upload Undertaking";
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
                    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetUndertakingAttachmentList();

                    if (arr_industrialNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                    {
                        string str_fname;
                        string str_ext;
                        string str_newFileNameWithPath = "";
                        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_insertIndustrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment();
                        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();
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
                                        if (FileUploadUndertaking.PostedFile.ContentLength < AttachmentSizeLimit())
                                        {
                                            obj_insertIndustrialNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadUndertaking.PostedFile);
                                            obj_insertIndustrialNewApplicationAttachment.ContentType = FileUploadUndertaking.PostedFile.ContentType;
                                            obj_insertIndustrialNewApplicationAttachment.AttachmentPath = FileUploadUndertaking.FileName;
                                            obj_insertIndustrialNewApplicationAttachment.FileExtension = str_ext;
                                        }
                                        else
                                        {
                                            strStatus = "File Upload Success";
                                            lblMessageUndertaking.Text = "File can not upload. It has more than 5 MB size";
                                            lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Success";
                                        lblMessageUndertaking.Text = "Not a valid file!!..Select an other file!!";
                                        lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Success";
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
                            obj_insertIndustrialNewApplicationAttachment.IndustrialNewApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                            obj_insertIndustrialNewApplicationAttachment.AttachmentCode = obj_industrialNewApplication.Undertaking.UndertakingAttachCode;
                            obj_insertIndustrialNewApplicationAttachment.AttachmentName = txtUndertaking.Text;
                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                            obj_insertIndustrialNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                            if (obj_insertIndustrialNewApplicationAttachment.Add() == 1)
                            {
                                strStatus = "File Upload Success";
                                obj_insertIndustrialNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;

                                lblMessageUndertaking.ForeColor = Color.Green;
                            }
                            else
                            {
                                strStatus = "File Upload Failed";

                                lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                // return;
                            }
                            BindUndertakingAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                            clearMessage();
                            lblMessageUndertaking.Text = obj_insertIndustrialNewApplicationAttachment.CustumMessage;
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
    }
    protected void btnNonPolluting_Click(object sender, EventArgs e)
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
                strActionName = "File Upload Ground Water Quality Report";
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
                arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetNonPollutingAttachmentList();

                if (arr_industrialNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_insertIndustrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment();
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadNonPolluting.HasFile)
                        {

                            str_ext = System.IO.Path.GetExtension(FileUploadNonPolluting.PostedFile.FileName).ToLower();

                            str_fname = FileUploadNonPolluting.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadNonPolluting.PostedFile))
                                {

                                    if (FileUploadNonPolluting.PostedFile.ContentLength < AttachmentSizeLimit())
                                    {
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadNonPolluting.PostedFile);
                                        obj_insertIndustrialNewApplicationAttachment.ContentType = FileUploadNonPolluting.PostedFile.ContentType;
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentPath = FileUploadNonPolluting.FileName;
                                        obj_insertIndustrialNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageNonPolluting.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageNonPolluting.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageNonPolluting.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageNonPolluting.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageNonPolluting.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageNonPolluting.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageNonPolluting.Text = "Please select a file..!!";
                            lblMessageNonPolluting.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertIndustrialNewApplicationAttachment.IndustrialNewApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentCode = obj_industrialNewApplication.IndustrialNewNonPollutingAttCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentName = txtNonPollutingAttachment.Text; ;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertIndustrialNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertIndustrialNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            lblMessageNonPolluting.ForeColor = Color.Green;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";

                            lblMessageNonPolluting.ForeColor = System.Drawing.Color.Red;

                        }
                        BindNonPollutingAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageNonPolluting.Text = obj_insertIndustrialNewApplicationAttachment.CustumMessage;
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
                    lblMessageNonPolluting.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageNonPolluting.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

    }
    protected void btnUploadSigneddoc_Click(object sender, EventArgs e)
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
                strActionName = "File Upload Signed Document attachment";
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
                arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetSignedDocAttachmentList();

                if (arr_industrialNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_insertIndustrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment();
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadSigneddoc.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadSigneddoc.PostedFile.FileName).ToLower();
                            str_fname = FileUploadSigneddoc.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadSigneddoc.PostedFile))
                                {
                                    if (FileUploadSigneddoc.PostedFile.ContentLength < AttachmentSizeLimit())
                                    {
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadSigneddoc.PostedFile);
                                        obj_insertIndustrialNewApplicationAttachment.ContentType = FileUploadSigneddoc.PostedFile.ContentType;
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentPath = FileUploadSigneddoc.FileName;
                                        obj_insertIndustrialNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageSigneddoc.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageSigneddoc.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageSigneddoc.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageSigneddoc.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageSigneddoc.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageSigneddoc.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageSigneddoc.Text = "Please select a file..!!";
                            lblMessageSigneddoc.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertIndustrialNewApplicationAttachment.IndustrialNewApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentCode = obj_industrialNewApplication.SignedDocAttCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentName = txtSigneddoc.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertIndustrialNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertIndustrialNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            lblMessageSigneddoc.ForeColor = Color.Green;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageSigneddoc.ForeColor = System.Drawing.Color.Red;
                        }
                        BindSignedDocAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageSigneddoc.Text = obj_insertIndustrialNewApplicationAttachment.CustumMessage;
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

 
    protected void btnUploadMSME_Click(object sender, EventArgs e)
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
                strActionName = "File MSME";
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
                arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetMSMEAttachmentList();

                if (arr_industrialNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_insertIndustrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment();
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadMSME.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadMSME.PostedFile.FileName).ToLower();
                            str_fname = FileUploadMSME.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadMSME.PostedFile))
                                {
                                    if (FileUploadMSME.PostedFile.ContentLength < AttachmentSizeLimit())
                                    {
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadMSME.PostedFile);
                                        obj_insertIndustrialNewApplicationAttachment.ContentType = FileUploadMSME.PostedFile.ContentType;
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentPath = FileUploadMSME.FileName;
                                        obj_insertIndustrialNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMSMEMessage.Text = "File can not upload. It has more than 5 MB size";
                                        lblMSMEMessage.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMSMEMessage.Text = "Not a valid file!!..Select an other file!!";
                                    lblMSMEMessage.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMSMEMessage.Text = "Not a valid file!!..Select an other file!!";
                                lblMSMEMessage.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMSMEMessage.Text = "Please select a file..!!";
                            lblMSMEMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertIndustrialNewApplicationAttachment.IndustrialNewApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentCode = obj_industrialNewApplication.MSMEAttCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentName = txtMSME.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertIndustrialNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertIndustrialNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            lblMSMEMessage.ForeColor = Color.Green;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMSMEMessage.ForeColor = System.Drawing.Color.Red;
                        }

                        BindMSMEAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        clearMessage();
                        lblMSMEMessage.Text = obj_insertIndustrialNewApplicationAttachment.CustumMessage;
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
                strActionName = "File Wetland";
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
                arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetWetlandAreaAttachmentList();

                if (arr_industrialNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_insertIndustrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment();
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();
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
                                    if (FileUploadWetlandArea.PostedFile.ContentLength < AttachmentSizeLimit())
                                    {
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadWetlandArea.PostedFile);
                                        obj_insertIndustrialNewApplicationAttachment.ContentType = FileUploadWetlandArea.PostedFile.ContentType;
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentPath = FileUploadWetlandArea.FileName;
                                        obj_insertIndustrialNewApplicationAttachment.FileExtension = str_ext;
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
                        obj_insertIndustrialNewApplicationAttachment.IndustrialNewApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentCode = obj_industrialNewApplication.WetlandAreaAttCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentName = txtWetlandArea.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertIndustrialNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertIndustrialNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            lblMessageWetlanArea.ForeColor = Color.Green;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";

                            lblMessageWetlanArea.ForeColor = System.Drawing.Color.Red;
                        }

                        BindWetlandAreaAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageWetlanArea.Text = obj_insertIndustrialNewApplicationAttachment.CustumMessage;
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
   
    
    protected void btnAffidavitNonAva_Click(object sender, EventArgs e)
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
                strActionName = "File Affidavit Non Ava Charges";
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
                arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetAffidavitNonAvaAttachmentList();

                if (arr_industrialNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_insertIndustrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment();
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadAffidavitNonAva.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadAffidavitNonAva.PostedFile.FileName).ToLower();
                            str_fname = FileUploadAffidavitNonAva.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadAffidavitNonAva.PostedFile))
                                {
                                    if (FileUploadAffidavitNonAva.PostedFile.ContentLength < AttachmentSizeLimit())
                                    {
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadAffidavitNonAva.PostedFile);
                                        obj_insertIndustrialNewApplicationAttachment.ContentType = FileUploadAffidavitNonAva.PostedFile.ContentType;
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentPath = FileUploadAffidavitNonAva.FileName;
                                        obj_insertIndustrialNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblmessageAffidavitNonAva.Text = "File can not upload. It has more than 5 MB size";
                                        lblmessageAffidavitNonAva.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblmessageAffidavitNonAva.Text = "Not a valid file!!..Select an other file!!";
                                    lblmessageAffidavitNonAva.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblmessageAffidavitNonAva.Text = "Not a valid file!!..Select an other file!!";
                                lblmessageAffidavitNonAva.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblmessageAffidavitNonAva.Text = "Please select a file..!!";
                            lblmessageAffidavitNonAva.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertIndustrialNewApplicationAttachment.IndustrialNewApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentCode = obj_industrialNewApplication.AffidavitNonAvaAttCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentName = txtAffidavitNonAva.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertIndustrialNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertIndustrialNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            lblmessageAffidavitNonAva.ForeColor = Color.Green;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";

                            lblmessageAffidavitNonAva.ForeColor = System.Drawing.Color.Red;
                        }

                        BindAffidavitNonAvaAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        clearMessage();
                        lblmessageAffidavitNonAva.Text = obj_insertIndustrialNewApplicationAttachment.CustumMessage;
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

  
    protected void btnUploadAffidavitOtherThanMSME_Click(object sender, EventArgs e)
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
                strActionName = "File  Affidavit Other Than MSME";
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
                arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetAffidavitOtherMSMEAttachmentList();

                if (arr_industrialNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_insertIndustrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment();
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadAffidavitOtherThanMSME.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadAffidavitOtherThanMSME.PostedFile.FileName).ToLower();
                            str_fname = FileUploadAffidavitOtherThanMSME.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadAffidavitOtherThanMSME.PostedFile))
                                {
                                    if (FileUploadAffidavitOtherThanMSME.PostedFile.ContentLength < AttachmentSizeLimit())
                                    {
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadAffidavitOtherThanMSME.PostedFile);
                                        obj_insertIndustrialNewApplicationAttachment.ContentType = FileUploadAffidavitOtherThanMSME.PostedFile.ContentType;
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentPath = FileUploadAffidavitOtherThanMSME.FileName;
                                        obj_insertIndustrialNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageAffidavitOtherThanMSME.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageAffidavitOtherThanMSME.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageAffidavitOtherThanMSME.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageAffidavitOtherThanMSME.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageAffidavitOtherThanMSME.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageAffidavitOtherThanMSME.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageAffidavitOtherThanMSME.Text = "Please select a file..!!";
                            lblMessageAffidavitOtherThanMSME.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertIndustrialNewApplicationAttachment.IndustrialNewApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentCode = obj_industrialNewApplication.AffidavitOtherMSMEAttCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentName = txtAffidavitOtherThanMSME.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertIndustrialNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertIndustrialNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            lblMessageAffidavitOtherThanMSME.ForeColor = Color.Green;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageAffidavitOtherThanMSME.ForeColor = System.Drawing.Color.Red;
                        }

                        BindAffidavitOtherThanMSMEAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageAffidavitOtherThanMSME.Text = obj_insertIndustrialNewApplicationAttachment.CustumMessage;
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

    #region RowDeleting

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
                if (DeleteAttchment((GridView)sender, e, lblMessageSitePlan, strActionName) == 1)
                {
                    //BindIndustrialNewApplicationAttachmentSitePlanDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                }

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
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
                if (DeleteAttchment((GridView)sender, e, lblMessageCertifiedRevenueSketch, strActionName) == 1)
                {
                    //BindIndustrialNewApplicationAttachmentCertifiedRevenueSketchDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
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
                if (DeleteAttchment((GridView)sender, e, lblMessageDocumentsofOwnership, strActionName) == 1)
                {

                    //BindIndustrialNewApplicationAttachmentDocumentsofOwnershipAndLeasesofOwnershipDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
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
                strActionName = "File Delete Water Balance Flow Chart";
                if (DeleteAttchment((GridView)sender, e, lblMessageGroundwaterAvailability, strActionName) == 1)
                {
                    // BindIndustrialNewApplicationAttachmentWaterRequirementDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                }
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
                strActionName = "File Delete Ground Water Availability";
                if (DeleteAttchment((GridView)sender, e, lblMessageGroundwaterAvailability, strActionName) == 1)
                    BindGroundwaterAvailabilityAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
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
                strActionName = "File Delete Rain Water Harvesting/Artificial Recharge proposal";
                if (DeleteAttchment((GridView)sender, e, lblMessageRainwaterHarvesting, strActionName) == 1)
                    BindRainwaterHarvestingAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

            }
            catch (Exception)
            {
                //lblMessageRainwaterHarvesting.Text = ex.Message;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
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
                strActionName = "File Delete Undertaking";
                if (DeleteAttchment((GridView)sender, e, lblMessageUndertaking, strActionName) == 1)

                    BindUndertakingAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
            }
            catch (Exception)
            {
                //lblMessageUndertaking.Text = ex.Message;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
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
                strActionName = "Delete Consent/ Approval of Government Agency";
                long lng_IndustrialNewApplicationCode = Convert.ToInt32(gvReferralLetterAttachment.DataKeys[e.RowIndex].Values["IndustrialNewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvReferralLetterAttachment.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvReferralLetterAttachment.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_industrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment(lng_IndustrialNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);


                //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                //string str_fullPath = str_startPathFromConfig + obj_industrialNewApplicationAttachment.AttachmentPath;

                ///////////////////////


                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;

                arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetCpoyOfReferralLetterAttachmentList(int_AttachmentCode, NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment.SortingFieldForAttachment.NoSorting);


                //-----------------------

                if (obj_industrialNewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Success";
                    lblMessageReferralLetter.Text = obj_industrialNewApplicationAttachment.CustumMessage;
                    lblMessageReferralLetter.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}

                    if (arr_industrialNewApplicationAttachmentList.Count() == 1)
                    {
                        strStatus = "Filde Delete Failed";
                        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADReferralLetter obj_industrialNewReferralLetterForDelete = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADReferralLetter(lng_IndustrialNewApplicationCode, Convert.ToInt32(ddlReferralLetter.SelectedValue));
                        obj_industrialNewReferralLetterForDelete.Delete();
                    }

                    int int_ReferralLetterCode = Convert.ToInt32(ddlReferralLetter.SelectedValue);
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADReferralLetter obj_industrialNewReferralLetter = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADReferralLetter(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text), int_ReferralLetterCode);
                    BindRefferalLetterAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text), obj_industrialNewReferralLetter.ReferralLetterAttachCode);

                }
                else
                {
                    lblMessageReferralLetter.Text = obj_industrialNewApplicationAttachment.CustumMessage;
                    lblMessageReferralLetter.ForeColor = System.Drawing.Color.Red;

                }
            }
            catch (Exception)
            {
                // lblMessageReferralLetter.Text = ex.Message;
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
                if (DeleteAttchment((GridView)sender, e, lblMessageSourceofAvailabilityofSurfaceWater, strActionName) == 1)

                    BindSourceofAvailabilityofSurfaceWaterAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));



            }
            catch (Exception)
            {
                //lblMessageSourceofAvailabilityofSurfaceWater.Text = ex.Message;
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
                strActionName = "File Delete Extra";

                if (DeleteAttchment((GridView)sender, e, lblMessageExtra, strActionName) == 1)
                    BindExtraAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

            }
            catch (Exception)
            {
                // lblMessageExtra.Text = ex.Message;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
  
    protected void gvConsent_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Consent";
                if (DeleteAttchment((GridView)sender, e, lblMessageConsent, strActionName) == 1)
                    BindConsentAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
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
            long lng_IndustrialNewApplicationCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["IndustrialNewApplicationCode"]);
            int int_AttachmentCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["AttachmentCode"]);
            int int_AttachmentCodeSerialNumber = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
            lblMessage.ForeColor = System.Drawing.Color.Red;
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_industrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment(lng_IndustrialNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
            if (obj_industrialNewApplicationAttachment.Delete() == 1)
            {
                lblMessage.Text = obj_industrialNewApplicationAttachment.CustumMessage;
                strStatus = "File Delete Success";
                return 1;
            }
            else
            {
                lblMessage.Text = obj_industrialNewApplicationAttachment.CustumMessage;
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
    protected void gvMSME_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete MSME";
                if (DeleteAttchment((GridView)sender, e, lblMSMEMessage, strActionName) == 1)
                    BindMSMEAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

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

                    BindWetlandAreaAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }

    
  
    protected void gvSigneddoc_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Signed documnet";
                if (DeleteAttchment((GridView)sender, e, lblMessageSigneddoc, strActionName) == 1)

                    BindSignedDocAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }

    protected void gvNonPolluting_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //Delete
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
                strActionName = "File Delete Non-Polluting";
                if (DeleteAttchment((GridView)sender, e, lblMessageNonPolluting, strActionName) == 1)

                    BindNonPollutingAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));


            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }


    protected void gvAffidavitNonAva_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Affidavit non availablity";
                if (DeleteAttchment((GridView)sender, e, lblmessageAffidavitNonAva, strActionName) == 1)
                    BindAffidavitNonAvaAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }
     protected void gvAffidavitOtherThanMSME_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Other than MSME";
                if (DeleteAttchment((GridView)sender, e, lblMessageAffidavitOtherThanMSME, strActionName) == 1)
                    BindAffidavitOtherThanMSMEAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }
    #endregion

    #region ViewFile_Click

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
                    long lng_industrialNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.INDSADAppDownloadFiles(lng_industrialNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                    long lng_industrialNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.INDSADAppDownloadFiles(lng_industrialNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

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


    #endregion



    #region SelectedIndexChanged
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
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADReferralLetter obj_industrialNewReferralLetter = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADReferralLetter(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text), int_ReferralLetterCode);

                BindRefferalLetterAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text), obj_industrialNewReferralLetter.ReferralLetterAttachCode);
                lblMessageReferralLetter.Text = "";
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
                //lblMessage.Text = ex.Message;
                //lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    #endregion

    #region Button Click
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            Server.Transfer("AbstractionStructureKLD.aspx");

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
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
                strActionName = "Submit Application";
                long lng_submittedApplicationCode = 0;
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                try
                {
                    string ErrorMessage = string.Empty;

                    if (lblAffidavitNonAva2.Visible)
                    {

                        if (obj_industrialNewApplication.GetAffidavitNonAvaAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Affidavit regarding Non - availability of water supply from local government agencies" : ErrorMessage + ",Affidavit regarding Non - availability of water supply from local government agencies"; }
                    }
                    if (lblSourceWaterAvailability2.Visible)
                    {
                        if (obj_industrialNewApplication.GetSourceofAvailabilityofSurfaceWaterAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Source Water Availability/Non-availability Certificate" : ErrorMessage + ",Source Water Availability/Non-availability Certificate"; }
                    }
                    if (lblGroundWaterQualityReport2.Visible)
                    {
                        if (obj_industrialNewApplication.GetNonPollutingAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Ground Water Quality Report" : ErrorMessage + ",Ground Water Quality Report"; }
                    }
                    if (lblRainWaterHarvesting2.Visible)
                    {
                        if (obj_industrialNewApplication.GetRainwaterHarvestingAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Rain Water Harvesting/Artificial Recharge proposal" : ErrorMessage + ",Rain Water Harvesting/Artificial Recharge proposal"; }
                    }
                   
                    if (lblConsent2.Visible)
                    {
                        if (obj_industrialNewApplication.GetConsentAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Consent to Establish in case of Over Exploited Category" : ErrorMessage + ",Consent to Establish in case of Over Exploited Category"; }
                    }
                    if (lblMSME2.Visible)
                    {
                        if (obj_industrialNewApplication.GetMSMEAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "MSME certificate in case of MSME" : ErrorMessage + ",MSME certificate in case of MSME"; }
                    }
                    if (lblAffidavitOtherThanMSME2.Visible)
                    {
                        if (obj_industrialNewApplication.GetAffidavitOtherMSMEAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Affidavit in case of drinking/domestic/green belt (OE areas) for industries other than MSME" : ErrorMessage + ",Affidavit in case of drinking/domestic/green belt (OE areas) for industries other than MSME"; }
                    }
                    if (lblWetlandArea2.Visible)
                    {
                        if (obj_industrialNewApplication.GetWetlandAreaAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Approval from Wetland Authority" : ErrorMessage + ",Approval from Wetland Authority"; }
                    }
                    //if (lblBharatKosh2.Visible)
                    //{
                    //    if (obj_industrialNewApplication.GetBharatKoshRecieptAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Bharat Kosh Reciept" : ErrorMessage + ",Bharat Kosh Reciept"; }
                    //}
                    //if (lblAbstRestCharge2.Visible)
                    //{
                    //    if (obj_industrialNewApplication.GetAbsRestChargeAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Bharatkosh reciept/Copy of Demand Draft (Abstraction Charges)" : ErrorMessage + ",Bharatkosh reciept/Copy of Demand Draft (Abstraction Charges)"; }
                    //}
                    //if (lblRestCharge2.Visible)
                    //{
                    //    if (obj_industrialNewApplication.GetRestChargeAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Bharatkosh reciept/Copy of Demand Draft (Restoration Charges)" : ErrorMessage + ",Bharatkosh reciept/Copy of Demand Draft (Restoration Charges)"; }
                    //}
                    if (lblConsentApproval2.Visible)
                    {
                        if (AttachmentsCountReferalLetter() < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Consent/ Approval of Government Agency" : ErrorMessage + ",Consent/ Approval of Government Agency"; }
                    }
                    if (lblSigneddoc2.Visible)
                    {
                        if (obj_industrialNewApplication.GetSignedDocAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Aplication with Signature and Seal" : ErrorMessage + ",Aplication with Signature and Seal"; }
                    }

                    //non-mandatory
                    //if (lblPenalty2.Visible)
                    //{
                    //    if (obj_industrialNewApplication.GetPenaltyAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Penalty Attachment" : ErrorMessage + ",Penalty Attachment"; }
                    //}
                    //if (lblExtraAttachment2.Visible)
                    //{
                    //    if (obj_industrialNewApplication.GetExtraAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Extra Attachment" : ErrorMessage + ",Extra Attachment"; }
                    //}





                    //if (lblHydrogeologicalReport2.Visible)
                    //{
                    //    if (obj_industrialNewApplication.GetGroundwaterAvailabilityAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Hydrogeological Report" : ErrorMessage + ",Hydrogeological Report"; }
                    //}

                    //if (lblAuthorizationLetter2.Visible)
                    //{
                    //    if (obj_industrialNewApplication.GetUndertakingAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Authorization Letter" : ErrorMessage + ",Authorization Letter"; }
                    //}         


                    if (!string.IsNullOrEmpty(ErrorMessage))
                    {
                        lblMessage.Text = ErrorMessage + " Attachments are Mandatory.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    lngIndSubmitAppCode = lng_submittedApplicationCode;
                    Server.Transfer("INDNewReadyToSubmitKLD.aspx");
                }
                catch (ThreadAbortException)
                {


                }
                catch (Exception EX)
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
                    obj_industrialNewApplication.Dispose();
                }
            }
        }
    }
    #endregion

   
  


}