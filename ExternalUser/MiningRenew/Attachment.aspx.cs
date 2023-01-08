using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Drawing;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Threading;
using NOCAP.BLL.Mining.Renew.SADApplication;



public partial class ExternalUser_MiningRenew_Attachment : System.Web.UI.Page
{
    string strPageName = "MINRenAttachment";
    string strActionName = "";
    string strStatus = "";
    //decimal GroundWaterRequirement0KLD = 0;
    //decimal GroundWaterRequirement10KLD = 10;
    decimal GroundWaterRequirement100KLD = 100;
    long lngMinSubmitAppCode;
    public long MinSubmitAppCode
    {
        get
        {
            return lngMinSubmitAppCode;
        }
        set
        {
            lngMinSubmitAppCode = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        if (!IsPostBack)
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;

           

            try
            {
                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblModeFrom");
                        if (SourceLabel != null) { lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); }
                    }
                }
                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblMiningApplicationCodeFrom");
                        if (SourceLabel != null) { lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    lblApplicationCode.Text = lblMiningApplicationCodeFrom.Text;

                    BindMiningRenewApplicationAttachmentGroundWaterFlowDirectionDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningRenewApplicationAttachmentGroundWaterObservationWellsDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningRenewApplicationAttachmentGQualityOfGroundWaterInAreaDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningRenewApplicationAttachmentChangesInTopographyDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningRenewApplicationAttachmentChangesInDrainageDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));


                    BindMiningRenewApplicationAttachmentReasonForNotApplyingBeforeDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningRenewApplicationAttachmentExisingNOCDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));


                    //BindMiningRenewApplicationAttachmentDetailsUtilizationOfPumpedWaterDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    //BindMiningRenewApplicationAttachmentGroundWaterRegimeMapDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                    BindMiningRenewApplicationAttachmentExtraAttachmentsDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningRenewApplicationComplianceConditionNOC(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningRenewComplianceConditionNOCAttachmentDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningRenewApplicationAttachmentBharatKoshRecieptDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningRenewApplicationAttachmentSignedDocDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindMiningRenewApplicationComplianceConditionNOCOther();
                    BindMiningRenewComplianceConditionNOCOtherAttachmentDetails();


                    BindAffidavitAttachmentDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindWaterAuditReportAttachmentDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindSourceWaterNonAvailabilityCertificateAttachmentDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    BindWaterQualityMinSeepAttachmentDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    
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

    
    protected void txtSubmit_Click(object sender, EventArgs e)
    {
        strActionName = "Submit";

        long lng_submittedApplicationCode = 0;
        clearMessage();
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
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
                string ErrorMessage = string.Empty;

                if (obj_MiningRenewApplication.GetGroundWaterflowDirectionAttachmentList().Length < 1) { ErrorMessage = "GroundWaterFlowDirectionAttachment"; }
                if (obj_MiningRenewApplication.GetMonitorGroundWaterRegimeObservationWellAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "GWLevelOfObservationWells/Piezometer" : ErrorMessage + ", GWLevelOfObservationWells/Piezometer"; }
                if (obj_MiningRenewApplication.GetMonitorGroundWaterRegimeGQsuroudingsAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "GeneralQualityOfGWInArea" : ErrorMessage + ", GeneralQualityOfGWInArea"; }
                if (obj_MiningRenewApplication.GetExistingNOCAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "ExistingNOC" : ErrorMessage + ", ExistingNOC"; }
               // if (lblBharatKosh2.Visible) { if (obj_MiningRenewApplication.GetBharatKoshRecieptAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Bharat Kosh Reciept" : ErrorMessage + ",Bharat Kosh Reciept"; } }
                if (lblSignDoc2.Visible) { if (obj_MiningRenewApplication.GetSignedDocAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Aplication with Signature and Seal" : ErrorMessage + ",Aplication with Signature and Seal"; }
                }
                if (lblAffidavit2.Visible) { if (Convert.ToInt32(hdnAffidavit.Value) < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Affidavit of Compliance of NOC Condition" : ErrorMessage + ",Affidavit of Compliance of NOC Condition"; } }
                if (lblWaterAuditReport2.Visible) { if (Convert.ToInt32(hdnWaterAuditReport.Value) < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Water Audit Report" : ErrorMessage + ",Water Audit Report"; } }
                if (lblSourceWaterAvailability2.Visible) { if (Convert.ToInt32(hdnSourceWaterAvailability.Value) < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Source Water Non-availability Certificate" : ErrorMessage + ",Source Water Non-availability Certificate"; } }
                if (lblWaterQualityMinSeep2.Visible) { if (Convert.ToInt32(hdnWaterQualityMinSeep.Value) < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Water Quality Report Of Mine Seepage/Discharge" : ErrorMessage + ",Water Quality Report Of Mine Seepage/Discharge"; } }

                if (!string.IsNullOrEmpty(ErrorMessage))
                {
                    lblMessage.Text = ErrorMessage + " Attachments are Mandatory.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                lngMinSubmitAppCode = lng_submittedApplicationCode;
                Server.Transfer("MINRenewReadyToSubmit.aspx");
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception)
            {
                strStatus = "Record Save Failed !";
                //lblFinalMsg.Text = HttpUtility.HtmlEncode(ex.Message);
                // error handle
            }
            finally
            {
                obj_MiningRenewApplication.Dispose();

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

    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {

    }
    protected void btnPrev_Click(object sender, EventArgs e)
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
                Server.Transfer("~/ExternalUser/MiningRenew/SelfDeclaration.aspx");
            }
        }
    }
    

    #region RowDataBound
    protected void gvMINRenewComplianceConditionNOC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblCompCondNOCDesc = (Label)e.Row.FindControl("lblCompCondNOCType");
                    Label lblCompCondNOCCode = (Label)e.Row.FindControl("lblCompCondNOCCode");

                    NOCAP.BLL.Master.ComplianceConditionNOC objComplianceConditionNOC = new NOCAP.BLL.Master.ComplianceConditionNOC(Convert.ToInt32(lblCompCondNOCCode.Text));
                    lblCompCondNOCDesc.Text = HttpUtility.HtmlEncode(objComplianceConditionNOC.ComplianceConditionDescription);

                    //e.Row.Cells[2].

                    BindMiningRenewComplianceConditionNOCAttachmentDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }
    #endregion

    #region RowCommand
    protected void gvMINRenewComplianceConditionNOC_RowCommand(object sender, GridViewCommandEventArgs e)
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
                Label lblMsgMINCompCondAttachment = null;
                if (e.CommandName == "UploadFileForCompCondNOC")
                {
                    strActionName = "Upload Compliance Condition NOC Attachment";

                    GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                    FileUpload FileUploadCompCondNOCAtt = (FileUpload)row.FindControl("FileUploadCompCondNOC");
                    TextBox txtFileCompCondNOCAttachment = (TextBox)row.FindControl("txtAttachmentNameCompCondNOC");
                    GridView gvCompCondNOCAtt = (GridView)row.FindControl("gvCompCondNOCAttachment");
                    Label lblCompCondNOCCode = (Label)row.FindControl("lblCompCondNOCCode");

                    int b = row.RowIndex;

                    foreach (GridViewRow row1 in gvMINRenewComplianceConditionNOC.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            Label lblMsgMINCompCondNOCAttachmentDelete = (Label)row1.FindControl("lblMessageMINCompCondNOCAttachmentDelete");
                            lblMsgMINCompCondNOCAttachmentDelete.Text = "";
                            Label lblMsgMINCompCondNOCAttachment1 = (Label)row1.FindControl("lblMessageMINCompCondAttachment");
                            lblMsgMINCompCondNOCAttachment1.Text = "";

                            if (row1.RowIndex == b)
                            {
                                lblMsgMINCompCondAttachment = (Label)gvMINRenewComplianceConditionNOC.Rows[b].FindControl("lblMessageMINCompCondAttachment");
                            }
                            else
                            {
                                TextBox txtAttachmentName = (TextBox)row1.FindControl("txtAttachmentNameCompCondNOC");
                                txtAttachmentName.Text = "";
                            }
                        }
                    }


                    // lblMsgMINCompCondAttachment.Text = "";

                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplicationForNoLimit = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOC obj_MiningRenewSADComplianceConditionNOC = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOC(Convert.ToInt32(lblMiningApplicationCodeFrom.Text), Convert.ToInt32(lblCompCondNOCCode.Text));
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;

                    arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplicationForNoLimit.GetMiningComplianceConditionNOCAttachmentList(obj_MiningRenewSADComplianceConditionNOC.ComplianceConditionAttachmentCode);

                    if (arr_MiningRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                    {
                        try
                        {
                            string str_fname;
                            string str_ext;

                            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_insertMiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment();
                            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                            List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_MiningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();

                            byte[] buffer = new byte[1];

                            if (FileUploadCompCondNOCAtt.HasFile)
                            {
                                str_ext = System.IO.Path.GetExtension(FileUploadCompCondNOCAtt.PostedFile.FileName).ToLower();
                                str_fname = FileUploadCompCondNOCAtt.FileName;

                                if (str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                                {
                                    if (NOCAPExternalUtility.IsValidFile(FileUploadCompCondNOCAtt.PostedFile))
                                    {
                                        if (FileUploadCompCondNOCAtt.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                        {
                                            obj_insertMiningRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadCompCondNOCAtt.PostedFile);
                                            obj_insertMiningRenewApplicationAttachment.ContentType = FileUploadCompCondNOCAtt.PostedFile.ContentType;
                                            obj_insertMiningRenewApplicationAttachment.AttachmentPath = FileUploadCompCondNOCAtt.FileName;
                                            obj_insertMiningRenewApplicationAttachment.FileExtension = str_ext;
                                        }
                                        else
                                        {
                                            strStatus = "File Upload Failed";
                                            lblMsgMINCompCondAttachment.Text = "File can not upload. It has more than 5 MB size";
                                            lblMsgMINCompCondAttachment.ForeColor = System.Drawing.Color.Red;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMsgMINCompCondAttachment.Text = "Not a valid file!!..Select another file!!";
                                        lblMsgMINCompCondAttachment.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMsgMINCompCondAttachment.Text = "Not a valid file!!..Select another file!!";
                                    lblMsgMINCompCondAttachment.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                lblMsgMINCompCondAttachment.Text = "Please select a file..!!";
                                lblMsgMINCompCondAttachment.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                            obj_insertMiningRenewApplicationAttachment.MiningRenewApplicationCode = obj_MiningRenewApplication.MiningRenewApplicationCode;
                            obj_insertMiningRenewApplicationAttachment.AttachmentCode = obj_MiningRenewSADComplianceConditionNOC.ComplianceConditionAttachmentCode;
                            obj_insertMiningRenewApplicationAttachment.AttachmentName = txtFileCompCondNOCAttachment.Text;

                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                            obj_insertMiningRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                            if (obj_insertMiningRenewApplicationAttachment.Add() == 1)
                            {
                                strStatus = "File Upload Success";
                                lblMsgMINCompCondAttachment.Text = "File Uploaded Successfully";
                                lblMsgMINCompCondAttachment.ForeColor = System.Drawing.Color.Green;

                                BindMiningRenewComplianceConditionNOCAttachmentDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMsgMINCompCondAttachment.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;
                                lblMsgMINCompCondAttachment.ForeColor = System.Drawing.Color.Red;

                            }

                            //lblMsgMINCompCondAttachment.ForeColor = Color.Green;
                            //lblMsgMINCompCondAttachment.Text = "";
                            txtFileCompCondNOCAttachment.Text = "";
                            BindMiningRenewComplianceConditionNOCAttachmentDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));

                            lblMessageGWFlowDirectionMap.Text = "";
                            lblMessageGWLevelObservation.Text = "";
                            lblMessageGQofGWInArea.Text = "";
                            lblMessageChangesInTopography.Text = "";
                            lblMessageChangesInDrainage.Text = "";
                            lblMessageExistingNOC.Text = "";
                            lblMessageExtra.Text = "";
                            lblMessageReasonForNotApplyingBefore.Text = "";
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
                        strStatus = "Maximum number of files to be uploaded is 5";
                        lblMsgMINCompCondAttachment.Text = "Maximum number of files to be uploaded is 5";
                        lblMsgMINCompCondAttachment.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void gvCompCondNOCAttachment_RowCommand(object sender, GridViewCommandEventArgs e)
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
                Label lblMsgMINCompCondNOCAttachmentDelete = null;
                if (e.CommandName == "DeleteFile")
                {
                    strActionName = "Delete File MINCompCondNOCAttachment";

                    GridViewRow gvCompCondNOCAttachmentRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    GridView gvCompCondNOCAttachment = (GridView)(gvCompCondNOCAttachmentRow.Parent.Parent);
                    GridViewRow gvMINRenewComplianceConditionNOCRow = (GridViewRow)(gvCompCondNOCAttachment.NamingContainer);

                    int b = gvMINRenewComplianceConditionNOCRow.RowIndex;

                    foreach (GridViewRow row in gvMINRenewComplianceConditionNOC.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            Label lblMsgMINCompCondNOCAttachmentDelete1 = (Label)row.FindControl("lblMessageMINCompCondNOCAttachmentDelete");
                            lblMsgMINCompCondNOCAttachmentDelete1.Text = "";
                            Label lblMsgMINCompCondAttachment = (Label)row.FindControl("lblMessageMINCompCondAttachment");
                            lblMsgMINCompCondAttachment.Text = "";

                            TextBox txtAttachmentName = (TextBox)row.FindControl("txtAttachmentNameCompCondNOC");
                            txtAttachmentName.Text = "";

                            if (row.RowIndex == b)
                            {
                                lblMsgMINCompCondNOCAttachmentDelete = (Label)gvMINRenewComplianceConditionNOC.Rows[b].FindControl("lblMessageMINCompCondNOCAttachmentDelete");
                            }
                        }
                    }

                    string[] CommandArgument = e.CommandArgument.ToString().Split(',');
                    long lng_MiningRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_AttachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_AttachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_MiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment(lng_MiningRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);

                    //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                    //string str_fullPath = str_startPathFromConfig + obj_MiningRenewApplicationAttachment.AttachmentPath;


                    if (obj_MiningRenewApplicationAttachment.Delete() == 1)
                    {
                        strStatus = "File Deleted Success";

                        lblMsgMINCompCondNOCAttachmentDelete.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                        lblMsgMINCompCondNOCAttachmentDelete.ForeColor = System.Drawing.Color.Red;

                        //if (File.Exists(str_fullPath))
                        //{
                        //    File.Delete(str_fullPath);
                        //}
                        BindMiningRenewComplianceConditionNOCAttachmentDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    }
                    else
                    {
                        strStatus = "File Deleted Failed";
                        lblMsgMINCompCondNOCAttachmentDelete.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                        lblMsgMINCompCondNOCAttachmentDelete.ForeColor = System.Drawing.Color.Red;
                    }
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
  
    protected void gvMINRenewComplianceConditionNOCOther_RowCommand(object sender, GridViewCommandEventArgs e)
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
                Label lblMsgMINCompCondAttachmentOther = null;
                if (e.CommandName == "UploadFileForCompCondNOCOther")
                {
                    strActionName = "Upload Compliance Condition NOC - Other Attachment";

                    GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                    FileUpload FileUploadCompCondNOCAttOther = (FileUpload)row.FindControl("FileUploadCompCondNOCOther");
                    TextBox txtFileCompCondNOCAttachmentOther = (TextBox)row.FindControl("txtAttachmentNameCompCondNOCOther");
                    GridView gvCompCondNOCAttOther = (GridView)row.FindControl("gvCompCondNOCAttachmentOther");
                    Label lblCompCondNOCOtherSerialNumber = (Label)row.FindControl("lblCompCondNOCOtherSerialNumber");

                    int b = row.RowIndex;

                    foreach (GridViewRow row1 in gvMINRenewComplianceConditionNOCOther.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            Label lblMsgMINCompCondNOCAttachmentOtherDelete1 = (Label)row1.FindControl("lblMessageMINCompCondNOCAttachmentOtherDelete");
                            lblMsgMINCompCondNOCAttachmentOtherDelete1.Text = "";

                            Label lblMessageMINCompCondAttachmentOther1 = (Label)row1.FindControl("lblMessageMINCompCondAttachmentOther");
                            lblMessageMINCompCondAttachmentOther1.Text = "";

                            if (row1.RowIndex == b)
                            {
                                lblMsgMINCompCondAttachmentOther = (Label)gvMINRenewComplianceConditionNOCOther.Rows[b].FindControl("lblMessageMINCompCondAttachmentOther");
                            }
                            else
                            {
                                TextBox txtAttachmentName = (TextBox)row1.FindControl("txtAttachmentNameCompCondNOCOther");
                                txtAttachmentName.Text = "";
                            }
                        }
                    }

                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplicationForNoLimit = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOCOther obj_MiningRenewSADComplianceConditionNOCOther = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOCOther(Convert.ToInt32(lblMiningApplicationCodeFrom.Text), Convert.ToInt32(lblCompCondNOCOtherSerialNumber.Text));
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;

                    arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplicationForNoLimit.GetMiningComplianceConditionNOCOtherAttachmentList(obj_MiningRenewSADComplianceConditionNOCOther.ComplianceConditionAttachmentCode);

                    if (arr_MiningRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                    {
                        try
                        {
                            string str_fname;
                            string str_ext;

                            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_insertMiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment();
                            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                            List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_MiningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();

                            byte[] buffer = new byte[1];

                            if (FileUploadCompCondNOCAttOther.HasFile)
                            {
                                str_ext = System.IO.Path.GetExtension(FileUploadCompCondNOCAttOther.PostedFile.FileName).ToLower();
                                str_fname = FileUploadCompCondNOCAttOther.FileName;

                                if (str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                                {
                                    if (NOCAPExternalUtility.IsValidFile(FileUploadCompCondNOCAttOther.PostedFile))
                                    {
                                        if (FileUploadCompCondNOCAttOther.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                        {
                                            obj_insertMiningRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadCompCondNOCAttOther.PostedFile);
                                            obj_insertMiningRenewApplicationAttachment.ContentType = FileUploadCompCondNOCAttOther.PostedFile.ContentType;
                                            obj_insertMiningRenewApplicationAttachment.AttachmentPath = FileUploadCompCondNOCAttOther.FileName;
                                            obj_insertMiningRenewApplicationAttachment.FileExtension = str_ext;
                                        }
                                        else
                                        {
                                            strStatus = "File Upload Failed";
                                            lblMsgMINCompCondAttachmentOther.Text = "File can not upload. It has more than 5 MB size";
                                            lblMsgMINCompCondAttachmentOther.ForeColor = System.Drawing.Color.Red;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMsgMINCompCondAttachmentOther.Text = "Not a valid file!!..Select another file!!";
                                        lblMsgMINCompCondAttachmentOther.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMsgMINCompCondAttachmentOther.Text = "Not a valid file!!..Select another file!!";
                                    lblMsgMINCompCondAttachmentOther.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                lblMsgMINCompCondAttachmentOther.Text = "Please select a file..!!";
                                lblMsgMINCompCondAttachmentOther.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                            obj_insertMiningRenewApplicationAttachment.MiningRenewApplicationCode = obj_MiningRenewApplication.MiningRenewApplicationCode;
                            obj_insertMiningRenewApplicationAttachment.AttachmentCode = obj_MiningRenewSADComplianceConditionNOCOther.ComplianceConditionAttachmentCode;
                            obj_insertMiningRenewApplicationAttachment.AttachmentName = txtFileCompCondNOCAttachmentOther.Text;

                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                            obj_insertMiningRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                            if (obj_insertMiningRenewApplicationAttachment.Add() == 1)
                            {
                                strStatus = "File Upload Success";
                                lblMsgMINCompCondAttachmentOther.Text = "File Upload Success";
                                lblMsgMINCompCondAttachmentOther.ForeColor = System.Drawing.Color.Green;

                                //BindMiningRenewComplianceConditionNOCOtherAttachmentDetails();
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMsgMINCompCondAttachmentOther.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;
                                lblMsgMINCompCondAttachmentOther.ForeColor = System.Drawing.Color.Red;

                            }


                            txtFileCompCondNOCAttachmentOther.Text = "";
                            BindMiningRenewComplianceConditionNOCOtherAttachmentDetails();


                            lblMessageGWFlowDirectionMap.Text = "";
                            lblMessageGWLevelObservation.Text = "";
                            lblMessageGQofGWInArea.Text = "";
                            lblMessageChangesInTopography.Text = "";
                            lblMessageChangesInDrainage.Text = "";
                            lblMessageExistingNOC.Text = "";
                            lblMessageExtra.Text = "";
                            lblMessageReasonForNotApplyingBefore.Text = "";

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
                        strStatus = "Maximum number of files to be uploaded is 5";
                        lblMsgMINCompCondAttachmentOther.Text = "Maximum number of files to be uploaded is 5";
                        lblMsgMINCompCondAttachmentOther.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }


    protected void gvCompCondNOCAttachmentOther_RowCommand(object sender, GridViewCommandEventArgs e)
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
                Label lblMsgMINCompCondNOCAttachmentOtherDelete = null;
                if (e.CommandName == "DeleteFile")
                {
                    strActionName = "Delete File MINCompCondNOCAttachmentOther";


                    GridViewRow gvCompCondNOCAttachmentOtherRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    GridView gvCompCondNOCAttachmentOther = (GridView)(gvCompCondNOCAttachmentOtherRow.Parent.Parent);
                    GridViewRow gvMINRenewComplianceConditionNOCOtherRow = (GridViewRow)(gvCompCondNOCAttachmentOther.NamingContainer);

                    int b = gvMINRenewComplianceConditionNOCOtherRow.RowIndex;

                    foreach (GridViewRow row in gvMINRenewComplianceConditionNOCOther.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            Label lblMsgMINCompCondNOCAttachmentOtherDelete1 = (Label)row.FindControl("lblMessageMINCompCondNOCAttachmentOtherDelete");
                            lblMsgMINCompCondNOCAttachmentOtherDelete1.Text = "";
                            Label lblMsgMINCompCondAttachmentOther = (Label)row.FindControl("lblMessageMINCompCondAttachmentOther");
                            lblMsgMINCompCondAttachmentOther.Text = "";

                            TextBox txtAttachmentNameOther = (TextBox)row.FindControl("txtAttachmentNameCompCondNOCOther");
                            txtAttachmentNameOther.Text = "";


                            if (row.RowIndex == b)
                            {
                                lblMsgMINCompCondNOCAttachmentOtherDelete = (Label)gvMINRenewComplianceConditionNOCOther.Rows[b].FindControl("lblMessageMINCompCondNOCAttachmentOtherDelete");
                            }
                        }
                    }

                    string[] CommandArgument = e.CommandArgument.ToString().Split(',');
                    long lng_MiningRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_AttachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_AttachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);


                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_MiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment(lng_MiningRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);

                    //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                    //string str_fullPath = str_startPathFromConfig + obj_MiningRenewApplicationAttachment.AttachmentPath;


                    if (obj_MiningRenewApplicationAttachment.Delete() == 1)
                    {
                        strStatus = "File Deleted Success";

                        lblMsgMINCompCondNOCAttachmentOtherDelete.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                        lblMsgMINCompCondNOCAttachmentOtherDelete.ForeColor = System.Drawing.Color.Red;

                        //if (File.Exists(str_fullPath))
                        //{
                        //    File.Delete(str_fullPath);
                        //}
                        BindMiningRenewComplianceConditionNOCOtherAttachmentDetails();
                    }
                    else
                    {
                        strStatus = "File Deleted Failed";
                        lblMsgMINCompCondNOCAttachmentOtherDelete.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                        lblMsgMINCompCondNOCAttachmentOtherDelete.ForeColor = System.Drawing.Color.Red;
                    }
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
    
    #endregion

  

    #region Private
   
    private void clearMessage()
    {
        lblMessageGWFlowDirectionMap.Text = "";
        lblMessageGWLevelObservation.Text = "";
        lblMessageGQofGWInArea.Text = "";
        lblMessageChangesInTopography.Text = "";
        lblMessageChangesInDrainage.Text = "";
        lblMessageExistingNOC.Text = "";
        lblMessageExtra.Text = "";
        lblMessageReasonForNotApplyingBefore.Text = "";
        lblMessageAffidavit.Text = "";
        txtAffidavit.Text = "";
        lblMessageWaterAuditReport.Text = "";
        txtWaterAuditReport.Text = "";
        lblMessageSourceofAvailabilityofSurfaceWater.Text = "";
        txtSourceofAvailabilityofSurfaceWater.Text = "";
        lblMessageWaterQualityMinSeep.Text = "";
        txtWaterQualityMinSeep.Text = "";

    }
    private void BindMiningRenewApplicationAttachmentGroundWaterFlowDirectionDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_MiningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
            arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplication.GetGroundWaterflowDirectionAttachmentList();
            lblGroundwaterFlowDirectionMap2.Visible = true;
            lblGroundwaterFlowDirectionMap.Text = "Groundwater Flow Direction Map (" + arr_MiningRenewApplicationAttachmentList.Length.ToString() + ")";
            gvGWFlowDirectionMap.DataSource = arr_MiningRenewApplicationAttachmentList;
            gvGWFlowDirectionMap.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindMiningRenewApplicationAttachmentGroundWaterObservationWellsDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);

            List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_MiningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
            arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplication.GetMonitorGroundWaterRegimeObservationWellAttachmentList();

            lblGWLevelofObservation2.Visible = true;
            lblGWLevelofObservation.Text = "GW Level of Observation Wells/Piezometer (" + arr_MiningRenewApplicationAttachmentList.Length.ToString() + ")";
            gvGWLevelObservation.DataSource = arr_MiningRenewApplicationAttachmentList;
            gvGWLevelObservation.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindMiningRenewApplicationAttachmentGQualityOfGroundWaterInAreaDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);

            List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_MiningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
            arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplication.GetMonitorGroundWaterRegimeGQsuroudingsAttachmentList();
            lblGeneralQuality2.Visible = true;
            lblGeneralQuality.Text = "General Quality of GW in Area (" + arr_MiningRenewApplicationAttachmentList.Length.ToString() + ")";
            gvGQofGWInArea.DataSource = arr_MiningRenewApplicationAttachmentList;
            gvGQofGWInArea.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindMiningRenewApplicationAttachmentExtraAttachmentsDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);

            List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_MiningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
            arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplication.GetExtraAttachmentList();
            lblExtraAttachment2.Visible = false;
            lblExtraAttachment.Text = "Extra Attachment (" + arr_MiningRenewApplicationAttachmentList.Length.ToString() + ")";
            gvExtra.DataSource = arr_MiningRenewApplicationAttachmentList;
            gvExtra.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindMiningRenewApplicationAttachmentChangesInTopographyDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);

            List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_MiningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
            arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplication.GetChangesInTopographyAttachmentList();
            lblChangesTopography2.Visible = false;
            lblChangesTopography.Text = "Changes in Topography (" + arr_MiningRenewApplicationAttachmentList.Length.ToString() + ")";
            gvChangesInTopography.DataSource = arr_MiningRenewApplicationAttachmentList;
            gvChangesInTopography.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindMiningRenewApplicationAttachmentChangesInDrainageDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);

            List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_MiningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
            arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplication.GetChangesInDrainageAttachmentList();
            lblChangesDrainage2.Visible = false;
            lblChangesDrainage.Text = "Changes in Drainage (" + arr_MiningRenewApplicationAttachmentList.Length.ToString() + ")";
            gvChangesInDrainage.DataSource = arr_MiningRenewApplicationAttachmentList;
            gvChangesInDrainage.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindMiningRenewApplicationAttachmentReasonForNotApplyingBeforeDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Mining.New.Application.MiningNewApplication objMiningNewApplication = obj_MiningRenewApplication.GetFirstMiningApplication();

            if (objMiningNewApplication != null)
            {
                lblHeadingProjName.Text = HttpUtility.HtmlEncode(objMiningNewApplication.NameOfMining);
            }

            List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_MiningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
            arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplication.GetReasonForNotApplyingBeforeAttachmentList();
            lblReasonforNotApplying2.Visible = false;
            lblReasonforNotApplying.Text = "Reason For Not Applying Renewal Before (" + arr_MiningRenewApplicationAttachmentList.Length.ToString() + ")";
            gvReasonForNotApplyingBefore.DataSource = arr_MiningRenewApplicationAttachmentList;
            gvReasonForNotApplyingBefore.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindMiningRenewApplicationAttachmentExisingNOCDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_MiningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
            arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplication.GetExistingNOCAttachmentList();
            lblExistingNOC2.Visible = true;
            lblExistingNOC.Text = "Existing NOC (" + arr_MiningRenewApplicationAttachmentList.Length.ToString() + ")";
            gvExistingNOC.DataSource = arr_MiningRenewApplicationAttachmentList;
            gvExistingNOC.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindMiningRenewApplicationComplianceConditionNOC(long lngA_ApplicationCode)
    {
        try
        {
            int intStatus = 0;
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOCExt obj_MiningRenewSADComplianceConditionNOCExt = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOCExt();

            intStatus = obj_MiningRenewSADComplianceConditionNOCExt.GetComplianceConditionListForApplicationCodeExt(lngA_ApplicationCode, NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOCExt.SortingField.ComplianceConditionNOCDescription);
            if (intStatus == 1)
            {

                gvMINRenewComplianceConditionNOC.DataSource = obj_MiningRenewSADComplianceConditionNOCExt.MiningRenewSADComplianceConditionNOCExtCollectionExt;
                gvMINRenewComplianceConditionNOC.DataBind();

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindMiningRenewComplianceConditionNOCAttachmentDetails(long lng_ApplicationCode)
    {
        try
        {
            foreach (GridViewRow row in gvMINRenewComplianceConditionNOC.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    GridView gvCompCondNOCAtt = (GridView)row.FindControl("gvCompCondNOCAttachment");

                    Label lblCompCondNOCCode = (Label)row.FindControl("lblCompCondNOCCode");

                    //// Inner Grid View Binding

                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOC obj_MiningRenewSADComplianceConditionNOC = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOC(Convert.ToInt32(lblMiningApplicationCodeFrom.Text), Convert.ToInt32(lblCompCondNOCCode.Text));

                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentListForCount;

                    arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplication.GetMiningComplianceConditionNOCAttachmentList(obj_MiningRenewSADComplianceConditionNOC.ComplianceConditionAttachmentCode, NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment.SortingFieldForAttachment.NoSorting);

                    arr_MiningRenewApplicationAttachmentListForCount = obj_MiningRenewApplication.GetMiningComplianceConditionNOCAttachmentList(NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment.SortingFieldForAttachment.NoSorting);
                    lblComplianceReport2.Visible = false;
                    lblComplianceReport.Text = "Compliance Report (" + arr_MiningRenewApplicationAttachmentListForCount.Length.ToString() + ")";
                    gvCompCondNOCAtt.DataSource = arr_MiningRenewApplicationAttachmentList;
                    gvCompCondNOCAtt.DataBind();
                }
            }
        }
        catch (Exception)
        { }
    }

    private void BindMiningRenewApplicationComplianceConditionNOCOther()
    {
        try
        {
            MiningRenewSADComplianceConditionNOCOther obj_MiningRenewSADComplianceConditionNOCOther = new MiningRenewSADComplianceConditionNOCOther();
            int int_status = 0;
            if (lblMiningApplicationCodeFrom.Text != "" && NOCAPExternalUtility.IsNumeric(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)))
            {
                obj_MiningRenewSADComplianceConditionNOCOther.MiningRenewApplicationCode = Convert.ToInt64(lblMiningApplicationCodeFrom.Text);

                int_status = obj_MiningRenewSADComplianceConditionNOCOther.GetList(MiningRenewSADComplianceConditionNOCOther.SortingField.NoSorting);

                MiningRenewSADComplianceConditionNOCOther[] arr_MiningRenewSADComplianceConditionNOCOther;
                arr_MiningRenewSADComplianceConditionNOCOther = obj_MiningRenewSADComplianceConditionNOCOther.MiningRenewSADComplianceConditionNOCOtherCollection;

                if ((int_status == 1))
                {
                    gvMINRenewComplianceConditionNOCOther.DataSource = arr_MiningRenewSADComplianceConditionNOCOther;
                    gvMINRenewComplianceConditionNOCOther.DataBind();
                }
                else
                {
                    lblMessage.Text = HttpUtility.HtmlEncode(obj_MiningRenewSADComplianceConditionNOCOther.CustumMessage);
                }
            }
            else
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindMiningRenewComplianceConditionNOCOtherAttachmentDetails()
    {
        try
        {
            foreach (GridViewRow row in gvMINRenewComplianceConditionNOCOther.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    GridView gvCompCondNOCAttOther = (GridView)row.FindControl("gvCompCondNOCAttachmentOther");

                    Label lblCompCondNOCOtherSerialNumber = (Label)row.FindControl("lblCompCondNOCOtherSerialNumber");

                    //// Inner Grid View Binding

                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOCOther obj_MiningRenewSADComplianceConditionNOCOther = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOCOther(Convert.ToInt32(lblMiningApplicationCodeFrom.Text), Convert.ToInt32(lblCompCondNOCOtherSerialNumber.Text));

                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentListForCount;

                    arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplication.GetMiningComplianceConditionNOCOtherAttachmentList(obj_MiningRenewSADComplianceConditionNOCOther.ComplianceConditionAttachmentCode, NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment.SortingFieldForAttachment.NoSorting);

                    arr_MiningRenewApplicationAttachmentListForCount = obj_MiningRenewApplication.GetMiningComplianceConditionNOCOtherAttachmentList(NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment.SortingFieldForAttachment.NoSorting);
                    lblComplianceReportOther2.Visible = false;
                    lblComplianceReportOther.Text = "Compliance Report - Other (" + arr_MiningRenewApplicationAttachmentListForCount.Length.ToString() + ")";
                    gvCompCondNOCAttOther.DataSource = arr_MiningRenewApplicationAttachmentList;
                    gvCompCondNOCAttOther.DataBind();
                }
            }
        }
        catch (Exception)
        { }
    }
    private void BindAffidavitAttachmentDetails(long lngA_ApplicationCode)
    {
        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int Count = 0;
            BindGridView(gvAffidavit, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref Count);
            if (decTotalGroundWaterRequirement < GroundWaterRequirement100KLD)
                lblAffidavit2.Visible = true;
            else
                lblAffidavit2.Visible = false;

            lblAffidavit.Text = HttpUtility.HtmlEncode("Affidavit of Compliance of NOC Condition (" + Count.ToString() + ")");
            hdnAffidavit.Value = Count.ToString();
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void BindWaterAuditReportAttachmentDetails(long lngA_ApplicationCode)
    {
        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int Count = 0;
            BindGridView(gvWaterAuditReport, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref Count);
            if (decTotalGroundWaterRequirement > GroundWaterRequirement100KLD)
                lblWaterAuditReport2.Visible = true;
            else
                lblWaterAuditReport2.Visible = false;

            lblWaterAuditReport.Text = HttpUtility.HtmlEncode("Water Audit Report (" + Count.ToString() + ")");
            hdnWaterAuditReport.Value = Count.ToString();
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindSourceWaterNonAvailabilityCertificateAttachmentDetails(long lngA_ApplicationCode)
    {
        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int Count = 0;
            BindGridView(gvSourceofAvailabilityofSurfaceWater, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref Count);
            //if (decTotalGroundWaterRequirement > GroundWaterRequirement100KLD)
            //    lblSourceWaterAvailability2.Visible = true;
            //else
            //    lblSourceWaterAvailability2.Visible = true;
            lblSourceWaterAvailability2.Visible = true;
            lblSourceWaterAvailability.Text = HttpUtility.HtmlEncode("Source Water Non-availability Certificate (" + Count.ToString() + ")");
            hdnSourceWaterAvailability.Value = Count.ToString();
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindWaterQualityMinSeepAttachmentDetails(long lngA_ApplicationCode)
    {
        try
        {
            decimal decTotalGroundWaterRequirement = 0;
            int Count = 0;
            BindGridView(gvWaterQualityMinSeep, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref Count);

            lblWaterQualityMinSeep2.Visible = true;
            lblWaterQualityMinSeep.Text = HttpUtility.HtmlEncode("Water Quality Report Of Mine Seepage/Discharge (" + Count.ToString() + ")");
            hdnWaterQualityMinSeep.Value = Count.ToString();
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }


    private void BindGridView(GridView gv, long lngA_ApplicationCode, ref decimal decTotalGroundWaterRequirement, ref int AttCount)
    {
        try {
            MiningRenewSADApplication obj_miningRenewApplication = new MiningRenewSADApplication(lngA_ApplicationCode);
            MiningRenewSADApplicationAttachment[] arr_miningRenewApplicationAttachmentList = null;

            if (gv.ID == "gvAffidavit")
                arr_miningRenewApplicationAttachmentList = obj_miningRenewApplication.GetAffidavitNOCCondiAttachmentList();
            else if (gv.ID == "gvWaterAuditReport")
                arr_miningRenewApplicationAttachmentList = obj_miningRenewApplication.GetWaterAuditReportAttachmentList();
            else if (gv.ID == "gvSourceofAvailabilityofSurfaceWater")
                arr_miningRenewApplicationAttachmentList = obj_miningRenewApplication.GetCertiNonAvaAttachmentList();
            else if (gv.ID == "gvWaterQualityMinSeep")
                arr_miningRenewApplicationAttachmentList = obj_miningRenewApplication.GetWaterQualityMinSeepAttachmentList();


            gv.DataSource = arr_miningRenewApplicationAttachmentList;
            gv.DataBind();
            AttCount = arr_miningRenewApplicationAttachmentList.Length;
            decTotalGroundWaterRequirement = Convert.ToDecimal(obj_miningRenewApplication.GWREquiredThroughAbstractStructureExisting + obj_miningRenewApplication.GWREquiredThroughAbstractStructureAdditional + obj_miningRenewApplication.GWRequiredThroughMiningSeepingExisting + obj_miningRenewApplication.GWRequiredThroughMiningSeepingAdditional);
        }
        catch(Exception ex)
        {

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
    
    private void BindMiningRenewApplicationAttachmentBharatKoshRecieptDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_miningRenewApplicationAttachmentList;
            arr_miningRenewApplicationAttachmentList = obj_miningRenewApplication.GetBharatKoshRecieptAttachmentList();
            gvBharatKoshReciept.DataSource = arr_miningRenewApplicationAttachmentList;
            gvBharatKoshReciept.DataBind();
         
            lblBharatKosh.Text = HttpUtility.HtmlEncode("Bharat Kosh Reciept Attachment (" + arr_miningRenewApplicationAttachmentList.Length.ToString() + ")");
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindMiningRenewApplicationAttachmentSignedDocDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_miningRenewApplicationAttachmentList;
            arr_miningRenewApplicationAttachmentList = obj_miningRenewApplication.GetSignedDocAttachmentList();
            gvApplicationSignatureSeal.DataSource = arr_miningRenewApplicationAttachmentList;
            gvApplicationSignatureSeal.DataBind();
            lblSignDoc2.Visible = true;
            lblSignDoc.Text = HttpUtility.HtmlEncode("Aplication with Signature and Seal Attachment (" + arr_miningRenewApplicationAttachmentList.Length.ToString() + ")");
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    #endregion

    #region ViewFile
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
                    long lng_miningRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINRenewSADAppDownloadFiles(lng_miningRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

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
    protected void lbtnChangesInDrainage_Click(object sender, CommandEventArgs e)
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
                    long lng_MiningRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.MINRenewSADAppDownloadFiles(lng_MiningRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }

    protected void lbtnViewFile_Click(object sender, CommandEventArgs e)
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
                    long lng_MiningRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.MINRenewSADAppDownloadFiles(lng_MiningRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }
    protected void lbtnFlowDirectionViewFile_Click(object sender, CommandEventArgs e)
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
                    long lng_MiningRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.MINRenewSADAppDownloadFiles(lng_MiningRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }
    protected void lbtnObservationWellsViewFile_Click(object sender, CommandEventArgs e)
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
                    long lng_MiningRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.MINRenewSADAppDownloadFiles(lng_MiningRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }
    protected void lbtnGeneralQualityViewFile_Click(object sender, CommandEventArgs e)
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
                    long lng_MiningRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.MINRenewSADAppDownloadFiles(lng_MiningRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }
    protected void lbtnExtraAttViewFile_Click(object sender, CommandEventArgs e)
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
                    long lng_MiningRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.MINRenewSADAppDownloadFiles(lng_MiningRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;

            }
        }
    }
    protected void lbtnViewReasonForNotApplyingBeforeFile_Click(object sender, CommandEventArgs e)
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
                    long lng_MiningRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.MINRenewSADAppDownloadFiles(lng_MiningRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;
            }
        }
    }
    protected void lbtnViewExistingNOCFile_Click(object sender, CommandEventArgs e)
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
                    long lng_MiningRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.MINRenewSADAppDownloadFiles(lng_MiningRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;
            }
        }
    }
    protected void lnkMiningCompCondNOCAttachmentView_Click(object sender, CommandEventArgs e)
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
                    long lng_MiningRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.MINRenewSADAppDownloadFiles(lng_MiningRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;
            }
        }
    }
    protected void lnkMiningCompCondNOCAttachmentViewOther_Click(object sender, CommandEventArgs e)
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
                    long lng_MiningRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.MINRenewSADAppDownloadFiles(lng_MiningRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                Session["CSRF"] = hidCSRF.Value;
            }
        }
    }

    protected void lbtnAplicationSignatureSealViewFile_Click(object sender, CommandEventArgs e)
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
                    long lng_miningRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINRenewSADAppDownloadFiles(lng_miningRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

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
                Session["CSRF"] = hidCSRF.Value;
            }
        }
    }
    protected void lbtnBharatKoshRecieptViewFile_Click(object sender, CommandEventArgs e)
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
                    long lng_miningRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.MINRenewSADAppDownloadFiles(lng_miningRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

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
                Session["CSRF"] = hidCSRF.Value;
            }
        }
    }
    #endregion
    private int DeleteAttchment(GridView gv, GridViewDeleteEventArgs e, Label lblMessage, string str_ActionName)
    {
        try
        {
            long lng_MiningRenewApplicationCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["MiningRenewApplicationCode"]);
            int int_AttachmentCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["AttachmentCode"]);
            int int_AttachmentCodeSerialNumber = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
            lblMessage.ForeColor = System.Drawing.Color.Red;
            MiningRenewSADApplicationAttachment obj_MiningRenewApplicationAttachment = new MiningRenewSADApplicationAttachment(lng_MiningRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
            if (obj_MiningRenewApplicationAttachment.Delete() == 1)
            {
                lblMessage.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                strStatus = "File Delete Success";
                return 1;
            }
            else
            {
                lblMessage.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
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



    #region RowDeleting
    protected void gvAffidavit_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Affidavit";
                if (DeleteAttchment((GridView)sender, e, lblMessageAffidavit, strActionName) == 1)
                {
                    BindAffidavitAttachmentDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }
    protected void gvWaterAuditReport_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Water Audit Report";
                if (DeleteAttchment((GridView)sender, e, lblMessageWaterAuditReport, strActionName) == 1)
                {
                    BindWaterAuditReportAttachmentDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }
    protected void gvSourceofAvailabilityofSurfaceWater_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Source of Availability of Surface Water";
                if (DeleteAttchment((GridView)sender, e, lblMessageSourceofAvailabilityofSurfaceWater, strActionName) == 1)
                {
                    BindSourceWaterNonAvailabilityCertificateAttachmentDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }
    protected void gvWaterQualityMinSeep_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Water Quality Min Seep";
                if (DeleteAttchment((GridView)sender, e, lblMessageWaterQualityMinSeep, strActionName) == 1)
                {
                    BindWaterQualityMinSeepAttachmentDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }


    protected void gvGWFlowDirectionMap_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "DeleteGWFlowDirectionMap";
                long lng_MiningRenewApplicationCode = Convert.ToInt32(gvGWFlowDirectionMap.DataKeys[e.RowIndex].Values["MiningRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvGWFlowDirectionMap.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvGWFlowDirectionMap.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_MiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment(lng_MiningRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningRenewApplicationAttachment.AttachmentPath;

                if (obj_MiningRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    lblMessageGWFlowDirectionMap.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                    lblMessageGWFlowDirectionMap.ForeColor = System.Drawing.Color.Red;

                    BindMiningRenewApplicationAttachmentGroundWaterFlowDirectionDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageGWFlowDirectionMap.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                    lblMessageGWFlowDirectionMap.ForeColor = System.Drawing.Color.Red;

                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void gvGWLevelObservation_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "DeleteGWLevelObservation";
                long lng_MiningRenewApplicationCode = Convert.ToInt32(gvGWLevelObservation.DataKeys[e.RowIndex].Values["MiningRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvGWLevelObservation.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvGWLevelObservation.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_MiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment(lng_MiningRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningRenewApplicationAttachment.AttachmentPath;

                if (obj_MiningRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    lblMessageGWLevelObservation.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                    lblMessageGWLevelObservation.ForeColor = System.Drawing.Color.Red;

                    BindMiningRenewApplicationAttachmentGroundWaterObservationWellsDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageGWLevelObservation.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                    lblMessageGWLevelObservation.ForeColor = System.Drawing.Color.Red;

                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void gvGQofGWInArea_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "DeleteGQofGWInArea";
                long lng_MiningRenewApplicationCode = Convert.ToInt32(gvGQofGWInArea.DataKeys[e.RowIndex].Values["MiningRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvGQofGWInArea.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvGQofGWInArea.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_MiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment(lng_MiningRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningRenewApplicationAttachment.AttachmentPath;

                if (obj_MiningRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    lblMessageGQofGWInArea.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                    lblMessageGQofGWInArea.ForeColor = System.Drawing.Color.Red;

                    BindMiningRenewApplicationAttachmentGQualityOfGroundWaterInAreaDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageGQofGWInArea.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                    lblMessageGQofGWInArea.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void gvExtra_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "DeleteExtraAttachment";
                long lng_MiningRenewApplicationCode = Convert.ToInt32(gvExtra.DataKeys[e.RowIndex].Values["MiningRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvExtra.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvExtra.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_MiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment(lng_MiningRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningRenewApplicationAttachment.AttachmentPath;

                if (obj_MiningRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    lblMessageExtra.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                    lblMessageExtra.ForeColor = System.Drawing.Color.Red;

                    BindMiningRenewApplicationAttachmentExtraAttachmentsDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageExtra.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                    lblMessageExtra.ForeColor = System.Drawing.Color.Red;

                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void gvChangesInTopography_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "DeleteChangeInTopographyAttachment";
                long lng_MiningRenewApplicationCode = Convert.ToInt32(gvChangesInTopography.DataKeys[e.RowIndex].Values["MiningRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvChangesInTopography.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvChangesInTopography.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_MiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment(lng_MiningRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningRenewApplicationAttachment.AttachmentPath;

                if (obj_MiningRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    lblMessageChangesInTopography.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                    lblMessageChangesInTopography.ForeColor = System.Drawing.Color.Red;

                    BindMiningRenewApplicationAttachmentChangesInTopographyDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageChangesInTopography.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                    lblMessageChangesInTopography.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void gvChangesInDrainage_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "DeleteChangeInDrainageAttachment";
                long lng_MiningRenewApplicationCode = Convert.ToInt32(gvChangesInDrainage.DataKeys[e.RowIndex].Values["MiningRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvChangesInDrainage.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvChangesInDrainage.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_MiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment(lng_MiningRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                string str_fullPath = str_startPathFromConfig + obj_MiningRenewApplicationAttachment.AttachmentPath;

                if (obj_MiningRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Successfully !";
                    lblMessageChangesInDrainage.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                    lblMessageChangesInDrainage.ForeColor = System.Drawing.Color.Red;

                    BindMiningRenewApplicationAttachmentChangesInDrainageDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed !";
                    lblMessageChangesInDrainage.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                    lblMessageChangesInDrainage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                strStatus = "File Delete Failed !";
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
    protected void gvReasonForNotApplyingBefore_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Reason For Not Applying Renewal Before";
                long lng_MiningRenewApplicationCode = Convert.ToInt32(gvReasonForNotApplyingBefore.DataKeys[e.RowIndex].Values["MiningRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvReasonForNotApplyingBefore.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvReasonForNotApplyingBefore.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_MiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment(lng_MiningRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                //string str_fullPath = str_startPathFromConfig + obj_MiningRenewApplicationAttachment.AttachmentPath;

                if (obj_MiningRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Deleted Success";
                    lblMessageReasonForNotApplyingBefore.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                    lblMessageReasonForNotApplyingBefore.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    BindMiningRenewApplicationAttachmentReasonForNotApplyingBeforeDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Deleting Failed";
                    lblMessageReasonForNotApplyingBefore.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                    lblMessageReasonForNotApplyingBefore.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
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
    protected void gvExistingNOC_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Existing NOC";
                long lng_MiningRenewApplicationCode = Convert.ToInt32(gvExistingNOC.DataKeys[e.RowIndex].Values["MiningRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvExistingNOC.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvExistingNOC.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_MiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment(lng_MiningRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                //string str_fullPath = str_startPathFromConfig + obj_MiningRenewApplicationAttachment.AttachmentPath;

                if (obj_MiningRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Deleted Success";
                    lblMessageExistingNOC.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                    lblMessageExistingNOC.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    BindMiningRenewApplicationAttachmentExisingNOCDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Deleting Failed";
                    lblMessageExistingNOC.Text = obj_MiningRenewApplicationAttachment.CustumMessage;
                    lblMessageExistingNOC.ForeColor = System.Drawing.Color.Red;
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

    protected void gvAplicationSignatureSeal_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Aplication with Signature and Seal";
                long lng_MiningRenewApplicationCode = Convert.ToInt32(gvApplicationSignatureSeal.DataKeys[e.RowIndex].Values["MiningRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvApplicationSignatureSeal.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvApplicationSignatureSeal.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_miningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment(lng_MiningRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                if (obj_miningRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Success";
                    lblMessageAplicationSignatureSeal.Text = obj_miningRenewApplicationAttachment.CustumMessage;
                    lblMessageAplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;
                    BindMiningRenewApplicationAttachmentSignedDocDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed";
                    lblMessageAplicationSignatureSeal.Text = obj_miningRenewApplicationAttachment.CustumMessage;
                    lblMessageAplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;

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
    protected void BharatKoshReciept_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Bharat Kosh Reciept";
                long lng_ApplicationCode = Convert.ToInt32(gvBharatKoshReciept.DataKeys[e.RowIndex].Values["MiningRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvBharatKoshReciept.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvBharatKoshReciept.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_miningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment(lng_ApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                if (obj_miningRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Success";
                    lblMessageBharatKoshReciept.Text = obj_miningRenewApplicationAttachment.CustumMessage;
                    lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
                    BindMiningRenewApplicationAttachmentBharatKoshRecieptDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed";
                    lblMessageBharatKoshReciept.Text = obj_miningRenewApplicationAttachment.CustumMessage;
                    lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;

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
    #endregion

    #region Upload
    
    protected void btnUploadAffidavit_Click(object sender, EventArgs e)
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
                strActionName = "File Upload Affidavit";
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewApplicationForNoLimit = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_miningRenewApplicationAttachmentList;
                arr_miningRenewApplicationAttachmentList = obj_miningRenewApplicationForNoLimit.GetAffidavitNOCCondiAttachmentList();

                if (arr_miningRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_insertMiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment();
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_miningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadAffidavit.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadAffidavit.PostedFile.FileName).ToLower();
                            str_fname = FileUploadAffidavit.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadAffidavit.PostedFile))
                                {
                                    if (FileUploadAffidavit.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadAffidavit.PostedFile);
                                        obj_insertMiningRenewApplicationAttachment.ContentType = FileUploadAffidavit.PostedFile.ContentType;
                                        obj_insertMiningRenewApplicationAttachment.AttachmentPath = FileUploadAffidavit.FileName;
                                        obj_insertMiningRenewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageAffidavit.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageAffidavit.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageAffidavit.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageAffidavit.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageAffidavit.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageAffidavit.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageAffidavit.Text = "Please select a file..!!";
                            lblMessageAffidavit.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertMiningRenewApplicationAttachment.MiningRenewApplicationCode = obj_miningRenewApplication.MiningRenewApplicationCode;
                        obj_insertMiningRenewApplicationAttachment.AttachmentCode = obj_miningRenewApplication.AffidavitNOCCondiAttCode;
                        obj_insertMiningRenewApplicationAttachment.AttachmentName = txtAffidavit.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertMiningRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertMiningRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageAffidavit.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;
                            lblMessageAffidavit.ForeColor = System.Drawing.Color.Red;

                        }


                        BindAffidavitAttachmentDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageAffidavit.ForeColor = Color.Green;
                        lblMessageAffidavit.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;

                    }
                    catch (Exception ex)
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
                    lblMessageAffidavit.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageAffidavit.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUploadWaterAuditReport_Click(object sender, EventArgs e)
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
                strActionName = "File Upload Water Audit Report";
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewApplicationForNoLimit = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_miningRenewApplicationAttachmentList;
                arr_miningRenewApplicationAttachmentList = obj_miningRenewApplicationForNoLimit.GetWaterAuditReportAttachmentList();

                if (arr_miningRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_insertMiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment();
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_miningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadWaterAuditReport.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadWaterAuditReport.PostedFile.FileName).ToLower();
                            str_fname = FileUploadWaterAuditReport.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadWaterAuditReport.PostedFile))
                                {
                                    if (FileUploadWaterAuditReport.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadWaterAuditReport.PostedFile);
                                        obj_insertMiningRenewApplicationAttachment.ContentType = FileUploadWaterAuditReport.PostedFile.ContentType;
                                        obj_insertMiningRenewApplicationAttachment.AttachmentPath = FileUploadWaterAuditReport.FileName;
                                        obj_insertMiningRenewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageWaterAuditReport.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageWaterAuditReport.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageWaterAuditReport.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageWaterAuditReport.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageWaterAuditReport.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageWaterAuditReport.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageWaterAuditReport.Text = "Please select a file..!!";
                            lblMessageWaterAuditReport.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertMiningRenewApplicationAttachment.MiningRenewApplicationCode = obj_miningRenewApplication.MiningRenewApplicationCode;
                        obj_insertMiningRenewApplicationAttachment.AttachmentCode = obj_miningRenewApplication.WaterAuditReportAttCode;
                        obj_insertMiningRenewApplicationAttachment.AttachmentName = txtWaterAuditReport.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertMiningRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertMiningRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageWaterAuditReport.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;
                            lblMessageWaterAuditReport.ForeColor = System.Drawing.Color.Red;

                        }


                        BindWaterAuditReportAttachmentDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageWaterAuditReport.ForeColor = Color.Green;
                        lblMessageWaterAuditReport.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;

                    }
                    catch (Exception ex)
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
                    lblMessageAffidavit.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageAffidavit.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void lbtnUplodSourceofAvailabilityofSurfaceWater_Click(object sender, EventArgs e)
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
                strActionName = "File Upload Source Water Non-availability Certificate";
                MiningRenewSADApplication obj_miningRenewApplicationForNoLimit = new MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                MiningRenewSADApplicationAttachment[] arr_miningRenewApplicationAttachmentList;
                arr_miningRenewApplicationAttachmentList = obj_miningRenewApplicationForNoLimit.GetCertiNonAvaAttachmentList();

                if (arr_miningRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    MiningRenewSADApplicationAttachment obj_insertMiningRenewApplicationAttachment = new MiningRenewSADApplicationAttachment();
                    MiningRenewSADApplication obj_miningRenewApplication = new MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<MiningRenewSADApplicationAttachment> lst_miningRenewApplicationAttachmentList = new List<MiningRenewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (txtFileUploadSourceofAvailabilityofSurfaceWater.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(txtFileUploadSourceofAvailabilityofSurfaceWater.PostedFile.FileName).ToLower();
                            str_fname = txtFileUploadSourceofAvailabilityofSurfaceWater.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(txtFileUploadSourceofAvailabilityofSurfaceWater.PostedFile))
                                {
                                    if (txtFileUploadSourceofAvailabilityofSurfaceWater.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(txtFileUploadSourceofAvailabilityofSurfaceWater.PostedFile);
                                        obj_insertMiningRenewApplicationAttachment.ContentType = txtFileUploadSourceofAvailabilityofSurfaceWater.PostedFile.ContentType;
                                        obj_insertMiningRenewApplicationAttachment.AttachmentPath = txtFileUploadSourceofAvailabilityofSurfaceWater.FileName;
                                        obj_insertMiningRenewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageSourceofAvailabilityofSurfaceWater.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageSourceofAvailabilityofSurfaceWater.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageSourceofAvailabilityofSurfaceWater.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageSourceofAvailabilityofSurfaceWater.Text = "Please select a file..!!";
                            lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertMiningRenewApplicationAttachment.MiningRenewApplicationCode = obj_miningRenewApplication.MiningRenewApplicationCode;
                        obj_insertMiningRenewApplicationAttachment.AttachmentCode = obj_miningRenewApplication.CertiNonAvaAttCode;
                        obj_insertMiningRenewApplicationAttachment.AttachmentName = txtSourceofAvailabilityofSurfaceWater.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertMiningRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertMiningRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageSourceofAvailabilityofSurfaceWater.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;
                            lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;

                        }


                        BindSourceWaterNonAvailabilityCertificateAttachmentDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                        clearMessage();
                        lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = Color.Green;
                        lblMessageSourceofAvailabilityofSurfaceWater.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;

                    }
                    catch (Exception ex)
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
                    lblMessageSourceofAvailabilityofSurfaceWater.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void btnUplodWaterQualityMinSeep_Click(object sender, EventArgs e)
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
                strActionName = "File Upload Water Audit Report";
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewApplicationForNoLimit = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_miningRenewApplicationAttachmentList;
                arr_miningRenewApplicationAttachmentList = obj_miningRenewApplicationForNoLimit.GetWaterQualityMinSeepAttachmentList();

                if (arr_miningRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_insertMiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment();
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_miningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadWaterQualityMinSeep.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadWaterQualityMinSeep.PostedFile.FileName).ToLower();
                            str_fname = FileUploadWaterAuditReport.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadWaterQualityMinSeep.PostedFile))
                                {
                                    if (FileUploadWaterQualityMinSeep.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadWaterQualityMinSeep.PostedFile);
                                        obj_insertMiningRenewApplicationAttachment.ContentType = FileUploadWaterQualityMinSeep.PostedFile.ContentType;
                                        obj_insertMiningRenewApplicationAttachment.AttachmentPath = FileUploadWaterQualityMinSeep.FileName;
                                        obj_insertMiningRenewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageWaterQualityMinSeep.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageWaterQualityMinSeep.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageWaterQualityMinSeep.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageWaterQualityMinSeep.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageWaterQualityMinSeep.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageWaterQualityMinSeep.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageWaterQualityMinSeep.Text = "Please select a file..!!";
                            lblMessageWaterQualityMinSeep.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertMiningRenewApplicationAttachment.MiningRenewApplicationCode = obj_miningRenewApplication.MiningRenewApplicationCode;
                        obj_insertMiningRenewApplicationAttachment.AttachmentCode = obj_miningRenewApplication.WaterQualityMinSeepAttCode;
                        obj_insertMiningRenewApplicationAttachment.AttachmentName = txtWaterQualityMinSeep.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertMiningRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertMiningRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageWaterQualityMinSeep.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;
                            lblMessageWaterQualityMinSeep.ForeColor = System.Drawing.Color.Red;

                        }


                        BindWaterQualityMinSeepAttachmentDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageWaterQualityMinSeep.ForeColor = Color.Green;
                        lblMessageWaterQualityMinSeep.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;

                    }
                    catch (Exception ex)
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
                    lblMessageAffidavit.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageAffidavit.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }


    protected void btnUplodExistingNOCFile_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
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
                    strActionName = "File Upload Exising NOC";
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplicationForNoLimit = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
                    arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplicationForNoLimit.GetExistingNOCAttachmentList();

                    if (arr_MiningRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                    {
                        string str_fname;
                        string str_ext;
                        string str_newFileNameWithPath = "";

                        string str_restPath = "";

                        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_insertMiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment();
                        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                        List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_MiningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();
                        byte[] buffer = new byte[1];
                        try
                        {
                            if (txtFileUploadExistingNOC.HasFile)
                            {
                                str_ext = System.IO.Path.GetExtension(txtFileUploadExistingNOC.PostedFile.FileName).ToLower();
                                str_fname = txtFileUploadExistingNOC.FileName;

                                if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                                {

                                    if (NOCAPExternalUtility.IsValidFile(txtFileUploadExistingNOC.PostedFile))
                                    {
                                        if (txtFileUploadExistingNOC.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                        {
                                            obj_insertMiningRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(txtFileUploadExistingNOC.PostedFile);
                                            obj_insertMiningRenewApplicationAttachment.ContentType = txtFileUploadExistingNOC.PostedFile.ContentType;
                                            obj_insertMiningRenewApplicationAttachment.AttachmentPath = txtFileUploadExistingNOC.FileName;
                                            obj_insertMiningRenewApplicationAttachment.FileExtension = str_ext;
                                        }
                                        else
                                        {
                                            strStatus = "File Upload Failed";
                                            lblMessageExistingNOC.Text = "File can not upload. It has more than 5 MB size";
                                            lblMessageExistingNOC.ForeColor = System.Drawing.Color.Red;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageExistingNOC.Text = "Not a valid file!!..Select an other file!!";
                                        lblMessageExistingNOC.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageExistingNOC.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageExistingNOC.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }

                            }
                            else
                            {
                                lblMessageExistingNOC.Text = "Please select a file..!!";
                                lblMessageExistingNOC.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                            obj_insertMiningRenewApplicationAttachment.MiningRenewApplicationCode = obj_MiningRenewApplication.MiningRenewApplicationCode;
                            obj_insertMiningRenewApplicationAttachment.AttachmentCode = obj_MiningRenewApplication.MiningRenewExistingNOC.ExistNOCAttachCode;
                            obj_insertMiningRenewApplicationAttachment.AttachmentName = txtExistingNOC.Text;

                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                            obj_insertMiningRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                            if (obj_insertMiningRenewApplicationAttachment.Add() == 1)
                            {
                                strStatus = "File Upload Success";
                                obj_insertMiningRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;

                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageExistingNOC.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;
                                lblMessageExistingNOC.ForeColor = System.Drawing.Color.Red;

                            }

                            lblMessageExistingNOC.ForeColor = Color.Green;
                            lblMessageExistingNOC.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;
                            txtExistingNOC.Text = "";
                            BindMiningRenewApplicationAttachmentExisingNOCDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));


                            lblMessageGWFlowDirectionMap.Text = "";
                            lblMessageGWLevelObservation.Text = "";
                            lblMessageGQofGWInArea.Text = "";
                            lblMessageChangesInTopography.Text = "";
                            lblMessageChangesInDrainage.Text = "";
                            //lblMessageExistingNOC.Text = "";
                            lblMessageExtra.Text = "";
                            lblMessageReasonForNotApplyingBefore.Text = "";
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
                        lblMessageExistingNOC.Text = "Maximum number of files to be uploaded is 5";
                        lblMessageExistingNOC.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
    }


    protected void btnUplodReasonForNotApplyingBefore_Click(object sender, EventArgs e)
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
                strActionName = "File Upload Reason For Not Applying Before";
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplicationForNoLimit = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
                arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplicationForNoLimit.GetReasonForNotApplyingBeforeAttachmentList();

                if (arr_MiningRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    string str_restPath = "";

                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_insertMiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment();
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_MiningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (txtFileUploadReasonForNotApplyingBefore.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(txtFileUploadReasonForNotApplyingBefore.PostedFile.FileName).ToLower();
                            str_fname = txtFileUploadReasonForNotApplyingBefore.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(txtFileUploadReasonForNotApplyingBefore.PostedFile))
                                {
                                    if (txtFileUploadReasonForNotApplyingBefore.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(txtFileUploadReasonForNotApplyingBefore.PostedFile);
                                        obj_insertMiningRenewApplicationAttachment.ContentType = txtFileUploadReasonForNotApplyingBefore.PostedFile.ContentType;
                                        obj_insertMiningRenewApplicationAttachment.AttachmentPath = txtFileUploadReasonForNotApplyingBefore.FileName;
                                        obj_insertMiningRenewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Deleting Failed";
                                        lblMessageReasonForNotApplyingBefore.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageReasonForNotApplyingBefore.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Deleting Failed";
                                    lblMessageReasonForNotApplyingBefore.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageReasonForNotApplyingBefore.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Deleting Failed";
                                lblMessageReasonForNotApplyingBefore.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageReasonForNotApplyingBefore.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageReasonForNotApplyingBefore.Text = "Please select a file..!!";
                            lblMessageReasonForNotApplyingBefore.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningRenewApplicationAttachment.MiningRenewApplicationCode = obj_MiningRenewApplication.MiningRenewApplicationCode;
                        obj_insertMiningRenewApplicationAttachment.AttachmentCode = obj_MiningRenewApplication.MiningRenewExistingNOC.ExistingNOCReasonForNotApplyBeforeExpiryAttachCode;
                        obj_insertMiningRenewApplicationAttachment.AttachmentName = txtReasonForNotApplyingBefore.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Deleted Successfully";
                            obj_insertMiningRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Deletion Failed";
                            lblMessageReasonForNotApplyingBefore.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;
                            lblMessageReasonForNotApplyingBefore.ForeColor = System.Drawing.Color.Red;
                        }
                        lblMessageReasonForNotApplyingBefore.ForeColor = Color.Green;
                        lblMessageReasonForNotApplyingBefore.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;
                        txtReasonForNotApplyingBefore.Text = "";
                        BindMiningRenewApplicationAttachmentReasonForNotApplyingBeforeDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                        lblMessageGWFlowDirectionMap.Text = "";
                        lblMessageGWLevelObservation.Text = "";
                        lblMessageGQofGWInArea.Text = "";
                        lblMessageChangesInTopography.Text = "";
                        lblMessageChangesInDrainage.Text = "";
                        lblMessageExistingNOC.Text = "";
                        lblMessageExtra.Text = "";
                        //lblMessageReasonForNotApplyingBefore.Text = "";


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
                    lblMessageReasonForNotApplyingBefore.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageReasonForNotApplyingBefore.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void btnUplodGWFlowDirectionMap_Click(object sender, EventArgs e)
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
                strActionName = "UploadGWFlowDirectionMap";
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplicationForNoLimit = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
                arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplicationForNoLimit.GetGroundWaterflowDirectionAttachmentList();
                if (arr_MiningRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_insertMiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment();
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_MiningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();

                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadGWFlowDirectionMap.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadGWFlowDirectionMap.PostedFile.FileName).ToLower();
                            str_fname = FileUploadGWFlowDirectionMap.FileName;

                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadGWFlowDirectionMap.PostedFile))
                                {
                                    if (FileUploadGWFlowDirectionMap.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadGWFlowDirectionMap.PostedFile);
                                        obj_insertMiningRenewApplicationAttachment.ContentType = FileUploadGWFlowDirectionMap.PostedFile.ContentType;
                                        obj_insertMiningRenewApplicationAttachment.AttachmentPath = FileUploadGWFlowDirectionMap.FileName;
                                        obj_insertMiningRenewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageGWFlowDirectionMap.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageGWFlowDirectionMap.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageGWFlowDirectionMap.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageGWFlowDirectionMap.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }

                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageGWFlowDirectionMap.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageGWFlowDirectionMap.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageGWFlowDirectionMap.Text = "Please select a file..!!";
                            lblMessageGWFlowDirectionMap.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningRenewApplicationAttachment.MiningRenewApplicationCode = obj_MiningRenewApplication.MiningRenewApplicationCode;
                        obj_insertMiningRenewApplicationAttachment.AttachmentCode = obj_MiningRenewApplication.MiningRenewSADDewatering.GWFlowAttachCode;

                        obj_insertMiningRenewApplicationAttachment.AttachmentName = txtGWFlowDirectionMap.Text;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageGWFlowDirectionMap.Text = "File Upload Success";
                            lblMessageGWFlowDirectionMap.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageGWFlowDirectionMap.Text = "File Upload Failed";
                            lblMessageGWFlowDirectionMap.ForeColor = System.Drawing.Color.Red;
                        }
                        lblMessageGWFlowDirectionMap.ForeColor = Color.Green;
                        lblMessageGWFlowDirectionMap.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;
                        txtGWFlowDirectionMap.Text = "";
                        BindMiningRenewApplicationAttachmentGroundWaterFlowDirectionDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                        //lblMessageGWFlowDirectionMap.Text = "";
                        lblMessageGWLevelObservation.Text = "";
                        lblMessageGQofGWInArea.Text = "";
                        lblMessageChangesInTopography.Text = "";
                        lblMessageChangesInDrainage.Text = "";
                        lblMessageExistingNOC.Text = "";
                        lblMessageExtra.Text = "";
                        lblMessageReasonForNotApplyingBefore.Text = "";
                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";
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
                    lblMessageGWFlowDirectionMap.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageGWFlowDirectionMap.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUplodGWLevelObservation_Click(object sender, EventArgs e)
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
                strActionName = "UploadGWLevelObservation";
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplicationForNoLimit = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
                arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplicationForNoLimit.GetMonitorGroundWaterRegimeObservationWellAttachmentList();
                if (arr_MiningRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_insertMiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment();
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_MiningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();

                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadGWLevelObservation.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadGWLevelObservation.PostedFile.FileName).ToLower();
                            str_fname = FileUploadGWLevelObservation.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadGWLevelObservation.PostedFile))
                                {
                                    if (FileUploadGWLevelObservation.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadGWLevelObservation.PostedFile);
                                        obj_insertMiningRenewApplicationAttachment.ContentType = FileUploadGWLevelObservation.PostedFile.ContentType;
                                        obj_insertMiningRenewApplicationAttachment.AttachmentPath = FileUploadGWLevelObservation.FileName;
                                        obj_insertMiningRenewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageGWLevelObservation.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageGWLevelObservation.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageGWLevelObservation.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageGWLevelObservation.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageGWLevelObservation.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageGWLevelObservation.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageGWLevelObservation.Text = "Please select a file..!!";
                            lblMessageGWLevelObservation.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningRenewApplicationAttachment.MiningRenewApplicationCode = obj_MiningRenewApplication.MiningRenewApplicationCode;
                        obj_insertMiningRenewApplicationAttachment.AttachmentCode = obj_MiningRenewApplication.MiningRenewMonitoringGWRegime.MonitoringGWDetailOfLevelOfObservationWellsAttachCode;

                        obj_insertMiningRenewApplicationAttachment.AttachmentName = txtGWLevelObservation.Text; ;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageGWLevelObservation.Text = "File Upload Success";
                            lblMessageGWLevelObservation.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageGWLevelObservation.Text = "File Upload Failed";
                            lblMessageGWLevelObservation.ForeColor = System.Drawing.Color.Red;

                        }
                        lblMessageGWLevelObservation.ForeColor = Color.Green;
                        lblMessageGWLevelObservation.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;

                        txtGWLevelObservation.Text = "";

                        BindMiningRenewApplicationAttachmentGroundWaterObservationWellsDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                        lblMessageGWFlowDirectionMap.Text = "";
                        //lblMessageGWLevelObservation.Text = "";
                        lblMessageGQofGWInArea.Text = "";
                        lblMessageChangesInTopography.Text = "";
                        lblMessageChangesInDrainage.Text = "";
                        lblMessageExistingNOC.Text = "";
                        lblMessageExtra.Text = "";
                        lblMessageReasonForNotApplyingBefore.Text = "";

                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";
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
                    lblMessageGWLevelObservation.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageGWLevelObservation.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUplodGQofGWInArea_Click(object sender, EventArgs e)
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
                strActionName = "UplodGQofGWInArea";
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplicationForNoLimit = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
                arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplicationForNoLimit.GetMonitorGroundWaterRegimeGQsuroudingsAttachmentList();
                if (arr_MiningRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_insertMiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment();
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_MiningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();

                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadGQofGWInArea.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadGQofGWInArea.PostedFile.FileName).ToLower();
                            str_fname = FileUploadGQofGWInArea.FileName;

                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadGQofGWInArea.PostedFile))
                                {
                                    if (FileUploadGQofGWInArea.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadGQofGWInArea.PostedFile);
                                        obj_insertMiningRenewApplicationAttachment.ContentType = FileUploadGQofGWInArea.PostedFile.ContentType;
                                        obj_insertMiningRenewApplicationAttachment.AttachmentPath = FileUploadGQofGWInArea.FileName;
                                        obj_insertMiningRenewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageGQofGWInArea.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageGQofGWInArea.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageGQofGWInArea.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageGQofGWInArea.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageGQofGWInArea.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageGQofGWInArea.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageGQofGWInArea.Text = "Please select a file..!!";
                            lblMessageGQofGWInArea.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningRenewApplicationAttachment.MiningRenewApplicationCode = obj_MiningRenewApplication.MiningRenewApplicationCode;
                        obj_insertMiningRenewApplicationAttachment.AttachmentCode = obj_MiningRenewApplication.MiningRenewMonitoringGWRegime.MonitoringGWGeneralQualityOfGWInSurroundingAttachCode;

                        obj_insertMiningRenewApplicationAttachment.AttachmentName = txtGQofGWInArea.Text; ;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageGQofGWInArea.Text = "File Upload Success";
                            lblMessageGQofGWInArea.ForeColor = System.Drawing.Color.Green;
                            //obj_insertIndustrialNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            //lblMessageSitePlan.Text = obj_insertIndustrialNewApplicationAttachment.CustumMessage;
                            lblMessageGQofGWInArea.Text = "File Upload Failed";
                            lblMessageGQofGWInArea.ForeColor = System.Drawing.Color.Red;

                        }
                        lblMessageGQofGWInArea.ForeColor = Color.Green;
                        lblMessageGQofGWInArea.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;

                        txtGQofGWInArea.Text = "";

                        BindMiningRenewApplicationAttachmentGQualityOfGroundWaterInAreaDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                        lblMessageGWFlowDirectionMap.Text = "";
                        lblMessageGWLevelObservation.Text = "";
                        //lblMessageGQofGWInArea.Text = "";
                        lblMessageChangesInTopography.Text = "";
                        lblMessageChangesInDrainage.Text = "";
                        lblMessageExistingNOC.Text = "";
                        lblMessageExtra.Text = "";
                        lblMessageReasonForNotApplyingBefore.Text = "";


                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";
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
                    lblMessageGQofGWInArea.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageGQofGWInArea.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUploadExtra_Click(object sender, EventArgs e)
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
                strActionName = "UploadExtraAttachment";
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplicationForNoLimit = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
                arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplicationForNoLimit.GetExtraAttachmentList();
                if (arr_MiningRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_insertMiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment();
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_MiningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();

                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadExtra.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadExtra.PostedFile.FileName).ToLower();
                            str_fname = FileUploadExtra.FileName;

                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadExtra.PostedFile))
                                {
                                    if (FileUploadExtra.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadExtra.PostedFile);
                                        obj_insertMiningRenewApplicationAttachment.ContentType = FileUploadExtra.PostedFile.ContentType;
                                        obj_insertMiningRenewApplicationAttachment.AttachmentPath = FileUploadExtra.FileName;
                                        obj_insertMiningRenewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageExtra.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageExtra.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }

                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageExtra.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageExtra.Text = "Please select a file..!!";
                            lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningRenewApplicationAttachment.MiningRenewApplicationCode = obj_MiningRenewApplication.MiningRenewApplicationCode;
                        obj_insertMiningRenewApplicationAttachment.AttachmentCode = obj_MiningRenewApplication.MiningRenewExtraAttachmentCode;

                        obj_insertMiningRenewApplicationAttachment.AttachmentName = txtExtraAttachment.Text; ;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageExtra.Text = "File Upload Success";
                            lblMessageExtra.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";

                            lblMessageExtra.Text = "File Upload Failed";
                            lblMessageExtra.ForeColor = System.Drawing.Color.Red;

                        }


                        lblMessageExtra.ForeColor = Color.Green;
                        lblMessageExtra.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;

                        txtExtraAttachment.Text = "";

                        BindMiningRenewApplicationAttachmentExtraAttachmentsDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                        lblMessageGWFlowDirectionMap.Text = "";
                        lblMessageGWLevelObservation.Text = "";
                        lblMessageGQofGWInArea.Text = "";
                        lblMessageChangesInTopography.Text = "";
                        lblMessageChangesInDrainage.Text = "";
                        lblMessageExistingNOC.Text = "";
                        //lblMessageExtra.Text = "";
                        lblMessageReasonForNotApplyingBefore.Text = "";

                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";
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
    protected void btnUploadChangesInTopography_Click(object sender, EventArgs e)
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
                strActionName = "UploadChangesInTopographyAttachment";
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplicationForNoLimit = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
                arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplicationForNoLimit.GetChangesInTopographyAttachmentList();
                if (arr_MiningRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_insertMiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment();
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_MiningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();

                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadChangesInTopography.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadChangesInTopography.PostedFile.FileName).ToLower();
                            str_fname = FileUploadChangesInTopography.FileName;

                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadChangesInTopography.PostedFile))
                                {
                                    if (FileUploadChangesInTopography.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadChangesInTopography.PostedFile);
                                        obj_insertMiningRenewApplicationAttachment.ContentType = FileUploadChangesInTopography.PostedFile.ContentType;
                                        obj_insertMiningRenewApplicationAttachment.AttachmentPath = FileUploadChangesInTopography.FileName;
                                        obj_insertMiningRenewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageChangesInTopography.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageChangesInTopography.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageChangesInTopography.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageChangesInTopography.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }

                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageChangesInTopography.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageChangesInTopography.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageChangesInTopography.Text = "Please select a file..!!";
                            lblMessageChangesInTopography.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningRenewApplicationAttachment.MiningRenewApplicationCode = obj_MiningRenewApplication.MiningRenewApplicationCode;
                        obj_insertMiningRenewApplicationAttachment.AttachmentCode = obj_MiningRenewApplication.ChangeTopographyofAreaAttachmentCode;

                        obj_insertMiningRenewApplicationAttachment.AttachmentName = txtChangesInTopographyAttachment.Text; ;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageChangesInTopography.Text = "File Upload Success";
                            lblMessageChangesInTopography.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";

                            lblMessageChangesInTopography.Text = "File Upload Failed";
                            lblMessageChangesInTopography.ForeColor = System.Drawing.Color.Red;

                        }
                        lblMessageChangesInTopography.ForeColor = Color.Green;
                        lblMessageChangesInTopography.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;

                        txtChangesInTopographyAttachment.Text = "";

                        BindMiningRenewApplicationAttachmentChangesInTopographyDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                        lblMessageGWFlowDirectionMap.Text = "";
                        lblMessageGWLevelObservation.Text = "";
                        lblMessageGQofGWInArea.Text = "";
                        //lblMessageChangesInTopography.Text = "";
                        lblMessageChangesInDrainage.Text = "";
                        lblMessageExistingNOC.Text = "";
                        lblMessageExtra.Text = "";
                        lblMessageReasonForNotApplyingBefore.Text = "";

                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";
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
    protected void btnUploadChangesInDrainage_Click(object sender, EventArgs e)
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
                strActionName = "UploadChangesInDrainageAttachment";
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplicationForNoLimit = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
                arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplicationForNoLimit.GetChangesInDrainageAttachmentList();
                if (arr_MiningRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_insertMiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment();
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_MiningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();

                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadChangesInDrainage.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadChangesInDrainage.PostedFile.FileName).ToLower();
                            str_fname = FileUploadChangesInDrainage.FileName;

                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadChangesInDrainage.PostedFile))
                                {
                                    if (FileUploadChangesInDrainage.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadChangesInDrainage.PostedFile);
                                        obj_insertMiningRenewApplicationAttachment.ContentType = FileUploadChangesInDrainage.PostedFile.ContentType;
                                        obj_insertMiningRenewApplicationAttachment.AttachmentPath = FileUploadChangesInDrainage.FileName;
                                        obj_insertMiningRenewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed !";
                                        lblMessageChangesInDrainage.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageChangesInDrainage.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed !";
                                    lblMessageChangesInDrainage.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageChangesInDrainage.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }

                            }
                            else
                            {
                                strStatus = "File Upload Failed !";
                                lblMessageChangesInDrainage.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageChangesInDrainage.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageChangesInDrainage.Text = "Please select a file..!!";
                            lblMessageChangesInDrainage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertMiningRenewApplicationAttachment.MiningRenewApplicationCode = obj_MiningRenewApplication.MiningRenewApplicationCode;
                        obj_insertMiningRenewApplicationAttachment.AttachmentCode = obj_MiningRenewApplication.ChangeDrainageAreaAttachmentCode;

                        obj_insertMiningRenewApplicationAttachment.AttachmentName = txtChangesInDrainage.Text; ;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageChangesInDrainage.Text = "File Upload Success";
                            lblMessageChangesInDrainage.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";

                            lblMessageChangesInDrainage.Text = "File Upload Failed";
                            lblMessageChangesInDrainage.ForeColor = System.Drawing.Color.Red;

                        }

                        lblMessageChangesInDrainage.ForeColor = Color.Green;
                        lblMessageChangesInDrainage.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;

                        txtChangesInDrainage.Text = "";

                        BindMiningRenewApplicationAttachmentChangesInDrainageDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                        lblMessageGWFlowDirectionMap.Text = "";
                        lblMessageGWLevelObservation.Text = "";
                        lblMessageGQofGWInArea.Text = "";
                        lblMessageChangesInTopography.Text = "";
                        //lblMessageChangesInDrainage.Text = "";
                        lblMessageExistingNOC.Text = "";
                        lblMessageExtra.Text = "";
                        lblMessageReasonForNotApplyingBefore.Text = "";

                    }
                    catch (Exception)
                    {
                        strStatus = "File Upload Failed !";
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

    protected void btnUploadBharatKoshReciept_Click(object sender, EventArgs e)
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
                strActionName = "File Upload Bharat Kosh Reciept";
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplicationForNoLimit = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
                arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplicationForNoLimit.GetBharatKoshRecieptAttachmentList();
                if (arr_MiningRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_insertMiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment();
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_MiningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadBharatKoshReciept.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadBharatKoshReciept.PostedFile.FileName).ToLower();
                            str_fname = FileUploadBharatKoshReciept.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadBharatKoshReciept.PostedFile))
                                {
                                    if (FileUploadBharatKoshReciept.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadBharatKoshReciept.PostedFile);
                                        obj_insertMiningRenewApplicationAttachment.ContentType = FileUploadBharatKoshReciept.PostedFile.ContentType;
                                        obj_insertMiningRenewApplicationAttachment.AttachmentPath = FileUploadBharatKoshReciept.FileName;
                                        obj_insertMiningRenewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageBharatKoshReciept.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageBharatKoshReciept.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageBharatKoshReciept.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageBharatKoshReciept.Text = "Please select a file..!!";
                            lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertMiningRenewApplicationAttachment.MiningRenewApplicationCode = obj_MiningRenewApplication.MiningRenewApplicationCode;
                        obj_insertMiningRenewApplicationAttachment.AttachmentCode = obj_MiningRenewApplication.MiningRenewBharatKoshRecieptAttachmentCode;
                        obj_insertMiningRenewApplicationAttachment.AttachmentName = txtBharatKoshReciept.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertMiningRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertMiningRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageBharatKoshReciept.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;
                            lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;

                        }

                        lblMessageBharatKoshReciept.ForeColor = Color.Green;
                        lblMessageBharatKoshReciept.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;
                        txtBharatKoshReciept.Text = "";
                        BindMiningRenewApplicationAttachmentBharatKoshRecieptDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                        
                        lblMessageGWFlowDirectionMap.Text = "";
                        lblMessageGWLevelObservation.Text = "";
                        lblMessageGQofGWInArea.Text = "";
                        lblMessageChangesInTopography.Text = "";
                        lblMessageChangesInDrainage.Text = "";
                        lblMessageExistingNOC.Text = "";
                                              lblMessageReasonForNotApplyingBefore.Text = "";
                        lblMessageExtra.Text = "";
                       
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

    protected void btnUploadApplicationSignatureSeal_Click(object sender, EventArgs e)
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
                strActionName = "File Upload Aplication with Signature and Seal";
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplicationForNoLimit = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
                arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplicationForNoLimit.GetSignedDocAttachmentList();

                if (arr_MiningRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment obj_insertMiningRenewApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment();
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment> lst_MiningRenewApplicationAttachmentList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment>();
                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadApplicationSignatureSeal.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadApplicationSignatureSeal.PostedFile.FileName).ToLower();
                            str_fname = FileUploadApplicationSignatureSeal.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadApplicationSignatureSeal.PostedFile))
                                {
                                    if (FileUploadApplicationSignatureSeal.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertMiningRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadApplicationSignatureSeal.PostedFile);
                                        obj_insertMiningRenewApplicationAttachment.ContentType = FileUploadApplicationSignatureSeal.PostedFile.ContentType;
                                        obj_insertMiningRenewApplicationAttachment.AttachmentPath = FileUploadApplicationSignatureSeal.FileName;
                                        obj_insertMiningRenewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageAplicationSignatureSeal.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageAplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageAplicationSignatureSeal.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageAplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageAplicationSignatureSeal.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageAplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageAplicationSignatureSeal.Text = "Please select a file..!!";
                            lblMessageAplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertMiningRenewApplicationAttachment.MiningRenewApplicationCode = obj_MiningRenewApplication.MiningRenewApplicationCode;
                        obj_insertMiningRenewApplicationAttachment.AttachmentCode = obj_MiningRenewApplication.SignedDocAttCode;
                        obj_insertMiningRenewApplicationAttachment.AttachmentName = txtApplicationSignatureSeal.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertMiningRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertMiningRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertMiningRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageAplicationSignatureSeal.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;
                            lblMessageAplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;

                        }

                        lblMessageAplicationSignatureSeal.ForeColor = Color.Green;
                        lblMessageAplicationSignatureSeal.Text = obj_insertMiningRenewApplicationAttachment.CustumMessage;
                        txtApplicationSignatureSeal.Text = "";
                        BindMiningRenewApplicationAttachmentSignedDocDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                        lblMessageExtra.Text = "";
                      
                        lblMessageBharatKoshReciept.Text = "";
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
    #endregion


}