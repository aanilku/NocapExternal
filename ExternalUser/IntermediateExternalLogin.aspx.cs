using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class IntermediateExternalLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

              
                    if (Request.QueryString["UserName"] != null)
                    {
                        NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(NOCAPExternalUtility.Decrypt(Server.HtmlDecode(Request.QueryString["UserName"])));
                        if (obj_ExternalUser.ExternalUserCode > 0)
                        {
                            Session["ExternalUserCode"] = obj_ExternalUser.ExternalUserCode;
                            if (obj_ExternalUser.PwdHash.Length < 50)
                                Response.Redirect("~/ExternalUser/UserManagement/ChangePassword.aspx", false);
                            else
                                Response.Redirect("~/ExternalUser/ApplicantHome.aspx", false);


                        }
                        else
                        {
                            Response.Redirect("~/ExternalLogin.aspx", false);
                        }
                    }
                
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
}