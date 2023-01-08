using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net;

using System.Net.Http.Headers;

/// <summary>
/// Summary description for APIUtility
/// </summary>
public class APIUtility
{
    public static HttpClient Method_Headers(string accessToken, string endpointURL)
    {
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
        HttpClient client = new HttpClient(handler);

        try
        {
            client.BaseAddress = new Uri(endpointURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("authorization", "Bearer " + accessToken);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return client;

    }

    public static HttpClient Method_Headers(string accessId, string accessSecret, string apiKey, string endpointURL, string str_mediaType)
    {
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
        HttpClient client = new HttpClient(handler);

        try
        {
            client.BaseAddress = new Uri(endpointURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(str_mediaType));
            client.DefaultRequestHeaders.Add("access-id", accessId);
            client.DefaultRequestHeaders.Add("access-secret", accessSecret);
            client.DefaultRequestHeaders.Add("api-key", apiKey);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return client;

    }

    public static HttpClient Method_Headers(string endpointURL)
    {
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
        HttpClient client = new HttpClient(handler);

        try
        {
            client.BaseAddress = new Uri(endpointURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return client;

    }
    public static string stringToJson(string departmentId, string licenseId, string redirectionUrl, string swsId, string stateId)
    {
        return "{"

        + "\"departmentId\": \"" + departmentId + "\","

        + "\"licenseId\": \"" + licenseId + "\","

        + "\"redirectionUrl\": \"" + redirectionUrl + "\","
         + "\"swsId\": \"" + swsId + "\","
        + "\"stateId\": \"" + stateId + "\""

 + "}";



    }
    public static string stringToJson(string api_access_token, string applicationNumber, string imgType, long ApplicationCode)
    {
        return "{"

        + "\"api_access_token\": \"" + api_access_token + "\","

        + "\"applicationNumber\": \"" + applicationNumber + "\","

          + "\"imgType\": \"" + imgType + "\","

           + "\"ApplicationCode\": \"" + ApplicationCode + "\","



 + "}";



    }
    public static string stringToJson(string api_access_token, string applicationCode, string applicationNumber)
    {
        return "{"

        + "\"api_access_token\": \"" + api_access_token + "\","

        + "\"applicationCode\": \"" + applicationCode + "\","

        + "\"applicationNumber\": \"" + applicationNumber + "\","
        
    

 + "}";



    }
    public static string stringToJson(string ministryId, string departmentId)
    {
        return "{"

        + "\"ministryId\": \"" + ministryId + "\","

        + "\"departmentId\": \"" + departmentId + "\","
 + "}";



    }
}