using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_MiningRenew_De_WateringAdditionalStructure : System.Web.UI.Page
{
    string strPageName = "MINDe-WateringAdditionalStructure";
    string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        if (!IsPostBack)
        {
            ValidationExpInit();
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            RngValYearOfConstruction.MinimumValue = System.DateTime.Now.Year.ToString();
            if (NOCAPExternalUtility.FillDropDownTypeOfStructure(ref ddlTypeOfStructure) != 1)
            {
                lblMessage.Text = "Problem in Type of Structure population";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            if (NOCAPExternalUtility.FillDropDownLiftingDevice(ref ddlModeOfLift) != 1)
            {
                lblMessage.Text = "Problem in Type of Lifting Device";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
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
                if (lblModeFrom.Text.Trim() == "Edit") { BindIGVMinRenewAdditionalDGWASDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)); }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    private void ValidationExpInit()
    {
        revtxtNoOfAdditionalStructure.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtNoOfAdditionalStructure.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtYearOfConstruction.ValidationExpression = ValidationUtility.txtValForNumericWithOutFirstCharacterZero;
        revtxtYearOfConstruction.ErrorMessage = ValidationUtility.txtValForNumericWithOutFirstCharacterZeroMsg;

        revtxtDepthMeter.ValidationExpression = ValidationUtility.txtValForDecimalValue("4", "2");
        revtxtDepthMeter.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("4", "2");

        revtxtDiameterMM.ValidationExpression = ValidationUtility.txtValForNumericWithOutFirstCharacterZero;
        revtxtDiameterMM.ErrorMessage = ValidationUtility.txtValForNumericWithOutFirstCharacterZeroMsg;

        revtxtDepthWaterBelowGroundLevel.ValidationExpression = ValidationUtility.txtValForDecimalValue("4", "2");
        revtxtDepthWaterBelowGroundLevel.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("4", "2");

        revtxtDischarge.ValidationExpression = ValidationUtility.txtValForDecimalValue("4", "2");
        revtxtDischarge.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("4", "2");

        revtxtOperationalHoursDay.ValidationExpression = ValidationUtility.txtValForNumericWithOutFirstCharacterZero;
        revtxtOperationalHoursDay.ErrorMessage = ValidationUtility.txtValForNumericWithOutFirstCharacterZeroMsg;

        revtxtOperationalDaysYear.ValidationExpression = ValidationUtility.txtValForNumericWithOutFirstCharacterZero;
        revtxtOperationalDaysYear.ErrorMessage = ValidationUtility.txtValForNumericWithOutFirstCharacterZeroMsg;

        revtxtHorsePowerPump.ValidationExpression = ValidationUtility.txtValForDecimalValue("3", "2");
        revtxtHorsePowerPump.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("3", "2");

        revtxtWhetherPermissionRegisteredWithCGWADet.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtWhetherPermissionRegisteredWithCGWADet.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtWhetherPermissionRegisteredWithCGWADet.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("100");
        revLengthtxtWhetherPermissionRegisteredWithCGWADet.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("100");
    }

    private void DisableEnableForm(bool status)
    {
        txtNoOfAdditionalStructure.Enabled = status;
        ddlTypeOfStructure.Enabled = status;
        txtYearOfConstruction.Enabled = status;
        txtDepthMeter.Enabled = status;
        txtDiameterMM.Enabled = status;
        txtDepthWaterBelowGroundLevel.Enabled = status;
        txtDischarge.Enabled = status;
        txtOperationalHoursDay.Enabled = status;
        txtOperationalDaysYear.Enabled = status;
        ddlModeOfLift.Enabled = status;
        txtHorsePowerPump.Enabled = status;
        ddlWhetherFittedWithWaterMeter.Enabled = status;
        ddlWhetherPermissionRegisteredWithCGWA.Enabled = status;
        txtWhetherPermissionRegisteredWithCGWADet.Enabled = status;
        btnAdd.Enabled = status;
    }

    private void BindIGVMinRenewAdditionalDGWASDetails(long lngA_ApplicationCode)
    {
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
        NOCAP.BLL.Mining.New.Application.MiningNewApplication objMiningNewApplication = obj_MiningRenewApplication.GetFirstMiningApplication();

        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure obj_minRenewAdditionalDGWASBlank = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure();
        List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure> lst_minRenewAdditionalDGWASBlank = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure>();
        List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure> lst_minRenewAdditionalDGWAS = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure>();
        try
        {
            if (objMiningNewApplication != null)
            {
                lblHeadingProjName.Text = HttpUtility.HtmlEncode(objMiningNewApplication.NameOfMining);
            }
            if (obj_MiningRenewApplication.MiningRenewSADDewatering.WhetherGWTableIntersected == NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADDewatering.GWTableIntersected.No)
            { DisableEnableForm(false); }
            else { DisableEnableForm(true); }

            txtNoOfAdditionalStructure.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.DewateringNoOfAdditionalStructure));
            if (txtNoOfAdditionalStructure.Text == "")
            { txtNoOfAdditionalStructure.Text = "0"; }
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure[] arr_minRenewAdditionalDGWAS;
            arr_minRenewAdditionalDGWAS = obj_MiningRenewApplication.GetMiningRenewSADAdditionalDeWateringAbstractionStructureList();
            foreach (NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure test in arr_minRenewAdditionalDGWAS) 
            { 
                lst_minRenewAdditionalDGWAS.Add(test); 
            }
            
            gvdeWateringAdditional.DataSource = lst_minRenewAdditionalDGWAS;
            gvdeWateringAdditional.DataBind();
        
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally
        {
        }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
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
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure obj_minRenewAdditionalDGWASBlankForGridToObj = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure();
                List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure> lst_minRenewAdditionalDGWASNewList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure>();
                foreach (GridViewRow gvRow in gvdeWateringAdditional.Rows)
                {
                    obj_minRenewAdditionalDGWASBlankForGridToObj = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure();
                    obj_minRenewAdditionalDGWASBlankForGridToObj.MiningRenewApplicationCode = Convert.ToInt64(gvdeWateringAdditional.DataKeys[gvRow.RowIndex].Value.ToString());
                    if (Convert.ToInt64(gvdeWateringAdditional.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                    {
                        Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                        if (lbl_SerialNumber.Text.Trim() == "") { obj_minRenewAdditionalDGWASBlankForGridToObj.SerialNumber = 0; }
                        else { obj_minRenewAdditionalDGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim()); }

                        Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");
                        if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "") { obj_minRenewAdditionalDGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0; }
                        else { obj_minRenewAdditionalDGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim()); }

                        Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                        if (lbl_YearOfConstruction.Text.Trim() == "") { obj_minRenewAdditionalDGWASBlankForGridToObj.YearOfConstruction = null; }
                        else { obj_minRenewAdditionalDGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim()); }

                        Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");
                        if (lbl_DepthExist.Text.Trim() == "") { obj_minRenewAdditionalDGWASBlankForGridToObj.DepthExist = null; }
                        else { obj_minRenewAdditionalDGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text); }

                        Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");
                        if (lbl_Diameter.Text.Trim() == "") { obj_minRenewAdditionalDGWASBlankForGridToObj.Diameter = null; }
                        else { obj_minRenewAdditionalDGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text); }

                        Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");
                        if (lbl_DepthBelowWaterLevel.Text.Trim() == "") { obj_minRenewAdditionalDGWASBlankForGridToObj.DepthBelowWaterLevel = null; }
                        else { obj_minRenewAdditionalDGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text); }

                        Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");
                        if (lbl_Discharge.Text.Trim() == "") { obj_minRenewAdditionalDGWASBlankForGridToObj.Discharge = null; }
                        else { obj_minRenewAdditionalDGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text); }

                        Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");
                        if (lbl_OperationalHousrDay.Text.Trim() == "") { obj_minRenewAdditionalDGWASBlankForGridToObj.OperationalHousrDay = null; }
                        else { obj_minRenewAdditionalDGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text); }

                        Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");
                        if (lbl_OperationalDaysYear.Text.Trim() == "") { obj_minRenewAdditionalDGWASBlankForGridToObj.OperationalDaysYear = null; }
                        else { obj_minRenewAdditionalDGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text); }

                        Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");
                        if (lbl_LiftingDeviceCode.Text.Trim() == "") { obj_minRenewAdditionalDGWASBlankForGridToObj.LiftingDeviceCode = null; }
                        else { obj_minRenewAdditionalDGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text); }

                        Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");
                        if (lbl_PowerOfPump.Text.Trim() == "") { obj_minRenewAdditionalDGWASBlankForGridToObj.PowerOfPump = null; }
                        else { obj_minRenewAdditionalDGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text); }

                        Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                        switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                        {
                            case "":
                                obj_minRenewAdditionalDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;
                                break;
                            case "Yes":
                                obj_minRenewAdditionalDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;
                                break;
                            case "No":
                                obj_minRenewAdditionalDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;
                                break;
                        }

                        Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                        switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                        {
                            case "":
                                obj_minRenewAdditionalDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;
                                break;
                            case "Yes":
                                obj_minRenewAdditionalDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;
                                break;
                            case "No":
                                obj_minRenewAdditionalDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;
                                break;
                        }

                        Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");
                        if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "") { obj_minRenewAdditionalDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null; }
                        else { obj_minRenewAdditionalDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text; }
                        lst_minRenewAdditionalDGWASNewList.Add(obj_minRenewAdditionalDGWASBlankForGridToObj);
                    }
                }
                //---------------
                obj_minRenewAdditionalDGWASBlankForGridToObj = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure();
                obj_minRenewAdditionalDGWASBlankForGridToObj.MiningRenewApplicationCode = Convert.ToInt64(lblMiningApplicationCodeFrom.Text);
                if (NOCAPExternalUtility.IsNumeric(ddlTypeOfStructure.SelectedValue)) { obj_minRenewAdditionalDGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(ddlTypeOfStructure.SelectedValue); }
                else { return; }
                if (txtYearOfConstruction.Text.Trim() == "") { obj_minRenewAdditionalDGWASBlankForGridToObj.YearOfConstruction = null; }
                else { obj_minRenewAdditionalDGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt32(txtYearOfConstruction.Text.Trim()); }

                if (txtDepthMeter.Text.Trim() == "") { obj_minRenewAdditionalDGWASBlankForGridToObj.DepthExist = null; }
                else
                {
                    obj_minRenewAdditionalDGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(txtDepthMeter.Text.Trim());
                }
                if (txtDiameterMM.Text.Trim() == "")
                {
                    obj_minRenewAdditionalDGWASBlankForGridToObj.Diameter = null;
                }
                else
                {
                    obj_minRenewAdditionalDGWASBlankForGridToObj.Diameter = Convert.ToInt32(txtDiameterMM.Text.Trim());
                }

                if (txtDepthWaterBelowGroundLevel.Text.Trim() == "")
                {
                    obj_minRenewAdditionalDGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_minRenewAdditionalDGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(txtDepthWaterBelowGroundLevel.Text.Trim());
                }
                if (txtDischarge.Text.Trim() == "")
                {
                    obj_minRenewAdditionalDGWASBlankForGridToObj.Discharge = null;
                }
                else
                {
                    obj_minRenewAdditionalDGWASBlankForGridToObj.Discharge = Convert.ToDecimal(txtDischarge.Text.Trim());
                }
                if (txtOperationalHoursDay.Text.Trim() == "")
                {
                    obj_minRenewAdditionalDGWASBlankForGridToObj.OperationalHousrDay = null;
                }
                else
                {
                    obj_minRenewAdditionalDGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(txtOperationalHoursDay.Text.Trim());
                }

                if (txtOperationalDaysYear.Text.Trim() == "")
                {
                    obj_minRenewAdditionalDGWASBlankForGridToObj.OperationalDaysYear = null;
                }
                else
                {
                    obj_minRenewAdditionalDGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(txtOperationalDaysYear.Text.Trim());
                }

                if (ddlModeOfLift.SelectedValue == "")
                {
                    obj_minRenewAdditionalDGWASBlankForGridToObj.LiftingDeviceCode = null;
                }
                else
                {
                    if (NOCAPExternalUtility.IsNumeric(ddlModeOfLift.SelectedValue)) { obj_minRenewAdditionalDGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(ddlModeOfLift.SelectedValue); }
                }
                if (txtHorsePowerPump.Text.Trim() == "")
                {
                    obj_minRenewAdditionalDGWASBlankForGridToObj.PowerOfPump = null;
                }
                else
                {
                    obj_minRenewAdditionalDGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(txtHorsePowerPump.Text.Trim());
                }

                switch (ddlWhetherFittedWithWaterMeter.SelectedValue)
                {
                    case "":
                        obj_minRenewAdditionalDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_minRenewAdditionalDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_minRenewAdditionalDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }

                switch (ddlWhetherPermissionRegisteredWithCGWA.SelectedValue)
                {
                    case "":
                        obj_minRenewAdditionalDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_minRenewAdditionalDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_minRenewAdditionalDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (txtWhetherPermissionRegisteredWithCGWADet.Text.Trim() == "")
                {
                    obj_minRenewAdditionalDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_minRenewAdditionalDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = Convert.ToString(txtWhetherPermissionRegisteredWithCGWADet.Text.Trim());
                }

                lst_minRenewAdditionalDGWASNewList.Add(obj_minRenewAdditionalDGWASBlankForGridToObj);
                gvdeWateringAdditional.DataSource = lst_minRenewAdditionalDGWASNewList;
                gvdeWateringAdditional.DataBind();
                ClearGASAdditionalScreen();
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

    private void ClearGASAdditionalScreen()
    {
        ddlTypeOfStructure.SelectedValue = "";
        txtYearOfConstruction.Text = "";
        txtDepthMeter.Text = "";
        txtDiameterMM.Text = "";
        txtDepthWaterBelowGroundLevel.Text = "";
        txtDischarge.Text = "";
        txtOperationalHoursDay.Text = "";
        txtOperationalDaysYear.Text = "";
        ddlModeOfLift.SelectedValue = "";
        txtHorsePowerPump.Text = "";
        txtWhetherPermissionRegisteredWithCGWADet.Text = "";
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
            if (Page.IsValid)
            {

                Server.Transfer("~/ExternalUser/MiningRenew/De-WateringExistingStructure.aspx");

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
                UpdateMinRenewAdditionalDGWASDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
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
                if (UpdateMinRenewAdditionalDGWASDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)) == 1)
                {
                    Server.Transfer("~/ExternalUser/MiningRenew/DetailsUtiizationOfPumpedWater.aspx");
                }
                else
                {
                }
            }
        }
    }

    private int UpdateMinRenewAdditionalDGWASDetails(long lngA_ApplicationCode)
    {
        try
        {
            if (txtNoOfAdditionalStructure.Text.Trim() == "")
            {
                lblMessage.Text = "Number of Additional Structures can not be Blank";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            if (!NOCAPExternalUtility.IsNumeric(txtNoOfAdditionalStructure.Text))
            {
                lblMessage.Text = "Number of Additional Structures allows Numeric";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            if (Convert.ToInt32(txtNoOfAdditionalStructure.Text) != gvdeWateringAdditional.Rows.Count)
            {
                lblMessage.Text = "Number of Records of Detailed Structures should be equal to the Number of Additional Structures";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure> lst_minRenewAdditionalDGWASList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure>();
            foreach (GridViewRow gvRow in gvdeWateringAdditional.Rows)
            {
                Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");
                Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");
                Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");
                Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");
                Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");
                Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");
                Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");
                Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");
                Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");
                Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure obj_minRenewAdditionalDGWAS = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure();
                obj_minRenewAdditionalDGWAS.MiningRenewApplicationCode = Convert.ToInt32(gvdeWateringAdditional.DataKeys[gvRow.RowIndex].Value.ToString());

                if (lbl_TypeOfAbstractionStructureCode.Text == "") { obj_minRenewAdditionalDGWAS.TypeOfAbstractionStructureCode = 0; }
                else { obj_minRenewAdditionalDGWAS.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text); }

                if (lbl_YearOfConstruction.Text.Trim() == "") { obj_minRenewAdditionalDGWAS.YearOfConstruction = null; }
                else { obj_minRenewAdditionalDGWAS.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim()); }

                if (lbl_DepthExist.Text.Trim() == "") { obj_minRenewAdditionalDGWAS.DepthExist = null; }
                else { obj_minRenewAdditionalDGWAS.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text); }

                if (lbl_Diameter.Text.Trim() == "") { obj_minRenewAdditionalDGWAS.Diameter = null; }
                else { obj_minRenewAdditionalDGWAS.Diameter = Convert.ToInt32(lbl_Diameter.Text); }

                if (lbl_DepthBelowWaterLevel.Text.Trim() == "") { obj_minRenewAdditionalDGWAS.DepthBelowWaterLevel = null; }
                else { obj_minRenewAdditionalDGWAS.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text); }

                if (lbl_Discharge.Text.Trim() == "") { obj_minRenewAdditionalDGWAS.Discharge = null; }
                else { obj_minRenewAdditionalDGWAS.Discharge = Convert.ToDecimal(lbl_Discharge.Text); }

                if (lbl_OperationalHousrDay.Text.Trim() == "") { obj_minRenewAdditionalDGWAS.OperationalHousrDay = null; }
                else { obj_minRenewAdditionalDGWAS.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text); }

                if (lbl_OperationalDaysYear.Text.Trim() == "") { obj_minRenewAdditionalDGWAS.OperationalDaysYear = null; }
                else { obj_minRenewAdditionalDGWAS.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text); }

                if (lbl_LiftingDeviceCode.Text.Trim() == "") { obj_minRenewAdditionalDGWAS.LiftingDeviceCode = null; }
                else { obj_minRenewAdditionalDGWAS.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text); }

                if (lbl_PowerOfPump.Text.Trim() == "") { obj_minRenewAdditionalDGWAS.PowerOfPump = null; }
                else { obj_minRenewAdditionalDGWAS.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text); }

                switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                {
                    case "":
                        obj_minRenewAdditionalDGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;
                        break;
                    case "Yes":
                        obj_minRenewAdditionalDGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;
                        break;
                    case "No":
                        obj_minRenewAdditionalDGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;
                        break;
                }

                switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                {
                    case "":
                        obj_minRenewAdditionalDGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;
                        break;
                    case "Yes":
                        obj_minRenewAdditionalDGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;
                        break;
                    case "No":
                        obj_minRenewAdditionalDGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;
                        break;
                }
                if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "") { obj_minRenewAdditionalDGWAS.WhetherPermissionRegisteredWithCGWADetails = null; }
                else { obj_minRenewAdditionalDGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text; }
                obj_minRenewAdditionalDGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                lst_minRenewAdditionalDGWASList.Add(obj_minRenewAdditionalDGWAS);
            }

            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure[] arr_tempMinDeWateringAdditionalListBLL = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure[lst_minRenewAdditionalDGWASList.Count];
            lst_minRenewAdditionalDGWASList.CopyTo(arr_tempMinDeWateringAdditionalListBLL);
            obj_MiningRenewApplication.MiningRenewAdditionalDeWateringAbstractionStructureList = arr_tempMinDeWateringAdditionalListBLL;

            if (txtNoOfAdditionalStructure.Text.Trim() == "") { obj_MiningRenewApplication.DewateringNoOfAdditionalStructure = 0; }
            else { obj_MiningRenewApplication.DewateringNoOfAdditionalStructure = Convert.ToInt32(txtNoOfAdditionalStructure.Text.Trim()); }

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
                BindIGVMinRenewAdditionalDGWASDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
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
    


    protected void ddlWhetherPermissionRegisteredWithCGWA_SelectedIndexChanged(object sender, EventArgs e)
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
                if (ddlWhetherPermissionRegisteredWithCGWA.SelectedItem.Text == "No")
                {
                    txtWhetherPermissionRegisteredWithCGWADet.Text = "";
                    txtWhetherPermissionRegisteredWithCGWADet.Enabled = false;
                }
                else { txtWhetherPermissionRegisteredWithCGWADet.Enabled = true; }
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
    protected void gvdeWateringAdditional_RowCommand(object sender, GridViewCommandEventArgs e)
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
                if (e.CommandName == "DeleteName")
                {
                    int int_Code = Convert.ToInt32(e.CommandArgument);
                    int lst_IntIndex = int_Code - 1;
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure obj_MinRenewAdditionalDGWASBlankForGridToObj = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure();
                    List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure> lst_MinRenewAdditionalDGWASNewList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure>();
                    foreach (GridViewRow gvRow in gvdeWateringAdditional.Rows)
                    {
                        obj_MinRenewAdditionalDGWASBlankForGridToObj = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure();
                        obj_MinRenewAdditionalDGWASBlankForGridToObj.MiningRenewApplicationCode = Convert.ToInt64(gvdeWateringAdditional.DataKeys[gvRow.RowIndex].Value.ToString());
                        if (Convert.ToInt64(gvdeWateringAdditional.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                        {
                            Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                            if (lbl_SerialNumber.Text.Trim() == "") { obj_MinRenewAdditionalDGWASBlankForGridToObj.SerialNumber = 0; }
                            else { obj_MinRenewAdditionalDGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim()); }

                            Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");
                            if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "") { obj_MinRenewAdditionalDGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0; }
                            else { obj_MinRenewAdditionalDGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim()); }

                            Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                            if (lbl_YearOfConstruction.Text.Trim() == "") { obj_MinRenewAdditionalDGWASBlankForGridToObj.YearOfConstruction = null; }
                            else { obj_MinRenewAdditionalDGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim()); }

                            Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");
                            if (lbl_DepthExist.Text.Trim() == "") { obj_MinRenewAdditionalDGWASBlankForGridToObj.DepthExist = null; }
                            else { obj_MinRenewAdditionalDGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text); }

                            Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");
                            if (lbl_Diameter.Text.Trim() == "") { obj_MinRenewAdditionalDGWASBlankForGridToObj.Diameter = null; }
                            else { obj_MinRenewAdditionalDGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text); }

                            Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");
                            if (lbl_DepthBelowWaterLevel.Text.Trim() == "") { obj_MinRenewAdditionalDGWASBlankForGridToObj.DepthBelowWaterLevel = null; }
                            else { obj_MinRenewAdditionalDGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text); }

                            Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");
                            if (lbl_Discharge.Text.Trim() == "") { obj_MinRenewAdditionalDGWASBlankForGridToObj.Discharge = null; }
                            else { obj_MinRenewAdditionalDGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text); }

                            Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");
                            if (lbl_OperationalHousrDay.Text.Trim() == "") { obj_MinRenewAdditionalDGWASBlankForGridToObj.OperationalHousrDay = null; }
                            else { obj_MinRenewAdditionalDGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text); }

                            Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");
                            if (lbl_OperationalDaysYear.Text.Trim() == "") { obj_MinRenewAdditionalDGWASBlankForGridToObj.OperationalDaysYear = null; }
                            else { obj_MinRenewAdditionalDGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text); }

                            Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");
                            if (lbl_LiftingDeviceCode.Text.Trim() == "") { obj_MinRenewAdditionalDGWASBlankForGridToObj.LiftingDeviceCode = null; }
                            else { obj_MinRenewAdditionalDGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text); }

                            Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");
                            if (lbl_PowerOfPump.Text.Trim() == "") { obj_MinRenewAdditionalDGWASBlankForGridToObj.PowerOfPump = null; }
                            else { obj_MinRenewAdditionalDGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text); }

                            Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                            switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                            {
                                case "":
                                    obj_MinRenewAdditionalDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;
                                    break;
                                case "Yes":
                                    obj_MinRenewAdditionalDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;
                                    break;
                                case "No":
                                    obj_MinRenewAdditionalDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;
                                    break;
                            }
                            Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                            switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                            {
                                case "":
                                    obj_MinRenewAdditionalDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;
                                    break;
                                case "Yes":
                                    obj_MinRenewAdditionalDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;
                                    break;
                                case "No":
                                    obj_MinRenewAdditionalDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;
                                    break;
                            }

                            Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");
                            if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "") { obj_MinRenewAdditionalDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null; }
                            else { obj_MinRenewAdditionalDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text; }
                            lst_MinRenewAdditionalDGWASNewList.Add(obj_MinRenewAdditionalDGWASBlankForGridToObj);
                        }
                    }
                    lst_MinRenewAdditionalDGWASNewList.RemoveAt(lst_IntIndex);
                    gvdeWateringAdditional.DataSource = lst_MinRenewAdditionalDGWASNewList;
                    gvdeWateringAdditional.DataBind();
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


    protected void gvdeWateringAdditional_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure obj_MinRenewAdditionalDGWAS = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure();
        Label lbl_TypeOfAbstractionStructureCode = null;
        Label lbl_TypeOfAbstractionStructureName = null;
        Label lbl_LiftingDeviceCode = null;
        Label lbl_LiftingDeviceName = null;
        Label lbl_SerialNumber = null;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                lbl_SerialNumber = (Label)e.Row.FindControl("SerialNumber");
                lbl_TypeOfAbstractionStructureCode = (Label)e.Row.FindControl("TypeOfAbstractionStructureCode");
                lbl_TypeOfAbstractionStructureName = (Label)e.Row.FindControl("TypeOfAbstractionStructureName");
                lbl_LiftingDeviceCode = (Label)e.Row.FindControl("LiftingDeviceCode");
                lbl_LiftingDeviceName = (Label)e.Row.FindControl("LiftingDeviceName");
                if (lbl_SerialNumber.Text == "0")
                {
                    lbl_TypeOfAbstractionStructureCode = (Label)e.Row.FindControl("TypeOfAbstractionStructureCode");
                    NOCAP.BLL.Master.TypeOfAbstractionStructure obj_typeOfAbstractionStructureCode = new NOCAP.BLL.Master.TypeOfAbstractionStructure(Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text));
                    lbl_TypeOfAbstractionStructureName.Text = HttpUtility.HtmlEncode(obj_typeOfAbstractionStructureCode.TypeOfAbstractionStructureDesc);
                    if (lbl_LiftingDeviceCode.Text == "") { lbl_LiftingDeviceName.Text = ""; }
                    else
                    {
                        lbl_LiftingDeviceName = (Label)e.Row.FindControl("LiftingDeviceName");
                        NOCAP.BLL.Master.LiftingDevice obj_liftingDevice = new NOCAP.BLL.Master.LiftingDevice(Convert.ToInt32(lbl_LiftingDeviceCode.Text));
                        lbl_LiftingDeviceName.Text = HttpUtility.HtmlEncode(obj_liftingDevice.LiftingDeviceDesc);
                    }
                }
                else
                {
                    obj_MinRenewAdditionalDGWAS = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalDeWateringAbstractionStructure(Convert.ToInt64(gvdeWateringAdditional.DataKeys[e.Row.RowIndex].Values[0].ToString()), Convert.ToInt32(lbl_SerialNumber.Text));
                    lbl_TypeOfAbstractionStructureName.Text = HttpUtility.HtmlEncode(obj_MinRenewAdditionalDGWAS.GetTypeOfAbstractionStructure().TypeOfAbstractionStructureDesc);
                    if (obj_MinRenewAdditionalDGWAS.GetLiftingDevice() != null)
                    {
                        lbl_LiftingDeviceName.Text = HttpUtility.HtmlEncode(obj_MinRenewAdditionalDGWAS.GetLiftingDevice().LiftingDeviceDesc);
                        lbl_LiftingDeviceCode.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MinRenewAdditionalDGWAS.GetLiftingDevice().LiftingDeviceCode));
                    }
                }
            }
            else
            {
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