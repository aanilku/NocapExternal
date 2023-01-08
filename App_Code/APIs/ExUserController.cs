using System;
//using System.Collections.Generic;
using System.Linq;
//using System.Net;
//using System.Net.Http;
using System.Web.Http;
using System.Text;
using System.Threading;
using System.Net.Http;
using System.Web.Http.Cors;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Web;
using System.Net;
using NOCAP.BLL.Misc.Compliance;


//[EnableCorsAttribute("*",  "*",  "*")]
public class ExUserController : ApiController
{



    // [EnableCors(origins: "http://localhost:8100", headers: "*", methods: "Get")]
    // [BasicAuthentication]
    //public HttpResponseMessage Get()
    //{
    //    HttpResponseMessage v = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, "UserName and password should not be empty");
    //    // string n=  v.Content.ToString();

    //    //   string str_userName = Thread.CurrentPrincipal.Identity.Name;
    //    //return new string[] { "value2" };
    //    return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, "UserName and password should not be empty");

    //}


    #region Login API
    [HttpPost]
    public MobExUser login([FromBody]MobExUser obj_ExUser)
    {
        try
        {
            MobExUser obj_mobExUser = new MobExUser();

            if (obj_ExUser.password == "")
            {
                obj_mobExUser.Status = "Not Ok";
                obj_mobExUser.Message = "False";
                return obj_mobExUser;
            }
            else
            {
                if (obj_ExUser.password.Length % 4 == 0)
                {
                    string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(obj_ExUser.password));
                    // spliting decodeauthToken using ':' 
                    string[] userNamePassword = decodedAuthenticationToken.Split(':');
                    string userName = userNamePassword[0];
                    string password = userNamePassword[1];
                    string str_msg = "";

                    NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = null;
                    if (GetUserProfile(userName, password, out str_msg))
                    {
                        obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(userName);
                        NOCAP.BLL.Master.State obj_State = new NOCAP.BLL.Master.State(obj_ExternalUser.StateCode);
                        NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_ExternalUser.StateCode, obj_ExternalUser.DistrictCode);
                        NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_ExternalUser.StateCode, obj_ExternalUser.DistrictCode, (int)obj_ExternalUser.SubDistrictCode);
                        obj_mobExUser.UserName = obj_ExternalUser.ExternalUserName;
                        obj_mobExUser.MobileNumber = obj_ExternalUser.ExternalUserMobileNumber;
                        obj_mobExUser.AddressLine1 = obj_ExternalUser.AddressLine1;
                        obj_mobExUser.AddressLine2 = obj_ExternalUser.AddressLine2;
                        obj_mobExUser.AddressLine3 = obj_ExternalUser.AddressLine3;
                        obj_mobExUser.StateName = obj_State.StateName;
                        obj_mobExUser.DistrictName = obj_District.DistrictName;
                        obj_mobExUser.SubDistrictName = obj_SubDistrict.SubDistrictName;
                        obj_mobExUser.PinCode = obj_ExternalUser.PinCode;
                        obj_mobExUser.CustumMessage = obj_ExternalUser.CustumMessage;
                        obj_mobExUser.Status = "Ok";
                        obj_mobExUser.Message = "True";
                        obj_mobExUser.ApplicationNumber = GetApplicationNumberList(obj_ExternalUser.ExternalUserCode);
                        return obj_mobExUser;
                    }
                    else
                    {
                        // NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(userName);
                        obj_mobExUser.CustumMessage = str_msg;//"User Name or Password is not correct";
                        obj_mobExUser.Status = "Not Ok";
                        obj_mobExUser.Message = "False";
                        return obj_mobExUser;
                        //  return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, "User Name or Password is wrong");
                    }
                }
                else
                {
                    obj_mobExUser.CustumMessage = "Not Found";
                    obj_mobExUser.Status = "Not Ok";
                    obj_mobExUser.Message = "False";
                    return obj_mobExUser;
                    //  return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, "User Name or Password is wrong");
                }
            }
        }
        catch (Exception ex)
        {
            MobExUser obj_mobExUser = new MobExUser();
            obj_mobExUser.CustumMessage = ex.Message;
            obj_mobExUser.Status = "Not Ok";
            obj_mobExUser.Message = "False";
            return obj_mobExUser;

        }
    }
    public static bool GetUserProfile(string userName, string password, out string str_msg)
    {
        try
        {
            str_msg = "";
            NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(userName);
            bool LoginSuccess = false;
            if (obj_ExternalUser.ExternalUserCode > 0)
            {
                string paswordSalt = obj_ExternalUser.PwdHash; string ExhasPassSalt = "";
                if (obj_ExternalUser.PwdHash.Length < 50)
                {
                    LoginSuccess = false;
                    str_msg = "External Uesr-Please reset the password";
                }
                else
                {
                    ExhasPassSalt = NOCAPExternalUtility.GenerateSHA512String(password);
                    if (ExhasPassSalt.ToLower() == paswordSalt.ToLower())
                    {
                        LoginSuccess = true;
                        str_msg = "External Uesr-Login Successfully";
                    }
                    else
                    {
                        LoginSuccess = false;
                        str_msg = "External Uesr-User Name or Password is not correct";
                    }
                }

            }
            return LoginSuccess;
        }
        catch (Exception ex)
        {
            str_msg = "External Uesr-" + ex.Message;
            return false;
        }
    }
    private static string CreateMD5(string input)
    {
        // Use input string to calculate MD5 hash
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
    #endregion

    #region  SelfCompliance APIs

    [HttpPost]
    public HttpResponseMessage GetAppCompDetails([FromBody] MobSelfCompliance objA_MobSelfCompliance)
    {



        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null;
        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = null;

        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = null;
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = null;
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = null;
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = null;

        NOCAP.BLL.Master.Town obj_Town = null;// new NOCAP.BLL.Master.Town(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
        NOCAP.BLL.Master.Village obj_Village = null;// new NOCAP.BLL.Master.Village(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);



        NOCAP.BLL.Utility.Utility.GetAppplicationObjectForNOCNo(out obj_IndustrialNewApplication,
            out obj_InfrastructureNewApplication, out obj_MiningNewApplication,
           out obj_IndustrialRenewApplication, out obj_InfrastructureRenewApplication, out obj_MiningRenewApplication, objA_MobSelfCompliance.NOCNo);
        NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = null;// new NOCAP.BLL.Misc.Compliance.SelfCompliance(objA_MobSelfCompliance.ApplicationCode);
        MobSelfCompliance obj_MobSelfCompliance = null;
        MobSelfCompliance obj_MobSelfComplianceEmpty = new MobSelfCompliance();


        if (obj_IndustrialNewApplication != null)
        {
            if (objA_MobSelfCompliance.AppliedFor.ToLower() == "fresh")
            {
                obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(obj_IndustrialNewApplication.IndustrialNewApplicationCode);
                if (obj_selfCompliance.ApplicationCode != 0 && obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                {
                    #region IND New 
                    NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();
                    obj_MobSelfCompliance = new MobSelfCompliance();
                    obj_MobSelfCompliance.ApplicationCode = obj_selfCompliance.ApplicationCode;
                    obj_MobSelfCompliance.NOCNo = objA_MobSelfCompliance.NOCNo;
                    NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_IndustrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter(obj_selfCompliance.ApplicationCode);// obj_IndustrialNewApplication.GetIssuedLetter();
                    NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_IndustrialNewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);
                    obj_MobSelfCompliance.AppliedFor = "Fresh";
                    obj_MobSelfCompliance.ProjectName = obj_IndustrialNewApplication.NameOfIndustry;
                    obj_MobSelfCompliance.AppNo = obj_IndustrialNewApplication.IndustrialNewApplicationNumber;
                    obj_MobSelfCompliance.AddressLine1 = HttpUtility.HtmlEncode(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1);
                    obj_MobSelfCompliance.AddressLine2 = HttpUtility.HtmlEncode(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2);
                    obj_MobSelfCompliance.AddressLine3 = HttpUtility.HtmlEncode(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3);
                    obj_MobSelfCompliance.StateName = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().StateName));
                    obj_MobSelfCompliance.DistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.District(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName));
                    obj_MobSelfCompliance.SubDistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.SubDistrict(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName));
                    obj_Town = new NOCAP.BLL.Master.Town(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
                    obj_Village = new NOCAP.BLL.Master.Village(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);
                    if (obj_Town.TownName != "")
                        obj_MobSelfCompliance.Town = HttpUtility.HtmlEncode(obj_Town.TownName);
                    if (obj_Village.VillageName != "")
                        obj_MobSelfCompliance.Village = HttpUtility.HtmlEncode(obj_Village.VillageName);
                    obj_MobSelfCompliance.NOCValidatation = "(" + Convert.ToDateTime(obj_IndustrialNewIssusedLetter.ValidityStartDate).ToShortDateString() + " - " + Convert.ToDateTime(obj_IndustrialNewIssusedLetter.ValidityEndDate).ToShortDateString() + ")";
                    obj_MobSelfCompliance.QtyPerDay = Convert.ToDecimal(obj_IndustrialNewIssusedLetter.GroundWaterAbsRecommToApprPerDay);
                    obj_MobSelfCompliance.QtyPerYear = Convert.ToDecimal(obj_IndustrialNewIssusedLetter.GroundWaterAbsRecommToApprPerYear);
                    obj_MobSelfCompliance.CatOfBlock = obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc();
                    obj_MobSelfCompliance.ComplianceSubmitDate = obj_selfCompliance.ComplianceSubmitDate;
                    obj_MobSelfCompliance.PresentGroundWaterReq = obj_selfCompliance.PresentGroundWaterReq;
                    obj_MobSelfCompliance.PresentGroundWaterReqInDay = obj_selfCompliance.PresentGroundWaterReqInDay;
                    obj_MobSelfCompliance.PresentGroundWaterReqInYear = obj_selfCompliance.PresentGroundWaterReqInYear;
                    obj_MobSelfCompliance.WaterMetFitted = obj_selfCompliance.WaterMetFitted;
                    obj_MobSelfCompliance.WaterMetFittedDesc = obj_selfCompliance.WaterMetFittedDesc;
                    obj_MobSelfCompliance.GWQualityPreMonsoon = obj_selfCompliance.GWQualityPreMonsoon;
                    obj_MobSelfCompliance.GWQualityPreMonsoonDesc = obj_selfCompliance.GWQualityPreMonsoonDesc;
                    obj_MobSelfCompliance.RWHArtificialRecharge = obj_selfCompliance.RWHArtificialRecharge;
                    obj_MobSelfCompliance.RWHArtificialRechargeNo = obj_selfCompliance.RWHArtificialRechargeNo;
                    obj_MobSelfCompliance.RWHArtificialRechargeCapacity = obj_selfCompliance.RWHArtificialRechargeCapacity;
                    obj_MobSelfCompliance.RWHArtificialRechargeDesc = obj_selfCompliance.RWHArtificialRechargeDesc;
                    obj_MobSelfCompliance.PhotoGraph = obj_selfCompliance.PhotoGraph;
                    obj_MobSelfCompliance.PiezoAWLRTelemetry = obj_selfCompliance.PiezoAWLRTelemetry;
                    obj_MobSelfCompliance.PiezoAWLRTelemetryDesc = obj_selfCompliance.PiezoAWLRTelemetryDesc;
                    obj_MobSelfCompliance.RecycleReuse = obj_selfCompliance.RecycleReuse;
                    obj_MobSelfCompliance.RecycleReuseInDay = obj_selfCompliance.RecycleReuseInDay;
                    obj_MobSelfCompliance.RecycleReuseInYear = obj_selfCompliance.RecycleReuseInYear;
                    obj_MobSelfCompliance.RecycleReuseDesc = obj_selfCompliance.RecycleReuseDesc;
                    obj_MobSelfCompliance.ActionTakenReportWithinOneYear = obj_selfCompliance.ActionTakenReportWithinOneYear;




                    obj_MobSelfCompliance.ActionTakenReportWithinOneYearDesc = obj_selfCompliance.ActionTakenReportWithinOneYearDesc;
                    obj_MobSelfCompliance.PreSelfsentGroundWaterReqInYear = obj_selfCompliance.PreSelfsentGroundWaterReqInYear;
                    obj_MobSelfCompliance.AbstraStructExistingAsPerNOC = obj_selfCompliance.AbstraStructExistingAsPerNOC;
                    obj_MobSelfCompliance.AbstraStructProposedAsPerNOC = obj_selfCompliance.AbstraStructProposedAsPerNOC;
                    obj_MobSelfCompliance.NumberOfFunMeter = obj_selfCompliance.NumberOfFunMeter;
                    obj_MobSelfCompliance.NoOfSelfPiezoTelem = obj_selfCompliance.NoOfSelfPiezoTelem;
                    obj_MobSelfCompliance.NoOfSelfPiezo = obj_selfCompliance.NoOfSelfPiezo;
                    obj_MobSelfCompliance.NoOfPiezoDWLRTelem = obj_selfCompliance.NoOfPiezoDWLRTelem;
                    obj_MobSelfCompliance.NoOfSelfPiezoDWLRTelem = obj_selfCompliance.NoOfSelfPiezoDWLRTelem;
                    obj_MobSelfCompliance.GroundWaterMonitoringDataAttCode = obj_selfCompliance.GroundWaterMonitoringDataAttCode;
                    obj_MobSelfCompliance.MiningSeepageAttCode = obj_selfCompliance.MiningSeepageAttCode;
                    obj_MobSelfCompliance.AnnualCalibrationAttCode = obj_selfCompliance.AnnualCalibrationAttCode;
                    obj_MobSelfCompliance.VerifiedBy = obj_selfCompliance.VerifiedBy;
                    obj_MobSelfCompliance.VerifiedOn = obj_selfCompliance.VerifiedOn;



                    obj_MobSelfCompliance.Remarks = obj_selfCompliance.Remarks;
                    obj_MobSelfCompliance.CreatedOnByUC = obj_selfCompliance.CreatedOnByUC;
                    obj_MobSelfCompliance.CreatedByUC = obj_selfCompliance.CreatedByUC;
                    obj_MobSelfCompliance.CreatedOnByExUC = obj_selfCompliance.CreatedOnByExUC;
                    obj_MobSelfCompliance.CreatedByExUC = obj_selfCompliance.CreatedByExUC;
                    obj_MobSelfCompliance.SubmittedOnByUC = obj_selfCompliance.SubmittedOnByUC;
                    obj_MobSelfCompliance.SubmittedByUC = obj_selfCompliance.SubmittedByUC;
                    obj_MobSelfCompliance.SubmittedOnByExUC = obj_selfCompliance.SubmittedOnByExUC;
                    obj_MobSelfCompliance.SubmittedByExUC = obj_selfCompliance.SubmittedByExUC;
                    obj_MobSelfCompliance.ModifiedOnByUC = obj_selfCompliance.ModifiedOnByUC;
                    obj_MobSelfCompliance.ModifiedByUC = obj_selfCompliance.ModifiedByUC;
                    obj_MobSelfCompliance.ModifiedOnByExUC = obj_selfCompliance.ModifiedOnByExUC;
                    obj_MobSelfCompliance.ModifiedByExUC = obj_selfCompliance.ModifiedByExUC;
                    obj_MobSelfCompliance.CustumMessage = obj_selfCompliance.CustumMessage;
                    obj_MobSelfCompliance.Status = 1;


                    objA_MobSelfCompliance.NOCNo = obj_selfCompliance.NOCNo;
                    objA_MobSelfCompliance.ValidityStartDate = obj_selfCompliance.ValidityStartDate;
                    objA_MobSelfCompliance.ValidityEndDate = obj_selfCompliance.ValidityEndDate;
                    objA_MobSelfCompliance.GroundWaterAbsDayAppr = obj_selfCompliance.GroundWaterAbsDayAppr;
                    objA_MobSelfCompliance.GroundWaterDewDayAppr = obj_selfCompliance.GroundWaterDewDayAppr;
                    objA_MobSelfCompliance.GroundWaterAbsYearAppr = obj_selfCompliance.GroundWaterAbsYearAppr;
                    objA_MobSelfCompliance.GroundWaterDewYearAppr = obj_selfCompliance.GroundWaterDewYearAppr;
                    objA_MobSelfCompliance.InspectionAgencyCode = obj_selfCompliance.InspectionAgencyCode;
                    objA_MobSelfCompliance.AnyOtherAgency = obj_selfCompliance.AnyOtherAgency;
                    objA_MobSelfCompliance.DateOfInspection = obj_selfCompliance.DateOfInspection;
                    objA_MobSelfCompliance.PreGroundWaterDewReqInDay = obj_selfCompliance.PreGroundWaterDewReqInDay;
                    objA_MobSelfCompliance.PreGroundWaterDewReqInYear = obj_selfCompliance.PreGroundWaterDewReqInYear;
                    objA_MobSelfCompliance.PreGroundWaterAnyVariReqInDay = obj_selfCompliance.PreGroundWaterAnyVariReqInDay;
                    objA_MobSelfCompliance.PreGroundWaterAnyVariReqInYear = obj_selfCompliance.PreGroundWaterAnyVariReqInYear;
                    objA_MobSelfCompliance.AbstraStructExisting = obj_selfCompliance.AbstraStructExisting;
                    objA_MobSelfCompliance.AbstraStructProposed = obj_selfCompliance.AbstraStructProposed;
                    objA_MobSelfCompliance.NoAbsDewStrucAtPresent = obj_selfCompliance.NoAbsDewStrucAtPresent;
                    objA_MobSelfCompliance.FuncAbstStruct = obj_selfCompliance.FuncAbstStruct;
                    objA_MobSelfCompliance.MeterTypeCode = obj_selfCompliance.MeterTypeCode;
                    objA_MobSelfCompliance.NumberOfFunMeter = obj_selfCompliance.NumberOfFunMeter;
                    objA_MobSelfCompliance.TypeOfAbstStructCode = obj_selfCompliance.TypeOfAbstStructCode;
                    objA_MobSelfCompliance.NumberOfStruct = obj_selfCompliance.NumberOfStruct;
                    objA_MobSelfCompliance.QuantOfRecharge = obj_selfCompliance.QuantOfRecharge;
                    objA_MobSelfCompliance.NoOfPiezo = obj_selfCompliance.NoOfPiezo;
                    objA_MobSelfCompliance.NoOfPiezoDWLR = obj_selfCompliance.NoOfPiezoDWLR;
                    objA_MobSelfCompliance.NoOfPiezoTelem = obj_selfCompliance.NoOfPiezoTelem;
                    objA_MobSelfCompliance.NoOfObserwell = obj_selfCompliance.NoOfObserwell;
                    objA_MobSelfCompliance.NoOfFunctPiezo = obj_selfCompliance.NoOfFunctPiezo;
                    objA_MobSelfCompliance.QuanttreatWasteWaterIND = obj_selfCompliance.QuanttreatWasteWaterIND;
                    objA_MobSelfCompliance.QuanttreatWasteWaterINDGreen = obj_selfCompliance.QuanttreatWasteWaterINDGreen;
                    objA_MobSelfCompliance.QuanttreatWasteWaterINDOther = obj_selfCompliance.QuanttreatWasteWaterINDOther;
                    objA_MobSelfCompliance.AuditAgency = obj_selfCompliance.AuditAgency;
                    objA_MobSelfCompliance.DateOfInspectionAudit = obj_selfCompliance.DateOfInspectionAudit;
                    objA_MobSelfCompliance.AnyViolationNOCCondiDesc = obj_selfCompliance.AnyViolationNOCCondiDesc;
                    objA_MobSelfCompliance.AnyOtherCompliancesDesc = obj_selfCompliance.AnyOtherCompliancesDesc;
                    objA_MobSelfCompliance.WellFittedWithWaterFlowMeterAttCode = obj_selfCompliance.WellFittedWithWaterFlowMeterAttCode;
                    objA_MobSelfCompliance.GeoRainWaterHarvRechAttCode = obj_selfCompliance.GeoRainWaterHarvRechAttCode;
                    objA_MobSelfCompliance.GroundWaterQualityAttCode = obj_selfCompliance.GroundWaterQualityAttCode;
                    objA_MobSelfCompliance.GroundwaterAbstractionDataAttCode = obj_selfCompliance.GroundwaterAbstractionDataAttCode;
                    objA_MobSelfCompliance.NOCAttCode = obj_selfCompliance.NOCAttCode;
                    objA_MobSelfCompliance.CopySiteInspectionAttCode = obj_selfCompliance.CopySiteInspectionAttCode;
                    objA_MobSelfCompliance.GroundwaterMonitoringAttCode = obj_selfCompliance.GroundwaterMonitoringAttCode;
                    objA_MobSelfCompliance.PhotoETPSTPAttCode = obj_selfCompliance.PhotoETPSTPAttCode;
                    objA_MobSelfCompliance.WaterAuditAttCode = obj_selfCompliance.WaterAuditAttCode;
                    objA_MobSelfCompliance.IARModelingAttCode = obj_selfCompliance.IARModelingAttCode;
                    objA_MobSelfCompliance.ExtraAttCode = obj_selfCompliance.ExtraAttCode;



                    objA_MobSelfCompliance.InspectionReport = obj_selfCompliance.InspectionReport;
                    objA_MobSelfCompliance.PreGroundWaterAnyVari = obj_selfCompliance.PreGroundWaterAnyVari;
                    objA_MobSelfCompliance.AbstrDataSubmittedTW = obj_selfCompliance.AbstrDataSubmittedTW;
                    objA_MobSelfCompliance.StructPhoto = obj_selfCompliance.StructPhoto;

                    objA_MobSelfCompliance.AbstStructFittedWithWM = obj_selfCompliance.AbstStructFittedWithWM;
                    objA_MobSelfCompliance.TelemInstalled = obj_selfCompliance.TelemInstalled;
                    objA_MobSelfCompliance.AnnuCalibOfWM = obj_selfCompliance.AnnuCalibOfWM;
                    objA_MobSelfCompliance.PhotoWellFittedWithWM = obj_selfCompliance.PhotoWellFittedWithWM;

                    objA_MobSelfCompliance.GWQuality = obj_selfCompliance.GWQuality;
                    objA_MobSelfCompliance.MineSeepageQuality = obj_selfCompliance.MineSeepageQuality;
                    objA_MobSelfCompliance.WaterSampleAnalyzed = obj_selfCompliance.WaterSampleAnalyzed;
                    objA_MobSelfCompliance.GWReportWithinTime = obj_selfCompliance.GWReportWithinTime;

                    objA_MobSelfCompliance.WithOutPremises = obj_selfCompliance.WithOutPremises;
                    objA_MobSelfCompliance.PhotoRechargeStruct = obj_selfCompliance.PhotoRechargeStruct;
                    objA_MobSelfCompliance.GeoPiezoAWLRTelem = obj_selfCompliance.GeoPiezoAWLRTelem;
                    objA_MobSelfCompliance.MoniDataSubmitted = obj_selfCompliance.MoniDataSubmitted;

                    objA_MobSelfCompliance.GeoPhotoOfSTP = obj_selfCompliance.GeoPhotoOfSTP;
                    objA_MobSelfCompliance.SubSCWithinTimeFrame = obj_selfCompliance.SubSCWithinTimeFrame;
                    objA_MobSelfCompliance.WaterAuditInspection = obj_selfCompliance.WaterAuditInspection;
                    objA_MobSelfCompliance.WaterAuditReport = obj_selfCompliance.WaterAuditReport;

                    objA_MobSelfCompliance.ImpactAssementReport = obj_selfCompliance.ImpactAssementReport;
                    objA_MobSelfCompliance.ImpactAssementAIRReport = obj_selfCompliance.ImpactAssementAIRReport;
                    objA_MobSelfCompliance.AnyViolationNOCCondi = obj_selfCompliance.AnyViolationNOCCondi;
                    objA_MobSelfCompliance.AnyOtherCompliances = obj_selfCompliance.AnyOtherCompliances;
                    objA_MobSelfCompliance.PreSelfsentGroundWaterReqInDay = obj_selfCompliance.PreSelfsentGroundWaterReqInDay;





                    //NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_SelfComplianceAttachment = obj_selfCompliance.GetSelfComplianceAttachmentList();
                    //NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_PhotographsAttachment = obj_selfCompliance.GetPhotographsAttachmentList();

                    //List<NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment> list = new List<NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment>();
                    //if (arr_SelfComplianceAttachment.Length > 0)
                    //{
                    //    foreach (NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment att in arr_SelfComplianceAttachment)
                    //    {
                    //        NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachmentB = obj_selfComplianceAttachment.DownloadFile(obj_selfCompliance.ApplicationCode, att.AttachmentCode, att.AttachmentCodeSerialNumber);
                    //        att.AttachmentFile = obj_selfComplianceAttachmentB.AttachmentFile;
                    //        att.ContentType = obj_selfComplianceAttachmentB.ContentType;
                    //        att.FileExtension = obj_selfComplianceAttachmentB.FileExtension;
                    //        list.Add(att);
                    //    }
                    //    obj_MobSelfCompliance.SelfComplianceAttachment = list.ToArray();
                    //}
                    //if (arr_PhotographsAttachment.Length > 0)
                    //{
                    //    list = new List<NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment>();
                    //    foreach (NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment att in arr_PhotographsAttachment)
                    //    {
                    //        NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachmentB = obj_selfComplianceAttachment.DownloadFile(obj_selfCompliance.ApplicationCode, att.AttachmentCode, att.AttachmentCodeSerialNumber);
                    //        att.AttachmentFile = obj_selfComplianceAttachmentB.AttachmentFile;
                    //        att.ContentType = obj_selfComplianceAttachmentB.ContentType;
                    //        att.FileExtension = obj_selfComplianceAttachmentB.FileExtension;
                    //        list.Add(att);
                    //    }
                    //    obj_MobSelfCompliance.PhotographsAttachment = list.ToArray();
                    //}
                    return Request.CreateResponse<MobSelfCompliance>(System.Net.HttpStatusCode.OK, obj_MobSelfCompliance);
                    #endregion
                }
                else
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfComplianceEmpty);
            }
            else
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfComplianceEmpty);
        }
        else if (obj_IndustrialRenewApplication != null)
        {
            if (objA_MobSelfCompliance.AppliedFor.ToLower() == "renewal")
            {
                #region IND Renew


                NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter obj_IndustrialRenewIssusedLetter = new NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter(obj_selfCompliance.ApplicationCode); //obj_IndustrialRenewApplication.GetIssuedLetter();

                obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_IndustrialRenewApplication.FirstApplicationCode);
                obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(obj_IndustrialRenewApplication.IndustrialRenewApplicationCode);
                obj_MobSelfComplianceEmpty = new MobSelfCompliance();
                if (obj_selfCompliance.ApplicationCode != 0 && obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                {
                    obj_MobSelfCompliance = new MobSelfCompliance();
                    obj_MobSelfCompliance.ApplicationCode = obj_selfCompliance.ApplicationCode;

                    obj_MobSelfCompliance.NOCNo = objA_MobSelfCompliance.NOCNo;

                    NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_IndustrialRenewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);

                    obj_MobSelfCompliance.AppliedFor = HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(obj_IndustrialRenewApplication.LinkDepth));
                    obj_MobSelfCompliance.ProjectName = obj_IndustrialNewApplication.NameOfIndustry;
                    obj_MobSelfCompliance.AppNo = obj_IndustrialNewApplication.IndustrialNewApplicationNumber;
                    obj_MobSelfCompliance.AddressLine1 = HttpUtility.HtmlEncode(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1);
                    obj_MobSelfCompliance.AddressLine2 = HttpUtility.HtmlEncode(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2);
                    obj_MobSelfCompliance.AddressLine3 = HttpUtility.HtmlEncode(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3);
                    obj_MobSelfCompliance.StateName = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().StateName));
                    obj_MobSelfCompliance.DistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.District(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName));
                    obj_MobSelfCompliance.SubDistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.SubDistrict(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName));
                    obj_Town = new NOCAP.BLL.Master.Town(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
                    obj_Village = new NOCAP.BLL.Master.Village(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);
                    if (obj_Town.TownName != "")
                        obj_MobSelfCompliance.Town = HttpUtility.HtmlEncode(obj_Town.TownName);
                    if (obj_Village.VillageName != "")
                        obj_MobSelfCompliance.Village = HttpUtility.HtmlEncode(obj_Village.VillageName);
                    obj_MobSelfCompliance.NOCValidatation = "(" + Convert.ToDateTime(obj_IndustrialRenewIssusedLetter.ValidityStartDate).ToShortDateString() + " - " + Convert.ToDateTime(obj_IndustrialRenewIssusedLetter.ValidityEndDate).ToShortDateString() + ")";
                    obj_MobSelfCompliance.QtyPerDay = Convert.ToDecimal(obj_IndustrialRenewIssusedLetter.GroundWaterAbsRecommToApprPerDay);
                    obj_MobSelfCompliance.QtyPerYear = Convert.ToDecimal(obj_IndustrialRenewIssusedLetter.GroundWaterAbsRecommToApprPerYear);
                    obj_MobSelfCompliance.CatOfBlock = obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc();
                    obj_MobSelfCompliance.ComplianceSubmitDate = obj_selfCompliance.ComplianceSubmitDate;
                    obj_MobSelfCompliance.PresentGroundWaterReq = obj_selfCompliance.PresentGroundWaterReq;
                    obj_MobSelfCompliance.PresentGroundWaterReqInDay = obj_selfCompliance.PresentGroundWaterReqInDay;
                    obj_MobSelfCompliance.PresentGroundWaterReqInYear = obj_selfCompliance.PresentGroundWaterReqInYear;
                    obj_MobSelfCompliance.WaterMetFitted = obj_selfCompliance.WaterMetFitted;
                    obj_MobSelfCompliance.WaterMetFittedDesc = obj_selfCompliance.WaterMetFittedDesc;
                    obj_MobSelfCompliance.GWQualityPreMonsoon = obj_selfCompliance.GWQualityPreMonsoon;
                    obj_MobSelfCompliance.GWQualityPreMonsoonDesc = obj_selfCompliance.GWQualityPreMonsoonDesc;
                    obj_MobSelfCompliance.RWHArtificialRecharge = obj_selfCompliance.RWHArtificialRecharge;
                    obj_MobSelfCompliance.RWHArtificialRechargeNo = obj_selfCompliance.RWHArtificialRechargeNo;
                    obj_MobSelfCompliance.RWHArtificialRechargeCapacity = obj_selfCompliance.RWHArtificialRechargeCapacity;
                    obj_MobSelfCompliance.RWHArtificialRechargeDesc = obj_selfCompliance.RWHArtificialRechargeDesc;
                    obj_MobSelfCompliance.PhotoGraph = obj_selfCompliance.PhotoGraph;
                    obj_MobSelfCompliance.PiezoAWLRTelemetry = obj_selfCompliance.PiezoAWLRTelemetry;
                    obj_MobSelfCompliance.PiezoAWLRTelemetryDesc = obj_selfCompliance.PiezoAWLRTelemetryDesc;
                    obj_MobSelfCompliance.RecycleReuse = obj_selfCompliance.RecycleReuse;
                    obj_MobSelfCompliance.RecycleReuseInDay = obj_selfCompliance.RecycleReuseInDay;
                    obj_MobSelfCompliance.RecycleReuseInYear = obj_selfCompliance.RecycleReuseInYear;
                    obj_MobSelfCompliance.RecycleReuseDesc = obj_selfCompliance.RecycleReuseDesc;
                    obj_MobSelfCompliance.ActionTakenReportWithinOneYear = obj_selfCompliance.ActionTakenReportWithinOneYear;
                    obj_MobSelfCompliance.ActionTakenReportWithinOneYearDesc = obj_selfCompliance.ActionTakenReportWithinOneYearDesc;
                    obj_MobSelfCompliance.Remarks = obj_selfCompliance.Remarks;
                    obj_MobSelfCompliance.CreatedOnByUC = obj_selfCompliance.CreatedOnByUC;
                    obj_MobSelfCompliance.CreatedByUC = obj_selfCompliance.CreatedByUC;
                    obj_MobSelfCompliance.CreatedOnByExUC = obj_selfCompliance.CreatedOnByExUC;
                    obj_MobSelfCompliance.CreatedByExUC = obj_selfCompliance.CreatedByExUC;
                    obj_MobSelfCompliance.SubmittedOnByUC = obj_selfCompliance.SubmittedOnByUC;
                    obj_MobSelfCompliance.SubmittedByUC = obj_selfCompliance.SubmittedByUC;
                    obj_MobSelfCompliance.SubmittedOnByExUC = obj_selfCompliance.SubmittedOnByExUC;
                    obj_MobSelfCompliance.SubmittedByExUC = obj_selfCompliance.SubmittedByExUC;
                    obj_MobSelfCompliance.ModifiedOnByUC = obj_selfCompliance.ModifiedOnByUC;
                    obj_MobSelfCompliance.ModifiedByUC = obj_selfCompliance.ModifiedByUC;
                    obj_MobSelfCompliance.ModifiedOnByExUC = obj_selfCompliance.ModifiedOnByExUC;
                    obj_MobSelfCompliance.ModifiedByExUC = obj_selfCompliance.ModifiedByExUC;
                    obj_MobSelfCompliance.CustumMessage = obj_selfCompliance.CustumMessage;
                    obj_MobSelfCompliance.Status = 1;
                    #region Amardeep

                    objA_MobSelfCompliance.NOCNo = obj_selfCompliance.NOCNo;
                    objA_MobSelfCompliance.ValidityStartDate = obj_selfCompliance.ValidityStartDate;
                    objA_MobSelfCompliance.ValidityEndDate = obj_selfCompliance.ValidityEndDate;
                    objA_MobSelfCompliance.GroundWaterAbsDayAppr = obj_selfCompliance.GroundWaterAbsDayAppr;
                    objA_MobSelfCompliance.GroundWaterDewDayAppr = obj_selfCompliance.GroundWaterDewDayAppr;
                    objA_MobSelfCompliance.GroundWaterAbsYearAppr = obj_selfCompliance.GroundWaterAbsYearAppr;
                    objA_MobSelfCompliance.GroundWaterDewYearAppr = obj_selfCompliance.GroundWaterDewYearAppr;
                    objA_MobSelfCompliance.InspectionAgencyCode = obj_selfCompliance.InspectionAgencyCode;
                    objA_MobSelfCompliance.AnyOtherAgency = obj_selfCompliance.AnyOtherAgency;
                    objA_MobSelfCompliance.DateOfInspection = obj_selfCompliance.DateOfInspection;
                    objA_MobSelfCompliance.PreGroundWaterDewReqInDay = obj_selfCompliance.PreGroundWaterDewReqInDay;
                    objA_MobSelfCompliance.PreGroundWaterDewReqInYear = obj_selfCompliance.PreGroundWaterDewReqInYear;
                    objA_MobSelfCompliance.PreGroundWaterAnyVariReqInDay = obj_selfCompliance.PreGroundWaterAnyVariReqInDay;
                    objA_MobSelfCompliance.PreGroundWaterAnyVariReqInYear = obj_selfCompliance.PreGroundWaterAnyVariReqInYear;
                    objA_MobSelfCompliance.AbstraStructExisting = obj_selfCompliance.AbstraStructExisting;
                    objA_MobSelfCompliance.AbstraStructProposed = obj_selfCompliance.AbstraStructProposed;
                    objA_MobSelfCompliance.NoAbsDewStrucAtPresent = obj_selfCompliance.NoAbsDewStrucAtPresent;
                    objA_MobSelfCompliance.FuncAbstStruct = obj_selfCompliance.FuncAbstStruct;
                    objA_MobSelfCompliance.MeterTypeCode = obj_selfCompliance.MeterTypeCode;
                    objA_MobSelfCompliance.NumberOfFunMeter = obj_selfCompliance.NumberOfFunMeter;
                    objA_MobSelfCompliance.TypeOfAbstStructCode = obj_selfCompliance.TypeOfAbstStructCode;
                    objA_MobSelfCompliance.NumberOfStruct = obj_selfCompliance.NumberOfStruct;
                    objA_MobSelfCompliance.QuantOfRecharge = obj_selfCompliance.QuantOfRecharge;
                    objA_MobSelfCompliance.NoOfPiezo = obj_selfCompliance.NoOfPiezo;
                    objA_MobSelfCompliance.NoOfPiezoDWLR = obj_selfCompliance.NoOfPiezoDWLR;
                    objA_MobSelfCompliance.NoOfPiezoTelem = obj_selfCompliance.NoOfPiezoTelem;
                    objA_MobSelfCompliance.NoOfObserwell = obj_selfCompliance.NoOfObserwell;
                    objA_MobSelfCompliance.NoOfFunctPiezo = obj_selfCompliance.NoOfFunctPiezo;
                    objA_MobSelfCompliance.QuanttreatWasteWaterIND = obj_selfCompliance.QuanttreatWasteWaterIND;
                    objA_MobSelfCompliance.QuanttreatWasteWaterINDGreen = obj_selfCompliance.QuanttreatWasteWaterINDGreen;
                    objA_MobSelfCompliance.QuanttreatWasteWaterINDOther = obj_selfCompliance.QuanttreatWasteWaterINDOther;
                    objA_MobSelfCompliance.AuditAgency = obj_selfCompliance.AuditAgency;
                    objA_MobSelfCompliance.DateOfInspectionAudit = obj_selfCompliance.DateOfInspectionAudit;
                    objA_MobSelfCompliance.AnyViolationNOCCondiDesc = obj_selfCompliance.AnyViolationNOCCondiDesc;
                    objA_MobSelfCompliance.AnyOtherCompliancesDesc = obj_selfCompliance.AnyOtherCompliancesDesc;
                    objA_MobSelfCompliance.WellFittedWithWaterFlowMeterAttCode = obj_selfCompliance.WellFittedWithWaterFlowMeterAttCode;
                    objA_MobSelfCompliance.GeoRainWaterHarvRechAttCode = obj_selfCompliance.GeoRainWaterHarvRechAttCode;
                    objA_MobSelfCompliance.GroundWaterQualityAttCode = obj_selfCompliance.GroundWaterQualityAttCode;
                    objA_MobSelfCompliance.GroundwaterAbstractionDataAttCode = obj_selfCompliance.GroundwaterAbstractionDataAttCode;
                    objA_MobSelfCompliance.NOCAttCode = obj_selfCompliance.NOCAttCode;
                    objA_MobSelfCompliance.CopySiteInspectionAttCode = obj_selfCompliance.CopySiteInspectionAttCode;
                    objA_MobSelfCompliance.GroundwaterMonitoringAttCode = obj_selfCompliance.GroundwaterMonitoringAttCode;
                    objA_MobSelfCompliance.PhotoETPSTPAttCode = obj_selfCompliance.PhotoETPSTPAttCode;
                    objA_MobSelfCompliance.WaterAuditAttCode = obj_selfCompliance.WaterAuditAttCode;
                    objA_MobSelfCompliance.IARModelingAttCode = obj_selfCompliance.IARModelingAttCode;
                    objA_MobSelfCompliance.ExtraAttCode = obj_selfCompliance.ExtraAttCode;



                    objA_MobSelfCompliance.InspectionReport = obj_selfCompliance.InspectionReport;
                    objA_MobSelfCompliance.PreGroundWaterAnyVari = obj_selfCompliance.PreGroundWaterAnyVari;
                    objA_MobSelfCompliance.AbstrDataSubmittedTW = obj_selfCompliance.AbstrDataSubmittedTW;
                    objA_MobSelfCompliance.StructPhoto = obj_selfCompliance.StructPhoto;

                    objA_MobSelfCompliance.AbstStructFittedWithWM = obj_selfCompliance.AbstStructFittedWithWM;
                    objA_MobSelfCompliance.TelemInstalled = obj_selfCompliance.TelemInstalled;
                    objA_MobSelfCompliance.AnnuCalibOfWM = obj_selfCompliance.AnnuCalibOfWM;
                    objA_MobSelfCompliance.PhotoWellFittedWithWM = obj_selfCompliance.PhotoWellFittedWithWM;

                    objA_MobSelfCompliance.GWQuality = obj_selfCompliance.GWQuality;
                    objA_MobSelfCompliance.MineSeepageQuality = obj_selfCompliance.MineSeepageQuality;
                    objA_MobSelfCompliance.WaterSampleAnalyzed = obj_selfCompliance.WaterSampleAnalyzed;
                    objA_MobSelfCompliance.GWReportWithinTime = obj_selfCompliance.GWReportWithinTime;

                    objA_MobSelfCompliance.WithOutPremises = obj_selfCompliance.WithOutPremises;
                    objA_MobSelfCompliance.PhotoRechargeStruct = obj_selfCompliance.PhotoRechargeStruct;
                    objA_MobSelfCompliance.GeoPiezoAWLRTelem = obj_selfCompliance.GeoPiezoAWLRTelem;
                    objA_MobSelfCompliance.MoniDataSubmitted = obj_selfCompliance.MoniDataSubmitted;

                    objA_MobSelfCompliance.GeoPhotoOfSTP = obj_selfCompliance.GeoPhotoOfSTP;
                    objA_MobSelfCompliance.SubSCWithinTimeFrame = obj_selfCompliance.SubSCWithinTimeFrame;
                    objA_MobSelfCompliance.WaterAuditInspection = obj_selfCompliance.WaterAuditInspection;
                    objA_MobSelfCompliance.WaterAuditReport = obj_selfCompliance.WaterAuditReport;

                    objA_MobSelfCompliance.ImpactAssementReport = obj_selfCompliance.ImpactAssementReport;
                    objA_MobSelfCompliance.ImpactAssementAIRReport = obj_selfCompliance.ImpactAssementAIRReport;
                    objA_MobSelfCompliance.AnyViolationNOCCondi = obj_selfCompliance.AnyViolationNOCCondi;
                    objA_MobSelfCompliance.AnyOtherCompliances = obj_selfCompliance.AnyOtherCompliances;

                    obj_MobSelfCompliance.ActionTakenReportWithinOneYearDesc = obj_selfCompliance.ActionTakenReportWithinOneYearDesc;
                    obj_MobSelfCompliance.PreSelfsentGroundWaterReqInYear = obj_selfCompliance.PreSelfsentGroundWaterReqInYear;
                    obj_MobSelfCompliance.AbstraStructExistingAsPerNOC = obj_selfCompliance.AbstraStructExistingAsPerNOC;
                    obj_MobSelfCompliance.AbstraStructProposedAsPerNOC = obj_selfCompliance.AbstraStructProposedAsPerNOC;
                    obj_MobSelfCompliance.NumberOfFunMeter = obj_selfCompliance.NumberOfFunMeter;
                    obj_MobSelfCompliance.NoOfSelfPiezoTelem = obj_selfCompliance.NoOfSelfPiezoTelem;
                    obj_MobSelfCompliance.NoOfSelfPiezo = obj_selfCompliance.NoOfSelfPiezo;
                    obj_MobSelfCompliance.NoOfPiezoDWLRTelem = obj_selfCompliance.NoOfPiezoDWLRTelem;
                    obj_MobSelfCompliance.NoOfSelfPiezoDWLRTelem = obj_selfCompliance.NoOfSelfPiezoDWLRTelem;
                    obj_MobSelfCompliance.GroundWaterMonitoringDataAttCode = obj_selfCompliance.GroundWaterMonitoringDataAttCode;
                    obj_MobSelfCompliance.MiningSeepageAttCode = obj_selfCompliance.MiningSeepageAttCode;
                    obj_MobSelfCompliance.AnnualCalibrationAttCode = obj_selfCompliance.AnnualCalibrationAttCode;
                    obj_MobSelfCompliance.VerifiedBy = obj_selfCompliance.VerifiedBy;
                    obj_MobSelfCompliance.VerifiedOn = obj_selfCompliance.VerifiedOn;


                    #endregion
                    return Request.CreateResponse<MobSelfCompliance>(System.Net.HttpStatusCode.OK, obj_MobSelfCompliance);
                }
                #endregion
                else
                    return Request.CreateResponse<MobSelfCompliance>(System.Net.HttpStatusCode.NotFound, obj_MobSelfComplianceEmpty);
            }
            else
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfComplianceEmpty);
        }
        else if (obj_InfrastructureNewApplication != null)
        {
            if (objA_MobSelfCompliance.AppliedFor.ToLower() == "fresh")
            {
                #region INF New

                obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(obj_InfrastructureNewApplication.InfrastructureNewApplicationCode);
                if (obj_selfCompliance.ApplicationCode != 0 && obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                {
                    obj_MobSelfCompliance = new MobSelfCompliance();
                    obj_MobSelfCompliance.ApplicationCode = obj_selfCompliance.ApplicationCode;

                    obj_MobSelfCompliance.NOCNo = objA_MobSelfCompliance.NOCNo;

                    NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_InfrastructureNewIssusedLetter = new NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter(obj_selfCompliance.ApplicationCode); //obj_InfrastructureNewApplication.GetIssuedLetter();
                    NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);

                    obj_MobSelfCompliance.AppliedFor = "Fresh";
                    obj_MobSelfCompliance.ProjectName = obj_InfrastructureNewApplication.NameOfInfrastructure;
                    obj_MobSelfCompliance.AppNo = obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber;
                    obj_MobSelfCompliance.AddressLine1 = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1);
                    obj_MobSelfCompliance.AddressLine2 = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2);
                    obj_MobSelfCompliance.AddressLine3 = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3);

                    obj_MobSelfCompliance.StateName = HttpUtility.HtmlEncode(Convert.ToString(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.GetState().StateName));
                    obj_MobSelfCompliance.DistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.District(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName));
                    obj_MobSelfCompliance.SubDistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName));
                    obj_Town = new NOCAP.BLL.Master.Town(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
                    obj_Village = new NOCAP.BLL.Master.Village(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);
                    if (obj_Town.TownName != "")
                        obj_MobSelfCompliance.Town = HttpUtility.HtmlEncode(obj_Town.TownName);
                    if (obj_Village.VillageName != "")
                        obj_MobSelfCompliance.Village = HttpUtility.HtmlEncode(obj_Village.VillageName);
                    obj_MobSelfCompliance.NOCValidatation = "(" + Convert.ToDateTime(obj_InfrastructureNewIssusedLetter.ValidityStartDate).ToShortDateString() + " - " + Convert.ToDateTime(obj_InfrastructureNewIssusedLetter.ValidityEndDate).ToShortDateString() + ")";
                    obj_MobSelfCompliance.QtyPerDay = Convert.ToDecimal(obj_InfrastructureNewIssusedLetter.GroundWaterAbsRecommToApprPerDay);
                    obj_MobSelfCompliance.QtyPerYear = Convert.ToDecimal(obj_InfrastructureNewIssusedLetter.GroundWaterAbsRecommToApprPerYear);
                    obj_MobSelfCompliance.CatOfBlock = obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc();
                    obj_MobSelfCompliance.ComplianceSubmitDate = obj_selfCompliance.ComplianceSubmitDate;
                    obj_MobSelfCompliance.PresentGroundWaterReq = obj_selfCompliance.PresentGroundWaterReq;
                    obj_MobSelfCompliance.PresentGroundWaterReqInDay = obj_selfCompliance.PresentGroundWaterReqInDay;
                    obj_MobSelfCompliance.PresentGroundWaterReqInYear = obj_selfCompliance.PresentGroundWaterReqInYear;
                    obj_MobSelfCompliance.WaterMetFitted = obj_selfCompliance.WaterMetFitted;
                    obj_MobSelfCompliance.WaterMetFittedDesc = obj_selfCompliance.WaterMetFittedDesc;
                    obj_MobSelfCompliance.GWQualityPreMonsoon = obj_selfCompliance.GWQualityPreMonsoon;
                    obj_MobSelfCompliance.GWQualityPreMonsoonDesc = obj_selfCompliance.GWQualityPreMonsoonDesc;
                    obj_MobSelfCompliance.RWHArtificialRecharge = obj_selfCompliance.RWHArtificialRecharge;
                    obj_MobSelfCompliance.RWHArtificialRechargeNo = obj_selfCompliance.RWHArtificialRechargeNo;
                    obj_MobSelfCompliance.RWHArtificialRechargeCapacity = obj_selfCompliance.RWHArtificialRechargeCapacity;
                    obj_MobSelfCompliance.RWHArtificialRechargeDesc = obj_selfCompliance.RWHArtificialRechargeDesc;
                    obj_MobSelfCompliance.PhotoGraph = obj_selfCompliance.PhotoGraph;
                    obj_MobSelfCompliance.PiezoAWLRTelemetry = obj_selfCompliance.PiezoAWLRTelemetry;
                    obj_MobSelfCompliance.PiezoAWLRTelemetryDesc = obj_selfCompliance.PiezoAWLRTelemetryDesc;
                    obj_MobSelfCompliance.RecycleReuse = obj_selfCompliance.RecycleReuse;
                    obj_MobSelfCompliance.RecycleReuseInDay = obj_selfCompliance.RecycleReuseInDay;
                    obj_MobSelfCompliance.RecycleReuseInYear = obj_selfCompliance.RecycleReuseInYear;
                    obj_MobSelfCompliance.RecycleReuseDesc = obj_selfCompliance.RecycleReuseDesc;
                    obj_MobSelfCompliance.ActionTakenReportWithinOneYear = obj_selfCompliance.ActionTakenReportWithinOneYear;
                    obj_MobSelfCompliance.ActionTakenReportWithinOneYearDesc = obj_selfCompliance.ActionTakenReportWithinOneYearDesc;
                    obj_MobSelfCompliance.Remarks = obj_selfCompliance.Remarks;
                    obj_MobSelfCompliance.CreatedOnByUC = obj_selfCompliance.CreatedOnByUC;
                    obj_MobSelfCompliance.CreatedByUC = obj_selfCompliance.CreatedByUC;
                    obj_MobSelfCompliance.CreatedOnByExUC = obj_selfCompliance.CreatedOnByExUC;
                    obj_MobSelfCompliance.CreatedByExUC = obj_selfCompliance.CreatedByExUC;
                    obj_MobSelfCompliance.SubmittedOnByUC = obj_selfCompliance.SubmittedOnByUC;
                    obj_MobSelfCompliance.SubmittedByUC = obj_selfCompliance.SubmittedByUC;
                    obj_MobSelfCompliance.SubmittedOnByExUC = obj_selfCompliance.SubmittedOnByExUC;
                    obj_MobSelfCompliance.SubmittedByExUC = obj_selfCompliance.SubmittedByExUC;
                    obj_MobSelfCompliance.ModifiedOnByUC = obj_selfCompliance.ModifiedOnByUC;
                    obj_MobSelfCompliance.ModifiedByUC = obj_selfCompliance.ModifiedByUC;
                    obj_MobSelfCompliance.ModifiedOnByExUC = obj_selfCompliance.ModifiedOnByExUC;
                    obj_MobSelfCompliance.ModifiedByExUC = obj_selfCompliance.ModifiedByExUC;
                    obj_MobSelfCompliance.CustumMessage = obj_selfCompliance.CustumMessage;
                    obj_MobSelfCompliance.Status = 1;
                    #region Amardeep

                    objA_MobSelfCompliance.NOCNo = obj_selfCompliance.NOCNo;
                    objA_MobSelfCompliance.ValidityStartDate = obj_selfCompliance.ValidityStartDate;
                    objA_MobSelfCompliance.ValidityEndDate = obj_selfCompliance.ValidityEndDate;
                    objA_MobSelfCompliance.GroundWaterAbsDayAppr = obj_selfCompliance.GroundWaterAbsDayAppr;
                    objA_MobSelfCompliance.GroundWaterDewDayAppr = obj_selfCompliance.GroundWaterDewDayAppr;
                    objA_MobSelfCompliance.GroundWaterAbsYearAppr = obj_selfCompliance.GroundWaterAbsYearAppr;
                    objA_MobSelfCompliance.GroundWaterDewYearAppr = obj_selfCompliance.GroundWaterDewYearAppr;
                    objA_MobSelfCompliance.InspectionAgencyCode = obj_selfCompliance.InspectionAgencyCode;
                    objA_MobSelfCompliance.AnyOtherAgency = obj_selfCompliance.AnyOtherAgency;
                    objA_MobSelfCompliance.DateOfInspection = obj_selfCompliance.DateOfInspection;
                    objA_MobSelfCompliance.PreGroundWaterDewReqInDay = obj_selfCompliance.PreGroundWaterDewReqInDay;
                    objA_MobSelfCompliance.PreGroundWaterDewReqInYear = obj_selfCompliance.PreGroundWaterDewReqInYear;
                    objA_MobSelfCompliance.PreGroundWaterAnyVariReqInDay = obj_selfCompliance.PreGroundWaterAnyVariReqInDay;
                    objA_MobSelfCompliance.PreGroundWaterAnyVariReqInYear = obj_selfCompliance.PreGroundWaterAnyVariReqInYear;
                    objA_MobSelfCompliance.AbstraStructExisting = obj_selfCompliance.AbstraStructExisting;
                    objA_MobSelfCompliance.AbstraStructProposed = obj_selfCompliance.AbstraStructProposed;
                    objA_MobSelfCompliance.NoAbsDewStrucAtPresent = obj_selfCompliance.NoAbsDewStrucAtPresent;
                    objA_MobSelfCompliance.FuncAbstStruct = obj_selfCompliance.FuncAbstStruct;
                    objA_MobSelfCompliance.MeterTypeCode = obj_selfCompliance.MeterTypeCode;
                    objA_MobSelfCompliance.NumberOfFunMeter = obj_selfCompliance.NumberOfFunMeter;
                    objA_MobSelfCompliance.TypeOfAbstStructCode = obj_selfCompliance.TypeOfAbstStructCode;
                    objA_MobSelfCompliance.NumberOfStruct = obj_selfCompliance.NumberOfStruct;
                    objA_MobSelfCompliance.QuantOfRecharge = obj_selfCompliance.QuantOfRecharge;
                    objA_MobSelfCompliance.NoOfPiezo = obj_selfCompliance.NoOfPiezo;
                    objA_MobSelfCompliance.NoOfPiezoDWLR = obj_selfCompliance.NoOfPiezoDWLR;
                    objA_MobSelfCompliance.NoOfPiezoTelem = obj_selfCompliance.NoOfPiezoTelem;
                    objA_MobSelfCompliance.NoOfObserwell = obj_selfCompliance.NoOfObserwell;
                    objA_MobSelfCompliance.NoOfFunctPiezo = obj_selfCompliance.NoOfFunctPiezo;
                    objA_MobSelfCompliance.QuanttreatWasteWaterIND = obj_selfCompliance.QuanttreatWasteWaterIND;
                    objA_MobSelfCompliance.QuanttreatWasteWaterINDGreen = obj_selfCompliance.QuanttreatWasteWaterINDGreen;
                    objA_MobSelfCompliance.QuanttreatWasteWaterINDOther = obj_selfCompliance.QuanttreatWasteWaterINDOther;
                    objA_MobSelfCompliance.AuditAgency = obj_selfCompliance.AuditAgency;
                    objA_MobSelfCompliance.DateOfInspectionAudit = obj_selfCompliance.DateOfInspectionAudit;
                    objA_MobSelfCompliance.AnyViolationNOCCondiDesc = obj_selfCompliance.AnyViolationNOCCondiDesc;
                    objA_MobSelfCompliance.AnyOtherCompliancesDesc = obj_selfCompliance.AnyOtherCompliancesDesc;
                    objA_MobSelfCompliance.WellFittedWithWaterFlowMeterAttCode = obj_selfCompliance.WellFittedWithWaterFlowMeterAttCode;
                    objA_MobSelfCompliance.GeoRainWaterHarvRechAttCode = obj_selfCompliance.GeoRainWaterHarvRechAttCode;
                    objA_MobSelfCompliance.GroundWaterQualityAttCode = obj_selfCompliance.GroundWaterQualityAttCode;
                    objA_MobSelfCompliance.GroundwaterAbstractionDataAttCode = obj_selfCompliance.GroundwaterAbstractionDataAttCode;
                    objA_MobSelfCompliance.NOCAttCode = obj_selfCompliance.NOCAttCode;
                    objA_MobSelfCompliance.CopySiteInspectionAttCode = obj_selfCompliance.CopySiteInspectionAttCode;
                    objA_MobSelfCompliance.GroundwaterMonitoringAttCode = obj_selfCompliance.GroundwaterMonitoringAttCode;
                    objA_MobSelfCompliance.PhotoETPSTPAttCode = obj_selfCompliance.PhotoETPSTPAttCode;
                    objA_MobSelfCompliance.WaterAuditAttCode = obj_selfCompliance.WaterAuditAttCode;
                    objA_MobSelfCompliance.IARModelingAttCode = obj_selfCompliance.IARModelingAttCode;
                    objA_MobSelfCompliance.ExtraAttCode = obj_selfCompliance.ExtraAttCode;



                    objA_MobSelfCompliance.InspectionReport = obj_selfCompliance.InspectionReport;
                    objA_MobSelfCompliance.PreGroundWaterAnyVari = obj_selfCompliance.PreGroundWaterAnyVari;
                    objA_MobSelfCompliance.AbstrDataSubmittedTW = obj_selfCompliance.AbstrDataSubmittedTW;
                    objA_MobSelfCompliance.StructPhoto = obj_selfCompliance.StructPhoto;

                    objA_MobSelfCompliance.AbstStructFittedWithWM = obj_selfCompliance.AbstStructFittedWithWM;
                    objA_MobSelfCompliance.TelemInstalled = obj_selfCompliance.TelemInstalled;
                    objA_MobSelfCompliance.AnnuCalibOfWM = obj_selfCompliance.AnnuCalibOfWM;
                    objA_MobSelfCompliance.PhotoWellFittedWithWM = obj_selfCompliance.PhotoWellFittedWithWM;

                    objA_MobSelfCompliance.GWQuality = obj_selfCompliance.GWQuality;
                    objA_MobSelfCompliance.MineSeepageQuality = obj_selfCompliance.MineSeepageQuality;
                    objA_MobSelfCompliance.WaterSampleAnalyzed = obj_selfCompliance.WaterSampleAnalyzed;
                    objA_MobSelfCompliance.GWReportWithinTime = obj_selfCompliance.GWReportWithinTime;

                    objA_MobSelfCompliance.WithOutPremises = obj_selfCompliance.WithOutPremises;
                    objA_MobSelfCompliance.PhotoRechargeStruct = obj_selfCompliance.PhotoRechargeStruct;
                    objA_MobSelfCompliance.GeoPiezoAWLRTelem = obj_selfCompliance.GeoPiezoAWLRTelem;
                    objA_MobSelfCompliance.MoniDataSubmitted = obj_selfCompliance.MoniDataSubmitted;

                    objA_MobSelfCompliance.GeoPhotoOfSTP = obj_selfCompliance.GeoPhotoOfSTP;
                    objA_MobSelfCompliance.SubSCWithinTimeFrame = obj_selfCompliance.SubSCWithinTimeFrame;
                    objA_MobSelfCompliance.WaterAuditInspection = obj_selfCompliance.WaterAuditInspection;
                    objA_MobSelfCompliance.WaterAuditReport = obj_selfCompliance.WaterAuditReport;

                    objA_MobSelfCompliance.ImpactAssementReport = obj_selfCompliance.ImpactAssementReport;
                    objA_MobSelfCompliance.ImpactAssementAIRReport = obj_selfCompliance.ImpactAssementAIRReport;
                    objA_MobSelfCompliance.AnyViolationNOCCondi = obj_selfCompliance.AnyViolationNOCCondi;
                    objA_MobSelfCompliance.AnyOtherCompliances = obj_selfCompliance.AnyOtherCompliances;



                    obj_MobSelfCompliance.ActionTakenReportWithinOneYearDesc = obj_selfCompliance.ActionTakenReportWithinOneYearDesc;
                    obj_MobSelfCompliance.PreSelfsentGroundWaterReqInYear = obj_selfCompliance.PreSelfsentGroundWaterReqInYear;
                    obj_MobSelfCompliance.AbstraStructExistingAsPerNOC = obj_selfCompliance.AbstraStructExistingAsPerNOC;
                    obj_MobSelfCompliance.AbstraStructProposedAsPerNOC = obj_selfCompliance.AbstraStructProposedAsPerNOC;
                    obj_MobSelfCompliance.NumberOfFunMeter = obj_selfCompliance.NumberOfFunMeter;
                    obj_MobSelfCompliance.NoOfSelfPiezoTelem = obj_selfCompliance.NoOfSelfPiezoTelem;
                    obj_MobSelfCompliance.NoOfSelfPiezo = obj_selfCompliance.NoOfSelfPiezo;
                    obj_MobSelfCompliance.NoOfPiezoDWLRTelem = obj_selfCompliance.NoOfPiezoDWLRTelem;
                    obj_MobSelfCompliance.NoOfSelfPiezoDWLRTelem = obj_selfCompliance.NoOfSelfPiezoDWLRTelem;
                    obj_MobSelfCompliance.GroundWaterMonitoringDataAttCode = obj_selfCompliance.GroundWaterMonitoringDataAttCode;
                    obj_MobSelfCompliance.MiningSeepageAttCode = obj_selfCompliance.MiningSeepageAttCode;
                    obj_MobSelfCompliance.AnnualCalibrationAttCode = obj_selfCompliance.AnnualCalibrationAttCode;
                    obj_MobSelfCompliance.VerifiedBy = obj_selfCompliance.VerifiedBy;
                    obj_MobSelfCompliance.VerifiedOn = obj_selfCompliance.VerifiedOn;
                    #endregion
                    return Request.CreateResponse<MobSelfCompliance>(System.Net.HttpStatusCode.OK, obj_MobSelfCompliance);
                }
                else
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfComplianceEmpty);

                #endregion
            }
            else
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfComplianceEmpty);

        }
        else if (obj_InfrastructureRenewApplication != null)
        {
            if (objA_MobSelfCompliance.AppliedFor.ToLower() == "renewal")
            {
                #region INF Renew

                obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode);
                if (obj_selfCompliance.ApplicationCode != 0 && obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                {
                    obj_MobSelfCompliance = new MobSelfCompliance();
                    obj_MobSelfCompliance.ApplicationCode = obj_selfCompliance.ApplicationCode;

                    obj_MobSelfCompliance.NOCNo = objA_MobSelfCompliance.NOCNo;

                    NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter obj_InfrastructureRenewIssusedLetter = new NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter(obj_selfCompliance.ApplicationCode);// obj_InfrastructureRenewApplication.GetIssuedLetter();

                    obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_InfrastructureRenewApplication.FirstApplicationCode);
                    NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureRenewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);

                    obj_MobSelfCompliance.AppliedFor = HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(obj_InfrastructureRenewApplication.LinkDepth));
                    obj_MobSelfCompliance.ProjectName = obj_InfrastructureNewApplication.NameOfInfrastructure;
                    obj_MobSelfCompliance.AppNo = obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber;
                    obj_MobSelfCompliance.AddressLine1 = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1);
                    obj_MobSelfCompliance.AddressLine2 = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2);
                    obj_MobSelfCompliance.AddressLine3 = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3);
                    obj_MobSelfCompliance.StateName = HttpUtility.HtmlEncode(Convert.ToString(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.GetState().StateName));
                    obj_MobSelfCompliance.DistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.District(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName));
                    obj_MobSelfCompliance.SubDistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName));
                    obj_Town = new NOCAP.BLL.Master.Town(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
                    obj_Village = new NOCAP.BLL.Master.Village(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);
                    if (obj_Town.TownName != "")
                        obj_MobSelfCompliance.Town = HttpUtility.HtmlEncode(obj_Town.TownName);
                    if (obj_Village.VillageName != "")
                        obj_MobSelfCompliance.Village = HttpUtility.HtmlEncode(obj_Village.VillageName);
                    obj_MobSelfCompliance.NOCValidatation = "(" + Convert.ToDateTime(obj_InfrastructureRenewIssusedLetter.ValidityStartDate).ToShortDateString() + " - " + Convert.ToDateTime(obj_InfrastructureRenewIssusedLetter.ValidityEndDate).ToShortDateString() + ")";
                    obj_MobSelfCompliance.QtyPerDay = Convert.ToDecimal(obj_InfrastructureRenewIssusedLetter.GroundWaterAbsRecommToApprPerDay);
                    obj_MobSelfCompliance.QtyPerYear = Convert.ToDecimal(obj_InfrastructureRenewIssusedLetter.GroundWaterAbsRecommToApprPerYear);
                    obj_MobSelfCompliance.CatOfBlock = obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc();
                    obj_MobSelfCompliance.ComplianceSubmitDate = obj_selfCompliance.ComplianceSubmitDate;
                    obj_MobSelfCompliance.PresentGroundWaterReq = obj_selfCompliance.PresentGroundWaterReq;
                    obj_MobSelfCompliance.PresentGroundWaterReqInDay = obj_selfCompliance.PresentGroundWaterReqInDay;
                    obj_MobSelfCompliance.PresentGroundWaterReqInYear = obj_selfCompliance.PresentGroundWaterReqInYear;
                    obj_MobSelfCompliance.WaterMetFitted = obj_selfCompliance.WaterMetFitted;
                    obj_MobSelfCompliance.WaterMetFittedDesc = obj_selfCompliance.WaterMetFittedDesc;
                    obj_MobSelfCompliance.GWQualityPreMonsoon = obj_selfCompliance.GWQualityPreMonsoon;
                    obj_MobSelfCompliance.GWQualityPreMonsoonDesc = obj_selfCompliance.GWQualityPreMonsoonDesc;
                    obj_MobSelfCompliance.RWHArtificialRecharge = obj_selfCompliance.RWHArtificialRecharge;
                    obj_MobSelfCompliance.RWHArtificialRechargeNo = obj_selfCompliance.RWHArtificialRechargeNo;
                    obj_MobSelfCompliance.RWHArtificialRechargeCapacity = obj_selfCompliance.RWHArtificialRechargeCapacity;
                    obj_MobSelfCompliance.RWHArtificialRechargeDesc = obj_selfCompliance.RWHArtificialRechargeDesc;
                    obj_MobSelfCompliance.PhotoGraph = obj_selfCompliance.PhotoGraph;
                    obj_MobSelfCompliance.PiezoAWLRTelemetry = obj_selfCompliance.PiezoAWLRTelemetry;
                    obj_MobSelfCompliance.PiezoAWLRTelemetryDesc = obj_selfCompliance.PiezoAWLRTelemetryDesc;
                    obj_MobSelfCompliance.RecycleReuse = obj_selfCompliance.RecycleReuse;
                    obj_MobSelfCompliance.RecycleReuseInDay = obj_selfCompliance.RecycleReuseInDay;
                    obj_MobSelfCompliance.RecycleReuseInYear = obj_selfCompliance.RecycleReuseInYear;
                    obj_MobSelfCompliance.RecycleReuseDesc = obj_selfCompliance.RecycleReuseDesc;
                    obj_MobSelfCompliance.ActionTakenReportWithinOneYear = obj_selfCompliance.ActionTakenReportWithinOneYear;
                    obj_MobSelfCompliance.ActionTakenReportWithinOneYearDesc = obj_selfCompliance.ActionTakenReportWithinOneYearDesc;
                    obj_MobSelfCompliance.Remarks = obj_selfCompliance.Remarks;
                    obj_MobSelfCompliance.CreatedOnByUC = obj_selfCompliance.CreatedOnByUC;
                    obj_MobSelfCompliance.CreatedByUC = obj_selfCompliance.CreatedByUC;
                    obj_MobSelfCompliance.CreatedOnByExUC = obj_selfCompliance.CreatedOnByExUC;
                    obj_MobSelfCompliance.CreatedByExUC = obj_selfCompliance.CreatedByExUC;
                    obj_MobSelfCompliance.SubmittedOnByUC = obj_selfCompliance.SubmittedOnByUC;
                    obj_MobSelfCompliance.SubmittedByUC = obj_selfCompliance.SubmittedByUC;
                    obj_MobSelfCompliance.SubmittedOnByExUC = obj_selfCompliance.SubmittedOnByExUC;
                    obj_MobSelfCompliance.SubmittedByExUC = obj_selfCompliance.SubmittedByExUC;
                    obj_MobSelfCompliance.ModifiedOnByUC = obj_selfCompliance.ModifiedOnByUC;
                    obj_MobSelfCompliance.ModifiedByUC = obj_selfCompliance.ModifiedByUC;
                    obj_MobSelfCompliance.ModifiedOnByExUC = obj_selfCompliance.ModifiedOnByExUC;
                    obj_MobSelfCompliance.ModifiedByExUC = obj_selfCompliance.ModifiedByExUC;
                    obj_MobSelfCompliance.CustumMessage = obj_selfCompliance.CustumMessage;
                    obj_MobSelfCompliance.Status = 1;
                    #region Amardeep

                    objA_MobSelfCompliance.NOCNo = obj_selfCompliance.NOCNo;
                    objA_MobSelfCompliance.ValidityStartDate = obj_selfCompliance.ValidityStartDate;
                    objA_MobSelfCompliance.ValidityEndDate = obj_selfCompliance.ValidityEndDate;
                    objA_MobSelfCompliance.GroundWaterAbsDayAppr = obj_selfCompliance.GroundWaterAbsDayAppr;
                    objA_MobSelfCompliance.GroundWaterDewDayAppr = obj_selfCompliance.GroundWaterDewDayAppr;
                    objA_MobSelfCompliance.GroundWaterAbsYearAppr = obj_selfCompliance.GroundWaterAbsYearAppr;
                    objA_MobSelfCompliance.GroundWaterDewYearAppr = obj_selfCompliance.GroundWaterDewYearAppr;
                    objA_MobSelfCompliance.InspectionAgencyCode = obj_selfCompliance.InspectionAgencyCode;
                    objA_MobSelfCompliance.AnyOtherAgency = obj_selfCompliance.AnyOtherAgency;
                    objA_MobSelfCompliance.DateOfInspection = obj_selfCompliance.DateOfInspection;
                    objA_MobSelfCompliance.PreGroundWaterDewReqInDay = obj_selfCompliance.PreGroundWaterDewReqInDay;
                    objA_MobSelfCompliance.PreGroundWaterDewReqInYear = obj_selfCompliance.PreGroundWaterDewReqInYear;
                    objA_MobSelfCompliance.PreGroundWaterAnyVariReqInDay = obj_selfCompliance.PreGroundWaterAnyVariReqInDay;
                    objA_MobSelfCompliance.PreGroundWaterAnyVariReqInYear = obj_selfCompliance.PreGroundWaterAnyVariReqInYear;
                    objA_MobSelfCompliance.AbstraStructExisting = obj_selfCompliance.AbstraStructExisting;
                    objA_MobSelfCompliance.AbstraStructProposed = obj_selfCompliance.AbstraStructProposed;
                    objA_MobSelfCompliance.NoAbsDewStrucAtPresent = obj_selfCompliance.NoAbsDewStrucAtPresent;
                    objA_MobSelfCompliance.FuncAbstStruct = obj_selfCompliance.FuncAbstStruct;
                    objA_MobSelfCompliance.MeterTypeCode = obj_selfCompliance.MeterTypeCode;
                    objA_MobSelfCompliance.NumberOfFunMeter = obj_selfCompliance.NumberOfFunMeter;
                    objA_MobSelfCompliance.TypeOfAbstStructCode = obj_selfCompliance.TypeOfAbstStructCode;
                    objA_MobSelfCompliance.NumberOfStruct = obj_selfCompliance.NumberOfStruct;
                    objA_MobSelfCompliance.QuantOfRecharge = obj_selfCompliance.QuantOfRecharge;
                    objA_MobSelfCompliance.NoOfPiezo = obj_selfCompliance.NoOfPiezo;
                    objA_MobSelfCompliance.NoOfPiezoDWLR = obj_selfCompliance.NoOfPiezoDWLR;
                    objA_MobSelfCompliance.NoOfPiezoTelem = obj_selfCompliance.NoOfPiezoTelem;
                    objA_MobSelfCompliance.NoOfObserwell = obj_selfCompliance.NoOfObserwell;
                    objA_MobSelfCompliance.NoOfFunctPiezo = obj_selfCompliance.NoOfFunctPiezo;
                    objA_MobSelfCompliance.QuanttreatWasteWaterIND = obj_selfCompliance.QuanttreatWasteWaterIND;
                    objA_MobSelfCompliance.QuanttreatWasteWaterINDGreen = obj_selfCompliance.QuanttreatWasteWaterINDGreen;
                    objA_MobSelfCompliance.QuanttreatWasteWaterINDOther = obj_selfCompliance.QuanttreatWasteWaterINDOther;
                    objA_MobSelfCompliance.AuditAgency = obj_selfCompliance.AuditAgency;
                    objA_MobSelfCompliance.DateOfInspectionAudit = obj_selfCompliance.DateOfInspectionAudit;
                    objA_MobSelfCompliance.AnyViolationNOCCondiDesc = obj_selfCompliance.AnyViolationNOCCondiDesc;
                    objA_MobSelfCompliance.AnyOtherCompliancesDesc = obj_selfCompliance.AnyOtherCompliancesDesc;
                    objA_MobSelfCompliance.WellFittedWithWaterFlowMeterAttCode = obj_selfCompliance.WellFittedWithWaterFlowMeterAttCode;
                    objA_MobSelfCompliance.GeoRainWaterHarvRechAttCode = obj_selfCompliance.GeoRainWaterHarvRechAttCode;
                    objA_MobSelfCompliance.GroundWaterQualityAttCode = obj_selfCompliance.GroundWaterQualityAttCode;
                    objA_MobSelfCompliance.GroundwaterAbstractionDataAttCode = obj_selfCompliance.GroundwaterAbstractionDataAttCode;
                    objA_MobSelfCompliance.NOCAttCode = obj_selfCompliance.NOCAttCode;
                    objA_MobSelfCompliance.CopySiteInspectionAttCode = obj_selfCompliance.CopySiteInspectionAttCode;
                    objA_MobSelfCompliance.GroundwaterMonitoringAttCode = obj_selfCompliance.GroundwaterMonitoringAttCode;
                    objA_MobSelfCompliance.PhotoETPSTPAttCode = obj_selfCompliance.PhotoETPSTPAttCode;
                    objA_MobSelfCompliance.WaterAuditAttCode = obj_selfCompliance.WaterAuditAttCode;
                    objA_MobSelfCompliance.IARModelingAttCode = obj_selfCompliance.IARModelingAttCode;
                    objA_MobSelfCompliance.ExtraAttCode = obj_selfCompliance.ExtraAttCode;



                    objA_MobSelfCompliance.InspectionReport = obj_selfCompliance.InspectionReport;
                    objA_MobSelfCompliance.PreGroundWaterAnyVari = obj_selfCompliance.PreGroundWaterAnyVari;
                    objA_MobSelfCompliance.AbstrDataSubmittedTW = obj_selfCompliance.AbstrDataSubmittedTW;
                    objA_MobSelfCompliance.StructPhoto = obj_selfCompliance.StructPhoto;

                    objA_MobSelfCompliance.AbstStructFittedWithWM = obj_selfCompliance.AbstStructFittedWithWM;
                    objA_MobSelfCompliance.TelemInstalled = obj_selfCompliance.TelemInstalled;
                    objA_MobSelfCompliance.AnnuCalibOfWM = obj_selfCompliance.AnnuCalibOfWM;
                    objA_MobSelfCompliance.PhotoWellFittedWithWM = obj_selfCompliance.PhotoWellFittedWithWM;

                    objA_MobSelfCompliance.GWQuality = obj_selfCompliance.GWQuality;
                    objA_MobSelfCompliance.MineSeepageQuality = obj_selfCompliance.MineSeepageQuality;
                    objA_MobSelfCompliance.WaterSampleAnalyzed = obj_selfCompliance.WaterSampleAnalyzed;
                    objA_MobSelfCompliance.GWReportWithinTime = obj_selfCompliance.GWReportWithinTime;

                    objA_MobSelfCompliance.WithOutPremises = obj_selfCompliance.WithOutPremises;
                    objA_MobSelfCompliance.PhotoRechargeStruct = obj_selfCompliance.PhotoRechargeStruct;
                    objA_MobSelfCompliance.GeoPiezoAWLRTelem = obj_selfCompliance.GeoPiezoAWLRTelem;
                    objA_MobSelfCompliance.MoniDataSubmitted = obj_selfCompliance.MoniDataSubmitted;

                    objA_MobSelfCompliance.GeoPhotoOfSTP = obj_selfCompliance.GeoPhotoOfSTP;
                    objA_MobSelfCompliance.SubSCWithinTimeFrame = obj_selfCompliance.SubSCWithinTimeFrame;
                    objA_MobSelfCompliance.WaterAuditInspection = obj_selfCompliance.WaterAuditInspection;
                    objA_MobSelfCompliance.WaterAuditReport = obj_selfCompliance.WaterAuditReport;

                    objA_MobSelfCompliance.ImpactAssementReport = obj_selfCompliance.ImpactAssementReport;
                    objA_MobSelfCompliance.ImpactAssementAIRReport = obj_selfCompliance.ImpactAssementAIRReport;
                    objA_MobSelfCompliance.AnyViolationNOCCondi = obj_selfCompliance.AnyViolationNOCCondi;
                    objA_MobSelfCompliance.AnyOtherCompliances = obj_selfCompliance.AnyOtherCompliances;


                    obj_MobSelfCompliance.ActionTakenReportWithinOneYearDesc = obj_selfCompliance.ActionTakenReportWithinOneYearDesc;
                    obj_MobSelfCompliance.PreSelfsentGroundWaterReqInYear = obj_selfCompliance.PreSelfsentGroundWaterReqInYear;
                    obj_MobSelfCompliance.AbstraStructExistingAsPerNOC = obj_selfCompliance.AbstraStructExistingAsPerNOC;
                    obj_MobSelfCompliance.AbstraStructProposedAsPerNOC = obj_selfCompliance.AbstraStructProposedAsPerNOC;
                    obj_MobSelfCompliance.NumberOfFunMeter = obj_selfCompliance.NumberOfFunMeter;
                    obj_MobSelfCompliance.NoOfSelfPiezoTelem = obj_selfCompliance.NoOfSelfPiezoTelem;
                    obj_MobSelfCompliance.NoOfSelfPiezo = obj_selfCompliance.NoOfSelfPiezo;
                    obj_MobSelfCompliance.NoOfPiezoDWLRTelem = obj_selfCompliance.NoOfPiezoDWLRTelem;
                    obj_MobSelfCompliance.NoOfSelfPiezoDWLRTelem = obj_selfCompliance.NoOfSelfPiezoDWLRTelem;
                    obj_MobSelfCompliance.GroundWaterMonitoringDataAttCode = obj_selfCompliance.GroundWaterMonitoringDataAttCode;
                    obj_MobSelfCompliance.MiningSeepageAttCode = obj_selfCompliance.MiningSeepageAttCode;
                    obj_MobSelfCompliance.AnnualCalibrationAttCode = obj_selfCompliance.AnnualCalibrationAttCode;
                    obj_MobSelfCompliance.VerifiedBy = obj_selfCompliance.VerifiedBy;
                    obj_MobSelfCompliance.VerifiedOn = obj_selfCompliance.VerifiedOn;

                    #endregion
                    return Request.CreateResponse<MobSelfCompliance>(System.Net.HttpStatusCode.OK, obj_MobSelfCompliance);
                }
                else
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfComplianceEmpty);

                #endregion
            }
            else
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfComplianceEmpty);


        }
        else if (obj_MiningNewApplication != null)
        {
            if (objA_MobSelfCompliance.AppliedFor.ToLower() == "fresh")
            {
                #region MIN New

                obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(obj_MiningNewApplication.ApplicationCode);
                if (obj_selfCompliance.ApplicationCode != 0 && obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                {
                    obj_MobSelfCompliance = new MobSelfCompliance();
                    obj_MobSelfCompliance.ApplicationCode = obj_selfCompliance.ApplicationCode;

                    obj_MobSelfCompliance.NOCNo = objA_MobSelfCompliance.NOCNo;

                    NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter obj_MiningNewIssusedLetter = new NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter(obj_selfCompliance.ApplicationCode); //obj_MiningNewApplication.GetIssuedLetter();
                    NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);

                    obj_MobSelfCompliance.AppliedFor = "Fresh";
                    obj_MobSelfCompliance.ProjectName = obj_MiningNewApplication.NameOfMining;
                    obj_MobSelfCompliance.AppNo = obj_MiningNewApplication.MiningNewApplicationNumber;
                    obj_MobSelfCompliance.AddressLine1 = HttpUtility.HtmlEncode(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1);
                    obj_MobSelfCompliance.AddressLine2 = HttpUtility.HtmlEncode(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2);
                    obj_MobSelfCompliance.AddressLine3 = HttpUtility.HtmlEncode(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3);
                    obj_MobSelfCompliance.StateName = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.GetState().StateName));
                    obj_MobSelfCompliance.DistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.District(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName));
                    obj_MobSelfCompliance.SubDistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName));
                    obj_Town = new NOCAP.BLL.Master.Town(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
                    obj_Village = new NOCAP.BLL.Master.Village(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);
                    if (obj_Town.TownName != "")
                        obj_MobSelfCompliance.Town = HttpUtility.HtmlEncode(obj_Town.TownName);
                    if (obj_Village.VillageName != "")
                        obj_MobSelfCompliance.Village = HttpUtility.HtmlEncode(obj_Village.VillageName);
                    obj_MobSelfCompliance.NOCValidatation = "(" + Convert.ToDateTime(obj_MiningNewIssusedLetter.ValidityStartDate).ToShortDateString() + " - " + Convert.ToDateTime(obj_MiningNewIssusedLetter.ValidityEndDate).ToShortDateString() + ")";
                    obj_MobSelfCompliance.QtyPerDay = Convert.ToDecimal(obj_MiningNewIssusedLetter.GroundWaterAbsRecommToApprPerDay);
                    obj_MobSelfCompliance.QtyPerYear = Convert.ToDecimal(obj_MiningNewIssusedLetter.GroundWaterAbsRecommToApprPerYear);
                    obj_MobSelfCompliance.CatOfBlock = obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc();
                    obj_MobSelfCompliance.ComplianceSubmitDate = obj_selfCompliance.ComplianceSubmitDate;
                    obj_MobSelfCompliance.PresentGroundWaterReq = obj_selfCompliance.PresentGroundWaterReq;
                    obj_MobSelfCompliance.PresentGroundWaterReqInDay = obj_selfCompliance.PresentGroundWaterReqInDay;
                    obj_MobSelfCompliance.PresentGroundWaterReqInYear = obj_selfCompliance.PresentGroundWaterReqInYear;
                    obj_MobSelfCompliance.WaterMetFitted = obj_selfCompliance.WaterMetFitted;
                    obj_MobSelfCompliance.WaterMetFittedDesc = obj_selfCompliance.WaterMetFittedDesc;
                    obj_MobSelfCompliance.GWQualityPreMonsoon = obj_selfCompliance.GWQualityPreMonsoon;
                    obj_MobSelfCompliance.GWQualityPreMonsoonDesc = obj_selfCompliance.GWQualityPreMonsoonDesc;
                    obj_MobSelfCompliance.RWHArtificialRecharge = obj_selfCompliance.RWHArtificialRecharge;
                    obj_MobSelfCompliance.RWHArtificialRechargeNo = obj_selfCompliance.RWHArtificialRechargeNo;
                    obj_MobSelfCompliance.RWHArtificialRechargeCapacity = obj_selfCompliance.RWHArtificialRechargeCapacity;
                    obj_MobSelfCompliance.RWHArtificialRechargeDesc = obj_selfCompliance.RWHArtificialRechargeDesc;
                    obj_MobSelfCompliance.PhotoGraph = obj_selfCompliance.PhotoGraph;
                    obj_MobSelfCompliance.PiezoAWLRTelemetry = obj_selfCompliance.PiezoAWLRTelemetry;
                    obj_MobSelfCompliance.PiezoAWLRTelemetryDesc = obj_selfCompliance.PiezoAWLRTelemetryDesc;
                    obj_MobSelfCompliance.RecycleReuse = obj_selfCompliance.RecycleReuse;
                    obj_MobSelfCompliance.RecycleReuseInDay = obj_selfCompliance.RecycleReuseInDay;
                    obj_MobSelfCompliance.RecycleReuseInYear = obj_selfCompliance.RecycleReuseInYear;
                    obj_MobSelfCompliance.RecycleReuseDesc = obj_selfCompliance.RecycleReuseDesc;
                    obj_MobSelfCompliance.ActionTakenReportWithinOneYear = obj_selfCompliance.ActionTakenReportWithinOneYear;
                    obj_MobSelfCompliance.ActionTakenReportWithinOneYearDesc = obj_selfCompliance.ActionTakenReportWithinOneYearDesc;
                    obj_MobSelfCompliance.Remarks = obj_selfCompliance.Remarks;
                    obj_MobSelfCompliance.CreatedOnByUC = obj_selfCompliance.CreatedOnByUC;
                    obj_MobSelfCompliance.CreatedByUC = obj_selfCompliance.CreatedByUC;
                    obj_MobSelfCompliance.CreatedOnByExUC = obj_selfCompliance.CreatedOnByExUC;
                    obj_MobSelfCompliance.CreatedByExUC = obj_selfCompliance.CreatedByExUC;
                    obj_MobSelfCompliance.SubmittedOnByUC = obj_selfCompliance.SubmittedOnByUC;
                    obj_MobSelfCompliance.SubmittedByUC = obj_selfCompliance.SubmittedByUC;
                    obj_MobSelfCompliance.SubmittedOnByExUC = obj_selfCompliance.SubmittedOnByExUC;
                    obj_MobSelfCompliance.SubmittedByExUC = obj_selfCompliance.SubmittedByExUC;
                    obj_MobSelfCompliance.ModifiedOnByUC = obj_selfCompliance.ModifiedOnByUC;
                    obj_MobSelfCompliance.ModifiedByUC = obj_selfCompliance.ModifiedByUC;
                    obj_MobSelfCompliance.ModifiedOnByExUC = obj_selfCompliance.ModifiedOnByExUC;
                    obj_MobSelfCompliance.ModifiedByExUC = obj_selfCompliance.ModifiedByExUC;
                    obj_MobSelfCompliance.CustumMessage = obj_selfCompliance.CustumMessage;
                    obj_MobSelfCompliance.Status = 1;
                    #region Amardeep

                    objA_MobSelfCompliance.NOCNo = obj_selfCompliance.NOCNo;
                    objA_MobSelfCompliance.ValidityStartDate = obj_selfCompliance.ValidityStartDate;
                    objA_MobSelfCompliance.ValidityEndDate = obj_selfCompliance.ValidityEndDate;
                    objA_MobSelfCompliance.GroundWaterAbsDayAppr = obj_selfCompliance.GroundWaterAbsDayAppr;
                    objA_MobSelfCompliance.GroundWaterDewDayAppr = obj_selfCompliance.GroundWaterDewDayAppr;
                    objA_MobSelfCompliance.GroundWaterAbsYearAppr = obj_selfCompliance.GroundWaterAbsYearAppr;
                    objA_MobSelfCompliance.GroundWaterDewYearAppr = obj_selfCompliance.GroundWaterDewYearAppr;
                    objA_MobSelfCompliance.InspectionAgencyCode = obj_selfCompliance.InspectionAgencyCode;
                    objA_MobSelfCompliance.AnyOtherAgency = obj_selfCompliance.AnyOtherAgency;
                    objA_MobSelfCompliance.DateOfInspection = obj_selfCompliance.DateOfInspection;
                    objA_MobSelfCompliance.PreGroundWaterDewReqInDay = obj_selfCompliance.PreGroundWaterDewReqInDay;
                    objA_MobSelfCompliance.PreGroundWaterDewReqInYear = obj_selfCompliance.PreGroundWaterDewReqInYear;
                    objA_MobSelfCompliance.PreGroundWaterAnyVariReqInDay = obj_selfCompliance.PreGroundWaterAnyVariReqInDay;
                    objA_MobSelfCompliance.PreGroundWaterAnyVariReqInYear = obj_selfCompliance.PreGroundWaterAnyVariReqInYear;
                    objA_MobSelfCompliance.AbstraStructExisting = obj_selfCompliance.AbstraStructExisting;
                    objA_MobSelfCompliance.AbstraStructProposed = obj_selfCompliance.AbstraStructProposed;
                    objA_MobSelfCompliance.NoAbsDewStrucAtPresent = obj_selfCompliance.NoAbsDewStrucAtPresent;
                    objA_MobSelfCompliance.FuncAbstStruct = obj_selfCompliance.FuncAbstStruct;
                    objA_MobSelfCompliance.MeterTypeCode = obj_selfCompliance.MeterTypeCode;
                    objA_MobSelfCompliance.NumberOfFunMeter = obj_selfCompliance.NumberOfFunMeter;
                    objA_MobSelfCompliance.TypeOfAbstStructCode = obj_selfCompliance.TypeOfAbstStructCode;
                    objA_MobSelfCompliance.NumberOfStruct = obj_selfCompliance.NumberOfStruct;
                    objA_MobSelfCompliance.QuantOfRecharge = obj_selfCompliance.QuantOfRecharge;
                    objA_MobSelfCompliance.NoOfPiezo = obj_selfCompliance.NoOfPiezo;
                    objA_MobSelfCompliance.NoOfPiezoDWLR = obj_selfCompliance.NoOfPiezoDWLR;
                    objA_MobSelfCompliance.NoOfPiezoTelem = obj_selfCompliance.NoOfPiezoTelem;
                    objA_MobSelfCompliance.NoOfObserwell = obj_selfCompliance.NoOfObserwell;
                    objA_MobSelfCompliance.NoOfFunctPiezo = obj_selfCompliance.NoOfFunctPiezo;
                    objA_MobSelfCompliance.QuanttreatWasteWaterIND = obj_selfCompliance.QuanttreatWasteWaterIND;
                    objA_MobSelfCompliance.QuanttreatWasteWaterINDGreen = obj_selfCompliance.QuanttreatWasteWaterINDGreen;
                    objA_MobSelfCompliance.QuanttreatWasteWaterINDOther = obj_selfCompliance.QuanttreatWasteWaterINDOther;
                    objA_MobSelfCompliance.AuditAgency = obj_selfCompliance.AuditAgency;
                    objA_MobSelfCompliance.DateOfInspectionAudit = obj_selfCompliance.DateOfInspectionAudit;
                    objA_MobSelfCompliance.AnyViolationNOCCondiDesc = obj_selfCompliance.AnyViolationNOCCondiDesc;
                    objA_MobSelfCompliance.AnyOtherCompliancesDesc = obj_selfCompliance.AnyOtherCompliancesDesc;
                    objA_MobSelfCompliance.WellFittedWithWaterFlowMeterAttCode = obj_selfCompliance.WellFittedWithWaterFlowMeterAttCode;
                    objA_MobSelfCompliance.GeoRainWaterHarvRechAttCode = obj_selfCompliance.GeoRainWaterHarvRechAttCode;
                    objA_MobSelfCompliance.GroundWaterQualityAttCode = obj_selfCompliance.GroundWaterQualityAttCode;
                    objA_MobSelfCompliance.GroundwaterAbstractionDataAttCode = obj_selfCompliance.GroundwaterAbstractionDataAttCode;
                    objA_MobSelfCompliance.NOCAttCode = obj_selfCompliance.NOCAttCode;
                    objA_MobSelfCompliance.CopySiteInspectionAttCode = obj_selfCompliance.CopySiteInspectionAttCode;
                    objA_MobSelfCompliance.GroundwaterMonitoringAttCode = obj_selfCompliance.GroundwaterMonitoringAttCode;
                    objA_MobSelfCompliance.PhotoETPSTPAttCode = obj_selfCompliance.PhotoETPSTPAttCode;
                    objA_MobSelfCompliance.WaterAuditAttCode = obj_selfCompliance.WaterAuditAttCode;
                    objA_MobSelfCompliance.IARModelingAttCode = obj_selfCompliance.IARModelingAttCode;
                    objA_MobSelfCompliance.ExtraAttCode = obj_selfCompliance.ExtraAttCode;



                    objA_MobSelfCompliance.InspectionReport = obj_selfCompliance.InspectionReport;
                    objA_MobSelfCompliance.PreGroundWaterAnyVari = obj_selfCompliance.PreGroundWaterAnyVari;
                    objA_MobSelfCompliance.AbstrDataSubmittedTW = obj_selfCompliance.AbstrDataSubmittedTW;
                    objA_MobSelfCompliance.StructPhoto = obj_selfCompliance.StructPhoto;

                    objA_MobSelfCompliance.AbstStructFittedWithWM = obj_selfCompliance.AbstStructFittedWithWM;
                    objA_MobSelfCompliance.TelemInstalled = obj_selfCompliance.TelemInstalled;
                    objA_MobSelfCompliance.AnnuCalibOfWM = obj_selfCompliance.AnnuCalibOfWM;
                    objA_MobSelfCompliance.PhotoWellFittedWithWM = obj_selfCompliance.PhotoWellFittedWithWM;

                    objA_MobSelfCompliance.GWQuality = obj_selfCompliance.GWQuality;
                    objA_MobSelfCompliance.MineSeepageQuality = obj_selfCompliance.MineSeepageQuality;
                    objA_MobSelfCompliance.WaterSampleAnalyzed = obj_selfCompliance.WaterSampleAnalyzed;
                    objA_MobSelfCompliance.GWReportWithinTime = obj_selfCompliance.GWReportWithinTime;

                    objA_MobSelfCompliance.WithOutPremises = obj_selfCompliance.WithOutPremises;
                    objA_MobSelfCompliance.PhotoRechargeStruct = obj_selfCompliance.PhotoRechargeStruct;
                    objA_MobSelfCompliance.GeoPiezoAWLRTelem = obj_selfCompliance.GeoPiezoAWLRTelem;
                    objA_MobSelfCompliance.MoniDataSubmitted = obj_selfCompliance.MoniDataSubmitted;

                    objA_MobSelfCompliance.GeoPhotoOfSTP = obj_selfCompliance.GeoPhotoOfSTP;
                    objA_MobSelfCompliance.SubSCWithinTimeFrame = obj_selfCompliance.SubSCWithinTimeFrame;
                    objA_MobSelfCompliance.WaterAuditInspection = obj_selfCompliance.WaterAuditInspection;
                    objA_MobSelfCompliance.WaterAuditReport = obj_selfCompliance.WaterAuditReport;

                    objA_MobSelfCompliance.ImpactAssementReport = obj_selfCompliance.ImpactAssementReport;
                    objA_MobSelfCompliance.ImpactAssementAIRReport = obj_selfCompliance.ImpactAssementAIRReport;
                    objA_MobSelfCompliance.AnyViolationNOCCondi = obj_selfCompliance.AnyViolationNOCCondi;
                    objA_MobSelfCompliance.AnyOtherCompliances = obj_selfCompliance.AnyOtherCompliances;

                    obj_MobSelfCompliance.ActionTakenReportWithinOneYearDesc = obj_selfCompliance.ActionTakenReportWithinOneYearDesc;
                    obj_MobSelfCompliance.PreSelfsentGroundWaterReqInYear = obj_selfCompliance.PreSelfsentGroundWaterReqInYear;
                    obj_MobSelfCompliance.AbstraStructExistingAsPerNOC = obj_selfCompliance.AbstraStructExistingAsPerNOC;
                    obj_MobSelfCompliance.AbstraStructProposedAsPerNOC = obj_selfCompliance.AbstraStructProposedAsPerNOC;
                    obj_MobSelfCompliance.NumberOfFunMeter = obj_selfCompliance.NumberOfFunMeter;
                    obj_MobSelfCompliance.NoOfSelfPiezoTelem = obj_selfCompliance.NoOfSelfPiezoTelem;
                    obj_MobSelfCompliance.NoOfSelfPiezo = obj_selfCompliance.NoOfSelfPiezo;
                    obj_MobSelfCompliance.NoOfPiezoDWLRTelem = obj_selfCompliance.NoOfPiezoDWLRTelem;
                    obj_MobSelfCompliance.NoOfSelfPiezoDWLRTelem = obj_selfCompliance.NoOfSelfPiezoDWLRTelem;
                    obj_MobSelfCompliance.GroundWaterMonitoringDataAttCode = obj_selfCompliance.GroundWaterMonitoringDataAttCode;
                    obj_MobSelfCompliance.MiningSeepageAttCode = obj_selfCompliance.MiningSeepageAttCode;
                    obj_MobSelfCompliance.AnnualCalibrationAttCode = obj_selfCompliance.AnnualCalibrationAttCode;
                    obj_MobSelfCompliance.VerifiedBy = obj_selfCompliance.VerifiedBy;
                    obj_MobSelfCompliance.VerifiedOn = obj_selfCompliance.VerifiedOn;


                    #endregion
                    return Request.CreateResponse<MobSelfCompliance>(System.Net.HttpStatusCode.OK, obj_MobSelfCompliance);
                }
                else
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfComplianceEmpty);

                #endregion
            }
            else
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfComplianceEmpty);

        }
        else if (obj_MiningRenewApplication != null)
        {
            if (objA_MobSelfCompliance.AppliedFor.ToLower() == "renewal")
            {
                #region MIN Renew

                obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(obj_MiningRenewApplication.MiningRenewApplicationCode);
                if (obj_selfCompliance.ApplicationCode != 0 && obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                {
                    obj_MobSelfCompliance = new MobSelfCompliance();
                    obj_MobSelfCompliance.ApplicationCode = obj_selfCompliance.ApplicationCode;

                    obj_MobSelfCompliance.NOCNo = objA_MobSelfCompliance.NOCNo;

                    NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter obj_MiningRenewIssusedLetter = new NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter(obj_selfCompliance.ApplicationCode); //obj_MiningRenewApplication.GetIssuedLetter();

                    obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_MiningRenewApplication.FirstApplicationCode);
                    NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningRenewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);

                    obj_MobSelfCompliance.AppliedFor = HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(obj_MiningRenewApplication.LinkDepth));
                    obj_MobSelfCompliance.ProjectName = obj_MiningNewApplication.NameOfMining;
                    obj_MobSelfCompliance.AppNo = obj_MiningNewApplication.MiningNewApplicationNumber;
                    obj_MobSelfCompliance.AddressLine1 = HttpUtility.HtmlEncode(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1);
                    obj_MobSelfCompliance.AddressLine2 = HttpUtility.HtmlEncode(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2);
                    obj_MobSelfCompliance.AddressLine3 = HttpUtility.HtmlEncode(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3);
                    obj_MobSelfCompliance.StateName = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.GetState().StateName));
                    obj_MobSelfCompliance.DistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.District(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName));
                    obj_MobSelfCompliance.SubDistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName));
                    obj_Town = new NOCAP.BLL.Master.Town(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
                    obj_Village = new NOCAP.BLL.Master.Village(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);
                    if (obj_Town.TownName != "")
                        obj_MobSelfCompliance.Town = HttpUtility.HtmlEncode(obj_Town.TownName);
                    if (obj_Village.VillageName != "")
                        obj_MobSelfCompliance.Village = HttpUtility.HtmlEncode(obj_Village.VillageName);
                    obj_MobSelfCompliance.NOCValidatation = "(" + Convert.ToDateTime(obj_MiningRenewIssusedLetter.ValidityStartDate).ToShortDateString() + " - " + Convert.ToDateTime(obj_MiningRenewIssusedLetter.ValidityEndDate).ToShortDateString() + ")";
                    obj_MobSelfCompliance.QtyPerDay = Convert.ToDecimal(obj_MiningRenewIssusedLetter.GroundWaterAbsRecommToApprPerDay);
                    obj_MobSelfCompliance.QtyPerYear = Convert.ToDecimal(obj_MiningRenewIssusedLetter.GroundWaterAbsRecommToApprPerYear);
                    obj_MobSelfCompliance.CatOfBlock = obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc();
                    obj_MobSelfCompliance.ComplianceSubmitDate = obj_selfCompliance.ComplianceSubmitDate;
                    obj_MobSelfCompliance.PresentGroundWaterReq = obj_selfCompliance.PresentGroundWaterReq;
                    obj_MobSelfCompliance.PresentGroundWaterReqInDay = obj_selfCompliance.PresentGroundWaterReqInDay;
                    obj_MobSelfCompliance.PresentGroundWaterReqInYear = obj_selfCompliance.PresentGroundWaterReqInYear;
                    obj_MobSelfCompliance.WaterMetFitted = obj_selfCompliance.WaterMetFitted;
                    obj_MobSelfCompliance.WaterMetFittedDesc = obj_selfCompliance.WaterMetFittedDesc;
                    obj_MobSelfCompliance.GWQualityPreMonsoon = obj_selfCompliance.GWQualityPreMonsoon;
                    obj_MobSelfCompliance.GWQualityPreMonsoonDesc = obj_selfCompliance.GWQualityPreMonsoonDesc;
                    obj_MobSelfCompliance.RWHArtificialRecharge = obj_selfCompliance.RWHArtificialRecharge;
                    obj_MobSelfCompliance.RWHArtificialRechargeNo = obj_selfCompliance.RWHArtificialRechargeNo;
                    obj_MobSelfCompliance.RWHArtificialRechargeCapacity = obj_selfCompliance.RWHArtificialRechargeCapacity;
                    obj_MobSelfCompliance.RWHArtificialRechargeDesc = obj_selfCompliance.RWHArtificialRechargeDesc;
                    obj_MobSelfCompliance.PhotoGraph = obj_selfCompliance.PhotoGraph;
                    obj_MobSelfCompliance.PiezoAWLRTelemetry = obj_selfCompliance.PiezoAWLRTelemetry;
                    obj_MobSelfCompliance.PiezoAWLRTelemetryDesc = obj_selfCompliance.PiezoAWLRTelemetryDesc;
                    obj_MobSelfCompliance.RecycleReuse = obj_selfCompliance.RecycleReuse;
                    obj_MobSelfCompliance.RecycleReuseInDay = obj_selfCompliance.RecycleReuseInDay;
                    obj_MobSelfCompliance.RecycleReuseInYear = obj_selfCompliance.RecycleReuseInYear;
                    obj_MobSelfCompliance.RecycleReuseDesc = obj_selfCompliance.RecycleReuseDesc;
                    obj_MobSelfCompliance.ActionTakenReportWithinOneYear = obj_selfCompliance.ActionTakenReportWithinOneYear;
                    obj_MobSelfCompliance.ActionTakenReportWithinOneYearDesc = obj_selfCompliance.ActionTakenReportWithinOneYearDesc;
                    obj_MobSelfCompliance.Remarks = obj_selfCompliance.Remarks;
                    obj_MobSelfCompliance.CreatedOnByUC = obj_selfCompliance.CreatedOnByUC;
                    obj_MobSelfCompliance.CreatedByUC = obj_selfCompliance.CreatedByUC;
                    obj_MobSelfCompliance.CreatedOnByExUC = obj_selfCompliance.CreatedOnByExUC;
                    obj_MobSelfCompliance.CreatedByExUC = obj_selfCompliance.CreatedByExUC;
                    obj_MobSelfCompliance.SubmittedOnByUC = obj_selfCompliance.SubmittedOnByUC;
                    obj_MobSelfCompliance.SubmittedByUC = obj_selfCompliance.SubmittedByUC;
                    obj_MobSelfCompliance.SubmittedOnByExUC = obj_selfCompliance.SubmittedOnByExUC;
                    obj_MobSelfCompliance.SubmittedByExUC = obj_selfCompliance.SubmittedByExUC;
                    obj_MobSelfCompliance.ModifiedOnByUC = obj_selfCompliance.ModifiedOnByUC;
                    obj_MobSelfCompliance.ModifiedByUC = obj_selfCompliance.ModifiedByUC;
                    obj_MobSelfCompliance.ModifiedOnByExUC = obj_selfCompliance.ModifiedOnByExUC;
                    obj_MobSelfCompliance.ModifiedByExUC = obj_selfCompliance.ModifiedByExUC;
                    obj_MobSelfCompliance.CustumMessage = obj_selfCompliance.CustumMessage;
                    obj_MobSelfCompliance.Status = 1;
                    #region Amardeep

                    objA_MobSelfCompliance.NOCNo = obj_selfCompliance.NOCNo;
                    objA_MobSelfCompliance.ValidityStartDate = obj_selfCompliance.ValidityStartDate;
                    objA_MobSelfCompliance.ValidityEndDate = obj_selfCompliance.ValidityEndDate;
                    objA_MobSelfCompliance.GroundWaterAbsDayAppr = obj_selfCompliance.GroundWaterAbsDayAppr;
                    objA_MobSelfCompliance.GroundWaterDewDayAppr = obj_selfCompliance.GroundWaterDewDayAppr;
                    objA_MobSelfCompliance.GroundWaterAbsYearAppr = obj_selfCompliance.GroundWaterAbsYearAppr;
                    objA_MobSelfCompliance.GroundWaterDewYearAppr = obj_selfCompliance.GroundWaterDewYearAppr;
                    objA_MobSelfCompliance.InspectionAgencyCode = obj_selfCompliance.InspectionAgencyCode;
                    objA_MobSelfCompliance.AnyOtherAgency = obj_selfCompliance.AnyOtherAgency;
                    objA_MobSelfCompliance.DateOfInspection = obj_selfCompliance.DateOfInspection;
                    objA_MobSelfCompliance.PreGroundWaterDewReqInDay = obj_selfCompliance.PreGroundWaterDewReqInDay;
                    objA_MobSelfCompliance.PreGroundWaterDewReqInYear = obj_selfCompliance.PreGroundWaterDewReqInYear;
                    objA_MobSelfCompliance.PreGroundWaterAnyVariReqInDay = obj_selfCompliance.PreGroundWaterAnyVariReqInDay;
                    objA_MobSelfCompliance.PreGroundWaterAnyVariReqInYear = obj_selfCompliance.PreGroundWaterAnyVariReqInYear;
                    objA_MobSelfCompliance.AbstraStructExisting = obj_selfCompliance.AbstraStructExisting;
                    objA_MobSelfCompliance.AbstraStructProposed = obj_selfCompliance.AbstraStructProposed;
                    objA_MobSelfCompliance.NoAbsDewStrucAtPresent = obj_selfCompliance.NoAbsDewStrucAtPresent;
                    objA_MobSelfCompliance.FuncAbstStruct = obj_selfCompliance.FuncAbstStruct;
                    objA_MobSelfCompliance.MeterTypeCode = obj_selfCompliance.MeterTypeCode;
                    objA_MobSelfCompliance.NumberOfFunMeter = obj_selfCompliance.NumberOfFunMeter;
                    objA_MobSelfCompliance.TypeOfAbstStructCode = obj_selfCompliance.TypeOfAbstStructCode;
                    objA_MobSelfCompliance.NumberOfStruct = obj_selfCompliance.NumberOfStruct;
                    objA_MobSelfCompliance.QuantOfRecharge = obj_selfCompliance.QuantOfRecharge;
                    objA_MobSelfCompliance.NoOfPiezo = obj_selfCompliance.NoOfPiezo;
                    objA_MobSelfCompliance.NoOfPiezoDWLR = obj_selfCompliance.NoOfPiezoDWLR;
                    objA_MobSelfCompliance.NoOfPiezoTelem = obj_selfCompliance.NoOfPiezoTelem;
                    objA_MobSelfCompliance.NoOfObserwell = obj_selfCompliance.NoOfObserwell;
                    objA_MobSelfCompliance.NoOfFunctPiezo = obj_selfCompliance.NoOfFunctPiezo;
                    objA_MobSelfCompliance.QuanttreatWasteWaterIND = obj_selfCompliance.QuanttreatWasteWaterIND;
                    objA_MobSelfCompliance.QuanttreatWasteWaterINDGreen = obj_selfCompliance.QuanttreatWasteWaterINDGreen;
                    objA_MobSelfCompliance.QuanttreatWasteWaterINDOther = obj_selfCompliance.QuanttreatWasteWaterINDOther;
                    objA_MobSelfCompliance.AuditAgency = obj_selfCompliance.AuditAgency;
                    objA_MobSelfCompliance.DateOfInspectionAudit = obj_selfCompliance.DateOfInspectionAudit;
                    objA_MobSelfCompliance.AnyViolationNOCCondiDesc = obj_selfCompliance.AnyViolationNOCCondiDesc;
                    objA_MobSelfCompliance.AnyOtherCompliancesDesc = obj_selfCompliance.AnyOtherCompliancesDesc;
                    objA_MobSelfCompliance.WellFittedWithWaterFlowMeterAttCode = obj_selfCompliance.WellFittedWithWaterFlowMeterAttCode;
                    objA_MobSelfCompliance.GeoRainWaterHarvRechAttCode = obj_selfCompliance.GeoRainWaterHarvRechAttCode;
                    objA_MobSelfCompliance.GroundWaterQualityAttCode = obj_selfCompliance.GroundWaterQualityAttCode;
                    objA_MobSelfCompliance.GroundwaterAbstractionDataAttCode = obj_selfCompliance.GroundwaterAbstractionDataAttCode;
                    objA_MobSelfCompliance.NOCAttCode = obj_selfCompliance.NOCAttCode;
                    objA_MobSelfCompliance.CopySiteInspectionAttCode = obj_selfCompliance.CopySiteInspectionAttCode;
                    objA_MobSelfCompliance.GroundwaterMonitoringAttCode = obj_selfCompliance.GroundwaterMonitoringAttCode;
                    objA_MobSelfCompliance.PhotoETPSTPAttCode = obj_selfCompliance.PhotoETPSTPAttCode;
                    objA_MobSelfCompliance.WaterAuditAttCode = obj_selfCompliance.WaterAuditAttCode;
                    objA_MobSelfCompliance.IARModelingAttCode = obj_selfCompliance.IARModelingAttCode;
                    objA_MobSelfCompliance.ExtraAttCode = obj_selfCompliance.ExtraAttCode;



                    objA_MobSelfCompliance.InspectionReport = obj_selfCompliance.InspectionReport;
                    objA_MobSelfCompliance.PreGroundWaterAnyVari = obj_selfCompliance.PreGroundWaterAnyVari;
                    objA_MobSelfCompliance.AbstrDataSubmittedTW = obj_selfCompliance.AbstrDataSubmittedTW;
                    objA_MobSelfCompliance.StructPhoto = obj_selfCompliance.StructPhoto;

                    objA_MobSelfCompliance.AbstStructFittedWithWM = obj_selfCompliance.AbstStructFittedWithWM;
                    objA_MobSelfCompliance.TelemInstalled = obj_selfCompliance.TelemInstalled;
                    objA_MobSelfCompliance.AnnuCalibOfWM = obj_selfCompliance.AnnuCalibOfWM;
                    objA_MobSelfCompliance.PhotoWellFittedWithWM = obj_selfCompliance.PhotoWellFittedWithWM;

                    objA_MobSelfCompliance.GWQuality = obj_selfCompliance.GWQuality;
                    objA_MobSelfCompliance.MineSeepageQuality = obj_selfCompliance.MineSeepageQuality;
                    objA_MobSelfCompliance.WaterSampleAnalyzed = obj_selfCompliance.WaterSampleAnalyzed;
                    objA_MobSelfCompliance.GWReportWithinTime = obj_selfCompliance.GWReportWithinTime;

                    objA_MobSelfCompliance.WithOutPremises = obj_selfCompliance.WithOutPremises;
                    objA_MobSelfCompliance.PhotoRechargeStruct = obj_selfCompliance.PhotoRechargeStruct;
                    objA_MobSelfCompliance.GeoPiezoAWLRTelem = obj_selfCompliance.GeoPiezoAWLRTelem;
                    objA_MobSelfCompliance.MoniDataSubmitted = obj_selfCompliance.MoniDataSubmitted;

                    objA_MobSelfCompliance.GeoPhotoOfSTP = obj_selfCompliance.GeoPhotoOfSTP;
                    objA_MobSelfCompliance.SubSCWithinTimeFrame = obj_selfCompliance.SubSCWithinTimeFrame;
                    objA_MobSelfCompliance.WaterAuditInspection = obj_selfCompliance.WaterAuditInspection;
                    objA_MobSelfCompliance.WaterAuditReport = obj_selfCompliance.WaterAuditReport;

                    objA_MobSelfCompliance.ImpactAssementReport = obj_selfCompliance.ImpactAssementReport;
                    objA_MobSelfCompliance.ImpactAssementAIRReport = obj_selfCompliance.ImpactAssementAIRReport;
                    objA_MobSelfCompliance.AnyViolationNOCCondi = obj_selfCompliance.AnyViolationNOCCondi;
                    objA_MobSelfCompliance.AnyOtherCompliances = obj_selfCompliance.AnyOtherCompliances;



                    obj_MobSelfCompliance.ActionTakenReportWithinOneYearDesc = obj_selfCompliance.ActionTakenReportWithinOneYearDesc;
                    obj_MobSelfCompliance.PreSelfsentGroundWaterReqInYear = obj_selfCompliance.PreSelfsentGroundWaterReqInYear;
                    obj_MobSelfCompliance.AbstraStructExistingAsPerNOC = obj_selfCompliance.AbstraStructExistingAsPerNOC;
                    obj_MobSelfCompliance.AbstraStructProposedAsPerNOC = obj_selfCompliance.AbstraStructProposedAsPerNOC;
                    obj_MobSelfCompliance.NumberOfFunMeter = obj_selfCompliance.NumberOfFunMeter;
                    obj_MobSelfCompliance.NoOfSelfPiezoTelem = obj_selfCompliance.NoOfSelfPiezoTelem;
                    obj_MobSelfCompliance.NoOfSelfPiezo = obj_selfCompliance.NoOfSelfPiezo;
                    obj_MobSelfCompliance.NoOfPiezoDWLRTelem = obj_selfCompliance.NoOfPiezoDWLRTelem;
                    obj_MobSelfCompliance.NoOfSelfPiezoDWLRTelem = obj_selfCompliance.NoOfSelfPiezoDWLRTelem;
                    obj_MobSelfCompliance.GroundWaterMonitoringDataAttCode = obj_selfCompliance.GroundWaterMonitoringDataAttCode;
                    obj_MobSelfCompliance.MiningSeepageAttCode = obj_selfCompliance.MiningSeepageAttCode;
                    obj_MobSelfCompliance.AnnualCalibrationAttCode = obj_selfCompliance.AnnualCalibrationAttCode;
                    obj_MobSelfCompliance.VerifiedBy = obj_selfCompliance.VerifiedBy;
                    obj_MobSelfCompliance.VerifiedOn = obj_selfCompliance.VerifiedOn;
                    #endregion
                    return Request.CreateResponse<MobSelfCompliance>(System.Net.HttpStatusCode.OK, obj_MobSelfCompliance);
                }
                else
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfComplianceEmpty);

                #endregion
            }
            else
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfComplianceEmpty);
        }
        else
            return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, "Invalid user credentials");
    }


    #region SelfComplianceAttachment
    [HttpPost]
    public HttpResponseMessage GetSelfComplianceNOCAttachment([FromBody] MobSelfCompliance objA_MobSelfCompliance)
    {
        NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_SelfComplianceAttachment = null;

        if (objA_MobSelfCompliance.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(objA_MobSelfCompliance.ApplicationCode);

            arr_SelfComplianceAttachment = obj_selfCompliance.GetNOCAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfComplianceAttachment);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }

    }

    [HttpPost]
    public HttpResponseMessage GetSelfComplianceSiteInspectionAttachment([FromBody] MobSelfCompliance objA_MobSelfCompliance)
    {
        NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_SelfComplianceAttachment = null;

        if (objA_MobSelfCompliance.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(objA_MobSelfCompliance.ApplicationCode);

            arr_SelfComplianceAttachment = obj_selfCompliance.GetCopySiteInspectionAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfComplianceAttachment);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }

    [HttpPost]
    public HttpResponseMessage GetSelfComplianceGeoPhotoWithdrawalStructAttachment([FromBody] MobSelfCompliance objA_MobSelfCompliance)
    {
        NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_SelfComplianceAttachment = null;
        if (objA_MobSelfCompliance.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(objA_MobSelfCompliance.ApplicationCode);

            arr_SelfComplianceAttachment = obj_selfCompliance.GetGroundwaterAbstractionDataAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfComplianceAttachment);
        }

        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }

    [HttpPost]
    public HttpResponseMessage GetSelfComplianceAnnualCalibrationAttachment([FromBody] MobSelfCompliance objA_MobSelfCompliance)
    {
        NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_SelfComplianceAttachment = null;
        if (objA_MobSelfCompliance.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(objA_MobSelfCompliance.ApplicationCode);

            arr_SelfComplianceAttachment = obj_selfCompliance.GetAnnualCalibrationAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfComplianceAttachment);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }

    [HttpPost]
    public HttpResponseMessage GetSelfComplianceGeoPhotowellFittedAttachment([FromBody] MobSelfCompliance objA_MobSelfCompliance)
    {
        NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_SelfComplianceAttachment = null;
        if (objA_MobSelfCompliance.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(objA_MobSelfCompliance.ApplicationCode);

            arr_SelfComplianceAttachment = obj_selfCompliance.GetWellFittedWithWaterFlowMeterAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfComplianceAttachment);
        }

        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
    [HttpPost]
    public HttpResponseMessage GetSelfComplianceGWQualityReporAttachment([FromBody] MobSelfCompliance objA_MobSelfCompliance)
    {
        NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_SelfComplianceAttachment = null;
        if (objA_MobSelfCompliance.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(objA_MobSelfCompliance.ApplicationCode);

            arr_SelfComplianceAttachment = obj_selfCompliance.GetGroundWaterQualityAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfComplianceAttachment);
        }

        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
    [HttpPost]
    public HttpResponseMessage GetSelfComplianceMineSeepageQualityReportAttachment([FromBody] MobSelfCompliance objA_MobSelfCompliance)
    {
        NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_SelfComplianceAttachment = null;
        if (objA_MobSelfCompliance.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(objA_MobSelfCompliance.ApplicationCode);

            arr_SelfComplianceAttachment = obj_selfCompliance.GetMiningSeepageAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfComplianceAttachment);
        }

        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
    [HttpPost]
    public HttpResponseMessage GetSelfComplianceGeoPhotoRechargeStructuresAttachment([FromBody] MobSelfCompliance objA_MobSelfCompliance)
    {
        NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_SelfComplianceAttachment = null;
        if (objA_MobSelfCompliance.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(objA_MobSelfCompliance.ApplicationCode);
            arr_SelfComplianceAttachment = obj_selfCompliance.GetGeoRainWaterHarvRechAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfComplianceAttachment);
        }

        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
    [HttpPost]
    public HttpResponseMessage GetSelfComplianceGeoPhotoPiezometersObservationDigitalTelemetryAttachment([FromBody] MobSelfCompliance objA_MobSelfCompliance)
    {
        NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_SelfComplianceAttachment = null;
        if (objA_MobSelfCompliance.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(objA_MobSelfCompliance.ApplicationCode);

            arr_SelfComplianceAttachment = obj_selfCompliance.GetGroundwaterMonitoringAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfComplianceAttachment);
        }

        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
    [HttpPost]
    public HttpResponseMessage GetSelfComplianceGWMonitoringDataAttachment([FromBody] MobSelfCompliance objA_MobSelfCompliance)
    {
        NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_SelfComplianceAttachment = null;
        if (objA_MobSelfCompliance.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(objA_MobSelfCompliance.ApplicationCode);

            arr_SelfComplianceAttachment = obj_selfCompliance.GetGroundWaterMonitoringDataAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfComplianceAttachment);
        }

        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }

    [HttpPost]
    public HttpResponseMessage GetSelfComplianceSewageTreatmentPlantEffluentTreatmentPlantAttachment([FromBody] MobSelfCompliance objA_MobSelfCompliance)
    {
        NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_SelfComplianceAttachment = null;
        if (objA_MobSelfCompliance.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(objA_MobSelfCompliance.ApplicationCode);

            arr_SelfComplianceAttachment = obj_selfCompliance.GetPhotoETPSTPAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfComplianceAttachment);
        }

        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }


    #endregion
    #endregion





    #region  SelfInspection APIs
    //[HttpPost]
    //public HttpResponseMessage GetAppInspectionDetails([FromBody] MobSelfInspection objA_MobSelfInspection)
    //{



    //    NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null;
    //    NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = null;

    //    NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = null;
    //    NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = null;
    //    NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = null;
    //    NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = null;

    //    NOCAP.BLL.Master.Town obj_Town = null;
    //    NOCAP.BLL.Master.Village obj_Village = null;

    //    NOCAP.BLL.Utility.Utility.GetAppplicationObjectForNOCNo(out obj_IndustrialNewApplication,
    //        out obj_InfrastructureNewApplication, out obj_MiningNewApplication,
    //       out obj_IndustrialRenewApplication, out obj_InfrastructureRenewApplication, out obj_MiningRenewApplication, objA_MobSelfInspection.NOCNo);
    //    NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = null;
    //    MobSelfInspection obj_MobSelfInspection = null;
    //    MobSelfInspection obj_MobSelfInspectionEmpty = new MobSelfInspection();


    //    if (obj_IndustrialNewApplication != null)
    //    {
    //        if (objA_MobSelfInspection.AppliedFor.ToLower() == "fresh")
    //        {
    //            obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(obj_IndustrialNewApplication.IndustrialNewApplicationCode);
    //            if (obj_selfInspection.ApplicationCode != 0 && obj_selfInspection.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
    //            {
    //                #region IND New 

    //                obj_MobSelfInspection = new MobSelfInspection();
    //                obj_MobSelfInspection.ApplicationCode = obj_selfInspection.ApplicationCode;
    //                obj_MobSelfInspection.NOCNo = objA_MobSelfInspection.NOCNo;
    //                NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_IndustrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter(obj_selfInspection.ApplicationCode);// obj_IndustrialNewApplication.GetIssuedLetter();
    //                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_IndustrialNewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);
    //                obj_MobSelfInspection.AppliedFor = "Fresh";
    //                obj_MobSelfInspection.ProjectName = obj_IndustrialNewApplication.NameOfIndustry;
    //                obj_MobSelfInspection.AppNo = obj_IndustrialNewApplication.IndustrialNewApplicationNumber;
    //                obj_MobSelfInspection.AddressLine1 = HttpUtility.HtmlEncode(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1);
    //                obj_MobSelfInspection.AddressLine2 = HttpUtility.HtmlEncode(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2);
    //                obj_MobSelfInspection.AddressLine3 = HttpUtility.HtmlEncode(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3);
    //                obj_MobSelfInspection.StateName = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().StateName));
    //                obj_MobSelfInspection.DistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.District(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName));
    //                obj_MobSelfInspection.SubDistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.SubDistrict(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName));
    //                obj_Town = new NOCAP.BLL.Master.Town(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
    //                obj_Village = new NOCAP.BLL.Master.Village(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);
    //                if (obj_Town.TownName != "")
    //                    obj_MobSelfInspection.Town = HttpUtility.HtmlEncode(obj_Town.TownName);
    //                if (obj_Village.VillageName != "")
    //                    obj_MobSelfInspection.Village = HttpUtility.HtmlEncode(obj_Village.VillageName);
    //                obj_MobSelfInspection.NOCValidatation = "(" + Convert.ToDateTime(obj_IndustrialNewIssusedLetter.ValidityStartDate).ToShortDateString() + " - " + Convert.ToDateTime(obj_IndustrialNewIssusedLetter.ValidityEndDate).ToShortDateString() + ")";
    //                obj_MobSelfInspection.QtyPerDay = Convert.ToDecimal(obj_IndustrialNewIssusedLetter.GroundWaterAbsRecommToApprPerDay);
    //                obj_MobSelfInspection.QtyPerYear = Convert.ToDecimal(obj_IndustrialNewIssusedLetter.GroundWaterAbsRecommToApprPerYear);
    //                obj_MobSelfInspection.CatOfBlock = obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc();
    //                obj_MobSelfInspection.InspectionSubmitDate = obj_selfInspection.InspectionSubmitDate;
    //                obj_MobSelfInspection.PresentGroundWaterReq = obj_selfInspection.PresentGroundWaterReq;
    //                obj_MobSelfInspection.PresentGroundWaterReqInDay = obj_selfInspection.PresentGroundWaterReqInDay;
    //                obj_MobSelfInspection.PresentGroundWaterReqInYear = obj_selfInspection.PresentGroundWaterReqInYear;
    //                obj_MobSelfInspection.WaterMetFitted = obj_selfInspection.WaterMetFitted;
    //                obj_MobSelfInspection.WaterMetFittedDesc = obj_selfInspection.WaterMetFittedDesc;
    //                obj_MobSelfInspection.GWQualityPreMonsoon = obj_selfInspection.GWQualityPreMonsoon;
    //                obj_MobSelfInspection.GWQualityPreMonsoonDesc = obj_selfInspection.GWQualityPreMonsoonDesc;
    //                obj_MobSelfInspection.RWHArtificialRecharge = obj_selfInspection.RWHArtificialRecharge;
    //                obj_MobSelfInspection.RWHArtificialRechargeNo = obj_selfInspection.RWHArtificialRechargeNo;
    //                obj_MobSelfInspection.RWHArtificialRechargeCapacity = obj_selfInspection.RWHArtificialRechargeCapacity;
    //                obj_MobSelfInspection.RWHArtificialRechargeDesc = obj_selfInspection.RWHArtificialRechargeDesc;
    //                obj_MobSelfInspection.PhotoGraph = obj_selfInspection.PhotoGraph;
    //                obj_MobSelfInspection.PiezoAWLRTelemetry = obj_selfInspection.PiezoAWLRTelemetry;
    //                obj_MobSelfInspection.PiezoAWLRTelemetryDesc = obj_selfInspection.PiezoAWLRTelemetryDesc;
    //                obj_MobSelfInspection.RecycleReuse = obj_selfInspection.RecycleReuse;
    //                obj_MobSelfInspection.RecycleReuseInDay = obj_selfInspection.RecycleReuseInDay;
    //                obj_MobSelfInspection.RecycleReuseInYear = obj_selfInspection.RecycleReuseInYear;
    //                obj_MobSelfInspection.RecycleReuseDesc = obj_selfInspection.RecycleReuseDesc;
    //                obj_MobSelfInspection.ActionTakenReportWithinOneYear = obj_selfInspection.ActionTakenReportWithinOneYear;




    //                obj_MobSelfInspection.ActionTakenReportWithinOneYearDesc = obj_selfInspection.ActionTakenReportWithinOneYearDesc;
    //                obj_MobSelfInspection.PreSelfsentGroundWaterReqInYear = obj_selfInspection.PreSelfsentGroundWaterReqInYear;
    //                obj_MobSelfInspection.AbstraStructExistingAsPerNOC = obj_selfInspection.AbstraStructExistingAsPerNOC;
    //                obj_MobSelfInspection.AbstraStructProposedAsPerNOC = obj_selfInspection.AbstraStructProposedAsPerNOC;
    //                obj_MobSelfInspection.NumberOfFunMeter = obj_selfInspection.NumberOfFunMeter;
    //                obj_MobSelfInspection.NoOfSelfPiezoTelem = obj_selfInspection.NoOfSelfPiezoTelem;
    //                obj_MobSelfInspection.NoOfSelfPiezo = obj_selfInspection.NoOfSelfPiezo;
    //                obj_MobSelfInspection.NoOfPiezoDWLRTelem = obj_selfInspection.NoOfPiezoDWLRTelem;
    //                obj_MobSelfInspection.NoOfSelfPiezoDWLRTelem = obj_selfInspection.NoOfSelfPiezoDWLRTelem;
    //                obj_MobSelfInspection.GroundWaterMonitoringDataAttCode = obj_selfInspection.GroundWaterMonitoringDataAttCode;
    //                obj_MobSelfInspection.MiningSeepageAttCode = obj_selfInspection.MiningSeepageAttCode;
    //                obj_MobSelfInspection.AnnualCalibrationAttCode = obj_selfInspection.AnnualCalibrationAttCode;
    //                obj_MobSelfInspection.VerifiedBy = obj_selfInspection.VerifiedBy;
    //                obj_MobSelfInspection.VerifiedOn = obj_selfInspection.VerifiedOn;



    //                obj_MobSelfInspection.Remarks = obj_selfInspection.Remarks;
    //                obj_MobSelfInspection.CreatedOnByUC = obj_selfInspection.CreatedOnByUC;
    //                obj_MobSelfInspection.CreatedByUC = obj_selfInspection.CreatedByUC;
    //                obj_MobSelfInspection.CreatedOnByExUC = obj_selfInspection.CreatedOnByExUC;
    //                obj_MobSelfInspection.CreatedByExUC = obj_selfInspection.CreatedByExUC;
    //                obj_MobSelfInspection.SubmittedOnByUC = obj_selfInspection.SubmittedOnByUC;
    //                obj_MobSelfInspection.SubmittedByUC = obj_selfInspection.SubmittedByUC;
    //                obj_MobSelfInspection.SubmittedOnByExUC = obj_selfInspection.SubmittedOnByExUC;
    //                obj_MobSelfInspection.SubmittedByExUC = obj_selfInspection.SubmittedByExUC;
    //                obj_MobSelfInspection.ModifiedOnByUC = obj_selfInspection.ModifiedOnByUC;
    //                obj_MobSelfInspection.ModifiedByUC = obj_selfInspection.ModifiedByUC;
    //                obj_MobSelfInspection.ModifiedOnByExUC = obj_selfInspection.ModifiedOnByExUC;
    //                obj_MobSelfInspection.ModifiedByExUC = obj_selfInspection.ModifiedByExUC;
    //                obj_MobSelfInspection.CustumMessage = obj_selfInspection.CustumMessage;
    //                obj_MobSelfInspection.Status = 1;


    //                objA_MobSelfInspection.NOCNo = obj_selfInspection.NOCNo;
    //                objA_MobSelfInspection.ValidityStartDate = obj_selfInspection.ValidityStartDate;
    //                objA_MobSelfInspection.ValidityEndDate = obj_selfInspection.ValidityEndDate;
    //                objA_MobSelfInspection.GroundWaterAbsDayAppr = obj_selfInspection.GroundWaterAbsDayAppr;
    //                objA_MobSelfInspection.GroundWaterDewDayAppr = obj_selfInspection.GroundWaterDewDayAppr;
    //                objA_MobSelfInspection.GroundWaterAbsYearAppr = obj_selfInspection.GroundWaterAbsYearAppr;
    //                objA_MobSelfInspection.GroundWaterDewYearAppr = obj_selfInspection.GroundWaterDewYearAppr;
    //                objA_MobSelfInspection.InspectionAgencyCode = obj_selfInspection.InspectionAgencyCode;
    //                objA_MobSelfInspection.AnyOtherAgency = obj_selfInspection.AnyOtherAgency;
    //                objA_MobSelfInspection.DateOfInspection = obj_selfInspection.DateOfInspection;
    //                objA_MobSelfInspection.PreGroundWaterDewReqInDay = obj_selfInspection.PreGroundWaterDewReqInDay;
    //                objA_MobSelfInspection.PreGroundWaterDewReqInYear = obj_selfInspection.PreGroundWaterDewReqInYear;
    //                objA_MobSelfInspection.PreGroundWaterAnyVariReqInDay = obj_selfInspection.PreGroundWaterAnyVariReqInDay;
    //                objA_MobSelfInspection.PreGroundWaterAnyVariReqInYear = obj_selfInspection.PreGroundWaterAnyVariReqInYear;
    //                objA_MobSelfInspection.AbstraStructExisting = obj_selfInspection.AbstraStructExisting;
    //                objA_MobSelfInspection.AbstraStructProposed = obj_selfInspection.AbstraStructProposed;
    //                objA_MobSelfInspection.NoAbsDewStrucAtPresent = obj_selfInspection.NoAbsDewStrucAtPresent;
    //                objA_MobSelfInspection.FuncAbstStruct = obj_selfInspection.FuncAbstStruct;
    //                objA_MobSelfInspection.MeterTypeCode = obj_selfInspection.MeterTypeCode;
    //                objA_MobSelfInspection.NumberOfFunMeter = obj_selfInspection.NumberOfFunMeter;
    //                objA_MobSelfInspection.TypeOfAbstStructCode = obj_selfInspection.TypeOfAbstStructCode;
    //                objA_MobSelfInspection.NumberOfStruct = obj_selfInspection.NumberOfStruct;
    //                objA_MobSelfInspection.QuantOfRecharge = obj_selfInspection.QuantOfRecharge;
    //                objA_MobSelfInspection.NoOfPiezo = obj_selfInspection.NoOfPiezo;
    //                objA_MobSelfInspection.NoOfPiezoDWLR = obj_selfInspection.NoOfPiezoDWLR;
    //                objA_MobSelfInspection.NoOfPiezoTelem = obj_selfInspection.NoOfPiezoTelem;
    //                objA_MobSelfInspection.NoOfObserwell = obj_selfInspection.NoOfObserwell;
    //                objA_MobSelfInspection.NoOfFunctPiezo = obj_selfInspection.NoOfFunctPiezo;
    //                objA_MobSelfInspection.QuanttreatWasteWaterIND = obj_selfInspection.QuanttreatWasteWaterIND;
    //                objA_MobSelfInspection.QuanttreatWasteWaterINDGreen = obj_selfInspection.QuanttreatWasteWaterINDGreen;
    //                objA_MobSelfInspection.QuanttreatWasteWaterINDOther = obj_selfInspection.QuanttreatWasteWaterINDOther;
    //                objA_MobSelfInspection.AuditAgency = obj_selfInspection.AuditAgency;
    //                objA_MobSelfInspection.DateOfInspectionAudit = obj_selfInspection.DateOfInspectionAudit;
    //                objA_MobSelfInspection.AnyViolationNOCCondiDesc = obj_selfInspection.AnyViolationNOCCondiDesc;
    //                objA_MobSelfInspection.AnyOtherCompliancesDesc = obj_selfInspection.AnyOtherCompliancesDesc;
    //                objA_MobSelfInspection.WellFittedWithWaterFlowMeterAttCode = obj_selfInspection.WellFittedWithWaterFlowMeterAttCode;
    //                objA_MobSelfInspection.GeoRainWaterHarvRechAttCode = obj_selfInspection.GeoRainWaterHarvRechAttCode;
    //                objA_MobSelfInspection.GroundWaterQualityAttCode = obj_selfInspection.GroundWaterQualityAttCode;
    //                objA_MobSelfInspection.GroundwaterAbstractionDataAttCode = obj_selfInspection.GroundwaterAbstractionDataAttCode;
    //                objA_MobSelfInspection.NOCAttCode = obj_selfInspection.NOCAttCode;
    //                objA_MobSelfInspection.CopySiteInspectionAttCode = obj_selfInspection.CopySiteInspectionAttCode;
    //                objA_MobSelfInspection.GroundwaterMonitoringAttCode = obj_selfInspection.GroundwaterMonitoringAttCode;
    //                objA_MobSelfInspection.PhotoETPSTPAttCode = obj_selfInspection.PhotoETPSTPAttCode;
    //                objA_MobSelfInspection.WaterAuditAttCode = obj_selfInspection.WaterAuditAttCode;
    //                objA_MobSelfInspection.IARModelingAttCode = obj_selfInspection.IARModelingAttCode;
    //                objA_MobSelfInspection.ExtraAttCode = obj_selfInspection.ExtraAttCode;



    //                objA_MobSelfInspection.InspectionReport = obj_selfInspection.InspectionReport;
    //                objA_MobSelfInspection.PreGroundWaterAnyVari = obj_selfInspection.PreGroundWaterAnyVari;
    //                objA_MobSelfInspection.AbstrDataSubmittedTW = obj_selfInspection.AbstrDataSubmittedTW;
    //                objA_MobSelfInspection.StructPhoto = obj_selfInspection.StructPhoto;

    //                objA_MobSelfInspection.AbstStructFittedWithWM = obj_selfInspection.AbstStructFittedWithWM;
    //                objA_MobSelfInspection.TelemInstalled = obj_selfInspection.TelemInstalled;
    //                objA_MobSelfInspection.AnnuCalibOfWM = obj_selfInspection.AnnuCalibOfWM;
    //                objA_MobSelfInspection.PhotoWellFittedWithWM = obj_selfInspection.PhotoWellFittedWithWM;

    //                objA_MobSelfInspection.GWQuality = obj_selfInspection.GWQuality;
    //                objA_MobSelfInspection.MineSeepageQuality = obj_selfInspection.MineSeepageQuality;
    //                objA_MobSelfInspection.WaterSampleAnalyzed = obj_selfInspection.WaterSampleAnalyzed;
    //                objA_MobSelfInspection.GWReportWithinTime = obj_selfInspection.GWReportWithinTime;

    //                objA_MobSelfInspection.WithOutPremises = obj_selfInspection.WithOutPremises;
    //                objA_MobSelfInspection.PhotoRechargeStruct = obj_selfInspection.PhotoRechargeStruct;
    //                objA_MobSelfInspection.GeoPiezoAWLRTelem = obj_selfInspection.GeoPiezoAWLRTelem;
    //                objA_MobSelfInspection.MoniDataSubmitted = obj_selfInspection.MoniDataSubmitted;

    //                objA_MobSelfInspection.GeoPhotoOfSTP = obj_selfInspection.GeoPhotoOfSTP;
    //                objA_MobSelfInspection.SubSCWithinTimeFrame = obj_selfInspection.SubSCWithinTimeFrame;
    //                objA_MobSelfInspection.WaterAuditInspection = obj_selfInspection.WaterAuditInspection;
    //                objA_MobSelfInspection.WaterAuditReport = obj_selfInspection.WaterAuditReport;

    //                objA_MobSelfInspection.ImpactAssementReport = obj_selfInspection.ImpactAssementReport;
    //                objA_MobSelfInspection.ImpactAssementAIRReport = obj_selfInspection.ImpactAssementAIRReport;
    //                objA_MobSelfInspection.AnyViolationNOCCondi = obj_selfInspection.AnyViolationNOCCondi;
    //                objA_MobSelfInspection.AnyOtherCompliances = obj_selfInspection.AnyOtherCompliances;
    //                objA_MobSelfInspection.PreSelfsentGroundWaterReqInDay = obj_selfInspection.PreSelfsentGroundWaterReqInDay;


    //                return Request.CreateResponse<MobSelfInspection>(System.Net.HttpStatusCode.OK, obj_MobSelfInspection);
    //                #endregion
    //            }
    //            else
    //                return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfInspectionEmpty);
    //        }
    //        else
    //            return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfInspectionEmpty);
    //    }
    //    else if (obj_IndustrialRenewApplication != null)
    //    {
    //        if (objA_MobSelfInspection.AppliedFor.ToLower() == "renewal")
    //        {
    //            #region IND Renew


    //            NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter obj_IndustrialRenewIssusedLetter = new NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter(obj_selfInspection.ApplicationCode); //obj_IndustrialRenewApplication.GetIssuedLetter();

    //            obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_IndustrialRenewApplication.FirstApplicationCode);
    //            obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(obj_IndustrialRenewApplication.IndustrialRenewApplicationCode);
    //            obj_MobSelfInspectionEmpty = new MobSelfInspection();
    //            if (obj_selfInspection.ApplicationCode != 0 && obj_selfInspection.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
    //            {
    //                obj_MobSelfInspection = new MobSelfInspection();
    //                obj_MobSelfInspection.ApplicationCode = obj_selfInspection.ApplicationCode;

    //                obj_MobSelfInspection.NOCNo = objA_MobSelfInspection.NOCNo;

    //                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_IndustrialRenewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);

    //                obj_MobSelfInspection.AppliedFor = HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(obj_IndustrialRenewApplication.LinkDepth));
    //                obj_MobSelfInspection.ProjectName = obj_IndustrialNewApplication.NameOfIndustry;
    //                obj_MobSelfInspection.AppNo = obj_IndustrialNewApplication.IndustrialNewApplicationNumber;
    //                obj_MobSelfInspection.AddressLine1 = HttpUtility.HtmlEncode(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1);
    //                obj_MobSelfInspection.AddressLine2 = HttpUtility.HtmlEncode(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2);
    //                obj_MobSelfInspection.AddressLine3 = HttpUtility.HtmlEncode(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3);
    //                obj_MobSelfInspection.StateName = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.GetState().StateName));
    //                obj_MobSelfInspection.DistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.District(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName));
    //                obj_MobSelfInspection.SubDistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.SubDistrict(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName));
    //                obj_Town = new NOCAP.BLL.Master.Town(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
    //                obj_Village = new NOCAP.BLL.Master.Village(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);
    //                if (obj_Town.TownName != "")
    //                    obj_MobSelfInspection.Town = HttpUtility.HtmlEncode(obj_Town.TownName);
    //                if (obj_Village.VillageName != "")
    //                    obj_MobSelfInspection.Village = HttpUtility.HtmlEncode(obj_Village.VillageName);
    //                obj_MobSelfInspection.NOCValidatation = "(" + Convert.ToDateTime(obj_IndustrialRenewIssusedLetter.ValidityStartDate).ToShortDateString() + " - " + Convert.ToDateTime(obj_IndustrialRenewIssusedLetter.ValidityEndDate).ToShortDateString() + ")";
    //                obj_MobSelfInspection.QtyPerDay = Convert.ToDecimal(obj_IndustrialRenewIssusedLetter.GroundWaterAbsRecommToApprPerDay);
    //                obj_MobSelfInspection.QtyPerYear = Convert.ToDecimal(obj_IndustrialRenewIssusedLetter.GroundWaterAbsRecommToApprPerYear);
    //                obj_MobSelfInspection.CatOfBlock = obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc();
    //                obj_MobSelfInspection.ComplianceSubmitDate = obj_selfInspection.ComplianceSubmitDate;
    //                obj_MobSelfInspection.PresentGroundWaterReq = obj_selfInspection.PresentGroundWaterReq;
    //                obj_MobSelfInspection.PresentGroundWaterReqInDay = obj_selfInspection.PresentGroundWaterReqInDay;
    //                obj_MobSelfInspection.PresentGroundWaterReqInYear = obj_selfInspection.PresentGroundWaterReqInYear;
    //                obj_MobSelfInspection.WaterMetFitted = obj_selfInspection.WaterMetFitted;
    //                obj_MobSelfInspection.WaterMetFittedDesc = obj_selfInspection.WaterMetFittedDesc;
    //                obj_MobSelfInspection.GWQualityPreMonsoon = obj_selfInspection.GWQualityPreMonsoon;
    //                obj_MobSelfInspection.GWQualityPreMonsoonDesc = obj_selfInspection.GWQualityPreMonsoonDesc;
    //                obj_MobSelfInspection.RWHArtificialRecharge = obj_selfInspection.RWHArtificialRecharge;
    //                obj_MobSelfInspection.RWHArtificialRechargeNo = obj_selfInspection.RWHArtificialRechargeNo;
    //                obj_MobSelfInspection.RWHArtificialRechargeCapacity = obj_selfInspection.RWHArtificialRechargeCapacity;
    //                obj_MobSelfInspection.RWHArtificialRechargeDesc = obj_selfInspection.RWHArtificialRechargeDesc;
    //                obj_MobSelfInspection.PhotoGraph = obj_selfInspection.PhotoGraph;
    //                obj_MobSelfInspection.PiezoAWLRTelemetry = obj_selfInspection.PiezoAWLRTelemetry;
    //                obj_MobSelfInspection.PiezoAWLRTelemetryDesc = obj_selfInspection.PiezoAWLRTelemetryDesc;
    //                obj_MobSelfInspection.RecycleReuse = obj_selfInspection.RecycleReuse;
    //                obj_MobSelfInspection.RecycleReuseInDay = obj_selfInspection.RecycleReuseInDay;
    //                obj_MobSelfInspection.RecycleReuseInYear = obj_selfInspection.RecycleReuseInYear;
    //                obj_MobSelfInspection.RecycleReuseDesc = obj_selfInspection.RecycleReuseDesc;
    //                obj_MobSelfInspection.ActionTakenReportWithinOneYear = obj_selfInspection.ActionTakenReportWithinOneYear;
    //                obj_MobSelfInspection.ActionTakenReportWithinOneYearDesc = obj_selfInspection.ActionTakenReportWithinOneYearDesc;
    //                obj_MobSelfInspection.Remarks = obj_selfInspection.Remarks;
    //                obj_MobSelfInspection.CreatedOnByUC = obj_selfInspection.CreatedOnByUC;
    //                obj_MobSelfInspection.CreatedByUC = obj_selfInspection.CreatedByUC;
    //                obj_MobSelfInspection.CreatedOnByExUC = obj_selfInspection.CreatedOnByExUC;
    //                obj_MobSelfInspection.CreatedByExUC = obj_selfInspection.CreatedByExUC;
    //                obj_MobSelfInspection.SubmittedOnByUC = obj_selfInspection.SubmittedOnByUC;
    //                obj_MobSelfInspection.SubmittedByUC = obj_selfInspection.SubmittedByUC;
    //                obj_MobSelfInspection.SubmittedOnByExUC = obj_selfInspection.SubmittedOnByExUC;
    //                obj_MobSelfInspection.SubmittedByExUC = obj_selfInspection.SubmittedByExUC;
    //                obj_MobSelfInspection.ModifiedOnByUC = obj_selfInspection.ModifiedOnByUC;
    //                obj_MobSelfInspection.ModifiedByUC = obj_selfInspection.ModifiedByUC;
    //                obj_MobSelfInspection.ModifiedOnByExUC = obj_selfInspection.ModifiedOnByExUC;
    //                obj_MobSelfInspection.ModifiedByExUC = obj_selfInspection.ModifiedByExUC;
    //                obj_MobSelfInspection.CustumMessage = obj_selfInspection.CustumMessage;
    //                obj_MobSelfInspection.Status = 1;
    //                #region Amardeep

    //                objA_MobSelfInspection.NOCNo = obj_selfInspection.NOCNo;
    //                objA_MobSelfInspection.ValidityStartDate = obj_selfInspection.ValidityStartDate;
    //                objA_MobSelfInspection.ValidityEndDate = obj_selfInspection.ValidityEndDate;
    //                objA_MobSelfInspection.GroundWaterAbsDayAppr = obj_selfInspection.GroundWaterAbsDayAppr;
    //                objA_MobSelfInspection.GroundWaterDewDayAppr = obj_selfInspection.GroundWaterDewDayAppr;
    //                objA_MobSelfInspection.GroundWaterAbsYearAppr = obj_selfInspection.GroundWaterAbsYearAppr;
    //                objA_MobSelfInspection.GroundWaterDewYearAppr = obj_selfInspection.GroundWaterDewYearAppr;
    //                objA_MobSelfInspection.InspectionAgencyCode = obj_selfInspection.InspectionAgencyCode;
    //                objA_MobSelfInspection.AnyOtherAgency = obj_selfInspection.AnyOtherAgency;
    //                objA_MobSelfInspection.DateOfInspection = obj_selfInspection.DateOfInspection;
    //                objA_MobSelfInspection.PreGroundWaterDewReqInDay = obj_selfInspection.PreGroundWaterDewReqInDay;
    //                objA_MobSelfInspection.PreGroundWaterDewReqInYear = obj_selfInspection.PreGroundWaterDewReqInYear;
    //                objA_MobSelfInspection.PreGroundWaterAnyVariReqInDay = obj_selfInspection.PreGroundWaterAnyVariReqInDay;
    //                objA_MobSelfInspection.PreGroundWaterAnyVariReqInYear = obj_selfInspection.PreGroundWaterAnyVariReqInYear;
    //                objA_MobSelfInspection.AbstraStructExisting = obj_selfInspection.AbstraStructExisting;
    //                objA_MobSelfInspection.AbstraStructProposed = obj_selfInspection.AbstraStructProposed;
    //                objA_MobSelfInspection.NoAbsDewStrucAtPresent = obj_selfInspection.NoAbsDewStrucAtPresent;
    //                objA_MobSelfInspection.FuncAbstStruct = obj_selfInspection.FuncAbstStruct;
    //                objA_MobSelfInspection.MeterTypeCode = obj_selfInspection.MeterTypeCode;
    //                objA_MobSelfInspection.NumberOfFunMeter = obj_selfInspection.NumberOfFunMeter;
    //                objA_MobSelfInspection.TypeOfAbstStructCode = obj_selfInspection.TypeOfAbstStructCode;
    //                objA_MobSelfInspection.NumberOfStruct = obj_selfInspection.NumberOfStruct;
    //                objA_MobSelfInspection.QuantOfRecharge = obj_selfInspection.QuantOfRecharge;
    //                objA_MobSelfInspection.NoOfPiezo = obj_selfInspection.NoOfPiezo;
    //                objA_MobSelfInspection.NoOfPiezoDWLR = obj_selfInspection.NoOfPiezoDWLR;
    //                objA_MobSelfInspection.NoOfPiezoTelem = obj_selfInspection.NoOfPiezoTelem;
    //                objA_MobSelfInspection.NoOfObserwell = obj_selfInspection.NoOfObserwell;
    //                objA_MobSelfInspection.NoOfFunctPiezo = obj_selfInspection.NoOfFunctPiezo;
    //                objA_MobSelfInspection.QuanttreatWasteWaterIND = obj_selfInspection.QuanttreatWasteWaterIND;
    //                objA_MobSelfInspection.QuanttreatWasteWaterINDGreen = obj_selfInspection.QuanttreatWasteWaterINDGreen;
    //                objA_MobSelfInspection.QuanttreatWasteWaterINDOther = obj_selfInspection.QuanttreatWasteWaterINDOther;
    //                objA_MobSelfInspection.AuditAgency = obj_selfInspection.AuditAgency;
    //                objA_MobSelfInspection.DateOfInspectionAudit = obj_selfInspection.DateOfInspectionAudit;
    //                objA_MobSelfInspection.AnyViolationNOCCondiDesc = obj_selfInspection.AnyViolationNOCCondiDesc;
    //                objA_MobSelfInspection.AnyOtherCompliancesDesc = obj_selfInspection.AnyOtherCompliancesDesc;
    //                objA_MobSelfInspection.WellFittedWithWaterFlowMeterAttCode = obj_selfInspection.WellFittedWithWaterFlowMeterAttCode;
    //                objA_MobSelfInspection.GeoRainWaterHarvRechAttCode = obj_selfInspection.GeoRainWaterHarvRechAttCode;
    //                objA_MobSelfInspection.GroundWaterQualityAttCode = obj_selfInspection.GroundWaterQualityAttCode;
    //                objA_MobSelfInspection.GroundwaterAbstractionDataAttCode = obj_selfInspection.GroundwaterAbstractionDataAttCode;
    //                objA_MobSelfInspection.NOCAttCode = obj_selfInspection.NOCAttCode;
    //                objA_MobSelfInspection.CopySiteInspectionAttCode = obj_selfInspection.CopySiteInspectionAttCode;
    //                objA_MobSelfInspection.GroundwaterMonitoringAttCode = obj_selfInspection.GroundwaterMonitoringAttCode;
    //                objA_MobSelfInspection.PhotoETPSTPAttCode = obj_selfInspection.PhotoETPSTPAttCode;
    //                objA_MobSelfInspection.WaterAuditAttCode = obj_selfInspection.WaterAuditAttCode;
    //                objA_MobSelfInspection.IARModelingAttCode = obj_selfInspection.IARModelingAttCode;
    //                objA_MobSelfInspection.ExtraAttCode = obj_selfInspection.ExtraAttCode;



    //                objA_MobSelfInspection.InspectionReport = obj_selfInspection.InspectionReport;
    //                objA_MobSelfInspection.PreGroundWaterAnyVari = obj_selfInspection.PreGroundWaterAnyVari;
    //                objA_MobSelfInspection.AbstrDataSubmittedTW = obj_selfInspection.AbstrDataSubmittedTW;
    //                objA_MobSelfInspection.StructPhoto = obj_selfInspection.StructPhoto;

    //                objA_MobSelfInspection.AbstStructFittedWithWM = obj_selfInspection.AbstStructFittedWithWM;
    //                objA_MobSelfInspection.TelemInstalled = obj_selfInspection.TelemInstalled;
    //                objA_MobSelfInspection.AnnuCalibOfWM = obj_selfInspection.AnnuCalibOfWM;
    //                objA_MobSelfInspection.PhotoWellFittedWithWM = obj_selfInspection.PhotoWellFittedWithWM;

    //                objA_MobSelfInspection.GWQuality = obj_selfInspection.GWQuality;
    //                objA_MobSelfInspection.MineSeepageQuality = obj_selfInspection.MineSeepageQuality;
    //                objA_MobSelfInspection.WaterSampleAnalyzed = obj_selfInspection.WaterSampleAnalyzed;
    //                objA_MobSelfInspection.GWReportWithinTime = obj_selfInspection.GWReportWithinTime;

    //                objA_MobSelfInspection.WithOutPremises = obj_selfInspection.WithOutPremises;
    //                objA_MobSelfInspection.PhotoRechargeStruct = obj_selfInspection.PhotoRechargeStruct;
    //                objA_MobSelfInspection.GeoPiezoAWLRTelem = obj_selfInspection.GeoPiezoAWLRTelem;
    //                objA_MobSelfInspection.MoniDataSubmitted = obj_selfInspection.MoniDataSubmitted;

    //                objA_MobSelfInspection.GeoPhotoOfSTP = obj_selfInspection.GeoPhotoOfSTP;
    //                objA_MobSelfInspection.SubSCWithinTimeFrame = obj_selfInspection.SubSCWithinTimeFrame;
    //                objA_MobSelfInspection.WaterAuditInspection = obj_selfInspection.WaterAuditInspection;
    //                objA_MobSelfInspection.WaterAuditReport = obj_selfInspection.WaterAuditReport;

    //                objA_MobSelfInspection.ImpactAssementReport = obj_selfInspection.ImpactAssementReport;
    //                objA_MobSelfInspection.ImpactAssementAIRReport = obj_selfInspection.ImpactAssementAIRReport;
    //                objA_MobSelfInspection.AnyViolationNOCCondi = obj_selfInspection.AnyViolationNOCCondi;
    //                objA_MobSelfInspection.AnyOtherCompliances = obj_selfInspection.AnyOtherCompliances;

    //                obj_MobSelfInspection.ActionTakenReportWithinOneYearDesc = obj_selfInspection.ActionTakenReportWithinOneYearDesc;
    //                obj_MobSelfInspection.PreSelfsentGroundWaterReqInYear = obj_selfInspection.PreSelfsentGroundWaterReqInYear;
    //                obj_MobSelfInspection.AbstraStructExistingAsPerNOC = obj_selfInspection.AbstraStructExistingAsPerNOC;
    //                obj_MobSelfInspection.AbstraStructProposedAsPerNOC = obj_selfInspection.AbstraStructProposedAsPerNOC;
    //                obj_MobSelfInspection.NumberOfFunMeter = obj_selfInspection.NumberOfFunMeter;
    //                obj_MobSelfInspection.NoOfSelfPiezoTelem = obj_selfInspection.NoOfSelfPiezoTelem;
    //                obj_MobSelfInspection.NoOfSelfPiezo = obj_selfInspection.NoOfSelfPiezo;
    //                obj_MobSelfInspection.NoOfPiezoDWLRTelem = obj_selfInspection.NoOfPiezoDWLRTelem;
    //                obj_MobSelfInspection.NoOfSelfPiezoDWLRTelem = obj_selfInspection.NoOfSelfPiezoDWLRTelem;
    //                obj_MobSelfInspection.GroundWaterMonitoringDataAttCode = obj_selfInspection.GroundWaterMonitoringDataAttCode;
    //                obj_MobSelfInspection.MiningSeepageAttCode = obj_selfInspection.MiningSeepageAttCode;
    //                obj_MobSelfInspection.AnnualCalibrationAttCode = obj_selfInspection.AnnualCalibrationAttCode;
    //                obj_MobSelfInspection.VerifiedBy = obj_selfInspection.VerifiedBy;
    //                obj_MobSelfInspection.VerifiedOn = obj_selfInspection.VerifiedOn;


    //                #endregion
    //                return Request.CreateResponse<MobSelfInspection>(System.Net.HttpStatusCode.OK, obj_MobSelfInspection);
    //            }
    //            #endregion
    //            else
    //                return Request.CreateResponse<MobSelfInspection>(System.Net.HttpStatusCode.NotFound, obj_MobSelfInspectionEmpty);
    //        }
    //        else
    //            return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfInspectionEmpty);
    //    }
    //    else if (obj_InfrastructureNewApplication != null)
    //    {
    //        if (objA_MobSelfInspection.AppliedFor.ToLower() == "fresh")
    //        {
    //            #region INF New

    //            obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(obj_InfrastructureNewApplication.InfrastructureNewApplicationCode);
    //            if (obj_selfInspection.ApplicationCode != 0 && obj_selfInspection.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
    //            {
    //                obj_MobSelfInspection = new MobSelfInspection();
    //                obj_MobSelfInspection.ApplicationCode = obj_selfInspection.ApplicationCode;

    //                obj_MobSelfInspection.NOCNo = objA_MobSelfInspection.NOCNo;

    //                NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_InfrastructureNewIssusedLetter = new NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter(obj_selfInspection.ApplicationCode); //obj_InfrastructureNewApplication.GetIssuedLetter();
    //                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);

    //                obj_MobSelfInspection.AppliedFor = "Fresh";
    //                obj_MobSelfInspection.ProjectName = obj_InfrastructureNewApplication.NameOfInfrastructure;
    //                obj_MobSelfInspection.AppNo = obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber;
    //                obj_MobSelfInspection.AddressLine1 = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1);
    //                obj_MobSelfInspection.AddressLine2 = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2);
    //                obj_MobSelfInspection.AddressLine3 = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3);

    //                obj_MobSelfInspection.StateName = HttpUtility.HtmlEncode(Convert.ToString(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.GetState().StateName));
    //                obj_MobSelfInspection.DistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.District(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName));
    //                obj_MobSelfInspection.SubDistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName));
    //                obj_Town = new NOCAP.BLL.Master.Town(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
    //                obj_Village = new NOCAP.BLL.Master.Village(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);
    //                if (obj_Town.TownName != "")
    //                    obj_MobSelfInspection.Town = HttpUtility.HtmlEncode(obj_Town.TownName);
    //                if (obj_Village.VillageName != "")
    //                    obj_MobSelfInspection.Village = HttpUtility.HtmlEncode(obj_Village.VillageName);
    //                obj_MobSelfInspection.NOCValidatation = "(" + Convert.ToDateTime(obj_InfrastructureNewIssusedLetter.ValidityStartDate).ToShortDateString() + " - " + Convert.ToDateTime(obj_InfrastructureNewIssusedLetter.ValidityEndDate).ToShortDateString() + ")";
    //                obj_MobSelfInspection.QtyPerDay = Convert.ToDecimal(obj_InfrastructureNewIssusedLetter.GroundWaterAbsRecommToApprPerDay);
    //                obj_MobSelfInspection.QtyPerYear = Convert.ToDecimal(obj_InfrastructureNewIssusedLetter.GroundWaterAbsRecommToApprPerYear);
    //                obj_MobSelfInspection.CatOfBlock = obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc();
    //                obj_MobSelfInspection.ComplianceSubmitDate = obj_selfInspection.ComplianceSubmitDate;
    //                obj_MobSelfInspection.PresentGroundWaterReq = obj_selfInspection.PresentGroundWaterReq;
    //                obj_MobSelfInspection.PresentGroundWaterReqInDay = obj_selfInspection.PresentGroundWaterReqInDay;
    //                obj_MobSelfInspection.PresentGroundWaterReqInYear = obj_selfInspection.PresentGroundWaterReqInYear;
    //                obj_MobSelfInspection.WaterMetFitted = obj_selfInspection.WaterMetFitted;
    //                obj_MobSelfInspection.WaterMetFittedDesc = obj_selfInspection.WaterMetFittedDesc;
    //                obj_MobSelfInspection.GWQualityPreMonsoon = obj_selfInspection.GWQualityPreMonsoon;
    //                obj_MobSelfInspection.GWQualityPreMonsoonDesc = obj_selfInspection.GWQualityPreMonsoonDesc;
    //                obj_MobSelfInspection.RWHArtificialRecharge = obj_selfInspection.RWHArtificialRecharge;
    //                obj_MobSelfInspection.RWHArtificialRechargeNo = obj_selfInspection.RWHArtificialRechargeNo;
    //                obj_MobSelfInspection.RWHArtificialRechargeCapacity = obj_selfInspection.RWHArtificialRechargeCapacity;
    //                obj_MobSelfInspection.RWHArtificialRechargeDesc = obj_selfInspection.RWHArtificialRechargeDesc;
    //                obj_MobSelfInspection.PhotoGraph = obj_selfInspection.PhotoGraph;
    //                obj_MobSelfInspection.PiezoAWLRTelemetry = obj_selfInspection.PiezoAWLRTelemetry;
    //                obj_MobSelfInspection.PiezoAWLRTelemetryDesc = obj_selfInspection.PiezoAWLRTelemetryDesc;
    //                obj_MobSelfInspection.RecycleReuse = obj_selfInspection.RecycleReuse;
    //                obj_MobSelfInspection.RecycleReuseInDay = obj_selfInspection.RecycleReuseInDay;
    //                obj_MobSelfInspection.RecycleReuseInYear = obj_selfInspection.RecycleReuseInYear;
    //                obj_MobSelfInspection.RecycleReuseDesc = obj_selfInspection.RecycleReuseDesc;
    //                obj_MobSelfInspection.ActionTakenReportWithinOneYear = obj_selfInspection.ActionTakenReportWithinOneYear;
    //                obj_MobSelfInspection.ActionTakenReportWithinOneYearDesc = obj_selfInspection.ActionTakenReportWithinOneYearDesc;
    //                obj_MobSelfInspection.Remarks = obj_selfInspection.Remarks;
    //                obj_MobSelfInspection.CreatedOnByUC = obj_selfInspection.CreatedOnByUC;
    //                obj_MobSelfInspection.CreatedByUC = obj_selfInspection.CreatedByUC;
    //                obj_MobSelfInspection.CreatedOnByExUC = obj_selfInspection.CreatedOnByExUC;
    //                obj_MobSelfInspection.CreatedByExUC = obj_selfInspection.CreatedByExUC;
    //                obj_MobSelfInspection.SubmittedOnByUC = obj_selfInspection.SubmittedOnByUC;
    //                obj_MobSelfInspection.SubmittedByUC = obj_selfInspection.SubmittedByUC;
    //                obj_MobSelfInspection.SubmittedOnByExUC = obj_selfInspection.SubmittedOnByExUC;
    //                obj_MobSelfInspection.SubmittedByExUC = obj_selfInspection.SubmittedByExUC;
    //                obj_MobSelfInspection.ModifiedOnByUC = obj_selfInspection.ModifiedOnByUC;
    //                obj_MobSelfInspection.ModifiedByUC = obj_selfInspection.ModifiedByUC;
    //                obj_MobSelfInspection.ModifiedOnByExUC = obj_selfInspection.ModifiedOnByExUC;
    //                obj_MobSelfInspection.ModifiedByExUC = obj_selfInspection.ModifiedByExUC;
    //                obj_MobSelfInspection.CustumMessage = obj_selfInspection.CustumMessage;
    //                obj_MobSelfInspection.Status = 1;
    //                #region Amardeep

    //                objA_MobSelfInspection.NOCNo = obj_selfInspection.NOCNo;
    //                objA_MobSelfInspection.ValidityStartDate = obj_selfInspection.ValidityStartDate;
    //                objA_MobSelfInspection.ValidityEndDate = obj_selfInspection.ValidityEndDate;
    //                objA_MobSelfInspection.GroundWaterAbsDayAppr = obj_selfInspection.GroundWaterAbsDayAppr;
    //                objA_MobSelfInspection.GroundWaterDewDayAppr = obj_selfInspection.GroundWaterDewDayAppr;
    //                objA_MobSelfInspection.GroundWaterAbsYearAppr = obj_selfInspection.GroundWaterAbsYearAppr;
    //                objA_MobSelfInspection.GroundWaterDewYearAppr = obj_selfInspection.GroundWaterDewYearAppr;
    //                objA_MobSelfInspection.InspectionAgencyCode = obj_selfInspection.InspectionAgencyCode;
    //                objA_MobSelfInspection.AnyOtherAgency = obj_selfInspection.AnyOtherAgency;
    //                objA_MobSelfInspection.DateOfInspection = obj_selfInspection.DateOfInspection;
    //                objA_MobSelfInspection.PreGroundWaterDewReqInDay = obj_selfInspection.PreGroundWaterDewReqInDay;
    //                objA_MobSelfInspection.PreGroundWaterDewReqInYear = obj_selfInspection.PreGroundWaterDewReqInYear;
    //                objA_MobSelfInspection.PreGroundWaterAnyVariReqInDay = obj_selfInspection.PreGroundWaterAnyVariReqInDay;
    //                objA_MobSelfInspection.PreGroundWaterAnyVariReqInYear = obj_selfInspection.PreGroundWaterAnyVariReqInYear;
    //                objA_MobSelfInspection.AbstraStructExisting = obj_selfInspection.AbstraStructExisting;
    //                objA_MobSelfInspection.AbstraStructProposed = obj_selfInspection.AbstraStructProposed;
    //                objA_MobSelfInspection.NoAbsDewStrucAtPresent = obj_selfInspection.NoAbsDewStrucAtPresent;
    //                objA_MobSelfInspection.FuncAbstStruct = obj_selfInspection.FuncAbstStruct;
    //                objA_MobSelfInspection.MeterTypeCode = obj_selfInspection.MeterTypeCode;
    //                objA_MobSelfInspection.NumberOfFunMeter = obj_selfInspection.NumberOfFunMeter;
    //                objA_MobSelfInspection.TypeOfAbstStructCode = obj_selfInspection.TypeOfAbstStructCode;
    //                objA_MobSelfInspection.NumberOfStruct = obj_selfInspection.NumberOfStruct;
    //                objA_MobSelfInspection.QuantOfRecharge = obj_selfInspection.QuantOfRecharge;
    //                objA_MobSelfInspection.NoOfPiezo = obj_selfInspection.NoOfPiezo;
    //                objA_MobSelfInspection.NoOfPiezoDWLR = obj_selfInspection.NoOfPiezoDWLR;
    //                objA_MobSelfInspection.NoOfPiezoTelem = obj_selfInspection.NoOfPiezoTelem;
    //                objA_MobSelfInspection.NoOfObserwell = obj_selfInspection.NoOfObserwell;
    //                objA_MobSelfInspection.NoOfFunctPiezo = obj_selfInspection.NoOfFunctPiezo;
    //                objA_MobSelfInspection.QuanttreatWasteWaterIND = obj_selfInspection.QuanttreatWasteWaterIND;
    //                objA_MobSelfInspection.QuanttreatWasteWaterINDGreen = obj_selfInspection.QuanttreatWasteWaterINDGreen;
    //                objA_MobSelfInspection.QuanttreatWasteWaterINDOther = obj_selfInspection.QuanttreatWasteWaterINDOther;
    //                objA_MobSelfInspection.AuditAgency = obj_selfInspection.AuditAgency;
    //                objA_MobSelfInspection.DateOfInspectionAudit = obj_selfInspection.DateOfInspectionAudit;
    //                objA_MobSelfInspection.AnyViolationNOCCondiDesc = obj_selfInspection.AnyViolationNOCCondiDesc;
    //                objA_MobSelfInspection.AnyOtherCompliancesDesc = obj_selfInspection.AnyOtherCompliancesDesc;
    //                objA_MobSelfInspection.WellFittedWithWaterFlowMeterAttCode = obj_selfInspection.WellFittedWithWaterFlowMeterAttCode;
    //                objA_MobSelfInspection.GeoRainWaterHarvRechAttCode = obj_selfInspection.GeoRainWaterHarvRechAttCode;
    //                objA_MobSelfInspection.GroundWaterQualityAttCode = obj_selfInspection.GroundWaterQualityAttCode;
    //                objA_MobSelfInspection.GroundwaterAbstractionDataAttCode = obj_selfInspection.GroundwaterAbstractionDataAttCode;
    //                objA_MobSelfInspection.NOCAttCode = obj_selfInspection.NOCAttCode;
    //                objA_MobSelfInspection.CopySiteInspectionAttCode = obj_selfInspection.CopySiteInspectionAttCode;
    //                objA_MobSelfInspection.GroundwaterMonitoringAttCode = obj_selfInspection.GroundwaterMonitoringAttCode;
    //                objA_MobSelfInspection.PhotoETPSTPAttCode = obj_selfInspection.PhotoETPSTPAttCode;
    //                objA_MobSelfInspection.WaterAuditAttCode = obj_selfInspection.WaterAuditAttCode;
    //                objA_MobSelfInspection.IARModelingAttCode = obj_selfInspection.IARModelingAttCode;
    //                objA_MobSelfInspection.ExtraAttCode = obj_selfInspection.ExtraAttCode;



    //                objA_MobSelfInspection.InspectionReport = obj_selfInspection.InspectionReport;
    //                objA_MobSelfInspection.PreGroundWaterAnyVari = obj_selfInspection.PreGroundWaterAnyVari;
    //                objA_MobSelfInspection.AbstrDataSubmittedTW = obj_selfInspection.AbstrDataSubmittedTW;
    //                objA_MobSelfInspection.StructPhoto = obj_selfInspection.StructPhoto;

    //                objA_MobSelfInspection.AbstStructFittedWithWM = obj_selfInspection.AbstStructFittedWithWM;
    //                objA_MobSelfInspection.TelemInstalled = obj_selfInspection.TelemInstalled;
    //                objA_MobSelfInspection.AnnuCalibOfWM = obj_selfInspection.AnnuCalibOfWM;
    //                objA_MobSelfInspection.PhotoWellFittedWithWM = obj_selfInspection.PhotoWellFittedWithWM;

    //                objA_MobSelfInspection.GWQuality = obj_selfInspection.GWQuality;
    //                objA_MobSelfInspection.MineSeepageQuality = obj_selfInspection.MineSeepageQuality;
    //                objA_MobSelfInspection.WaterSampleAnalyzed = obj_selfInspection.WaterSampleAnalyzed;
    //                objA_MobSelfInspection.GWReportWithinTime = obj_selfInspection.GWReportWithinTime;

    //                objA_MobSelfInspection.WithOutPremises = obj_selfInspection.WithOutPremises;
    //                objA_MobSelfInspection.PhotoRechargeStruct = obj_selfInspection.PhotoRechargeStruct;
    //                objA_MobSelfInspection.GeoPiezoAWLRTelem = obj_selfInspection.GeoPiezoAWLRTelem;
    //                objA_MobSelfInspection.MoniDataSubmitted = obj_selfInspection.MoniDataSubmitted;

    //                objA_MobSelfInspection.GeoPhotoOfSTP = obj_selfInspection.GeoPhotoOfSTP;
    //                objA_MobSelfInspection.SubSCWithinTimeFrame = obj_selfInspection.SubSCWithinTimeFrame;
    //                objA_MobSelfInspection.WaterAuditInspection = obj_selfInspection.WaterAuditInspection;
    //                objA_MobSelfInspection.WaterAuditReport = obj_selfInspection.WaterAuditReport;

    //                objA_MobSelfInspection.ImpactAssementReport = obj_selfInspection.ImpactAssementReport;
    //                objA_MobSelfInspection.ImpactAssementAIRReport = obj_selfInspection.ImpactAssementAIRReport;
    //                objA_MobSelfInspection.AnyViolationNOCCondi = obj_selfInspection.AnyViolationNOCCondi;
    //                objA_MobSelfInspection.AnyOtherCompliances = obj_selfInspection.AnyOtherCompliances;



    //                obj_MobSelfInspection.ActionTakenReportWithinOneYearDesc = obj_selfInspection.ActionTakenReportWithinOneYearDesc;
    //                obj_MobSelfInspection.PreSelfsentGroundWaterReqInYear = obj_selfInspection.PreSelfsentGroundWaterReqInYear;
    //                obj_MobSelfInspection.AbstraStructExistingAsPerNOC = obj_selfInspection.AbstraStructExistingAsPerNOC;
    //                obj_MobSelfInspection.AbstraStructProposedAsPerNOC = obj_selfInspection.AbstraStructProposedAsPerNOC;
    //                obj_MobSelfInspection.NumberOfFunMeter = obj_selfInspection.NumberOfFunMeter;
    //                obj_MobSelfInspection.NoOfSelfPiezoTelem = obj_selfInspection.NoOfSelfPiezoTelem;
    //                obj_MobSelfInspection.NoOfSelfPiezo = obj_selfInspection.NoOfSelfPiezo;
    //                obj_MobSelfInspection.NoOfPiezoDWLRTelem = obj_selfInspection.NoOfPiezoDWLRTelem;
    //                obj_MobSelfInspection.NoOfSelfPiezoDWLRTelem = obj_selfInspection.NoOfSelfPiezoDWLRTelem;
    //                obj_MobSelfInspection.GroundWaterMonitoringDataAttCode = obj_selfInspection.GroundWaterMonitoringDataAttCode;
    //                obj_MobSelfInspection.MiningSeepageAttCode = obj_selfInspection.MiningSeepageAttCode;
    //                obj_MobSelfInspection.AnnualCalibrationAttCode = obj_selfInspection.AnnualCalibrationAttCode;
    //                obj_MobSelfInspection.VerifiedBy = obj_selfInspection.VerifiedBy;
    //                obj_MobSelfInspection.VerifiedOn = obj_selfInspection.VerifiedOn;
    //                #endregion
    //                return Request.CreateResponse<MobSelfInspection>(System.Net.HttpStatusCode.OK, obj_MobSelfInspection);
    //            }
    //            else
    //                return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfInspectionEmpty);

    //            #endregion
    //        }
    //        else
    //            return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfInspectionEmpty);

    //    }
    //    else if (obj_InfrastructureRenewApplication != null)
    //    {
    //        if (objA_MobSelfInspection.AppliedFor.ToLower() == "renewal")
    //        {
    //            #region INF Renew

    //            obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode);
    //            if (obj_selfInspection.ApplicationCode != 0 && obj_selfInspection.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
    //            {
    //                obj_MobSelfInspection = new MobSelfInspection();
    //                obj_MobSelfInspection.ApplicationCode = obj_selfInspection.ApplicationCode;

    //                obj_MobSelfInspection.NOCNo = objA_MobSelfInspection.NOCNo;

    //                NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter obj_InfrastructureRenewIssusedLetter = new NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter(obj_selfInspection.ApplicationCode);// obj_InfrastructureRenewApplication.GetIssuedLetter();

    //                obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_InfrastructureRenewApplication.FirstApplicationCode);
    //                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureRenewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);

    //                obj_MobSelfInspection.AppliedFor = HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(obj_InfrastructureRenewApplication.LinkDepth));
    //                obj_MobSelfInspection.ProjectName = obj_InfrastructureNewApplication.NameOfInfrastructure;
    //                obj_MobSelfInspection.AppNo = obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber;
    //                obj_MobSelfInspection.AddressLine1 = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1);
    //                obj_MobSelfInspection.AddressLine2 = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2);
    //                obj_MobSelfInspection.AddressLine3 = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3);
    //                obj_MobSelfInspection.StateName = HttpUtility.HtmlEncode(Convert.ToString(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.GetState().StateName));
    //                obj_MobSelfInspection.DistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.District(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName));
    //                obj_MobSelfInspection.SubDistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName));
    //                obj_Town = new NOCAP.BLL.Master.Town(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
    //                obj_Village = new NOCAP.BLL.Master.Village(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);
    //                if (obj_Town.TownName != "")
    //                    obj_MobSelfInspection.Town = HttpUtility.HtmlEncode(obj_Town.TownName);
    //                if (obj_Village.VillageName != "")
    //                    obj_MobSelfInspection.Village = HttpUtility.HtmlEncode(obj_Village.VillageName);
    //                obj_MobSelfInspection.NOCValidatation = "(" + Convert.ToDateTime(obj_InfrastructureRenewIssusedLetter.ValidityStartDate).ToShortDateString() + " - " + Convert.ToDateTime(obj_InfrastructureRenewIssusedLetter.ValidityEndDate).ToShortDateString() + ")";
    //                obj_MobSelfInspection.QtyPerDay = Convert.ToDecimal(obj_InfrastructureRenewIssusedLetter.GroundWaterAbsRecommToApprPerDay);
    //                obj_MobSelfInspection.QtyPerYear = Convert.ToDecimal(obj_InfrastructureRenewIssusedLetter.GroundWaterAbsRecommToApprPerYear);
    //                obj_MobSelfInspection.CatOfBlock = obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc();
    //                obj_MobSelfInspection.ComplianceSubmitDate = obj_selfInspection.ComplianceSubmitDate;
    //                obj_MobSelfInspection.PresentGroundWaterReq = obj_selfInspection.PresentGroundWaterReq;
    //                obj_MobSelfInspection.PresentGroundWaterReqInDay = obj_selfInspection.PresentGroundWaterReqInDay;
    //                obj_MobSelfInspection.PresentGroundWaterReqInYear = obj_selfInspection.PresentGroundWaterReqInYear;
    //                obj_MobSelfInspection.WaterMetFitted = obj_selfInspection.WaterMetFitted;
    //                obj_MobSelfInspection.WaterMetFittedDesc = obj_selfInspection.WaterMetFittedDesc;
    //                obj_MobSelfInspection.GWQualityPreMonsoon = obj_selfInspection.GWQualityPreMonsoon;
    //                obj_MobSelfInspection.GWQualityPreMonsoonDesc = obj_selfInspection.GWQualityPreMonsoonDesc;
    //                obj_MobSelfInspection.RWHArtificialRecharge = obj_selfInspection.RWHArtificialRecharge;
    //                obj_MobSelfInspection.RWHArtificialRechargeNo = obj_selfInspection.RWHArtificialRechargeNo;
    //                obj_MobSelfInspection.RWHArtificialRechargeCapacity = obj_selfInspection.RWHArtificialRechargeCapacity;
    //                obj_MobSelfInspection.RWHArtificialRechargeDesc = obj_selfInspection.RWHArtificialRechargeDesc;
    //                obj_MobSelfInspection.PhotoGraph = obj_selfInspection.PhotoGraph;
    //                obj_MobSelfInspection.PiezoAWLRTelemetry = obj_selfInspection.PiezoAWLRTelemetry;
    //                obj_MobSelfInspection.PiezoAWLRTelemetryDesc = obj_selfInspection.PiezoAWLRTelemetryDesc;
    //                obj_MobSelfInspection.RecycleReuse = obj_selfInspection.RecycleReuse;
    //                obj_MobSelfInspection.RecycleReuseInDay = obj_selfInspection.RecycleReuseInDay;
    //                obj_MobSelfInspection.RecycleReuseInYear = obj_selfInspection.RecycleReuseInYear;
    //                obj_MobSelfInspection.RecycleReuseDesc = obj_selfInspection.RecycleReuseDesc;
    //                obj_MobSelfInspection.ActionTakenReportWithinOneYear = obj_selfInspection.ActionTakenReportWithinOneYear;
    //                obj_MobSelfInspection.ActionTakenReportWithinOneYearDesc = obj_selfInspection.ActionTakenReportWithinOneYearDesc;
    //                obj_MobSelfInspection.Remarks = obj_selfInspection.Remarks;
    //                obj_MobSelfInspection.CreatedOnByUC = obj_selfInspection.CreatedOnByUC;
    //                obj_MobSelfInspection.CreatedByUC = obj_selfInspection.CreatedByUC;
    //                obj_MobSelfInspection.CreatedOnByExUC = obj_selfInspection.CreatedOnByExUC;
    //                obj_MobSelfInspection.CreatedByExUC = obj_selfInspection.CreatedByExUC;
    //                obj_MobSelfInspection.SubmittedOnByUC = obj_selfInspection.SubmittedOnByUC;
    //                obj_MobSelfInspection.SubmittedByUC = obj_selfInspection.SubmittedByUC;
    //                obj_MobSelfInspection.SubmittedOnByExUC = obj_selfInspection.SubmittedOnByExUC;
    //                obj_MobSelfInspection.SubmittedByExUC = obj_selfInspection.SubmittedByExUC;
    //                obj_MobSelfInspection.ModifiedOnByUC = obj_selfInspection.ModifiedOnByUC;
    //                obj_MobSelfInspection.ModifiedByUC = obj_selfInspection.ModifiedByUC;
    //                obj_MobSelfInspection.ModifiedOnByExUC = obj_selfInspection.ModifiedOnByExUC;
    //                obj_MobSelfInspection.ModifiedByExUC = obj_selfInspection.ModifiedByExUC;
    //                obj_MobSelfInspection.CustumMessage = obj_selfInspection.CustumMessage;
    //                obj_MobSelfInspection.Status = 1;
    //                #region Amardeep

    //                objA_MobSelfInspection.NOCNo = obj_selfInspection.NOCNo;
    //                objA_MobSelfInspection.ValidityStartDate = obj_selfInspection.ValidityStartDate;
    //                objA_MobSelfInspection.ValidityEndDate = obj_selfInspection.ValidityEndDate;
    //                objA_MobSelfInspection.GroundWaterAbsDayAppr = obj_selfInspection.GroundWaterAbsDayAppr;
    //                objA_MobSelfInspection.GroundWaterDewDayAppr = obj_selfInspection.GroundWaterDewDayAppr;
    //                objA_MobSelfInspection.GroundWaterAbsYearAppr = obj_selfInspection.GroundWaterAbsYearAppr;
    //                objA_MobSelfInspection.GroundWaterDewYearAppr = obj_selfInspection.GroundWaterDewYearAppr;
    //                objA_MobSelfInspection.InspectionAgencyCode = obj_selfInspection.InspectionAgencyCode;
    //                objA_MobSelfInspection.AnyOtherAgency = obj_selfInspection.AnyOtherAgency;
    //                objA_MobSelfInspection.DateOfInspection = obj_selfInspection.DateOfInspection;
    //                objA_MobSelfInspection.PreGroundWaterDewReqInDay = obj_selfInspection.PreGroundWaterDewReqInDay;
    //                objA_MobSelfInspection.PreGroundWaterDewReqInYear = obj_selfInspection.PreGroundWaterDewReqInYear;
    //                objA_MobSelfInspection.PreGroundWaterAnyVariReqInDay = obj_selfInspection.PreGroundWaterAnyVariReqInDay;
    //                objA_MobSelfInspection.PreGroundWaterAnyVariReqInYear = obj_selfInspection.PreGroundWaterAnyVariReqInYear;
    //                objA_MobSelfInspection.AbstraStructExisting = obj_selfInspection.AbstraStructExisting;
    //                objA_MobSelfInspection.AbstraStructProposed = obj_selfInspection.AbstraStructProposed;
    //                objA_MobSelfInspection.NoAbsDewStrucAtPresent = obj_selfInspection.NoAbsDewStrucAtPresent;
    //                objA_MobSelfInspection.FuncAbstStruct = obj_selfInspection.FuncAbstStruct;
    //                objA_MobSelfInspection.MeterTypeCode = obj_selfInspection.MeterTypeCode;
    //                objA_MobSelfInspection.NumberOfFunMeter = obj_selfInspection.NumberOfFunMeter;
    //                objA_MobSelfInspection.TypeOfAbstStructCode = obj_selfInspection.TypeOfAbstStructCode;
    //                objA_MobSelfInspection.NumberOfStruct = obj_selfInspection.NumberOfStruct;
    //                objA_MobSelfInspection.QuantOfRecharge = obj_selfInspection.QuantOfRecharge;
    //                objA_MobSelfInspection.NoOfPiezo = obj_selfInspection.NoOfPiezo;
    //                objA_MobSelfInspection.NoOfPiezoDWLR = obj_selfInspection.NoOfPiezoDWLR;
    //                objA_MobSelfInspection.NoOfPiezoTelem = obj_selfInspection.NoOfPiezoTelem;
    //                objA_MobSelfInspection.NoOfObserwell = obj_selfInspection.NoOfObserwell;
    //                objA_MobSelfInspection.NoOfFunctPiezo = obj_selfInspection.NoOfFunctPiezo;
    //                objA_MobSelfInspection.QuanttreatWasteWaterIND = obj_selfInspection.QuanttreatWasteWaterIND;
    //                objA_MobSelfInspection.QuanttreatWasteWaterINDGreen = obj_selfInspection.QuanttreatWasteWaterINDGreen;
    //                objA_MobSelfInspection.QuanttreatWasteWaterINDOther = obj_selfInspection.QuanttreatWasteWaterINDOther;
    //                objA_MobSelfInspection.AuditAgency = obj_selfInspection.AuditAgency;
    //                objA_MobSelfInspection.DateOfInspectionAudit = obj_selfInspection.DateOfInspectionAudit;
    //                objA_MobSelfInspection.AnyViolationNOCCondiDesc = obj_selfInspection.AnyViolationNOCCondiDesc;
    //                objA_MobSelfInspection.AnyOtherCompliancesDesc = obj_selfInspection.AnyOtherCompliancesDesc;
    //                objA_MobSelfInspection.WellFittedWithWaterFlowMeterAttCode = obj_selfInspection.WellFittedWithWaterFlowMeterAttCode;
    //                objA_MobSelfInspection.GeoRainWaterHarvRechAttCode = obj_selfInspection.GeoRainWaterHarvRechAttCode;
    //                objA_MobSelfInspection.GroundWaterQualityAttCode = obj_selfInspection.GroundWaterQualityAttCode;
    //                objA_MobSelfInspection.GroundwaterAbstractionDataAttCode = obj_selfInspection.GroundwaterAbstractionDataAttCode;
    //                objA_MobSelfInspection.NOCAttCode = obj_selfInspection.NOCAttCode;
    //                objA_MobSelfInspection.CopySiteInspectionAttCode = obj_selfInspection.CopySiteInspectionAttCode;
    //                objA_MobSelfInspection.GroundwaterMonitoringAttCode = obj_selfInspection.GroundwaterMonitoringAttCode;
    //                objA_MobSelfInspection.PhotoETPSTPAttCode = obj_selfInspection.PhotoETPSTPAttCode;
    //                objA_MobSelfInspection.WaterAuditAttCode = obj_selfInspection.WaterAuditAttCode;
    //                objA_MobSelfInspection.IARModelingAttCode = obj_selfInspection.IARModelingAttCode;
    //                objA_MobSelfInspection.ExtraAttCode = obj_selfInspection.ExtraAttCode;



    //                objA_MobSelfInspection.InspectionReport = obj_selfInspection.InspectionReport;
    //                objA_MobSelfInspection.PreGroundWaterAnyVari = obj_selfInspection.PreGroundWaterAnyVari;
    //                objA_MobSelfInspection.AbstrDataSubmittedTW = obj_selfInspection.AbstrDataSubmittedTW;
    //                objA_MobSelfInspection.StructPhoto = obj_selfInspection.StructPhoto;

    //                objA_MobSelfInspection.AbstStructFittedWithWM = obj_selfInspection.AbstStructFittedWithWM;
    //                objA_MobSelfInspection.TelemInstalled = obj_selfInspection.TelemInstalled;
    //                objA_MobSelfInspection.AnnuCalibOfWM = obj_selfInspection.AnnuCalibOfWM;
    //                objA_MobSelfInspection.PhotoWellFittedWithWM = obj_selfInspection.PhotoWellFittedWithWM;

    //                objA_MobSelfInspection.GWQuality = obj_selfInspection.GWQuality;
    //                objA_MobSelfInspection.MineSeepageQuality = obj_selfInspection.MineSeepageQuality;
    //                objA_MobSelfInspection.WaterSampleAnalyzed = obj_selfInspection.WaterSampleAnalyzed;
    //                objA_MobSelfInspection.GWReportWithinTime = obj_selfInspection.GWReportWithinTime;

    //                objA_MobSelfInspection.WithOutPremises = obj_selfInspection.WithOutPremises;
    //                objA_MobSelfInspection.PhotoRechargeStruct = obj_selfInspection.PhotoRechargeStruct;
    //                objA_MobSelfInspection.GeoPiezoAWLRTelem = obj_selfInspection.GeoPiezoAWLRTelem;
    //                objA_MobSelfInspection.MoniDataSubmitted = obj_selfInspection.MoniDataSubmitted;

    //                objA_MobSelfInspection.GeoPhotoOfSTP = obj_selfInspection.GeoPhotoOfSTP;
    //                objA_MobSelfInspection.SubSCWithinTimeFrame = obj_selfInspection.SubSCWithinTimeFrame;
    //                objA_MobSelfInspection.WaterAuditInspection = obj_selfInspection.WaterAuditInspection;
    //                objA_MobSelfInspection.WaterAuditReport = obj_selfInspection.WaterAuditReport;

    //                objA_MobSelfInspection.ImpactAssementReport = obj_selfInspection.ImpactAssementReport;
    //                objA_MobSelfInspection.ImpactAssementAIRReport = obj_selfInspection.ImpactAssementAIRReport;
    //                objA_MobSelfInspection.AnyViolationNOCCondi = obj_selfInspection.AnyViolationNOCCondi;
    //                objA_MobSelfInspection.AnyOtherCompliances = obj_selfInspection.AnyOtherCompliances;


    //                obj_MobSelfInspection.ActionTakenReportWithinOneYearDesc = obj_selfInspection.ActionTakenReportWithinOneYearDesc;
    //                obj_MobSelfInspection.PreSelfsentGroundWaterReqInYear = obj_selfInspection.PreSelfsentGroundWaterReqInYear;
    //                obj_MobSelfInspection.AbstraStructExistingAsPerNOC = obj_selfInspection.AbstraStructExistingAsPerNOC;
    //                obj_MobSelfInspection.AbstraStructProposedAsPerNOC = obj_selfInspection.AbstraStructProposedAsPerNOC;
    //                obj_MobSelfInspection.NumberOfFunMeter = obj_selfInspection.NumberOfFunMeter;
    //                obj_MobSelfInspection.NoOfSelfPiezoTelem = obj_selfInspection.NoOfSelfPiezoTelem;
    //                obj_MobSelfInspection.NoOfSelfPiezo = obj_selfInspection.NoOfSelfPiezo;
    //                obj_MobSelfInspection.NoOfPiezoDWLRTelem = obj_selfInspection.NoOfPiezoDWLRTelem;
    //                obj_MobSelfInspection.NoOfSelfPiezoDWLRTelem = obj_selfInspection.NoOfSelfPiezoDWLRTelem;
    //                obj_MobSelfInspection.GroundWaterMonitoringDataAttCode = obj_selfInspection.GroundWaterMonitoringDataAttCode;
    //                obj_MobSelfInspection.MiningSeepageAttCode = obj_selfInspection.MiningSeepageAttCode;
    //                obj_MobSelfInspection.AnnualCalibrationAttCode = obj_selfInspection.AnnualCalibrationAttCode;
    //                obj_MobSelfInspection.VerifiedBy = obj_selfInspection.VerifiedBy;
    //                obj_MobSelfInspection.VerifiedOn = obj_selfInspection.VerifiedOn;

    //                #endregion
    //                return Request.CreateResponse<MobSelfInspection>(System.Net.HttpStatusCode.OK, obj_MobSelfInspection);
    //            }
    //            else
    //                return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfInspectionEmpty);

    //            #endregion
    //        }
    //        else
    //            return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfInspectionEmpty);


    //    }
    //    else if (obj_MiningNewApplication != null)
    //    {
    //        if (objA_MobSelfInspection.AppliedFor.ToLower() == "fresh")
    //        {
    //            #region MIN New

    //            obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(obj_MiningNewApplication.ApplicationCode);
    //            if (obj_selfInspection.ApplicationCode != 0 && obj_selfInspection.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
    //            {
    //                obj_MobSelfInspection = new MobSelfInspection();
    //                obj_MobSelfInspection.ApplicationCode = obj_selfInspection.ApplicationCode;

    //                obj_MobSelfInspection.NOCNo = objA_MobSelfInspection.NOCNo;

    //                NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter obj_MiningNewIssusedLetter = new NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter(obj_selfInspection.ApplicationCode); //obj_MiningNewApplication.GetIssuedLetter();
    //                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);

    //                obj_MobSelfInspection.AppliedFor = "Fresh";
    //                obj_MobSelfInspection.ProjectName = obj_MiningNewApplication.NameOfMining;
    //                obj_MobSelfInspection.AppNo = obj_MiningNewApplication.MiningNewApplicationNumber;
    //                obj_MobSelfInspection.AddressLine1 = HttpUtility.HtmlEncode(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1);
    //                obj_MobSelfInspection.AddressLine2 = HttpUtility.HtmlEncode(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2);
    //                obj_MobSelfInspection.AddressLine3 = HttpUtility.HtmlEncode(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3);
    //                obj_MobSelfInspection.StateName = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.GetState().StateName));
    //                obj_MobSelfInspection.DistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.District(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName));
    //                obj_MobSelfInspection.SubDistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName));
    //                obj_Town = new NOCAP.BLL.Master.Town(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
    //                obj_Village = new NOCAP.BLL.Master.Village(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);
    //                if (obj_Town.TownName != "")
    //                    obj_MobSelfInspection.Town = HttpUtility.HtmlEncode(obj_Town.TownName);
    //                if (obj_Village.VillageName != "")
    //                    obj_MobSelfInspection.Village = HttpUtility.HtmlEncode(obj_Village.VillageName);
    //                obj_MobSelfInspection.NOCValidatation = "(" + Convert.ToDateTime(obj_MiningNewIssusedLetter.ValidityStartDate).ToShortDateString() + " - " + Convert.ToDateTime(obj_MiningNewIssusedLetter.ValidityEndDate).ToShortDateString() + ")";
    //                obj_MobSelfInspection.QtyPerDay = Convert.ToDecimal(obj_MiningNewIssusedLetter.GroundWaterAbsRecommToApprPerDay);
    //                obj_MobSelfInspection.QtyPerYear = Convert.ToDecimal(obj_MiningNewIssusedLetter.GroundWaterAbsRecommToApprPerYear);
    //                obj_MobSelfInspection.CatOfBlock = obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc();
    //                obj_MobSelfInspection.ComplianceSubmitDate = obj_selfInspection.ComplianceSubmitDate;
    //                obj_MobSelfInspection.PresentGroundWaterReq = obj_selfInspection.PresentGroundWaterReq;
    //                obj_MobSelfInspection.PresentGroundWaterReqInDay = obj_selfInspection.PresentGroundWaterReqInDay;
    //                obj_MobSelfInspection.PresentGroundWaterReqInYear = obj_selfInspection.PresentGroundWaterReqInYear;
    //                obj_MobSelfInspection.WaterMetFitted = obj_selfInspection.WaterMetFitted;
    //                obj_MobSelfInspection.WaterMetFittedDesc = obj_selfInspection.WaterMetFittedDesc;
    //                obj_MobSelfInspection.GWQualityPreMonsoon = obj_selfInspection.GWQualityPreMonsoon;
    //                obj_MobSelfInspection.GWQualityPreMonsoonDesc = obj_selfInspection.GWQualityPreMonsoonDesc;
    //                obj_MobSelfInspection.RWHArtificialRecharge = obj_selfInspection.RWHArtificialRecharge;
    //                obj_MobSelfInspection.RWHArtificialRechargeNo = obj_selfInspection.RWHArtificialRechargeNo;
    //                obj_MobSelfInspection.RWHArtificialRechargeCapacity = obj_selfInspection.RWHArtificialRechargeCapacity;
    //                obj_MobSelfInspection.RWHArtificialRechargeDesc = obj_selfInspection.RWHArtificialRechargeDesc;
    //                obj_MobSelfInspection.PhotoGraph = obj_selfInspection.PhotoGraph;
    //                obj_MobSelfInspection.PiezoAWLRTelemetry = obj_selfInspection.PiezoAWLRTelemetry;
    //                obj_MobSelfInspection.PiezoAWLRTelemetryDesc = obj_selfInspection.PiezoAWLRTelemetryDesc;
    //                obj_MobSelfInspection.RecycleReuse = obj_selfInspection.RecycleReuse;
    //                obj_MobSelfInspection.RecycleReuseInDay = obj_selfInspection.RecycleReuseInDay;
    //                obj_MobSelfInspection.RecycleReuseInYear = obj_selfInspection.RecycleReuseInYear;
    //                obj_MobSelfInspection.RecycleReuseDesc = obj_selfInspection.RecycleReuseDesc;
    //                obj_MobSelfInspection.ActionTakenReportWithinOneYear = obj_selfInspection.ActionTakenReportWithinOneYear;
    //                obj_MobSelfInspection.ActionTakenReportWithinOneYearDesc = obj_selfInspection.ActionTakenReportWithinOneYearDesc;
    //                obj_MobSelfInspection.Remarks = obj_selfInspection.Remarks;
    //                obj_MobSelfInspection.CreatedOnByUC = obj_selfInspection.CreatedOnByUC;
    //                obj_MobSelfInspection.CreatedByUC = obj_selfInspection.CreatedByUC;
    //                obj_MobSelfInspection.CreatedOnByExUC = obj_selfInspection.CreatedOnByExUC;
    //                obj_MobSelfInspection.CreatedByExUC = obj_selfInspection.CreatedByExUC;
    //                obj_MobSelfInspection.SubmittedOnByUC = obj_selfInspection.SubmittedOnByUC;
    //                obj_MobSelfInspection.SubmittedByUC = obj_selfInspection.SubmittedByUC;
    //                obj_MobSelfInspection.SubmittedOnByExUC = obj_selfInspection.SubmittedOnByExUC;
    //                obj_MobSelfInspection.SubmittedByExUC = obj_selfInspection.SubmittedByExUC;
    //                obj_MobSelfInspection.ModifiedOnByUC = obj_selfInspection.ModifiedOnByUC;
    //                obj_MobSelfInspection.ModifiedByUC = obj_selfInspection.ModifiedByUC;
    //                obj_MobSelfInspection.ModifiedOnByExUC = obj_selfInspection.ModifiedOnByExUC;
    //                obj_MobSelfInspection.ModifiedByExUC = obj_selfInspection.ModifiedByExUC;
    //                obj_MobSelfInspection.CustumMessage = obj_selfInspection.CustumMessage;
    //                obj_MobSelfInspection.Status = 1;
    //                #region Amardeep

    //                objA_MobSelfInspection.NOCNo = obj_selfInspection.NOCNo;
    //                objA_MobSelfInspection.ValidityStartDate = obj_selfInspection.ValidityStartDate;
    //                objA_MobSelfInspection.ValidityEndDate = obj_selfInspection.ValidityEndDate;
    //                objA_MobSelfInspection.GroundWaterAbsDayAppr = obj_selfInspection.GroundWaterAbsDayAppr;
    //                objA_MobSelfInspection.GroundWaterDewDayAppr = obj_selfInspection.GroundWaterDewDayAppr;
    //                objA_MobSelfInspection.GroundWaterAbsYearAppr = obj_selfInspection.GroundWaterAbsYearAppr;
    //                objA_MobSelfInspection.GroundWaterDewYearAppr = obj_selfInspection.GroundWaterDewYearAppr;
    //                objA_MobSelfInspection.InspectionAgencyCode = obj_selfInspection.InspectionAgencyCode;
    //                objA_MobSelfInspection.AnyOtherAgency = obj_selfInspection.AnyOtherAgency;
    //                objA_MobSelfInspection.DateOfInspection = obj_selfInspection.DateOfInspection;
    //                objA_MobSelfInspection.PreGroundWaterDewReqInDay = obj_selfInspection.PreGroundWaterDewReqInDay;
    //                objA_MobSelfInspection.PreGroundWaterDewReqInYear = obj_selfInspection.PreGroundWaterDewReqInYear;
    //                objA_MobSelfInspection.PreGroundWaterAnyVariReqInDay = obj_selfInspection.PreGroundWaterAnyVariReqInDay;
    //                objA_MobSelfInspection.PreGroundWaterAnyVariReqInYear = obj_selfInspection.PreGroundWaterAnyVariReqInYear;
    //                objA_MobSelfInspection.AbstraStructExisting = obj_selfInspection.AbstraStructExisting;
    //                objA_MobSelfInspection.AbstraStructProposed = obj_selfInspection.AbstraStructProposed;
    //                objA_MobSelfInspection.NoAbsDewStrucAtPresent = obj_selfInspection.NoAbsDewStrucAtPresent;
    //                objA_MobSelfInspection.FuncAbstStruct = obj_selfInspection.FuncAbstStruct;
    //                objA_MobSelfInspection.MeterTypeCode = obj_selfInspection.MeterTypeCode;
    //                objA_MobSelfInspection.NumberOfFunMeter = obj_selfInspection.NumberOfFunMeter;
    //                objA_MobSelfInspection.TypeOfAbstStructCode = obj_selfInspection.TypeOfAbstStructCode;
    //                objA_MobSelfInspection.NumberOfStruct = obj_selfInspection.NumberOfStruct;
    //                objA_MobSelfInspection.QuantOfRecharge = obj_selfInspection.QuantOfRecharge;
    //                objA_MobSelfInspection.NoOfPiezo = obj_selfInspection.NoOfPiezo;
    //                objA_MobSelfInspection.NoOfPiezoDWLR = obj_selfInspection.NoOfPiezoDWLR;
    //                objA_MobSelfInspection.NoOfPiezoTelem = obj_selfInspection.NoOfPiezoTelem;
    //                objA_MobSelfInspection.NoOfObserwell = obj_selfInspection.NoOfObserwell;
    //                objA_MobSelfInspection.NoOfFunctPiezo = obj_selfInspection.NoOfFunctPiezo;
    //                objA_MobSelfInspection.QuanttreatWasteWaterIND = obj_selfInspection.QuanttreatWasteWaterIND;
    //                objA_MobSelfInspection.QuanttreatWasteWaterINDGreen = obj_selfInspection.QuanttreatWasteWaterINDGreen;
    //                objA_MobSelfInspection.QuanttreatWasteWaterINDOther = obj_selfInspection.QuanttreatWasteWaterINDOther;
    //                objA_MobSelfInspection.AuditAgency = obj_selfInspection.AuditAgency;
    //                objA_MobSelfInspection.DateOfInspectionAudit = obj_selfInspection.DateOfInspectionAudit;
    //                objA_MobSelfInspection.AnyViolationNOCCondiDesc = obj_selfInspection.AnyViolationNOCCondiDesc;
    //                objA_MobSelfInspection.AnyOtherCompliancesDesc = obj_selfInspection.AnyOtherCompliancesDesc;
    //                objA_MobSelfInspection.WellFittedWithWaterFlowMeterAttCode = obj_selfInspection.WellFittedWithWaterFlowMeterAttCode;
    //                objA_MobSelfInspection.GeoRainWaterHarvRechAttCode = obj_selfInspection.GeoRainWaterHarvRechAttCode;
    //                objA_MobSelfInspection.GroundWaterQualityAttCode = obj_selfInspection.GroundWaterQualityAttCode;
    //                objA_MobSelfInspection.GroundwaterAbstractionDataAttCode = obj_selfInspection.GroundwaterAbstractionDataAttCode;
    //                objA_MobSelfInspection.NOCAttCode = obj_selfInspection.NOCAttCode;
    //                objA_MobSelfInspection.CopySiteInspectionAttCode = obj_selfInspection.CopySiteInspectionAttCode;
    //                objA_MobSelfInspection.GroundwaterMonitoringAttCode = obj_selfInspection.GroundwaterMonitoringAttCode;
    //                objA_MobSelfInspection.PhotoETPSTPAttCode = obj_selfInspection.PhotoETPSTPAttCode;
    //                objA_MobSelfInspection.WaterAuditAttCode = obj_selfInspection.WaterAuditAttCode;
    //                objA_MobSelfInspection.IARModelingAttCode = obj_selfInspection.IARModelingAttCode;
    //                objA_MobSelfInspection.ExtraAttCode = obj_selfInspection.ExtraAttCode;



    //                objA_MobSelfInspection.InspectionReport = obj_selfInspection.InspectionReport;
    //                objA_MobSelfInspection.PreGroundWaterAnyVari = obj_selfInspection.PreGroundWaterAnyVari;
    //                objA_MobSelfInspection.AbstrDataSubmittedTW = obj_selfInspection.AbstrDataSubmittedTW;
    //                objA_MobSelfInspection.StructPhoto = obj_selfInspection.StructPhoto;

    //                objA_MobSelfInspection.AbstStructFittedWithWM = obj_selfInspection.AbstStructFittedWithWM;
    //                objA_MobSelfInspection.TelemInstalled = obj_selfInspection.TelemInstalled;
    //                objA_MobSelfInspection.AnnuCalibOfWM = obj_selfInspection.AnnuCalibOfWM;
    //                objA_MobSelfInspection.PhotoWellFittedWithWM = obj_selfInspection.PhotoWellFittedWithWM;

    //                objA_MobSelfInspection.GWQuality = obj_selfInspection.GWQuality;
    //                objA_MobSelfInspection.MineSeepageQuality = obj_selfInspection.MineSeepageQuality;
    //                objA_MobSelfInspection.WaterSampleAnalyzed = obj_selfInspection.WaterSampleAnalyzed;
    //                objA_MobSelfInspection.GWReportWithinTime = obj_selfInspection.GWReportWithinTime;

    //                objA_MobSelfInspection.WithOutPremises = obj_selfInspection.WithOutPremises;
    //                objA_MobSelfInspection.PhotoRechargeStruct = obj_selfInspection.PhotoRechargeStruct;
    //                objA_MobSelfInspection.GeoPiezoAWLRTelem = obj_selfInspection.GeoPiezoAWLRTelem;
    //                objA_MobSelfInspection.MoniDataSubmitted = obj_selfInspection.MoniDataSubmitted;

    //                objA_MobSelfInspection.GeoPhotoOfSTP = obj_selfInspection.GeoPhotoOfSTP;
    //                objA_MobSelfInspection.SubSCWithinTimeFrame = obj_selfInspection.SubSCWithinTimeFrame;
    //                objA_MobSelfInspection.WaterAuditInspection = obj_selfInspection.WaterAuditInspection;
    //                objA_MobSelfInspection.WaterAuditReport = obj_selfInspection.WaterAuditReport;

    //                objA_MobSelfInspection.ImpactAssementReport = obj_selfInspection.ImpactAssementReport;
    //                objA_MobSelfInspection.ImpactAssementAIRReport = obj_selfInspection.ImpactAssementAIRReport;
    //                objA_MobSelfInspection.AnyViolationNOCCondi = obj_selfInspection.AnyViolationNOCCondi;
    //                objA_MobSelfInspection.AnyOtherCompliances = obj_selfInspection.AnyOtherCompliances;

    //                obj_MobSelfInspection.ActionTakenReportWithinOneYearDesc = obj_selfInspection.ActionTakenReportWithinOneYearDesc;
    //                obj_MobSelfInspection.PreSelfsentGroundWaterReqInYear = obj_selfInspection.PreSelfsentGroundWaterReqInYear;
    //                obj_MobSelfInspection.AbstraStructExistingAsPerNOC = obj_selfInspection.AbstraStructExistingAsPerNOC;
    //                obj_MobSelfInspection.AbstraStructProposedAsPerNOC = obj_selfInspection.AbstraStructProposedAsPerNOC;
    //                obj_MobSelfInspection.NumberOfFunMeter = obj_selfInspection.NumberOfFunMeter;
    //                obj_MobSelfInspection.NoOfSelfPiezoTelem = obj_selfInspection.NoOfSelfPiezoTelem;
    //                obj_MobSelfInspection.NoOfSelfPiezo = obj_selfInspection.NoOfSelfPiezo;
    //                obj_MobSelfInspection.NoOfPiezoDWLRTelem = obj_selfInspection.NoOfPiezoDWLRTelem;
    //                obj_MobSelfInspection.NoOfSelfPiezoDWLRTelem = obj_selfInspection.NoOfSelfPiezoDWLRTelem;
    //                obj_MobSelfInspection.GroundWaterMonitoringDataAttCode = obj_selfInspection.GroundWaterMonitoringDataAttCode;
    //                obj_MobSelfInspection.MiningSeepageAttCode = obj_selfInspection.MiningSeepageAttCode;
    //                obj_MobSelfInspection.AnnualCalibrationAttCode = obj_selfInspection.AnnualCalibrationAttCode;
    //                obj_MobSelfInspection.VerifiedBy = obj_selfInspection.VerifiedBy;
    //                obj_MobSelfInspection.VerifiedOn = obj_selfInspection.VerifiedOn;


    //                #endregion
    //                return Request.CreateResponse<MobSelfInspection>(System.Net.HttpStatusCode.OK, obj_MobSelfInspection);
    //            }
    //            else
    //                return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfInspectionEmpty);

    //            #endregion
    //        }
    //        else
    //            return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfInspectionEmpty);

    //    }
    //    else if (obj_MiningRenewApplication != null)
    //    {
    //        if (objA_MobSelfInspection.AppliedFor.ToLower() == "renewal")
    //        {
    //            #region MIN Renew

    //            obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(obj_MiningRenewApplication.MiningRenewApplicationCode);
    //            if (obj_selfInspection.ApplicationCode != 0 && obj_selfInspection.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
    //            {
    //                obj_MobSelfInspection = new MobSelfInspection();
    //                obj_MobSelfInspection.ApplicationCode = obj_selfInspection.ApplicationCode;

    //                obj_MobSelfInspection.NOCNo = objA_MobSelfInspection.NOCNo;

    //                NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter obj_MiningRenewIssusedLetter = new NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter(obj_selfInspection.ApplicationCode); //obj_MiningRenewApplication.GetIssuedLetter();

    //                obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_MiningRenewApplication.FirstApplicationCode);
    //                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningRenewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);

    //                obj_MobSelfInspection.AppliedFor = HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(obj_MiningRenewApplication.LinkDepth));
    //                obj_MobSelfInspection.ProjectName = obj_MiningNewApplication.NameOfMining;
    //                obj_MobSelfInspection.AppNo = obj_MiningNewApplication.MiningNewApplicationNumber;
    //                obj_MobSelfInspection.AddressLine1 = HttpUtility.HtmlEncode(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1);
    //                obj_MobSelfInspection.AddressLine2 = HttpUtility.HtmlEncode(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2);
    //                obj_MobSelfInspection.AddressLine3 = HttpUtility.HtmlEncode(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3);
    //                obj_MobSelfInspection.StateName = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.GetState().StateName));
    //                obj_MobSelfInspection.DistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.District(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName));
    //                obj_MobSelfInspection.SubDistrictName = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName));
    //                obj_Town = new NOCAP.BLL.Master.Town(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
    //                obj_Village = new NOCAP.BLL.Master.Village(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);
    //                if (obj_Town.TownName != "")
    //                    obj_MobSelfInspection.Town = HttpUtility.HtmlEncode(obj_Town.TownName);
    //                if (obj_Village.VillageName != "")
    //                    obj_MobSelfInspection.Village = HttpUtility.HtmlEncode(obj_Village.VillageName);
    //                obj_MobSelfInspection.NOCValidatation = "(" + Convert.ToDateTime(obj_MiningRenewIssusedLetter.ValidityStartDate).ToShortDateString() + " - " + Convert.ToDateTime(obj_MiningRenewIssusedLetter.ValidityEndDate).ToShortDateString() + ")";
    //                obj_MobSelfInspection.QtyPerDay = Convert.ToDecimal(obj_MiningRenewIssusedLetter.GroundWaterAbsRecommToApprPerDay);
    //                obj_MobSelfInspection.QtyPerYear = Convert.ToDecimal(obj_MiningRenewIssusedLetter.GroundWaterAbsRecommToApprPerYear);
    //                obj_MobSelfInspection.CatOfBlock = obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc();
    //                obj_MobSelfInspection.ComplianceSubmitDate = obj_selfInspection.ComplianceSubmitDate;
    //                obj_MobSelfInspection.PresentGroundWaterReq = obj_selfInspection.PresentGroundWaterReq;
    //                obj_MobSelfInspection.PresentGroundWaterReqInDay = obj_selfInspection.PresentGroundWaterReqInDay;
    //                obj_MobSelfInspection.PresentGroundWaterReqInYear = obj_selfInspection.PresentGroundWaterReqInYear;
    //                obj_MobSelfInspection.WaterMetFitted = obj_selfInspection.WaterMetFitted;
    //                obj_MobSelfInspection.WaterMetFittedDesc = obj_selfInspection.WaterMetFittedDesc;
    //                obj_MobSelfInspection.GWQualityPreMonsoon = obj_selfInspection.GWQualityPreMonsoon;
    //                obj_MobSelfInspection.GWQualityPreMonsoonDesc = obj_selfInspection.GWQualityPreMonsoonDesc;
    //                obj_MobSelfInspection.RWHArtificialRecharge = obj_selfInspection.RWHArtificialRecharge;
    //                obj_MobSelfInspection.RWHArtificialRechargeNo = obj_selfInspection.RWHArtificialRechargeNo;
    //                obj_MobSelfInspection.RWHArtificialRechargeCapacity = obj_selfInspection.RWHArtificialRechargeCapacity;
    //                obj_MobSelfInspection.RWHArtificialRechargeDesc = obj_selfInspection.RWHArtificialRechargeDesc;
    //                obj_MobSelfInspection.PhotoGraph = obj_selfInspection.PhotoGraph;
    //                obj_MobSelfInspection.PiezoAWLRTelemetry = obj_selfInspection.PiezoAWLRTelemetry;
    //                obj_MobSelfInspection.PiezoAWLRTelemetryDesc = obj_selfInspection.PiezoAWLRTelemetryDesc;
    //                obj_MobSelfInspection.RecycleReuse = obj_selfInspection.RecycleReuse;
    //                obj_MobSelfInspection.RecycleReuseInDay = obj_selfInspection.RecycleReuseInDay;
    //                obj_MobSelfInspection.RecycleReuseInYear = obj_selfInspection.RecycleReuseInYear;
    //                obj_MobSelfInspection.RecycleReuseDesc = obj_selfInspection.RecycleReuseDesc;
    //                obj_MobSelfInspection.ActionTakenReportWithinOneYear = obj_selfInspection.ActionTakenReportWithinOneYear;
    //                obj_MobSelfInspection.ActionTakenReportWithinOneYearDesc = obj_selfInspection.ActionTakenReportWithinOneYearDesc;
    //                obj_MobSelfInspection.Remarks = obj_selfInspection.Remarks;
    //                obj_MobSelfInspection.CreatedOnByUC = obj_selfInspection.CreatedOnByUC;
    //                obj_MobSelfInspection.CreatedByUC = obj_selfInspection.CreatedByUC;
    //                obj_MobSelfInspection.CreatedOnByExUC = obj_selfInspection.CreatedOnByExUC;
    //                obj_MobSelfInspection.CreatedByExUC = obj_selfInspection.CreatedByExUC;
    //                obj_MobSelfInspection.SubmittedOnByUC = obj_selfInspection.SubmittedOnByUC;
    //                obj_MobSelfInspection.SubmittedByUC = obj_selfInspection.SubmittedByUC;
    //                obj_MobSelfInspection.SubmittedOnByExUC = obj_selfInspection.SubmittedOnByExUC;
    //                obj_MobSelfInspection.SubmittedByExUC = obj_selfInspection.SubmittedByExUC;
    //                obj_MobSelfInspection.ModifiedOnByUC = obj_selfInspection.ModifiedOnByUC;
    //                obj_MobSelfInspection.ModifiedByUC = obj_selfInspection.ModifiedByUC;
    //                obj_MobSelfInspection.ModifiedOnByExUC = obj_selfInspection.ModifiedOnByExUC;
    //                obj_MobSelfInspection.ModifiedByExUC = obj_selfInspection.ModifiedByExUC;
    //                obj_MobSelfInspection.CustumMessage = obj_selfInspection.CustumMessage;
    //                obj_MobSelfInspection.Status = 1;
    //                #region Amardeep

    //                objA_MobSelfInspection.NOCNo = obj_selfInspection.NOCNo;
    //                objA_MobSelfInspection.ValidityStartDate = obj_selfInspection.ValidityStartDate;
    //                objA_MobSelfInspection.ValidityEndDate = obj_selfInspection.ValidityEndDate;
    //                objA_MobSelfInspection.GroundWaterAbsDayAppr = obj_selfInspection.GroundWaterAbsDayAppr;
    //                objA_MobSelfInspection.GroundWaterDewDayAppr = obj_selfInspection.GroundWaterDewDayAppr;
    //                objA_MobSelfInspection.GroundWaterAbsYearAppr = obj_selfInspection.GroundWaterAbsYearAppr;
    //                objA_MobSelfInspection.GroundWaterDewYearAppr = obj_selfInspection.GroundWaterDewYearAppr;
    //                objA_MobSelfInspection.InspectionAgencyCode = obj_selfInspection.InspectionAgencyCode;
    //                objA_MobSelfInspection.AnyOtherAgency = obj_selfInspection.AnyOtherAgency;
    //                objA_MobSelfInspection.DateOfInspection = obj_selfInspection.DateOfInspection;
    //                objA_MobSelfInspection.PreGroundWaterDewReqInDay = obj_selfInspection.PreGroundWaterDewReqInDay;
    //                objA_MobSelfInspection.PreGroundWaterDewReqInYear = obj_selfInspection.PreGroundWaterDewReqInYear;
    //                objA_MobSelfInspection.PreGroundWaterAnyVariReqInDay = obj_selfInspection.PreGroundWaterAnyVariReqInDay;
    //                objA_MobSelfInspection.PreGroundWaterAnyVariReqInYear = obj_selfInspection.PreGroundWaterAnyVariReqInYear;
    //                objA_MobSelfInspection.AbstraStructExisting = obj_selfInspection.AbstraStructExisting;
    //                objA_MobSelfInspection.AbstraStructProposed = obj_selfInspection.AbstraStructProposed;
    //                objA_MobSelfInspection.NoAbsDewStrucAtPresent = obj_selfInspection.NoAbsDewStrucAtPresent;
    //                objA_MobSelfInspection.FuncAbstStruct = obj_selfInspection.FuncAbstStruct;
    //                objA_MobSelfInspection.MeterTypeCode = obj_selfInspection.MeterTypeCode;
    //                objA_MobSelfInspection.NumberOfFunMeter = obj_selfInspection.NumberOfFunMeter;
    //                objA_MobSelfInspection.TypeOfAbstStructCode = obj_selfInspection.TypeOfAbstStructCode;
    //                objA_MobSelfInspection.NumberOfStruct = obj_selfInspection.NumberOfStruct;
    //                objA_MobSelfInspection.QuantOfRecharge = obj_selfInspection.QuantOfRecharge;
    //                objA_MobSelfInspection.NoOfPiezo = obj_selfInspection.NoOfPiezo;
    //                objA_MobSelfInspection.NoOfPiezoDWLR = obj_selfInspection.NoOfPiezoDWLR;
    //                objA_MobSelfInspection.NoOfPiezoTelem = obj_selfInspection.NoOfPiezoTelem;
    //                objA_MobSelfInspection.NoOfObserwell = obj_selfInspection.NoOfObserwell;
    //                objA_MobSelfInspection.NoOfFunctPiezo = obj_selfInspection.NoOfFunctPiezo;
    //                objA_MobSelfInspection.QuanttreatWasteWaterIND = obj_selfInspection.QuanttreatWasteWaterIND;
    //                objA_MobSelfInspection.QuanttreatWasteWaterINDGreen = obj_selfInspection.QuanttreatWasteWaterINDGreen;
    //                objA_MobSelfInspection.QuanttreatWasteWaterINDOther = obj_selfInspection.QuanttreatWasteWaterINDOther;
    //                objA_MobSelfInspection.AuditAgency = obj_selfInspection.AuditAgency;
    //                objA_MobSelfInspection.DateOfInspectionAudit = obj_selfInspection.DateOfInspectionAudit;
    //                objA_MobSelfInspection.AnyViolationNOCCondiDesc = obj_selfInspection.AnyViolationNOCCondiDesc;
    //                objA_MobSelfInspection.AnyOtherCompliancesDesc = obj_selfInspection.AnyOtherCompliancesDesc;
    //                objA_MobSelfInspection.WellFittedWithWaterFlowMeterAttCode = obj_selfInspection.WellFittedWithWaterFlowMeterAttCode;
    //                objA_MobSelfInspection.GeoRainWaterHarvRechAttCode = obj_selfInspection.GeoRainWaterHarvRechAttCode;
    //                objA_MobSelfInspection.GroundWaterQualityAttCode = obj_selfInspection.GroundWaterQualityAttCode;
    //                objA_MobSelfInspection.GroundwaterAbstractionDataAttCode = obj_selfInspection.GroundwaterAbstractionDataAttCode;
    //                objA_MobSelfInspection.NOCAttCode = obj_selfInspection.NOCAttCode;
    //                objA_MobSelfInspection.CopySiteInspectionAttCode = obj_selfInspection.CopySiteInspectionAttCode;
    //                objA_MobSelfInspection.GroundwaterMonitoringAttCode = obj_selfInspection.GroundwaterMonitoringAttCode;
    //                objA_MobSelfInspection.PhotoETPSTPAttCode = obj_selfInspection.PhotoETPSTPAttCode;
    //                objA_MobSelfInspection.WaterAuditAttCode = obj_selfInspection.WaterAuditAttCode;
    //                objA_MobSelfInspection.IARModelingAttCode = obj_selfInspection.IARModelingAttCode;
    //                objA_MobSelfInspection.ExtraAttCode = obj_selfInspection.ExtraAttCode;



    //                objA_MobSelfInspection.InspectionReport = obj_selfInspection.InspectionReport;
    //                objA_MobSelfInspection.PreGroundWaterAnyVari = obj_selfInspection.PreGroundWaterAnyVari;
    //                objA_MobSelfInspection.AbstrDataSubmittedTW = obj_selfInspection.AbstrDataSubmittedTW;
    //                objA_MobSelfInspection.StructPhoto = obj_selfInspection.StructPhoto;

    //                objA_MobSelfInspection.AbstStructFittedWithWM = obj_selfInspection.AbstStructFittedWithWM;
    //                objA_MobSelfInspection.TelemInstalled = obj_selfInspection.TelemInstalled;
    //                objA_MobSelfInspection.AnnuCalibOfWM = obj_selfInspection.AnnuCalibOfWM;
    //                objA_MobSelfInspection.PhotoWellFittedWithWM = obj_selfInspection.PhotoWellFittedWithWM;

    //                objA_MobSelfInspection.GWQuality = obj_selfInspection.GWQuality;
    //                objA_MobSelfInspection.MineSeepageQuality = obj_selfInspection.MineSeepageQuality;
    //                objA_MobSelfInspection.WaterSampleAnalyzed = obj_selfInspection.WaterSampleAnalyzed;
    //                objA_MobSelfInspection.GWReportWithinTime = obj_selfInspection.GWReportWithinTime;

    //                objA_MobSelfInspection.WithOutPremises = obj_selfInspection.WithOutPremises;
    //                objA_MobSelfInspection.PhotoRechargeStruct = obj_selfInspection.PhotoRechargeStruct;
    //                objA_MobSelfInspection.GeoPiezoAWLRTelem = obj_selfInspection.GeoPiezoAWLRTelem;
    //                objA_MobSelfInspection.MoniDataSubmitted = obj_selfInspection.MoniDataSubmitted;

    //                objA_MobSelfInspection.GeoPhotoOfSTP = obj_selfInspection.GeoPhotoOfSTP;
    //                objA_MobSelfInspection.SubSCWithinTimeFrame = obj_selfInspection.SubSCWithinTimeFrame;
    //                objA_MobSelfInspection.WaterAuditInspection = obj_selfInspection.WaterAuditInspection;
    //                objA_MobSelfInspection.WaterAuditReport = obj_selfInspection.WaterAuditReport;

    //                objA_MobSelfInspection.ImpactAssementReport = obj_selfInspection.ImpactAssementReport;
    //                objA_MobSelfInspection.ImpactAssementAIRReport = obj_selfInspection.ImpactAssementAIRReport;
    //                objA_MobSelfInspection.AnyViolationNOCCondi = obj_selfInspection.AnyViolationNOCCondi;
    //                objA_MobSelfInspection.AnyOtherCompliances = obj_selfInspection.AnyOtherCompliances;



    //                obj_MobSelfInspection.ActionTakenReportWithinOneYearDesc = obj_selfInspection.ActionTakenReportWithinOneYearDesc;
    //                obj_MobSelfInspection.PreSelfsentGroundWaterReqInYear = obj_selfInspection.PreSelfsentGroundWaterReqInYear;
    //                obj_MobSelfInspection.AbstraStructExistingAsPerNOC = obj_selfInspection.AbstraStructExistingAsPerNOC;
    //                obj_MobSelfInspection.AbstraStructProposedAsPerNOC = obj_selfInspection.AbstraStructProposedAsPerNOC;
    //                obj_MobSelfInspection.NumberOfFunMeter = obj_selfInspection.NumberOfFunMeter;
    //                obj_MobSelfInspection.NoOfSelfPiezoTelem = obj_selfInspection.NoOfSelfPiezoTelem;
    //                obj_MobSelfInspection.NoOfSelfPiezo = obj_selfInspection.NoOfSelfPiezo;
    //                obj_MobSelfInspection.NoOfPiezoDWLRTelem = obj_selfInspection.NoOfPiezoDWLRTelem;
    //                obj_MobSelfInspection.NoOfSelfPiezoDWLRTelem = obj_selfInspection.NoOfSelfPiezoDWLRTelem;
    //                obj_MobSelfInspection.GroundWaterMonitoringDataAttCode = obj_selfInspection.GroundWaterMonitoringDataAttCode;
    //                obj_MobSelfInspection.MiningSeepageAttCode = obj_selfInspection.MiningSeepageAttCode;
    //                obj_MobSelfInspection.AnnualCalibrationAttCode = obj_selfInspection.AnnualCalibrationAttCode;
    //                obj_MobSelfInspection.VerifiedBy = obj_selfInspection.VerifiedBy;
    //                obj_MobSelfInspection.VerifiedOn = obj_selfInspection.VerifiedOn;
    //                #endregion
    //                return Request.CreateResponse<MobSelfInspection>(System.Net.HttpStatusCode.OK, obj_MobSelfInspection);
    //            }
    //            else
    //                return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfInspectionEmpty);

    //            #endregion
    //        }
    //        else
    //            return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_MobSelfInspectionEmpty);
    //    }
    //    else
    //        return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, "Invalid user credentials");
    //}

    #region SelfInspectionAttachment
    [HttpPost]
    public HttpResponseMessage GetSelfInspectionNOCAttachment([FromBody] MobSelfInspection objA_MobSelfInspection)
    {
        NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment[] arr_SelfInspectionAttachment = null;
        if (objA_MobSelfInspection.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(objA_MobSelfInspection.ApplicationCode);

            arr_SelfInspectionAttachment = obj_selfInspection.GetNOCAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfInspectionAttachment);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }

    [HttpPost]
    public HttpResponseMessage GetSelfInspectionSiteInspectionAttachment([FromBody] MobSelfInspection objA_MobSelfInspection)
    {
        NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment[] arr_SelfInspectionAttachment = null;

        if (objA_MobSelfInspection.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(objA_MobSelfInspection.ApplicationCode);

            arr_SelfInspectionAttachment = obj_selfInspection.GetCopySiteInspectionAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfInspectionAttachment);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }

    [HttpPost]
    public HttpResponseMessage GetSelfInspectionGeoPhotoWithdrawalStructAttachment([FromBody] MobSelfInspection objA_MobSelfInspection)
    {
        NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment[] arr_SelfInspectionAttachment = null;
        if (objA_MobSelfInspection.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(objA_MobSelfInspection.ApplicationCode);

            arr_SelfInspectionAttachment = obj_selfInspection.GetGroundwaterAbstractionDataAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfInspectionAttachment);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }

    [HttpPost]
    public HttpResponseMessage GetSelfInspectionAnnualCalibrationAttachment([FromBody] MobSelfInspection objA_MobSelfInspection)
    {
        NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment[] arr_SelfInspectionAttachment = null;
        if (objA_MobSelfInspection.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(objA_MobSelfInspection.ApplicationCode);

            arr_SelfInspectionAttachment = obj_selfInspection.GetAnnualCalibrationAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfInspectionAttachment);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }

    [HttpPost]
    public HttpResponseMessage GetSelfInspectionGeoPhotowellFittedAttachment([FromBody] MobSelfInspection objA_MobSelfInspection)
    {
        NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment[] arr_SelfInspectionAttachment = null;
        if (objA_MobSelfInspection.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(objA_MobSelfInspection.ApplicationCode);

            arr_SelfInspectionAttachment = obj_selfInspection.GetWellFittedWithWaterFlowMeterAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfInspectionAttachment);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
    [HttpPost]
    public HttpResponseMessage GetSelfInspectionGWQualityReporAttachment([FromBody] MobSelfInspection objA_MobSelfInspection)
    {
        NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment[] arr_SelfInspectionAttachment = null;
        if (objA_MobSelfInspection.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(objA_MobSelfInspection.ApplicationCode);

            arr_SelfInspectionAttachment = obj_selfInspection.GetGroundWaterQualityAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfInspectionAttachment);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
    [HttpPost]
    public HttpResponseMessage GetSelfInspectionMineSeepageQualityReportAttachment([FromBody] MobSelfInspection objA_MobSelfInspection)
    {
        NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment[] arr_SelfInspectionAttachment = null;
        if (objA_MobSelfInspection.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(objA_MobSelfInspection.ApplicationCode);

            arr_SelfInspectionAttachment = obj_selfInspection.GetMiningSeepageAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfInspectionAttachment);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
    [HttpPost]
    public HttpResponseMessage GetSelfInspectionGeoPhotoRechargeStructuresAttachment([FromBody] MobSelfInspection objA_MobSelfInspection)
    {
        NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment[] arr_SelfInspectionAttachment = null;
        if (objA_MobSelfInspection.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(objA_MobSelfInspection.ApplicationCode);
            arr_SelfInspectionAttachment = obj_selfInspection.GetGeoRainWaterHarvRechAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfInspectionAttachment);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
    [HttpPost]
    public HttpResponseMessage GetSelfInspectionGeoPhotoPiezometersObservationDigitalTelemetryAttachment([FromBody] MobSelfInspection objA_MobSelfInspection)
    {
        NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment[] arr_SelfInspectionAttachment = null;
        if (objA_MobSelfInspection.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(objA_MobSelfInspection.ApplicationCode);

            arr_SelfInspectionAttachment = obj_selfInspection.GetGroundwaterMonitoringAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfInspectionAttachment);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
    [HttpPost]
    public HttpResponseMessage GetSelfInspectionGWMonitoringDataAttachment([FromBody] MobSelfInspection objA_MobSelfInspection)
    {
        NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment[] arr_SelfInspectionAttachment = null;
        if (objA_MobSelfInspection.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(objA_MobSelfInspection.ApplicationCode);

            arr_SelfInspectionAttachment = obj_selfInspection.GetGroundWaterMonitoringDataAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfInspectionAttachment);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }

    [HttpPost]
    public HttpResponseMessage GetSelfInspectionSewageTreatmentPlantEffluentTreatmentPlantAttachment([FromBody] MobSelfInspection objA_MobSelfInspection)
    {
        NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment[] arr_SelfInspectionAttachment = null;
        if (objA_MobSelfInspection.ApplicationCode != 0)
        {
            NOCAP.BLL.Misc.SelfInspection.SelfInspection obj_selfInspection = new NOCAP.BLL.Misc.SelfInspection.SelfInspection(objA_MobSelfInspection.ApplicationCode);

            arr_SelfInspectionAttachment = obj_selfInspection.GetPhotoETPSTPAttachmentList();
            return Request.CreateResponse(HttpStatusCode.OK, arr_SelfInspectionAttachment);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
        }
    }


    #endregion
    #endregion


    #region Common APIs
    private string[] GetApplicationNumberList(long userCode)
    {
        List<string> list_appNo = new List<string>();

        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(userCode));

        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication[] arrIndAppCount = obj_externalUser.GetSubmittedIndustrialNewApplicationList();

        var appNumber =
            from item in arrIndAppCount
            where item.ApplicantExternalUserCode == userCode && item.LatestApplicationStatusCode == 5
            select item.IndustrialNewApplicationNumber;

        list_appNo.AddRange(appNumber);

        appNumber = from item in obj_externalUser.GetSubmittedInfrastructureNewApplicationList()
                    where item.ApplicantExternalUserCode == userCode && item.LatestApplicationStatusCode == 5
                    select item.InfrastructureNewApplicationNumber;
        list_appNo.AddRange(appNumber);

        appNumber = from item in obj_externalUser.GetSubmittedMiningNewApplicationList()
                    where item.ApplicantExternalUserCode == userCode && item.LatestApplicationStatusCode == 5
                    select item.MiningNewApplicationNumber;
        list_appNo.AddRange(appNumber);
        return list_appNo.ToArray();
    }
    [HttpPost]
    public List<KeyValuePair<string, string>> GetStateList()
    {
        List<KeyValuePair<string, string>> list = null;
        NOCAP.BLL.Master.State obj = new NOCAP.BLL.Master.State();
        obj.GetAll();
        NOCAP.BLL.Master.State[] arr = obj.StateCollection;
        if (arr != null)
        {
            list = new List<KeyValuePair<string, string>>();
            foreach (var a in arr)
            {
                list.Add(new KeyValuePair<string, string>(a.StateCode.ToString(), a.StateName));
            }
        }
        // if (stateCode != "")
        //  list = list.Where(a=>a.Value== stateCode).ToList();
        return list;
    }
    [HttpPost]
    public List<KeyValuePair<string, string>> GetDistrictList([FromBody] ProposedLocation obj_ProposedLocation)
    {
        List<KeyValuePair<string, string>> list = null;
        NOCAP.BLL.Master.District obj = new NOCAP.BLL.Master.District();
        obj.GetAll();
        NOCAP.BLL.Master.District[] arr = obj.DistrictCollection.Where(a => a.StateCode == Convert.ToInt32(obj_ProposedLocation.stateCode)).ToArray();
        if (arr != null)
        {
            list = new List<KeyValuePair<string, string>>();
            foreach (var a in arr)
            {
                list.Add(new KeyValuePair<string, string>(a.DistrictCode.ToString(), a.DistrictName));
            }
        }
        return list;
    }
    [HttpPost]
    public List<KeyValuePair<string, string>> GetSubDistrictList(ProposedLocation obj_ProposedLocation)
    {
        List<KeyValuePair<string, string>> list = null;
        NOCAP.BLL.Master.SubDistrict obj = new NOCAP.BLL.Master.SubDistrict();
        obj.GetAll();
        NOCAP.BLL.Master.SubDistrict[] arr = obj.SubDistrictCollection.Where(a => a.StateCode == Convert.ToInt32(obj_ProposedLocation.stateCode)).ToArray().Where(a => a.DistrictCode == Convert.ToInt32(obj_ProposedLocation.districtCode)).ToArray();
        if (arr != null)
        {
            list = new List<KeyValuePair<string, string>>();
            foreach (var a in arr)
            {
                list.Add(new KeyValuePair<string, string>(a.SubDistrictCode.ToString(), a.SubDistrictName));
            }
        }
        return list;
    }

    [HttpPost]
    public List<KeyValuePair<string, string>> GetVillageList(ProposedLocation obj_ProposedLocation)
    {
        List<KeyValuePair<string, string>> list = null;
        NOCAP.BLL.Master.Village obj = new NOCAP.BLL.Master.Village();
        obj.GetAll();
        NOCAP.BLL.Master.Village[] arr = obj.VillageCollection.Where(a => a.StateCode == Convert.ToInt32(obj_ProposedLocation.stateCode)).ToArray().Where(a => a.DistrictCode == Convert.ToInt32(obj_ProposedLocation.districtCode)).Where(a => a.SubDistrictCode == Convert.ToInt32(obj_ProposedLocation.subDistrictCode)).ToArray();
        if (arr != null)
        {
            list = new List<KeyValuePair<string, string>>();
            foreach (var a in arr)
            {
                list.Add(new KeyValuePair<string, string>(a.VillageCode.ToString(), a.VillageName));
            }
        }
        return list;
    }
    [HttpPost]
    public List<KeyValuePair<string, string>> GetTownList(ProposedLocation obj_ProposedLocation)
    {
        List<KeyValuePair<string, string>> list = null;
        NOCAP.BLL.Master.Town obj = new NOCAP.BLL.Master.Town();
        obj.GetAll();
        NOCAP.BLL.Master.Town[] arr = obj.TownCollection.Where(a => a.StateCode == Convert.ToInt32(obj_ProposedLocation.stateCode)).ToArray().Where(a => a.DistrictCode == Convert.ToInt32(obj_ProposedLocation.districtCode)).Where(a => a.SubDistrictCode == Convert.ToInt32(obj_ProposedLocation.subDistrictCode)).ToArray();
        if (arr != null)
        {
            list = new List<KeyValuePair<string, string>>();
            foreach (var a in arr)
            {
                list.Add(new KeyValuePair<string, string>(a.TownCode.ToString(), a.TownName));
            }
        }
        return list;
    }
    [HttpPost]
    public HttpResponseMessage GetNOCNumberList([FromBody] AppModel obj_AppModel)
    {
        List<KeyValuePair<string, string>> list_NOCNumber = new List<KeyValuePair<string, string>>();
        if (obj_AppModel.appNumber.Contains("IND"))
        {
            #region IND
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
            obj_IndustrialNewApplication.GetAll();
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication[] arr = obj_IndustrialNewApplication.IndustrialNewApplicationCollection;

            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplicationForRenewal = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_AppModel.appNumber);

            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication();
            obj_IndustrialRenewApplication.GetAll();
            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication[] arrRen = obj_IndustrialRenewApplication.IndustrialRenewApplicationCollection;

            var NOCNumber =
                 from item in arr
                 where item.IndustrialNewApplicationNumber == obj_AppModel.appNumber && item.LatestApplicationStatusCode == 5 && item.NOCNumber != ""
                 select new KeyValuePair<string, string>(item.NOCNumber, item.IndustrialNewApplicationCode.ToString());
            list_NOCNumber.AddRange(NOCNumber);
            NOCNumber =
                from item in arrRen
                where item.FirstApplicationCode == obj_IndustrialNewApplicationForRenewal.IndustrialNewApplicationCode && item.LatestApplicationStatusCode == 5 && item.NOCNumber != ""
                select new KeyValuePair<string, string>(item.NOCNumber, item.IndustrialRenewApplicationCode.ToString());
            list_NOCNumber.AddRange(NOCNumber);
            #endregion
        }
        else if (obj_AppModel.appNumber.Contains("INF"))
        {
            #region INF
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
            obj_InfrastructureNewApplication.GetAll();
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication[] arr = obj_InfrastructureNewApplication.InfrastructureNewApplicationCollection;

            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplicationForRenewal = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_AppModel.appNumber);

            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication();
            obj_InfrastructureRenewApplication.GetAll();
            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication[] arrRen = obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCollection;

            var NOCNumber =
                 from item in arr
                 where item.InfrastructureNewApplicationNumber == obj_AppModel.appNumber && item.LatestApplicationStatusCode == 5 && item.NOCNumber != ""
                 select new KeyValuePair<string, string>(item.NOCNumber, item.InfrastructureNewApplicationCode.ToString());
            list_NOCNumber.AddRange(NOCNumber);
            NOCNumber =
              from item in arrRen
              where item.FirstApplicationCode == obj_InfrastructureNewApplicationForRenewal.InfrastructureNewApplicationCode && item.LatestApplicationStatusCode == 5 && item.NOCNumber != ""
              select new KeyValuePair<string, string>(item.NOCNumber, item.InfrastructureRenewApplicationCode.ToString());
            list_NOCNumber.AddRange(NOCNumber);
            #endregion
        }
        else if (obj_AppModel.appNumber.Contains("MIN"))
        {
            #region MIN
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication();
            obj_MiningNewApplication.GetAll();
            NOCAP.BLL.Mining.New.Application.MiningNewApplication[] arr = obj_MiningNewApplication.MiningNewApplicationCollection;

            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplicationForRenewal = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_AppModel.appNumber);

            NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication();
            obj_MiningRenewApplication.GetAll();
            NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication[] arrRen = obj_MiningRenewApplication.MiningRenewApplicationCollection;


            var NOCNumber =
                 from item in arr
                 where item.MiningNewApplicationNumber == obj_AppModel.appNumber && item.LatestApplicationStatusCode == 5 && item.NOCNumber != ""
                 select new KeyValuePair<string, string>(item.NOCNumber, item.ApplicationCode.ToString());
            list_NOCNumber.AddRange(NOCNumber);
            NOCNumber =
              from item in arrRen
              where item.FirstApplicationCode == obj_MiningNewApplicationForRenewal.ApplicationCode && item.LatestApplicationStatusCode == 5 && item.NOCNumber != ""
              select new KeyValuePair<string, string>(item.NOCNumber, item.MiningRenewApplicationCode.ToString());
            list_NOCNumber.AddRange(NOCNumber);
            #endregion
        }
        else
        {
            return Request.CreateResponse<List<KeyValuePair<string, string>>>(HttpStatusCode.NotFound, list_NOCNumber);
        }

        return Request.CreateResponse<List<KeyValuePair<string, string>>>(HttpStatusCode.OK, list_NOCNumber);
        //  return list_NOCNumber.ToArray();
    }

    [HttpPost]
    public HttpResponseMessage GetApplicationLocation([FromBody] ApplicationLocation obj_ApplicationLocation)
    {
        if (obj_ApplicationLocation.ApplicationCode != 0)
        {
            ApplicationLocation obj_applicationLocation = null;
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null;

            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = null;
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = null;
            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = null;
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = null;
            NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = null;
            NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_InfrastructureNewApplication, out obj_MiningNewApplication, out obj_IndustrialRenewApplication, out obj_InfrastructureRenewApplication, out obj_MiningRenewApplication, obj_ApplicationLocation.ApplicationCode);


            if (obj_IndustrialNewApplication != null)
            {
                #region IND New
                obj_applicationLocation = new ApplicationLocation();
                obj_applicationLocation.ApplicationCode = obj_ApplicationLocation.ApplicationCode;
                obj_applicationLocation.ApplicationNumber = obj_IndustrialNewApplication.IndustrialNewApplicationNumber;
                obj_applicationLocation.NOCNumber = obj_IndustrialNewApplication.NOCNumber;
                obj_applicationLocation.ApplicationName = obj_IndustrialNewApplication.NameOfIndustry;
                obj_applicationLocation.AddressLine1 = obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1;
                obj_applicationLocation.AddressLine2 = obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2;
                obj_applicationLocation.AddressLine3 = obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3;
                obj_applicationLocation.StateCode = obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode.ToString();
                obj_applicationLocation.StateName = new NOCAP.BLL.Master.State(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName.ToString();
                obj_applicationLocation.DistrictCode = obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode.ToString();
                obj_applicationLocation.DistrictName = new NOCAP.BLL.Master.District(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName.ToString();
                obj_applicationLocation.SubDistrictCode = obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode.ToString();
                obj_applicationLocation.SubDistrictName = new NOCAP.BLL.Master.SubDistrict(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName.ToString();
                obj_applicationLocation.VillageCode = obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode.ToString();
                obj_applicationLocation.VillageName = new NOCAP.BLL.Master.Village(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode).VillageName.ToString();
                obj_applicationLocation.TownCode = obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.TownCode.ToString();
                obj_applicationLocation.TownName = new NOCAP.BLL.Master.Town(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.TownCode).TownName.ToString();
                obj_applicationLocation.Lat = obj_IndustrialNewApplication.ProposedLocation.ProposedLatitude.ToString();
                obj_applicationLocation.Long = obj_IndustrialNewApplication.ProposedLocation.ProposedLongitude.ToString();
                #endregion
            }
            else if (obj_IndustrialRenewApplication != null)
            {
                #region IND Renew
                obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_IndustrialRenewApplication.FirstApplicationCode);
                obj_applicationLocation = new ApplicationLocation();
                obj_applicationLocation.ApplicationCode = obj_ApplicationLocation.ApplicationCode;
                obj_applicationLocation.ApplicationNumber = obj_IndustrialNewApplication.IndustrialNewApplicationNumber;
                obj_applicationLocation.NOCNumber = obj_IndustrialNewApplication.NOCNumber;
                obj_applicationLocation.ApplicationName = obj_IndustrialNewApplication.NameOfIndustry;
                obj_applicationLocation.AddressLine1 = obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1;
                obj_applicationLocation.AddressLine2 = obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2;
                obj_applicationLocation.AddressLine3 = obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3;
                obj_applicationLocation.StateCode = obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode.ToString();
                obj_applicationLocation.DistrictCode = obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode.ToString();
                obj_applicationLocation.SubDistrictCode = obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode.ToString();
                obj_applicationLocation.VillageCode = obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode.ToString();
                obj_applicationLocation.TownCode = obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.TownCode.ToString();
                obj_applicationLocation.Lat = obj_IndustrialNewApplication.ProposedLocation.ProposedLatitude.ToString();
                obj_applicationLocation.Long = obj_IndustrialNewApplication.ProposedLocation.ProposedLongitude.ToString();
                obj_applicationLocation.StateName = new NOCAP.BLL.Master.State(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName.ToString();
                obj_applicationLocation.DistrictName = new NOCAP.BLL.Master.District(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName.ToString();
                obj_applicationLocation.SubDistrictName = new NOCAP.BLL.Master.SubDistrict(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName.ToString();
                obj_applicationLocation.VillageName = new NOCAP.BLL.Master.Village(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode).VillageName.ToString();
                obj_applicationLocation.TownName = new NOCAP.BLL.Master.Town(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.TownCode).TownName.ToString();
                #endregion
            }
            else if (obj_InfrastructureNewApplication != null)
            {
                #region INF New
                obj_applicationLocation = new ApplicationLocation();
                obj_applicationLocation.ApplicationCode = obj_ApplicationLocation.ApplicationCode;
                obj_applicationLocation.ApplicationNumber = obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber;
                obj_applicationLocation.NOCNumber = obj_InfrastructureNewApplication.NOCNumber;
                obj_applicationLocation.ApplicationName = obj_InfrastructureNewApplication.NameOfInfrastructure;
                obj_applicationLocation.AddressLine1 = obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1;
                obj_applicationLocation.AddressLine2 = obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2;
                obj_applicationLocation.AddressLine3 = obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3;
                obj_applicationLocation.StateCode = obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode.ToString();
                obj_applicationLocation.DistrictCode = obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode.ToString();
                obj_applicationLocation.SubDistrictCode = obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode.ToString();
                obj_applicationLocation.VillageCode = obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode.ToString();
                obj_applicationLocation.TownCode = obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.TownCode.ToString();
                obj_applicationLocation.Lat = obj_InfrastructureNewApplication.ProposedLocation.ProposedLatitude.ToString();
                obj_applicationLocation.Long = obj_InfrastructureNewApplication.ProposedLocation.ProposedLongitude.ToString();
                obj_applicationLocation.StateName = new NOCAP.BLL.Master.State(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName.ToString();
                obj_applicationLocation.DistrictName = new NOCAP.BLL.Master.District(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName.ToString();
                obj_applicationLocation.SubDistrictName = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName.ToString();
                obj_applicationLocation.VillageName = new NOCAP.BLL.Master.Village(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode).VillageName.ToString();
                obj_applicationLocation.TownName = new NOCAP.BLL.Master.Town(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.TownCode).TownName.ToString();
                #endregion
            }
            else if (obj_InfrastructureRenewApplication != null)
            {
                #region INF Renew
                obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_InfrastructureRenewApplication.FirstApplicationCode);
                obj_applicationLocation = new ApplicationLocation();
                obj_applicationLocation.ApplicationCode = obj_ApplicationLocation.ApplicationCode;
                obj_applicationLocation.ApplicationNumber = obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber;
                obj_applicationLocation.NOCNumber = obj_InfrastructureNewApplication.NOCNumber;
                obj_applicationLocation.ApplicationName = obj_InfrastructureNewApplication.NameOfInfrastructure;
                obj_applicationLocation.AddressLine1 = obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1;
                obj_applicationLocation.AddressLine2 = obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2;
                obj_applicationLocation.AddressLine3 = obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3;
                obj_applicationLocation.StateCode = obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode.ToString();
                obj_applicationLocation.DistrictCode = obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode.ToString();
                obj_applicationLocation.SubDistrictCode = obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode.ToString();
                obj_applicationLocation.VillageCode = obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode.ToString();
                obj_applicationLocation.TownCode = obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.TownCode.ToString();
                obj_applicationLocation.Lat = obj_InfrastructureNewApplication.ProposedLocation.ProposedLatitude.ToString();
                obj_applicationLocation.Long = obj_InfrastructureNewApplication.ProposedLocation.ProposedLongitude.ToString();
                obj_applicationLocation.StateName = new NOCAP.BLL.Master.State(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName.ToString();
                obj_applicationLocation.DistrictName = new NOCAP.BLL.Master.District(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName.ToString();
                obj_applicationLocation.SubDistrictName = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName.ToString();
                obj_applicationLocation.VillageName = new NOCAP.BLL.Master.Village(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode).VillageName.ToString();
                obj_applicationLocation.TownName = new NOCAP.BLL.Master.Town(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.TownCode).TownName.ToString();
                #endregion
            }
            else if (obj_MiningNewApplication != null)
            {
                #region MIN New
                obj_applicationLocation = new ApplicationLocation();
                obj_applicationLocation.ApplicationCode = obj_ApplicationLocation.ApplicationCode;
                obj_applicationLocation.ApplicationNumber = obj_MiningNewApplication.MiningNewApplicationNumber;
                obj_applicationLocation.NOCNumber = obj_MiningNewApplication.NOCNumber;
                obj_applicationLocation.ApplicationName = obj_MiningNewApplication.NameOfMining;
                obj_applicationLocation.AddressLine1 = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1;
                obj_applicationLocation.AddressLine2 = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2;
                obj_applicationLocation.AddressLine3 = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3;
                obj_applicationLocation.StateCode = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode.ToString();
                obj_applicationLocation.DistrictCode = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode.ToString();
                obj_applicationLocation.SubDistrictCode = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode.ToString();
                obj_applicationLocation.VillageCode = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode.ToString();
                obj_applicationLocation.TownCode = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode.ToString();
                obj_applicationLocation.Lat = obj_MiningNewApplication.ProposedLocation.ProposedLatitude.ToString();
                obj_applicationLocation.Long = obj_MiningNewApplication.ProposedLocation.ProposedLongitude.ToString();
                obj_applicationLocation.StateName = new NOCAP.BLL.Master.State(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName.ToString();
                obj_applicationLocation.DistrictName = new NOCAP.BLL.Master.District(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName.ToString();
                obj_applicationLocation.SubDistrictName = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName.ToString();
                obj_applicationLocation.VillageName = new NOCAP.BLL.Master.Village(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode).VillageName.ToString();
                obj_applicationLocation.TownName = new NOCAP.BLL.Master.Town(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode).TownName.ToString();
                #endregion
            }
            else if (obj_MiningRenewApplication != null)
            {
                #region MIN Renew
                obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_MiningRenewApplication.FirstApplicationCode);
                obj_applicationLocation = new ApplicationLocation();
                obj_applicationLocation.ApplicationCode = obj_ApplicationLocation.ApplicationCode;
                obj_applicationLocation.ApplicationNumber = obj_MiningNewApplication.MiningNewApplicationNumber;
                obj_applicationLocation.NOCNumber = obj_MiningNewApplication.NOCNumber;
                obj_applicationLocation.ApplicationName = obj_MiningNewApplication.NameOfMining;
                obj_applicationLocation.AddressLine1 = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1;
                obj_applicationLocation.AddressLine2 = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2;
                obj_applicationLocation.AddressLine3 = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3;
                obj_applicationLocation.StateCode = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode.ToString();
                obj_applicationLocation.DistrictCode = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode.ToString();
                obj_applicationLocation.SubDistrictCode = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode.ToString();
                obj_applicationLocation.VillageCode = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode.ToString();
                obj_applicationLocation.TownCode = obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode.ToString();
                obj_applicationLocation.Lat = obj_MiningNewApplication.ProposedLocation.ProposedLatitude.ToString();
                obj_applicationLocation.Long = obj_MiningNewApplication.ProposedLocation.ProposedLongitude.ToString();
                obj_applicationLocation.StateName = new NOCAP.BLL.Master.State(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName.ToString();
                obj_applicationLocation.DistrictName = new NOCAP.BLL.Master.District(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName.ToString();
                obj_applicationLocation.SubDistrictName = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName.ToString();
                obj_applicationLocation.VillageName = new NOCAP.BLL.Master.Village(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode).VillageName.ToString();
                obj_applicationLocation.TownName = new NOCAP.BLL.Master.Town(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode).TownName.ToString();
                #endregion
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj_applicationLocation);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Appliccation Code can not be 0 , null and Empty");
        }
    }
    #endregion



    #region  Mob API


    [HttpPost]
    public HttpResponseMessage SubmitSelfCompliance(string api_access_token, string applicationCode)
    {
        string applicationNumber = "";
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(Convert.ToInt64(applicationCode));
        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(Convert.ToInt64(applicationCode));
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(Convert.ToInt64(applicationCode));
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(Convert.ToInt64(applicationCode));
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(Convert.ToInt64(applicationCode));
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(Convert.ToInt64(applicationCode));
        NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_InfrastructureNewApplication, out obj_MiningNewApplication, out obj_IndustrialRenewApplication, out obj_InfrastructureRenewApplication, out obj_MiningRenewApplication, Convert.ToInt64(applicationCode));
        if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.CreatedByExUC > 0)
        {
            applicationNumber = obj_IndustrialNewApplication.IndustrialNewApplicationNumber;
        }
        else if (obj_IndustrialRenewApplication != null && obj_IndustrialRenewApplication.CreatedByExUC > 0)
        {
            obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_IndustrialRenewApplication.FirstApplicationCode);
            applicationNumber = obj_IndustrialNewApplication.IndustrialNewApplicationNumber;
        }

        else if (obj_InfrastructureNewApplication != null && obj_InfrastructureNewApplication.CreatedByExUC > 0)
        {
            applicationNumber = obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber;
        }
        else if (obj_InfrastructureRenewApplication != null && obj_InfrastructureRenewApplication.CreatedByExUC > 0)
        {
            obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_InfrastructureRenewApplication.FirstApplicationCode);
            applicationNumber = obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber;
        }

        else if (obj_MiningNewApplication != null && obj_MiningNewApplication.CreatedByExUC > 0)
        {
            applicationNumber = obj_MiningNewApplication.MiningNewApplicationNumber;
        }
        else if (obj_MiningRenewApplication != null && obj_MiningRenewApplication.CreatedByExUC > 0)
        {
            obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_MiningRenewApplication.FirstApplicationCode);
            applicationNumber = obj_MiningNewApplication.MiningNewApplicationNumber;
        }
        SelfCompliance obj_SelfCompliance = new SelfCompliance(Convert.ToInt64(applicationCode));
        if (obj_SelfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.No)
        {
            HttpResponseMessage obj_selfComplianceBLL = SubmitSelfComplianceResponse(api_access_token, applicationCode, applicationNumber);
            if (obj_selfComplianceBLL.IsSuccessStatusCode)
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Record Add Successfully");
            else
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Record not Add Successfully");
        }
        else
            return Request.CreateErrorResponse(HttpStatusCode.Conflict, obj_SelfCompliance.CustumMessage);
    }

    // private HttpResponseMessage SubmitSelfCompliance(SelfComplianceResponse Roots, string api_access_token, string applicationCode, string applicationNumber)
    private HttpResponseMessage SubmitSelfComplianceResponse(string api_access_token, string applicationCode, string applicationNumber)
    {
        HttpResponseMessage messge = null;
        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://scgapp.aims-cgwb.org/api_pass/index.php");
            client.DefaultRequestHeaders.Accept.Clear();
            string registerUserJson = APIUtility.stringToJson(api_access_token, applicationCode, applicationNumber);

            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Uri.EscapeUriString(client.BaseAddress.ToString()));
            request.Content = new StringContent(registerUserJson, Encoding.UTF8, "application/json");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            messge = client.PostAsync("https://scgapp.aims-cgwb.org/api_pass/index.php", request.Content).Result;
            SelfComplianceResponse.Root obj_SelfComplianceResponseRoot = null;

            if (messge != null)
            {
                if (messge.IsSuccessStatusCode)
                {
                    string result = messge.Content.ReadAsStringAsync().Result;
                    obj_SelfComplianceResponseRoot = new SelfComplianceResponse.Root();
                    obj_SelfComplianceResponseRoot = JsonConvert.DeserializeObject<SelfComplianceResponse.Root>(result);


                    if (obj_SelfComplianceResponseRoot != null)
                    {
                        NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfComplianceBLL = new NOCAP.BLL.Misc.Compliance.SelfCompliance(obj_SelfComplianceResponseRoot.form_data.applicationCode);

                        obj_selfComplianceBLL.NOCNo = obj_SelfComplianceResponseRoot.form_data.nocNumber;
                        obj_selfComplianceBLL.QuantOfRecharge = obj_SelfComplianceResponseRoot.form_data.quantumOfRecharge;
                        obj_selfComplianceBLL.CapOfSTPETP = obj_SelfComplianceResponseRoot.form_data.capicityOfStpEtp;

                        switch (obj_SelfComplianceResponseRoot.form_data.copyOfSiteInspReportCon)
                        {
                            case 0:
                                obj_selfComplianceBLL.InspectionReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                                break;
                            case 1:
                                obj_selfComplianceBLL.InspectionReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                                break;
                            default:
                                obj_selfComplianceBLL.InspectionReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                                break;
                        }

                        switch (Convert.ToInt32(obj_SelfComplianceResponseRoot.form_data.anualCalWaterMtrGovt))
                        {
                            case 0:
                                obj_selfComplianceBLL.AnnuCalibOfWM = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                                break;
                            case 1:
                                obj_selfComplianceBLL.AnnuCalibOfWM = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                                break;
                            default:
                                obj_selfComplianceBLL.AnnuCalibOfWM = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                                break;
                        }

                        switch (Convert.ToInt32(obj_SelfComplianceResponseRoot.form_data.gwQualityReportAtach))
                        {
                            case 0:
                                obj_selfComplianceBLL.GWQuality = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                                break;
                            case 1:
                                obj_selfComplianceBLL.GWQuality = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                                break;
                            default:
                                obj_selfComplianceBLL.GWQuality = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                                break;
                        }

                        switch (Convert.ToInt32(obj_SelfComplianceResponseRoot.form_data.mineSepageQltyRep))
                        {
                            case 0:
                                obj_selfComplianceBLL.MineSeepageQuality = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                                break;
                            case 1:
                                obj_selfComplianceBLL.MineSeepageQuality = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                                break;
                            default:
                                obj_selfComplianceBLL.MineSeepageQuality = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                                break;
                        }

                        switch (Convert.ToInt32(obj_SelfComplianceResponseRoot.form_data.gwMoniteringData))
                        {
                            case 0:
                                obj_selfComplianceBLL.GroundWaterMonitoringData = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
                                break;
                            case 1:
                                obj_selfComplianceBLL.GroundWaterMonitoringData = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                                break;
                            default:
                                obj_selfComplianceBLL.GroundWaterMonitoringData = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                                break;
                        }

                        obj_selfComplianceBLL.StructPhoto = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                        obj_selfComplianceBLL.PhotoWellFittedWithWM = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                        obj_selfComplianceBLL.PhotoRechargeStruct = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                        obj_selfComplianceBLL.GeoPiezoAWLRTelem = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                        obj_selfComplianceBLL.GeoPhotoOfSTP = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                        obj_selfComplianceBLL.WaterAuditReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
                        obj_selfComplianceBLL.ImpactAssementAIRReport = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;

                        obj_selfComplianceBLL.ComplianceSubmitDate = Convert.ToDateTime(obj_SelfComplianceResponseRoot.form_data.submitted_at);
                        obj_selfComplianceBLL.AnyOtherAgency = obj_SelfComplianceResponseRoot.form_data.nameOfAgencyType;
                        obj_selfComplianceBLL.DateOfInspection = Convert.ToDateTime(obj_SelfComplianceResponseRoot.form_data.dateOfInspection);
                        obj_selfComplianceBLL.MeterTypeCode = Convert.ToInt32(obj_SelfComplianceResponseRoot.form_data.typeOfMetter);
                        obj_selfComplianceBLL.NumberOfFunMeter = Convert.ToInt32(obj_SelfComplianceResponseRoot.form_data.NoOfFunctionalMeter);
                        obj_selfComplianceBLL.NoOfPiezo = Convert.ToInt32(obj_SelfComplianceResponseRoot.form_data.noOfPiezometerInsNoc);
                        obj_selfComplianceBLL.NoOfSelfPiezo = Convert.ToInt32(obj_SelfComplianceResponseRoot.form_data.noOfPiezometerInsSelf);
                        obj_selfComplianceBLL.NoOfPiezoDWLR = Convert.ToInt32(obj_SelfComplianceResponseRoot.form_data.piezometerWithDWLR);
                        obj_selfComplianceBLL.NoOfPiezoTelem = Convert.ToInt32(obj_SelfComplianceResponseRoot.form_data.piezometerWithTelemeter);
                        obj_selfComplianceBLL.NoOfObserwell = Convert.ToInt32(obj_SelfComplianceResponseRoot.form_data.numOfObjWellKey);
                        obj_selfComplianceBLL.CapOfSTPETP = Convert.ToInt32(obj_SelfComplianceResponseRoot.form_data.capicityOfStpEtp);
                        obj_selfComplianceBLL.QuanttreatWasteWater = Convert.ToDecimal(obj_SelfComplianceResponseRoot.form_data.quantmOfWasteWater);
                        obj_selfComplianceBLL.ExtraAttCode = Convert.ToInt32(obj_SelfComplianceResponseRoot.form_data.extraAttachments);
                        obj_selfComplianceBLL.Remarks = Convert.ToString(obj_SelfComplianceResponseRoot.form_data.remarks);

                        if (obj_selfComplianceBLL != null && obj_selfComplianceBLL.ApplicationCode == 0)
                        {
                            obj_selfComplianceBLL.ApplicationCode = obj_SelfComplianceResponseRoot.form_data.applicationCode;
                            obj_selfComplianceBLL.CreatedByExUC = Convert.ToInt32(obj_SelfComplianceResponseRoot.form_data.userCode);
                            int int_result = obj_selfComplianceBLL.Add();
                            if (int_result == 1)
                            {
                                // return Request.CreateResponse(HttpStatusCode.OK, obj_selfComplianceBLL.CustumMessage);
                            }
                            else
                            {
                                //return Request.CreateErrorResponse(HttpStatusCode.Conflict, obj_selfComplianceBLL.CustumMessage);
                            }
                        }
                        else
                        {
                            obj_selfComplianceBLL.ModifiedByExUC = Convert.ToInt64(obj_SelfComplianceResponseRoot.form_data.userCode);
                            int int_result2 = obj_selfComplianceBLL.Update();
                            if (int_result2 == 1)
                            {
                                //return Request.CreateResponse(HttpStatusCode.OK, obj_selfComplianceBLL.CustumMessage);
                            }
                            else
                            {
                                //return Request.CreateErrorResponse(HttpStatusCode.Conflict, obj_selfComplianceBLL.CustumMessage);

                            }
                        }

                        HttpResponseMessage obj_HttpResponseMessage = Attachments(Convert.ToInt32(obj_SelfComplianceResponseRoot.form_data.userCode), Convert.ToInt64(applicationCode), api_access_token, applicationNumber);







                        if (obj_HttpResponseMessage.StatusCode == HttpStatusCode.OK)
                        {

                            NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_SelfComplianceAttachment = null;

                            NOCAP.BLL.Misc.Compliance.SelfCompliance objA_SelfComplianceBLL = new NOCAP.BLL.Misc.Compliance.SelfCompliance(obj_SelfComplianceResponseRoot.form_data.applicationCode);
                            NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();

                            NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachmentC = null;

                            obj_selfComplianceAttachment.ApplicationCode = obj_SelfComplianceResponseRoot.form_data.applicationCode;

                            var GetData = obj_selfComplianceAttachment.GetAll();

                            arr_SelfComplianceAttachment = obj_selfComplianceAttachment.SelfComplianceAttachmentCollection;


                            obj_selfComplianceAttachmentC = (NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment)arr_SelfComplianceAttachment.Where(a => a.AttachmentCode == objA_SelfComplianceBLL.NOCAttCode).FirstOrDefault();

                            if (obj_selfComplianceAttachmentC == null)

                                return Request.CreateResponse(HttpStatusCode.NotFound, "Noc Number is not found");

                            if (objA_SelfComplianceBLL.InspectionReport == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                            {
                                obj_selfComplianceAttachmentC = (NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment)arr_SelfComplianceAttachment.Where(a => a.AttachmentCode == objA_SelfComplianceBLL.CopySiteInspectionAttCode).FirstOrDefault();

                                if (obj_selfComplianceAttachmentC == null)

                                    return Request.CreateResponse(HttpStatusCode.NotFound, "CopySiteInspection Attachments not found");
                            }

                            if (objA_SelfComplianceBLL.StructPhoto == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                            {
                                obj_selfComplianceAttachmentC = (NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment)arr_SelfComplianceAttachment.Where(a => a.AttachmentCode == objA_SelfComplianceBLL.GroundwaterAbstractionDataAttCode).FirstOrDefault();

                                if (obj_selfComplianceAttachmentC == null)

                                    return Request.CreateResponse(HttpStatusCode.NotFound, "GroundwaterAbstractionData Attachments not found");
                            }


                            if (objA_SelfComplianceBLL.AnnuCalibOfWM == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                            {
                                obj_selfComplianceAttachmentC = (NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment)arr_SelfComplianceAttachment.Where(a => a.AttachmentCode == objA_SelfComplianceBLL.AnnualCalibrationAttCode).FirstOrDefault();

                                if (obj_selfComplianceAttachmentC == null)

                                    return Request.CreateResponse(HttpStatusCode.NotFound, "AnnualCalibration Attachments not found");
                            }


                            if (objA_SelfComplianceBLL.PhotoWellFittedWithWM == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                            {
                                obj_selfComplianceAttachmentC = (NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment)arr_SelfComplianceAttachment.Where(a => a.AttachmentCode == objA_SelfComplianceBLL.WellFittedWithWaterFlowMeterAttCode).FirstOrDefault();

                                if (obj_selfComplianceAttachmentC == null)

                                    return Request.CreateResponse(HttpStatusCode.NotFound, "WellFittedWithWaterFlowMeter Attachments not found");
                            }


                            if (objA_SelfComplianceBLL.GWQuality == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                            {
                                obj_selfComplianceAttachmentC = (NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment)arr_SelfComplianceAttachment.Where(a => a.AttachmentCode == objA_SelfComplianceBLL.GroundWaterQualityAttCode).FirstOrDefault();

                                if (obj_selfComplianceAttachmentC == null)

                                    return Request.CreateResponse(HttpStatusCode.NotFound, "GroundWaterQuality Attachments not found");
                            }


                            if (objA_SelfComplianceBLL.MineSeepageQuality == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                            {
                                obj_selfComplianceAttachmentC = (NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment)arr_SelfComplianceAttachment.Where(a => a.AttachmentCode == objA_SelfComplianceBLL.MiningSeepageAttCode).FirstOrDefault();

                                if (obj_selfComplianceAttachmentC == null)

                                    return Request.CreateResponse(HttpStatusCode.NotFound, "MiningSeepage Attachments not found");
                            }


                            if (objA_SelfComplianceBLL.PhotoRechargeStruct == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                            {
                                obj_selfComplianceAttachmentC = (NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment)arr_SelfComplianceAttachment.Where(a => a.AttachmentCode == objA_SelfComplianceBLL.GeoRainWaterHarvRechAttCode).FirstOrDefault();

                                if (obj_selfComplianceAttachmentC == null)

                                    return Request.CreateResponse(HttpStatusCode.NotFound, "GeoRainWaterHarvRech Attachments not found");
                            }

                            if (objA_SelfComplianceBLL.GeoPiezoAWLRTelem == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                            {
                                obj_selfComplianceAttachmentC = (NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment)arr_SelfComplianceAttachment.Where(a => a.AttachmentCode == objA_SelfComplianceBLL.GroundwaterMonitoringAttCode).FirstOrDefault();

                                if (obj_selfComplianceAttachmentC == null)

                                    return Request.CreateResponse(HttpStatusCode.NotFound, "GroundwaterMonitoring Attachments not found");
                            }

                            if (objA_SelfComplianceBLL.GroundWaterMonitoringData == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                            {
                                obj_selfComplianceAttachmentC = (NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment)arr_SelfComplianceAttachment.Where(a => a.AttachmentCode == objA_SelfComplianceBLL.GroundWaterMonitoringDataAttCode).FirstOrDefault();

                                if (obj_selfComplianceAttachmentC == null)

                                    return Request.CreateResponse(HttpStatusCode.NotFound, "GroundWaterMonitoringData Attachments not found");
                            }

                            if (objA_SelfComplianceBLL.GeoPhotoOfSTP == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                            {
                                obj_selfComplianceAttachmentC = (NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment)arr_SelfComplianceAttachment.Where(a => a.AttachmentCode == objA_SelfComplianceBLL.PhotoETPSTPAttCode).FirstOrDefault();

                                if (obj_selfComplianceAttachmentC == null)

                                    return Request.CreateResponse(HttpStatusCode.NotFound, "PhotoETPSTP Attachments not found");
                            }

                            if (objA_SelfComplianceBLL.WaterAuditReport == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                            {
                                obj_selfComplianceAttachmentC = (NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment)arr_SelfComplianceAttachment.Where(a => a.AttachmentCode == objA_SelfComplianceBLL.WaterAuditAttCode).FirstOrDefault();

                                if (obj_selfComplianceAttachmentC == null)

                                    return Request.CreateResponse(HttpStatusCode.NotFound, "WaterAudit Attachments not found");
                            }


                            if (objA_SelfComplianceBLL.ImpactAssementAIRReport == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
                            {
                                obj_selfComplianceAttachmentC = (NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment)arr_SelfComplianceAttachment.Where(a => a.AttachmentCode == objA_SelfComplianceBLL.IARModelingAttCode).FirstOrDefault();

                                if (obj_selfComplianceAttachmentC == null)

                                    return Request.CreateResponse(HttpStatusCode.NotFound, "IARModeling Attachments not found");
                            }

                            obj_selfComplianceAttachmentC = (NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment)arr_SelfComplianceAttachment.Where(a => a.AttachmentCode == objA_SelfComplianceBLL.ExtraAttCode).FirstOrDefault();
                            if (obj_selfComplianceAttachmentC == null)
                                return Request.CreateResponse(HttpStatusCode.NotFound, "Extra Attachments not found");
                        }
                        NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(obj_selfComplianceBLL.ApplicationCode);

                        if (obj_SelfCompliance.ApplicationCode != 0)
                        {
                            if (obj_SelfCompliance.SubmitApplication(obj_SelfCompliance.ApplicationCode, null, Convert.ToInt32(obj_SelfComplianceResponseRoot.form_data.userCode)) == 1)
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, obj_selfComplianceBLL.CustumMessage);
                            }
                            else
                            {

                                return Request.CreateErrorResponse(HttpStatusCode.Conflict, obj_SelfCompliance.CustumMessage);

                            }
                        }

                    }
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Something went Wrong");
            }
        }
        catch (WebException ex)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
        }
        return messge;
    }

    /////////////       Model Class API Attachments /////////////   
    private HttpResponseMessage Attachments(int userCode, long ApplicationCode, string api_access_token, string applicationNumber)
    {
        HttpResponseMessage messge = null;
        try
        {
            SelfComplianceAttachment obj_SelfComplianceAttachment = new SelfComplianceAttachment(ApplicationCode);
            if (obj_SelfComplianceAttachment != null && obj_SelfComplianceAttachment.CreatedByExUC > 0)
            {
                obj_SelfComplianceAttachment.GetAll();
                SelfComplianceAttachment[] arr_SelfComplianceAttachment = obj_SelfComplianceAttachment.SelfComplianceAttachmentCollection;
                if (arr_SelfComplianceAttachment != null && arr_SelfComplianceAttachment.Length > 0)
                {
                    SelfComplianceAttachment obj_SelfComplianceAttachmentForDelete = null;
                    foreach (SelfComplianceAttachment obj_SelfComplianceAttachmentFor in arr_SelfComplianceAttachment)
                    {
                        obj_SelfComplianceAttachmentForDelete = new SelfComplianceAttachment(ApplicationCode, obj_SelfComplianceAttachmentFor.AttachmentCode, obj_SelfComplianceAttachmentFor.AttachmentCodeSerialNumber);
                        obj_SelfComplianceAttachmentForDelete.Delete();
                    }

                }
            }
            SelfCompliance obj_selfCompliance = new SelfCompliance(ApplicationCode);
            messge = Attachment(userCode, ApplicationCode, api_access_token, applicationNumber, obj_selfCompliance.NOCAttCode);
            messge = Attachment(userCode, ApplicationCode, api_access_token, applicationNumber, obj_selfCompliance.CopySiteInspectionAttCode);
            messge = Attachment(userCode, ApplicationCode, api_access_token, applicationNumber, obj_selfCompliance.GroundwaterAbstractionDataAttCode);
            messge = Attachment(userCode, ApplicationCode, api_access_token, applicationNumber, obj_selfCompliance.WellFittedWithWaterFlowMeterAttCode);
            messge = Attachment(userCode, ApplicationCode, api_access_token, applicationNumber, obj_selfCompliance.AnnualCalibrationAttCode);
            messge = Attachment(userCode, ApplicationCode, api_access_token, applicationNumber, obj_selfCompliance.GroundWaterQualityAttCode);
            messge = Attachment(userCode, ApplicationCode, api_access_token, applicationNumber, obj_selfCompliance.MiningSeepageAttCode);
            messge = Attachment(userCode, ApplicationCode, api_access_token, applicationNumber, obj_selfCompliance.GeoRainWaterHarvRechAttCode);
            messge = Attachment(userCode, ApplicationCode, api_access_token, applicationNumber, obj_selfCompliance.GroundwaterMonitoringAttCode);
            messge = Attachment(userCode, ApplicationCode, api_access_token, applicationNumber, obj_selfCompliance.GroundWaterMonitoringDataAttCode);
            messge = Attachment(userCode, ApplicationCode, api_access_token, applicationNumber, obj_selfCompliance.PhotoETPSTPAttCode);
            messge = Attachment(userCode, ApplicationCode, api_access_token, applicationNumber, obj_selfCompliance.ExtraAttCode);
            messge = Attachment(userCode, ApplicationCode, api_access_token, applicationNumber, obj_selfCompliance.WaterAuditAttCode);
            messge = Attachment(userCode, ApplicationCode, api_access_token, applicationNumber, obj_selfCompliance.IARModelingAttCode);

        }
        catch (WebException ex)
        {
            return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
        }

        return messge;
    }
    private HttpResponseMessage Attachment(int userCode, long ApplicationCode, string api_access_token, string applicationNumber, int imgType)
    {
        HttpResponseMessage messge = null;
        try
        {


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://scgapp.aims-cgwb.org/api_pass/getAllImgFile.php");
            client.DefaultRequestHeaders.Accept.Clear();

            string registerUserJson = APIUtility.stringToJson(api_access_token, applicationNumber, imgType.ToString(), ApplicationCode);
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Uri.EscapeUriString(client.BaseAddress.ToString()));
            request.Content = new StringContent(registerUserJson, Encoding.UTF8, "application/json");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            messge = client.PostAsync("https://scgapp.aims-cgwb.org/api_pass/getAllImgFile.php", request.Content).Result;

            SelfComplianceAttResponse.Root objroot = null;

            if (messge != null)
            {
                if (messge.IsSuccessStatusCode)
                {
                    string result = messge.Content.ReadAsStringAsync().Result;

                    objroot = new SelfComplianceAttResponse.Root();

                    objroot = JsonConvert.DeserializeObject<SelfComplianceAttResponse.Root>(result);

                }
                SelfComplianceAttachment obj_selfComplianceAttachment = new SelfComplianceAttachment();
                SelfCompliance obj_selfComplianceBLL = new SelfCompliance(ApplicationCode);

                foreach (SelfComplianceAttResponse.AllImage item in objroot.all_image)
                {
                    obj_selfComplianceAttachment.ApplicationCode = item.appCode;
                    string str_ext = item.fileExt.ToString();
                    if (str_ext == "doc" || str_ext == "docx" || str_ext == "jpg" || str_ext == "jpeg" || str_ext == "pdf")
                    {
                        #region Switch
                        switch (imgType)
                        {
                            case 1:
                                obj_selfComplianceAttachment.AttachmentCode = obj_selfComplianceBLL.NOCAttCode;
                                break;
                            case 2:
                                obj_selfComplianceAttachment.AttachmentCode = obj_selfComplianceBLL.CopySiteInspectionAttCode;
                                break;
                            case 3:
                                obj_selfComplianceAttachment.AttachmentCode = obj_selfComplianceBLL.GroundwaterAbstractionDataAttCode;
                                break;
                            case 4:
                                obj_selfComplianceAttachment.AttachmentCode = obj_selfComplianceBLL.WellFittedWithWaterFlowMeterAttCode;
                                break;
                            case 5:
                                obj_selfComplianceAttachment.AttachmentCode = obj_selfComplianceBLL.AnnualCalibrationAttCode;
                                break;
                            case 6:
                                obj_selfComplianceAttachment.AttachmentCode = obj_selfComplianceBLL.GroundWaterQualityAttCode;
                                break;
                            case 7:
                                obj_selfComplianceAttachment.AttachmentCode = obj_selfComplianceBLL.MiningSeepageAttCode;
                                break;
                            case 8:
                                obj_selfComplianceAttachment.AttachmentCode = obj_selfComplianceBLL.GeoRainWaterHarvRechAttCode;
                                break;
                            case 9:
                                obj_selfComplianceAttachment.AttachmentCode = obj_selfComplianceBLL.GroundwaterMonitoringAttCode;
                                break;
                            case 10:
                                obj_selfComplianceAttachment.AttachmentCode = obj_selfComplianceBLL.GroundWaterMonitoringDataAttCode;
                                break;
                            case 11:
                                obj_selfComplianceAttachment.AttachmentCode = obj_selfComplianceBLL.PhotoETPSTPAttCode;
                                break;
                            case 12:
                                obj_selfComplianceAttachment.AttachmentCode = obj_selfComplianceBLL.ExtraAttCode;
                                break;
                            case 13:
                                obj_selfComplianceAttachment.AttachmentCode = obj_selfComplianceBLL.WaterAuditAttCode;
                                break;
                            case 14:
                                obj_selfComplianceAttachment.AttachmentCode = obj_selfComplianceBLL.IARModelingAttCode;
                                break;
                        }
                        #endregion
                    }
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Attachments Format is Not Correct");
                    obj_selfComplianceAttachment.AttachmentName = Convert.ToString(item.attachmentName);
                    obj_selfComplianceAttachment.AttachmentPath = item.uploadPath;
                    obj_selfComplianceAttachment.CreatedByExUC = userCode;
                    obj_selfComplianceAttachment.AttachmentFile = item.file_type;
                    obj_selfComplianceAttachment.FileExtension = Convert.ToString(item.fileExt);
                    obj_selfComplianceAttachment.ContentType = Convert.ToString(item.contentType);
                    int int_result = obj_selfComplianceAttachment.Add();
                    if (int_result == 1)
                        return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Record Add Successfully");
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Something Went Wrong");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Something Went Wrong");
            }
        }
        catch (WebException ex)
        {
            return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
        }

        return messge;
    }

    ///////////////////  ENd  ///////////////////////


    #endregion
























    [HttpPost]
    public HttpResponseMessage AddAppCompDetails([FromBody] MobSelfCompliance objA_MobSelfCompliance)
    {

        if (objA_MobSelfCompliance.ApplicationCode == 0)
            return Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, "ApplicationCode should not be 0");
        else if (objA_MobSelfCompliance.ApplicationCode != 0 && objA_MobSelfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes)
            return Request.CreateErrorResponse(System.Net.HttpStatusCode.Created, "Already Exists");
        else
        {
            NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_industrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter(objA_MobSelfCompliance.ApplicationCode);
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(objA_MobSelfCompliance.ApplicationCode);
            int status = 0;
            if (Convert.ToDateTime(obj_industrialNewIssusedLetter.ValidityStartDate).AddMonths(11) < DateTime.Now && Convert.ToDateTime(obj_industrialNewIssusedLetter.ValidityEndDate) > DateTime.Now && obj_IndustrialNewApplication.LatestApplicationStatusCode == Convert.ToInt32(NOCAP.BLL.Utility.Utility.LatestApplicationStatusCodeOption.Approved))
            {
                NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(objA_MobSelfCompliance.ApplicationCode);
                if (obj_selfCompliance.ApplicationCode == 0)
                {
                    status = AddData(objA_MobSelfCompliance);
                    if (status == 1)
                        return Request.CreateResponse(System.Net.HttpStatusCode.Created, "Submit");
                    else
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotAcceptable, "Not Submit");

                }
                else
                {
                    status = UpdateData(objA_MobSelfCompliance);
                    if (status == 1)
                        return Request.CreateResponse(System.Net.HttpStatusCode.Created, "Update");
                    else
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotAcceptable, objA_MobSelfCompliance.CustumMessage);
                }
            }
            else
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotAcceptable, "Not Update");


        }
    }


    [HttpPost]
    public HttpResponseMessage PostUserImage()
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        try
        {

            var httpRequest = HttpContext.Current.Request;

            foreach (string file in httpRequest.Files)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                var postedFile = httpRequest.Files[file];
                if (postedFile != null && postedFile.ContentLength > 0)
                {

                    int MaxContentLength = 512001; //Size = 500kb  

                    IList<string> AllowedFileExtensions = new List<string> { ".doc", ".docx", ".jpg", ".jpeg", ".pdf" };
                    var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                    var extension = ext.ToLower();
                    if (!AllowedFileExtensions.Contains(extension))
                    {
                        var message = string.Format("Please Upload image of type .doc,.docx,.jpg,.jpeg,.pdf.");
                        dict.Add("error", message);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                    }
                    else if (postedFile.ContentLength > MaxContentLength)
                    {
                        var message = string.Format("Please Upload a file upto 1 mb.");
                        dict.Add("error", message);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                    }
                    else
                    {
                        var filePath = HttpContext.Current.Server.MapPath("~/Userimage/" + postedFile.FileName + extension);
                        postedFile.SaveAs(filePath);

                    }
                }

                var message1 = string.Format("Image Updated Successfully.");
                return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
            }
            var res = string.Format("Please Upload a image.");
            dict.Add("error", res);
            return Request.CreateResponse(HttpStatusCode.NotFound, dict);
        }
        catch (Exception ex)
        {
            var res = string.Format(ex.Message);
            dict.Add("error", res);
            return Request.CreateResponse(HttpStatusCode.NotFound, dict);
        }
    }

    private int AddData(MobSelfCompliance objA_MobSelfCompliance)
    {
        try
        {

            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance();

            if (SubmitData(ref obj_SelfCompliance, objA_MobSelfCompliance) == 0)
                return 0;
            else
            {
                obj_SelfCompliance.ApplicationCode = objA_MobSelfCompliance.ApplicationCode;
                obj_SelfCompliance.CreatedByExUC = objA_MobSelfCompliance.CreatedByExUC;
                if (obj_SelfCompliance.Add() == 1)
                    return 1;
                else
                    return 0;
            }
        }
        catch (Exception ex)
        {

            return 0;
        }
        finally
        {

        }
    }
    private int UpdateData(MobSelfCompliance objA_MobSelfCompliance)
    {
        try
        {
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(objA_MobSelfCompliance.ApplicationCode);

            if (SubmitData(ref obj_SelfCompliance, objA_MobSelfCompliance) == 0)
                return 0;
            else
            {
                obj_SelfCompliance.ModifiedByExUC = objA_MobSelfCompliance.ModifiedByExUC;
                if (obj_SelfCompliance.Update() == 1)
                    return 1;
                else
                    return 0;

            }
        }
        catch (Exception)
        {

            return 0;
        }
        finally
        {

        }
    }

    private int SubmitData(ref NOCAP.BLL.Misc.Compliance.SelfCompliance refobj_SelfCompliance, MobSelfCompliance objA_MobSelfCompliance)
    {
        try
        {

            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(refobj_SelfCompliance.ApplicationCode);

            obj_SelfCompliance.NOCNo = objA_MobSelfCompliance.NOCNo;
            if (!NOCAPExternalUtility.IsValidDate(Convert.ToDateTime(objA_MobSelfCompliance.ValidityStartDate).ToString("dd/MM/yyyy")))
                return 0;
            else
                obj_SelfCompliance.ValidityStartDate = objA_MobSelfCompliance.ValidityStartDate;
            if (!NOCAPExternalUtility.IsValidDate(Convert.ToDateTime(objA_MobSelfCompliance.ValidityEndDate).ToString("dd/MM/yyyy")))
                return 0;
            else
                obj_SelfCompliance.ValidityStartDate = objA_MobSelfCompliance.ValidityEndDate;



            obj_SelfCompliance.ComplianceSubmitDate = DateTime.Now;
            obj_SelfCompliance.GroundWaterAbsDayAppr = objA_MobSelfCompliance.GroundWaterAbsDayAppr;
            if (objA_MobSelfCompliance.GroundWaterDewDayAppr.ToString() != "")
                obj_SelfCompliance.GroundWaterDewDayAppr = objA_MobSelfCompliance.GroundWaterDewDayAppr;
            obj_SelfCompliance.GroundWaterAbsYearAppr = objA_MobSelfCompliance.GroundWaterAbsYearAppr;
            if (objA_MobSelfCompliance.GroundWaterDewYearAppr.ToString() != "")
                obj_SelfCompliance.GroundWaterDewYearAppr = objA_MobSelfCompliance.GroundWaterDewYearAppr;

            refobj_SelfCompliance = obj_SelfCompliance;
            return 1;
        }
        catch (Exception)
        {

            return 0;
        }
    }


    //[HttpPost]
    //public HttpResponseMessage SendOTP([FromBody]MobExUser obj_ExUser)
    //{
    //    MobExUser obj_mobExUser = new MobExUser();

    //    if (obj_ExUser.UserName == "")
    //        return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, "Please enter valid User Name");
    //    else
    //    {
    //        try
    //        {
    //            if (SMSUtility.IsSendSMSEnable())
    //            {
    //                NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(txtUserName.Text);
    //                if (obj_ExternalUser.ExternalUserCode > 0)
    //                {
    //                    string strMobileNo = obj_ExternalUser.ExternalUserMobileNumber;
    //                    string OTPMessage = "";
    //                    strMobileNumberTo = strMobileNo;
    //                    OTPMessage = NOCAPExternalUtility.GetRandomNumber();
    //                    Session["OTP"] = OTPMessage;
    //                    OTPMessage = "NOCAP- One Time Password (OTP) is :" + OTPMessage + "-CGWA";
    //                    if (SMSUtility.sendOTPtoMobile(OTPMessage, strMobileNo, "1007161718802381027", out strSMSUserName).Trim() == "Platform accepted")
    //                    {
    //                        ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('One time Password(OTP) has been Sent to your Mobile No, Enter OTP to Complete Reset Password');", true);
    //                        SaveSMSAlert();
    //                        return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_mobExUser);
    //                    }
    //                    else
    //                    {
    //                        ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Resend One time Password(OTP)');", true);
    //                    }
    //                }

    //            }
    //            else
    //                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, obj_mobExUser);
    //        }
    //        catch (Exception ex)
    //        { return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, obj_mobExUser); }

    //    }
    //}

    //public void SaveSMSAlert()
    //{
    //    try
    //    {
    //        strActionName = "InsertSMSAlert";
    //        NOCAP.BLL.SMSAlert.SMSAlert obj_insertSMSAlert = new NOCAP.BLL.SMSAlert.SMSAlert();
    //        if (lng_AppCode != null)
    //        {
    //            obj_insertSMSAlert.AppCode = Convert.ToInt64(lng_AppCode);
    //        }
    //        else
    //        {
    //            obj_insertSMSAlert.AppCode = lng_AppCode;
    //        }
    //        obj_insertSMSAlert.SMSType = NOCAP.BLL.SMSAlert.SMSAlert.SMSTypeAFR.Forget;
    //        obj_insertSMSAlert.NOCAPApplicationType = NOCAP.BLL.SMSAlert.SMSAlert.NOCAPApplicationTypeEI.NOCAPExternal;
    //        strMessage = "OTP Send Successfully for forget Password.";
    //        obj_insertSMSAlert.AppTypeCode = int_AppTypeCode;

    //        obj_insertSMSAlert.AppPurposeCode = int_AppPurposeCode;
    //        obj_insertSMSAlert.AlertStagesCode = int_AlertStagesCode;
    //        obj_insertSMSAlert.UserCode = int_UserCode;
    //        obj_insertSMSAlert.ExUserCode = lng_ExuserCode;

    //        obj_insertSMSAlert.SMSUseName = strSMSUserName;
    //        if (strMobileNumberTo.Trim() != "")
    //        {
    //            obj_insertSMSAlert.MobileNo = strMobileNumberTo;
    //        }

    //        obj_insertSMSAlert.Message = strMessage.Trim();

    //        obj_insertSMSAlert.CreatedByUserCode = int_CreatedByUC;
    //        obj_insertSMSAlert.CreatedByExUserCode = lng_CreatedByExUC;


    //        if (obj_insertSMSAlert.Add() == 1)
    //        {
    //            strStatus = "SMS Alert Saved  Successfully.";

    //        }
    //        else
    //        {
    //            strStatus = "SMS Alert Not  Saved.";
    //        }
    //    }

    //    catch (Exception)
    //    {
    //        strStatus = "SMS Alert Not  Saved.";

    //    }
    //    finally
    //    {
    //        ActionTrail obj_ExtActionTrail = new ActionTrail();
    //        if (Session["ExternalUserCode"] != null)
    //        {
    //            obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
    //            obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
    //            obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
    //            obj_ExtActionTrail.Status = strStatus;
    //            if (obj_ExtActionTrail != null)
    //                ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
    //        }
    //    }


    //}

    [BasicAuthentication]
    [HttpPost]
    //public HttpResponseMessage login([FromBody] string mobExUser)
    public HttpResponseMessage login2()
    {
        MobExUser obj_mobExUser = new MobExUser();
        string str_userName = Thread.CurrentPrincipal.Identity.Name;
        // string str_userName obj_MobExUser.UserName,        str_password = obj_MobExUser.password;
        NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = null;
        // if (GetUserProfile(str_userName, str_password))
        //{
        obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(str_userName);
        if (obj_ExternalUser != null)
        {
            NOCAP.BLL.Master.State obj_State = new NOCAP.BLL.Master.State(obj_ExternalUser.StateCode);
            NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_ExternalUser.StateCode, obj_ExternalUser.DistrictCode);
            NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_ExternalUser.StateCode, obj_ExternalUser.DistrictCode, (int)obj_ExternalUser.SubDistrictCode);
            obj_mobExUser.UserName = obj_ExternalUser.ExternalUserName;
            obj_mobExUser.MobileNumber = obj_ExternalUser.ExternalUserMobileNumber;
            obj_mobExUser.AddressLine1 = obj_ExternalUser.AddressLine1;
            obj_mobExUser.AddressLine2 = obj_ExternalUser.AddressLine2;
            obj_mobExUser.AddressLine3 = obj_ExternalUser.AddressLine3;
            obj_mobExUser.StateName = obj_State.StateName;
            obj_mobExUser.DistrictName = obj_District.DistrictName;
            obj_mobExUser.SubDistrictName = obj_SubDistrict.SubDistrictName;
            obj_mobExUser.PinCode = obj_ExternalUser.PinCode;
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, obj_mobExUser);
        }
        else
            return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, obj_mobExUser);
        //}
        // else
        // return obj_mobExUser;
    }







}
