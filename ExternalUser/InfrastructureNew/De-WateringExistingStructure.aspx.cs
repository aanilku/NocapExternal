using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_InfrastructureNew_De_WateringExistingStructure : System.Web.UI.Page
{
    string strPageName = "INFDeWateringExistringStructure";
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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblInfrastructureApplicationCodeFrom");
                        if (SourceLabel != null) { lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); }
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


                if (lblModeFrom.Text.Trim() == "Edit") { BindDeWateringExistingStructureDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text)); }
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
        revtxtMinimumDepth.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtMinimumDepth.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtMaximumDepth.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtMaximumDepth.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtmaximumDeptProposed.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtmaximumDeptProposed.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtProQtyOfGroundWaterDewaterdDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtProQtyOfGroundWaterDewaterdDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtProQtyOfGroundWaterDewaterdYear.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtProQtyOfGroundWaterDewaterdYear.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        revtxtProposedUtilizationPumpedwater.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtProposedUtilizationPumpedwater.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revtxtOtherInformation.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtOtherInformation.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

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
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure obj_infNewExistingDeWateringBlank = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure();
        List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure> lst_infNewExistingDeWateringBlank = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure>();
        List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure> lst_infNewExistingDeWatering = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure>();

        try
        {
            switch (obj_infrastructureNewApplication.InfrastructureDewatering.DewateringRequired)
            {
                case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADDewatering.deWateringRequired.Yes:
                    ddlDewateringRequired.SelectedValue = "Yes";
                    break;
                case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADDewatering.deWateringRequired.No:
                    ddlDewateringRequired.SelectedValue = "No";
                    break;
            }
            txtNumberOfExistingDewaterStruct.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.InfrastructureDewatering.DewateringNoOfExistingStructure));
            if (txtNumberOfExistingDewaterStruct.Text == "") { txtNumberOfExistingDewaterStruct.Text = "0"; }
            txtMinimumDepth.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.InfrastructureDewatering.DewateringMinimumDepth));
            txtMaximumDepth.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.InfrastructureDewatering.DewateringMaximumDepth));

            //category = Convert.ToString(new NOCAP.BLL.Master.ApplicationTypeCategory((int)obj_infrastructureNewApplication.ApplicationTypeCode, (int)obj_infrastructureNewApplication.ApplicationTypeCategoryCode).ApplicationTypeCategoryDesc);
            //if (category == "Residential apartment" || category == "Group housing" || category == "Government water Supply agencies")
            //{
            //    trmaximumDeptProposed.Visible = false;
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
            //else
            //{
            txtmaximumDeptProposed.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.InfrastructureDewatering.DewatringMaximumProposedDepth));

            //}
            txtProQtyOfGroundWaterDewaterdDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.InfrastructureDewatering.DewaterProQtyGroundWaterDewaterdDay));
            txtProQtyOfGroundWaterDewaterdYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.InfrastructureDewatering.DewaterProQtyGroundWaterDewaterdYear));
            txtProposedUtilizationPumpedwater.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.InfrastructureDewatering.ProposedUtilizationofPumpedWater));
            txtOtherInformation.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.InfrastructureDewatering.AnyOtherInformation));

            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure[] arr_infNewExistingDeWatering;
            arr_infNewExistingDeWatering = obj_infrastructureNewApplication.GetInfrastructureNewSADExistingDeWateringAbstractionStructureList();
            foreach (NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure test in arr_infNewExistingDeWatering) { lst_infNewExistingDeWatering.Add(test); }

            //if (arr_infNewExistingDeWatering.Count() > 0)
            //{
            gvdeWateringExisting.DataSource = lst_infNewExistingDeWatering;
            gvdeWateringExisting.DataBind();
            //}
            //else
            //{
            //    lst_infNewExistingDeWateringBlank.Add(obj_infNewExistingDeWateringBlank);
            //    gvdeWateringExisting.DataSource = lst_infNewExistingDeWateringBlank;
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
                //NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));

                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure obj_infNewExistingDeWateringBlankForGridToObj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure();
                List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure> lst_infNewExistingDeWateringNewList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure>();
                //if (gvdeWateringExisting.Rows.Count == Convert.ToInt32(txtNumberOfExistingDewaterStruct.Text == "" ? "0" : txtNumberOfExistingDewaterStruct.Text))
                //{
                //    lblMessage.Text = "You can Enter Only " + txtNumberOfExistingDewaterStruct.Text + " Structures.";
                //    lblMessage.ForeColor = System.Drawing.Color.Red;
                //    return;
                //}
                //else { lblMessage.Text = ""; }
                foreach (GridViewRow gvRow in gvdeWateringExisting.Rows)
                {
                    obj_infNewExistingDeWateringBlankForGridToObj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure();
                    obj_infNewExistingDeWateringBlankForGridToObj.ApplicationCode = Convert.ToInt64(gvdeWateringExisting.DataKeys[gvRow.RowIndex].Value.ToString());
                    int ss = gvRow.RowIndex;
                    if (Convert.ToInt64(gvdeWateringExisting.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                    {
                        Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                        if (lbl_SerialNumber.Text.Trim() == "")
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.SerialNumber = 0;
                        }
                        else
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                        }

                        Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");

                        if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                        }
                        else
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                        }
                        Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                        if (lbl_YearOfConstruction.Text.Trim() == "")
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.YearOfConstruction = null;
                        }
                        else
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                        }
                        Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                        if (lbl_DepthExist.Text.Trim() == "")
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.DepthExist = null;
                        }
                        else
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                        }
                        Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                        if (lbl_Diameter.Text.Trim() == "")
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.Diameter = null;
                        }
                        else
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                        }
                        Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                        if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.DepthBelowWaterLevel = null;
                        }
                        else
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                        }
                        Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                        if (lbl_Discharge.Text.Trim() == "")
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.Discharge = null;
                        }
                        else
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                        }
                        Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                        if (lbl_OperationalHousrDay.Text.Trim() == "")
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.OperationalHousrDay = null;
                        }
                        else
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                        }

                        Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                        if (lbl_OperationalDaysYear.Text.Trim() == "")
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.OperationalDaysYear = null;
                        }
                        else
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                        }
                        Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                        if (lbl_LiftingDeviceCode.Text.Trim() == "")
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.LiftingDeviceCode = null;
                        }
                        else
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                        }
                        Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                        if (lbl_PowerOfPump.Text.Trim() == "")
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.PowerOfPump = null;
                        }
                        else
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                        }

                        Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                        switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                        {
                            case "":
                                obj_infNewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                break;
                            case "Yes":
                                obj_infNewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                break;
                            case "No":
                                obj_infNewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                break;
                        }
                        Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                        switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                        {
                            case "":
                                obj_infNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                break;
                            case "Yes":
                                obj_infNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                break;
                            case "No":
                                obj_infNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                break;
                        }

                        Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                        if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                        }
                        else
                        {
                            obj_infNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                        }

                        lst_infNewExistingDeWateringNewList.Add(obj_infNewExistingDeWateringBlankForGridToObj);


                    }
                }
                //---------------
                obj_infNewExistingDeWateringBlankForGridToObj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure();
                obj_infNewExistingDeWateringBlankForGridToObj.ApplicationCode = Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text);

                if (NOCAPExternalUtility.IsNumeric(ddlTypeOfStructure.SelectedValue)) { obj_infNewExistingDeWateringBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(ddlTypeOfStructure.SelectedValue); } else { return; }

                if (txtYearOfConstruction.Text.Trim() == "")
                {
                    obj_infNewExistingDeWateringBlankForGridToObj.YearOfConstruction = null;
                }
                else
                {
                    obj_infNewExistingDeWateringBlankForGridToObj.YearOfConstruction = Convert.ToInt32(txtYearOfConstruction.Text.Trim());
                }
                if (txtDepthMeter.Text.Trim() == "")
                {
                    obj_infNewExistingDeWateringBlankForGridToObj.DepthExist = null;
                }
                else
                {
                    obj_infNewExistingDeWateringBlankForGridToObj.DepthExist = Convert.ToDecimal(txtDepthMeter.Text.Trim());
                }
                if (txtDiameterMM.Text.Trim() == "")
                {
                    obj_infNewExistingDeWateringBlankForGridToObj.Diameter = null;
                }
                else
                {
                    obj_infNewExistingDeWateringBlankForGridToObj.Diameter = Convert.ToInt32(txtDiameterMM.Text.Trim());
                }

                
                if (txtDepthWaterBelowGroundLevel.Text.Trim() == "")
                {
                    obj_infNewExistingDeWateringBlankForGridToObj.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_infNewExistingDeWateringBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(txtDepthWaterBelowGroundLevel.Text.Trim());
                }
                if (txtDischarge.Text.Trim() == "")
                {
                    obj_infNewExistingDeWateringBlankForGridToObj.Discharge = null;
                }
                else
                {
                    obj_infNewExistingDeWateringBlankForGridToObj.Discharge = Convert.ToDecimal(txtDischarge.Text.Trim());
                }
                if (txtOperationalHoursDay.Text.Trim() == "")
                {
                    obj_infNewExistingDeWateringBlankForGridToObj.OperationalHousrDay = null;
                }
                else
                {
                    obj_infNewExistingDeWateringBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(txtOperationalHoursDay.Text.Trim());
                }

                if (txtOperationalDaysYear.Text.Trim() == "")
                {
                    obj_infNewExistingDeWateringBlankForGridToObj.OperationalDaysYear = null;
                }
                else
                {
                    obj_infNewExistingDeWateringBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(txtOperationalDaysYear.Text.Trim());
                }

                if (ddlModeOfLift.SelectedValue == "")
                {
                    obj_infNewExistingDeWateringBlankForGridToObj.LiftingDeviceCode = null;
                }
                else
                {
                    if (NOCAPExternalUtility.IsNumeric(ddlModeOfLift.SelectedValue)) { obj_infNewExistingDeWateringBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(ddlModeOfLift.SelectedValue); }
                }
                if (txtHorsePowerPump.Text.Trim() == "")
                {
                    obj_infNewExistingDeWateringBlankForGridToObj.PowerOfPump = null;
                }
                else
                {
                    obj_infNewExistingDeWateringBlankForGridToObj.PowerOfPump = Convert.ToDecimal(txtHorsePowerPump.Text.Trim());
                }

                switch (ddlWhetherFittedWithWaterMeter.SelectedValue)
                {
                    case "":
                        obj_infNewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_infNewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_infNewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }

                switch (ddlWhetherPermissionRegisteredWithCGWA.SelectedValue)
                {
                    case "":
                        obj_infNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_infNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_infNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (txtWhetherPermissionRegisteredWithCGWADet.Text.Trim() == "")
                {
                    obj_infNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_infNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = Convert.ToString(txtWhetherPermissionRegisteredWithCGWADet.Text.Trim());
                }

                lst_infNewExistingDeWateringNewList.Add(obj_infNewExistingDeWateringBlankForGridToObj);

                gvdeWateringExisting.DataSource = lst_infNewExistingDeWateringNewList;
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
    protected void gvdeWateringExisting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure obj_infNewExistingDGWAS = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure();
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
                    obj_infNewExistingDGWAS = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure(Convert.ToInt64(gvdeWateringExisting.DataKeys[e.Row.RowIndex].Values[0].ToString()), Convert.ToInt32(lbl_SerialNumber.Text));
                    lbl_TypeOfAbstractionStructureName.Text = HttpUtility.HtmlEncode(obj_infNewExistingDGWAS.GetTypeOfAbstractionStructure().TypeOfAbstractionStructureDesc);
                    if (obj_infNewExistingDGWAS.GetLiftingDevice() != null)
                    {
                        lbl_LiftingDeviceName.Text = HttpUtility.HtmlEncode(obj_infNewExistingDGWAS.GetLiftingDevice().LiftingDeviceDesc);
                        lbl_LiftingDeviceCode.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infNewExistingDGWAS.GetLiftingDevice().LiftingDeviceCode));
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
                if (UpdateIndNewExistingDeWateringDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text)) == 1)
                {
                    Server.Transfer("~/ExternalUser/InfrastructureNew/De-WateringProposedStructure.aspx");
                }
                else
                {
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
                UpdateIndNewExistingDeWateringDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
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
            Server.Transfer("~/ExternalUser/InfrastructureNew/WaterRequirementDetails.aspx");
        }
    }

    private int UpdateIndNewExistingDeWateringDetails(long lngA_ApplicationCode)
    {
        try
        {
            strActionName = "UpdateExistingDewateringDetails";
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

            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure> lst_infNewExistingDeWateringList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure>();
            //if (gvdeWateringExisting.Rows.Count != Convert.ToInt32(txtNumberOfExistingDewaterStruct.Text == "" ? "0" : txtNumberOfExistingDewaterStruct.Text))
            //{
            //    lblMessage.Text = "Please Enter " + txtNumberOfExistingDewaterStruct.Text + " Structures.";
            //    lblMessage.ForeColor = System.Drawing.Color.Red;
            //    return 0;
            //}
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

                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure obj_infNewExistingDeWatering = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure();

                obj_infNewExistingDeWatering.ApplicationCode = Convert.ToInt32(gvdeWateringExisting.DataKeys[gvRow.RowIndex].Value.ToString());

                if (lbl_TypeOfAbstractionStructureCode.Text == "")
                {
                    obj_infNewExistingDeWatering.TypeOfAbstractionStructureCode = 0;
                }
                else
                {
                    obj_infNewExistingDeWatering.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text);
                }
                if (lbl_YearOfConstruction.Text.Trim() == "")
                {
                    obj_infNewExistingDeWatering.YearOfConstruction = null;
                }
                else
                {
                    obj_infNewExistingDeWatering.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text);
                }

                if (lbl_DepthExist.Text.Trim() == "")
                {
                    obj_infNewExistingDeWatering.DepthExist = null;
                }
                else
                {
                    obj_infNewExistingDeWatering.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                }
                if (lbl_Diameter.Text.Trim() == "")
                {
                    obj_infNewExistingDeWatering.Diameter = null;
                }
                else
                {
                    obj_infNewExistingDeWatering.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                }
                if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                {
                    obj_infNewExistingDeWatering.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_infNewExistingDeWatering.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                }
                if (lbl_Discharge.Text.Trim() == "")
                {
                    obj_infNewExistingDeWatering.Discharge = null;
                }
                else
                {
                    obj_infNewExistingDeWatering.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                }
                if (lbl_OperationalHousrDay.Text.Trim() == "")
                {
                    obj_infNewExistingDeWatering.OperationalHousrDay = null;
                }
                else
                {
                    obj_infNewExistingDeWatering.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                }
                if (lbl_OperationalDaysYear.Text.Trim() == "")
                {
                    obj_infNewExistingDeWatering.OperationalDaysYear = null;
                }
                else
                {
                    obj_infNewExistingDeWatering.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                }
                if (lbl_LiftingDeviceCode.Text.Trim() == "")
                {
                    obj_infNewExistingDeWatering.LiftingDeviceCode = null;
                }
                else
                {
                    obj_infNewExistingDeWatering.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                }
                if (lbl_PowerOfPump.Text.Trim() == "")
                {
                    obj_infNewExistingDeWatering.PowerOfPump = null;
                }
                else
                {
                    obj_infNewExistingDeWatering.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                }
                switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                {
                    case "":
                        obj_infNewExistingDeWatering.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_infNewExistingDeWatering.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_infNewExistingDeWatering.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }
                switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                {
                    case "":
                        obj_infNewExistingDeWatering.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_infNewExistingDeWatering.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_infNewExistingDeWatering.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                {
                    obj_infNewExistingDeWatering.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_infNewExistingDeWatering.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                }

                obj_infNewExistingDeWatering.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;

                lst_infNewExistingDeWateringList.Add(obj_infNewExistingDeWatering);
            }

            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure[] arr_tempInfDeWateringExistingListBLL = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure[lst_infNewExistingDeWateringList.Count];
            lst_infNewExistingDeWateringList.CopyTo(arr_tempInfDeWateringExistingListBLL);
            obj_infrastructureNewApplication.InfrastructureNewSADExistingDeWateringAbstractionStructureList = arr_tempInfDeWateringExistingListBLL;

            switch (ddlDewateringRequired.SelectedValue)
            {
                case "Yes":
                    obj_infrastructureNewApplication.InfrastructureDewatering.DewateringRequired = NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADDewatering.deWateringRequired.Yes;
                    break;
                case "No":
                    obj_infrastructureNewApplication.InfrastructureDewatering.DewateringRequired = NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADDewatering.deWateringRequired.No;
                    break;
            }

            if (txtMinimumDepth.Text == "") { obj_infrastructureNewApplication.InfrastructureDewatering.DewateringMinimumDepth = null; }
            else { obj_infrastructureNewApplication.InfrastructureDewatering.DewateringMinimumDepth = Convert.ToDecimal(txtMinimumDepth.Text.Trim()); }

            if (txtMaximumDepth.Text == "") { obj_infrastructureNewApplication.InfrastructureDewatering.DewateringMaximumDepth = null; }
            else { obj_infrastructureNewApplication.InfrastructureDewatering.DewateringMaximumDepth = Convert.ToDecimal(txtMaximumDepth.Text.Trim()); }

            
            if (txtmaximumDeptProposed.Text == "") { obj_infrastructureNewApplication.InfrastructureDewatering.DewatringMaximumProposedDepth = null; }
            else { obj_infrastructureNewApplication.InfrastructureDewatering.DewatringMaximumProposedDepth = Convert.ToDecimal(txtmaximumDeptProposed.Text.Trim()); }

            if (txtProQtyOfGroundWaterDewaterdDay.Text == "") { obj_infrastructureNewApplication.InfrastructureDewatering.DewaterProQtyGroundWaterDewaterdDay = null; }
            else { obj_infrastructureNewApplication.InfrastructureDewatering.DewaterProQtyGroundWaterDewaterdDay = Convert.ToDecimal(txtProQtyOfGroundWaterDewaterdDay.Text.Trim()); }

            if (txtProQtyOfGroundWaterDewaterdYear.Text == "") { obj_infrastructureNewApplication.InfrastructureDewatering.DewaterProQtyGroundWaterDewaterdYear = null; }
            else { obj_infrastructureNewApplication.InfrastructureDewatering.DewaterProQtyGroundWaterDewaterdYear = Convert.ToDecimal(txtProQtyOfGroundWaterDewaterdYear.Text.Trim()); }

            if (txtProposedUtilizationPumpedwater.Text == "") { obj_infrastructureNewApplication.InfrastructureDewatering.ProposedUtilizationofPumpedWater = null; }
            else { obj_infrastructureNewApplication.InfrastructureDewatering.ProposedUtilizationofPumpedWater = Convert.ToString(txtProposedUtilizationPumpedwater.Text.Trim()); }

            if (txtOtherInformation.Text == "") { obj_infrastructureNewApplication.InfrastructureDewatering.AnyOtherInformation = null; }
            else { obj_infrastructureNewApplication.InfrastructureDewatering.AnyOtherInformation = Convert.ToString(txtOtherInformation.Text.Trim()); }

            if (txtNumberOfExistingDewaterStruct.Text.Trim() == "") { obj_infrastructureNewApplication.InfrastructureDewatering.DewateringNoOfExistingStructure = null; }
            else { obj_infrastructureNewApplication.InfrastructureDewatering.DewateringNoOfExistingStructure = Convert.ToInt32(txtNumberOfExistingDewaterStruct.Text.Trim()); }

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
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure obj_infNewExistingDeWateringBlankForGridToObj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure();
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure> lst_infNewExistingDeWateringNewList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure>();


                    foreach (GridViewRow gvRow in gvdeWateringExisting.Rows)
                    {
                        obj_infNewExistingDeWateringBlankForGridToObj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingDeWateringAbstractionStructure();
                        obj_infNewExistingDeWateringBlankForGridToObj.ApplicationCode = Convert.ToInt64(gvdeWateringExisting.DataKeys[gvRow.RowIndex].Value.ToString());
                        if (Convert.ToInt64(gvdeWateringExisting.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                        {
                            Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                            if (lbl_SerialNumber.Text.Trim() == "")
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.SerialNumber = 0;
                            }
                            else
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                            }
                            Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");
                            if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                            }
                            else
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                            }
                            Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                            if (lbl_YearOfConstruction.Text.Trim() == "")
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.YearOfConstruction = null;
                            }
                            else
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                            }
                            Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                            if (lbl_DepthExist.Text.Trim() == "")
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.DepthExist = null;
                            }
                            else
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                            }
                            Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                            if (lbl_Diameter.Text.Trim() == "")
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.Diameter = null;
                            }
                            else
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                            }
                            Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                            if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.DepthBelowWaterLevel = null;
                            }
                            else
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                            }
                            Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                            if (lbl_Discharge.Text.Trim() == "")
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.Discharge = null;
                            }
                            else
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                            }
                            Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                            if (lbl_OperationalHousrDay.Text.Trim() == "")
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.OperationalHousrDay = null;
                            }
                            else
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                            }

                            Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                            if (lbl_OperationalDaysYear.Text.Trim() == "")
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.OperationalDaysYear = null;
                            }
                            else
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                            }
                            Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                            if (lbl_LiftingDeviceCode.Text.Trim() == "")
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.LiftingDeviceCode = null;
                            }
                            else
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                            }
                            Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                            if (lbl_PowerOfPump.Text.Trim() == "")
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.PowerOfPump = null;
                            }
                            else
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                            }

                            Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                            switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                            {
                                case "":
                                    obj_infNewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_infNewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                    break;
                                case "No":
                                    obj_infNewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                    break;
                            }
                            Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                            switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                            {
                                case "":
                                    obj_infNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_infNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                    break;
                                case "No":
                                    obj_infNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                    break;
                            }

                            Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                            if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                            }
                            else
                            {
                                obj_infNewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                            }

                            lst_infNewExistingDeWateringNewList.Add(obj_infNewExistingDeWateringBlankForGridToObj);


                        }
                    }
                    lst_infNewExistingDeWateringNewList.RemoveAt(lst_IntIndex);
                    gvdeWateringExisting.DataSource = lst_infNewExistingDeWateringNewList;
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
                txtMaximumDepth.Text = "";
                rfvtxtMinimumDepth.Enabled = false;
                txtMinimumDepth.Text = "";
                rfvtxtMaximumDepth.Enabled = false;
                txtmaximumDeptProposed.Text = "";
                rfvtxtmaximumDeptProposed.Enabled = false;
                txtProposedUtilizationPumpedwater.Text = "";
                rfvtxtProposedUtilizationPumpedwater.Enabled = false;
                txtProQtyOfGroundWaterDewaterdYear.Text = "";
                txtOtherInformation.Text = "";
                rfvtxtOtherInformation.Enabled = false;
                ClearDeWateringExistingScreen();
                //gvdeWateringExisting.DataSource = null;
                //gvdeWateringExisting.DataBind();
                txtNumberOfExistingDewaterStruct.Text = "0";
                txtProQtyOfGroundWaterDewaterdDay.Text = "";

                EnableDisableAll(false);
            }
            else if (ddlDewateringRequired.SelectedValue == "Yes")
            {
                rfvtxtMinimumDepth.Enabled = true;
                rfvtxtMaximumDepth.Enabled = true;
                rfvtxtmaximumDeptProposed.Enabled = true;
                rfvtxtProposedUtilizationPumpedwater.Enabled = true;
                rfvtxtOtherInformation.Enabled = true;
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
        txtMinimumDepth.Enabled = Status;
        txtMaximumDepth.Enabled = Status;
        txtmaximumDeptProposed.Enabled = Status;
        txtProposedUtilizationPumpedwater.Enabled = Status;
        txtOtherInformation.Enabled = Status;
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
        txtProQtyOfGroundWaterDewaterdDay.Enabled = Status;
        rfvtxtProQtyOfGroundWaterDewaterdDay.Enabled = Status;
        txtProQtyOfGroundWaterDewaterdYear.Enabled = Status;
        rfvtxtProQtyOfGroundWaterDewaterdYear.Enabled = Status;
    }

}