using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Threading;

public partial class ExternalUser_InfrastructureRenew_RenewalDetail_InfrastructureRenewDetail : System.Web.UI.Page
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

                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplicationForMainGrid = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(Convert.ToInt32(lblAppCode.Text));

                if (obj_InfrastructureNewApplicationForMainGrid != null)
                {
                    lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplicationForMainGrid.InfrastructureNewApplicationNumber);
                    lblInfrastructureName.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplicationForMainGrid.NameOfInfrastructure);

                    if (obj_InfrastructureNewApplicationForMainGrid.RewAppCodeFinally != null)
                    {
                        int MaxLinkDepth = obj_InfrastructureNewApplicationForMainGrid.GetMaxLinkDepthByFirstApplicationCode();

                        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(Convert.ToInt64(obj_InfrastructureNewApplicationForMainGrid.RewAppCodeFinally));

                        DataTable dt = new DataTable();

                        dt.Columns.Add("InfrastructureFirstApplicationCode");
                        dt.Columns.Add("LinkDepth");
                        dt.Columns.Add("LinkDepthHeader");
                        for (int i = MaxLinkDepth; i > 0; i--)
                        {
                            DataRow dr = dt.NewRow();
                            dr["InfrastructureFirstApplicationCode"] = obj_InfrastructureRenewApplication.FirstApplicationCode;
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
                string InfrastructureFirstApplicationCode = gvMainGrid.DataKeys[e.Row.RowIndex].Values[0].ToString();
                HiddenField hdnLinkDepthSN = (HiddenField)e.Row.FindControl("hdnLinkDepthSN");
                string LinkDepthSN = hdnLinkDepthSN.Value;
                NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication();

                if (!NOCAPExternalUtility.IsNumeric(InfrastructureFirstApplicationCode))
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
                GridView grdRenewInfrastructureApplication = (GridView)e.Row.FindControl("grdRenewInfrastructureApplication");
                grdRenewInfrastructureApplication.DataSource = obj_externalUser.GetSubmittedInfrastructureRenewApplicationList(Convert.ToInt64(InfrastructureFirstApplicationCode), Convert.ToInt32(LinkDepthSN));
                grdRenewInfrastructureApplication.DataBind();

            }
        }
        catch (Exception ex)
        {
            //  throw;
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
    }
    protected void grdRenewInfrastructureApplication_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication objKeys_InfrastructureRenewApplication = (NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication)e.Row.DataItem;

            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplicationCurrent = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(objKeys_InfrastructureRenewApplication.InfrastructureRenewApplicationCode);

            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplicationPrevious = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication();
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplicationPrevious = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();

            NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter obj_infrastructureRenewIssusedLetter = new NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter();
            obj_infrastructureRenewIssusedLetter.populateInfrastructureRenewIssusedLetterForINFAppCode(objKeys_InfrastructureRenewApplication.InfrastructureRenewApplicationCode);


            Label lblLatestAppStatusCode = (Label)e.Row.FindControl("lblLatestAppStatusCode");
            Label lblPreviousNOCNumber = (Label)e.Row.FindControl("lblPreviousNOCNumber");
            Label lblPrevNOCLetterStartDate = (Label)e.Row.FindControl("lblPrevNOCLetterStartDate");
            Label lblPrevNOCLetterEndDate = (Label)e.Row.FindControl("lblPrevNOCLetterEndDate");
            LinkButton lnkbtnScan = (LinkButton)e.Row.FindControl("lbtnScanDownload");
            LinkButton lnkbtnDigital = (LinkButton)e.Row.FindControl("lbtnDownload");

            LinkButton lnkCompliance = (LinkButton)e.Row.FindControl("lnkCompliance");
            LinkButton lnkInspection = (LinkButton)e.Row.FindControl("lnkInspection");
            if (obj_infrastructureRenewIssusedLetter.ScanAttPath != null && obj_infrastructureRenewIssusedLetter.ScanAttPath != "")
            {
                lnkbtnScan.Visible = true;
            }
            else
            {
                lnkbtnScan.Visible = false;
            }

            if (obj_infrastructureRenewIssusedLetter.AttPath != null && obj_infrastructureRenewIssusedLetter.AttPath != "")
            {
                lnkbtnDigital.Visible = true;
            }
            else
            {
                lnkbtnDigital.Visible = false;
            }

            //imran

            string SubmittedType = Convert.ToString(obj_infrastructureRenewApplicationCurrent.SubmittedType);
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
            if (obj_infrastructureRenewIssusedLetter != null && obj_infrastructureRenewIssusedLetter.ValidityStartDate != null && obj_infrastructureRenewApplicationCurrent.NOCNumber != null)
            {

                NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(objKeys_InfrastructureRenewApplication.InfrastructureRenewApplicationCode);                
                if (obj_selfCompliance.ApplicationCode != 0 && obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                {
                    lnkCompliance.Text = "Self Compliance (Filled)";
                    lnkCompliance.Visible = true;
                }
                else
                {
                    if (Convert.ToDateTime(obj_infrastructureRenewIssusedLetter.ValidityStartDate).AddMonths(11) < DateTime.Now && Convert.ToDateTime(obj_infrastructureRenewIssusedLetter.ValidityEndDate) > DateTime.Now && obj_infrastructureRenewApplicationCurrent.LatestApplicationStatusCode == Convert.ToInt32(NOCAP.BLL.Utility.Utility.LatestApplicationStatusCodeOption.Approved))                   
                        lnkCompliance.Visible = true;                    
                    else                    
                        lnkCompliance.Visible = true;
                    
                    lnkCompliance.Text = "Self Compliance";
                }
                NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_SelfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(objKeys_InfrastructureRenewApplication.InfrastructureRenewApplicationCode);
                if (obj_SelfInspection.ApplicationCode != 0 && obj_SelfInspection.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                {
                    lnkInspection.Text = "Self Inspection (Filled)";
                    lnkInspection.Visible = true;
                }
                else
                {
                    if (Convert.ToDateTime(obj_infrastructureRenewIssusedLetter.ValidityEndDate).AddMonths(-6) <= DateTime.Now &&  obj_infrastructureRenewApplicationCurrent.LatestApplicationStatusCode == Convert.ToInt32(NOCAP.BLL.Utility.Utility.LatestApplicationStatusCodeOption.Approved))
                        lnkInspection.Visible = true;
                    else
                        lnkInspection.Visible = false;

                    lnkInspection.Text = "Self Inspection";
                }
            }
            else
            {
                lnkCompliance.Visible = true;
                lnkInspection.Visible = true;
            }
            /////////

            obj_infrastructureRenewApplicationCurrent.GetPreviousInfrastructureApplication(out obj_infrastructureNewApplicationPrevious, out obj_infrastructureRenewApplicationPrevious);
            if (obj_infrastructureNewApplicationPrevious != null)
            {
                lblPreviousNOCNumber.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplicationPrevious.NOCNumber);

                if (obj_infrastructureNewApplicationPrevious.NameOfInfrastructure.Trim() != "")
                {
                    NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_infrastructureNewIssusedLetter = new NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter(obj_infrastructureNewApplicationPrevious.ApplicationCode);
                    if (obj_infrastructureNewIssusedLetter.ValidityStartDate != null)
                    {
                        lblPrevNOCLetterStartDate.Text = HttpUtility.HtmlEncode("(" + Convert.ToDateTime(obj_infrastructureNewIssusedLetter.ValidityStartDate).ToString("dd/MM/yyyy"));
                    }
                    if (obj_infrastructureNewIssusedLetter.ValidityEndDate != null)
                    {
                        lblPrevNOCLetterEndDate.Text = HttpUtility.HtmlEncode(" - " + Convert.ToDateTime(obj_infrastructureNewIssusedLetter.ValidityEndDate).ToString("dd/MM/yyyy") + ")");
                    }
                }
            }
            if (obj_infrastructureRenewApplicationPrevious != null)
            {
                lblPreviousNOCNumber.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewApplicationPrevious.NOCNumber);

                NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter obj_InfrastructureRenewIssusedLetter = new NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter(obj_infrastructureRenewApplicationPrevious.InfrastructureRenewApplicationCode);
                if (obj_InfrastructureRenewIssusedLetter.ValidityStartDate != null)
                {
                    lblPrevNOCLetterStartDate.Text = "(" + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_InfrastructureRenewIssusedLetter.ValidityStartDate)).ToString("dd/MM/yyyy");
                }
                if (obj_InfrastructureRenewIssusedLetter.ValidityEndDate != null)
                {
                    lblPrevNOCLetterEndDate.Text = HttpUtility.HtmlEncode(" - " + Convert.ToDateTime(obj_InfrastructureRenewIssusedLetter.ValidityEndDate).ToString("dd/MM/yyyy") + ")");
                }

            }
            if (obj_infrastructureRenewApplicationCurrent.LatestApplicationStatusCode != 0)
            {
                NOCAP.BLL.Master.ApplicationStatus obj_ApplicationStatus = new NOCAP.BLL.Master.ApplicationStatus(obj_infrastructureRenewApplicationCurrent.LatestApplicationStatusCode);
                lblLatestAppStatusCode.Text = HttpUtility.HtmlEncode(obj_ApplicationStatus.ApplicationStatusDescription);
            }
        }
    }
    protected void grdRenewInfrastructureApplication_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "InfrastructureRenewApplicationCode")
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
        try
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
                    Server.Transfer("~/ExternalUser/InfrastructureRenew/Status/InfrastructureRenewStatus.aspx");
                }
            }
        }
        catch (ThreadAbortException)
        {
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
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
                int intINFRenAppCode = 0;
                LinkButton button = (LinkButton)sender;
                string lnkTxt = button.Text;
                GridViewRow row = (GridViewRow)button.NamingContainer;
                if (row != null)
                {
                    if (NOCAPExternalUtility.IsNumeric(button.CommandArgument))
                    {
                        intINFRenAppCode = Convert.ToInt32(button.CommandArgument);
                    }
                }
                int int_infRenAppCode = intINFRenAppCode;
                NOCAPExternalUtility.INFRenScanLetterDownloadFiles(int_infRenAppCode);
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
                int intINFRenAppCode = 0;
                LinkButton button = (LinkButton)sender;
                string lnkTxt = button.Text;
                GridViewRow row = (GridViewRow)button.NamingContainer;
                if (row != null)
                {
                    if (NOCAPExternalUtility.IsNumeric(button.CommandArgument))
                    {
                        intINFRenAppCode = Convert.ToInt32(button.CommandArgument);
                    }
                }
                int int_infRenAppCode = intINFRenAppCode;
                NOCAPExternalUtility.INFRenLetterAppDownloadFiles(int_infRenAppCode);
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