using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_UserManagement_ChangePassword : System.Web.UI.Page
{
    string strPageName = "ChangePassword";
    string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                //Random randomclass = new Random();
                ViewState["rno"] = NOCAPExternalUtility.Create16DigitString(); //randomclass.Next();
                btnChangePassword.Attributes.Add("onclick", "javascript:return combineMD5Sha512(" + ViewState["rno"] + ");");
                lnkHome.Attributes.Add("onclick", "javascript:ClearControl();");

                 //LinkButton LinkButton1 = (LinkButton)Master.FindControl("LinkButton1");
                 //if (LinkButton1 != null)
                 //{
                 //    LinkButton1.Attributes.Add("onclick", "javascript:ClearControl();");
                 //}
                 //LinkButton LinkButton2 = (LinkButton)Master.FindControl("LinkButton2");
                 //if (LinkButton2 != null)
                 //{
                 //    LinkButton2.Attributes.Add("onclick", "javascript:ClearControl();");
                 //}
                LinkButton lbnLogout = (LinkButton)Master.FindControl("lbnLogout");
                if (lbnLogout != null)
                {
                    lbnLogout.Attributes.Add("onclick", "javascript:ClearControl();");
                }

                 Menu ExternalMenu = (Menu)Master.FindControl("ExternalMenu");
                 if (ExternalMenu != null)
                 {
                     ExternalMenu.Visible = false;
                 }

                 btnGoHome.Attributes.Add("onclick", "javascript:ClearControl();");


               //  HtmlControl topmenu = (HtmlControl)Master.FindControl("topmenu");
               //  Menu ExternalMenu = (Menu)Master.FindControl("ExternalMenu");

               //// ExternalMenu
               //  if (ExternalMenu != null)
               //  {
               //      ExternalMenu.Attributes.Add("onclick", "javascript:ClearControl();");
               //  }

            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }


    protected void btnGoHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ExternalUser/ApplicantHome.aspx",false);
    }
    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        strActionName = "Submit";
        if (Page.IsValid)
        {
            try
            {


                int changePssswordStatus = 0;
                int externalusercode = 0;
                externalusercode = Convert.ToInt32(Session["ExternalUserCode"]);

                if (externalusercode > 0)
                {
                    NOCAP.BLL.UserManagement.ExternalUser exu = new NOCAP.BLL.UserManagement.ExternalUser(externalusercode);
                    //exu.ActionPerformByUserCode = externalusercode;
                    exu.ModifiedByExUC = externalusercode;
                    exu.ModifiedByUC = null;
                    /// start   
                    string seed = ViewState["rno"].ToString();
                    string OldhashvalueMD5 = hdnOldResultValue.Value.ToString();
                    string[] old_passMD5 = Regex.Split(OldhashvalueMD5, seed);
                    string old_passwordMD5 = old_passMD5[0].ToString();

                    string Oldhashvaluesha = hdnBeforeOldResultValuesha512.Value.ToString();
                    string[] old_passsha = Regex.Split(Oldhashvaluesha, seed);
                    string old_passwordsha = old_passsha[0].ToString();

                    string new_passwordhdn = hdnNewResultValue.Value.ToString();
                    string[] new_pass = Regex.Split(new_passwordhdn, seed);
                    string new_password = new_pass[0].ToString();

                    // Check for Confirm and New Passwords   ///
                    string con_passwordhdn = hdnConfResultValue.Value.ToString();
                    string[] con_pass = Regex.Split(con_passwordhdn, seed);
                    string con_password = con_pass[0].ToString();

                    if (new_password == con_password)
                    {
                        if (exu != null)
                        {
                            //  User myUser = UserDB.GetItem(user_id);
                            string paswordSalt = seed + exu.PwdHash;
                            string str_hdnResultValue = "";
                            string user_Oldpassword = "";
                            string old_password = "";

                            if (exu.PwdHash.Length < 50)
                            {
                                user_Oldpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(paswordSalt, "MD5");
                                str_hdnResultValue = OldhashvalueMD5;
                                old_password = old_passwordMD5;
                            }
                            else
                            {
                                user_Oldpassword = NOCAPExternalUtility.GenerateSHA512String(paswordSalt);
                                str_hdnResultValue = Oldhashvaluesha;
                                old_password = old_passwordsha;
                            }

                            //string user_Oldpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(paswordSalt, "MD5");
                            if (str_hdnResultValue == user_Oldpassword.ToLower())
                            {
                                if (new_password == "" || new_password == null)
                                {
                                    lblMsg.Text = "Please enter valid password";
                                    return;
                                }
                                //check old password and new password not be same
                                if (new_password != exu.PwdHash)
                                {  //success
                                    changePssswordStatus = exu.ChangePassword(old_password, new_password, con_password);
                                    if (changePssswordStatus == 1)
                                    {
                                        strStatus = "Change Password Successfully !";
                                        lblMsg.Text = "Password Changed Successfully";
                                        //ClientScript.RegisterStartupScript(this.GetType(), "Confirmation", "alert('Password Changed Successfully');", true);
                                        lblMsg.ForeColor = System.Drawing.Color.Green;
                                        Response.Redirect("~/ExternalUser/UserManagement/SuccessPage.aspx", false);
                                    }
                                    else
                                    {
                                        strStatus = "Change Password Failed !";
                                        lblMsg.Text = exu.CustumMessage.ToString();
                                    }
                                }
                                else
                                {
                                    strStatus = "Current password and New Password should not be same!";
                                    lblMsg.Text = "Current password and New Password should not be same!";
                                }
                            }
                            else
                            {
                                strStatus = "Please enter valid password !";
                                lblMsg.Text = "Please enter valid password";
                            }
                        }
                        else
                        {
                            strStatus = "Invalid User!";
                            lblMsg.Text = "Invalid User";
                        }
                    }
                    else
                    {
                        strStatus = "New Password and Re-Enter New Password is not Same !";
                        lblMsg.Text = "New Password and Re-Enter New Password is not Same";
                    }
                }
                else
                {
                    strStatus = "Invalid User!";
                    lblMsg.Text = "Invalid User- please Login again";
                    //ClientScript.RegisterStartupScript(this.GetType(), "Error while Validating User", "alert('Invalid User- please Login again');", true);
                    Session.Clear();
                    Session["ExternalUserCode"] = 0;
                    Response.Redirect("~/default.aspx", false);
                }
            }
            catch (Exception)
            {
                strStatus = "Change Password Failed !";
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                ActionTrail obj_ExtActionTrail = new ActionTrail();
                if (Session["ExternalUserCode"] != null)
                {
                    obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
                    obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
                    obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
                    obj_ExtActionTrail.Status = strStatus;
                    if (obj_ExtActionTrail != null)
                        ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
                }

            }
        }
    }
}