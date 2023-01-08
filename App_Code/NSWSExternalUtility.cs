using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Web.SessionState;
using System.Configuration;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http.Headers;

/// <summary>
/// Summary description for NSWSExternalUtility
/// </summary>
public class NSWSExternalUtility
{

    public NSWSExternalUtility()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static long DateTimeToEpoch(DateTime date)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return Convert.ToInt64((date - epoch).TotalSeconds);

    }
    public static DateTime EpochDateTime(double timestamp)
    {
        DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0); //from start epoch time
        start = start.AddSeconds(timestamp);
        return start;
    }
    public static byte[] INDScanLetterFiles(long int_appcode)
    {
        try
        {
            byte[] bytes = null;
            NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter objIndustrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter();
            NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter objIndustrialNewIssusedLetterFile = objIndustrialNewIssusedLetter.DownloadScanLetterFile(int_appcode);
            if (objIndustrialNewIssusedLetterFile != null)
            {
                bytes = objIndustrialNewIssusedLetterFile.AttachmentFile;



            }
            return bytes;
        }
        catch (Exception)
        {
            return null;
        }
    }
    public static byte[] INFScanLetterFiles(long int_appcode)
    {
        try
        {
            byte[] bytes = null;
            NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_InfrastructureNewIssusedLetter = new NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter();
            NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_InfrastructureNewIssusedLetterFile = obj_InfrastructureNewIssusedLetter.DownloadScanLetterFile(int_appcode);
            if (obj_InfrastructureNewIssusedLetterFile != null)
            {
                bytes = obj_InfrastructureNewIssusedLetterFile.AttachmentFile;
            }
            return bytes;
        }
        catch (Exception)
        {
            return null;
        }
    }
    public static byte[] MINScanLetterFiles(long int_appcode)
    {
        try
        {
            byte[] bytes = null;
            NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter obj_MiningNewIssusedLetter = new NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter();
            NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter obj_MiningNewIssusedLetterFile = obj_MiningNewIssusedLetter.DownloadScanLetterFile(int_appcode);
            if (obj_MiningNewIssusedLetterFile != null)
            {
                bytes = obj_MiningNewIssusedLetterFile.AttachmentFile;
            }
            return bytes;
        }
        catch (Exception)
        {
            return null;
        }
    }
    private static void ActionTrail(NSWSUser obj_NSWSUser, string CustumMessage, string strControllerName, string strActionName)
    {
        //strActionName = str_strActionName;
        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(obj_NSWSUser.InvestorSWSId, 0);
        NOCAP.BLL.UserManagement.NSWSActionTrail obj_NSWSActionTrail = new NOCAP.BLL.UserManagement.NSWSActionTrail();
        obj_NSWSActionTrail.UserCode = !string.IsNullOrEmpty(obj_externalUser.ExternalUserCode.ToString()) ? Convert.ToInt64(obj_externalUser.ExternalUserCode) : (Int64?)null;
        obj_NSWSActionTrail.InvestorSWSId = obj_NSWSUser.InvestorSWSId;
        obj_NSWSActionTrail.IP_Address = GetIp();
        obj_NSWSActionTrail.ActionPerformed = strControllerName + "-" + strActionName;
        obj_NSWSActionTrail.Status = CustumMessage;
        if (obj_NSWSActionTrail != null)
            obj_NSWSActionTrail.Add();
    }
    private static string GetIp()
    {
        string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(ip))
        {
            ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        return ip;
    }
    public static int AddNSWSAPIWinServiceCallStatus(long lng_ApplicationCode, int int_APICode, int? int_SendOrder = null, int? int_ActionStatusCode = null, int? int_CreatedByExUC = null, int? int_CreatedByUC = null, NOCAP.BLL.NSWSWinSerAPICall.NSWSAPIWinServiceCallStatus.SendStatusYesNo enum_SendStatus = NOCAP.BLL.NSWSWinSerAPICall.NSWSAPIWinServiceCallStatus.SendStatusYesNo.NotDefine)
    {
        NOCAP.BLL.NSWSWinSerAPICall.NSWSAPIWinServiceCallStatus obj_NSWSAPIWinServiceCallStatus = new NOCAP.BLL.NSWSWinSerAPICall.NSWSAPIWinServiceCallStatus();

        obj_NSWSAPIWinServiceCallStatus.ApplicationCode = lng_ApplicationCode;
        obj_NSWSAPIWinServiceCallStatus.APICode = int_APICode;
        obj_NSWSAPIWinServiceCallStatus.SendOrder = int_SendOrder;
        obj_NSWSAPIWinServiceCallStatus.ActionStatusCode = int_ActionStatusCode;
        obj_NSWSAPIWinServiceCallStatus.CreatedByExUC = int_CreatedByExUC;
        obj_NSWSAPIWinServiceCallStatus.CreatedByUC = int_CreatedByUC;
        obj_NSWSAPIWinServiceCallStatus.SendStatus = enum_SendStatus;
        return obj_NSWSAPIWinServiceCallStatus.Add();
    }
    public static HttpResponseMessage PushLicense(string swsId, long lngA_ApplicationCode, string str_LicenseReqDate, string str_licenseId)
    {

        HttpResponseMessage tokenResponse = null;
        try
        {
            int linkdepth = 0;
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = null;// new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = null;// new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = null;// new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = null;// new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = null;// new NOCAP.BLL.Mining.New.Application.MiningNewApplication();
            NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplication = null;// new NOCAP.BLL.Mining.New.Application.MiningNewApplication();
            NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_industrialNewApplication, out obj_infrastructureNewApplication, out obj_miningNewApplication, out obj_industrialRenewApplication, out obj_infrastructureRenewApplication, out obj_miningRenewApplication, lngA_ApplicationCode);

            if (obj_industrialNewApplication != null || obj_infrastructureNewApplication != null || obj_miningNewApplication != null)
                linkdepth = 1;
            else if (obj_industrialRenewApplication != null)
                linkdepth = obj_industrialRenewApplication.LinkDepth;
            else if (obj_infrastructureRenewApplication != null)
                linkdepth = obj_infrastructureRenewApplication.LinkDepth;
            else if (obj_miningRenewApplication != null)
                linkdepth = obj_miningRenewApplication.LinkDepth;


        
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //DeptCredential obj_DeptCredential = NOCAPExternalUtility.PopulateDeptCredential();
            string mediatype = "application/json";
            HttpClient client = APIUtility.Method_Headers(accessId: ConfigurationManager.AppSettings["AccessId"], accessSecret: ConfigurationManager.AppSettings["AccessSecret"], apiKey: APIKeys.LicensePushKey, endpointURL: APIUrls.LicensePushPPEURL, str_mediaType: mediatype);

            //this input data
            LicensePushAPI obj_LicensePushAPI = new LicensePushAPI();
            List<LicensePushAPI> list_LicensePushAPI = new List<LicensePushAPI>();
            obj_LicensePushAPI.LicenseId = str_licenseId;
            obj_LicensePushAPI.LicenseVer = linkdepth.ToString();
            obj_LicensePushAPI.SWSId = swsId;
            obj_LicensePushAPI.InvestorReqId = "";//lngA_ApplicationCode.ToString();
            obj_LicensePushAPI.LicenseReqDate = str_LicenseReqDate;
            obj_LicensePushAPI.MinisteryId = ConfigurationManager.AppSettings["MinistryId"];
            obj_LicensePushAPI.DepartmentId = ConfigurationManager.AppSettings["DepartmentId"];
            list_LicensePushAPI.Add(obj_LicensePushAPI);

            string registerUserJson = JsonConvert.SerializeObject(list_LicensePushAPI.ToArray());

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Uri.EscapeUriString(client.BaseAddress.ToString()));
            request.Content = new StringContent(registerUserJson, Encoding.UTF8, "application/json");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            tokenResponse = client.PostAsync(Uri.EscapeUriString(client.BaseAddress.ToString()), request.Content).Result;
            tokenResponse = client.PostAsync(Uri.EscapeUriString(client.BaseAddress.ToString()), request.Content).Result;

            return tokenResponse;
        }
        catch (HttpRequestException ex)
        {

            return tokenResponse;
        }
        catch (Exception ex)
        {

            return tokenResponse;
        }

    }
    public static HttpResponseMessage PushLicenseStatus(string swsId, long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Master.NSWSAppStatusMap obj_NSWSAppStatusMap = null;
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = null;
            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = null;
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = null;
            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = null;
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = null;
            NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplication = null;
            NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_industrialNewApplication, out obj_infrastructureNewApplication, out obj_miningNewApplication, out obj_industrialRenewApplication, out obj_infrastructureRenewApplication, out obj_miningRenewApplication, lngA_ApplicationCode);
        
            LicenseStatus obj_LicenseStatus = new LicenseStatus();
            List<LicenseStastusList> list_LicenseStastusList = new List<LicenseStastusList>();
            LicenseStastusList obj_LicenseStastusList = new LicenseStastusList();

            if (obj_industrialNewApplication != null && obj_industrialNewApplication.CreatedByExUC > 0)
            {
                obj_NSWSAppStatusMap = new NOCAP.BLL.Master.NSWSAppStatusMap(obj_industrialNewApplication.LatestApplicationStatusCode);
                obj_LicenseStastusList.LicenseReqNum = obj_industrialNewApplication.InvestorRequestID;
            }

            else if (obj_infrastructureNewApplication != null && obj_infrastructureNewApplication.CreatedByExUC > 0)
            {
                obj_NSWSAppStatusMap = new NOCAP.BLL.Master.NSWSAppStatusMap(obj_infrastructureNewApplication.LatestApplicationStatusCode);
                obj_LicenseStastusList.LicenseReqNum = obj_infrastructureNewApplication.InvestorRequestID;
            }
            else if (obj_miningNewApplication != null && obj_miningNewApplication.CreatedByExUC > 0)
            {
                obj_NSWSAppStatusMap = new NOCAP.BLL.Master.NSWSAppStatusMap(obj_miningNewApplication.LatestApplicationStatusCode);
                obj_LicenseStastusList.LicenseReqNum = obj_miningNewApplication.InvestorRequestID;
            }
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            obj_LicenseStastusList.LicenseStatus = obj_NSWSAppStatusMap.NSWSAppStatusShort;
            list_LicenseStastusList.Add(obj_LicenseStastusList);
            obj_LicenseStatus.MinisteryId = ConfigurationManager.AppSettings["MinistryId"];
            obj_LicenseStatus.DepartmentId = ConfigurationManager.AppSettings["DepartmentId"];
            obj_LicenseStatus.LicenseStastusListCollection = list_LicenseStastusList.ToArray();
            string mediatype = "application/json";
            HttpClient client = APIUtility.Method_Headers(accessId: ConfigurationManager.AppSettings["AccessId"], accessSecret: ConfigurationManager.AppSettings["AccessSecret"], apiKey: APIKeys.LicenseStatusKey, endpointURL: APIUrls.LicenseStatusPPEURL, str_mediaType: mediatype);
            //  this input data
            string registerUserJson = JsonConvert.SerializeObject(obj_LicenseStatus);
            NSWSUser obj_NSWSUser = new NSWSUser();
            obj_NSWSUser.InvestorSWSId = swsId;
            ActionTrail(obj_NSWSUser, registerUserJson, "PushLicenseStatus", "PushLicenseStatus");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Uri.EscapeUriString(client.BaseAddress.ToString()));
            request.Content = new StringContent(registerUserJson, Encoding.UTF8, "application/json");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return client.PostAsync(Uri.EscapeUriString(client.BaseAddress.ToString()), request.Content).Result;


        }
        catch (HttpRequestException ex)
        {

            return null;
        }
        catch (Exception ex)
        {

            return null;
        }

    }
    public static HttpResponseMessage PushDocumentAPI(string swsId, long lngA_ApplicationCode)
    {
        try
        {
            string mediatype = "application/json";
            byte[] arr_file = null;
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null;
            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = null;
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = null;
            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = null;
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = null;
            NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplication = null;
            NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_infrastructureNewApplication, out obj_miningNewApplication, out obj_industrialRenewApplication, out obj_infrastructureRenewApplication, out obj_miningRenewApplication, lngA_ApplicationCode);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
         
            PushDocumentAPI obj_PushDocumentAPI = new PushDocumentAPI();

            obj_PushDocumentAPI.DocumentID = ConfigurationManager.AppSettings["DocumentID"];// "MDOC000099";// + lngA_ApplicationCode.ToString();
            obj_PushDocumentAPI.DocumentName = "No Objection Certificate";
            obj_PushDocumentAPI.SWSId = swsId;
            if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.CreatedByExUC > 0)
            {
                arr_file = INDScanLetterFiles(lngA_ApplicationCode);
                obj_PushDocumentAPI.ApprovalID = obj_IndustrialNewApplication.InvestorRequestID;
                obj_PushDocumentAPI.InvestorReqId = obj_IndustrialNewApplication.InvestorRequestID;
            }
            //else if (obj_industrialRenewApplication != null)
            //{
            //    obj_PushDocumentAPI.ApprovalID = obj_industrialRenewApplication.InvestorRequestID;// swsId + "-" + obj_DeptCredential.LicenseIdIND + str_LicenseReqDate;               
            //    obj_PushDocumentAPI.InvestorReqId = obj_industrialRenewApplication.InvestorRequestID;
            //}
            else if (obj_infrastructureNewApplication != null && obj_infrastructureNewApplication.CreatedByExUC > 0)
            {
                arr_file = INFScanLetterFiles(lngA_ApplicationCode);
                obj_PushDocumentAPI.ApprovalID = obj_infrastructureNewApplication.InvestorRequestID;// swsId + "-" + obj_DeptCredential.LicenseIdIND + str_LicenseReqDate;               
                obj_PushDocumentAPI.InvestorReqId = obj_infrastructureNewApplication.InvestorRequestID;
            }
            //else if (obj_infrastructureRenewApplication != null)
            //{
            //    obj_PushDocumentAPI.ApprovalID = obj_infrastructureRenewApplication.InvestorRequestID;// swsId + "-" + obj_DeptCredential.LicenseIdIND + str_LicenseReqDate;               
            //    obj_PushDocumentAPI.InvestorReqId = obj_infrastructureRenewApplication.InvestorRequestID;
            //}
            else if (obj_miningNewApplication != null && obj_miningNewApplication.CreatedByExUC > 0)
            {

                arr_file = MINScanLetterFiles(lngA_ApplicationCode);
                obj_PushDocumentAPI.ApprovalID = obj_miningNewApplication.InvestorRequestID;// swsId + "-" + obj_DeptCredential.LicenseIdIND + str_LicenseReqDate;               
                obj_PushDocumentAPI.InvestorReqId = obj_miningNewApplication.InvestorRequestID;
            }


            // obj_PushDocumentAPI.MinisteryDepartmentId = ConfigurationManager.AppSettings["MinistryId"] + ConfigurationManager.AppSettings["DepartmentId"];
            obj_PushDocumentAPI.MinisteryDepartmentId = ConfigurationManager.AppSettings["DepartmentId"];
            string requestJson = JsonConvert.SerializeObject(obj_PushDocumentAPI);
            NSWSUser obj_NSWSUser = new NSWSUser();
            obj_NSWSUser.InvestorSWSId = swsId;
            ActionTrail(obj_NSWSUser, requestJson, "PushDocumentAPI", "PushDocumentAPI");
            HttpClient client = APIUtility.Method_Headers(accessId: ConfigurationManager.AppSettings["AccessId"], accessSecret: ConfigurationManager.AppSettings["AccessSecret"], apiKey: APIKeys.PushDocumentKey, endpointURL: APIUrls.PushDocumentPPEURL, str_mediaType: mediatype);

            var multiPartStream = new MultipartFormDataContent();
            MemoryStream stream = new MemoryStream(arr_file);
            ByteArrayContent firstPart = new ByteArrayContent(arr_file, 0, arr_file.Length);
            firstPart.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
            multiPartStream.Add(firstPart, "file", "NoObjectionCertificate.pdf");
            stream.Dispose();
            multiPartStream.Add((new StringContent(requestJson, Encoding.UTF8, "application/json")), "requestJson");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Uri.EscapeUriString(client.BaseAddress.ToString()));
            request.Content = multiPartStream;
            return client.PostAsync(Uri.EscapeUriString(client.BaseAddress.ToString()), request.Content).Result;
        }
        catch (HttpRequestException ex)
        {

            return null;
        }
        catch (Exception ex)
        {

            return null;
        }

    }
    

}