using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_InfrastructureNew_ProposedGroundwaterAbstractionStructure : System.Web.UI.Page
{
    string strPageName = "INFGroundWaterProposedStructure";
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
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    BindIGVInfNewProposedGWASDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
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

    private void ValidationExpInit()
    {
        revtxtNumbeOfProposedGAS.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtNumbeOfProposedGAS.ErrorMessage = ValidationUtility.txtValForNumericMsg;

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

    private void BindIGVInfNewProposedGWASDetails(long lngA_ApplicationCode)
    {
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure obj_infNewProposedGWASBlank = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure();
        List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure> lst_infNewProposedGWASBlank = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure>();
        List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure> lst_infNewProposedGWAS = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure>();

        try
        {
            txtNumbeOfProposedGAS.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.NumberOfStructureProposed));
            if (txtNumbeOfProposedGAS.Text == "") { txtNumbeOfProposedGAS.Text = "0"; }

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
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure[] arr_infNewProposedGWAS;
            arr_infNewProposedGWAS = obj_infrastructureNewApplication.GetInfrastructureNewSADProposedGroundWaterAbstractionStructureList();
            foreach (NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure test in arr_infNewProposedGWAS)
            {
                lst_infNewProposedGWAS.Add(test);
            }

            //if (arr_infNewProposedGWAS.Count() > 0)
            //{
            gvGASProposed.DataSource = lst_infNewProposedGWAS;
            gvGASProposed.DataBind();
            //}
            //else
            //{

            //    lst_infNewProposedGWAS.Add(obj_infNewProposedGWASBlank);
            //    gvGASProposed.DataSource = lst_infNewProposedGWAS;
            //    gvGASProposed.DataBind();
            //    int int_NoOfCol = 0;
            //    int_NoOfCol = gvGASProposed.Rows[0].Cells.Count;
            //    gvGASProposed.Rows[0].Cells.Clear();
            //    gvGASProposed.Rows[0].Cells.Add(new TableCell());
            //    gvGASProposed.Rows[0].Cells[0].ColumnSpan = int_NoOfCol;
            //    gvGASProposed.Rows[0].Cells[0].Text = "No Records exsist in GAS Proposed";

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
            if (ddlWhetherPermissionRegisteredWithCGWA.SelectedValue == "Yes") { txtWhetherPermissionRegisteredWithCGWADet.Enabled = true; }
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
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure obj_infNewProposedGWASBlankForGridToObj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure();
                List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure> lst_infNewProposedGWASNewList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure>();
                //if (gvGASProposed.Rows.Count == Convert.ToInt32(txtNumbeOfProposedGAS.Text))
                //{
                //    lblMessage.Text = "You can Enter Only " + txtNumbeOfProposedGAS.Text + " Structures.";
                //    lblMessage.ForeColor = System.Drawing.Color.Red;
                //    return;
                //}
                //else { lblMessage.Text = ""; }
                foreach (GridViewRow gvRow in gvGASProposed.Rows)
                {
                    obj_infNewProposedGWASBlankForGridToObj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure();
                    obj_infNewProposedGWASBlankForGridToObj.ApplicationCode = Convert.ToInt64(gvGASProposed.DataKeys[gvRow.RowIndex].Value.ToString());
                    if (Convert.ToInt64(gvGASProposed.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                    {
                        Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                        if (lbl_SerialNumber.Text.Trim() == "")
                        {
                            obj_infNewProposedGWASBlankForGridToObj.SerialNumber = 0;
                        }
                        else
                        {
                            obj_infNewProposedGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                        }

                        Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");

                        if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                        {
                            obj_infNewProposedGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                        }
                        else
                        {
                            obj_infNewProposedGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                        }
                        Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                        if (lbl_YearOfConstruction.Text.Trim() == "")
                        {
                            obj_infNewProposedGWASBlankForGridToObj.YearOfConstruction = null;
                        }
                        else
                        {
                            obj_infNewProposedGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                        }
                        Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                        if (lbl_DepthExist.Text.Trim() == "")
                        {
                            obj_infNewProposedGWASBlankForGridToObj.DepthExist = null;
                        }
                        else
                        {
                            obj_infNewProposedGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                        }
                        Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                        if (lbl_Diameter.Text.Trim() == "")
                        {
                            obj_infNewProposedGWASBlankForGridToObj.Diameter = null;
                        }
                        else
                        {
                            obj_infNewProposedGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                        }
                        Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                        if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                        {
                            obj_infNewProposedGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                        }
                        else
                        {
                            obj_infNewProposedGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                        }
                        Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                        if (lbl_Discharge.Text.Trim() == "")
                        {
                            obj_infNewProposedGWASBlankForGridToObj.Discharge = null;
                        }
                        else
                        {
                            obj_infNewProposedGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                        }
                        Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                        if (lbl_OperationalHousrDay.Text.Trim() == "")
                        {
                            obj_infNewProposedGWASBlankForGridToObj.OperationalHousrDay = null;
                        }
                        else
                        {
                            obj_infNewProposedGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                        }

                        Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                        if (lbl_OperationalDaysYear.Text.Trim() == "")
                        {
                            obj_infNewProposedGWASBlankForGridToObj.OperationalDaysYear = null;
                        }
                        else
                        {
                            obj_infNewProposedGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                        }
                        Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                        if (lbl_LiftingDeviceCode.Text.Trim() == "")
                        {
                            obj_infNewProposedGWASBlankForGridToObj.LiftingDeviceCode = null;
                        }
                        else
                        {
                            obj_infNewProposedGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                        }
                        Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                        if (lbl_PowerOfPump.Text.Trim() == "")
                        {
                            obj_infNewProposedGWASBlankForGridToObj.PowerOfPump = null;
                        }
                        else
                        {
                            obj_infNewProposedGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                        }

                        Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                        switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                        {
                            case "":
                                obj_infNewProposedGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                break;
                            case "Yes":
                                obj_infNewProposedGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                break;
                            case "No":
                                obj_infNewProposedGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                break;
                        }
                        Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                        switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                        {
                            case "":
                                obj_infNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                break;
                            case "Yes":
                                obj_infNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                break;
                            case "No":
                                obj_infNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                break;
                        }

                        Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                        if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                        {
                            obj_infNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                        }
                        else
                        {
                            obj_infNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                        }

                        lst_infNewProposedGWASNewList.Add(obj_infNewProposedGWASBlankForGridToObj);


                    }
                }
                //---------------
                obj_infNewProposedGWASBlankForGridToObj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure();
                obj_infNewProposedGWASBlankForGridToObj.ApplicationCode = Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text);
                if (NOCAPExternalUtility.IsNumeric(ddlTypeOfStructure.SelectedValue)) { obj_infNewProposedGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(ddlTypeOfStructure.SelectedValue); }
                else { return; }
                if (txtYearOfConstruction.Text.Trim() == "")
                {
                    obj_infNewProposedGWASBlankForGridToObj.YearOfConstruction = null;
                }
                else
                {
                    obj_infNewProposedGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt32(txtYearOfConstruction.Text.Trim());
                }
                if (txtDepthMeter.Text.Trim() == "")
                {
                    obj_infNewProposedGWASBlankForGridToObj.DepthExist = null;
                }
                else
                {
                    obj_infNewProposedGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(txtDepthMeter.Text.Trim());
                }
                if (txtDiameterMM.Text.Trim() == "")
                {
                    obj_infNewProposedGWASBlankForGridToObj.Diameter = null;
                }
                else
                {
                    obj_infNewProposedGWASBlankForGridToObj.Diameter = Convert.ToInt32(txtDiameterMM.Text.Trim());
                }

                if (txtDepthWaterBelowGroundLevel.Text.Trim() == "")
                {
                    obj_infNewProposedGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_infNewProposedGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(txtDepthWaterBelowGroundLevel.Text.Trim());
                }
                if (txtDischarge.Text.Trim() == "")
                {
                    obj_infNewProposedGWASBlankForGridToObj.Discharge = null;
                }
                else
                {
                    obj_infNewProposedGWASBlankForGridToObj.Discharge = Convert.ToDecimal(txtDischarge.Text.Trim());
                }
                if (txtOperationalHoursDay.Text.Trim() == "")
                {
                    obj_infNewProposedGWASBlankForGridToObj.OperationalHousrDay = null;
                }
                else
                {
                    obj_infNewProposedGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(txtOperationalHoursDay.Text.Trim());
                }

                if (txtOperationalDaysYear.Text.Trim() == "")
                {
                    obj_infNewProposedGWASBlankForGridToObj.OperationalDaysYear = null;
                }
                else
                {
                    obj_infNewProposedGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(txtOperationalDaysYear.Text.Trim());
                }

                if (ddlModeOfLift.SelectedValue == "")
                {
                    obj_infNewProposedGWASBlankForGridToObj.LiftingDeviceCode = null;
                }
                else
                {
                    if (NOCAPExternalUtility.IsNumeric(ddlModeOfLift.SelectedValue)) { obj_infNewProposedGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(ddlModeOfLift.SelectedValue); }
                }
                if (txtHorsePowerPump.Text.Trim() == "")
                {
                    obj_infNewProposedGWASBlankForGridToObj.PowerOfPump = null;
                }
                else
                {
                    obj_infNewProposedGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(txtHorsePowerPump.Text.Trim());
                }

                switch (ddlWhetherFittedWithWaterMeter.SelectedValue)
                {
                    case "":
                        obj_infNewProposedGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_infNewProposedGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_infNewProposedGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }

                switch (ddlWhetherPermissionRegisteredWithCGWA.SelectedValue)
                {
                    case "":
                        obj_infNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_infNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_infNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (txtWhetherPermissionRegisteredWithCGWADet.Text.Trim() == "")
                {
                    obj_infNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_infNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = Convert.ToString(txtWhetherPermissionRegisteredWithCGWADet.Text.Trim());
                }
                lst_infNewProposedGWASNewList.Add(obj_infNewProposedGWASBlankForGridToObj);
                gvGASProposed.DataSource = lst_infNewProposedGWASNewList;
                gvGASProposed.DataBind();
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

    protected void gvGASProposed_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure obj_infNewProposedGWASBlankForGridToObj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure();
                    List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure> lst_infNewProposedGWASNewList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure>();
                    foreach (GridViewRow gvRow in gvGASProposed.Rows)
                    {
                        obj_infNewProposedGWASBlankForGridToObj = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure();
                        obj_infNewProposedGWASBlankForGridToObj.ApplicationCode = Convert.ToInt64(gvGASProposed.DataKeys[gvRow.RowIndex].Value.ToString());

                        if (Convert.ToInt64(gvGASProposed.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                        {
                            Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                            if (lbl_SerialNumber.Text.Trim() == "")
                            {
                                obj_infNewProposedGWASBlankForGridToObj.SerialNumber = 0;
                            }
                            else
                            {
                                obj_infNewProposedGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                            }
                            Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");
                            if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                            {
                                obj_infNewProposedGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                            }
                            else
                            {
                                obj_infNewProposedGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                            }
                            Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                            if (lbl_YearOfConstruction.Text.Trim() == "")
                            {
                                obj_infNewProposedGWASBlankForGridToObj.YearOfConstruction = null;
                            }
                            else
                            {
                                obj_infNewProposedGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                            }
                            Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                            if (lbl_DepthExist.Text.Trim() == "")
                            {
                                obj_infNewProposedGWASBlankForGridToObj.DepthExist = null;
                            }
                            else
                            {
                                obj_infNewProposedGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                            }
                            Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                            if (lbl_Diameter.Text.Trim() == "")
                            {
                                obj_infNewProposedGWASBlankForGridToObj.Diameter = null;
                            }
                            else
                            {
                                obj_infNewProposedGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                            }
                            Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                            if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                            {
                                obj_infNewProposedGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                            }
                            else
                            {
                                obj_infNewProposedGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                            }
                            Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                            if (lbl_Discharge.Text.Trim() == "")
                            {
                                obj_infNewProposedGWASBlankForGridToObj.Discharge = null;
                            }
                            else
                            {
                                obj_infNewProposedGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                            }
                            Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                            if (lbl_OperationalHousrDay.Text.Trim() == "")
                            {
                                obj_infNewProposedGWASBlankForGridToObj.OperationalHousrDay = null;
                            }
                            else
                            {
                                obj_infNewProposedGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                            }

                            Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                            if (lbl_OperationalDaysYear.Text.Trim() == "")
                            {
                                obj_infNewProposedGWASBlankForGridToObj.OperationalDaysYear = null;
                            }
                            else
                            {
                                obj_infNewProposedGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                            }
                            Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                            if (lbl_LiftingDeviceCode.Text.Trim() == "")
                            {
                                obj_infNewProposedGWASBlankForGridToObj.LiftingDeviceCode = null;
                            }
                            else
                            {
                                obj_infNewProposedGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                            }
                            Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                            if (lbl_PowerOfPump.Text.Trim() == "")
                            {
                                obj_infNewProposedGWASBlankForGridToObj.PowerOfPump = null;
                            }
                            else
                            {
                                obj_infNewProposedGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                            }

                            Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                            switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                            {
                                case "":
                                    obj_infNewProposedGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_infNewProposedGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                    break;
                                case "No":
                                    obj_infNewProposedGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                    break;
                            }
                            Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                            switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                            {
                                case "":
                                    obj_infNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_infNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                    break;
                                case "No":
                                    obj_infNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                    break;
                            }

                            Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                            if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                            {
                                obj_infNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                            }
                            else
                            {
                                obj_infNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                            }
                            lst_infNewProposedGWASNewList.Add(obj_infNewProposedGWASBlankForGridToObj);
                        }
                    }
                    lst_infNewProposedGWASNewList.RemoveAt(lst_IntIndex);
                    gvGASProposed.DataSource = lst_infNewProposedGWASNewList;
                    gvGASProposed.DataBind();
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
    protected void gvGASProposed_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure obj_infNewProposedGWAS = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure();
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
                    if (lbl_LiftingDeviceCode.Text == "")
                    {
                        lbl_LiftingDeviceName.Text = "";
                    }
                    else
                    {
                        lbl_LiftingDeviceName = (Label)e.Row.FindControl("LiftingDeviceName");
                        NOCAP.BLL.Master.LiftingDevice obj_liftingDevice = new NOCAP.BLL.Master.LiftingDevice(Convert.ToInt32(lbl_LiftingDeviceCode.Text));
                        lbl_LiftingDeviceName.Text =  HttpUtility.HtmlEncode(obj_liftingDevice.LiftingDeviceDesc);
                    }
                }
                else
                {
                    obj_infNewProposedGWAS = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure(Convert.ToInt64(gvGASProposed.DataKeys[e.Row.RowIndex].Values[0].ToString()), Convert.ToInt32(lbl_SerialNumber.Text));
                    lbl_TypeOfAbstractionStructureName.Text = HttpUtility.HtmlEncode(obj_infNewProposedGWAS.GetTypeOfAbstractionStructure().TypeOfAbstractionStructureDesc);
                    if (obj_infNewProposedGWAS.GetLiftingDevice() != null)
                    {
                        lbl_LiftingDeviceName.Text = HttpUtility.HtmlEncode(obj_infNewProposedGWAS.GetLiftingDevice().LiftingDeviceDesc);
                        lbl_LiftingDeviceCode.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infNewProposedGWAS.GetLiftingDevice().LiftingDeviceCode));
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
                Server.Transfer("~/ExternalUser/InfrastructureNew/ExistingGroundwaterAbstractionStructure.aspx");
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
                UpdateInfNewProposedGWASDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
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
                if (UpdateInfNewProposedGWASDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text)) == 1)
                {
                    //Server.Transfer("~/ExternalUser/InfrastructureNew/OtherDetails.aspx");
                    Server.Transfer("~/ExternalUser/InfrastructureNew/BreakUpOfWaterRequirment.aspx");
                }
                else
                {
                }
            }
        }
    }
    private int UpdateInfNewProposedGWASDetails(long lngA_ApplicationCode)
    {
        try
        {
            strActionName = "UpdateGroundWaterProposedDetails";
            if (txtNumbeOfProposedGAS.Text.Trim() == "")
            {
                lblMessage.Text = "Number of Proposed Structures can not be Blank";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            if (!NOCAPExternalUtility.IsNumeric(txtNumbeOfProposedGAS.Text))
            {
                lblMessage.Text = "Number of Proposed Structures allows Numeric";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            if (Convert.ToInt32(txtNumbeOfProposedGAS.Text) != gvGASProposed.Rows.Count)
            {
                lblMessage.Text = "Number of Records of Detailed Structures should be equal to the Number of Proposed Groundwater Abstraction Structures";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure> lst_infNewProposedGWASList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure>();
            //if (gvGASProposed.Rows.Count != Convert.ToInt32(txtNumbeOfProposedGAS.Text))
            //{
            //    lblMessage.Text = "Please Enter " + txtNumbeOfProposedGAS.Text + " Structures.";
            //    lblMessage.ForeColor = System.Drawing.Color.Red;
            //    return 0;
            //}
            foreach (GridViewRow gvRow in gvGASProposed.Rows)
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

                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure obj_infNewProposedGWAS = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure();
                obj_infNewProposedGWAS.ApplicationCode = Convert.ToInt32(gvGASProposed.DataKeys[gvRow.RowIndex].Value.ToString());

                if (lbl_TypeOfAbstractionStructureCode.Text == "")
                {
                    obj_infNewProposedGWAS.TypeOfAbstractionStructureCode = 0;
                }
                else
                {
                    obj_infNewProposedGWAS.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text);
                }
                if (lbl_YearOfConstruction.Text.Trim() == "")
                {
                    obj_infNewProposedGWAS.YearOfConstruction = null;
                }
                else
                {
                    obj_infNewProposedGWAS.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                }
                if (lbl_DepthExist.Text.Trim() == "")
                {
                    obj_infNewProposedGWAS.DepthExist = null;
                }
                else
                {
                    obj_infNewProposedGWAS.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                }
                if (lbl_Diameter.Text.Trim() == "")
                {
                    obj_infNewProposedGWAS.Diameter = null;
                }
                else
                {
                    obj_infNewProposedGWAS.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                }
                if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                {
                    obj_infNewProposedGWAS.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_infNewProposedGWAS.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                }
                if (lbl_Discharge.Text.Trim() == "")
                {
                    obj_infNewProposedGWAS.Discharge = null;
                }
                else
                {
                    obj_infNewProposedGWAS.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                }
                if (lbl_OperationalHousrDay.Text.Trim() == "")
                {
                    obj_infNewProposedGWAS.OperationalHousrDay = null;
                }
                else
                {
                    obj_infNewProposedGWAS.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                }
                if (lbl_OperationalDaysYear.Text.Trim() == "")
                {
                    obj_infNewProposedGWAS.OperationalDaysYear = null;
                }
                else
                {
                    obj_infNewProposedGWAS.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                }
                if (lbl_LiftingDeviceCode.Text.Trim() == "")
                {
                    obj_infNewProposedGWAS.LiftingDeviceCode = null;
                }
                else
                {
                    obj_infNewProposedGWAS.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                }
                if (lbl_PowerOfPump.Text.Trim() == "")
                {
                    obj_infNewProposedGWAS.PowerOfPump = null;
                }
                else
                {
                    obj_infNewProposedGWAS.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                }
                switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                {
                    case "":
                        obj_infNewProposedGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_infNewProposedGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_infNewProposedGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }
                switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                {
                    case "":
                        obj_infNewProposedGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_infNewProposedGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_infNewProposedGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                {
                    obj_infNewProposedGWAS.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_infNewProposedGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                }

                obj_infNewProposedGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;

                lst_infNewProposedGWASList.Add(obj_infNewProposedGWAS);
            }



            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure[] arr_tempInfGwasProposedListBLL = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructure[lst_infNewProposedGWASList.Count];
            lst_infNewProposedGWASList.CopyTo(arr_tempInfGwasProposedListBLL);
            obj_infrastructureNewApplication.InfrastructureNewSADProposedGroundWaterAbstractionStructureList = arr_tempInfGwasProposedListBLL;
            if (txtNumbeOfProposedGAS.Text.Trim() == "")
            {
                obj_infrastructureNewApplication.NumberOfStructureProposed = null;
            }
            else
            {
                obj_infrastructureNewApplication.NumberOfStructureProposed = Convert.ToInt32(txtNumbeOfProposedGAS.Text.Trim());
            }

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_infrastructureNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_infrastructureNewApplication.Update() == 1)
            {
                strStatus = "Update Successfull";
                lblMessage.Text = "Successfully Saved";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                strStatus = "Update Unsuccessfull";
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