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

public partial class ExternalUser_InfrastructureRenew_OtherDetails : System.Web.UI.Page
{
    string strPageName = "INFRenewOtherDetails";
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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblInfrastructureApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                        }
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
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);

            lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewApplication.NameOfInfrastructure);

          //  txtGroundwaterAvailabilityDetails.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewApplication.GroundWaterAvailability.GroundWaterAvailabilityDesc);
            txtRrainwaterDetails.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc);

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private int UpdateOtherDetails(long lngA_ApplicationCode)
    {
        try
        {

            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);
            obj_InfrastructureRenewApplication.GroundWaterAvailability.GroundWaterAvailabilityDesc = "";
            //if (txtGroundwaterAvailabilityDetails.Text.Trim() == "")
            //{
            //    obj_InfrastructureRenewApplication.GroundWaterAvailability.GroundWaterAvailabilityDesc = "";
            //}
            //else
            //{
            //    obj_InfrastructureRenewApplication.GroundWaterAvailability.GroundWaterAvailabilityDesc = txtGroundwaterAvailabilityDetails.Text.Trim();
            //}
            if (txtRrainwaterDetails.Text.Trim() == "")
            {
                obj_InfrastructureRenewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc = "";
            }
            else
            {
                obj_InfrastructureRenewApplication.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc = txtRrainwaterDetails.Text.Trim();
            }



            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_InfrastructureRenewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_InfrastructureRenewApplication.Update() == 1)
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

                lblMessage.Text = obj_InfrastructureRenewApplication.CustumMessage;
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
                if (UpdateOtherDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text)) == 1)
                {
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
                    UpdateOtherDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));

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

}