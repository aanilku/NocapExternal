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

public partial class ExternalUser_IndustrialNew_LandUseDetails : System.Web.UI.Page
{
    string strPageName = "INFLandUseDetails";
    string strActionName = "";
    string strStatus = "";
    //string category = "";
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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblInfrastructureApplicationCodeFrom");
                        if (SourceLabel != null) { lblInfrastructureApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit")
                { BindLandUseFormDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text), sender, e); }
                GenratAndBindLandUseTypeGridViewDetails();

                //btnSaveAsDraft.Attributes.Add("onclick", "javascript:ValidateCheckBoxList();");

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "function", "GetData();", true);
    }

    private void ValidationExpInit()
    {
        revtxtSourceOfAvailability.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtSourceOfAvailability.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revLengthtxtSourceOfAvailability.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("250");
        revLengthtxtSourceOfAvailability.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("250");

        //revtxtAverageAnnualRainfall.ValidationExpression = ValidationUtility.txtValForDecimalValue("12", "2");
        //revtxtAverageAnnualRainfall.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("12", "2");
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
                    ScriptManager.RegisterStartupScript(this, GetType(), "function", "getSum('E');getSum('P');", true);
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

    private void BindLandUseFormDetails(long lngA_ApplicationCode, Object sender, EventArgs e)
    {
        try
        {
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            txtSourceOfAvailability.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.SourceOfAvailabilityOfSurfaceWaterForInfrastructureUse.SourceOfAvalabilityOfSurfaceWaterDesc);
            //  txtAverageAnnualRainfall.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_infrastructureNewApplication.AverageAnnualRainfallInTheArea));
            lblTypeOfInfrastructure.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.ApplicationTypeCategory(obj_infrastructureNewApplication.ApplicationTypeCode, obj_infrastructureNewApplication.ApplicationTypeCategoryCode).ApplicationTypeCategoryDesc);

                switch (obj_infrastructureNewApplication.GroundWaterUtilizationFor)
                {
                    case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.NewProject:
                        rbtnWhetherGroundWaterUtilization.SelectedValue = "NewIndustry";
                        break;
                    case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.ExistingIndustry:
                        rbtnWhetherGroundWaterUtilization.SelectedValue = "ExistingIndustry";
                        break;
                    case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.ExpansionProgramme:
                        rbtnWhetherGroundWaterUtilization.SelectedValue = "ExpansionProgramExistingIndustry";
                        break;

                }
                rbtnWhetherGroundWaterUtilization_SelectedIndexChanged(sender, e);
                //if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExistingIndustry")
                //{
                //    if (!Convert.IsDBNull(obj_infrastructureNewApplication.DateOfCommencement))
                //    {
                //        txtDateOfCommencement.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_infrastructureNewApplication.DateOfCommencement).ToString("dd/MM/yyyy"));
                //    }
                //}
                //else
                if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExistingIndustry" || rbtnWhetherGroundWaterUtilization.SelectedValue == "ExpansionProgramExistingIndustry")
                {
                    switch (obj_infrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND)
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
                        if (!Convert.IsDBNull(obj_infrastructureNewApplication.DateOfCommencement))
                        {
                            txtDateOfCommencement.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_infrastructureNewApplication.DateOfCommencement).ToString("dd/MM/yyyy"));
                        }
                    }
                }

            if (obj_infrastructureNewApplication.DrinkingUse == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.UseOption.Yes)
                {
                    chkGWUtilizationUse.Items[0].Selected = true;
                }
                if (obj_infrastructureNewApplication.ConstructionUse == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.UseOption.Yes)
                {
                    chkGWUtilizationUse.Items[1].Selected = true;
                }
                if (obj_infrastructureNewApplication.CommercialUse == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.UseOption.Yes)
                {
                    chkGWUtilizationUse.Items[2].Selected = true;
                }
                if (obj_infrastructureNewApplication.DewaterUse == NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.UseOption.Yes)
                {
                    chkGWUtilizationUse.Items[3].Selected = true;
                }

                
            //if (trWhetherGroundWaterUtilization.Visible == false)
            //{
            //    RowNOCObtainedForExistIND.Visible = false;
            //    RowDateOfCommencement.Visible = false;
            //}

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


            strActionName = "UpdateLandUseDetails";
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            obj_infrastructureNewApplication.SourceOfAvailabilityOfSurfaceWaterForInfrastructureUse.SourceOfAvalabilityOfSurfaceWaterDesc = txtSourceOfAvailability.Text;
            obj_infrastructureNewApplication.AverageAnnualRainfallInTheArea = null;// Convert.ToDecimal(txtAverageAnnualRainfall.Text);


                var checkbox = (from item in chkGWUtilizationUse.Items.Cast<ListItem>() where item.Selected select item).ToArray();
                if (checkbox.Count() <= 0)
                {
                    lblMessage.Text = "Please select at least one check box";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return 0;
                }
                else
                {
                    if (chkGWUtilizationUse.Items[0].Selected)
                    {
                    obj_infrastructureNewApplication.DrinkingUse = NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.UseOption.Yes;
                    }
                    else
                    {
                    obj_infrastructureNewApplication.DrinkingUse = NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.UseOption.No;
                    }
                    if (chkGWUtilizationUse.Items[1].Selected)
                    {
                        obj_infrastructureNewApplication.ConstructionUse = NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.UseOption.Yes;
                    }
                    else { obj_infrastructureNewApplication.ConstructionUse = NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.UseOption.No; }
                    if (chkGWUtilizationUse.Items[2].Selected)
                    {
                        obj_infrastructureNewApplication.CommercialUse = NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.UseOption.Yes; 
                    }
                    else
                    { obj_infrastructureNewApplication.CommercialUse = NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.UseOption.No; }
                    if (chkGWUtilizationUse.Items[3].Selected)
                    {
                        obj_infrastructureNewApplication.DewaterUse = NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.UseOption.Yes;
                    }
                    else
                    { obj_infrastructureNewApplication.DewaterUse = NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.UseOption.No; }
                }
            switch (rbtnWhetherGroundWaterUtilization.SelectedValue)
            {
                case "NewIndustry":
                    obj_infrastructureNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.NewProject;
                    obj_infrastructureNewApplication.DateOfCommencement = null;
                    obj_infrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                    break;
                case "ExistingIndustry":
                    obj_infrastructureNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.ExistingIndustry;
                    //obj_infrastructureNewApplication.NOCObtainForExistIndustry.NOCObtainForExistIND = NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.NotDefined;
                    break;
                case "ExpansionProgramExistingIndustry":
                    obj_infrastructureNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.ExpansionProgramme;
                    break;
                default:
                    obj_infrastructureNewApplication.GroundWaterUtilizationFor = NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.NotDefined;
                    break;
            }

            List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADLandUse> lst_InfrastructureNewLandUseList = new List<NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADLandUse>();
            foreach (GridViewRow gvRow in gvLandUseType.Rows)
            {
                TextBox txt_Existing = (TextBox)gvRow.FindControl("txtExisting");
                TextBox txt_Proposed = (TextBox)gvRow.FindControl("txtProposed");
                if (!(txt_Existing.Text.Trim() == "" && txt_Proposed.Text.Trim() == ""))
                {
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADLandUse obj_tempInfrastructureNewLandUse = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADLandUse();
                    obj_tempInfrastructureNewLandUse.LandUseTypeCode = Convert.ToInt32(gvLandUseType.DataKeys[gvRow.RowIndex].Value.ToString());
                    if (txt_Existing.Text.Trim() == "")
                    {
                        obj_tempInfrastructureNewLandUse.LandUseExist = null;
                    }
                    else
                    {
                        obj_tempInfrastructureNewLandUse.LandUseExist = Convert.ToDecimal(txt_Existing.Text.Trim());
                    }
                    if (txt_Proposed.Text.Trim() == "")
                    {
                        obj_tempInfrastructureNewLandUse.LandUseProposed = null;
                    }
                    else
                    {
                        obj_tempInfrastructureNewLandUse.LandUseProposed = Convert.ToDecimal(txt_Proposed.Text.Trim());
                    }
                    lst_InfrastructureNewLandUseList.Add(obj_tempInfrastructureNewLandUse);
                }
            }

            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADLandUse[] arr_tempLandUseListBLL = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADLandUse[lst_InfrastructureNewLandUseList.Count];
            lst_InfrastructureNewLandUseList.CopyTo(arr_tempLandUseListBLL);
            obj_infrastructureNewApplication.LandUseDetailOfExistingProposed.InfrastructureNewLandUseList = arr_tempLandUseListBLL;

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_infrastructureNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (obj_infrastructureNewApplication.Update() == 1)
            {
                strStatus = "Update Successfully";
                lblMessage.Text = "Successfully Saved";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                strStatus = "Update Unsuccessfull";
                lblMessage.Text = obj_infrastructureNewApplication.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
        }
        catch (Exception ex)
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
                if (UpdateLandUseFormDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text)) == 1) { Server.Transfer("~/ExternalUser/InfrastructureNew/WaterRequirementDetails.aspx"); }
                else { }
            }
        }
    }

    protected void btnSaveAsDraft_Click1(object sender, EventArgs e)
    {
        //AddLandUseDetails();
        if (Page.IsValid)
        {
            //chkGWUtilizationUse.Items.Count


            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                UpdateLandUseFormDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                GenratAndBindLandUseTypeGridViewDetails();
                
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
            Server.Transfer("~/ExternalUser/InfrastructureNew/CommunicationAddress.aspx");
        }
    }


    protected void gvLandUseType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (lblInfrastructureApplicationCodeFrom.Text.Trim() != "")
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
                    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADLandUse obj_tempInfrastructureNewLandUse = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADLandUse(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text), Convert.ToInt32(gvLandUseType.DataKeys[gvRow.RowIndex].Value.ToString()));
                    txtExisting_Temp.Text = Convert.ToString(obj_tempInfrastructureNewLandUse.LandUseExist);
                    txtProposed_Temp.Text = Convert.ToString(obj_tempInfrastructureNewLandUse.LandUseProposed);
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally { }
    }
    protected void rbtnWhetherGroundWaterUtilization_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (rbtnWhetherGroundWaterUtilization.SelectedValue == "ExistingIndustry" || rbtnWhetherGroundWaterUtilization.SelectedValue == "ExpansionProgramExistingIndustry")
        //{
        //    RowNOCObtainedForExistIND.Visible = true;
        //    RowDateOfCommencement.Visible = false;
        //    ddlNOCObtainedForExistIND_SelectedIndexChanged(sender, e);
        //}
        //else
        //{
        //    RowDateOfCommencement.Visible = false;
        //    RowNOCObtainedForExistIND.Visible = false;
        //}
    }
    protected void ddlNOCObtainedForExistIND_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlNOCObtainedForExistIND.SelectedValue == "N")
        //{
        //    RowDateOfCommencement.Visible = true;
        //}
        //else
        //{
        //    RowDateOfCommencement.Visible = false;
        //}
    }
    
}














