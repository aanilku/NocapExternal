using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.Misc.Telemetry;
using NOCAP.BLL.UserManagement;

public partial class ExternalUser_Telemetry_TelemetryUserLoginDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            if (PreviousPage != null)
            {
                Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");

                if (placeHolder != null)
                {
                    Label lblApplicationRenewCode = (Label)placeHolder.FindControl("lblApplicationRenewCode");
                    if (lblApplicationRenewCode != null && lblApplicationRenewCode.Text != "")
                        lblApplicationCodeFrom.Text = HttpUtility.HtmlEncode(lblApplicationRenewCode.Text);
                    else
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblApplicationCode");
                        if (SourceLabel != null && SourceLabel.Text != "")
                            lblApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                    }
                    Label SourceLabelPreviousPage = (Label)placeHolder.FindControl("lblApplicationCodeFrom");
                    if (SourceLabelPreviousPage != null && SourceLabelPreviousPage.Text != "")
                        lblApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabelPreviousPage.Text);

                    BindTelemetryDetailGrid(Convert.ToInt64(lblApplicationCodeFrom.Text.Trim()));
                }
            }

            

        }
    }

    void BindTelemetryDetailGrid(long AppCode)
    {
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null;
              NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = null;
      
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = null;

        NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_InfrastructureNewApplication, out obj_MiningNewApplication,  AppCode);

        if (obj_IndustrialNewApplication!=null && obj_IndustrialNewApplication.CreatedByExUC>0)
        {
          lblName.Text = obj_IndustrialNewApplication.NameOfIndustry;
          lblNo.Text = obj_IndustrialNewApplication.IndustrialNewApplicationNumber;
        }
        if (obj_MiningNewApplication != null && obj_MiningNewApplication.CreatedByExUC > 0)
        {
            lblName.Text = obj_MiningNewApplication.NameOfMining;
            lblNo.Text = obj_MiningNewApplication.MiningNewApplicationNumber;
        }
        if (obj_InfrastructureNewApplication != null && obj_InfrastructureNewApplication.CreatedByExUC > 0)
        {
            lblName.Text = obj_InfrastructureNewApplication.NameOfInfrastructure;
            lblNo.Text = obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber;
        }
        NOCAP.BLL.Misc.Telemetry.TelemetryUserLoginDetail obj_TelemetryUserLoginDetail1 = new NOCAP.BLL.Misc.Telemetry.TelemetryUserLoginDetail();
        obj_TelemetryUserLoginDetail1.ApplicationCode = AppCode;
        gvTelemetry.DataSource = obj_TelemetryUserLoginDetail1.GetTelemetryUserLoginDetailList();
        gvTelemetry.DataBind();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {


        TelemetryUserLoginDetail obj_TelemetryUserLoginDetail = new TelemetryUserLoginDetail();
        obj_TelemetryUserLoginDetail.ApplicationCode = Convert.ToInt64(lblApplicationCodeFrom.Text);  //take from previous
        obj_TelemetryUserLoginDetail.TelemetryName = txtTelName.Text;
        obj_TelemetryUserLoginDetail.TelemetryUrl = txtTelUrl.Text;
        obj_TelemetryUserLoginDetail.TelUserName = txtTelUserName.Text;
        obj_TelemetryUserLoginDetail.TelPassword = txtTelPassword.Text;
        obj_TelemetryUserLoginDetail.CreatedByExUC = Convert.ToInt64(Session["ExternalUserCode"]);
        int int_result = obj_TelemetryUserLoginDetail.Add();
        if (int_result == 1)
        {
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = obj_TelemetryUserLoginDetail.CustumMessage;
            ControlClear();
            BindTelemetryDetailGrid(Convert.ToInt64(lblApplicationCodeFrom.Text));
        }
        else
        {
            lblMessage.Text = obj_TelemetryUserLoginDetail.CustumMessage;
        }

    }


    protected void gvTelemetry_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
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
                gvTelemetry.EditIndex = -1;
                BindTelemetryDetailGrid(Convert.ToInt64(lblApplicationCodeFrom.Text));
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
 

    protected void gvTelemetry_RowEditing(object sender, GridViewEditEventArgs e)
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

                int int_FinalEditIndexNo = 0;
                int int_EditIndexNo = 0;
                lblMessage.Text = "";

                string str_applicationCode = Convert.ToString(gvTelemetry.DataKeys[e.NewEditIndex].Values["ApplicationCode"]);
                string str_applicationSN = Convert.ToString(gvTelemetry.DataKeys[e.NewEditIndex].Values["ApplicationSN"]);

                gvTelemetry.EditIndex = -1;
                BindTelemetryDetailGrid(Convert.ToInt64(lblApplicationCodeFrom.Text));

                DataKey key = default(DataKey);
                foreach (GridViewRow row in gvTelemetry.Rows)
                {
                    string str_tempapplicationCode = null;
                    string str_tempapplicationSN = null;

                    int_EditIndexNo = row.RowIndex;
                    key = gvTelemetry.DataKeys[int_EditIndexNo];
                    str_tempapplicationCode = Convert.ToString(key.Values["ApplicationCode"]);
                    str_tempapplicationSN = Convert.ToString(key.Values["ApplicationSN"]);

                    if (str_tempapplicationCode == str_applicationCode && str_tempapplicationSN == str_applicationSN)
                    {
                        int_FinalEditIndexNo = row.RowIndex;

                    }
                }
                gvTelemetry.EditIndex = int_FinalEditIndexNo;

                gvTelemetry.DataBind();



               // NOCAP.BLL.Misc.Telemetry.TelemetryUserLoginDetail populeteobj_TelemetryUserLoginDetail1 = new NOCAP.BLL.Misc.Telemetry.TelemetryUserLoginDetail(Convert.ToInt64(str_applicationCode), Convert.ToInt32(str_applicationSN));




            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
                //lblMessage.Text = ex.Message;
            }
            finally
            {

            }
        }
    }


    protected void gvTelemetry_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            // strActionName = "Update";
            try
            {
                if (Page.IsValid)
                {
                    //  int ApplicationTypeCode;
                    // string strYNCode = "";
                    //  string strYNVerificationCode = "";
                    //DropDownList ddTemp = new DropDownList();
                    // DropDownList ddTempVerification = new DropDownList();
                    // NOCAP.BLL.UserManagement.User obj_internalUser = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(Session["ExternalUserCode"]));

                    // string str_role;
                    int index = gvTelemetry.EditIndex;
                    GridViewRow row = gvTelemetry.Rows[index];


                    TextBox txtTelemetryName = (TextBox)row.FindControl("txtTelemetryName");
                    TextBox txtTelemetryUrl = (TextBox)row.FindControl("txtTelemetryUrl");
                    TextBox txtTelUserName = (TextBox)row.FindControl("txtTelUserName");
                    TextBox txtTelPassword = (TextBox)row.FindControl("txtTelPassword");



                    long lng_ApplicationCode = Convert.ToInt64(gvTelemetry.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                    int int_ApplicationSN = Convert.ToInt32(gvTelemetry.DataKeys[e.RowIndex].Values["ApplicationSN"]);
                    TelemetryUserLoginDetail obj_TelemetryUserLoginDetail = new TelemetryUserLoginDetail(lng_ApplicationCode, int_ApplicationSN);
                    obj_TelemetryUserLoginDetail.TelemetryName = txtTelemetryName.Text;
                    obj_TelemetryUserLoginDetail.TelemetryUrl = txtTelemetryUrl.Text;
                    obj_TelemetryUserLoginDetail.TelUserName = txtTelUserName.Text;
                    obj_TelemetryUserLoginDetail.TelPassword = txtTelPassword.Text;

                    obj_TelemetryUserLoginDetail.ModifiedByExUC = Convert.ToInt64(Session["ExternalUserCode"]);




                    if (obj_TelemetryUserLoginDetail.Update() == 1)
                    {
                        //strStatus = "Record Update Successfully !";
                        lblMessage.Text = HttpUtility.HtmlEncode(obj_TelemetryUserLoginDetail.CustumMessage);
                        lblMessage.ForeColor = System.Drawing.Color.Blue;
                        gvTelemetry.EditIndex = -1;
                        BindTelemetryDetailGrid(Convert.ToInt64(lblApplicationCodeFrom.Text));

                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Record Update Failed');", true);

                        //strStatus = "Record Update Failed !";
                        //Label lblApplicationTypeDescription = (Label)row.FindControl("lblApplicationTypeDescription");
                        //if (lblApplicationTypeDescription != null)
                        //{
                        //    lblApplicationTypeDescription.Visible = true;
                        //    lblApplicationTypeDescription.Text = HttpUtility.HtmlEncode(obj_TelemetryUserLoginDetail.CustumMessage);
                        //    lblApplicationTypeDescription.ForeColor = System.Drawing.Color.Red;
                        //}

                    }
                }
            }
            catch (Exception)
            {
                // strStatus = "Record Update Failed !";
                Response.Redirect("~/ExternalErrorPage.aspx", false);

            }
            finally
            {
                //ActionTrail obj_IntActionTrail = new ActionTrail();
                //if (Session["InternalUserCode"] != null)
                //{
                //    obj_IntActionTrail.UserCode = Convert.ToInt64(Session["InternalUserCode"]);
                //    obj_IntActionTrail.IP_Address = Request.UserHostAddress;
                //    obj_IntActionTrail.ActionPerformed = strPageName + "-" + strActionName;
                //    obj_IntActionTrail.Status = strStatus;
                //    if (obj_IntActionTrail != null)
                //        ActionTrailDAL.IntActionSave(obj_IntActionTrail);
                //}
            }
        }
    }


    protected void gvTelemetry_Sorting(object sender, GridViewSortEventArgs e)
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

                NOCAP.BLL.Master.ApplicationType obj_applicationType = new NOCAP.BLL.Master.ApplicationType();
                lblSortField.Text = e.SortExpression;
                gvTelemetry.EditIndex = -1;
                BindTelemetryDetailGrid(Convert.ToInt64(lblApplicationCodeFrom.Text));
                lblMessage.Text = "";

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
                //lblMessage.Text = ex.Message;

            }
        }
    }


    protected void gvTelemetry_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
                lblMessage.Text = "";
                gvTelemetry.EditIndex = -1;
                gvTelemetry.PageIndex = e.NewPageIndex;
                BindTelemetryDetailGrid(Convert.ToInt64(lblApplicationCodeFrom.Text));
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
                //lblMessage.Text = ex.Message;

            }
        }
    }
     

    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        ControlClear();
    }

    void ControlClear()
    {
        txtTelName.Text = "";
        txtTelUrl.Text = "";
        txtTelUserName.Text = "";
        txtTelPassword.Text = "";
    }


    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ExternalUser/ApplicantHome.aspx", false);
    }
     
    protected void gvTelemetry_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                if (!NOCAPExternalUtility.IsNumeric(gvTelemetry.DataKeys[e.RowIndex].Values["ApplicationCode"]))
                {

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('ApplicationCode allows only Numeric');", true);
                    return;
                }
                if (!NOCAPExternalUtility.IsNumeric(gvTelemetry.DataKeys[e.RowIndex].Values["ApplicationSN"]))
                {

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('ApplicationSN allows only Numeric');", true);
                    return;
                }
                long lng_ApplicationCode = Convert.ToInt32(gvTelemetry.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_ApplicationSN = Convert.ToInt32(gvTelemetry.DataKeys[e.RowIndex].Values["ApplicationSN"]);
                TelemetryUserLoginDetail obj_deleteTelemetryUserLoginDetail = new TelemetryUserLoginDetail(lng_ApplicationCode, int_ApplicationSN);
                if (obj_deleteTelemetryUserLoginDetail.Delete() == 1)
                {                     
                    lblMessage.Text = HttpUtility.HtmlEncode(obj_deleteTelemetryUserLoginDetail.CustumMessage);
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    BindTelemetryDetailGrid(Convert.ToInt64(lblApplicationCodeFrom.Text.Trim()));
                }
                else
                {                    
                    lblMessage.Text = HttpUtility.HtmlEncode(obj_deleteTelemetryUserLoginDetail.CustumMessage);
                    lblMessage.ForeColor = System.Drawing.Color.Red;

                }
            }
            catch (Exception)
            {                
                Response.Redirect("~/InternalErrorPage.aspx", false);
                //lblMessage.Text = ex.Message;

            }             
        }
    }

     
   
}