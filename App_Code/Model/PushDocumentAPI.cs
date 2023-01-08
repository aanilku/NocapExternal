using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for PushDocumentAPI
/// </summary>
public class PushDocumentAPI
{


    [JsonProperty("documentId")]
    public string DocumentID
    { get; set; }

    [JsonProperty("documentName")]
    public string DocumentName
    { get; set; }

    
    [JsonProperty("approvalId")]
    public string ApprovalID
    { get; set; }
    [JsonProperty("swsId")]
    public string SWSId
    { get; set; }
    [JsonProperty("investorReqId")]
    public string InvestorReqId
    { get; set; }

    
    [JsonProperty("mnstryDprtmntId")]
    public string MinisteryDepartmentId
    { get; set; }
   
  


    public PushDocumentAPI()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}