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

public partial class ExternalUser_IndustrialRenew_ExistingNOCIssued : System.Web.UI.Page
{
    string strPageName = "INDRenewExistingNOCIssued";
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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblIndustialApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                        }
                    }

                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    BindCommunicationDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
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
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplicationPrevious = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplicationPrevious = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication();

        try
        {
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewSADApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);


            if (obj_industrialRenewSADApplication != null && obj_industrialRenewSADApplication.IndustrialRenewApplicationCode > 0)
            {
                lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_industrialRenewSADApplication.NameOfIndustry);
                //txtNOCLetterNo.Text = HttpUtility.HtmlEncode(obj_industrialRenewSADApplication.IndustrialRenewExistingNOC.ExistingNOCLetterNumber);
                //if (obj_industrialRenewSADApplication.IndustrialRenewExistingNOC.ExistingNOCDateOfIssuance != null)
                //    txtDateofIssuance.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_industrialRenewSADApplication.IndustrialRenewExistingNOC.ExistingNOCDateOfIssuance).ToShortDateString());
                //if (obj_industrialRenewSADApplication.IndustrialRenewExistingNOC.ExistingNOCValidityStartDate != null)
                //    txtValidityStartDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_industrialRenewSADApplication.IndustrialRenewExistingNOC.ExistingNOCValidityStartDate).ToShortDateString());
                //if (obj_industrialRenewSADApplication.IndustrialRenewExistingNOC.ExistingNOCValidityEndDate != null)
                //    txtValidityEndDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_industrialRenewSADApplication.IndustrialRenewExistingNOC.ExistingNOCValidityEndDate).ToShortDateString());
                txtReasonfornotapplyingforrenewal.Text = HttpUtility.HtmlEncode(obj_industrialRenewSADApplication.IndustrialRenewExistingNOC.ExistingNOCReasonForNotApplyBeforeExpiry);
            }


            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewSADApplicationCurrent = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);
            obj_industrialRenewSADApplicationCurrent.GetPreviousIndustrialApplication(out obj_industrialNewApplicationPrevious, out obj_industrialRenewApplicationPrevious);
            if (obj_industrialNewApplicationPrevious != null && obj_industrialNewApplicationPrevious.IndustrialNewApplicationCode > 0)
            {
                lblNOCLetterNo.Text = HttpUtility.HtmlEncode(obj_industrialNewApplicationPrevious.NOCNumber);
                lblDateofIssuance.Text = HttpUtility.HtmlEncode(obj_industrialNewApplicationPrevious.GetIssuedLetter().IssueLetterDate.ToShortDateString());
                lblValidityStartDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_industrialNewApplicationPrevious.GetIssuedLetter().ValidityStartDate).ToShortDateString());
                lblValidityEndDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_industrialNewApplicationPrevious.GetIssuedLetter().ValidityEndDate).ToShortDateString());
            }

            // work pending due to non compliation of renewal issuletter

            if (obj_industrialRenewApplicationPrevious != null && obj_industrialRenewApplicationPrevious.IndustrialRenewApplicationCode > 0)
            {
                lblNOCLetterNo.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplicationPrevious.NOCNumber);
                lblDateofIssuance.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplicationPrevious.GetIssuedLetter().IssueLetterDate.ToShortDateString());
                lblValidityStartDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_industrialRenewApplicationPrevious.GetIssuedLetter().ValidityStartDate).ToShortDateString());
                lblValidityEndDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_industrialRenewApplicationPrevious.GetIssuedLetter().ValidityEndDate).ToShortDateString());

            }


        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally
        {
            obj_industrialNewApplicationPrevious = null;
            obj_industrialRenewApplicationPrevious = null;
        }

    }

    private int UpdateCommunicationDetails(long lngA_ApplicationCode)
    {
        if (Page.IsValid)
        {
            try
            {
                strActionName = "Update Existing NOC Issued";
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);

                //obj_industrialRenewApplication.IndustrialRenewExistingNOC.ExistingNOCLetterNumber = txtNOCLetterNo.Text;
                //obj_industrialRenewApplication.IndustrialRenewExistingNOC.ExistingNOCDateOfIssuance =Convert.ToDateTime(txtDateofIssuance.Text);
                //obj_industrialRenewApplication.IndustrialRenewExistingNOC.ExistingNOCValidityStartDate = Convert.ToDateTime(txtValidityStartDate.Text);
                //obj_industrialRenewApplication.IndustrialRenewExistingNOC.ExistingNOCValidityEndDate = Convert.ToDateTime(txtValidityEndDate.Text);
                obj_industrialRenewApplication.IndustrialRenewExistingNOC.ExistingNOCReasonForNotApplyBeforeExpiry = txtReasonfornotapplyingforrenewal.Text;

                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                obj_industrialRenewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

                if (obj_industrialRenewApplication.Update() == 1)
                {
                    strStatus = "Update Success";
                    lblMessage.Text = "Successfully Saved";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    return 1;
                }
                else
                {
                    lblMessage.Text = obj_industrialRenewApplication.CustumMessage;
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
                    if (UpdateCommunicationDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text)) == 1)
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
                    if (UpdateCommunicationDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text)) == 1)
                    {
                        Server.Transfer("~/ExternalUser/IndustrialRenew/WaterRequirementDetails.aspx");

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

            Server.Transfer("~/ExternalUser/IndustrialRenew/CommunicationAddress.aspx");
        }
    }



}