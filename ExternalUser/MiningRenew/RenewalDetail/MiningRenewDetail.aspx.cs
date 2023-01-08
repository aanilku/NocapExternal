using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Threading;

public partial class ExternalUser_MiningRenew_RenewalDetail_MiningRenewDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRFs"] = hidCSRFs.Value;
            BindData();
        }
    }
    void BindData()
    {
        try
        {
            Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
            if (placeHolder != null)
            {
                Label lblAppCode = (Label)placeHolder.FindControl("lblApplicationCode");
                lblApplicationNewCode.Text = HttpUtility.HtmlEncode(lblAppCode.Text);
                if (!NOCAPExternalUtility.IsNumeric(lblAppCode.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert(' Application Code allows only Numeric ');", true);
                    return;
                }

                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplicationForMainGrid = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(Convert.ToInt32(lblAppCode.Text));

                if (obj_MiningNewApplicationForMainGrid != null)
                {
                    lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationForMainGrid.MiningNewApplicationNumber);
                    lblMiningName.Text = HttpUtility.HtmlEncode(obj_MiningNewApplicationForMainGrid.NameOfMining);

                    if (obj_MiningNewApplicationForMainGrid.RewAppCodeFinally != null)
                    {
                        int MaxLinkDepth = obj_MiningNewApplicationForMainGrid.GetMaxLinkDepthByFirstApplicationCode();

                        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(Convert.ToInt64(obj_MiningNewApplicationForMainGrid.RewAppCodeFinally));

                        DataTable dt = new DataTable();

                        dt.Columns.Add("MiningFirstApplicationCode");
                        dt.Columns.Add("LinkDepth");
                        dt.Columns.Add("LinkDepthHeader");
                        for (int i = MaxLinkDepth; i > 0; i--)
                        {
                            DataRow dr = dt.NewRow();
                            dr["MiningFirstApplicationCode"] = obj_MiningRenewApplication.FirstApplicationCode;
                            dr["LinkDepth"] = i;
                            dr["LinkDepthHeader"] = MaxLinkDepth == i ? "Renew " + NOCAPExternalUtility.AddOrdinal(i) + " - Present" : "Renew " + NOCAPExternalUtility.AddOrdinal(i);
                            dt.Rows.Add(dr);
                        }
                        gvMainGrid.DataSource = dt;
                        gvMainGrid.DataBind();
                    }
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    protected void gvMainGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string MiningFirstApplicationCode = gvMainGrid.DataKeys[e.Row.RowIndex].Values[0].ToString();
                HiddenField hdnLinkDepthSN = (HiddenField)e.Row.FindControl("hdnLinkDepthSN");
                string LinkDepthSN = hdnLinkDepthSN.Value;
                NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication();

                if (!NOCAPExternalUtility.IsNumeric(MiningFirstApplicationCode))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert(' Application First Code allows only Numeric ');", true);
                    return;
                }
                if (!NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert(' User Code allows only Numeric ');", true);
                    return;
                }
                if (!NOCAPExternalUtility.IsNumeric(LinkDepthSN))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert(' Depth SN allows only Numeric ');", true);
                    return;
                }
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                GridView grdRenewMiningApplication = (GridView)e.Row.FindControl("grdRenewMiningApplication");
                grdRenewMiningApplication.DataSource = obj_externalUser.GetSubmittedMiningRenewApplicationList(Convert.ToInt64(MiningFirstApplicationCode), Convert.ToInt32(LinkDepthSN));
                grdRenewMiningApplication.DataBind();

            }
        }
        catch (Exception ex)
        {
            //  throw;
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
    }
    protected void grdRenewMiningApplication_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication objKeys_MiningRenewApplication = (NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication)e.Row.DataItem;

            NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplicationCurrent = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(objKeys_MiningRenewApplication.MiningRenewApplicationCode);

            NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplicationPrevious = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication();
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplicationPrevious = new NOCAP.BLL.Mining.New.Application.MiningNewApplication();

            NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter obj_miningRenewIssusedLetter = new NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter(objKeys_MiningRenewApplication.MiningRenewApplicationCode);
            //obj_miningRenewIssusedLetter.populateMiningRenewIssusedLetterForMINAppCode(objKeys_MiningRenewApplication.MiningRenewApplicationCode);


            Label lblLatestAppStatusCode = (Label)e.Row.FindControl("lblLatestAppStatusCode");
            Label lblPreviousNOCNumber = (Label)e.Row.FindControl("lblPreviousNOCNumber");
            Label lblPrevNOCLetterStartDate = (Label)e.Row.FindControl("lblPrevNOCLetterStartDate");
            Label lblPrevNOCLetterEndDate = (Label)e.Row.FindControl("lblPrevNOCLetterEndDate");
            LinkButton lnkbtnScan = (LinkButton)e.Row.FindControl("lbtnScanDownload");
            LinkButton lnkbtnDigital = (LinkButton)e.Row.FindControl("lbtnDownload");
            LinkButton lnkCompliance = (LinkButton)e.Row.FindControl("lnkCompliance");
            LinkButton lnkInspection = (LinkButton)e.Row.FindControl("lnkInspection");
            if (obj_miningRenewIssusedLetter.ScanAttPath != null && obj_miningRenewIssusedLetter.ScanAttPath != "")
            {
                lnkbtnScan.Visible = true;
            }
            else
            {
                lnkbtnScan.Visible = false;
            }
            if (obj_miningRenewIssusedLetter.AttPath != null && obj_miningRenewIssusedLetter.AttPath != "")
            {
                lnkbtnDigital.Visible = true;
            }
            else
            {
                lnkbtnDigital.Visible = false;
            }
            //imran

            string SubmittedType = Convert.ToString(obj_miningRenewApplicationCurrent.SubmittedType);
            LinkButton lnkBtn = new LinkButton();
            lnkBtn = (LinkButton)e.Row.FindControl("lbtnView");
            if (SubmittedType == "Archival")
            {
                lnkBtn.Enabled = false;
                lnkBtn.Text = "";
                lnkBtn.ForeColor = System.Drawing.Color.Black;
                lnkBtn.Style.Add("text-decoration", "none");
            }
            else if (SubmittedType == "Online")
            {
                lnkBtn.Visible = true;
            }
            ///////////////// lnkCompliance button visibility
            if (obj_miningRenewIssusedLetter != null && obj_miningRenewIssusedLetter.ValidityStartDate != null && obj_miningRenewApplicationCurrent.NOCNumber != null)
            {

                NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(objKeys_MiningRenewApplication.MiningRenewApplicationCode);
                if (obj_selfCompliance.ApplicationCode != 0 && obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                {
                    lnkCompliance.Text = "Self Compliance (Filled)";
                    lnkCompliance.Visible = true;
                }
                else
                {
                    if (Convert.ToDateTime(obj_miningRenewIssusedLetter.ValidityStartDate).AddMonths(11) < DateTime.Now && Convert.ToDateTime(obj_miningRenewIssusedLetter.ValidityEndDate) > DateTime.Now && obj_miningRenewApplicationCurrent.LatestApplicationStatusCode == Convert.ToInt32(NOCAP.BLL.Utility.Utility.LatestApplicationStatusCodeOption.Approved))
                        lnkCompliance.Visible = true;
                    else
                        lnkCompliance.Visible = true;

                    lnkCompliance.Text = "Self Compliance";
                }

                NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_SelfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(objKeys_MiningRenewApplication.MiningRenewApplicationCode);
                if (obj_SelfInspection.ApplicationCode != 0 && obj_SelfInspection.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                {
                    lnkInspection.Text = "Self Inspection (Filled)";
                    lnkInspection.Visible = true;
                }
                else
                {
                    if (Convert.ToDateTime(obj_miningRenewIssusedLetter.ValidityEndDate).AddMonths(-6) <= DateTime.Now &&  obj_miningRenewApplicationCurrent.LatestApplicationStatusCode == Convert.ToInt32(NOCAP.BLL.Utility.Utility.LatestApplicationStatusCodeOption.Approved))
                        lnkInspection.Visible = true;
                    else
                        lnkInspection.Visible = false;

                    lnkInspection.Text = "Self Inspection";
                }
            }
            else
            {
                lnkCompliance.Visible = true;
                lnkInspection.Visible = false;
            }

            /////////
            obj_miningRenewApplicationCurrent.GetPreviousMiningApplication(out obj_miningNewApplicationPrevious, out obj_miningRenewApplicationPrevious);
            if (obj_miningNewApplicationPrevious != null)
            {
                lblPreviousNOCNumber.Text = HttpUtility.HtmlEncode(obj_miningNewApplicationPrevious.NOCNumber);

                if (obj_miningNewApplicationPrevious.NameOfMining.Trim() != "")
                {
                    NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter obj_miningNewIssusedLetter = new NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter(obj_miningNewApplicationPrevious.ApplicationCode);
                    if (obj_miningNewIssusedLetter.ValidityStartDate != null)
                    {
                        lblPrevNOCLetterStartDate.Text = HttpUtility.HtmlEncode("(" + Convert.ToDateTime(obj_miningNewIssusedLetter.ValidityStartDate).ToString("dd/MM/yyyy"));
                    }
                    if (obj_miningNewIssusedLetter.ValidityEndDate != null)
                    {
                        lblPrevNOCLetterEndDate.Text = HttpUtility.HtmlEncode(" - " + Convert.ToDateTime(obj_miningNewIssusedLetter.ValidityEndDate).ToString("dd/MM/yyyy") + ")");
                    }
                }
            }
            if (obj_miningRenewApplicationPrevious != null)
            {
                lblPreviousNOCNumber.Text = HttpUtility.HtmlEncode(obj_miningRenewApplicationPrevious.NOCNumber);

                if (obj_miningRenewApplicationPrevious.LinkDepth > 0)
                {
                    NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter obj_MiningRenewIssusedLetter = new NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter(obj_miningRenewApplicationPrevious.MiningRenewApplicationCode);
                    if (obj_MiningRenewIssusedLetter.ValidityStartDate != null)
                    {
                        lblPrevNOCLetterStartDate.Text = "(" + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_MiningRenewIssusedLetter.ValidityStartDate)).ToString("dd/MM/yyyy");
                    }
                    if (obj_MiningRenewIssusedLetter.ValidityEndDate != null)
                    {
                        lblPrevNOCLetterEndDate.Text = HttpUtility.HtmlEncode(" - " + Convert.ToDateTime(obj_MiningRenewIssusedLetter.ValidityEndDate).ToString("dd/MM/yyyy") + ")");  //add html encode
                    }
                }


            }
            if (obj_miningRenewApplicationCurrent.LatestApplicationStatusCode != 0)
            {
                NOCAP.BLL.Master.ApplicationStatus obj_ApplicationStatus = new NOCAP.BLL.Master.ApplicationStatus(obj_miningRenewApplicationCurrent.LatestApplicationStatusCode);
                lblLatestAppStatusCode.Text = HttpUtility.HtmlEncode(obj_ApplicationStatus.ApplicationStatusDescription);
            }
        }
    }
    protected void grdRenewMiningApplication_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "MiningRenewApplicationCode")
            {
                lblApplicationRenewCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    protected void lbtnViewStatus_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRFs"] = hidCSRFs.Value;

            if (e.CommandArgument != null)
            {
                lblApplicationRenewCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);
                Server.Transfer("~/ExternalUser/MiningRenew/Status/MiningRenewStatus.aspx");
            }
        }
    }

    protected void lbtnScanDownload_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                int intMINRenAppCode = 0;
                LinkButton button = (LinkButton)sender;
                string lnkTxt = button.Text;
                GridViewRow row = (GridViewRow)button.NamingContainer;
                if (row != null)
                {
                    if (NOCAPExternalUtility.IsNumeric(button.CommandArgument))
                    {
                        intMINRenAppCode = Convert.ToInt32(button.CommandArgument);
                    }
                }
                int int_minRenAppCode = intMINRenAppCode;
                NOCAPExternalUtility.MINRenScanLetterDownloadFiles(int_minRenAppCode);
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRFs"] = hidCSRFs.Value;

            }
        }
    }
    protected void lbtnDownload_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                int intMINRenAppCode = 0;
                LinkButton button = (LinkButton)sender;
                string lnkTxt = button.Text;
                GridViewRow row = (GridViewRow)button.NamingContainer;
                if (row != null)
                {
                    if (NOCAPExternalUtility.IsNumeric(button.CommandArgument))
                    {
                        intMINRenAppCode = Convert.ToInt32(button.CommandArgument);
                    }
                }
                int int_minRenAppCode = intMINRenAppCode;
                NOCAPExternalUtility.MINRenLetterAppDownloadFiles(int_minRenAppCode);
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRFs"] = hidCSRFs.Value;

            }
        }
    }
    protected void lnkCompliance_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRFs"] = hidCSRFs.Value;

                if (e.CommandArgument != null)
                {
                    lblApplicationRenewCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationRenewCode.Text));

                    if (obj_selfCompliance.ApplicationCode != 0 && obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                    {
                        Server.Transfer("~/ExternalUser/Compliance/Reports/SelfComplianceViewer.aspx");
                    }
                    else
                    {
                        Server.Transfer("~/ExternalUser/Compliance/SelfComplianceA.aspx");
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
            finally
            {
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRFs"] = hidCSRFs.Value;
            }
        }
    }
    protected void lnkInspection_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRFs"] = hidCSRFs.Value;

                if (e.CommandArgument != null)
                {
                    lblApplicationRenewCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);
                    NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_SelfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(Convert.ToInt64(lblApplicationRenewCode.Text));

                    if (obj_SelfInspection.ApplicationCode != 0 && obj_SelfInspection.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                    {
                        Server.Transfer("~/ExternalUser/SelfInspection/Reports/SelfInspectionViewer.aspx");
                    }
                    else
                    {
                        Server.Transfer("~/ExternalUser/SelfInspection/SelfInspectionA.aspx");
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
            finally
            {
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

            }
        }
    }

}