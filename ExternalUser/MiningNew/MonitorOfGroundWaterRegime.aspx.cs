using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_Mining_MonitorOfGroundWaterRegime : System.Web.UI.Page
{
    string strPageName = "MINMonitorOfGroundWaterRegime";
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
                if (lblModeFrom.Text.Trim() == "Edit") { BindGVMinGroundWaterRegimeDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)); }
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

        revtxtNoOfWellsPiezoMetersProToMonitor.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtNoOfWellsPiezoMetersProToMonitor.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtNoOfWellsPiezoMetersProToMonitor.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("200");
        revLengthtxtNoOfWellsPiezoMetersProToMonitor.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("200");

        revtxtNOfPiezometerSurrounding.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtNOfPiezometerSurrounding.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtNOfPiezometerSurrounding.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("100");
        revLengthtxtNOfPiezometerSurrounding.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("100");

        revtxtWSSurrounding.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtWSSurrounding.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtWSSurrounding.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("100");
        revLengthtxtWSSurrounding.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("100");

        revtxtAnyOtherItem.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtAnyOtherItem.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtAnyOtherItem.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("100");
        revLengthtxtAnyOtherItem.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("100");
    }

    private void BindGVMinGroundWaterRegimeDetails(long lngA_ApplicationCode)
    {
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);

        txtMonitoGWRegimeDetails.Text = obj_MiningNewApplication.MiningNewMonitoringGWRegime.MonitoringGWRegimeLocationDetailofWells;
        txtNoOfWellsPiezometers.Text = obj_MiningNewApplication.MiningNewMonitoringGWRegime.MonitoringGWRegimeNumberOfWells;
        txtGWLevelofObservationwellsPiezometer.Text = obj_MiningNewApplication.MiningNewMonitoringGWRegime.MonitoringGWDetailOfLevelOfObservationWells;
        txtNoOfWellsPiezoMetersProToMonitor.Text = obj_MiningNewApplication.MiningNewMonitoringGWRegime.MonitoringGWNumberOfwellsProposedToMonitor;
        txtNOfPiezometerSurrounding.Text = obj_MiningNewApplication.MiningNewMonitoringGWRegime.MonitoringGWNumberOfPiezometerProposedToMonitorinSurrounding;
        //txtNoOfPiezometersProToConstructSurrounding.Text = obj_MiningNewApplication.MiningNewMonitoringGWRegime.MonitoringGWGeneralQualityOfGWInSurrounding;
        txtWSSurrounding.Text = obj_MiningNewApplication.MiningNewMonitoringGWRegime.MonitoringGWGeneralQualityOfGWInSurrounding;
        txtAnyOtherItem.Text = obj_MiningNewApplication.MiningNewMonitoringGWRegime.MonitoringGWAnyOtherItem;
    }

    private int UpdateMinMonitorOfGroundWaterRegimeDetail(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
            obj_MiningNewApplication.MiningNewMonitoringGWRegime.MonitoringGWRegimeLocationDetailofWells = txtMonitoGWRegimeDetails.Text;
            obj_MiningNewApplication.MiningNewMonitoringGWRegime.MonitoringGWRegimeNumberOfWells = txtNoOfWellsPiezometers.Text;
            obj_MiningNewApplication.MiningNewMonitoringGWRegime.MonitoringGWDetailOfLevelOfObservationWells = txtGWLevelofObservationwellsPiezometer.Text;
            obj_MiningNewApplication.MiningNewMonitoringGWRegime.MonitoringGWNumberOfwellsProposedToMonitor = txtNoOfWellsPiezoMetersProToMonitor.Text;
            obj_MiningNewApplication.MiningNewMonitoringGWRegime.MonitoringGWNumberOfPiezometerProposedToMonitorinSurrounding = txtNOfPiezometerSurrounding.Text;
            //txtNoOfPiezometersProToConstructSurrounding.Text = obj_MiningNewApplication.MiningNewMonitoringGWRegime.MonitoringGWGeneralQualityOfGWInSurrounding;
            obj_MiningNewApplication.MiningNewMonitoringGWRegime.MonitoringGWGeneralQualityOfGWInSurrounding = txtWSSurrounding.Text;
            obj_MiningNewApplication.MiningNewMonitoringGWRegime.MonitoringGWAnyOtherItem = txtAnyOtherItem.Text;
            
            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_MiningNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_MiningNewApplication.Update() == 1)
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

                lblMessage.Text = obj_MiningNewApplication.CustumMessage;
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
            Server.Transfer("~/ExternalUser/MiningNew/ProposedUtiizationOfPumpedWater.aspx");
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
                UpdateMinMonitorOfGroundWaterRegimeDetail(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
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
                if (UpdateMinMonitorOfGroundWaterRegimeDetail(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)) == 1) { Server.Transfer("~/ExternalUser/MiningNew/DetailsExistingGroundwaterAbstractionStructure.aspx"); }
                else { }
            }
        }
    }
}