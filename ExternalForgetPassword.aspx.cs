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
using System.Text.RegularExpressions;
public partial class ExternalLogin : System.Web.UI.Page
{
    string strPageName = "ExternalForgetPassword";
    string strActionName = "";
    string strStatus = "";

    long? lng_AppCode = null;
    int? int_AppTypeCode = null;
    int? int_AppPurposeCode = null;
    int? int_UserCode = null;
    int? int_AlertStagesCode = null;
    long? lng_ExuserCode = null;
    int? int_CreatedByUC = null;

    long? lng_CreatedByExUC = null;
    string strMessage = "";
    string strMobileNumberTo = "";
    string strSMSUserName = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;

                //Random randomclass = new Random();
                ViewState["rno"] = NOCAPExternalUtility.Create16DigitString(); //randomclass.Next();
                btnResetPassword.Attributes.Add("onclick", "javascript:return sha512auth(" + ViewState["rno"] + ");");

                lnkHome.Attributes.Add("onclick", "javascript:ClearControl();");
                btnGoHome.Attributes.Add("onclick", "javascript:ClearControl();");

            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }


    protected void btnGoHome_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;

            Response.Redirect("~/", false);
        }
    }
    protected void btnResetPassword_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;

            strActionName = "Reset Password";
            NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(txtUserName.Text);
            if (Page.IsValid)
            {
                try
                {
                    int resetPssswordStatus = 0;
                    if (Convert.ToString(Session["OTP"]) == txtOPT.Text)
                    {

                        string seed = ViewState["rno"].ToString();

                        string new_passwordhdn = hdnNewResultValue.Value.ToString();
                        string[] new_pass = Regex.Split(new_passwordhdn, seed);
                        string new_password = new_pass[0].ToString();

                        // Check for Confirm and New Passwords   ///
                        string con_passwordhdn = hdnConfResultValue.Value.ToString();
                        string[] con_pass = Regex.Split(con_passwordhdn, seed);
                        string con_password = con_pass[0].ToString();

                        if (new_password == con_password)
                        {

                            if (obj_ExternalUser != null)
                            {
                                obj_ExternalUser.ActionPerformByUserCode = 1;    // this UserCode given by Anil sir.

                                if (new_password == "" || new_password == null)
                                {
                                    lblMsg.Text = "Please enter valid password";
                                    return;
                                }
                                //check old password and new password not be same
                                if (new_password != obj_ExternalUser.PwdHash)
                                {  //success
                                    resetPssswordStatus = obj_ExternalUser.ResetPassword(new_password, con_password);
                                    if (resetPssswordStatus == 1)
                                    {
                                        strStatus = "Reset Password Successfully !";
                                        lblMsg.Text = "Reset Password Successfully";
                                        //ClientScript.RegisterStartupScript(this.GetType(), "Confirmation", "alert('Password Changed Successfully');", true);
                                        lblMsg.ForeColor = System.Drawing.Color.Green;
                                        Response.Redirect("~/ExternalUser/UserManagement/SuccessPage.aspx", false);
                                    }
                                    else
                                    {
                                        strStatus = "Reset Password Failed  !";
                                        lblMsg.Text = obj_ExternalUser.CustumMessage.ToString();
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
                                strStatus = "Invalid User";
                                lblMsg.Text = "Invalid User";
                            }
                        }
                        else
                        {
                            strStatus = "New Password and Re-Enter New Password is not Same!";
                            lblMsg.Text = "New Password and Re-Enter New Password is not Same";
                        }
                    }
                    else
                    {
                        strStatus = "Invalid OTP!";
                        lblMsg.Text = "Invalid OTP";
                        //ClientScript.RegisterStartupScript(this.GetType(), "Error while Validating User", "alert('Invalid User- please Login again');", true);
                    }
                }
                catch (Exception)
                {
                    strStatus = "Reset Password Failed  !";
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
                }
                finally
                {
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
        }
    }

    //public string GetRandomNumber()
    //{
    //    try
    //    {
    //        string[] arrStr = "1,2,3,4,5,6,7,8,9,0".Split(",".ToCharArray());
    //        string strNum = string.Empty;
    //        Random r = new Random();
    //        for (int i = 0; i < 6; i++)
    //        {
    //            strNum += arrStr[r.Next(0, arrStr.Length - 1)];
    //        }
    //        return strNum;
    //    }
    //    catch (Exception)
    //    {
    //        return "";  
    //    }
    //}

    protected void lnkSendOTP_Click(object sender, EventArgs e)
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
                if (SMSUtility.IsSendSMSEnable())
                {
                    NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(txtUserName.Text);
                    if (obj_ExternalUser.ExternalUserCode > 0)
                    {
                        string strMobileNo = obj_ExternalUser.ExternalUserMobileNumber;
                        string OTPMessage = "";
                        strMobileNumberTo = strMobileNo;
                        OTPMessage = NOCAPExternalUtility.GetRandomNumber();

                        // OTPMessage = "123456";                 
                        Session["OTP"] = OTPMessage;

                        OTPMessage = "NOCAP- One Time Password (OTP) is :" + OTPMessage + "-CGWA";
                        if (SMSUtility.sendOTPtoMobile(OTPMessage, strMobileNo, "1007161718802381027", out strSMSUserName).Trim() == "Platform accepted")
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('One time Password(OTP) has been Sent to your Mobile No, Enter OTP to Complete Reset Password');", true);
                            SaveSMSAlert();
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Resend One time Password(OTP)');", true);
                        }

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Please enter valid User Name');", true);
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
        }
    }
    public void SaveSMSAlert()
    {
        try
        {
            strActionName = "InsertSMSAlert";
            NOCAP.BLL.SMSAlert.SMSAlert obj_insertSMSAlert = new NOCAP.BLL.SMSAlert.SMSAlert();



            if (lng_AppCode != null)
            {
                obj_insertSMSAlert.AppCode = Convert.ToInt64(lng_AppCode);
            }
            else
            {
                obj_insertSMSAlert.AppCode = lng_AppCode;
            }
            obj_insertSMSAlert.SMSType = NOCAP.BLL.SMSAlert.SMSAlert.SMSTypeAFR.Forget;
            obj_insertSMSAlert.NOCAPApplicationType = NOCAP.BLL.SMSAlert.SMSAlert.NOCAPApplicationTypeEI.NOCAPExternal;
            strMessage = "OTP Send Successfully for forget Password.";
            obj_insertSMSAlert.AppTypeCode = int_AppTypeCode;

            obj_insertSMSAlert.AppPurposeCode = int_AppPurposeCode;
            obj_insertSMSAlert.AlertStagesCode = int_AlertStagesCode;
            obj_insertSMSAlert.UserCode = int_UserCode;
            obj_insertSMSAlert.ExUserCode = lng_ExuserCode;

            obj_insertSMSAlert.SMSUseName = strSMSUserName;
            if (strMobileNumberTo.Trim() != "")
            {
                obj_insertSMSAlert.MobileNo = strMobileNumberTo;
            }

            obj_insertSMSAlert.Message = strMessage.Trim();

            obj_insertSMSAlert.CreatedByUserCode = int_CreatedByUC;
            obj_insertSMSAlert.CreatedByExUserCode = lng_CreatedByExUC;


            if (obj_insertSMSAlert.Add() == 1)
            {
                strStatus = "SMS Alert Saved  Successfully.";

            }
            else
            {
                strStatus = "SMS Alert Not  Saved.";
            }
        }

        catch (Exception)
        {
            strStatus = "SMS Alert Not  Saved.";

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