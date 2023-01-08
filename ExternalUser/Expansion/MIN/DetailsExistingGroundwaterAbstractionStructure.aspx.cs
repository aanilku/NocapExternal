using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_Expansion_MIN_DetailsExistingGroundwaterAbstractionStructure : System.Web.UI.Page
{
    string strPageName = "MINDetailsExistingGroundwaterAbstractionStructure";
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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblMiningApplicationCodeFrom");
                        if (SourceLabel != null) { lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); }
                    }
                }
                if (lblMiningApplicationCodeFrom.Text.Trim() != "") { BindIGVMinNewExistingGWASDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)); }
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

    private void BindIGVMinNewExistingGWASDetails(long lngA_ApplicationCode)
    {
        NOCAP.BLL.Mining.Expansion.MiningExpansionApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.Expansion.MiningExpansionApplication(lngA_ApplicationCode);
        NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure obj_minNewExistingGWASBlank = new NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure();
        List<NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure> lst_minNewExistingGWASBlank = new List<NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure>();
        List<NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure> lst_minNewExistingGWAS = new List<NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure>();
        try
        {
            txtNumbeOfExistingGAS.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.NumberOfStructureExisting));
            if (txtNumbeOfExistingGAS.Text == "") { txtNumbeOfExistingGAS.Text = "0"; }
            NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure[] arr_minNewExistingGWAS;
            arr_minNewExistingGWAS = obj_MiningNewApplication.GetMiningExpansionExistingGroundWaterAbstractionStructureList();
            foreach (NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure test in arr_minNewExistingGWAS) { lst_minNewExistingGWAS.Add(test); }
            //if (arr_minNewExistingGWAS.Count() > 0)
            //{
                gvGASExisting.DataSource = lst_minNewExistingGWAS;
                gvGASExisting.DataBind();
            //}
            //else
            //{
            //    lst_minNewExistingGWASBlank.Add(obj_minNewExistingGWASBlank);
            //    gvGASExisting.DataSource = lst_minNewExistingGWASBlank;
            //    gvGASExisting.DataBind();
            //    //int int_NoOfCol = 0;
            //    //int_NoOfCol = gvGASExisting.Rows[0].Cells.Count;
            //    //gvGASExisting.Rows[0].Cells.Clear();
            //    //gvGASExisting.Rows[0].Cells.Add(new TableCell());
            //    //gvGASExisting.Rows[0].Cells[0].ColumnSpan = int_NoOfCol;
            //    //gvGASExisting.Rows[0].Cells[0].Text = "No Records exsist in GAS Existing";
            //   // gvGASExisting.e
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
                // Server.Transfer("~/ExternalUser/MiningNew/MonitorOfGroundWaterRegime.aspx");
                Server.Transfer("ProposedUtiizationOfPumpedWater.aspx");
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
                UpdateMinNewExistingGWASDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
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

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;

            if (Page.IsValid)
            {
                if (UpdateMinNewExistingGWASDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)) == 1)
                {
                    Server.Transfer("~/ExternalUser/Expansion/MIN/DetailsProposedGroundwaterAbstractionStructure.aspx");
                }
            }
        }
    }

    private int UpdateMinNewExistingGWASDetails(long lngA_ApplicationCode)
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
            if (Convert.ToInt32(txtNumbeOfExistingGAS.Text) != gvGASExisting.Rows.Count)
            {
                lblMessage.Text = "Number of Records of Detailed Structures should be equal to the Number of Existing Groundwater Abstraction Structures";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
            NOCAP.BLL.Mining.Expansion.MiningExpansionApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.Expansion.MiningExpansionApplication(lngA_ApplicationCode);
            List<NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure> lst_minNewExistingGWASList = new List<NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure>();
           
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
                NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure obj_minNewExistingGWAS = new NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure();
                obj_minNewExistingGWAS.ApplicationCode = Convert.ToInt32(gvGASExisting.DataKeys[gvRow.RowIndex].Value.ToString());

                if (lbl_TypeOfAbstractionStructureCode.Text == "")
                {
                    obj_minNewExistingGWAS.TypeOfAbstractionStructureCode = 0;
                }
                else
                {
                    obj_minNewExistingGWAS.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text);
                }
                if (lbl_YearOfConstruction.Text.Trim() == "")
                {
                    obj_minNewExistingGWAS.YearOfConstruction = null;
                }
                else
                {
                    obj_minNewExistingGWAS.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                }
                if (lbl_DepthExist.Text.Trim() == "")
                {
                    obj_minNewExistingGWAS.DepthExist = null;
                }
                else
                {
                    obj_minNewExistingGWAS.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                }
                if (lbl_Diameter.Text.Trim() == "")
                {
                    obj_minNewExistingGWAS.Diameter = null;
                }
                else
                {
                    obj_minNewExistingGWAS.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                }
                if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                {
                    obj_minNewExistingGWAS.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_minNewExistingGWAS.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                }
                if (lbl_Discharge.Text.Trim() == "")
                {
                    obj_minNewExistingGWAS.Discharge = null;
                }
                else
                {
                    obj_minNewExistingGWAS.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                }
                if (lbl_OperationalHousrDay.Text.Trim() == "")
                {
                    obj_minNewExistingGWAS.OperationalHousrDay = null;
                }
                else
                {
                    obj_minNewExistingGWAS.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                }
                if (lbl_OperationalDaysYear.Text.Trim() == "")
                {
                    obj_minNewExistingGWAS.OperationalDaysYear = null;
                }
                else
                {
                    obj_minNewExistingGWAS.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                }
                if (lbl_LiftingDeviceCode.Text.Trim() == "")
                {
                    obj_minNewExistingGWAS.LiftingDeviceCode = null;
                }
                else
                {
                    obj_minNewExistingGWAS.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                }
                if (lbl_PowerOfPump.Text.Trim() == "")
                {
                    obj_minNewExistingGWAS.PowerOfPump = null;
                }
                else
                {
                    obj_minNewExistingGWAS.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                }
                switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                {
                    case "":
                        obj_minNewExistingGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_minNewExistingGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_minNewExistingGWAS.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }
                switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                {
                    case "":
                        obj_minNewExistingGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_minNewExistingGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_minNewExistingGWAS.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                {
                    obj_minNewExistingGWAS.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_minNewExistingGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                }

                obj_minNewExistingGWAS.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;

                lst_minNewExistingGWASList.Add(obj_minNewExistingGWAS);
            }



            NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure[] arr_tempMinGwasExistingListBLL = new NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure[lst_minNewExistingGWASList.Count];
            lst_minNewExistingGWASList.CopyTo(arr_tempMinGwasExistingListBLL);
            obj_MiningNewApplication.MiningExpansionExistingGroundWaterAbstractionStructureList = arr_tempMinGwasExistingListBLL;
            if (txtNumbeOfExistingGAS.Text.Trim() == "")
            {
                obj_MiningNewApplication.NumberOfStructureExisting = null;
            }
            else
            {
                obj_MiningNewApplication.NumberOfStructureExisting = Convert.ToInt32(txtNumbeOfExistingGAS.Text.Trim());
            }

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_MiningNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_MiningNewApplication.Update() == 1)
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

                lblMessage.Text = obj_MiningNewApplication.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                //BindIGVMinNewExistingGWASDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
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
                NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure obj_MinNewExistingGWASBlankForGridToObj = new NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure();
                List<NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure> lst_MinNewExistingGWASNewList = new List<NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure>();
                foreach (GridViewRow gvRow in gvGASExisting.Rows)
                {
                    obj_MinNewExistingGWASBlankForGridToObj = new NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure();
                    obj_MinNewExistingGWASBlankForGridToObj.ApplicationCode = Convert.ToInt64(gvGASExisting.DataKeys[gvRow.RowIndex].Value.ToString());
                    if (Convert.ToInt64(gvGASExisting.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                    {
                        Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                        if (lbl_SerialNumber.Text.Trim() == "")
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.SerialNumber = 0;
                        }
                        else
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                        }

                        Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");

                        if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                        }
                        else
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                        }
                        Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                        if (lbl_YearOfConstruction.Text.Trim() == "")
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.YearOfConstruction = null;
                        }
                        else
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                        }
                        Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                        if (lbl_DepthExist.Text.Trim() == "")
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.DepthExist = null;
                        }
                        else
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                        }
                        Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                        if (lbl_Diameter.Text.Trim() == "")
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.Diameter = null;
                        }
                        else
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                        }
                        Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                        if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                        }
                        else
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                        }
                        Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                        if (lbl_Discharge.Text.Trim() == "")
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.Discharge = null;
                        }
                        else
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                        }
                        Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                        if (lbl_OperationalHousrDay.Text.Trim() == "")
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.OperationalHousrDay = null;
                        }
                        else
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                        }

                        Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                        if (lbl_OperationalDaysYear.Text.Trim() == "")
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.OperationalDaysYear = null;
                        }
                        else
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                        }
                        Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                        if (lbl_LiftingDeviceCode.Text.Trim() == "")
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.LiftingDeviceCode = null;
                        }
                        else
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                        }
                        Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                        if (lbl_PowerOfPump.Text.Trim() == "")
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.PowerOfPump = null;
                        }
                        else
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                        }

                        Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                        switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                        {
                            case "":
                                obj_MinNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                break;
                            case "Yes":
                                obj_MinNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                break;
                            case "No":
                                obj_MinNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                break;
                        }
                        Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                        switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                        {
                            case "":
                                obj_MinNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                break;
                            case "Yes":
                                obj_MinNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                break;
                            case "No":
                                obj_MinNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                break;
                        }

                        Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                        if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                        }
                        else
                        {
                            obj_MinNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                        }

                        lst_MinNewExistingGWASNewList.Add(obj_MinNewExistingGWASBlankForGridToObj);
                    }
                }
                //---------------
                obj_MinNewExistingGWASBlankForGridToObj = new NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure();
                obj_MinNewExistingGWASBlankForGridToObj.ApplicationCode = Convert.ToInt64(lblMiningApplicationCodeFrom.Text);

                if (NOCAPExternalUtility.IsNumeric(ddlTypeOfStructure.SelectedValue)) { obj_MinNewExistingGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(ddlTypeOfStructure.SelectedValue); }
                else { return; }

                if (txtYearOfConstruction.Text.Trim() == "")
                {
                    obj_MinNewExistingGWASBlankForGridToObj.YearOfConstruction = null;
                }
                else
                {
                    obj_MinNewExistingGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt32(txtYearOfConstruction.Text.Trim());
                }
                if (txtDepthMeter.Text.Trim() == "")
                {
                    obj_MinNewExistingGWASBlankForGridToObj.DepthExist = null;
                }
                else
                {
                    obj_MinNewExistingGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(txtDepthMeter.Text.Trim());
                }
                if (txtDiameterMM.Text.Trim() == "")
                {
                    obj_MinNewExistingGWASBlankForGridToObj.Diameter = null;
                }
                else
                {
                    obj_MinNewExistingGWASBlankForGridToObj.Diameter = Convert.ToInt32(txtDiameterMM.Text.Trim());
                }

                if (txtDepthWaterBelowGroundLevel.Text.Trim() == "")
                {
                    obj_MinNewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                }
                else
                {
                    obj_MinNewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(txtDepthWaterBelowGroundLevel.Text.Trim());
                }
                if (txtDischarge.Text.Trim() == "")
                {
                    obj_MinNewExistingGWASBlankForGridToObj.Discharge = null;
                }
                else
                {
                    obj_MinNewExistingGWASBlankForGridToObj.Discharge = Convert.ToDecimal(txtDischarge.Text.Trim());
                }
                if (txtOperationalHoursDay.Text.Trim() == "")
                {
                    obj_MinNewExistingGWASBlankForGridToObj.OperationalHousrDay = null;
                }
                else
                {
                    obj_MinNewExistingGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(txtOperationalHoursDay.Text.Trim());
                }

                if (txtOperationalDaysYear.Text.Trim() == "")
                {
                    obj_MinNewExistingGWASBlankForGridToObj.OperationalDaysYear = null;
                }
                else
                {
                    obj_MinNewExistingGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(txtOperationalDaysYear.Text.Trim());
                }

                if (ddlModeOfLift.SelectedValue == "")
                {
                    obj_MinNewExistingGWASBlankForGridToObj.LiftingDeviceCode = null;
                }
                else
                {
                    if (NOCAPExternalUtility.IsNumeric(ddlModeOfLift.SelectedValue)) { obj_MinNewExistingGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(ddlModeOfLift.SelectedValue); }
                }
                if (txtHorsePowerPump.Text.Trim() == "")
                {
                    obj_MinNewExistingGWASBlankForGridToObj.PowerOfPump = null;
                }
                else
                {
                    obj_MinNewExistingGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(txtHorsePowerPump.Text.Trim());
                }

                switch (ddlWhetherFittedWithWaterMeter.SelectedValue)
                {
                    case "":
                        obj_MinNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                        break;
                    case "Yes":
                        obj_MinNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                        break;
                    case "No":
                        obj_MinNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                        break;
                }

                switch (ddlWhetherPermissionRegisteredWithCGWA.SelectedValue)
                {
                    case "":
                        obj_MinNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                        break;
                    case "Yes":
                        obj_MinNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                        break;
                    case "No":
                        obj_MinNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                        break;
                }
                if (txtWhetherPermissionRegisteredWithCGWADet.Text.Trim() == "")
                {
                    obj_MinNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                }
                else
                {
                    obj_MinNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = Convert.ToString(txtWhetherPermissionRegisteredWithCGWADet.Text.Trim());
                }
                lst_MinNewExistingGWASNewList.Add(obj_MinNewExistingGWASBlankForGridToObj);
                gvGASExisting.DataSource = lst_MinNewExistingGWASNewList;
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
                    NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure obj_MinNewExistingGWASBlankForGridToObj = new NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure();
                    List<NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure> lst_MinNewExistingGWASNewList = new List<NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure>();
                    foreach (GridViewRow gvRow in gvGASExisting.Rows)
                    {
                        obj_MinNewExistingGWASBlankForGridToObj = new NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure();
                        obj_MinNewExistingGWASBlankForGridToObj.ApplicationCode = Convert.ToInt64(gvGASExisting.DataKeys[gvRow.RowIndex].Value.ToString());
                        if (Convert.ToInt64(gvGASExisting.DataKeys[gvRow.RowIndex].Value.ToString()) != 0)
                        {
                            Label lbl_SerialNumber = (Label)gvRow.FindControl("SerialNumber");
                            if (lbl_SerialNumber.Text.Trim() == "")
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.SerialNumber = 0;
                            }
                            else
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.SerialNumber = Convert.ToInt16(lbl_SerialNumber.Text.Trim());
                            }
                            Label lbl_TypeOfAbstractionStructureCode = (Label)gvRow.FindControl("TypeOfAbstractionStructureCode");
                            if (lbl_TypeOfAbstractionStructureCode.Text.Trim() == "")
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.TypeOfAbstractionStructureCode = 0;
                            }
                            else
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.TypeOfAbstractionStructureCode = Convert.ToInt32(lbl_TypeOfAbstractionStructureCode.Text.Trim());
                            }
                            Label lbl_YearOfConstruction = (Label)gvRow.FindControl("YearOfConstruction");
                            if (lbl_YearOfConstruction.Text.Trim() == "")
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.YearOfConstruction = null;
                            }
                            else
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.YearOfConstruction = Convert.ToInt16(lbl_YearOfConstruction.Text.Trim());
                            }
                            Label lbl_DepthExist = (Label)gvRow.FindControl("DepthExist");

                            if (lbl_DepthExist.Text.Trim() == "")
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.DepthExist = null;
                            }
                            else
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.DepthExist = Convert.ToDecimal(lbl_DepthExist.Text);
                            }
                            Label lbl_Diameter = (Label)gvRow.FindControl("Diameter");

                            if (lbl_Diameter.Text.Trim() == "")
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.Diameter = null;
                            }
                            else
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.Diameter = Convert.ToInt32(lbl_Diameter.Text);
                            }
                            Label lbl_DepthBelowWaterLevel = (Label)gvRow.FindControl("DepthBelowWaterLevel");

                            if (lbl_DepthBelowWaterLevel.Text.Trim() == "")
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = null;
                            }
                            else
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.DepthBelowWaterLevel = Convert.ToDecimal(lbl_DepthBelowWaterLevel.Text);
                            }
                            Label lbl_Discharge = (Label)gvRow.FindControl("Discharge");

                            if (lbl_Discharge.Text.Trim() == "")
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.Discharge = null;
                            }
                            else
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.Discharge = Convert.ToDecimal(lbl_Discharge.Text);
                            }
                            Label lbl_OperationalHousrDay = (Label)gvRow.FindControl("OperationalHousrDay");


                            if (lbl_OperationalHousrDay.Text.Trim() == "")
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.OperationalHousrDay = null;
                            }
                            else
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.OperationalHousrDay = Convert.ToInt32(lbl_OperationalHousrDay.Text);
                            }

                            Label lbl_OperationalDaysYear = (Label)gvRow.FindControl("OperationalDaysYear");

                            if (lbl_OperationalDaysYear.Text.Trim() == "")
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.OperationalDaysYear = null;
                            }
                            else
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.OperationalDaysYear = Convert.ToInt32(lbl_OperationalDaysYear.Text);
                            }
                            Label lbl_LiftingDeviceCode = (Label)gvRow.FindControl("LiftingDeviceCode");

                            if (lbl_LiftingDeviceCode.Text.Trim() == "")
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.LiftingDeviceCode = null;
                            }
                            else
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.LiftingDeviceCode = Convert.ToInt32(lbl_LiftingDeviceCode.Text);
                            }
                            Label lbl_PowerOfPump = (Label)gvRow.FindControl("PowerOfPump");

                            if (lbl_PowerOfPump.Text.Trim() == "")
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.PowerOfPump = null;
                            }
                            else
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.PowerOfPump = Convert.ToDecimal(lbl_PowerOfPump.Text);
                            }

                            Label lbl_WaterFittedWithWaterMeterOrNot = (Label)gvRow.FindControl("WaterFittedWithWaterMeterOrNot");
                            switch (lbl_WaterFittedWithWaterMeterOrNot.Text)
                            {
                                case "":
                                    obj_MinNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_MinNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.Yes;

                                    break;
                                case "No":
                                    obj_MinNewExistingGWASBlankForGridToObj.WaterFittedWithWaterMeterOrNot = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WaterFittedWithWaterMeterOrNotOption.No;

                                    break;
                            }
                            Label lbl_WhetherPermissionRegisteredWithCGWA = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWA");
                            switch (lbl_WhetherPermissionRegisteredWithCGWA.Text)
                            {
                                case "":
                                    obj_MinNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.NotDefined;

                                    break;
                                case "Yes":
                                    obj_MinNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.Yes;

                                    break;
                                case "No":
                                    obj_MinNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWA = NOCAP.BLL.Common.GroundWaterAbstractionStructure.WhetherPermissionRegisteredWithCGWAOption.No;

                                    break;
                            }

                            Label lbl_WhetherPermissionRegisteredWithCGWADetails = (Label)gvRow.FindControl("WhetherPermissionRegisteredWithCGWADetails");

                            if (lbl_WhetherPermissionRegisteredWithCGWADetails.Text.Trim() == "")
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = null;
                            }
                            else
                            {
                                obj_MinNewExistingGWASBlankForGridToObj.WhetherPermissionRegisteredWithCGWADetails = lbl_WhetherPermissionRegisteredWithCGWADetails.Text;
                            }

                            lst_MinNewExistingGWASNewList.Add(obj_MinNewExistingGWASBlankForGridToObj);
                        }
                    }
                    lst_MinNewExistingGWASNewList.RemoveAt(lst_IntIndex);
                    gvGASExisting.DataSource = lst_MinNewExistingGWASNewList;
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

    protected void gvGASExisting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure obj_minNewExistingGWAS = new NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure();
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
                    obj_minNewExistingGWAS = new NOCAP.BLL.Mining.Expansion.MiningExpansionExistingGroundWaterAbstractionStructure(Convert.ToInt64(gvGASExisting.DataKeys[e.Row.RowIndex].Values[0].ToString()), Convert.ToInt32(lbl_SerialNumber.Text));
                    lbl_TypeOfAbstractionStructureName.Text = HttpUtility.HtmlEncode(obj_minNewExistingGWAS.GetTypeOfAbstractionStructure().TypeOfAbstractionStructureDesc);
                    if (obj_minNewExistingGWAS.GetLiftingDevice() != null)
                    {
                        lbl_LiftingDeviceName.Text = HttpUtility.HtmlEncode(obj_minNewExistingGWAS.GetLiftingDevice().LiftingDeviceDesc);
                        lbl_LiftingDeviceCode.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_minNewExistingGWAS.GetLiftingDevice().LiftingDeviceCode));
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