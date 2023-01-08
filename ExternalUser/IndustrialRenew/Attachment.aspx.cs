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
using NOCAP.BLL.Industrial.Renew.SADApplication;

public partial class ExternalUser_IndustrialRenew_Attachment : System.Web.UI.Page
{
    string strPageName = "INDRenewAttachment";
    string strActionName = "";
    string strStatus = "";
    //decimal GroundWaterRequirement0KLD = 0;
    //decimal GroundWaterRequirement10KLD = 10;
    decimal GroundWaterRequirement100KLD = 100;
    long lngIndRenewSubmitAppCode;
    public long IndRenewSubmitAppCode
    {
        get
        {
            return lngIndRenewSubmitAppCode;
        }
        set
        {
            lngIndRenewSubmitAppCode = value;
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


                // if (NOCAPExternalUtility.FillDropDownReferralLetterType(ref ddlReferralLetter) != 1)
                // {
                //  lblMessageReferralLetter.Text = "Problem in ReferralLetter Population!";
                // }
                //else
                // {
                //  ddlReferralLetter.Items[0].Value = "0";
                // }

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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblIndustialApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                        }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {



                    lblApplicationCode.Text = lblIndustialApplicationCodeFrom.Text;
                    BindIndustrialRenewApplicationAttachmentExisingNOCDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    BindIndustrialRenewApplicationAttachmentRainwaterHarvestingDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    BindIndustrialRenewApplicationComplianceConditionNOC(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    BindIndustrialRenewComplianceConditionNOCAttachmentDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    BindAffidavitAttachmentDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    BindWaterAuditReportAttachmentDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    BindSourceWaterNonAvailabilityCertificateAttachmentDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

                    BindIndustrialRenewApplicationAttachmentBharatKoshRecieptDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

                    BindIndustrialRenewApplicationAttachmentSignedDocDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));


                    BindIndustrialRenewApplicationAttachmentExtraDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));


                    // BindIndustrialRenewApplicationAttachmentGroundwaterAvailabilityDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));


                    // BindIndustrialRenewApplicationAttachmentUndertakingDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));


                    // BindIndustrialRenewApplicationComplianceConditionNOCOther();
                    //  BindIndustrialRenewComplianceConditionNOCOtherAttachmentDetails();

                    //BindIndustrialNewApplicationAttachmentRefferalLetterDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    //BindIndustrialNewApplicationAttachmentNonPollutingDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                }

                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_IndustrialRenewApplication.NameOfIndustry);
            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }

    }


    private void BindGridView(GridView gv, long lngA_ApplicationCode, ref decimal decTotalGroundWaterRequirement, ref int AttCount)
    {
        IndustrialRenewSADApplication obj_industrialRenewApplication = new IndustrialRenewSADApplication(lngA_ApplicationCode);
        IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList = null;

        if (gv.ID == "gvAffidavit")
            arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetAffidavitNOCCondiAttachmentList();
        else if (gv.ID == "gvWaterAuditReport")
            arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetWaterAuditReportAttachmentList();
         else if (gv.ID == "gvSourceofAvailabilityofSurfaceWater")
           arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetCertificateNonAvaAttachmentList();
        // else if (gv.ID == "gvRainwaterHarvesting")
        //  arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetRainwaterHarvestingAttachmentList();
        // else if (gv.ID == "gvImpactReportOCS")
        //   arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetImpactAssOCSAttachmentList();
        // else if (gv.ID == "gvReferralLetterAttachment")
        //   arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetCpoyOfReferralLetterAttachmentList();
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
        //   arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetAffidavitOtherMSMEAttachmentList();
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
        //arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetBharatKoshRecieptAttachmentList();

        // else if (gv.ID == "gvSigneddoc")
        // arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetSigneddocAttachmentList();
        // else if (gv.ID == "gvPenalty")
        //   arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetPenaltyAttachmentList();

        // else if (gv.ID == "gvExtra")
        //  arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetExtraAttachmentList();

        gv.DataSource = arr_industrialRenewApplicationAttachmentList;
        gv.DataBind();
        AttCount = arr_industrialRenewApplicationAttachmentList.Length;
        decTotalGroundWaterRequirement = Convert.ToDecimal(obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist + obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional);// + (obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));
    }
    private int DeleteAttchment(GridView gv, GridViewDeleteEventArgs e, Label lblMessage, string str_ActionName)
    {
        try
        {
            long lng_IndustrialRenewApplicationCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["IndustrialRenewApplicationCode"]);
            int int_AttachmentCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["AttachmentCode"]);
            int int_AttachmentCodeSerialNumber = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
            lblMessage.ForeColor = System.Drawing.Color.Red;
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_industrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment(lng_IndustrialRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
            if (obj_industrialRenewApplicationAttachment.Delete() == 1)
            {
                lblMessage.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
                strStatus = "File Delete Success";
                return 1;
            }
            else
            {
                lblMessage.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
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
                    long lng_industrialRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.INDRenewSADAppDownloadFiles(lng_industrialRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

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


    #region Upload Buton
    protected void btnUplodWaterRequirement_Click(object sender, EventArgs e)
    {
        //    if (Page.IsValid)
        //    {
        //        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        //        {
        //            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        //        }
        //        else
        //        {
        //            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
        //            Session["CSRF"] = hidCSRF.Value;
        //            strActionName = "File Upload Water Requirement";
        //            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplicationForNoLimit = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
        //            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
        //            arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplicationForNoLimit.GetWaterRequrementAttachmentList();

        //            if (arr_industrialRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
        //            {
        //                string str_fname;
        //                string str_ext;
        //                string str_newFileNameWithPath = "";
        //                string str_restPath = "";

        //                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_insertIndustrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment();
        //                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
        //                List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment>();

        //                byte[] buffer = new byte[1];
        //                try
        //                {
        //                    if (FileUploadWaterRequirement.HasFile)
        //                    {
        //                        str_ext = System.IO.Path.GetExtension(FileUploadWaterRequirement.PostedFile.FileName).ToLower();
        //                        str_fname = FileUploadWaterRequirement.FileName;

        //                        if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
        //                        {
        //                            if (NOCAPExternalUtility.IsValidFile(FileUploadWaterRequirement.PostedFile))
        //                            {
        //                                if (FileUploadWaterRequirement.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
        //                                {
        //                                    obj_insertIndustrialRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadWaterRequirement.PostedFile);
        //                                    obj_insertIndustrialRenewApplicationAttachment.ContentType = FileUploadWaterRequirement.PostedFile.ContentType;
        //                                    obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = FileUploadWaterRequirement.FileName;
        //                                    obj_insertIndustrialRenewApplicationAttachment.FileExtension = str_ext;
        //                                }
        //                                else
        //                                {
        //                                    strStatus = "File Upload Failed";
        //                                    lblMessageWaterRequirement.Text = "File can not upload. It has more than 5 MB size";
        //                                    lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
        //                                    return;
        //                                }
        //                            }
        //                            else
        //                            {
        //                                strStatus = "File Upload Failed";
        //                                lblMessageWaterRequirement.Text = "Not a valid file!!..Select an other file!!";
        //                                lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
        //                                return;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            strStatus = "File Upload Failed";
        //                            lblMessageWaterRequirement.Text = "Not a valid file!!..Select an other file!!";
        //                            lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
        //                            return;
        //                        }

        //                    }
        //                    else
        //                    {
        //                        lblMessageWaterRequirement.Text = "Please select a file..!!";
        //                        lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
        //                        return;
        //                    }
        //                    obj_insertIndustrialRenewApplicationAttachment.IndustrialRenewApplicationCode = obj_industrialRenewApplication.IndustrialRenewApplicationCode;
        //                    obj_insertIndustrialRenewApplicationAttachment.AttachmentCode = obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.WaterRequrementAttachCode;
        //                    obj_insertIndustrialRenewApplicationAttachment.AttachmentName = txtWaterRequirement.Text;

        //                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
        //                    obj_insertIndustrialRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

        //                    if (obj_insertIndustrialRenewApplicationAttachment.Add() == 1)
        //                    {
        //                        strStatus = "File Upload Success";
        //                        obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
        //                    }
        //                    else
        //                    {
        //                        strStatus = "File Upload Failed";
        //                        lblMessageWaterRequirement.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
        //                        lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
        //                    }

        //                    lblMessageWaterRequirement.ForeColor = Color.Green;
        //                    lblMessageWaterRequirement.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
        //                    txtWaterRequirement.Text = "";
        //                    BindIndustrialRenewApplicationAttachmentWaterRequirementDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
        //                }
        //                catch (Exception)
        //                {
        //                    Response.Redirect("~/ExternalErrorPage.aspx", false);
        //                }
        //                finally
        //                {
        //                    ActionTrail obj_ExtActionTrail = new ActionTrail();
        //                    if (Session["ExternalUserCode"] != null)
        //                    {
        //                        obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
        //                        obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
        //                        obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
        //                        obj_ExtActionTrail.Status = strStatus;
        //                        if (obj_ExtActionTrail != null)
        //                            ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                lblMessageWaterRequirement.Text = "Maximum number of files to be uploaded is 5";
        //                lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
        //            }
        //        }
        //    }
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
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplicationForNoLimit = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
                arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplicationForNoLimit.GetGroundwaterAvailabilityAttachmentList();

                if (arr_industrialRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;

                    string str_newFileNameWithPath = "";

                    string str_restPath = "";

                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_insertIndustrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment();
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment>();

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
                                    if (FileUploadGroundwaterAvailability.PostedFile.ContentLength <NOCAPExternalUtility.ExternalAttachmentSizeLimit())
                                    {
                                        obj_insertIndustrialRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadGroundwaterAvailability.PostedFile);
                                        obj_insertIndustrialRenewApplicationAttachment.ContentType = FileUploadGroundwaterAvailability.PostedFile.ContentType;
                                        obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = FileUploadGroundwaterAvailability.FileName;
                                        obj_insertIndustrialRenewApplicationAttachment.FileExtension = str_ext;
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
                        obj_insertIndustrialRenewApplicationAttachment.IndustrialRenewApplicationCode = obj_industrialRenewApplication.IndustrialRenewApplicationCode;
                        obj_insertIndustrialRenewApplicationAttachment.AttachmentCode = obj_industrialRenewApplication.GroundWaterAvailability.GroundWaterAvailabilityAttachCode;
                        obj_insertIndustrialRenewApplicationAttachment.AttachmentName = txtgvGroundwaterAvailability.Text;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertIndustrialRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageGroundwaterAvailability.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
                            lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                        }
                        lblMessageGroundwaterAvailability.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
                        lblMessageGroundwaterAvailability.ForeColor = Color.Green;
                        txtgvGroundwaterAvailability.Text = "";
                        BindIndustrialRenewApplicationAttachmentGroundwaterAvailabilityDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
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
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplicationForNoLimit = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
                arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplicationForNoLimit.GetRainwaterHarvestingAttachmentList();

                if (arr_industrialRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;

                    string str_newFileNameWithPath = "";

                    string str_restPath = "";

                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_insertIndustrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment();
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment>();

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
                                        obj_insertIndustrialRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadRainwaterHarvesting.PostedFile);
                                        obj_insertIndustrialRenewApplicationAttachment.ContentType = FileUploadRainwaterHarvesting.PostedFile.ContentType;
                                        obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = FileUploadRainwaterHarvesting.FileName;
                                        obj_insertIndustrialRenewApplicationAttachment.FileExtension = str_ext;

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
                        obj_insertIndustrialRenewApplicationAttachment.IndustrialRenewApplicationCode = obj_industrialRenewApplication.IndustrialRenewApplicationCode;
                        obj_insertIndustrialRenewApplicationAttachment.AttachmentCode = obj_industrialRenewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeAttachCode;
                        obj_insertIndustrialRenewApplicationAttachment.AttachmentName = txtRainwaterHarvesting.Text;

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (obj_insertIndustrialRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageRainwaterHarvesting.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
                            lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                        }

                        lblMessageRainwaterHarvesting.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
                        lblMessageRainwaterHarvesting.ForeColor = Color.Green;
                        txtRainwaterHarvesting.Text = "";
                        BindIndustrialRenewApplicationAttachmentRainwaterHarvestingDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
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
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplicationForNoLimit = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
                    arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplicationForNoLimit.GetUndertakingAttachmentList();

                    if (arr_industrialRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                    {
                        string str_fname;
                        string str_ext;

                        string str_newFileNameWithPath = "";

                        string str_restPath = "";

                        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_insertIndustrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment();
                        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment>();

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
                                            obj_insertIndustrialRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadUndertaking.PostedFile);
                                            obj_insertIndustrialRenewApplicationAttachment.ContentType = FileUploadUndertaking.PostedFile.ContentType;
                                            obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = FileUploadUndertaking.FileName;
                                            obj_insertIndustrialRenewApplicationAttachment.FileExtension = str_ext;
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
                            obj_insertIndustrialRenewApplicationAttachment.IndustrialRenewApplicationCode = obj_industrialRenewApplication.IndustrialRenewApplicationCode;
                            obj_insertIndustrialRenewApplicationAttachment.AttachmentCode = obj_industrialRenewApplication.Undertaking.UndertakingAttachCode;

                            obj_insertIndustrialRenewApplicationAttachment.AttachmentName = txtUndertaking.Text;

                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                            obj_insertIndustrialRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                            if (obj_insertIndustrialRenewApplicationAttachment.Add() == 1)
                            {
                                strStatus = "File Upload Success";
                                obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageUndertaking.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
                                lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                            lblMessageUndertaking.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
                            lblMessageUndertaking.ForeColor = Color.Green;
                            txtUndertaking.Text = "";
                            BindIndustrialRenewApplicationAttachmentUndertakingDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
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
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplicationForNoLimit = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
                arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplicationForNoLimit.GetAffidavitNOCCondiAttachmentList();

                if (arr_industrialRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_insertIndustrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment();
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment>();
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
                                        obj_insertIndustrialRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadAffidavit.PostedFile);
                                        obj_insertIndustrialRenewApplicationAttachment.ContentType = FileUploadAffidavit.PostedFile.ContentType;
                                        obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = FileUploadAffidavit.FileName;
                                        obj_insertIndustrialRenewApplicationAttachment.FileExtension = str_ext;
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
                        obj_insertIndustrialRenewApplicationAttachment.IndustrialRenewApplicationCode = obj_industrialRenewApplication.IndustrialRenewApplicationCode;
                        obj_insertIndustrialRenewApplicationAttachment.AttachmentCode = obj_industrialRenewApplication.AffidavitNOCCondiAttCode;
                        obj_insertIndustrialRenewApplicationAttachment.AttachmentName = txtAffidavit.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertIndustrialRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageAffidavit.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
                            lblMessageAffidavit.ForeColor = System.Drawing.Color.Red;

                        }


                        BindAffidavitAttachmentDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageAffidavit.ForeColor = Color.Green;
                        lblMessageAffidavit.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;

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
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplicationForNoLimit = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
                arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplicationForNoLimit.GetWaterAuditReportAttachmentList();

                if (arr_industrialRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_insertIndustrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment();
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment>();
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
                                        obj_insertIndustrialRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadWaterAuditReport.PostedFile);
                                        obj_insertIndustrialRenewApplicationAttachment.ContentType = FileUploadWaterAuditReport.PostedFile.ContentType;
                                        obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = FileUploadWaterAuditReport.FileName;
                                        obj_insertIndustrialRenewApplicationAttachment.FileExtension = str_ext;
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
                        obj_insertIndustrialRenewApplicationAttachment.IndustrialRenewApplicationCode = obj_industrialRenewApplication.IndustrialRenewApplicationCode;
                        obj_insertIndustrialRenewApplicationAttachment.AttachmentCode = obj_industrialRenewApplication.WaterAuditReportAttCode;
                        obj_insertIndustrialRenewApplicationAttachment.AttachmentName = txtWaterAuditReport.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertIndustrialRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageWaterAuditReport.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
                            lblMessageWaterAuditReport.ForeColor = System.Drawing.Color.Red;

                        }


                        BindWaterAuditReportAttachmentDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageWaterAuditReport.ForeColor = Color.Green;
                        lblMessageWaterAuditReport.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;

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
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplicationForNoLimit = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
                arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplicationForNoLimit.GetWaterAuditReportAttachmentList();

                if (arr_industrialRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_insertIndustrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment();
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment>();
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
                                        obj_insertIndustrialRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(txtFileUploadSourceofAvailabilityofSurfaceWater.PostedFile);
                                        obj_insertIndustrialRenewApplicationAttachment.ContentType = txtFileUploadSourceofAvailabilityofSurfaceWater.PostedFile.ContentType;
                                        obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = txtFileUploadSourceofAvailabilityofSurfaceWater.FileName;
                                        obj_insertIndustrialRenewApplicationAttachment.FileExtension = str_ext;
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
                        obj_insertIndustrialRenewApplicationAttachment.IndustrialRenewApplicationCode = obj_industrialRenewApplication.IndustrialRenewApplicationCode;
                        obj_insertIndustrialRenewApplicationAttachment.AttachmentCode = obj_industrialRenewApplication.CertiNonAvaAttCode;
                        obj_insertIndustrialRenewApplicationAttachment.AttachmentName = txtSourceofAvailabilityofSurfaceWater.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertIndustrialRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageSourceofAvailabilityofSurfaceWater.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
                            lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = System.Drawing.Color.Red;

                        }


                        BindSourceWaterNonAvailabilityCertificateAttachmentDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        clearMessage();
                        lblMessageSourceofAvailabilityofSurfaceWater.ForeColor = Color.Green;
                        lblMessageSourceofAvailabilityofSurfaceWater.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;

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
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplicationForNoLimit = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
                arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplicationForNoLimit.GetBharatKoshRecieptAttachmentList();

                if (arr_industrialRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_insertIndustrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment();
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment>();
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
                                        obj_insertIndustrialRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadBharatKoshReciept.PostedFile);
                                        obj_insertIndustrialRenewApplicationAttachment.ContentType = FileUploadBharatKoshReciept.PostedFile.ContentType;
                                        obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = FileUploadBharatKoshReciept.FileName;
                                        obj_insertIndustrialRenewApplicationAttachment.FileExtension = str_ext;
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
                        obj_insertIndustrialRenewApplicationAttachment.IndustrialRenewApplicationCode = obj_industrialRenewApplication.IndustrialRenewApplicationCode;
                        obj_insertIndustrialRenewApplicationAttachment.AttachmentCode = obj_industrialRenewApplication.IndustrialRenewBharatKoshRecieptAttachmentCode;
                        obj_insertIndustrialRenewApplicationAttachment.AttachmentName = txtBharatKoshReciept.Text;
                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertIndustrialRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageBharatKoshReciept.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
                            lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;

                        }

                        lblMessageBharatKoshReciept.ForeColor = Color.Green;
                        lblMessageBharatKoshReciept.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
                        txtBharatKoshReciept.Text = "";
                        BindIndustrialRenewApplicationAttachmentBharatKoshRecieptDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

                        lblMessageExtra.Text = "";
                        lblMessageGroundwaterAvailability.Text = "";
                        lblMessageRainwaterHarvesting.Text = "";
                        //lblMessageSourceofAvailabilityofSurfaceWater.Text = "";
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
                IndustrialRenewSADApplication obj_industrialRenewApplicationForNoLimit = new IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
                arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplicationForNoLimit.GetBharatKoshRecieptAttachmentList();

                if (arr_industrialRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    IndustrialRenewSADApplicationAttachment obj_insertIndustrialRenewApplicationAttachment = new IndustrialRenewSADApplicationAttachment();
                    IndustrialRenewSADApplication obj_industrialRenewApplication = new IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<IndustrialRenewSADApplicationAttachment>();
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
                                        obj_insertIndustrialRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadApplicationSignatureSeal.PostedFile);
                                        obj_insertIndustrialRenewApplicationAttachment.ContentType = FileUploadApplicationSignatureSeal.PostedFile.ContentType;
                                        obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = FileUploadApplicationSignatureSeal.FileName;
                                        obj_insertIndustrialRenewApplicationAttachment.FileExtension = str_ext;
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMessageApplicationSignatureSeal.Text = "File can not upload. It has more than 5 MB size";
                                        lblMessageApplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessageApplicationSignatureSeal.Text = "Not a valid file!!..Select an other file!!";
                                    lblMessageApplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageApplicationSignatureSeal.Text = "Not a valid file!!..Select an other file!!";
                                lblMessageApplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                        else
                        {
                            lblMessageApplicationSignatureSeal.Text = "Please select a file..!!";
                            lblMessageApplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        obj_insertIndustrialRenewApplicationAttachment.IndustrialRenewApplicationCode = obj_industrialRenewApplication.IndustrialRenewApplicationCode;
                        obj_insertIndustrialRenewApplicationAttachment.AttachmentCode = obj_industrialRenewApplication.SignedDocAttCode;
                        obj_insertIndustrialRenewApplicationAttachment.AttachmentName = txtApplicationSignatureSeal.Text;
                        ExternalUser obj_externalUser = new ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                        if (obj_insertIndustrialRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageApplicationSignatureSeal.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
                            lblMessageApplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;

                        }

                        lblMessageApplicationSignatureSeal.ForeColor = Color.Green;
                        lblMessageApplicationSignatureSeal.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
                        txtApplicationSignatureSeal.Text = "";
                        BindIndustrialRenewApplicationAttachmentSignedDocDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

                        lblMessageExtra.Text = "";
                        lblMessageGroundwaterAvailability.Text = "";
                        lblMessageRainwaterHarvesting.Text = "";
                        lblMessageUndertaking.Text = "";
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
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplicationForNoLimit = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
                arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplicationForNoLimit.GetExtraAttachmentList();

                if (arr_industrialRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;

                    string str_newFileNameWithPath = "";

                    string str_restPath = "";

                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_insertIndustrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment();
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment>();

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

                                        obj_insertIndustrialRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadExtra.PostedFile);
                                        obj_insertIndustrialRenewApplicationAttachment.ContentType = FileUploadExtra.PostedFile.ContentType;
                                        obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = FileUploadExtra.FileName;
                                        obj_insertIndustrialRenewApplicationAttachment.FileExtension = str_ext;


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
                        obj_insertIndustrialRenewApplicationAttachment.IndustrialRenewApplicationCode = obj_industrialRenewApplication.IndustrialRenewApplicationCode;
                        obj_insertIndustrialRenewApplicationAttachment.AttachmentCode = obj_industrialRenewApplication.IndustrialRenewExtraAttachmentCode;

                        obj_insertIndustrialRenewApplicationAttachment.AttachmentName = txtExtraAttachment.Text; ;


                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        obj_insertIndustrialRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;



                        if (obj_insertIndustrialRenewApplicationAttachment.Add() == 1)
                        {
                            strStatus = "File Upload Success";
                            obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessageExtra.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
                            lblMessageExtra.ForeColor = System.Drawing.Color.Red;

                        }

                        lblMessageExtra.ForeColor = Color.Green;
                        lblMessageExtra.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
                        txtExtraAttachment.Text = "";
                        BindIndustrialRenewApplicationAttachmentExtraDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        //lblMessageCertifiedRevenueSketch.Text = "";
                        //lblMessageReasonForNotApplyingBefore.Text = "";
                        lblMessageGroundwaterAvailability.Text = "";
                        lblMessageRainwaterHarvesting.Text = "";
                        lblMessageExistingNOC.Text = "";
                        lblMessageUndertaking.Text = "";
                        //lblMessageWaterRequirement.Text = "";
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
    protected void btnUploadSitePlan_Click(object sender, EventArgs e)
    {
        //    if (Page.IsValid)
        //    {
        //        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        //        {
        //            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        //        }
        //        else
        //        {

        //            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
        //            Session["CSRF"] = hidCSRF.Value;

        //            strActionName = "UploadSitePlan";
        //            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplicationForNoLimit = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
        //            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
        //            arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplicationForNoLimit.GetSitePlanAttachmentList();

        //            if (arr_industrialRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
        //            {
        //                try
        //                {
        //                    string str_fname;
        //                    string str_ext;

        //                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_insertIndustrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment();
        //                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
        //                    List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment>();

        //                    byte[] buffer = new byte[1];

        //                    if (FileUploadSitePlan.HasFile)
        //                    {
        //                        str_ext = System.IO.Path.GetExtension(FileUploadSitePlan.PostedFile.FileName).ToLower();

        //                        str_fname = FileUploadSitePlan.FileName;
        //                        if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
        //                        {
        //                            if (NOCAPExternalUtility.IsValidFile(FileUploadSitePlan.PostedFile))
        //                            {
        //                                if (FileUploadSitePlan.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
        //                                {

        //                                    obj_insertIndustrialRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadSitePlan.PostedFile);
        //                                    obj_insertIndustrialRenewApplicationAttachment.ContentType = FileUploadSitePlan.PostedFile.ContentType;
        //                                    obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = FileUploadSitePlan.FileName;
        //                                    obj_insertIndustrialRenewApplicationAttachment.FileExtension = str_ext;
        //                                }
        //                                else
        //                                {
        //                                    strStatus = "File Upload Failed";
        //                                    lblMessageSitePlan.Text = "File can not upload. It has more than 5 MB size";
        //                                    lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
        //                                    return;
        //                                }
        //                            }
        //                            else
        //                            {
        //                                strStatus = "File Upload Failed";
        //                                lblMessageSitePlan.Text = "Not a valid file!!..Select an other file!!";
        //                                lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
        //                                return;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            strStatus = "File Upload Failed";
        //                            lblMessageSitePlan.Text = "Not a valid file!!..Select an other file!!";
        //                            lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
        //                            return;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        lblMessageSitePlan.Text = "Please select a file..!!";
        //                        lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
        //                        return;
        //                    }

        //                    obj_insertIndustrialRenewApplicationAttachment.IndustrialRenewApplicationCode = obj_industrialRenewApplication.IndustrialRenewApplicationCode;
        //                    obj_insertIndustrialRenewApplicationAttachment.AttachmentCode = obj_industrialRenewApplication.RenewProLocSitePlanAttachmentCode;
        //                    obj_insertIndustrialRenewApplicationAttachment.AttachmentName = txtSitePlanAttachment.Text;

        //                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
        //                    obj_insertIndustrialRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

        //                    if (obj_insertIndustrialRenewApplicationAttachment.Add() == 1)
        //                    {
        //                        strStatus = "File Upload Success";
        //                        lblMessageSitePlan.Text = "File Upload Success";
        //                        lblMessageSitePlan.ForeColor = System.Drawing.Color.Green;
        //                    }
        //                    else
        //                    {
        //                        strStatus = "File Upload Failed";
        //                        lblMessageSitePlan.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
        //                        lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
        //                    }

        //                    lblMessageSitePlan.ForeColor = Color.Green;
        //                    txtSitePlanAttachment.Text = "";
        //                    BindIndustrialRenewApplicationAttachmentSitePlanDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
        //                    //lblMessageCertifiedRevenueSketch.Text = "";
        //                    //lblMessageReasonForNotApplyingBefore.Text = "";
        //                    lblMessageGroundwaterAvailability.Text = "";
        //                    lblMessageRainwaterHarvesting.Text = "";
        //                    lblMessageExistingNOC.Text = "";
        //                    lblMessageUndertaking.Text = "";
        //                    //lblMessageWaterRequirement.Text = "";

        //                }
        //                catch (Exception)
        //                {
        //                    Response.Redirect("~/ExternalErrorPage.aspx", false);
        //                }
        //                finally
        //                {
        //                    ActionTrail obj_ExtActionTrail = new ActionTrail();
        //                    if (Session["ExternalUserCode"] != null)
        //                    {
        //                        obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
        //                        obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
        //                        obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
        //                        obj_ExtActionTrail.Status = strStatus;
        //                        if (obj_ExtActionTrail != null)
        //                            ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                lblMessageSitePlan.Text = "Maximum number of files to be uploaded is 5";
        //                lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
        //            }
        //        }
        //    }
    }
    protected void btnUplodReasonForNotApplyingBefore_Click(object sender, EventArgs e)
    {
        //    if (Page.IsValid)
        //    {
        //        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        //        {
        //            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        //        }
        //        else
        //        {

        //            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
        //            Session["CSRF"] = hidCSRF.Value;
        //            strActionName = "File Upload Reason For Not Applying Before";
        //            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplicationForNoLimit = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
        //            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
        //            arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplicationForNoLimit.GetReasonForNotApplyingBeforeAttachmentList();

        //            if (arr_industrialRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
        //            {
        //                string str_fname;
        //                string str_ext;
        //                string str_newFileNameWithPath = "";
        //                string str_restPath = "";

        //                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_insertIndustrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment();
        //                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
        //                List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment>();
        //                byte[] buffer = new byte[1];
        //                try
        //                {
        //                    if (txtFileUploadReasonForNotApplyingBefore.HasFile)
        //                    {
        //                        str_ext = System.IO.Path.GetExtension(txtFileUploadReasonForNotApplyingBefore.PostedFile.FileName).ToLower();
        //                        str_fname = txtFileUploadReasonForNotApplyingBefore.FileName;
        //                        if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
        //                        {
        //                            if (NOCAPExternalUtility.IsValidFile(txtFileUploadReasonForNotApplyingBefore.PostedFile))
        //                            {
        //                                if (txtFileUploadReasonForNotApplyingBefore.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
        //                                {
        //                                    obj_insertIndustrialRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(txtFileUploadReasonForNotApplyingBefore.PostedFile);
        //                                    obj_insertIndustrialRenewApplicationAttachment.ContentType = txtFileUploadReasonForNotApplyingBefore.PostedFile.ContentType;
        //                                    obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = txtFileUploadReasonForNotApplyingBefore.FileName;
        //                                    obj_insertIndustrialRenewApplicationAttachment.FileExtension = str_ext;
        //                                }
        //                                else
        //                                {
        //                                    strStatus = "File Deleting Failed";
        //                                    lblMessageReasonForNotApplyingBefore.Text = "File can not upload. It has more than 5 MB size";
        //                                    lblMessageReasonForNotApplyingBefore.ForeColor = System.Drawing.Color.Red;
        //                                    return;
        //                                }
        //                            }
        //                            else
        //                            {
        //                                strStatus = "File Deleting Failed";
        //                                lblMessageReasonForNotApplyingBefore.Text = "Not a valid file!!..Select an other file!!";
        //                                lblMessageReasonForNotApplyingBefore.ForeColor = System.Drawing.Color.Red;
        //                                return;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            strStatus = "File Deleting Failed";
        //                            lblMessageReasonForNotApplyingBefore.Text = "Not a valid file!!..Select an other file!!";
        //                            lblMessageReasonForNotApplyingBefore.ForeColor = System.Drawing.Color.Red;
        //                            return;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        lblMessageReasonForNotApplyingBefore.Text = "Please select a file..!!";
        //                        lblMessageReasonForNotApplyingBefore.ForeColor = System.Drawing.Color.Red;
        //                        return;
        //                    }

        //                    obj_insertIndustrialRenewApplicationAttachment.IndustrialRenewApplicationCode = obj_industrialRenewApplication.IndustrialRenewApplicationCode;
        //                    obj_insertIndustrialRenewApplicationAttachment.AttachmentCode = obj_industrialRenewApplication.IndustrialRenewExistingNOC.ExistingNOCReasonForNotApplyBeforeExpiryAttachCode;
        //                    obj_insertIndustrialRenewApplicationAttachment.AttachmentName = txtReasonForNotApplyingBefore.Text;
        //                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
        //                    obj_insertIndustrialRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

        //                    if (obj_insertIndustrialRenewApplicationAttachment.Add() == 1)
        //                    {
        //                        strStatus = "File Deleted Success";
        //                        obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
        //                    }
        //                    else
        //                    {
        //                        strStatus = "File Deleting Failed";
        //                        lblMessageReasonForNotApplyingBefore.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
        //                        lblMessageReasonForNotApplyingBefore.ForeColor = System.Drawing.Color.Red;
        //                    }
        //                    lblMessageReasonForNotApplyingBefore.ForeColor = Color.Green;
        //                    lblMessageReasonForNotApplyingBefore.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
        //                    txtReasonForNotApplyingBefore.Text = "";
        //                    BindIndustrialRenewApplicationAttachmentReasonForNotApplyingBeforeDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
        //                }
        //                catch (Exception)
        //                {
        //                    Response.Redirect("~/ExternalErrorPage.aspx", false);
        //                }
        //                finally
        //                {
        //                    ActionTrail obj_ExtActionTrail = new ActionTrail();
        //                    if (Session["ExternalUserCode"] != null)
        //                    {
        //                        obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
        //                        obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
        //                        obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
        //                        obj_ExtActionTrail.Status = strStatus;
        //                        if (obj_ExtActionTrail != null)
        //                            ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                lblMessageReasonForNotApplyingBefore.Text = "Maximum number of files to be uploaded is 5";
        //                lblMessageReasonForNotApplyingBefore.ForeColor = System.Drawing.Color.Red;
        //            }
        //        }
        //    }
    }



    protected void btnUploadCertifiedRevenueSketch_Click(object sender, EventArgs e)
    {
        //    if (Page.IsValid)
        //    {
        //        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        //        {
        //            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        //        }
        //        else
        //        {

        //            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
        //            Session["CSRF"] = hidCSRF.Value;

        //            strActionName = "File Upload Certificate of Revenue Sketch";
        //            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplicationForNoLimit = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
        //            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
        //            arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplicationForNoLimit.GetCertifiedRevenueSketAttachmentList();

        //            if (arr_industrialRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
        //            {
        //                try
        //                {
        //                    string str_fname;
        //                    string str_ext;
        //                    string str_newFileNameWithPath = "";
        //                    string str_restPath = "";

        //                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_insertIndustrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment();
        //                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
        //                    List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment>();

        //                    byte[] buffer = new byte[1];

        //                    if (txtFileUploadCertifiedRevenueSketch.HasFile)
        //                    {

        //                        str_ext = System.IO.Path.GetExtension(txtFileUploadCertifiedRevenueSketch.PostedFile.FileName).ToLower();

        //                        str_fname = txtFileUploadCertifiedRevenueSketch.FileName;

        //                        if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
        //                        {

        //                            if (NOCAPExternalUtility.IsValidFile(txtFileUploadCertifiedRevenueSketch.PostedFile))
        //                            {

        //                                if (txtFileUploadCertifiedRevenueSketch.PostedFile.ContentLength < NOCAPExternalUtility.ExternalAttachmentSizeLimit())
        //                                {
        //                                    obj_insertIndustrialRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(txtFileUploadCertifiedRevenueSketch.PostedFile);
        //                                    obj_insertIndustrialRenewApplicationAttachment.ContentType = txtFileUploadCertifiedRevenueSketch.PostedFile.ContentType;
        //                                    obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = txtFileUploadCertifiedRevenueSketch.FileName;
        //                                    obj_insertIndustrialRenewApplicationAttachment.FileExtension = str_ext;
        //                                }
        //                                else
        //                                {
        //                                    strStatus = "File Upload Failed";
        //                                    lblMessageCertifiedRevenueSketch.Text = "File can not upload. It has more than 5 MB size";
        //                                    lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
        //                                    return;
        //                                }
        //                            }
        //                            else
        //                            {
        //                                strStatus = "File Upload Failed";
        //                                lblMessageCertifiedRevenueSketch.Text = "Not a valid file!!..Select an other file!!";
        //                                lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
        //                                return;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            strStatus = "File Upload Failed";
        //                            lblMessageCertifiedRevenueSketch.Text = "Not a valid file!!..Select an other file!!";
        //                            lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
        //                            return;
        //                        }

        //                    }
        //                    else
        //                    {
        //                        lblMessageCertifiedRevenueSketch.Text = "Please select a file..!!";
        //                        lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
        //                        return;
        //                    }

        //                    obj_insertIndustrialRenewApplicationAttachment.IndustrialRenewApplicationCode = obj_industrialRenewApplication.IndustrialRenewApplicationCode;
        //                    obj_insertIndustrialRenewApplicationAttachment.AttachmentCode = obj_industrialRenewApplication.RenewProLocCertReveSketAttachmentCode;
        //                    obj_insertIndustrialRenewApplicationAttachment.AttachmentName = txtCertifiedRevenueSketchAttachment.Text;

        //                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
        //                    obj_insertIndustrialRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

        //                    if (obj_insertIndustrialRenewApplicationAttachment.Add() == 1)
        //                    {
        //                        strStatus = "File Upload Failed";
        //                        obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
        //                    }
        //                    else
        //                    {
        //                        strStatus = "File Upload Failed";
        //                        lblMessageCertifiedRevenueSketch.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
        //                        lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
        //                    }

        //                    lblMessageCertifiedRevenueSketch.ForeColor = Color.Green;
        //                    lblMessageCertifiedRevenueSketch.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
        //                    BindIndustrialRenewApplicationAttachmentCertifiedRevenueSketchDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
        //                }

        //                catch (Exception)
        //                {
        //                    Response.Redirect("~/ExternalErrorPage.aspx", false);
        //                }
        //                finally
        //                {
        //                    ActionTrail obj_ExtActionTrail = new ActionTrail();
        //                    if (Session["ExternalUserCode"] != null)
        //                    {
        //                        obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
        //                        obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
        //                        obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
        //                        obj_ExtActionTrail.Status = strStatus;
        //                        if (obj_ExtActionTrail != null)
        //                            ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                lblMessageCertifiedRevenueSketch.Text = "Maximum number of files to be uploaded is 5";
        //                lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
        //            }
        //        }
        //    }
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
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplicationForNoLimit = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
                    arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplicationForNoLimit.GetExistingNOCAttachmentList();

                    if (arr_industrialRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                    {
                        string str_fname;
                        string str_ext;
                        string str_newFileNameWithPath = "";

                        string str_restPath = "";

                        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_insertIndustrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment();
                        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment>();
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
                                            obj_insertIndustrialRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(txtFileUploadExistingNOC.PostedFile);
                                            obj_insertIndustrialRenewApplicationAttachment.ContentType = txtFileUploadExistingNOC.PostedFile.ContentType;
                                            obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = txtFileUploadExistingNOC.FileName;
                                            obj_insertIndustrialRenewApplicationAttachment.FileExtension = str_ext;
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

                            obj_insertIndustrialRenewApplicationAttachment.IndustrialRenewApplicationCode = obj_industrialRenewApplication.IndustrialRenewApplicationCode;
                            obj_insertIndustrialRenewApplicationAttachment.AttachmentCode = obj_industrialRenewApplication.IndustrialRenewExistingNOC.ExistNOCAttachCode;
                            obj_insertIndustrialRenewApplicationAttachment.AttachmentName = txtExistingNOC.Text;

                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                            obj_insertIndustrialRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                            if (obj_insertIndustrialRenewApplicationAttachment.Add() == 1)
                            {
                                strStatus = "File Upload Success";
                                obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;

                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessageExistingNOC.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
                                lblMessageExistingNOC.ForeColor = System.Drawing.Color.Red;

                            }

                            lblMessageExistingNOC.ForeColor = Color.Green;
                            lblMessageExistingNOC.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
                            txtExistingNOC.Text = "";
                            BindIndustrialRenewApplicationAttachmentExisingNOCDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
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
    #endregion



    #region RowDeleting
    //protected void gvWaterRequirement_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
    //    {
    //        Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
    //    }
    //    else
    //    {

    //        hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
    //        Session["CSRF"] = hidCSRF.Value;
    //        try
    //        {
    //            strActionName = "File Delete Water Requirement";
    //            long lng_IndustrialRenewApplicationCode = Convert.ToInt32(gvWaterRequirement.DataKeys[e.RowIndex].Values["IndustrialRenewApplicationCode"]);
    //            int int_AttachmentCode = Convert.ToInt32(gvWaterRequirement.DataKeys[e.RowIndex].Values["AttachmentCode"]);
    //            int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvWaterRequirement.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
    //            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_industrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment(lng_IndustrialRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
    //            //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
    //            //string str_fullPath = str_startPathFromConfig + obj_industrialRenewApplicationAttachment.AttachmentPath;

    //            if (obj_industrialRenewApplicationAttachment.Delete() == 1)
    //            {
    //                strStatus = "File Delete Success";
    //                lblMessageWaterRequirement.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
    //                lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
    //                //if (File.Exists(str_fullPath))
    //                //{
    //                //    File.Delete(str_fullPath);
    //                //}
    //                BindIndustrialRenewApplicationAttachmentWaterRequirementDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
    //            }
    //            else
    //            {
    //                strStatus = "File Delete Failed";
    //                lblMessageWaterRequirement.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
    //                lblMessageWaterRequirement.ForeColor = System.Drawing.Color.Red;
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            Response.Redirect("~/ExternalErrorPage.aspx", false);
    //        }
    //        finally
    //        {
    //            ActionTrail obj_ExtActionTrail = new ActionTrail();
    //            if (Session["ExternalUserCode"] != null)
    //            {
    //                obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
    //                obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
    //                obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
    //                obj_ExtActionTrail.Status = strStatus;
    //                if (obj_ExtActionTrail != null)
    //                    ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
    //            }
    //        }
    //    }
    //}
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
                long lng_IndustrialRenewApplicationCode = Convert.ToInt32(gvExistingNOC.DataKeys[e.RowIndex].Values["IndustrialRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvExistingNOC.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvExistingNOC.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_industrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment(lng_IndustrialRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                //string str_fullPath = str_startPathFromConfig + obj_industrialRenewApplicationAttachment.AttachmentPath;

                if (obj_industrialRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Deleted Success";
                    lblMessageExistingNOC.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
                    lblMessageExistingNOC.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    BindIndustrialRenewApplicationAttachmentExisingNOCDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Deleting Failed";
                    lblMessageExistingNOC.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
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
                long lng_IndustrialRenewApplicationCode = Convert.ToInt32(gvGroundwaterAvailability.DataKeys[e.RowIndex].Values["IndustrialRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvGroundwaterAvailability.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvGroundwaterAvailability.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_industrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment(lng_IndustrialRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);

                //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                //string str_fullPath = str_startPathFromConfig + obj_industrialRenewApplicationAttachment.AttachmentPath;

                if (obj_industrialRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Success";
                    lblMessageGroundwaterAvailability.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
                    lblMessageGroundwaterAvailability.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    BindIndustrialRenewApplicationAttachmentGroundwaterAvailabilityDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed";
                    lblMessageGroundwaterAvailability.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
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
                long lng_IndustrialRenewApplicationCode = Convert.ToInt32(gvRainwaterHarvesting.DataKeys[e.RowIndex].Values["IndustrialRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvRainwaterHarvesting.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvRainwaterHarvesting.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_industrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment(lng_IndustrialRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);

                //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                //string str_fullPath = str_startPathFromConfig + obj_industrialRenewApplicationAttachment.AttachmentPath;

                if (obj_industrialRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Success";
                    lblMessageRainwaterHarvesting.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
                    lblMessageRainwaterHarvesting.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    BindIndustrialRenewApplicationAttachmentRainwaterHarvestingDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed";
                    lblMessageRainwaterHarvesting.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
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
                long lng_IndustrialRenewApplicationCode = Convert.ToInt32(gvUndertaking.DataKeys[e.RowIndex].Values["IndustrialRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvUndertaking.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvUndertaking.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_industrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment(lng_IndustrialRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);

                //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                //string str_fullPath = str_startPathFromConfig + obj_industrialRenewApplicationAttachment.AttachmentPath;

                if (obj_industrialRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Success";
                    lblMessageUndertaking.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
                    lblMessageUndertaking.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}
                    BindIndustrialRenewApplicationAttachmentUndertakingDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed";
                    lblMessageUndertaking.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
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
    //protected void gvReasonForNotApplyingBefore_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
    //    {
    //        Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
    //    }
    //    else
    //    {
    //        hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
    //        Session["CSRF"] = hidCSRF.Value;
    //        try
    //        {
    //            strActionName = "File Delete Reason For Not Applying Renewal Before";
    //            long lng_IndustrialRenewApplicationCode = Convert.ToInt32(gvReasonForNotApplyingBefore.DataKeys[e.RowIndex].Values["IndustrialRenewApplicationCode"]);
    //            int int_AttachmentCode = Convert.ToInt32(gvReasonForNotApplyingBefore.DataKeys[e.RowIndex].Values["AttachmentCode"]);
    //            int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvReasonForNotApplyingBefore.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
    //            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_industrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment(lng_IndustrialRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
    //            //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
    //            //string str_fullPath = str_startPathFromConfig + obj_industrialRenewApplicationAttachment.AttachmentPath;

    //            if (obj_industrialRenewApplicationAttachment.Delete() == 1)
    //            {
    //                strStatus = "File Deleted Success";
    //                lblMessageReasonForNotApplyingBefore.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
    //                lblMessageReasonForNotApplyingBefore.ForeColor = System.Drawing.Color.Red;
    //                //if (File.Exists(str_fullPath))
    //                //{
    //                //    File.Delete(str_fullPath);
    //                //}
    //                BindIndustrialRenewApplicationAttachmentReasonForNotApplyingBeforeDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
    //            }
    //            else
    //            {
    //                strStatus = "File Deleting Failed";
    //                lblMessageReasonForNotApplyingBefore.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
    //                lblMessageReasonForNotApplyingBefore.ForeColor = System.Drawing.Color.Red;
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            Response.Redirect("~/ExternalErrorPage.aspx", false);
    //        }
    //        ActionTrail obj_ExtActionTrail = new ActionTrail();
    //        if (Session["ExternalUserCode"] != null)
    //        {
    //            obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
    //            obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
    //            obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
    //            obj_ExtActionTrail.Status = strStatus;
    //            if (obj_ExtActionTrail != null)
    //                ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
    //        }
    //    }
    //}

    //protected void gvCertifiedRevenueSketch_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
    //    {
    //        Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
    //    }
    //    else
    //    {
    //        hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
    //        Session["CSRF"] = hidCSRF.Value;
    //        try
    //        {
    //            strActionName = "File Delete Certificate of Revenue Sketch";
    //            long lng_IndustrialRenewApplicationCode = Convert.ToInt32(gvCertifiedRevenueSketch.DataKeys[e.RowIndex].Values["IndustrialRenewApplicationCode"]);
    //            int int_AttachmentCode = Convert.ToInt32(gvCertifiedRevenueSketch.DataKeys[e.RowIndex].Values["AttachmentCode"]);
    //            int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvCertifiedRevenueSketch.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
    //            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_industrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment(lng_IndustrialRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
    //            //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
    //            //string str_fullPath = str_startPathFromConfig + obj_industrialRenewApplicationAttachment.AttachmentPath;

    //            if (obj_industrialRenewApplicationAttachment.Delete() == 1)
    //            {
    //                strStatus = "File Deleted Success";
    //                lblMessageCertifiedRevenueSketch.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
    //                lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
    //                //if (File.Exists(str_fullPath))
    //                //{
    //                //    File.Delete(str_fullPath);
    //                //}
    //                BindIndustrialRenewApplicationAttachmentCertifiedRevenueSketchDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
    //            }
    //            else
    //            {
    //                strStatus = "File Deleting Failed";
    //                lblMessageCertifiedRevenueSketch.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
    //                lblMessageCertifiedRevenueSketch.ForeColor = System.Drawing.Color.Red;
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            Response.Redirect("~/ExternalErrorPage.aspx", false);
    //        }
    //        finally
    //        {
    //            ActionTrail obj_ExtActionTrail = new ActionTrail();
    //            if (Session["ExternalUserCode"] != null)
    //            {
    //                obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
    //                obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
    //                obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
    //                obj_ExtActionTrail.Status = strStatus;
    //                if (obj_ExtActionTrail != null)
    //                    ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
    //            }
    //        }
    //    }
    //}
    //protected void gvSitePlan_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
    //    {
    //        Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
    //    }
    //    else
    //    {
    //        hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
    //        Session["CSRF"] = hidCSRF.Value;
    //        try
    //        {
    //            strActionName = "Delete File Site Plan";
    //            long lng_IndustrialRenewApplicationCode = Convert.ToInt32(gvSitePlan.DataKeys[e.RowIndex].Values["IndustrialRenewApplicationCode"]);
    //            int int_AttachmentCode = Convert.ToInt32(gvSitePlan.DataKeys[e.RowIndex].Values["AttachmentCode"]);
    //            int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvSitePlan.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
    //            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_industrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment(lng_IndustrialRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);

    //            //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
    //            //string str_fullPath = str_startPathFromConfig + obj_industrialRenewApplicationAttachment.AttachmentPath;


    //            if (obj_industrialRenewApplicationAttachment.Delete() == 1)
    //            {
    //                strStatus = "File Deleted Success";
    //                lblMessageSitePlan.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
    //                lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
    //                //if (File.Exists(str_fullPath))
    //                //{
    //                //    File.Delete(str_fullPath);
    //                //}
    //                BindIndustrialRenewApplicationAttachmentSitePlanDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
    //            }
    //            else
    //            {
    //                strStatus = "File Deleted Failed";
    //                lblMessageSitePlan.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
    //                lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            Response.Redirect("~/ExternalErrorPage.aspx", false);
    //        }
    //        finally
    //        {
    //            ActionTrail obj_ExtActionTrail = new ActionTrail();
    //            if (Session["ExternalUserCode"] != null)
    //            {
    //                obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
    //                obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
    //                obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
    //                obj_ExtActionTrail.Status = strStatus;
    //                if (obj_ExtActionTrail != null)
    //                    ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
    //            }
    //        }
    //    }
    //}
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
                long lng_IndustrialRenewApplicationCode = Convert.ToInt32(gvExtra.DataKeys[e.RowIndex].Values["IndustrialRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvExtra.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvExtra.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_industrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment(lng_IndustrialRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);


                //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                //string str_fullPath = str_startPathFromConfig + obj_industrialRenewApplicationAttachment.AttachmentPath;


                if (obj_industrialRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Success";
                    lblMessageExtra.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
                    lblMessageExtra.ForeColor = System.Drawing.Color.Red;
                    //if (File.Exists(str_fullPath))
                    //{
                    //    File.Delete(str_fullPath);
                    //}

                    BindIndustrialRenewApplicationAttachmentExtraDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

                }
                else
                {
                    strStatus = "File Delete Failed";
                    lblMessageExtra.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
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
                long lng_IndustrialRenewApplicationCode = Convert.ToInt32(gvBharatKoshReciept.DataKeys[e.RowIndex].Values["IndustrialRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvBharatKoshReciept.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvBharatKoshReciept.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_industrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment(lng_IndustrialRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                if (obj_industrialRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Success";
                    lblMessageBharatKoshReciept.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
                    lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
                    BindIndustrialRenewApplicationAttachmentBharatKoshRecieptDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed";
                    lblMessageBharatKoshReciept.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
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
                    BindAffidavitAttachmentDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
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
                    BindWaterAuditReportAttachmentDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
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
                    BindSourceWaterNonAvailabilityCertificateAttachmentDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
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
                strActionName = "File Delete Bharat Kosh Reciept";
                long lng_IndustrialRenewApplicationCode = Convert.ToInt32(gvApplicationSignatureSeal.DataKeys[e.RowIndex].Values["IndustrialRenewApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvApplicationSignatureSeal.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvApplicationSignatureSeal.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                IndustrialRenewSADApplicationAttachment obj_industrialRenewApplicationAttachment = new IndustrialRenewSADApplicationAttachment(lng_IndustrialRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
                if (obj_industrialRenewApplicationAttachment.Delete() == 1)
                {
                    strStatus = "File Delete Success";
                    lblMessageApplicationSignatureSeal.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
                    lblMessageApplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;
                    BindIndustrialRenewApplicationAttachmentSignedDocDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                }
                else
                {
                    strStatus = "File Delete Failed";
                    lblMessageApplicationSignatureSeal.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
                    lblMessageApplicationSignatureSeal.ForeColor = System.Drawing.Color.Red;

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

    #region ViewFile
    protected void lnkIndustrialCompCondNOCAttachmentView_Click(object sender, CommandEventArgs e)
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
                    long lng_industrialRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.INDRenewSADAppDownloadFiles(lng_industrialRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                    long lng_industrialRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.INDRenewSADAppDownloadFiles(lng_industrialRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                    long lng_industrialRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.INDRenewSADAppDownloadFiles(lng_industrialRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                    long lng_industrialRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.INDRenewSADAppDownloadFiles(lng_industrialRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                    long lng_industrialRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.INDRenewSADAppDownloadFiles(lng_industrialRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                    long lng_industrialRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.INDRenewSADAppDownloadFiles(lng_industrialRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                    long lng_industrialRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.INDRenewSADAppDownloadFiles(lng_industrialRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                    long lng_industrialRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.INDRenewSADAppDownloadFiles(lng_industrialRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

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
                    long lng_industrialRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.INDRenewSADAppDownloadFiles(lng_industrialRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

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

    protected void lnkIndustrialCompCondNOCAttachmentViewOther_Click(object sender, CommandEventArgs e)
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
                    long lng_industrialRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.INDRenewSADAppDownloadFiles(lng_industrialRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                    long lng_industrialRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);

                    NOCAPExternalUtility.INDRenewSADAppDownloadFiles(lng_industrialRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
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
                    long lng_industrialRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);


                    NOCAPExternalUtility.INDRenewSADAppDownloadFiles(lng_industrialRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

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
                    long lng_industrialNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.INDRenewSADAppDownloadFiles(lng_industrialNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

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

    #region RowDataBound
    protected void gvINDRenewComplianceConditionNOC_RowDataBound(object sender, GridViewRowEventArgs e)
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

                    BindIndustrialRenewComplianceConditionNOCAttachmentDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
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
                Label lblMsgINDCompCondNOCAttachmentOtherDelete = null;
                if (e.CommandName == "DeleteFile")
                {
                    strActionName = "Delete File INDCompCondNOCAttachmentOther";


                    GridViewRow gvCompCondNOCAttachmentOtherRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    GridView gvCompCondNOCAttachmentOther = (GridView)(gvCompCondNOCAttachmentOtherRow.Parent.Parent);
                    GridViewRow gvINDRenewComplianceConditionNOCOtherRow = (GridViewRow)(gvCompCondNOCAttachmentOther.NamingContainer);

                    int b = gvINDRenewComplianceConditionNOCOtherRow.RowIndex;

                    foreach (GridViewRow row in gvINDRenewComplianceConditionNOCOther.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            Label lblMsgINDCompCondNOCAttachmentOtherDelete1 = (Label)row.FindControl("lblMessageINDCompCondNOCAttachmentOtherDelete");
                            lblMsgINDCompCondNOCAttachmentOtherDelete1.Text = "";
                            Label lblMsgINDCompCondAttachmentOther = (Label)row.FindControl("lblMessageINDCompCondAttachmentOther");
                            lblMsgINDCompCondAttachmentOther.Text = "";

                            TextBox txtAttachmentNameOther = (TextBox)row.FindControl("txtAttachmentNameCompCondNOCOther");
                            txtAttachmentNameOther.Text = "";


                            if (row.RowIndex == b)
                            {
                                lblMsgINDCompCondNOCAttachmentOtherDelete = (Label)gvINDRenewComplianceConditionNOCOther.Rows[b].FindControl("lblMessageINDCompCondNOCAttachmentOtherDelete");
                            }
                        }
                    }

                    string[] CommandArgument = e.CommandArgument.ToString().Split(',');
                    long lng_IndustrialRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_AttachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_AttachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);


                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_industrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment(lng_IndustrialRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);

                    //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                    //string str_fullPath = str_startPathFromConfig + obj_industrialRenewApplicationAttachment.AttachmentPath;


                    if (obj_industrialRenewApplicationAttachment.Delete() == 1)
                    {
                        strStatus = "File Deleted Success";

                        lblMsgINDCompCondNOCAttachmentOtherDelete.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
                        lblMsgINDCompCondNOCAttachmentOtherDelete.ForeColor = System.Drawing.Color.Red;

                        //if (File.Exists(str_fullPath))
                        //{
                        //    File.Delete(str_fullPath);
                        //}
                        BindIndustrialRenewComplianceConditionNOCOtherAttachmentDetails();
                    }
                    else
                    {
                        strStatus = "File Deleted Failed";
                        lblMsgINDCompCondNOCAttachmentOtherDelete.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
                        lblMsgINDCompCondNOCAttachmentOtherDelete.ForeColor = System.Drawing.Color.Red;
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
    protected void gvINDRenewComplianceConditionNOCOther_RowCommand(object sender, GridViewCommandEventArgs e)
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
                Label lblMsgINDCompCondAttachmentOther = null;
                if (e.CommandName == "UploadFileForCompCondNOCOther")
                {
                    strActionName = "Upload Compliance Condition NOC - Other Attachment";

                    GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                    FileUpload FileUploadCompCondNOCAttOther = (FileUpload)row.FindControl("FileUploadCompCondNOCOther");
                    TextBox txtFileCompCondNOCAttachmentOther = (TextBox)row.FindControl("txtAttachmentNameCompCondNOCOther");
                    GridView gvCompCondNOCAttOther = (GridView)row.FindControl("gvCompCondNOCAttachmentOther");
                    Label lblCompCondNOCOtherSerialNumber = (Label)row.FindControl("lblCompCondNOCOtherSerialNumber");

                    int b = row.RowIndex;

                    foreach (GridViewRow row1 in gvINDRenewComplianceConditionNOCOther.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            Label lblMsgINDCompCondNOCAttachmentOtherDelete1 = (Label)row1.FindControl("lblMessageINDCompCondNOCAttachmentOtherDelete");
                            lblMsgINDCompCondNOCAttachmentOtherDelete1.Text = "";

                            Label lblMessageINDCompCondAttachmentOther1 = (Label)row1.FindControl("lblMessageINDCompCondAttachmentOther");
                            lblMessageINDCompCondAttachmentOther1.Text = "";

                            if (row1.RowIndex == b)
                            {
                                lblMsgINDCompCondAttachmentOther = (Label)gvINDRenewComplianceConditionNOCOther.Rows[b].FindControl("lblMessageINDCompCondAttachmentOther");
                            }
                            else
                            {
                                TextBox txtAttachmentName = (TextBox)row1.FindControl("txtAttachmentNameCompCondNOCOther");
                                txtAttachmentName.Text = "";
                            }
                        }
                    }

                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplicationForNoLimit = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOCOther obj_IndustrialRenewSADComplianceConditionNOCOther = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOCOther(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text), Convert.ToInt32(lblCompCondNOCOtherSerialNumber.Text));
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;

                    arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplicationForNoLimit.GetIndustrialComplianceConditionNOCOtherAttachmentList(obj_IndustrialRenewSADComplianceConditionNOCOther.ComplianceConditionAttachmentCode);


                    if (arr_industrialRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                    {
                        try
                        {
                            string str_fname;
                            string str_ext;

                            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_insertIndustrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment();
                            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                            List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment>();

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
                                            obj_insertIndustrialRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadCompCondNOCAttOther.PostedFile);
                                            obj_insertIndustrialRenewApplicationAttachment.ContentType = FileUploadCompCondNOCAttOther.PostedFile.ContentType;
                                            obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = FileUploadCompCondNOCAttOther.FileName;
                                            obj_insertIndustrialRenewApplicationAttachment.FileExtension = str_ext;
                                        }
                                        else
                                        {
                                            strStatus = "File Upload Failed";
                                            lblMsgINDCompCondAttachmentOther.Text = "File can not upload. It has more than 5 MB size";
                                            lblMsgINDCompCondAttachmentOther.ForeColor = System.Drawing.Color.Red;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMsgINDCompCondAttachmentOther.Text = "Not a valid file!!..Select another file!!";
                                        lblMsgINDCompCondAttachmentOther.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMsgINDCompCondAttachmentOther.Text = "Not a valid file!!..Select another file!!";
                                    lblMsgINDCompCondAttachmentOther.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                lblMsgINDCompCondAttachmentOther.Text = "Please select a file..!!";
                                lblMsgINDCompCondAttachmentOther.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                            obj_insertIndustrialRenewApplicationAttachment.IndustrialRenewApplicationCode = obj_industrialRenewApplication.IndustrialRenewApplicationCode;
                            obj_insertIndustrialRenewApplicationAttachment.AttachmentCode = obj_IndustrialRenewSADComplianceConditionNOCOther.ComplianceConditionAttachmentCode;
                            obj_insertIndustrialRenewApplicationAttachment.AttachmentName = txtFileCompCondNOCAttachmentOther.Text;

                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                            obj_insertIndustrialRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                            if (obj_insertIndustrialRenewApplicationAttachment.Add() == 1)
                            {
                                strStatus = "File Upload Success";
                                lblMsgINDCompCondAttachmentOther.Text = "File Upload Success";
                                lblMsgINDCompCondAttachmentOther.ForeColor = System.Drawing.Color.Green;

                                BindIndustrialRenewComplianceConditionNOCOtherAttachmentDetails();
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMsgINDCompCondAttachmentOther.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
                                lblMsgINDCompCondAttachmentOther.ForeColor = System.Drawing.Color.Red;

                            }

                            //lblMsgINDCompCondAttachment.ForeColor = Color.Green;
                            //lblMsgINDCompCondAttachment.Text = "";
                            txtFileCompCondNOCAttachmentOther.Text = "";
                            BindIndustrialRenewComplianceConditionNOCOtherAttachmentDetails();
                            //lblMessageCertifiedRevenueSketch.Text = "";
                            //lblMessageReasonForNotApplyingBefore.Text = "";
                            lblMessageGroundwaterAvailability.Text = "";
                            lblMessageRainwaterHarvesting.Text = "";
                            lblMessageExistingNOC.Text = "";
                            lblMessageUndertaking.Text = "";
                            //lblMessageWaterRequirement.Text = "";

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
                        lblMsgINDCompCondAttachmentOther.Text = "Maximum number of files to be uploaded is 5";
                        lblMsgINDCompCondAttachmentOther.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    protected void gvINDRenewComplianceConditionNOC_RowCommand(object sender, GridViewCommandEventArgs e)
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
                Label lblMsgINDCompCondAttachment = null;
                if (e.CommandName == "UploadFileForCompCondNOC")
                {
                    strActionName = "Upload Compliance Condition NOC Attachment";

                    GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                    FileUpload FileUploadCompCondNOCAtt = (FileUpload)row.FindControl("FileUploadCompCondNOC");
                    TextBox txtFileCompCondNOCAttachment = (TextBox)row.FindControl("txtAttachmentNameCompCondNOC");
                    GridView gvCompCondNOCAtt = (GridView)row.FindControl("gvCompCondNOCAttachment");
                    Label lblCompCondNOCCode = (Label)row.FindControl("lblCompCondNOCCode");
                    //  Label lblMsgINDCompCondAttachment = (Label)row.FindControl("lblMessageINDCompCondAttachment");
                    int b = row.RowIndex;

                    foreach (GridViewRow row1 in gvINDRenewComplianceConditionNOC.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            Label lblMsgINDCompCondNOCAttachmentDelete = (Label)row1.FindControl("lblMessageINDCompCondNOCAttachmentDelete");
                            lblMsgINDCompCondNOCAttachmentDelete.Text = "";
                            Label lblMsgINDCompCondNOCAttachment1 = (Label)row1.FindControl("lblMessageINDCompCondAttachment");
                            lblMsgINDCompCondNOCAttachment1.Text = "";

                            if (row1.RowIndex == b)
                            {
                                lblMsgINDCompCondAttachment = (Label)gvINDRenewComplianceConditionNOC.Rows[b].FindControl("lblMessageINDCompCondAttachment");
                            }
                            else
                            {
                                TextBox txtAttachmentName = (TextBox)row1.FindControl("txtAttachmentNameCompCondNOC");
                                txtAttachmentName.Text = "";
                            }
                        }
                    }


                    // lblMsgINDCompCondAttachment.Text = "";

                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplicationForNoLimit = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOC obj_IndustrialRenewSADComplianceConditionNOC = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOC(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text), Convert.ToInt32(lblCompCondNOCCode.Text));
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;

                    arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplicationForNoLimit.GetIndustrialComplianceConditionNOCAttachmentList(obj_IndustrialRenewSADComplianceConditionNOC.ComplianceConditionAttachmentCode);

                    if (arr_industrialRenewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                    {
                        try
                        {
                            string str_fname;
                            string str_ext;

                            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_insertIndustrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment();
                            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                            List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment>();

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
                                            obj_insertIndustrialRenewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadCompCondNOCAtt.PostedFile);
                                            obj_insertIndustrialRenewApplicationAttachment.ContentType = FileUploadCompCondNOCAtt.PostedFile.ContentType;
                                            obj_insertIndustrialRenewApplicationAttachment.AttachmentPath = FileUploadCompCondNOCAtt.FileName;
                                            obj_insertIndustrialRenewApplicationAttachment.FileExtension = str_ext;
                                        }
                                        else
                                        {
                                            strStatus = "File Upload Failed";
                                            lblMsgINDCompCondAttachment.Text = "File can not upload. It has more than 5 MB size";
                                            lblMsgINDCompCondAttachment.ForeColor = System.Drawing.Color.Red;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblMsgINDCompCondAttachment.Text = "Not a valid file!!..Select another file!!";
                                        lblMsgINDCompCondAttachment.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMsgINDCompCondAttachment.Text = "Not a valid file!!..Select another file!!";
                                    lblMsgINDCompCondAttachment.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                lblMsgINDCompCondAttachment.Text = "Please select a file..!!";
                                lblMsgINDCompCondAttachment.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                            obj_insertIndustrialRenewApplicationAttachment.IndustrialRenewApplicationCode = obj_industrialRenewApplication.IndustrialRenewApplicationCode;
                            obj_insertIndustrialRenewApplicationAttachment.AttachmentCode = obj_IndustrialRenewSADComplianceConditionNOC.ComplianceConditionAttachmentCode;
                            obj_insertIndustrialRenewApplicationAttachment.AttachmentName = txtFileCompCondNOCAttachment.Text;

                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                            obj_insertIndustrialRenewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;

                            if (obj_insertIndustrialRenewApplicationAttachment.Add() == 1)
                            {
                                strStatus = "File Upload Success";
                                lblMsgINDCompCondAttachment.Text = "File Upload Success";
                                lblMsgINDCompCondAttachment.ForeColor = System.Drawing.Color.Green;

                                BindIndustrialRenewComplianceConditionNOCAttachmentDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMsgINDCompCondAttachment.Text = obj_insertIndustrialRenewApplicationAttachment.CustumMessage;
                                lblMsgINDCompCondAttachment.ForeColor = System.Drawing.Color.Red;

                            }

                            //lblMsgINDCompCondAttachment.ForeColor = Color.Green;
                            //lblMsgINDCompCondAttachment.Text = "";
                            txtFileCompCondNOCAttachment.Text = "";
                            BindIndustrialRenewComplianceConditionNOCAttachmentDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
                            //lblMessageCertifiedRevenueSketch.Text = "";
                            //lblMessageReasonForNotApplyingBefore.Text = "";
                            lblMessageGroundwaterAvailability.Text = "";
                            lblMessageRainwaterHarvesting.Text = "";
                            lblMessageExistingNOC.Text = "";
                            lblMessageUndertaking.Text = "";
                            //lblMessageWaterRequirement.Text = "";
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
                        lblMsgINDCompCondAttachment.Text = "Maximum number of files to be uploaded is 5";
                        lblMsgINDCompCondAttachment.ForeColor = System.Drawing.Color.Red;
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
                Label lblMsgINDCompCondNOCAttachmentDelete = null;
                if (e.CommandName == "DeleteFile")
                {
                    strActionName = "Delete File INDCompCondNOCAttachment";


                    GridViewRow gvCompCondNOCAttachmentRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    GridView gvCompCondNOCAttachment = (GridView)(gvCompCondNOCAttachmentRow.Parent.Parent);
                    GridViewRow gvINDRenewComplianceConditionNOCRow = (GridViewRow)(gvCompCondNOCAttachment.NamingContainer);

                    int b = gvINDRenewComplianceConditionNOCRow.RowIndex;

                    foreach (GridViewRow row in gvINDRenewComplianceConditionNOC.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            Label lblMsgINDCompCondNOCAttachmentDelete1 = (Label)row.FindControl("lblMessageINDCompCondNOCAttachmentDelete");
                            lblMsgINDCompCondNOCAttachmentDelete1.Text = "";
                            Label lblMsgINDCompCondAttachment = (Label)row.FindControl("lblMessageINDCompCondAttachment");
                            lblMsgINDCompCondAttachment.Text = "";

                            TextBox txtAttachmentName = (TextBox)row.FindControl("txtAttachmentNameCompCondNOC");
                            txtAttachmentName.Text = "";

                            if (row.RowIndex == b)
                            {
                                lblMsgINDCompCondNOCAttachmentDelete = (Label)gvINDRenewComplianceConditionNOC.Rows[b].FindControl("lblMessageINDCompCondNOCAttachmentDelete");
                            }
                        }
                    }

                    string[] CommandArgument = e.CommandArgument.ToString().Split(',');
                    long lng_IndustrialRenewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_AttachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_AttachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);


                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment obj_industrialRenewApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment(lng_IndustrialRenewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);

                    //string str_startPathFromConfig = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
                    //string str_fullPath = str_startPathFromConfig + obj_industrialRenewApplicationAttachment.AttachmentPath;


                    if (obj_industrialRenewApplicationAttachment.Delete() == 1)
                    {
                        strStatus = "File Deleted Success";

                        lblMsgINDCompCondNOCAttachmentDelete.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
                        lblMsgINDCompCondNOCAttachmentDelete.ForeColor = System.Drawing.Color.Red;

                        //if (File.Exists(str_fullPath))
                        //{
                        //    File.Delete(str_fullPath);
                        //}
                        BindIndustrialRenewComplianceConditionNOCAttachmentDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    }
                    else
                    {
                        strStatus = "File Deleted Failed";
                        lblMsgINDCompCondNOCAttachmentDelete.Text = obj_industrialRenewApplicationAttachment.CustumMessage;
                        lblMsgINDCompCondNOCAttachmentDelete.ForeColor = System.Drawing.Color.Red;
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

    //private int NOCAPExternalUtility.ExternalAttachmentSizeLimit()
    //{
    //    try
    //    {
    //        NOCAP.BLL.Common.AttachmentLimit obj_attachmentLimit = new NOCAP.BLL.Common.AttachmentLimit();

    //        int AttachmentSize = 1048576 * (obj_attachmentLimit.SizeOfEachAttachment);
    //        return AttachmentSize;
    //    }
    //    catch
    //    {
    //        Response.Redirect("~/ExternalErrorPage.aspx", false);
    //        return 0;
    //    }
    //}
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






    private void BindIndustrialRenewApplicationAttachmentExisingNOCDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment>();
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
            arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetExistingNOCAttachmentList();
            lblExistingNOC2.Visible = true;
            lblExistingNOC.Text = "Existing NOC (" + arr_industrialRenewApplicationAttachmentList.Length.ToString() + ")";
            gvExistingNOC.DataSource = arr_industrialRenewApplicationAttachmentList;
            gvExistingNOC.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void BindIndustrialRenewApplicationAttachmentGroundwaterAvailabilityDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment>();
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
            arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetGroundwaterAvailabilityAttachmentList();
            lblGroundwaterAvailabilityReport2.Visible = false;
            lblGroundwaterAvailabilityReport.Text = "Groundwater Availability (" + arr_industrialRenewApplicationAttachmentList.Length.ToString() + ")";
            gvGroundwaterAvailability.DataSource = arr_industrialRenewApplicationAttachmentList;
            gvGroundwaterAvailability.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindIndustrialRenewApplicationAttachmentRainwaterHarvestingDetails(long lngA_ApplicationCode)
    {
        try
        {


            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment>();
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
            arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetRainwaterHarvestingAttachmentList();
            lblRainWaterHarvesting2.Visible = false;
            lblRainWaterHarvesting.Text = "Rainwater Harvesting (" + arr_industrialRenewApplicationAttachmentList.Length.ToString() + ")";
            gvRainwaterHarvesting.DataSource = arr_industrialRenewApplicationAttachmentList;
            gvRainwaterHarvesting.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindIndustrialRenewApplicationAttachmentUndertakingDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment>();
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
            arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetUndertakingAttachmentList();
            lblAuthorization2.Visible = false;
            lblAuthorization.Text = "Authorization (" + arr_industrialRenewApplicationAttachmentList.Length.ToString() + ")";
            gvUndertaking.DataSource = arr_industrialRenewApplicationAttachmentList;
            gvUndertaking.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindIndustrialRenewApplicationAttachmentExtraDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment> lst_industrialRenewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment>();
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;

            arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetExtraAttachmentList();
            lblExtraAttachment2.Visible = false;
            lblExtraAttachment.Text = "Extra Attachment (" + arr_industrialRenewApplicationAttachmentList.Length.ToString() + ")";
            gvExtra.DataSource = arr_industrialRenewApplicationAttachmentList;
            gvExtra.DataBind();
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindIndustrialRenewComplianceConditionNOCAttachmentDetails(long lng_ApplicationCode)
    {
        try
        {
            foreach (GridViewRow row in gvINDRenewComplianceConditionNOC.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    GridView gvCompCondNOCAtt = (GridView)row.FindControl("gvCompCondNOCAttachment");

                    Label lblCompCondNOCCode = (Label)row.FindControl("lblCompCondNOCCode");

                    //// Inner Grid View Binding

                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOC obj_IndustrialRenewSADComplianceConditionNOC = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOC(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text), Convert.ToInt32(lblCompCondNOCCode.Text));

                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentListForCount;

                    arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetIndustrialComplianceConditionNOCAttachmentList(obj_IndustrialRenewSADComplianceConditionNOC.ComplianceConditionAttachmentCode, NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment.SortingFieldForAttachment.NoSorting);

                    arr_industrialRenewApplicationAttachmentListForCount = obj_industrialRenewApplication.GetIndustrialComplianceConditionNOCAttachmentList(NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment.SortingFieldForAttachment.NoSorting);
                    lblComplianceConditionNOC2.Visible = false;
                    lblComplianceConditionNOC.Text = "Compliance Report (" + arr_industrialRenewApplicationAttachmentListForCount.Length.ToString() + ")";
                    gvCompCondNOCAtt.DataSource = arr_industrialRenewApplicationAttachmentList;
                    gvCompCondNOCAtt.DataBind();
                }
            }
        }
        catch (Exception)
        { }
    }

    private void BindIndustrialRenewApplicationComplianceConditionNOC(long lngA_ApplicationCode)
    {
        try
        {
            int intStatus = 0;
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOCExt obj_IndustrialRenewSADComplianceConditionNOCExt = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOCExt();

            intStatus = obj_IndustrialRenewSADComplianceConditionNOCExt.GetComplianceConditionListForApplicationCodeExt(lngA_ApplicationCode, NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOCExt.SortingField.ComplianceConditionNOCDescription);
            if (intStatus == 1)
            {

                gvINDRenewComplianceConditionNOC.DataSource = obj_IndustrialRenewSADComplianceConditionNOCExt.IndustrialRenewSADComplianceConditionNOCCollectionExt;
                gvINDRenewComplianceConditionNOC.DataBind();

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void clearMessage()
    {
        lblMessageAffidavit.Text = "";
        txtAffidavit.Text = "";

        lblMessageWaterAuditReport.Text = "";
        txtWaterAuditReport.Text = "";

        lblMessageSourceofAvailabilityofSurfaceWater.Text = "";
        txtSourceofAvailabilityofSurfaceWater.Text = "";
        //lblMessageSitePlan.Text = "";
        //lblMessageCertifiedRevenueSketch.Text = "";
        //lblMessageReasonForNotApplyingBefore.Text = "";
        lblMessageExistingNOC.Text = "";
        //lblMessageWaterRequirement.Text = "";
        lblMessageGroundwaterAvailability.Text = "";
        lblMessageRainwaterHarvesting.Text = "";
        lblMessageUndertaking.Text = "";
        //lblMessageReferralLetter.Text = "";
        lblMessageExtra.Text = "";

    }
    //private int AttachmentsCountReferalLetter()
    //{
    //    int Count = 0;
    //    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
    //    string SelectedValue = ddlReferralLetter.SelectedValue;
    //    for (int i = 1; i < ddlReferralLetter.Items.Count; i++)
    //    {
    //        ddlReferralLetter.SelectedIndex = i;
    //        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADReferralLetter obj_industrialNewReferralLetter = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADReferralLetter(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text), Convert.ToInt32(ddlReferralLetter.SelectedValue));
    //        Count = Count + obj_industrialNewApplication.GetCpoyOfReferralLetterAttachmentList(obj_industrialNewReferralLetter.ReferralLetterAttachCode, NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment.SortingFieldForAttachment.NoSorting).Length;
    //    }
    //    ddlReferralLetter.SelectedValue = SelectedValue;
    //    return Count;
    //}

    //private void BindIndustrialRenewApplicationComplianceConditionNOCOther()
    //{
    //    try
    //    {
    //        IndustrialRenewSADComplianceConditionNOCOther obj_industrialRenewSADComplianceConditionNOCOther = new IndustrialRenewSADComplianceConditionNOCOther();
    //        int int_status = 0;
    //        if (lblIndustialApplicationCodeFrom.Text != "" && NOCAPExternalUtility.IsNumeric(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text)))
    //        {
    //            obj_industrialRenewSADComplianceConditionNOCOther.IndustrialRenewApplicationCode = Convert.ToInt64(lblIndustialApplicationCodeFrom.Text);

    //            int_status = obj_industrialRenewSADComplianceConditionNOCOther.GetList(IndustrialRenewSADComplianceConditionNOCOther.SortingField.NoSorting);

    //            IndustrialRenewSADComplianceConditionNOCOther[] arr_IndustrialRenewSADComplianceConditionNOCOther;
    //            arr_IndustrialRenewSADComplianceConditionNOCOther = obj_industrialRenewSADComplianceConditionNOCOther.IndustrialRenewSADComplianceConditionNOCOtherCollection;

    //            if ((int_status == 1))
    //            {

    //                gvINDRenewComplianceConditionNOCOther.DataSource = arr_IndustrialRenewSADComplianceConditionNOCOther;
    //                gvINDRenewComplianceConditionNOCOther.DataBind();
    //            }
    //            else
    //            {
    //                lblMessage.Text = HttpUtility.HtmlEncode(obj_industrialRenewSADComplianceConditionNOCOther.CustumMessage);
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

    private void BindIndustrialRenewComplianceConditionNOCOtherAttachmentDetails()
    {
        try
        {
            foreach (GridViewRow row in gvINDRenewComplianceConditionNOCOther.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    GridView gvCompCondNOCAttOther = (GridView)row.FindControl("gvCompCondNOCAttachmentOther");

                    Label lblCompCondNOCOtherSerialNumber = (Label)row.FindControl("lblCompCondNOCOtherSerialNumber");

                    //// Inner Grid View Binding

                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOCOther obj_IndustrialRenewSADComplianceConditionNOCOther = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOCOther(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text), Convert.ToInt32(lblCompCondNOCOtherSerialNumber.Text));

                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentListForCount;

                    arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetIndustrialComplianceConditionNOCOtherAttachmentList(obj_IndustrialRenewSADComplianceConditionNOCOther.ComplianceConditionAttachmentCode, NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment.SortingFieldForAttachment.NoSorting);

                    arr_industrialRenewApplicationAttachmentListForCount = obj_industrialRenewApplication.GetIndustrialComplianceConditionNOCOtherAttachmentList(NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment.SortingFieldForAttachment.NoSorting);
                    lblComplianceConditionNOCOther2.Visible = false;
                    lblComplianceConditionNOCOther.Text = "Compliance Report - Other (" + arr_industrialRenewApplicationAttachmentListForCount.Length.ToString() + ")";
                    gvCompCondNOCAttOther.DataSource = arr_industrialRenewApplicationAttachmentList;
                    gvCompCondNOCAttOther.DataBind();
                }
            }
        }
        catch (Exception)
        { }
    }
    private void BindIndustrialRenewApplicationAttachmentBharatKoshRecieptDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);

            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
            arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetBharatKoshRecieptAttachmentList();
            gvBharatKoshReciept.DataSource = arr_industrialRenewApplicationAttachmentList;
            gvBharatKoshReciept.DataBind();
            //lblBharatKosh2.Visible = true;
            lblBharatKosh.Text = HttpUtility.HtmlEncode("Bharat Kosh Reciept Attachment (" + arr_industrialRenewApplicationAttachmentList.Length.ToString() + ")");
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
            //NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);

            //NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
            //arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetBharatKoshRecieptAttachmentList();
            //gvBharatKoshReciept.DataSource = arr_industrialRenewApplicationAttachmentList;
            //gvBharatKoshReciept.DataBind();
            decimal decTotalGroundWaterRequirement = 0;
            int Count = 0;
            BindGridView(gvAffidavit, lngA_ApplicationCode, ref decTotalGroundWaterRequirement, ref Count);
            if (decTotalGroundWaterRequirement < GroundWaterRequirement100KLD)
                lblAffidavit2.Visible = true;
            else
                lblAffidavit2.Visible = false;

            lblAffidavit.Text = HttpUtility.HtmlEncode("Affidavit of Compliance of NOC Condition (" + Count.ToString() + ")");
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
                lblWaterAuditReport2.Visible = false;//disabled as member request
            else
                lblWaterAuditReport2.Visible = false;

            lblWaterAuditReport.Text = HttpUtility.HtmlEncode("Water Audit Report (" + Count.ToString() + ")");
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
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    
    private void BindIndustrialRenewApplicationAttachmentSignedDocDetails(long lngA_ApplicationCode)
    {
        try
        {
            IndustrialRenewSADApplication obj_industrialRenewApplication = new IndustrialRenewSADApplication(lngA_ApplicationCode);
            IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
            arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetSigneddocAttachmentList();
            gvApplicationSignatureSeal.DataSource = arr_industrialRenewApplicationAttachmentList;
            gvApplicationSignatureSeal.DataBind();
            lblSigneddoc2.Visible = true;
            lblSigneddoc.Text = HttpUtility.HtmlEncode("Aplication with Signature and Seal Attachment (" + arr_industrialRenewApplicationAttachmentList.Length.ToString() + ")");
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    #endregion

    #region Button Click
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
            //Server.Transfer("SelfDeclaration.aspx");
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
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));

                try
                {
                    string ErrorMessage = string.Empty;
                    //if (obj_industrialRenewApplication.GetSitePlanAttachmentList().Length < 1) { ErrorMessage = "Site Plan"; }
                    //if (obj_industrialRenewApplication.GetRainwaterHarvestingAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Rainwater Harvesting" : ErrorMessage + ",Rainwater Harvesting"; }
                    if (obj_industrialRenewApplication.GetExistingNOCAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Existing NOC" : ErrorMessage + ",Existing NOC"; }

                    if (lblRainWaterHarvesting2.Visible){if (obj_industrialRenewApplication.GetRainwaterHarvestingAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Details of Rainwater Harvesting / Artificial Recharge Measures" : ErrorMessage + ",Details of Rainwater Harvesting / Artificial Recharge Measures"; }}
                    if (lblComplianceConditionNOC2.Visible) { if (obj_industrialRenewApplication.GetIndustrialComplianceConditionNOCAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Compliance Condition NOC" : ErrorMessage + ",Compliance Condition NOC"; } }
                    if (lblAffidavit2.Visible) { if (obj_industrialRenewApplication.GetAffidavitNOCCondiAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Affidavit of Compliance of NOC Condition" : ErrorMessage + ",Affidavit of Compliance of NOC Condition"; } }
                    if (lblWaterAuditReport2.Visible) { if (obj_industrialRenewApplication.GetWaterAuditReportAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Water Audit Report" : ErrorMessage + ",Water Audit Report"; } }
                    if (lblSourceWaterAvailability2.Visible) { if (obj_industrialRenewApplication.GetCertificateNonAvaAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Source Water Non-availability Certificate" : ErrorMessage + ",Source Water Non-availability Certificate"; } }


                    //if (lblBharatKosh2.Visible){if (obj_industrialRenewApplication.GetBharatKoshRecieptAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Bharat Kosh Reciept" : ErrorMessage + ",Bharat Kosh Reciept"; }}
                    if (lblSigneddoc2.Visible){ if (obj_industrialRenewApplication.GetSigneddocAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Aplication with Signature and Seal" : ErrorMessage + ",Aplication with Signature and Seal"; } }

                    if (!string.IsNullOrEmpty(ErrorMessage))
                    {
                        lblMessage.Text = ErrorMessage + " Attachments are Mandatory.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    lngIndRenewSubmitAppCode = lng_submittedApplicationCode;
                    // Server.Transfer("Submit.aspx");
                    Server.Transfer("INDRenewReadyToSubmit.aspx");
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
                    obj_industrialRenewApplication.Dispose();
                }
            }
        }
    }
    #endregion


}