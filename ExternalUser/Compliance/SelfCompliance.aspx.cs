using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Threading;

public partial class ExternalUser_Compliance_SelfCompliance : System.Web.UI.Page
{
    string strPageName = "SelfCompliance";
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
                NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_selfComplianceAttachment;
                arr_selfComplianceAttachment = obj_selfCompliance.GetWaterAuditAttachmentList();


                try
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));

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
                                    obj_selfComplianceAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadWaterAuditInspection.PostedFile);
                                    obj_selfComplianceAttachment.ContentType = FileUploadWaterAuditInspection.PostedFile.ContentType;
                                    obj_selfComplianceAttachment.AttachmentPath = FileUploadWaterAuditInspection.FileName;
                                    obj_selfComplianceAttachment.FileExtension = str_ext;
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
                    obj_selfComplianceAttachment.ApplicationCode = obj_SelfCompliance.ApplicationCode;
                    obj_selfComplianceAttachment.AttachmentCode = obj_SelfCompliance.WaterAuditAttCode;
                    obj_selfComplianceAttachment.AttachmentName = "WaterAudit";

                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    obj_selfComplianceAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;


                    if (obj_selfComplianceAttachment.Add() == 1)
                    {
                        strStatus = "File Upload Success";
                        lblWaterAuditInspection.Text = "File Upload Success";
                        lblWaterAuditInspection.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        lblWaterAuditInspection.Text = obj_selfComplianceAttachment.CustumMessage;
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
                NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_selfComplianceAttachment;
                arr_selfComplianceAttachment = obj_selfCompliance.GetIARModelingAttachmentList();


                try
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));

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
                                    obj_selfComplianceAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadIAR.PostedFile);
                                    obj_selfComplianceAttachment.ContentType = FileUploadIAR.PostedFile.ContentType;
                                    obj_selfComplianceAttachment.AttachmentPath = FileUploadIAR.FileName;
                                    obj_selfComplianceAttachment.FileExtension = str_ext;
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
                    obj_selfComplianceAttachment.ApplicationCode = obj_SelfCompliance.ApplicationCode;
                    obj_selfComplianceAttachment.AttachmentCode = obj_SelfCompliance.IARModelingAttCode;
                    obj_selfComplianceAttachment.AttachmentName = "IAR";

                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    obj_selfComplianceAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;


                    if (obj_selfComplianceAttachment.Add() == 1)
                    {
                        strStatus = "File Upload Success";
                        lblIAR.Text = "File Upload Success";
                        lblIAR.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        lblIAR.Text = obj_selfComplianceAttachment.CustumMessage;
                        lblIAR.ForeColor = System.Drawing.Color.Red;

                    }

                    lblWaterAuditInspection.Text = "";
                    BindGridView(gvIAR, Convert.ToInt64(lblApplicationCodeFrom.Text), ref lblIARCount);

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
                NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_selfComplianceAttachment;
                arr_selfComplianceAttachment = obj_selfCompliance.GetExtraAttachmentList();


                try
                {
                    string str_fname;
                    string str_ext;

                    NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));

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
                                    obj_selfComplianceAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadExtra.PostedFile);
                                    obj_selfComplianceAttachment.ContentType = FileUploadExtra.PostedFile.ContentType;
                                    obj_selfComplianceAttachment.AttachmentPath = FileUploadExtra.FileName;
                                    obj_selfComplianceAttachment.FileExtension = str_ext;
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
                    obj_selfComplianceAttachment.ApplicationCode = obj_SelfCompliance.ApplicationCode;
                    obj_selfComplianceAttachment.AttachmentCode = obj_SelfCompliance.ExtraAttCode;
                    obj_selfComplianceAttachment.AttachmentName = "lblExtra";

                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    obj_selfComplianceAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;


                    if (obj_selfComplianceAttachment.Add() == 1)
                    {
                        strStatus = "File Upload Success";
                        lblExtra.Text = "File Upload Success";
                        lblExtra.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        lblExtra.Text = obj_selfComplianceAttachment.CustumMessage;
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

                    NOCAPExternalUtility.SelfcompDownloadFiles(lng_industrialNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
            NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_SelfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment(lng_ApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);

            if (obj_SelfComplianceAttachment.Delete() == 1)
            {
                lblMessage.Text = obj_SelfComplianceAttachment.CustumMessage;
                strStatus = "File Delete Success";
                return 1;
            }
            else
            {
                lblMessage.Text = obj_SelfComplianceAttachment.CustumMessage;
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
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(lngA_ApplicationCode);

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
                    obj_SelfCompliance.SubSCWithinTimeFrame = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.SubSCWithinTimeFrame = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.SubSCWithinTimeFrame = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.SubSCWithinTimeFrame = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }

            switch (IFddlWaterAuditInspection.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.WaterAuditInspectionApplicable = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.WaterAuditInspectionApplicable = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.WaterAuditInspectionApplicable = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.WaterAuditInspectionApplicable = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            switch (ddlWateAuditinspection.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.WaterAuditInspection = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.WaterAuditInspection = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.WaterAuditInspection = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.WaterAuditInspection = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            switch (ddlWateAuditinspectionNOC.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.WaterAuditInspectionAsPerNOC = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.WaterAuditInspectionAsPerNOC = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.WaterAuditInspectionAsPerNOC = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.WaterAuditInspectionAsPerNOC = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            obj_SelfCompliance.AuditAgency = txtAuditAgency.Text.Trim();
            if (txtDateOfInsp.Text.Trim() != "")
                obj_SelfCompliance.DateOfInspectionAudit = Convert.ToDateTime(txtDateOfInsp.Text.Trim());
            switch (ddlWaterauditreportattached.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.WaterAuditReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.WaterAuditReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.WaterAuditReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.WaterAuditReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }

            switch (ddlImpactassessmentreport.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.ImpactAssementReportApplicable = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.ImpactAssementReportApplicable = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.ImpactAssementReportApplicable = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.ImpactAssementReportApplicable = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            switch (ddlRequirement.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.ImpactAssementReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.ImpactAssementReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.ImpactAssementReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.ImpactAssementReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            switch (ddlSubmittedNOC.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.ImpactAssementReportSubmitted = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.ImpactAssementReportSubmitted = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.ImpactAssementReportSubmitted = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.ImpactAssementReportSubmitted = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            switch (ddlCopyIAR.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.ImpactAssementAIRReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.ImpactAssementAIRReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.ImpactAssementAIRReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.ImpactAssementAIRReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }
            switch (ddlAnyViolation.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.AnyViolationNOCCondi = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.AnyViolationNOCCondi = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.AnyViolationNOCCondi = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.AnyViolationNOCCondi = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }


            obj_SelfCompliance.AnyViolationNOCCondiDesc = txtAnyViolation.Text.Trim();
            switch (ddlAnyothercompliance.SelectedValue.ToString())
            {
                case "1":
                    obj_SelfCompliance.AnyOtherCompliances = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                    break;
                case "0":
                    obj_SelfCompliance.AnyOtherCompliances = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                    break;
                case "":
                    obj_SelfCompliance.AnyOtherCompliances = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
                default:
                    obj_SelfCompliance.AnyOtherCompliances = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                    break;
            }


            obj_SelfCompliance.AnyOtherCompliancesDesc = txtAnyothercompliance.Text.Trim();
            obj_SelfCompliance.Remarks = txtRemarks.Text.Trim();
            obj_SelfCompliance.ComplianceSubmitDate = DateTime.Now;
            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_SelfCompliance.ModifiedByExUC = obj_externalUser.ExternalUserCode;
            if (obj_SelfCompliance.Update() == 1)
            {
                strStatus = "Update Success";
                lblMessage.Text = obj_SelfCompliance.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                strStatus = "Update Failed";
                lblMessage.Text = obj_SelfCompliance.CustumMessage;
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
        NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(lngA_ApplicationCode);
        NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_SelfComplianceAttachment = null;

        if (gv.ID == "gvWaterAuditInspection")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetWaterAuditAttachmentList();
        else if (gv.ID == "gvIAR")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetIARModelingAttachmentList();
        else if (gv.ID == "gvExtra")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetExtraAttachmentList();

        gv.DataSource = arr_SelfComplianceAttachment;
        gv.DataBind();
        AttCount.Text = arr_SelfComplianceAttachment.Length.ToString();
    }
    private void PopulateData(long lngA_ApplicationCode, object sender, EventArgs e)
    {
        try
        {
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(lngA_ApplicationCode);
            if (obj_selfCompliance.ApplicationCode != 0)
            {

                switch (obj_selfCompliance.SubSCWithinTimeFrame)
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
                switch (obj_selfCompliance.WaterAuditInspectionApplicable)
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
                switch (obj_selfCompliance.WaterAuditInspection)
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
                switch (obj_selfCompliance.WaterAuditInspectionAsPerNOC)
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
                txtAuditAgency.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.AuditAgency));
                if (obj_selfCompliance.DateOfInspectionAudit != null)

                    txtDateOfInsp.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_selfCompliance.DateOfInspectionAudit).ToString("dd/MM/yyyy"));
                switch (obj_selfCompliance.WaterAuditReport)
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


                switch (obj_selfCompliance.ImpactAssementReportApplicable)
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
                switch (obj_selfCompliance.ImpactAssementReport)
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
                switch (obj_selfCompliance.ImpactAssementReportSubmitted)
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
                switch (obj_selfCompliance.ImpactAssementAIRReport)
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
                switch (obj_selfCompliance.AnyViolationNOCCondi)
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
                txtAnyViolation.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.AnyViolationNOCCondiDesc));
                switch (obj_selfCompliance.AnyOtherCompliances)
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
                txtAnyothercompliance.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.AnyOtherCompliancesDesc));

                txtRemarks.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.Remarks));

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
            Server.Transfer("SelfComplianceB.aspx");

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
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
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
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                    status = UpdateData(Convert.ToInt64(lblApplicationCodeFrom.Text), sender, e);
                    if (status == 1)
                    {
                        Server.Transfer("~/ExternalUser/Compliance/SelfComplianceSubmit.aspx");
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

   



   
}


