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

public partial class ExternalUser_MiningRenew_Submit : System.Web.UI.Page
{
    string strPageName = "MINRenSubmit";
    string strActionName = "";
    string strStatus = "";
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
        // obj_MiningNewApplication.GWREquiredThroughAbstractStructure
        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            try
            {
                //rngDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
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
                        SourceLabel = (Label)placeHolder.FindControl("lblMiningApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                        }
                        else
                        {
                            SourceLabel = (Label)placeHolder.FindControl("lblApplicationCodeFrom");
                            if (SourceLabel != null)
                            {
                                lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                            }
                            else
                            {
                                SourceLabel = (Label)placeHolder.FindControl("lblApplicationCode");
                                if (SourceLabel != null)
                                {
                                    lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                                }
                            }

                        }
                    }

                    bindDetails();
                    BindSelfDeclarationDetails();
                    DisplayApplicationStop();
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
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewSADApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_miningRenewSADApplication.FirstApplicationCode);

        NOCAP.BLL.Master.ApplicationStop obj_applicationStop = new NOCAP.BLL.Master.ApplicationStop(obj_miningRenewSADApplication.ApplicationTypeCode, obj_miningRenewSADApplication.ApplicationPurposeCode);


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
    private void BindSelfDeclarationDetails()
    {
        try
        {

            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewSADApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));

            if (obj_MiningRenewSADApplication != null)
            {
                

                NOCAP.BLL.Master.FeeRequiredPending objFeeRequiredPending = new NOCAP.BLL.Master.FeeRequiredPending(obj_MiningRenewSADApplication.ApplicationTypeCode, obj_MiningRenewSADApplication.ApplicationPurposeCode);
                if (objFeeRequiredPending != null && objFeeRequiredPending.RequiredPending == NOCAP.BLL.Master.FeeRequiredPending.RequiredOrPending.Required)
                    lblFee.Text = HttpUtility.HtmlEncode("Reciept of Processing Fee of Rs. " + objFeeRequiredPending.Amount + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(objFeeRequiredPending.Amount))) + ") submitted  through NON TAX RECEIPT PORTAL (https://bharatkosh.gov.in) should be attached in online application at prescribed place before submission of application.");
                else
                    lblFee.Text = "Processing Fee : Not Required.";


                NOCAP.BLL.Template.SelfDeclaration.SelfDeclarationTemplate obj_SelfDeclarationTemplate = new NOCAP.BLL.Template.SelfDeclaration.SelfDeclarationTemplate();
                obj_SelfDeclarationTemplate.GetAll();
                NOCAP.BLL.Template.SelfDeclaration.SelfDeclarationTemplate[] arr = obj_SelfDeclarationTemplate.SelfDeclarationTemplateCollection;
                arr = Array.FindAll(arr, a => a.WaterQuantityStart < Convert.ToDecimal(lblWaterReqThroughAbsStruc.Text) + Convert.ToDecimal(lblGWrequiredMiningSeeping.Text == "" ? "0" : lblGWrequiredMiningSeeping.Text) && a.WaterQuantityUpto >= Convert.ToDecimal(lblWaterReqThroughAbsStruc.Text) + Convert.ToDecimal(lblGWrequiredMiningSeeping.Text == "" ? "0" : lblGWrequiredMiningSeeping.Text));


                obj_SelfDeclarationTemplate = arr.SingleOrDefault();
                GeneralConditionCustomEditor.Content = obj_SelfDeclarationTemplate.GeneralConditionTemplateContent;


            }

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

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

                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplicationPrevious = new NOCAP.BLL.Mining.New.Application.MiningNewApplication();
                NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplicationPrevious = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication();

                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = obj_miningRenewApplication.GetFirstMiningApplication();

                // Existing NOC Details
                if (obj_miningRenewApplication.GetPreviousMiningApplication(out obj_miningNewApplicationPrevious, out obj_miningRenewApplicationPrevious) == 1)
                {

                    if (obj_miningNewApplicationPrevious != null)
                    {
                        lblMINExistingNOCNumber.Text = HttpUtility.HtmlEncode(obj_miningNewApplicationPrevious.NOCNumber);
                        NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter obj_miningNewIssusedLetter = new NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter(obj_miningNewApplicationPrevious.ApplicationCode);
                        if (obj_miningNewIssusedLetter.ValidityStartDate != null && obj_miningNewIssusedLetter.ValidityEndDate != null)
                        {
                            lblMINNOCValidity.Text = "(" + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_miningNewIssusedLetter.ValidityStartDate)).ToString("dd/MM/yyyy") + " - " + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_miningNewIssusedLetter.ValidityEndDate)).ToString("dd/MM/yyyy") + ")";
                        }
                    }
                    if (obj_miningRenewApplicationPrevious != null)
                    {
                        lblMINExistingNOCNumber.Text = HttpUtility.HtmlEncode(obj_miningRenewApplicationPrevious.NOCNumber);
                        NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter obj_miningRenewIssusedLetter = new NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter(obj_miningRenewApplicationPrevious.MiningRenewApplicationCode);
                        if (obj_miningRenewIssusedLetter.ValidityStartDate != null && obj_miningRenewIssusedLetter.ValidityEndDate != null)
                        {
                            lblMINNOCValidity.Text = "(" + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_miningRenewIssusedLetter.ValidityStartDate)).ToString("dd/MM/yyyy") + " - " + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_miningRenewIssusedLetter.ValidityEndDate)).ToString("dd/MM/yyyy") + ")";
                        }
                    }
                }
                // End Existing NOC Details


                NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode);
                NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
                NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
                NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);


                lblWaterReqThroughAbsStruc.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_miningRenewApplication.GWREquiredThroughAbstractStructureAdditional == null ? obj_miningRenewApplication.GWREquiredThroughAbstractStructureExisting : obj_miningRenewApplication.GWREquiredThroughAbstractStructureExisting + obj_miningRenewApplication.GWREquiredThroughAbstractStructureAdditional));
                lblGWrequiredMiningSeeping.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_miningRenewApplication.GWRequiredThroughMiningSeepingAdditional == null ? obj_miningRenewApplication.GWRequiredThroughMiningSeepingExisting : obj_miningRenewApplication.GWRequiredThroughMiningSeepingExisting + obj_miningRenewApplication.GWRequiredThroughMiningSeepingAdditional));

                lblNameOfMining.Text = HttpUtility.HtmlEncode(obj_miningNewApplication.NameOfMining);

                lblAppliedForRenewal.Text = HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(obj_miningRenewApplication.LinkDepth));

                if (obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode > 0)
                    lblState.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.GetState().StateName));
                //lblSubDistrict.Text = Convert.ToString(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
                lblAreaType.Text = HttpUtility.HtmlEncode(obj_miningNewApplication.GetApplySubDistrictAreaTypeCategoryHistory().AreaTypeDesc());
                lblAreaTypeCatagory.Text = HttpUtility.HtmlEncode(obj_miningNewApplication.GetApplySubDistrictAreaTypeCategoryHistory().AreaTypeCategoryDesc());

                NOCAP.BLL.Master.SubDistrictAreaTypeCategory obj_presSubDistrictAreaTypeCategory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategory(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_presSubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_presSubDistrictAreaTypeCategory.StateCode, obj_presSubDistrictAreaTypeCategory.DistrictCode, obj_presSubDistrictAreaTypeCategory.SubDistrictCode, obj_presSubDistrictAreaTypeCategory.SubDistrictCurrentAreaCategoryKey);

                lblpresAreaType.Text = HttpUtility.HtmlEncode(obj_presSubDistrictAreaTypeCategoryHistory.AreaTypeDesc());
                lblpresAreaTypeCatagory.Text = HttpUtility.HtmlEncode(obj_presSubDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc());


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
            Server.Transfer("MINRenewOnlinePayment.aspx");
        }
    }
    //private bool CheckSignedDocDetails(long lngA_ApplicationCode)
    //{
    //    try
    //    {
    //        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
    //        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment[] arr_MiningRenewApplicationAttachmentList;
    //        arr_MiningRenewApplicationAttachmentList = obj_MiningRenewApplication.GetSignedDocAttachmentList();
    //        if (arr_MiningRenewApplicationAttachmentList != null && arr_MiningRenewApplicationAttachmentList.Count() > 0)
    //            return true;
    //        else
    //            return false;

    //    }
    //    catch (Exception ex)
    //    {
    //        //  Response.Redirect("~/ExternalErrorPage.aspx", false);
    //        return false;
    //    }
    //}
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
                bool PaymentFlage = false;
                strActionName = "Submit Application";
                long lng_submittedApplicationCode = 0;
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                //NOCAP.BLL.Master.NTRPIntegration obj_NTRPIntegration = new NOCAP.BLL.Master.NTRPIntegration((int)NOCAP.BLL.Common.CommonEnum.ApplicationType.Mining);
                //#region To Check Offline or online payment
                //switch (obj_miningRenewApplication.PaymentTypeMode)
                //{
                //    #region Combined
                //    case NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined:
                //        #region Proccessing Fee
                //        if (obj_miningRenewApplication.ProFeeOrderPaymentCode != null)
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
                //        if (obj_miningRenewApplication.ProFeeOrderPaymentCode != null)
                //            PaymentFlage = true;
                //        else
                //            PaymentFlage = false;
                //        #endregion

                //        #region Charge
                //        if (!PaymentFlage)
                //        {
                //            if (obj_miningRenewApplication.GWChargeOrderPaymentCode != null)
                //                PaymentFlage = true;
                //            else
                //                PaymentFlage = false;
                //        }
                //        #endregion

                //        #region Penalty
                //        if (!PaymentFlage)
                //        {
                //            if (obj_miningRenewApplication.PenaltyOrderPaymentCode != null)
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
                //    if (obj_miningRenewApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined)
                //    {
                //        if (obj_miningRenewApplication.ProFeeOrderPaymentCode != null)
                //        {
                //            obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_miningRenewApplication.MiningRenewApplicationCode, obj_miningRenewApplication.ProFeeOrderPaymentCode.ToString());
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
                //    else if (obj_miningRenewApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single)
                //    {
                //        if (obj_miningRenewApplication.ProFeeOrderPaymentCode != null)
                //        {
                //            obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_miningRenewApplication.MiningRenewApplicationCode, (obj_miningRenewApplication.ProFeeOrderPaymentCode == null ? "0" : obj_miningRenewApplication.ProFeeOrderPaymentCode.ToString()));
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
                //        if (obj_miningRenewApplication.GWChargeOrderPaymentCode != null && obj_miningRenewApplication.WaterQualityCode == 1)
                //        {
                //            obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_miningRenewApplication.MiningRenewApplicationCode, (obj_miningRenewApplication.GWChargeOrderPaymentCode == null ? "0" : obj_miningRenewApplication.GWChargeOrderPaymentCode.ToString()));
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
                //        if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_miningRenewApplication.MiningRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                //        {
                //            if (obj_miningRenewApplication.PenaltyOrderPaymentCode != null)
                //            {
                //                obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_miningRenewApplication.MiningRenewApplicationCode, (obj_miningRenewApplication.PenaltyOrderPaymentCode == null ? "0" : obj_miningRenewApplication.PenaltyOrderPaymentCode.ToString()));
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
                //    else if (obj_miningRenewApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.NotDefined)
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
                //    if (!(obj_miningRenewApplication.GetBharatKoshRecieptAttachmentList().Length > 0))
                //    {
                //        lblMessage.Text = "Please attach Bharat kosh receipt for application fee.";
                //        lblMessage.ForeColor = System.Drawing.Color.Red;
                //        return;
                //    }
                //    if (obj_miningRenewApplication.WaterQualityCode == 1)
                //    {
                //        if (!(obj_miningRenewApplication.GetAbsRestChargeAttachmentList().Length > 0))
                //        {
                //            lblMessage.Text = "Please attach Bharatkosh reciept(Ground Water Abstraction Charge).";
                //            lblMessage.ForeColor = System.Drawing.Color.Red;
                //            return;
                //        }
                //    }
                //    if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_miningRenewApplication.MiningRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                //    {
                //        if (!(obj_miningRenewApplication.GetPenaltyAttachmentList().Length > 0))
                //        {
                //            lblMessage.Text = "Please attach Bharatkosh reciept(Penalty).";
                //            lblMessage.ForeColor = System.Drawing.Color.Red;

                //            return;
                //        }
                //    }
                //    if (!(obj_miningRenewApplication.GetSignedDocAttachmentList().Length > 0))
                //    {
                //        lblMessage.Text = "Please attach Signed Document.";
                //        lblMessage.ForeColor = System.Drawing.Color.Red;

                //        return;
                //    }

                //}
                //#endregion
                try
                {

                    //if (obj_miningRenewApplication.BhaTransRefNoOnline == "")
                    //{


                    //    obj_miningRenewApplication.BharatTransReferanceNumber = Convert.ToInt64(txtBharatKoshRefferenceNo.Text.Trim());
                    //    obj_miningRenewApplication.BharatTransDated = Convert.ToDateTime(txtBharatKoshDated.Text.Trim());

                    //}


                    //if (!CheckSignedDocDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)))
                    //{
                    //    lblMessage.Text = "Please attach Signed Document .";
                    //    lblMessage.ForeColor = System.Drawing.Color.Red;
                    //    return;
                    //}
                    string ErrorMessage = string.Empty;
                    if (DisplayApplicationStop() == 1)
                    {
                        if (obj_miningRenewApplication.SubmitApplication(out lng_submittedApplicationCode, null, obj_externalUser.ExternalUserCode) == 1)
                        {
                            lngMinSubmitAppCode = lng_submittedApplicationCode;
                            Server.Transfer("MinSubmitSuccess.aspx");
                        }
                        else
                        {
                            strStatus = "Application Submit Failed";
                            lblFinalMsg.Text = obj_miningRenewApplication.CustumMessage;
                            lblFinalMsg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else { strStatus = "Submission-Presently Closed."; }

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
                    obj_miningRenewApplication.Dispose();
                }
            }
        }
    }
}