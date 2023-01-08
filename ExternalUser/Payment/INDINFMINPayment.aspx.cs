using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
public partial class ExternalUser_Payment_INDINFMINPayment : System.Web.UI.Page
{
    string strPageName = "INDINFMINPayment";
    string strActionName = "";
    string strStatus = "";

    //static string prevPage = String.Empty;
    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        MasterPageFile = NOCAPExternalUtility.SetMasterPageWithoutLeftMenu((Convert.ToInt32(Session["ExternalUserCode"])));
    //    }
    //    catch (Exception)
    //    {
    //        Response.Redirect("~/ExternalErrorPage.aspx", false);
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                //Session["CSRF"] = hidCSRF.Value;
                ViewState["CSRF"] = hidCSRF.Value;
                FillDropDownApplicationType();
                FillDropDownApplicationPurpose();            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }

    #region Private Method
    private void FillDropDownApplicationType()
    {
        if (NOCAPExternalUtility.FillDropDownApplicationType(ref ddlApplicationType) != 1)
            Response.Write("Problem in Application Type ");
    }
    private void FillDropDownApplicationPurpose()
    {
        if (NOCAPExternalUtility.FillDropDownApplicationPurpose(ref ddlApplicationPurpose) != 1)
            Response.Write("Problem in Application Purpose");
        ddlApplicationPurpose.Items.Remove(ddlApplicationPurpose.Items.FindByText("Withdraw"));
        ddlApplicationPurpose.Items.Remove(ddlApplicationPurpose.Items.FindByText("Cancelation"));
    }
    private void LoadPaymentApplication()
    {
         NOCAP.BLL.Misc.Payment.Payment objPaymentApplication = new NOCAP.BLL.Misc.Payment.Payment();

         if (ddlApplicationType.SelectedIndex > 0)
             objPaymentApplication.AppTypeCode = Convert.ToInt32(ddlApplicationType.SelectedValue.ToString());    
         if (ddlApplicationPurpose.SelectedIndex > 0)
             objPaymentApplication.AppPurposeCode = Convert.ToInt32(ddlApplicationPurpose.SelectedValue);
         objPaymentApplication.ApplicantExUserCode = Convert.ToInt64(Session["ExternalUserCode"].ToString());
         List<NOCAP.BLL.Misc.Payment.Payment> obj_PaymentList = objPaymentApplication.GetFilterPaymentApplicationList(objPaymentApplication);
         grdPaymentList.DataSource = obj_PaymentList;
         grdPaymentList.DataBind();
    
    }
    #endregion

    #region DDL Index Changed
    //protected void ddlApplicationType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
    //        Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
    //    else
    //    {
    //        //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "CheckHideOrShow();", true);
    //        hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
    //        ViewState["CSRF"] = hidCSRF.Value;
    //        try
    //        {
    //            if (ddlApplicationType.SelectedValue != "")
    //            {
    //                if (!NOCAPExternalUtility.IsNumeric(ddlApplicationType.SelectedValue))
    //                {
    //                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Application Code allows only Numeric Value.');", true);
    //                    return;
    //                }
    //                int APPtypecode = Convert.ToInt32(ddlApplicationType.SelectedValue);
    //                if ((NOCAPExternalUtility.FillDropDownApplicationTypeCategory(ref ddlApplicationTypeCat, APPtypecode)) == 1)
    //                {
    //                    ddlApplicationTypeCat.Enabled = true;
    //                    strActionName = "AdvanceSearchSuccess";
    //                    strStatus = "Record Search successfully";
    //                }
    //                else
    //                    Response.Redirect("~/InternalErrorPage.aspx", false);
    //            }
    //            else
    //            {
    //                ddlApplicationTypeCat.SelectedValue = "";
    //                ddlApplicationTypeCat.Enabled = false;
    //                ddlApplicationTypeCat.Text = "";
    //                ddlApplicationTypeCat.SelectedIndex = 0;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            lblMsg.Text = "Please select Application Type.";
    //        }
    //        finally
    //        {
    //            ActionTrail obj_ExtActionTrail = new ActionTrail();
    //            if (Session["ExternalUserCode"] != null)
    //            {
    //                obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
    //                obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
    //                obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
    //                obj_ExtActionTrail.Status = strStatus;
    //                if (obj_ExtActionTrail != null)
    //                    ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
    //            }
    //        }
    //    }
    //}
    #endregion

    #region Button Click
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {
               
                LoadPaymentApplication();
              
            }
            catch (Exception)
            {
                //lblMsg.Text = ex.Message;
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

    #region grdPaymentList Events
    protected void grdPaymentList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        NOCAP.BLL.Misc.Payment.Payment obj_Payment = new NOCAP.BLL.Misc.Payment.Payment();

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //string key = grdSearchList.DataKeys[e.Row.RowIndex].Value.ToString();
            //e.Row.Attributes.Add("id", key);
            LinkButton lnkBtn = new LinkButton();
             LinkButton lnkbtnView=new LinkButton();
            //lnkBtn = (LinkButton)e.Row.FindControl("lbtnWorkFlowRequire");
            //if (lnkBtn.Text == "YES")
            //    (e.Row.FindControl("lbtnWorkFlowRequire") as LinkButton).Enabled = true;
            //else
            //{
            //    (e.Row.FindControl("lbtnWorkFlowRequire") as LinkButton).Enabled = false;
            //    lnkBtn.ForeColor = System.Drawing.Color.Black;
            //}

            lnkBtn = (LinkButton)e.Row.FindControl("AppliedForRenewal");
            HiddenField hdn = (HiddenField)e.Row.FindControl("hdnRenewal");
            HiddenField hdnPaymentAmountReceiveFinally = (HiddenField)e.Row.FindControl("hdnPaymentAmountReceiveFinally");

            if (Convert.ToInt16(hdn.Value) > 0)
                lnkBtn.Text = "Renew (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(Convert.ToInt32(hdn.Value))) + ")";
            else
                lnkBtn.Text = "New";
            lnkBtn = (LinkButton)e.Row.FindControl("Pay");
            lnkbtnView = (LinkButton)e.Row.FindControl("View");
            if (hdnPaymentAmountReceiveFinally.Value == "No")
            {
                lnkBtn.Enabled = true;
                lnkbtnView.Enabled = false;
            }
            else
            {
                lnkBtn.Enabled = false;
                lnkbtnView.Enabled = true;
            }

        }
    }
   
    protected void grdPaymentList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)        
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);        
        else
        {
            
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {
                grdPaymentList.PageIndex = e.NewPageIndex;
                LoadPaymentApplication();
                strActionName = "PaymentSearchSuccess";
                strStatus = "Record Search successfully";
            }          
            catch (Exception ex)
            {
                lblMsg.Text = "Please select the page number.";
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
  
    protected void grdPaymentList_RowCommand(object sender, GridViewCommandEventArgs e)
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
                if (e.CommandName == "Pay")
                {
                    string rowIndex = Convert.ToString(e.CommandArgument);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('SubmitPayment.aspx','_blank')", true);
                    //Server.Transfer("~/ExternalUser/IndustrialNew/Status/IndustrialNewStatus.aspx");
                }
                else if (e.CommandName == "View")
                {
                    string rowIndex = Convert.ToString(e.CommandArgument);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('ViewPayment.aspx','_blank')", true);
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Please select at one row at one time.";
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
    protected void grdPaymentList_Sorting(object sender, GridViewSortEventArgs e)
    {
        grdPaymentList.EditIndex = -1;
        LoadPaymentApplication();
    }
    #endregion
   
   
}