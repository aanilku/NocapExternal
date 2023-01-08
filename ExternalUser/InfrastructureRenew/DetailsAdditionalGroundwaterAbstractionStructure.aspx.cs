using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.DAL.UserManagement;
using NOCAP.BLL.UserManagement;


public partial class ExternalUser_InfrastructureRenew_DetailsAdditionalGroundwaterAbstractionStructure : System.Web.UI.Page
{
    string strPageName = "INFRenewAdditionalGroundWaterAbstractionStructure";
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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblInfrastructureApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                        }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {

                    BindIGVInfRenewAdditionalGWASDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
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

    private void BindIGVInfRenewAdditionalGWASDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure obj_infRenewAdditionalGWASBlank = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure();
            List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure> lst_infRenewAdditionalGWASBlank = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure>();
            List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure> lst_infRenewAdditionalGWAS = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure>();

            lblHeadingProjName.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewSADApplication.NameOfInfrastructure);

            txtNumbeOfAdditionalGAS.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureRenewSADApplication.NumberOfStructureAdditional));
            if (txtNumbeOfAdditionalGAS.Text == "") { txtNumbeOfAdditionalGAS.Text = "0"; }
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure[] arr_infRenewAdditionalGWAS;
            arr_infRenewAdditionalGWAS = obj_infrastructureRenewSADApplication.GetInfrastructureRenewAdditionalGroundWaterAbstractionStructureList();

            foreach (NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure test in arr_infRenewAdditionalGWAS)
            {
                lst_infRenewAdditionalGWAS.Add(test);
            }
            gvGASAdditional.DataSource = lst_infRenewAdditionalGWAS;
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
                    UpdateInfRenewAdditionalGWASDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text));
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
                if (UpdateInfRenewAdditionalGWASDetails(Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text)) == 1)
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

                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure obj_infRenewAdditionalGWASBlankForGridToObj = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure();
                List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure> lst_infRenewAdditionalGWASNewList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure>();


                foreach (GridViewRow gvRow in gvGASAdditional.Rows)
                {

                    obj_infRenewAdditionalGWASBlankForGridToObj = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure();

                    obj_infRenewAdditionalGWASBlankForGridToObj.InfrastructureRenewApplicationCode = Convert.ToInt64(gvGASAdditional.DataKeys[gvRow.RowIndex].Value.ToString());

                    if (Convert.ToInt64(gvGASAdditional.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                    {
                        Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                        if (lbl_SerialNumber.Text.Trim() == "")
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.SerialNumber = 0;
                        }
                        else
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                        }

                        Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");

                        if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                        }
                        else
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                        }
                        Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                        if (lbl_YearOfConstruction.Text.Trim() == "")
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.YearOfConstruction = null;
                        }
                        else
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                        }
                        Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                        if (lbl_DepthExist.Text.Trim() == "")
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.DepthExist = null;
                        }
                        else
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                        }
                        Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                        if (lbl_Diameter.Text.Trim() == "")
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.Diameter = null;
                        }
                        else
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                        }
                        Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                        if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                        }
                        else
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                        }
                        Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                        if (lbl_Discharge.Text.Trim() == "")
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.Discharge = null;
                        }
                        else
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                        }
                        Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                        if (lbl_OperationalHousrDay.Text.Trim() == "")
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.OperationalHousrDay = null;
                        }
                        else
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                        }

                        Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                        if (lbl_OperationalDaysYear.Text.Trim() == "")
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.OperationalDaysYear = null;
                        }
                        else
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                        }
                        Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                        if (lbl_LiftingDeviceCode.Text.Trim() == "")
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.LiftingDeviceCode = null;
                        }
                        else
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                        }
                        Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                        if (lbl_PowerOfPump.Text.Trim() == "")
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.PowerOfPump = null;
                        }
                        else
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                        }

                        Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                        switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                        {
                            case "":
                                obj_infRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                break;
                            case "Yes":
                                obj_infRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                break;
                            case "No":
                                obj_infRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                break;
                        }
                        Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                        switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                        {
                            case "":
                                obj_infRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                break;
                            case "Yes":
                                obj_infRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                break;
                            case "No":
                                obj_infRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                break;
                        }

                        Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                        if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                        }
                        else
                        {
                            obj_infRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                        }

                        lst_infRenewAdditionalGWASNewList.Add(obj_infRenewAdditionalGWASBlankForGridToObj);


                    }
                }
                //---------------
                obj_infRenewAdditionalGWASBlankForGridToObj = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure();
                obj_infRenewAdditionalGWASBlankForGridToObj.InfrastructureRenewApplicationCode = Convert.ToInt64(lblInfrastructureApplicationCodeFrom.Text);

                obj_infRenewAdditionalGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(ddlTypeOfStructure.SelectedValue);

                if (txtYearOfConstruction.Text.Trim() == "")
                {
                    obj_infRenewAdditionalGWASBlankForGridToObj.YearOfConstruction = null;
                }
                else
                {
                    obj_infRenewAdditionalGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt32(txtYearOfConstruction.Text.Trim());
                }
                if (txtDepthMeter.Text.Trim() == "")
                {
                    obj_infRenewAdditionalGWASBlankForGridToObj.DepthExist = null;
                }
                else
                {
                    obj_infRenewAdditionalGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(txtDepthMeter.Text.Trim());
                }
                if (txtDiameterMM.Text.Trim() == "")
                {
                    obj_infRenewAdditionalGWASBlankForGridToObj.Diameter = null;
                }
                else
                {
                    obj_infRenewAdditionalGWASBlankForGridToObj.Diameter = Convert.ToInt32(txtDiameterMM.Text.Trim());
                }

                if (txtDepthWaterBelowGroundLevel.Text.Trim() == "")
                {
                    obj_infRenewAdditionalGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_infRenewAdditionalGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(txtDepthWaterBelowGroundLevel.Text.Trim());
                }
                if (txtDischarge.Text.Trim() == "")
                {
                    obj_infRenewAdditionalGWASBlankForGridToObj.Discharge = null;
                }
                else
                {
                    obj_infRenewAdditionalGWASBlankForGridToObj.Discharge = Convert.ToDecimal(txtDischarge.Text.Trim());
                }
                if (txtOperationalHoursDay.Text.Trim() == "")
                {
                    obj_infRenewAdditionalGWASBlankForGridToObj.OperationalHousrDay = null;
                }
                else
                {
                    obj_infRenewAdditionalGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(txtOperationalHoursDay.Text.Trim());
                }

                if (txtOperationalDaysYear.Text.Trim() == "")
                {
                    obj_infRenewAdditionalGWASBlankForGridToObj.OperationalDaysYear = null;
                }
                else
                {
                    obj_infRenewAdditionalGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(txtOperationalDaysYear.Text.Trim());
                }

                if (ddlModeOfLift.SelectedValue == "")
                {
                    obj_infRenewAdditionalGWASBlankForGridToObj.LiftingDeviceCode = null;
                }
                else
                {
                    obj_infRenewAdditionalGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(ddlModeOfLift.SelectedValue);
                }
                if (txtHorsePowerPump.Text.Trim() == "")
                {
                    obj_infRenewAdditionalGWASBlankForGridToObj.PowerOfPump = null;
                }
                else
                {
                    obj_infRenewAdditionalGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(txtHorsePowerPump.Text.Trim());
                }

                switch (ddlWhetherFittedWithWaterMeter.SelectedValue)
                {
                    case "":
                        obj_infRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_infRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_infRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }

                switch (ddlWhetherPermissionRegisteredWithCGWA.SelectedValue)
                {
                    case "":
                        obj_infRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_infRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_infRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (txtWhetherPermissionRegisteredWithCGWADet.Text.Trim() == "")
                {
                    obj_infRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_infRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = Convert.ToString(txtWhetherPermissionRegisteredWithCGWADet.Text.Trim());
                }

                lst_infRenewAdditionalGWASNewList.Add(obj_infRenewAdditionalGWASBlankForGridToObj);

                gvGASAdditional.DataSource = lst_infRenewAdditionalGWASNewList;
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

            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure obj_infRenewAdditionalGWAS = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure();

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
                    obj_infRenewAdditionalGWAS = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure(Convert.ToInt64(gvGASAdditional.DataKeys[e.Row.RowIndex].Values[0].ToString()), Convert.ToInt32(lbl_SerialNumber.Text));

                    lbl_TypeOfAbstractionStructureName.Text = HttpUtility.HtmlEncode(obj_infRenewAdditionalGWAS.GetTypeOfAbstractionStructure().TypeOfAbstractionStructureDesc);

                    if (obj_infRenewAdditionalGWAS.GetLiftingDevice() != null)
                    {

                        lbl_LiftingDeviceName.Text = HttpUtility.HtmlEncode(obj_infRenewAdditionalGWAS.GetLiftingDevice().LiftingDeviceDesc);

                        lbl_LiftingDeviceCode.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infRenewAdditionalGWAS.GetLiftingDevice().LiftingDeviceCode));
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

    private int UpdateInfRenewAdditionalGWASDetails(long lngA_ApplicationCode)
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
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication(lngA_ApplicationCode);

            List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure> lst_infRenewAdditionalGWASList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure>();

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

                NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure obj_infRenewAdditionalGWAS = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure();

                obj_infRenewAdditionalGWAS.InfrastructureRenewApplicationCode = Convert.ToInt32(gvGASAdditional.DataKeys[gvRow.RowIndex].Value.ToString());

                if (lbl_TypeOfAbstractionStructureCode.Text == "")
                {
                    obj_infRenewAdditionalGWAS.TypeOfAbstractionStructureCode = 0;
                }
                else
                {
                    obj_infRenewAdditionalGWAS.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text);
                }
                if (lbl_YearOfConstruction.Text.Trim() == "")
                {
                    obj_infRenewAdditionalGWAS.YearOfConstruction = null;
                }
                else
                {
                    obj_infRenewAdditionalGWAS.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                }
                if (lbl_DepthExist.Text.Trim() == "")
                {
                    obj_infRenewAdditionalGWAS.DepthExist = null;
                }
                else
                {
                    obj_infRenewAdditionalGWAS.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                }
                if (lbl_Diameter.Text.Trim() == "")
                {
                    obj_infRenewAdditionalGWAS.Diameter = null;
                }
                else
                {
                    obj_infRenewAdditionalGWAS.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                }
                if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                {
                    obj_infRenewAdditionalGWAS.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_infRenewAdditionalGWAS.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                }
                if (lbl_Discharge.Text.Trim() == "")
                {
                    obj_infRenewAdditionalGWAS.Discharge = null;
                }
                else
                {
                    obj_infRenewAdditionalGWAS.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                }
                if (lbl_OperationalHousrDay.Text.Trim() == "")
                {
                    obj_infRenewAdditionalGWAS.OperationalHousrDay = null;
                }
                else
                {
                    obj_infRenewAdditionalGWAS.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                }
                if (lbl_OperationalDaysYear.Text.Trim() == "")
                {
                    obj_infRenewAdditionalGWAS.OperationalDaysYear = null;
                }
                else
                {
                    obj_infRenewAdditionalGWAS.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                }
                if (lbl_LiftingDeviceCode.Text.Trim() == "")
                {
                    obj_infRenewAdditionalGWAS.LiftingDeviceCode = null;
                }
                else
                {
                    obj_infRenewAdditionalGWAS.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                }
                if (lbl_PowerOfPump.Text.Trim() == "")
                {
                    obj_infRenewAdditionalGWAS.PowerOfPump = null;
                }
                else
                {
                    obj_infRenewAdditionalGWAS.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                }
                switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                {
                    case "":
                        obj_infRenewAdditionalGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_infRenewAdditionalGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_infRenewAdditionalGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }
                switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                {
                    case "":
                        obj_infRenewAdditionalGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_infRenewAdditionalGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_infRenewAdditionalGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                {
                    obj_infRenewAdditionalGWAS.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_infRenewAdditionalGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                }

                obj_infRenewAdditionalGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;

                lst_infRenewAdditionalGWASList.Add(obj_infRenewAdditionalGWAS);
            }



            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure[] arr_tempInfGwasAdditionalListBLL = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure[lst_infRenewAdditionalGWASList.Count];
            lst_infRenewAdditionalGWASList.CopyTo(arr_tempInfGwasAdditionalListBLL);


            obj_InfrastructureRenewApplication.InfrastructureRenewAdditionalGroundWaterAbstractionStructureList = arr_tempInfGwasAdditionalListBLL;

            if (txtNumbeOfAdditionalGAS.Text.Trim() == "")
            {
                obj_InfrastructureRenewApplication.NumberOfStructureAdditional = null;
            }
            else
            {
                obj_InfrastructureRenewApplication.NumberOfStructureAdditional = Convert.ToInt32(txtNumbeOfAdditionalGAS.Text.Trim());
            }

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_InfrastructureRenewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_InfrastructureRenewApplication.Update() == 1)
            {
                strStatus = "Update Success";
                lblMessage.Text = "Successfully Saved";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                strStatus = "Update Failed";
                lblMessage.Text = obj_InfrastructureRenewApplication.CustumMessage;
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
                    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure obj_infRenewAdditionalGWASBlankForGridToObj = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure();
                    List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure> lst_infRenewAdditionalGWASNewList = new List<NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure>();


                    foreach (GridViewRow gvRow in gvGASAdditional.Rows)
                    {

                        obj_infRenewAdditionalGWASBlankForGridToObj = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADAdditionalGroundWaterAbstractionStructure();

                        obj_infRenewAdditionalGWASBlankForGridToObj.InfrastructureRenewApplicationCode = Convert.ToInt64(gvGASAdditional.DataKeys[gvRow.RowIndex].Value.ToString());

                        if (Convert.ToInt64(gvGASAdditional.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                        {
                            Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                            if (lbl_SerialNumber.Text.Trim() == "")
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.SerialNumber = 0;
                            }
                            else
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                            }
                            Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");
                            if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                            }
                            else
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                            }
                            Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                            if (lbl_YearOfConstruction.Text.Trim() == "")
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.YearOfConstruction = null;
                            }
                            else
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                            }
                            Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                            if (lbl_DepthExist.Text.Trim() == "")
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.DepthExist = null;
                            }
                            else
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                            }
                            Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                            if (lbl_Diameter.Text.Trim() == "")
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.Diameter = null;
                            }
                            else
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                            }
                            Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                            if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                            }
                            else
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                            }
                            Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                            if (lbl_Discharge.Text.Trim() == "")
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.Discharge = null;
                            }
                            else
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                            }
                            Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                            if (lbl_OperationalHousrDay.Text.Trim() == "")
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.OperationalHousrDay = null;
                            }
                            else
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                            }

                            Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                            if (lbl_OperationalDaysYear.Text.Trim() == "")
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.OperationalDaysYear = null;
                            }
                            else
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                            }
                            Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                            if (lbl_LiftingDeviceCode.Text.Trim() == "")
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.LiftingDeviceCode = null;
                            }
                            else
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                            }
                            Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                            if (lbl_PowerOfPump.Text.Trim() == "")
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.PowerOfPump = null;
                            }
                            else
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                            }

                            Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                            switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                            {
                                case "":
                                    obj_infRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_infRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                    break;
                                case "No":
                                    obj_infRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                    break;
                            }
                            Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                            switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                            {
                                case "":
                                    obj_infRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_infRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                    break;
                                case "No":
                                    obj_infRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                    break;
                            }

                            Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                            if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                            }
                            else
                            {
                                obj_infRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                            }

                            lst_infRenewAdditionalGWASNewList.Add(obj_infRenewAdditionalGWASBlankForGridToObj);
                        }
                    }
                    lst_infRenewAdditionalGWASNewList.RemoveAt(lst_IntIndex);
                    gvGASAdditional.DataSource = lst_infRenewAdditionalGWASNewList;
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