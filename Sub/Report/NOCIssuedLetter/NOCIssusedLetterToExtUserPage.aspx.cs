using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
public partial class Sub_Report_NOCIssuedLetter_NOCIssusedLetterToExtUserPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //NTRPTest();
        if (IsPostBack != true)
        {
            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;


            if (NOCAPExternalUtility.FillDropDownIssuedLetterTypeName(ref ddlIssueLetterType) != 1)
            {

                lblMessage.Text = "Problem in Issue Letter Type Name.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            if (NOCAPExternalUtility.FillDropDownState(ref ddlState) != 1)
            {
                lblMessage.Text = "Problem in state population";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            if (NOCAPExternalUtility.FillDropDownApplicationType(ref ddlApplicationType) != 1)
            {
                Response.Write("Problem in Application Type ");
            }
            //if (NOCAPExternalUtility.FillDropDownAreaType(ref ddlAreaType) != 1)
            //{
            //    Response.Write("Problem in Application Type ");
            //}
            //if (NOCAPExternalUtility.FillDropDownAreaType(ref ddlPresentAreaType) != 1)
            //{
            //    Response.Write("Problem in Application Type ");
            //}

        }

    }

    protected void btnShowRecord_Click(object sender, EventArgs e)
    {
        if (txtCaptchaCode.Text.Trim() != "")
        {
            if (Convert.ToString(Session["Captcha"]) == txtCaptchaCode.Text.Trim())
            {
                NOCAP.BLL.Misc.NOCLetterForPublic.NOCLetterForPublic obj_NOCLetterForPublic = new NOCAP.BLL.Misc.NOCLetterForPublic.NOCLetterForPublic();
                if (ddlState.SelectedIndex > 0)
                    obj_NOCLetterForPublic.StateCode = Convert.ToInt16(ddlState.SelectedValue.ToString());
                if (ddlDistrict.SelectedIndex > 0)
                    obj_NOCLetterForPublic.DistrictCode = Convert.ToInt16(ddlDistrict.SelectedValue.ToString());
                if (ddlSubDistrict.SelectedIndex > 0)
                    obj_NOCLetterForPublic.SubDistrictCode = Convert.ToInt16(ddlSubDistrict.SelectedValue.ToString());
                if (ddlApplicationType.SelectedIndex > 0)
                    obj_NOCLetterForPublic.ApplicationTypeCode = Convert.ToInt16(ddlApplicationType.SelectedValue.ToString());
                if (ddlApplicationTypeCat.SelectedIndex > 0)
                    obj_NOCLetterForPublic.ApplicationTypeCategoryCode = Convert.ToInt16(ddlApplicationTypeCat.SelectedValue.ToString());



               // if (ddlAreaType.SelectedIndex > 0)
                   // obj_NOCLetterForPublic.AreaTypeCode = Convert.ToInt16(ddlAreaType.SelectedValue.ToString());

               // if (ddlAreaTypeCat.SelectedIndex > 0)
                   // obj_NOCLetterForPublic.AreaTypeCatCode = Convert.ToInt16(ddlAreaTypeCat.SelectedValue.ToString());




               // if (ddlPresentAreaType.SelectedIndex > 0)
                   // obj_NOCLetterForPublic.PresentAreaTypeCode = Convert.ToInt16(ddlPresentAreaType.SelectedValue.ToString());

               // if (ddlPresentAreaTypeCat.SelectedIndex > 0)
                   // obj_NOCLetterForPublic.PresentAreaTypeCatCode = Convert.ToInt16(ddlPresentAreaTypeCat.SelectedValue.ToString());
                obj_NOCLetterForPublic.ProjectName = txtProjectType.Text.Trim();
                obj_NOCLetterForPublic.ApplicationNumber = txtApplicationNo.Text.Trim();
                if (ddlIssueLetterType.SelectedIndex > 0)
                   obj_NOCLetterForPublic.LetterTypeCode = Convert.ToInt32(ddlIssueLetterType.SelectedValue.ToString());
                if (obj_NOCLetterForPublic.GetAll() == 1)
                {
                    NOCAP.BLL.Misc.NOCLetterForPublic.NOCLetterForPublic[] arr = obj_NOCLetterForPublic.NOCLetterForPublicCollection;
                    gvNOCIssuedLetter.DataSource = obj_NOCLetterForPublic.NOCLetterForPublicCollection.ToList();
                    gvNOCIssuedLetter.DataBind();
                }
                else
                {
                    gvNOCIssuedLetter.DataSource = null;
                    gvNOCIssuedLetter.DataBind();
                }
            }
            else
            {
                lblMessage.Text = "Please Enter Valid Captcha Code";
                txtCaptchaCode.Text = "";
                return;
            }
        }
        else
        {
            lblMessage.Text = "Please Enter Captcha Code";
            txtCaptchaCode.Focus();
            return;
        }
        txtCaptchaCode.Text = "";
        lblMessage.Text = "";








    }

    #region NTRP Test
    //private void NTRPTest()
    //{
    //    NOCAP.BLL.Master.NTRPMapping obj_NTRPMapping = new NOCAP.BLL.Master.NTRPMapping(1);
    //    NOCAP.BLL.Misc.Payment.SADOnlinePayment obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment();
    //    obj_SADOnlinePayment.GetALL();
    //    NOCAP.BLL.Misc.Payment.SADOnlinePayment[] arr_SADOnlinePayment = obj_SADOnlinePayment.SADOnlinePaymentCollection;

    //    foreach (NOCAP.BLL.Misc.Payment.SADOnlinePayment SADOnlinePayment in arr_SADOnlinePayment)
    //    {

    //        string[] arr = TransactionStatus(SADOnlinePayment, obj_NTRPMapping, "https://training.pfms.gov.in/bharatkosh/getstatus");
    //        if (arr != null && arr.Length > 0)
    //        {

    //            #region PaymentStatus
    //            switch (arr[1].ToUpper())
    //            {

    //                case "SUCCESS":
    //                    SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.SUCCESS;
    //                    SADOnlinePayment.StatusXX = "SUCCESS";
    //                    break;
    //                case "FAIL":
    //                    SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.FAIL;
    //                    SADOnlinePayment.StatusXX = "FAIL";
    //                    break;
    //                case "STATUS UNKNOWN/ABORT":
    //                    SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.StatusUnknownAbort;
    //                    SADOnlinePayment.StatusXX = "STATUS UNKNOWN/ABORT";
    //                    break;
    //                case "NO RECORD FOUND":
    //                    SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.NoRecordsFound;
    //                    SADOnlinePayment.StatusXX = "NO RECORD FOUND";
    //                    break;
    //                case "BOOKED":
    //                    SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.BOOKED;
    //                    SADOnlinePayment.StatusXX = "BOOKED";
    //                    break;
    //                case "PENDING":
    //                    SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.PENDING;
    //                    SADOnlinePayment.StatusXX = "PENDING";
    //                    break;
    //                case "EXPIRED":
    //                    SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.EXPIRED;
    //                    SADOnlinePayment.StatusXX = "EXPIRED";
    //                    break;
    //                case "FAILED OR REFUND IF RECEIVED":
    //                    SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.FailedOrRefundIfReceived;
    //                    SADOnlinePayment.StatusXX = "FAILED OR REFUND IF RECEIVED";
    //                    break;
    //                case "REFUND SETTLED":
    //                    SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.RefundSettled;
    //                    SADOnlinePayment.StatusXX = "REFUND SETTLED";
    //                    break;
    //                case "PAYMENT INITIATED":
    //                    SADOnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.PaymentInitiated;
    //                    SADOnlinePayment.StatusXX = "PAYMENT INITIATED";
    //                    break;
    //                case "CONFIRMED":
    //                    SADOnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.Confirmed;
    //                    SADOnlinePayment.StatusXX = "CONFIRMED";
    //                    break;
    //                case "INCOMPLETE":
    //                    SADOnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.Incomplete;
    //                    SADOnlinePayment.StatusXX = "INCOMPLETE";
    //                    break;
    //                case "SUBMITTED":
    //                    SADOnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.Submitted;
    //                    SADOnlinePayment.StatusXX = "SUBMITTED";
    //                    break;

    //                default:
    //                    SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.NotDefined;
    //                    SADOnlinePayment.StatusXX = "NotDefined";
    //                    break;
    //            }
    //            #endregion

    //            #region ReceiptStatus
    //            // switch (arr[1])
    //            // {


    //            //case "PAYMENT INITIATED":
    //            //    SADOnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.PaymentInitiated;
    //            //    SADOnlinePayment.StatusXX = "PAYMENT INITIATED";
    //            //    break;
    //            //case "CONFIRMED":
    //            //    SADOnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.Confirmed;
    //            //    SADOnlinePayment.StatusXX = "CONFIRMED";
    //            //    break;
    //            //case "INCOMPLETE":
    //            //    SADOnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.Incomplete;
    //            //    SADOnlinePayment.StatusXX = "INCOMPLETE";
    //            //    break;
    //            //case "SUBMITTED":
    //            //    SADOnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.Submitted;
    //            //    SADOnlinePayment.StatusXX = "SUBMITTED";
    //            //    break;
    //            //default:

    //            //    SADOnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.NotDefined;
    //            //    SADOnlinePayment.StatusXX = "NotDefined";
    //            //    break;
    //            // }
    //            #endregion

    //            SADOnlinePayment.TransactionRefNo = arr[2];

    //        }
    //        SADOnlinePayment.SetOnlinePaymentStatusByWinService();
    //    }
    //}

    //private string[] TransactionStatus(NOCAP.BLL.Misc.Payment.SADOnlinePayment SADOnlinePayment, NOCAP.BLL.Master.NTRPMapping NTRPMapping, string URL)
    //{
    //    HttpResponseMessage messge = null;
    //    string[] arr = null;
    //    string access_token = string.Empty;
    //    try
    //    {

    //        HttpClient client = new HttpClient();
    //        client.BaseAddress = new Uri(URL);
    //        client.DefaultRequestHeaders.Accept.Clear();
    //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

    //        Dictionary<string, string> openWith = new Dictionary<string, string>();
    //        openWith.Add("OrderId", SADOnlinePayment.OrderPaymentCode);
    //        openWith.Add("PurposeId", NTRPMapping.OrderContent.ToString());

    //        FormUrlEncodedContent content = new FormUrlEncodedContent(openWith);
    //        System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
    //        messge = client.PostAsync(URL, content).Result;
    //        if (messge.IsSuccessStatusCode)
    //        {
    //            string result = messge.Content.ReadAsStringAsync().Result;
    //            arr = result.Split('|');
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        arr = null;
    //    }
    //    return arr;
    //}

    #endregion
    protected void gvNOCIssuedLetter_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow) // checking if row is datarow or not
        {
            HiddenField hdnLinkDepth = e.Row.FindControl("hdnLinkDepth") as HiddenField;
            Label lblAppPurposecode = e.Row.FindControl("lblAppPurposecode") as Label;
            if (hdnLinkDepth.Value.ToString() != "0")
                lblAppPurposecode.Text = NOCAPExternalUtility.AddOrdinal(Convert.ToInt32(hdnLinkDepth.Value)).ToString() + " Renewal";
            else
                lblAppPurposecode.Text = "New";
        }
    }

    protected void lbtnScanDownload_Click(object sender, EventArgs e)
    {

        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {

                //DateTime dd = new DateTime();
                //dd.ToShortDateString();

                int intINDAppCode = 0;
                LinkButton button = (LinkButton)sender;
                string lnkTxt = button.Text;
                GridViewRow row = (GridViewRow)button.NamingContainer;
                if (row != null)
                {
                    if (NOCAPExternalUtility.IsNumeric(button.CommandArgument))
                    {
                        intINDAppCode = Convert.ToInt32(button.CommandArgument);
                    }
                }
                int int_indAppCode = intINDAppCode;
                //Details Amardeep
                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = null;
                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = null;
                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = null;

                NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = null;
                NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = null;
                NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplication = null;

                NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_industrialNewApplication, out obj_infrastructureNewApplication, out obj_miningNewApplication, out obj_industrialRenewApplication, out obj_infrastructureRenewApplication, out obj_miningRenewApplication, intINDAppCode);
                if (obj_industrialNewApplication != null)
                {
                    NOCAPExternalUtility.INDScanLetterDownloadFiles(int_indAppCode);
                }
                else if (obj_infrastructureNewApplication != null)
                {
                    NOCAPExternalUtility.INFScanLetterDownloadFiles(int_indAppCode);
                }
                else if (obj_miningNewApplication != null)
                {
                    NOCAPExternalUtility.MINScanLetterDownloadFiles(int_indAppCode);
                }

                else if (obj_industrialRenewApplication != null)
                {
                    NOCAPExternalUtility.INDRenScanLetterDownloadFiles(int_indAppCode);
                }
                else if (obj_infrastructureRenewApplication != null)
                {
                    NOCAPExternalUtility.INFRenScanLetterDownloadFiles(int_indAppCode);
                }
                else if (obj_miningRenewApplication != null)
                {
                    NOCAPExternalUtility.MINRenScanLetterDownloadFiles(int_indAppCode);
                }
                else
                {


                }
                //
                //NOCAPExternalUtility.INDScanLetterDownloadFiles(int_indAppCode);
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
                int intINDAppCode = 0;
                LinkButton button = (LinkButton)sender;
                GridViewRow row = (GridViewRow)button.NamingContainer;
                if (row != null)
                {
                    if (NOCAPExternalUtility.IsNumeric(button.CommandArgument))
                    {
                        intINDAppCode = Convert.ToInt32(button.CommandArgument);
                    }

                }
                int int_indAppCode = intINDAppCode;
                //Details Amardeep
                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = null;
                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = null;
                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = null;

                NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = null;
                NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = null;
                NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplication = null;

                NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_industrialNewApplication, out obj_infrastructureNewApplication, out obj_miningNewApplication, out obj_industrialRenewApplication, out obj_infrastructureRenewApplication, out obj_miningRenewApplication, intINDAppCode);
                if (obj_industrialNewApplication != null)
                {
                    NOCAPExternalUtility.INDLetterAppDownloadFiles(int_indAppCode);
                }
                else if (obj_infrastructureNewApplication != null)
                {
                    NOCAPExternalUtility.INFLetterAppDownloadFiles(int_indAppCode);
                }
                else if (obj_miningNewApplication != null)
                {
                    NOCAPExternalUtility.MINLetterAppDownloadFiles(int_indAppCode);
                }

                else if (obj_industrialRenewApplication != null)
                {
                    NOCAPExternalUtility.INDRenLetterAppDownloadFiles(int_indAppCode);
                }
                else if (obj_infrastructureRenewApplication != null)
                {
                    NOCAPExternalUtility.INFRenLetterAppDownloadFiles(int_indAppCode);
                }
                else if (obj_miningRenewApplication != null)
                {
                    NOCAPExternalUtility.MINRenLetterAppDownloadFiles(int_indAppCode);
                }
                else
                {


                }
                //NOCAPExternalUtility.INDLetterAppDownloadFiles(int_indAppCode);
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
    protected void imgBtnCaptchaRefresh_Click(object sender, ImageClickEventArgs e)
    {
        txtCaptchaCode.Text = "";
        lblMessage.Text = "";
    }

    protected void gvNOCIssuedLetter_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvNOCIssuedLetter.PageIndex = e.NewPageIndex;
        NOCAP.BLL.Misc.NOCLetterForPublic.NOCLetterForPublic obj_NOCLetterForPublic = new NOCAP.BLL.Misc.NOCLetterForPublic.NOCLetterForPublic();
        if (ddlState.SelectedIndex > 0)
            obj_NOCLetterForPublic.StateCode = Convert.ToInt16(ddlState.SelectedValue.ToString());
        if (ddlDistrict.SelectedIndex > 0)
            obj_NOCLetterForPublic.DistrictCode = Convert.ToInt16(ddlDistrict.SelectedValue.ToString());
        if (obj_NOCLetterForPublic.GetAll() == 1)
        {
            gvNOCIssuedLetter.DataSource = obj_NOCLetterForPublic.NOCLetterForPublicCollection.ToList();
            gvNOCIssuedLetter.DataBind();
        }

    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {

        int intStateCode;
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {

                lblMessage.Text = "";
                ddlDistrict.Items.Clear();
                ddlSubDistrict.Items.Clear();
                if (ddlState.SelectedValue == "")
                {
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlDistrict);
                }
                else
                {
                    intStateCode = Convert.ToInt32(ddlState.SelectedValue);
                    if (NOCAPExternalUtility.FillDropDownDistrict(ref ddlDistrict, intStateCode) != 1)
                    {
                        lblMessage.Text = "Problem in district population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {

        int int_StateCode;
        int int_DistricCode;

        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {

                lblMessage.Text = "";
                ddlSubDistrict.Items.Clear();
                //ddlTownOrVillage.SelectedValue = "";
                //ddlVillage.Items.Clear();
                // ddlTown.Items.Clear();

                if (ddlDistrict.SelectedValue == "")
                {
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlSubDistrict);
                }
                else
                {
                    int_StateCode = Convert.ToInt32(ddlState.SelectedValue);
                    int_DistricCode = Convert.ToInt32(ddlDistrict.SelectedValue);

                    if (NOCAPExternalUtility.FillDropDownSubDistrict(ref ddlSubDistrict, int_DistricCode, int_StateCode) != 1)
                    {
                        lblMessage.Text = "Problem in Subdistrict population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }


    }

    protected void ddlApplicationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                lblMessage.Text = "";
                int APPtypecode = Convert.ToInt32(ddlApplicationType.SelectedValue);
                if (!((NOCAPExternalUtility.FillDropDownApplicationTypeCategoryBasedOnApplicationType(ref ddlApplicationTypeCat, APPtypecode)) == 1))
                {
                    lblMessage.Text = "Problem in Subdistrict population";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    //protected void ddlAreaType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
    //    {
    //        Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
    //    }
    //    else
    //    {
    //        try
    //        {
    //            lblMessage.Text = "";
    //            int APPtypecode = Convert.ToInt32(ddlAreaType.SelectedValue);
    //            if (!((NOCAPExternalUtility.FillDropDownAreaTypeCategoryBasedOnAreaType(ref ddlAreaTypeCat, APPtypecode)) == 1))
    //            {
    //                lblMessage.Text = "Problem in Subdistrict population";
    //                lblMessage.ForeColor = System.Drawing.Color.Red;
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            Response.Redirect("~/ExternalErrorPage.aspx", false);
    //        }
    //    }
    //}
    //protected void ddlPresentAreaType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
    //    {
    //        Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
    //    }
    //    else
    //    {
    //        try
    //        {
    //            lblMessage.Text = "";
    //            int APPtypecode = Convert.ToInt32(ddlPresentAreaType.SelectedValue);
    //            if (!((NOCAPExternalUtility.FillDropDownAreaTypeCategoryBasedOnAreaType(ref ddlPresentAreaTypeCat, APPtypecode)) == 1))
    //            {
    //                lblMessage.Text = "Problem in Subdistrict population";
    //                lblMessage.ForeColor = System.Drawing.Color.Red;
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            Response.Redirect("~/ExternalErrorPage.aspx", false);
    //        }
    //    }
    //}
}