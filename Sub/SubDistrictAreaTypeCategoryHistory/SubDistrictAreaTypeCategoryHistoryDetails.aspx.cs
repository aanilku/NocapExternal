using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sub_SubDistrictAreaTypeCategoryHistory_SubDistrictAreaTypeCategoryHistoryDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                if (NOCAPExternalUtility.FillDropDownState(ref ddlState) != 1)
                {
                    lblMessage.Text = "Error In Getting state List";
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
                int int_DistrictCode = Convert.ToInt32(ddlDistrict.SelectedValue);
                if (int_DistrictCode > 0)
                {

                    BindSubDistrictAreaTypeCategoryHistoryDetails(Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), lblSortField.Text);
                }
                else
                {
                    gvSubDistrictAreaTypeCatHist.DataSource = null;
                    gvSubDistrictAreaTypeCatHist.DataBind();

                }



            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }

    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<NOCAP.BLL.Master.SubDistrict> lst_subDistrictList = new List<NOCAP.BLL.Master.SubDistrict>();
        NOCAP.BLL.Master.SubDistrict obj_subDistrictBlank = new NOCAP.BLL.Master.SubDistrict();

        int int_intStateCode;
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
                int_intStateCode = Convert.ToInt32(ddlState.SelectedValue);
                ddlDistrict.Items.Clear();
                if (ddlState.SelectedValue == "")
                {

                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlDistrict);

                }
                else
                {

                    if (NOCAPExternalUtility.FillDropDownDistrict(ref ddlDistrict, int_intStateCode) != 1)
                    {
                        Response.Write("Problem in district population");
                    }
                }

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }

    }
    private void BindSubDistrictAreaTypeCategoryHistoryDetails(int intA_StateCode = 0, int intA_DistrictCode = 0, string str_sortfieldName = "")
    {

        try
        {

            NOCAP.BLL.Master.District obj_district = new NOCAP.BLL.Master.District(intA_StateCode, intA_DistrictCode);
            List<NOCAP.BLL.Master.SubDistrict> lst_subDistrictList = new List<NOCAP.BLL.Master.SubDistrict>();
            NOCAP.BLL.Master.SubDistrict obj_subDistrictBlank = new NOCAP.BLL.Master.SubDistrict();
            NOCAP.BLL.Master.SubDistrict[] arr_subDistrictList;

            switch (str_sortfieldName)
            {

                case "SubDistrictCode":
                    arr_subDistrictList = obj_district.GetSubDistrictList(NOCAP.BLL.Master.District.SortingFieldForSubDistrict.SubDistrictCode);
                    break;
                case "SubDistrictName":

                    arr_subDistrictList = obj_district.GetSubDistrictList(NOCAP.BLL.Master.District.SortingFieldForSubDistrict.SubDistrictName);
                    break;
                default:
                    arr_subDistrictList = obj_district.GetSubDistrictList(NOCAP.BLL.Master.District.SortingFieldForSubDistrict.SubDistrictName);
                    break;


            }

            if (arr_subDistrictList.Count() > 0)
            {

                gvSubDistrictAreaTypeCatHist.DataSource = arr_subDistrictList;
                gvSubDistrictAreaTypeCatHist.DataBind();

            }
            else
            {

                lst_subDistrictList.Add(obj_subDistrictBlank);
                gvSubDistrictAreaTypeCatHist.DataSource = lst_subDistrictList;
                gvSubDistrictAreaTypeCatHist.DataBind();
                int int_NoOfCol = 0;
                int_NoOfCol = gvSubDistrictAreaTypeCatHist.Rows[0].Cells.Count;
                gvSubDistrictAreaTypeCatHist.Rows[0].Cells.Clear();
                gvSubDistrictAreaTypeCatHist.Rows[0].Cells.Add(new TableCell());
                gvSubDistrictAreaTypeCatHist.Rows[0].Cells[0].ColumnSpan = int_NoOfCol;
                gvSubDistrictAreaTypeCatHist.Rows[0].Cells[0].Text = "No Records exsist in Sub District Area Type Category History";

            }
            if (intA_DistrictCode == 0)
            {

                lst_subDistrictList.Add(obj_subDistrictBlank);
                gvSubDistrictAreaTypeCatHist.DataSource = lst_subDistrictList;
                gvSubDistrictAreaTypeCatHist.DataBind();
                int int_NoOfCol = 0;
                int_NoOfCol = gvSubDistrictAreaTypeCatHist.Rows[0].Cells.Count;
                gvSubDistrictAreaTypeCatHist.Rows[0].Cells.Clear();
                gvSubDistrictAreaTypeCatHist.Rows[0].Cells.Add(new TableCell());
                gvSubDistrictAreaTypeCatHist.Rows[0].Cells[0].ColumnSpan = int_NoOfCol;
                gvSubDistrictAreaTypeCatHist.Rows[0].Cells[0].Text = "No Records exsist in Sub District Area Type Category History"; ;

            }

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    protected void gvSubDistrictAreaTypeCatHist_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_subDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory();
        NOCAP.BLL.Master.SubDistrictAreaTypeCategory obj_subDistrictAreaTypeCategory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategory();
        if (e.Row.RowType == DataControlRowType.Header)
        {
        }

        try
        {
            Label lbl_areaTypeCatName = new Label();
            Label lbl_areaTypeDesc = new Label();
            Label lbl_StartDate = new Label();
            Label lbl_NotificationDate = new Label();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                lbl_areaTypeCatName = (Label)e.Row.FindControl("lblAreaTypeCatName");
                lbl_areaTypeDesc = (Label)e.Row.FindControl("lblAreaTypeName");
                lbl_StartDate = (Label)e.Row.FindControl("lblStartDate");
                lbl_NotificationDate = (Label)e.Row.FindControl("lblnotificationDate");


                int int_StateCode = Convert.ToInt32(gvSubDistrictAreaTypeCatHist.DataKeys[e.Row.RowIndex].Values[0]);
                int int_DistrictCode = Convert.ToInt32(gvSubDistrictAreaTypeCatHist.DataKeys[e.Row.RowIndex].Values[1]);
                int int_SubDistrictCode = Convert.ToInt32(gvSubDistrictAreaTypeCatHist.DataKeys[e.Row.RowIndex].Values[2]);


                obj_subDistrictAreaTypeCategory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategory(int_StateCode, int_DistrictCode, int_SubDistrictCode);

                obj_subDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(int_StateCode, int_DistrictCode, int_SubDistrictCode, obj_subDistrictAreaTypeCategory.SubDistrictCurrentAreaCategoryKey);
                if (Convert.ToString(obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode) == "")
                {
                    lbl_areaTypeCatName.Text = "";

                }
                else
                {
                    if (lbl_areaTypeCatName == null)
                    {

                    }
                    else
                    {

                        lbl_areaTypeCatName.Text = HttpUtility.HtmlEncode(obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc());
                        lbl_areaTypeDesc.Text = HttpUtility.HtmlEncode(obj_subDistrictAreaTypeCategoryHistory.AreaTypeDesc());



                        if (obj_subDistrictAreaTypeCategoryHistory.StartDate == null && obj_subDistrictAreaTypeCategoryHistory.DateOfNotification == null)
                        {

                            lbl_StartDate.Text = "";
                            lbl_NotificationDate.Text = "";


                        }
                        else
                        {
                            lbl_StartDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_subDistrictAreaTypeCategoryHistory.StartDate).ToString("dd-MM-yyyy"));
                            lbl_NotificationDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_subDistrictAreaTypeCategoryHistory.DateOfNotification).ToString("dd-MM-yyyy"));
                        }



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
    }

    protected void gvSubDistrictAreaTypeCatHist_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            int int_DistrictCode = Convert.ToInt32(ddlDistrict.SelectedValue);
            NOCAP.BLL.Master.SubDistrict obj_subDistrict = new NOCAP.BLL.Master.SubDistrict();
            lblSortField.Text = HttpUtility.HtmlEncode(e.SortExpression);
            gvSubDistrictAreaTypeCatHist.EditIndex = -1;
            // BindSubDistrictAreaTypeCategoryHistoryDetails(Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), lblSortField.Text);

            lblMessage.Text = "";

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }


}

