using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_MiningRenew_DetailsAdditionalGroundwaterAbstractionStructure : System.Web.UI.Page
{

    string strPageName = "MINRenewDetailsAdditionalGroundwaterAbstractionStructure";
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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblMiningApplicationCodeFrom");
                        if (SourceLabel != null) { lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    BindIGVMinRenewAdditionalGWASDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
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

    private void BindIGVMinRenewAdditionalGWASDetails(long lngA_ApplicationCode)
    {
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
        NOCAP.BLL.Mining.New.Application.MiningNewApplication objMiningNewApplication = obj_MiningRenewApplication.GetFirstMiningApplication();

        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure obj_minRenewAdditionalGWASBlank = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure();
        List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure> lst_minRenewAdditionalGWASBlank = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure>();
        List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure> lst_minRenewAdditionalGWAS = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure>();

        try
        {
             
            if (objMiningNewApplication != null)
            {
                lblHeadingProjName.Text = HttpUtility.HtmlEncode(objMiningNewApplication.NameOfMining);
            }
            txtNumbeOfAdditionalGAS.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.NumberOfStructureAdditional));
            if (txtNumbeOfAdditionalGAS.Text == "") { txtNumbeOfAdditionalGAS.Text = "0"; }
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure[] arr_minRenewAdditionalGWAS;
            arr_minRenewAdditionalGWAS = obj_MiningRenewApplication.GetMiningRenewAdditionalGroundWaterAbstractionStructureList();
            foreach (NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure test in arr_minRenewAdditionalGWAS)
            {
                lst_minRenewAdditionalGWAS.Add(test);
            }

            //if (arr_minNewProposedGWAS.Count() > 0)
            //{
            gvGASAdditional.DataSource = lst_minRenewAdditionalGWAS;
            gvGASAdditional.DataBind();
            //}
            //else
            //{

            //    lst_minNewProposedGWAS.Add(obj_minNewProposedGWASBlank);
            //    gvGASProposed.DataSource = lst_minNewProposedGWAS;
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
                Server.Transfer("~/ExternalUser/MiningRenew/DetailsExistingGroundwaterAbstractionStructure.aspx");
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
                UpdateminRenewAdditionalGWASDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
            }
        }
    }
    private int UpdateminRenewAdditionalGWASDetails(long lngA_ApplicationCode)
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
            if (Convert.ToInt32(txtNumbeOfAdditionalGAS.Text) != gvGASAdditional.Rows.Count)
            {
                lblMessage.Text = "Number of Records of Detailed Structures should be equal to the Number of Additional Groundwater Abstraction Structures";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure> lst_minRenewAdditionalGWASList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure>();
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

                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure obj_minRenewAdditionalGWAS = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure();
                obj_minRenewAdditionalGWAS.MiningRenewApplicationCode = Convert.ToInt32(gvGASAdditional.DataKeys[gvRow.RowIndex].Value.ToString());

                if (lbl_TypeOfAbstractionStructureCode.Text == "")
                {
                    obj_minRenewAdditionalGWAS.TypeOfAbstractionStructureCode = 0;
                }
                else
                {
                    obj_minRenewAdditionalGWAS.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text);
                }
                if (lbl_YearOfConstruction.Text.Trim() == "")
                {
                    obj_minRenewAdditionalGWAS.YearOfConstruction = null;
                }
                else
                {
                    obj_minRenewAdditionalGWAS.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                }
                if (lbl_DepthExist.Text.Trim() == "")
                {
                    obj_minRenewAdditionalGWAS.DepthExist = null;
                }
                else
                {
                    obj_minRenewAdditionalGWAS.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                }
                if (lbl_Diameter.Text.Trim() == "")
                {
                    obj_minRenewAdditionalGWAS.Diameter = null;
                }
                else
                {
                    obj_minRenewAdditionalGWAS.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                }
                if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                {
                    obj_minRenewAdditionalGWAS.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_minRenewAdditionalGWAS.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                }
                if (lbl_Discharge.Text.Trim() == "")
                {
                    obj_minRenewAdditionalGWAS.Discharge = null;
                }
                else
                {
                    obj_minRenewAdditionalGWAS.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                }
                if (lbl_OperationalHousrDay.Text.Trim() == "")
                {
                    obj_minRenewAdditionalGWAS.OperationalHousrDay = null;
                }
                else
                {
                    obj_minRenewAdditionalGWAS.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                }
                if (lbl_OperationalDaysYear.Text.Trim() == "")
                {
                    obj_minRenewAdditionalGWAS.OperationalDaysYear = null;
                }
                else
                {
                    obj_minRenewAdditionalGWAS.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                }
                if (lbl_LiftingDeviceCode.Text.Trim() == "")
                {
                    obj_minRenewAdditionalGWAS.LiftingDeviceCode = null;
                }
                else
                {
                    obj_minRenewAdditionalGWAS.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                }
                if (lbl_PowerOfPump.Text.Trim() == "")
                {
                    obj_minRenewAdditionalGWAS.PowerOfPump = null;
                }
                else
                {
                    obj_minRenewAdditionalGWAS.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                }
                switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                {
                    case "":
                        obj_minRenewAdditionalGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_minRenewAdditionalGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_minRenewAdditionalGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }
                switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                {
                    case "":
                        obj_minRenewAdditionalGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_minRenewAdditionalGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_minRenewAdditionalGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                {
                    obj_minRenewAdditionalGWAS.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_minRenewAdditionalGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                }

                obj_minRenewAdditionalGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;

                lst_minRenewAdditionalGWASList.Add(obj_minRenewAdditionalGWAS);
            }



            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure[] arr_tempMinRenewGwasAdditionalListBLL = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure[lst_minRenewAdditionalGWASList.Count];
            lst_minRenewAdditionalGWASList.CopyTo(arr_tempMinRenewGwasAdditionalListBLL);
            obj_MiningRenewApplication.MiningRenewAdditionalGroundWaterAbstractionStructureList = arr_tempMinRenewGwasAdditionalListBLL;
            if (txtNumbeOfAdditionalGAS.Text.Trim() == "")
            {
                obj_MiningRenewApplication.NumberOfStructureAdditional = null;
            }
            else
            {
                obj_MiningRenewApplication.NumberOfStructureAdditional = Convert.ToInt32(txtNumbeOfAdditionalGAS.Text.Trim());
            }

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_MiningRenewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_MiningRenewApplication.Update() == 1)
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

                lblMessage.Text = obj_MiningRenewApplication.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                BindIGVMinRenewAdditionalGWASDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
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
    protected void btnNext_Click(object sender, EventArgs e)
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
                if (UpdateminRenewAdditionalGWASDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)) == 1) { Server.Transfer("~/ExternalUser/MiningRenew/ComplianceConditionNOC.aspx"); }
                else
                {
                }
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
                NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure obj_minRenewAdditionalGWASBlankForGridToObj = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure();
                List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure> lst_minRenewAdditionalGWASNewList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure>();
                foreach (GridViewRow gvRow in gvGASAdditional.Rows)
                {
                    obj_minRenewAdditionalGWASBlankForGridToObj = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure();
                    obj_minRenewAdditionalGWASBlankForGridToObj.MiningRenewApplicationCode = Convert.ToInt64(gvGASAdditional.DataKeys[gvRow.RowIndex].Value.ToString());
                    if (Convert.ToInt64(gvGASAdditional.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                    {
                        Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                        if (lbl_SerialNumber.Text.Trim() == "")
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.SerialNumber = 0;
                        }
                        else
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                        }

                        Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");

                        if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                        }
                        else
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                        }
                        Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                        if (lbl_YearOfConstruction.Text.Trim() == "")
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.YearOfConstruction = null;
                        }
                        else
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                        }
                        Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                        if (lbl_DepthExist.Text.Trim() == "")
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.DepthExist = null;
                        }
                        else
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                        }
                        Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                        if (lbl_Diameter.Text.Trim() == "")
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.Diameter = null;
                        }
                        else
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                        }
                        Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                        if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                        }
                        else
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                        }
                        Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                        if (lbl_Discharge.Text.Trim() == "")
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.Discharge = null;
                        }
                        else
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                        }
                        Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                        if (lbl_OperationalHousrDay.Text.Trim() == "")
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.OperationalHousrDay = null;
                        }
                        else
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                        }

                        Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                        if (lbl_OperationalDaysYear.Text.Trim() == "")
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.OperationalDaysYear = null;
                        }
                        else
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                        }
                        Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                        if (lbl_LiftingDeviceCode.Text.Trim() == "")
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.LiftingDeviceCode = null;
                        }
                        else
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                        }
                        Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                        if (lbl_PowerOfPump.Text.Trim() == "")
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.PowerOfPump = null;
                        }
                        else
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                        }

                        Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                        switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                        {
                            case "":
                                obj_minRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                break;
                            case "Yes":
                                obj_minRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                break;
                            case "No":
                                obj_minRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                break;
                        }
                        Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                        switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                        {
                            case "":
                                obj_minRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                break;
                            case "Yes":
                                obj_minRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                break;
                            case "No":
                                obj_minRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                break;
                        }

                        Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                        if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                        }
                        else
                        {
                            obj_minRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                        }

                        lst_minRenewAdditionalGWASNewList.Add(obj_minRenewAdditionalGWASBlankForGridToObj);


                    }
                }
                //---------------
                obj_minRenewAdditionalGWASBlankForGridToObj = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure();
                obj_minRenewAdditionalGWASBlankForGridToObj.MiningRenewApplicationCode = Convert.ToInt64(lblMiningApplicationCodeFrom.Text);
                if (NOCAPExternalUtility.IsNumeric(ddlTypeOfStructure.SelectedValue))
                { obj_minRenewAdditionalGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(ddlTypeOfStructure.SelectedValue); }
                else { return; }
                if (txtYearOfConstruction.Text.Trim() == "")
                {
                    obj_minRenewAdditionalGWASBlankForGridToObj.YearOfConstruction = null;
                }
                else
                {
                    obj_minRenewAdditionalGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt32(txtYearOfConstruction.Text.Trim());
                }
                if (txtDepthMeter.Text.Trim() == "")
                {
                    obj_minRenewAdditionalGWASBlankForGridToObj.DepthExist = null;
                }
                else
                {
                    obj_minRenewAdditionalGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(txtDepthMeter.Text.Trim());
                }
                if (txtDiameterMM.Text.Trim() == "")
                {
                    obj_minRenewAdditionalGWASBlankForGridToObj.Diameter = null;
                }
                else
                {
                    obj_minRenewAdditionalGWASBlankForGridToObj.Diameter = Convert.ToInt32(txtDiameterMM.Text.Trim());
                }

                if (txtDepthWaterBelowGroundLevel.Text.Trim() == "")
                {
                    obj_minRenewAdditionalGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_minRenewAdditionalGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(txtDepthWaterBelowGroundLevel.Text.Trim());
                }
                if (txtDischarge.Text.Trim() == "")
                {
                    obj_minRenewAdditionalGWASBlankForGridToObj.Discharge = null;
                }
                else
                {
                    obj_minRenewAdditionalGWASBlankForGridToObj.Discharge = Convert.ToDecimal(txtDischarge.Text.Trim());
                }
                if (txtOperationalHoursDay.Text.Trim() == "")
                {
                    obj_minRenewAdditionalGWASBlankForGridToObj.OperationalHousrDay = null;
                }
                else
                {
                    obj_minRenewAdditionalGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(txtOperationalHoursDay.Text.Trim());
                }

                if (txtOperationalDaysYear.Text.Trim() == "")
                {
                    obj_minRenewAdditionalGWASBlankForGridToObj.OperationalDaysYear = null;
                }
                else
                {
                    obj_minRenewAdditionalGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(txtOperationalDaysYear.Text.Trim());
                }

                if (ddlModeOfLift.SelectedValue == "")
                {
                    obj_minRenewAdditionalGWASBlankForGridToObj.LiftingDeviceCode = null;
                }
                else
                {
                    if (NOCAPExternalUtility.IsNumeric(ddlModeOfLift.SelectedValue)) { obj_minRenewAdditionalGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(ddlModeOfLift.SelectedValue); }
                }
                if (txtHorsePowerPump.Text.Trim() == "")
                {
                    obj_minRenewAdditionalGWASBlankForGridToObj.PowerOfPump = null;
                }
                else
                {
                    obj_minRenewAdditionalGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(txtHorsePowerPump.Text.Trim());
                }

                switch (ddlWhetherFittedWithWaterMeter.SelectedValue)
                {
                    case "":
                        obj_minRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_minRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_minRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }

                switch (ddlWhetherPermissionRegisteredWithCGWA.SelectedValue)
                {
                    case "":
                        obj_minRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_minRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_minRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (txtWhetherPermissionRegisteredWithCGWADet.Text.Trim() == "")
                {
                    obj_minRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_minRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = Convert.ToString(txtWhetherPermissionRegisteredWithCGWADet.Text.Trim());
                }
                lst_minRenewAdditionalGWASNewList.Add(obj_minRenewAdditionalGWASBlankForGridToObj);
                gvGASAdditional.DataSource = lst_minRenewAdditionalGWASNewList;
                gvGASAdditional.DataBind();
                ClearGASAdditionalScreen();
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
    private void ClearGASAdditionalScreen()
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
            if (ddlWhetherPermissionRegisteredWithCGWA.SelectedValue == "Yes") { txtWhetherPermissionRegisteredWithCGWADet.Enabled = true; }
            else
            {
                txtWhetherPermissionRegisteredWithCGWADet.Enabled = false;
                txtWhetherPermissionRegisteredWithCGWADet.Text = "";
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
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure obj_minRenewAdditionalGWASBlankForGridToObj = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure();
                    List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure> lst_minRenewAdditionalGWASNewList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure>();
                    foreach (GridViewRow gvRow in gvGASAdditional.Rows)
                    {
                        obj_minRenewAdditionalGWASBlankForGridToObj = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure();
                        obj_minRenewAdditionalGWASBlankForGridToObj.MiningRenewApplicationCode = Convert.ToInt64(gvGASAdditional.DataKeys[gvRow.RowIndex].Value.ToString());

                        if (Convert.ToInt64(gvGASAdditional.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                        {
                            Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                            if (lbl_SerialNumber.Text.Trim() == "")
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.SerialNumber = 0;
                            }
                            else
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                            }
                            Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");
                            if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                            }
                            else
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                            }
                            Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                            if (lbl_YearOfConstruction.Text.Trim() == "")
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.YearOfConstruction = null;
                            }
                            else
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                            }
                            Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                            if (lbl_DepthExist.Text.Trim() == "")
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.DepthExist = null;
                            }
                            else
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                            }
                            Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                            if (lbl_Diameter.Text.Trim() == "")
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.Diameter = null;
                            }
                            else
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                            }
                            Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                            if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                            }
                            else
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                            }
                            Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                            if (lbl_Discharge.Text.Trim() == "")
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.Discharge = null;
                            }
                            else
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                            }
                            Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                            if (lbl_OperationalHousrDay.Text.Trim() == "")
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.OperationalHousrDay = null;
                            }
                            else
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                            }

                            Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                            if (lbl_OperationalDaysYear.Text.Trim() == "")
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.OperationalDaysYear = null;
                            }
                            else
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                            }
                            Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                            if (lbl_LiftingDeviceCode.Text.Trim() == "")
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.LiftingDeviceCode = null;
                            }
                            else
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                            }
                            Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                            if (lbl_PowerOfPump.Text.Trim() == "")
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.PowerOfPump = null;
                            }
                            else
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                            }

                            Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                            switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                            {
                                case "":
                                    obj_minRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_minRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                    break;
                                case "No":
                                    obj_minRenewAdditionalGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                    break;
                            }
                            Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                            switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                            {
                                case "":
                                    obj_minRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_minRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                    break;
                                case "No":
                                    obj_minRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                    break;
                            }

                            Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                            if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                            }
                            else
                            {
                                obj_minRenewAdditionalGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                            }
                            lst_minRenewAdditionalGWASNewList.Add(obj_minRenewAdditionalGWASBlankForGridToObj);
                        }
                    }
                    lst_minRenewAdditionalGWASNewList.RemoveAt(lst_IntIndex);
                    gvGASAdditional.DataSource = lst_minRenewAdditionalGWASNewList;
                    gvGASAdditional.DataBind();
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

    protected void gvGASAdditional_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure obj_minRenewAdditionalGWAS = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure();
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
                    obj_minRenewAdditionalGWAS = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADAdditionalGroundWaterAbstractionStructure(Convert.ToInt64(gvGASAdditional.DataKeys[e.Row.RowIndex].Values[0].ToString()), Convert.ToInt32(lbl_SerialNumber.Text));
                    lbl_TypeOfAbstractionStructureName.Text = HttpUtility.HtmlEncode(obj_minRenewAdditionalGWAS.GetTypeOfAbstractionStructure().TypeOfAbstractionStructureDesc);
                    if (obj_minRenewAdditionalGWAS.GetLiftingDevice() != null)
                    {
                        lbl_LiftingDeviceName.Text = HttpUtility.HtmlEncode(obj_minRenewAdditionalGWAS.GetLiftingDevice().LiftingDeviceDesc);
                        lbl_LiftingDeviceCode.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_minRenewAdditionalGWAS.GetLiftingDevice().LiftingDeviceCode));
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
}