using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sub_GWAbstractionCharges_GWAbstractionCalculation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {            
            if (!IsPostBack)
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;

                if (NOCAPExternalUtility.FillDropDownState(ref ddlState) != 1)
                {
                    lblMessage.Text = "Problem in state population";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                                
                ApplicationType();
                Panel1.Visible = false;

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


    private void ApplicationType()
    {
        DataTable apptypedt = new DataTable();
        apptypedt.Columns.Add("AppTypeCode", typeof(int));
        apptypedt.Columns.Add("AppTypeName");
        apptypedt.Rows.Add(1, "Industrial");
        apptypedt.Rows.Add(2, "Infrastructure");
        apptypedt.Rows.Add(3, "Mining");
        apptypedt.Rows.Add(4, "Domestic");
        apptypedt.Rows.Add(5, "BulkWater");
        ddlApplicationType.DataSource = apptypedt;
        ddlApplicationType.DataTextField = "AppTypeName";
        ddlApplicationType.DataValueField = "AppTypeCode";
        ddlApplicationType.DataBind();

        ddlApplicationType.Items.Insert(0, "--Select--");
    }


    private void DomesticType()
    {
        DataTable apptypedt = new DataTable();
        apptypedt.Columns.Add("AppDomCode", typeof(int));
        apptypedt.Columns.Add("AppDomName");
        apptypedt.Rows.Add(1, "Residential ");
        apptypedt.Rows.Add(2, "Group-Housing");
        apptypedt.Rows.Add(3, "Government-Water");
        ddlApplicationPurpose.DataSource = apptypedt;
        ddlApplicationPurpose.DataTextField = "AppDomName";
        ddlApplicationPurpose.DataValueField = "AppDomCode";
        ddlApplicationPurpose.DataBind();

        ddlApplicationPurpose.Items.Insert(0, "--Select--");
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
                int int_applicationTypeCode = Convert.ToInt32(ddlApplicationType.SelectedValue);

                if (int_applicationTypeCode == 4)
                {
                    Panel1.Visible = true;
                    DomesticType();
                }
                else
                {
                    Panel1.Visible = false;
                }



            }
            catch (Exception ex)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
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
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Domestic Type Allow only Numeric');", true);
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

                    int int_stateCode = Convert.ToInt32(ddlState.SelectedValue);
                    int int_districtCode = Convert.ToInt32(ddlDistrict.SelectedValue);
                    int int_subDistrictCode = Convert.ToInt32(ddlSubDistrict.SelectedValue);

                    int int_applicationTypeCode = Convert.ToInt32(ddlApplicationType.SelectedValue);
                    int int_domtypecode = Convert.ToInt32(ddlApplicationPurpose.SelectedValue);


                    NOCAP.BLL.Master.SubDistrict obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(int_stateCode, int_districtCode, int_subDistrictCode);
                    NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_subDistrictAreaTypeCategoryHistory = null;
                    obj_subDistrictAreaTypeCategoryHistory = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory();
                    string areatype = obj_subDistrictAreaTypeCategoryHistory.AreaTypeDesc();
                    string areacat = obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc();

                    decimal perday = Convert.ToDecimal(txtGroundWaterRequirementExist.Text);
                    decimal noofdays = Convert.ToDecimal(txtSurfaceWaterRequirementExist.Text);
                    decimal rate;
                    decimal total;

                    if (int_applicationTypeCode == 1) // industrial
                    {
                        if (areacat == "Safe")
                        {
                            if (perday > 0 && perday <= 50)
                            {
                                rate = 1;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }

                            else if (perday > 50 && perday <= 200)
                            {
                                rate = 3;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }

                            else if (perday > 200 && perday <= 1000)
                            {
                                rate = 5;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 1000 && perday <= 5000)
                            {
                                rate = 8;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 5000)
                            {
                                rate = 10;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }

                        }

                        else if (areacat == "Semi Critical")
                        {
                            if (perday > 0 && perday <= 50)
                            {
                                rate = 2;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }

                            else if (perday > 50 && perday <= 200)
                            {
                                rate = 5;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }

                            else if (perday > 200 && perday <= 1000)
                            {
                                rate = 10;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 1000 && perday <= 5000)
                            {
                                rate = 15;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 5000)
                            {
                                rate = 20;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }

                        }

                        else if (areacat == "Critical")
                        {
                            if (perday > 0 && perday <= 50)
                            {
                                rate = 4;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }

                            else if (perday > 50 && perday <= 200)
                            {
                                rate = 10;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 200 && perday <= 1000)
                            {
                                rate = 20;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 1000 && perday <= 5000)
                            {
                                rate = 40;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 5000)
                            {
                                rate = 60;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                        }

                        else if (areacat == "Over Exploited")
                        {
                            if (perday > 0 && perday <= 50)
                            {
                                rate = 8;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }

                            else if (perday > 50 && perday <= 200)
                            {
                                rate = 20;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 200 && perday <= 1000)
                            {
                                rate = 40;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 1000 && perday <= 5000)
                            {
                                rate = 80;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 5000)
                            {
                                rate = 120;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                        }
                    }

                    else if (int_applicationTypeCode == 2) //infrastructure
                    {
                        if (areacat == "Safe")
                        {

                            if (perday > 0 && perday <= 200)
                            {
                                rate = 1;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 200 && perday <= 1000)
                            {
                                rate = 2;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 1000 && perday <= 5000)
                            {
                                rate = 3;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 5000)
                            {
                                rate = 5;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                        }
                        else if (areacat == "Semi Critical")
                        {

                            if (perday > 0 && perday <= 200)
                            {
                                rate = 2;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 200 && perday <= 1000)
                            {
                                rate = 3;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 1000 && perday <= 5000)
                            {
                                rate = 5;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 5000)
                            {
                                rate = 8;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                        }
                        else if (areacat == "Critical")
                        {

                            if (perday > 0 && perday <= 200)
                            {
                                rate = 4;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 200 && perday <= 1000)
                            {
                                rate = 6;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 1000 && perday <= 5000)
                            {
                                rate = 8;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 5000)
                            {
                                rate = 10;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                        }
                        else if (areacat == "Over Exploited")
                        {

                            if (perday > 0 && perday <= 200)
                            {
                                rate = 6;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 200 && perday <= 1000)
                            {
                                rate = 10;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 1000 && perday <= 5000)
                            {
                                rate = 16;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 5000)
                            {
                                rate = 20;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                        }
                    }

                    else if (int_applicationTypeCode == 3) // mining
                    {
                        if (areacat == "Safe")
                        {

                            if (perday > 0 && perday <= 200)
                            {
                                rate = 1;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 200 && perday <= 1000)
                            {
                                rate = 2;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 1000 && perday <= 5000)
                            {
                                rate = Convert.ToDecimal(2.50);
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 5000)
                            {
                                rate = 3;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                        }
                        else if (areacat == "Semi Critical")
                        {

                            if (perday > 0 && perday <= 200)
                            {
                                rate = 2;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 200 && perday <= 1000)
                            {
                                rate = Convert.ToDecimal(2.5);
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 1000 && perday <= 5000)
                            {
                                rate = 3;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 5000)
                            {
                                rate = 4;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                        }
                        else if (areacat == "Critical")
                        {

                            if (perday > 0 && perday <= 200)
                            {
                                rate = 3;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 200 && perday <= 1000)
                            {
                                rate = 4;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 1000 && perday <= 5000)
                            {
                                rate = 5;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 5000)
                            {
                                rate = 6;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                        }
                        else if (areacat == "Over Exploited")
                        {

                            if (perday > 0 && perday <= 200)
                            {
                                rate = 4;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 200 && perday <= 1000)
                            {
                                rate = 5;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 1000 && perday <= 5000)
                            {
                                rate = 6;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                            else if (perday > 5000)
                            {
                                rate = 7;
                                total = (perday * noofdays * rate);
                                lblMessage.Text = "Total Amount to be paid = Rs." + total;
                            }
                        }
                    }

                    else if (int_applicationTypeCode == 4) // domestic
                    {
                        if (int_domtypecode == 3)
                        {
                            rate = Convert.ToDecimal(0.5);
                            total = (perday * noofdays * rate);
                            lblMessage.Text = "Total Amount to be paid = Rs." + total;
                        }
                        else
                        {
                            if (areacat == "Safe")
                            {
                                if (perday > 0 && perday <= 50)
                                {
                                    rate = 1;
                                    total = (perday * noofdays * rate);
                                    lblMessage.Text = "Total Amount to be paid = Rs." + total;
                                }

                                else if (perday > 50 && perday <= 200)
                                {
                                    rate = 3;
                                    total = (perday * noofdays * rate);
                                    lblMessage.Text = "Total Amount to be paid = Rs." + total;
                                }
                                else if (perday > 200 && perday <= 1000)
                                {
                                    rate = 5;
                                    total = (perday * noofdays * rate);
                                    lblMessage.Text = "Total Amount to be paid = Rs." + total;
                                }
                                else if (perday > 1000 && perday <= 5000)
                                {
                                    rate = 8;
                                    total = (perday * noofdays * rate);
                                    lblMessage.Text = "Total Amount to be paid = Rs." + total;
                                }
                                else if (perday > 5000)
                                {
                                    rate = 10;
                                    total = (perday * noofdays * rate);
                                    lblMessage.Text = "Total Amount to be paid = Rs." + total;
                                }
                            }
                            else if (areacat == "Semi Critical")
                            {
                                if (perday > 0 && perday <= 50)
                                {
                                    rate = 2;
                                    total = (perday * noofdays * rate);
                                    lblMessage.Text = "Total Amount to be paid = Rs." + total;
                                }

                                else if (perday > 50 && perday <= 200)
                                {
                                    rate = 5;
                                    total = (perday * noofdays * rate);
                                    lblMessage.Text = "Total Amount to be paid = Rs." + total;
                                }
                                else if (perday > 200 && perday <= 1000)
                                {
                                    rate = 10;
                                    total = (perday * noofdays * rate);
                                    lblMessage.Text = "Total Amount to be paid = Rs." + total;
                                }
                                else if (perday > 1000 && perday <= 5000)
                                {
                                    rate = 15;
                                    total = (perday * noofdays * rate);
                                    lblMessage.Text = "Total Amount to be paid = Rs." + total;
                                }
                                else if (perday > 5000)
                                {
                                    rate = 20;
                                    total = (perday * noofdays * rate);
                                    lblMessage.Text = "Total Amount to be paid = Rs." + total;
                                }
                            }
                            else if (areacat == "Critical")
                            {
                                if (perday > 0 && perday <= 50)
                                {
                                    rate = 4;
                                    total = (perday * noofdays * rate);
                                    lblMessage.Text = "Total Amount to be paid = Rs." + total;
                                }

                                else if (perday > 50 && perday <= 200)
                                {
                                    rate = 10;
                                    total = (perday * noofdays * rate);
                                    lblMessage.Text = "Total Amount to be paid = Rs." + total;
                                }
                                else if (perday > 200 && perday <= 1000)
                                {
                                    rate = 20;
                                    total = (perday * noofdays * rate);
                                    lblMessage.Text = "Total Amount to be paid = Rs." + total;
                                }
                                else if (perday > 1000 && perday <= 5000)
                                {
                                    rate = 40;
                                    total = (perday * noofdays * rate);
                                    lblMessage.Text = "Total Amount to be paid = Rs." + total;
                                }
                                else if (perday > 5000)
                                {
                                    rate = 60;
                                    total = (perday * noofdays * rate);
                                    lblMessage.Text = "Total Amount to be paid = Rs." + total;
                                }
                            }
                            else if (areacat == "Over Exploited")
                            {
                                if (perday > 0 && perday <= 50)
                                {
                                    rate = 8;
                                    total = (perday * noofdays * rate);
                                    lblMessage.Text = "Total Amount to be paid = Rs." + total;
                                }

                                else if (perday > 50 && perday <= 200)
                                {
                                    rate = 20;
                                    total = (perday * noofdays * rate);
                                    lblMessage.Text = "Total Amount to be paid = Rs." + total;
                                }
                                else if (perday > 200 && perday <= 1000)
                                {
                                    rate = 40;
                                    total = (perday * noofdays * rate);
                                    lblMessage.Text = "Total Amount to be paid = Rs." + total;
                                }
                                else if (perday > 1000 && perday <= 5000)
                                {
                                    rate = 80;
                                    total = (perday * noofdays * rate);
                                    lblMessage.Text = "Total Amount to be paid = Rs." + total;
                                }
                                else if (perday > 5000)
                                {
                                    rate = 120;
                                    total = (perday * noofdays * rate);
                                    lblMessage.Text = "Total Amount to be paid = Rs." + total;
                                }
                            }
                        }
                        
                    }


                    else if (int_applicationTypeCode == 5) // bulk water
                    {
                        if (areacat == "Safe")
                        {

                            rate = 10;
                            total = (perday * noofdays * rate);
                            lblMessage.Text = "Total Amount to be paid = Rs." + total;

                        }
                        else if (areacat == "Semi Critical")
                        {

                            rate = 20;
                            total = (perday * noofdays * rate);
                            lblMessage.Text = "Total Amount to be paid = Rs." + total;

                        }
                        else if (areacat == "Critical")
                        {

                            rate = 25;
                            total = (perday * noofdays * rate);
                            lblMessage.Text = "Total Amount to be paid = Rs." + total;

                        }
                        else if (areacat == "Over Exploited")
                        {

                            rate = 35;
                            total = (perday * noofdays * rate);
                            lblMessage.Text = "Total Amount to be paid = Rs." + total;

                        }
                    }


                    else
                    {
                        lblMessage.Text = "Please enter valid days";
                    }





                    if (int_domtypecode == 3)
                    {
                        lblAreaTypeCategory.Text = "";
                        lblCategorizationofAssessmentUnits.Text = "";

                    }
                    else
                    {
                        lblAreaTypeCategory.Text = HttpUtility.HtmlEncode(obj_subDistrictAreaTypeCategoryHistory.AreaTypeDesc());
                        lblCategorizationofAssessmentUnits.Text = HttpUtility.HtmlEncode(obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc());
                    }

                    


                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
                }
            }
        }
        

        
    }
}