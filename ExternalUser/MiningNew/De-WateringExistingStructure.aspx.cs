using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_Mining_De_WateringExistingStructure : System.Web.UI.Page
{
    string strPageName = "MINDe-WateringExistingStructure";
    string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        ScriptManager.RegisterStartupScript(this, GetType(), "function", "CalculateSumGroundWaterReq();", true);
        if (!IsPostBack)
        {
            ValidationExpInit();
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            RngValYearOfConstruction.MaximumValue = System.DateTime.Now.Year.ToString();
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

                if (NOCAPExternalUtility.FillDropDownTypeOfStructure(ref ddlTypeOfStructure) == 0)
                {
                    lblMessage.Text = "Problem in Type of Structure population";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                if (NOCAPExternalUtility.FillDropDownLiftingDevice(ref ddlModeOfLift) == 0)
                {
                    lblMessage.Text = "Problem in Mode of Lifting population";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }


                if (lblModeFrom.Text.Trim() == "Edit") { BindDeWateringExistingStructureDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text)); }
                ddlDewateringRequired_SelectedIndexChanged(sender, e);
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally { }
        }
    }

    private void ValidationExpInit()
    {
        revtxtMinimumPremansoon.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtMinimumPremansoon.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtMinimunPostMansoon.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtMinimunPostMansoon.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtMaximumPreMansoon.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtMaximumPreMansoon.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtMaximunPostMansoon.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtMaximunPostMansoon.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtmaximumDeptProposed.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtmaximumDeptProposed.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtGWFlowDirection.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtGWFlowDirection.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revtxtOtherInformation.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtOtherInformation.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revtxtGWRequirementAbstractStruct.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtGWRequirementAbstractStruct.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revatxtGWRequirementAbstractStructYear.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revatxtGWRequirementAbstractStructYear.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        revtxtGWrequiredMiningSeeping.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtGWrequiredMiningSeeping.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtGWrequiredMiningSeepingYear.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtGWrequiredMiningSeepingYear.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        revtxtNumberOfExistingDewaterStruct.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtNumberOfExistingDewaterStruct.ErrorMessage = ValidationUtility.txtValForNumericMsg;

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

    private void BindDeWateringExistingStructureDetails(long lngA_ApplicationCode)
    {
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure obj_minNewExistingDeWateringBlank = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure();
        List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure> lst_minNewExistingDeWateringBlank = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure>();
        List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure> lst_minNewExistingDeWatering = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure>();

        try
        {
            switch (obj_MiningNewApplication.MiningNewSADDewatering.WhetherGWTableIntersected)
            {
                case NOCAP.BLL.Mining.New.SADApplication.MiningNewSADDewatering.GWTableIntersected.Yes:
                    ddlDewateringRequired.SelectedValue = "Yes";
                    break;
                case NOCAP.BLL.Mining.New.SADApplication.MiningNewSADDewatering.GWTableIntersected.No:
                    ddlDewateringRequired.SelectedValue = "No";
                    break;
            }
            txtNumberOfExistingDewaterStruct.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.DewateringNoOfExistingStructure));
            if (txtNumberOfExistingDewaterStruct.Text == "") { txtNumberOfExistingDewaterStruct.Text = "0"; }
            txtMinimumPremansoon.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.MiningNewSADDewatering.GWTableDepthMinPreMansoon));
            txtMinimunPostMansoon.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.MiningNewSADDewatering.GWTableDepthMinPostMansoon));
            
            txtMaximumPreMansoon.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.MiningNewSADDewatering.GWTableDepthMaxPreMansoon));
            txtMaximunPostMansoon.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.MiningNewSADDewatering.GWTableDepthMaxPostMansoon));

            txtmaximumDeptProposed.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.MiningNewSADDewatering.MaxDepthPrposedtoDewater));
            txtGWFlowDirection.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.MiningNewSADDewatering.GWFlowDirection));
            txtOtherInformation.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.MiningNewSADDewatering.AnyOtherInformation));
            txtGWRequirementAbstractStruct.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.GWREquiredThroughAbstractStructure));
            txtGWRequirementAbstractStructYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.GWREquiredThroughAbstractStructureYear));
            txtGWrequiredMiningSeeping.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.GWRequiredThroughMiningSeeping));
            txtGWrequiredMiningSeepingYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.GWRequiredThroughMiningSeepingYear));
            
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure[] arr_minNewExistingDeWatering;
            arr_minNewExistingDeWatering = obj_MiningNewApplication.GetMiningNewSADExistingDeWateringAbstractionStructureList();
            foreach (NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure test in arr_minNewExistingDeWatering) { lst_minNewExistingDeWatering.Add(test); }

            //if (arr_minNewExistingDeWatering.Count() > 0)
            //{
                gvdeWateringExisting.DataSource = lst_minNewExistingDeWatering;
                gvdeWateringExisting.DataBind();
            //}
            //else
            //{
            //    lst_minNewExistingDeWateringBlank.Add(obj_minNewExistingDeWateringBlank);
            //    gvdeWateringExisting.DataSource = lst_minNewExistingDeWateringBlank;
            //    gvdeWateringExisting.DataBind();
            //    int int_NoOfCol = 0;
            //    int_NoOfCol = gvdeWateringExisting.Rows[0].Cells.Count;
            //    gvdeWateringExisting.Rows[0].Cells.Clear();
            //    gvdeWateringExisting.Rows[0].Cells.Add(new TableCell());
            //    gvdeWateringExisting.Rows[0].Cells[0].ColumnSpan = int_NoOfCol;
            //    gvdeWateringExisting.Rows[0].Cells[0].Text = "No Records exsist in GAS Existing";
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
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure obj_minNewExistingDeWateringBlankForGridToObj = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure();
                List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure> lst_minNewExistingDeWateringNewList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure>();
                foreach (GridViewRow gvRow in gvdeWateringExisting.Rows)
                {
                    obj_minNewExistingDeWateringBlankForGridToObj = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure();
                    obj_minNewExistingDeWateringBlankForGridToObj.ApplicationCode = Convert.ToInt64(gvdeWateringExisting.DataKeys[gvRow.RowIndex].Value.ToString());
                    int ss = gvRow.RowIndex;
                    if (Convert.ToInt64(gvdeWateringExisting.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                    {
                        Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                        if (lbl_SerialNumber.Text.Trim() == "")
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.SerialNumber = 0;
                        }
                        else
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                        }

                        Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");

                        if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                        }
                        else
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                        }
                        Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                        if (lbl_YearOfConstruction.Text.Trim() == "")
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.YearOfConstruction = null;
                        }
                        else
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                        }
                        Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                        if (lbl_DepthExist.Text.Trim() == "")
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.DepthExist = null;
                        }
                        else
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                        }
                        Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                        if (lbl_Diameter.Text.Trim() == "")
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.Diameter = null;
                        }
                        else
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                        }
                        Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                        if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.DepthBelowWaterLevel = null;
                        }
                        else
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                        }
                        Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                        if (lbl_Discharge.Text.Trim() == "")
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.Discharge = null;
                        }
                        else
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                        }
                        Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                        if (lbl_OperationalHousrDay.Text.Trim() == "")
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.OperationalHousrDay = null;
                        }
                        else
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                        }

                        Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                        if (lbl_OperationalDaysYear.Text.Trim() == "")
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.OperationalDaysYear = null;
                        }
                        else
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                        }
                        Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                        if (lbl_LiftingDeviceCode.Text.Trim() == "")
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.LiftingDeviceCode = null;
                        }
                        else
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                        }
                        Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                        if (lbl_PowerOfPump.Text.Trim() == "")
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.PowerOfPump = null;
                        }
                        else
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                        }

                        Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                        switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                        {
                            case "":
                                obj_minNewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                break;
                            case "Yes":
                                obj_minNewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                break;
                            case "No":
                                obj_minNewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                break;
                        }
                        Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                        switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                        {
                            case "":
                                obj_minNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                break;
                            case "Yes":
                                obj_minNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                break;
                            case "No":
                                obj_minNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                break;
                        }

                        Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                        if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                        }
                        else
                        {
                            obj_minNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                        }

                        lst_minNewExistingDeWateringNewList.Add(obj_minNewExistingDeWateringBlankForGridToObj);


                    }
                }
                //---------------

                obj_minNewExistingDeWateringBlankForGridToObj = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure();
                obj_minNewExistingDeWateringBlankForGridToObj.ApplicationCode = Convert.ToInt64(lblMiningApplicationCodeFrom.Text);

                if (NOCAPExternalUtility.IsNumeric(ddlTypeOfStructure.SelectedValue)) { obj_minNewExistingDeWateringBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(ddlTypeOfStructure.SelectedValue); } else { return; }

                if (txtYearOfConstruction.Text.Trim() == "")
                {
                    obj_minNewExistingDeWateringBlankForGridToObj.YearOfConstruction = null;
                }
                else
                {
                    obj_minNewExistingDeWateringBlankForGridToObj.YearOfConstruction = Convert.ToInt32(txtYearOfConstruction.Text.Trim());
                }
                if (txtDepthMeter.Text.Trim() == "")
                {
                    obj_minNewExistingDeWateringBlankForGridToObj.DepthExist = null;
                }
                else
                {
                    obj_minNewExistingDeWateringBlankForGridToObj.DepthExist = Convert.ToDecimal(txtDepthMeter.Text.Trim());
                }
                if (txtDiameterMM.Text.Trim() == "")
                {
                    obj_minNewExistingDeWateringBlankForGridToObj.Diameter = null;
                }
                else
                {
                    obj_minNewExistingDeWateringBlankForGridToObj.Diameter = Convert.ToInt32(txtDiameterMM.Text.Trim());
                }

                if (txtDepthWaterBelowGroundLevel.Text.Trim() == "")
                {
                    obj_minNewExistingDeWateringBlankForGridToObj.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_minNewExistingDeWateringBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(txtDepthWaterBelowGroundLevel.Text.Trim());
                }
                if (txtDischarge.Text.Trim() == "")
                {
                    obj_minNewExistingDeWateringBlankForGridToObj.Discharge = null;
                }
                else
                {
                    obj_minNewExistingDeWateringBlankForGridToObj.Discharge = Convert.ToDecimal(txtDischarge.Text.Trim());
                }
                if (txtOperationalHoursDay.Text.Trim() == "")
                {
                    obj_minNewExistingDeWateringBlankForGridToObj.OperationalHousrDay = null;
                }
                else
                {
                    obj_minNewExistingDeWateringBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(txtOperationalHoursDay.Text.Trim());
                }

                if (txtOperationalDaysYear.Text.Trim() == "")
                {
                    obj_minNewExistingDeWateringBlankForGridToObj.OperationalDaysYear = null;
                }
                else
                {
                    obj_minNewExistingDeWateringBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(txtOperationalDaysYear.Text.Trim());
                }

                if (ddlModeOfLift.SelectedValue == "")
                {
                    obj_minNewExistingDeWateringBlankForGridToObj.LiftingDeviceCode = null;
                }
                else
                {
                    if (NOCAPExternalUtility.IsNumeric(ddlModeOfLift.SelectedValue)) { obj_minNewExistingDeWateringBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(ddlModeOfLift.SelectedValue); }
                }
                if (txtHorsePowerPump.Text.Trim() == "")
                {
                    obj_minNewExistingDeWateringBlankForGridToObj.PowerOfPump = null;
                }
                else
                {
                    obj_minNewExistingDeWateringBlankForGridToObj.PowerOfPump = Convert.ToDecimal(txtHorsePowerPump.Text.Trim());
                }

                switch (ddlWhetherFittedWithWaterMeter.SelectedValue)
                {
                    case "":
                        obj_minNewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_minNewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_minNewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }

                switch (ddlWhetherPermissionRegisteredWithCGWA.SelectedValue)
                {
                    case "":
                        obj_minNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_minNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_minNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (txtWhetherPermissionRegisteredWithCGWADet.Text.Trim() == "")
                {
                    obj_minNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_minNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = Convert.ToString(txtWhetherPermissionRegisteredWithCGWADet.Text.Trim());
                }

                lst_minNewExistingDeWateringNewList.Add(obj_minNewExistingDeWateringBlankForGridToObj);

                gvdeWateringExisting.DataSource = lst_minNewExistingDeWateringNewList;
                gvdeWateringExisting.DataBind();

                ClearDeWateringExistingScreen();
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
                UpdateminNewExistingDeWateringDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
            }
        }
    }

    private int UpdateminNewExistingDeWateringDetails(long lngA_ApplicationCode)
    {
        try
        {
            if (txtNumberOfExistingDewaterStruct.Text.Trim() == "")
            {
                lblMessage.Text = "Number of Existing Structures can not be Blank";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            if (!NOCAPExternalUtility.IsNumeric(txtNumberOfExistingDewaterStruct.Text))
            {
                lblMessage.Text = "Number of Existing Structures allows Numeric";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            if (Convert.ToInt32(txtNumberOfExistingDewaterStruct.Text) != gvdeWateringExisting.Rows.Count)
            {
                lblMessage.Text = "Number of Records of Detailed Structures should be equal to the Number of Existing Structures";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            decimal GWRequirement = Convert.ToDecimal(txtGWRequirementAbstractStruct.Text == "" ? "0" : txtGWRequirementAbstractStruct.Text) + Convert.ToDecimal(txtGWrequiredMiningSeeping.Text == "" ? "0" : txtGWrequiredMiningSeeping.Text);
            if (GWRequirement == 0)
            {
                lblMessage.Text = "14.(Ground Water required through Abstract Structure + Ground Water required through Dewatering / Mining Seeping) can not be Zero.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }



            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure> lst_minNewExistingDeWateringList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure>();
            foreach (GridViewRow gvRow in gvdeWateringExisting.Rows)
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

                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure obj_minNewExistingDeWatering = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure();
                obj_minNewExistingDeWatering.ApplicationCode = Convert.ToInt32(gvdeWateringExisting.DataKeys[gvRow.RowIndex].Value.ToString());

                if (lbl_TypeOfAbstractionStructureCode.Text == "") { obj_minNewExistingDeWatering.TypeOfAbstractionStructureCode = 0; }
                else { obj_minNewExistingDeWatering.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text); }
                
                if (lbl_YearOfConstruction.Text.Trim() == "")
                {
                    obj_minNewExistingDeWatering.YearOfConstruction = null;
                }
                else
                {
                    obj_minNewExistingDeWatering.YearOfConstruction = Convert.ToInt32(lbl_YearOfConstruction.Text);
                }

                if (lbl_DepthExist.Text.Trim() == "")
                {
                    obj_minNewExistingDeWatering.DepthExist = null;
                }
                else
                {
                    obj_minNewExistingDeWatering.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                }
                if (lbl_Diameter.Text.Trim() == "")
                {
                    obj_minNewExistingDeWatering.Diameter = null;
                }
                else
                {
                    obj_minNewExistingDeWatering.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                }
                if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                {
                    obj_minNewExistingDeWatering.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_minNewExistingDeWatering.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                }
                if (lbl_Discharge.Text.Trim() == "")
                {
                    obj_minNewExistingDeWatering.Discharge = null;
                }
                else
                {
                    obj_minNewExistingDeWatering.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                }
                if (lbl_OperationalHousrDay.Text.Trim() == "")
                {
                    obj_minNewExistingDeWatering.OperationalHousrDay = null;
                }
                else
                {
                    obj_minNewExistingDeWatering.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                }
                if (lbl_OperationalDaysYear.Text.Trim() == "")
                {
                    obj_minNewExistingDeWatering.OperationalDaysYear = null;
                }
                else
                {
                    obj_minNewExistingDeWatering.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                }
                if (lbl_LiftingDeviceCode.Text.Trim() == "")
                {
                    obj_minNewExistingDeWatering.LiftingDeviceCode = null;
                }
                else
                {
                    obj_minNewExistingDeWatering.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                }
                if (lbl_PowerOfPump.Text.Trim() == "")
                {
                    obj_minNewExistingDeWatering.PowerOfPump = null;
                }
                else
                {
                    obj_minNewExistingDeWatering.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                }
                switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                {
                    case "":
                        obj_minNewExistingDeWatering.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_minNewExistingDeWatering.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_minNewExistingDeWatering.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }
                switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                {
                    case "":
                        obj_minNewExistingDeWatering.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_minNewExistingDeWatering.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_minNewExistingDeWatering.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                {
                    obj_minNewExistingDeWatering.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_minNewExistingDeWatering.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                }

                obj_minNewExistingDeWatering.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;

                lst_minNewExistingDeWateringList.Add(obj_minNewExistingDeWatering);
            }

            obj_miningNewApplication.GWREquiredThroughAbstractStructure =Convert.ToDecimal(txtGWRequirementAbstractStruct.Text);
            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure[] arr_tempMinDeWateringExistingListBLL = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure[lst_minNewExistingDeWateringList.Count];
            lst_minNewExistingDeWateringList.CopyTo(arr_tempMinDeWateringExistingListBLL);
            obj_miningNewApplication.MiningNewExistingDeWateringAbstractionStructureList = arr_tempMinDeWateringExistingListBLL;

            switch (ddlDewateringRequired.SelectedValue)
            {
                case "Yes":
                    obj_miningNewApplication.MiningNewSADDewatering.WhetherGWTableIntersected = NOCAP.BLL.Mining.New.SADApplication.MiningNewSADDewatering.GWTableIntersected.Yes;
                    break;
                case "No":
                    obj_miningNewApplication.MiningNewSADDewatering.WhetherGWTableIntersected = NOCAP.BLL.Mining.New.SADApplication.MiningNewSADDewatering.GWTableIntersected.No;
                    break;
            }

            if (txtMinimumPremansoon.Text == "") { obj_miningNewApplication.MiningNewSADDewatering.GWTableDepthMinPreMansoon = null; }
            else { obj_miningNewApplication.MiningNewSADDewatering.GWTableDepthMinPreMansoon = Convert.ToDecimal(txtMinimumPremansoon.Text.Trim()); }

            if (txtMinimunPostMansoon.Text == "") { obj_miningNewApplication.MiningNewSADDewatering.GWTableDepthMinPostMansoon = null; }
            else { obj_miningNewApplication.MiningNewSADDewatering.GWTableDepthMinPostMansoon = Convert.ToDecimal(txtMinimunPostMansoon.Text.Trim()); }

            if (txtMaximumPreMansoon.Text == "") { obj_miningNewApplication.MiningNewSADDewatering.GWTableDepthMaxPreMansoon = null; }
            else { obj_miningNewApplication.MiningNewSADDewatering.GWTableDepthMaxPreMansoon = Convert.ToDecimal(txtMaximumPreMansoon.Text.Trim()); }

            if (txtMaximunPostMansoon.Text == "") { obj_miningNewApplication.MiningNewSADDewatering.GWTableDepthMaxPostMansoon = null; }
            else { obj_miningNewApplication.MiningNewSADDewatering.GWTableDepthMaxPostMansoon = Convert.ToDecimal(txtMaximunPostMansoon.Text.Trim()); }

            if (txtmaximumDeptProposed.Text == "") { obj_miningNewApplication.MiningNewSADDewatering.MaxDepthPrposedtoDewater = null; }
            else { obj_miningNewApplication.MiningNewSADDewatering.MaxDepthPrposedtoDewater = Convert.ToDecimal(txtmaximumDeptProposed.Text.Trim()); }

            if (txtGWFlowDirection.Text == "") { obj_miningNewApplication.MiningNewSADDewatering.GWFlowDirection = null; }
            else { obj_miningNewApplication.MiningNewSADDewatering.GWFlowDirection = txtGWFlowDirection.Text.Trim(); }

            if (txtOtherInformation.Text == "") { obj_miningNewApplication.MiningNewSADDewatering.AnyOtherInformation = null; }
            else { obj_miningNewApplication.MiningNewSADDewatering.AnyOtherInformation = Convert.ToString(txtOtherInformation.Text.Trim()); }

            if (txtGWRequirementAbstractStruct.Text == "") { obj_miningNewApplication.GWREquiredThroughAbstractStructure = null; }
            else { obj_miningNewApplication.GWREquiredThroughAbstractStructure = Convert.ToDecimal(txtGWRequirementAbstractStruct.Text.Trim()); }

            if (txtGWRequirementAbstractStructYear.Text == "") { obj_miningNewApplication.GWREquiredThroughAbstractStructureYear = null; }
            else { obj_miningNewApplication.GWREquiredThroughAbstractStructureYear = Convert.ToDecimal(txtGWRequirementAbstractStructYear.Text.Trim()); }

            if (txtGWrequiredMiningSeeping.Text == "") { obj_miningNewApplication.GWRequiredThroughMiningSeeping = null; }
            else { obj_miningNewApplication.GWRequiredThroughMiningSeeping = Convert.ToDecimal(txtGWrequiredMiningSeeping.Text.Trim()); }

            if (txtGWrequiredMiningSeepingYear.Text == "") { obj_miningNewApplication.GWRequiredThroughMiningSeepingYear = null; }
            else { obj_miningNewApplication.GWRequiredThroughMiningSeepingYear = Convert.ToDecimal(txtGWrequiredMiningSeepingYear.Text.Trim()); }

            if (txtNumberOfExistingDewaterStruct.Text.Trim() == "") { obj_miningNewApplication.NumberOfStructureExisting = null; }
            else { obj_miningNewApplication.DewateringNoOfExistingStructure = Convert.ToInt32(txtNumberOfExistingDewaterStruct.Text.Trim()); }

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_miningNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_miningNewApplication.Update() == 1)
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

                lblMessage.Text = obj_miningNewApplication.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                BindDeWateringExistingStructureDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
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
                if (UpdateminNewExistingDeWateringDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)) == 1)
                {
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                    decimal dec_netGroundWaterRequirement = Convert.ToDecimal(txtGWRequirementAbstractStruct.Text == "" ? "0" : txtGWRequirementAbstractStruct.Text) + Convert.ToDecimal(txtGWrequiredMiningSeeping.Text == "" ? "0" : txtGWrequiredMiningSeeping.Text);
                    if (obj_miningNewApplication.MSME == NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.MSMEYesNo.Yes && dec_netGroundWaterRequirement < 10 && obj_miningNewApplication.MSMETypeCode != 3)
                    {

                        // lblMessage.Text = "You are exempted from seeking NOC for ground water withdrawal from CGWA, since your project is under MSME category and ground water withdrawal is less than 10 KLD as per new guidelines w.e.f 24.09.2020";
                        // lblMessage.ForeColor = System.Drawing.Color.Green;
                        Server.Transfer("~/ExternalUser/MiningNew/SalientFeature.aspx");

                    }
                    else
                    {
                        Server.Transfer("~/ExternalUser/MiningNew/De-WateringProposedStructure.aspx");
                    }
                    
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
            if (Page.IsValid)
            {
                Server.Transfer("~/ExternalUser/MiningNew/LandUseDetails.aspx");
            }
        }
    }
    protected void ddlDewateringRequired_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            if (ddlDewateringRequired.SelectedValue == "No")
            {
                txtMinimumPremansoon.Text = "";
                txtMinimunPostMansoon.Text = "";
                RequiredFieldValidatortxtMinimumDepth.Enabled = false;
                RequiredFieldValidator2.Enabled = false;
                txtMaximumPreMansoon.Text = "";
                txtMaximunPostMansoon.Text = "";
                rfvtxtMaximumPreMansoon.Enabled = false;
                rfvtxtMaximunPostMansoon.Enabled = false;
                txtmaximumDeptProposed.Text = "";
                rfvtxtmaximumDeptProposed.Enabled = false;
                txtGWFlowDirection.Text = "";
                rfvtxtGWFlowDirection.Enabled = false;
                txtOtherInformation.Text = "";
                rfvtxtOtherInformation.Enabled = false;
                txtGWrequiredMiningSeeping.Text = "";
                //rfvtxtGWrequiredMiningSeeping.Enabled = false;
                txtGWrequiredMiningSeepingYear.Text = "";
                rfvtxtGWrequiredMiningSeepingYear.Enabled = false;
                txtNumberOfExistingDewaterStruct.Text = "";
                rfvtxtNumberOfExistingDewaterStruct.Enabled = false;
                txtNumberOfExistingDewaterStruct.Text = "0";
                ClearDeWateringExistingScreen();
                //gvdeWateringExisting.DataSource = null;
                //gvdeWateringExisting.DataBind();
                EnableDisableAll(false);
            }
            else if (ddlDewateringRequired.SelectedValue == "Yes")
            {
                rfvtxtMaximumPreMansoon.Enabled = true;
                rfvtxtMaximunPostMansoon.Enabled = true;
                RequiredFieldValidatortxtMinimumDepth.Enabled = true;
                RequiredFieldValidator2.Enabled = true;
                rfvtxtmaximumDeptProposed.Enabled = true;
                rfvtxtGWFlowDirection.Enabled = true;
                rfvtxtOtherInformation.Enabled = true;
                rfvtxtGWRequirementAbstractStruct.Enabled = true;
                //rfvtxtGWrequiredMiningSeeping.Enabled = true;
                rfvtxtGWrequiredMiningSeepingYear.Enabled = true;
                rfvtxtNumberOfExistingDewaterStruct.Enabled = true;
                EnableDisableAll(true);
            }
            if (txtNumberOfExistingDewaterStruct.Text == "")
            {
                txtNumberOfExistingDewaterStruct.Text = "0";
            }
        }
    }

    private void EnableDisableAll(bool Status)
    {
        txtNumberOfExistingDewaterStruct.Enabled = Status;
        txtMinimumPremansoon.Enabled = Status;
        txtMinimunPostMansoon.Enabled = Status;
        txtMaximumPreMansoon.Enabled = Status;
        txtMaximunPostMansoon.Enabled = Status;
        txtmaximumDeptProposed.Enabled = Status;
        txtGWFlowDirection.Enabled = Status;
        txtOtherInformation.Enabled = Status;
        //txtGWRequirementAbstractStruct.Enabled = Status;
        txtGWrequiredMiningSeeping.Enabled = Status;
        txtGWrequiredMiningSeepingYear.Enabled = Status;
        txtNumberOfExistingDewaterStruct.Enabled = Status;
        ddlTypeOfStructure.Enabled = Status;
        txtYearOfConstruction.Enabled = Status;
        txtDepthMeter.Enabled = Status;
        txtDiameterMM.Enabled = Status;
        txtDepthWaterBelowGroundLevel.Enabled = Status;
        txtDischarge.Enabled = Status;
        txtOperationalHoursDay.Enabled = Status;
        txtOperationalDaysYear.Enabled = Status;
        ddlModeOfLift.Enabled = Status;
        txtHorsePowerPump.Enabled = Status;
        ddlWhetherFittedWithWaterMeter.Enabled = Status;
        ddlWhetherPermissionRegisteredWithCGWA.Enabled = Status;
        txtWhetherPermissionRegisteredWithCGWADet.Enabled = Status;
        btnAdd.Enabled = Status;
    }

    private void ClearDeWateringExistingScreen()
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
 
    protected void gvdeWateringExisting_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure obj_minNewExistingDeWateringBlankForGridToObj = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure();
                    List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure> lst_minNewExistingDeWateringNewList = new List<NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure>();


                    foreach (GridViewRow gvRow in gvdeWateringExisting.Rows)
                    {
                        obj_minNewExistingDeWateringBlankForGridToObj = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure();
                        obj_minNewExistingDeWateringBlankForGridToObj.ApplicationCode = Convert.ToInt64(gvdeWateringExisting.DataKeys[gvRow.RowIndex].Value.ToString());
                        if (Convert.ToInt64(gvdeWateringExisting.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                        {
                            Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                            if (lbl_SerialNumber.Text.Trim() == "")
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.SerialNumber = 0;
                            }
                            else
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                            }
                            Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");
                            if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                            }
                            else
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                            }
                            Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                            if (lbl_YearOfConstruction.Text.Trim() == "")
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.YearOfConstruction = null;
                            }
                            else
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                            }
                            Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                            if (lbl_DepthExist.Text.Trim() == "")
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.DepthExist = null;
                            }
                            else
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                            }
                            Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                            if (lbl_Diameter.Text.Trim() == "")
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.Diameter = null;
                            }
                            else
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                            }
                            Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                            if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.DepthBelowWaterLevel = null;
                            }
                            else
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                            }
                            Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                            if (lbl_Discharge.Text.Trim() == "")
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.Discharge = null;
                            }
                            else
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                            }
                            Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                            if (lbl_OperationalHousrDay.Text.Trim() == "")
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.OperationalHousrDay = null;
                            }
                            else
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                            }

                            Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                            if (lbl_OperationalDaysYear.Text.Trim() == "")
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.OperationalDaysYear = null;
                            }
                            else
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                            }
                            Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                            if (lbl_LiftingDeviceCode.Text.Trim() == "")
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.LiftingDeviceCode = null;
                            }
                            else
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                            }
                            Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                            if (lbl_PowerOfPump.Text.Trim() == "")
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.PowerOfPump = null;
                            }
                            else
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                            }

                            Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                            switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                            {
                                case "":
                                    obj_minNewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_minNewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                    break;
                                case "No":
                                    obj_minNewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                    break;
                            }
                            Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                            switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                            {
                                case "":
                                    obj_minNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_minNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                    break;
                                case "No":
                                    obj_minNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                    break;
                            }

                            Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                            if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                            }
                            else
                            {
                                obj_minNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                            }

                            lst_minNewExistingDeWateringNewList.Add(obj_minNewExistingDeWateringBlankForGridToObj);


                        }
                    }
                    lst_minNewExistingDeWateringNewList.RemoveAt(lst_IntIndex);
                    gvdeWateringExisting.DataSource = lst_minNewExistingDeWateringNewList;
                    gvdeWateringExisting.DataBind();

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
    protected void gvdeWateringExisting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure obj_minNewExistingDGWAS = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure();
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
                    obj_minNewExistingDGWAS = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADExistingDeWateringAbstractionStructure(Convert.ToInt64(gvdeWateringExisting.DataKeys[e.Row.RowIndex].Values[0].ToString()), Convert.ToInt32(lbl_SerialNumber.Text));
                    lbl_TypeOfAbstractionStructureName.Text = HttpUtility.HtmlEncode(obj_minNewExistingDGWAS.GetTypeOfAbstractionStructure().TypeOfAbstractionStructureDesc);
                    if (obj_minNewExistingDGWAS.GetLiftingDevice() != null)
                    {
                        lbl_LiftingDeviceName.Text = HttpUtility.HtmlEncode(obj_minNewExistingDGWAS.GetLiftingDevice().LiftingDeviceDesc);
                        lbl_LiftingDeviceCode.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_minNewExistingDGWAS.GetLiftingDevice().LiftingDeviceCode));
                    }
                }
            }
            else { }
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