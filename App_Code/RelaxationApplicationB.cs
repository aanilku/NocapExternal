using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relaxation
{
    public class RelaxationApplicationB  //:NOCAP.BLL.Common.CommonApplication
    {
        private long lng_iNDAppCode;
        //if appled by internal user then user code else 0. 0 means it is applied by Extenal user directly.
        private int int_applicantUserCode;
        private int? int_gWChargesDays;
        private decimal? dec_gWChargesQty;
        private int? int_mSMETypeCode;
        private int? int_waterChargeTypeCode;
        private decimal? dec_waterChargeAmount;
        private long lng_applicantExUserCode;
        private int int_applySubDistrictAreaCatKey;
        private int int_waterQualityCode;
        private int int_appTypeCode;
        private int int_appPurposeCode;
        private int int_appTypeCatCode;
        private string str_iNDName;
        private string str_referralLetterNo;
      //  private Relaxation.IndustrialNewProposedLocation obj_industrialNewProposedLocation;
        private string str_proUdaMcGp;
        private NOCAP.BLL.Common.Address obj_commAddress;

        private NOCAP.BLL.Common.PhoneNumber obj_commPhoneNumber;
        private NOCAP.BLL.Common.MobileNumber obj_CommMobileNumber;

        private string str_commEmailID;
        private NOCAP.BLL.Common.FaxNumber obj_commFaxNumber;

        private string str_salientFeatureOfIND;
        //redefine in inheritence
        //private NOCAP.BLL.Industrial.New.Application.IndustrialNewLandUseDetailOfExistingProposed obj_industrialNewLandUseDetailOfExistingProposed;

        private string str_drainageArea;
        NOCAP.BLL.Common.SourceOfAvailabilityOfSurfaceWater obj_SourceOfAvailabilityOfSurfaceWaterForIndustrialUse;
        private decimal? dec_avgAnnualRainfall;
        private string str_towVillWithIn2KM;

        private GroundWaterUtilizationForOption enu_GroundWaterUtilizationFor;
        private NOCAP.BLL.Common.NOCObtainForExistIndustry obj_NOCObtainForExistIndustry;

        private DateTime? dt_DateOfCommencement;
        private DateTime? dt_dateOfCommExpanIND;
        private long? lng_bhaTransRefNo;
        private DateTime? dt_bhaTransDated;
        public int? VerificationSN
        {
            get;
            set;
        }
        public DateTime? ECGWillegalFrom
        {
            get
           ;
            set
           ;
        }
        public DateTime? ECGWillegalTo
        {
            get
           ;
             set
           ;
        }
        public decimal? ECillegalQty
        {
            get
           ;
             set
           ;
        }
        public int? ECReasonCode
        {
            get
           ;
             set
           ;
        }
        public string ECReasonOther
        {
            get
           ;
             set
           ;
        }
       // public string OrderPaymentCode { get; set; }
       // public string BhaTransRefNoOnline { get; set; }
        //public DateTime? BhaTransDatedOnline { get; set; }
        public long? BharatTransReferanceNumber
        {
            get
            {
                return lng_bhaTransRefNo;
            }
            set
            {
                lng_bhaTransRefNo = value;
            }
        }
        public enum ECOption
        {
            Yes = 1,
            No = 2,
            NotDefined = 3
        }
        public ECOption ECReqFinally
        { get; set; }
        public ECOption ECRecFinally
        { get; set; }
        public decimal? ECAmout
        { get; set; }
        //public Master.WaterQuality.WaterQualityTypeOption WaterQualityType
        //{
        //    get;
        //    set;
        //}
        public int? WaterQualityCodeFinally
        {
            get
            ;
            set
            ;
        }
        public enum WaterChargeReqFinallyYesNo
        {
            Yes = 1,
            No = 2,
            NotDefine = 3
        }
        public WaterChargeReqFinallyYesNo WaterChargeReqFinally
        {
            get;
            set;
        }

        public DateTime? BharatTransDated
        {
            get
            {
                return dt_bhaTransDated;
            }
            set
            {
                dt_bhaTransDated = value;
            }
        }
        public DateTime? DateOfCommencement
        {
            get
            {
                return dt_DateOfCommencement;
            }
            set
            {
                dt_DateOfCommencement = value;
            }
        }


        public DateTime? DateOfExpansionOfProject
        {
            get
            {
                return dt_dateOfCommExpanIND;
            }
            set
            {
                dt_dateOfCommExpanIND = value;
            }
        }


       // private Relaxation.IndustrialNewWaterRequirementRecycledUses obj_IndustrialNewWaterRequirementRecycledUses;


        private int? int_noOfStructureExisting;
        private int? int_noOfStructureProposed;
        private HardCopyReqYesNo enu_HardCopyReq;
        public HardCopyReqYesNo HardCopyReq
        {
            get
            {
                return enu_HardCopyReq;
            }
            set
            {
                enu_HardCopyReq = value;
            }

        }
        public enum HardCopyReqYesNo
        {
            Yes = 1,
            No = 2
        }
        private AllowUpdateYesNo enu_AllowUpdate;


        public AllowUpdateYesNo AllowUpdate
        {
            get
            {
                return enu_AllowUpdate;
            }
            set
            {
                enu_AllowUpdate = value;
            }

        }
        public enum AllowUpdateYesNo
        {
            Yes = 1,
            No = 2
        }
        public enum MSMEYesNo
        {
            Yes = 1,
            No = 2,
            NotDefine = 3
        }
        private MSMEYesNo enu_MSME;
        private WaterChargeStatusYesNo enu_WaterChargeStatus;
        public WaterChargeStatusYesNo WaterChargeStatus
        { get { return enu_WaterChargeStatus; } set { enu_WaterChargeStatus = value; } }
        public enum WaterChargeStatusYesNo
        {
            Yes = 1,
            No = 2,
            NotDefine = 3
        }
        public enum WetLandAreaYesNo
        {
            Yes = 1,
            No = 2,
            NotDefine = 3
        }
        private WetLandAreaYesNo enu_WetLandAreaYesNo;
        public MSMEYesNo MSME
        {
            get
            {
                return enu_MSME;
            }
            set
            {
                enu_MSME = value;
            }
        }
        public WetLandAreaYesNo WetLandArea
        {
            get
            {
                return enu_WetLandAreaYesNo;
            }
            set
            {
                enu_WetLandAreaYesNo = value;
            }
        }

        //transfer these two-tranfered
        //private NOCAP.BLL.Industrial.New.Application.IndustrialNewExistingGroundWaterAbstractionStructure[] arr_IndustrialNewExistingGroundWaterAbstractionStructure;
        //private NOCAP.BLL.Industrial.New.Application.IndustrialNewProposedGroundWaterAbstractionStructure[] arr_IndustrialNewProposedGroundWaterAbstractionStructure;

        NOCAP.BLL.Common.GroundWaterAvailability obj_groundWaterAvailability;

        NOCAP.BLL.Common.RainwaterHarvestingAndArtificialRecharge obj_RainwaterHarvestingAndArtificialRecharge;


        NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance obj_EarlierAppliedGroundwaterClearance;


        NOCAP.BLL.Common.Undertaking obj_Undertaking;


        private SaveApplicationOption enu_SaveApplication;

        private int int_extraAttCode;
        private int int_bharatKoshRecieptAttCode;
        private int int_scannedINDAppAttCode;
        private int int_nonPollutingAttCode;
        private int int_iNDAttCode;

        private int int_signedDocAttCode;
        private int int_mSMEAttCode;
        private int int_wetlandAreaAttCode;
        private int int_absRestChargeAttCode;
        private int int_penaltyAttCode;
        private int int_impactAssOCSAttCode;
        private int int_auditReportAttCode;
        private int int_affidavitOtherMSMEAttCode;
        private int int_affidavitNonAvaAttCode;
        private int int_restChargeAttCode;
        private int int_consentAttCode;

        private string str_custMessage;



        public enum GroundWaterUtilizationForOption
        {
            NotDefined = 1,
            NewIndustry = 2,
            ExistingIndustry = 3,
            ExpansionProgramOfExitingIndustry = 4
        }
        public enum WaterChargeRecFinallyYesNo
        {
            Yes = 1,
            No = 2,
            NotDefine = 3
        }
        //public enum ECRecFinallyYesNo
        //{
        //    Yes = 1,
        //    No = 2,
        //    NotDefine = 3
        //}
       
        private WaterChargeRecFinallyYesNo enu_waterChargeRecFinally;
        public WaterChargeRecFinallyYesNo WaterChargeRecFinally
        {
            get { return enu_waterChargeRecFinally; }
            set { enu_waterChargeRecFinally = value; }
        }
        public enum SaveApplicationOption
        {
            Submitted = 1,
            SaveAsDraft = 2
        }
        private int? int_waterChargeTypeCodeFinally;
        private decimal? dec_gWChargeAmtFinally, dec_gWArearAmtFinally;
        public int? WaterChargeTypeCodeFinally
        {
            get
            {
                return int_waterChargeTypeCodeFinally;
            }
            set
            {
                int_waterChargeTypeCodeFinally = value;
            }
        }
        public decimal? GWChargeAmtFinally
        {
            get
            {
                return dec_gWChargeAmtFinally;
            }
            set
            {
                dec_gWChargeAmtFinally = value;
            }
        }
        public decimal? GWArearAmtFinally
        {
            get
            {
                return dec_gWArearAmtFinally;
            }
            set
            {
                dec_gWArearAmtFinally = value;
            }
        }



        //private string str_custMessage;
        bool disposed = false;

        public RelaxationApplicationB()
        {
            ApplicationCode = 0;
            ApplicantUserCode = 0;
            ApplicantExternalUserCode = 0;
            ApplySubDistrictAreaCategoryKey = 0;
            ApplicationTypeCode = 0;
            WaterQualityCode = 0;
            ApplicationPurposeCode = 0;
            ApplicationTypeCategoryCode = 0;
            NameOfIndustry = "";
            NameOfUDAAndMCAndGP = "";
            //ProposedLocation = new IndustrialNewProposedLocation();
            CommunicationAddress = new NOCAP.BLL.Common.Address();
            CommunicationMobileNumber = new NOCAP.BLL.Common.MobileNumber();
            CommunicationPhoneNumber = new NOCAP.BLL.Common.PhoneNumber();
            CommunicationEmailID = "";
            CommunicationFaxNumber = new NOCAP.BLL.Common.FaxNumber();
            SalientFeatureOfIndustrialActivity = "";
            //LandUseDetailOfExistingProposed = new IndustrialNewLandUseDetailOfExistingProposed();

            DrainageInTheArea = "";
          //  SourceOfAvaillabilityOfSurfaceWaterForIndustrialUse = new NOCAP.BLL.Common.SourceOfAvailabilityOfSurfaceWater();
            AverageAnnualRainfallInTheArea = null;
            TownshipVillageWithin2KM = "";
           // GroundWaterUtilizationFor = GroundWaterUtilizationForOption.NotDefined;
           // NOCObtainForExistIndustry = new BLL.Common.NOCObtainForExistIndustry();
            DateOfCommencement = null;
            //IndustrialNewWaterRequirementRecycledUsesDetail = new IndustrialNewWaterRequirementRecycledUses();
            NumberOfStructureExisting = null;
            NumberOfStructureProposed = null;
            GroundWaterAvailability = new NOCAP.BLL.Common.GroundWaterAvailability();
            RainwaterHarvestingAndArtificialRecharge = new NOCAP.BLL.Common.RainwaterHarvestingAndArtificialRecharge();
            EarlierAppliedGroundwaterClearance = new NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance();
            Undertaking = new NOCAP.BLL.Common.Undertaking();

            DateOfCommencement = null;
            DateOfExpansionOfProject = null;
        }



        public long ApplicationCode
        {
            get
            {
                return lng_iNDAppCode;
            }
            internal set
            {
                lng_iNDAppCode = value;
            }
        }

        public NOCAP.BLL.Common.Address CommunicationAddress
        {
            get
            {
                return obj_commAddress;
            }
            set
            {
                obj_commAddress = value;
            }
        }



        public NOCAP.BLL.Common.PhoneNumber CommunicationPhoneNumber
        {

            get
            {
                return obj_commPhoneNumber;
            }
            set
            {
                obj_commPhoneNumber = value;
            }

        }


        public NOCAP.BLL.Common.MobileNumber CommunicationMobileNumber
        {
            get
            {
                return obj_CommMobileNumber;
            }
            set
            {
                obj_CommMobileNumber = value;
            }
        }

        public NOCAP.BLL.Common.FaxNumber CommunicationFaxNumber
        {

            get
            {
                return obj_commFaxNumber;
            }
            set
            {
                obj_commFaxNumber = value;
            }


        }

        public int ApplicantUserCode
        {
            get
            {
                return int_applicantUserCode;
            }
            set
            {
                int_applicantUserCode = value;
            }
        }
        public int? GWChargesDays
        {
            get
            {
                return int_gWChargesDays;
            }
            set
            {
                int_gWChargesDays = value;
            }
        }
        public decimal? GWChargesQty
        {
            get
            {
                return dec_gWChargesQty;
            }
            set
            {
                dec_gWChargesQty = value;
            }
        }

        public int? MSMETypeCode
        {
            get
            {
                return int_mSMETypeCode;
            }
            set
            {
                int_mSMETypeCode = value;
            }
        }
        public int? WaterChargeTypeCode
        {
            get
            {
                return int_waterChargeTypeCode;
            }
            set
            {
                int_waterChargeTypeCode = value;
            }
        }
        public decimal? WaterChargeAmount
        { get { return dec_waterChargeAmount; } set { dec_waterChargeAmount = value; } }
        public long ApplicantExternalUserCode
        {
            get
            {
                return lng_applicantExUserCode;
            }
            set
            {
                lng_applicantExUserCode = value;
            }
        }


        public int ApplySubDistrictAreaCategoryKey
        {
            get
            {
                return int_applySubDistrictAreaCatKey;
            }
            set
            {
                int_applySubDistrictAreaCatKey = value;
            }
        }

        public int WaterQualityCode
        {
            get
            {
                return int_waterQualityCode;
            }
            set
            {
                int_waterQualityCode = value;
            }
        }


        public int ApplicationTypeCode
        {
            get
            {
                return int_appTypeCode;
            }
            set
            {
                int_appTypeCode = value;
            }
        }

        public int ApplicationPurposeCode
        {
            get
            {
                return int_appPurposeCode;
            }
            set
            {
                int_appPurposeCode = value;
            }
        }

        public int ApplicationTypeCategoryCode
        {
            get
            {
                return int_appTypeCatCode;
            }
            set
            {
                int_appTypeCatCode = value;
            }
        }
        public string ReferralLetterNo
        {
            get
            {
                return str_referralLetterNo;
            }
            set
            {
                str_referralLetterNo = value;
            }
        }
        public string NameOfIndustry
        {
            get
            {
                return str_iNDName;
            }
            set
            {
                str_iNDName = value;

            }

        }


        public string NameOfUDAAndMCAndGP
        {
            get
            {
                return str_proUdaMcGp;
            }
            set
            {
                str_proUdaMcGp = value;
            }
        }


        

        public string CommunicationEmailID
        {
            get
            {
                return str_commEmailID;
            }
            set
            {
                str_commEmailID = value;
            }

        }

        public string SalientFeatureOfIndustrialActivity
        {
            get
            {
                return str_salientFeatureOfIND;
            }
            set
            {
                str_salientFeatureOfIND = value;
 
            }

        }







        public string DrainageInTheArea
        {
            get
            {
                return str_drainageArea;
            }
            set
            {
                str_drainageArea = value;


            }

        }

        



        public decimal? AverageAnnualRainfallInTheArea
        {
            get
            {
                return dec_avgAnnualRainfall;
            }
            set
            {
                dec_avgAnnualRainfall = value;


            }

        }



        public string TownshipVillageWithin2KM
        {
            get
            {
                return str_towVillWithIn2KM;
            }
            set
            {
                str_towVillWithIn2KM = value;


            }

        }

         

        public int? NumberOfStructureExisting
        {
            get
            {
                return int_noOfStructureExisting;
            }
            set
            {
                int_noOfStructureExisting = value;
            }
        }
        public int? NumberOfStructureProposed
        {
            get
            {
                return int_noOfStructureProposed;
            }
            set
            {
                int_noOfStructureProposed = value;
            }
        }


        public NOCAP.BLL.Common.GroundWaterAvailability GroundWaterAvailability
        {
            get
            {
                return obj_groundWaterAvailability;
            }
            set
            {
                obj_groundWaterAvailability = value;
            }
        }

        public NOCAP.BLL.Common.RainwaterHarvestingAndArtificialRecharge RainwaterHarvestingAndArtificialRecharge
        {
            get
            {
                return obj_RainwaterHarvestingAndArtificialRecharge;
            }
            set
            {
                obj_RainwaterHarvestingAndArtificialRecharge = value;
            }
        }

        public NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance EarlierAppliedGroundwaterClearance
        {
            get
            {
                return obj_EarlierAppliedGroundwaterClearance;
            }
            set
            {
                obj_EarlierAppliedGroundwaterClearance = value;
            }
        }



        public NOCAP.BLL.Common.Undertaking Undertaking
        {
            get
            {
                return obj_Undertaking;
            }
            set
            {
                obj_Undertaking = value;
            }
        }


        public string CustumMessage
        {
            get
            {
                return str_custMessage;
            }
            internal set
            {
                str_custMessage = value;
            }
        }

        public SaveApplicationOption SaveApplicationAs
        {
            get
            {
                return enu_SaveApplication;
            }
            internal set
            {
                enu_SaveApplication = value;
            }
        }

        public int IndustrialNewExtraAttachmentCode
        {
            get
            {
                return int_extraAttCode;
            }
            internal set
            {
                int_extraAttCode = value;
            }
        }
        public int IndustrialNewBharatKoshRecieptAttachmentCode
        {
            get
            {
                return int_bharatKoshRecieptAttCode;
            }
            internal set
            {
                int_bharatKoshRecieptAttCode = value;
            }
        }
        public int IndustrialScannedINDAppAttCode
        {
            get
            {
                return int_scannedINDAppAttCode;
            }
            internal set
            {
                int_scannedINDAppAttCode = value;
            }
        }

        public int IndustrialNewAttachmentCode
        {
            get
            {
                return int_iNDAttCode;
            }
            internal set
            {
                int_iNDAttCode = value;
            }
        }
        public int SignedDocAttCode
        {

            get
            {
                return int_signedDocAttCode;
            }
            internal set
            {
                int_signedDocAttCode = value;
            }
        }
        public int MSMEAttCode
        {

            get
            {
                return int_mSMEAttCode;
            }
            internal set
            {
                int_mSMEAttCode = value;
            }
        }
        public int WetlandAreaAttCode
        {

            get
            {
                return int_wetlandAreaAttCode;
            }
            internal set
            {
                int_wetlandAreaAttCode = value;
            }
        }
        public int AbsRestChargeAttCode
        {

            get
            {
                return int_absRestChargeAttCode;
            }
            internal set
            {
                int_absRestChargeAttCode = value;
            }
        }
        public int PenaltyAttCode
        {

            get
            {
                return int_penaltyAttCode;
            }
            internal set
            {
                int_penaltyAttCode = value;
            }
        }
        public int ImpactAssOCSAttCode
        {

            get
            {
                return int_impactAssOCSAttCode;
            }
            internal set
            {
                int_impactAssOCSAttCode = value;
            }
        }
        public int AuditReportAttCode
        {

            get
            {
                return int_auditReportAttCode;
            }
            internal set
            {
                int_auditReportAttCode = value;
            }
        }
        public int AffidavitOtherMSMEAttCode
        {

            get
            {
                return int_affidavitOtherMSMEAttCode;
            }
            internal set
            {
                int_affidavitOtherMSMEAttCode = value;
            }
        }
        public int AffidavitNonAvaAttCode
        {

            get
            {
                return int_affidavitNonAvaAttCode;
            }
            internal set
            {
                int_affidavitNonAvaAttCode = value;
            }
        }
        public int RestChargeAttCode
        {

            get
            {
                return int_restChargeAttCode;
            }
            internal set
            {
                int_restChargeAttCode = value;
            }
        }
        public int ConsentAttCode
        {
            get { return int_consentAttCode; }
            set { int_consentAttCode = value; }
        }

        public int IndustrialNewNonPollutingAttCode
        {
            get
            {
                return int_nonPollutingAttCode;
            }
            internal set
            {
                int_nonPollutingAttCode = value;
            }
        }


















































        public void Dispose()
        {
            //destroy the university...
            //destroy the departments too... (composition)

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here. 
                //
                // Free other state (managed objects).

            }

            // Free any unmanaged objects here. 
            //
            // Free your own state (unmanaged objects).
            // Set large fields to null.

            //str_custMessage = null;
            disposed = true;
        }



    }
}
