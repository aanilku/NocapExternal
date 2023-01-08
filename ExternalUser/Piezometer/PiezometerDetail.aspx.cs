using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.Misc.Piezometer;
using System.Threading;

public partial class ExternalUser_Piezometer_PiezometerDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ValidationExpInit();
            //txtInstallDate_CalendarExtender.EndDate = System.DateTime.Now;

            txtInstallDate_CalendarExtender.EndDate = System.DateTime.Now;

            txtInstallDate.Attributes.Add("readonly", "readonly");

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;          

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

                    BindPiezometerDetailGrid(Convert.ToInt64(lblApplicationCodeFrom.Text.Trim()));
                }
            }
        }
    }
    private void ValidationExpInit()
    {
        revtxtLocation.ValidationExpression = ValidationUtility.txtValSingleLineWithSpecialCharacters;
        revtxtLocation.ErrorMessage = ValidationUtility.txtValSingleLineWithSpecialCharactersMsg;

        revtxtPiezoLat.ValidationExpression = ValidationUtility.txtValForDecimalValue("4", "6");
        revtxtPiezoLat.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("4", "6");

        revtxtPiezoLong.ValidationExpression = ValidationUtility.txtValForDecimalValue("4", "6");
        revtxtPiezoLong.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("4", "6");

        revtxtDepthPiezo.ValidationExpression = ValidationUtility.txtValForDecimalValue("3", "2");
        revtxtDepthPiezo.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("3", "2");

        revtxtHgtMsrnig.ValidationExpression = ValidationUtility.txtValForDecimalValue("3", "2");
        revtxtHgtMsrnig.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("3", "2");        

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
    void BindPiezometerDetailGrid(long AppCode)
    {
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null;
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = null;

        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = null;

        NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_InfrastructureNewApplication, out obj_MiningNewApplication, AppCode);

        if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.NameOfIndustry!="")
        {
            lblName.Text = obj_IndustrialNewApplication.NameOfIndustry;
            lblNo.Text = obj_IndustrialNewApplication.IndustrialNewApplicationNumber;
        }
        if (obj_MiningNewApplication != null && obj_MiningNewApplication.NameOfMining != "")
        {
            lblName.Text = obj_MiningNewApplication.NameOfMining;
            lblNo.Text = obj_MiningNewApplication.MiningNewApplicationNumber;
        }
        if (obj_InfrastructureNewApplication != null && obj_InfrastructureNewApplication.NameOfInfrastructure!="")
        {
            lblName.Text = obj_InfrastructureNewApplication.NameOfInfrastructure;
            lblNo.Text = obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber;
        }
        NOCAP.BLL.Misc.Piezometer.PiezometerDetail obj_PiezometerDetail1 = new NOCAP.BLL.Misc.Piezometer.PiezometerDetail();
        obj_PiezometerDetail1.ApplicationCode = AppCode;       
        gvPiezometer.DataSource = obj_PiezometerDetail1.GetPiezometerDetailList();
        gvPiezometer.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

       PiezometerDetail obj_PiezometerDetail = new PiezometerDetail(); 
        obj_PiezometerDetail.ApplicationCode = Convert.ToInt64(lblApplicationCodeFrom.Text);  //take from previous        
        obj_PiezometerDetail.Location = txtLocation.Text;
        obj_PiezometerDetail.Latitude = Convert.ToDecimal(txtPiezoLat.Text);
        obj_PiezometerDetail.Longitude = Convert.ToDecimal(txtPiezoLong.Text);
        obj_PiezometerDetail.DepthofPiezo = Convert.ToDecimal(txtDepthPiezo.Text);
        obj_PiezometerDetail.HeightofMeasuring = Convert.ToDecimal(txtHgtMsrnig.Text);
        obj_PiezometerDetail.InstallDate = Convert.ToDateTime(txtInstallDate.Text);
        obj_PiezometerDetail.CreatedByExUC = Convert.ToInt64(Session["ExternalUserCode"]);
        int int_result = obj_PiezometerDetail.Add();
        if (int_result == 1)
        {
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = obj_PiezometerDetail.CustumMessage;
            ControlClear();
            BindPiezometerDetailGrid(Convert.ToInt64(lblApplicationCodeFrom.Text));
        }
        else
        {
            lblMessage.Text = obj_PiezometerDetail.CustumMessage;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        ControlClear();
    }
    void ControlClear()
    {

        txtLocation.Text = "";
        txtPiezoLat.Text = "";
        txtPiezoLong.Text = "";
        txtDepthPiezo.Text = "";
        txtHgtMsrnig.Text = "";
        txtInstallDate.Text = "";   

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ExternalUser/ApplicantHome.aspx", false);
    }
    protected void gvPiezometer_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
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
                gvPiezometer.EditIndex = -1;
                BindPiezometerDetailGrid(Convert.ToInt64(lblApplicationCodeFrom.Text));
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void gvPiezometer_RowEditing(object sender, GridViewEditEventArgs e)
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

                string str_applicationCode = Convert.ToString(gvPiezometer.DataKeys[e.NewEditIndex].Values["ApplicationCode"]);
                string str_piezometerCode = Convert.ToString(gvPiezometer.DataKeys[e.NewEditIndex].Values["PiezometerCode"]);

                gvPiezometer.EditIndex = -1;
                BindPiezometerDetailGrid(Convert.ToInt64(lblApplicationCodeFrom.Text));

                DataKey key = default(DataKey);
                foreach (GridViewRow row in gvPiezometer.Rows)
                {
                    string str_tempapplicationCode = null;
                    string str_temppiezometerCode = null;

                    int_EditIndexNo = row.RowIndex;
                    key = gvPiezometer.DataKeys[int_EditIndexNo];
                    str_tempapplicationCode = Convert.ToString(key.Values["ApplicationCode"]);
                    str_temppiezometerCode = Convert.ToString(key.Values["PiezometerCode"]);

                    if (str_tempapplicationCode == str_applicationCode && str_temppiezometerCode == str_piezometerCode)
                    {
                        int_FinalEditIndexNo = row.RowIndex;
                    }
                }
                gvPiezometer.EditIndex = int_FinalEditIndexNo;
                gvPiezometer.DataBind();               
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);               
            }
            finally
            {

            }
        }
    }
    protected void gvPiezometer_RowUpdating(object sender, GridViewUpdateEventArgs e)
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
                if (Page.IsValid)
                {                  
                    int index = gvPiezometer.EditIndex;
                    GridViewRow row = gvPiezometer.Rows[index];

                    TextBox txtPiezometerLocation = (TextBox)row.FindControl("txtPiezometerLocation");
                    TextBox txtPiezometerLatitude = (TextBox)row.FindControl("txtPiezometerLatitude");
                    TextBox txtPiezometerLongitude = (TextBox)row.FindControl("txtPiezometerLongitude");
                    TextBox txtDepthOfPiezometer = (TextBox)row.FindControl("txtDepthOfPiezometer");
                    TextBox txtHeightOfMeasuringPoint = (TextBox)row.FindControl("txtHeightOfMeasuringPoint");
                    //TextBox txtInstallDate = (TextBox)row.FindControl("txtInstallDate");

                    long lng_ApplicationCode = Convert.ToInt64(gvPiezometer.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                    int int_PiezometerCode = Convert.ToInt32(gvPiezometer.DataKeys[e.RowIndex].Values["PiezometerCode"]);
                    //int int_SN = Convert.ToInt32(gvPiezometer.DataKeys[e.RowIndex].Values["SN"]);
                    PiezometerDetail obj_PiezometerDetail = new PiezometerDetail(lng_ApplicationCode, int_PiezometerCode);

                    if(txtPiezometerLocation.Text!=null)
                    {
                        obj_PiezometerDetail.Location= txtPiezometerLocation.Text;
                    }
                    if (txtPiezometerLatitude.Text != null)
                    {
                        obj_PiezometerDetail.Latitude = Convert.ToDecimal(txtPiezometerLatitude.Text);
                    }
                    if (txtPiezometerLongitude.Text != null)
                    {
                        obj_PiezometerDetail.Longitude = Convert.ToDecimal(txtPiezometerLongitude.Text);
                    }
                    if (txtDepthOfPiezometer.Text != null)
                    {
                        obj_PiezometerDetail.DepthofPiezo = Convert.ToDecimal(txtDepthOfPiezometer.Text);
                    }
                    if (txtHeightOfMeasuringPoint.Text != null)
                    {
                        obj_PiezometerDetail.HeightofMeasuring = Convert.ToDecimal(txtHeightOfMeasuringPoint.Text);
                    }
                    //if (txtInstallDate.Text != null)
                    //{
                    //    obj_PiezometerDetail.InstallDate = Convert.ToDateTime(txtInstallDate.Text);
                    //}                   

                    if (obj_PiezometerDetail.Update() == 1)
                    {
                        
                        lblMessage.Text = HttpUtility.HtmlEncode(obj_PiezometerDetail.CustumMessage);
                        lblMessage.ForeColor = System.Drawing.Color.Blue;
                        gvPiezometer.EditIndex = -1;
                        BindPiezometerDetailGrid(Convert.ToInt64(lblApplicationCodeFrom.Text));
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Record Update Failed');", true);                       

                    }
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
    }
    protected void gvPiezometer_Sorting(object sender, GridViewSortEventArgs e)
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
                gvPiezometer.EditIndex = -1;
                BindPiezometerDetailGrid(Convert.ToInt64(lblApplicationCodeFrom.Text));
                lblMessage.Text = "";
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false); 
            }
        }
    }
    protected void gvPiezometer_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
                gvPiezometer.EditIndex = -1;
                gvPiezometer.PageIndex = e.NewPageIndex;
                BindPiezometerDetailGrid(Convert.ToInt64(lblApplicationCodeFrom.Text));
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);               
            }
        }
    }
    protected void gvPiezometer_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                if (!NOCAPExternalUtility.IsNumeric(gvPiezometer.DataKeys[e.RowIndex].Values["ApplicationCode"]))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('ApplicationCode allows only Numeric');", true);
                    return;
                }
                if (!NOCAPExternalUtility.IsNumeric(gvPiezometer.DataKeys[e.RowIndex].Values["PiezometerCode"]))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('PiezometerCode allows only Numeric');", true);
                    return;
                }
                long lng_ApplicationCode = Convert.ToInt32(gvPiezometer.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_PiezometerCode = Convert.ToInt32(gvPiezometer.DataKeys[e.RowIndex].Values["PiezometerCode"]);
                //int int_SN = Convert.ToInt32(gvPiezometer.DataKeys[e.RowIndex].Values["SN"]);
                PiezometerDetail obj_deletePiezometerDetail = new PiezometerDetail(lng_ApplicationCode, int_PiezometerCode);
                if (obj_deletePiezometerDetail.Delete() == 1)
                {
                    lblMessage.Text = HttpUtility.HtmlEncode(obj_deletePiezometerDetail.CustumMessage);
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    BindPiezometerDetailGrid(Convert.ToInt64(lblApplicationCodeFrom.Text.Trim()));
                }
                else
                {
                    lblMessage.Text = HttpUtility.HtmlEncode(obj_deletePiezometerDetail.CustumMessage);
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);                
            }
        }
    }  
    protected void gvPiezometer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType== DataControlRowType.DataRow)
        {
            if((e.Row.RowState & DataControlRowState.Edit)>0)
            {
                RegularExpressionValidator txtPiezometerLatitude = (e.Row.FindControl("revtxtPiezometerLatitude") as RegularExpressionValidator);
                RegularExpressionValidator txtPiezometerLongitude = (e.Row.FindControl("revtxtPiezometerLongitude") as RegularExpressionValidator);
                RegularExpressionValidator txtDepthOfPiezometer = (e.Row.FindControl("revtxtDepthOfPiezometer") as RegularExpressionValidator);
                RegularExpressionValidator txtHeightOfMeasuringPoint = (e.Row.FindControl("revtxtHeightOfMeasuringPoint") as RegularExpressionValidator);


                txtPiezometerLatitude.ValidationExpression = ValidationUtility.txtValForDecimalValue("4", "6");
                txtPiezometerLatitude.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("4", "6");

                txtPiezometerLongitude.ValidationExpression = ValidationUtility.txtValForDecimalValue("4", "6");
                txtPiezometerLongitude.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("4", "6");

                txtDepthOfPiezometer.ValidationExpression = ValidationUtility.txtValForDecimalValue("3", "2");
                txtDepthOfPiezometer.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("3", "2");

                txtHeightOfMeasuringPoint.ValidationExpression = ValidationUtility.txtValForDecimalValue("3", "2");
                txtHeightOfMeasuringPoint.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("3", "2");              


            }

        }    

    }

    protected void lbtnHistory_Click(object sender, CommandEventArgs e)
    {
        //if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        //{
        //    Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        //}
        //else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {              

                if (e.CommandArgument != null)
                {
                    lblPiezoCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());  //added html encode lblApplicationCode.Text = e.CommandArgument.ToString();
                   Server.Transfer("~/ExternalUser/Piezometer/PiezometerHistory.aspx");
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
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRF.Value;

            }
        }
    }
    
}
