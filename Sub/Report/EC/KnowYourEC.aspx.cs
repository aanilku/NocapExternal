using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sub_Report_EC_KnowYourEC : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                // RvResult.Visible = false;
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
                RowAppTypeCate.Visible = false;
                if (NOCAPExternalUtility.FillDropDownState(ref ddlState) != 1)
                {
                    lblMessage.Text = "Problem in state population";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }

                NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlDistrict);
                NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlSubDistrict);

            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        int int_StateCode;
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;

            try
            {
                // RvResult.Visible = false;
                ddlDistrict.Items.Clear();
                ddlSubDistrict.Items.Clear();

                if (ddlState.SelectedValue == "")
                {
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlDistrict);
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlSubDistrict);
                }
                else
                {
                    int_StateCode = Convert.ToInt32(ddlState.SelectedValue);

                    if (NOCAPExternalUtility.FillDropDownDistrict(ref ddlDistrict, int_StateCode) != 1)
                    {
                        lblMessage.Text = "Problem in district population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlSubDistrict);
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
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        int int_StateCode;
        int int_DistricCode;

        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;

            try
            {
                // RvResult.Visible = false;
                ddlSubDistrict.Items.Clear();

                if (ddlDistrict.SelectedValue == "")
                {
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlSubDistrict);
                }
                else
                {
                    int_DistricCode = Convert.ToInt32(ddlDistrict.SelectedValue);
                    int_StateCode = Convert.ToInt32(ddlState.SelectedValue);

                    if (NOCAPExternalUtility.FillDropDownSubDistrict(ref ddlSubDistrict, int_DistricCode, int_StateCode) != 1)
                    {

                        lblMessage.Text = "Problem in Sub-district population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
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
    protected void ddlSubDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;

            try
            {




                // RvResult.Visible = false;
                NOCAP.BLL.Master.SubDistrict obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(Convert.ToInt32(ddlState.SelectedValue.ToString()), Convert.ToInt32(ddlDistrict.SelectedValue.ToString()), Convert.ToInt32(ddlSubDistrict.SelectedValue.ToString()));
                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_subDistrictAreaTypeCategoryHistory = null;
                obj_subDistrictAreaTypeCategoryHistory = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory();
                lblAreaTypeCatCode.Text = obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode.ToString();

                //if (obj_subDistrictAreaTypeCategoryHistory == null || obj_subDistrictAreaTypeCategoryHistory.AreaTypeCode == 0 || obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode == 0)
                //{
                //    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlAreaTypeCategory);
                //}
                //else
                //{
                //    if (NOCAPExternalUtility.FillDropDownAreaTypeCategoryBasedOnAreaType(ref ddlAreaTypeCategory, obj_subDistrictAreaTypeCategoryHistory.AreaTypeCode) != 1)
                //    {
                //        lblMessage.Text = "Problem in Area Type Category population";
                //        lblMessage.ForeColor = System.Drawing.Color.Red;
                //    }

                //}
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
   

    protected void btnShowRecord_Click(object sender, EventArgs e)
    {
      
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {
               // decimal dec_Year = 365m;
                decimal result = 0m;
                NOCAP.BLL.Master.DrinkDomEC obj_DrinkDomEC = null;
                NOCAP.BLL.Master.PackDrinkEC obj_PackDrinkEC = null;
                NOCAP.BLL.Master.IndustrialEC obj_IndustrialEC = null;
                NOCAP.BLL.Master.InfrastructureEC obj_InfrastructureEC = null;
                NOCAP.BLL.Master.MiningEC obj_MiningEC = null;
                NOCAP.BLL.Master.MinimumEC obj_MinimumEC = new NOCAP.BLL.Master.MinimumEC(Convert.ToInt32(ddlApplicationType.SelectedValue.ToString()));
                //NOCAP.BLL.Master.DeterrentFactor obj_DeterrentFactor = new NOCAP.BLL.Master.DeterrentFactor(1, Convert.ToDecimal(txtQty.Text), Convert.ToDecimal(Convert.ToDecimal(txtDays.Text) / dec_Year));

                DateTime dtf = Convert.ToDateTime(txtGWWillegalFrom.Text);
                int int_illegalDays = Convert.ToInt32(Convert.ToDateTime(txtGWWillegalTo.Text).Subtract(dtf).Days);
                NOCAP.BLL.Master.DeterrentFactor obj_DeterrentFactor = new NOCAP.BLL.Master.DeterrentFactor(1, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(txtGWWillegalFrom.Text), Convert.ToDateTime(txtGWWillegalTo.Text));

                if (ddlApplicationType.SelectedValue == "1")
                {
                    obj_DrinkDomEC = new NOCAP.BLL.Master.DrinkDomEC(1, Convert.ToInt32(lblAreaTypeCatCode.Text), Convert.ToDecimal(txtQty.Text));
                    if (obj_DrinkDomEC.CreatedByUserCode == 0)
                    {
                        lblMessage.Text = "Entry not found";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    result = Convert.ToDecimal(txtQty.Text) * obj_DrinkDomEC.Rate * Convert.ToInt32(int_illegalDays) * obj_DeterrentFactor.DeterrentFactorRate;

                }


                if (ddlApplicationType.SelectedValue == "2")
                {
                    if (ddlApplicationTypeCat.SelectedValue.ToString() == "73")
                    {
                        obj_PackDrinkEC = new NOCAP.BLL.Master.PackDrinkEC(1, Convert.ToInt32(lblAreaTypeCatCode.Text), Convert.ToDecimal(txtQty.Text));
                        if (obj_PackDrinkEC.CreatedByUserCode == 0)
                        {
                            lblMessage.Text = "Entry not found";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        result = Convert.ToDecimal(txtQty.Text) * obj_PackDrinkEC.Rate * Convert.ToInt32(int_illegalDays) * obj_DeterrentFactor.DeterrentFactorRate;

                    }
                    else
                    {
                        obj_IndustrialEC = new NOCAP.BLL.Master.IndustrialEC(1, Convert.ToInt32(lblAreaTypeCatCode.Text), Convert.ToDecimal(txtQty.Text));
                        if (obj_IndustrialEC.CreatedByUserCode == 0)
                        {
                            lblMessage.Text = "Entry not found";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        result = Convert.ToDecimal(txtQty.Text) * obj_IndustrialEC.Rate * Convert.ToInt32(int_illegalDays) * obj_DeterrentFactor.DeterrentFactorRate;
                    }
                }
                if (ddlApplicationType.SelectedValue == "3")
                {
                    obj_InfrastructureEC = new NOCAP.BLL.Master.InfrastructureEC(1, Convert.ToInt32(lblAreaTypeCatCode.Text), Convert.ToDecimal(txtQty.Text));
                    if (obj_InfrastructureEC.CreatedByUserCode == 0)
                    {
                        lblMessage.Text = "Entry not found";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    result = Convert.ToDecimal(txtQty.Text) * obj_InfrastructureEC.Rate * Convert.ToInt32(int_illegalDays) * obj_DeterrentFactor.DeterrentFactorRate;
                }
                if (ddlApplicationType.SelectedValue == "4")
                {
                    obj_MiningEC = new NOCAP.BLL.Master.MiningEC(1, Convert.ToInt32(lblAreaTypeCatCode.Text), Convert.ToDecimal(txtQty.Text));
                    if (obj_MiningEC.CreatedByUserCode == 0)
                    {
                        lblMessage.Text = "Entry not found";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    result = Convert.ToDecimal(txtQty.Text) * obj_MiningEC.Rate * Convert.ToInt32(int_illegalDays) * obj_DeterrentFactor.DeterrentFactorRate;
                }
                //else { lblMessage.Text = "Please select Application Type"; }
                if (result > 0 && result < obj_MinimumEC.MinimumECAmount)
                {
                    //lblMessage.Text = "Your Environmental Compensation (EC) is (Rs.): " + NOCAPExternalUtility.DecimalFormat(Convert.ToDouble(obj_MinimumEC.MinimumECAmount)) + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(result))) + ")";
                    lblMessage.Text = "Your Environmental Compensation (EC) is (Rs.): " + String.Format("{0:0.00}", obj_MinimumEC.MinimumECAmount) + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(obj_MinimumEC.MinimumECAmount))) + ")";

                }
                else
                {
                    lblMessage.Text = "Your Environmental Compensation (EC) is (Rs.): " + String.Format("{0:0.00}", result) + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(result))) + ")";

                }
            }
            catch (Exception ex)
            { Response.Redirect("~/ExternalErrorPage.aspx", false); }
        }
    }


    protected void ddlApplicationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        RowAppTypeCate.Visible = false;
        if (ddlApplicationType.SelectedValue == "2")
        {
            if (NOCAPExternalUtility.FillDropDownApplicationTypeCategory(ref ddlApplicationTypeCat, NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Industrial")) != 1)
            {
                lblMessage.Text = "Problem in Application Type Category population";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            RowAppTypeCate.Visible = true;
        }
        if (ddlApplicationType.SelectedValue == "3")
        {
            if (NOCAPExternalUtility.FillDropDownApplicationTypeCategory(ref ddlApplicationTypeCat, NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Infrastructure")) != 1)
            {
                lblMessage.Text = "Problem in Application Type Category population";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            RowAppTypeCate.Visible = true;
        }
        if (ddlApplicationType.SelectedValue == "4")
        {
            if (NOCAPExternalUtility.FillDropDownApplicationTypeCategory(ref ddlApplicationTypeCat, NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Mining")) != 1)
            {
                lblMessage.Text = "Problem in Application Type Category population";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            RowAppTypeCate.Visible = true;
        }
    }

    protected void ddlApplicationTypeCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlApplicationTypeCat.SelectedValue.ToString() == "73")
        { }
    }

  
}

