using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Threading;
using System.Globalization;

public partial class ExternalUser_MiningRenew_ExistingNOCIssued : System.Web.UI.Page
{
    string strPageName = "MINRenewExistingNOCIssued";
    string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            validationPropertySet();
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
                        if (SourceLabel != null)
                        {
                            lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                        }
                    }
                }
                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblMiningApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                        }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    BindExistingNOCDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {

                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "function", "GetData();", true);
    }

    void validationPropertySet()
    {
        revtxtReasonfornotapplyingforrenewal.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtReasonfornotapplyingforrenewal.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtReasonfornotapplyingforrenewal.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("100");
        revLengthtxtReasonfornotapplyingforrenewal.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("100");

    }

    private void BindExistingNOCDetails(long lngA_ApplicationCode)
    {
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplicationPrevious = new NOCAP.BLL.Mining.New.Application.MiningNewApplication();
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplicationPrevious = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication();
        try
        {
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewSADApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = obj_miningRenewSADApplication.GetFirstMiningApplication();

            if (obj_miningRenewSADApplication != null && obj_miningRenewSADApplication.MiningRenewApplicationCode > 0)
            {
                lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.NameOfMining);
                txtReasonfornotapplyingforrenewal.Text = HttpUtility.HtmlEncode(obj_miningRenewSADApplication.MiningRenewExistingNOC.ExistingNOCReasonForNotApplyBeforeExpiry);
            }
             
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewSADApplicationCurrent = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
            obj_miningRenewSADApplicationCurrent.GetPreviousMiningApplication(out obj_miningNewApplicationPrevious, out obj_miningRenewApplicationPrevious);
            if (obj_miningNewApplicationPrevious != null && obj_miningNewApplicationPrevious.ApplicationCode > 0)
            {
                lblNOCLetterNo.Text = HttpUtility.HtmlEncode(obj_miningNewApplicationPrevious.NOCNumber);
                lblDateofIssuance.Text = HttpUtility.HtmlEncode(obj_miningNewApplicationPrevious.GetIssuedLetter().IssueLetterDate.ToShortDateString());
                lblValidityStartDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_miningNewApplicationPrevious.GetIssuedLetter().ValidityStartDate).ToShortDateString());
                lblValidityEndDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_miningNewApplicationPrevious.GetIssuedLetter().ValidityEndDate).ToShortDateString());
            }
            if (obj_miningRenewApplicationPrevious != null && obj_miningRenewApplicationPrevious.MiningRenewApplicationCode > 0)
            {
                lblNOCLetterNo.Text = HttpUtility.HtmlEncode(obj_miningRenewApplicationPrevious.NOCNumber);
                lblDateofIssuance.Text = HttpUtility.HtmlEncode(obj_miningRenewApplicationPrevious.GetIssuedLetter().IssueLetterDate.ToShortDateString());
                lblValidityStartDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_miningRenewApplicationPrevious.GetIssuedLetter().ValidityStartDate).ToShortDateString());
                lblValidityEndDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_miningRenewApplicationPrevious.GetIssuedLetter().ValidityEndDate).ToShortDateString());
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally
        {
            obj_miningNewApplicationPrevious = null;
            obj_miningRenewApplicationPrevious = null;
        }

    }

    private int UpdateExistingNOCDetails(long lngA_ApplicationCode)
    {
        if (Page.IsValid)
        {
            try
            {
                strActionName = "Update Existing NOC Issued";
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
                
                obj_miningRenewApplication.MiningRenewExistingNOC.ExistingNOCReasonForNotApplyBeforeExpiry = txtReasonfornotapplyingforrenewal.Text;

                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                obj_miningRenewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

                if (obj_miningRenewApplication.Update() == 1)
                {
                    strStatus = "Update Success";
                    lblMessage.Text = "Successfully Saved";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    return 1;
                }
                else
                {
                    lblMessage.Text = obj_miningRenewApplication.CustumMessage;
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
        return 0;
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
            try
            {

                if (Page.IsValid)
                {
                    if (UpdateExistingNOCDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text)) == 1)
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

            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void txtNext_Click(object sender, EventArgs e)
    {
        string str_RedirectPath = "";
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;

            try
            {

                if (Page.IsValid)
                {
                    if (UpdateExistingNOCDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)) == 1)
                    {
                        Server.Transfer("~/ExternalUser/MiningRenew/LandUseDetails.aspx");
                    }
                }
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
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

            Server.Transfer("~/ExternalUser/MiningRenew/CommunicationAddress.aspx");
        }
    }
}