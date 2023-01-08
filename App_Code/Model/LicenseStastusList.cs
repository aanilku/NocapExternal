using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LicenseStastusList
/// </summary>
public class LicenseStastusList
{
    public LicenseStastusList()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [JsonProperty("licenseReqNum")]
    public string LicenseReqNum
    { get; set; }
    [JsonProperty("licenseStatus")]
    public string LicenseStatus
    { get; set; }
}