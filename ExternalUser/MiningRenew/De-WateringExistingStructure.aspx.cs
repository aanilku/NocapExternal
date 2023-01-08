using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_MiningRenew_De_WateringExistingStructure : System.Web.UI.Page
{
    string strPageName = "MINRenDe-WateringExistingStructure";
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


                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    BindDeWateringExistingStructureDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
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



        revtxtGWRequirementAbstractStructExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtGWRequirementAbstractStructExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revatxtGWRequirementAbstractStructYearExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revatxtGWRequirementAbstractStructYearExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        revtxtGWrequiredMiningSeepingExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtGWrequiredMiningSeepingExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtGWrequiredMiningSeepingYearExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtGWrequiredMiningSeepingYearExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");



        // additional

        revtxtGWRequirementAbstractStructAddit.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtGWRequirementAbstractStructAddit.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revatxtGWRequirementAbstractStructYearAddit.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revatxtGWRequirementAbstractStructYearAddit.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");
        revtxtGroundWaterRequirementYear.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtGroundWaterRequirementYear.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        revtxtGWrequiredMiningSeepingAddit.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtGWrequiredMiningSeepingAddit.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");

        revtxtGWrequiredMiningSeepingYearAddit.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtGWrequiredMiningSeepingYearAddit.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

        // end additional 


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
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
        NOCAP.BLL.Mining.New.Application.MiningNewApplication objMiningNewApplication = obj_MiningRenewApplication.GetFirstMiningApplication();


        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure obj_minRenewExistingDeWateringBlank = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure();
        List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure> lst_minRenewExistingDeWateringBlank = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure>();
        List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure> lst_minRenewExistingDeWatering = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure>();

        try
        {
            if (objMiningNewApplication != null)
            {
                lblHeadingProjName.Text = HttpUtility.HtmlEncode(objMiningNewApplication.NameOfMining);
            }

            switch (obj_MiningRenewApplication.MiningRenewSADDewatering.WhetherGWTableIntersected)
            {
                case NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADDewatering.GWTableIntersected.Yes:
                    ddlDewateringRequired.SelectedValue = "Yes";
                    break;
                case NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADDewatering.GWTableIntersected.No:
                    ddlDewateringRequired.SelectedValue = "No";
                    break;
            }

            txtNumberOfExistingDewaterStruct.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.DewateringNoOfExistingStructure));
            if (txtNumberOfExistingDewaterStruct.Text == "") { txtNumberOfExistingDewaterStruct.Text = "0"; }
            txtMinimumPremansoon.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.MiningRenewSADDewatering.GWTableDepthMinPreMansoon));
            txtMinimunPostMansoon.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.MiningRenewSADDewatering.GWTableDepthMinPostMansoon));

            txtMaximumPreMansoon.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.MiningRenewSADDewatering.GWTableDepthMaxPreMansoon));
            txtMaximunPostMansoon.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.MiningRenewSADDewatering.GWTableDepthMaxPostMansoon));

            txtmaximumDeptProposed.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.MiningRenewSADDewatering.MaxDepthPrposedtoDewater));
            txtGWFlowDirection.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.MiningRenewSADDewatering.GWFlowDirection));
            txtOtherInformation.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.MiningRenewSADDewatering.AnyOtherInformation));

            txtGWRequirementAbstractStructExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.GWREquiredThroughAbstractStructureExisting));
            txtGWRequirementAbstractStructYearExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.GWREquiredThroughAbstractStructureYearExisting));
            txtGWrequiredMiningSeepingExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.GWRequiredThroughMiningSeepingExisting));
            txtGWrequiredMiningSeepingYearExist.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.GWRequiredThroughMiningSeepingYearExisting));


            txtGWRequirementAbstractStructAddit.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.GWREquiredThroughAbstractStructureAdditional));
            txtGWRequirementAbstractStructYearAddit.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.GWREquiredThroughAbstractStructureYearAdditional));
            txtGroundWaterRequirementYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.GroundWaterReqInYear));


            txtGWrequiredMiningSeepingAddit.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.GWRequiredThroughMiningSeepingAdditional));
            txtGWrequiredMiningSeepingYearAddit.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.GWRequiredThroughMiningSeepingYearAdditional));


            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure[] arr_minRenewExistingDeWatering;
            arr_minRenewExistingDeWatering = obj_MiningRenewApplication.GetMiningRenewSADExistingDeWateringAbstractionStructureList();
            foreach (NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure test in arr_minRenewExistingDeWatering)
            {
                lst_minRenewExistingDeWatering.Add(test);
            }
            gvdeWateringExisting.DataSource = lst_minRenewExistingDeWatering;
            gvdeWateringExisting.DataBind();
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
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure obj_minRenewExistingDeWateringBlankForGridToObj = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure();
                List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure> lst_minRenewExistingDeWateringList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure>();
                foreach (GridViewRow gvRow in gvdeWateringExisting.Rows)
                {
                    obj_minRenewExistingDeWateringBlankForGridToObj = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure();
                    obj_minRenewExistingDeWateringBlankForGridToObj.MiningRenewApplicationCode = Convert.ToInt64(gvdeWateringExisting.DataKeys[gvRow.RowIndex].Value.ToString());
                    int ss = gvRow.RowIndex;
                    if (Convert.ToInt64(gvdeWateringExisting.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                    {
                        Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                        if (lbl_SerialNumber.Text.Trim() == "")
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.SerialNumber = 0;
                        }
                        else
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                        }

                        Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");

                        if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                        }
                        else
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                        }
                        Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                        if (lbl_YearOfConstruction.Text.Trim() == "")
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.YearOfConstruction = null;
                        }
                        else
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                        }
                        Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                        if (lbl_DepthExist.Text.Trim() == "")
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.DepthExist = null;
                        }
                        else
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                        }
                        Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                        if (lbl_Diameter.Text.Trim() == "")
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.Diameter = null;
                        }
                        else
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                        }
                        Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                        if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.DepthBelowWaterLevel = null;
                        }
                        else
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                        }
                        Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                        if (lbl_Discharge.Text.Trim() == "")
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.Discharge = null;
                        }
                        else
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                        }
                        Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                        if (lbl_OperationalHousrDay.Text.Trim() == "")
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.OperationalHousrDay = null;
                        }
                        else
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                        }

                        Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                        if (lbl_OperationalDaysYear.Text.Trim() == "")
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.OperationalDaysYear = null;
                        }
                        else
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                        }
                        Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                        if (lbl_LiftingDeviceCode.Text.Trim() == "")
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.LiftingDeviceCode = null;
                        }
                        else
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                        }
                        Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                        if (lbl_PowerOfPump.Text.Trim() == "")
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.PowerOfPump = null;
                        }
                        else
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                        }

                        Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                        switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                        {
                            case "":
                                obj_minRenewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                break;
                            case "Yes":
                                obj_minRenewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                break;
                            case "No":
                                obj_minRenewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                break;
                        }
                        Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                        switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                        {
                            case "":
                                obj_minRenewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                break;
                            case "Yes":
                                obj_minRenewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                break;
                            case "No":
                                obj_minRenewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                break;
                        }

                        Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                        if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                        }
                        else
                        {
                            obj_minRenewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                        }

                        lst_minRenewExistingDeWateringList.Add(obj_minRenewExistingDeWateringBlankForGridToObj);


                    }
                }
                //---------------

                obj_minRenewExistingDeWateringBlankForGridToObj = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure();
                obj_minRenewExistingDeWateringBlankForGridToObj.MiningRenewApplicationCode = Convert.ToInt64(lblMiningApplicationCodeFrom.Text);

                if (NOCAPExternalUtility.IsNumeric(ddlTypeOfStructure.SelectedValue)) { obj_minRenewExistingDeWateringBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(ddlTypeOfStructure.SelectedValue); } else { return; }

                if (txtYearOfConstruction.Text.Trim() == "")
                {
                    obj_minRenewExistingDeWateringBlankForGridToObj.YearOfConstruction = null;
                }
                else
                {
                    obj_minRenewExistingDeWateringBlankForGridToObj.YearOfConstruction = Convert.ToInt32(txtYearOfConstruction.Text.Trim());
                }
                if (txtDepthMeter.Text.Trim() == "")
                {
                    obj_minRenewExistingDeWateringBlankForGridToObj.DepthExist = null;
                }
                else
                {
                    obj_minRenewExistingDeWateringBlankForGridToObj.DepthExist = Convert.ToDecimal(txtDepthMeter.Text.Trim());
                }
                if (txtDiameterMM.Text.Trim() == "")
                {
                    obj_minRenewExistingDeWateringBlankForGridToObj.Diameter = null;
                }
                else
                {
                    obj_minRenewExistingDeWateringBlankForGridToObj.Diameter = Convert.ToInt32(txtDiameterMM.Text.Trim());
                }

                if (txtDepthWaterBelowGroundLevel.Text.Trim() == "")
                {
                    obj_minRenewExistingDeWateringBlankForGridToObj.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_minRenewExistingDeWateringBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(txtDepthWaterBelowGroundLevel.Text.Trim());
                }
                if (txtDischarge.Text.Trim() == "")
                {
                    obj_minRenewExistingDeWateringBlankForGridToObj.Discharge = null;
                }
                else
                {
                    obj_minRenewExistingDeWateringBlankForGridToObj.Discharge = Convert.ToDecimal(txtDischarge.Text.Trim());
                }
                if (txtOperationalHoursDay.Text.Trim() == "")
                {
                    obj_minRenewExistingDeWateringBlankForGridToObj.OperationalHousrDay = null;
                }
                else
                {
                    obj_minRenewExistingDeWateringBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(txtOperationalHoursDay.Text.Trim());
                }

                if (txtOperationalDaysYear.Text.Trim() == "")
                {
                    obj_minRenewExistingDeWateringBlankForGridToObj.OperationalDaysYear = null;
                }
                else
                {
                    obj_minRenewExistingDeWateringBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(txtOperationalDaysYear.Text.Trim());
                }

                if (ddlModeOfLift.SelectedValue == "")
                {
                    obj_minRenewExistingDeWateringBlankForGridToObj.LiftingDeviceCode = null;
                }
                else
                {
                    if (NOCAPExternalUtility.IsNumeric(ddlModeOfLift.SelectedValue)) { obj_minRenewExistingDeWateringBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(ddlModeOfLift.SelectedValue); }
                }
                if (txtHorsePowerPump.Text.Trim() == "")
                {
                    obj_minRenewExistingDeWateringBlankForGridToObj.PowerOfPump = null;
                }
                else
                {
                    obj_minRenewExistingDeWateringBlankForGridToObj.PowerOfPump = Convert.ToDecimal(txtHorsePowerPump.Text.Trim());
                }

                switch (ddlWhetherFittedWithWaterMeter.SelectedValue)
                {
                    case "":
                        obj_minRenewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_minRenewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_minRenewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }

                switch (ddlWhetherPermissionRegisteredWithCGWA.SelectedValue)
                {
                    case "":
                        obj_minRenewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_minRenewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_minRenewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (txtWhetherPermissionRegisteredWithCGWADet.Text.Trim() == "")
                {
                    obj_minRenewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_minRenewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = Convert.ToString(txtWhetherPermissionRegisteredWithCGWADet.Text.Trim());
                }

                lst_minRenewExistingDeWateringList.Add(obj_minRenewExistingDeWateringBlankForGridToObj);

                gvdeWateringExisting.DataSource = lst_minRenewExistingDeWateringList;
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
                UpdateminRenewExistingDeWateringDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
            }
        }
    }

    private int UpdateminRenewExistingDeWateringDetails(long lngA_ApplicationCode)
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
            decimal GWRequirement = Convert.ToDecimal(txtGWRequirementAbstractStructExist.Text == "" ? "0" : txtGWRequirementAbstractStructExist.Text) + Convert.ToDecimal(txtGWrequiredMiningSeepingExist.Text == "" ? "0" : txtGWrequiredMiningSeepingExist.Text);
            if (GWRequirement == 0)
            {
                lblMessage.Text = "14.(Ground Water required through Abstract Structure + Ground Water required through Dewatering / Mining Seeping) can not be Zero.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }



            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure> lst_minRenewExistingDeWateringList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure>();
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

                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure obj_minRenewExistingDeWatering = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure();
                obj_minRenewExistingDeWatering.MiningRenewApplicationCode = Convert.ToInt32(gvdeWateringExisting.DataKeys[gvRow.RowIndex].Value.ToString());

                if (lbl_TypeOfAbstractionStructureCode.Text == "") { obj_minRenewExistingDeWatering.TypeOfAbstractionStructureCode = 0; }
                else { obj_minRenewExistingDeWatering.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text); }

                if (lbl_YearOfConstruction.Text.Trim() == "")
                {
                    obj_minRenewExistingDeWatering.YearOfConstruction = null;
                }
                else
                {
                    obj_minRenewExistingDeWatering.YearOfConstruction = Convert.ToInt32(lbl_YearOfConstruction.Text);
                }

                if (lbl_DepthExist.Text.Trim() == "")
                {
                    obj_minRenewExistingDeWatering.DepthExist = null;
                }
                else
                {
                    obj_minRenewExistingDeWatering.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                }
                if (lbl_Diameter.Text.Trim() == "")
                {
                    obj_minRenewExistingDeWatering.Diameter = null;
                }
                else
                {
                    obj_minRenewExistingDeWatering.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                }
                if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                {
                    obj_minRenewExistingDeWatering.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_minRenewExistingDeWatering.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                }
                if (lbl_Discharge.Text.Trim() == "")
                {
                    obj_minRenewExistingDeWatering.Discharge = null;
                }
                else
                {
                    obj_minRenewExistingDeWatering.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                }
                if (lbl_OperationalHousrDay.Text.Trim() == "")
                {
                    obj_minRenewExistingDeWatering.OperationalHousrDay = null;
                }
                else
                {
                    obj_minRenewExistingDeWatering.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                }
                if (lbl_OperationalDaysYear.Text.Trim() == "")
                {
                    obj_minRenewExistingDeWatering.OperationalDaysYear = null;
                }
                else
                {
                    obj_minRenewExistingDeWatering.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                }
                if (lbl_LiftingDeviceCode.Text.Trim() == "")
                {
                    obj_minRenewExistingDeWatering.LiftingDeviceCode = null;
                }
                else
                {
                    obj_minRenewExistingDeWatering.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                }
                if (lbl_PowerOfPump.Text.Trim() == "")
                {
                    obj_minRenewExistingDeWatering.PowerOfPump = null;
                }
                else
                {
                    obj_minRenewExistingDeWatering.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                }
                switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                {
                    case "":
                        obj_minRenewExistingDeWatering.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_minRenewExistingDeWatering.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_minRenewExistingDeWatering.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }
                switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                {
                    case "":
                        obj_minRenewExistingDeWatering.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_minRenewExistingDeWatering.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_minRenewExistingDeWatering.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                {
                    obj_minRenewExistingDeWatering.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_minRenewExistingDeWatering.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                }

                obj_minRenewExistingDeWatering.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;

                lst_minRenewExistingDeWateringList.Add(obj_minRenewExistingDeWatering);
            }


            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure[] arr_tempMinDeWateringExistingListBLL = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure[lst_minRenewExistingDeWateringList.Count];
            lst_minRenewExistingDeWateringList.CopyTo(arr_tempMinDeWateringExistingListBLL);
            obj_miningRenewApplication.MiningRenewExistingDeWateringAbstractionStructureList = arr_tempMinDeWateringExistingListBLL;


            switch (ddlDewateringRequired.SelectedValue)
            {
                case "Yes":
                    obj_miningRenewApplication.MiningRenewSADDewatering.WhetherGWTableIntersected = NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADDewatering.GWTableIntersected.Yes;
                    break;
                case "No":
                    obj_miningRenewApplication.MiningRenewSADDewatering.WhetherGWTableIntersected = NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADDewatering.GWTableIntersected.No;
                    break;
            }

            if (txtMinimumPremansoon.Text == "") { obj_miningRenewApplication.MiningRenewSADDewatering.GWTableDepthMinPreMansoon = null; }
            else { obj_miningRenewApplication.MiningRenewSADDewatering.GWTableDepthMinPreMansoon = Convert.ToDecimal(txtMinimumPremansoon.Text.Trim()); }

            if (txtMinimunPostMansoon.Text == "") { obj_miningRenewApplication.MiningRenewSADDewatering.GWTableDepthMinPostMansoon = null; }
            else { obj_miningRenewApplication.MiningRenewSADDewatering.GWTableDepthMinPostMansoon = Convert.ToDecimal(txtMinimunPostMansoon.Text.Trim()); }

            if (txtMaximumPreMansoon.Text == "") { obj_miningRenewApplication.MiningRenewSADDewatering.GWTableDepthMaxPreMansoon = null; }
            else { obj_miningRenewApplication.MiningRenewSADDewatering.GWTableDepthMaxPreMansoon = Convert.ToDecimal(txtMaximumPreMansoon.Text.Trim()); }

            if (txtMaximunPostMansoon.Text == "") { obj_miningRenewApplication.MiningRenewSADDewatering.GWTableDepthMaxPostMansoon = null; }
            else { obj_miningRenewApplication.MiningRenewSADDewatering.GWTableDepthMaxPostMansoon = Convert.ToDecimal(txtMaximunPostMansoon.Text.Trim()); }

            if (txtmaximumDeptProposed.Text == "") { obj_miningRenewApplication.MiningRenewSADDewatering.MaxDepthPrposedtoDewater = null; }
            else { obj_miningRenewApplication.MiningRenewSADDewatering.MaxDepthPrposedtoDewater = Convert.ToDecimal(txtmaximumDeptProposed.Text.Trim()); }

            if (txtGWFlowDirection.Text == "") { obj_miningRenewApplication.MiningRenewSADDewatering.GWFlowDirection = null; }
            else { obj_miningRenewApplication.MiningRenewSADDewatering.GWFlowDirection = txtGWFlowDirection.Text.Trim(); }

            if (txtOtherInformation.Text == "") { obj_miningRenewApplication.MiningRenewSADDewatering.AnyOtherInformation = null; }
            else { obj_miningRenewApplication.MiningRenewSADDewatering.AnyOtherInformation = Convert.ToString(txtOtherInformation.Text.Trim()); }



            if (txtGWRequirementAbstractStructExist.Text == "") { obj_miningRenewApplication.GWREquiredThroughAbstractStructureExisting = null; }
            else { obj_miningRenewApplication.GWREquiredThroughAbstractStructureExisting = Convert.ToDecimal(txtGWRequirementAbstractStructExist.Text.Trim()); }

            if (txtGWRequirementAbstractStructYearExist.Text == "") { obj_miningRenewApplication.GWREquiredThroughAbstractStructureYearExisting = null; }
            else { obj_miningRenewApplication.GWREquiredThroughAbstractStructureYearExisting = Convert.ToDecimal(txtGWRequirementAbstractStructYearExist.Text.Trim()); }

            if (txtGWrequiredMiningSeepingExist.Text == "") { obj_miningRenewApplication.GWRequiredThroughMiningSeepingExisting = null; }
            else { obj_miningRenewApplication.GWRequiredThroughMiningSeepingExisting = Convert.ToDecimal(txtGWrequiredMiningSeepingExist.Text.Trim()); }

            if (txtGWrequiredMiningSeepingYearExist.Text == "") { obj_miningRenewApplication.GWRequiredThroughMiningSeepingYearExisting = null; }
            else { obj_miningRenewApplication.GWRequiredThroughMiningSeepingYearExisting = Convert.ToDecimal(txtGWrequiredMiningSeepingYearExist.Text.Trim()); }

            // Additional

            if (txtGWRequirementAbstractStructAddit.Text == "") { obj_miningRenewApplication.GWREquiredThroughAbstractStructureAdditional = null; }
            else { obj_miningRenewApplication.GWREquiredThroughAbstractStructureAdditional = Convert.ToDecimal(txtGWRequirementAbstractStructAddit.Text.Trim()); }

            if (txtGWRequirementAbstractStructYearAddit.Text == "") { obj_miningRenewApplication.GWREquiredThroughAbstractStructureYearAdditional = null; }
            else { obj_miningRenewApplication.GWREquiredThroughAbstractStructureYearAdditional = Convert.ToDecimal(txtGWRequirementAbstractStructYearAddit.Text.Trim()); }

            if (txtGroundWaterRequirementYear.Text == "") { obj_miningRenewApplication.GroundWaterReqInYear = null; }
            else { obj_miningRenewApplication.GroundWaterReqInYear = Convert.ToDecimal(txtGroundWaterRequirementYear.Text.Trim()); }

            if (txtGWrequiredMiningSeepingAddit.Text == "") { obj_miningRenewApplication.GWRequiredThroughMiningSeepingAdditional = null; }
            else { obj_miningRenewApplication.GWRequiredThroughMiningSeepingAdditional = Convert.ToDecimal(txtGWrequiredMiningSeepingAddit.Text.Trim()); }

            if (txtGWrequiredMiningSeepingYearAddit.Text == "") { obj_miningRenewApplication.GWRequiredThroughMiningSeepingYearAdditional = null; }
            else { obj_miningRenewApplication.GWRequiredThroughMiningSeepingYearAdditional = Convert.ToDecimal(txtGWrequiredMiningSeepingYearAddit.Text.Trim()); }

            // end additional


            if (txtNumberOfExistingDewaterStruct.Text.Trim() == "") { obj_miningRenewApplication.NumberOfStructureExisting = null; }
            else { obj_miningRenewApplication.DewateringNoOfExistingStructure = Convert.ToInt32(txtNumberOfExistingDewaterStruct.Text.Trim()); }

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_miningRenewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_miningRenewApplication.Update() == 1)
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

                lblMessage.Text = obj_miningRenewApplication.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                BindDeWateringExistingStructureDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
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
            try
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                if (Page.IsValid)
                {
                    if (UpdateminRenewExistingDeWateringDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)) == 1)
                    {
                        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                        decimal dec_netGroundWaterRequirement = Convert.ToDecimal(txtGWRequirementAbstractStructExist.Text == "" ? "0" : txtGWRequirementAbstractStructExist.Text) + Convert.ToDecimal(txtGWRequirementAbstractStructAddit.Text == "" ? "0" : txtGWRequirementAbstractStructAddit.Text) + Convert.ToDecimal(txtGWrequiredMiningSeepingExist.Text == "" ? "0" : txtGWrequiredMiningSeepingExist.Text) + Convert.ToDecimal(txtGWrequiredMiningSeepingAddit.Text == "" ? "0" : txtGWrequiredMiningSeepingAddit.Text);
                        if (obj_miningRenewApplication.MSME == NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.MSMEYesNo.Yes && dec_netGroundWaterRequirement < 10 && obj_miningRenewApplication.MSMETypeCode != 3)
                        {
                            Server.Transfer("SalientFeature.aspx");

                        }
                        else
                        {
                            Server.Transfer("De-WateringAdditionalStructure.aspx");
                        }

                    }
                }
            }
            catch(System.Threading.ThreadAbortException ex)
            {

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
                Server.Transfer("~/ExternalUser/MiningRenew/LandUseDetails.aspx");
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

            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));


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
                txtNumberOfExistingDewaterStruct.Text = "0";
                rfvtxtNumberOfExistingDewaterStruct.Enabled = false;



                txtGWrequiredMiningSeepingExist.Text = "";
                txtGWrequiredMiningSeepingExist.Enabled = false;
                rfvtxtGWrequiredMiningSeepingExist.Enabled = false;

                txtGWrequiredMiningSeepingAddit.Text = "";
                txtGWrequiredMiningSeepingAddit.Enabled = false;
                rfvtxtGWrequiredMiningSeepingAddit.Enabled = false;


                txtGWrequiredMiningSeepingYearExist.Text = "";
                txtGWrequiredMiningSeepingYearExist.Enabled = false;
                rfvtxtGWrequiredMiningSeepingYearExist.Enabled = false;


                txtGWrequiredMiningSeepingYearAddit.Text = "";
                txtGWrequiredMiningSeepingYearAddit.Enabled = false;
                rfvtxtGWrequiredMiningSeepingYearAddit.Enabled = false;



                switch (obj_MiningRenewApplication.GroundWaterUtilizationFor)
                {
                    case NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.GroundWaterUtilizationForOption.ExistingGroundWater:
                        txtGWRequirementAbstractStructAddit.Text = "";
                        txtGWRequirementAbstractStructAddit.Enabled = false;
                        rfvtxtGWRequirementAbstractStructAddit.Enabled = false;

                        txtGWRequirementAbstractStructYearAddit.Text = "";
                        txtGWRequirementAbstractStructYearAddit.Enabled = false;
                        rfvtxtGWRequirementAbstractStructYearAddit.Enabled = false;

                        break;
                    case NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.GroundWaterUtilizationForOption.ExistingGWWithAdditionalGWRequirment:

                        txtGWRequirementAbstractStructAddit.Enabled = true;
                        rfvtxtGWRequirementAbstractStructAddit.Enabled = true;

                        txtGWRequirementAbstractStructYearAddit.Enabled = true;
                        rfvtxtGWRequirementAbstractStructYearAddit.Enabled = true;

                        break;
                }

                ClearDeWateringExistingScreen();
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
                rfvtxtNumberOfExistingDewaterStruct.Enabled = true;



                txtGWrequiredMiningSeepingExist.Enabled = true;
                rfvtxtGWrequiredMiningSeepingExist.Enabled = true;

                txtGWrequiredMiningSeepingYearExist.Enabled = true;
                rfvtxtGWrequiredMiningSeepingYearExist.Enabled = true;


                switch (obj_MiningRenewApplication.GroundWaterUtilizationFor)
                {
                    case NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.GroundWaterUtilizationForOption.ExistingGroundWater:

                        txtGWRequirementAbstractStructAddit.Text = "";
                        txtGWRequirementAbstractStructAddit.Enabled = false;
                        rfvtxtGWRequirementAbstractStructAddit.Enabled = false;

                        txtGWrequiredMiningSeepingAddit.Text = "";
                        txtGWrequiredMiningSeepingAddit.Enabled = false;
                        rfvtxtGWrequiredMiningSeepingAddit.Enabled = false;

                        txtGWRequirementAbstractStructYearAddit.Text = "";
                        txtGWRequirementAbstractStructYearAddit.Enabled = false;
                        rfvtxtGWRequirementAbstractStructYearAddit.Enabled = false;

                        txtGWrequiredMiningSeepingYearAddit.Text = "";
                        txtGWrequiredMiningSeepingYearAddit.Enabled = false;
                        rfvtxtGWrequiredMiningSeepingYearAddit.Enabled = false;


                        break;
                    case NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.GroundWaterUtilizationForOption.ExistingGWWithAdditionalGWRequirment:

                        txtGWRequirementAbstractStructAddit.Enabled = true;
                        rfvtxtGWRequirementAbstractStructAddit.Enabled = true;


                        txtGWrequiredMiningSeepingAddit.Enabled = true;
                        rfvtxtGWrequiredMiningSeepingAddit.Enabled = true;


                        txtGWRequirementAbstractStructYearAddit.Enabled = true;
                        rfvtxtGWRequirementAbstractStructYearAddit.Enabled = true;

                        txtGWrequiredMiningSeepingYearAddit.Enabled = true;
                        rfvtxtGWrequiredMiningSeepingYearAddit.Enabled = true;

                        break;
                }




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

        //txtGWrequiredMiningSeepingExist.Enabled = Status;
        //txtGWrequiredMiningSeepingYearExist.Enabled = Status;
        //txtGWrequiredMiningSeepingAddit.Enabled = Status;
        //txtGWrequiredMiningSeepingYearAddit.Enabled = Status;

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
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure obj_minRenewExistingDeWateringBlankForGridToObj = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure();
                    List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure> lst_minRenewExistingDeWateringList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure>();

                    foreach (GridViewRow gvRow in gvdeWateringExisting.Rows)
                    {
                        obj_minRenewExistingDeWateringBlankForGridToObj = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure();
                        obj_minRenewExistingDeWateringBlankForGridToObj.MiningRenewApplicationCode = Convert.ToInt64(gvdeWateringExisting.DataKeys[gvRow.RowIndex].Value.ToString());
                        if (Convert.ToInt64(gvdeWateringExisting.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                        {
                            Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                            if (lbl_SerialNumber.Text.Trim() == "")
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.SerialNumber = 0;
                            }
                            else
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                            }
                            Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");
                            if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                            }
                            else
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                            }
                            Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                            if (lbl_YearOfConstruction.Text.Trim() == "")
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.YearOfConstruction = null;
                            }
                            else
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                            }
                            Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                            if (lbl_DepthExist.Text.Trim() == "")
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.DepthExist = null;
                            }
                            else
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                            }
                            Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                            if (lbl_Diameter.Text.Trim() == "")
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.Diameter = null;
                            }
                            else
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                            }
                            Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                            if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.DepthBelowWaterLevel = null;
                            }
                            else
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                            }
                            Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                            if (lbl_Discharge.Text.Trim() == "")
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.Discharge = null;
                            }
                            else
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                            }
                            Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                            if (lbl_OperationalHousrDay.Text.Trim() == "")
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.OperationalHousrDay = null;
                            }
                            else
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                            }

                            Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                            if (lbl_OperationalDaysYear.Text.Trim() == "")
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.OperationalDaysYear = null;
                            }
                            else
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                            }
                            Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                            if (lbl_LiftingDeviceCode.Text.Trim() == "")
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.LiftingDeviceCode = null;
                            }
                            else
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                            }
                            Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                            if (lbl_PowerOfPump.Text.Trim() == "")
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.PowerOfPump = null;
                            }
                            else
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                            }

                            Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                            switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                            {
                                case "":
                                    obj_minRenewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_minRenewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                    break;
                                case "No":
                                    obj_minRenewExistingDeWateringBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                    break;
                            }
                            Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                            switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                            {
                                case "":
                                    obj_minRenewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_minRenewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                    break;
                                case "No":
                                    obj_minRenewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                    break;
                            }

                            Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                            if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                            }
                            else
                            {
                                obj_minRenewExistingDeWateringBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                            }
                            lst_minRenewExistingDeWateringList.Add(obj_minRenewExistingDeWateringBlankForGridToObj);
                        }
                    }
                    lst_minRenewExistingDeWateringList.RemoveAt(lst_IntIndex);
                    gvdeWateringExisting.DataSource = lst_minRenewExistingDeWateringList;
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
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure obj_minRenewExistingDGWAS = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure();
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
                    obj_minRenewExistingDGWAS = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADExistingDeWateringAbstractionStructure(Convert.ToInt64(gvdeWateringExisting.DataKeys[e.Row.RowIndex].Values[0].ToString()), Convert.ToInt32(lbl_SerialNumber.Text));
                    lbl_TypeOfAbstractionStructureName.Text = HttpUtility.HtmlEncode(obj_minRenewExistingDGWAS.GetTypeOfAbstractionStructure().TypeOfAbstractionStructureDesc);
                    if (obj_minRenewExistingDGWAS.GetLiftingDevice() != null)
                    {
                        lbl_LiftingDeviceName.Text = HttpUtility.HtmlEncode(obj_minRenewExistingDGWAS.GetLiftingDevice().LiftingDeviceDesc);
                        lbl_LiftingDeviceCode.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_minRenewExistingDGWAS.GetLiftingDevice().LiftingDeviceCode));
                    }
                }
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