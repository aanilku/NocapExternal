using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_Mining_ProposedUtiizationOfPumpedWater : System.Web.UI.Page
{
    string strPageName = "MINProposedUtiizationOfPumpedWater";
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
                ddlDomesticUseInMines_SelectedIndexChanged(sender, e);
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
        revatxtDomesticUseInMines.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revatxtDomesticUseInMines.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        revtxtDomesticUseInMinesDesc.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtDomesticUseInMinesDesc.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtDomesticUseInMinesDesc.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("100");
        revLengthtxtDomesticUseInMinesDesc.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("100");

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
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);

        switch (obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProDomesticUseInMine)
        {
            case NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.DomesticUseInMineStatus.Yes:
                ddlDomesticUseInMines.SelectedValue = "Yes";
                break;
            case NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.DomesticUseInMineStatus.No:
                ddlDomesticUseInMines.SelectedValue = "No";
                break;
        }
        txtDomesticUseInMines.Text = Convert.ToString(obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProDomesticUseInMines);
        txtDomesticUseInMinesDesc.Text = Convert.ToString(obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProDomesticUseInMineDetail);

        switch (obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumpedWaterSupply)
        {
            case NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.WaterSupplyStatus.Yes:
                ddlWatersupply.SelectedValue = "Yes";
                break;
            case NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.WaterSupplyStatus.No:
                ddlWatersupply.SelectedValue = "No";
                break;
        }
        txtWaterSuply.Text = Convert.ToString(obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterWS);
        txtWaterSuplyDesc.Text = Convert.ToString(obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterWSDetail);

        switch (obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumpedWaterinAgiculture)
        {
            case NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.AgricultureStatus.Yes:
                ddlAgriculture.SelectedValue = "Yes";
                break;
            case NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.AgricultureStatus.No:
                ddlAgriculture.SelectedValue = "No";
                break;
        }
        txtAgriculture.Text = Convert.ToString(obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterAgri);
        txtAgricultureDesc.Text = Convert.ToString(obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterAgriDetail);
        switch (obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumpedWaterinGreenBeltDevelopment)
        {
            case NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.GBDStatus.Yes:
                ddlGBDevelopment.SelectedValue = "Yes";
                break;
            case NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.GBDStatus.No:
                ddlGBDevelopment.SelectedValue = "No";
                break;
        }
        txtGBDevelopment.Text = Convert.ToString(obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterGD);
        txtGBDevelopmentDesc.Text = Convert.ToString(obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterGDDetail);
        switch (obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumpedWaterinSuppOfDust)
        {
            case NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.SuppOfDustStatus.Yes:
                ddlSuppresionOFDust.SelectedValue = "Yes";
                break;
            case NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.SuppOfDustStatus.No:
                ddlSuppresionOFDust.SelectedValue = "No";
                break;
        }
        txtSuppervisionOfDust.Text = Convert.ToString(obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterSuppDust);
        txtSuppervisionOfDustDesc.Text = Convert.ToString(obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterSuppDustDetail);
        switch (obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumpedWaterinRecharge)
        {
            case NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.RechargeStatus.Yes:
                ddlRecharge.SelectedValue = "Yes";
                break;
            case NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.RechargeStatus.No:
                ddlRecharge.SelectedValue = "No";
                break;
        }
        txtRecharge.Text = Convert.ToString(obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterRech);
        txtRechargeDesc.Text = Convert.ToString(obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterRechDetail);
        txtPumpedWaterAnyOtherItem.Text = obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiAnyOtherItem;
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
                if (UpdateMinUtilizationPumpedWaterDetail(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)) == 1) {
                    //    Server.Transfer("~/ExternalUser/MiningNew/MonitorOfGroundWaterRegime.aspx");
                    Server.Transfer("DetailsExistingGroundwaterAbstractionStructure.aspx");
                }
                else { }
            }
        }
    }

    private int UpdateMinUtilizationPumpedWaterDetail(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);

            switch (ddlDomesticUseInMines.SelectedValue)
            {
                case "Yes":
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProDomesticUseInMine = NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.DomesticUseInMineStatus.Yes;
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProDomesticUseInMineDetail = txtDomesticUseInMinesDesc.Text;
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProDomesticUseInMines = Convert.ToDecimal(txtDomesticUseInMines.Text);
                    break;
                case "No":
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProDomesticUseInMine = NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.DomesticUseInMineStatus.No;
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProDomesticUseInMineDetail = null;
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProDomesticUseInMines = null;
                    break;
            }

            switch (ddlWatersupply.SelectedValue)
            {
                case "Yes":
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumpedWaterSupply = NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.WaterSupplyStatus.Yes;
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterWSDetail = txtWaterSuplyDesc.Text;
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterWS = Convert.ToDecimal(txtWaterSuply.Text);
                    break;
                case "No":
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumpedWaterSupply = NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.WaterSupplyStatus.No;
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterWSDetail = null;
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterWS = null;
                    break;
            }
            switch (ddlAgriculture.SelectedValue)
            {
                case "Yes":
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumpedWaterinAgiculture = NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.AgricultureStatus.Yes;
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterAgri = Convert.ToDecimal(txtAgriculture.Text);
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterAgriDetail = txtAgricultureDesc.Text;
                    break;
                case "No":
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumpedWaterinAgiculture = NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.AgricultureStatus.No;
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterAgri = null;
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterAgriDetail = null;
                    break;
            }

            switch (ddlGBDevelopment.SelectedValue)
            {
                case "Yes":
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumpedWaterinGreenBeltDevelopment = NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.GBDStatus.Yes;
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterGD = Convert.ToDecimal(txtGBDevelopment.Text);
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterGDDetail = txtGBDevelopmentDesc.Text;
                    break;
                case "No":
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumpedWaterinGreenBeltDevelopment = NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.GBDStatus.No;
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterGD = null;
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterGDDetail = null;
                    break;
            }

            switch (ddlSuppresionOFDust.SelectedValue)
            {
                case "Yes":
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumpedWaterinSuppOfDust = NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.SuppOfDustStatus.Yes;
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterSuppDust =Convert.ToDecimal(txtSuppervisionOfDust.Text);
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterSuppDustDetail = txtSuppervisionOfDustDesc.Text;
                    break;
                case "No":
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumpedWaterinSuppOfDust = NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.SuppOfDustStatus.No;
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterSuppDustDetail = null;
                    break;
            }

            switch (ddlRecharge.SelectedValue)
            {
                case "Yes":
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumpedWaterinRecharge = NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.RechargeStatus.Yes;
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterRech = Convert.ToDecimal(txtRecharge.Text);
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterRechDetail = txtRechargeDesc.Text;
                    break;
                case "No":
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumpedWaterinRecharge = NOCAP.BLL.Mining.New.Common.MiningNewProposedUtilizationofPumpedWater.RechargeStatus.No;
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterRech = null;
                    obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiPumWaterRechDetail = null;
                    break;
            }

            obj_MiningNewApplication.MiningNewProposedUtilizationofPumpedWater.ProUtiAnyOtherItem = txtPumpedWaterAnyOtherItem.Text;
            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_MiningNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_MiningNewApplication.Update() == 1)
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
                UpdateMinUtilizationPumpedWaterDetail(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
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
            Server.Transfer("~/ExternalUser/MiningNew/De-WateringProposedStructure.aspx");
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
    protected void ddlDomesticUseInMines_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            if (ddlDomesticUseInMines.SelectedValue == "Yes")
            {
                txtDomesticUseInMines.Enabled = true;
                txtDomesticUseInMinesDesc.Enabled = true;
                revtxtDomesticUseInMines.Enabled = true;
            }
            else
            {
                txtDomesticUseInMines.Enabled = false;
                txtDomesticUseInMines.Text = "";
                txtDomesticUseInMinesDesc.Enabled = false;
                txtDomesticUseInMinesDesc.Text = "";
                revtxtDomesticUseInMines.Enabled = false;
            }
        }
    }
}