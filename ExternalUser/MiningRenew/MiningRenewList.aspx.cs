using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExternalUser_MiningRenew_MiningRenewList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblModeFrom.Text = "";
            lblMiningApplicationCodeFrom.Text = "";
            if (!IsPostBack)
            {
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;
                BindMinRenewEligibleApplication();
                BindMinNthRenewEligibleApplication();
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindMinRenewEligibleApplication()
    {
        try
        {
            ViewState["MinRenewSno"] = 0;  //For Sr No
            if (NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                gvMinRenewEligibleApplication.DataSource = obj_externalUser.GetSubmittedMiningNewApplicationList();
                gvMinRenewEligibleApplication.DataBind();
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void BindMinNthRenewEligibleApplication()
    {
        try
        {
            ViewState["MinNthRenewSno"] = 0;  //For Sr No
            if (NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                gvMinNthRenewEligibleApplication.DataSource = obj_externalUser.GetSubmittedMiningRenewApplicationList();
                gvMinNthRenewEligibleApplication.DataBind();
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    protected void gvMinRenewEligibleApplication_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lbMinRenewSno = new Label();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                lbMinRenewSno = (Label)e.Row.FindControl("lbMinRenewSno");
                Label lblIssueLetterStartDate = (Label)e.Row.FindControl("lblIssueLetterStartDate");
                Label lblIssueLetterEndDate = (Label)e.Row.FindControl("lblIssueLetterEndDate");

                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(Convert.ToInt64(gvMinRenewEligibleApplication.DataKeys[e.Row.RowIndex].Value));
                string strStatusMessage = "";
                string strCustMessage = "";
                NOCAP.BLL.Common.CommonEnum.YesNoOption enu_EligibleYesNo;

                enu_EligibleYesNo = NOCAP.BLL.Misc.ApplyRenewSADApp.IsEligibleForApplyRenewSADApp.IsEligibleForApplyRenewSADApplication(Convert.ToInt64(gvMinRenewEligibleApplication.DataKeys[e.Row.RowIndex].Value), out strStatusMessage, out strCustMessage);
                DateTime dtTodayDateOnly = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                switch (enu_EligibleYesNo)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:

                        NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter obj_miningNewIssusedLetter = new NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter(obj_miningNewApplication.ApplicationCode);
                        if (obj_miningNewIssusedLetter != null && obj_miningNewIssusedLetter.ValidityStartDate != null && obj_miningNewApplication.NOCNumber.Trim() != "")
                        {

                            NOCAP.BLL.Master.VisibleRenewListBeforeExpire objVisibleRenewListBeforeExpire = new NOCAP.BLL.Master.VisibleRenewListBeforeExpire();
                            DateTime dtValidityEndDateOnly = new DateTime();
                            if (obj_miningNewIssusedLetter.ValidityEndDate != null)
                            {
                                dtValidityEndDateOnly = new DateTime(obj_miningNewIssusedLetter.ValidityEndDate.Value.Year, obj_miningNewIssusedLetter.ValidityEndDate.Value.Month, obj_miningNewIssusedLetter.ValidityEndDate.Value.Day);
                                if ((Convert.ToDateTime(obj_miningNewIssusedLetter.ValidityEndDate) - System.DateTime.Today).Days <= objVisibleRenewListBeforeExpire.NumberOfDays)
                               // if ((dtValidityEndDateOnly - dtTodayDateOnly).Days <= objVisibleRenewListBeforeExpire.NumberOfDays && dtValidityEndDateOnly >= dtTodayDateOnly)
                                {
                                    ViewState["MinRenewSno"] = HttpUtility.HtmlEncode(Convert.ToInt32(ViewState["MinRenewSno"]) + 1);
                                    lbMinRenewSno.Text =HttpUtility.HtmlEncode(Convert.ToString(ViewState["MinRenewSno"]));
                                    e.Row.Visible = true;

                                    if (obj_miningNewApplication.NameOfMining.Trim() != "")
                                    {
                                        if (obj_miningNewIssusedLetter.ValidityStartDate != null)
                                        {
                                            lblIssueLetterStartDate.Text = HttpUtility.HtmlEncode("(" + Convert.ToDateTime(obj_miningNewIssusedLetter.ValidityStartDate).ToString("dd/MM/yyyy"));
                                        }
                                        if (obj_miningNewIssusedLetter.ValidityEndDate != null)
                                        {
                                            lblIssueLetterEndDate.Text = HttpUtility.HtmlEncode(" - " + Convert.ToDateTime(obj_miningNewIssusedLetter.ValidityEndDate).ToString("dd/MM/yyyy") + ")");
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
   
    protected void gvMinRenewEligibleApplication_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(e.CommandArgument);
                    lblModeFrom.Text = "New";
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

   
    protected void gvMinNthRenewEligibleApplication_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lbMinNthRenewSno = new Label();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                lbMinNthRenewSno = (Label)e.Row.FindControl("lbMinNthRenewSno");
                Label lblIssueLetterStartDate = (Label)e.Row.FindControl("lblIssueLetterStartDate");
                Label lblIssueLetterEndDate = (Label)e.Row.FindControl("lblIssueLetterEndDate");
                Label MiningNewApplicationNumber = (Label)e.Row.FindControl("MiningNewApplicationNumber");
                Label lblLinkDepth = (Label)e.Row.FindControl("lblLinkDepth");
                Label lblNameOfMining = (Label)e.Row.FindControl("NameOfMining");

                NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(Convert.ToInt64(gvMinNthRenewEligibleApplication.DataKeys[e.Row.RowIndex].Value));
                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = obj_miningRenewApplication.GetFirstMiningApplication();

                if (obj_miningNewApplication.NameOfMining != null)
                {
                    MiningNewApplicationNumber.Text = HttpUtility.HtmlEncode(obj_miningNewApplication.MiningNewApplicationNumber);
                    lblNameOfMining.Text = HttpUtility.HtmlEncode(obj_miningNewApplication.NameOfMining);
                    lblLinkDepth.Text = HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(Convert.ToInt32(lblLinkDepth.Text)));
                }

                string strStatusMessage = "";
                string strCustMessage = "";
                NOCAP.BLL.Common.CommonEnum.YesNoOption enu_EligibleYesNo;
                enu_EligibleYesNo = NOCAP.BLL.Misc.ApplyRenewSADApp.IsEligibleForApplyRenewSADApp.IsEligibleForApplyRenewSADApplication(Convert.ToInt64(gvMinNthRenewEligibleApplication.DataKeys[e.Row.RowIndex].Value), out strStatusMessage, out strCustMessage);
                DateTime dtTodayDateOnly = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                switch (enu_EligibleYesNo)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:

                        NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter obj_miningRenewIssusedLetter = obj_miningRenewApplication.GetIssuedLetter();

                        if (obj_miningRenewIssusedLetter != null && obj_miningRenewIssusedLetter.ValidityStartDate != null && obj_miningRenewApplication.NOCNumber!=null && obj_miningRenewApplication.NOCNumber.Trim() != "")
                        {

                            NOCAP.BLL.Master.VisibleRenewListBeforeExpire objVisibleRenewListBeforeExpire = new NOCAP.BLL.Master.VisibleRenewListBeforeExpire();
                            DateTime dtValidityEndDateOnly = new DateTime();
                            if (obj_miningRenewIssusedLetter.ValidityEndDate != null)
                            {
                                dtValidityEndDateOnly = new DateTime(obj_miningRenewIssusedLetter.ValidityEndDate.Value.Year, obj_miningRenewIssusedLetter.ValidityEndDate.Value.Month, obj_miningRenewIssusedLetter.ValidityEndDate.Value.Day);
                                if ((Convert.ToDateTime(obj_miningRenewIssusedLetter.ValidityEndDate) - System.DateTime.Today).Days <= objVisibleRenewListBeforeExpire.NumberOfDays)                          
                                // if ((dtValidityEndDateOnly - dtTodayDateOnly).Days <= objVisibleRenewListBeforeExpire.NumberOfDays && dtValidityEndDateOnly >= dtTodayDateOnly)
                                {
                                    ViewState["MinNthRenewSno"] = Convert.ToInt32(ViewState["MinNthRenewSno"]) + 1;
                                    lbMinNthRenewSno.Text = HttpUtility.HtmlEncode(Convert.ToString(ViewState["MinNthRenewSno"]));
                                    e.Row.Visible = true;

                                    if (obj_miningRenewApplication.LinkDepth > 0)
                                    {
                                        if (obj_miningRenewIssusedLetter.ValidityStartDate != null)
                                        {
                                            lblIssueLetterStartDate.Text = HttpUtility.HtmlEncode("(" + Convert.ToDateTime(obj_miningRenewIssusedLetter.ValidityStartDate).ToString("dd/MM/yyyy"));
                                        }
                                        if (obj_miningRenewIssusedLetter.ValidityEndDate != null)
                                        {
                                            lblIssueLetterEndDate.Text = HttpUtility.HtmlEncode(" - " + Convert.ToDateTime(obj_miningRenewIssusedLetter.ValidityEndDate).ToString("dd/MM/yyyy") + ")");
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
    protected void gvMinNthRenewEligibleApplication_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    lblMiningApplicationCodeFrom.Text =HttpUtility.HtmlEncode(e.CommandArgument);
                    lblModeFrom.Text = "New";
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
}