using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_IndustrialRenew_DetailsExistingGroundwaterAbstractionStructure : System.Web.UI.Page
{
    string strPageName = "INDRenewExistingGroundWaterAbstractionStructure";
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
            try
            {

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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblIndustialApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                        }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    BindIGVIndRenewExistingGWASDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
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
    private void BindIGVIndRenewExistingGWASDetails(long lngA_ApplicationCode)
    {
        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);
        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure obj_indRenewExistingGWASBlank = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure();
        List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure> lst_indRenewExistingGWASBlank = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure>();
        List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure> lst_indRenewExistingGWAS = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure>();
        try
        {
            lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplication.NameOfIndustry);

            txtNumbeOfExistingGAS.Text = Convert.ToString(obj_industrialRenewApplication.NumberOfStructureExisting);
            if (txtNumbeOfExistingGAS.Text == "") { txtNumbeOfExistingGAS.Text = "0"; }
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure[] arr_indRenewExistingGWAS;
            arr_indRenewExistingGWAS = obj_industrialRenewApplication.GetIndustrialRenewExistingGroundWaterAbstractionStructureList();

            foreach (NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure test in arr_indRenewExistingGWAS)
            {
                lst_indRenewExistingGWAS.Add(test);
            }
            gvGASExisting.DataSource = lst_indRenewExistingGWAS;
            gvGASExisting.DataBind();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
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
            Server.Transfer("~/ExternalUser/IndustrialRenew/RecycledWaterUsage.aspx");
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
                    UpdateIndRenewExistingGWASDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
                }
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
                if (UpdateIndRenewExistingGWASDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text)) == 1)
                {
                    Server.Transfer("~/ExternalUser/IndustrialRenew/DetailsAdditionalGroundwaterAbstractionStructure.aspx");
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
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
                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure obj_indRenewExistingGWASBlankForGridToObj = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure();
                List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure> lst_indRenewExistingGWASNewList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure>();

                foreach (GridViewRow gvRow in gvGASExisting.Rows)
                {
                    obj_indRenewExistingGWASBlankForGridToObj = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure();
                    obj_indRenewExistingGWASBlankForGridToObj.IndustrialRenewApplicationCode = Convert.ToInt64(gvGASExisting.DataKeys[gvRow.RowIndex].Value.ToString());

                    if (Convert.ToInt64(gvGASExisting.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                    {
                        Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                        if (lbl_SerialNumber.Text.Trim() == "")
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.SerialNumber = 0;
                        }
                        else
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                        }
                        Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");

                        if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                        }
                        else
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                        }
                        Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                        if (lbl_YearOfConstruction.Text.Trim() == "")
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.YearOfConstruction = null;
                        }
                        else
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                        }
                        Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                        if (lbl_DepthExist.Text.Trim() == "")
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.DepthExist = null;
                        }
                        else
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                        }
                        Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                        if (lbl_Diameter.Text.Trim() == "")
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.Diameter = null;
                        }
                        else
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                        }
                        Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                        if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                        }
                        else
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                        }
                        Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                        if (lbl_Discharge.Text.Trim() == "")
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.Discharge = null;
                        }
                        else
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                        }
                        Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                        if (lbl_OperationalHousrDay.Text.Trim() == "")
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.OperationalHousrDay = null;
                        }
                        else
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                        }

                        Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                        if (lbl_OperationalDaysYear.Text.Trim() == "")
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.OperationalDaysYear = null;
                        }
                        else
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                        }
                        Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                        if (lbl_LiftingDeviceCode.Text.Trim() == "")
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.LiftingDeviceCode = null;
                        }
                        else
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                        }
                        Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                        if (lbl_PowerOfPump.Text.Trim() == "")
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.PowerOfPump = null;
                        }
                        else
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                        }

                        Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                        switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                        {
                            case "":
                                obj_indRenewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                break;
                            case "Yes":
                                obj_indRenewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                break;
                            case "No":
                                obj_indRenewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;
                                break;
                        }
                        Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                        switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                        {
                            case "":
                                obj_indRenewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                break;
                            case "Yes":
                                obj_indRenewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                break;
                            case "No":
                                obj_indRenewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                break;
                        }

                        Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                        if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                        }
                        else
                        {
                            obj_indRenewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                        }

                        lst_indRenewExistingGWASNewList.Add(obj_indRenewExistingGWASBlankForGridToObj);
                    }
                }
                //---------------
                obj_indRenewExistingGWASBlankForGridToObj = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure();
                obj_indRenewExistingGWASBlankForGridToObj.IndustrialRenewApplicationCode = Convert.ToInt64(lblIndustialApplicationCodeFrom.Text);

                obj_indRenewExistingGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(ddlTypeOfStructure.SelectedValue);

                if (txtYearOfConstruction.Text.Trim() == "")
                {
                    obj_indRenewExistingGWASBlankForGridToObj.YearOfConstruction = null;
                }
                else
                {
                    obj_indRenewExistingGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt32(txtYearOfConstruction.Text.Trim());
                }
                if (txtDepthMeter.Text.Trim() == "")
                {
                    obj_indRenewExistingGWASBlankForGridToObj.DepthExist = null;
                }
                else
                {
                    obj_indRenewExistingGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(txtDepthMeter.Text.Trim());
                }
                if (txtDiameterMM.Text.Trim() == "")
                {
                    obj_indRenewExistingGWASBlankForGridToObj.Diameter = null;
                }
                else
                {
                    obj_indRenewExistingGWASBlankForGridToObj.Diameter = Convert.ToInt32(txtDiameterMM.Text.Trim());
                }

                if (txtDepthWaterBelowGroundLevel.Text.Trim() == "")
                {
                    obj_indRenewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_indRenewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(txtDepthWaterBelowGroundLevel.Text.Trim());
                }
                if (txtDischarge.Text.Trim() == "")
                {
                    obj_indRenewExistingGWASBlankForGridToObj.Discharge = null;
                }
                else
                {
                    obj_indRenewExistingGWASBlankForGridToObj.Discharge = Convert.ToDecimal(txtDischarge.Text.Trim());
                }
                if (txtOperationalHoursDay.Text.Trim() == "")
                {
                    obj_indRenewExistingGWASBlankForGridToObj.OperationalHousrDay = null;
                }
                else
                {
                    obj_indRenewExistingGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(txtOperationalHoursDay.Text.Trim());
                }

                if (txtOperationalDaysYear.Text.Trim() == "")
                {
                    obj_indRenewExistingGWASBlankForGridToObj.OperationalDaysYear = null;
                }
                else
                {
                    obj_indRenewExistingGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(txtOperationalDaysYear.Text.Trim());
                }

                if (ddlModeOfLift.SelectedValue == "")
                {
                    obj_indRenewExistingGWASBlankForGridToObj.LiftingDeviceCode = null;
                }
                else
                {
                    obj_indRenewExistingGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(ddlModeOfLift.SelectedValue);
                }
                if (txtHorsePowerPump.Text.Trim() == "")
                {
                    obj_indRenewExistingGWASBlankForGridToObj.PowerOfPump = null;
                }
                else
                {
                    obj_indRenewExistingGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(txtHorsePowerPump.Text.Trim());
                }

                switch (ddlWhetherFittedWithWaterMeter.SelectedValue)
                {
                    case "":
                        obj_indRenewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_indRenewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_indRenewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }

                switch (ddlWhetherPermissionRegisteredWithCGWA.SelectedValue)
                {
                    case "":
                        obj_indRenewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_indRenewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_indRenewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (txtWhetherPermissionRegisteredWithCGWADet.Text.Trim() == "")
                {
                    obj_indRenewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_indRenewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = Convert.ToString(txtWhetherPermissionRegisteredWithCGWADet.Text.Trim());
                }
                lst_indRenewExistingGWASNewList.Add(obj_indRenewExistingGWASBlankForGridToObj);

                gvGASExisting.DataSource = lst_indRenewExistingGWASNewList;
                gvGASExisting.DataBind();

                ClearGASExistingScreen();
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void gvGASExisting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure obj_indRenewExistingGWAS = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure();

            Label lbl_TypeOfAbstractionStructureCode = null;
            Label lbl_TypeOfAbstractionStructureName = null;
            Label lbl_LiftingDeviceCode = null;
            Label lbl_LiftingDeviceName = null;
            Label lbl_SerialNumber = null;

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

                    if (lbl_LiftingDeviceCode.Text == "")
                    {
                        lbl_LiftingDeviceName.Text = "";
                    }
                    else
                    {
                        lbl_LiftingDeviceName = (Label)e.Row.FindControl("LiftingDeviceName");
                        NOCAP.BLL.Master.LiftingDevice obj_liftingDevice = new NOCAP.BLL.Master.LiftingDevice(Convert.ToInt32(lbl_LiftingDeviceCode.Text));
                        lbl_LiftingDeviceName.Text = HttpUtility.HtmlEncode(obj_liftingDevice.LiftingDeviceDesc);
                    }
                }
                else
                {
                    obj_indRenewExistingGWAS = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure(Convert.ToInt64(gvGASExisting.DataKeys[e.Row.RowIndex].Values[0].ToString()), Convert.ToInt32(lbl_SerialNumber.Text));
                    lbl_TypeOfAbstractionStructureName.Text = HttpUtility.HtmlEncode(obj_indRenewExistingGWAS.GetTypeOfAbstractionStructure().TypeOfAbstractionStructureDesc);
                    if (obj_indRenewExistingGWAS.GetLiftingDevice() != null)
                    {
                        lbl_LiftingDeviceName.Text = HttpUtility.HtmlEncode(obj_indRenewExistingGWAS.GetLiftingDevice().LiftingDeviceDesc);
                        lbl_LiftingDeviceCode.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_indRenewExistingGWAS.GetLiftingDevice().LiftingDeviceCode));
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
    }
    private int UpdateIndRenewExistingGWASDetails(long lngA_ApplicationCode)
    {
        try
        {
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
            if (Convert.ToInt32(txtNumbeOfExistingGAS.Text.Trim()) != gvGASExisting.Rows.Count)
            {
                lblMessage.Text = "Number of Records in \"Detail of Structures\" should be equal to the \"Number of Existing Structures\" given above.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            strActionName = "UpdateExistingGroundWaterAbstractionStructure";
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);

            List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure> lst_indRenewExistingGWASList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure>();

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

                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure obj_indRenewExistingGWAS = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure();

                obj_indRenewExistingGWAS.IndustrialRenewApplicationCode = Convert.ToInt32(gvGASExisting.DataKeys[gvRow.RowIndex].Value.ToString());

                if (lbl_TypeOfAbstractionStructureCode.Text == "")
                {
                    obj_indRenewExistingGWAS.TypeOfAbstractionStructureCode = 0;
                }
                else
                {
                    obj_indRenewExistingGWAS.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text);
                }
                if (lbl_YearOfConstruction.Text.Trim() == "")
                {
                    obj_indRenewExistingGWAS.YearOfConstruction = null;
                }
                else
                {
                    obj_indRenewExistingGWAS.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                }
                if (lbl_DepthExist.Text.Trim() == "")
                {
                    obj_indRenewExistingGWAS.DepthExist = null;
                }
                else
                {
                    obj_indRenewExistingGWAS.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                }
                if (lbl_Diameter.Text.Trim() == "")
                {
                    obj_indRenewExistingGWAS.Diameter = null;
                }
                else
                {
                    obj_indRenewExistingGWAS.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                }
                if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                {
                    obj_indRenewExistingGWAS.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_indRenewExistingGWAS.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                }
                if (lbl_Discharge.Text.Trim() == "")
                {
                    obj_indRenewExistingGWAS.Discharge = null;
                }
                else
                {
                    obj_indRenewExistingGWAS.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                }
                if (lbl_OperationalHousrDay.Text.Trim() == "")
                {
                    obj_indRenewExistingGWAS.OperationalHousrDay = null;
                }
                else
                {
                    obj_indRenewExistingGWAS.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                }
                if (lbl_OperationalDaysYear.Text.Trim() == "")
                {
                    obj_indRenewExistingGWAS.OperationalDaysYear = null;
                }
                else
                {
                    obj_indRenewExistingGWAS.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                }
                if (lbl_LiftingDeviceCode.Text.Trim() == "")
                {
                    obj_indRenewExistingGWAS.LiftingDeviceCode = null;
                }
                else
                {
                    obj_indRenewExistingGWAS.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                }
                if (lbl_PowerOfPump.Text.Trim() == "")
                {
                    obj_indRenewExistingGWAS.PowerOfPump = null;
                }
                else
                {
                    obj_indRenewExistingGWAS.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                }
                switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                {
                    case "":
                        obj_indRenewExistingGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_indRenewExistingGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_indRenewExistingGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }
                switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                {
                    case "":
                        obj_indRenewExistingGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_indRenewExistingGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_indRenewExistingGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                {
                    obj_indRenewExistingGWAS.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_indRenewExistingGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                }
                obj_indRenewExistingGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;

                lst_indRenewExistingGWASList.Add(obj_indRenewExistingGWAS);
            }
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure[] arr_tempIndGwasExistingListBLL = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure[lst_indRenewExistingGWASList.Count];
            lst_indRenewExistingGWASList.CopyTo(arr_tempIndGwasExistingListBLL);
            obj_industrialRenewApplication.IndustrialRenewExistingGroundWaterAbstractionStructureList = arr_tempIndGwasExistingListBLL;

            if (txtNumbeOfExistingGAS.Text.Trim() == "")
            {
                obj_industrialRenewApplication.NumberOfStructureExisting = null;
            }
            else
            {
                obj_industrialRenewApplication.NumberOfStructureExisting = Convert.ToInt32(txtNumbeOfExistingGAS.Text.Trim());
            }
            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_industrialRenewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_industrialRenewApplication.Update() == 1)
            {
                strStatus = "Update Success";
                lblMessage.Text = "Successfully Saved";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                strStatus = "Update Failed";
                lblMessage.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplication.CustumMessage);
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
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure obj_indRenewExistingGWASBlankForGridToObj = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure();
                    List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure> lst_indRenewExistingGWASNewList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure>();

                    foreach (GridViewRow gvRow in gvGASExisting.Rows)
                    {
                        obj_indRenewExistingGWASBlankForGridToObj = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADExistingGroundWaterAbstractionStructure();
                        obj_indRenewExistingGWASBlankForGridToObj.IndustrialRenewApplicationCode = Convert.ToInt64(gvGASExisting.DataKeys[gvRow.RowIndex].Value.ToString());
                        if (Convert.ToInt64(gvGASExisting.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                        {
                            Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                            if (lbl_SerialNumber.Text.Trim() == "")
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.SerialNumber = 0;
                            }
                            else
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                            }
                            Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");
                            if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                            }
                            else
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                            }
                            Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                            if (lbl_YearOfConstruction.Text.Trim() == "")
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.YearOfConstruction = null;
                            }
                            else
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                            }
                            Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                            if (lbl_DepthExist.Text.Trim() == "")
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.DepthExist = null;
                            }
                            else
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                            }
                            Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                            if (lbl_Diameter.Text.Trim() == "")
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.Diameter = null;
                            }
                            else
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                            }
                            Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                            if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                            }
                            else
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                            }
                            Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                            if (lbl_Discharge.Text.Trim() == "")
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.Discharge = null;
                            }
                            else
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                            }
                            Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                            if (lbl_OperationalHousrDay.Text.Trim() == "")
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.OperationalHousrDay = null;
                            }
                            else
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                            }

                            Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                            if (lbl_OperationalDaysYear.Text.Trim() == "")
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.OperationalDaysYear = null;
                            }
                            else
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                            }
                            Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                            if (lbl_LiftingDeviceCode.Text.Trim() == "")
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.LiftingDeviceCode = null;
                            }
                            else
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                            }
                            Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                            if (lbl_PowerOfPump.Text.Trim() == "")
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.PowerOfPump = null;
                            }
                            else
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                            }

                            Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                            switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                            {
                                case "":
                                    obj_indRenewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_indRenewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                    break;
                                case "No":
                                    obj_indRenewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                    break;
                            }
                            Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                            switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                            {
                                case "":
                                    obj_indRenewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_indRenewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                    break;
                                case "No":
                                    obj_indRenewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                    break;
                            }

                            Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                            if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                            }
                            else
                            {
                                obj_indRenewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                            }

                            lst_indRenewExistingGWASNewList.Add(obj_indRenewExistingGWASBlankForGridToObj);


                        }
                    }
                    lst_indRenewExistingGWASNewList.RemoveAt(lst_IntIndex);
                    gvGASExisting.DataSource = lst_indRenewExistingGWASNewList;
                    gvGASExisting.DataBind();
                }
            }

            catch (Exception)
            {
                //lblMessage.Text = ex.Message;
                //lblMessage.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    private void ClearGASExistingScreen()
    {
        try
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
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
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
                    txtWhetherPermissionRegisteredWithCGWADet.Enabled = false;
                }
                else
                {
                    txtWhetherPermissionRegisteredWithCGWADet.Enabled = true;
                }

            }
            catch (Exception)
            {
                //lblMessage.Text = ex.Message;
                //lblMessage.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
}