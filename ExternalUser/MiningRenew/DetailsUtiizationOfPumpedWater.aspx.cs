using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_MiningRenew_DetailsUtiizationOfPumpedWater : System.Web.UI.Page
{
    string strPageName = "MINRenDetailsUtiizationOfPumpedWater";
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
                if (lblModeFrom.Text.Trim() == "Edit") { BindGVMinUtilizationPumpedWaterDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)); }
                ddlWatersupply_SelectedIndexChanged(sender, e);
                ddlAgriculture_SelectedIndexChanged(sender, e);
                ddlGBDevelopment_SelectedIndexChanged(sender, e);
                ddlSuppresionOFDust_SelectedIndexChanged(sender, e);
                ddlRecharge_SelectedIndexChanged(sender, e);
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
        revatxtWaterSuply.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revatxtWaterSuply.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        revtxtWaterSuplyDesc.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtWaterSuplyDesc.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtWaterSuplyDesc.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("100");
        revLengthtxtWaterSuplyDesc.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("100");

        revtxtAgriculture.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtAgriculture.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        revtxtAgricultureDesc.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtAgricultureDesc.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtAgricultureDesc.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("100");
        revLengthtxtAgricultureDesc.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("100");

        revatxtGBDevelopment.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revatxtGBDevelopment.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        revtxtGBDevelopmentDesc.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtGBDevelopmentDesc.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtGBDevelopmentDesc.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("100");
        revLengthtxtGBDevelopmentDesc.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("100");

        revatxtSuppervisionOfDust.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revatxtSuppervisionOfDust.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        revtxtSuppervisionOfDustDesc.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtSuppervisionOfDustDesc.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtSuppervisionOfDustDesc.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("100");
        revLengthtxtSuppervisionOfDustDesc.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("100");

        revtxtRecharge.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtRecharge.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        revtxtRechargeDesc.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtRechargeDesc.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtRechargeDesc.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("100");
        revLengthtxtRechargeDesc.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("100");

        revtxtPumpedWaterAnyOtherItem.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtPumpedWaterAnyOtherItem.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtPumpedWaterAnyOtherItem.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("100");
        revLengthtxtPumpedWaterAnyOtherItem.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("100");
    }

    private void BindGVMinUtilizationPumpedWaterDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Mining.New.Application.MiningNewApplication objMiningNewApplication = obj_MiningRenewApplication.GetFirstMiningApplication();

            if (objMiningNewApplication != null)
            {
                lblHeadingProjName.Text = HttpUtility.HtmlEncode(objMiningNewApplication.NameOfMining);
            }
            switch (obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumpedWaterSupply)
            {
                case NOCAP.BLL.Mining.Renew.Common.MiningRenewDetailUtilizationofPumpedWater.WaterSupplyStatus.Yes:
                    ddlWatersupply.SelectedValue = "Yes";
                    break;
                case NOCAP.BLL.Mining.Renew.Common.MiningRenewDetailUtilizationofPumpedWater.WaterSupplyStatus.No:
                    ddlWatersupply.SelectedValue = "No";
                    break;
            }
            txtWaterSuply.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterWS));
            txtWaterSuplyDesc.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterWSDetail));

            switch (obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumpedWaterinAgiculture)
            {
                case NOCAP.BLL.Mining.Renew.Common.MiningRenewDetailUtilizationofPumpedWater.AgricultureStatus.Yes:
                    ddlAgriculture.SelectedValue = "Yes";
                    break;
                case NOCAP.BLL.Mining.Renew.Common.MiningRenewDetailUtilizationofPumpedWater.AgricultureStatus.No:
                    ddlAgriculture.SelectedValue = "No";
                    break;
            }
            txtAgriculture.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterAgri));
            txtAgricultureDesc.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterAgriDetail));
            switch (obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumpedWaterinGreenBeltDevelopment)
            {
                case NOCAP.BLL.Mining.Renew.Common.MiningRenewDetailUtilizationofPumpedWater.GBDStatus.Yes:
                    ddlGBDevelopment.SelectedValue = "Yes";
                    break;
                case NOCAP.BLL.Mining.Renew.Common.MiningRenewDetailUtilizationofPumpedWater.GBDStatus.No:
                    ddlGBDevelopment.SelectedValue = "No";
                    break;
            }
            txtGBDevelopment.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterGD));
            txtGBDevelopmentDesc.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterGDDetail));
            switch (obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumpedWaterinSuppOfDust)
            {
                case NOCAP.BLL.Mining.Renew.Common.MiningRenewDetailUtilizationofPumpedWater.SuppOfDustStatus.Yes:
                    ddlSuppresionOFDust.SelectedValue = "Yes";
                    break;
                case NOCAP.BLL.Mining.Renew.Common.MiningRenewDetailUtilizationofPumpedWater.SuppOfDustStatus.No:
                    ddlSuppresionOFDust.SelectedValue = "No";
                    break;
            }
            txtSuppervisionOfDust.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterSuppDust));
            txtSuppervisionOfDustDesc.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterSuppDustDetail));
            switch (obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumpedWaterinRecharge)
            {
                case NOCAP.BLL.Mining.Renew.Common.MiningRenewDetailUtilizationofPumpedWater.RechargeStatus.Yes:
                    ddlRecharge.SelectedValue = "Yes";
                    break;
                case NOCAP.BLL.Mining.Renew.Common.MiningRenewDetailUtilizationofPumpedWater.RechargeStatus.No:
                    ddlRecharge.SelectedValue = "No";
                    break;
            }
            txtRecharge.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterRech));
            txtRechargeDesc.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterRechDetail));
            txtPumpedWaterAnyOtherItem.Text = HttpUtility.HtmlEncode(obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiAnyOtherItem);
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
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
                if (UpdateMinRenewUtilizationPumpedWaterDetail(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)) == 1) 
                { 
                    Server.Transfer("~/ExternalUser/MiningRenew/MonitorOfGroundWaterRegime.aspx"); 
                }
                else 
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
                }
            }
        }
    }

    private int UpdateMinRenewUtilizationPumpedWaterDetail(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);

            //switch (ddlDomesticUseInMines.SelectedValue)
            //{
            //    case "Yes":
            //        obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProDomesticUseInMine = NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.DomesticUseInMineStatus.Yes;
            //        obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProDomesticUseInMineDetail = txtDomesticUseInMinesDesc.Text;
            //        obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProDomesticUseInMines = Convert.ToDecimal(txtDomesticUseInMines.Text);
            //        break;
            //    case "No":
            //        obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProDomesticUseInMine = NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.DomesticUseInMineStatus.No;
            //        obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProDomesticUseInMineDetail = null;
            //        obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProDomesticUseInMines = null;
            //        break;
            //}

            switch (ddlWatersupply.SelectedValue)
            {
                case "Yes":
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumpedWaterSupply = NOCAP.BLL.Mining.Renew.Common.MiningRenewDetailUtilizationofPumpedWater.WaterSupplyStatus.Yes;
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterWSDetail = txtWaterSuplyDesc.Text;
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterWS = Convert.ToDecimal(txtWaterSuply.Text);
                    break;
                case "No":
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumpedWaterSupply = NOCAP.BLL.Mining.Renew.Common.MiningRenewDetailUtilizationofPumpedWater.WaterSupplyStatus.No;
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterWSDetail = null;
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterWS = null;
                    break;
            }
            switch (ddlAgriculture.SelectedValue)
            {
                case "Yes":
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumpedWaterinAgiculture = NOCAP.BLL.Mining.Renew.Common.MiningRenewDetailUtilizationofPumpedWater.AgricultureStatus.Yes;
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterAgri = Convert.ToDecimal(txtAgriculture.Text);
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterAgriDetail = txtAgricultureDesc.Text;
                    break;
                case "No":
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumpedWaterinAgiculture = NOCAP.BLL.Mining.Renew.Common.MiningRenewDetailUtilizationofPumpedWater.AgricultureStatus.No;
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterAgri = null;
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterAgriDetail = null;
                    break;
            }

            switch (ddlGBDevelopment.SelectedValue)
            {
                case "Yes":
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumpedWaterinGreenBeltDevelopment = NOCAP.BLL.Mining.Renew.Common.MiningRenewDetailUtilizationofPumpedWater.GBDStatus.Yes;
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterGD = Convert.ToDecimal(txtGBDevelopment.Text);
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterGDDetail = txtGBDevelopmentDesc.Text;
                    break;
                case "No":
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumpedWaterinGreenBeltDevelopment = NOCAP.BLL.Mining.Renew.Common.MiningRenewDetailUtilizationofPumpedWater.GBDStatus.No;
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterGD = null;
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterGDDetail = null;
                    break;
            }

            switch (ddlSuppresionOFDust.SelectedValue)
            {
                case "Yes":
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumpedWaterinSuppOfDust = NOCAP.BLL.Mining.Renew.Common.MiningRenewDetailUtilizationofPumpedWater.SuppOfDustStatus.Yes;
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterSuppDust = Convert.ToDecimal(txtSuppervisionOfDust.Text);
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterSuppDustDetail = txtSuppervisionOfDustDesc.Text;
                    break;
                case "No":
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumpedWaterinSuppOfDust = NOCAP.BLL.Mining.Renew.Common.MiningRenewDetailUtilizationofPumpedWater.SuppOfDustStatus.No;
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterSuppDustDetail = null;
                    break;
            }

            switch (ddlRecharge.SelectedValue)
            {
                case "Yes":
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumpedWaterinRecharge = NOCAP.BLL.Mining.Renew.Common.MiningRenewDetailUtilizationofPumpedWater.RechargeStatus.Yes;
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterRech = Convert.ToDecimal(txtRecharge.Text);
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterRechDetail = txtRechargeDesc.Text;
                    break;
                case "No":
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumpedWaterinRecharge = NOCAP.BLL.Mining.Renew.Common.MiningRenewDetailUtilizationofPumpedWater.RechargeStatus.No;
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterRech = null;
                    obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiPumWaterRechDetail = null;
                    break;
            }

            obj_MiningRenewApplication.MiningRenewDetailUtilizationofPumpedWater.DetailUtiAnyOtherItem = txtPumpedWaterAnyOtherItem.Text;
            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_MiningRenewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_MiningRenewApplication.Update() == 1)
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

                lblMessage.Text = obj_MiningRenewApplication.CustumMessage;
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
                UpdateMinRenewUtilizationPumpedWaterDetail(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
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
            Server.Transfer("~/ExternalUser/MiningRenew/De-WateringAdditionalStructure.aspx");
        }
    }
    protected void ddlWatersupply_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            if (ddlWatersupply.SelectedValue == "Yes") { txtWaterSuply.Enabled = true; txtWaterSuplyDesc.Enabled = true; revtxtWaterSuply.Enabled = true; }
            else { txtWaterSuply.Enabled = false; txtWaterSuply.Text = ""; revtxtWaterSuply.Enabled = false; txtWaterSuplyDesc.Enabled = false; txtWaterSuplyDesc.Text = ""; }
        }
    }
    protected void ddlAgriculture_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            if (ddlAgriculture.SelectedValue == "Yes")
            {
                txtAgriculture.Enabled = true;
                txtAgricultureDesc.Enabled = true;
                revddlAgriculture.Enabled = true;
            }
            else
            {
                txtAgriculture.Enabled = false;
                txtAgriculture.Text = "";
                txtAgricultureDesc.Enabled = false;
                txtAgricultureDesc.Text = "";
                revddlAgriculture.Enabled = false;
            }

        }
    }
    protected void ddlGBDevelopment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            if (ddlGBDevelopment.SelectedValue == "Yes")
            {
                txtGBDevelopment.Enabled = true;
                txtGBDevelopmentDesc.Enabled = true;
                revtxtGBDevelopment.Enabled = true;
            }
            else
            {
                txtGBDevelopment.Enabled = false;
                txtGBDevelopment.Text = "";
                txtGBDevelopmentDesc.Enabled = false;
                txtGBDevelopmentDesc.Text = "";
                revtxtGBDevelopment.Enabled = false;
            }
        }
    }
    protected void ddlSuppresionOFDust_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            if (ddlSuppresionOFDust.SelectedValue == "Yes")
            {
                txtSuppervisionOfDust.Enabled = true;
                txtSuppervisionOfDustDesc.Enabled = true;
                revtxtSuppervisionOfDust.Enabled = true;
            }
            else
            {
                txtSuppervisionOfDust.Enabled = false;
                txtSuppervisionOfDust.Text = "";
                txtSuppervisionOfDustDesc.Enabled = false;
                txtSuppervisionOfDustDesc.Text = "";
                revtxtSuppervisionOfDust.Enabled = false;
            }
        }
    }
    protected void ddlRecharge_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            if (ddlRecharge.SelectedValue == "Yes")
            {
                txtRecharge.Enabled = true;
                txtRechargeDesc.Enabled = true;
                rfvtxtRecharge.Enabled = true;
            }
            else
            {
                txtRecharge.Enabled = false;
                txtRecharge.Text = "";
                txtRechargeDesc.Enabled = false;
                txtRechargeDesc.Text = "";
                rfvtxtRecharge.Enabled = false;
            }
        }
    }
    
}