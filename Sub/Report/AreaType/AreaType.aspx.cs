using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using System.Security.Principal;
using System.Reflection;
using Microsoft.Reporting.WebForms;
using System.Text;

public partial class Sub_Report_AreaType_AreaType : System.Web.UI.Page
{



    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                //ddlAreaTypeCategory.Items.Clear();
                if (NOCAPExternalUtility.FillDropDownAreaTypeCategoryBasedOnAreaType(ref ddlAreaTypeCategory, 2) != 1)
                {
                    lblMessage.Text = "Problem in Area Type Category population";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }

                //if (NOCAPExternalUtility.FillDropDownAreaType(ref ddlAreaType) != 1)
                //{
                //    lblMessage.Text = "Problem in Area Type population";
                //    lblMessage.ForeColor = System.Drawing.Color.Red;
                //}

                NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlDistrict);
                NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlSubDistrict);
                //NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlAreaTypeCategory);
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }

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

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
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

            try
            {
                RvResult.Visible = false;
                ddlDistrict.Items.Clear();
                ddlSubDistrict.Items.Clear();

                if (ddlState.SelectedValue == "")
                {
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlDistrict);
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlSubDistrict);
                }
                else
                {
                    int_StateCode = Convert.ToInt32(ddlState.SelectedValue);

                    if (NOCAPExternalUtility.FillDropDownDistrict(ref ddlDistrict, int_StateCode) != 1)
                    {
                        lblMessage.Text = "Problem in district population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlSubDistrict);
                }
               
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
            }
        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        int int_StateCode;
        int int_DistricCode;

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
                RvResult.Visible = false;
                ddlSubDistrict.Items.Clear();

                if (ddlDistrict.SelectedValue == "")
                {
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlSubDistrict);
                }
                else
                {
                    int_DistricCode = Convert.ToInt32(ddlDistrict.SelectedValue);
                    int_StateCode = Convert.ToInt32(ddlState.SelectedValue);

                    if (NOCAPExternalUtility.FillDropDownSubDistrict(ref ddlSubDistrict, int_DistricCode, int_StateCode) != 1)
                    {

                        lblMessage.Text = "Problem in Sub-district population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
            }
        }
    }

    


    //protected void ddlAreaType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int int_AreaTypeCode;
    //    if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
    //    {
    //        Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
    //    }
    //    else
    //    {

    //        hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
    //        ViewState["CSRF"] = hidCSRF.Value;

    //        try
    //        {
    //            RvResult.Visible = false;
    //            ddlAreaTypeCategory.Items.Clear();

    //            if (ddlAreaType.SelectedValue == "")
    //            {
    //                NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlAreaTypeCategory);
    //            }
    //            else
    //            {
    //                int_AreaTypeCode = Convert.ToInt32(ddlAreaType.SelectedValue);

    //                if (NOCAPExternalUtility.FillDropDownAreaTypeCategoryBasedOnAreaType(ref ddlAreaTypeCategory, int_AreaTypeCode) != 1)
    //                {
    //                    lblMessage.Text = "Problem in Area Type Category population";
    //                    lblMessage.ForeColor = System.Drawing.Color.Red;
    //                }
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            Response.Redirect("~/ExternalErrorPage.aspx", false);
    //        }
    //        finally
    //        {
    //        }
    //    }
    //}

    protected void btnShowRecord_Click(object sender, EventArgs e)
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

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;

            try
            {

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
                if (ddlSubDistrict.SelectedIndex > 0 && !NOCAPExternalUtility.IsNumeric(ddlSubDistrict.SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Sub District allows Numeric ');", true);
                    return;
                }
                //if (ddlAreaType.SelectedIndex > 0 && !NOCAPExternalUtility.IsNumeric(ddlAreaType.SelectedValue))
                //{
                //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Area Type allows Numeric ');", true);
                //    return;
                //}
                if (ddlAreaTypeCategory.SelectedIndex > 0 && !NOCAPExternalUtility.IsNumeric(ddlAreaTypeCategory.SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Area Type Category allows Numeric ');", true);
                    return;
                }
                RvResult.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                RvResult.Reset();
                RvResult.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
                string reportServerUrl = Convert.ToString(ConfigurationManager.AppSettings["ReportServerUrl"]);
                if (string.IsNullOrEmpty(reportServerUrl))
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false); // updated by gurudas on 30.07.18 for security
                }
                   // throw new Exception("Missing Report Server Url  from web.config file");
                RvResult.ServerReport.ReportServerUrl = new Uri(reportServerUrl);

                RvResult.ServerReport.ReportPath = NOCAPExternalUtility.GetReportFolderName() + "/Reports/Master/AreaType";
               // RvResult.ServerReport.ReportPath = "/cgwa-noc/NOCAP/Reports/Master/AreaType";

                ReportParameter[] param = new ReportParameter[4];
                if (ddlState.SelectedValue == "")
                { param[0] = new ReportParameter("intFilterStateCode", "0"); }
                else
                { param[0] = new ReportParameter("intFilterStateCode", HttpUtility.HtmlEncode(ddlState.SelectedValue)); }

                if (ddlDistrict.SelectedValue == "")
                { param[1] = new ReportParameter("intFilterDistrictCode", "0"); }
                else
                { param[1] = new ReportParameter("intFilterDistrictCode", HttpUtility.HtmlEncode(ddlDistrict.SelectedValue)); }

                if (ddlSubDistrict.SelectedValue == "")
                { param[2] = new ReportParameter("intFilterSubDistrictCode", "0"); }
                else
                { param[2] = new ReportParameter("intFilterSubDistrictCode", HttpUtility.HtmlEncode(ddlSubDistrict.SelectedValue)); }

                //if (ddlAreaType.SelectedValue == "")
                //{
                //    param[3] = new ReportParameter("intFilterAreaTypeCode", "0");
                //}
                //else
                //{
                //    param[3] = new ReportParameter("intFilterAreaTypeCode", HttpUtility.HtmlEncode(ddlAreaType.SelectedValue));
                //}

                if (ddlAreaTypeCategory.SelectedValue == "")
                { param[3] = new ReportParameter("intFilterAreaTypeCatCode", "0"); }
                else
                { param[3] = new ReportParameter("intFilterAreaTypeCatCode", HttpUtility.HtmlEncode(ddlAreaTypeCategory.SelectedValue)); }


                RvResult.ServerReport.SetParameters(param);


                RvResult.ServerReport.Refresh();
                RvResult.Visible = true;
                DisableUnwantedExportFormats();
            }
            catch (Exception)
            {
                //Response.Write(ex.Message);
                Response.Redirect("~/ExternalErrorPage.aspx", false);

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
                //string domain = Convert.ToString(ConfigurationManager.AppSettings["ReportDomain"]);
                //if (string.IsNullOrEmpty(domain)) throw new Exception("Missing domain from web.config file");
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


    protected void ddlSubDistrict_SelectedIndexChanged(object sender, EventArgs e)
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
                RvResult.Visible = false;
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void ddlAreaTypeCategory_SelectedIndexChanged(object sender, EventArgs e)
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
                RvResult.Visible = false;
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
}