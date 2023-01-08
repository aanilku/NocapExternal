using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Drawing;
using System.IO;

public partial class ExternalUser_InfrastructureNew_SalientFeature : System.Web.UI.Page
{
    string strPageName = "INFSalientFeatures";
    string strActionName = "";
    string strStatus = "";

    long lngInfSubmitAppCode;

    public long InfSubmitAppCode
    {
        get
        {
            return lngInfSubmitAppCode;
        }
        set
        {
            lngInfSubmitAppCode = value;
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
                        if (SourceLabel != null) { lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); }
                    }

                }
                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblInfrastructureApplicationCodeFrom");
                        if (SourceLabel != null) { lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    BindLandUseFormDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    BindMSMEAttachment(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
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
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Master.ExemptionLetterConditions Obj_Exemption = new NOCAP.BLL.Master.ExemptionLetterConditions();
            //lblExemptionMessage.Text =HttpUtility.HtmlEncode("Since proposed area fall in <b>" + Obj_Exemption.AreaTypeDesc() + "(" + Obj_Exemption.AreaTypeCategoryDesc() + ")</b> (GWRE-2011). and proposed net ground water requirement is not more than <b>" + Obj_Exemption.GWWaterQuantityLessThan + "m<sup>3</sup>/day</b>. you are exempted from obtaining NOC for ground water withdrawal. Your application is elligible for exemption");
            //txtSalientFeaturesOfInActivity.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.SalientFeatureOfInfrastructureActivity);
            lblExemptionMessage.Text = HttpUtility.HtmlEncode("You are exempted from seeking NOC for ground water withdrawal from CGWA, since your project is under MSME category and ground water withdrawal is less than 10 KLD as per new guidelines w.e.f 24.09.2020.<br/> Please upload MSME certificate for getting Exemtion.");
            lblExemptionMessage.ForeColor = System.Drawing.Color.Green;
        }
        catch (Exception) { Response.Redirect("~/ExternalErrorPage.aspx", false); }
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
            //Server.Transfer("~/ExternalUser/InfrastructureNew/BreakUpOfWaterRequirment.aspx");
            Server.Transfer("WaterRequirementDetails.aspx");
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
                        UpdateSalientFeaturesDetail(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
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
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
        NOCAP.BLL.Master.ApplicationStop obj_applicationStop = new NOCAP.BLL.Master.ApplicationStop(obj_infrastructureNewApplication.ApplicationTypeCode, obj_infrastructureNewApplication.ApplicationPurposeCode);
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
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            //obj_infrastructureNewApplication.SalientFeatureOfIndustrialActivity = txtSalientFeaturesOfInActivity.Text;
            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_infrastructureNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;
            if (obj_infrastructureNewApplication.Update() == 1)
            {
                lblMessage.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.CustumMessage); //Added New for bringing proper message
                lblMessage.ForeColor = System.Drawing.Color.Green;//Added New for bringing proper message
                return 1;
            }
            else
            {
                lblMessage.Text = obj_infrastructureNewApplication.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
            //lblMessage.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
            return 0;
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
                strActionName = "SubmitSalientFeatures";
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
                arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplicationForNoLimit.GetMSMEAttachmentList();
                if (arr_infrastructureNewApplicationAttachmentList.Count() <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Upload MSME Certificate');", true);

                }
                else
                {
                    if (DisplayApplicationStop() == 1)
                    {
                        if (UpdateSalientFeaturesDetail(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text)) == 1)
                        {
                            long lng_submittedApplicationCode = 0;
                            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                            if (obj_infrastructureNewApplication.SubmitApplication(out lng_submittedApplicationCode, null, obj_externalUser.ExternalUserCode) == 1)
                            {
                                strStatus = "Submit Successfull";
                                lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(lng_submittedApplicationCode);
                                lngInfSubmitAppCode = lng_submittedApplicationCode;
                                Server.Transfer("~/ExternalUser/InfrastructureNew/InfExemSubmitSuccess.aspx");

                            }
                            else
                            {
                                lblMessage.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.CustumMessage);  //Added New for bringing proper message
                                strStatus = "Submit Unsuccessfull";
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Record Saved Failed !";  //Added New for bringing proper message
                            strStatus = "Update Unsuccessfull";
                        }
                    }
                }
            }
            catch (ThreadAbortException ex)
            {
                strActionName = "Submit";
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
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructuregNewApplicationForNoLimit = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
                arr_infrastructureNewApplicationAttachmentList = obj_infrastructuregNewApplicationForNoLimit.GetMSMEAttachmentList();

                if (arr_infrastructureNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_insertInfrastructureNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment> lst_NewApplicationAttachmentList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment>();
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
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadMSME.PostedFile);
                                        obj_insertInfrastructureNewApplicationAttachment.ContentType = FileUploadMSME.PostedFile.ContentType;
                                        obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = FileUploadMSME.FileName;
                                        obj_insertInfrastructureNewApplicationAttachment.FileExtension = str_ext;
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
                        obj_insertInfrastructureNewApplicationAttachment.ApplicationCode = obj_infrastructureNewApplication.ApplicationCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentCode = obj_infrastructureNewApplication.MSMEAttCode;
                        obj_insertInfrastructureNewApplicationAttachment.AttachmentName = txtMSME.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertInfrastructureNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertInfrastructureNewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertInfrastructureNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            lblMSMEMessage.ForeColor = Color.Green;

                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMSMEMessage.ForeColor = System.Drawing.Color.Red;
                        }

                        BindMSMEAttachment(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                        clearMessage();
                        lblMSMEMessage.Text = obj_insertInfrastructureNewApplicationAttachment.CustumMessage;
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
    private void BindMSMEAttachment(long lngA_ApplicationCode)
    {
        try
        {
            int AttCount = 0;
            BindGridView(gvMSME, lngA_ApplicationCode, ref AttCount);
            lblMSME.Text = HttpUtility.HtmlEncode("MSME certificate in case of MSME (" + AttCount.ToString() + ")");


        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
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
    private void BindGridView(GridView gv, long lngA_ApplicationCode, ref int AttCount)
    {
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList = null;

        arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetMSMEAttachmentList();

        gv.DataSource = arr_infrastructureNewApplicationAttachmentList;
        gv.DataBind();
        AttCount = arr_infrastructureNewApplicationAttachmentList.Length;
    }
    private int DeleteAttchment(GridView gv, GridViewDeleteEventArgs e, Label lblMessage, string str_ActionName)
    {
        try
        {
            long lng_MiningNewApplicationCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["ApplicationCode"]);
            int int_AttachmentCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["AttachmentCode"]);
            int int_AttachmentCodeSerialNumber = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
            lblMessage.ForeColor = System.Drawing.Color.Red;
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment obj_miningNewApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment(lng_MiningNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
            if (obj_miningNewApplicationAttachment.Delete() == 1)
            {
                lblMessage.Text = obj_miningNewApplicationAttachment.CustumMessage;
                strStatus = "File Delete Success";
                return 1;
            }
            else
            {
                lblMessage.Text = obj_miningNewApplicationAttachment.CustumMessage;
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
                    BindMSMEAttachment(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

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
                    long lng_miningNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.INFSADAppDownloadFiles(lng_miningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

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
    private void clearMessage()
    {
        lblMSMEMessage.Text = "";
        txtMSME.Text = "";
    }
}