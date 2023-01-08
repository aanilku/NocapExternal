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
using System.Threading;

public partial class ExternalUser_IndustrialNew_AbstractionStructureKLD : System.Web.UI.Page
{
    string strPageName = "INDExistingGroundWaterAbstractionStructure";
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
                RngValYearOfConstructionP.MinimumValue = System.DateTime.Now.Year.ToString();
                

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

                if (NOCAPExternalUtility.FillDropDownTypeOfStructure(ref ddlTypeOfStructureP) != 1)
                {

                    lblMessage.Text = "Problem in Type of Structure population";
                    lblMessage.ForeColor = System.Drawing.Color.Red;

                }
                if (NOCAPExternalUtility.FillDropDownLiftingDevice(ref ddlModeOfLiftP) != 1)
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
                        SourceLabel = (Label)placeHolder.FindControl("lblIndustialApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                        }
                    }

                }
               
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    BindIGVIndNewExistingGWASDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {
                // lblMessage.Text = ex.Message;
                //  lblMessage.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }

    }
    #region Private
    private void ValidationExpInit()
    {
        #region GAS
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
        #endregion

        #region GASP
        revtxtNumbeOfProposedGAS.ValidationExpression = ValidationUtility.txtValForNumeric;
        revtxtNumbeOfProposedGAS.ErrorMessage = ValidationUtility.txtValForNumericMsg;

        revtxtYearOfConstructionP.ValidationExpression = ValidationUtility.txtValForNumericWithOutFirstCharacterZero;
        revtxtYearOfConstructionP.ErrorMessage = ValidationUtility.txtValForNumericWithOutFirstCharacterZeroMsg;

        revtxtDepthMeterP.ValidationExpression = ValidationUtility.txtValForDecimalValue("4", "2");
        revtxtDepthMeterP.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("4", "2");

        revtxtDiameterMMP.ValidationExpression = ValidationUtility.txtValForNumericWithOutFirstCharacterZero;
        revtxtDiameterMMP.ErrorMessage = ValidationUtility.txtValForNumericWithOutFirstCharacterZeroMsg;

        revtxtDepthWaterBelowGroundLevelP.ValidationExpression = ValidationUtility.txtValForDecimalValue("4", "2");
        revtxtDepthWaterBelowGroundLevelP.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("4", "2");

        revtxtDischargeP.ValidationExpression = ValidationUtility.txtValForDecimalValue("4", "2");
        revtxtDischargeP.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("4", "2");

        revtxtOperationalHoursDayP.ValidationExpression = ValidationUtility.txtValForNumericWithOutFirstCharacterZero;
        revtxtOperationalHoursDayP.ErrorMessage = ValidationUtility.txtValForNumericWithOutFirstCharacterZeroMsg;

        revtxtOperationalDaysYearP.ValidationExpression = ValidationUtility.txtValForNumericWithOutFirstCharacterZero;
        revtxtOperationalDaysYearP.ErrorMessage = ValidationUtility.txtValForNumericWithOutFirstCharacterZeroMsg;

        revtxtHorsePowerPumpP.ValidationExpression = ValidationUtility.txtValForDecimalValue("3", "2");
        revtxtHorsePowerPumpP.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("3", "2");

        revtxtWhetherPermissionRegisteredWithCGWADetP.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtWhetherPermissionRegisteredWithCGWADetP.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtWhetherPermissionRegisteredWithCGWADetP.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("100");
        revLengthtxtWhetherPermissionRegisteredWithCGWADetP.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("100");
        #endregion
    }
    private void BindIGVIndNewExistingGWASDetails(long lngA_ApplicationCode)
    {

        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(lngA_ApplicationCode);
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure obj_indNewExistingGWASBlank = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure();
        List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure> lst_indNewExistingGWASBlank = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure>();
        List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure> lst_indNewExistingGWAS = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure>();
        try
        {
            txtNumbeOfExistingGAS.Text = Convert.ToString(obj_industrialNewApplication.NumberOfStructureExisting);
            if (txtNumbeOfExistingGAS.Text == "") { txtNumbeOfExistingGAS.Text = "0"; }
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure[] arr_indNewExistingGWAS;
            arr_indNewExistingGWAS = obj_industrialNewApplication.GetIndustrialNewExistingGroundWaterAbstractionStructureList();
            foreach (NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure test in arr_indNewExistingGWAS)
            {
                lst_indNewExistingGWAS.Add(test);
            }
            gvGASExisting.DataSource = lst_indNewExistingGWAS;
            gvGASExisting.DataBind();




            List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure> lst_indNewProposedGWAS = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure>();

            txtNumbeOfProposedGAS.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.NumberOfStructureProposed));
            if (txtNumbeOfProposedGAS.Text == "") { txtNumbeOfProposedGAS.Text = "0"; }
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure[] arr_indNewProposedGWAS;
            arr_indNewProposedGWAS = obj_industrialNewApplication.GetIndustrialNewProposedGroundWaterAbstractionStructureList();

            foreach (NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure test in arr_indNewProposedGWAS)
            {
                lst_indNewProposedGWAS.Add(test);
            }

            gvGASProposed.DataSource = lst_indNewProposedGWAS;
            gvGASProposed.DataBind();

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
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
    private int UpdateIndNewExistingGWASDetails(long lngA_ApplicationCode)
    {
        try
        {
            #region GASE
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
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(lngA_ApplicationCode);

            List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure> lst_indNewExistingGWASList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure>();

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

                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure obj_indNewExistingGWAS = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure();

                obj_indNewExistingGWAS.IndustrialNewApplicationCode = Convert.ToInt32(gvGASExisting.DataKeys[gvRow.RowIndex].Value.ToString());

                if (lbl_TypeOfAbstractionStructureCode.Text == "")
                {
                    obj_indNewExistingGWAS.TypeOfAbstractionStructureCode = 0;
                }
                else
                {
                    obj_indNewExistingGWAS.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text);
                }
                if (lbl_YearOfConstruction.Text.Trim() == "")
                {
                    obj_indNewExistingGWAS.YearOfConstruction = null;
                }
                else
                {
                    obj_indNewExistingGWAS.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                }
                if (lbl_DepthExist.Text.Trim() == "")
                {
                    obj_indNewExistingGWAS.DepthExist = null;
                }
                else
                {
                    obj_indNewExistingGWAS.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                }
                if (lbl_Diameter.Text.Trim() == "")
                {
                    obj_indNewExistingGWAS.Diameter = null;
                }
                else
                {
                    obj_indNewExistingGWAS.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                }
                if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                {
                    obj_indNewExistingGWAS.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_indNewExistingGWAS.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                }
                if (lbl_Discharge.Text.Trim() == "")
                {
                    obj_indNewExistingGWAS.Discharge = null;
                }
                else
                {
                    obj_indNewExistingGWAS.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                }
                if (lbl_OperationalHousrDay.Text.Trim() == "")
                {
                    obj_indNewExistingGWAS.OperationalHousrDay = null;
                }
                else
                {
                    obj_indNewExistingGWAS.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                }
                if (lbl_OperationalDaysYear.Text.Trim() == "")
                {
                    obj_indNewExistingGWAS.OperationalDaysYear = null;
                }
                else
                {
                    obj_indNewExistingGWAS.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                }
                if (lbl_LiftingDeviceCode.Text.Trim() == "")
                {
                    obj_indNewExistingGWAS.LiftingDeviceCode = null;
                }
                else
                {
                    obj_indNewExistingGWAS.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                }
                if (lbl_PowerOfPump.Text.Trim() == "")
                {
                    obj_indNewExistingGWAS.PowerOfPump = null;
                }
                else
                {
                    obj_indNewExistingGWAS.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                }
                switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                {
                    case "":
                        obj_indNewExistingGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_indNewExistingGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_indNewExistingGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }
                switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                {
                    case "":
                        obj_indNewExistingGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_indNewExistingGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_indNewExistingGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                {
                    obj_indNewExistingGWAS.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_indNewExistingGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                }

                obj_indNewExistingGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;

                lst_indNewExistingGWASList.Add(obj_indNewExistingGWAS);
            }



            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure[] arr_tempIndGwasExistingListBLL = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure[lst_indNewExistingGWASList.Count];
            lst_indNewExistingGWASList.CopyTo(arr_tempIndGwasExistingListBLL);
            obj_industrialNewApplication.IndustrialNewExistingGroundWaterAbstractionStructureList = arr_tempIndGwasExistingListBLL;

            if (txtNumbeOfExistingGAS.Text.Trim() == "")
            {
                obj_industrialNewApplication.NumberOfStructureExisting = null;
            }
            else
            {
                obj_industrialNewApplication.NumberOfStructureExisting = Convert.ToInt32(txtNumbeOfExistingGAS.Text.Trim());
            }
            #endregion

            #region GASP
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
            if (Convert.ToInt32(txtNumbeOfProposedGAS.Text.Trim()) != gvGASProposed.Rows.Count)
            {
                lblMessage.Text = "Number of Records in \"Detail of Structures\" should be equal to the \"Number of Proposed Structures\" given above.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            strActionName = "UpdateProposedGroundWaterAbstractionStructure";
           
            List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure> lst_indNewProposedGWASList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure>();

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

                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure obj_indNewProposedGWAS = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure();

                obj_indNewProposedGWAS.IndustrialNewApplicationCode = Convert.ToInt32(gvGASProposed.DataKeys[gvRow.RowIndex].Value.ToString());

                if (lbl_TypeOfAbstractionStructureCode.Text == "")
                {
                    obj_indNewProposedGWAS.TypeOfAbstractionStructureCode = 0;
                }
                else
                {
                    obj_indNewProposedGWAS.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text);
                }
                if (lbl_YearOfConstruction.Text.Trim() == "")
                {
                    obj_indNewProposedGWAS.YearOfConstruction = null;
                }
                else
                {
                    obj_indNewProposedGWAS.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                }
                if (lbl_DepthExist.Text.Trim() == "")
                {
                    obj_indNewProposedGWAS.DepthExist = null;
                }
                else
                {
                    obj_indNewProposedGWAS.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                }
                if (lbl_Diameter.Text.Trim() == "")
                {
                    obj_indNewProposedGWAS.Diameter = null;
                }
                else
                {
                    obj_indNewProposedGWAS.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                }
                if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                {
                    obj_indNewProposedGWAS.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_indNewProposedGWAS.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                }
                if (lbl_Discharge.Text.Trim() == "")
                {
                    obj_indNewProposedGWAS.Discharge = null;
                }
                else
                {
                    obj_indNewProposedGWAS.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                }
                if (lbl_OperationalHousrDay.Text.Trim() == "")
                {
                    obj_indNewProposedGWAS.OperationalHousrDay = null;
                }
                else
                {
                    obj_indNewProposedGWAS.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                }
                if (lbl_OperationalDaysYear.Text.Trim() == "")
                {
                    obj_indNewProposedGWAS.OperationalDaysYear = null;
                }
                else
                {
                    obj_indNewProposedGWAS.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                }
                if (lbl_LiftingDeviceCode.Text.Trim() == "")
                {
                    obj_indNewProposedGWAS.LiftingDeviceCode = null;
                }
                else
                {
                    obj_indNewProposedGWAS.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                }
                if (lbl_PowerOfPump.Text.Trim() == "")
                {
                    obj_indNewProposedGWAS.PowerOfPump = null;
                }
                else
                {
                    obj_indNewProposedGWAS.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                }
                switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                {
                    case "":
                        obj_indNewProposedGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_indNewProposedGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_indNewProposedGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }
                switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                {
                    case "":
                        obj_indNewProposedGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_indNewProposedGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_indNewProposedGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                {
                    obj_indNewProposedGWAS.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_indNewProposedGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                }

                obj_indNewProposedGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;

                lst_indNewProposedGWASList.Add(obj_indNewProposedGWAS);
            }



            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure[] arr_tempIndGwasProposedListBLL = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure[lst_indNewProposedGWASList.Count];
            lst_indNewProposedGWASList.CopyTo(arr_tempIndGwasProposedListBLL);

            obj_industrialNewApplication.IndustrialNewProposedGroundWaterAbstractionStructureList = arr_tempIndGwasProposedListBLL;

            if (txtNumbeOfProposedGAS.Text.Trim() == "")
            {
                obj_industrialNewApplication.NumberOfStructureProposed = null;
            }
            else
            {
                obj_industrialNewApplication.NumberOfStructureProposed = Convert.ToInt32(txtNumbeOfProposedGAS.Text.Trim());
            }

            #endregion
            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_industrialNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_industrialNewApplication.Update() == 1)
            {
                strStatus = "Update Success";
                lblMessage.Text = "Successfully Saved";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                strStatus = "Update Failed";
                lblMessage.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.CustumMessage);
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

    #endregion

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
            Server.Transfer("~/ExternalUser/IndustrialNew/IndustrialNewKLD.aspx");

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

                    UpdateIndNewExistingGWASDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));

                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
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

                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure obj_indNewExistingGWASBlankForGridToObj = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure();
                List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure> lst_indNewExistingGWASNewList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure>();


                foreach (GridViewRow gvRow in gvGASExisting.Rows)
                {

                    obj_indNewExistingGWASBlankForGridToObj = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure();

                    obj_indNewExistingGWASBlankForGridToObj.IndustrialNewApplicationCode = Convert.ToInt64(gvGASExisting.DataKeys[gvRow.RowIndex].Value.ToString());

                    if (Convert.ToInt64(gvGASExisting.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                    {
                        Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                        if (lbl_SerialNumber.Text.Trim() == "")
                        {
                            obj_indNewExistingGWASBlankForGridToObj.SerialNumber = 0;
                        }
                        else
                        {
                            obj_indNewExistingGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                        }

                        Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");

                        if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                        {
                            obj_indNewExistingGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                        }
                        else
                        {
                            obj_indNewExistingGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                        }
                        Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                        if (lbl_YearOfConstruction.Text.Trim() == "")
                        {
                            obj_indNewExistingGWASBlankForGridToObj.YearOfConstruction = null;
                        }
                        else
                        {
                            obj_indNewExistingGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                        }
                        Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                        if (lbl_DepthExist.Text.Trim() == "")
                        {
                            obj_indNewExistingGWASBlankForGridToObj.DepthExist = null;
                        }
                        else
                        {
                            obj_indNewExistingGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                        }
                        Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                        if (lbl_Diameter.Text.Trim() == "")
                        {
                            obj_indNewExistingGWASBlankForGridToObj.Diameter = null;
                        }
                        else
                        {
                            obj_indNewExistingGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                        }
                        Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                        if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                        {
                            obj_indNewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                        }
                        else
                        {
                            obj_indNewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                        }
                        Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                        if (lbl_Discharge.Text.Trim() == "")
                        {
                            obj_indNewExistingGWASBlankForGridToObj.Discharge = null;
                        }
                        else
                        {
                            obj_indNewExistingGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                        }
                        Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                        if (lbl_OperationalHousrDay.Text.Trim() == "")
                        {
                            obj_indNewExistingGWASBlankForGridToObj.OperationalHousrDay = null;
                        }
                        else
                        {
                            obj_indNewExistingGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                        }

                        Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                        if (lbl_OperationalDaysYear.Text.Trim() == "")
                        {
                            obj_indNewExistingGWASBlankForGridToObj.OperationalDaysYear = null;
                        }
                        else
                        {
                            obj_indNewExistingGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                        }
                        Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                        if (lbl_LiftingDeviceCode.Text.Trim() == "")
                        {
                            obj_indNewExistingGWASBlankForGridToObj.LiftingDeviceCode = null;
                        }
                        else
                        {
                            obj_indNewExistingGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                        }
                        Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                        if (lbl_PowerOfPump.Text.Trim() == "")
                        {
                            obj_indNewExistingGWASBlankForGridToObj.PowerOfPump = null;
                        }
                        else
                        {
                            obj_indNewExistingGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                        }

                        Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                        switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                        {
                            case "":
                                obj_indNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                break;
                            case "Yes":
                                obj_indNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                break;
                            case "No":
                                obj_indNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                break;
                        }
                        Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                        switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                        {
                            case "":
                                obj_indNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                break;
                            case "Yes":
                                obj_indNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                break;
                            case "No":
                                obj_indNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                break;
                        }

                        Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                        if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                        {
                            obj_indNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                        }
                        else
                        {
                            obj_indNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                        }

                        lst_indNewExistingGWASNewList.Add(obj_indNewExistingGWASBlankForGridToObj);


                    }
                }
                //---------------
                obj_indNewExistingGWASBlankForGridToObj = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure();
                obj_indNewExistingGWASBlankForGridToObj.IndustrialNewApplicationCode = Convert.ToInt64(lblIndustialApplicationCodeFrom.Text);

                obj_indNewExistingGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(ddlTypeOfStructure.SelectedValue);

                if (txtYearOfConstruction.Text.Trim() == "")
                {
                    obj_indNewExistingGWASBlankForGridToObj.YearOfConstruction = null;
                }
                else
                {
                    obj_indNewExistingGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt32(txtYearOfConstruction.Text.Trim());
                }
                if (txtDepthMeter.Text.Trim() == "")
                {
                    obj_indNewExistingGWASBlankForGridToObj.DepthExist = null;
                }
                else
                {
                    obj_indNewExistingGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(txtDepthMeter.Text.Trim());
                }
                if (txtDiameterMM.Text.Trim() == "")
                {
                    obj_indNewExistingGWASBlankForGridToObj.Diameter = null;
                }
                else
                {
                    obj_indNewExistingGWASBlankForGridToObj.Diameter = Convert.ToInt32(txtDiameterMM.Text.Trim());
                }

                if (txtDepthWaterBelowGroundLevel.Text.Trim() == "")
                {
                    obj_indNewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_indNewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(txtDepthWaterBelowGroundLevel.Text.Trim());
                }
                if (txtDischarge.Text.Trim() == "")
                {
                    obj_indNewExistingGWASBlankForGridToObj.Discharge = null;
                }
                else
                {
                    obj_indNewExistingGWASBlankForGridToObj.Discharge = Convert.ToDecimal(txtDischarge.Text.Trim());
                }
                if (txtOperationalHoursDay.Text.Trim() == "")
                {
                    obj_indNewExistingGWASBlankForGridToObj.OperationalHousrDay = null;
                }
                else
                {
                    obj_indNewExistingGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(txtOperationalHoursDay.Text.Trim());
                }

                if (txtOperationalDaysYear.Text.Trim() == "")
                {
                    obj_indNewExistingGWASBlankForGridToObj.OperationalDaysYear = null;
                }
                else
                {
                    obj_indNewExistingGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(txtOperationalDaysYear.Text.Trim());
                }

                if (ddlModeOfLift.SelectedValue == "")
                {
                    obj_indNewExistingGWASBlankForGridToObj.LiftingDeviceCode = null;
                }
                else
                {
                    obj_indNewExistingGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(ddlModeOfLift.SelectedValue);
                }
                if (txtHorsePowerPump.Text.Trim() == "")
                {
                    obj_indNewExistingGWASBlankForGridToObj.PowerOfPump = null;
                }
                else
                {
                    obj_indNewExistingGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(txtHorsePowerPump.Text.Trim());
                }

                switch (ddlWhetherFittedWithWaterMeter.SelectedValue)
                {
                    case "":
                        obj_indNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_indNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_indNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }

                switch (ddlWhetherPermissionRegisteredWithCGWA.SelectedValue)
                {
                    case "":
                        obj_indNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_indNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_indNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (txtWhetherPermissionRegisteredWithCGWADet.Text.Trim() == "")
                {
                    obj_indNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_indNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = Convert.ToString(txtWhetherPermissionRegisteredWithCGWADet.Text.Trim());
                }

                lst_indNewExistingGWASNewList.Add(obj_indNewExistingGWASBlankForGridToObj);

                gvGASExisting.DataSource = lst_indNewExistingGWASNewList;
                gvGASExisting.DataBind();

                ClearGASExistingScreen();
            }

            catch (Exception)
            {
                //lblMessage.Text = ex.Message;
                //lblMessage.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void txtNext_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try {
                if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
                {
                    Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
                }
                else
                {

                    hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                    Session["CSRF"] = hidCSRF.Value;
                    if (UpdateIndNewExistingGWASDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text)) == 1)
                    {
                        Server.Transfer("~/ExternalUser/IndustrialNew/AttachmentKLD.aspx");
                    }
                }
            }
            catch (ThreadAbortException)
            {


            }
            catch (Exception ex)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    protected void gvGASExisting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure obj_indNewExistingGWAS = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure();
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
                    obj_indNewExistingGWAS = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure(Convert.ToInt64(gvGASExisting.DataKeys[e.Row.RowIndex].Values[0].ToString()), Convert.ToInt32(lbl_SerialNumber.Text));
                    lbl_TypeOfAbstractionStructureName.Text = HttpUtility.HtmlEncode(obj_indNewExistingGWAS.GetTypeOfAbstractionStructure().TypeOfAbstractionStructureDesc);
                    if (obj_indNewExistingGWAS.GetLiftingDevice() != null)
                    {
                        lbl_LiftingDeviceName.Text = HttpUtility.HtmlEncode(obj_indNewExistingGWAS.GetLiftingDevice().LiftingDeviceDesc);
                        lbl_LiftingDeviceCode.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_indNewExistingGWAS.GetLiftingDevice().LiftingDeviceCode));
                    }
                }
            }
            
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
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
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure obj_indNewExistingGWASBlankForGridToObj = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure();
                    List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure> lst_indNewExistingGWASNewList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure>();


                    foreach (GridViewRow gvRow in gvGASExisting.Rows)
                    {

                        obj_indNewExistingGWASBlankForGridToObj = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADExistingGroundWaterAbstractionStructure();

                        obj_indNewExistingGWASBlankForGridToObj.IndustrialNewApplicationCode = Convert.ToInt64(gvGASExisting.DataKeys[gvRow.RowIndex].Value.ToString());

                        if (Convert.ToInt64(gvGASExisting.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                        {
                            Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                            if (lbl_SerialNumber.Text.Trim() == "")
                            {
                                obj_indNewExistingGWASBlankForGridToObj.SerialNumber = 0;
                            }
                            else
                            {
                                obj_indNewExistingGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                            }
                            Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");
                            if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                            {
                                obj_indNewExistingGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                            }
                            else
                            {
                                obj_indNewExistingGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                            }
                            Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                            if (lbl_YearOfConstruction.Text.Trim() == "")
                            {
                                obj_indNewExistingGWASBlankForGridToObj.YearOfConstruction = null;
                            }
                            else
                            {
                                obj_indNewExistingGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                            }
                            Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                            if (lbl_DepthExist.Text.Trim() == "")
                            {
                                obj_indNewExistingGWASBlankForGridToObj.DepthExist = null;
                            }
                            else
                            {
                                obj_indNewExistingGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                            }
                            Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                            if (lbl_Diameter.Text.Trim() == "")
                            {
                                obj_indNewExistingGWASBlankForGridToObj.Diameter = null;
                            }
                            else
                            {
                                obj_indNewExistingGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                            }
                            Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                            if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                            {
                                obj_indNewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                            }
                            else
                            {
                                obj_indNewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                            }
                            Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                            if (lbl_Discharge.Text.Trim() == "")
                            {
                                obj_indNewExistingGWASBlankForGridToObj.Discharge = null;
                            }
                            else
                            {
                                obj_indNewExistingGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                            }
                            Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                            if (lbl_OperationalHousrDay.Text.Trim() == "")
                            {
                                obj_indNewExistingGWASBlankForGridToObj.OperationalHousrDay = null;
                            }
                            else
                            {
                                obj_indNewExistingGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                            }

                            Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                            if (lbl_OperationalDaysYear.Text.Trim() == "")
                            {
                                obj_indNewExistingGWASBlankForGridToObj.OperationalDaysYear = null;
                            }
                            else
                            {
                                obj_indNewExistingGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                            }
                            Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                            if (lbl_LiftingDeviceCode.Text.Trim() == "")
                            {
                                obj_indNewExistingGWASBlankForGridToObj.LiftingDeviceCode = null;
                            }
                            else
                            {
                                obj_indNewExistingGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                            }
                            Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                            if (lbl_PowerOfPump.Text.Trim() == "")
                            {
                                obj_indNewExistingGWASBlankForGridToObj.PowerOfPump = null;
                            }
                            else
                            {
                                obj_indNewExistingGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                            }

                            Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                            switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                            {
                                case "":
                                    obj_indNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_indNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                    break;
                                case "No":
                                    obj_indNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                    break;
                            }
                            Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                            switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                            {
                                case "":
                                    obj_indNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_indNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                    break;
                                case "No":
                                    obj_indNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                    break;
                            }

                            Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                            if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                            {
                                obj_indNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                            }
                            else
                            {
                                obj_indNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                            }

                            lst_indNewExistingGWASNewList.Add(obj_indNewExistingGWASBlankForGridToObj);


                        }
                    }
                    lst_indNewExistingGWASNewList.RemoveAt(lst_IntIndex);
                    gvGASExisting.DataSource = lst_indNewExistingGWASNewList;
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







    protected void gvGASProposed_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure obj_indNewProposedGWAS = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure();
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
                    obj_indNewProposedGWAS = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure(Convert.ToInt64(gvGASProposed.DataKeys[e.Row.RowIndex].Values[0].ToString()), Convert.ToInt32(lbl_SerialNumber.Text));
                    lbl_TypeOfAbstractionStructureName.Text = HttpUtility.HtmlEncode(obj_indNewProposedGWAS.GetTypeOfAbstractionStructure().TypeOfAbstractionStructureDesc);
                    if (obj_indNewProposedGWAS.GetLiftingDevice() != null)
                    {
                        lbl_LiftingDeviceName.Text = HttpUtility.HtmlEncode(obj_indNewProposedGWAS.GetLiftingDevice().LiftingDeviceDesc);
                        lbl_LiftingDeviceCode.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_indNewProposedGWAS.GetLiftingDevice().LiftingDeviceCode));
                    }

                }

            }
           
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

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
                    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure obj_indNewProposedGWASBlankForGridToObj = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure();
                    List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure> lst_indNewProposedGWASNewList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure>();


                    foreach (GridViewRow gvRow in gvGASProposed.Rows)
                    {

                        obj_indNewProposedGWASBlankForGridToObj = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure();

                        obj_indNewProposedGWASBlankForGridToObj.IndustrialNewApplicationCode = Convert.ToInt64(gvGASProposed.DataKeys[gvRow.RowIndex].Value.ToString());

                        if (Convert.ToInt64(gvGASProposed.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                        {
                            Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                            if (lbl_SerialNumber.Text.Trim() == "")
                            {
                                obj_indNewProposedGWASBlankForGridToObj.SerialNumber = 0;
                            }
                            else
                            {
                                obj_indNewProposedGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                            }
                            Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");
                            if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                            {
                                obj_indNewProposedGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                            }
                            else
                            {
                                obj_indNewProposedGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                            }
                            Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                            if (lbl_YearOfConstruction.Text.Trim() == "")
                            {
                                obj_indNewProposedGWASBlankForGridToObj.YearOfConstruction = null;
                            }
                            else
                            {
                                obj_indNewProposedGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                            }
                            Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                            if (lbl_DepthExist.Text.Trim() == "")
                            {
                                obj_indNewProposedGWASBlankForGridToObj.DepthExist = null;
                            }
                            else
                            {
                                obj_indNewProposedGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                            }
                            Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                            if (lbl_Diameter.Text.Trim() == "")
                            {
                                obj_indNewProposedGWASBlankForGridToObj.Diameter = null;
                            }
                            else
                            {
                                obj_indNewProposedGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                            }
                            Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                            if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                            {
                                obj_indNewProposedGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                            }
                            else
                            {
                                obj_indNewProposedGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                            }
                            Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                            if (lbl_Discharge.Text.Trim() == "")
                            {
                                obj_indNewProposedGWASBlankForGridToObj.Discharge = null;
                            }
                            else
                            {
                                obj_indNewProposedGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                            }
                            Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                            if (lbl_OperationalHousrDay.Text.Trim() == "")
                            {
                                obj_indNewProposedGWASBlankForGridToObj.OperationalHousrDay = null;
                            }
                            else
                            {
                                obj_indNewProposedGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                            }

                            Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                            if (lbl_OperationalDaysYear.Text.Trim() == "")
                            {
                                obj_indNewProposedGWASBlankForGridToObj.OperationalDaysYear = null;
                            }
                            else
                            {
                                obj_indNewProposedGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                            }
                            Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                            if (lbl_LiftingDeviceCode.Text.Trim() == "")
                            {
                                obj_indNewProposedGWASBlankForGridToObj.LiftingDeviceCode = null;
                            }
                            else
                            {
                                obj_indNewProposedGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                            }
                            Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                            if (lbl_PowerOfPump.Text.Trim() == "")
                            {
                                obj_indNewProposedGWASBlankForGridToObj.PowerOfPump = null;
                            }
                            else
                            {
                                obj_indNewProposedGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                            }

                            Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                            switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                            {
                                case "":
                                    obj_indNewProposedGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_indNewProposedGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                    break;
                                case "No":
                                    obj_indNewProposedGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                    break;
                            }
                            Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                            switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                            {
                                case "":
                                    obj_indNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_indNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                    break;
                                case "No":
                                    obj_indNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                    break;
                            }

                            Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                            if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                            {
                                obj_indNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                            }
                            else
                            {
                                obj_indNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                            }

                            lst_indNewProposedGWASNewList.Add(obj_indNewProposedGWASBlankForGridToObj);


                        }
                    }
                    lst_indNewProposedGWASNewList.RemoveAt(lst_IntIndex);
                    gvGASProposed.DataSource = lst_indNewProposedGWASNewList;
                    gvGASProposed.DataBind();
                }
            }

            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void btnAddP_Click(object sender, EventArgs e)
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



                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure obj_indNewProposedGWASBlankForGridToObj = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure();
                List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure> lst_indNewProposedGWASNewList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure>();


                foreach (GridViewRow gvRow in gvGASProposed.Rows)
                {

                    obj_indNewProposedGWASBlankForGridToObj = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure();

                    obj_indNewProposedGWASBlankForGridToObj.IndustrialNewApplicationCode = Convert.ToInt64(gvGASProposed.DataKeys[gvRow.RowIndex].Value.ToString());

                    if (Convert.ToInt64(gvGASProposed.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                    {
                        Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                        if (lbl_SerialNumber.Text.Trim() == "")
                        {
                            obj_indNewProposedGWASBlankForGridToObj.SerialNumber = 0;
                        }
                        else
                        {
                            obj_indNewProposedGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                        }

                        Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");

                        if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                        {
                            obj_indNewProposedGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                        }
                        else
                        {
                            obj_indNewProposedGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                        }
                        Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                        if (lbl_YearOfConstruction.Text.Trim() == "")
                        {
                            obj_indNewProposedGWASBlankForGridToObj.YearOfConstruction = null;
                        }
                        else
                        {
                            obj_indNewProposedGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                        }
                        Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                        if (lbl_DepthExist.Text.Trim() == "")
                        {
                            obj_indNewProposedGWASBlankForGridToObj.DepthExist = null;
                        }
                        else
                        {
                            obj_indNewProposedGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                        }
                        Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                        if (lbl_Diameter.Text.Trim() == "")
                        {
                            obj_indNewProposedGWASBlankForGridToObj.Diameter = null;
                        }
                        else
                        {
                            obj_indNewProposedGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                        }
                        Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                        if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                        {
                            obj_indNewProposedGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                        }
                        else
                        {
                            obj_indNewProposedGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                        }
                        Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                        if (lbl_Discharge.Text.Trim() == "")
                        {
                            obj_indNewProposedGWASBlankForGridToObj.Discharge = null;
                        }
                        else
                        {
                            obj_indNewProposedGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                        }
                        Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                        if (lbl_OperationalHousrDay.Text.Trim() == "")
                        {
                            obj_indNewProposedGWASBlankForGridToObj.OperationalHousrDay = null;
                        }
                        else
                        {
                            obj_indNewProposedGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                        }

                        Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                        if (lbl_OperationalDaysYear.Text.Trim() == "")
                        {
                            obj_indNewProposedGWASBlankForGridToObj.OperationalDaysYear = null;
                        }
                        else
                        {
                            obj_indNewProposedGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                        }
                        Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                        if (lbl_LiftingDeviceCode.Text.Trim() == "")
                        {
                            obj_indNewProposedGWASBlankForGridToObj.LiftingDeviceCode = null;
                        }
                        else
                        {
                            obj_indNewProposedGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                        }
                        Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                        if (lbl_PowerOfPump.Text.Trim() == "")
                        {
                            obj_indNewProposedGWASBlankForGridToObj.PowerOfPump = null;
                        }
                        else
                        {
                            obj_indNewProposedGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                        }

                        Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                        switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                        {
                            case "":
                                obj_indNewProposedGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                break;
                            case "Yes":
                                obj_indNewProposedGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                break;
                            case "No":
                                obj_indNewProposedGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                break;
                        }
                        Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                        switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                        {
                            case "":
                                obj_indNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                break;
                            case "Yes":
                                obj_indNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                break;
                            case "No":
                                obj_indNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                break;
                        }

                        Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                        if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                        {
                            obj_indNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                        }
                        else
                        {
                            obj_indNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                        }

                        lst_indNewProposedGWASNewList.Add(obj_indNewProposedGWASBlankForGridToObj);


                    }
                }
                //---------------
                obj_indNewProposedGWASBlankForGridToObj = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure();
                obj_indNewProposedGWASBlankForGridToObj.IndustrialNewApplicationCode = Convert.ToInt64(lblIndustialApplicationCodeFrom.Text);

                obj_indNewProposedGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(ddlTypeOfStructureP.SelectedValue);

                if (txtYearOfConstructionP.Text.Trim() == "")
                {
                    obj_indNewProposedGWASBlankForGridToObj.YearOfConstruction = null;
                }
                else
                {
                    obj_indNewProposedGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt32(txtYearOfConstructionP.Text.Trim());
                }
                if (txtDepthMeterP.Text.Trim() == "")
                {
                    obj_indNewProposedGWASBlankForGridToObj.DepthExist = null;
                }
                else
                {
                    obj_indNewProposedGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(txtDepthMeterP.Text.Trim());
                }
                if (txtDiameterMMP.Text.Trim() == "")
                {
                    obj_indNewProposedGWASBlankForGridToObj.Diameter = null;
                }
                else
                {
                    obj_indNewProposedGWASBlankForGridToObj.Diameter = Convert.ToInt32(txtDiameterMMP.Text.Trim());
                }

                if (txtDepthWaterBelowGroundLevelP.Text.Trim() == "")
                {
                    obj_indNewProposedGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_indNewProposedGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(txtDepthWaterBelowGroundLevelP.Text.Trim());
                }
                if (txtDischargeP.Text.Trim() == "")
                {
                    obj_indNewProposedGWASBlankForGridToObj.Discharge = null;
                }
                else
                {
                    obj_indNewProposedGWASBlankForGridToObj.Discharge = Convert.ToDecimal(txtDischargeP.Text.Trim());
                }
                if (txtOperationalHoursDayP.Text.Trim() == "")
                {
                    obj_indNewProposedGWASBlankForGridToObj.OperationalHousrDay = null;
                }
                else
                {
                    obj_indNewProposedGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(txtOperationalHoursDayP.Text.Trim());
                }

                if (txtOperationalDaysYearP.Text.Trim() == "")
                {
                    obj_indNewProposedGWASBlankForGridToObj.OperationalDaysYear = null;
                }
                else
                {
                    obj_indNewProposedGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(txtOperationalDaysYearP.Text.Trim());
                }

                if (ddlModeOfLiftP.SelectedValue == "")
                {
                    obj_indNewProposedGWASBlankForGridToObj.LiftingDeviceCode = null;
                }
                else
                {
                    obj_indNewProposedGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(ddlModeOfLiftP.SelectedValue);
                }
                if (txtHorsePowerPumpP.Text.Trim() == "")
                {
                    obj_indNewProposedGWASBlankForGridToObj.PowerOfPump = null;
                }
                else
                {
                    obj_indNewProposedGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(txtHorsePowerPumpP.Text.Trim());
                }

                switch (ddlWhetherFittedWithWaterMeterP.SelectedValue)
                {
                    case "":
                        obj_indNewProposedGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_indNewProposedGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_indNewProposedGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }

                switch (ddlWhetherPermissionRegisteredWithCGWAP.SelectedValue)
                {
                    case "":
                        obj_indNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_indNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_indNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (txtWhetherPermissionRegisteredWithCGWADetP.Text.Trim() == "")
                {
                    obj_indNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_indNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = Convert.ToString(txtWhetherPermissionRegisteredWithCGWADetP.Text.Trim());
                }

                lst_indNewProposedGWASNewList.Add(obj_indNewProposedGWASBlankForGridToObj);

                gvGASProposed.DataSource = lst_indNewProposedGWASNewList;
                gvGASProposed.DataBind();

                ClearGASProposedScreen();
            }

            catch (Exception)
            {
                //lblMessage.Text = ex.Message;
                //lblMessage.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    private void ClearGASProposedScreen()
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
    protected void ddlWhetherPermissionRegisteredWithCGWAP_SelectedIndexChanged(object sender, EventArgs e)
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


                if (ddlWhetherPermissionRegisteredWithCGWAP.SelectedItem.Text == "No")
                {
                    txtWhetherPermissionRegisteredWithCGWADetP.Enabled = false;
                }
                else
                {
                    txtWhetherPermissionRegisteredWithCGWADetP.Enabled = true;
                }

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }

    }
    //private int UpdateIndNewProposedGWASDetails(long lngA_ApplicationCode)
    //{
    //    try
    //    {
    //        if (txtNumbeOfProposedGAS.Text.Trim() == "")
    //        {
    //            lblMessage.Text = "Number of Proposed Structures can not be Blank";
    //            lblMessage.ForeColor = System.Drawing.Color.Red;
    //            return 0;
    //        }
    //        if (!NOCAPExternalUtility.IsNumeric(txtNumbeOfProposedGAS.Text))
    //        {
    //            lblMessage.Text = "Number of Proposed Structures allows Numeric";
    //            lblMessage.ForeColor = System.Drawing.Color.Red;
    //            return 0;
    //        }
    //        if (Convert.ToInt32(txtNumbeOfProposedGAS.Text.Trim()) != gvGASProposed.Rows.Count)
    //        {
    //            lblMessage.Text = "Number of Records in \"Detail of Structures\" should be equal to the \"Number of Proposed Structures\" given above.";
    //            lblMessage.ForeColor = System.Drawing.Color.Red;
    //            return 0;
    //        }
    //        strActionName = "UpdateProposedGroundWaterAbstractionStructure";
    //        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(lngA_ApplicationCode);

    //        List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure> lst_indNewProposedGWASList = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure>();

    //        foreach (GridViewRow gvRow in gvGASProposed.Rows)
    //        {
    //            Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");
    //            Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
    //            Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");
    //            Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");
    //            Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");
    //            Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");
    //            Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");
    //            Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");
    //            Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");
    //            Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");
    //            Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
    //            Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
    //            Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

    //            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure obj_indNewProposedGWAS = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure();

    //            obj_indNewProposedGWAS.IndustrialNewApplicationCode = Convert.ToInt32(gvGASProposed.DataKeys[gvRow.RowIndex].Value.ToString());

    //            if (lbl_TypeOfAbstractionStructureCode.Text == "")
    //            {
    //                obj_indNewProposedGWAS.TypeOfAbstractionStructureCode = 0;
    //            }
    //            else
    //            {
    //                obj_indNewProposedGWAS.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text);
    //            }
    //            if (lbl_YearOfConstruction.Text.Trim() == "")
    //            {
    //                obj_indNewProposedGWAS.YearOfConstruction = null;
    //            }
    //            else
    //            {
    //                obj_indNewProposedGWAS.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
    //            }
    //            if (lbl_DepthExist.Text.Trim() == "")
    //            {
    //                obj_indNewProposedGWAS.DepthExist = null;
    //            }
    //            else
    //            {
    //                obj_indNewProposedGWAS.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
    //            }
    //            if (lbl_Diameter.Text.Trim() == "")
    //            {
    //                obj_indNewProposedGWAS.Diameter = null;
    //            }
    //            else
    //            {
    //                obj_indNewProposedGWAS.Diameter = Convert.ToInt32(lbl_Diameter.Text);
    //            }
    //            if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
    //            {
    //                obj_indNewProposedGWAS.DepthBelowWaterLevel = null;
    //            }
    //            else
    //            {
    //                obj_indNewProposedGWAS.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
    //            }
    //            if (lbl_Discharge.Text.Trim() == "")
    //            {
    //                obj_indNewProposedGWAS.Discharge = null;
    //            }
    //            else
    //            {
    //                obj_indNewProposedGWAS.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
    //            }
    //            if (lbl_OperationalHousrDay.Text.Trim() == "")
    //            {
    //                obj_indNewProposedGWAS.OperationalHousrDay = null;
    //            }
    //            else
    //            {
    //                obj_indNewProposedGWAS.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
    //            }
    //            if (lbl_OperationalDaysYear.Text.Trim() == "")
    //            {
    //                obj_indNewProposedGWAS.OperationalDaysYear = null;
    //            }
    //            else
    //            {
    //                obj_indNewProposedGWAS.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
    //            }
    //            if (lbl_LiftingDeviceCode.Text.Trim() == "")
    //            {
    //                obj_indNewProposedGWAS.LiftingDeviceCode = null;
    //            }
    //            else
    //            {
    //                obj_indNewProposedGWAS.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
    //            }
    //            if (lbl_PowerOfPump.Text.Trim() == "")
    //            {
    //                obj_indNewProposedGWAS.PowerOfPump = null;
    //            }
    //            else
    //            {
    //                obj_indNewProposedGWAS.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
    //            }
    //            switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
    //            {
    //                case "":
    //                    obj_indNewProposedGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

    //                    break;
    //                case "Yes":
    //                    obj_indNewProposedGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

    //                    break;
    //                case "No":
    //                    obj_indNewProposedGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

    //                    break;
    //            }
    //            switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
    //            {
    //                case "":
    //                    obj_indNewProposedGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

    //                    break;
    //                case "Yes":
    //                    obj_indNewProposedGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

    //                    break;
    //                case "No":
    //                    obj_indNewProposedGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

    //                    break;
    //            }
    //            if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
    //            {
    //                obj_indNewProposedGWAS.WhetherPermissionRegisteredWithCGWADetails = null;
    //            }
    //            else
    //            {
    //                obj_indNewProposedGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
    //            }

    //            obj_indNewProposedGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;

    //            lst_indNewProposedGWASList.Add(obj_indNewProposedGWAS);
    //        }



    //        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure[] arr_tempIndGwasProposedListBLL = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure[lst_indNewProposedGWASList.Count];
    //        lst_indNewProposedGWASList.CopyTo(arr_tempIndGwasProposedListBLL);
            
    //        obj_industrialNewApplication.IndustrialNewProposedGroundWaterAbstractionStructureList = arr_tempIndGwasProposedListBLL;

    //        if (txtNumbeOfProposedGAS.Text.Trim() == "")
    //        {
    //            obj_industrialNewApplication.NumberOfStructureProposed = null;
    //        }
    //        else
    //        {
    //            obj_industrialNewApplication.NumberOfStructureProposed = Convert.ToInt32(txtNumbeOfProposedGAS.Text.Trim());
    //        }

    //        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
    //        obj_industrialNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

    //        if (obj_industrialNewApplication.Update() == 1)
    //        {
    //            strStatus = "Update Success";
    //            lblMessage.Text = "Successfully Saved";
    //            lblMessage.ForeColor = System.Drawing.Color.Green;
    //            return 1;
    //        }
    //        else
    //        {
    //            strStatus = "Update Failed";
    //            lblMessage.Text = obj_industrialNewApplication.CustumMessage;
    //            lblMessage.ForeColor = System.Drawing.Color.Red;
    //            return 0;
    //        }

    //    }
    //    catch (Exception)
    //    {
    //        Response.Redirect("~/ExternalErrorPage.aspx", false);
    //        return 0;
    //    }
    //    finally
    //    {
    //        ActionTrail obj_ExtActionTrail = new ActionTrail();
    //        if (Session["ExternalUserCode"] != null)
    //        {
    //            obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
    //            obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
    //            obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
    //            obj_ExtActionTrail.Status = strStatus;
    //            if (obj_ExtActionTrail != null)
    //                ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
    //        }
    //    }
    //}
    //private void BindIGVIndNewProposedGWASDetails(long lngA_ApplicationCode)
    //{
    //    try
    //    {

    //        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(lngA_ApplicationCode);

    //        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure obj_indNewProposedGWASBlank = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure();

    //        List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure> lst_indNewProposedGWASBlank = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure>();

    //        List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure> lst_indNewProposedGWAS = new List<NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure>();


    //        txtNumbeOfProposedGAS.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.NumberOfStructureProposed));
    //        if (txtNumbeOfProposedGAS.Text == "") { txtNumbeOfProposedGAS.Text = "0"; }
    //        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure[] arr_indNewProposedGWAS;
    //        arr_indNewProposedGWAS = obj_industrialNewApplication.GetIndustrialNewProposedGroundWaterAbstractionStructureList();

    //        foreach (NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADProposedGroundWaterAbstractionStructure test in arr_indNewProposedGWAS)
    //        {
    //            lst_indNewProposedGWAS.Add(test);
    //        }

    //        gvGASProposed.DataSource = lst_indNewProposedGWAS;
    //        gvGASProposed.DataBind();
            

    //    }
    //    catch (Exception)
    //    {
    //        Response.Redirect("~/ExternalErrorPage.aspx", false);
    //    }


    //}

}