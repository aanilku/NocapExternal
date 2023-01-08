using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP;
public partial class Sub_Report_Penalty_Penalty : System.Web.UI.Page
{
    //string strPageName = "Penalty";
    //string strActionName = "";
    //string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                BindPenaltyDetails(NOCAP.BLL.Master.Penalty.SortingField.SortOrder.ToString());
                BindCorrectionChargeDetails(NOCAP.BLL.Master.CorrectionCharge.SortingField.SortOrder.ToString());
            }
            catch (Exception)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);
            }
        }
    }
    private void BindPenaltyDetails(string str_sortfieldName = "")
    {
        try
        {
            
            NOCAP.BLL.Master.Penalty obj_penalty = new NOCAP.BLL.Master.Penalty();
            NOCAP.BLL.Master.Penalty obj_penaltyBlank = new NOCAP.BLL.Master.Penalty();
            List<NOCAP.BLL.Master.Penalty> lst_penalty = new List<NOCAP.BLL.Master.Penalty>();

            int int_status = 0;
            switch (str_sortfieldName)
            {

                case "PenaltyCode":
                    int_status = obj_penalty.GetAll(NOCAP.BLL.Master.Penalty.SortingField.PenaltyCode);
                    break;
                case "PenaltyDesc":
                    int_status = obj_penalty.GetAll(NOCAP.BLL.Master.Penalty.SortingField.PenaltyDesc);
                    break;
                case "SortOrder":
                    int_status = obj_penalty.GetAll(NOCAP.BLL.Master.Penalty.SortingField.SortOrder);
                    break;
                default:
                    int_status = obj_penalty.GetAll(NOCAP.BLL.Master.Penalty.SortingField.PenaltyDesc);
                    break;
            }
            NOCAP.BLL.Master.Penalty[] arr_penalty;
            arr_penalty = obj_penalty.PenaltyCollection;
            if ((int_status == 1))
            {

                if (arr_penalty.Count() > 0)
                {

                    gvPenalty.DataSource = arr_penalty;
                    gvPenalty.DataBind();

                }
                else
                {

                    lst_penalty.Add(obj_penaltyBlank);
                    gvPenalty.DataSource = lst_penalty;
                    gvPenalty.DataBind();
                    int int_NoOfCol = 0;
                    int_NoOfCol = gvPenalty.Rows[0].Cells.Count;
                    gvPenalty.Rows[0].Cells.Clear();
                    gvPenalty.Rows[0].Cells.Add(new TableCell());
                    gvPenalty.Rows[0].Cells[0].ColumnSpan = int_NoOfCol;
                    gvPenalty.Rows[0].Cells[0].Text = "No Records exsist in Penalty";
                }
            }

            else
            {
                lblMessage.Text = HttpUtility.HtmlEncode(obj_penalty.CustumMessage);


            }
        }
        catch (Exception)
        {
            Response.Redirect("~/InternalErrorPage.aspx", false);
            //lblMessage.Text = ex.Message;

        }
        finally
        {


        }
    }
    private void BindCorrectionChargeDetails(string str_sortfieldName = "")
    {
        try
        {

            NOCAP.BLL.Master.CorrectionCharge obj_correctionCharge = new NOCAP.BLL.Master.CorrectionCharge();
            NOCAP.BLL.Master.CorrectionCharge obj_correctionChargeBlank = new NOCAP.BLL.Master.CorrectionCharge();
            List<NOCAP.BLL.Master.CorrectionCharge> lst_correctionCharge = new List<NOCAP.BLL.Master.CorrectionCharge>();

            int int_status = 0;
            switch (str_sortfieldName)
            {

                case "CorrectionChargeCode":
                    int_status = obj_correctionCharge.GetAll(NOCAP.BLL.Master.CorrectionCharge.SortingField.CorrectionChargeCode);
                    break;
                case "CorrectionChargeDesc":
                    int_status = obj_correctionCharge.GetAll(NOCAP.BLL.Master.CorrectionCharge.SortingField.CorrectionChargeDesc);
                    break;
                case "SortOrder":
                    int_status = obj_correctionCharge.GetAll(NOCAP.BLL.Master.CorrectionCharge.SortingField.SortOrder);
                    break;
                default:
                    int_status = obj_correctionCharge.GetAll(NOCAP.BLL.Master.CorrectionCharge.SortingField.CorrectionChargeDesc);
                    break;
            }
            NOCAP.BLL.Master.CorrectionCharge[] arr_correctionCharge;
            arr_correctionCharge = obj_correctionCharge.CorrectionChargeCollection;
            if ((int_status == 1))
            {

                if (arr_correctionCharge.Count() > 0)
                {

                    gvCorrectionCharge.DataSource = arr_correctionCharge;
                    gvCorrectionCharge.DataBind();

                }
                else
                {

                    lst_correctionCharge.Add(obj_correctionChargeBlank);
                    gvCorrectionCharge.DataSource = lst_correctionCharge;
                    gvCorrectionCharge.DataBind();
                    int int_NoOfCol = 0;
                    int_NoOfCol = gvCorrectionCharge.Rows[0].Cells.Count;
                    gvCorrectionCharge.Rows[0].Cells.Clear();
                    gvCorrectionCharge.Rows[0].Cells.Add(new TableCell());
                    gvCorrectionCharge.Rows[0].Cells[0].ColumnSpan = int_NoOfCol;
                    gvCorrectionCharge.Rows[0].Cells[0].Text = "No Records exsist in CorrectionCharge";
                }
            }

            else
            {
                lblMessage.Text = HttpUtility.HtmlEncode(obj_correctionCharge.CustumMessage);


            }
        }
        catch (Exception)
        {
            Response.Redirect("~/InternalErrorPage.aspx", false);
        }
        finally
        {


        }
    }
    protected void gvCorrectionCharge_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
                gvCorrectionCharge.EditIndex = -1;
                gvCorrectionCharge.PageIndex = e.NewPageIndex;
                BindCorrectionChargeDetails(NOCAP.BLL.Master.CorrectionCharge.SortingField.SortOrder.ToString());
            }
            catch (Exception)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);
            }
        }
    }

    //protected void gvPenalty_Sorting(object sender, GridViewSortEventArgs e)
    //{


    //    if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
    //    {
    //        Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
    //    }
    //    else
    //    {

    //        hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
    //        Session["CSRF"] = hidCSRF.Value;
    //        try
    //        {

    //            NOCAP.BLL.Master.Penalty obj_penalty = new NOCAP.BLL.Master.Penalty();
    //            lblSortField.Text = HttpUtility.HtmlEncode(e.SortExpression);
    //            gvPenalty.EditIndex = -1;
    //            BindPenaltyDetails(lblSortField.Text);
    //            lblMessage.Text = "";
    //        }

    //        catch (Exception)
    //        {
    //            Response.Redirect("~/InternalErrorPage.aspx", false);
    //            //lblMessage.Text = ex.Message;

    //        }
    //    }
    //}
    protected void gvPenalty_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
                gvPenalty.EditIndex = -1;
                gvPenalty.PageIndex = e.NewPageIndex;
                BindPenaltyDetails(NOCAP.BLL.Master.Penalty.SortingField.SortOrder.ToString());
            }
            catch (Exception)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);
                //lblMessage.Text = ex.Message;

            }
        }
    }

    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
    //    {
    //        Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
    //    }
    //    else
    //    {

    //        hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
    //        Session["CSRF"] = hidCSRF.Value;
    //        strActionName = "Submit";
    //        try
    //        {
    //            if (Page.IsValid)
    //            {
    //                //NOCAP.BLL.Master.AreaType obj_insertAreaType = new NOCAP.BLL.Master.AreaType();
    //                //NOCAP.BLL.UserManagement.User obj_internalUser = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(Session["InternalUserCode"]));



    //                //if (obj_internalUser.UserName != "" && obj_internalUser.UserCode > 0)
    //                //{
                        
    //                        BindPenaltyDetails();
                        


    //                //}
    //                //else
    //                //{
    //                //    strStatus = "Record Submit Failed !";
    //                //    lblMessage.Text = "Not a valid user";
    //                //    lblMessage.ForeColor = System.Drawing.Color.Red;

    //                //}

    //            }

    //        }
    //        catch (Exception)
    //        {
    //            strStatus = "Record Submit Failed !";
    //            Response.Redirect("~/InternalErrorPage.aspx", false);
    //            //lblMessage.Text = ex.Message;

    //        }

    //        finally
    //        {
    //            //txtAreaTypeDescription.Text = "";
    //            ActionTrail obj_IntActionTrail = new ActionTrail();
    //            if (Session["InternalUserCode"] != null)
    //            {
    //                obj_IntActionTrail.UserCode = Convert.ToInt64(Session["InternalUserCode"]);
    //                obj_IntActionTrail.IP_Address = Request.UserHostAddress;
    //                obj_IntActionTrail.ActionPerformed = strPageName + "-" + strActionName;
    //                obj_IntActionTrail.Status = strStatus;
    //                if (obj_IntActionTrail != null)
    //                    ActionTrailDAL.IntActionSave(obj_IntActionTrail);
    //            }

    //        }
    //    }
    //}
    
}