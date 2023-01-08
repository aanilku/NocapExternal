using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_InfrastructureRenew_InfrastructureRenew : System.Web.UI.Page
{
    string strPageName = "InfrastructureRenew";
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
                    if (NOCAPExternalUtility.FillDropDownMSMEType(ref ddMSMEType) != 1)
                    {
                        lblMessage.Text = "Problem in MSME Type";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    //reqValiTown.Enabled = false;
                    //reqValiVillage.Enabled = false;
                    if (PreviousPage != null)
                    {
                        Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                        if (placeHolder != null)
                        {
                            Label SourceLabel = (Label)placeHolder.FindControl("lblMode");
                            if (SourceLabel != null)
                            {
                                lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                            }
                            Label SourceLabelPreviousPage = (Label)placeHolder.FindControl("lblModeFrom");

                            if (SourceLabelPreviousPage != null)
                            {
                                lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabelPreviousPage.Text);
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
                                lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                            }
                            Label SourceLabelPreviousPage = (Label)placeHolder.FindControl("lblInfrastructureApplicationCodeFrom");

                            if (SourceLabelPreviousPage != null)
                            {
                                lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabelPreviousPage.Text);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblModeFrom.Text = "";
                    lblPageTitleFrom.Text = "";
                    lblInfrastructureApplicationCodeFrom.Text = "";
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
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
                    BindGeneralInformationLocationDetails(obj_infrastructureRenewSADApplication.FirstApplicationCode);
                    //txtNameOfInfraStructure.Text = obj_infrastructureRenewSADApplication.NameOfInfrastructure;
                    switch (obj_infrastructureRenewSADApplication.GroundWaterUtilizationFor)
                    {
                        case NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.GroundWaterUtilizationForOption.ExistingGroundWater:
                            rbtnWhetherGroundWaterUtilization.SelectedValue = "ExistingGW";
                            break;
                        case NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.GroundWaterUtilizationForOption.ExistingGWWithAdditionalGWRequirment:
                            rbtnWhetherGroundWaterUtilization.SelectedValue = "ExistingGWwithAdditionalGWRequirment";
                            break;
                    }
                    switch (obj_infrastructureRenewSADApplication.MSME)
                    {
                        case NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication.MSMEYesNo.Yes:
                            ddlMSME.SelectedValue = "Y";
                            break;
                        case NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication.MSMEYesNo.No:
                            ddlMSME.SelectedValue = "N";
                            break;
                        default:
                            break;
                    }

                    ddMSMEType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.MSMETypeCode));
                    if (obj_infrastructureRenewSADApplication.WaterQualityCode != null)
                        ddlWaterQualityType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.WaterQualityCode));

                }
                else
                {
                    NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication objInfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));

                    if (objInfrastructureRenewApplication != null && objInfrastructureRenewApplication.LinkDepth > 0)
                    {
                        BindGeneralInformationLocationDetails(Convert.ToInt64(objInfrastructureRenewApplication.FirstApplicationCode));
                    }
                    else
                    {
                        BindGeneralInformationLocationDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
                    }
                }

            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }


    private void BindGeneralInformationLocationDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(lngA_ApplicationCode);
            //NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplicationPre = null;
            //NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplicationPre = null;


            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));

            if (obj_infrastructureNewApplication != null && obj_infrastructureNewApplication.ApplicationCode > 0)
            {

                if (obj_infrastructureRenewApplication.LinkDepth > 0)
                {

                    if (obj_infrastructureRenewApplication.WaterQualityCodeFinally != null)
                        ddlWaterQualityType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewApplication.WaterQualityCodeFinally));
                    else
                        ddlWaterQualityType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewApplication.WaterQualityCode));

                }
                else
                {
                    if (obj_infrastructureNewApplication.WaterQualityCodeFinally != null)
                        ddlWaterQualityType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.WaterQualityCodeFinally));
                    else
                        ddlWaterQualityType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.WaterQualityCode));
                }

                txtNameOfInfraStructure.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.NameOfInfrastructure);
                txtAddressLine1.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1);
                txtAddressLine2.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2);
                txtAddressLine3.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3);

                ddlApplicationTypeCategory.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.ApplicationTypeCategoryCode));
                ddlState.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode));
                NOCAPExternalUtility.FillDropDownDistrict(ref ddlDistrict, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode);
                ddlDistrict.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode));
                NOCAPExternalUtility.FillDropDownSubDistrict(ref ddlSubDistrict, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode);
                ddlSubDistrict.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode));
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
                if (CheckAndAddAndUpdateInfrastructureApplication() == 1)
                {
                    Server.Transfer("~/ExternalUser/InfrastructureRenew/CommunicationAddress.aspx");
                }
            }
        }
    }
    private int CheckAndAddAndUpdateInfrastructureApplication()
    {
        try
        {
            if (lblModeFrom.Text.Trim() == "Edit")
            {
                if (UpdateGeneralInformationLocationDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text.Trim())) == 1)
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
    private int UpdateGeneralInformationLocationDetails(long lngA_ApplicationCode)
    {
        try
        {
            strActionName = "UpdateInfrastructureRenewApplication";
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));

            if (obj_infrastructureRenewSADApplication != null && obj_infrastructureRenewSADApplication.InfrastructureRenewApplicationCode > 0)
            {
                obj_infrastructureRenewSADApplication.NameOfInfrastructure = txtNameOfInfraStructure.Text;
                switch (rbtnWhetherGroundWaterUtilization.SelectedValue)
                {
                    case "ExistingGW":
                        obj_infrastructureRenewSADApplication.GroundWaterUtilizationFor = NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.GroundWaterUtilizationForOption.ExistingGroundWater;
                        break;

                    case "ExistingGWwithAdditionalGWRequirment":
                        obj_infrastructureRenewSADApplication.GroundWaterUtilizationFor = NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.GroundWaterUtilizationForOption.ExistingGWWithAdditionalGWRequirment;
                        break;
                }
                switch (ddlMSME.SelectedValue)
                {
                    case "Y":
                        obj_infrastructureRenewSADApplication.MSME = NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication.MSMEYesNo.Yes;
                        break;
                    case "N":
                        obj_infrastructureRenewSADApplication.MSME = NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication.MSMEYesNo.No;
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
                if (obj_infrastructureRenewSADApplication.MSME == NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.MSMEYesNo.Yes)
                {
                    obj_infrastructureRenewSADApplication.MSMETypeCode = Convert.ToInt32(ddMSMEType.SelectedValue);
                }


                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                obj_infrastructureRenewSADApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

                string str_state = "a";
                NOCAP.BLL.Master.StateGroundWaterAuthority obj_StateGroundWaterAuthority = new NOCAP.BLL.Master.StateGroundWaterAuthority(Convert.ToInt32(ddlState.SelectedValue), str_state);
                obj_infrastructureRenewSADApplication.WaterQualityCode =Convert.ToInt32(ddlWaterQualityType.SelectedValue.ToString());
                if (obj_StateGroundWaterAuthority.AddressStateCode == Convert.ToInt32(ddlState.SelectedValue))
                {
                    lblMessage.Text = "Renewal not allowed";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return 0;
                }
                else
                { //}
                    if (obj_infrastructureRenewSADApplication.Update() == 1)
                    {
                        lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewApplicationCode));
                        lblModeFrom.Text = "Edit";
                        strStatus = "Update Success";
                        lblMessage.Text = "Successfully Saved";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        return 1;
                    }
                    else
                    {
                        strStatus = "Update Failed";
                        lblMessage.Text = obj_infrastructureRenewSADApplication.CustumMessage;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return 0;
                    }
                }
            }
            else
            {
                strStatus = "Update Failed";
                lblMessage.Text = obj_infrastructureRenewSADApplication.CustumMessage;
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
            strActionName = "AddRenewInfrastructureApplication";
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication();
            obj_infrastructureRenewSADApplication.NameOfInfrastructure = txtNameOfInfraStructure.Text;
            switch (rbtnWhetherGroundWaterUtilization.SelectedValue)
            {
                case "ExistingGW":
                    obj_infrastructureRenewSADApplication.GroundWaterUtilizationFor = NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.GroundWaterUtilizationForOption.ExistingGroundWater;
                    break;

                case "ExistingGWwithAdditionalGWRequirment":
                    obj_infrastructureRenewSADApplication.GroundWaterUtilizationFor = NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.GroundWaterUtilizationForOption.ExistingGWWithAdditionalGWRequirment;
                    break;
            }

            obj_infrastructureRenewSADApplication.ApplicationCodeForApplyingRenew = Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text);

            //obj_infrastructureRenewSADApplication.LinkedInfrastructureNewApplicationCode = Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text);


            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));

            obj_infrastructureRenewSADApplication.CreatedByExUC = obj_externalUser.ExternalUserCode;
            switch (ddlMSME.SelectedValue)
            {
                case "Y":
                    obj_infrastructureRenewSADApplication.MSME = NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication.MSMEYesNo.Yes;
                    break;
                case "N":
                    obj_infrastructureRenewSADApplication.MSME = NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication.MSMEYesNo.No;
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
            if (obj_infrastructureRenewSADApplication.MSME == NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.MSMEYesNo.Yes)
            {
                obj_infrastructureRenewSADApplication.MSMETypeCode = Convert.ToInt32(ddMSMEType.SelectedValue);
            }

            string str_state = "a";
            NOCAP.BLL.Master.StateGroundWaterAuthority obj_StateGroundWaterAuthority = new NOCAP.BLL.Master.StateGroundWaterAuthority(Convert.ToInt32(ddlState.SelectedValue), str_state);
            obj_infrastructureRenewSADApplication.WaterQualityCode = Convert.ToInt32(ddlWaterQualityType.SelectedValue.ToString());
            if (obj_StateGroundWaterAuthority.AddressStateCode == Convert.ToInt32(ddlState.SelectedValue))
            {
                lblMessage.Text = "Renewal not allowed";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            else
            { //}
                if (obj_infrastructureRenewSADApplication.Add() == 1)
                {
                    strStatus = "Add Success";
                    lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.InfrastructureRenewApplicationCode));
                    lblModeFrom.Text = "Edit";
                    lblPageTitleFrom.Text = "Self";
                    lblMessage.Text = "Successfully Saved";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    return 1;
                }
                else
                {
                    strStatus = "Add Failed";
                    lblMessage.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewSADApplication.CustumMessage);
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
                    CheckAndAddAndUpdateInfrastructureApplication();
                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
                }
            }
        }
    }
}