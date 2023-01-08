using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MobSelfCompliance
/// </summary>
public class MobSelfCompliance
{
    public MobSelfCompliance()
    {
        CustumMessage = "Self Compliance not found";
        Status = 0;
    }
    public string AppNo { get; set; }
    public string ProjectName { get; set; }
    public string AppliedFor { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string AddressLine3 { get; set; }
    public string StateName { get; set; }
    public string DistrictName { get; set; }
    public string SubDistrictName { get; set; }
    public string Village { get; set; }
    public string Town { get; set; }    
    public string NOCValidatation { get; set; }
    public decimal QtyPerDay { get; set; }
    public decimal QtyPerYear { get; set; }
    public string CatOfBlock { get; set; }
    public enum SortingField
    {
        NoSorting = 0,
        ApplicationCode = 1,
        ComplianceSubmitDate = 3,
        ComplianceSubmitDateDesc = 4
    }
    public long ApplicationCode { get; set; }
    public int? ApplicantUserCode { get; set; }
    public long? ApplicantExUserCode { get; set; }
    public DateTime? ComplianceSubmitDate { get; set; }
    public decimal? PresentGroundWaterReqInDay { get; set; }
    public decimal? PresentGroundWaterReqInYear { get; set; }
    public string WaterMetFittedDesc { get; set; }
    public int WaterMeterFittedAttachmentCode { get; internal set; }
    public string GWQualityPreMonsoonDesc
    {
        get
        ;
        set
        ;
    }
    public int GWQualityPreMonsoonAttachmentCode
    {
        get
        ;
        internal set
        ;
    }
    public int? RWHArtificialRechargeNo
    {
        get
        ;
        set
        ;
    }
    public decimal? RWHArtificialRechargeCapacity
    {
        get
        ;
        set
        ;
    }
    public string RWHArtificialRechargeDesc
    {
        get
        ;
        set
        ;
    }
    public int RWHArtificialRechargeAttachmentCode
    {
        get
        ;
        internal set
        ;
    }
    public int PhotoGraphAttachmentCode
    {
        get
        ;
        internal set
       ;
    }
    public string PiezoAWLRTelemetryDesc
    {
        get
       ;
        set
        ;
    }
    public int PiezoAWLRTelemetryAttachmentCode
    {
        get
        ;
        internal set
        ;
    }
    public string RecycleReuseDesc
    {
        get
        ;
        set
        ;
    }
    public decimal? RecycleReuseInDay
    {
        get
        ;
        set
       ;
    }
    public decimal? RecycleReuseInYear
    {
        get
        ;
        set
        ;
    }
    public int RecycleReuseAttachmentCode
    {
        get
        ;
        internal set
        ;
    }
  
    #region Enum
    public NOCAP.BLL.Common.CommonEnum.YesNoOption PresentGroundWaterReq { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption WaterMetFitted { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption GWQualityPreMonsoon
    {
        get
        ;
        set
        ;
    }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption RWHArtificialRecharge
    {
        get
        ;
        set
       ;
    }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption PhotoGraph
    {
        get
        ;
        set
       ;
    }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption PiezoAWLRTelemetry
    {
        get
        ;
        set
        ;
    }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption RecycleReuse{get;set;}
    public NOCAP.BLL.Common.CommonEnum.YesNoOption ActionTakenReportWithinOneYear{get;set;}
    public NOCAP.BLL.Common.CommonEnum.YesNoOption InspectionReport { get; set; }
    public NOCAP.BLL.Common.CommonEnum.VariationInQuantum PreGroundWaterAnyVari { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption AbstrDataSubmittedTW { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption TelemInstalled { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption AnnuCalibOfWM { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption PhotoWellFittedWithWM { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption GWQuality { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption MineSeepageQuality { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption WaterSampleAnalyzed { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption GWReportWithinTime { get; set; }

    public NOCAP.BLL.Common.CommonEnum.YesNoOption WithOutPremises { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption StructPhoto { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption AbstStructFittedWithWM { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption GeoPiezoAWLRTelem { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption MoniDataSubmitted { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption PiezometerDWLRTelemetry { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption SubSCWithinTimeFrame { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption WaterAuditInspection { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption WaterAuditReport { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption ImpactAssementReport { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption ImpactAssementAIRReport { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption AnyViolationNOCCondi { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption AnyOtherCompliances { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption AdoptionVillages
    {
        get
        ;
        set
        ;
    }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption PlantationTrees
    {
        get
        ;
        set
       ;
    }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption SchoolSanitationDrinkingWater
    {
        get
        ;
        set
        ;
    }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption Submitted
    {
        get
       ;
        set
        ;
    }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption GeoPhotoOfSTP { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption PhotoRechargeStruct { get; set; }
    #endregion
    public int Status { get; set; }
    #region Add New Feilds
    //Add New Feilds
    public decimal? PreSelfsentGroundWaterReqInDay { get; set; }
    public decimal? PreSelfsentGroundWaterReqInYear { get; set; }
    public int? AbstraStructExistingAsPerNOC { get; set; }
    public int? AbstraStructProposedAsPerNOC { get; set; }
    public int? NumberOfFunMeter { get; set; }
    #region New Enum
    public NOCAP.BLL.Common.CommonEnum.YesNoOption Verified
    {
        get
        ;
        set
        ;
    }
    public NOCAP.BLL.Master.SelfCompTypeOfARStructureDetail[] SelfCompTypeOfARStructureDetailCollection { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption WaterAuditInspectionApplicable { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption WaterAuditInspectionAsPerNOC { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption WaterAuditReportApplicable { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption ImpactAssementReportApplicable { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption ImpactAssementReportSubmitted { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption GroundWaterMonitoringData { get; set; }
    public NOCAP.BLL.Common.CommonEnum.YesNoOption RainWaterHarv { get; set; }
    #endregion
    public int? NoOfSelfPiezoTelem { get; set; }
    public int? NoOfSelfPiezo { get; set; }
    public int? NoOfPiezoDWLRTelem { get; set; }
    public int? NoOfSelfPiezoDWLRTelem { get; set; }
    public int GroundWaterMonitoringDataAttCode { get; set; }
    public int MiningSeepageAttCode { get; set; }
    public int AnnualCalibrationAttCode { get; set; }
    public int? VerifiedBy
    {
        get
       ;
        internal set
        ;
    }
    public DateTime? VerifiedOn
    {
        get
       ;
        internal set
        ;
    }
    #endregion

    //Below
    public string ActionTakenReportWithinOneYearDesc
    {
        get
        ;
        set
       ;
    }
    public string NOCNo { get; set; }
    public DateTime? ValidityStartDate { get; set; }
    public DateTime? ValidityEndDate { get; set; }
    public decimal? GroundWaterAbsDayAppr { get; set; }
    public decimal? GroundWaterDewDayAppr { get; set; }
    public decimal? GroundWaterAbsYearAppr { get; set; }
    public decimal? GroundWaterDewYearAppr { get; set; }
    public int? InspectionAgencyCode { get; set; }
    public string AnyOtherAgency { get; set; }
    public DateTime? DateOfInspection { get; set; }
    public decimal? PreGroundWaterDewReqInDay { get; set; }
    public decimal? PreGroundWaterDewReqInYear { get; set; }
    public decimal? PreGroundWaterAnyVariReqInDay { get; set; }
    public decimal? PreGroundWaterAnyVariReqInYear { get; set; }
    public int? AbstraStructExisting { get; set; }
    public int? AbstraStructProposed { get; set; }
    public int? NoAbsDewStrucAtPresent { get; set; }
    public int? FuncAbstStruct { get; set; }
    public int? MeterTypeCode { get; set; }
    public int? TypeOfAbstStructCode { get; set; }
    public int? NumberOfStruct { get; set; }
    public decimal? QuantOfRecharge { get; set; }
    public int? NoOfPiezo { get; set; }
    public int? NoOfPiezoDWLR { get; set; }
    public int? NoOfPiezoTelem { get; set; }
    public int? NoOfObserwell { get; set; }
    public int? NoOfFunctPiezo { get; set; }
    public int? NoOfSTPETP { get; set; }
    public int? CapOfSTPETP { get; set; }
    public decimal? QuanttreatWasteWater { get; set; }
    public decimal? QuanttreatWasteWaterIND { get; set; }
    public decimal? QuanttreatWasteWaterINDGreen { get; set; }
    public decimal? QuanttreatWasteWaterINDOther { get; set; }
    public string AnyViolationNOCCondiDesc { get; set; }
    public string AuditAgency { get; set; }
    public DateTime? DateOfInspectionAudit { get; set; }
    public string AnyOtherCompliancesDesc { get; set; }
    public int WellFittedWithWaterFlowMeterAttCode { get; set; }
    public int GeoRainWaterHarvRechAttCode { get; set; }
    public int GroundWaterQualityAttCode { get; set; }
    public int GroundwaterAbstractionDataAttCode { get; set; }
    public int NOCAttCode { get; set; }
    public int CopySiteInspectionAttCode { get; set; }
    public int PhotoETPSTPAttCode { get; set; }
    public int WaterAuditAttCode { get; set; }
    public int IARModelingAttCode { get; set; }
    public int GroundwaterMonitoringAttCode { get; set; }
    public int ExtraAttCode { get; set; }
    public string Remarks
    {
        get
        ;
        set
        ;
    }
    public string AdoptionVillagesDesc
    {
        get
        ;
        set
        ;
    }
    public int AdoptionVillageAttachmentCode
    {
        get
        ;
        internal set
        ;
    }
    public string PlantationTreesDesc
    {
        get
        ;
        set
        ;
    }
    public int PlantationTreeAttachmentCode
    {
        get
        ;
        internal set
        ;
    }
    public string SchoolSanitationDrinkingWaterDesc
    {
        get
        ;
        set
        ;
    }
    public int SchoolSanitationDrinkingWaterAttachmentCode
    {
        get
       ;
        internal set
        ;
    }
    public int SelfComplianceAttachmentCode
    {
        get
        ;
        internal set
        ;
    }
    public int AttachmentCode
    {
        get
        ;
        internal set
        ;
    }
    public DateTime? CreatedOnByUC
    {
        get
       ;
        internal set
        ;
    }
    public int? CreatedByUC
    {
        get
        ;
        internal set
       ;
    }
    public DateTime? CreatedOnByExUC
    {
        get
       ;
        internal set
        ;
    }
    public long? CreatedByExUC
    {
        get
        ;
        set
        ;
    }
    public DateTime? SubmittedOnByUC
    {
        get
        ;
        internal set
        ;
    }
    public int? SubmittedByUC
    {
        get
        ;
        internal set
        ;
    }
    public DateTime? SubmittedOnByExUC
    {
        get
        ;
        internal set
        ;
    }
    public long? SubmittedByExUC
    {
        get
        ;
        internal set
        ;
    }
    public DateTime? ModifiedOnByUC
    {
        get
        ;
        internal set
;
    }
    public int? ModifiedByUC
    {
        get
        ;
        set
       ;
    }
    public DateTime? ModifiedOnByExUC
    {
        get
        ;
        internal set
        ;
    }
    public long? ModifiedByExUC
    {
        get
        ;
        set
        ;
    }
    public string CustumMessage
    {
        get
        ;
        internal set
       ;
    }

    //public NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] SelfComplianceAttachment { get; set; }
    //public NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] PhotographsAttachment { get; set; }
    

}