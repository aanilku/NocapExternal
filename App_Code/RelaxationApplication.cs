using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Relaxation
{
    public class RelaxationApplication : Relaxation.RelaxationApplicationB
    { 

        //private Relaxation.IndustrialNewLandUseDetailOfExistingProposed obj_industrialNewLandUseDetailOfExistingProposed;


        //private Relaxation.IndustrialNewExistingGroundWaterAbstractionStructure[] arr_IndustrialNewExistingGroundWaterAbstractionStructure;
        //private Relaxation.IndustrialNewProposedGroundWaterAbstractionStructure[] arr_IndustrialNewProposedGroundWaterAbstractionStructure;

        private Relaxation.RelaxationApplication[] arr_RelaxationApplicationCollection;

        private SubmittedTypeOption enu_SubmittedType;
        private EligibleForExemptionLetterOption enu_EligibleForExemptionLetter;
        private WorkFlowRequiredOption enu_WorkFlowRequired;
        private VerificationRequiredOption enu_VerificationRequired;

        private int int_latestAppStatusCode;

        private string str_iNDAppNo;
        private string str_iNDAppNoOld;
        private int? int_evaluationSN;
        private decimal dec_netGroundWaterReq;
        // Added on 16/3/2015
        private int? int_screningCommitteeSN;
        private int? int_DLECSN;
        private int? int_presentCalledSN;
        private int int_referBackSN;
       // private NOCAP.BLL.Common.VerificationInfo obj_VerificationInfo;

        //record created first time
        private DateTime? dt_createdOnByUC;
        private int? int_createdByUC;
        private DateTime? dt_createdOnExUC;
        private long? lng_createdByExUC;

        
        private DateTime? dt_submittedOnByUC;
        private int? int_submittedByUC;
        private DateTime? dt_submittedOnByExUC;
        private long? lng_submittedByExUC;

        //modified
        private DateTime? dt_modifiedOnByUC;
        private int? int_modifiedByUC;
        private DateTime? dt_modifiedOnByExUC;
        private long? lng_modifiedByExUC;


        //Added on 23/2/2015
        private VerificationProcessingStageOption enu_VerificationProcessingStage;
        private VerificationProcessingStageCompleteOption enu_VerificationProcessingStageComplete;
        private ApplicationProcessingStageOption enu_ApplicationProcessingStage;
        private ApplicationProcessingStageCompleteOption enu_ApplicationProcessingStageComplete;
        private NOCProcessingStageOption enu_NOCProcessingStage;
        private NOCProcessingStageCompleteOption enu_NOCProcessingStageComplete;
        private ApplicationDisbursementStageOption enu_ApplicationDisbursementStage;
        private ApplicationDisbursementStageCompleteOption enu_ApplicationDisbursementStageComplete;
        private WorkFlowCompleteOption enu_WorkFlowComplete;
        private FileCloseOption enu_FileClose;

        //Added on 28/8/2015
        private EligibleForRenewOption enu_EligibleForRenew;
        private AllowFinallyForRenewOption enu_AllowFinallyForRenew;
        private long? lng_rewAppCodeFinally;
        private long? lng_latestRenewAppCode;
        private long? lng_latestRenewIssLetterAppCode;
        private long? lng_latestRenewNOCAppCode;
        private RenewAppliedOption enu_RenewApplied;
        private RenewedOption enu_Renewed;
        // private IsEligibleForApplyRenewSADApplicationOption enu_IsEligibleForApplyRenewSADApplication;
        private long? lng_renSADAppCode;
        //start Properties for NOC
        private string str_nocNumber;
        private string str_nocNumberOld;
        private long? lng_nocSN;
        //End Properties for NOC

        private decimal? dec_payReqAmt;
        private NOCAP.BLL.Common.PaymentRequirementOption.PayReqOption enu_PayReq;
        private NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption enu_PayAmtRecFinally;

        private int? int_nOCProformaSN;


        
        public decimal? PayReqAmt
        {
            get
            {
                return dec_payReqAmt;
            }
            set
            {
                dec_payReqAmt = value;
            }
        }



        public NOCAP.BLL.Common.PaymentRequirementOption.PayReqOption PayReq
        {
            get
            {
                return enu_PayReq;
            }
            internal set
            {
                enu_PayReq = value;
            }
        }

        public NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption PayAmtRecFinally
        {
            get
            {
                return enu_PayAmtRecFinally;
            }
            internal set
            {
                enu_PayAmtRecFinally = value;
            }

        }

        public int? NOCProformaSN
        {
            get
            {
                return int_nOCProformaSN;
            }
            set
            {
                int_nOCProformaSN = value;
            }
        }
        public string NOCNumber
        {
            get
            {
                return str_nocNumber;
            }
            set
            {
                str_nocNumber = value;
            }
        }

        public string NOCNumberOld
        {
            get
            {
                return str_nocNumberOld;
            }
            set
            {
                str_nocNumberOld = value;
            }
        }

        public long? NOCSN
        {
            get
            {
                return lng_nocSN;
            }
            set
            {
                lng_nocSN = value;
            }
        }




        //Adedd on 16/3/2015
        public int? ScreningCommitteeSN
        {
            get
            {
                return int_screningCommitteeSN;
            }
            set
            {
                int_screningCommitteeSN = value;
            }
        }
        public int? DLECSN
        {
            get
            {
                return int_DLECSN;
            }
            set
            {
                int_DLECSN = value;
            }
        }
        public int? PresentationCalledSerialNumber
        {
            get
            {
                return int_presentCalledSN;
            }
            set
            {
                int_presentCalledSN = value;
            }
        }
        public int ReferBackSN
        {
            get
            {
                return int_referBackSN;
            }
            set
            {
                int_referBackSN = value;
            }
        }
        public decimal NetGroundWaterReq
        {
            get
            {
                return dec_netGroundWaterReq;
            }
            set
            {
                dec_netGroundWaterReq = value;
            }
        }

        public RelaxationApplication()
            : base()
        {
            NetGroundWaterReq = 0;
           // VerificationDetail = new NOCAP.BLL.Common.VerificationInfo();

           // LandUseDetailOfExistingProposed = new IndustrialNewLandUseDetailOfExistingProposed();
            CreatedOnByUC = null;
            CreatedByUC = null;
            CreatedOnByExUC = null;
            CreatedByExUC = null;
            SubmittedOnByUC = null;
            SubmittedByUC = null;
            SubmittedOnByExUC = null;
            SubmittedByExUC = null;
            ModifiedOnByUC = null;
            ModifiedByUC = null;
            ModifiedOnByExUC = null;
            ModifiedByExUC = null;

           // VerificationDetail = new NOCAP.BLL.Common.VerificationInfo();

            //IsEligibleForApplyRenewSADApplication = IsEligibleForApplyRenewSADApplicationOption.No;
            RenewSADApplicationCode = null;

           // BhaTransDated = null;
        }


        public enum SortingFieldForAttachment
        {

            NoSorting = 0,
            ApplicationCode = 1,
            AttachmentCode = 2,
            AttachmentCodeSerialNumber = 3,
            AttachmentName = 4,
            AttachmentPath = 5
        }

        public enum NOCObtainForExistINDOption
        {
            NotDefined = 0,
            Yes = 1,
            No = 2,
        }
        public enum SortingField
        {

            NoSorting = 0,
            ApplicationCode = 1,
        }
        public enum SubmittedTypeOption
        {
            Online = 1,
            Archival = 2
        }
        public enum SubmittedOption
        {
            Yes = 1,
            No = 2,
            NotDefine=3
        }
        public enum EligibleForExemptionLetterOption
        {
            Yes = 1,
            No = 2
        }
        public enum WorkFlowRequiredOption
        {
            Yes = 1,
            No = 2
        }
        public enum VerificationRequiredOption
        {
            Yes = 1,
            No = 2
        }



        //Added on 23/2/2015

        public enum VerificationProcessingStageOption
        {
            Yes = 1,
            No = 2,
            NotDefined = 3
        }
        public enum VerificationProcessingStageCompleteOption
        {
            Yes = 1,
            No = 2,
            NotDefined = 3
        }
        public enum ApplicationProcessingStageOption
        {
            Yes = 1,
            No = 2,
            NotDefined = 3
        }
        public enum ApplicationProcessingStageCompleteOption
        {
            Yes = 1,
            No = 2,
            NotDefined = 3
        }
        public enum NOCProcessingStageOption
        {
            Yes = 1,
            No = 2,
            NotDefined = 3
        }
        public enum NOCProcessingStageCompleteOption
        {
            Yes = 1,
            No = 2,
            NotDefined = 3
        }
        public enum ApplicationDisbursementStageOption
        {
            Yes = 1,
            No = 2,
            NotDefined = 3
        }
        public enum ApplicationDisbursementStageCompleteOption
        {
            Yes = 1,
            No = 2,
            NotDefined = 3
        }
        public enum WorkFlowCompleteOption
        {
            Yes = 1,
            No = 2,
            NotDefined = 3
        }
        public enum FileCloseOption
        {
            Yes = 1,
            No = 2,
            NotDefined = 3
        }

        public enum EligibleForRenewOption
        {
            Yes = 1,
            No = 2,
            NotDefined = 3
        }

        public enum AllowFinallyForRenewOption
        {
            Yes = 1,
            No = 2,
            NotDefined = 3
        }
        public enum RenewAppliedOption
        {
            Yes = 1,
            No = 2,
            NotDefined = 3
        }
        public enum RenewedOption
        {
            Yes = 1,
            No = 2,
            NotDefined = 3
        }
        
        public SubmittedTypeOption SubmittedType
        {
            get
            {
                return enu_SubmittedType;
            }
            internal set
            {
                enu_SubmittedType = value;
            }
        }


        public EligibleForExemptionLetterOption EligibleForExemptionLetter
        {
            get
            {
                return enu_EligibleForExemptionLetter;
            }
            internal set
            {
                enu_EligibleForExemptionLetter = value;
            }
        }
        public WorkFlowRequiredOption WorkFlowRequired
        {
            get
            {
                return enu_WorkFlowRequired;
            }
            internal set
            {
                enu_WorkFlowRequired = value;
            }
        }
        public VerificationRequiredOption VerificationRequired
        {
            get
            {
                return enu_VerificationRequired;
            }
            internal set
            {
                enu_VerificationRequired = value;
            }
        }



        //Added on 23/2/2015
        public VerificationProcessingStageOption VerificationProcessingStage
        {
            get
            {
                return enu_VerificationProcessingStage;
            }
            internal set
            {
                enu_VerificationProcessingStage = value;
            }
        }
        public VerificationProcessingStageCompleteOption VerificationProcessingStageComplete
        {
            get
            {
                return enu_VerificationProcessingStageComplete;
            }
            internal set
            {
                enu_VerificationProcessingStageComplete = value;
            }
        }
        public ApplicationProcessingStageOption ApplicationProcessingStage
        {
            get
            {
                return enu_ApplicationProcessingStage;
            }
            internal set
            {
                enu_ApplicationProcessingStage = value;
            }
        }
        public ApplicationProcessingStageCompleteOption ApplicationProcessingStageComplete
        {
            get
            {
                return enu_ApplicationProcessingStageComplete;
            }
            internal set
            {
                enu_ApplicationProcessingStageComplete = value;
            }
        }
        public NOCProcessingStageOption NOCProcessingStage
        {
            get
            {
                return enu_NOCProcessingStage;
            }
            internal set
            {
                enu_NOCProcessingStage = value;
            }
        }
        public NOCProcessingStageCompleteOption NOCProcessingStageComplete
        {
            get
            {
                return enu_NOCProcessingStageComplete;
            }
            internal set
            {
                enu_NOCProcessingStageComplete = value;
            }
        }
        public ApplicationDisbursementStageOption ApplicationDisbursementStage
        {
            get
            {
                return enu_ApplicationDisbursementStage;
            }
            internal set
            {
                enu_ApplicationDisbursementStage = value;
            }
        }
        public ApplicationDisbursementStageCompleteOption ApplicationDisbursementStageComplete
        {
            get
            {
                return enu_ApplicationDisbursementStageComplete;
            }
            internal set
            {
                enu_ApplicationDisbursementStageComplete = value;
            }
        }
        public WorkFlowCompleteOption WorkFlowComplete
        {
            get
            {
                return enu_WorkFlowComplete;
            }
            internal set
            {
                enu_WorkFlowComplete = value;
            }
        }
        public FileCloseOption FileClose
        {
            get
            {
                return enu_FileClose;
            }
            internal set
            {
                enu_FileClose = value;
            }
        }

        public EligibleForRenewOption EligibleForRenew
        {
            get
            {
                return enu_EligibleForRenew;
            }
            internal set
            {
                enu_EligibleForRenew = value;
            }
        }
        public AllowFinallyForRenewOption AllowFinallyForRenew
        {
            get
            {
                return enu_AllowFinallyForRenew;
            }
            internal set
            {
                enu_AllowFinallyForRenew = value;
            }
        }
        public long? RewAppCodeFinally
        {
            get
            {
                return lng_rewAppCodeFinally;
            }
            set
            {
                lng_rewAppCodeFinally = value;
            }
        }

        public long? LatestRenewAppCode
        {
            get
            {
                return lng_latestRenewAppCode;
            }
            set
            {
                lng_latestRenewAppCode = value;
            }
        }

        public long? LatestRenewIssLetterAppCode
        {
            get
            {
                return lng_latestRenewIssLetterAppCode;
            }
            set
            {
                lng_latestRenewIssLetterAppCode = value;
            }
        }
        public long? LatestRenewNOCAppCode
        {
            get
            {
                return lng_latestRenewNOCAppCode;
            }
            set
            {
                lng_latestRenewNOCAppCode = value;
            }
        }
        public RenewAppliedOption RenewApplied
        {
            get
            {
                return enu_RenewApplied;
            }
            internal set
            {
                enu_RenewApplied = value;
            }
        }
        public RenewedOption Renewed
        {
            get
            {
                return enu_Renewed;
            }
            internal set
            {
                enu_Renewed = value;
            }
        }
        
        public long? RenewSADApplicationCode
        {
            get
            {
                return lng_renSADAppCode;
            }
            set
            {
                lng_renSADAppCode = value;
            }
        }

        public int LatestApplicationStatusCode
        {
            get
            {
                return int_latestAppStatusCode;
            }
            internal set
            {
                int_latestAppStatusCode = value;
            }
        }


        //remove
        public string ApplicationNumber
        {
            get
            {
                return str_iNDAppNo;
            }
            internal set
            {
                str_iNDAppNo = value;

            }
        }

        public string IndustrialOldApplicationNumber
        {
            get
            {
                return str_iNDAppNoOld;
            }
            set
            {

                str_iNDAppNoOld = value;
            }
        }

        public int? EvaluationSN
        {
            get
            {
                return int_evaluationSN;
            }
            internal set
            {
                int_evaluationSN = value;
            }
        }

        

        public DateTime? CreatedOnByUC
        {
            get
            {
                return dt_createdOnByUC;
            }
            internal set
            {
                dt_createdOnByUC = value;
            }
        }

        public int? CreatedByUC
        {
            get
            {
                return int_createdByUC;
            }
            internal set
            {
                int_createdByUC = value;
            }
        }

        public DateTime? CreatedOnByExUC
        {
            get
            {
                return dt_createdOnExUC;
            }
            internal set
            {
                dt_createdOnExUC = value;
            }
        }
        public long? CreatedByExUC
        {
            get
            {
                return lng_createdByExUC;
            }
            set
            {
                lng_createdByExUC = value;
            }
        }

      

        public DateTime? SubmittedOnByUC
        {
            get
            {
                return dt_submittedOnByUC;
            }
            internal set
            {
                dt_submittedOnByUC = value;
            }
        }

        public int? SubmittedByUC
        {
            get
            {
                return int_submittedByUC;
            }
            internal set
            {
                int_submittedByUC = value;
            }
        }

        public DateTime? SubmittedOnByExUC
        {
            get
            {
                return dt_submittedOnByExUC;
            }
            internal set
            {
                dt_submittedOnByExUC = value;
            }
        }
        public long? SubmittedByExUC
        {
            get
            {
                return lng_submittedByExUC;
            }
             set
            {
                lng_submittedByExUC = value;
            }
        }


        public DateTime? ModifiedOnByUC
        {
            get
            {
                return dt_modifiedOnByUC;
            }
            internal set
            {
                dt_modifiedOnByUC = value;
            }
        }

        public int? ModifiedByUC
        {
            get
            {
                return int_modifiedByUC;
            }
            set
            {
                int_modifiedByUC = value;
            }
        }

        public DateTime? ModifiedOnByExUC
        {
            get
            {
                return dt_modifiedOnByExUC;
            }
            internal set
            {
                dt_modifiedOnByExUC = value;
            }
        }
        public long? ModifiedByExUC
        {
            get
            {
                return lng_modifiedByExUC;
            }
            set
            {
                lng_modifiedByExUC = value;
            }
        }
        public RelaxationApplication[] RelaxationApplicationCollection
        {
            get
            {
                return arr_RelaxationApplicationCollection;

            }
            internal set
            {
                arr_RelaxationApplicationCollection = value;
            }
        }
         

        public NOCAP.BLL.Master.ApplicationStatus GetApplicationStatus()
        {
            NOCAP.BLL.Master.ApplicationStatus obj_applicationStatus = new NOCAP.BLL.Master.ApplicationStatus(this.LatestApplicationStatusCode);
            try
            {
                return obj_applicationStatus;
            }
            catch
            {
                return null;
            }
            finally
            {
                obj_applicationStatus = null;
            }
        }
        public NOCAP.BLL.UserManagement.User GetApplicantUser()
        {
            NOCAP.BLL.UserManagement.User obj_applicantUser = new NOCAP.BLL.UserManagement.User(this.ApplicantUserCode);
            try
            {
                return obj_applicantUser;
            }
            catch
            {
                return null;
            }
            finally
            {
                obj_applicantUser = null;
            }
        }
         
        public NOCAP.BLL.UserManagement.ExternalUser GetApplicantExternalUser()
        {
            NOCAP.BLL.UserManagement.ExternalUser obj_applicantExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(this.ApplicantExternalUserCode);
            try
            {
                return obj_applicantExternalUser;
            }
            catch
            {
                return null;
            }
            finally
            {
                obj_applicantExternalUser = null;
            }
        }

        public int StateCode { get; set; }
        public int DistrictCode { get; set; }
        public int SubDistrictCode { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public enum VillageOrTownOption
        {
            Village = 1,
            Town = 2,
        }

         
        public int TownCode { get; set; }
        public int VillageCode { get; set; }
        public int? PinCode { get; set; }

        public VillageOrTownOption VillageOrTown { get; set; }
        public GroundWaterUtilizationForOption GroundWaterUtilizationFor { get; set; }
        public NOCObtainForExistINDOption NOCObtainForExistIND { get; set; }

        //public string BhaTransRefNo { get; set; }
        //public DateTime? BhaTransDated { get; set; }
        public decimal? PayMentAmount { get; set; }
        public int PayMentAttCode { get; set; }

        public string UIDNumber { get; set; }

        public SubmittedOption Submitted { get; set; }

        //public NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory GetApplySubDistrictAreaTypeCategoryHistory()
        //{
        //    NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(this.ProposedLocation.ProposedLocationAddress.StateCode, this.ProposedLocation.ProposedLocationAddress.DistrictCode, this.ProposedLocation.ProposedLocationAddress.SubDistrictCode, this.ApplySubDistrictAreaCategoryKey);
        //    try
        //    {
        //        return obj_SubDistrictAreaTypeCategoryHistory;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        obj_SubDistrictAreaTypeCategoryHistory = null;
        //    }
        //}



        #region Main Function


        public RelaxationApplication(long lngA_ApplicationCode)
            : this()
        {
            PopulateRelaxationApplicationForApplicationCode(lngA_ApplicationCode);
        }


        private int PopulateRelaxationApplicationForApplicationCode(long lngA_ApplicationCode = 0)
        {

            if (lngA_ApplicationCode == 0)
            {
                this.CustumMessage = " RelaxationApplication - ApplicationCode required.";
                return 0;
            }


            this.ApplicationCode = lngA_ApplicationCode;
             
            RelaxationApplicationDAL obj_relaxationApplicationDAL = new RelaxationApplicationDAL();
             
            try
            {
                if (obj_relaxationApplicationDAL.PopulateRelaxationApplicationForApplicationCode(this) == 0)
                {
                    return 0;
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }



        public int Add()
        {
            Relaxation.RelaxationApplicationDAL obj_RelaxationApplicationDAL = new Relaxation.RelaxationApplicationDAL();

            if (this.ApplicationCode > 0)
            {
                
                this.CustumMessage = "Industrial New Application  - ApplicationCode should  not be provided provided ";
                return 0;

            }

            if (this.ApplicationTypeCode == 0)
            {
                this.CustumMessage = "Industrial New Application  - ApplicationTypeCode required.";
                return 0;

            }
            if (this.ApplicationPurposeCode == 0)
            {
                this.CustumMessage = "Industrial New Application  - ApplicationPurposeCoder required.";
                return 0;

            }
            if (this.ApplicationTypeCategoryCode == 0)
            {
                this.CustumMessage = "Industrial New Application  - ApplicationTypeCategoryCode required.";
                return 0;

            }

            
            if ((this.NameOfIndustry).Trim().Length == 0)
            {
                this.CustumMessage = "Industrial New Application  - NameOfIndustry required.";
                return 0;
            }
            if ((this.AddressLine1).Trim().Length == 0)
            {
                this.CustumMessage = "Industrial New Application  - AddressLine1 required.";
                return 0;
            }
            if ((this.StateCode) == 0)
            {
                this.CustumMessage = "Industrial New Application  - State Code required.";
                return 0;
            }
            if ((this.DistrictCode) == 0)
            {
                this.CustumMessage = "Industrial New Application  - District Code required.";
                return 0;
            }

            if ((this.SubDistrictCode) == 0)
            {
                this.CustumMessage = "Industrial New Application  - SubDistrictCode required.";
                return 0;
            }
            if (!(Enum.IsDefined(typeof(VillageOrTownOption), this.VillageOrTown)))
            {
                this.CustumMessage = "Industrial New Application  - Please select village or town.";
                return 0;
            }

            //if ((this.VillageCode) == 0)
            //{
            //    this.CustumMessage = "Industrial New Application  - VillageCode required.";
            //    return 0;
            //}


            //if (this.ApplySubDistrictAreaCategoryKey == 0)
            //{
            //    this.CustumMessage = "Industrial New Application - ApplySubDistrictAreaCategoryKey required.";
            //    return 0;

            //}



            //int int_stateCode = this.ProposedLocation.ProposedLocationAddress.StateCode;
            //int int_districtCode = this.ProposedLocation.ProposedLocationAddress.DistrictCode;
            //int int_subDistrictCode = this.ProposedLocation.ProposedLocationAddress.SubDistrictCode;
            //int int_applicationTypeCode = this.ApplicationTypeCode;
            //int int_applicationTypeCategoryCode = this.ApplicationTypeCategoryCode;

            //int int_applicationPurposeCode = this.ApplicationPurposeCode;

            //NOCAP.BLL.Master.ApplicationAllowOrNotForApply obj_ApplicationAllowOrNotForApply = new NOCAP.BLL.Master.ApplicationAllowOrNotForApply(int_applicationTypeCode, int_applicationPurposeCode, int_applicationTypeCategoryCode, int_stateCode, int_districtCode, int_subDistrictCode, 1);
            ////chexk for area type cate not defined
            //NOCAP.BLL.Master.SubDistrictAreaTypeCategory obj_SubDistrictAreaTypeCategory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategory(int_stateCode, int_districtCode, int_subDistrictCode);
            //if (obj_SubDistrictAreaTypeCategory.SubDistrictCurrentAreaCategoryKey == 0)
            //{
            //    this.CustumMessage = "Industrial New Application  - Area Type Category not defined";
            //    return 0;

            //}

            //if (obj_ApplicationAllowOrNotForApply.ProceedForFinalApply == NOCAP.BLL.Master.ApplicationAllowOrNotForApply.ApplicationAllowNotAllowForApply.Allow)
            //{

            //}
            //else
            //{
            //    if (obj_ApplicationAllowOrNotForApply.ProceedForApplyOnAllowPendingApplicationOnAreaTypeCategory == NOCAP.BLL.Master.ApplicationAllowOrNotForApply.ApplicationAllowNotAllowForApply.NotAllow)
            //    {

            //        this.CustumMessage = "Industrial New Application  - Not Allow due to Area Type Category";
            //        return 0;

            //    }
            //    if (obj_ApplicationAllowOrNotForApply.ProceedForApplyOnApplicationNotAlloweOnWaterBasedIndustry == NOCAP.BLL.Master.ApplicationAllowOrNotForApply.ApplicationAllowNotAllowForApply.NotAllow)
            //    {
            //        this.CustumMessage = "Industrial New Application  - Not Allow due to Water Based Industry";
            //        return 0;
            //    }
            //    if (obj_ApplicationAllowOrNotForApply.ProceedForApplyOnApplicationCheckForStateGroundWaterAuthority == NOCAP.BLL.Master.ApplicationAllowOrNotForApply.ApplicationAllowNotAllowForApply.NotAllow)
            //    {
            //        this.CustumMessage = "Industrial New Application  - Not Allow due to State Ground Water Authority";
            //        return 0;

            //    }
            //    this.CustumMessage = "Industrial New Application  - Not Allow";

            //    return 0;


            //}








            if (this.CreatedByUC == null && this.CreatedByExUC == null)
            {
                this.CustumMessage = "Industrial New Application  - CreatedByUC or  CreatedByExUC required.";
                return 0;
            }
            if (this.CreatedByUC != null && this.CreatedByExUC != null)
            {
                this.CustumMessage = "Industrial New Application  - only one either CreatedByUC or  CreatedByExUC required.";
                return 0;
            }
            try
            {
                if (obj_RelaxationApplicationDAL.AddRelaxationApplication(this) == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch
            {
                this.CustumMessage = "Application Problem in BLL";
                return 0;
            }




        }
        public int Update()
        {

            Relaxation.RelaxationApplicationDAL obj_RelaxationApplicationDAL = new Relaxation.RelaxationApplicationDAL();

            if (this.ApplicationCode <= 0)
            {
                this.CustumMessage = "Industrial New Application  - ApplicationCode should  be provided provided ";
                return 0;

            }

            if (this.ApplicationTypeCode == 0)
            {
                this.CustumMessage = "Industrial New Application  - ApplicationTypeCode required.";
                return 0;

            }
            if (this.ApplicationPurposeCode == 0)
            {
                this.CustumMessage = "Industrial New Application  - ApplicationPurposeCoder required.";
                return 0;

            }
            if (this.ApplicationTypeCategoryCode == 0)
            {
                this.CustumMessage = "Industrial New Application  - ApplicationTypeCategoryCode required.";
                return 0;

            }

            if ((this.NameOfIndustry).Trim().Length == 0)
            {
                this.CustumMessage = "Industrial New Application  - NameOfIndustry required.";
                return 0;
            }
            if ((this.AddressLine1).Trim().Length == 0)
            {
                this.CustumMessage = "Industrial New Application  - AddressLine1 required.";
                return 0;
            }
            if ((this.StateCode) == 0)
            {
                this.CustumMessage = "Industrial New Application  - State Code required.";
                return 0;
            }
            if ((this.DistrictCode) == 0)
            {
                this.CustumMessage = "Industrial New Application  - District Code required.";
                return 0;
            }

            if ((this.SubDistrictCode) == 0)
            {
                this.CustumMessage = "Industrial New Application  - SubDistrictCode required.";
                return 0;
            }
            if (!(Enum.IsDefined(typeof(VillageOrTownOption), this.VillageOrTown)))
            {
                this.CustumMessage = "Industrial New Application  - Please select village or town.";
                return 0;
            }

            //if ((this.VillageCode) == 0)
            //{
            //    this.CustumMessage = "Industrial New Application  - VillageCode required.";
            //    return 0;
            //}





            //int int_stateCode = this.ProposedLocation.ProposedLocationAddress.StateCode;
            //int int_districtCode = this.ProposedLocation.ProposedLocationAddress.DistrictCode;
            //int int_subDistrictCode = this.ProposedLocation.ProposedLocationAddress.SubDistrictCode;
            //int int_applicationTypeCode = this.ApplicationTypeCode;
            //int int_applicationTypeCategoryCode = this.ApplicationTypeCategoryCode;

            //int int_applicationPurposeCode = this.ApplicationPurposeCode;

            //NOCAP.BLL.Master.ApplicationAllowOrNotForApply obj_ApplicationAllowOrNotForApply = new NOCAP.BLL.Master.ApplicationAllowOrNotForApply(int_applicationTypeCode, int_applicationPurposeCode, int_applicationTypeCategoryCode, int_stateCode, int_districtCode, int_subDistrictCode, 1);
            ////chexk for area type cate not defined
            //NOCAP.BLL.Master.SubDistrictAreaTypeCategory obj_SubDistrictAreaTypeCategory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategory(int_stateCode, int_districtCode, int_subDistrictCode);
            //if (obj_SubDistrictAreaTypeCategory.SubDistrictCurrentAreaCategoryKey == 0)
            //{
            //    this.CustumMessage = "Industrial New Application  - Area Type Category not defined";
            //    return 0;

            //}

            //if (obj_ApplicationAllowOrNotForApply.ProceedForFinalApply == NOCAP.BLL.Master.ApplicationAllowOrNotForApply.ApplicationAllowNotAllowForApply.Allow)
            //{

            //}
            //else
            //{
            //    if (obj_ApplicationAllowOrNotForApply.ProceedForApplyOnAllowPendingApplicationOnAreaTypeCategory == NOCAP.BLL.Master.ApplicationAllowOrNotForApply.ApplicationAllowNotAllowForApply.NotAllow)
            //    {

            //        this.CustumMessage = "Industrial New Application  - Not Allow due to Area Type Category";
            //        return 0;

            //    }
            //    if (obj_ApplicationAllowOrNotForApply.ProceedForApplyOnApplicationNotAlloweOnWaterBasedIndustry == NOCAP.BLL.Master.ApplicationAllowOrNotForApply.ApplicationAllowNotAllowForApply.NotAllow)
            //    {
            //        this.CustumMessage = "Industrial New Application  - Not Allow due to Water Based Industry";
            //        return 0;
            //    }
            //    if (obj_ApplicationAllowOrNotForApply.ProceedForApplyOnApplicationCheckForStateGroundWaterAuthority == NOCAP.BLL.Master.ApplicationAllowOrNotForApply.ApplicationAllowNotAllowForApply.NotAllow)
            //    {
            //        this.CustumMessage = "Industrial New Application  - Not Allow due to State Ground Water Authority";
            //        return 0;

            //    }
            //    this.CustumMessage = "Industrial New Application  - Not Allow";
            //    return 0;


            //}















            //if (this.ApplySubDistrictAreaCategoryKey == 0)
            //{
            //    this.CustumMessage = "Industrial New Application - ApplySubDistrictAreaCategoryKey required.";
            //    return 0;

            //}

            if (this.ModifiedByUC == null && this.ModifiedByExUC == null)
            {
                this.CustumMessage = "Industrial New Application  - ModifiedByUC or  ModifiedByExUC required.";
                return 0;
            }
            if (this.ModifiedByUC != null && this.ModifiedByExUC != null)
            {
                this.CustumMessage = "Industrial New Application  - only one either ModifiedByUC or  ModifiedByExUC required.";
                return 0;
            }


            try
            {
                if (obj_RelaxationApplicationDAL.UpdateRelaxationApplication(this) == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch
            {
                this.CustumMessage = "Application Problem in BLL";
                return 0;
            }

        }

        
      
      
        #endregion


   
    }



}




