using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ApplicationLocation
/// </summary>
public class ApplicationLocation
{
    public ApplicationLocation()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public long ApplicationCode{get;set;}
    public string ApplicationNumber { get; set; }
    public string NOCNumber { get; set; }
    public string ApplicationName { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string AddressLine3 { get; set; }
    public string StateCode { get; set; }
    public string StateName { get; set; }
    public string DistrictCode { get; set; }
    public string DistrictName { get; set; }
    public string SubDistrictCode { get; set; }
    public string SubDistrictName { get; set; }
    public string VillageCode { get; set; }
    public string VillageName { get; set; }
    public string TownCode { get; set; }
    public string TownName { get; set; }
    public string Lat { get; set; }
    public string Long { get; set; }
}