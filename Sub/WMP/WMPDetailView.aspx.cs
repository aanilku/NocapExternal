using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Sub_WMP_WMPDetailView : System.Web.UI.Page
{
    //string strPageName = "WMPAttachment";
    //string strActionName = "";
    string strStatus = "";
 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (NOCAPExternalUtility.FillDropDownState(ref ddlState) != 1)
                {
                    lblMessage.Text = "Problem in state population";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                Session["CSRF"] = hidCSRF.Value;
                ViewState["CSRF"] = hidCSRF.Value;
                ddlState_SelectedIndexChanged(sender, e);

            }
            NOCAP.BLL.Master.WMPAttachment obj_WMPAttachment = new NOCAP.BLL.Master.WMPAttachment();
            obj_WMPAttachment.GetAll();
            NOCAP.BLL.Master.WMPAttachment[] arr = obj_WMPAttachment.WMPAttachmentCollection;
            if (ddlState.Items.Count > 1)
            {
                foreach (ListItem item in ddlState.Items)
                {
                    if (item.Value.ToString() != "")
                    {
                        var result = Array.Find(arr, element => element.StateCode == Convert.ToInt32(item.Value.ToString()));
                        if (result == null)
                            item.Attributes.Add("style", "color:black;");
                        else
                            item.Attributes.Add("style", "color:blue;");
                    }
                }
             
            }

            if (ddlDistrict.Items.Count > 1)
            {
                foreach (ListItem item in ddlDistrict.Items)
                {
                    if (item.Value.ToString() != "")
                    {
                        var result = Array.Find(arr, element => element.DistrictCode == Convert.ToInt32(item.Value.ToString()));
                        if (result == null)
                            item.Attributes.Add("style", "color:black;");
                        else
                            item.Attributes.Add("style", "color:blue;");
                    }
                }
            }
            if (ddlSubdistrict.Items.Count > 1)
            {
                foreach (ListItem item in ddlSubdistrict.Items)
                {
                    if (item.Value.ToString() != "")
                    {
                        var result = Array.Find(arr, element => element.SubDistrictCode == Convert.ToInt32(item.Value.ToString()));
                        if (result == null)
                            item.Attributes.Add("style", "color:black;");
                        else
                            item.Attributes.Add("style", "color:blue;");
                    }
                }
            }
            


        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intStateCode;
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

                lblMessage.Text = "";
                ddlDistrict.Items.Clear();
                ddlSubdistrict.Items.Clear();
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
                ddlDistrict_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
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

                lblMessage.Text = "";
                ddlSubdistrict.Items.Clear();

                if (ddlDistrict.SelectedValue == "")
                {
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlSubdistrict);
                }
                else
                {
                    int_DistricCode = Convert.ToInt32(ddlDistrict.SelectedValue);
                    int_StateCode = Convert.ToInt32(ddlState.SelectedValue);
                    if (NOCAPExternalUtility.FillDropDownSubDistrict(ref ddlSubdistrict, int_DistricCode, int_StateCode) != 1)
                    {
                        lblMessage.Text = "Problem in Subdistrict population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                NOCAP.BLL.Master.WMPAttachment obj_WMPAttachment = new NOCAP.BLL.Master.WMPAttachment();
                obj_WMPAttachment.GetAll();
                NOCAP.BLL.Master.WMPAttachment[] arr = obj_WMPAttachment.WMPAttachmentCollection;
                if (ddlDistrict.Items.Count > 1)
                {
                    foreach (ListItem item in ddlDistrict.Items)
                    {
                        if (item.Value.ToString() != "")
                        {
                            var result = Array.Find(arr, element => element.DistrictCode == Convert.ToInt32(item.Value.ToString()));
                            if (result == null)
                                item.Attributes.Add("style", "color:black;");
                            else
                                item.Attributes.Add("style", "color:blue;");
                        }
                    }
                }
                ddlSubdistrict_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void ddlSubdistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            NOCAP.BLL.Master.WMPAttachment obj_WMPAttachment = new NOCAP.BLL.Master.WMPAttachment();
            obj_WMPAttachment.GetAll();
            NOCAP.BLL.Master.WMPAttachment[] arr = obj_WMPAttachment.WMPAttachmentCollection;

            if (ddlSubdistrict.Items.Count > 1)
            {
                foreach (ListItem item in ddlSubdistrict.Items)
                {
                    if (item.Value.ToString() != "")
                    {
                        var result = Array.Find(arr, element => element.SubDistrictCode == Convert.ToInt32(item.Value.ToString()));
                        if (result == null)
                            item.Attributes.Add("style", "color:black;");
                        else
                            item.Attributes.Add("style", "color:blue;");
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void BindGridView()
    {
        try
        {
            NOCAP.BLL.Master.WMPAttachment obj_WMPAttachment = new NOCAP.BLL.Master.WMPAttachment();
            if (ddlState.SelectedIndex > 0)
                obj_WMPAttachment.StateCode = Convert.ToInt32(ddlState.SelectedValue.ToString());
            if (ddlDistrict.SelectedIndex > 0)
                obj_WMPAttachment.DistrictCode = Convert.ToInt32(ddlDistrict.SelectedValue.ToString());
            if (ddlSubdistrict.SelectedIndex > 0)
                obj_WMPAttachment.SubDistrictCode = Convert.ToInt32(ddlSubdistrict.SelectedValue.ToString());
            obj_WMPAttachment.GetAllForKeys();
            NOCAP.BLL.Master.WMPAttachment[] arr_WMPAttachmentList = obj_WMPAttachment.WMPAttachmentCollection;
            gvWMP.DataSource = arr_WMPAttachmentList;
            gvWMP.DataBind();
            lblCount.Text = arr_WMPAttachmentList.Length.ToString();
        }
        catch (Exception ex)
        {

        }

    }
    //private void FillRepeater()
    //{
    //    try
    //    {
    //        NOCAP.BLL.Master.WMPAttachment obj_WMPAttachment = new NOCAP.BLL.Master.WMPAttachment();
    //        if (ddlState.SelectedIndex > 0)
    //            obj_WMPAttachment.StateCode = Convert.ToInt32(ddlState.SelectedValue.ToString());
    //        if (ddlDistrict.SelectedIndex > 0)
    //            obj_WMPAttachment.DistrictCode = Convert.ToInt32(ddlDistrict.SelectedValue.ToString());
    //        if (ddlSubdistrict.SelectedIndex > 0)
    //            obj_WMPAttachment.SubDistrictCode = Convert.ToInt32(ddlSubdistrict.SelectedValue.ToString());
    //        obj_WMPAttachment.GetAllForKeys();
    //        NOCAP.BLL.Master.WMPAttachment[] arr_WMPAttachmentList = obj_WMPAttachment.WMPAttachmentCollection;
    //        rptUsers.DataSource = arr_WMPAttachmentList;
    //        rptUsers.DataBind();
    //        lblCount.Text = arr_WMPAttachmentList.Length.ToString();
    //    }
    //    catch (Exception ex)
    //    {

    //    }
        
    //}
    protected void DownloadOrViewFile(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {

                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    int int_StateCode = Convert.ToInt32(CommandArgument[0]);
                    int int_DistrictCode = Convert.ToInt32(CommandArgument[1]);
                    int int_SubDistrictCode = Convert.ToInt32(CommandArgument[2]);
                    int int_SN = Convert.ToInt32(CommandArgument[3]);

                    if (btn.Text.ToString() == "View")
                        NOCAPExternalUtility.WMPViewFiles(int_StateCode, int_DistrictCode, int_SubDistrictCode, int_SN);
                    else
                        NOCAPExternalUtility.WMPDownloadFiles(int_StateCode, int_DistrictCode, int_SubDistrictCode, int_SN);

                    // NOCAPExternalUtility.WMPDownloadFiles(int_StateCode, int_DistrictCode, int_SubDistrictCode, int_SN);
                    NOCAPExternalUtility.WMPViewFiles(int_StateCode, int_DistrictCode, int_SubDistrictCode, int_SN);
                }

            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
            }
        }
    }

    protected void gvWMP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField lblStateCode = (HiddenField)e.Row.FindControl("lblStateCode");
                HiddenField lblDistrictCode = (HiddenField)e.Row.FindControl("lblDistrictCode");
                HiddenField lblSubDistrictCode = (HiddenField)e.Row.FindControl("lblSubDistrictCode");
                Label lblStateName = (Label)e.Row.FindControl("lblStateName");
                Label lblDistrictName = (Label)e.Row.FindControl("lblDistrictName");
                Label lblSubDistrictName = (Label)e.Row.FindControl("lblSubDistrictName");
                lblStateName.Text = new NOCAP.BLL.Master.State(Convert.ToInt32(lblStateCode.Value)).StateName;
                lblDistrictName.Text = new NOCAP.BLL.Master.District(Convert.ToInt32(lblStateCode.Value), Convert.ToInt32(lblDistrictCode.Value)).DistrictName;
                lblSubDistrictName.Text = new NOCAP.BLL.Master.SubDistrict(Convert.ToInt32(lblStateCode.Value), Convert.ToInt32(lblDistrictCode.Value), Convert.ToInt32(lblSubDistrictCode.Value)).SubDistrictName;
            }
        }
        catch (Exception ex)
        { }
    }

    
    //private int DeleteAttchment(GridView gv, GridViewDeleteEventArgs e, Label lblMessage, string str_ActionName)
    //{
    //    try
    //    {
    //        int int_StateCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["StateCode"]);
    //        int int_DistrictCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["DistrictCode"]);
    //        int int_SubDistrictCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["SubDistrictCode"]);
    //        int int_AttachmentCodeSerialNumber = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["AttCodeSN"]);
    //        lblMessage.ForeColor = System.Drawing.Color.Red;
    //        NOCAP.BLL.Master.WMPAttachment obj_WMPAttachment = new NOCAP.BLL.Master.WMPAttachment();
    //        obj_WMPAttachment.StateCode = int_StateCode;
    //        obj_WMPAttachment.DistrictCode = int_DistrictCode;
    //        obj_WMPAttachment.SubDistrictCode = int_SubDistrictCode;
    //        obj_WMPAttachment.AttCodeSN = int_AttachmentCodeSerialNumber;

    //        if (obj_WMPAttachment.Delete() == 1)
    //        {
    //            lblMessage.Text = obj_WMPAttachment.CustumMessage;
    //            strStatus = "File Delete Success";
    //            return 1;
    //        }
    //        else
    //        {
    //            lblMessage.Text = obj_WMPAttachment.CustumMessage;
    //            strStatus = "File Delete Failed";
    //            return 0;
    //        }
    //    }
    //    catch (Exception ex)
    //    { return 0; }
    //    finally
    //    {

    //    }
    //}
    protected void imgBtnCaptchaRefresh_Click(object sender, ImageClickEventArgs e)
    {
        txtCaptchaCode.Text = "";
        lblMessage.Text = "";
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            BindGridView();
            //FillRepeater();
            txtCaptchaCode.Text = "";
        }
        catch (Exception ex)
        { Response.Redirect("~/ExternalErrorPage.aspx", false); }
    }
    //protected void rptUsers_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (rptUsers.Items.Count < 1)
    //    {
    //        if (e.Item.ItemType == ListItemType.Footer)
    //        {
    //            Label lblFooter = (Label)e.Item.FindControl("lblEmptyData");
    //            lblFooter.Visible = true;
    //        }
    //    }
    //}

    protected void gvWMP_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
                gvWMP.PageIndex = e.NewPageIndex;
                BindGridView();               
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Please select the page number.";
            }
        }
    }
}