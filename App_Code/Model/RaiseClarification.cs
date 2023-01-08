using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RaiseClarification
/// </summary>
public class RaiseClarification
{
    public RaiseClarification()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [JsonProperty("ministryId")]
    public string MinisteryId
    { get; set; }
    [JsonProperty("departmentId")]
    public string DepartmentId
    { get; set; }

    [JsonProperty("licenseIdentificationAttribute")]
    public licenseIdentificationAttributeList[] licenseIdentificationAttributeCollection
    { get; set; }
}