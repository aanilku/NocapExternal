using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_Expansion_IND_DetailsProposedGroundwaterAbstractionStructure : System.Web.UI.Page
{
    string strPageName = "INDProposedGroundWaterAbstractionStructure";
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
                if (lblIndustialApplicationCodeFrom.Text.Trim() != "")
                {

                    BindIGVIndNewProposedGWASDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
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

    private void BindIGVIndNewProposedGWASDetails(long lngA_ApplicationCode)
    {
        try
        {

            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(lngA_ApplicationCode);

            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure obj_indNewProposedGWASBlank = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure();

            List<NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure> lst_indNewProposedGWASBlank = new List<NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure>();

            List<NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure> lst_indNewProposedGWAS = new List<NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure>();


            txtNumbeOfProposedGAS.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.NumberOfStructureProposed));
            if (txtNumbeOfProposedGAS.Text == "") { txtNumbeOfProposedGAS.Text = "0"; }
            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure[] arr_indNewProposedGWAS;
            arr_indNewProposedGWAS = obj_industrialNewApplication.GetIndustrialNewProposedGroundWaterAbstractionStructureList();

            foreach (NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure test in arr_indNewProposedGWAS)
            {
                lst_indNewProposedGWAS.Add(test);
            }
            
               gvGASProposed.DataSource = lst_indNewProposedGWAS;
               gvGASProposed.DataBind();
          

        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
            //lblMessage.ForeColor = System.Drawing.Color.Red;
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

                    UpdateIndExpansionProposedGWASDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));

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
                if (UpdateIndExpansionProposedGWASDetails(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text)) == 1)
                {
                    //if (Session["OnePage"].ToString() == "1")
                    //    Server.Transfer("Attachment.aspx");
                    //else
                       
                    Server.Transfer("OtherDetails.aspx");
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



                NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure obj_indNewProposedGWASBlankForGridToObj = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure();
                List<NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure> lst_indNewProposedGWASNewList = new List<NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure>();


                foreach (GridViewRow gvRow in gvGASProposed.Rows)
                {

                    obj_indNewProposedGWASBlankForGridToObj = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure();

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
                obj_indNewProposedGWASBlankForGridToObj = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure();
                obj_indNewProposedGWASBlankForGridToObj.IndustrialNewApplicationCode = Convert.ToInt64(lblIndustialApplicationCodeFrom.Text);

                obj_indNewProposedGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(ddlTypeOfStructure.SelectedValue);

                if (txtYearOfConstruction.Text.Trim() == "")
                {
                    obj_indNewProposedGWASBlankForGridToObj.YearOfConstruction = null;
                }
                else
                {
                    obj_indNewProposedGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt32(txtYearOfConstruction.Text.Trim());
                }
                if (txtDepthMeter.Text.Trim() == "")
                {
                    obj_indNewProposedGWASBlankForGridToObj.DepthExist = null;
                }
                else
                {
                    obj_indNewProposedGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(txtDepthMeter.Text.Trim());
                }
                if (txtDiameterMM.Text.Trim() == "")
                {
                    obj_indNewProposedGWASBlankForGridToObj.Diameter = null;
                }
                else
                {
                    obj_indNewProposedGWASBlankForGridToObj.Diameter = Convert.ToInt32(txtDiameterMM.Text.Trim());
                }

                if (txtDepthWaterBelowGroundLevel.Text.Trim() == "")
                {
                    obj_indNewProposedGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_indNewProposedGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(txtDepthWaterBelowGroundLevel.Text.Trim());
                }
                if (txtDischarge.Text.Trim() == "")
                {
                    obj_indNewProposedGWASBlankForGridToObj.Discharge = null;
                }
                else
                {
                    obj_indNewProposedGWASBlankForGridToObj.Discharge = Convert.ToDecimal(txtDischarge.Text.Trim());
                }
                if (txtOperationalHoursDay.Text.Trim() == "")
                {
                    obj_indNewProposedGWASBlankForGridToObj.OperationalHousrDay = null;
                }
                else
                {
                    obj_indNewProposedGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(txtOperationalHoursDay.Text.Trim());
                }

                if (txtOperationalDaysYear.Text.Trim() == "")
                {
                    obj_indNewProposedGWASBlankForGridToObj.OperationalDaysYear = null;
                }
                else
                {
                    obj_indNewProposedGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(txtOperationalDaysYear.Text.Trim());
                }

                if (ddlModeOfLift.SelectedValue == "")
                {
                    obj_indNewProposedGWASBlankForGridToObj.LiftingDeviceCode = null;
                }
                else
                {
                    obj_indNewProposedGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(ddlModeOfLift.SelectedValue);
                }
                if (txtHorsePowerPump.Text.Trim() == "")
                {
                    obj_indNewProposedGWASBlankForGridToObj.PowerOfPump = null;
                }
                else
                {
                    obj_indNewProposedGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(txtHorsePowerPump.Text.Trim());
                }

                switch (ddlWhetherFittedWithWaterMeter.SelectedValue)
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

                switch (ddlWhetherPermissionRegisteredWithCGWA.SelectedValue)
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
                if (txtWhetherPermissionRegisteredWithCGWADet.Text.Trim() == "")
                {
                    obj_indNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_indNewProposedGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = Convert.ToString(txtWhetherPermissionRegisteredWithCGWADet.Text.Trim());
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
    protected void gvGASProposed_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure obj_indNewProposedGWAS = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure();

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
                    obj_indNewProposedGWAS = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure(Convert.ToInt64(gvGASProposed.DataKeys[e.Row.RowIndex].Values[0].ToString()), Convert.ToInt32(lbl_SerialNumber.Text));

                    lbl_TypeOfAbstractionStructureName.Text = HttpUtility.HtmlEncode(obj_indNewProposedGWAS.GetTypeOfAbstractionStructure().TypeOfAbstractionStructureDesc);

                    if (obj_indNewProposedGWAS.GetLiftingDevice() != null)
                    {

                        lbl_LiftingDeviceName.Text = HttpUtility.HtmlEncode(obj_indNewProposedGWAS.GetLiftingDevice().LiftingDeviceDesc);

                        lbl_LiftingDeviceCode.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_indNewProposedGWAS.GetLiftingDevice().LiftingDeviceCode));
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

    private int UpdateIndExpansionProposedGWASDetails(long lngA_ApplicationCode)
    {
        try
        {
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
            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(lngA_ApplicationCode);

            List<NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure> lst_indNewProposedGWASList = new List<NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure>();

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

                NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure obj_indNewProposedGWAS = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure();

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



            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure[] arr_tempIndGwasProposedListBLL = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure[lst_indNewProposedGWASList.Count];
            lst_indNewProposedGWASList.CopyTo(arr_tempIndGwasProposedListBLL);
            // obj_industrialNewApplication.IndustrialNewProposedGroundWaterAbstractionStructureList = arr_tempIndGwasProposedListBLL;

            obj_industrialNewApplication.IndustrialNewProposedGroundWaterAbstractionStructureList = arr_tempIndGwasProposedListBLL;

            if (txtNumbeOfProposedGAS.Text.Trim() == "")
            {
                obj_industrialNewApplication.NumberOfStructureProposed = null;
            }
            else
            {
                obj_industrialNewApplication.NumberOfStructureProposed = Convert.ToInt32(txtNumbeOfProposedGAS.Text.Trim());
            }

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
                lblMessage.Text = obj_industrialNewApplication.CustumMessage;
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
                    NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure obj_indNewProposedGWASBlankForGridToObj = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure();
                    List<NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure> lst_indNewProposedGWASNewList = new List<NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure>();


                    foreach (GridViewRow gvRow in gvGASProposed.Rows)
                    {

                        obj_indNewProposedGWASBlankForGridToObj = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionProposedGroundWaterAbstractionStructure();

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