using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sub_CheckEligibility_CheckEligibility : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //revtxtDateOfCommencement.ValidationExpression = ValidationUtility.txtValForDate;
            //revtxtDateOfCommencement.ErrorMessage = ValidationUtility.txtValForDateMsg;
            txtDOB_CalendarExtender.EndDate = System.DateTime.Now;

            //revtxtDateOfExpansion.ValidationExpression = ValidationUtility.txtValForDate;
            //revtxtDateOfExpansion.ErrorMessage = ValidationUtility.txtValForDateMsg;
            txtDateOfExpansion_CalendarExtender.EndDate = System.DateTime.Now;
            lblReason.Text = "";
            if (!IsPostBack)
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;

                if (NOCAPExternalUtility.FillDropDownState(ref ddlState) != 1)
                {
                    lblMessage.Text = "Problem in state population";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                if (NOCAPExternalUtility.FillDropDownApplicationPurpose(ref ddlApplicationPurpose) != 1)
                {
                    Response.Write("Problem in Application Purpose population");
                }
                else
                {
                    NOCAPExternalUtility.HideEntriesInDropDown(ref ddlApplicationPurpose, "2,3,4");
                }
                if (NOCAPExternalUtility.FillDropDownApplicationType(ref ddlApplicationType) != 1)
                {
                    Response.Write("Problem in Application Type population");
                }
                else
                {
                    NOCAPExternalUtility.HideEntriesInDropDown(ref ddlApplicationType, "1");
                }
                if (NOCAPExternalUtility.FillDropDownWaterQuality(ref ddlWaterQualityType, NOCAP.BLL.Master.WaterQuality.VisibilityYesNo.Yes) != 1)
                {
                    lblMessage.Text = "Problem in Water Quality";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                Panel1.Visible = false;
                Panel6.Visible = false;

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally
        {
        }
    }
    protected void ddlApplicationType_SelectedIndexChanged(object sender, EventArgs e)
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
                if (NOCAPExternalUtility.FillDropDownApplicationTypeCategory(ref ddlApplicationTypeCategory, NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType(getApplicationType(ddlApplicationType.SelectedValue))) != 1)
                {
                    lblMessage.Text = "Problem in Application Type Category population";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                if (NOCAPExternalUtility.FillDropDownApplicationPurpose(ref ddlApplicationPurpose) != 1)
                {
                    Response.Write("Problem in Application Purpose population");
                }
                else
                {
                    NOCAPExternalUtility.HideEntriesInDropDown(ref ddlApplicationPurpose, "2,3,4");
                }

                Panel1.Visible = false;
                Panel6.Visible = false;

            }
            catch (Exception ex)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void ddlApplicationTypeCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel6.Visible = false;
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intStateCode;
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

                lblMessage.Text = "";
                ddlDistrict.Items.Clear();
                ddlSubDistrict.Items.Clear();
                if (ddlState.SelectedValue == "")
                {

                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlDistrict);
                }
                else
                {
                    intStateCode = Convert.ToInt32(ddlState.SelectedValue);
                    if (NOCAPExternalUtility.FillDropDownDistrict(ref ddlDistrict, intStateCode) != 1)
                    {
                        lblMessage.Text = "Problem in district population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }


                }

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        int int_StateCode;
        int int_DistricCode;
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
                lblMessage.Text = "";
                ddlSubDistrict.Items.Clear();

                if (ddlDistrict.SelectedValue == "")
                {
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlSubDistrict);
                }
                else
                {
                    int_StateCode = Convert.ToInt32(ddlState.SelectedValue);
                    int_DistricCode = Convert.ToInt32(ddlDistrict.SelectedValue);
                    if (NOCAPExternalUtility.FillDropDownSubDistrict(ref ddlSubDistrict, int_DistricCode, int_StateCode) != 1)
                    {
                        lblMessage.Text = "Problem in Subdistrict population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    private string getApplicationType(string AppTypeCode)
    {
        switch (ddlApplicationType.SelectedValue)
        {
            case "1":
                return "Domestic";
            case "2":
                return "Industrial";
            case "3":
                return "Infrastructure";
            case "4":
                return "Mining";
            default:
                return string.Empty;
        }
    }
    private string getApplicationPurpose(string AppPurposeCode)
    {
        switch (AppPurposeCode)
        {
            case "1":
                return "New";
            case "2":
                return "Renew";
            case "3":
                return "Withdraw";
            case "4":
                return "Cancelation";
            default:
                return string.Empty;
        }
    }

    protected void btnCheck_Click(object sender, EventArgs e)
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
                    if (ddlApplicationType.SelectedIndex > 0 && !NOCAPExternalUtility.IsNumeric(ddlApplicationType.SelectedValue))
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Application Type Allow only Numeric');", true);
                        return;
                    }
                    if (ddlApplicationPurpose.SelectedIndex > 0 && !NOCAPExternalUtility.IsNumeric(ddlApplicationPurpose.SelectedValue))
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Application Purpose Allow only Numeric');", true);
                        return;
                    }
                    if (ddlApplicationTypeCategory.SelectedIndex > 0 && !NOCAPExternalUtility.IsNumeric(ddlApplicationTypeCategory.SelectedValue))
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Application Type Category Allow only Numeric');", true);
                        return;
                    }
                    if (ddlState.SelectedIndex > 0 && !NOCAPExternalUtility.IsNumeric(ddlState.SelectedValue))
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('State Allow only Numeric');", true);
                        return;
                    }
                    if (ddlDistrict.SelectedIndex > 0 && !NOCAPExternalUtility.IsNumeric(ddlDistrict.SelectedValue))
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('District Allow only Numeric');", true);
                        return;
                    }
                    if (ddlSubDistrict.SelectedIndex > 0 && !NOCAPExternalUtility.IsNumeric(ddlSubDistrict.SelectedValue))
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Sub-District Allow only Numeric');", true);
                        return;
                    }
                    if (ddlWaterQualityType.SelectedIndex > 0 && !NOCAPExternalUtility.IsNumeric(ddlWaterQualityType.SelectedValue))
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Water Quality Allow only Numeric');", true);
                        return;
                    }
                    Panel1.Visible = true;
                    int int_stateCode = Convert.ToInt32(ddlState.SelectedValue);
                    int int_districtCode = Convert.ToInt32(ddlDistrict.SelectedValue);
                    int int_subDistrictCode = Convert.ToInt32(ddlSubDistrict.SelectedValue);

                    int int_applicationTypeCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType(getApplicationType(ddlApplicationType.SelectedValue));
                  //  int int_applicationPurposeCode = 1;
                     int int_applicationPurposeCode = NOCAPExternalUtility.EquivalentApplicationPurposeCodeOfDatabaseForApplicationPurpose(getApplicationPurpose(ddlApplicationPurpose.SelectedValue));
                    int int_applicationTypeCategoryCode = Convert.ToInt32(ddlApplicationTypeCategory.SelectedValue);
                    int int_WaterQualityTypeCode = Convert.ToInt32(ddlWaterQualityType.SelectedValue);


                    lblStateGroundWaterAuthorityAddress.Text = "";

                    NOCAP.BLL.Master.ApplicationCheckForStateGroundWaterAuthority obj_applicationCheckForStateGroundWaterAuthority = new NOCAP.BLL.Master.ApplicationCheckForStateGroundWaterAuthority(int_applicationTypeCode, int_applicationPurposeCode);
                    if (obj_applicationCheckForStateGroundWaterAuthority.CheckPending == NOCAP.BLL.Master.ApplicationCheckForStateGroundWaterAuthority.ApplicationCheckPending.Check)
                    {
                        // check state grou auth
                        NOCAP.BLL.Master.StateGroundWaterAuthority obj_stateGroundWaterAuthority = new NOCAP.BLL.Master.StateGroundWaterAuthority(int_stateCode, "n");
                        if (obj_stateGroundWaterAuthority.StateGroundWaterAuthorityCode > 0)
                        {
                            // not allow for active
                            if (obj_stateGroundWaterAuthority.Active == NOCAP.BLL.Master.StateGroundWaterAuthority.ActiveYesNo.Yes)
                            {
                                //for state ground water authority address
                                NOCAP.BLL.Master.State obj_state = new NOCAP.BLL.Master.State(obj_stateGroundWaterAuthority.AddressStateCode);
                                NOCAP.BLL.Master.District obj_district = new NOCAP.BLL.Master.District(obj_stateGroundWaterAuthority.AddressStateCode, obj_stateGroundWaterAuthority.AddressDistrictCode);


                                string StateGroundWaterAuthorityAddress = "Please approach <b>State Government</b> for issuance of NOC for ground water withdrawal:" + "<b><br/><br/>" + HttpUtility.HtmlEncode(obj_stateGroundWaterAuthority.StateGroundWaterAuthorityName) + "<br/>";
                                StateGroundWaterAuthorityAddress = StateGroundWaterAuthorityAddress + "Address : " + HttpUtility.HtmlEncode(obj_stateGroundWaterAuthority.AddressLine1) + HttpUtility.HtmlEncode(obj_stateGroundWaterAuthority.AddressLine2) + " " + HttpUtility.HtmlEncode(obj_stateGroundWaterAuthority.AddressLine3);
                                StateGroundWaterAuthorityAddress = StateGroundWaterAuthorityAddress + "<br/> State : " + obj_state.StateName + "<br/> District : " + obj_district.DistrictName + "<br/> Pincode : " + obj_stateGroundWaterAuthority.PinCode + "</b>";

                                lblStateGroundWaterAuthorityAddress.Text = StateGroundWaterAuthorityAddress;


                                //ProceedForApplyOnApplicationCheckForStateGroundWaterAuthority =NOCAP.BLL.Master.ApplicationAllowOrNotForApply.ApplicationAllowNotAllowForApply.NotAllow;
                                lblStateGroundWaterAuthority.Text = "Yes";
                            }
                            else if (obj_stateGroundWaterAuthority.Active == NOCAP.BLL.Master.StateGroundWaterAuthority.ActiveYesNo.No)
                            {
                                lblStateGroundWaterAuthority.Text = "No";
                            }
                            else
                            {
                                NOCAP.BLL.Master.StateGroundWaterAuthorityDetail obj_StateGroundWaterAuthorityDetail = new NOCAP.BLL.Master.StateGroundWaterAuthorityDetail(obj_stateGroundWaterAuthority.StateGroundWaterAuthorityCode, int_applicationTypeCode, int_applicationPurposeCode);

                                if (obj_StateGroundWaterAuthorityDetail.Active == NOCAP.BLL.Master.StateGroundWaterAuthorityDetail.ActiveYesNo.No)
                                {
                                    NOCAP.BLL.Master.State obj_state = new NOCAP.BLL.Master.State(obj_stateGroundWaterAuthority.AddressStateCode);
                                    NOCAP.BLL.Master.District obj_district = new NOCAP.BLL.Master.District(obj_stateGroundWaterAuthority.AddressStateCode, obj_stateGroundWaterAuthority.AddressDistrictCode);


                                    string StateGroundWaterAuthorityAddress = "Please approach <b>State Government</b> for issuance of NOC for ground water withdrawal:" + "<b><br/><br/>" + HttpUtility.HtmlEncode(obj_stateGroundWaterAuthority.StateGroundWaterAuthorityName) + "<br/>";
                                    StateGroundWaterAuthorityAddress = StateGroundWaterAuthorityAddress + "Address : " + HttpUtility.HtmlEncode(obj_stateGroundWaterAuthority.AddressLine1) + HttpUtility.HtmlEncode(obj_stateGroundWaterAuthority.AddressLine2) + " " + HttpUtility.HtmlEncode(obj_stateGroundWaterAuthority.AddressLine3);
                                    StateGroundWaterAuthorityAddress = StateGroundWaterAuthorityAddress + "<br/> State : " + obj_state.StateName + "<br/> District : " + obj_district.DistrictName + "<br/> Pincode : " + obj_stateGroundWaterAuthority.PinCode + "</b>";

                                    lblStateGroundWaterAuthorityAddress.Text = StateGroundWaterAuthorityAddress;
                                    lblStateGroundWaterAuthority.Text = "Yes";
                                }
                                else
                                {
                                    lblStateGroundWaterAuthority.Text = "No";
                                }

                            }

                        }
                        else
                        {
                            //ProceedForApplyOnApplicationCheckForStateGroundWaterAuthority = NOCAP.BLL.Master.ApplicationAllowOrNotForApply.ApplicationAllowNotAllowForApply.Allow;
                            lblStateGroundWaterAuthority.Text = "No";
                        }
                    }
                    else
                    {
                        lblStateGroundWaterAuthority.Text = "No";
                        //ProceedForApplyOnApplicationCheckForStateGroundWaterAuthority = NOCAP.BLL.Master.ApplicationAllowOrNotForApply.ApplicationAllowNotAllowForApply.Allow;
                    }

                    if (lblStateGroundWaterAuthority.Text == "No")
                    {
                        Panel4.Visible = true;
                        NOCAP.BLL.Master.SubDistrict obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(int_stateCode, int_districtCode, int_subDistrictCode);
                        NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_subDistrictAreaTypeCategoryHistory = null;
                        obj_subDistrictAreaTypeCategoryHistory = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory();

                        if (obj_subDistrictAreaTypeCategoryHistory == null || obj_subDistrictAreaTypeCategoryHistory.AreaTypeCode == 0 || obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode == 0)
                        {
                            lblAreaTypeCategory.Text = "Area Type Not Defined";
                            lblReason.Text = "<span style='color:Red'><sup>*</sup></span>Your Area type is not defined.";
                        }
                        else
                        {
                            //int_currentAreaTypeCode = obj_subDistrictAreaTypeCategoryHistory.AreaTypeCode;
                            lblAreaTypeCategory.Text = HttpUtility.HtmlEncode(obj_subDistrictAreaTypeCategoryHistory.AreaTypeDesc());
                            lblCategorizationofAssessmentUnits.Text = HttpUtility.HtmlEncode(obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc());

                            //int_currentAreaTypeCategoryCode = obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode;

                        }


                        NOCAP.BLL.Master.ApplicationNotAlloweOnWaterBasedIndustry obj_applicationNotAlloweAccordingWaterBasedIndustry = new NOCAP.BLL.Master.ApplicationNotAlloweOnWaterBasedIndustry(obj_subDistrictAreaTypeCategoryHistory.AreaTypeCode, obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode, int_applicationTypeCode, int_applicationPurposeCode);
                        NOCAP.BLL.Master.ApplicationTypeCategory obj_applicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory();

                        NOCAP.BLL.Master.AllowPendingApplicationOnAreaTypeCategory obj_applicationAllowOrNotAccordingAreaTypeCategory = new NOCAP.BLL.Master.AllowPendingApplicationOnAreaTypeCategory(obj_subDistrictAreaTypeCategoryHistory.AreaTypeCode, obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode, int_applicationTypeCode, int_applicationPurposeCode);

                        if (obj_applicationAllowOrNotAccordingAreaTypeCategory.AllowPending == NOCAP.BLL.Master.AllowPendingApplicationOnAreaTypeCategory.ApplicationAllowPending.Allow)
                        {

                        }
                        else
                        {
                            if (lblReason.Text == "")
                                lblReason.Text = "<span style='color:Red'><sup>*</sup></span>You are <b>not eligible</b> to apply for issuance of NOC for ground water withdrawal since your project falls under <b>'" + lblAreaTypeCategory.Text + "(" + lblCategorizationofAssessmentUnits.Text + ")'</b> assessment unit.";
                        }


                        if (obj_applicationNotAlloweAccordingWaterBasedIndustry.ApplicationPurposeCode > 0)
                        {
                            // check for water base
                            obj_applicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory(int_applicationTypeCode, int_applicationTypeCategoryCode);
                            if (obj_applicationTypeCategory.WaterBased == NOCAP.BLL.Master.ApplicationTypeCategory.WaterBasedYesNo.Yes)
                            {
                                //water base not allow
                                lblWaterBased.Text = "Yes";
                                if (lblReason.Text == "")
                                    lblReason.Text = "<span style='color:Red'><sup>*</sup></span>You are <b>not eligible</b> to apply for issuance of NOC for ground water withdrawal since your project is <b>'Industry using ground water as raw material or Water Intensive Unit'</b> and falls under <b>'" + lblAreaTypeCategory.Text + "(" + lblCategorizationofAssessmentUnits.Text + ")'</b> category.";
                            }
                            else
                            {
                                lblWaterBased.Text = "No";
                            }
                        }
                        else
                        {
                            // do not check for water base
                            lblWaterBased.Text = "No";
                        }






                    }
                    else
                    {
                        Panel4.Visible = false;
                        lblAreaTypeCategory.Text = "";
                        lblCategorizationofAssessmentUnits.Text = "";
                        lblWaterBased.Text = "";
                        lblFinal.Text = "";
                    }





                    NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption enm_NOCObtainForExistIND = new NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption();
                    switch (ddlNOCObtainedForExistIND.SelectedValue)
                    {
                        case "Y":
                            enm_NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.Yes;
                            break;
                        case "N":
                            enm_NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No;
                            break;
                        default:
                            enm_NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                            break;
                    }

                    NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption enm_WhetherGroundWaterUtilizationFor = new NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption();

                    switch (rbtnWhetherGroundWaterUtilization.SelectedValue)
                    {
                        case "NewIndustry":
                            enm_WhetherGroundWaterUtilizationFor = NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.NewIndustry;
                            break;
                        case "ExistingIndustry":
                            enm_WhetherGroundWaterUtilizationFor = NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry;
                            break;
                        case "ExpansionProgramExistingIndustry":
                            enm_WhetherGroundWaterUtilizationFor = NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExpansionProgramOfExitingIndustry;
                            break;
                        default:
                            enm_WhetherGroundWaterUtilizationFor = NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.NotDefined;
                            break;
                    }

                    DateTime? dt_DateOfCommExistIND = null;
                    if (txtDateOfCommencement.Text != "")
                    {
                        dt_DateOfCommExistIND = Convert.ToDateTime(txtDateOfCommencement.Text);
                    }
                    DateTime? dt_DateOfExpansionIND = null;
                    if (txtDateOfExpansion.Text != "")
                    {
                        dt_DateOfExpansionIND = Convert.ToDateTime(txtDateOfExpansion.Text);
                    }


                    NOCAP.BLL.Master.ApplicationAllowOrNotForApply obj_ApplicationAllowOrNotForApply = new NOCAP.BLL.Master.ApplicationAllowOrNotForApply(int_applicationTypeCode, int_applicationPurposeCode, int_applicationTypeCategoryCode, int_stateCode, int_districtCode, int_subDistrictCode, int_WaterQualityTypeCode, enm_WhetherGroundWaterUtilizationFor, enm_NOCObtainForExistIND, dt_DateOfCommExistIND, dt_DateOfExpansionIND);




                    switch (rbtnWhetherGroundWaterUtilization.SelectedValue)
                    {
                        case "NewIndustry":
                            lblGroundWaterUtilizationFor.Text = "New Industry";
                            RowNOCObtainedForExistINDRe.Visible = false;
                            RowDateOfCommencementRe.Visible = false;
                            RowDateOfExpansionRe.Visible = false;
                            break;
                        case "ExistingIndustry":
                            lblGroundWaterUtilizationFor.Text = "Existing Industry";
                            RowNOCObtainedForExistINDRe.Visible = true;
                            if (ddlNOCObtainedForExistIND.SelectedValue == "Y")
                            {
                                ddlNOCObtainedForExistINDRe.Text = "Yes";
                                RowDateOfCommencementRe.Visible = false;
                                RowDateOfExpansionRe.Visible = false;
                            }
                            else if (ddlNOCObtainedForExistIND.SelectedValue == "N")
                            {
                                RowDateOfCommencementRe.Visible = true;
                                RowDateOfExpansionRe.Visible = false;
                                ddlNOCObtainedForExistINDRe.Text = "No";
                                txtDateOfCommencementRe.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(txtDateOfCommencement.Text).ToString("dd/MM/yyyy"));
                            }
                            break;
                        case "ExpansionProgramExistingIndustry":
                            lblGroundWaterUtilizationFor.Text = "Expansion Program of Existing Industry";
                            RowNOCObtainedForExistINDRe.Visible = true;
                            if (ddlNOCObtainedForExistIND.SelectedValue == "Y")
                            {
                                ddlNOCObtainedForExistINDRe.Text = "Yes";
                                RowDateOfCommencementRe.Visible = false;
                                RowDateOfExpansionRe.Visible = false;
                            }
                            else if (ddlNOCObtainedForExistIND.SelectedValue == "N")
                            {
                                RowDateOfCommencementRe.Visible = true;
                                RowDateOfExpansionRe.Visible = true;
                                ddlNOCObtainedForExistINDRe.Text = "No";
                                txtDateOfCommencementRe.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(txtDateOfCommencement.Text).ToString("dd/MM/yyyy"));
                                txtDateOfExpansionRe.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(txtDateOfExpansion.Text).ToString("dd/MM/yyyy"));
                            }
                            break;
                        default:
                            break;
                    }











                    if (enm_WhetherGroundWaterUtilizationFor == NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry || enm_WhetherGroundWaterUtilizationFor == NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExpansionProgramOfExitingIndustry)
                    {
                        lblReason.Text = obj_ApplicationAllowOrNotForApply.DisplayMessage;
                    }


                    lblFinal.Text = HttpUtility.HtmlEncode(obj_ApplicationAllowOrNotForApply.ProceedForFinalApply);
                    if (lblFinal.Text == "Allow")
                    {
                        lblFinal.ForeColor = System.Drawing.Color.Green; lblFinal.Text = "Allowed";
                        lblReason.Text = "<span style='color:Red'><sup>*</sup></span>You are <b>Eligible</b> to apply for issuance of NOC for ground water withdrawal.";
                    }
                    else { lblFinal.ForeColor = System.Drawing.Color.Red; lblFinal.Text = "Not Allowed"; }

                    NOCAP.BLL.Master.WaterQuality obj_WaterQuality = new NOCAP.BLL.Master.WaterQuality(int_WaterQualityTypeCode);
                    if (obj_WaterQuality != null && obj_WaterQuality.WaterQualityDesc != "")
                    {
                        lblWaterQualityType.Text = HttpUtility.HtmlEncode(obj_WaterQuality.WaterQualityDesc);
                    }
                    CheckSegmentBAreaType(int_stateCode, int_districtCode, int_subDistrictCode);
                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);

                }
            }
        }
    }
    private void CheckSegmentBAreaType(int int_stateCode, int int_districtCode, int int_subDistrictCode)
    {
        if (ddlApplicationType.SelectedValue.ToString() != "4")
        {
            NOCAP.BLL.Master.SegmentBAreaType obj_SegmentBAreaType = new NOCAP.BLL.Master.SegmentBAreaType(int_stateCode, int_districtCode, int_subDistrictCode);
            if (obj_SegmentBAreaType.CreatedOn != null)
            {
                Panel6.Visible = true;
            }
            else
            {
                Panel6.Visible = false;
            }
        }
        else
        {
            Panel6.Visible = false;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            Response.Redirect("CheckEligibility.aspx");
        }
    }

    protected void ValidateDate(object sender, ServerValidateEventArgs e)
    {
        if (NOCAPExternalUtility.IsValidDate(e.Value))
        {
            e.IsValid = true;
        }
        else
        {
            e.IsValid = false;
        }
    }

    protected void ddlApplicationPurpose_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel6.Visible = false;
    }
    protected void ddlNOCObtainedForExistIND_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNOCObtainedForExistIND.SelectedValue == "N")
        {

            rvtxtDateOfCommencement.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");



            RowDateOfCommencement.Visible = true;
            rfvtxtDateOfCommencement.Enabled = true;
            txtDateOfExpansion.Text = "";
            if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExpansionProgramExistingIndustry")
            {
                rvtxtDateOfExpansion.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                RowDateOfExpansion.Visible = true;
                rfvtxtDateOfExpansion.Enabled = true;

            }
        }
        else
        {
            txtDateOfCommencement.Text = "";
            txtDateOfExpansion.Text = "";
            RowDateOfCommencement.Visible = false;
            rfvtxtDateOfCommencement.Enabled = false;

            RowDateOfExpansion.Visible = false;
            rfvtxtDateOfExpansion.Enabled = false;
        }
    }

    protected void rbtnWhetherGroundWaterUtilization_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel6.Visible = false;
        if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExistingIndustry")
        {
            RowNOCObtainedForExistIND.Visible = true;
            RowDateOfCommencement.Visible = false;
            RowDateOfExpansion.Visible = false;
            rfvddlNOCObtainedForExistIND.Enabled = true;
            ddlNOCObtainedForExistIND_SelectedIndexChanged(sender, e);
            //txtDateOfExpansion.Text = "";



        }
        else if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExpansionProgramExistingIndustry")
        {
            RowNOCObtainedForExistIND.Visible = true;
            RowDateOfCommencement.Visible = false;
            RowDateOfExpansion.Visible = false;
            rfvddlNOCObtainedForExistIND.Enabled = true;
            ddlNOCObtainedForExistIND_SelectedIndexChanged(sender, e);
        }
        else
        {
            RowDateOfCommencement.Visible = false;
            RowDateOfExpansion.Visible = false;
            RowNOCObtainedForExistIND.Visible = false;
            rfvddlNOCObtainedForExistIND.Enabled = false;
            txtDateOfCommencement.Text = "";
            txtDateOfExpansion.Text = "";
        }
    }
    protected void imgbtnCalendar_Click(object sender, ImageClickEventArgs e)
    {

    }

}