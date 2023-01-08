using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LicensePullAPI
/// </summary>
public class LicensePullAPI
{
    public LicensePullAPI()
    {
        MinistryStateId = "M023";
        DepartmentId = "M023_D001";
        WingId = "M023_D001_WNG01";
    }
    [JsonProperty("ministryStateId")]
    public string MinistryStateId
    {
        get
        ;
        set
        ;
    }
    [JsonProperty("deptId")]
    public string DepartmentId
    {
        get
        ;
        set
        ;
    }
    [JsonProperty("wingId")]
    public string WingId
    {
        get
        ;
        set
        ;
    }
}