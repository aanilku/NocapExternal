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

public partial class ExternalUser_MiningNew_Submit : System.Web.UI.Page
{
    string strPageName = "MINSubmit";
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
                            if (SourceLabel != null)
                            {
                                lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                            }
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
                    BindSelfDeclarationDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    DisplayApplicationStop();
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    private void BindSelfDeclarationDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);

            NOCAP.BLL.Master.FeeRequiredPending objFeeRequiredPending = new NOCAP.BLL.Master.FeeRequiredPending(obj_MiningNewApplication.ApplicationTypeCode, obj_MiningNewApplication.ApplicationPurposeCode);










            if (objFeeRequiredPending != null && objFeeRequiredPending.RequiredPending == NOCAP.BLL.Master.FeeRequiredPending.RequiredOrPending.Required)
            {
                lblFee.Text = HttpUtility.HtmlEncode("Reciept of Processing Fee of Rs. " + objFeeRequiredPending.Amount + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(objFeeRequiredPending.Amount))) + ") submitted  through NON TAX RECEIPT PORTAL (https://bharatkosh.gov.in) should be attached in online application at prescribed place before submission of application.");

            }
            else
            {
                lblFee.Text = "Processing Fee : Not Required.";
            }






            NOCAP.BLL.Template.SelfDeclaration.SelfDeclarationTemplate obj_SelfDeclarationTemplate = new NOCAP.BLL.Template.SelfDeclaration.SelfDeclarationTemplate();
            obj_SelfDeclarationTemplate.GetAll();
            NOCAP.BLL.Template.SelfDeclaration.SelfDeclarationTemplate[] arr = obj_SelfDeclarationTemplate.SelfDeclarationTemplateCollection;

            switch (obj_MiningNewApplication.MiningNewSADDewatering.WhetherGWTableIntersected)
            {
                case NOCAP.BLL.Mining.New.SADApplication.MiningNewSADDewatering.GWTableIntersected.Yes:
                    arr = Array.FindAll(arr, a => a.WaterQuantityStart < Convert.ToDecimal(lblWaterReqThroughAbsStruc.Text) + Convert.ToDecimal(lblGWrequiredMiningSeeping.Text) && a.WaterQuantityUpto >= Convert.ToDecimal(lblWaterReqThroughAbsStruc.Text) + Convert.ToDecimal(lblGWrequiredMiningSeeping.Text));

                    break;
                case NOCAP.BLL.Mining.New.SADApplication.MiningNewSADDewatering.GWTableIntersected.No:
                    arr = Array.FindAll(arr, a => a.WaterQuantityStart < Convert.ToDecimal(lblWaterReqThroughAbsStruc.Text) && a.WaterQuantityUpto >= Convert.ToDecimal(lblWaterReqThroughAbsStruc.Text));

                    break;
            }

            obj_SelfDeclarationTemplate = arr.SingleOrDefault();
            GeneralConditionCustomEditor.Content = obj_SelfDeclarationTemplate.GeneralConditionTemplateContent;



        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally
        {
        }
    }

    private int DisplayApplicationStop()
    {
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
        NOCAP.BLL.Master.ApplicationStop obj_applicationStop = new NOCAP.BLL.Master.ApplicationStop(obj_miningNewApplication.ApplicationTypeCode, obj_miningNewApplication.ApplicationPurposeCode);
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
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));

                // NOCAP.BLL.Master.ApplicationTypeCategory obj_ApplicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_industrialNewApplication.ApplicationTypeCode, obj_industrialNewApplication.ApplicationTypeCategoryCode);
                NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode);
                NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
                NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
                NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);


                lblWaterReqThroughAbsStruc.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_miningNewApplication.GWREquiredThroughAbstractStructure));
                lblGWrequiredMiningSeeping.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_miningNewApplication.GWRequiredThroughMiningSeeping));
                lblNameOfIndustry.Text = HttpUtility.HtmlEncode(obj_miningNewApplication.NameOfMining);
                if (obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode > 0)
                    lblState.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.GetState().StateName));
                //lblSubDistrict.Text = Convert.ToString(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
                // lblAreaType.Text = HttpUtility.HtmlEncode(obj_miningNewApplication.GetApplySubDistrictAreaTypeCategoryHistory().AreaTypeDesc());
                lblAreaTypeCatagory.Text = HttpUtility.HtmlEncode(obj_miningNewApplication.GetApplySubDistrictAreaTypeCategoryHistory().AreaTypeCategoryDesc());
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
            //Server.Transfer("Attachment.aspx");
            Server.Transfer("MINNewOnlinePayment.aspx");
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

                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;

                strActionName = "Submit Application";
                long lng_submittedApplicationCode = 0;
                bool PaymentFlage = false;
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                //NOCAP.BLL.Master.NTRPIntegration obj_NTRPIntegration = new NOCAP.BLL.Master.NTRPIntegration((int)NOCAP.BLL.Common.CommonEnum.ApplicationType.Mining);

                //#region To Check Offline or online payment
                //switch (obj_miningNewApplication.PaymentTypeMode)
                //{
                //    #region Combined
                //    case NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined:
                //        #region Proccessing Fee
                //        if (obj_miningNewApplication.ProFeeOrderPaymentCode != null)
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
                //        if (obj_miningNewApplication.ProFeeOrderPaymentCode != null)
                //            PaymentFlage = true;
                //        else
                //            PaymentFlage = false;
                //        #endregion

                //        #region Charge
                //        if (!PaymentFlage)
                //        {
                //            if (obj_miningNewApplication.GWChargeOrderPaymentCode != null)
                //                PaymentFlage = true;
                //            else
                //                PaymentFlage = false;
                //        }
                //        #endregion

                //        #region Penalty
                //        if (!PaymentFlage)
                //        {
                //            if (obj_miningNewApplication.PenaltyOrderPaymentCode != null)
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
                //    if (obj_miningNewApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined)
                //    {
                //        if (obj_miningNewApplication.ProFeeOrderPaymentCode != null)
                //        {
                //            obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_miningNewApplication.ApplicationCode, obj_miningNewApplication.ProFeeOrderPaymentCode.ToString());
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
                //    else if (obj_miningNewApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single)
                //    {
                //        if (obj_miningNewApplication.ProFeeOrderPaymentCode != null)
                //        {
                //            obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_miningNewApplication.ApplicationCode, (obj_miningNewApplication.ProFeeOrderPaymentCode == null ? "0" : obj_miningNewApplication.ProFeeOrderPaymentCode.ToString()));
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
                //        if (obj_miningNewApplication.GWChargeOrderPaymentCode != null && obj_miningNewApplication.WaterQualityCode == 1)
                //        {
                //            obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_miningNewApplication.ApplicationCode, (obj_miningNewApplication.GWChargeOrderPaymentCode == null ? "0" : obj_miningNewApplication.GWChargeOrderPaymentCode.ToString()));
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
                //        if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_miningNewApplication.ApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                //        {
                //            if (obj_miningNewApplication.PenaltyOrderPaymentCode != null)
                //            {
                //                obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_miningNewApplication.ApplicationCode, (obj_miningNewApplication.PenaltyOrderPaymentCode == null ? "0" : obj_miningNewApplication.PenaltyOrderPaymentCode.ToString()));
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
                //    else if (obj_miningNewApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.NotDefined)
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
                //    if (!(obj_miningNewApplication.GetBharatKoshRecieptAttachmentList().Length > 0))
                //    {
                //        lblMessage.Text = "Please attach Bharat kosh receipt for application fee.";
                //        lblMessage.ForeColor = System.Drawing.Color.Red;
                //        return;
                //    }
                //    if (obj_miningNewApplication.WaterQualityCode == 1)
                //    {
                //        if (!(obj_miningNewApplication.GetAbsRestChargeAttachmentList().Length > 0))
                //        {
                //            lblMessage.Text = "Please attach Bharatkosh reciept(Ground Water Abstraction Charge).";
                //            lblMessage.ForeColor = System.Drawing.Color.Red;
                //            return;
                //        }
                //    }
                //    //switch (obj_miningNewApplication.GroundWaterUtilizationFor)
                //    //{
                //    //    case NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry:
                //    //        NOCAP.BLL.Master.Penalty obj_Penalty = new NOCAP.BLL.Master.Penalty(17);
                //    //        switch (obj_miningNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND)
                //    //        {
                //    //            case NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No:
                //    //                if (!(obj_miningNewApplication.GetPenaltyAttachmentList().Length > 0))
                //    //                {
                //    //                    lblMessage.Text = "Please attach Bharatkosh reciept(Penalty).";
                //    //                    lblMessage.ForeColor = System.Drawing.Color.Red;

                //    //                    return;
                //    //                }
                //    //                break;
                //    //        }
                //    //        break;
                //    //}
                    
                //    if (!(obj_miningNewApplication.GetSignedDocAttachmentList().Length > 0))
                //    {
                //        lblMessage.Text = "Please attach Signed Document.";
                //        lblMessage.ForeColor = System.Drawing.Color.Red;

                //        return;
                //    }
                //}
                //#endregion
                try
                {


                    //if (!CheckSignedDocDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)))
                    //{
                    //    lblMessage.Text = "Please attach Signed Document .";
                    //    lblMessage.ForeColor = System.Drawing.Color.Red;
                    //    return;
                    //}
                    string ErrorMessage = string.Empty;
                    if (DisplayApplicationStop() == 1)
                    {

                        if (obj_miningNewApplication.SubmitApplication(out lng_submittedApplicationCode, null, obj_externalUser.ExternalUserCode) == 1)
                        {
                            lngMinSubmitAppCode = lng_submittedApplicationCode;
                            Server.Transfer("MinSubmitSuccess.aspx");
                        }
                        else
                        {
                            strStatus = "Application Submit Failed";
                            lblFinalMsg.Text = obj_miningNewApplication.CustumMessage;
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
                    //lblFinalMsg.Text = ex.Message;
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
                    obj_miningNewApplication.Dispose();
                }
            }
        }
    }
    //private bool CheckSignedDocDetails(long lngA_ApplicationCode)
    //{
    //    try
    //    {
    //        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
    //        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment[] arr_MiningNewApplicationAttachmentList;
    //        arr_MiningNewApplicationAttachmentList = obj_MiningNewApplication.GetSignedDocAttachmentList();
    //        if (arr_MiningNewApplicationAttachmentList != null && arr_MiningNewApplicationAttachmentList.Count() > 0)
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
}