using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_InfrastructureNew_OtherDetails : System.Web.UI.Page
{
    string strPageName = "INFOtherDetails";
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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblInfrastructureApplicationCodeFrom");
                        if (SourceLabel != null) { lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    BindOtherDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
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
    //    revtxtReferralLetterNo.ValidationExpression = ValidationUtility.txtValNAMultiLine;
    //    revtxtReferralLetterNo.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;
        //revtxtGroundwaterAvailabilityDetails.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtGroundwaterAvailabilityDetails.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //revLengthtxtGroundwaterAvailabilityDetails.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        //revLengthtxtGroundwaterAvailabilityDetails.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");

        //revtxtRrainwaterDetails.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtRrainwaterDetails.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //revLengthtxtRrainwaterDetails.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        //revLengthtxtRrainwaterDetails.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");

        //revtxtStateGovtApprovalLetter.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtStateGovtApprovalLetter.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //revLengthtxtStateGovtApprovalLetter.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        //revLengthtxtStateGovtApprovalLetter.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");

        revtxtEarlierAppliedGWClearDesc.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtEarlierAppliedGWClearDesc.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtEarlierAppliedGWClearDesc.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        revLengthtxtEarlierAppliedGWClearDesc.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");
    }

    private void BindOtherDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
           // txtGroundwaterAvailabilityDetails.Text = obj_infrastructureNewApplication.GroundWaterAvailability.GroundWaterAvailabilityDesc;
           // txtRrainwaterDetails.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc);
           // txtReferralLetterNo.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.ReferralLetterNo);
            if (obj_infrastructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance == NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.Yes)
            {
                chkboxEarlierAppliedGWClear.Checked = true;
                txtEarlierAppliedGWClearDesc.Enabled = true;
            }
            else
            {
                chkboxEarlierAppliedGWClear.Checked = false;
                txtEarlierAppliedGWClearDesc.Enabled = false;
            }
            txtEarlierAppliedGWClearDesc.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceDesc);
           // txtStateGovtApprovalLetter.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.ApprovalLetterofStateGovtAgency.ApprovalLetterDesc);
          //  BindInfrastructureNewReferralLetterDetails(lngA_ApplicationCode);
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
    //        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
    //        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter obj_infrastructureNewReferralLetterBlank = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter();
    //        List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter> lst_infrastructureNewReferralLetterList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter>();
    //        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter[] arr_InfrastructureNewReferralLetterList;
    //        arr_InfrastructureNewReferralLetterList = obj_infrastructureNewApplication.GetInfraNewReferralLetterList();
    //        if (arr_InfrastructureNewReferralLetterList.Count() > 0)
    //        {
    //            gvIndRefLetter.DataSource = arr_InfrastructureNewReferralLetterList;
    //            gvIndRefLetter.DataBind();
    //        }
    //        else
    //        {
    //            lst_infrastructureNewReferralLetterList.Add(obj_infrastructureNewReferralLetterBlank);
    //            gvIndRefLetter.DataSource = lst_infrastructureNewReferralLetterList;
    //            gvIndRefLetter.DataBind();
    //            int int_NoOfCol = 0;
    //            int_NoOfCol = gvIndRefLetter.Rows[0].Cells.Count;
    //            gvIndRefLetter.Rows[0].Cells.Clear();
    //            gvIndRefLetter.Rows[0].Cells.Add(new TableCell());
    //            gvIndRefLetter.Rows[0].Cells[0].ColumnSpan = int_NoOfCol;
    //            gvIndRefLetter.Rows[0].Cells[0].Text = "No Records exsist in Infrastructure New Referral Letter";
    //        }
    //        if (obj_infrastructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance == NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.Yes)
    //        {
    //            txtEarlierAppliedGWClearDesc.Text = obj_infrastructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceDesc;
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
                //Server.Transfer("ProposedGroundwaterAbstractionStructure.aspx");
                Server.Transfer("WaterSupplyDetail.aspx");
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
                UpdateOtherDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
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
                if (UpdateOtherDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text)) == 1) { Server.Transfer("~/ExternalUser/InfrastructureNew/Attachment.aspx"); }
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
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter obj_infrastructureNewReferralLetter = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADReferralLetter(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text), Convert.ToInt32(lbl_ReferralLetterTypeCode.Text));
                lbl_ReferralLetterTypeName.Text =  HttpUtility.HtmlEncode(obj_infrastructureNewReferralLetter.GetReferralLetterTypeDesc());
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

    private int UpdateOtherDetails(long lngA_ApplicationCode)
    {
        try
        {


            strActionName = "UpdateOtherDetails";
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);

            //obj_infrastructureNewApplication.ReferralLetterNo = txtReferralLetterNo.Text;
            obj_infrastructureNewApplication.ReferralLetterNo = "";
            obj_infrastructureNewApplication.GroundWaterAvailability.GroundWaterAvailabilityDesc = "";
            obj_infrastructureNewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc = "";
            obj_infrastructureNewApplication.ApprovalLetterofStateGovtAgency.ApprovalLetterDesc = "";
            //if (txtGroundwaterAvailabilityDetails.Text.Trim() == "")
            //{
            //    obj_infrastructureNewApplication.GroundWaterAvailability.GroundWaterAvailabilityDesc = "";
            //}
            //else
            //{
            //    obj_infrastructureNewApplication.GroundWaterAvailability.GroundWaterAvailabilityDesc = txtGroundwaterAvailabilityDetails.Text.Trim();
            //}
            //if (txtRrainwaterDetails.Text.Trim() == "")
            //{
            //    obj_infrastructureNewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc = "";
            //}
            //else
            //{
            //    obj_infrastructureNewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc = txtRrainwaterDetails.Text.Trim();
            //}

            //if (txtStateGovtApprovalLetter.Text.Trim() == "")
            //{
            //    obj_infrastructureNewApplication.ApprovalLetterofStateGovtAgency.ApprovalLetterDesc = "";
            //}
            //else { obj_infrastructureNewApplication.ApprovalLetterofStateGovtAgency.ApprovalLetterDesc = txtStateGovtApprovalLetter.Text; }

            if (chkboxEarlierAppliedGWClear.Checked)
            {
                obj_infrastructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance = NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.Yes;
            }
            else
            {
                obj_infrastructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance = NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.No;
            }
            if (txtEarlierAppliedGWClearDesc.Text.Trim() == "")
            {
                obj_infrastructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceDesc = "";
            }
            else
            {
                obj_infrastructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceDesc = txtEarlierAppliedGWClearDesc.Text.Trim();
            }

            //if (chkboxEarlierAppliedGWClear.Checked)
            //{
            //    obj_infrastructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance = NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.Yes;
            //    obj_infrastructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceDesc = txtEarlierAppliedGWClearDesc.Text;
            //}
            //else 
            //{ obj_infrastructureNewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance = NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.No; }

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_infrastructureNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_infrastructureNewApplication.Update() == 1)
            {
                strStatus = "Update Successfully";
                lblMessage.Text = "Successfully Saved";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                strStatus = "Update Unsuccessfull";
                lblMessage.Text = obj_infrastructureNewApplication.CustumMessage;
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
}