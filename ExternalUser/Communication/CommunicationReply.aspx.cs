using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.Misc.Communication;

public partial class ExternalUser_Communication_CommunicationReply : System.Web.UI.Page
{
    //string strPageName = "CommunicationReply";
    //string strActionName = "";
    string strStatus = "";
    CommunicationRequest.SortingField SortingField1 = CommunicationRequest.SortingField.AppCode;
    CommunicationRequest.IsVerificationSN enu_IsVerificationSN;
    CommunicationRequest.IsEvaluationSN enu_IsEvaluationSN;
    CommunicationRequest.IsScreningCommitteeSN enu_IsScreningCommitteeSN;
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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblApplicationCode");
                        if (SourceLabel != null)
                        {
                            lblApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                        }
                        Label SourceLabelPreviousPage = (Label)placeHolder.FindControl("lblApplicationRenewCode");
                        if (SourceLabelPreviousPage != null)
                        {
                            lblApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabelPreviousPage.Text);
                        }
                        if (lblApplicationCodeFrom.Text.Trim() != "")
                        {
                            GetINDINFMINDetails(Convert.ToInt64(lblApplicationCodeFrom.Text));
                            //PopulateCommunicationReplyVeri(Convert.ToInt64(lblApplicationCodeFrom.Text), sender, e);
                            PopulateCommunicationReplyVeri(Convert.ToInt64(lblApplicationCodeFrom.Text));
                            PopulateCommunicationReplyEva(Convert.ToInt64(lblApplicationCodeFrom.Text));
                            PopulateCommunicationReplyScree(Convert.ToInt64(lblApplicationCodeFrom.Text));
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



    protected void lbtnAddReply_Click(object sender, EventArgs e)
    {
        ModalPopupExtenderLogin.Show();
    }

    protected void btnSave_Click(object sender, EventArgs e)
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
                    CommunicationReply obj_CommunicationReply = new CommunicationReply();
                    obj_CommunicationReply.AppCode = Convert.ToInt64(lblApplicationCodeFrom.Text);
                    obj_CommunicationReply.CommunicatReqSNumber = Convert.ToInt32(lblCommunicationReqSNumber.Text.Trim());
                    obj_CommunicationReply.CommunicatedText = txtReplyDescription.Text.Trim();
                    string str_fname;
                    string str_ext;
                    string str_newFileNameWithPath = "";
                    byte[] buffer = new byte[1];
                    if (FileUploadCommunicatioReply.HasFile)
                    {
                        str_ext = System.IO.Path.GetExtension(FileUploadCommunicatioReply.PostedFile.FileName).ToLower();
                        str_fname = FileUploadCommunicatioReply.FileName;
                        if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                        {
                            if (NOCAPExternalUtility.IsValidFile(FileUploadCommunicatioReply.PostedFile))
                            {
                                if (FileUploadCommunicatioReply.PostedFile.ContentLength < AttachmentSizeLimit())
                                {
                                    obj_CommunicationReply.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadCommunicatioReply.PostedFile);
                                    obj_CommunicationReply.ContentType = FileUploadCommunicatioReply.PostedFile.ContentType;
                                    obj_CommunicationReply.AttachmentPath = FileUploadCommunicatioReply.FileName;
                                    obj_CommunicationReply.FileExtension = str_ext;
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblMessage.Text = "File can not upload. It has more than 5 MB size";
                                    lblMessage.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessage.Text = "Not a valid file!!..Select an other file!!";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessage.Text = "Not a valid file!!..Select an other file!!";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                    }

                    
                    // obj_CommunicationReply.AttachmentCode = obj_industrialNewApplication.IndustrialNewBharatKoshRecieptAttachmentCode;
                    obj_CommunicationReply.AttachmentName = txtAttachmentDesc.Text;
                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    obj_CommunicationReply.CreateModifySubmitByExtIntUser.CreatedByUC = (Int32)obj_externalUser.ExternalUserCode;
                    obj_CommunicationReply.FromExtUserCode = obj_externalUser.ExternalUserCode;
                    obj_CommunicationReply.ToIntUserCode = Convert.ToInt32(lblIntUserCode.Text.Trim());
                    obj_CommunicationReply.CommuniTypeCode = 1;
                    if (obj_CommunicationReply.Submit() == 1)
                    {
                        // strStatus = "File Upload Success";
                        obj_CommunicationReply.AttachmentPath = str_newFileNameWithPath;
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Text = obj_CommunicationReply.CustumMessage;
                        PopulateCommunicationReplyVeri(obj_CommunicationReply.AppCode);
                        PopulateCommunicationReplyEva(obj_CommunicationReply.AppCode);
                        PopulateCommunicationReplyScree(obj_CommunicationReply.AppCode);
                    }
                    else
                    {
                        //strStatus = "File Upload Failed";
                        lblMessage.Text = obj_CommunicationReply.CustumMessage;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    txtAttachmentDesc.Text = "";

                }
                catch (ThreadAbortException ex)
                {

                }
                catch (Exception ex)
                {
                    Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
                }
            }
        }
    }

    protected void BtnCrosClose_Click(object sender, EventArgs e)
    {
        ModalPopupExtenderLogin.Hide();
    }


    #region Private Common Function
    private void ValidationExepInit()
    {
        //    revtxtPresentwithdrawalInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        //    revtxtPresentwithdrawalInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        //    revtxtPresentwithdrawalInYear.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        //    revtxtPresentwithdrawalInYear.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        //    revtxtRWHArtificialRechargeNo.ValidationExpression = ValidationUtility.txtValForNumeric;
        //    revtxtRWHArtificialRechargeNo.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        //    revtxtRWHArtificialRechargeCapacity.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        //    revtxtRWHArtificialRechargeCapacity.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        //    revtxtRecycleReuseInDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        //    revtxtRecycleReuseInDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        //    revtxtRecycleReuseInYear.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        //    revtxtRecycleReuseInYear.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        //    revtxtWaterMeterFittedDesc.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //    revtxtWaterMeterFittedDesc.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //    revtxtGroundWaterQualityDesc.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //    revtxtGroundWaterQualityDesc.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //    revtxtRWHArtificialRechargeNoCapacityDesc.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //    revtxtRWHArtificialRechargeNoCapacityDesc.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //    revtxtPiezometerDesc.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //    revtxtPiezometerDesc.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //    revtxtRecycleReuseDesc.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //    revtxtRecycleReuseDesc.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //    revtxtActionTakenReportWithin1YearsDesc.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //    revtxtActionTakenReportWithin1YearsDesc.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //    revtxtRemarks.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //    revtxtRemarks.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //    revtxtAdoptionofVillagesDesc.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //    revtxtAdoptionofVillagesDesc.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //    revtxtPlantationofTreesDesc.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //    revtxtPlantationofTreesDesc.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //    revtxtSchoolSaniDrinkingWaterDesc.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //    revtxtSchoolSaniDrinkingWaterDesc.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;
    }
    public void GetINDINFMINDetails(long ApplicationCode)
    {

        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication();


        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication();
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication();
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication();



        NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_industrialNewApplication, out obj_infrastructureNewApplication, out obj_miningNewApplication, out obj_industrialRenewApplication, out obj_infrastructureRenewApplication, out obj_miningRenewApplication, ApplicationCode);

        if (obj_industrialNewApplication != null)
        {
            GetIndustrialDetails(ApplicationCode);

        }
        else if (obj_infrastructureNewApplication != null)
        {
            GetInfrastructureDetails(ApplicationCode);
        }
        else if (obj_miningNewApplication != null)
        {
            GetMininingDetails(ApplicationCode);

        }
        else if (obj_industrialRenewApplication != null)
        {
            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(ApplicationCode);
            if (obj_IndustrialRenewApplication != null)
                GetIndustrialDetails(obj_IndustrialRenewApplication.FirstApplicationCode);

        }
        else if (obj_infrastructureRenewApplication != null)
        {
            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(ApplicationCode);
            if (obj_InfrastructureRenewApplication != null)
                GetInfrastructureDetails(obj_InfrastructureRenewApplication.FirstApplicationCode);

        }
        else if (obj_miningRenewApplication != null)
        {
            NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(ApplicationCode);
            if (obj_MiningRenewApplication != null)
                GetMininingDetails(obj_MiningRenewApplication.FirstApplicationCode);
        }
        else
        {
            lblMessage.Text = " Application Code does not exist.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Visible = true;
        }


    }
    private void GetIndustrialDetails(long ApplicationCode)
    {
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(ApplicationCode);
        NOCAP.BLL.Master.ApplicationTypeCategory obj_ApplicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_industrialNewApplication.ApplicationTypeCode, obj_industrialNewApplication.ApplicationTypeCategoryCode);
        NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
        NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);
        NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_IndustrialNewIssusedLetter = obj_industrialNewApplication.GetIssuedLetter();

        if (obj_industrialNewApplication != null)
        {

            try
            {
                lblAppType.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.ApplicationType(obj_industrialNewApplication.ApplicationTypeCode).ApplicationTypeDescription);
                lblAppName.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.NameOfIndustry);
                lblAppPurpose.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.ApplicationPurpose(obj_industrialNewApplication.ApplicationPurposeCode).ApplicationPurposeDesc);
                lblApplicationNumber.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.IndustrialNewApplicationNumber);
            }
            catch (Exception)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);
            }
            finally
            {
                obj_industrialNewApplication = null;
                obj_ApplicationTypeCategory = null;
                obj_District = null;
                obj_SubDistrict = null;
                obj_Town = null;
                obj_Village = null;
            }
        }
    }
    private void GetInfrastructureDetails(long ApplicationCode)
    {
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(ApplicationCode);
        NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_InfrastructureNewApplication.CommunicationAddress.StateCode, obj_InfrastructureNewApplication.CommunicationAddress.DistrictCode);
        NOCAP.BLL.Master.District obj_DistrictPro = new NOCAP.BLL.Master.District(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode);
        NOCAP.BLL.Master.ApplicationTypeCategory obj_ApplicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_InfrastructureNewApplication.ApplicationTypeCode, obj_InfrastructureNewApplication.ApplicationTypeCategoryCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.CommunicationAddress.StateCode, obj_InfrastructureNewApplication.CommunicationAddress.DistrictCode, obj_InfrastructureNewApplication.CommunicationAddress.SubDistrictCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrictPro = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
        NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);

        if (obj_InfrastructureNewApplication != null)
        {

            try
            {

                lblAppType.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.ApplicationType(obj_InfrastructureNewApplication.ApplicationTypeCode).ApplicationTypeDescription);
                lblAppName.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.NameOfInfrastructure);
                lblAppPurpose.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.ApplicationPurpose(obj_InfrastructureNewApplication.ApplicationPurposeCode).ApplicationPurposeDesc);
                lblApplicationNumber.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber);
            }
            catch (Exception)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);
            }
            finally
            {
                obj_InfrastructureNewApplication = null;
                obj_ApplicationTypeCategory = null;
                obj_District = null;
                obj_SubDistrict = null;
                obj_Town = null;
                obj_Village = null;
            }
        }
    }
    private void GetMininingDetails(long ApplicationCode)
    {
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(ApplicationCode);
        NOCAP.BLL.Master.ApplicationTypeCategory obj_ApplicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_MiningNewApplication.ApplicationTypeCode, obj_MiningNewApplication.ApplicationTypeCategoryCode);
        NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_MiningNewApplication.CommunicationAddress.StateCode, obj_MiningNewApplication.CommunicationAddress.DistrictCode);
        NOCAP.BLL.Master.District obj_DistrictPro = new NOCAP.BLL.Master.District(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.CommunicationAddress.StateCode, obj_MiningNewApplication.CommunicationAddress.DistrictCode, obj_MiningNewApplication.CommunicationAddress.SubDistrictCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrictPro = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
        NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);

        if (obj_MiningNewApplication != null)
        {

            try
            {
                lblAppType.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.ApplicationType(obj_MiningNewApplication.ApplicationTypeCode).ApplicationTypeDescription);
                lblAppName.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.NameOfMining);
                lblAppPurpose.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.ApplicationPurpose(obj_MiningNewApplication.ApplicationPurposeCode).ApplicationPurposeDesc);
                lblApplicationNumber.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.MiningNewApplicationNumber);
            }
            catch (Exception)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);
            }
            finally
            {
                obj_MiningNewApplication = null;
                obj_ApplicationTypeCategory = null;
                obj_District = null;
                obj_SubDistrict = null;
                obj_Town = null;
                obj_Village = null;
            }
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
            //lblMessageSitePlan.Text = "Problem in Fetch Data";
            //lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
            return 0;
        }

    }
    #endregion

    #region Communication at verification
    #region Private FunctionM

    private void PopulateCommunicationReplyVeri(long AppCode)
    {
        try
        {
            CommunicationRequest obj_CommunicationRequest = new CommunicationRequest { AppCode=AppCode};            
            enu_IsVerificationSN = NOCAP.BLL.Misc.Communication.CommunicationRequest.IsVerificationSN.Yes;
            obj_CommunicationRequest.GetAll(SortingField1, enu_IsVerificationSN);
            NOCAP.BLL.Misc.Communication.CommunicationRequest[] arr = null;
            arr = obj_CommunicationRequest.CommunicationRequestCollection;
            grdviewCommRequestVeri.DataSource = arr;
            grdviewCommRequestVeri.DataBind();

        }
        catch (Exception)
        {
           // lblMessage.Text = ex.Message;
        }



    }

    #endregion



    #region grdviewCommRequestVeri   
    protected void grdviewCommRequestVeri_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCommIsCosingVeri = (Label)e.Row.FindControl("lblCommIsCosingVeri");
                if (lblCommIsCosingVeri.Text.Trim() == "Yes")
                {
                    LinkButton lbtnAddReplyVeri = (LinkButton)e.Row.FindControl("lbtnAddReplyVeri");
                    lbtnAddReplyVeri.Visible = false;
                }
                else
                {
                    int rownumber = e.Row.RowIndex;
                    if (rownumber > 0)
                    {
                        LinkButton lbtnAddReplyVeri = (LinkButton)e.Row.FindControl("lbtnAddReplyVeri");
                        lbtnAddReplyVeri.Visible = true;
                    }
                }
                Label FromIntUserCode = (Label)e.Row.FindControl("lblFromUser");
                lblIntUserCode.Text = FromIntUserCode.Text.Trim();
                NOCAP.BLL.UserManagement.User obj = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(FromIntUserCode.Text));
                Label lblCommRequestedUser = (Label)e.Row.FindControl("lblCommRequestedUser");
                LinkButton btnlNoFileAttachmentVeri = (LinkButton)e.Row.FindControl("btnlNoFileAttachmentVeri");
                lblCommRequestedUser.Text = obj.UserName;
                LinkButton lbtnAttachment = (LinkButton)e.Row.FindControl("lbtnAttachment");
                HiddenField hdnAttachment = (HiddenField)e.Row.FindControl("hdnAttachment");
                string[] hdnAttachmentArr = hdnAttachment.Value.Split(',');
                NOCAP.BLL.Misc.Communication.CommuniRequestAttachment obj_CommuniRequestAttachment = new NOCAP.BLL.Misc.Communication.CommuniRequestAttachment();
                obj_CommuniRequestAttachment.AppCode = Convert.ToInt64(hdnAttachmentArr[0].ToString());
                obj_CommuniRequestAttachment.CommunicatReqSNumber = Convert.ToInt32(hdnAttachmentArr[1].ToString());
                obj_CommuniRequestAttachment.GetAll();
                GridView grdCommReqAttachmentVeri = (GridView)e.Row.FindControl("grdCommReqAttachmentVeri");

                if (obj_CommuniRequestAttachment.CommuniRequestAttachmentCollection.Length > 0)
                {
                    grdCommReqAttachmentVeri.DataSource = obj_CommuniRequestAttachment.CommuniRequestAttachmentCollection;
                    grdCommReqAttachmentVeri.DataBind();
                    grdCommReqAttachmentVeri.Visible = true;
                }
                else
                {
                    btnlNoFileAttachmentVeri.Visible = true;
                    btnlNoFileAttachmentVeri.Enabled = false;
                }


                string AppCode = grdviewCommRequestVeri.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView grdviewCommReplyVeri = (GridView)e.Row.FindControl("grdviewCommReplyVeri");
                NOCAP.BLL.Misc.Communication.CommunicationReply obj_CommunicationReply = new NOCAP.BLL.Misc.Communication.CommunicationReply();
                obj_CommunicationReply.AppCode = Convert.ToInt64(AppCode);

                obj_CommunicationReply.CommunicatReqSNumber = Convert.ToInt32(hdnAttachmentArr[1].ToString());
                obj_CommunicationReply.GetAll();

                if (obj_CommunicationReply.CommunicationReplyCollection.Length > 0)
                {
                    grdviewCommReplyVeri.Visible = true;
                    grdviewCommReplyVeri.DataSource = obj_CommunicationReply.CommunicationReplyCollection;
                    grdviewCommReplyVeri.DataBind();
                }
                else
                {
                    grdviewCommReplyVeri.Visible = false;


                }
            }
        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
        }
    }
    protected void grdviewCommRequestVeri_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "AppCode")
            {

                ModalPopupExtenderLogin.Show();
                string[] arr = e.CommandArgument.ToString().Split(',');
                lblCommunicationReqSNumber.Text = arr[1].ToString();
            }
        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region grdviewCommReplyVeri
    protected void lbtnReqAttachmentVeri_Command(object sender, CommandEventArgs e)
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
                    long lng_AppCode = Convert.ToInt64(CommandArgument[0]);
                    int int_CommunicatReqSNumber = Convert.ToInt32(CommandArgument[1]);
                    int int_AttachmentCode = Convert.ToInt32(CommandArgument[2]);
                    int int_AttachmentCodeSNum = Convert.ToInt32(CommandArgument[3]);

                    NOCAPExternalUtility.CommReqDownloadFiles(lng_AppCode, int_AttachmentCode, int_AttachmentCodeSNum);
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
    protected void grdviewCommReplyVeri_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblFromExUser = (Label)e.Row.FindControl("lblFromExUser");

                NOCAP.BLL.UserManagement.ExternalUser obj = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(lblFromExUser.Text));

                Label lblCommReplyUser = (Label)e.Row.FindControl("lblCommReplyUser");
                LinkButton btnReplyNoFileAttachmentVeri = (LinkButton)e.Row.FindControl("btnReplyNoFileAttachmentVeri");
                lblCommReplyUser.Text = obj.ExternalUserName;
               
                HiddenField hdnReplyAttachment = (HiddenField)e.Row.FindControl("hdnReplyAttachment");
                string[] hdnAttachmentArr = hdnReplyAttachment.Value.Split(',');
                NOCAP.BLL.Misc.Communication.CommuniReplyAttachment obj_CommuniReplyAttachment = new NOCAP.BLL.Misc.Communication.CommuniReplyAttachment();
                obj_CommuniReplyAttachment.AppCode = Convert.ToInt64(hdnAttachmentArr[0].ToString());
                obj_CommuniReplyAttachment.CommunicatReplySN = Convert.ToInt32(hdnAttachmentArr[1].ToString());
                obj_CommuniReplyAttachment.GetAll();
                GridView grdCommRepAttachmentVeri = (GridView)e.Row.FindControl("grdCommRepAttachmentVeri");

                if (obj_CommuniReplyAttachment.CommuniReplyAttachmentCollection.Length > 0)
                {
                    grdCommRepAttachmentVeri.DataSource = obj_CommuniReplyAttachment.CommuniReplyAttachmentCollection;
                    grdCommRepAttachmentVeri.DataBind();
                    grdCommRepAttachmentVeri.Visible = true;
                }
                else
                {
                    btnReplyNoFileAttachmentVeri.Visible = true;
                    btnReplyNoFileAttachmentVeri.Enabled = false;
                }
            }
        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
        }
    }
    protected void lbtnReplyAttachmentVeri_Command(object sender, CommandEventArgs e)
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
                    long lng_AppCode = Convert.ToInt64(CommandArgument[0]);
                    int int_CommunicatReqSNumber = Convert.ToInt32(CommandArgument[1]);
                    int int_AttachmentCode = Convert.ToInt32(CommandArgument[2]);
                    int int_AttachmentCodeSNum = Convert.ToInt32(CommandArgument[3]);

                    NOCAPExternalUtility.CommReplyDownloadFiles(lng_AppCode, int_AttachmentCode, int_AttachmentCodeSNum);
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

    #endregion

    #region Communication at Eva
    #region Private Function
    private void PopulateCommunicationReplyEva(long AppCode)
    {
        try
        {
            NOCAP.BLL.Misc.Communication.CommunicationRequest obj_CommunicationRequest = new NOCAP.BLL.Misc.Communication.CommunicationRequest();
            obj_CommunicationRequest.AppCode = AppCode;
            enu_IsVerificationSN = NOCAP.BLL.Misc.Communication.CommunicationRequest.IsVerificationSN.No;
            enu_IsEvaluationSN = NOCAP.BLL.Misc.Communication.CommunicationRequest.IsEvaluationSN.Yes;
            obj_CommunicationRequest.GetAll(SortingField1, enu_IsVerificationSN, enu_IsEvaluationSN);
            NOCAP.BLL.Misc.Communication.CommunicationRequest[] arr = null;
            arr = obj_CommunicationRequest.CommunicationRequestCollection;
            grdviewCommRequestEva.DataSource = arr;
            grdviewCommRequestEva.DataBind();

        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
        }
    }
    #endregion
    protected void lbtnAttachmentEva_Command(object sender, CommandEventArgs e)
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
                    long lng_AppCode = Convert.ToInt64(CommandArgument[0]);
                    int int_CommunicatReqSNumber = Convert.ToInt32(CommandArgument[1]);
                    int int_AttachmentCode = Convert.ToInt32(CommandArgument[2]);
                    int int_AttachmentCodeSNum = Convert.ToInt32(CommandArgument[3]);
                    NOCAPExternalUtility.CommReqDownloadFiles(lng_AppCode, int_AttachmentCode, int_AttachmentCodeSNum);
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
    #region grdviewCommReplyEva
    protected void grdviewCommReplyEva_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblFromExUser = (Label)e.Row.FindControl("lblFromExUser");
                NOCAP.BLL.UserManagement.ExternalUser obj = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(lblFromExUser.Text));
                Label lblCommReplyUser = (Label)e.Row.FindControl("lblCommReplyUser");
                LinkButton btnReplyNoFileAttachmentEva = (LinkButton)e.Row.FindControl("btnReplyNoFileAttachmentEva");
                lblCommReplyUser.Text = obj.ExternalUserName;
                //LinkButton lbtnAttachment = (LinkButton)e.Row.FindControl("lbtnAttachment");
                HiddenField hdnReplyAttachment = (HiddenField)e.Row.FindControl("hdnReplyAttachment");
                string[] hdnAttachmentArr = hdnReplyAttachment.Value.Split(',');
                NOCAP.BLL.Misc.Communication.CommuniReplyAttachment obj_CommuniReplyAttachment = new NOCAP.BLL.Misc.Communication.CommuniReplyAttachment();
                obj_CommuniReplyAttachment.AppCode = Convert.ToInt64(hdnAttachmentArr[0].ToString());
                obj_CommuniReplyAttachment.CommunicatReplySN = Convert.ToInt32(hdnAttachmentArr[1].ToString());
                obj_CommuniReplyAttachment.GetAll();
                GridView grdCommRepAttachment = (GridView)e.Row.FindControl("grdCommRepAttachment");

                if (obj_CommuniReplyAttachment.CommuniReplyAttachmentCollection.Length > 0)
                {
                    grdCommRepAttachment.DataSource = obj_CommuniReplyAttachment.CommuniReplyAttachmentCollection;
                    grdCommRepAttachment.DataBind();
                    grdCommRepAttachment.Visible = true;
                }
                else
                {
                    btnReplyNoFileAttachmentEva.Visible = true;
                    btnReplyNoFileAttachmentEva.Enabled = false;
                }
            }
        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
        }
    }

    protected void lbtnReplyAttachmentEva_Command(object sender, CommandEventArgs e)
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
                    long lng_AppCode = Convert.ToInt64(CommandArgument[0]);
                    int int_CommunicatReqSNumber = Convert.ToInt32(CommandArgument[1]);
                    int int_AttachmentCode = Convert.ToInt32(CommandArgument[2]);
                    int int_AttachmentCodeSNum = Convert.ToInt32(CommandArgument[3]);

                    NOCAPExternalUtility.CommReplyDownloadFiles(lng_AppCode, int_AttachmentCode, int_AttachmentCodeSNum);
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
    #region grdviewCommRequestEva
    protected void grdviewCommRequestEva_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCommIsCosingEva = (Label)e.Row.FindControl("lblCommIsCosingEva");
                int rownumber = e.Row.RowIndex;
                if (rownumber > 0)
                {
                    LinkButton lbtnAddReply = (LinkButton)e.Row.FindControl("lbtnAddReply");
                    lbtnAddReply.Visible = false;
                }
                Label FromIntUserCode = (Label)e.Row.FindControl("lblFromUser");
                lblIntUserCode.Text = FromIntUserCode.Text.Trim();
                NOCAP.BLL.UserManagement.User obj = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(FromIntUserCode.Text));

                Label lblCommRequestedUser = (Label)e.Row.FindControl("lblCommRequestedUser");
                LinkButton btnlNoFileAttachmentEva = (LinkButton)e.Row.FindControl("btnlNoFileAttachmentEva");
                lblCommRequestedUser.Text = obj.UserName;
                LinkButton lbtnAttachment = (LinkButton)e.Row.FindControl("lbtnAttachment");
                HiddenField hdnAttachment = (HiddenField)e.Row.FindControl("hdnAttachment");
                string[] hdnAttachmentArr = hdnAttachment.Value.Split(',');
                NOCAP.BLL.Misc.Communication.CommuniRequestAttachment obj_CommuniRequestAttachment = new NOCAP.BLL.Misc.Communication.CommuniRequestAttachment();
                obj_CommuniRequestAttachment.AppCode = Convert.ToInt64(hdnAttachmentArr[0].ToString());
                obj_CommuniRequestAttachment.CommunicatReqSNumber = Convert.ToInt32(hdnAttachmentArr[1].ToString());
                obj_CommuniRequestAttachment.GetAll();
                GridView grdCommReqAttachmentEva = (GridView)e.Row.FindControl("grdCommReqAttachmentEva");

                if (obj_CommuniRequestAttachment.CommuniRequestAttachmentCollection.Length > 0)
                {
                    grdCommReqAttachmentEva.DataSource = obj_CommuniRequestAttachment.CommuniRequestAttachmentCollection;
                    grdCommReqAttachmentEva.DataBind();
                    grdCommReqAttachmentEva.Visible = true;
                }
                else
                {
                    btnlNoFileAttachmentEva.Visible = true;
                    btnlNoFileAttachmentEva.Enabled = false;
                }


                string AppCode = grdviewCommRequestVeri.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView grdviewCommReplyEva = (GridView)e.Row.FindControl("grdviewCommReplyEva");
                NOCAP.BLL.Misc.Communication.CommunicationReply obj_CommunicationReply = new NOCAP.BLL.Misc.Communication.CommunicationReply();
                obj_CommunicationReply.AppCode = Convert.ToInt64(AppCode);

                obj_CommunicationReply.CommunicatReqSNumber = Convert.ToInt32(hdnAttachmentArr[1].ToString());
                obj_CommunicationReply.GetAll();

                if (obj_CommunicationReply.CommunicationReplyCollection.Length > 0)
                {
                    grdviewCommReplyEva.Visible = true;
                    grdviewCommReplyEva.DataSource = obj_CommunicationReply.CommunicationReplyCollection;
                    grdviewCommReplyEva.DataBind();
                }
                else
                {
                    grdviewCommReplyEva.Visible = false;
                }
            }
        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
        }
    }
    protected void grdviewCommRequestEva_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "AppCode")
            {
                ModalPopupExtenderLogin.Show();
                string[] arr = e.CommandArgument.ToString().Split(',');
                lblCommunicationReqSNumber.Text = arr[1].ToString();
            }
        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
        }
    }
    #endregion
    #endregion

    #region Communication at ScreeningCommittee
    #region Private Function

    private void PopulateCommunicationReplyScree(long AppCode)
    {
        try
        {
            NOCAP.BLL.Misc.Communication.CommunicationRequest obj_CommunicationRequest = new NOCAP.BLL.Misc.Communication.CommunicationRequest();
            obj_CommunicationRequest.AppCode = AppCode;
            enu_IsVerificationSN = NOCAP.BLL.Misc.Communication.CommunicationRequest.IsVerificationSN.No;
            enu_IsEvaluationSN = NOCAP.BLL.Misc.Communication.CommunicationRequest.IsEvaluationSN.No;
            enu_IsScreningCommitteeSN = NOCAP.BLL.Misc.Communication.CommunicationRequest.IsScreningCommitteeSN.Yes;
            obj_CommunicationRequest.GetAll(SortingField1, enu_IsVerificationSN, enu_IsEvaluationSN, enu_IsScreningCommitteeSN);
            NOCAP.BLL.Misc.Communication.CommunicationRequest[] arr = null;
            arr = obj_CommunicationRequest.CommunicationRequestCollection;
            grdviewCommRequestScree.DataSource = arr;
            grdviewCommRequestScree.DataBind();

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }



    }
    #endregion

    #region grdviewCommRequestScree
    protected void grdviewCommRequestScree_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCommIsCosingScree = (Label)e.Row.FindControl("lblCommIsCosingScree");
                if (lblCommIsCosingScree.Text.Trim() == "Yes")
                {
                    LinkButton lbtnAddReplyScree = (LinkButton)e.Row.FindControl("lbtnAddReplyScree");
                    lbtnAddReplyScree.Visible = false;
                }
                else
                {
                    int rownumber = e.Row.RowIndex;
                    if (rownumber > 0)
                    {
                        LinkButton lbtnAddReplyScree = (LinkButton)e.Row.FindControl("lbtnAddReplyScree");
                        lbtnAddReplyScree.Visible = false;
                    }
                }
                Label FromIntUserCode = (Label)e.Row.FindControl("lblFromUser");
                lblIntUserCode.Text = FromIntUserCode.Text.Trim();
                NOCAP.BLL.UserManagement.User obj = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(FromIntUserCode.Text));
                Label lblCommRequestedUser = (Label)e.Row.FindControl("lblCommRequestedUser");
                LinkButton btnlNoFileAttachmentScree = (LinkButton)e.Row.FindControl("btnlNoFileAttachmentScree");
                lblCommRequestedUser.Text = obj.UserName;
                LinkButton lbtnAttachment = (LinkButton)e.Row.FindControl("lbtnAttachment");
                HiddenField hdnAttachment = (HiddenField)e.Row.FindControl("hdnAttachment");
                string[] hdnAttachmentArr = hdnAttachment.Value.Split(',');
                NOCAP.BLL.Misc.Communication.CommuniRequestAttachment obj_CommuniRequestAttachment = new NOCAP.BLL.Misc.Communication.CommuniRequestAttachment();
                obj_CommuniRequestAttachment.AppCode = Convert.ToInt64(hdnAttachmentArr[0].ToString());
                obj_CommuniRequestAttachment.CommunicatReqSNumber = Convert.ToInt32(hdnAttachmentArr[1].ToString());
                obj_CommuniRequestAttachment.GetAll();
                GridView grdCommReqAttachmentScree = (GridView)e.Row.FindControl("grdCommReqAttachmentScree");

                if (obj_CommuniRequestAttachment.CommuniRequestAttachmentCollection.Length > 0)
                {
                    grdCommReqAttachmentScree.DataSource = obj_CommuniRequestAttachment.CommuniRequestAttachmentCollection;
                    grdCommReqAttachmentScree.DataBind();
                    grdCommReqAttachmentScree.Visible = true;
                }
                else
                {
                    btnlNoFileAttachmentScree.Visible = true;
                    btnlNoFileAttachmentScree.Enabled = false;
                }


                string AppCode = grdviewCommRequestScree.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView grdviewCommReplyScree = (GridView)e.Row.FindControl("grdviewCommReplyScree");
                NOCAP.BLL.Misc.Communication.CommunicationReply obj_CommunicationReply = new NOCAP.BLL.Misc.Communication.CommunicationReply();
                obj_CommunicationReply.AppCode = Convert.ToInt64(AppCode);

                obj_CommunicationReply.CommunicatReqSNumber = Convert.ToInt32(hdnAttachmentArr[1].ToString());
                obj_CommunicationReply.GetAll();

                if (obj_CommunicationReply.CommunicationReplyCollection.Length > 0)
                {
                    grdviewCommReplyScree.Visible = true;
                    grdviewCommReplyScree.DataSource = obj_CommunicationReply.CommunicationReplyCollection;
                    grdviewCommReplyScree.DataBind();
                }
                else
                {
                    grdviewCommReplyScree.Visible = false;
                }
            }
        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
        }
    }

    protected void grdviewCommRequestScree_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            if (e.CommandName == "AppCode")
            {

                ModalPopupExtenderLogin.Show();
                string[] arr = e.CommandArgument.ToString().Split(',');
                lblCommunicationReqSNumber.Text = arr[1].ToString();
            }
        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
        }
    }

    protected void lbtnReqAttachmentScree_Command(object sender, CommandEventArgs e)
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
                    long lng_AppCode = Convert.ToInt64(CommandArgument[0]);
                    int int_CommunicatReqSNumber = Convert.ToInt32(CommandArgument[1]);
                    int int_AttachmentCode = Convert.ToInt32(CommandArgument[2]);
                    int int_AttachmentCodeSNum = Convert.ToInt32(CommandArgument[3]);
                    NOCAPExternalUtility.CommReqDownloadFiles(lng_AppCode, int_AttachmentCode, int_AttachmentCodeSNum);
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

    #region grdviewCommReplyScree
    protected void grdviewCommReplyScree_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblFromExUser = (Label)e.Row.FindControl("lblFromExUser");

                NOCAP.BLL.UserManagement.ExternalUser obj = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(lblFromExUser.Text));

                Label lblCommReplyUser = (Label)e.Row.FindControl("lblCommReplyUser");
                LinkButton btnReplyNoFileAttachmentScree = (LinkButton)e.Row.FindControl("btnReplyNoFileAttachmentScree");
                lblCommReplyUser.Text = obj.ExternalUserName;
                HiddenField hdnReplyAttachment = (HiddenField)e.Row.FindControl("hdnReplyAttachment");
                string[] hdnAttachmentArr = hdnReplyAttachment.Value.Split(',');
                NOCAP.BLL.Misc.Communication.CommuniReplyAttachment obj_CommuniReplyAttachment = new NOCAP.BLL.Misc.Communication.CommuniReplyAttachment();
                obj_CommuniReplyAttachment.AppCode = Convert.ToInt64(hdnAttachmentArr[0].ToString());
                obj_CommuniReplyAttachment.CommunicatReplySN = Convert.ToInt32(hdnAttachmentArr[1].ToString());
                obj_CommuniReplyAttachment.GetAll();
                GridView grdCommRepAttachmentScree = (GridView)e.Row.FindControl("grdCommRepAttachmentScree");
                if (obj_CommuniReplyAttachment.CommuniReplyAttachmentCollection.Length > 0)
                {
                    grdCommRepAttachmentScree.DataSource = obj_CommuniReplyAttachment.CommuniReplyAttachmentCollection;
                    grdCommRepAttachmentScree.DataBind();
                    grdCommRepAttachmentScree.Visible = true;
                }
                else
                {
                    btnReplyNoFileAttachmentScree.Visible = true;
                    btnReplyNoFileAttachmentScree.Enabled = false;
                }
            }
        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
        }
    }

    protected void lbtnReplyAttachmentScree_Command(object sender, CommandEventArgs e)
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
                    long lng_AppCode = Convert.ToInt64(CommandArgument[0]);
                    int int_CommunicatReqSNumber = Convert.ToInt32(CommandArgument[1]);
                    int int_AttachmentCode = Convert.ToInt32(CommandArgument[2]);
                    int int_AttachmentCodeSNum = Convert.ToInt32(CommandArgument[3]);
                    NOCAPExternalUtility.CommReplyDownloadFiles(lng_AppCode, int_AttachmentCode, int_AttachmentCodeSNum);
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
    #endregion
}