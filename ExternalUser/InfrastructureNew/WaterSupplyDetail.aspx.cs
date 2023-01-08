using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_InfrastructureNew_WaterSupplyDetail : System.Web.UI.Page
{
    string strPageName = "INFWaterSupplyDetails";
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
                if (lblModeFrom.Text.Trim() == "Edit") { BindGVInfNewWaterSupplyDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text)); }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                ddlGovtWaterSupplyExists_SelectedIndexChanged(sender, e);
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "function", "GetData();", true);
    }

    private void ValidationExpInit()
    {
        revtxtGroundwaterAvailabilityDetails.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtGroundwaterAvailabilityDetails.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtGroundwaterAvailabilityDetails.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        revLengthtxtGroundwaterAvailabilityDetails.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");
    } 

    private void BindGVInfNewWaterSupplyDetails(long lngA_ApplicationCode)
    {
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
        if (obj_infrastructureNewApplication.LocalGovtWaterSupplyExists == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.lGWSNetworkExist.Yes)
        {
            txtGroundwaterAvailabilityDetails.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.LocalGovtWaterSupplyExitsDetails);
            ddlGovtWaterSupplyExists.SelectedValue = "Yes";
        }
        else { ddlGovtWaterSupplyExists.SelectedValue = "No"; }




        //category = Convert.ToString(new NOCAP.BLL.Master.ApplicationTypeCategory((int)obj_infrastructureNewApplication.ApplicationTypeCode, (int)obj_infrastructureNewApplication.ApplicationTypeCategoryCode).ApplicationTypeCategoryDesc);
        //if (category == "Residential apartment" || category == "Group housing" || category == "Government water Supply agencies")
        //{
        //    trWaterSupplyActionStatus.Visible = false;
        //    trWaterSupplyAgency.Visible = false;
        //    trWaterSupplyCommited.Visible = false;
        //    trWaterSupplyDenied.Visible = false;
        //}
        switch (obj_infrastructureNewApplication.ObtainedAppliedforWaterSupplyfromGovtPvtAgency.ObtainedAppliedWaterSupplyFromGovtPvtAgency)
        {
            case NOCAP.BLL.Common.ObtainedAppliedWaterSupply.ObtAppWSGovPriAgency.Applied:
                ddlWaterSupplyActionStatus.SelectedValue = "Applied";
                break;
            case NOCAP.BLL.Common.ObtainedAppliedWaterSupply.ObtAppWSGovPriAgency.Obtained:
                ddlWaterSupplyActionStatus.SelectedValue = "Obtained";
                break;
            case NOCAP.BLL.Common.ObtainedAppliedWaterSupply.ObtAppWSGovPriAgency.NotDefined:
                ddlWaterSupplyActionStatus.SelectedValue = "";
                break;
        }
        ddlWaterSupplyAgency.SelectedValue = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.ObtainedAppliedforWaterSupplyfromGovtPvtAgency.WaterSupplyGovtPvtAgency.Trim());
        
        switch (obj_infrastructureNewApplication.ObtainedAppliedforWaterSupplyfromGovtPvtAgency.WaterSupplyCommited)
        {
            case NOCAP.BLL.Common.ObtainedAppliedWaterSupply.GovtPriWSCommitted.Yes:
                ddlWaterSupplyCommited.SelectedValue = "Yes";
                break;
            case NOCAP.BLL.Common.ObtainedAppliedWaterSupply.GovtPriWSCommitted.No:
                ddlWaterSupplyCommited.SelectedValue = "No";
                break;
        }

        switch (obj_infrastructureNewApplication.ObtainedAppliedforWaterSupplyfromGovtPvtAgency.WaterSupplyDenied)
        {
            case NOCAP.BLL.Common.ObtainedAppliedWaterSupply.GovPriWSDenied.Yes:
                ddlWaterSupplyDenied.SelectedValue = "Yes";
                break;
            case NOCAP.BLL.Common.ObtainedAppliedWaterSupply.GovPriWSDenied.No:
                ddlWaterSupplyDenied.SelectedValue = "No";
                break;
        }

        //if (obj_infrastructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance == NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.Yes)
        //{
        //    txtEarlierAppliedGWClearDesc.Text = obj_infrastructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceDesc;
        //    chkboxEarlierAppliedGWClear.Checked = true;
        //}
        //else
        //{
        //    chkboxEarlierAppliedGWClear.Checked = false;
        //    txtEarlierAppliedGWClearDesc.Text = ""; 
        //}
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
                Server.Transfer("~/ExternalUser/InfrastructureNew/BreakUpOfWaterRequirment.aspx");
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
                UpdateInfNewWaterSupplyDetail(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
            }
        }
    }

    private int UpdateInfNewWaterSupplyDetail(long lngA_ApplicationCode)
    {
        try
        {
            strActionName = "UpdateWaterSupplyDetails";
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            if (ddlGovtWaterSupplyExists.SelectedValue == "Yes")
            {
                obj_infrastructureNewApplication.LocalGovtWaterSupplyExists = NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.lGWSNetworkExist.Yes;
                obj_infrastructureNewApplication.LocalGovtWaterSupplyExitsDetails = txtGroundwaterAvailabilityDetails.Text;
            }
            else { obj_infrastructureNewApplication.LocalGovtWaterSupplyExists = NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.lGWSNetworkExist.No; }

            switch (ddlWaterSupplyActionStatus.SelectedValue)
            {
                case "Applied":
                    obj_infrastructureNewApplication.ObtainedAppliedforWaterSupplyfromGovtPvtAgency.ObtainedAppliedWaterSupplyFromGovtPvtAgency = NOCAP.BLL.Common.ObtainedAppliedWaterSupply.ObtAppWSGovPriAgency.Applied;
                    break;
                case "Obtained":
                    obj_infrastructureNewApplication.ObtainedAppliedforWaterSupplyfromGovtPvtAgency.ObtainedAppliedWaterSupplyFromGovtPvtAgency = NOCAP.BLL.Common.ObtainedAppliedWaterSupply.ObtAppWSGovPriAgency.Obtained;
                    break;
                default:
                    obj_infrastructureNewApplication.ObtainedAppliedforWaterSupplyfromGovtPvtAgency.ObtainedAppliedWaterSupplyFromGovtPvtAgency = NOCAP.BLL.Common.ObtainedAppliedWaterSupply.ObtAppWSGovPriAgency.NotDefined;
                    break;
            }
            obj_infrastructureNewApplication.ObtainedAppliedforWaterSupplyfromGovtPvtAgency.WaterSupplyGovtPvtAgency = ddlWaterSupplyAgency.SelectedValue;

            switch (ddlWaterSupplyCommited.SelectedValue)
            {
                case "Yes":
                    obj_infrastructureNewApplication.ObtainedAppliedforWaterSupplyfromGovtPvtAgency.WaterSupplyCommited = NOCAP.BLL.Common.ObtainedAppliedWaterSupply.GovtPriWSCommitted.Yes;
                    break;
                case "No":
                    obj_infrastructureNewApplication.ObtainedAppliedforWaterSupplyfromGovtPvtAgency.WaterSupplyCommited = NOCAP.BLL.Common.ObtainedAppliedWaterSupply.GovtPriWSCommitted.No;
                    break;
            }
            switch (ddlWaterSupplyDenied.SelectedValue)
            {
                case "Yes":
                    obj_infrastructureNewApplication.ObtainedAppliedforWaterSupplyfromGovtPvtAgency.WaterSupplyDenied = NOCAP.BLL.Common.ObtainedAppliedWaterSupply.GovPriWSDenied.Yes;
                    break;
                case "No":
                    obj_infrastructureNewApplication.ObtainedAppliedforWaterSupplyfromGovtPvtAgency.WaterSupplyDenied = NOCAP.BLL.Common.ObtainedAppliedWaterSupply.GovPriWSDenied.No;
                    break;
            }

            //if (chkboxEarlierAppliedGWClear.Checked) 
            //{
            //    obj_infrastructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance = NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.Yes;
            //    obj_infrastructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceDesc = txtEarlierAppliedGWClearDesc.Text;
            //}
            //else { obj_infrastructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance = NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.No; }

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_infrastructureNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_infrastructureNewApplication.Update() == 1)
            {
                strStatus = "Update Successfully";
                lblMessage.Text = "Successfully Saved";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                strStatus = "Update Unsuccessfull";
                lblMessage.Text = obj_infrastructureNewApplication.CustumMessage;
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
                if (UpdateInfNewWaterSupplyDetail(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text)) == 1)
                { //Server.Transfer("~/ExternalUser/InfrastructureNew/ExistingGroundwaterAbstractionStructure.aspx");
                    Server.Transfer("~/ExternalUser/InfrastructureNew/OtherDetails.aspx");
                }
                else { }
            }
        }
    }
    //protected void chkboxEarlierAppliedGWClear_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkboxEarlierAppliedGWClear_CheckedChanged.Checked)
    //    {
    //        txtEarlierAppliedGWClearDesc.Enabled = true;
    //    }
    //    else 
    //    {
    //        txtEarlierAppliedGWClearDesc.Text = "";
    //        txtEarlierAppliedGWClearDesc.Enabled = false; 
    //    }
    //}
    //protected void gvIndRefLetter_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //}
    protected void ddlGovtWaterSupplyExists_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            if (ddlGovtWaterSupplyExists.SelectedValue == "Yes") { txtGroundwaterAvailabilityDetails.Enabled = true; }
            else
            {
                txtGroundwaterAvailabilityDetails.Text = "";
                txtGroundwaterAvailabilityDetails.Enabled = false;
            }
        }   
    }
}