using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Configuration;
using NOCAP;
using System.Threading;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using NOCAP.BLL.Infrastructure.Renew.SADApplication;


public partial class ExternalUser_InfrastructureRenew_Attachment : System.Web.UI.Page
{
    string strPageName = "INFRenewAttachment";
    string strActionName = "";
    string strStatus = "";
    //decimal GroundWaterRequirement0KLD = 0;
    //decimal GroundWaterRequirement10KLD = 10;
    decimal GroundWaterRequirement100KLD = 100;

    long lngInfRenewSubmitAppCode;
    public long InfRenewSubmitAppCode
    {
        get
        {
            return lngInfRenewSubmitAppCode;
        }
        set
        {
            lngInfRenewSubmitAppCode = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        if (!IsPostBack)
        {
            try
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;


                if (NOCAPExternalUtility.FillDropDownReferralLetterType(ref ddlReferralLetter) != 1)
                {
                    lblMessageReferralLetter.Text = "Problem in Referral Letter Population!";
                }
                else
                {
                    ddlReferralLetter.Items[0].Value = "0";
                }

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
                    }
                }
                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblInfrastructureApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                        }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    lblApplicationCode.Text = lblInfrastructureApplicationCodeFrom.Text;


                    BindInfrastructureRenewApplicationAttachmentExisingNOCDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindInfrastructureRenewApplicationAttachmentRainwaterHarvestingDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindInfrastructureRenewApplicationComplianceConditionNOC(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindInfrastructureRenewComplianceConditionNOCAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

                    BindAffidavitAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindWaterAuditReportAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

                    BindSourceWaterNonAvailabilityCertificateAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

                    BindInfrastructureRenewApplicationAttachmentBharatKoshRecieptDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindInfrastructureRenewApplicationAttachmentSignedDocDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));


                    //  BindInfrastructureRenewApplicationAttachmentSitePlanDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    //  BindInfrastructureRenewApplicationAttachmentCertifiedRevenueSketchDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    //  BindInfrastructureRenewApplicationAttachmentReasonForNotApplyingBeforeDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));


                    BindInfrastructureRenewApplicationAttachmentWaterRequirementDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindInfrastructureRenewApplicationAttachmentGroundwaterAvailabilityDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

                    // BindInfrastructureRenewApplicationAttachmentUndertakingDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindInfrastructureRenewApplicationAttachmentExtraDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

                    //  BindInfrastructureRenewApplicationComplianceConditionNOCOther();
                    //  BindInfrastructureRenewComplianceConditionNOCOtherAttachmentDetails();




                    //BindInfrastructureNewApplicationAttachmentRefferalLetterDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    //BindInfrastructureNewApplicationAttachmentNonPollutingDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindProofOfOwnershipAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindProofofownershipLeaseoftankerAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

                }
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewApplication.NameOfInfrastructure);
            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    #region Upload
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
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
                arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplicationForNoLimit.GetSignedDocAttachmentList();

                if (arr_InfrastructureRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_insertInfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();
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
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadApplicationSignatureSeal.PostedFile);
                                        obj_insertInfrastructureRenewApplicationAttachment.ContentType = FileUploadApplicationSignatureSeal.PostedFile.ContentType;
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = FileUploadApplicationSignatureSeal.FileName;
                                        obj_insertInfrastructureRenewApplicationAttachment.FileExtension = str_ext;
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
                        obj_insertInfrastructureRenewApplicationAttachment.InfrastructureRenewApplicationCode = obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentCode = obj_InfrastructureRenewApplication.SignedDocAttCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentName = txtApplicationSignatureSeal.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageAplicationSignatureSeal.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                            lblMessageAplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;

                        }

                        lblMessageAplicationSignatureSeal.ForeColor = Color.Green;
                        lblMessageAplicationSignatureSeal.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                        txtApplicationSignatureSeal.Text = "";
                        BindInfrastructureRenewApplicationAttachmentSignedDocDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

                        lblMessageExtra.Text = "";
                        lblMessageGroundwaterAvailability.Text = "";
                        lblMessageRainwaterHarvesting.Text = "";

                        lblMessageUndertaking.Text = "";
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

    protected void btnUplodUndertaking_Click(object sender, EventArgs e)
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
                if (Page.IsValid)
                {
                    strActionName = "File Upload Undertaking";
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
                    arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplicationForNoLimit.GetUndertakingAttachmentList();

                    if (arr_InfrastructureRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                    {
                        string str_fname;
                        string str_ext;

                        string str_newFileNameWithPath = "";

                        string str_restPath = "";

                        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_insertInfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment();
                        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();

                        byte[] buffer = new byte[1];
                        try
                        {
                            if (FileUploadUndertaking.HasFile)
                            {
                                str_ext = System.IO.Path.GetExtension(FileUploadUndertaking.PostedFile.FileName).ToLower();
                                str_fname = FileUploadUndertaking.FileName;

                                if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                                {
                                    if (NOCAPExternalUtility.IsValidFile(FileUploadUndertaking.PostedFile))
                                    {
                                        if (FileUploadUndertaking.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                        {
                                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadUndertaking.PostedFile);
                                            obj_insertInfrastructureRenewApplicationAttachment.ContentType = FileUploadUndertaking.PostedFile.ContentType;
                                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = FileUploadUndertaking.FileName;
                                            obj_insertInfrastructureRenewApplicationAttachment.FileExtension = str_ext;
                                        }
                                        else
                                        {
                                            strStatus = "File Upload Success";
                                            lblMessageUndertaking.Text = "File can not upload. It has more than 5 MB size";
                                            lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Success";
                                        lblMessageUndertaking.Text = "Not a valid file!!..Select an other file!!";
                                        lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Success";
                                    lblMessageUndertaking.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                lblMessageUndertaking.Text = "Please select a file..!!";
                                lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                            obj_insertInfrastructureRenewApplicationAttachment.InfrastructureRenewApplicationCode = obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode;
                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentCode = obj_InfrastructureRenewApplication.Undertaking.UndertakingAttachCode;

                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentName = txtUndertaking.Text;

                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                            obj_insertInfrastructureRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                            if (obj_insertInfrastructureRenewApplicationAttachment.Add() == 1)
                            {
                                strStatus = "File Upload Success";
                                obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageUndertaking.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                                lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                            lblMessageUndertaking.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                            lblMessageUndertaking.ForeColor = Color.Green;
                            txtUndertaking.Text = "";
                            BindInfrastructureRenewApplicationAttachmentUndertakingDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
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
                        lblMessageUndertaking.Text = "Maximum number of files to be uploaded is 5";
                        lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                    }
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
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
                    arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplicationForNoLimit.GetExistingNOCAttachmentList();

                    if (arr_InfrastructureRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                    {
                        string str_fname;
                        string str_ext;
                        string str_newFileNameWithPath = "";

                        string str_restPath = "";

                        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_insertInfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment();
                        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();
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
                                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(txtFileUploadExistingNOC.PostedFile);
                                            obj_insertInfrastructureRenewApplicationAttachment.ContentType = txtFileUploadExistingNOC.PostedFile.ContentType;
                                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = txtFileUploadExistingNOC.FileName;
                                            obj_insertInfrastructureRenewApplicationAttachment.FileExtension = str_ext;
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

                            obj_insertInfrastructureRenewApplicationAttachment.InfrastructureRenewApplicationCode = obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode;
                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentCode = obj_InfrastructureRenewApplication.InfrastructureRenewExistingNOC.ExistNOCAttachCode;
                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentName = txtExistingNOC.Text;

                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                            obj_insertInfrastructureRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                            if (obj_insertInfrastructureRenewApplicationAttachment.Add() == 1)
                            {
                                strStatus = "File Upload Success";
                                obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;

                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageExistingNOC.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                                lblMessageExistingNOC.ForeColor = System.Drawing.Color.Red;

                            }

                            lblMessageExistingNOC.ForeColor = Color.Green;
                            lblMessageExistingNOC.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                            txtExistingNOC.Text = "";
                            BindInfrastructureRenewApplicationAttachmentExisingNOCDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
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
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_infrastructureRenewApplicationAttachmentList;
                arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplicationForNoLimit.GetAffidavitNOCCondiAttachmentList();

                if (arr_infrastructureRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_insertInfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_infrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();
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
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadAffidavit.PostedFile);
                                        obj_insertInfrastructureRenewApplicationAttachment.ContentType = FileUploadAffidavit.PostedFile.ContentType;
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = FileUploadAffidavit.FileName;
                                        obj_insertInfrastructureRenewApplicationAttachment.FileExtension = str_ext;
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
                        obj_insertInfrastructureRenewApplicationAttachment.InfrastructureRenewApplicationCode = obj_infrastructureRenewApplication.InfrastructureRenewApplicationCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentCode = obj_infrastructureRenewApplication.AffidavitNOCCondiAttCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentName = txtAffidavit.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertInfrastructureRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageAffidavit.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                            lblMessageAffidavit.ForeColor = System.Drawing.Color.Red;

                        }


                        BindAffidavitAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageAffidavit.ForeColor = Color.Green;
                        lblMessageAffidavit.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;

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
                InfrastructureRenewSADApplication obj_infrastructureRenewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                InfrastructureRenewSADApplicationAttachment[] arr_infrastructureRenewApplicationAttachmentList;
                arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplicationForNoLimit.GetWaterAuditReportAttachmentList();

                if (arr_infrastructureRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    InfrastructureRenewSADApplicationAttachment obj_insertInfrastructureRenewApplicationAttachment = new InfrastructureRenewSADApplicationAttachment();
                    InfrastructureRenewSADApplication obj_infrastructureRenewApplication = new InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<InfrastructureRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<InfrastructureRenewSADApplicationAttachment>();
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
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadWaterAuditReport.PostedFile);
                                        obj_insertInfrastructureRenewApplicationAttachment.ContentType = FileUploadWaterAuditReport.PostedFile.ContentType;
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = FileUploadWaterAuditReport.FileName;
                                        obj_insertInfrastructureRenewApplicationAttachment.FileExtension = str_ext;
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
                        obj_insertInfrastructureRenewApplicationAttachment.InfrastructureRenewApplicationCode = obj_infrastructureRenewApplication.InfrastructureRenewApplicationCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentCode = obj_infrastructureRenewApplication.WaterAuditReportAttCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentName = txtWaterAuditReport.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertInfrastructureRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageWaterAuditReport.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                            lblMessageWaterAuditReport.ForeColor = System.Drawing.Color.Red;

                        }


                        BindWaterAuditReportAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageWaterAuditReport.ForeColor = Color.Green;
                        lblMessageWaterAuditReport.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;

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
                    lblMessageWaterAuditReport.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageWaterAuditReport.ForeColor = System.Drawing.Color.Red;
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
                InfrastructureRenewSADApplication obj_infrastructureRenewApplicationForNoLimit = new InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                InfrastructureRenewSADApplicationAttachment[] arr_infrastructureRenewApplicationAttachmentList;
                arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplicationForNoLimit.GetCertiNonAvaAttachmentList();

                if (arr_infrastructureRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    InfrastructureRenewSADApplicationAttachment obj_insertInfrastructureRenewApplicationAttachment = new InfrastructureRenewSADApplicationAttachment();
                    InfrastructureRenewSADApplication obj_infrastructureRenewApplication = new InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<InfrastructureRenewSADApplicationAttachment> lst_infrastructureRenewApplicationAttachmentList = new List<InfrastructureRenewSADApplicationAttachment>();
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
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(txtFileUploadSourceofAvailabilityofSurfaceWater.PostedFile);
                                        obj_insertInfrastructureRenewApplicationAttachment.ContentType = txtFileUploadSourceofAvailabilityofSurfaceWater.PostedFile.ContentType;
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = txtFileUploadSourceofAvailabilityofSurfaceWater.FileName;
                                        obj_insertInfrastructureRenewApplicationAttachment.FileExtension = str_ext;
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
                        obj_insertInfrastructureRenewApplicationAttachment.InfrastructureRenewApplicationCode = obj_infrastructureRenewApplication.InfrastructureRenewApplicationCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentCode = obj_infrastructureRenewApplication.CertiNonAvaAttCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentName = txtSourceofAvailabilityofSurfaceWater.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertInfrastructureRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageSourceofAvailabilityofSurfaceWater.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                            lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;

                        }


                        BindSourceWaterNonAvailabilityCertificateAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

                        clearMessage();
                        lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = Color.Green;
                        lblMessageSourceofAvailabilityofSurfaceWater.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;

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
                strActionName = "File Upload Extra";
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
                arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplicationForNoLimit.GetExtraAttachmentList();

                if (arr_InfrastructureRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;

                    string str_newFileNameWithPath = "";

                    string str_restPath = "";

                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_insertInfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();

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

                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadExtra.PostedFile);
                                        obj_insertInfrastructureRenewApplicationAttachment.ContentType = FileUploadExtra.PostedFile.ContentType;
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = FileUploadExtra.FileName;
                                        obj_insertInfrastructureRenewApplicationAttachment.FileExtension = str_ext;


                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageExtra.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageExtra.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
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
                        obj_insertInfrastructureRenewApplicationAttachment.InfrastructureRenewApplicationCode = obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentCode = obj_InfrastructureRenewApplication.InfrastructureRenewExtraAttachmentCode;

                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentName = txtExtraAttachment.Text; ;


                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;



                        if (obj_insertInfrastructureRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageExtra.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                            lblMessageExtra.ForeColor = System.Drawing.Color.Red;

                        }

                        lblMessageExtra.ForeColor = Color.Green;
                        lblMessageExtra.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                        txtExtraAttachment.Text = "";
                        BindInfrastructureRenewApplicationAttachmentExtraDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        lblMessageCertifiedRevenueSketch.Text = "";
                        lblMessageReasonForNotApplyingBefore.Text = "";
                        lblMessageGroundwaterAvailability.Text = "";
                        lblMessageRainwaterHarvesting.Text = "";
                        lblMessageExistingNOC.Text = "";
                        lblMessageUndertaking.Text = "";
                        lblMessageWaterRequirement.Text = "";
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

    protected void btnUplodWaterRequirement_Click(object sender, EventArgs e)
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
                strActionName = "File Upload Water Requirement";
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
                arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplicationForNoLimit.GetWaterRequrementAttachmentList();

                if (arr_InfrastructureRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    string str_restPath = "";

                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_insertInfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();

                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadWaterRequirement.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadWaterRequirement.PostedFile.FileName).ToLower();
                            str_fname = FileUploadWaterRequirement.FileName;

                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadWaterRequirement.PostedFile))
                                {
                                    if (FileUploadWaterRequirement.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadWaterRequirement.PostedFile);
                                        obj_insertInfrastructureRenewApplicationAttachment.ContentType = FileUploadWaterRequirement.PostedFile.ContentType;
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = FileUploadWaterRequirement.FileName;
                                        obj_insertInfrastructureRenewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageWaterRequirement.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageWaterRequirement.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageWaterRequirement.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageWaterRequirement.Text = "Please select a file..!!";
                            lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertInfrastructureRenewApplicationAttachment.InfrastructureRenewApplicationCode = obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentCode = obj_InfrastructureRenewApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.WaterRequrementAttachCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentName = txtWaterRequirement.Text;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageWaterRequirement.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                            lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
                        }

                        lblMessageWaterRequirement.ForeColor = Color.Green;
                        lblMessageWaterRequirement.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                        txtWaterRequirement.Text = "";
                        BindInfrastructureRenewApplicationAttachmentWaterRequirementDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
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
                    lblMessageWaterRequirement.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void btnUplodGroundwaterAvailability_Click(object sender, EventArgs e)
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
                strActionName = "File Upload Ground Water Avaialbility";
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
                arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplicationForNoLimit.GetGroundwaterAvailabilityAttachmentList();

                if (arr_InfrastructureRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;

                    string str_newFileNameWithPath = "";

                    string str_restPath = "";

                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_insertInfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();

                    byte[] buffer = new byte[1];
                    try
                    {
                        if (FileUploadGroundwaterAvailability.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadGroundwaterAvailability.PostedFile.FileName).ToLower();
                            str_fname = FileUploadGroundwaterAvailability.FileName;

                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadGroundwaterAvailability.PostedFile))
                                {
                                    if (FileUploadGroundwaterAvailability.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadGroundwaterAvailability.PostedFile);
                                        obj_insertInfrastructureRenewApplicationAttachment.ContentType = FileUploadGroundwaterAvailability.PostedFile.ContentType;
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = FileUploadGroundwaterAvailability.FileName;
                                        obj_insertInfrastructureRenewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageGroundwaterAvailability.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageGroundwaterAvailability.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageGroundwaterAvailability.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageGroundwaterAvailability.Text = "Please select a file..!!";
                            lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertInfrastructureRenewApplicationAttachment.InfrastructureRenewApplicationCode = obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentCode = obj_InfrastructureRenewApplication.GroundWaterAvailability.GroundWaterAvailabilityAttachCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentName = txtgvGroundwaterAvailability.Text;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageGroundwaterAvailability.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                            lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                        }
                        lblMessageGroundwaterAvailability.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                        lblMessageGroundwaterAvailability.ForeColor = Color.Green;
                        txtgvGroundwaterAvailability.Text = "";
                        BindInfrastructureRenewApplicationAttachmentGroundwaterAvailabilityDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
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
                    lblMessageGroundwaterAvailability.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUplodRainwaterHarvesting_Click(object sender, EventArgs e)
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
                strActionName = "File Upload Rain Water Harvesting";
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
                arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplicationForNoLimit.GetRainwaterHarvestingAttachmentList();

                if (arr_InfrastructureRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;

                    string str_newFileNameWithPath = "";

                    string str_restPath = "";

                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_insertInfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();

                    byte[] buffer = new byte[1];
                    try
                    {


                        if (FileUploadRainwaterHarvesting.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadRainwaterHarvesting.PostedFile.FileName).ToLower();
                            str_fname = FileUploadRainwaterHarvesting.FileName;

                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadRainwaterHarvesting.PostedFile))
                                {
                                    if (FileUploadRainwaterHarvesting.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadRainwaterHarvesting.PostedFile);
                                        obj_insertInfrastructureRenewApplicationAttachment.ContentType = FileUploadRainwaterHarvesting.PostedFile.ContentType;
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = FileUploadRainwaterHarvesting.FileName;
                                        obj_insertInfrastructureRenewApplicationAttachment.FileExtension = str_ext;

                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageRainwaterHarvesting.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageRainwaterHarvesting.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageRainwaterHarvesting.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageRainwaterHarvesting.Text = "Please select a file..!!";
                            lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertInfrastructureRenewApplicationAttachment.InfrastructureRenewApplicationCode = obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentCode = obj_InfrastructureRenewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeAttachCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentName = txtRainwaterHarvesting.Text;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageRainwaterHarvesting.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                            lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                        }

                        lblMessageRainwaterHarvesting.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                        lblMessageRainwaterHarvesting.ForeColor = Color.Green;
                        txtRainwaterHarvesting.Text = "";
                        BindInfrastructureRenewApplicationAttachmentRainwaterHarvestingDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
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
                    lblMessageRainwaterHarvesting.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
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
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
                arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplicationForNoLimit.GetReasonForNotApplyingBeforeAttachmentList();

                if (arr_InfrastructureRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    string str_restPath = "";

                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_insertInfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();
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
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(txtFileUploadReasonForNotApplyingBefore.PostedFile);
                                        obj_insertInfrastructureRenewApplicationAttachment.ContentType = txtFileUploadReasonForNotApplyingBefore.PostedFile.ContentType;
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = txtFileUploadReasonForNotApplyingBefore.FileName;
                                        obj_insertInfrastructureRenewApplicationAttachment.FileExtension = str_ext;
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

                        obj_insertInfrastructureRenewApplicationAttachment.InfrastructureRenewApplicationCode = obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentCode = obj_InfrastructureRenewApplication.InfrastructureRenewExistingNOC.ExistingNOCReasonForNotApplyBeforeExpiryAttachCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentName = txtReasonForNotApplyingBefore.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Deleted Success";
                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Deleting Failed";
                            lblMessageReasonForNotApplyingBefore.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                            lblMessageReasonForNotApplyingBefore.ForeColor = System.Drawing.Color.Red;
                        }
                        lblMessageReasonForNotApplyingBefore.ForeColor = Color.Green;
                        lblMessageReasonForNotApplyingBefore.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                        txtReasonForNotApplyingBefore.Text = "";
                        BindInfrastructureRenewApplicationAttachmentReasonForNotApplyingBeforeDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
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

    protected void btnUploadCertifiedRevenueSketch_Click(object sender, EventArgs e)
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

                strActionName = "File Upload Certificate of Revenue Sketch";
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
                arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplicationForNoLimit.GetCertifiedRevenueSketAttachmentList();

                if (arr_InfrastructureRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    try
                    {
                        string str_fname;
                        string str_ext;
                        string str_newFileNameWithPath = "";
                        string str_restPath = "";

                        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_insertInfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment();
                        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();

                        byte[] buffer = new byte[1];

                        if (txtFileUploadCertifiedRevenueSketch.HasFile)
                        {

                            str_ext = System.IO.Path.GetExtension(txtFileUploadCertifiedRevenueSketch.PostedFile.FileName).ToLower();

                            str_fname = txtFileUploadCertifiedRevenueSketch.FileName;

                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {

                                if (NOCAPExternalUtility.IsValidFile(txtFileUploadCertifiedRevenueSketch.PostedFile))
                                {

                                    if (txtFileUploadCertifiedRevenueSketch.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(txtFileUploadCertifiedRevenueSketch.PostedFile);
                                        obj_insertInfrastructureRenewApplicationAttachment.ContentType = txtFileUploadCertifiedRevenueSketch.PostedFile.ContentType;
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = txtFileUploadCertifiedRevenueSketch.FileName;
                                        obj_insertInfrastructureRenewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageCertifiedRevenueSketch.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageCertifiedRevenueSketch.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageCertifiedRevenueSketch.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageCertifiedRevenueSketch.Text = "Please select a file..!!";
                            lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertInfrastructureRenewApplicationAttachment.InfrastructureRenewApplicationCode = obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentCode = obj_InfrastructureRenewApplication.RenewProLocCertReveSketAttachmentCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentName = txtCertifiedRevenueSketchAttachment.Text;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Failed";
                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageCertifiedRevenueSketch.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                            lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
                        }

                        lblMessageCertifiedRevenueSketch.ForeColor = Color.Green;
                        lblMessageCertifiedRevenueSketch.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                        BindInfrastructureRenewApplicationAttachmentCertifiedRevenueSketchDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
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
                    lblMessageCertifiedRevenueSketch.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    protected void btnUploadSitePlan_Click(object sender, EventArgs e)
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

                strActionName = "UploadSitePlan";
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
                arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplicationForNoLimit.GetSitePlanAttachmentList();

                if (arr_InfrastructureRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    try
                    {
                        string str_fname;
                        string str_ext;

                        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_insertInfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment();
                        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();

                        byte[] buffer = new byte[1];

                        if (FileUploadSitePlan.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadSitePlan.PostedFile.FileName).ToLower();

                            str_fname = FileUploadSitePlan.FileName;
                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadSitePlan.PostedFile))
                                {
                                    if (FileUploadSitePlan.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {

                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadSitePlan.PostedFile);
                                        obj_insertInfrastructureRenewApplicationAttachment.ContentType = FileUploadSitePlan.PostedFile.ContentType;
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = FileUploadSitePlan.FileName;
                                        obj_insertInfrastructureRenewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageSitePlan.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageSitePlan.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageSitePlan.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblMessageSitePlan.Text = "Please select a file..!!";
                            lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertInfrastructureRenewApplicationAttachment.InfrastructureRenewApplicationCode = obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentCode = obj_InfrastructureRenewApplication.RenewProLocSitePlanAttachmentCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentName = txtSitePlanAttachment.Text;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblMessageSitePlan.Text = "File Upload Success";
                            lblMessageSitePlan.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageSitePlan.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                            lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                        }

                        lblMessageSitePlan.ForeColor = Color.Green;
                        txtSitePlanAttachment.Text = "";
                        BindInfrastructureRenewApplicationAttachmentSitePlanDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        lblMessageCertifiedRevenueSketch.Text = "";
                        lblMessageReasonForNotApplyingBefore.Text = "";
                        lblMessageGroundwaterAvailability.Text = "";
                        lblMessageRainwaterHarvesting.Text = "";
                        lblMessageExistingNOC.Text = "";
                        lblMessageUndertaking.Text = "";
                        lblMessageWaterRequirement.Text = "";

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
                    lblMessageSitePlan.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
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
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
                arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplicationForNoLimit.GetBharatKoshRecieptAttachmentList();

                if (arr_InfrastructureRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_insertInfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();
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
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadBharatKoshReciept.PostedFile);
                                        obj_insertInfrastructureRenewApplicationAttachment.ContentType = FileUploadBharatKoshReciept.PostedFile.ContentType;
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = FileUploadBharatKoshReciept.FileName;
                                        obj_insertInfrastructureRenewApplicationAttachment.FileExtension = str_ext;
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
                        obj_insertInfrastructureRenewApplicationAttachment.InfrastructureRenewApplicationCode = obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentCode = obj_InfrastructureRenewApplication.InfraStructureRenewBharatKoshRecieptAttachmentCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentName = txtBharatKoshReciept.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageBharatKoshReciept.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                            lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;

                        }

                        lblMessageBharatKoshReciept.ForeColor = Color.Green;
                        lblMessageBharatKoshReciept.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                        txtBharatKoshReciept.Text = "";
                        BindInfrastructureRenewApplicationAttachmentBharatKoshRecieptDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

                        lblMessageExtra.Text = "";
                        lblMessageGroundwaterAvailability.Text = "";
                        lblMessageRainwaterHarvesting.Text = "";

                        lblMessageUndertaking.Text = "";
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
    protected void btnUploadProofOfOwnershipOfLand_Click(object sender, EventArgs e)
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
                strActionName = "ProofOfOwnershipOfLand";
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_infrastructureRenewApplicationAttachmentList;
                arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplicationForNoLimit.GetProofOwnershipLandAttachmentList();

                if (arr_infrastructureRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileName = "";
                    string str_newFileNameWithPath = "";

                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_insertInfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_infrastructureNewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();

                    byte[] buffer = new byte[1];
                    try
                    {


                        if (FileUploadProofOfOwnershipOfLand.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadProofOfOwnershipOfLand.PostedFile.FileName).ToLower();
                            str_fname = FileUploadProofOfOwnershipOfLand.FileName;

                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadProofOfOwnershipOfLand.PostedFile))
                                {
                                    if (FileUploadProofOfOwnershipOfLand.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadProofOfOwnershipOfLand.PostedFile);
                                        obj_insertInfrastructureRenewApplicationAttachment.ContentType = FileUploadProofOfOwnershipOfLand.PostedFile.ContentType;
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = FileUploadProofOfOwnershipOfLand.FileName;
                                        obj_insertInfrastructureRenewApplicationAttachment.FileExtension = str_ext;

                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageUndertaking.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageUndertaking.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageUndertaking.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }

                        else
                        {
                            lblMessageUndertaking.Text = "Please select a file..!!";
                            lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertInfrastructureRenewApplicationAttachment.InfrastructureRenewApplicationCode = obj_infrastructureNewApplication.InfrastructureRenewApplicationCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentCode = obj_infrastructureNewApplication.ProofOwnershipLandAttCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentName = txtProofOfOwnershipOfLand.Text;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageUndertaking.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                            lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        lblProofofownershipoflandofsize200sqm.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                        lblProofofownershipoflandofsize200sqm.ForeColor = Color.Green;
                        txtProofOfOwnershipOfLand.Text = "";
                        BindProofOfOwnershipAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

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
                    lblMessageUndertaking.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void btnUploadProofofownershipLeaseoftanker_Click(object sender, EventArgs e)
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
                strActionName = "ProofofownershipLeaseoftanker";
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_infrastructureRenewApplicationAttachmentList;
                arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplicationForNoLimit.GetProofOwnershipTankerAttachmentList();

                if (arr_infrastructureRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileName = "";
                    string str_newFileNameWithPath = "";
                   
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_insertInfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_infrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();
                    

                    byte[] buffer = new byte[1];
                    try
                    {


                        if (FileUploadProofofownershipLeaseoftanker.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadProofofownershipLeaseoftanker.PostedFile.FileName).ToLower();
                            str_fname = FileUploadProofofownershipLeaseoftanker.FileName;

                            if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadProofofownershipLeaseoftanker.PostedFile))
                                {
                                    if (FileUploadProofofownershipLeaseoftanker.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadProofofownershipLeaseoftanker.PostedFile);
                                        obj_insertInfrastructureRenewApplicationAttachment.ContentType = FileUploadProofofownershipLeaseoftanker.PostedFile.ContentType;
                                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = FileUploadProofofownershipLeaseoftanker.FileName;
                                        obj_insertInfrastructureRenewApplicationAttachment.FileExtension = str_ext;

                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblProofofownershipLeaseTanker.Text = "File can not upload. It has more than 5 MB size";
                                        lblProofofownershipLeaseTanker.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblProofofownershipLeaseTanker.Text = "Not a valid file!!..Select an other file!!";
                                    lblProofofownershipLeaseTanker.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblProofofownershipLeaseTanker.Text = "Not a valid file!!..Select an other file!!";
                                lblProofofownershipLeaseTanker.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }

                        else
                        {
                            lblProofofownershipLeaseTanker.Text = "Please select a file..!!";
                            lblProofofownershipLeaseTanker.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        obj_insertInfrastructureRenewApplicationAttachment.InfrastructureRenewApplicationCode = obj_infrastructureRenewApplication.InfrastructureRenewApplicationCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentCode = obj_infrastructureRenewApplication.ProofOwnershipTankerAttCode;
                        obj_insertInfrastructureRenewApplicationAttachment.AttachmentName = txtProofofownershipLeaseoftanker.Text;
                        //  obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_restPath;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertInfrastructureRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblProofofownershipLeaseTanker.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                            lblProofofownershipLeaseTanker.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        lblProofofownershipLeaseTanker.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                        lblProofofownershipLeaseTanker.ForeColor = Color.Green;
                        txtProofofownershipLeaseoftanker.Text = "";
                        BindProofofownershipLeaseoftankerAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

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
                    lblMessageUndertaking.Text = "Maximum number of files to be uploaded is 5";
                    lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    #endregion

    #region Private
    private void clearMessage()
    {
        lblMessageExistingNOC.Text = "";
        txtExistingNOC.Text = "";

        lblMessageRainwaterHarvesting.Text = "";
        txtRainwaterHarvesting.Text = "";

        lblMessageAffidavit.Text = "";
        txtAffidavit.Text = "";

        lblMessageWaterAuditReport.Text = "";
        txtWaterAuditReport.Text = "";

        lblMessageSourceofAvailabilityofSurfaceWater.Text = "";
        txtSourceofAvailabilityofSurfaceWater.Text = "";

        lblMessageBharatKoshReciept.Text = "";
        txtBharatKoshReciept.Text = "";

        lblMessageAplicationSignatureSeal.Text = "";
        txtApplicationSignatureSeal.Text = "";
        lblMessageExtra.Text = "";
        txtExtraAttachment.Text = "";

        lblMessageSitePlan.Text = "";
        lblMessageCertifiedRevenueSketch.Text = "";
        lblMessageReasonForNotApplyingBefore.Text = "";

        lblMessageWaterRequirement.Text = "";
        lblMessageGroundwaterAvailability.Text = "";

        lblMessageUndertaking.Text = "";
        lblMessageReferralLetter.Text = "";


    }

    //private void BindInfrastructureRenewApplicationComplianceConditionNOCOther()
    //{
    //    try
    //    {
    //        InfrastructureRenewSADComplianceConditionNOCOther obj_InfrastructureRenewSADComplianceConditionNOCOther = new InfrastructureRenewSADComplianceConditionNOCOther();
    //        int int_status = 0;
    //        if (lblInfrastructureApplicationCodeFrom.Text != "" && NOCAPExternalUtility.IsNumeric(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text)))
    //        {
    //            obj_InfrastructureRenewSADComplianceConditionNOCOther.InfrastructureRenewApplicationCode = Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text);

    //            int_status = obj_InfrastructureRenewSADComplianceConditionNOCOther.GetList(InfrastructureRenewSADComplianceConditionNOCOther.SortingField.NoSorting);

    //            InfrastructureRenewSADComplianceConditionNOCOther[] arr_InfrastructureRenewSADComplianceConditionNOCOther;
    //            arr_InfrastructureRenewSADComplianceConditionNOCOther = obj_InfrastructureRenewSADComplianceConditionNOCOther.InfrastructureRenewSADComplianceConditionNOCOtherCollection;

    //            if ((int_status == 1))
    //            {
    //                gvINFRenewComplianceConditionNOCOther.DataSource = arr_InfrastructureRenewSADComplianceConditionNOCOther;
    //                gvINFRenewComplianceConditionNOCOther.DataBind();
    //            }
    //            else
    //            {
    //                lblMessage.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewSADComplianceConditionNOCOther.CustumMessage);
    //            }
    //        }
    //        else
    //        {
    //            Response.Redirect("~/ExternalErrorPage.aspx", false);
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        Response.Redirect("~/ExternalErrorPage.aspx", false);
    //    }
    //}

    private void BindInfrastructureRenewComplianceConditionNOCOtherAttachmentDetails()
    {
        try
        {
            foreach (GridViewRow row in gvINFRenewComplianceConditionNOCOther.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    GridView gvCompCondNOCAttOther = (GridView)row.FindControl("gvCompCondNOCAttachmentOther");

                    Label lblCompCondNOCOtherSerialNumber = (Label)row.FindControl("lblCompCondNOCOtherSerialNumber");

                    //// Inner Grid View Binding

                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOCOther obj_InfrastructureRenewSADComplianceConditionNOCOther = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOCOther(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text), Convert.ToInt32(lblCompCondNOCOtherSerialNumber.Text));

                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentListForCount;

                    arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplication.GetInfrastructureComplianceConditionNOCOtherAttachmentList(obj_InfrastructureRenewSADComplianceConditionNOCOther.ComplianceConditionAttachmentCode, NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment.SortingFieldForAttachment.NoSorting);

                    arr_InfrastructureRenewApplicationAttachmentListForCount = obj_InfrastructureRenewApplication.GetInfrastructureComplianceConditionNOCOtherAttachmentList(NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment.SortingFieldForAttachment.NoSorting);
                    lblComplianceReport2.Visible = false;
                    lblComplianceReport.Text = "Compliance Report - Other (" + arr_InfrastructureRenewApplicationAttachmentListForCount.Length.ToString() + ")";
                    gvCompCondNOCAttOther.DataSource = arr_InfrastructureRenewApplicationAttachmentList;
                    gvCompCondNOCAttOther.DataBind();
                }
            }
        }
        catch (Exception)
        { }
    }

    private void BindInfrastructureRenewApplicationAttachmentBharatKoshRecieptDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_infrastructureRenewApplicationAttachmentList;
            arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplication.GetBharatKoshRecieptAttachmentList();
            gvBharatKoshReciept.DataSource = arr_infrastructureRenewApplicationAttachmentList;
            gvBharatKoshReciept.DataBind();
            //decimal decTotalGroundWaterRequirement = Convert.ToDecimal(obj_infrastructureRenewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement + (obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));
           
            lblBharatKosh.Text = HttpUtility.HtmlEncode("Bharat Kosh Reciept Attachment (" + arr_infrastructureRenewApplicationAttachmentList.Length.ToString() + ")");
            hdnBharatKosh.Value = arr_infrastructureRenewApplicationAttachmentList.Length.ToString();
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindInfrastructureRenewApplicationAttachmentSignedDocDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_infrastructureRenewApplicationAttachmentList;
            arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplication.GetSignedDocAttachmentList();
            gvApplicationSignatureSeal.DataSource = arr_infrastructureRenewApplicationAttachmentList;
            gvApplicationSignatureSeal.DataBind();
            lblSigneddoc2.Visible = true;
            lblSigneddoc.Text = HttpUtility.HtmlEncode("Aplication with Signature and Seal Attachment (" + arr_infrastructureRenewApplicationAttachmentList.Length.ToString() + ")");
            hdnSigneddoc.Value = arr_infrastructureRenewApplicationAttachmentList.Length.ToString();
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
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

    private void BindInfrastructureRenewApplicationAttachmentSitePlanDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
            arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplication.GetSitePlanAttachmentList();
            lblSitePlan2.Visible = true;
            lblSitePlan.Text = "Site Plan (" + arr_InfrastructureRenewApplicationAttachmentList.Length.ToString() + ")";
            gvSitePlan.DataSource = arr_InfrastructureRenewApplicationAttachmentList;
            gvSitePlan.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void BindInfrastructureRenewApplicationAttachmentCertifiedRevenueSketchDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
            arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplication.GetCertifiedRevenueSketAttachmentList();
            lblCertifiedRevenueSketch2.Visible = false;
            lblCertifiedRevenueSketch.Text = "Certified Revenue Sketch (" + arr_InfrastructureRenewApplicationAttachmentList.Length.ToString() + ")";
            gvCertifiedRevenueSketch.DataSource = arr_InfrastructureRenewApplicationAttachmentList;
            gvCertifiedRevenueSketch.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void BindInfrastructureRenewApplicationAttachmentReasonForNotApplyingBeforeDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
            arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplication.GetReasonForNotApplyingBeforeAttachmentList();
            lblReason2.Visible = false;
            lblReason.Text = "Reason For Not Applying Renewal Before (" + arr_InfrastructureRenewApplicationAttachmentList.Length.ToString() + ")";
            gvReasonForNotApplyingBefore.DataSource = arr_InfrastructureRenewApplicationAttachmentList;
            gvReasonForNotApplyingBefore.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindInfrastructureRenewApplicationAttachmentExisingNOCDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
            arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplication.GetExistingNOCAttachmentList();
            lblExistingNOC2.Visible = true;
            hdnExistingNOC.Value = arr_InfrastructureRenewApplicationAttachmentList.Length.ToString();
            lblExistingNOC.Text = "Existing NOC (" + arr_InfrastructureRenewApplicationAttachmentList.Length.ToString() + ")";
            gvExistingNOC.DataSource = arr_InfrastructureRenewApplicationAttachmentList;
            gvExistingNOC.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindInfrastructureRenewApplicationAttachmentWaterRequirementDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
            arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplication.GetWaterRequrementAttachmentList();
            lblEnclose2.Visible = false;
            lblEnclose.Text = "Water Requirement (" + arr_InfrastructureRenewApplicationAttachmentList.Length.ToString() + ")";
            gvWaterRequirement.DataSource = arr_InfrastructureRenewApplicationAttachmentList;
            gvWaterRequirement.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindInfrastructureRenewApplicationAttachmentGroundwaterAvailabilityDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
            arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplication.GetGroundwaterAvailabilityAttachmentList();
            lblGroundwaterAvailability2.Visible = false;
            lblGroundwaterAvailability.Text = "Groundwater Availability (" + arr_InfrastructureRenewApplicationAttachmentList.Length.ToString() + ")";
            gvGroundwaterAvailability.DataSource = arr_InfrastructureRenewApplicationAttachmentList;
            gvGroundwaterAvailability.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindInfrastructureRenewApplicationAttachmentRainwaterHarvestingDetails(long lngA_ApplicationCode)
    {
        try
        {


            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
            arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplication.GetRainwaterHarvestingAttachmentList();
            hdnRainwaterHarvesting.Value = arr_InfrastructureRenewApplicationAttachmentList.Length.ToString();
            lblRainwaterHarvesting2.Visible = false;
            lblRainwaterHarvesting.Text = "Rainwater Harvesting (" + arr_InfrastructureRenewApplicationAttachmentList.Length.ToString() + ")";
            gvRainwaterHarvesting.DataSource = arr_InfrastructureRenewApplicationAttachmentList;
            gvRainwaterHarvesting.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindInfrastructureRenewApplicationAttachmentUndertakingDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
            arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplication.GetUndertakingAttachmentList();
            lblAuthorization2.Visible = false;
            lblAuthorization.Text = "Authorization (" + arr_InfrastructureRenewApplicationAttachmentList.Length.ToString() + ")";
            gvUndertaking.DataSource = arr_InfrastructureRenewApplicationAttachmentList;
            gvUndertaking.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindInfrastructureRenewApplicationAttachmentExtraDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;

            arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplication.GetExtraAttachmentList();
            lblExtraAttachment2.Visible = false;
            lblExtraAttachment.Text = "Extra Attachment (" + arr_InfrastructureRenewApplicationAttachmentList.Length.ToString() + ")";
            gvExtra.DataSource = arr_InfrastructureRenewApplicationAttachmentList;
            gvExtra.DataBind();
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindInfrastructureRenewComplianceConditionNOCAttachmentDetails(long lng_ApplicationCode)
    {
        try
        {
            foreach (GridViewRow row in gvINFRenewComplianceConditionNOC.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    GridView gvCompCondNOCAtt = (GridView)row.FindControl("gvCompCondNOCAttachment");

                    Label lblCompCondNOCCode = (Label)row.FindControl("lblCompCondNOCCode");

                    //// Inner Grid View Binding

                    InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

                    InfrastructureRenewSADComplianceConditionNOC obj_InfrastructureRenewSADComplianceConditionNOC = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOC(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text), Convert.ToInt32(lblCompCondNOCCode.Text));
                    InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;
                    InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentListForCount;

                    arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplication.GetInfrastructureComplianceConditionNOCAttachmentList(obj_InfrastructureRenewSADComplianceConditionNOC.ComplianceConditionAttachmentCode, NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment.SortingFieldForAttachment.NoSorting);

                    arr_InfrastructureRenewApplicationAttachmentListForCount = obj_InfrastructureRenewApplication.GetInfrastructureComplianceConditionNOCAttachmentList(NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment.SortingFieldForAttachment.NoSorting);
                    lblComplianceReport2.Visible = false;
                    lblComplianceReport.Text = "Compliance Report (" + arr_InfrastructureRenewApplicationAttachmentListForCount.Length.ToString() + ")";
                    gvCompCondNOCAtt.DataSource = arr_InfrastructureRenewApplicationAttachmentList;
                    gvCompCondNOCAtt.DataBind();
                }
            }
        }
        catch (Exception)
        { }
    }

    private void BindInfrastructureRenewApplicationComplianceConditionNOC(long lngA_ApplicationCode)
    {
        try
        {
            int intStatus = 0;
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOCExt obj_InfrastructureRenewSADComplianceConditionNOCExt = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOCExt();

            intStatus = obj_InfrastructureRenewSADComplianceConditionNOCExt.GetComplianceConditionListForApplicationCodeExt(lngA_ApplicationCode, NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOCExt.SortingField.ComplianceConditionNOCDescription);
            if (intStatus == 1)
            {

                gvINFRenewComplianceConditionNOC.DataSource = obj_InfrastructureRenewSADComplianceConditionNOCExt.InfrastructureRenewSADComplianceConditionNOCCollectionExt;
                gvINFRenewComplianceConditionNOC.DataBind();

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void BindProofOfOwnershipAttachmentDetails(long lngA_ApplicationCode)
    {

        try
        {
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj.FirstApplicationCode);
       
            NOCAP.BLL.Master.ApplicationTypeCategory obj2 = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_InfrastructureNewApplication.ApplicationTypeCode, obj_InfrastructureNewApplication.ApplicationTypeCategoryCode);

            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvProofOfOwnershipOfLand, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblProofofownershipoflandofsize200sqmormore.Text = HttpUtility.HtmlEncode("Proof Of Ownership of Land Attachment (" + AttCount.ToString() + ")");

            if (obj2.ApplicationTypeCategoryDesc == "Bulk Water Suppliers")
                lblProofofownershipofland.Visible = true;
            else
                lblProofofownershipofland.Visible = false;

        

        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void BindProofofownershipLeaseoftankerAttachmentDetails(long lngA_ApplicationCode)
    {

        try
        {

            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj.FirstApplicationCode);

            NOCAP.BLL.Master.ApplicationTypeCategory obj2 = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_InfrastructureNewApplication.ApplicationTypeCode, obj_InfrastructureNewApplication.ApplicationTypeCategoryCode);

            decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvProofofownershipLeaseoftanker, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref AttCount);
            lblProofofownershipLeaseoftanker.Text = HttpUtility.HtmlEncode("Proof of Ownership / lease of Tanker Attachment (" + AttCount.ToString() + ")");
            if (obj2.ApplicationTypeCategoryDesc == "Bulk Water Suppliers")
                lblProofofownershipLease.Visible = true;
            else
                lblProofofownershipLease.Visible = false;
           
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }


    #endregion    

    #region RowDeleting
    protected void gvWaterRequirement_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Water Requirement";
                long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(gvWaterRequirement.DataKeys[e.RowIndex].Values["InfrastructureRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvWaterRequirement.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvWaterRequirement.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_InfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment(lng_InfrastructureRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                //string str_fullPath = str_startPathFromConfig + obj_InfrastructureRenewApplicationAttachment.AttachmentPath;

                if (obj_InfrastructureRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Success";
                    lblMessageWaterRequirement.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                    lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    BindInfrastructureRenewApplicationAttachmentWaterRequirementDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed";
                    lblMessageWaterRequirement.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                    lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
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
                long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(gvReasonForNotApplyingBefore.DataKeys[e.RowIndex].Values["InfrastructureRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvReasonForNotApplyingBefore.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvReasonForNotApplyingBefore.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_InfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment(lng_InfrastructureRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                //string str_fullPath = str_startPathFromConfig + obj_InfrastructureRenewApplicationAttachment.AttachmentPath;

                if (obj_InfrastructureRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Deleted Success";
                    lblMessageReasonForNotApplyingBefore.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                    lblMessageReasonForNotApplyingBefore.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    BindInfrastructureRenewApplicationAttachmentReasonForNotApplyingBeforeDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Deleting Failed";
                    lblMessageReasonForNotApplyingBefore.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
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
                strActionName = "File Delete Extra";
                long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(gvExtra.DataKeys[e.RowIndex].Values["InfrastructureRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvExtra.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvExtra.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_InfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment(lng_InfrastructureRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);


                //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                //string str_fullPath = str_startPathFromConfig + obj_InfrastructureRenewApplicationAttachment.AttachmentPath;


                if (obj_InfrastructureRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Success";
                    lblMessageExtra.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                    lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}

                    BindInfrastructureRenewApplicationAttachmentExtraDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

                }
                else
                {
                    strStatus = "File Delete Failed";
                    lblMessageExtra.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                    lblMessageExtra.ForeColor = System.Drawing.Color.Red;

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
                    BindAffidavitAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
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
                    BindWaterAuditReportAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
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
                    BindSourceWaterNonAvailabilityCertificateAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }
    protected void gvUndertaking_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Undertaking";
                long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(gvUndertaking.DataKeys[e.RowIndex].Values["InfrastructureRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvUndertaking.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvUndertaking.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_InfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment(lng_InfrastructureRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);

                //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                //string str_fullPath = str_startPathFromConfig + obj_InfrastructureRenewApplicationAttachment.AttachmentPath;

                if (obj_InfrastructureRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Success";
                    lblMessageUndertaking.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                    lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    BindInfrastructureRenewApplicationAttachmentUndertakingDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed";
                    lblMessageUndertaking.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                    lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
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
    protected void gvSitePlan_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "Delete File Site Plan";
                long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(gvSitePlan.DataKeys[e.RowIndex].Values["InfrastructureRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvSitePlan.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvSitePlan.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_InfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment(lng_InfrastructureRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);

                //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                //string str_fullPath = str_startPathFromConfig + obj_InfrastructureRenewApplicationAttachment.AttachmentPath;


                if (obj_InfrastructureRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Deleted Success";
                    lblMessageSitePlan.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                    lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //  File.Delete(str_fullPath);
                    //}
                    BindInfrastructureRenewApplicationAttachmentSitePlanDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Deleted Failed";
                    lblMessageSitePlan.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                    lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
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
                long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(gvExistingNOC.DataKeys[e.RowIndex].Values["InfrastructureRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvExistingNOC.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvExistingNOC.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_InfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment(lng_InfrastructureRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                //string str_fullPath = str_startPathFromConfig + obj_InfrastructureRenewApplicationAttachment.AttachmentPath;

                if (obj_InfrastructureRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Deleted Success";
                    lblMessageExistingNOC.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                    lblMessageExistingNOC.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    BindInfrastructureRenewApplicationAttachmentExisingNOCDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Deleting Failed";
                    lblMessageExistingNOC.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
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
    protected void gvCertifiedRevenueSketch_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Certificate of Revenue Sketch";
                long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(gvCertifiedRevenueSketch.DataKeys[e.RowIndex].Values["InfrastructureRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvCertifiedRevenueSketch.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvCertifiedRevenueSketch.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_InfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment(lng_InfrastructureRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                //string str_fullPath = str_startPathFromConfig + obj_InfrastructureRenewApplicationAttachment.AttachmentPath;

                if (obj_InfrastructureRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Deleted Success";
                    lblMessageCertifiedRevenueSketch.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                    lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    BindInfrastructureRenewApplicationAttachmentCertifiedRevenueSketchDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Deleting Failed";
                    lblMessageCertifiedRevenueSketch.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                    lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
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
                long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(gvBharatKoshReciept.DataKeys[e.RowIndex].Values["InfrastructureRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvBharatKoshReciept.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvBharatKoshReciept.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_infrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment(lng_InfrastructureRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                if (obj_infrastructureRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Success";
                    lblMessageBharatKoshReciept.Text = obj_infrastructureRenewApplicationAttachment.CustumMessage;
                    lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
                    BindInfrastructureRenewApplicationAttachmentBharatKoshRecieptDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed";
                    lblMessageBharatKoshReciept.Text = obj_infrastructureRenewApplicationAttachment.CustumMessage;
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
    protected void gvRainwaterHarvesting_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Rainwater Harvesting";
                long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(gvRainwaterHarvesting.DataKeys[e.RowIndex].Values["InfrastructureRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvRainwaterHarvesting.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvRainwaterHarvesting.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_InfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment(lng_InfrastructureRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);

                //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                //string str_fullPath = str_startPathFromConfig + obj_InfrastructureRenewApplicationAttachment.AttachmentPath;

                if (obj_InfrastructureRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Success";
                    lblMessageRainwaterHarvesting.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                    lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    BindInfrastructureRenewApplicationAttachmentRainwaterHarvestingDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed";
                    lblMessageRainwaterHarvesting.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                    lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
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
    protected void gvApplicationSignatureSeal_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(gvApplicationSignatureSeal.DataKeys[e.RowIndex].Values["InfrastructureRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvApplicationSignatureSeal.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvApplicationSignatureSeal.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_infrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment(lng_InfrastructureRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                if (obj_infrastructureRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Success";
                    lblMessageAplicationSignatureSeal.Text = obj_infrastructureRenewApplicationAttachment.CustumMessage;
                    lblMessageAplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;
                    BindInfrastructureRenewApplicationAttachmentSignedDocDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed";
                    lblMessageAplicationSignatureSeal.Text = obj_infrastructureRenewApplicationAttachment.CustumMessage;
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
    protected void gvGroundwaterAvailability_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Ground Water Availability";
                long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(gvGroundwaterAvailability.DataKeys[e.RowIndex].Values["InfrastructureRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvGroundwaterAvailability.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvGroundwaterAvailability.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_InfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment(lng_InfrastructureRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);

                //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                //string str_fullPath = str_startPathFromConfig + obj_InfrastructureRenewApplicationAttachment.AttachmentPath;

                if (obj_InfrastructureRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Success";
                    lblMessageGroundwaterAvailability.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                    lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    BindInfrastructureRenewApplicationAttachmentGroundwaterAvailabilityDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed";
                    lblMessageGroundwaterAvailability.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                    lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
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
    protected void gvProofOfOwnershipOfLand_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                if (DeleteAttchment((GridView)sender, e, lblProofofownershipoflandofsize200sqm, strActionName) == 1)
                    BindProofOfOwnershipAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

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

    protected void gvProofofownershipLeaseoftanker_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Proof of ownership/Lease of tanker";
                if (DeleteAttchment((GridView)sender, e, lblProofofownershipLeaseTanker, strActionName) == 1)
                    BindProofofownershipLeaseoftankerAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

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
                    long lng_infrastructureRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.INFRenewSADAppDownloadFiles(lng_infrastructureRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

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

    protected void lbtnViewUndertakingFile_Click(object sender, CommandEventArgs e)
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
                    long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.INFRenewSADAppDownloadFiles(lng_InfrastructureRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                    long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.INFRenewSADAppDownloadFiles(lng_InfrastructureRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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


    protected void lbtnViewCertifiedRevenueSketchFile_Click(object sender, CommandEventArgs e)
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
                    long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.INFRenewSADAppDownloadFiles(lng_InfrastructureRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                    long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.INFRenewSADAppDownloadFiles(lng_InfrastructureRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                    long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.INFRenewSADAppDownloadFiles(lng_InfrastructureRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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


    protected void lnkInfrastructureCompCondNOCAttachmentView_Click(object sender, CommandEventArgs e)
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
                    long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.INFRenewSADAppDownloadFiles(lng_InfrastructureRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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

    protected void lnkInfrastructureCompCondNOCAttachmentViewOther_Click(object sender, CommandEventArgs e)
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
                    long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.INFRenewSADAppDownloadFiles(lng_InfrastructureRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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

    protected void lbtnExtraViewFile_Click(object sender, CommandEventArgs e)
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
                    long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);


                    NOCAPExternalUtility.INFRenewSADAppDownloadFiles(lng_InfrastructureRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

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

    protected void lbtnNonPollutingViewFile_Click(object sender, CommandEventArgs e)
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
                    long lng_InfrastructureNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.INFRenewSADAppDownloadFiles(lng_InfrastructureNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

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

    protected void lbtnViewGroundwaterAvailabilityFile_Click(object sender, CommandEventArgs e)
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
                    long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.INFRenewSADAppDownloadFiles(lng_InfrastructureRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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


    protected void lbtnViewRainwaterHarvestingFile_Click(object sender, CommandEventArgs e)
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
                    long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.INFRenewSADAppDownloadFiles(lng_InfrastructureRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                    long lng_infrastructureRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.INFRenewSADAppDownloadFiles(lng_infrastructureRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

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
                    long lng_infrastructureRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.INFRenewSADAppDownloadFiles(lng_infrastructureRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

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
    private void BindGridView(GridView gv, long lngA_ApplicationCode, ref decimal decTotalGroundWaterRequirement, ref int AttCount)
    {
        InfrastructureRenewSADApplication obj_infrastructureRenewApplication = new InfrastructureRenewSADApplication(lngA_ApplicationCode);
        InfrastructureRenewSADApplicationAttachment[] arr_infrastructureRenewApplicationAttachmentList = null;

        if (gv.ID == "gvAffidavit")
            arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplication.GetAffidavitNOCCondiAttachmentList();
        else if (gv.ID == "gvWaterAuditReport")
            arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplication.GetWaterAuditReportAttachmentList();
        else if (gv.ID == "gvSourceofAvailabilityofSurfaceWater")
            arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplication.GetCertiNonAvaAttachmentList();
        else if (gv.ID == "gvProofOfOwnershipOfLand")
            arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplication.GetProofOwnershipLandAttachmentList();
        else if (gv.ID == "gvProofofownershipLeaseoftanker")
            arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplication.GetProofOwnershipTankerAttachmentList();
        // else if (gv.ID == "gvRainwaterHarvesting")
        //  arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplication.GetRainwaterHarvestingAttachmentList();
        // else if (gv.ID == "gvImpactReportOCS")
        //   arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplication.GetImpactAssOCSAttachmentList();
        // else if (gv.ID == "gvReferralLetterAttachment")
        //   arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplication.GetCpoyOfReferralLetterAttachmentList();
        //else if (gv.ID == "gvMSME")
        //{
        //    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetMSMEAttachmentList();
        //    switch (obj_industrialNewApplication.MSME)
        //    {
        //        case NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.MSMEYesNo.Yes:
        //            lblMSME2.Visible = true;
        //            break;
        //        case NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.MSMEYesNo.No:
        //            lblMSME2.Visible = false;
        //            break;
        //    }
        //}
        // else if (gv.ID == "gvAffidavitOtherThanMSME")
        //   arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplication.GetAffidavitOtherMSMEAttachmentList();
        //else if (gv.ID == "gvWetlandArea")
        //{
        //    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetWetlandAreaAttachmentList();
        //    switch (obj_industrialNewApplication.WetLandArea)
        //    {
        //        case NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.WetLandAreaYesNo.Yes:
        //            lblWetlandArea2.Visible = true;
        //            break;
        //        case NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.WetLandAreaYesNo.No:
        //            lblWetlandArea2.Visible = false;
        //            break;
        //    }
        //}
        // else if (gv.ID == "gvBharatKoshReciept")
        //arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplication.GetBharatKoshRecieptAttachmentList();

        // else if (gv.ID == "gvSigneddoc")
        // arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplication.GetSigneddocAttachmentList();
        // else if (gv.ID == "gvPenalty")
        //   arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplication.GetPenaltyAttachmentList();

        // else if (gv.ID == "gvExtra")
        //  arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplication.GetExtraAttachmentList();

        gv.DataSource = arr_infrastructureRenewApplicationAttachmentList;
        gv.DataBind();
        AttCount = arr_infrastructureRenewApplicationAttachmentList.Length;
        decTotalGroundWaterRequirement = Convert.ToDecimal(obj_infrastructureRenewApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist + obj_infrastructureRenewApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional);// + (obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));
    }
    private int DeleteAttchment(GridView gv, GridViewDeleteEventArgs e, Label lblMessage, string str_ActionName)
    {
        try
        {
            long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["InfrastructureRenewApplicationCode"]);
            int int_AttachmentCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["AttachmentCode"]);
            int int_AttachmentCodeSerialNumber = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
            lblMessage.ForeColor = System.Drawing.Color.Red;
            InfrastructureRenewSADApplicationAttachment obj_InfrastructureRenewApplicationAttachment = new InfrastructureRenewSADApplicationAttachment(lng_InfrastructureRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
            if (obj_InfrastructureRenewApplicationAttachment.Delete() == 1)
            {
                lblMessage.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                strStatus = "File Delete Success";
                return 1;
            }
            else
            {
                lblMessage.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
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
            Server.Transfer("OtherDetails.aspx");
        }
    }

    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
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
                clearMessage();
                strActionName = "Submit Application";
                long lng_submittedApplicationCode = 0;
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));

                try
                {
                    string ErrorMessage = string.Empty;
                   
                    if (Convert.ToInt32(hdnExistingNOC.Value) < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Existing NOC" : ErrorMessage + ",Existing NOC"; }


                    if (lblRainwaterHarvesting2.Visible)
                    {
                        if (Convert.ToInt32(hdnRainwaterHarvesting.Value) < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Details of Rainwater Harvesting / Artificial Recharge Measures" : ErrorMessage + ",Details of Rainwater Harvesting / Artificial Recharge Measures"; }
                    }
                    

                    if (lblAffidavit2.Visible) { if (Convert.ToInt32(hdnAffidavit.Value) < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Affidavit of Compliance of NOC Condition" : ErrorMessage + ",Affidavit of Compliance of NOC Condition"; } }
                    if (lblWaterAuditReport2.Visible) { if (Convert.ToInt32(hdnWaterAuditReport.Value) < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Water Audit Report" : ErrorMessage + ",Water Audit Report"; } }
                    if (lblSourceWaterAvailability2.Visible) { if (Convert.ToInt32(hdnSourceWaterAvailability.Value) < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Source Water Non-availability Certificate" : ErrorMessage + ",Source Water Non-availability Certificate"; } }

                   
                    if (lblSigneddoc2.Visible)
                    {
                        if (Convert.ToInt32(hdnSigneddoc.Value) < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Aplication with Signature and Seal" : ErrorMessage + ",Aplication with Signature and Seal"; }


                    }
                    if (!string.IsNullOrEmpty(ErrorMessage))
                    {
                        lblMessage.Text = ErrorMessage + " Attachments are Mandatory.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    lngInfRenewSubmitAppCode = lng_submittedApplicationCode;
                    Server.Transfer("INFRenewReadyToSubmit.aspx");
                }
                catch (ThreadAbortException)
                {


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
                    obj_InfrastructureRenewApplication.Dispose();
                }
            }
        }
    }

    #region RowDataBound
    protected void gvINFRenewComplianceConditionNOC_RowDataBound(object sender, GridViewRowEventArgs e)
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

                    BindInfrastructureRenewComplianceConditionNOCAttachmentDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
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
                Label lblMsgINFCompCondNOCAttachmentOtherDelete = null;
                if (e.CommandName == "DeleteFile")
                {
                    strActionName = "Delete File INFCompCondNOCAttachmentOther";


                    GridViewRow gvCompCondNOCAttachmentOtherRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    GridView gvCompCondNOCAttachmentOther = (GridView)(gvCompCondNOCAttachmentOtherRow.Parent.Parent);
                    GridViewRow gvINFRenewComplianceConditionNOCOtherRow = (GridViewRow)(gvCompCondNOCAttachmentOther.NamingContainer);

                    int b = gvINFRenewComplianceConditionNOCOtherRow.RowIndex;

                    foreach (GridViewRow row in gvINFRenewComplianceConditionNOCOther.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            Label lblMsgINFCompCondNOCAttachmentOtherDelete1 = (Label)row.FindControl("lblMessageINFCompCondNOCAttachmentOtherDelete");
                            lblMsgINFCompCondNOCAttachmentOtherDelete1.Text = "";
                            Label lblMsgINFCompCondAttachmentOther = (Label)row.FindControl("lblMessageINFCompCondAttachmentOther");
                            lblMsgINFCompCondAttachmentOther.Text = "";

                            TextBox txtAttachmentNameOther = (TextBox)row.FindControl("txtAttachmentNameCompCondNOCOther");
                            txtAttachmentNameOther.Text = "";


                            if (row.RowIndex == b)
                            {
                                lblMsgINFCompCondNOCAttachmentOtherDelete = (Label)gvINFRenewComplianceConditionNOCOther.Rows[b].FindControl("lblMessageINFCompCondNOCAttachmentOtherDelete");
                            }
                        }
                    }

                    string[] CommandArgument = e.CommandArgument.ToString().Split(',');
                    long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_AttachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_AttachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);


                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_InfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment(lng_InfrastructureRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);

                    //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                    //string str_fullPath = str_startPathFromConfig + obj_InfrastructureRenewApplicationAttachment.AttachmentPath;


                    if (obj_InfrastructureRenewApplicationAttachment.Delete() == 1)
                    {
                        strStatus = "File Deleted Success";

                        lblMsgINFCompCondNOCAttachmentOtherDelete.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                        lblMsgINFCompCondNOCAttachmentOtherDelete.ForeColor = System.Drawing.Color.Red;

                        //if (File.Exists(str_fullPath))
                        //{
                        //    File.Delete(str_fullPath);
                        //}
                        BindInfrastructureRenewComplianceConditionNOCOtherAttachmentDetails();
                    }
                    else
                    {
                        strStatus = "File Deleted Failed";
                        lblMsgINFCompCondNOCAttachmentOtherDelete.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                        lblMsgINFCompCondNOCAttachmentOtherDelete.ForeColor = System.Drawing.Color.Red;
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
    protected void gvINFRenewComplianceConditionNOCOther_RowCommand(object sender, GridViewCommandEventArgs e)
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
                Label lblMsgINFCompCondAttachmentOther = null;
                if (e.CommandName == "UploadFileForCompCondNOCOther")
                {
                    strActionName = "Upload Compliance Condition NOC - Other Attachment";

                    GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                    FileUpload FileUploadCompCondNOCAttOther = (FileUpload)row.FindControl("FileUploadCompCondNOCOther");
                    TextBox txtFileCompCondNOCAttachmentOther = (TextBox)row.FindControl("txtAttachmentNameCompCondNOCOther");
                    GridView gvCompCondNOCAttOther = (GridView)row.FindControl("gvCompCondNOCAttachmentOther");
                    Label lblCompCondNOCOtherSerialNumber = (Label)row.FindControl("lblCompCondNOCOtherSerialNumber");

                    int b = row.RowIndex;

                    foreach (GridViewRow row1 in gvINFRenewComplianceConditionNOCOther.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            Label lblMsgINFCompCondNOCAttachmentOtherDelete1 = (Label)row1.FindControl("lblMessageINFCompCondNOCAttachmentOtherDelete");
                            lblMsgINFCompCondNOCAttachmentOtherDelete1.Text = "";

                            Label lblMessageINFCompCondAttachmentOther1 = (Label)row1.FindControl("lblMessageINFCompCondAttachmentOther");
                            lblMessageINFCompCondAttachmentOther1.Text = "";

                            if (row1.RowIndex == b)
                            {
                                lblMsgINFCompCondAttachmentOther = (Label)gvINFRenewComplianceConditionNOCOther.Rows[b].FindControl("lblMessageINFCompCondAttachmentOther");
                            }
                            else
                            {
                                TextBox txtAttachmentName = (TextBox)row1.FindControl("txtAttachmentNameCompCondNOCOther");
                                txtAttachmentName.Text = "";
                            }
                        }
                    }

                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOCOther obj_InfrastructureRenewSADComplianceConditionNOCOther = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOCOther(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text), Convert.ToInt32(lblCompCondNOCOtherSerialNumber.Text));
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;

                    arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplicationForNoLimit.GetInfrastructureComplianceConditionNOCOtherAttachmentList(obj_InfrastructureRenewSADComplianceConditionNOCOther.ComplianceConditionAttachmentCode);

                    if (arr_InfrastructureRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                    {
                        try
                        {
                            string str_fname;
                            string str_ext;

                            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_insertInfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment();
                            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                            List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();

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
                                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadCompCondNOCAttOther.PostedFile);
                                            obj_insertInfrastructureRenewApplicationAttachment.ContentType = FileUploadCompCondNOCAttOther.PostedFile.ContentType;
                                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = FileUploadCompCondNOCAttOther.FileName;
                                            obj_insertInfrastructureRenewApplicationAttachment.FileExtension = str_ext;
                                        }
                                        else
                                        {
                                            strStatus = "File Upload Failed";
                                            lblMsgINFCompCondAttachmentOther.Text = "File can not upload. It has more than 5 MB size";
                                            lblMsgINFCompCondAttachmentOther.ForeColor = System.Drawing.Color.Red;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMsgINFCompCondAttachmentOther.Text = "Not a valid file!!..Select another file!!";
                                        lblMsgINFCompCondAttachmentOther.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMsgINFCompCondAttachmentOther.Text = "Not a valid file!!..Select another file!!";
                                    lblMsgINFCompCondAttachmentOther.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                lblMsgINFCompCondAttachmentOther.Text = "Please select a file..!!";
                                lblMsgINFCompCondAttachmentOther.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                            obj_insertInfrastructureRenewApplicationAttachment.InfrastructureRenewApplicationCode = obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode;
                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentCode = obj_InfrastructureRenewSADComplianceConditionNOCOther.ComplianceConditionAttachmentCode;
                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentName = txtFileCompCondNOCAttachmentOther.Text;

                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                            obj_insertInfrastructureRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                            if (obj_insertInfrastructureRenewApplicationAttachment.Add() == 1)
                            {
                                strStatus = "File Upload Success";
                                lblMsgINFCompCondAttachmentOther.Text = "File Upload Success";
                                lblMsgINFCompCondAttachmentOther.ForeColor = System.Drawing.Color.Green;

                                BindInfrastructureRenewComplianceConditionNOCOtherAttachmentDetails();
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMsgINFCompCondAttachmentOther.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                                lblMsgINFCompCondAttachmentOther.ForeColor = System.Drawing.Color.Red;

                            }

                            //lblMsgINFCompCondAttachment.ForeColor = Color.Green;
                            //lblMsgINFCompCondAttachment.Text = "";
                            txtFileCompCondNOCAttachmentOther.Text = "";
                            BindInfrastructureRenewComplianceConditionNOCOtherAttachmentDetails();
                            lblMessageCertifiedRevenueSketch.Text = "";
                            lblMessageReasonForNotApplyingBefore.Text = "";
                            lblMessageGroundwaterAvailability.Text = "";
                            lblMessageRainwaterHarvesting.Text = "";
                            lblMessageExistingNOC.Text = "";
                            lblMessageUndertaking.Text = "";
                            lblMessageWaterRequirement.Text = "";

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
                        lblMsgINFCompCondAttachmentOther.Text = "Maximum number of files to be uploaded is 5";
                        lblMsgINFCompCondAttachmentOther.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    protected void gvINFRenewComplianceConditionNOC_RowCommand(object sender, GridViewCommandEventArgs e)
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
                Label lblMsgINFCompCondAttachment = null;
                if (e.CommandName == "UploadFileForCompCondNOC")
                {
                    strActionName = "Upload Compliance Condition NOC Attachment";

                    GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                    FileUpload FileUploadCompCondNOCAtt = (FileUpload)row.FindControl("FileUploadCompCondNOC");
                    TextBox txtFileCompCondNOCAttachment = (TextBox)row.FindControl("txtAttachmentNameCompCondNOC");
                    GridView gvCompCondNOCAtt = (GridView)row.FindControl("gvCompCondNOCAttachment");
                    Label lblCompCondNOCCode = (Label)row.FindControl("lblCompCondNOCCode");
                    //  Label lblMsgINFCompCondAttachment = (Label)row.FindControl("lblMessageINFCompCondAttachment");
                    int b = row.RowIndex;

                    foreach (GridViewRow row1 in gvINFRenewComplianceConditionNOC.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            Label lblMsgINFCompCondNOCAttachmentDelete = (Label)row1.FindControl("lblMessageINFCompCondNOCAttachmentDelete");
                            lblMsgINFCompCondNOCAttachmentDelete.Text = "";
                            Label lblMsgINFCompCondNOCAttachment1 = (Label)row1.FindControl("lblMessageINFCompCondAttachment");
                            lblMsgINFCompCondNOCAttachment1.Text = "";

                            if (row1.RowIndex == b)
                            {
                                lblMsgINFCompCondAttachment = (Label)gvINFRenewComplianceConditionNOC.Rows[b].FindControl("lblMessageINFCompCondAttachment");
                            }
                            else
                            {
                                TextBox txtAttachmentName = (TextBox)row1.FindControl("txtAttachmentNameCompCondNOC");
                                txtAttachmentName.Text = "";
                            }
                        }
                    }


                    // lblMsgINFCompCondAttachment.Text = "";

                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOC obj_InfrastructureRenewSADComplianceConditionNOC = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOC(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text), Convert.ToInt32(lblCompCondNOCCode.Text));
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_InfrastructureRenewApplicationAttachmentList;

                    arr_InfrastructureRenewApplicationAttachmentList = obj_InfrastructureRenewApplicationForNoLimit.GetInfrastructureComplianceConditionNOCAttachmentList(obj_InfrastructureRenewSADComplianceConditionNOC.ComplianceConditionAttachmentCode);

                    if (arr_InfrastructureRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                    {
                        try
                        {
                            string str_fname;
                            string str_ext;

                            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_insertInfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment();
                            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                            List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment> lst_InfrastructureRenewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment>();

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
                                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadCompCondNOCAtt.PostedFile);
                                            obj_insertInfrastructureRenewApplicationAttachment.ContentType = FileUploadCompCondNOCAtt.PostedFile.ContentType;
                                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentPath = FileUploadCompCondNOCAtt.FileName;
                                            obj_insertInfrastructureRenewApplicationAttachment.FileExtension = str_ext;
                                        }
                                        else
                                        {
                                            strStatus = "File Upload Failed";
                                            lblMsgINFCompCondAttachment.Text = "File can not upload. It has more than 5 MB size";
                                            lblMsgINFCompCondAttachment.ForeColor = System.Drawing.Color.Red;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMsgINFCompCondAttachment.Text = "Not a valid file!!..Select another file!!";
                                        lblMsgINFCompCondAttachment.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMsgINFCompCondAttachment.Text = "Not a valid file!!..Select another file!!";
                                    lblMsgINFCompCondAttachment.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                lblMsgINFCompCondAttachment.Text = "Please select a file..!!";
                                lblMsgINFCompCondAttachment.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                            obj_insertInfrastructureRenewApplicationAttachment.InfrastructureRenewApplicationCode = obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode;
                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentCode = obj_InfrastructureRenewSADComplianceConditionNOC.ComplianceConditionAttachmentCode;
                            obj_insertInfrastructureRenewApplicationAttachment.AttachmentName = txtFileCompCondNOCAttachment.Text;

                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                            obj_insertInfrastructureRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                            if (obj_insertInfrastructureRenewApplicationAttachment.Add() == 1)
                            {
                                strStatus = "File Upload Success";
                                lblMsgINFCompCondAttachment.Text = "File Upload Success";
                                lblMsgINFCompCondAttachment.ForeColor = System.Drawing.Color.Green;

                                BindInfrastructureRenewComplianceConditionNOCAttachmentDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMsgINFCompCondAttachment.Text = obj_insertInfrastructureRenewApplicationAttachment.CustumMessage;
                                lblMsgINFCompCondAttachment.ForeColor = System.Drawing.Color.Red;

                            }

                            //lblMsgINFCompCondAttachment.ForeColor = Color.Green;
                            //lblMsgINFCompCondAttachment.Text = "";
                            txtFileCompCondNOCAttachment.Text = "";
                            BindInfrastructureRenewComplianceConditionNOCAttachmentDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
                            lblMessageCertifiedRevenueSketch.Text = "";
                            lblMessageReasonForNotApplyingBefore.Text = "";
                            lblMessageGroundwaterAvailability.Text = "";
                            lblMessageRainwaterHarvesting.Text = "";
                            lblMessageExistingNOC.Text = "";
                            lblMessageUndertaking.Text = "";
                            lblMessageWaterRequirement.Text = "";
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
                        lblMsgINFCompCondAttachment.Text = "Maximum number of files to be uploaded is 5";
                        lblMsgINFCompCondAttachment.ForeColor = System.Drawing.Color.Red;
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
                Label lblMsgINFCompCondNOCAttachmentDelete = null;
                if (e.CommandName == "DeleteFile")
                {
                    strActionName = "Delete File INFCompCondNOCAttachment";


                    GridViewRow gvCompCondNOCAttachmentRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    GridView gvCompCondNOCAttachment = (GridView)(gvCompCondNOCAttachmentRow.Parent.Parent);
                    GridViewRow gvINFRenewComplianceConditionNOCRow = (GridViewRow)(gvCompCondNOCAttachment.NamingContainer);

                    int b = gvINFRenewComplianceConditionNOCRow.RowIndex;

                    foreach (GridViewRow row in gvINFRenewComplianceConditionNOC.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            Label lblMsgINFCompCondNOCAttachmentDelete1 = (Label)row.FindControl("lblMessageINFCompCondNOCAttachmentDelete");
                            lblMsgINFCompCondNOCAttachmentDelete1.Text = "";
                            Label lblMsgINFCompCondAttachment = (Label)row.FindControl("lblMessageINFCompCondAttachment");
                            lblMsgINFCompCondAttachment.Text = "";

                            TextBox txtAttachmentName = (TextBox)row.FindControl("txtAttachmentNameCompCondNOC");
                            txtAttachmentName.Text = "";

                            if (row.RowIndex == b)
                            {
                                lblMsgINFCompCondNOCAttachmentDelete = (Label)gvINFRenewComplianceConditionNOC.Rows[b].FindControl("lblMessageINFCompCondNOCAttachmentDelete");
                            }
                        }
                    }

                    string[] CommandArgument = e.CommandArgument.ToString().Split(',');
                    long lng_InfrastructureRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_AttachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_AttachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);


                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment obj_InfrastructureRenewApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment(lng_InfrastructureRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);

                    //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                    //string str_fullPath = str_startPathFromConfig + obj_InfrastructureRenewApplicationAttachment.AttachmentPath;


                    if (obj_InfrastructureRenewApplicationAttachment.Delete() == 1)
                    {
                        strStatus = "File Deleted Success";

                        lblMsgINFCompCondNOCAttachmentDelete.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                        lblMsgINFCompCondNOCAttachmentDelete.ForeColor = System.Drawing.Color.Red;

                        //if (File.Exists(str_fullPath))
                        //{
                        //    File.Delete(str_fullPath);
                        //}
                        BindInfrastructureRenewComplianceConditionNOCAttachmentDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    }
                    else
                    {
                        strStatus = "File Deleted Failed";
                        lblMsgINFCompCondNOCAttachmentDelete.Text = obj_InfrastructureRenewApplicationAttachment.CustumMessage;
                        lblMsgINFCompCondNOCAttachmentDelete.ForeColor = System.Drawing.Color.Red;
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

}