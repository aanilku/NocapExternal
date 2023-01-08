﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
using System.Net;
using System.Reflection;

public partial class ExternalUser_InfrastructureNew_Reports_INFSADReportViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (PreviousPage != null)
            {
                try
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label lblApplicationCode = (Label)placeHolder.FindControl("lblApplicationCode");
                        if (lblApplicationCode != null)
                        {
                            BindReport(lblApplicationCode.Text);
                        }
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);

                }
            }
        }
    }

    void BindReport(string ApplicationCode)
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
            RvResult.ServerReport.ReportPath = NOCAPExternalUtility.GetReportFolderName() + "/Infrastructure/New/PrintApplication/InfrastructureSADApplicationforExternal";

            param[0] = new ReportParameter("AppCode", ApplicationCode);
            RvResult.ServerReport.SetParameters(param);
            RvResult.ServerReport.Refresh();
        }
        catch (Exception ex)
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
                //string domain = Convert.ToString(ConfigurationManager.AppSettings["ReportDomain"]);
                //if (string.IsNullOrEmpty(domain)) throw new Exception("Missing domain from web.config file");
                //return new NetworkCredential(userName, password, domain);
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

            if (extension.Name == "XML" || extension.Name == "IMAGE" || extension.Name == "MHTML" || extension.Name == "CSV" || extension.Name == "WORD" || extension.Name == "EXCEL")
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
}