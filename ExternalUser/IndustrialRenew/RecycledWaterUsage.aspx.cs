using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Threading;

public partial class ExternalUser_IndustrialRenew_RecycledWaterUsage : System.Web.UI.Page
{
    string strPageName = "INDRenewRecycledWaterUsage";
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
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    BindRecycledWaterUsageDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
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


        //revtxtIndActDaysInYear.ValidationExpression = ValidationUtility.txtValForNumeric;
        //revtxtIndActDaysInYear.ErrorMessage = ValidationUtility.txtValForNumericMsg;


        revtxtEfflDischargeAfterTreatmentExistNoOfDay.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtEfflDischargeAfterTreatmentExistNoOfDay.ErrorMessage = ValidationUtility.txtValForNumericMsg;
        revtxtEfflDischargeAfterTreatmentAdditInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtEfflDischargeAfterTreatmentAdditInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtEfflDischargeAfterTreatmentAdditNoOfDay.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtEfflDischargeAfterTreatmentAdditNoOfDay.ErrorMessage = ValidationUtility.txtValForNumericMsg;
        revtxtAvaIndustrialActivityExistInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtAvaIndustrialActivityExistInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtAvaIndustrialActivityAdditAvailInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtAvaIndustrialActivityAdditAvailInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtAvaDomesticExistInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtAvaDomesticExistInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtAvaDomesticAdditAvailInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtAvaDomesticAdditAvailInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtAvaGreenbeltDevelopmentExistInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtAvaGreenbeltDevelopmentExistInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtAvaGreenbeltDevelopmentAdditAvailInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtAvaGreenbeltDevelopmentAdditAvailInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtAvaOtherUseExistInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtAvaOtherUseExistInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtAvaOtherUseAdditAvailInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtAvaOtherUseAdditAvailInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
    }

    private void BindRecycledWaterUsageDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);

            if (obj_industrialRenewApplication != null && obj_industrialRenewApplication.IndustrialRenewApplicationCode > 0)
            {

                lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplication.NameOfIndustry);
                txtEfflSewGeneratedETPSTPExistInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPExist);
                txtEfflSewGeneratedETPSTPAdditInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPAddit);
                txtEfflAvailableTreatedforUsageExistInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagExist);
                txtEfflAvailableTreatedforUsageAdditInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagAddit);
                txtEfflDischargeAfterTreatmentExistInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatExist);
                txtEfflDischargeAfterTreatmentAdditInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatAddit);


                txtEfflSewGeneratedETPSTPExistNoOfDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPExistNoOfDay);
                txtEfflSewGeneratedETPSTPAdditNoOfDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPAdditNoOfDay);
                txtEfflAvailableTreatedforUsageExistNoOfDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagExistNoOfDay);
                txtEfflAvailableTreatedforUsageAdditNoOfDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagAdditNoOfDay);
                txtEfflDischargeAfterTreatmentExistNoOfDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatExistNoOfDay);
                txtEfflDischargeAfterTreatmentAdditNoOfDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatAdditNoOfDay);



                //


                txtEfflSewGeneratedETPSTPExistInYear.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPExist * obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPExistNoOfDay);
                txtEfflSewGeneratedETPSTPAdditInYear.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPAddit * obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPAdditNoOfDay);
                txtEfflAvailableTreatedforUsageExistInYear.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagExist * obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagExistNoOfDay);
                txtEfflAvailableTreatedforUsageAdditInYear.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagAddit * obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagAdditNoOfDay);
                txtEfflDischargeAfterTreatmentExistInYear.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatExist * obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatExistNoOfDay);
                txtEfflDischargeAfterTreatmentAdditInYear.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatAddit * obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatAdditNoOfDay);


                //

                //txtEfflSewGeneratedETPSTPExistInYear.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPExistInYear);
                //txtEfflSewGeneratedETPSTPAdditInYear.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPAdditInYear);
                //txtEfflAvailableTreatedforUsageExistInYear.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagExistInYear);
                //txtEfflAvailableTreatedforUsageAdditInYear.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagAdditInYear);
                //txtEfflDischargeAfterTreatmentExistInYear.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatExistInYear);
                //txtEfflDischargeAfterTreatmentAdditInYear.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatAdditInYear);


                txtEfflSewGeneratedETPSTPTotalInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPExist + obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPAddit);
                txtEfflSewGeneratedETPSTPTotalInYear.Text = Convert.ToString((obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPExist * obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPExistNoOfDay) + (obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPAddit * obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPAdditNoOfDay));

                txtEfflAvailableTreatedforUsageTotalInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagExist + obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagAddit);
                txtEfflAvailableTreatedforUsageTotalInYear.Text = Convert.ToString((obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagExist * obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagExistNoOfDay) + (obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagAddit * obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagAdditNoOfDay));

                txtEfflDischargeAfterTreatmentTotalInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatExist + obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatAddit);
                txtEfflDischargeAfterTreatmentTotalInYear.Text = Convert.ToString((obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatExist * obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatExistNoOfDay) + (obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatAddit * obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatAdditNoOfDay));





                txtAvaIndustrialActivityExistInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentIndActExist);
                txtAvaIndustrialActivityAdditAvailInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentIndActAddit);
                txtAvaDomesticExistInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentDomExist);
                txtAvaDomesticAdditAvailInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentDomAddit);
                txtAvaGreenbeltDevelopmentExistInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentGreenDevExist);
                txtAvaGreenbeltDevelopmentAdditAvailInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentGreenDevAddit);
                txtAvaOtherUseExistInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentOtherUseExist);
                txtAvaOtherUseAdditAvailInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentOtherUseAddit);


                txtAvaIndustrialActivityTotalInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentIndActExist + obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentIndActAddit);
                txtAvaDomesticTotalInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentDomExist + obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentDomAddit);
                txtAvaGreenbeltDevelopmentTotalInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentGreenDevExist + obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentGreenDevAddit);
                txtAvaOtherUseTotalInDay.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentOtherUseExist + obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentOtherUseAddit);

                txtRecycleUsageActivityExitTotal.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentIndActExist
                    + obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentDomExist
                    + obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentGreenDevExist
                    + obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentOtherUseExist
                    );

                txtRecycleUsageActivityAdditTotal.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentIndActAddit
                    + obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentDomAddit
                    + obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentGreenDevAddit
                    + obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentOtherUseAddit
                    );

                txtRecycleUsageActivityTotalUseAvailTotal.Text = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentIndActExist
                    + obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentIndActAddit
                    + obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentDomExist
                    + obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentDomAddit
                    + obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentGreenDevExist
                    + obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentGreenDevAddit
                    + obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentOtherUseExist
                   + obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentOtherUseAddit
                );

                hidTotalRecycledWaterUsage.Value = Convert.ToString(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.RecycleWaterUsesAdditional
                                                                    + obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist);




                switch (obj_industrialRenewApplication.GroundWaterUtilizationFor)
                {
                    case NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication.GroundWaterUtilizationForOption.ExistingGroundWater:

                        txtEfflSewGeneratedETPSTPAdditInDay.Enabled = false;
                        rfvtxtEfflSewGeneratedETPSTPAdditInDay.Enabled = false;
                        //txtEfflSewGeneratedETPSTPAdditInYear.Enabled = false;
                        rfvtxtEfflSewGeneratedETPSTPAdditNoOfDay.Enabled = false;
                        txtEfflAvailableTreatedforUsageAdditInDay.Enabled = false;
                        rfvtxtEfflAvailableTreatedforUsageAdditInDay.Enabled = false;
                        //txtEfflAvailableTreatedforUsageAdditInYear.Enabled = false;
                        rfvtxtEfflAvailableTreatedforUsageAdditNoOfDay.Enabled = false;
                        txtEfflDischargeAfterTreatmentAdditInDay.Enabled = false;
                        rfvtxtEfflDischargeAfterTreatmentAdditInDay.Enabled = false;
                        //txtEfflDischargeAfterTreatmentAdditInYear.Enabled = false;
                        rfvtxtEfflDischargeAfterTreatmentAdditNoOfDay.Enabled = false;


                        txtAvaIndustrialActivityAdditAvailInDay.Enabled = false;
                        rfvtxtAvaIndustrialActivityAdditAvailInDay.Enabled = false;
                        txtAvaDomesticAdditAvailInDay.Enabled = false;
                        rfvtxtAvaDomesticAdditAvailInDay.Enabled = false;
                        txtAvaGreenbeltDevelopmentAdditAvailInDay.Enabled = false;
                        rfvtxtAvaGreenbeltDevelopmentAdditAvailInDay.Enabled = false;
                        txtAvaOtherUseAdditAvailInDay.Enabled = false;
                        rfvtxtAvaOtherUseAdditAvailInDay.Enabled = false;
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

                        txtAvaIndustrialActivityAdditAvailInDay.Text = "0";
                        txtAvaDomesticAdditAvailInDay.Text = "0";
                        txtAvaGreenbeltDevelopmentAdditAvailInDay.Text = "0";
                        txtAvaOtherUseAdditAvailInDay.Text = "0";
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
                    case NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication.GroundWaterUtilizationForOption.ExistingGWWithAdditionalGWRequirment:

                        txtEfflSewGeneratedETPSTPAdditInDay.Enabled = true;
                        rfvtxtEfflSewGeneratedETPSTPAdditInDay.Enabled = true;
                        //txtEfflSewGeneratedETPSTPAdditInYear.Enabled = true;
                        rfvtxtEfflSewGeneratedETPSTPAdditNoOfDay.Enabled = true;
                        txtEfflAvailableTreatedforUsageAdditInDay.Enabled = true;
                        rfvtxtEfflAvailableTreatedforUsageAdditInDay.Enabled = true;
                        //txtEfflAvailableTreatedforUsageAdditInYear.Enabled = true;
                        rfvtxtEfflAvailableTreatedforUsageAdditNoOfDay.Enabled = true;
                        txtEfflDischargeAfterTreatmentAdditInDay.Enabled = true;
                        rfvtxtEfflDischargeAfterTreatmentAdditInDay.Enabled = true;
                        //txtEfflDischargeAfterTreatmentAdditInYear.Enabled = true;
                        rfvtxtEfflDischargeAfterTreatmentAdditNoOfDay.Enabled = true;


                        txtAvaIndustrialActivityAdditAvailInDay.Enabled = true;
                        rfvtxtAvaIndustrialActivityAdditAvailInDay.Enabled = true;
                        txtAvaDomesticAdditAvailInDay.Enabled = true;
                        rfvtxtAvaDomesticAdditAvailInDay.Enabled = true;
                        txtAvaGreenbeltDevelopmentAdditAvailInDay.Enabled = true;
                        rfvtxtAvaGreenbeltDevelopmentAdditAvailInDay.Enabled = true;
                        txtAvaOtherUseAdditAvailInDay.Enabled = true;
                        rfvtxtAvaOtherUseAdditAvailInDay.Enabled = true;


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
                lblMessage.Text = "Reeor on Page";
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
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);


            // for Effluent

            if (txtEfflSewGeneratedETPSTPExistInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflSewGeneratedETPSTPExistInDay.Text.Trim()))
                obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPExist = Convert.ToDecimal(txtEfflSewGeneratedETPSTPExistInDay.Text);

            if (txtEfflSewGeneratedETPSTPAdditInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflSewGeneratedETPSTPAdditInDay.Text.Trim()))
                obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPAddit = Convert.ToDecimal(txtEfflSewGeneratedETPSTPAdditInDay.Text);


            if (txtEfflAvailableTreatedforUsageExistInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflAvailableTreatedforUsageExistInDay.Text.Trim()))
                obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagExist = Convert.ToDecimal(txtEfflAvailableTreatedforUsageExistInDay.Text);

            if (txtEfflAvailableTreatedforUsageAdditInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflAvailableTreatedforUsageAdditInDay.Text.Trim()))
                obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagAddit = Convert.ToDecimal(txtEfflAvailableTreatedforUsageAdditInDay.Text);



            if (txtEfflDischargeAfterTreatmentExistInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflDischargeAfterTreatmentExistInDay.Text.Trim()))
                obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatExist = Convert.ToDecimal(txtEfflDischargeAfterTreatmentExistInDay.Text);

            if (txtEfflDischargeAfterTreatmentAdditInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflDischargeAfterTreatmentAdditInDay.Text.Trim()))
                obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatAddit = Convert.ToDecimal(txtEfflDischargeAfterTreatmentAdditInDay.Text);



            if (txtEfflSewGeneratedETPSTPExistNoOfDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflSewGeneratedETPSTPExistNoOfDay.Text.Trim()))
                obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPExistNoOfDay = Convert.ToInt32(txtEfflSewGeneratedETPSTPExistNoOfDay.Text);

            if (txtEfflSewGeneratedETPSTPAdditNoOfDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflSewGeneratedETPSTPAdditNoOfDay.Text.Trim()))
                obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPAdditNoOfDay = Convert.ToInt32(txtEfflSewGeneratedETPSTPAdditNoOfDay.Text);

            if (txtEfflAvailableTreatedforUsageExistNoOfDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflAvailableTreatedforUsageExistNoOfDay.Text.Trim()))
                obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagExistNoOfDay = Convert.ToInt32(txtEfflAvailableTreatedforUsageExistNoOfDay.Text);

            if (txtEfflAvailableTreatedforUsageAdditNoOfDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflAvailableTreatedforUsageAdditNoOfDay.Text.Trim()))
                obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagAdditNoOfDay = Convert.ToInt32(txtEfflAvailableTreatedforUsageAdditNoOfDay.Text);

            if (txtEfflDischargeAfterTreatmentExistNoOfDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflDischargeAfterTreatmentExistNoOfDay.Text.Trim()))
                obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatExistNoOfDay = Convert.ToInt32(txtEfflDischargeAfterTreatmentExistNoOfDay.Text);
            if (txtEfflDischargeAfterTreatmentAdditNoOfDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflDischargeAfterTreatmentAdditNoOfDay.Text.Trim()))
                obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatAdditNoOfDay = Convert.ToInt32(txtEfflDischargeAfterTreatmentAdditNoOfDay.Text);



            //if (txtEfflSewGeneratedETPSTPExistInYear.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflSewGeneratedETPSTPExistInYear.Text.Trim()))
            //    obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPExistInYear = Convert.ToDecimal(txtEfflSewGeneratedETPSTPExistInYear.Text);

            //if (txtEfflSewGeneratedETPSTPAdditInYear.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflSewGeneratedETPSTPAdditInYear.Text.Trim()))
            //    obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageInETPSTPAdditInYear = Convert.ToDecimal(txtEfflSewGeneratedETPSTPAdditInYear.Text);

            //if (txtEfflAvailableTreatedforUsageExistInYear.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflAvailableTreatedforUsageExistInYear.Text.Trim()))
            //    obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagExistInYear = Convert.ToDecimal(txtEfflAvailableTreatedforUsageExistInYear.Text);

            //if (txtEfflAvailableTreatedforUsageAdditInYear.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflAvailableTreatedforUsageAdditInYear.Text.Trim()))
            //    obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentAvailableTreatSewUsagAdditInYear = Convert.ToDecimal(txtEfflAvailableTreatedforUsageAdditInYear.Text);

            //if (txtEfflDischargeAfterTreatmentExistInYear.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflDischargeAfterTreatmentExistInYear.Text.Trim()))
            //    obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatExistInYear = Convert.ToDecimal(txtEfflDischargeAfterTreatmentExistInYear.Text);
            //if (txtEfflDischargeAfterTreatmentAdditInYear.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtEfflDischargeAfterTreatmentAdditInYear.Text.Trim()))
            //    obj_industrialRenewApplication.IndustrialRenewBreakupOFRecycleWater.EffluentSewerageDischargeAfterTreatAdditInYear = Convert.ToDecimal(txtEfflDischargeAfterTreatmentAdditInYear.Text);




            //for Available

            if (txtAvaIndustrialActivityExistInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtAvaIndustrialActivityExistInDay.Text.Trim()))
                obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentIndActExist = Convert.ToDecimal(txtAvaIndustrialActivityExistInDay.Text);

            if (txtAvaIndustrialActivityAdditAvailInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtAvaIndustrialActivityAdditAvailInDay.Text.Trim()))
                obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentIndActAddit = Convert.ToDecimal(txtAvaIndustrialActivityAdditAvailInDay.Text);

            if (txtAvaDomesticExistInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtAvaDomesticExistInDay.Text.Trim()))
                obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentDomExist = Convert.ToDecimal(txtAvaDomesticExistInDay.Text);
            if (txtAvaDomesticAdditAvailInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtAvaDomesticAdditAvailInDay.Text.Trim()))
                obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentDomAddit = Convert.ToDecimal(txtAvaDomesticAdditAvailInDay.Text);

            if (txtAvaGreenbeltDevelopmentExistInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtAvaGreenbeltDevelopmentExistInDay.Text.Trim()))
                obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentGreenDevExist = Convert.ToDecimal(txtAvaGreenbeltDevelopmentExistInDay.Text);

            if (txtAvaGreenbeltDevelopmentAdditAvailInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtAvaGreenbeltDevelopmentAdditAvailInDay.Text.Trim()))
                obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentGreenDevAddit = Convert.ToDecimal(txtAvaGreenbeltDevelopmentAdditAvailInDay.Text);
            if (txtAvaOtherUseExistInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtAvaOtherUseExistInDay.Text.Trim()))
                obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentOtherUseExist = Convert.ToDecimal(txtAvaOtherUseExistInDay.Text);

            if (txtAvaOtherUseAdditAvailInDay.Text.Trim() != "" && NOCAPExternalUtility.IsNumeric(txtAvaOtherUseAdditAvailInDay.Text.Trim()))
                obj_industrialRenewApplication.IndustrialRenewAvailableTreatedEffluentWater.AvailableTreatedEffluentOtherUseAddit = Convert.ToDecimal(txtAvaOtherUseAdditAvailInDay.Text);



            //ScriptManager.RegisterStartupScript(this, typeof(Page), "Testing", "CalculateSumTreatedWaterUtilizedInDay();", true);
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "Testing1", "CalculateSumTreatedWaterUtilizedInYear();", true);

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
                    if (UpdateRecycledWaterUsageDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text)) == 1)
                    {

                        Server.Transfer("~/ExternalUser/IndustrialRenew/DetailsExistingGroundwaterAbstractionStructure.aspx");
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
            Server.Transfer("~/ExternalUser/IndustrialRenew/WaterRequirementDetails.aspx");
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

                    UpdateRecycledWaterUsageDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }


}