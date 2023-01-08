using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
using System.Reflection;
using System.Net;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;


public partial class Sub_NOCIssuedLetter_NOCIssusedLetterToExtUser : System.Web.UI.Page
{
    string strStatus = "";
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            NOCAP.BLL.UserManagement.ExternalUserLogIn obj_Login = new NOCAP.BLL.UserManagement.ExternalUserLogIn();

            if (obj_Login.ProccedNextToLogin(Convert.ToInt64(Session["ExternalUserCode"])) == 1)
            {
                if (obj_Login.ExternalUserCode < 1)
                {
                    MasterPageFile = "~/ExternalUser/Sub/Report/SubReportMaster.master";
                }
                else
                {
                    NOCAP.BLL.UserManagement.ExternalUser obj_exUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                    MasterPageFile = "~/ExternalUser/ExternalUserMaster.master";
                }
            }
            else
            {
                //MasterPageFile = "~/Sub/ApplicantRegistrationMaster.master";
            }
        }
        catch (Exception)
        {
            lblMessage.Text = "Problem in page";

        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = string.Empty;
            if (Request.UrlReferrer != null)
            {
                if (!IsPostBack)
                {
                    RvResult.Visible = false;
                    hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                    ViewState["CSRF"] = hidCSRF.Value;
                }
            }
            else
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/InternalErrorPage.aspx", false);
        }
    }
    void BindReport()
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
            string reportServerUrl = Convert.ToString(ConfigurationManager.AppSettings["ReportServerUrl"]);
            if (string.IsNullOrEmpty(reportServerUrl)) throw new Exception("Missing Report Server Url  from web.config file");
            RvResult.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
            RvResult.ServerReport.ReportPath = NOCAPExternalUtility.GetReportFolderName() + "/AllApplication/NOCIssusedLetterToExtUser";
            List<ReportParameter> param = new List<ReportParameter>();


            ReportParameter p1 = new ReportParameter("dtFilterValidityStartDate");
            p1.Values.Add(null);
            param.Add(p1);

            ReportParameter p2 = new ReportParameter("dtFilterValidityEndDate");
            p2.Values.Add(null);
            param.Add(p2);

            ReportParameter p3 = new ReportParameter("dtFilterIssueLetterDate");
            p3.Values.Add(null);
            param.Add(p3);

            ReportParameter p1FromDt = new ReportParameter("dtFilterIssueValidityFromDate");
            p1FromDt.Values.Add(null);
            param.Add(p1FromDt);

            ReportParameter p2ToDt = new ReportParameter("dtFilterIssueValidityToDate");
            p2ToDt.Values.Add(null);
            param.Add(p2ToDt);

            ReportParameter p4 = new ReportParameter("IntFilterProLocStateCode");
            p4.Values.Add(null);
            param.Add(p4);

            ReportParameter p5 = new ReportParameter("IntFilterProLocDistrictCode");
            p5.Values.Add(null);
            param.Add(p5);

            ReportParameter p6 = new ReportParameter("IntFilterProLocSubDistrictCode");
            p6.Values.Add(null);
            param.Add(p6);
            string FilterIssuedLetterTypeCode = "1";
            ReportParameter p7 = new ReportParameter("intFilterIssuedLetterTypeCode", FilterIssuedLetterTypeCode);
            p7.Values.Add("1");
            param.Add(p7);

            ReportParameter p8 = new ReportParameter("intFilterAppCode");
            p8.Values.Add(null);
            param.Add(p8);

            ReportParameter p9 = new ReportParameter("strFilterAppNo");
            p9.Values.Add(null);
            param.Add(p9);

            ReportParameter p10 = new ReportParameter("strFilterNOCNo");
            p10.Values.Add(null);
            param.Add(p10);

            ReportParameter p11 = new ReportParameter("intFilterAppTypeCode");
            p11.Values.Add(null);
            param.Add(p11);

            ReportParameter p12 = new ReportParameter("intFilterAppTypeCatCode");
            p12.Values.Add(null);
            param.Add(p12);
            ReportParameter p13 = new ReportParameter("intFilterUserCode");
            p13.Values.Add("54");

            param.Add(p13);

            ReportParameter p14 = new ReportParameter("intFilterAppStatus");
            p14.Values.Add(null);
            param.Add(p14);


            ReportParameter p15 = new ReportParameter("chrSubmittedType");
            p15.Values.Add("O");
            param.Add(p15);

            RvResult.ServerReport.SetParameters(param);
            RvResult.ServerReport.Refresh();
            RvResult.Visible = true;
            DisableUnwantedExportFormats();
            strStatus = "Report Generated Successfully !";

        }
        catch (Exception ex)
        {
            strStatus = "Report Generated Error  !";
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);

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
    public void DisableUnwantedExportFormats()
    {
        foreach (RenderingExtension extension in RvResult.ServerReport.ListRenderingExtensions())
        {
            if (extension.Name == "XML" || extension.Name == "IMAGE" || extension.Name == "MHTML" || extension.Name == "CSV" || extension.Name == "WORD")
                ReflectivelySetVisibilityFalse(extension);
        }
    }
    private void ReflectivelySetVisibilityFalse(RenderingExtension extension)
    {
        FieldInfo info = extension.GetType().GetField("m_isVisible", BindingFlags.NonPublic | BindingFlags.Instance);
        if (info != null)
            info.SetValue(extension, false);

    }
    protected void btnShowRecord_Click(object sender, EventArgs e)
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
                if (txtCaptchaCode.Text != string.Empty)
                {
                    if (txtCaptchaCode.Text == "W68HP")
                    {
                        BindReport();
                    }
                    else
                    {
                        lblMessage.Text = "Invalid Captcha Code";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        RvResult.Visible = false;
                    }
                }
                else
                {
                    lblMessage.Text = "Enter Captcha Code";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    RvResult.Visible = false;
                }
                txtCaptchaCode.Text = string.Empty;
            }
            catch (Exception ex)
            {
                strStatus = "Report Generated Error  !";
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }

        }
    }
}