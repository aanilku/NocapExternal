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


public partial class Sub_Report_AreaType_SegmentBAreaType : System.Web.UI.Page
{
    string strPageName = "SegmentBAreaType";
    string strActionName = "";
    string strStatus = "";

    protected void Page_PreInit(object sender, EventArgs e)
    {

        try
        {

            NOCAP.BLL.UserManagement.ExternalUserLogIn obj_Login = new NOCAP.BLL.UserManagement.ExternalUserLogIn();
            if (obj_Login.ProccedNextToLogin(Convert.ToInt64(Session["ExternalUserCode"])) == 1)
            {
                if (obj_Login.ExternalUserCode < 1)                
                    MasterPageFile = "~/ExternalUser/Sub/Report/SubReportMaster.master";                
                else
                {
                    NOCAP.BLL.UserManagement.ExternalUser obj_exUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                    MasterPageFile = "~/ExternalUser/ExternalUserMaster.master";
                }

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
            if (Request.UrlReferrer != null)
            {
                if (!IsPostBack)
                {
                    RvResult.Visible = false;
                    hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                    ViewState["CSRF"] = hidCSRF.Value;
                    if (NOCAPExternalUtility.FillDropDownState(ref ddlState) != 1)
                    {
                        lblMessage.Text = "Problem in state population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlDistrict);
                }
            }
            else
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    protected void btnShowRecord_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(NOCAPExternalUtility.GetReportFolderName()))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Invalid Report Folder Name');", true);
                return;
            }

            if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {
                strActionName = "ShowReport";
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;

                if (ddlState.SelectedIndex > 0 && !NOCAPExternalUtility.IsNumeric(ddlState.SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('State allows Numeric ');", true);
                    return;
                }
                if (ddlDistrict.SelectedIndex > 0 && !NOCAPExternalUtility.IsNumeric(ddlDistrict.SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('District allows Numeric ');", true);
                    return;
                }

                RvResult.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                RvResult.Reset();
                RvResult.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
                string reportServerUrl = Convert.ToString(ConfigurationManager.AppSettings["ReportServerUrl"]);
                if (string.IsNullOrEmpty(reportServerUrl)) throw new Exception("Missing Report Server Url  from web.config file");
                RvResult.ServerReport.ReportServerUrl = new Uri(reportServerUrl);

                RvResult.ServerReport.ReportPath = NOCAPExternalUtility.GetReportFolderName() + "/Reports/Master/SegmentBAreaType";

                ReportParameter[] param = new ReportParameter[2];
                if (ddlState.SelectedValue != "")
                { param[0] = new ReportParameter("intFilterStateCode", HttpUtility.HtmlEncode(ddlState.SelectedValue)); }
                else
                { param[0] = new ReportParameter("intFilterStateCode", new string[] { null }, false); }

                if (ddlDistrict.SelectedValue != "")
                { param[1] = new ReportParameter("intFilterDistrictCode", HttpUtility.HtmlEncode(ddlDistrict.SelectedValue)); }
                else
                { param[1] = new ReportParameter("intFilterDistrictCode", new string[] { null }, false); }
                RvResult.ServerReport.SetParameters(param);
                RvResult.ServerReport.Refresh();
                RvResult.Visible = true;
                DisableUnwantedExportFormats();
                strStatus = "Report Generated Successfully !";

            }
        }

        catch (Exception)
        {
            strStatus = "Report Generated Error  !";
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);

        }
        finally
        {
            ActionTrail obj_IntActionTrail = new ActionTrail();
            if (Session["InternalUserCode"] != null)
            {
                obj_IntActionTrail.UserCode = Convert.ToInt64(Session["InternalUserCode"]);
                obj_IntActionTrail.IP_Address = Request.UserHostAddress;
                obj_IntActionTrail.ActionPerformed = strPageName + "-" + strActionName;
                obj_IntActionTrail.Status = strStatus;
                if (obj_IntActionTrail != null)
                    ActionTrailDAL.IntActionSave(obj_IntActionTrail);
            }
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
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int int_StateCode;
            if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
                RvResult.Visible = false;
                ddlDistrict.Items.Clear();
                if (ddlState.SelectedValue == "")
                { NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlDistrict); }
                else
                {
                    if (!NOCAPExternalUtility.IsNumeric(ddlState.SelectedValue))
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('State allows only Numeric');", true);
                        return;
                    }
                    int_StateCode = Convert.ToInt32(ddlState.SelectedValue);
                    if (NOCAPExternalUtility.FillDropDownDistrict(ref ddlDistrict, int_StateCode) != 1)
                    {
                        lblMessage.Text = "Problem in district population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }

    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
                RvResult.Visible = false;
            }
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
    }
}