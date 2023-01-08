using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LicenseStatus
/// </summary>
public class LicenseStatus
{
    public LicenseStatus()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [JsonProperty("ministryStateId")]
    public string MinisteryId
    { get; set; }
    [JsonProperty("departmentId")]
    public string DepartmentId
    { get; set; }

    [JsonProperty("licenseStastusList")]
    public LicenseStastusList[] LicenseStastusListCollection
    { get; set; }
}