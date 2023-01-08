using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Drawing;
using System.Threading;
using System.Configuration;
using System.IO;


public partial class ExternalUser_Compliance_SelfComplianceAttachment : System.Web.UI.Page
{
    string strPageName = "SelfComplianceAttachment";
    string strActionName = "";
    string strStatus = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;

            Tab1.CssClass = "Clicked";
            Tab2.CssClass = "Initial";
            MainView.ActiveViewIndex = 0;

            if (PreviousPage != null)
            {
                Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                if (placeHolder != null)
                {
                    Label SourceLabel = (Label)placeHolder.FindControl("lblApplicationCodeFrom");
                    if (SourceLabel != null)
                    {
                        lblApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                    }
                }
                if (lblApplicationCodeFrom.Text.Trim() != "")
                {
                    BindPhotographsAttachment(Convert.ToInt64(lblApplicationCodeFrom.Text));
                    BindSelfComplianceAttachment(Convert.ToInt64(lblApplicationCodeFrom.Text));
                }
            }
        }
    }
    protected void Tab1_Click(object sender, EventArgs e)
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
                Tab1.CssClass = "Clicked";
                Tab2.CssClass = "Initial";

                MainView.ActiveViewIndex = 0;
                lblMessagePhotographs.Text = "";
                txtPhotographs.Text = "";
                lblMessagePhotographs.Text = "";
                txtSelfComplianceAttachment.Text = "";
            }
            catch (Exception)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);
            }
        }
    }
    protected void Tab2_Click(object sender, EventArgs e)
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
                Tab1.CssClass = "Initial";
                Tab2.CssClass = "Clicked";
                MainView.ActiveViewIndex = 1;
                lblMessagePhotographs.Text = "";
                txtPhotographs.Text = "";
                lblMessagePhotographs.Text = "";
                txtSelfComplianceAttachment.Text = "";

            }
            catch (Exception)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);
            }
        }
    }
    protected void gvPhotographs_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gvPhotographs_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "Delete Self Compliance Attachment";
                long lng_ApplicationCode = Convert.ToInt32(gvPhotographs.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvPhotographs.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvPhotographs.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_SelfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment(lng_ApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);

                //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                //string str_fullPath = str_startPathFromConfig + obj_SelfComplianceAttachment.AttachmentPath;

                if (obj_SelfComplianceAttachment.Delete() == 1)
                {
                    strStatus = "File Deleted Success";
                    lblMessagePhotographs.Text = obj_SelfComplianceAttachment.CustumMessage;
                    lblMessagePhotographs.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    BindPhotographsAttachment(Convert.ToInt64(lblApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Deleted Failed";
                    lblMessagePhotographs.Text = obj_SelfComplianceAttachment.CustumMessage;
                    lblMessagePhotographs.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
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
    }

    protected void btnUploadPhotographs_Click(object sender, EventArgs e)
    {

        if (Page.IsValid)
        {
            if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;

                strActionName = "UploadPhotographs";
                NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_selfComplianceAttachment;
                arr_selfComplianceAttachment = obj_selfCompliance.GetPhotographsAttachmentList();

                if (arr_selfComplianceAttachment.Count() < 4)
                {
                    try
                    {
                        string str_fname;
                        string str_ext;

                        NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();
                        NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));

                        //List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();

                        byte[] buffer = new byte[1];

                        if (FileUploadPhotographs.HasFile)
                        {

                            str_ext = System.IO.Path.GetExtension(FileUploadPhotographs.PostedFile.FileName).ToLower();

                            str_fname = FileUploadPhotographs.FileName;

                            if ( str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {

                                if (NOCAPExternalUtility.IsValidFile(FileUploadPhotographs.PostedFile))
                                {
                                    if (FileUploadPhotographs.PostedFile.ContentLength < AttachmentSizeLimitForPhotoGraph())
                                    {
                                        obj_selfComplianceAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadPhotographs.PostedFile);
                                        obj_selfComplianceAttachment.ContentType = FileUploadPhotographs.PostedFile.ContentType;
                                        obj_selfComplianceAttachment.AttachmentPath = FileUploadPhotographs.FileName;
                                        obj_selfComplianceAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessagePhotographs.Text = "File can not upload. It has more than 500 KB size";
                                        lblMessagePhotographs.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessagePhotographs.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessagePhotographs.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }

                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessagePhotographs.Text = "Not a valid file!!..Select an other file!!";
                                lblMessagePhotographs.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessagePhotographs.Text = "Please select a file..!!";
                            lblMessagePhotographs.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_selfComplianceAttachment.ApplicationCode = obj_SelfCompliance.ApplicationCode;
                        obj_selfComplianceAttachment.AttachmentCode = obj_SelfCompliance.PhotoGraphAttachmentCode;
                        obj_selfComplianceAttachment.AttachmentName = txtPhotographs.Text;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_selfComplianceAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;


                        if (obj_selfComplianceAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessagePhotographs.Text = "File Upload Success";
                            lblMessagePhotographs.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessagePhotographs.Text = obj_selfComplianceAttachment.CustumMessage;
                            lblMessagePhotographs.ForeColor = System.Drawing.Color.Red;

                        }

                   //     lblMessagePhotographs.ForeColor = Color.Red;
                        txtSelfComplianceAttachment.Text = "";
                        lblMessageSelfCompliance.Text = "";

                        BindPhotographsAttachment(Convert.ToInt64(lblApplicationCodeFrom.Text));

                    }
                    catch (Exception)
                    {
                        Response.Redirect("~/ExternalErrorPage.aspx", false);
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
                else
                {
                    lblMessagePhotographs.Text = "Maximum number of files to be uploaded is 5";
                    lblMessagePhotographs.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }



    protected void btnUploadSelfComplianceAttachment_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;

                strActionName = "UploadPhotographs";
                NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_selfComplianceAttachment;
                arr_selfComplianceAttachment = obj_selfCompliance.GetSelfComplianceAttachmentList();

                if (arr_selfComplianceAttachment.Count() < 1)
                {
                    try
                    {
                        string str_fname;
                        string str_ext;

                        NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();
                        NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));

                        //List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();

                        byte[] buffer = new byte[1];

                        if (FileUploadSelfComplianceAttachment.HasFile)
                        {

                            str_ext = System.IO.Path.GetExtension(FileUploadSelfComplianceAttachment.PostedFile.FileName).ToLower();

                            str_fname = FileUploadSelfComplianceAttachment.FileName;

                            if (str_ext == ".doc" || str_ext == ".docx" || str_ext == ".pdf")
                            {

                                if (NOCAPExternalUtility.IsValidFile(FileUploadSelfComplianceAttachment.PostedFile))
                                {
                                    if (FileUploadSelfComplianceAttachment.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_selfComplianceAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadSelfComplianceAttachment.PostedFile);
                                        obj_selfComplianceAttachment.ContentType = FileUploadSelfComplianceAttachment.PostedFile.ContentType;
                                        obj_selfComplianceAttachment.AttachmentPath = FileUploadSelfComplianceAttachment.FileName;
                                        obj_selfComplianceAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageSelfCompliance.Text = "File can not upload. It has more than 10 MB size";
                                        lblMessageSelfCompliance.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageSelfCompliance.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageSelfCompliance.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }

                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageSelfCompliance.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageSelfCompliance.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageSelfCompliance.Text = "Please select a file..!!";
                            lblMessageSelfCompliance.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_selfComplianceAttachment.ApplicationCode = obj_SelfCompliance.ApplicationCode;
                        obj_selfComplianceAttachment.AttachmentCode = obj_SelfCompliance.SelfComplianceAttachmentCode;
                        obj_selfComplianceAttachment.AttachmentName = txtSelfComplianceAttachment.Text;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_selfComplianceAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;


                        if (obj_selfComplianceAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageSelfCompliance.Text = "File Upload Success";
                            lblMessageSelfCompliance.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageSelfCompliance.Text = obj_selfComplianceAttachment.CustumMessage;
                            lblMessageSelfCompliance.ForeColor = System.Drawing.Color.Red;
                        }

                        //lblMessageSelfCompliance.ForeColor = Color.Green;
                        lblMessagePhotographs.Text = "";
                        txtPhotographs.Text = "";

                        BindSelfComplianceAttachment(Convert.ToInt64(lblApplicationCodeFrom.Text));
                    }
                    catch (Exception)
                    {
                        Response.Redirect("~/ExternalErrorPage.aspx", false);
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
                else
                {
                    lblMessageSelfCompliance.Text = "Maximum number of files to be uploaded is 1";
                    lblMessageSelfCompliance.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void gvSelfComplianceAttachment_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
          
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            Server.Transfer("SelfCompliance.aspx");
         
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
                strActionName = "Submit Application";
                try
                {
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    if (obj_SelfCompliance.ApplicationCode != 0)
                    {

                        if (obj_SelfCompliance.SubmitApplication(obj_SelfCompliance.ApplicationCode, null, obj_externalUser.ExternalUserCode) == 1)
                        {
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Now your record is ready to take action');", true);
                            Server.Transfer("~/ExternalUser/ApplicantHome.aspx");
                        }
                        else
                        {
                            strStatus = "Application Submit Failed";
                            lblFinalMsg.Text = obj_SelfCompliance.CustumMessage;
                            lblFinalMsg.ForeColor = System.Drawing.Color.Red;

                            //display error
                        }

                    }


                }
                catch (ThreadAbortException)
                {


                }
                catch (Exception)
                {
                    Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
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
        }
    }

    


    private int AttachmentSizeLimitForPhotoGraph()
    {
        try
        {

            int AttachmentSize = 512001; //500KB
            return AttachmentSize;
        }
        catch
        {
            Response.Redirect("~/InternalErrorPage.aspx", false);
            return 0;
        }
    }

    private void BindPhotographsAttachment(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(lngA_ApplicationCode);
            NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_SelfComplianceAttachment;
            arr_SelfComplianceAttachment = obj_selfCompliance.GetPhotographsAttachmentList();

            gvPhotographs.DataSource = arr_SelfComplianceAttachment;
            gvPhotographs.DataBind();

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindSelfComplianceAttachment(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(lngA_ApplicationCode);
            NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_SelfComplianceAttachment;
            arr_SelfComplianceAttachment = obj_selfCompliance.GetSelfComplianceAttachmentList();

            gvSelfComplianceAttachment.DataSource = arr_SelfComplianceAttachment;
            gvSelfComplianceAttachment.DataBind();

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    protected void lbtnViewSelfComplianceAttachmentFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();

                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_applicationCode = Convert.ToInt64(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachmentB = obj_selfComplianceAttachment.DownloadFile(lng_applicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

                    if (obj_selfComplianceAttachmentB != null)
                    {
                        byte[] bytes = obj_selfComplianceAttachmentB.AttachmentFile;
                        HttpContext.Current.Response.Buffer = true;
                        HttpContext.Current.Response.Charset = "";
                        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        HttpContext.Current.Response.ContentType = obj_selfComplianceAttachmentB.ContentType;
                        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "SelfComp_" + Convert.ToString(lng_applicationCode) + "_" + Convert.ToString(int_attachmentCode) + "_" + Convert.ToString(int_attachmentCodeSerialNumber) + obj_selfComplianceAttachmentB.FileExtension);
                        HttpContext.Current.Response.BinaryWrite(bytes);
                        HttpContext.Current.Response.Flush();


                    }
                    
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
            }
        }
    }
    protected void lbtnViewFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();

                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_applicationCode = Convert.ToInt64(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachmentB  = obj_selfComplianceAttachment.DownloadFile(lng_applicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

                    if (obj_selfComplianceAttachmentB != null)
                    {
                        byte[] bytes = obj_selfComplianceAttachmentB.AttachmentFile;
                        HttpContext.Current.Response.Buffer = true;
                        HttpContext.Current.Response.Charset = "";
                        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        HttpContext.Current.Response.ContentType = HttpUtility.HtmlEncode(obj_selfComplianceAttachmentB.ContentType);
                        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "SelfComp_" + Convert.ToString(lng_applicationCode) + "_" + Convert.ToString(int_attachmentCode) + "_" + Convert.ToString(int_attachmentCodeSerialNumber) + obj_selfComplianceAttachmentB.FileExtension);
                        HttpContext.Current.Response.BinaryWrite(bytes);
                        HttpContext.Current.Response.Flush();

                        
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
            }
        }
    }
    protected void gvSelfComplianceAttachment_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "Delete Self Compliance Attachment";
                long lng_ApplicationCode = Convert.ToInt32(gvSelfComplianceAttachment.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvSelfComplianceAttachment.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvSelfComplianceAttachment.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_SelfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment(lng_ApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);

                //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                //string str_fullPath = str_startPathFromConfig + obj_SelfComplianceAttachment.AttachmentPath;


                if (obj_SelfComplianceAttachment.Delete() == 1)
                {
                    strStatus = "File Deleted Success";
                    lblMessageSelfCompliance.Text = obj_SelfComplianceAttachment.CustumMessage;
                    lblMessageSelfCompliance.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    BindSelfComplianceAttachment(Convert.ToInt64(lblApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Deleted Failed";
                    lblMessageSelfCompliance.Text = obj_SelfComplianceAttachment.CustumMessage;
                    lblMessageSelfCompliance.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
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
    }
}