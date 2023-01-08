using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_InfrastructureNew_De_WateringProposedStructure : System.Web.UI.Page
{
    string strPageName = "INFDeWateringProposedStructure";
    string strActionName = "";
    string strStatus = "";
    //string category = "";
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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblInfrastructureApplicationCodeFrom");
                        if (SourceLabel != null) { lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit") { BindIGVInfNewProposedDGWASDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text)); }

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

    private void ValidationExpInit()
    {
        revtxtNoOfProposedStructure.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtNoOfProposedStructure.ErrorMessage = ValidationUtility.txtValForNumericMsg;

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

    private void BindIGVInfNewProposedDGWASDetails(long lngA_ApplicationCode)
    {
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure obj_infNewProposedDGWASBlank = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure();
        List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure> lst_infNewProposedDGWASBlank = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure>();
        List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure> lst_infNewProposedDGWAS = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure>();
        try
        {
            if (obj_infrastructureNewApplication.InfrastructureDewatering.DewateringRequired == NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADDewatering.deWateringRequired.No) { DisableEnableForm(false); }
            else { DisableEnableForm(true); }
            
            txtNoOfProposedStructure.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.InfrastructureDewatering.DewateringNoOfProposedStructure));
            if (txtNoOfProposedStructure.Text == "") { txtNoOfProposedStructure.Text = "0"; }
            //category = Convert.ToString(new NOCAP.BLL.Master.ApplicationTypeCategory((int)obj_infrastructureNewApplication.ApplicationTypeCode, (int)obj_infrastructureNewApplication.ApplicationTypeCategoryCode).ApplicationTypeCategoryDesc);
            //if (category == "Residential apartment" || category == "Group housing" || category == "Government water Supply agencies")
            //{
            //    trDepthWaterBelowGroundLevel.Visible = false;
            //    trDischarge.Visible = false;
            //    trOperationalHoursDay.Visible = false;
            //    trOperationalDaysYear.Visible = false;
            //    trModeOfLift.Visible = false;
            //    trHorsePowerPump.Visible = false;
            //    trWhetherFittedWithWaterMeter.Visible = false;
            //    trWhetherPermissionRegisteredWithCGWA.Visible = false;
            //    trWhetherPermissionRegisteredWithCGWADet.Visible = false;
            //}
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure[] arr_infNewProposedDGWAS;
            arr_infNewProposedDGWAS = obj_infrastructureNewApplication.GetInfrastructureNewSADProposedDeWateringAbstractionStructureList();
            foreach (NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure test in arr_infNewProposedDGWAS) { lst_infNewProposedDGWAS.Add(test); }
            //if (arr_infNewProposedDGWAS.Count() > 0)
            //{
            gvdeWateringPrposed.DataSource = lst_infNewProposedDGWAS;
            gvdeWateringPrposed.DataBind();
            //}
            //else
            //{
            //    lst_infNewProposedDGWASBlank.Add(obj_infNewProposedDGWASBlank);
            //    gvdeWateringPrposed.DataSource = lst_infNewProposedDGWASBlank;
            //    gvdeWateringPrposed.DataBind();
            //    int int_NoOfCol = 0;
            //    int_NoOfCol = gvdeWateringPrposed.Rows[0].Cells.Count;
            //    gvdeWateringPrposed.Rows[0].Cells.Clear();
            //    gvdeWateringPrposed.Rows[0].Cells.Add(new TableCell());
            //    gvdeWateringPrposed.Rows[0].Cells[0].ColumnSpan = int_NoOfCol;
            //    gvdeWateringPrposed.Rows[0].Cells[0].Text = "No Records exsist in GAS Proposed";
            //}
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally
        {
        }
    }

    private void DisableEnableForm(bool status)
    {
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
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure obj_infNewProposedDGWASBlankForGridToObj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure();
                List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure> lst_infNewProposedDGWASNewList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure>();
                //if (gvdeWateringPrposed.Rows.Count == Convert.ToInt32(txtNoOfProposedStructure.Text))
                //{
                //    lblMessage.Text = "You can Enter Only " + txtNoOfProposedStructure.Text + " Structures.";
                //    lblMessage.ForeColor = System.Drawing.Color.Red;
                //    return;
                //}
                //else { lblMessage.Text = ""; }
                foreach (GridViewRow gvRow in gvdeWateringPrposed.Rows)
                {
                    obj_infNewProposedDGWASBlankForGridToObj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure();
                    obj_infNewProposedDGWASBlankForGridToObj.ApplicationCode = Convert.ToInt64(gvdeWateringPrposed.DataKeys[gvRow.RowIndex].Value.ToString());
                    if (Convert.ToInt64(gvdeWateringPrposed.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                    {
                        Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                        if (lbl_SerialNumber.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.SerialNumber = 0; }
                        else { obj_infNewProposedDGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim()); }

                        Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");
                        if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0; }
                        else { obj_infNewProposedDGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim()); }

                        Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                        if (lbl_YearOfConstruction.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.YearOfConstruction = null; }
                        else { obj_infNewProposedDGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim()); }

                        Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");
                        if (lbl_DepthExist.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.DepthExist = null; }
                        else { obj_infNewProposedDGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text); }

                        Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");
                        if (lbl_Diameter.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.Diameter = null; }
                        else { obj_infNewProposedDGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text); }

                        Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");
                        if (lbl_DepthBelowWaterLevel.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.DepthBelowWaterLevel = null; }
                        else { obj_infNewProposedDGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text); }

                        Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");
                        if (lbl_Discharge.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.Discharge = null; }
                        else { obj_infNewProposedDGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text); }

                        Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");
                        if (lbl_OperationalHousrDay.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.OperationalHousrDay = null; }
                        else { obj_infNewProposedDGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text); }

                        Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");
                        if (lbl_OperationalDaysYear.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.OperationalDaysYear = null; }
                        else { obj_infNewProposedDGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text); }

                        Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");
                        if (lbl_LiftingDeviceCode.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.LiftingDeviceCode = null; }
                        else { obj_infNewProposedDGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text); }

                        Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");
                        if (lbl_PowerOfPump.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.PowerOfPump = null; }
                        else { obj_infNewProposedDGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text); }

                        Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                        switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                        {
                            case "":
                                obj_infNewProposedDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;
                                break;
                            case "Yes":
                                obj_infNewProposedDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;
                                break;
                            case "No":
                                obj_infNewProposedDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;
                                break;
                        }

                        Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                        switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                        {
                            case "":
                                obj_infNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;
                                break;
                            case "Yes":
                                obj_infNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;
                                break;
                            case "No":
                                obj_infNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;
                                break;
                        }

                        Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");
                        if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null; }
                        else { obj_infNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text; }
                        lst_infNewProposedDGWASNewList.Add(obj_infNewProposedDGWASBlankForGridToObj);
                    }
                }
                //---------------
                obj_infNewProposedDGWASBlankForGridToObj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure();
                obj_infNewProposedDGWASBlankForGridToObj.ApplicationCode = Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text);
                if (NOCAPExternalUtility.IsNumeric(ddlTypeOfStructure.SelectedValue)) { obj_infNewProposedDGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(ddlTypeOfStructure.SelectedValue); }
                else { return; }
                if (txtYearOfConstruction.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.YearOfConstruction = null; }
                else { obj_infNewProposedDGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt32(txtYearOfConstruction.Text.Trim()); }

                if (txtDepthMeter.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.DepthExist = null; }
                else
                {
                    obj_infNewProposedDGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(txtDepthMeter.Text.Trim());
                }
                if (txtDiameterMM.Text.Trim() == "")
                {
                    obj_infNewProposedDGWASBlankForGridToObj.Diameter = null;
                }
                else
                {
                    obj_infNewProposedDGWASBlankForGridToObj.Diameter = Convert.ToInt32(txtDiameterMM.Text.Trim());
                }

                if (txtDepthWaterBelowGroundLevel.Text.Trim() == "")
                {
                    obj_infNewProposedDGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_infNewProposedDGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(txtDepthWaterBelowGroundLevel.Text.Trim());
                }
                if (txtDischarge.Text.Trim() == "")
                {
                    obj_infNewProposedDGWASBlankForGridToObj.Discharge = null;
                }
                else
                {
                    obj_infNewProposedDGWASBlankForGridToObj.Discharge = Convert.ToDecimal(txtDischarge.Text.Trim());
                }
                if (txtOperationalHoursDay.Text.Trim() == "")
                {
                    obj_infNewProposedDGWASBlankForGridToObj.OperationalHousrDay = null;
                }
                else
                {
                    obj_infNewProposedDGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(txtOperationalHoursDay.Text.Trim());
                }

                if (txtOperationalDaysYear.Text.Trim() == "")
                {
                    obj_infNewProposedDGWASBlankForGridToObj.OperationalDaysYear = null;
                }
                else
                {
                    obj_infNewProposedDGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(txtOperationalDaysYear.Text.Trim());
                }

                if (ddlModeOfLift.SelectedValue == "")
                {
                    obj_infNewProposedDGWASBlankForGridToObj.LiftingDeviceCode = null;
                }
                else
                {
                    if (NOCAPExternalUtility.IsNumeric(ddlModeOfLift.SelectedValue)) { obj_infNewProposedDGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(ddlModeOfLift.SelectedValue); }
                }
                if (txtHorsePowerPump.Text.Trim() == "")
                {
                    obj_infNewProposedDGWASBlankForGridToObj.PowerOfPump = null;
                }
                else
                {
                    obj_infNewProposedDGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(txtHorsePowerPump.Text.Trim());
                }

                switch (ddlWhetherFittedWithWaterMeter.SelectedValue)
                {
                    case "":
                        obj_infNewProposedDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_infNewProposedDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_infNewProposedDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }

                switch (ddlWhetherPermissionRegisteredWithCGWA.SelectedValue)
                {
                    case "":
                        obj_infNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_infNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_infNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (txtWhetherPermissionRegisteredWithCGWADet.Text.Trim() == "")
                {
                    obj_infNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_infNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = Convert.ToString(txtWhetherPermissionRegisteredWithCGWADet.Text.Trim());
                }

                lst_infNewProposedDGWASNewList.Add(obj_infNewProposedDGWASBlankForGridToObj);
                gvdeWateringPrposed.DataSource = lst_infNewProposedDGWASNewList;
                gvdeWateringPrposed.DataBind();
                ClearGASProposedScreen();
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

    private void ClearGASProposedScreen()
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

    protected void gvdeWateringPrposed_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure obj_infNewProposedDGWASBlankForGridToObj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure();
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure> lst_infNewProposedDGWASNewList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure>();
                    foreach (GridViewRow gvRow in gvdeWateringPrposed.Rows)
                    {
                        obj_infNewProposedDGWASBlankForGridToObj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure();
                        obj_infNewProposedDGWASBlankForGridToObj.ApplicationCode = Convert.ToInt64(gvdeWateringPrposed.DataKeys[gvRow.RowIndex].Value.ToString());
                        if (Convert.ToInt64(gvdeWateringPrposed.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                        {
                            Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                            if (lbl_SerialNumber.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.SerialNumber = 0; }
                            else { obj_infNewProposedDGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim()); }

                            Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");
                            if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0; }
                            else { obj_infNewProposedDGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim()); }

                            Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                            if (lbl_YearOfConstruction.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.YearOfConstruction = null; }
                            else { obj_infNewProposedDGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim()); }

                            Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");
                            if (lbl_DepthExist.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.DepthExist = null; }
                            else { obj_infNewProposedDGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text); }

                            Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");
                            if (lbl_Diameter.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.Diameter = null; }
                            else { obj_infNewProposedDGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text); }

                            Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");
                            if (lbl_DepthBelowWaterLevel.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.DepthBelowWaterLevel = null; }
                            else { obj_infNewProposedDGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text); }

                            Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");
                            if (lbl_Discharge.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.Discharge = null; }
                            else { obj_infNewProposedDGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text); }

                            Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");
                            if (lbl_OperationalHousrDay.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.OperationalHousrDay = null; }
                            else { obj_infNewProposedDGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text); }

                            Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");
                            if (lbl_OperationalDaysYear.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.OperationalDaysYear = null; }
                            else { obj_infNewProposedDGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text); }

                            Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");
                            if (lbl_LiftingDeviceCode.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.LiftingDeviceCode = null; }
                            else { obj_infNewProposedDGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text); }

                            Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");
                            if (lbl_PowerOfPump.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.PowerOfPump = null; }
                            else { obj_infNewProposedDGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text); }

                            Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                            switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                            {
                                case "":
                                    obj_infNewProposedDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;
                                    break;
                                case "Yes":
                                    obj_infNewProposedDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;
                                    break;
                                case "No":
                                    obj_infNewProposedDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;
                                    break;
                            }
                            Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                            switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                            {
                                case "":
                                    obj_infNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;
                                    break;
                                case "Yes":
                                    obj_infNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;
                                    break;
                                case "No":
                                    obj_infNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;
                                    break;
                            }

                            Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");
                            if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "") { obj_infNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null; }
                            else { obj_infNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text; }
                            lst_infNewProposedDGWASNewList.Add(obj_infNewProposedDGWASBlankForGridToObj);
                        }
                    }
                    lst_infNewProposedDGWASNewList.RemoveAt(lst_IntIndex);
                    gvdeWateringPrposed.DataSource = lst_infNewProposedDGWASNewList;
                    gvdeWateringPrposed.DataBind();
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
    protected void gvdeWateringPrposed_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure obj_infNewProposedDGWAS = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure();
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
                    obj_infNewProposedDGWAS = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure(Convert.ToInt64(gvdeWateringPrposed.DataKeys[e.Row.RowIndex].Values[0].ToString()), Convert.ToInt32(lbl_SerialNumber.Text));
                    lbl_TypeOfAbstractionStructureName.Text = HttpUtility.HtmlEncode(obj_infNewProposedDGWAS.GetTypeOfAbstractionStructure().TypeOfAbstractionStructureDesc);
                    if (obj_infNewProposedDGWAS.GetLiftingDevice() != null)
                    {
                        lbl_LiftingDeviceName.Text = HttpUtility.HtmlEncode(obj_infNewProposedDGWAS.GetLiftingDevice().LiftingDeviceDesc);
                        lbl_LiftingDeviceCode.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infNewProposedDGWAS.GetLiftingDevice().LiftingDeviceCode));
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
                Server.Transfer("~/ExternalUser/InfrastructureNew/De-WateringExistingStructure.aspx");
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
                if (UpdateInfNewProposedDGWASDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text)) == 1)
                {
                    //Server.Transfer("~/ExternalUser/InfrastructureNew/BreakUpOfWaterRequirment.aspx");
                    Server.Transfer("~/ExternalUser/InfrastructureNew/ExistingGroundwaterAbstractionStructure.aspx");
                }
                else
                {
                }
            }
        }
    }


    private int UpdateInfNewProposedDGWASDetails(long lngA_ApplicationCode)
    {
        try
        {
            strActionName = "UpdateDewateringProposedDetails";
            if (txtNoOfProposedStructure.Text.Trim() == "")
            {
                lblMessage.Text = "Number of Proposed Structures can not be Blank";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            if (!NOCAPExternalUtility.IsNumeric(txtNoOfProposedStructure.Text))
            {
                lblMessage.Text = "Number of Proposed Structures allows Numeric";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            if (Convert.ToInt32(txtNoOfProposedStructure.Text) != gvdeWateringPrposed.Rows.Count)
            {
                lblMessage.Text = "Number of Records of Detailed Structures should be equal to the Number of Proposed Structures";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            else { lblMessage.Text = ""; }
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);

            if (obj_infrastructureNewApplication.InfrastructureDewatering.DewateringRequired == NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADDewatering.deWateringRequired.Yes)
            {
                if (obj_infrastructureNewApplication.InfrastructureDewatering.DewateringNoOfExistingStructure == null || obj_infrastructureNewApplication.InfrastructureDewatering.DewateringNoOfExistingStructure == 0)
                {
                    if (txtNoOfProposedStructure.Text == "" || txtNoOfProposedStructure.Text == "0")
                    {
                        lblMessage.Text = "Either 3(i.a)(f) Dewater-Existing or 3(1.a)(g) Dewater-Proposed is Required.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return 0;
                    }
                }
                else { lblMessage.Text = ""; }
            }
            List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure> lst_infNewProposedDGWASList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure>();
            foreach (GridViewRow gvRow in gvdeWateringPrposed.Rows)
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
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure obj_infNewProposedDGWAS = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure();
                obj_infNewProposedDGWAS.ApplicationCode = Convert.ToInt32(gvdeWateringPrposed.DataKeys[gvRow.RowIndex].Value.ToString());

                if (lbl_TypeOfAbstractionStructureCode.Text == "") { obj_infNewProposedDGWAS.TypeOfAbstractionStructureCode = 0; }
                else { obj_infNewProposedDGWAS.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text); }

                if (lbl_YearOfConstruction.Text.Trim() == "") { obj_infNewProposedDGWAS.YearOfConstruction = null; }
                else { obj_infNewProposedDGWAS.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim()); }

                if (lbl_DepthExist.Text.Trim() == "") { obj_infNewProposedDGWAS.DepthExist = null; }
                else { obj_infNewProposedDGWAS.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text); }

                if (lbl_Diameter.Text.Trim() == "") { obj_infNewProposedDGWAS.Diameter = null; }
                else { obj_infNewProposedDGWAS.Diameter = Convert.ToInt32(lbl_Diameter.Text); }

                if (lbl_DepthBelowWaterLevel.Text.Trim() == "") { obj_infNewProposedDGWAS.DepthBelowWaterLevel = null; }
                else { obj_infNewProposedDGWAS.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text); }

                if (lbl_Discharge.Text.Trim() == "") { obj_infNewProposedDGWAS.Discharge = null; }
                else { obj_infNewProposedDGWAS.Discharge = Convert.ToDecimal(lbl_Discharge.Text); }

                if (lbl_OperationalHousrDay.Text.Trim() == "") { obj_infNewProposedDGWAS.OperationalHousrDay = null; }
                else { obj_infNewProposedDGWAS.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text); }

                if (lbl_OperationalDaysYear.Text.Trim() == "") { obj_infNewProposedDGWAS.OperationalDaysYear = null; }
                else { obj_infNewProposedDGWAS.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text); }

                if (lbl_LiftingDeviceCode.Text.Trim() == "") { obj_infNewProposedDGWAS.LiftingDeviceCode = null; }
                else { obj_infNewProposedDGWAS.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text); }

                if (lbl_PowerOfPump.Text.Trim() == "") { obj_infNewProposedDGWAS.PowerOfPump = null; }
                else { obj_infNewProposedDGWAS.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text); }

                switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                {
                    case "":
                        obj_infNewProposedDGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;
                        break;
                    case "Yes":
                        obj_infNewProposedDGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;
                        break;
                    case "No":
                        obj_infNewProposedDGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;
                        break;
                }

                switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                {
                    case "":
                        obj_infNewProposedDGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;
                        break;
                    case "Yes":
                        obj_infNewProposedDGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;
                        break;
                    case "No":
                        obj_infNewProposedDGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;
                        break;
                }
                if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "") { obj_infNewProposedDGWAS.WhetherPermissionRegisteredWithCGWADetails = null; }
                else { obj_infNewProposedDGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text; }
                obj_infNewProposedDGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                lst_infNewProposedDGWASList.Add(obj_infNewProposedDGWAS);
            }

            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure[] arr_tempInfDeWateringProposedListBLL = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedDeWateringAbstractionStructure[lst_infNewProposedDGWASList.Count];
            lst_infNewProposedDGWASList.CopyTo(arr_tempInfDeWateringProposedListBLL);
            obj_infrastructureNewApplication.InfrastructureNewSADProposedDeWateringAbstractionStructureList = arr_tempInfDeWateringProposedListBLL;

            if (txtNoOfProposedStructure.Text.Trim() == "") { obj_infrastructureNewApplication.InfrastructureDewatering.DewateringNoOfProposedStructure = null; }
            else { obj_infrastructureNewApplication.InfrastructureDewatering.DewateringNoOfProposedStructure = Convert.ToInt32(txtNoOfProposedStructure.Text.Trim()); }

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_infrastructureNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_infrastructureNewApplication.Update() == 1)
            {
                strStatus = "Saved Successfully";
                lblMessage.Text = "Successfully Saved";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                strStatus = "Save Unsuccessfull";
                lblMessage.Text = obj_infrastructureNewApplication.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                BindIGVInfNewProposedDGWASDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
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
                UpdateInfNewProposedDGWASDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
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
}