using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

public partial class Sub_Report_ExDashboard_ExternalUserDashboard : System.Web.UI.Page
{


    public StringBuilder ss = new StringBuilder();

    //Amardeep
    public StringBuilder Str_RegionWiseNOCIssuedExIND = new StringBuilder();
    public StringBuilder Str_RegionWiseNOCIssuedExINF = new StringBuilder();
    public StringBuilder Str_RegionWiseNOCIssuedExMIN = new StringBuilder();

    public StringBuilder Str_RegionWiseNOCIssuedExINDRen = new StringBuilder();
    public StringBuilder Str_RegionWiseNOCIssuedExINFRen = new StringBuilder();
    public StringBuilder Str_RegionWiseNOCIssuedExMINRen = new StringBuilder();
    //Amardeep


    public string Str_StateWiseAppCountINDColor = "rgb(255, 144, 80)";
    public string Str_StateWiseAppCountINFColor = "rgb(9, 78, 127)";
    public string Str_StateWiseAppCountMINColor = "rgb(9, 78, 0)";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            if (NOCAPExternalUtility.FillDropDownState(ref ddlState) != 1)
            {
                Response.Write("Problem in state population");
            }
            NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlOffice);


            if (NOCAPExternalUtility.FillDropDownApplicationType(ref ddlApplicationType) != 1)
            {
                Response.Write("Problem in Application Type population");
                return;
            }
            NOCAPExternalUtility.HideEntriesInDropDown(ref ddlApplicationType, "1");

            if (NOCAPExternalUtility.FillDropDownApplicationPurpose(ref ddlApplicationPurpose) != 1)
            {
                Response.Write("Problem in Application Purpose");
            }
            if (NOCAPExternalUtility.FillDropDownApplicationStatus(ref ddlApplicationStatus) != 1)
            {
                Response.Write("Problem in Application Status");
            }
            if ((NOCAPExternalUtility.FillDropDownAreaTypeCategory(ref ddlAreatypeCatDesc, 2)) == 1)
                ddlAreatypeCatDesc.Enabled = true;
            else
                Response.Redirect("~/InternalErrorPage.aspx", false);

            if (NOCAPExternalUtility.FillDropDownRegionalOffice(ref ddlOffice) != 1)
            {
                Response.Write("Problem in Regional office population");
            }
        }
        lblApproved.Text = "";
        lblApprovedRen.Text = "";
        lblCancelled.Text = "";
        lblCancelledRen.Text = "";
        lblExempted.Text = "";
        lblExemptedRen.Text = "";
        lblInprocess.Text = "";
        lblInprocessRen.Text = "";
        lblReferback.Text = "";
        lblReferbackRen.Text = "";
        lblRejected.Text = "";
        lblRejectedRen.Text = "";
        lblSubmitted.Text = "";
        lblSubmittedRen.Text = "";
        lblTotalNoofapplicationreceived.Text = "";
        lblTotalNoofapplicationreceivedRen.Text = "";
        //GetDataBindAppraisalStatus();

    }

    private void GetDataBindAppraisalStatus()
    {
        GetExternalUserDashboard();
    }


    #region  ExternalUserDashboard
    private void GetExternalUserDashboard()
    {
        int AppType = 0, AreatypeCat = 0, State = 0, AppStatus = 0, Office = 0, AppPurpose = 0;
        DateTime? dt_datefrom = null, dt_dateto = null;
        NOCAP.BLL.DashboardEx.ExternalUserDashBoard obj_ExternalUserDashBoard = new NOCAP.BLL.DashboardEx.ExternalUserDashBoard();
        List<List<NOCAP.BLL.DashboardEx.ExternalUserDashBoard>> list_list_ExternalUserDashBoard = null;
        List<NOCAP.BLL.DashboardEx.ExternalUserDashBoard> list_ExternalUserDashBoard = null;
        List<NOCAP.BLL.DashboardEx.ExternalUserDashBoard> list_ExternalUserDashBoardRen = null;//Renew
        List<NOCAP.BLL.DashboardEx.ExternalUserDashBoard> list_RegionWiseNOCIssuedExNew = null;//New
        List<NOCAP.BLL.DashboardEx.ExternalUserDashBoard> list_ExternalUserDashBoardBtnCount = null;
        NOCAP.BLL.DashboardEx.ExternalUserDashBoard ExternalUserDashBoardBtnCount = null;
        List<NOCAP.BLL.DashboardEx.ExternalUserDashBoard> list_ExternalUserDashBoardBtnCountRen = null;
        NOCAP.BLL.DashboardEx.ExternalUserDashBoard ExternalUserDashBoardBtnCountRen = null;

        try
        {


            if (ddlApplicationType.SelectedIndex > 0)
                AppType = Convert.ToInt32(ddlApplicationType.SelectedValue.ToString());
            if (ddlAreatypeCatDesc.SelectedIndex > 0)
                AreatypeCat = Convert.ToInt32(ddlAreatypeCatDesc.SelectedValue.ToString());
            if (ddlState.SelectedIndex > 0)
                State = Convert.ToInt32(ddlState.SelectedValue.ToString());
            if (ddlApplicationStatus.SelectedIndex > 0)
                AppStatus = Convert.ToInt32(ddlApplicationStatus.SelectedValue.ToString());
            if (ddlOffice.SelectedIndex > 0)
                Office = Convert.ToInt32(ddlOffice.SelectedValue.ToString());
            if (ddlApplicationPurpose.SelectedIndex > 0)
                AppPurpose = Convert.ToInt32(ddlApplicationPurpose.SelectedValue.ToString());

            decimal? QuantityFrom = !string.IsNullOrEmpty(txtQuantityFrom.Text) ? decimal.Parse(txtQuantityFrom.Text.Replace(",", "")) : (decimal?)null;

            decimal? QuantityTo = !string.IsNullOrEmpty(txtQuantityTo.Text) ? decimal.Parse(txtQuantityTo.Text.Replace(",", "")) : (decimal?)null;
            if (txtFromDate.Text != "")
                dt_datefrom = Convert.ToDateTime(txtFromDate.Text);
            if (txtToDate.Text != "")
                dt_dateto = Convert.ToDateTime(txtToDate.Text);
            list_list_ExternalUserDashBoard = obj_ExternalUserDashBoard.GetAll(
                AppType != 0 ? AppType : 0, AreatypeCat != 0 ? AreatypeCat : 0, State != 0 ? State : 0,
                AppStatus != 0 ? AppStatus : 0, Office != 0 ? Office : 0, dt_datefrom, dt_dateto, QuantityFrom,
                QuantityTo, AppPurpose != 0 ? AppPurpose : 0);

            if (list_list_ExternalUserDashBoard != null)
            {


                //  list_ExternalUserDashBoardBtnCount = list_list_ExternalUserDashBoard.ElementAt(0);
                ExternalUserDashBoardBtnCount = obj_ExternalUserDashBoard.ExternalUserDashBoardNew[0];// list_ExternalUserDashBoardBtnCount.ElementAt(0);
                //New
                lblTotalNoofapplicationreceived.Text = (ExternalUserDashBoardBtnCount.FreshApproved +
                    ExternalUserDashBoardBtnCount.FreshInProcess + ExternalUserDashBoardBtnCount.FreshRejected +
                    ExternalUserDashBoardBtnCount.FreshReferback + ExternalUserDashBoardBtnCount.FreshCancelled +
                    ExternalUserDashBoardBtnCount.FreshExempted + ExternalUserDashBoardBtnCount.FreshSubmitted).ToString();

                lblApproved.Text = ExternalUserDashBoardBtnCount.FreshApproved.ToString();
                lblInprocess.Text = ExternalUserDashBoardBtnCount.FreshInProcess.ToString();
                lblRejected.Text = ExternalUserDashBoardBtnCount.FreshRejected.ToString();
                lblReferback.Text = ExternalUserDashBoardBtnCount.FreshReferback.ToString();

                lblCancelled.Text = ExternalUserDashBoardBtnCount.FreshCancelled.ToString();
                lblExempted.Text = ExternalUserDashBoardBtnCount.FreshExempted.ToString();
                lblSubmitted.Text = ExternalUserDashBoardBtnCount.FreshSubmitted.ToString();




                //Renew
                // list_ExternalUserDashBoardBtnCountRen = list_list_ExternalUserDashBoard.ElementAt(1);
                ExternalUserDashBoardBtnCountRen = obj_ExternalUserDashBoard.ExternalUserDashBoardRenew[0];// list_ExternalUserDashBoardBtnCountRen.ElementAt(0);
                lblTotalNoofapplicationreceivedRen.Text = (ExternalUserDashBoardBtnCountRen.FreshApproved + ExternalUserDashBoardBtnCountRen.FreshInProcess +
                    ExternalUserDashBoardBtnCountRen.FreshRejected +
                    ExternalUserDashBoardBtnCountRen.FreshReferback + ExternalUserDashBoardBtnCountRen.FreshCancelled
                    + ExternalUserDashBoardBtnCountRen.FreshExempted + ExternalUserDashBoardBtnCountRen.FreshSubmitted).ToString();

                lblApprovedRen.Text = ExternalUserDashBoardBtnCountRen.FreshApproved.ToString();
                lblInprocessRen.Text = ExternalUserDashBoardBtnCountRen.FreshInProcess.ToString();
                lblRejectedRen.Text = ExternalUserDashBoardBtnCountRen.FreshRejected.ToString();
                lblReferbackRen.Text = ExternalUserDashBoardBtnCountRen.FreshReferback.ToString();

                lblCancelledRen.Text = ExternalUserDashBoardBtnCountRen.FreshCancelled.ToString();
                lblExemptedRen.Text = ExternalUserDashBoardBtnCountRen.FreshExempted.ToString();
                lblSubmittedRen.Text = ExternalUserDashBoardBtnCountRen.FreshSubmitted.ToString();

                // list_ExternalUserDashBoard.RemoveAt(0);

                //list_RegionWiseNOCIssuedEx.RemoveAt(1);


                //New Graph
                list_RegionWiseNOCIssuedExNew = obj_ExternalUserDashBoard.ExternalUserDashBoardNOCCount.ToList(); // list_list_ExternalUserDashBoard.ElementAt(2);//.Where(c => c.AppPurposeCode == 1).ToList();
                Str_RegionWiseNOCIssuedExIND.Append(new JavaScriptSerializer().Serialize(list_RegionWiseNOCIssuedExNew.Where(c => c.AppTypeCode == Convert.ToUInt32(NOCAP.BLL.Utility.Utility.ApplicationTypeOption.Industrial)).ToList()));
                if (Str_RegionWiseNOCIssuedExIND.ToString() == "")
                    Str_RegionWiseNOCIssuedExIND.Append("''");
                Str_RegionWiseNOCIssuedExINF.Append(new JavaScriptSerializer().Serialize(list_RegionWiseNOCIssuedExNew.Where(c => c.AppTypeCode == Convert.ToUInt32(NOCAP.BLL.Utility.Utility.ApplicationTypeOption.Infrastructure)).ToList()));
                if (Str_RegionWiseNOCIssuedExINF.ToString() == "")
                    Str_RegionWiseNOCIssuedExINF.Append("''");
                Str_RegionWiseNOCIssuedExMIN.Append(new JavaScriptSerializer().Serialize(list_RegionWiseNOCIssuedExNew.Where(c => c.AppTypeCode == Convert.ToUInt32(NOCAP.BLL.Utility.Utility.ApplicationTypeOption.Mining)).ToList()));
                if (Str_RegionWiseNOCIssuedExMIN.ToString() == "")
                    Str_RegionWiseNOCIssuedExMIN.Append("''");

                // list_ExternalUserDashBoard.RemoveAt(1);




                list_ExternalUserDashBoardRen = obj_ExternalUserDashBoard.ExternalUserDashBoardAppCount.ToList();// list_list_ExternalUserDashBoard.ElementAt(3);//.Where(c => c.AppPurposeCode == 2).ToList();

                Str_RegionWiseNOCIssuedExINDRen.Append(new JavaScriptSerializer().Serialize(list_ExternalUserDashBoardRen.Where(c => c.AppTypeCode == Convert.ToUInt32(NOCAP.BLL.Utility.Utility.ApplicationTypeOption.Industrial)).ToList()));
                if (Str_RegionWiseNOCIssuedExINDRen.ToString() == "")
                    Str_RegionWiseNOCIssuedExINDRen.Append("''");
                Str_RegionWiseNOCIssuedExINFRen.Append(new JavaScriptSerializer().Serialize(list_ExternalUserDashBoardRen.Where(c => c.AppTypeCode == Convert.ToUInt32(NOCAP.BLL.Utility.Utility.ApplicationTypeOption.Infrastructure)).ToList()));
                if (Str_RegionWiseNOCIssuedExINFRen.ToString() == "")
                    Str_RegionWiseNOCIssuedExINFRen.Append("''");
                Str_RegionWiseNOCIssuedExMINRen.Append(new JavaScriptSerializer().Serialize(list_ExternalUserDashBoardRen.Where(c => c.AppTypeCode == Convert.ToUInt32(NOCAP.BLL.Utility.Utility.ApplicationTypeOption.Mining)).ToList()));
                if (Str_RegionWiseNOCIssuedExMINRen.ToString() == "")
                    Str_RegionWiseNOCIssuedExMINRen.Append("''");
            }

        }
        catch (Exception ex)
        {
            string Msg = ex.Message;
        }
    }
    #endregion


    protected void btnShowChart_Click(object sender, EventArgs e)
    {
        ViewState["Sorting"] = null;
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
                GetDataBindAppraisalStatus();
            }
            catch (Exception ex)
            {
            }
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddlApplicationType.SelectedIndex = 0;
        ddlAreatypeCatDesc.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        ddlApplicationStatus.SelectedIndex = 0;
        ddlOffice.SelectedIndex = 0;
        ddlApplicationPurpose.SelectedIndex = 0;
        //txtFromDate.Text = "";
        // txtToDate.Text = "";
        txtQuantityFrom.Text = "";
        txtQuantityTo.Text = "";
        lblApproved.Text = "";
        lblApprovedRen.Text = "";
        lblCancelled.Text = "";
        lblCancelledRen.Text = "";
        lblExempted.Text = "";
        lblExemptedRen.Text = "";
        lblInprocess.Text = "";
        lblInprocessRen.Text = "";
        lblReferback.Text = "";
        lblReferbackRen.Text = "";
        lblRejected.Text = "";
        lblRejectedRen.Text = "";
        lblSubmitted.Text = "";
        lblSubmittedRen.Text = "";
        lblTotalNoofapplicationreceived.Text = "";
        lblTotalNoofapplicationreceivedRen.Text = "";
        //try
        //{
        //    GetDataBindAppraisalStatus();
        //}
        //catch (Exception ex)
        //{
        //}
    }
}