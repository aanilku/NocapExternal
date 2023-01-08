using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sub_Report_GW_Charges_Calculation_GWChargesCalculation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
                RowAppTypeCate.Visible = false;
                if ( NOCAPExternalUtility.FillDropDownState(ref ddlState) != 1)
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


    protected void ddlApplicationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        RowAppTypeCate.Visible = false;
        ClearForm();

        if (ddlApplicationType.SelectedValue == "1")
        {
            if (NOCAPExternalUtility.FillDropDownApplicationTypeCategory(ref ddlApplicationTypeCat, NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Domestic")) != 1)
            {
                lblMessage.Text = "Problem in Application Type Category population";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            RowAppTypeCate.Visible = true;
            
            lblCum.Text = "(cum/month):";

        }
        if (ddlApplicationType.SelectedValue == "2")
        {
            if (NOCAPExternalUtility.FillDropDownApplicationTypeCategory(ref ddlApplicationTypeCat, NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Industrial")) != 1)
            {
                lblMessage.Text = "Problem in Application Type Category population";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            RowAppTypeCate.Visible = true;
            lblCum.Text = "(cum/day):";

        }
        if (ddlApplicationType.SelectedValue == "3")
        {
            if (NOCAPExternalUtility.FillDropDownApplicationTypeCategory(ref ddlApplicationTypeCat, NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Infrastructure")) != 1)
            {
                lblMessage.Text = "Problem in Application Type Category population";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            RowAppTypeCate.Visible = true;
            lblCum.Text = "(cum/day):";
        }
        if (ddlApplicationType.SelectedValue == "4")
        {
            if (NOCAPExternalUtility.FillDropDownApplicationTypeCategory(ref ddlApplicationTypeCat, NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType("Mining")) != 1)
            {
                lblMessage.Text = "Problem in Application Type Category population";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            RowAppTypeCate.Visible = true;
            lblCum.Text = "(cum/day):";
        }
        if (ddlApplicationType.SelectedValue == "5")
        {
            lblCum.Text = "(cum/day):";
        }
    }

    protected void ddlApplicationTypeCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlApplicationTypeCat.SelectedValue.ToString() == "73")
        { }
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

                NOCAP.BLL.Master.SubDistrict obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(Convert.ToInt32(ddlState.SelectedValue.ToString()), Convert.ToInt32(ddlDistrict.SelectedValue.ToString()), Convert.ToInt32(ddlSubDistrict.SelectedValue.ToString()));
                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_subDistrictAreaTypeCategoryHistory = null;
                obj_subDistrictAreaTypeCategoryHistory = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory();
                lblAreaTypeCatCode.Text = obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode.ToString();

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
                //decimal dec_Year = 365m;
                decimal result = 0m;
                
                NOCAP.BLL.Master.DrinkDomCharge obj_DrinkDomCharge = null;
                NOCAP.BLL.Master.IndustrialCharge obj_IndustrialCharge = null;
                NOCAP.BLL.Master.InfrastructureCharge obj_InfrastructureCharge = null;
                NOCAP.BLL.Master.MiningCharge obj_MiningCharge = null;
                NOCAP.BLL.Master.BulkWaterCharge obj_BulkWaterCharge = null;
                NOCAP.BLL.Master.PackDrinkCharge obj_PackDrinkCharge = null;

                if (ddlApplicationType.SelectedValue == "1")
                {
                    obj_DrinkDomCharge = new NOCAP.BLL.Master.DrinkDomCharge(1, Convert.ToInt32(ddlApplicationTypeCat.SelectedValue.ToString()), Convert.ToDecimal(txtQty.Text));
                    result = Convert.ToDecimal(txtQty.Text) * obj_DrinkDomCharge.Rate * Convert.ToInt32(txtDays.Text);
                }

                if (ddlApplicationType.SelectedValue == "5")
                {
                    obj_BulkWaterCharge = new NOCAP.BLL.Master.BulkWaterCharge(1, Convert.ToInt32(lblAreaTypeCatCode.Text), Convert.ToDecimal(txtQty.Text));

                    result = Convert.ToDecimal(txtQty.Text) * obj_BulkWaterCharge.Rate * Convert.ToInt32(txtDays.Text);
                }


                //if (ddlApplicationTypeCat.SelectedValue.ToString() == "73")
                //   {
                    
                //   }
                if (ddlApplicationType.SelectedValue == "2")
                {
                    if (ddlApplicationTypeCat.SelectedValue.ToString() == "73")
                    {
                        obj_PackDrinkCharge = new NOCAP.BLL.Master.PackDrinkCharge(1, Convert.ToInt32(lblAreaTypeCatCode.Text), Convert.ToDecimal(txtQty.Text));
                        result = Convert.ToDecimal(txtQty.Text) * obj_PackDrinkCharge.Rate * Convert.ToInt32(txtDays.Text);
                    }
                    else
                    {
                        obj_IndustrialCharge = new NOCAP.BLL.Master.IndustrialCharge(1, Convert.ToInt32(lblAreaTypeCatCode.Text), Convert.ToDecimal(txtQty.Text));
                        result = Convert.ToDecimal(txtQty.Text) * obj_IndustrialCharge.Rate * Convert.ToInt32(txtDays.Text);
                    }


                }
                if (ddlApplicationType.SelectedValue == "3")
                {

                    obj_InfrastructureCharge = new NOCAP.BLL.Master.InfrastructureCharge(1, Convert.ToInt32(lblAreaTypeCatCode.Text), Convert.ToDecimal(txtQty.Text));
                    
                    result = Convert.ToDecimal(txtQty.Text) * obj_InfrastructureCharge.Rate * Convert.ToInt32(txtDays.Text);
                }
                if (ddlApplicationType.SelectedValue == "4")
                {
                    obj_MiningCharge = new NOCAP.BLL.Master.MiningCharge(1, Convert.ToInt32(lblAreaTypeCatCode.Text), Convert.ToDecimal(txtQty.Text));
                    
                    result = Convert.ToDecimal(txtQty.Text) * obj_MiningCharge.Rate * Convert.ToInt32(txtDays.Text);
                }
                
                if (ddlApplicationTypeCat.SelectedValue == "12")
                {
                    lblMessage.Text = "Data not found";
                }
                else
                {
                    lblResult.Text = "(" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(result.ToString())) + ")";
                    if (Convert.ToInt32(lblAreaTypeCatCode.Text) == 5)
                    {

                        lblMessage.Text = "Your Ground Water Restoration charges is (Rs.): " + String.Format("{0:0.00}", result);
                    }
                    else
                    {
                        lblMessage.Text = "Your Ground Water Abstraction charges is (Rs.): " + String.Format("{0:0.00}", result);
                    }                    

                }                 

            }
            catch (Exception ex)
            { Response.Redirect("~/ExternalErrorPage.aspx", false); }
        }
    }

    public void ClearForm()
    {
        try
        {
            txtDays.Text = "";
            txtQty.Text = "";
            lblCum.Text = "";
            lblMessage.Text = "";
            ddlState.SelectedValue = "";
            ddlDistrict.SelectedValue = "";
            ddlSubDistrict.SelectedValue = "";
        }
        catch (Exception)
        {
            Response.Redirect("~/InternalErrorPage.aspx", false);
        }
    }

}