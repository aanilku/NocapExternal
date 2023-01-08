using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.Infrastructure.Renew.SADApplication;
using System.Threading;

public partial class ExternalUser_InfrastructureRenew_ComplianceConditionNOC : System.Web.UI.Page
{
    //string strPageName = "INFRenewComplianceConditionNOC";
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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblInfrastructureApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                        }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    BindComplianceConditionNOCDetails();

                }

                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewApplication.NameOfInfrastructure);
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
                    if (UpdateComplianceConditionNOC() == 1)
                    {
                        Server.Transfer("~/ExternalUser/InfrastructureRenew/ComplianceConditionNOCOther.aspx");
                        //Response.Redirect("~/ExternalUser/InfrastructureRenew/ComplianceConditionNOCOther.aspx");
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
            List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOC> lst_InfrastructureRenewSADComplianceConditionNOCList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOC>();
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewSADApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
            foreach (GridViewRow gvRow in gvCompCondNOC.Rows)
            {
                // Label lblCompCondCode = (Label)gvRow.FindControl("lblCompCondCode");
                DropDownList ddlCompCondApplicable = (DropDownList)gvRow.FindControl("ddlCompCondApplicable");
                TextBox txtStatusOfCompliance = (TextBox)gvRow.FindControl("txtStatusOfCompliance");

                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOC obj_InfrastructureRenewSADCompCondNOC = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOC();

                obj_InfrastructureRenewSADCompCondNOC.ComplianceConditionCode = Convert.ToInt32(gvCompCondNOC.DataKeys[gvRow.RowIndex].Value.ToString());

                switch (ddlCompCondApplicable.SelectedValue)
                {
                    case "":
                        obj_InfrastructureRenewSADCompCondNOC.ComplianceConditionsApplicable = NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOC.CompCondApplicable.NotApplicable;
                        break;
                    case "Y":
                        obj_InfrastructureRenewSADCompCondNOC.ComplianceConditionsApplicable = NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOC.CompCondApplicable.Yes;
                        break;
                    case "N":
                        obj_InfrastructureRenewSADCompCondNOC.ComplianceConditionsApplicable = NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOC.CompCondApplicable.No;
                        break;
                }
                obj_InfrastructureRenewSADCompCondNOC.StatusOfCompliance = txtStatusOfCompliance.Text;

                lst_InfrastructureRenewSADComplianceConditionNOCList.Add(obj_InfrastructureRenewSADCompCondNOC);
            }
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOC[] arr_tempInfrastructureRenewSADCompCondNOCListBLL = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOC[lst_InfrastructureRenewSADComplianceConditionNOCList.Count];
            lst_InfrastructureRenewSADComplianceConditionNOCList.CopyTo(arr_tempInfrastructureRenewSADCompCondNOCListBLL);
            obj_InfrastructureRenewSADApplication.InfrastructureRenewSADComplianceConditionNOCList = arr_tempInfrastructureRenewSADCompCondNOCListBLL;
            //obj_InfrastructureRenewSADApplication.InfrastructureRenewApplicationCode = Convert.ToInt64(lblIndustialApplicationCodeFrom.Text);

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_InfrastructureRenewSADApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_InfrastructureRenewSADApplication.Update() == 1)
            {
                strStatus = "Update Success";
                lblMessage.Text = "Successfully Saved";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                strStatus = "Update Failed";
                lblMessage.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewSADApplication.CustumMessage);
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
        //int int_InfrastructureRenewApplicationCode = 0;
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

            InfrastructureRenewSADComplianceConditionNOC obj_InfrastructureRenewSADComplianceConditionNOC = new InfrastructureRenewSADComplianceConditionNOC(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text), int_CompCondNOCCode);
            // NOCAP.BLL.Master.ComplianceConditionNOC obj_ComplianceConditionNOC = new NOCAP.BLL.Master.ComplianceConditionNOC(int_ComplianceConditionNOCCode);

            if (obj_InfrastructureRenewSADComplianceConditionNOC != null && obj_InfrastructureRenewSADComplianceConditionNOC.ComplianceConditionCode > 0)
            {
                TextBox txtStatusOfCompliance = (TextBox)e.Row.FindControl("txtStatusOfCompliance");
                if (txtStatusOfCompliance != null)
                    txtStatusOfCompliance.Text = obj_InfrastructureRenewSADComplianceConditionNOC.StatusOfCompliance;

                DropDownList ddlCompCondApplicable = (DropDownList)e.Row.FindControl("ddlCompCondApplicable");
                if (ddlCompCondApplicable != null)
                {

                    switch (obj_InfrastructureRenewSADComplianceConditionNOC.ComplianceConditionsApplicable)
                    {
                        case NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOC.CompCondApplicable.NotApplicable:
                            ddlCompCondApplicable.SelectedValue = "A";
                            break;
                        case NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOC.CompCondApplicable.Yes:
                            ddlCompCondApplicable.SelectedValue = "Y";
                            break;
                        case NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOC.CompCondApplicable.No:
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