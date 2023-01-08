using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DocumentRepository
/// </summary>
public class DocumentRepository
{
    public DocumentRepository()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [JsonProperty("swsId")]
    public string[] SwsId
    { get; set; }

   
}