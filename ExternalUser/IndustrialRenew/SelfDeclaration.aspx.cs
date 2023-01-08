using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_IndustrialRenew_SelfDeclaration : System.Web.UI.Page
{
    string strPageName = "INDRenewSelfDeclaration";
    string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            rngDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;

            lblMessage.Text = "";

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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblIndustialApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                        }
                    }

                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {

                    BindSelfDeclarationDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }


        }
    }
    private void BindSelfDeclarationDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewSADApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);
            lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_IndustrialRenewSADApplication.NameOfIndustry);
            obj_industrialNewApplication = obj_IndustrialRenewSADApplication.GetFirstIndustrialApplication();



            if (obj_IndustrialRenewSADApplication.BharatTransReferanceNumber == null)
                txtBharatKoshRefferenceNo.Text = "";
            else
                txtBharatKoshRefferenceNo.Text = obj_IndustrialRenewSADApplication.BharatTransReferanceNumber.ToString();
            if (obj_IndustrialRenewSADApplication.BharatTransDated == null)
                txtBharatKoshDated.Text = "";
            else
                txtBharatKoshDated.Text = Convert.ToDateTime(obj_IndustrialRenewSADApplication.BharatTransDated).ToString("dd/MM/yyyy");


            if (obj_industrialNewApplication != null)
            {


                NOCAP.BLL.Master.FeeRequiredPending objFeeRequiredPending = new NOCAP.BLL.Master.FeeRequiredPending(obj_IndustrialRenewSADApplication.ApplicationTypeCode, obj_IndustrialRenewSADApplication.ApplicationPurposeCode);

                if (objFeeRequiredPending != null && objFeeRequiredPending.RequiredPending == NOCAP.BLL.Master.FeeRequiredPending.RequiredOrPending.Required)
                {
                    //lblFee.Text = HttpUtility.HtmlEncode("Applicant has to Submit Processing Fee of Rs " + objFeeRequiredPending.Amount + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(objFeeRequiredPending.Amount))) + ") in the form of Demand Draft drawn in Favour of PAO, CGWB and Payable at Faridabad, Haryana.)");
                    //lblFee.Text = HttpUtility.HtmlEncode("Applicant has to Submit Processing Fee of Rs " + objFeeRequiredPending.Amount + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(objFeeRequiredPending.Amount))) + ") through NON TAX RECEIPT PORTAL (https://bharatkosh.gov.in). A receipt will be generated. Please fill in the Transaction Ref No. and Date from the receipt, in print out of application and attach receipt along with hard copy of application.");                          
                    lblFee.Text = HttpUtility.HtmlEncode("Reciept of Processing Fee of Rs. " + objFeeRequiredPending.Amount + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(objFeeRequiredPending.Amount))) + ") submitted  through NON TAX RECEIPT PORTAL (https://bharatkosh.gov.in) should be attached in online application at prescribed place before submission of application.");

                }
                else
                {
                    lblFee.Text = "Processing Fee : Not Required.";

                }

                if (obj_industrialNewApplication.Undertaking.UndertakingAgreement == NOCAP.BLL.Common.Undertaking.UndertakingAgreementOption.Yes)
                {
                    chkBoxUndertaking.Checked = true;
                }
                else
                {
                    chkBoxUndertaking.Checked = false;
                }

                NOCAP.BLL.Master.HeadQuarter obj_headQuarter = new NOCAP.BLL.Master.HeadQuarter(1);
                string HQOfficeName = obj_headQuarter.HeadQuarterName;
                string HQAddLine1 = obj_headQuarter.AddressLine1;
                string HQAddLine2 = obj_headQuarter.AddressLine2;
                string HQAddLine3 = obj_headQuarter.AddressLine3;
                string HQState = obj_headQuarter.GetAddressStateName();
                string HQDistrict = obj_headQuarter.GetAddressDistrictName();
                string HQSubDistrict = obj_headQuarter.GetAddressSubDistrictName();
                string HQPinCode = Convert.ToString(obj_headQuarter.PinCode);

                string RegionalOfficeName = obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().RegionalOfficeName;
                string RegionalAddLine1 = obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().AddressLine1;
                string RegionalAddLine2 = obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().AddressLine2;
                string RegionalAddLine3 = obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().AddressLine3;
                string RegionalSubDistrict = obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().GetAddressSubDistrictName();
                string RegionalDistrict = obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().GetAddressDistrictName();
                string RegionalState = obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().GetAddressStateName();

                string RegionalPinCode = Convert.ToString(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().PinCode);

                lblRegionalOffi.Text = HttpUtility.HtmlEncode(RegionalOfficeName + ", <br />" + RegionalAddLine1 + RegionalAddLine2 == "" ? " " : ", <br />" + RegionalAddLine2 + RegionalAddLine3 == "" ? "" : ", <br />" + RegionalAddLine3 + ", <br />" + RegionalDistrict + ", " + RegionalState + " - " + RegionalPinCode);
            }
            else
            {
                lblMessage.Text = "First Application Code Required";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    private int UpdateSelfDeclarationDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);


            if (chkBoxUndertaking.Checked)
            {
                obj_industrialRenewApplication.Undertaking.UndertakingAgreement = NOCAP.BLL.Common.Undertaking.UndertakingAgreementOption.Yes;
            }
            else
            {
                obj_industrialRenewApplication.Undertaking.UndertakingAgreement = NOCAP.BLL.Common.Undertaking.UndertakingAgreementOption.No;
            }


            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_industrialRenewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (txtBharatKoshRefferenceNo.Text != "")
                obj_industrialRenewApplication.BharatTransReferanceNumber = Convert.ToInt64(txtBharatKoshRefferenceNo.Text);
            else
                obj_industrialRenewApplication.BharatTransReferanceNumber = null;
            if (txtBharatKoshDated.Text != "")
                obj_industrialRenewApplication.BharatTransDated = Convert.ToDateTime(txtBharatKoshDated.Text);
            else
                obj_industrialRenewApplication.BharatTransDated = null;


            if (obj_industrialRenewApplication.Update() == 1)
            {
                strActionName = "SaveAsDraft";
                strStatus = "Record Save Successfully !";

                lblMessage.Text = "Successfully Saved";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                strActionName = "SaveAsDraft";
                strStatus = "Record Save Failed !";

                lblMessage.Text = obj_industrialRenewApplication.CustumMessage;
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
            Server.Transfer("OtherDetails.aspx");
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
                if (chkBoxUndertaking.Checked)
                {
                    UpdateSelfDeclarationDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                }
                else
                {
                    lblMessage.Text = "Please accept Undertaking";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    Page.SetFocus(chkBoxUndertaking);
                }
            }

            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
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
            if (chkBoxUndertaking.Checked)
            {

                if (UpdateSelfDeclarationDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text)) == 1)
                {
                    Server.Transfer("Attachment.aspx");
                }

            }
            else
            {
                lblMessage.Text = "Please accept Undertaking";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                Page.SetFocus(chkBoxUndertaking);
            }
        }

    }
    
    protected void ValidateDate(object sender, ServerValidateEventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            e.IsValid = false;
        }
    }

}