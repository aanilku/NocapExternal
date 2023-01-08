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

public partial class ExternalUser_IndustrialRenew_OtherDetails : System.Web.UI.Page
{
    string strPageName = "INDRenewOtherDetails";
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
                if (lblModeFrom.Text.Trim() == "Edit")
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
        //revtxtGroundwaterAvailabilityDetails.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtGroundwaterAvailabilityDetails.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //revLengthtxtGroundwaterAvailabilityDetails.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        //revLengthtxtGroundwaterAvailabilityDetails.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");

        revtxtRrainwaterDetails.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtRrainwaterDetails.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtRrainwaterDetails.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        revLengthtxtRrainwaterDetails.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");

        //revtxtEarlierAppliedGWClearDesc.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtEarlierAppliedGWClearDesc.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //revLengthtxtEarlierAppliedGWClearDesc.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        //revLengthtxtEarlierAppliedGWClearDesc.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");
    }

    private void BindOtherDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);

            lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplication.NameOfIndustry);

           // txtGroundwaterAvailabilityDetails.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplication.GroundWaterAvailability.GroundWaterAvailabilityDesc);
            txtRrainwaterDetails.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc);
            //if (obj_industrialRenewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance == NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.Yes)
            //{
            //    chkboxEarlierAppliedGWClear.Checked = true;
            //    txtEarlierAppliedGWClearDesc.Enabled = true;
            //}
            //else
            //{
            //    chkboxEarlierAppliedGWClear.Checked = false;
            //    txtEarlierAppliedGWClearDesc.Enabled = false;
            //}
            //txtEarlierAppliedGWClearDesc.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceDesc);
           // BindIndustrialRenewReferralLetterDetails(lngA_ApplicationCode);     

        }
        catch (Exception)
        {           
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
         
    }
    //private void BindIndustrialRenewReferralLetterDetails(long lngA_ApplicationCode)
    //{
    //    try
    //    {
    //        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);
    //        //NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADReferralLetter obj_industrialRenewReferralLetter = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADReferralLetter(lngA_ApplicationCode, 1);
    //        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADReferralLetter obj_industrialRenewReferralLetterBlank = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADReferralLetter();
    //        List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADReferralLetter> lst_industrialRenewReferralLetterList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADReferralLetter>();
    //        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADReferralLetter[] arr_IndustrialRenewReferralLetterList;
    //        arr_IndustrialRenewReferralLetterList = obj_industrialRenewApplication.GetIndustrialRenewReferralLetterList();

    //        if (arr_IndustrialRenewReferralLetterList.Count() > 0)
    //        {

    //            gvIndRefLetter.DataSource = arr_IndustrialRenewReferralLetterList;
    //            gvIndRefLetter.DataBind();

    //        }
    //        else
    //        {

    //            lst_industrialRenewReferralLetterList.Add(obj_industrialRenewReferralLetterBlank);
    //            gvIndRefLetter.DataSource = lst_industrialRenewReferralLetterList;
    //            gvIndRefLetter.DataBind();
    //            int int_NoOfCol = 0;
    //            int_NoOfCol = gvIndRefLetter.Rows[0].Cells.Count;
    //            gvIndRefLetter.Rows[0].Cells.Clear();
    //            gvIndRefLetter.Rows[0].Cells.Add(new TableCell());
    //            gvIndRefLetter.Rows[0].Cells[0].ColumnSpan = int_NoOfCol;
    //            gvIndRefLetter.Rows[0].Cells[0].Text = "No Records exsist in IndustrialRenewReferralLetter";

    //        }
    //    }
    //    catch (Exception)
    //    {
    //        //lblMessage.Text = ex.Message;
    //        //lblMessage.ForeColor = System.Drawing.Color.Red;
    //        Response.Redirect("~/ExternalErrorPage.aspx", false);
    //    }
        
    //}

    //protected void gvIndRefLetter_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    Label lbl_ReferralLetterTypeName = null;
    //    Label lbl_ReferralLetterTypeCode = null;

    //    try
    //    {
    //        if(e.Row.RowType==DataControlRowType.DataRow)
    //        {
    //            lbl_ReferralLetterTypeCode = (Label)e.Row.FindControl("ReferralLetterTypeCode");
    //            lbl_ReferralLetterTypeName= (Label)e.Row.FindControl("ReferralLetterTypeName");
    //            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADReferralLetter obj_industrialRenewReferralLetter = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADReferralLetter(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text),Convert.ToInt32(lbl_ReferralLetterTypeCode.Text));

    //            lbl_ReferralLetterTypeName.Text= HttpUtility.HtmlEncode(obj_industrialRenewReferralLetter.GetReferralLetterTypeDesc());
    //        }
    //    }
    //    catch (Exception)
    //    {             
    //        Response.Redirect("~/ExternalErrorPage.aspx", false);
    //    }
         
    //}

    private int UpdateOtherDetails(long lngA_ApplicationCode)
    {
        try
        {

            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);
            obj_industrialRenewApplication.GroundWaterAvailability.GroundWaterAvailabilityDesc = "";
            //if (txtGroundwaterAvailabilityDetails.Text.Trim() == "")
            //{
            //    obj_industrialRenewApplication.GroundWaterAvailability.GroundWaterAvailabilityDesc = "";
            //}
            //else
            //{
            //    obj_industrialRenewApplication.GroundWaterAvailability.GroundWaterAvailabilityDesc = txtGroundwaterAvailabilityDetails.Text.Trim();
            //}
            if (txtRrainwaterDetails.Text.Trim() == "")
            {
                obj_industrialRenewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc = "";
            }
            else
            {
                obj_industrialRenewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc = txtRrainwaterDetails.Text.Trim();
            }
            //if (chkboxEarlierAppliedGWClear.Checked)
            //{
            //    obj_industrialRenewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance = NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.Yes;
            //}
            //else
            //{
            //    obj_industrialRenewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance = NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.No;
            //}
            //if (txtEarlierAppliedGWClearDesc.Text.Trim() == "")
            //{
            //    obj_industrialRenewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceDesc = "";
            //}
            //else
            //{
            //    obj_industrialRenewApplication.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceDesc = txtEarlierAppliedGWClearDesc.Text.Trim();
            //}


            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_industrialRenewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

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
                  //  Server.Transfer("SelfDeclaration.aspx");
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
            Server.Transfer("ComplianceConditionNOCOther.aspx");
        }
    }


    //protected void chkboxEarlierAppliedGWClear_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
    //    {
    //        Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
    //    }
    //    else
    //    {

    //        hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
    //        Session["CSRF"] = hidCSRF.Value;
    //        try
    //        {

    //            if (chkboxEarlierAppliedGWClear.Checked)
    //            {
    //                txtEarlierAppliedGWClearDesc.Enabled = true;
    //            }
    //            else
    //            {
    //                txtEarlierAppliedGWClearDesc.Enabled = false;
    //                txtEarlierAppliedGWClearDesc.Text = "";
    //            }

    //        }
    //        catch (Exception)
    //        {
    //            Response.Redirect("~/ExternalErrorPage.aspx", false);
    //        }
    //    }
    //}
}