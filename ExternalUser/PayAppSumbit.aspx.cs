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
using System.Data;

public partial class ExternalUser_PayAppSumbit : System.Web.UI.Page
{
    string strPageName = "Payment";
    string strActionName = "";
    string strStatus = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                ValidationExpInit();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                lblMessage.Text = "";
                pnlPaymentDetail.Visible = false;
                if (NOCAPExternalUtility.FillDropDownApplicationType(ref ddlApplicationType) != 1)
                    Response.Write("Problem in Application Type ");
                ddlApplicationType.Focus();
              
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
                ddlApplicationType_SelectedIndexChanged(sender, e);
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }





    #region Private
    
    private void AddPenaltyGrandTotal()
    {
        float GTotal = 0f;
        for (int i = 0; i < gvPenalty.Rows.Count; i++)
        {
            String total = (gvPenalty.Rows[i].FindControl("lblRate") as Label).Text;
            GTotal += Convert.ToSingle(total);
        }
        txtPenaltyAmount.Text = GTotal.ToString();
        //       LabelTotalAmount.Text = GTotal.ToString() + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(GTotal.ToString())) + ")";

    }
    private void BindData(object sender, EventArgs e)
    {
        try
        {

            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = null;
            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = null;
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = null;
            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = null;
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = null;
            NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplication = null;

            NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationNumber(out obj_industrialNewApplication,
                out obj_infrastructureNewApplication,
                out obj_miningNewApplication,
                out obj_IndustrialRenewApplication,
                out obj_infrastructureRenewApplication,
                out obj_miningRenewApplication, ddlApplicationNumber.SelectedValue.ToString());



            string str_lblOfflinePenaltyAmount = "";

            if (obj_industrialNewApplication != null && obj_industrialNewApplication.CreatedByExUC > 0 && obj_industrialNewApplication.NameOfIndustry.Trim() != "")
            {
                pnlPaymentDetail.Visible = true;
                obj_IndustrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication();
                obj_IndustrialRenewApplication.FirstApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                if (obj_IndustrialRenewApplication.GetAll() == 1)
                {
                    obj_IndustrialRenewApplication = obj_IndustrialRenewApplication.IndustrialRenewApplicationCollection.Where(x => x.FirstApplicationCode == obj_industrialNewApplication.IndustrialNewApplicationCode)
                       .OrderByDescending(x => x.IndustrialRenewApplicationCode)
                            .ToList()
                            .FirstOrDefault();
                    lblAppCode.Text = obj_IndustrialRenewApplication.IndustrialRenewApplicationCode.ToString();
                    lblAppPur.Text = new NOCAP.BLL.Master.ApplicationPurpose(obj_IndustrialRenewApplication.ApplicationPurposeCode).ApplicationPurposeDesc;
                    BindGrondWaterCharges(Convert.ToInt32(obj_IndustrialRenewApplication.WaterQualityCodeFinally), Convert.ToString(obj_IndustrialRenewApplication.GWChargeAmtFinally),
                        Convert.ToString(obj_IndustrialRenewApplication.GWArearAmtFinally));
                    BindPenalty(obj_IndustrialRenewApplication.IndustrialRenewApplicationCode, Convert.ToInt32(obj_IndustrialRenewApplication.PenaltySN));
                    BindPaymentDetail(sender, e, obj_IndustrialRenewApplication.IndustrialRenewApplicationCode);
                    if (obj_IndustrialRenewApplication.ECReqFinally == NOCAP.BLL.Common.CommonApplication.ECOption.Yes && obj_IndustrialRenewApplication.ECRecFinally == NOCAP.BLL.Common.CommonApplication.ECOption.No)
                    {
                        rowEC.Visible = true;
                        rfvEC.Enabled = true;
                    }
                    else
                    {
                        rfvEC.Enabled = false;
                        rowEC.Visible = false;
                    }
                }
                else
                {
                    lblAppCode.Text = obj_industrialNewApplication.IndustrialNewApplicationCode.ToString();
                    lblAppPur.Text = new NOCAP.BLL.Master.ApplicationPurpose(obj_industrialNewApplication.ApplicationPurposeCode).ApplicationPurposeDesc;
                    BindGrondWaterCharges(Convert.ToInt32(obj_industrialNewApplication.WaterQualityCodeFinally), Convert.ToString(obj_industrialNewApplication.GWChargeAmtFinally), Convert.ToString(obj_industrialNewApplication.GWArearAmtFinally));
                    BindPenalty(obj_industrialNewApplication.IndustrialNewApplicationCode, Convert.ToInt32(obj_industrialNewApplication.PenaltySN));
                    BindPaymentDetail(sender, e, obj_industrialNewApplication.IndustrialNewApplicationCode);

                    if (obj_industrialNewApplication.ECReqFinally == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.ECOption.Yes
                        && obj_industrialNewApplication.ECRecFinally == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.ECOption.No)
                    {
                        rowEC.Visible = true;
                        rfvEC.Enabled = true;
                    }
                    else
                    {
                        rowEC.Visible = false;
                        rfvEC.Enabled = false;
                    }
                }

                lblAppName.Text = Convert.ToString(obj_industrialNewApplication.NameOfIndustry);
                lblAppNo.Text = obj_industrialNewApplication.IndustrialNewApplicationNumber;
                lblAppType.Text = new NOCAP.BLL.Master.ApplicationType(obj_industrialNewApplication.ApplicationTypeCode).ApplicationTypeDescription;



            }
            else if (obj_infrastructureNewApplication != null && obj_infrastructureNewApplication.CreatedByExUC > 0 && obj_infrastructureNewApplication.NameOfInfrastructure.Trim() != "")
            {
                // pnlPaymentDetail.Visible = true;

                obj_infrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication();
                obj_infrastructureRenewApplication.FirstApplicationCode = obj_infrastructureNewApplication.InfrastructureNewApplicationCode;
                if (obj_infrastructureRenewApplication.GetAll() == 1)
                {

                    obj_infrastructureRenewApplication = obj_infrastructureRenewApplication.InfrastructureRenewApplicationCollection.Where(x => x.FirstApplicationCode == obj_infrastructureNewApplication.InfrastructureNewApplicationCode)
                        .OrderByDescending(x => x.InfrastructureRenewApplicationCode)
                             .ToList()
                             .FirstOrDefault();
                    lblAppCode.Text = obj_infrastructureRenewApplication.InfrastructureRenewApplicationCode.ToString();
                    lblAppPur.Text = new NOCAP.BLL.Master.ApplicationPurpose(obj_infrastructureRenewApplication.ApplicationPurposeCode).ApplicationPurposeDesc;
                    BindGrondWaterCharges(Convert.ToInt32(obj_infrastructureRenewApplication.WaterQualityCodeFinally), Convert.ToString(obj_infrastructureRenewApplication.GWChargeAmtFinally), Convert.ToString(obj_infrastructureRenewApplication.GWArearAmtFinally));
                    BindPenalty(obj_infrastructureRenewApplication.InfrastructureRenewApplicationCode, Convert.ToInt32(obj_infrastructureRenewApplication.PenaltySN));
                    BindPaymentDetail(sender, e, obj_infrastructureRenewApplication.InfrastructureRenewApplicationCode);

                    if (obj_infrastructureRenewApplication.ECReqFinally == NOCAP.BLL.Common.CommonApplication.ECOption.Yes
                        && obj_infrastructureRenewApplication.ECRecFinally == NOCAP.BLL.Common.CommonApplication.ECOption.No)
                    {
                        rowEC.Visible = true;
                        rfvEC.Enabled = true;
                    }
                    else
                    {
                        rowEC.Visible = false;
                        rfvEC.Enabled = false;
                    }
                }
                else
                {
                    lblAppCode.Text = obj_infrastructureNewApplication.InfrastructureNewApplicationCode.ToString();
                    lblAppPur.Text = new NOCAP.BLL.Master.ApplicationPurpose(obj_infrastructureNewApplication.ApplicationPurposeCode).ApplicationPurposeDesc;
                    BindGrondWaterCharges(Convert.ToInt32(obj_infrastructureNewApplication.WaterQualityCodeFinally), Convert.ToString(obj_infrastructureNewApplication.GWChargeAmtFinally), Convert.ToString(obj_infrastructureNewApplication.GWArearAmtFinally));
                    BindPenalty(obj_infrastructureNewApplication.InfrastructureNewApplicationCode, Convert.ToInt32(obj_infrastructureNewApplication.PenaltySN));
                    BindPaymentDetail(sender, e, obj_infrastructureNewApplication.InfrastructureNewApplicationCode);
                    if (obj_infrastructureNewApplication.ECReqFinally == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.ECOption.Yes
                       && obj_infrastructureNewApplication.ECRecFinally == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.ECOption.No)
                    {
                        rowEC.Visible = true;
                        rfvEC.Enabled = true;
                    }
                    else
                    {
                        rowEC.Visible = false;
                        rfvEC.Enabled = false;
                    }
                }

                lblAppName.Text = Convert.ToString(obj_infrastructureNewApplication.NameOfInfrastructure);
                lblAppNo.Text = obj_infrastructureNewApplication.InfrastructureNewApplicationNumber;
                lblAppType.Text = new NOCAP.BLL.Master.ApplicationType(obj_infrastructureNewApplication.ApplicationTypeCode).ApplicationTypeDescription;

            }
            else if (obj_miningNewApplication != null && obj_miningNewApplication.CreatedByExUC > 0 && obj_miningNewApplication.NameOfMining.Trim() != "")
            {
                // pnlPaymentDetail.Visible = true;
                obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication();
                obj_miningRenewApplication.FirstApplicationCode = obj_miningNewApplication.ApplicationCode;
                if (obj_miningRenewApplication.GetAll() == 1)
                {
                    obj_miningRenewApplication = obj_miningRenewApplication.MiningRenewApplicationCollection.Where(x => x.FirstApplicationCode == obj_miningNewApplication.ApplicationCode)
                                          .OrderByDescending(x => x.MiningRenewApplicationCode)
                                               .ToList()
                                               .FirstOrDefault();
                    lblAppCode.Text = obj_miningNewApplication.ApplicationCode.ToString();
                    lblAppPur.Text = new NOCAP.BLL.Master.ApplicationPurpose(obj_miningRenewApplication.ApplicationPurposeCode).ApplicationPurposeDesc;

                    BindGrondWaterCharges(Convert.ToInt32(obj_miningRenewApplication.WaterQualityCodeFinally),
                        Convert.ToString(obj_miningRenewApplication.GWChargeAmtFinally), Convert.ToString(obj_miningRenewApplication.GWArearAmtFinally));
                    BindPenalty(obj_miningRenewApplication.MiningRenewApplicationCode, Convert.ToInt32(obj_miningRenewApplication.PenaltySN));
                    BindPaymentDetail(sender, e, obj_miningRenewApplication.MiningRenewApplicationCode);
                    if (obj_miningRenewApplication.ECReqFinally == NOCAP.BLL.Common.CommonApplication.ECOption.Yes
                       && obj_miningRenewApplication.ECRecFinally == NOCAP.BLL.Common.CommonApplication.ECOption.No)
                    {
                        rowEC.Visible = true;
                        rfvEC.Enabled = true;
                    }
                    else
                    {
                        rowEC.Visible = false;
                        rfvEC.Enabled = false;
                    }
                }
                else
                {
                    lblAppCode.Text = obj_miningNewApplication.ApplicationCode.ToString();
                    lblAppPur.Text = new NOCAP.BLL.Master.ApplicationPurpose(obj_miningNewApplication.ApplicationPurposeCode).ApplicationPurposeDesc;

                    BindGrondWaterCharges(Convert.ToInt32(obj_miningNewApplication.WaterQualityCodeFinally), Convert.ToString(obj_miningNewApplication.GWChargeAmtFinally), Convert.ToString(obj_miningNewApplication.GWArearAmtFinally));
                    BindPenalty(obj_miningNewApplication.ApplicationCode, Convert.ToInt32(obj_miningNewApplication.PenaltySN));
                    BindPaymentDetail(sender, e, obj_miningNewApplication.ApplicationCode);
                    if (obj_miningNewApplication.ECReqFinally == NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.ECOption.Yes
                     && obj_miningNewApplication.ECRecFinally == NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.ECOption.No)
                    {
                        rowEC.Visible = true;
                        rfvEC.Enabled = true;
                    }
                    else
                    {
                        rowEC.Visible = false;
                        rfvEC.Enabled = false;
                    }
                }



                lblAppName.Text = Convert.ToString(obj_miningNewApplication.NameOfMining);
                lblAppNo.Text = obj_miningNewApplication.MiningNewApplicationNumber;
                lblAppType.Text = new NOCAP.BLL.Master.ApplicationType(obj_miningNewApplication.ApplicationTypeCode).ApplicationTypeDescription;
            }

            else
            { //pnlPaymentDetail.Visible = false;
            }

            int intA_LabelTotalAmount = 0, intA_lblTotalAmountCorrectionChargePopup = 0;
            // if (LabelTotalAmount.Text.Split('/')[0] != "")
            //  intA_LabelTotalAmount =Convert.ToInt32(LabelTotalAmount.Text.Split('/')[0]);
            // if (lblTotalAmountCorrectionChargePopup.Text.Split('/')[0] != "")
            //    intA_lblTotalAmountCorrectionChargePopup =Convert.ToInt32(lblTotalAmountCorrectionChargePopup.Text.Split('/')[0]);
            // str_lblPenaltyAmount = Convert.ToString(intA_lblTotalAmountCorrectionChargePopup + intA_LabelTotalAmount);
            // lblPenaltyAmount.Text = str_lblPenaltyAmount + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(str_lblPenaltyAmount)) + ")";
            str_lblOfflinePenaltyAmount = Convert.ToString(intA_lblTotalAmountCorrectionChargePopup + intA_LabelTotalAmount);
            //lblOfflinePenaltyAmount.Text = str_lblOfflinePenaltyAmount + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(str_lblOfflinePenaltyAmount)) + ")";
            //txtTotalAmount.Text = txtPenaltyAmount.Text.Trim();
            //lblOfflineChargeType.Text = lblChargeType.Text;

        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindPenalty(long lngA_AppCode, int intA_PenaltySN)
    {
        NOCAP.BLL.PenaltyImpose.PenaltyImpose obj_PenaltyImpose = new NOCAP.BLL.PenaltyImpose.PenaltyImpose(lngA_AppCode, intA_PenaltySN);

        if (obj_PenaltyImpose != null && obj_PenaltyImpose.PenaltySN > 0 && obj_PenaltyImpose.PenaltyRecFinally == NOCAP.BLL.PenaltyImpose.PenaltyImpose.PenaltyOptionRecd.No)
        {
            NOCAPExternalUtility.BindGridViewPenaltyImposeDetail(ref gvPenalty, ref gvPenalty, lngA_AppCode, intA_PenaltySN);
            if (gvPenalty.Rows.Count < 1)
            {
                rowPenalty.Visible = false;
                rfvPenaltyAmount.Enabled = false;
            }
            else
            {
                rowPenalty.Visible = true;
                rfvPenaltyAmount.Enabled = true;
            }
            NOCAPExternalUtility.BindGridViewPenaltyCorrImposeDetail(ref gvCorrection, ref gvCorrection, lngA_AppCode, intA_PenaltySN);
            if (gvCorrection.Rows.Count < 1)
            {
                rowModification.Visible = false;
                rfvCorrAmount.Enabled = false;
            }
            else
            {
                rowModification.Visible = true;
                rfvCorrAmount.Enabled = true;
            }
        }
        else
        {
            rowPenalty.Visible = false;
            rowModification.Visible = false;
        }

    }
   
    private void BindGrondWaterCharges(int intA_WaterQualityCodeFinally, string strA_lblGWCharge, string strA_lblGWChargeArear)
    {
        if (intA_WaterQualityCodeFinally == 1)
        {

            BindGrondWaterCharges(strA_lblGWCharge, strA_lblGWChargeArear);
        }

    }
    private void BindGrondWaterCharges(string strA_lblGWCharge, string strA_lblGWChargeArear)
    {

        //lblGWCharge.Text = strA_lblGWCharge + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(strA_lblGWCharge)) + ")";
        //lblGWChargeArear.Text = strA_lblGWChargeArear + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(strA_lblGWChargeArear)) + ")";

        // txtGWCharge.Text = lblGWCharge.Text;
        // txtGWChargeArear.Text = lblGWChargeArear.Text;
    }
    private void BindPaymentDetail(object sender, EventArgs e, long lngA_AppCode)
    {
        try
        {
            NOCAP.BLL.Misc.Payment.OnlinePayment obj_OnlinePayment = new NOCAP.BLL.Misc.Payment.OnlinePayment();
            obj_OnlinePayment.ApplicationCode = lngA_AppCode;
            NOCAP.BLL.Misc.Payment.OnlinePayment[] arr_OnlinePayment = null;
            NOCAP.BLL.Misc.Payment.OnlinePaymentDetails[] arr_OnlinePaymentDetails = null;
            if (obj_OnlinePayment.GetALL() == 1)
            {
                arr_OnlinePayment = obj_OnlinePayment.OnlinePaymentCollection;
                arr_OnlinePayment = obj_OnlinePayment.OnlinePaymentCollection
                           .Where(x => x.PaymentStatus == NOCAP.BLL.Common.CommonEnum.PaymentStatus.SUCCESS
                           || x.PaymentStatus == NOCAP.BLL.Common.CommonEnum.PaymentStatus.PENDING).ToArray();
                if (arr_OnlinePayment != null && arr_OnlinePayment.Length > 0)
                {
                    switch (arr_OnlinePayment[0].PaymentMethodMode)
                    {
                        #region Combined
                        case NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine:
                            rdBtnPayMode.SelectedValue = "1";
                            if (obj_OnlinePayment.GetALL() == 1)
                            {
                                arr_OnlinePayment = obj_OnlinePayment.OnlinePaymentCollection
                                .Where(x => x.PaymentStatus == NOCAP.BLL.Common.CommonEnum.PaymentStatus.SUCCESS
                                || x.PaymentStatus == NOCAP.BLL.Common.CommonEnum.PaymentStatus.PENDING).ToArray();
                                if (arr_OnlinePayment != null && arr_OnlinePayment.Length > 0)
                                {
                                    PayBtn.Enabled = false;

                                }
                                else
                                {
                                    PayBtn.Enabled = true;

                                }
                            }
                            else
                                PayBtn.Enabled = true;



                            break;
                        #endregion

                        #region Single
                        case NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine:
                            rdBtnPayMode.SelectedValue = "0";
                            #region Charge
                            if (obj_OnlinePayment.GetALL() == 1)
                            {
                                arr_OnlinePayment = obj_OnlinePayment.OnlinePaymentCollection
                                .Where(x => x.PaymentStatus == NOCAP.BLL.Common.CommonEnum.PaymentStatus.SUCCESS
                                || x.PaymentStatus == NOCAP.BLL.Common.CommonEnum.PaymentStatus.PENDING).ToArray();
                                if (arr_OnlinePayment != null && arr_OnlinePayment.Length > 0)
                                {
                                    NOCAP.BLL.Misc.Payment.OnlinePaymentDetails obj_OnlinePaymentDetails = new NOCAP.BLL.Misc.Payment.OnlinePaymentDetails();
                                    obj_OnlinePaymentDetails.AppCode = arr_OnlinePayment[0].ApplicationCode;
                                    obj_OnlinePaymentDetails.OrderPaymentCode = arr_OnlinePayment[0].OrderPaymentCode;
                                    obj_OnlinePaymentDetails.GetALL();
                                    arr_OnlinePaymentDetails = obj_OnlinePaymentDetails.OnlinePaymentDetailsCollection
                                .Where(x => x.PaymentTypeCode == 2 || x.PaymentTypeCode == 3).ToArray();
                                    if (arr_OnlinePaymentDetails != null && arr_OnlinePaymentDetails.Length > 0)
                                    {
                                        rdbtnCharge.Enabled = false;
                                        btnCharge.Enabled = false;
                                    }
                                    else
                                    {
                                        rdbtnCharge.Enabled = true;
                                        btnCharge.Enabled = true;
                                    }


                                    arr_OnlinePaymentDetails = obj_OnlinePaymentDetails.OnlinePaymentDetailsCollection.Where(x => x.PaymentTypeCode == 4).ToArray();
                                    if (arr_OnlinePaymentDetails != null && arr_OnlinePaymentDetails.Length > 0)
                                    {
                                        rdbtnPenalty.Enabled = false;
                                        btnPenalty.Enabled = false;
                                    }
                                    else
                                    {
                                        rdbtnPenalty.Enabled = true;
                                        btnPenalty.Enabled = true;
                                    }

                                    arr_OnlinePaymentDetails = obj_OnlinePaymentDetails.OnlinePaymentDetailsCollection.Where(x => x.PaymentTypeCode == 5).ToArray();
                                    if (arr_OnlinePaymentDetails != null && arr_OnlinePaymentDetails.Length > 0)
                                    {
                                        rdbtnEC.Enabled = false;
                                        btnEC.Enabled = false;
                                    }
                                    else
                                    {
                                        rdbtnEC.Enabled = true;
                                        btnEC.Enabled = true;
                                    }
                                }
                                else
                                {
                                    rdbtnCharge.Enabled = true;
                                    btnCharge.Enabled = true;
                                    rdbtnPenalty.Enabled = true;
                                    btnPenalty.Enabled = true;
                                    btnCorrection.Enabled = true;
                                    rdbtnCorrection.Enabled = true;
                                    btnEC.Enabled = true;
                                    rdbtnEC.Enabled = true;


                                }
                            }
                            else
                                PayBtn.Enabled = true;



                            #endregion


                            break;
                            #endregion


                    }
                }
                else
                    rdBtnPayMode.SelectedValue = "1";

            }
            else
                rdBtnPayMode.SelectedValue = "1";




            rdBtnPayMode_SelectedIndexChanged(sender, e);


        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }






    private void ValidationExpInit()
    {
        //RegularExpressionValidator25.ValidationExpression = ValidationUtility.txtV;
        //RegularExpressionValidator25.ErrorMessage = ValidationUtility.txtValForNOCNumberMsg;

        //revtxtSalientFeaturesOfInActivity.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtSalientFeaturesOfInActivity.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;


        //revLengthtxtSalientFeaturesOfInActivity.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        //revLengthtxtSalientFeaturesOfInActivity.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");


    }
    private int AreaTypeCategoryCode(NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null,
        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = null,
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = null,
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = null,
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = null, NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = null)
    {
        NOCAP.BLL.Master.SubDistrict obj_subDistrict = null;
        NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_subDistrictAreaTypeCategoryHistory = null;
        if (obj_IndustrialNewApplication != null)
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        else if (obj_IndustrialRenewApplication != null)
        {
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_IndustrialRenewApplication.FirstApplicationCode);
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        }
        else if (obj_MiningNewApplication != null)
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);

        else if (obj_MiningRenewApplication != null)
        {
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_MiningRenewApplication.FirstApplicationCode);
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        }
        else if (obj_InfrastructureNewApplication != null)
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);

        else if (obj_InfrastructureRenewApplication != null)
        {
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_InfrastructureRenewApplication.FirstApplicationCode);
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);

        }


        obj_subDistrictAreaTypeCategoryHistory = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory();
        return obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode;
    }
    private void CreateXmlParam(Dictionary<int, decimal> dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode enu, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode enu2, decimal decA_GWAmountValue, decimal? ArearAmount = null,
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null,
        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = null,
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = null,
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = null,
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = null,
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = null, string strActionName = "", string strStatus = "")
    {


        string XMLstr = ""; string OrderPaymentCode = "";
        if (obj_IndustrialNewApplication != null)
            XMLstr = NOCAPExternalUtility.CreateXMLForApplication(Convert.ToInt64(Session["ExternalUserCode"]), dict, decA_GWAmountValue, ArearAmount, enu, enu2, ref OrderPaymentCode, obj_IndustrialNewApplication: obj_IndustrialNewApplication);
        else if (obj_IndustrialRenewApplication != null)
            XMLstr = NOCAPExternalUtility.CreateXMLForApplication(Convert.ToInt64(Session["ExternalUserCode"]), dict, decA_GWAmountValue, ArearAmount, enu, enu2, ref OrderPaymentCode, obj_IndustrialRenewApplication: obj_IndustrialRenewApplication);
        else if (obj_InfrastructureNewApplication != null)
            XMLstr = NOCAPExternalUtility.CreateXMLForApplication(Convert.ToInt64(Session["ExternalUserCode"]), dict, decA_GWAmountValue, ArearAmount, enu, enu2, ref OrderPaymentCode, obj_InfrastructureNewApplication: obj_InfrastructureNewApplication);
        else if (obj_InfrastructureRenewApplication != null)
            XMLstr = NOCAPExternalUtility.CreateXMLForApplication(Convert.ToInt64(Session["ExternalUserCode"]), dict, decA_GWAmountValue, ArearAmount, enu, enu2, ref OrderPaymentCode, obj_InfrastructureRenewApplication: obj_InfrastructureRenewApplication);
        else if (obj_MiningNewApplication != null)
            XMLstr = NOCAPExternalUtility.CreateXMLForApplication(Convert.ToInt64(Session["ExternalUserCode"]), dict, decA_GWAmountValue, ArearAmount, enu, enu2, ref OrderPaymentCode, obj_MiningNewApplication: obj_MiningNewApplication);
        else if (obj_MiningRenewApplication != null)
            XMLstr = NOCAPExternalUtility.CreateXMLForApplication(Convert.ToInt64(Session["ExternalUserCode"]), dict, decA_GWAmountValue, ArearAmount, enu, enu2, ref OrderPaymentCode, obj_MiningRenewApplication: obj_MiningRenewApplication);
        SendXml(XMLstr, OrderPaymentCode, strActionName, strStatus);


    }
    private void SendXml(string XMLstr, string OrderPaymentCode, string strActionName, string strStatus)
    {
        NOCAP.BLL.Misc.Payment.OnlinePayment obj_OnlinePayment = null;
        try
        {
            strActionName = "Combined Online Payment for All";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(XMLstr);
            xmlDoc.Save(Server.MapPath("LOBAFile.xml"));
            xmlDoc = NOCAPExternalUtility.signedFun(xmlDoc);
            foreach (XmlNode node in xmlDoc)
            {
                if (node.NodeType == XmlNodeType.XmlDeclaration)
                {
                    xmlDoc.RemoveChild(node);
                }
            }
            obj_OnlinePayment = new NOCAP.BLL.Misc.Payment.OnlinePayment(Convert.ToInt64(lblAppCode.Text), OrderPaymentCode);
            obj_OnlinePayment.OnlinePaymentXMLSent = xmlDoc.OuterXml;
            obj_OnlinePayment.SetOnlinePaymentXML();

            string URI = ConfigurationManager.AppSettings["NTRPURI"];
            xmlDoc.Save(Server.MapPath("LOBAFilewithsign.xml"));


            string xmlDocBase64 = Convert.ToBase64String(Encoding.Default.GetBytes(xmlDoc.OuterXml));

            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
            sb.AppendFormat("<form name='form' action='{0}' method='post'>", URI);
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
            OnlinePaymentActionTrail obj_OnlinePaymentActionTrail = new OnlinePaymentActionTrail();
            if (Session["ExternalUserCode"] != null)
            {
                obj_OnlinePaymentActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
                obj_OnlinePaymentActionTrail.IP_Address = Request.UserHostAddress;
                obj_OnlinePaymentActionTrail.ActionPerformed = strPageName + "-" + strActionName;
                obj_OnlinePaymentActionTrail.Status = strStatus + ",OrderPaymentCode-" + Convert.ToString(OrderPaymentCode) + ",App Code-" + Convert.ToString(obj_OnlinePayment.ApplicationCode);
                if (obj_OnlinePaymentActionTrail != null)
                    obj_OnlinePaymentActionTrail.AddOnlinePaymentAction(obj_OnlinePaymentActionTrail);
            }
        }

    }

    #endregion

    #region Button Click

    #region  All Payment
    protected void PayBtn_Click(object sender, EventArgs e)
    {


        strActionName = "Combined Online Payment for All";
        strStatus = "Start Payment";
        int int_areaTypeCategoryCode = 0;
        // bool WaterQualityCode = true;
        Dictionary<int, decimal> dict = new Dictionary<int, decimal>();
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null;
        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = null;
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = null;
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = null;
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = null;
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = null;

        NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_InfrastructureNewApplication, out obj_MiningNewApplication,
            out obj_IndustrialRenewApplication, out obj_InfrastructureRenewApplication, out obj_MiningRenewApplication, Convert.ToInt64(lblAppCode.Text));

        //if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.CreatedByExUC > 0 && obj_IndustrialNewApplication.WaterQualityCode == 1)
        //{
        //    WaterQualityCode = true;
        //    int_areaTypeCategoryCode = AreaTypeCategoryCode(obj_IndustrialNewApplication);
        //}
        //else if (obj_IndustrialRenewApplication != null && obj_IndustrialRenewApplication.CreatedByExUC > 0 && obj_IndustrialRenewApplication.WaterQualityCode == 1)
        //{
        //    WaterQualityCode = true;
        //    int_areaTypeCategoryCode = AreaTypeCategoryCode(null, obj_IndustrialRenewApplication);
        //}
        //else if (obj_MiningNewApplication != null && obj_MiningNewApplication.CreatedByExUC > 0 && obj_MiningNewApplication.WaterQualityCode == 1)
        //{
        //    WaterQualityCode = true;
        //    int_areaTypeCategoryCode = AreaTypeCategoryCode(null, null, obj_MiningNewApplication);
        //}
        //else if (obj_MiningRenewApplication != null && obj_MiningRenewApplication.CreatedByExUC > 0 && obj_MiningRenewApplication.WaterQualityCode == 1)
        //{
        //    WaterQualityCode = true;
        //    int_areaTypeCategoryCode = AreaTypeCategoryCode(null, null, null, obj_MiningRenewApplication);
        //}
        //else if (obj_InfrastructureNewApplication != null && obj_InfrastructureNewApplication.CreatedByExUC > 0 && obj_InfrastructureNewApplication.WaterQualityCode == 1)
        //{
        //    WaterQualityCode = true;
        //    int_areaTypeCategoryCode = AreaTypeCategoryCode(null, null, null, null, obj_InfrastructureNewApplication);
        //}

        //else if (obj_InfrastructureRenewApplication != null && obj_InfrastructureRenewApplication.CreatedByExUC > 0 && obj_InfrastructureRenewApplication.WaterQualityCode == 1)
        //{
        //    WaterQualityCode = true;
        //    int_areaTypeCategoryCode = AreaTypeCategoryCode(null, null, null, null, null, obj_InfrastructureRenewApplication);
        //}
        //else
        //    WaterQualityCode = false;


        //dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.ProcFee, Convert.ToDecimal(lblFeeAmout.Text));
        //if (WaterQualityCode)
        // {
        if (txtGWCharge.Text.Trim() != "" || txtGWChargeArear.Text.Trim() != "")
        {
            if (int_areaTypeCategoryCode == 5)
                dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWRCharge, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)) + (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)));
            else
                dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWACharge, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)) + (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)));
        }
        // }
        if (gvPenalty.Rows.Count != 0)
            dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.Penalty, Convert.ToDecimal(txtPenaltyAmount.Text));
        if (gvCorrection.Rows.Count != 0)
            dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.ModificationCharge, Convert.ToDecimal(txtCorrAmount.Text));
        if (txtEC.Text.Trim() != "")
        {
            dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.EC, Convert.ToDecimal(txtEC.Text.Trim()));
        }
        if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.CreatedByExUC > 0)
            CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), obj_IndustrialNewApplication, null, null, null, null, null, strActionName, strStatus);
        else if (obj_IndustrialRenewApplication != null && obj_IndustrialRenewApplication.CreatedByExUC > 0)
            CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), null, obj_IndustrialRenewApplication, null, null, null, null, strActionName, strStatus);
        else if (obj_MiningNewApplication != null && obj_MiningNewApplication.CreatedByExUC > 0)
            CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), null, null, obj_MiningNewApplication, null, null, null, strActionName, strStatus);
        else if (obj_MiningRenewApplication != null && obj_MiningRenewApplication.CreatedByExUC > 0)
            CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), null, null, null, obj_MiningRenewApplication, null, null, strActionName, strStatus);
        else if (obj_InfrastructureNewApplication != null && obj_InfrastructureNewApplication.CreatedByExUC > 0)
            CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), null, null, null, null, obj_InfrastructureNewApplication, null, strActionName, strStatus);
        else if (obj_InfrastructureRenewApplication != null && obj_InfrastructureRenewApplication.CreatedByExUC > 0)
            CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), null, null, null, null, null, obj_InfrastructureRenewApplication, strActionName, strStatus);

    }
    #endregion

    #region Single Payment
    protected void btnCharge_Click(object sender, EventArgs e)
    {
        strActionName = "Single Payment for Charge-" + (rdbtnCharge.SelectedValue == "0" ? "NEFT/RTGS" : "Online");
        strStatus = "Start Payment";
        int int_areaTypeCategoryCode = 0;
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null;
        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = null;
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = null;
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = null;
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = null;
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = null;

        NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_InfrastructureNewApplication, out obj_MiningNewApplication,
            out obj_IndustrialRenewApplication, out obj_InfrastructureRenewApplication, out obj_MiningRenewApplication, Convert.ToInt64(lblAppCode.Text));

        if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.CreatedByExUC > 0)
            int_areaTypeCategoryCode = AreaTypeCategoryCode(obj_IndustrialNewApplication);
        else if (obj_IndustrialRenewApplication != null && obj_IndustrialRenewApplication.CreatedByExUC > 0)
            int_areaTypeCategoryCode = AreaTypeCategoryCode(null, obj_IndustrialRenewApplication);
        else if (obj_MiningNewApplication != null && obj_MiningNewApplication.CreatedByExUC > 0)
            int_areaTypeCategoryCode = AreaTypeCategoryCode(null, null, obj_MiningNewApplication);

        else if (obj_MiningRenewApplication != null && obj_MiningRenewApplication.CreatedByExUC > 0)
            int_areaTypeCategoryCode = AreaTypeCategoryCode(null, null, null, obj_MiningRenewApplication);

        else if (obj_InfrastructureNewApplication != null && obj_InfrastructureNewApplication.CreatedByExUC > 0)
            int_areaTypeCategoryCode = AreaTypeCategoryCode(null, null, null, null, obj_InfrastructureNewApplication);

        else if (obj_InfrastructureRenewApplication != null && obj_InfrastructureRenewApplication.CreatedByExUC > 0)
            int_areaTypeCategoryCode = AreaTypeCategoryCode(null, null, null, null, null, obj_InfrastructureRenewApplication);
        Dictionary<int, decimal> dict = new Dictionary<int, decimal>();
        if (int_areaTypeCategoryCode == 5)
            dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWRCharge, (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)) + (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)));
        else
            dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWACharge, (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)) + (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)));
        if (rdbtnCharge.SelectedValue == "1")
        {
            if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), obj_IndustrialNewApplication, null, null, null, null, null, strActionName, strStatus);
            else if (obj_IndustrialRenewApplication != null && obj_IndustrialRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), null, obj_IndustrialRenewApplication, null, null, null, null, strActionName, strStatus);
            else if (obj_MiningNewApplication != null && obj_MiningNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), null, null, obj_MiningNewApplication, null, null, null, strActionName, strStatus);
            else if (obj_MiningRenewApplication != null && obj_MiningRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), null, null, null, obj_MiningRenewApplication, null, null, strActionName, strStatus);
            else if (obj_InfrastructureNewApplication != null && obj_InfrastructureNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), null, null, null, null, obj_InfrastructureNewApplication, null, strActionName, strStatus);
            else if (obj_InfrastructureRenewApplication != null && obj_InfrastructureRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), null, null, null, null, null, obj_InfrastructureRenewApplication, strActionName, strStatus);

        }
        else
        {
            if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), obj_IndustrialNewApplication, null, null, null, null, null, strActionName, strStatus);
            else if (obj_IndustrialRenewApplication != null && obj_IndustrialRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), null, obj_IndustrialRenewApplication, null, null, null, null, strActionName, strStatus);
            else if (obj_MiningNewApplication != null && obj_MiningNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), null, null, obj_MiningNewApplication, null, null, null, strActionName, strStatus);
            else if (obj_MiningRenewApplication != null && obj_MiningRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), null, null, null, obj_MiningRenewApplication, null, null, strActionName, strStatus);
            else if (obj_InfrastructureNewApplication != null && obj_InfrastructureNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), null, null, null, null, obj_InfrastructureNewApplication, null, strActionName, strStatus);
            else if (obj_InfrastructureRenewApplication != null && obj_InfrastructureRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)), (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)), null, null, null, null, null, obj_InfrastructureRenewApplication, strActionName, strStatus);
            strStatus = "Start Payment";

        }

    }

    protected void btnPenalty_Click(object sender, EventArgs e)
    {
        strActionName = "Single Payment for Penalty-";// + (rdbtnPenalty.SelectedValue == "0" ? "NEFT/RTGS" : "Online");
        strStatus = "Start Payment";
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null;
        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = null;
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = null;
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = null;
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = null;
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = null;

        NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_InfrastructureNewApplication, out obj_MiningNewApplication,
            out obj_IndustrialRenewApplication, out obj_InfrastructureRenewApplication, out obj_MiningRenewApplication, Convert.ToInt64(lblAppCode.Text));

        Dictionary<int, decimal> dict = new Dictionary<int, decimal>();
        if (gvPenalty.Rows.Count != 0)
            dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.Penalty, Convert.ToDecimal(txtPenaltyAmount.Text.Trim()));
        if (rdbtnPenalty.SelectedValue == "1")
        {
            if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, obj_IndustrialNewApplication, null, null, null, null, null, strActionName, strStatus);
            else if (obj_IndustrialRenewApplication != null && obj_IndustrialRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, obj_IndustrialRenewApplication, null, null, null, null, strActionName, strStatus);
            else if (obj_MiningNewApplication != null && obj_MiningNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, obj_MiningNewApplication, null, null, null, strActionName, strStatus);
            else if (obj_MiningRenewApplication != null && obj_MiningRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, obj_MiningRenewApplication, null, null, strActionName, strStatus);
            else if (obj_InfrastructureNewApplication != null && obj_InfrastructureNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, null, obj_InfrastructureNewApplication, null, strActionName, strStatus);
            else if (obj_InfrastructureRenewApplication != null && obj_InfrastructureRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, null, null, obj_InfrastructureRenewApplication, strActionName, strStatus);

        }
        else
        {

            if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, obj_IndustrialNewApplication, null, null, null, null, null, strActionName, strStatus);
            else if (obj_IndustrialRenewApplication != null && obj_IndustrialRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, obj_IndustrialRenewApplication, null, null, null, null, strActionName, strStatus);
            else if (obj_MiningNewApplication != null && obj_MiningNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, obj_MiningNewApplication, null, null, null, strActionName, strStatus);
            else if (obj_MiningRenewApplication != null && obj_MiningRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, obj_MiningRenewApplication, null, null, strActionName, strStatus);
            else if (obj_InfrastructureNewApplication != null && obj_InfrastructureNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, null, obj_InfrastructureNewApplication, null, strActionName, strStatus);
            else if (obj_InfrastructureRenewApplication != null && obj_InfrastructureRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, null, null, obj_InfrastructureRenewApplication, strActionName, strStatus);
            strStatus = "Start Payment";
        }


    }
    protected void btnCorrection_Click(object sender, EventArgs e)
    {
        strActionName = "Single Payment for Correction-" + (rdbtnCorrection.SelectedValue == "0" ? "NEFT/RTGS" : "Online");
        strStatus = "Start Payment";
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null;
        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = null;
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = null;
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = null;
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = null;
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = null;

        NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_InfrastructureNewApplication, out obj_MiningNewApplication,
            out obj_IndustrialRenewApplication, out obj_InfrastructureRenewApplication, out obj_MiningRenewApplication, Convert.ToInt64(lblAppCode.Text));

        Dictionary<int, decimal> dict = new Dictionary<int, decimal>();
        if (gvCorrection.Rows.Count != 0)
            dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.ModificationCharge, Convert.ToDecimal(txtCorrAmount.Text.Trim()));
        if (rdbtnCorrection.SelectedValue == "1")
        {
            if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, obj_IndustrialNewApplication, null, null, null, null, null, strActionName, strStatus);
            else if (obj_IndustrialRenewApplication != null && obj_IndustrialRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, obj_IndustrialRenewApplication, null, null, null, null, strActionName, strStatus);
            else if (obj_MiningNewApplication != null && obj_MiningNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, obj_MiningNewApplication, null, null, null, strActionName, strStatus);
            else if (obj_MiningRenewApplication != null && obj_MiningRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, obj_MiningRenewApplication, null, null, strActionName, strStatus);
            else if (obj_InfrastructureNewApplication != null && obj_InfrastructureNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, null, obj_InfrastructureNewApplication, null, strActionName, strStatus);
            else if (obj_InfrastructureRenewApplication != null && obj_InfrastructureRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, null, null, obj_InfrastructureRenewApplication, strActionName, strStatus);

        }
        else
        {

            if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, obj_IndustrialNewApplication, null, null, null, null, null, strActionName, strStatus);
            else if (obj_IndustrialRenewApplication != null && obj_IndustrialRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, obj_IndustrialRenewApplication, null, null, null, null, strActionName, strStatus);
            else if (obj_MiningNewApplication != null && obj_MiningNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, obj_MiningNewApplication, null, null, null, strActionName, strStatus);
            else if (obj_MiningRenewApplication != null && obj_MiningRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, obj_MiningRenewApplication, null, null, strActionName, strStatus);
            else if (obj_InfrastructureNewApplication != null && obj_InfrastructureNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, null, obj_InfrastructureNewApplication, null, strActionName, strStatus);
            else if (obj_InfrastructureRenewApplication != null && obj_InfrastructureRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, null, null, obj_InfrastructureRenewApplication, strActionName, strStatus);
            strStatus = "Start Payment";
        }


    }

    protected void btnEC_Click(object sender, EventArgs e)
    {

        strActionName = "Single Payment for EC-" + (rdbtnEC.SelectedValue == "0" ? "NEFT/RTGS" : "Online");
        strStatus = "Start Payment";
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null;
        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = null;
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = null;
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = null;
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = null;
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = null;

        NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_InfrastructureNewApplication, out obj_MiningNewApplication,
            out obj_IndustrialRenewApplication, out obj_InfrastructureRenewApplication, out obj_MiningRenewApplication, Convert.ToInt64(lblAppCode.Text));

        Dictionary<int, decimal> dict = new Dictionary<int, decimal>();

        if (txtEC.Text.Trim() != "")
        {
            dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.EC, Convert.ToDecimal(txtEC.Text.Trim()));
        }
        if (rdbtnEC.SelectedValue == "1")
        {
            if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, obj_IndustrialNewApplication, null, null, null, null, null, strActionName, strStatus);
            else if (obj_IndustrialRenewApplication != null && obj_IndustrialRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, obj_IndustrialRenewApplication, null, null, null, null, strActionName, strStatus);
            else if (obj_MiningNewApplication != null && obj_MiningNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, obj_MiningNewApplication, null, null, null, strActionName, strStatus);
            else if (obj_MiningRenewApplication != null && obj_MiningRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, obj_MiningRenewApplication, null, null, strActionName, strStatus);
            else if (obj_InfrastructureNewApplication != null && obj_InfrastructureNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, null, obj_InfrastructureNewApplication, null, strActionName, strStatus);
            else if (obj_InfrastructureRenewApplication != null && obj_InfrastructureRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, null, null, obj_InfrastructureRenewApplication, strActionName, strStatus);

        }
        else
        {

            if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, obj_IndustrialNewApplication, null, null, null, null, null, strActionName, strStatus);
            else if (obj_IndustrialRenewApplication != null && obj_IndustrialRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, obj_IndustrialRenewApplication, null, null, null, null, strActionName, strStatus);
            else if (obj_MiningNewApplication != null && obj_MiningNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, obj_MiningNewApplication, null, null, null, strActionName, strStatus);
            else if (obj_MiningRenewApplication != null && obj_MiningRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, obj_MiningRenewApplication, null, null, strActionName, strStatus);
            else if (obj_InfrastructureNewApplication != null && obj_InfrastructureNewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, null, obj_InfrastructureNewApplication, null, strActionName, strStatus);
            else if (obj_InfrastructureRenewApplication != null && obj_InfrastructureRenewApplication.CreatedByExUC > 0)
                CreateXmlParam(dict, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, 0, null, null, null, null, null, null, obj_InfrastructureRenewApplication, strActionName, strStatus);

        }


    }
    #endregion
    #endregion

    protected void rdBtnPayMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str_msg = "";

        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = null;
        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = null;
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = null;
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = null;
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = null;
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = null;
        NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_industrialNewApplication, out obj_infrastructureNewApplication, out obj_MiningNewApplication, out obj_IndustrialRenewApplication, out obj_infrastructureRenewApplication, out obj_MiningRenewApplication, Convert.ToInt64(lblAppCode.Text));

        if (rdBtnPayMode.SelectedValue.ToString() != "")
        {
            if (Convert.ToInt32(rdBtnPayMode.SelectedValue.ToString()) == 1)
            {

                PayBtn.Visible = true;
                singleCharge.Visible = false;
                singlePenalty.Visible = false;
                singleCorrection.Visible = false;
                singleEC.Visible = false;
                if (obj_industrialNewApplication != null && obj_industrialNewApplication.CreatedByExUC > 0)
                    obj_industrialNewApplication.SetPaymentTypeMode(Convert.ToInt64(lblAppCode.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, out str_msg);
                if (obj_IndustrialRenewApplication != null && obj_IndustrialRenewApplication.CreatedByExUC > 0)
                    obj_IndustrialRenewApplication.SetPaymentTypeMode(Convert.ToInt64(lblAppCode.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, out str_msg);
                if (obj_infrastructureNewApplication != null && obj_infrastructureNewApplication.CreatedByExUC > 0)
                    obj_infrastructureNewApplication.SetPaymentTypeMode(Convert.ToInt64(lblAppCode.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, out str_msg);
                if (obj_infrastructureRenewApplication != null && obj_infrastructureRenewApplication.CreatedByExUC > 0)
                    obj_infrastructureRenewApplication.SetPaymentTypeMode(Convert.ToInt64(lblAppCode.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, out str_msg);

                if (obj_MiningNewApplication != null && obj_MiningNewApplication.CreatedByExUC > 0)
                    obj_MiningNewApplication.SetPaymentTypeMode(Convert.ToInt64(lblAppCode.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, out str_msg);
                if (obj_MiningRenewApplication != null && obj_MiningRenewApplication.CreatedByExUC > 0)
                    obj_MiningRenewApplication.SetPaymentTypeMode(Convert.ToInt64(lblAppCode.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, out str_msg);

            }
            else
            {

                if (obj_industrialNewApplication != null && obj_industrialNewApplication.CreatedByExUC > 0)
                    obj_industrialNewApplication.SetPaymentTypeMode(Convert.ToInt64(lblAppCode.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, out str_msg);
                if (obj_IndustrialRenewApplication != null && obj_IndustrialRenewApplication.CreatedByExUC > 0)
                    obj_IndustrialRenewApplication.SetPaymentTypeMode(Convert.ToInt64(lblAppCode.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, out str_msg);
                if (obj_infrastructureNewApplication != null && obj_infrastructureNewApplication.CreatedByExUC > 0)
                    obj_infrastructureNewApplication.SetPaymentTypeMode(Convert.ToInt64(lblAppCode.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, out str_msg);
                if (obj_infrastructureRenewApplication != null && obj_infrastructureRenewApplication.CreatedByExUC > 0)
                    obj_infrastructureRenewApplication.SetPaymentTypeMode(Convert.ToInt64(lblAppCode.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, out str_msg);

                if (obj_MiningNewApplication != null && obj_MiningNewApplication.CreatedByExUC > 0)
                    obj_MiningNewApplication.SetPaymentTypeMode(Convert.ToInt64(lblAppCode.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, out str_msg);
                if (obj_MiningRenewApplication != null && obj_MiningRenewApplication.CreatedByExUC > 0)
                    obj_MiningRenewApplication.SetPaymentTypeMode(Convert.ToInt64(lblAppCode.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, out str_msg);


                PayBtn.Visible = false;
                singleCharge.Visible = true;
                singlePenalty.Visible = true;
                singleCorrection.Visible = true;
                singleEC.Visible = true;

            }
        }

    }


    #region DDL
    protected void ddlApplicationType_SelectedIndexChanged(object sender, EventArgs e)
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
                pnlPaymentDetail.Visible = false;
                if (ddlApplicationType.SelectedValue != "")
                {
                    if (!NOCAPExternalUtility.IsNumeric(ddlApplicationType.SelectedValue))
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Application Code allows only Numeric Value.');", true);
                        return;
                    }

                    if (NOCAPExternalUtility.FillDropDownApplicationNumber(ref ddlApplicationNumber, Convert.ToInt64(Session["ExternalUserCode"]), Convert.ToInt32(ddlApplicationType.SelectedValue)) != 1)
                    {
                        lblMessage.Text = "Problem in Application Type";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        ddlApplicationNumber.Enabled = false;
                    }
                    else
                        ddlApplicationNumber.Enabled = true;
                }
                else
                {
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlApplicationNumber);
                    //ddlApplicationNumber.SelectedValue = "";
                    //ddlApplicationNumber.Enabled = false;
                    //ddlApplicationNumber.Text = "";
                    //ddlApplicationNumber.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = "Please select Application Type.";
            }

        }
    }
    protected void ddlApplicationNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlPaymentDetail.Visible = true;
        lblMessage.Text = "";
        BindData(sender, e);       
    }
    #endregion

}