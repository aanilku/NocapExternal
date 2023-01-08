using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;


public partial class ExternalUser_InfrastructureRenew_WaterRequirementDetails : System.Web.UI.Page
{
    string strPageName = "INFRenewWaterRequirementDetails";
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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblInfrastructureApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                        }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    BindWaterRequirementDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "function", "SumTWR();cal1();", true);
            }
        }
    }

    #region Private Method
    private void ValidationExpInit()
    {
        revtxtGroundWaterRequirementExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtGroundWaterRequirementExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtGroundWaterRequirementYear.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtGroundWaterRequirementYear.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        revtxtGroundWaterRequirementAddit.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtGroundWaterRequirementAddit.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtSurfaceWaterRequirementExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtSurfaceWaterRequirementExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtSurfaceWaterRequirementAddit.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtSurfaceWaterRequirementAddit.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtAdditionalExistingWaterSupplyExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtAdditionalExistingWaterSupplyExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtAdditionalExistingWaterSupplyAddit.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtAdditionalExistingWaterSupplyAddit.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtRecyWaterUsageExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtRecyWaterUsageExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtRecyWaterUsageAddit.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtRecyWaterUsageAddit.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtCommeUseExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtCommeUseExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtCommeUseAddit.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtCommeUseAddit.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtCommeUseNoOfDaysInYear.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtCommeUseNoOfDaysInYear.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtResidUseExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtResidUseExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtResidUseAddit.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtResidUseAddit.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtResidUseNoOfDaysInYear.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtResidUseNoOfDaysInYear.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtGreenDevelExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtGreenDevelExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtGreenDevelAdditional.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtGreenDevelAdditional.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtGreenDevelDaysInYear.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtGreenDevelDaysInYear.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtFlushReqExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtFlushReqExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtFlushReqAddit.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtFlushReqAddit.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtFlushReqNoOfDaysInYear.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtFlushReqNoOfDaysInYear.ErrorMessage = ValidationUtility.txtValForNumericMsg;
    }
    private void BindWaterRequirementDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);

            if (obj_infrastructureRenewSADApplication != null && obj_infrastructureRenewSADApplication.InfrastructureRenewApplicationCode > 0)
            {
                lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewSADApplication.NameOfInfrastructure);

                txtGroundWaterRequirementAddit.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional));
                txtSurfaceWaterRequirementAddit.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementAdditional));
                txtAdditionalExistingWaterSupplyAddit.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.AdditionalExistingWaterSupplyFromAnyAgencyAdditional));
                txtRecyWaterUsageAddit.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.RecycleWaterUsesAdditional));
                txtGroundWaterRequirementExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));
                txtGroundWaterRequirementYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.GroundWaterReqInYear));

                txtSurfaceWaterRequirementExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementExist));
                txtAdditionalExistingWaterSupplyExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.AdditionalExistingWaterSupplyFromAnyAgencyExist));
                txtRecyWaterUsageExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist));

                txtCommeUseExistRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_ExistingReq));
                txtCommeUseAdditRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_AdditionalReq));
                txtCommeUseNoOfOperationalDaysInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_NoOfOperationdaysInAYear));

                txtResidUseExistRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ExistingReq));
                txtResidUseAdditRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_AdditionalReq));
                txtResidUseNoOfOperationalDaysInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_NoOfOperationdaysInAYear));

                txtGreenDevelEnviMaintExistRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ExistingReq));
                txtGreenDevelEnviMaintAdditionalRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_AdditionalReq));
                txtGreenDevelEnviMaintNoOfOperationalDaysInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_NoOfOperationdaysInAYear));

                txtFlushReqExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.FlushingReq_ExistingReq));
                txtFlushReqAddit.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.FlushingReq_AdditionalReq));
                txtFlushReqNoOfOperationalDaysInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.FlushingReq_NoOfOperationdaysInAYear));


                switch (obj_infrastructureRenewSADApplication.GroundWaterUtilizationFor)
                {

                    case NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.GroundWaterUtilizationForOption.ExistingGroundWater:
                        //Start First Grid
                        txtGroundWaterRequirementAddit.Enabled = false;
                        rfvtxtGroundWaterRequirementAddit.Enabled = false;
                        txtSurfaceWaterRequirementAddit.Enabled = false;
                        rfvtxtSurfaceWaterRequirementAddit.Enabled = false;
                        txtAdditionalExistingWaterSupplyAddit.Enabled = false;
                        rfvtxtAdditionalExistingWaterSupplyAddit.Enabled = false;
                        txtRecyWaterUsageAddit.Enabled = false;
                        rfvtxtRecyWaterUsageAddit.Enabled = false;

                        txtGroundWaterRequirementAddit.Text = "0";
                        txtSurfaceWaterRequirementAddit.Text = "0";
                        txtAdditionalExistingWaterSupplyAddit.Text = "0";
                        txtRecyWaterUsageAddit.Text = "0";

                        txtGroundWaterRequirementExist.Enabled = true;
                        rfvtxtGroundWaterRequirementExist.Enabled = true;
                        txtSurfaceWaterRequirementExist.Enabled = true;
                        rfvtxtSurfaceWaterRequirementExist.Enabled = true;
                        txtAdditionalExistingWaterSupplyExist.Enabled = true;
                        rfvtxtAdditionalExistingWaterSupplyExist.Enabled = true;
                        txtRecyWaterUsageExist.Enabled = true;
                        rfvtxtRecyWaterUsageExist.Enabled = true;
                        //End First Grid

                        //Start Second Grid

                        txtCommeUseExistRequirement.Enabled = true;
                        rfvtxtCommeUseExistRequirement.Enabled = true;
                        txtResidUseExistRequirement.Enabled = true;
                        rfvtxtResidUseExistRequirement.Enabled = true;
                        txtGreenDevelEnviMaintExistRequirement.Enabled = true;
                        rfvtxtGreenDevelEnviMaintExistRequirement.Enabled = true;
                        txtFlushReqExist.Enabled = true;
                        rfvtxtFlushReqExist.Enabled = true;

                        txtCommeUseAdditRequirement.Enabled = false;
                        rfvtxtCommeUseAdditRequirement.Enabled = false;
                        txtResidUseAdditRequirement.Enabled = false;
                        rfvtxtResidUseAdditRequirement.Enabled = false;
                        txtGreenDevelEnviMaintAdditionalRequirement.Enabled = false;
                        rfvtxtGreenDevelEnviMaintAdditionalRequirement.Enabled = false;
                        txtFlushReqAddit.Enabled = false;
                        rfvtxtFlushReqAddit.Enabled = false;

                        txtCommeUseAdditRequirement.Text = "0";
                        txtResidUseAdditRequirement.Text = "0";
                        txtGreenDevelEnviMaintAdditionalRequirement.Text = "0";
                        txtFlushReqAddit.Text = "0";
                        //End Second Grid

                        break;
                    case NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.GroundWaterUtilizationForOption.ExistingGWWithAdditionalGWRequirment:
                        //Start First Grid

                        txtGroundWaterRequirementAddit.Enabled = true;
                        rfvtxtGroundWaterRequirementAddit.Enabled = true;
                        txtSurfaceWaterRequirementAddit.Enabled = true;
                        rfvtxtSurfaceWaterRequirementAddit.Enabled = true;
                        txtAdditionalExistingWaterSupplyAddit.Enabled = true;
                        rfvtxtAdditionalExistingWaterSupplyAddit.Enabled = true;
                        txtRecyWaterUsageAddit.Enabled = true;
                        rfvtxtRecyWaterUsageAddit.Enabled = true;


                        txtGroundWaterRequirementExist.Enabled = true;
                        rfvtxtGroundWaterRequirementExist.Enabled = true;
                        txtSurfaceWaterRequirementExist.Enabled = true;
                        rfvtxtSurfaceWaterRequirementExist.Enabled = true;
                        txtAdditionalExistingWaterSupplyExist.Enabled = true;
                        rfvtxtAdditionalExistingWaterSupplyExist.Enabled = true;
                        txtRecyWaterUsageExist.Enabled = true;
                        rfvtxtRecyWaterUsageExist.Enabled = true;
                        //End First Grid

                        //Start Second Grid

                        txtCommeUseExistRequirement.Enabled = true;
                        rfvtxtCommeUseExistRequirement.Enabled = true;
                        txtResidUseExistRequirement.Enabled = true;
                        rfvtxtResidUseExistRequirement.Enabled = true;
                        txtGreenDevelEnviMaintExistRequirement.Enabled = true;
                        rfvtxtGreenDevelEnviMaintExistRequirement.Enabled = true;
                        txtFlushReqExist.Enabled = true;
                        rfvtxtFlushReqExist.Enabled = true;

                        txtCommeUseAdditRequirement.Enabled = true;
                        rfvtxtCommeUseAdditRequirement.Enabled = true;
                        txtResidUseAdditRequirement.Enabled = true;
                        rfvtxtResidUseAdditRequirement.Enabled = true;
                        txtGreenDevelEnviMaintAdditionalRequirement.Enabled = true;
                        rfvtxtGreenDevelEnviMaintAdditionalRequirement.Enabled = true;
                        txtFlushReqAddit.Enabled = true;
                        rfvtxtFlushReqAddit.Enabled = true;

                        //End Second Grid
                        break;
                    default:
                        break;
                }
            }
            else
            {
                lblMessage.Text = "Error on Page";
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
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);

            if (txtGroundWaterRequirementAddit.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional = Convert.ToDecimal(txtGroundWaterRequirementAddit.Text.Trim());
            }
            if (txtSurfaceWaterRequirementAddit.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementAdditional = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementAdditional = Convert.ToDecimal(txtSurfaceWaterRequirementAddit.Text.Trim());
            }
            if (txtAdditionalExistingWaterSupplyAddit.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.AdditionalExistingWaterSupplyFromAnyAgencyAdditional = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.AdditionalExistingWaterSupplyFromAnyAgencyAdditional = Convert.ToDecimal(txtAdditionalExistingWaterSupplyAddit.Text.Trim());
            }
            if (txtRecyWaterUsageAddit.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.RecycleWaterUsesAdditional = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.RecycleWaterUsesAdditional = Convert.ToDecimal(txtRecyWaterUsageAddit.Text.Trim());
            }
            if (txtGroundWaterRequirementExist.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist = Convert.ToDecimal(txtGroundWaterRequirementExist.Text.Trim());
            }
            if (txtGroundWaterRequirementYear.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.GroundWaterReqInYear = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.GroundWaterReqInYear = Convert.ToDecimal(txtGroundWaterRequirementYear.Text.Trim());
            }
            if (txtSurfaceWaterRequirementExist.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementExist = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementExist = Convert.ToDecimal(txtSurfaceWaterRequirementExist.Text.Trim());
            }
            if (txtAdditionalExistingWaterSupplyExist.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.AdditionalExistingWaterSupplyFromAnyAgencyExist = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.AdditionalExistingWaterSupplyFromAnyAgencyExist = Convert.ToDecimal(txtAdditionalExistingWaterSupplyExist.Text.Trim());
            }
            if (txtRecyWaterUsageExist.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist = Convert.ToDecimal(txtRecyWaterUsageExist.Text.Trim());
            }


            if (txtCommeUseExistRequirement.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_ExistingReq = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_ExistingReq = Convert.ToDecimal(txtCommeUseExistRequirement.Text.Trim());
            }

            if (txtCommeUseAdditRequirement.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_AdditionalReq = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_AdditionalReq = Convert.ToDecimal(txtCommeUseAdditRequirement.Text.Trim());
            }

            if (txtCommeUseNoOfOperationalDaysInYear.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_NoOfOperationdaysInAYear = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_NoOfOperationdaysInAYear = Convert.ToInt32(txtCommeUseNoOfOperationalDaysInYear.Text.Trim());
            }

            if (txtResidUseExistRequirement.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ExistingReq = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ExistingReq = Convert.ToDecimal(txtResidUseExistRequirement.Text.Trim());
            }
            if (txtResidUseAdditRequirement.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_AdditionalReq = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_AdditionalReq = Convert.ToDecimal(txtResidUseAdditRequirement.Text.Trim());
            }
            if (txtResidUseNoOfOperationalDaysInYear.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_NoOfOperationdaysInAYear = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_NoOfOperationdaysInAYear = Convert.ToInt32(txtResidUseNoOfOperationalDaysInYear.Text.Trim());
            }

            if (txtGreenDevelEnviMaintExistRequirement.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ExistingReq = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ExistingReq = Convert.ToDecimal(txtGreenDevelEnviMaintExistRequirement.Text.Trim());
            }
            if (txtGreenDevelEnviMaintAdditionalRequirement.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_AdditionalReq = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_AdditionalReq = Convert.ToDecimal(txtGreenDevelEnviMaintAdditionalRequirement.Text.Trim());
            }
            if (txtGreenDevelEnviMaintNoOfOperationalDaysInYear.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_NoOfOperationdaysInAYear = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_NoOfOperationdaysInAYear = Convert.ToInt32(txtGreenDevelEnviMaintNoOfOperationalDaysInYear.Text.Trim());
            }

            if (txtFlushReqExist.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.FlushingReq_ExistingReq = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.FlushingReq_ExistingReq = Convert.ToDecimal(txtFlushReqExist.Text.Trim());
            }
            if (txtFlushReqAddit.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.FlushingReq_AdditionalReq = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.FlushingReq_AdditionalReq = Convert.ToDecimal(txtFlushReqAddit.Text.Trim());
            }
            if (txtFlushReqNoOfOperationalDaysInYear.Text.Trim() == "")
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.FlushingReq_NoOfOperationdaysInAYear = null;
            }
            else
            {
                obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.FlushingReq_NoOfOperationdaysInAYear = Convert.ToInt32(txtFlushReqNoOfOperationalDaysInYear.Text.Trim());
            }


            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_infrastructureRenewSADApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_infrastructureRenewSADApplication.Update() == 1)
            {
                strActionName = "SaveAsDraft";
                strStatus = "Record Saved Successfully !";

                lblMessage.Text = "Successfully Saved";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                strActionName = "SaveAsDraft";
                strStatus = "Record Saving Failed !";
                lblMessage.Text = obj_infrastructureRenewSADApplication.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
        }
        catch (Exception)
        {
            strActionName = "SaveAsDraft";
            strStatus = "Record Save Failed !";
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
    #endregion
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
            Server.Transfer("~/ExternalUser/InfrastructureRenew/ExistingNOCIssued.aspx");
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
                    UpdateWaterRequirementDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
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
                try
                {
                    hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                    Session["CSRF"] = hidCSRF.Value;
                    if (UpdateWaterRequirementDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text)) == 1)
                    {

                        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_infrastructureRenewApplication.FirstApplicationCode);
                        decimal dec_netGroundWaterRequirement = Convert.ToDecimal(txtGroundWaterRequirementExist.Text == "" ? "0" : txtGroundWaterRequirementExist.Text) + Convert.ToDecimal(txtGroundWaterRequirementAddit.Text == "" ? "0" : txtGroundWaterRequirementAddit.Text);
                        if ((new NOCAP.BLL.Master.ApplicationTypeCategory(obj_infrastructureNewApplication.ApplicationTypeCode, obj_infrastructureNewApplication.ApplicationTypeCategoryCode).ExemptionAllow==NOCAP.BLL.Master.ApplicationTypeCategory.ExemptionAllowYesNo.Yes) && obj_infrastructureRenewApplication.MSME == NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.MSMEYesNo.Yes && dec_netGroundWaterRequirement < 10 && obj_infrastructureRenewApplication.MSMETypeCode != 3)
                            Server.Transfer("SalientFeature.aspx");                        
                        else                        
                            Server.Transfer("RecycledWaterUsage.aspx");                        
                    }
                }
                catch (System.Threading.ThreadAbortException ex)
                {

                }
            }
        }
    }


}