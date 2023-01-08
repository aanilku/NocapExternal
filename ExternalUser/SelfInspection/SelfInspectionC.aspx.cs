using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Threading;
using System.Drawing;
using NOCAP.BLL.Master;
using NOCAP.BLL.Industrial.New.Application;
using NOCAP.BLL.Infrastructure.New.Application;
using NOCAP.BLL.Mining.New.Application;
using NOCAP.BLL.Industrial.Renew.Application;
using NOCAP.BLL.Infrastructure.Renew.Application;
using NOCAP.BLL.Mining.Renew.Application;

public partial class ExternalUser_SelfInspection_SelfInspectionC : System.Web.UI.Page
{
    string strPageName = "SelfInspection";
    string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                ValidationExepInit();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;

                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");

                    if (placeHolder != null)
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblApplicationCodeFrom");
                        if (SourceLabel != null && SourceLabel.Text != "")
                        {
                            lblApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                        }

                        Label SourceLabelPreviousPage = (Label)placeHolder.FindControl("lblApplicationRenewCode");
                        if (SourceLabelPreviousPage != null && SourceLabelPreviousPage.Text != "")
                        {
                            lblApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabelPreviousPage.Text);
                        }
                        //txtComplianceSubmitDt.Text = DateTime.Now.ToString("dd/MM/yyyy");


                        if (lblApplicationCodeFrom.Text.Trim() != "")
                        {

                            // Start new code for OTP Verify
                            NOCAPExternalUtility.FillDropDownConsultant(ref ddlConsultant);
                            OTPVerifyEnavleDesable();
                            // End new code for OTP Verify


                            chkUndertaking.Checked = true;
                            PopulateData(Convert.ToInt64(lblApplicationCodeFrom.Text), sender, e);

                            //ddlRecycleReuse_SelectedIndexChanged(sender, e);
                            // ddlRWHArtificialRecharge_SelectedIndexChanged(sender, e);

                            IFWaterAuditInspection_SelectedIndexChanged(sender, e);
                            ddlImpactassessmentreport_SelectedIndexChanged(sender, e);
                            ddlAnyViolation_SelectedIndexChanged(sender, e);
                            ddlAnyothercompliance_SelectedIndexChanged(sender, e);
                            ddlWaterauditreportattached_SelectedIndexChanged(sender, e);
                            ddlCopyIAR_SelectedIndexChanged(sender, e);
                            BindGridView(gvWaterAuditInspection, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblWaterAuditInspectionCount);
                            BindGridView(gvIAR, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblIARCount);
                            BindGridView(gvExtra, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblExtraCount);

                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblModeFrom.Text = "";
            lblPageTitleFrom.Text = "";
            lblApplicationCodeFrom.Text = "";
        }

    }




    #region Attachment
    protected void btnUploadWaterAuditInspection_Click(object sender, EventArgs e)
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

                strActionName = "UploadSiteInspection";
                NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(Convert.ToInt64(lblApplicationCodeFrom.Text));
                NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment[] arr_selfComplianceAttachment;
                arr_selfComplianceAttachment = obj_selfInspection.GetWaterAuditAttachmentList();


                try
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment obj_selfInspectionAttachment = new NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment();
                    NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_SelfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(Convert.ToInt64(lblApplicationCodeFrom.Text));

                    byte[] buffer = new byte[1];

                    if (FileUploadWaterAuditInspection.HasFile)
                    {

                        str_ext = System.IO.Path.GetExtension(FileUploadWaterAuditInspection.PostedFile.FileName).ToLower();

                        str_fname = FileUploadWaterAuditInspection.FileName;

                        if (str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                        {

                            if (NOCAPExternalUtility.IsValidFile(FileUploadWaterAuditInspection.PostedFile))
                            {
                                if (FileUploadWaterAuditInspection.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                {
                                    obj_selfInspectionAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadWaterAuditInspection.PostedFile);
                                    obj_selfInspectionAttachment.ContentType = FileUploadWaterAuditInspection.PostedFile.ContentType;
                                    obj_selfInspectionAttachment.AttachmentPath = FileUploadWaterAuditInspection.FileName;
                                    obj_selfInspectionAttachment.FileExtension = str_ext;
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblWaterAuditInspection.Text = "File can not upload. It has more than 5 MB size";
                                    lblWaterAuditInspection.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblWaterAuditInspection.Text = "Not a valid file!!..Select an other file!!";
                                lblWaterAuditInspection.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblWaterAuditInspection.Text = "Not a valid file!!..Select an other file!!";
                            lblWaterAuditInspection.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                    }
                    else
                    {
                        lblWaterAuditInspection.Text = "Please select a file..!!";
                        lblWaterAuditInspection.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    obj_selfInspectionAttachment.ApplicationCode = obj_selfInspection.ApplicationCode;
                    obj_selfInspectionAttachment.AttachmentCode = obj_selfInspection.WaterAuditAttCode;
                    obj_selfInspectionAttachment.AttachmentName = "WaterAudit";

                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    obj_selfInspectionAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;


                    if (obj_selfInspectionAttachment.Add() == 1)
                    {
                        strStatus = "File Upload Success";
                        lblWaterAuditInspection.Text = "File Upload Success";
                        lblWaterAuditInspection.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        lblWaterAuditInspection.Text = obj_selfInspectionAttachment.CustumMessage;
                        lblWaterAuditInspection.ForeColor = System.Drawing.Color.Red;

                    }


                    BindGridView(gvWaterAuditInspection, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblWaterAuditInspectionCount);

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
    protected void btnUploadIAR_Click(object sender, EventArgs e)
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

                strActionName = "UploadIAR";
                NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(Convert.ToInt64(lblApplicationCodeFrom.Text));
                NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment[] arr_selfComplianceAttachment;
                arr_selfComplianceAttachment = obj_selfInspection.GetIARModelingAttachmentList();


                try
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment obj_selfInspectionAttachment = new NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment();
                    NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_SelfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(Convert.ToInt64(lblApplicationCodeFrom.Text));

                    if (obj_SelfInspection != null && obj_SelfInspection.ImpactAssOCSOTPVerified == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                    {
                        byte[] buffer = new byte[1];

                        if (FileUploadIAR.HasFile)
                        {

                            str_ext = System.IO.Path.GetExtension(FileUploadIAR.PostedFile.FileName).ToLower();

                            str_fname = FileUploadIAR.FileName;

                            if (str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {

                                if (NOCAPExternalUtility.IsValidFile(FileUploadIAR.PostedFile))
                                {
                                    if (FileUploadIAR.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_selfInspectionAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadIAR.PostedFile);
                                        obj_selfInspectionAttachment.ContentType = FileUploadIAR.PostedFile.ContentType;
                                        obj_selfInspectionAttachment.AttachmentPath = FileUploadIAR.FileName;
                                        obj_selfInspectionAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblIAR.Text = "File can not upload. It has more than 5 MB size";
                                        lblIAR.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblIAR.Text = "Not a valid file!!..Select an other file!!";
                                    lblIAR.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }

                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblIAR.Text = "Not a valid file!!..Select an other file!!";
                                lblIAR.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblIAR.Text = "Please select a file..!!";
                            lblIAR.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_selfInspectionAttachment.ApplicationCode = obj_selfInspection.ApplicationCode;
                        obj_selfInspectionAttachment.AttachmentCode = obj_selfInspection.IARModelingAttCode;
                        obj_selfInspectionAttachment.AttachmentName = "IAR";

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_selfInspectionAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;


                        if (obj_selfInspectionAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            lblIAR.Text = "File Upload Success";
                            lblIAR.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblIAR.Text = obj_selfInspectionAttachment.CustumMessage;
                            lblIAR.ForeColor = System.Drawing.Color.Red;

                        }

                        lblWaterAuditInspection.Text = "";
                        BindGridView(gvIAR, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblIARCount);
                    }
                    else
                    {
                        lblIAR.Text = "Please Verified Consultant OTP";
                        lblIAR.ForeColor = System.Drawing.Color.Red;
                        return;
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
    protected void btnUploadExra_Click(object sender, EventArgs e)
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

                strActionName = "UploadIAR";
                NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(Convert.ToInt64(lblApplicationCodeFrom.Text));
                NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment[] arr_selfComplianceAttachment;
                arr_selfComplianceAttachment = obj_selfInspection.GetExtraAttachmentList();


                try
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment obj_selfInspectionAttachment = new NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment();
                    NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_SelfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(Convert.ToInt64(lblApplicationCodeFrom.Text));

                    byte[] buffer = new byte[1];

                    if (FileUploadExtra.HasFile)
                    {

                        str_ext = System.IO.Path.GetExtension(FileUploadExtra.PostedFile.FileName).ToLower();

                        str_fname = FileUploadExtra.FileName;

                        if (str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                        {

                            if (NOCAPExternalUtility.IsValidFile(FileUploadExtra.PostedFile))
                            {
                                if (FileUploadExtra.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                {
                                    obj_selfInspectionAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadExtra.PostedFile);
                                    obj_selfInspectionAttachment.ContentType = FileUploadExtra.PostedFile.ContentType;
                                    obj_selfInspectionAttachment.AttachmentPath = FileUploadExtra.FileName;
                                    obj_selfInspectionAttachment.FileExtension = str_ext;
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblExtra.Text = "File can not upload. It has more than 5 MB size";
                                    lblExtra.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblExtra.Text = "Not a valid file!!..Select an other file!!";
                                lblExtra.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblExtra.Text = "Not a valid file!!..Select an other file!!";
                            lblExtra.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                    }
                    else
                    {
                        lblExtra.Text = "Please select a file..!!";
                        lblExtra.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    obj_selfInspectionAttachment.ApplicationCode = obj_selfInspection.ApplicationCode;
                    obj_selfInspectionAttachment.AttachmentCode = obj_selfInspection.ExtraAttCode;
                    obj_selfInspectionAttachment.AttachmentName = "lblExtra";

                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    obj_selfInspectionAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;


                    if (obj_selfInspectionAttachment.Add() == 1)
                    {
                        strStatus = "File Upload Success";
                        lblExtra.Text = "File Upload Success";
                        lblExtra.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        lblExtra.Text = obj_selfInspectionAttachment.CustumMessage;
                        lblExtra.ForeColor = System.Drawing.Color.Red;

                    }
                    lblWaterAuditInspection.Text = "";
                    lblWaterAuditInspection.Text = "";
                    BindGridView(gvExtra, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblExtraCount);

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
    #endregion

    #region ViewFile_Click

    protected void ViewFile(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
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

                    NOCAPExternalUtility.SelfInsepDownloadFiles(lng_industrialNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
                }

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
                //lblMessageReferralLetter.Text = ex.Message;
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

    #region Delete row
    protected void gvWaterAuditInspection_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Penalty";
                if (DeleteAttchment((GridView)sender, e, lblWaterAuditInspection, strActionName) == 1)
                    BindGridView(gvWaterAuditInspection, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblWaterAuditInspectionCount);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }
    protected void gvIAR_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Penalty";
                if (DeleteAttchment((GridView)sender, e, lblIAR, strActionName) == 1)
                    BindGridView(gvIAR, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblIARCount);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }
    protected void gvExtra_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Penalty";
                if (DeleteAttchment((GridView)sender, e, lblExtra, strActionName) == 1)
                    BindGridView(gvExtra, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblExtraCount);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }
    private int DeleteAttchment(GridView gv, GridViewDeleteEventArgs e, Label lblMessage, string str_ActionName)
    {
        try
        {
            strActionName = "Delete Self gvgeophotostruct Attachment";
            long lng_ApplicationCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["ApplicationCode"]);
            int int_AttachmentCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["AttachmentCode"]);
            int int_AttachmentCodeSerialNumber = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
            NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment obj_selfInspectionAttachment = new NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment(lng_ApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);

            if (obj_selfInspectionAttachment.Delete() == 1)
            {
                lblMessage.Text = obj_selfInspectionAttachment.CustumMessage;
                strStatus = "File Delete Success";
                return 1;
            }
            else
            {
                lblMessage.Text = obj_selfInspectionAttachment.CustumMessage;
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
    #endregion

    #region Private
    private void ValidationExepInit()
    {


        revtxtAnyothercompliance.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtAnyothercompliance.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;
        revtxtAnyViolation.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtAnyViolation.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;
        revtxtRemarks.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtRemarks.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

    }
    private int UpdateData(long lngA_ApplicationCode, object sender, EventArgs e)
    {
        try
        {
            strActionName = "UpdateSelfCompliance";
            NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(lngA_ApplicationCode);

            //if (ddlWateAuditinspection.SelectedValue.ToString() == "1")
            //{
            //    if (lblWaterAuditInspectionCount.Text == "0")
            //    {
            //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Water Audit Inspection should be attached');", true);
            //        return 0;
            //    }

            //}
            if (ddlCopyIAR.SelectedValue.ToString() == "1")
            {
                if (lblIARCount.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('IAR should be attached');", true);
                    return 0;
                }

            }
            if (ddlWaterauditreportattached.SelectedValue.ToString() == "1")
            {
                if (lblWaterAuditInspectionCount.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Water Audit report should be attached');", true);
                    return 0;
                }

            }



            switch (ddlComplianceReportWSTF.SelectedValue.ToString())
            {
                case "1":
                    obj_selfInspection.SubSCWithinTimeFrame = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_selfInspection.SubSCWithinTimeFrame = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_selfInspection.SubSCWithinTimeFrame = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_selfInspection.SubSCWithinTimeFrame = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }

            switch (IFddlWaterAuditInspection.SelectedValue.ToString())
            {
                case "1":
                    obj_selfInspection.WaterAuditInspectionApplicable = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_selfInspection.WaterAuditInspectionApplicable = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_selfInspection.WaterAuditInspectionApplicable = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_selfInspection.WaterAuditInspectionApplicable = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            switch (ddlWateAuditinspection.SelectedValue.ToString())
            {
                case "1":
                    obj_selfInspection.WaterAuditInspection = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_selfInspection.WaterAuditInspection = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_selfInspection.WaterAuditInspection = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_selfInspection.WaterAuditInspection = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            switch (ddlWateAuditinspectionNOC.SelectedValue.ToString())
            {
                case "1":
                    obj_selfInspection.WaterAuditInspectionAsPerNOC = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_selfInspection.WaterAuditInspectionAsPerNOC = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_selfInspection.WaterAuditInspectionAsPerNOC = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_selfInspection.WaterAuditInspectionAsPerNOC = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            obj_selfInspection.AuditAgency = txtAuditAgency.Text.Trim();
            if (txtDateOfInsp.Text.Trim() != "")
                obj_selfInspection.DateOfInspectionAudit = Convert.ToDateTime(txtDateOfInsp.Text.Trim());
            switch (ddlWaterauditreportattached.SelectedValue.ToString())
            {
                case "1":
                    obj_selfInspection.WaterAuditReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_selfInspection.WaterAuditReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_selfInspection.WaterAuditReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_selfInspection.WaterAuditReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }

            switch (ddlImpactassessmentreport.SelectedValue.ToString())
            {
                case "1":
                    obj_selfInspection.ImpactAssementReportApplicable = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_selfInspection.ImpactAssementReportApplicable = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_selfInspection.ImpactAssementReportApplicable = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_selfInspection.ImpactAssementReportApplicable = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            switch (ddlRequirement.SelectedValue.ToString())
            {
                case "1":
                    obj_selfInspection.ImpactAssementReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_selfInspection.ImpactAssementReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_selfInspection.ImpactAssementReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_selfInspection.ImpactAssementReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            switch (ddlSubmittedNOC.SelectedValue.ToString())
            {
                case "1":
                    obj_selfInspection.ImpactAssementReportSubmitted = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_selfInspection.ImpactAssementReportSubmitted = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_selfInspection.ImpactAssementReportSubmitted = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_selfInspection.ImpactAssementReportSubmitted = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            switch (ddlCopyIAR.SelectedValue.ToString())
            {
                case "1":
                    obj_selfInspection.ImpactAssementAIRReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_selfInspection.ImpactAssementAIRReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_selfInspection.ImpactAssementAIRReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_selfInspection.ImpactAssementAIRReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            switch (ddlAnyViolation.SelectedValue.ToString())
            {
                case "1":
                    obj_selfInspection.AnyViolationNOCCondi = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_selfInspection.AnyViolationNOCCondi = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_selfInspection.AnyViolationNOCCondi = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_selfInspection.AnyViolationNOCCondi = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }


            obj_selfInspection.AnyViolationNOCCondiDesc = txtAnyViolation.Text.Trim();
            switch (ddlAnyothercompliance.SelectedValue.ToString())
            {
                case "1":
                    obj_selfInspection.AnyOtherInspection = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_selfInspection.AnyOtherInspection = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_selfInspection.AnyOtherInspection = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_selfInspection.AnyOtherInspection = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            if (chkUndertaking.Checked)
                obj_selfInspection.Undertaking = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
            else
                obj_selfInspection.Undertaking = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
           
            
            obj_selfInspection.AnyOtherInspectionDesc = txtAnyothercompliance.Text.Trim();
            obj_selfInspection.Remarks = txtRemarks.Text.Trim();
            obj_selfInspection.InspectionSubmitDate = DateTime.Now;
            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_selfInspection.ModifiedByExUC = obj_externalUser.ExternalUserCode;
            if (obj_selfInspection.Update() == 1)
            {
                strStatus = "Update Success";
                lblMessage.Text = obj_selfInspection.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                strStatus = "Update Failed";
                lblMessage.Text = obj_selfInspection.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
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


   
    private void BindGridView(GridView gv, long lngA_ApplicationCode, ref Label AttCount)
    {
        NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(lngA_ApplicationCode);
        NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment[] arr_SelfComplianceAttachment = null;

        if (gv.ID == "gvWaterAuditInspection")
            arr_SelfComplianceAttachment = obj_selfInspection.GetWaterAuditAttachmentList();
        else if (gv.ID == "gvIAR")
            arr_SelfComplianceAttachment = obj_selfInspection.GetIARModelingAttachmentList();
        else if (gv.ID == "gvExtra")
            arr_SelfComplianceAttachment = obj_selfInspection.GetExtraAttachmentList();

        gv.DataSource = arr_SelfComplianceAttachment;
        gv.DataBind();
        AttCount.Text = arr_SelfComplianceAttachment.Length.ToString();
    }
    private void PopulateData(long lngA_ApplicationCode, object sender, EventArgs e)
    {
        try
        {
            NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(lngA_ApplicationCode);
            if (obj_selfInspection.ApplicationCode != 0)
            {

                switch (obj_selfInspection.SubSCWithinTimeFrame)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlComplianceReportWSTF.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlComplianceReportWSTF.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlComplianceReportWSTF.SelectedValue = "";
                        break;
                    default:
                        ddlComplianceReportWSTF.SelectedValue = "";
                        break;
                }
                switch (obj_selfInspection.WaterAuditInspectionApplicable)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        IFddlWaterAuditInspection.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        IFddlWaterAuditInspection.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        IFddlWaterAuditInspection.SelectedValue = "";
                        break;
                    default:
                        IFddlWaterAuditInspection.SelectedValue = "";
                        break;
                }
                switch (obj_selfInspection.WaterAuditInspection)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlWateAuditinspection.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlWateAuditinspection.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlWateAuditinspection.SelectedValue = "";
                        break;
                    default:
                        ddlWateAuditinspection.SelectedValue = "";
                        break;
                }
                switch (obj_selfInspection.WaterAuditInspectionAsPerNOC)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlWateAuditinspectionNOC.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlWateAuditinspectionNOC.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlWateAuditinspectionNOC.SelectedValue = "";
                        break;
                    default:
                        ddlWateAuditinspectionNOC.SelectedValue = "";
                        break;
                }
                txtAuditAgency.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfInspection.AuditAgency));
                if (obj_selfInspection.DateOfInspectionAudit != null)

                    txtDateOfInsp.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_selfInspection.DateOfInspectionAudit).ToString("dd/MM/yyyy"));
                switch (obj_selfInspection.WaterAuditReport)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlWaterauditreportattached.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlWaterauditreportattached.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlWaterauditreportattached.SelectedValue = "";
                        break;
                    default:
                        ddlWaterauditreportattached.SelectedValue = "";
                        break;
                }


                switch (obj_selfInspection.ImpactAssementReportApplicable)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlImpactassessmentreport.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlImpactassessmentreport.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlImpactassessmentreport.SelectedValue = "";
                        break;
                    default:
                        ddlImpactassessmentreport.SelectedValue = "";
                        break;
                }
                switch (obj_selfInspection.ImpactAssementReport)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlRequirement.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlRequirement.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlRequirement.SelectedValue = "";
                        break;
                    default:
                        ddlRequirement.SelectedValue = "";
                        break;
                }
                switch (obj_selfInspection.ImpactAssementReportSubmitted)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlSubmittedNOC.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlSubmittedNOC.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlSubmittedNOC.SelectedValue = "";
                        break;
                    default:
                        ddlSubmittedNOC.SelectedValue = "";
                        break;
                }
                switch (obj_selfInspection.ImpactAssementAIRReport)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlCopyIAR.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlCopyIAR.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlCopyIAR.SelectedValue = "";
                        break;
                    default:
                        ddlCopyIAR.SelectedValue = "";
                        break;
                }
                switch (obj_selfInspection.AnyViolationNOCCondi)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlAnyViolation.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlAnyViolation.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlAnyViolation.SelectedValue = "";
                        break;
                    default:
                        ddlAnyViolation.SelectedValue = "";
                        break;
                }
                txtAnyViolation.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfInspection.AnyViolationNOCCondiDesc));
                switch (obj_selfInspection.AnyOtherInspection)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        ddlAnyothercompliance.SelectedValue = "1";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        ddlAnyothercompliance.SelectedValue = "0";
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        ddlAnyothercompliance.SelectedValue = "";
                        break;
                    default:
                        ddlAnyothercompliance.SelectedValue = "";
                        break;
                }
                txtAnyothercompliance.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfInspection.AnyOtherInspectionDesc));

                txtRemarks.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfInspection.Remarks));
                switch (obj_selfInspection.Undertaking)
                {
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes:
                        chkUndertaking.Checked = true;
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.No:
                        chkUndertaking.Checked = false;
                        break;
                    case NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined:
                        chkUndertaking.Checked = false;
                        break;
                    default:
                        chkUndertaking.Checked =false;
                        break;
                }
                
            }

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    #endregion

    #region Button
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
            Server.Transfer("SelfInspectionB.aspx");

        }
    }

    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
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
                try
                {
                    NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(Convert.ToInt64(lblApplicationCodeFrom.Text));
                    if (UpdateData(Convert.ToInt64(lblApplicationCodeFrom.Text), sender, e) == 1)
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Record Updated Successfully');", true);
                    PopulateData(Convert.ToInt64(lblApplicationCodeFrom.Text), sender, e);
                }
                catch (Exception)
                {
                    Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
                }
            }
        }
    }



    protected void btnNext_Click(object sender, EventArgs e)
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
                try
                {
                    int status = 0;
                    NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(Convert.ToInt64(lblApplicationCodeFrom.Text));
                    status = UpdateData(Convert.ToInt64(lblApplicationCodeFrom.Text), sender, e);
                    if (status == 1)
                    {
                        Server.Transfer("SelfInspectionSumit.aspx");
                    }
                }
                catch (ThreadAbortException)
                {

                }
                catch (Exception ex)
                {
                    Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
                }
            }
        }
    }
    #endregion

    #region SelectionIndexchange
    protected void ddlRWHArtificialRecharge_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlRWHArtificialRecharge.SelectedValue == "0")
            //{
            //    txtRWHArtificialRechargeNo.Text = string.Empty;
            //    txtRWHArtificialRechargeNo.Enabled = false;
            //    revtxtRWHArtificialRechargeNo.Enabled = false;

            //    txtRWHArtificialRechargeCapacity.Text = string.Empty;
            //    txtRWHArtificialRechargeCapacity.Enabled = false;
            //    revtxtRWHArtificialRechargeCapacity.Enabled = false;
            //}
            //else
            //{
            //    txtRWHArtificialRechargeNo.Enabled = true;
            //    revtxtRWHArtificialRechargeNo.Enabled = true;

            //    txtRWHArtificialRechargeCapacity.Enabled = true;
            //    revtxtRWHArtificialRechargeCapacity.Enabled = true;
            //}
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
    }
    protected void ddlRecycleReuse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //    if (ddlRecycleReuse.SelectedValue == "0")
            //    {
            //        txtRecycleReuseInDay.Text = string.Empty;
            //        txtRecycleReuseInDay.Enabled = false;
            //        revtxtRecycleReuseInDay.Enabled = false;

            //        txtRecycleReuseInYear.Text = string.Empty;
            //        txtRecycleReuseInYear.Enabled = false;
            //        revtxtRecycleReuseInYear.Enabled = false;
            //    }
            //    else
            //    {
            //        txtRecycleReuseInDay.Enabled = true;
            //        revtxtRecycleReuseInDay.Enabled = true;

            //        txtRecycleReuseInYear.Enabled = true;
            //        revtxtRecycleReuseInYear.Enabled = true;
            //    }
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
    }
    protected void ddlWaterauditreportattached_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWaterauditreportattached.SelectedValue.ToString() == "1")
        {
            FileUploadWaterAuditInspection.Enabled = true;
            btnUploadWaterAuditInspection.Enabled = true;
            gvWaterAuditInspection.Enabled = true;
        }
        else
        {
            FileUploadWaterAuditInspection.Enabled = false;
            btnUploadWaterAuditInspection.Enabled = false;
            gvWaterAuditInspection.Enabled = false;
        }
    }


    protected void ddlCopyIAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCopyIAR.SelectedValue.ToString() == "1")
        {
            FileUploadIAR.Enabled = true;
            btnUploadIAR.Enabled = true;
            gvIAR.Enabled = true;
        }
        else
        {
            FileUploadIAR.Enabled = false;
            btnUploadIAR.Enabled = false;
            gvIAR.Enabled = false;
        }
    }





    protected void IFWaterAuditInspection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IFddlWaterAuditInspection.SelectedValue.ToString() == "1")
        {
            ddlWateAuditinspection.Enabled = true;
            ddlWateAuditinspectionNOC.Enabled = true;
            txtAuditAgency.Enabled = true;
            txtDateOfInsp.Enabled = true;
            rfvtxtAuditAgency.Enabled = true;
            rfvtxtDateOfInsp.Enabled = true;
            ddlWaterauditreportattached.Enabled = true;
            calDateOfInsp.Enabled = true;
            rfvddlWateAuditinspection.Enabled = true;
            rfvddlWateAuditinspectionNOC.Enabled = true;
            rfvddlWaterauditreportattached.Enabled = true;
        }
        else
        {
            ddlWateAuditinspection.Enabled = false;
            ddlWateAuditinspection.SelectedIndex = 0;
            ddlWateAuditinspectionNOC.Enabled = false;
            ddlWateAuditinspectionNOC.SelectedIndex = 0;
            txtAuditAgency.Enabled = false;
            txtAuditAgency.Text = string.Empty;
            rfvtxtAuditAgency.Enabled = false;
            rfvtxtAuditAgency.Text = string.Empty;
            txtDateOfInsp.Enabled = false;
            txtDateOfInsp.Text = string.Empty;
            rfvtxtDateOfInsp.Enabled = false;

            txtDateOfInsp.Enabled = false;
            txtDateOfInsp.Text = string.Empty;
            ddlWaterauditreportattached.Enabled = false;
            ddlWaterauditreportattached.SelectedIndex = 0;
            calDateOfInsp.Enabled = false;
            rfvddlWateAuditinspection.Enabled = false;
            rfvddlWateAuditinspectionNOC.Enabled = false;
            rfvddlWaterauditreportattached.Enabled = false;
        }
    }

    protected void ddlImpactassessmentreport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlImpactassessmentreport.SelectedValue.ToString() == "1")
        {
            ddlRequirement.Enabled = true;
            ddlSubmittedNOC.Enabled = true;
            ddlCopyIAR.Enabled = true;
            rfvddlRequirement.Enabled = true;
            rfvddlSubmittedNOC.Enabled = true;
            rfvddlCopyIAR.Enabled = true;

        }
        else
        {
            ddlRequirement.Enabled = false;
            ddlRequirement.SelectedIndex = 0;
            ddlSubmittedNOC.Enabled = false;
            ddlSubmittedNOC.SelectedIndex = 0;

            ddlCopyIAR.Enabled = false;
            ddlCopyIAR.SelectedIndex = 0;
            rfvddlRequirement.Enabled = false;
            rfvddlSubmittedNOC.Enabled = false;
            rfvddlCopyIAR.Enabled = false;


        }
    }

    protected void ddlAnyViolation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAnyViolation.SelectedValue.ToString() == "1")
        {
            txtAnyViolation.Enabled = true;
            rfvtxtAnyViolation.Enabled = true;


        }
        else
        {
            txtAnyViolation.Enabled = false;
            txtAnyViolation.Text = string.Empty;
            rfvtxtAnyViolation.Enabled = false;

        }
    }

    protected void ddlAnyothercompliance_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAnyothercompliance.SelectedValue.ToString() == "1")
        {
            txtAnyothercompliance.Enabled = true;
            rfvtxtAnyothercompliance.Enabled = true;


        }
        else
        {
            txtAnyothercompliance.Enabled = false;
            txtAnyothercompliance.Text = string.Empty;
            rfvtxtAnyothercompliance.Enabled = false;

        }
    }




    #endregion

    void OTPVerifyEnavleDesable()
    {
        NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(Convert.ToInt64(lblApplicationCodeFrom.Text));        
        if (obj_selfInspection != null && obj_selfInspection.ApplicationCode > 0)
        {
            if (obj_selfInspection.ImpactAssOCSOTPVerified ==  NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
            {
                ddlConsultant.ClearSelection();
                ddlConsultant.Items.FindByValue(Convert.ToString(obj_selfInspection.ImpactAssOCSOTPVerifiedByCC)).Selected = true;
                ddlConsultant.Enabled = false;
                txtImpactReportOCSOTP.Enabled = false;
                btnOTPVerify.Enabled = false;
                lblOTPVerified.Text = Convert.ToString(obj_selfInspection.ImpactAssOCSOTPVerified);
                btnSendOTP.Enabled = false;
            }
            else
            {
                ddlConsultant.Enabled = true;
                txtImpactReportOCSOTP.Enabled = true;
                btnOTPVerify.Enabled = true;
                lblOTPVerified.Text = Convert.ToString(NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.ImpactAssOCSOTPVerifiedYesNo.No);
                btnSendOTP.Enabled = true;
            }
        }

    }
    protected void btnOTPVerify_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["ConsultantOTP"]) == txtImpactReportOCSOTP.Text)
        {
            NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(Convert.ToInt64(lblApplicationCodeFrom.Text));
            //IndustrialNewSADApplication obj_industrialNewSADApplication = new IndustrialNewSADApplication(Convert.ToUInt32(lblApplicationCodeFrom.Text));
            if (obj_selfInspection.ApplicationCode > 0)
            {
                obj_selfInspection.ImpactAssOCSOTPVerified =  NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                obj_selfInspection.ImpactAssOCSOTPVerifiedByCC = Convert.ToInt32(ddlConsultant.SelectedValue);
                obj_selfInspection.ModifiedByExUC = Convert.ToInt64(Session["ExternalUserCode"]); ;
                int int_result = obj_selfInspection.SetImpactAssOCSOTPVerified();
                if (int_result == 1)
                {
                    ddlConsultant.SelectedIndex = -1;
                    txtImpactReportOCSOTP.Text = "";
                    lblIAR.Text = "OTP Verifired successfully.";
                    lblIAR.ForeColor = Color.Green;
                    OTPVerifyEnavleDesable();
                    return;
                }
                else
                {
                    lblIAR.Text = "OTP Verification failed.";
                    lblIAR.ForeColor = Color.Red;
                    return;
                }

            }
            else
            {
                lblIAR.Text = "Error on page.";
                lblIAR.ForeColor = Color.Red;
                return;
            }
        }
        else
        {
            lblIAR.Text = "Invalid OTP, Please try again";
            lblIAR.ForeColor = Color.Red;
            return;
        }

    }

    protected void btnSendOTP_Click(object sender, EventArgs e)
    {
        string EmailServerName = "";
        string strSMSUserName = "";

        if (ddlConsultant.SelectedIndex > 0)
        {
            ConsultantDetail obj_ConsultantDetail = new ConsultantDetail(Convert.ToInt32(ddlConsultant.SelectedValue));
            if (obj_ConsultantDetail != null && obj_ConsultantDetail.ConsultantCode > 0)
            {
                string OTPMessage = "";
                string OTP = "";
                OTP = NOCAPExternalUtility.GetRandomNumber();
                Session["ConsultantOTP"] = OTP;

                if (!NOCAPExternalUtility.IsNumeric(obj_ConsultantDetail.MobileNumber))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('Invalid Mobile Number !!');", true);
                    return;
                }

                IndustrialNewApplication objA_industrialNewApplication = null;
                InfrastructureNewApplication objA_infrastructureNewApplication = null;
                MiningNewApplication objA_miningNewApplication = null;

                IndustrialRenewApplication objA_industrialRenewApplication = null;
                InfrastructureRenewApplication objA_infrastructureRenewApplication = null;
                MiningRenewApplication objA_miningRenewApplication = null;
                long lngA_ApplicationCode = Convert.ToInt64(lblApplicationCodeFrom.Text);


               int intResult= NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out objA_industrialNewApplication, out  objA_infrastructureNewApplication, out  objA_miningNewApplication,
                   out  objA_industrialRenewApplication, out objA_infrastructureRenewApplication, out  objA_miningRenewApplication,  lngA_ApplicationCode );

                string applicationName = "";
                string str_TownName = "";
                string str_VillageName = "";
                string str_StateName = "";
                string str_DistrictName = "";
                string str_SubDistrictName = "";

                if (intResult == 1)
                {

                  


                    if (objA_industrialNewApplication != null && objA_industrialNewApplication.IndustrialNewApplicationCode > 0)
                    {
                        applicationName = objA_industrialNewApplication.NameOfIndustry;

                        //for Address                       
                         str_StateName = new NOCAP.BLL.Master.State(objA_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
                         str_DistrictName = new NOCAP.BLL.Master.District(objA_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objA_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName;
                         str_SubDistrictName = new NOCAP.BLL.Master.SubDistrict(objA_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objA_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, objA_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;

                        if (objA_industrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown == NOCAP.BLL.Common.Address.VillageOrTownOption.Town)
                        {
                            str_TownName = "Town Name : ";
                            str_TownName = str_TownName + new NOCAP.BLL.Master.Town(objA_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objA_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, objA_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, objA_industrialNewApplication.ProposedLocation.ProposedLocationAddress.TownCode).TownName;
                            str_TownName = str_TownName + ", ";
                        }
                        if (objA_industrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown == NOCAP.BLL.Common.Address.VillageOrTownOption.Village)
                        {
                            str_VillageName = "Village Name : ";
                            str_VillageName = str_VillageName + new NOCAP.BLL.Master.Village(objA_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objA_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, objA_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, objA_industrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode).VillageName;
                            str_VillageName = str_VillageName + ", ";
                        }
                        //for Address



                    }
                    if (objA_infrastructureNewApplication != null && objA_infrastructureNewApplication.InfrastructureNewApplicationCode > 0)
                    {
                        applicationName = objA_infrastructureNewApplication.NameOfInfrastructure;

                        //for Address

                        str_StateName = new NOCAP.BLL.Master.State(objA_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
                         str_DistrictName = new NOCAP.BLL.Master.District(objA_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objA_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName;
                         str_SubDistrictName = new NOCAP.BLL.Master.SubDistrict(objA_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objA_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, objA_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;

                        if (objA_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown == NOCAP.BLL.Common.Address.VillageOrTownOption.Town)
                        {
                            str_TownName = "Town Name : ";
                            str_TownName = str_TownName + new NOCAP.BLL.Master.Town(objA_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objA_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, objA_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, objA_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.TownCode).TownName;
                            str_TownName = str_TownName + ", ";
                        }
                        if (objA_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown == NOCAP.BLL.Common.Address.VillageOrTownOption.Village)
                        {
                            str_VillageName = "Village Name : ";
                            str_VillageName = str_VillageName + new NOCAP.BLL.Master.Village(objA_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objA_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, objA_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, objA_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode).VillageName;
                            str_VillageName = str_VillageName + ", ";
                        }
                        //for Address

                    }
                    if (objA_miningNewApplication != null && objA_miningNewApplication.ApplicationCode > 0)
                    {
                        applicationName = objA_miningNewApplication.NameOfMining;
                        //for Address
                        str_StateName = new NOCAP.BLL.Master.State(objA_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
                         str_DistrictName = new NOCAP.BLL.Master.District(objA_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objA_miningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName;
                         str_SubDistrictName = new NOCAP.BLL.Master.SubDistrict(objA_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objA_miningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, objA_miningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;

                        if (objA_miningNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown == NOCAP.BLL.Common.Address.VillageOrTownOption.Town)
                        {
                            str_TownName = "Town Name : ";
                            str_TownName = str_TownName + new NOCAP.BLL.Master.Town(objA_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objA_miningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, objA_miningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, objA_miningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode).TownName;
                            str_TownName = str_TownName + ", ";
                        }
                        if (objA_miningNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown == NOCAP.BLL.Common.Address.VillageOrTownOption.Village)
                        {
                            str_VillageName = "Village Name : ";
                            str_VillageName = str_VillageName + new NOCAP.BLL.Master.Village(objA_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objA_miningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, objA_miningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, objA_miningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode).VillageName;
                            str_VillageName = str_VillageName + ", ";
                        }
                        //for Address

                    }
                    if (objA_industrialRenewApplication != null && objA_industrialRenewApplication.IndustrialRenewApplicationCode > 0)
                    {                       

                        IndustrialNewApplication objL_IndustrialNewApplication = new IndustrialNewApplication(objA_industrialRenewApplication.FirstApplicationCode);

                        applicationName = objL_IndustrialNewApplication.NameOfIndustry;
                        //for Address
                        str_StateName = new NOCAP.BLL.Master.State(objL_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
                         str_DistrictName = new NOCAP.BLL.Master.District(objL_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objL_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName;
                         str_SubDistrictName = new NOCAP.BLL.Master.SubDistrict(objL_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objL_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, objL_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;

                        if (objL_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown == NOCAP.BLL.Common.Address.VillageOrTownOption.Town)
                        {
                            str_TownName = "Town Name : ";
                            str_TownName = str_TownName + new NOCAP.BLL.Master.Town(objL_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objL_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, objL_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, objL_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.TownCode).TownName;
                            str_TownName = str_TownName + ", ";
                        }
                        if (objL_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown == NOCAP.BLL.Common.Address.VillageOrTownOption.Village)
                        {
                            str_VillageName = "Village Name : ";
                            str_VillageName = str_VillageName + new NOCAP.BLL.Master.Village(objL_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objL_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, objL_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, objL_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode).VillageName;
                            str_VillageName = str_VillageName + ", ";
                        }
                        //for Address
                    }
                    if (objA_infrastructureRenewApplication != null && objA_infrastructureRenewApplication.InfrastructureRenewApplicationCode > 0)
                    {
                        InfrastructureNewApplication objL_infrastructureNewApplication = new InfrastructureNewApplication(objA_infrastructureRenewApplication.FirstApplicationCode);

                        applicationName = objL_infrastructureNewApplication.NameOfInfrastructure;

                         str_StateName = new NOCAP.BLL.Master.State(objL_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
                         str_DistrictName = new NOCAP.BLL.Master.District(objL_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objL_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName;
                         str_SubDistrictName = new NOCAP.BLL.Master.SubDistrict(objL_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objL_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, objL_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;

                        if (objL_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown == NOCAP.BLL.Common.Address.VillageOrTownOption.Town)
                        {
                            str_TownName = "Town Name : ";
                            str_TownName = str_TownName + new NOCAP.BLL.Master.Town(objL_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objL_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, objL_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, objL_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.TownCode).TownName;
                            str_TownName = str_TownName + ", ";
                        }
                        if (objL_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown == NOCAP.BLL.Common.Address.VillageOrTownOption.Village)
                        {
                            str_VillageName = "Village Name : ";
                            str_VillageName = str_VillageName + new NOCAP.BLL.Master.Village(objL_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objL_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, objL_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, objL_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode).VillageName;
                            str_VillageName = str_VillageName + ", ";
                        }
                        //for Address

                    }
                    if (objA_miningRenewApplication != null && objA_miningRenewApplication.MiningRenewApplicationCode > 0)
                    {
                        MiningNewApplication objL_MiningNewApplication = new MiningNewApplication(objA_miningRenewApplication.FirstApplicationCode);
                        applicationName = objL_MiningNewApplication.NameOfMining;

                         str_StateName = new NOCAP.BLL.Master.State(objL_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
                         str_DistrictName = new NOCAP.BLL.Master.District(objL_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objL_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName;
                         str_SubDistrictName = new NOCAP.BLL.Master.SubDistrict(objL_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objL_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, objL_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;

                        if (objL_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown == NOCAP.BLL.Common.Address.VillageOrTownOption.Town)
                        {
                            str_TownName = "Town Name : ";
                            str_TownName = str_TownName + new NOCAP.BLL.Master.Town(objL_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objL_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, objL_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, objL_MiningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode).TownName;
                            str_TownName = str_TownName + ", ";
                        }
                        if (objL_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageOrTown == NOCAP.BLL.Common.Address.VillageOrTownOption.Village)
                        {
                            str_VillageName = "Village Name : ";
                            str_VillageName = str_VillageName + new NOCAP.BLL.Master.Village(objL_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, objL_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, objL_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, objL_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode).VillageName;
                            str_VillageName = str_VillageName + ", ";
                        }
                        //for Address

                    }


                }

               // NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(Convert.ToInt64(lblApplicationCodeFrom.Text));
                if (applicationName !="")
                {

                    if (SMSUtility.IsSendSMSEnable())
                    {
                        OTPMessage = "Do not share OTP with anyone. OTP for submission of IAR / CHR to CGWA in respect of your application name : " + applicationName + " is :" + OTP;
                        string msgRes = SMSUtility.sendOTPtoMobile(OTPMessage, obj_ConsultantDetail.MobileNumber, "1007165035970164831", out strSMSUserName);
                        //string msgRes = "Platform accepted";
                        // lblOTPMsg.Text = OTPMessage;
                        if (msgRes.Trim() == "Platform accepted")
                        {
                            // SaveSMSAlert();
                            ClientScript.RegisterStartupScript(this.GetType(), "SMSAlert", "alert('One time Password(OTP) has been Sent to your Mobile No, Enter OTP to Complete Your verification Process');", true);

                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "SMSAlert", "alert('Error in sending OTP in mobile number');", true);
                        }

                    }
                    if (EmailUtility.IsSendEmailEnable())
                    {                        

                        string strBody = "<table style='max-width: 600px;margin: auto;width: 100%;border: 1px solid #f9f9f9;'><tr><td style='padding:30px;' colspan='3'><p>Dear <span>" + obj_ConsultantDetail.ConsultantName + "</span></p><p>Your One Time Password (OTP) for submission of Impact Assessment Report / Comprehensive Hydrogeological Report to CGWA in respect of Application for <span style='font-style: italic;'> " + applicationName + ", " + str_VillageName + str_TownName + " Sub District : " + str_SubDistrictName + ", District : " + str_DistrictName + ", State : " + str_StateName + " </span> is mentioned below.</p><p>Please enter this OTP in the required field to proceed further.</p></td></tr><tr style='background-color: #1d65a3;'><td style='color: #fff;text-align: center; padding: 5px 0px;' colspan='3'>DETAILS</td></tr><tr><td style='padding-left: 30px;'><strong>One Time Password (OTP)</strong></td><td style='padding-right: 30px;' colspan='2'><strong>" + OTP + "</strong></td></tr><tr><td  colspan='3' style='padding:30px;'><p>The above mentioned OTP is valid for 15 minutes.</p><p>Sincerely, <br /><strong>CGWA, New Delhi</strong></p></td></tr></table>";

                        // string strBody = "<p>Sir/Madam,</p><p> </br></br>Do not share OTP with anyone. OTP for submission of IAR / CHR to CGWA in respect of your application name : " + applicationName + " is <span style='color: blue;'> " + OTP + "</span> </p><p><br />  This is an auto-generated email.&nbsp; Do not reply to this email.<br />  </p>";
                        bool boolResult = EmailUtility.SendMail(out EmailServerName, StrTo: obj_ConsultantDetail.EmailID, StrSubject: "OTP for submission of IAR / CHR to CGWA", StrBody: strBody);

                        if (boolResult == true)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "EmailAlert", "alert('One time Password(OTP) has been Sent to your email , Enter OTP to Complete Your verification Process');", true);

                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "EmailAlert", "alert('Error in sending OTP in your email');", true);
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error on page');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error on page');", true);
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Please Select Consultant');", true);
        }

    }

}


