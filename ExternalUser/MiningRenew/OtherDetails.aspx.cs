using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
public partial class ExternalUser_MiningRenew_OtherDetails : System.Web.UI.Page
{
    string strPageName = "MINRenewOtherDetails";
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
                            lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);                      
                    }
                }
                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblMiningApplicationCodeFrom");
                        if (SourceLabel != null)
                            lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit")
                    BindOtherDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "function", "GetData();", true);
    }

    #region Private Method
    private void ValidationExpInit()
    {
        revtxtGainfulUtilazatoinOfPumpWaterDetails.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtGainfulUtilazatoinOfPumpWaterDetails.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtGainfulUtilazatoinOfPumpWaterDetails.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("2000");
        revLengthtxtGainfulUtilazatoinOfPumpWaterDetails.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("2000");

        revtxtRrainwaterDetails.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtRrainwaterDetails.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtRrainwaterDetails.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        revLengthtxtRrainwaterDetails.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");
    }
    private void BindOtherDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = obj_miningRenewApplication.GetFirstMiningApplication();
            
            lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_miningNewApplication.NameOfMining);

            txtGainfulUtilazatoinOfPumpWaterDetails.Text = HttpUtility.HtmlEncode(obj_miningRenewApplication.GainfulUtilizationofPumpedWaterDesc);
            txtRrainwaterDetails.Text = HttpUtility.HtmlEncode(obj_miningRenewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc);
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    private int UpdateOtherDetails(long lngA_ApplicationCode)
    {
        try
        {

            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);

            if (txtGainfulUtilazatoinOfPumpWaterDetails.Text.Trim() == "")
                obj_miningRenewApplication.GainfulUtilizationofPumpedWaterDesc = "";
            else
                obj_miningRenewApplication.GainfulUtilizationofPumpedWaterDesc = txtGainfulUtilazatoinOfPumpWaterDetails.Text.Trim();
            if (txtRrainwaterDetails.Text.Trim() == "")
                obj_miningRenewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc = "";
            else
                obj_miningRenewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc = txtRrainwaterDetails.Text.Trim();



            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_miningRenewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_miningRenewApplication.Update() == 1)
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

                lblMessage.Text = obj_miningRenewApplication.CustumMessage;
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

    #endregion

    #region Button Click
    protected void txtNext_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                if (UpdateOtherDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text)) == 1)
                    Server.Transfer("Attachment.aspx");
            }
        }
    }
    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                try
                {
                    UpdateOtherDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
                }
            }
        }
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            Server.Transfer("ComplianceConditionNOCOther.aspx");
        }
    }


    #endregion
}