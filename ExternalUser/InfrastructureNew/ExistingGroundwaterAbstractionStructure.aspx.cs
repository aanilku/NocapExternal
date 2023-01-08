using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_InfrastructureNew_ExistingGroundwaterAbstractionStructure : System.Web.UI.Page
{
    string strPageName = "INFGroundWaterExistringStructure";
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
                if (lblModeFrom.Text.Trim() == "Edit") { BindIGVInfNewExistingGWASDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text)); }
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
        revtxtNumbeOfExistingGAS.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtNumbeOfExistingGAS.ErrorMessage = ValidationUtility.txtValForNumericMsg;

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

    private void BindIGVInfNewExistingGWASDetails(long lngA_ApplicationCode)
    {
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure obj_infNewExistingGWASBlank = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure();
        List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure> lst_infNewExistingGWASBlank = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure>();
        List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure> lst_infNewExistingGWAS = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure>();
        try
        {
            txtNumbeOfExistingGAS.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.NumberOfStructureExisting));
            if (txtNumbeOfExistingGAS.Text == "") { txtNumbeOfExistingGAS.Text = "0"; }

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
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure[] arr_infNewExistingGWAS;
            arr_infNewExistingGWAS = obj_infrastructureNewApplication.GetInfrastructureNewSADExistingGroundWaterAbstractionStructureeList();
            foreach (NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure test in arr_infNewExistingGWAS) { lst_infNewExistingGWAS.Add(test); }
            //if (arr_infNewExistingGWAS.Count() > 0)
            //{
            gvGASExisting.DataSource = lst_infNewExistingGWAS;
            gvGASExisting.DataBind();
            //}
            //else
            //{
            //    lst_infNewExistingGWASBlank.Add(obj_infNewExistingGWASBlank);
            //    gvGASExisting.DataSource = lst_infNewExistingGWASBlank;
            //    gvGASExisting.DataBind();
            //    int int_NoOfCol = 0;
            //    int_NoOfCol = gvGASExisting.Rows[0].Cells.Count;
            //    gvGASExisting.Rows[0].Cells.Clear();
            //    gvGASExisting.Rows[0].Cells.Add(new TableCell());
            //    gvGASExisting.Rows[0].Cells[0].ColumnSpan = int_NoOfCol;
            //    gvGASExisting.Rows[0].Cells[0].Text = "No Records exsist in GAS Existing";
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
            if (ddlWhetherPermissionRegisteredWithCGWA.SelectedValue == "Yes")
            {
                txtWhetherPermissionRegisteredWithCGWADet.Enabled = true;
            }
            else
            {
                txtWhetherPermissionRegisteredWithCGWADet.Enabled = false;
                txtWhetherPermissionRegisteredWithCGWADet.Text = "";
            }
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
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure obj_infNewExistingGWASBlankForGridToObj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure();
                List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure> lst_infNewExistingGWASNewList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure>();
                //if (gvGASExisting.Rows.Count == Convert.ToInt32(txtNumbeOfExistingGAS.Text))
                //{
                //    lblMessage.Text = "You can Enter Only " + txtNumbeOfExistingGAS.Text + " Structures.";
                //    lblMessage.ForeColor = System.Drawing.Color.Red;
                //    return;
                //}
                //else { lblMessage.Text = ""; }
                foreach (GridViewRow gvRow in gvGASExisting.Rows)
                {
                    obj_infNewExistingGWASBlankForGridToObj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure();
                    obj_infNewExistingGWASBlankForGridToObj.ApplicationCode = Convert.ToInt64(gvGASExisting.DataKeys[gvRow.RowIndex].Value.ToString());
                    if (Convert.ToInt64(gvGASExisting.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                    {
                        Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                        if (lbl_SerialNumber.Text.Trim() == "")
                        {
                            obj_infNewExistingGWASBlankForGridToObj.SerialNumber = 0;
                        }
                        else
                        {
                            obj_infNewExistingGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                        }

                        Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");

                        if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                        {
                            obj_infNewExistingGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                        }
                        else
                        {
                            obj_infNewExistingGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                        }
                        Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                        if (lbl_YearOfConstruction.Text.Trim() == "")
                        {
                            obj_infNewExistingGWASBlankForGridToObj.YearOfConstruction = null;
                        }
                        else
                        {
                            obj_infNewExistingGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                        }
                        Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                        if (lbl_DepthExist.Text.Trim() == "")
                        {
                            obj_infNewExistingGWASBlankForGridToObj.DepthExist = null;
                        }
                        else
                        {
                            obj_infNewExistingGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                        }
                        Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                        if (lbl_Diameter.Text.Trim() == "")
                        {
                            obj_infNewExistingGWASBlankForGridToObj.Diameter = null;
                        }
                        else
                        {
                            obj_infNewExistingGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                        }
                        Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                        if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                        {
                            obj_infNewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                        }
                        else
                        {
                            obj_infNewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                        }
                        Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                        if (lbl_Discharge.Text.Trim() == "")
                        {
                            obj_infNewExistingGWASBlankForGridToObj.Discharge = null;
                        }
                        else
                        {
                            obj_infNewExistingGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                        }
                        Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                        if (lbl_OperationalHousrDay.Text.Trim() == "")
                        {
                            obj_infNewExistingGWASBlankForGridToObj.OperationalHousrDay = null;
                        }
                        else
                        {
                            obj_infNewExistingGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                        }

                        Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                        if (lbl_OperationalDaysYear.Text.Trim() == "")
                        {
                            obj_infNewExistingGWASBlankForGridToObj.OperationalDaysYear = null;
                        }
                        else
                        {
                            obj_infNewExistingGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                        }
                        Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                        if (lbl_LiftingDeviceCode.Text.Trim() == "")
                        {
                            obj_infNewExistingGWASBlankForGridToObj.LiftingDeviceCode = null;
                        }
                        else
                        {
                            obj_infNewExistingGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                        }
                        Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                        if (lbl_PowerOfPump.Text.Trim() == "")
                        {
                            obj_infNewExistingGWASBlankForGridToObj.PowerOfPump = null;
                        }
                        else
                        {
                            obj_infNewExistingGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                        }

                        Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                        switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                        {
                            case "":
                                obj_infNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                break;
                            case "Yes":
                                obj_infNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                break;
                            case "No":
                                obj_infNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                break;
                        }
                        Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                        switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                        {
                            case "":
                                obj_infNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                break;
                            case "Yes":
                                obj_infNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                break;
                            case "No":
                                obj_infNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                break;
                        }

                        Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                        if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                        {
                            obj_infNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                        }
                        else
                        {
                            obj_infNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                        }

                        lst_infNewExistingGWASNewList.Add(obj_infNewExistingGWASBlankForGridToObj);
                    }
                }
                //---------------
                obj_infNewExistingGWASBlankForGridToObj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure();
                obj_infNewExistingGWASBlankForGridToObj.ApplicationCode = Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text);

                if (NOCAPExternalUtility.IsNumeric(ddlTypeOfStructure.SelectedValue)) { obj_infNewExistingGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(ddlTypeOfStructure.SelectedValue); }
                else { return; }

                if (txtYearOfConstruction.Text.Trim() == "")
                {
                    obj_infNewExistingGWASBlankForGridToObj.YearOfConstruction = null;
                }
                else
                {
                    obj_infNewExistingGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt32(txtYearOfConstruction.Text.Trim());
                }
                if (txtDepthMeter.Text.Trim() == "")
                {
                    obj_infNewExistingGWASBlankForGridToObj.DepthExist = null;
                }
                else
                {
                    obj_infNewExistingGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(txtDepthMeter.Text.Trim());
                }
                if (txtDiameterMM.Text.Trim() == "")
                {
                    obj_infNewExistingGWASBlankForGridToObj.Diameter = null;
                }
                else
                {
                    obj_infNewExistingGWASBlankForGridToObj.Diameter = Convert.ToInt32(txtDiameterMM.Text.Trim());
                }

                if (txtDepthWaterBelowGroundLevel.Text.Trim() == "")
                {
                    obj_infNewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_infNewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(txtDepthWaterBelowGroundLevel.Text.Trim());
                }
                if (txtDischarge.Text.Trim() == "")
                {
                    obj_infNewExistingGWASBlankForGridToObj.Discharge = null;
                }
                else
                {
                    obj_infNewExistingGWASBlankForGridToObj.Discharge = Convert.ToDecimal(txtDischarge.Text.Trim());
                }
                if (txtOperationalHoursDay.Text.Trim() == "")
                {
                    obj_infNewExistingGWASBlankForGridToObj.OperationalHousrDay = null;
                }
                else
                {
                    obj_infNewExistingGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(txtOperationalHoursDay.Text.Trim());
                }

                if (txtOperationalDaysYear.Text.Trim() == "")
                {
                    obj_infNewExistingGWASBlankForGridToObj.OperationalDaysYear = null;
                }
                else
                {
                    obj_infNewExistingGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(txtOperationalDaysYear.Text.Trim());
                }

                if (ddlModeOfLift.SelectedValue == "")
                {
                    obj_infNewExistingGWASBlankForGridToObj.LiftingDeviceCode = null;
                }
                else
                {
                    if (NOCAPExternalUtility.IsNumeric(ddlModeOfLift.SelectedValue)) { obj_infNewExistingGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(ddlModeOfLift.SelectedValue); }
                }
                if (txtHorsePowerPump.Text.Trim() == "")
                {
                    obj_infNewExistingGWASBlankForGridToObj.PowerOfPump = null;
                }
                else
                {
                    obj_infNewExistingGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(txtHorsePowerPump.Text.Trim());
                }

                switch (ddlWhetherFittedWithWaterMeter.SelectedValue)
                {
                    case "":
                        obj_infNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_infNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_infNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }

                switch (ddlWhetherPermissionRegisteredWithCGWA.SelectedValue)
                {
                    case "":
                        obj_infNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_infNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_infNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (txtWhetherPermissionRegisteredWithCGWADet.Text.Trim() == "")
                {
                    obj_infNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_infNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = Convert.ToString(txtWhetherPermissionRegisteredWithCGWADet.Text.Trim());
                }
                lst_infNewExistingGWASNewList.Add(obj_infNewExistingGWASBlankForGridToObj);
                gvGASExisting.DataSource = lst_infNewExistingGWASNewList;
                gvGASExisting.DataBind();
                ClearGASExistingScreen();
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

    private void ClearGASExistingScreen()
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

    protected void gvGASExisting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure obj_infNewExistingGWAS = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure();
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
                    lbl_TypeOfAbstractionStructureName.Text =  HttpUtility.HtmlEncode(obj_typeOfAbstractionStructureCode.TypeOfAbstractionStructureDesc);
                    if (lbl_LiftingDeviceCode.Text == "") { lbl_LiftingDeviceName.Text = ""; }
                    else
                    {
                        lbl_LiftingDeviceName = (Label)e.Row.FindControl("LiftingDeviceName");
                        NOCAP.BLL.Master.LiftingDevice obj_liftingDevice = new NOCAP.BLL.Master.LiftingDevice(Convert.ToInt32(lbl_LiftingDeviceCode.Text));
                        lbl_LiftingDeviceName.Text =  HttpUtility.HtmlEncode(obj_liftingDevice.LiftingDeviceDesc);
                    }
                }
                else
                {
                    obj_infNewExistingGWAS = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure(Convert.ToInt64(gvGASExisting.DataKeys[e.Row.RowIndex].Values[0].ToString()), Convert.ToInt32(lbl_SerialNumber.Text));
                    lbl_TypeOfAbstractionStructureName.Text =  HttpUtility.HtmlEncode(obj_infNewExistingGWAS.GetTypeOfAbstractionStructure().TypeOfAbstractionStructureDesc);
                    if (obj_infNewExistingGWAS.GetLiftingDevice() != null)
                    {
                        lbl_LiftingDeviceName.Text =  HttpUtility.HtmlEncode(obj_infNewExistingGWAS.GetLiftingDevice().LiftingDeviceDesc);
                        lbl_LiftingDeviceCode.Text =  HttpUtility.HtmlEncode(Convert.ToString(obj_infNewExistingGWAS.GetLiftingDevice().LiftingDeviceCode));
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
    protected void gvGASExisting_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure obj_infNewExistingGWASBlankForGridToObj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure();
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure> lst_infNewExistingGWASNewList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure>();
                    foreach (GridViewRow gvRow in gvGASExisting.Rows)
                    {
                        obj_infNewExistingGWASBlankForGridToObj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure();
                        obj_infNewExistingGWASBlankForGridToObj.ApplicationCode = Convert.ToInt64(gvGASExisting.DataKeys[gvRow.RowIndex].Value.ToString());
                        if (Convert.ToInt64(gvGASExisting.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                        {
                            Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                            if (lbl_SerialNumber.Text.Trim() == "")
                            {
                                obj_infNewExistingGWASBlankForGridToObj.SerialNumber = 0;
                            }
                            else
                            {
                                obj_infNewExistingGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                            }
                            Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");
                            if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                            {
                                obj_infNewExistingGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                            }
                            else
                            {
                                obj_infNewExistingGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                            }
                            Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                            if (lbl_YearOfConstruction.Text.Trim() == "")
                            {
                                obj_infNewExistingGWASBlankForGridToObj.YearOfConstruction = null;
                            }
                            else
                            {
                                obj_infNewExistingGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                            }
                            Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                            if (lbl_DepthExist.Text.Trim() == "")
                            {
                                obj_infNewExistingGWASBlankForGridToObj.DepthExist = null;
                            }
                            else
                            {
                                obj_infNewExistingGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                            }
                            Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                            if (lbl_Diameter.Text.Trim() == "")
                            {
                                obj_infNewExistingGWASBlankForGridToObj.Diameter = null;
                            }
                            else
                            {
                                obj_infNewExistingGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                            }
                            Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                            if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                            {
                                obj_infNewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                            }
                            else
                            {
                                obj_infNewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                            }
                            Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                            if (lbl_Discharge.Text.Trim() == "")
                            {
                                obj_infNewExistingGWASBlankForGridToObj.Discharge = null;
                            }
                            else
                            {
                                obj_infNewExistingGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                            }
                            Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                            if (lbl_OperationalHousrDay.Text.Trim() == "")
                            {
                                obj_infNewExistingGWASBlankForGridToObj.OperationalHousrDay = null;
                            }
                            else
                            {
                                obj_infNewExistingGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                            }

                            Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                            if (lbl_OperationalDaysYear.Text.Trim() == "")
                            {
                                obj_infNewExistingGWASBlankForGridToObj.OperationalDaysYear = null;
                            }
                            else
                            {
                                obj_infNewExistingGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                            }
                            Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                            if (lbl_LiftingDeviceCode.Text.Trim() == "")
                            {
                                obj_infNewExistingGWASBlankForGridToObj.LiftingDeviceCode = null;
                            }
                            else
                            {
                                obj_infNewExistingGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                            }
                            Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                            if (lbl_PowerOfPump.Text.Trim() == "")
                            {
                                obj_infNewExistingGWASBlankForGridToObj.PowerOfPump = null;
                            }
                            else
                            {
                                obj_infNewExistingGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                            }

                            Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                            switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                            {
                                case "":
                                    obj_infNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_infNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                    break;
                                case "No":
                                    obj_infNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                    break;
                            }
                            Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                            switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                            {
                                case "":
                                    obj_infNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_infNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                    break;
                                case "No":
                                    obj_infNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                    break;
                            }

                            Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                            if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                            {
                                obj_infNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                            }
                            else
                            {
                                obj_infNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                            }

                            lst_infNewExistingGWASNewList.Add(obj_infNewExistingGWASBlankForGridToObj);


                        }
                    }
                    lst_infNewExistingGWASNewList.RemoveAt(lst_IntIndex);
                    gvGASExisting.DataSource = lst_infNewExistingGWASNewList;
                    gvGASExisting.DataBind();

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
                //Server.Transfer("~/ExternalUser/InfrastructureNew/WaterSupplyDetail.aspx");
                Server.Transfer("~/ExternalUser/InfrastructureNew/De-WateringProposedStructure.aspx"); 
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
                UpdateInfNewExistingGWASDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
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
                if (UpdateInfNewExistingGWASDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text)) == 1)
                {
                    Server.Transfer("~/ExternalUser/InfrastructureNew/ProposedGroundwaterAbstractionStructure.aspx");
                }
                else { }
            }
        }
    }
    private int UpdateInfNewExistingGWASDetails(long lngA_ApplicationCode)
    {
        try
        {
            strActionName = "UpdateGroundWaterExistingDetails";
            if (txtNumbeOfExistingGAS.Text.Trim() == "")
            {
                lblMessage.Text = "Number of Existing Structures can not be Blank";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            if (!NOCAPExternalUtility.IsNumeric(txtNumbeOfExistingGAS.Text))
            {
                lblMessage.Text = "Number of Existing Structures allows Numeric";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            if (Convert.ToInt32(txtNumbeOfExistingGAS.Text) != gvGASExisting.Rows.Count)
            {
                lblMessage.Text = "Number of Records of Detailed Structures should be equal to the Number of Existing Structures";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure> lst_infNewExistingGWASList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure>();
            //if (gvGASExisting.Rows.Count != Convert.ToInt32(txtNumbeOfExistingGAS.Text))
            //{
            //    lblMessage.Text = "Please Enter " + txtNumbeOfExistingGAS.Text + " Structures.";
            //    lblMessage.ForeColor = System.Drawing.Color.Red;
            //    return 0;
            //}
            foreach (GridViewRow gvRow in gvGASExisting.Rows)
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
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure obj_infNewExistingGWAS = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure();
                obj_infNewExistingGWAS.ApplicationCode = Convert.ToInt32(gvGASExisting.DataKeys[gvRow.RowIndex].Value.ToString());

                if (lbl_TypeOfAbstractionStructureCode.Text == "")
                {
                    obj_infNewExistingGWAS.TypeOfAbstractionStructureCode = 0;
                }
                else
                {
                    obj_infNewExistingGWAS.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text);
                }
                if (lbl_YearOfConstruction.Text.Trim() == "")
                {
                    obj_infNewExistingGWAS.YearOfConstruction = null;
                }
                else
                {
                    obj_infNewExistingGWAS.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                }
                if (lbl_DepthExist.Text.Trim() == "")
                {
                    obj_infNewExistingGWAS.DepthExist = null;
                }
                else
                {
                    obj_infNewExistingGWAS.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                }
                if (lbl_Diameter.Text.Trim() == "")
                {
                    obj_infNewExistingGWAS.Diameter = null;
                }
                else
                {
                    obj_infNewExistingGWAS.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                }
                if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                {
                    obj_infNewExistingGWAS.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_infNewExistingGWAS.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                }
                if (lbl_Discharge.Text.Trim() == "")
                {
                    obj_infNewExistingGWAS.Discharge = null;
                }
                else
                {
                    obj_infNewExistingGWAS.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                }
                if (lbl_OperationalHousrDay.Text.Trim() == "")
                {
                    obj_infNewExistingGWAS.OperationalHousrDay = null;
                }
                else
                {
                    obj_infNewExistingGWAS.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                }
                if (lbl_OperationalDaysYear.Text.Trim() == "")
                {
                    obj_infNewExistingGWAS.OperationalDaysYear = null;
                }
                else
                {
                    obj_infNewExistingGWAS.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                }
                if (lbl_LiftingDeviceCode.Text.Trim() == "")
                {
                    obj_infNewExistingGWAS.LiftingDeviceCode = null;
                }
                else
                {
                    obj_infNewExistingGWAS.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                }
                if (lbl_PowerOfPump.Text.Trim() == "")
                {
                    obj_infNewExistingGWAS.PowerOfPump = null;
                }
                else
                {
                    obj_infNewExistingGWAS.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                }
                switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                {
                    case "":
                        obj_infNewExistingGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_infNewExistingGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_infNewExistingGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }
                switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                {
                    case "":
                        obj_infNewExistingGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_infNewExistingGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_infNewExistingGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                {
                    obj_infNewExistingGWAS.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_infNewExistingGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                }

                obj_infNewExistingGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;

                lst_infNewExistingGWASList.Add(obj_infNewExistingGWAS);
            }



            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure[] arr_tempInfGwasExistingListBLL = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructure[lst_infNewExistingGWASList.Count];
            lst_infNewExistingGWASList.CopyTo(arr_tempInfGwasExistingListBLL);
            obj_infrastructureNewApplication.InfrastructureNewSADExistingGroundWaterAbstractionStructureList = arr_tempInfGwasExistingListBLL;
            if (txtNumbeOfExistingGAS.Text.Trim() == "")
            {
                obj_infrastructureNewApplication.NumberOfStructureExisting = null;
            }
            else
            {
                obj_infrastructureNewApplication.NumberOfStructureExisting = Convert.ToInt32(txtNumbeOfExistingGAS.Text.Trim());
            }

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
                strStatus = "Save Successfull";
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