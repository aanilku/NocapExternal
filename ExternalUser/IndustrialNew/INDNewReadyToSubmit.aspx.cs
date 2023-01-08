using Microsoft.Reporting.WebForms;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExternalUser_IndustrialNew_INDNewReadyToSubmit : System.Web.UI.Page
{
    string strActionName = "ReadyToSubmit", strStatus = "", strPageName = "INDNewReadyToSubmit";

    protected void Page_Load(object sender, EventArgs e)
    {

        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
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
                        SourceLabel = (Label)placeHolder.FindControl("lblIndustialApplicationCodeFrom");
                        if (SourceLabel != null)
                            lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                    }
                }
                BindReport(lblIndustialApplicationCodeFrom.Text);

            }
            catch (Exception ex)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }


    private void BindReport(string ApplicationCode)
    {
        try
        {
            if (string.IsNullOrEmpty(NOCAPExternalUtility.GetReportFolderName()))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Invalid Report Folder Name');", true);
                return;
            }
            RvResult.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            RvResult.Reset();
            RvResult.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
            RvResult.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServerUrl"]);
            ReportParameter[] param = new ReportParameter[1];
            RvResult.ServerReport.ReportPath = NOCAPExternalUtility.GetReportFolderName() + "/Industrial/New/PrintApplication/IndustrialSADApplication";
            param[0] = new ReportParameter("AppCode", ApplicationCode);
            RvResult.ServerReport.SetParameters(param);
            RvResult.ServerReport.Refresh();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);

        }
    }


    [Serializable]
    public sealed class MyReportServerCredentials : IReportServerCredentials
    {
        public WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;
            }
        }

        public ICredentials NetworkCredentials
        {
            get
            {
                string userName = Convert.ToString(ConfigurationManager.AppSettings["ReportUserName"]);
                if (string.IsNullOrEmpty(userName)) throw new Exception("Missing user name from web.config file");
                string password = Convert.ToString(ConfigurationManager.AppSettings["ReportPassword"]);
                if (string.IsNullOrEmpty(password)) throw new Exception("Missing password from web.config file");

                return new NetworkCredential(userName, password);
            }
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;
            return false;
        }
    }
    private void DisableUnwantedExportFormats()
    {

        foreach (RenderingExtension extension in RvResult.ServerReport.ListRenderingExtensions())
        {

            if (extension.Name == "XML" || extension.Name == "IMAGE" || extension.Name == "MHTML" || extension.Name == "CSV" || extension.Name == "WORD")
            {
                ReflectivelySetVisibilityFalse(extension);
            }

        }
    }
    private void ReflectivelySetVisibilityFalse(RenderingExtension extension)
    {
        FieldInfo info = extension.GetType().GetField("m_isVisible", BindingFlags.NonPublic | BindingFlags.Instance);

        if (info != null)
        {
            info.SetValue(extension, false);
        }
    }

    protected void btnReadyToSubmitSubmit_Click(object sender, EventArgs e)
    {


        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            {

                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {
                long lng_submittedApplicationCode = 0;
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                obj_industrialNewApplication.Undertaking.UndertakingAgreement = NOCAP.BLL.Common.Undertaking.UndertakingAgreementOption.Yes;
                if (obj_industrialNewApplication.Update() == 1)
                {
                    if (obj_industrialNewApplication.ReadyToSubmitApplication(out lng_submittedApplicationCode, null, obj_externalUser.ExternalUserCode) == 1)
                    {

                        lblFinalMsg.Text = "Please proceed for payment and submit the application.";

                        lblFinalMsg.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {
                        strStatus = "Application Submit Failed";
                        lblFinalMsg.Text = obj_industrialNewApplication.CustumMessage;
                        lblFinalMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
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
            Server.Transfer("Attachment.aspx");
        }

    }
}