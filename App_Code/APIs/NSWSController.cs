using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;

public class NSWSController : ApiController
{
    string strControllerName = "NSWSController";
    string strActionName = "Register";



    #region Active APIs
    [HttpGet]
    public string Get()
    {
        return "hello";
    }


    [HttpPost]
    public HttpResponseMessage Register([FromBody] NSWSUser obj_NSWSUser)
    {
        ActionTrail(obj_NSWSUser, obj_NSWSUser.ExternalUserName.ToString() + "," + obj_NSWSUser.ExternalUserEmailID + ", Yes, PING API Called");
        HttpResponseMessage response = null;
        try
        {

            ////New Code
            //string ip = GetIp();
            //string[] ipArr = ConfigurationManager.AppSettings["NSWSIpAddress"].ToString().Split(',');
            //if (!ipArr.Contains(ip))
            //{
            //    ActionTrail(obj_NSWSUser, ip + " - this ip address is not allowed - " + obj_NSWSUser.InvestorSWSId);
            //    response = Request.CreateErrorResponse(System.Net.HttpStatusCode.Unauthorized, ip + " - this ip address is not allowed for Production Server.");
            //    return response;
            //}
            ////End



            if (obj_NSWSUser.InvestorSWSId == "")
            {
                ActionTrail(obj_NSWSUser, "External User - Investor SWS Id should not be empty.-" + obj_NSWSUser.InvestorSWSId);
                response = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, "Investor SWS Id should not be empty");
                return response;

            }
            else
            {

                if (ValidationUtility.ValidateSWSId(obj_NSWSUser.InvestorSWSId))
                {
                    ActionTrail(obj_NSWSUser, "External User -Investor SWS Id should be correct.-" + obj_NSWSUser.InvestorSWSId);
                    response = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, "Investor SWS Id should be correct");
                    return response;

                }
            }

            ActionTrail(obj_NSWSUser, "ApprovalId-" + obj_NSWSUser.ApprovalId);
            if (!(obj_NSWSUser.ApprovalId == ConfigurationManager.AppSettings["LicenseIdIND"] || obj_NSWSUser.ApprovalId == ConfigurationManager.AppSettings["LicenseIdINF"] || obj_NSWSUser.ApprovalId == ConfigurationManager.AppSettings["LicenseIdMIN"]))
            {
                response = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, "ApprovalId does not match/");
                return response;
            }



            if (obj_NSWSUser.ExternalUserTitleCode == 0)
            {
                ActionTrail(obj_NSWSUser, "External User -External User Title Code required.-" + obj_NSWSUser.ExternalUserTitleCode.ToString());
                response = Request.CreateResponse<string>(System.Net.HttpStatusCode.BadRequest, "External User Title Code required");
                return response;
            }
            if (string.IsNullOrEmpty(obj_NSWSUser.ExternalUserFirstName))
            {
                ActionTrail(obj_NSWSUser, "External User -External User First Name required.-" + obj_NSWSUser.ExternalUserFirstName);
                response = Request.CreateResponse<string>(System.Net.HttpStatusCode.BadRequest, "External User First Name required");
                return response;
            }
            obj_NSWSUser.ExternalUserName = obj_NSWSUser.InvestorSWSId;

            //if (string.IsNullOrEmpty(obj_NSWSUser.ExternalUserName))
            //{
            //    ActionTrail(obj_NSWSUser, "External User -External User Name required.-" + obj_NSWSUser.ExternalUserName);
            //    response= Request.CreateResponse<string>(System.Net.HttpStatusCode.BadRequest, "External User Name required");
            //   return response;
            //}
            if (string.IsNullOrEmpty(obj_NSWSUser.ExternalUserEmailID))
            {
                ActionTrail(obj_NSWSUser, "External User -External User EmailID required.-" + obj_NSWSUser.ExternalUserEmailID);
                response = Request.CreateResponse<string>(System.Net.HttpStatusCode.BadRequest, "External User EmailID required");
                return response;
            }
            if (obj_NSWSUser.ExternalUserMobileNumberISD != "91")
            {
                ActionTrail(obj_NSWSUser, "External User -External User Mobile Number ISD must be 91.-" + obj_NSWSUser.ExternalUserMobileNumberISD);
                response = Request.CreateResponse<string>(System.Net.HttpStatusCode.BadRequest, "External User Mobile Number ISD must be 91");
                return response;
            }
            if (string.IsNullOrEmpty(obj_NSWSUser.ExternalUserMobileNumber))
            {
                ActionTrail(obj_NSWSUser, "External User -External User Mobile Number  required.-" + obj_NSWSUser.ExternalUserMobileNumber);
                response = Request.CreateResponse<string>(System.Net.HttpStatusCode.BadRequest, "External User Mobile Number  required");
                return response;
            }

            if (string.IsNullOrEmpty(obj_NSWSUser.AddressLine1))
            {
                ActionTrail(obj_NSWSUser, "External User -AddressLine1  required.-" + obj_NSWSUser.AddressLine1);
                response = Request.CreateResponse<string>(System.Net.HttpStatusCode.BadRequest, "AddressLine1  required");
                return response;
            }
            if (obj_NSWSUser.StateCode == 0)
            {
                ActionTrail(obj_NSWSUser, "External User -State Code  required.-" + obj_NSWSUser.StateCode.ToString());
                response = Request.CreateResponse<string>(System.Net.HttpStatusCode.BadRequest, "State Code required");
                return response;
            }

            if (obj_NSWSUser.DistrictCode == 0)
            {
                ActionTrail(obj_NSWSUser, "External User -District Code  required.-" + obj_NSWSUser.DistrictCode.ToString());
                response = Request.CreateResponse<string>(System.Net.HttpStatusCode.BadRequest, "District Code required");
                return response;
            }



            if (obj_NSWSUser.PinCode == 0)
            {
                ActionTrail(obj_NSWSUser, "External User -Pin Code  required.-" + obj_NSWSUser.PinCode.ToString());
                response = Request.CreateResponse<string>(System.Net.HttpStatusCode.BadRequest, "Pin Code required");
                return response;
            }

            if (obj_NSWSUser.GenderCode == 0)
            {
                ActionTrail(obj_NSWSUser, "External User -Gender Code  required.-" + obj_NSWSUser.GenderCode.ToString());
                response = Request.CreateResponse<string>(System.Net.HttpStatusCode.BadRequest, "Gender Code  required");
                return response;
            }


            if (obj_NSWSUser.IDProofTypeCode == 0)
            {
                ActionTrail(obj_NSWSUser, "External User -ID Proof Type Code required.-" + obj_NSWSUser.IDProofTypeCode.ToString());
                response = Request.CreateResponse<string>(System.Net.HttpStatusCode.BadRequest, "ID Proof Type Code required");
                return response;
            }

            if (string.IsNullOrEmpty(obj_NSWSUser.IDProofUniqueNo))
            {
                ActionTrail(obj_NSWSUser, "External User -ID Proof Unique No required.-" + obj_NSWSUser.IDProofUniqueNo.ToString());
                response = Request.CreateResponse<string>(System.Net.HttpStatusCode.BadRequest, "ID Proof Unique No required");
                return response;
            }

            if (string.IsNullOrEmpty(obj_NSWSUser.DateOfBirth))
            {
                ActionTrail(obj_NSWSUser, "External User -Date Of Birth required.-" + obj_NSWSUser.DateOfBirth.ToString());
                response = Request.CreateResponse<string>(System.Net.HttpStatusCode.BadRequest, "Date Of Birth required");
                return response;
            }


            SSOAuthentication obj_SSOAuthentication = null;
            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser();
            obj_externalUser.InvestorSWSId = obj_NSWSUser.InvestorSWSId;
            obj_externalUser.ExternalUserTitleCode = obj_NSWSUser.ExternalUserTitleCode;
            obj_externalUser.ExternalUserFirstName = obj_NSWSUser.ExternalUserFirstName;
            obj_externalUser.ExternalUserName = obj_NSWSUser.ExternalUserName;

            obj_externalUser.ExternalUserEmailID = obj_NSWSUser.ExternalUserEmailID;

            obj_externalUser.ExternalUserMobileNumberISD = obj_NSWSUser.ExternalUserMobileNumberISD;
            obj_externalUser.ExternalUserMobileNumber = obj_NSWSUser.ExternalUserMobileNumber;
            obj_externalUser.AddressLine1 = obj_NSWSUser.AddressLine1;
            obj_externalUser.StateCode = obj_NSWSUser.StateCode;
            obj_externalUser.DistrictCode = obj_NSWSUser.DistrictCode;
            obj_externalUser.PinCode = obj_NSWSUser.PinCode;
            obj_externalUser.DateOfBirth = Convert.ToDateTime(obj_NSWSUser.DateOfBirth);
            obj_externalUser.GenderCode = obj_NSWSUser.GenderCode;
            obj_externalUser.IDProofTypeCode = obj_NSWSUser.IDProofTypeCode;
            obj_externalUser.IDProofUniqueNo = obj_NSWSUser.IDProofUniqueNo;

            //obj_externalUser.PwdHash = CreateMD5(obj_NSWSUser.InvestorSWSId.ToString() + obj_NSWSUser.ExternalUserName);
            obj_externalUser.PwdHash=NOCAPExternalUtility.GenerateSHA512String(obj_NSWSUser.InvestorSWSId.ToString() + obj_NSWSUser.ExternalUserName);
            switch (obj_NSWSUser.ExternalUserActive)
            {
                case "Y":
                    obj_externalUser.ExternalUserActive = NOCAP.BLL.UserManagement.ExternalUser.ExternalUserActiveYesNo.Yes;
                    break;
                case "N":
                    obj_externalUser.ExternalUserActive = NOCAP.BLL.UserManagement.ExternalUser.ExternalUserActiveYesNo.No;
                    break;
                default:
                    obj_externalUser.ExternalUserActive = NOCAP.BLL.UserManagement.ExternalUser.ExternalUserActiveYesNo.No;
                    break;
            }
            NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(obj_NSWSUser.InvestorSWSId);
            #region User Exists in NOCAP
            if (obj_ExternalUser.InvestorSWSId != "")
            {
                HttpResponseMessage message = AccessToken(obj_NSWSUser, APIUrls.authTokenPPEURL, ref obj_SSOAuthentication);
                if (message.StatusCode == HttpStatusCode.OK)
                {
                    ActionTrail(obj_NSWSUser, "Existing user-" + "AccessToken-" + obj_SSOAuthentication.AccessToken);
                    message = RedirectAPI(obj_SSOAuthentication.AccessToken, obj_externalUser.InvestorSWSId.ToString(), obj_externalUser.ExternalUserName, obj_NSWSUser.ApprovalId);
                    if (message.IsSuccessStatusCode)
                    {
                        ActionTrail(obj_NSWSUser, "Existing user-" + "Redirectional url reponse is OK");

                        if (obj_externalUser.AddSWSUser() > 0)
                        {
                            ActionTrail(obj_NSWSUser, obj_externalUser.CustumMessage);
                            response = Request.CreateResponse<string>(System.Net.HttpStatusCode.OK, obj_externalUser.CustumMessage);
                        }
                        else
                        {
                            ActionTrail(obj_NSWSUser, obj_externalUser.CustumMessage);
                            if (obj_externalUser.CustumMessage == "Error in executing SQL")
                                response = Request.CreateResponse<string>(System.Net.HttpStatusCode.InternalServerError, obj_externalUser.CustumMessage);
                            else
                                response = Request.CreateResponse<string>(System.Net.HttpStatusCode.OK, obj_externalUser.CustumMessage);

                        }
                    }
                    else
                    {
                        ActionTrail(obj_NSWSUser, "Existing user-" + "Redirectional url reponse is not OK");
                        ActionTrail(obj_NSWSUser, Convert.ToString(message));
                        //
                        response = Request.CreateResponse<string>(message.StatusCode, message.ReasonPhrase);

                    }

                }
                
                else
                {   ActionTrail(obj_NSWSUser, "Existing user-" + "AccessToken-" + message.ReasonPhrase); 
                  
                    response = Request.CreateResponse<string>(message.StatusCode, message.ReasonPhrase);

                }
            }
            #endregion

            #region User does not Exists in NOCAP
            else
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUserlist = new NOCAP.BLL.UserManagement.ExternalUser();
                obj_ExternalUserlist.GetAll();
                NOCAP.BLL.UserManagement.ExternalUser[] arr_ExternalUser = obj_ExternalUserlist.ExternalUserCollection;
                NOCAP.BLL.UserManagement.ExternalUser[] arr_ExternalUser2 = arr_ExternalUser.Where(a => a.ExternalUserMobileNumber == obj_NSWSUser.ExternalUserMobileNumber).ToArray();
                if (arr_ExternalUser2.Length > 0)
                {
                    ActionTrail(obj_NSWSUser, "Non-existing user-" + "User exists with mobile nnumber " + obj_NSWSUser.ExternalUserMobileNumber);
                    response = Request.CreateResponse<string>(System.Net.HttpStatusCode.Conflict, "User exists with mobile nnumber " + obj_NSWSUser.ExternalUserMobileNumber);
                }
                else
                {

                    HttpResponseMessage message = AccessToken(obj_NSWSUser, APIUrls.authTokenPPEURL, ref obj_SSOAuthentication);
                    //new
                    ActionTrail(obj_NSWSUser, "Non-existing user-" + Convert.ToString(message));
                    //
                    if (message.StatusCode == HttpStatusCode.OK)
                    {
                        ActionTrail(obj_NSWSUser, "Non-existing user-" + "AccessToken-" + obj_SSOAuthentication.AccessToken);
                        if (obj_externalUser.AddSWSUser() == 1)
                        {
                            ActionTrail(obj_NSWSUser, "Non-existing user-" + obj_externalUser.CustumMessage);
                            if (RedirectAPI(obj_SSOAuthentication.AccessToken, obj_externalUser.InvestorSWSId.ToString(), obj_externalUser.ExternalUserName, obj_NSWSUser.ApprovalId).IsSuccessStatusCode)
                            {
                                ActionTrail(obj_NSWSUser, "Non-existing user-" + "Redirectional url reponse is OK");
                            }
                            else { ActionTrail(obj_NSWSUser, "Non-existing user-" + "Redirectional url reponse is not OK"); }
                        }
                        else
                        {
                            ActionTrail(obj_NSWSUser, "Non-existing user-" + obj_externalUser.CustumMessage);
                            response = Request.CreateResponse<string>(System.Net.HttpStatusCode.Conflict, obj_externalUser.CustumMessage);
                        }
                    }
                    else { ActionTrail(obj_NSWSUser, "Non-existing user-" + "AccessToken-" + message.ReasonPhrase); }

                }
            }
            #endregion
            return response;
        }
        catch (Exception ex)
        {
            ActionTrail(obj_NSWSUser, ex.Message);
            return Request.CreateResponse<string>(HttpStatusCode.Forbidden, ex.Message);

        }
    }
    private HttpResponseMessage AccessToken(NSWSUser obj_NSWSUser, string URL, ref SSOAuthentication ref_SSOAuthentication)
    {
        HttpResponseMessage messge = null;
        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            Dictionary<string, string> openWith = new Dictionary<string, string>();
            openWith.Add("client_id", ConfigurationManager.AppSettings["ClientId"]);
            openWith.Add("client_secret", ConfigurationManager.AppSettings["ClientSecret"]);
            openWith.Add("grant_type", ConfigurationManager.AppSettings["GrantType"]);
            openWith.Add("username", ConfigurationManager.AppSettings["Username"]);
            openWith.Add("password", ConfigurationManager.AppSettings["Password"]);
            FormUrlEncodedContent content = new FormUrlEncodedContent(openWith);
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            messge = client.PostAsync(URL, content).Result;
            if (messge != null)
            {
                if (messge.IsSuccessStatusCode)
                {
                    string result = messge.Content.ReadAsStringAsync().Result;
                    ref_SSOAuthentication = JsonConvert.DeserializeObject<SSOAuthentication>(result);
                }
                else
                {
                    // Result_Msg = messge.ReasonPhrase;
                    ref_SSOAuthentication = null;
                }
            }
        }
        catch (WebException ex)
        {
            if (ex.Status == WebExceptionStatus.NameResolutionFailure)
            {
                ActionTrail(obj_NSWSUser, "NameResolutionFailure in SSO-" + ex.Message);
            }
            if (ex.Status == WebExceptionStatus.ProtocolError)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null)
                {
                    ActionTrail(obj_NSWSUser, "ProtocolError in SSO-" + (int)response.StatusCode);
                }
                else
                {
                    ActionTrail(obj_NSWSUser, "ProtocolError in SSO-" + ex.Message);
                }
            }
            else
            {
                ActionTrail(obj_NSWSUser, "ProtocolError in SSO-" + ex.Message);
            }
            ref_SSOAuthentication = null;

        }
        catch (Exception ex)
        {
            ActionTrail(obj_NSWSUser, "Exception in SSO-" + ex.Message);
            ActionTrail(obj_NSWSUser, "Exception in SSO-" + ex.InnerException.Message.ToString());
            ActionTrail(obj_NSWSUser, "Exception in SSO-" + ex.InnerException.InnerException.Message.ToString());
            // ActionTrail(obj_NSWSUser, "Exception in SSO-" + ex.InnerException.InnerException.InnerException.Message.ToString());

            ref_SSOAuthentication = null;

        }
        return messge;
    }
    private HttpResponseMessage RedirectAPI(string accessToken, string swsId, string userName, string str_ApprovalId)
    {
        HttpResponseMessage tokenResponse = null;
        try
        {


            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string redirectionUrl = ConfigurationManager.AppSettings["redirectionUrl"].ToString() + HttpUtility.UrlEncode(HttpContext.Current.Server.HtmlEncode(NOCAPExternalUtility.Encrypt(userName)));
            string stateId = string.Empty;
            HttpClient client = APIUtility.Method_Headers(accessToken, APIUrls.redirectionPPEURL);
            //this input data


            //  string registerUserJson = APIUtility.stringToJson(ConfigurationManager.AppSettings["DepartmentId"], ConfigurationManager.AppSettings["LicenseIdIND"], redirectionUrl, swsId, stateId);
            string registerUserJson = APIUtility.stringToJson(ConfigurationManager.AppSettings["DepartmentId"], str_ApprovalId, redirectionUrl, swsId, stateId);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Uri.EscapeUriString(client.BaseAddress.ToString()));
            request.Content = new StringContent(registerUserJson, Encoding.UTF8, "application/json");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            tokenResponse = client.PostAsync(Uri.EscapeUriString(client.BaseAddress.ToString()), request.Content).Result;
            NSWSUser g = new NSWSUser();
            g.InvestorSWSId = swsId;

            string result = tokenResponse.Content.ReadAsStringAsync().Result;
            ActionTrail(g, result);
            return tokenResponse;
        }
        catch (HttpRequestException ex)
        {
            return tokenResponse;
        }

    }


    [HttpPost]
    public HttpResponseMessage PushLicense([FromBody] NSWSUser obj_NSWSUser)
    {
        long AppCode = 136;
        DateTime dt = new DateTime();
        HttpResponseMessage tokenResponse = null;
        try
        {

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string mediatype = "application/json";
            HttpClient client = APIUtility.Method_Headers(accessId: ConfigurationManager.AppSettings["AccessId"], accessSecret: ConfigurationManager.AppSettings["AccessSecret"], apiKey: APIKeys.LicensePushKey, endpointURL: APIUrls.LicensePushPPEURL, str_mediaType: mediatype);

            //this input data
            LicensePushAPI obj_LicensePushAPI = new LicensePushAPI();
            List<LicensePushAPI> list_LicensePushAPI = new List<LicensePushAPI>();
            obj_LicensePushAPI.LicenseId = ConfigurationManager.AppSettings["LicenseIdIND"];
            obj_LicensePushAPI.LicenseVer = "1";
            obj_LicensePushAPI.SWSId = obj_NSWSUser.InvestorSWSId;
            obj_LicensePushAPI.InvestorReqId = AppCode.ToString();
            obj_LicensePushAPI.LicenseReqDate = NSWSExternalUtility.DateTimeToEpoch(dt).ToString();
            obj_LicensePushAPI.MinisteryId = ConfigurationManager.AppSettings["MinistryId"];
            obj_LicensePushAPI.DepartmentId = ConfigurationManager.AppSettings["DepartmentId"];
            list_LicensePushAPI.Add(obj_LicensePushAPI);

            string registerUserJson = JsonConvert.SerializeObject(list_LicensePushAPI.ToArray());

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Uri.EscapeUriString(client.BaseAddress.ToString()));
            request.Content = new StringContent(registerUserJson, Encoding.UTF8, "application/json");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            tokenResponse = client.PostAsync(Uri.EscapeUriString(client.BaseAddress.ToString()), request.Content).Result;

            if (tokenResponse.StatusCode == HttpStatusCode.OK || tokenResponse.StatusCode == HttpStatusCode.Conflict)
            {
                if (tokenResponse.IsSuccessStatusCode)
                {
                    string str = "";
                    string result = tokenResponse.Content.ReadAsStringAsync().Result;
                    LicensePushAPIResponse obj_LicensePushAPIResponse = JsonConvert.DeserializeObject<LicensePushAPIResponse>(result);
                    NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(AppCode);
                    ActionTrail(obj_NSWSUser, "result-" + result);
                    LicenseReqid obj_LicenseReqid = obj_LicensePushAPIResponse.licenseReqid;
                    if (obj_LicensePushAPIResponse.Status != "409")
                    {
                        ActionTrail(obj_NSWSUser, "LicensePushAPIResponse-" + obj_LicensePushAPIResponse.licenseReqid.SavedId[0]);
                        obj_IndustrialNewApplication.SetInvestorReqID(AppCode, obj_LicenseReqid.SavedId[0], out str);
                        ActionTrail(obj_NSWSUser, "out  str-" + str);
                    }
                    else
                    {
                        ActionTrail(obj_NSWSUser, "LicensePushAPIResponse-" + obj_LicensePushAPIResponse.licenseReqid.DuplicateId[0]);
                        obj_IndustrialNewApplication.SetInvestorReqID(AppCode, obj_LicenseReqid.DuplicateId[0], out str);
                        ActionTrail(obj_NSWSUser, "out 409 str-" + str);

                    }
                }
            }
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

    [HttpPost]
    public HttpResponseMessage LicenseStatus([FromBody] NSWSUser obj_NSWSUser)
    {
        long AppCode = 175;
        NOCAP.BLL.Master.NSWSAppStatusMap obj_NSWSAppStatusMap = null;
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null;
        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = null;
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = null;
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = null;
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = null;
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplication = null;
        NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_infrastructureNewApplication, out obj_miningNewApplication, out obj_industrialRenewApplication, out obj_infrastructureRenewApplication, out obj_miningRenewApplication, AppCode);
        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = null;
        HttpResponseMessage tokenResponse = null;
        try
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


            LicenseStatus obj_LicenseStatus = new LicenseStatus();
            List<LicenseStastusList> list_LicenseStastusList = new List<LicenseStastusList>();
            LicenseStastusList obj_LicenseStastusList = new LicenseStastusList();
            if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.CreatedByExUC > 0)
            {
                obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(obj_IndustrialNewApplication.ApplicantExternalUserCode);
                if (obj_externalUser.InvestorSWSId != "")
                {
                    obj_NSWSAppStatusMap = new NOCAP.BLL.Master.NSWSAppStatusMap(obj_IndustrialNewApplication.LatestApplicationStatusCode);
                    obj_LicenseStastusList.LicenseReqNum = obj_IndustrialNewApplication.InvestorRequestID;
                }

            }
            else if (obj_infrastructureNewApplication != null && obj_infrastructureNewApplication.CreatedByExUC > 0)
            {
                obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(obj_infrastructureNewApplication.ApplicantExternalUserCode);
                if (obj_externalUser.InvestorSWSId != "")
                {
                    obj_NSWSAppStatusMap = new NOCAP.BLL.Master.NSWSAppStatusMap(obj_infrastructureNewApplication.LatestApplicationStatusCode);
                    obj_LicenseStastusList.LicenseReqNum = obj_infrastructureNewApplication.InvestorRequestID;
                }
            }
            else if (obj_miningNewApplication != null && obj_miningNewApplication.CreatedByExUC > 0)
            {
                obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(obj_miningNewApplication.ApplicantExternalUserCode);
                if (obj_externalUser.InvestorSWSId != "")
                {
                    obj_NSWSAppStatusMap = new NOCAP.BLL.Master.NSWSAppStatusMap(obj_miningNewApplication.LatestApplicationStatusCode);
                    obj_LicenseStastusList.LicenseReqNum = obj_miningNewApplication.InvestorRequestID;
                }
            }
            if (obj_externalUser.InvestorSWSId != "")
            {
                obj_LicenseStastusList.LicenseStatus = obj_NSWSAppStatusMap.NSWSAppStatusShort;
                list_LicenseStastusList.Add(obj_LicenseStastusList);
                obj_LicenseStatus.MinisteryId = ConfigurationManager.AppSettings["MinistryId"];
                obj_LicenseStatus.DepartmentId = ConfigurationManager.AppSettings["DepartmentId"];
                obj_LicenseStatus.LicenseStastusListCollection = list_LicenseStastusList.ToArray();
                string mediatype = "application/json";

                HttpClient client = APIUtility.Method_Headers(accessId: ConfigurationManager.AppSettings["AccessId"], accessSecret: ConfigurationManager.AppSettings["AccessSecret"], apiKey: APIKeys.LicenseStatusKey, endpointURL: APIUrls.LicenseStatusPPEURL, str_mediaType: mediatype);
                // HttpClient client = APIUtility.Method_Headers(accessId: obj_NSWSAPIHeader.AccessId, accessSecret: obj_NSWSAPIHeader.AccessSecret, apiKey: obj_NSWSAPIHeader.ApiKey, endpointURL: APIUrls.LicenseStatusPPEURL, str_mediaType: mediatype);
                //  this input data
                string registerUserJson = JsonConvert.SerializeObject(obj_LicenseStatus);
                ActionTrail(obj_NSWSUser, registerUserJson + obj_externalUser.InvestorSWSId);
                ActionTrail(obj_NSWSUser, "AccessId-" + ConfigurationManager.AppSettings["AccessId"] + ",accessSecret-" + ConfigurationManager.AppSettings["AccessSecret"] + ",ApiKey-" + APIKeys.LicenseStatusKey + ",url-" + APIUrls.LicenseStatusPPEURL);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Uri.EscapeUriString(client.BaseAddress.ToString()));
                request.Content = new StringContent(registerUserJson, Encoding.UTF8, "application/json");
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                tokenResponse = client.PostAsync(Uri.EscapeUriString(client.BaseAddress.ToString()), request.Content).Result;
            }

            return tokenResponse;
        }
        catch (HttpRequestException ex)
        {
            return null;
        }

    }

    [HttpPost]
    public HttpResponseMessage PushDocumentAPI([FromBody] NSWSUser obj_NSWSUser)
    {
        HttpResponseMessage PushDocumentAPIResponse = null;
        try
        {
            long lngA_ApplicationCode = 182;
            string mediatype = "application/json";
            byte[] arr_file = null;
            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = null;

            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null;
            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = null;
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = null;
            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = null;
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = null;
            NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplication = null;
            NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_infrastructureNewApplication, out obj_miningNewApplication, out obj_industrialRenewApplication, out obj_infrastructureRenewApplication, out obj_miningRenewApplication, lngA_ApplicationCode);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            PushDocumentAPI obj_PushDocumentAPI = new PushDocumentAPI();

            obj_PushDocumentAPI.DocumentID = ConfigurationManager.AppSettings["DocumentID"];
            obj_PushDocumentAPI.DocumentName = "No Objection Certificate";

            if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.CreatedByExUC > 0)
            {
                obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(obj_IndustrialNewApplication.ApplicantExternalUserCode);
                if (obj_externalUser.InvestorSWSId != "")
                {
                    obj_PushDocumentAPI.SWSId = obj_externalUser.InvestorSWSId;
                    arr_file = NSWSExternalUtility.INDScanLetterFiles(lngA_ApplicationCode);
                    obj_PushDocumentAPI.ApprovalID = obj_IndustrialNewApplication.InvestorRequestID;
                    obj_PushDocumentAPI.InvestorReqId = obj_IndustrialNewApplication.InvestorRequestID;
                }
            }

            else if (obj_infrastructureNewApplication != null && obj_infrastructureNewApplication.CreatedByExUC > 0)
            {
                obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(obj_infrastructureNewApplication.ApplicantExternalUserCode);
                if (obj_externalUser.InvestorSWSId != "")
                {
                    obj_PushDocumentAPI.SWSId = obj_externalUser.InvestorSWSId;
                    arr_file = NSWSExternalUtility.INFScanLetterFiles(lngA_ApplicationCode);
                    obj_PushDocumentAPI.ApprovalID = obj_infrastructureNewApplication.InvestorRequestID;
                    obj_PushDocumentAPI.InvestorReqId = obj_infrastructureNewApplication.InvestorRequestID;
                }
            }

            else if (obj_miningNewApplication != null && obj_miningNewApplication.CreatedByExUC > 0)
            {

                obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(obj_miningNewApplication.ApplicantExternalUserCode);
                if (obj_externalUser.InvestorSWSId != "")
                {
                    obj_PushDocumentAPI.SWSId = obj_externalUser.InvestorSWSId;
                    arr_file = NSWSExternalUtility.MINScanLetterFiles(lngA_ApplicationCode);
                    obj_PushDocumentAPI.ApprovalID = obj_miningNewApplication.InvestorRequestID;
                    obj_PushDocumentAPI.InvestorReqId = obj_miningNewApplication.InvestorRequestID;
                }
            }
            if (obj_externalUser.InvestorSWSId != "")
            {
                //obj_PushDocumentAPI.MinisteryDepartmentId = ConfigurationManager.AppSettings["MinistryId"] + ConfigurationManager.AppSettings["DepartmentId"];
                obj_PushDocumentAPI.MinisteryDepartmentId = ConfigurationManager.AppSettings["DepartmentId"];
                string requestJson = JsonConvert.SerializeObject(obj_PushDocumentAPI);
                ActionTrail(obj_NSWSUser, requestJson);
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
                PushDocumentAPIResponse = client.PostAsync(Uri.EscapeUriString(client.BaseAddress.ToString()), request.Content).Result;


            }
            return PushDocumentAPIResponse;
        }
        catch (HttpRequestException ex)
        {

            return PushDocumentAPIResponse;
        }
        catch (Exception ex)
        {

            return PushDocumentAPIResponse;
        }

        //    HttpResponseMessage tokenResponse = null;
        //    try
        //    {
        //        long AppCode = 152;

        //        byte[] arr_file = null;
        //        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null;
        //        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = null;
        //        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = null;
        //        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = null;
        //        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = null;
        //        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplication = null;
        //        NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_infrastructureNewApplication, out obj_miningNewApplication, out obj_industrialRenewApplication, out obj_infrastructureRenewApplication, out obj_miningRenewApplication, AppCode);



        //        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //        NSWSAPIHeader obj_NSWSAPIHeader = new NSWSAPIHeader(APIKeys.PushDocumentKey);

        //        PushDocumentAPI obj_PushDocumentAPI = new PushDocumentAPI();
        //        if (obj_IndustrialNewApplication != null)
        //        {
        //            arr_file = NSWSExternalUtility.INDScanLetterFiles(AppCode);
        //            obj_PushDocumentAPI.DocumentID = "MDOC" + obj_IndustrialNewApplication.IndustrialNewApplicationCode.ToString();
        //            obj_PushDocumentAPI.ApprovalID = obj_IndustrialNewApplication.InvestorRequestID;//optional it can be blank
        //            obj_PushDocumentAPI.InvestorReqId = obj_IndustrialNewApplication.InvestorRequestID;
        //        }
        //        else if (obj_infrastructureNewApplication != null)
        //        {
        //            arr_file = NSWSExternalUtility.INFScanLetterFiles(AppCode);
        //            obj_PushDocumentAPI.DocumentID = "MDOC" + obj_infrastructureNewApplication.InfrastructureNewApplicationCode.ToString();
        //            obj_PushDocumentAPI.ApprovalID = obj_infrastructureNewApplication.InvestorRequestID;//optional it can be blank
        //            obj_PushDocumentAPI.InvestorReqId = obj_infrastructureNewApplication.InvestorRequestID;
        //        }
        //        else if (obj_miningNewApplication != null)
        //        {
        //            arr_file = NSWSExternalUtility.MINScanLetterFiles(AppCode);
        //            obj_PushDocumentAPI.DocumentID = "MDOC" + obj_miningNewApplication.ApplicationCode.ToString();
        //            obj_PushDocumentAPI.ApprovalID = obj_miningNewApplication.InvestorRequestID;//optional it can be blank
        //            obj_PushDocumentAPI.InvestorReqId = obj_miningNewApplication.InvestorRequestID;
        //        }
        //        obj_PushDocumentAPI.DocumentName = "No Objection Certificate";

        //        obj_PushDocumentAPI.SWSId = obj_NSWSUser.InvestorSWSId;

        //        obj_PushDocumentAPI.MinisteryDepartmentId = ConfigurationManager.AppSettings["MinistryId"] + ConfigurationManager.AppSettings["DepartmentId"];
        //        string requestJson = JsonConvert.SerializeObject(obj_PushDocumentAPI);

        //string mediatype = "application/json";//"multipart/form-data";
        //HttpClient client = APIUtility.Method_Headers(accessId: obj_NSWSAPIHeader.AccessId, accessSecret: obj_NSWSAPIHeader.AccessSecret, apiKey: obj_NSWSAPIHeader.ApiKey, endpointURL: APIUrls.PushDocumentPPEURL, str_mediaType: mediatype);




        //var multiPartStream = new MultipartFormDataContent();
        //MemoryStream stream = new MemoryStream(arr_file);
        //ByteArrayContent firstPart = new ByteArrayContent(arr_file, 0, arr_file.Length);
        //firstPart.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
        //multiPartStream.Add(firstPart, "file", "LicensePush.pdf");
        //stream.Dispose();
        //multiPartStream.Add((new StringContent(requestJson, Encoding.UTF8, "application/json")), "requestJson");
        //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Uri.EscapeUriString(client.BaseAddress.ToString()));
        //request.Content = multiPartStream;
        //tokenResponse = client.PostAsync(Uri.EscapeUriString(client.BaseAddress.ToString()), request.Content).Result;
        //ActionTrail(obj_NSWSUser, tokenResponse.ToString());
        //if (tokenResponse.StatusCode == HttpStatusCode.OK)
        //{
        //        if (tokenResponse.IsSuccessStatusCode)
        //        {
        //            string str = tokenResponse.Content.ReadAsStringAsync().Result;
        //            var a = JsonConvert.DeserializeObject(tokenResponse.Content.ReadAsStringAsync().Result);
        //            ActionTrail(obj_NSWSUser, str);
        //        }
        //    }
        //    return tokenResponse;


        //}
        //catch (HttpRequestException ex)
        //{
        //    ActionTrail(obj_NSWSUser, "HttpRequestException-" + ex.Message);
        //    return tokenResponse;
        //}
        //catch (Exception ex)
        //{
        //    ActionTrail(obj_NSWSUser, "Exception-" + ex.Message);
        //    return tokenResponse;
        //}

    }


    [HttpPost]
    public HttpResponseMessage RaiseClarification()
    {


        try
        {
            long AppCode = 158;
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null;
            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = null;
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = null;
            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = null;
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = null;
            NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplication = null;
            NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_infrastructureNewApplication, out obj_miningNewApplication, out obj_industrialRenewApplication, out obj_infrastructureRenewApplication, out obj_miningRenewApplication, AppCode);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


            RaiseClarification obj_RaiseClarification = new RaiseClarification();
            licenseIdentificationAttributeList obj_licenseIdentificationAttributeList = new licenseIdentificationAttributeList();
            List<licenseIdentificationAttributeList> list_licenseIdentificationAttributeList = new List<licenseIdentificationAttributeList>();
            obj_RaiseClarification.DepartmentId = ConfigurationManager.AppSettings["DepartmentId"];
            obj_RaiseClarification.MinisteryId = ConfigurationManager.AppSettings["MinistryId"];
            if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.CreatedByExUC > 0)
                obj_licenseIdentificationAttributeList.LicenseReqId = obj_IndustrialNewApplication.InvestorRequestID;
            if (obj_infrastructureNewApplication != null && obj_infrastructureNewApplication.CreatedByExUC > 0)
                obj_licenseIdentificationAttributeList.LicenseReqId = obj_infrastructureNewApplication.InvestorRequestID;
            if (obj_miningNewApplication != null && obj_miningNewApplication.CreatedByExUC > 0)
                obj_licenseIdentificationAttributeList.LicenseReqId = obj_miningNewApplication.InvestorRequestID;
            obj_licenseIdentificationAttributeList.QueryDesc = "The deadline for this API is this week.";
            obj_licenseIdentificationAttributeList.QueryType = "N";
            list_licenseIdentificationAttributeList.Add(obj_licenseIdentificationAttributeList);
            obj_RaiseClarification.licenseIdentificationAttributeCollection = list_licenseIdentificationAttributeList.ToArray();

            string requestJson = JsonConvert.SerializeObject(obj_RaiseClarification);
            string mediatype = "application/json";
            HttpClient client = APIUtility.Method_Headers(accessId: ConfigurationManager.AppSettings["AccessId"], accessSecret: ConfigurationManager.AppSettings["AccessSecret"], apiKey: APIKeys.RaiseClarificationKey, endpointURL: APIUrls.RaiseClarificationPPEURL, str_mediaType: mediatype);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Uri.EscapeUriString(client.BaseAddress.ToString()));

            request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
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

    [HttpPost]
    public HttpResponseMessage PullClarification()
    {


        try
        {

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string requestJson = APIUtility.stringToJson(ConfigurationManager.AppSettings["MinistryId"], ConfigurationManager.AppSettings["DepartmentId"]);
            string mediatype = "application/json";
            HttpClient client = APIUtility.Method_Headers(accessId: ConfigurationManager.AppSettings["AccessId"], accessSecret: ConfigurationManager.AppSettings["AccessSecret"], apiKey: APIKeys.PullClarificationResponseKey, endpointURL: APIUrls.PullClarificationResponsePPEURL, str_mediaType: mediatype);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Uri.EscapeUriString(client.BaseAddress.ToString()));

            request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
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

    #endregion

    #region Private Function
    private void ActionTrail(NSWSUser obj_NSWSUser, string CustumMessage)
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
    private string GetIp()
    {
        string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(ip))
        {
            ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        return ip;
    }



    //private string CreateMD5(string input)
    //{
    //    // Use input string to calculate MD5 hash
    //    using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
    //    {
    //        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
    //        byte[] hashBytes = md5.ComputeHash(inputBytes);

    //        // Convert the byte array to hexadecimal string
    //        StringBuilder sb = new StringBuilder();
    //        for (int i = 0; i < hashBytes.Length; i++)
    //        {
    //            sb.Append(hashBytes[i].ToString("X2"));
    //        }
    //        return sb.ToString();
    //    }
    //}
    private long ToUnixTime(DateTime date)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return Convert.ToInt64((date - epoch).TotalSeconds);
    }
    //[HttpPost]
    //public string Register([FromBody] NSWSUser obj_NSWSUser)
    //{
    //    ActionTrail(obj_NSWSUser, obj_NSWSUser.ExternalUserName.ToString() + "," + obj_NSWSUser.ExternalUserEmailID + ", Yes, PING API Called");
    //    HttpResponseMessage localResponsemsg = null;
    //    try
    //    {
    //        if (obj_NSWSUser.InvestorSWSId == "")
    //        {
    //            ActionTrail(obj_NSWSUser, "External User - Investor SWS Id should not be empty.-" + obj_NSWSUser.InvestorSWSId);
    //            // return Request.CreateResponse<string>(System.Net.HttpStatusCode.BadRequest, "Investor SWS Id should not be empty");
    //            return "External User -Investor SWS Id should not be empty.";
    //        }
    //        else
    //        {
    //            if (ValidationUtility.ValidateSWSId(obj_NSWSUser.InvestorSWSId))
    //            {
    //                ActionTrail(obj_NSWSUser, "External User -Investor SWS Id should be in correct.-" + obj_NSWSUser.InvestorSWSId);
    //                return "External User -Investor SWS Id should be in correct.";
    //            }
    //        }
    //        if (obj_NSWSUser.ExternalUserTitleCode == 0)
    //        {
    //            ActionTrail(obj_NSWSUser, "External User -External User Title Code required.-" + obj_NSWSUser.ExternalUserTitleCode.ToString());
    //            return "External User - External User Title Code required.";
    //        }
    //        if (string.IsNullOrEmpty(obj_NSWSUser.ExternalUserFirstName))
    //        {
    //            ActionTrail(obj_NSWSUser, "External User -External User First Name required.-" + obj_NSWSUser.ExternalUserFirstName);
    //            return "External User - External User First Name required.";
    //        }
    //        obj_NSWSUser.ExternalUserName = obj_NSWSUser.InvestorSWSId;
    //        //if (string.IsNullOrEmpty(obj_NSWSUser.ExternalUserName))
    //        //{
    //        //    ActionTrail(obj_NSWSUser, "External User -External User Name required.-" + obj_NSWSUser.ExternalUserName);
    //        //    return "External User - External User Name required.";
    //        //}
    //        if (string.IsNullOrEmpty(obj_NSWSUser.ExternalUserEmailID))
    //        {
    //            ActionTrail(obj_NSWSUser, "External User -External User EmailID required.-" + obj_NSWSUser.ExternalUserEmailID);
    //            return "External User - External User EmailID required.";
    //        }
    //        if (obj_NSWSUser.ExternalUserMobileNumberISD != "91")
    //        {
    //            ActionTrail(obj_NSWSUser, "External User -External User Mobile Number ISD must be 91.-" + obj_NSWSUser.ExternalUserMobileNumberISD);
    //            return "External User - External User Mobile Number ISD must be 91";
    //        }
    //        if (string.IsNullOrEmpty(obj_NSWSUser.ExternalUserMobileNumber))
    //        {
    //            ActionTrail(obj_NSWSUser, "External User -External User Mobile Number  required.-" + obj_NSWSUser.ExternalUserMobileNumber);
    //            return "External User - External User Mobile Number  required";
    //        }
    //        if (string.IsNullOrEmpty(obj_NSWSUser.AddressLine1))
    //        {
    //            ActionTrail(obj_NSWSUser, "External User -AddressLine1  required.-" + obj_NSWSUser.AddressLine1);
    //            return "External User - AddressLine1 required.";
    //        }
    //        if (obj_NSWSUser.StateCode == 0)
    //        {
    //            ActionTrail(obj_NSWSUser, "External User -State Code  required.-" + obj_NSWSUser.StateCode.ToString());
    //            return "External User - State Code required.";
    //        }
    //        ActionTrail(obj_NSWSUser, "External User -State Code  required.-" + obj_NSWSUser.StateCode.ToString());
    //        if (obj_NSWSUser.DistrictCode == 0)
    //        {
    //            ActionTrail(obj_NSWSUser, "External User -District Code  required.-" + obj_NSWSUser.DistrictCode.ToString());
    //            return "External User - District Code required.";
    //        }
    //        ActionTrail(obj_NSWSUser, "External User -District Code  required.-" + obj_NSWSUser.DistrictCode.ToString());
    //        if (obj_NSWSUser.PinCode == 0)
    //        {
    //            ActionTrail(obj_NSWSUser, "External User -Pin Code  required.-" + obj_NSWSUser.PinCode.ToString());
    //            return "External User - Pin Code required.";
    //        }
    //        ActionTrail(obj_NSWSUser, "External User -Pin Code  required.-" + obj_NSWSUser.PinCode.ToString());
    //        if (obj_NSWSUser.GenderCode == 0)
    //        {
    //            ActionTrail(obj_NSWSUser, "External User -Gender Code  required.-" + obj_NSWSUser.GenderCode.ToString());
    //            return "External User - Gender Code required.";
    //        }
    //        ActionTrail(obj_NSWSUser, "External User -Gender Code  required.-" + obj_NSWSUser.GenderCode.ToString());
    //        if (obj_NSWSUser.IDProofTypeCode == 0)
    //        {
    //            ActionTrail(obj_NSWSUser, "External User -ID Proof Type Code required.-" + obj_NSWSUser.IDProofTypeCode.ToString());
    //            return "External User - ID Proof Type Code required.";
    //        }
    //        ActionTrail(obj_NSWSUser, "External User -ID Proof Type Code required.-" + obj_NSWSUser.IDProofTypeCode.ToString());
    //        if (string.IsNullOrEmpty(obj_NSWSUser.IDProofUniqueNo))
    //        {
    //            ActionTrail(obj_NSWSUser, "External User -ID Proof Unique No required.-" + obj_NSWSUser.IDProofUniqueNo.ToString());
    //            return "External User - ID Proof Unique No required.";
    //        }
    //        ActionTrail(obj_NSWSUser, "External User -ID Proof Unique No required.-" + obj_NSWSUser.IDProofUniqueNo.ToString());
    //        if (string.IsNullOrEmpty(obj_NSWSUser.DateOfBirth))
    //        {
    //            ActionTrail(obj_NSWSUser, "External User -Date Of Birth required.-" + obj_NSWSUser.DateOfBirth.ToString());
    //            return "External User - Date Of Birth required.";
    //        }
    //        ActionTrail(obj_NSWSUser, "External User -Date Of Birth required.-" + obj_NSWSUser.DateOfBirth.ToString());
    //        SSOAuthentication obj_SSOAuthentication = null;
    //        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser();
    //        obj_externalUser.InvestorSWSId = obj_NSWSUser.InvestorSWSId;
    //        obj_externalUser.ExternalUserTitleCode = obj_NSWSUser.ExternalUserTitleCode;
    //        obj_externalUser.ExternalUserFirstName = obj_NSWSUser.ExternalUserFirstName;
    //        obj_externalUser.ExternalUserName = obj_NSWSUser.ExternalUserName;

    //        obj_externalUser.ExternalUserEmailID = obj_NSWSUser.ExternalUserEmailID;

    //        obj_externalUser.ExternalUserMobileNumberISD = obj_NSWSUser.ExternalUserMobileNumberISD;
    //        obj_externalUser.ExternalUserMobileNumber = obj_NSWSUser.ExternalUserMobileNumber;
    //        obj_externalUser.AddressLine1 = obj_NSWSUser.AddressLine1;
    //        obj_externalUser.StateCode = obj_NSWSUser.StateCode;
    //        obj_externalUser.DistrictCode = obj_NSWSUser.DistrictCode;
    //        obj_externalUser.PinCode = obj_NSWSUser.PinCode;
    //        obj_externalUser.DateOfBirth = Convert.ToDateTime(obj_NSWSUser.DateOfBirth);
    //        obj_externalUser.GenderCode = obj_NSWSUser.GenderCode;
    //        obj_externalUser.IDProofTypeCode = obj_NSWSUser.IDProofTypeCode;
    //        obj_externalUser.IDProofUniqueNo = obj_NSWSUser.IDProofUniqueNo;
    //        obj_externalUser.PwdHash = CreateMD5(obj_NSWSUser.InvestorSWSId.ToString() + obj_NSWSUser.ExternalUserName);// obj_NSWSUser.PwdHash;
    //        switch (obj_NSWSUser.ExternalUserActive)
    //        {
    //            case "Y":
    //                obj_externalUser.ExternalUserActive = NOCAP.BLL.UserManagement.ExternalUser.ExternalUserActiveYesNo.Yes;
    //                break;
    //            case "N":
    //                obj_externalUser.ExternalUserActive = NOCAP.BLL.UserManagement.ExternalUser.ExternalUserActiveYesNo.No;
    //                break;
    //            default:
    //                obj_externalUser.ExternalUserActive = NOCAP.BLL.UserManagement.ExternalUser.ExternalUserActiveYesNo.No;
    //                break;
    //        }
    //        ActionTrail(obj_NSWSUser, obj_NSWSUser.ExternalUserName.ToString() + "," + obj_NSWSUser.ExternalUserEmailID + ", Yes, before sso authentication PING API Called");
    //        HttpResponseMessage message = AccessToken(obj_NSWSUser, APIUrls.authTokenPPEURL, ref obj_SSOAuthentication);
    //        ActionTrail(obj_NSWSUser, obj_NSWSUser.ExternalUserName.ToString() + "," + obj_NSWSUser.ExternalUserEmailID + ", Yes, after sso authentication PING API Called");
    //        if (message != null && message.StatusCode == HttpStatusCode.OK)
    //        {
    //            ActionTrail(obj_NSWSUser, "Access Token-" + obj_SSOAuthentication.AccessToken);
    //            if (RedirectAPI(obj_SSOAuthentication.AccessToken, obj_externalUser.InvestorSWSId.ToString(), obj_externalUser.ExternalUserName, ref localResponsemsg))
    //            {
    //                if (localResponsemsg.StatusCode == HttpStatusCode.OK)
    //                {
    //                    if (obj_externalUser.AddSWSUser() > 0)
    //                    {
    //                        ActionTrail(obj_NSWSUser, "User has been registered successfully");

    //                        return "User has been registered successfully.";
    //                    }
    //                    else
    //                    {
    //                        ActionTrail(obj_NSWSUser, obj_externalUser.CustumMessage);
    //                        return obj_externalUser.CustumMessage;
    //                    }
    //                }
    //                else
    //                {
    //                    ActionTrail(obj_NSWSUser, "RedirectAPI-" + localResponsemsg.StatusCode.ToString());
    //                    return "problem in RedirectAPI" + localResponsemsg.StatusCode.ToString();
    //                }
    //            }
    //            else
    //            {
    //                if (localResponsemsg != null)
    //                {
    //                    ActionTrail(obj_NSWSUser, "RedirectAPI-" + localResponsemsg.StatusCode.ToString());
    //                    return "problem in RedirectAPI" + localResponsemsg.StatusCode.ToString();
    //                }
    //                else
    //                {
    //                    ActionTrail(obj_NSWSUser, "problem  in RedirectAPI");
    //                    return "problem in RedirectAPI";
    //                }
    //            }
    //        }
    //        else
    //        {
    //            if (message != null)
    //            {
    //                ActionTrail(obj_NSWSUser, "problem  in SSO Authentication -" + message.StatusCode.ToString());
    //                return "problem  in SSO Authentication -" + message.StatusCode.ToString();
    //            }
    //            else
    //            {
    //                ActionTrail(obj_NSWSUser, "problem  in SSO Authentication");
    //                return "problem  in SSO Authentication";

    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        //  HttpResponseException
    //        ActionTrail(obj_NSWSUser, ex.Message);
    //        return ex.Message;

    //    }
    //}
    //private HttpResponseMessage AccessToken(NSWSUser obj_NSWSUser, string URL, ref SSOAuthentication ref_SSOAuthentication)
    //{
    //    HttpResponseMessage messge = null;
    //    try
    //    {
    //        HttpClient client = new HttpClient();
    //        client.BaseAddress = new Uri(URL);
    //        client.DefaultRequestHeaders.Accept.Clear();

    //        Dictionary<string, string> openWith = new Dictionary<string, string>();
    //        openWith.Add("client_id", ConfigurationManager.AppSettings["ClientId"]);
    //        openWith.Add("client_secret", ConfigurationManager.AppSettings["ClientSecret"]);
    //        openWith.Add("grant_type", ConfigurationManager.AppSettings["GrantType"]);
    //        openWith.Add("username", ConfigurationManager.AppSettings["Username"]);
    //        openWith.Add("password", ConfigurationManager.AppSettings["Password"]);
    //        FormUrlEncodedContent content = new FormUrlEncodedContent(openWith);
    //        System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
    //        messge = client.PostAsync(URL, content).Result;
    //        if (messge != null)
    //        {
    //            if (messge.IsSuccessStatusCode)
    //            {
    //                string result = messge.Content.ReadAsStringAsync().Result;
    //                ref_SSOAuthentication = JsonConvert.DeserializeObject<SSOAuthentication>(result);
    //            }
    //            else
    //            {
    //                // Result_Msg = messge.ReasonPhrase;
    //                ref_SSOAuthentication = null;
    //            }
    //        }
    //    }
    //    catch (WebException ex)
    //    {
    //        if (ex.Status == WebExceptionStatus.NameResolutionFailure)
    //        {
    //            ActionTrail(obj_NSWSUser, "NameResolutionFailure in SSO-" + ex.Message);
    //        }
    //        if (ex.Status == WebExceptionStatus.ProtocolError)
    //        {
    //            var response = ex.Response as HttpWebResponse;
    //            if (response != null)
    //            {
    //                ActionTrail(obj_NSWSUser, "ProtocolError in SSO-" + (int)response.StatusCode);
    //            }
    //            else
    //            {
    //                ActionTrail(obj_NSWSUser, "ProtocolError in SSO-" + ex.Message);
    //            }
    //        }
    //        else
    //        {
    //            ActionTrail(obj_NSWSUser, "ProtocolError in SSO-" + ex.Message);
    //        }
    //        ref_SSOAuthentication = null;

    //    }
    //    catch (Exception ex)
    //    {
    //        ActionTrail(obj_NSWSUser, "Exception in SSO-" + ex.Message);
    //        ActionTrail(obj_NSWSUser, "Exception in SSO-" + ex.InnerException.Message.ToString());
    //        ActionTrail(obj_NSWSUser, "Exception in SSO-" + ex.InnerException.InnerException.Message.ToString());
    //        // ActionTrail(obj_NSWSUser, "Exception in SSO-" + ex.InnerException.InnerException.InnerException.Message.ToString());

    //        ref_SSOAuthentication = null;

    //    }
    //    return messge;
    //}
    //private bool RedirectAPI(string accessToken, string swsId, string userName, ref HttpResponseMessage Responsemsg)
    //{
    //    try
    //    {
    //        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

    //        string redirectionUrl = "http://trg.cwc.gov.in/NOCAPTest/NSWS/NSWSToNOCAP.aspx?UserName=" + HttpUtility.UrlEncode(HttpContext.Current.Server.HtmlEncode(NOCAPExternalUtility.Encrypt(userName)));
    //        string stateId = string.Empty;
    //        //  string redirectionUrl = "http://cgwa-noc.gov.in/NSWS/NSWSToNOCAP.aspx?UserName=" + NOCAPExternalUtility.Encrypt(userName), stateId = string.Empty;

    //        HttpClient client = APIUtility.Method_Headers(accessToken, APIUrls.redirectionPPEURL);
    //        //this input data
    //        string registerUserJson = APIUtility.stringToJson(ConfigurationManager.AppSettings["DepartmentId"], ConfigurationManager.AppSettings["LicenseIdIND"], redirectionUrl, swsId, stateId);

    //        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Uri.EscapeUriString(client.BaseAddress.ToString()));
    //        request.Content = new StringContent(registerUserJson, Encoding.UTF8, "application/json");
    //        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
    //        HttpResponseMessage tokenResponse = client.PostAsync(Uri.EscapeUriString(client.BaseAddress.ToString()), request.Content).Result;
    //        Responsemsg = tokenResponse;            
    //        return tokenResponse.IsSuccessStatusCode;
    //    }
    //    catch (HttpRequestException ex)
    //    {
    //        return false;
    //    }

    //}
    #endregion



}
