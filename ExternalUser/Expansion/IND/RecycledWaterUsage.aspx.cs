using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_Expansion_IND_RecycledWaterUsage : System.Web.UI.Page
{
    string strPageName = "RecycledWaterUsage";
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
                    BindRecycledWaterUsageDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {
                //lblMessage.Text = ex.Message;
                //lblMessage.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }

    private void ValidationExpInit()
    {
        revtxtTotalwasteWaterInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtTotalwasteWaterInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtQtyReuseIndustrialInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtQtyReuseIndustrialInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtQtyReuseGBDevInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtQtyReuseGBDevInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtOtherUsesInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtOtherUsesInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
    }

    private void BindRecycledWaterUsageDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(lngA_ApplicationCode);


            txtNetGroundWaterRequirement.Text = HttpUtility.HtmlEncode(Convert.ToDecimal(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement) + Convert.ToDecimal(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));

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
            hidtxtGroundWaterRequirement.Value = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement));
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Testing", "CalculateSumTreatedWaterUtilizedInDay();", true);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Testing1", "CalculateSumTreatedWaterUtilizedInYear();", true);
           

        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
            //lblMessage.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    private int UpdateRecycledWaterUsageDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(lngA_ApplicationCode);


            //if (obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == (Convert.ToDecimal(txtNetGroundWaterRequirement.Text) + Convert.ToDecimal(txtQuantityReuseIndustrialActivityInDay.Text) + Convert.ToDecimal(txtQuantityReuseGreenBeltDevelInDay.Text) + Convert.ToDecimal(txtOtherUsesInDay.Text)))
            //{
            if (txtTotalwasteWaterGeneratedInDay.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedInDay = null;
            }
            else
            {
                if (!(NOCAPExternalUtility.IsNumeric(txtTotalwasteWaterGeneratedInDay.Text.Trim())))
                {
                }
                else
                {
                    obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedInDay = Convert.ToDecimal(txtTotalwasteWaterGeneratedInDay.Text.Trim());
                }
            }
            if (txtTotalwasteWaterGeneratedNoOfDay.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedNoOfDay = null;
            }
            else
            {
                if (!(NOCAPExternalUtility.IsNumeric(txtTotalwasteWaterGeneratedNoOfDay.Text.Trim())))
                {
                }
                else
                {
                    obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedNoOfDay = Convert.ToInt32(txtTotalwasteWaterGeneratedNoOfDay.Text.Trim());
                }
            }
            obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedInYear = Convert.ToDecimal(txtTotalwasteWaterGeneratedInDay.Text.Trim()) * Convert.ToDecimal(txtTotalwasteWaterGeneratedNoOfDay.Text.Trim());

            if (txtQuantityReuseIndustrialActivityInDay.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseIndustrialActivityInDay = null;
            }
            else
            {
                if (!(NOCAPExternalUtility.IsNumeric(txtQuantityReuseIndustrialActivityInDay.Text.Trim())))
                {
                }
                else
                {
                    obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseIndustrialActivityInDay = Convert.ToDecimal(txtQuantityReuseIndustrialActivityInDay.Text.Trim());
                }
            }

            obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseIndustrialActivityNoOfDay = obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_NoOfOperationdaysInAYear;
            obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseIndustrialActivityInYear = Convert.ToDecimal(txtQuantityReuseIndustrialActivityInDay.Text.Trim()) * Convert.ToDecimal(txtQuantityReuseIndustrialActivityNoOfDay.Text.Trim());
            
            if (txtQuantityReuseGreenBeltDevelInDay.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseGreenBeltDevelopmentInDay = null;
            }
            else
            {
                if (!(NOCAPExternalUtility.IsNumeric(txtQuantityReuseGreenBeltDevelInDay.Text.Trim())))
                {
                }
                else
                {
                    obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseGreenBeltDevelopmentInDay = Convert.ToDecimal(txtQuantityReuseGreenBeltDevelInDay.Text.Trim());
                }
            }
            obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseGreenBeltDevelopmentNoOfDay = obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_NoOfOperationdaysInAYear ;
            obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseGreenBeltDevelopmentInYear = Convert.ToDecimal(txtQuantityReuseGreenBeltDevelInDay.Text.Trim()) * Convert.ToDecimal(txtQuantityReuseGreenBeltDevelNoOfDay.Text.Trim());

            if (txtOtherUsesInDay.Text.Trim() == "")
            {
                obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInDay = null;
            }
            else
            {
                if (!(NOCAPExternalUtility.IsNumeric(txtOtherUsesInDay.Text.Trim())))
                {
                }
                else
                {
                    obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInDay = Convert.ToDecimal(txtOtherUsesInDay.Text.Trim());
                }
            }
            obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseNoOfDay = obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_NoOfOperationdaysInAYear;
            obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInYear = Convert.ToDecimal(txtOtherUsesInDay.Text.Trim()) * Convert.ToDecimal(txtOtherUsesNoOfDay.Text.Trim());
            
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Testing", "CalculateSumTreatedWaterUtilizedInDay();", true);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Testing1", "CalculateSumTreatedWaterUtilizedInYear();", true);

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
            // }
            //else
            // {

            //ScriptManager.RegisterStartupScript(this, typeof(Page), "Testing", "CalculateSumTreatedWaterUtilizedInDay();", true);
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "Testing1", "CalculateSumTreatedWaterUtilizedInYear();", true);

            //lblMessage.Text = "Net Ground Water Requirement is not Matched with  Ground Water Requirement";
            //lblMessage.ForeColor = System.Drawing.Color.Red;
            //return 0;
            // }
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
    protected void btnNext_Click(object sender, EventArgs e)
    {
        
        NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter obj_ApplicationAllowOrNotForExemptionLetter = new NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter();
        string str_RedirectPath = "";
        try
        {
            if (Page.IsValid)
            {
                if (txtNetGroundWaterRequirement.Text.Trim() != "" && !(NOCAPExternalUtility.IsNumeric(txtNetGroundWaterRequirement.Text.Trim())))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('NetGroundWaterRequirement allows Numeric ');", true);
                    return ;
                }

                if (txtQuantityReuseIndustrialActivityInDay.Text.Trim() != "" && !(NOCAPExternalUtility.IsNumeric(txtQuantityReuseIndustrialActivityInDay.Text.Trim())))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert( 'Reuse in Industrial Activity allows Numeric ');", true);
                    return;
                }

                if (txtQuantityReuseGreenBeltDevelInDay.Text.Trim() != "" && !(NOCAPExternalUtility.IsNumeric(txtQuantityReuseGreenBeltDevelInDay.Text.Trim())))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert( 'Reuse for Green Belt Development ');", true);
                    return;
                }

                if (txtOtherUsesInDay.Text.Trim() != "" && !(NOCAPExternalUtility.IsNumeric(txtOtherUsesInDay.Text.Trim())))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert( 'OtherUses InDay allows Numeric ');", true);
                    return;
                }


                if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                {
                    Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
                }
                else
                {

                    hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                    Session["CSRF"] = hidCSRF.Value;
                    if (UpdateRecycledWaterUsageDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text)) == 1)
                    {

                        NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        int int_stateCode = obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode;
                        int int_districtCode = obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode;
                        int int_subDistrictCode = obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode;
                        int int_applicationTypeCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Industrial");
                        int int_applicationPurposeCode = NOCAPExternalUtility.EquivalentApplicationPurposeCodeOfDatabaseForApplicationPurpose("New");
                        int int_applicationTypeCategoryCode = obj_industrialNewApplication.ApplicationTypeCategoryCode;
                        int int_WaterQualityTypeCode = obj_industrialNewApplication.WaterQualityCode;
                        decimal? dec_gwRequirement = obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement;
                        //decimal? dec_netGroundWaterRequirement = (obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement) - ((obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseIndustrialActivityInDay == null ? 0 : obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseIndustrialActivityInDay) + (obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseGreenBeltDevelopmentInDay == null ? 0 : obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseGreenBeltDevelopmentInDay) + (obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInDay == null ? 0 : obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInDay));
                        decimal? dec_netGroundWaterRequirement = (obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement) + (obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist);
                        obj_ApplicationAllowOrNotForExemptionLetter = new NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter(int_applicationTypeCode, int_applicationPurposeCode, int_applicationTypeCategoryCode, int_stateCode, int_districtCode, int_subDistrictCode, dec_netGroundWaterRequirement, int_WaterQualityTypeCode);


                        if (obj_industrialNewApplication.MSME == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.MSMEYesNo.Yes && dec_netGroundWaterRequirement < 10 && obj_industrialNewApplication.MSMETypeCode != 3)// && ddlWaterqty.SelectedValue.ToString() == "1")
                        {
                            str_RedirectPath = "~/ExternalUser/Expansion/IND/SalientFeature.aspx";

                        }
                        else
                        {
                            str_RedirectPath = "~/ExternalUser/Expansion/IND/DetailsExistingGroundwaterAbstractionStructure.aspx";

                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally
        {

            switch (str_RedirectPath)
            {
                case "~/ExternalUser/Expansion/IND/SalientFeature.aspx":
                    Server.Transfer("~/ExternalUser/Expansion/IND/SalientFeature.aspx");
                    break;
                case "~/ExternalUser/Expansion/IND/DetailsExistingGroundwaterAbstractionStructure.aspx":
                    Server.Transfer("~/ExternalUser/Expansion/IND/DetailsExistingGroundwaterAbstractionStructure.aspx");
                    break;

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
            Server.Transfer("~/ExternalUser/Expansion/IND/WaterRequirementDetails.aspx");
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
                    if (txtNetGroundWaterRequirement.Text.Trim() != "" && !(NOCAPExternalUtility.IsNumeric(txtNetGroundWaterRequirement.Text.Trim())))
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('NetGroundWaterRequirement allows Numeric ');", true);
                        return;
                    }

                    if (txtQuantityReuseIndustrialActivityInDay.Text.Trim() != "" && !(NOCAPExternalUtility.IsNumeric(txtQuantityReuseIndustrialActivityInDay.Text.Trim())))
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert( 'Reuse in Industrial Activity allows Numeric ');", true);
                        return;
                    }

                    if (txtQuantityReuseGreenBeltDevelInDay.Text.Trim() != "" && !(NOCAPExternalUtility.IsNumeric(txtQuantityReuseGreenBeltDevelInDay.Text.Trim())))
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert( 'Reuse for Green Belt Development ');", true);
                        return;
                    }

                    if (txtOtherUsesInDay.Text.Trim() != "" && !(NOCAPExternalUtility.IsNumeric(txtOtherUsesInDay.Text.Trim())))
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert( 'OtherUses InDay allows Numeric ');", true);
                        return;
                    }

                    UpdateRecycledWaterUsageDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    //public void CalculateSumTreatedWaterUtilizedInDay()
    //{
    //    int Val1;
    //    int Val2;
    //    int Val3;
    //    int Total;
    //    //if ((!string.IsNullOrEmpty(txtQuantityReuseIndustrialActivityInDay.Text) && (!string.IsNullOrEmpty(txtQuantityReuseGreenBeltDevelInDay.Text)) && (!string.IsNullOrEmpty(txtOtherUsesInDay.Text))))
    //    //{
    //    //    Total = Convert.ToInt32(txtQuantityReuseIndustrialActivityInDay.Text) + Convert.ToInt32(txtQuantityReuseGreenBeltDevelInDay.Text) + Convert.ToInt32(txtOtherUsesInDay.Text);
    //    //    txtTotalTreatedWaterUtilizedInDay.Text = Convert.ToString(Total);
    //    //}

    //    //if (!(NOCAPExternalUtility.IsNumeric(txtQuantityReuseIndustrialActivityInDay.Text.Trim())))
    //    //{
    //    //    Response.Write("Not Numeric");
    //    //}
    //    try
    //    {

    //        if (txtQuantityReuseIndustrialActivityInDay.Text.Trim() == "")
    //        {
    //            Val1 = 0;
    //        }
    //        else
    //        {
    //            if (!(NOCAPExternalUtility.IsNumeric(txtQuantityReuseIndustrialActivityInDay.Text.Trim())))
    //            {
    //                Val1 = 0;
    //            }
    //            else
    //            {
    //                Val1 = Convert.ToInt32(txtQuantityReuseIndustrialActivityInDay.Text.Trim());
    //            }

    //        }
    //        if (txtQuantityReuseGreenBeltDevelInDay.Text.Trim() == "")
    //        {
    //            Val2 = 0;
    //        }
    //        else
    //        {
    //            if (!(NOCAPExternalUtility.IsNumeric(txtQuantityReuseGreenBeltDevelInDay.Text.Trim())))
    //            {
    //                Val2 = 0;
    //            }
    //            else
    //            {
    //                Val2 = Convert.ToInt32(txtQuantityReuseGreenBeltDevelInDay.Text.Trim());
    //            }

    //        }
    //        if (txtOtherUsesInDay.Text.Trim() == "")
    //        {
    //            Val3 = 0;
    //        }
    //        else
    //        {

    //            if (!(NOCAPExternalUtility.IsNumeric(txtOtherUsesInDay.Text.Trim())))
    //            {
    //                Val3 = 0;
    //            }
    //            else
    //            {
    //                Val3 = Convert.ToInt32(txtOtherUsesInDay.Text.Trim());
    //            }
    //        }
    //        Total = Val1 + Val2 + Val3;
    //        txtTotalTreatedWaterUtilizedInDay.Text = Convert.ToString(Total);
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.Text = ex.Message;
    //        lblMessage.ForeColor = System.Drawing.Color.Red;
    //    }
    //    finally
    //    {
    //    }
    //}
    //protected void txtQuantityReuseIndustrialActivityInDay_TextChanged(object sender, EventArgs e)
    //{
    //    CalculateSumTreatedWaterUtilizedInDay();
    //}
    //protected void txtQuantityReuseGreenBeltDevelInDay_TextChanged(object sender, EventArgs e)
    //{
    //    CalculateSumTreatedWaterUtilizedInDay();
    //}
    //protected void txtOtherUsesInDay_TextChanged(object sender, EventArgs e)
    //{
    //    CalculateSumTreatedWaterUtilizedInDay();
    //}
    //public void CalculateSumTreatedWaterUtilizedInYear()
    //{
    //    int Val1;
    //    int Val2;
    //    int Val3;
    //    int Total;

    //    try
    //    {

    //        if (txtQuantityReuseIndustrialActivityInYear.Text.Trim() == "")
    //        {
    //            Val1 = 0;
    //        }
    //        else
    //        {
    //            if (!(NOCAPExternalUtility.IsNumeric(txtQuantityReuseIndustrialActivityInYear.Text.Trim())))
    //            {
    //                Val1 = 0;
    //            }
    //            else
    //            {
    //                Val1 = Convert.ToInt32(txtQuantityReuseIndustrialActivityInYear.Text.Trim());
    //            }

    //        }
    //        if (txtQuantityReuseGreenBeltDevelInYear.Text.Trim() == "")
    //        {
    //            Val2 = 0;
    //        }
    //        else
    //        {
    //            if (!(NOCAPExternalUtility.IsNumeric(txtQuantityReuseGreenBeltDevelInYear.Text.Trim())))
    //            {
    //                Val2 = 0;
    //            }
    //            else
    //            {
    //                Val2 = Convert.ToInt32(txtQuantityReuseGreenBeltDevelInYear.Text.Trim());
    //            }

    //        }
    //        if (txtOtherUsesInYear.Text.Trim() == "")
    //        {
    //            Val3 = 0;
    //        }
    //        else
    //        {

    //            if (!(NOCAPExternalUtility.IsNumeric(txtOtherUsesInYear.Text.Trim())))
    //            {
    //                Val3 = 0;
    //            }
    //            else
    //            {
    //                Val3 = Convert.ToInt32(txtOtherUsesInYear.Text.Trim());
    //            }
    //        }
    //        Total = Val1 + Val2 + Val3;
    //        txtTotalTreatedWaterUtilizedInYear.Text = Convert.ToString(Total);
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.Text = ex.Message;
    //        lblMessage.ForeColor = System.Drawing.Color.Red;
    //    }
    //    finally
    //    {
    //    }
    //}
    //protected void txtQuantityReuseIndustrialActivityInYear_TextChanged(object sender, EventArgs e)
    //{
    //    CalculateSumTreatedWaterUtilizedInYear();
    //}

    //protected void txtQuantityReuseGreenBeltDevelInYear_TextChanged(object sender, EventArgs e)
    //{
    //    CalculateSumTreatedWaterUtilizedInYear();
    //}

    //protected void txtOtherUsesInYear_TextChanged(object sender, EventArgs e)
    //{
    //    CalculateSumTreatedWaterUtilizedInYear();
    //}

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
}