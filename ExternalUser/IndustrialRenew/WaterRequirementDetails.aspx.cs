using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_IndustrialRenew_WaterRequirementDetails : System.Web.UI.Page
{
    string strPageName = "INDRenewWaterRequirementDetails";
    string strActionName = "";
    string strStatus = "";

    // Note Pro meance Addit in controls id(use in Nameing conventions)

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ValidationExpInit();
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;

            lblMessage.Text = "";
            try
            {
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
                    BindWaterRequirementDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {
                //lblMessage.Text = ex.Message;
                //lblMessage.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
               ScriptManager.RegisterStartupScript(this, GetType(), "function", "SumTWR();cal1();", true);
            }
        }
    }

    private void ValidationExpInit()
    {
        revtxtGroundWaterRequirementExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtGroundWaterRequirementExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtGroundWaterRequirementPro.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtGroundWaterRequirementPro.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtGroundWaterRequirementYear.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtGroundWaterRequirementYear.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");


        revtxtSurfaceWaterRequirementExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtSurfaceWaterRequirementExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtSurfaceWaterRequirementPro.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtSurfaceWaterRequirementPro.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtProposedExistingWaterSupplyExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtProposedExistingWaterSupplyExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtProposedExistingWaterSupplyPro.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtProposedExistingWaterSupplyPro.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtRecyWaterUsageExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtRecyWaterUsageExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtRecyWaterUsagePro.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtRecyWaterUsagePro.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtIndActExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtIndActExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtIndActProposed.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtIndActProposed.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtIndActDaysInYear.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtIndActDaysInYear.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtResidDomExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtResidDomExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtResidDomProposed.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtResidDomProposed.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtResidDomDaysInYear.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtResidDomDaysInYear.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtGreenDevelExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtGreenDevelExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtGreenDevelProposed.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtGreenDevelProposed.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtGreenDevelDaysInYear.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtGreenDevelDaysInYear.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtOtherUseExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtOtherUseExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtOtherUseProposed.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtOtherUseProposed.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtOtherUseDaysInYear.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtOtherUseDaysInYear.ErrorMessage = ValidationUtility.txtValForNumericMsg;
    }

    private void BindWaterRequirementDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);

            if (obj_industrialRenewApplication != null && obj_industrialRenewApplication.IndustrialRenewApplicationCode > 0)
            {
                lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplication.NameOfIndustry);
                txtGroundWaterRequirementYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.GroundWaterReqInYear));

                txtGroundWaterRequirementPro.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional));
                txtSurfaceWaterRequirementPro.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementAdditional));
                txtProposedExistingWaterSupplyPro.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.AdditionalExistingWaterSupplyFromAnyAgencyAdditional));
                txtRecyWaterUsagePro.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.RecycleWaterUsesAdditional));
                txtGroundWaterRequirementExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));
                txtSurfaceWaterRequirementExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementExist));
                txtProposedExistingWaterSupplyExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.AdditionalExistingWaterSupplyFromAnyAgencyExist));
                txtRecyWaterUsageExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist));

                txtIndActExistRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ExistingReq));
                txtIndActProposedRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_AdditionalReq));
                txtIndActNoOfOperationalDaysInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_NoOfOperationdaysInAYear));

                txtResidDomExistRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ExistingReq));
                txtResidDomProposedRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_AdditionalReq));
                txtResidDomNoOfOperationalDaysInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_NoOfOperationdaysInAYear));

                txtGreenDevelEnviMaintExistRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ExistingReq));
                txtGreenDevelEnviMaintProposedRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_AdditionalReq));
                txtGreenDevelEnviMaintNoOfOperationalDaysInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_NoOfOperationdaysInAYear));

                txtOtherUseExistRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ExistingReq));
                txtOtherUseProposedRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_AdditionalReq));
                txtOtherUseNoOfOperationalDaysInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_NoOfOperationdaysInAYear));


                switch (obj_industrialRenewApplication.GroundWaterUtilizationFor)
                {
                    case NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication.GroundWaterUtilizationForOption.ExistingGroundWater:
                        //Start Fist Grid
                        txtGroundWaterRequirementPro.Enabled = false;
                        rfvtxtGroundWaterRequirementPro.Enabled = false;
                        txtSurfaceWaterRequirementPro.Enabled = false;
                        rfvtxtSurfaceWaterRequirementPro.Enabled = false;
                        txtProposedExistingWaterSupplyPro.Enabled = false;
                        rfvtxtProposedExistingWaterSupplyPro.Enabled = false;
                        txtRecyWaterUsagePro.Enabled = false;
                        rfvtxtRecyWaterUsagePro.Enabled = false;
                        

                        txtGroundWaterRequirementPro.Text = "0";
                        txtSurfaceWaterRequirementPro.Text = "0";
                        txtProposedExistingWaterSupplyPro.Text = "0";
                        txtRecyWaterUsagePro.Text = "0";
                        
                        txtGroundWaterRequirementExist.Enabled = true;
                        rfvtxtGroundWaterRequirementExist.Enabled = true;
                        txtSurfaceWaterRequirementExist.Enabled = true;
                        rfvtxtSurfaceWaterRequirementExist.Enabled = true;
                        txtProposedExistingWaterSupplyExist.Enabled = true;
                        rfvtxtProposedExistingWaterSupplyExist.Enabled = true;
                        txtRecyWaterUsageExist.Enabled = true;
                        rfvtxtRecyWaterUsageExist.Enabled = true;
                        //End First Grid
                        //Start Second Grid
                        txtIndActExistRequirement.Enabled = true;
                        rfvtxtIndActExistRequirement.Enabled = true;
                        txtResidDomExistRequirement.Enabled = true;
                        rfvtxtResidDomExistRequirement.Enabled = true;
                        txtGreenDevelEnviMaintExistRequirement.Enabled = true;
                        rfvtxtGreenDevelEnviMaintExistRequirement.Enabled = true;
                        txtOtherUseExistRequirement.Enabled = true;
                        rfvtxtOtherUseExistRequirement.Enabled = true;

                        txtIndActProposedRequirement.Enabled = false;
                        rfvtxtIndActProposedRequirement.Enabled = false;
                        txtResidDomProposedRequirement.Enabled = false;
                        rfvtxtResidDomProposedRequirement.Enabled = false;
                        txtGreenDevelEnviMaintProposedRequirement.Enabled = false;
                        rfvtxtGreenDevelEnviMaintProposedRequirement.Enabled = false;
                        txtOtherUseProposedRequirement.Enabled = false;
                        rfvtxtOtherUseProposedRequirement.Enabled = false;

                        txtIndActProposedRequirement.Text = "0";
                        txtResidDomProposedRequirement.Text = "0";
                        txtGreenDevelEnviMaintProposedRequirement.Text = "0";
                        txtOtherUseProposedRequirement.Text = "0";
                        //End Second Grid

                        break;
                    case NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication.GroundWaterUtilizationForOption.ExistingGWWithAdditionalGWRequirment:
                        //Start First Grid
                        txtGroundWaterRequirementPro.Enabled = true;
                        rfvtxtGroundWaterRequirementPro.Enabled = true;
                        txtSurfaceWaterRequirementPro.Enabled = true;
                        rfvtxtSurfaceWaterRequirementPro.Enabled = true;
                        txtProposedExistingWaterSupplyPro.Enabled = true;
                        rfvtxtProposedExistingWaterSupplyPro.Enabled = true;
                        txtRecyWaterUsagePro.Enabled = true;
                        rfvtxtRecyWaterUsagePro.Enabled = true;

                        txtGroundWaterRequirementExist.Enabled = true;
                        rfvtxtGroundWaterRequirementExist.Enabled = true;
                        txtSurfaceWaterRequirementExist.Enabled = true;
                        rfvtxtSurfaceWaterRequirementExist.Enabled = true;
                        txtProposedExistingWaterSupplyExist.Enabled = true;
                        rfvtxtProposedExistingWaterSupplyExist.Enabled = true;
                        txtRecyWaterUsageExist.Enabled = true;
                        rfvtxtRecyWaterUsageExist.Enabled = true;
                        //ENd First Grid
                        //Start Second Grid
                        txtIndActExistRequirement.Enabled = true;
                        rfvtxtIndActExistRequirement.Enabled = true;
                        txtResidDomExistRequirement.Enabled = true;
                        rfvtxtResidDomExistRequirement.Enabled = true;
                        txtGreenDevelEnviMaintExistRequirement.Enabled = true;
                        rfvtxtGreenDevelEnviMaintExistRequirement.Enabled = true;
                        txtOtherUseExistRequirement.Enabled = true;
                        rfvtxtOtherUseExistRequirement.Enabled = true;

                        txtIndActProposedRequirement.Enabled = true;
                        rfvtxtIndActProposedRequirement.Enabled = true;
                        txtResidDomProposedRequirement.Enabled = true;
                        rfvtxtResidDomProposedRequirement.Enabled = true;
                        txtGreenDevelEnviMaintProposedRequirement.Enabled = true;
                        rfvtxtGreenDevelEnviMaintProposedRequirement.Enabled = true;
                        txtOtherUseProposedRequirement.Enabled = true;
                        rfvtxtOtherUseProposedRequirement.Enabled = true;
                        //End Second Grid
                        break;
                    default:
                        break;
                }
            }
            else
            {
                lblMessage.Text = "Reeor on Page";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

        }
        catch (Exception)
        {            
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    private int UpdateWaterRequirementDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);
            if (txtGroundWaterRequirementYear.Text.Trim() == "")
            {
                obj_industrialRenewApplication.GroundWaterReqInYear = null;
            }
            else
            {
                obj_industrialRenewApplication.GroundWaterReqInYear = Convert.ToDecimal(txtGroundWaterRequirementYear.Text.Trim());
            }
            if (txtGroundWaterRequirementPro.Text.Trim() == "")
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional = null;
            }
            else
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional = Convert.ToDecimal(txtGroundWaterRequirementPro.Text.Trim());
            }
            if (txtSurfaceWaterRequirementPro.Text.Trim() == "")
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementAdditional = null;
            }
            else
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementAdditional = Convert.ToDecimal(txtSurfaceWaterRequirementPro.Text.Trim());
            }
            if (txtProposedExistingWaterSupplyPro.Text.Trim() == "")
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.AdditionalExistingWaterSupplyFromAnyAgencyAdditional = null;
            }
            else
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.AdditionalExistingWaterSupplyFromAnyAgencyAdditional = Convert.ToDecimal(txtProposedExistingWaterSupplyPro.Text.Trim());
            }
            if (txtRecyWaterUsagePro.Text.Trim() == "")
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.RecycleWaterUsesAdditional = null;
            }
            else
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.RecycleWaterUsesAdditional = Convert.ToDecimal(txtRecyWaterUsagePro.Text.Trim());
            }


            if (txtGroundWaterRequirementExist.Text.Trim() == "")
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist = null;
            }
            else
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist = Convert.ToDecimal(txtGroundWaterRequirementExist.Text.Trim());
            }
            if (txtSurfaceWaterRequirementExist.Text.Trim() == "")
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementExist = null;
            }
            else
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementExist = Convert.ToDecimal(txtSurfaceWaterRequirementExist.Text.Trim());
            }
            if (txtProposedExistingWaterSupplyExist.Text.Trim() == "")
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.AdditionalExistingWaterSupplyFromAnyAgencyExist = null;
            }
            else
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.AdditionalExistingWaterSupplyFromAnyAgencyExist = Convert.ToDecimal(txtProposedExistingWaterSupplyExist.Text.Trim());
            }
            if (txtRecyWaterUsageExist.Text.Trim() == "")
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist = null;
            }
            else
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist = Convert.ToDecimal(txtRecyWaterUsageExist.Text.Trim());
            }






            if (txtIndActExistRequirement.Text.Trim() == "")
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ExistingReq = null;
            }
            else
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ExistingReq = Convert.ToDecimal(txtIndActExistRequirement.Text.Trim());
            }


            if (txtIndActProposedRequirement.Text.Trim() == "")
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_AdditionalReq = null;
            }
            else
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_AdditionalReq = Convert.ToDecimal(txtIndActProposedRequirement.Text.Trim());
            }



            if (txtIndActNoOfOperationalDaysInYear.Text.Trim() == "")
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_NoOfOperationdaysInAYear = null;
            }
            else
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_NoOfOperationdaysInAYear = Convert.ToInt32(txtIndActNoOfOperationalDaysInYear.Text.Trim());
            }

            if (txtResidDomExistRequirement.Text.Trim() == "")
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ExistingReq = null;
            }
            else
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ExistingReq = Convert.ToDecimal(txtResidDomExistRequirement.Text.Trim());
            }
            if (txtResidDomProposedRequirement.Text.Trim() == "")
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_AdditionalReq = null;
            }
            else
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_AdditionalReq = Convert.ToDecimal(txtResidDomProposedRequirement.Text.Trim());
            }
            if (txtResidDomNoOfOperationalDaysInYear.Text.Trim() == "")
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_NoOfOperationdaysInAYear = null;
            }
            else
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_NoOfOperationdaysInAYear = Convert.ToInt32(txtResidDomNoOfOperationalDaysInYear.Text.Trim());
            }

            if (txtGreenDevelEnviMaintExistRequirement.Text.Trim() == "")
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ExistingReq = null;
            }
            else
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ExistingReq = Convert.ToDecimal(txtGreenDevelEnviMaintExistRequirement.Text.Trim());
            }
            if (txtGreenDevelEnviMaintProposedRequirement.Text.Trim() == "")
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_AdditionalReq = null;
            }
            else
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_AdditionalReq = Convert.ToDecimal(txtGreenDevelEnviMaintProposedRequirement.Text.Trim());
            }
            if (txtGreenDevelEnviMaintNoOfOperationalDaysInYear.Text.Trim() == "")
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_NoOfOperationdaysInAYear = null;
            }
            else
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_NoOfOperationdaysInAYear = Convert.ToInt32(txtGreenDevelEnviMaintNoOfOperationalDaysInYear.Text.Trim());
            }

            if (txtOtherUseExistRequirement.Text.Trim() == "")
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ExistingReq = null;
            }
            else
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ExistingReq = Convert.ToDecimal(txtOtherUseExistRequirement.Text.Trim());
            }
            if (txtOtherUseProposedRequirement.Text.Trim() == "")
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_AdditionalReq = null;
            }
            else
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_AdditionalReq = Convert.ToDecimal(txtOtherUseProposedRequirement.Text.Trim());
            }
            if (txtOtherUseNoOfOperationalDaysInYear.Text.Trim() == "")
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_NoOfOperationdaysInAYear = null;
            }
            else
            {
                obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_NoOfOperationdaysInAYear = Convert.ToInt32(txtOtherUseNoOfOperationalDaysInYear.Text.Trim());
            }


            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_industrialRenewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_industrialRenewApplication.Update() == 1)
            {
                strActionName = "SaveAsDraft";
                strStatus = "Record Save Successfully !";

                lblMessage.Text = "Successfully Saved";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                strActionName = "SaveAsDraft";
                strStatus = "Record Save Failed !";

                lblMessage.Text = obj_industrialRenewApplication.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }

        }
        catch (Exception)
        {
            strActionName = "SaveAsDraft";
            strStatus = "Record Save Failed !";

            //lblMessage.Text = ex.Message;
            //lblMessage.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
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
            Server.Transfer("~/ExternalUser/IndustrialRenew/ExistingNOCIssued.aspx");
        }
    }
    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {
                try
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "function", "SumTWR();cal1();", true);
                    hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                    Session["CSRF"] = hidCSRF.Value;
                    UpdateWaterRequirementDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
                }
            }
        }


    }
    protected void txtNext_Click(object sender, EventArgs e)
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
                if (UpdateWaterRequirementDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text)) == 1)
                {
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    decimal? dec_netGroundWaterRequirement = (obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist) + (obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional == null ? 0 : obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional);
                    
                    if (obj_industrialRenewApplication.MSME == NOCAP.BLL.Industrial.Renew.Common.IndustrialRenewApplicationB.MSMEYesNo.Yes && dec_netGroundWaterRequirement < 10 && obj_industrialRenewApplication.MSMETypeCode != 3)
                    {

                        Server.Transfer("~/ExternalUser/IndustrialRenew/SalientFeature.aspx");

                    }
                    else
                    {
                        Server.Transfer("~/ExternalUser/IndustrialRenew/RecycledWaterUsage.aspx");
                    }
                        
                }
            }
        }
    }


}