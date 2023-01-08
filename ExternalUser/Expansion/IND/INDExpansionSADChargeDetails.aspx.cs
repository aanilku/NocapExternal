using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExternalUser_Expansion_IND_INDExpansionSADChargeDetails : System.Web.UI.Page
{

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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblAppCode");
                        if (SourceLabel != null)
                        {
                            lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                        }
                    }

                }

                BindWaterRequirementDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                
            }

            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "function", "CalculateSumTreatedWaterUtilizedInDay();cal1();", true);
                
            }
        }
    }
    private void ValidationExpInit()
    {
        //revtxtGroundWaterRequirementExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        //revtxtGroundWaterRequirementExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        //revtxtGroundWaterRequirementPro.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        //revtxtGroundWaterRequirementPro.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        //revtxtSurfaceWaterRequirementExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        //revtxtSurfaceWaterRequirementExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        //revtxtSurfaceWaterRequirementPro.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        //revtxtSurfaceWaterRequirementPro.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        //revtxtProposedExistingWaterSupplyExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        //revtxtProposedExistingWaterSupplyExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        //revtxtProposedExistingWaterSupplyPro.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        //revtxtProposedExistingWaterSupplyPro.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        //revtxtRecyWaterUsageExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        //revtxtRecyWaterUsageExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        //revtxtRecyWaterUsagePro.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        //revtxtRecyWaterUsagePro.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

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
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(lngA_ApplicationCode);
            //txtGroundWaterRequirementPro.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement));
            //txtSurfaceWaterRequirementPro.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirement));
            //txtProposedExistingWaterSupplyPro.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.ProposedExistingWaterSupplyFromAnyAgency));
            //txtRecyWaterUsagePro.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.RecycleWaterUses));
            //txtGroundWaterRequirementExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));
            //txtSurfaceWaterRequirementExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementExist));
            //txtProposedExistingWaterSupplyExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.ProposedExistingWaterSupplyFromAnyAgencyExist));
            //txtRecyWaterUsageExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist));

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
                    //txtGroundWaterRequirementExist.Enabled = false;
                    //rfvtxtGroundWaterRequirementExist.Enabled = false;
                    //txtSurfaceWaterRequirementExist.Enabled = false;
                    //rfvtxtSurfaceWaterRequirementExist.Enabled = false;
                    //txtProposedExistingWaterSupplyExist.Enabled = false;
                    //rfvtxtProposedExistingWaterSupplyExist.Enabled = false;
                    //txtRecyWaterUsageExist.Enabled = false;
                    //rfvtxtRecyWaterUsageExist.Enabled = false;
                    //txtGroundWaterRequirementExist.Text = "0";
                    //txtSurfaceWaterRequirementExist.Text = "0";
                    //txtProposedExistingWaterSupplyExist.Text = "0";
                    //txtRecyWaterUsageExist.Text = "0";


                    //txtGroundWaterRequirementPro.Enabled = true;
                    //rfvtxtGroundWaterRequirementPro.Enabled = true;
                    //txtSurfaceWaterRequirementPro.Enabled = true;
                    //rfvtxtSurfaceWaterRequirementPro.Enabled = true;
                    //txtProposedExistingWaterSupplyPro.Enabled = true;
                    //rfvtxtProposedExistingWaterSupplyPro.Enabled = true;
                    //txtRecyWaterUsagePro.Enabled = true;
                    //rfvtxtRecyWaterUsagePro.Enabled = true;
                    //ENd First Grid


                    //Start Second Grid
               
                    rfvtxtIndActExistRequirement.Enabled = false;              
                    rfvtxtResidDomExistRequirement.Enabled = false;               
                    rfvtxtGreenDevelEnviMaintExistRequirement.Enabled = false;
                    rfvtxtOtherUseExistRequirement.Enabled = false;

                    txtIndActExistRequirement.Text = "0";
                    txtResidDomExistRequirement.Text = "0";
                    txtGreenDevelEnviMaintExistRequirement.Text = "0";
                    txtOtherUseExistRequirement.Text = "0";

                    
                    rfvtxtIndActProposedRequirement.Enabled = true;              
                    rfvtxtResidDomProposedRequirement.Enabled = true;
                    rfvtxtGreenDevelEnviMaintProposedRequirement.Enabled = true;
                    rfvtxtOtherUseProposedRequirement.Enabled = true;
                    //End Second Grid


                    break;
                case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry:
                    //Start Fist Grid
                    //txtGroundWaterRequirementPro.Enabled = false;
                    //rfvtxtGroundWaterRequirementPro.Enabled = false;
                    //txtSurfaceWaterRequirementPro.Enabled = false;
                    //rfvtxtSurfaceWaterRequirementPro.Enabled = false;
                    //txtProposedExistingWaterSupplyPro.Enabled = false;
                    //rfvtxtProposedExistingWaterSupplyPro.Enabled = false;
                    //txtRecyWaterUsagePro.Enabled = false;
                    //rfvtxtRecyWaterUsagePro.Enabled = false;

                    //txtGroundWaterRequirementPro.Text = "0";
                    //txtSurfaceWaterRequirementPro.Text = "0";
                    //txtProposedExistingWaterSupplyPro.Text = "0";
                    //txtRecyWaterUsagePro.Text = "0";

                    //txtGroundWaterRequirementExist.Enabled = true;
                    //rfvtxtGroundWaterRequirementExist.Enabled = true;
                    //txtSurfaceWaterRequirementExist.Enabled = true;
                    //rfvtxtSurfaceWaterRequirementExist.Enabled = true;
                    //txtProposedExistingWaterSupplyExist.Enabled = true;
                    //rfvtxtProposedExistingWaterSupplyExist.Enabled = true;
                    //txtRecyWaterUsageExist.Enabled = true;
                    //rfvtxtRecyWaterUsageExist.Enabled = true;
                    //ENd First Grid
                    //Start Second Grid
                
                    rfvtxtIndActExistRequirement.Enabled = true;              
                    rfvtxtResidDomExistRequirement.Enabled = true;           
                    rfvtxtGreenDevelEnviMaintExistRequirement.Enabled = true;         
                    rfvtxtOtherUseExistRequirement.Enabled = true;                    
                    rfvtxtIndActProposedRequirement.Enabled = false;                
                    rfvtxtResidDomProposedRequirement.Enabled = false;                 
                    rfvtxtGreenDevelEnviMaintProposedRequirement.Enabled = false;               
                    rfvtxtOtherUseProposedRequirement.Enabled = false;
                    txtIndActProposedRequirement.Text = "0";
                    txtResidDomProposedRequirement.Text = "0";
                    txtGreenDevelEnviMaintProposedRequirement.Text = "0";
                    txtOtherUseProposedRequirement.Text = "0";
                    //End Second Grid

                    break;
                case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.GroundWaterUtilizationForOption.ExpansionProgramOfExitingIndustry:
                    //Start First Grid
                    //txtGroundWaterRequirementPro.Enabled = true;
                    //rfvtxtGroundWaterRequirementPro.Enabled = true;
                    //txtSurfaceWaterRequirementPro.Enabled = true;
                    //rfvtxtSurfaceWaterRequirementPro.Enabled = true;
                    //txtProposedExistingWaterSupplyPro.Enabled = true;
                    //rfvtxtProposedExistingWaterSupplyPro.Enabled = true;
                    //txtRecyWaterUsagePro.Enabled = true;
                    //rfvtxtRecyWaterUsagePro.Enabled = true;

                    //txtGroundWaterRequirementExist.Enabled = true;
                    //rfvtxtGroundWaterRequirementExist.Enabled = true;
                    //txtSurfaceWaterRequirementExist.Enabled = true;
                    //rfvtxtSurfaceWaterRequirementExist.Enabled = true;
                    //txtProposedExistingWaterSupplyExist.Enabled = true;
                    //rfvtxtProposedExistingWaterSupplyExist.Enabled = true;
                    //txtRecyWaterUsageExist.Enabled = true;
                    //rfvtxtRecyWaterUsageExist.Enabled = true;
                    //ENd First Grid
                    //Start Second Grid
               
                    rfvtxtIndActExistRequirement.Enabled = true;              
                    rfvtxtResidDomExistRequirement.Enabled = true;                   
                    rfvtxtGreenDevelEnviMaintExistRequirement.Enabled = true;                 
                    rfvtxtOtherUseExistRequirement.Enabled = true;                    
                    rfvtxtIndActProposedRequirement.Enabled = true;               
                    rfvtxtResidDomProposedRequirement.Enabled = true;                
                    rfvtxtGreenDevelEnviMaintProposedRequirement.Enabled = true;                
                    rfvtxtOtherUseProposedRequirement.Enabled = true;
                    //End Second Grid
                    break;
                default:
                    break;
            }
            txtRate.Text = NOCAPExternalUtility.GetGroundWaterChargeRateForSADAppCode(lngA_ApplicationCode);


            txtTotalwasteWaterGeneratedInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedInDay));
            txtTotalwasteWaterGeneratedNoOfDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedNoOfDay));
            txtTotalwasteWaterGeneratedInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedInYear));

            txtQuantityTreatedWaterAvailable.Text = HttpUtility.HtmlEncode((Convert.ToDecimal(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.RecycleWaterUses) + Convert.ToDecimal(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist)).ToString("0.00"));

            txtQuantityReuseIndustrialActivityInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseIndustrialActivityInDay));
            txtQuantityReuseIndustrialActivityNoOfDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_NoOfOperationdaysInAYear));
            txtQuantityReuseIndustrialActivityInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseIndustrialActivityInYear));
            txtQuantityReuseGreenBeltDevelInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseGreenBeltDevelopmentInDay));
            txtQuantityReuseGreenBeltDevelNoOfDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_NoOfOperationdaysInAYear));
            txtQuantityReuseGreenBeltDevelInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseGreenBeltDevelopmentInYear));
            txtOtherUsesInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInDay));
            txtOtherUsesNoOfDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_NoOfOperationdaysInAYear));
            txtOtherUsesInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInYear));


        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }

}