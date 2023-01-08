using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Threading;

public partial class ExternalUser_InfrastructureRenew_CommunicationAddress : System.Web.UI.Page
{
    string strPageName = "INFRenewCommunicationAddress";
    string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ValidationExpInit();
            try
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                if (NOCAPExternalUtility.FillDropDownState(ref ddlState) != 1)
                {
                    lblMessage.Text = "Problem in state population";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
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
                    BindCommunicationDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    private void ValidationExpInit()
    {
        revtxtAddressLine1.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtAddressLine1.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revtxtAddressLine2.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtAddressLine2.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revtxtAddressLine3.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtAddressLine3.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revtxtPinCode.ValidationExpression = ValidationUtility.txtValForPinCode;
        revtxtPinCode.ErrorMessage = ValidationUtility.txtValForPinCodeMsg;

        revtxtStdCode.ValidationExpression = ValidationUtility.txtValForNumericWithOutFirstCharacterZero;
        revtxtStdCode.ErrorMessage = ValidationUtility.txtValForNumericWithOutFirstCharacterZeroMsg;

        revtxtPhoneNumber.ValidationExpression = ValidationUtility.txtValForNumericWithOutFirstCharacterZero;
        revtxtPhoneNumber.ErrorMessage = ValidationUtility.txtValForNumericWithOutFirstCharacterZeroMsg;

        revtxtMobileNumber.ValidationExpression = ValidationUtility.txtValForMobileNumber;
        revtxtMobileNumber.ErrorMessage = ValidationUtility.txtValForMobileNumberMsg;

        revtxtStdCodeFax.ValidationExpression = ValidationUtility.txtValForNumericWithOutFirstCharacterZero;
        revtxtStdCodeFax.ErrorMessage = ValidationUtility.txtValForNumericWithOutFirstCharacterZeroMsg;

        revtxtFaxNumber.ValidationExpression = ValidationUtility.txtValForNumericWithOutFirstCharacterZero;
        revtxtFaxNumber.ErrorMessage = ValidationUtility.txtValForNumericWithOutFirstCharacterZeroMsg;

        revtxtEmail.ValidationExpression = ValidationUtility.txtValForEmail;
        revtxtEmail.ErrorMessage = ValidationUtility.txtValForEmailMsg;

    }

    private void BindCommunicationDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);
            if (obj_infrastructureRenewApplication != null && obj_infrastructureRenewApplication.InfrastructureRenewApplicationCode > 0)
            {

                lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewApplication.NameOfInfrastructure);

                txtAddressLine1.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewApplication.CommunicationAddress.AddressLine1);
                txtAddressLine2.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewApplication.CommunicationAddress.AddressLine2);
                txtAddressLine3.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewApplication.CommunicationAddress.AddressLine3);
                
                ddlState.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewApplication.CommunicationAddress.StateCode));

                if (ddlState.SelectedIndex > 0)
                {
                    ddlDistrict.Items.Clear();
                    NOCAPExternalUtility.FillDropDownDistrict(ref ddlDistrict, obj_infrastructureRenewApplication.CommunicationAddress.StateCode);
                    ddlDistrict.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewApplication.CommunicationAddress.DistrictCode));


                }
                if (ddlDistrict.SelectedIndex > 0)
                {
                    ddlSubDistrict.Items.Clear();
                    NOCAPExternalUtility.FillDropDownSubDistrict(ref ddlSubDistrict, obj_infrastructureRenewApplication.CommunicationAddress.DistrictCode, obj_infrastructureRenewApplication.CommunicationAddress.StateCode);
                    ddlSubDistrict.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewApplication.CommunicationAddress.SubDistrictCode));
                }
            

                
                txtPinCode.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewApplication.CommunicationAddress.PinCode));
                //txtCountryCode.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplication.CommunicationPhoneNumber.PhoneNumberISD);
                txtStdCode.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewApplication.CommunicationPhoneNumber.PhoneNumberSTD);
                txtPhoneNumber.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewApplication.CommunicationPhoneNumber.PhoneNumberRest);
                //txtMobileCountryCode.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplication.CommunicationMobileNumber.MobileNumberISD);
                txtMobileNumber.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewApplication.CommunicationMobileNumber.MobileNumberRest);
                //txtCountryCodeFax.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplication.CommunicationFaxNumber.FaxNumberISD);
                txtStdCodeFax.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewApplication.CommunicationFaxNumber.FaxNumberSTD);
                txtFaxNumber.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewApplication.CommunicationFaxNumber.FaxNumberRest);
                txtEmail.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewApplication.CommunicationEmailID);
            }
            else
            {
                lblMessage.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewApplication.CustumMessage); ;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        int int_StateCode;
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
                ddlDistrict.Items.Clear();
                ddlSubDistrict.Items.Clear();

                if (ddlState.SelectedValue == "")
                {
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlDistrict);
                }
                else
                {
                    int_StateCode = Convert.ToInt32(ddlState.SelectedValue);

                    if (NOCAPExternalUtility.FillDropDownDistrict(ref ddlDistrict, int_StateCode) != 1)
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
                ddlSubDistrict.Items.Clear();
                if (ddlDistrict.SelectedValue == "")
                {
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlSubDistrict);
                }
                else
                {
                    int_DistricCode = Convert.ToInt32(ddlDistrict.SelectedValue);
                    int_StateCode = Convert.ToInt32(ddlState.SelectedValue);

                    if (NOCAPExternalUtility.FillDropDownSubDistrict(ref ddlSubDistrict, int_DistricCode, int_StateCode) != 1)
                    {
                        lblMessage.Text = "Problem in Sub-district population";
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
    private int UpdateCommunicationDetails(long lngA_ApplicationCode)
    {
        if (Page.IsValid)
        {
            try
            {
                strActionName = "Update Communication Address";
                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);

                obj_infrastructureRenewSADApplication.CommunicationAddress.AddressLine1 = txtAddressLine1.Text;
                obj_infrastructureRenewSADApplication.CommunicationAddress.AddressLine2 = txtAddressLine2.Text;
                obj_infrastructureRenewSADApplication.CommunicationAddress.AddressLine3 = txtAddressLine3.Text;
                if (NOCAPExternalUtility.IsNumeric(ddlState.SelectedValue)) { obj_infrastructureRenewSADApplication.CommunicationAddress.StateCode = Convert.ToInt32(ddlState.SelectedValue); }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('State allows Numeric ');", true);
                    return 0;
                }
                if (NOCAPExternalUtility.IsNumeric(ddlDistrict.SelectedValue)) { obj_infrastructureRenewSADApplication.CommunicationAddress.DistrictCode = Convert.ToInt32(ddlDistrict.SelectedValue); }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('District allows Numeric ');", true);
                    return 0;
                }

                if (ddlSubDistrict.SelectedIndex > 0 && !NOCAPExternalUtility.IsNumeric(ddlSubDistrict.SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Sub District allows Numeric ');", true);
                    return 0;

                }
                else
                {
                    if (ddlSubDistrict.SelectedValue == "")
                    {
                        obj_infrastructureRenewSADApplication.CommunicationAddress.SubDistrictCode = 0;
                    }
                    else
                    {
                        obj_infrastructureRenewSADApplication.CommunicationAddress.SubDistrictCode = Convert.ToInt32(ddlSubDistrict.SelectedValue);
                    }
                }
                obj_infrastructureRenewSADApplication.CommunicationAddress.PinCode = Convert.ToInt32(txtPinCode.Text);
                obj_infrastructureRenewSADApplication.CommunicationPhoneNumber.PhoneNumberISD = txtPhoneNumber.Text != "" ? txtCountryCode.Text : "";
                obj_infrastructureRenewSADApplication.CommunicationPhoneNumber.PhoneNumberSTD = txtStdCode.Text;
                obj_infrastructureRenewSADApplication.CommunicationPhoneNumber.PhoneNumberRest = txtPhoneNumber.Text;
                obj_infrastructureRenewSADApplication.CommunicationMobileNumber.MobileNumberISD = txtMobileNumber.Text != "" ? txtMobileCountryCode.Text : "";
                obj_infrastructureRenewSADApplication.CommunicationMobileNumber.MobileNumberRest = txtMobileNumber.Text;
                obj_infrastructureRenewSADApplication.CommunicationFaxNumber.FaxNumberISD = txtFaxNumber.Text != "" ? txtCountryCodeFax.Text : "";
                obj_infrastructureRenewSADApplication.CommunicationFaxNumber.FaxNumberSTD = txtStdCodeFax.Text;
                obj_infrastructureRenewSADApplication.CommunicationFaxNumber.FaxNumberRest = txtFaxNumber.Text;
                obj_infrastructureRenewSADApplication.CommunicationEmailID = txtEmail.Text;

                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                obj_infrastructureRenewSADApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

                if (obj_infrastructureRenewSADApplication.Update() == 1)
                {
                    strStatus = "Update Success";
                    lblMessage.Text = "Successfully Saved";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    return 1;
                }
                else
                {
                    lblMessage.Text = obj_infrastructureRenewSADApplication.CustumMessage;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return 0;
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
                return 0;
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
        return 0;
    }
    protected void txtNext_Click(object sender, EventArgs e)
    {
        string str_RedirectPath = "";
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
                    if (UpdateCommunicationDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text)) == 1)
                    {
                        Server.Transfer("~/ExternalUser/InfrastructureRenew/ExistingNOCIssued.aspx");
                    }
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

    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
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
                    if (UpdateCommunicationDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text)) == 1)
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
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
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

            Server.Transfer("~/ExternalUser/InfrastructureRenew/InfrastructureRenew.aspx");
        }

    }

}