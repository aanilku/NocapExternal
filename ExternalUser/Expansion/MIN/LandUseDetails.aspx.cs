using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_Expansion_MIN_LandUseDetails : System.Web.UI.Page
{
    string strPageName = "MINLandUseDetails";
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
                if (lblMiningApplicationCodeFrom.Text.Trim() != "") { BindLandUseFormDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text)); }
                GenratAndBindLandUseTypeGridViewDetails();

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "function", "getSum('E');getSum('P');GetData();", true);
    }

    private void ValidationExpInit()
    {
        revtxtSalientFeaturesOfInActivity.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtSalientFeaturesOfInActivity.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtSalientFeaturesOfInActivity.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        revLengthtxtSalientFeaturesOfInActivity.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");

        revtxtLandUseDetailSurrounding.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtLandUseDetailSurrounding.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtLandUseDetailSurrounding.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        revLengthtxtLandUseDetailSurrounding.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");


        //revtxtTopographyAreaRegional.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtTopographyAreaRegional.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //revLengthtxtTopographyAreaRegional.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("1000");
        //revLengthtxtTopographyAreaRegional.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("1000");

        //revtxtTopographyProjectArea.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtTopographyProjectArea.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //revLengthtxtTopographyProjectArea.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("1000");
        //revLengthtxtTopographyProjectArea.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("1000");

        //revtxtDrainageAreaRegional.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtDrainageAreaRegional.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //revLengthtxtDrainageAreaRegional.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("1000");
        //revLengthtxtDrainageAreaRegional.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("1000");

        //revtxtDrainageProjectArea.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtDrainageProjectArea.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //revLengthtxtDrainageProjectArea.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("1000");
        //revLengthtxtDrainageProjectArea.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("1000");

        //revtxtSourceOfAvailability.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtSourceOfAvailability.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //revLengthtxtSourceOfAvailability.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("250");
        //revLengthtxtSourceOfAvailability.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("250");

        //revtxtAverageAnnualRainfall.ValidationExpression = ValidationUtility.txtValForDecimalValue("12", "2");
        //revtxtAverageAnnualRainfall.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("12", "2");

        //revtxtTownshipVillageWithin10kmRadius.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtTownshipVillageWithin10kmRadius.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        //revLengthtxtTownshipVillageWithin10kmRadius.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        //revLengthtxtTownshipVillageWithin10kmRadius.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");


    }

    private void GenratAndBindLandUseTypeGridViewDetails()
    {
        NOCAP.BLL.Master.LandUseType obj_landUseType = new NOCAP.BLL.Master.LandUseType();
        NOCAP.BLL.Master.LandUseType obj_landUseTypeBlank = new NOCAP.BLL.Master.LandUseType();
        List<NOCAP.BLL.Master.LandUseType> lst_landUseType = new List<NOCAP.BLL.Master.LandUseType>();
        try
        {
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
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally { }
    }

    private void BindLandUseFormDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.Expansion.MiningExpansionApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.Expansion.MiningExpansionApplication(lngA_ApplicationCode);
            txtSalientFeaturesOfInActivity.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.SailentFeatureofMiningActivity);
            txtLandUseDetailSurrounding.Text = obj_MiningNewApplication.LandUseDetailofSurrondings;
            //txtTopographyAreaRegional.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.TopographyofAreaRegional);
            //txtTopographyProjectArea.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.TopographyofProjectArea);

            //txtDrainageAreaRegional.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.DrainageAreaRegional);
            //txtDrainageProjectArea.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.DrainageProjectArea);

            //txtSourceOfAvailability.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.SourceOfAvailabilityOfSurfaceWater.SourceOfAvalabilityOfSurfaceWaterDesc);
            //txtAverageAnnualRainfall.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.AverageAnnualRainFallinArea));

            //txtTownshipVillageWithin10kmRadius.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.TownShipVillageWithin10KmRadius));
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally { }
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
            Server.Transfer("~/ExternalUser/Expansion/MIN/CommunicationAddress.aspx");
        }
    }
    protected void btnSaveAsDraft_Click1(object sender, EventArgs e)
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
                UpdateLandUseFormDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                GenratAndBindLandUseTypeGridViewDetails();
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
                if (UpdateLandUseFormDetails(Convert.ToInt64(lblMiningApplicationCodeFrom.Text)) == 1) 
                { Server.Transfer("~/ExternalUser/Expansion/MIN/De-WateringExistingStructure.aspx"); }
                else
                {
                }
            }
        }

    }
    protected void gvLandUseType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (lblMiningApplicationCodeFrom.Text.Trim() != "")
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
                    NOCAP.BLL.Mining.Expansion.MiningExpansionLandUse obj_tempMiningNewLandUse = new NOCAP.BLL.Mining.Expansion.MiningExpansionLandUse(Convert.ToInt32(lblMiningApplicationCodeFrom.Text), Convert.ToInt32(gvLandUseType.DataKeys[gvRow.RowIndex].Value.ToString()));
                    txtExisting_Temp.Text = Convert.ToString(obj_tempMiningNewLandUse.LandUseExist);
                    txtProposed_Temp.Text = Convert.ToString(obj_tempMiningNewLandUse.LandUseProposed);
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl = (Label)e.Row.FindControl("lblLandUseDetails");
                    lbl.Text =HttpUtility.HtmlEncode(lbl.Text + " <span class='Coumpulsory'>*</span>");
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally { }
    }

    private int UpdateLandUseFormDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.Expansion.MiningExpansionApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.Expansion.MiningExpansionApplication(lngA_ApplicationCode);
            obj_MiningNewApplication.SailentFeatureofMiningActivity = txtSalientFeaturesOfInActivity.Text;
            obj_MiningNewApplication.LandUseDetailofSurrondings = txtLandUseDetailSurrounding.Text;

            List<NOCAP.BLL.Mining.Expansion.MiningExpansionLandUse> lst_MiningNewLandUseList = new List<NOCAP.BLL.Mining.Expansion.MiningExpansionLandUse>();
            foreach (GridViewRow gvRow in gvLandUseType.Rows)
            {
                TextBox txt_Existing = (TextBox)gvRow.FindControl("txtExisting");
                TextBox txt_Proposed = (TextBox)gvRow.FindControl("txtProposed");
                if (!(txt_Existing.Text.Trim() == "" && txt_Proposed.Text.Trim() == ""))
                {
                    NOCAP.BLL.Mining.Expansion.MiningExpansionLandUse obj_tempMiningNewLandUse = new NOCAP.BLL.Mining.Expansion.MiningExpansionLandUse();
                    obj_tempMiningNewLandUse.LandUseTypeCode = Convert.ToInt32(gvLandUseType.DataKeys[gvRow.RowIndex].Value.ToString());
                    if (txt_Existing.Text.Trim() == "")
                    {
                        obj_tempMiningNewLandUse.LandUseExist = null;
                    }
                    else
                    {
                        obj_tempMiningNewLandUse.LandUseExist = Convert.ToDecimal(txt_Existing.Text.Trim());
                    }
                    if (txt_Proposed.Text.Trim() == "")
                    {
                        obj_tempMiningNewLandUse.LandUseProposed = null;
                    }
                    else
                    {
                        obj_tempMiningNewLandUse.LandUseProposed = Convert.ToDecimal(txt_Proposed.Text.Trim());
                    }
                    lst_MiningNewLandUseList.Add(obj_tempMiningNewLandUse);
                }
            }
            NOCAP.BLL.Mining.Expansion.MiningExpansionLandUse[] arr_tempLandUseListBLL = new NOCAP.BLL.Mining.Expansion.MiningExpansionLandUse[lst_MiningNewLandUseList.Count];
            lst_MiningNewLandUseList.CopyTo(arr_tempLandUseListBLL);
            obj_MiningNewApplication.LandUseDetailOfExistingProposed.MiningExpansionLandUseList = arr_tempLandUseListBLL;

            obj_MiningNewApplication.TopographyofAreaRegional = "";// txtTopographyAreaRegional.Text;
            obj_MiningNewApplication.TopographyofProjectArea = "";// txtTopographyProjectArea.Text;
            obj_MiningNewApplication.DrainageAreaRegional = "";// txtDrainageAreaRegional.Text;
            obj_MiningNewApplication.DrainageProjectArea = "";// txtDrainageProjectArea.Text;
            obj_MiningNewApplication.SourceOfAvailabilityOfSurfaceWater.SourceOfAvalabilityOfSurfaceWaterDesc = "";// txtSourceOfAvailability.Text;
            obj_MiningNewApplication.AverageAnnualRainFallinArea = null;// Convert.ToDecimal(txtAverageAnnualRainfall.Text);
            obj_MiningNewApplication.TownShipVillageWithin10KmRadius = "";// txtTownshipVillageWithin10kmRadius.Text;

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_MiningNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_MiningNewApplication.Update() == 1)
            {

                strActionName = "SaveAsDraft";
                strStatus = "Record Save Successfully !";

                lblMessage.Text = "Saved Successfully";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {

                strActionName = "SaveAsDraft";
                strStatus = "Record Save Failed !";

                lblMessage.Text = obj_MiningNewApplication.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
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

}