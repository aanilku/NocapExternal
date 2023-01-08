using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP;
using System.Configuration;
using System.IO;
using System.Threading;

public partial class ExternalUser_ApplicantHome : System.Web.UI.Page
{
    public long lng_IndAppCode = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack != true)
            {


                // Session["ExternalUserCode"] = 3101;

                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

                btnIndustrialTab.CssClass = "Clicked";
                btnInfrastructureTab.CssClass = "Initial";
                btnMiningTab.CssClass = "Initial";
                MainView.ActiveViewIndex = 0;

                BindIndNewSubmittedData();
                BindIndNewSaveAsDraftData();
                BindIndRenewSaveAsDraftData();





                BindInfNewSubmittedData();
                BindInfNewSaveAsDraftData();
                BindInfRenewSaveAsDraftData();


                BindIndExpansionSaveAsDraftData();
                BindINFExpansionSaveAsDraftData();
                BindMINExpansionSaveAsDraftData();


                BindminNewSaveAsDraftData();
                BindMinNewSubmittedData();
                BindMinRenewSaveAsDraftData();

            }

        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }


    #region Private

    private void BindIndNewSubmittedData()
    {
        try
        {
            if (NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
            {

                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));

                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication[] arrIndAppCount = obj_externalUser.GetSubmittedIndustrialNewApplicationList();
                gvIndNewSubmitted.DataSource = arrIndAppCount;
                gvIndNewSubmitted.DataBind();
                if (arrIndAppCount.Count() != 0)
                {
                    lblIndAppCount.Text = HttpUtility.HtmlEncode("(Count : " + arrIndAppCount.Count() + ")");  //added html encode lblIndAppCount.Text = "(Count : " + arrIndAppCount.Count() + ")";
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindIndNewSaveAsDraftData()
    {
        try
        {
            if (NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
            {

                int int_indAppTypeCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Industrial");
                NOCAP.BLL.Master.SADValidity objSADValidity = new NOCAP.BLL.Master.SADValidity(int_indAppTypeCode);
                if (objSADValidity != null)
                {
                    lblMsgForInd.Text = HttpUtility.HtmlEncode(Convert.ToString(objSADValidity.AllowNoOfMaxApplication));  //added html encode lblMsgForInd.Text = Convert.ToString(objSADValidity.AllowNoOfMaxApplication);
                    lblSadValidityInd.Text = HttpUtility.HtmlEncode(Convert.ToString(objSADValidity.NoOfMonthsForValidity));  //added html encode lblSadValidityInd.Text = Convert.ToString(objSADValidity.NoOfMonthsForValidity);
                }


                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));


                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication[] arrIndSADAppCount = obj_externalUser.GetSaveAsDraftIndustrialNewApplicationList();
                gvIndNewSaveAsDraft.DataSource = arrIndSADAppCount;
                gvIndNewSaveAsDraft.DataBind();

                if (arrIndSADAppCount.Count() != 0)
                {
                    lblIndSADAppCount.Text = HttpUtility.HtmlEncode("(Count : " + arrIndSADAppCount.Count() + ")");  //added html encode lblIndSADAppCount.Text = "(Count : " + arrIndSADAppCount.Count() + ")";
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void BindIndRenewSaveAsDraftData()
    {
        try
        {
            if (NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
            {


                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                gvIndRenewSaveAsDraft.DataSource = obj_externalUser.GetSaveAsDraftIndustrialRenewApplicationList();
                gvIndRenewSaveAsDraft.DataBind();

            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void BindIndExpansionSaveAsDraftData()
    {
        try
        {
            if (NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
            {


                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                gvIndExpansionSaveAsDraft.DataSource = obj_externalUser.GetSaveAsDraftIndustrialExpansionApplicationList();
                gvIndExpansionSaveAsDraft.DataBind();

            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void BindINFExpansionSaveAsDraftData()
    {
        try
        {
            if (NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
            {

                int int_infAppCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Infrastructure");
                NOCAP.BLL.Master.SADValidity objSADValidity = new NOCAP.BLL.Master.SADValidity(int_infAppCode);
                if (objSADValidity != null)
                {
                    lblMsgForInFExpansion.Text = Convert.ToString(HttpUtility.HtmlEncode(objSADValidity.AllowNoOfMaxApplication));  //added html encode lblMsgForInf.Text = Convert.ToString(objSADValidity.AllowNoOfMaxApplication);
                    lblSadValidityInFExpansion.Text = Convert.ToString(HttpUtility.HtmlEncode(objSADValidity.NoOfMonthsForValidity));  //added html encode lblSadValidityInf.Text = Convert.ToString(objSADValidity.NoOfMonthsForValidity);
                }

                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));

                NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication[] arrInfSADAppCount = obj_externalUser.GetSaveAsDraftInfrastructureExpansionApplicationList();
                gvInFExpansionSaveAsDraft.DataSource = arrInfSADAppCount;
                gvInFExpansionSaveAsDraft.DataBind();



                if (arrInfSADAppCount.Count() != 0)
                {
                    lblInfSADAppCount.Text = HttpUtility.HtmlEncode("(Count : " + arrInfSADAppCount.Count() + ")");   // added html encode lblInfSADAppCount.Text = "(Count : " + arrInfSADAppCount.Count() + ")";
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

        //try
        //{
        //    if (NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
        //    {


        //        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
        //        gvInFExpansionSaveAsDraft.DataSource = obj_externalUser.GetSaveAsDraftIndustrialExpansionApplicationList();
        //        gvInFExpansionSaveAsDraft.DataBind();

        //    }
        //}
        //catch (Exception)
        //{
        //    Response.Redirect("~/ExternalErrorPage.aspx", false);
        //}
    }

    private void BindMINExpansionSaveAsDraftData()
    {

        try
        {
            if (NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
            {
                int int_minAppCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Mining");
                NOCAP.BLL.Master.SADValidity objSADValidity = new NOCAP.BLL.Master.SADValidity(int_minAppCode);
                if (objSADValidity != null)
                {
                    lblMsgForMinExpansion.Text = Convert.ToString(HttpUtility.HtmlEncode(objSADValidity.AllowNoOfMaxApplication));  //added html encode lblMsgForMin.Text = Convert.ToString(objSADValidity.AllowNoOfMaxApplication);
                    lblExpansionSADValidityMin.Text = Convert.ToString(HttpUtility.HtmlEncode(objSADValidity.NoOfMonthsForValidity));  //added html encode lblSADValidityMin.Text = Convert.ToString(objSADValidity.NoOfMonthsForValidity);
                }
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                NOCAP.BLL.Mining.Expansion.MiningExpansionApplication[] arr = obj_externalUser.GetSaveAsDraftMiningExpansionApplicationList();

                gvMINExpansionSaveAsDraft.DataSource = arr;
                gvMINExpansionSaveAsDraft.DataBind();

                //NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication[] arrMinSADAppCount = obj_externalUser.GetSaveAsDraftMiningNewApplicationList();
                if (arr.Count() != 0)
                {
                    lblMinSADAppCount.Text = HttpUtility.HtmlEncode("(Count : " + arr.Count() + ")");  //added html enclode lblMinSADAppCount.Text = "(Count : " + arrMinSADAppCount.Count() + ")";
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
       
    }

    private void BindInfRenewSaveAsDraftData()
    {
        try
        {
            if (NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                gvInfRenewSaveAsDraft.DataSource = obj_externalUser.GetSaveAsDraftInfrastructureRenewApplicationList();
                gvInfRenewSaveAsDraft.DataBind();

            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void BindMinRenewSaveAsDraftData()
    {
        try
        {
            if (NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                gvMinRenewSaveAsDraft.DataSource = obj_externalUser.GetSaveAsDraftMiningRenewApplicationList();
                gvMinRenewSaveAsDraft.DataBind();
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void BindInfNewSubmittedData()
    {
        try
        {
            if (NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication[] arrInfAppCount = obj_externalUser.GetSubmittedInfrastructureNewApplicationList();
                gvInfNewSubmitted.DataSource = arrInfAppCount;
                gvInfNewSubmitted.DataBind();


                if (arrInfAppCount.Count() != 0)
                {
                    lblInfAppCount.Text = HttpUtility.HtmlEncode("(Count : " + arrInfAppCount.Count() + ")"); //added html encode lblInfAppCount.Text = "(Count : " + arrInfAppCount.Count() + ")";
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindInfNewSaveAsDraftData()
    {
        try
        {
            if (NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
            {

                int int_infAppCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Infrastructure");
                NOCAP.BLL.Master.SADValidity objSADValidity = new NOCAP.BLL.Master.SADValidity(int_infAppCode);
                if (objSADValidity != null)
                {
                    lblMsgForInf.Text = Convert.ToString(HttpUtility.HtmlEncode(objSADValidity.AllowNoOfMaxApplication));  //added html encode lblMsgForInf.Text = Convert.ToString(objSADValidity.AllowNoOfMaxApplication);
                    lblSadValidityInf.Text = Convert.ToString(HttpUtility.HtmlEncode(objSADValidity.NoOfMonthsForValidity));  //added html encode lblSadValidityInf.Text = Convert.ToString(objSADValidity.NoOfMonthsForValidity);
                }

                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication[] arrInfSADAppCount = obj_externalUser.GetSaveAsDraftInfrastructureNewApplicationList();
                gvInfNewSaveAsDraft.DataSource = arrInfSADAppCount;
                gvInfNewSaveAsDraft.DataBind();



                if (arrInfSADAppCount.Count() != 0)
                {
                    lblInfSADAppCount.Text = HttpUtility.HtmlEncode("(Count : " + arrInfSADAppCount.Count() + ")");   // added html encode lblInfSADAppCount.Text = "(Count : " + arrInfSADAppCount.Count() + ")";
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void BindMinNewSubmittedData()
    {
        try
        {
            if (NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                NOCAP.BLL.Mining.New.Application.MiningNewApplication[] arrMinAppCount = obj_externalUser.GetSubmittedMiningNewApplicationList();
                gvMinNewSubmitted.DataSource = arrMinAppCount;
                gvMinNewSubmitted.DataBind();


                if (arrMinAppCount.Count() != 0)
                {
                    lblMinAppCount.Text = HttpUtility.HtmlEncode("(Count : " + arrMinAppCount.Count() + ")");  // added html encode lblMinAppCount.Text = "(Count : " + arrMinAppCount.Count() + ")";
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindminNewSaveAsDraftData()
    {
        try
        {
            if (NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
            {
                int int_minAppCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Mining");
                NOCAP.BLL.Master.SADValidity objSADValidity = new NOCAP.BLL.Master.SADValidity(int_minAppCode);
                if (objSADValidity != null)
                {
                    lblMsgForMin.Text = Convert.ToString(HttpUtility.HtmlEncode(objSADValidity.AllowNoOfMaxApplication));  //added html encode lblMsgForMin.Text = Convert.ToString(objSADValidity.AllowNoOfMaxApplication);
                    lblSADValidityMin.Text = Convert.ToString(HttpUtility.HtmlEncode(objSADValidity.NoOfMonthsForValidity));  //added html encode lblSADValidityMin.Text = Convert.ToString(objSADValidity.NoOfMonthsForValidity);
                }
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication[] arr = obj_externalUser.GetSaveAsDraftMiningNewApplicationList();
                gvMINNewSaveAsDraft.DataSource = arr;
                gvMINNewSaveAsDraft.DataBind();

                //NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication[] arrMinSADAppCount = obj_externalUser.GetSaveAsDraftMiningNewApplicationList();
                if (arr.Count() != 0)
                {
                    lblMinSADAppCount.Text = HttpUtility.HtmlEncode("(Count : " + arr.Count() + ")");  //added html enclode lblMinSADAppCount.Text = "(Count : " + arrMinSADAppCount.Count() + ")";
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void EnableDisableLinkButton(NOCAP.BLL.Common.CommonEnum.YesNoOption enu_NTRPIntegration, long lngA_ApplicationCode,
        LinkButton lnkbtnEdit, LinkButton lnkbtnPayment, LinkButton lnkbtnSubmit,
        NOCAP.BLL.Common.CommonEnum.YesNoOption enm_ReadyToSubmit,
        NOCAP.BLL.Common.CommonEnum.PaymentTypeMode enm_PaymentTypeMode
        , int intA_WaterQualityCode
        , NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_ProFeeSADOnlinePayment
        , NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_GWChargeSADOnlinePayment
        , NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_PenaltySADOnlinePayment
        , int intA_AreaTypeCategoryCode)
    {
        try
        {
            NOCAP.BLL.Misc.Payment.SADOnlinePayment obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment();
            obj_SADOnlinePayment.ApplicationCode = lngA_ApplicationCode;
            NOCAP.BLL.Misc.Payment.SADOnlinePayment[] arr_SADOnlinePayment = null;

            NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails obj_SADOnlinePaymentDetails = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails(lngA_ApplicationCode);
            NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails[] arr_SADOnlinePaymentDetails = null;
            if (enu_NTRPIntegration == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
            {
                if (enm_ReadyToSubmit == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                {
                    #region PaymentTypeMode Combined
                    if (enm_PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined)
                    {
                        if (obj_SADOnlinePayment.GetALL() == 1)
                        {
                            arr_SADOnlinePayment = obj_SADOnlinePayment.SADOnlinePaymentCollection
                             .Where(x => x.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success
                             || x.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Pending).ToArray();
                            if (arr_SADOnlinePayment != null && arr_SADOnlinePayment.Length > 0)
                            {
                                lnkbtnPayment.Enabled = false;
                                lnkbtnEdit.Enabled = false;
                            }
                            else
                            {
                                lnkbtnPayment.Enabled = true;
                                lnkbtnEdit.Enabled = true;
                            }
                        }
                        else
                        {
                            lnkbtnSubmit.Enabled = false;

                        }
                        //if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                        //{
                        //    switch (objA_ProFeeSADOnlinePayment.FinalPaymentStatus)
                        //    {
                        //        case NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Failed:
                        //            lnkbtnPayment.Enabled = true;
                        //            lnkbtnEdit.Enabled = true;
                        //            break;
                        //        case NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success:
                        //            lnkbtnPayment.Enabled = false;
                        //            lnkbtnEdit.Enabled = false;
                        //            break;
                        //        case NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Pending:
                        //            lnkbtnPayment.Enabled = false;
                        //            lnkbtnEdit.Enabled = false;
                        //            lnkbtnSubmit.Enabled = false;
                        //            break;
                        //        case NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.NotDefined:
                        //            lnkbtnPayment.Enabled = false;
                        //            lnkbtnEdit.Enabled = false;
                        //            break;
                        //    }
                        //}
                        //else
                        //{
                        //    lnkbtnSubmit.Enabled = false;
                        //}

                    }

                    #endregion

                    #region PaymentTypeMode Single
                    else if (enm_PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single)
                    {


                        #region Edit Button Enable/Disable

                        if (obj_SADOnlinePayment.GetALL() == 1)
                        {
                            arr_SADOnlinePayment = obj_SADOnlinePayment.SADOnlinePaymentCollection
                             .Where(x => x.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success
                             || x.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Pending
                             ).ToArray();
                            if (arr_SADOnlinePayment != null && arr_SADOnlinePayment.Length > 0)
                                lnkbtnEdit.Enabled = false;
                            else
                                lnkbtnEdit.Enabled = true;
                        }
                        //if ((objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0) || (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0) || (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0))
                        //{
                        //    #region ProFeeSADOnlinePayment
                        //    if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                        //    {
                        //        switch (objA_ProFeeSADOnlinePayment.FinalPaymentStatus)
                        //        {
                        //            case NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Failed:

                        //                lnkbtnEdit.Enabled = true;
                        //                break;
                        //            case NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success:

                        //                lnkbtnEdit.Enabled = false;
                        //                break;
                        //            case NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Pending:

                        //                lnkbtnEdit.Enabled = false;
                        //                break;
                        //            case NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.NotDefined:

                        //                lnkbtnEdit.Enabled = false;
                        //                break;

                        //        }
                        //    }
                        //    #endregion

                        //    #region GWChargeSADOnlinePayment
                        //    if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                        //    {
                        //        switch (objA_GWChargeSADOnlinePayment.FinalPaymentStatus)
                        //        {
                        //            case NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Failed:

                        //                lnkbtnEdit.Enabled = true;
                        //                break;
                        //            case NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success:

                        //                lnkbtnEdit.Enabled = false;
                        //                break;
                        //            case NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Pending:

                        //                lnkbtnEdit.Enabled = false;
                        //                break;
                        //            case NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.NotDefined:

                        //                lnkbtnEdit.Enabled = false;
                        //                break;

                        //        }
                        //    }
                        //    #endregion

                        //    #region PenaltySADOnlinePayment
                        //    if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                        //    {
                        //        switch (objA_PenaltySADOnlinePayment.FinalPaymentStatus)
                        //        {
                        //            case NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Failed:

                        //                lnkbtnEdit.Enabled = true;
                        //                break;
                        //            case NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success:

                        //                lnkbtnEdit.Enabled = false;
                        //                break;
                        //            case NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Pending:

                        //                lnkbtnEdit.Enabled = false;
                        //                break;
                        //            case NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.NotDefined:

                        //                lnkbtnEdit.Enabled = false;
                        //                break;

                        //        }
                        //    }
                        //    #endregion

                        //}

                        #endregion

                        #region MakePayment Button Enable/Disable
                        string str_ProFee = "N", str_gwCharge = "N", str_Penalty = "N";
                        if (obj_SADOnlinePayment.GetALL() == 1)
                        {
                            arr_SADOnlinePayment = obj_SADOnlinePayment.SADOnlinePaymentCollection
                             .Where(x => x.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success).ToArray();
                            if (arr_SADOnlinePayment != null && arr_SADOnlinePayment.Length > 0)
                            {
                                for (int i = 0; i < arr_SADOnlinePayment.Length; i++)
                                {
                                    obj_SADOnlinePaymentDetails = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails(arr_SADOnlinePayment[i].ApplicationCode, arr_SADOnlinePayment[i].OrderPaymentCode, 1);
                                    if (obj_SADOnlinePaymentDetails != null && obj_SADOnlinePaymentDetails.CreatedByExUC > 0)
                                        str_ProFee = "Y";
                                    if (intA_WaterQualityCode == 1)
                                    {
                                        if (intA_AreaTypeCategoryCode == 5)
                                            obj_SADOnlinePaymentDetails = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails(arr_SADOnlinePayment[i].ApplicationCode, arr_SADOnlinePayment[i].OrderPaymentCode, 2);
                                        else
                                            obj_SADOnlinePaymentDetails = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails(arr_SADOnlinePayment[i].ApplicationCode, arr_SADOnlinePayment[i].OrderPaymentCode, 3);
                                        if (obj_SADOnlinePaymentDetails != null && obj_SADOnlinePaymentDetails.CreatedByExUC > 0)
                                            str_gwCharge = "Y";
                                    }

                                    obj_SADOnlinePaymentDetails = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails(arr_SADOnlinePayment[i].ApplicationCode, arr_SADOnlinePayment[i].OrderPaymentCode, 4);
                                    if (obj_SADOnlinePaymentDetails != null && obj_SADOnlinePaymentDetails.CreatedByExUC > 0)
                                        str_Penalty = "Y";
                                }
                            }

                        }


                        //if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                        //{
                        //    if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                        //        str_ProFee = "Y";
                        //    else
                        //        str_ProFee = "N";
                        //}

                        //if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                        //{
                        //    if (objA_GWChargeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                        //        str_gwCharge = "Y";
                        //    else
                        //        str_gwCharge = "N";
                        //}
                        //if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                        //{
                        //    if (objA_PenaltySADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                        //        str_Penalty = "Y";
                        //    else
                        //        str_Penalty = "N";
                        //}

                        if (NOCAPExternalUtility.CheckPenaltyForSADApplication(lngA_ApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes && intA_WaterQualityCode == 1)
                        {

                            if (str_ProFee == "Y" && str_gwCharge == "Y" && str_Penalty == "Y")
                                lnkbtnPayment.Enabled = false;
                            else
                                lnkbtnPayment.Enabled = true;
                        }
                        else
                        {
                            if (intA_WaterQualityCode == 1)
                            {
                                if (str_ProFee == "Y" && str_gwCharge == "Y")
                                    lnkbtnPayment.Enabled = false;
                                else
                                    lnkbtnPayment.Enabled = true;
                            }
                            else if (NOCAPExternalUtility.CheckPenaltyForSADApplication(lngA_ApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                            {
                                if (str_ProFee == "Y" && str_Penalty == "Y")
                                    lnkbtnPayment.Enabled = false;
                                else
                                    lnkbtnPayment.Enabled = true;
                            }
                            else
                            {
                                if (str_ProFee == "Y")
                                    lnkbtnPayment.Enabled = false;
                                else
                                    lnkbtnPayment.Enabled = true;
                            }

                        }
                        #endregion

                        #region Submit Button Enable/Disable
                        string str_subProFee = "N", str_subgwCharge = "N", str_subPenalty = "N";
                        if (obj_SADOnlinePayment.GetALL() == 1)
                        {
                            arr_SADOnlinePayment = obj_SADOnlinePayment.SADOnlinePaymentCollection
                             .Where(x => x.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success).ToArray();
                            if (arr_SADOnlinePayment != null && arr_SADOnlinePayment.Length > 0)
                            {
                                for (int i = 0; i < arr_SADOnlinePayment.Length; i++)
                                {
                                    obj_SADOnlinePaymentDetails = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails(arr_SADOnlinePayment[i].ApplicationCode, arr_SADOnlinePayment[i].OrderPaymentCode, 1);
                                    if (obj_SADOnlinePaymentDetails != null && obj_SADOnlinePaymentDetails.CreatedByExUC > 0)
                                        str_subProFee = "Y";
                                    if (intA_WaterQualityCode == 1)
                                    {
                                        if (intA_AreaTypeCategoryCode == 5)
                                            obj_SADOnlinePaymentDetails = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails(arr_SADOnlinePayment[i].ApplicationCode, arr_SADOnlinePayment[i].OrderPaymentCode, 2);
                                        else
                                            obj_SADOnlinePaymentDetails = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails(arr_SADOnlinePayment[i].ApplicationCode, arr_SADOnlinePayment[i].OrderPaymentCode, 3);
                                        if (obj_SADOnlinePaymentDetails != null && obj_SADOnlinePaymentDetails.CreatedByExUC > 0)
                                            str_subgwCharge = "Y";
                                    }

                                    obj_SADOnlinePaymentDetails = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails(arr_SADOnlinePayment[i].ApplicationCode, arr_SADOnlinePayment[i].OrderPaymentCode, 4);
                                    if (obj_SADOnlinePaymentDetails != null && obj_SADOnlinePaymentDetails.CreatedByExUC > 0)
                                        str_subPenalty = "Y";
                                }
                            }

                        }
                        //if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                        //{
                        //    if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                        //        str_subProFee = "Y";
                        //    else
                        //        str_subProFee = "N";
                        //}
                        //if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                        //{
                        //    if (objA_GWChargeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                        //        str_subgwCharge = "Y";
                        //    else
                        //        str_subgwCharge = "N";
                        //}
                        //if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                        //{
                        //    if (objA_PenaltySADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                        //        str_subPenalty = "Y";
                        //    else
                        //        str_subPenalty = "N";
                        //}

                        if (NOCAPExternalUtility.CheckPenaltyForSADApplication(lngA_ApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes && intA_WaterQualityCode == 1)
                        {
                            if (str_subProFee == "Y" && str_subgwCharge == "Y" && str_subPenalty == "Y")
                                lnkbtnSubmit.Enabled = true;
                            else
                                lnkbtnSubmit.Enabled = false;
                        }
                        else
                        {

                            if (intA_WaterQualityCode == 1)
                            {
                                if (str_subProFee == "Y" && str_subgwCharge == "Y")
                                    lnkbtnSubmit.Enabled = true;
                                else
                                    lnkbtnSubmit.Enabled = false;
                            }
                            else if (NOCAPExternalUtility.CheckPenaltyForSADApplication(lngA_ApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                            {
                                if (str_subProFee == "Y" && str_subPenalty == "Y")
                                    lnkbtnSubmit.Enabled = true;
                                else
                                    lnkbtnSubmit.Enabled = false;
                            }
                            else
                            {
                                if (str_subProFee == "Y")
                                    lnkbtnSubmit.Enabled = true;
                                else
                                    lnkbtnSubmit.Enabled = false;
                            }
                        }

                        #endregion

                    }
                    else
                    {
                        lnkbtnSubmit.Enabled = false;
                    }

                    #endregion
                }
                else
                {
                   // lnkbtnPayment.Enabled = false;
                    lnkbtnSubmit.Enabled = false;
                }
            }
            else if (enm_ReadyToSubmit == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes && enu_NTRPIntegration == NOCAP.BLL.Common.CommonEnum.YesNoOption.No)
            {
                lnkbtnSubmit.Enabled = true;
              //  lnkbtnPayment.Enabled = false;
            }
            else
            {
              //  lnkbtnPayment.Enabled = false;
                lnkbtnSubmit.Enabled = false;
            }



            if (enu_NTRPIntegration == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes && objA_ProFeeSADOnlinePayment == null && objA_GWChargeSADOnlinePayment == null && objA_PenaltySADOnlinePayment == null)
            {
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewSADApplication = null;
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewSADApplication = null;
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewSADApplication = null;
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplication = null;
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_miningNewSADApplication = null;
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewSADApplication = null;

                NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialExpansionApplication = null;
                NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication obj_infrastructureExpansionApplication = null;
                NOCAP.BLL.Mining.Expansion.MiningExpansionApplication obj_miningExpansionApplication = null;


                NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_industrialNewSADApplication, out obj_infrastructureNewSADApplication,
                    out obj_miningNewSADApplication, out obj_industrialRenewSADApplication, out obj_infrastructureRenewSADApplication, out obj_miningRenewSADApplication,                   
                    lngA_ApplicationCode);
                NOCAP.BLL.Utility.Utility.GetExpansionAppplicationObjectForApplicationCode(out obj_industrialExpansionApplication, out obj_infrastructureExpansionApplication, out obj_miningExpansionApplication,
                   lngA_ApplicationCode);

                #region obj_industrialNewSADApplication
                if (obj_industrialNewSADApplication != null && obj_industrialNewSADApplication.CreatedByExUC > 0)
                {

                    if (obj_industrialNewSADApplication.GetBharatKoshRecieptAttachmentList().Length > 0)
                        lnkbtnSubmit.Enabled = true;
                    else
                    {
                        lnkbtnSubmit.Enabled = false;
                        return;
                    }
                    if (obj_industrialNewSADApplication.WaterQualityCode == 1)
                    {
                        if (obj_industrialNewSADApplication.GetAbsRestChargeAttachmentList().Length > 0)
                            lnkbtnSubmit.Enabled = true;
                        else
                        {
                            lnkbtnSubmit.Enabled = false;
                            return;
                        }
                    }

                    switch (obj_industrialNewSADApplication.GroundWaterUtilizationFor)
                    {
                        case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry:
                            NOCAP.BLL.Master.Penalty obj_Penalty = new NOCAP.BLL.Master.Penalty(17);
                            switch (obj_industrialNewSADApplication.NOCObtainForExistIndustry.NOCObtainForExistIND)
                            {
                                case NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No:
                                    if (obj_industrialNewSADApplication.GetPenaltyAttachmentList().Length > 0)
                                        lnkbtnSubmit.Enabled = true;
                                    else { lnkbtnSubmit.Enabled = false; return; }
                                    break;
                            }
                            break;
                    }
                }
                #endregion

                #region obj_industrialRenewSADApplication
                if (obj_industrialRenewSADApplication != null && obj_industrialRenewSADApplication.CreatedByExUC > 0)
                {

                    if (obj_industrialRenewSADApplication.GetBharatKoshRecieptAttachmentList().Length > 0)
                        lnkbtnSubmit.Enabled = true;
                    else
                    {
                        lnkbtnSubmit.Enabled = false;
                        return;
                    }
                    if (obj_industrialRenewSADApplication.WaterQualityCode == 1)
                    {
                        if (obj_industrialRenewSADApplication.GetAbsRestChargeAttachmentList().Length > 0)
                            lnkbtnSubmit.Enabled = true;
                        else
                        {
                            lnkbtnSubmit.Enabled = false;
                            return;
                        }
                    }
                    if (NOCAPExternalUtility.CheckPenaltyForSADApplication(lngA_ApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                    {
                        if (obj_industrialRenewSADApplication.GetPenaltyAttachmentList().Length > 0)
                            lnkbtnSubmit.Enabled = true;
                        else { lnkbtnSubmit.Enabled = false; return; }
                    }

                }
                #endregion

                #region obj_infrastructureNewSADApplication
                if (obj_infrastructureNewSADApplication != null && obj_infrastructureNewSADApplication.CreatedByExUC > 0)
                {

                    if (obj_infrastructureNewSADApplication.GetBharatKoshRecieptAttachmentList().Length > 0)
                        lnkbtnSubmit.Enabled = true;
                    else
                    {
                        lnkbtnSubmit.Enabled = false;
                        return;
                    }
                    if (obj_infrastructureNewSADApplication.WaterQualityCode == 1)
                    {
                        if (obj_infrastructureNewSADApplication.GetAbsRestChargeAttachmentList().Length > 0)
                            lnkbtnSubmit.Enabled = true;
                        else
                        {
                            lnkbtnSubmit.Enabled = false;
                            return;
                        }
                    }

                    switch (obj_infrastructureNewSADApplication.GroundWaterUtilizationFor)
                    {
                        case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry:
                            NOCAP.BLL.Master.Penalty obj_Penalty = new NOCAP.BLL.Master.Penalty(17);
                            switch (obj_infrastructureNewSADApplication.NOCObtainForExistIndustry.NOCObtainForExistIND)
                            {
                                case NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No:
                                    if (obj_infrastructureNewSADApplication.GetPenaltyAttachmentList().Length > 0)
                                        lnkbtnSubmit.Enabled = true;
                                    else { lnkbtnSubmit.Enabled = false; return; }
                                    break;
                            }
                            break;
                    }
                }
                #endregion

                #region obj_infrastructureRenewSADApplication
                if (obj_infrastructureRenewSADApplication != null && obj_infrastructureRenewSADApplication.CreatedByExUC > 0)
                {

                    if (obj_infrastructureRenewSADApplication.GetBharatKoshRecieptAttachmentList().Length > 0)
                        lnkbtnSubmit.Enabled = true;
                    else
                    {
                        lnkbtnSubmit.Enabled = false;
                        return;
                    }
                    if (obj_infrastructureRenewSADApplication.WaterQualityCode == 1)
                    {
                        if (obj_infrastructureRenewSADApplication.GetAbsRestChargeAttachmentList().Length > 0)
                            lnkbtnSubmit.Enabled = true;
                        else
                        {
                            lnkbtnSubmit.Enabled = false;
                            return;
                        }
                    }
                    if (NOCAPExternalUtility.CheckPenaltyForSADApplication(lngA_ApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                    {
                        if (obj_infrastructureRenewSADApplication.GetPenaltyAttachmentList().Length > 0)
                            lnkbtnSubmit.Enabled = true;
                        else { lnkbtnSubmit.Enabled = false; return; }
                    }

                }
                #endregion

                #region obj_miningNewSADApplication
                if (obj_miningNewSADApplication != null && obj_miningNewSADApplication.CreatedByExUC > 0)
                {

                    if (obj_miningNewSADApplication.GetBharatKoshRecieptAttachmentList().Length > 0)
                        lnkbtnSubmit.Enabled = true;
                    else
                    {
                        lnkbtnSubmit.Enabled = false;
                        return;
                    }
                    if (obj_miningNewSADApplication.WaterQualityCode == 1)
                    {
                        if (obj_miningNewSADApplication.GetAbsRestChargeAttachmentList().Length > 0)
                            lnkbtnSubmit.Enabled = true;
                        else
                        {
                            lnkbtnSubmit.Enabled = false;
                            return;
                        }
                    }
                    switch (obj_miningNewSADApplication.GroundWaterUtilizationFor)
                    {
                        case NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry:
                            NOCAP.BLL.Master.Penalty obj_Penalty = new NOCAP.BLL.Master.Penalty(17);
                            switch (obj_miningNewSADApplication.NOCObtainForExistIndustry.NOCObtainForExistIND)
                            {
                                case NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No:
                                    if (obj_miningNewSADApplication.GetPenaltyAttachmentList().Length > 0)
                                        lnkbtnSubmit.Enabled = true;
                                    else { lnkbtnSubmit.Enabled = false; return; }
                                    break;
                            }
                            break;
                    }
                }
                #endregion

                #region obj_miningRenewSADApplication
                if (obj_miningRenewSADApplication != null && obj_miningRenewSADApplication.CreatedByExUC > 0)
                {

                    if (obj_miningRenewSADApplication.GetBharatKoshRecieptAttachmentList().Length > 0)
                        lnkbtnSubmit.Enabled = true;
                    else
                    {
                        lnkbtnSubmit.Enabled = false;
                        return;
                    }
                    if (obj_miningRenewSADApplication.WaterQualityCode == 1)
                    {
                        if (obj_miningRenewSADApplication.GetAbsRestChargeAttachmentList().Length > 0)
                            lnkbtnSubmit.Enabled = true;
                        else
                        {
                            lnkbtnSubmit.Enabled = false;
                            return;
                        }
                    }
                    if (NOCAPExternalUtility.CheckPenaltyForSADApplication(lngA_ApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                    {
                        if (obj_miningRenewSADApplication.GetPenaltyAttachmentList().Length > 0)
                            lnkbtnSubmit.Enabled = true;
                        else { lnkbtnSubmit.Enabled = false; return; }
                    }

                }
                #endregion

                #region obj_industrialExpansionApplication
                if (obj_industrialExpansionApplication != null && obj_industrialExpansionApplication.CreatedByExUC > 0)
                {

                    if (obj_industrialExpansionApplication.BharatKoshRecieptIndustrialApplicationAttachmentList().Length > 0)
                        lnkbtnSubmit.Enabled = true;
                    else
                    {
                        lnkbtnSubmit.Enabled = false;
                        return;
                    }
                    if (obj_industrialExpansionApplication.WaterQualityCode == 1)
                    {
                        if (obj_industrialExpansionApplication.GetAbsRestChargeAttachmentList().Length > 0)
                            lnkbtnSubmit.Enabled = true;
                        else
                        {
                            lnkbtnSubmit.Enabled = false;
                            return;
                        }
                    }

                    switch (obj_industrialExpansionApplication.GroundWaterUtilizationFor)
                    {
                        case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry:
                            NOCAP.BLL.Master.Penalty obj_Penalty = new NOCAP.BLL.Master.Penalty(17);
                            switch (obj_industrialExpansionApplication.NOCObtainForExistIndustry.NOCObtainForExistIND)
                            {
                                case NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No:
                                    if (obj_industrialExpansionApplication.GetPenaltyAttachmentList().Length > 0)
                                        lnkbtnSubmit.Enabled = true;
                                    else { lnkbtnSubmit.Enabled = false; return; }
                                    break;
                            }
                            break;
                    }
                }
                #endregion

                #region obj_infrastructureExpansionApplication
                if (obj_infrastructureExpansionApplication != null && obj_infrastructureExpansionApplication.CreatedByExUC > 0)
                {

                    if (obj_infrastructureExpansionApplication.GetBharatKoshRecieptAttachmentList().Length > 0)
                        lnkbtnSubmit.Enabled = true;
                    else
                    {
                        lnkbtnSubmit.Enabled = false;
                        return;
                    }
                    if (obj_infrastructureExpansionApplication.WaterQualityCode == 1)
                    {
                        if (obj_infrastructureExpansionApplication.GetAbsRestChargeAttachmentList().Length > 0)
                            lnkbtnSubmit.Enabled = true;
                        else
                        {
                            lnkbtnSubmit.Enabled = false;
                            return;
                        }
                    }

                    switch (obj_infrastructureExpansionApplication.GroundWaterUtilizationFor)
                    {
                        case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry:
                            NOCAP.BLL.Master.Penalty obj_Penalty = new NOCAP.BLL.Master.Penalty(17);
                            switch (obj_infrastructureExpansionApplication.NOCObtainForExistIndustry.NOCObtainForExistIND)
                            {
                                case NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No:
                                    if (obj_infrastructureExpansionApplication.GetPenaltyAttachmentList().Length > 0)
                                        lnkbtnSubmit.Enabled = true;
                                    else { lnkbtnSubmit.Enabled = false; return; }
                                    break;
                            }
                            break;
                    }
                }
                #endregion

                #region obj_miningExpansionApplication
                if (obj_miningExpansionApplication != null && obj_miningExpansionApplication.CreatedByExUC > 0)
                {

                    if (obj_miningExpansionApplication.GetBharatKoshRecieptAttachmentList().Length > 0)
                        lnkbtnSubmit.Enabled = true;
                    else
                    {
                        lnkbtnSubmit.Enabled = false;
                        return;
                    }
                    if (obj_miningExpansionApplication.WaterQualityCode == 1)
                    {
                        if (obj_miningExpansionApplication.GetAbsRestChargeAttachmentList().Length > 0)
                            lnkbtnSubmit.Enabled = true;
                        else
                        {
                            lnkbtnSubmit.Enabled = false;
                            return;
                        }
                    }
                    switch (obj_miningExpansionApplication.GroundWaterUtilizationFor)
                    {
                        case NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry:
                            NOCAP.BLL.Master.Penalty obj_Penalty = new NOCAP.BLL.Master.Penalty(17);
                            switch (obj_miningExpansionApplication.NOCObtainForExistIndustry.NOCObtainForExistIND)
                            {
                                case NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No:
                                    if (obj_miningExpansionApplication.GetPenaltyAttachmentList().Length > 0)
                                        lnkbtnSubmit.Enabled = true;
                                    else { lnkbtnSubmit.Enabled = false; return; }
                                    break;
                            }
                            break;
                    }
                }
                #endregion

            }



        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    #endregion



    #region Tab Click
    protected void btnIndustrialTab_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                btnIndustrialTab.CssClass = "Clicked";
                btnInfrastructureTab.CssClass = "Initial";
                btnMiningTab.CssClass = "Initial";
                btnDomestic.CssClass = "Initial";
                MainView.ActiveViewIndex = 0;
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void btnInfrastructureTab_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                btnIndustrialTab.CssClass = "Initial";
                btnInfrastructureTab.CssClass = "Clicked";
                btnMiningTab.CssClass = "Initial";
                btnDomestic.CssClass = "Initial";
                MainView.ActiveViewIndex = 1;
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    protected void btnMiningTab_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {

                btnIndustrialTab.CssClass = "Initial";
                btnInfrastructureTab.CssClass = "Initial";
                btnMiningTab.CssClass = "Clicked";
                btnDomestic.CssClass = "Initial";
                MainView.ActiveViewIndex = 2;
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    #endregion

    #region Button Click
    protected void lbtnEditInfRenewSaveAsDraft_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandArgument != null)
                {
                    lng_IndAppCode = Convert.ToInt32(e.CommandArgument.ToString());
                    lblMode.Text = "Edit";
                    lblPageTitle.Text = Page.Title;
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }



    protected void lnkMinRenewDetail_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

                if (e.CommandArgument != null)
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);
                    Server.Transfer("~/ExternalUser/MiningRenew/RenewalDetail/MiningRenewDetail.aspx");
                }
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
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;
            }
        }
    }
    protected void lbtnRenewal_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

                if (e.CommandArgument != null)
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());  //added html encode lblApplicationCode.Text = e.CommandArgument.ToString();
                    Server.Transfer("~/ExternalUser/IndustrialRenew/RenewalDetail/IndustrialRenewDetail.aspx");
                }
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
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

            }
        }
    }

    protected void lnkInfRenewDetail_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

                if (e.CommandArgument != null)
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString()); //addedd html encode lblApplicationCode.Text = e.CommandArgument.ToString();
                    Server.Transfer("~/ExternalUser/InfrastructureRenew/RenewalDetail/InfrastructureRenewDetail.aspx");
                }
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
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;
            }
        }
    }

    protected void lnkCompliance_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

                if (e.CommandArgument != null)
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCode.Text));

                    if (obj_selfCompliance.ApplicationCode != 0 && obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                    {
                        //Session["ApplicationCodeForSelfCompliance"] = lblApplicationCode.Text;
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open(' Compliance/Reports/SelfComplianceReportViewer.aspx ','_blank')", true);
                        Server.Transfer("~/ExternalUser/Compliance/Reports/SelfComplianceViewer.aspx");
                    }
                    else
                    {
                        Server.Transfer("~/ExternalUser/Compliance/SelfComplianceA.aspx");
                    }
                }
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
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

            }
        }
    }
    protected void lnkInspection_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

                if (e.CommandArgument != null)
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);
                    NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_SelfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(Convert.ToInt64(lblApplicationCode.Text));

                    if (obj_SelfInspection.ApplicationCode != 0 && obj_SelfInspection.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                    {
                        Server.Transfer("~/ExternalUser/SelfInspection/Reports/SelfInspectionViewer.aspx");
                    }
                    else
                    {
                        Server.Transfer("~/ExternalUser/SelfInspection/SelfInspectionA.aspx");
                    }
                }
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
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

            }
        }
    }
    protected void lnkPayRestCharges_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

                if (e.CommandArgument != null)
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);

                    Session["OnlinePayment"] = Convert.ToInt64(lblApplicationCode.Text);
                    //Server.Transfer("~/ExternalUser/PayOnBharatKosh.aspx");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('../ExternalUser/PayOnBharatKosh.aspx ','_blank')", true);

                }
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
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

            }
        }
    }
    protected void lnkPayAbstractionCharges_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

                if (e.CommandArgument != null)
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);
                    //Server.Transfer("~/ExternalUser/PayOnBharatKosh.aspx");
                    Session["OnlinePayment"] = Convert.ToInt64(lblApplicationCode.Text);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('../ExternalUser/PayOnBharatKosh.aspx ','_blank')", true);

                }
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
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

            }
        }
    }
    protected void lbtnDownload_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                long lngINDAppCode = 0;
                LinkButton button = (LinkButton)sender;
                GridViewRow row = (GridViewRow)button.NamingContainer;
                if (row != null)
                {
                    if (NOCAPExternalUtility.IsNumeric(button.CommandArgument))
                    {
                        lngINDAppCode = Convert.ToInt32(button.CommandArgument);
                    }
                    //NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter objIndustrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter();
                    //objIndustrialNewIssusedLetter.populateIndustrialNewIssusedLetterForINDAppCode(intINDAppCode);
                    //ViewState["path1"] = HttpUtility.HtmlEncode(objIndustrialNewIssusedLetter.AttPath);
                }
                //string strFilePath = ConfigurationManager.AppSettings["NOCAPFilePath"] + ConfigurationManager.AppSettings["NOCAPPDF"] + Path.DirectorySeparatorChar.ToString() + Convert.ToString(ViewState["path1"]);
                //Response.ContentType = ContentType;
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(HttpUtility.HtmlEncode(strFilePath)));
                //Response.WriteFile(HttpUtility.HtmlEncode(strFilePath));


                //LinkButton btn = (LinkButton)sender;
                long lng_indAppCode = lngINDAppCode;
                NOCAPExternalUtility.INDLetterAppDownloadFiles(lng_indAppCode);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

            }
        }
    }

    protected void lbtnInfDownload_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {


            //try
            //{
            //    LinkButton button = (LinkButton)sender;
            //    GridViewRow row = (GridViewRow)button.NamingContainer;
            //    if (row != null)
            //    {
            //        int infcode = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            //        NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter objIndustrialNewIssusedLetter = new NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter();
            //        objIndustrialNewIssusedLetter.populateInfrastructureNewIssusedLetterForINfAppCode(infcode);
            //        ViewState["path1"] = HttpUtility.HtmlEncode(objIndustrialNewIssusedLetter.AttPath);
            //    }
            //    string strFilePath = ConfigurationManager.AppSettings["NOCAPFilePath"] + ConfigurationManager.AppSettings["NOCAPPDF"] + Path.DirectorySeparatorChar.ToString() + Convert.ToString(ViewState["path1"]);
            //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(HttpUtility.HtmlEncode(strFilePath)));
            //    Response.WriteFile(HttpUtility.HtmlEncode(strFilePath));
            //}
            //catch (Exception)
            //{
            //    Response.Redirect("~/ExternalErrorPage.aspx", false);
            //}
            //finally
            //{
            //    Response.End();
            //    hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            //    Session["CSRFs"] = hidCSRFs.Value;
            //}

            try
            {
                long lngINDAppCode = 0;
                LinkButton button = (LinkButton)sender;
                GridViewRow row = (GridViewRow)button.NamingContainer;
                if (row != null)
                {
                    if (NOCAPExternalUtility.IsNumeric(button.CommandArgument))
                    {
                        lngINDAppCode = Convert.ToInt32(button.CommandArgument);
                    }
                    //NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter objIndustrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter();
                    //objIndustrialNewIssusedLetter.populateIndustrialNewIssusedLetterForINDAppCode(intINDAppCode);
                    //ViewState["path1"] = HttpUtility.HtmlEncode(objIndustrialNewIssusedLetter.AttPath);
                }
                //string strFilePath = ConfigurationManager.AppSettings["NOCAPFilePath"] + ConfigurationManager.AppSettings["NOCAPPDF"] + Path.DirectorySeparatorChar.ToString() + Convert.ToString(ViewState["path1"]);
                //Response.ContentType = ContentType;
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(HttpUtility.HtmlEncode(strFilePath)));
                //Response.WriteFile(HttpUtility.HtmlEncode(strFilePath));


                //LinkButton btn = (LinkButton)sender;
                long lng_indAppCode = lngINDAppCode;
                NOCAPExternalUtility.INFLetterAppDownloadFiles(lng_indAppCode);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

            }
        }
    }

    protected void lbtnMinDownload_Click(object sender, EventArgs e)
    {

        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {


            //try
            //{
            //    LinkButton button = (LinkButton)sender;
            //    GridViewRow row = (GridViewRow)button.NamingContainer;
            //    if (row != null)
            //    {
            //        int infcode = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            //        NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter objMiningNewIssusedLetter = new NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter();
            //        objMiningNewIssusedLetter.populateMiningfornewMincode(infcode);
            //        ViewState["path1"] = HttpUtility.HtmlEncode(objMiningNewIssusedLetter.AttPath);
            //    }
            //    string strFilePath = ConfigurationManager.AppSettings["NOCAPFilePath"] + ConfigurationManager.AppSettings["NOCAPPDF"] + Path.DirectorySeparatorChar.ToString() + Convert.ToString(ViewState["path1"]);
            //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(HttpUtility.HtmlEncode(strFilePath)));
            //    Response.WriteFile(HttpUtility.HtmlEncode(strFilePath));
            //}
            //catch (Exception)
            //{
            //    Response.Redirect("~/ExternalErrorPage.aspx", false);
            //}
            //finally
            //{
            //    Response.End();
            //    hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            //    Session["CSRFs"] = hidCSRFs.Value;
            //}

            try
            {
                long lngMINAppCode = 0;
                LinkButton button = (LinkButton)sender;
                GridViewRow row = (GridViewRow)button.NamingContainer;
                if (row != null)
                {
                    if (NOCAPExternalUtility.IsNumeric(button.CommandArgument))
                    {
                        lngMINAppCode = Convert.ToInt32(button.CommandArgument);
                    }
                    //NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter objIndustrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter();
                    //objIndustrialNewIssusedLetter.populateIndustrialNewIssusedLetterForINDAppCode(intINDAppCode);
                    //ViewState["path1"] = HttpUtility.HtmlEncode(objIndustrialNewIssusedLetter.AttPath);
                }
                //string strFilePath = ConfigurationManager.AppSettings["NOCAPFilePath"] + ConfigurationManager.AppSettings["NOCAPPDF"] + Path.DirectorySeparatorChar.ToString() + Convert.ToString(ViewState["path1"]);
                //Response.ContentType = ContentType;
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(HttpUtility.HtmlEncode(strFilePath)));
                //Response.WriteFile(HttpUtility.HtmlEncode(strFilePath));


                //LinkButton btn = (LinkButton)sender;
                long lng_indAppCode = lngMINAppCode;
                NOCAPExternalUtility.MINLetterAppDownloadFiles(lng_indAppCode);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

            }
        }
    }
    protected void lbtnScanInfDownload_Click(object sender, EventArgs e)
    {

        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                int intINFAppCode = 0;
                LinkButton button = (LinkButton)sender;
                string lnkTxt = button.Text;
                GridViewRow row = (GridViewRow)button.NamingContainer;
                if (row != null)
                {
                    if (NOCAPExternalUtility.IsNumeric(button.CommandArgument))
                    {
                        intINFAppCode = Convert.ToInt32(button.CommandArgument);
                    }
                }
                int int_infAppCode = intINFAppCode;
                NOCAPExternalUtility.INFScanLetterDownloadFiles(int_infAppCode);
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

            }
        }
    }

    protected void lbtnScanMinDownload_Click(object sender, EventArgs e)
    {

        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                int intMinAppCode = 0;
                LinkButton button = (LinkButton)sender;
                string lnkTxt = button.Text;
                GridViewRow row = (GridViewRow)button.NamingContainer;
                if (row != null)
                {
                    if (NOCAPExternalUtility.IsNumeric(button.CommandArgument))
                    {
                        intMinAppCode = Convert.ToInt32(button.CommandArgument);
                    }
                }
                int int_infAppCode = intMinAppCode;
                NOCAPExternalUtility.MINScanLetterDownloadFiles(int_infAppCode);
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

            }
        }
    }



    protected void btnDomestic_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                btnIndustrialTab.CssClass = "Initial";
                btnInfrastructureTab.CssClass = "Initial";
                btnMiningTab.CssClass = "Initial";
                btnDomestic.CssClass = "Clicked";
                MainView.ActiveViewIndex = 3;
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }



    protected void lbtnScanDownload_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    int intINDAppCode = 0;
        //    LinkButton button = (LinkButton)sender;
        //    GridViewRow row = (GridViewRow)button.NamingContainer;
        //    if (row != null)
        //    {
        //        if (NOCAPExternalUtility.IsNumeric(button.CommandArgument))
        //        {
        //            intINDAppCode = Convert.ToInt32(button.CommandArgument);
        //        }
        //    }
        //    int int_indAppCode = intINDAppCode;
        //    NOCAPExternalUtility.INDScanLetterDownloadFiles(int_indAppCode);
        //}
        //catch (Exception)
        //{
        //    Response.Redirect("~/ExternalErrorPage.aspx", false);
        //}
        //finally
        //{
        //    Response.End();
        //    hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
        //    Session["CSRFs"] = hidCSRFs.Value;

        //}
        //  ----------------
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                int lngINDAppCode = 0;
                LinkButton button = (LinkButton)sender;
                string lnkTxt = button.Text;
                GridViewRow row = (GridViewRow)button.NamingContainer;
                if (row != null)
                {
                    if (NOCAPExternalUtility.IsNumeric(button.CommandArgument))
                    {
                        lngINDAppCode = Convert.ToInt32(button.CommandArgument);
                    }
                }
                long lng_indAppCode = lngINDAppCode;
                NOCAPExternalUtility.INDScanLetterDownloadFiles(lng_indAppCode);
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

            }
        }
    }
    // lbtnExpansionEdit_Click
    protected void lbtnINDExpansionEdit_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandArgument != null)
                {
                    lng_IndAppCode = Convert.ToInt64(e.CommandArgument.ToString());
                    lblMode.Text = "Edit";
                    lblPageTitle.Text = Page.Title;
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);
                    NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_IndustrialExpansionApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(lng_IndAppCode);
                    if (obj_IndustrialExpansionApplication.UpToHundredKLD != NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                        Server.Transfer("~/ExternalUser/Expansion/IND/IndustrialExpansion.aspx");
                    else
                    {
                        // Server.Transfer("~/ExternalUser/IndustrialNew/a.aspx");
                        Server.Transfer("~/ExternalUser/Expansion/IND/IndustrialNewKLD.aspx");
                    }
                }
            }
            catch (ThreadAbortException)
            {


            }
            catch (Exception ex)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    protected void lbtnINFExpansionEdit_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandArgument != null)
                {

                    lblMode.Text = "Edit";
                    lblPageTitle.Text = Page.Title;
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());  //added html encode lblApplicationCode.Text = e.CommandArgument.ToString();
                                                                                                     //Server.Transfer("~/ExternalUser/InfrastructureNew/InfrastructureNew.aspx");


                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    protected void lbtnMINExpansionEdit_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandArgument != null)
                {

                    lblMode.Text = "Edit";
                    lblPageTitle.Text = Page.Title;
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);
                    //Server.Transfer("~/ExternalUser/IndustrialNew/IndustrialNew.aspx");

                    //Server.Transfer("t.aspx");
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    protected void lbtnEdit_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandArgument != null)
                {
                    lng_IndAppCode = Convert.ToInt64(e.CommandArgument.ToString());
                    lblMode.Text = "Edit";
                    lblPageTitle.Text = Page.Title;
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(lng_IndAppCode);
                    if (obj_IndustrialNewSADApplication.UpToHundredKLD != NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                        Server.Transfer("~/ExternalUser/IndustrialNew/IndustrialNew.aspx");
                    else
                    {
                        // Server.Transfer("~/ExternalUser/IndustrialNew/a.aspx");
                        Server.Transfer("~/ExternalUser/IndustrialNew/IndustrialNewKLD.aspx");
                    }
                }
            }
            catch (ThreadAbortException)
            {


            }
            catch (Exception ex)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void lbtnSubmit_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandArgument != null)
                {
                    lng_IndAppCode = Convert.ToInt32(e.CommandArgument.ToString());
                    lblMode.Text = "Edit";

                    lblPageTitle.Text = Page.Title;
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    protected void lbtnEditIndRenewSaveAsDraft_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandArgument != null)
                {
                    lng_IndAppCode = Convert.ToInt32(e.CommandArgument.ToString());
                    lblMode.Text = "Edit";
                    lblPageTitle.Text = Page.Title;
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());  // added html encode lblApplicationCode.Text = e.CommandArgument.ToString();
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }


    protected void lbtnEditMinRenewSaveAsDraft_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandArgument != null)
                {
                    lblMode.Text = "Edit";
                    lblPageTitle.Text = Page.Title;
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());   //added html encode lblApplicationCode.Text = e.CommandArgument.ToString();
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }


    protected void lbtnEditInfrastructure_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandArgument != null)
                {

                    lblMode.Text = "Edit";
                    lblPageTitle.Text = Page.Title;
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());  //added html encode lblApplicationCode.Text = e.CommandArgument.ToString();
                                                                                                     //Server.Transfer("~/ExternalUser/InfrastructureNew/InfrastructureNew.aspx");


                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    protected void lbtnMiningEdit_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandArgument != null)
                {

                    lblMode.Text = "Edit";
                    lblPageTitle.Text = Page.Title;
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);
                    //Server.Transfer("~/ExternalUser/IndustrialNew/IndustrialNew.aspx");

                    //Server.Transfer("t.aspx");
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }


    protected void lbtnViewInfrastructureStatus_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;

            if (e.CommandArgument != null)
            {
                lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());  //added html encode lblApplicationCode.Text = e.CommandArgument.ToString();
                Server.Transfer("~/ExternalUser/InfrastructureNew/Status/InfrastructureNewStatus.aspx");
            }
        }
    }

    protected void lbtnViewMiningStatus_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            if (e.CommandArgument != null)
            {
                lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());  // added html encode lblApplicationCode.Text =e.CommandArgument.ToString();
                Server.Transfer("~/ExternalUser/MiningNew/Status/MiningNewStatus.aspx");
            }
        }

    }
    protected void lbtnAppNameChangeStatus_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;

            if (e.CommandArgument != null)
            {
                lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);
                Server.Transfer("~/ExternalUser/ApplicationManagement/ApplicationNameChangeStatus.aspx");
            }
        }
    }
    protected void lbtnViewStatus_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;

            if (e.CommandArgument != null)
            {
                lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);
                Server.Transfer("~/ExternalUser/IndustrialNew/Status/IndustrialNewStatus.aspx");
            }
        }
    }

    #endregion

    #region RowCommand

    #region IND

    protected void gvIndNewSaveAsDraft_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandName == "ApplicationCode")
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());
                }

                if (e.CommandName == "OrderPaymentCode")
                {

                    string[] arr = HttpUtility.HtmlEncode(e.CommandArgument.ToString()).Split(',');
                    lblOrderPaymentCode.Text = arr[0];
                    lblApplicationCode.Text = arr[1];
                }
                if (e.CommandName == "MakePayment")
                {

                    string[] arr = HttpUtility.HtmlEncode(e.CommandArgument.ToString()).Split(',');
                    lblOrderPaymentCode.Text = arr[0];
                    lblApplicationCode.Text = arr[1];
                    lblApplicationCodeForPayment.Text = arr[1];
                    lblMode.Text = "Edit";
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void gvIndRenewSaveAsDraft_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandName == "ApplicationCode")
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());
                }
                if (e.CommandName == "OrderPaymentCode")
                {

                    string[] arr = HttpUtility.HtmlEncode(e.CommandArgument.ToString()).Split(',');
                    lblOrderPaymentCode.Text = arr[0];
                    lblApplicationCode.Text = arr[1];
                }
                if (e.CommandName == "MakePayment")
                {

                    string[] arr = HttpUtility.HtmlEncode(e.CommandArgument.ToString()).Split(',');
                    lblOrderPaymentCode.Text = arr[0];
                    lblApplicationCode.Text = arr[1];
                    lblApplicationCodeForPayment.Text = arr[1];
                    lblMode.Text = "Edit";
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void gvIndNewSubmitted_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandName == "ApplicationCode")
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());  //added html encode lblApplicationCode.Text = e.CommandArgument.ToString();
                }
                if (e.CommandName == "TelemetryDetail")
                {

                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());
                }
                if (e.CommandName == "PiezometerDetail")
                {

                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    protected void gvIndExpansionSaveAsDraft_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandName == "ApplicationCode")
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());
                }
                if (e.CommandName == "OrderPaymentCode")
                {

                    string[] arr = HttpUtility.HtmlEncode(e.CommandArgument.ToString()).Split(',');
                    lblOrderPaymentCode.Text = arr[0];
                    lblApplicationCode.Text = arr[1];
                }
                if (e.CommandName == "MakePayment")
                {

                    string[] arr = HttpUtility.HtmlEncode(e.CommandArgument.ToString()).Split(',');
                    lblOrderPaymentCode.Text = arr[0];
                    lblApplicationCode.Text = arr[1];
                    lblApplicationCodeForPayment.Text = arr[1];
                    lblMode.Text = "Edit";
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    #endregion

    #region INF
    protected void gvInfNewSaveAsDraft_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandName == "ApplicationCode")
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());
                }

                if (e.CommandName == "OrderPaymentCode")
                {
                    string[] arr = HttpUtility.HtmlEncode(e.CommandArgument.ToString()).Split(',');
                    lblOrderPaymentCode.Text = arr[0];
                    lblApplicationCode.Text = arr[1];
                }
                if (e.CommandName == "MakePayment")
                {

                    string[] arr = HttpUtility.HtmlEncode(e.CommandArgument.ToString()).Split(',');
                    lblOrderPaymentCode.Text = arr[0];
                    lblApplicationCode.Text = arr[1];
                    lblApplicationCodeForPayment.Text = arr[1];
                    lblMode.Text = "Edit";
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }


    protected void gvInfRenewSaveAsDraft_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandName == "ApplicationCode")
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());
                }
                if (e.CommandName == "OrderPaymentCode")
                {
                    string[] arr = HttpUtility.HtmlEncode(e.CommandArgument.ToString()).Split(',');
                    lblOrderPaymentCode.Text = arr[0];
                    lblApplicationCode.Text = arr[1];
                }
                if (e.CommandName == "MakePayment")
                {

                    string[] arr = HttpUtility.HtmlEncode(e.CommandArgument.ToString()).Split(',');
                    lblOrderPaymentCode.Text = arr[0];
                    lblApplicationCode.Text = arr[1];
                    lblApplicationCodeForPayment.Text = arr[1];
                    lblMode.Text = "Edit";
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void gvInfNewSubmitted_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandName == "ApplicationCode")
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());  //added html encode lblApplicationCode.Text = e.CommandArgument.ToString();
                }
                if (e.CommandName == "TelemetryDetail")
                {

                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());
                }
                if (e.CommandName == "PiezometerDetail")
                {

                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    protected void gvInFExpansionSaveAsDraft_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandName == "ApplicationCode")
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());
                }

                if (e.CommandName == "OrderPaymentCode")
                {
                    string[] arr = HttpUtility.HtmlEncode(e.CommandArgument.ToString()).Split(',');
                    lblOrderPaymentCode.Text = arr[0];
                    lblApplicationCode.Text = arr[1];
                }
                if (e.CommandName == "MakePayment")
                {

                    string[] arr = HttpUtility.HtmlEncode(e.CommandArgument.ToString()).Split(',');
                    lblOrderPaymentCode.Text = arr[0];
                    lblApplicationCode.Text = arr[1];
                    lblApplicationCodeForPayment.Text = arr[1];
                    lblMode.Text = "Edit";
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    #endregion

    #region MIN
    protected void gvMINNewSaveAsDraft_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandName == "ApplicationCode")
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());
                }

                if (e.CommandName == "OrderPaymentCode")
                {

                    string[] arr = HttpUtility.HtmlEncode(e.CommandArgument.ToString()).Split(',');
                    lblOrderPaymentCode.Text = arr[0];
                    lblApplicationCode.Text = arr[1];
                }
                if (e.CommandName == "MakePayment")
                {

                    string[] arr = HttpUtility.HtmlEncode(e.CommandArgument.ToString()).Split(',');
                    lblOrderPaymentCode.Text = arr[0];
                    lblApplicationCode.Text = arr[1];
                    lblApplicationCodeForPayment.Text = arr[1];
                    lblMode.Text = "Edit";
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void gvMinRenewSaveAsDraft_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandName == "ApplicationCode")
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());
                }

                if (e.CommandName == "OrderPaymentCode")
                {
                    string[] arr = HttpUtility.HtmlEncode(e.CommandArgument.ToString()).Split(',');
                    lblOrderPaymentCode.Text = arr[0];
                    lblApplicationCode.Text = arr[1];
                }
                if (e.CommandName == "MakePayment")
                {

                    string[] arr = HttpUtility.HtmlEncode(e.CommandArgument.ToString()).Split(',');
                    lblOrderPaymentCode.Text = arr[0];
                    lblApplicationCode.Text = arr[1];
                    lblApplicationCodeForPayment.Text = arr[1];
                    lblMode.Text = "Edit";
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void gvMinNewSubmitted_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandName == "ApplicationCode")
                {
                    lblApplicationCode.Text = e.CommandArgument.ToString();
                }
                if (e.CommandName == "TelemetryDetail")
                {

                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());
                }
                if (e.CommandName == "PiezometerDetail")
                {

                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    protected void gvMINExpansionSaveAsDraft_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandName == "ApplicationCode")
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());
                }

                if (e.CommandName == "OrderPaymentCode")
                {

                    string[] arr = HttpUtility.HtmlEncode(e.CommandArgument.ToString()).Split(',');
                    lblOrderPaymentCode.Text = arr[0];
                    lblApplicationCode.Text = arr[1];
                }
                if (e.CommandName == "MakePayment")
                {

                    string[] arr = HttpUtility.HtmlEncode(e.CommandArgument.ToString()).Split(',');
                    lblOrderPaymentCode.Text = arr[0];
                    lblApplicationCode.Text = arr[1];
                    lblApplicationCodeForPayment.Text = arr[1];
                    lblMode.Text = "Edit";
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    #endregion

    #endregion

    #region RowDataBound

    #region IND
    protected void gvIndNewSaveAsDraft_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                long NewApplicationCode = Convert.ToInt64(gvIndNewSaveAsDraft.DataKeys[e.Row.RowIndex].Values["IndustrialNewApplicationCode"]);
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialExpansionApplication =
                    new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(NewApplicationCode);
                LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton lbtnSubmit = (LinkButton)e.Row.FindControl("lbtnSubmit");
                LinkButton lnkbtnMakePayment = (LinkButton)e.Row.FindControl("lnkbtnMakePayment");

                NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_ProFeeSADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_industrialExpansionApplication.IndustrialNewApplicationCode, (obj_industrialExpansionApplication.ProFeeOrderPaymentCode == null ? "0" : obj_industrialExpansionApplication.ProFeeOrderPaymentCode.ToString()));
                NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_GWChargeSADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_industrialExpansionApplication.IndustrialNewApplicationCode, (obj_industrialExpansionApplication.GWChargeOrderPaymentCode == null ? "0" : obj_industrialExpansionApplication.GWChargeOrderPaymentCode.ToString()));
                NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_PenaltySADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_industrialExpansionApplication.IndustrialNewApplicationCode, (obj_industrialExpansionApplication.PenaltyOrderPaymentCode == null ? "0" : obj_industrialExpansionApplication.PenaltyOrderPaymentCode.ToString()));
                lbtnEdit.Enabled = true;
                lnkbtnMakePayment.Enabled = true;
                NOCAP.BLL.Master.NTRPIntegration obj_nTRPIntegration = new NOCAP.BLL.Master.NTRPIntegration((int)NOCAP.BLL.Common.CommonEnum.ApplicationType.Industrial);


                EnableDisableLinkButton(obj_nTRPIntegration.Active, NewApplicationCode, lbtnEdit, lnkbtnMakePayment,
                    lbtnSubmit, obj_industrialExpansionApplication.ReadyToSubmit,
                    obj_industrialExpansionApplication.PaymentTypeMode, obj_industrialExpansionApplication.WaterQualityCode
                    , objA_ProFeeSADOnlinePayment, objA_GWChargeSADOnlinePayment, objA_PenaltySADOnlinePayment
                    , NOCAPExternalUtility.AreaTypeCategoryCode(obj_industrialExpansionApplication)
                    );
                //if (obj_industrialNewSADApplication.ReadyToSubmit == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                //{
                //    #region PaymentTypeMode Combined
                //    if (obj_industrialNewSADApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined)
                //    {
                //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            lbtnEdit.Enabled = false;
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus != NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Failed)
                //                lnkbtnMakePayment.Enabled = false;
                //            else
                //                lnkbtnMakePayment.Enabled = true;
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                lbtnSubmit.Enabled = true;
                //            else
                //                lbtnSubmit.Enabled = false;
                //        }


                //    }
                //    #endregion

                //    #region PaymentTypeMode Single
                //    else if (obj_industrialNewSADApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single)
                //    {

                //        #region Edit Button Enable/Disable
                //        if ((objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0) || (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0) || (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0))
                //            lbtnEdit.Enabled = false;
                //        #endregion

                //        #region MakePayment Button Enable/Disable
                //        string str_ProFee = "N", str_gwCharge = "N", str_Penalty = "N";
                //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_ProFee = "Y";
                //            else
                //                str_ProFee = "N";
                //        }
                //        if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_GWChargeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_gwCharge = "Y";
                //            else
                //                str_gwCharge = "N";
                //        }
                //        if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_PenaltySADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_Penalty = "Y";
                //            else
                //                str_Penalty = "N";
                //        }

                //        if (obj_industrialNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry && obj_industrialNewSADApplication.WaterQualityCode == 1)
                //        {

                //            if (str_ProFee == "Y" && str_gwCharge == "Y" && str_Penalty == "Y")
                //                lnkbtnMakePayment.Enabled = false;
                //            else
                //                lnkbtnMakePayment.Enabled = true;
                //        }
                //        else
                //        {
                //            if (obj_industrialNewSADApplication.WaterQualityCode == 1)
                //            {
                //                if (str_ProFee == "Y" && str_gwCharge == "Y")
                //                    lnkbtnMakePayment.Enabled = false;
                //                else
                //                    lnkbtnMakePayment.Enabled = true;
                //            }
                //            else if (obj_industrialNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry)
                //            {
                //                if (str_ProFee == "Y" && str_Penalty == "Y")
                //                    lnkbtnMakePayment.Enabled = false;
                //                else
                //                    lnkbtnMakePayment.Enabled = true;
                //            }
                //            else
                //            {
                //                if (str_ProFee == "Y")
                //                    lnkbtnMakePayment.Enabled = false;
                //                else
                //                    lnkbtnMakePayment.Enabled = true;
                //            }

                //        }
                //        #endregion

                //        #region Submit Button Enable/Disable
                //        string str_subProFee = "N", str_subgwCharge = "N", str_subPenalty = "N";
                //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_subProFee = "Y";
                //            else
                //                str_subProFee = "N";
                //        }
                //        if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_GWChargeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_subgwCharge = "Y";
                //            else
                //                str_subgwCharge = "N";
                //        }
                //        if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_PenaltySADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_subPenalty = "Y";
                //            else
                //                str_subPenalty = "N";
                //        }

                //        if (obj_industrialNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry && obj_industrialNewSADApplication.WaterQualityCode == 1)
                //        {
                //            if (str_subProFee == "Y" && str_subgwCharge == "Y" && str_subPenalty == "Y")
                //                lbtnSubmit.Enabled = true;
                //            else
                //                lbtnSubmit.Enabled = false;
                //        }
                //        else
                //        {

                //            if (obj_industrialNewSADApplication.WaterQualityCode == 1)
                //            {
                //                if (str_subProFee == "Y" && str_subgwCharge == "Y")
                //                    lbtnSubmit.Enabled = true;
                //                else
                //                    lbtnSubmit.Enabled = false;
                //            }
                //            else if (obj_industrialNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry)
                //            {
                //                if (str_subProFee == "Y" && str_subPenalty == "Y")
                //                    lbtnSubmit.Enabled = true;
                //                else
                //                    lbtnSubmit.Enabled = false;
                //            }
                //            else
                //            {
                //                if (str_subProFee == "Y")
                //                    lbtnSubmit.Enabled = true;
                //                else
                //                    lbtnSubmit.Enabled = false;
                //            }
                //        }

                //        #endregion

                //    }
                //    else
                //    {
                //        lbtnSubmit.Enabled = false;
                //    }

                //    #endregion
                //}
                //else
                //{
                //    lnkbtnMakePayment.Enabled = false;
                //    lbtnSubmit.Enabled = false;
                //}

            }

        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    protected void gvIndRenewSaveAsDraft_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();

                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplicationPrev = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
                NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplicationPrev = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication();
              
                long RenewApplicationCode = Convert.ToInt64(gvIndRenewSaveAsDraft.DataKeys[e.Row.RowIndex].Values["IndustrialRenewApplicationCode"]);
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewSADApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(RenewApplicationCode);
                obj_industrialNewApplication = obj_industrialRenewSADApplication.GetFirstIndustrialApplication();
                if (obj_industrialNewApplication != null)
                {
                    Label lblApplicationNumber = (Label)e.Row.FindControl("lblApplicationNumber");
                    Label lblRenewalCount = (Label)e.Row.FindControl("lblRenewalCount");
                    Label lblNOCNumber = (Label)e.Row.FindControl("lblNOCNumber");
                    Label lblIssueLetterStartDate = (Label)e.Row.FindControl("lblIssueLetterStartDate");
                    Label lblIssueLetterEndDate = (Label)e.Row.FindControl("lblIssueLetterEndDate");
                    Label lblOldApplicationNumber = (Label)e.Row.FindControl("lblOldApplicationNumber");
                    Label lblOldNOCNumber = (Label)e.Row.FindControl("lblOldNOCNumber");

                    //  LinkButton lnkPaymentStatus = (LinkButton)e.Row.FindControl("indrenlnkPaymentStatus");

                    LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");

                    LinkButton lbtnSubmit = (LinkButton)e.Row.FindControl("lbtnSubmit");
                    LinkButton lnkbtnMakePayment = (LinkButton)e.Row.FindControl("lnkbtnMakePayment");


                    NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_ProFeeSADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_industrialRenewSADApplication.IndustrialRenewApplicationCode, (obj_industrialRenewSADApplication.ProFeeOrderPaymentCode == null ? "0" : obj_industrialRenewSADApplication.ProFeeOrderPaymentCode.ToString()));
                    NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_GWChargeSADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_industrialRenewSADApplication.IndustrialRenewApplicationCode, (obj_industrialRenewSADApplication.GWChargeOrderPaymentCode == null ? "0" : obj_industrialRenewSADApplication.GWChargeOrderPaymentCode.ToString()));
                    NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_PenaltySADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_industrialRenewSADApplication.IndustrialRenewApplicationCode, (obj_industrialRenewSADApplication.PenaltyOrderPaymentCode == null ? "0" : obj_industrialRenewSADApplication.PenaltyOrderPaymentCode.ToString()));
                    lbtnEdit.Enabled = true;
                    lnkbtnMakePayment.Enabled = true;
                    NOCAP.BLL.Master.NTRPIntegration obj_nTRPIntegration = new NOCAP.BLL.Master.NTRPIntegration((int)NOCAP.BLL.Common.CommonEnum.ApplicationType.Industrial);

                    EnableDisableLinkButton(obj_nTRPIntegration.Active, RenewApplicationCode, lbtnEdit, lnkbtnMakePayment,
                    lbtnSubmit, obj_industrialRenewSADApplication.ReadyToSubmit,
                    obj_industrialRenewSADApplication.PaymentTypeMode, (obj_industrialRenewSADApplication.WaterQualityCode == null ? 0 : (int)obj_industrialRenewSADApplication.WaterQualityCode)
                    , objA_ProFeeSADOnlinePayment, objA_GWChargeSADOnlinePayment, objA_PenaltySADOnlinePayment
                     , NOCAPExternalUtility.AreaTypeCategoryCodeForAppCode(new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_industrialRenewSADApplication.FirstApplicationCode))
                    );
                    //if (obj_industrialRenewSADApplication.ReadyToSubmit == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                    //{
                    //    #region PaymentTypeMode Combined
                    //    if (obj_industrialRenewSADApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined)
                    //    {
                    //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            lbtnEdit.Enabled = false;
                    //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus != NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Failed && (obj_industrialRenewSADApplication.ReadyToSubmit == NOCAP.BLL.Common.CommonEnum.YesNoOption.No || obj_industrialRenewSADApplication.ReadyToSubmit == NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined))
                    //                lnkbtnMakePayment.Enabled = false;
                    //            else
                    //                lnkbtnMakePayment.Enabled = true;
                    //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                lbtnSubmit.Enabled = true;
                    //            else
                    //                lbtnSubmit.Enabled = false;
                    //        }

                    //    }
                    //    #endregion

                    //    #region PaymentTypeMode Single
                    //    else if (obj_industrialRenewSADApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single)
                    //    {

                    //        #region Edit Button Enable/Disable
                    //        if ((objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0) || (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0) || (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0))
                    //            lbtnEdit.Enabled = false;
                    //        #endregion

                    //        #region MakePayment Button Enable/Disable
                    //        string str_ProFee = "N", str_gwCharge = "N", str_Penalty = "N";
                    //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                str_ProFee = "Y";
                    //            else
                    //                str_ProFee = "N";
                    //        }
                    //        if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            if (objA_GWChargeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                str_gwCharge = "Y";
                    //            else
                    //                str_gwCharge = "N";
                    //        }
                    //        if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            if (objA_PenaltySADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                str_Penalty = "Y";
                    //            else
                    //                str_Penalty = "N";
                    //        }



                    //        if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_industrialRenewSADApplication.IndustrialRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes && obj_industrialRenewSADApplication.WaterQualityCode == 1)
                    //        {
                    //            if (str_ProFee == "Y" && str_gwCharge == "Y" && str_Penalty == "Y")
                    //                lnkbtnMakePayment.Enabled = false;
                    //            else
                    //                lnkbtnMakePayment.Enabled = true;
                    //        }
                    //        else if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_industrialRenewSADApplication.IndustrialRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.No)
                    //        {
                    //            if (obj_industrialRenewSADApplication.WaterQualityCode == 1)
                    //            {
                    //                if (str_ProFee == "Y" && str_gwCharge == "Y")
                    //                    lnkbtnMakePayment.Enabled = false;
                    //            }
                    //            else
                    //            {
                    //                if (str_ProFee == "Y")
                    //                    lnkbtnMakePayment.Enabled = false;
                    //                else
                    //                    lnkbtnMakePayment.Enabled = true;
                    //            }
                    //        }
                    //        else { lnkbtnMakePayment.Enabled = true; }
                    //        #endregion

                    //        #region Submit Button Enable/Disable
                    //        string str_subProFee = "N", str_subgwCharge = "N", str_subPenalty = "N";
                    //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus ==
                    //               NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                str_subProFee = "Y";
                    //            else
                    //                str_subProFee = "N";
                    //        }
                    //        if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            if (objA_GWChargeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                str_subgwCharge = "Y";
                    //            else
                    //                str_subgwCharge = "N";
                    //        }
                    //        if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            if (objA_PenaltySADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                str_subPenalty = "Y";
                    //            else
                    //                str_subPenalty = "N";
                    //        }

                    //        if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_industrialRenewSADApplication.IndustrialRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes && obj_industrialRenewSADApplication.WaterQualityCode == 1)
                    //        {
                    //            if (str_subProFee == "Y" && str_subgwCharge == "Y" && str_subPenalty == "Y")
                    //                lbtnSubmit.Enabled = true;
                    //            else
                    //                lbtnSubmit.Enabled = false;
                    //        }
                    //        else if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_industrialRenewSADApplication.IndustrialRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.No)
                    //        {
                    //            if (obj_industrialRenewSADApplication.WaterQualityCode == 1)
                    //            {
                    //                if (str_subProFee == "Y" && str_subgwCharge == "Y")
                    //                    lbtnSubmit.Enabled = true;

                    //            }
                    //            else
                    //            {
                    //                if (str_subProFee == "Y")
                    //                    lbtnSubmit.Enabled = true;
                    //                else
                    //                    lbtnSubmit.Enabled = false;
                    //            }

                    //        }
                    //        else { lbtnSubmit.Enabled = false; }
                    //        #endregion

                    //    }
                    //    #endregion
                    //    else { lbtnSubmit.Enabled = false; }
                    //}
                    //else { lbtnSubmit.Enabled = false; lnkbtnMakePayment.Enabled = false; }



                    if (obj_industrialRenewSADApplication.GetPreviousIndustrialApplication(out obj_industrialNewApplicationPrev, out obj_industrialRenewApplicationPrev) == 1)
                    {
                        if (obj_industrialNewApplicationPrev != null)
                        {
                            lblNOCNumber.Text = HttpUtility.HtmlEncode(obj_industrialNewApplicationPrev.NOCNumber);

                            NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_industrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter(obj_industrialNewApplicationPrev.IndustrialNewApplicationCode);
                            if (obj_industrialNewIssusedLetter.ValidityStartDate != null)
                            {
                                lblIssueLetterStartDate.Text = "(" + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_industrialNewIssusedLetter.ValidityStartDate)).ToString("dd/MM/yyyy");
                            }
                            if (obj_industrialNewIssusedLetter.ValidityEndDate != null)
                            {
                                lblIssueLetterEndDate.Text = " - " + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_industrialNewIssusedLetter.ValidityEndDate)).ToString("dd/MM/yyyy") + ")";
                            }
                        }
                        else if (obj_industrialRenewApplicationPrev != null)
                        {
                            lblNOCNumber.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplicationPrev.NOCNumber);

                            NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter obj_industrialRenewIssusedLetter = new NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter(obj_industrialRenewApplicationPrev.IndustrialRenewApplicationCode);
                            if (obj_industrialRenewIssusedLetter.ValidityStartDate != null)
                            {
                                lblIssueLetterStartDate.Text = "(" + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_industrialRenewIssusedLetter.ValidityStartDate)).ToString("dd/MM/yyyy");
                            }
                            if (obj_industrialRenewIssusedLetter.ValidityEndDate != null)
                            {
                                lblIssueLetterEndDate.Text = " - " + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_industrialRenewIssusedLetter.ValidityEndDate)).ToString("dd/MM/yyyy") + ")";
                            }


                        }
                    }

                    if (obj_industrialNewApplication.IndustrialOldApplicationNumber.Trim() != "")
                    {
                        lblOldApplicationNumber.Text = "(Old: " + HttpUtility.HtmlEncode(obj_industrialNewApplication.IndustrialOldApplicationNumber) + ")";
                    }
                    if (obj_industrialNewApplication.NOCNumberOld.Trim() != "")
                    {
                        lblOldNOCNumber.Text = "<br/>(Old: " + HttpUtility.HtmlEncode(obj_industrialNewApplication.NOCNumberOld) + ")";
                    }
                    lblApplicationNumber.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.IndustrialNewApplicationNumber);
                    lblRenewalCount.Text = HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(obj_industrialRenewSADApplication.LinkDepth));  //added html encode lblRenewalCount.Text = NOCAPExternalUtility.AddOrdinal(obj_industrialRenewSADApplication.LinkDepth);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    protected void gvIndExpansionSaveAsDraft_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                long NewApplicationCode = Convert.ToInt64(gvIndNewSaveAsDraft.DataKeys[e.Row.RowIndex].Values["IndustrialNewApplicationCode"]);
                NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewSADApplication =
                    new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(NewApplicationCode);

                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialExpansionApplication =
                   new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(NewApplicationCode);

                LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton lbtnSubmit = (LinkButton)e.Row.FindControl("lbtnSubmit");
                LinkButton lnkbtnMakePayment = (LinkButton)e.Row.FindControl("lnkbtnMakePayment");

                NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_ProFeeSADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_industrialNewSADApplication.IndustrialNewApplicationCode, (obj_industrialNewSADApplication.ProFeeOrderPaymentCode == null ? "0" : obj_industrialNewSADApplication.ProFeeOrderPaymentCode.ToString()));
                NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_GWChargeSADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_industrialNewSADApplication.IndustrialNewApplicationCode, (obj_industrialNewSADApplication.GWChargeOrderPaymentCode == null ? "0" : obj_industrialNewSADApplication.GWChargeOrderPaymentCode.ToString()));
                NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_PenaltySADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_industrialNewSADApplication.IndustrialNewApplicationCode, (obj_industrialNewSADApplication.PenaltyOrderPaymentCode == null ? "0" : obj_industrialNewSADApplication.PenaltyOrderPaymentCode.ToString()));
                lbtnEdit.Enabled = true;
               // lnkbtnMakePayment.Enabled = true;
                NOCAP.BLL.Master.NTRPIntegration obj_nTRPIntegration = new NOCAP.BLL.Master.NTRPIntegration((int)NOCAP.BLL.Common.CommonEnum.ApplicationType.Industrial);


                EnableDisableLinkButton(obj_nTRPIntegration.Active, NewApplicationCode, lbtnEdit, lnkbtnMakePayment,
                    lbtnSubmit, obj_industrialNewSADApplication.ReadyToSubmit,
                    obj_industrialNewSADApplication.PaymentTypeMode, obj_industrialNewSADApplication.WaterQualityCode
                    , objA_ProFeeSADOnlinePayment, objA_GWChargeSADOnlinePayment, objA_PenaltySADOnlinePayment
                    , NOCAPExternalUtility.AreaTypeCategoryCode(obj_industrialExpansionApplication) 
                    );
                //if (obj_industrialNewSADApplication.ReadyToSubmit == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                //{
                //    #region PaymentTypeMode Combined
                //    if (obj_industrialNewSADApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined)
                //    {
                //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            lbtnEdit.Enabled = false;
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus != NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Failed)
                //                lnkbtnMakePayment.Enabled = false;
                //            else
                //                lnkbtnMakePayment.Enabled = true;
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                lbtnSubmit.Enabled = true;
                //            else
                //                lbtnSubmit.Enabled = false;
                //        }


                //    }
                //    #endregion

                //    #region PaymentTypeMode Single
                //    else if (obj_industrialNewSADApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single)
                //    {

                //        #region Edit Button Enable/Disable
                //        if ((objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0) || (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0) || (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0))
                //            lbtnEdit.Enabled = false;
                //        #endregion

                //        #region MakePayment Button Enable/Disable
                //        string str_ProFee = "N", str_gwCharge = "N", str_Penalty = "N";
                //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_ProFee = "Y";
                //            else
                //                str_ProFee = "N";
                //        }
                //        if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_GWChargeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_gwCharge = "Y";
                //            else
                //                str_gwCharge = "N";
                //        }
                //        if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_PenaltySADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_Penalty = "Y";
                //            else
                //                str_Penalty = "N";
                //        }

                //        if (obj_industrialNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry && obj_industrialNewSADApplication.WaterQualityCode == 1)
                //        {

                //            if (str_ProFee == "Y" && str_gwCharge == "Y" && str_Penalty == "Y")
                //                lnkbtnMakePayment.Enabled = false;
                //            else
                //                lnkbtnMakePayment.Enabled = true;
                //        }
                //        else
                //        {
                //            if (obj_industrialNewSADApplication.WaterQualityCode == 1)
                //            {
                //                if (str_ProFee == "Y" && str_gwCharge == "Y")
                //                    lnkbtnMakePayment.Enabled = false;
                //                else
                //                    lnkbtnMakePayment.Enabled = true;
                //            }
                //            else if (obj_industrialNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry)
                //            {
                //                if (str_ProFee == "Y" && str_Penalty == "Y")
                //                    lnkbtnMakePayment.Enabled = false;
                //                else
                //                    lnkbtnMakePayment.Enabled = true;
                //            }
                //            else
                //            {
                //                if (str_ProFee == "Y")
                //                    lnkbtnMakePayment.Enabled = false;
                //                else
                //                    lnkbtnMakePayment.Enabled = true;
                //            }

                //        }
                //        #endregion

                //        #region Submit Button Enable/Disable
                //        string str_subProFee = "N", str_subgwCharge = "N", str_subPenalty = "N";
                //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_subProFee = "Y";
                //            else
                //                str_subProFee = "N";
                //        }
                //        if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_GWChargeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_subgwCharge = "Y";
                //            else
                //                str_subgwCharge = "N";
                //        }
                //        if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_PenaltySADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_subPenalty = "Y";
                //            else
                //                str_subPenalty = "N";
                //        }

                //        if (obj_industrialNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry && obj_industrialNewSADApplication.WaterQualityCode == 1)
                //        {
                //            if (str_subProFee == "Y" && str_subgwCharge == "Y" && str_subPenalty == "Y")
                //                lbtnSubmit.Enabled = true;
                //            else
                //                lbtnSubmit.Enabled = false;
                //        }
                //        else
                //        {

                //            if (obj_industrialNewSADApplication.WaterQualityCode == 1)
                //            {
                //                if (str_subProFee == "Y" && str_subgwCharge == "Y")
                //                    lbtnSubmit.Enabled = true;
                //                else
                //                    lbtnSubmit.Enabled = false;
                //            }
                //            else if (obj_industrialNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry)
                //            {
                //                if (str_subProFee == "Y" && str_subPenalty == "Y")
                //                    lbtnSubmit.Enabled = true;
                //                else
                //                    lbtnSubmit.Enabled = false;
                //            }
                //            else
                //            {
                //                if (str_subProFee == "Y")
                //                    lbtnSubmit.Enabled = true;
                //                else
                //                    lbtnSubmit.Enabled = false;
                //            }
                //        }

                //        #endregion

                //    }
                //    else
                //    {
                //        lbtnSubmit.Enabled = false;
                //    }

                //    #endregion
                //}
                //else
                //{
                //    lnkbtnMakePayment.Enabled = false;
                //    lbtnSubmit.Enabled = false;
                //}

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    protected void gvIndNewSubmitted_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
            Label lblLatestAppStatusCode = new Label();
            LinkButton lnkbtn = new LinkButton();
            LinkButton lnkbtnScan = new LinkButton();
            //Label lblApplyType = new Label();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                object objTemp = gvIndNewSubmitted.DataKeys[e.Row.RowIndex].Value as object;
                string id = objTemp.ToString();
                NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter objIndustrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter();
                objIndustrialNewIssusedLetter.populateIndustrialNewIssusedLetterForINDAppCode(Convert.ToInt32(id));
                lnkbtn = (LinkButton)e.Row.FindControl("lbtnDownload");
                lnkbtnScan = (LinkButton)e.Row.FindControl("lbtnScanDownload");
                LinkButton lnkNOCNumber = (LinkButton)e.Row.FindControl("lnkNOCNumber");
                Label lblApplyType = (Label)e.Row.FindControl("lblApplyType");
                LinkButton lnkRenewDetail = (LinkButton)e.Row.FindControl("lnkRenewDetail");
                LinkButton lnkCompliance = (LinkButton)e.Row.FindControl("lnkCompliance");
                LinkButton lnkInspection = (LinkButton)e.Row.FindControl("lnkInspection");
                LinkButton lnkPayAbstractionCharges = (LinkButton)e.Row.FindControl("lnkPayAbstractionCharges");
                LinkButton lbtnCommunicationRequired = (LinkButton)e.Row.FindControl("lbtnCommunicationRequired");
                ViewState["path"] = HttpUtility.HtmlEncode(objIndustrialNewIssusedLetter.AttPath);
                //Made by Ashu
                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(Convert.ToInt64(lnkNOCNumber.Text));
                NOCAP.BLL.Misc.Communication.CommunicationRequest obj_CommunicationRequestForArr = new NOCAP.BLL.Misc.Communication.CommunicationRequest();
                obj_CommunicationRequestForArr.AppCode = obj_IndustrialNewApplication.IndustrialNewApplicationCode;
                obj_CommunicationRequestForArr.IsClose = NOCAP.BLL.Common.CommonEnum.FlagYesNo.No;
                obj_CommunicationRequestForArr.GetAll();
                NOCAP.BLL.Misc.Communication.CommunicationRequest[] arr = obj_CommunicationRequestForArr.CommunicationRequestCollection;
                if (arr.Count() > 0)
                {
                    lbtnCommunicationRequired.Visible = true;
                }
                else
                {
                    lbtnCommunicationRequired.Visible = false;
                }
                lnkNOCNumber.Text = HttpUtility.HtmlEncode(obj_IndustrialNewApplication.NOCNumber);


                //start Self Compliance Gaurav Jain 06/04/2017
                //imran
                NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_industrialNewIssusedLetter = obj_IndustrialNewApplication.GetIssuedLetter();
                // if (obj_industrialNewIssusedLetter != null && obj_industrialNewIssusedLetter.ValidityStartDate != null && obj_IndustrialNewApplication.EligibleForExemptionLetter == NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication.EligibleForExemptionLetterOption.No)
                if (obj_industrialNewIssusedLetter != null && obj_industrialNewIssusedLetter.ValidityStartDate != null && lnkNOCNumber.Text.Trim() != "")
                {
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(gvIndNewSubmitted.DataKeys[e.Row.RowIndex].Value));
                    if (obj_selfCompliance.ApplicationCode != 0 && obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                    {
                        lnkCompliance.Text = "Self Compliance (Filled)";
                        lnkCompliance.Visible = true;
                    }
                    else
                    {
                        if (Convert.ToDateTime(obj_industrialNewIssusedLetter.ValidityStartDate).AddMonths(11) < DateTime.Now && Convert.ToDateTime(obj_industrialNewIssusedLetter.ValidityEndDate) > DateTime.Now && obj_IndustrialNewApplication.LatestApplicationStatusCode == Convert.ToInt32(NOCAP.BLL.Utility.Utility.LatestApplicationStatusCodeOption.Approved))
                            lnkCompliance.Visible = true;
                        else
                            lnkCompliance.Visible = true;

                        lnkCompliance.Text = "Self Compliance (Not Filled)";
                    }

                    NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_SelfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(Convert.ToInt64(gvIndNewSubmitted.DataKeys[e.Row.RowIndex].Value));
                    if (obj_SelfInspection.ApplicationCode != 0 && obj_SelfInspection.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                    {
                        lnkInspection.Text = "Self Inspection (Filled)";
                        lnkInspection.Visible = true;
                    }
                    else
                    {
                        if (Convert.ToDateTime(obj_industrialNewIssusedLetter.ValidityEndDate).AddMonths(-6) <= DateTime.Now && obj_IndustrialNewApplication.LatestApplicationStatusCode == Convert.ToInt32(NOCAP.BLL.Utility.Utility.LatestApplicationStatusCodeOption.Approved))
                            lnkInspection.Visible = true;
                        else
                            lnkInspection.Visible = true;

                        lnkInspection.Text = "Self Inspection (Not Filled)";
                    }

                }
                else
                {
                    lnkCompliance.Visible = true;
                    lnkInspection.Visible = false;
                }

                if (obj_IndustrialNewApplication.WaterChargeTypeCodeFinally == 2 && obj_IndustrialNewApplication.WaterChargeReqFinally == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.WaterChargeReqFinallyYesNo.Yes && obj_IndustrialNewApplication.WaterChargeRecFinally == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.WaterChargeRecFinallyYesNo.No)
                    lnkPayAbstractionCharges.Visible = true;
                else if (obj_IndustrialNewApplication.WaterChargeTypeCodeFinally == 2 && obj_IndustrialNewApplication.WaterChargeReqFinally == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.WaterChargeReqFinallyYesNo.Yes && obj_IndustrialNewApplication.WaterChargeRecFinally == NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.WaterChargeRecFinallyYesNo.Yes)
                {
                    lnkPayAbstractionCharges.Visible = true;
                    lnkPayAbstractionCharges.Text = "Ground Water Charge (Submitted)";

                }
                else
                    lnkPayAbstractionCharges.Visible = false;
                //End Self Compliance 

                if (obj_IndustrialNewApplication.RewAppCodeFinally != null)
                {
                    lnkRenewDetail.Text = "Detail";
                }
                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplicationEligibleForExemption = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(Convert.ToInt64(gvIndNewSubmitted.DataKeys[e.Row.RowIndex].Value));
                string EligibleForExemptionLett = Convert.ToString(obj_industrialNewApplicationEligibleForExemption.EligibleForExemptionLetter);
                LinkButton lnkBtn = new LinkButton();
                lnkBtn = (LinkButton)e.Row.FindControl("lbtnEdit");
                if (EligibleForExemptionLett == "Yes")
                {
                    lnkBtn.Enabled = false;
                    lnkBtn.Text = "";
                    lnkBtn.ForeColor = System.Drawing.Color.Black;
                    lnkBtn.Style.Add("text-decoration", "none");
                    //lnkBtn.CssClass = "NoUnderLine";
                }
                else if (EligibleForExemptionLett == "No")
                {
                    lnkBtn.Visible = true;
                }
                if (objTemp != null)
                {
                    if (objIndustrialNewIssusedLetter.AttPath != null && objIndustrialNewIssusedLetter.ScanAttPath != null && objIndustrialNewIssusedLetter.ScanAttPath != "" && objIndustrialNewIssusedLetter.AttPath != "")
                    {
                        lnkbtn.Visible = true;
                        lnkbtnScan.Visible = true;
                        int intIssueLetterTypeCode = objIndustrialNewIssusedLetter.IssuedLetterTypeCode;
                        NOCAP.BLL.Master.IssuedLettersType objIssuedLettersType = new NOCAP.BLL.Master.IssuedLettersType();
                        objIssuedLettersType.populateIssuedLetterTypeForIssuedLetterTypeCode(intIssueLetterTypeCode);
                        if (objIssuedLettersType != null)
                        {
                            lnkbtn.Text = HttpUtility.HtmlEncode(objIssuedLettersType.IssuedLetterTypeName);  // added html encode lnkbtn.Text = objIssuedLettersType.IssuedLetterTypeName;
                        }

                    }
                    else if (objIndustrialNewIssusedLetter.AttPath != "" && objIndustrialNewIssusedLetter.AttPath != null)
                    {
                        lnkbtnScan.Visible = false;
                        lnkbtn.Visible = true;
                        int intIssueLetterTypeCode = objIndustrialNewIssusedLetter.IssuedLetterTypeCode;
                        NOCAP.BLL.Master.IssuedLettersType objIssuedLettersType = new NOCAP.BLL.Master.IssuedLettersType();
                        objIssuedLettersType.populateIssuedLetterTypeForIssuedLetterTypeCode(intIssueLetterTypeCode);
                        if (objIssuedLettersType != null)
                        {
                            lnkbtn.Text = HttpUtility.HtmlEncode(objIssuedLettersType.IssuedLetterTypeName);  //added html encode lnkbtn.Text = objIssuedLettersType.IssuedLetterTypeName;
                        }
                    }
                    else if (objIndustrialNewIssusedLetter.ScanAttPath != "" && objIndustrialNewIssusedLetter.ScanAttPath != null)
                    {
                        lnkbtn.Visible = false;
                        lnkbtnScan.Visible = true;

                    }
                    else
                    {
                        lnkbtn.Visible = false;
                        lnkbtnScan.Visible = false;
                    }
                }
                lblLatestAppStatusCode = (Label)e.Row.FindControl("lblLatestAppStatusCode");
                obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(Convert.ToInt32(gvIndNewSubmitted.DataKeys[e.Row.RowIndex].Value.ToString()));
                NOCAP.BLL.Master.ApplicationStatus obj_ApplicationStatus = new NOCAP.BLL.Master.ApplicationStatus(obj_industrialNewApplication.LatestApplicationStatusCode);


                if (!string.IsNullOrEmpty(obj_ApplicationStatus.ApplicationStatusDescription))
                {
                    // lblLatestAppStatusCode.Text = "( " + obj_industrialNewApplication.GetApplicationStatus().ApplicationStatusDescription + " )";
                    lblLatestAppStatusCode.Text = HttpUtility.HtmlEncode("( " + obj_ApplicationStatus.ApplicationStatusDescription + " )");  // added html encode lblLatestAppStatusCode.Text = "( " + obj_ApplicationStatus.ApplicationStatusDescription + " )";

                    //Start For Presentation 
                    Label lblLatestPresentReq = (Label)e.Row.FindControl("lblLatestPresentReq");
                    if (obj_industrialNewApplication.PresentationCalledSerialNumber != null)
                    {
                        NOCAP.BLL.Misc.Presentation.PresentationCalled obj_PresentationCalled = new NOCAP.BLL.Misc.Presentation.PresentationCalled(obj_industrialNewApplication.IndustrialNewApplicationCode, obj_industrialNewApplication.PresentationCalledSerialNumber);
                        if (obj_PresentationCalled.ApplicationCode != 0)
                        {
                            if (obj_PresentationCalled.PresentCalled == NOCAP.BLL.Common.CommonEnum.PresentationCalledOption.Yes)
                            {
                                if (obj_PresentationCalled.PresentationFinalize == NOCAP.BLL.Common.CommonEnum.PresentationFinalizeOption.No)
                                {
                                    lblLatestPresentReq.Text = "<br />Presentation<br />Required";
                                }
                            }
                        }
                    }
                    //End For Presentation 

                }

                if (obj_industrialNewApplication.SubmittedType == NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication.SubmittedTypeOption.Online)
                {
                    lblApplyType.Text = "Online";
                }
                else
                {

                    lblApplyType.Text = "Archival";
                    lnkBtn.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    #endregion

    #region INF
    protected void gvInfNewSaveAsDraft_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                long NewApplicationCode = Convert.ToInt64(gvInfNewSaveAsDraft.DataKeys[e.Row.RowIndex].Values["ApplicationCode"]);
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewSADApplication =
                    new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(NewApplicationCode);

                LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton lnkbtnMakePayment = (LinkButton)e.Row.FindControl("lnkbtnMakePayment");
                LinkButton lbtnSubmit = (LinkButton)e.Row.FindControl("lbtnSubmit");

                NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_ProFeeSADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_infrastructureNewSADApplication.InfrastructureNewApplicationCode, (obj_infrastructureNewSADApplication.ProFeeOrderPaymentCode == null ? "0" : obj_infrastructureNewSADApplication.ProFeeOrderPaymentCode.ToString()));
                NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_GWChargeSADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_infrastructureNewSADApplication.InfrastructureNewApplicationCode, (obj_infrastructureNewSADApplication.GWChargeOrderPaymentCode == null ? "0" : obj_infrastructureNewSADApplication.GWChargeOrderPaymentCode.ToString()));
                NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_PenaltySADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_infrastructureNewSADApplication.InfrastructureNewApplicationCode, (obj_infrastructureNewSADApplication.PenaltyOrderPaymentCode == null ? "0" : obj_infrastructureNewSADApplication.PenaltyOrderPaymentCode.ToString()));
                lnkbtnMakePayment.Enabled = true;
                lbtnEdit.Enabled = true;
                NOCAP.BLL.Master.NTRPIntegration obj_nTRPIntegration = new NOCAP.BLL.Master.NTRPIntegration((int)NOCAP.BLL.Common.CommonEnum.ApplicationType.Infrastructure);

                EnableDisableLinkButton(obj_nTRPIntegration.Active, NewApplicationCode, lbtnEdit, lnkbtnMakePayment,
                    lbtnSubmit, obj_infrastructureNewSADApplication.ReadyToSubmit,
                    obj_infrastructureNewSADApplication.PaymentTypeMode, obj_infrastructureNewSADApplication.WaterQualityCode
                    , objA_ProFeeSADOnlinePayment, objA_GWChargeSADOnlinePayment, objA_PenaltySADOnlinePayment
                    , NOCAPExternalUtility.AreaTypeCategoryCode(null, obj_infrastructureNewSADApplication)
                    );
                //if (obj_infrastructureNewSADApplication.ReadyToSubmit == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                //{
                //    #region PaymentTypeMode Combined
                //    if (obj_infrastructureNewSADApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined)
                //    {
                //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            lbtnEdit.Enabled = false;
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus != NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Failed && (obj_infrastructureNewSADApplication.ReadyToSubmit == NOCAP.BLL.Common.CommonEnum.YesNoOption.No || obj_infrastructureNewSADApplication.ReadyToSubmit == NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined))
                //                lnkbtnMakePayment.Enabled = false;
                //            else
                //                lnkbtnMakePayment.Enabled = true;
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                lbtnSubmit.Enabled = true;
                //            else
                //                lbtnSubmit.Enabled = false;
                //        }

                //    }
                //    #endregion

                //    #region PaymentTypeMode Single
                //    else if (obj_infrastructureNewSADApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single)
                //    {

                //        #region Edit Button Enable/Disable
                //        if ((objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0) || (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0) || (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0))
                //            lbtnEdit.Enabled = false;
                //        #endregion

                //        #region MakePayment Button Enable/Disable
                //        string str_ProFee = "N", str_gwCharge = "N", str_Penalty = "N";
                //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_ProFee = "Y";
                //            else
                //                str_ProFee = "N";
                //        }
                //        if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_GWChargeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_gwCharge = "Y";
                //            else
                //                str_gwCharge = "N";
                //        }
                //        if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_PenaltySADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_Penalty = "Y";
                //            else
                //                str_Penalty = "N";
                //        }



                //        if (obj_infrastructureNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry && obj_infrastructureNewSADApplication.WaterQualityCode == 1)
                //        {

                //            if (str_ProFee == "Y" && str_gwCharge == "Y" && str_Penalty == "Y")
                //                lnkbtnMakePayment.Enabled = false;
                //            else
                //                lnkbtnMakePayment.Enabled = true;
                //        }
                //        else
                //        {
                //            if (obj_infrastructureNewSADApplication.WaterQualityCode == 1)
                //            {
                //                if (str_ProFee == "Y" && str_gwCharge == "Y")
                //                    lnkbtnMakePayment.Enabled = false;
                //                else
                //                    lnkbtnMakePayment.Enabled = true;
                //            }
                //            else if (obj_infrastructureNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry)
                //            {
                //                if (str_ProFee == "Y" && str_Penalty == "Y")
                //                    lnkbtnMakePayment.Enabled = false;
                //                else
                //                    lnkbtnMakePayment.Enabled = true;
                //            }
                //            else
                //            {
                //                if (str_ProFee == "Y")
                //                    lnkbtnMakePayment.Enabled = false;
                //                else
                //                    lnkbtnMakePayment.Enabled = true;
                //            }

                //        }




                //        #endregion

                //        #region Submit Button Enable/Disable
                //        string str_subProFee = "N", str_subgwCharge = "N", str_subPenalty = "N";
                //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus ==
                //               NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_subProFee = "Y";
                //            else
                //                str_subProFee = "N";
                //        }
                //        if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_GWChargeSADOnlinePayment.FinalPaymentStatus ==
                //           NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_subgwCharge = "Y";
                //            else
                //                str_subgwCharge = "N";
                //        }
                //        if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_PenaltySADOnlinePayment.FinalPaymentStatus ==
                //            NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_subPenalty = "Y";
                //            else
                //                str_subPenalty = "N";
                //        }



                //        if (obj_infrastructureNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry && obj_infrastructureNewSADApplication.WaterQualityCode == 1)
                //        {
                //            if (str_subProFee == "Y" && str_subgwCharge == "Y" && str_subPenalty == "Y")
                //                lbtnSubmit.Enabled = true;
                //            else
                //                lbtnSubmit.Enabled = false;
                //        }
                //        else
                //        {

                //            if (obj_infrastructureNewSADApplication.WaterQualityCode == 1)
                //            {
                //                if (str_subProFee == "Y" && str_subgwCharge == "Y")
                //                    lbtnSubmit.Enabled = true;
                //                else
                //                    lbtnSubmit.Enabled = false;
                //            }
                //            else if (obj_infrastructureNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry)
                //            {
                //                if (str_subProFee == "Y" && str_subPenalty == "Y")
                //                    lbtnSubmit.Enabled = true;
                //                else
                //                    lbtnSubmit.Enabled = false;
                //            }
                //            else
                //            {
                //                if (str_subProFee == "Y")
                //                    lbtnSubmit.Enabled = true;
                //                else
                //                    lbtnSubmit.Enabled = false;
                //            }
                //        }


                //        #endregion

                //    }
                //    #endregion
                //    else { lbtnSubmit.Enabled = false; }
                //}
                //else { lbtnSubmit.Enabled = false; lnkbtnMakePayment.Enabled = false; }






            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    protected void gvInfRenewSaveAsDraft_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();

                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplicationPrev = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
                NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplicationPrev = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication();


                long RenewApplicationCode = Convert.ToInt64(gvInfRenewSaveAsDraft.DataKeys[e.Row.RowIndex].Values["InfrastructureRenewApplicationCode"]);
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(RenewApplicationCode);
                obj_infrastructureNewApplication = obj_infrastructureRenewSADApplication.GetFirstInfrastructureApplication();
                if (obj_infrastructureNewApplication != null)
                {
                    Label lblApplicationNumber = (Label)e.Row.FindControl("lblApplicationNumber");
                    Label lblRenewalCount = (Label)e.Row.FindControl("lblRenewalCount");
                    Label lblNOCNumber = (Label)e.Row.FindControl("lblNOCNumber");
                    Label lblIssueLetterStartDate = (Label)e.Row.FindControl("lblIssueLetterStartDate");
                    Label lblIssueLetterEndDate = (Label)e.Row.FindControl("lblIssueLetterEndDate");
                    Label lblOldApplicationNumber = (Label)e.Row.FindControl("lblOldApplicationNumber");
                    Label lblOldNOCNumber = (Label)e.Row.FindControl("lblOldNOCNumber");
                    // LinkButton lnkPaymentStatus = (LinkButton)e.Row.FindControl("infrenlnkPaymentStatus");

                    LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                    LinkButton lbtnSubmit = (LinkButton)e.Row.FindControl("lbtnSubmit");
                    LinkButton lnkbtnMakePayment = (LinkButton)e.Row.FindControl("lnkbtnMakePayment");

                    NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_ProFeeSADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_infrastructureRenewSADApplication.InfrastructureRenewApplicationCode, (obj_infrastructureRenewSADApplication.ProFeeOrderPaymentCode == null ? "0" : obj_infrastructureRenewSADApplication.ProFeeOrderPaymentCode.ToString()));
                    NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_GWChargeSADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_infrastructureRenewSADApplication.InfrastructureRenewApplicationCode, (obj_infrastructureRenewSADApplication.GWChargeOrderPaymentCode == null ? "0" : obj_infrastructureRenewSADApplication.GWChargeOrderPaymentCode.ToString()));
                    NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_PenaltySADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_infrastructureRenewSADApplication.InfrastructureRenewApplicationCode, (obj_infrastructureRenewSADApplication.PenaltyOrderPaymentCode == null ? "0" : obj_infrastructureRenewSADApplication.PenaltyOrderPaymentCode.ToString()));
                    lbtnEdit.Enabled = true;
                    lnkbtnMakePayment.Enabled = true;
                    NOCAP.BLL.Master.NTRPIntegration obj_nTRPIntegration = new NOCAP.BLL.Master.NTRPIntegration((int)NOCAP.BLL.Common.CommonEnum.ApplicationType.Infrastructure);

                    EnableDisableLinkButton(obj_nTRPIntegration.Active, RenewApplicationCode, lbtnEdit, lnkbtnMakePayment,
                   lbtnSubmit, obj_infrastructureRenewSADApplication.ReadyToSubmit,
                   obj_infrastructureRenewSADApplication.PaymentTypeMode, (obj_infrastructureRenewSADApplication.WaterQualityCode == null ? 0 : (int)obj_infrastructureRenewSADApplication.WaterQualityCode)
                   , objA_ProFeeSADOnlinePayment, objA_GWChargeSADOnlinePayment, objA_PenaltySADOnlinePayment
                   , NOCAPExternalUtility.AreaTypeCategoryCodeForAppCode(null, new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_infrastructureRenewSADApplication.FirstApplicationCode))
                   );
                    //if (obj_infrastructureRenewSADApplication.ReadyToSubmit == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                    //{
                    //    #region PaymentTypeMode Combined
                    //    if (obj_infrastructureRenewSADApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined)
                    //    {
                    //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            lbtnEdit.Enabled = false;
                    //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus != NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Failed && (obj_infrastructureRenewSADApplication.ReadyToSubmit == NOCAP.BLL.Common.CommonEnum.YesNoOption.No || obj_infrastructureRenewSADApplication.ReadyToSubmit == NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined))
                    //                lnkbtnMakePayment.Enabled = false;
                    //            else
                    //                lnkbtnMakePayment.Enabled = true;
                    //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                lbtnSubmit.Enabled = true;
                    //            else
                    //                lbtnSubmit.Enabled = false;
                    //        }

                    //    }
                    //    #endregion

                    //    #region PaymentTypeMode Single
                    //    else if (obj_infrastructureRenewSADApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single)
                    //    {

                    //        #region Edit Button Enable/Disable
                    //        if ((objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0) || (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0) || (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0))
                    //            lbtnEdit.Enabled = false;
                    //        #endregion

                    //        #region MakePayment Button Enable/Disable
                    //        string str_ProFee = "N", str_gwCharge = "N", str_Penalty = "N";
                    //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                str_ProFee = "Y";
                    //            else
                    //                str_ProFee = "N";
                    //        }
                    //        if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            if (objA_GWChargeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                str_gwCharge = "Y";
                    //            else
                    //                str_gwCharge = "N";
                    //        }
                    //        if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            if (objA_PenaltySADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                str_Penalty = "Y";
                    //            else
                    //                str_Penalty = "N";
                    //        }




                    //        if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_infrastructureRenewSADApplication.InfrastructureRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes && obj_infrastructureRenewSADApplication.WaterQualityCode == 1)
                    //        {
                    //            if (str_ProFee == "Y" && str_gwCharge == "Y" && str_Penalty == "Y")
                    //                lnkbtnMakePayment.Enabled = false;
                    //            else
                    //                lnkbtnMakePayment.Enabled = true;
                    //        }
                    //        else if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_infrastructureRenewSADApplication.InfrastructureRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.No)
                    //        {
                    //            if (obj_infrastructureRenewSADApplication.WaterQualityCode == 1)
                    //            {
                    //                if (str_ProFee == "Y" && str_gwCharge == "Y")
                    //                    lnkbtnMakePayment.Enabled = false;
                    //            }
                    //            else
                    //            {
                    //                if (str_ProFee == "Y")
                    //                    lnkbtnMakePayment.Enabled = false;
                    //                else
                    //                    lnkbtnMakePayment.Enabled = true;
                    //            }
                    //        }
                    //        else { lnkbtnMakePayment.Enabled = true; }


                    //        #endregion

                    //        #region Submit Button Enable/Disable
                    //        string str_subProFee = "N", str_subgwCharge = "N", str_subPenalty = "N";
                    //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus ==
                    //               NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                str_subProFee = "Y";
                    //            else
                    //                str_subProFee = "N";
                    //        }
                    //        if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            if (objA_GWChargeSADOnlinePayment.FinalPaymentStatus ==
                    //           NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                str_subgwCharge = "Y";
                    //            else
                    //                str_subgwCharge = "N";
                    //        }
                    //        if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            if (objA_PenaltySADOnlinePayment.FinalPaymentStatus ==
                    //            NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                str_subPenalty = "Y";
                    //            else
                    //                str_subPenalty = "N";
                    //        }

                    //        if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_infrastructureRenewSADApplication.InfrastructureRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes && obj_infrastructureRenewSADApplication.WaterQualityCode == 1)
                    //        {
                    //            if (str_subProFee == "Y" && str_subgwCharge == "Y" && str_subPenalty == "Y")
                    //                lbtnSubmit.Enabled = true;
                    //            else
                    //                lbtnSubmit.Enabled = false;
                    //        }
                    //        else if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_infrastructureRenewSADApplication.InfrastructureRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.No)
                    //        {
                    //            if (obj_infrastructureRenewSADApplication.WaterQualityCode == 1)
                    //            {
                    //                if (str_subProFee == "Y" && str_subgwCharge == "Y")
                    //                    lbtnSubmit.Enabled = true;

                    //            }
                    //            else
                    //            {
                    //                if (str_subProFee == "Y")
                    //                    lbtnSubmit.Enabled = true;
                    //                else
                    //                    lbtnSubmit.Enabled = false;
                    //            }

                    //        }
                    //        else { lbtnSubmit.Enabled = false; }

                    //        #endregion

                    //    }
                    //    #endregion
                    //    else
                    //    { lbtnSubmit.Enabled = false; }
                    //}
                    //else { lbtnSubmit.Enabled = false; lnkbtnMakePayment.Enabled = false; }




                    if (obj_infrastructureRenewSADApplication.GetPreviousInfrastructureApplication(out obj_infrastructureNewApplicationPrev, out obj_infrastructureRenewApplicationPrev) == 1)
                    {
                        if (obj_infrastructureNewApplicationPrev != null)
                        {
                            lblNOCNumber.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplicationPrev.NOCNumber);

                            NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_infrastructureNewIssusedLetter = new NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter(obj_infrastructureNewApplicationPrev.ApplicationCode);
                            if (obj_infrastructureNewIssusedLetter.ValidityStartDate != null)
                            {
                                lblIssueLetterStartDate.Text = "(" + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_infrastructureNewIssusedLetter.ValidityStartDate)).ToString("dd/MM/yyyy");
                            }
                            if (obj_infrastructureNewIssusedLetter.ValidityEndDate != null)
                            {
                                lblIssueLetterEndDate.Text = " - " + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_infrastructureNewIssusedLetter.ValidityEndDate)).ToString("dd/MM/yyyy") + ")";
                            }
                        }
                        else if (obj_infrastructureRenewApplicationPrev != null)
                        {
                            lblNOCNumber.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewApplicationPrev.NOCNumber);

                            NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter obj_infrastructureRenewIssusedLetter = new NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter(obj_infrastructureRenewApplicationPrev.InfrastructureRenewApplicationCode);
                            if (obj_infrastructureRenewIssusedLetter.ValidityStartDate != null)
                            {
                                lblIssueLetterStartDate.Text = "(" + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_infrastructureRenewIssusedLetter.ValidityStartDate)).ToString("dd/MM/yyyy");
                            }
                            if (obj_infrastructureRenewIssusedLetter.ValidityEndDate != null)
                            {
                                lblIssueLetterEndDate.Text = " - " + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_infrastructureRenewIssusedLetter.ValidityEndDate)).ToString("dd/MM/yyyy") + ")";
                            }

                        }
                    }

                    if (obj_infrastructureNewApplication.InfrastructureOldApplicationNumber.Trim() != "")
                    {
                        lblOldApplicationNumber.Text = "(Old: " + HttpUtility.HtmlEncode(obj_infrastructureNewApplication.InfrastructureOldApplicationNumber) + ")";
                    }
                    if (obj_infrastructureNewApplication.NOCNumberOld.Trim() != "")
                    {
                        lblOldNOCNumber.Text = "<br/>(Old: " + HttpUtility.HtmlEncode(obj_infrastructureNewApplication.NOCNumberOld) + ")";
                    }
                    lblApplicationNumber.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.InfrastructureNewApplicationNumber);
                    lblRenewalCount.Text = HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(obj_infrastructureRenewSADApplication.LinkDepth));  //added html encode lblRenewalCount.Text = NOCAPExternalUtility.AddOrdinal(obj_infrastructureRenewSADApplication.LinkDepth);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    protected void gvInfNewSubmitted_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        try
        {
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfratsructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
            Label lblLatestAppStatusCode = new Label();
            LinkButton lnkbtn = new LinkButton();
            LinkButton lnkbtnScan = new LinkButton();


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                object objTemp = gvInfNewSubmitted.DataKeys[e.Row.RowIndex].Value as object;
                string id = objTemp.ToString();
                NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter objInfratsructureNewIssusedLetter = new NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter();
                objInfratsructureNewIssusedLetter.populateInfrastructureNewIssusedLetterForINFAppCode(Convert.ToInt32(id));
                lnkbtn = (LinkButton)e.Row.FindControl("lbtnInfDownload");
                lnkbtnScan = (LinkButton)e.Row.FindControl("lbtnScanInfDownload");
                LinkButton lnkNOCNumber = (LinkButton)e.Row.FindControl("lnkNOCNumber");
                Label lblApplyType = (Label)e.Row.FindControl("lblApplyType");
                LinkButton lnkCompliance = (LinkButton)e.Row.FindControl("lnkCompliance");
                LinkButton lnkInspection = (LinkButton)e.Row.FindControl("lnkInspection");

                LinkButton lnkInfRenewDetail = (LinkButton)e.Row.FindControl("lnkInfRenewDetail");


                ViewState["path"] = HttpUtility.HtmlEncode(objInfratsructureNewIssusedLetter.AttPath);
                //Made by Ashu
                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(Convert.ToInt64(lnkNOCNumber.Text));
                lnkNOCNumber.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.NOCNumber);

                // start Self Compliacne

                NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_infrastructureNewIssusedLetter = obj_InfrastructureNewApplication.GetIssuedLetter();

                if (obj_infrastructureNewIssusedLetter != null && obj_infrastructureNewIssusedLetter.ValidityStartDate != null && lnkNOCNumber.Text.Trim() != "")
                {
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(gvInfNewSubmitted.DataKeys[e.Row.RowIndex].Value));

                    if (obj_selfCompliance.ApplicationCode != 0 && obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                    {
                        lnkCompliance.Text = "Self Compliance (Filled)";
                        lnkCompliance.Visible = true;
                    }
                    else
                    {

                        if (Convert.ToDateTime(obj_infrastructureNewIssusedLetter.ValidityStartDate).AddMonths(11) < DateTime.Now && Convert.ToDateTime(obj_infrastructureNewIssusedLetter.ValidityEndDate) > DateTime.Now && obj_InfrastructureNewApplication.LatestApplicationStatusCode == Convert.ToInt32(NOCAP.BLL.Utility.Utility.LatestApplicationStatusCodeOption.Approved))
                            lnkCompliance.Visible = true;
                        else
                            lnkCompliance.Visible = true;
                        lnkCompliance.Text = "Self Compliance (Not Filled)";
                    }
                    // End Self Compliance

                    NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_SelfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(Convert.ToInt64(gvInfNewSubmitted.DataKeys[e.Row.RowIndex].Value));
                    if (obj_SelfInspection.ApplicationCode != 0 && obj_SelfInspection.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                    {
                        lnkInspection.Text = "Self Inspection (Filled)";
                        lnkInspection.Visible = true;
                    }
                    else
                    {
                        if (Convert.ToDateTime(obj_infrastructureNewIssusedLetter.ValidityEndDate).AddMonths(-6) <= DateTime.Now && obj_InfrastructureNewApplication.LatestApplicationStatusCode == Convert.ToInt32(NOCAP.BLL.Utility.Utility.LatestApplicationStatusCodeOption.Approved))
                            lnkInspection.Visible = true;
                        else
                            lnkInspection.Visible = true;
                        lnkInspection.Text = "Self Inspection (Not Filled)";
                    }

                    // End Self Inspection
                    if (obj_InfrastructureNewApplication.RewAppCodeFinally != null)
                    {
                        lnkInfRenewDetail.Text = "Detail";
                    }
                    NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfratsructureNewApplicationEligibleForExemption = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(Convert.ToInt64(gvInfNewSubmitted.DataKeys[e.Row.RowIndex].Value));
                    string EligibleForExemptionLett = Convert.ToString(obj_InfratsructureNewApplicationEligibleForExemption.EligibleForExemptionLetter);
                    LinkButton lnkBtn = new LinkButton();
                    lnkBtn = (LinkButton)e.Row.FindControl("lbtnEdit");
                    if (EligibleForExemptionLett == "Yes")
                    {
                        lnkBtn.Enabled = false;
                        lnkBtn.Text = "";
                        lnkBtn.ForeColor = System.Drawing.Color.Black;
                        lnkBtn.Style.Add("text-decoration", "none");
                        //lnkBtn.CssClass = "NoUnderLine";
                    }
                    else if (EligibleForExemptionLett == "No")
                    {
                        lnkBtn.Visible = true;
                    }



                    if (objTemp != null)
                    {
                        if (objInfratsructureNewIssusedLetter.AttPath != null && objInfratsructureNewIssusedLetter.ScanAttPath != null && objInfratsructureNewIssusedLetter.ScanAttPath != "" && objInfratsructureNewIssusedLetter.AttPath != "")
                        {
                            lnkbtn.Visible = true;
                            lnkbtnScan.Visible = true;
                            int intIssueLetterTypeCode = objInfratsructureNewIssusedLetter.IssuedLetterTypeCode;
                            NOCAP.BLL.Master.IssuedLettersType objIssuedLettersType = new NOCAP.BLL.Master.IssuedLettersType();
                            //  NOCAP.BLL.Master.IssuedLettersType objIssuedLettersTypeFill = new NOCAP.BLL.Master.IssuedLettersType();
                            int returnValue = objIssuedLettersType.populateIssuedLetterTypeForIssuedLetterTypeCode(intIssueLetterTypeCode);
                            if (returnValue != 0)
                            {
                                lnkbtn.Text = HttpUtility.HtmlEncode(objIssuedLettersType.IssuedLetterTypeName);  // added html encode lnkbtn.Text = objIssuedLettersType.IssuedLetterTypeName;
                            }
                        }
                        else if (objInfratsructureNewIssusedLetter.AttPath != "" && objInfratsructureNewIssusedLetter.AttPath != null)
                        {
                            lnkbtnScan.Visible = false;
                            lnkbtn.Visible = true;
                            int intIssueLetterTypeCode = objInfratsructureNewIssusedLetter.IssuedLetterTypeCode;
                            NOCAP.BLL.Master.IssuedLettersType objIssuedLettersType = new NOCAP.BLL.Master.IssuedLettersType();
                            objIssuedLettersType.populateIssuedLetterTypeForIssuedLetterTypeCode(intIssueLetterTypeCode);
                            if (objIssuedLettersType != null)
                            {
                                lnkbtn.Text = HttpUtility.HtmlEncode(objIssuedLettersType.IssuedLetterTypeName); //added html encode lnkbtn.Text = objIssuedLettersType.IssuedLetterTypeName;
                            }
                        }
                        else if (objInfratsructureNewIssusedLetter.ScanAttPath != "" && objInfratsructureNewIssusedLetter.ScanAttPath != null)
                        {
                            lnkbtn.Visible = false;
                            lnkbtnScan.Visible = true;

                        }
                        else
                        {
                            lnkbtn.Visible = false;
                            lnkbtnScan.Visible = false;
                        }
                    }
                    lblLatestAppStatusCode = (Label)e.Row.FindControl("lblLatestAppStatusCode1");
                    obj_InfratsructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(Convert.ToInt32(gvInfNewSubmitted.DataKeys[e.Row.RowIndex].Value.ToString()));
                    if (!string.IsNullOrEmpty(obj_InfratsructureNewApplication.GetApplicationStatus().ApplicationStatusDescription))
                    {
                        lblLatestAppStatusCode.Text = "( " + HttpUtility.HtmlEncode(obj_InfratsructureNewApplication.GetApplicationStatus().ApplicationStatusDescription) + " )";  //added html encode lblLatestAppStatusCode.Text = "( " + obj_InfratsructureNewApplication.GetApplicationStatus().ApplicationStatusDescription + " )";

                        //Start For Presentation 
                        Label lblLatestPresentReq = (Label)e.Row.FindControl("lblLatestPresentReq1");
                        if (obj_InfratsructureNewApplication.PresentationCalledSerialNumber != null)
                        {
                            NOCAP.BLL.Misc.Presentation.PresentationCalled obj_PresentationCalled = new NOCAP.BLL.Misc.Presentation.PresentationCalled(obj_InfratsructureNewApplication.ApplicationCode, obj_InfratsructureNewApplication.PresentationCalledSerialNumber);
                            if (obj_PresentationCalled.ApplicationCode != 0)
                            {
                                if (obj_PresentationCalled.PresentCalled == NOCAP.BLL.Common.CommonEnum.PresentationCalledOption.Yes)
                                {
                                    if (obj_PresentationCalled.PresentationFinalize == NOCAP.BLL.Common.CommonEnum.PresentationFinalizeOption.No)
                                    {
                                        lblLatestPresentReq.Text = "<br />Presentation<br />Required";
                                    }
                                }
                            }
                        }
                        //End For Presentation 
                    }
                    if (obj_InfratsructureNewApplication.SubmittedType == NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication.SubmittedTypeOption.Online)
                    {
                        lblApplyType.Text = "Online";
                    }
                    else
                    {
                        lblApplyType.Text = "Archival";
                        lnkBtn.Visible = false;
                    }

                }
                else
                {
                    lnkCompliance.Visible = true;
                    lnkInspection.Visible = false;
                }
            }
        }
        catch (Exception Ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }


    }

    protected void gvInFExpansionSaveAsDraft_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                long NewApplicationCode = Convert.ToInt64(gvInfNewSaveAsDraft.DataKeys[e.Row.RowIndex].Values["ApplicationCode"]);
                NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication obj_infrastructureExpansionApplication =
                    new NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication(NewApplicationCode);

                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewSADApplication =
                   new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(NewApplicationCode);

                LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton lnkbtnMakePayment = (LinkButton)e.Row.FindControl("lnkbtnMakePayment");
                LinkButton lbtnSubmit = (LinkButton)e.Row.FindControl("lbtnSubmit");

                NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_ProFeeSADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_infrastructureExpansionApplication.InfrastructureNewApplicationCode, (obj_infrastructureExpansionApplication.ProFeeOrderPaymentCode == null ? "0" : obj_infrastructureExpansionApplication.ProFeeOrderPaymentCode.ToString()));
                NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_GWChargeSADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_infrastructureExpansionApplication.InfrastructureNewApplicationCode, (obj_infrastructureExpansionApplication.GWChargeOrderPaymentCode == null ? "0" : obj_infrastructureExpansionApplication.GWChargeOrderPaymentCode.ToString()));
                NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_PenaltySADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_infrastructureExpansionApplication.InfrastructureNewApplicationCode, (obj_infrastructureExpansionApplication.PenaltyOrderPaymentCode == null ? "0" : obj_infrastructureExpansionApplication.PenaltyOrderPaymentCode.ToString()));
                lnkbtnMakePayment.Enabled = true;
                lbtnEdit.Enabled = true;
                NOCAP.BLL.Master.NTRPIntegration obj_nTRPIntegration = new NOCAP.BLL.Master.NTRPIntegration((int)NOCAP.BLL.Common.CommonEnum.ApplicationType.Infrastructure);

                EnableDisableLinkButton(obj_nTRPIntegration.Active, NewApplicationCode, lbtnEdit, lnkbtnMakePayment,
                    lbtnSubmit, obj_infrastructureExpansionApplication.ReadyToSubmit,
                    obj_infrastructureExpansionApplication.PaymentTypeMode, obj_infrastructureExpansionApplication.WaterQualityCode
                    , objA_ProFeeSADOnlinePayment, objA_GWChargeSADOnlinePayment, objA_PenaltySADOnlinePayment
                    , NOCAPExternalUtility.AreaTypeCategoryCode(null, obj_infrastructureNewSADApplication)
                    );
                //if (obj_infrastructureNewSADApplication.ReadyToSubmit == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                //{
                //    #region PaymentTypeMode Combined
                //    if (obj_infrastructureNewSADApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined)
                //    {
                //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            lbtnEdit.Enabled = false;
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus != NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Failed && (obj_infrastructureNewSADApplication.ReadyToSubmit == NOCAP.BLL.Common.CommonEnum.YesNoOption.No || obj_infrastructureNewSADApplication.ReadyToSubmit == NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined))
                //                lnkbtnMakePayment.Enabled = false;
                //            else
                //                lnkbtnMakePayment.Enabled = true;
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                lbtnSubmit.Enabled = true;
                //            else
                //                lbtnSubmit.Enabled = false;
                //        }

                //    }
                //    #endregion

                //    #region PaymentTypeMode Single
                //    else if (obj_infrastructureNewSADApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single)
                //    {

                //        #region Edit Button Enable/Disable
                //        if ((objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0) || (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0) || (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0))
                //            lbtnEdit.Enabled = false;
                //        #endregion

                //        #region MakePayment Button Enable/Disable
                //        string str_ProFee = "N", str_gwCharge = "N", str_Penalty = "N";
                //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_ProFee = "Y";
                //            else
                //                str_ProFee = "N";
                //        }
                //        if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_GWChargeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_gwCharge = "Y";
                //            else
                //                str_gwCharge = "N";
                //        }
                //        if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_PenaltySADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_Penalty = "Y";
                //            else
                //                str_Penalty = "N";
                //        }



                //        if (obj_infrastructureNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry && obj_infrastructureNewSADApplication.WaterQualityCode == 1)
                //        {

                //            if (str_ProFee == "Y" && str_gwCharge == "Y" && str_Penalty == "Y")
                //                lnkbtnMakePayment.Enabled = false;
                //            else
                //                lnkbtnMakePayment.Enabled = true;
                //        }
                //        else
                //        {
                //            if (obj_infrastructureNewSADApplication.WaterQualityCode == 1)
                //            {
                //                if (str_ProFee == "Y" && str_gwCharge == "Y")
                //                    lnkbtnMakePayment.Enabled = false;
                //                else
                //                    lnkbtnMakePayment.Enabled = true;
                //            }
                //            else if (obj_infrastructureNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry)
                //            {
                //                if (str_ProFee == "Y" && str_Penalty == "Y")
                //                    lnkbtnMakePayment.Enabled = false;
                //                else
                //                    lnkbtnMakePayment.Enabled = true;
                //            }
                //            else
                //            {
                //                if (str_ProFee == "Y")
                //                    lnkbtnMakePayment.Enabled = false;
                //                else
                //                    lnkbtnMakePayment.Enabled = true;
                //            }

                //        }




                //        #endregion

                //        #region Submit Button Enable/Disable
                //        string str_subProFee = "N", str_subgwCharge = "N", str_subPenalty = "N";
                //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus ==
                //               NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_subProFee = "Y";
                //            else
                //                str_subProFee = "N";
                //        }
                //        if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_GWChargeSADOnlinePayment.FinalPaymentStatus ==
                //           NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_subgwCharge = "Y";
                //            else
                //                str_subgwCharge = "N";
                //        }
                //        if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_PenaltySADOnlinePayment.FinalPaymentStatus ==
                //            NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_subPenalty = "Y";
                //            else
                //                str_subPenalty = "N";
                //        }



                //        if (obj_infrastructureNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry && obj_infrastructureNewSADApplication.WaterQualityCode == 1)
                //        {
                //            if (str_subProFee == "Y" && str_subgwCharge == "Y" && str_subPenalty == "Y")
                //                lbtnSubmit.Enabled = true;
                //            else
                //                lbtnSubmit.Enabled = false;
                //        }
                //        else
                //        {

                //            if (obj_infrastructureNewSADApplication.WaterQualityCode == 1)
                //            {
                //                if (str_subProFee == "Y" && str_subgwCharge == "Y")
                //                    lbtnSubmit.Enabled = true;
                //                else
                //                    lbtnSubmit.Enabled = false;
                //            }
                //            else if (obj_infrastructureNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry)
                //            {
                //                if (str_subProFee == "Y" && str_subPenalty == "Y")
                //                    lbtnSubmit.Enabled = true;
                //                else
                //                    lbtnSubmit.Enabled = false;
                //            }
                //            else
                //            {
                //                if (str_subProFee == "Y")
                //                    lbtnSubmit.Enabled = true;
                //                else
                //                    lbtnSubmit.Enabled = false;
                //            }
                //        }


                //        #endregion

                //    }
                //    #endregion
                //    else { lbtnSubmit.Enabled = false; }
                //}
                //else { lbtnSubmit.Enabled = false; lnkbtnMakePayment.Enabled = false; }






            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    #endregion

    #region MIN
    protected void gvMINNewSaveAsDraft_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                long NewApplicationCode = Convert.ToInt64(gvMINNewSaveAsDraft.DataKeys[e.Row.RowIndex].Values["ApplicationCode"]);
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_miningNewSADApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(NewApplicationCode);


                // LinkButton lnkPaymentStatus = (LinkButton)e.Row.FindControl("minlnkPaymentStatus");


                LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton lbtnSubmit = (LinkButton)e.Row.FindControl("lbtnMinSubmit");
                LinkButton lnkbtnMakePayment = (LinkButton)e.Row.FindControl("lnkbtnMakePayment");





                NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_ProFeeSADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_miningNewSADApplication.ApplicationCode, (obj_miningNewSADApplication.ProFeeOrderPaymentCode == null ? "0" : obj_miningNewSADApplication.ProFeeOrderPaymentCode.ToString()));
                NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_GWChargeSADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_miningNewSADApplication.ApplicationCode, (obj_miningNewSADApplication.GWChargeOrderPaymentCode == null ? "0" : obj_miningNewSADApplication.GWChargeOrderPaymentCode.ToString()));
                NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_PenaltySADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_miningNewSADApplication.ApplicationCode, (obj_miningNewSADApplication.PenaltyOrderPaymentCode == null ? "0" : obj_miningNewSADApplication.PenaltyOrderPaymentCode.ToString()));
                lbtnEdit.Enabled = true;
                lnkbtnMakePayment.Enabled = true;
                NOCAP.BLL.Master.NTRPIntegration obj_nTRPIntegration = new NOCAP.BLL.Master.NTRPIntegration((int)NOCAP.BLL.Common.CommonEnum.ApplicationType.Mining);

                EnableDisableLinkButton(obj_nTRPIntegration.Active, NewApplicationCode, lbtnEdit, lnkbtnMakePayment,
                   lbtnSubmit, obj_miningNewSADApplication.ReadyToSubmit,
                   obj_miningNewSADApplication.PaymentTypeMode, obj_miningNewSADApplication.WaterQualityCode
                   , objA_ProFeeSADOnlinePayment, objA_GWChargeSADOnlinePayment, objA_PenaltySADOnlinePayment
                   , NOCAPExternalUtility.AreaTypeCategoryCode(null, null, obj_miningNewSADApplication)
                   );
                //if (obj_miningNewSADApplication.ReadyToSubmit == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                //{
                //    #region PaymentTypeMode Combined
                //    if (obj_miningNewSADApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined)
                //    {
                //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            lbtnEdit.Enabled = false;
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus != NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Failed)
                //                lnkbtnMakePayment.Enabled = false;
                //            else
                //                lnkbtnMakePayment.Enabled = true;
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                lbtnSubmit.Enabled = true;
                //            else
                //                lbtnSubmit.Enabled = false;
                //        }

                //    }
                //    #endregion

                //    #region PaymentTypeMode Single
                //    else if (obj_miningNewSADApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single)
                //    {

                //        #region Edit Button Enable/Disable
                //        if ((objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0) || (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0) || (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0))
                //            lbtnEdit.Enabled = false;
                //        #endregion

                //        #region MakePayment Button Enable/Disable
                //        string str_ProFee = "N", str_gwCharge = "N", str_Penalty = "N";
                //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_ProFee = "Y";
                //            else
                //                str_ProFee = "N";
                //        }
                //        if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_GWChargeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_gwCharge = "Y";
                //            else
                //                str_gwCharge = "N";
                //        }
                //        if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_PenaltySADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_Penalty = "Y";
                //            else
                //                str_Penalty = "N";
                //        }





                //        if (obj_miningNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry && obj_miningNewSADApplication.WaterQualityCode == 1)
                //        {

                //            if (str_ProFee == "Y" && str_gwCharge == "Y" && str_Penalty == "Y")
                //                lnkbtnMakePayment.Enabled = false;
                //            else
                //                lnkbtnMakePayment.Enabled = true;
                //        }
                //        else
                //        {
                //            if (obj_miningNewSADApplication.WaterQualityCode == 1)
                //            {
                //                if (str_ProFee == "Y" && str_gwCharge == "Y")
                //                    lnkbtnMakePayment.Enabled = false;
                //                else
                //                    lnkbtnMakePayment.Enabled = true;
                //            }
                //            else if (obj_miningNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry)
                //            {
                //                if (str_ProFee == "Y" && str_Penalty == "Y")
                //                    lnkbtnMakePayment.Enabled = false;
                //                else
                //                    lnkbtnMakePayment.Enabled = true;
                //            }
                //            else
                //            {
                //                if (str_ProFee == "Y")
                //                    lnkbtnMakePayment.Enabled = false;
                //                else
                //                    lnkbtnMakePayment.Enabled = true;
                //            }

                //        }


                //        #endregion

                //        #region Submit Button Enable/Disable
                //        string str_subProFee = "N", str_subgwCharge = "N", str_subPenalty = "N";
                //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus ==
                //               NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_subProFee = "Y";
                //            else
                //                str_subProFee = "N";
                //        }
                //        if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_GWChargeSADOnlinePayment.FinalPaymentStatus ==
                //           NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_subgwCharge = "Y";
                //            else
                //                str_subgwCharge = "N";
                //        }
                //        if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_PenaltySADOnlinePayment.FinalPaymentStatus ==
                //            NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_subPenalty = "Y";
                //            else
                //                str_subPenalty = "N";
                //        }

                //        if (obj_miningNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry && obj_miningNewSADApplication.WaterQualityCode == 1)
                //        {
                //            if (str_subProFee == "Y" && str_subgwCharge == "Y" && str_subPenalty == "Y")
                //                lbtnSubmit.Enabled = true;
                //            else
                //                lbtnSubmit.Enabled = false;
                //        }
                //        else
                //        {

                //            if (obj_miningNewSADApplication.WaterQualityCode == 1)
                //            {
                //                if (str_subProFee == "Y" && str_subgwCharge == "Y")
                //                    lbtnSubmit.Enabled = true;
                //                else
                //                    lbtnSubmit.Enabled = false;
                //            }
                //            else if (obj_miningNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry)
                //            {
                //                if (str_subProFee == "Y" && str_subPenalty == "Y")
                //                    lbtnSubmit.Enabled = true;
                //                else
                //                    lbtnSubmit.Enabled = false;
                //            }
                //            else
                //            {
                //                if (str_subProFee == "Y")
                //                    lbtnSubmit.Enabled = true;
                //                else
                //                    lbtnSubmit.Enabled = false;
                //            }
                //        }


                //        #endregion

                //    }
                //    #endregion
                //    else
                //    { lbtnSubmit.Enabled = false; }
                //}
                //else
                //{ lbtnSubmit.Enabled = false; lnkbtnMakePayment.Enabled = false; }







            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    protected void gvMinRenewSaveAsDraft_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication();

                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplicationPrev = new NOCAP.BLL.Mining.New.Application.MiningNewApplication();
                NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplicationPrev = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication();


                long RenewApplicationCode = Convert.ToInt64(gvMinRenewSaveAsDraft.DataKeys[e.Row.RowIndex].Values["MiningRenewApplicationCode"]);
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewSADApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(RenewApplicationCode);
                obj_miningNewApplication = obj_miningRenewSADApplication.GetFirstMiningApplication();
                if (obj_miningNewApplication != null)
                {
                    Label lblApplicationNumber = (Label)e.Row.FindControl("lblApplicationNumber");
                    Label lblRenewalCount = (Label)e.Row.FindControl("lblRenewalCount");
                    Label lblNOCNumber = (Label)e.Row.FindControl("lblNOCNumber");
                    Label lblIssueLetterStartDate = (Label)e.Row.FindControl("lblIssueLetterStartDate");
                    Label lblIssueLetterEndDate = (Label)e.Row.FindControl("lblIssueLetterEndDate");
                    Label lblOldApplicationNumber = (Label)e.Row.FindControl("lblOldApplicationNumber");
                    Label lblOldNOCNumber = (Label)e.Row.FindControl("lblOldNOCNumber");
                    Label lblNameOfMining = (Label)e.Row.FindControl("NameOfMining");

                    //  LinkButton lnkPaymentStatus = (LinkButton)e.Row.FindControl("minrenlnkPaymentStatus");
                    LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                    LinkButton lnkbtnMakePayment = (LinkButton)e.Row.FindControl("lnkbtnMakePayment");
                    LinkButton lbtnSubmit = (LinkButton)e.Row.FindControl("lbtnMinSubmit");

                    NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_ProFeeSADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_miningRenewSADApplication.MiningRenewApplicationCode, (obj_miningRenewSADApplication.ProFeeOrderPaymentCode == null ? "0" : obj_miningRenewSADApplication.ProFeeOrderPaymentCode.ToString()));
                    NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_GWChargeSADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_miningRenewSADApplication.MiningRenewApplicationCode, (obj_miningRenewSADApplication.GWChargeOrderPaymentCode == null ? "0" : obj_miningRenewSADApplication.GWChargeOrderPaymentCode.ToString()));
                    NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_PenaltySADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_miningRenewSADApplication.MiningRenewApplicationCode, (obj_miningRenewSADApplication.PenaltyOrderPaymentCode == null ? "0" : obj_miningRenewSADApplication.PenaltyOrderPaymentCode.ToString()));
                    lbtnEdit.Enabled = true;
                    lnkbtnMakePayment.Enabled = true;
                    NOCAP.BLL.Master.NTRPIntegration obj_nTRPIntegration = new NOCAP.BLL.Master.NTRPIntegration((int)NOCAP.BLL.Common.CommonEnum.ApplicationType.Mining);

                    EnableDisableLinkButton(obj_nTRPIntegration.Active, RenewApplicationCode, lbtnEdit, lnkbtnMakePayment,
                  lbtnSubmit, obj_miningRenewSADApplication.ReadyToSubmit,
                  obj_miningRenewSADApplication.PaymentTypeMode, (obj_miningRenewSADApplication.WaterQualityCode == null ? 0 : (int)obj_miningRenewSADApplication.WaterQualityCode)
                  , objA_ProFeeSADOnlinePayment, objA_GWChargeSADOnlinePayment, objA_PenaltySADOnlinePayment
                  , NOCAPExternalUtility.AreaTypeCategoryCodeForAppCode(null, null, new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_miningRenewSADApplication.FirstApplicationCode))
                  );
                    //if (obj_miningRenewSADApplication.ReadyToSubmit == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                    //{
                    //    #region PaymentTypeMode Combined
                    //    if (obj_miningRenewSADApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined)
                    //    {
                    //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            lbtnEdit.Enabled = false;
                    //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus != NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Failed)
                    //                lnkbtnMakePayment.Enabled = false;
                    //            else
                    //                lnkbtnMakePayment.Enabled = true;
                    //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                lbtnSubmit.Enabled = true;
                    //            else
                    //                lbtnSubmit.Enabled = false;
                    //        }

                    //    }
                    //    #endregion

                    //    #region PaymentTypeMode Single
                    //    else if (obj_miningRenewSADApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single)
                    //    {

                    //        #region Edit Button Enable/Disable
                    //        if ((objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0) || (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0) || (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0))
                    //            lbtnEdit.Enabled = false;
                    //        #endregion

                    //        #region MakePayment Button Enable/Disable
                    //        string str_ProFee = "N", str_gwCharge = "N", str_Penalty = "N";
                    //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                str_ProFee = "Y";
                    //            else
                    //                str_ProFee = "N";
                    //        }
                    //        if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            if (objA_GWChargeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                str_gwCharge = "Y";
                    //            else
                    //                str_gwCharge = "N";
                    //        }
                    //        if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            if (objA_PenaltySADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                str_Penalty = "Y";
                    //            else
                    //                str_Penalty = "N";
                    //        }


                    //        if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_miningRenewSADApplication.MiningRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes && obj_miningRenewSADApplication.WaterQualityCode == 1)
                    //        {
                    //            if (str_ProFee == "Y" && str_gwCharge == "Y" && str_Penalty == "Y")
                    //                lnkbtnMakePayment.Enabled = false;
                    //            else
                    //                lnkbtnMakePayment.Enabled = true;
                    //        }
                    //        else if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_miningRenewSADApplication.MiningRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.No)
                    //        {
                    //            if (obj_miningRenewSADApplication.WaterQualityCode == 1)
                    //            {
                    //                if (str_ProFee == "Y" && str_gwCharge == "Y")
                    //                    lnkbtnMakePayment.Enabled = false;
                    //            }
                    //            else
                    //            {
                    //                if (str_ProFee == "Y")
                    //                    lnkbtnMakePayment.Enabled = false;
                    //                else
                    //                    lnkbtnMakePayment.Enabled = true;
                    //            }
                    //        }
                    //        else { lnkbtnMakePayment.Enabled = true; }

                    //        #endregion

                    //        #region Submit Button Enable/Disable
                    //        string str_subProFee = "N", str_subgwCharge = "N", str_subPenalty = "N";
                    //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus ==
                    //               NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                str_subProFee = "Y";
                    //            else
                    //                str_subProFee = "N";
                    //        }
                    //        if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            if (objA_GWChargeSADOnlinePayment.FinalPaymentStatus ==
                    //           NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                str_subgwCharge = "Y";
                    //            else
                    //                str_subgwCharge = "N";
                    //        }
                    //        if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                    //        {
                    //            if (objA_PenaltySADOnlinePayment.FinalPaymentStatus ==
                    //            NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                    //                str_subPenalty = "Y";
                    //            else
                    //                str_subPenalty = "N";
                    //        }


                    //        if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_miningRenewSADApplication.MiningRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes && obj_miningRenewSADApplication.WaterQualityCode == 1)
                    //        {
                    //            if (str_subProFee == "Y" && str_subgwCharge == "Y" && str_subPenalty == "Y")
                    //                lbtnSubmit.Enabled = true;
                    //            else
                    //                lbtnSubmit.Enabled = false;
                    //        }
                    //        else if (NOCAPExternalUtility.CheckPenaltyForSADApplication(obj_miningRenewSADApplication.MiningRenewApplicationCode) == NOCAP.BLL.Common.CommonEnum.YesNoOption.No)
                    //        {
                    //            if (obj_miningRenewSADApplication.WaterQualityCode == 1)
                    //            {
                    //                if (str_subProFee == "Y" && str_subgwCharge == "Y")
                    //                    lbtnSubmit.Enabled = true;

                    //            }
                    //            else
                    //            {
                    //                if (str_subProFee == "Y")
                    //                    lbtnSubmit.Enabled = true;
                    //                else
                    //                    lbtnSubmit.Enabled = false;
                    //            }

                    //        }
                    //        else { lbtnSubmit.Enabled = false; }

                    //        #endregion

                    //    }
                    //    #endregion
                    //    else { lbtnSubmit.Enabled = false; }
                    //}
                    //else { lbtnSubmit.Enabled = false; lnkbtnMakePayment.Enabled = false; }


                    lblNameOfMining.Text = HttpUtility.HtmlEncode(obj_miningNewApplication.NameOfMining);

                    if (obj_miningRenewSADApplication.GetPreviousMiningApplication(out obj_miningNewApplicationPrev, out obj_miningRenewApplicationPrev) == 1)
                    {
                        if (obj_miningNewApplicationPrev != null)
                        {
                            lblNOCNumber.Text = HttpUtility.HtmlEncode(obj_miningNewApplicationPrev.NOCNumber);

                            NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter obj_miningNewIssusedLetter = new NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter(obj_miningNewApplicationPrev.ApplicationCode);
                            if (obj_miningNewIssusedLetter.ValidityStartDate != null)
                            {
                                lblIssueLetterStartDate.Text = "(" + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_miningNewIssusedLetter.ValidityStartDate)).ToString("dd/MM/yyyy");
                            }
                            if (obj_miningNewIssusedLetter.ValidityEndDate != null)
                            {
                                lblIssueLetterEndDate.Text = " - " + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_miningNewIssusedLetter.ValidityEndDate)).ToString("dd/MM/yyyy") + ")";
                            }
                        }
                        else if (obj_miningRenewApplicationPrev != null)
                        {
                            lblNOCNumber.Text = HttpUtility.HtmlEncode(obj_miningRenewApplicationPrev.NOCNumber);

                            NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter obj_miningRenewIssusedLetter = new NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter(obj_miningRenewApplicationPrev.MiningRenewApplicationCode);
                            if (obj_miningRenewIssusedLetter.ValidityStartDate != null)
                            {
                                lblIssueLetterStartDate.Text = "(" + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_miningRenewIssusedLetter.ValidityStartDate)).ToString("dd/MM/yyyy");
                            }
                            if (obj_miningRenewIssusedLetter.ValidityEndDate != null)
                            {
                                lblIssueLetterEndDate.Text = " - " + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_miningRenewIssusedLetter.ValidityEndDate)).ToString("dd/MM/yyyy") + ")";
                            }
                        }
                    }

                    if (obj_miningNewApplication.MiningOldApplicationNumber.Trim() != "")
                    {
                        lblOldApplicationNumber.Text = "(Old: " + HttpUtility.HtmlEncode(obj_miningNewApplication.MiningOldApplicationNumber) + ")";
                    }
                    //if (obj_miningNewApplication.NOCNumberOld.Trim() != "")
                    //{
                    //    lblOldNOCNumber.Text = "<br/>(Old: " + HttpUtility.HtmlEncode(obj_miningNewApplication.NOCNumberOld) + ")";
                    //}
                    lblApplicationNumber.Text = HttpUtility.HtmlEncode(obj_miningNewApplication.MiningNewApplicationNumber);
                    lblRenewalCount.Text = HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(obj_miningRenewSADApplication.LinkDepth));  //added html encode lblRenewalCount.Text = NOCAPExternalUtility.AddOrdinal(obj_miningRenewSADApplication.LinkDepth);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    protected void gvMinNewSubmitted_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication();
            Label lblLatestAppStatusCode = new Label();
            LinkButton lnkbtn = new LinkButton();
            LinkButton lnkbtnScan = new LinkButton();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                object objTemp = gvMinNewSubmitted.DataKeys[e.Row.RowIndex].Value as object;
                string id = objTemp.ToString();
                NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter objMiningNewIssusedLetter = new NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter();
                objMiningNewIssusedLetter.populateMiningfornewMincode(Convert.ToInt32(id));
                lnkbtn = (LinkButton)e.Row.FindControl("lbtnMinDownload");
                lnkbtnScan = (LinkButton)e.Row.FindControl("lbtnScanMinDownload");
                LinkButton lnkNOCNumber = (LinkButton)e.Row.FindControl("lnkNOCNumber");
                Label lblApplyType = (Label)e.Row.FindControl("lblApplyType");
                LinkButton lnkCompliance = (LinkButton)e.Row.FindControl("lnkCompliance");
                LinkButton lnkInspection = (LinkButton)e.Row.FindControl("lnkInspection");

                LinkButton lnkMinRenewDetail = (LinkButton)e.Row.FindControl("lnkMinRenewDetail");


                ViewState["path"] = HttpUtility.HtmlEncode(objMiningNewIssusedLetter.AttPath);
                //Made by Ashu
                NOCAP.BLL.Mining.New.Application.MiningNewApplication objMiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(Convert.ToInt64(lnkNOCNumber.Text));
                lnkNOCNumber.Text = HttpUtility.HtmlEncode(objMiningNewApplication.NOCNumber);

                // start Self compliance

                NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter obj_miningNewIssusedLetter = objMiningNewApplication.GetIssuedLetter();

                if (obj_miningNewIssusedLetter != null && obj_miningNewIssusedLetter.ValidityStartDate != null && lnkNOCNumber.Text.Trim() != "")
                {
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(gvMinNewSubmitted.DataKeys[e.Row.RowIndex].Value));

                    //  if (obj_selfCompliance.ApplicationCode != 0 && obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes && objMiningNewApplication.SubmittedType == NOCAP.BLL.Mining.New.Application.MiningNewApplication.SubmittedTypeOption.Online)
                    if (obj_selfCompliance.ApplicationCode != 0 && obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                    {
                        lnkCompliance.Text = "Self Compliance (Filled)";
                        lnkCompliance.Visible = true;
                    }
                    else
                    {



                        if (Convert.ToDateTime(obj_miningNewIssusedLetter.ValidityStartDate).AddMonths(11) < DateTime.Now && Convert.ToDateTime(obj_miningNewIssusedLetter.ValidityEndDate) > DateTime.Now && objMiningNewApplication.LatestApplicationStatusCode == Convert.ToInt32(NOCAP.BLL.Utility.Utility.LatestApplicationStatusCodeOption.Approved))
                        {
                            lnkCompliance.Visible = true;
                        }
                        else
                        {
                            lnkCompliance.Visible = true;
                        }
                        lnkCompliance.Text = "Self Compliance (Not Filled)";
                    }
                    NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_SelfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(Convert.ToInt64(gvMinNewSubmitted.DataKeys[e.Row.RowIndex].Value));
                    if (obj_SelfInspection.ApplicationCode != 0 && obj_SelfInspection.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                    {
                        lnkInspection.Text = "Self Inspection (Filled)";
                        lnkInspection.Visible = true;
                    }
                    else
                    {
                        if (Convert.ToDateTime(obj_miningNewIssusedLetter.ValidityEndDate).AddMonths(-6) <= DateTime.Now && objMiningNewApplication.LatestApplicationStatusCode == Convert.ToInt32(NOCAP.BLL.Utility.Utility.LatestApplicationStatusCodeOption.Approved))
                            lnkInspection.Visible = true;
                        else
                            lnkInspection.Visible = true;
                        lnkInspection.Text = "Self Inspection (Not Filled)";
                    }

                }
                else
                {
                    lnkCompliance.Visible = true;
                    lnkInspection.Visible = false;
                }

                // end Self compliance


                if (objMiningNewApplication.RewAppCodeFinally != null)
                {
                    lnkMinRenewDetail.Text = "Detail";
                }

                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplicationEligibleForExemption = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(Convert.ToInt64(gvMinNewSubmitted.DataKeys[e.Row.RowIndex].Value));
                string EligibleForExemptionLett = Convert.ToString(obj_MiningNewApplicationEligibleForExemption.EligibleForExemptionLetter);
                LinkButton lnkBtn = new LinkButton();
                lnkBtn = (LinkButton)e.Row.FindControl("lbtnEdit");
                if (EligibleForExemptionLett == "Yes")
                {
                    lnkBtn.Enabled = false;
                    lnkBtn.Text = "";
                    lnkBtn.ForeColor = System.Drawing.Color.Black;
                    lnkBtn.Style.Add("text-decoration", "none");
                    //lnkBtn.CssClass = "NoUnderLine";
                }
                else if (EligibleForExemptionLett == "No")
                {
                    lnkBtn.Visible = true;
                }
                if (objTemp != null)
                {
                    if (objMiningNewIssusedLetter.AttPath != null && objMiningNewIssusedLetter.ScanAttPath != null && objMiningNewIssusedLetter.ScanAttPath != "" && objMiningNewIssusedLetter.AttPath != "")
                    {
                        lnkbtn.Visible = true;
                        lnkbtnScan.Visible = true;
                        int intIssueLetterTypeCode = objMiningNewIssusedLetter.IssuedLetterTypeCode;
                        NOCAP.BLL.Master.IssuedLettersType objIssuedLettersType = new NOCAP.BLL.Master.IssuedLettersType();
                        objIssuedLettersType.populateIssuedLetterTypeForIssuedLetterTypeCode(intIssueLetterTypeCode);
                        if (objIssuedLettersType != null)
                        {
                            lnkbtn.Text = HttpUtility.HtmlEncode(objIssuedLettersType.IssuedLetterTypeName);  //added lnkbtn.Text = objIssuedLettersType.IssuedLetterTypeName;
                        }

                    }
                    else if (objMiningNewIssusedLetter.AttPath != "" && objMiningNewIssusedLetter.AttPath != null)
                    {
                        lnkbtnScan.Visible = false;
                        lnkbtn.Visible = true;
                        int intIssueLetterTypeCode = objMiningNewIssusedLetter.IssuedLetterTypeCode;
                        NOCAP.BLL.Master.IssuedLettersType objIssuedLettersType = new NOCAP.BLL.Master.IssuedLettersType();
                        objIssuedLettersType.populateIssuedLetterTypeForIssuedLetterTypeCode(intIssueLetterTypeCode);
                        if (objIssuedLettersType != null)
                        {
                            lnkbtn.Text = HttpUtility.HtmlEncode(objIssuedLettersType.IssuedLetterTypeName);  //added html encode lnkbtn.Text = objIssuedLettersType.IssuedLetterTypeName;
                        }
                    }
                    else if (objMiningNewIssusedLetter.ScanAttPath != "" && objMiningNewIssusedLetter.ScanAttPath != null)
                    {
                        lnkbtn.Visible = false;
                        lnkbtnScan.Visible = true;

                    }
                    else
                    {
                        lnkbtn.Visible = false;
                        lnkbtnScan.Visible = false;
                    }
                }
                lblLatestAppStatusCode = (Label)e.Row.FindControl("lblLatestAppStatusCode3");
                obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(Convert.ToInt32(gvMinNewSubmitted.DataKeys[e.Row.RowIndex].Value.ToString()));
                if (!string.IsNullOrEmpty(obj_MiningNewApplication.GetApplicationStatus().ApplicationStatusDescription))
                {
                    lblLatestAppStatusCode.Text = "( " + HttpUtility.HtmlEncode(obj_MiningNewApplication.GetApplicationStatus().ApplicationStatusDescription) + " )";  //added html encode lblLatestAppStatusCode.Text = "( " + obj_MiningNewApplication.GetApplicationStatus().ApplicationStatusDescription + " )";
                    //Start For Presentation 
                    Label lblLatestPresentReq = (Label)e.Row.FindControl("lblLatestPresentReq3");
                    if (obj_MiningNewApplication.PresentationCalledSerialNumber != null)
                    {
                        NOCAP.BLL.Misc.Presentation.PresentationCalled obj_PresentationCalled = new NOCAP.BLL.Misc.Presentation.PresentationCalled(obj_MiningNewApplication.ApplicationCode, obj_MiningNewApplication.PresentationCalledSerialNumber);
                        if (obj_PresentationCalled.ApplicationCode != 0)
                        {
                            if (obj_PresentationCalled.PresentCalled == NOCAP.BLL.Common.CommonEnum.PresentationCalledOption.Yes)
                            {
                                if (obj_PresentationCalled.PresentationFinalize == NOCAP.BLL.Common.CommonEnum.PresentationFinalizeOption.No)
                                {
                                    lblLatestPresentReq.Text = "<br />Presentation<br />Required";
                                }
                            }
                        }
                    }
                    //End For Presentation 
                }
                if (obj_MiningNewApplication.SubmittedType == NOCAP.BLL.Mining.New.Application.MiningNewApplication.SubmittedTypeOption.Online)
                {
                    lblApplyType.Text = "Online";
                }
                else
                {
                    lblApplyType.Text = "Archival";
                    lnkBtn.Visible = false;
                }

            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }


    }

    protected void gvMINExpansionSaveAsDraft_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                long NewApplicationCode = Convert.ToInt64(gvMINNewSaveAsDraft.DataKeys[e.Row.RowIndex].Values["ApplicationCode"]);

                NOCAP.BLL.Mining.Expansion.MiningExpansionApplication obj_miningExpansionApplication = new NOCAP.BLL.Mining.Expansion.MiningExpansionApplication(NewApplicationCode);


                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_miningNewSADApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(NewApplicationCode);


                // LinkButton lnkPaymentStatus = (LinkButton)e.Row.FindControl("minlnkPaymentStatus");


                LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton lbtnSubmit = (LinkButton)e.Row.FindControl("lbtnMinSubmit");
                LinkButton lnkbtnMakePayment = (LinkButton)e.Row.FindControl("lnkbtnMakePayment");





                NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_ProFeeSADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_miningExpansionApplication.ApplicationCode, (obj_miningExpansionApplication.ProFeeOrderPaymentCode == null ? "0" : obj_miningExpansionApplication.ProFeeOrderPaymentCode.ToString()));
                NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_GWChargeSADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_miningExpansionApplication.ApplicationCode, (obj_miningExpansionApplication.GWChargeOrderPaymentCode == null ? "0" : obj_miningExpansionApplication.GWChargeOrderPaymentCode.ToString()));
                NOCAP.BLL.Misc.Payment.SADOnlinePayment objA_PenaltySADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_miningExpansionApplication.ApplicationCode, (obj_miningExpansionApplication.PenaltyOrderPaymentCode == null ? "0" : obj_miningExpansionApplication.PenaltyOrderPaymentCode.ToString()));
                lbtnEdit.Enabled = true;
                lnkbtnMakePayment.Enabled = true;
                NOCAP.BLL.Master.NTRPIntegration obj_nTRPIntegration = new NOCAP.BLL.Master.NTRPIntegration((int)NOCAP.BLL.Common.CommonEnum.ApplicationType.Mining);

                EnableDisableLinkButton(obj_nTRPIntegration.Active, NewApplicationCode, lbtnEdit, lnkbtnMakePayment,
                   lbtnSubmit, obj_miningExpansionApplication.ReadyToSubmit,
                   obj_miningExpansionApplication.PaymentTypeMode, obj_miningExpansionApplication.WaterQualityCode
                   , objA_ProFeeSADOnlinePayment, objA_GWChargeSADOnlinePayment, objA_PenaltySADOnlinePayment
                   , NOCAPExternalUtility.AreaTypeCategoryCode(null, null, obj_miningNewSADApplication)
                   );
                //if (obj_miningNewSADApplication.ReadyToSubmit == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                //{
                //    #region PaymentTypeMode Combined
                //    if (obj_miningNewSADApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined)
                //    {
                //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            lbtnEdit.Enabled = false;
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus != NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Failed)
                //                lnkbtnMakePayment.Enabled = false;
                //            else
                //                lnkbtnMakePayment.Enabled = true;
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                lbtnSubmit.Enabled = true;
                //            else
                //                lbtnSubmit.Enabled = false;
                //        }

                //    }
                //    #endregion

                //    #region PaymentTypeMode Single
                //    else if (obj_miningNewSADApplication.PaymentTypeMode == NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single)
                //    {

                //        #region Edit Button Enable/Disable
                //        if ((objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0) || (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0) || (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0))
                //            lbtnEdit.Enabled = false;
                //        #endregion

                //        #region MakePayment Button Enable/Disable
                //        string str_ProFee = "N", str_gwCharge = "N", str_Penalty = "N";
                //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_ProFee = "Y";
                //            else
                //                str_ProFee = "N";
                //        }
                //        if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_GWChargeSADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_gwCharge = "Y";
                //            else
                //                str_gwCharge = "N";
                //        }
                //        if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_PenaltySADOnlinePayment.FinalPaymentStatus == NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_Penalty = "Y";
                //            else
                //                str_Penalty = "N";
                //        }





                //        if (obj_miningNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry && obj_miningNewSADApplication.WaterQualityCode == 1)
                //        {

                //            if (str_ProFee == "Y" && str_gwCharge == "Y" && str_Penalty == "Y")
                //                lnkbtnMakePayment.Enabled = false;
                //            else
                //                lnkbtnMakePayment.Enabled = true;
                //        }
                //        else
                //        {
                //            if (obj_miningNewSADApplication.WaterQualityCode == 1)
                //            {
                //                if (str_ProFee == "Y" && str_gwCharge == "Y")
                //                    lnkbtnMakePayment.Enabled = false;
                //                else
                //                    lnkbtnMakePayment.Enabled = true;
                //            }
                //            else if (obj_miningNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry)
                //            {
                //                if (str_ProFee == "Y" && str_Penalty == "Y")
                //                    lnkbtnMakePayment.Enabled = false;
                //                else
                //                    lnkbtnMakePayment.Enabled = true;
                //            }
                //            else
                //            {
                //                if (str_ProFee == "Y")
                //                    lnkbtnMakePayment.Enabled = false;
                //                else
                //                    lnkbtnMakePayment.Enabled = true;
                //            }

                //        }


                //        #endregion

                //        #region Submit Button Enable/Disable
                //        string str_subProFee = "N", str_subgwCharge = "N", str_subPenalty = "N";
                //        if (objA_ProFeeSADOnlinePayment != null && objA_ProFeeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_ProFeeSADOnlinePayment.FinalPaymentStatus ==
                //               NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_subProFee = "Y";
                //            else
                //                str_subProFee = "N";
                //        }
                //        if (objA_GWChargeSADOnlinePayment != null && objA_GWChargeSADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_GWChargeSADOnlinePayment.FinalPaymentStatus ==
                //           NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_subgwCharge = "Y";
                //            else
                //                str_subgwCharge = "N";
                //        }
                //        if (objA_PenaltySADOnlinePayment != null && objA_PenaltySADOnlinePayment.CreatedByExUC > 0)
                //        {
                //            if (objA_PenaltySADOnlinePayment.FinalPaymentStatus ==
                //            NOCAP.BLL.Common.CommonEnum.FinalPaymentStatus.Success)
                //                str_subPenalty = "Y";
                //            else
                //                str_subPenalty = "N";
                //        }

                //        if (obj_miningNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry && obj_miningNewSADApplication.WaterQualityCode == 1)
                //        {
                //            if (str_subProFee == "Y" && str_subgwCharge == "Y" && str_subPenalty == "Y")
                //                lbtnSubmit.Enabled = true;
                //            else
                //                lbtnSubmit.Enabled = false;
                //        }
                //        else
                //        {

                //            if (obj_miningNewSADApplication.WaterQualityCode == 1)
                //            {
                //                if (str_subProFee == "Y" && str_subgwCharge == "Y")
                //                    lbtnSubmit.Enabled = true;
                //                else
                //                    lbtnSubmit.Enabled = false;
                //            }
                //            else if (obj_miningNewSADApplication.GroundWaterUtilizationFor == NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry)
                //            {
                //                if (str_subProFee == "Y" && str_subPenalty == "Y")
                //                    lbtnSubmit.Enabled = true;
                //                else
                //                    lbtnSubmit.Enabled = false;
                //            }
                //            else
                //            {
                //                if (str_subProFee == "Y")
                //                    lbtnSubmit.Enabled = true;
                //                else
                //                    lbtnSubmit.Enabled = false;
                //            }
                //        }


                //        #endregion

                //    }
                //    #endregion
                //    else
                //    { lbtnSubmit.Enabled = false; }
                //}
                //else
                //{ lbtnSubmit.Enabled = false; lnkbtnMakePayment.Enabled = false; }







            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    #endregion
    #endregion



    protected void lbtnMakePayment_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;

            if (e.CommandArgument != null)
            {
                lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);

                Server.Transfer("~/ExternalUser/PayAppSumbit.aspx");
            }
        }
    }
    private void SetCookie(string Key, string Value)
    {
        Response.Cookies[Key].Value = Value;
        Response.Cookies[Key].Path = ConfigurationManager.AppSettings["UserDefinedCookiePathFilter"];
    }
}