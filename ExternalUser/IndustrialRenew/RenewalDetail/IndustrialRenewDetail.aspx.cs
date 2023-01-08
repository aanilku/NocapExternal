using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Threading;

public partial class ExternalUser_IndustrialRenew_RenewalDetail_IndustrialRenewDetail : System.Web.UI.Page
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
                 lblApplicationCode.Text =HttpUtility.HtmlEncode(lblAppCode.Text);  //add html encode

                if (!NOCAPExternalUtility.IsNumeric(lblAppCode.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert(' Application Code allows only Numeric ');", true);
                    return;
                }

                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplicationForMainGrid = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(Convert.ToInt32(lblAppCode.Text));

                if (obj_IndustrialNewApplicationForMainGrid != null)
                {
                    lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_IndustrialNewApplicationForMainGrid.IndustrialNewApplicationNumber);
                    lblIndustoryName.Text = HttpUtility.HtmlEncode(obj_IndustrialNewApplicationForMainGrid.NameOfIndustry);

                    if (obj_IndustrialNewApplicationForMainGrid.RewAppCodeFinally != null)
                    {
                        int MaxLinkDepth = obj_IndustrialNewApplicationForMainGrid.GetMaxLinkDepthByFirstApplicationCode();

                        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(Convert.ToInt64(obj_IndustrialNewApplicationForMainGrid.RewAppCodeFinally));
                        
                        DataTable dt = new DataTable();

                        dt.Columns.Add("IndustrialFirstApplicationCode");
                        dt.Columns.Add("LinkDepth");
                        dt.Columns.Add("LinkDepthHeader");
                        for (int i = MaxLinkDepth; i > 0; i--)
                        {
                            DataRow dr = dt.NewRow();
                            dr["IndustrialFirstApplicationCode"] = obj_IndustrialRenewApplication.FirstApplicationCode;
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
                string IndustrialFirstApplicationCode = gvMainGrid.DataKeys[e.Row.RowIndex].Values[0].ToString();
                HiddenField hdnLinkDepthSN = (HiddenField)e.Row.FindControl("hdnLinkDepthSN");
                string LinkDepthSN = hdnLinkDepthSN.Value;
                NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication();

                if (!NOCAPExternalUtility.IsNumeric(IndustrialFirstApplicationCode))
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
                GridView grdRenewIndustrialApplication = (GridView)e.Row.FindControl("grdRenewIndustrialApplication");                
                grdRenewIndustrialApplication.DataSource = obj_externalUser.GetSubmittedIndustrialRenewApplicationList(Convert.ToInt64(IndustrialFirstApplicationCode), Convert.ToInt32(LinkDepthSN));
                grdRenewIndustrialApplication.DataBind();
            }
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
    }
    protected void grdRenewIndustrialApplication_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication objKeys_IndustrialRenewApplication = (NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication)e.Row.DataItem;

            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplicationCurrent = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(objKeys_IndustrialRenewApplication.IndustrialRenewApplicationCode);

            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplicationPrevious = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication();
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplicationPrevious = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();

            NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter obj_industrialRenewIssusedLetter = new NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter(objKeys_IndustrialRenewApplication.IndustrialRenewApplicationCode);
            obj_industrialRenewIssusedLetter.populateIndustrialRenewIssusedLetterForINDAppCode(objKeys_IndustrialRenewApplication.IndustrialRenewApplicationCode);

            Label lblLatestAppStatusCode = (Label)e.Row.FindControl("lblLatestAppStatusCode");
            Label lblPreviousNOCNumber = (Label)e.Row.FindControl("lblPreviousNOCNumber");
            Label lblPrevNOCLetterStartDate = (Label)e.Row.FindControl("lblPrevNOCLetterStartDate");
            Label lblPrevNOCLetterEndDate = (Label)e.Row.FindControl("lblPrevNOCLetterEndDate");
            LinkButton lnkbtnScan = (LinkButton)e.Row.FindControl("lbtnScanDownload");
            LinkButton lbtnDigital = (LinkButton)e.Row.FindControl("lbtnDownload");
            

            LinkButton lnkCompliance = (LinkButton)e.Row.FindControl("lnkCompliance");
            LinkButton lnkInspection = (LinkButton)e.Row.FindControl("lnkInspection");
            if (obj_industrialRenewIssusedLetter.ScanAttPath != null && obj_industrialRenewIssusedLetter.ScanAttPath != "")
            {
                lnkbtnScan.Visible = true;
            }
            else
            {
                lnkbtnScan.Visible = false;
            }
            if (obj_industrialRenewIssusedLetter.AttPath != null && obj_industrialRenewIssusedLetter.AttPath != "")
            {
                lbtnDigital.Visible = true;
            }
            else
            {
                lbtnDigital.Visible = false;
            }
            
            //imran
            
            string SubmittedType = Convert.ToString(obj_industrialRenewApplicationCurrent.SubmittedType);
            LinkButton lnkBtn = new LinkButton();
            lnkBtn = (LinkButton)e.Row.FindControl("lbtnView");
            if(SubmittedType=="Archival")
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



            if (obj_industrialRenewIssusedLetter != null && obj_industrialRenewIssusedLetter.ValidityStartDate != null && obj_industrialRenewApplicationCurrent.NOCNumber != null)
            {

                NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(objKeys_IndustrialRenewApplication.IndustrialRenewApplicationCode);
                if (obj_selfCompliance.ApplicationCode != 0 && obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                {
                    lnkCompliance.Text = "Self Compliance (Filled)";
                    lnkCompliance.Visible = true;
                }
                else
                {
                    if (Convert.ToDateTime(obj_industrialRenewIssusedLetter.ValidityStartDate).AddMonths(11) < DateTime.Now && Convert.ToDateTime(obj_industrialRenewIssusedLetter.ValidityEndDate) > DateTime.Now &&  obj_industrialRenewApplicationCurrent.LatestApplicationStatusCode == Convert.ToInt32(NOCAP.BLL.Utility.Utility.LatestApplicationStatusCodeOption.Approved))
                    {
                        lnkCompliance.Visible = true;
                    }
                    else
                    {
                        lnkCompliance.Visible = true;
                    }
                    lnkCompliance.Text = "Self Compliance";
                }



                NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_SelfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(objKeys_IndustrialRenewApplication.IndustrialRenewApplicationCode);
                if (obj_SelfInspection.ApplicationCode != 0 && obj_SelfInspection.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                {
                    lnkInspection.Text = "Self Inspection (Filled)";
                    lnkInspection.Visible = true;
                }
                else
                {
                    if (Convert.ToDateTime(obj_industrialRenewIssusedLetter.ValidityEndDate).AddMonths(-6) <= DateTime.Now  && obj_industrialRenewApplicationCurrent.LatestApplicationStatusCode == Convert.ToInt32(NOCAP.BLL.Utility.Utility.LatestApplicationStatusCodeOption.Approved))
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





            obj_industrialRenewApplicationCurrent.GetPreviousIndustrialApplication(out obj_industrialNewApplicationPrevious, out obj_industrialRenewApplicationPrevious);
            if (obj_industrialNewApplicationPrevious != null)
            {
                lblPreviousNOCNumber.Text = HttpUtility.HtmlEncode(obj_industrialNewApplicationPrevious.NOCNumber);

                if (obj_industrialNewApplicationPrevious.NameOfIndustry.Trim() != "")
                {
                    NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_industrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter(obj_industrialNewApplicationPrevious.IndustrialNewApplicationCode);
                    if (obj_industrialNewIssusedLetter.ValidityStartDate != null)
                    {
                        lblPrevNOCLetterStartDate.Text = "(" + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_industrialNewIssusedLetter.ValidityStartDate)).ToString("dd/MM/yyyy");
                    }
                    if (obj_industrialNewIssusedLetter.ValidityEndDate != null)
                    {
                        lblPrevNOCLetterEndDate.Text = " - " + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_industrialNewIssusedLetter.ValidityEndDate)).ToString("dd/MM/yyyy") + ")";
                    }
                }
            }
            if (obj_industrialRenewApplicationPrevious != null)
            {
                lblPreviousNOCNumber.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplicationPrevious.NOCNumber);

                NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter objIndustrialRenewIssusedLetter = new NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter(obj_industrialRenewApplicationPrevious.IndustrialRenewApplicationCode);

                if (objIndustrialRenewIssusedLetter.ValidityStartDate != null)
                {
                    lblPrevNOCLetterStartDate.Text = "(" + Convert.ToDateTime(HttpUtility.HtmlEncode(objIndustrialRenewIssusedLetter.ValidityStartDate)).ToString("dd/MM/yyyy");
                }
                if (objIndustrialRenewIssusedLetter.ValidityEndDate != null)
                {
                    lblPrevNOCLetterEndDate.Text = " - " + Convert.ToDateTime(HttpUtility.HtmlEncode(objIndustrialRenewIssusedLetter.ValidityEndDate)).ToString("dd/MM/yyyy") + ")";
                }

            }

            if (obj_industrialRenewApplicationCurrent.LatestApplicationStatusCode != 0)
            {
                NOCAP.BLL.Master.ApplicationStatus obj_ApplicationStatus = new NOCAP.BLL.Master.ApplicationStatus(obj_industrialRenewApplicationCurrent.LatestApplicationStatusCode);
                lblLatestAppStatusCode.Text = HttpUtility.HtmlEncode(obj_ApplicationStatus.ApplicationStatusDescription);
            }

        }
    }

    protected void grdRenewIndustrialApplication_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "IndustrialRenewApplicationCode")
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
                lblApplicationRenewCode.Text =HttpUtility.HtmlEncode(e.CommandArgument.ToString());  //add html encode
                Server.Transfer("~/ExternalUser/IndustrialRenew/Status/IndustrialRenewStatus.aspx");
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
                int intINDRenAppCode = 0;
                LinkButton button = (LinkButton)sender;
                string lnkTxt = button.Text;
                GridViewRow row = (GridViewRow)button.NamingContainer;
                if (row != null)
                {
                    if (NOCAPExternalUtility.IsNumeric(button.CommandArgument))
                    {
                        intINDRenAppCode = Convert.ToInt32(button.CommandArgument);
                    }
                }
                int int_indRenAppCode = intINDRenAppCode;
                NOCAPExternalUtility.INDRenScanLetterDownloadFiles(int_indRenAppCode);
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
                int intINDRenAppCode = 0;
                LinkButton button = (LinkButton)sender;
                string lnkTxt = button.Text;
                GridViewRow row = (GridViewRow)button.NamingContainer;
                if (row != null)
                {
                    if (NOCAPExternalUtility.IsNumeric(button.CommandArgument))
                    {
                        intINDRenAppCode = Convert.ToInt32(button.CommandArgument);
                    }
                }
                int int_indRenAppCode = intINDRenAppCode;
                NOCAPExternalUtility.INDRenLetterAppDownloadFiles(int_indRenAppCode);
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