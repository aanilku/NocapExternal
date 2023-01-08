using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.DAL.UserManagement;
using NOCAP.BLL.UserManagement;

public partial class ExternalUser_LogInMsg : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["ExternalUserCode"] == null)
            {
                //Session["ExternalUserCode"] = null;
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                FormsAuthentication.SignOut();
                Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                Response.Redirect("~/Default.aspx",false);
                //Response.Redirect("~/Default.aspx");
            }

            if (!NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
            {
                //Session["ExternalUserCode"] = null;
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                FormsAuthentication.SignOut();
                Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                Response.Redirect("~/Default.aspx", false);
                //Response.Redirect("~/Default.aspx");
            }


            if (Request.UrlReferrer == null)
            {
                //Session["ExternalUserCode"] = null;
                //Response.Redirect("http://www.google.com");
                //PreviousPage.ToString();


                Session["ExternalUserCode"] = null;
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                FormsAuthentication.SignOut();
                Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                Response.Redirect("~/Default.aspx",false);
            }



            NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
            if (obj_ExternalUser.ExternalUserName=="")
            {
                Session["ExternalUserCode"] = null;
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                FormsAuthentication.SignOut();
                Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                Response.Redirect("~/Default.aspx", false);

            }
          

            if (!IsPostBack)
            {

                NOCAP.BLL.UserManagement.ExternalUserLogIn obj_Login = new NOCAP.BLL.UserManagement.ExternalUserLogIn();

                if (obj_Login.ProccedNextToLogin(Convert.ToInt64(Session["ExternalUserCode"])) == 1)
                {
                    if (obj_Login.ExternalUserCode < 1)
                    {
                        //lblLoginMsg.Text = "Not a valid user";
                        ////lblLoginMsg.Text = obj_Login.CustumMessage;
                        //Session.Clear();
                        //Session["ExternalUserCode"] = 0;
                        //Session.Timeout = 30;


                        Session["ExternalUserCode"] = null;
                        Session.Clear();
                        Session.RemoveAll();
                        Session.Abandon();
                        FormsAuthentication.SignOut();
                        Response.Redirect("~/Default.aspx",false);

                    }
                    else
                    {
                        NOCAP.BLL.UserManagement.ExternalUser obj_exUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                        lblLoginMsg.Text = "<b>Welcome : </b>" + HttpUtility.HtmlEncode(obj_exUser.ExternalUserName);
                        ///---------------------------added by chirag on 10-05-2016 for updated library----------------------//
                        NOCAP.BLL.Audit.ExternalUserAuditTrail obj_ExternalUserAuditTrail = new NOCAP.BLL.Audit.ExternalUserAuditTrail();
                        obj_ExternalUserAuditTrail = obj_ExternalUserAuditTrail.GetExUserLastLoginDetail(Convert.ToInt64(Session["ExternalUserCode"]));
                        if(obj_ExternalUserAuditTrail!=null)
                        {
                            lblLoginHitory.Text += "<b>Previous Login Date Time:</b> " + HttpUtility.HtmlEncode(obj_ExternalUserAuditTrail.CreatedDate.ToString("dd/MM/yyyy HH:mm:ss tt")) + " <b>, IP Address:</b> " + HttpUtility.HtmlEncode(obj_ExternalUserAuditTrail.IP_Address);
                        }

                        //----------------------code by chirag end here-------------------------------------------------------//

                            //ActionTrail objLoginHistory = ActionTrailDAL.GetExtLoginHistory(Convert.ToInt64(Session["ExternalUserCode"]));
                            //if (objLoginHistory != null)
                            //{
                            //    lblLoginHitory.Text += "<b>Previous Login Date Time:</b> " + objLoginHistory.CreatedDate.ToString("dd/MM/yyyy HH:mm:ss tt") + " <b>, IP Address:</b> " + objLoginHistory.IP_Address;
                            //}
                         
                    }
                    //Session["ExternalUserCode"] = obj_Login.ExternalUserCode;
                    //Response.Write(obj_Login.CustumMessage);
                    //lblMsg.Text = obj_Login.CustumMessage;
                    //FormsAuthentication.RedirectFromLoginPage(obj_Login.ExternalUserName, false);
                }
                else
                {

                    //lblLoginMsg.Text = obj_Login.CustumMessage;
                    //Session.Clear();
                    //Session["ExternalUserCode"] = 0;
                    //Session.Timeout = 30;



                    Session["ExternalUserCode"] = null;
                    Session.Clear();
                    Session.RemoveAll();
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Default.aspx",false);

                    //Response.Write(obj_Login.CustumMessage);
                }

            }

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
}