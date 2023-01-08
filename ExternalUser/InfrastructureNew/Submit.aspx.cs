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

public partial class ExternalUser_InfrastructureNew_Submit : System.Web.UI.Page
{
    string strPageName = "INFSubmit";
    string strActionName = "";
    string strStatus = "";
    string category = "";
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
            Server.Transfer("INFNewOnlinePayment.aspx");
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
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                //NOCAP.BLL.Master.NTRPIntegration obj_NTRPIntegration = new NOCAP.BLL.Master.NTRPIntegration((int)NOCAP.BLL.Common.CommonEnum.ApplicationType.Infrastructure);
               
                //#region To Check Offline or online payment
                //switch (obj_infrastructureNewApplication.PaymentTypeMode)
                //{
                //    #region Combined
                //    case NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined:
                //        #region Proccessing Fee
                //        if (obj_infrastructureNewApplication.ProFeeOrderPaymentCode != null)
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
                //        if (obj_infrastructureNewApplication.ProFeeOrderPaymentCode != null)
                //            PaymentFlage = true;
                //        else
                //            PaymentFlage = false;
                //        #endregion

                //        #region Charge
                //        if (!PaymentFlage)
                //        {
                //            if (obj_infrastructureNewApplication.GWChargeOrderPaymentCode != null)
                //                PaymentFlage = true;
                //            else
                //                PaymentFlage = false;
                //        }
                //        #endregion

                //        #region Penalty
                //        if (!PaymentFlage)
                //        {
                //            if (obj_infrastructureNewApplication.PenaltyOrderPaymentCode != null)
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

                //    if (obj_infrastructureNewApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined)
                //    {
                //        if (obj_infrastructureNewApplication.ProFeeOrderPaymentCode != null)
                //        {
                //            obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_infrastructureNewApplication.InfrastructureNewApplicationCode, obj_infrastructureNewApplication.ProFeeOrderPaymentCode.ToString());
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
                //    else if (obj_infrastructureNewApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single)
                //    {
                //        if (obj_infrastructureNewApplication.ProFeeOrderPaymentCode != null)
                //        {
                //            obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_infrastructureNewApplication.InfrastructureNewApplicationCode, (obj_infrastructureNewApplication.ProFeeOrderPaymentCode == null ? "0" : obj_infrastructureNewApplication.ProFeeOrderPaymentCode.ToString()));
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
                //        if (obj_infrastructureNewApplication.GWChargeOrderPaymentCode != null && obj_infrastructureNewApplication.WaterQualityCode == 1)
                //        {
                //            obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_infrastructureNewApplication.InfrastructureNewApplicationCode, (obj_infrastructureNewApplication.GWChargeOrderPaymentCode == null ? "0" : obj_infrastructureNewApplication.GWChargeOrderPaymentCode.ToString()));
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
                //        if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_infrastructureNewApplication.InfrastructureNewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                //        {

                //            if (obj_infrastructureNewApplication.PenaltyOrderPaymentCode != null)
                //            {
                //                obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_infrastructureNewApplication.InfrastructureNewApplicationCode, (obj_infrastructureNewApplication.PenaltyOrderPaymentCode == null ? "0" : obj_infrastructureNewApplication.PenaltyOrderPaymentCode.ToString()));
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
                //    else if (obj_infrastructureNewApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.NotDefined)
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
                //    if (!(obj_infrastructureNewApplication.GetBharatKoshRecieptAttachmentList().Length > 0))
                //    {
                //        lblMessage.Text = "Please attach Bharat kosh receipt for application fee.";
                //        lblMessage.ForeColor = System.Drawing.Color.Red;
                //        return;
                //    }
                //    if (obj_infrastructureNewApplication.WaterQualityCode == 1)
                //    {
                //        if (!(obj_infrastructureNewApplication.GetAbsRestChargeAttachmentList().Length > 0))
                //        {
                //            lblMessage.Text = "Please attach Bharatkosh reciept(Ground Water Abstraction Charge).";
                //            lblMessage.ForeColor = System.Drawing.Color.Red;
                //            return;
                //        }
                //    }
                //    //switch (obj_infrastructureNewApplication.GroundWaterUtilizationFor)
                //    //{
                //    //    case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry:
                //    //        NOCAP.BLL.Master.Penalty obj_Penalty = new NOCAP.BLL.Master.Penalty(17);
                //    //        switch (obj_infrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND)
                //    //        {
                //    //            case NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No:

                //    //                if (!(obj_infrastructureNewApplication.GetPenaltyAttachmentList().Length > 0))
                //    //                {
                //    //                    lblMessage.Text = "Please attach Bharatkosh reciept(Penalty).";
                //    //                    lblMessage.ForeColor = System.Drawing.Color.Red;
                //    //                    return;
                //    //                }
                //    //                break;
                //    //        }
                //    //        break;
                //    //}
                   
                //    if (!(obj_infrastructureNewApplication.GetSignedDocAttachmentList().Length > 0))
                //    {
                //        lblMessage.Text = "Please attach Signed Document.";
                //        lblMessage.ForeColor = System.Drawing.Color.Red;

                //        return;
                //    }

                //}
                //#endregion
                try
                {
                    category = Convert.ToString(new NOCAP.BLL.Master.ApplicationTypeCategory((int)obj_infrastructureNewApplication.ApplicationTypeCode, (int)obj_infrastructureNewApplication.ApplicationTypeCategoryCode).ApplicationTypeCategoryDesc);
                    if (category == "Government Water Supply Agencies")

                    {
                        if (obj_infrastructureNewApplication.NoOfPopulation == null)
                        {
                            lblMessage.Text = "Please fill Populations Benefited";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                    }


                    //if (!CheckSignedDocDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text)))
                    //{
                    //    lblMessage.Text = "Please attach Signed Document .";
                    //    lblMessage.ForeColor = System.Drawing.Color.Red;
                    //    return;
                    //}

                    string ErrorMessage = string.Empty;
                    if (DisplayApplicationStop() == 1)
                    {

                        if (obj_infrastructureNewApplication.SubmitApplication(out lng_submittedApplicationCode, null, obj_externalUser.ExternalUserCode) == 1)
                        {
                            lngInfSubmitAppCode = lng_submittedApplicationCode;
                            Server.Transfer("InfSubmitSuccess.aspx");
                        }
                        else
                        {
                            strStatus = "Application Submit Failed";
                            //--Response.Write(obj_industrialNewApplication.CustumMessage);
                            lblFinalMsg.Text = obj_infrastructureNewApplication.CustumMessage;
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
                    obj_infrastructureNewApplication.Dispose();
                }
            }
        }
    }

    #region Private
    //private bool CheckSignedDocDetails(long lngA_ApplicationCode)
    //{
    //    try
    //    {
    //        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
    //        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment[] arr_infrastructureNewApplicationAttachmentList;
    //        arr_infrastructureNewApplicationAttachmentList = obj_infrastructureNewApplication.GetSignedDocAttachmentList();
    //        if (arr_infrastructureNewApplicationAttachmentList != null && arr_infrastructureNewApplicationAttachmentList.Count() > 0)
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
    private int DisplayApplicationStop()
    {
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
        NOCAP.BLL.Master.ApplicationStop obj_applicationStop = new NOCAP.BLL.Master.ApplicationStop(obj_infrastructureNewApplication.ApplicationTypeCode, obj_infrastructureNewApplication.ApplicationPurposeCode);
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
    private void bindDetails()
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
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

                // NOCAP.BLL.Master.ApplicationTypeCategory obj_ApplicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_industrialNewApplication.ApplicationTypeCode, obj_industrialNewApplication.ApplicationTypeCategoryCode);
                NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode);
                NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
                NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
                NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);


                lblNetGroundWaterRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString((obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement) + (obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist)));
                lblNameOfIndustry.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.NameOfInfrastructure);
                if (obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode > 0)
                    lblState.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.GetState().StateName));
                //lblSubDistrict.Text = Convert.ToString(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);

                //category = Convert.ToString(new NOCAP.BLL.Master.ApplicationTypeCategory((int)obj_infrastructureNewApplication.ApplicationTypeCode, (int)obj_infrastructureNewApplication.ApplicationTypeCategoryCode).ApplicationTypeCategoryDesc);
                //if (category == "Residential apartment" || category == "Group housing" || category == "Government water Supply agencies")
                //{
                //trAreaType.Visible = false;
                if (obj_infrastructureNewApplication.DrinkingUse == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.UseOption.Yes)
                {
                    chkGWUtilizationUse.Items[0].Selected = true;
                }
                if (obj_infrastructureNewApplication.ConstructionUse == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.UseOption.Yes)
                {
                    chkGWUtilizationUse.Items[1].Selected = true;
                }
                if (obj_infrastructureNewApplication.CommercialUse == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.UseOption.Yes)
                {
                    chkGWUtilizationUse.Items[2].Selected = true;
                }
                if (obj_infrastructureNewApplication.DewaterUse == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.UseOption.Yes)
                {
                    chkGWUtilizationUse.Items[3].Selected = true;
                }
                //switch (obj_infrastructureNewApplication.GWUtilizationUseFor)
                //{
                //    case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GWUtilizationUseForOption.DrinkDomestic:
                //        rbtnGWUtilizationUse.SelectedValue = "1";
                //        break;
                //    case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GWUtilizationUseForOption.Construction:
                //        rbtnGWUtilizationUse.SelectedValue = "2";
                //        break;
                //    case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GWUtilizationUseForOption.Commercial:
                //        rbtnGWUtilizationUse.SelectedValue = "3";
                //        break;
                //    case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GWUtilizationUseForOption.Dewatering:
                //        rbtnGWUtilizationUse.SelectedValue = "4";
                //        break;

                //}
                //}
                //else
                //{
                // trchkGWUtilizationUse.Visible = false;
                //}

                lblAreaType.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.GetApplySubDistrictAreaTypeCategoryHistory().AreaTypeDesc());
                lblAreaTypeCatagory.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.GetApplySubDistrictAreaTypeCategoryHistory().AreaTypeCategoryDesc());
                if (obj_District.DistrictName != "")
                    lblDistrict.Text = HttpUtility.HtmlEncode(obj_District.DistrictName);
                if (obj_SubDistrict.SubDistrictName != "")
                    lblSubDistrict.Text = HttpUtility.HtmlEncode(obj_SubDistrict.SubDistrictName);

                if (obj_Town.TownName != "")
                    lblVillageTown.Text = HttpUtility.HtmlEncode(obj_Town.TownName);
                if (obj_Village.VillageName != "")
                    lblVillageTown.Text = HttpUtility.HtmlEncode(obj_Village.VillageName);

                switch (obj_infrastructureNewApplication.WetLandArea)
                {
                    case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.WetLandAreaYesNo.Yes:
                        lblWetlandarea.Text = "Yes";
                        break;
                    case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.WetLandAreaYesNo.No:
                        lblWetlandarea.Text = "No";
                        break;
                    case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.WetLandAreaYesNo.NotDefine:
                        lblWetlandarea.Text = "NotDefine";
                        break;
                    default:
                        lblWetlandarea.Text = "NotDefine";
                        break;
                }
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
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewSADApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
            NOCAP.BLL.Master.FeeRequiredPending objFeeRequiredPending = new NOCAP.BLL.Master.FeeRequiredPending(obj_infrastructureNewSADApplication.ApplicationTypeCode, obj_infrastructureNewSADApplication.ApplicationPurposeCode);
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

    #endregion
}