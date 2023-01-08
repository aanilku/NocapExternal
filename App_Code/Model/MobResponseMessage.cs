using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

/// <summary>
/// Summary description for MobResponseMessage
/// </summary>
public class MobResponseMessage: HttpRequestMessage
{
    public string error { get; set; }
    public string error_description { get; set; }
    
    public MobResponseMessage(string str_error="",string str_error_description="")
    {
        error = str_error;
        error_description = str_error_description;
    }
}