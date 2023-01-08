using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Threading;
using System.IO;
using System.Drawing;

public partial class ExternalUser_Expansion_IND_SalientFeatureKLD : System.Web.UI.Page
{
    string strPageName = "SalientFeature";
    string strActionName = "";
    string strStatus = "";

    long lngIndSubmitAppCode;
    public long IndSubmitAppCode
    {
        get
        {
            return lngIndSubmitAppCode;
        }
        set
        {
            lngIndSubmitAppCode = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;

            lblMessage.Text = "";
            try
            {
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
                        {
                            lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                        }
                    }

                }

                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    BindLandUseFormDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    BindMSMEAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                }
                DisplayApplicationStop();
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }
    private void BindLandUseFormDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Master.ExemptionLetterConditions Obj_Exemption = new NOCAP.BLL.Master.ExemptionLetterConditions();

            lblExemptionMessage.Text = "You are exempted from seeking NOC for ground water withdrawal from CGWA, since your project is under MSME category and ground water withdrawal is less than 10 KLD as per new guidelines w.e.f 24.09.2020.<br/> Please upload MSME certificate for getting Exemtion.";
            lblExemptionMessage.ForeColor = System.Drawing.Color.Green;
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
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
            Server.Transfer("IndustrialNewKLD.aspx");
        }
    }
    protected void btnSaveAsDraft_Click1(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                try
                {
                    if (DisplayApplicationStop() == 1)
                    {
                        UpdateSalientFeaturesDetail(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    }

                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
                }
            }
        }
    }
    private int DisplayApplicationStop()
    {
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
        NOCAP.BLL.Master.ApplicationStop obj_applicationStop = new NOCAP.BLL.Master.ApplicationStop(obj_industrialNewApplication.ApplicationTypeCode, obj_industrialNewApplication.ApplicationPurposeCode);
        if (obj_applicationStop.Stop == NOCAP.BLL.Master.ApplicationStop.StopYesNo.Yes)
        {
            lblMessage.Text = HttpUtility.HtmlEncode("Submission-Presently Closed." + " We are not accepting application.");
            lblMessage.Enabled = true;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;
            btnSubmit.Enabled = false;
            return 0;
        }
        else
            return 1;
    }
    private int UpdateSalientFeaturesDetail(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(lngA_ApplicationCode);

            //obj_industrialNewApplication.SalientFeatureOfIndustrialActivity = txtSalientFeaturesOfInActivity.Text;


            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_industrialNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;







            if (obj_industrialNewApplication.Update() == 1)
            {
                lblMessage.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.CustumMessage); //Added New for bringing proper message
                lblMessage.ForeColor = System.Drawing.Color.Green;//Added New for bringing proper message
                strActionName = "SaveAsDraft";
                strStatus = "Record Save Successfully !";
                return 1;
            }
            else
            {
                strActionName = "SaveAsDraft";
                strStatus = "Record Save Failed !";

                lblMessage.Text = obj_industrialNewApplication.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            //}
        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
            //lblMessage.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
            return 0;
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
                arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetMSMEAttachmentList();
                if (arr_industrialNewApplicationAttachmentList.Count() <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Upload MSME Certificate');", true);

                }
                else
                {
                    if (DisplayApplicationStop() == 1)
                    {
                        if (UpdateSalientFeaturesDetail(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text)) == 1)
                        {
                            long lng_submittedApplicationCode = 0;
                            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));



                            if (obj_industrialNewApplication.SubmitApplication(out lng_submittedApplicationCode, null, obj_externalUser.ExternalUserCode) == 1)
                            {
                                strActionName = "Submit";
                                strStatus = "Record Save Successfully !";
                                lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(lng_submittedApplicationCode);
                                lngIndSubmitAppCode = lng_submittedApplicationCode;
                                Server.Transfer("~/ExternalUser/IndustrialNew/IndExemSubmitSuccessKLD.aspx");

                            }
                            else
                            {
                                lblMessage.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.CustumMessage);  //Added New for bringing proper message
                                strActionName = "Submit";
                                strStatus = "Record Save Failed !";
                            }


                        }
                        else
                        {
                            strActionName = "SaveAsDraft";
                            strStatus = "Record Save Failed !";

                            lblMessage.Text = "Record Save Failed !";
                            lblMessage.ForeColor = System.Drawing.Color.Red;

                        }
                    }
                }

            }
            catch (ThreadAbortException)
            {
                strActionName = "Submit";
            }
            catch (Exception)
            {
                strActionName = "Submit";
                strStatus = "Record Save Failed !";
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


    private void BindGridView(GridView gv, long lngA_ApplicationCode, ref int AttCount)
    {
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(lngA_ApplicationCode);
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList = null;

        arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetMSMEAttachmentList();

        gv.DataSource = arr_industrialNewApplicationAttachmentList;
        gv.DataBind();
        AttCount = arr_industrialNewApplicationAttachmentList.Length;

    }
    private void BindMSMEAttachment(long lngA_ApplicationCode)
    {
        try {
            int AttCount = 0;
            BindGridView(gvMSME, lngA_ApplicationCode, ref AttCount);
            lblMSME.Text = HttpUtility.HtmlEncode("MSME certificate in case of MSME (" + AttCount.ToString() + ")");
           
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void clearMessage()
    {
        lblMSMEMessage.Text = "";
        txtMSME.Text = "";

    }
    private int AttachmentSizeLimit()
    {
        try
        {
            NOCAP.BLL.Common.AttachmentLimit obj_attachmentLimit = new NOCAP.BLL.Common.AttachmentLimit();

            int AttachmentSize = 1048576 * (obj_attachmentLimit.SizeOfEachAttachment);
            return AttachmentSize;
        }
        catch
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
            return 0;
        }

    }

    private int AttachmentNumberLimit()
    {
        try
        {
            NOCAP.BLL.Common.AttachmentLimit obj_attachmentNoLimit = new NOCAP.BLL.Common.AttachmentLimit();

            int AttachmentNumber = obj_attachmentNoLimit.NumberOfAttachment;
            return AttachmentNumber;
        }
        catch
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
            return 0;
        }
        finally
        {
        }
    }
    protected void btnUploadMSME_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "File MSME";
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
                arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetMSMEAttachmentList();

                if (arr_industrialNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_insertIndustrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment();
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadMSME.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadMSME.PostedFile.FileName).ToLower();
                            str_fname = FileUploadMSME.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadMSME.PostedFile))
                                {
                                    if (FileUploadMSME.PostedFile.ContentLength < AttachmentSizeLimit())
                                    {
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadMSME.PostedFile);
                                        obj_insertIndustrialNewApplicationAttachment.ContentType = FileUploadMSME.PostedFile.ContentType;
                                        obj_insertIndustrialNewApplicationAttachment.AttachmentPath = FileUploadMSME.FileName;
                                        obj_insertIndustrialNewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMSMEMessage.Text = "File can not upload. It has more than 5 MB size";
                                        lblMSMEMessage.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMSMEMessage.Text = "Not a valid file!!..Select an other file!!";
                                    lblMSMEMessage.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMSMEMessage.Text = "Not a valid file!!..Select an other file!!";
                                lblMSMEMessage.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMSMEMessage.Text = "Please select a file..!!";
                            lblMSMEMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertIndustrialNewApplicationAttachment.IndustrialNewApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentCode = obj_industrialNewApplication.MSMEAttCode;
                        obj_insertIndustrialNewApplicationAttachment.AttachmentName = txtMSME.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertIndustrialNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertIndustrialNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            lblMSMEMessage.ForeColor = Color.Green;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMSMEMessage.ForeColor = System.Drawing.Color.Red;
                        }

                        BindMSMEAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        clearMessage();
                        lblMSMEMessage.Text = obj_insertIndustrialNewApplicationAttachment.CustumMessage;
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
                    lblMessageExtra.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    private int DeleteAttchment(GridView gv, GridViewDeleteEventArgs e, Label lblMessage, string str_ActionName)
    {
        try
        {
            long lng_IndustrialNewApplicationCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["IndustrialNewApplicationCode"]);
            int int_AttachmentCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["AttachmentCode"]);
            int int_AttachmentCodeSerialNumber = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
            lblMessage.ForeColor = System.Drawing.Color.Red;
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_industrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment(lng_IndustrialNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
            if (obj_industrialNewApplicationAttachment.Delete() == 1)
            {
                lblMessage.Text = obj_industrialNewApplicationAttachment.CustumMessage;
                strStatus = "File Delete Success";
                return 1;
            }
            else
            {
                lblMessage.Text = obj_industrialNewApplicationAttachment.CustumMessage;
                strStatus = "File Delete Failed";
                return 0;
            }
        }
        catch (Exception ex)
        { return 0; }
        finally
        {
            ActionTrail obj_ExtActionTrail = new ActionTrail();
            if (Session["ExternalUserCode"] != null)
            {
                obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
                obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
                obj_ExtActionTrail.ActionPerformed = strPageName + "-" + str_ActionName;
                obj_ExtActionTrail.Status = strStatus;
                if (obj_ExtActionTrail != null)
                    ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
            }
        }
    }
    protected void gvMSME_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "File Delete MSME";
                if (DeleteAttchment((GridView)sender, e, lblMSMEMessage, strActionName) == 1)
                    BindMSMEAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }

    protected void ViewFile(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {

                if (e.CommandArgument != null)
                {

                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_industrialNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.INDSADAppDownloadFiles(lng_industrialNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
            }
        }
    }
}