using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Threading;

public partial class ExternalUser_InfrastructureRenew_ExistingNOCIssued : System.Web.UI.Page
{
    string strPageName = "INFRenewExistingNOCIssued";
    string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //rvtxtDateofIssuance.MaximumValue = DateTime.Now.ToShortDateString();
            //regfvtxtValidityStartDate.MaximumValue = DateTime.Now.ToShortDateString();
            //regfvtxtValidityEndDate.MaximumValue = DateTime.Now.ToShortDateString();
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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblInfrastructureApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                        }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    BindCommunicationDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
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

    private void BindCommunicationDetails(long lngA_ApplicationCode)
    {
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplicationPrevious = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplicationPrevious = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication();
        try
        {
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);

            if (obj_infrastructureRenewSADApplication != null && obj_infrastructureRenewSADApplication.InfrastructureRenewApplicationCode > 0)
            {
                lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewSADApplication.NameOfInfrastructure);
                txtReasonfornotapplyingforrenewal.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewSADApplication.InfrastructureRenewExistingNOC.ExistingNOCReasonForNotApplyBeforeExpiry);
            }

            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplicationCurrent = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);
            obj_infrastructureRenewSADApplicationCurrent.GetPreviousInfrastructureApplication(out obj_infrastructureNewApplicationPrevious, out obj_infrastructureRenewApplicationPrevious);
            if (obj_infrastructureNewApplicationPrevious != null && obj_infrastructureNewApplicationPrevious.ApplicationCode > 0)
            {
                lblNOCLetterNo.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplicationPrevious.NOCNumber);
                lblDateofIssuance.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplicationPrevious.GetIssuedLetter().IssueLetterDate.ToShortDateString());
                lblValidityStartDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_infrastructureNewApplicationPrevious.GetIssuedLetter().ValidityStartDate).ToShortDateString());
                lblValidityEndDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_infrastructureNewApplicationPrevious.GetIssuedLetter().ValidityEndDate).ToShortDateString());
            }

            if (obj_infrastructureRenewApplicationPrevious != null && obj_infrastructureRenewApplicationPrevious.InfrastructureRenewApplicationCode > 0)
            {
                lblNOCLetterNo.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewApplicationPrevious.NOCNumber);
                lblDateofIssuance.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewApplicationPrevious.GetIssuedLetter().IssueLetterDate.ToShortDateString());
                lblValidityStartDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_infrastructureRenewApplicationPrevious.GetIssuedLetter().ValidityStartDate).ToShortDateString());
                lblValidityEndDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_infrastructureRenewApplicationPrevious.GetIssuedLetter().ValidityEndDate).ToShortDateString());
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally
        {
            obj_infrastructureNewApplicationPrevious = null;
            obj_infrastructureRenewApplicationPrevious = null;
        }
    }
    private int UpdateCommunicationDetails(long lngA_ApplicationCode)
    {
        if (Page.IsValid)
        {
            try
            {
                strActionName = "Update Existing NOC Issued";
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);

                //obj_industrialRenewApplication.IndustrialRenewExistingNOC.ExistingNOCLetterNumber = txtNOCLetterNo.Text;
                //obj_industrialRenewApplication.IndustrialRenewExistingNOC.ExistingNOCDateOfIssuance =Convert.ToDateTime(txtDateofIssuance.Text);
                //obj_industrialRenewApplication.IndustrialRenewExistingNOC.ExistingNOCValidityStartDate = Convert.ToDateTime(txtValidityStartDate.Text);
                //obj_industrialRenewApplication.IndustrialRenewExistingNOC.ExistingNOCValidityEndDate = Convert.ToDateTime(txtValidityEndDate.Text);
                obj_infrastructureRenewSADApplication.InfrastructureRenewExistingNOC.ExistingNOCReasonForNotApplyBeforeExpiry = txtReasonfornotapplyingforrenewal.Text;

                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                obj_infrastructureRenewSADApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

                if (obj_infrastructureRenewSADApplication.Update() == 1)
                {
                    strStatus = "Update Success";
                    lblMessage.Text = "Successfully Saved";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    return 1;
                }
                else
                {
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
                    if (UpdateCommunicationDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text)) == 1)
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
                    if (UpdateCommunicationDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text)) == 1)
                    {
                        Server.Transfer("~/ExternalUser/InfrastructureRenew/WaterRequirementDetails.aspx");
                        //Response.Redirect("~/ExternalUser/InfrastructureRenew/WaterRequirementDetails.aspx");
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

            Server.Transfer("~/ExternalUser/InfrastructureRenew/CommunicationAddress.aspx");
        }
    }



}