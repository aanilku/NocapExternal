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

public partial class ExternalUser_IndustrialNew_SelfDeclaration : System.Web.UI.Page
{
    string strPageName = "SelfDeclaration";
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
                //lblMessage.Text = ex.Message;
                //lblMessage.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }


        }
    }
    private void CalculateCharges(long lngA_ApplicationCode, decimal dec_Qty, int int_days)
    {
        decimal result = 0m;
        int intA_AreaTypeCatCode = AreaTypeCatCode(lngA_ApplicationCode);
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(lngA_ApplicationCode);
        NOCAP.BLL.Master.PackDrinkCharge obj_PackDrinkCharge = null;
        NOCAP.BLL.Master.IndustrialCharge obj_IndustrialCharge = null;
        
        if (obj_IndustrialNewSADApplication.ApplicationTypeCategoryCode.ToString()== "73")
        {
            obj_PackDrinkCharge = new NOCAP.BLL.Master.PackDrinkCharge(1, intA_AreaTypeCatCode, Convert.ToDecimal(dec_Qty));

            result = Convert.ToDecimal(dec_Qty) * obj_PackDrinkCharge.Rate * Convert.ToInt32(int_days);
        }
        else
        {
            obj_IndustrialCharge = new NOCAP.BLL.Master.IndustrialCharge(1, intA_AreaTypeCatCode, dec_Qty);
            result = Convert.ToDecimal(dec_Qty) * obj_IndustrialCharge.Rate * Convert.ToInt32(int_days);
        }




        //if (intA_AreaTypeCatCode == 5)
        //{
        //    lblCharges.Text = HttpUtility.HtmlEncode("Restoration Charge of Rs. " + String.Format("{0:0.00}", result) + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(result))) + ") submitted  through NON TAX RECEIPT PORTAL (https://bharatkosh.gov.in) should be attached in online application at prescribed place before submission of application.");
        //}
        //else
        //{
        //    lblCharges.Text = HttpUtility.HtmlEncode("Abstraction Charge of Rs. " + String.Format("{0:0.00}", result) + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(result))) + ") submitted  through NON TAX RECEIPT PORTAL (https://bharatkosh.gov.in) should be attached in online application at prescribed place before submission of application.");
        //}
    }
    private int AreaTypeCatCode(long lngA_ApplicationCode)
    {
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(lngA_ApplicationCode);
        NOCAP.BLL.Master.SubDistrict obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(Convert.ToInt32(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode), Convert.ToInt32(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode), Convert.ToInt32(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode));
        NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_subDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplication.ApplySubDistrictAreaCategoryKey);
        obj_subDistrictAreaTypeCategoryHistory = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory();
        string AreaTypeCatCode = obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode.ToString();
        NOCAP.BLL.Master.AreaTypeCategory obj_AreaTypeCategory = new NOCAP.BLL.Master.AreaTypeCategory(obj_subDistrictAreaTypeCategoryHistory.AreaTypeCode, obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode);
        return obj_AreaTypeCategory.AreaTypeCategoryCode;
    }
    private void BindSelfDeclarationDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(lngA_ApplicationCode);
            DateTime d = Convert.ToDateTime(obj_industrialNewApplication.BharatTransDated);//.ToString("dd/MM/yyyy");
            if (obj_industrialNewApplication.BharatTransReferanceNumber == null)
                txtBharatKoshRefferenceNo.Text = "";
            else
                txtBharatKoshRefferenceNo.Text = obj_industrialNewApplication.BharatTransReferanceNumber.ToString();
            if (obj_industrialNewApplication.BharatTransDated == null)
                txtBharatKoshDated.Text = "";
            else
                txtBharatKoshDated.Text = Convert.ToDateTime(obj_industrialNewApplication.BharatTransDated).ToString("dd/MM/yyyy");
            NOCAP.BLL.Master.FeeRequiredPending objFeeRequiredPending = new NOCAP.BLL.Master.FeeRequiredPending(obj_industrialNewApplication.ApplicationTypeCode, obj_industrialNewApplication.ApplicationPurposeCode);
            if (objFeeRequiredPending != null && objFeeRequiredPending.RequiredPending == NOCAP.BLL.Master.FeeRequiredPending.RequiredOrPending.Required)
            {
                //lblFee.Text = HttpUtility.HtmlEncode("Applicant has to Submit Processing Fee of Rs " + objFeeRequiredPending.Amount + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(objFeeRequiredPending.Amount)))  + ") in the form of Demand Draft drawn in Favour of PAO, CGWB and Payable at Faridabad, Haryana.)");                          
                //lblFee.Text = HttpUtility.HtmlEncode("Applicant has to Submit Processing Fee of Rs " + objFeeRequiredPending.Amount + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(objFeeRequiredPending.Amount))) + ") through NON TAX RECEIPT PORTAL (https://bharatkosh.gov.in). A receipt will be generated. Please fill in the Transaction Ref No. and Date from the receipt, in print out of application and attach receipt along with hard copy of application.");
                lblFee.Text = HttpUtility.HtmlEncode("Reciept of Processing Fee of Rs. " + objFeeRequiredPending.Amount + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(objFeeRequiredPending.Amount))) + ") submitted  through NON TAX RECEIPT PORTAL (https://bharatkosh.gov.in) should be attached in online application at prescribed place before submission of application.");

            }
            else
            {
                lblFee.Text = "Processing Fee : Not Required.";

            }

            //if (obj_industrialNewApplication.Undertaking.UndertakingAgreement == NOCAP.BLL.Common.Undertaking.UndertakingAgreementOption.Yes)
            //{
            //    chkBoxUndertaking.Checked = true;
            //}
            //else
            //{
            //    chkBoxUndertaking.Checked = false;
            //}

            NOCAP.BLL.Master.HeadQuarter obj_headQuarter = new NOCAP.BLL.Master.HeadQuarter(1);
            string HQOfficeName = obj_headQuarter.HeadQuarterName;
            string HQAddLine1 = obj_headQuarter.AddressLine1;
            string HQAddLine2 = obj_headQuarter.AddressLine2;
            string HQAddLine3 = obj_headQuarter.AddressLine3;
            string HQState = obj_headQuarter.GetAddressStateName();
            string HQDistrict = obj_headQuarter.GetAddressDistrictName();
            string HQSubDistrict = obj_headQuarter.GetAddressSubDistrictName();
            string HQPinCode = Convert.ToString(obj_headQuarter.PinCode);
            //lblHqAddress.Text = HttpUtility.HtmlEncode(HQOfficeName) + ", <br />" + HttpUtility.HtmlEncode(HQAddLine1) + ", <br />" + HttpUtility.HtmlEncode(HQAddLine2) + ", <br />" + HttpUtility.HtmlEncode(HQAddLine3) + ", <br />" + HttpUtility.HtmlEncode(HQSubDistrict) + ", " + HttpUtility.HtmlEncode(HQDistrict) + ", " + HttpUtility.HtmlEncode(HQState) + " - " + HttpUtility.HtmlEncode(HQPinCode);
            //NOCAP.BLL.Master.State obj_State = new NOCAP.BLL.Master.State(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode);
            //Response.Write(obj_State.GetAssociatedRegionalOffice().RegionalOfficeName);

            //Response.Write(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().RegionalOfficeName);
            string RegionalOfficeName = obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().RegionalOfficeName;
            string RegionalAddLine1 = obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().AddressLine1;
            string RegionalAddLine2 = obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().AddressLine2;
            string RegionalAddLine3 = obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().AddressLine3;
            string RegionalSubDistrict = obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().GetAddressSubDistrictName();
            string RegionalDistrict = obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().GetAddressDistrictName();
            string RegionalState = obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().GetAddressStateName();

            string RegionalPinCode = Convert.ToString(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().PinCode);
            //lblRegionalOffi.Text = HttpUtility.HtmlEncode(RegionalOfficeName) + ", <br />" + HttpUtility.HtmlEncode(RegionalAddLine1) + (RegionalAddLine2 == "" ? " " : ", <br />") + HttpUtility.HtmlEncode(RegionalAddLine2) + (RegionalAddLine3 == "" ? "" : ", <br />") + HttpUtility.HtmlEncode(RegionalAddLine3) + ", <br />" + HttpUtility.HtmlEncode(RegionalDistrict) + ", " + HttpUtility.HtmlEncode(RegionalState) + " - " + HttpUtility.HtmlEncode(RegionalPinCode);
            if (obj_industrialNewApplication.GWChargesDays > 0 && obj_industrialNewApplication.GWChargesQty > 0)
            {
                RowCharges.Visible = true;
                CalculateCharges(lngA_ApplicationCode, (decimal)obj_industrialNewApplication.GWChargesQty, (int)obj_industrialNewApplication.GWChargesDays);
            }
            else { RowCharges.Visible = false; }

        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
            //lblMessage.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    private int UpdateSelfDeclarationDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(lngA_ApplicationCode);

            // DateTime d = Convert.ToDateTime(txtBharatKoshDated.Text);
            obj_industrialNewApplication.Undertaking.UndertakingAgreement = NOCAP.BLL.Common.Undertaking.UndertakingAgreementOption.Yes;
            //if (chkBoxUndertaking.Checked)
            //{
            //    obj_industrialNewApplication.Undertaking.UndertakingAgreement = NOCAP.BLL.Common.Undertaking.UndertakingAgreementOption.Yes;
            //}
            //else
            //{
            //    obj_industrialNewApplication.Undertaking.UndertakingAgreement = NOCAP.BLL.Common.Undertaking.UndertakingAgreementOption.No;
            //}


            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_industrialNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;
            if (txtBharatKoshRefferenceNo.Text != "")
                obj_industrialNewApplication.BharatTransReferanceNumber = Convert.ToInt64(txtBharatKoshRefferenceNo.Text);
            else
                obj_industrialNewApplication.BharatTransReferanceNumber = null;
            if (txtBharatKoshDated.Text != "")
                obj_industrialNewApplication.BharatTransDated = Convert.ToDateTime(txtBharatKoshDated.Text);
            else
                obj_industrialNewApplication.BharatTransDated = null;


            if (obj_industrialNewApplication.Update() == 1)
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

                lblMessage.Text = obj_industrialNewApplication.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }

        }
        catch (Exception)
        {
            strActionName = "SaveAsDraft";
            strStatus = "Record Save Failed !";

            //lblMessage.Text = ex.Message;
            //lblMessage.ForeColor = System.Drawing.Color.Red;
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
        try
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
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
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
                UpdateSelfDeclarationDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                //if (chkBoxUndertaking.Checked)
                //{
                //    UpdateSelfDeclarationDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                //}
                //else
                //{
                //    lblMessage.Text = "Please accept Undertaking";
                //    lblMessage.ForeColor = System.Drawing.Color.Red;
                //    Page.SetFocus(chkBoxUndertaking);
                //}
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
            if (IsValid)
            {
                if (UpdateSelfDeclarationDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text)) == 1)
                {
                    Server.Transfer("Attachment.aspx");
                }
            }

            //if (chkBoxUndertaking.Checked)
            //{
            //    if (IsValid)
            //    {
            //        if (UpdateSelfDeclarationDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text)) == 1)
            //        {
            //            Server.Transfer("Attachment.aspx");
            //        }
            //    }

            //}
            //else
            //{
            //    lblMessage.Text = "Please accept Undertaking";
            //    lblMessage.ForeColor = System.Drawing.Color.Red;
            //    Page.SetFocus(chkBoxUndertaking);
            //}
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