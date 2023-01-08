using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.Industrial.Renew.SADApplication;
using System.Threading;
public partial class ExternalUser_IndustrialRenew_ComplianceConditionNOC : System.Web.UI.Page
{
    //string strPageName = "INDRenewComplianceConditionNOC";
    //string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // ValidationExpInit();
            try
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;                 

                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblModeFrom");
                        if (SourceLabel != null)
                        {
                            lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                        }
                    }

                }
                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblIndustialApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                        }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                     BindComplianceConditionNOCDetails();
                       
                }

                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_IndustrialRenewApplication.NameOfIndustry);
            }
            catch (Exception)
            {               
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    void BindComplianceConditionNOCDetails()
    { 

        try
        {
            NOCAP.BLL.Master.ComplianceConditionNOC obj_ComplianceConditionNOC = new NOCAP.BLL.Master.ComplianceConditionNOC();
            NOCAP.BLL.Master.ComplianceConditionNOC obj_ComplianceConditionNOCBlank = new NOCAP.BLL.Master.ComplianceConditionNOC();

            List<NOCAP.BLL.Master.ComplianceConditionNOC> list_ComplianceConditionNOC = new List<NOCAP.BLL.Master.ComplianceConditionNOC>();
           
            int int_status = 0;         

            int_status = obj_ComplianceConditionNOC.GetList(NOCAP.BLL.Master.ComplianceConditionNOC.VisibilityYesNo.NotDefined, NOCAP.BLL.Master.ComplianceConditionNOC.SortingField.ComplianceConditionDescription);          
          
            NOCAP.BLL.Master.ComplianceConditionNOC[] arr_ComplianceConditionNOC;
            arr_ComplianceConditionNOC = obj_ComplianceConditionNOC.ComplianceConditionNOCCollection;

            if ((int_status == 1))
            {
                if (arr_ComplianceConditionNOC.Count() > 0)
                {
                    gvCompCondNOC.DataSource = arr_ComplianceConditionNOC;
                    gvCompCondNOC.DataBind();
                }
                else
                {
                    list_ComplianceConditionNOC.Add(obj_ComplianceConditionNOCBlank);
                    gvCompCondNOC.DataSource = list_ComplianceConditionNOC;
                    gvCompCondNOC.DataBind();
                    int int_NoOfCol = 0;
                    int_NoOfCol = gvCompCondNOC.Rows[0].Cells.Count;
                    gvCompCondNOC.Rows[0].Cells.Clear();
                    gvCompCondNOC.Rows[0].Cells.Add(new TableCell());
                    gvCompCondNOC.Rows[0].Cells[0].ColumnSpan = int_NoOfCol;
                    gvCompCondNOC.Rows[0].Cells[0].Text = "Records do not exist";
                }
            }
            else
            {
                lblMessage.Text = HttpUtility.HtmlEncode(obj_ComplianceConditionNOC.CustumMessage);
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);          
        }
        
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
            Server.Transfer("DetailsAdditionalGroundwaterAbstractionStructure.aspx");
        }
    }
    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
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
                    UpdateComplianceConditionNOC();
                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
                }
            }
        }

    }
    protected void txtNext_Click(object sender, EventArgs e)
    {

        if (Page.IsValid)
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
                    if (UpdateComplianceConditionNOC()== 1)
                    {
                        Server.Transfer("~/ExternalUser/IndustrialRenew/ComplianceConditionNOCOther.aspx");
                    }
                }
                catch (ThreadAbortException)
                {

                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
                }
            }
        }


    }

    int UpdateComplianceConditionNOC()
    {
        try
        {
            //strActionName = "UpdateComplianceConditionNOC";
            List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOC> lst_industrialRenewSADComplianceConditionNOCList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOC>();
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewSADApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
            foreach (GridViewRow gvRow in gvCompCondNOC.Rows)
            {
                // Label lblCompCondCode = (Label)gvRow.FindControl("lblCompCondCode");
                DropDownList ddlCompCondApplicable = (DropDownList)gvRow.FindControl("ddlCompCondApplicable");
                TextBox txtStatusOfCompliance = (TextBox)gvRow.FindControl("txtStatusOfCompliance");

                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOC obj_IndustrialRenewSADCompCondNOC = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOC();

                obj_IndustrialRenewSADCompCondNOC.ComplianceConditionCode = Convert.ToInt32(gvCompCondNOC.DataKeys[gvRow.RowIndex].Value.ToString());

                switch (ddlCompCondApplicable.SelectedValue)
                {
                    case "":
                        obj_IndustrialRenewSADCompCondNOC.ComplianceConditionsApplicable = NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOC.CompCondApplicable.NotApplicable;
                        break;
                    case "Y":
                        obj_IndustrialRenewSADCompCondNOC.ComplianceConditionsApplicable = NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOC.CompCondApplicable.Yes;
                        break;
                    case "N":
                        obj_IndustrialRenewSADCompCondNOC.ComplianceConditionsApplicable = NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOC.CompCondApplicable.No;
                        break;
                }
                obj_IndustrialRenewSADCompCondNOC.StatusOfCompliance = txtStatusOfCompliance.Text;

                lst_industrialRenewSADComplianceConditionNOCList.Add(obj_IndustrialRenewSADCompCondNOC);
            }
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOC[] arr_tempIndustrialRenewSADCompCondNOCListBLL = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOC[lst_industrialRenewSADComplianceConditionNOCList.Count];
            lst_industrialRenewSADComplianceConditionNOCList.CopyTo(arr_tempIndustrialRenewSADCompCondNOCListBLL);
            obj_industrialRenewSADApplication.IndustrialRenewSADComplianceConditionNOCList = arr_tempIndustrialRenewSADCompCondNOCListBLL;
            //obj_industrialRenewSADApplication.IndustrialRenewApplicationCode = Convert.ToInt64(lblIndustialApplicationCodeFrom.Text);

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_industrialRenewSADApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_industrialRenewSADApplication.Update() == 1)
            {
                strStatus = "Update Success";
                lblMessage.Text = "Successfully Saved";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                strStatus = "Update Failed";
                lblMessage.Text = HttpUtility.HtmlEncode(obj_industrialRenewSADApplication.CustumMessage);
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
            return 0;
        }
    }
     
    protected void gvCompCondNOC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //int int_IndustrialRenewApplicationCode = 0;
        int int_CompCondNOCCode = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            RegularExpressionValidator revtxtStatusOfCompliance = (RegularExpressionValidator)e.Row.FindControl("revtxtStatusOfCompliance");
            RegularExpressionValidator revLengthtxtStatusOfCompliance = (RegularExpressionValidator)e.Row.FindControl("revLengthtxtStatusOfCompliance");

            revtxtStatusOfCompliance.ValidationExpression = ValidationUtility.txtValNAMultiLine;
            revtxtStatusOfCompliance.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;
            revLengthtxtStatusOfCompliance.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
            revLengthtxtStatusOfCompliance.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");

             


            if (!NOCAPExternalUtility.IsNumeric(gvCompCondNOC.DataKeys[e.Row.DataItemIndex].Values["ComplianceConditionCode"]))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Compliance ConditionNOC Code allows only Numeric');", true);
                return;
            }
            int_CompCondNOCCode = Convert.ToInt32(gvCompCondNOC.DataKeys[e.Row.DataItemIndex].Values["ComplianceConditionCode"]);

            IndustrialRenewSADComplianceConditionNOC obj_industrialRenewSADComplianceConditionNOC = new IndustrialRenewSADComplianceConditionNOC(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text), int_CompCondNOCCode);
           // NOCAP.BLL.Master.ComplianceConditionNOC obj_ComplianceConditionNOC = new NOCAP.BLL.Master.ComplianceConditionNOC(int_ComplianceConditionNOCCode);

            if (obj_industrialRenewSADComplianceConditionNOC != null && obj_industrialRenewSADComplianceConditionNOC.ComplianceConditionCode > 0)
            {
                TextBox txtStatusOfCompliance = (TextBox)e.Row.FindControl("txtStatusOfCompliance");
                if (txtStatusOfCompliance != null)
                    txtStatusOfCompliance.Text = obj_industrialRenewSADComplianceConditionNOC.StatusOfCompliance;

                DropDownList ddlCompCondApplicable = (DropDownList)e.Row.FindControl("ddlCompCondApplicable");
                if (ddlCompCondApplicable != null)
                {

                    switch (obj_industrialRenewSADComplianceConditionNOC.ComplianceConditionsApplicable)
                    {
                        case NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOC.CompCondApplicable.NotApplicable:
                            ddlCompCondApplicable.SelectedValue = "A";
                            break;
                        case NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOC.CompCondApplicable.Yes:
                            ddlCompCondApplicable.SelectedValue = "Y";
                            break;
                        case NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADComplianceConditionNOC.CompCondApplicable.No:
                            ddlCompCondApplicable.SelectedValue = "N";
                            break;
                    }
                }
            }
            else
            {
                DropDownList ddlCompCondApplicable = (DropDownList)e.Row.FindControl("ddlCompCondApplicable");
                if (ddlCompCondApplicable != null)
                    ddlCompCondApplicable.SelectedIndex = -1;
                 TextBox txtStatusOfCompliance = (TextBox)e.Row.FindControl("txtStatusOfCompliance");
                 if (txtStatusOfCompliance != null)
                        txtStatusOfCompliance.Text = "";

            }
             
        }

    }

   
}