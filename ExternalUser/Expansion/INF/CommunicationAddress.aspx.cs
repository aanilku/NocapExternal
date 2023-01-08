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

public partial class ExternalUser_Expansion_INF_CommunicationAddress : System.Web.UI.Page
{
    string strPageName = "INFCommunicationAddress";
    string strActionName = "";
    string strStatus = "";
    //string category = "";
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
                            lblinfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                        }
                    }

                }
                if (lblinfrastructureApplicationCodeFrom.Text.Trim() != "")
                {
                    BindCommunicationDetails(Convert.ToInt32(lblinfrastructureApplicationCodeFrom.Text));
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
            NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication obj_infrastructureExpansionApplication = new NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication(lngA_ApplicationCode);

            txtAddressLine1.Text = HttpUtility.HtmlEncode(obj_infrastructureExpansionApplication.CommunicationAddress.AddressLine1);
            txtAddressLine2.Text = HttpUtility.HtmlEncode(obj_infrastructureExpansionApplication.CommunicationAddress.AddressLine2);
            txtAddressLine3.Text = HttpUtility.HtmlEncode(obj_infrastructureExpansionApplication.CommunicationAddress.AddressLine3);
            ddlState.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.CommunicationAddress.StateCode));


            if (ddlState.SelectedIndex > 0)
            {
                ddlDistrict.Items.Clear();
                NOCAPExternalUtility.FillDropDownDistrict(ref ddlDistrict, obj_infrastructureExpansionApplication.CommunicationAddress.StateCode);
                ddlDistrict.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.CommunicationAddress.DistrictCode));


            }
            if (ddlDistrict.SelectedIndex > 0)
            {
                ddlSubDistrict.Items.Clear();
                NOCAPExternalUtility.FillDropDownSubDistrict(ref ddlSubDistrict, obj_infrastructureExpansionApplication.CommunicationAddress.DistrictCode, obj_infrastructureExpansionApplication.CommunicationAddress.StateCode);
                ddlSubDistrict.SelectedValue = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.CommunicationAddress.SubDistrictCode));
            }


            txtPinCode.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureExpansionApplication.CommunicationAddress.PinCode));
            //txtCountryCode.Text = HttpUtility.HtmlEncode(obj_infrastructureExpansionApplication.CommunicationPhoneNumber.PhoneNumberISD);
            txtStdCode.Text = HttpUtility.HtmlEncode(obj_infrastructureExpansionApplication.CommunicationPhoneNumber.PhoneNumberSTD);
            txtPhoneNumber.Text = HttpUtility.HtmlEncode(obj_infrastructureExpansionApplication.CommunicationPhoneNumber.PhoneNumberRest);
            //txtMobileCountryCode.Text = HttpUtility.HtmlEncode(obj_infrastructureExpansionApplication.CommunicationMobileNumber.MobileNumberISD);
            txtMobileNumber.Text = HttpUtility.HtmlEncode(obj_infrastructureExpansionApplication.CommunicationMobileNumber.MobileNumberRest);
            //txtCountryCodeFax.Text = HttpUtility.HtmlEncode(obj_infrastructureExpansionApplication.CommunicationFaxNumber.FaxNumberISD);
            txtStdCodeFax.Text = HttpUtility.HtmlEncode(obj_infrastructureExpansionApplication.CommunicationFaxNumber.FaxNumberSTD);


            txtFaxNumber.Text = HttpUtility.HtmlEncode(obj_infrastructureExpansionApplication.CommunicationFaxNumber.FaxNumberRest);
            txtEmail.Text = HttpUtility.HtmlEncode(obj_infrastructureExpansionApplication.CommunicationEmailID);
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally
        {
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            int int_StateCode;

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
            finally
            {
            }
        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            int int_StateCode;
            int int_DistricCode;


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
            finally
            {
            }
        }
    }

    private int UpdateCommunicationDetails(long lngA_ApplicationCode)
    {
        try
        {
            strActionName = "UpdateCommunicationAddressDetails";
            NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication obj_infrastructureExpansionApplication = new NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication(lngA_ApplicationCode);

            obj_infrastructureExpansionApplication.CommunicationAddress.AddressLine1 = txtAddressLine1.Text;
            obj_infrastructureExpansionApplication.CommunicationAddress.AddressLine2 = txtAddressLine2.Text;
            obj_infrastructureExpansionApplication.CommunicationAddress.AddressLine3 = txtAddressLine3.Text;
            if (NOCAPExternalUtility.IsNumeric(ddlState.SelectedValue)) { obj_infrastructureExpansionApplication.CommunicationAddress.StateCode = Convert.ToInt32(ddlState.SelectedValue); }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('State allows Numeric ');", true);
                return 0; 
            }
            if (NOCAPExternalUtility.IsNumeric(ddlDistrict.SelectedValue)) { obj_infrastructureExpansionApplication.CommunicationAddress.DistrictCode = Convert.ToInt32(ddlDistrict.SelectedValue); }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('District allows Numeric ');", true); 
                return 0;
            }
            

            //if (NOCAPExternalUtility.IsNumeric(ddlSubDistrict.SelectedValue)) { obj_infrastructureExpansionApplication.CommunicationAddress.SubDistrictCode = Convert.ToInt32(ddlSubDistrict.SelectedValue); } else { return 0; }
            if (ddlSubDistrict.SelectedIndex > 0 && !NOCAPExternalUtility.IsNumeric(ddlSubDistrict.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Sub District allows Numeric ');", true);
                return 0;

            }
            else
            {
                if (ddlSubDistrict.SelectedValue == "")
                {
                    obj_infrastructureExpansionApplication.CommunicationAddress.SubDistrictCode = 0;
                }
                else
                {
                    obj_infrastructureExpansionApplication.CommunicationAddress.SubDistrictCode = Convert.ToInt32(ddlSubDistrict.SelectedValue);
                }
            }
            
            obj_infrastructureExpansionApplication.CommunicationAddress.PinCode = Convert.ToInt32(txtPinCode.Text);
            obj_infrastructureExpansionApplication.CommunicationPhoneNumber.PhoneNumberISD = txtPhoneNumber.Text != "" ? txtCountryCode.Text : "";
            obj_infrastructureExpansionApplication.CommunicationPhoneNumber.PhoneNumberSTD = txtStdCode.Text;
            obj_infrastructureExpansionApplication.CommunicationPhoneNumber.PhoneNumberRest = txtPhoneNumber.Text;
            obj_infrastructureExpansionApplication.CommunicationMobileNumber.MobileNumberISD = txtMobileNumber.Text != "" ? txtMobileCountryCode.Text : ""; 
            obj_infrastructureExpansionApplication.CommunicationMobileNumber.MobileNumberRest = txtMobileNumber.Text;
            obj_infrastructureExpansionApplication.CommunicationFaxNumber.FaxNumberISD = txtFaxNumber.Text != "" ? txtCountryCodeFax.Text : ""; 
            obj_infrastructureExpansionApplication.CommunicationFaxNumber.FaxNumberSTD = txtStdCodeFax.Text;
            obj_infrastructureExpansionApplication.CommunicationFaxNumber.FaxNumberRest = txtFaxNumber.Text;
            obj_infrastructureExpansionApplication.CommunicationEmailID = txtEmail.Text;

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_infrastructureExpansionApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_infrastructureExpansionApplication.Update() == 1)
            {
                strStatus = "Saved Successfully";
                lblMessage.Text = "Successfully Saved";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                strStatus = "Save Unsuccessfull";
                lblMessage.Text = obj_infrastructureExpansionApplication.CustumMessage;
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

    protected void txtNext_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            //NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter obj_ApplicationAllowOrNotForExemptionLetter = new NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter();
            //string str_RedirectPath = "";
            try
            {
                if (Page.IsValid)
                {

                    if (lblinfrastructureApplicationCodeFrom.Text !="")
                    {
                        Server.Transfer("~/ExternalUser/Expansion/INF/LandUseDetails.aspx");
                    }
                    //if (UpdateCommunicationDetails(Convert.ToInt32(lblinfrastructureApplicationCodeFrom.Text)) == 1)
                    //{

                    //    //NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication obj_infrastructureExpansionApplication = new NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication(Convert.ToInt32(lblinfrastructureApplicationCodeFrom.Text));
                    //    //int int_stateCode = obj_infrastructureExpansionApplication.ProposedLocation.ProposedLocationAddress.StateCode;
                    //    //int int_districtCode = obj_infrastructureExpansionApplication.ProposedLocation.ProposedLocationAddress.DistrictCode;
                    //    //int int_subDistrictCode = obj_infrastructureExpansionApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode;
                    //    //int int_applicationTypeCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Infrastructure");
                    //    //int int_applicationPurposeCode = NOCAPExternalUtility.EquivalentApplicationPurposeCodeOfDatabaseForApplicationPurpose("New");
                    //    //int int_applicationTypeCategoryCode = obj_infrastructureExpansionApplication.ApplicationTypeCategoryCode;
                    //    //decimal? dec_gwRequirement = obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement;
                    //    //obj_ApplicationAllowOrNotForExemptionLetter = new NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter(int_applicationTypeCode, int_applicationPurposeCode, int_applicationTypeCategoryCode, int_stateCode, int_districtCode, int_subDistrictCode, dec_gwRequirement);
                    //    //if (obj_ApplicationAllowOrNotForExemptionLetter.ProceedForFinalExemptionLetter == NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter.ApplicationAllowOrNotForExemptionLetterOption.Allow)
                    //    //{
                    //    //    str_RedirectPath = "~/ExternalUser/InfrastructureNew/SalientFeature.aspx";
                    //    //}
                    //    //else
                    //    //{
                    //    //str_RedirectPath = "~/ExternalUser/InfrastructureNew/LandUseDetails.aspx";
                    //    //}
                    //    Server.Transfer("~/ExternalUser/Expansion/INF/LandUseDetails.aspx");
                    //}
                    //else { }
                }
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                //obj_ApplicationAllowOrNotForExemptionLetter.Dispose();
                //switch (str_RedirectPath)
                //{
                //    case "~/ExternalUser/InfrastructureNew/SalientFeature.aspx":
                //        Server.Transfer("~/ExternalUser/InfrastructureNew/SalientFeature.aspx");
                //        break;
                //    case "~/ExternalUser/InfrastructureNew/LandUseDetails.aspx":
                //        Server.Transfer("~/ExternalUser/InfrastructureNew/LandUseDetails.aspx");
                //        break;
                //}
            }
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
                UpdateCommunicationDetails(Convert.ToInt32(lblinfrastructureApplicationCodeFrom.Text));
            }
        }

        //NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication obj_infrastructureExpansionApplication = new NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication(Convert.ToInt32(lblinfrastructureApplicationCodeFrom.Text));
        //int int_stateCode = obj_infrastructureExpansionApplication.ProposedLocation.ProposedLocationAddress.StateCode;
        //int int_districtCode = obj_infrastructureExpansionApplication.ProposedLocation.ProposedLocationAddress.DistrictCode;
        //int int_subDistrictCode = obj_infrastructureExpansionApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode;
        //int int_applicationTypeCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Infrastructure");
        //int int_applicationPurposeCode = NOCAPExternalUtility.EquivalentApplicationPurposeCodeOfDatabaseForApplicationPurpose("New");
        //int int_applicationTypeCategoryCode = obj_infrastructureExpansionApplication.ApplicationTypeCategoryCode;
        //decimal? dec_gwRequirement = obj_infrastructureExpansionApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement;

        //NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter obj_ApplicationAllowOrNotForExemptionLetter = new NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter(int_applicationTypeCode, int_applicationPurposeCode, int_applicationTypeCategoryCode, int_stateCode, int_districtCode, int_subDistrictCode, dec_gwRequirement);

        //Response.Write("Category:" + obj_ApplicationAllowOrNotForExemptionLetter.ProceedForExemptionLetterOnAreaTypeCategory + " <br / > <br /> ");
        //Response.Write("Water Based:" + obj_ApplicationAllowOrNotForExemptionLetter.ProceedForExemptionLetterOnWaterBased + " <br / > <br /> ");
        //Response.Write("Water Less Than:" + obj_ApplicationAllowOrNotForExemptionLetter.ProceedForExemptionLetterOnGWWaterQuantityLessThan + " <br / > <br /> ");
        //Response.Write("Final:" + obj_ApplicationAllowOrNotForExemptionLetter.ProceedForFinalExemptionLetter + " <br / > <br /> ");

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
            Server.Transfer("~/ExternalUser/Expansion/INF/InfrastructureNew.aspx");
        }
    }
    
}