using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Threading;
using NOCAP;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;


using System.Security.Principal;
using System.Net;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using System.Reflection;
using System.Collections;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public partial class ExternalUser_IndustrialRenew_IndExemSubmitSuccess : System.Web.UI.Page
{
    byte[] arr_Report = null;
    string strPageName = "SendEmail";
    string strActionName = "Submit";
    string strStatus = "";

    long? lng_AppCode = null;
    int? int_AppTypeCode = null;
    int? int_AppPurposeCode = null;
    int? int_UserCode = null;
    int? int_AlertStagesCode = null;
    long? lng_ExuserCode = null;
    int? int_CreatedByUC = null;

    long lngSubmittedApplicationCodeFinally;
    long? lng_CreatedByExUC = null;
    string strToEmailId = "";
    string strSubject = "";
    string strBody = "";
    //string strMessage = "";
    //string strMobileNumberTo = "";
    //string strSMSUserName = null;
    string strEmailServerName = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (PreviousPage != null)
            {
                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblIndustialApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                        }
                    }

                }

                long lngSubmittedApplicationCode = PreviousPage.IndSubmitAppCode;
                lngSubmittedApplicationCodeFinally = lngSubmittedApplicationCode;
                GetIndustrialApplicationNumber(lngSubmittedApplicationCode);


                // GenratePDFReport(lngSubmittedApplicationCode.ToString());
                BindReport(lngSubmittedApplicationCode);
                SendMail();
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally { arr_Report = null; }
    }

    void SendMail()
    {
        if (EmailUtility.IsSendEmailEnable())
        {
            NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
            if (obj_ExternalUser.ExternalUserCode > 0 && obj_ExternalUser.ExternalUserEmailID != "")
            {
                NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(lngSubmittedApplicationCodeFinally);

                string Subject = " NOCAP - Exemption for Application No. " + HttpUtility.HtmlEncode(Convert.ToString(ViewState["AppNo"]));
                // string Body = "<html><body><p>Dear " + " ";
                // Body += HttpUtility.HtmlEncode(obj_ExternalUser.ExternalUserName);
                string Body = " ,</p><p style='margin-left:50px;'>This is to certify that as per information furnished by the applicant, :<br />";
                Body += "M/s <b>" + HttpUtility.HtmlEncode(Convert.ToString(ViewState["NameofIndustry"])) + "</b> comes under Micro and Small Enterprises category and has ground <br />";
                Body += "water withdrawal of less than 10 cum/day. As per  S.O. 3289(E) dated 24/09/2020 <br />";
                Body += "by Department Of Water Resources, River Development And Ganga Rejuvenation, <br />";
                Body += "Guidelines to regulate and control ground water extraction in India, 2020 Micro and <br />";
                Body += "small Enterprises drawing ground water less than 10 cum/day are exempted.<br /><br />";

                Body += "The firm is exempted from seeking Renewal of NOC (" + HttpUtility.HtmlEncode(Convert.ToString(ViewState["PrevNOCNumber"])) + ") for Application No. (" + HttpUtility.HtmlEncode(Convert.ToString(ViewState["AppNo"])) + ") <br />";
                Body += "This certificate is generated  based on information provided by the applicant, CGWA <br />";
                Body += "has not  verified the claim made by applicant. Any false information furnished by the <br />";
                Body += "applicant, shall invite legal action against him/her as per S.O. 3289(E) dated 24/09/2020. <br /><br />";
                Body += "This is an auto-generated email.  Do not reply to this email.";

                // Body += "Please note your application number for future reference.</p><br /><br /><br /><p>This is system generated mail.</p></body></html>";
                // arr_Report
                //  bool boolResult = EmailUtility.SendMail(out strEmailServerName, obj_ExternalUser.ExternalUserEmailID, StrSubject: Subject, StrBody: Body);
                // bool boolResult = EmailUtility.SendMail(out strEmailServerName, obj_ExternalUser.ExternalUserEmailID + "," + obj_industrialNewApplication.CommunicationEmailID, StrBcc: "chaurasia@nic.in", StrSubject: Subject, StrBody: Body);
                bool boolResult = EmailUtility.SendMailHavingFileStream(out strEmailServerName, obj_ExternalUser.ExternalUserEmailID + "," + obj_industrialRenewApplication.CommunicationEmailID, StrBcc: "chaurasia@nic.in", StrSubject: Subject, StrBody: Body, BytAttachFile: new MemoryStream(arr_Report).ToArray(), StrContentType: "application/pdf/octet-stream", StrFileExtension: ".pdf");

                lblState.Text = HttpUtility.HtmlEncode(Convert.ToString(ViewState["State"]));
                lblDistrict.Text = HttpUtility.HtmlEncode(Convert.ToString(ViewState["District"]));
                lblSubDistrict.Text = HttpUtility.HtmlEncode(Convert.ToString(ViewState["SubDistrict"]));
                lblQuentity.Text = HttpUtility.HtmlEncode(Convert.ToString(ViewState["GroundWater"]) + "m<sup>3</sup>/day");
                strToEmailId = obj_ExternalUser.ExternalUserEmailID;
                if (!boolResult)
                {
                    lblSentMailStatus.Text = "No email was send";
                }
                else
                {
                    lblSentMailStatus.Text = "";
                    SaveEmailAlert();
                }
            }
        }
    }

    public void GetIndustrialApplicationNumber(long lngSubmittedApplicationCode)
    {
        try
        {


            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(lngSubmittedApplicationCode);
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_industrialRenewApplication.FirstApplicationCode);
            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplicationPrevious = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication((long)(obj_industrialRenewApplication.LinkedIndustrialRenewApplicationCode == null ? 0 : obj_industrialRenewApplication.LinkedIndustrialRenewApplicationCode));

            if (obj_industrialNewApplication.IndustrialNewApplicationNumber != "")
            {
                msgSubmit.Text = "Your Application Submitted Successfully.Your Application Detail are :";
                msgSubmit.ForeColor = System.Drawing.Color.Green;
                Session["ApplicationCode"] = obj_industrialRenewApplication.IndustrialRenewApplicationCode;
                lblAppNo.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.IndustrialNewApplicationNumber);
                lblNameofIndustry.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.NameOfIndustry);
                ViewState["AppNo"] = obj_industrialNewApplication.IndustrialNewApplicationNumber.ToString();
                ViewState["NameofIndustry"] = obj_industrialNewApplication.NameOfIndustry.ToString();
                ViewState["State"] = obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().StateName;
                ViewState["District"] = (new NOCAP.BLL.Master.District(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode)).DistrictName;
                ViewState["SubDistrict"] = (new NOCAP.BLL.Master.SubDistrict(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode)).SubDistrictName;
                ViewState["AppType"] = obj_industrialNewApplication.ApplicationTypeCode;
                ViewState["AppPurpose"] = obj_industrialNewApplication.ApplicationPurposeCode;
                if (obj_industrialRenewApplication.LinkDepth > 1)
                    ViewState["PrevNOCNumber"] = obj_industrialRenewApplicationPrevious.NOCNumber;
                else
                    ViewState["PrevNOCNumber"] = obj_industrialNewApplication.NOCNumber;
                lblSubmitDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_industrialRenewApplication.SubmittedOnByExUC).ToString("dd/MM/yyyy"));
                lblRefMsg.Text = "Please note your application number for future reference.";
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    protected void btnExemLetter_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/ExternalUser/IndustrialRenew/Reports/INDINFMINForExemLetter.aspx");
    }

    public void SaveEmailAlert()
    {
        try
        {

            NOCAP.BLL.EmailAlert.EmailAlert obj_insertEmailAlert = new NOCAP.BLL.EmailAlert.EmailAlert();

            if (!string.IsNullOrEmpty(Session["ApplicationCode"].ToString()))
            {
                lng_AppCode = Convert.ToInt64(Session["ApplicationCode"]);
            }
            obj_insertEmailAlert.AppCode = Convert.ToInt64(lng_AppCode);
            obj_insertEmailAlert.EmailType = NOCAP.BLL.EmailAlert.EmailAlert.EmailTypeAFR.Alert;
            obj_insertEmailAlert.NOCAPApplicationType = NOCAP.BLL.EmailAlert.EmailAlert.NOCAPApplicationTypeEI.NOCAPExternal;
            if (!string.IsNullOrEmpty(ViewState["AppType"].ToString()))
            {
                int_AppTypeCode = Convert.ToInt32(ViewState["AppType"]);
            }
            if (!string.IsNullOrEmpty(ViewState["AppPurpose"].ToString()))
            {
                int_AppPurposeCode = Convert.ToInt32(ViewState["AppPurpose"]);
            }
            obj_insertEmailAlert.AppTypeCode = Convert.ToInt32(int_AppTypeCode);
            obj_insertEmailAlert.AppPurposeCode = Convert.ToInt32(int_AppPurposeCode);
            obj_insertEmailAlert.UserCode = Convert.ToInt32(int_UserCode);
            obj_insertEmailAlert.ExUserCode = Convert.ToInt64(lng_ExuserCode);
            obj_insertEmailAlert.AlertStagesCode = Convert.ToInt32(int_AlertStagesCode);
            obj_insertEmailAlert.EmailServerName = strEmailServerName;
            lng_CreatedByExUC = Convert.ToInt64(Session["ExternalUserCode"]);
            obj_insertEmailAlert.CreatedByExUserCode = lng_CreatedByExUC;
            obj_insertEmailAlert.CreatedByUserCode = int_CreatedByUC;

            if (!string.IsNullOrEmpty(strToEmailId))
            {
                obj_insertEmailAlert.ToEmailID = strToEmailId;
            }


            obj_insertEmailAlert.Subject = strSubject;
            obj_insertEmailAlert.Message = strBody;
            if (obj_insertEmailAlert.Add() == 1)
            {
                strStatus = "Email Alert Save Successfully.";

            }
            else
            {

                strStatus = "Email Alert Not Saved.";

            }
        }

        catch (Exception)
        {
            strStatus = "Email Alert Not Saved.";


        }
        finally
        {
            ActionTrail obj_ExtActionTrail = new ActionTrail();
            if (Session["ExternalUserCode"] != null)
            {
                obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
                obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
                obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
                obj_ExtActionTrail.Status = strStatus;
                if (obj_ExtActionTrail != null)
                    ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
            }
        }

    }

    protected void BindReport(long lngSubmittedApplicationCode)
    {
        try
        {

            if (string.IsNullOrEmpty(NOCAPExternalUtility.GetReportFolderName()))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Invalid Report Folder Name');", true);
                return;
            }


            strActionName = "ShowReport";
            RvResult.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            RvResult.Reset();
            RvResult.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
            string reportServerUrl = Convert.ToString(ConfigurationManager.AppSettings["ReportServerUrl"]);
            if (string.IsNullOrEmpty(reportServerUrl)) throw new Exception("Missing Report Server Url  from web.config file");
            RvResult.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
            RvResult.ServerReport.ReportPath = NOCAPExternalUtility.GetReportFolderName() + "/Reports/Letter/RenewExemptionLetter";
            ReportParameter[] param = new ReportParameter[1];
            param[0] = new ReportParameter("bigintFilterRenAppCode", lngSubmittedApplicationCode.ToString());
            RvResult.ServerReport.SetParameters(param);
            RvResult.ServerReport.Refresh();
            RvResult.Visible = true;
            DisableUnwantedExportFormats();
            strStatus = "Report Generated Successfully !";
            string mimeType, encoding, extension, deviceInfo;
            string[] streamids; Microsoft.Reporting.WebForms.Warning[] warnings;
            deviceInfo = "<DeviceInfo>" + "<SimplePageHeaders>True</SimplePageHeaders>" + "</DeviceInfo>";
            byte[] bytes = RvResult.ServerReport.Render("pdf", deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
            arr_Report = bytes;
            SaveExmLetterInfo(lngSubmittedApplicationCode, bytes);
        }
        catch (Exception ex)
        {
            strStatus = "Report Generated Error  !";
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);

        }
        finally
        {
            ActionTrail obj_IntActionTrail = new ActionTrail();
            if (Session["ExternalUserCode"] != null)
            {
                obj_IntActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
                obj_IntActionTrail.IP_Address = Request.UserHostAddress;
                obj_IntActionTrail.ActionPerformed = strPageName + "-" + strActionName;
                obj_IntActionTrail.Status = strStatus;
                if (obj_IntActionTrail != null)
                    ActionTrailDAL.IntActionSave(obj_IntActionTrail);
            }
        }
    }

    void SaveExmLetterInfo(long longApplicationCode, byte[] DataFile)
    {
        try
        {
            //NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_industrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter(longApplicationCode);


            NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter objIndustrialRenewIssusedLetter = new NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter(longApplicationCode);

            //  objIndustrialNewIssusedLetter.INDAppCode = Convert.ToInt64(longApplicationCode);
            //  objIndustrialNewIssusedLetter.IssuedLetterTypeCode = obj_industrialNewIssusedLetter.IssuedLetterTypeCode;// 3;
            objIndustrialRenewIssusedLetter.AutoSaved = NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter.AutoSavedOption.Yes;
            objIndustrialRenewIssusedLetter.DigitialSigned = NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter.DigitialSignedOption.No;
            objIndustrialRenewIssusedLetter.NOCForAbstraction = NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter.NOCForAbstractionOption.Yes;
            objIndustrialRenewIssusedLetter.NOCForAbstractionAndDewatering = NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter.NOCForAbstractionAndDewateringOption.No;
            objIndustrialRenewIssusedLetter.NOCForDewaterring = NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter.NOCForDewaterringOption.No;
            objIndustrialRenewIssusedLetter.IssueByExUserCode = Convert.ToInt64(Session["ExternalUserCode"]);
            string strFilePath = Convert.ToString(longApplicationCode) + ".pdf";
            objIndustrialRenewIssusedLetter.ScanAttPath = strFilePath;
            objIndustrialRenewIssusedLetter.AttachmentFile = DataFile;
            objIndustrialRenewIssusedLetter.FileExtension = "." + "pdf";
            objIndustrialRenewIssusedLetter.ContentType = "application/pdf";
            objIndustrialRenewIssusedLetter.CreatedByExUC = Convert.ToInt64(Session["ExternalUserCode"]);

            objIndustrialRenewIssusedLetter.AttachmentFileDigitialSigned = DataFile;// NOCAPInternalUtility.StreamFile(FileUploadScanLetter.PostedFile);
            objIndustrialRenewIssusedLetter.ContentTypeDigitialSigned = null;
            objIndustrialRenewIssusedLetter.FileExtensionDigitialSigned = null;
            objIndustrialRenewIssusedLetter.AttPath = null;

            if (objIndustrialRenewIssusedLetter != null)
            {
                if (objIndustrialRenewIssusedLetter.UpdateManualLetter() == 1)
                {
                    //lblMsg.Text = "Your Data has been Submited Successfully !";
                    //   lblMsg.Text = "Thanks for applying.</br>Your project is not falling under ‘water intensive industry’ group. It is located in ‘Safe’ category area </br> and your net ground water requirement is less than 100 m3/day. For such project NOC is not</br> required for groundwater extraction. An exemption letter is being issued.";
                    // lnkDownloadPDF.Enabled = true;
                    // lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    // lblMsg.Text = "Your Application has allready submitted !";
                    // lnkDownloadPDF.Enabled = false;
                    // lblMsg.ForeColor = System.Drawing.Color.Red;

                }
            }
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
}