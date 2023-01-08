using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Relaxation
{
    class RelaxationApplicationDAL
    {

        #region Main Function
      
      
     
        public int AddRelaxationApplication(Relaxation.RelaxationApplication objAr_RelaxationApplicationBLL)
        {

            SqlConnection connn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            int int_connOpenOrNot = 0;
            //int_status 0- for unsucessfull login, 1- for unsucessfull login
            int int_status = 0;
            SqlCommand cmd_addRelaxationApplication = new SqlCommand("spAddRelaxationApplication", connn);
            SqlParameter par_addRelaxationApplication = new SqlParameter();
            try
            {
                connn.Open();
                int_connOpenOrNot = 1;
                int_status = 0;
                cmd_addRelaxationApplication.CommandType = CommandType.StoredProcedure;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.Add("@bintINDAppCode", SqlDbType.BigInt);
                par_addRelaxationApplication.Direction = ParameterDirection.Output;
                //par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intApplySubDistrictAreaCatKey", objAr_RelaxationApplicationBLL.ApplySubDistrictAreaCategoryKey);
                //par_addRelaxationApplication.Direction = ParameterDirection.Input;

                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strUIDNumber", objAr_RelaxationApplicationBLL.UIDNumber);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;

                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intWaterQualityCode", objAr_RelaxationApplicationBLL.WaterQualityCode);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;


                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intAppTypeCode", objAr_RelaxationApplicationBLL.ApplicationTypeCode);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intAppPurposeCode", objAr_RelaxationApplicationBLL.ApplicationPurposeCode);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intAppTypeCatCode", objAr_RelaxationApplicationBLL.ApplicationTypeCategoryCode);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strINDName", objAr_RelaxationApplicationBLL.NameOfIndustry);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strProLocAddressLine1", objAr_RelaxationApplicationBLL.AddressLine1);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strProLocAddressLine2", objAr_RelaxationApplicationBLL.AddressLine2);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strProLocAddressLine3", objAr_RelaxationApplicationBLL.AddressLine3);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intProLocStateCode", objAr_RelaxationApplicationBLL.StateCode);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intProLocDistrictCode", objAr_RelaxationApplicationBLL.DistrictCode);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intProLocSubDistrictCode", objAr_RelaxationApplicationBLL.SubDistrictCode);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                switch (objAr_RelaxationApplicationBLL.VillageOrTown)
                {
                    case RelaxationApplication.VillageOrTownOption.Village:
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chProLocVillOrTown", "V");
                        break;
                    case RelaxationApplication.VillageOrTownOption.Town:
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chProLocVillOrTown", "T");
                        break;
                }
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intProLocTownCode", objAr_RelaxationApplicationBLL.TownCode);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intProLocVillageCode", objAr_RelaxationApplicationBLL.VillageCode);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                if (objAr_RelaxationApplicationBLL.PinCode == null)
                {
                    par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intProLocPinCode", System.DBNull.Value);

                }
                else
                {
                    par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intProLocPinCode", objAr_RelaxationApplicationBLL.PinCode);

                }
                par_addRelaxationApplication.Direction = ParameterDirection.Input;

                //par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strProUdaMcGp", objAr_RelaxationApplicationBLL.NameOfUDAAndMCAndGP);
                //par_addRelaxationApplication.Direction = ParameterDirection.Input;

                //if (objAr_RelaxationApplicationBLL.ProposedLocation.ProposedLatitude == null)
                //{
                //    par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@decProLat", System.DBNull.Value);
                //}
                //else
                //{
                //    par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@decProLat", objAr_RelaxationApplicationBLL.ProposedLocation.ProposedLatitude);
                //}
                //par_addRelaxationApplication.Direction = ParameterDirection.Input;

                //if (objAr_RelaxationApplicationBLL.ProposedLocation.ProposedLongitude == null)
                //{
                //    par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@decProLong", System.DBNull.Value);
                //}
                //else
                //{
                //    par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@decProLong", objAr_RelaxationApplicationBLL.ProposedLocation.ProposedLongitude);
                //}
                //par_addRelaxationApplication.Direction = ParameterDirection.Input;


                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strCommAddressLine1", objAr_RelaxationApplicationBLL.CommunicationAddress.AddressLine1);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strCommAddressLine2", objAr_RelaxationApplicationBLL.CommunicationAddress.AddressLine2);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strCommAddressLine3", objAr_RelaxationApplicationBLL.CommunicationAddress.AddressLine3);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;

                if (objAr_RelaxationApplicationBLL.CommunicationAddress.StateCode == 0)
                {
                    par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intCommStateCode", System.DBNull.Value);

                }
                else
                {
                    par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intCommStateCode", objAr_RelaxationApplicationBLL.CommunicationAddress.StateCode);

                }
                par_addRelaxationApplication.Direction = ParameterDirection.Input;

                if (objAr_RelaxationApplicationBLL.CommunicationAddress.DistrictCode == 0)
                {
                    par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intCommDistrictCode", System.DBNull.Value);

                }
                else
                {
                    par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intCommDistrictCode", objAr_RelaxationApplicationBLL.CommunicationAddress.DistrictCode);

                }
                par_addRelaxationApplication.Direction = ParameterDirection.Input;

                if (objAr_RelaxationApplicationBLL.CommunicationAddress.SubDistrictCode == 0)
                {
                    par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intCommSubDistrictCode", System.DBNull.Value);

                }
                else
                {
                    par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intCommSubDistrictCode", objAr_RelaxationApplicationBLL.CommunicationAddress.SubDistrictCode);

                }
                par_addRelaxationApplication.Direction = ParameterDirection.Input;






                //par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intCommStateCode", objAr_RelaxationApplicationBLL.CommunicationAddress.StateCode);
                //par_addRelaxationApplication.Direction = ParameterDirection.Input;
                //par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intCommDistrictCode", objAr_RelaxationApplicationBLL.CommunicationAddress.DistrictCode);
                //par_addRelaxationApplication.Direction = ParameterDirection.Input;
                //par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intCommSubDistrictCode", objAr_RelaxationApplicationBLL.CommunicationAddress.SubDistrictCode);
                //par_addRelaxationApplication.Direction = ParameterDirection.Input;
                if (objAr_RelaxationApplicationBLL.CommunicationAddress.PinCode == null)
                {
                    par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intCommPinCode", System.DBNull.Value);

                }
                else
                {
                    par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intCommPinCode", objAr_RelaxationApplicationBLL.CommunicationAddress.PinCode);

                }
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strCommPhoneNumberISD", objAr_RelaxationApplicationBLL.CommunicationPhoneNumber.PhoneNumberISD);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strCommPhoneNumberSTD", objAr_RelaxationApplicationBLL.CommunicationPhoneNumber.PhoneNumberSTD);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strCommPhoneNumber", objAr_RelaxationApplicationBLL.CommunicationPhoneNumber.PhoneNumberRest);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strCommMobileNumberISD", objAr_RelaxationApplicationBLL.CommunicationMobileNumber.MobileNumberISD);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strCommMobileNumber", objAr_RelaxationApplicationBLL.CommunicationMobileNumber.MobileNumberRest);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strCommEmailID", objAr_RelaxationApplicationBLL.CommunicationEmailID);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strCommFaxNumberISD", objAr_RelaxationApplicationBLL.CommunicationFaxNumber.FaxNumberISD);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strCommFaxNumberSTD", objAr_RelaxationApplicationBLL.CommunicationFaxNumber.FaxNumberSTD);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strCommFaxNumber", objAr_RelaxationApplicationBLL.CommunicationFaxNumber.FaxNumberRest);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;



                //par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strSalientFeatureOfIND", objAr_RelaxationApplicationBLL.SalientFeatureOfIndustrialActivity);
                //par_addRelaxationApplication.Direction = ParameterDirection.Input;


                

                //par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strDrainageArea", objAr_RelaxationApplicationBLL.DrainageInTheArea);
                //par_addRelaxationApplication.Direction = ParameterDirection.Input;
                //par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strSourceOfAvailOfSWForIND", objAr_RelaxationApplicationBLL.SourceOfAvaillabilityOfSurfaceWaterForIndustrialUse.SourceOfAvalabilityOfSurfaceWaterDesc);
                //par_addRelaxationApplication.Direction = ParameterDirection.Input;

                //if (objAr_RelaxationApplicationBLL.AverageAnnualRainfallInTheArea == null)
                //{
                //    par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@decAvgAnnualRainfall", System.DBNull.Value);
                //}
                //else
                //{
                //    par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@decAvgAnnualRainfall", objAr_RelaxationApplicationBLL.AverageAnnualRainfallInTheArea);
                //}
                //par_addRelaxationApplication.Direction = ParameterDirection.Input;
                //par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@strTowVillWithIn2KM", objAr_RelaxationApplicationBLL.TownshipVillageWithin2KM);
                //par_addRelaxationApplication.Direction = ParameterDirection.Input;

                switch (objAr_RelaxationApplicationBLL.GroundWaterUtilizationFor)
                {
                    case Relaxation.RelaxationApplication.GroundWaterUtilizationForOption.NotDefined:
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chGWUtiForNewIND", "");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chGWUtiForExistIND", "");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chGWUtiForExpExistIND", "");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                    case Relaxation.RelaxationApplication.GroundWaterUtilizationForOption.NewIndustry:
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chGWUtiForNewIND", "Y");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chGWUtiForExistIND", "");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chGWUtiForExpExistIND", "");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;

                        break;

                    //new code
                    case Relaxation.RelaxationApplication.GroundWaterUtilizationForOption.ExistingIndustry:
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chGWUtiForNewIND", "");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chGWUtiForExistIND", "Y");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chGWUtiForExpExistIND", "");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;

                        break;
                    //
                    case Relaxation.RelaxationApplication.GroundWaterUtilizationForOption.ExpansionProgramOfExitingIndustry:
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chGWUtiForNewIND", "");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chGWUtiForExistIND", "");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chGWUtiForExpExistIND", "Y");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                }

                switch (objAr_RelaxationApplicationBLL.NOCObtainForExistIND)
                {
                    case Relaxation.RelaxationApplication.NOCObtainForExistINDOption.Yes:
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chNOCObtainForExistIND", "Y");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                    case Relaxation.RelaxationApplication.NOCObtainForExistINDOption.No:
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chNOCObtainForExistIND", "N");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                    default:
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chNOCObtainForExistIND", "");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                }

                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@dtDateOfCommExistIND", objAr_RelaxationApplicationBLL.DateOfCommencement);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@dtDateOfCommExpanIND", objAr_RelaxationApplicationBLL.DateOfExpansionOfProject);

                par_addRelaxationApplication.Direction = ParameterDirection.Input;


                //par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intWaterChargeTypeCodeFinally", objAr_RelaxationApplicationBLL.WaterChargeTypeCodeFinally);
                //par_addRelaxationApplication.Direction = ParameterDirection.Input;
                //par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@decGWChargeAmtFinally", objAr_RelaxationApplicationBLL.GWChargeAmtFinally);
                //par_addRelaxationApplication.Precision = 18;
                //par_addRelaxationApplication.Scale = 2;
                //par_addRelaxationApplication.Direction = ParameterDirection.Input;
                //par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@decGWArearAmtFinally", objAr_RelaxationApplicationBLL.GWArearAmtFinally);
                //par_addRelaxationApplication.Precision = 18;
                //par_addRelaxationApplication.Scale = 2;
                //par_addRelaxationApplication.Direction = ParameterDirection.Input;



                switch (objAr_RelaxationApplicationBLL.MSME)
                {
                    case Relaxation.RelaxationApplication.MSMEYesNo.Yes:
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chMSME", "Y");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                    case Relaxation.RelaxationApplication.MSMEYesNo.No:
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chMSME", "N");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                    default:
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chMSME", "");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                }

                switch (objAr_RelaxationApplicationBLL.WetLandArea)
                {
                    case Relaxation.RelaxationApplication.WetLandAreaYesNo.Yes:
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chWetlandArea", "Y");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                    case Relaxation.RelaxationApplication.WetLandAreaYesNo.No:
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chWetlandArea", "N");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                    default:
                        par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@chWetlandArea", "");
                        par_addRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                }


                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intMSMETypeCode", objAr_RelaxationApplicationBLL.MSMETypeCode);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;



                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@intCreatedByUC", objAr_RelaxationApplicationBLL.CreatedByUC);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;

                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.AddWithValue("@bintCreatedByExUC", objAr_RelaxationApplicationBLL.CreatedByExUC);
                par_addRelaxationApplication.Direction = ParameterDirection.Input;

                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.Add("@strCustMsg", SqlDbType.VarChar, 50);
                par_addRelaxationApplication.Direction = ParameterDirection.Output;
                par_addRelaxationApplication = cmd_addRelaxationApplication.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
                par_addRelaxationApplication.Direction = ParameterDirection.ReturnValue;
                cmd_addRelaxationApplication.ExecuteNonQuery();
                connn.Close();
                objAr_RelaxationApplicationBLL.CustumMessage = cmd_addRelaxationApplication.Parameters["@strCustMsg"].Value.ToString();
                objAr_RelaxationApplicationBLL.ApplicationCode = Convert.ToInt64(cmd_addRelaxationApplication.Parameters["@bintINDAppCode"].Value);

                int_status = Convert.ToInt32(cmd_addRelaxationApplication.Parameters["RETURN_VALUE"].Value.ToString());

                return int_status;
            }
            catch (Exception ex)
            {
                objAr_RelaxationApplicationBLL.CustumMessage = "Industrial New Application Problem in DAL" + ex.Message;
                if (int_connOpenOrNot == 1)
                {
                    connn.Close();
                }
                return int_status;
            }
            finally
            {
                if (cmd_addRelaxationApplication != null)
                {
                    cmd_addRelaxationApplication.Dispose();
                }
                if (connn != null)
                {
                    connn.Dispose();
                }

            }
        }
           
        public int UpdateRelaxationApplication(Relaxation.RelaxationApplication objAr_RelaxationApplicationBLL)
        {


            SqlConnection connn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            int int_connOpenOrNot = 0;
            int int_status = 0;
            SqlCommand cmd_updateRelaxationApplication = new SqlCommand("spUpdateRelaxationApplication", connn);
            SqlParameter par_updateRelaxationApplication = new SqlParameter();
            try
            {
                connn.Open();
                int_connOpenOrNot = 1;
                int_status = 0;
                cmd_updateRelaxationApplication.CommandType = CommandType.StoredProcedure;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@bintINDAppCode", objAr_RelaxationApplicationBLL.ApplicationCode);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strUIDNumber", objAr_RelaxationApplicationBLL.UIDNumber);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;

                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intWaterQualityCode", objAr_RelaxationApplicationBLL.WaterQualityCode);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;

                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intAppTypeCode", objAr_RelaxationApplicationBLL.ApplicationTypeCode);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;

                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intAppTypeCatCode", objAr_RelaxationApplicationBLL.ApplicationTypeCategoryCode);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strINDName", objAr_RelaxationApplicationBLL.NameOfIndustry);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strProLocAddressLine1", objAr_RelaxationApplicationBLL.AddressLine1);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strProLocAddressLine2", objAr_RelaxationApplicationBLL.AddressLine2);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strProLocAddressLine3", objAr_RelaxationApplicationBLL.AddressLine3);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intProLocStateCode", objAr_RelaxationApplicationBLL.StateCode);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intProLocDistrictCode", objAr_RelaxationApplicationBLL.DistrictCode);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intProLocSubDistrictCode", objAr_RelaxationApplicationBLL.SubDistrictCode);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                switch (objAr_RelaxationApplicationBLL.VillageOrTown)
                {
                    case RelaxationApplication.VillageOrTownOption.Village:
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chProLocVillOrTown", "V");
                        break;
                    case RelaxationApplication.VillageOrTownOption.Town:
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chProLocVillOrTown", "T");
                        break;
                }
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intProLocTownCode", objAr_RelaxationApplicationBLL.TownCode);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intProLocVillageCode", objAr_RelaxationApplicationBLL.VillageCode);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                if (objAr_RelaxationApplicationBLL.PinCode == null)
                {
                    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intProLocPinCode", System.DBNull.Value);
                }
                else
                {
                    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intProLocPinCode", objAr_RelaxationApplicationBLL.PinCode);
                }
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;

                //par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strProUdaMcGp", objAr_RelaxationApplicationBLL.NameOfUDAAndMCAndGP);
                //par_updateRelaxationApplication.Direction = ParameterDirection.Input;

                //if (objAr_RelaxationApplicationBLL.ProposedLocation.ProposedLatitude == null)
                //{
                //    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@decProLat", System.DBNull.Value);
                //}
                //else
                //{
                //    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@decProLat", objAr_RelaxationApplicationBLL.ProposedLocation.ProposedLatitude);
                //}
                //par_updateRelaxationApplication.Direction = ParameterDirection.Input;


                //if (objAr_RelaxationApplicationBLL.ProposedLocation.ProposedLongitude == null)
                //{
                //    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@decProLong", System.DBNull.Value);
                //}
                //else
                //{
                //    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@decProLong", objAr_RelaxationApplicationBLL.ProposedLocation.ProposedLongitude);
                //}
                //par_updateRelaxationApplication.Direction = ParameterDirection.Input;

                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strCommAddressLine1", objAr_RelaxationApplicationBLL.CommunicationAddress.AddressLine1);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strCommAddressLine2", objAr_RelaxationApplicationBLL.CommunicationAddress.AddressLine2);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strCommAddressLine3", objAr_RelaxationApplicationBLL.CommunicationAddress.AddressLine3);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;

                if (objAr_RelaxationApplicationBLL.CommunicationAddress.StateCode == 0)
                {
                    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intCommStateCode", System.DBNull.Value);

                }
                else
                {
                    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intCommStateCode", objAr_RelaxationApplicationBLL.CommunicationAddress.StateCode);

                }

                par_updateRelaxationApplication.Direction = ParameterDirection.Input;

                if (objAr_RelaxationApplicationBLL.CommunicationAddress.DistrictCode == 0)
                {
                    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intCommDistrictCode", System.DBNull.Value);

                }
                else
                {
                    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intCommDistrictCode", objAr_RelaxationApplicationBLL.CommunicationAddress.DistrictCode);

                }


                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                if (objAr_RelaxationApplicationBLL.CommunicationAddress.SubDistrictCode == 0)
                {
                    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intCommSubDistrictCode", System.DBNull.Value);

                }
                else
                {
                    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intCommSubDistrictCode", objAr_RelaxationApplicationBLL.CommunicationAddress.SubDistrictCode);

                }
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;


                switch (objAr_RelaxationApplicationBLL.Submitted)
                {
                    case  RelaxationApplication.SubmittedOption.Yes:
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chSubmitted", "Y");
                        break;
                    case RelaxationApplication.SubmittedOption.No:
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chSubmitted", "N");
                        break;
                    case RelaxationApplication.SubmittedOption.NotDefine:
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chSubmitted", "N");
                        break;
                }
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;


                //par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intWaterChargeTypeCodeFinally", objAr_RelaxationApplicationBLL.WaterChargeTypeCodeFinally);
                //par_updateRelaxationApplication.Direction = ParameterDirection.Input;

                //par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@decGWChargeAmtFinally", objAr_RelaxationApplicationBLL.GWChargeAmtFinally);
                //par_updateRelaxationApplication.Precision = 18;
                //par_updateRelaxationApplication.Scale = 2;
                //par_updateRelaxationApplication.Direction = ParameterDirection.Input;

                //par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@decGWChargeAmtFinally", objAr_RelaxationApplicationBLL.GWChargeAmtFinally);
                //par_updateRelaxationApplication.Precision = 18;
                //par_updateRelaxationApplication.Scale = 2;
                //par_updateRelaxationApplication.Direction = ParameterDirection.Input;





                //par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intCommStateCode", objAr_RelaxationApplicationBLL.CommunicationAddress.StateCode);
                //par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                //par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intCommDistrictCode", objAr_RelaxationApplicationBLL.CommunicationAddress.DistrictCode);
                //par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                //par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intCommSubDistrictCode", objAr_RelaxationApplicationBLL.CommunicationAddress.SubDistrictCode);
                //par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                if (objAr_RelaxationApplicationBLL.CommunicationAddress.PinCode == null)
                {
                    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intCommPinCode", System.DBNull.Value);

                }
                else
                {
                    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intCommPinCode", objAr_RelaxationApplicationBLL.CommunicationAddress.PinCode);

                }
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strCommPhoneNumberISD", objAr_RelaxationApplicationBLL.CommunicationPhoneNumber.PhoneNumberISD);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strCommPhoneNumberSTD", objAr_RelaxationApplicationBLL.CommunicationPhoneNumber.PhoneNumberSTD);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strCommPhoneNumber", objAr_RelaxationApplicationBLL.CommunicationPhoneNumber.PhoneNumberRest);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strCommMobileNumberISD", objAr_RelaxationApplicationBLL.CommunicationMobileNumber.MobileNumberISD);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strCommMobileNumber", objAr_RelaxationApplicationBLL.CommunicationMobileNumber.MobileNumberRest);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strCommEmailID", objAr_RelaxationApplicationBLL.CommunicationEmailID);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strCommFaxNumberISD", objAr_RelaxationApplicationBLL.CommunicationFaxNumber.FaxNumberISD);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strCommFaxNumberSTD", objAr_RelaxationApplicationBLL.CommunicationFaxNumber.FaxNumberSTD);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strCommFaxNumber", objAr_RelaxationApplicationBLL.CommunicationFaxNumber.FaxNumberRest);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;


                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@bintBhaTransRefNo", objAr_RelaxationApplicationBLL.BharatTransReferanceNumber);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;


                if (objAr_RelaxationApplicationBLL.BharatTransDated == null)
                {
                    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@dtBhaTransDated", System.DBNull.Value);
                }
                else
                {
                    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@dtBhaTransDated", objAr_RelaxationApplicationBLL.BharatTransDated);
                }
                //par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@dtBhaTransDated", objAr_RelaxationApplicationBLL.BhaTransDated);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;



                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@decPayMentAmount", objAr_RelaxationApplicationBLL.PayMentAmount);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;

                //par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strSalientFeatureOfIND", objAr_RelaxationApplicationBLL.SalientFeatureOfIndustrialActivity);
                //par_updateRelaxationApplication.Direction = ParameterDirection.Input;


                //if (objAr_RelaxationApplicationBLL.LandUseDetailOfExistingProposed.IndustrialNewLandUseList != null)
                //{
                //    StringBuilder sbLandUse = new StringBuilder();
                //    Relaxation.IndustrialNewLandUse[] arr_IndustrialNewLandUse;

                //    arr_IndustrialNewLandUse = objAr_RelaxationApplicationBLL.LandUseDetailOfExistingProposed.IndustrialNewLandUseList;

                //    sbLandUse.AppendLine("<?xml version=\"1.0\" ?>");
                //    sbLandUse.AppendLine("<LandUseDetails>");

                //    foreach (Relaxation.IndustrialNewLandUse obj_IndustrialNewLandUse in arr_IndustrialNewLandUse)
                //    {
                //        sbLandUse.AppendLine("<LandUseDetail>");

                //        //sbLandUse.AppendLine("<INDAppCode>" + Convert.ToString(obj_IndustrialNewLandUse.ApplicationCode) + "</INDAppCode>");
                //        sbLandUse.AppendLine("<LandUseTypeCode>" + Convert.ToString(obj_IndustrialNewLandUse.LandUseTypeCode) + "</LandUseTypeCode>");

                //        if (obj_IndustrialNewLandUse.LandUseExist != null)
                //        {
                //            sbLandUse.AppendLine("<INDLandUseTypeExist>" + Convert.ToString(obj_IndustrialNewLandUse.LandUseExist) + "</INDLandUseTypeExist>");
                //        }
                //        else
                //        {
                //            sbLandUse.AppendLine("<INDLandUseTypeExist>" + "" + "</INDLandUseTypeExist>");
                //        }

                //        if (obj_IndustrialNewLandUse.LandUseProposed != null)
                //        {
                //            sbLandUse.AppendLine("<INDLandUseTypePro>" + Convert.ToString(obj_IndustrialNewLandUse.LandUseProposed) + "</INDLandUseTypePro>");
                //        }
                //        else
                //        {
                //            sbLandUse.AppendLine("<INDLandUseTypePro>" + "" + "</INDLandUseTypePro>");

                //        }

                //        sbLandUse.AppendLine("</LandUseDetail>");
                //    }

                //    sbLandUse.AppendLine("</LandUseDetails>");
                //    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@XMLLandUse", sbLandUse.ToString());
                //    par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                //}

                //par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strDrainageArea", objAr_RelaxationApplicationBLL.DrainageInTheArea);
                //par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                //par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strSourceOfAvailOfSWForIND", objAr_RelaxationApplicationBLL.SourceOfAvaillabilityOfSurfaceWaterForIndustrialUse.SourceOfAvalabilityOfSurfaceWaterDesc);
                //par_updateRelaxationApplication.Direction = ParameterDirection.Input;


                //if (objAr_RelaxationApplicationBLL.AverageAnnualRainfallInTheArea == null)
                //{
                //    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@decAvgAnnualRainfall", System.DBNull.Value);
                //}
                //else
                //{
                //    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@decAvgAnnualRainfall", objAr_RelaxationApplicationBLL.AverageAnnualRainfallInTheArea);
                //}
                //par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                //par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@strTowVillWithIn2KM", objAr_RelaxationApplicationBLL.TownshipVillageWithin2KM);
                //par_updateRelaxationApplication.Direction = ParameterDirection.Input;

                switch (objAr_RelaxationApplicationBLL.GroundWaterUtilizationFor)
                {
                    case Relaxation.RelaxationApplication.GroundWaterUtilizationForOption.NotDefined:
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chGWUtiForNewIND", "");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chGWUtiForExistIND", "");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chGWUtiForExpExistIND", "");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                    case Relaxation.RelaxationApplication.GroundWaterUtilizationForOption.NewIndustry:
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chGWUtiForNewIND", "Y");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chGWUtiForExistIND", "");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chGWUtiForExpExistIND", "");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;

                        break;

                    //new code

                    case Relaxation.RelaxationApplication.GroundWaterUtilizationForOption.ExistingIndustry:

                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chGWUtiForNewIND", "");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chGWUtiForExistIND", "Y");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chGWUtiForExpExistIND", "");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;

                        break;
                    //


                    case Relaxation.RelaxationApplication.GroundWaterUtilizationForOption.ExpansionProgramOfExitingIndustry:
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chGWUtiForNewIND", "");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chGWUtiForExistIND", "");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chGWUtiForExpExistIND", "Y");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                }

                switch (objAr_RelaxationApplicationBLL.NOCObtainForExistIND)
                {
                    case Relaxation.RelaxationApplication.NOCObtainForExistINDOption.Yes:
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chNOCObtainForExistIND", "Y");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                    case Relaxation.RelaxationApplication.NOCObtainForExistINDOption.No:
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chNOCObtainForExistIND", "N");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                    default:
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chNOCObtainForExistIND", "");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                }


                if (objAr_RelaxationApplicationBLL.DateOfCommencement == null)
                {
                    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@dtDateOfCommExistIND", System.DBNull.Value);
                }
                else
                {
                    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@dtDateOfCommExistIND", objAr_RelaxationApplicationBLL.DateOfCommencement);
                }

                //par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@dtDateOfCommExistIND", objAr_RelaxationApplicationBLL.DateOfCommencement);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;

                if (objAr_RelaxationApplicationBLL.DateOfExpansionOfProject == null)
                {
                    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@dtDateOfCommExpanIND", System.DBNull.Value);
                }
                else
                {
                    par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@dtDateOfCommExpanIND", objAr_RelaxationApplicationBLL.DateOfExpansionOfProject);
                }

               // par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@dtDateOfCommExpanIND", objAr_RelaxationApplicationBLL.DateOfExpansionOfProject);

                par_updateRelaxationApplication.Direction = ParameterDirection.Input;



                switch (objAr_RelaxationApplicationBLL.MSME)
                {
                    case Relaxation.RelaxationApplication.MSMEYesNo.Yes:
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chMSME", "Y");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                    case Relaxation.RelaxationApplication.MSMEYesNo.No:
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chMSME", "N");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                    default:
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chMSME", "");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                }

                switch (objAr_RelaxationApplicationBLL.WetLandArea)
                {
                    case Relaxation.RelaxationApplication.WetLandAreaYesNo.Yes:
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chWetlandArea", "Y");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                    case Relaxation.RelaxationApplication.WetLandAreaYesNo.No:
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chWetlandArea", "N");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                    default:
                        par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@chWetlandArea", "");
                        par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                        break;
                }

                  
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intMSMETypeCode", objAr_RelaxationApplicationBLL.MSMETypeCode);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;


                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@intModifiedByUC", objAr_RelaxationApplicationBLL.ModifiedByUC);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;

                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.AddWithValue("@bintModifiedByExUC", objAr_RelaxationApplicationBLL.ModifiedByExUC);
                par_updateRelaxationApplication.Direction = ParameterDirection.Input;
                 

                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.Add("@strCustMsg", SqlDbType.VarChar, 50);
                par_updateRelaxationApplication.Direction = ParameterDirection.Output;
                par_updateRelaxationApplication = cmd_updateRelaxationApplication.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
                par_updateRelaxationApplication.Direction = ParameterDirection.ReturnValue;
                cmd_updateRelaxationApplication.ExecuteNonQuery();
                connn.Close();
                objAr_RelaxationApplicationBLL.CustumMessage = cmd_updateRelaxationApplication.Parameters["@strCustMsg"].Value.ToString();
                int_status = Convert.ToInt32(cmd_updateRelaxationApplication.Parameters["RETURN_Value"].Value.ToString());
                return int_status;
            }
            catch (Exception ex)
            {
                objAr_RelaxationApplicationBLL.CustumMessage = "Application Problem in DAL" + ex.Message;
                if (int_connOpenOrNot == 1)
                {
                    connn.Close();
                }
                return int_status;
            }
            finally
            {
                if (cmd_updateRelaxationApplication != null)
                {
                    cmd_updateRelaxationApplication.Dispose();
                }
                if (connn != null)
                {
                    connn.Dispose();
                }

            }

        }

        public int PopulateRelaxationApplicationForApplicationCode(RelaxationApplication objAr_RelaxationApplicationBLL)
        {
            SqlConnection connn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            int int_connOpenOrNot = 0;
            int int_status = 0;
            SqlCommand cmd_populateRelaxationApplicationForApplicationCode = new SqlCommand("spPopulateRelaxationApplicationForApplicationCode", connn);
            SqlParameter par_populateRelaxationApplicationForApplicationCode = new SqlParameter();
            try
            {
                connn.Open();
                int_connOpenOrNot = 1;
                int_status = 0;
                cmd_populateRelaxationApplicationForApplicationCode.CommandType = CommandType.StoredProcedure;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.AddWithValue("@bintINDAppCode", objAr_RelaxationApplicationBLL.ApplicationCode);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.InputOutput;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@bintProFeeOrderPaymentCode", SqlDbType.BigInt);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@bintGWChargeOrderPaymentCode", SqlDbType.BigInt);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@bintPenaltyOrderPaymentCode", SqlDbType.BigInt);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intApplicantUserCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intApplicantExUserCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strUIDNumber", SqlDbType.NVarChar,200);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intAppTypeCode", SqlDbType.Int);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intWaterQualityCode", SqlDbType.Int);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intAppPurposeCode", SqlDbType.Int);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intAppTypeCatCode", SqlDbType.Int);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strINDName", SqlDbType.NVarChar, 100);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strProLocAddressLine1", SqlDbType.NVarChar, 100);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strProLocAddressLine2", SqlDbType.NVarChar, 100);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strProLocAddressLine3", SqlDbType.NVarChar, 100);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intProLocStateCode", SqlDbType.Int);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intProLocDistrictCode", SqlDbType.Int);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intProLocSubDistrictCode", SqlDbType.Int);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@chProLocVillOrTown", SqlDbType.Char, 1);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intProLocTownCode", SqlDbType.Int);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intProLocVillageCode", SqlDbType.Int);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intProLocPinCode", SqlDbType.Int);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;


                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intProLocSitePlanAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intProLocCertReveSketAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strProUdaMcGp", SqlDbType.NVarChar, 100);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decProLat", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 10;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 6;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decProLong", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 10;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 6;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strCommAddressLine1", SqlDbType.NVarChar, 100);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strCommAddressLine2", SqlDbType.NVarChar, 100);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strCommAddressLine3", SqlDbType.NVarChar, 100);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intCommStateCode", SqlDbType.Int);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intCommDistrictCode", SqlDbType.Int);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intCommSubDistrictCode", SqlDbType.Int);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intCommPinCode", SqlDbType.Int);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strCommPhoneNumberISD", SqlDbType.NVarChar, 4);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strCommPhoneNumberSTD", SqlDbType.NVarChar, 4);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strCommPhoneNumber", SqlDbType.NVarChar, 10);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strCommMobileNumberISD", SqlDbType.NVarChar, 4);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strCommMobileNumber", SqlDbType.NVarChar, 10);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strCommEmailID", SqlDbType.NVarChar, 50);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strCommFaxNumberISD", SqlDbType.NVarChar, 4);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strCommFaxNumberSTD", SqlDbType.NVarChar, 4);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strCommFaxNumber", SqlDbType.NVarChar, 10);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;



                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strSalientFeatureOfIND", SqlDbType.NVarChar, 500);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strReferralLetterNo", SqlDbType.NVarChar, 100);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@bintBhaTransRefNo", SqlDbType.BigInt, 19);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@dtBhaTransDated", SqlDbType.DateTime);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decPayMentAmount", SqlDbType.Decimal);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@dtBhaTransDatedOnline", SqlDbType.DateTime);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intLandUseTypeOwnsLeaAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strDrainageArea", SqlDbType.NVarChar, 250);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strSourceOfSWForIND", SqlDbType.NVarChar, 250);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intSourceOfSWForINDAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decAvgAnnualRainfall", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 18;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strTowVillWithIn2KM", SqlDbType.NVarChar, 500);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@chGWUtiForNewIND", SqlDbType.Char, 1);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@chGWUtiForExistIND", SqlDbType.Char, 1);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@chGWUtiForExpExistIND", SqlDbType.Char, 1);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@chNOCObtainForExistIND", SqlDbType.Char, 1);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@dtDateOfCommExistIND", SqlDbType.DateTime);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@dtDateOfCommExpanIND", SqlDbType.DateTime);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;


                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intINDDetailWaterReqAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decGroundWaterReq", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decSurfaceWaterReq", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decWSFromAgency", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decRecyWaterUses", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decGroundWaterReqExist", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decSurfaceWaterReqExist", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decWSFromAgencyExist", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decRecyWaterUsesExist", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;





                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decINDActExistReq", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decINDActProReq", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;//
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intINDActNoOfOperationDayInYear", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decDOMActExistReq", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decDOMActProReq", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intDOMActNoOfOperationDayInYear", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decGDEMExistReq", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decGDEMProReq", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intGDEMNoOfOperationDayInYear", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decOtherUseExistReq", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;/////
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decOtherUseProReq", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intOtherUseNoOfOperationDayInYear", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decTotalWasteWaterInDay", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;//
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intTotalWasteWaterNoOfDay", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;//
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decTotalWasteWaterInYear", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 13;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decReuseInINDActInDay", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intReuseInINDActNoOfDay", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decReuseInINDActInYear", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 13;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decReuseForGDInDay", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intReuseForGDNoOfDay", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decReuseForGDInYear", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 13;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decOtherUseInInDay", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intOtherUseNoOfDay", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 9;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decOtherUseInYear", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 13;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intNoOfStructureExisting", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intNoOfStructureProposed", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strGWAvailability", SqlDbType.NVarChar, 500);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intGWAvailabilityAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strDetRainHarARForGW", SqlDbType.NVarChar, 500);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intDetRainHarARForGWAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@chEarlierAppliedGWClear", SqlDbType.Char, 1);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strEarlierAppliedGWClearDesc", SqlDbType.NVarChar, 500);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@chUndertaking", SqlDbType.Char, 1);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@chMSME", SqlDbType.Char, 1);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@chWetlandArea", SqlDbType.Char, 1);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intGWChargesDays", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decGWChargesQty", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 18;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;





                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@chWaterChargeStatus", SqlDbType.Char, 1);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;


                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intMSMETypeCode", SqlDbType.Int);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@bintBhaTransRefNo", SqlDbType.BigInt);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@dtBhaTransDated", SqlDbType.DateTime);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@decWaterChargeAmount", SqlDbType.Decimal);
                //par_populateRelaxationApplicationForApplicationCode.Precision = 18;
                //par_populateRelaxationApplicationForApplicationCode.Scale = 2;
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;



                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@chAllowUpdate", SqlDbType.Char, 1);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@chHardCopyReq", SqlDbType.Char, 1);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intUndertakingAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@chSubmitted", SqlDbType.Char, 1);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@chPaymentTypeMode", SqlDbType.Char, 1);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intExtraAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intBharatKoshRecieptAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intNonPollutingAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intSignedDocAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intMSMEAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intWetlandareaAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intAbsRestChargeAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intPenaltyAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intImpactAssOCSAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intAuditReportAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intAffidavitOtherMSMEAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intAffidavitNonAvaAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intRestChargeAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                //par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intConsentAttCode", SqlDbType.Int);
                //par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;


                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intPayMentAttCode", SqlDbType.Int);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@chSubmitted", SqlDbType.Char,1);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                 

                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@dtCreatedOnByUC", SqlDbType.DateTime);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intCreatedByUC", SqlDbType.Int);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@dtCreatedOnByExUC", SqlDbType.DateTime);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@bintCreatedByExUC", SqlDbType.BigInt);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;

                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@dtModifiedOnByUC", SqlDbType.DateTime);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@intModifiedByUC", SqlDbType.Int);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@dtModifiedOnByExUC", SqlDbType.DateTime);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@bintModifiedByExUC", SqlDbType.BigInt);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("@strCustMsg", SqlDbType.VarChar, 1000);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.Output;
                par_populateRelaxationApplicationForApplicationCode = cmd_populateRelaxationApplicationForApplicationCode.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
                par_populateRelaxationApplicationForApplicationCode.Direction = ParameterDirection.ReturnValue;
                cmd_populateRelaxationApplicationForApplicationCode.ExecuteNonQuery();
                connn.Close();
                objAr_RelaxationApplicationBLL.ApplicationCode = Convert.ToInt64(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@bintINDAppCode"].Value);

                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@bintProFeeOrderPaymentCode"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.ProFeeOrderPaymentCode = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.ProFeeOrderPaymentCode = Convert.ToInt64(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@bintProFeeOrderPaymentCode"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@bintGWChargeOrderPaymentCode"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.GWChargeOrderPaymentCode = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.GWChargeOrderPaymentCode = Convert.ToInt64(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@bintGWChargeOrderPaymentCode"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@bintPenaltyOrderPaymentCode"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.PenaltyOrderPaymentCode = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.PenaltyOrderPaymentCode = Convert.ToInt64(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@bintPenaltyOrderPaymentCode"].Value);
                //}
                //objAr_RelaxationApplicationBLL.ApplicantUserCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intApplicantUserCode"].Value);
                //objAr_RelaxationApplicationBLL.ApplicantExternalUserCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intApplicantExUserCode"].Value);
                objAr_RelaxationApplicationBLL.UIDNumber = Convert.ToString(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strUIDNumber"].Value);

                objAr_RelaxationApplicationBLL.ApplicationTypeCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intAppTypeCode"].Value);
                objAr_RelaxationApplicationBLL.WaterQualityCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intWaterQualityCode"].Value);
                objAr_RelaxationApplicationBLL.ApplicationPurposeCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intAppPurposeCode"].Value);
                objAr_RelaxationApplicationBLL.ApplicationTypeCategoryCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intAppTypeCatCode"].Value);

                objAr_RelaxationApplicationBLL.NameOfIndustry = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strINDName"].Value.ToString();

                objAr_RelaxationApplicationBLL.AddressLine1 = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strProLocAddressLine1"].Value.ToString();
                objAr_RelaxationApplicationBLL.AddressLine2 = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strProLocAddressLine2"].Value.ToString();
                objAr_RelaxationApplicationBLL.AddressLine3 = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strProLocAddressLine3"].Value.ToString();
                objAr_RelaxationApplicationBLL.StateCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intProLocStateCode"].Value);
                objAr_RelaxationApplicationBLL.DistrictCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intProLocDistrictCode"].Value);
                objAr_RelaxationApplicationBLL.SubDistrictCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intProLocSubDistrictCode"].Value);

                switch (cmd_populateRelaxationApplicationForApplicationCode.Parameters["@chProLocVillOrTown"].Value.ToString())
                {
                    case "T":
                        objAr_RelaxationApplicationBLL.VillageOrTown = RelaxationApplication.VillageOrTownOption.Town;
                        break;
                    case "V":
                        objAr_RelaxationApplicationBLL.VillageOrTown = RelaxationApplication.VillageOrTownOption.Village;
                        break;
                }

                objAr_RelaxationApplicationBLL.TownCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intProLocTownCode"].Value);
                objAr_RelaxationApplicationBLL.VillageCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intProLocVillageCode"].Value);

                if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intProLocPinCode"].Value))
                {
                    objAr_RelaxationApplicationBLL.PinCode = null;
                }
                else
                {
                    objAr_RelaxationApplicationBLL.PinCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intProLocPinCode"].Value);
                }

                //objAr_RelaxationApplicationBLL. = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intProLocSitePlanAttCode"].Value);
                //objAr_RelaxationApplicationBLL.ProposedLocation.ProposedLocationCertifiedRevenueSketAttachCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intProLocCertReveSketAttCode"].Value);
                //objAr_RelaxationApplicationBLL.NameOfUDAAndMCAndGP = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strProUdaMcGp"].Value.ToString();

                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decProLat"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.ProposedLocation.ProposedLatitude = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.ProposedLocation.ProposedLatitude = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decProLat"].Value);
                //}

                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decProLong"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.ProposedLocation.ProposedLongitude = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.ProposedLocation.ProposedLongitude = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decProLong"].Value);
                //}

                objAr_RelaxationApplicationBLL.CommunicationAddress.AddressLine1 = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strCommAddressLine1"].Value.ToString();
                objAr_RelaxationApplicationBLL.CommunicationAddress.AddressLine2 = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strCommAddressLine2"].Value.ToString();
                objAr_RelaxationApplicationBLL.CommunicationAddress.AddressLine3 = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strCommAddressLine3"].Value.ToString();
                objAr_RelaxationApplicationBLL.CommunicationAddress.StateCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intCommStateCode"].Value);
                objAr_RelaxationApplicationBLL.CommunicationAddress.DistrictCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intCommDistrictCode"].Value);
                objAr_RelaxationApplicationBLL.CommunicationAddress.SubDistrictCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intCommSubDistrictCode"].Value);

                if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intCommPinCode"].Value))
                {
                    objAr_RelaxationApplicationBLL.CommunicationAddress.PinCode = null;
                }
                else
                {
                    objAr_RelaxationApplicationBLL.CommunicationAddress.PinCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intCommPinCode"].Value);
                }
                objAr_RelaxationApplicationBLL.CommunicationPhoneNumber.PhoneNumberISD = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strCommPhoneNumberISD"].Value.ToString();
                objAr_RelaxationApplicationBLL.CommunicationPhoneNumber.PhoneNumberSTD = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strCommPhoneNumberSTD"].Value.ToString();
                objAr_RelaxationApplicationBLL.CommunicationPhoneNumber.PhoneNumberRest = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strCommPhoneNumber"].Value.ToString();
                objAr_RelaxationApplicationBLL.CommunicationMobileNumber.MobileNumberISD = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strCommMobileNumberISD"].Value.ToString();
                objAr_RelaxationApplicationBLL.CommunicationMobileNumber.MobileNumberRest = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strCommMobileNumber"].Value.ToString();
                objAr_RelaxationApplicationBLL.CommunicationEmailID = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strCommEmailID"].Value.ToString();
                objAr_RelaxationApplicationBLL.CommunicationFaxNumber.FaxNumberISD = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strCommFaxNumberISD"].Value.ToString();
                objAr_RelaxationApplicationBLL.CommunicationFaxNumber.FaxNumberSTD = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strCommFaxNumberSTD"].Value.ToString();
                objAr_RelaxationApplicationBLL.CommunicationFaxNumber.FaxNumberRest = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strCommFaxNumber"].Value.ToString();


                //objAr_RelaxationApplicationBLL.SalientFeatureOfIndustrialActivity = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strSalientFeatureOfIND"].Value.ToString();
                //objAr_RelaxationApplicationBLL.ReferralLetterNo = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strReferralLetterNo"].Value.ToString();

                if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@bintBhaTransRefNo"].Value))
                {
                    objAr_RelaxationApplicationBLL.BharatTransReferanceNumber = null;
                }
                else
                {
                    objAr_RelaxationApplicationBLL.BharatTransReferanceNumber = Convert.ToInt64(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@bintBhaTransRefNo"].Value);
                }
                if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtBhaTransDated"].Value))
                {
                    objAr_RelaxationApplicationBLL.BharatTransDated = null;
                }
                else
                {
                    objAr_RelaxationApplicationBLL.BharatTransDated = Convert.ToDateTime(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtBhaTransDated"].Value);
                }

                if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decPayMentAmount"].Value))
                {
                    objAr_RelaxationApplicationBLL.PayMentAmount = null;
                }
                else
                {
                    objAr_RelaxationApplicationBLL.PayMentAmount = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decPayMentAmount"].Value);
                }

                // objAr_RelaxationApplicationBLL.BhaTransRefNoOnline = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strBhaTransRefNoOnline"].Value.ToString();
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtBhaTransDatedOnline"].Value))
                // {
                // objAr_RelaxationApplicationBLL.BhaTransDatedOnline = null;
                // }
                // else
                // {
                //  objAr_RelaxationApplicationBLL.BhaTransDatedOnline = Convert.ToDateTime(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtBhaTransDatedOnline"].Value);
                //}


                //////////////////////////////////
                //objAr_RelaxationApplicationBLL.LandUseDetailOfExistingProposed.LandUseTypeOwnershipLeaseAttachCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intLandUseTypeOwnsLeaAttCode"].Value);
                //objAr_RelaxationApplicationBLL.DrainageInTheArea = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strDrainageArea"].Value.ToString();
                //objAr_RelaxationApplicationBLL.SourceOfAvaillabilityOfSurfaceWaterForIndustrialUse.SourceOfAvalabilityOfSurfaceWaterDesc = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strSourceOfSWForIND"].Value.ToString();
                //objAr_RelaxationApplicationBLL.SourceOfAvaillabilityOfSurfaceWaterForIndustrialUse.SourceOfAvalabilityOfSurfaceWaterAttachCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intSourceOfSWForINDAttCode"].Value);
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decAvgAnnualRainfall"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.AverageAnnualRainfallInTheArea = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.AverageAnnualRainfallInTheArea = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decAvgAnnualRainfall"].Value);
                //}
                //objAr_RelaxationApplicationBLL.TownshipVillageWithin2KM = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strTowVillWithIn2KM"].Value.ToString();

                if (cmd_populateRelaxationApplicationForApplicationCode.Parameters["@chGWUtiForNewIND"].Value.ToString() == "Y")
                {
                    objAr_RelaxationApplicationBLL.GroundWaterUtilizationFor = RelaxationApplication.GroundWaterUtilizationForOption.NewIndustry;
                }
                else if (cmd_populateRelaxationApplicationForApplicationCode.Parameters["@chGWUtiForExistIND"].Value.ToString() == "Y")
                {
                    objAr_RelaxationApplicationBLL.GroundWaterUtilizationFor = RelaxationApplication.GroundWaterUtilizationForOption.ExistingIndustry;
                }
                else
                {
                    if (cmd_populateRelaxationApplicationForApplicationCode.Parameters["@chGWUtiForExpExistIND"].Value.ToString() == "Y")
                    {
                        objAr_RelaxationApplicationBLL.GroundWaterUtilizationFor = RelaxationApplication.GroundWaterUtilizationForOption.ExpansionProgramOfExitingIndustry;
                    }
                }

                switch (Convert.ToString(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@chNOCObtainForExistIND"].Value))
                {
                    case "Y":
                        objAr_RelaxationApplicationBLL.NOCObtainForExistIND = RelaxationApplication.NOCObtainForExistINDOption.Yes;
                        break;
                    case "N":
                        objAr_RelaxationApplicationBLL.NOCObtainForExistIND = RelaxationApplication.NOCObtainForExistINDOption.No;
                        break;
                    default:
                        objAr_RelaxationApplicationBLL.NOCObtainForExistIND = RelaxationApplication.NOCObtainForExistINDOption.NotDefined;
                        break;
                }

                if (!Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtDateOfCommExistIND"].Value)) { objAr_RelaxationApplicationBLL.DateOfCommencement = Convert.ToDateTime(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtDateOfCommExistIND"].Value); }
                else { objAr_RelaxationApplicationBLL.DateOfCommencement = null; }


                if (!Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtDateOfCommExpanIND"].Value)) { objAr_RelaxationApplicationBLL.DateOfExpansionOfProject = Convert.ToDateTime(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtDateOfCommExpanIND"].Value); }
                else { objAr_RelaxationApplicationBLL.DateOfExpansionOfProject = null; }






                //objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.WaterRequrementAttachCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intINDDetailWaterReqAttCode"].Value);

                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decGroundWaterReq"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decGroundWaterReq"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decSurfaceWaterReq"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirement = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirement = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decSurfaceWaterReq"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decWSFromAgency"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.ProposedExistingWaterSupplyFromAnyAgency = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.ProposedExistingWaterSupplyFromAnyAgency = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decWSFromAgency"].Value);
                //}
                //////////////////////

                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decRecyWaterUses"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.RecycleWaterUses = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.RecycleWaterUses = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decRecyWaterUses"].Value);
                //}





                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decGroundWaterReqExist"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decGroundWaterReqExist"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decSurfaceWaterReqExist"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementExist = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.SurfaceWaterRequirementExist = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decSurfaceWaterReqExist"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decWSFromAgencyExist"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.ProposedExistingWaterSupplyFromAnyAgencyExist = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.ProposedExistingWaterSupplyFromAnyAgencyExist = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decWSFromAgencyExist"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decRecyWaterUsesExist"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.RecycleWaterUsesExist = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decRecyWaterUsesExist"].Value);
                //}


                /////////////////////

















                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decINDActExistReq"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ExistingReq = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ExistingReq = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decINDActExistReq"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decINDActProReq"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ProposedReq = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_ProposedReq = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decINDActProReq"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intINDActNoOfOperationDayInYear"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_NoOfOperationdaysInAYear = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.IndutrialActivity_NoOfOperationdaysInAYear = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intINDActNoOfOperationDayInYear"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decDOMActExistReq"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ExistingReq = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ExistingReq = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decDOMActExistReq"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decDOMActProReq"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ProposedReq = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_ProposedReq = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decDOMActProReq"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intDOMActNoOfOperationDayInYear"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_NoOfOperationdaysInAYear = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.ResidentialDomestic_NoOfOperationdaysInAYear = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intDOMActNoOfOperationDayInYear"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decGDEMExistReq"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ExistingReq = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ExistingReq = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decGDEMExistReq"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decGDEMProReq"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ProposedReq = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_ProposedReq = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decGDEMProReq"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intGDEMNoOfOperationDayInYear"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_NoOfOperationdaysInAYear = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.GreenDeveEnviMain_NoOfOperationdaysInAYear = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intGDEMNoOfOperationDayInYear"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decOtherUseExistReq"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ExistingReq = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ExistingReq = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decOtherUseExistReq"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decOtherUseProReq"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ProposedReq = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_ProposedReq = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decOtherUseProReq"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intOtherUseNoOfOperationDayInYear"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_NoOfOperationdaysInAYear = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfWaterRequirementAndUses.OtherUse_NoOfOperationdaysInAYear = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intOtherUseNoOfOperationDayInYear"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decTotalWasteWaterInDay"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedInDay = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedInDay = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decTotalWasteWaterInDay"].Value);
                //}


                ///////2
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intTotalWasteWaterNoOfDay"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedNoOfDay = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedNoOfDay = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intTotalWasteWaterNoOfDay"].Value);
                //}


                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decTotalWasteWaterInYear"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedInYear = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TotalWasteWaterGenratedInYear = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decTotalWasteWaterInYear"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decReuseInINDActInDay"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseIndustrialActivityInDay = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseIndustrialActivityInDay = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decReuseInINDActInDay"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intReuseInINDActNoOfDay"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseIndustrialActivityNoOfDay = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseIndustrialActivityNoOfDay = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intReuseInINDActNoOfDay"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decReuseInINDActInYear"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseIndustrialActivityInYear = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseIndustrialActivityInYear = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decReuseInINDActInYear"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decReuseForGDInDay"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseGreenBeltDevelopmentInDay = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseGreenBeltDevelopmentInDay = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decReuseForGDInDay"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intReuseForGDNoOfDay"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseGreenBeltDevelopmentNoOfDay = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseGreenBeltDevelopmentNoOfDay = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intReuseForGDNoOfDay"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decReuseForGDInYear"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseGreenBeltDevelopmentInYear = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.ReuseGreenBeltDevelopmentInYear = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decReuseForGDInYear"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decOtherUseInInDay"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInDay = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInDay = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decOtherUseInInDay"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intOtherUseNoOfDay"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseNoOfDay = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseNoOfDay = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intOtherUseNoOfDay"].Value);
                //}
                ////////2




                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decOtherUseInYear"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInYear = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.IndustrialNewWaterRequirementRecycledUsesDetail.BreakupOfRecycleWaterUses.TreatedWater.OtherUseInYear = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decOtherUseInYear"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intNoOfStructureExisting"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.NumberOfStructureExisting = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.NumberOfStructureExisting = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intNoOfStructureExisting"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intNoOfStructureProposed"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.NumberOfStructureProposed = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.NumberOfStructureProposed = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intNoOfStructureProposed"].Value);
                //}
                //objAr_RelaxationApplicationBLL.GroundWaterAvailability.GroundWaterAvailabilityDesc = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strGWAvailability"].Value.ToString();
                //objAr_RelaxationApplicationBLL.GroundWaterAvailability.GroundWaterAvailabilityAttachCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intGWAvailabilityAttCode"].Value);
                //objAr_RelaxationApplicationBLL.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeDesc = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strDetRainHarARForGW"].Value.ToString();
                //objAr_RelaxationApplicationBLL.RainwaterHarvestingAndArtificialRecharge.RainwaterHarvestingAndArtificialRechargeAttachCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intDetRainHarARForGWAttCode"].Value);

                //switch (cmd_populateRelaxationApplicationForApplicationCode.Parameters["@chEarlierAppliedGWClear"].Value.ToString())
                //{
                //    case "Y":
                //        objAr_RelaxationApplicationBLL.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance = NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.Yes;
                //        break;
                //    case "N":
                //        objAr_RelaxationApplicationBLL.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearance = NOCAP.BLL.Common.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceOption.No;
                //        break;
                //}
                //objAr_RelaxationApplicationBLL.EarlierAppliedGroundwaterClearance.EarlierAppliedGroundWaterClearanceDesc = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strEarlierAppliedGWClearDesc"].Value.ToString();

                //switch (cmd_populateRelaxationApplicationForApplicationCode.Parameters["@chUndertaking"].Value.ToString())
                //{
                //    case "Y":
                //        objAr_RelaxationApplicationBLL.Undertaking.UndertakingAgreement = NOCAP.BLL.Common.Undertaking.UndertakingAgreementOption.Yes;
                //        break;
                //    case "N":
                //        objAr_RelaxationApplicationBLL.Undertaking.UndertakingAgreement = NOCAP.BLL.Common.Undertaking.UndertakingAgreementOption.No;
                //        break;
                //}
                switch (cmd_populateRelaxationApplicationForApplicationCode.Parameters["@chMSME"].Value.ToString())
                {
                    case "Y":
                        objAr_RelaxationApplicationBLL.MSME = RelaxationApplication.MSMEYesNo.Yes;
                        break;
                    case "N":
                        objAr_RelaxationApplicationBLL.MSME = RelaxationApplication.MSMEYesNo.No;
                        break;
                    case "":
                        objAr_RelaxationApplicationBLL.MSME = RelaxationApplication.MSMEYesNo.NotDefine;
                        break;
                    default:
                        objAr_RelaxationApplicationBLL.MSME = RelaxationApplication.MSMEYesNo.NotDefine;
                        break;
                }
                switch (cmd_populateRelaxationApplicationForApplicationCode.Parameters["@chWetlandArea"].Value.ToString())
                {
                    case "Y":
                        objAr_RelaxationApplicationBLL.WetLandArea = RelaxationApplication.WetLandAreaYesNo.Yes;
                        break;
                    case "N":
                        objAr_RelaxationApplicationBLL.WetLandArea = RelaxationApplication.WetLandAreaYesNo.No;
                        break;
                    case "":
                        objAr_RelaxationApplicationBLL.WetLandArea = RelaxationApplication.WetLandAreaYesNo.NotDefine;
                        break;
                    default:
                        objAr_RelaxationApplicationBLL.WetLandArea = RelaxationApplication.WetLandAreaYesNo.NotDefine;
                        break;
                }
                //objAr_RelaxationApplicationBLL.GWChargesDays = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intGWChargesDays"].Value);
                //objAr_RelaxationApplicationBLL.GWChargesQty = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decGWChargesQty"].Value);


                //switch (cmd_populateRelaxationApplicationForApplicationCode.Parameters["@chWaterChargeStatus"].Value.ToString())
                //{
                //    case "Y":
                //        objAr_RelaxationApplicationBLL.WaterChargeStatus = RelaxationApplication.WaterChargeStatusYesNo.Yes;
                //        break;
                //    case "N":
                //        objAr_RelaxationApplicationBLL.WaterChargeStatus = RelaxationApplication.WaterChargeStatusYesNo.No;
                //        break;
                //    case "":
                //        objAr_RelaxationApplicationBLL.WaterChargeStatus = RelaxationApplication.WaterChargeStatusYesNo.NotDefine;
                //        break;
                //    default:
                //        objAr_RelaxationApplicationBLL.WaterChargeStatus = RelaxationApplication.WaterChargeStatusYesNo.NotDefine;
                //        break;
                //}

                if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intMSMETypeCode"].Value))
                {
                    objAr_RelaxationApplicationBLL.MSMETypeCode = null;
                }
                else
                {
                    objAr_RelaxationApplicationBLL.MSMETypeCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intMSMETypeCode"].Value);
                }



                //objAr_RelaxationApplicationBLL.WaterChargeTypeCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intWaterChargeTypeCode"].Value);
                //objAr_RelaxationApplicationBLL.WaterChargeAmount = Convert.ToDecimal(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@decWaterChargeAmount"].Value);


                //switch (cmd_populateRelaxationApplicationForApplicationCode.Parameters["@chAllowUpdate"].Value.ToString())
                //{
                //    case "Y":
                //        objAr_RelaxationApplicationBLL.AllowUpdate = RelaxationApplication.AllowUpdateYesNo.Yes;
                //        break;
                //    case "N":
                //        objAr_RelaxationApplicationBLL.AllowUpdate = RelaxationApplication.AllowUpdateYesNo.No;
                //        break;
                //}
                //switch (cmd_populateRelaxationApplicationForApplicationCode.Parameters["@chHardCopyReq"].Value.ToString())
                //{
                //    case "Y":
                //        objAr_RelaxationApplicationBLL.HardCopyReq = RelaxationApplication.HardCopyReqYesNo.Yes;
                //        break;
                //    case "N":
                //        objAr_RelaxationApplicationBLL.HardCopyReq = RelaxationApplication.HardCopyReqYesNo.No;
                //        break;
                //}

                //objAr_RelaxationApplicationBLL.Undertaking.UndertakingAttachCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intUndertakingAttCode"].Value);

                //switch (cmd_populateRelaxationApplicationForApplicationCode.Parameters["@chSubmitted"].Value.ToString())
                //{
                //    case "Y":
                //        objAr_RelaxationApplicationBLL.SaveApplicationAs = RelaxationApplication.SaveApplicationOption.Submitted;
                //        break;
                //    default:
                //        objAr_RelaxationApplicationBLL.SaveApplicationAs = NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.SaveApplicationOption.SaveAsDraft;
                //        break;
                //}
                //switch (cmd_populateRelaxationApplicationForApplicationCode.Parameters["@chPaymentTypeMode"].Value.ToString())
                //{
                //    case "S":
                //        objAr_RelaxationApplicationBLL.PaymentTypeMode = BLL.Common.CommonEnum.PaymentTypeMode.Single;
                //        break;
                //    case "C":
                //        objAr_RelaxationApplicationBLL.PaymentTypeMode = BLL.Common.CommonEnum.PaymentTypeMode.Combined;
                //        break;
                //    default:
                //        objAr_RelaxationApplicationBLL.PaymentTypeMode = BLL.Common.CommonEnum.PaymentTypeMode.NotDefined;
                //        break;
                //}

                ////@intExtraAttCode
                //objAr_RelaxationApplicationBLL.IndustrialNewExtraAttachmentCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intExtraAttCode"].Value);
                //objAr_RelaxationApplicationBLL.IndustrialNewBharatKoshRecieptAttachmentCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intBharatKoshRecieptAttCode"].Value);
                //objAr_RelaxationApplicationBLL.IndustrialNewNonPollutingAttCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intNonPollutingAttCode"].Value);


                //objAr_RelaxationApplicationBLL.SignedDocAttCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intSignedDocAttCode"].Value);
                //objAr_RelaxationApplicationBLL.MSMEAttCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intMSMEAttCode"].Value);
                //objAr_RelaxationApplicationBLL.WetlandAreaAttCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intWetlandareaAttCode"].Value);
                //objAr_RelaxationApplicationBLL.AbsRestChargeAttCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intAbsRestChargeAttCode"].Value);
                //objAr_RelaxationApplicationBLL.PenaltyAttCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intPenaltyAttCode"].Value);
                //objAr_RelaxationApplicationBLL.ImpactAssOCSAttCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intImpactAssOCSAttCode"].Value);
                //objAr_RelaxationApplicationBLL.AuditReportAttCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intAuditReportAttCode"].Value);
                //objAr_RelaxationApplicationBLL.AffidavitOtherMSMEAttCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intAffidavitOtherMSMEAttCode"].Value);
                //objAr_RelaxationApplicationBLL.AffidavitNonAvaAttCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intAffidavitNonAvaAttCode"].Value);
                //objAr_RelaxationApplicationBLL.RestChargeAttCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intRestChargeAttCode"].Value);
                objAr_RelaxationApplicationBLL.PayMentAttCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intPayMentAttCode"].Value);

                // objAr_RelaxationApplicationBLL.PayMentAttCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intPayMentAttCode"].Value);
                 

                switch (cmd_populateRelaxationApplicationForApplicationCode.Parameters["@chSubmitted"].Value.ToString())
                {
                    case "Y":
                        objAr_RelaxationApplicationBLL.Submitted = RelaxationApplication.SubmittedOption.Yes;
                        break;
                    case "N":
                        objAr_RelaxationApplicationBLL.Submitted = RelaxationApplication.SubmittedOption.No;
                        break;
                    case "":
                        objAr_RelaxationApplicationBLL.Submitted = RelaxationApplication.SubmittedOption.NotDefine;
                        break;
                    default:
                        objAr_RelaxationApplicationBLL.Submitted = RelaxationApplication.SubmittedOption.NotDefine;
                        break;
                }

                // objAr_RelaxationApplicationBLL.IndustrialNewAttachmentCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intINDAttCode"].Value);

                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intLatestAppStatusCode"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.LatestApplicationStatusCode = 0;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.LatestApplicationStatusCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intLatestAppStatusCode"].Value);
                //}
                //switch (cmd_populateRelaxationApplicationForApplicationCode.Parameters["@chVerificationRequired"].Value.ToString())
                //{
                //    case "Y":
                //        objAr_RelaxationApplicationBLL.VerificationDetail.VerificationRequired = NOCAP.BLL.Common.VerificationInfo.VerificationRequiredOption.Yes;
                //        break;
                //    case "N":
                //        objAr_RelaxationApplicationBLL.VerificationDetail.VerificationRequired = NOCAP.BLL.Common.VerificationInfo.VerificationRequiredOption.No;
                //        break;
                //    default:
                //        objAr_RelaxationApplicationBLL.VerificationDetail.VerificationRequired = NOCAP.BLL.Common.VerificationInfo.VerificationRequiredOption.NotDefined;
                //        break;
                //}
                //switch (cmd_populateRelaxationApplicationForApplicationCode.Parameters["@chVerified"].Value.ToString())
                //{
                //    case "Y":
                //        objAr_RelaxationApplicationBLL.VerificationDetail.Verified = NOCAP.BLL.Common.VerificationInfo.VerifiedOption.Yes;
                //        break;
                //    case "N":
                //        objAr_RelaxationApplicationBLL.VerificationDetail.Verified = NOCAP.BLL.Common.VerificationInfo.VerifiedOption.No;
                //        break;
                //    default:
                //        objAr_RelaxationApplicationBLL.VerificationDetail.Verified = NOCAP.BLL.Common.VerificationInfo.VerifiedOption.NotDefined;
                //        break;
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intVerifiedByUserCode"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.VerificationDetail.VerifiedByUserCode = 0;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.VerificationDetail.VerifiedByUserCode = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intVerifiedByUserCode"].Value);
                //}
                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtVerifiedDate"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.VerificationDetail.VerifiedDate = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.VerificationDetail.VerifiedDate = Convert.ToDateTime(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtVerifiedDate"].Value);
                //}
                //objAr_RelaxationApplicationBLL.IndustrialNewApplicationNumber = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strINDAppNo"].Value.ToString();
                ////if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intINDAppSN"].Value))
                ////{
                ////    objAr_RelaxationApplicationBLL. = null;
                ////}
                ////else
                ////{
                ////    objAr_RelaxationApplicationBLL. = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intINDAppSN"].Value);
                ////}
                ////objAr_RelaxationApplicationBLL. = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strINDNOCNo"].Value.ToString();
                ////if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intINDNOCSN"].Value))
                ////{
                ////    objAr_RelaxationApplicationBLL. = null;
                ////}
                ////else
                ////{
                ////    objAr_RelaxationApplicationBLL. = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intINDNOCSN"].Value);
                ////}
                ////if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtNOCStartDate"].Value))
                ////{
                ////    objAr_RelaxationApplicationBLL. = null;
                ////}
                ////else
                ////{
                ////    objAr_RelaxationApplicationBLL. = Convert.ToDateTime(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtNOCStartDate"].Value);
                ////}
                ////if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtNOCEndDate"].Value))
                ////{
                ////    objAr_RelaxationApplicationBLL. = null;
                ////}
                ////else
                ////{
                ////    objAr_RelaxationApplicationBLL. = Convert.ToDateTime(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtNOCEndDate"].Value);
                ////}



                if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtCreatedOnByUC"].Value))
                {
                    objAr_RelaxationApplicationBLL.CreatedOnByUC = null;
                }
                else
                {
                    objAr_RelaxationApplicationBLL.CreatedOnByUC = Convert.ToDateTime(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtCreatedOnByUC"].Value);
                }

                if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intCreatedByUC"].Value))
                {
                    objAr_RelaxationApplicationBLL.CreatedByUC = null;
                }
                else
                {
                    objAr_RelaxationApplicationBLL.CreatedByUC = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intCreatedByUC"].Value);
                }


                if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtCreatedOnByExUC"].Value))
                {
                    objAr_RelaxationApplicationBLL.CreatedOnByExUC = null;
                }
                else
                {
                    objAr_RelaxationApplicationBLL.CreatedOnByExUC = Convert.ToDateTime(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtCreatedOnByExUC"].Value);
                }

                if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@bintCreatedByExUC"].Value))
                {
                    objAr_RelaxationApplicationBLL.CreatedByExUC = null;
                }
                else
                {
                    objAr_RelaxationApplicationBLL.CreatedByExUC = Convert.ToInt64(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@bintCreatedByExUC"].Value);
                }

                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtSubmittedOnByUC"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.SubmittedOnByUC = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.SubmittedOnByUC = Convert.ToDateTime(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtSubmittedOnByUC"].Value);
                //}

                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intSubmittedByUC"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.SubmittedByUC = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.SubmittedByUC = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intSubmittedByUC"].Value);
                //}

                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtSubmittedOnByExUC"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.SubmittedOnByExUC = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.SubmittedOnByExUC = Convert.ToDateTime(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtSubmittedOnByExUC"].Value);
                //}

                //if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@bintSubmittedByExUC"].Value))
                //{
                //    objAr_RelaxationApplicationBLL.SubmittedByExUC = null;
                //}
                //else
                //{
                //    objAr_RelaxationApplicationBLL.SubmittedByExUC = Convert.ToInt64(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@bintSubmittedByExUC"].Value);
                //}

                if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtModifiedOnByUC"].Value))
                {
                    objAr_RelaxationApplicationBLL.ModifiedOnByUC = null;
                }
                else
                {
                    objAr_RelaxationApplicationBLL.ModifiedOnByUC = Convert.ToDateTime(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtModifiedOnByUC"].Value);
                }

                if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intModifiedByUC"].Value))
                {
                    objAr_RelaxationApplicationBLL.ModifiedByUC = null;
                }
                else
                {
                    objAr_RelaxationApplicationBLL.ModifiedByUC = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@intModifiedByUC"].Value);
                }

                if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtModifiedOnByExUC"].Value))
                {
                    objAr_RelaxationApplicationBLL.ModifiedOnByExUC = null;
                }
                else
                {
                    objAr_RelaxationApplicationBLL.ModifiedOnByExUC = Convert.ToDateTime(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@dtModifiedOnByExUC"].Value);
                }

                if (Convert.IsDBNull(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@bintModifiedByExUC"].Value))
                {
                    objAr_RelaxationApplicationBLL.ModifiedByExUC = null;
                }
                else
                {
                    objAr_RelaxationApplicationBLL.ModifiedByExUC = Convert.ToInt64(cmd_populateRelaxationApplicationForApplicationCode.Parameters["@bintModifiedByExUC"].Value);
                }


                objAr_RelaxationApplicationBLL.CustumMessage = cmd_populateRelaxationApplicationForApplicationCode.Parameters["@strCustMsg"].Value.ToString();
                int_status = Convert.ToInt32(cmd_populateRelaxationApplicationForApplicationCode.Parameters["RETURN_VALUE"].Value.ToString());

                return int_status;
            }
            catch (Exception ex)
            {
                objAr_RelaxationApplicationBLL.CustumMessage = "Industrial New Application Attachment Problem in DAL" + ex.Message;
                if (int_connOpenOrNot == 1)
                {
                    connn.Close();
                }
                return int_status;
            }
            finally
            {
                if (cmd_populateRelaxationApplicationForApplicationCode != null)
                {
                    cmd_populateRelaxationApplicationForApplicationCode.Dispose();
                }
                if (connn != null)
                {
                    connn.Dispose();
                }

            }
        }

          

        #endregion



    }
}
