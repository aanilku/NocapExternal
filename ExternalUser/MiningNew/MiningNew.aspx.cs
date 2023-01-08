using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_Mining_MiningNew : System.Web.UI.Page
{
    string strPageName = "MINMiningNew";
    string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

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
                            if (SourceLabel != null) { lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); }
                            Label SourceLabelPreviousPage = (Label)placeHolder.FindControl("lblMiningApplicationCodeFrom");
                            if (SourceLabelPreviousPage != null) { lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabelPreviousPage.Text); }
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
                if (NOCAPExternalUtility.FillDropDownApplicationTypeCategory(ref ddlApplicationTypeCategory, NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Mining")) != 1)
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

                if (lblMiningApplicationCodeFrom.Text.Trim() != "")
                {
                    BindGeneralInformationLocationDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text), sender, e);
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
        revtxtNameOfMining.ValidationExpression = ValidationUtility.txtValForProjectName;
        revtxtNameOfMining.ErrorMessage = ValidationUtility.txtValForProjectNameMsg;

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
    private void BindGeneralInformationLocationDetails(long lngA_ApplicationCode, Object sender, EventArgs e)
    {
        try
        {
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
            //txtGroundWaterRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.GWREquiredThroughAbstractStructure));
            txtNameOfMining.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.NameOfMining);
            txtAddressLine1.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1);
            txtAddressLine2.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2);
            txtAddressLine3.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3);
            ddlWaterQualityType.SelectedValue = HttpUtility.HtmlEncode(obj_MiningNewApplication.WaterQualityCode);
            ddlApplicationTypeCategory.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.ApplicationTypeCategoryCode));
            ddlState.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode));
            NOCAPExternalUtility.FillDropDownDistrict(ref ddlDistrict, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode);
            ddlDistrict.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode));
            NOCAPExternalUtility.FillDropDownSubDistrict(ref ddlSubDistrict, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode);
            ddlSubDistrict.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode));


            switch (obj_MiningNewApplication.MSME)
            {
                case NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication.MSMEYesNo.Yes:
                    ddlMSME.SelectedValue = "Y";
                    break;
                case NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication.MSMEYesNo.No:
                    ddlMSME.SelectedValue = "N";
                    break;
                default:
                    break;
            }

            ddMSMEType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.MSMETypeCode));

            switch (obj_MiningNewApplication.WetLandArea)
            {
                case NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WetLandAreaYesNo.Yes:
                    ddlWetlandArea.SelectedValue = "Y";
                    break;
                case NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WetLandAreaYesNo.No:
                    ddlWetlandArea.SelectedValue = "N";
                    break;
                case NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WetLandAreaYesNo.NotDefine:
                    ddlWetlandArea.SelectedValue = "N";
                    break;

            }
            if (NOCAPExternalUtility.FillDropDownTownOrVillage(ref ddlTownOrVillage) != 1)
            {
                lblMessage.Text = "Problem in Village/Town population";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            if (NOCAPExternalUtility.FillDropDownVillage(ref ddlVillage, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode) != 1)
            {
                lblMessage.Text = "Problem in Village population";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            if (NOCAPExternalUtility.FillDropDownTown(ref ddlTown, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode) != 1)
            {
                lblMessage.Text = "Problem in Town population";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            switch (obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown)
            {
                case NOCAP.BLL.Common.Address.VillageOrTownOption.Town:
                    ddlTownOrVillage.SelectedValue = "T";
                    ddlTown.Visible = true;
                    ddlTown.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode));
                    ddlVillage.Visible = false;
                    ddlVillage.SelectedIndex = 0;
                    break;
                case NOCAP.BLL.Common.Address.VillageOrTownOption.Village:
                    ddlTown.Visible = false;
                    ddlVillage.Visible = true;
                    ddlTownOrVillage.SelectedValue = "V";
                    ddlVillage.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode));
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

            txtProLat.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.ProposedLocation.ProposedLatitude);
            txtProLong.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.ProposedLocation.ProposedLongitude);

            switch (obj_MiningNewApplication.GroundWaterUtilizationFor)
            {
                case NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.NewIndustry:
                    rbtnWhetherGroundWaterUtilization.SelectedValue = "NewIndustry";
                    break;
                case NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry:
                    rbtnWhetherGroundWaterUtilization.SelectedValue = "ExistingIndustry";
                    break;
                case NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExpansionProgramOfExitingIndustry:
                    rbtnWhetherGroundWaterUtilization.SelectedValue = "ExpansionProgramExistingIndustry";
                    break;

            }
            rbtnWhetherGroundWaterUtilization_SelectedIndexChanged(sender, e);
            //if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExistingIndustry")
            //{
            //    if (!Convert.IsDBNull(obj_MiningNewApplication.DateOfCommencement))
            //    {
            //        txtDateOfCommencement.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_MiningNewApplication.DateOfCommencement).ToString("dd/MM/yyyy"));
            //    }
            //}
            //else 

            if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExistingIndustry" || rbtnWhetherGroundWaterUtilization.SelectedValue == "ExpansionProgramExistingIndustry")
            {
                switch (obj_MiningNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND)
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
                    if (!Convert.IsDBNull(obj_MiningNewApplication.DateOfCommencement))
                    {
                        txtDateOfCommencement.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_MiningNewApplication.DateOfCommencement).ToString("dd/MM/yyyy"));
                    }
                    if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExpansionProgramExistingIndustry")
                    {
                        if (!String.IsNullOrEmpty(Convert.ToString(obj_MiningNewApplication.DateOfExpansionOfProject)))
                        {
                            txtDateOfExpansion.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_MiningNewApplication.DateOfExpansionOfProject).ToString("dd/MM/yyyy"));
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
        }
    }

    protected void Check_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {
                int int_stateCode = Convert.ToInt32(ddlState.SelectedValue);
                int int_districtCode = Convert.ToInt32(ddlDistrict.SelectedValue);
                int int_subDistrictCode = Convert.ToInt32(ddlSubDistrict.SelectedValue);
                int int_applicationTypeCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Mining");
                int int_applicationPurposeCode = NOCAPExternalUtility.EquivalentApplicationPurposeCodeOfDatabaseForApplicationPurpose("New");
                int int_applicationTypeCategoryCode = Convert.ToInt32(ddlApplicationTypeCategory.SelectedValue);

                NOCAP.BLL.Master.ApplicationAllowOrNotForApply obj_ApplicationAllowOrNotForApply = new NOCAP.BLL.Master.ApplicationAllowOrNotForApply(int_applicationTypeCode, int_applicationPurposeCode, int_applicationTypeCategoryCode, int_stateCode, int_districtCode, int_subDistrictCode, Convert.ToInt32(ddlWaterQualityType.SelectedValue));
                Response.Write("AreaTypeCategory:" + HttpUtility.HtmlEncode(obj_ApplicationAllowOrNotForApply.ProceedForApplyOnAllowPendingApplicationOnAreaTypeCategory) + " <br / > <br /> ");

                Response.Write("Water Based:" + HttpUtility.HtmlEncode(obj_ApplicationAllowOrNotForApply.ProceedForApplyOnApplicationNotAlloweOnWaterBasedIndustry) + " <br / > <br /> ");
                Response.Write("State Ground Water Authority:" + HttpUtility.HtmlEncode(obj_ApplicationAllowOrNotForApply.ProceedForApplyOnApplicationCheckForStateGroundWaterAuthority) + " <br / > <br /> ");
                Response.Write("Final:" + HttpUtility.HtmlEncode(obj_ApplicationAllowOrNotForApply.ProceedForFinalApply));

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
                if (Page.IsValid)
                {
                    if (CheckAndAddAndUpdateMiningApplication() == 1)
                    {

                    }
                    else { }
                }
            }
        }
    }

    private int CheckAndAddAndUpdateMiningApplication()
    {
        int int_applicationTypeCategoryCode, int_stateCode, int_districtCode, int_subDistrictCode;
        if (NOCAPExternalUtility.IsNumeric(ddlApplicationTypeCategory.SelectedValue)) { int_applicationTypeCategoryCode = Convert.ToInt32(ddlApplicationTypeCategory.SelectedValue); } else { return 0; }
        if (NOCAPExternalUtility.IsNumeric(ddlState.SelectedValue)) { int_stateCode = Convert.ToInt32(ddlState.SelectedValue); } else { return 0; }
        if (NOCAPExternalUtility.IsNumeric(ddlDistrict.SelectedValue)) { int_districtCode = Convert.ToInt32(ddlDistrict.SelectedValue); } else { return 0; }
        if (NOCAPExternalUtility.IsNumeric(ddlSubDistrict.SelectedValue)) { int_subDistrictCode = Convert.ToInt32(ddlSubDistrict.SelectedValue); } else { return 0; }
        int int_applicationTypeCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Mining");
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
            lblMessage.Text = "Mining New Application  - Area Type Category not defined";
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
                    if (UpdateGeneralInformationLocationDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text.Trim())) == 1)
                    {
                        strActionName = "SaveAsDraft";
                        strStatus = "Record Save Successfully !";

                        lblMessage.Text = "Saved Successfully";
                        lblMessage.ForeColor = System.Drawing.Color.Green;

                        return 1;
                    }
                    else
                    {

                        strActionName = "SaveAsDraft";
                        strStatus = "Record Save Failed !";
                        return 0;
                    }
                }
                else
                {
                    if (AddGeneralInformationLocationDetails() == 1)
                    {
                        strActionName = "SaveAsDraft";
                        strStatus = "Record Add Successfully !";


                        lblMessage.Text = "Saved Successfully";
                        lblMessage.ForeColor = System.Drawing.Color.Blue;
                        return 1;
                    }
                    else
                    {
                        strActionName = "SaveAsDraft";
                        strStatus = "Record Add Failed !";

                        return 0;
                    }
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
                    // if (obj_ApplicationAllowOrNotForApply.ProceedForApplyOnApplicationAreaTypeCheckForExistIndustry == NOCAP.BLL.Master.ApplicationAllowOrNotForApply.ApplicationAllowNotAllowForApply.NotAllow)
                    //  {
                    lblMessage.Text = HttpUtility.HtmlEncode(obj_ApplicationAllowOrNotForApply.DisplayMessage);
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    // }
                }
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

    private int AddGeneralInformationLocationDetails()
    {
        try
        {
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication();
            obj_MiningNewApplication.ApplicationTypeCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Mining");
            obj_MiningNewApplication.ApplicationPurposeCode = NOCAPExternalUtility.EquivalentApplicationPurposeCodeOfDatabaseForApplicationPurpose("New");
            obj_MiningNewApplication.ApplicationTypeCategoryCode = Convert.ToInt32(ddlApplicationTypeCategory.SelectedValue);
            obj_MiningNewApplication.WaterQualityCode = Convert.ToInt32(ddlWaterQualityType.SelectedValue);
            obj_MiningNewApplication.NameOfMining = txtNameOfMining.Text;
            obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1 = txtAddressLine1.Text;
            obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2 = txtAddressLine2.Text;
            obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3 = txtAddressLine3.Text;
            obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode = Convert.ToInt32(ddlState.SelectedValue);
            obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode = Convert.ToInt32(ddlDistrict.SelectedValue);
            obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode = Convert.ToInt32(ddlSubDistrict.SelectedValue);

            switch (ddlMSME.SelectedValue)
            {
                case "Y":
                    obj_MiningNewApplication.MSME = NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication.MSMEYesNo.Yes;
                    break;
                case "N":
                    obj_MiningNewApplication.MSME = NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication.MSMEYesNo.No;
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
            if (obj_MiningNewApplication.MSME == NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.MSMEYesNo.Yes)
            {
                obj_MiningNewApplication.MSMETypeCode = Convert.ToInt32(ddMSMEType.SelectedValue);
            }
            else
            {
                obj_MiningNewApplication.MSMETypeCode = null;
            }

            switch (ddlWetlandArea.SelectedValue)
            {
                case "Y":
                    obj_MiningNewApplication.WetLandArea = NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WetLandAreaYesNo.Yes;
                    break;
                case "N":
                    obj_MiningNewApplication.WetLandArea = NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WetLandAreaYesNo.No;
                    break;
                case "":
                    obj_MiningNewApplication.WetLandArea = NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WetLandAreaYesNo.NotDefine;
                    break;
                default:
                    obj_MiningNewApplication.WetLandArea = NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WetLandAreaYesNo.NotDefine;
                    break;
            }
            switch (ddlTownOrVillage.SelectedValue)
            {
                case "V":
                    obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode = Convert.ToInt32(ddlVillage.SelectedValue);
                    obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown = NOCAP.BLL.Common.Address.VillageOrTownOption.Village;
                    break;
                case "T":
                    obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode = Convert.ToInt32(ddlTown.SelectedValue);
                    obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown = NOCAP.BLL.Common.Address.VillageOrTownOption.Town;
                    break;
            }
            obj_MiningNewApplication.ProposedLocation.ProposedLatitude = Convert.ToDecimal(txtProLat.Text);
            obj_MiningNewApplication.ProposedLocation.ProposedLongitude = Convert.ToDecimal(txtProLong.Text);

            switch (rbtnWhetherGroundWaterUtilization.SelectedValue)
            {
                case "NewIndustry":
                    obj_MiningNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.NewIndustry;
                    obj_MiningNewApplication.DateOfCommencement = null;
                    obj_MiningNewApplication.DateOfExpansionOfProject = null;
                    obj_MiningNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                    break;
                case "ExistingIndustry":
                    //obj_MiningNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry;
                    //obj_MiningNewApplication.DateOfCommencement = Convert.ToDateTime(txtDateOfCommencement.Text);
                    //obj_MiningNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                    //break;
                    obj_MiningNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry;
                    switch (ddlNOCObtainedForExistIND.SelectedValue)
                    {
                        case "Y":
                            obj_MiningNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.Yes;
                            obj_MiningNewApplication.DateOfCommencement = null;
                            obj_MiningNewApplication.DateOfExpansionOfProject = null;
                            break;
                        case "N":
                            obj_MiningNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No;
                            obj_MiningNewApplication.DateOfCommencement = Convert.ToDateTime(txtDateOfCommencement.Text);
                            obj_MiningNewApplication.DateOfExpansionOfProject = null;
                            break;
                        default:
                            obj_MiningNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                            obj_MiningNewApplication.DateOfCommencement = null;
                            obj_MiningNewApplication.DateOfExpansionOfProject = null;
                            break;
                    }
                    break;
                case "ExpansionProgramExistingIndustry":
                    obj_MiningNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExpansionProgramOfExitingIndustry;
                    switch (ddlNOCObtainedForExistIND.SelectedValue)
                    {
                        case "Y":
                            obj_MiningNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.Yes;
                            obj_MiningNewApplication.DateOfCommencement = null;
                            obj_MiningNewApplication.DateOfExpansionOfProject = null;
                            break;
                        case "N":
                            obj_MiningNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No;
                            obj_MiningNewApplication.DateOfCommencement = Convert.ToDateTime(txtDateOfCommencement.Text);
                            obj_MiningNewApplication.DateOfExpansionOfProject = Convert.ToDateTime(txtDateOfExpansion.Text);
                            break;
                        default:
                            obj_MiningNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                            obj_MiningNewApplication.DateOfCommencement = null;
                            obj_MiningNewApplication.DateOfExpansionOfProject = null;
                            break;
                    }
                    break;
                default:
                    obj_MiningNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.NotDefined;
                    break;
            }



            NOCAP.BLL.Master.SubDistrict obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
            obj_MiningNewApplication.ApplySubDistrictAreaCategoryKey = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory().SubDistrictAreaTypeCategoryKey;

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_MiningNewApplication.CreatedByExUC = obj_externalUser.ExternalUserCode;
            //string s = obj_MiningNewApplication.GroundWaterAvailability;
            if (obj_MiningNewApplication.Add() == 1)
            {
                lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.ApplicationCode));
                lblModeFrom.Text = "Edit";
                lblPageTitleFrom.Text = "Self";
                lblMessage.Text = "Saved Successfully";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                lblMessage.Text = obj_MiningNewApplication.CustumMessage;
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
        }
    }


    private int UpdateGeneralInformationLocationDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
            obj_MiningNewApplication.ApplicationTypeCategoryCode = Convert.ToInt32(ddlApplicationTypeCategory.SelectedValue);
            obj_MiningNewApplication.NameOfMining = txtNameOfMining.Text;
            obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1 = txtAddressLine1.Text;
            obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2 = txtAddressLine2.Text;
            obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3 = txtAddressLine3.Text;
            obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode = Convert.ToInt32(ddlState.SelectedValue);
            obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode = Convert.ToInt32(ddlDistrict.SelectedValue);
            obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode = Convert.ToInt32(ddlSubDistrict.SelectedValue);
            obj_MiningNewApplication.WaterQualityCode = Convert.ToInt32(ddlWaterQualityType.SelectedValue);

            switch (ddlMSME.SelectedValue)
            {
                case "Y":
                    obj_MiningNewApplication.MSME = NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication.MSMEYesNo.Yes;
                    break;
                case "N":
                    obj_MiningNewApplication.MSME = NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication.MSMEYesNo.No;
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
            if (obj_MiningNewApplication.MSME == NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.MSMEYesNo.Yes)
            {
                obj_MiningNewApplication.MSMETypeCode = Convert.ToInt32(ddMSMEType.SelectedValue);
            }
            else
            {
                obj_MiningNewApplication.MSMETypeCode = null;
            }

            switch (ddlWetlandArea.SelectedValue)
            {
                case "Y":
                    obj_MiningNewApplication.WetLandArea = NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WetLandAreaYesNo.Yes;
                    break;
                case "N":
                    obj_MiningNewApplication.WetLandArea = NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WetLandAreaYesNo.No;
                    break;
                case "":
                    obj_MiningNewApplication.WetLandArea = NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WetLandAreaYesNo.NotDefine;
                    break;
                default:
                    obj_MiningNewApplication.WetLandArea = NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WetLandAreaYesNo.NotDefine;
                    break;
            }
            switch (ddlTownOrVillage.SelectedValue)
            {
                case "V":
                    if (NOCAPExternalUtility.IsNumeric(ddlVillage.SelectedValue))
                    {
                        obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode = Convert.ToInt32(ddlVillage.SelectedValue);
                        obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown = NOCAP.BLL.Common.Address.VillageOrTownOption.Village;
                    }
                    else { return 0; }
                    break;
                case "T":
                    if (NOCAPExternalUtility.IsNumeric(ddlTown.SelectedValue))
                    {
                        obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode = Convert.ToInt32(ddlTown.SelectedValue);
                        obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown = NOCAP.BLL.Common.Address.VillageOrTownOption.Town;
                    }
                    else { return 0; }
                    break;
            }
            obj_MiningNewApplication.ProposedLocation.ProposedLatitude = Convert.ToDecimal(txtProLat.Text);
            obj_MiningNewApplication.ProposedLocation.ProposedLongitude = Convert.ToDecimal(txtProLong.Text);

            switch (rbtnWhetherGroundWaterUtilization.SelectedValue)
            {
                case "NewIndustry":
                    obj_MiningNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.NewIndustry;
                    obj_MiningNewApplication.DateOfCommencement = null;
                    obj_MiningNewApplication.DateOfExpansionOfProject = null;
                    obj_MiningNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                    break;
                case "ExistingIndustry":
                    //obj_MiningNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry;
                    //obj_MiningNewApplication.DateOfCommencement = Convert.ToDateTime(txtDateOfCommencement.Text);
                    //obj_MiningNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                    //break;
                    obj_MiningNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry;
                    switch (ddlNOCObtainedForExistIND.SelectedValue)
                    {
                        case "Y":
                            obj_MiningNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.Yes;
                            obj_MiningNewApplication.DateOfExpansionOfProject = null;
                            obj_MiningNewApplication.DateOfCommencement = null;
                            break;
                        case "N":
                            obj_MiningNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No;
                            obj_MiningNewApplication.DateOfExpansionOfProject = null;
                            obj_MiningNewApplication.DateOfCommencement = Convert.ToDateTime(txtDateOfCommencement.Text);
                            break;
                        default:
                            obj_MiningNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                            obj_MiningNewApplication.DateOfExpansionOfProject = null;
                            obj_MiningNewApplication.DateOfCommencement = null;
                            break;
                    }
                    break;
                case "ExpansionProgramExistingIndustry":
                    obj_MiningNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExpansionProgramOfExitingIndustry;
                    switch (ddlNOCObtainedForExistIND.SelectedValue)
                    {
                        case "Y":
                            obj_MiningNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.Yes;
                            obj_MiningNewApplication.DateOfCommencement = null;
                            obj_MiningNewApplication.DateOfExpansionOfProject = null;
                            break;
                        case "N":
                            obj_MiningNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No;
                            obj_MiningNewApplication.DateOfCommencement = Convert.ToDateTime(txtDateOfCommencement.Text);
                            obj_MiningNewApplication.DateOfExpansionOfProject = Convert.ToDateTime(txtDateOfExpansion.Text);
                            break;
                        default:
                            obj_MiningNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                            obj_MiningNewApplication.DateOfCommencement = null;
                            obj_MiningNewApplication.DateOfExpansionOfProject = null;
                            break;
                    }
                    break;
                default:
                    obj_MiningNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.NotDefined;
                    break;
            }


            NOCAP.BLL.Master.SubDistrict obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
            obj_MiningNewApplication.ApplySubDistrictAreaCategoryKey = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory().SubDistrictAreaTypeCategoryKey;
            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_MiningNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;
            if (obj_MiningNewApplication.Update() == 1)
            {
                lblMessage.Text = "Saved Successfully";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                lblMessage.Text = obj_MiningNewApplication.CustumMessage;
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
                if (Page.IsValid)
                {
                    if (CheckAndAddAndUpdateMiningApplication() == 1) { Server.Transfer("~/ExternalUser/MiningNew/CommunicationAddress.aspx"); }
                    else { }
                }
            }
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        int int_StateCode;
        int int_DistricCode;
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
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
        int int_StateCode;
        int int_DistricCode;
        int int_SubDistrictCode;
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
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
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlTown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intStateCode;
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
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
    protected void rbtnWhetherGroundWaterUtilization_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExistingIndustry")
        //{
        //    RowDateOfCommencement.Visible = true;
        //    RowNOCObtainedForExistIND.Visible = false;
        //    rfvddlNOCObtainedForExistIND.Enabled = false;
        //    rfvtxtDateOfCommencement.Enabled = true;
        //}
        //else

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
    protected void imgbtnCalendar_Click(object sender, ImageClickEventArgs e)
    {

    }
}

