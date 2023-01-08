using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for licenseIdentificationAttributeList
/// </summary>
public class licenseIdentificationAttributeList
{
    public licenseIdentificationAttributeList()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [JsonProperty("licenseReqId")]
    public string LicenseReqId
    { get; set; }
    [JsonProperty("queryDesc")]
    public string QueryDesc
    { get; set; }
    [JsonProperty("queryType")]
    public string QueryType
    { get; set; }
}