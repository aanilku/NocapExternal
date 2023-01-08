using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Summary description for LicensePushAPIResponse
/// </summary>
public class LicensePushAPIResponse
{
    public LicensePushAPIResponse()
    {
        //
        // TODO: Add constructor logic here
        //
    }
   
    [JsonProperty("status")]
    public string Status { get; set; }
    [JsonProperty("message")]
    public string Message { get; set; }
    [JsonProperty("licenseReqid")]
    public LicenseReqid licenseReqid { get; set; }
}
