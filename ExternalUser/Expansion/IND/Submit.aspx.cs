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

using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Web.Http;
using System.Net;
using System.Text;

public partial class ExternalUser_Expansion_IND_Submit : System.Web.UI.Page
{
    string strPageName = "INDSubmit";
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
                            if (SourceLabel != null)
                            {
                                lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                            }
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
                    }

                }

                bindDetails();
                BindSelfDeclarationDetails();
                DisplayApplicationStop();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    private void BindSelfDeclarationDetails()
    {
        try
        {

            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
            //if (obj_industrialNewApplication.BhaTransRefNoOnline != "")
            //{

            //    tblOfflinePayment.Visible = false;
            //    tblOnlinePayment.Visible = true;

            //    lblTransactionNo.Text = obj_industrialNewApplication.BhaTransRefNoOnline;
            //    lblTransactionDate.Text = Convert.ToDateTime(obj_industrialNewApplication.BhaTransDatedOnline).ToString("dd/MM/yyyy");

            //}
            //else
            //{
            //    tblOfflinePayment.Visible = true;
            //    tblOnlinePayment.Visible = false;
            //    // if (obj_industrialNewApplication.BharatTransReferanceNumber == null)
            //    txtBharatKoshRefferenceNo.Text = "";
            //    // else
            //    //   txtBharatKoshRefferenceNo.Text = obj_industrialNewApplication.BharatTransReferanceNumber.ToString();
            //    // if (obj_industrialNewApplication.BharatTransDated == null)
            //    txtBharatKoshDated.Text = "";
            //    // else
            //    // txtBharatKoshDated.Text = Convert.ToDateTime(obj_industrialNewApplication.BharatTransDated).ToString("dd/MM/yyyy");

            //}


            NOCAP.BLL.Master.FeeRequiredPending objFeeRequiredPending = new NOCAP.BLL.Master.FeeRequiredPending(obj_industrialNewApplication.ApplicationTypeCode, obj_industrialNewApplication.ApplicationPurposeCode);
            //if (objFeeRequiredPending != null && objFeeRequiredPending.RequiredPending == NOCAP.BLL.Master.FeeRequiredPending.RequiredOrPending.Required)
            //    lblFee.Text = HttpUtility.HtmlEncode("Reciept of Processing Fee of Rs. " + objFeeRequiredPending.Amount + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(objFeeRequiredPending.Amount))) + ") submitted  through NON TAX RECEIPT PORTAL (https://bharatkosh.gov.in) should be attached in online application at prescribed place before submission of application.");
            //else
            //    lblFee.Text = "Processing Fee : Not Required.";
            NOCAP.BLL.Template.SelfDeclaration.SelfDeclarationTemplate obj_SelfDeclarationTemplate = new NOCAP.BLL.Template.SelfDeclaration.SelfDeclarationTemplate();
            obj_SelfDeclarationTemplate.GetAll();
            NOCAP.BLL.Template.SelfDeclaration.SelfDeclarationTemplate[] arr = obj_SelfDeclarationTemplate.SelfDeclarationTemplateCollection;
            arr = Array.FindAll(arr, a => a.WaterQuantityStart < Convert.ToDecimal(lblNetGroundWaterRequirement.Text) && a.WaterQuantityUpto >= Convert.ToDecimal(lblNetGroundWaterRequirement.Text));


            obj_SelfDeclarationTemplate = arr.SingleOrDefault();
            GeneralConditionCustomEditor.Content = obj_SelfDeclarationTemplate.GeneralConditionTemplateContent;


        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    private int DisplayApplicationStop()
    {
        NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
        NOCAP.BLL.Master.ApplicationStop obj_applicationStop = new NOCAP.BLL.Master.ApplicationStop(obj_industrialNewApplication.ApplicationTypeCode, obj_industrialNewApplication.ApplicationPurposeCode);
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
                NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

                // NOCAP.BLL.Master.ApplicationTypeCategory obj_ApplicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_industrialNewApplication.ApplicationTypeCode, obj_industrialNewApplication.ApplicationTypeCategoryCode);
                NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode);
                NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
                NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
                NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);


              
                lblNetGroundWaterRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString((obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement) + (obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist)));
                lblNameOfIndustry.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.NameOfIndustry);
                if (obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode > 0)
                    lblState.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().StateName));
                //lblSubDistrict.Text = Convert.ToString(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
                switch (obj_industrialNewApplication.MSME)
                {
                    case NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication.MSMEYesNo.Yes:
                        lblMSME.Text = "Yes";
                        break;
                    case NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication.MSMEYesNo.No:
                        lblMSME.Text = "No";
                        break;
                    case NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication.MSMEYesNo.NotDefine:
                        lblMSME.Text = "Not Define";
                        break;
                    default:
                        break;
                }
                switch (obj_industrialNewApplication.WetLandArea)
                {
                    case NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication.WetLandAreaYesNo.Yes:
                        lblWetlandarea.Text = "Yes";
                        break;
                    case NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication.WetLandAreaYesNo.No:
                        lblWetlandarea.Text = "No";
                        break;
                    case NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication.WetLandAreaYesNo.NotDefine:
                        lblWetlandarea.Text = "Not Define";
                        break;
                    default:
                        break;
                }

                lblAreaTypeCatagory.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.GetApplySubDistrictAreaTypeCategoryHistory().AreaTypeCategoryDesc());
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
                bool PaymentFlage = false;
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "Submit Application";
                long lng_submittedApplicationCode = 0;
                NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));


                //NOCAP.BLL.Master.NTRPIntegration obj_NTRPIntegration = new NOCAP.BLL.Master.NTRPIntegration((int)NOCAP.BLL.Common.CommonEnum.ApplicationType.Industrial);
                //#region To Check Offline or online payment
                //switch (obj_industrialNewApplication.PaymentTypeMode)
                //{
                //    #region Combined
                //    case NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined:
                //        #region Proccessing Fee
                //        if (obj_industrialNewApplication.ProFeeOrderPaymentCode != null)
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
                //        if (obj_industrialNewApplication.ProFeeOrderPaymentCode != null)                        
                //            PaymentFlage = true;                        
                //        else
                //            PaymentFlage = false;
                //        #endregion

                //        #region Charge
                //        if (!PaymentFlage)
                //        {
                //            if (obj_industrialNewApplication.GWChargeOrderPaymentCode != null)
                //                PaymentFlage = true;
                //            else
                //                PaymentFlage = false;
                //        }
                //        #endregion

                //        #region Penalty
                //        if (!PaymentFlage)
                //        {
                //            if (obj_industrialNewApplication.PenaltyOrderPaymentCode != null)
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
                //    if (obj_industrialNewApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined)
                //    {
                //        if (obj_industrialNewApplication.ProFeeOrderPaymentCode != null)
                //        {
                //            obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_industrialNewApplication.IndustrialNewApplicationCode, obj_industrialNewApplication.ProFeeOrderPaymentCode.ToString());
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
                //    else if (obj_industrialNewApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single)
                //    {
                //        if (obj_industrialNewApplication.ProFeeOrderPaymentCode != null)
                //        {
                //            obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_industrialNewApplication.IndustrialNewApplicationCode, (obj_industrialNewApplication.ProFeeOrderPaymentCode == null ? "0" : obj_industrialNewApplication.ProFeeOrderPaymentCode.ToString()));
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
                //        if (obj_industrialNewApplication.GWChargeOrderPaymentCode != null && obj_industrialNewApplication.WaterQualityCode == 1)
                //        {
                //            obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_industrialNewApplication.IndustrialNewApplicationCode, (obj_industrialNewApplication.GWChargeOrderPaymentCode == null ? "0" : obj_industrialNewApplication.GWChargeOrderPaymentCode.ToString()));
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
                //        if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_industrialNewApplication.IndustrialNewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                //        {

                //            if (obj_industrialNewApplication.PenaltyOrderPaymentCode != null)
                //            {
                //                obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_industrialNewApplication.IndustrialNewApplicationCode, (obj_industrialNewApplication.PenaltyOrderPaymentCode == null ? "0" : obj_industrialNewApplication.PenaltyOrderPaymentCode.ToString()));
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
                //    else if (obj_industrialNewApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.NotDefined)
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
                //    if(!(obj_industrialNewApplication.GetBharatKoshRecieptAttachmentList().Length>0))
                //    {
                //        lblMessage.Text = "Please attach Bharat kosh receipt for application fee.";
                //        lblMessage.ForeColor = System.Drawing.Color.Red;
                //        return;
                //    }
                //    if (obj_industrialNewApplication.WaterQualityCode == 1)
                //    {
                //        if (!(obj_industrialNewApplication.GetAbsRestChargeAttachmentList().Length > 0))
                //        {
                //            lblMessage.Text = "Please attach Bharatkosh reciept(Ground Water Charge).";
                //            lblMessage.ForeColor = System.Drawing.Color.Red;
                //            return;
                //        }
                //    }
                //    //switch (obj_industrialNewApplication.GroundWaterUtilizationFor)
                //    //{
                //    //    case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry:
                //    //        NOCAP.BLL.Master.Penalty obj_Penalty = new NOCAP.BLL.Master.Penalty(17);
                //    //        switch (obj_industrialNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND)
                //    //        {
                //    //            case NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No:

                //    //                if (!(obj_industrialNewApplication.GetPenaltyAttachmentList().Length > 0))
                //    //                {
                //    //                    lblMessage.Text = "Please attach Bharatkosh reciept(Penalty).";
                //    //                    lblMessage.ForeColor = System.Drawing.Color.Red;
                //    //                    return;
                //    //                }
                //    //                break;
                //    //        }
                //    //        break;
                //    //}                    
                //    if (!(obj_industrialNewApplication.GetSignedDocAttachmentList().Length > 0))
                //    {
                //        lblMessage.Text = "Please attach Signed Document.";
                //        lblMessage.ForeColor = System.Drawing.Color.Red;

                //        return;
                //    }

                //}
                //#endregion
                try
                {
                    string ErrorMessage = string.Empty;

                    if (DisplayApplicationStop() == 1)
                    {
                        obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                        if (obj_industrialNewApplication.SubmitApplication(null, obj_externalUser.ExternalUserCode) == 1)
                        {
                            lngIndSubmitAppCode = lng_submittedApplicationCode;
                            Server.Transfer("IndSubmitSuccess.aspx");
                        }
                        else
                        {
                            strStatus = "Application Submit Failed";
                            //--Response.Write(obj_industrialNewApplication.CustumMessage);
                            lblFinalMsg.Text = obj_industrialNewApplication.CustumMessage;
                            lblFinalMsg.ForeColor = System.Drawing.Color.Red;
                        }

                    }
                    else
                    { strStatus = "Submission-Presently Closed."; }
                    //}
                    //else
                    // {
                    // lblMessage.Text = obj_industrialNewApplication.CustumMessage;
                    //lblMessage.ForeColor = System.Drawing.Color.Red;
                    // }



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
                    obj_industrialNewApplication.Dispose();
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
            Server.Transfer("Attachment.aspx");
        }
    }
    //private bool CheckSignedDocDetails(long lngA_ApplicationCode)
    //{
    //    try
    //    {
    //        NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(lngA_ApplicationCode);
    //        NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
    //        arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetSignedDocAttachmentList();
    //        if (arr_industrialNewApplicationAttachmentList != null && arr_industrialNewApplicationAttachmentList.Count() > 0)
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