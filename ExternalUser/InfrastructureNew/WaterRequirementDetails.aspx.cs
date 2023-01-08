using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_IndustrialNew_WaterRequirementDetails : System.Web.UI.Page
{
    string strPageName = "INFWaterRequirement";
    string strActionName = "";
    string strStatus = "";
    string category = "";
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
                if (lblModeFrom.Text.Trim() == "Edit") { BindWaterRequirementDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text)); }

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally { }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "function", "SumTWR()", true);
    }

    private void ValidationExpInit()
    {
        revtxtDwellingUnits.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtDwellingUnits.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revtxtDwellingPopulation.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtDwellingPopulation.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revtxtCommercialUnits.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtCommercialUnits.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revtxtCommercialPopulation.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtCommercialPopulation.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revtxtIndustrialUnits.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtIndustrialUnits.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revtxtOtherUnits.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtOtherUnits.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revtxtPopulation.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtPopulation.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revtxtGroundWaterRequirementExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtGroundWaterRequirementExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtGroundWaterRequirementYear.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtGroundWaterRequirementYear.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        revtxtGroundWaterRequirementPro.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtGroundWaterRequirementPro.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

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
    }


    private void BindWaterRequirementDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            txtGroundWaterRequirementPro.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement));
            txtSurfaceWaterRequirementPro.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirement));
            txtProposedExistingWaterSupplyPro.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.ProposedExistingWaterSupplyFromAnyAgency));
            txtRecyWaterUsagePro.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.RecycleWaterUses));
            txtGroundWaterRequirementExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));
            txtGroundWaterRequirementYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.GroundWaterReqInYear));

            txtSurfaceWaterRequirementExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementExist));
            txtProposedExistingWaterSupplyExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.ProposedExistingWaterSupplyFromAnyAgencyExist));
            txtRecyWaterUsageExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist));
            switch (obj_infrastructureNewApplication.GroundWaterUtilizationFor)
            {
                case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.GroundWaterUtilizationForOption.NewProject:
                    rfvtxtGroundWaterRequirementExist.Enabled = false;
                    rfvtxtSurfaceWaterRequirementExist.Enabled = false;
                    rfvtxtProposedExistingWaterSupplyExist.Enabled = false;
                    rfvtxtRecyWaterUsageExist.Enabled = false;
                    txtGroundWaterRequirementExist.Enabled = false;
                    txtSurfaceWaterRequirementExist.Enabled = false;
                    txtProposedExistingWaterSupplyExist.Enabled = false;
                    txtRecyWaterUsageExist.Enabled = false;
                    txtGroundWaterRequirementExist.Text = "0";
                    txtSurfaceWaterRequirementExist.Text = "0";
                    txtProposedExistingWaterSupplyExist.Text = "0";
                    txtRecyWaterUsageExist.Text = "0";

                    rfvtxtGroundWaterRequirementPro.Enabled = true;
                    rfvtxtSurfaceWaterRequirementPro.Enabled = true;
                    rfvtxtProposedExistingWaterSupplyPro.Enabled = true;
                    rfvtxtRecyWaterUsagePro.Enabled = true;
                    txtGroundWaterRequirementPro.Enabled = true;
                    txtSurfaceWaterRequirementPro.Enabled = true;
                    rfvtxtProposedExistingWaterSupplyPro.Enabled = true;
                    txtRecyWaterUsagePro.Enabled = true;
                    break;
                case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry:
                    rfvtxtGroundWaterRequirementExist.Enabled = true;
                    rfvtxtSurfaceWaterRequirementExist.Enabled = true;
                    rfvtxtProposedExistingWaterSupplyExist.Enabled = true;
                    rfvtxtRecyWaterUsageExist.Enabled = true;
                    txtGroundWaterRequirementExist.Enabled = true;
                    txtSurfaceWaterRequirementExist.Enabled = true;
                    txtProposedExistingWaterSupplyExist.Enabled = true;
                    txtRecyWaterUsageExist.Enabled = true;

                    rfvtxtGroundWaterRequirementPro.Enabled = false;
                    rfvtxtSurfaceWaterRequirementPro.Enabled = false;
                    rfvtxtProposedExistingWaterSupplyPro.Enabled = false;
                    rfvtxtRecyWaterUsagePro.Enabled = false;
                    txtGroundWaterRequirementPro.Enabled = false;
                    txtSurfaceWaterRequirementPro.Enabled = false;
                    txtProposedExistingWaterSupplyPro.Enabled = false;
                    txtRecyWaterUsagePro.Enabled = false;
                    txtGroundWaterRequirementPro.Text = "0";
                    txtSurfaceWaterRequirementPro.Text = "0";
                    txtProposedExistingWaterSupplyPro.Text = "0";
                    txtRecyWaterUsagePro.Text = "0";
                    break;
                case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.GroundWaterUtilizationForOption.ExpansionProgramme:
                    rfvtxtGroundWaterRequirementExist.Enabled = true;
                    rfvtxtSurfaceWaterRequirementExist.Enabled = true;
                    rfvtxtProposedExistingWaterSupplyExist.Enabled = true;
                    rfvtxtRecyWaterUsageExist.Enabled = true;
                    txtGroundWaterRequirementExist.Enabled = true;
                    txtSurfaceWaterRequirementExist.Enabled = true;
                    txtProposedExistingWaterSupplyExist.Enabled = true;
                    txtRecyWaterUsageExist.Enabled = true;

                    rfvtxtGroundWaterRequirementPro.Enabled = true;
                    rfvtxtSurfaceWaterRequirementPro.Enabled = true;
                    rfvtxtProposedExistingWaterSupplyPro.Enabled = true;
                    rfvtxtRecyWaterUsagePro.Enabled = true;
                    txtGroundWaterRequirementPro.Enabled = true;
                    txtSurfaceWaterRequirementPro.Enabled = true;
                    rfvtxtProposedExistingWaterSupplyPro.Enabled = true;
                    txtRecyWaterUsagePro.Enabled = true;

                    rfvtxtGroundWaterRequirementExist.Enabled = true;
                    rfvtxtSurfaceWaterRequirementExist.Enabled = true;
                    rfvtxtProposedExistingWaterSupplyExist.Enabled = true;
                    rfvtxtRecyWaterUsageExist.Enabled = true;
                    txtGroundWaterRequirementExist.Enabled = true;
                    txtSurfaceWaterRequirementExist.Enabled = true;
                    txtProposedExistingWaterSupplyExist.Enabled = true;
                    txtRecyWaterUsageExist.Enabled = true;

                    rfvtxtGroundWaterRequirementPro.Enabled = true;
                    rfvtxtSurfaceWaterRequirementPro.Enabled = true;
                    rfvtxtProposedExistingWaterSupplyPro.Enabled = true;
                    rfvtxtRecyWaterUsagePro.Enabled = true;
                    txtGroundWaterRequirementPro.Enabled = true;
                    txtSurfaceWaterRequirementPro.Enabled = true;
                    rfvtxtProposedExistingWaterSupplyPro.Enabled = true;
                    txtRecyWaterUsagePro.Enabled = true;

                    break;
                default:
                    break;
            }

            category = Convert.ToString(new NOCAP.BLL.Master.ApplicationTypeCategory((int)obj_infrastructureNewApplication.ApplicationTypeCode, (int)obj_infrastructureNewApplication.ApplicationTypeCategoryCode).ApplicationTypeCategoryDesc);
            if (category == "Government Water Supply Agencies")
            {
                txtDwellingUnits.Enabled = false;
                RequiredFieldValidatortxtDweelunit.Enabled = false;
                txtDwellingPopulation.Enabled = false;
                RFVtxtDwellingPopulation.Enabled = false;
                txtCommercialUnits.Enabled = false;
                rfvtxtCommercialUnits.Enabled = false;
                txtCommercialPopulation.Enabled = false;
                rfvtxtCommercialPopulation.Enabled = false;
                txtIndustrialUnits.Enabled = false;
                RequiredFieldValidatortxtIndustrialUnits.Enabled = false;
                txtOtherUnits.Enabled = false;
                RequiredFieldValidatortxtOtherUnits.Enabled = false;

                txtPopulation.Enabled = true;
                txtPopulation.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.NoOfPopulation));


            }
            else
            {
                txtDwellingUnits.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.NoOfDwellingUnits));
                txtDwellingPopulation.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.DwellPopulation));
                txtCommercialUnits.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.NoOfCommercialUnits));
                txtCommercialPopulation.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.CommercialPopulation));
                txtIndustrialUnits.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.NoOfIndustrialUnits));
                txtOtherUnits.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.NoOfOtherUnits));

                txtPopulation.Enabled = false;
                rfvtxtPopulation.Enabled = false;
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
            Server.Transfer("~/ExternalUser/InfrastructureNew/LandUseDetails.aspx");
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
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                UpdateWaterRequirementDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
            }
        }
    }
    private int UpdateWaterRequirementDetails(long lngA_ApplicationCode)
    {
        try
        {
            strActionName = "UpdateWaterRequirementDetails";
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            category = Convert.ToString(new NOCAP.BLL.Master.ApplicationTypeCategory((int)obj_infrastructureNewApplication.ApplicationTypeCode, (int)obj_infrastructureNewApplication.ApplicationTypeCategoryCode).ApplicationTypeCategoryDesc);
            obj_infrastructureNewApplication.NoOfDwellingUnits = Convert.ToString(txtDwellingUnits.Text);
            obj_infrastructureNewApplication.DwellPopulation = Convert.ToString(txtDwellingPopulation.Text);
            obj_infrastructureNewApplication.NoOfCommercialUnits = Convert.ToString(txtCommercialUnits.Text);
            obj_infrastructureNewApplication.CommercialPopulation = Convert.ToString(txtCommercialPopulation.Text);
            obj_infrastructureNewApplication.NoOfIndustrialUnits = Convert.ToString(txtIndustrialUnits.Text);
            obj_infrastructureNewApplication.NoOfOtherUnits = Convert.ToString(txtOtherUnits.Text);
            obj_infrastructureNewApplication.NoOfPopulation = Convert.ToString(txtPopulation.Text);
            if (txtGroundWaterRequirementPro.Text.Trim() == "")
            {
                obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement = null;
            }
            else
            {
                obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement = Convert.ToDecimal(txtGroundWaterRequirementPro.Text.Trim());
            }
            if (txtSurfaceWaterRequirementPro.Text.Trim() == "")
            {
                obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirement = null;
            }
            else
            {
                obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirement = Convert.ToDecimal(txtSurfaceWaterRequirementPro.Text.Trim());
            }
            if (txtProposedExistingWaterSupplyPro.Text.Trim() == "")
            {
                obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.ProposedExistingWaterSupplyFromAnyAgency = null;
            }
            else
            {
                obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.ProposedExistingWaterSupplyFromAnyAgency = Convert.ToDecimal(txtProposedExistingWaterSupplyPro.Text.Trim());
            }
            if (txtRecyWaterUsagePro.Text.Trim() == "")
            {
                obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.RecycleWaterUses = null;
            }
            else
            {
                obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.RecycleWaterUses = Convert.ToDecimal(txtRecyWaterUsagePro.Text.Trim());
            }


            if (txtGroundWaterRequirementExist.Text.Trim() == "")
            {
                obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist = null;
            }
            else
            {
                obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist = Convert.ToDecimal(txtGroundWaterRequirementExist.Text.Trim());
            }

            if (txtGroundWaterRequirementYear.Text.Trim() == "")
            {
                obj_infrastructureNewApplication.GroundWaterReqInYear = null;
            }
            else
            {
                obj_infrastructureNewApplication.GroundWaterReqInYear = Convert.ToDecimal(txtGroundWaterRequirementYear.Text.Trim());
            }
            if (txtSurfaceWaterRequirementExist.Text.Trim() == "")
            {
                obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementExist = null;
            }
            else
            {
                obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementExist = Convert.ToDecimal(txtSurfaceWaterRequirementExist.Text.Trim());
            }
            if (txtProposedExistingWaterSupplyExist.Text.Trim() == "")
            {
                obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.ProposedExistingWaterSupplyFromAnyAgencyExist = null;
            }
            else
            {
                obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.ProposedExistingWaterSupplyFromAnyAgencyExist = Convert.ToDecimal(txtProposedExistingWaterSupplyExist.Text.Trim());
            }
            if (txtRecyWaterUsageExist.Text.Trim() == "")
            {
                obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist = null;
            }
            else
            {
                obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist = Convert.ToDecimal(txtRecyWaterUsageExist.Text.Trim());
            }
            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_infrastructureNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;
            if (obj_infrastructureNewApplication.Update() == 1)
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

                lblMessage.Text = obj_infrastructureNewApplication.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
        }
        catch (Exception)
        {
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
                if (UpdateWaterRequirementDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text)) == 1)
                {
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewSADApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    decimal dec_netGroundWaterRequirement = Convert.ToDecimal(txtGroundWaterRequirementExist.Text == "" ? "0" : txtGroundWaterRequirementExist.Text) + Convert.ToDecimal(txtGroundWaterRequirementPro.Text == "" ? "0" : txtGroundWaterRequirementPro.Text);
                    if ((new NOCAP.BLL.Master.ApplicationTypeCategory(obj_infrastructureNewSADApplication.ApplicationTypeCode, obj_infrastructureNewSADApplication.ApplicationTypeCategoryCode)).ExemptionAllow == NOCAP.BLL.Master.ApplicationTypeCategory.ExemptionAllowYesNo.Yes && obj_infrastructureNewSADApplication.MSME == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.MSMEYesNo.Yes && dec_netGroundWaterRequirement < 10 && obj_infrastructureNewSADApplication.MSMETypeCode != 3)
                        Server.Transfer("SalientFeature.aspx");
                    else
                        Server.Transfer("De-WateringExistingStructure.aspx");
                }
            }
        }
    }
}