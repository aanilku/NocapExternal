using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.Mining.Renew.SADApplication;
using System.Threading;


public partial class ExternalUser_MiningRenew_ComplianceConditionNOC : System.Web.UI.Page
{
    //string strPageName = "MINRenewComplianceConditionNOC";
    //string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblMiningApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                        }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    BindComplianceConditionNOCDetails();
                }

                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
                NOCAP.BLL.Mining.New.Application.MiningNewApplication objMiningNewApplication = obj_MiningRenewApplication.GetFirstMiningApplication();

                if (objMiningNewApplication != null)
                {
                    lblHeadingProjName.Text = HttpUtility.HtmlEncode(objMiningNewApplication.NameOfMining);
                }

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
                    if (UpdateComplianceConditionNOC() == 1)
                    {
                        lblMessage.Text = "Successfully Saved";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMessage.Text = "Error on Page";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
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
                        Server.Transfer("~/ExternalUser/MiningRenew/ComplianceConditionNOCOther.aspx");
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

            List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOC> lst_miningRenewSADComplianceConditionNOCList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOC>();
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewSADApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
            foreach (GridViewRow gvRow in gvCompCondNOC.Rows)
            {
                DropDownList ddlCompCondApplicable = (DropDownList)gvRow.FindControl("ddlCompCondApplicable");
                TextBox txtStatusOfCompliance = (TextBox)gvRow.FindControl("txtStatusOfCompliance");

                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOC obj_MiningRenewSADCompCondNOC = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOC();

                obj_MiningRenewSADCompCondNOC.ComplianceConditionCode = Convert.ToInt32(gvCompCondNOC.DataKeys[gvRow.RowIndex].Value.ToString());

                switch (ddlCompCondApplicable.SelectedValue)
                {
                    case "":
                        obj_MiningRenewSADCompCondNOC.ComplianceConditionsApplicable = NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOC.CompCondApplicable.NotApplicable;
                        break;
                    case "Y":
                        obj_MiningRenewSADCompCondNOC.ComplianceConditionsApplicable = NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOC.CompCondApplicable.Yes;
                        break;
                    case "N":
                        obj_MiningRenewSADCompCondNOC.ComplianceConditionsApplicable = NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOC.CompCondApplicable.No;
                        break;
                }
                obj_MiningRenewSADCompCondNOC.StatusOfCompliance = txtStatusOfCompliance.Text;

                lst_miningRenewSADComplianceConditionNOCList.Add(obj_MiningRenewSADCompCondNOC);
            }
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOC[] arr_tempMiningRenewSADCompCondNOCListBLL = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOC[lst_miningRenewSADComplianceConditionNOCList.Count];
            lst_miningRenewSADComplianceConditionNOCList.CopyTo(arr_tempMiningRenewSADCompCondNOCListBLL);
            obj_miningRenewSADApplication.MiningRenewSADComplianceConditionNOCList = arr_tempMiningRenewSADCompCondNOCListBLL;

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_miningRenewSADApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_miningRenewSADApplication.Update() == 1)
            {
                strStatus = "Update Success";
                lblMessage.Text = "Successfully Saved";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                strStatus = "Update Failed";
                lblMessage.Text = HttpUtility.HtmlEncode(obj_miningRenewSADApplication.CustumMessage);
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

            MiningRenewSADComplianceConditionNOC obj_miningRenewSADComplianceConditionNOC = new MiningRenewSADComplianceConditionNOC(Convert.ToInt64(lblMiningApplicationCodeFrom.Text), int_CompCondNOCCode);

            if (obj_miningRenewSADComplianceConditionNOC != null && obj_miningRenewSADComplianceConditionNOC.ComplianceConditionCode > 0)
            {
                TextBox txtStatusOfCompliance = (TextBox)e.Row.FindControl("txtStatusOfCompliance");
                if (txtStatusOfCompliance != null)
                    txtStatusOfCompliance.Text = obj_miningRenewSADComplianceConditionNOC.StatusOfCompliance;

                DropDownList ddlCompCondApplicable = (DropDownList)e.Row.FindControl("ddlCompCondApplicable");
                if (ddlCompCondApplicable != null)
                {

                    switch (obj_miningRenewSADComplianceConditionNOC.ComplianceConditionsApplicable)
                    {
                        case NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOC.CompCondApplicable.NotApplicable:
                            ddlCompCondApplicable.SelectedValue = "A";
                            break;
                        case NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOC.CompCondApplicable.Yes:
                            ddlCompCondApplicable.SelectedValue = "Y";
                            break;
                        case NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADComplianceConditionNOC.CompCondApplicable.No:
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