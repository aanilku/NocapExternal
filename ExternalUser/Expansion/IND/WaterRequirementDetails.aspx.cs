using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_Expansion_IND_WaterRequirementDetails : System.Web.UI.Page
{
    string strPageName = "WaterRequirementDetails";
    string strActionName = "";
    string strStatus = "";

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
                if (lblIndustialApplicationCodeFrom.Text.Trim() != "")
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
            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(lngA_ApplicationCode);
            txtGroundWaterRequirementPro.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement));
            txtSurfaceWaterRequirementPro.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirement));
            txtProposedExistingWaterSupplyPro.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.ProposedExistingWaterSupplyFromAnyAgency));
            txtRecyWaterUsagePro.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.RecycleWaterUses));
            txtGroundWaterRequirementExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));
            txtGroundWaterRequirementYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.GroundWaterReqInYear));

            txtSurfaceWaterRequirementExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementExist));
            txtProposedExistingWaterSupplyExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.ProposedExistingWaterSupplyFromAnyAgencyExist));
            txtRecyWaterUsageExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist));

            txtIndActExistRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ExistingReq));
            txtIndActProposedRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ProposedReq));
            txtIndActNoOfOperationalDaysInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_NoOfOperationdaysInAYear));

            txtResidDomExistRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ExistingReq));
            txtResidDomProposedRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ProposedReq));
            txtResidDomNoOfOperationalDaysInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_NoOfOperationdaysInAYear));

            txtGreenDevelEnviMaintExistRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ExistingReq));
            txtGreenDevelEnviMaintProposedRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ProposedReq));
            txtGreenDevelEnviMaintNoOfOperationalDaysInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_NoOfOperationdaysInAYear));

            txtOtherUseExistRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ExistingReq));
            txtOtherUseProposedRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ProposedReq));
            txtOtherUseNoOfOperationalDaysInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_NoOfOperationdaysInAYear));


            switch (obj_industrialNewApplication.GroundWaterUtilizationFor)
            {
                case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.GroundWaterUtilizationForOption.NewIndustry:
                    // Start First Grid
                    txtGroundWaterRequirementExist.Enabled = false;
                    rfvtxtGroundWaterRequirementExist.Enabled = false;
                    txtSurfaceWaterRequirementExist.Enabled = false;
                    rfvtxtSurfaceWaterRequirementExist.Enabled = false;
                    txtProposedExistingWaterSupplyExist.Enabled = false;
                    rfvtxtProposedExistingWaterSupplyExist.Enabled = false;
                    txtRecyWaterUsageExist.Enabled = false;
                    rfvtxtRecyWaterUsageExist.Enabled = false;
                    txtGroundWaterRequirementExist.Text = "0";
                    
                    txtSurfaceWaterRequirementExist.Text = "0";
                    txtProposedExistingWaterSupplyExist.Text = "0";
                    txtRecyWaterUsageExist.Text = "0";


                    txtGroundWaterRequirementPro.Enabled = true;
                    rfvtxtGroundWaterRequirementPro.Enabled = true;
                    txtSurfaceWaterRequirementPro.Enabled = true;
                    rfvtxtSurfaceWaterRequirementPro.Enabled = true;
                    txtProposedExistingWaterSupplyPro.Enabled = true;
                    rfvtxtProposedExistingWaterSupplyPro.Enabled = true;
                    txtRecyWaterUsagePro.Enabled = true;
                    rfvtxtRecyWaterUsagePro.Enabled = true;
                    //ENd First Grid


                    //Start Second Grid
                    txtIndActExistRequirement.Enabled = false;
                    rfvtxtIndActExistRequirement.Enabled = false;
                    txtResidDomExistRequirement.Enabled = false;
                    rfvtxtResidDomExistRequirement.Enabled = false;
                    txtGreenDevelEnviMaintExistRequirement.Enabled = false;
                    rfvtxtGreenDevelEnviMaintExistRequirement.Enabled = false;
                    txtOtherUseExistRequirement.Enabled = false;
                    rfvtxtOtherUseExistRequirement.Enabled = false;

                    txtIndActExistRequirement.Text = "0";
                    txtResidDomExistRequirement.Text = "0";
                    txtGreenDevelEnviMaintExistRequirement.Text = "0";
                    txtOtherUseExistRequirement.Text = "0";


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
                case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry:
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
                case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.GroundWaterUtilizationForOption.ExpansionProgramOfExitingIndustry:
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
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
            //lblMessage.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    private int UpdateWaterRequirementDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(lngA_ApplicationCode);

            if (txtGroundWaterRequirementPro.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement = null;
            }
            else
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement = Convert.ToDecimal(txtGroundWaterRequirementPro.Text.Trim());
            }
            if (txtSurfaceWaterRequirementPro.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirement = null;
            }
            else
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirement = Convert.ToDecimal(txtSurfaceWaterRequirementPro.Text.Trim());
            }
            if (txtProposedExistingWaterSupplyPro.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.ProposedExistingWaterSupplyFromAnyAgency = null;
            }
            else
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.ProposedExistingWaterSupplyFromAnyAgency = Convert.ToDecimal(txtProposedExistingWaterSupplyPro.Text.Trim());
            }
            if (txtRecyWaterUsagePro.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.RecycleWaterUses = null;
            }
            else
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.RecycleWaterUses = Convert.ToDecimal(txtRecyWaterUsagePro.Text.Trim());
            }


            if (txtGroundWaterRequirementExist.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist = null;
            }
            else
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist = Convert.ToDecimal(txtGroundWaterRequirementExist.Text.Trim());
            }
            if (txtGroundWaterRequirementYear.Text.Trim() == "")
            {
                obj_industrialNewApplication.GroundWaterReqInYear = null;
            }
            else
            {
                obj_industrialNewApplication.GroundWaterReqInYear = Convert.ToDecimal(txtGroundWaterRequirementYear.Text.Trim());
            }
           
            if (txtSurfaceWaterRequirementExist.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementExist = null;
            }
            else
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementExist = Convert.ToDecimal(txtSurfaceWaterRequirementExist.Text.Trim());
            }
            if (txtProposedExistingWaterSupplyExist.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.ProposedExistingWaterSupplyFromAnyAgencyExist = null;
            }
            else
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.ProposedExistingWaterSupplyFromAnyAgencyExist = Convert.ToDecimal(txtProposedExistingWaterSupplyExist.Text.Trim());
            }
            if (txtRecyWaterUsageExist.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist = null;
            }
            else
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist = Convert.ToDecimal(txtRecyWaterUsageExist.Text.Trim());
            }
            if (txtIndActExistRequirement.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ExistingReq = null;
            }
            else
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ExistingReq = Convert.ToDecimal(txtIndActExistRequirement.Text.Trim());
            }
            if (txtIndActProposedRequirement.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ProposedReq = null;
            }
            else
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ProposedReq = Convert.ToDecimal(txtIndActProposedRequirement.Text.Trim());
            }
            if (txtIndActNoOfOperationalDaysInYear.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_NoOfOperationdaysInAYear = null;
            }
            else
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_NoOfOperationdaysInAYear = Convert.ToInt32(txtIndActNoOfOperationalDaysInYear.Text.Trim());
            }

            if (txtResidDomExistRequirement.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ExistingReq = null;
            }
            else
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ExistingReq = Convert.ToDecimal(txtResidDomExistRequirement.Text.Trim());
            }
            if (txtResidDomProposedRequirement.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ProposedReq = null;
            }
            else
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ProposedReq = Convert.ToDecimal(txtResidDomProposedRequirement.Text.Trim());
            }
            if (txtResidDomNoOfOperationalDaysInYear.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_NoOfOperationdaysInAYear = null;
            }
            else
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_NoOfOperationdaysInAYear = Convert.ToInt32(txtResidDomNoOfOperationalDaysInYear.Text.Trim());
            }

            if (txtGreenDevelEnviMaintExistRequirement.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ExistingReq = null;
            }
            else
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ExistingReq = Convert.ToDecimal(txtGreenDevelEnviMaintExistRequirement.Text.Trim());
            }
            if (txtGreenDevelEnviMaintProposedRequirement.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ProposedReq = null;
            }
            else
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ProposedReq = Convert.ToDecimal(txtGreenDevelEnviMaintProposedRequirement.Text.Trim());
            }
            if (txtGreenDevelEnviMaintNoOfOperationalDaysInYear.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_NoOfOperationdaysInAYear = null;
            }
            else
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_NoOfOperationdaysInAYear = Convert.ToInt32(txtGreenDevelEnviMaintNoOfOperationalDaysInYear.Text.Trim());
            }

            if (txtOtherUseExistRequirement.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ExistingReq = null;
            }
            else
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ExistingReq = Convert.ToDecimal(txtOtherUseExistRequirement.Text.Trim());
            }
            if (txtOtherUseProposedRequirement.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ProposedReq = null;
            }
            else
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ProposedReq = Convert.ToDecimal(txtOtherUseProposedRequirement.Text.Trim());
            }
            if (txtOtherUseNoOfOperationalDaysInYear.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_NoOfOperationdaysInAYear = null;
            }
            else
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_NoOfOperationdaysInAYear = Convert.ToInt32(txtOtherUseNoOfOperationalDaysInYear.Text.Trim());
            }

           





            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_industrialNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;



            if (obj_industrialNewApplication.Update() == 1)
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

                lblMessage.Text = obj_industrialNewApplication.CustumMessage;
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
           
            Server.Transfer("~/ExternalUser/Expansion/IND/LandUseDetails.aspx");
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
        string str_RedirectPath = "";
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
                NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));

                int Status = 1;

                decimal IndActTotal = 0, OtherActTotal = 0;
                if (txtIndActExistRequirement.Text.Trim() != "")
                    IndActTotal = IndActTotal + Convert.ToDecimal(txtIndActExistRequirement.Text.Trim());
                if (txtIndActProposedRequirement.Text.Trim() != "")
                    IndActTotal = IndActTotal + Convert.ToDecimal(txtIndActProposedRequirement.Text.Trim());

                if (txtOtherUseExistRequirement.Text.Trim() != "")
                    OtherActTotal = OtherActTotal + Convert.ToDecimal(txtOtherUseExistRequirement.Text.Trim());
                if (txtOtherUseProposedRequirement.Text.Trim() != "")
                    OtherActTotal = OtherActTotal + Convert.ToDecimal(txtOtherUseProposedRequirement.Text.Trim());

                CheckMSMEWithAreaTypeCat(obj_industrialNewApplication, IndActTotal, OtherActTotal, out Status);
                if (Status == 1)
                {
                    if (UpdateWaterRequirementDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text)) == 1)
                    {

                        str_RedirectPath = "~/ExternalUser/Expansion/IND/RecycledWaterUsage.aspx";
                        Server.Transfer(str_RedirectPath);
                    }
                }

            }


        }
    }

    private void CheckMSMEWithAreaTypeCat(NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication objA_IndustrialNewSADApplication, decimal dec_IndustrialUse, decimal dec_OtherUse, out int Status)
    {
        Status = 1;
        NOCAP.BLL.Master.SubDistrictAreaTypeCategory obj_SubDistrictAreaTypeCategory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategory(Convert.ToInt32(objA_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode), objA_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, objA_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(objA_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, objA_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, objA_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_SubDistrictAreaTypeCategory.SubDistrictCurrentAreaCategoryKey);

        if (objA_IndustrialNewSADApplication.WaterQualityCode.ToString() == "1")
        {
            switch (objA_IndustrialNewSADApplication.GroundWaterUtilizationFor)
            {
                case NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication.GroundWaterUtilizationForOption.NewIndustry:
                    if (obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode == 5)//Over-Exploited
                    {
                        switch (objA_IndustrialNewSADApplication.MSME)
                        {
                            case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.MSMEYesNo.No:
                                if (dec_IndustrialUse > 0 || dec_OtherUse > 0)
                                //if((Convert.ToInt32(txtIndActExistRequirement.Text.Trim())+ Convert.ToInt32(txtIndActProposedRequirement.Text.Trim())) >0 ||(Convert.ToInt32(txtOtherUseExistRequirement.Text.Trim()) + Convert.ToInt32(txtOtherUseProposedRequirement.Text.Trim())>0))
                                {
                                    lblMessage.Text = "Industrial Activity or Other Use - NOC for groundwater extraction shall not be granted in Over Exploited Area.";
                                    lblMessage.ForeColor = System.Drawing.Color.Red;
                                    Status = 0;
                                }
                                else
                                    Status = 1;
                                break;
                        }

                    }
                    else
                        Status = 1;
                    break;
            }
        }
        else
            Status = 1;


    }


}