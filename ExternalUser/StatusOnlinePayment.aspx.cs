using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExternalUser_StatusOnlinePayment : System.Web.UI.Page
{
    string strPageName = "StatusOnlinePayment";
    string strActionName = "";
    string strStatus = "";
    NOCAP.BLL.Misc.Payment.SADOnlinePaymentHistory obj_OnlinePaymentHist = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentHistory();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            if (PreviousPage != null)
            {

                try
                {

                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication();
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication();
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication();
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication();
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication();
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication();

                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label lblOrderPaymentCode = (Label)placeHolder.FindControl("lblOrderPaymentCode");
                        Label lblApplicationCode = (Label)placeHolder.FindControl("lblApplicationCode");


                        if (lblOrderPaymentCode != null)
                        {

                            NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_InfrastructureNewApplication, out obj_MiningNewApplication, out obj_IndustrialRenewApplication, out obj_InfrastructureRenewApplication, out obj_MiningRenewApplication, Convert.ToInt64(lblApplicationCode.Text));

                            if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.CreatedByExUC > 0)
                            {
                                lblAppName.Text = obj_IndustrialNewApplication.NameOfIndustry;
                            }
                            else if (obj_IndustrialRenewApplication != null && obj_IndustrialRenewApplication.CreatedByExUC > 0)
                            {
                                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplicationl = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_IndustrialRenewApplication.FirstApplicationCode);
                                lblAppName.Text = obj_IndustrialNewApplicationl.NameOfIndustry;
                            }
                            else if (obj_InfrastructureNewApplication != null && obj_InfrastructureNewApplication.CreatedByExUC > 0)
                            {
                                lblAppName.Text = obj_InfrastructureNewApplication.NameOfInfrastructure;
                            }
                            else if (obj_InfrastructureRenewApplication != null && obj_InfrastructureRenewApplication.CreatedByExUC > 0)
                            {
                                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplicationl = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_InfrastructureRenewApplication.FirstApplicationCode);
                                lblAppName.Text = obj_InfrastructureNewApplicationl.NameOfInfrastructure;
                            }
                            else if (obj_MiningNewApplication != null && obj_MiningNewApplication.CreatedByExUC > 0)
                            {
                                lblAppName.Text = obj_MiningNewApplication.NameOfMining;
                            }
                            else if (obj_MiningRenewApplication != null && obj_MiningRenewApplication.CreatedByExUC > 0)
                            {
                                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplicationl = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_MiningRenewApplication.FirstApplicationCode);
                                lblAppName.Text = obj_MiningNewApplicationl.NameOfMining;
                            }
                            Label1.Text = lblApplicationCode.Text;
                            NOCAP.BLL.Misc.Payment.SADOnlinePaymentExt obj_OnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentExt();

                            obj_OnlinePayment.ApplicationCode = Convert.ToInt64(lblApplicationCode.Text);
                            obj_OnlinePayment.GetALL();
                            BindGridView(gvSADPayment, obj_OnlinePayment.SADOnlinePaymentExtCollection);

                            BindGridViewHist();
                            BindAppFeeReceived();
                            BindChargesReceivedPenalty();
                            BindChargesReceivedGWCharges();

                        }
                    }

                }
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
                }
            }
        }
    }

    private void BindGridView(GridView gv, NOCAP.BLL.Misc.Payment.SADOnlinePayment[] arrSAD_OnlinepPay)
    {
        gv.DataSource = arrSAD_OnlinepPay;
        gv.DataBind();
    }
    private void BindGridViewHist()
    {
        obj_OnlinePaymentHist.ApplicationCode = Convert.ToInt64(Label1.Text);
        obj_OnlinePaymentHist.GetALL();
        gvSADPaymentHist.DataSource = obj_OnlinePaymentHist.SADOnlinePaymentHistoryCollection;
        gvSADPaymentHist.DataBind();
    }

    protected void gvSADPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string OrderPaymentCode = (string)gvSADPayment.DataKeys[e.Row.RowIndex]["OrderPaymentCode"];
                GridView gvSADPaymentDetails = (GridView)e.Row.FindControl("gvSADPaymentDetails");
                NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetailsExt obj_SADOnlinePaymentDetailsExt = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetailsExt();

                obj_SADOnlinePaymentDetailsExt.OrderPaymentCode = OrderPaymentCode;
                obj_SADOnlinePaymentDetailsExt.GetALL();
                gvSADPaymentDetails.DataSource = obj_SADOnlinePaymentDetailsExt.SADOnlinePaymentDetailsExtCollection;
                gvSADPaymentDetails.DataBind();
            }
        }
        catch(Exception ex)
        {

        }
    }
    protected void gvSADPaymentHist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string OrderPaymentCode = (string)gvSADPayment.DataKeys[e.Row.RowIndex]["OrderPaymentCode"];
            GridView gvSADPaymentDetailsHist = (GridView)e.Row.FindControl("gvSADPaymentDetailsHist");
            NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetailsHistory obj_SADOnlinePaymentDetailsExt = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetailsHistory();

            obj_SADOnlinePaymentDetailsExt.OrderPaymentCode = OrderPaymentCode;
            obj_SADOnlinePaymentDetailsExt.GetALL();
            gvSADPaymentDetailsHist.DataSource = obj_SADOnlinePaymentDetailsExt.SADOnlinePaymentDetailsHistoryCollection;
            gvSADPaymentDetailsHist.DataBind();
        }
    }

    //protected void gvSADPayment_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{       
    //    gvSADPayment.EditIndex = -1;
    //    NOCAP.BLL.Misc.Payment.SADOnlinePaymentExt obj_OnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentExt();
    //    BindGridView(gvSADPayment , obj_OnlinePayment.SADOnlinePaymentExtCollection);
    //}
    protected void gvSADPayment_RowEditing(object sender, GridViewEditEventArgs e)
    {

        try {
            int int_FinalEditIndexNo = 0;
            int int_EditIndexNo = 0;
            string str_tempapplicationCode = null;
            string str_tempOrderPaymentCode = null;

            string str_applicationCode = Convert.ToString(gvSADPayment.DataKeys[e.NewEditIndex].Values["ApplicationCode"]);
            string str_OrderPaymentCode = Convert.ToString(gvSADPayment.DataKeys[e.NewEditIndex].Values["OrderPaymentCode"]);

            gvSADPayment.EditIndex = e.NewEditIndex;
            GridViewRow row = gvSADPayment.Rows[e.NewEditIndex];
            DataKey key = default(DataKey);

            if (row != null)
            {

                int_EditIndexNo = row.RowIndex;
                key = gvSADPayment.DataKeys[int_EditIndexNo];
                str_tempapplicationCode = Convert.ToString(key.Values["ApplicationCode"]);
                str_tempOrderPaymentCode = Convert.ToString(key.Values["OrderPaymentCode"]);

                if (str_tempapplicationCode == str_applicationCode && str_tempOrderPaymentCode == str_OrderPaymentCode)
                {
                    int_FinalEditIndexNo = row.RowIndex;

                }
                NOCAP.BLL.Misc.Payment.SADOnlinePaymentExt obj_OnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentExt();
                obj_OnlinePayment.ApplicationCode = Convert.ToInt64(str_applicationCode);
                obj_OnlinePayment.GetALL();
                BindGridView(gvSADPayment, obj_OnlinePayment.SADOnlinePaymentExtCollection);
                gvSADPayment.EditIndex = int_FinalEditIndexNo;
                gvSADPayment.DataBind();
            }
        }
        catch(Exception ex)
        {

        }
    }

    protected void gvSADPayment_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                int index = gvSADPayment.EditIndex;
                GridViewRow row = gvSADPayment.Rows[index];
                TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                long lng_ApplicationCode = Convert.ToInt64(gvSADPayment.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                string str_OrderPaymentCode = (string)gvSADPayment.DataKeys[e.RowIndex].Values["OrderPaymentCode"];

                NOCAP.BLL.Misc.Payment.SADOnlinePaymentExt obj_SADOnlinePaymentDetailsExt = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentExt();
                obj_SADOnlinePaymentDetailsExt.ApplicationCode = lng_ApplicationCode;
                obj_SADOnlinePaymentDetailsExt.OrderPaymentCode = str_OrderPaymentCode;
                if (txtRemark.Text != null)
                {
                    obj_SADOnlinePaymentDetailsExt.Remarks = Convert.ToString(txtRemark.Text);
                }
                obj_SADOnlinePaymentDetailsExt.UpdatedByExUC = Convert.ToInt32(Session["ExternalUserCode"]);


                obj_SADOnlinePaymentDetailsExt.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.TERMINATE;
                if (chkBoxUndertaking.Checked)
                    obj_SADOnlinePaymentDetailsExt.UndertakingAgreement = NOCAP.BLL.Misc.Payment.SADOnlinePayment.UndertakingAgreementOption.Yes;
                else
                    obj_SADOnlinePaymentDetailsExt.UndertakingAgreement = NOCAP.BLL.Misc.Payment.SADOnlinePayment.UndertakingAgreementOption.No;

                if (obj_SADOnlinePaymentDetailsExt.SetSADOnlinePaymentTerminate() == 1)
                {
                    lblMessage.Text = HttpUtility.HtmlEncode(obj_SADOnlinePaymentDetailsExt.CustumMessage);
                    lblMessage.ForeColor = System.Drawing.Color.Blue;
                    gvSADPayment.EditIndex = -1;
                    NOCAP.BLL.Misc.Payment.SADOnlinePaymentExt obj_OnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentExt();
                    obj_OnlinePayment.ApplicationCode = Convert.ToInt64(lng_ApplicationCode);
                    obj_OnlinePayment.GetALL();
                    BindGridView(gvSADPayment, obj_OnlinePayment.SADOnlinePaymentExtCollection);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Record Update Failed');", true);
                }
                BindGridViewHist();
            }
        }
        catch (Exception ex)
        { }
        finally
        {
            ActionTrail obj_ActionTrail = new ActionTrail();
            if (Session["ExternalUserCode"] != null)
            {
                obj_ActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
                obj_ActionTrail.IP_Address = Request.UserHostAddress;
                obj_ActionTrail.ActionPerformed = strPageName + "-" + strActionName;
                obj_ActionTrail.Status = strStatus;
                if (obj_ActionTrail != null)
                    ActionTrailDAL.ExtActionSave(obj_ActionTrail);
            }
        }
    }
    protected void gvSADPayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSADPayment.EditIndex = -1;
        gvSADPayment.PageIndex = e.NewPageIndex;

    }



    ////////////////////  SACHIN      //////////////////////


    private void BindAppFeeReceived(string str_sortfieldName = "")
    {
        try
        {
            NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus obj_GroundWaterChargeRecBLL = new NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus();
            int int_status = 0;
            obj_GroundWaterChargeRecBLL.ApplicationCode = Convert.ToInt64(Label1.Text);
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
                lblMessage.Text = HttpUtility.HtmlEncode(obj_GroundWaterChargeRecBLL.CustumMessage);
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
    protected void gvApplicationFee_Sorting(object sender, GridViewSortEventArgs e)
    {

        NOCAP.BLL.Master.State obj_State = new NOCAP.BLL.Master.State();
        lblSortFieldPenalty.Text = HttpUtility.HtmlEncode(e.SortExpression);
        gvApplicationFee.EditIndex = -1;

    }
    protected void ViewFile(object sender, CommandEventArgs e)
    {

        if (e.CommandArgument != null)
        {
            LinkButton btn = (LinkButton)sender;
            string[] CommandArgument = btn.CommandArgument.Split(',');
            long lng_ApplicationCode = Convert.ToInt32(CommandArgument[0]);
            int int_SN = Convert.ToInt32(CommandArgument[1]);
            NOCAPExternalUtility.SADAppFeeDownloadFiles(lng_ApplicationCode, int_SN);

        }
    }
    private void BindChargesReceivedPenalty(string str_sortfieldName = "")
    {
        try
        {
            NOCAP.BLL.PenaltyCorrectionCharges.SADPenaltyCorrectionChargesRec obj_PenaltyCorrectChargesRecBLL = new NOCAP.BLL.PenaltyCorrectionCharges.SADPenaltyCorrectionChargesRec();

            int int_status = 0;
            obj_PenaltyCorrectChargesRecBLL.ApplicationCode = Convert.ToInt64(Label1.Text);
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
    protected void gvPenalty_Sorting(object sender, GridViewSortEventArgs e)
    {

        NOCAP.BLL.Master.State obj_State = new NOCAP.BLL.Master.State();
        lblSortFieldPenalty.Text = HttpUtility.HtmlEncode(e.SortExpression);
        gvPenalty.EditIndex = -1;

        BindChargesReceivedPenalty(lblSortFieldPenalty.Text);

    }
    protected void DownloadOrViewFilePenaltyCorrection(object sender, CommandEventArgs e)
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
    private void BindChargesReceivedGWCharges(string str_sortfieldName = "")
    {
        try
        {
            NOCAP.BLL.GroundWaterChargeRec.SADGroundWaterChargesRec obj_GroundWaterChargeRecBLL = new NOCAP.BLL.GroundWaterChargeRec.SADGroundWaterChargesRec();
            int int_status = 0;
            obj_GroundWaterChargeRecBLL.ApplicationCode = Convert.ToInt64(Label1.Text);
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
                lblMessage.Text = HttpUtility.HtmlEncode(obj_GroundWaterChargeRecBLL.CustumMessage);
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    protected void DownloadOrViewFileGWCharges(object sender, CommandEventArgs e)
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
    protected void gvGWCharges_Sorting(object sender, GridViewSortEventArgs e)
    {

        NOCAP.BLL.Master.State obj_State = new NOCAP.BLL.Master.State();
        lblSortFieldPenalty.Text = HttpUtility.HtmlEncode(e.SortExpression);
        gvGWCharges.EditIndex = -1;

        BindChargesReceivedGWCharges(lblSortFieldGWCharges.Text);
    }

    /////////////////////////   



    protected void imgbtnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        #region SAD Online Payment
        NOCAP.BLL.Misc.Payment.SADOnlinePayment obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment();
        ImageButton btn = (ImageButton)sender;
        string[] CommandArgument = btn.CommandArgument.Split(',');

        string str_OrderPaymentCodeForNTRP = CommandArgument[0];
        obj_SADOnlinePayment.OrderPaymentCodeForNTRP = str_OrderPaymentCodeForNTRP;
        NOCAP.BLL.Common.CommonEnum.PaymentStatus enu_PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.NotDefined;
        NOCAP.BLL.Common.CommonEnum.ReceiptStatus enu_ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.NotDefined;
        try
        {
            string[] arr = NOCAPExternalUtility.TransactionStatusMain(obj_SADOnlinePayment.OrderPaymentCodeForNTRP.Trim(), ref enu_PaymentStatus,ref enu_ReceiptStatus);          
            if (arr != null && arr.Length > 0)
            {
                obj_SADOnlinePayment.PaymentStatus = enu_PaymentStatus;
                obj_SADOnlinePayment.ReceiptStatus = enu_ReceiptStatus;
                obj_SADOnlinePayment.StatusXX = arr[1].ToUpper();
                obj_SADOnlinePayment.TransactionRefNo = arr[2];
                obj_SADOnlinePayment.SetOnlinePaymentStatusByWinService();



                NOCAP.BLL.Misc.Payment.SADOnlinePaymentExt obj_OnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentExt();

                obj_OnlinePayment.ApplicationCode = Convert.ToInt64(Label1.Text.Trim());
                obj_OnlinePayment.GetALL();
                BindGridView(gvSADPayment, obj_OnlinePayment.SADOnlinePaymentExtCollection);
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            ActionTrail obj_ActionTrail = new ActionTrail();
            if (Session["ExternalUserCode"] != null)
            {
                obj_ActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
                obj_ActionTrail.IP_Address = Request.UserHostAddress;
                obj_ActionTrail.ActionPerformed = strPageName + "-" + strActionName;
                obj_ActionTrail.Status = strStatus;
                if (obj_ActionTrail != null)
                    ActionTrailDAL.ExtActionSave(obj_ActionTrail);
            }
        }

        #endregion
    }
   
}