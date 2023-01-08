using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LicensePushAPI
/// </summary>
public class LicensePushAPI
{
    public LicensePushAPI()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [JsonProperty("licenseId")]
    public string LicenseId
    { get; set; }
    [JsonProperty("licenseVer")]
    public string LicenseVer
    { get; set; }
    [JsonProperty("swsId")]
    public string SWSId
    { get; set; }
    [JsonProperty("investorReqId")]
    public string InvestorReqId
    { get; set; }
    [JsonProperty("licenseReqDate")]
    public string LicenseReqDate
    { get; set; }
    [JsonProperty("ministeryId")]
    public string MinisteryId
    { get; set; }
    [JsonProperty("departmentId")]
    public string DepartmentId
    { get; set; }
    public LicensePushAPI[] Collection
    { get; set; }
}