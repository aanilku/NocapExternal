using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_Expansion_MIN_CommunicationAddress : System.Web.UI.Page
{
    string strPageName = "MINCommunicationAddress";
    string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ValidationExpInit();
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;

            if (NOCAPExternalUtility.FillDropDownState(ref ddlState) != 1)
            {
                lblMessage.Text = "Problem in state population";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            try
            {
                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblModeFrom");
                        if (SourceLabel != null) { lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); }
                    }
                }
                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblMiningApplicationCodeFrom");
                        if (SourceLabel != null) { lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); }
                    }
                }
                if (lblMiningApplicationCodeFrom.Text.Trim() != "") { BindCommunicationDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text)); }
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
            NOCAP.BLL.Mining.Expansion.MiningExpansionApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.Expansion.MiningExpansionApplication(lngA_ApplicationCode);
            txtAddressLine1.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.CommunicationAddress.AddressLine1);
            txtAddressLine2.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.CommunicationAddress.AddressLine2);
            txtAddressLine3.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.CommunicationAddress.AddressLine3);
            ddlState.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.CommunicationAddress.StateCode));

            if (ddlState.SelectedIndex > 0)
            {
                ddlDistrict.Items.Clear();
                NOCAPExternalUtility.FillDropDownDistrict(ref ddlDistrict, obj_MiningNewApplication.CommunicationAddress.StateCode);
                ddlDistrict.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.CommunicationAddress.DistrictCode));


            }
            if (ddlDistrict.SelectedIndex > 0)
            {
                ddlSubDistrict.Items.Clear();
                NOCAPExternalUtility.FillDropDownSubDistrict(ref ddlSubDistrict, obj_MiningNewApplication.CommunicationAddress.DistrictCode, obj_MiningNewApplication.CommunicationAddress.StateCode);
                ddlSubDistrict.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.CommunicationAddress.SubDistrictCode));
            }


            txtPinCode.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.CommunicationAddress.PinCode));
            //txtCountryCode.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.CommunicationPhoneNumber.PhoneNumberISD);
            txtStdCode.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.CommunicationPhoneNumber.PhoneNumberSTD);
            txtPhoneNumber.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.CommunicationPhoneNumber.PhoneNumberRest);
            //txtMobileCountryCode.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.CommunicationMobileNumber.MobileNumberISD);
            txtMobileNumber.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.CommunicationMobileNumber.MobileNumberRest);
            //txtCountryCodeFax.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.CommunicationFaxNumber.FaxNumberISD);
            txtStdCodeFax.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.CommunicationFaxNumber.FaxNumberSTD);
            txtFaxNumber.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.CommunicationFaxNumber.FaxNumberRest);
            txtEmail.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.CommunicationEmailID);
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally
        {
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
            Server.Transfer("~/ExternalUser/Expansion/MIN/MiningExpansion.aspx");
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
            if (Page.IsValid)
            {
                UpdateCommunicationDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
            }

            NOCAP.BLL.Mining.Expansion.MiningExpansionApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.Expansion.MiningExpansionApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
            int int_stateCode = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode;
            int int_districtCode = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode;
            int int_subDistrictCode = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode;
            int int_applicationTypeCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Infrastructure");
            int int_applicationPurposeCode = NOCAPExternalUtility.EquivalentApplicationPurposeCodeOfDatabaseForApplicationPurpose("New");
            int int_applicationTypeCategoryCode = obj_MiningNewApplication.ApplicationTypeCategoryCode;
            decimal? dec_gwRequirement = obj_MiningNewApplication.GWREquiredThroughAbstractStructure;

            NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter obj_ApplicationAllowOrNotForExemptionLetter = new NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter(int_applicationTypeCode, int_applicationPurposeCode, int_applicationTypeCategoryCode, int_stateCode, int_districtCode, int_subDistrictCode, dec_gwRequirement);

            //Response.Write("Category:" + obj_ApplicationAllowOrNotForExemptionLetter.ProceedForExemptionLetterOnAreaTypeCategory + " <br / > <br /> ");
            //Response.Write("Water Based:" + obj_ApplicationAllowOrNotForExemptionLetter.ProceedForExemptionLetterOnWaterBased + " <br / > <br /> ");
            //Response.Write("Water Less Than:" + obj_ApplicationAllowOrNotForExemptionLetter.ProceedForExemptionLetterOnGWWaterQuantityLessThan + " <br / > <br /> ");
            //Response.Write("Final:" + obj_ApplicationAllowOrNotForExemptionLetter.ProceedForFinalExemptionLetter + " <br / > <br /> ");
        }
    }
    protected void txtNext_Click(object sender, EventArgs e)
    {
        NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter obj_ApplicationAllowOrNotForExemptionLetter = new NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter();
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
                    if (UpdateCommunicationDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text)) == 1)
                    {

                        NOCAP.BLL.Mining.Expansion.MiningExpansionApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.Expansion.MiningExpansionApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                        int int_stateCode = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode;
                        int int_districtCode = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode;
                        int int_subDistrictCode = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode;
                        int int_applicationTypeCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Mining");
                        int int_applicationPurposeCode = NOCAPExternalUtility.EquivalentApplicationPurposeCodeOfDatabaseForApplicationPurpose("New");
                        int int_applicationTypeCategoryCode = obj_MiningNewApplication.ApplicationTypeCategoryCode;
                        decimal? dec_gwRequirement = obj_MiningNewApplication.GWREquiredThroughAbstractStructure;
                        obj_ApplicationAllowOrNotForExemptionLetter = new NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter(int_applicationTypeCode, int_applicationPurposeCode, int_applicationTypeCategoryCode, int_stateCode, int_districtCode, int_subDistrictCode, dec_gwRequirement);
                        if (obj_ApplicationAllowOrNotForExemptionLetter.ProceedForFinalExemptionLetter == NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter.ApplicationAllowOrNotForExemptionLetterOption.Allow)
                        {
                            str_RedirectPath = "~/ExternalUser/Expansion/MIN/SalientFeature.aspx";
                        }
                        else
                        {
                            str_RedirectPath = "~/ExternalUser/Expansion/MIN/LandUseDetails.aspx";
                        }
                    }
                    else { }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                obj_ApplicationAllowOrNotForExemptionLetter.Dispose();
                switch (str_RedirectPath)
                {
                    case "~/ExternalUser/Expansion/MIN/SalientFeature.aspx":
                        Server.Transfer("~/ExternalUser/Expansion/MIN/SalientFeature.aspx");
                        break;
                    case "~/ExternalUser/Expansion/MIN/LandUseDetails.aspx":
                        Server.Transfer("~/ExternalUser/Expansion/MIN/LandUseDetails.aspx");
                        break;
                }
            }
        }
    }

    private int UpdateCommunicationDetails(long lngA_ApplicationCode)
    {
      
            try
            {
                NOCAP.BLL.Mining.Expansion.MiningExpansionApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.Expansion.MiningExpansionApplication(lngA_ApplicationCode);
                obj_MiningNewApplication.CommunicationAddress.AddressLine1 = txtAddressLine1.Text;
                obj_MiningNewApplication.CommunicationAddress.AddressLine2 = txtAddressLine2.Text;
                obj_MiningNewApplication.CommunicationAddress.AddressLine3 = txtAddressLine3.Text;
                //if (NOCAPExternalUtility.IsNumeric(ddlState.SelectedValue)) { obj_MiningNewApplication.CommunicationAddress.StateCode = Convert.ToInt32(ddlState.SelectedValue); } else { return 0; }
                //if (NOCAPExternalUtility.IsNumeric(ddlDistrict.SelectedValue)) { obj_MiningNewApplication.CommunicationAddress.DistrictCode = Convert.ToInt32(ddlDistrict.SelectedValue); } else { return 0; }
                //if (NOCAPExternalUtility.IsNumeric(ddlSubDistrict.SelectedValue)) { obj_MiningNewApplication.CommunicationAddress.SubDistrictCode = Convert.ToInt32(ddlSubDistrict.SelectedValue); } else { return 0; }


                if (NOCAPExternalUtility.IsNumeric(ddlState.SelectedValue)) { obj_MiningNewApplication.CommunicationAddress.StateCode = Convert.ToInt32(ddlState.SelectedValue); }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('State allows Numeric ');", true);
                    return 0;
                }
                if (NOCAPExternalUtility.IsNumeric(ddlDistrict.SelectedValue)) { obj_MiningNewApplication.CommunicationAddress.DistrictCode = Convert.ToInt32(ddlDistrict.SelectedValue); }
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
                        obj_MiningNewApplication.CommunicationAddress.SubDistrictCode = 0;
                    }
                    else
                    {
                        obj_MiningNewApplication.CommunicationAddress.SubDistrictCode = Convert.ToInt32(ddlSubDistrict.SelectedValue);
                    }
                }

                obj_MiningNewApplication.CommunicationAddress.PinCode = Convert.ToInt32(txtPinCode.Text);
                obj_MiningNewApplication.CommunicationPhoneNumber.PhoneNumberISD = txtPhoneNumber.Text != "" ? txtCountryCode.Text : "";
                obj_MiningNewApplication.CommunicationPhoneNumber.PhoneNumberSTD = txtStdCode.Text;
                obj_MiningNewApplication.CommunicationPhoneNumber.PhoneNumberRest = txtPhoneNumber.Text;
                obj_MiningNewApplication.CommunicationMobileNumber.MobileNumberISD = txtMobileNumber.Text != "" ? txtMobileCountryCode.Text : "";
                obj_MiningNewApplication.CommunicationMobileNumber.MobileNumberRest = txtMobileNumber.Text;
                obj_MiningNewApplication.CommunicationFaxNumber.FaxNumberISD = txtFaxNumber.Text != "" ? txtCountryCodeFax.Text : ""; 
                obj_MiningNewApplication.CommunicationFaxNumber.FaxNumberSTD = txtStdCodeFax.Text;
                obj_MiningNewApplication.CommunicationFaxNumber.FaxNumberRest = txtFaxNumber.Text;
                obj_MiningNewApplication.CommunicationEmailID = txtEmail.Text;
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                obj_MiningNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;
                if (obj_MiningNewApplication.Update() == 1)
                {
                    strActionName = "SaveAsDraft";
                    strStatus = "Record Save Successfully !";


                    lblMessage.Text = "Saved Successfully";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    return 1;
                }
                else
                {
                    strActionName = "SaveAsDraft";
                    strStatus = "Record Save Failed !";

                    lblMessage.Text = obj_MiningNewApplication.CustumMessage;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return 0;
                }
            }
            catch (Exception)
            {
                strActionName = "SaveAsDraft";
                strStatus = "Record Save Failed !";

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
                if (ddlState.SelectedValue == "") { NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlDistrict); }
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
            finally
            {
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
                if (ddlDistrict.SelectedValue == "") { NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlSubDistrict); }
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
            finally
            {
            }
        }
    }
 
}