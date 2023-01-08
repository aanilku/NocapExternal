using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Threading;

public partial class ExternalUser_Expansion_INF_BreakUpOfWaterRequirment : System.Web.UI.Page
{
    string strPageName = "INFBreakUpofWaterRequirment";
    string strActionName = "";
    string strStatus = "";
    //string category = "";
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
                if (lblInfrastructureApplicationCodeFrom.Text.Trim() != "") { BindGVInfNewBreakUpOfWaterDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text)); }
                ddlETPSTPProposed_SelectedIndexChanged(sender, e);
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


    private void ValidationExpInit()
    {
        revtxtResidDomExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtResidDomExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtResidDomProposed.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtResidDomProposed.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtResidDomDaysInYear.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtResidDomDaysInYear.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtCOMExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtCOMExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtCOMProposed.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtCOMProposed.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtCOMDaysInYear.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtCOMDaysInYear.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtIndActExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtIndActExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtIndActProposed.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtIndActProposed.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtIndActDaysInYear.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtIndActDaysInYear.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtGreenDevelExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtGreenDevelExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtConstAct.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtConstAct.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");



        revtxtGreenDevelProposed.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtGreenDevelProposed.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");



        revtxtConstActProposed.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtConstActProposed.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtGreenDevelDaysInYear.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtGreenDevelDaysInYear.ErrorMessage = ValidationUtility.txtValForNumericMsg;


        revtxtConstActNoOfOperationalDaysInYear.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtConstActNoOfOperationalDaysInYear.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtOtherUseExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtOtherUseExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtOtherUseProposed.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtOtherUseProposed.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtOtherUseDaysInYear.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtOtherUseDaysInYear.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtTotalwasteWaterInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtTotalwasteWaterInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtQuantityReuseCommInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtQuantityReuseCommInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtQuantityReuseIndActInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtQuantityReuseIndActInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtQtyReuseGBDevInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtQtyReuseGBDevInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtOtherUsesInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtOtherUsesInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

    }

    private void BindGVInfNewBreakUpOfWaterDetails(long lngA_ApplicationCode)
    {
        NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication obj_infrastructureExpansionApplication = new NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication(lngA_ApplicationCode);

        hdnWaterReqExistingTotal.Value = Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist + obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementExist + obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.ProposedExistingWaterSupplyFromAnyAgencyExist + obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist);
        hdnWaterReqProposedTotal.Value = Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement + obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirement + obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.ProposedExistingWaterSupplyFromAnyAgency + obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.RecycleWaterUses);
        hdnRecycledWaterUsageTotal.Value = Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.RecycleWaterUses + obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist);

        //category = Convert.ToString(new NOCAP.BLL.Master.ApplicationTypeCategory((int)obj_infrastructureExpansionApplication.ApplicationTypeCode, (int)obj_infrastructureExpansionApplication.ApplicationTypeCategoryCode).ApplicationTypeCategoryDesc);
        //if (category == "Residential apartment" || category == "Group housing" || category == "Government water Supply agencies")
        //{
        //trIndActExistRequirement.Visible = false;
        //rfvtxtIndActExistRequirement.Enabled = false;
        //rfvtxtIndActProposedRequirement.Enabled = false;
        //RequiredFieldValidator19.Enabled = false;
        //}



        txtIndActExistRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ExistingReq));
        txtIndActProposedRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ProposedReq));
        txtIndActNoOfOperationalDaysInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_NoOfOperationdaysInAYear));
        txtCOMExistRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_ExistingReq));
        txtCOMProposedRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_ProposedReq));
        txtCOMNoOfOperationalDaysInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_NoOfOperationsInAYear));
        txtResidDomExistRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ExistingReq));
        txtResidDomProposedRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ProposedReq));
        txtResidDomNoOfOperationalDaysInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_NoOfOperationdaysInAYear));
        txtGreenDevelEnviMaintExistRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ExistingReq));
        txtGreenDevelEnviMaintProposedRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ProposedReq));
        txtGreenDevelEnviMaintNoOfOperationalDaysInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_NoOfOperationdaysInAYear));

        txtConstActExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ConstAct_ExistingReq));
        txtConstActProposed.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ConstAct_ProposedReq));
        txtConstActNoOfOperationalDaysInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ConstAct_NoOfOperationdaysInAYear));


        txtOtherUseExistRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ExistingReq));
        txtOtherUseProposedRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ProposedReq));
        txtOtherUseNoOfOperationalDaysInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_NoOfOperationdaysInAYear));

        if (obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.EtpStpProposed == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewBreakupOFRecycleWater.ETPSTPProposed.Yes)
        {

            txtTotalwasteWaterGeneratedInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedInDay));
            txtTotalwasteWaterGeneratedNoOfDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedNoOfDay));
            txtTotalwasteWaterGeneratedInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedInYear));

            txtQuantityTreatedWaterAvailable.Text = HttpUtility.HtmlEncode((Convert.ToDecimal(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.RecycleWaterUses) + Convert.ToDecimal(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist)).ToString("0.00"));


            txtQuantityReuseCommercialActivityInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.CommercialActivityInDay));
            txtQuantityReuseCommercialActivityNoOfDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.CommercialActivityNoOfDay));
            txtQuantityReuseCommercialActivityInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.CommercialActivityInYear));
            txtQuantityReuseIndustrialActivityInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.IndustrialActivityInDay));
            txtQuantityReuseIndustrialActivityNoOfDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.CommercialActivityNoOfDay));
            txtQuantityReuseIndustrialActivityInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.IndustrialActivityInYear));
            txtQuantityReuseGreenBeltDevelInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.GreenBeltDevelopmentInDay));
            txtQuantityReuseGreenBeltDevelNoOfDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.GreenBeltDevelopmentNoOfDay));
            txtQuantityReuseGreenBeltDevelInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.GreenBeltDevelopmentInYear));
            txtOtherUsesInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInDay));
            txtOtherUsesNoOfDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseNoOfDay));
            txtOtherUsesInYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInYear));

            //txtIndActivityPerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.IndustrialActivityInDay));
            //txtIndActivityPerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.IndustrialActivityInYear));
            //txtCommercialActivityPerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.CommercialActivityInDay));
            //txtCommercialActivityPerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.CommercialActivityInYear));
            //txtGreenBeltDevelPerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.GreenBeltDevelopmentInDay));
            //txtGreenBeltDevelPerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.GreenBeltDevelopmentInYear));
            //txtDomesticPerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.DomesticActivityInDay));
            //txtDomesticPerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.DomesticActivityInYear));
            //txtOtherUsePerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInDay));
            //txtOtherUsePerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInYear));
            ddlETPSTPProposed.SelectedValue = "Yes";
        }
        else { ddlETPSTPProposed.SelectedValue = "No"; }

        txtNetGroundWaterRequirement.Text = HttpUtility.HtmlEncode(Convert.ToDecimal(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement) + Convert.ToDecimal(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));

        switch (obj_infrastructureExpansionApplication.GroundWaterUtilizationFor)
        {
            case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.GroundWaterUtilizationForOption.NewProject:
                txtResidDomExistRequirement.Enabled = false;
                txtCOMExistRequirement.Enabled = false;
                txtIndActExistRequirement.Enabled = false;
                txtGreenDevelEnviMaintExistRequirement.Enabled = false;
                txtConstActExist.Enabled = false;
                txtOtherUseExistRequirement.Enabled = false;
                rfvtxtResidDomExistRequirement.Enabled = false;
                rfvtxtCOMExistRequirement.Enabled = false;
                rfvtxtIndActExistRequirement.Enabled = false;
                rfvtxtGreenDevelEnviMaintExistRequirement.Enabled = false;
                rfvtxtConstAct.Enabled = false;
                rfvtxtOtherUseExistRequirement.Enabled = false;
                txtResidDomExistRequirement.Text = "0";
                txtCOMExistRequirement.Text = "0";
                txtIndActExistRequirement.Text = "0";
                txtGreenDevelEnviMaintExistRequirement.Text = "0";
                txtConstActExist.Text = "0";
                txtOtherUseExistRequirement.Text = "0";

                txtResidDomProposedRequirement.Enabled = true;
                txtCOMProposedRequirement.Enabled = true;
                txtIndActProposedRequirement.Enabled = true;
                txtGreenDevelEnviMaintProposedRequirement.Enabled = true;
                txtOtherUseProposedRequirement.Enabled = true;
                rfvtxtResidDomProposedRequirement.Enabled = true;
                rfvtxtCOMProposedRequirement.Enabled = true;
                rfvtxtIndActProposedRequirement.Enabled = true;
                rfvtxtGreenDevelEnviMaintProposedRequirement.Enabled = true;
                rfvtxtConstActProposed.Enabled = true;
                rfvtxtOtherUseProposedRequirement.Enabled = true;

                break;
            case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry:
                txtResidDomExistRequirement.Enabled = true;
                txtCOMExistRequirement.Enabled = true;
                txtIndActExistRequirement.Enabled = true;
                txtGreenDevelEnviMaintExistRequirement.Enabled = true;
                txtConstActExist.Enabled = true;
                txtOtherUseExistRequirement.Enabled = true;
                rfvtxtResidDomExistRequirement.Enabled = true;
                rfvtxtCOMExistRequirement.Enabled = true;
                rfvtxtIndActExistRequirement.Enabled = true;
                rfvtxtGreenDevelEnviMaintExistRequirement.Enabled = true;
                rfvtxtConstAct.Enabled = true;
                rfvtxtOtherUseExistRequirement.Enabled = true;

                txtResidDomProposedRequirement.Enabled = false;
                txtCOMProposedRequirement.Enabled = false;
                txtIndActProposedRequirement.Enabled = false;
                txtGreenDevelEnviMaintProposedRequirement.Enabled = false;
                txtOtherUseProposedRequirement.Enabled = false;
                rfvtxtResidDomProposedRequirement.Enabled = false;
                rfvtxtCOMProposedRequirement.Enabled = false;
                rfvtxtIndActProposedRequirement.Enabled = false;
                rfvtxtGreenDevelEnviMaintProposedRequirement.Enabled = false;
                rfvtxtConstActProposed.Enabled = false;
                rfvtxtOtherUseProposedRequirement.Enabled = false;
                txtResidDomProposedRequirement.Text = "0";
                txtCOMProposedRequirement.Text = "0";
                txtIndActProposedRequirement.Text = "0";
                txtGreenDevelEnviMaintProposedRequirement.Text = "0";
                txtConstActProposed.Text = "0";
                txtOtherUseProposedRequirement.Text = "0";
                break;
            case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.GroundWaterUtilizationForOption.ExpansionProgramme:
                txtResidDomExistRequirement.Enabled = true;
                txtCOMExistRequirement.Enabled = true;
                txtIndActExistRequirement.Enabled = true;
                txtGreenDevelEnviMaintExistRequirement.Enabled = true;
                txtConstActExist.Enabled = true;
                txtOtherUseExistRequirement.Enabled = true;
                rfvtxtResidDomExistRequirement.Enabled = true;
                rfvtxtCOMExistRequirement.Enabled = true;
                rfvtxtIndActExistRequirement.Enabled = true;
                rfvtxtGreenDevelEnviMaintExistRequirement.Enabled = true;
                rfvtxtConstAct.Enabled = true;
                rfvtxtOtherUseExistRequirement.Enabled = true;
                txtResidDomProposedRequirement.Enabled = true;
                txtCOMProposedRequirement.Enabled = true;
                txtIndActProposedRequirement.Enabled = true;
                txtGreenDevelEnviMaintProposedRequirement.Enabled = true;
                txtOtherUseProposedRequirement.Enabled = true;
                rfvtxtResidDomProposedRequirement.Enabled = true;
                rfvtxtCOMProposedRequirement.Enabled = true;
                rfvtxtIndActProposedRequirement.Enabled = true;
                rfvtxtGreenDevelEnviMaintProposedRequirement.Enabled = true;
                rfvtxtConstActProposed.Enabled = true;
                rfvtxtOtherUseProposedRequirement.Enabled = true;
                break;
            default:
                break;
        }

        ScriptManager.RegisterStartupScript(this, GetType(), "function", "cal();", true);
    }

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
                Server.Transfer("~/ExternalUser/Expansion/INF/ProposedGroundwaterAbstractionStructure.aspx");
                //Server.Transfer("~/ExternalUser/InfrastructureNew/De-WateringProposedStructure.aspx");
            }
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
                UpdateInfNewBreakUpOfWaterDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
                //BindGVInfNewBreakUpOfWaterDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
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
                NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter obj_ApplicationAllowOrNotForExemptionLetter = new NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter();
                //string str_RedirectPath = "";
                try
                {
                    if (UpdateInfNewBreakUpOfWaterDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text)) == 1)
                    {
                        NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication obj_infrastructureExpansionApplication = new NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        int int_stateCode = obj_infrastructureExpansionApplication.ProposedLocation.ProposedLocationAddress.StateCode;
                        int int_districtCode = obj_infrastructureExpansionApplication.ProposedLocation.ProposedLocationAddress.DistrictCode;
                        int int_subDistrictCode = obj_infrastructureExpansionApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode;
                        int int_applicationTypeCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Infrastructure");
                        int int_applicationPurposeCode = NOCAPExternalUtility.EquivalentApplicationPurposeCodeOfDatabaseForApplicationPurpose("New");
                        int int_applicationTypeCategoryCode = obj_infrastructureExpansionApplication.ApplicationTypeCategoryCode;
                        int int_WaterQualityCode = obj_infrastructureExpansionApplication.WaterQualityCode;
                        decimal? dec_gwRequirement = obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement;
                        decimal? dec_netGroundWaterRequirement = obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement + obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist;


                        obj_ApplicationAllowOrNotForExemptionLetter = new NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter(int_applicationTypeCode, int_applicationPurposeCode, int_applicationTypeCategoryCode, int_stateCode, int_districtCode, int_subDistrictCode, dec_netGroundWaterRequirement, int_WaterQualityCode);
                        if (obj_ApplicationAllowOrNotForExemptionLetter.ProceedForFinalExemptionLetter == NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter.ApplicationAllowOrNotForExemptionLetterOption.NotAllow)
                        {
                            Server.Transfer("WaterSupplyDetail.aspx");
                        }


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "function", "cal();", true);
                    }
                }
                catch (ThreadAbortException ex)
                { }
                catch (Exception ex)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
                }
                finally
                {
                    obj_ApplicationAllowOrNotForExemptionLetter.Dispose();
                    //switch (str_RedirectPath)
                    //{
                    //    case "SalientFeature.aspx":
                    //        Server.Transfer("SalientFeature.aspx");
                    //        break;
                    //    case "~/ExternalUser/InfrastructureNew/WaterSupplyDetail.aspx":
                    //        Server.Transfer("~/ExternalUser/InfrastructureNew/WaterSupplyDetail.aspx");
                    //        break;
                    //}
                }

            }
        }
    }



    private int UpdateInfNewBreakUpOfWaterDetails(long lngA_ApplicationCode)
    {
        try
        {
            decimal sum = 0;
            strActionName = "UpdateBreakOfWaterDetails";
            NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication obj_infrastructureExpansionApplication = new NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication(lngA_ApplicationCode);

            //sum = Convert.ToDecimal(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement) - (Convert.ToDecimal(txtIndActivityPerDay.Text != "" ? txtIndActivityPerDay.Text : "0") + Convert.ToDecimal(txtCommercialActivityPerDay.Text != "" ? txtCommercialActivityPerDay.Text : "0") + Convert.ToDecimal(txtGreenBeltDevelPerDay.Text != "" ? txtGreenBeltDevelPerDay.Text : "0") + Convert.ToDecimal(txtDomesticPerDay.Text != "" ? txtDomesticPerDay.Text : "0") + Convert.ToDecimal(txtOtherUsePerDay.Text != "" ? txtOtherUsePerDay.Text : "0"));

            //if (sum != Convert.ToDecimal(txtNetGroundWaterRequirement.Text))
            //{
            //    lblMessage.Text = "Net Ground Water Requirement = ( Ground Water Requirement - Total Treated Water Utilized )";
            //    lblMessage.ForeColor = System.Drawing.Color.Red;
            //    return 0;
            //}

            //if (obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == Convert.ToDecimal(txtNetGroundWaterRequirement.Text))
            //{

            if (txtIndActExistRequirement.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ExistingReq = null; }
            else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ExistingReq = Convert.ToDecimal(txtIndActExistRequirement.Text); }

            if (txtIndActProposedRequirement.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ProposedReq = null; }
            else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ProposedReq = Convert.ToDecimal(txtIndActProposedRequirement.Text.Trim()); }

            if (txtIndActNoOfOperationalDaysInYear.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_NoOfOperationdaysInAYear = null; }
            else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_NoOfOperationdaysInAYear = Convert.ToInt32(txtIndActNoOfOperationalDaysInYear.Text); }



            if (txtCOMExistRequirement.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_ExistingReq = null; }
            else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_ExistingReq = Convert.ToDecimal(txtCOMExistRequirement.Text); }

            if (txtCOMProposedRequirement.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_ProposedReq = null; }
            else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_ProposedReq = Convert.ToDecimal(txtCOMProposedRequirement.Text.Trim()); }

            if (txtCOMNoOfOperationalDaysInYear.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_NoOfOperationsInAYear = null; }
            else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_NoOfOperationsInAYear = Convert.ToInt32(txtCOMNoOfOperationalDaysInYear.Text); }






            if (txtResidDomExistRequirement.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ExistingReq = null; }
            else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ExistingReq = Convert.ToDecimal(txtResidDomExistRequirement.Text); }

            if (txtResidDomProposedRequirement.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ProposedReq = null; }
            else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ProposedReq = Convert.ToDecimal(txtResidDomProposedRequirement.Text); }

            if (txtResidDomNoOfOperationalDaysInYear.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_NoOfOperationdaysInAYear = null; }
            else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_NoOfOperationdaysInAYear = Convert.ToInt32(txtResidDomNoOfOperationalDaysInYear.Text); }

            if (txtGreenDevelEnviMaintExistRequirement.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ExistingReq = null; }
            else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ExistingReq = Convert.ToDecimal(txtGreenDevelEnviMaintExistRequirement.Text); }

            if (txtGreenDevelEnviMaintProposedRequirement.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ProposedReq = null; }
            else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ProposedReq = Convert.ToDecimal(txtGreenDevelEnviMaintProposedRequirement.Text); }

            if (txtGreenDevelEnviMaintNoOfOperationalDaysInYear.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_NoOfOperationdaysInAYear = null; }
            else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_NoOfOperationdaysInAYear = Convert.ToInt32(txtGreenDevelEnviMaintNoOfOperationalDaysInYear.Text); }


            if (txtConstActExist.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ConstAct_ExistingReq = null; }
            else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ConstAct_ExistingReq = Convert.ToDecimal(txtConstActExist.Text); }

            if (txtConstActProposed.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ConstAct_ProposedReq = null; }
            else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ConstAct_ProposedReq = Convert.ToDecimal(txtConstActProposed.Text); }

            if (txtConstActNoOfOperationalDaysInYear.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ConstAct_NoOfOperationdaysInAYear = null; }
            else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ConstAct_NoOfOperationdaysInAYear = Convert.ToInt32(txtConstActNoOfOperationalDaysInYear.Text); }



            if (txtOtherUseExistRequirement.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ExistingReq = null; }
            else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ExistingReq = Convert.ToDecimal(txtOtherUseExistRequirement.Text); }

            if (txtOtherUseProposedRequirement.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ProposedReq = null; }
            else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ProposedReq = Convert.ToDecimal(txtOtherUseProposedRequirement.Text); }

            if (txtOtherUseNoOfOperationalDaysInYear.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_NoOfOperationdaysInAYear = null; }
            else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_NoOfOperationdaysInAYear = Convert.ToInt32(txtOtherUseNoOfOperationalDaysInYear.Text); }

            switch (ddlETPSTPProposed.SelectedValue)
            {
                case "Yes":
                    obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.EtpStpProposed = NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewBreakupOFRecycleWater.ETPSTPProposed.Yes;
                    break;
                case "No":
                    obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.EtpStpProposed = NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewBreakupOFRecycleWater.ETPSTPProposed.No;
                    break;
            }

            if (ddlETPSTPProposed.SelectedValue == "Yes")
            {

                if (txtTotalwasteWaterGeneratedInDay.Text.Trim() == "")
                {
                    obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedInDay = null;
                }
                else
                {
                    if (!(NOCAPExternalUtility.IsNumeric(txtTotalwasteWaterGeneratedInDay.Text.Trim())))
                    {
                    }
                    else
                    {
                        obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedInDay = Convert.ToDecimal(txtTotalwasteWaterGeneratedInDay.Text.Trim());
                    }
                }
                if (txtTotalwasteWaterGeneratedNoOfDay.Text.Trim() == "")
                {
                    obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedNoOfDay = null;
                }
                else
                {
                    if (!(NOCAPExternalUtility.IsNumeric(txtTotalwasteWaterGeneratedNoOfDay.Text.Trim())))
                    {
                    }
                    else
                    {
                        obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedNoOfDay = Convert.ToInt32(txtTotalwasteWaterGeneratedNoOfDay.Text.Trim());
                    }
                }
                obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedInYear = Convert.ToDecimal(txtTotalwasteWaterGeneratedInDay.Text.Trim()) * Convert.ToDecimal(txtTotalwasteWaterGeneratedNoOfDay.Text.Trim());







                if (txtQuantityReuseCommercialActivityInDay.Text.Trim() == "")
                {
                    obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.CommercialActivityInDay = null;
                }
                else
                {
                    if (!(NOCAPExternalUtility.IsNumeric(txtQuantityReuseCommercialActivityInDay.Text.Trim())))
                    {
                    }
                    else
                    {
                        obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.CommercialActivityInDay = Convert.ToDecimal(txtQuantityReuseCommercialActivityInDay.Text.Trim());
                    }
                }

                obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.CommercialActivityNoOfDay = obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_NoOfOperationsInAYear;
                obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.CommercialActivityInYear = Convert.ToDecimal(txtQuantityReuseCommercialActivityInDay.Text.Trim()) * Convert.ToDecimal(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.CommercialActivityNoOfDay);














                if (txtQuantityReuseIndustrialActivityInDay.Text.Trim() == "")
                {
                    obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.IndustrialActivityInDay = null;
                }
                else
                {
                    if (!(NOCAPExternalUtility.IsNumeric(txtQuantityReuseIndustrialActivityInDay.Text.Trim())))
                    {
                    }
                    else
                    {
                        obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.IndustrialActivityInDay = Convert.ToDecimal(txtQuantityReuseIndustrialActivityInDay.Text.Trim());
                    }
                }

                obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.IndustrialActivityNoOfDay = obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_NoOfOperationdaysInAYear;
                obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.IndustrialActivityInYear = Convert.ToDecimal(txtQuantityReuseIndustrialActivityInDay.Text.Trim()) * Convert.ToDecimal(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.IndustrialActivityNoOfDay);

                if (txtQuantityReuseGreenBeltDevelInDay.Text.Trim() == "")
                {
                    obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.GreenBeltDevelopmentInDay = null;
                }
                else
                {
                    if (!(NOCAPExternalUtility.IsNumeric(txtQuantityReuseGreenBeltDevelInDay.Text.Trim())))
                    {
                    }
                    else
                    {
                        obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.GreenBeltDevelopmentInDay = Convert.ToDecimal(txtQuantityReuseGreenBeltDevelInDay.Text.Trim());
                    }
                }
                obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.GreenBeltDevelopmentNoOfDay = obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_NoOfOperationdaysInAYear;
                obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.GreenBeltDevelopmentInYear = Convert.ToDecimal(txtQuantityReuseGreenBeltDevelInDay.Text.Trim()) * Convert.ToDecimal(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.GreenBeltDevelopmentNoOfDay);

                if (txtOtherUsesInDay.Text.Trim() == "")
                {
                    obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInDay = null;
                }
                else
                {
                    if (!(NOCAPExternalUtility.IsNumeric(txtOtherUsesInDay.Text.Trim())))
                    {
                    }
                    else
                    {
                        obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInDay = Convert.ToDecimal(txtOtherUsesInDay.Text.Trim());
                    }
                }
                obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseNoOfDay = obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_NoOfOperationdaysInAYear;
                obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInYear = Convert.ToDecimal(txtOtherUsesInDay.Text.Trim()) * Convert.ToDecimal(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseNoOfDay);



                //if (txtIndActivityPerDay.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.IndustrialActivityInDay = null; }
                //else
                //{
                //    obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.IndustrialActivityInDay = Convert.ToDecimal(txtIndActivityPerDay.Text);
                //}

                //if (txtIndActivityPerYear.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.IndustrialActivityInYear = null; }
                //else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.IndustrialActivityInYear = Convert.ToDecimal(txtIndActivityPerYear.Text); }

                //if (txtCommercialActivityPerDay.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.CommercialActivityInDay = null; }
                //else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.CommercialActivityInDay = Convert.ToDecimal(txtCommercialActivityPerDay.Text); }

                //if (txtCommercialActivityPerYear.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.CommercialActivityInYear = null; }
                //else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.CommercialActivityInYear = Convert.ToDecimal(txtCommercialActivityPerYear.Text); }

                //if (txtGreenBeltDevelPerDay.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.GreenBeltDevelopmentInDay = null; }
                //else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.GreenBeltDevelopmentInDay = Convert.ToDecimal(txtGreenBeltDevelPerDay.Text); }

                //if (txtGreenBeltDevelPerYear.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.GreenBeltDevelopmentInYear = null; }
                //else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.GreenBeltDevelopmentInYear = Convert.ToDecimal(txtGreenBeltDevelPerYear.Text); }

                //if (txtDomesticPerDay.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.DomesticActivityInDay = null; }
                //else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.DomesticActivityInDay = Convert.ToDecimal(txtDomesticPerDay.Text); }

                //if (txtDomesticPerYear.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.DomesticActivityInYear = null; }
                //else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.DomesticActivityInYear = Convert.ToDecimal(txtDomesticPerYear.Text); }

                //if (txtOtherUsePerDay.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInDay = null; }
                //else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInDay = Convert.ToDecimal(txtOtherUsePerDay.Text); }

                //if (txtOtherUsePerYear.Text.Trim() == "") { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInYear = null; }
                //else { obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInYear = Convert.ToDecimal(txtOtherUsePerYear.Text); }
            }
            else
            {
                obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.IndustrialActivityInDay = null;
                obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.IndustrialActivityInYear = null;
                obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.CommercialActivityInDay = null;
                obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.CommercialActivityInYear = null;
                obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.GreenBeltDevelopmentInDay = null;
                obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.GreenBeltDevelopmentInYear = null;
                obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.DomesticActivityInDay = null;
                obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.DomesticActivityInYear = null;
                obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInDay = null;
                obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInYear = null;
            }

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_infrastructureExpansionApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            //imran
            NOCAP.BLL.Master.SegmentBAreaType obj_SegmentBAreaType = new NOCAP.BLL.Master.SegmentBAreaType(obj_infrastructureExpansionApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_infrastructureExpansionApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_infrastructureExpansionApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
            if (obj_SegmentBAreaType.CreatedOn != null && ((obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_ExistingReq + obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.CommercialActivity_ProposedReq) > 0 || (obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ExistingReq + obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ProposedReq) > 0 || (obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ExistingReq + obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ProposedReq) > 0))
            {

                lblMessage.Text = "Commercial,Infrastructure Activity or Other Use-NOC for groundwater extraction shall not be granted in Segment B.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            else
            {
                if (obj_infrastructureExpansionApplication.Update() == 1)
                {
                    strStatus = "Saved Successfully";
                    lblMessage.Text = "Successfully Saved";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    return 1;
                }
                else
                {
                    strStatus = "Save Unsuccessfull";
                    lblMessage.Text = obj_infrastructureExpansionApplication.CustumMessage;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return 0;
                }
            }
            //}
            //else
            //{
            //    lblMessage.Text = "Net Ground Water Requirement is not Matched with 3(i) Ground Water Requirement.";
            //    lblMessage.ForeColor = System.Drawing.Color.Red;
            //    return 0;
            //}

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
            ScriptManager.RegisterStartupScript(this, GetType(), "function", "cal();", true);
        }
    }

    protected void ddlETPSTPProposed_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            if (ddlETPSTPProposed.SelectedValue == "Yes")
            {
                NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication obj_infrastructureExpansionApplication = new NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
                txtQuantityTreatedWaterAvailable.Text = HttpUtility.HtmlEncode((Convert.ToDecimal(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.RecycleWaterUses) + Convert.ToDecimal(obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist)).ToString("0.00"));


                txtTotalwasteWaterGeneratedInDay.Enabled = true;
                rfvtxtTotalwasteWaterGeneratedInDay.Enabled = true;
                txtTotalwasteWaterGeneratedNoOfDay.Enabled = true;
                rfvtxtTotalwasteWaterGeneratedNoOfDay.Enabled = true;
                txtQuantityReuseCommercialActivityInDay.Enabled = true;
                rfvtxtQuantityReuseCommercialActivityInDay.Enabled = true;
                txtQuantityReuseIndustrialActivityInDay.Enabled = true;
                rfvtxtQuantityReuseIndustrialActivityInDay.Enabled = true;
                txtQuantityReuseGreenBeltDevelInDay.Enabled = true;
                rfvtxtQuantityReuseGreenBeltDevelInDay.Enabled = true;
                txtOtherUsesInDay.Enabled = true;
                rfvtxtOtherUsesInDay.Enabled = true;
            }
            else
            {
                txtTotalwasteWaterGeneratedInDay.Enabled = false;
                rfvtxtTotalwasteWaterGeneratedInDay.Enabled = false;
                txtTotalwasteWaterGeneratedNoOfDay.Enabled = false;
                rfvtxtTotalwasteWaterGeneratedNoOfDay.Enabled = false;
                txtQuantityReuseCommercialActivityInDay.Enabled = false;
                rfvtxtQuantityReuseCommercialActivityInDay.Enabled = false;
                txtQuantityReuseIndustrialActivityInDay.Enabled = false;
                rfvtxtQuantityReuseIndustrialActivityInDay.Enabled = false;
                txtQuantityReuseGreenBeltDevelInDay.Enabled = false;
                rfvtxtQuantityReuseGreenBeltDevelInDay.Enabled = false;
                txtOtherUsesInDay.Enabled = false;
                rfvtxtOtherUsesInDay.Enabled = false;
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "function", "cal();", true);
    }
}