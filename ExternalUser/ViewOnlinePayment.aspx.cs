using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class ExternalUser_ViewOnlinePayment : System.Web.UI.Page
{
    string strPageName = "StatusOnlinePayment";
    string strActionName = "";
    string strStatus = "";
    NOCAP.BLL.Misc.Payment.OnlinePaymentHistory obj_OnlinePaymentHist = new NOCAP.BLL.Misc.Payment.OnlinePaymentHistory();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            if (PreviousPage != null)
            {

                try
                {

                    NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
                    NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication();
                    NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
                    NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication();
                    NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication();
                    NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication();

                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {

                        Label lblApplicationCode = (Label)placeHolder.FindControl("lblAppCode");
                        NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_InfrastructureNewApplication,
                            out obj_MiningNewApplication, out obj_IndustrialRenewApplication,
                            out obj_InfrastructureRenewApplication, out obj_MiningRenewApplication, Convert.ToInt64(lblApplicationCode.Text));

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
                        NOCAP.BLL.Misc.Payment.OnlinePaymentExt obj_OnlinePayment = new NOCAP.BLL.Misc.Payment.OnlinePaymentExt();

                        obj_OnlinePayment.ApplicationCode = Convert.ToInt64(lblApplicationCode.Text);
                        obj_OnlinePayment.GetALL();
                        BindGridView(gvPayment, obj_OnlinePayment.OnlinePaymentExtCollection);

                        BindGridViewHist();
                        //BindAppFeeReceived();
                        //BindChargesReceivedPenalty();
                        //BindChargesReceivedGWCharges();


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

    private void BindGridView(GridView gv, NOCAP.BLL.Misc.Payment.OnlinePayment[] arrSAD_OnlinepPay)
    {
        gv.DataSource = arrSAD_OnlinepPay;
        gv.DataBind();
    }
    private void BindGridViewHist()
    {
        obj_OnlinePaymentHist.ApplicationCode = Convert.ToInt64(Label1.Text);
        obj_OnlinePaymentHist.GetALL();
        gvPaymentHist.DataSource = obj_OnlinePaymentHist.OnlinePaymentHistoryCollection;
        gvPaymentHist.DataBind();
    }

    protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string OrderPaymentCode = (string)gvPayment.DataKeys[e.Row.RowIndex]["OrderPaymentCode"];
                GridView gvPaymentDetails = (GridView)e.Row.FindControl("gvPaymentDetails");
                NOCAP.BLL.Misc.Payment.OnlinePaymentDetailsExt obj_OnlinePaymentDetailsExt = new NOCAP.BLL.Misc.Payment.OnlinePaymentDetailsExt();

                obj_OnlinePaymentDetailsExt.OrderPaymentCode = OrderPaymentCode;
                obj_OnlinePaymentDetailsExt.GetALL();
                gvPaymentDetails.DataSource = obj_OnlinePaymentDetailsExt.OnlinePaymentDetailsExtCollection;
                gvPaymentDetails.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void gvPaymentHist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string OrderPaymentCode = (string)gvPayment.DataKeys[e.Row.RowIndex]["OrderPaymentCode"];
            GridView gvPaymentDetailsHist = (GridView)e.Row.FindControl("gvPaymentDetailsHist");
            NOCAP.BLL.Misc.Payment.OnlinePaymentDetailsHistory obj_OnlinePaymentDetailsExt = new NOCAP.BLL.Misc.Payment.OnlinePaymentDetailsHistory();

            obj_OnlinePaymentDetailsExt.OrderPaymentCode = OrderPaymentCode;
            obj_OnlinePaymentDetailsExt.GetALL();
            gvPaymentDetailsHist.DataSource = obj_OnlinePaymentDetailsExt.OnlinePaymentDetailsHistoryCollection;
            gvPaymentDetailsHist.DataBind();
        }
    }

    //protected void gvPayment_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{       
    //    gvPayment.EditIndex = -1;
    //    NOCAP.BLL.Misc.Payment.OnlinePaymentExt obj_OnlinePayment = new NOCAP.BLL.Misc.Payment.OnlinePaymentExt();
    //    BindGridView(gvPayment , obj_OnlinePayment.OnlinePaymentExtCollection);
    //}
    protected void gvPayment_RowEditing(object sender, GridViewEditEventArgs e)
    {

        try
        {
            int int_FinalEditIndexNo = 0;
            int int_EditIndexNo = 0;
            string str_tempapplicationCode = null;
            string str_tempOrderPaymentCode = null;

            string str_applicationCode = Convert.ToString(gvPayment.DataKeys[e.NewEditIndex].Values["ApplicationCode"]);
            string str_OrderPaymentCode = Convert.ToString(gvPayment.DataKeys[e.NewEditIndex].Values["OrderPaymentCode"]);

            gvPayment.EditIndex = e.NewEditIndex;
            GridViewRow row = gvPayment.Rows[e.NewEditIndex];
            DataKey key = default(DataKey);

            if (row != null)
            {

                int_EditIndexNo = row.RowIndex;
                key = gvPayment.DataKeys[int_EditIndexNo];
                str_tempapplicationCode = Convert.ToString(key.Values["ApplicationCode"]);
                str_tempOrderPaymentCode = Convert.ToString(key.Values["OrderPaymentCode"]);

                if (str_tempapplicationCode == str_applicationCode && str_tempOrderPaymentCode == str_OrderPaymentCode)
                {
                    int_FinalEditIndexNo = row.RowIndex;

                }
                NOCAP.BLL.Misc.Payment.OnlinePaymentExt obj_OnlinePayment = new NOCAP.BLL.Misc.Payment.OnlinePaymentExt();
                obj_OnlinePayment.ApplicationCode = Convert.ToInt64(str_applicationCode);
                obj_OnlinePayment.GetALL();
                BindGridView(gvPayment, obj_OnlinePayment.OnlinePaymentExtCollection);
                gvPayment.EditIndex = int_FinalEditIndexNo;
                gvPayment.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void gvPayment_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                int index = gvPayment.EditIndex;
                GridViewRow row = gvPayment.Rows[index];
                TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                long lng_ApplicationCode = Convert.ToInt64(gvPayment.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                string str_OrderPaymentCode = (string)gvPayment.DataKeys[e.RowIndex].Values["OrderPaymentCode"];

                NOCAP.BLL.Misc.Payment.OnlinePaymentExt obj_OnlinePaymentDetailsExt = new NOCAP.BLL.Misc.Payment.OnlinePaymentExt();
                obj_OnlinePaymentDetailsExt.ApplicationCode = lng_ApplicationCode;
                obj_OnlinePaymentDetailsExt.OrderPaymentCode = str_OrderPaymentCode;
                if (txtRemark.Text != null)
                {
                    obj_OnlinePaymentDetailsExt.Remarks = Convert.ToString(txtRemark.Text);
                }
                obj_OnlinePaymentDetailsExt.UpdatedByExUC = Convert.ToInt32(Session["ExternalUserCode"]);


                obj_OnlinePaymentDetailsExt.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.TERMINATE;
                if (chkBoxUndertaking.Checked)
                    obj_OnlinePaymentDetailsExt.UndertakingAgreement = NOCAP.BLL.Misc.Payment.OnlinePayment.UndertakingAgreementOption.Yes;
                else
                    obj_OnlinePaymentDetailsExt.UndertakingAgreement = NOCAP.BLL.Misc.Payment.OnlinePayment.UndertakingAgreementOption.No;

                if (obj_OnlinePaymentDetailsExt.SetOnlinePaymentTerminate() == 1)
                {
                    lblMessage.Text = HttpUtility.HtmlEncode(obj_OnlinePaymentDetailsExt.CustumMessage);
                    lblMessage.ForeColor = System.Drawing.Color.Blue;
                    gvPayment.EditIndex = -1;
                    NOCAP.BLL.Misc.Payment.OnlinePaymentExt obj_OnlinePayment = new NOCAP.BLL.Misc.Payment.OnlinePaymentExt();
                    obj_OnlinePayment.ApplicationCode = Convert.ToInt64(lng_ApplicationCode);
                    obj_OnlinePayment.GetALL();
                    BindGridView(gvPayment, obj_OnlinePayment.OnlinePaymentExtCollection);

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
    protected void gvPayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPayment.EditIndex = -1;
        gvPayment.PageIndex = e.NewPageIndex;

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
        #region  Online Payment
        NOCAP.BLL.Misc.Payment.OnlinePayment obj_OnlinePayment = new NOCAP.BLL.Misc.Payment.OnlinePayment();
        ImageButton btn = (ImageButton)sender;
        string[] CommandArgument = btn.CommandArgument.Split(',');

        string str_OrderPaymentCodeForNTRP = CommandArgument[0];
        obj_OnlinePayment.OrderPaymentCodeForNTRP = str_OrderPaymentCodeForNTRP;
        NOCAP.BLL.Common.CommonEnum.PaymentStatus enu_PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.NotDefined;
        NOCAP.BLL.Common.CommonEnum.ReceiptStatus enu_ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.NotDefined;
        try
        {
            string[] arr = NOCAPExternalUtility.TransactionStatusMain(obj_OnlinePayment.OrderPaymentCodeForNTRP.Trim(), ref enu_PaymentStatus, ref enu_ReceiptStatus);
            if (arr != null && arr.Length > 0)
            {
                obj_OnlinePayment.PaymentStatus = enu_PaymentStatus;
                obj_OnlinePayment.ReceiptStatus = enu_ReceiptStatus;

                //switch (arr[1].ToUpper())
                //{

                //    case "SUCCESS":
                //        obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.SUCCESS;
                //        break;
                //    case "FAIL":
                //        obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.FAIL;
                //        break;
                //    case "STATUS UNKNOWN/ABORT":
                //        obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.StatusUnknownAbort;
                //        break;
                //    case "NO RECORD FOUND":
                //        obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.NoRecordsFound;
                //        break;
                //    case "BOOKED":
                //        obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.BOOKED;
                //        break;
                //    case "PENDING":
                //        obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.PENDING;
                //        break;
                //    case "EXPIRED":
                //        obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.EXPIRED;
                //        break;
                //    case "FAILREF":
                //        obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.FailedOrRefundIfReceived;
                //        break;
                //    case "REFSET":
                //        obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.RefundSettled;
                //        break;
                //    case "OFFDSCRD":
                //        obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.PENDING;
                //        break;
                //    case "TERMINATE":
                //        obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.TERMINATE;
                //        break;
                //    case "CREATED":
                //        obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.Created;
                //        break;
                //    case "QUITED":
                //        obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.Quited;
                //        break;
                //    case "DELETED":
                //        obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.Deleted;
                //        break;
                //    case "REINITIATION":
                //        obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.ReInitiation;
                //        break;
                //    case "HOLD":
                //        obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.HOLD;
                //        break;

                //    case "PAYMENT INITIATED":
                //        obj_OnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.PaymentInitiated;
                //        break;
                //    case "CONFIRMED":
                //        obj_OnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.Confirmed;
                //        break;
                //    case "INCOMPLETE":
                //        obj_OnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.Incomplete;
                //        break;
                //    case "SUBMITTED":
                //        obj_OnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.Submitted;
                //        break;
                //    default:
                //        obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.NotDefined;
                //        break;
                //}
                obj_OnlinePayment.StatusXX = arr[1].ToUpper();
                obj_OnlinePayment.TransactionRefNo = arr[2];

                obj_OnlinePayment.SetOnlinePaymentStatusByWinService();
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