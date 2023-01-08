using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_IndustrialNew_IndustrialNew : System.Web.UI.Page
{
    string strPageName = "INDIndustrialNew";
    string strActionName = "";
    string strStatus = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            lblMessage.Text = "";
            if (!IsPostBack)
            {

                txtDOB_CalendarExtender.EndDate = System.DateTime.Now;
                txtDateOfExpansion_CalendarExtender.EndDate = System.DateTime.Now;
                ValidationExpInit();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
                try
                {
                    reqValiTown.Enabled = false;
                    reqValiVillage.Enabled = false;

                    if (PreviousPage != null)
                    {
                        Control placeHolder =
                            PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                        if (placeHolder != null)
                        {
                            Label SourceLabel =
                            (Label)placeHolder.FindControl("lblMode");

                            if (SourceLabel != null)
                            {
                                lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);  // add html encode

                            }

                            Label SourceLabelPreviousPage = (Label)placeHolder.FindControl("lblModeFrom");

                            if (SourceLabelPreviousPage != null)
                            {
                                lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabelPreviousPage.Text);  // add html encode

                            }
                        }
                    }

                    if (PreviousPage != null)
                    {
                        Control placeHolder =
                            PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                        if (placeHolder != null)
                        {
                            Label SourceLabel =
                            (Label)placeHolder.FindControl("lblPageTitle");
                            if (SourceLabel != null)
                            {
                                lblPageTitleFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); // add html encode

                            }
                        }

                    }
                    if (PreviousPage != null)
                    {
                        Control placeHolder =
                            PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                        if (placeHolder != null)
                        {
                            Label SourceLabel =
                            (Label)placeHolder.FindControl("lblApplicationCode");
                            if (SourceLabel != null)
                            {
                                lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);  //add html encode

                            }
                            Label SourceLabelPreviousPage = (Label)placeHolder.FindControl("lblIndustialApplicationCodeFrom");

                            if (SourceLabelPreviousPage != null)
                            {
                                lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabelPreviousPage.Text);  //add html encode

                            }
                        }

                    }

                    if (NOCAPExternalUtility.FillDropDownMSMEType(ref ddMSMEType) != 1)
                    {
                        lblMessage.Text = "Problem in MSME Type";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }

                    //ddMSMEType.Enabled = false;
                    //rfvMSMEType.Enabled = false;                
                }
                catch (Exception ex)
                {
                    lblModeFrom.Text = "";
                    lblPageTitleFrom.Text = "";
                    lblIndustialApplicationCodeFrom.Text = "";

                }


                if (NOCAPExternalUtility.FillDropDownApplicationTypeCategory(ref ddlApplicationTypeCategory, NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Industrial")) != 1)
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


                if (lblIndustialApplicationCodeFrom.Text.Trim() != "")
                {
                    BindGeneralInformationLocationDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text), sender, e);
                }

                ddlTownOrVillage_SelectedIndexChanged(sender, e);
            }


        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }

    private void ValidationExpInit()
    {
        revtxtNameOfIndustry.ValidationExpression = ValidationUtility.txtValForProjectName;
        revtxtNameOfIndustry.ErrorMessage = ValidationUtility.txtValForProjectNameMsg;

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
    private int AddGeneralInformationLocationDetails()
    {
        try
        {
            strActionName = "AddNewIndustiralApplication";
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication();
            obj_industrialNewApplication.WaterQualityCode = Convert.ToInt32(ddlWaterQualityType.SelectedValue);
            obj_industrialNewApplication.ApplicationTypeCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Industrial");
            obj_industrialNewApplication.ApplicationPurposeCode = NOCAPExternalUtility.EquivalentApplicationPurposeCodeOfDatabaseForApplicationPurpose("New");
            obj_industrialNewApplication.ApplicationTypeCategoryCode = Convert.ToInt32(ddlApplicationTypeCategory.SelectedValue);
            // obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement = Convert.ToDecimal(txtGroundWaterRequirement.Text);

            obj_industrialNewApplication.NameOfIndustry = txtNameOfIndustry.Text;
            obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1 = txtAddressLine1.Text;
            obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2 = txtAddressLine2.Text;
            obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3 = txtAddressLine3.Text;
            obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode = Convert.ToInt32(ddlState.SelectedValue);
            obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode = Convert.ToInt32(ddlDistrict.SelectedValue);
            obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode = Convert.ToInt32(ddlSubDistrict.SelectedValue);
            obj_industrialNewApplication.UpToHundredKLD = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
            switch (ddlMSME.SelectedValue)
            {
                case "Y":
                    obj_industrialNewApplication.MSME = NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.MSMEYesNo.Yes;
                    break;
                case "N":
                    obj_industrialNewApplication.MSME = NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.MSMEYesNo.No;
                    break;
            }
            switch (ddlWetlandArea.SelectedValue)
            {
                case "Y":
                    obj_industrialNewApplication.WetLandArea = NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.WetLandAreaYesNo.Yes;
                    break;
                case "N":
                    obj_industrialNewApplication.WetLandArea = NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.WetLandAreaYesNo.No;
                    break;
            }
            switch (ddlTownOrVillage.SelectedValue)
            {
                case "V":
                    obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode = Convert.ToInt32(ddlVillage.SelectedValue);
                    obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown = NOCAP.BLL.Common.Address.VillageOrTownOption.Village;
                    break;
                case "T":
                    obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.TownCode = Convert.ToInt32(ddlTown.SelectedValue);
                    obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown = NOCAP.BLL.Common.Address.VillageOrTownOption.Town;
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
            if (obj_industrialNewApplication.MSME == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.MSMEYesNo.Yes)
            {
                //ddMSMEType.Visible = true;
                //ddMSMEType.Enabled = true;
                //rfvMSMEType.Enabled = true;
                obj_industrialNewApplication.MSMETypeCode = Convert.ToInt32(ddMSMEType.SelectedValue);
            }
            else
            {
                //ddMSMEType.Visible = false;
                //ddMSMEType.Enabled = false;
                //rfvMSMEType.Enabled = false;
                obj_industrialNewApplication.MSMETypeCode = null;
            }

            //if (ddMSMEType.SelectedValue == "")
            //{
            //    obj_industrialNewApplication.MSMETypeCode = null;
            //    rfvMSMEType.Enabled = false;
            //}
            //else
            //{
            //    obj_industrialNewApplication.MSMETypeCode = Convert.ToInt32(ddMSMEType.SelectedValue);
            //    rfvMSMEType.Enabled = true;
            //}
            obj_industrialNewApplication.ProposedLocation.ProposedLatitude = Convert.ToDecimal(txtProLat.Text);
            obj_industrialNewApplication.ProposedLocation.ProposedLongitude = Convert.ToDecimal(txtProLong.Text);

            switch (rbtnWhetherGroundWaterUtilization.SelectedValue)
            {
                case "NewIndustry":
                    obj_industrialNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.GroundWaterUtilizationForOption.NewIndustry;
                    obj_industrialNewApplication.DateOfCommencement = null;
                    obj_industrialNewApplication.DateOfExpansionOfProject = null;
                    obj_industrialNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                    break;
                case "ExistingIndustry":
                    obj_industrialNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.GroundWaterUtilizationForOption.ExistingIndustry;
                    switch (ddlNOCObtainedForExistIND.SelectedValue)
                    {
                        case "Y":
                            obj_industrialNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.Yes;
                            obj_industrialNewApplication.DateOfCommencement = null;
                            obj_industrialNewApplication.DateOfExpansionOfProject = null;
                            break;
                        case "N":
                            obj_industrialNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No;
                            obj_industrialNewApplication.DateOfCommencement = Convert.ToDateTime(txtDateOfCommencement.Text);
                            obj_industrialNewApplication.DateOfExpansionOfProject = null;
                            break;
                        default:
                            obj_industrialNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                            obj_industrialNewApplication.DateOfCommencement = null;
                            obj_industrialNewApplication.DateOfExpansionOfProject = null;
                            break;
                    }
                    break;
                case "ExpansionProgramExistingIndustry":
                    obj_industrialNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.GroundWaterUtilizationForOption.ExpansionProgramOfExitingIndustry;
                    switch (ddlNOCObtainedForExistIND.SelectedValue)
                    {
                        case "Y":
                            obj_industrialNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.Yes;
                            obj_industrialNewApplication.DateOfCommencement = null;
                            obj_industrialNewApplication.DateOfExpansionOfProject = null;
                            break;
                        case "N":
                            obj_industrialNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No;
                            obj_industrialNewApplication.DateOfCommencement = Convert.ToDateTime(txtDateOfCommencement.Text);
                            obj_industrialNewApplication.DateOfExpansionOfProject = Convert.ToDateTime(txtDateOfExpansion.Text);
                            break;
                        default:
                            obj_industrialNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                            obj_industrialNewApplication.DateOfCommencement = null;
                            obj_industrialNewApplication.DateOfExpansionOfProject = null;
                            break;
                    }
                    break;
                default:
                    obj_industrialNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.GroundWaterUtilizationForOption.NotDefined;
                    break;
            }

            NOCAP.BLL.Master.SubDistrict obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);

            obj_industrialNewApplication.ApplySubDistrictAreaCategoryKey = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory().SubDistrictAreaTypeCategoryKey;

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_industrialNewApplication.CreatedByExUC = obj_externalUser.ExternalUserCode;
            if (obj_industrialNewApplication.Add() == 1)
            {
                strStatus = "Add Success";
                lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.IndustrialNewApplicationCode);
                lblModeFrom.Text = "Edit";
                lblPageTitleFrom.Text = "Self";
                lblMessage.Text = "Successfully Saved";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                strStatus = "Add Failed";
                lblMessage.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.CustumMessage);
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }

        }
        catch (Exception)
        {
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


    private int UpdateGeneralInformationLocationDetails(long lngA_ApplicationCode)
    {
        try
        {
            strActionName = "UpdateIndustrialNewApplication";
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(lngA_ApplicationCode);
            obj_industrialNewApplication.WaterQualityCode = Convert.ToInt32(ddlWaterQualityType.SelectedValue);

            obj_industrialNewApplication.ApplicationTypeCategoryCode = Convert.ToInt32(ddlApplicationTypeCategory.SelectedValue);
            obj_industrialNewApplication.NameOfIndustry = txtNameOfIndustry.Text;
            obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1 = txtAddressLine1.Text;
            obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2 = txtAddressLine2.Text;
            obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3 = txtAddressLine3.Text;
            obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode = Convert.ToInt32(ddlState.SelectedValue);
            obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode = Convert.ToInt32(ddlDistrict.SelectedValue);
            obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode = Convert.ToInt32(ddlSubDistrict.SelectedValue);

            //static ground waret - delete it

            // obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement = Convert.ToDecimal(txtGroundWaterRequirement.Text);
            obj_industrialNewApplication.UpToHundredKLD = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
            switch (ddlMSME.SelectedValue)
            {
                case "Y":
                    obj_industrialNewApplication.MSME = NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.MSMEYesNo.Yes;
                    break;
                case "N":
                    obj_industrialNewApplication.MSME = NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.MSMEYesNo.No;
                    break;
            }
            switch (ddlWetlandArea.SelectedValue)
            {
                case "Y":
                    obj_industrialNewApplication.WetLandArea = NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.WetLandAreaYesNo.Yes;
                    break;
                case "N":
                    obj_industrialNewApplication.WetLandArea = NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.WetLandAreaYesNo.No;
                    break;
            }
            switch (ddlTownOrVillage.SelectedValue)
            {
                case "V":
                    obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode = Convert.ToInt32(ddlVillage.SelectedValue);
                    obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown = NOCAP.BLL.Common.Address.VillageOrTownOption.Village;
                    break;
                case "T":
                    obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.TownCode = Convert.ToInt32(ddlTown.SelectedValue);
                    obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown = NOCAP.BLL.Common.Address.VillageOrTownOption.Town;
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
            if (obj_industrialNewApplication.MSME == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.MSMEYesNo.Yes)
            {
                //ddMSMEType.Visible = true;
                //ddMSMEType.Enabled = true;
                //rfvMSMEType.Enabled = true;
                obj_industrialNewApplication.MSMETypeCode = Convert.ToInt32(ddMSMEType.SelectedValue);
            }
            else
            {
                //ddMSMEType.Visible = false;
                //ddMSMEType.Enabled = false;
                //rfvMSMEType.Enabled = false;
                obj_industrialNewApplication.MSMETypeCode = null;
            }

            //if (ddMSMEType.SelectedValue == "")
            //{
            //    obj_industrialNewApplication.MSMETypeCode = null;
            //    rfvMSMEType.Enabled = false;
            //}
            //else
            //{
            //    obj_industrialNewApplication.MSMETypeCode = Convert.ToInt32(ddMSMEType.SelectedValue);
            //    rfvMSMEType.Enabled = true;
            //}
            obj_industrialNewApplication.ProposedLocation.ProposedLatitude = Convert.ToDecimal(txtProLat.Text);
            obj_industrialNewApplication.ProposedLocation.ProposedLongitude = Convert.ToDecimal(txtProLong.Text);

            switch (rbtnWhetherGroundWaterUtilization.SelectedValue)
            {
                case "NewIndustry":
                    obj_industrialNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.GroundWaterUtilizationForOption.NewIndustry;
                    obj_industrialNewApplication.DateOfCommencement = null;
                    obj_industrialNewApplication.DateOfExpansionOfProject = null;
                    obj_industrialNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                    break;
                case "ExistingIndustry":
                    obj_industrialNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.GroundWaterUtilizationForOption.ExistingIndustry;
                    switch (ddlNOCObtainedForExistIND.SelectedValue)
                    {
                        case "Y":
                            obj_industrialNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.Yes;
                            obj_industrialNewApplication.DateOfCommencement = null;
                            obj_industrialNewApplication.DateOfExpansionOfProject = null;
                            break;
                        case "N":
                            obj_industrialNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No;
                            obj_industrialNewApplication.DateOfCommencement = Convert.ToDateTime(txtDateOfCommencement.Text);
                            obj_industrialNewApplication.DateOfExpansionOfProject = null;
                            break;
                        default:
                            obj_industrialNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                            obj_industrialNewApplication.DateOfCommencement = null;
                            obj_industrialNewApplication.DateOfExpansionOfProject = null;
                            break;
                    }
                    break;
                case "ExpansionProgramExistingIndustry":
                    obj_industrialNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.GroundWaterUtilizationForOption.ExpansionProgramOfExitingIndustry;
                    switch (ddlNOCObtainedForExistIND.SelectedValue)
                    {
                        case "Y":
                            obj_industrialNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.Yes;
                            obj_industrialNewApplication.DateOfCommencement = null;
                            obj_industrialNewApplication.DateOfExpansionOfProject = null;
                            break;
                        case "N":
                            obj_industrialNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No;
                            obj_industrialNewApplication.DateOfCommencement = Convert.ToDateTime(txtDateOfCommencement.Text);
                            obj_industrialNewApplication.DateOfExpansionOfProject = Convert.ToDateTime(txtDateOfExpansion.Text);
                            break;
                        default:
                            obj_industrialNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                            obj_industrialNewApplication.DateOfCommencement = null;
                            obj_industrialNewApplication.DateOfExpansionOfProject = null;
                            break;
                    }
                    break;
                default:
                    obj_industrialNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.GroundWaterUtilizationForOption.NotDefined;
                    break;
            }


            NOCAP.BLL.Master.SubDistrict obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);

            obj_industrialNewApplication.ApplySubDistrictAreaCategoryKey = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory().SubDistrictAreaTypeCategoryKey;

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_industrialNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_industrialNewApplication.Update() == 1)
            {
                strStatus = "Update Success";
                lblMessage.Text = "Successfully Saved";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                strStatus = "Update Failed";
                lblMessage.Text = obj_industrialNewApplication.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }



        }
        catch (Exception ex)
        {
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

    private int CheckAndAddAndUpdateIndustrialApplication()
    {
        try
        {

            int int_applicationTypeCategoryCode = Convert.ToInt32(ddlApplicationTypeCategory.SelectedValue);
            int int_stateCode = Convert.ToInt32(ddlState.SelectedValue);
            int int_districtCode = Convert.ToInt32(ddlDistrict.SelectedValue);
            int int_subDistrictCode = Convert.ToInt32(ddlSubDistrict.SelectedValue);
            int int_applicationTypeCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Industrial");
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
                lblMessage.Text = "Industrial New Application  - Area Type Category not defined";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;

            }


            //check for area type not defined




            if (obj_ApplicationAllowOrNotForApply.ProceedForFinalApply == NOCAP.BLL.Master.ApplicationAllowOrNotForApply.ApplicationAllowNotAllowForApply.Allow)
            {
                int Status = 1;
                CheckMSMEWithAreaTypeCat(out Status);
                if (Status == 1)
                {
                    if (lblModeFrom.Text.Trim() == "Edit")
                    {
                        if (UpdateGeneralInformationLocationDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text.Trim())) == 1)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        if (AddGeneralInformationLocationDetails() == 1)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else
                {

                    return 0;
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
                    //{
                    lblMessage.Text = obj_ApplicationAllowOrNotForApply.DisplayMessage;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    //}
                }

                return 0;


            }
        }

        catch (Exception)
        {
            //lblMessage.Text = obj_ApplicationAllowOrNotForApply.CustumMessage;
            // lblMessage.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
            return 0;
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
                try
                {
                    CheckAndAddAndUpdateIndustrialApplication();

                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
                }
            }
        }
    }

    private void BindGeneralInformationLocationDetails(long lngA_ApplicationCode, Object sender, EventArgs e)
    {
        try
        {
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(lngA_ApplicationCode);
            //txtGroundWaterRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement));

            ddlWaterQualityType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.WaterQualityCode));
            txtNameOfIndustry.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.NameOfIndustry);
            txtAddressLine1.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1);
            txtAddressLine2.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2);
            txtAddressLine3.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3);

            ddlApplicationTypeCategory.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.ApplicationTypeCategoryCode));
            ddlState.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode));
            NOCAPExternalUtility.FillDropDownDistrict(ref ddlDistrict, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode);
            ddlDistrict.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode));
            NOCAPExternalUtility.FillDropDownSubDistrict(ref ddlSubDistrict, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode);
            ddlSubDistrict.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode));
            if (NOCAPExternalUtility.FillDropDownTownOrVillage(ref ddlTownOrVillage) != 1)
            {
                lblMessage.Text = "Problem in Village/Town population";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            if (NOCAPExternalUtility.FillDropDownVillage(ref ddlVillage, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode) != 1)
            {
                lblMessage.Text = "Problem in Village population";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            if (NOCAPExternalUtility.FillDropDownTown(ref ddlTown, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode) != 1)
            {
                lblMessage.Text = "Problem in Town population";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            switch (obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown)
            {
                case NOCAP.BLL.Common.Address.VillageOrTownOption.Town:
                    ddlTownOrVillage.SelectedValue = "T";
                    ddlTown.Visible = true;
                    ddlTown.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.TownCode));
                    ddlVillage.Visible = false;
                    ddlVillage.SelectedIndex = 0;
                    break;
                case NOCAP.BLL.Common.Address.VillageOrTownOption.Village:
                    ddlTown.Visible = false;
                    ddlVillage.Visible = true;
                    ddlTownOrVillage.SelectedValue = "V";
                    ddlVillage.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode));
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

            txtProLat.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.ProposedLocation.ProposedLatitude);
            txtProLong.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.ProposedLocation.ProposedLongitude);

            switch (obj_industrialNewApplication.GroundWaterUtilizationFor)
            {
                case NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.GroundWaterUtilizationForOption.NewIndustry:
                    rbtnWhetherGroundWaterUtilization.SelectedValue = "NewIndustry";
                    break;
                case NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.GroundWaterUtilizationForOption.ExistingIndustry:
                    rbtnWhetherGroundWaterUtilization.SelectedValue = "ExistingIndustry";
                    break;
                case NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.GroundWaterUtilizationForOption.ExpansionProgramOfExitingIndustry:
                    rbtnWhetherGroundWaterUtilization.SelectedValue = "ExpansionProgramExistingIndustry";
                    break;
            }
            rbtnWhetherGroundWaterUtilization_SelectedIndexChanged(sender, e);
            if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExistingIndustry" || rbtnWhetherGroundWaterUtilization.SelectedValue == "ExpansionProgramExistingIndustry")
            {
                switch (obj_industrialNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND)
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
                    if (!String.IsNullOrEmpty(Convert.ToString((obj_industrialNewApplication.DateOfCommencement))))
                    {
                        txtDateOfCommencement.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_industrialNewApplication.DateOfCommencement).ToString("dd/MM/yyyy"));
                    }
                    if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExpansionProgramExistingIndustry")
                    {
                        if (!String.IsNullOrEmpty(Convert.ToString(obj_industrialNewApplication.DateOfExpansionOfProject)))
                        {
                            txtDateOfExpansion.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_industrialNewApplication.DateOfExpansionOfProject).ToString("dd/MM/yyyy"));
                        }
                    }
                }
            }
            switch (obj_industrialNewApplication.MSME)
            {
                case NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.MSMEYesNo.Yes:
                    ddlMSME.SelectedValue = "Y";
                    break;
                case NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.MSMEYesNo.No:
                    ddlMSME.SelectedValue = "N";
                    break;
                default:
                    break;
            }
            switch (obj_industrialNewApplication.WetLandArea)
            {
                case NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.WetLandAreaYesNo.Yes:
                    ddlWetlandArea.SelectedValue = "Y";
                    break;
                case NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.WetLandAreaYesNo.No:
                    ddlWetlandArea.SelectedValue = "N";
                    break;
                default:
                    break;
            }
            ddMSMEType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.MSMETypeCode));
        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
            //lblMessage.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }

    public void FFillDropDownApplicationTypeCategory(int int_ApplicationTypeCategoryCode, int intApplicationTypeCode)
    {

        try
        {

            if (NOCAPExternalUtility.FillDropDownApplicationTypeCategory(ref ddlApplicationTypeCategory, intApplicationTypeCode) != 1)
            {
                lblMessage.Text = "Problem in Application Type Category population";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            ddlApplicationTypeCategory.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(int_ApplicationTypeCategoryCode));
        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
            //lblMessage.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);

        }


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
                if (ddlState.SelectedValue == "")
                {

                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlDistrict);
                }
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
                //lblMessage.Text = ex.Message;
                //lblMessage.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
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

                if (ddlDistrict.SelectedValue == "")
                {
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlSubDistrict);
                }
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
                //lblMessage.Text = ex.Message;
                //lblMessage.ForeColor = System.Drawing.Color.Red;
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
                // lblMessage.Text = ex.Message;
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
            try
            {


                switch (ddlTownOrVillage.SelectedValue)
                {
                    case "T":
                        ddlTown.Visible = true;
                        ddlVillage.Visible = false;
                        reqValiVillage.Enabled = false;
                        reqValiTown.Enabled = true;
                        break;
                    case "V":
                        ddlTown.Visible = false;
                        ddlVillage.Visible = true;
                        reqValiTown.Enabled = false;
                        reqValiVillage.Enabled = true;
                        break;
                    case "":
                        ddlTown.Visible = false;
                        ddlVillage.Visible = false;
                        reqValiTown.Enabled = false;
                        reqValiVillage.Enabled = false;
                        break;
                    default:
                        ddlTown.Visible = false;
                        ddlVillage.Visible = false;
                        reqValiTown.Enabled = false;
                        reqValiVillage.Enabled = false;
                        break;
                }

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
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
                int int_applicationTypeCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Industrial");
                int int_applicationPurposeCode = NOCAPExternalUtility.EquivalentApplicationPurposeCodeOfDatabaseForApplicationPurpose("New");
                int int_applicationTypeCategoryCode = Convert.ToInt32(ddlApplicationTypeCategory.SelectedValue);
                int int_WaterQualityTypeCode = Convert.ToInt32(ddlWaterQualityType.SelectedValue);

                NOCAP.BLL.Master.ApplicationAllowOrNotForApply obj_ApplicationAllowOrNotForApply = new NOCAP.BLL.Master.ApplicationAllowOrNotForApply(int_applicationTypeCode, int_applicationPurposeCode, int_applicationTypeCategoryCode, int_stateCode, int_districtCode, int_subDistrictCode, int_WaterQualityTypeCode);
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

    protected void ddlApplicationTypeCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            lblMessage.Text = "";
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
                if (CheckAndAddAndUpdateIndustrialApplication() == 1)
                {

                    Server.Transfer("~/ExternalUser/IndustrialNew/CommunicationAddress.aspx");


                }
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
            rvtxtDateOfCommencement.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
            RowDateOfCommencement.Visible = true;
            rfvtxtDateOfCommencement.Enabled = true;
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
    /// <summary>
    /// Status 1 for Allow and 0 for not allow
    /// </summary>
    /// <returns></returns>
    private void CheckMSMEWithAreaTypeCat(out int Status)
    {
        Status = 1;
        NOCAP.BLL.Master.SubDistrictAreaTypeCategory obj_SubDistrictAreaTypeCategory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategory(Convert.ToInt32(ddlState.SelectedValue.ToString()), Convert.ToInt32(ddlDistrict.SelectedValue.ToString()), Convert.ToInt32(ddlSubDistrict.SelectedValue.ToString()));
        NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(Convert.ToInt32(ddlState.SelectedValue.ToString()), Convert.ToInt32(ddlDistrict.SelectedValue.ToString()), Convert.ToInt32(ddlSubDistrict.SelectedValue.ToString()), obj_SubDistrictAreaTypeCategory.SubDistrictCurrentAreaCategoryKey);
        if (ddlWaterQualityType.SelectedValue.ToString() == "1")
        {
            if (rbtnWhetherGroundWaterUtilization.SelectedValue == "NewIndustry")
            {
                if (obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode == 5)//Over-Exploited
                {
                    if (ddlMSME.SelectedValue.ToString() == "Y")
                    {
                        if (ddlApplicationTypeCategory.SelectedValue.ToString() != "73")
                            Status = 1;//  return 1;                        
                        else
                        {
                            lblMessage.Text = "Packaged Drinking Water (MSME) is not allowed in Over-Exploited Area.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            Status = 0;//  return 0;
                        }
                    }
                    else
                        Status = 1;
                }
                else
                    Status = 1;
            }
            else if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExistingIndustry")
                Status = 1;
            else if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExpansionProgramExistingIndustry")
            {
                if (obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode == 5)//Over-Exploited
                {
                    lblMessage.Text = "Application is not allowed in Over-Exploited Area with ExpansionProgramExistingIndustry.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    Status = 0;
                }
                else
                    Status = 1;
            }
        }
        else
            Status = 1;


    }

    //protected void ddlMSME_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication();
    //    if (obj_industrialNewApplication.MSME == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.MSMEYesNo.Yes)
    //    {
    //        //ddMSMEType.Visible = true;
    //        //ddMSMEType.Enabled = true;
    //        //rfvMSMEType.Enabled = true;
    //    }
    //    else
    //    {
    //        //ddMSMEType.Visible = false;
    //        //ddMSMEType.Enabled = false;
    //        //rfvMSMEType.Enabled = false;
    //    }
    //}
}