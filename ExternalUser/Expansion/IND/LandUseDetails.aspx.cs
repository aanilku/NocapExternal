using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using NOCAP;
using System.Text;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_Expansion_IND_LandUseDetails : System.Web.UI.Page
{
    string strPageName = "INDLandUseDetails";
    string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ValidationExpInit();
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;

            lblMessage.Text = "";
            try
            {
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
                    BindLandUseFormDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text), sender, e);
                }
                GenratAndBindLandUseTypeGridViewDetails();

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
        revtxtSalientFeaturesOfInActivity.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtSalientFeaturesOfInActivity.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //revtxtAverageAnnualRainfall.ValidationExpression = ValidationUtility.txtValForDecimalValue("12", "2");
        //revtxtAverageAnnualRainfall.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("12", "2");

        revLengthtxtSalientFeaturesOfInActivity.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        revLengthtxtSalientFeaturesOfInActivity.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");

      //  revtxtDrainageInTheArea.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtDrainageInTheArea.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //revLengthtxtDrainageInTheArea.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("250");
        //revLengthtxtDrainageInTheArea.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("250");

        //revtxtTownshipVillageWithin2kmRadius.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtTownshipVillageWithin2kmRadius.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //revLengthtxtTownshipVillageWithin2kmRadius.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        //revLengthtxtTownshipVillageWithin2kmRadius.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");

        //revtxtSourceOfAvailability.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtSourceOfAvailability.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //revLengthtxtSourceOfAvailability.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("250");
        //revLengthtxtSourceOfAvailability.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("250");
    }

    private void GenratAndBindLandUseTypeGridViewDetails()
    {
        try
        {
            NOCAP.BLL.Master.LandUseType obj_landUseType = new NOCAP.BLL.Master.LandUseType();
            NOCAP.BLL.Master.LandUseType obj_landUseTypeBlank = new NOCAP.BLL.Master.LandUseType();
            List<NOCAP.BLL.Master.LandUseType> lst_landUseType = new List<NOCAP.BLL.Master.LandUseType>();

            int int_status = 0;
            int_status = obj_landUseType.GetALL(NOCAP.BLL.Master.LandUseType.SortingField.LandUseTypeDesc);

            NOCAP.BLL.Master.LandUseType[] arr_landUseType;
            arr_landUseType = obj_landUseType.LandUseTypeCollection;

            if ((int_status == 1))
            {
                if (arr_landUseType.Count() > 0)
                {

                    gvLandUseType.DataSource = arr_landUseType;
                    gvLandUseType.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "function", "getSum('E');getSum('P');GetData();", true);
                }
                else
                {
                    lst_landUseType.Add(obj_landUseTypeBlank);
                    gvLandUseType.DataSource = lst_landUseType;
                    gvLandUseType.DataBind();
                    int int_NoOfCol = 0;
                    int_NoOfCol = gvLandUseType.Rows[0].Cells.Count;
                    gvLandUseType.Rows[0].Cells.Clear();
                    gvLandUseType.Rows[0].Cells.Add(new TableCell());
                    gvLandUseType.Rows[0].Cells[0].ColumnSpan = int_NoOfCol;
                    gvLandUseType.Rows[0].Cells[0].Text = "No Records exsist in LandUseType";
                }

            }

            else
            {
                lblMessage.Text = obj_landUseType.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
            //lblMessage.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    private void BindLandUseFormDetails(long lngA_ApplicationCode, Object sender, EventArgs e)
    {
        try
        {
            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(lngA_ApplicationCode);

            txtSalientFeaturesOfInActivity.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.SalientFeatureOfIndustrialActivity);
            //txtDrainageInTheArea.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.DrainageInTheArea);
            //txtSourceOfAvailability.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.SourceOfAvaillabilityOfSurfaceWaterForIndustrialUse.SourceOfAvalabilityOfSurfaceWaterDesc);
            //txtAverageAnnualRainfall.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_industrialNewApplication.AverageAnnualRainfallInTheArea));
            //txtTownshipVillageWithin2kmRadius.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.TownshipVillageWithin2KM);

            switch (obj_industrialNewApplication.GroundWaterUtilizationFor)
            {
                case NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication.GroundWaterUtilizationForOption.NewIndustry:
                    rbtnWhetherGroundWaterUtilization.SelectedValue = "NewIndustry";
                    break;
                case NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication.GroundWaterUtilizationForOption.ExistingIndustry:
                    rbtnWhetherGroundWaterUtilization.SelectedValue = "ExistingIndustry";
                    break;
                case NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication.GroundWaterUtilizationForOption.ExpansionProgramOfExitingIndustry:
                    rbtnWhetherGroundWaterUtilization.SelectedValue = "ExpansionProgramExistingIndustry";
                    break;

            }
            rbtnWhetherGroundWaterUtilization_SelectedIndexChanged(sender, e);
            if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExistingIndustry" || rbtnWhetherGroundWaterUtilization.SelectedValue == "ExpansionProgramExistingIndustry")
            {
                switch (obj_industrialNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND)
                {
                    case NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.Yes:
                        ddlNOCObtainedForExistIND.SelectedValue = "Y";
                        break;
                    case NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No:
                        ddlNOCObtainedForExistIND.SelectedValue = "N";
                        break;
                    default:
                        break;
                }
                ddlNOCObtainedForExistIND_SelectedIndexChanged(sender, e);
                if (ddlNOCObtainedForExistIND.SelectedValue == "N")
                {
                    if (!Convert.IsDBNull(obj_industrialNewApplication.DateOfCommencement))
                    {
                        txtDateOfCommencement.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_industrialNewApplication.DateOfCommencement).ToString("dd/MM/yyyy"));
                    }
                }
            }

        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
            //lblMessage.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    private int UpdateLandUseFormDetails(long lngA_ApplicationCode)
    {
        try
        {
            strActionName = "UpdateLandUseDatail";
            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication(lngA_ApplicationCode);

            obj_industrialNewApplication.SalientFeatureOfIndustrialActivity = txtSalientFeaturesOfInActivity.Text;
            obj_industrialNewApplication.DrainageInTheArea = "";// txtDrainageInTheArea.Text;
            obj_industrialNewApplication.SourceOfAvaillabilityOfSurfaceWaterForIndustrialUse.SourceOfAvalabilityOfSurfaceWaterDesc = "";// txtSourceOfAvailability.Text;
            obj_industrialNewApplication.AverageAnnualRainfallInTheArea = null;// Convert.ToDecimal(txtAverageAnnualRainfall.Text);
           obj_industrialNewApplication.TownshipVillageWithin2KM ="";// txtTownshipVillageWithin2kmRadius.Text;
            

            List<NOCAP.BLL.Industrial.Expansion.IndustrialExpansionLandUse> lst_IndustrialNewLandUseList = new List<NOCAP.BLL.Industrial.Expansion.IndustrialExpansionLandUse>();
            foreach (GridViewRow gvRow in gvLandUseType.Rows)
            {
                TextBox txt_Existing = (TextBox)gvRow.FindControl("txtExisting");
                TextBox txt_Proposed = (TextBox)gvRow.FindControl("txtProposed");
                if (!(txt_Existing.Text.Trim() == "" && txt_Proposed.Text.Trim() == ""))
                {
                    NOCAP.BLL.Industrial.Expansion.IndustrialExpansionLandUse obj_tempIndustrialNewLandUse = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionLandUse();
                    //obj_tempIndustrialNewLandUse.IndustrialNewApplicationCode = obj_industrialNewApplication.IndustrialNewApplicationCode;
                    obj_tempIndustrialNewLandUse.LandUseTypeCode = Convert.ToInt32(gvLandUseType.DataKeys[gvRow.RowIndex].Value.ToString());
                    if (txt_Existing.Text.Trim() == "")
                    {
                        obj_tempIndustrialNewLandUse.LandUseExist = null;
                    }
                    else
                    {
                        obj_tempIndustrialNewLandUse.LandUseExist = Convert.ToDecimal(txt_Existing.Text.Trim());
                    }
                    if (txt_Proposed.Text.Trim() == "")
                    {
                        obj_tempIndustrialNewLandUse.LandUseProposed = null;
                    }
                    else
                    {
                        obj_tempIndustrialNewLandUse.LandUseProposed = Convert.ToDecimal(txt_Proposed.Text.Trim());
                    }
                    lst_IndustrialNewLandUseList.Add(obj_tempIndustrialNewLandUse);

                }
            }

            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionLandUse[] arr_tempLandUseListBLL = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionLandUse[lst_IndustrialNewLandUseList.Count];
            lst_IndustrialNewLandUseList.CopyTo(arr_tempLandUseListBLL);
            obj_industrialNewApplication.LandUseDetailOfExistingProposed.IndustrialNewLandUseList = arr_tempLandUseListBLL;

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
    protected void btnNext_Click(object sender, EventArgs e)
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
                if (UpdateLandUseFormDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text)) == 1)
                {
                    Server.Transfer("~/ExternalUser/Expansion/IND/WaterRequirementDetails.aspx");
                }
            }
        }
    }

    protected void btnSaveAsDraft_Click1(object sender, EventArgs e)
    {
        //AddLandUseDetails();
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

                    UpdateLandUseFormDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);

                }
                finally
                {
                    GenratAndBindLandUseTypeGridViewDetails();
                }

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
            Server.Transfer("~/ExternalUser/Expansion/IND/CommunicationAddress.aspx");
        }
    }


    protected void gvLandUseType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (lblIndustialApplicationCodeFrom.Text.Trim() != "")
            {
                foreach (GridViewRow gvRow in gvLandUseType.Rows)
                {
                    RegularExpressionValidator revtxtExisting = (RegularExpressionValidator)gvRow.FindControl("revtxtExisting");
                    revtxtExisting.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
                    revtxtExisting.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");
                    RegularExpressionValidator revtxtProposed = (RegularExpressionValidator)gvRow.FindControl("revtxtProposed");
                    revtxtProposed.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
                    revtxtProposed.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");


                    TextBox txtExisting_Temp = (TextBox)gvRow.FindControl("txtExisting");
                    TextBox txtProposed_Temp = (TextBox)gvRow.FindControl("txtProposed");
                    NOCAP.BLL.Industrial.Expansion.IndustrialExpansionLandUse obj_tempIndustrialNewLandUse = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionLandUse(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text), Convert.ToInt32(gvLandUseType.DataKeys[gvRow.RowIndex].Value.ToString()));
                    txtExisting_Temp.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_tempIndustrialNewLandUse.LandUseExist));
                    txtProposed_Temp.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_tempIndustrialNewLandUse.LandUseProposed));

                }


            }
        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
            //lblMessage.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    protected void txtDrainageInTheArea_TextChanged(object sender, EventArgs e)
    {

    }
    protected void rbtnWhetherGroundWaterUtilization_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExistingIndustry" || rbtnWhetherGroundWaterUtilization.SelectedValue == "ExpansionProgramExistingIndustry")
        {
            RowNOCObtainedForExistIND.Visible = true;
            RowDateOfCommencement.Visible = false;
            ddlNOCObtainedForExistIND_SelectedIndexChanged(sender, e);
        }
        else
        {
            RowDateOfCommencement.Visible = false;
            RowNOCObtainedForExistIND.Visible = false;
        }
    }
    protected void ddlNOCObtainedForExistIND_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNOCObtainedForExistIND.SelectedValue == "N")
        {
            RowDateOfCommencement.Visible = true;
        }
        else
        {
            RowDateOfCommencement.Visible = false;
        }
    }
}














