using NOCAP.BLL.Grievance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExternalUser_Grievance_Submitted : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value; 

           BindGrievanceApplicationGrid();
            //BindGrievanceAppealGrid();

        }
    }
    void BindGrievanceApplicationGrid()
    {
        try
        {
            GrievanceApplication obj_grievanceApplication = new GrievanceApplication();

            obj_grievanceApplication.ApplicantExUserCode = Convert.ToInt64(Session["ExternalUserCode"]);
            List<GrievanceApplicationExt> arrfilterGrievanceApplication = obj_grievanceApplication.GetGrievanceApplicationListExt().Where(x => x.GrievanceDepth == null).ToList();
            if (arrfilterGrievanceApplication != null && arrfilterGrievanceApplication.Count > 0)
            {
                gvGrievance.DataSource = arrfilterGrievanceApplication;
                gvGrievance.DataBind();
            }             
        }
        catch (Exception Ex)
        {

        }
    }    
    void Fresh()
    {
        List<GrievanceApplication> GrievanceApplicationList = new List<GrievanceApplication>();
        GrievanceApplicationList.Add(new GrievanceApplication { GrievanceCode = 1, GrievanceNumber = "GN/2022", FirstGrievanceCode = null, NextAppealSubmitted = NOCAP.BLL.Common.CommonEnum.FlagYesNo.Yes, Remark = "A Prorammer's Guide to ADO.NET1" });
        GrievanceApplicationList.Add(new GrievanceApplication { GrievanceCode = 2, GrievanceNumber = "GN/2022", FirstGrievanceCode = null, NextAppealSubmitted = NOCAP.BLL.Common.CommonEnum.FlagYesNo.No, Remark = "A Prorammer's Guide to ADO.NET2" });
        GrievanceApplicationList.Add(new GrievanceApplication { GrievanceCode = 3, GrievanceNumber = "GN/2022", FirstGrievanceCode = null, NextAppealSubmitted = NOCAP.BLL.Common.CommonEnum.FlagYesNo.NotDefined, Remark = "A Prorammer's Guide to ADO.NET3" });



        gvGrievance.DataSource = GrievanceApplicationList;
        gvGrievance.DataBind();

    }
    protected void lbtnEdit_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandArgument != null)
                {                  
                    lblGrievanceCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void lbtnEdit_Click2(object sender, CommandEventArgs e)
    {
       
        hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRFs.Value;
            try
            {
                if (e.CommandArgument != null)
                {
                    lblGrievanceCode.Text = HttpUtility.HtmlEncode(e.CommandArgument.ToString());  //added html encode lblApplicationCode.Text = e.CommandArgument.ToString();
                    Server.Transfer("~/ExternalUser/Grievance/SubmitAppeal.aspx");
                }
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value; 
        }
    }
    protected void btnFristAppeal_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandArgument != null)
                {                   
                    lblGrievanceCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);                   
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                long lng_grievanceCode = Convert.ToInt64(gvGrievance.DataKeys[e.Row.RowIndex].Values["GrievanceCode"]);
                GridView gvShowReGrievance = e.Row.FindControl("gvShowReGrievance") as GridView;
                Button lbtnEdit = e.Row.FindControl("lbtnEdit") as Button;
                Label lblFileClosed= e.Row.FindControl("lblFileClosed") as Label;
                if (lblFileClosed.Text == "Yes")
                {
                    GrievanceAppealExt obj_GrievanceAppeal = new GrievanceAppealExt();
                    obj_GrievanceAppeal.GrievanceCode = lng_grievanceCode;
                    GrievanceAppealExt[] Arr_GrievanceAppealExt = obj_GrievanceAppeal.GetGrievanceAppealExt();
                    GrievanceAppealExt[] Arr_GrievanceAppealExttemp = Arr_GrievanceAppealExt.Where(a => a.FileClosed == NOCAP.BLL.Common.CommonEnum.FlagYesNo.No).ToArray();
                    if (Arr_GrievanceAppealExttemp != null && Arr_GrievanceAppealExttemp.Length > 0)
                    {
                        lbtnEdit.Enabled = false;
                    }
                    else
                    {
                        if (Arr_GrievanceAppealExt != null && Arr_GrievanceAppealExt.Length > 0)
                        {
                            if (Arr_GrievanceAppealExt != null && Arr_GrievanceAppealExt[Arr_GrievanceAppealExt.Length-1].Depth <= 2)
                                lbtnEdit.Enabled = true;
                            else
                                lbtnEdit.Visible = false;
                        }
                        else
                            lbtnEdit.Enabled = true;
                    }
                    gvShowReGrievance.DataSource = obj_GrievanceAppeal.GetGrievanceAppealExt();
                    gvShowReGrievance.DataBind();
                }
                else
                    lbtnEdit.Enabled = false;
            }
        }
        catch (Exception EX)
        {

        }    
    }   
}