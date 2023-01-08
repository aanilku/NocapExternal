using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_MiningRenew_MonitorOfGroundWaterRegime : System.Web.UI.Page
{

    string strPageName = "MINRenMonitorOfGroundWaterRegime";
    string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ValidationExpInit();
            lblMessage.Text = "";
            try
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;

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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblMiningApplicationCodeFrom");
                        if (SourceLabel != null) { lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit") { BindGVMinRenewalGroundWaterRegimeDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)); }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {

            }

        }
        ScriptManager.RegisterStartupScript(this, GetType(), "function", "GetData();", true);

    }

    private void ValidationExpInit()
    {
        revtxtMonitoGWRegimeDetails.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtMonitoGWRegimeDetails.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtMonitoGWRegimeDetails.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        revLengthtxtMonitoGWRegimeDetails.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");


        revtxtNoOfWellsPiezometers.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtNoOfWellsPiezometers.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtNoOfWellsPiezometers.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        revLengthtxtNoOfWellsPiezometers.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");


        revtxtGWLevelofObservationwellsPiezometer.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtGWLevelofObservationwellsPiezometer.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtGWLevelofObservationwellsPiezometer.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("100");
        revLengthtxtGWLevelofObservationwellsPiezometer.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("100");

        revtxtWSSurrounding.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtWSSurrounding.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtWSSurrounding.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("100");
        revLengthtxtWSSurrounding.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("100");

        revtxtAnyOtherItem.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtAnyOtherItem.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtAnyOtherItem.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("100");
        revLengthtxtAnyOtherItem.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("100");

        revtxtDetailChangeGWRegim.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtDetailChangeGWRegim.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtDetailChangeGWRegim.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("100");
        revLengthtxtDetailChangeGWRegim.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("100");

    }

    private void BindGVMinRenewalGroundWaterRegimeDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Mining.New.Application.MiningNewApplication objMiningNewApplication = obj_MiningRenewApplication.GetFirstMiningApplication();

            if (objMiningNewApplication != null)
            {
                lblHeadingProjName.Text = HttpUtility.HtmlEncode(objMiningNewApplication.NameOfMining);
            }

            txtMonitoGWRegimeDetails.Text = HttpUtility.HtmlEncode(obj_MiningRenewApplication.MiningRenewMonitoringGWRegime.MonitoringGWRegimeLocationDetailofWells);
            txtNoOfWellsPiezometers.Text = HttpUtility.HtmlEncode(obj_MiningRenewApplication.MiningRenewMonitoringGWRegime.MonitoringGWRegimeNumberOfWells);
            txtGWLevelofObservationwellsPiezometer.Text = HttpUtility.HtmlEncode(obj_MiningRenewApplication.MiningRenewMonitoringGWRegime.MonitoringGWDetailOfLevelOfObservationWells);

            txtWSSurrounding.Text = HttpUtility.HtmlEncode(obj_MiningRenewApplication.MiningRenewMonitoringGWRegime.MonitoringGWGeneralQualityOfGWInSurrounding);
            txtAnyOtherItem.Text = HttpUtility.HtmlEncode(obj_MiningRenewApplication.MiningRenewMonitoringGWRegime.MonitoringGWAnyOtherItem);

            txtDetailChangeGWRegim.Text = HttpUtility.HtmlEncode(obj_MiningRenewApplication.DetailsChangeGWRegime);
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private int UpdateMinRenewMonitorOfGroundWaterRegimeDetail(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
            obj_MiningRenewApplication.MiningRenewMonitoringGWRegime.MonitoringGWRegimeLocationDetailofWells = txtMonitoGWRegimeDetails.Text;
            obj_MiningRenewApplication.MiningRenewMonitoringGWRegime.MonitoringGWRegimeNumberOfWells = txtNoOfWellsPiezometers.Text;
            obj_MiningRenewApplication.MiningRenewMonitoringGWRegime.MonitoringGWDetailOfLevelOfObservationWells = txtGWLevelofObservationwellsPiezometer.Text;
            obj_MiningRenewApplication.MiningRenewMonitoringGWRegime.MonitoringGWGeneralQualityOfGWInSurrounding = txtWSSurrounding.Text;
            obj_MiningRenewApplication.MiningRenewMonitoringGWRegime.MonitoringGWAnyOtherItem = txtAnyOtherItem.Text;
            obj_MiningRenewApplication.DetailsChangeGWRegime = txtDetailChangeGWRegim.Text;

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_MiningRenewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_MiningRenewApplication.Update() == 1)
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

                lblMessage.Text = obj_MiningRenewApplication.CustumMessage;
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
            Server.Transfer("~/ExternalUser/MiningRenew/DetailsUtiizationOfPumpedWater.aspx");
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
            if (Page.IsValid)
            {
                UpdateMinRenewMonitorOfGroundWaterRegimeDetail(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
            }
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            if (Page.IsValid)
            {
                if (UpdateMinRenewMonitorOfGroundWaterRegimeDetail(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)) == 1) { Server.Transfer("~/ExternalUser/MiningRenew/DetailsExistingGroundwaterAbstractionStructure.aspx"); }
                else { }
            }
        }
    }
}