using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using NOCAP;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_Expansion_IND_OtherDetails : System.Web.UI.Page
{
    string strPageName = "OtherDetails";
    string strActionName = "";
    string strStatus = "";


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ValidationExpInit();
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
                if (lblIndustialApplicationCodeFrom.Text.Trim() != "")
                {
                    BindOtherDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "function", "GetData();", true);
    }


    private void ValidationExpInit()
    {
    
        //revtxtReferralLetterNo.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtReferralLetterNo.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;
        //revtxtGroundwaterAvailabilityDetails.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtGroundwaterAvailabilityDetails.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //revLengthtxtGroundwaterAvailabilityDetails.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        // revLengthtxtGroundwaterAvailabilityDetails.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");

        //revtxtRrainwaterDetails.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtRrainwaterDetails.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //      revLengthtxtRrainwaterDetails.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        //        revLengthtxtRrainwaterDetails.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");

        revtxtEarlierAppliedGWClearDesc.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtEarlierAppliedGWClearDesc.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtEarlierAppliedGWClearDesc.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        revLengthtxtEarlierAppliedGWClearDesc.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");
    }

    private void BindOtherDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(lngA_ApplicationCode);

            //txtGroundwaterAvailabilityDetails.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.GroundWaterAvailability.GroundWaterAvailabilityDesc);
            // txtRrainwaterDetails.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc);
            // txtReferralLetterNo.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.ReferralLetterNo);
            if (obj_industrialNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance == NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.Yes)
            {
                chkboxEarlierAppliedGWClear.Checked = true;
                txtEarlierAppliedGWClearDesc.Enabled = true;
            }
            else
            {
                chkboxEarlierAppliedGWClear.Checked = false;
                txtEarlierAppliedGWClearDesc.Enabled = false;
            }
            txtEarlierAppliedGWClearDesc.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceDesc);
           
            //BindIndustrialNewReferralLetterDetails(lngA_ApplicationCode);
            //NOCAP.BLL.Master.State obj_State = new NOCAP.BLL.Master.State(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode);
            //Response.Write(obj_State.GetAssociatedRegionalOffice().RegionalOfficeName);

            //Response.Write(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().RegionalOfficeName);

        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
            //lblMessage.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    //private void BindIndustrialNewReferralLetterDetails(long lngA_ApplicationCode)
    //{
    //    try
    //    {
    //        NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(lngA_ApplicationCode);
    //        //NOCAP.BLL.Industrial.Expansion.IndustrialNewSADReferralLetter obj_industrialNewReferralLetter = new NOCAP.BLL.Industrial.Expansion.IndustrialNewSADReferralLetter(lngA_ApplicationCode, 1);
    //        NOCAP.BLL.Industrial.Expansion.IndustrialNewSADReferralLetter obj_industrialNewReferralLetterBlank = new NOCAP.BLL.Industrial.Expansion.IndustrialNewSADReferralLetter();
    //        List<NOCAP.BLL.Industrial.Expansion.IndustrialNewSADReferralLetter> lst_industrialNewReferralLetterList = new List<NOCAP.BLL.Industrial.Expansion.IndustrialNewSADReferralLetter>();
    //        NOCAP.BLL.Industrial.Expansion.IndustrialNewSADReferralLetter[] arr_IndustrialNewReferralLetterList;
    //        arr_IndustrialNewReferralLetterList = obj_industrialNewApplication.GetIndustrialNewReferralLetterList();

    //        if (arr_IndustrialNewReferralLetterList.Count() > 0)
    //        {

    //            gvIndRefLetter.DataSource = arr_IndustrialNewReferralLetterList;
    //            gvIndRefLetter.DataBind();

    //        }
    //        else
    //        {

    //            lst_industrialNewReferralLetterList.Add(obj_industrialNewReferralLetterBlank);
    //            gvIndRefLetter.DataSource = lst_industrialNewReferralLetterList;
    //            gvIndRefLetter.DataBind();
    //            int int_NoOfCol = 0;
    //            int_NoOfCol = gvIndRefLetter.Rows[0].Cells.Count;
    //            gvIndRefLetter.Rows[0].Cells.Clear();
    //            gvIndRefLetter.Rows[0].Cells.Add(new TableCell());
    //            gvIndRefLetter.Rows[0].Cells[0].ColumnSpan = int_NoOfCol;
    //            gvIndRefLetter.Rows[0].Cells[0].Text = "No Records exsist in IndustrialNewReferralLetter";

    //        }
    //    }
    //    catch (Exception)
    //    {
    //        //lblMessage.Text = ex.Message;
    //        //lblMessage.ForeColor = System.Drawing.Color.Red;
    //        Response.Redirect("~/ExternalErrorPage.aspx", false);
    //    }

    //}
    protected void gvIndRefLetter_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label lbl_ReferralLetterTypeName = null;
        Label lbl_ReferralLetterTypeCode = null;

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                lbl_ReferralLetterTypeCode = (Label)e.Row.FindControl("ReferralLetterTypeCode");
                lbl_ReferralLetterTypeName = (Label)e.Row.FindControl("ReferralLetterTypeName");
                NOCAP.BLL.Industrial.Expansion.IndustrialExpansionReferralLetter obj_industrialNewReferralLetter = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionReferralLetter(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text), Convert.ToInt32(lbl_ReferralLetterTypeCode.Text));

                lbl_ReferralLetterTypeName.Text = HttpUtility.HtmlEncode(obj_industrialNewReferralLetter.GetReferralLetterTypeDesc());
            }
        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
            //lblMessage.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    private int UpdateOtherDetails(long lngA_ApplicationCode)
    {

        try
        {

            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(lngA_ApplicationCode);
            obj_industrialNewApplication.GroundWaterAvailability.GroundWaterAvailabilityDesc = "";
            obj_industrialNewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc = "";
            obj_industrialNewApplication.ReferralLetterNo = "";// txtReferralLetterNo.Text;
            NOCAP.BLL.Master.PackDrinkCharge obj_PackDrinkCharge = null;
            NOCAP.BLL.Master.IndustrialCharge obj_IndustrialCharge = null;
            //if (txtGroundwaterAvailabilityDetails.Text.Trim() == "")
            //{
            //   obj_industrialNewApplication.GroundWaterAvailability.GroundWaterAvailabilityDesc = "";
            //}
            //else
            //{
            //    obj_industrialNewApplication.GroundWaterAvailability.GroundWaterAvailabilityDesc = txtGroundwaterAvailabilityDetails.Text.Trim();
            //}
            //if (txtRrainwaterDetails.Text.Trim() == "")
            //{
            //    obj_industrialNewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc = "";
            //}
            //else
            //{
            //    obj_industrialNewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc = txtRrainwaterDetails.Text.Trim();
            //}
            if (chkboxEarlierAppliedGWClear.Checked)
            {
                obj_industrialNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance = NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.Yes;
            }
            else
            {
                obj_industrialNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance = NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.No;
            }
            if (txtEarlierAppliedGWClearDesc.Text.Trim() == "")
            {
                obj_industrialNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceDesc = "";
            }
            else
            {
                obj_industrialNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceDesc = txtEarlierAppliedGWClearDesc.Text.Trim();
            }
          
          
            obj_industrialNewApplication.WaterChargeStatus = NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.WaterChargeStatusYesNo.Yes;
            int intA_AreaTypeCatCode = AreaTypeCatCode(lngA_ApplicationCode);
            if (obj_industrialNewApplication.ApplicationTypeCategoryCode.ToString() == "73")
            {
                obj_PackDrinkCharge = new NOCAP.BLL.Master.PackDrinkCharge(1, intA_AreaTypeCatCode, Convert.ToDecimal(obj_industrialNewApplication.GWChargesQty));
                obj_industrialNewApplication.WaterChargeTypeCode = obj_PackDrinkCharge.WaterChargeTypeCode;
            }
            else
            {
                obj_IndustrialCharge = new NOCAP.BLL.Master.IndustrialCharge(1, intA_AreaTypeCatCode, Convert.ToDecimal(obj_industrialNewApplication.GWChargesQty));
                obj_industrialNewApplication.WaterChargeTypeCode = obj_IndustrialCharge.WaterChargeTypeCode;
            }





            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_industrialNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

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
        catch (Exception ex)
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
    private decimal CalculateCharges(long lngA_ApplicationCode, decimal dec_Qty, int int_days)
    {
        decimal result = 0m;
        int intA_AreaTypeCatCode = AreaTypeCatCode(lngA_ApplicationCode);
        NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_IndustrialExpansionApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(lngA_ApplicationCode);
        NOCAP.BLL.Master.PackDrinkCharge obj_PackDrinkCharge = null;
        NOCAP.BLL.Master.IndustrialCharge obj_IndustrialCharge = null;

        if (obj_IndustrialExpansionApplication.ApplicationTypeCategoryCode.ToString() == "73")
        {
            obj_PackDrinkCharge = new NOCAP.BLL.Master.PackDrinkCharge(1, intA_AreaTypeCatCode, Convert.ToDecimal(dec_Qty));
            result = Convert.ToDecimal(dec_Qty) * obj_PackDrinkCharge.Rate * Convert.ToInt32(int_days);

        }
        else
        {
            obj_IndustrialCharge = new NOCAP.BLL.Master.IndustrialCharge(1, intA_AreaTypeCatCode, dec_Qty);
            result = Convert.ToDecimal(dec_Qty) * obj_IndustrialCharge.Rate * Convert.ToInt32(int_days);
        }
        return result;
    }
    private int AreaTypeCatCode(long lngA_ApplicationCode)
    {
        NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(lngA_ApplicationCode);
        NOCAP.BLL.Master.SubDistrict obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(Convert.ToInt32(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode), Convert.ToInt32(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode), Convert.ToInt32(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode));
        NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_subDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplication.ApplySubDistrictAreaCategoryKey);
        obj_subDistrictAreaTypeCategoryHistory = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory();
        string AreaTypeCatCode = obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode.ToString();
        NOCAP.BLL.Master.AreaTypeCategory obj_AreaTypeCategory = new NOCAP.BLL.Master.AreaTypeCategory(obj_subDistrictAreaTypeCategoryHistory.AreaTypeCode, obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode);
        return obj_AreaTypeCategory.AreaTypeCategoryCode;
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
                if (UpdateOtherDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text)) == 1)
                {
                    // Server.Transfer("SelfDeclaration.aspx");
                    Server.Transfer("Attachment.aspx");
                }
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
                try
                {
                    UpdateOtherDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
                }
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
            Server.Transfer("DetailsProposedGroundwaterAbstractionStructure.aspx");
        }
    }


    protected void chkboxEarlierAppliedGWClear_CheckedChanged(object sender, EventArgs e)
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

                if (chkboxEarlierAppliedGWClear.Checked)
                {
                    txtEarlierAppliedGWClearDesc.Enabled = true;
                }
                else
                {
                    txtEarlierAppliedGWClearDesc.Enabled = false;
                    txtEarlierAppliedGWClearDesc.Text = "";
                }

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
}