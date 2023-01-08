using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Drawing;
using NOCAP;
using System.Net;
using System.Text.RegularExpressions;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;


public partial class Sub_ApplicantRegi_ApplicantRegistration : System.Web.UI.Page
{
    string strPageName = "ApplicantRegistration";
    string strActionName = "";
    string strStatus = "";

    long? lng_AppCode = null;
    int? int_AppTypeCode = null;
    int? int_AppPurposeCode = null;
    int? int_UserCode = null;
    int? int_AlertStagesCode = null;
    long? lng_ExuserCode = null;
    int? int_CreatedByUC = null;
    string strSubject = "";
    string strBody = "";
    string strToEmailId = "";
    long? lng_CreatedByExUC = null;
    string strMessage = "";
    string strMobileNumberTo = "";
    string strSMSUserName = "";
    string strEmailServerName = "";

    //NOCAP.BLL.UserManagement.ExternalUser objExternalUser = new NOCAP.BLL.UserManagement.ExternalUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtDOB_CalendarExtender.EndDate = DateTime.Now;
            rvtxtDOB.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
            if (IsPostBack != true)
            {
                MultiView1.ActiveViewIndex = 0;
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;

                //Random randomclass = new Random();
                Session["rno"] = NOCAPExternalUtility.Create16DigitString();//randomclass.Next();
                btnSubmit.Attributes.Add("onclick", "javascript:return SHA512auth();");
                btnCancel.Attributes.Add("onclick", "javascript:ClearControl();");
                ddlDistrict.Attributes.Add("onclick", "javascript:ClearControl();");
                ddlGender.Attributes.Add("onclick", "javascript:ClearControl();");
                ddlIDProofType.Attributes.Add("onclick", "javascript:ClearControl();");
                ddlState.Attributes.Add("onclick", "javascript:ClearControl();");
                ddlSubDistrict.Attributes.Add("onclick", "javascript:ClearControl();");
                ddlTitle.Attributes.Add("onclick", "javascript:ClearControl();");
                lbtnCheckUserNameAvail.Attributes.Add("onclick", "javascript:ClearControl();");
                imgbtnCalendar.Attributes.Add("onclick", "javascript:ClearControl();");
                //lnkbtnRefresh.Attributes.Add("onclick", "javascript:ClearControl();");

                HtmlControl lnkHome = (HtmlControl)Master.FindControl("lnkHome");
                if (lnkHome != null)
                {
                    lnkHome.Attributes.Add("onclick", "javascript:ClearControl();");
                }

                HtmlControl spnleftmenu = (HtmlControl)Master.FindControl("spnleftmenu");
                if (spnleftmenu != null)
                {
                    spnleftmenu.Attributes.Add("onclick", "javascript:ClearControl();");
                }


                LinkButton LinkButton1 = (LinkButton)Master.FindControl("LinkButton1");
                if (LinkButton1 != null)
                {
                    LinkButton1.Attributes.Add("onclick", "javascript:ClearControl();");
                }
                LinkButton LinkButton2 = (LinkButton)Master.FindControl("LinkButton2");
                if (LinkButton2 != null)
                {
                    LinkButton2.Attributes.Add("onclick", "javascript:ClearControl();");
                }
                LinkButton LinkButton3 = (LinkButton)Master.FindControl("LinkButton3");
                if (LinkButton3 != null)
                {
                    LinkButton3.Attributes.Add("onclick", "javascript:ClearControl();");
                }

                FillDropDownGender();
                FillDropDownState();
                FillDropDownIDProofType();
                FillDropDownTitle();
                lblCheckUserNameAvailMsg.Text = "";
                lblCheckUserNameAvailMsg.ForeColor = System.Drawing.Color.Red;
                lblEnterVerificationCodeMessage.ForeColor = Color.Red;
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    protected void ValidateDate(object sender, ServerValidateEventArgs e)
    {
        if (NOCAPExternalUtility.IsValidDate(e.Value))
        {
            e.IsValid = true;
        }
        else
        {
            e.IsValid = false;
        }
    }

    #region Private Methods
    private void FillDropDownTitle()
    {
        NOCAP.BLL.Master.Title obj_title = new NOCAP.BLL.Master.Title();
        NOCAP.BLL.Master.Title[] arr;
        List<NOCAP.BLL.Master.Title> listTitle = new List<NOCAP.BLL.Master.Title>();
        try
        {
            ddlTitle.Items.Clear();
            if (obj_title.GetALL(NOCAP.BLL.Master.Title.SortingField.TitleDesc) == 1)
            {
                arr = obj_title.TitleCollection;

                if (arr.Count() > 0)
                {
                    ddlTitle.DataSource = arr;
                    ddlTitle.DataTextField = "TitleDesc";
                    ddlTitle.DataValueField = "TitleCode";
                    ddlTitle.DataBind();
                }
            }
            else
            {
                Response.Write(obj_title.CustumMessage);
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlTitle);
    }
    void SendMail(long UserCode)
    {
        try
        {
            if (EmailUtility.IsSendEmailEnable())
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(UserCode);
                if (obj_ExternalUser.ExternalUserCode > 0)
                {

                    strSubject = "NOCAP- New User Registration";
                    strBody = "<html><body><p>Dear " + " ";
                    strBody += HttpUtility.HtmlEncode(obj_ExternalUser.ExternalUserFirstName).ToUpper() + " " + HttpUtility.HtmlEncode(obj_ExternalUser.ExternalUserLastName).ToUpper();
                    strBody += " ,</p><p style='margin-left:50px;'>Your registeration is successful.:<br />";
                    strBody += "<b>User Name for Login:</b>" + HttpUtility.HtmlEncode(obj_ExternalUser.ExternalUserName) + "<br />";
                    strBody += "</p><br /><br /><br /><p>This is system genrated mail. Please do not reply.</p></body></html>";
                    bool boolResult = false;
                    boolResult = EmailUtility.SendMail(out strEmailServerName, obj_ExternalUser.ExternalUserEmailID + "," + obj_ExternalUser.ExternalUserAlternateEmailID, StrBcc: "chaurasia@nic.in", StrSubject: strSubject, StrBody: strBody);
                    if (!boolResult)
                    {
                        //SaveEmailAlert();

                    }
                    else
                    {
                        //SaveEmailAlert();


                    }

                }

            }
        }
        catch (Exception ex)
        {
            ActionTrail obj_ExtActionTrail = new ActionTrail();
            obj_ExtActionTrail.UserCode = UserCode;
            obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
            obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
            obj_ExtActionTrail.Status = "Email Alert Not Send";
            if (obj_ExtActionTrail != null)
                ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);

        }
    }
    //void SendSMS(long UserCode)
    //{
    //    try
    //    {
    //        if (SMSAlertUtility.IsSendSMSAlertEnable())
    //        {
    //            NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(UserCode);
    //            if (obj_ExternalUser.ExternalUserCode > 0)
    //            {
    //                strMessage = "Your registration is successful.:<br />" + "User Name for Login:" + HttpUtility.HtmlEncode(obj_ExternalUser.ExternalUserName)  + ".-CGWA";
    //                bool boolResult = false;

    //                strMobileNumberTo = obj_ExternalUser.ExternalUserMobileNumber;
    //                if (SMSAlertUtility.sendAlertToMobile(strMessage, obj_ExternalUser.ExternalUserMobileNumber, "1007161718628036017", out strSMSUserName).Trim() == "Platform accepted")
    //                {
    //                    boolResult = true;

    //                }
    //                if (!boolResult)
    //                {
    //                    if (string.IsNullOrEmpty(lblSMSNotSend.Text.Trim()))
    //                    {
    //                        lblSMSNotSend.Text = HttpUtility.HtmlEncode(strMobileNumberTo.Trim());
    //                    }
    //                    else
    //                    {
    //                        lblSMSNotSend.Text = HttpUtility.HtmlEncode(lblSMSNotSend.Text.Trim() + "," + strMobileNumberTo.Trim());  // add html encode

    //                    }

    //                    lblSentSMSExternalUserStatus.Text = "No SMS was send to External User Mobile";

    //                }
    //                else
    //                {
    //                    SaveSMSAlert();
    //                    lblSentSMSExternalUserStatus.Text = "";

    //                }




    //            }
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        ActionTrail obj_ExtActionTrail = new ActionTrail();
    //        obj_ExtActionTrail.UserCode = UserCode;
    //        obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
    //        obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
    //        obj_ExtActionTrail.Status = "SMS Alert Not Send";
    //        if (obj_ExtActionTrail != null)
    //            ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
    //    }
    //}

    private void FillDropDownState()
    {
        NOCAP.BLL.Master.State obj_state = new NOCAP.BLL.Master.State();
        NOCAP.BLL.Master.State[] arr;
        List<NOCAP.BLL.Master.State> listState = new List<NOCAP.BLL.Master.State>();
        try
        {
            ddlState.Items.Clear();

            if (obj_state.GetAll(NOCAP.BLL.Master.State.SortingField.StateName) == 1)
            {
                arr = obj_state.StateCollection;

                if (arr.Count() > 0)
                {
                    ddlState.DataSource = arr;
                    ddlState.DataTextField = "StateName";
                    ddlState.DataValueField = "StateCode";
                    ddlState.DataBind();
                }
            }
            else
            {
                Response.Write(obj_state.CustumMessage);
            }

        }
        catch
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlState);
    }
    private void FillDropDownDistrict(int intStateCode)
    {
        NOCAP.BLL.Master.State obj_state = new NOCAP.BLL.Master.State(intStateCode);

        try
        {
            ddlDistrict.Items.Clear();

            ddlDistrict.DataSource = obj_state.GetDistrictList(NOCAP.BLL.Master.State.SortingFieldForDistrict.DistrictName);
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictCode";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, "--Select--");


        }
        catch
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void FillDropDownSubDistrict(int intStateCode, int intDistrictCode)
    {
        NOCAP.BLL.Master.District obj_district = new NOCAP.BLL.Master.District(intStateCode, intDistrictCode);

        try
        {

            ddlSubDistrict.DataSource = obj_district.GetSubDistrictList(NOCAP.BLL.Master.District.SortingFieldForSubDistrict.SubDistrictName);
            ddlSubDistrict.DataTextField = "SubDistrictName";
            ddlSubDistrict.DataValueField = "SubDistrictCode";
            ddlSubDistrict.DataBind();
            ddlSubDistrict.Items.Insert(0, "--Select--");
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
    }
    private void FillDropDownIDProofType()
    {
        NOCAP.BLL.Master.IDProofType obj_iDProofType = new NOCAP.BLL.Master.IDProofType();
        NOCAP.BLL.Master.IDProofType[] arr;
        List<NOCAP.BLL.Master.IDProofType> listIDProofType = new List<NOCAP.BLL.Master.IDProofType>();

        try
        {
            ddlIDProofType.Items.Clear();
            if (obj_iDProofType.GetALL(NOCAP.BLL.Master.IDProofType.SortingField.IDProofTypeDesc) == 1)
            {
                arr = obj_iDProofType.IDProofTypeCollection;

                if (arr.Count() > 0)
                {

                    ddlIDProofType.DataSource = arr;
                    ddlIDProofType.DataTextField = "IDProofTypeDesc";
                    ddlIDProofType.DataValueField = "IDProofTypeCode";
                    ddlIDProofType.DataBind();
                }

            }
            else
            {
                Response.Write(obj_iDProofType.CustumMessage);
            }
        }

        catch
        {

            Response.Write(obj_iDProofType.CustumMessage);
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlIDProofType);
    }
    private void FillDropDownGender()
    {
        NOCAP.BLL.Master.Gender obj_gender = new NOCAP.BLL.Master.Gender();
        NOCAP.BLL.Master.Gender[] arr;
        List<NOCAP.BLL.Master.Gender> listGender = new List<NOCAP.BLL.Master.Gender>();

        try
        {
            ddlGender.Items.Clear();
            if (obj_gender.GetALL(NOCAP.BLL.Master.Gender.SortingField.GenderDesc) == 1)
            {
                arr = obj_gender.GenderCollection;

                if (arr.Count() > 0)
                {

                    ddlGender.DataSource = arr;
                    ddlGender.DataTextField = "GenderDesc";
                    ddlGender.DataValueField = "GenderCode";
                    ddlGender.DataBind();
                }

            }
            else
            {
                Response.Write(obj_gender.CustumMessage);
            }
        }

        catch
        {
            Response.Write(obj_gender.CustumMessage);
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlGender);
    }
    private void ClearScreen()
    {

        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtUserName.Text = "";
        txtEmail.Text = "";
        txtConfirmEmail.Text = "";
        txtAlternateEmail.Text = "";
        txtStdCode.Text = "";
        txtCountryCode.Text = "";
        txtPhoneNumber.Text = "";
        txtMobileNumber.Text = "";
        txtAddressLine1.Text = "";
        txtAddressLine2.Text = "";
        txtAddressLine3.Text = "";
        txtPinCode.Text = "";
        txtDOB.Text = "";
        txtIDProofUniqueNo.Text = "";
        txtUID.Text = "";
        txtCaptcha.Text = "";
        txtPassword.Text = "";
        txtReEnterPassword.Text = "";
        MultiView1.ActiveViewIndex = 0;
        ddlDistrict.SelectedIndex = -1;
        ddlGender.SelectedIndex = -1;
        ddlState.SelectedIndex = -1;
        ddlSubDistrict.SelectedIndex = -1;
        ddlTitle.SelectedIndex = -1;
        ddlIDProofType.SelectedIndex = -1;

    }
    private void SaveSMSAlert()
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
            obj_insertSMSAlert.SMSType = NOCAP.BLL.SMSAlert.SMSAlert.SMSTypeAFR.Registration;
            obj_insertSMSAlert.NOCAPApplicationType = NOCAP.BLL.SMSAlert.SMSAlert.NOCAPApplicationTypeEI.NOCAPExternal;
            strMessage = "OTP Send Successfully for Registration.";
            obj_insertSMSAlert.AppTypeCode = int_AppTypeCode;
            obj_insertSMSAlert.AppPurposeCode = int_AppPurposeCode;
            obj_insertSMSAlert.AlertStagesCode = int_AlertStagesCode;
            obj_insertSMSAlert.UserCode = int_UserCode;
            obj_insertSMSAlert.ExUserCode = lng_ExuserCode;
            obj_insertSMSAlert.SMSUseName = strSMSUserName;
            strMobileNumberTo = txtMobileNumber.Text.Trim();

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
    #endregion

    #region Link Button
    protected void lnkbtnRefresh_Click(object sender, EventArgs e)
    {
        txtCaptcha.Text = "";
    }
    protected void lbtnCheckUserNameAvail_Click(object sender, EventArgs e)
    {

        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser();
        lblCheckUserNameAvailMsg.Text = "";
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
                if (txtUserName.Text.Trim() == "")
                {
                    lblCheckUserNameAvailMsg.ForeColor = System.Drawing.Color.Red;
                    lblCheckUserNameAvailMsg.Text = "Please enter user name";
                    return;
                }

                if (obj_externalUser.IsExternalUserNameExist(txtUserName.Text.Trim()) == 1)
                {
                    lblCheckUserNameAvailMsg.ForeColor = System.Drawing.Color.Red;
                    lblCheckUserNameAvailMsg.Text = "Not Available";
                    return;
                }
                else
                {
                    lblCheckUserNameAvailMsg.ForeColor = System.Drawing.Color.Green;
                    lblCheckUserNameAvailMsg.Text = "Available";
                    return;
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    #endregion


    #region ddl SelectedIndexChanged
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intStateCode;
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
                intStateCode = Convert.ToInt32(ddlState.SelectedValue);

                ddlDistrict.Items.Clear();
                if (ddlState.SelectedValue == "")
                {
                    ListItem ps = new ListItem();

                    ps.Value = "";
                    ps.Text = "<Please Select>";
                    ps.Selected = true;
                    ddlDistrict.Items.Insert(0, ps);
                }
                else
                {
                    FillDropDownDistrict(intStateCode);
                }



            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intStateCode;
        int intDistrictCode;
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
                intStateCode = Convert.ToInt32(ddlState.SelectedValue);
                intDistrictCode = Convert.ToInt32(ddlDistrict.SelectedValue);

                ddlSubDistrict.Items.Clear();
                if (ddlState.SelectedValue == "" && ddlDistrict.SelectedValue == "")
                {
                    ListItem ps = new ListItem();

                    ps.Value = "";
                    ps.Text = "<Please Select>";
                    ps.Selected = true;
                    ddlSubDistrict.Items.Insert(0, ps);
                }
                else
                {
                    FillDropDownSubDistrict(intStateCode, intDistrictCode);
                }



            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    #endregion

    #region Button Click
    protected void btnReset_Click(object sender, EventArgs e)
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
                ClearScreen();
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                NOCAP.BLL.UserManagement.ExternalUser externaluser = new NOCAP.BLL.UserManagement.ExternalUser();
                try
                {
                    strActionName = "Submit";
                    FileUpload FileUploadIDAttachment = (FileUpload)Session["FileUpload1"];
                    if (!FileUploadIDAttachment.HasFile)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Please Attach ID Proof');", true);
                        return;
                    }
                    if (ddlIDProofType.SelectedItem.Text == "Voter ID")
                    {
                        Regex obj = new Regex("^[A-Za-z]{3}[0-9]{7}$");
                        Match VoterID = obj.Match(txtIDProofUniqueNo.Text);
                        if (VoterID.Success)
                        {

                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('Invalid VoterId Number !!');", true);
                            return;
                        }
                    }

                    if (ddlIDProofType.SelectedItem.Text == "PAN Card")
                    {
                        Regex obj = new Regex("^[A-Za-z]{5}[0-9]{4}[A-Za-z]{1}$");
                        Match PanCard = obj.Match(txtIDProofUniqueNo.Text);
                        if (PanCard.Success) { }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('Invalid Pan Card Number !!');", true);
                            return;
                        }
                    }
                    if (ddlIDProofType.SelectedItem.Text == "BPL Card")
                    {
                        Regex obj = new Regex(@"^[a-zA-Z0-9/-]{5,20}[^\\@<>_!]$");
                        Match BplCard = obj.Match(txtIDProofUniqueNo.Text);
                        if (BplCard.Success) { }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('Invalid BLP Card Number !!');", true);
                            return;
                        }
                    }
                    if (ddlIDProofType.SelectedItem.Text == "Udyam Registration Card")
                    {
                        //Regex obj = new Regex(@"^[a-zA-Z0-9/-]{5,20}[^\\@<>_!]$");
                        //Match BplCard = obj.Match(txtIDProofUniqueNo.Text);
                        //if (BplCard.Success) { }
                        //else
                        //{
                        //    ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('Invalid BLP Card Number !!');", true);
                        //    return;
                        //}
                    }
                    if (Session["Captcha"].ToString() != txtCaptcha.Text)
                    {
                        lblEnterVerificationCodeMessage.Text = "Invalid Captcha, Please try again";
                        txtCaptcha.Text = "";
                        return;
                    }
                    if (Session["OTP"].ToString() != txtOTP.Text)
                    {
                        lblEnterVerificationCodeMessage.Text = "Invalid OTP, Please try again";
                        txtCaptcha.Text = "";
                        return;
                    }
                    string str_fname;
                    string str_ext;
                    byte[] buffer = new byte[1];
                    if (FileUploadIDAttachment.HasFile)
                    {
                        str_ext = System.IO.Path.GetExtension(FileUploadIDAttachment.PostedFile.FileName).ToLower();
                        str_fname = FileUploadIDAttachment.FileName;
                        if (str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                        {
                            externaluser.IDProofAttName = FileUploadIDAttachment.FileName;
                            //externaluser.AttachmentFile = (NOCAPExternalUtility.StreamFile(FileUploadIDAttachment.PostedFile));
                            externaluser.AttachmentFile = (byte[])Session["FileContent"];
                            externaluser.ContentType = FileUploadIDAttachment.PostedFile.ContentType;
                            externaluser.FileExtension = str_ext;
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblUploadIDProofMessage.Text = "Not a valid file!!..Select an other file!!";
                            lblUploadIDProofMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                    }
                    else
                    {
                        lblUploadIDProofMessage.Text = "Please select a file..!!";
                        lblUploadIDProofMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    lblEnterVerificationCodeMessage.Text = "";
                    externaluser.ExternalUserTitleCode = Convert.ToInt32(ddlTitle.SelectedValue);
                    externaluser.ExternalUserFirstName = txtFirstName.Text;
                    externaluser.ExternalUserLastName = txtLastName.Text;
                    externaluser.ExternalUserName = txtUserName.Text;
                    string myHashhdn = hdnPassResultValue.Value.ToString();
                    string[] new_pass = Regex.Split(myHashhdn, Convert.ToString(Session["rno"]));
                    string myHash = new_pass[0].ToString();
                    externaluser.PwdSalt = Convert.ToInt64(Session["rno"]);

                    if (myHash == hdnConfResultValue.Value)
                    {
                        if (myHash != "" || myHash != null)
                        {
                            externaluser.PwdHash = myHash;
                        }
                        else
                        {
                            lblEnterVerificationCodeMessage.Text = "Please enter Valid Password!";
                            return;
                        }
                    }
                    else
                    {
                        lblEnterVerificationCodeMessage.Text = "Password and Confirm Password Should be Same!";
                        return;
                    }

                    externaluser.ExternalUserEmailID = txtEmail.Text;
                    externaluser.ExternalUserAlternateEmailID = txtAlternateEmail.Text;
                    externaluser.ExternalUserPhoneNumberISD = txtPhoneNumber.Text != "" ? txtCountryCode.Text : "";
                    externaluser.ExternalUserPhoneNumberSTD = txtStdCode.Text;
                    externaluser.ExternalUserPhoneNumber = txtPhoneNumber.Text;
                    externaluser.ExternalUserMobileNumberISD = txtMobileNumber.Text != "" ? txtMobileCountryCode.Text : "";
                    externaluser.ExternalUserMobileNumber = txtMobileNumber.Text;
                    externaluser.ExternalUserActive = NOCAP.BLL.UserManagement.ExternalUser.ExternalUserActiveYesNo.Yes;
                    externaluser.Remark = "";
                    externaluser.AddressLine1 = txtAddressLine1.Text;
                    externaluser.AddressLine2 = txtAddressLine2.Text;
                    externaluser.AddressLine3 = txtAddressLine3.Text;
                    externaluser.StateCode = Convert.ToInt32(ddlState.SelectedValue);
                    externaluser.DistrictCode = Convert.ToInt32(ddlDistrict.SelectedValue);
                    if (ddlSubDistrict.SelectedValue != "--Select--")
                    {
                        externaluser.SubDistrictCode = Convert.ToInt32(ddlSubDistrict.SelectedValue);
                    }
                    else
                    {
                        externaluser.SubDistrictCode = null;
                    }
                    externaluser.PinCode = Convert.ToInt32(txtPinCode.Text);

                    externaluser.DateOfBirth = Convert.ToDateTime(txtDOB.Text);
                    externaluser.GenderCode = Convert.ToInt32(ddlGender.SelectedValue);
                    externaluser.UID = txtUID.Text;
                    externaluser.IDProofTypeCode = Convert.ToInt32(ddlIDProofType.SelectedValue);
                    externaluser.IDProofUniqueNo = txtIDProofUniqueNo.Text;

                    //externaluser.ActionPerformByUserCode = 1;
                    if (externaluser.Register() == 1)
                    {

                        SendMail(externaluser.ExternalUserCode);
                        //SendSMS(externaluser.ExternalUserCode);
                        strStatus = "Record Submit Successfully !";
                        lblEnterVerificationCodeMessage.Text = externaluser.CustumMessage;
                        Session.Remove("FileUpload1");
                        Session.Remove("FileContent");

                        //ClearScreen();
                        ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('You have been Registered Successfully. Please login to Proceed Further.');window.location='ApplicantRegistration.aspx'", true);
                    }
                    else
                    {
                        strStatus = "Record Submit Failed !";
                        lblEnterVerificationCodeMessage.Text = externaluser.CustumMessage;
                    }
                }

                catch (Exception)
                {
                    strStatus = "Record Submit Failed !";
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
                }
                finally
                {
                    NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(externaluser.ExternalUserName);
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



    protected void btnProceed_Click1(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                NOCAP.BLL.UserManagement.ExternalUser externaluser = new NOCAP.BLL.UserManagement.ExternalUser();
                try
                {
                    strActionName = "Proceed";
                    if (!FileUploadIDProof.HasFile)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Please Attach ID Proof');", true);
                        return;
                    }
                    else
                    {
                        string str_fname;
                        string str_ext;
                        if (FileUploadIDProof.HasFile)
                        {
                            str_ext = System.IO.Path.GetExtension(FileUploadIDProof.PostedFile.FileName).ToLower();
                            str_fname = FileUploadIDProof.FileName;
                            if (str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                            {
                                if (NOCAPExternalUtility.IsValidFile(FileUploadIDProof.PostedFile))
                                {
                                    if (FileUploadIDProof.PostedFile.ContentLength <= NOCAPExternalUtility.AttachmentSizeLimitofID())
                                    {
                                        Session["FileContent"] = (NOCAPExternalUtility.StreamFile(FileUploadIDProof.PostedFile));
                                    }
                                    else
                                    {
                                        strStatus = "File Upload Failed";
                                        lblUploadIDProofMessage.Text = "File can not upload. It has more than 300KB size";
                                        lblUploadIDProofMessage.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                                else
                                {
                                    strStatus = "File Upload Failed";
                                    lblUploadIDProofMessage.Text = "Not a valid file!!..Select an other file!!";
                                    lblUploadIDProofMessage.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblUploadIDProofMessage.Text = "Not a valid file!!..Select an other file!!";
                                lblUploadIDProofMessage.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            lblUploadIDProofMessage.Text = "Please select a file..!!";
                            lblUploadIDProofMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                    }
                    if (ddlIDProofType.SelectedItem.Text == "Voter ID")
                    {
                        Regex obj = new Regex("^[A-Za-z]{3}[0-9]{7}$");
                        Match VoterID = obj.Match(txtIDProofUniqueNo.Text);
                        if (VoterID.Success)
                        {

                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('Invalid VoterId Number !!');", true);
                            return;
                        }
                    }

                    if (ddlIDProofType.SelectedItem.Text == "PAN Card")
                    {
                        Regex obj = new Regex("^[A-Za-z]{5}[0-9]{4}[A-Za-z]{1}$");
                        Match PanCard = obj.Match(txtIDProofUniqueNo.Text);
                        if (PanCard.Success) { }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('Invalid Pan Card Number !!');", true);
                            return;
                        }
                    }
                    if (ddlIDProofType.SelectedItem.Text == "BPL Card")
                    {
                        Regex obj = new Regex(@"^[a-zA-Z0-9/-]{5,20}[^\\@<>_!]$");
                        Match BplCard = obj.Match(txtIDProofUniqueNo.Text);
                        if (BplCard.Success) { }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('Invalid BLP Card Number !!');", true);
                            return;
                        }
                    }


                    if (SMSUtility.IsSendSMSEnable())
                    {

                        string OTPMessage = "";
                        OTPMessage = NOCAPExternalUtility.GetRandomNumber();
                        // OTPMessage = "123456";
                        Session["OTP"] = OTPMessage;

                        if (!NOCAPExternalUtility.IsNumeric(txtMobileNumber.Text))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('Invalid Mobile Number !!');", true);
                            return;
                        }

                        OTPMessage = "One Time Password (OTP) for username:" + txtUserName.Text + " is :" + OTPMessage + "-CGWA";
                        string msgRes = SMSUtility.sendOTPtoMobile(OTPMessage, txtMobileNumber.Text, "1007161718789988150", out strSMSUserName);
                        // msgRes = "Platform accepted";
                        if (msgRes.Trim() == "Platform accepted")
                        {
                            MultiView1.ActiveViewIndex = 1;
                            SaveSMSAlert();
                            ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('One time Password(OTP) has been Sent to your Mobile No, Enter OTP to Complete Your Registration');", true);

                            lblddlTitle.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.Title(Convert.ToInt32(ddlTitle.SelectedValue)).TitleDesc);
                            lbltxtFirstName.Text = HttpUtility.HtmlEncode(txtFirstName.Text);
                            lbltxtLastName.Text = HttpUtility.HtmlEncode(txtLastName.Text);
                            lbltxtUserName.Text = HttpUtility.HtmlEncode(txtUserName.Text);
                            lbltxtEmail.Text = HttpUtility.HtmlEncode(txtEmail.Text);
                            lbltxtAlternateEmail.Text = HttpUtility.HtmlEncode(txtAlternateEmail.Text);
                            if (txtPhoneNumber.Text != "") { lbltxtPhoneNumber.Text = HttpUtility.HtmlEncode("+" + txtCountryCode.Text + "-" + txtStdCode.Text + "-" + txtPhoneNumber.Text); }
                            if (txtMobileNumber.Text != "") { lbltxtMobileNumber.Text = HttpUtility.HtmlEncode("+" + txtMobileCountryCode.Text + "-" + txtMobileNumber.Text); }
                            lbltxtAddressLine1.Text = HttpUtility.HtmlEncode(txtAddressLine1.Text);
                            lbltxtAddressLine2.Text = HttpUtility.HtmlEncode(txtAddressLine2.Text);
                            lbltxtAddressLine3.Text = HttpUtility.HtmlEncode(txtAddressLine3.Text);
                            lblddlState.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.State(Convert.ToInt32(ddlState.SelectedValue)).StateName);
                            lblddlDistrict.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.District(Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue)).DistrictName);
                            if (ddlSubDistrict.SelectedValue != "--Select--") { lblddlSubDistrict.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.SubDistrict(Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlSubDistrict.SelectedValue)).SubDistrictName); }
                            lbltxtPinCode.Text = HttpUtility.HtmlEncode(txtPinCode.Text);
                            lbltxtDOB.Text = HttpUtility.HtmlEncode(txtDOB.Text);
                            lblddlGender.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.Gender(Convert.ToInt32(ddlGender.SelectedValue)).GenderDesc);
                            lbltxtUID.Text = HttpUtility.HtmlEncode(txtUID.Text);
                            lblddlIDProofType.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.IDProofType(Convert.ToInt32(ddlIDProofType.SelectedValue)).IDProofTypeDesc);
                            lbltxtIDProofUniqueNo.Text = HttpUtility.HtmlEncode(txtIDProofUniqueNo.Text);
                            lblFileUploadIDProof.Text = HttpUtility.HtmlEncode(FileUploadIDProof.PostedFile.FileName);
                            Session["FileUpload1"] = FileUploadIDProof;
                            //txtOTP.Text = Convert.ToString(Session["OTP"]);
                        }
                        else { ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Error in sending OTP');", true); }
                    }

                }
                catch (Exception)
                {
                    strStatus = "Record Submit Failed !";
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
                }
                finally
                {
                    NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(externaluser.ExternalUserName);
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
    #endregion
    protected void imgBtnCaptchaRefresh_Click(object sender, ImageClickEventArgs e)
    {
        txtCaptcha.Text = "";
    }
}