using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SelfComplianceResponse
/// </summary>
public class SelfComplianceResponse
{
    public class FormData
    {

        public string sc_id { get; set; }
        public long applicationCode { get; set; }
        public string projectName { get; set; }
        public string applicationNumber { get; set; }
        public string appliedFor { get; set; }
        public string typeOfProject { get; set; }
        public string nocNumber { get; set; }
        public string validityFrom { get; set; }
        public string validityTo { get; set; }
        public string abstractionDay { get; set; }
        public string dewateringDay { get; set; }
        public string abstractionYear { get; set; }
        public string dewateringYear { get; set; }
        public string nameOfApplicant { get; set; }
        public string unitAddLineOne { get; set; }
        public string unitAddLineTwo { get; set; }
        public string unitAddLineThree { get; set; }
        public string unitState { get; set; }
        public string unitStateCode { get; set; }
        public string unitDistrict { get; set; }
        public string unitDistrictCode { get; set; }
        public string unitSubDistrict { get; set; }
        public string unitSubDistrictCode { get; set; }
        public string unitVillage { get; set; }
        public string unitVillageCode { get; set; }
        public object comAddressCriSafe { get; set; }
        public string unitLatitude { get; set; }
        public string unitLongitude { get; set; }
        public string uniTowName { get; set; }
        public string uniTownCode { get; set; }
        public string nameOfAgencyType { get; set; }
        public string nameOfAegncy { get; set; }
        public string dateOfInspection { get; set; }
        public int copyOfSiteInspReportCon { get; set; }
        public string presentWidhrawalGW { get; set; }
        public string abstractionDayGWNoc { get; set; }
        public string abstractionYearGWNoc { get; set; }
        public string abstractionDayGWSelf { get; set; }
        public string abstractionYearGWSelf { get; set; }
        public string deWateringDayGwNoc { get; set; }
        public string deWateringYearGwNoc { get; set; }
        public string deWateringDayGwSelf { get; set; }
        public string deWateringYearGwSelf { get; set; }
        public string variationWidType { get; set; }
        public string variationWidDay { get; set; }
        public string variationWidYear { get; set; }
        public string abstractionDataTW { get; set; }
        public string structerNocExist { get; set; }
        public string structerSelfComExist { get; set; }
        public string structerNocProposed { get; set; }
        public string structerSelfComProposed { get; set; }
        public string NoFunctionalStructer { get; set; }
        public string geoTaggedPhotoCon { get; set; }
        public string waterMtrAbsStructer { get; set; }
        public string typeOfMetter { get; set; }
        public string telemetryInstalled { get; set; }
        public string NoOfFunctionalMeter { get; set; }
        public string anualCalWaterMtrGovt { get; set; }
        public string telemetry_username { get; set; }
        public string telemetry_password { get; set; }
        public string geoTagedWellAttached { get; set; }
        public string gwQualityReportAtach { get; set; }
        public string mineSepageQltyRep { get; set; }
        public string watrHarvesImplementArtifical { get; set; }
        public string waterSamplesGovtLab { get; set; }
        public string repSubTimeFrame { get; set; }
        public string typeOfStructure { get; set; }
        public string noOfStructure { get; set; }
        public string withinPremiseOutside { get; set; }
        public decimal quantumOfRecharge { get; set; }
        public string geotagedPhoRecharge { get; set; }
        public string noOfPiezometerInsNoc { get; set; }
        public string noOfPiezometerInsSelf { get; set; }
        public string piezometerWithDWLR { get; set; }
        public string piezometerWithDWLRS { get; set; }
        public string piezometerWithTelemeter { get; set; }
        public string piezometerWithTelmtrSelf { get; set; }
        public string noOfFunctionalPiezo { get; set; }
        public string piezoObserDWLRFittedPhoto { get; set; }
        public string moniterigDataSubNOC { get; set; }
        public string numOfObjWellKey { get; set; }
        public string gwMoniteringData { get; set; }
        public string telemetry_based_pz { get; set; }
        public string telemetry_based_pz_login_id { get; set; }
        public string telemetry_based_pz_password { get; set; }
        public string stpEtpInstalled { get; set; }
        public string noOfStpEtpInstalled { get; set; }
        public int capicityOfStpEtp { get; set; }
        public string quantmOfWasteWater { get; set; }
        public string sewagGeoTagPhoto { get; set; }
        public string quantumIndustProcess { get; set; }
        public string quantumGreenBelt { get; set; }
        public string quantumOthrUses { get; set; }
        public string subSelfComprepOnline { get; set; }
        public string voilationOfNocCond { get; set; }
        public string voilationOfNocRepoted { get; set; }
        public string otherCompNocCond { get; set; }
        public string otherCompNoc { get; set; }
        public object extraAttachments { get; set; }
        public string remarks { get; set; }
        public string submitted_at { get; set; }
        public string updated_at { get; set; }
        public string device_lat { get; set; }
        public string device_long { get; set; }
        public string device_area { get; set; }
        public string device_city { get; set; }
        public string device_state { get; set; }
        public string device_pincode { get; set; }
        public string userCode { get; set; }

    }
    public class Root
    {
        public FormData form_data { get; set; }
        public string response { get; set; }
        public string dataStatus { get; set; }
        public string message { get; set; }
    }
}