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

public partial class ExternalUser_ViewAllOnlinePayment : System.Web.UI.Page
{
    string strPageName = "AllOnlinePayment";
    string strActionName = "";
    string strStatus = "";
    NOCAP.BLL.Misc.Payment.OnlinePaymentHistory obj_OnlinePaymentHist = new NOCAP.BLL.Misc.Payment.OnlinePaymentHistory();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {

            try
            {
               
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                if (NOCAPExternalUtility.FillDropDownApplicationType(ref ddlApplicationType) != 1)
                    Response.Write("Problem in Application Type ");
                ddlApplicationType.Focus();
             
               
                ddlApplicationType_SelectedIndexChanged(sender, e);


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

    private void BindGridView(GridView gv, NOCAP.BLL.Misc.Payment.OnlinePayment[] arrSAD_OnlinepPay)
    {
        gv.DataSource = arrSAD_OnlinepPay;
        gv.DataBind();
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
    protected void gvPayment_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {


            if (e.CommandName == "OrderPaymentCode")
            {

                string[] arr = HttpUtility.HtmlEncode(e.CommandArgument.ToString()).Split(',');
                lblAppCode.Text = arr[0];
             
            }

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }

    protected void gvPayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPayment.EditIndex = -1;
        gvPayment.PageIndex = e.NewPageIndex;

    }

    protected void ddlApplicationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"]= hidCSRF.Value;
            try
            {
             
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
        NOCAP.BLL.Misc.Payment.OnlinePaymentExt obj_OnlinePayment = new NOCAP.BLL.Misc.Payment.OnlinePaymentExt();
        obj_OnlinePayment.CreatedByExUC = Convert.ToInt64(Session["ExternalUserCode"]);
        obj_OnlinePayment.GetALL();
        BindGridView(gvPayment, obj_OnlinePayment.OnlinePaymentExtCollection);

    }
}