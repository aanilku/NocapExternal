using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_Expansion_MIN_OtherDetails : System.Web.UI.Page
{
    string strPageName = "MINOtherDetails";
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
                if (lblMiningApplicationCodeFrom.Text.Trim() != "")
                {
                    BindOtherDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
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
        ScriptManager.RegisterStartupScript(this, GetType(), "function", "GetData();", true);
    }

    private void ValidationExpInit()
    {
        //revtxtReferralLetterNo.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtReferralLetterNo.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //revtxtGroundwaterAvailabilityDetails.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtGroundwaterAvailabilityDetails.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;


        //revLengthtxtGroundwaterAvailabilityDetails.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        //revLengthtxtGroundwaterAvailabilityDetails.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");

        //revtxtRrainwaterDetails.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtRrainwaterDetails.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //revLengthtxtRrainwaterDetails.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        //revLengthtxtRrainwaterDetails.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");

        revtxtEarlierAppliedGWClearDesc.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtEarlierAppliedGWClearDesc.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtEarlierAppliedGWClearDesc.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        revLengthtxtEarlierAppliedGWClearDesc.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");
    }

    private void BindOtherDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.Expansion.MiningExpansionApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.Expansion.MiningExpansionApplication(lngA_ApplicationCode);
           // txtGroundwaterAvailabilityDetails.Text = obj_MiningNewApplication.GroundWaterAvailability.GroundWaterAvailabilityDesc;
           // txtRrainwaterDetails.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc);
           // txtReferralLetterNo.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.ReferralLetterNo);
            if (obj_MiningNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance == NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.Yes)
            {
                chkboxEarlierAppliedGWClear.Checked = true;
                txtEarlierAppliedGWClearDesc.Enabled = true;
            }
            else
            {
                chkboxEarlierAppliedGWClear.Checked = false;
                txtEarlierAppliedGWClearDesc.Enabled = false;
            }
            txtEarlierAppliedGWClearDesc.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceDesc);

            // BindMiningNewReferralLetterDetails(lngA_ApplicationCode);
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally
        {
        }
    }
    //private void BindInfrastructureNewReferralLetterDetails(long lngA_ApplicationCode)
    //{
    //    try
    //    {
    //        NOCAP.BLL.Mining.Expansion.MiningExpansionApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.Expansion.MiningExpansionApplication(lngA_ApplicationCode);
    //        NOCAP.BLL.Mining.Expansion.MiningNewSADReferralLetter obj_MiningNewReferralLetterBlank = new NOCAP.BLL.Mining.Expansion.MiningNewSADReferralLetter();
    //        List<NOCAP.BLL.Mining.Expansion.MiningNewSADReferralLetter> lst_MiningNewReferralLetterList = new List<NOCAP.BLL.Mining.Expansion.MiningNewSADReferralLetter>();
    //        NOCAP.BLL.Mining.Expansion.MiningNewSADReferralLetter[] arr_MiningNewReferralLetterList;
    //        arr_MiningNewReferralLetterList = obj_MiningNewApplication.GetMiningNewReferralLetterList();
    //        if (arr_MiningNewReferralLetterList.Count() > 0)
    //        {
    //            gvIndRefLetter.DataSource = arr_MiningNewReferralLetterList;
    //            gvIndRefLetter.DataBind();
    //        }
    //        else
    //        {
    //            lst_MiningNewReferralLetterList.Add(obj_MiningNewReferralLetterBlank);
    //            gvIndRefLetter.DataSource = lst_MiningNewReferralLetterList;
    //            gvIndRefLetter.DataBind();
    //            int int_NoOfCol = 0;
    //            int_NoOfCol = gvIndRefLetter.Rows[0].Cells.Count;
    //            gvIndRefLetter.Rows[0].Cells.Clear();
    //            gvIndRefLetter.Rows[0].Cells.Add(new TableCell());
    //            gvIndRefLetter.Rows[0].Cells[0].ColumnSpan = int_NoOfCol;
    //            gvIndRefLetter.Rows[0].Cells[0].Text = "No Records exsist in Mining New Referral Letter";
    //        }
    //        if (obj_MiningNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance == NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.Yes)
    //        {
    //            txtEarlierAppliedGWClearDesc.Text = obj_MiningNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceDesc;
    //            chkboxEarlierAppliedGWClear.Checked = true;
    //        }
    //        else
    //        {
    //            chkboxEarlierAppliedGWClear.Checked = false;
    //            txtEarlierAppliedGWClearDesc.Text = "";
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        Response.Redirect("~/ExternalErrorPage.aspx", false);
    //    }
    //    finally
    //    {
    //    }
    //}
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
            if (Page.IsValid)
            {
                Server.Transfer("~/ExternalUser/Expansion/MIN/DetailsProposedGroundwaterAbstractionStructure.aspx");
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
            if (Page.IsValid)
            {
                UpdateOtherDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
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
            if (Page.IsValid)
            {
                if (UpdateOtherDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text)) == 1)
                {
                    try
                    {
                        Server.Transfer("~/ExternalUser/Expansion/MIN/Attachment.aspx");
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                }
            }
        }
    }
    private int UpdateOtherDetails(long lngA_ApplicationCode)
    {
        try
        {

            NOCAP.BLL.Mining.Expansion.MiningExpansionApplication obj_MiningstructureNewApplication = new NOCAP.BLL.Mining.Expansion.MiningExpansionApplication(lngA_ApplicationCode);
            obj_MiningstructureNewApplication.ReferralLetterNo = "";// txtReferralLetterNo.Text;
            obj_MiningstructureNewApplication.GroundWaterAvailability.GroundWaterAvailabilityDesc = "";
            obj_MiningstructureNewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc = "";
            //if (txtGroundwaterAvailabilityDetails.Text.Trim() == "")
            //{
            //    obj_MiningstructureNewApplication.GroundWaterAvailability.GroundWaterAvailabilityDesc = "";
            //}
            //else
            //{
            //    obj_MiningstructureNewApplication.GroundWaterAvailability.GroundWaterAvailabilityDesc = txtGroundwaterAvailabilityDetails.Text.Trim();
            //}
            //if (txtRrainwaterDetails.Text.Trim() == "")
            //{
            //    obj_MiningstructureNewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc = "";
            //}
            //else
            //{
            //    obj_MiningstructureNewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc = txtRrainwaterDetails.Text.Trim();
            //}

            //if (txtStateGovtApprovalLetter.Text.Trim() == "") { obj_MiningstructureNewApplication.ApprovalLetterofStateGovtAgency = null; }
            //else { obj_MiningstructureNewApplication.ApprovalLetterofStateGovtAgency = txtStateGovtApprovalLetter.Text; }

            if (chkboxEarlierAppliedGWClear.Checked)
            {
                obj_MiningstructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance = NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.Yes;
            }
            else
            {
                obj_MiningstructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance = NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.No;
            }
            if (txtEarlierAppliedGWClearDesc.Text.Trim() == "")
            {
                obj_MiningstructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceDesc = "";
            }
            else
            {
                obj_MiningstructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceDesc = txtEarlierAppliedGWClearDesc.Text.Trim();
            }

            if (chkboxEarlierAppliedGWClear.Checked)
            {
                obj_MiningstructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance = NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.Yes;
                obj_MiningstructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceDesc = txtEarlierAppliedGWClearDesc.Text;
            }
            else
            { obj_MiningstructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance = NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.No; }

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_MiningstructureNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_MiningstructureNewApplication.Update() == 1)
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

                lblMessage.Text = obj_MiningstructureNewApplication.CustumMessage;
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
    }
    //private void BindMiningNewReferralLetterDetails(long lngA_ApplicationCode)
    //{
    //    try
    //    {   
    //        NOCAP.BLL.Mining.Expansion.MiningExpansionApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.Expansion.MiningExpansionApplication(lngA_ApplicationCode);
    //        NOCAP.BLL.Mining.Expansion.MiningNewSADReferralLetter obj_MiningNewReferralLetterBlank = new NOCAP.BLL.Mining.Expansion.MiningNewSADReferralLetter();
    //        List<NOCAP.BLL.Mining.Expansion.MiningNewSADReferralLetter> lst_MiningNewReferralLetterList = new List<NOCAP.BLL.Mining.Expansion.MiningNewSADReferralLetter>();
    //        NOCAP.BLL.Mining.Expansion.MiningNewSADReferralLetter[] arr_MiningNewReferralLetterList;
    //        arr_MiningNewReferralLetterList = obj_MiningNewApplication.GetMiningNewReferralLetterList();
    //        if (arr_MiningNewReferralLetterList.Count() > 0)
    //        {

    //            gvMINRefLetter.DataSource = arr_MiningNewReferralLetterList;
    //            gvMINRefLetter.DataBind();

    //        }
    //        else
    //        {

    //            lst_MiningNewReferralLetterList.Add(obj_MiningNewReferralLetterBlank);
    //            gvMINRefLetter.DataSource = lst_MiningNewReferralLetterList;
    //            gvMINRefLetter.DataBind();
    //            int int_NoOfCol = 0;
    //            int_NoOfCol = gvMINRefLetter.Rows[0].Cells.Count;
    //            gvMINRefLetter.Rows[0].Cells.Clear();
    //            gvMINRefLetter.Rows[0].Cells.Add(new TableCell());
    //            gvMINRefLetter.Rows[0].Cells[0].ColumnSpan = int_NoOfCol;
    //            gvMINRefLetter.Rows[0].Cells[0].Text = "No Records exsist in MiningNewReferralLetter";

    //        }
    //    }
    //    catch (Exception)
    //    {
    //        Response.Redirect("~/ExternalErrorPage.aspx", false);
    //    }

    //}
    protected void gvMINRefLetter_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label lbl_ReferralLetterTypeName = null;
        Label lbl_ReferralLetterTypeCode = null;

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                lbl_ReferralLetterTypeCode = (Label)e.Row.FindControl("ReferralLetterTypeCode");
                lbl_ReferralLetterTypeName = (Label)e.Row.FindControl("ReferralLetterTypeName");
                NOCAP.BLL.Mining.Expansion.MiningExpansionReferralLetter obj_MiningNewReferralLetter = new NOCAP.BLL.Mining.Expansion.MiningExpansionReferralLetter(Convert.ToInt64(lblMiningApplicationCodeFrom.Text), Convert.ToInt32(lbl_ReferralLetterTypeCode.Text));
                lbl_ReferralLetterTypeName.Text = HttpUtility.HtmlEncode(obj_MiningNewReferralLetter.GetReferralLetterTypeDesc());
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