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
public partial class ExternalForgetLoginId : System.Web.UI.Page
{
    string strPageName = "ExternalForgetLoginId";
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
                ViewState["rno"] = NOCAPExternalUtility.Create16DigitString();


                revtxtMobileNo.ValidationExpression = ValidationUtility.txtValForMobileNumber;
                revtxtMobileNo.ErrorMessage = ValidationUtility.txtValForMobileNumberMsg;
                rvDOB.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                rvDOB.MinimumValue = DateTime.Now.AddYears(-100).ToString("dd/MM/yyyy");

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
            Response.Redirect("~/ExternalLogin.aspx", false);
        }
    }

   

    protected void btnForgetLoginId_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;

            if (SMSUtility.IsSendSMSEnable())
            {
                NOCAP.BLL.UserManagement.ExternalUser[] obj_ExternalUserArray = null;
                try
                {
                    NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser();
                     obj_ExternalUserArray = obj_ExternalUser.GetAllExternalUserForSearch(str_MobileNumber: txtMobileNo.Text.Trim(), dt_DateOfBirth: Convert.ToDateTime(txtDOB.Text), enu_SortField: NOCAP.BLL.UserManagement.ExternalUser.SortingField.ExternalUserCode);
                    if (obj_ExternalUserArray != null && obj_ExternalUserArray.Length == 1)
                    {
                        if (obj_ExternalUserArray[0].ExternalUserCode > 0 && obj_ExternalUserArray[0].ExternalUserMobileNumber.Length==10)
                        {
                            string strMobileNo = obj_ExternalUserArray[0].ExternalUserMobileNumber;
                            string OTPMessage = "";
                            strMobileNumberTo = strMobileNo;
                            OTPMessage = obj_ExternalUserArray[0].ExternalUserName;
                            lng_ExuserCode = obj_ExternalUserArray[0].ExternalUserCode;
                            // Session["OTP"] = OTPMessage;
                            OTPMessage = "NOCAP- Dear " + obj_ExternalUserArray[0].ExternalUserFirstName + ", Your User Name is : " + OTPMessage+"-CGWA";
                            if (SMSUtility.sendOTPtoMobile(OTPMessage, strMobileNo, "1007161718798368076", out  strSMSUserName).Trim() == "Platform accepted")
                            {
                                strStatus = "Message Sent Successfully !"; 
                                //lblMsg.Text = "Your Login ID has been Sent Successfully to your Mobile No";
                                ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Your User Name has been sent to your Mobile Number');", true);
                                SaveSMSAlert();
                                txtDOB.Text = "";
                                txtMobileNo.Text = "";
                            }
                            else
                            {
                                strStatus = "Message Sent Failed";
                                lblMsg.Text = "Invalid User";
                                ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Please try again');", true);
                            }
                        }
                        else
                        {
                            strStatus = "Message Sent Failed";
                            lblMsg.Text = "Invalid User";
                            ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Not a valid Mobile Number/DOB');", true);
                        }
                    }
                    else
                    {
                        strStatus = "Message Sent Failed";
                        lblMsg.Text = "Invalid User";
                        ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Not a valid Mobile Number/DOB');", true);
                    }
                }
                catch (Exception)
                {
                    strStatus = "Message Sent Failed";
                    lblMsg.Text = "Invalid User";
                    Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
                }
                finally
                {
                    if (obj_ExternalUserArray != null && obj_ExternalUserArray.Length == 1)
                    {
                        ActionTrail obj_ExtActionTrail = new ActionTrail();
                        if (obj_ExternalUserArray[0].ExternalUserCode > 0)
                        {
                            obj_ExtActionTrail.UserCode = obj_ExternalUserArray[0].ExternalUserCode;
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
            strMessage = "User Name has been sent Successfully.";
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
                strStatus = "SMS Alert Saved Successfully.";
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
            if (lng_AppCode != null)
            {
                obj_ExtActionTrail.UserCode = Convert.ToInt64(lng_AppCode);
                obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
                obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
                obj_ExtActionTrail.Status = strStatus;
                if (obj_ExtActionTrail != null)
                    ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
            }
        }


    }

}