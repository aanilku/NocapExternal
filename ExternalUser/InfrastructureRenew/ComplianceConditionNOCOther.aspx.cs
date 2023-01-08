using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.Infrastructure.Renew.SADApplication;

public partial class ExternalUser_InfrastructureRenew_ComplianceConditionNOCOther : System.Web.UI.Page
{
    //string strPageName = "INFRenewComplianceConditionNOCOther";
    string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessageAdd.Text = "";
        lblMessage.Text = "";
        if (!IsPostBack)
        {
            ValidationExpInit();
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
                    BindComplianceConditionNOCOtherDetails();
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

    void ValidationExpInit()
    {
        revtxtComplianceConditionEnterOther.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtComplianceConditionEnterOther.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;
        revtxtStatusofComplianceOther.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtStatusofComplianceOther.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtComplianceConditionEnterOther.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        revLengthtxtComplianceConditionEnterOther.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");
        revLengthtxtStatusofComplianceOther.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        revLengthtxtStatusofComplianceOther.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");




    }
    void BindComplianceConditionNOCOtherDetails()
    {
        try
        {
            InfrastructureRenewSADComplianceConditionNOCOther obj_InfrastructureRenewSADComplianceConditionNOCOther = new InfrastructureRenewSADComplianceConditionNOCOther();
            int int_status = 0;
            if (lblInfrastructureApplicationCodeFrom.Text != "" && NOCAPExternalUtility.IsNumeric(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text)))
            {
                obj_InfrastructureRenewSADComplianceConditionNOCOther.InfrastructureRenewApplicationCode = Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text);

                int_status = obj_InfrastructureRenewSADComplianceConditionNOCOther.GetList(InfrastructureRenewSADComplianceConditionNOCOther.SortingField.NoSorting);

                InfrastructureRenewSADComplianceConditionNOCOther[] arr_InfrastructureRenewSADComplianceConditionNOCOther;
                arr_InfrastructureRenewSADComplianceConditionNOCOther = obj_InfrastructureRenewSADComplianceConditionNOCOther.InfrastructureRenewSADComplianceConditionNOCOtherCollection;

                if ((int_status == 1))
                {
                    if (arr_InfrastructureRenewSADComplianceConditionNOCOther.Count() > 0)
                    {
                        gvCompCondNOCOther.DataSource = arr_InfrastructureRenewSADComplianceConditionNOCOther;
                        gvCompCondNOCOther.DataBind();
                    }
                }
                else
                {
                    lblMessage.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewSADComplianceConditionNOCOther.CustumMessage);
                }
            }
            else
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
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
            Server.Transfer("ComplianceConditionNOC.aspx");
        }
    }
    protected void txtNext_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/ExternalUser/InfrastructureRenew/OtherDetails.aspx");
    }

    protected void btnComConAddOther_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            try
            {

                InfrastructureRenewSADComplianceConditionNOCOther obj_InfrastructureRenewSADComplianceConditionNOCOther = new InfrastructureRenewSADComplianceConditionNOCOther();
                obj_InfrastructureRenewSADComplianceConditionNOCOther.InfrastructureRenewApplicationCode = Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text);
                obj_InfrastructureRenewSADComplianceConditionNOCOther.ComplianceConditionEnter = txtComplianceConditionEnterOther.Text;
                obj_InfrastructureRenewSADComplianceConditionNOCOther.StatusOfCompliance = txtStatusofComplianceOther.Text;

                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                obj_InfrastructureRenewSADComplianceConditionNOCOther.CreatedByExUC = obj_externalUser.ExternalUserCode;

                int int_result = 0;
                int_result = obj_InfrastructureRenewSADComplianceConditionNOCOther.Add();
                if (int_result == 1)
                {
                    txtComplianceConditionEnterOther.Text = "";
                    txtStatusofComplianceOther.Text = "";
                    BindComplianceConditionNOCOtherDetails();

                    lblMessageAdd.Text = "Successfully Saved";
                    lblMessageAdd.ForeColor = System.Drawing.Color.Green;
                }

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    protected void gvCompCondNOCOther_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
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
                gvCompCondNOCOther.EditIndex = -1;
                BindComplianceConditionNOCOtherDetails();

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }

    }

    protected void gvCompCondNOCOther_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            strActionName = "Delete";
            try
            {
                if (!NOCAPExternalUtility.IsNumeric(gvCompCondNOCOther.DataKeys[e.RowIndex].Values["InfrastructureRenewApplicationCode"]))
                {

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('InfrastructureRenewApplicationCode allows only Numeric');", true);
                    return;
                }
                if (!NOCAPExternalUtility.IsNumeric(gvCompCondNOCOther.DataKeys[e.RowIndex].Values["ComplianceConditionNOCSerialNumber"]))
                {

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('ComplianceConditionNOCSerialNumber allows only Numeric');", true);
                    return;
                }
                int int_IndRenewAppCode = Convert.ToInt32(gvCompCondNOCOther.DataKeys[e.RowIndex].Values["InfrastructureRenewApplicationCode"]);
                int int_CompCondNOCSN = Convert.ToInt32(gvCompCondNOCOther.DataKeys[e.RowIndex].Values["ComplianceConditionNOCSerialNumber"]);

                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOCOther obj_InfrastructureRenewSADComplianceConditionNOCOther = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOCOther(int_IndRenewAppCode, int_CompCondNOCSN);
                if (obj_InfrastructureRenewSADComplianceConditionNOCOther.Delete() == 1)
                {
                    strStatus = "Record Delete Successfully !";
                    lblMessage.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewSADComplianceConditionNOCOther.CustumMessage);
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    BindComplianceConditionNOCOtherDetails();
                }
                else
                {
                    strStatus = "Record Delete Failed !";
                    lblMessage.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewSADComplianceConditionNOCOther.CustumMessage);
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                strStatus = "Record Delete Failed !";
                Response.Redirect("~/ExternalErrorPage.aspx", false);

            }

        }
    }
    protected void gvCompCondNOCOther_RowEditing(object sender, GridViewEditEventArgs e)
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
                int int_InfrastructureRenewAppCode;
                int int_CompCondNOCSN;
                int int_FinalEditIndexNo = 0;
                int int_EditIndexNo = 0;



                lblMessage.Text = "";
                if (!NOCAPExternalUtility.IsNumeric(gvCompCondNOCOther.DataKeys[e.NewEditIndex].Values["InfrastructureRenewApplicationCode"]))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('InfrastructureRenewApplicationCode allows only Numeric');", true);
                    return;
                }
                int_InfrastructureRenewAppCode = Convert.ToInt32(gvCompCondNOCOther.DataKeys[e.NewEditIndex].Values["InfrastructureRenewApplicationCode"]);

                if (!NOCAPExternalUtility.IsNumeric(gvCompCondNOCOther.DataKeys[e.NewEditIndex].Values["ComplianceConditionNOCSerialNumber"]))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('ComplianceConditionNOCSerialNumber allows only Numeric');", true);
                    return;
                }
                int_CompCondNOCSN = Convert.ToInt32(gvCompCondNOCOther.DataKeys[e.NewEditIndex].Values["ComplianceConditionNOCSerialNumber"]);

                Label lblComplianceConditionEnter = (Label)gvCompCondNOCOther.Rows[e.NewEditIndex].FindControl("lblComplianceConditionEnter");
                Label lblStatusOfComplianceOther = (Label)gvCompCondNOCOther.Rows[e.NewEditIndex].FindControl("lblStatusOfComplianceOther");

                gvCompCondNOCOther.EditIndex = -1;
                BindComplianceConditionNOCOtherDetails();
                DataKey key = default(DataKey);
                foreach (GridViewRow row in gvCompCondNOCOther.Rows)
                {
                    string str_tempInfrastructureRenewAppCode = null;
                    string str_tempCompCondNOCSN = null;


                    int_EditIndexNo = row.RowIndex;
                    key = gvCompCondNOCOther.DataKeys[int_EditIndexNo];
                    str_tempInfrastructureRenewAppCode = Convert.ToString(key.Values["InfrastructureRenewApplicationCode"]);
                    str_tempCompCondNOCSN = Convert.ToString(key.Values["ComplianceConditionNOCSerialNumber"]);

                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOCOther obj_InfrastructureRenewSADComplianceConditionNOCOther = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADComplianceConditionNOCOther(int_InfrastructureRenewAppCode, int_CompCondNOCSN);
                    if (str_tempInfrastructureRenewAppCode == Convert.ToString(int_InfrastructureRenewAppCode) && str_tempCompCondNOCSN == Convert.ToString(int_CompCondNOCSN))
                    {
                        int_FinalEditIndexNo = row.RowIndex;
                    }
                }
                gvCompCondNOCOther.EditIndex = int_FinalEditIndexNo;
                gvCompCondNOCOther.DataBind();

                TextBox txtComplianceConditionEnter = (TextBox)gvCompCondNOCOther.Rows[e.NewEditIndex].FindControl("txtComplianceConditionEnter");
                if (txtComplianceConditionEnter != null && lblComplianceConditionEnter != null)
                {
                    txtComplianceConditionEnter.Text = lblComplianceConditionEnter.Text;
                }

                TextBox txtStatusOfComplianceOther = (TextBox)gvCompCondNOCOther.Rows[e.NewEditIndex].FindControl("txtStatusOfComplianceOther");
                if (txtStatusOfComplianceOther != null && lblStatusOfComplianceOther != null)
                {
                    txtStatusOfComplianceOther.Text = lblStatusOfComplianceOther.Text;
                }

                //// grid validation
                RegularExpressionValidator revtxtComplianceConditionEnter = (RegularExpressionValidator)gvCompCondNOCOther.Rows[e.NewEditIndex].FindControl("revtxtComplianceConditionEnter");
                if (revtxtComplianceConditionEnter != null)
                {
                    revtxtComplianceConditionEnter.ValidationExpression = ValidationUtility.txtValNAMultiLine;
                    revtxtComplianceConditionEnter.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;
                }
                RegularExpressionValidator revtxtStatusofComplianceOther = (RegularExpressionValidator)gvCompCondNOCOther.Rows[e.NewEditIndex].FindControl("revtxtStatusofComplianceOther");
                if (revtxtStatusofComplianceOther != null)
                {
                    revtxtStatusofComplianceOther.ValidationExpression = ValidationUtility.txtValNAMultiLine;
                    revtxtStatusofComplianceOther.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;
                }
                RegularExpressionValidator revLengthgrdtxtComplianceConditionEnter = (RegularExpressionValidator)gvCompCondNOCOther.Rows[e.NewEditIndex].FindControl("revLengthgrdtxtComplianceConditionEnter");
                if (revLengthgrdtxtComplianceConditionEnter != null)
                {
                    revLengthgrdtxtComplianceConditionEnter.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
                    revLengthgrdtxtComplianceConditionEnter.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");
                }
                RegularExpressionValidator revLengthgrdtxtStatusOfComplianceOther = (RegularExpressionValidator)gvCompCondNOCOther.Rows[e.NewEditIndex].FindControl("revLengthgrdtxtStatusOfComplianceOther");
                if (revLengthgrdtxtStatusOfComplianceOther != null)
                {
                    revLengthgrdtxtStatusOfComplianceOther.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
                    revLengthgrdtxtStatusOfComplianceOther.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");
                }




            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }

    }
    protected void gvCompCondNOCOther_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            strActionName = "Update";
            try
            {
                if (Page.IsValid)
                {
                    int int_IndRenewAppCode;
                    int int_CompCondNOCSN;


                    int index = gvCompCondNOCOther.EditIndex;
                    GridViewRow row = gvCompCondNOCOther.Rows[index];
                    if (!NOCAPExternalUtility.IsNumeric(gvCompCondNOCOther.DataKeys[e.RowIndex].Values["InfrastructureRenewApplicationCode"]))
                    {

                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('InfrastructureRenewApplicationCode allows only Numeric');", true);
                        return;
                    }
                    if (!NOCAPExternalUtility.IsNumeric(gvCompCondNOCOther.DataKeys[e.RowIndex].Values["ComplianceConditionNOCSerialNumber"]))
                    {

                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('ComplianceConditionNOCSerialNumber allows only Numeric');", true);
                        return;
                    }
                    int_IndRenewAppCode = Convert.ToInt32(gvCompCondNOCOther.DataKeys[e.RowIndex].Values["InfrastructureRenewApplicationCode"]);
                    int_CompCondNOCSN = Convert.ToInt32(gvCompCondNOCOther.DataKeys[e.RowIndex].Values["ComplianceConditionNOCSerialNumber"]);


                    InfrastructureRenewSADComplianceConditionNOCOther obj_InfrastructureRenewSADComplianceConditionNOCOther = new InfrastructureRenewSADComplianceConditionNOCOther(int_IndRenewAppCode, int_CompCondNOCSN);

                    TextBox txtComplianceConditionEnter = (TextBox)row.FindControl("txtComplianceConditionEnter");
                    if (txtComplianceConditionEnter != null)
                    {
                        obj_InfrastructureRenewSADComplianceConditionNOCOther.ComplianceConditionEnter = txtComplianceConditionEnter.Text;
                    }

                    TextBox txtStatusOfComplianceOther = (TextBox)row.FindControl("txtStatusOfComplianceOther");
                    if (txtStatusOfComplianceOther != null)
                    {
                        obj_InfrastructureRenewSADComplianceConditionNOCOther.StatusOfCompliance = txtStatusOfComplianceOther.Text;
                    }

                    NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                    obj_InfrastructureRenewSADComplianceConditionNOCOther.ModifiedByExUC = obj_externalUser.ExternalUserCode;


                    if (obj_InfrastructureRenewSADComplianceConditionNOCOther.Update() == 1)
                    {
                        strStatus = "Record Update Successfully !";
                        lblMessage.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewSADComplianceConditionNOCOther.CustumMessage);
                        lblMessage.ForeColor = System.Drawing.Color.Blue;
                        gvCompCondNOCOther.EditIndex = -1;
                        BindComplianceConditionNOCOtherDetails();


                    }
                    else
                    {
                        strStatus = "Record Update Failed !";
                        Label lbl_District = (Label)row.FindControl("lbl_DistrictName");
                        if (lbl_District != null)
                        {
                            lbl_District.Visible = true;
                            lbl_District.Text = "Record already exist!";
                            lbl_District.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            catch (Exception)
            {
                strStatus = "Record Update Failed !";
                Response.Redirect("~/ExternalErrorPage.aspx", false);

            }

        }
    }

    protected void gvCompCondNOCOther_RowDataBound(object sender, GridViewRowEventArgs e)
    {



    }
}