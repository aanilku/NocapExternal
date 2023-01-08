using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP;
using System.Threading;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_IndustrialRenew_Submit : System.Web.UI.Page
{
    string strPageName = "INDRenewSubmit";
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
        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            try
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
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
                        else
                        {
                            SourceLabel = (Label)placeHolder.FindControl("lblMode");
                            lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                        }
                        SourceLabel = (Label)placeHolder.FindControl("lblIndustialApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                        }
                        else
                        {
                            SourceLabel = (Label)placeHolder.FindControl("lblApplicationCodeFrom");
                            if (SourceLabel != null)
                            {
                                lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                            }
                            else
                            {
                                SourceLabel = (Label)placeHolder.FindControl("lblApplicationCode");
                                if (SourceLabel != null)
                                {
                                    lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                                }
                            }

                        }
                        bindDetails();
                        DisplayApplicationStop();
                        BindSelfDeclarationDetails();
                    }

                }



            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    private int DisplayApplicationStop()
    {
        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewSADApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_IndustrialRenewSADApplication.FirstApplicationCode);

        NOCAP.BLL.Master.ApplicationStop obj_applicationStop = new NOCAP.BLL.Master.ApplicationStop(obj_IndustrialRenewSADApplication.ApplicationTypeCode, obj_IndustrialRenewSADApplication.ApplicationPurposeCode);
        if (obj_applicationStop.Stop == NOCAP.BLL.Master.ApplicationStop.StopYesNo.Yes)
        {
            lblAppStop.Text = HttpUtility.HtmlEncode("Submission-Presently Closed." + " We are not accepting application.");
            lblAppStop.Enabled = true;
            lblAppStop.Visible = true;
            btnSubmit.Enabled = false;
            return 0;
        }
        else
            return 1;
    }
    protected void bindDetails()
    {
        try
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;

                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplicationPrevious = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
                NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplicationPrevious = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication();


                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplicationFirst = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewSADApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
                obj_industrialNewApplicationFirst = obj_IndustrialRenewSADApplication.GetFirstIndustrialApplication();

                // New Code added for Nth Renewal

                obj_IndustrialRenewSADApplication.GetPreviousIndustrialApplication(out obj_industrialNewApplicationPrevious, out obj_industrialRenewApplicationPrevious);

                if (obj_industrialNewApplicationPrevious != null)
                {
                    // Existing NOC Details

                    lblINDExistingNOCNumber.Text = HttpUtility.HtmlEncode(obj_industrialNewApplicationPrevious.NOCNumber);

                    NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_industrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter(obj_industrialNewApplicationPrevious.IndustrialNewApplicationCode);
                    if (obj_industrialNewIssusedLetter.ValidityStartDate != null && obj_industrialNewIssusedLetter.ValidityEndDate != null)
                    {
                        lblINDNOCValidity.Text = "(" + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_industrialNewIssusedLetter.ValidityStartDate)).ToString("dd/MM/yyyy") + " - " + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_industrialNewIssusedLetter.ValidityEndDate)).ToString("dd/MM/yyyy") + ")";
                    }
                    // End Existing NOC Details
                }

                if (obj_industrialRenewApplicationPrevious != null)
                {
                    lblINDExistingNOCNumber.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplicationPrevious.NOCNumber);

                    NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter obj_industrialRenewIssusedLetter = new NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter(obj_industrialRenewApplicationPrevious.IndustrialRenewApplicationCode);
                    if (obj_industrialRenewIssusedLetter.ValidityStartDate != null && obj_industrialRenewIssusedLetter.ValidityEndDate != null)
                    {
                        lblINDNOCValidity.Text = "(" + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_industrialRenewIssusedLetter.ValidityStartDate)).ToString("dd/MM/yyyy") + " - " + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_industrialRenewIssusedLetter.ValidityEndDate)).ToString("dd/MM/yyyy") + ")";
                    }
                }

                // End New Code added for Nth Renewal

                NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_industrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.DistrictCode);
                NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_industrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
                NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_industrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.TownCode);
                NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_industrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.VillageCode);


                lblNetGroundWaterRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString((obj_IndustrialRenewSADApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist==null?0: obj_IndustrialRenewSADApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist) + (obj_IndustrialRenewSADApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional==null?0: obj_IndustrialRenewSADApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional)));
                lblNameOfIndustry.Text = HttpUtility.HtmlEncode(obj_IndustrialRenewSADApplication.NameOfIndustry);


                lblAppliedForRenewal.Text = HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(obj_IndustrialRenewSADApplication.LinkDepth));


                if (obj_industrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.StateCode > 0)
                    lblState.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.GetState().StateName));
                //lblSubDistrict.Text = Convert.ToString(obj_industrialNewApplicationPrevious.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
                // lblAreaType.Text = HttpUtility.HtmlEncode(obj_industrialNewApplicationFirst.GetApplySubDistrictAreaTypeCategoryHistory().AreaTypeDesc());
                lblAreaTypeCatagory.Text = HttpUtility.HtmlEncode(obj_industrialNewApplicationFirst.GetApplySubDistrictAreaTypeCategoryHistory().AreaTypeCategoryDesc());

                // added by GuruDas

                NOCAP.BLL.Master.SubDistrictAreaTypeCategory obj_presSubDistrictAreaTypeCategory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategory(obj_industrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_presSubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_presSubDistrictAreaTypeCategory.StateCode, obj_presSubDistrictAreaTypeCategory.DistrictCode, obj_presSubDistrictAreaTypeCategory.SubDistrictCode, obj_presSubDistrictAreaTypeCategory.SubDistrictCurrentAreaCategoryKey);
                // lblpresAreaType.Text = HttpUtility.HtmlEncode(obj_presSubDistrictAreaTypeCategoryHistory.AreaTypeDesc());
                lblpresAreaTypeCatagory.Text = HttpUtility.HtmlEncode(obj_presSubDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc());
                // End added by GuruDas


                if (obj_District.DistrictName != "")
                    lblDistrict.Text = HttpUtility.HtmlEncode(obj_District.DistrictName);
                if (obj_SubDistrict.SubDistrictName != "")
                    lblSubDistrict.Text = HttpUtility.HtmlEncode(obj_SubDistrict.SubDistrictName);

                if (obj_Town.TownName != "")
                    lblVillageTown.Text = HttpUtility.HtmlEncode(obj_Town.TownName);
                if (obj_Village.VillageName != "")
                    lblVillageTown.Text = HttpUtility.HtmlEncode(obj_Village.VillageName);
            }

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindSelfDeclarationDetails()
    {
        try
        {

            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewSADApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
            //if (obj_industrialRenewSADApplication.BhaTransRefNoOnline != "")
            //{
            //    tblOfflinePayment.Visible = false;
            //    tblOnlinePayment.Visible = true;

            //    lblTransactionNo.Text = obj_industrialRenewSADApplication.BhaTransRefNoOnline;
            //    lblTransactionDate.Text = Convert.ToDateTime(obj_industrialRenewSADApplication.BhaTransDatedOnline).ToString("dd/MM/yyyy");

            //}
            //else
            //{
            //    tblOfflinePayment.Visible = true;
            //    tblOnlinePayment.Visible = false;

            //    txtBharatKoshRefferenceNo.Text = "";

            //    txtBharatKoshDated.Text = "";
            //}

            NOCAP.BLL.Master.FeeRequiredPending objFeeRequiredPending = new NOCAP.BLL.Master.FeeRequiredPending(obj_industrialRenewSADApplication.ApplicationTypeCode, obj_industrialRenewSADApplication.ApplicationPurposeCode);
            if (objFeeRequiredPending != null && objFeeRequiredPending.RequiredPending == NOCAP.BLL.Master.FeeRequiredPending.RequiredOrPending.Required)
                lblFee.Text = HttpUtility.HtmlEncode("Reciept of Processing Fee of Rs. " + objFeeRequiredPending.Amount + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(objFeeRequiredPending.Amount))) + ") submitted  through NON TAX RECEIPT PORTAL (https://bharatkosh.gov.in) should be attached in online application at prescribed place before submission of application.");
            else
                lblFee.Text = "Processing Fee : Not Required.";
            NOCAP.BLL.Template.SelfDeclaration.SelfDeclarationTemplate obj_SelfDeclarationTemplate = new NOCAP.BLL.Template.SelfDeclaration.SelfDeclarationTemplate();
            obj_SelfDeclarationTemplate.GetAll();
            NOCAP.BLL.Template.SelfDeclaration.SelfDeclarationTemplate[] arr = obj_SelfDeclarationTemplate.SelfDeclarationTemplateCollection;
            arr = Array.FindAll(arr, a => a.WaterQuantityStart < Convert.ToDecimal(lblNetGroundWaterRequirement.Text) && a.WaterQuantityUpto >= Convert.ToDecimal(lblNetGroundWaterRequirement.Text));


            obj_SelfDeclarationTemplate = arr.SingleOrDefault();
            GeneralConditionCustomEditor.Content = obj_SelfDeclarationTemplate.GeneralConditionTemplateContent;



        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

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
                //int FlageForUpdate = 1;
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                bool PaymentFlage = false;
                strActionName = "Submit Application";
                long lng_submittedApplicationCode = 0;
                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplicationPrev = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
                NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplicationPrev = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication();

                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));


                //NOCAP.BLL.Master.NTRPIntegration obj_NTRPIntegration = new NOCAP.BLL.Master.NTRPIntegration((int)NOCAP.BLL.Common.CommonEnum.ApplicationType.Industrial);


                //#region To Check Offline or online payment
                //switch (obj_industrialRenewApplication.PaymentTypeMode)
                //{
                //    #region Combined
                //    case NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined:
                //        #region Proccessing Fee
                //        if (obj_industrialRenewApplication.ProFeeOrderPaymentCode != null)
                //        {
                //            PaymentFlage = true;
                //        }
                //        else
                //            PaymentFlage = false;

                //        #endregion

                //        break;
                //    #endregion

                //    #region Single
                //    case NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single:
                //        #region Proccessing Fee
                //        if (obj_industrialRenewApplication.ProFeeOrderPaymentCode != null)
                //            PaymentFlage = true;
                //        else
                //            PaymentFlage = false;
                //        #endregion

                //        #region Charge
                //        if (!PaymentFlage)
                //        {
                //            if (obj_industrialRenewApplication.GWChargeOrderPaymentCode != null)
                //                PaymentFlage = true;
                //            else
                //                PaymentFlage = false;
                //        }
                //        #endregion

                //        #region Penalty
                //        if (!PaymentFlage)
                //        {
                //            if (obj_industrialRenewApplication.PenaltyOrderPaymentCode != null)
                //                PaymentFlage = true;
                //            else
                //                PaymentFlage = false;
                //        }
                //        #endregion
                //        break;
                //    #endregion
                //    default:
                //        PaymentFlage = false;
                //        break;

                //}
                //#endregion
                //if (obj_NTRPIntegration.Active == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes && PaymentFlage)
                //{
                //    #region NTRPIntegration
                //    NOCAP.BLL.Misc.Payment.SADOnlinePayment obj_SADOnlinePayment = null;

                //    if (obj_industrialRenewApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined)
                //    {
                //        if (obj_industrialRenewApplication.ProFeeOrderPaymentCode != null)
                //        {
                //            obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_industrialRenewApplication.IndustrialRenewApplicationCode, obj_industrialRenewApplication.ProFeeOrderPaymentCode.ToString());
                //            if (obj_SADOnlinePayment.FinalPaymentStatus != NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //            {
                //                lblMessage.Text = "Please wait for payment is being success";
                //                lblMessage.ForeColor = System.Drawing.Color.Red;
                //                return;
                //            }

                //        }
                //        else
                //        {
                //            lblMessage.Text = "Please make payment for charges";
                //            lblMessage.ForeColor = System.Drawing.Color.Red;
                //            return;
                //        }
                //    }
                //    else if (obj_industrialRenewApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single)
                //    {
                //        if (obj_industrialRenewApplication.ProFeeOrderPaymentCode != null)
                //        {
                //            obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_industrialRenewApplication.IndustrialRenewApplicationCode, (obj_industrialRenewApplication.ProFeeOrderPaymentCode == null ? "0" : obj_industrialRenewApplication.ProFeeOrderPaymentCode.ToString()));
                //            if (obj_SADOnlinePayment.FinalPaymentStatus != NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //            {
                //                lblMessage.Text = "Please wait for payment is being success";
                //                lblMessage.ForeColor = System.Drawing.Color.Red;
                //                return;
                //            }
                //        }
                //        else
                //        {
                //            lblMessage.Text = "Please make payment for application fee";
                //            lblMessage.ForeColor = System.Drawing.Color.Red;
                //            return;
                //        }
                //        if (obj_industrialRenewApplication.GWChargeOrderPaymentCode != null && obj_industrialRenewApplication.WaterQualityCode == 1)
                //        {
                //            obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_industrialRenewApplication.IndustrialRenewApplicationCode, (obj_industrialRenewApplication.GWChargeOrderPaymentCode == null ? "0" : obj_industrialRenewApplication.GWChargeOrderPaymentCode.ToString()));
                //            if (obj_SADOnlinePayment.FinalPaymentStatus != NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //            {
                //                lblMessage.Text = "Please wait for payment is being success";
                //                lblMessage.ForeColor = System.Drawing.Color.Red;
                //                return;
                //            }
                //        }
                //        else
                //        {
                //            lblMessage.Text = "Please make payment for charges";
                //            lblMessage.ForeColor = System.Drawing.Color.Red;
                //            return;
                //        }
                //        if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_industrialRenewApplication.IndustrialRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                //        {

                //            if (obj_industrialRenewApplication.PenaltyOrderPaymentCode != null)
                //            {
                //                obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_industrialRenewApplication.IndustrialRenewApplicationCode, (obj_industrialRenewApplication.PenaltyOrderPaymentCode == null ? "0" : obj_industrialRenewApplication.PenaltyOrderPaymentCode.ToString()));
                //                if (obj_SADOnlinePayment.FinalPaymentStatus != NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                {
                //                    lblMessage.Text = "Please wait for payment is being success";
                //                    lblMessage.ForeColor = System.Drawing.Color.Red;
                //                    return;
                //                }
                //            }
                //            else
                //            {
                //                lblMessage.Text = "Please make payment for penalty";
                //                lblMessage.ForeColor = System.Drawing.Color.Red;
                //                return;
                //            }
                //        }

                //    }
                //    else if (obj_industrialRenewApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.NotDefined)
                //    {
                //        lblMessage.Text = "Please make payment for charges";
                //        lblMessage.ForeColor = System.Drawing.Color.Red;
                //        return;
                //    }
                //    #endregion
                //}

                //#region To Check Bharatkosh attachment
                //else
                //{
                //    if (!(obj_industrialRenewApplication.GetBharatKoshRecieptAttachmentList().Length > 0))
                //    {
                //        lblMessage.Text = "Please attach Bharat kosh receipt for application fee.";
                //        lblMessage.ForeColor = System.Drawing.Color.Red;
                //        return;
                //    }
                //    if (obj_industrialRenewApplication.WaterQualityCode == 1)
                //    {
                //        if (!(obj_industrialRenewApplication.GetAbsRestChargeAttachmentList().Length > 0))
                //        {
                //            lblMessage.Text = "Please attach Bharatkosh reciept(Ground Water Abstraction Charge).";
                //            lblMessage.ForeColor = System.Drawing.Color.Red;
                //            return;
                //        }
                //    }
                //    if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_industrialRenewApplication.IndustrialRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                //    {
                //        if (!(obj_industrialRenewApplication.GetPenaltyAttachmentList().Length > 0))
                //        {
                //            lblMessage.Text = "Please attach Bharatkosh reciept(Penalty).";
                //            lblMessage.ForeColor = System.Drawing.Color.Red;

                //            return;
                //        }
                //    }
                //    if (!(obj_industrialRenewApplication.GetSigneddocAttachmentList().Length > 0))
                //    {
                //        lblMessage.Text = "Please attach Signed Document.";
                //        lblMessage.ForeColor = System.Drawing.Color.Red;

                //        return;
                //    }

                //}
                //#endregion
                try
                {


                    //if (!CheckSignedDocDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text)))
                    //{
                    //    lblMessage.Text = "Please attach Signed Document .";
                    //    lblMessage.ForeColor = System.Drawing.Color.Red;
                    //    return;
                    //}
                    string ErrorMessage = string.Empty;


                    if (DisplayApplicationStop() == 1)
                    {

                        if (obj_industrialRenewApplication.SubmitApplication(out lng_submittedApplicationCode, null, obj_externalUser.ExternalUserCode) == 1)
                        {
                            lngIndSubmitAppCode = lng_submittedApplicationCode;
                            Server.Transfer("IndSubmitSuccess.aspx");
                        }
                        else
                        {
                            strStatus = "Application Submit Failed";
                            lblFinalMsg.Text = obj_industrialRenewApplication.CustumMessage;
                            lblFinalMsg.ForeColor = System.Drawing.Color.Red;
                        }

                    }
                    else
                    { strStatus = "Submission-Presently Closed."; }

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
            // Server.Transfer("Attachment.aspx");
            Server.Transfer("INDRenewOnlinePayment.aspx");
        }
    }
    //private bool CheckSignedDocDetails(long lngA_ApplicationCode)
    //{
    //    try
    //    {
    //        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);
    //        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment[] arr_industrialRenewApplicationAttachmentList;
    //        arr_industrialRenewApplicationAttachmentList = obj_industrialRenewApplication.GetSigneddocAttachmentList();
    //        if (arr_industrialRenewApplicationAttachmentList != null && arr_industrialRenewApplicationAttachmentList.Count() > 0)
    //            return true;
    //        else
    //            return false;

    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //    }
    //}
}