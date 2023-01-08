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

public partial class ExternalUser_InfrastructureRenew_Submit : System.Web.UI.Page
{
    string strPageName = "INFRenewSubmit";
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
                        SourceLabel = (Label)placeHolder.FindControl("lblInfrastructureApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                        }
                        else
                        {
                            SourceLabel = (Label)placeHolder.FindControl("lblApplicationCodeFrom");
                            if (SourceLabel != null)
                            {
                                lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                            }
                            else
                            {
                                SourceLabel = (Label)placeHolder.FindControl("lblApplicationCode");
                                if (SourceLabel != null)
                                {
                                    lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                                }
                            }

                        }
                    }
                }
                bindDetails();
                BindSelfDeclarationDetails();
                DisplayApplicationStop();

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    private int DisplayApplicationStop()
    {
        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_infrastructureRenewSADApplication.FirstApplicationCode);

        NOCAP.BLL.Master.ApplicationStop obj_applicationStop = new NOCAP.BLL.Master.ApplicationStop(obj_infrastructureRenewSADApplication.ApplicationTypeCode, obj_infrastructureRenewSADApplication.ApplicationPurposeCode);
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

            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewSADApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
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

                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplicationPrevious = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
                NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplicationPrevious = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication();


                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewSADApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
                obj_InfrastructureNewApplication = obj_InfrastructureRenewSADApplication.GetFirstInfrastructureApplication();

                obj_InfrastructureRenewSADApplication.GetPreviousInfrastructureApplication(out obj_infrastructureNewApplicationPrevious, out obj_infrastructureRenewApplicationPrevious);

                if (obj_infrastructureNewApplicationPrevious != null)
                {
                    // Existing NOC Details

                    lblINFExistingNOCNumber.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplicationPrevious.NOCNumber);

                    NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_infrastructureNewIssusedLetter = new NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter(obj_infrastructureNewApplicationPrevious.ApplicationCode);
                    if (obj_infrastructureNewIssusedLetter.ValidityStartDate != null && obj_infrastructureNewIssusedLetter.ValidityEndDate != null)
                    {
                        lblINFNOCValidity.Text = HttpUtility.HtmlEncode("(" + Convert.ToDateTime(obj_infrastructureNewIssusedLetter.ValidityStartDate).ToString("dd/MM/yyyy") + " - " + Convert.ToDateTime(obj_infrastructureNewIssusedLetter.ValidityEndDate).ToString("dd/MM/yyyy") + ")");
                    }
                    // End Existing NOC Details
                }

                if (obj_infrastructureRenewApplicationPrevious != null)
                {
                    lblINFExistingNOCNumber.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewApplicationPrevious.NOCNumber);

                    NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter obj_infrastructureRenewIssusedLetter = new NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter(obj_infrastructureRenewApplicationPrevious.InfrastructureRenewApplicationCode);
                    if (obj_infrastructureRenewIssusedLetter.ValidityStartDate != null && obj_infrastructureRenewIssusedLetter.ValidityEndDate != null)
                    {
                        lblINFNOCValidity.Text = "(" + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_infrastructureRenewIssusedLetter.ValidityStartDate)).ToString("dd/MM/yyyy") + " - " + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_infrastructureRenewIssusedLetter.ValidityEndDate)).ToString("dd/MM/yyyy") + ")";
                    }
                }

                NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode);
                NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
                NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
                NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);


                lblNetGroundWaterRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString((obj_InfrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist==null?0: obj_InfrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist) + (obj_InfrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional==null?0: obj_InfrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional)));
                lblNameOfInfrastructure.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewSADApplication.NameOfInfrastructure);

                lblAppliedForRenewal.Text = HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(obj_InfrastructureRenewSADApplication.LinkDepth));


                if (obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode > 0)
                    lblState.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.GetState().StateName));
                //lblSubDistrict.Text = Convert.ToString(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
                lblAreaType.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.GetApplySubDistrictAreaTypeCategoryHistory().AreaTypeDesc());
                lblAreaTypeCatagory.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.GetApplySubDistrictAreaTypeCategoryHistory().AreaTypeCategoryDesc());

                NOCAP.BLL.Master.SubDistrictAreaTypeCategory obj_presSubDistrictAreaTypeCategory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategory(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
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
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                //NOCAP.BLL.Master.NTRPIntegration obj_NTRPIntegration = new NOCAP.BLL.Master.NTRPIntegration((int)NOCAP.BLL.Common.CommonEnum.ApplicationType.Infrastructure);
                //#region To Check Offline or online payment
                //switch (obj_InfrastructureRenewApplication.PaymentTypeMode)
                //{
                //    #region Combined
                //    case NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined:
                //        #region Proccessing Fee
                //        if (obj_InfrastructureRenewApplication.ProFeeOrderPaymentCode != null)
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
                //        if (obj_InfrastructureRenewApplication.ProFeeOrderPaymentCode != null)
                //            PaymentFlage = true;
                //        else
                //            PaymentFlage = false;
                //        #endregion

                //        #region Charge
                //        if (!PaymentFlage)
                //        {
                //            if (obj_InfrastructureRenewApplication.GWChargeOrderPaymentCode != null)
                //                PaymentFlage = true;
                //            else
                //                PaymentFlage = false;
                //        }
                //        #endregion

                //        #region Penalty
                //        if (!PaymentFlage)
                //        {
                //            if (obj_InfrastructureRenewApplication.PenaltyOrderPaymentCode != null)
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

                //    if (obj_InfrastructureRenewApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined)
                //    {
                //        if (obj_InfrastructureRenewApplication.ProFeeOrderPaymentCode != null)
                //        {
                //            obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode, obj_InfrastructureRenewApplication.ProFeeOrderPaymentCode.ToString());
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
                //    else if (obj_InfrastructureRenewApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single)
                //    {
                //        if (obj_InfrastructureRenewApplication.ProFeeOrderPaymentCode != null)
                //        {
                //            obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode, (obj_InfrastructureRenewApplication.ProFeeOrderPaymentCode == null ? "0" : obj_InfrastructureRenewApplication.ProFeeOrderPaymentCode.ToString()));
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
                //        if (obj_InfrastructureRenewApplication.GWChargeOrderPaymentCode != null && obj_InfrastructureRenewApplication.WaterQualityCode == 1)
                //        {
                //            obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode, (obj_InfrastructureRenewApplication.GWChargeOrderPaymentCode == null ? "0" : obj_InfrastructureRenewApplication.GWChargeOrderPaymentCode.ToString()));
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
                //        if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                //        {

                //            if (obj_InfrastructureRenewApplication.PenaltyOrderPaymentCode != null)
                //            {
                //                obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode, (obj_InfrastructureRenewApplication.PenaltyOrderPaymentCode == null ? "0" : obj_InfrastructureRenewApplication.PenaltyOrderPaymentCode.ToString()));
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
                //    else if (obj_InfrastructureRenewApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.NotDefined)
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
                //    if (!(obj_InfrastructureRenewApplication.GetBharatKoshRecieptAttachmentList().Length > 0))
                //    {
                //        lblMessage.Text = "Please attach Bharat kosh receipt for application fee.";
                //        lblMessage.ForeColor = System.Drawing.Color.Red;
                //        return;
                //    }
                //    if (obj_InfrastructureRenewApplication.WaterQualityCode == 1)
                //    {
                //        if (!(obj_InfrastructureRenewApplication.GetAbsRestChargeAttachmentList().Length > 0))
                //        {
                //            lblMessage.Text = "Please attach Bharatkosh reciept(Ground Water Abstraction Charge).";
                //            lblMessage.ForeColor = System.Drawing.Color.Red;
                //            return;
                //        }
                //    }
                //    if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                //    {
                //        if (!(obj_InfrastructureRenewApplication.GetPenaltyAttachmentList().Length > 0))
                //        {
                //            lblMessage.Text = "Please attach Bharatkosh reciept(Penalty).";
                //            lblMessage.ForeColor = System.Drawing.Color.Red;

                //            return;
                //        }
                //    }
                //    if (!(obj_InfrastructureRenewApplication.GetSignedDocAttachmentList().Length > 0))
                //    {
                //        lblMessage.Text = "Please attach Signed Document.";
                //        lblMessage.ForeColor = System.Drawing.Color.Red;

                //        return;
                //    }

                //}
                //#endregion
                try
                {
                    //if (!CheckSignedDocDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text)))
                    //{
                    //    lblMessage.Text = "Please attach Signed Document .";
                    //    lblMessage.ForeColor = System.Drawing.Color.Red;
                    //    return;
                    //}
                    string ErrorMessage = string.Empty;

                    if (DisplayApplicationStop() == 1)
                    {

                        if (obj_InfrastructureRenewApplication.SubmitApplication(out lng_submittedApplicationCode, null, obj_externalUser.ExternalUserCode) == 1)
                        {
                            lngInfSubmitAppCode = lng_submittedApplicationCode;
                            Server.Transfer("InfSubmitSuccess.aspx");
                        }
                        else
                        {
                            strStatus = "Application Submit Failed";
                            lblFinalMsg.Text = obj_InfrastructureRenewApplication.CustumMessage;
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
                    obj_InfrastructureRenewApplication.Dispose();
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
            Server.Transfer("INFRenewOnlinePayment.aspx");
        }
    }
    //private bool CheckSignedDocDetails(long lngA_ApplicationCode)
    //{
    //    try
    //    {
    //        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);
    //        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment[] arr_infrastructureRenewApplicationAttachmentList;
    //        arr_infrastructureRenewApplicationAttachmentList = obj_infrastructureRenewApplication.GetSignedDocAttachmentList();
    //        if (arr_infrastructureRenewApplicationAttachmentList != null && arr_infrastructureRenewApplicationAttachmentList.Count() > 0)
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