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
public partial class ExternalUser_Expansion_IND_SalientFeature : System.Web.UI.Page
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
                if (lblIndustialApplicationCodeFrom.Text.Trim() != "")
                {
                    BindLandUseFormDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    BindMSMEAttachment(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                }
                //GenratAndBindLandUseTypeGridViewDetails();
                DisplayApplicationStop();
            }
            catch (Exception)
            {
                //lblMessage.Text = ex.Message;
                //lblMessage.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }


    private void BindLandUseFormDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(lngA_ApplicationCode);
            NOCAP.BLL.Master.ExemptionLetterConditions Obj_Exemption = new NOCAP.BLL.Master.ExemptionLetterConditions();

            lblExemptionMessage.Text = "You are exempted from seeking NOC for ground water withdrawal from CGWA, since your project is under MSME category and ground water withdrawal is less than 10 KLD as per new guidelines w.e.f 24.09.2020.<br/> Please upload MSME certificate for getting Exemtion.";
            lblExemptionMessage.ForeColor = System.Drawing.Color.Green;
            //lblExemptionMessage.Text=HttpUtility.HtmlEncode("Since proposed area fall in <b>"+Obj_Exemption.AreaTypeDesc() +"("+Obj_Exemption.AreaTypeCategoryDesc()+")</b> (GWRE-2011). and proposed net ground water requirement is not more than <b>"+Obj_Exemption.GWWaterQuantityLessThan+"m<sup>3</sup>/day</b>. you are exempted from obtaining NOC for ground water withdrawal. Your Application is Elligible For Exemption");
            // txtSalientFeaturesOfInActivity.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.SalientFeatureOfIndustrialActivity);
        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
            //lblMessage.ForeColor = System.Drawing.Color.Red;
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

            if (Session["OnePage"].ToString() == "1")

                Server.Transfer("WaterRequirementDetails.aspx");
            else
                Server.Transfer("RecycledWaterUsage.aspx");

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
        NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
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
            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(lngA_ApplicationCode);

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
                NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
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
                            int lng_submittedApplicationCode = 0;
                            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));



                            if (obj_industrialNewApplication.SubmitApplication(lng_submittedApplicationCode, obj_externalUser.ExternalUserCode) == 1)
                            {
                                strActionName = "Submit";
                                strStatus = "Record Save Successfully !";
                                lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(lng_submittedApplicationCode);                                
                                lngIndSubmitAppCode = lng_submittedApplicationCode;
                                Server.Transfer("~/ExternalUser/IndustrialNew/IndExemSubmitSuccess.aspx");
                                
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
        NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(lngA_ApplicationCode);
        NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplicationAttachment[] arr_industrialNewApplicationAttachmentList = null;

        arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetMSMEAttachmentList();

        gv.DataSource = arr_industrialNewApplicationAttachmentList;
        gv.DataBind();
        AttCount = arr_industrialNewApplicationAttachmentList.Length;
        //decTotalGroundWaterRequirement = Convert.ToDecimal(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement + (obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));

        //if (gv.ID == "gvAffidavitNonAva")
        //    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetAffidavitNonAvaAttachmentList();
        //else if (gv.ID == "gvSourceofAvailabilityofSurfaceWater")
        //    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetSourceofAvailabilityofSurfaceWaterAttachmentList();
        //else if (gv.ID == "gvNonPolluting")
        //    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetNonPollutingAttachmentList();
        //else if (gv.ID == "gvRainwaterHarvesting")
        //    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetRainwaterHarvestingAttachmentList();
        //else if (gv.ID == "gvImpactReportOCS")
        //    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetImpactAssOCSAttachmentList();
        //else if (gv.ID == "gvReferralLetterAttachment")
        //    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetCpoyOfReferralLetterAttachmentList();
        //else if (gv.ID == "gvMSME")

        //{
        //    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetMSMEAttachmentList();
        //    switch (obj_industrialNewApplication.MSME)
        //    {
        //        case NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication.MSMEYesNo.Yes:
        //            lblMSME2.Visible = true;
        //            break;
        //        case NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication.MSMEYesNo.No:
        //            lblMSME2.Visible = false;
        //            break;
        //    }
        //}
        //else if (gv.ID == "gvAffidavitOtherThanMSME")
        //    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetAffidavitOtherMSMEAttachmentList();
        //else if (gv.ID == "gvWetlandArea")
        //{
        //    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetWetlandAreaAttachmentList();
        //    switch (obj_industrialNewApplication.WetLandArea)
        //    {
        //        case NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication.WetLandAreaYesNo.Yes:
        //            lblWetlandArea2.Visible = true;
        //            break;
        //        case NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication.WetLandAreaYesNo.No:
        //            lblWetlandArea2.Visible = false;
        //            break;
        //    }
        //}
        //else if (gv.ID == "gvBharatKoshReciept")
        //    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetBharatKoshRecieptAttachmentList();
        //else if (gv.ID == "gvAbstRestCharge")
        //    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetAbsRestChargeAttachmentList();
        //else if (gv.ID == "gvRestCharge")
        //    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetRestChargeAttachmentList();
        //else if (gv.ID == "gvSigneddoc")
        //    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetSignedDocAttachmentList();
        //else if (gv.ID == "gvPenalty")
        //    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetPenaltyAttachmentList();
        //else if (gv.ID == "gvConsent")
        //{
        //    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetConsentAttachmentList();
        //}
        //else if (gv.ID == "gvExtra")
        //    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetExtraAttachmentList();
        //else if (gv.ID == "gvGroundwaterAvailability")
        //    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetGroundwaterAvailabilityAttachmentList();

        //gv.DataSource = arr_industrialNewApplicationAttachmentList;
        //gv.DataBind();
        //AttCount = arr_industrialNewApplicationAttachmentList.Length;
        //decTotalGroundWaterRequirement = Convert.ToDecimal(obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement + (obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));
    }

    private void BindMSMEAttachment(long lngA_ApplicationCode)
    {
        try
        {
            //decimal decTotalGroundWaterRequirement = 0;
            int AttCount = 0;
            BindGridView(gvMSME, lngA_ApplicationCode, ref AttCount);
            lblMSME.Text = HttpUtility.HtmlEncode("MSME certificate in case of MSME (" + AttCount.ToString() + ")");
            // NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(lngA_ApplicationCode);

            //if (lblMSME2.Visible)
            //{
            //    if ((decTotalGroundWaterRequirement > GroundWaterRequirement0KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement10KLD))
            //        lblMSME2.Visible = false;
            //    else if ((decTotalGroundWaterRequirement > GroundWaterRequirement10KLD && decTotalGroundWaterRequirement <= GroundWaterRequirement100KLD))
            //        lblMSME2.Visible = true;
            //    else if (decTotalGroundWaterRequirement > GroundWaterRequirement100KLD)
            //        lblMSME2.Visible = true;
            //    else
            //        lblMSME2.Visible = false;

            //}

        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void clearMessage()
    {
        //lblmessageAffidavitNonAva.Text = "";
        //txtAffidavitNonAva.Text = "";
        //lblMessageSourceofAvailabilityofSurfaceWater.Text = "";
        //txtSourceofAvailabilityofSurfaceWater.Text = "";
        //lblMessageNonPolluting.Text = "";
        //txtNonPollutingAttachment.Text = "";
        //lblMessageRainwaterHarvesting.Text = "";
        //txtRainwaterHarvesting.Text = "";
        //lblMessageImpactReportOCS.Text = "";
        //txtImpactReportOCS.Text = "";
        //lblMessageReferralLetter.Text = "";
        //txtReferralLetter.Text = "";
        //lblMessageConsent.Text = "";
        //txtConsent.Text = "";
        lblMSMEMessage.Text = "";
        txtMSME.Text = "";
        //lblMessageAffidavitOtherThanMSME.Text = "";
        //txtAffidavitOtherThanMSME.Text = "";
        //lblMessageWetlanArea.Text = "";
        //txtWetlandArea.Text = "";
        //lblMessageBharatKoshReciept.Text = "";
        //txtBharatKoshReciept.Text = "";
        //lblMessageAbstRestCharge.Text = "";
        //txtAbstRestCharge.Text = "";
        //lblMessageRestCharge.Text = "";
        //txtRestCharge.Text = "";
        //lblMessageSigneddoc.Text = "";
        //txtSigneddoc.Text = "";
        //lblMessagePenalty.Text = "";
        //txtPenalty.Text = "";
        //lblMessageExtra.Text = "";
        //txtExtraAttachment.Text = "";
        //lblMessageGroundwaterAvailability.Text = "";
        //txtgvGroundwaterAvailability.Text = "";
        //lblMessageUndertaking.Text = "";
        //txtUndertaking.Text = "";
        //lblMessageSitePlan.Text = "";
        //txtSitePlanAttachment.Text = "";
        //lblMessageCertifiedRevenueSketch.Text = "";
        //txtCertifiedRevenueSketchAttachment.Text = "";
        //lblMessageDocumentsofOwnership.Text = "";
        //txtDocumentsofOwnership.Text = "";
        //lblMessageWaterRequirement.Text = "";
        //txtWaterRequirement.Text = "";
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
            //lblMessageSitePlan.Text = "Problem in Fetch Data";
            //lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
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
            //lblMessageSitePlan.Text = "Problem in Fetch Data";
            //lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
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
                NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
                arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetMSMEAttachmentList();

                if (arr_industrialNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
                {
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplicationAttachment obj_insertIndustrialNewApplicationAttachment = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplicationAttachment();
                    NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    List<NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplicationAttachment>();
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
            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplicationAttachment obj_industrialNewApplicationAttachment = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplicationAttachment(lng_IndustrialNewApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);
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