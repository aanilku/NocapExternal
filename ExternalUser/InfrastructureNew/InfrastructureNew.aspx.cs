using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_InfrastructureNew_InfrastructureNew : System.Web.UI.Page
{
    string strPageName = "InfrastructureNew";
    string strActionName = "";
    string strStatus = "";
    //string category = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            // NOCAPExternalUtility.MsgBox("You are being redirected to National Single Window Sysytem (NSWS). Please apply through NSWS.");

            if (!IsPostBack)
            {
                txtDOB_CalendarExtender.EndDate = System.DateTime.Now;
                txtDateOfExpansion_CalendarExtender.EndDate = System.DateTime.Now;
                ValidationExpInit();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
                try
                {
                    if (PreviousPage != null)//For Mode
                    {
                        Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                        if (placeHolder != null)
                        {
                            Label SourceLabel = (Label)placeHolder.FindControl("lblMode");
                            if (SourceLabel != null) { lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); }
                            Label SourceLabelPreviousPage = (Label)placeHolder.FindControl("lblModeFrom");
                            if (SourceLabelPreviousPage != null) { lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabelPreviousPage.Text); }
                        }
                    }
                    if (PreviousPage != null)//For Page Title
                    {
                        Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                        if (placeHolder != null)
                        {
                            Label SourceLabel = (Label)placeHolder.FindControl("lblPageTitle");
                            if (SourceLabel != null) { lblPageTitleFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); }
                        }
                    }
                    if (PreviousPage != null)//For Application Code
                    {
                        Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                        if (placeHolder != null)
                        {
                            Label SourceLabel = (Label)placeHolder.FindControl("lblApplicationCode");
                            if (SourceLabel != null) { lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); }
                            Label SourceLabelPreviousPage = (Label)placeHolder.FindControl("lblInfrastructureApplicationCodeFrom");
                            if (SourceLabelPreviousPage != null) { lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabelPreviousPage.Text); }
                        }
                    }

                    if (NOCAPExternalUtility.FillDropDownMSMEType(ref ddMSMEType) != 1)
                    {
                        lblMessage.Text = "Problem in MSME Type";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
                }

                if (NOCAPExternalUtility.FillDropDownApplicationTypeCategory(ref ddlApplicationTypeCategory, NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Infrastructure")) != 1)
                {
                    lblMessage.Text = "Problem in Application Type Category population";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                if (NOCAPExternalUtility.FillDropDownState(ref ddlState, NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes) != 1)
                {
                    lblMessage.Text = "Problem in state population";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                if (NOCAPExternalUtility.FillDropDownWaterQuality(ref ddlWaterQualityType, NOCAP.BLL.Master.WaterQuality.VisibilityYesNo.Yes) != 1)
                {
                    lblMessage.Text = "Problem in Water Quality";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }

                if (lblInfrastructureApplicationCodeFrom.Text.Trim() != "")
                {
                    BindGeneralInformationLocationDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text), sender, e);
                }
                ddlTownOrVillage_SelectedIndexChanged(sender, e);
            }

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void ValidationExpInit()
    {
        revtxtNameOfInfraStructure.ValidationExpression = ValidationUtility.txtValForProjectName;
        revtxtNameOfInfraStructure.ErrorMessage = ValidationUtility.txtValForProjectNameMsg;

        revtxtAddressLine1.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtAddressLine1.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revtxtAddressLine2.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtAddressLine2.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revtxtAddressLine3.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtAddressLine3.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //revtxtDateOfCommencement.ValidationExpression = ValidationUtility.txtValForDate;
        //revtxtDateOfCommencement.ErrorMessage = ValidationUtility.txtValForDateMsg;

        //revtxtDateOfExpansion.ValidationExpression = ValidationUtility.txtValForDate;
        //revtxtDateOfExpansion.ErrorMessage = ValidationUtility.txtValForDateMsg;

        revtxtProLat.ValidationExpression = ValidationUtility.txtValForDecimalValue("10", "6");
        revtxtProLat.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("10", "6");

        revtxtProLong.ValidationExpression = ValidationUtility.txtValForDecimalValue("10", "6");
        revtxtProLong.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("10", "6");
    }
    protected void ValidateDate(object sender, ServerValidateEventArgs e)
    {
        if (NOCAPExternalUtility.IsValidDate(e.Value))
        {
            e.IsValid = true;
        }
        else
        {
            e.IsValid = false;
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            int intStateCode;
            try
            {
                lblMessage.Text = "";
                ddlDistrict.Items.Clear();
                ddlSubDistrict.Items.Clear();
                ddlTownOrVillage.SelectedValue = "";
                ddlVillage.Items.Clear();
                ddlTown.Items.Clear();
                if (ddlState.SelectedValue == "") { NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlDistrict); }
                else
                {
                    intStateCode = Convert.ToInt32(ddlState.SelectedValue);
                    if (NOCAPExternalUtility.FillDropDownDistrict(ref ddlDistrict, intStateCode) != 1)
                    {
                        lblMessage.Text = "Problem in district population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            int int_StateCode;
            int int_DistricCode;
            try
            {
                lblMessage.Text = "";
                ddlSubDistrict.Items.Clear();
                ddlTownOrVillage.SelectedValue = "";
                ddlVillage.Items.Clear();
                ddlTown.Items.Clear();
                if (ddlDistrict.SelectedValue == "") { NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlSubDistrict); }
                else
                {
                    int_DistricCode = Convert.ToInt32(ddlDistrict.SelectedValue);
                    int_StateCode = Convert.ToInt32(ddlState.SelectedValue);
                    if (NOCAPExternalUtility.FillDropDownSubDistrict(ref ddlSubDistrict, int_DistricCode, int_StateCode) != 1)
                    {
                        lblMessage.Text = "Problem in Subdistrict population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void ddlSubDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            int int_StateCode;
            int int_DistricCode;
            int int_SubDistrictCode;
            try
            {
                lblMessage.Text = "";
                ddlTownOrVillage.SelectedValue = "";
                ddlVillage.Items.Clear();
                ddlTown.Items.Clear();
                if (ddlSubDistrict.SelectedValue == "")
                {
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlVillage);
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlTown);
                }
                else
                {
                    int_SubDistrictCode = Convert.ToInt32(ddlSubDistrict.SelectedValue);
                    int_DistricCode = Convert.ToInt32(ddlDistrict.SelectedValue);
                    int_StateCode = Convert.ToInt32(ddlState.SelectedValue);
                    if (NOCAPExternalUtility.FillDropDownTownOrVillage(ref ddlTownOrVillage) != 1)
                    {
                        lblMessage.Text = "Problem in Villlage/Town population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    if (NOCAPExternalUtility.FillDropDownVillage(ref ddlVillage, int_SubDistrictCode, int_DistricCode, int_StateCode) != 1)
                    {
                        lblMessage.Text = "Problem in Village population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    if (NOCAPExternalUtility.FillDropDownTown(ref ddlTown, int_SubDistrictCode, int_DistricCode, int_StateCode) != 1)
                    {
                        lblMessage.Text = "Problem in Town population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {

        if (Page.IsValid)
        {
            if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
                if (CheckAndAddAndUpdateInfrastructureApplication() == 1)
                {
                    lblMessage.Text = "Successfully Saved";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    //lblMessage.Text = "Record saving failed !";
                    //lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    private int CheckAndAddAndUpdateInfrastructureApplication()
    {
        int int_applicationTypeCategoryCode, int_stateCode, int_districtCode, int_subDistrictCode;
        if (NOCAPExternalUtility.IsNumeric(ddlApplicationTypeCategory.SelectedValue)) { int_applicationTypeCategoryCode = Convert.ToInt32(ddlApplicationTypeCategory.SelectedValue); } else { return 0; }
        if (NOCAPExternalUtility.IsNumeric(ddlState.SelectedValue)) { int_stateCode = Convert.ToInt32(ddlState.SelectedValue); } else { return 0; }
        if (NOCAPExternalUtility.IsNumeric(ddlDistrict.SelectedValue)) { int_districtCode = Convert.ToInt32(ddlDistrict.SelectedValue); } else { return 0; }
        if (NOCAPExternalUtility.IsNumeric(ddlSubDistrict.SelectedValue)) { int_subDistrictCode = Convert.ToInt32(ddlSubDistrict.SelectedValue); } else { return 0; }
        int int_applicationTypeCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Infrastructure");
        int int_applicationPurposeCode = NOCAPExternalUtility.EquivalentApplicationPurposeCodeOfDatabaseForApplicationPurpose("New");
        int int_WaterQualityTypeCode = Convert.ToInt32(ddlWaterQualityType.SelectedValue);

        NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption enm_NOCObtainForExistIND = new NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption();
        switch (ddlNOCObtainedForExistIND.SelectedValue)
        {
            case "Y":
                enm_NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.Yes;
                break;
            case "N":
                enm_NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No;
                break;
            default:
                enm_NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                break;
        }

        NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption enm_WhetherGroundWaterUtilizationFor = new NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption();

        switch (rbtnWhetherGroundWaterUtilization.SelectedValue)
        {
            case "NewIndustry":
                enm_WhetherGroundWaterUtilizationFor = NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.NewIndustry;
                break;
            case "ExistingIndustry":
                enm_WhetherGroundWaterUtilizationFor = NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry;
                break;
            case "ExpansionProgramExistingIndustry":
                enm_WhetherGroundWaterUtilizationFor = NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExpansionProgramOfExitingIndustry;
                break;
            default:
                enm_WhetherGroundWaterUtilizationFor = NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.NotDefined;
                break;
        }

        DateTime? dt_DateOfCommExistIND = null;
        if (txtDateOfCommencement.Text != "")
        {
            dt_DateOfCommExistIND = Convert.ToDateTime(txtDateOfCommencement.Text);
        }

        DateTime? dt_DateOfExpansionIND = null;
        if (txtDateOfExpansion.Text != "")
        {
            dt_DateOfExpansionIND = Convert.ToDateTime(txtDateOfExpansion.Text);
        }


        NOCAP.BLL.Master.ApplicationAllowOrNotForApply obj_ApplicationAllowOrNotForApply = new NOCAP.BLL.Master.ApplicationAllowOrNotForApply(int_applicationTypeCode, int_applicationPurposeCode, int_applicationTypeCategoryCode, int_stateCode, int_districtCode, int_subDistrictCode, int_WaterQualityTypeCode, enm_WhetherGroundWaterUtilizationFor, enm_NOCObtainForExistIND, dt_DateOfCommExistIND, dt_DateOfExpansionIND);
        NOCAP.BLL.Master.SubDistrictAreaTypeCategory obj_SubDistrictAreaTypeCategory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategory(int_stateCode, int_districtCode, int_subDistrictCode);
        if (obj_SubDistrictAreaTypeCategory.SubDistrictCurrentAreaCategoryKey == 0)
        {
            lblMessage.Text = "Infrastructure New Application  - Area Type Category not defined";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return 0;
        }
        try
        {
            //check for area type not defined
            if (obj_ApplicationAllowOrNotForApply.ProceedForFinalApply == NOCAP.BLL.Master.ApplicationAllowOrNotForApply.ApplicationAllowNotAllowForApply.Allow)
            {
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    if (UpdateGeneralInformationLocationDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text.Trim())) == 1) { return 1; }
                    else { return 0; }
                }
                else
                {
                    if (AddGeneralInformationLocationDetails() == 1) { return 1; }
                    else { return 0; }
                }
            }
            else
            {
                NOCAP.BLL.Master.WaterQuality obj_WaterQuality = new NOCAP.BLL.Master.WaterQuality(int_WaterQualityTypeCode);

                if (obj_WaterQuality != null && obj_WaterQuality.WaterQualityDesc != "" && obj_WaterQuality.BypassCondition == NOCAP.BLL.Master.WaterQuality.BypassConditionYesNo.No)
                {
                    if (enm_WhetherGroundWaterUtilizationFor == NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.NewIndustry)
                    {
                        if (obj_ApplicationAllowOrNotForApply.ProceedForApplyOnAllowPendingApplicationOnAreaTypeCategory == NOCAP.BLL.Master.ApplicationAllowOrNotForApply.ApplicationAllowNotAllowForApply.NotAllow)
                        {
                            lblMessage.Text = "Not Allow due to Area Type Category";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    if (obj_ApplicationAllowOrNotForApply.ProceedForApplyOnApplicationNotAlloweOnWaterBasedIndustry == NOCAP.BLL.Master.ApplicationAllowOrNotForApply.ApplicationAllowNotAllowForApply.NotAllow)
                    {
                        lblMessage.Text = "Not Allow due to Water Based Industry";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (obj_ApplicationAllowOrNotForApply.ProceedForApplyOnApplicationCheckForStateGroundWaterAuthority == NOCAP.BLL.Master.ApplicationAllowOrNotForApply.ApplicationAllowNotAllowForApply.NotAllow)
                {
                    lblMessage.Text = "Not Allow due to State Ground Water Authority";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                if (enm_WhetherGroundWaterUtilizationFor != NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.NewIndustry)
                {
                    //if (obj_ApplicationAllowOrNotForApply.ProceedForApplyOnApplicationAreaTypeCheckForExistIndustry == NOCAP.BLL.Master.ApplicationAllowOrNotForApply.ApplicationAllowNotAllowForApply.NotAllow)
                    // {
                    lblMessage.Text = HttpUtility.HtmlEncode(obj_ApplicationAllowOrNotForApply.DisplayMessage);
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    // }
                }
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
        }
    }
    private int UpdateGeneralInformationLocationDetails(long lngA_ApplicationCode)
    {
        try
        {
            strActionName = "UpdateInfrastructureNewApplication";
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            obj_InfrastructureNewApplication.ApplicationTypeCategoryCode = Convert.ToInt32(ddlApplicationTypeCategory.SelectedValue);
            obj_InfrastructureNewApplication.NameOfInfrastructure = txtNameOfInfraStructure.Text;
            obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1 = txtAddressLine1.Text;
            obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2 = txtAddressLine2.Text;
            obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3 = txtAddressLine3.Text;
            obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode = Convert.ToInt32(ddlState.SelectedValue);
            obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode = Convert.ToInt32(ddlDistrict.SelectedValue);
            obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode = Convert.ToInt32(ddlSubDistrict.SelectedValue);
            obj_InfrastructureNewApplication.WaterQualityCode = Convert.ToInt32(ddlWaterQualityType.SelectedValue);
            //static ground waret - delete it
            //obj_InfrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement = Convert.ToDecimal(txtGroundWaterRequirement.Text);
            switch (ddlTownOrVillage.SelectedValue)
            {
                case "V":
                    if (NOCAPExternalUtility.IsNumeric(ddlVillage.SelectedValue))
                    {
                        obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode = Convert.ToInt32(ddlVillage.SelectedValue);
                        obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown = NOCAP.BLL.Common.Address.VillageOrTownOption.Village;
                    }
                    else { return 0; }
                    break;
                case "T":
                    if (NOCAPExternalUtility.IsNumeric(ddlTown.SelectedValue))
                    {
                        obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.TownCode = Convert.ToInt32(ddlTown.SelectedValue);
                        obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown = NOCAP.BLL.Common.Address.VillageOrTownOption.Town;
                    }
                    else { return 0; }
                    break;
            }


            switch (ddlMSME.SelectedValue)
            {
                case "Y":
                    obj_InfrastructureNewApplication.MSME = NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.MSMEYesNo.Yes;
                    break;
                case "N":
                    obj_InfrastructureNewApplication.MSME = NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.MSMEYesNo.No;
                    break;
            }

            if (ddlMSME.SelectedValue == "Y")
            {
                if (ddMSMEType.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Fill MSME Type');", true);
                    return 0;
                }

            }
            if (obj_InfrastructureNewApplication.MSME == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.MSMEYesNo.Yes)
            {
                obj_InfrastructureNewApplication.MSMETypeCode = Convert.ToInt32(ddMSMEType.SelectedValue);
            }
            else
            {
                obj_InfrastructureNewApplication.MSMETypeCode = null;
            }

            switch (ddlWetlandArea.SelectedValue)
            {
                case "Y":
                    obj_InfrastructureNewApplication.WetLandArea = NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.WetLandAreaYesNo.Yes;
                    break;
                case "N":
                    obj_InfrastructureNewApplication.WetLandArea = NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.WetLandAreaYesNo.No;
                    break;
                case "":
                    obj_InfrastructureNewApplication.WetLandArea = NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.WetLandAreaYesNo.NotDefine;
                    break;
                default:
                    obj_InfrastructureNewApplication.WetLandArea = NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.WetLandAreaYesNo.NotDefine;
                    break;
            }

            if (txtProLat.Text.Trim() == "")
            { obj_InfrastructureNewApplication.ProposedLocation.ProposedLatitude = null; }
            else
            { obj_InfrastructureNewApplication.ProposedLocation.ProposedLatitude = Convert.ToDecimal(txtProLat.Text); }
            if (txtProLong.Text.Trim() == "")
            { obj_InfrastructureNewApplication.ProposedLocation.ProposedLongitude = null; }
            else
            { obj_InfrastructureNewApplication.ProposedLocation.ProposedLongitude = Convert.ToDecimal(txtProLong.Text); }

            switch (rbtnWhetherGroundWaterUtilization.SelectedValue)
            {
                case "NewIndustry":
                    obj_InfrastructureNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.NewProject;
                    obj_InfrastructureNewApplication.DateOfCommencement = null;
                    obj_InfrastructureNewApplication.DateOfExpansionOfProject = null;
                    obj_InfrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                    break;
                case "ExistingIndustry":
                    //obj_InfrastructureNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.ExistingIndustry;
                    //obj_InfrastructureNewApplication.DateOfCommencement = Convert.ToDateTime(txtDateOfCommencement.Text);
                    //obj_InfrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                    //break;
                    obj_InfrastructureNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.ExistingIndustry;
                    switch (ddlNOCObtainedForExistIND.SelectedValue)
                    {
                        case "Y":
                            obj_InfrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.Yes;
                            obj_InfrastructureNewApplication.DateOfCommencement = null;
                            obj_InfrastructureNewApplication.DateOfExpansionOfProject = null;
                            break;
                        case "N":
                            obj_InfrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No;
                            obj_InfrastructureNewApplication.DateOfCommencement = Convert.ToDateTime(txtDateOfCommencement.Text);
                            obj_InfrastructureNewApplication.DateOfExpansionOfProject = null;
                            break;
                        default:
                            obj_InfrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                            obj_InfrastructureNewApplication.DateOfCommencement = null;
                            obj_InfrastructureNewApplication.DateOfExpansionOfProject = null;
                            break;
                    }
                    break;
                case "ExpansionProgramExistingIndustry":
                    obj_InfrastructureNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.ExpansionProgramme;
                    switch (ddlNOCObtainedForExistIND.SelectedValue)
                    {
                        case "Y":
                            obj_InfrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.Yes;
                            obj_InfrastructureNewApplication.DateOfCommencement = null;
                            obj_InfrastructureNewApplication.DateOfExpansionOfProject = null;
                            break;
                        case "N":
                            obj_InfrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No;
                            obj_InfrastructureNewApplication.DateOfCommencement = Convert.ToDateTime(txtDateOfCommencement.Text);
                            obj_InfrastructureNewApplication.DateOfExpansionOfProject = Convert.ToDateTime(txtDateOfExpansion.Text);
                            break;
                        default:
                            obj_InfrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                            obj_InfrastructureNewApplication.DateOfCommencement = null;
                            obj_InfrastructureNewApplication.DateOfExpansionOfProject = null;
                            break;
                    }
                    break;
                default:
                    obj_InfrastructureNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.NotDefined;
                    break;
            }



            NOCAP.BLL.Master.SubDistrict obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
            obj_InfrastructureNewApplication.ApplySubDistrictAreaCategoryKey = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory().SubDistrictAreaTypeCategoryKey;
            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_InfrastructureNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;
            if (obj_InfrastructureNewApplication.Update() == 1)
            {
                strStatus = "Update Sccessfully";
                return 1;
            }
            else
            {
                strStatus = "Update Unsuccessfull";
                lblMessage.Text = obj_InfrastructureNewApplication.CustumMessage;
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
    private int AddGeneralInformationLocationDetails()
    {
        try
        {
            strActionName = "AddInfrastructureNewDetails";
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication();
            obj_InfrastructureNewApplication.ApplicationTypeCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Infrastructure");
            obj_InfrastructureNewApplication.ApplicationPurposeCode = NOCAPExternalUtility.EquivalentApplicationPurposeCodeOfDatabaseForApplicationPurpose("New");
            obj_InfrastructureNewApplication.ApplicationTypeCategoryCode = Convert.ToInt32(ddlApplicationTypeCategory.SelectedValue);
            //obj_InfrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement = Convert.ToDecimal(txtGroundWaterRequirement.Text);

            obj_InfrastructureNewApplication.NameOfInfrastructure = txtNameOfInfraStructure.Text;

            obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1 = txtAddressLine1.Text;
            obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2 = txtAddressLine2.Text;
            obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3 = txtAddressLine3.Text;
            obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode = Convert.ToInt32(ddlState.SelectedValue);
            obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode = Convert.ToInt32(ddlDistrict.SelectedValue);
            obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode = Convert.ToInt32(ddlSubDistrict.SelectedValue);
            obj_InfrastructureNewApplication.WaterQualityCode = Convert.ToInt32(ddlWaterQualityType.SelectedValue);



            switch (ddlMSME.SelectedValue)
            {
                case "Y":
                    obj_InfrastructureNewApplication.MSME = NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.MSMEYesNo.Yes;
                    break;
                case "N":
                    obj_InfrastructureNewApplication.MSME = NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.MSMEYesNo.No;
                    break;
            }
            if (ddlMSME.SelectedValue == "Y")
            {
                if (ddMSMEType.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Fill MSME Type');", true);
                    return 0;
                }

            }

            if (obj_InfrastructureNewApplication.MSME == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.MSMEYesNo.Yes)
            {
                obj_InfrastructureNewApplication.MSMETypeCode = Convert.ToInt32(ddMSMEType.SelectedValue);
            }
            else
            {
                obj_InfrastructureNewApplication.MSMETypeCode = null;
            }


            switch (ddlTownOrVillage.SelectedValue)
            {
                case "V":
                    obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode = Convert.ToInt32(ddlVillage.SelectedValue);
                    obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown = NOCAP.BLL.Common.Address.VillageOrTownOption.Village;
                    break;
                case "T":
                    obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.TownCode = Convert.ToInt32(ddlTown.SelectedValue);
                    obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown = NOCAP.BLL.Common.Address.VillageOrTownOption.Town;
                    break;
            }
            switch (ddlWetlandArea.SelectedValue)
            {
                case "Y":
                    obj_InfrastructureNewApplication.WetLandArea = NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.WetLandAreaYesNo.Yes;
                    break;
                case "N":
                    obj_InfrastructureNewApplication.WetLandArea = NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.WetLandAreaYesNo.No;
                    break;
                case "":
                    obj_InfrastructureNewApplication.WetLandArea = NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.WetLandAreaYesNo.NotDefine;
                    break;
                default:
                    obj_InfrastructureNewApplication.WetLandArea = NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.WetLandAreaYesNo.NotDefine;
                    break;
            }

            if (txtProLat.Text.Trim() == "")
            { obj_InfrastructureNewApplication.ProposedLocation.ProposedLatitude = null; }
            else
            { obj_InfrastructureNewApplication.ProposedLocation.ProposedLatitude = Convert.ToDecimal(txtProLat.Text); }
            if (txtProLong.Text.Trim() == "")
            { obj_InfrastructureNewApplication.ProposedLocation.ProposedLongitude = null; }
            else
            { obj_InfrastructureNewApplication.ProposedLocation.ProposedLongitude = Convert.ToDecimal(txtProLong.Text); }
            //obj_InfrastructureNewApplication.ProposedLocation.ProposedLatitude = Convert.ToDecimal(txtProLat.Text);
            //obj_InfrastructureNewApplication.ProposedLocation.ProposedLongitude = Convert.ToDecimal(txtProLong.Text);
            switch (rbtnWhetherGroundWaterUtilization.SelectedValue)
            {
                case "NewIndustry":
                    obj_InfrastructureNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.NewProject;
                    obj_InfrastructureNewApplication.DateOfCommencement = null;
                    obj_InfrastructureNewApplication.DateOfExpansionOfProject = null;
                    obj_InfrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                    break;
                case "ExistingIndustry":
                    //obj_InfrastructureNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.ExistingIndustry;
                    //obj_InfrastructureNewApplication.DateOfCommencement = Convert.ToDateTime(txtDateOfCommencement.Text);
                    //obj_InfrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                    //break;
                    obj_InfrastructureNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.ExistingIndustry;
                    switch (ddlNOCObtainedForExistIND.SelectedValue)
                    {
                        case "Y":
                            obj_InfrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.Yes;
                            obj_InfrastructureNewApplication.DateOfCommencement = null;
                            obj_InfrastructureNewApplication.DateOfExpansionOfProject = null;
                            break;
                        case "N":
                            obj_InfrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No;
                            obj_InfrastructureNewApplication.DateOfCommencement = Convert.ToDateTime(txtDateOfCommencement.Text);
                            obj_InfrastructureNewApplication.DateOfExpansionOfProject = null;
                            break;
                        default:
                            obj_InfrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                            obj_InfrastructureNewApplication.DateOfCommencement = null;
                            obj_InfrastructureNewApplication.DateOfExpansionOfProject = null;
                            break;
                    }
                    break;
                case "ExpansionProgramExistingIndustry":
                    obj_InfrastructureNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.ExpansionProgramme;
                    switch (ddlNOCObtainedForExistIND.SelectedValue)
                    {
                        case "Y":
                            obj_InfrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.Yes;
                            obj_InfrastructureNewApplication.DateOfCommencement = null;
                            obj_InfrastructureNewApplication.DateOfExpansionOfProject = null;
                            break;
                        case "N":
                            obj_InfrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No;
                            obj_InfrastructureNewApplication.DateOfCommencement = Convert.ToDateTime(txtDateOfCommencement.Text);
                            obj_InfrastructureNewApplication.DateOfExpansionOfProject = Convert.ToDateTime(txtDateOfExpansion.Text);
                            break;
                        default:
                            obj_InfrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                            obj_InfrastructureNewApplication.DateOfCommencement = null;
                            obj_InfrastructureNewApplication.DateOfExpansionOfProject = null;
                            break;
                    }
                    break;
                default:
                    obj_InfrastructureNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.NotDefined;
                    break;
            }


            NOCAP.BLL.Master.SubDistrict obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
            obj_InfrastructureNewApplication.ApplySubDistrictAreaCategoryKey = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory().SubDistrictAreaTypeCategoryKey;

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_InfrastructureNewApplication.CreatedByExUC = obj_externalUser.ExternalUserCode;
            //string s = obj_InfrastructureNewApplication.GroundWaterAvailability;
            if (obj_InfrastructureNewApplication.Add() == 1)
            {
                strStatus = "Added Successfully";
                lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.ApplicationCode);
                lblModeFrom.Text = "Edit";
                lblPageTitleFrom.Text = "Self";
                return 1;
            }
            else
            {
                strStatus = "Add Unsuccessfull";
                lblMessage.Text = obj_InfrastructureNewApplication.CustumMessage;
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
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
                if (CheckAndAddAndUpdateInfrastructureApplication() == 1) { Server.Transfer("~/ExternalUser/InfrastructureNew/CommunicationAddress.aspx"); }
                else { }
            }
        }
    }
    protected void Check_Click(object sender, EventArgs e)
    {
        int int_stateCode = Convert.ToInt32(ddlState.SelectedValue);
        int int_districtCode = Convert.ToInt32(ddlDistrict.SelectedValue);
        int int_subDistrictCode = Convert.ToInt32(ddlSubDistrict.SelectedValue);
        int int_applicationTypeCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Infrastructure");
        int int_applicationPurposeCode = NOCAPExternalUtility.EquivalentApplicationPurposeCodeOfDatabaseForApplicationPurpose("New");
        int int_applicationTypeCategoryCode = Convert.ToInt32(ddlApplicationTypeCategory.SelectedValue);

        NOCAP.BLL.Master.ApplicationAllowOrNotForApply obj_ApplicationAllowOrNotForApply = new NOCAP.BLL.Master.ApplicationAllowOrNotForApply(int_applicationTypeCode, int_applicationPurposeCode, int_applicationTypeCategoryCode, int_stateCode, int_districtCode, int_subDistrictCode, Convert.ToInt32(ddlWaterQualityType.SelectedValue));
        Response.Write(HttpUtility.HtmlEncode("AreaTypeCategory:" + obj_ApplicationAllowOrNotForApply.ProceedForApplyOnAllowPendingApplicationOnAreaTypeCategory + " <br / > <br /> "));

        Response.Write(HttpUtility.HtmlEncode("Water Based:" + obj_ApplicationAllowOrNotForApply.ProceedForApplyOnApplicationNotAlloweOnWaterBasedIndustry + " <br / > <br /> "));
        Response.Write(HttpUtility.HtmlEncode("State Ground Water Authority:" + obj_ApplicationAllowOrNotForApply.ProceedForApplyOnApplicationCheckForStateGroundWaterAuthority + " <br / > <br /> "));
        Response.Write(HttpUtility.HtmlEncode("Final:" + obj_ApplicationAllowOrNotForApply.ProceedForFinalApply));
    }


    private void BindGeneralInformationLocationDetails(long lngA_ApplicationCode, Object sender, EventArgs e)
    {
        try
        {
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            //txtGroundWaterRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement));
            txtNameOfInfraStructure.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.NameOfInfrastructure);
            txtAddressLine1.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1);
            txtAddressLine2.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2);
            txtAddressLine3.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3);

            ddlWaterQualityType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.WaterQualityCode));
            ddlApplicationTypeCategory.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.ApplicationTypeCategoryCode));
            ddlState.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode));
            NOCAPExternalUtility.FillDropDownDistrict(ref ddlDistrict, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode);
            ddlDistrict.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode));
            NOCAPExternalUtility.FillDropDownSubDistrict(ref ddlSubDistrict, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode);
            ddlSubDistrict.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode));


            switch (obj_infrastructureNewApplication.MSME)
            {
                case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.MSMEYesNo.Yes:
                    ddlMSME.SelectedValue = "Y";
                    break;
                case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.MSMEYesNo.No:
                    ddlMSME.SelectedValue = "N";
                    break;
                default:
                    break;
            }

            ddMSMEType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.MSMETypeCode));


            if (NOCAPExternalUtility.FillDropDownTownOrVillage(ref ddlTownOrVillage) != 1)
            {
                lblMessage.Text = "Problem in Village/Town population";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            if (NOCAPExternalUtility.FillDropDownVillage(ref ddlVillage, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode) != 1)
            {
                lblMessage.Text = "Problem in Village population";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            if (NOCAPExternalUtility.FillDropDownTown(ref ddlTown, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode) != 1)
            {
                lblMessage.Text = "Problem in Town population";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            switch (obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown)
            {
                case NOCAP.BLL.Common.Address.VillageOrTownOption.Town:
                    ddlTownOrVillage.SelectedValue = "T";
                    ddlTown.Visible = true;
                    ddlTown.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.TownCode));
                    ddlVillage.Visible = false;
                    ddlVillage.SelectedIndex = 0;
                    break;
                case NOCAP.BLL.Common.Address.VillageOrTownOption.Village:
                    ddlTown.Visible = false;
                    ddlVillage.Visible = true;
                    ddlTownOrVillage.SelectedValue = "V";
                    ddlVillage.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode));
                    ddlTown.SelectedIndex = 0;
                    break;
                default:
                    ddlTownOrVillage.SelectedValue = "";
                    ddlTown.SelectedIndex = 0;
                    ddlVillage.SelectedIndex = 0;
                    ddlTown.Visible = false;
                    ddlVillage.Visible = false;
                    break;
            }

            txtProLat.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.ProposedLocation.ProposedLatitude);
            txtProLong.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.ProposedLocation.ProposedLongitude);

            switch (obj_infrastructureNewApplication.GroundWaterUtilizationFor)
            {
                case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.NewProject:
                    rbtnWhetherGroundWaterUtilization.SelectedValue = "NewIndustry";
                    break;
                case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.ExistingIndustry:
                    rbtnWhetherGroundWaterUtilization.SelectedValue = "ExistingIndustry";
                    break;
                case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.ExpansionProgramme:
                    rbtnWhetherGroundWaterUtilization.SelectedValue = "ExpansionProgramExistingIndustry";
                    break;

            }
            rbtnWhetherGroundWaterUtilization_SelectedIndexChanged(sender, e);
            //if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExistingIndustry")
            //{
            //    if (!Convert.IsDBNull(obj_infrastructureNewApplication.DateOfCommencement))
            //    {
            //        txtDateOfCommencement.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_infrastructureNewApplication.DateOfCommencement).ToString("dd/MM/yyyy"));
            //    }
            //}
            //else
            if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExistingIndustry" || rbtnWhetherGroundWaterUtilization.SelectedValue == "ExpansionProgramExistingIndustry")
            {
                switch (obj_infrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND)
                {
                    case NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.Yes:
                        ddlNOCObtainedForExistIND.SelectedValue = "Y";
                        break;
                    case NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No:
                        ddlNOCObtainedForExistIND.SelectedValue = "N";
                        break;
                    default:
                        break;
                }
                ddlNOCObtainedForExistIND_SelectedIndexChanged(sender, e);
                if (ddlNOCObtainedForExistIND.SelectedValue == "N")
                {
                    if (!String.IsNullOrEmpty(Convert.ToString((obj_infrastructureNewApplication.DateOfCommencement))))
                    {
                        txtDateOfCommencement.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_infrastructureNewApplication.DateOfCommencement).ToString("dd/MM/yyyy"));
                    }
                    if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExpansionProgramExistingIndustry")
                    {
                        if (!String.IsNullOrEmpty(Convert.ToString(obj_infrastructureNewApplication.DateOfExpansionOfProject)))
                        {
                            txtDateOfExpansion.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_infrastructureNewApplication.DateOfExpansionOfProject).ToString("dd/MM/yyyy"));
                        }
                    }
                }
            }
            switch (obj_infrastructureNewApplication.WetLandArea)
            {
                case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.WetLandAreaYesNo.Yes:
                    ddlWetlandArea.SelectedValue = "Y";
                    break;
                case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.WetLandAreaYesNo.No:
                    ddlWetlandArea.SelectedValue = "N";
                    break;
                case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.WetLandAreaYesNo.NotDefine:
                    ddlWetlandArea.SelectedValue = "N";
                    break;
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
    protected void ddlTownOrVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            switch (ddlTownOrVillage.SelectedValue)
            {
                case "T":
                    ddlTown.Visible = true;
                    ddlVillage.Visible = false;
                    reqValiVillage.Enabled = false;
                    reqValiTown.Enabled = true;
                    lblTown.Visible = true;
                    lblVillage.Visible = false;
                    break;
                case "V":
                    ddlTown.Visible = false;
                    ddlVillage.Visible = true;
                    reqValiTown.Enabled = false;
                    reqValiVillage.Enabled = true;
                    lblVillage.Visible = true;
                    lblTown.Visible = false;
                    break;
                case "":
                    ddlTown.Visible = false;
                    ddlVillage.Visible = false;
                    reqValiTown.Enabled = false;
                    reqValiVillage.Enabled = false;
                    lblVillage.Visible = false;
                    lblTown.Visible = false;
                    break;
                default:
                    ddlTown.Visible = false;
                    ddlVillage.Visible = false;
                    reqValiTown.Enabled = false;
                    reqValiVillage.Enabled = false;
                    lblVillage.Visible = false;
                    lblTown.Visible = false;
                    break;
            }
        }
    }
    protected void rbtnWhetherGroundWaterUtilization_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExistingIndustry")
        {
            RowNOCObtainedForExistIND.Visible = true;
            RowDateOfCommencement.Visible = false;
            RowDateOfExpansion.Visible = false;
            rfvddlNOCObtainedForExistIND.Enabled = true;
            ddlNOCObtainedForExistIND_SelectedIndexChanged(sender, e);
        }
        else if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExpansionProgramExistingIndustry")
        {
            RowNOCObtainedForExistIND.Visible = true;
            RowDateOfCommencement.Visible = false;
            RowDateOfExpansion.Visible = false;
            rfvddlNOCObtainedForExistIND.Enabled = true;
            ddlNOCObtainedForExistIND_SelectedIndexChanged(sender, e);
        }
        else
        {
            RowDateOfCommencement.Visible = false;
            RowDateOfExpansion.Visible = false;
            RowNOCObtainedForExistIND.Visible = false;
            rfvddlNOCObtainedForExistIND.Enabled = false;
            txtDateOfCommencement.Text = "";
            txtDateOfExpansion.Text = "";
        }
    }
    protected void ddlNOCObtainedForExistIND_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNOCObtainedForExistIND.SelectedValue == "N")
        {
            RowDateOfCommencement.Visible = true;
            rfvtxtDateOfCommencement.Enabled = true;
            rvtxtDateOfCommencement.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
            txtDateOfExpansion.Text = "";
            if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExpansionProgramExistingIndustry")
            {
                rvtxtDateOfExpansion.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                RowDateOfExpansion.Visible = true;
                rfvtxtDateOfExpansion.Enabled = true;

            }
        }
        else
        {
            txtDateOfCommencement.Text = "";
            txtDateOfExpansion.Text = "";

            RowDateOfCommencement.Visible = false;
            rfvtxtDateOfCommencement.Enabled = false;

            RowDateOfExpansion.Visible = false;
            rfvtxtDateOfExpansion.Enabled = false;
        }
    }
    protected void imgbtnCalendar_Click(object sender, ImageClickEventArgs e)
    {

    }
}