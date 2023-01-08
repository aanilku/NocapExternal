using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PullDocument
/// </summary>
public class PullDocument
{
    public PullDocument()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [JsonProperty("contentId")]
    public string[] ContentId
    { get; set; }
}