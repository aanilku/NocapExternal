using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClarificationResponse
/// </summary>
public class ClarificationResponse
{
    public ClarificationResponse()
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
    [JsonProperty("wingId")]
    public string WingId { get; set; }
}