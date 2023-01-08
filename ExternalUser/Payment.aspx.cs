using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Xml;
using System.Threading;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Drawing;
using NOCAP.BLL.ProcessingFee;
using NOCAP.BLL.Master;

public partial class ExternalUser_Payment : System.Web.UI.Page
{

    string strPageName = "Payment";
    string strActionName = "";
    string strStatus = "";
    DateTime dtBharatKoshDate = new DateTime(1900, 08, 29);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txtBharatKoshDatedCalendarExtenderGWCharges.EndDate = System.DateTime.Now;
            // revtxtBharatKoshDatedGWCharges.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
            rngDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
            rngPenalty.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
            rngAppFee.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");

            ValidationExpInit();
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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblApplicationCodeForPayment");
                        if (SourceLabel != null)
                            lblApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                    }
                }
                tblOfflinePayment.Visible = false;
                BindPaymentDetail(sender, e, Convert.ToInt64(lblApplicationCodeFrom.Text));
                BindAppFeeReceived();
                BindChargesReceivedGWCharges();
                BindChargesReceivedPenalty();

                BindGvDoubleTaxPay();

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }

    #region Private
    private void BindChargesReceivedPenalty(string str_sortfieldName = "")
    {
        try
        {
            NOCAP.BLL.PenaltyCorrectionCharges.SADPenaltyCorrectionChargesRec obj_PenaltyCorrectChargesRecBLL = new NOCAP.BLL.PenaltyCorrectionCharges.SADPenaltyCorrectionChargesRec();

            int int_status = 0;
            obj_PenaltyCorrectChargesRecBLL.ApplicationCode = Convert.ToInt64(lblAppCode.Text);
            switch (str_sortfieldName)
            {
                case "CreatedOn":
                    int_status = obj_PenaltyCorrectChargesRecBLL.GetAll(NOCAP.BLL.PenaltyCorrectionCharges.PenaltyCorrectionChargesRec.SortingField.CreatedOn);
                    break;
                case "DDDated":
                    int_status = obj_PenaltyCorrectChargesRecBLL.GetAll(NOCAP.BLL.PenaltyCorrectionCharges.PenaltyCorrectionChargesRec.SortingField.DDDated);
                    break;
                case "SN":
                    int_status = obj_PenaltyCorrectChargesRecBLL.GetAll(NOCAP.BLL.PenaltyCorrectionCharges.PenaltyCorrectionChargesRec.SortingField.SN);
                    break;
                case "":
                    int_status = obj_PenaltyCorrectChargesRecBLL.GetAll(NOCAP.BLL.PenaltyCorrectionCharges.PenaltyCorrectionChargesRec.SortingField.NoSorting);
                    break;
                default:
                    int_status = obj_PenaltyCorrectChargesRecBLL.GetAll(NOCAP.BLL.PenaltyCorrectionCharges.PenaltyCorrectionChargesRec.SortingField.NoSorting);
                    break;
            }
            NOCAP.BLL.PenaltyCorrectionCharges.SADPenaltyCorrectionChargesRec[] arr_PenaltyCorrectChargesRec;
            arr_PenaltyCorrectChargesRec = obj_PenaltyCorrectChargesRecBLL.PCorrectChargesRecCollection;
            if (int_status == 1)
            {
                gvPenalty.DataSource = arr_PenaltyCorrectChargesRec;
                gvPenalty.DataBind();

            }
            else
            {
                lblMessage.Text = HttpUtility.HtmlEncode(obj_PenaltyCorrectChargesRecBLL.CustumMessage);
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally
        {

        }
    }
    private void BindPaymentDetail(object sender, EventArgs e, long lngA_ApplicationCode)
    {
        try
        {
            int? intA_WaterQualityCode = 0;
            int intA_AreaTypeCategoryCode =0;
            string str_minAmt = "";
           // bool PaymentModeFlage = false;
            string str_chargeAmt = "";
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewSADApplication = null;
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewSADApplication = null;
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewSADApplication = null;
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplication = null;
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_miningNewSADApplication = null;
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewSADApplication = null;
            NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_industrialNewSADApplication, out obj_infrastructureNewSADApplication, out obj_miningNewSADApplication, out obj_industrialRenewSADApplication, out obj_infrastructureRenewSADApplication, out obj_miningRenewSADApplication, lngA_ApplicationCode);


            NOCAP.BLL.Master.FeeRequiredPending obj_FeeRequiredPending = null;


           

            #region Industrial New Application
            if (obj_industrialNewSADApplication != null && obj_industrialNewSADApplication.CreatedByExUC > 0)
            {



                intA_WaterQualityCode = obj_industrialNewSADApplication.WaterChargeTypeCode;
                intA_AreaTypeCategoryCode = NOCAPExternalUtility.AreaTypeCategoryCode(obj_industrialNewSADApplication);
                StateCode.Value = Convert.ToString(obj_industrialNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode);
                lblAppType.Text = new NOCAP.BLL.Master.ApplicationType(obj_industrialNewSADApplication.ApplicationTypeCode).ApplicationTypeDescription; //"Industrial";
                lblAppPurpose.Text = new NOCAP.BLL.Master.ApplicationPurpose(obj_industrialNewSADApplication.ApplicationPurposeCode).ApplicationPurposeDesc;//"New";
                lblAppCode.Text = obj_industrialNewSADApplication.IndustrialNewApplicationCode.ToString();
                lblAppName.Text = obj_industrialNewSADApplication.NameOfIndustry;
                if (AreaTypeCategoryCode(obj_industrialNewSADApplication) == 5)
                {
                    lblChargeType.Text = " (Restoration)";

                }
                else
                {
                    lblChargeType.Text = " (Abstraction)";

                }
                lblOfflineChargeType.Text = lblChargeType.Text;
                obj_FeeRequiredPending = new NOCAP.BLL.Master.FeeRequiredPending(obj_industrialNewSADApplication.ApplicationTypeCode, obj_industrialNewSADApplication.ApplicationPurposeCode);
                txtAppFee.Text = obj_FeeRequiredPending.Amount.ToString();
                if (obj_industrialNewSADApplication.WaterQualityCode == 2)
                {
                    rowChargeOnLine.Visible = false;
                    rowChargeOffLine.Visible = false;

                    lblAbstChargeINDRenewSAD.Visible = false;
                }
                else
                {
                    rdbtnCharge.Enabled = true;
                    btnCharge.Enabled = true;

                    lblAbstChargeINDRenewSAD.Visible = true;
                }
                
                



                switch (obj_industrialNewSADApplication.GroundWaterUtilizationFor)
                {
                    case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry:
                        NOCAP.BLL.Master.Penalty obj_Penalty = new NOCAP.BLL.Master.Penalty(17);
                        switch (obj_industrialNewSADApplication.NOCObtainForExistIndustry.NOCObtainForExistIND)
                        {
                            case NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No:
                                lblPenaltyAmount.Text = obj_Penalty.Rate.ToString();
                                lblOfflinePenaltyAmount.Text = obj_Penalty.Rate.ToString();
                                lblPenaltyType.Text = " (" + obj_Penalty.PenaltyDesc + ")";
                                lblOfflinePenaltyType.Text = " (" + obj_Penalty.PenaltyDesc + ")";
                                rowPenalty.Visible = true;
                                //lblPenaltyINDNewSAD.Visible = true;
                                rowOfflinePenalty.Visible = true;
                                break;
                        }
                        break;
                }

            }
            #endregion

            #region Industrial Renew Application
            else if (obj_industrialRenewSADApplication != null && obj_industrialRenewSADApplication.CreatedByExUC > 0)
            {
                intA_WaterQualityCode = obj_industrialRenewSADApplication.WaterChargeTypeCode;
                intA_AreaTypeCategoryCode = NOCAPExternalUtility.AreaTypeCategoryCodeForAppCode(new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_industrialRenewSADApplication.FirstApplicationCode));
             
                StateCode.Value = Convert.ToString(obj_industrialRenewSADApplication.CommunicationAddress.StateCode);
                lblAppType.Text = new NOCAP.BLL.Master.ApplicationType(obj_industrialRenewSADApplication.ApplicationTypeCode).ApplicationTypeDescription; //"Industrial";
                lblAppPurpose.Text = new NOCAP.BLL.Master.ApplicationPurpose(obj_industrialRenewSADApplication.ApplicationPurposeCode).ApplicationPurposeDesc;//"New";
                lblAppCode.Text = obj_industrialRenewSADApplication.IndustrialRenewApplicationCode.ToString();
                lblAppName.Text = (new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_industrialRenewSADApplication.FirstApplicationCode).NameOfIndustry);// obj_industrialNewSADApplication.NameOfIndustry;
                if (AreaTypeCategoryCode(null, obj_industrialRenewSADApplication) == 5)
                {
                    lblChargeType.Text = "Restoration";

                    lblINDRenGWC.Text = "Bharatkosh Reciept (Ground Water Restoration Charges)";
                }
                else
                {
                    lblChargeType.Text = "Abstraction";

                    lblINDRenGWC.Text = "Bharatkosh Reciept (Ground Water Abstraction Charges)";
                }
                lblOfflineChargeType.Text = lblChargeType.Text;
                obj_FeeRequiredPending = new NOCAP.BLL.Master.FeeRequiredPending(obj_industrialRenewSADApplication.ApplicationTypeCode, obj_industrialRenewSADApplication.ApplicationPurposeCode);
                txtAppFee.Text = obj_FeeRequiredPending.Amount.ToString();
               
                if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_industrialRenewSADApplication.IndustrialRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                {
                    NOCAP.BLL.Master.Penalty obj_Penalty = new NOCAP.BLL.Master.Penalty(17);
                    
                    lblPenaltyAmount.Text = obj_Penalty.Rate.ToString();
                    lblOfflinePenaltyAmount.Text = obj_Penalty.Rate.ToString();
                    lblPenaltyType.Text = " (" + obj_Penalty.PenaltyDesc + ")";
                    lblOfflinePenaltyType.Text = " (" + obj_Penalty.PenaltyDesc + ")";
                    rowPenalty.Visible = true;
                    rowOfflinePenalty.Visible = true;

                }


            }
            #endregion

            #region Mining New Application
            if (obj_miningNewSADApplication != null && obj_miningNewSADApplication.CreatedByExUC > 0)
            {
                intA_WaterQualityCode = obj_miningNewSADApplication.WaterChargeTypeCode;
                intA_AreaTypeCategoryCode = NOCAPExternalUtility.AreaTypeCategoryCode(null,null,obj_miningNewSADApplication);
                StateCode.Value = Convert.ToString(obj_miningNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode);
                lblAppType.Text = new NOCAP.BLL.Master.ApplicationType(obj_miningNewSADApplication.ApplicationTypeCode).ApplicationTypeDescription; //"Industrial";
                lblAppPurpose.Text = new NOCAP.BLL.Master.ApplicationPurpose(obj_miningNewSADApplication.ApplicationPurposeCode).ApplicationPurposeDesc;//"New";
                lblAppCode.Text = obj_miningNewSADApplication.ApplicationCode.ToString();
                lblAppName.Text = obj_miningNewSADApplication.NameOfMining;
                if (AreaTypeCategoryCode(null, null, obj_miningNewSADApplication) == 5)
                {
                    lblChargeType.Text = "Restoration";

                }
                else
                {
                    lblChargeType.Text = "Abstraction";


                }
                lblOfflineChargeType.Text = lblChargeType.Text;
                obj_FeeRequiredPending = new NOCAP.BLL.Master.FeeRequiredPending(obj_miningNewSADApplication.ApplicationTypeCode, obj_miningNewSADApplication.ApplicationPurposeCode);
                txtAppFee.Text = obj_FeeRequiredPending.Amount.ToString();
                
                switch (obj_miningNewSADApplication.GroundWaterUtilizationFor)
                {
                    case NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry:
                        NOCAP.BLL.Master.Penalty obj_Penalty = new NOCAP.BLL.Master.Penalty(17);
                        switch (obj_miningNewSADApplication.NOCObtainForExistIndustry.NOCObtainForExistIND)
                        {
                            case NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No:
                                lblPenaltyAmount.Text = obj_Penalty.Rate.ToString();
                                lblOfflinePenaltyAmount.Text = obj_Penalty.Rate.ToString();
                                lblPenaltyType.Text = " (" + obj_Penalty.PenaltyDesc + ")";
                                lblOfflinePenaltyType.Text = " (" + obj_Penalty.PenaltyDesc + ")";
                                rowPenalty.Visible = true;

                                rowOfflinePenalty.Visible = true;
                                break;
                        }
                        break;
                }
            }
            #endregion

            #region Mining Renew Application
            if (obj_miningRenewSADApplication != null && obj_miningRenewSADApplication.CreatedByExUC > 0)
            {
                intA_WaterQualityCode = obj_miningRenewSADApplication.WaterChargeTypeCode;
                intA_AreaTypeCategoryCode = NOCAPExternalUtility.AreaTypeCategoryCodeForAppCode(null,null,new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_miningRenewSADApplication.FirstApplicationCode));

                StateCode.Value = Convert.ToString(obj_miningRenewSADApplication.CommunicationAddress.StateCode);
                lblAppType.Text = new NOCAP.BLL.Master.ApplicationType(obj_miningRenewSADApplication.ApplicationTypeCode).ApplicationTypeDescription; //"Industrial";
                lblAppPurpose.Text = new NOCAP.BLL.Master.ApplicationPurpose(obj_miningRenewSADApplication.ApplicationPurposeCode).ApplicationPurposeDesc;//"New";
                lblAppCode.Text = obj_miningRenewSADApplication.MiningRenewApplicationCode.ToString();
                lblAppName.Text = (new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_miningRenewSADApplication.FirstApplicationCode).NameOfMining);// obj_industrialNewSADApplication.NameOfIndustry;
                if (AreaTypeCategoryCode(null, null, null, obj_miningRenewSADApplication) == 5)
                {
                    lblChargeType.Text = "Restoration";


                }
                else
                {
                    lblChargeType.Text = "Abstraction";


                }
                lblOfflineChargeType.Text = lblChargeType.Text;
                obj_FeeRequiredPending = new NOCAP.BLL.Master.FeeRequiredPending(obj_miningRenewSADApplication.ApplicationTypeCode, obj_miningRenewSADApplication.ApplicationPurposeCode);
                txtAppFee.Text = obj_FeeRequiredPending.Amount.ToString();
               
                if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_miningRenewSADApplication.MiningRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                {
                    NOCAP.BLL.Master.Penalty obj_Penalty = new NOCAP.BLL.Master.Penalty(17);
                   
                    lblPenaltyAmount.Text = obj_Penalty.Rate.ToString();
                    lblOfflinePenaltyAmount.Text = obj_Penalty.Rate.ToString();
                    lblPenaltyType.Text = " (" + obj_Penalty.PenaltyDesc + ")";
                    lblOfflinePenaltyType.Text = " (" + obj_Penalty.PenaltyDesc + ")";
                    rowPenalty.Visible = true;
                    rowOfflinePenalty.Visible = true;

                }

            }
            #endregion

            #region Infrastructure New Application
            if (obj_infrastructureNewSADApplication != null && obj_infrastructureNewSADApplication.CreatedByExUC > 0)
            {
                intA_WaterQualityCode = obj_infrastructureNewSADApplication.WaterChargeTypeCode;
                intA_AreaTypeCategoryCode = NOCAPExternalUtility.AreaTypeCategoryCode(null, obj_infrastructureNewSADApplication);
                StateCode.Value = Convert.ToString(obj_infrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode);
                lblAppType.Text = new NOCAP.BLL.Master.ApplicationType(obj_infrastructureNewSADApplication.ApplicationTypeCode).ApplicationTypeDescription; //"Industrial";
                lblAppPurpose.Text = new NOCAP.BLL.Master.ApplicationPurpose(obj_infrastructureNewSADApplication.ApplicationPurposeCode).ApplicationPurposeDesc;//"New";
                lblAppCode.Text = obj_infrastructureNewSADApplication.InfrastructureNewApplicationCode.ToString();
                lblAppName.Text = obj_infrastructureNewSADApplication.NameOfInfrastructure;
                if (AreaTypeCategoryCode(null, null, null, null, obj_infrastructureNewSADApplication) == 5)
                {
                    lblChargeType.Text = "Restoration";

                }
                else
                {
                    lblChargeType.Text = "Abstraction";


                }
                lblOfflineChargeType.Text = lblChargeType.Text;
                obj_FeeRequiredPending = new NOCAP.BLL.Master.FeeRequiredPending(obj_infrastructureNewSADApplication.ApplicationTypeCode, obj_infrastructureNewSADApplication.ApplicationPurposeCode);
                txtAppFee.Text = obj_FeeRequiredPending.Amount.ToString();
                
                switch (obj_infrastructureNewSADApplication.GroundWaterUtilizationFor)
                {
                    case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.ExistingIndustry:
                        NOCAP.BLL.Master.Penalty obj_Penalty = new NOCAP.BLL.Master.Penalty(17);
                        switch (obj_infrastructureNewSADApplication.NOCObtainForExistIndustry.NOCObtainForExistIND)
                        {
                            case NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No:
                                lblPenaltyAmount.Text = obj_Penalty.Rate.ToString();
                                lblOfflinePenaltyAmount.Text = obj_Penalty.Rate.ToString();
                                lblPenaltyType.Text = " (" + obj_Penalty.PenaltyDesc + ")";
                                lblOfflinePenaltyType.Text = " (" + obj_Penalty.PenaltyDesc + ")";
                                rowPenalty.Visible = true;

                                rowOfflinePenalty.Visible = true;
                                break;
                        }
                        break;
                }
            }
            #endregion

            #region Infrastructure Renew Application
            if (obj_infrastructureRenewSADApplication != null && obj_infrastructureRenewSADApplication.CreatedByExUC > 0)
            {
                intA_WaterQualityCode = obj_infrastructureRenewSADApplication.WaterChargeTypeCode;
                intA_AreaTypeCategoryCode = NOCAPExternalUtility.AreaTypeCategoryCodeForAppCode(null, new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_infrastructureRenewSADApplication.FirstApplicationCode));

                StateCode.Value = Convert.ToString(obj_infrastructureRenewSADApplication.CommunicationAddress.StateCode);
                lblAppType.Text = new NOCAP.BLL.Master.ApplicationType(obj_infrastructureRenewSADApplication.ApplicationTypeCode).ApplicationTypeDescription; //"Industrial";
                lblAppPurpose.Text = new NOCAP.BLL.Master.ApplicationPurpose(obj_infrastructureRenewSADApplication.ApplicationPurposeCode).ApplicationPurposeDesc;//"New";
                lblAppCode.Text = obj_infrastructureRenewSADApplication.InfrastructureRenewApplicationCode.ToString();
                lblAppName.Text = (new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_infrastructureRenewSADApplication.FirstApplicationCode).NameOfInfrastructure);// obj_industrialNewSADApplication.NameOfIndustry;
                if (AreaTypeCategoryCode(null, null, null, null, null, obj_infrastructureRenewSADApplication) == 5)
                {

                    lblChargeType.Text = "Restoration";

                }
                else
                {
                    lblChargeType.Text = "Abstraction";

                }
                lblOfflineChargeType.Text = lblChargeType.Text;
                obj_FeeRequiredPending = new NOCAP.BLL.Master.FeeRequiredPending(obj_infrastructureRenewSADApplication.ApplicationTypeCode, obj_infrastructureRenewSADApplication.ApplicationPurposeCode);
                txtAppFee.Text = obj_FeeRequiredPending.Amount.ToString();
               
                if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_infrastructureRenewSADApplication.InfrastructureRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                {
                    NOCAP.BLL.Master.Penalty obj_Penalty = new NOCAP.BLL.Master.Penalty(17);                   
                    lblPenaltyAmount.Text = obj_Penalty.Rate.ToString();
                    lblOfflinePenaltyAmount.Text = obj_Penalty.Rate.ToString();
                    lblPenaltyType.Text = " (" + obj_Penalty.PenaltyDesc + ")";
                    lblOfflinePenaltyType.Text = " (" + obj_Penalty.PenaltyDesc + ")";
                    rowPenalty.Visible = true;
                    rowOfflinePenalty.Visible = true;

                }

            }
            #endregion

            NOCAP.BLL.Misc.Payment.SADOnlinePayment obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment();
            int int_Status = 0;
            NOCAP.BLL.Misc.Payment.SADOnlinePayment[] arr_SADOnlinePayment = null;

            NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails obj_SADOnlinePaymentDetails = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails(lngA_ApplicationCode);

            obj_SADOnlinePayment.ApplicationCode = lngA_ApplicationCode;
            int_Status = obj_SADOnlinePayment.GetALL();
            arr_SADOnlinePayment = obj_SADOnlinePayment.SADOnlinePaymentCollection
                         .Where(x => x.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success
                         || x.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Pending).ToArray();



            foreach (NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_SADOnlinePayment in arr_SADOnlinePayment)
            {
                switch (objA_SADOnlinePayment.PaymentTypeMode)
                {
                    #region Combined
                    case NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined:
                        #region Proccessing Fee
                        if (int_Status == 1)
                        {
                            if (arr_SADOnlinePayment != null && arr_SADOnlinePayment.Length > 0)
                            {
                               // PaymentModeFlage = true;
                                PayBtn.Enabled = false;
                                ddlPaidFee.SelectedIndex = 2;
                                rdBtnPayMode.SelectedValue = "1";
                            }
                            else
                            
                                PayBtn.Enabled = true;
                               
                            
                        }



                        #endregion

                        break;
                    #endregion

                    #region Single
                    case NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single:
                        rdBtnPayMode.SelectedValue = "0";
                        for (int i = 0; i < arr_SADOnlinePayment.Length; i++)
                        {
                            #region Proccessing Fee
                            obj_SADOnlinePaymentDetails = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails(arr_SADOnlinePayment[i].ApplicationCode, arr_SADOnlinePayment[i].OrderPaymentCode, 1);
                            if (obj_SADOnlinePaymentDetails != null && obj_SADOnlinePaymentDetails.CreatedByExUC > 0)
                            {
                               // PaymentModeFlage = true;
                                rdbtnAppFee.Enabled = false;
                                btnAppFee.Enabled = false;
                                ddlPaidFee.SelectedIndex = 2;
                            }
                            #endregion



                            #region Charge
                            if (intA_WaterQualityCode == 1)
                            {
                                rdbtnCharge.Enabled = true;
                                btnCharge.Enabled = true;
                                lblAbstChargeINDRenewSAD.Visible = true;
                                if (intA_AreaTypeCategoryCode == 5)
                                    obj_SADOnlinePaymentDetails = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails(arr_SADOnlinePayment[i].ApplicationCode, arr_SADOnlinePayment[i].OrderPaymentCode, 2);
                                else
                                    obj_SADOnlinePaymentDetails = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails(arr_SADOnlinePayment[i].ApplicationCode, arr_SADOnlinePayment[i].OrderPaymentCode, 3);
                                if (obj_SADOnlinePaymentDetails != null && obj_SADOnlinePaymentDetails.CreatedByExUC > 0)
                                {
                                   // PaymentModeFlage = true;
                                    rdbtnCharge.Enabled = false;
                                    btnCharge.Enabled = false;
                                    ddlPaidFee.SelectedIndex = 2;
                                }
                            }
                            else
                            {
                                rowChargeOnLine.Visible = false;
                                rowChargeOffLine.Visible = false;
                                lblAbstChargeINDRenewSAD.Visible = false;

                            }

                            #endregion


                            #region Penalty
                            obj_SADOnlinePaymentDetails = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails(arr_SADOnlinePayment[i].ApplicationCode, arr_SADOnlinePayment[i].OrderPaymentCode, 4);
                            if (obj_SADOnlinePaymentDetails != null && obj_SADOnlinePaymentDetails.CreatedByExUC > 0)
                            {
                                rdbtnPenalty.Enabled = false;
                                btnPenalty.Enabled = false;
                                ddlPaidFee.SelectedIndex = 2;
                            }

                            else
                            {
                                rdbtnPenalty.Enabled = true;
                                btnPenalty.Enabled = true;
                            }
                            #endregion

                        }
                        break;








                       

                    #endregion
                  

                }
            }
            str_chargeAmt = NOCAPExternalUtility.GetGroundWaterChargeForSADAppCode(lngA_ApplicationCode, out str_minAmt);

            // lblMaxCharge.Text = str_chargeAmt + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(str_chargeAmt)) + ")";


            txtGWCharge.Text = str_minAmt;
            lblGWCharge.Text = HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(str_minAmt));


            txtOffGWCharge.Text = str_minAmt;
            lblOffGWCharge.Text = HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(str_minAmt));
            // lblMaxOffGWCharge.Text = str_chargeAmt + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(str_chargeAmt)) + ")";




            lblFeeAmout.Text = obj_FeeRequiredPending.Amount.ToString();
            lblOfflineFeeAmout.Text = obj_FeeRequiredPending.Amount.ToString();
            ddlPaidFee_SelectedIndexChanged(sender, e);
            rdBtnPayMode_SelectedIndexChanged(sender, e);


        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    private void ValidationExpInit()
    {
        //revtxtSalientFeaturesOfInActivity.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtSalientFeaturesOfInActivity.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;


        //revLengthtxtSalientFeaturesOfInActivity.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        //revLengthtxtSalientFeaturesOfInActivity.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");


    }
    private int AreaTypeCategoryCode(NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = null,
        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewSADApplication = null,
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewSADApplication = null,
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewSADApplication = null,
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewSADApplication = null, NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewSADApplication = null)
    {
        NOCAP.BLL.Master.SubDistrict obj_subDistrict = null;
        NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_subDistrictAreaTypeCategoryHistory = null;
        if (obj_IndustrialNewSADApplication != null)
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        else if (obj_IndustrialRenewSADApplication != null)
        {
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_IndustrialRenewSADApplication.FirstApplicationCode);
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        }
        else if (obj_MiningNewSADApplication != null)
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);

        else if (obj_MiningRenewSADApplication != null)
        {
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_MiningRenewSADApplication.FirstApplicationCode);
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        }
        else if (obj_InfrastructureNewSADApplication != null)
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);

        else if (obj_InfrastructureRenewSADApplication != null)
        {
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_InfrastructureRenewSADApplication.FirstApplicationCode);
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);

        }


        obj_subDistrictAreaTypeCategoryHistory = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory();
        return obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode;
    }
    private void CreateXmlParam(Dictionary<int, decimal> dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode enu, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode enu2, decimal decA_GWAmountValue, decimal? ArearAmount = null,
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = null,
        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewSADApplication = null,
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewSADApplication = null,
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewSADApplication = null,
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewSADApplication = null,
        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewSADApplication = null, string strActionName = "", string strStatus = "")
    {


        string XMLstr = ""; string OrderPaymentCode = "";
        if (obj_IndustrialNewSADApplication != null)
            XMLstr = NOCAPExternalUtility.CreateXMLForSADApplication(Convert.ToInt64(Session["ExternalUserCode"]), dict, decA_GWAmountValue, ArearAmount, enu, enu2, ref OrderPaymentCode, obj_IndustrialNewSADApplication: obj_IndustrialNewSADApplication);
        else if (obj_IndustrialRenewSADApplication != null)
            XMLstr = NOCAPExternalUtility.CreateXMLForSADApplication(Convert.ToInt64(Session["ExternalUserCode"]), dict, decA_GWAmountValue, ArearAmount, enu, enu2, ref OrderPaymentCode, obj_IndustrialRenewSADApplication: obj_IndustrialRenewSADApplication);
        else if (obj_InfrastructureNewSADApplication != null)
            XMLstr = NOCAPExternalUtility.CreateXMLForSADApplication(Convert.ToInt64(Session["ExternalUserCode"]), dict, decA_GWAmountValue, ArearAmount, enu, enu2, ref OrderPaymentCode, obj_InfrastructureNewSADApplication: obj_InfrastructureNewSADApplication);
        else if (obj_InfrastructureRenewSADApplication != null)
            XMLstr = NOCAPExternalUtility.CreateXMLForSADApplication(Convert.ToInt64(Session["ExternalUserCode"]), dict, decA_GWAmountValue, ArearAmount, enu, enu2, ref OrderPaymentCode, obj_InfrastructureRenewSADApplication: obj_InfrastructureRenewSADApplication);
        else if (obj_MiningNewSADApplication != null)
            XMLstr = NOCAPExternalUtility.CreateXMLForSADApplication(Convert.ToInt64(Session["ExternalUserCode"]), dict, decA_GWAmountValue, ArearAmount, enu, enu2, ref OrderPaymentCode, obj_MiningNewSADApplication: obj_MiningNewSADApplication);
        else if (obj_MiningRenewSADApplication != null)
            XMLstr = NOCAPExternalUtility.CreateXMLForSADApplication(Convert.ToInt64(Session["ExternalUserCode"]), dict, decA_GWAmountValue, ArearAmount, enu, enu2, ref OrderPaymentCode, obj_MiningRenewSADApplication: obj_MiningRenewSADApplication);
        SendXml(XMLstr, OrderPaymentCode, strActionName, strStatus);


    }
    private void SendXml(string XMLstr, string OrderPaymentCode, string strActionName, string strStatus)
    {
        NOCAP.BLL.Misc.Payment.SADOnlinePayment obj_SADOnlinePayment = null;
        try
        {
            strActionName = "Combined Online Payment for All";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(XMLstr);
            xmlDoc = NOCAPExternalUtility.signedFun(xmlDoc);
            foreach (XmlNode node in xmlDoc)
            {
                if (node.NodeType == XmlNodeType.XmlDeclaration)
                {
                    xmlDoc.RemoveChild(node);
                }
            }
            obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(Convert.ToInt64(lblApplicationCodeFrom.Text), OrderPaymentCode);
            obj_SADOnlinePayment.OnlinePaymentXMLSent = xmlDoc.OuterXml;
            obj_SADOnlinePayment.SetOnlinePaymentXML();

            string xmlDocBase64 = Convert.ToBase64String(Encoding.Default.GetBytes(xmlDoc.OuterXml));
            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
            sb.AppendFormat("<form name='form' action='{0}' method='post'>", ConfigurationManager.AppSettings["NTRPURI"]);
            sb.AppendFormat("<input type='hidden' name='bharrkkosh' value='{0}'>", HttpUtility.HtmlEncode(xmlDocBase64));
            sb.Append("</form>");
            sb.Append("</body>");
            sb.Append("</html>");
            Response.Write(sb.ToString());
            Response.End();
        }
        catch (ThreadAbortException ex)
        { }
        catch (Exception ex)
        {
            strStatus = ex.Message;
            lblMessage.Text = ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
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
            SADOnlinePaymentActionTrail obj_SADOnlinePaymentActionTrail = new SADOnlinePaymentActionTrail();
            if (Session["ExternalUserCode"] != null)
            {
                obj_SADOnlinePaymentActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
                obj_SADOnlinePaymentActionTrail.IP_Address = Request.UserHostAddress;
                obj_SADOnlinePaymentActionTrail.ActionPerformed = strPageName + "-" + strActionName;
                obj_SADOnlinePaymentActionTrail.Status = strStatus + ",OrderPaymentCode-" + Convert.ToString(OrderPaymentCode) + ",App Code-" + lblApplicationCodeFrom.Text.Trim();
                if (obj_SADOnlinePaymentActionTrail != null)
                    obj_SADOnlinePaymentActionTrail.AddSADOnlinePaymentAction(obj_SADOnlinePaymentActionTrail);
            }
        }

    }
    private void UploadBharatKoshRecieptApplicationFee()
    {

        hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
        Session["CSRF"] = hidCSRF.Value;
        strActionName = "File Abstraction Charges";
        NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus obj_SADProcessingFeeStatus = new NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus();
        NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus obj_SADProcessingFeeStatusForNoLimit = new NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus();
        obj_SADProcessingFeeStatusForNoLimit.ApplicationCode = Convert.ToInt64(lblAppCode.Text);
        NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus[] arr_SADProcessingFeeStatusList;
        obj_SADProcessingFeeStatusForNoLimit.GetAll();
        arr_SADProcessingFeeStatusList = obj_SADProcessingFeeStatusForNoLimit.ProcessingFeeStatusCollection;
        if (arr_SADProcessingFeeStatusList.Count() < AttachmentNumberLimit())
        {
            string str_fname;
            string str_ext;
            string str_newFileNameWithPath = "";

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
                            if (FileUploadBharatKoshReciept.PostedFile.ContentLength < AttachmentSizeLimit())
                            {

                                obj_SADProcessingFeeStatus.AttFile = (NOCAPExternalUtility.StreamFile(FileUploadBharatKoshReciept.PostedFile));
                                obj_SADProcessingFeeStatus.ContentType = FileUploadBharatKoshReciept.PostedFile.ContentType;
                                obj_SADProcessingFeeStatus.FileExtension = str_ext;
                                obj_SADProcessingFeeStatus.AttPath = FileUploadBharatKoshReciept.FileName;
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

                obj_SADProcessingFeeStatus.BharatTransReferanceNumber = Convert.ToInt64(txtBhartRefAppFee.Text);
                obj_SADProcessingFeeStatus.BharatTransDated = Convert.ToDateTime(txtBhartDateAppFee.Text);
                obj_SADProcessingFeeStatus.Amount = Convert.ToDecimal(txtAppFee.Text);


                obj_SADProcessingFeeStatus.BharatPayStatus = NOCAP.BLL.Common.CommonEnum.ProcessingFeePaymentStatusOption.Success;



                obj_SADProcessingFeeStatus.ApplicationCode = Convert.ToInt32(lblAppCode.Text);


                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                obj_SADProcessingFeeStatus.CreatedByUserCode = (int)obj_externalUser.ExternalUserCode;
                obj_SADProcessingFeeStatus.AttName = txtBharatKoshReciept.Text.Trim();


                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = null;
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewSADApplication = null;
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewSADApplication = null;
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewSADApplication = null;
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewSADApplication = null;
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewSADApplication = null;

                NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_IndustrialNewSADApplication, out obj_InfrastructureNewSADApplication, out obj_MiningNewSADApplication,
                    out obj_IndustrialRenewSADApplication, out obj_InfrastructureRenewSADApplication, out obj_MiningRenewSADApplication, Convert.ToInt64(lblAppCode.Text));
                if (obj_IndustrialNewSADApplication != null && obj_IndustrialNewSADApplication.CreatedByExUC > 0)
                    obj_SADProcessingFeeStatus.AttachmentCode = obj_IndustrialNewSADApplication.IndustrialNewBharatKoshRecieptAttachmentCode;
                else if (obj_IndustrialRenewSADApplication != null && obj_IndustrialRenewSADApplication.CreatedByExUC > 0)
                    obj_SADProcessingFeeStatus.AttachmentCode = obj_IndustrialRenewSADApplication.IndustrialRenewBharatKoshRecieptAttachmentCode;
                else if (obj_InfrastructureNewSADApplication != null && obj_InfrastructureNewSADApplication.CreatedByExUC > 0)
                    obj_SADProcessingFeeStatus.AttachmentCode = obj_InfrastructureNewSADApplication.InfraStructureNewBharatKoshRecieptAttachmentCode;
                else if (obj_InfrastructureRenewSADApplication != null && obj_InfrastructureRenewSADApplication.CreatedByExUC > 0)
                    obj_SADProcessingFeeStatus.AttachmentCode = obj_InfrastructureRenewSADApplication.InfraStructureRenewBharatKoshRecieptAttachmentCode;
                else if (obj_MiningNewSADApplication != null && obj_MiningNewSADApplication.CreatedByExUC > 0)
                    obj_SADProcessingFeeStatus.AttachmentCode = obj_MiningNewSADApplication.MiningNewBharatKoshRecieptAttachmentCode;
                else if (obj_MiningRenewSADApplication != null && obj_MiningRenewSADApplication.CreatedByExUC > 0)
                    obj_SADProcessingFeeStatus.AttachmentCode = obj_MiningRenewSADApplication.MiningRenewBharatKoshRecieptAttachmentCode;

                if (obj_SADProcessingFeeStatus.Add() == 1)
                {
                    strStatus = "File Upload Success";
                    lblMessageBharatKoshReciept.ForeColor = Color.Green;
                    BindAppFeeReceived();
                }
                else
                {
                    strStatus = "File Upload Failed";
                    lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
                }
                lblMessageBharatKoshReciept.Text = obj_SADProcessingFeeStatus.CustumMessage;
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
            lblMessageBharatKoshReciept.Text = "Maximum number of files to be uploaded is 5";
            lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
        }



    }

    private void UploadDoubleTaxPayment()
    {

        hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
        Session["CSRF"] = hidCSRF.Value;
        strActionName = "File Abstraction Charges";
        DoubleTaxPayment obj_DoubleTaxPaymentStatus = new DoubleTaxPayment();
        DoubleTaxPayment obj_DoubleTaxPaymentLimit = new DoubleTaxPayment();
        DoubleTaxPayment[] arr_DoubleTaxPaymentList;
        arr_DoubleTaxPaymentList = obj_DoubleTaxPaymentLimit.GetDoubleTaxPaymentListForAppCodeList(Convert.ToInt64(lblAppCode.Text), DoubleTaxPayment.SortingField.NoSorting);
        if (arr_DoubleTaxPaymentList.Count() < AttachmentNumberLimit())
        {
            string str_fname;
            string str_ext;
            string str_newFileNameWithPath = "";

            byte[] buffer = new byte[1];
            try
            {
                if (FileUploadDoubleTaxPay.HasFile)
                {
                    str_ext = System.IO.Path.GetExtension(FileUploadDoubleTaxPay.PostedFile.FileName).ToLower();
                    str_fname = FileUploadDoubleTaxPay.FileName;
                    if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                    {
                        if (NOCAPExternalUtility.IsValidFile(FileUploadDoubleTaxPay.PostedFile))
                        {
                            if (FileUploadDoubleTaxPay.PostedFile.ContentLength < AttachmentSizeLimit())
                            {

                                obj_DoubleTaxPaymentStatus.AttFile = (NOCAPExternalUtility.StreamFile(FileUploadDoubleTaxPay.PostedFile));
                                obj_DoubleTaxPaymentStatus.ContentType = FileUploadDoubleTaxPay.PostedFile.ContentType;
                                obj_DoubleTaxPaymentStatus.FileExtension = str_ext;
                                obj_DoubleTaxPaymentStatus.AttPath = FileUploadDoubleTaxPay.FileName;
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                LblDoubleTax.Text = "File can not upload. It has more than 5 MB size";
                                LblDoubleTax.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            LblDoubleTax.Text = "Not a valid file!!..Select an other file!!";
                            LblDoubleTax.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        LblDoubleTax.Text = "Not a valid file!!..Select an other file!!";
                        LblDoubleTax.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                }
                else
                {
                    LblDoubleTax.Text = "Please select a file..!!";
                    LblDoubleTax.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                obj_DoubleTaxPaymentStatus.StatePaidAmt = Convert.ToDecimal(txtStatePaidAmt.Text);

                obj_DoubleTaxPaymentStatus.PaidToAgency = Convert.ToString(TxtPaidToAgancy.Text);
                obj_DoubleTaxPaymentStatus.ApplicationCode = Convert.ToInt32(lblAppCode.Text);
                obj_DoubleTaxPaymentStatus.AttName = txtAttNameDoubleTax.Text.Trim();

                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                obj_DoubleTaxPaymentStatus.CreatedByExUC = (int)obj_externalUser.ExternalUserCode;

                if (obj_DoubleTaxPaymentStatus.Add() == 1)
                {
                    strStatus = "File Upload Success";
                    LblDoubleTax.ForeColor = Color.Green;
                    BindGvDoubleTaxPay();
                }
                else
                {
                    strStatus = "File Upload Failed";
                    LblDoubleTax.ForeColor = System.Drawing.Color.Red;
                }
                LblDoubleTax.Text = obj_DoubleTaxPaymentStatus.CustumMessage;
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
            LblDoubleTax.Text = "Maximum number of files to be uploaded is 5";
            LblDoubleTax.ForeColor = System.Drawing.Color.Red;
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

    private void UploadBharatKoshRecieptINDSAD()
    {
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplicationForNoLimit = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblAppCode.Text));
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment[] arr_industrialNewApplicationAttachmentList;
        arr_industrialNewApplicationAttachmentList = obj_industrialNewApplicationForNoLimit.GetBharatKoshRecieptAttachmentList();
        if (arr_industrialNewApplicationAttachmentList.Count() < AttachmentNumberLimit())
        {
            #region Attachment
            string str_fname;
            string str_ext;
            string str_newFileNameWithPath = "";
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment obj_insertIndustrialNewApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment();
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt32(lblAppCode.Text));
            List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment> lst_industrialNewApplicationAttachmentList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment>();
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
                                obj_insertIndustrialNewApplicationAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadBharatKoshReciept.PostedFile);
                                obj_insertIndustrialNewApplicationAttachment.ContentType = FileUploadBharatKoshReciept.PostedFile.ContentType;
                                obj_insertIndustrialNewApplicationAttachment.AttachmentPath = FileUploadBharatKoshReciept.FileName;
                                obj_insertIndustrialNewApplicationAttachment.FileExtension = str_ext;
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
                obj_insertIndustrialNewApplicationAttachment.IndustrialNewApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                obj_insertIndustrialNewApplicationAttachment.AttachmentCode = obj_industrialNewApplication.IndustrialNewBharatKoshRecieptAttachmentCode;
                obj_insertIndustrialNewApplicationAttachment.AttachmentName = txtBharatKoshReciept.Text;
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                obj_insertIndustrialNewApplicationAttachment.CreatedByExUC = obj_externalUser.ExternalUserCode;
                if (obj_insertIndustrialNewApplicationAttachment.Add() == 1)
                {
                    strStatus = "File Upload Success";
                    obj_insertIndustrialNewApplicationAttachment.AttachmentPath = str_newFileNameWithPath;
                    lblMessageBharatKoshReciept.ForeColor = Color.Green;


                }
                else
                {
                    strStatus = "File Upload Failed";

                    lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
                }
                //BindBharatKoshRecieptAttachment(Convert.ToInt32(lblAppCode.Text));
                //  clearMessage();
                lblMessageBharatKoshReciept.Text = obj_insertIndustrialNewApplicationAttachment.CustumMessage;
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
            #endregion
        }
        else
        {
            lblMessage.Text = "Maximum number of files to be uploaded is 5";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void UploadBharatKoshRecieptAbstChargeSAD()
    {

        hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
        Session["CSRF"] = hidCSRF.Value;
        strActionName = "File Abstraction Charges";
        NOCAP.BLL.GroundWaterChargeRec.SADGroundWaterChargesRec obj_GroundWaterChargeRec = new NOCAP.BLL.GroundWaterChargeRec.SADGroundWaterChargesRec();
        NOCAP.BLL.GroundWaterChargeRec.SADGroundWaterChargesRec obj_GroundWaterChargeRecForNoLimit = new NOCAP.BLL.GroundWaterChargeRec.SADGroundWaterChargesRec();
        obj_GroundWaterChargeRecForNoLimit.ApplicationCode = Convert.ToInt64(lblAppCode.Text);
        NOCAP.BLL.GroundWaterChargeRec.SADGroundWaterChargesRec[] arr_SADGroundWaterChargesRecList;
        obj_GroundWaterChargeRecForNoLimit.GetAll();
        arr_SADGroundWaterChargesRecList = obj_GroundWaterChargeRecForNoLimit.SADGroundWaterChargesRecCollection;
        if (arr_SADGroundWaterChargesRecList.Count() < AttachmentNumberLimit())
        {
            string str_fname;
            string str_ext;
            string str_newFileNameWithPath = "";

            byte[] buffer = new byte[1];
            try
            {
                if (FileUploadAbstChargeINDRenewSAD.HasFile)
                {
                    str_ext = System.IO.Path.GetExtension(FileUploadAbstChargeINDRenewSAD.PostedFile.FileName).ToLower();
                    str_fname = FileUploadAbstChargeINDRenewSAD.FileName;
                    if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                    {
                        if (NOCAPExternalUtility.IsValidFile(FileUploadAbstChargeINDRenewSAD.PostedFile))
                        {
                            if (FileUploadAbstChargeINDRenewSAD.PostedFile.ContentLength < AttachmentSizeLimit())
                            {

                                obj_GroundWaterChargeRec.AttachmentFile = (NOCAPExternalUtility.StreamFile(FileUploadAbstChargeINDRenewSAD.PostedFile));
                                obj_GroundWaterChargeRec.ContentType = FileUploadAbstChargeINDRenewSAD.PostedFile.ContentType;
                                obj_GroundWaterChargeRec.FileExtension = str_ext;
                                obj_GroundWaterChargeRec.AttPath = FileUploadAbstChargeINDRenewSAD.FileName;
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

                obj_GroundWaterChargeRec.BharatTransReferanceNumber = Convert.ToInt64(txtBharatKoshRefferenceNoGWCharges.Text);
                obj_GroundWaterChargeRec.BharatTransDated = Convert.ToDateTime(txtBharatKoshDatedGWCharges.Text);
                if (txtGWChargeAmountFinally.Text.Trim() != "")
                    obj_GroundWaterChargeRec.Amount = Convert.ToDecimal(txtGWChargeAmountFinally.Text);
                if (txtGWArearAmountFinally.Text.Trim() != "")
                    obj_GroundWaterChargeRec.ArearAmount = Convert.ToDecimal(txtGWArearAmountFinally.Text);

                obj_GroundWaterChargeRec.BharatPayStatus = NOCAP.BLL.Common.CommonEnum.GWChargePaymentStatusOption.Success;



                obj_GroundWaterChargeRec.ApplicationCode = Convert.ToInt32(lblAppCode.Text);

                obj_GroundWaterChargeRec.EMISN = 1;
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                obj_GroundWaterChargeRec.CreatedByUserCode = (int)obj_externalUser.ExternalUserCode;
                obj_GroundWaterChargeRec.AttName = txtAbstChargeINDRenewSAD.Text.Trim();


                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = null;
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewSADApplication = null;
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewSADApplication = null;
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewSADApplication = null;
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewSADApplication = null;
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewSADApplication = null;

                NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_IndustrialNewSADApplication, out obj_InfrastructureNewSADApplication, out obj_MiningNewSADApplication,
                    out obj_IndustrialRenewSADApplication, out obj_InfrastructureRenewSADApplication, out obj_MiningRenewSADApplication, Convert.ToInt64(lblAppCode.Text));
                if (obj_IndustrialNewSADApplication != null && obj_IndustrialNewSADApplication.CreatedByExUC > 0)
                {
                    obj_GroundWaterChargeRec.AttachmentCode = obj_IndustrialNewSADApplication.AbsRestChargeAttCode;
                    if (AreaTypeCategoryCode(obj_IndustrialNewSADApplication) == 5)
                        obj_GroundWaterChargeRec.PaymentTypeCode = (int)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWRCharge;
                    else
                        obj_GroundWaterChargeRec.PaymentTypeCode = (int)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWACharge;
                }
                else if (obj_IndustrialRenewSADApplication != null && obj_IndustrialRenewSADApplication.CreatedByExUC > 0)
                {
                    obj_GroundWaterChargeRec.AttachmentCode = obj_IndustrialRenewSADApplication.AbsRestChargeAttCode;
                    if (AreaTypeCategoryCode(null, obj_IndustrialRenewSADApplication) == 5)
                        obj_GroundWaterChargeRec.PaymentTypeCode = (int)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWRCharge;
                    else
                        obj_GroundWaterChargeRec.PaymentTypeCode = (int)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWACharge;
                }
                else if (obj_InfrastructureNewSADApplication != null && obj_InfrastructureNewSADApplication.CreatedByExUC > 0)
                {
                    obj_GroundWaterChargeRec.AttachmentCode = obj_InfrastructureNewSADApplication.AbsRestChargeAttCode;
                    if (AreaTypeCategoryCode(null, null, null, null, obj_InfrastructureNewSADApplication) == 5)
                        obj_GroundWaterChargeRec.PaymentTypeCode = (int)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWRCharge;
                    else
                        obj_GroundWaterChargeRec.PaymentTypeCode = (int)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWACharge;
                }
                else if (obj_InfrastructureRenewSADApplication != null && obj_InfrastructureRenewSADApplication.CreatedByExUC > 0)
                {
                    obj_GroundWaterChargeRec.AttachmentCode = obj_InfrastructureRenewSADApplication.AbsRestChargeAttCode;
                    if (AreaTypeCategoryCode(null, null, null, null, null, obj_InfrastructureRenewSADApplication) == 5)
                        obj_GroundWaterChargeRec.PaymentTypeCode = (int)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWRCharge;
                    else
                        obj_GroundWaterChargeRec.PaymentTypeCode = (int)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWACharge;
                }
                else if (obj_MiningNewSADApplication != null && obj_MiningNewSADApplication.CreatedByExUC > 0)
                {
                    obj_GroundWaterChargeRec.AttachmentCode = obj_MiningNewSADApplication.AbsRestChargeAttCode;
                    if (AreaTypeCategoryCode(null, null, obj_MiningNewSADApplication) == 5)
                        obj_GroundWaterChargeRec.PaymentTypeCode = (int)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWRCharge;
                    else
                        obj_GroundWaterChargeRec.PaymentTypeCode = (int)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWACharge;
                }
                else if (obj_MiningRenewSADApplication != null && obj_MiningRenewSADApplication.CreatedByExUC > 0)
                {
                    obj_GroundWaterChargeRec.AttachmentCode = obj_MiningRenewSADApplication.AbsRestChargeAttCode;
                    if (AreaTypeCategoryCode(null, null, null, obj_MiningRenewSADApplication) == 5)
                        obj_GroundWaterChargeRec.PaymentTypeCode = (int)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWRCharge;
                    else
                        obj_GroundWaterChargeRec.PaymentTypeCode = (int)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWACharge;
                }
                if (obj_GroundWaterChargeRec.Add() == 1)
                {
                    strStatus = "File Upload Success";
                    lblMessageBharatKoshReciept.ForeColor = Color.Green;
                    BindChargesReceivedGWCharges();
                }
                else
                {
                    strStatus = "File Upload Failed";
                    lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
                }
                lblMessageBharatKoshReciept.Text = obj_GroundWaterChargeRec.CustumMessage;
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
            lblMessageBharatKoshReciept.Text = "Maximum number of files to be uploaded is 5";
            lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
        }



    }
    private void UploadBharatKoshRecieptPenaltySAD()
    {

        hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
        Session["CSRF"] = hidCSRF.Value;
        strActionName = "File Abstraction Charges";
        NOCAP.BLL.PenaltyCorrectionCharges.SADPenaltyCorrectionChargesRec obj_SADPenaltyCorrectionChargesRec = new NOCAP.BLL.PenaltyCorrectionCharges.SADPenaltyCorrectionChargesRec();
        NOCAP.BLL.PenaltyCorrectionCharges.SADPenaltyCorrectionChargesRec obj_SADPenaltyCorrectionChargesRecForNoLimit = new NOCAP.BLL.PenaltyCorrectionCharges.SADPenaltyCorrectionChargesRec();
        obj_SADPenaltyCorrectionChargesRecForNoLimit.ApplicationCode = Convert.ToInt64(lblAppCode.Text);
        NOCAP.BLL.PenaltyCorrectionCharges.SADPenaltyCorrectionChargesRec[] arr_SADPenaltyCorrectionChargesRecList;
        obj_SADPenaltyCorrectionChargesRecForNoLimit.GetAll();
        arr_SADPenaltyCorrectionChargesRecList = obj_SADPenaltyCorrectionChargesRecForNoLimit.PCorrectChargesRecCollection;
        if (arr_SADPenaltyCorrectionChargesRecList.Count() < AttachmentNumberLimit())
        {
            string str_fname;
            string str_ext;
            string str_newFileNameWithPath = "";

            byte[] buffer = new byte[1];
            try
            {
                if (FileUploadPenaltyINDNewSAD.HasFile)
                {
                    str_ext = System.IO.Path.GetExtension(FileUploadPenaltyINDNewSAD.PostedFile.FileName).ToLower();
                    str_fname = FileUploadPenaltyINDNewSAD.FileName;
                    if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                    {
                        if (NOCAPExternalUtility.IsValidFile(FileUploadPenaltyINDNewSAD.PostedFile))
                        {
                            if (FileUploadPenaltyINDNewSAD.PostedFile.ContentLength < AttachmentSizeLimit())
                            {

                                obj_SADPenaltyCorrectionChargesRec.AttachmentFile = (NOCAPExternalUtility.StreamFile(FileUploadPenaltyINDNewSAD.PostedFile));
                                obj_SADPenaltyCorrectionChargesRec.ContentType = FileUploadPenaltyINDNewSAD.PostedFile.ContentType;
                                obj_SADPenaltyCorrectionChargesRec.FileExtension = str_ext;
                                obj_SADPenaltyCorrectionChargesRec.AttPath = FileUploadPenaltyINDNewSAD.FileName;
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


                obj_SADPenaltyCorrectionChargesRec.BharatTransReferanceNumber = Convert.ToInt64(txtBharatKoshRefferenceNoPenalty.Text.Trim());
                obj_SADPenaltyCorrectionChargesRec.BharatTransDated = Convert.ToDateTime(txtBharatKoshDatedPenalty.Text);
                obj_SADPenaltyCorrectionChargesRec.Amount = Convert.ToDecimal(txtPenaltyFinally.Text);

                obj_SADPenaltyCorrectionChargesRec.BharatPayStatus = NOCAP.BLL.Common.CommonEnum.PCorrectChargePaymentStatusOption.Success;



                obj_SADPenaltyCorrectionChargesRec.ApplicationCode = Convert.ToInt32(lblAppCode.Text);


                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                obj_SADPenaltyCorrectionChargesRec.CreatedByUserCode = (int)obj_externalUser.ExternalUserCode;
                obj_SADPenaltyCorrectionChargesRec.AttName = txtPenaltyINDNewSAD.Text.Trim();


                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = null;
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewSADApplication = null;
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewSADApplication = null;
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewSADApplication = null;
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewSADApplication = null;
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewSADApplication = null;

                NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_IndustrialNewSADApplication, out obj_InfrastructureNewSADApplication, out obj_MiningNewSADApplication,
                    out obj_IndustrialRenewSADApplication, out obj_InfrastructureRenewSADApplication, out obj_MiningRenewSADApplication, Convert.ToInt64(lblAppCode.Text));
                if (obj_IndustrialNewSADApplication != null && obj_IndustrialNewSADApplication.CreatedByExUC > 0)
                    obj_SADPenaltyCorrectionChargesRec.AttachmentCode = obj_IndustrialNewSADApplication.PenaltyAttCode;
                else if (obj_IndustrialRenewSADApplication != null && obj_IndustrialRenewSADApplication.CreatedByExUC > 0)
                    obj_SADPenaltyCorrectionChargesRec.AttachmentCode = obj_IndustrialRenewSADApplication.PenaltyAttCode;
                else if (obj_InfrastructureNewSADApplication != null && obj_InfrastructureNewSADApplication.CreatedByExUC > 0)
                    obj_SADPenaltyCorrectionChargesRec.AttachmentCode = obj_InfrastructureNewSADApplication.PenaltyAttCode;
                else if (obj_InfrastructureRenewSADApplication != null && obj_InfrastructureRenewSADApplication.CreatedByExUC > 0)
                    obj_SADPenaltyCorrectionChargesRec.AttachmentCode = obj_InfrastructureRenewSADApplication.PenaltyAttCode;
                else if (obj_MiningNewSADApplication != null && obj_MiningNewSADApplication.CreatedByExUC > 0)
                    obj_SADPenaltyCorrectionChargesRec.AttachmentCode = obj_MiningNewSADApplication.PenaltyAttCode;
                else if (obj_MiningRenewSADApplication != null && obj_MiningRenewSADApplication.CreatedByExUC > 0)
                    obj_SADPenaltyCorrectionChargesRec.AttachmentCode = obj_MiningRenewSADApplication.PenaltyAttCode;

                if (obj_SADPenaltyCorrectionChargesRec.Add() == 1)
                {
                    strStatus = "File Upload Success";
                    lblMessageBharatKoshReciept.ForeColor = Color.Green;
                    BindChargesReceivedPenalty();
                }
                else
                {
                    strStatus = "File Upload Failed";
                    lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
                }
                lblMessageBharatKoshReciept.Text = obj_SADPenaltyCorrectionChargesRec.CustumMessage;
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
            lblMessageBharatKoshReciept.Text = "Maximum number of files to be uploaded is 5";
            lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
        }



    }


    private int DeleteAttchmentAppFee(GridView gv, GridViewDeleteEventArgs e, Label lblMessage, string str_ActionName, int int_AttachmentCode)
    {
        try
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus obj_SADProcessingFeeStatus = new NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus();
            obj_SADProcessingFeeStatus.ApplicationCode = Convert.ToInt64(lblApplicationCodeFrom.Text);
            obj_SADProcessingFeeStatus.AttachmentCode = int_AttachmentCode;
            obj_SADProcessingFeeStatus.SN = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["SN"]);
            if (obj_SADProcessingFeeStatus.Delete() == 1)
            {
                lblMessage.Text = obj_SADProcessingFeeStatus.CustumMessage;
                strStatus = "File Delete Success";
                return 1;
            }
            else
            {
                lblMessage.Text = obj_SADProcessingFeeStatus.CustumMessage;
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
    private void DeleteAttchmentGWCharge(GridView gv, GridViewDeleteEventArgs e, Label lblMessage, string str_ActionName, int int_AttachmentCode)
    {
        try
        {
            NOCAP.BLL.GroundWaterChargeRec.SADGroundWaterChargesRec obj_GroundWaterChargeRec = new NOCAP.BLL.GroundWaterChargeRec.SADGroundWaterChargesRec();
            obj_GroundWaterChargeRec.ApplicationCode = Convert.ToInt32(gvGWCharges.DataKeys[e.RowIndex].Values["ApplicationCode"]);
            obj_GroundWaterChargeRec.SN = Convert.ToInt32(gvGWCharges.DataKeys[e.RowIndex].Values["SN"]);
            obj_GroundWaterChargeRec.AttachmentCode = int_AttachmentCode;
            if (obj_GroundWaterChargeRec.Delete() == 1)
            {
                strStatus = "Record Delete Successfully !";
                lblMessageBharatKoshReciept.Text = HttpUtility.HtmlEncode(obj_GroundWaterChargeRec.CustumMessage);
                lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Green;
                Clear();
                BindChargesReceivedGWCharges();
            }
            else
            {
                strStatus = "Record Delete Failed !";
                lblMessageBharatKoshReciept.Text = HttpUtility.HtmlEncode(obj_GroundWaterChargeRec.CustumMessage);
                lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        { }
    }
    private void DeleteAttchmentPenalty(GridView gv, GridViewDeleteEventArgs e, Label lblMessage, string str_ActionName, int int_AttachmentCode)
    {
        try
        {
            NOCAP.BLL.PenaltyCorrectionCharges.SADPenaltyCorrectionChargesRec obj_SADPenaltyCorrectionChargesRec = new NOCAP.BLL.PenaltyCorrectionCharges.SADPenaltyCorrectionChargesRec();
            obj_SADPenaltyCorrectionChargesRec.ApplicationCode = Convert.ToInt32(gvPenalty.DataKeys[e.RowIndex].Values["ApplicationCode"]);
            obj_SADPenaltyCorrectionChargesRec.PenaltySN = Convert.ToInt32(gvPenalty.DataKeys[e.RowIndex].Values["SN"]);
            obj_SADPenaltyCorrectionChargesRec.SN = Convert.ToInt32(gvPenalty.DataKeys[e.RowIndex].Values["SN"]);
            obj_SADPenaltyCorrectionChargesRec.AttachmentCode = int_AttachmentCode;
            if (obj_SADPenaltyCorrectionChargesRec.Delete() == 1)
            {
                strStatus = "Record Delete Successfully !";
                lblMessageBharatKoshReciept.Text = HttpUtility.HtmlEncode(obj_SADPenaltyCorrectionChargesRec.CustumMessage);
                lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Green;
                Clear();
                BindChargesReceivedPenalty();
            }
            else
            {
                strStatus = "Record Delete Failed !";
                lblMessageBharatKoshReciept.Text = HttpUtility.HtmlEncode(obj_SADPenaltyCorrectionChargesRec.CustumMessage);
                lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        { }
    }


    private void BindChargesReceivedGWCharges(string str_sortfieldName = "")
    {
        try
        {
            NOCAP.BLL.GroundWaterChargeRec.SADGroundWaterChargesRec obj_GroundWaterChargeRecBLL = new NOCAP.BLL.GroundWaterChargeRec.SADGroundWaterChargesRec();
            int int_status = 0;
            obj_GroundWaterChargeRecBLL.ApplicationCode = Convert.ToInt64(lblAppCode.Text);
            switch (str_sortfieldName)
            {
                case "CreatedOn":
                    int_status = obj_GroundWaterChargeRecBLL.GetAll(NOCAP.BLL.GroundWaterChargeRec.SADGroundWaterChargesRec.SortingField.CreatedOn);
                    break;
                case "DDDated":
                    int_status = obj_GroundWaterChargeRecBLL.GetAll(NOCAP.BLL.GroundWaterChargeRec.SADGroundWaterChargesRec.SortingField.DDDated);
                    break;
                case "SN":
                    int_status = obj_GroundWaterChargeRecBLL.GetAll(NOCAP.BLL.GroundWaterChargeRec.SADGroundWaterChargesRec.SortingField.SN);
                    break;
                case "":
                    int_status = obj_GroundWaterChargeRecBLL.GetAll(NOCAP.BLL.GroundWaterChargeRec.SADGroundWaterChargesRec.SortingField.NoSorting);
                    break;
                default:
                    int_status = obj_GroundWaterChargeRecBLL.GetAll(NOCAP.BLL.GroundWaterChargeRec.SADGroundWaterChargesRec.SortingField.NoSorting);
                    break;
            }
            NOCAP.BLL.GroundWaterChargeRec.SADGroundWaterChargesRec[] arr_GroundWaterChargesRec;
            arr_GroundWaterChargesRec = obj_GroundWaterChargeRecBLL.SADGroundWaterChargesRecCollection;


            if (int_status == 1)
            {
                gvGWCharges.DataSource = arr_GroundWaterChargesRec;
                gvGWCharges.DataBind();
            }
            else
            {
                lblMessageBharatKoshReciept.Text = HttpUtility.HtmlEncode(obj_GroundWaterChargeRecBLL.CustumMessage);
                lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally
        {

        }
    }
    private void BindAppFeeReceived(string str_sortfieldName = "")
    {
        try
        {
            NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus obj_GroundWaterChargeRecBLL = new NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus();
            int int_status = 0;
            obj_GroundWaterChargeRecBLL.ApplicationCode = Convert.ToInt64(lblAppCode.Text);
            switch (str_sortfieldName)
            {
                case "CreatedOn":
                    int_status = obj_GroundWaterChargeRecBLL.GetAll(NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus.SortingField.CreatedOn);
                    break;
                case "DDDated":
                    int_status = obj_GroundWaterChargeRecBLL.GetAll(NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus.SortingField.DDDated);
                    break;
                case "SN":
                    int_status = obj_GroundWaterChargeRecBLL.GetAll(NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus.SortingField.SN);
                    break;
                case "":
                    int_status = obj_GroundWaterChargeRecBLL.GetAll(NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus.SortingField.NoSorting);
                    break;
                default:
                    int_status = obj_GroundWaterChargeRecBLL.GetAll(NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus.SortingField.NoSorting);
                    break;
            }
            NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus[] arr_GroundWaterChargesRec;
            arr_GroundWaterChargesRec = obj_GroundWaterChargeRecBLL.ProcessingFeeStatusCollection;


            if (int_status == 1)
            {
                gvApplicationFee.DataSource = arr_GroundWaterChargesRec;
                gvApplicationFee.DataBind();
            }
            else
            {
                lblMessageBharatKoshReciept.Text = HttpUtility.HtmlEncode(obj_GroundWaterChargeRecBLL.CustumMessage);
                lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally
        {

        }
    }

    private void BindGvDoubleTaxPay(string str_sortfieldName = "")
    {
        try
        {
            decimal AmountForDoubleTax = 0;

            DoubleTaxPayment obj_DoubleTaxPaymentBLL = new DoubleTaxPayment();

            obj_DoubleTaxPaymentBLL.ApplicationCode = Convert.ToInt64(lblAppCode.Text);
            DoubleTaxPayment[] arr_DoubleTaxPayment;

            arr_DoubleTaxPayment = obj_DoubleTaxPaymentBLL.GetDoubleTaxPaymentListForAppCodeList(Convert.ToInt64(lblAppCode.Text), DoubleTaxPayment.SortingField.NoSorting);

            if (arr_DoubleTaxPayment != null)
            {
                for (int i = 0; i < arr_DoubleTaxPayment.Length; i++)
                {
                    AmountForDoubleTax += Convert.ToDecimal(arr_DoubleTaxPayment[i].StatePaidAmt);
                }

                if (AmountForDoubleTax > Convert.ToDecimal(txtGWCharge.Text))
                {
                    pnBharatkoshGWAC.Visible = false;
                }
                else
                {
                    pnBharatkoshGWAC.Visible = true;
                }


                gvDoubleTax.DataSource = arr_DoubleTaxPayment;
                gvDoubleTax.DataBind();
            }
            else
            {
                LblDoubleTax.Text = HttpUtility.HtmlEncode(obj_DoubleTaxPaymentBLL.CustumMessage);
                LblDoubleTax.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally
        {

        }
    }
    private void Clear()
    {
        txtBhartRefAppFee.Text = ""; txtBhartDateAppFee.Text = ""; txtBharatKoshReciept.Text = "";
        txtPenaltyFinally.Text = ""; txtBharatKoshRefferenceNoPenalty.Text = ""; txtBharatKoshDatedPenalty.Text = ""; txtPenaltyINDNewSAD.Text = "";
        txtGWChargeAmountFinally.Text = ""; txtGWArearAmountFinally.Text = ""; txtBharatKoshRefferenceNoGWCharges.Text = ""; txtBharatKoshDatedGWCharges.Text = ""; txtAbstChargeINDRenewSAD.Text = "";
        txtStatePaidAmt.Text = ""; TxtPaidToAgancy.Text = ""; txtAttNameDoubleTax.Text = "";
    }
    #endregion

    #region Button Click

    protected void PayBtn_Click(object sender, EventArgs e)
    {

        strActionName = "Combined Online Payment for All";
        strStatus = "Start Payment";
        int int_areaTypeCategoryCode = 0;
        bool WaterQualityCode = true;
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = null;
        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewSADApplication = null;
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewSADApplication = null;
        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewSADApplication = null;
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewSADApplication = null;
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewSADApplication = null;

        NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_IndustrialNewSADApplication, out obj_InfrastructureNewSADApplication, out obj_MiningNewSADApplication,
            out obj_IndustrialRenewSADApplication, out obj_InfrastructureRenewSADApplication, out obj_MiningRenewSADApplication, Convert.ToInt64(lblApplicationCodeFrom.Text));

        if (obj_IndustrialNewSADApplication != null && obj_IndustrialNewSADApplication.CreatedByExUC > 0 && obj_IndustrialNewSADApplication.WaterQualityCode == 1)
        {
            WaterQualityCode = true;
            int_areaTypeCategoryCode = AreaTypeCategoryCode(obj_IndustrialNewSADApplication);
        }
        else if (obj_IndustrialRenewSADApplication != null && obj_IndustrialRenewSADApplication.CreatedByExUC > 0 && obj_IndustrialRenewSADApplication.WaterQualityCode == 1)
        {
            WaterQualityCode = true;
            int_areaTypeCategoryCode = AreaTypeCategoryCode(null, obj_IndustrialRenewSADApplication);
        }
        else if (obj_MiningNewSADApplication != null && obj_MiningNewSADApplication.CreatedByExUC > 0 && obj_MiningNewSADApplication.WaterQualityCode == 1)
        {
            WaterQualityCode = true;
            int_areaTypeCategoryCode = AreaTypeCategoryCode(null, null, obj_MiningNewSADApplication);
        }
        else if (obj_MiningRenewSADApplication != null && obj_MiningRenewSADApplication.CreatedByExUC > 0 && obj_MiningRenewSADApplication.WaterQualityCode == 1)
        {
            WaterQualityCode = true;
            int_areaTypeCategoryCode = AreaTypeCategoryCode(null, null, null, obj_MiningRenewSADApplication);
        }
        else if (obj_InfrastructureNewSADApplication != null && obj_InfrastructureNewSADApplication.CreatedByExUC > 0 && obj_InfrastructureNewSADApplication.WaterQualityCode == 1)
        {
            WaterQualityCode = true;
            int_areaTypeCategoryCode = AreaTypeCategoryCode(null, null, null, null, obj_InfrastructureNewSADApplication);
        }

        else if (obj_InfrastructureRenewSADApplication != null && obj_InfrastructureRenewSADApplication.CreatedByExUC > 0 && obj_InfrastructureRenewSADApplication.WaterQualityCode == 1)
        {
            WaterQualityCode = true;
            int_areaTypeCategoryCode = AreaTypeCategoryCode(null, null, null, null, null, obj_InfrastructureRenewSADApplication);
        }
        else
            WaterQualityCode = false;

        Dictionary<int, decimal> dict = new Dictionary<int, decimal>();
        dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.ProcFee, Convert.ToDecimal(lblFeeAmout.Text));
        if (WaterQualityCode)
        {
            if (int_areaTypeCategoryCode == 5)
                dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWRCharge, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)) + (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)));
            else
                dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWACharge, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)) + (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)));
        }
        if (lblPenaltyType.Text != "")
            dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.Penalty, Convert.ToDecimal(lblPenaltyAmount.Text));

        if (obj_IndustrialNewSADApplication != null && obj_IndustrialNewSADApplication.CreatedByExUC > 0)
            CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), obj_IndustrialNewSADApplication, null, null, null, null, null, strActionName, strStatus);
        else if (obj_IndustrialRenewSADApplication != null && obj_IndustrialRenewSADApplication.CreatedByExUC > 0)
            CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), null, obj_IndustrialRenewSADApplication, null, null, null, null, strActionName, strStatus);
        else if (obj_MiningNewSADApplication != null && obj_MiningNewSADApplication.CreatedByExUC > 0)
            CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), null, null, obj_MiningNewSADApplication, null, null, null, strActionName, strStatus);
        else if (obj_MiningRenewSADApplication != null && obj_MiningRenewSADApplication.CreatedByExUC > 0)
            CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), null, null, null, obj_MiningRenewSADApplication, null, null, strActionName, strStatus);
        else if (obj_InfrastructureNewSADApplication != null && obj_InfrastructureNewSADApplication.CreatedByExUC > 0)
            CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), null, null, null, null, obj_InfrastructureNewSADApplication, null, strActionName, strStatus);
        else if (obj_InfrastructureRenewSADApplication != null && obj_InfrastructureRenewSADApplication.CreatedByExUC > 0)
            CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), null, null, null, null, null, obj_InfrastructureRenewSADApplication, strActionName, strStatus);

    }


    protected void btnAppFee_Click(object sender, EventArgs e)
    {

        strActionName = "Single Payment for Application Fee-" + (rdbtnAppFee.SelectedValue == "0" ? "NEFT/RTGS" : "Online");
        strStatus = "Start Payment";
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = null;
        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewSADApplication = null;
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewSADApplication = null;
        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewSADApplication = null;
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewSADApplication = null;
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewSADApplication = null;

        NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_IndustrialNewSADApplication, out obj_InfrastructureNewSADApplication, out obj_MiningNewSADApplication,
            out obj_IndustrialRenewSADApplication, out obj_InfrastructureRenewSADApplication, out obj_MiningRenewSADApplication, Convert.ToInt64(lblApplicationCodeFrom.Text));


        Dictionary<int, decimal> dict = new Dictionary<int, decimal>();
        dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.ProcFee, Convert.ToDecimal(lblOfflineFeeAmout.Text));
        if (rdbtnAppFee.SelectedValue == "1")
        {
            if (obj_IndustrialNewSADApplication != null && obj_IndustrialNewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, obj_IndustrialNewSADApplication, null, null, null, null, null, strActionName, strStatus);
            else if (obj_IndustrialRenewSADApplication != null && obj_IndustrialRenewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, obj_IndustrialRenewSADApplication, null, null, null, null, strActionName, strStatus);
            else if (obj_MiningNewSADApplication != null && obj_MiningNewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, obj_MiningNewSADApplication, null, null, null, strActionName, strStatus);
            else if (obj_MiningRenewSADApplication != null && obj_MiningRenewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, obj_MiningRenewSADApplication, null, null, strActionName, strStatus);
            else if (obj_InfrastructureNewSADApplication != null && obj_InfrastructureNewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, null, obj_InfrastructureNewSADApplication, null, strActionName, strStatus);
            else if (obj_InfrastructureRenewSADApplication != null && obj_InfrastructureRenewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, null, null, obj_InfrastructureRenewSADApplication, strActionName, strStatus);

        }
        else
        {
            if (obj_IndustrialNewSADApplication != null && obj_IndustrialNewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, obj_IndustrialNewSADApplication, null, null, null, null, null, strActionName, strStatus);
            else if (obj_IndustrialRenewSADApplication != null && obj_IndustrialRenewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, obj_IndustrialRenewSADApplication, null, null, null, null, strActionName, strStatus);
            else if (obj_MiningNewSADApplication != null && obj_MiningNewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, obj_MiningNewSADApplication, null, null, null, strActionName, strStatus);
            else if (obj_MiningRenewSADApplication != null && obj_MiningRenewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, obj_MiningRenewSADApplication, null, null, strActionName, strStatus);
            else if (obj_InfrastructureNewSADApplication != null && obj_InfrastructureNewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, null, obj_InfrastructureNewSADApplication, null, strActionName, strStatus);
            else if (obj_InfrastructureRenewSADApplication != null && obj_InfrastructureRenewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, null, null, obj_InfrastructureRenewSADApplication, strActionName, strStatus);

        }


    }

    protected void btnCharge_Click(object sender, EventArgs e)
    {
        strActionName = "Single Payment for Charge-" + (rdbtnCharge.SelectedValue == "0" ? "NEFT/RTGS" : "Online");
        strStatus = "Start Payment";
        int int_areaTypeCategoryCode = 0;
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = null;
        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewSADApplication = null;
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewSADApplication = null;
        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewSADApplication = null;
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewSADApplication = null;
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewSADApplication = null;

        NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_IndustrialNewSADApplication, out obj_InfrastructureNewSADApplication, out obj_MiningNewSADApplication,
            out obj_IndustrialRenewSADApplication, out obj_InfrastructureRenewSADApplication, out obj_MiningRenewSADApplication, Convert.ToInt64(lblApplicationCodeFrom.Text));

        if (obj_IndustrialNewSADApplication != null && obj_IndustrialNewSADApplication.CreatedByExUC > 0)
            int_areaTypeCategoryCode = AreaTypeCategoryCode(obj_IndustrialNewSADApplication);
        else if (obj_IndustrialRenewSADApplication != null && obj_IndustrialRenewSADApplication.CreatedByExUC > 0)
            int_areaTypeCategoryCode = AreaTypeCategoryCode(null, obj_IndustrialRenewSADApplication);
        else if (obj_MiningNewSADApplication != null && obj_MiningNewSADApplication.CreatedByExUC > 0)
            int_areaTypeCategoryCode = AreaTypeCategoryCode(null, null, obj_MiningNewSADApplication);

        else if (obj_MiningRenewSADApplication != null && obj_MiningRenewSADApplication.CreatedByExUC > 0)
            int_areaTypeCategoryCode = AreaTypeCategoryCode(null, null, null, obj_MiningRenewSADApplication);

        else if (obj_InfrastructureNewSADApplication != null && obj_InfrastructureNewSADApplication.CreatedByExUC > 0)
            int_areaTypeCategoryCode = AreaTypeCategoryCode(null, null, null, null, obj_InfrastructureNewSADApplication);

        else if (obj_InfrastructureRenewSADApplication != null && obj_InfrastructureRenewSADApplication.CreatedByExUC > 0)
            int_areaTypeCategoryCode = AreaTypeCategoryCode(null, null, null, null, null, obj_InfrastructureRenewSADApplication);
        Dictionary<int, decimal> dict = new Dictionary<int, decimal>();
        if (int_areaTypeCategoryCode == 5)
            dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWRCharge, (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)) + (txtOffGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtOffGWCharge.Text)));
        else
            dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWACharge, (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)) + (txtOffGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtOffGWCharge.Text)));
        if (rdbtnCharge.SelectedValue == "1")
        {
            if (obj_IndustrialNewSADApplication != null && obj_IndustrialNewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtOffGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtOffGWCharge.Text)), (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)), obj_IndustrialNewSADApplication, null, null, null, null, null, strActionName, strStatus);
            else if (obj_IndustrialRenewSADApplication != null && obj_IndustrialRenewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtOffGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtOffGWCharge.Text)), (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)), null, obj_IndustrialRenewSADApplication, null, null, null, null, strActionName, strStatus);
            else if (obj_MiningNewSADApplication != null && obj_MiningNewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtOffGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtOffGWCharge.Text)), (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)), null, null, obj_MiningNewSADApplication, null, null, null, strActionName, strStatus);
            else if (obj_MiningRenewSADApplication != null && obj_MiningRenewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtOffGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtOffGWCharge.Text)), (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)), null, null, null, obj_MiningRenewSADApplication, null, null, strActionName, strStatus);
            else if (obj_InfrastructureNewSADApplication != null && obj_InfrastructureNewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtOffGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtOffGWCharge.Text)), (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)), null, null, null, null, obj_InfrastructureNewSADApplication, null, strActionName, strStatus);
            else if (obj_InfrastructureRenewSADApplication != null && obj_InfrastructureRenewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtOffGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtOffGWCharge.Text)), (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)), null, null, null, null, null, obj_InfrastructureRenewSADApplication, strActionName, strStatus);

        }
        else
        {
            if (obj_IndustrialNewSADApplication != null && obj_IndustrialNewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtOffGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtOffGWCharge.Text)), (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)), obj_IndustrialNewSADApplication, null, null, null, null, null, strActionName, strStatus);
            else if (obj_IndustrialRenewSADApplication != null && obj_IndustrialRenewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtOffGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtOffGWCharge.Text)), (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)), null, obj_IndustrialRenewSADApplication, null, null, null, null, strActionName, strStatus);
            else if (obj_MiningNewSADApplication != null && obj_MiningNewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtOffGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtOffGWCharge.Text)), (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)), null, null, obj_MiningNewSADApplication, null, null, null, strActionName, strStatus);
            else if (obj_MiningRenewSADApplication != null && obj_MiningRenewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtOffGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtOffGWCharge.Text)), (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)), null, null, null, obj_MiningRenewSADApplication, null, null, strActionName, strStatus);
            else if (obj_InfrastructureNewSADApplication != null && obj_InfrastructureNewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtOffGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtOffGWCharge.Text)), (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)), null, null, null, null, obj_InfrastructureNewSADApplication, null, strActionName, strStatus);
            else if (obj_InfrastructureRenewSADApplication != null && obj_InfrastructureRenewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtOffGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtOffGWCharge.Text)), (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)), null, null, null, null, null, obj_InfrastructureRenewSADApplication, strActionName, strStatus);
            strStatus = "Start Payment";

        }

    }

    protected void btnPenalty_Click(object sender, EventArgs e)
    {
        strActionName = "Single Payment for Penalty-" + (rdbtnPenalty.SelectedValue == "0" ? "NEFT/RTGS" : "Online");
        strStatus = "Start Payment";
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = null;
        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewSADApplication = null;
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewSADApplication = null;
        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewSADApplication = null;
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewSADApplication = null;
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewSADApplication = null;

        NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_IndustrialNewSADApplication, out obj_InfrastructureNewSADApplication, out obj_MiningNewSADApplication,
            out obj_IndustrialRenewSADApplication, out obj_InfrastructureRenewSADApplication, out obj_MiningRenewSADApplication, Convert.ToInt64(lblApplicationCodeFrom.Text));

        Dictionary<int, decimal> dict = new Dictionary<int, decimal>();
        if (lblPenaltyType.Text != "")
            dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.Penalty, Convert.ToDecimal(lblOfflinePenaltyAmount.Text));
        if (rdbtnPenalty.SelectedValue == "1")
        {
            if (obj_IndustrialNewSADApplication != null && obj_IndustrialNewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, obj_IndustrialNewSADApplication, null, null, null, null, null, strActionName, strStatus);
            else if (obj_IndustrialRenewSADApplication != null && obj_IndustrialRenewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, obj_IndustrialRenewSADApplication, null, null, null, null, strActionName, strStatus);
            else if (obj_MiningNewSADApplication != null && obj_MiningNewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, obj_MiningNewSADApplication, null, null, null, strActionName, strStatus);
            else if (obj_MiningRenewSADApplication != null && obj_MiningRenewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, obj_MiningRenewSADApplication, null, null, strActionName, strStatus);
            else if (obj_InfrastructureNewSADApplication != null && obj_InfrastructureNewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, null, obj_InfrastructureNewSADApplication, null, strActionName, strStatus);
            else if (obj_InfrastructureRenewSADApplication != null && obj_InfrastructureRenewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, null, null, obj_InfrastructureRenewSADApplication, strActionName, strStatus);

        }
        else
        {

            if (obj_IndustrialNewSADApplication != null && obj_IndustrialNewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, obj_IndustrialNewSADApplication, null, null, null, null, null, strActionName, strStatus);
            else if (obj_IndustrialRenewSADApplication != null && obj_IndustrialRenewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, obj_IndustrialRenewSADApplication, null, null, null, null, strActionName, strStatus);
            else if (obj_MiningNewSADApplication != null && obj_MiningNewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, obj_MiningNewSADApplication, null, null, null, strActionName, strStatus);
            else if (obj_MiningRenewSADApplication != null && obj_MiningRenewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, obj_MiningRenewSADApplication, null, null, strActionName, strStatus);
            else if (obj_InfrastructureNewSADApplication != null && obj_InfrastructureNewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, null, obj_InfrastructureNewSADApplication, null, strActionName, strStatus);
            else if (obj_InfrastructureRenewSADApplication != null && obj_InfrastructureRenewSADApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, null, null, obj_InfrastructureRenewSADApplication, strActionName, strStatus);
            strStatus = "Start Payment";
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
                UploadBharatKoshRecieptApplicationFee();
                Clear();
            }
        }
    }

    protected void btnUploadAbstCharge_Click(object sender, EventArgs e)
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

                UploadBharatKoshRecieptAbstChargeSAD();
                Clear();
            }
        }
    }

    protected void btnUploadPenalty_Click(object sender, EventArgs e)
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
                strActionName = "File Upload Bharat Kosh Reciept for penalty";
                UploadBharatKoshRecieptPenaltySAD();

                Clear();
            }
        }








    }
    protected void btnUploadDoubleTaxPay_Click(object sender, EventArgs e)
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
                // strActionName = "File Upload Bharat Kosh Reciept for penalty";
                UploadDoubleTaxPayment();
                BindGvDoubleTaxPay();
                Clear();
            }
        }


    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {
                string str_Next = string.Empty;
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                strActionName = "Transfer Ready To Submit";
                //long lng_submittedApplicationCode = 0;
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewSADApplication = null;
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewSADApplication = null;
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewSADApplication = null;
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplication = null;
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_miningNewSADApplication = null;
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewSADApplication = null;
                NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_industrialNewSADApplication, out obj_infrastructureNewSADApplication, out obj_miningNewSADApplication, out obj_industrialRenewSADApplication, out obj_infrastructureRenewSADApplication, out obj_miningRenewSADApplication, Convert.ToInt64(lblAppCode.Text));


                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                try
                {
                    string ErrorMessage = string.Empty;

                    string str_msg = string.Empty;
                    if (obj_industrialNewSADApplication != null && obj_industrialNewSADApplication.CreatedByExUC > 0)
                    {
                        #region IND New SAD

                        str_Next = "IndustrialNew/Submit.aspx";
                        obj_industrialNewSADApplication.SetPaymentTypeMode(Convert.ToInt64(lblAppCode.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.NotDefined, out str_msg);

                        if (lblBharatKosh2.Visible)
                        {
                            if (obj_industrialNewSADApplication.GetBharatKoshRecieptAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Bharat Kosh Reciept" : ErrorMessage + ",Bharat Kosh Reciept"; }
                        }

                        if (lblAbstChargeINDRenewSAD.Visible)
                        {
                            if (obj_industrialNewSADApplication.GetAbsRestChargeAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Bharatkosh reciept (Ground Water Abstraction Charges)" : ErrorMessage + ",Bharatkosh reciept (Ground Water Abstraction Charges)"; }
                        }
                        #endregion

                    }
                    else if (obj_industrialRenewSADApplication != null && obj_industrialRenewSADApplication.CreatedByExUC > 0)
                    {
                        #region IND Renew SAD
                        str_Next = "IndustrialRenew/Submit.aspx";
                        obj_industrialRenewSADApplication.SetPaymentTypeMode(Convert.ToInt64(lblAppCode.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.NotDefined, out str_msg);
                        if (lblBharatKosh2.Visible)
                        {
                            if (obj_industrialRenewSADApplication.GetBharatKoshRecieptAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Bharat Kosh Reciept" : ErrorMessage + ",Bharat Kosh Reciept"; }
                        }


                        if (lblAbstChargeINDRenewSAD.Visible)
                        {
                            if (obj_industrialRenewSADApplication.GetAbsRestChargeAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Bharatkosh reciept (Ground Water Abstraction Charges)" : ErrorMessage + ",Bharatkosh reciept (Ground Water Abstraction Charges)"; }
                        }


                        #endregion

                    }
                    else if (obj_infrastructureNewSADApplication != null && obj_infrastructureNewSADApplication.CreatedByExUC > 0)
                    {
                        #region INF New SAD
                        str_Next = "InfrastructureNew/Submit.aspx";
                        obj_infrastructureNewSADApplication.SetPaymentTypeMode(Convert.ToInt64(lblAppCode.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.NotDefined, out str_msg);

                        if (lblBharatKosh2.Visible)
                        {
                            if (obj_infrastructureNewSADApplication.GetBharatKoshRecieptAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Bharat Kosh Reciept" : ErrorMessage + ",Bharat Kosh Reciept"; }
                        }


                        if (lblAbstChargeINDRenewSAD.Visible)
                        {
                            if (obj_infrastructureNewSADApplication.GetAbsRestChargeAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Bharatkosh reciept (Ground Water Abstraction Charges)" : ErrorMessage + ",Bharatkosh reciept (Ground Water Abstraction Charges)"; }
                        }

                        #endregion

                    }
                    else if (obj_infrastructureRenewSADApplication != null && obj_infrastructureRenewSADApplication.CreatedByExUC > 0)
                    {
                        #region INF Renew SAD
                        str_Next = "InfrastructureRenew/Submit.aspx";
                        obj_infrastructureRenewSADApplication.SetPaymentTypeMode(Convert.ToInt64(lblAppCode.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.NotDefined, out str_msg);

                        if (lblBharatKosh2.Visible)
                        {
                            if (obj_infrastructureRenewSADApplication.GetBharatKoshRecieptAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Bharat Kosh Reciept" : ErrorMessage + ",Bharat Kosh Reciept"; }
                        }


                        if (lblAbstChargeINDRenewSAD.Visible)
                        {
                            if (obj_infrastructureRenewSADApplication.GetAbsRestChargeAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Bharatkosh reciept (Ground Water Abstraction Charges)" : ErrorMessage + ",Bharatkosh reciept (Ground Water Abstraction Charges)"; }
                        }

                        #endregion

                    }
                    else if (obj_miningNewSADApplication != null && obj_miningNewSADApplication.CreatedByExUC > 0)
                    {

                        #region MIN New SAD
                        str_Next = "MiningNew/Submit.aspx";
                        obj_miningNewSADApplication.SetPaymentTypeMode(Convert.ToInt64(lblAppCode.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.NotDefined, out str_msg);



                        if (lblBharatKosh2.Visible)
                        {
                            if (obj_miningNewSADApplication.GetBharatKoshRecieptAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Bharat Kosh Reciept" : ErrorMessage + ",Bharat Kosh Reciept"; }
                        }


                        if (lblAbstChargeINDRenewSAD.Visible)
                        {
                            if (obj_miningNewSADApplication.GetAbsRestChargeAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Bharatkosh reciept (Ground Water Abstraction Charges)" : ErrorMessage + ",Bharatkosh reciept (Ground Water Abstraction Charges)"; }
                        }


                        #endregion

                    }
                    else
                    {
                        #region MIN Renew SAD
                        str_Next = "MiningRenew/Submit.aspx";
                        obj_miningRenewSADApplication.SetPaymentTypeMode(Convert.ToInt64(lblAppCode.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.NotDefined, out str_msg);



                        if (lblBharatKosh2.Visible)
                        {
                            if (obj_miningRenewSADApplication.GetBharatKoshRecieptAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Bharat Kosh Reciept" : ErrorMessage + ",Bharat Kosh Reciept"; }
                        }


                        if (lblAbstChargeINDRenewSAD.Visible)
                        {
                            if (obj_miningRenewSADApplication.GetAbsRestChargeAttachmentList().Length < 1) { ErrorMessage = ErrorMessage == string.Empty ? "Bharatkosh reciept (Ground Water Abstraction Charges)" : ErrorMessage + ",Bharatkosh reciept (Ground Water Abstraction Charges)"; }
                        }
                        #endregion

                    }

                    if (!string.IsNullOrEmpty(ErrorMessage))
                    {
                        lblMessageBharatKoshReciept.Text = ErrorMessage + " Attachments are Mandatory.";
                        lblMessageBharatKoshReciept.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    //lngIndSubmitAppCode = lng_submittedApplicationCode;

                    Server.Transfer(str_Next);


                }
                catch (ThreadAbortException)
                {


                }
                catch (Exception EX)
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
    }


    #endregion

    #region Radio Button
    protected void rdBtnPayMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str_msg = "";

        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewSADApplication = null;
        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewSADApplication = null;
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewSADApplication = null;
        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplication = null;
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_miningNewSADApplication = null;
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewSADApplication = null;
        NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_industrialNewSADApplication, out obj_infrastructureNewSADApplication, out obj_miningNewSADApplication, out obj_industrialRenewSADApplication, out obj_infrastructureRenewSADApplication, out obj_miningRenewSADApplication, Convert.ToInt64(lblApplicationCodeFrom.Text));

        if (rdBtnPayMode.SelectedValue.ToString() != "")
        {
            if (Convert.ToInt32(rdBtnPayMode.SelectedValue.ToString()) == 1)
            {
                rfvOnlinePayment.Enabled = true;
                rfvOfflinePayment.Enabled = false;
                tblOnlinePayment.Visible = true;
                tblOfflinePayment.Visible = false;

                if (obj_industrialNewSADApplication != null && obj_industrialNewSADApplication.CreatedByExUC > 0)
                    obj_industrialNewSADApplication.SetPaymentTypeMode(Convert.ToInt64(lblApplicationCodeFrom.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, out str_msg);
                if (obj_industrialRenewSADApplication != null && obj_industrialRenewSADApplication.CreatedByExUC > 0)
                    obj_industrialRenewSADApplication.SetPaymentTypeMode(Convert.ToInt64(lblApplicationCodeFrom.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, out str_msg);
                if (obj_infrastructureNewSADApplication != null && obj_infrastructureNewSADApplication.CreatedByExUC > 0)
                    obj_infrastructureNewSADApplication.SetPaymentTypeMode(Convert.ToInt64(lblApplicationCodeFrom.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, out str_msg);
                if (obj_infrastructureRenewSADApplication != null && obj_infrastructureRenewSADApplication.CreatedByExUC > 0)
                    obj_infrastructureRenewSADApplication.SetPaymentTypeMode(Convert.ToInt64(lblApplicationCodeFrom.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, out str_msg);

                if (obj_miningNewSADApplication != null && obj_miningNewSADApplication.CreatedByExUC > 0)
                    obj_miningNewSADApplication.SetPaymentTypeMode(Convert.ToInt64(lblApplicationCodeFrom.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, out str_msg);
                if (obj_miningRenewSADApplication != null && obj_miningRenewSADApplication.CreatedByExUC > 0)
                    obj_miningRenewSADApplication.SetPaymentTypeMode(Convert.ToInt64(lblApplicationCodeFrom.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, out str_msg);

            }
            else
            {

                if (obj_industrialNewSADApplication != null && obj_industrialNewSADApplication.CreatedByExUC > 0)
                    obj_industrialNewSADApplication.SetPaymentTypeMode(Convert.ToInt64(lblApplicationCodeFrom.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, out str_msg);
                if (obj_industrialRenewSADApplication != null && obj_industrialRenewSADApplication.CreatedByExUC > 0)
                    obj_industrialRenewSADApplication.SetPaymentTypeMode(Convert.ToInt64(lblApplicationCodeFrom.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, out str_msg);
                if (obj_infrastructureNewSADApplication != null && obj_infrastructureNewSADApplication.CreatedByExUC > 0)
                    obj_infrastructureNewSADApplication.SetPaymentTypeMode(Convert.ToInt64(lblApplicationCodeFrom.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, out str_msg);
                if (obj_infrastructureRenewSADApplication != null && obj_infrastructureRenewSADApplication.CreatedByExUC > 0)
                    obj_infrastructureRenewSADApplication.SetPaymentTypeMode(Convert.ToInt64(lblApplicationCodeFrom.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, out str_msg);

                if (obj_miningNewSADApplication != null && obj_miningNewSADApplication.CreatedByExUC > 0)
                    obj_miningNewSADApplication.SetPaymentTypeMode(Convert.ToInt64(lblApplicationCodeFrom.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, out str_msg);
                if (obj_miningRenewSADApplication != null && obj_miningRenewSADApplication.CreatedByExUC > 0)
                    obj_miningRenewSADApplication.SetPaymentTypeMode(Convert.ToInt64(lblApplicationCodeFrom.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, out str_msg);


                rfvOnlinePayment.Enabled = false;
                rfvOfflinePayment.Enabled = true;
                tblOnlinePayment.Visible = false;
                tblOfflinePayment.Visible = true;


            }
        }

    }
    #endregion

    #region DropDownList
    protected void ddlPaidFee_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMessageBharatKoshReciept.Text = "";
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = null;
        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewSADApplication = null;
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewSADApplication = null;
        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewSADApplication = null;
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewSADApplication = null;
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewSADApplication = null;

        NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_IndustrialNewSADApplication, out obj_InfrastructureNewSADApplication, out obj_MiningNewSADApplication,
            out obj_IndustrialRenewSADApplication, out obj_InfrastructureRenewSADApplication, out obj_MiningRenewSADApplication, Convert.ToInt64(lblApplicationCodeFrom.Text));

        State obj_state = new State(Convert.ToInt32(StateCode.Value));

        if (obj_state.IsDoubleTax == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
        {
            PanelDoubleTaxPay.Visible = true;
        }

        if (ddlPaidFee.SelectedValue == "1")
        {
            pnlOffline.Visible = true;
            pnlOnline.Visible = false;


        }
        else if (ddlPaidFee.SelectedValue == "2")
        {
            pnlOnline.Visible = true;
            pnlOffline.Visible = false;
        }
        else
        {
            pnlOnline.Visible = false;
            pnlOffline.Visible = false;
            PanelDoubleTaxPay.Visible = false;
        }
    }
    #endregion

    #region RowDeleting

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
                NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus obj_SADProcessingFeeStatus = new NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus();
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = null;
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewSADApplication = null;
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewSADApplication = null;
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewSADApplication = null;
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewSADApplication = null;
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewSADApplication = null;

                NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_IndustrialNewSADApplication, out obj_InfrastructureNewSADApplication, out obj_MiningNewSADApplication,
                    out obj_IndustrialRenewSADApplication, out obj_InfrastructureRenewSADApplication, out obj_MiningRenewSADApplication,
                    Convert.ToInt64(lblApplicationCodeFrom.Text));
                if (obj_IndustrialNewSADApplication != null && obj_IndustrialNewSADApplication.CreatedByExUC > 0)
                {
                    DeleteAttchmentAppFee((GridView)sender, e, lblMessageBharatKoshReciept, strActionName, obj_IndustrialNewSADApplication.IndustrialNewBharatKoshRecieptAttachmentCode);

                }
                else if (obj_IndustrialRenewSADApplication != null && obj_IndustrialRenewSADApplication.CreatedByExUC > 0)
                {
                    DeleteAttchmentAppFee((GridView)sender, e, lblMessageBharatKoshReciept, strActionName, obj_IndustrialRenewSADApplication.IndustrialRenewBharatKoshRecieptAttachmentCode);

                }
                else if (obj_InfrastructureNewSADApplication != null && obj_InfrastructureNewSADApplication.CreatedByExUC > 0)
                {
                    DeleteAttchmentAppFee((GridView)sender, e, lblMessageBharatKoshReciept, strActionName, obj_InfrastructureNewSADApplication.InfraStructureNewBharatKoshRecieptAttachmentCode);

                }
                else if (obj_InfrastructureRenewSADApplication != null && obj_InfrastructureRenewSADApplication.CreatedByExUC > 0)
                {
                    DeleteAttchmentAppFee((GridView)sender, e, lblMessageBharatKoshReciept, strActionName, obj_InfrastructureRenewSADApplication.InfraStructureRenewBharatKoshRecieptAttachmentCode);

                }
                else if (obj_MiningNewSADApplication != null && obj_MiningNewSADApplication.CreatedByExUC > 0)
                {
                    DeleteAttchmentAppFee((GridView)sender, e, lblMessageBharatKoshReciept, strActionName, obj_MiningNewSADApplication.MiningNewBharatKoshRecieptAttachmentCode);

                }
                else if (obj_MiningRenewSADApplication != null && obj_MiningRenewSADApplication.CreatedByExUC > 0)
                {
                    DeleteAttchmentAppFee((GridView)sender, e, lblMessageBharatKoshReciept, strActionName, obj_MiningRenewSADApplication.MiningRenewBharatKoshRecieptAttachmentCode);

                }
                Clear();
                BindAppFeeReceived();
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }

    protected void gvPenalty_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            strActionName = "Delete";
            try
            {
                if (!NOCAPExternalUtility.IsNumeric(gvPenalty.DataKeys[e.RowIndex].Values["ApplicationCode"]))
                {

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('ApplicationCode allows only Numeric');", true);
                    return;
                }

                if (!NOCAPExternalUtility.IsNumeric(gvPenalty.DataKeys[e.RowIndex].Values["SN"]))
                {

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('SN allows only Numeric');", true);
                    return;
                }
                NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus obj_SADProcessingFeeStatus = new NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus();
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = null;
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewSADApplication = null;
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewSADApplication = null;
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewSADApplication = null;
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewSADApplication = null;
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewSADApplication = null;

                NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_IndustrialNewSADApplication, out obj_InfrastructureNewSADApplication, out obj_MiningNewSADApplication,
                    out obj_IndustrialRenewSADApplication, out obj_InfrastructureRenewSADApplication, out obj_MiningRenewSADApplication,
                    Convert.ToInt64(lblApplicationCodeFrom.Text));
                if (obj_IndustrialNewSADApplication != null && obj_IndustrialNewSADApplication.CreatedByExUC > 0)
                {
                    DeleteAttchmentPenalty((GridView)sender, e, lblMessageBharatKoshReciept, strActionName, obj_IndustrialNewSADApplication.PenaltyAttCode);

                }
                else if (obj_IndustrialRenewSADApplication != null && obj_IndustrialRenewSADApplication.CreatedByExUC > 0)
                {
                    DeleteAttchmentPenalty((GridView)sender, e, lblMessageBharatKoshReciept, strActionName, obj_IndustrialRenewSADApplication.PenaltyAttCode);

                }
                else if (obj_InfrastructureNewSADApplication != null && obj_InfrastructureNewSADApplication.CreatedByExUC > 0)
                {
                    DeleteAttchmentPenalty((GridView)sender, e, lblMessageBharatKoshReciept, strActionName, obj_InfrastructureNewSADApplication.PenaltyAttCode);

                }
                else if (obj_InfrastructureRenewSADApplication != null && obj_InfrastructureRenewSADApplication.CreatedByExUC > 0)
                {
                    DeleteAttchmentPenalty((GridView)sender, e, lblMessageBharatKoshReciept, strActionName, obj_InfrastructureRenewSADApplication.PenaltyAttCode);

                }
                else if (obj_MiningNewSADApplication != null && obj_MiningNewSADApplication.CreatedByExUC > 0)
                {
                    DeleteAttchmentPenalty((GridView)sender, e, lblMessageBharatKoshReciept, strActionName, obj_MiningNewSADApplication.PenaltyAttCode);

                }
                else if (obj_MiningRenewSADApplication != null && obj_MiningRenewSADApplication.CreatedByExUC > 0)
                {
                    DeleteAttchmentPenalty((GridView)sender, e, lblMessageBharatKoshReciept, strActionName, obj_MiningRenewSADApplication.PenaltyAttCode);

                }






            }
            catch (Exception ex)
            {
                strStatus = "Record Delete Failed !";
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

    protected void gvGWCharges_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            strActionName = "Delete";
            try
            {
                if (!NOCAPExternalUtility.IsNumeric(gvGWCharges.DataKeys[e.RowIndex].Values["ApplicationCode"]))
                {

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('ApplicationCode allows only Numeric');", true);
                    return;
                }

                if (!NOCAPExternalUtility.IsNumeric(gvGWCharges.DataKeys[e.RowIndex].Values["SN"]))
                {

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('SN allows only Numeric');", true);
                    return;
                }
                NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus obj_SADProcessingFeeStatus = new NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus();
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = null;
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewSADApplication = null;
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewSADApplication = null;
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewSADApplication = null;
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewSADApplication = null;
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewSADApplication = null;

                NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_IndustrialNewSADApplication, out obj_InfrastructureNewSADApplication, out obj_MiningNewSADApplication,
                    out obj_IndustrialRenewSADApplication, out obj_InfrastructureRenewSADApplication, out obj_MiningRenewSADApplication,
                    Convert.ToInt64(lblApplicationCodeFrom.Text));
                if (obj_IndustrialNewSADApplication != null && obj_IndustrialNewSADApplication.CreatedByExUC > 0)
                {
                    DeleteAttchmentGWCharge((GridView)sender, e, lblMessageBharatKoshReciept, strActionName, obj_IndustrialNewSADApplication.AbsRestChargeAttCode);

                }
                else if (obj_IndustrialRenewSADApplication != null && obj_IndustrialRenewSADApplication.CreatedByExUC > 0)
                {
                    DeleteAttchmentGWCharge((GridView)sender, e, lblMessageBharatKoshReciept, strActionName, obj_IndustrialRenewSADApplication.AbsRestChargeAttCode);

                }
                else if (obj_InfrastructureNewSADApplication != null && obj_InfrastructureNewSADApplication.CreatedByExUC > 0)
                {
                    DeleteAttchmentGWCharge((GridView)sender, e, lblMessageBharatKoshReciept, strActionName, obj_InfrastructureNewSADApplication.AbsRestChargeAttCode);

                }
                else if (obj_InfrastructureRenewSADApplication != null && obj_InfrastructureRenewSADApplication.CreatedByExUC > 0)
                {
                    DeleteAttchmentGWCharge((GridView)sender, e, lblMessageBharatKoshReciept, strActionName, obj_InfrastructureRenewSADApplication.AbsRestChargeAttCode);

                }
                else if (obj_MiningNewSADApplication != null && obj_MiningNewSADApplication.CreatedByExUC > 0)
                {
                    DeleteAttchmentGWCharge((GridView)sender, e, lblMessageBharatKoshReciept, strActionName, obj_MiningNewSADApplication.AbsRestChargeAttCode);

                }
                else if (obj_MiningRenewSADApplication != null && obj_MiningRenewSADApplication.CreatedByExUC > 0)
                {
                    DeleteAttchmentGWCharge((GridView)sender, e, lblMessageBharatKoshReciept, strActionName, obj_MiningRenewSADApplication.AbsRestChargeAttCode);

                }






            }
            catch (Exception ex)
            {
                strStatus = "Record Delete Failed !";
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

    protected void gvDoubleTax_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                strActionName = "File Delete Double Tax Reciept";

                DoubleTaxPayment Obj_doubleTaxPayment = new DoubleTaxPayment();

                Obj_doubleTaxPayment.ApplicationCode = Convert.ToInt32(gvDoubleTax.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                Obj_doubleTaxPayment.SN = Convert.ToInt32(gvDoubleTax.DataKeys[e.RowIndex].Values["SN"]);

                if (Obj_doubleTaxPayment.Delete() == 1)
                {
                    strStatus = "Record Delete Successfully !";
                    LblDoubleTax.Text = HttpUtility.HtmlEncode(Obj_doubleTaxPayment.CustumMessage);
                    LblDoubleTax.ForeColor = System.Drawing.Color.Green;
                    Clear();
                    BindGvDoubleTaxPay();
                }
                else
                {
                    strStatus = "Record Delete Failed !";
                    LblDoubleTax.Text = HttpUtility.HtmlEncode(Obj_doubleTaxPayment.CustumMessage);
                    LblDoubleTax.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception ex)
            {
                strStatus = "Record Delete Failed !";
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

    #region Sorting
    protected void gvPenalty_Sorting(object sender, GridViewSortEventArgs e)
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
                NOCAP.BLL.Master.State obj_State = new NOCAP.BLL.Master.State();
                lblSortFieldPenalty.Text = HttpUtility.HtmlEncode(e.SortExpression);
                gvPenalty.EditIndex = -1;

                BindChargesReceivedPenalty(lblSortFieldPenalty.Text);
            }
            catch (Exception Ex)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    protected void gvApplicationFee_Sorting(object sender, GridViewSortEventArgs e)
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
                NOCAP.BLL.Master.State obj_State = new NOCAP.BLL.Master.State();
                lblSortFieldPenalty.Text = HttpUtility.HtmlEncode(e.SortExpression);
                gvGWCharges.EditIndex = -1;

                Clear();
                BindChargesReceivedGWCharges(lblSortFieldGWCharges.Text);
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

    protected void gvGWCharges_Sorting(object sender, GridViewSortEventArgs e)
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
                NOCAP.BLL.Master.State obj_State = new NOCAP.BLL.Master.State();
                lblSortFieldPenalty.Text = HttpUtility.HtmlEncode(e.SortExpression);
                gvGWCharges.EditIndex = -1;
                Clear();
                BindChargesReceivedGWCharges(lblSortFieldGWCharges.Text);
            }
            catch (Exception)
            {
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

    protected void gvDoubleTax_Sorting(object sender, GridViewSortEventArgs e)
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

            }
            catch (Exception)
            {
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

    protected void DownloadOrViewFiles(object sender, CommandEventArgs e)
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
                    long lng_ApplicationCode = Convert.ToInt64(CommandArgument[0]);
                    int int_SN = Convert.ToInt32(CommandArgument[1]);

                    NOCAPExternalUtility.SADAppFeeDownloadFiles(lng_ApplicationCode, int_SN);
                }

            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
            }
        }
    }


    protected void ValidateDate(object sender, ServerValidateEventArgs e)
    {
        if (NOCAPExternalUtility.IsValidDate(e.Value))
        {
            e.IsValid = true;
        }
        else
        {
            e.IsValid = false;
        }
    }

    protected void DownloadOrViewFilePenaltyCorrection(object sender, CommandEventArgs e)
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
                    long lng_ApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_PenaltySN = Convert.ToInt32(CommandArgument[1]);
                    int int_SN = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.SADPenaltyCorrectionChargeDownloadFiles(lng_ApplicationCode, int_PenaltySN, int_SN);
                }

            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
            }
        }
    }

    protected void lblViewUploadedLinkApplicationFee(object sender, CommandEventArgs e)
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
                    long lng_ApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_SN = Convert.ToInt32(CommandArgument[1]);
                    //  NOCAPExternalUtility.SADPenaltyCorrectionChargeDownloadFiles(lng_ApplicationCode, int_SN);
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
            }
        }
    }

    protected void DownloadOrViewFileGWCharges(object sender, CommandEventArgs e)
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
                    long lng_ApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_SN = Convert.ToInt32(CommandArgument[1]);
                    NOCAPExternalUtility.SADGroundWaterChargeDownloadFiles(lng_ApplicationCode, int_SN);
                }

            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
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
                    long lng_ApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_SN = Convert.ToInt32(CommandArgument[1]);
                    NOCAPExternalUtility.DoubleTaxDownloadFiles(lng_ApplicationCode, int_SN);
                }

            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
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