using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Summary description for LicenseReqid
/// </summary>
public class LicenseReqid
{
    public LicenseReqid()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [JsonProperty("duplicateId")]
    public string[] DuplicateId { get; set; }
    [JsonProperty("savedId")]
    public string[] SavedId { get; set; }
}