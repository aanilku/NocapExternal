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

public partial class ExternalUser_IndustrialNew_IndustrialRenew : System.Web.UI.Page
{
    string strPageName = "INDRenewIndustrialNew";
    string strActionName = "";
    string strStatus = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            if (!IsPostBack)
            {

                //txtDOB_CalendarExtender.EndDate = System.DateTime.Now;
                //txtDateOfExpansion_CalendarExtender.EndDate = System.DateTime.Now;
                //ValidationExpInit();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
                try
                {
                    //reqValiTown.Enabled = false;
                    //reqValiVillage.Enabled = false;

                    if (PreviousPage != null)
                    {
                        Control placeHolder =
                            PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                        if (placeHolder != null)
                        {
                            Label SourceLabel = (Label)placeHolder.FindControl("lblMode");

                            if (SourceLabel != null)
                            {
                                lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);  //add html encode

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
                        Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                        if (placeHolder != null)
                        {
                            Label SourceLabel = (Label)placeHolder.FindControl("lblApplicationCode");
                            if (SourceLabel != null)
                            {
                                lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);  // add html encode
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


                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));

                    BindGeneralInformationLocationDetails(obj_industrialRenewApplication.FirstApplicationCode);
                    switch (obj_industrialRenewApplication.GroundWaterUtilizationFor)
                    {
                        case NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication.GroundWaterUtilizationForOption.ExistingGroundWater:
                            rbtnWhetherGroundWaterUtilization.SelectedValue = "ExistingGW";
                            break;

                        case NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication.GroundWaterUtilizationForOption.ExistingGWWithAdditionalGWRequirment:
                            rbtnWhetherGroundWaterUtilization.SelectedValue = "ExistingGWwithAdditionalGWRequirment";
                            break;
                    }
                    if (obj_industrialRenewApplication.WaterQualityCode != null)
                        ddlWaterQualityType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.WaterQualityCode));

                }
                else
                {
                    NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication objIndustrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));

                    if (objIndustrialRenewApplication != null && objIndustrialRenewApplication.LinkDepth > 0)
                    {
                        BindGeneralInformationLocationDetails(Convert.ToInt64(objIndustrialRenewApplication.FirstApplicationCode));
                    }
                    else
                    {
                        BindGeneralInformationLocationDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
                    }
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }

    private int AddGeneralInformationLocationDetails()
    {
        try
        {
            strActionName = "AddRenewIndustiralApplication";
            //NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialSADRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication();
            obj_industrialSADRenewApplication.NameOfIndustry = lblNameOfIndustry.Text;

            switch (ddlMSME.SelectedValue)
            {
                case "Y":
                    obj_industrialSADRenewApplication.MSME = NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication.MSMEYesNo.Yes;
                    break;
                case "N":
                    obj_industrialSADRenewApplication.MSME = NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication.MSMEYesNo.No;
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
            if (obj_industrialSADRenewApplication.MSME == NOCAP.BLL.Industrial.Renew.Common.IndustrialRenewApplicationB.MSMEYesNo.Yes)
            {
                obj_industrialSADRenewApplication.MSMETypeCode = Convert.ToInt32(ddMSMEType.SelectedValue);
            }
            else
            {
                obj_industrialSADRenewApplication.MSMETypeCode = null;
            }
            switch (rbtnWhetherGroundWaterUtilization.SelectedValue)
            {
                case "ExistingGW":
                    obj_industrialSADRenewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication.GroundWaterUtilizationForOption.ExistingGroundWater;
                    break;

                case "ExistingGWwithAdditionalGWRequirment":
                    obj_industrialSADRenewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication.GroundWaterUtilizationForOption.ExistingGWWithAdditionalGWRequirment;
                    break;
            }

            obj_industrialSADRenewApplication.ApplicationCodeForApplyingRenew = Convert.ToInt64(lblIndustialApplicationCodeFrom.Text);

            //   obj_industrialSADRenewApplication.LinkedIndustrialNewApplicationCode = Convert.ToInt64(lblIndustialApplicationCodeFrom.Text);

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));

            obj_industrialSADRenewApplication.CreatedByExUC = obj_externalUser.ExternalUserCode;
            string str_state = "a";
            NOCAP.BLL.Master.StateGroundWaterAuthority obj_StateGroundWaterAuthority = new NOCAP.BLL.Master.StateGroundWaterAuthority(Convert.ToInt32(ddlState.SelectedValue), str_state);

            obj_industrialSADRenewApplication.WaterQualityCode = Convert.ToInt32(ddlWaterQualityType.SelectedValue.ToString());
            if (obj_StateGroundWaterAuthority.AddressStateCode == Convert.ToInt32(ddlState.SelectedValue))
            {
                lblMessage.Text = "Renewal not allowed";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            else
            { //}
                if (obj_industrialSADRenewApplication.Add() == 1)
                {
                    strStatus = "Add Success";
                    lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(obj_industrialSADRenewApplication.IndustrialRenewApplicationCode);
                    lblModeFrom.Text = "Edit";
                    lblPageTitleFrom.Text = "Self";
                    lblMessage.Text = "Successfully Saved";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    return 1;
                }
                else
                {
                    strStatus = "Add Failed";
                    lblMessage.Text = obj_industrialSADRenewApplication.CustumMessage;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return 0;
                }

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

    private int UpdateGeneralInformationLocationDetails(long lngA_ApplicationCode)
    {
        try
        {
            strActionName = "UpdateIndustrialRenewApplication";
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));

            if (obj_industrialRenewApplication != null && obj_industrialRenewApplication.IndustrialRenewApplicationCode > 0)
            {
                obj_industrialRenewApplication.NameOfIndustry = lblNameOfIndustry.Text;

                switch (ddlMSME.SelectedValue)
                {
                    case "Y":
                        obj_industrialRenewApplication.MSME = NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication.MSMEYesNo.Yes;
                        break;
                    case "N":
                        obj_industrialRenewApplication.MSME = NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication.MSMEYesNo.No;
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
                if (obj_industrialRenewApplication.MSME == NOCAP.BLL.Industrial.Renew.Common.IndustrialRenewApplicationB.MSMEYesNo.Yes)
                {
                    obj_industrialRenewApplication.MSMETypeCode = Convert.ToInt32(ddMSMEType.SelectedValue);
                }
                else
                {
                    obj_industrialRenewApplication.MSMETypeCode = null;
                }
                switch (rbtnWhetherGroundWaterUtilization.SelectedValue)
                {
                    case "ExistingGW":
                        obj_industrialRenewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication.GroundWaterUtilizationForOption.ExistingGroundWater;
                        break;

                    case "ExistingGWwithAdditionalGWRequirment":
                        obj_industrialRenewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication.GroundWaterUtilizationForOption.ExistingGWWithAdditionalGWRequirment;
                        break;
                }


                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));

                obj_industrialRenewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

                string str_state = "a";
                NOCAP.BLL.Master.StateGroundWaterAuthority obj_StateGroundWaterAuthority = new NOCAP.BLL.Master.StateGroundWaterAuthority(Convert.ToInt32(ddlState.SelectedValue), str_state);
                obj_industrialRenewApplication.WaterQualityCode = Convert.ToInt32(ddlWaterQualityType.SelectedValue.ToString());
                if (obj_StateGroundWaterAuthority.AddressStateCode == Convert.ToInt32(ddlState.SelectedValue))
                {
                    lblMessage.Text = "Renewal not allowed";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return 0;
                }
                else
                { //}

                    if (obj_industrialRenewApplication.Update() == 1)
                    {
                        lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplication.IndustrialRenewApplicationCode);
                        lblModeFrom.Text = "Edit";


                        strStatus = "Update Success";
                        lblMessage.Text = "Successfully Saved";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        return 1;
                    }
                    else
                    {
                        strStatus = "Update Failed";
                        lblMessage.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplication.CustumMessage);
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return 0;
                    }
                }
            }
            else
            {
                strStatus = "Update Failed";
                lblMessage.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplication.CustumMessage);
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

    private int CheckAndAddAndUpdateIndustrialApplication()
    {
        try
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

        catch (Exception)
        {
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

    private void BindGeneralInformationLocationDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(lngA_ApplicationCode);
            
            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewSADApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
            switch (obj_industrialRenewSADApplication.MSME)
            {
                case NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication.MSMEYesNo.Yes:
                    ddlMSME.SelectedValue = "Y";
                    break;
                case NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication.MSMEYesNo.No:
                    ddlMSME.SelectedValue = "N";
                    break;
                default:
                    break;
            }
            ddMSMEType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewSADApplication.MSMETypeCode));

            if (obj_industrialNewApplication != null && obj_industrialNewApplication.IndustrialNewApplicationCode > 0)
            {
                if (obj_industrialRenewApplication.LinkDepth > 0)
                {                  

                    if (obj_industrialRenewApplication.WaterQualityCodeFinally != null)
                        ddlWaterQualityType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.WaterQualityCodeFinally));
                    else
                        ddlWaterQualityType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.WaterQualityCode));

                }
                else
                {
                    if (obj_industrialNewApplication.WaterQualityCodeFinally != null)
                        ddlWaterQualityType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.WaterQualityCodeFinally));
                    else
                        ddlWaterQualityType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.WaterQualityCode));

                }

                lblNameOfIndustry.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.NameOfIndustry);
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

            }
            else
            {
                lblMessage.Text = "Error on Page. Please Contact to Administrator !";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
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
                      Server.Transfer("~/ExternalUser/IndustrialRenew/CommunicationAddress.aspx");

                    //Server.Transfer("~/ExternalUser/IndustrialRenew/INDRenewOnlinePayment.aspx");
                    
                }
            }
        }
    }
}