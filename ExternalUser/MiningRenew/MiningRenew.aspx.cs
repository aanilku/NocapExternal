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

public partial class ExternalUser_MiningRenew_MiningRenew : System.Web.UI.Page
{

    string strPageName = "MiningRenew";
    string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
                try
                {
                    if (NOCAPExternalUtility.FillDropDownMSMEType(ref ddMSMEType) != 1)
                    {
                        lblMessage.Text = "Problem in MSME Type";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
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
                        Control placeHolder =
                            PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                        if (placeHolder != null)
                        {
                            Label SourceLabel =
                            (Label)placeHolder.FindControl("lblApplicationCode");
                            if (SourceLabel != null)
                            {
                                lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                            }
                            Label SourceLabelPreviousPage = (Label)placeHolder.FindControl("lblMiningApplicationCodeFrom");

                            if (SourceLabelPreviousPage != null)
                            {
                                lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabelPreviousPage.Text);
                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    lblModeFrom.Text = "";
                    lblPageTitleFrom.Text = "";
                    lblMiningApplicationCodeFrom.Text = "";
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


                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
                    BindGeneralInformationLocationDetails(obj_miningRenewApplication.FirstApplicationCode);
                    switch (obj_miningRenewApplication.GroundWaterUtilizationFor)
                    {
                        case NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication.GroundWaterUtilizationForOption.ExistingGroundWater:
                            rbtnWhetherGroundWaterUtilization.SelectedValue = "ExistingGW";
                            break;

                        case NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication.GroundWaterUtilizationForOption.ExistingGWWithAdditionalGWRequirment:
                            rbtnWhetherGroundWaterUtilization.SelectedValue = "ExistingGWwithAdditionalGWRequirment";
                            break;
                    }
                    switch (obj_miningRenewApplication.MSME)
                    {
                        case NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication.MSMEYesNo.Yes:
                            ddlMSME.SelectedValue = "Y";
                            break;
                        case NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication.MSMEYesNo.No:
                            ddlMSME.SelectedValue = "N";
                            break;
                        default:
                            break;
                    }

                    ddMSMEType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_miningRenewApplication.MSMETypeCode));
                    if (obj_miningRenewApplication.WaterQualityCode != null)
                        ddlWaterQualityType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_miningRenewApplication.WaterQualityCode));


                }

                else
                {
                    NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication objMiningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));

                    if (objMiningRenewApplication != null && objMiningRenewApplication.LinkDepth > 0)
                    {
                        BindGeneralInformationLocationDetails(objMiningRenewApplication.FirstApplicationCode);
                    }
                    else
                    {
                        BindGeneralInformationLocationDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
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
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(lngA_ApplicationCode);
            NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));

            if (obj_miningNewApplication != null && obj_miningNewApplication.ApplicationCode > 0)
            {
                if (obj_miningRenewApplication.LinkDepth > 0)
                {

                    if (obj_miningRenewApplication.WaterQualityCodeFinally != null)
                        ddlWaterQualityType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_miningRenewApplication.WaterQualityCodeFinally));
                    else
                        ddlWaterQualityType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_miningNewApplication.WaterQualityCode));

                }
                else
                {
                    if (obj_miningNewApplication.WaterQualityCodeFinally != null)
                        ddlWaterQualityType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_miningNewApplication.WaterQualityCodeFinally));
                    else
                        ddlWaterQualityType.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_miningNewApplication.WaterQualityCode));
                }

                lblNameOfMining.Text = HttpUtility.HtmlEncode(obj_miningNewApplication.NameOfMining);
                txtAddressLine1.Text = HttpUtility.HtmlEncode(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1);
                txtAddressLine2.Text = HttpUtility.HtmlEncode(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2);
                txtAddressLine3.Text = HttpUtility.HtmlEncode(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3);

                ddlApplicationTypeCategory.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_miningNewApplication.ApplicationTypeCategoryCode));
                ddlState.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode));
                NOCAPExternalUtility.FillDropDownDistrict(ref ddlDistrict, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode);
                ddlDistrict.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode));
                NOCAPExternalUtility.FillDropDownSubDistrict(ref ddlSubDistrict, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode);
                ddlSubDistrict.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode));
                if (NOCAPExternalUtility.FillDropDownTownOrVillage(ref ddlTownOrVillage) != 1)
                {
                    lblMessage.Text = "Problem in Village/Town population";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }

                if (NOCAPExternalUtility.FillDropDownVillage(ref ddlVillage, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode) != 1)
                {
                    lblMessage.Text = "Problem in Village population";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }

                if (NOCAPExternalUtility.FillDropDownTown(ref ddlTown, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode) != 1)
                {
                    lblMessage.Text = "Problem in Town population";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }

                switch (obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown)
                {
                    case NOCAP.BLL.Common.Address.VillageOrTownOption.Town:
                        ddlTownOrVillage.SelectedValue = "T";
                        ddlTown.Visible = true;
                        ddlTown.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode));
                        ddlVillage.Visible = false;
                        ddlVillage.SelectedIndex = 0;
                        break;
                    case NOCAP.BLL.Common.Address.VillageOrTownOption.Village:
                        ddlTown.Visible = false;
                        ddlVillage.Visible = true;
                        ddlTownOrVillage.SelectedValue = "V";
                        ddlVillage.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode));
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
                if (CheckAndAddAndUpdateMiningApplication() == 1)
                {
                    Server.Transfer("~/ExternalUser/MiningRenew/CommunicationAddress.aspx");
                }
            }
        }
    }
    private int CheckAndAddAndUpdateMiningApplication()
    {
        try
        {
            if (lblModeFrom.Text.Trim() == "Edit")
            {
                if (UpdateGeneralInformationLocationDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text.Trim())) == 1)
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
            strActionName = "UpdateMiningRenewApplication";
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewSADApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));

            
            if (obj_miningRenewSADApplication != null && obj_miningRenewSADApplication.MiningRenewApplicationCode > 0)
            {
                //obj_miningRenewSADApplication.NameOfMining = txtNameOfInfraStructure.Text;
                switch (rbtnWhetherGroundWaterUtilization.SelectedValue)
                {
                    case "ExistingGW":
                        obj_miningRenewSADApplication.GroundWaterUtilizationFor = NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.GroundWaterUtilizationForOption.ExistingGroundWater;
                        break;

                    case "ExistingGWwithAdditionalGWRequirment":
                        obj_miningRenewSADApplication.GroundWaterUtilizationFor = NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.GroundWaterUtilizationForOption.ExistingGWWithAdditionalGWRequirment;
                        break;
                }

                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                obj_miningRenewSADApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

                string str_state = "a";
                NOCAP.BLL.Master.StateGroundWaterAuthority obj_StateGroundWaterAuthority = new NOCAP.BLL.Master.StateGroundWaterAuthority(Convert.ToInt32(ddlState.SelectedValue), str_state);

                if (obj_StateGroundWaterAuthority.AddressStateCode == Convert.ToInt32(ddlState.SelectedValue))
                {
                    lblMessage.Text = "Renewal not allowed";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return 0;
                }
                else
                {
                    switch (ddlMSME.SelectedValue)
                    {
                        case "Y":
                            obj_miningRenewSADApplication.MSME = NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication.MSMEYesNo.Yes;
                            break;
                        case "N":
                            obj_miningRenewSADApplication.MSME = NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication.MSMEYesNo.No;
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
                    obj_miningRenewSADApplication.WaterQualityCode = Convert.ToInt32(ddlWaterQualityType.SelectedValue.ToString());
                    if (obj_miningRenewSADApplication.MSME == NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.MSMEYesNo.Yes)
                    {
                        obj_miningRenewSADApplication.MSMETypeCode = Convert.ToInt32(ddMSMEType.SelectedValue);
                    }
                    else
                    {
                        obj_miningRenewSADApplication.MSMETypeCode = null;
                    }
                    if (obj_miningRenewSADApplication.Update() == 1)
                    {
                        lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(obj_miningRenewSADApplication.MiningRenewApplicationCode);
                        lblModeFrom.Text = "Edit";
                        strStatus = "Update Success";
                        lblMessage.Text = "Successfully Saved";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        return 1;
                    }
                    else
                    {
                        strStatus = "Update Failed";
                        lblMessage.Text = obj_miningRenewSADApplication.CustumMessage;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return 0;
                    }
                }
               
            }
            else
            {
                strStatus = "Update Failed";
                lblMessage.Text = obj_miningRenewSADApplication.CustumMessage;
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
            strActionName = "AddRenewMiningApplication";
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewSADApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication();
            //obj_miningRenewSADApplication.NameOfInfrastructure = txtNameOfInfraStructure.Text;
            switch (rbtnWhetherGroundWaterUtilization.SelectedValue)
            {
                case "ExistingGW":
                    obj_miningRenewSADApplication.GroundWaterUtilizationFor = NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.GroundWaterUtilizationForOption.ExistingGroundWater;
                    break;

                case "ExistingGWwithAdditionalGWRequirment":
                    obj_miningRenewSADApplication.GroundWaterUtilizationFor = NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.GroundWaterUtilizationForOption.ExistingGWWithAdditionalGWRequirment;
                    break;
            }
            obj_miningRenewSADApplication.ApplicationCodeForApplyingRenew = Convert.ToInt64(lblMiningApplicationCodeFrom.Text);
            // obj_miningRenewSADApplication.LinkedMiningNewApplicationCode = Convert.ToInt64(lblMiningApplicationCodeFrom.Text);

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
            obj_miningRenewSADApplication.CreatedByExUC = obj_externalUser.ExternalUserCode;

            string str_state = "a";
            NOCAP.BLL.Master.StateGroundWaterAuthority obj_StateGroundWaterAuthority = new NOCAP.BLL.Master.StateGroundWaterAuthority(Convert.ToInt32(ddlState.SelectedValue), str_state);

            if (obj_StateGroundWaterAuthority.AddressStateCode == Convert.ToInt32(ddlState.SelectedValue))
            {
                lblMessage.Text = "Renewal not allowed";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            else
            {
                switch (ddlMSME.SelectedValue)
                {
                    case "Y":
                        obj_miningRenewSADApplication.MSME = NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication.MSMEYesNo.Yes;
                        break;
                    case "N":
                        obj_miningRenewSADApplication.MSME = NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication.MSMEYesNo.No;
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
                obj_miningRenewSADApplication.WaterQualityCode = Convert.ToInt32(ddlWaterQualityType.SelectedValue.ToString());
                if (obj_miningRenewSADApplication.MSME == NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.MSMEYesNo.Yes)
                {
                    obj_miningRenewSADApplication.MSMETypeCode = Convert.ToInt32(ddMSMEType.SelectedValue);
                }
                else
                {
                    obj_miningRenewSADApplication.MSMETypeCode = null;
                }
                if (obj_miningRenewSADApplication.Add() == 1)
                {
                    strStatus = "Add Success";
                    lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(obj_miningRenewSADApplication.MiningRenewApplicationCode);
                    lblModeFrom.Text = "Edit";
                    lblPageTitleFrom.Text = "Self";
                    lblMessage.Text = "Successfully Saved";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    return 1;
                }
                else
                {
                    strStatus = "Add Failed";
                    lblMessage.Text = HttpUtility.HtmlEncode(obj_miningRenewSADApplication.CustumMessage);
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
                if (CheckAndAddAndUpdateMiningApplication() == 1)
                {
                    lblMessage.Text = "Successfully Saved";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.Text = "Error on Page";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void Check_Click(object sender, EventArgs e)
    {

    }
}