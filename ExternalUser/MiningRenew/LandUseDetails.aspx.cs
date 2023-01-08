using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_MiningRenew_LandUseDetails : System.Web.UI.Page
{
    string strPageName = "MINRenLandUseDetails";
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
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    BindLandUseFormDetails(Convert.ToInt32(lblMiningApplicationCodeFrom.Text));
                }
                
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

        revtxtLandUseDetailSurrounding.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtLandUseDetailSurrounding.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtLandUseDetailSurrounding.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        revLengthtxtLandUseDetailSurrounding.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");


        revtxtTopographyAreaRegional.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtTopographyAreaRegional.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtTopographyAreaRegional.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("1000");
        revLengthtxtTopographyAreaRegional.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("1000");

        revtxtTopographyProjectArea.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtTopographyProjectArea.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtTopographyProjectArea.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("1000");
        revLengthtxtTopographyProjectArea.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("1000");

        revtxtDrainageAreaRegional.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtDrainageAreaRegional.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtDrainageAreaRegional.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("1000");
        revLengthtxtDrainageAreaRegional.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("1000");

        revtxtDrainageProjectArea.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtDrainageProjectArea.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtDrainageProjectArea.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("1000");
        revLengthtxtDrainageProjectArea.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("1000");

        revtxtTownshipVillageWithin10kmRadius.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtTownshipVillageWithin10kmRadius.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtTownshipVillageWithin10kmRadius.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        revLengthtxtTownshipVillageWithin10kmRadius.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");
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
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Mining.New.Application.MiningNewApplication objMiningNewApplication = obj_MiningRenewApplication.GetFirstMiningApplication();

            if (objMiningNewApplication != null)
            {
                lblHeadingProjName.Text = HttpUtility.HtmlEncode(objMiningNewApplication.NameOfMining);
            }
            txtLandUseDetailSurrounding.Text = HttpUtility.HtmlEncode(obj_MiningRenewApplication.ChangeLandUseDetailofSurrondings);

            switch (obj_MiningRenewApplication.WhetherChangeTopoOfAreaRegional)
            {
                case NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.WhetherChangeTopo.Yes:
                    ddlChangeTopo.SelectedValue = "Yes";
                    txtTopographyAreaRegional.Enabled = true;
                    txtTopographyProjectArea.Enabled = true;
                    rfvtxtTopographyAreaRegional.Enabled = true;
                    rfvtxtTopographyProjectArea.Enabled = true;
                    txtTopographyAreaRegional.Text = HttpUtility.HtmlEncode(obj_MiningRenewApplication.ChangeTopographyofAreaRegional);
                    txtTopographyProjectArea.Text = HttpUtility.HtmlEncode(obj_MiningRenewApplication.ChangeTopographyofProjectArea);
                    break;
                case NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.WhetherChangeTopo.No:
                    ddlChangeTopo.SelectedValue = "No";
                    txtTopographyAreaRegional.Enabled = false;
                    txtTopographyProjectArea.Enabled = false;
                    rfvtxtTopographyAreaRegional.Enabled = false;
                    rfvtxtTopographyProjectArea.Enabled = false;
                    txtTopographyAreaRegional.Text = string.Empty;
                    txtTopographyProjectArea.Text = string.Empty;
                    break;
            }

            switch (obj_MiningRenewApplication.WhetherChangeDrainageAreaRegional)
            {
                case NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.WhetherChangeDrainage.Yes:
                    ddlChangeDrainage.SelectedValue = "Yes";
                    txtDrainageAreaRegional.Enabled = true;
                    txtDrainageProjectArea.Enabled = true;
                    rfvtxtDrainageAreaRegional.Enabled = true;
                    rfvtxtDrainageProjectArea.Enabled = true;
                    txtDrainageAreaRegional.Text = HttpUtility.HtmlEncode(obj_MiningRenewApplication.ChangeDrainageAreaRegional);
                    txtDrainageProjectArea.Text = HttpUtility.HtmlEncode(obj_MiningRenewApplication.ChangeDrainageProjectArea);
                    break;
                case NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.WhetherChangeDrainage.No:
                    ddlChangeDrainage.SelectedValue = "No";
                    txtDrainageAreaRegional.Enabled = false;
                    txtDrainageProjectArea.Enabled = false;
                    rfvtxtDrainageAreaRegional.Enabled = false;
                    rfvtxtDrainageProjectArea.Enabled = false;

                    txtDrainageAreaRegional.Text = string.Empty;
                    txtDrainageProjectArea.Text = string.Empty;
                    break;
            }

            txtTownshipVillageWithin10kmRadius.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.TownShipVillageWithin10KmRadius));
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
            Server.Transfer("~/ExternalUser/MiningRenew/ExistingNOCIssued.aspx");
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
                {
                    Server.Transfer("~/ExternalUser/MiningRenew/De-WateringExistingStructure.aspx");
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
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADLandUse obj_tempMiningRenewLandUse = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADLandUse(Convert.ToInt32(lblMiningApplicationCodeFrom.Text), Convert.ToInt32(gvLandUseType.DataKeys[gvRow.RowIndex].Value.ToString()));
                    txtExisting_Temp.Text = Convert.ToString(obj_tempMiningRenewLandUse.LandUseExist);
                    txtProposed_Temp.Text = Convert.ToString(obj_tempMiningRenewLandUse.LandUseProposed);
                   
                   
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl = (Label)e.Row.FindControl("lblLandUseDetails");
                    //Label lbl2 = new Label();
                    //lbl2.Text = "<span class='Coumpulsory'>*</span>";
                    lbl.Text = HttpUtility.HtmlEncode(lbl.Text + "<span class='Coumpulsory'>*</span>");//lbl2.Text;
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
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);

            obj_MiningRenewApplication.ChangeLandUseDetailofSurrondings = txtLandUseDetailSurrounding.Text;

            List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADLandUse> lst_MiningRenewLandUseList = new List<NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADLandUse>();
            foreach (GridViewRow gvRow in gvLandUseType.Rows)
            {
                TextBox txt_Existing = (TextBox)gvRow.FindControl("txtExisting");
                TextBox txt_Proposed = (TextBox)gvRow.FindControl("txtProposed");
                if (!(txt_Existing.Text.Trim() == "" && txt_Proposed.Text.Trim() == ""))
                {
                    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADLandUse obj_tempMiningRenewLandUse = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADLandUse();
                    obj_tempMiningRenewLandUse.LandUseTypeCode = Convert.ToInt32(gvLandUseType.DataKeys[gvRow.RowIndex].Value.ToString());
                    if (txt_Existing.Text.Trim() == "")
                    {
                        obj_tempMiningRenewLandUse.LandUseExist = null;
                    }
                    else
                    {
                        obj_tempMiningRenewLandUse.LandUseExist = Convert.ToDecimal(txt_Existing.Text.Trim());
                    }
                    if (txt_Proposed.Text.Trim() == "")
                    {
                        obj_tempMiningRenewLandUse.LandUseProposed = null;
                    }
                    else
                    {
                        obj_tempMiningRenewLandUse.LandUseProposed = Convert.ToDecimal(txt_Proposed.Text.Trim());
                    }
                    lst_MiningRenewLandUseList.Add(obj_tempMiningRenewLandUse);
                }
            }
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADLandUse[] arr_tempLandUseListBLL = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADLandUse[lst_MiningRenewLandUseList.Count];
            lst_MiningRenewLandUseList.CopyTo(arr_tempLandUseListBLL);
            obj_MiningRenewApplication.LandUseDetailOfExistingAdditional.MiningRenewLandUseList = arr_tempLandUseListBLL;

            switch (ddlChangeTopo.SelectedValue)
            {
                case "Yes":
                    obj_MiningRenewApplication.WhetherChangeTopoOfAreaRegional = NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.WhetherChangeTopo.Yes;
                    obj_MiningRenewApplication.ChangeTopographyofAreaRegional = txtTopographyAreaRegional.Text;
                    obj_MiningRenewApplication.ChangeTopographyofProjectArea = txtTopographyProjectArea.Text;
                    break;
                case "No":
                    obj_MiningRenewApplication.WhetherChangeTopoOfAreaRegional = NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.WhetherChangeTopo.No;
                    obj_MiningRenewApplication.ChangeTopographyofAreaRegional = string.Empty;
                    obj_MiningRenewApplication.ChangeTopographyofProjectArea = string.Empty;
                    break;
            }


            switch (ddlChangeDrainage.SelectedValue)
            {
                case "Yes":
                    obj_MiningRenewApplication.WhetherChangeDrainageAreaRegional = NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.WhetherChangeDrainage.Yes;
                    obj_MiningRenewApplication.ChangeDrainageAreaRegional = txtDrainageAreaRegional.Text;
                    obj_MiningRenewApplication.ChangeDrainageProjectArea = txtDrainageProjectArea.Text;
                    break;
                case "No":
                    obj_MiningRenewApplication.WhetherChangeDrainageAreaRegional = NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.WhetherChangeDrainage.No;
                    obj_MiningRenewApplication.ChangeDrainageAreaRegional = string.Empty;
                    obj_MiningRenewApplication.ChangeDrainageProjectArea = string.Empty;
                    break;
            }

            obj_MiningRenewApplication.TownShipVillageWithin10KmRadius = txtTownshipVillageWithin10kmRadius.Text;

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_MiningRenewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_MiningRenewApplication.Update() == 1)
            {
                strActionName = "SaveAsDraft";
                strStatus = "Record Saved Successfully !";

                lblMessage.Text = "Saved Successfully";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {

                strActionName = "SaveAsDraft";
                strStatus = "Record Saving Failed !";

                lblMessage.Text = obj_MiningRenewApplication.CustumMessage;
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
    protected void ddlChangeTopo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            if (ddlChangeTopo.SelectedValue == "No")
            {
                txtTopographyAreaRegional.Enabled = false;
                txtTopographyProjectArea.Enabled = false;
                txtTopographyAreaRegional.Text = string.Empty;
                txtTopographyProjectArea.Text = string.Empty;

                rfvtxtTopographyAreaRegional.Enabled = false;
                rfvtxtTopographyProjectArea.Enabled = false;
            }
            else
            {
                txtTopographyAreaRegional.Enabled = true;
                txtTopographyProjectArea.Enabled = true;

                rfvtxtTopographyAreaRegional.Enabled = true;
                rfvtxtTopographyProjectArea.Enabled = true;

            }
        }
    }
    protected void ddlChangeDrainage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            if (ddlChangeDrainage.SelectedValue == "No")
            {
                txtDrainageAreaRegional.Enabled = false;
                txtDrainageProjectArea.Enabled = false;
                txtDrainageAreaRegional.Text = string.Empty;
                txtDrainageProjectArea.Text = string.Empty;

                rfvtxtDrainageAreaRegional.Enabled = false;
                rfvtxtDrainageProjectArea.Enabled = false;
            }
            else
            {
                txtDrainageAreaRegional.Enabled = true;
                txtDrainageProjectArea.Enabled = true;

                rfvtxtDrainageAreaRegional.Enabled = true;
                rfvtxtDrainageProjectArea.Enabled = true;
            }
        }
    }
}