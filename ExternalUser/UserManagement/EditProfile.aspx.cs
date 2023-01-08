using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Text.RegularExpressions;

public partial class ExternalUser_UserManagement_EditProfile : System.Web.UI.Page
{
    string strPageName = "EditProfile";
    string strActionName = "";
    string strStatus = "";
    string strSubject = "";
    string strBody = "";
    string strToEmailId = "";
    string strEmailServerName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ValidationExpInit();
            try
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;

                int externalusercode = 0;
                int intTitle;
                int intGender;
                int intState;
                int intDistrict;
                int intSubDistrict;
                int intIdProofType;
                externalusercode = Convert.ToInt32(Session["ExternalUserCode"]);
                if (externalusercode > 0)
                {

                    BindGrid(externalusercode);

                    NOCAP.BLL.UserManagement.ExternalUser exu = new NOCAP.BLL.UserManagement.ExternalUser(externalusercode);
                    txtFirstName.Text = HttpUtility.HtmlEncode(exu.ExternalUserFirstName.ToString());
                    txtLastName.Text = HttpUtility.HtmlEncode(exu.ExternalUserLastName.ToString());
                    txtUserName.Text = HttpUtility.HtmlEncode(exu.ExternalUserName.ToString());
                    txtEmail.Text = HttpUtility.HtmlEncode(exu.ExternalUserEmailID.ToString());
                    txtAlternateEmail.Text = HttpUtility.HtmlEncode(exu.ExternalUserAlternateEmailID.ToString());
                    txtStdCode.Text = HttpUtility.HtmlEncode(exu.ExternalUserPhoneNumberSTD.ToString());
                    txtPhoneNumber.Text = HttpUtility.HtmlEncode(exu.ExternalUserPhoneNumber.ToString());
                    txtMobileNumber.Text = HttpUtility.HtmlEncode(exu.ExternalUserMobileNumber.ToString());
                    txtAddressLine1.Text = HttpUtility.HtmlEncode(exu.AddressLine1.ToString());
                    txtAddressLine2.Text = HttpUtility.HtmlEncode(exu.AddressLine2.ToString());
                    txtAddressLine3.Text = HttpUtility.HtmlEncode(exu.AddressLine3.ToString());
                    txtPinCode.Text = HttpUtility.HtmlEncode(exu.PinCode.ToString());
                    txtDOB.Text = HttpUtility.HtmlEncode(exu.DateOfBirth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                    txtUID.Text = HttpUtility.HtmlEncode(exu.UID.ToString());
                    txtIDProofUniqueNo.Text = HttpUtility.HtmlEncode(exu.IDProofUniqueNo.ToString());
                    intTitle = Convert.ToInt32(HttpUtility.HtmlEncode(exu.ExternalUserTitleCode));
                    intGender = Convert.ToInt32(HttpUtility.HtmlEncode(exu.GenderCode));
                    intState = Convert.ToInt32(HttpUtility.HtmlEncode(exu.StateCode));
                    intDistrict = Convert.ToInt32(HttpUtility.HtmlEncode(exu.DistrictCode));
                    intSubDistrict = Convert.ToInt32(HttpUtility.HtmlEncode(exu.SubDistrictCode));
                    intIdProofType = Convert.ToInt32(HttpUtility.HtmlEncode(exu.IDProofTypeCode));

                    FillDropDownState(intState);
                    FillDropDownDistrict(intDistrict, intState);
                    FillDropDownSubDistrict(intState, intDistrict, intSubDistrict);
                    FillDropDownTitle(intTitle);
                    FillDropDownGender(intGender);
                    FillDropDownIdProofType(intIdProofType);
                    txtUserName.Enabled = false;
                }
                else
                {

                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    #region Private Methods
    private void SendMail(long UserCode)
    {
        try
        {
            if (EmailUtility.IsSendEmailEnable())
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(UserCode);
                if (obj_ExternalUser.ExternalUserCode > 0)
                {

                    strSubject = "NOCAP- User Profile Update";
                    strBody = "<html><body><p>Dear " + " ";
                    strBody += HttpUtility.HtmlEncode(obj_ExternalUser.ExternalUserFirstName).ToUpper() + " " + HttpUtility.HtmlEncode(obj_ExternalUser.ExternalUserLastName).ToUpper();
                    strBody += " ,</p><p style='margin-left:50px;'>Profile updated successfully for User Name:" + HttpUtility.HtmlEncode(obj_ExternalUser.ExternalUserName).ToUpper() + "<br />";
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
            NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlTitle);
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
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
            NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlState);
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
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
        catch (Exception)
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
            Response.Redirect("~/ExternalErrorPage.aspx", false);
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
            NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlIDProofType);
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
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
            NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlGender);
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private int IDproofDownloadFiles(long lng_UserCode)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.UserManagement.ExternalUser objExternalUser = new NOCAP.BLL.UserManagement.ExternalUser();
            objExternalUser = objExternalUser.DownloadIDProofFile(lng_UserCode);
            if (objExternalUser != null)
            {
                byte[] bytes = objExternalUser.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = HttpUtility.HtmlEncode(objExternalUser.ContentType);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "ExternalUser_" + Convert.ToString(lng_UserCode) + "_" + objExternalUser.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    private void ValidationExpInit()
    {
        revtxtMobileNumber.ValidationExpression = ValidationUtility.txtValForMobileNumber;
        revtxtMobileNumber.ErrorMessage = ValidationUtility.txtValForMobileNumberMsg;

        revtxtEmail.ValidationExpression = ValidationUtility.txtValForEmail;
        revtxtEmail.ErrorMessage = ValidationUtility.txtValForEmailMsg;
    }
    private void BindGrid(int intExUserCode)
    {
        try
        {

            NOCAP.BLL.UserManagement.ExternalUser exu = new NOCAP.BLL.UserManagement.ExternalUser(intExUserCode);
            List<NOCAP.BLL.UserManagement.ExternalUser> externaluserList = new List<NOCAP.BLL.UserManagement.ExternalUser>();
            if (exu.IDProofAttName != null && exu.IDProofAttName != "") { externaluserList.Add(exu); }
            else { externaluserList = null; }

            if (externaluserList != null)
            {
                gvIDProof.DataSource = externaluserList;
                gvIDProof.DataBind();
            }
            else
            {
                gvIDProof.DataSource = null;
                gvIDProof.DataBind();
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    private void FillDropDownTitle(int intTitle)
    {
        NOCAPExternalUtility.FillDropDownTitle(ref ddlTitle);
        ddlTitle.SelectedValue = Convert.ToString(intTitle);
    }
    private void FillDropDownGender(int intGender)
    {
        NOCAPExternalUtility.FillDropDownGender(ref ddlGender);
        ddlGender.SelectedValue = Convert.ToString(intGender);
    }

    private void FillDropDownIdProofType(int intIdProofType)
    {
        NOCAPExternalUtility.FillDropDownIdProofType(ref ddlIDProofType);
        ddlIDProofType.SelectedValue = Convert.ToString(intIdProofType);

    }
    private void FillDropDownState(int intState)
    {
        NOCAPExternalUtility.FillDropDownState(ref ddlState);
        ddlState.SelectedValue = Convert.ToString(intState);
    }
    private void FillDropDownDistrict(int intDistrict, int intState)
    {
        int intDistrictCode = intDistrict;
        try
        {
            if (NOCAPExternalUtility.FillDropDownDistrict(ref ddlDistrict, intState) != 1)
            {
                Response.Write("Problem in district population");
            }
            ddlDistrict.SelectedValue = Convert.ToString(intDistrictCode);
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void FillDropDownSubDistrict(int intState, int intDistrict, int intSubDistrict)
    {
        int intSubDistrictCode = intSubDistrict;
        try
        {
            if (NOCAPExternalUtility.FillDropDownSubDistrict(ref ddlSubDistrict, intDistrict, intState) != 1)
            {
                Response.Write("Problem in Sub-district population");
            }
            ddlSubDistrict.SelectedValue = Convert.ToString(intSubDistrictCode);
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    #endregion
    protected void lnkbtnRefresh_Click(object sender, EventArgs e)
    {

    }
    protected void lbtnCheckUserNameAvail_Click(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;

            strActionName = "Submit";
            int externalUserCode = Convert.ToInt32(Session["ExternalUserCode"]);
            try
            {
                NOCAP.BLL.UserManagement.ExternalUser externaluser = new NOCAP.BLL.UserManagement.ExternalUser(externalUserCode);
                List<NOCAP.BLL.UserManagement.ExternalUser> externaluserList = new List<NOCAP.BLL.UserManagement.ExternalUser>();
                if (externaluser.IDProofAttName != null && externaluser.IDProofAttName != "") { externaluserList.Add(externaluser); }
                else { externaluserList = null; }
                string str_fname;
                string str_ext;
                byte[] buffer = new byte[1];
                if (FileUploadIDAttachment.HasFile)
                {
                    str_ext = System.IO.Path.GetExtension(FileUploadIDAttachment.PostedFile.FileName).ToLower();
                    str_fname = FileUploadIDAttachment.FileName;
                    if (str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                    {
                        if (NOCAPExternalUtility.IsValidFile(FileUploadIDAttachment.PostedFile))
                        {
                            if (FileUploadIDAttachment.PostedFile.ContentLength <= NOCAPExternalUtility.AttachmentSizeLimitofID())
                            {
                                externaluser.IDProofAttName = FileUploadIDAttachment.FileName;
                                externaluser.AttachmentFile = (NOCAPExternalUtility.StreamFile(FileUploadIDAttachment.PostedFile));
                                externaluser.ContentType = FileUploadIDAttachment.PostedFile.ContentType;
                                externaluser.FileExtension = str_ext;
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessage.Text = "File can not upload. It has more than 300KB size";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessage.Text = "Not a valid file!!..Select an other file!!";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        lblMessage.Text = "Not a valid file!!..Select an other file!!";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
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
                externaluser.ExternalUserTitleCode = Convert.ToInt32(ddlTitle.SelectedValue);
                externaluser.ExternalUserFirstName = txtFirstName.Text;
                externaluser.ExternalUserLastName = txtLastName.Text;
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
                if (ddlSubDistrict.SelectedIndex != 0)
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
                externaluser.ModifiedByExUC = externalUserCode;
                externaluser.ModifiedByUC = null;
                if (externaluser.Update() == 1)
                {
                    SendMail(externaluser.ExternalUserCode);
                    gvIDProof.DataSource = externaluserList;
                    gvIDProof.DataBind();
                    strStatus = "Record Submit Successfully !";
                    Response.Write("<script>javascript:alert('" + externaluser.CustumMessage + "')</script>");
                    lblMessage.ForeColor = System.Drawing.Color.Blue;
                    lblMessage.Text = externaluser.CustumMessage;

                    if (externaluser.AttachmentFile != null)
                    {
                        BindGrid(externalUserCode);
                    }
                }
                else
                {
                    strStatus = "Record Submit Failed !";
                    Response.Write("<script>javascript:alert(" + "'" + externaluser.CustumMessage + "'" + ")</script>");
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = externaluser.CustumMessage;
                }
            }
            catch (Exception)
            {
                strStatus = "Record Submit Failed !";
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

    protected void btnReset_Click(object sender, EventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;

            Response.Redirect("../ApplicantHome.aspx", false);
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            int intStateCode;
            int intDistrictCode;
            try
            {
                intStateCode = Convert.ToInt32(ddlState.SelectedValue);
                if (ddlDistrict.SelectedValue != "")
                {
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
                else
                {
                    ddlSubDistrict.Items.Clear();
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            int intStateCode;
            try
            {
                if (ddlState.SelectedValue != "")
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
                else
                {
                    ddlDistrict.Items.Clear();
                    ddlSubDistrict.Items.Clear();
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void gvIDProof_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblAttachmentType = (Label)e.Row.FindControl("lblAttachmentTypeCode");
            if (lblAttachmentType != null)
            {
                lblAttachmentType.Text = HttpUtility.HtmlEncode((new NOCAP.BLL.Master.IDProofType(Convert.ToInt32(lblAttachmentType.Text)).IDProofTypeDesc));
            }
        }
    }

    protected void gvIDProof_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {
                if (e.CommandName == "View")
                {
                    int int_UserCode = Convert.ToInt32(e.CommandArgument);
                    IDproofDownloadFiles(Convert.ToInt64(e.CommandArgument));
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
            }
        }
    }
}