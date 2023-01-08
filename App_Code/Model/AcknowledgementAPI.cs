using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
/// <summary>
/// Summary description for AcknowledgementAPI
/// </summary>
public class AcknowledgementAPI
{
    public AcknowledgementAPI()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [JsonProperty("ackType")]
    public string AckType
    { get; set; }
    //[JsonProperty("requests")]
    //public string Requests
    //{ get; set; }
    [JsonProperty("ministryId")]
    public string MinistryId
    { get; set; }
    [JsonProperty("deptID")]
    public string DeptID
    { get; set; }

    public AcknowledgementAPI[] Collection
    { get; set; }
    [JsonProperty("requests")]
    public string[] Requests
    { get; set; }

}