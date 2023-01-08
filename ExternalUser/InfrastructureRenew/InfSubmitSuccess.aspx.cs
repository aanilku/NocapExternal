﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Threading;
using NOCAP;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_InfrastructureRenew_InfSubmitSuccess : System.Web.UI.Page
{
    string strPageName = "InfRenewSendSMSEmail";
    string strActionName = "Submit";
    string strStatus = "";

    long? lng_AppCode = null;
    int? int_AppTypeCode = null;
    int? int_AppPurposeCode = null;
    int? int_UserCode = null;
    int? int_AlertStagesCode = null;
    long? lng_ExuserCode = null;
    int? int_CreatedByUC = null;

    long? lng_CreatedByExUC = null;
    string strToEmailId = "";
    string strSubject = "";
    string strBody = "";
    string strMessage = "";
    string strMobileNumberTo = "";
    string strSMSUserName = null;
    string strEmailServerName = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (PreviousPage != null)
            {
                long lngSubmittedApplicationCode = PreviousPage.InfSubmitAppCode;
                GetInfrastructureApplicationNumber(lngSubmittedApplicationCode);
                SendMail();
                 SendSMS();
                if (!string.IsNullOrEmpty(lblSMSNotSend.Text.Trim()))
                {
                    lblSMSNotSend.Visible = true;
                    lblSendMsg.Visible = true;
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }


    void SendMail()
    {
        try
        {
            if (EmailUtility.IsSendEmailEnable())
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                if (obj_ExternalUser.ExternalUserCode > 0)
                {
                    strSubject = "Your Renewal Application Submitted Successfully";
                    strBody = "<html><body><p>Dear " + " ";
                    strBody += HttpUtility.HtmlEncode(obj_ExternalUser.ExternalUserName).ToUpper();
                    strBody += " ,</p><p style='margin-left:50px;'>Your Application Submitted Successfully.Application Details are :<br />";
                    strBody += "<b>Application Number :</b>" + HttpUtility.HtmlEncode(Convert.ToString(ViewState["AppNo"])) + "<br />";
                    strBody += "<b>Applied for Renewal of NOC Number :</b>" + HttpUtility.HtmlEncode(Convert.ToString(ViewState["PreNOCNumber"])) + "<br />";
                    strBody += "<b>Applied For Renewal :</b>" + HttpUtility.HtmlEncode(Convert.ToString(ViewState["LinkDepth"])) + "<br />";
                    strBody += "<b>Name of Industry :</b>" + HttpUtility.HtmlEncode(Convert.ToString(ViewState["NameofIndustry"])) + "<br />";
                    strBody += "Please note application number for future reference.</p><br /><br /><br /><p>This is system generated mail. Please do not reply.</p></body></html>";


                    bool boolResult = false;
                    if (!string.IsNullOrEmpty(obj_ExternalUser.ExternalUserEmailID))
                    {
                        strToEmailId = obj_ExternalUser.ExternalUserEmailID;
                        boolResult = EmailUtility.SendMail(out strEmailServerName, obj_ExternalUser.ExternalUserEmailID, StrBcc: "chaurasia@nic.in", StrSubject: strSubject, StrBody: strBody);
                        if (!boolResult)
                        {
                            //SaveEmailAlert();
                            lblSentMailExternalUserStatus.Text = "No email was send to External User";

                        }
                        else
                        {
                            //SaveEmailAlert();
                            lblSentMailExternalUserStatus.Text = "";

                        }
                    }
                    else
                    {
                        lblSentMailExternalUserStatus.Text = "External User EmailID Not Found";
                    }
                    if (Convert.ToString(ViewState["CommunicationEmailID"]) != "")
                    {
                        strToEmailId = strToEmailId + "," + Convert.ToString(ViewState["CommunicationEmailID"]);
                        boolResult = EmailUtility.SendMail(out strEmailServerName, Convert.ToString(ViewState["CommunicationEmailID"]), StrSubject: strSubject, StrBody: strBody);
                        if (!boolResult)
                        {
                            //  SaveEmailAlert(); 
                            lblSentMailCommunicationalStatus.Text = "No email was send to Communicational EmailId";

                        }
                        else
                        {
                            SaveEmailAlert();
                            lblSentMailCommunicationalStatus.Text = "";

                        }
                    }
                    else
                    {
                        strToEmailId = obj_ExternalUser.ExternalUserEmailID;
                        SaveEmailAlert();
                        //boolResult = EmailUtility.SendMail(out strEmailServerName, Convert.ToString(ViewState["CommunicationEmailID"]), StrSubject: strSubject, StrBody: strBody);
                        //if (boolResult)
                        //{
                        //    SaveEmailAlert();
                        //}
                        lblSentMailCommunicationalStatus.Text = "Communicational EmailID Not Found";
                    }
                }
            }
        }
        catch (Exception)
        {
            ActionTrail obj_ExtActionTrail = new ActionTrail();
            if (Session["ExternalUserCode"] != null)
            {
                obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
                obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
                obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
                obj_ExtActionTrail.Status = "Email Alert Not Send";
                if (obj_ExtActionTrail != null)
                    ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
            }
        }
    }
    public void SaveEmailAlert()
    {
        try
        {

            NOCAP.BLL.EmailAlert.EmailAlert obj_insertEmailAlert = new NOCAP.BLL.EmailAlert.EmailAlert();

            obj_insertEmailAlert.AppCode = Convert.ToInt64(lng_AppCode);
            obj_insertEmailAlert.EmailType = NOCAP.BLL.EmailAlert.EmailAlert.EmailTypeAFR.Alert;
            obj_insertEmailAlert.NOCAPApplicationType = NOCAP.BLL.EmailAlert.EmailAlert.NOCAPApplicationTypeEI.NOCAPExternal;
            obj_insertEmailAlert.AppTypeCode = Convert.ToInt32(int_AppTypeCode);
            obj_insertEmailAlert.AppPurposeCode = Convert.ToInt32(int_AppPurposeCode);
            obj_insertEmailAlert.UserCode = Convert.ToInt32(int_UserCode);
            obj_insertEmailAlert.ExUserCode = Convert.ToInt64(lng_ExuserCode);
            obj_insertEmailAlert.AlertStagesCode = Convert.ToInt32(int_AlertStagesCode);
            obj_insertEmailAlert.EmailServerName = strEmailServerName;
            lng_CreatedByExUC = Convert.ToInt64(Session["ExternalUserCode"]);
            obj_insertEmailAlert.CreatedByExUserCode = lng_CreatedByExUC;
            obj_insertEmailAlert.CreatedByUserCode = int_CreatedByUC;

            if (!string.IsNullOrEmpty(strToEmailId))
            {
                obj_insertEmailAlert.ToEmailID = strToEmailId;
            }


            obj_insertEmailAlert.Subject = strSubject;
            obj_insertEmailAlert.Message = strBody;

            if (obj_insertEmailAlert.Add() == 1)
            {
                strStatus = "Email Alert Save Successfully.";

            }
            else
            {

                strStatus = "Email Alert Not Saved.";

            }
        }

        catch (Exception)
        {
            strStatus = "Email Alert Not Saved.";


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

    void SendSMS()
    {
        try
        {
            if (SMSAlertUtility.IsSendSMSAlertEnable())
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                if (obj_ExternalUser.ExternalUserCode > 0)
                {
                    //strMessage = "Your Application Submitted Successfully.Your Application Number is" + HttpUtility.HtmlEncode(Convert.ToString(ViewState["AppNo"])) + ".";
                    strMessage = "Your Renewal Application Submitted Successfully for NOC Number : " + HttpUtility.HtmlEncode(Convert.ToString(ViewState["PreNOCNumber"])) + ".CGWA";
                    bool boolResult = false;
                    if (!string.IsNullOrEmpty(obj_ExternalUser.ExternalUserMobileNumber))
                    {
                        strMobileNumberTo = obj_ExternalUser.ExternalUserMobileNumber;
                        if (SMSAlertUtility.sendAlertToMobile(strMessage, obj_ExternalUser.ExternalUserMobileNumber, "1007161718646181295", out  strSMSUserName).Trim() == "Platform accepted")
                        {
                            boolResult = true;
                            //SMSAlertUtility.sendAlertToMobile(strMessage, "9868101108", "1007161718646181295", out strSMSUserName).Trim();

                        }
                        if (!boolResult)
                        {
                            if (string.IsNullOrEmpty(lblSMSNotSend.Text.Trim()))
                            {
                                lblSMSNotSend.Text =HttpUtility.HtmlEncode(strMobileNumberTo.Trim());
                            }
                            else
                            {
                                lblSMSNotSend.Text = HttpUtility.HtmlEncode(lblSMSNotSend.Text.Trim() + "," + strMobileNumberTo.Trim());

                            }
                            //SaveSMSAlert();
                            lblSentSMSExternalUserStatus.Text = "No SMS was send to External User Mobile";

                        }
                        else
                        {
                            SaveSMSAlert();
                            lblSentSMSExternalUserStatus.Text = "";

                        }
                    }
                    else
                    {
                        lblSentSMSExternalUserStatus.Text = "External User Mobile Number Not Found";
                    }

                    if (ViewState["CommunicationMobileNumber"] != null)
                    {

                        if (ViewState["CommunicationMobileNumber"].ToString() != obj_ExternalUser.ExternalUserMobileNumber)
                        {
                            strMobileNumberTo = Convert.ToString(ViewState["CommunicationMobileNumber"]);
                            if (SMSAlertUtility.sendAlertToMobile(strMessage, Convert.ToString(ViewState["CommunicationMobileNumber"]), "1007161718646181295", out  strSMSUserName).Trim() == "Platform accepted")
                            {
                                boolResult = true;

                            }
                            if (!boolResult)
                            {
                                if (string.IsNullOrEmpty(lblSMSNotSend.Text.Trim()))
                                {
                                    lblSMSNotSend.Text =HttpUtility.HtmlEncode(strMobileNumberTo.Trim());
                                }
                                else
                                {
                                    lblSMSNotSend.Text = HttpUtility.HtmlEncode(lblSMSNotSend.Text.Trim() + "," + strMobileNumberTo.Trim());

                                }
                                //SaveSMSAlert(); 
                                lblSentSMSCommunicationalStatus.Text = "No SMS was send to Communication Mobile";

                            }
                            else
                            {
                                SaveSMSAlert();
                                lblSentSMSCommunicationalStatus.Text = "";

                            }

                        }
                        else
                        {
                            lblSentSMSCommunicationalStatus.Text = "";
                        }
                    }
                    else
                    {

                        lblSentSMSCommunicationalStatus.Text = "Communicational Mobile Number Not Found";
                    }

                }
            }
        }
        catch (Exception)
        {
            ActionTrail obj_ExtActionTrail = new ActionTrail();
            if (Session["ExternalUserCode"] != null)
            {
                obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
                obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
                obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
                obj_ExtActionTrail.Status = "SMS Alert Not Send";
                if (obj_ExtActionTrail != null)
                    ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
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
            obj_insertSMSAlert.SMSType = NOCAP.BLL.SMSAlert.SMSAlert.SMSTypeAFR.Alert;
            obj_insertSMSAlert.NOCAPApplicationType = NOCAP.BLL.SMSAlert.SMSAlert.NOCAPApplicationTypeEI.NOCAPExternal;

            obj_insertSMSAlert.AppTypeCode = int_AppTypeCode;
            obj_insertSMSAlert.AppPurposeCode = int_AppPurposeCode;
            obj_insertSMSAlert.AlertStagesCode = int_AlertStagesCode;
            if (int_UserCode < 0)
            {
                obj_insertSMSAlert.UserCode = int_UserCode;
            }
            else
            {
                obj_insertSMSAlert.UserCode = null;
            }
            obj_insertSMSAlert.ExUserCode = lng_ExuserCode;
            //strSMSUserName = "Renew Application";
            obj_insertSMSAlert.SMSUseName = strSMSUserName;

            if (strMobileNumberTo.Trim() != "")
            {
                obj_insertSMSAlert.MobileNo = strMobileNumberTo;
            }

            obj_insertSMSAlert.Message = strMessage.Trim();
            lng_CreatedByExUC = Convert.ToInt64(Session["ExternalUserCode"]);
            obj_insertSMSAlert.CreatedByExUserCode = lng_CreatedByExUC;
            obj_insertSMSAlert.CreatedByUserCode = int_CreatedByUC;


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



    public void GetInfrastructureApplicationNumber(long lngSubmittedApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplicationPrevious = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplicationPrevious = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication();

            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(lngSubmittedApplicationCode);
            obj_InfrastructureNewApplication = obj_InfrastructureRenewApplication.GetFirstInfrastructureApplication();


            //if (obj_InfrastructureRenewApplication != null && obj_InfrastructureRenewApplication.PayReq == NOCAP.BLL.Common.PaymentRequirementOption.PayReqOption.Yes)
            //{
            //    //lblFee.Text = HttpUtility.HtmlEncode("Applicant has to Submit Processing Fee of Rs " + obj_InfrastructureRenewApplication.PayReqAmt + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(obj_InfrastructureRenewApplication.PayReqAmt))) + ") in the form of Demand Draft drawn in Favour of PAO, CGWB and Payable at Faridabad, Haryana.)");
            //    lblFee.Text = HttpUtility.HtmlEncode("Applicant has to Submit Processing Fee of Rs " + obj_InfrastructureRenewApplication.PayReqAmt + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(obj_InfrastructureRenewApplication.PayReqAmt))) + ") through NON TAX RECEIPT PORTAL (https://bharatkosh.gov.in). A receipt will be generated. Please fill in the Transaction Ref No. and Date from the receipt, in print out of application and attach receipt along with hard copy of application.");                          

            //}
            //else
            //{
            //    lblFee.Text = "Processing Fee : Not Taken.";

            //}

            if (obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber != "")
            {
                msgSubmit.Text = "Your Application Submitted Successfully.Your Application Details are :";
                msgSubmit.ForeColor = System.Drawing.Color.Green;

                lblAppliedForRenewal.Text = HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(obj_InfrastructureRenewApplication.LinkDepth));


                // new code added for nth renewal

                obj_InfrastructureRenewApplication.GetPreviousInfrastructureApplication(out obj_infrastructureNewApplicationPrevious, out obj_infrastructureRenewApplicationPrevious);

                // Existing NOC Details

                if (obj_infrastructureNewApplicationPrevious != null)
                {
                    lblINFExistingNOCNumber.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplicationPrevious.NOCNumber);

                    NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_infrastructureNewIssusedLetter = new NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter(obj_infrastructureNewApplicationPrevious.ApplicationCode);
                    if (obj_infrastructureNewIssusedLetter.ValidityStartDate != null && obj_infrastructureNewIssusedLetter.ValidityEndDate != null)
                    {
                        lblINFNOCValidity.Text = "(" + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_infrastructureNewIssusedLetter.ValidityStartDate)).ToString("dd/MM/yyyy") + " - " + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_infrastructureNewIssusedLetter.ValidityEndDate)).ToString("dd/MM/yyyy") + ")";
                    }

                    ViewState["PreNOCNumber"] = HttpUtility.HtmlEncode(obj_infrastructureNewApplicationPrevious.NOCNumber);
                }

                if (obj_infrastructureRenewApplicationPrevious != null)
                {
                    lblINFExistingNOCNumber.Text = HttpUtility.HtmlEncode(obj_infrastructureRenewApplicationPrevious.NOCNumber);

                    NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter obj_infrastructureRenewIssusedLetter = new NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter(obj_infrastructureRenewApplicationPrevious.InfrastructureRenewApplicationCode);
                    if (obj_infrastructureRenewIssusedLetter.ValidityStartDate != null && obj_infrastructureRenewIssusedLetter.ValidityEndDate != null)
                    {
                        lblINFNOCValidity.Text = "(" + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_infrastructureRenewIssusedLetter.ValidityStartDate)).ToString("dd/MM/yyyy") + " - " + Convert.ToDateTime(HttpUtility.HtmlEncode(obj_infrastructureRenewIssusedLetter.ValidityEndDate)).ToString("dd/MM/yyyy") + ")";
                    }

                    ViewState["PreNOCNumber"] = HttpUtility.HtmlEncode(obj_infrastructureRenewApplicationPrevious.NOCNumber);
                }

                // End Existing NOC Details
                
                // end new code added for nth renewal
                
                lng_AppCode = obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode;
                int_AppTypeCode = obj_InfrastructureRenewApplication.ApplicationTypeCode;
                int_AppPurposeCode = obj_InfrastructureRenewApplication.ApplicationPurposeCode;
                int_UserCode = obj_InfrastructureRenewApplication.ApplicantUserCode;
                lng_ExuserCode = Convert.ToInt64(obj_InfrastructureRenewApplication.ApplicantExternalUserCode);
                lblAppNo.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber.ToString());
                lblNameofInfrastructure.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.NameOfInfrastructure.ToString());
                ViewState["AppNo"] = obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber.ToString();
                ViewState["NameofIndustry"] = obj_InfrastructureNewApplication.NameOfInfrastructure.ToString();
                ViewState["CommunicationEmailID"] = obj_InfrastructureRenewApplication.CommunicationEmailID;
                ViewState["CommunicationMobileNumber"] = obj_InfrastructureRenewApplication.CommunicationMobileNumber.MobileNumberRest;
                lblSubmitDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_InfrastructureRenewApplication.SubmittedOnByExUC).ToString("dd/MM/yyyy"));
                lblRefMsg.Text = "Please note your application number for future reference.";
                lblRegionalOfficeAddress.Text =HttpUtility.HtmlEncode(NOCAPExternalUtility.GetRegionalOfficeAddress(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode)).ToString().Replace("\n", "<br/>");
                ViewState["ApplicationCode"] = obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode;
                ViewState["LinkDepth"] = HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(obj_InfrastructureRenewApplication.LinkDepth));
                //ViewState["PreNOCNumber"] = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.NOCNumber);
                lblNetGroundWaterRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_InfrastructureRenewApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist + obj_InfrastructureRenewApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional));
                   

            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    protected void lbtnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            lblApplicationRenewCode.Text =HttpUtility.HtmlEncode(ViewState["ApplicationCode"]);
        }
        catch (ThreadAbortException)
        {

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
}