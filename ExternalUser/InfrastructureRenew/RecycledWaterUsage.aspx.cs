using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_InfrastructureRenew_RecycledWaterUsage : System.Web.UI.Page
{
    string strPageName = "INFRenewRecycledWaterUsage";
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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblInfrastructureApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                        }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    BindRecycledWaterUsageDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    private void ValidationExpInit()
    {
        revtxtEfflSewGeneratedETPSTPExistInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtEfflSewGeneratedETPSTPExistInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtEfflSewGeneratedETPSTPAdditInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtEfflSewGeneratedETPSTPAdditInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtEfflSewGeneratedETPSTPExistNoOfDay.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtEfflSewGeneratedETPSTPExistNoOfDay.ErrorMessage = ValidationUtility.txtValForNumericMsg;
        revtxtEfflSewGeneratedETPSTPAdditNoOfDay.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtEfflSewGeneratedETPSTPAdditNoOfDay.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtEfflAvailableTreatedforUsageExistInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtEfflAvailableTreatedforUsageExistInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtEfflAvailableTreatedforUsageExistNoOfDay.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtEfflAvailableTreatedforUsageExistNoOfDay.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtEfflAvailableTreatedforUsageAdditInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtEfflAvailableTreatedforUsageAdditInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtEfflAvailableTreatedforUsageAdditNoOfDay.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtEfflAvailableTreatedforUsageAdditNoOfDay.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtEfflDischargeAfterTreatmentExistInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtEfflDischargeAfterTreatmentExistInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtEfflDischargeAfterTreatmentExistNoOfDay.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtEfflDischargeAfterTreatmentExistNoOfDay.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtEfflDischargeAfterTreatmentAdditInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtEfflDischargeAfterTreatmentAdditInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtEfflDischargeAfterTreatmentAdditNoOfDay.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtEfflDischargeAfterTreatmentAdditNoOfDay.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtAvaCommeUseExistInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtAvaCommeUseExistInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtAvaCommeUseAdditAvailInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtAvaCommeUseAdditAvailInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtAvaResiUseExistInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtAvaResiUseExistInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtAvaResiUseAdditAvailInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtAvaResiUseAdditAvailInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtAvaGreenbeltDevelopmentExistInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtAvaGreenbeltDevelopmentExistInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtAvaGreenbeltDevelopmentAdditAvailInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtAvaGreenbeltDevelopmentAdditAvailInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtAvaFlushReqExistInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtAvaFlushReqExistInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtAvaFlushReqAdditAvailInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtAvaFlushReqAdditAvailInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
    }

    private void BindRecycledWaterUsageDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);

            if (obj_infrastructureRenewSADApplication != null && obj_infrastructureRenewSADApplication.InfrastructureRenewApplicationCode > 0)
            {

                lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewSADApplication.NameOfInfrastructure);
                txtEfflSewGeneratedETPSTPExistInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPExist));
                txtEfflSewGeneratedETPSTPAdditInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPAddit));
                txtEfflAvailableTreatedforUsageExistInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagExist));
                txtEfflAvailableTreatedforUsageAdditInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagAddit));
                txtEfflDischargeAfterTreatmentExistInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatExist));
                txtEfflDischargeAfterTreatmentAdditInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatAddit));


                // add new 
                txtEfflSewGeneratedETPSTPExistNoOfDay.Text = Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPExistNoOfDay);
                txtEfflSewGeneratedETPSTPAdditNoOfDay.Text = Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPAdditNoOfDay);
                txtEfflAvailableTreatedforUsageExistNoOfDay.Text = Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagExistNoOfDay);
                txtEfflAvailableTreatedforUsageAdditNoOfDay.Text = Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagAdditNoOfDay);
                txtEfflDischargeAfterTreatmentExistNoOfDay.Text = Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatExistNoOfDay);
                txtEfflDischargeAfterTreatmentAdditNoOfDay.Text = Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatAdditNoOfDay);




                txtEfflSewGeneratedETPSTPExistInYear.Text = Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPExist * obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPExistNoOfDay);
                txtEfflSewGeneratedETPSTPAdditInYear.Text = Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPAddit * obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPAdditNoOfDay);
                txtEfflAvailableTreatedforUsageExistInYear.Text = Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagExist * obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagExistNoOfDay);
                txtEfflAvailableTreatedforUsageAdditInYear.Text = Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagAddit * obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagAdditNoOfDay);
                txtEfflDischargeAfterTreatmentExistInYear.Text = Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatExist * obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatExistNoOfDay);
                txtEfflDischargeAfterTreatmentAdditInYear.Text = Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatAddit * obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatAdditNoOfDay);
                // end add new



                txtEfflSewGeneratedETPSTPTotalInDay.Text = Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPExist + obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPAddit);
                txtEfflSewGeneratedETPSTPTotalInYear.Text = Convert.ToString((obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPExist * obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPExistNoOfDay) + (obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPAddit * obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPAdditNoOfDay));

                txtEfflAvailableTreatedforUsageTotalInDay.Text = Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagExist + obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagAddit);
                txtEfflAvailableTreatedforUsageTotalInYear.Text = Convert.ToString((obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagExist * obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagExistNoOfDay) + (obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagAddit * obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagAdditNoOfDay));

                txtEfflDischargeAfterTreatmentTotalInDay.Text = Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatExist + obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatAddit);
                txtEfflDischargeAfterTreatmentTotalInYear.Text = Convert.ToString((obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatExist * obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatExistNoOfDay) + (obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatAddit * obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatAdditNoOfDay));


                txtAvaCommeUseExistInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentCommercialUseExist));
                txtAvaCommeUseAdditAvailInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentCommercialUseAddit));
                txtAvaResiUseExistInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentResidentialUseExist));
                txtAvaResiUseAdditAvailInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentResidentialUseAddit));
                txtAvaGreenbeltDevelopmentExistInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentGreenDevExist));
                txtAvaGreenbeltDevelopmentAdditAvailInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentGreenDevAddit));
                txtAvaFlushReqExistInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentFlushingReqUseExist));
                txtAvaFlushReqAdditAvailInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentFlushingReqUseAddit));

                txtAvaCommUseTotalInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentCommercialUseExist + obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentCommercialUseAddit));
                txtAvaResiUseTotalInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentResidentialUseExist + obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentResidentialUseAddit));
                txtAvaGreenbeltDevelopmentTotalInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentGreenDevExist + obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentGreenDevAddit));
                txtAvaFlushReqTotalInDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentFlushingReqUseExist + obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentFlushingReqUseAddit));

                txtRecycleUsageActivityExitTotal.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentCommercialUseExist
                    + obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentResidentialUseExist
                    + obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentGreenDevExist
                    + obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentFlushingReqUseExist
                    ));

                txtRecycleUsageActivityAdditTotal.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentCommercialUseAddit
                    + obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentResidentialUseAddit
                    + obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentGreenDevAddit
                    + obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentFlushingReqUseAddit
                    ));

                txtRecycleUsageActivityTotalUseAvailTotal.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentCommercialUseExist
                    + obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentCommercialUseAddit
                    + obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentResidentialUseExist
                    + obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentResidentialUseAddit
                    + obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentGreenDevExist
                    + obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentGreenDevAddit
                    + obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentFlushingReqUseExist
                   + obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentFlushingReqUseAddit
                ));

                hidTotalRecycledWaterUsage.Value = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.RecycleWaterUsesAdditional
                                                                    + obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist));




                switch (obj_infrastructureRenewSADApplication.GroundWaterUtilizationFor)
                {
                    case NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.GroundWaterUtilizationForOption.ExistingGroundWater:

                        txtEfflSewGeneratedETPSTPAdditInDay.Enabled = false;
                        rfvtxtEfflSewGeneratedETPSTPAdditInDay.Enabled = false;
                       // txtEfflSewGeneratedETPSTPAdditInYear.Enabled = false;
                        rfvtxtEfflSewGeneratedETPSTPAdditNoOfDay.Enabled = false;
                        txtEfflAvailableTreatedforUsageAdditInDay.Enabled = false;
                        rfvtxtEfflAvailableTreatedforUsageAdditInDay.Enabled = false;
                       // txtEfflAvailableTreatedforUsageAdditInYear.Enabled = false;
                        rfvtxtEfflAvailableTreatedforUsageAdditNoOfDay.Enabled = false;
                        txtEfflDischargeAfterTreatmentAdditInDay.Enabled = false;
                        rfvtxtEfflDischargeAfterTreatmentAdditInDay.Enabled = false;
                       // txtEfflDischargeAfterTreatmentAdditInYear.Enabled = false;
                        rfvtxtEfflDischargeAfterTreatmentAdditNoOfDay.Enabled = false;


                        txtAvaCommeUseAdditAvailInDay.Enabled = false;
                        rfvtxtAvaCommeUseAdditAvailInDay.Enabled = false;
                        txtAvaResiUseAdditAvailInDay.Enabled = false;
                        rfvtxtAvaResiUseAdditAvailInDay.Enabled = false;
                        txtAvaGreenbeltDevelopmentAdditAvailInDay.Enabled = false;
                        rfvtxtAvaGreenbeltDevelopmentAdditAvailInDay.Enabled = false;
                        txtAvaFlushReqAdditAvailInDay.Enabled = false;
                        rfvtxtAvaFlushReqAdditAvailInDay.Enabled = false;
                        txtRecycleUsageActivityAdditTotal.Enabled = false;

                        txtEfflSewGeneratedETPSTPAdditNoOfDay.Enabled = false;
                        txtEfflAvailableTreatedforUsageAdditNoOfDay.Enabled = false;
                        txtEfflDischargeAfterTreatmentAdditNoOfDay.Enabled = false;


                        txtEfflSewGeneratedETPSTPAdditInDay.Text = "0";
                        txtEfflSewGeneratedETPSTPAdditInYear.Text = "0";
                        txtEfflAvailableTreatedforUsageAdditInDay.Text = "0";
                        txtEfflAvailableTreatedforUsageAdditInYear.Text = "0";
                        txtEfflDischargeAfterTreatmentAdditInDay.Text = "0";
                        txtEfflDischargeAfterTreatmentAdditInYear.Text = "0";

                        txtAvaCommeUseAdditAvailInDay.Text = "0";
                        txtAvaResiUseAdditAvailInDay.Text = "0";
                        txtAvaGreenbeltDevelopmentAdditAvailInDay.Text = "0";
                        txtAvaFlushReqAdditAvailInDay.Text = "0";
                        // txtRecycleUsageActivityAdditTotal.Text = "0";

                        txtEfflSewGeneratedETPSTPAdditNoOfDay.Text = "0";
                        txtEfflAvailableTreatedforUsageAdditNoOfDay.Text = "0";
                        txtEfflDischargeAfterTreatmentAdditNoOfDay.Text = "0";

                        txtEfflSewGeneratedETPSTPTotalInDay.Text = txtEfflSewGeneratedETPSTPExistInDay.Text;
                        txtEfflSewGeneratedETPSTPTotalInYear.Text = txtEfflSewGeneratedETPSTPExistInYear.Text;

                        txtEfflAvailableTreatedforUsageTotalInDay.Text = txtEfflAvailableTreatedforUsageExistInDay.Text;
                        txtEfflAvailableTreatedforUsageTotalInYear.Text = txtEfflAvailableTreatedforUsageExistInYear.Text;

                        txtEfflDischargeAfterTreatmentTotalInDay.Text = txtEfflDischargeAfterTreatmentExistInDay.Text;
                        txtEfflDischargeAfterTreatmentTotalInYear.Text = txtEfflDischargeAfterTreatmentExistInYear.Text;



                        break;
                    case NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.GroundWaterUtilizationForOption.ExistingGWWithAdditionalGWRequirment:

                        txtEfflSewGeneratedETPSTPAdditInDay.Enabled = true;
                        rfvtxtEfflSewGeneratedETPSTPAdditInDay.Enabled = true;
                        //txtEfflSewGeneratedETPSTPAdditInYear.Enabled = true;
                        rfvtxtEfflSewGeneratedETPSTPAdditNoOfDay.Enabled = true;
                        txtEfflAvailableTreatedforUsageAdditInDay.Enabled = true;
                        rfvtxtEfflAvailableTreatedforUsageAdditInDay.Enabled = true;
                      //  txtEfflAvailableTreatedforUsageAdditInYear.Enabled = true;
                        rfvtxtEfflAvailableTreatedforUsageAdditNoOfDay.Enabled = true;
                        txtEfflDischargeAfterTreatmentAdditInDay.Enabled = true;
                        rfvtxtEfflDischargeAfterTreatmentAdditInDay.Enabled = true;
                      //  txtEfflDischargeAfterTreatmentAdditInYear.Enabled = true;
                        rfvtxtEfflDischargeAfterTreatmentAdditNoOfDay.Enabled = true;

                        txtAvaCommeUseAdditAvailInDay.Enabled = true;
                        rfvtxtAvaCommeUseAdditAvailInDay.Enabled = true;
                        txtAvaResiUseAdditAvailInDay.Enabled = true;
                        rfvtxtAvaResiUseAdditAvailInDay.Enabled = true;
                        txtAvaGreenbeltDevelopmentAdditAvailInDay.Enabled = true;
                        rfvtxtAvaGreenbeltDevelopmentAdditAvailInDay.Enabled = true;
                        txtAvaFlushReqAdditAvailInDay.Enabled = true;
                        rfvtxtAvaFlushReqAdditAvailInDay.Enabled = true;

                        txtEfflSewGeneratedETPSTPAdditNoOfDay.Enabled = true;
                        txtEfflAvailableTreatedforUsageAdditNoOfDay.Enabled = true;
                        txtEfflDischargeAfterTreatmentAdditNoOfDay.Enabled = true;

                        break;
                    default:
                        break;
                }

                //ScriptManager.RegisterStartupScript(this, typeof(Page), "Testing", "CalculateSumTreatedWaterUtilizedInDay();", true);
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "Testing1", "CalculateSumTreatedWaterUtilizedInYear();", true);

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
    private int UpdateRecycledWaterUsageDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);

            // for Effluent

            if (txtEfflSewGeneratedETPSTPExistInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflSewGeneratedETPSTPExistInDay.Text.Trim()))
                obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPExist = Convert.ToDecimal(txtEfflSewGeneratedETPSTPExistInDay.Text);

            if (txtEfflSewGeneratedETPSTPAdditInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflSewGeneratedETPSTPAdditInDay.Text.Trim()))
                obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPAddit = Convert.ToDecimal(txtEfflSewGeneratedETPSTPAdditInDay.Text);

            if (txtEfflAvailableTreatedforUsageExistInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflAvailableTreatedforUsageExistInDay.Text.Trim()))
                obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagExist = Convert.ToDecimal(txtEfflAvailableTreatedforUsageExistInDay.Text);

            if (txtEfflAvailableTreatedforUsageAdditInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflAvailableTreatedforUsageAdditInDay.Text.Trim()))
                obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagAddit = Convert.ToDecimal(txtEfflAvailableTreatedforUsageAdditInDay.Text);


            if (txtEfflDischargeAfterTreatmentExistInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflDischargeAfterTreatmentExistInDay.Text.Trim()))
                obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatExist = Convert.ToDecimal(txtEfflDischargeAfterTreatmentExistInDay.Text);

            if (txtEfflDischargeAfterTreatmentAdditInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflDischargeAfterTreatmentAdditInDay.Text.Trim()))
                obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatAddit = Convert.ToDecimal(txtEfflDischargeAfterTreatmentAdditInDay.Text);



            if (txtEfflSewGeneratedETPSTPExistNoOfDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflSewGeneratedETPSTPExistNoOfDay.Text.Trim()))
                obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPExistNoOfDay = Convert.ToInt32(txtEfflSewGeneratedETPSTPExistNoOfDay.Text);

            if (txtEfflSewGeneratedETPSTPAdditNoOfDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflSewGeneratedETPSTPAdditNoOfDay.Text.Trim()))
                obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPAdditNoOfDay = Convert.ToInt32(txtEfflSewGeneratedETPSTPAdditNoOfDay.Text);

            if (txtEfflAvailableTreatedforUsageExistNoOfDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflAvailableTreatedforUsageExistNoOfDay.Text.Trim()))
                obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagExistNoOfDay = Convert.ToInt32(txtEfflAvailableTreatedforUsageExistNoOfDay.Text);

            if (txtEfflAvailableTreatedforUsageAdditNoOfDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflAvailableTreatedforUsageAdditNoOfDay.Text.Trim()))
                obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagAdditNoOfDay = Convert.ToInt32(txtEfflAvailableTreatedforUsageAdditNoOfDay.Text);

            if (txtEfflDischargeAfterTreatmentExistNoOfDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflDischargeAfterTreatmentExistNoOfDay.Text.Trim()))
                obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatExistNoOfDay = Convert.ToInt32(txtEfflDischargeAfterTreatmentExistNoOfDay.Text);
            if (txtEfflDischargeAfterTreatmentAdditNoOfDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflDischargeAfterTreatmentAdditNoOfDay.Text.Trim()))
                obj_infrastructureRenewSADApplication.InfrastructureRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatAdditNoOfDay = Convert.ToInt32(txtEfflDischargeAfterTreatmentAdditNoOfDay.Text);




            //for Available

            if (txtAvaCommeUseExistInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtAvaCommeUseExistInDay.Text.Trim()))
                obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentCommercialUseExist = Convert.ToDecimal(txtAvaCommeUseExistInDay.Text);

            if (txtAvaCommeUseAdditAvailInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtAvaCommeUseAdditAvailInDay.Text.Trim()))
                obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentCommercialUseAddit = Convert.ToDecimal(txtAvaCommeUseAdditAvailInDay.Text);

            if (txtAvaResiUseExistInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtAvaResiUseExistInDay.Text.Trim()))
                obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentResidentialUseExist = Convert.ToDecimal(txtAvaResiUseExistInDay.Text);

            if (txtAvaResiUseAdditAvailInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtAvaResiUseAdditAvailInDay.Text.Trim()))
                obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentResidentialUseAddit = Convert.ToDecimal(txtAvaResiUseAdditAvailInDay.Text);

            if (txtAvaGreenbeltDevelopmentExistInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtAvaGreenbeltDevelopmentExistInDay.Text.Trim()))
                obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentGreenDevExist = Convert.ToDecimal(txtAvaGreenbeltDevelopmentExistInDay.Text);

            if (txtAvaGreenbeltDevelopmentAdditAvailInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtAvaGreenbeltDevelopmentAdditAvailInDay.Text.Trim()))
                obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentGreenDevAddit = Convert.ToDecimal(txtAvaGreenbeltDevelopmentAdditAvailInDay.Text);

            if (txtAvaFlushReqExistInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtAvaFlushReqExistInDay.Text.Trim()))
                obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentFlushingReqUseExist = Convert.ToDecimal(txtAvaFlushReqExistInDay.Text);

            if (txtAvaFlushReqAdditAvailInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtAvaFlushReqAdditAvailInDay.Text.Trim()))
                obj_infrastructureRenewSADApplication.InfrastructureRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentFlushingReqUseAddit = Convert.ToDecimal(txtAvaFlushReqAdditAvailInDay.Text);



            //ScriptManager.RegisterStartupScript(this, typeof(Page), "Testing", "CalculateSumTreatedWaterUtilizedInDay();", true);
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "Testing1", "CalculateSumTreatedWaterUtilizedInYear();", true);

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_infrastructureRenewSADApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_infrastructureRenewSADApplication.Update() == 1)
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
    protected void btnNext_Click(object sender, EventArgs e)
    {

        NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter obj_ApplicationAllowOrNotForExemptionLetter = new NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter();
        string str_RedirectPath = "";
        try
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
                    if (UpdateRecycledWaterUsageDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text)) == 1)
                    {
                        Server.Transfer("~/ExternalUser/InfrastructureRenew/DetailsExistingGroundwaterAbstractionStructure.aspx");
                    }
                }
            }
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
            //switch (str_RedirectPath)
            //{
            //    case "~/ExternalUser/IndustrialRenew/SalientFeature.aspx":
            //        Server.Transfer("~/ExternalUser/IndustrialRenew/SalientFeature.aspx");
            //        break;
            //    case "~/ExternalUser/IndustrialRenew/DetailsExistingGroundwaterAbstractionStructure.aspx":
            //        Server.Transfer("~/ExternalUser/IndustrialRenew/DetailsExistingGroundwaterAbstractionStructure.aspx");
            //        break;

            //}
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
            Server.Transfer("~/ExternalUser/InfrastructureRenew/WaterRequirementDetails.aspx");
        }

    }
    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
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
                if (Page.IsValid)
                {
                    UpdateRecycledWaterUsageDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
}