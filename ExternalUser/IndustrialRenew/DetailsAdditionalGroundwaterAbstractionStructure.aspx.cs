using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.DAL.UserManagement;
using NOCAP.BLL.UserManagement;

public partial class ExternalUser_IndustrialRenew_DetailsAdditionalGroundwaterAbstractionStructure : System.Web.UI.Page
{
    string strPageName = "INDRenewAdditionalGroundWaterAbstractionStructure";
    string strActionName = "";
    string strStatus = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        if (!IsPostBack)
        {
            ValidationExpInit();
            try
            {
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

                    BindIGVIndRenewAdditionalGWASDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
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
        revtxtNumbeOfAdditionalGAS.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtNumbeOfAdditionalGAS.ErrorMessage = ValidationUtility.txtValForNumericMsg;

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

    private void BindIGVIndRenewAdditionalGWASDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure obj_indRenewAdditionalGWASBlank = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure();
            List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure> lst_indRenewAdditionalGWASBlank = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure>();
            List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure> lst_indRenewAdditionalGWAS = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure>();

            lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplication.NameOfIndustry);

            txtNumbeOfAdditionalGAS.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialRenewApplication.NumberOfStructureAdditional));
            if (txtNumbeOfAdditionalGAS.Text == "") { txtNumbeOfAdditionalGAS.Text = "0"; }
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure[] arr_indRenewAdditionalGWAS;
            arr_indRenewAdditionalGWAS = obj_industrialRenewApplication.GetIndustrialRenewAdditionalGroundWaterAbstractionStructureList();

            foreach (NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure test in arr_indRenewAdditionalGWAS)
            {
                lst_indRenewAdditionalGWAS.Add(test);
            }
            gvGASAdditional.DataSource = lst_indRenewAdditionalGWAS;
            gvGASAdditional.DataBind();
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
            Server.Transfer("DetailsExistingGroundwaterAbstractionStructure.aspx");
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
                    UpdateIndRenewAdditionalGWASDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
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
                if (UpdateIndRenewAdditionalGWASDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text)) == 1)
                {
                    Server.Transfer("ComplianceConditionNOC.aspx");
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



                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure obj_indRenewAdditionalGWASBlankForGridToObj = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure();
                List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure> lst_indRenewAdditionalGWASNewList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure>();


                foreach (GridViewRow gvRow in gvGASAdditional.Rows)
                {

                    obj_indRenewAdditionalGWASBlankForGridToObj = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure();

                    obj_indRenewAdditionalGWASBlankForGridToObj.IndustrialRenewApplicationCode = Convert.ToInt64(gvGASAdditional.DataKeys[gvRow.RowIndex].Value.ToString());

                    if (Convert.ToInt64(gvGASAdditional.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                    {
                        Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                        if (lbl_SerialNumber.Text.Trim() == "")
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.SerialNumber = 0;
                        }
                        else
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                        }

                        Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");

                        if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                        }
                        else
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                        }
                        Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                        if (lbl_YearOfConstruction.Text.Trim() == "")
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.YearOfConstruction = null;
                        }
                        else
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                        }
                        Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                        if (lbl_DepthExist.Text.Trim() == "")
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.DepthExist = null;
                        }
                        else
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                        }
                        Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                        if (lbl_Diameter.Text.Trim() == "")
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.Diameter = null;
                        }
                        else
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                        }
                        Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                        if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                        }
                        else
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                        }
                        Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                        if (lbl_Discharge.Text.Trim() == "")
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.Discharge = null;
                        }
                        else
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                        }
                        Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                        if (lbl_OperationalHousrDay.Text.Trim() == "")
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.OperationalHousrDay = null;
                        }
                        else
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                        }

                        Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                        if (lbl_OperationalDaysYear.Text.Trim() == "")
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.OperationalDaysYear = null;
                        }
                        else
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                        }
                        Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                        if (lbl_LiftingDeviceCode.Text.Trim() == "")
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.LiftingDeviceCode = null;
                        }
                        else
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                        }
                        Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                        if (lbl_PowerOfPump.Text.Trim() == "")
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.PowerOfPump = null;
                        }
                        else
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                        }

                        Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                        switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                        {
                            case "":
                                obj_indRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                break;
                            case "Yes":
                                obj_indRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                break;
                            case "No":
                                obj_indRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                break;
                        }
                        Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                        switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                        {
                            case "":
                                obj_indRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                break;
                            case "Yes":
                                obj_indRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                break;
                            case "No":
                                obj_indRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                break;
                        }

                        Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                        if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                        }
                        else
                        {
                            obj_indRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                        }

                        lst_indRenewAdditionalGWASNewList.Add(obj_indRenewAdditionalGWASBlankForGridToObj);


                    }
                }
                //---------------
                obj_indRenewAdditionalGWASBlankForGridToObj = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure();
                obj_indRenewAdditionalGWASBlankForGridToObj.IndustrialRenewApplicationCode = Convert.ToInt64(lblIndustialApplicationCodeFrom.Text);

                obj_indRenewAdditionalGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(ddlTypeOfStructure.SelectedValue);

                if (txtYearOfConstruction.Text.Trim() == "")
                {
                    obj_indRenewAdditionalGWASBlankForGridToObj.YearOfConstruction = null;
                }
                else
                {
                    obj_indRenewAdditionalGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt32(txtYearOfConstruction.Text.Trim());
                }
                if (txtDepthMeter.Text.Trim() == "")
                {
                    obj_indRenewAdditionalGWASBlankForGridToObj.DepthExist = null;
                }
                else
                {
                    obj_indRenewAdditionalGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(txtDepthMeter.Text.Trim());
                }
                if (txtDiameterMM.Text.Trim() == "")
                {
                    obj_indRenewAdditionalGWASBlankForGridToObj.Diameter = null;
                }
                else
                {
                    obj_indRenewAdditionalGWASBlankForGridToObj.Diameter = Convert.ToInt32(txtDiameterMM.Text.Trim());
                }

                if (txtDepthWaterBelowGroundLevel.Text.Trim() == "")
                {
                    obj_indRenewAdditionalGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_indRenewAdditionalGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(txtDepthWaterBelowGroundLevel.Text.Trim());
                }
                if (txtDischarge.Text.Trim() == "")
                {
                    obj_indRenewAdditionalGWASBlankForGridToObj.Discharge = null;
                }
                else
                {
                    obj_indRenewAdditionalGWASBlankForGridToObj.Discharge = Convert.ToDecimal(txtDischarge.Text.Trim());
                }
                if (txtOperationalHoursDay.Text.Trim() == "")
                {
                    obj_indRenewAdditionalGWASBlankForGridToObj.OperationalHousrDay = null;
                }
                else
                {
                    obj_indRenewAdditionalGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(txtOperationalHoursDay.Text.Trim());
                }

                if (txtOperationalDaysYear.Text.Trim() == "")
                {
                    obj_indRenewAdditionalGWASBlankForGridToObj.OperationalDaysYear = null;
                }
                else
                {
                    obj_indRenewAdditionalGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(txtOperationalDaysYear.Text.Trim());
                }

                if (ddlModeOfLift.SelectedValue == "")
                {
                    obj_indRenewAdditionalGWASBlankForGridToObj.LiftingDeviceCode = null;
                }
                else
                {
                    obj_indRenewAdditionalGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(ddlModeOfLift.SelectedValue);
                }
                if (txtHorsePowerPump.Text.Trim() == "")
                {
                    obj_indRenewAdditionalGWASBlankForGridToObj.PowerOfPump = null;
                }
                else
                {
                    obj_indRenewAdditionalGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(txtHorsePowerPump.Text.Trim());
                }

                switch (ddlWhetherFittedWithWaterMeter.SelectedValue)
                {
                    case "":
                        obj_indRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_indRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_indRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }

                switch (ddlWhetherPermissionRegisteredWithCGWA.SelectedValue)
                {
                    case "":
                        obj_indRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_indRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_indRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (txtWhetherPermissionRegisteredWithCGWADet.Text.Trim() == "")
                {
                    obj_indRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_indRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = Convert.ToString(txtWhetherPermissionRegisteredWithCGWADet.Text.Trim());
                }

                lst_indRenewAdditionalGWASNewList.Add(obj_indRenewAdditionalGWASBlankForGridToObj);

                gvGASAdditional.DataSource = lst_indRenewAdditionalGWASNewList;
                gvGASAdditional.DataBind();

                ClearGASAdditionalScreen();
            }

            catch (Exception)
            {
                //lblMessage.Text = ex.Message;
                //lblMessage.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void gvGASAdditional_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure obj_indRenewAdditionalGWAS = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure();

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
                    obj_indRenewAdditionalGWAS = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure(Convert.ToInt64(gvGASAdditional.DataKeys[e.Row.RowIndex].Values[0].ToString()), Convert.ToInt32(lbl_SerialNumber.Text));

                    lbl_TypeOfAbstractionStructureName.Text = HttpUtility.HtmlEncode(obj_indRenewAdditionalGWAS.GetTypeOfAbstractionStructure().TypeOfAbstractionStructureDesc);

                    if (obj_indRenewAdditionalGWAS.GetLiftingDevice() != null)
                    {

                        lbl_LiftingDeviceName.Text = HttpUtility.HtmlEncode(obj_indRenewAdditionalGWAS.GetLiftingDevice().LiftingDeviceDesc);

                        lbl_LiftingDeviceCode.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_indRenewAdditionalGWAS.GetLiftingDevice().LiftingDeviceCode));
                    }


                }

            }
            else
            {

            }
        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
            //lblMessage.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }

    private int UpdateIndRenewAdditionalGWASDetails(long lngA_ApplicationCode)
    {
        try
        {
            if (txtNumbeOfAdditionalGAS.Text.Trim() == "")
            {
                lblMessage.Text = "Number of Additional Structures can not be Blank";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            if (!NOCAPExternalUtility.IsNumeric(txtNumbeOfAdditionalGAS.Text))
            {
                lblMessage.Text = "Number of Additional Structures allows Numeric";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            if (Convert.ToInt32(txtNumbeOfAdditionalGAS.Text.Trim()) != gvGASAdditional.Rows.Count)
            {
                lblMessage.Text = "Number of Records in \"Detail of Structures\" should be equal to the \"Number of Additional Structures\" given above.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            strActionName = "UpdateAdditionalGroundWaterAbstractionStructure";
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication(lngA_ApplicationCode);

            List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure> lst_indRenewAdditionalGWASList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure>();

            foreach (GridViewRow gvRow in gvGASAdditional.Rows)
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

                NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure obj_indRenewAdditionalGWAS = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure();

                obj_indRenewAdditionalGWAS.IndustrialRenewApplicationCode = Convert.ToInt32(gvGASAdditional.DataKeys[gvRow.RowIndex].Value.ToString());

                if (lbl_TypeOfAbstractionStructureCode.Text == "")
                {
                    obj_indRenewAdditionalGWAS.TypeOfAbstractionStructureCode = 0;
                }
                else
                {
                    obj_indRenewAdditionalGWAS.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text);
                }
                if (lbl_YearOfConstruction.Text.Trim() == "")
                {
                    obj_indRenewAdditionalGWAS.YearOfConstruction = null;
                }
                else
                {
                    obj_indRenewAdditionalGWAS.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                }
                if (lbl_DepthExist.Text.Trim() == "")
                {
                    obj_indRenewAdditionalGWAS.DepthExist = null;
                }
                else
                {
                    obj_indRenewAdditionalGWAS.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                }
                if (lbl_Diameter.Text.Trim() == "")
                {
                    obj_indRenewAdditionalGWAS.Diameter = null;
                }
                else
                {
                    obj_indRenewAdditionalGWAS.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                }
                if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                {
                    obj_indRenewAdditionalGWAS.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_indRenewAdditionalGWAS.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                }
                if (lbl_Discharge.Text.Trim() == "")
                {
                    obj_indRenewAdditionalGWAS.Discharge = null;
                }
                else
                {
                    obj_indRenewAdditionalGWAS.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                }
                if (lbl_OperationalHousrDay.Text.Trim() == "")
                {
                    obj_indRenewAdditionalGWAS.OperationalHousrDay = null;
                }
                else
                {
                    obj_indRenewAdditionalGWAS.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                }
                if (lbl_OperationalDaysYear.Text.Trim() == "")
                {
                    obj_indRenewAdditionalGWAS.OperationalDaysYear = null;
                }
                else
                {
                    obj_indRenewAdditionalGWAS.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                }
                if (lbl_LiftingDeviceCode.Text.Trim() == "")
                {
                    obj_indRenewAdditionalGWAS.LiftingDeviceCode = null;
                }
                else
                {
                    obj_indRenewAdditionalGWAS.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                }
                if (lbl_PowerOfPump.Text.Trim() == "")
                {
                    obj_indRenewAdditionalGWAS.PowerOfPump = null;
                }
                else
                {
                    obj_indRenewAdditionalGWAS.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                }
                switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                {
                    case "":
                        obj_indRenewAdditionalGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_indRenewAdditionalGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_indRenewAdditionalGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }
                switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                {
                    case "":
                        obj_indRenewAdditionalGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_indRenewAdditionalGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_indRenewAdditionalGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                {
                    obj_indRenewAdditionalGWAS.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_indRenewAdditionalGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                }

                obj_indRenewAdditionalGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;

                lst_indRenewAdditionalGWASList.Add(obj_indRenewAdditionalGWAS);
            }



            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure[] arr_tempIndGwasAdditionalListBLL = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure[lst_indRenewAdditionalGWASList.Count];
            lst_indRenewAdditionalGWASList.CopyTo(arr_tempIndGwasAdditionalListBLL);
            

            obj_industrialRenewApplication.IndustrialRenewAdditionalGroundWaterAbstractionStructureList = arr_tempIndGwasAdditionalListBLL;

            if (txtNumbeOfAdditionalGAS.Text.Trim() == "")
            {
                obj_industrialRenewApplication.NumberOfStructureAdditional = null;
            }
            else
            {
                obj_industrialRenewApplication.NumberOfStructureAdditional = Convert.ToInt32(txtNumbeOfAdditionalGAS.Text.Trim());
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
                lblMessage.Text = obj_industrialRenewApplication.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }

        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
            //lblMessage.ForeColor = System.Drawing.Color.Red;
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

    protected void gvGASAdditional_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure obj_indRenewAdditionalGWASBlankForGridToObj = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure();
                    List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure> lst_indRenewAdditionalGWASNewList = new List<NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure>();


                    foreach (GridViewRow gvRow in gvGASAdditional.Rows)
                    {

                        obj_indRenewAdditionalGWASBlankForGridToObj = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADAdditionalGroundWaterAbstractionStructure();

                        obj_indRenewAdditionalGWASBlankForGridToObj.IndustrialRenewApplicationCode = Convert.ToInt64(gvGASAdditional.DataKeys[gvRow.RowIndex].Value.ToString());

                        if (Convert.ToInt64(gvGASAdditional.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                        {
                            Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                            if (lbl_SerialNumber.Text.Trim() == "")
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.SerialNumber = 0;
                            }
                            else
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                            }
                            Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");
                            if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                            }
                            else
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                            }
                            Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                            if (lbl_YearOfConstruction.Text.Trim() == "")
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.YearOfConstruction = null;
                            }
                            else
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                            }
                            Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                            if (lbl_DepthExist.Text.Trim() == "")
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.DepthExist = null;
                            }
                            else
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                            }
                            Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                            if (lbl_Diameter.Text.Trim() == "")
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.Diameter = null;
                            }
                            else
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                            }
                            Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                            if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                            }
                            else
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                            }
                            Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                            if (lbl_Discharge.Text.Trim() == "")
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.Discharge = null;
                            }
                            else
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                            }
                            Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                            if (lbl_OperationalHousrDay.Text.Trim() == "")
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.OperationalHousrDay = null;
                            }
                            else
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                            }

                            Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                            if (lbl_OperationalDaysYear.Text.Trim() == "")
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.OperationalDaysYear = null;
                            }
                            else
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                            }
                            Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                            if (lbl_LiftingDeviceCode.Text.Trim() == "")
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.LiftingDeviceCode = null;
                            }
                            else
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                            }
                            Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                            if (lbl_PowerOfPump.Text.Trim() == "")
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.PowerOfPump = null;
                            }
                            else
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                            }

                            Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                            switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                            {
                                case "":
                                    obj_indRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_indRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                    break;
                                case "No":
                                    obj_indRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                    break;
                            }
                            Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                            switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                            {
                                case "":
                                    obj_indRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_indRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                    break;
                                case "No":
                                    obj_indRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                    break;
                            }

                            Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                            if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                            }
                            else
                            {
                                obj_indRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                            }

                            lst_indRenewAdditionalGWASNewList.Add(obj_indRenewAdditionalGWASBlankForGridToObj);
                        }
                    }
                    lst_indRenewAdditionalGWASNewList.RemoveAt(lst_IntIndex);
                    gvGASAdditional.DataSource = lst_indRenewAdditionalGWASNewList;
                    gvGASAdditional.DataBind();
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    private void ClearGASAdditionalScreen()
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
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
}