using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExternalUser_RelaxationApplication_RelaxationApplication : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;

            BindGrid();
        }
    }


    void BindGrid()
    {
        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));

        if (obj_externalUser != null && obj_externalUser.ExternalUserCode > 0)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            SqlCommand cmd = new SqlCommand("spGetRelaxationApplicationList", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("@bintFilterApplicantExUserCode", obj_externalUser.ExternalUserCode);
            cmd.Parameters.Add(param);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            gvRelaxationApplication.DataSource = ds;
            gvRelaxationApplication.DataBind();
        }
        else
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

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
                   // int_IndAppCode = Convert.ToInt32(e.CommandArgument.ToString());
                    lblMode.Text = "Edit";
                    lblPageTitle.Text = Page.Title;
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(e.CommandArgument);
                    //Server.Transfer("~/ExternalUser/IndustrialNew/IndustrialNew.aspx");

                    //Server.Transfer("t.aspx");
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

}