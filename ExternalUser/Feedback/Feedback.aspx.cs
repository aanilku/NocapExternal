using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_Feedback_Feedback : System.Web.UI.Page
{
    string strPageName = "Feedback";
    string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                ValidationExpInit();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;

                NOCAPRating.CurrentRating = 5;
                BindUserInformation(Convert.ToInt64(Session["ExternalUserCode"]));
                FillDropDownApplicationNumber();
            }
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "function", "GetData();", true);
    }

    private void ValidationExpInit()
    {
        revtxtFeedbackDescription.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtFeedbackDescription.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtFeedbackDescription.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("1000");
        revLengthtxtFeedbackDescription.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("1000");
    }
    class CommonAppNumber
    {
        public long ApplicationCode { get; set; }
        public string ApplicationNumber { get; set; }
    }

    protected void FillDropDownApplicationNumber()
    {
        try
        {
            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));

            List<CommonAppNumber> CommcustomList = new List<CommonAppNumber>();

            List<CommonAppNumber> indcustomList = obj_externalUser.GetSubmittedIndustrialNewApplicationList().Where(x => x.SubmittedType == NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication.SubmittedTypeOption.Online).Select(x => new CommonAppNumber { ApplicationCode = x.IndustrialNewApplicationCode, ApplicationNumber = x.IndustrialNewApplicationNumber }).ToList();

            List<CommonAppNumber> infcustomList = obj_externalUser.GetSubmittedInfrastructureNewApplicationList().Where(x => x.SubmittedType == NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication.SubmittedTypeOption.Online).Select(x => new CommonAppNumber { ApplicationCode = x.ApplicationCode, ApplicationNumber = x.InfrastructureNewApplicationNumber }).ToList();

            List<CommonAppNumber> mincustomList = obj_externalUser.GetSubmittedMiningNewApplicationList().Where(x => x.SubmittedType == NOCAP.BLL.Mining.New.Application.MiningNewApplication.SubmittedTypeOption.Online).Select(x => new CommonAppNumber { ApplicationCode = x.ApplicationCode, ApplicationNumber = x.MiningNewApplicationNumber }).ToList();

            CommcustomList.AddRange(indcustomList);
            CommcustomList.AddRange(infcustomList);
            CommcustomList.AddRange(mincustomList);


            ddlApplicationNumber.DataSource = CommcustomList;
            ddlApplicationNumber.DataTextField = "ApplicationNumber";
            ddlApplicationNumber.DataValueField = "ApplicationCode";
            ddlApplicationNumber.DataBind();


            ListItem ps = new ListItem();
            ps.Value = "";
            ps.Text = "--Select--";
            ps.Selected = true;


            ddlApplicationNumber.Items.Insert(0, ps);

        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
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
                    strActionName = "Add Feedback";

                    NOCAP.BLL.Feedback.Feedback obj_feedback = new NOCAP.BLL.Feedback.Feedback();
                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    obj_feedback.ExternalUserCode = obj_externalUser.ExternalUserCode;
                    obj_feedback.ApplicationCode = Convert.ToInt64(ddlApplicationNumber.SelectedValue);
                    obj_feedback.FeedbackDescription = Convert.ToString(txtFeedbackDescription.Text);
                    obj_feedback.NOCAPRate = NOCAPRating.CurrentRating;
                    obj_feedback.ActionPerformByUserCode = Convert.ToInt64(Session["ExternalUserCode"]);

                    if (obj_feedback.Add() == 1)
                    {
                        strStatus = "Add Success";
                        lblMessage.Text = obj_feedback.CustumMessage;
                        lblMessage.ForeColor = System.Drawing.Color.Green;

                        txtFeedbackDescription.Text = string.Empty;
                        ddlApplicationNumber.SelectedValue = "";
                        lblProjectName.Text = string.Empty;
                        lblApplicationStatus.Text = string.Empty;
                    }
                    else
                    {
                        strStatus = "Add Failed";
                        lblMessage.Text = obj_feedback.CustumMessage;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (ThreadAbortException)
                {

                }
                catch (Exception)
                {
                    Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
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
        }
    }
    private void BindUserInformation(long lngA_ExternalUserCode)
    {
        try
        {
            NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(lngA_ExternalUserCode);
            if (obj_ExternalUser != null)
            {
                lblUserName.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_ExternalUser.ExternalUserFirstName) + " " + Convert.ToString(obj_ExternalUser.ExternalUserLastName));
                lblLoginName.Text = HttpUtility.HtmlEncode(obj_ExternalUser.ExternalUserName);
                lblEmailID.Text = HttpUtility.HtmlEncode(obj_ExternalUser.ExternalUserEmailID);
                lblMobileNumber.Text = HttpUtility.HtmlEncode(("+" + obj_ExternalUser.ExternalUserMobileNumberISD) + "-" + (obj_ExternalUser.ExternalUserMobileNumber));
            }
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
    }
    protected void ddlApplicationNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlApplicationNumber.SelectedValue != "")
            {
                GetINDINFMINDetails();
            }
            else
            {
                lblProjectName.Text = string.Empty;
                lblApplicationStatus.Text = string.Empty;
            }
        }
        catch
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
    }

    public void GetINDINFMINDetails()
    {

        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication();

        NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_industrialNewApplication, out obj_infrastructureNewApplication, out obj_miningNewApplication, Convert.ToInt64(ddlApplicationNumber.SelectedValue));

        if (obj_industrialNewApplication != null)
        {
            if (obj_industrialNewApplication.NameOfIndustry != "")
            {
                lblProjectName.Text = "<b>Project Name: </b>" + HttpUtility.HtmlEncode(obj_industrialNewApplication.NameOfIndustry);
            }
            else
            {
                lblProjectName.Text = "";
            }

            if (obj_industrialNewApplication.GetApplicationStatus().ApplicationStatusDescription != "")
            {
                lblApplicationStatus.Text = "<b>Application Status: </b>" + HttpUtility.HtmlEncode(obj_industrialNewApplication.GetApplicationStatus().ApplicationStatusDescription);
            }
            else
            {
                lblApplicationStatus.Text = "";
            }
        }
        else if (obj_infrastructureNewApplication != null)
        {
            if (obj_infrastructureNewApplication.NameOfInfrastructure != "")
            {
                lblProjectName.Text = "<b>Project Name: </b>" + HttpUtility.HtmlEncode(obj_infrastructureNewApplication.NameOfInfrastructure);
            }
            else
            {
                lblProjectName.Text = "";
            }
            if (obj_infrastructureNewApplication.GetApplicationStatus().ApplicationStatusDescription != "")
            {
                lblApplicationStatus.Text = "<b>Application Status: </b>" + HttpUtility.HtmlEncode(obj_infrastructureNewApplication.GetApplicationStatus().ApplicationStatusDescription);
            }
            else
            {
                lblApplicationStatus.Text = "";
            }

            // lblApplicationStatus.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.GetApplicationStatus().ApplicationStatusDescription);
        }
        else if (obj_miningNewApplication != null)
        {
            if (obj_miningNewApplication.NameOfMining != "")
            {
                lblProjectName.Text = "<b>Project Name: </b>" + HttpUtility.HtmlEncode(obj_miningNewApplication.NameOfMining);
            }
            else
            {
                lblProjectName.Text = "";
            }
            if (obj_miningNewApplication.GetApplicationStatus().ApplicationStatusDescription != "")
            {
                lblApplicationStatus.Text = "<b>Application Status: </b>" + HttpUtility.HtmlEncode(obj_miningNewApplication.GetApplicationStatus().ApplicationStatusDescription);
            }
            else
            {
                lblApplicationStatus.Text = "";
            }
            //lblApplicationStatus.Text = HttpUtility.HtmlEncode(obj_miningNewApplication.GetApplicationStatus().ApplicationStatusDescription);
        }
        else
        {
            ViewState["AppCode"] = null;
            lblMessage.Text = " Application Code Not Exists.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Visible = true;
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;

            txtFeedbackDescription.Text = string.Empty;
            ddlApplicationNumber.SelectedValue = "";
            NOCAPRating.CurrentRating = 5;
            lblApplicationStatus.Text = "";
            lblProjectName.Text = "";
        }
    }
    protected void lnkbtnGivenFeedback_Click(object sender, EventArgs e)
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
                Response.Redirect("Reports/FeedbackReportViewer.aspx");
            }
        }
    }
}