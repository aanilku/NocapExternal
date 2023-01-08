using NOCAP.BLL.Misc.Piezometer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExternalUser_Piezometer_PiezometerHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ValidationExpInit();
            txtDate_CalendarExtender.EndDate = System.DateTime.Now;
            txtDate.Attributes.Add("readonly", "readonly");       

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

                    Label lblPiezoCode = (Label)placeHolder.FindControl("lblPiezoCode");
                    if (lblPiezoCode != null && lblPiezoCode.Text != "")
                        lblPiezoCodecur.Text = HttpUtility.HtmlEncode(lblPiezoCode.Text);
                      BindPiezometerReadingGrid(Convert.ToInt64(lblApplicationCodeFrom.Text.Trim()),Convert.ToInt32(lblPiezoCodecur.Text));
                }
            }
        }
    }

    private void ValidationExpInit()
    {
        revtxtWaterLevel.ValidationExpression = ValidationUtility.txtValForDecimalValue("3", "2");
        revtxtWaterLevel.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("3", "2");              

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
    void BindPiezometerReadingGrid(long AppCode , int PiezometerCode)
    {
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null;
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = null;

        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = null;

        NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_InfrastructureNewApplication, out obj_MiningNewApplication, AppCode);

        if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.NameOfIndustry != "")
        {
            lblName.Text = obj_IndustrialNewApplication.NameOfIndustry;
            lblNo.Text = obj_IndustrialNewApplication.IndustrialNewApplicationNumber;
        }
        if (obj_MiningNewApplication != null && obj_MiningNewApplication.NameOfMining != "")
        {
            lblName.Text = obj_MiningNewApplication.NameOfMining;
            lblNo.Text = obj_MiningNewApplication.MiningNewApplicationNumber;
        }
        if (obj_InfrastructureNewApplication != null && obj_InfrastructureNewApplication.NameOfInfrastructure != "")
        {
            lblName.Text = obj_InfrastructureNewApplication.NameOfInfrastructure;
            lblNo.Text = obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber;
        }
        NOCAP.BLL.Misc.Piezometer.PiezometerReadings obj_PiezometerDetail1 = new NOCAP.BLL.Misc.Piezometer.PiezometerReadings();
        obj_PiezometerDetail1.ApplicationCode = AppCode;
        obj_PiezometerDetail1.PiezometerCode = PiezometerCode;
        gvPiezometer.DataSource = obj_PiezometerDetail1.GetPiezometerReadingList();
        gvPiezometer.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
             PiezometerReadings obj_PiezometerReadings = new PiezometerReadings();
             PiezometerDetail obj_PiezometerDetail = new PiezometerDetail(Convert.ToInt64(lblApplicationCodeFrom.Text), Convert.ToInt32(lblPiezoCodecur.Text));

             obj_PiezometerReadings.ApplicationCode = Convert.ToInt64(lblApplicationCodeFrom.Text);  //take from previous
             obj_PiezometerReadings.PiezometerCode = Convert.ToInt32(lblPiezoCodecur.Text);
             if (obj_PiezometerDetail.InstallDate < Convert.ToDateTime(txtDate.Text))
             {
                obj_PiezometerReadings.Date = txtDate.Text;
             }            
             else
             {            
                 lblMessage.Text = "Piezometer reading not earliar than install date";
                 return;            
             }
           // obj_PiezometerReadings.Date = txtDate.Text;
            //string Date = txtDate.Text;
            //string[] splitedDate = Date.Split('/');
            ////Save this in MonthDay field
            //string month = string.Join("/", splitedDate[0]);
            ////Save this in Year field
            //string year = splitedDate[1];
         //  PiezometerDetail obj_PiezometerDetail = new PiezometerDetail(Convert.ToInt64(lblApplicationCodeFrom.Text), Convert.ToInt32(lblPiezoCodecur.Text));

           if (obj_PiezometerDetail.DepthofPiezo > Convert.ToDecimal(txtWaterLevel.Text))
           {
            obj_PiezometerReadings.WaterLevel = Convert.ToDecimal(txtWaterLevel.Text);
           }
           else
           {
            lblMessage.Text = "WaterLevel Can not be greater than depth of piezometer";
            return;
          }
            obj_PiezometerReadings.CreatedByExUC = Convert.ToInt64(Session["ExternalUserCode"]);
            int int_result = obj_PiezometerReadings.Add();
            if (int_result == 1)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = obj_PiezometerReadings.CustumMessage;
                ControlClear();
                BindPiezometerReadingGrid(Convert.ToInt64(lblApplicationCodeFrom.Text) ,Convert.ToInt32(lblPiezoCodecur.Text));
            }
            else
            {
                lblMessage.Text = obj_PiezometerReadings.CustumMessage;
            }     
         }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        ControlClear();
    }
    void ControlClear()
    {
        txtDate.Text = "";
        txtWaterLevel.Text = "";       

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ExternalUser/ApplicantHome.aspx", false);
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            Server.Transfer("PiezometerDetail.aspx");

        }
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
                BindPiezometerReadingGrid(Convert.ToInt64(lblApplicationCodeFrom.Text), Convert.ToInt32(lblPiezoCodecur.Text));
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
                string str_sN = Convert.ToString(gvPiezometer.DataKeys[e.NewEditIndex].Values["SN"]);

                gvPiezometer.EditIndex = -1;
                BindPiezometerReadingGrid(Convert.ToInt64(lblApplicationCodeFrom.Text), Convert.ToInt32(lblPiezoCodecur.Text));

                DataKey key = default(DataKey);
                foreach (GridViewRow row in gvPiezometer.Rows)
                {
                    string str_tempapplicationCode = null;
                    string str_temppiezometerCode = null;
                    string str_tempsN = null;

                    int_EditIndexNo = row.RowIndex;
                    key = gvPiezometer.DataKeys[int_EditIndexNo];
                    str_tempapplicationCode = Convert.ToString(key.Values["ApplicationCode"]);
                    str_temppiezometerCode = Convert.ToString(key.Values["PiezometerCode"]);
                    str_tempsN = Convert.ToString(key.Values["SN"]);

                    if (str_tempapplicationCode == str_applicationCode && str_temppiezometerCode == str_piezometerCode && str_tempsN == str_sN)
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

                    TextBox txtDate = (TextBox)row.FindControl("txtDate");
                    TextBox txtWaterLevel = (TextBox)row.FindControl("txtWaterLevel");
                    

                    long lng_ApplicationCode = Convert.ToInt64(gvPiezometer.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                    int int_PiezometerCode = Convert.ToInt32(gvPiezometer.DataKeys[e.RowIndex].Values["PiezometerCode"]);
                    int int_sN = Convert.ToInt32(gvPiezometer.DataKeys[e.RowIndex].Values["SN"]);
                    PiezometerReadings obj_PiezometerReading = new PiezometerReadings(lng_ApplicationCode, int_PiezometerCode ,int_sN);
                    PiezometerDetail obj_PiezometerDetail = new PiezometerDetail(Convert.ToInt64(lblApplicationCodeFrom.Text), Convert.ToInt32(lblPiezoCodecur.Text));

                    if (txtDate.Text != null)
                    {
                        obj_PiezometerReading.Date = txtDate.Text;
                    }

                    if (obj_PiezometerDetail.DepthofPiezo > Convert.ToDecimal(txtWaterLevel.Text))
                    {
                        obj_PiezometerReading.WaterLevel = Convert.ToDecimal(txtWaterLevel.Text);
                    }
                    else
                    {
                        lblMessage.Text = "WaterLevel Can not be greater than depth of piezometer";
                        return;
                    }

                    //if (txtWaterLevel.Text != null)
                    //{
                    //    obj_PiezometerReading.WaterLevel = Convert.ToDecimal(txtWaterLevel.Text);
                    //}


                    if (obj_PiezometerReading.Update() == 1)
                    {

                        lblMessage.Text = HttpUtility.HtmlEncode(obj_PiezometerReading.CustumMessage);
                        lblMessage.ForeColor = System.Drawing.Color.Blue;
                        gvPiezometer.EditIndex = -1;
                        BindPiezometerReadingGrid(Convert.ToInt64(lblApplicationCodeFrom.Text), Convert.ToInt32(lblPiezoCodecur.Text));
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
                BindPiezometerReadingGrid(Convert.ToInt64(lblApplicationCodeFrom.Text), Convert.ToInt32(lblPiezoCodecur.Text));
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
                BindPiezometerReadingGrid(Convert.ToInt64(lblApplicationCodeFrom.Text), Convert.ToInt32(lblPiezoCodecur.Text));
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }  
    protected void gvPiezometer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                RegularExpressionValidator txtWaterLevel = (e.Row.FindControl("revtxtWaterLevel") as RegularExpressionValidator);

                txtWaterLevel.ValidationExpression = ValidationUtility.txtValForDecimalValue("3", "2");
                txtWaterLevel.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("3", "2");             

                
            }

        }

    }
}