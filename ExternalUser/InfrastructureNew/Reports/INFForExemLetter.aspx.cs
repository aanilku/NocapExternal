using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;
using System.Net;
using System.Configuration;
using System.IO;
using Microsoft.Reporting.WebForms;

public partial class ExternalUser_InfrastructureNew_Reports_INFForExemLetter : System.Web.UI.Page
{
    string format;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            if (PreviousPage != null)
            {
                try
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label lblApplicationCode = (Label)placeHolder.FindControl("lblInfrastructureApplicationCodeFrom");
                        if (lblApplicationCode != null)
                        {
                            ViewState["ApplicationCode"] = lblApplicationCode.Text + "_INF_Exem";
                            ViewState["INFCode"] = lblApplicationCode.Text;
                            GenratePDFReport(lblApplicationCode.Text);
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

    void GenratePDFReport(string ApplicationCode)
    {
        try
        {
            if (string.IsNullOrEmpty(NOCAPExternalUtility.GetReportFolderName()))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Invalid Report Folder Name');", true);
                return;
            }
            RvResult.Reset();
            RvResult.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            RvResult.Reset();
            RvResult.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
            RvResult.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServerUrl"]);
            ReportParameter[] param = new ReportParameter[1];
           // RvResult.ServerReport.ReportPath = "/cgwa-noc/NOCAP/Infrastructure/New/ExemptionLetter/INFForExemLetter";
            RvResult.ServerReport.ReportPath = NOCAPExternalUtility.GetReportFolderName() + "/Infrastructure/New/ExemptionLetter/INFForExemLetter";
            param[0] = new ReportParameter("INFAppCode", ApplicationCode);
            RvResult.ServerReport.SetParameters(param);
            string mimeType, encoding, extension, deviceInfo;
            string[] streamids; Microsoft.Reporting.WebForms.Warning[] warnings;
             format = "PDF";
            deviceInfo = "<DeviceInfo>" + "<SimplePageHeaders>True</SimplePageHeaders>" + "</DeviceInfo>";
            byte[] bytes = RvResult.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
            Response.Clear();
            //if (SaveData(ApplicationCode + "_INF_Exem" + ".pdf", bytes))
            //{
                // for insert the data in DB
                SaveExmLetterInfo(Convert.ToInt64(ApplicationCode),bytes);
            //}
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }


    void SaveExmLetterInfo(long longApplicationCode, byte[] Data)
    {
        try
        {
            NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter objInfrastructureNewIssusedLetter = new NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter();
            objInfrastructureNewIssusedLetter.INFAppCode = Convert.ToInt64(longApplicationCode);
            objInfrastructureNewIssusedLetter.IssuedLetterTypeCode = 3;
            objInfrastructureNewIssusedLetter.NOCAbs = 'N';
            objInfrastructureNewIssusedLetter.NOCAbsDew = 'N';
            objInfrastructureNewIssusedLetter.NOCDew = 'N';
            objInfrastructureNewIssusedLetter.IssueByExUserCode = Convert.ToInt64(Session["ExternalUserCode"]);
            //  objIndustrialNewIssusedLetter.IssuLetterContent = "aa";  //ConfigurationManager.AppSettings["NOCAPFilePath"] + "NOCAPPDF" + Path.DirectorySeparatorChar.ToString() +
            string strFilePath = Convert.ToString(ViewState["ApplicationCode"]) + ".pdf";
            objInfrastructureNewIssusedLetter.AttPath = strFilePath;
            objInfrastructureNewIssusedLetter.AttachmentFile = Data;
            objInfrastructureNewIssusedLetter.FileExtension = "." + format;
            objInfrastructureNewIssusedLetter.ContentType = "application/pdf";
            objInfrastructureNewIssusedLetter.CreatedByExUC = Convert.ToInt64(Session["ExternalUserCode"]);
            //objIndustrialNewIssusedLetter.CustumMessage = "Data Inserted.";
            if (objInfrastructureNewIssusedLetter != null)
            {
                if (objInfrastructureNewIssusedLetter.Add() == 1)
                {
                    lblMsg.Text = "Thanks for applying.</br>Your project is not falling under ‘water intensive industry’ group. It is located in ‘Safe’ category area </br> and your net ground water requirement is less than 100 m3/day. For such project NOC is not</br> required for groundwater extraction. An exemption letter is being issued.";
                    lnkGenratePDF.Enabled = true;
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMsg.Text = "Your Application has allready submitted !";
                    lnkGenratePDF.Enabled = false;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    // use of delete Gentertd PDF
                    //if ((Directory.Exists(ConfigurationManager.AppSettings["NOCAPFilePath"] + ConfigurationManager.AppSettings["NOCAPPDF"])))
                    //{
                    //    File.Delete(ConfigurationManager.AppSettings["NOCAPFilePath"] + ConfigurationManager.AppSettings["NOCAPPDF"] + Path.DirectorySeparatorChar.ToString() + Convert.ToString(ViewState["ApplicationCode"]) + ".pdf");

                    //}
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    //protected bool SaveData(string FileName, byte[] Data)
    //{
    //    BinaryWriter Writer = null;
    //    try
    //    {
    //        if (!(Directory.Exists(ConfigurationManager.AppSettings["NOCAPFilePath"] + "NOCAPPDF")))
    //        {
    //            DirectoryInfo di = Directory.CreateDirectory(ConfigurationManager.AppSettings["NOCAPFilePath"] + "NOCAPPDF");
    //        }
    //        Writer = new BinaryWriter(File.Create(ConfigurationManager.AppSettings["NOCAPFilePath"] + "NOCAPPDF" + Path.DirectorySeparatorChar.ToString() + FileName));
    //        Writer.Write(Data);
    //        Writer.Flush();
    //        Writer.Close();
    //    }
    //    catch (Exception)
    //    {
    //        return false;
    //    }
    //    return true;
    //}

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

    protected void lnkGenratePDF_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        else
        {
            //try
            //{
            //    string strFilePath = ConfigurationManager.AppSettings["NOCAPFilePath"] + "NOCAPPDF" + Path.DirectorySeparatorChar.ToString() + Convert.ToString(ViewState["ApplicationCode"]) + ".pdf";
            //    Response.ContentType = ContentType;
            //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(HttpUtility.HtmlEncode(strFilePath)));
            //    Response.WriteFile(HttpUtility.HtmlEncode(strFilePath));
            //}
            //catch (Exception)
            //{
            //    Response.Redirect("~/ExternalErrorPage.aspx", false);
            //}
            //finally
            //{
            //    Response.End();
            //    hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            //    Session["CSRF"] = hidCSRF.Value;
            //}
            try
            {
                LinkButton btn = (LinkButton)sender;
                int int_indAppCode = Convert.ToInt32(ViewState["INFCode"]);
                NOCAPExternalUtility.INFLetterAppDownloadFiles(int_indAppCode);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
            }
        }
    }
}