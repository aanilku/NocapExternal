using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Net.NetworkInformation;
using System.Net;
public partial class ExternalLogin : System.Web.UI.Page
{
    string strPageName = "ExternalLogin";
    string strActionName = "";
    string strStatus = "";
    string strPubipAddress = string.Empty;
    string strhostName = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack != true)
        {
            //if (Session["ExternalUserCode"] == null)
            //{
            //    //Response.Write("NULL");
            //}
            //string str= Context.Request.QueryString["previousUrl"];
            //ViewState["previousUrl"] = Context.Request.QueryString["previousUrl"];


            Session.Clear();
            lblMsg.Text = "";
            Session["ExternalUserCode"] = 0;
            // Session.Timeout = 30;

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;

            this.txtUserPassword.Attributes.Add("onkeypress", "callButtonEvent(this,'" + this.btnLogin.ClientID + "')");
            this.txtCaptchaCode.Attributes.Add("onkeypress", "callButtonEvent(this,'" + this.btnLogin.ClientID + "')");
            // ad new code
            //Random randomclass1 = new Random();
            Session["rno"] = NOCAPExternalUtility.Create16DigitString();
            //Response.Write(NOCAPExternalUtility.Create16DigitString());
            //  randomclass1.Next();
            btnLogin.Attributes.Add("onclick", "javascript:return combineMD5Sha512();");

            lbnNewYserRegi.Attributes.Add("onclick", "javascript:clearTxtPwd();");
            lbtnForgetPassword.Attributes.Add("onclick", "javascript:clearTxtPwd();");
            LinkButton3.Attributes.Add("onclick", "javascript:clearTxtPwd();");
            lnkGoHome.Attributes.Add("onclick", "javascript:clearTxtPwd();");
            imgBtnCaptchaRefresh.Attributes.Add("onclick", "javascript:clearTxtPwd();");
            lbtnForgetLoginId.Attributes.Add("onclick", "javascript:clearTxtPwd();");
        }


    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;

            if (txtCaptchaCode.Text.Trim() != "")
            {
                // if (Convert.ToString(Session["Captcha"]) == txtCaptchaCode.Text.Trim())
                if (true)
                {
                    NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(txtUserName.Text);
                    bool LoginSuccess = false;
                    try
                    {
                        if (obj_ExternalUser.ExternalUserCode > 0)
                        {
                            string paswordSalt = Session["rno"].ToString() + obj_ExternalUser.PwdHash;
                            string str_hasPassSalt = "";
                            string str_hdnResultValue = "";
                            if (obj_ExternalUser.PwdHash.Length < 50)
                            {
                                str_hasPassSalt = FormsAuthentication.HashPasswordForStoringInConfigFile(paswordSalt, "MD5");
                                str_hdnResultValue = hdnResultValue.Value;
                            }
                            else
                            {
                                str_hasPassSalt = NOCAPExternalUtility.GenerateSHA512String(paswordSalt);
                                str_hdnResultValue = hdnResultValuesha.Value;
                            }
                            //string hasPassSalt = FormsAuthentication.HashPasswordForStoringInConfigFile(paswordSalt, "MD5");

                            ////////////////////////////////change before production-make ==

                            if (str_hdnResultValue == str_hasPassSalt.ToLower())
                            {
                                //FormsAuthentication.SetAuthCookie(obj_ExternalUser.ExternalUserName, false);
                                FormsAuthentication.RedirectFromLoginPage(obj_ExternalUser.ExternalUserName, false);

                                SessionIDManager Manager = new SessionIDManager();
                                string NewID = Manager.CreateSessionID(Context);
                                bool redirected = false;
                                bool IsAdded = false;
                                Manager.SaveSessionID(Context, NewID, out redirected, out IsAdded);
                                strActionName = "Login";
                                strStatus = "Login Success";
                                LoginSuccess = true;

                                string stren = NOCAPExternalUtility.Encrypt(obj_ExternalUser.ExternalUserName);
                                Response.Redirect("~/ExternalUser/IntermediateExternalLogin.aspx?UserName=" + Server.UrlEncode(Server.HtmlEncode(stren)), false);

                            }
                            else
                            {
                                strActionName = "Login";
                                strStatus = "Login Failed";
                                txtCaptchaCode.Text = "";
                                lblMsg.Text = "Either User Name or Password is wrong";
                                return;
                            }
                        }
                        else
                        {
                            strActionName = "Login";
                            strStatus = "Login Failed";
                            txtCaptchaCode.Text = "";
                            lblMsg.Text = obj_ExternalUser.CustumMessage;
                            return;
                        }

                    }
                    catch (Exception)
                    {
                        strActionName = "Login";
                        strStatus = "Login Failed";
                        Response.Redirect("~/ExternalErrorPage.aspx", false);
                        //  lblMsg.Text = "error on page";
                    }
                    finally
                    {
                        NOCAP.BLL.Audit.ExternalUserAuditTrail obj_externalUserAuditTrail = new NOCAP.BLL.Audit.ExternalUserAuditTrail();
                        strhostName = Dns.GetHostName();
                        if (obj_ExternalUser != null)
                        {
                            obj_externalUserAuditTrail.ExUserCode = obj_ExternalUser.ExternalUserCode;
                            obj_externalUserAuditTrail.IP_Address = Request.UserHostAddress;
                            obj_externalUserAuditTrail.PublicIp_Address = strPubipAddress;
                            obj_externalUserAuditTrail.HostName = strhostName;
                            if (LoginSuccess)
                            {
                                obj_externalUserAuditTrail.LoginSuccess = NOCAP.BLL.Audit.ExternalUserAuditTrail.enum_LoginSuccess.Yes;
                                obj_externalUserAuditTrail.Remarks = "Login Success";

                            }
                            else
                            {
                                obj_externalUserAuditTrail.LoginSuccess = NOCAP.BLL.Audit.ExternalUserAuditTrail.enum_LoginSuccess.No;
                                obj_externalUserAuditTrail.Remarks = "Login Failed";

                            }
                            if (obj_externalUserAuditTrail.Add() == 1)
                            {
                                obj_externalUserAuditTrail.CustumMessage = "Record Added Succesfully";

                            }
                            else
                            {
                                obj_externalUserAuditTrail.CustumMessage = "Error in Adding Record to Audit Trail";
                            }

                        }

                        ActionTrail obj_ExtActionTrail = new ActionTrail();
                        if (obj_ExternalUser.ExternalUserCode > 0)
                        {
                            obj_ExtActionTrail.UserCode = obj_ExternalUser.ExternalUserCode;
                            obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
                            obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
                            obj_ExtActionTrail.Status = strStatus;
                            if (obj_ExtActionTrail != null)
                                ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
                        }
                    }
                }
                else
                {
                    lblMsg.Text = "Please Enter Valid Captcha Code";
                    txtCaptchaCode.Text = "";
                    return;
                }
            }
            else
            {
                lblMsg.Text = "Please Enter Captcha Code";
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Please Enter Captcha Code');", true);
                txtCaptchaCode.Focus();
                return;
            }
        }
    }

    protected void lbnNewYserRegi_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            Response.Redirect("Sub/ApplicantRegi/ApplicantRegistration.aspx");
        }
    }

    protected void lbtnForgetPassword_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            Response.Redirect("~/ExternalForgetPassword.aspx", false);
        }
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            Response.Redirect("~/");
        }
    }
    protected void imgBtnCaptchaRefresh_Click(object sender, ImageClickEventArgs e)
    {
        txtCaptchaCode.Text = "";
    }
    protected void lbtnForgetLoginId_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            Response.Redirect("~/ExternalForgetLoginId.aspx", false);
        }
    }
}