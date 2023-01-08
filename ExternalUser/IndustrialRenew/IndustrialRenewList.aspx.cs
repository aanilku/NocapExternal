using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExternalUser_IndustrialReNew_IndustrialRenewList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblModeFrom.Text = "";
            lblIndustialApplicationCodeFrom.Text = "";
            if (!IsPostBack)
            {
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

                BindIndRenewEligibleApplication();
                BindIndNthRenewEligibleApplication();
            }

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    private void BindIndRenewEligibleApplication()
    {
        try
        {
            ViewState["IndRenewSno"] = 0;  //For Sr No
            if (NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                gvIndRenewEligibleApplication.DataSource = obj_externalUser.GetSubmittedIndustrialNewApplicationList();
                gvIndRenewEligibleApplication.DataBind();
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void BindIndNthRenewEligibleApplication()
    {
        try
        {
            ViewState["IndNthRenewSno"] = 0;  //For Sr No
            if (NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                gvIndNthRenewEligibleApplication.DataSource = obj_externalUser.GetSubmittedIndustrialRenewApplicationList();
                gvIndNthRenewEligibleApplication.DataBind();
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    protected void gvIndRenewEligibleApplication_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lbIndRenewSno = new Label();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                lbIndRenewSno = (Label)e.Row.FindControl("lbIndRenewSno");
                Label lblIssueLetterStartDate = (Label)e.Row.FindControl("lblIssueLetterStartDate");
                Label lblIssueLetterEndDate = (Label)e.Row.FindControl("lblIssueLetterEndDate");


                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(Convert.ToInt64(gvIndRenewEligibleApplication.DataKeys[e.Row.RowIndex].Value));
                string strStatusMessage = "";
                string strCustMessage = "";
                NOCAP.BLL.Common.CommonEnum.YesNoOption enu_EligibleYesNo;
                enu_EligibleYesNo = NOCAP.BLL.Misc.ApplyRenewSADApp.IsEligibleForApplyRenewSADApp.IsEligibleForApplyRenewSADApplication(Convert.ToInt64(gvIndRenewEligibleApplication.DataKeys[e.Row.RowIndex].Value), out strStatusMessage, out strCustMessage);
                DateTime dtTodayDateOnly = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                switch (enu_EligibleYesNo)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:

                        NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_industrialNewIssusedLetter = obj_industrialNewApplication.GetIssuedLetter();

                        if (obj_industrialNewIssusedLetter != null && obj_industrialNewIssusedLetter.ValidityStartDate != null && obj_industrialNewApplication.NOCNumber.Trim() != "")
                        {
                            NOCAP.BLL.Master.VisibleRenewListBeforeExpire objVisibleRenewListBeforeExpire = new NOCAP.BLL.Master.VisibleRenewListBeforeExpire();
                            DateTime dtValidityEndDateOnly = new DateTime();
                            if (obj_industrialNewIssusedLetter.ValidityEndDate != null)
                            {
                                dtValidityEndDateOnly = new DateTime(obj_industrialNewIssusedLetter.ValidityEndDate.Value.Year, obj_industrialNewIssusedLetter.ValidityEndDate.Value.Month, obj_industrialNewIssusedLetter.ValidityEndDate.Value.Day);

                                if ((Convert.ToDateTime(obj_industrialNewIssusedLetter.ValidityEndDate) - System.DateTime.Today).Days <= objVisibleRenewListBeforeExpire.NumberOfDays)
                               // if ((dtValidityEndDateOnly - dtTodayDateOnly).Days <= objVisibleRenewListBeforeExpire.NumberOfDays && dtValidityEndDateOnly >= dtTodayDateOnly)
                                {
                                    ViewState["IndRenewSno"] = Convert.ToInt32(ViewState["IndRenewSno"]) + 1;
                                    lbIndRenewSno.Text = HttpUtility.HtmlEncode(Convert.ToString(ViewState["IndRenewSno"]));
                                    e.Row.Visible = true;

                                    if (obj_industrialNewApplication.NameOfIndustry.Trim() != "")
                                    {
                                        //   NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_industrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter(obj_industrialNewApplication.IndustrialNewApplicationCode);
                                        if (obj_industrialNewIssusedLetter.ValidityStartDate != null)
                                        {
                                            lblIssueLetterStartDate.Text = "(" + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_industrialNewIssusedLetter.ValidityStartDate)).ToString("dd/MM/yyyy");
                                        }
                                        if (obj_industrialNewIssusedLetter.ValidityEndDate != null)
                                        {
                                            lblIssueLetterEndDate.Text = " - " + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_industrialNewIssusedLetter.ValidityEndDate)).ToString("dd/MM/yyyy") + ")";
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
    
    protected void gvIndRenewEligibleApplication_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());  // add html encode
                    lblModeFrom.Text = "New";
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
   
    protected void gvIndNthRenewEligibleApplication_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lbIndNthRenewSno = new Label();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                lbIndNthRenewSno = (Label)e.Row.FindControl("lbIndNthRenewSno");
                Label lblIssueLetterStartDate = (Label)e.Row.FindControl("lblIssueLetterStartDate");
                Label lblIssueLetterEndDate = (Label)e.Row.FindControl("lblIssueLetterEndDate");
                Label IndustrialNewApplicationNumber = (Label)e.Row.FindControl("IndustrialNewApplicationNumber");

                Label lblLinkDepth = (Label)e.Row.FindControl("lblLinkDepth");
                Label lblNameOfIndustry = (Label)e.Row.FindControl("NameOfIndustry");




                NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(Convert.ToInt64(gvIndNthRenewEligibleApplication.DataKeys[e.Row.RowIndex].Value));
                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = obj_industrialRenewApplication.GetFirstIndustrialApplication();

                if (obj_industrialNewApplication!=null && obj_industrialNewApplication.NameOfIndustry != null)
                {
                    IndustrialNewApplicationNumber.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.IndustrialNewApplicationNumber);
                    lblLinkDepth.Text = NOCAPExternalUtility.AddOrdinal(Convert.ToInt32(lblLinkDepth.Text));
                    lblNameOfIndustry.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.NameOfIndustry);
                }

                string strStatusMessage = "";
                string strCustMessage = "";
                NOCAP.BLL.Common.CommonEnum.YesNoOption enu_EligibleYesNo;
                enu_EligibleYesNo = NOCAP.BLL.Misc.ApplyRenewSADApp.IsEligibleForApplyRenewSADApp.IsEligibleForApplyRenewSADApplication(Convert.ToInt64(gvIndNthRenewEligibleApplication.DataKeys[e.Row.RowIndex].Value), out strStatusMessage, out strCustMessage);
                DateTime dtTodayDateOnly = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                switch (enu_EligibleYesNo)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:

                        NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter obj_industrialRenewIssusedLetter = obj_industrialRenewApplication.GetIssuedLetter();


                        if (obj_industrialRenewIssusedLetter != null && obj_industrialRenewIssusedLetter.ValidityStartDate != null && obj_industrialRenewApplication.NOCNumber.Trim() != "")
                        {
                            NOCAP.BLL.Master.VisibleRenewListBeforeExpire objVisibleRenewListBeforeExpire = new NOCAP.BLL.Master.VisibleRenewListBeforeExpire();
                            DateTime dtValidityEndDateOnly = new DateTime();
                            if (obj_industrialRenewIssusedLetter.ValidityEndDate != null)
                            {
                                dtValidityEndDateOnly = new DateTime(obj_industrialRenewIssusedLetter.ValidityEndDate.Value.Year, obj_industrialRenewIssusedLetter.ValidityEndDate.Value.Month, obj_industrialRenewIssusedLetter.ValidityEndDate.Value.Day);

                                  if ((Convert.ToDateTime(obj_industrialRenewIssusedLetter.ValidityEndDate) - System.DateTime.Today).Days <= objVisibleRenewListBeforeExpire.NumberOfDays)
                               // if ((dtValidityEndDateOnly - dtTodayDateOnly).Days <= objVisibleRenewListBeforeExpire.NumberOfDays && dtValidityEndDateOnly >= dtTodayDateOnly)
                                {
                                    ViewState["IndNthRenewSno"] = Convert.ToInt32(ViewState["IndNthRenewSno"]) + 1;
                                    lbIndNthRenewSno.Text = HttpUtility.HtmlEncode(Convert.ToString(ViewState["IndNthRenewSno"]));
                                    e.Row.Visible = true;

                                    if (obj_industrialRenewApplication.LinkDepth > 0)
                                    {
                                        //   NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_industrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter(obj_industrialNewApplication.IndustrialNewApplicationCode);
                                        if (obj_industrialRenewIssusedLetter.ValidityStartDate != null)
                                        {
                                            lblIssueLetterStartDate.Text = HttpUtility.HtmlEncode("(" + Convert.ToDateTime(obj_industrialRenewIssusedLetter.ValidityStartDate).ToString("dd/MM/yyyy"));
                                        }
                                        if (obj_industrialRenewIssusedLetter.ValidityEndDate != null)
                                        {
                                            lblIssueLetterEndDate.Text = HttpUtility.HtmlEncode(" - " + Convert.ToDateTime(obj_industrialRenewIssusedLetter.ValidityEndDate).ToString("dd/MM/yyyy") + ")");
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
    protected void gvIndNthRenewEligibleApplication_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());  // add html encode
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