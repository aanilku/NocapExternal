using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_Mining_De_WateringProposedStructure : System.Web.UI.Page
{
    string strPageName = "MINDe-WateringProposedStructure";
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
                if (lblModeFrom.Text.Trim() == "Edit") { BindIGVMinNewProposedDGWASDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)); }
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

    private void DisableEnableForm(bool status)
    {
        txtNoOfProposedStructure.Enabled = status;
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

    private void BindIGVMinNewProposedDGWASDetails(long lngA_ApplicationCode)
    {
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure obj_minNewProposedDGWASBlank = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure();
        List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure> lst_minNewProposedDGWASBlank = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure>();
        List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure> lst_minNewProposedDGWAS = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure>();
        try
        {
            if (obj_MiningNewApplication.MiningNewSADDewatering.WhetherGWTableIntersected == NOCAP.BLL.Mining.New.SADApplication.MiningNewSADDewatering.GWTableIntersected.No) { DisableEnableForm(false); }
            else { DisableEnableForm(true); }

            txtNoOfProposedStructure.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.DewateringNoOfProposedStructure));
            if (txtNoOfProposedStructure.Text == "") { txtNoOfProposedStructure.Text = "0"; }
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure[] arr_minNewProposedDGWAS;
            arr_minNewProposedDGWAS = obj_MiningNewApplication.GetMiningNewSADProposedDeWateringAbstractionStructureList();
            foreach (NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure test in arr_minNewProposedDGWAS) { lst_minNewProposedDGWAS.Add(test); }
            //if (arr_minNewProposedDGWAS.Count() > 0)
            //{
                gvdeWateringPrposed.DataSource = lst_minNewProposedDGWAS;
                gvdeWateringPrposed.DataBind();
            //}
            //else
            //{
            //    lst_minNewProposedDGWASBlank.Add(obj_minNewProposedDGWASBlank);
            //    gvdeWateringPrposed.DataSource = lst_minNewProposedDGWASBlank;
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
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure obj_minNewProposedDGWASBlankForGridToObj = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure();
                List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure> lst_minNewProposedDGWASNewList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure>();
                foreach (GridViewRow gvRow in gvdeWateringPrposed.Rows)
                {
                    obj_minNewProposedDGWASBlankForGridToObj = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure();
                    obj_minNewProposedDGWASBlankForGridToObj.ApplicationCode = Convert.ToInt64(gvdeWateringPrposed.DataKeys[gvRow.RowIndex].Value.ToString());
                    if (Convert.ToInt64(gvdeWateringPrposed.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                    {
                        Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                        if (lbl_SerialNumber.Text.Trim() == "") { obj_minNewProposedDGWASBlankForGridToObj.SerialNumber = 0; }
                        else { obj_minNewProposedDGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim()); }

                        Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");
                        if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "") { obj_minNewProposedDGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0; }
                        else { obj_minNewProposedDGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim()); }

                        Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                        if (lbl_YearOfConstruction.Text.Trim() == "") { obj_minNewProposedDGWASBlankForGridToObj.YearOfConstruction = null; }
                        else { obj_minNewProposedDGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim()); }

                        Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");
                        if (lbl_DepthExist.Text.Trim() == "") { obj_minNewProposedDGWASBlankForGridToObj.DepthExist = null; }
                        else { obj_minNewProposedDGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text); }

                        Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");
                        if (lbl_Diameter.Text.Trim() == "") { obj_minNewProposedDGWASBlankForGridToObj.Diameter = null; }
                        else { obj_minNewProposedDGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text); }

                        Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");
                        if (lbl_DepthBelowWaterLevel.Text.Trim() == "") { obj_minNewProposedDGWASBlankForGridToObj.DepthBelowWaterLevel = null; }
                        else { obj_minNewProposedDGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text); }

                        Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");
                        if (lbl_Discharge.Text.Trim() == "") { obj_minNewProposedDGWASBlankForGridToObj.Discharge = null; }
                        else { obj_minNewProposedDGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text); }

                        Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");
                        if (lbl_OperationalHousrDay.Text.Trim() == "") { obj_minNewProposedDGWASBlankForGridToObj.OperationalHousrDay = null; }
                        else { obj_minNewProposedDGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text); }

                        Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");
                        if (lbl_OperationalDaysYear.Text.Trim() == "") { obj_minNewProposedDGWASBlankForGridToObj.OperationalDaysYear = null; }
                        else { obj_minNewProposedDGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text); }

                        Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");
                        if (lbl_LiftingDeviceCode.Text.Trim() == "") { obj_minNewProposedDGWASBlankForGridToObj.LiftingDeviceCode = null; }
                        else { obj_minNewProposedDGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text); }

                        Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");
                        if (lbl_PowerOfPump.Text.Trim() == "") { obj_minNewProposedDGWASBlankForGridToObj.PowerOfPump = null; }
                        else { obj_minNewProposedDGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text); }

                        Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                        switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                        {
                            case "":
                                obj_minNewProposedDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;
                                break;
                            case "Yes":
                                obj_minNewProposedDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;
                                break;
                            case "No":
                                obj_minNewProposedDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;
                                break;
                        }

                        Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                        switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                        {
                            case "":
                                obj_minNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;
                                break;
                            case "Yes":
                                obj_minNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;
                                break;
                            case "No":
                                obj_minNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;
                                break;
                        }

                        Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");
                        if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "") { obj_minNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null; }
                        else { obj_minNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text; }
                        lst_minNewProposedDGWASNewList.Add(obj_minNewProposedDGWASBlankForGridToObj);
                    }
                }
                //---------------
                obj_minNewProposedDGWASBlankForGridToObj = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure();
                obj_minNewProposedDGWASBlankForGridToObj.ApplicationCode = Convert.ToInt64(lblMiningApplicationCodeFrom.Text);
                if (NOCAPExternalUtility.IsNumeric(ddlTypeOfStructure.SelectedValue)) { obj_minNewProposedDGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(ddlTypeOfStructure.SelectedValue); }
                else { return; }
                if (txtYearOfConstruction.Text.Trim() == "") { obj_minNewProposedDGWASBlankForGridToObj.YearOfConstruction = null; }
                else { obj_minNewProposedDGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt32(txtYearOfConstruction.Text.Trim()); }

                if (txtDepthMeter.Text.Trim() == "") { obj_minNewProposedDGWASBlankForGridToObj.DepthExist = null; }
                else
                {
                    obj_minNewProposedDGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(txtDepthMeter.Text.Trim());
                }
                if (txtDiameterMM.Text.Trim() == "")
                {
                    obj_minNewProposedDGWASBlankForGridToObj.Diameter = null;
                }
                else
                {
                    obj_minNewProposedDGWASBlankForGridToObj.Diameter = Convert.ToInt32(txtDiameterMM.Text.Trim());
                }

                if (txtDepthWaterBelowGroundLevel.Text.Trim() == "")
                {
                    obj_minNewProposedDGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_minNewProposedDGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(txtDepthWaterBelowGroundLevel.Text.Trim());
                }
                if (txtDischarge.Text.Trim() == "")
                {
                    obj_minNewProposedDGWASBlankForGridToObj.Discharge = null;
                }
                else
                {
                    obj_minNewProposedDGWASBlankForGridToObj.Discharge = Convert.ToDecimal(txtDischarge.Text.Trim());
                }
                if (txtOperationalHoursDay.Text.Trim() == "")
                {
                    obj_minNewProposedDGWASBlankForGridToObj.OperationalHousrDay = null;
                }
                else
                {
                    obj_minNewProposedDGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(txtOperationalHoursDay.Text.Trim());
                }

                if (txtOperationalDaysYear.Text.Trim() == "")
                {
                    obj_minNewProposedDGWASBlankForGridToObj.OperationalDaysYear = null;
                }
                else
                {
                    obj_minNewProposedDGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(txtOperationalDaysYear.Text.Trim());
                }

                if (ddlModeOfLift.SelectedValue == "")
                {
                    obj_minNewProposedDGWASBlankForGridToObj.LiftingDeviceCode = null;
                }
                else
                {
                    if (NOCAPExternalUtility.IsNumeric(ddlModeOfLift.SelectedValue)) { obj_minNewProposedDGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(ddlModeOfLift.SelectedValue); }
                }
                if (txtHorsePowerPump.Text.Trim() == "")
                {
                    obj_minNewProposedDGWASBlankForGridToObj.PowerOfPump = null;
                }
                else
                {
                    obj_minNewProposedDGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(txtHorsePowerPump.Text.Trim());
                }

                switch (ddlWhetherFittedWithWaterMeter.SelectedValue)
                {
                    case "":
                        obj_minNewProposedDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_minNewProposedDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_minNewProposedDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }

                switch (ddlWhetherPermissionRegisteredWithCGWA.SelectedValue)
                {
                    case "":
                        obj_minNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_minNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_minNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (txtWhetherPermissionRegisteredWithCGWADet.Text.Trim() == "")
                {
                    obj_minNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_minNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = Convert.ToString(txtWhetherPermissionRegisteredWithCGWADet.Text.Trim());
                }

                lst_minNewProposedDGWASNewList.Add(obj_minNewProposedDGWASBlankForGridToObj);
                gvdeWateringPrposed.DataSource = lst_minNewProposedDGWASNewList;
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

                Server.Transfer("~/ExternalUser/MiningNew/De-WateringExistingStructure.aspx");

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
                UpdateMinNewProposedDGWASDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
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
                if (UpdateMinNewProposedDGWASDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)) == 1)
                {
                    Server.Transfer("~/ExternalUser/MiningNew/ProposedUtiizationOfPumpedWater.aspx");
                }
                else
                {
                }
            }
        }
    }

    private int UpdateMinNewProposedDGWASDetails(long lngA_ApplicationCode)
    {
        try
        {
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
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure> lst_minNewProposedDGWASList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure>();
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
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure obj_minNewProposedDGWAS = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure();
                obj_minNewProposedDGWAS.ApplicationCode = Convert.ToInt32(gvdeWateringPrposed.DataKeys[gvRow.RowIndex].Value.ToString());

                if (lbl_TypeOfAbstractionStructureCode.Text == "") { obj_minNewProposedDGWAS.TypeOfAbstractionStructureCode = 0; }
                else { obj_minNewProposedDGWAS.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text); }

                if (lbl_YearOfConstruction.Text.Trim() == "") { obj_minNewProposedDGWAS.YearOfConstruction = null; }
                else { obj_minNewProposedDGWAS.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim()); }

                if (lbl_DepthExist.Text.Trim() == "") { obj_minNewProposedDGWAS.DepthExist = null; }
                else { obj_minNewProposedDGWAS.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text); }

                if (lbl_Diameter.Text.Trim() == "") { obj_minNewProposedDGWAS.Diameter = null; }
                else { obj_minNewProposedDGWAS.Diameter = Convert.ToInt32(lbl_Diameter.Text); }

                if (lbl_DepthBelowWaterLevel.Text.Trim() == "") { obj_minNewProposedDGWAS.DepthBelowWaterLevel = null; }
                else { obj_minNewProposedDGWAS.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text); }

                if (lbl_Discharge.Text.Trim() == "") { obj_minNewProposedDGWAS.Discharge = null; }
                else { obj_minNewProposedDGWAS.Discharge = Convert.ToDecimal(lbl_Discharge.Text); }

                if (lbl_OperationalHousrDay.Text.Trim() == "") { obj_minNewProposedDGWAS.OperationalHousrDay = null; }
                else { obj_minNewProposedDGWAS.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text); }

                if (lbl_OperationalDaysYear.Text.Trim() == "") { obj_minNewProposedDGWAS.OperationalDaysYear = null; }
                else { obj_minNewProposedDGWAS.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text); }

                if (lbl_LiftingDeviceCode.Text.Trim() == "") { obj_minNewProposedDGWAS.LiftingDeviceCode = null; }
                else { obj_minNewProposedDGWAS.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text); }

                if (lbl_PowerOfPump.Text.Trim() == "") { obj_minNewProposedDGWAS.PowerOfPump = null; }
                else { obj_minNewProposedDGWAS.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text); }

                switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                {
                    case "":
                        obj_minNewProposedDGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;
                        break;
                    case "Yes":
                        obj_minNewProposedDGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;
                        break;
                    case "No":
                        obj_minNewProposedDGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;
                        break;
                }

                switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                {
                    case "":
                        obj_minNewProposedDGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;
                        break;
                    case "Yes":
                        obj_minNewProposedDGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;
                        break;
                    case "No":
                        obj_minNewProposedDGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;
                        break;
                }
                if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "") { obj_minNewProposedDGWAS.WhetherPermissionRegisteredWithCGWADetails = null; }
                else { obj_minNewProposedDGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text; }
                obj_minNewProposedDGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                lst_minNewProposedDGWASList.Add(obj_minNewProposedDGWAS);
            }

            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure[] arr_tempMinDeWateringProposedListBLL = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure[lst_minNewProposedDGWASList.Count];
            lst_minNewProposedDGWASList.CopyTo(arr_tempMinDeWateringProposedListBLL);
            obj_MiningNewApplication.MiningNewProposedDeWateringAbstractionStructureList = arr_tempMinDeWateringProposedListBLL;

            if (txtNoOfProposedStructure.Text.Trim() == "") { obj_MiningNewApplication.DewateringNoOfProposedStructure = 0; }
            else { obj_MiningNewApplication.DewateringNoOfProposedStructure = Convert.ToInt32(txtNoOfProposedStructure.Text.Trim()); }

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
                BindIGVMinNewProposedDGWASDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
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
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure obj_MinNewProposedDGWASBlankForGridToObj = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure();
                    List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure> lst_MinNewProposedDGWASNewList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure>();
                    foreach (GridViewRow gvRow in gvdeWateringPrposed.Rows)
                    {
                        obj_MinNewProposedDGWASBlankForGridToObj = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure();
                        obj_MinNewProposedDGWASBlankForGridToObj.ApplicationCode = Convert.ToInt64(gvdeWateringPrposed.DataKeys[gvRow.RowIndex].Value.ToString());
                        if (Convert.ToInt64(gvdeWateringPrposed.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                        {
                            Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                            if (lbl_SerialNumber.Text.Trim() == "") { obj_MinNewProposedDGWASBlankForGridToObj.SerialNumber = 0; }
                            else { obj_MinNewProposedDGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim()); }

                            Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");
                            if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "") { obj_MinNewProposedDGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0; }
                            else { obj_MinNewProposedDGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim()); }

                            Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                            if (lbl_YearOfConstruction.Text.Trim() == "") { obj_MinNewProposedDGWASBlankForGridToObj.YearOfConstruction = null; }
                            else { obj_MinNewProposedDGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim()); }

                            Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");
                            if (lbl_DepthExist.Text.Trim() == "") { obj_MinNewProposedDGWASBlankForGridToObj.DepthExist = null; }
                            else { obj_MinNewProposedDGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text); }

                            Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");
                            if (lbl_Diameter.Text.Trim() == "") { obj_MinNewProposedDGWASBlankForGridToObj.Diameter = null; }
                            else { obj_MinNewProposedDGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text); }

                            Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");
                            if (lbl_DepthBelowWaterLevel.Text.Trim() == "") { obj_MinNewProposedDGWASBlankForGridToObj.DepthBelowWaterLevel = null; }
                            else { obj_MinNewProposedDGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text); }

                            Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");
                            if (lbl_Discharge.Text.Trim() == "") { obj_MinNewProposedDGWASBlankForGridToObj.Discharge = null; }
                            else { obj_MinNewProposedDGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text); }

                            Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");
                            if (lbl_OperationalHousrDay.Text.Trim() == "") { obj_MinNewProposedDGWASBlankForGridToObj.OperationalHousrDay = null; }
                            else { obj_MinNewProposedDGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text); }

                            Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");
                            if (lbl_OperationalDaysYear.Text.Trim() == "") { obj_MinNewProposedDGWASBlankForGridToObj.OperationalDaysYear = null; }
                            else { obj_MinNewProposedDGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text); }

                            Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");
                            if (lbl_LiftingDeviceCode.Text.Trim() == "") { obj_MinNewProposedDGWASBlankForGridToObj.LiftingDeviceCode = null; }
                            else { obj_MinNewProposedDGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text); }

                            Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");
                            if (lbl_PowerOfPump.Text.Trim() == "") { obj_MinNewProposedDGWASBlankForGridToObj.PowerOfPump = null; }
                            else { obj_MinNewProposedDGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text); }

                            Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                            switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                            {
                                case "":
                                    obj_MinNewProposedDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;
                                    break;
                                case "Yes":
                                    obj_MinNewProposedDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;
                                    break;
                                case "No":
                                    obj_MinNewProposedDGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;
                                    break;
                            }
                            Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                            switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                            {
                                case "":
                                    obj_MinNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;
                                    break;
                                case "Yes":
                                    obj_MinNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;
                                    break;
                                case "No":
                                    obj_MinNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;
                                    break;
                            }

                            Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");
                            if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "") { obj_MinNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null; }
                            else { obj_MinNewProposedDGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text; }
                            lst_MinNewProposedDGWASNewList.Add(obj_MinNewProposedDGWASBlankForGridToObj);
                        }
                    }
                    lst_MinNewProposedDGWASNewList.RemoveAt(lst_IntIndex);
                    gvdeWateringPrposed.DataSource = lst_MinNewProposedDGWASNewList;
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
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure obj_MinNewProposedDGWAS = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure();
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
                    obj_MinNewProposedDGWAS = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADProposedDeWateringAbstractionStructure(Convert.ToInt64(gvdeWateringPrposed.DataKeys[e.Row.RowIndex].Values[0].ToString()), Convert.ToInt32(lbl_SerialNumber.Text));
                    lbl_TypeOfAbstractionStructureName.Text = HttpUtility.HtmlEncode(obj_MinNewProposedDGWAS.GetTypeOfAbstractionStructure().TypeOfAbstractionStructureDesc);
                    if (obj_MinNewProposedDGWAS.GetLiftingDevice() != null)
                    {
                        lbl_LiftingDeviceName.Text = HttpUtility.HtmlEncode(obj_MinNewProposedDGWAS.GetLiftingDevice().LiftingDeviceDesc);
                        lbl_LiftingDeviceCode.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MinNewProposedDGWAS.GetLiftingDevice().LiftingDeviceCode));
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