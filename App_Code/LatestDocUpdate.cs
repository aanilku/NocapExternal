using NOCAP.BLL.LatestUpdate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for LatestDocUpdate
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class LatestDocUpdate : System.Web.Services.WebService
{

    public LatestDocUpdate()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    public class Compliances
    {

        public string AppLicationNumber { get; set; }
        public string Name { get; set; }
        public string NOCNumber { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string ExpiredOnDate { get; set; }
    }

    [WebMethod]
    public List<Compliances> GetCompliancePendingList()
    {

        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialReNewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication();
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureReNewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication();
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningReNewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication();
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication();

        try
        {
            List<Compliances> dt = new List<Compliances>();
            Compliances objCom = new Compliances();
            NOCAP.BLL.UserManagement.User obj_User = new NOCAP.BLL.UserManagement.User();
            ;
            #region IND NEW


            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication[] arr_industrialNewApplication = obj_User.GetIndustrialNewApprovedApplicationList();//obj_IndustrialNewApplication.IndustrialNewApplicationCollection;                                                                                                                                                             // arr_industrialNewApplication = arr_industrialNewApplication.Where(x => x.LatestApplicationStatusCode == Convert.ToInt32(NOCAP.BLL.Utility.Utility.LatestApplicationStatusCodeOption.Approved)).ToArray();

            foreach (NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj in arr_industrialNewApplication)
            {
                NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_industrialNewIssusedLetter = obj.GetIssuedLetter();
                if (obj_industrialNewIssusedLetter != null && obj_industrialNewIssusedLetter.ValidityStartDate != null)
                {


                    if (Convert.ToDateTime(obj_industrialNewIssusedLetter.ValidityStartDate).AddMonths(11) < DateTime.Now && Convert.ToDateTime(obj_industrialNewIssusedLetter.ValidityEndDate) > DateTime.Now)
                    {

                        NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(obj.IndustrialNewApplicationCode);
                        if (obj_selfCompliance.ApplicationCode == 0 || obj_selfCompliance.Submitted ==
                            NOCAP.BLL.Common.CommonEnum.YesNoOption.No
                            || obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined)
                        {
                            objCom.AppLicationNumber = obj.IndustrialNewApplicationNumber;
                            objCom.Name = obj.NameOfIndustry;
                            objCom.NOCNumber = obj.NOCNumber;
                            objCom.StateName = new NOCAP.BLL.Master.State(obj.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
                            objCom.DistrictName = new NOCAP.BLL.Master.District(obj.ProposedLocation.ProposedLocationAddress.StateCode, obj.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName;
                            objCom.ExpiredOnDate = Convert.ToDateTime(obj_industrialNewIssusedLetter.ValidityStartDate).ToString("dd/mm/yyyy");
                            dt.Add(objCom);

                        }
                    }
                }
            }
            #endregion

            #region IND RENEW 


            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication[] arr_industrialReNewApplication = obj_User.GetIndustrialRenewApprovedApplicationList();


            foreach (NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj in arr_industrialReNewApplication)
            {
                obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj.FirstApplicationCode);
                NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter obj_industrialReNewIssusedLetter = obj.GetIssuedLetter();
                if (obj_industrialReNewIssusedLetter != null && obj_industrialReNewIssusedLetter.ValidityStartDate != null)
                {


                    if (Convert.ToDateTime(obj_industrialReNewIssusedLetter.ValidityStartDate).AddMonths(11) < DateTime.Now && Convert.ToDateTime(obj_industrialReNewIssusedLetter.ValidityEndDate) > DateTime.Now)
                    {

                        NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(obj.IndustrialRenewApplicationCode);
                        if (obj_selfCompliance.ApplicationCode == 0 ||
                            obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.No
                            || obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined)
                        {

                            objCom.AppLicationNumber = obj_IndustrialNewApplication.IndustrialNewApplicationNumber;
                            objCom.Name = obj.NameOfIndustry;
                            objCom.NOCNumber = obj.NOCNumber;
                            objCom.StateName = new NOCAP.BLL.Master.State(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
                            objCom.DistrictName = new NOCAP.BLL.Master.District(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName;
                            objCom.ExpiredOnDate = Convert.ToDateTime(obj_industrialReNewIssusedLetter.ValidityStartDate).ToString("dd/mm/yyyy");
                            dt.Add(objCom);

                        }
                    }
                }
            }
            #endregion

            #region INF NEW


            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication[] arr_infrastructureNewApplication = obj_User.GetInfrastructureNewApprovedApplicationList();


            foreach (NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj in arr_infrastructureNewApplication)
            {
                NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_infrastructureNewIssusedLetter = obj.GetIssuedLetter();
                if (obj_infrastructureNewIssusedLetter != null && obj_infrastructureNewIssusedLetter.ValidityStartDate != null)
                {

                    if (Convert.ToDateTime(obj_infrastructureNewIssusedLetter.ValidityStartDate).AddMonths(11) < DateTime.Now && Convert.ToDateTime(obj_infrastructureNewIssusedLetter.ValidityEndDate) > DateTime.Now)
                    {

                        NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(obj.InfrastructureNewApplicationCode);
                        if (obj_selfCompliance.ApplicationCode == 0 ||
                          obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.No
                          || obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined)
                        {
                            objCom.AppLicationNumber = obj.InfrastructureNewApplicationNumber;
                            objCom.Name = obj.NameOfInfrastructure;
                            objCom.NOCNumber = obj.NOCNumber;
                            objCom.StateName = new NOCAP.BLL.Master.State(obj.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
                            objCom.DistrictName = new NOCAP.BLL.Master.District(obj.ProposedLocation.ProposedLocationAddress.StateCode, obj.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName;
                            objCom.ExpiredOnDate = Convert.ToDateTime(obj_infrastructureNewIssusedLetter.ValidityStartDate).ToString("dd/mm/yyyy");
                            dt.Add(objCom);

                        }
                    }
                }
            }

            #endregion

            #region INF RENEW


            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication[] arr_infrastructureReNewApplication = obj_User.GetInfrastructureRenewApprovedApplicationList();


            foreach (NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj in arr_infrastructureReNewApplication)
            {

                obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj.FirstApplicationCode);

                NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter obj_infrastructureReNewIssusedLetter = obj.GetIssuedLetter();
                if (obj_infrastructureReNewIssusedLetter != null && obj_infrastructureReNewIssusedLetter.ValidityStartDate != null)
                {

                    if (Convert.ToDateTime(obj_infrastructureReNewIssusedLetter.ValidityStartDate).AddMonths(11) < DateTime.Now && Convert.ToDateTime(obj_infrastructureReNewIssusedLetter.ValidityEndDate) > DateTime.Now)
                    {
                        NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(obj.InfrastructureRenewApplicationCode);
                        if (obj_selfCompliance.ApplicationCode == 0 ||
                             obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.No
                             || obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined)
                        {

                            objCom.AppLicationNumber = obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber;
                            objCom.Name = obj.NameOfInfrastructure;
                            objCom.NOCNumber = obj.NOCNumber;
                            objCom.StateName = new NOCAP.BLL.Master.State(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
                            objCom.DistrictName = new NOCAP.BLL.Master.District(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName;
                            objCom.ExpiredOnDate = Convert.ToDateTime(obj_infrastructureReNewIssusedLetter.ValidityStartDate).ToString("dd/mm/yyyy");
                            dt.Add(objCom);

                        }
                    }
                }
            }
            #endregion

            #region MIN NEW


            NOCAP.BLL.Mining.New.Application.MiningNewApplication[] arr_MiningNewApplication = obj_User.GetMiningNewApprovedApplicationList();

            foreach (NOCAP.BLL.Mining.New.Application.MiningNewApplication obj in arr_MiningNewApplication)
            {
                NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter obj_MiningNewIssusedLetter = obj.GetIssuedLetter();
                if (obj_MiningNewIssusedLetter != null && obj_MiningNewIssusedLetter.ValidityStartDate != null)
                {

                    if (Convert.ToDateTime(obj_MiningNewIssusedLetter.ValidityStartDate).AddMonths(11) < DateTime.Now && Convert.ToDateTime(obj_MiningNewIssusedLetter.ValidityEndDate) > DateTime.Now)
                    {

                        NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(obj.ApplicationCode);
                        if (obj_selfCompliance.ApplicationCode == 0 ||
                              obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.No
                              || obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined)
                        {


                            objCom.AppLicationNumber = obj.MiningNewApplicationNumber;
                            objCom.Name = obj.NameOfMining;
                            objCom.NOCNumber = obj.NOCNumber;
                            objCom.StateName = new NOCAP.BLL.Master.State(obj.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
                            objCom.DistrictName = new NOCAP.BLL.Master.District(obj.ProposedLocation.ProposedLocationAddress.StateCode, obj.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName;
                            objCom.ExpiredOnDate = Convert.ToDateTime(obj_MiningNewIssusedLetter.ValidityStartDate).ToString("dd/mm/yyyy");
                            dt.Add(objCom);

                        }
                    }
                }
            }

            #endregion

            #region MIN RENEW


            NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication[] arr_MiningReNewApplication = obj_User.GetMiningRenewApprovedApplicationList();


            foreach (NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj in arr_MiningReNewApplication)
            {
                obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj.FirstApplicationCode);

                NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter obj_MiningReNewIssusedLetter = obj.GetIssuedLetter();
                if (obj_MiningReNewIssusedLetter != null && obj_MiningReNewIssusedLetter.ValidityStartDate != null)
                {
                    if (Convert.ToDateTime(obj_MiningReNewIssusedLetter.ValidityStartDate).AddMonths(11) < DateTime.Now && Convert.ToDateTime(obj_MiningReNewIssusedLetter.ValidityEndDate) > DateTime.Now)
                    {
                        NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(obj.MiningRenewApplicationCode);
                        if (obj_selfCompliance.ApplicationCode == 0 ||
                          obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.No
                          || obj_selfCompliance.Submitted == NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined)
                        {

                            objCom.AppLicationNumber = obj_MiningNewApplication.MiningNewApplicationNumber;
                            objCom.Name = obj.NameOfMining;
                            objCom.NOCNumber = obj.NOCNumber;
                            objCom.StateName = new NOCAP.BLL.Master.State(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
                            objCom.DistrictName = new NOCAP.BLL.Master.District(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode).DistrictName;
                            objCom.ExpiredOnDate = Convert.ToDateTime(obj_MiningReNewIssusedLetter.ValidityStartDate).ToString("dd/mm/yyyy");
                            dt.Add(objCom);

                        }
                    }
                }
            }

            #endregion

            return dt;

        }
        catch (Exception ex)
        {
            return null;
        }

    }

    [WebMethod]
    public NOCAP.BLL.UserManagement.User[] BindGrid_AllRenewalList()
    {
        NOCAP.BLL.UserManagement.User[] arrLatestUpdate = null;
        try
        {
            string notice = null;

            NOCAP.BLL.UserManagement.User obj_User = new NOCAP.BLL.UserManagement.User();
            obj_User.GetAllRenewalList();
            if (obj_User.UserCollection != null)
            {
                arrLatestUpdate = obj_User.UserCollection;

                return arrLatestUpdate;

            }
            else
            {
                return null;
            }

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    [WebMethod]
    public List<NOCAP.BLL.LatestUpdate.LatestDocument> bindGridLatestUpdateDocument()
    {
        NOCAP.BLL.LatestUpdate.LatestDocument[] arrLatestUpdate = null;
        try
        {
            string notice = null;

            NOCAP.BLL.LatestUpdate.LatestDocument obj_LatestUpdate = new NOCAP.BLL.LatestUpdate.LatestDocument();
            obj_LatestUpdate.Visibility = NOCAP.BLL.LatestUpdate.LatestDocument.VisibilityYesNo.Yes;
            arrLatestUpdate = obj_LatestUpdate.GetLatestDocumentList(NOCAP.BLL.LatestUpdate.LatestDocument.SortingField.NoSorting);
            if (arrLatestUpdate != null)
            {
                List<LatestDocument> listadd = new List<LatestDocument>();
                LatestDocument obj = null;
                for (int i = 0; i < arrLatestUpdate.Length; i++)
                {
                    obj = new LatestDocument();
                    obj.DocumentText = arrLatestUpdate[i].DocumentText;
                    obj.ConvertDateTimeFormet = Convert.ToDateTime(arrLatestUpdate[i].CreatedOn).ToShortDateString();
                    obj.IsNew = arrLatestUpdate[i].IsNew;
                    obj.FilePath = arrLatestUpdate[i].FilePath;
                    obj.DocumentCode = arrLatestUpdate[i].DocumentCode;
                    listadd.Add(obj);
                }

                return listadd;

            }
            else
            {
                return null;
            }

        }
        catch (Exception ex)
        {
            return null;
        }
    }
    [WebMethod]
    public List<NOCAP.BLL.LatestUpdate.LatestDocument> bindGridLatestOrderCircular()
    {
        NOCAP.BLL.LatestUpdate.LatestDocument[] arrLatestUpdate = null;
        try
        {
            string notice = null;

            NOCAP.BLL.LatestUpdate.LatestDocument obj_LatestUpdate = new NOCAP.BLL.LatestUpdate.LatestDocument();
            obj_LatestUpdate.Visibility = NOCAP.BLL.LatestUpdate.LatestDocument.VisibilityYesNo.Yes;
            arrLatestUpdate = obj_LatestUpdate.GetLatestDocumentList(NOCAP.BLL.LatestUpdate.LatestDocument.SortingField.NoSorting);
            if (arrLatestUpdate != null)
            {
                List<LatestDocument> listadd = new List<LatestDocument>();
                LatestDocument obj = null;
                for (int i = 0; i < arrLatestUpdate.Length; i++)
                {
                    if (arrLatestUpdate[i].OrderCircularAndPublicNotice == NOCAP.BLL.LatestUpdate.LatestDocument.ONAndPNField.OrderCircular || arrLatestUpdate[i].OrderCircularAndPublicNotice == NOCAP.BLL.LatestUpdate.LatestDocument.ONAndPNField.Both)
                    {
                        obj = new NOCAP.BLL.LatestUpdate.LatestDocument();
                        obj.DocumentText = arrLatestUpdate[i].DocumentText;
                        obj.ConvertDateTimeFormet = Convert.ToDateTime(arrLatestUpdate[i].CreatedOn).ToShortDateString();
                        obj.IsNew = arrLatestUpdate[i].IsNew;
                        obj.FilePath = arrLatestUpdate[i].FilePath;
                        obj.DocumentCode = arrLatestUpdate[i].DocumentCode;
                        listadd.Add(obj);
                    }
                }

                return listadd;

            }
            else
            {
                return null;
            }

        }
        catch (Exception ex)
        {
            return null;
        }
    }


    [WebMethod]
    public List<NOCAP.BLL.LatestUpdate.LatestDocument> bindGridLatestUpdateDocumentPublic()
    {
        NOCAP.BLL.LatestUpdate.LatestDocument[] arrLatestUpdate = null;
        try
        {
            string notice = null;

            NOCAP.BLL.LatestUpdate.LatestDocument obj_LatestUpdate = new NOCAP.BLL.LatestUpdate.LatestDocument();
            obj_LatestUpdate.Visibility = NOCAP.BLL.LatestUpdate.LatestDocument.VisibilityYesNo.Yes;
            arrLatestUpdate = obj_LatestUpdate.GetLatestDocumentList(NOCAP.BLL.LatestUpdate.LatestDocument.SortingField.NoSorting);
            if (arrLatestUpdate != null)
            {
                List<LatestDocument> listadd = new List<LatestDocument>();
                LatestDocument obj = null;
                for (int i = 0; i < arrLatestUpdate.Length; i++)
                {
                    if (arrLatestUpdate[i].OrderCircularAndPublicNotice == NOCAP.BLL.LatestUpdate.LatestDocument.ONAndPNField.PublicNotice || arrLatestUpdate[i].OrderCircularAndPublicNotice == NOCAP.BLL.LatestUpdate.LatestDocument.ONAndPNField.Both)
                    {
                        obj = new NOCAP.BLL.LatestUpdate.LatestDocument();
                        obj.DocumentText = arrLatestUpdate[i].DocumentText;
                        obj.ConvertDateTimeFormet = Convert.ToDateTime(arrLatestUpdate[i].CreatedOn).ToShortDateString();
                        obj.IsNew = arrLatestUpdate[i].IsNew;
                        obj.FilePath = arrLatestUpdate[i].FilePath;
                        obj.DocumentCode = arrLatestUpdate[i].DocumentCode;
                        listadd.Add(obj);
                    }
                }

                return listadd;

            }
            else
            {
                return null;
            }

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    [WebMethod]
    public void DownloadDocumentFile(long hndID)
    {
        if (hndID != null && Convert.ToInt32(hndID) > 0)
        {
            NOCAPExternalUtility.INTLatestUpdateDocumentDownloadFiles(Convert.ToInt32(hndID));
        }

    }






}
