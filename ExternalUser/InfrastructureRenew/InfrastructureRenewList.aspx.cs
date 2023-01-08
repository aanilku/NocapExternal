using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExternalUser_InfrastructureRenew_InfrastructureRenewList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblModeFrom.Text = "";
            lblInfrastructureApplicationCodeFrom.Text = "";
            if (!IsPostBack)
            {
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;
                BindInfRenewEligibleApplication();
                BindInfNthRenewEligibleApplication();
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindInfRenewEligibleApplication()
    {
        try
        {
            ViewState["InfRenewSno"] = 0;  //For Sr No
            if (NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                gvInfRenewEligibleApplication.DataSource = obj_externalUser.GetSubmittedInfrastructureNewApplicationList();
                gvInfRenewEligibleApplication.DataBind();
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void BindInfNthRenewEligibleApplication()
    {
        try
        {
            ViewState["InfNthRenewSno"] = 0;  //For Sr No
            if (NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                gvInfNthRenewEligibleApplication.DataSource = obj_externalUser.GetSubmittedInfrastructureRenewApplicationList();
                gvInfNthRenewEligibleApplication.DataBind();
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    protected void gvInfRenewEligibleApplication_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lbInfRenewSno = new Label();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                lbInfRenewSno = (Label)e.Row.FindControl("lbInfRenewSno");
                Label lblIssueLetterStartDate = (Label)e.Row.FindControl("lblIssueLetterStartDate");
                Label lblIssueLetterEndDate = (Label)e.Row.FindControl("lblIssueLetterEndDate");

                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(Convert.ToInt64(gvInfRenewEligibleApplication.DataKeys[e.Row.RowIndex].Value));
                string strStatusMessage = "";
                string strCustMessage = "";
                NOCAP.BLL.Common.CommonEnum.YesNoOption enu_EligibleYesNo;
                enu_EligibleYesNo = NOCAP.BLL.Misc.ApplyRenewSADApp.IsEligibleForApplyRenewSADApp.IsEligibleForApplyRenewSADApplication(Convert.ToInt64(gvInfRenewEligibleApplication.DataKeys[e.Row.RowIndex].Value), out strStatusMessage, out strCustMessage);
                DateTime dtTodayDateOnly = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                switch (enu_EligibleYesNo)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:

                        NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_infrastructureNewIssusedLetter = new NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter(obj_infrastructureNewApplication.ApplicationCode);
                        if (obj_infrastructureNewIssusedLetter != null && obj_infrastructureNewIssusedLetter.ValidityStartDate != null && obj_infrastructureNewApplication.NOCNumber.Trim() != "")
                        {
                            NOCAP.BLL.Master.VisibleRenewListBeforeExpire objVisibleRenewListBeforeExpire = new NOCAP.BLL.Master.VisibleRenewListBeforeExpire();

                            DateTime dtValidityEndDateOnly = new DateTime();
                            if (obj_infrastructureNewIssusedLetter.ValidityEndDate != null)
                            {
                                dtValidityEndDateOnly = new DateTime(obj_infrastructureNewIssusedLetter.ValidityEndDate.Value.Year, obj_infrastructureNewIssusedLetter.ValidityEndDate.Value.Month, obj_infrastructureNewIssusedLetter.ValidityEndDate.Value.Day);

                                 if ((Convert.ToDateTime(obj_infrastructureNewIssusedLetter.ValidityEndDate) - System.DateTime.Today).Days <= objVisibleRenewListBeforeExpire.NumberOfDays)
                                //if ((dtValidityEndDateOnly - dtTodayDateOnly).Days <= objVisibleRenewListBeforeExpire.NumberOfDays && dtValidityEndDateOnly >= dtTodayDateOnly)
                                {
                                    ViewState["InfRenewSno"] = Convert.ToInt32(ViewState["InfRenewSno"]) + 1;
                                    lbInfRenewSno.Text = HttpUtility.HtmlEncode(Convert.ToString(ViewState["InfRenewSno"]));
                                    e.Row.Visible = true;

                                    if (obj_infrastructureNewApplication.NameOfInfrastructure.Trim() != "")
                                    {
                                        if (obj_infrastructureNewIssusedLetter.ValidityStartDate != null)
                                        {
                                            lblIssueLetterStartDate.Text = HttpUtility.HtmlEncode("(" + Convert.ToDateTime(obj_infrastructureNewIssusedLetter.ValidityStartDate).ToString("dd/MM/yyyy"));
                                        }
                                        if (obj_infrastructureNewIssusedLetter.ValidityEndDate != null)
                                        {
                                            lblIssueLetterEndDate.Text = HttpUtility.HtmlEncode(" - " + Convert.ToDateTime(obj_infrastructureNewIssusedLetter.ValidityEndDate).ToString("dd/MM/yyyy") + ")");
                                        }
                                    }
                                }
                                else
                                {
                                    e.Row.Visible = false;
                                }
                            }
                            else
                            {
                                e.Row.Visible = false;
                            }
                        }
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        e.Row.Visible = false;
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        e.Row.Visible = false;
                        break;
                    default:
                        e.Row.Visible = false;
                        break;
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    protected void gvInfRenewEligibleApplication_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandName == "ApplicationCode")
                {
                    lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(e.CommandArgument);
                    lblModeFrom.Text = "New";
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }


    protected void gvInfNthRenewEligibleApplication_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandName == "ApplicationCode")
                {
                    lblInfrastructureApplicationCodeFrom.Text =HttpUtility.HtmlEncode(e.CommandArgument);
                    lblModeFrom.Text = "New";
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void gvInfNthRenewEligibleApplication_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lbInfNthRenewSno = new Label();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                lbInfNthRenewSno = (Label)e.Row.FindControl("lbInfNthRenewSno");
                Label lblIssueLetterStartDate = (Label)e.Row.FindControl("lblIssueLetterStartDate");
                Label lblIssueLetterEndDate = (Label)e.Row.FindControl("lblIssueLetterEndDate");
                Label InfrastructureNewApplicationNumber = (Label)e.Row.FindControl("InfrastructureNewApplicationNumber");
                Label lblLinkDepth = (Label)e.Row.FindControl("lblLinkDepth");
                Label lblNameOfInfra = (Label)e.Row.FindControl("NameOfInfrastructure");


                NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(Convert.ToInt64(gvInfNthRenewEligibleApplication.DataKeys[e.Row.RowIndex].Value));
                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = obj_infrastructureRenewApplication.GetFirstInfrastructureApplication();

                if (obj_infrastructureNewApplication.NameOfInfrastructure != null)
                {
                    InfrastructureNewApplicationNumber.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.InfrastructureNewApplicationNumber);
                    lblNameOfInfra.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.NameOfInfrastructure);
                    lblLinkDepth.Text = HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(Convert.ToInt32(lblLinkDepth.Text))); // add html encode
                }

                string strStatusMessage = "";
                string strCustMessage = "";
                NOCAP.BLL.Common.CommonEnum.YesNoOption enu_EligibleYesNo;
                enu_EligibleYesNo = NOCAP.BLL.Misc.ApplyRenewSADApp.IsEligibleForApplyRenewSADApp.IsEligibleForApplyRenewSADApplication(Convert.ToInt64(gvInfNthRenewEligibleApplication.DataKeys[e.Row.RowIndex].Value), out strStatusMessage, out strCustMessage);
                DateTime dtTodayDateOnly = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                switch (enu_EligibleYesNo)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:

                        NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter obj_infrastructureRenewIssusedLetter = obj_infrastructureRenewApplication.GetIssuedLetter();
                        if (obj_infrastructureRenewIssusedLetter != null && obj_infrastructureRenewIssusedLetter.ValidityStartDate != null && obj_infrastructureRenewApplication.NOCNumber.Trim() != "")
                        {


                            DateTime dtValidityEndDateOnly = new DateTime();
                            if (obj_infrastructureRenewIssusedLetter.ValidityEndDate != null)
                            {
                                dtValidityEndDateOnly = new DateTime(obj_infrastructureRenewIssusedLetter.ValidityEndDate.Value.Year, obj_infrastructureRenewIssusedLetter.ValidityEndDate.Value.Month, obj_infrastructureRenewIssusedLetter.ValidityEndDate.Value.Day);

                                NOCAP.BLL.Master.VisibleRenewListBeforeExpire objVisibleRenewListBeforeExpire = new NOCAP.BLL.Master.VisibleRenewListBeforeExpire();
                                 if ((Convert.ToDateTime(obj_infrastructureRenewIssusedLetter.ValidityEndDate) - System.DateTime.Today).Days <= objVisibleRenewListBeforeExpire.NumberOfDays)                          
                                //if ((dtValidityEndDateOnly - dtTodayDateOnly).Days <= objVisibleRenewListBeforeExpire.NumberOfDays && dtValidityEndDateOnly >= dtTodayDateOnly)
                                {
                                    ViewState["InfNthRenewSno"] = Convert.ToInt32(ViewState["InfNthRenewSno"]) + 1;
                                    lbInfNthRenewSno.Text = HttpUtility.HtmlEncode(Convert.ToString(ViewState["InfNthRenewSno"]));
                                    e.Row.Visible = true;

                                    if (obj_infrastructureRenewApplication.LinkDepth > 0)
                                    {
                                        if (obj_infrastructureRenewIssusedLetter.ValidityStartDate != null)
                                        {
                                            lblIssueLetterStartDate.Text = HttpUtility.HtmlEncode("(" + Convert.ToDateTime(obj_infrastructureRenewIssusedLetter.ValidityStartDate).ToString("dd/MM/yyyy")); // add html encode
                                        }
                                        if (obj_infrastructureRenewIssusedLetter.ValidityEndDate != null)
                                        {
                                            lblIssueLetterEndDate.Text = HttpUtility.HtmlEncode(" - " + Convert.ToDateTime(obj_infrastructureRenewIssusedLetter.ValidityEndDate).ToString("dd/MM/yyyy") + ")");  // add html encode
                                        }
                                    }
                                }
                                else
                                {
                                    e.Row.Visible = false;
                                }
                            }
                            else
                            {
                                e.Row.Visible = false;
                            }
                        }
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        e.Row.Visible = false;
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        e.Row.Visible = false;
                        break;
                    default:
                        e.Row.Visible = false;
                        break;
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

}