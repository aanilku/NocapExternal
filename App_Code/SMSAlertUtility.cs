using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Policy;


/// <summary>
/// Summary description for SMSAlertUtility
/// </summary>
public class SMSAlertUtility
{
    public SMSAlertUtility()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static bool IsSendSMSAlertEnable()
    {
        if (ConfigurationManager.AppSettings["SendSMSAlertEnable"] != null && ConfigurationManager.AppSettings["SendSMSAlertEnable"] == "Yes")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static string sendAlertToMobile(string msg, string MobileNo, string template_id, out string SMSAlertUserName)
    {
        string username = "";
        try
        {

            string Message = msg;
            string MobNo = MobileNo;
            string SMSurl = ConfigurationManager.AppSettings["SMSAlertUrl"].ToString();
            username = ConfigurationManager.AppSettings["SMSAlertUserName"].ToString();
            string PIN = ConfigurationManager.AppSettings["SMSAlertPassword"].ToString();
            string Senderid = ConfigurationManager.AppSettings["SMSAlertSenderId"].ToString();
            string Entityid = ConfigurationManager.AppSettings["SMSDeptEntityId"].ToString();
            SMSAlertUserName = username;
            if (SMSurl != null && SMSurl.Substring(0, 5) == "https")
            {
                Check_SSL_Certificate();
            }
            SMSurl += username + "&pin=" + PIN + "&message=" + Message + "&mnumber=" + MobNo + "&signature=" + Senderid + "&dlt_entity_id=" + Entityid + "&dlt_template_id=" + template_id;

            //Response.Write("http://smsgw.sms.gov.in/failsafe/HttpLink?username=nocagw.auth&pin=$Gd5Kp%231&message=a&mnumber=918130910200&signature=NICSMS");

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(SMSurl);
            req.Method = "POST";
            HttpWebResponse myResp = (HttpWebResponse)req.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = "";
            responseString = checkSendStatus(respStreamReader.ReadToEnd());
            respStreamReader.Close();
            myResp.Close();

            return responseString;

        }
        catch (Exception)
        {
            SMSAlertUserName = username;
            return "";
        }

    }

    public static string checkSendStatus(string str)
    {
        string targert_str1 = "info=";
        string target_str2 = "&";
        int count = 0;
        int len1 = targert_str1.Length;
        int indexfirst = 0;
        int totallengthfirst = 0;
        int indexsecond = 0;
        int occuredcount = 2;
        //string str = "Message Accepted for Request ID=13314108572451122533564~code=API000 & info=Platform accepted & Time =2014/09/16/14/17";
        string resultstr;
        //Response.Write(str.IndexOf(targert_str1));
        indexfirst = str.IndexOf(targert_str1);

        totallengthfirst = indexfirst + len1;
        //Response.Write(totallengthfirst);

        for (int i = 0; i <= str.Length - 1; i++)
        {
            if (target_str2 == str[i].ToString())
            {
                count++;
                if (occuredcount == count)
                {
                    indexsecond = i;
                    break;
                }
            }
        }
        resultstr = str.Substring(totallengthfirst, indexsecond - totallengthfirst);
        return resultstr;

    }

    //public static bool RemoteCertificateValidate(object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyError)
    //{
    //    if (((Convert.ToInt32(sslPolicyError) + SslPolicyErrors.RemoteCertificateChainErrors) == SslPolicyErrors.RemoteCertificateChainErrors))
    //    {
    //        return false;
    //    }
    //    else if (((Convert.ToInt32(sslPolicyError) + SslPolicyErrors.RemoteCertificateNameMismatch) == SslPolicyErrors.RemoteCertificateNameMismatch))
    //    {
    //        Zone z;
    //        z = Zone.CreateFromUrl(((HttpWebRequest)(sender)).RequestUri.ToString());
    //        if (((z.SecurityZone == System.Security.SecurityZone.Intranet) || (z.SecurityZone == System.Security.SecurityZone.MyComputer)))
    //        {
    //            return true;
    //        }
    //        return false;
    //    }
    //    else
    //    {
    //        return true;
    //    }
    //}

    public static void Check_SSL_Certificate()
    {
       
       // ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(RemoteCertificateValidate);
        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);
    }

    public static bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        if (((sslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) == SslPolicyErrors.RemoteCertificateChainErrors))
        {
            return true;
        }
        else if (((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) == SslPolicyErrors.RemoteCertificateNameMismatch))
        {
            Zone z = default(Zone);
            z = Zone.CreateFromUrl(((HttpWebRequest)sender).RequestUri.ToString());
            if ((z.SecurityZone == System.Security.SecurityZone.Intranet | z.SecurityZone == System.Security.SecurityZone.MyComputer))
            {
                return true;
            }
            return true;
        }
        else
        {
            return true;
        }
        //return true;
    }



}