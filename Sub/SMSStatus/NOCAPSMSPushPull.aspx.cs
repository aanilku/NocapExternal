using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Security.Policy;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Configuration;
using System.Net;


public partial class NOCAPSMSPushPull : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int intExistCount = 0;
        try
        {
            if (Request != null && Request.UserHostAddress == "164.100.14.193")
            {
                if (Request.QueryString["Sender"] != null && Request.QueryString["Destination"] != null && Request.QueryString["Message"] != null && Request.QueryString["Time"] != null)
                {
                    string Sender = Request.QueryString["Sender"].ToString();
                    string Destination = Request.QueryString["Destination"].ToString();
                    string Message = Request.QueryString["Message"].ToString();
                    string Time = Request.QueryString["Time"].ToString();

                    string strSendMessage = "";
                    string strApplicationType = "";
                    if (Message.Length > 10)
                    {
                        strApplicationType = Message.Substring(Message.Length - 8, 3).ToUpper();
                    }
                    AddPushPullSMSStatus(Sender, Destination, Message, Convert.ToDateTime(Time));
                    //AddPushPullSMSStatus("Sender", "Destination", Message, Convert.ToDateTime(Time));

                    string strSender = Sender.Substring(Sender.Length - 10, 10);

                    NOCAP.BLL.SMSInterface.PushPullSMSStatus obj_PushPullSMSStatus = new NOCAP.BLL.SMSInterface.PushPullSMSStatus();
                    obj_PushPullSMSStatus.ApplicationNumber = Message;
                    obj_PushPullSMSStatus.SenderMobNumber = strSender;
                    intExistCount = obj_PushPullSMSStatus.ExistUser();
                    if (intExistCount > 0)
                    {
                        switch (strApplicationType)
                        {
                            case "IND":
                                strSendMessage = GetCurrentINDApplicationStatus(Message);
                                break;
                            case "INF":
                                strSendMessage = GetCurrentINFApplicationStatus(Message);
                                break;
                            case "MIN":
                                strSendMessage = GetCurrentMINApplicationStatus(Message);
                                break;
                            default:
                                strSendMessage = "No Information Found";
                                break;

                        }
                    }
                    else
                    {
                        strSendMessage = "Mobile Number Not Registered";

                    }

                    if (SMSUtility.IsSendSMSEnable())
                    {
                        if (Sender != "" && strSendMessage != "")
                        {
                            if (sendsms(Sender, strSendMessage).Trim() == "Platform accepted")
                            {
                                // update
                                UpdatePushPullSMSStatus(Sender, Convert.ToDateTime(Time), NOCAP.BLL.SMSInterface.PushPullSMSStatus.SendStatusOption.Yes, strSendMessage);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally
        {
            Response.End();
        }
    }


    void AddPushPullSMSStatus(string Sender, string Destination, string Message, DateTime Time)
    {
        try
        {
            NOCAP.BLL.SMSInterface.PushPullSMSStatus objPushPullSMSStatus = new NOCAP.BLL.SMSInterface.PushPullSMSStatus();
            objPushPullSMSStatus.SenderMobNumber = Sender;
            objPushPullSMSStatus.DestinationMobNumber = Destination;
            objPushPullSMSStatus.GetMessage = Message;
            objPushPullSMSStatus.SMSReceiveDateTime = Time;
            objPushPullSMSStatus.AddPushPullSMSStatus();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    void UpdatePushPullSMSStatus(string Sender, DateTime dtSMSReceiveDateTime, NOCAP.BLL.SMSInterface.PushPullSMSStatus.SendStatusOption strSendStatus, string strSendApplicationStatus)
    {
        try
        {
            NOCAP.BLL.SMSInterface.PushPullSMSStatus objPushPullSMSStatus = new NOCAP.BLL.SMSInterface.PushPullSMSStatus();
            objPushPullSMSStatus.SenderMobNumber = Sender;
            objPushPullSMSStatus.SMSReceiveDateTime = dtSMSReceiveDateTime;
            objPushPullSMSStatus.SendStatus = NOCAP.BLL.SMSInterface.PushPullSMSStatus.SendStatusOption.Yes;
            objPushPullSMSStatus.SendApplicationStatus = strSendApplicationStatus;
            objPushPullSMSStatus.UpdatePushPullSMSStatus();
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    string GetCurrentINDApplicationStatus(string strINDAppNumber)
    {
        try
        {

            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(strINDAppNumber);
            if (obj_IndustrialNewApplication.EligibleForExemptionLetter == NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication.EligibleForExemptionLetterOption.Yes)
            {
                return "Application is Exemted";
            }
            else
            {
                NOCAP.BLL.Common.CurrentStatus obj_CurrentStatus = obj_IndustrialNewApplication.GetCurrentStatus();
                if (obj_CurrentStatus != null)
                {
                    return obj_CurrentStatus.CurrentStage;
                }
                else
                {
                    return "No Information Found";
                }
            }
        }
        catch (Exception)
        {
            return "";

        }
    }

    string GetCurrentINFApplicationStatus(string strINFAppNumber)
    {
        try
        {
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(strINFAppNumber);

            if (obj_InfrastructureNewApplication.EligibleForExemptionLetter == NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication.EligibleForExemptionLetterOption.Yes)
            {
                return "Application is Exemted";
            }
            else
            {

                NOCAP.BLL.Common.CurrentStatus obj_CurrentStatus = obj_InfrastructureNewApplication.GetCurrentStatus();
                if (obj_CurrentStatus != null)
                {
                    return obj_CurrentStatus.CurrentStage;
                }
                else
                {
                    return "No Information Found";
                }
            }
        }
        catch (Exception)
        {
            return "";

        }
    }
    string GetCurrentMINApplicationStatus(string strMINAppNumber)
    {
        try
        {
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(strMINAppNumber);


            if (obj_MiningNewApplication.EligibleForExemptionLetter == NOCAP.BLL.Mining.New.Application.MiningNewApplication.EligibleForExemptionLetterOption.Yes)
            {
                return "Application is Exemted";
            }
            else
            {

                NOCAP.BLL.Common.CurrentStatus obj_CurrentStatus = obj_MiningNewApplication.GetCurrentStatus();
                if (obj_CurrentStatus != null)
                {
                    return obj_CurrentStatus.CurrentStage;
                }
                else
                {
                    return "No Information Found";
                }
            }
        }
        catch (Exception)
        {
            return "";

        }
    }




    public string sendsms(string strMobno, string strMessage)
    {

        try
        {
            string SMSurl = ConfigurationManager.AppSettings["SMSUrl"].ToString();
            string username = ConfigurationManager.AppSettings["SMSUserName"].ToString();
            string PIN = ConfigurationManager.AppSettings["SMSPassword"].ToString();
            string Senderid = ConfigurationManager.AppSettings["SMSSenderId"].ToString();
            if (SMSurl != null && SMSurl.Substring(0, 5) == "https")
            {
                Check_SSL_Certificate();
            }
            string responseString = "";
            if ((strMobno != "") && (strMessage != ""))
            {
                SMSurl += username + "&pin=" + PIN + "&message=" + strMessage + "&mnumber=" + strMobno + "&signature=" + Senderid;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(SMSurl);
                HttpWebResponse httpResponse = (HttpWebResponse)(request.GetResponse());
                // string s = httpResponse.StatusDescription;
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(httpResponse.GetResponseStream());
                responseString = SMSUtility.checkSendStatus(respStreamReader.ReadToEnd());
                respStreamReader.Close();
                httpResponse.Close();

                //System.Threading.Thread.Sleep(3000);
            }
            return responseString;
        }
        catch (Exception)
        {
            return "";
        }

    }
    public static bool RemoteCertificateValidate(object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyError)
    {
        if (((Convert.ToInt32(sslPolicyError) + SslPolicyErrors.RemoteCertificateChainErrors) == SslPolicyErrors.RemoteCertificateChainErrors))
        {
            return false;
        }
        else if (((Convert.ToInt32(sslPolicyError) + SslPolicyErrors.RemoteCertificateNameMismatch) == SslPolicyErrors.RemoteCertificateNameMismatch))
        {
            Zone z;
            z = Zone.CreateFromUrl(((HttpWebRequest)(sender)).RequestUri.ToString());
            if (((z.SecurityZone == System.Security.SecurityZone.Intranet) || (z.SecurityZone == System.Security.SecurityZone.MyComputer)))
            {
                return true;
            }
            return false;
        }
        else
        {
            return true;
        }
    }

    public static void Check_SSL_Certificate()
    {
        // ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(new System.EventHandler(this.RemoteCertificateValidate));
        ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(RemoteCertificateValidate);
    }
}