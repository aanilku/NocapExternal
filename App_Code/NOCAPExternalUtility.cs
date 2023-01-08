using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Web.SessionState;
using System.Configuration;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Data;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Windows.Forms;

/// <summary>
/// Summary description for NOCAPExternalUtility
/// </summary>
public class NOCAPExternalUtility
{
    public static string ExternalErrorRedirectUrl = "~/ExternalErrorPage.aspx";
    //const string DESKey = "AQWSEDRZ";
    // const string DESIV = "HGFEDCBZ";
    const string DESKey = "AQWSEDRZTFDSWTYOKJHG123456789123";
    const string DESIV = "HGFEDCBZHJKGFDHK";
    public static DialogResult MsgBox(string msg)
    {
      return  MessageBox.Show(msg, "NOCAP");
    }


   
    public static int FillDropDownApplicationNumber(ref DropDownList ddl_ApplicationNumber, long lngA_usercode, int intA_AppTypeCode)
    {
        ExternalUser objAr_UserBLL = new ExternalUser(lngA_usercode);
       

        int int_status = 0;
        try
        {
            ddl_ApplicationNumber.Items.Clear();


            ddl_ApplicationNumber.DataTextField = "AppName";
            ddl_ApplicationNumber.DataValueField = "AppNo";
            ddl_ApplicationNumber.DataSource = objAr_UserBLL.GetApplicationNumberList(intA_AppTypeCode);
            ddl_ApplicationNumber.DataBind();
            int_status = 1;
            AddFirstItemInDropDownList(ref ddl_ApplicationNumber);
            return int_status;
        }
        catch (Exception)
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_ApplicationNumber);
            return int_status;
        }

    }

    public static int ExternalAttachmentSizeLimit()
    {

        NOCAP.BLL.Common.AttachmentLimit obj_attachmentLimit = new NOCAP.BLL.Common.AttachmentLimit();
        int AttachmentSize = 4048576 * (obj_attachmentLimit.SizeOfEachAttachment);
        return AttachmentSize;
    }
    public static string[] TransactionStatusMain(string strA_OrderPaymentCodeForNTRP, ref NOCAP.BLL.Common.CommonEnum.PaymentStatus enuA_PaymentStatus, ref NOCAP.BLL.Common.CommonEnum.ReceiptStatus enuA_ReceiptStatus)
    {
        HttpResponseMessage messge = null;
        string[] arr = null;
        string access_token = string.Empty;
        try
        {
            string URL = ConfigurationManager.AppSettings["bharatkoshUrl"].ToString();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ConfigurationManager.AppSettings["MediaTypeAppForm"].ToString()));

            Dictionary<string, string> openWith = new Dictionary<string, string>();
            openWith.Add("OrderId", strA_OrderPaymentCodeForNTRP.Trim());
            openWith.Add("PurposeId", ConfigurationManager.AppSettings["OrderContent"].ToString());

            FormUrlEncodedContent content = new FormUrlEncodedContent(openWith);
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            messge = client.PostAsync(URL, content).Result;
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;
                arr = result.Split('|');
                enuA_PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.NotDefined;
                enuA_ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.NotDefined;
                if (arr != null && arr.Length > 0)
                {

                    switch (arr[1].ToUpper())
                    {

                        case "SUCCESS":
                            enuA_PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.SUCCESS;
                            break;
                        case "FAIL":
                            enuA_PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.FAIL;
                            break;
                        case "STATUS UNKNOWN/ABORT":
                            enuA_PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.StatusUnknownAbort;
                            break;
                        case "NO RECORD FOUND":
                            enuA_PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.NoRecordsFound;
                            break;
                        case "BOOKED":
                            enuA_PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.BOOKED;
                            break;
                        case "PENDING":
                            enuA_PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.PENDING;
                            break;
                        case "EXPIRED":
                            enuA_PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.EXPIRED;
                            break;
                        case "FAILREF":
                            enuA_PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.FailedOrRefundIfReceived;
                            break;
                        case "REFSET":
                            enuA_PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.RefundSettled;
                            break;
                        case "OFFDSCRD":
                            enuA_PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.PENDING;
                            break;
                        case "TERMINATE":
                            enuA_PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.TERMINATE;
                            break;
                        case "CREATED":
                            enuA_PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.Created;
                            break;
                        case "QUITED":
                            enuA_PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.Quited;
                            break;
                        case "DELETED":
                            enuA_PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.Deleted;
                            break;
                        case "REINITIATION":
                            enuA_PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.ReInitiation;
                            break;
                        case "HOLD":
                            enuA_PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.HOLD;
                            break;

                        case "PAYMENT INITIATED":
                            enuA_ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.PaymentInitiated;
                            break;
                        case "CONFIRMED":
                            enuA_ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.Confirmed;
                            break;
                        case "INCOMPLETE":
                            enuA_ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.Incomplete;
                            break;
                        case "SUBMITTED":
                            enuA_ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.Submitted;
                            break;
                        default:
                            enuA_PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.NotDefined;
                            enuA_ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.NotDefined;
                            break;
                    }

                }

            }
        }

        catch (Exception ex)
        {
            string d = ex.Message;
            arr = null;
        }
        return arr;
    }

    public static int DoubleTaxDownloadFiles(long lng_ApplicationCode, int SN)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.ProcessingFee.DoubleTaxPayment obj_DoubleTaxPayment = new NOCAP.BLL.ProcessingFee.DoubleTaxPayment();
            obj_DoubleTaxPayment = obj_DoubleTaxPayment.DownloadDoubleTax(lng_ApplicationCode, SN);
            if (obj_DoubleTaxPayment != null)
            {
                byte[] bytes = obj_DoubleTaxPayment.AttFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = obj_DoubleTaxPayment.ContentType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "DoubleTaxPay" + Convert.ToString(lng_ApplicationCode) + "_" + obj_DoubleTaxPayment.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();

                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }

    }
    public static XmlNodeList GetRSAKey(XmlDocument xmlDoc)
    {
        try
        {
            string CertificateKey = ConfigurationManager.AppSettings["NTRPPublicKey"].ToString();
            X509Store certificate = new X509Store(StoreLocation.CurrentUser);


            certificate = new X509Store(StoreLocation.LocalMachine);
            certificate.Open(OpenFlags.ReadOnly);
            RSACryptoServiceProvider rsaKey = null;
            string checkCertificate = "";
            X509Certificate2 certx = new X509Certificate2();
            foreach (X509Certificate2 cert in certificate.Certificates)
            {
                if (cert.Subject.Contains(CertificateKey))
                {

                    // retrieve private key                  
                    // rsaKey = (RSACryptoServiceProvider)cert.PrivateKey;                       
                    rsaKey = (RSACryptoServiceProvider)cert.PublicKey.Key;
                    certx = cert;
                    checkCertificate = GetDateTimeValue(certx.NotAfter.ToString("yyyy-MM-dd")) >= GetDateTimeValue(DateTime.Now.ToString("yyyy-MM-dd")) ? "Valid " : "Expired";
                    break;
                }
            }
            if (checkCertificate == "Expired")
                throw new Exception("Valid certificate was not found or Expired");
            if (rsaKey == null && checkCertificate == "Expired")
                throw new Exception("Valid certificate was not found or Expired");
            string strResult = string.Empty;
            bool flag_SuccessFail = VerifyXml(xmlDoc, rsaKey, ref strResult);
            XmlNodeList elemList = xmlDoc.GetElementsByTagName("orderStatus");

            return elemList;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    private static bool VerifyXml(XmlDocument xmlDoc, RSA rsKey, ref string strResult)
    {
        bool flag_SuccessFail = false;
        try
        {
            if (xmlDoc == null)
                strResult = "Xml file is null";
            if (rsKey == null)
                strResult = "Key is null";
            // Format the document to ignore white spaces.
            xmlDoc.PreserveWhitespace = true;
            // Create a new SignedXml object and pass it
            // the XML document class.
            SignedXml signedXml = new SignedXml(xmlDoc);
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Signature");

            if (nodeList.Count <= 0)
            {
                strResult = "Verification failed: No Signature was found in the document.";
            }

            if (nodeList.Count >= 2)
            {
                throw new CryptographicException("Verification failed: More that one signature was found for the document.");
            }


            signedXml.LoadXml((XmlElement)nodeList[0]);
            if (signedXml.CheckSignature(rsKey))
            {
                flag_SuccessFail = true;
            }
            else
            {
                flag_SuccessFail = false;
            }
            return flag_SuccessFail;
        }
        catch (Exception ex)
        {
            return flag_SuccessFail;
        }
    }
    public static int SADPenaltyCorrectionChargeDownloadFiles(long lng_ApplicationCode, int PenaltySN, int SN)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.PenaltyCorrectionCharges.SADPenaltyCorrectionChargesRec obj_PenaltyCorrectionChargesRec = new NOCAP.BLL.PenaltyCorrectionCharges.SADPenaltyCorrectionChargesRec();
            obj_PenaltyCorrectionChargesRec = obj_PenaltyCorrectionChargesRec.DownloadPenaltyCorrectionChargesRec(lng_ApplicationCode, PenaltySN, SN);


            if (obj_PenaltyCorrectionChargesRec != null)
            {
                byte[] bytes = obj_PenaltyCorrectionChargesRec.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = obj_PenaltyCorrectionChargesRec.ContentType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "SADPenaltyCorrectionChargesRec_" + Convert.ToString(lng_ApplicationCode) + "_" + obj_PenaltyCorrectionChargesRec.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();

                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }

    }
    public static int SADGroundWaterChargeDownloadFiles(long lng_ApplicationCode, int SN)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.GroundWaterChargeRec.SADGroundWaterChargesRec obj_GroundWaterChargesRec = new NOCAP.BLL.GroundWaterChargeRec.SADGroundWaterChargesRec();
            obj_GroundWaterChargesRec = obj_GroundWaterChargesRec.DownloadGroundWaterChargeRec(lng_ApplicationCode, SN);
            if (obj_GroundWaterChargesRec != null)
            {
                byte[] bytes = obj_GroundWaterChargesRec.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = obj_GroundWaterChargesRec.ContentType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "GroundWaterChargesRec_" + Convert.ToString(lng_ApplicationCode) + "_" + obj_GroundWaterChargesRec.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();

                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }

    }
    public static int SADAppFeeDownloadFiles(long lng_ApplicationCode, int SN)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus obj_SADProcessingFeeStatus = new NOCAP.BLL.ProcessingFee.SADProcessingFeeStatus();
            obj_SADProcessingFeeStatus = obj_SADProcessingFeeStatus.DownloadApplicationFee(lng_ApplicationCode, SN);
            if (obj_SADProcessingFeeStatus != null)
            {
                byte[] bytes = obj_SADProcessingFeeStatus.AttFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = obj_SADProcessingFeeStatus.ContentType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "GroundWaterChargesRec_" + Convert.ToString(lng_ApplicationCode) + "_" + obj_SADProcessingFeeStatus.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();

                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }

    }
    public static string CreateXMLForApplication(long ExternalUserCode, Dictionary<int, decimal> obj_Dictionary, 
        decimal decA_GWAmountValue, decimal? ArearAmount, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode enu, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode enu2, ref string OrderPaymentCode, NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null, NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = null, NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = null, NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = null, NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = null, NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = null)
    {

        NOCAP.BLL.Misc.Payment.OnlinePaymentDetails obj_OnlinePaymentDetails = new NOCAP.BLL.Misc.Payment.OnlinePaymentDetails(); ;

        NOCAP.BLL.Misc.Payment.OnlinePaymentDetails[] arr_OnlinePaymentDetails = null;

        long ApplicationCode = 0;
        string ShopperEmailAddress = "", Address1 = "", Address2 = "", PostalCode = "", City = "", State = "", BillMobileNumber = "";
        if (obj_IndustrialNewApplication != null)
        {
            ApplicationCode = obj_IndustrialNewApplication.IndustrialNewApplicationCode;
            ShopperEmailAddress = obj_IndustrialNewApplication.CommunicationEmailID;
            Address1 = obj_IndustrialNewApplication.CommunicationAddress.AddressLine1;
            Address2 = obj_IndustrialNewApplication.CommunicationAddress.AddressLine2;
            PostalCode = ((int)obj_IndustrialNewApplication.CommunicationAddress.PinCode).ToString();
            City = new NOCAP.BLL.Master.SubDistrict(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;
            State = new NOCAP.BLL.Master.State(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
            BillMobileNumber = obj_IndustrialNewApplication.CommunicationMobileNumber.MobileNumberRest.ToString();

        }
        else if (obj_IndustrialRenewApplication != null)
        {
            obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_IndustrialRenewApplication.FirstApplicationCode);
            ApplicationCode = obj_IndustrialRenewApplication.IndustrialRenewApplicationCode;
            ShopperEmailAddress = obj_IndustrialRenewApplication.CommunicationEmailID;
            Address1 = obj_IndustrialRenewApplication.CommunicationAddress.AddressLine1;
            Address2 = obj_IndustrialRenewApplication.CommunicationAddress.AddressLine2;
            PostalCode = ((int)obj_IndustrialRenewApplication.CommunicationAddress.PinCode).ToString();
            BillMobileNumber = obj_IndustrialRenewApplication.CommunicationMobileNumber.MobileNumberRest.ToString();


            City = new NOCAP.BLL.Master.SubDistrict(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;
            State = new NOCAP.BLL.Master.State(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;


        }
        else if (obj_InfrastructureNewApplication != null)
        {
            ApplicationCode = obj_InfrastructureNewApplication.InfrastructureNewApplicationCode;
            ShopperEmailAddress = obj_InfrastructureNewApplication.CommunicationEmailID;
            Address1 = obj_InfrastructureNewApplication.CommunicationAddress.AddressLine1;
            Address2 = obj_InfrastructureNewApplication.CommunicationAddress.AddressLine2;
            PostalCode = ((int)obj_InfrastructureNewApplication.CommunicationAddress.PinCode).ToString();
            BillMobileNumber = obj_InfrastructureNewApplication.CommunicationMobileNumber.MobileNumberRest.ToString();
            City = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;
            State = new NOCAP.BLL.Master.State(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;


        }
        else if (obj_InfrastructureRenewApplication != null)
        {
            obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_InfrastructureRenewApplication.FirstApplicationCode);
            ApplicationCode = obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode;
            ShopperEmailAddress = obj_InfrastructureRenewApplication.CommunicationEmailID;
            Address1 = obj_InfrastructureRenewApplication.CommunicationAddress.AddressLine1;
            Address2 = obj_InfrastructureRenewApplication.CommunicationAddress.AddressLine2;
            PostalCode = ((int)obj_InfrastructureRenewApplication.CommunicationAddress.PinCode).ToString();
            BillMobileNumber = obj_InfrastructureRenewApplication.CommunicationMobileNumber.MobileNumberRest.ToString();
            City = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;
            State = new NOCAP.BLL.Master.State(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;



        }
        else if (obj_MiningNewApplication != null)
        {
            ApplicationCode = obj_MiningNewApplication.ApplicationCode;
            ShopperEmailAddress = obj_MiningNewApplication.CommunicationEmailID;
            Address1 = obj_MiningNewApplication.CommunicationAddress.AddressLine1;
            Address2 = obj_MiningNewApplication.CommunicationAddress.AddressLine2;
            PostalCode = ((int)obj_MiningNewApplication.CommunicationAddress.PinCode).ToString();
            BillMobileNumber = obj_MiningNewApplication.CommunicationMobileNumber.MobileNumberRest.ToString();
            City = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;
            State = new NOCAP.BLL.Master.State(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;


        }
        else
        {
            obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_MiningRenewApplication.FirstApplicationCode);
            ApplicationCode = obj_MiningRenewApplication.MiningRenewApplicationCode;
            ShopperEmailAddress = obj_MiningRenewApplication.CommunicationEmailID;
            Address1 = obj_MiningRenewApplication.CommunicationAddress.AddressLine1;
            Address2 = obj_MiningRenewApplication.CommunicationAddress.AddressLine2;
            PostalCode = ((int)obj_MiningRenewApplication.CommunicationAddress.PinCode).ToString();
            BillMobileNumber = obj_MiningRenewApplication.CommunicationMobileNumber.MobileNumberRest.ToString();
            City = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;
            State = new NOCAP.BLL.Master.State(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;



        }
        decimal totalAmount = 0;

        foreach (KeyValuePair<int, decimal> dict in obj_Dictionary)
        {
            totalAmount += dict.Value;
        }
        NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(ExternalUserCode);
        //Create SADOnlinePayment i.e. main
        NOCAP.BLL.Misc.Payment.OnlinePayment obj_OnlinePayment = OnlinePayment(ApplicationCode, totalAmount, ExternalUserCode, obj_Dictionary, decA_GWAmountValue, ArearAmount, enu, enu2, obj_Dictionary.Count());

        // NOCAP.BLL.Master.NTRPMapping obj_NTRPMapping = new NOCAP.BLL.Master.NTRPMapping();
        // obj_NTRPMapping.GetALL();
        //  NOCAP.BLL.Master.NTRPMapping[] arr_NTRPMapping = obj_NTRPMapping.NTRPMappingCollection;
        // NOCAP.BLL.Master.NTRPMapping obj_nTRPMapping = null;


        string strOnlonePaymentXMLDetail = null;
        try
        {
            //if (obj_OnlinePayment != null)
            //{
            //sbOnlinePaymentXMLDetail.AppendLine("</SADOnlinePaymentDetails>");
            // }
            obj_OnlinePaymentDetails.OrderPaymentCode = obj_OnlinePayment.OrderPaymentCode;
            obj_OnlinePaymentDetails.GetALL();
            arr_OnlinePaymentDetails = obj_OnlinePaymentDetails.OnlinePaymentDetailsCollection;
            OrderPaymentCode = obj_OnlinePayment.OrderPaymentCode;



            // obj_NTRPMapping = new NOCAP.BLL.Master.NTRPMapping(14794, 1);
            StringBuilder builder = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(builder);
            //Start XM DOcument
            writer.WriteStartDocument(true);
            // writer.Formatting = Formatting.Indented;
            //writer.Indentation = 2;

            #region ROOT
            //ROOT Element
            writer.WriteStartElement("BharatKoshPayment");
            writer.WriteAttributeString("DepartmentCode", ConfigurationManager.AppSettings["DepartmentCode"]);
            writer.WriteAttributeString("Version", "1.0");

            #region Submit
            //Submit Element
            writer.WriteStartElement("Submit");
            #region OrderBatch
            //Start OrderBatch
            writer.WriteStartElement("OrderBatch");
            writer.WriteAttributeString("TotalAmount", obj_OnlinePayment.TotalAmount.ToString());
            writer.WriteAttributeString("Transactions", obj_OnlinePayment.Transactions.ToString());
            writer.WriteAttributeString("merchantBatchCode", obj_OnlinePayment.OrderPaymentCodeForNTRP);
            #region Order
            for (int i = 0; i < arr_OnlinePaymentDetails.Length; i++)
            {
                //Start Order
                writer.WriteStartElement("Order");
                writer.WriteAttributeString("InstallationId", ConfigurationManager.AppSettings["InstallationId"]);
                if (i == 0)
                    writer.WriteAttributeString("OrderCode", obj_OnlinePayment.OrderPaymentCodeForNTRP);
                else
                    writer.WriteAttributeString("OrderCode", arr_OnlinePaymentDetails[i].OrderPaymentCodeForNTRPOrder);

                #region CartDetails
                //Start CartDetails
                writer.WriteStartElement("CartDetails");

                //Start Description
                writer.WriteStartElement("Description");
                // writer.WriteString(new NOCAP.BLL.Master.WaterChargeType(obj_OnlinePaymentDetails.water).WaterChargeTypeDesc);
                writer.WriteEndElement();     //Description Element


                //Start Amount
                writer.WriteStartElement("Amount");
                if (arr_OnlinePaymentDetails[i].PaymentTypeCode == 2 || arr_OnlinePaymentDetails[i].PaymentTypeCode == 3)
                {
                    if (arr_OnlinePaymentDetails[i].ArearAmount == null)
                        writer.WriteAttributeString("value", (arr_OnlinePaymentDetails[i].AmountValue).ToString());
                    else
                        writer.WriteAttributeString("value", (arr_OnlinePaymentDetails[i].AmountValue + arr_OnlinePaymentDetails[i].ArearAmount).ToString());
                }
                else
                    writer.WriteAttributeString("value", arr_OnlinePaymentDetails[i].AmountValue.ToString());
                writer.WriteAttributeString("exponent", "0");
                writer.WriteAttributeString("CurrencyCode", ConfigurationManager.AppSettings["CurrencyCode"]);

                writer.WriteEndElement();     //Amount Element

                //Start Amount
                writer.WriteStartElement("OrderContent");
                writer.WriteString(arr_OnlinePaymentDetails[i].OrderContent.ToString());
                writer.WriteEndElement();     //OrderContent Element

                //Start PaymentTypeId
                writer.WriteStartElement("PaymentTypeId");
                writer.WriteString(arr_OnlinePaymentDetails[i].PaymentTypeId.ToString());
                writer.WriteString("");
                writer.WriteEndElement();     //PaymentTypeId Element


                //Start PAOCode
                writer.WriteStartElement("PAOCode");
                writer.WriteString(ConfigurationManager.AppSettings["PAOCode"]);// obj_OnlinePaymentDetails.PAOCode);//imran
                writer.WriteEndElement();     //PAOCode Element

                //Start DDOCode
                writer.WriteStartElement("DDOCode");
                writer.WriteString(ConfigurationManager.AppSettings["DDOCode"]);
                writer.WriteEndElement();     //DDCode Element

                writer.WriteEndElement();     //CartDetails Element
                #endregion

                #region aymentMethodMask
                //Start PaymentMethodMask
                writer.WriteStartElement("PaymentMethodMask");

                //Start Include
                writer.WriteStartElement("Include");
                switch (obj_OnlinePayment.PaymentMethodMode)
                {
                    case NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine:
                        writer.WriteAttributeString("Code", "OffLine");
                        break;
                    case NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine:
                        writer.WriteAttributeString("Code", "OnLine");
                        break;
                    case NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.NotDefined:
                        writer.WriteAttributeString("Code", "");
                        break;
                }

                writer.WriteEndElement();     //Include Element
                writer.WriteEndElement();     //PaymentMethodMask Element
                #endregion


                #region Shopper
                //Start Shopper
                writer.WriteStartElement("Shopper");

                //Start Include
                writer.WriteStartElement("ShopperEmailAddress");
                writer.WriteString(ShopperEmailAddress);
                writer.WriteEndElement();     //ShopperEmailAddress Element
                writer.WriteEndElement();     //Shopper Element
                #endregion

                #region Shipping Address
                //Start ShippingAddress
                writer.WriteStartElement("ShippingAddress");
                //Start Address
                writer.WriteStartElement("Address");

                //Start FirstName
                writer.WriteStartElement("FirstName");
                writer.WriteString(obj_ExternalUser.ExternalUserFirstName);
                writer.WriteEndElement();     //FirstName Element
                                              //Start LastName
                writer.WriteStartElement("LastName");

                writer.WriteString(obj_ExternalUser.ExternalUserLastName);
                writer.WriteEndElement();     //LastName Element
                                              //Start Address1
                writer.WriteStartElement("Address1");
                writer.WriteString(Address1);
                writer.WriteEndElement();     //Address1 Element
                                              //Start Address2
                writer.WriteStartElement("Address2");
                writer.WriteString(Address2);
                writer.WriteEndElement();     //Address2 Element

                //Start PostalCode
                writer.WriteStartElement("PostalCode");
                writer.WriteString(PostalCode);
                writer.WriteEndElement();     //PostalCode Element

                //Start City
                writer.WriteStartElement("City");
                writer.WriteString(City);

                writer.WriteEndElement();     //City Element

                //Start StateRegion
                writer.WriteStartElement("StateRegion");
                writer.WriteString(State);
                writer.WriteEndElement();     //StateRegion Element
                                              //Start State
                writer.WriteStartElement("State");
                writer.WriteString(State);
                writer.WriteEndElement();     //State Element

                //Start CountryCode
                writer.WriteStartElement("CountryCode");
                writer.WriteString("INDIA");
                writer.WriteEndElement();     //CountryCode Element


                //Start MobileNumber
                writer.WriteStartElement("MobileNumber");
                writer.WriteString(BillMobileNumber);
                writer.WriteEndElement();     //MobileNumber Element

                writer.WriteEndElement();     //Address Element
                writer.WriteEndElement();     //ShippingAddress Element
                #endregion

                #region Billing Address
                //Start BillingAddress
                writer.WriteStartElement("BillingAddress");
                //Start Address
                writer.WriteStartElement("Address");

                //Start FirstName
                writer.WriteStartElement("FirstName");

                writer.WriteString(obj_ExternalUser.ExternalUserFirstName);
                writer.WriteEndElement();     //FirstName Element
                                              //Start LastName
                writer.WriteStartElement("LastName");
                writer.WriteString(obj_ExternalUser.ExternalUserLastName);
                writer.WriteEndElement();     //LastName Element
                                              //Start Address1
                writer.WriteStartElement("Address1");

                writer.WriteString(Address1);


                writer.WriteEndElement();     //Address1 Element
                                              //Start Address2
                writer.WriteStartElement("Address2");

                writer.WriteString(Address2);

                writer.WriteEndElement();     //Address2 Element

                //Start PostalCode
                writer.WriteStartElement("PostalCode");

                writer.WriteString(PostalCode);

                writer.WriteEndElement();     //PostalCode Element

                //Start City
                writer.WriteStartElement("City");
                writer.WriteString(City);
                writer.WriteEndElement();     //City Element

                //Start StateRegion
                writer.WriteStartElement("StateRegion");
                writer.WriteString(State);
                writer.WriteEndElement();     //StateRegion Element
                                              //Start State
                writer.WriteStartElement("State");
                writer.WriteString(State);
                writer.WriteEndElement();     //State Element

                //Start CountryCode
                writer.WriteStartElement("CountryCode");
                writer.WriteString("INDIA");
                writer.WriteEndElement();     //CountryCode Element


                //Start MobileNumber
                writer.WriteStartElement("MobileNumber");
                writer.WriteString(BillMobileNumber);
                writer.WriteEndElement();     //MobileNumber Element

                writer.WriteEndElement();     //Address Element
                writer.WriteEndElement();     //BillingAddress Element
                #endregion

                #region StatementNarrative
                //Start StatementNarrative
                writer.WriteStartElement("StatementNarrative");
                writer.WriteEndElement();     //StatementNarrative Element
                #endregion

                #region Remarks
                //Start Remarks
                writer.WriteStartElement("Remarks");
                writer.WriteEndElement();     //Remarks Element
                #endregion

                writer.WriteEndElement();     //Order Element
                #endregion
            }




            writer.WriteEndElement();     //OrderBatch Element
            #endregion
            writer.WriteEndElement();     //Submit Element
            #endregion
            writer.WriteEndElement();     //ROOT Element
            #endregion

            //End XML Document
            writer.WriteEndDocument();

            //Close writer
            writer.Close();
            return strOnlonePaymentXMLDetail = builder.ToString();//.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "<?xml version=\"1.0\"?>");
        }
        catch (Exception ex)
        { return strOnlonePaymentXMLDetail; }
    }
    public static NOCAP.BLL.Misc.Payment.OnlinePayment OnlinePayment(long ApplicationCode, decimal TotalAmount, long ExternalUserCode, 
        Dictionary<int, decimal> obj_Dictionary, decimal decA_GWAmountValue, decimal? decA_ArearAmount, 
        NOCAP.BLL.Common.CommonEnum.PaymentMethodMode enu, NOCAP.BLL.Common.CommonEnum.PaymentTypeMode enu2, int Transactions)
    {
        StringBuilder sbOnlinePaymentXMLDetail = new StringBuilder();
        NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(ExternalUserCode);
        NOCAP.BLL.Misc.Payment.OnlinePayment obj_OnlinePayment = new NOCAP.BLL.Misc.Payment.OnlinePayment();
        obj_OnlinePayment.ApplicationCode = ApplicationCode;
        obj_OnlinePayment.PaymentMethodMode = enu;
        obj_OnlinePayment.PaymentTypeMode = enu2;
        obj_OnlinePayment.Transactions = Transactions;
        obj_OnlinePayment.TotalAmount = TotalAmount;
        obj_OnlinePayment.CreatedByExUC = ExternalUserCode;

        sbOnlinePaymentXMLDetail.AppendLine("<?xml version=\"1.0\" ?>");
        // sbOnlinePaymentXMLDetail.AppendLine("<SADOnlinePaymentDetails>");
        NOCAP.BLL.Master.NTRPMapping obj_nTRPMapping = null;
        foreach (KeyValuePair<int, decimal> dict in obj_Dictionary)
        {
            obj_nTRPMapping = new NOCAP.BLL.Master.NTRPMapping(Convert.ToInt32(ConfigurationManager.AppSettings["OrderContent"].ToString()), Convert.ToInt32(dict.Key.ToString()));


            sbOnlinePaymentXMLDetail.AppendLine("<OnlinePaymentDetails>");
            // sbOnlinePaymentXMLDetail.AppendLine("<AppCode>" + Convert.ToString(obj_OnlinePayment.ApplicationCode) + "</AppCode>");
            //sbOnlinePaymentXMLDetail.AppendLine("<OrderPaymentCode>" + Convert.ToString(obj_OnlinePayment.OrderPaymentCode) + "</OrderPaymentCode>");
            sbOnlinePaymentXMLDetail.AppendLine("<PaymentTypeCode>" + dict.Key.ToString() + "</PaymentTypeCode>");
            sbOnlinePaymentXMLDetail.AppendLine("<PaymentTypeId>" + obj_nTRPMapping.PaymentTypeId.ToString() + "</PaymentTypeId>");
            if (Convert.ToInt32(dict.Key.ToString()) == 2 || Convert.ToInt32(dict.Key.ToString()) == 3)
            {
                sbOnlinePaymentXMLDetail.AppendLine("<AmountValue>" + decA_GWAmountValue.ToString() + "</AmountValue>");
                sbOnlinePaymentXMLDetail.AppendLine("<ArearAmount>" + decA_ArearAmount.ToString() + "</ArearAmount>");
            }
            else
                sbOnlinePaymentXMLDetail.AppendLine("<AmountValue>" + dict.Value.ToString() + "</AmountValue>");
            sbOnlinePaymentXMLDetail.AppendLine("<OrderContent>" + ConfigurationManager.AppSettings["OrderContent"].ToString() + "</OrderContent>");

            sbOnlinePaymentXMLDetail.AppendLine("</OnlinePaymentDetails>");
        }
        //sbOnlinePaymentXMLDetail.AppendLine("</SADOnlinePaymentDetail>");
        if (obj_OnlinePayment.Add(sbOnlinePaymentXMLDetail) == 1)
        {
            obj_OnlinePayment = new NOCAP.BLL.Misc.Payment.OnlinePayment(obj_OnlinePayment.ApplicationCode, obj_OnlinePayment.OrderPaymentCode);
        }
        else
        {
            obj_OnlinePayment = null;
        }
        return obj_OnlinePayment;
    }


    public static int BindGridViewPenaltyCorrImposeDetail(ref GridView gv_penaltyCorrectionRightGridView, ref GridView gvRightCorrectionChargeSingleTr, long lng_appCode, int int_penaltySn)
    {
        int int_status = 0;
        string strA_custumMessage;
        NOCAP.BLL.Master.CorrectionCharge obj_correctionCharge = new NOCAP.BLL.Master.CorrectionCharge();
        NOCAP.BLL.Master.CorrectionCharge[] arr_correctionCharge;
        NOCAP.BLL.PenaltyImpose.PenaltyCorrImposeDetail obj_PenaltyCorrImposeDetail = new NOCAP.BLL.PenaltyImpose.PenaltyCorrImposeDetail();
        NOCAP.BLL.PenaltyImpose.PenaltyCorrImposeDetail[] arr_PenaltyCorrImposeDetail;
        try
        {
            if (obj_correctionCharge.GetAll(NOCAP.BLL.Master.CorrectionCharge.SortingField.CorrectionChargeCode) == 1)
            {
                arr_PenaltyCorrImposeDetail = obj_PenaltyCorrImposeDetail.GetALLForKeys(out strA_custumMessage, out int_status, lng_appCode, int_penaltySn);
                arr_correctionCharge = obj_correctionCharge.CorrectionChargeCollection;

                if (arr_PenaltyCorrImposeDetail.Count() > 0)
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("CorrectionChargeCode", typeof(int));
                        dt.Columns.Add("CorrectionChargeDesc", typeof(string));
                        dt.Columns.Add("Rate", typeof(decimal));
                        dt.Columns.Add("Remark", typeof(string));
                        for (int i = 0; i < arr_PenaltyCorrImposeDetail.Length; i++)
                        {
                            DataRow newRow = dt.NewRow();
                            newRow["CorrectionChargeCode"] = arr_PenaltyCorrImposeDetail[i].CorrectionChargeCode;
                            obj_correctionCharge = new NOCAP.BLL.Master.CorrectionCharge(arr_PenaltyCorrImposeDetail[i].CorrectionChargeCode);
                            newRow["CorrectionChargeDesc"] = obj_correctionCharge.CorrectionChargeDesc;
                            newRow["Rate"] = arr_PenaltyCorrImposeDetail[i].Amount;
                            newRow["Remark"] = arr_PenaltyCorrImposeDetail[i].Remark;
                            dt.Rows.Add(newRow);
                        }
                        gv_penaltyCorrectionRightGridView.DataSource = dt;
                        gv_penaltyCorrectionRightGridView.DataBind();

                        gvRightCorrectionChargeSingleTr.DataSource = dt;
                        gvRightCorrectionChargeSingleTr.DataBind();
                        return int_status;
                    }
                    catch (Exception Ex)
                    {
                        string msg = Ex.Message;
                    }

                }
                return int_status;
            }
        }

        catch (Exception Ex)
        {
            string msg = Ex.Message;
        }
        return int_status;
    }

    public static int BindGridViewPenaltyImposeDetail(ref GridView gv_penaltyRightGridView, ref GridView gvRightSingleTr, long lng_appCode, int int_penaltySn)
    {
        int int_status = 0;
        string strA_custumMessage;
        NOCAP.BLL.Master.Penalty obj_penalty = new NOCAP.BLL.Master.Penalty();
        NOCAP.BLL.Master.Penalty[] arr_penalty;
        NOCAP.BLL.PenaltyImpose.PenaltyImposeDetail obj_PenaltyImposeDetail = new NOCAP.BLL.PenaltyImpose.PenaltyImposeDetail();
        NOCAP.BLL.PenaltyImpose.PenaltyImposeDetail[] arr_PenaltyImposeDetail;
        //arr_PenaltyImposeDetail = obj_PenaltyImposeDetail.GetALLForKeys(out strA_custumMessage, out int_status, lng_appCode, int_penaltySn);
        try
        {
            if (obj_penalty.GetAll(NOCAP.BLL.Master.Penalty.SortingField.PenaltyCode) == 1)
            {
                //if (obj_PenaltyImposeDetail.GetALL(NOCAP.BLL.PenaltyImpose.PenaltyImposeDetail.SortingField.PenaltyCode) == 1)
                //{
                arr_PenaltyImposeDetail = obj_PenaltyImposeDetail.GetALLForKeys(out strA_custumMessage, out int_status, lng_appCode, int_penaltySn);
                //arr_PenaltyImposeDetail = obj_PenaltyImposeDetail.PenaltyImposeDetailCollection;
                arr_penalty = obj_penalty.PenaltyCollection;

                if (arr_PenaltyImposeDetail.Count() > 0)
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("PenaltySN", typeof(int));
                        dt.Columns.Add("PenaltyCode", typeof(int));
                        dt.Columns.Add("PenaltyDesc", typeof(string));
                        dt.Columns.Add("Rate", typeof(decimal));
                        dt.Columns.Add("Remark", typeof(string));
                        for (int i = 0; i < arr_PenaltyImposeDetail.Length; i++)
                        {
                            DataRow newRow = dt.NewRow();
                            newRow["PenaltySN"] = arr_PenaltyImposeDetail[i].PenaltySN;
                            newRow["PenaltyCode"] = arr_PenaltyImposeDetail[i].PenaltyCode;
                            obj_penalty = new NOCAP.BLL.Master.Penalty(arr_PenaltyImposeDetail[i].PenaltyCode);
                            newRow["PenaltyDesc"] = obj_penalty.PenaltyDesc;
                            newRow["Rate"] = arr_PenaltyImposeDetail[i].Amount;
                            newRow["Remark"] = arr_PenaltyImposeDetail[i].Remark;
                            dt.Rows.Add(newRow);
                        }
                        gv_penaltyRightGridView.DataSource = dt;
                        gv_penaltyRightGridView.DataBind();

                        gvRightSingleTr.DataSource = dt;
                        gvRightSingleTr.DataBind();
                        return int_status;
                    }
                    catch (Exception Ex)
                    {
                        string msg = Ex.Message;
                    }

                }

                //}
                return int_status;
            }
        }

        catch (Exception Ex)
        {
            string msg = Ex.Message;
        }
        return int_status;
    }
    public static NOCAP.BLL.Common.CommonEnum.YesNoOption CheckPenaltyForApplication(long lngA_ApplicationCode)
    {
        NOCAP.BLL.Common.CommonEnum.YesNoOption enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplicationPrev = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplicationPrev = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication();
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplicationPrev = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplicationPrev = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication();
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplicationPrev = new NOCAP.BLL.Mining.New.Application.MiningNewApplication();
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplicationPrev = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication();


        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication();
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication();
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication();
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication();
        NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_industrialNewApplication, out obj_infrastructureNewApplication, out obj_miningNewApplication, out obj_industrialRenewApplication, out obj_infrastructureRenewApplication, out obj_miningRenewApplication, lngA_ApplicationCode);

        try
        {
            if (obj_industrialNewApplication != null && obj_industrialNewApplication.CreatedByExUC > 0)
            {

            }
            else if (obj_industrialRenewApplication != null && obj_industrialRenewApplication.CreatedByExUC > 0)
            {
                if (obj_industrialRenewApplication.GetPreviousIndustrialApplication(out obj_industrialNewApplicationPrev, out obj_industrialRenewApplicationPrev) == 1)
                {
                    if (obj_industrialNewApplicationPrev != null)
                    {
                        NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_industrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter(obj_industrialNewApplicationPrev.IndustrialNewApplicationCode);
                        if (obj_industrialNewIssusedLetter.ValidityEndDate != null)
                        {
                            if ((Convert.ToDateTime(obj_industrialNewIssusedLetter.ValidityEndDate) - System.DateTime.Today).Days < 0)
                                enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                            else
                                enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;

                        }
                    }
                    else if (obj_industrialRenewApplicationPrev != null)
                    {
                        NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter obj_industrialRenewIssusedLetter = new NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter(obj_industrialRenewApplicationPrev.IndustrialRenewApplicationCode);

                        if (obj_industrialRenewIssusedLetter.ValidityEndDate != null)
                        {
                            if ((Convert.ToDateTime(obj_industrialRenewIssusedLetter.ValidityEndDate) - System.DateTime.Today).Days < 0)
                                enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                            else
                                enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;

                        }
                    }
                }
            }
            else if (obj_infrastructureNewApplication != null && obj_infrastructureNewApplication.CreatedByExUC > 0)
            {

            }
            else if (obj_infrastructureRenewApplication != null && obj_infrastructureRenewApplication.CreatedByExUC > 0)
            {
                if (obj_infrastructureRenewApplication.GetPreviousInfrastructureApplication(out obj_infrastructureNewApplicationPrev, out obj_infrastructureRenewApplicationPrev) == 1)
                {
                    if (obj_infrastructureNewApplicationPrev != null)
                    {
                        NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_infrastructureNewIssusedLetter = new NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter(obj_infrastructureNewApplicationPrev.InfrastructureNewApplicationCode);
                        if (obj_infrastructureNewIssusedLetter.ValidityEndDate != null)
                        {
                            if ((Convert.ToDateTime(obj_infrastructureNewIssusedLetter.ValidityEndDate) - System.DateTime.Today).Days < 0)
                                enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                            else
                                enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;


                        }
                    }
                    else if (obj_infrastructureRenewApplicationPrev != null)
                    {
                        NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter obj_infrastructureRenewIssusedLetter = new NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter(obj_infrastructureRenewApplicationPrev.InfrastructureRenewApplicationCode);

                        if (obj_infrastructureRenewIssusedLetter.ValidityEndDate != null)
                        {
                            if ((Convert.ToDateTime(obj_infrastructureRenewIssusedLetter.ValidityEndDate) - System.DateTime.Today).Days < 0)
                                enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                            else
                                enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;

                        }
                    }
                }

            }
            else if (obj_miningNewApplication != null && obj_miningNewApplication.CreatedByExUC > 0)
            {

            }
            else if (obj_miningRenewApplication != null && obj_miningRenewApplication.CreatedByExUC > 0)
            {
                if (obj_miningRenewApplication.GetPreviousMiningApplication(out obj_miningNewApplicationPrev, out obj_miningRenewApplicationPrev) == 1)
                {
                    if (obj_miningNewApplicationPrev != null)
                    {
                        NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter obj_miningNewIssusedLetter = new NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter(obj_miningNewApplicationPrev.ApplicationCode);
                        if (obj_miningNewIssusedLetter.ValidityEndDate != null)
                        {
                            if ((Convert.ToDateTime(obj_miningNewIssusedLetter.ValidityEndDate) - System.DateTime.Today).Days < 0)
                                enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                            else
                                enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;

                        }
                    }
                    else if (obj_miningRenewApplicationPrev != null)
                    {
                        NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter obj_miningRenewIssusedLetter = new NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter(obj_miningRenewApplicationPrev.MiningRenewApplicationCode);

                        if (obj_miningRenewIssusedLetter.ValidityEndDate != null)
                        {
                            if ((Convert.ToDateTime(obj_miningRenewIssusedLetter.ValidityEndDate) - System.DateTime.Today).Days < 0)
                                enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
                            else
                                enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;

                        }
                    }
                }

            }
            return enm_checkPenalty;
        }
        catch (Exception ex)
        {
            enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
            return enm_checkPenalty;
        }
    }
    public static string GenerateSHA512String(string inputString)
    {
        SHA512 sha512 = SHA512Managed.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(inputString);
        byte[] hash = sha512.ComputeHash(bytes);
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; (i <= (hash.Length - 1)); i++)
        {
            stringBuilder.Append(hash[i].ToString("X2"));
        }
        return stringBuilder.ToString();

    }
    public static string GetGroundWaterChargeForSADAppCode(long lng_applicatonCode, out string str_minAmt)
    {
        decimal dec_result = 0m, dec_qty = 0m;
        int int_days = 0, int_areaTypeCode = 0, int_areaTypeCatCode, int_year = 0;
        str_minAmt = "";
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewSADApplication = null;
        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewSADApplication = null;
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewSADApplication = null;
        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplication = null;
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_miningNewSADApplication = null;
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewSADApplication = null;

        NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialExpansionApplication = null;
        NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication obj_infrastructureExpansionApplication = null;
        NOCAP.BLL.Mining.Expansion.MiningExpansionApplication obj_miningExpansionApplication = null;

        NOCAP.BLL.Master.DrinkDomCharge obj_DrinkDomCharge = null;
        NOCAP.BLL.Master.IndustrialCharge obj_IndustrialCharge = null;
        NOCAP.BLL.Master.InfrastructureCharge obj_InfrastructureCharge = null;
        NOCAP.BLL.Master.MiningCharge obj_MiningCharge = null;
        NOCAP.BLL.Master.BulkWaterCharge obj_BulkWaterCharge = null;
        NOCAP.BLL.Master.PackDrinkCharge obj_PackDrinkCharge = null;
        // NOCAP.BLL.Master.IssuLetterValidity obj_issuLetterValidity = null;
        try
        {
            NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_industrialNewSADApplication, out obj_infrastructureNewSADApplication,
               out obj_miningNewSADApplication, out obj_industrialRenewSADApplication, out obj_infrastructureRenewSADApplication, out obj_miningRenewSADApplication, lng_applicatonCode);
             //  out obj_industrialExpansionApplication, out obj_infrastructureExpansionApplication, out obj_miningExpansionApplication, lng_applicatonCode);

            //#region Domestic
            //if (obj_industrialNewSADApplication.ApplicationTypeCode == (int)NOCAP.BLL.Common.CommonEnum.ApplicationType.Domestic)
            //{
            //    obj_DrinkDomCharge = new NOCAP.BLL.Master.DrinkDomCharge(1, obj_industrialNewSADApplication.ApplicationTypeCategoryCode, dec_qty);
            //    dec_result = dec_qty * obj_DrinkDomCharge.Rate * int_days;
            //}
            //#endregion

            //#region BulkWater
            //if (obj_industrialNewSADApplication.ApplicationTypeCode == (int)NOCAP.BLL.Common.CommonEnum.ApplicationType.BulkWater)
            //{
            //    obj_BulkWaterCharge = new NOCAP.BLL.Master.BulkWaterCharge(1, obj_industrialNewSADApplication.ApplicationTypeCategoryCode, dec_qty);
            //    dec_result = dec_qty * obj_BulkWaterCharge.Rate * int_days;
            //}
            //#endregion
            #region Industrial New Application
            if (obj_industrialNewSADApplication != null && obj_industrialNewSADApplication.CreatedByExUC > 0)
            {
                int_areaTypeCode = AreaTypeCode(obj_industrialNewSADApplication, null, null);
                int_areaTypeCatCode = AreaTypeCategoryCode(obj_industrialNewSADApplication, null, null);
                dec_qty = (decimal)((obj_industrialNewSADApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 :
                    obj_industrialNewSADApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement) + (obj_industrialNewSADApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_industrialNewSADApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));
                if (obj_industrialNewSADApplication.ApplicationTypeCategoryCode == 73)
                {
                    obj_PackDrinkCharge = new NOCAP.BLL.Master.PackDrinkCharge(1, int_areaTypeCatCode, dec_qty);
                    if (obj_industrialNewSADApplication.GroundWaterReqInYear == null)
                        dec_result = 365 * obj_PackDrinkCharge.Rate;
                    else
                        dec_result = (decimal)obj_industrialNewSADApplication.GroundWaterReqInYear * obj_PackDrinkCharge.Rate;
                }
                else
                {
                    obj_IndustrialCharge = new NOCAP.BLL.Master.IndustrialCharge(1, int_areaTypeCatCode, dec_qty);
                    if (obj_industrialNewSADApplication.GroundWaterReqInYear == null)
                        dec_result = 365 * obj_IndustrialCharge.Rate;
                    else
                        dec_result = (decimal)obj_industrialNewSADApplication.GroundWaterReqInYear * obj_IndustrialCharge.Rate;
                }


            }
            #endregion

            #region Industrial Renew Application
            if (obj_industrialRenewSADApplication != null && obj_industrialRenewSADApplication.CreatedByExUC > 0)
            {
                dec_qty = (decimal)((obj_industrialRenewSADApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_industrialRenewSADApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirement) + (obj_industrialRenewSADApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_industrialRenewSADApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));

                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_industrialRenewSADApplication.FirstApplicationCode);
                int_areaTypeCode = AreaTypeCodeForAppCode(obj_industrialNewApplication);
                int_areaTypeCatCode = AreaTypeCategoryCodeForAppCode(obj_industrialNewApplication);

                // obj_issuLetterValidity = new NOCAP.BLL.Master.IssuLetterValidity(NOCAP.BLL.Common.IssuedLetterType.NOCLetter, int_areaTypeCode, int_areaTypeCatCode, obj_industrialRenewSADApplication.ApplicationTypeCode, obj_industrialRenewSADApplication.ApplicationPurposeCode);

                // int_year = DateTime.Now.AddMonths(obj_issuLetterValidity.NoOfMonthsForValidity).Year - DateTime.Now.Year;
                // int_days = DateTime.Now.AddMonths(obj_issuLetterValidity.NoOfMonthsForValidity).Subtract(DateTime.Now).Days;

                if (obj_industrialRenewSADApplication.ApplicationTypeCode == 73)
                {
                    obj_PackDrinkCharge = new NOCAP.BLL.Master.PackDrinkCharge(1, int_areaTypeCatCode, dec_qty);
                    if (obj_industrialRenewSADApplication.GroundWaterReqInYear == null)
                        dec_result = 365 * obj_PackDrinkCharge.Rate;
                    else
                        dec_result = (decimal)obj_industrialRenewSADApplication.GroundWaterReqInYear * obj_PackDrinkCharge.Rate;


                }
                else
                {
                    obj_IndustrialCharge = new NOCAP.BLL.Master.IndustrialCharge(1, int_areaTypeCatCode, dec_qty);
                    if (obj_industrialRenewSADApplication.GroundWaterReqInYear == null)
                        dec_result = 365 * obj_IndustrialCharge.Rate;
                    else
                        dec_result = (decimal)obj_industrialRenewSADApplication.GroundWaterReqInYear * obj_IndustrialCharge.Rate;
                }


            }
            #endregion

            #region Infrastructure New SAD Application
            if (obj_infrastructureNewSADApplication != null && obj_infrastructureNewSADApplication.CreatedByExUC > 0)
            {
                int_areaTypeCode = AreaTypeCode(null, obj_infrastructureNewSADApplication, null);
                int_areaTypeCatCode = AreaTypeCategoryCode(null, obj_infrastructureNewSADApplication, null);
                dec_qty = (decimal)((obj_infrastructureNewSADApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_infrastructureNewSADApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement) + (obj_infrastructureNewSADApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_infrastructureNewSADApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));
                obj_InfrastructureCharge = new NOCAP.BLL.Master.InfrastructureCharge(1, int_areaTypeCatCode, dec_qty);
                if (obj_infrastructureNewSADApplication.GroundWaterReqInYear == null)
                    dec_result = 365 * obj_InfrastructureCharge.Rate;
                else
                    dec_result = (decimal)obj_infrastructureNewSADApplication.GroundWaterReqInYear * obj_InfrastructureCharge.Rate;

            }
            #endregion

            #region Infrastructure Renew SAD Application
            if (obj_infrastructureRenewSADApplication != null && obj_infrastructureRenewSADApplication.CreatedByExUC > 0)
            {
                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_infrastructureRenewSADApplication.FirstApplicationCode);
                int_areaTypeCode = AreaTypeCodeForAppCode(null, obj_infrastructureNewApplication, null);
                int_areaTypeCatCode = AreaTypeCategoryCodeForAppCode(null, obj_infrastructureNewApplication, null);
                //  obj_issuLetterValidity = new NOCAP.BLL.Master.IssuLetterValidity(NOCAP.BLL.Common.IssuedLetterType.NOCLetter, int_areaTypeCode, int_areaTypeCatCode, obj_infrastructureRenewSADApplication.ApplicationTypeCode, obj_infrastructureRenewSADApplication.ApplicationPurposeCode);
                dec_qty = (decimal)((obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional == null ? 0 : obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional) + (obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));


                obj_InfrastructureCharge = new NOCAP.BLL.Master.InfrastructureCharge(1, int_areaTypeCatCode, dec_qty);
                if (obj_infrastructureRenewSADApplication.GroundWaterReqInYear == null)
                    dec_result = 365 * obj_InfrastructureCharge.Rate;
                else dec_result = (decimal)obj_infrastructureRenewSADApplication.GroundWaterReqInYear * obj_InfrastructureCharge.Rate;

            }
            #endregion

            #region Mining New SAD Application
            if (obj_miningNewSADApplication != null && obj_miningNewSADApplication.CreatedByExUC > 0)
            {
                int_areaTypeCode = AreaTypeCode(null, null, obj_miningNewSADApplication);
                int_areaTypeCatCode = AreaTypeCategoryCode(null, null, obj_miningNewSADApplication);
                dec_qty = (decimal)((obj_miningNewSADApplication.GWREquiredThroughAbstractStructure == null ? 0 : obj_miningNewSADApplication.GWREquiredThroughAbstractStructure) + (obj_miningNewSADApplication.GWRequiredThroughMiningSeeping == null ? 0 : obj_miningNewSADApplication.GWRequiredThroughMiningSeeping));
                obj_MiningCharge = new NOCAP.BLL.Master.MiningCharge(1, int_areaTypeCatCode, dec_qty);
                dec_result = (decimal)(obj_miningNewSADApplication.GroundWaterReqInYear == null ? 365 : obj_miningNewSADApplication.GroundWaterReqInYear) * obj_MiningCharge.Rate;

            }
            #endregion

            #region Mining Renew SAD Application
            if (obj_miningRenewSADApplication != null && obj_miningRenewSADApplication.CreatedByExUC > 0)
            {
                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_miningRenewSADApplication.FirstApplicationCode);
                int_areaTypeCode = AreaTypeCodeForAppCode(null, null, obj_miningNewApplication);
                int_areaTypeCatCode = AreaTypeCategoryCodeForAppCode(null, null, obj_miningNewApplication);                
                dec_qty = (decimal)((obj_miningRenewSADApplication.GWREquiredThroughAbstractStructureAdditional == null ? 0 : obj_miningRenewSADApplication.GWREquiredThroughAbstractStructureAdditional) + (obj_miningRenewSADApplication.GWREquiredThroughAbstractStructureExisting == null ? 0 : obj_miningRenewSADApplication.GWREquiredThroughAbstractStructureExisting) + (obj_miningRenewSADApplication.GWRequiredThroughMiningSeepingAdditional == null ? 0 : obj_miningRenewSADApplication.GWRequiredThroughMiningSeepingAdditional) + (obj_miningRenewSADApplication.GWRequiredThroughMiningSeepingExisting == null ? 0 : obj_miningRenewSADApplication.GWRequiredThroughMiningSeepingExisting));                
                obj_MiningCharge = new NOCAP.BLL.Master.MiningCharge(1, int_areaTypeCatCode, dec_qty);
                if (obj_miningRenewSADApplication.GroundWaterReqInYear == null)
                    dec_result = 365 * obj_MiningCharge.Rate;
                else
                    dec_result = (decimal)obj_miningRenewSADApplication.GroundWaterReqInYear * obj_MiningCharge.Rate;

            }
            #endregion



            //str_minAmt = String.Format("{0:0.00}", Convert.ToString(dec_result / int_year));
            // decimal s = dec_result / int_year;
            str_minAmt = Convert.ToString(Round(dec_result));

            // dec_result = Round(dec_result);
            return dec_result.ToString();// String.Format("{0:0.00}", Convert.ToString(dec_result));
        }
        catch (Exception ex)
        {
            return "";
        }
    }
    public static string GetGroundWaterChargeRateForSADAppCode(long lng_applicatonCode)
    {
        decimal dec_qty = 0m; string strA_Rate = "";
        int int_areaTypeCode = 0, int_areaTypeCatCode;

        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewSADApplication = null;
        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewSADApplication = null;
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewSADApplication = null;
        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplication = null;
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_miningNewSADApplication = null;
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewSADApplication = null;

        NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_industrialExpansionApplication = null;
        NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication obj_infrastructureExpansionApplication = null;
        NOCAP.BLL.Mining.Expansion.MiningExpansionApplication obj_miningExpansionApplication = null;

        NOCAP.BLL.Master.DrinkDomCharge obj_DrinkDomCharge = null;
        NOCAP.BLL.Master.IndustrialCharge obj_IndustrialCharge = null;
        NOCAP.BLL.Master.InfrastructureCharge obj_InfrastructureCharge = null;
        NOCAP.BLL.Master.MiningCharge obj_MiningCharge = null;
        NOCAP.BLL.Master.BulkWaterCharge obj_BulkWaterCharge = null;
        NOCAP.BLL.Master.PackDrinkCharge obj_PackDrinkCharge = null;
        NOCAP.BLL.Master.IssuLetterValidity obj_issuLetterValidity = null;
        try
        {
            NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_industrialNewSADApplication, out obj_infrastructureNewSADApplication,
                out obj_miningNewSADApplication, out obj_industrialRenewSADApplication, out obj_infrastructureRenewSADApplication, out obj_miningRenewSADApplication, lng_applicatonCode);
              //  out obj_industrialExpansionApplication, out obj_infrastructureExpansionApplication, out obj_miningExpansionApplication,
               

            //#region Domestic
            //if (obj_industrialNewSADApplication.ApplicationTypeCode == (int)NOCAP.BLL.Common.CommonEnum.ApplicationType.Domestic)
            //{
            //    obj_DrinkDomCharge = new NOCAP.BLL.Master.DrinkDomCharge(1, obj_industrialNewSADApplication.ApplicationTypeCategoryCode, dec_qty);
            //    dec_result = dec_qty * obj_DrinkDomCharge.Rate * int_days;
            //}
            //#endregion

            //#region BulkWater
            //if (obj_industrialNewSADApplication.ApplicationTypeCode == (int)NOCAP.BLL.Common.CommonEnum.ApplicationType.BulkWater)
            //{
            //    obj_BulkWaterCharge = new NOCAP.BLL.Master.BulkWaterCharge(1, obj_industrialNewSADApplication.ApplicationTypeCategoryCode, dec_qty);
            //    dec_result = dec_qty * obj_BulkWaterCharge.Rate * int_days;
            //}
            //#endregion
            #region Industrial New Application
            if (obj_industrialNewSADApplication != null && obj_industrialNewSADApplication.CreatedByExUC > 0)
            {
                dec_qty = (decimal)((obj_industrialNewSADApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 :
                   obj_industrialNewSADApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement) + (obj_industrialNewSADApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_industrialNewSADApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));

                int_areaTypeCode = AreaTypeCode(obj_industrialNewSADApplication, null, null);
                int_areaTypeCatCode = AreaTypeCategoryCode(obj_industrialNewSADApplication, null, null);
                if (obj_industrialNewSADApplication.ApplicationTypeCategoryCode == 73)
                {
                    obj_PackDrinkCharge = new NOCAP.BLL.Master.PackDrinkCharge(1, int_areaTypeCatCode, dec_qty);
                    strA_Rate = obj_PackDrinkCharge.Rate.ToString();
                }
                else
                {
                    obj_IndustrialCharge = new NOCAP.BLL.Master.IndustrialCharge(1, int_areaTypeCatCode, dec_qty);
                    strA_Rate = obj_IndustrialCharge.Rate.ToString();
                }


            }
            #endregion

            #region Industrial Renew Application
            if (obj_industrialRenewSADApplication != null && obj_industrialRenewSADApplication.CreatedByExUC > 0)
            {
                dec_qty = (decimal)((obj_industrialRenewSADApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_industrialRenewSADApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirement) + (obj_industrialRenewSADApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_industrialRenewSADApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));

                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_industrialRenewSADApplication.FirstApplicationCode);
                int_areaTypeCode = AreaTypeCodeForAppCode(obj_industrialNewApplication);
                int_areaTypeCatCode = AreaTypeCategoryCodeForAppCode(obj_industrialNewApplication);

                if (obj_industrialRenewSADApplication.ApplicationTypeCode == 73)
                {
                    obj_PackDrinkCharge = new NOCAP.BLL.Master.PackDrinkCharge(1, int_areaTypeCatCode, dec_qty);
                    strA_Rate = obj_PackDrinkCharge.Rate.ToString();
                }
                else
                {
                    obj_IndustrialCharge = new NOCAP.BLL.Master.IndustrialCharge(1, int_areaTypeCatCode, dec_qty);
                    strA_Rate = obj_IndustrialCharge.Rate.ToString();
                }


            }
            #endregion

            #region Infrastructure New SAD Application
            if (obj_infrastructureNewSADApplication != null && obj_infrastructureNewSADApplication.CreatedByExUC > 0)
            {
                int_areaTypeCode = AreaTypeCode(null, obj_infrastructureNewSADApplication, null);
                int_areaTypeCatCode = AreaTypeCategoryCode(null, obj_infrastructureNewSADApplication, null);
                dec_qty = (decimal)((obj_infrastructureNewSADApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_infrastructureNewSADApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement) + (obj_infrastructureNewSADApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_infrastructureNewSADApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));



                obj_InfrastructureCharge = new NOCAP.BLL.Master.InfrastructureCharge(1, int_areaTypeCatCode, dec_qty);
                strA_Rate = obj_InfrastructureCharge.Rate.ToString();

            }
            #endregion

            #region Infrastructure Renew SAD Application
            if (obj_infrastructureRenewSADApplication != null && obj_infrastructureRenewSADApplication.CreatedByExUC > 0)
            {
                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_infrastructureRenewSADApplication.FirstApplicationCode);
                int_areaTypeCode = AreaTypeCodeForAppCode(null, obj_infrastructureNewApplication, null);
                int_areaTypeCatCode = AreaTypeCategoryCodeForAppCode(null, obj_infrastructureNewApplication, null);
                dec_qty = (decimal)((obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional == null ? 0 : obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional) + (obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_infrastructureRenewSADApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));
                obj_InfrastructureCharge = new NOCAP.BLL.Master.InfrastructureCharge(1, int_areaTypeCatCode, dec_qty);
                strA_Rate = obj_InfrastructureCharge.Rate.ToString();

            }
            #endregion

            #region Mining New SAD Application
            if (obj_miningNewSADApplication != null && obj_miningNewSADApplication.CreatedByExUC > 0)
            {
                int_areaTypeCode = AreaTypeCode(null, null, obj_miningNewSADApplication);
                int_areaTypeCatCode = AreaTypeCategoryCode(null, null, obj_miningNewSADApplication);
                dec_qty = (decimal)((obj_miningNewSADApplication.GWREquiredThroughAbstractStructure == null ? 0 : obj_miningNewSADApplication.GWREquiredThroughAbstractStructure)
                    + (obj_miningNewSADApplication.GWRequiredThroughMiningSeeping == null ? 0 : obj_miningNewSADApplication.GWRequiredThroughMiningSeeping));
                obj_MiningCharge = new NOCAP.BLL.Master.MiningCharge(1, int_areaTypeCatCode, dec_qty);
                strA_Rate = obj_MiningCharge.Rate.ToString();

            }
            #endregion

            #region Mining Renew SAD Application
            if (obj_miningRenewSADApplication != null && obj_miningRenewSADApplication.CreatedByExUC > 0)
            {
                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_miningRenewSADApplication.FirstApplicationCode);
                int_areaTypeCode = AreaTypeCodeForAppCode(null, null, obj_miningNewApplication);
                int_areaTypeCatCode = AreaTypeCategoryCodeForAppCode(null, null, obj_miningNewApplication);
                dec_qty = (decimal)((obj_miningRenewSADApplication.GWREquiredThroughAbstractStructureAdditional == null ? 0 : obj_miningRenewSADApplication.GWREquiredThroughAbstractStructureAdditional)
                     + (obj_miningRenewSADApplication.GWREquiredThroughAbstractStructureExisting == null ? 0 : obj_miningRenewSADApplication.GWREquiredThroughAbstractStructureExisting)
                     + (obj_miningRenewSADApplication.GWRequiredThroughMiningSeepingAdditional == null ? 0 : obj_miningRenewSADApplication.GWRequiredThroughMiningSeepingAdditional)
                     + (obj_miningRenewSADApplication.GWRequiredThroughMiningSeepingExisting == null ? 0 : obj_miningRenewSADApplication.GWRequiredThroughMiningSeepingExisting)
                     );
                obj_MiningCharge = new NOCAP.BLL.Master.MiningCharge(1, int_areaTypeCatCode, dec_qty);
                strA_Rate = obj_MiningCharge.Rate.ToString();

            }
            #endregion



            return strA_Rate;
        }
        catch (Exception ex)
        {
            return "";
        }
    }


    public static string GetGroundWaterChargeForAppCode(long lng_applicatonCode, out string str_minAmt)
    {
        decimal dec_result = 0m, dec_qty = 0m;
        int int_days = 0, int_areaTypeCode = 0, int_areaTypeCatCode, int_year = 0;
        str_minAmt = "";
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = null;
        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = null;
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = null;
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = null;
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = null;
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplication = null;

        NOCAP.BLL.Master.DrinkDomCharge obj_DrinkDomCharge = null;
        NOCAP.BLL.Master.IndustrialCharge obj_IndustrialCharge = null;
        NOCAP.BLL.Master.InfrastructureCharge obj_InfrastructureCharge = null;
        NOCAP.BLL.Master.MiningCharge obj_MiningCharge = null;
        NOCAP.BLL.Master.BulkWaterCharge obj_BulkWaterCharge = null;
        NOCAP.BLL.Master.PackDrinkCharge obj_PackDrinkCharge = null;
        NOCAP.BLL.Master.IssuLetterValidity obj_issuLetterValidity = null;
        try
        {
            NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_industrialNewApplication, out obj_infrastructureNewApplication, out obj_miningNewApplication, out obj_industrialRenewApplication, out obj_infrastructureRenewApplication, out obj_miningRenewApplication, lng_applicatonCode);

            //#region Domestic
            //if (obj_industrialNewApplication.ApplicationTypeCode == (int)NOCAP.BLL.Common.CommonEnum.ApplicationType.Domestic)
            //{
            //    obj_DrinkDomCharge = new NOCAP.BLL.Master.DrinkDomCharge(1, obj_industrialNewApplication.ApplicationTypeCategoryCode, dec_qty);
            //    dec_result = dec_qty * obj_DrinkDomCharge.Rate * int_days;
            //}
            //#endregion

            //#region BulkWater
            //if (obj_industrialNewApplication.ApplicationTypeCode == (int)NOCAP.BLL.Common.CommonEnum.ApplicationType.BulkWater)
            //{
            //    obj_BulkWaterCharge = new NOCAP.BLL.Master.BulkWaterCharge(1, obj_industrialNewApplication.ApplicationTypeCategoryCode, dec_qty);
            //    dec_result = dec_qty * obj_BulkWaterCharge.Rate * int_days;
            //}
            //#endregion
            #region Industrial New Application
            if (obj_industrialNewApplication != null && obj_industrialNewApplication.CreatedByExUC > 0)
            {
                int_areaTypeCode = AreaTypeCodeForAppCode(obj_industrialNewApplication, null, null);
                int_areaTypeCatCode = AreaTypeCategoryCodeForAppCode(obj_industrialNewApplication, null, null);
                obj_issuLetterValidity = new NOCAP.BLL.Master.IssuLetterValidity(NOCAP.BLL.Common.IssuedLetterType.NOCLetter, int_areaTypeCode, int_areaTypeCatCode, obj_industrialNewApplication.ApplicationTypeCode, obj_industrialNewApplication.ApplicationPurposeCode);// purpose code 1 is for new and for 36 months                
                dec_qty = (decimal)((obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement) + (obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_industrialNewApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));
                int_year = DateTime.Now.AddMonths(obj_issuLetterValidity.NoOfMonthsForValidity).Year - DateTime.Now.Year;
                int_days = DateTime.Now.AddMonths(obj_issuLetterValidity.NoOfMonthsForValidity).Subtract(DateTime.Now).Days;

                if (obj_industrialNewApplication.ApplicationTypeCategoryCode == 73)
                {
                    obj_PackDrinkCharge = new NOCAP.BLL.Master.PackDrinkCharge(1, int_areaTypeCatCode, dec_qty);
                    dec_result = dec_qty * obj_PackDrinkCharge.Rate * int_days;
                }
                else
                {
                    obj_IndustrialCharge = new NOCAP.BLL.Master.IndustrialCharge(1, int_areaTypeCatCode, dec_qty);
                    dec_result = dec_qty * obj_IndustrialCharge.Rate * int_days;
                }


            }
            #endregion

            #region Industrial Renew Application
            if (obj_industrialRenewApplication != null && obj_industrialRenewApplication.CreatedByExUC > 0)
            {
                dec_qty = (decimal)((obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirement) + (obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_industrialRenewApplication.IndustrialRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));
                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_industrialRenewApplication.FirstApplicationCode);
                int_areaTypeCode = AreaTypeCodeForAppCode(obj_IndustrialNewApplication, null, null);
                int_areaTypeCatCode = AreaTypeCategoryCodeForAppCode(obj_IndustrialNewApplication, null, null);

                obj_issuLetterValidity = new NOCAP.BLL.Master.IssuLetterValidity(NOCAP.BLL.Common.IssuedLetterType.NOCLetter, int_areaTypeCode, int_areaTypeCatCode, obj_industrialRenewApplication.ApplicationTypeCode, 1);

                int_year = DateTime.Now.AddMonths(obj_issuLetterValidity.NoOfMonthsForValidity).Year - DateTime.Now.Year;
                int_days = DateTime.Now.AddMonths(obj_issuLetterValidity.NoOfMonthsForValidity).Subtract(DateTime.Now).Days;

                if (obj_industrialRenewApplication.ApplicationTypeCode == 73)
                {
                    obj_PackDrinkCharge = new NOCAP.BLL.Master.PackDrinkCharge(1, int_areaTypeCatCode, dec_qty);
                    dec_result = dec_qty * obj_PackDrinkCharge.Rate * int_days;
                }
                else
                {
                    obj_IndustrialCharge = new NOCAP.BLL.Master.IndustrialCharge(1, int_areaTypeCatCode, dec_qty);
                    dec_result = dec_qty * obj_IndustrialCharge.Rate * int_days;
                }


            }
            #endregion

            #region Infrastructure New  Application
            if (obj_infrastructureNewApplication != null && obj_infrastructureNewApplication.CreatedByExUC > 0)
            {
                int_areaTypeCode = AreaTypeCodeForAppCode(null, obj_infrastructureNewApplication, null);
                int_areaTypeCatCode = AreaTypeCategoryCodeForAppCode(null, obj_infrastructureNewApplication);
                obj_issuLetterValidity = new NOCAP.BLL.Master.IssuLetterValidity(NOCAP.BLL.Common.IssuedLetterType.NOCLetter, int_areaTypeCode, int_areaTypeCatCode, obj_infrastructureNewApplication.ApplicationTypeCode, obj_infrastructureNewApplication.ApplicationPurposeCode);
                dec_qty = (decimal)((obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement == null ? 0 : obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement) + (obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_infrastructureNewApplication.InfrastructurNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));

                int_year = DateTime.Now.AddMonths(obj_issuLetterValidity.NoOfMonthsForValidity).Year - DateTime.Now.Year;
                int_days = DateTime.Now.AddMonths(obj_issuLetterValidity.NoOfMonthsForValidity).Subtract(DateTime.Now).Days;


                obj_InfrastructureCharge = new NOCAP.BLL.Master.InfrastructureCharge(1, int_areaTypeCatCode, dec_qty);
                dec_result = dec_qty * obj_InfrastructureCharge.Rate * int_days;

            }
            #endregion

            #region Infrastructure Renew  Application
            if (obj_infrastructureRenewApplication != null && obj_infrastructureRenewApplication.CreatedByExUC > 0)
            {
                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_infrastructureRenewApplication.FirstApplicationCode);
                int_areaTypeCode = AreaTypeCodeForAppCode(null, obj_infrastructureNewApplication, null);
                int_areaTypeCatCode = AreaTypeCategoryCodeForAppCode(null, obj_InfrastructureNewApplication, null);
                obj_issuLetterValidity = new NOCAP.BLL.Master.IssuLetterValidity(NOCAP.BLL.Common.IssuedLetterType.NOCLetter, int_areaTypeCode, int_areaTypeCatCode, obj_infrastructureRenewApplication.ApplicationTypeCode, 1);
                dec_qty = (decimal)((obj_infrastructureRenewApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional == null ? 0 : obj_infrastructureRenewApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementAdditional) + (obj_infrastructureRenewApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist == null ? 0 : obj_infrastructureRenewApplication.InfrastructureRenewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));
                int_year = DateTime.Now.AddMonths(obj_issuLetterValidity.NoOfMonthsForValidity).Year - DateTime.Now.Year;
                int_days = DateTime.Now.AddMonths(obj_issuLetterValidity.NoOfMonthsForValidity).Subtract(DateTime.Now).Days;


                obj_InfrastructureCharge = new NOCAP.BLL.Master.InfrastructureCharge(1, int_areaTypeCatCode, dec_qty);
                dec_result = dec_qty * obj_InfrastructureCharge.Rate * int_days;

            }
            #endregion

            #region Mining New  Application
            if (obj_miningNewApplication != null && obj_miningNewApplication.CreatedByExUC > 0)
            {
                int_areaTypeCode = AreaTypeCodeForAppCode(null, null, obj_miningNewApplication);
                int_areaTypeCatCode = AreaTypeCategoryCodeForAppCode(null, null, obj_miningNewApplication);
                obj_issuLetterValidity = new NOCAP.BLL.Master.IssuLetterValidity(NOCAP.BLL.Common.IssuedLetterType.NOCLetter, int_areaTypeCode, int_areaTypeCatCode, obj_miningNewApplication.ApplicationTypeCode, obj_miningNewApplication.ApplicationPurposeCode);
                dec_qty = (decimal)((obj_miningNewApplication.GWREquiredThroughAbstractStructure == null ? 0 : obj_miningNewApplication.GWREquiredThroughAbstractStructure)
                    + (obj_miningNewApplication.GWRequiredThroughMiningSeeping == null ? 0 : obj_miningNewApplication.GWRequiredThroughMiningSeeping));

                int_year = DateTime.Now.AddMonths(obj_issuLetterValidity.NoOfMonthsForValidity).Year - DateTime.Now.Year;
                int_days = DateTime.Now.AddMonths(obj_issuLetterValidity.NoOfMonthsForValidity).Subtract(DateTime.Now).Days;


                obj_MiningCharge = new NOCAP.BLL.Master.MiningCharge(1, int_areaTypeCatCode, dec_qty);
                dec_result = dec_qty * obj_MiningCharge.Rate * int_days;

            }
            #endregion

            #region Mining Renew  Application
            if (obj_miningRenewApplication != null && obj_miningRenewApplication.CreatedByExUC > 0)
            {
                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_miningRenewApplication.FirstApplicationCode);
                int_areaTypeCode = AreaTypeCodeForAppCode(null, null, obj_MiningNewApplication);
                int_areaTypeCatCode = AreaTypeCategoryCodeForAppCode(null, null, obj_MiningNewApplication);
                obj_issuLetterValidity = new NOCAP.BLL.Master.IssuLetterValidity(NOCAP.BLL.Common.IssuedLetterType.NOCLetter, int_areaTypeCode, int_areaTypeCatCode, obj_miningRenewApplication.ApplicationTypeCode, 1);
                dec_qty = (decimal)((obj_miningRenewApplication.GWREquiredThroughAbstractStructureAdditional == null ? 0 : obj_miningRenewApplication.GWREquiredThroughAbstractStructureAdditional)
                    + (obj_miningRenewApplication.GWREquiredThroughAbstractStructureExisting == null ? 0 : obj_miningRenewApplication.GWREquiredThroughAbstractStructureExisting)
                    + (obj_miningRenewApplication.GWRequiredThroughMiningSeepingAdditional == null ? 0 : obj_miningRenewApplication.GWRequiredThroughMiningSeepingAdditional)
                    + (obj_miningRenewApplication.GWRequiredThroughMiningSeepingExisting == null ? 0 : obj_miningRenewApplication.GWRequiredThroughMiningSeepingExisting)
                    );

                int_year = DateTime.Now.AddMonths(obj_issuLetterValidity.NoOfMonthsForValidity).Year - DateTime.Now.Year;
                int_days = DateTime.Now.AddMonths(obj_issuLetterValidity.NoOfMonthsForValidity).Subtract(DateTime.Now).Days;


                obj_MiningCharge = new NOCAP.BLL.Master.MiningCharge(1, int_areaTypeCatCode, dec_qty);
                dec_result = dec_qty * obj_MiningCharge.Rate * int_days;

            }
            #endregion


            str_minAmt = String.Format("{0:0.00}", Convert.ToString(dec_result / int_year));
            // string s = String.Format("{0:0.00}", Convert.ToString(dec_result));

            return String.Format("{0:0.00}", Convert.ToString(dec_result));
        }
        catch (Exception ex)
        {
            return "";
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int Round(decimal value)
    {
        double decimalpoints = (double)Math.Abs(value - Math.Floor(value));
        if (decimalpoints > 0.0)
            return (int)Math.Floor(value) + 1;

        else
            return (int)Math.Round(value);

    }
    public static int AreaTypeCodeForAppCode(NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null, NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = null, NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = null)
    {
        NOCAP.BLL.Master.SubDistrict obj_subDistrict = null;
        NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_subDistrictAreaTypeCategoryHistory = null;
        if (obj_IndustrialNewApplication != null)
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        else if (obj_InfrastructureNewApplication != null)
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        else
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);


        obj_subDistrictAreaTypeCategoryHistory = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory();
        return obj_subDistrictAreaTypeCategoryHistory.AreaTypeCode;
    }
    public static int AreaTypeCategoryCodeForAppCode(NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null,
         NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = null, NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = null)
    {
        NOCAP.BLL.Master.SubDistrict obj_subDistrict = null;
        NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_subDistrictAreaTypeCategoryHistory = null;
        if (obj_IndustrialNewApplication != null)
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        else if (obj_MiningNewApplication != null)
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        else if (obj_InfrastructureNewApplication != null)
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);

        obj_subDistrictAreaTypeCategoryHistory = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory();
        return obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode;
    }

    public static int AreaTypeCode(NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = null, NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewSADApplication = null, NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewSADApplication = null)
    {
        NOCAP.BLL.Master.SubDistrict obj_subDistrict = null;
        NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_subDistrictAreaTypeCategoryHistory = null;
        if (obj_IndustrialNewSADApplication != null)
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        else if (obj_InfrastructureNewSADApplication != null)
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        else
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);


        obj_subDistrictAreaTypeCategoryHistory = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory();
        return obj_subDistrictAreaTypeCategoryHistory.AreaTypeCode;
    }
    public static int AreaTypeCategoryCode(NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = null,
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewSADApplication = null,
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewSADApplication = null,
        NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication obj_IndustrialExpansionApplication = null,
        NOCAP.BLL.Infrastructure.Expansion.InfrastructureExpansionApplication obj_InfrastructureExpansionApplication = null,
        NOCAP.BLL.Mining.Expansion.MiningExpansionApplication obj_MiningExpansionApplication = null )
    {
        NOCAP.BLL.Master.SubDistrict obj_subDistrict = null;
        NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_subDistrictAreaTypeCategoryHistory = null;
        if (obj_IndustrialNewSADApplication != null)
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        else if (obj_InfrastructureNewSADApplication != null)
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        else if (obj_MiningNewSADApplication != null)
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        else if (obj_IndustrialExpansionApplication != null)
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_IndustrialExpansionApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialExpansionApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialExpansionApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        else if (obj_InfrastructureExpansionApplication != null)
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureExpansionApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureExpansionApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureExpansionApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        else
            obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_MiningExpansionApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningExpansionApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningExpansionApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);

        obj_subDistrictAreaTypeCategoryHistory = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory();
        return obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode;
    }

  


    public static NOCAP.BLL.Common.CommonEnum.YesNoOption CheckPenaltyForSADApplication(long lngA_ApplicationCode)
    {
        return NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
        //NOCAP.BLL.Common.CommonEnum.YesNoOption enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
        //NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplicationPrev = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
        //NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplicationPrev = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication();
        //NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplicationPrev = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
        //NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplicationPrev = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication();
        //NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplicationPrev = new NOCAP.BLL.Mining.New.Application.MiningNewApplication();
        //NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplicationPrev = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication();
        //NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_LatestINDRenewApplication = null;
        //NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_LatestINFRenewApplication = null;
        //NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_LatestMINRenewApplication = null;
        //long? lngA_RenAppCode = null;
        //NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_industrialNewSADApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication();
        //NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_industrialRenewSADApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication();
        //NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewSADApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication();
        //NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_infrastructureRenewSADApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication();
        //NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_miningNewSADApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication();
        //NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_miningRenewSADApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication();
        //NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_industrialNewSADApplication, out obj_infrastructureNewSADApplication, out obj_miningNewSADApplication, out obj_industrialRenewSADApplication, out obj_infrastructureRenewSADApplication, out obj_miningRenewSADApplication, lngA_ApplicationCode);
        //#region Try
        //try
        //{
        //    if (obj_industrialNewSADApplication != null && obj_industrialNewSADApplication.CreatedByExUC > 0)
        //    {
        //        switch (obj_industrialNewSADApplication.GroundWaterUtilizationFor)
        //        {

        //            case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry:
        //                enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
        //                break;
        //            default:
        //                enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
        //                break;
        //        }

        //    }
        //    #region IndustrialRenewSADApplication
        //    else if (obj_industrialRenewSADApplication != null && obj_industrialRenewSADApplication.CreatedByExUC > 0)
        //    {

        //        if (obj_industrialRenewSADApplication.GetPreviousIndustrialApplication(out obj_industrialNewApplicationPrev, out obj_industrialRenewApplicationPrev) == 1)
        //        {

        //            obj_industrialRenewSADApplication.GetLatestIssueLetter(out lngA_RenAppCode);
        //            if (obj_industrialNewApplicationPrev != null && lngA_RenAppCode == null)
        //            {
        //                NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_industrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter(obj_industrialNewApplicationPrev.IndustrialNewApplicationCode);
        //                if (obj_industrialNewIssusedLetter.ValidityEndDate != null)
        //                {
        //                    if (Convert.ToDateTime(Convert.ToDateTime(obj_industrialNewIssusedLetter.ValidityEndDate).ToShortDateString()) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
        //                        enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
        //                    else
        //                        enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;

        //                }
        //            }
        //            else if (obj_industrialNewApplicationPrev != null && lngA_RenAppCode != null)
        //            {
        //                NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_industrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter(obj_industrialNewApplicationPrev.IndustrialNewApplicationCode);
        //                obj_LatestINDRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication((long)lngA_RenAppCode);
        //                if (obj_industrialNewIssusedLetter.ValidityEndDate != null)
        //                {
        //                    if (Convert.ToDateTime(Convert.ToDateTime(obj_industrialNewIssusedLetter.ValidityEndDate).ToShortDateString()) < Convert.ToDateTime(Convert.ToDateTime(obj_LatestINDRenewApplication.SubmittedOnByExUC).ToShortDateString()))
        //                        enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
        //                    else
        //                        enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;

        //                }
        //            }
        //            else if (obj_industrialRenewApplicationPrev != null && lngA_RenAppCode != null)
        //            {

        //                NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter obj_industrialRenewIssusedLetter = new NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter(obj_industrialRenewApplicationPrev.IndustrialRenewApplicationCode);
        //                obj_LatestINDRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication((long)lngA_RenAppCode);
        //                if (obj_industrialRenewIssusedLetter.ValidityEndDate != null && obj_LatestINDRenewApplication != null && obj_LatestINDRenewApplication.CreatedByExUC > 0)
        //                {
        //                    if (obj_industrialRenewIssusedLetter.RenewApplicationCode == obj_LatestINDRenewApplication.IndustrialRenewApplicationCode)
        //                    {
        //                        if (Convert.ToDateTime(Convert.ToDateTime(obj_industrialRenewIssusedLetter.ValidityEndDate).ToShortDateString()) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
        //                            enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
        //                        else
        //                            enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
        //                    }
        //                    else
        //                    {
        //                        if (Convert.ToDateTime(Convert.ToDateTime(obj_industrialRenewIssusedLetter.ValidityEndDate).ToShortDateString()) < Convert.ToDateTime(Convert.ToDateTime(obj_LatestINDRenewApplication.SubmittedOnByExUC).ToShortDateString()))
        //                            enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
        //                        else
        //                            enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
        //                    }
        //                }

        //            }
        //        }

        //    }
        //    #endregion
        //    else if (obj_infrastructureNewSADApplication != null && obj_infrastructureNewSADApplication.CreatedByExUC > 0)
        //    {
        //        switch (obj_infrastructureNewSADApplication.GroundWaterUtilizationFor)
        //        {

        //            case NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.GroundWaterUtilizationForOption.ExistingIndustry:
        //                enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
        //                break;
        //            default:
        //                enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
        //                break;



        //        }
        //    }
        //    #region  InfrastructureRenewSADApplication
        //    else if (obj_infrastructureRenewSADApplication != null && obj_infrastructureRenewSADApplication.CreatedByExUC > 0)
        //    {
        //        if (obj_infrastructureRenewSADApplication.GetPreviousInfrastructureApplication(out obj_infrastructureNewApplicationPrev, out obj_infrastructureRenewApplicationPrev) == 1)
        //        {
        //            obj_infrastructureRenewSADApplication.GetLatestIssueLetter(out lngA_RenAppCode);
        //            if (obj_infrastructureNewApplicationPrev != null && lngA_RenAppCode == null)
        //            {
        //                NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_infrastructureNewIssusedLetter = new NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter(obj_infrastructureNewApplicationPrev.InfrastructureNewApplicationCode);
        //                if (obj_infrastructureNewIssusedLetter.ValidityEndDate != null)
        //                {
        //                    if (Convert.ToDateTime(Convert.ToDateTime(obj_infrastructureNewIssusedLetter.ValidityEndDate).ToShortDateString()) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
        //                        enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
        //                    else
        //                        enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;


        //                }
        //            }
        //            else if (obj_infrastructureNewApplicationPrev != null && lngA_RenAppCode != null)
        //            {
        //                NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_infrastructureNewIssusedLetter = new NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter(obj_infrastructureNewApplicationPrev.InfrastructureNewApplicationCode);
        //                obj_LatestINFRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication((long)lngA_RenAppCode);
        //                if (obj_infrastructureNewIssusedLetter.ValidityEndDate != null)
        //                {
        //                    if (Convert.ToDateTime(Convert.ToDateTime(obj_infrastructureNewIssusedLetter.ValidityEndDate).ToShortDateString()) < Convert.ToDateTime(Convert.ToDateTime(obj_LatestINFRenewApplication.SubmittedOnByExUC).ToShortDateString()))
        //                        enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
        //                    else
        //                        enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;


        //                }
        //            }
        //            else if (obj_infrastructureRenewApplicationPrev != null)
        //            {
        //                NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter obj_infrastructureRenewIssusedLetter = new NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter(obj_infrastructureRenewApplicationPrev.InfrastructureRenewApplicationCode);
        //                obj_LatestINFRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication((long)lngA_RenAppCode);
        //                if (obj_infrastructureRenewIssusedLetter.ValidityEndDate != null && obj_LatestINFRenewApplication != null && obj_LatestINFRenewApplication.CreatedByExUC > 0)
        //                {
        //                    if (Convert.ToDateTime(Convert.ToDateTime(obj_infrastructureRenewIssusedLetter.ValidityEndDate).ToShortDateString()) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
        //                        enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
        //                    else
        //                        enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;

        //                }
        //                else
        //                {
        //                    if (Convert.ToDateTime(Convert.ToDateTime(obj_infrastructureRenewIssusedLetter.ValidityEndDate).ToShortDateString()) < Convert.ToDateTime(Convert.ToDateTime(obj_LatestINFRenewApplication.SubmittedOnByExUC).ToShortDateString()))
        //                        enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
        //                    else
        //                        enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
        //                }
        //            }
        //        }

        //    }
        //    #endregion
        //    else if (obj_miningNewSADApplication != null && obj_miningNewSADApplication.CreatedByExUC > 0)
        //    {
        //        switch (obj_miningNewSADApplication.GroundWaterUtilizationFor)
        //        {

        //            case NOCAP.BLL.Common.WhetherGroundWaterUtilizationFor.GroundWaterUtilizationForOption.ExistingIndustry:
        //                enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
        //                break;
        //            default:
        //                enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
        //                break;



        //        }
        //    }
        //    #region  MiningRenewSADApplication
        //    else if (obj_miningRenewSADApplication != null && obj_miningRenewSADApplication.CreatedByExUC > 0)
        //    {
        //        if (obj_miningRenewSADApplication.GetPreviousMiningApplication(out obj_miningNewApplicationPrev, out obj_miningRenewApplicationPrev) == 1)
        //        {
        //            obj_miningRenewSADApplication.GetLatestIssueLetter(out lngA_RenAppCode);
        //            if (obj_miningNewApplicationPrev != null && lngA_RenAppCode == null)
        //            {
        //                NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter obj_miningNewIssusedLetter = new NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter(obj_miningNewApplicationPrev.ApplicationCode);
        //                if (obj_miningNewIssusedLetter.ValidityEndDate != null)
        //                {
        //                    if (Convert.ToDateTime(Convert.ToDateTime(obj_miningNewIssusedLetter.ValidityEndDate).ToShortDateString()) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
        //                        enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
        //                    else
        //                        enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;

        //                }
        //            }
        //            else if (obj_miningNewApplicationPrev != null && lngA_RenAppCode != null)
        //            {
        //                NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter obj_miningNewIssusedLetter = new NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter(obj_miningNewApplicationPrev.ApplicationCode);
        //                obj_LatestMINRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication((long)lngA_RenAppCode);
        //                if (obj_miningNewIssusedLetter.ValidityEndDate != null)
        //                {
        //                    if (Convert.ToDateTime(Convert.ToDateTime(obj_miningNewIssusedLetter.ValidityEndDate).ToShortDateString()) < Convert.ToDateTime(Convert.ToDateTime(obj_LatestMINRenewApplication.SubmittedOnByExUC).ToShortDateString()))
        //                        enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
        //                    else
        //                        enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;

        //                }
        //            }
        //            else if (obj_miningRenewApplicationPrev != null)
        //            {
        //                NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter obj_miningRenewIssusedLetter = new NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter(obj_miningRenewApplicationPrev.MiningRenewApplicationCode);
        //                obj_LatestMINRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication((long)lngA_RenAppCode);
        //                if (obj_miningRenewIssusedLetter.ValidityEndDate != null && obj_LatestMINRenewApplication != null && obj_LatestMINRenewApplication.CreatedByExUC > 0)
        //                {
        //                    if (Convert.ToDateTime(Convert.ToDateTime(obj_miningRenewIssusedLetter.ValidityEndDate).ToShortDateString()) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
        //                        enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
        //                    else
        //                        enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
        //                }
        //                else
        //                {
        //                    if (Convert.ToDateTime(Convert.ToDateTime(obj_miningRenewIssusedLetter.ValidityEndDate).ToShortDateString()) < Convert.ToDateTime(Convert.ToDateTime(obj_LatestMINRenewApplication.SubmittedOnByExUC).ToShortDateString()))
        //                        enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.Yes;
        //                    else
        //                        enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.No;
        //                }
        //            }
        //        }

        //    }
        //    #endregion
        //    return enm_checkPenalty;
        //}
        //catch (Exception ex)
        //{
        //    enm_checkPenalty = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined;
        //    return enm_checkPenalty;
        //}
        //#endregion
    }
    public static int ApplicationNameChangeDownloadFiles(long int_noticecode, int int_SN)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.ApplicationManagement.ApplicationNameChange objNameChangeDocument = new NOCAP.BLL.ApplicationManagement.ApplicationNameChange();
            NOCAP.BLL.ApplicationManagement.ApplicationNameChange objNameChangeDocumentFile = objNameChangeDocument.DownloadNamechangeFile(int_noticecode, int_SN);
            if (objNameChangeDocumentFile != null)
            {
                byte[] bytes = objNameChangeDocumentFile.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = objNameChangeDocumentFile.ContentType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "INT_" + Convert.ToString(int_noticecode) + objNameChangeDocumentFile.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }
        finally
        {
            HttpContext.Current.Response.End();
        }
    }
    public static int FillDropDownConsultant(ref DropDownList ddl_Consultant)
    {
        NOCAP.BLL.Master.ConsultantDetail obj_consultantDetail = new NOCAP.BLL.Master.ConsultantDetail();
        NOCAP.BLL.Master.ConsultantDetail[] arr_consultantDetail;

        int int_status = 0;
        try
        {
            ddl_Consultant.Items.Clear();
            arr_consultantDetail = obj_consultantDetail.GetConsultantDetailList(enu_Active: NOCAP.BLL.Master.ConsultantDetail.YesNoOption.Yes);

            if (arr_consultantDetail.Count() > 0)
            {
                ddl_Consultant.DataSource = arr_consultantDetail;
                ddl_Consultant.DataTextField = "ConsultantName";
                ddl_Consultant.DataValueField = "ConsultantCode";
                ddl_Consultant.DataBind();

                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddl_Consultant);
            return int_status;
        }
        catch
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_Consultant);
            return int_status;
        }
    }
    public static int FillDropDownCorrecationCharge(ref DropDownList ddl_CorrecationChargeDropDown)
    {
        NOCAP.BLL.Master.CorrectionCharge obj_CorrectionCharge = new NOCAP.BLL.Master.CorrectionCharge();
        NOCAP.BLL.Master.CorrectionCharge[] arr;
        int int_status = 0;
        try
        {
            ddl_CorrecationChargeDropDown.Items.Clear();

            if (obj_CorrectionCharge.GetAll(NOCAP.BLL.Master.CorrectionCharge.SortingField.CorrectionChargeDesc) == 1)
            {
                arr = obj_CorrectionCharge.CorrectionChargeCollection;

                if (arr.Count() > 0)
                {
                    ddl_CorrecationChargeDropDown.DataSource = arr;
                    ddl_CorrecationChargeDropDown.DataTextField = "CorrectionChargeDesc";
                    ddl_CorrecationChargeDropDown.DataValueField = "CorrectionChargeCode";
                    ddl_CorrecationChargeDropDown.DataBind();
                }
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddl_CorrecationChargeDropDown);
            return int_status;
        }
        catch (Exception)
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_CorrecationChargeDropDown);
            return int_status;
        }
    }
    public static int FillDropDownApplicationType(ref DropDownList ddl_applicationTypeDropDown)
    {
        NOCAP.BLL.Master.ApplicationType obj_applicationType = new NOCAP.BLL.Master.ApplicationType();
        NOCAP.BLL.Master.ApplicationType[] arr;
        int int_status = 0;
        try
        {
            ddl_applicationTypeDropDown.Items.Clear();

            if (obj_applicationType.GetAll(NOCAP.BLL.Master.ApplicationType.SortingField.ApplicationTypeDescription) == 1)
            {
                arr = obj_applicationType.ApplicationTypeCollection;

                if (arr.Count() > 0)
                {
                    ddl_applicationTypeDropDown.DataSource = arr;
                    ddl_applicationTypeDropDown.DataTextField = "ApplicationTypeDescription";
                    ddl_applicationTypeDropDown.DataValueField = "ApplicationTypeCode";
                    ddl_applicationTypeDropDown.DataBind();
                }
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddl_applicationTypeDropDown);
            return int_status;
        }
        catch
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_applicationTypeDropDown);
            return int_status;
        }
    }
    public static int FillDropDownIssuedLetterTypeName(ref DropDownList ddl_issuedLetterTypeName)
    {
        NOCAP.BLL.Master.IssuedLettersType obj_issuedLetterTypeName = new NOCAP.BLL.Master.IssuedLettersType();
        NOCAP.BLL.Master.IssuedLettersType[] arr_issuedLetterTypeName;
        int int_status = 0;
        try
        {
            ddl_issuedLetterTypeName.Items.Clear();
            if (obj_issuedLetterTypeName.GetAll(NOCAP.BLL.Master.IssuedLettersType.SortingField.IssuedLetterTypeName) == 1)
            {
                arr_issuedLetterTypeName = obj_issuedLetterTypeName.IssuedLetterTypeCollection;
                if (arr_issuedLetterTypeName.Count() > 0)
                {
                    ddl_issuedLetterTypeName.DataSource = arr_issuedLetterTypeName;
                    ddl_issuedLetterTypeName.DataTextField = "IssuedLetterTypeName";
                    ddl_issuedLetterTypeName.DataValueField = "IssuedLetterTypeCode";
                    ddl_issuedLetterTypeName.DataBind();
                }
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddl_issuedLetterTypeName);
            return int_status;
        }
        catch
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_issuedLetterTypeName);
            return int_status;
        }
    }


    public static int INTLatestUpdateDocumentDownloadFiles(long int_noticecode)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.LatestUpdate.LatestDocument objLatestUpdateDocument = new NOCAP.BLL.LatestUpdate.LatestDocument();
            NOCAP.BLL.LatestUpdate.LatestDocument objLatestUpdateDocumentFile = objLatestUpdateDocument.DownloadLatestDocumentFile(int_noticecode);
            if (objLatestUpdateDocumentFile != null)
            {
                byte[] bytes = objLatestUpdateDocumentFile.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = objLatestUpdateDocumentFile.ContentType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "INT_" + Convert.ToString(int_noticecode) + objLatestUpdateDocumentFile.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }
        finally
        {
            HttpContext.Current.Response.End();
        }
    }
    public static int FillCheckBoxListPaymentType(ref CheckBoxList chk_PaymentType)
    {

        NOCAP.BLL.Master.PaymentType obj_PaymentType = new NOCAP.BLL.Master.PaymentType();

        NOCAP.BLL.Master.PaymentType[] arr;
        int int_status = 0;
        try
        {
            chk_PaymentType.Items.Clear();
            obj_PaymentType.GetALL();
            arr = obj_PaymentType.PaymentTypeCollection;
            if (arr != null && arr.Count() > 0)
            {
                chk_PaymentType.DataSource = arr;
                chk_PaymentType.DataTextField = "PaymentTypeDesc";
                chk_PaymentType.DataValueField = "PaymentTypeCode";
                chk_PaymentType.DataBind();
            }
            int_status = 1;
            // AddFirstItemInCheckBoxList(ref chk_PaymentType);
            return int_status;
        }
        catch (Exception)
        {
            int_status = 0;
            // AddFirstItemInCheckBoxList(ref chk_PaymentType);
            return int_status;
        }

    }

    public static NOCAP.BLL.Misc.Payment.OnlinePayment OnlinePayment(long ApplicationCode, decimal TotalAmount, int ExternalUserCode, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode enu, int Transactions)
    {
        NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(ExternalUserCode);
        NOCAP.BLL.Misc.Payment.OnlinePayment obj_OnlinePayment = new NOCAP.BLL.Misc.Payment.OnlinePayment();
        obj_OnlinePayment.ApplicationCode = ApplicationCode;
        obj_OnlinePayment.PaymentMethodMode = enu;
        obj_OnlinePayment.Transactions = Transactions;
        obj_OnlinePayment.TotalAmount = TotalAmount;
        obj_OnlinePayment.CreatedByExUC = ExternalUserCode;
        if (obj_OnlinePayment.Add() == 1)
        {
            obj_OnlinePayment = new NOCAP.BLL.Misc.Payment.OnlinePayment(obj_OnlinePayment.OrderPaymentCode);
        }
        else
        {
            obj_OnlinePayment = null;
        }
        return obj_OnlinePayment;
    }

    public static NOCAP.BLL.Misc.Payment.OnlinePaymentDetails[] OnlinePaymentDetails(NOCAP.BLL.Misc.Payment.OnlinePayment obj_OnlinePayment, decimal AmountValue, string OrderContent, string PaymentTypeCode, int ExternalUserCode)
    {
        NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(ExternalUserCode);
        NOCAP.BLL.Misc.Payment.OnlinePaymentDetails obj_OnlinePaymentDetails = new NOCAP.BLL.Misc.Payment.OnlinePaymentDetails();
        NOCAP.BLL.Misc.Payment.OnlinePaymentDetails[] arr_OnlinePaymentDetails = null;
        string[] arr_OrderContent = OrderContent.Split(',');
        string[] arr_PaymentTypeCode = PaymentTypeCode.Split(',');
        for (int i = 0; i < arr_OrderContent.Length - 1; i++)
        {

            obj_OnlinePaymentDetails.OrderPaymentCode = obj_OnlinePayment.OrderPaymentCode;
            obj_OnlinePaymentDetails.PaymentTypeCode = Convert.ToInt32(arr_PaymentTypeCode[i]);
            obj_OnlinePaymentDetails.AmountValue = AmountValue;
            obj_OnlinePaymentDetails.OrderContent = Convert.ToInt32(arr_OrderContent[i]);
            obj_OnlinePaymentDetails.CreatedByExUC = ExternalUserCode;

            if (obj_OnlinePaymentDetails.Add() == 1)
            {

            }
            else
            {
                obj_OnlinePaymentDetails = null;
                break;
            }
        }
        obj_OnlinePaymentDetails = new NOCAP.BLL.Misc.Payment.OnlinePaymentDetails();
        obj_OnlinePaymentDetails.OrderPaymentCode = obj_OnlinePayment.OrderPaymentCode;
        obj_OnlinePaymentDetails.GetALL(NOCAP.BLL.Misc.Payment.OnlinePaymentDetails.SortingField.OrderPaymentCode);
        arr_OnlinePaymentDetails = obj_OnlinePaymentDetails.OnlinePaymentDetailsCollection;
        return arr_OnlinePaymentDetails;
    }

    public static XmlDocument signedFun(XmlDocument xmlDoc)
    {
        string CertificateKey = ConfigurationManager.AppSettings["NOCAPDOCSigPrivateKey"];// "TestCertForLOBA";
        X509Store certificate = new X509Store(StoreLocation.CurrentUser);
        try
        {
            certificate = new X509Store(StoreLocation.LocalMachine);
            certificate.Open(OpenFlags.ReadOnly);
            RSACryptoServiceProvider rsaKey = null;
            string checkCertificate = "";
            X509Certificate2 certx = new X509Certificate2();
            foreach (X509Certificate2 cert in certificate.Certificates)
            {
                if (cert.Subject.Contains(CertificateKey))
                {
                    if (cert.HasPrivateKey)
                    {
                        // retrieve private key                  
                        rsaKey = (RSACryptoServiceProvider)cert.PrivateKey;
                    }
                    certx = cert;
                    checkCertificate = GetDateTimeValue(certx.NotAfter.ToString("yyyy-MM-dd")) >= GetDateTimeValue(DateTime.Now.ToString("yyyy-MM-dd")) ? "Valid " : "Expired";
                    break;
                }
            }
            if (rsaKey == null && checkCertificate == "Expired")
                throw new Exception("Valid certificate was not found or Expired");


            // Sign the XML document. 
            SignXml(xmlDoc, rsaKey, certx);

            return xmlDoc;
        }
        catch (CryptographicException ex)
        {
            ActionTrail obj_ExtActionTrail = new ActionTrail();
            obj_ExtActionTrail.UserCode = 1;
            obj_ExtActionTrail.IP_Address = "";
            obj_ExtActionTrail.ActionPerformed = "";
            obj_ExtActionTrail.Status = ex.Message;
            if (obj_ExtActionTrail != null)
                ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);

            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    private static void SignXml(XmlDocument xmlDoc, RSA Key, X509Certificate2 Cert)
    {
        // Check arguments. 
        if (xmlDoc == null)
            throw new ArgumentException("xmlDoc");
        if (Key == null)
            throw new ArgumentException("Key");

        // Create a SignedXml object.
        SignedXml signedXml = new SignedXml(xmlDoc);
        // SignedXml signedXml = new SignedXml(xmlDoc);

        // Add the key to the SignedXml document.
        signedXml.SigningKey = Key;
        // *** Create a KeyInfo structure
        KeyInfo keyInfo = new KeyInfo();
        // *** Specifically use the issuer and serial number for the data rather than the default
        KeyInfoX509Data keyInfoData = new KeyInfoX509Data();
        keyInfoData.AddIssuerSerial(Cert.Issuer, Cert.GetSerialNumberString());
        keyInfoData.AddCertificate(Cert);

        keyInfo.AddClause(keyInfoData);


        // *** provide the certficate info that gets embedded - note this is only
        // *** for specific formatting of the message to provide the cert info
        signedXml.KeyInfo = keyInfo;

        // Create a reference to be signed.
        Reference reference = new Reference();
        //        reference.Uri = "http://10.1.14.209/nocap/ResponseFromNTRP.aspx";
        reference.Uri = "";

        // Add an enveloped transformation to the reference.
        XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
        reference.AddTransform(env);

        // Add the reference to the SignedXml object.
        signedXml.AddReference(reference);

        // Compute the signature.
        signedXml.ComputeSignature();

        // Get the XML representation of the signature and save 
        // it to an XmlElement object.
        XmlElement xmlDigitalSignature = signedXml.GetXml();

        // Append the element to the XML document.
        xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));

    }
    private static DateTime GetDateTimeValue(string DateTimeValue)
    {
        string str_DateTimeValue = Convert.ToDateTime(DateTimeValue).ToString("yyyy-MM-dd h:mm tt");
        DateTime dt = DateTime.ParseExact(str_DateTimeValue, "yyyy-MM-dd h:mm tt", CultureInfo.InvariantCulture);
        return dt;
    }
    public static string CreateXMLForSADApplication(long ExternalUserCode, Dictionary<int, decimal> obj_Dictionary, 
        decimal decA_GWAmountValue, decimal? ArearAmount, 
        NOCAP.BLL.Common.CommonEnum.PaymentMethodMode enu,
            NOCAP.BLL.Common.CommonEnum.PaymentTypeMode enu2,
        ref string OrderPaymentCode, 
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = null, 
        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewSADApplication = null, NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewSADApplication = null, NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewSADApplication = null, NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewSADApplication = null, NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewSADApplication = null)
    {

        NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails obj_OnlinePaymentDetails = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails();

        NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails[] arr_OnlinePaymentDetails = null;

        long ApplicationCode = 0;
        string ShopperEmailAddress = "", Address1 = "", Address2 = "", PostalCode = "", City = "", State = "", BillMobileNumber = "";
        if (obj_IndustrialNewSADApplication != null)
        {
            ApplicationCode = obj_IndustrialNewSADApplication.IndustrialNewApplicationCode;
            ShopperEmailAddress = obj_IndustrialNewSADApplication.CommunicationEmailID;
            Address1 = obj_IndustrialNewSADApplication.CommunicationAddress.AddressLine1;
            Address2 = obj_IndustrialNewSADApplication.CommunicationAddress.AddressLine2;
            PostalCode = ((int)obj_IndustrialNewSADApplication.CommunicationAddress.PinCode).ToString();
            City = new NOCAP.BLL.Master.SubDistrict(obj_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;
            State = new NOCAP.BLL.Master.State(obj_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
            BillMobileNumber = obj_IndustrialNewSADApplication.CommunicationMobileNumber.MobileNumberRest.ToString();

        }
        else if (obj_IndustrialRenewSADApplication != null)
        {
            obj_IndustrialNewSADApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(obj_IndustrialRenewSADApplication.FirstApplicationCode);
            ApplicationCode = obj_IndustrialRenewSADApplication.IndustrialRenewApplicationCode;
            ShopperEmailAddress = obj_IndustrialRenewSADApplication.CommunicationEmailID;
            Address1 = obj_IndustrialRenewSADApplication.CommunicationAddress.AddressLine1;
            Address2 = obj_IndustrialRenewSADApplication.CommunicationAddress.AddressLine2;
            PostalCode = ((int)obj_IndustrialRenewSADApplication.CommunicationAddress.PinCode).ToString();
            BillMobileNumber = obj_IndustrialRenewSADApplication.CommunicationMobileNumber.MobileNumberRest.ToString();


            City = new NOCAP.BLL.Master.SubDistrict(obj_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;
            State = new NOCAP.BLL.Master.State(obj_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;


        }
        else if (obj_InfrastructureNewSADApplication != null)
        {
            ApplicationCode = obj_InfrastructureNewSADApplication.InfrastructureNewApplicationCode;
            ShopperEmailAddress = obj_InfrastructureNewSADApplication.CommunicationEmailID;
            Address1 = obj_InfrastructureNewSADApplication.CommunicationAddress.AddressLine1;
            Address2 = obj_InfrastructureNewSADApplication.CommunicationAddress.AddressLine2;
            PostalCode = ((int)obj_InfrastructureNewSADApplication.CommunicationAddress.PinCode).ToString();
            BillMobileNumber = obj_InfrastructureNewSADApplication.CommunicationMobileNumber.MobileNumberRest.ToString();
            City = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;
            State = new NOCAP.BLL.Master.State(obj_InfrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;


        }
        else if (obj_InfrastructureRenewSADApplication != null)
        {
            obj_InfrastructureNewSADApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(obj_InfrastructureRenewSADApplication.FirstApplicationCode);
            ApplicationCode = obj_InfrastructureRenewSADApplication.InfrastructureRenewApplicationCode;
            ShopperEmailAddress = obj_InfrastructureRenewSADApplication.CommunicationEmailID;
            Address1 = obj_InfrastructureRenewSADApplication.CommunicationAddress.AddressLine1;
            Address2 = obj_InfrastructureRenewSADApplication.CommunicationAddress.AddressLine2;
            PostalCode = ((int)obj_InfrastructureRenewSADApplication.CommunicationAddress.PinCode).ToString();
            BillMobileNumber = obj_InfrastructureRenewSADApplication.CommunicationMobileNumber.MobileNumberRest.ToString();
            City = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;
            State = new NOCAP.BLL.Master.State(obj_InfrastructureNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;



        }
        else if (obj_MiningNewSADApplication != null)
        {
            ApplicationCode = obj_MiningNewSADApplication.ApplicationCode;
            ShopperEmailAddress = obj_MiningNewSADApplication.CommunicationEmailID;
            Address1 = obj_MiningNewSADApplication.CommunicationAddress.AddressLine1;
            Address2 = obj_MiningNewSADApplication.CommunicationAddress.AddressLine2;
            PostalCode = ((int)obj_MiningNewSADApplication.CommunicationAddress.PinCode).ToString();
            BillMobileNumber = obj_MiningNewSADApplication.CommunicationMobileNumber.MobileNumberRest.ToString();
            City = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;
            State = new NOCAP.BLL.Master.State(obj_MiningNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;


        }
        else
        {
            obj_MiningNewSADApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication(obj_MiningRenewSADApplication.FirstApplicationCode);
            ApplicationCode = obj_MiningRenewSADApplication.MiningRenewApplicationCode;
            ShopperEmailAddress = obj_MiningRenewSADApplication.CommunicationEmailID;
            Address1 = obj_MiningRenewSADApplication.CommunicationAddress.AddressLine1;
            Address2 = obj_MiningRenewSADApplication.CommunicationAddress.AddressLine2;
            PostalCode = ((int)obj_MiningRenewSADApplication.CommunicationAddress.PinCode).ToString();
            BillMobileNumber = obj_MiningRenewSADApplication.CommunicationMobileNumber.MobileNumberRest.ToString();
            City = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;
            State = new NOCAP.BLL.Master.State(obj_MiningNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;



        }
        decimal totalAmount = 0;

        foreach (KeyValuePair<int, decimal> dict in obj_Dictionary)
        {
            totalAmount += dict.Value;
        }
        NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(ExternalUserCode);
        //Create SADOnlinePayment i.e. main
        NOCAP.BLL.Misc.Payment.SADOnlinePayment obj_OnlinePayment = SADOnlinePayment(ApplicationCode, totalAmount, ExternalUserCode, obj_Dictionary, decA_GWAmountValue, ArearAmount, enu, enu2,obj_Dictionary.Count());

        // NOCAP.BLL.Master.NTRPMapping obj_NTRPMapping = new NOCAP.BLL.Master.NTRPMapping();
        // obj_NTRPMapping.GetALL();
        //  NOCAP.BLL.Master.NTRPMapping[] arr_NTRPMapping = obj_NTRPMapping.NTRPMappingCollection;
        // NOCAP.BLL.Master.NTRPMapping obj_nTRPMapping = null;


        string strOnlonePaymentXMLDetail = null;
        try
        {
            //if (obj_OnlinePayment != null)
            //{
            //sbOnlinePaymentXMLDetail.AppendLine("</SADOnlinePaymentDetails>");
            // }
            obj_OnlinePaymentDetails.OrderPaymentCode = obj_OnlinePayment.OrderPaymentCode;
            obj_OnlinePaymentDetails.GetALL();
            arr_OnlinePaymentDetails = obj_OnlinePaymentDetails.SADOnlinePaymentDetailsCollection;
            OrderPaymentCode = obj_OnlinePayment.OrderPaymentCode;



            // obj_NTRPMapping = new NOCAP.BLL.Master.NTRPMapping(14794, 1);
            StringBuilder builder = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(builder);
            //Start XM DOcument
            writer.WriteStartDocument(true);
            // writer.Formatting = Formatting.Indented;
            //writer.Indentation = 2;

            #region ROOT
            //ROOT Element
            writer.WriteStartElement("BharatKoshPayment");
            writer.WriteAttributeString("DepartmentCode", ConfigurationManager.AppSettings["DepartmentCode"]);
            writer.WriteAttributeString("Version", "1.0");

            #region Submit
            //Submit Element
            writer.WriteStartElement("Submit");
            #region OrderBatch
            //Start OrderBatch
            writer.WriteStartElement("OrderBatch");
            writer.WriteAttributeString("TotalAmount", obj_OnlinePayment.TotalAmount.ToString());
            writer.WriteAttributeString("Transactions", obj_OnlinePayment.Transactions.ToString());
            writer.WriteAttributeString("merchantBatchCode", obj_OnlinePayment.OrderPaymentCodeForNTRP);
            #region Order
            for (int i = 0; i < arr_OnlinePaymentDetails.Length; i++)
            {
                //Start Order
                writer.WriteStartElement("Order");
                writer.WriteAttributeString("InstallationId", ConfigurationManager.AppSettings["InstallationId"]);
                if (i == 0)
                    writer.WriteAttributeString("OrderCode", obj_OnlinePayment.OrderPaymentCodeForNTRP);
                else
                    writer.WriteAttributeString("OrderCode", arr_OnlinePaymentDetails[i].OrderPaymentCodeForNTRPOrder);

                #region CartDetails
                //Start CartDetails
                writer.WriteStartElement("CartDetails");

                //Start Description
                writer.WriteStartElement("Description");
                // writer.WriteString(new NOCAP.BLL.Master.WaterChargeType(obj_OnlinePaymentDetails.water).WaterChargeTypeDesc);
                writer.WriteEndElement();     //Description Element


                //Start Amount
                writer.WriteStartElement("Amount");
                if (arr_OnlinePaymentDetails[i].PaymentTypeCode == 2 || arr_OnlinePaymentDetails[i].PaymentTypeCode == 3)
                {
                    if (arr_OnlinePaymentDetails[i].ArearAmount == null)
                        writer.WriteAttributeString("value", (arr_OnlinePaymentDetails[i].AmountValue).ToString());
                    else
                        writer.WriteAttributeString("value", (arr_OnlinePaymentDetails[i].AmountValue + arr_OnlinePaymentDetails[i].ArearAmount).ToString());
                }
                else
                    writer.WriteAttributeString("value", arr_OnlinePaymentDetails[i].AmountValue.ToString());
                writer.WriteAttributeString("exponent", "0");
                writer.WriteAttributeString("CurrencyCode", ConfigurationManager.AppSettings["CurrencyCode"]);

                writer.WriteEndElement();     //Amount Element

                //Start Amount
                writer.WriteStartElement("OrderContent");
                writer.WriteString(arr_OnlinePaymentDetails[i].OrderContent.ToString());
                writer.WriteEndElement();     //OrderContent Element

                //Start PaymentTypeId
                writer.WriteStartElement("PaymentTypeId");
                writer.WriteString(arr_OnlinePaymentDetails[i].PaymentTypeId.ToString());
                writer.WriteString("");
                writer.WriteEndElement();     //PaymentTypeId Element


                //Start PAOCode
                writer.WriteStartElement("PAOCode");
                writer.WriteString(ConfigurationManager.AppSettings["PAOCode"]);// obj_OnlinePaymentDetails.PAOCode);//imran
                writer.WriteEndElement();     //PAOCode Element

                //Start DDOCode
                writer.WriteStartElement("DDOCode");
                writer.WriteString(ConfigurationManager.AppSettings["DDOCode"]);
                writer.WriteEndElement();     //DDCode Element

                writer.WriteEndElement();     //CartDetails Element
                #endregion

                #region aymentMethodMask
                //Start PaymentMethodMask
                writer.WriteStartElement("PaymentMethodMask");

                //Start Include
                writer.WriteStartElement("Include");
                switch (obj_OnlinePayment.PaymentMethodMode)
                {
                    case NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine:
                        writer.WriteAttributeString("Code", "OffLine");
                        break;
                    case NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine:
                        writer.WriteAttributeString("Code", "OnLine");
                        break;
                    case NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.NotDefined:
                        writer.WriteAttributeString("Code", "");
                        break;
                }

                writer.WriteEndElement();     //Include Element
                writer.WriteEndElement();     //PaymentMethodMask Element
                #endregion


                #region Shopper
                //Start Shopper
                writer.WriteStartElement("Shopper");

                //Start Include
                writer.WriteStartElement("ShopperEmailAddress");
                writer.WriteString(ShopperEmailAddress);
                writer.WriteEndElement();     //ShopperEmailAddress Element
                writer.WriteEndElement();     //Shopper Element
                #endregion

                #region Shipping Address
                //Start ShippingAddress
                writer.WriteStartElement("ShippingAddress");
                //Start Address
                writer.WriteStartElement("Address");

                //Start FirstName
                writer.WriteStartElement("FirstName");
                writer.WriteString(obj_ExternalUser.ExternalUserFirstName);
                writer.WriteEndElement();     //FirstName Element
                                              //Start LastName
                writer.WriteStartElement("LastName");

                writer.WriteString(obj_ExternalUser.ExternalUserLastName);
                writer.WriteEndElement();     //LastName Element
                                              //Start Address1
                writer.WriteStartElement("Address1");
                writer.WriteString(Address1);
                writer.WriteEndElement();     //Address1 Element
                                              //Start Address2
                writer.WriteStartElement("Address2");
                writer.WriteString(Address2);
                writer.WriteEndElement();     //Address2 Element

                //Start PostalCode
                writer.WriteStartElement("PostalCode");
                writer.WriteString(PostalCode);
                writer.WriteEndElement();     //PostalCode Element

                //Start City
                writer.WriteStartElement("City");
                writer.WriteString(City);

                writer.WriteEndElement();     //City Element

                //Start StateRegion
                writer.WriteStartElement("StateRegion");
                writer.WriteString(State);
                writer.WriteEndElement();     //StateRegion Element
                                              //Start State
                writer.WriteStartElement("State");
                writer.WriteString(State);
                writer.WriteEndElement();     //State Element

                //Start CountryCode
                writer.WriteStartElement("CountryCode");
                writer.WriteString("INDIA");
                writer.WriteEndElement();     //CountryCode Element


                //Start MobileNumber
                writer.WriteStartElement("MobileNumber");
                writer.WriteString(BillMobileNumber);
                writer.WriteEndElement();     //MobileNumber Element

                writer.WriteEndElement();     //Address Element
                writer.WriteEndElement();     //ShippingAddress Element
                #endregion

                #region Billing Address
                //Start BillingAddress
                writer.WriteStartElement("BillingAddress");
                //Start Address
                writer.WriteStartElement("Address");

                //Start FirstName
                writer.WriteStartElement("FirstName");

                writer.WriteString(obj_ExternalUser.ExternalUserFirstName);
                writer.WriteEndElement();     //FirstName Element
                                              //Start LastName
                writer.WriteStartElement("LastName");
                writer.WriteString(obj_ExternalUser.ExternalUserLastName);
                writer.WriteEndElement();     //LastName Element
                                              //Start Address1
                writer.WriteStartElement("Address1");

                writer.WriteString(Address1);


                writer.WriteEndElement();     //Address1 Element
                                              //Start Address2
                writer.WriteStartElement("Address2");

                writer.WriteString(Address2);

                writer.WriteEndElement();     //Address2 Element

                //Start PostalCode
                writer.WriteStartElement("PostalCode");

                writer.WriteString(PostalCode);

                writer.WriteEndElement();     //PostalCode Element

                //Start City
                writer.WriteStartElement("City");
                writer.WriteString(City);
                writer.WriteEndElement();     //City Element

                //Start StateRegion
                writer.WriteStartElement("StateRegion");
                writer.WriteString(State);
                writer.WriteEndElement();     //StateRegion Element
                                              //Start State
                writer.WriteStartElement("State");
                writer.WriteString(State);
                writer.WriteEndElement();     //State Element

                //Start CountryCode
                writer.WriteStartElement("CountryCode");
                writer.WriteString("INDIA");
                writer.WriteEndElement();     //CountryCode Element


                //Start MobileNumber
                writer.WriteStartElement("MobileNumber");
                writer.WriteString(BillMobileNumber);
                writer.WriteEndElement();     //MobileNumber Element

                writer.WriteEndElement();     //Address Element
                writer.WriteEndElement();     //BillingAddress Element
                #endregion

                #region StatementNarrative
                //Start StatementNarrative
                writer.WriteStartElement("StatementNarrative");
                writer.WriteEndElement();     //StatementNarrative Element
                #endregion

                #region Remarks
                //Start Remarks
                writer.WriteStartElement("Remarks");
                writer.WriteEndElement();     //Remarks Element
                #endregion

                writer.WriteEndElement();     //Order Element
                #endregion
            }




            writer.WriteEndElement();     //OrderBatch Element
            #endregion
            writer.WriteEndElement();     //Submit Element
            #endregion
            writer.WriteEndElement();     //ROOT Element
            #endregion

            //End XML Document
            writer.WriteEndDocument();

            //Close writer
            writer.Close();
            return strOnlonePaymentXMLDetail = builder.ToString();//.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "<?xml version=\"1.0\"?>");
        }
        catch (Exception ex)
        { return strOnlonePaymentXMLDetail; }
    }

    public static NOCAP.BLL.Misc.Payment.SADOnlinePayment SADOnlinePayment(long ApplicationCode, decimal TotalAmount, long ExternalUserCode, 
        Dictionary<int, decimal> obj_Dictionary, decimal decA_GWAmountValue, decimal? decA_ArearAmount, 
        NOCAP.BLL.Common.CommonEnum.PaymentMethodMode enu,
        NOCAP.BLL.Common.CommonEnum.PaymentTypeMode enu2, int Transactions)
    {
        StringBuilder sbOnlinePaymentXMLDetail = new StringBuilder();
        NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(ExternalUserCode);
        NOCAP.BLL.Misc.Payment.SADOnlinePayment obj_OnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment();
        obj_OnlinePayment.ApplicationCode = ApplicationCode;
        obj_OnlinePayment.PaymentMethodMode = enu;
        obj_OnlinePayment.PaymentTypeMode = enu2;
        obj_OnlinePayment.Transactions = Transactions;
        obj_OnlinePayment.TotalAmount = TotalAmount;
        obj_OnlinePayment.CreatedByExUC = ExternalUserCode;

        sbOnlinePaymentXMLDetail.AppendLine("<?xml version=\"1.0\" ?>");
        // sbOnlinePaymentXMLDetail.AppendLine("<SADOnlinePaymentDetails>");
        NOCAP.BLL.Master.NTRPMapping obj_nTRPMapping = null;
        foreach (KeyValuePair<int, decimal> dict in obj_Dictionary)
        {
            obj_nTRPMapping = new NOCAP.BLL.Master.NTRPMapping(Convert.ToInt32(ConfigurationManager.AppSettings["OrderContent"].ToString()), Convert.ToInt32(dict.Key.ToString()));


            sbOnlinePaymentXMLDetail.AppendLine("<SADOnlinePaymentDetails>");
            // sbOnlinePaymentXMLDetail.AppendLine("<AppCode>" + Convert.ToString(obj_OnlinePayment.ApplicationCode) + "</AppCode>");
            //sbOnlinePaymentXMLDetail.AppendLine("<OrderPaymentCode>" + Convert.ToString(obj_OnlinePayment.OrderPaymentCode) + "</OrderPaymentCode>");
            sbOnlinePaymentXMLDetail.AppendLine("<PaymentTypeCode>" + dict.Key.ToString() + "</PaymentTypeCode>");
            sbOnlinePaymentXMLDetail.AppendLine("<PaymentTypeId>" + obj_nTRPMapping.PaymentTypeId.ToString() + "</PaymentTypeId>");
            if (Convert.ToInt32(dict.Key.ToString()) == 2 || Convert.ToInt32(dict.Key.ToString()) == 3)
            {
                sbOnlinePaymentXMLDetail.AppendLine("<AmountValue>" + decA_GWAmountValue.ToString() + "</AmountValue>");
                sbOnlinePaymentXMLDetail.AppendLine("<ArearAmount>" + decA_ArearAmount.ToString() + "</ArearAmount>");
            }
            else
                sbOnlinePaymentXMLDetail.AppendLine("<AmountValue>" + dict.Value.ToString() + "</AmountValue>");
            sbOnlinePaymentXMLDetail.AppendLine("<OrderContent>" + ConfigurationManager.AppSettings["OrderContent"].ToString() + "</OrderContent>");

            sbOnlinePaymentXMLDetail.AppendLine("</SADOnlinePaymentDetails>");
        }
        //sbOnlinePaymentXMLDetail.AppendLine("</SADOnlinePaymentDetail>");
        if (obj_OnlinePayment.Add(sbOnlinePaymentXMLDetail) == 1)
        {
            obj_OnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(obj_OnlinePayment.ApplicationCode, obj_OnlinePayment.OrderPaymentCode);
        }
        else
        {
            obj_OnlinePayment = null;
        }
        return obj_OnlinePayment;
    }
    public static NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails SADOnlinePaymentDetails(NOCAP.BLL.Misc.Payment.SADOnlinePayment obj_OnlinePayment, decimal decA_AmountValue, decimal decA_GWAmountValue, decimal? decA_ArearAmount, string OrderContent, string PaymentTypeCode, long ExternalUserCode)
    {
        NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(ExternalUserCode);
        NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails obj_OnlinePaymentDetails = new NOCAP.BLL.Misc.Payment.SADOnlinePaymentDetails();
        NOCAP.BLL.Master.NTRPMapping obj_NTRPMapping = null;
        string[] arr_OrderContent = OrderContent.Split(',');
        string[] arr_PaymentTypeCode = PaymentTypeCode.Split(',');

        for (int i = 0; i <= arr_OrderContent.Length - 1; i++)
        {
            obj_NTRPMapping = new NOCAP.BLL.Master.NTRPMapping(Convert.ToInt32(arr_OrderContent[i]), Convert.ToInt32(arr_PaymentTypeCode[i]));
            obj_OnlinePaymentDetails.AppCode = obj_OnlinePayment.ApplicationCode;

            obj_OnlinePaymentDetails.OrderPaymentCode = obj_OnlinePayment.OrderPaymentCode;
            obj_OnlinePaymentDetails.PaymentTypeCode = Convert.ToInt32(arr_PaymentTypeCode[i]);
            obj_OnlinePaymentDetails.PaymentTypeId = obj_NTRPMapping.PaymentTypeId;

            if (Convert.ToInt32(arr_PaymentTypeCode[i]) == 2 || Convert.ToInt32(arr_PaymentTypeCode[i]) == 3)
            {
                obj_OnlinePaymentDetails.ArearAmount = decA_ArearAmount;
                obj_OnlinePaymentDetails.AmountValue = decA_GWAmountValue;
            }
            else { obj_OnlinePaymentDetails.AmountValue = decA_AmountValue; }
            obj_OnlinePaymentDetails.OrderContent = Convert.ToInt32(arr_OrderContent[i]);
            obj_OnlinePaymentDetails.CreatedByExUC = ExternalUserCode;

            if (obj_OnlinePaymentDetails.Add() == 1)
            {

            }
            else
            {
                obj_OnlinePaymentDetails = null;
                break;
            }
        }

        return obj_OnlinePaymentDetails;
    }
    public static int FillRadioButtonListPaymentMode(ref RadioButtonList chk_PaymentType)
    {

        NOCAP.BLL.Master.ProcessingFeePaymentMode obj_PaymentType = new NOCAP.BLL.Master.ProcessingFeePaymentMode();

        NOCAP.BLL.Master.ProcessingFeePaymentMode[] arr;
        int int_status = 0;
        try
        {
            chk_PaymentType.Items.Clear();
            obj_PaymentType.GetAll(NOCAP.BLL.Common.CommonEnum.VisibilityYesNo.Yes);
            arr = obj_PaymentType.ProcessingFeePaymentModeCollection;
            if (arr != null && arr.Count() > 0)
            {
                chk_PaymentType.DataSource = arr;
                chk_PaymentType.DataTextField = "ProcessingFeePayModeDesc";
                chk_PaymentType.DataValueField = "ProcessingFeePayModeCode";
                chk_PaymentType.DataBind();
            }
            int_status = 1;
            // AddFirstItemInCheckBoxList(ref chk_PaymentType);
            return int_status;
        }
        catch (Exception)
        {
            int_status = 0;
            // AddFirstItemInCheckBoxList(ref chk_PaymentType);
            return int_status;
        }

    }


    public static int WMPDownloadFiles(int int_StateCode, int int_DistrictCode, int int_SubDistrictCode, int SN)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Master.WMPAttachment obj_WMPAttachment = new NOCAP.BLL.Master.WMPAttachment();
            // NOCAP.BLL.GroundWaterChargeRec.GroundWaterChargesRec obj_GroundWaterChargesRec = new NOCAP.BLL.GroundWaterChargeRec.GroundWaterChargesRec();
            obj_WMPAttachment = obj_WMPAttachment.DownloadWMPAttachment(int_StateCode, int_DistrictCode, int_SubDistrictCode, SN);
            if (obj_WMPAttachment != null)
            {
                byte[] bytes = obj_WMPAttachment.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = obj_WMPAttachment.ContentType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "WMPAttachment_" + Convert.ToString(int_StateCode) + "_" + Convert.ToString(int_DistrictCode) + "_" + Convert.ToString(int_SubDistrictCode) + "_" + obj_WMPAttachment.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();

                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }

    }
    public static int WMPViewFiles(int int_StateCode, int int_DistrictCode, int int_SubDistrictCode, int SN)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Master.WMPAttachment obj_WMPAttachment = new NOCAP.BLL.Master.WMPAttachment();
            obj_WMPAttachment = obj_WMPAttachment.DownloadWMPAttachment(int_StateCode, int_DistrictCode, int_SubDistrictCode, SN);
            if (obj_WMPAttachment != null)
            {
                byte[] bytes = obj_WMPAttachment.AttachmentFile;


                if (obj_WMPAttachment.ContentType == "application/pdf" || obj_WMPAttachment.ContentType == "image/jpeg")
                {
                    if (HttpContext.Current.Request.Browser.Browser.ToLower().Contains("internetexplorer"))
                        System.Web.HttpContext.Current.Session["DownloadCounter"] = 2;
                    else
                        System.Web.HttpContext.Current.Session["DownloadCounter"] = 1;
                    System.Web.HttpContext.Current.Session["nocapbytes"] = bytes;
                    System.Web.HttpContext.Current.Session["ContentType"] = obj_WMPAttachment.ContentType;
                    HttpContext.Current.Server.Transfer("~/Sub/WMP/DownloadPDFFile.aspx", false);
                }
                else
                {
                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.Charset = "";
                    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    HttpContext.Current.Response.ContentType = obj_WMPAttachment.ContentType;
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "WMP_" + Convert.ToString(int_StateCode) + "_" + Convert.ToString(int_DistrictCode) + "_" + Convert.ToString(int_SubDistrictCode) + "_" + Convert.ToString(SN) + obj_WMPAttachment.FileExtension);
                    HttpContext.Current.Response.BinaryWrite(bytes);
                    HttpContext.Current.Response.Flush();
                }

                intResult = 1;
            }
            return intResult;
        }
        catch (System.Threading.ThreadAbortException)
        {
            return 0;
        }
        catch (Exception ex)
        {
            return 0;
        }

    }

    public static int CommReqDownloadFiles(long lng_AppCode, int int_attachmentCode, int int_attachmentCodeSerialNumber)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Misc.Communication.CommuniRequestAttachment obj_CommuniRequestAttachment = new NOCAP.BLL.Misc.Communication.CommuniRequestAttachment();
            NOCAP.BLL.Common.CommunicationAttachementB obj_CommunicationAttachementB = obj_CommuniRequestAttachment.DownloadFile(lng_AppCode, int_attachmentCode, int_attachmentCodeSerialNumber);

            if (obj_CommunicationAttachementB != null)
            {
                byte[] bytes = obj_CommunicationAttachementB.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = obj_CommunicationAttachementB.ContentType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "CommReq_" + Convert.ToString(lng_AppCode) + "_" + Convert.ToString(int_attachmentCode) + "_" + Convert.ToString(int_attachmentCodeSerialNumber) + obj_CommunicationAttachementB.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();

                intResult = 1;
            }
            return intResult;
        }
        catch (Exception ex)
        {
            return 0;
        }

    }
    public static int CommReplyDownloadFiles(long lng_AppCode, int int_attachmentCode, int int_attachmentCodeSerialNumber)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Misc.Communication.CommuniReplyAttachment obj_CommuniReplyAttachment = new NOCAP.BLL.Misc.Communication.CommuniReplyAttachment();
            NOCAP.BLL.Common.CommunicationAttachementB obj_CommunicationAttachementB = obj_CommuniReplyAttachment.DownloadFile(lng_AppCode, int_attachmentCode, int_attachmentCodeSerialNumber);


            if (obj_CommunicationAttachementB != null)
            {
                byte[] bytes = obj_CommunicationAttachementB.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = obj_CommunicationAttachementB.ContentType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "CommReq_" + Convert.ToString(lng_AppCode) + "_" + Convert.ToString(int_attachmentCode) + "_" + Convert.ToString(int_attachmentCodeSerialNumber) + obj_CommunicationAttachementB.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();

                intResult = 1;
            }
            return intResult;
        }
        catch (Exception ex)
        {
            return 0;
        }

    }
    public static int FillDropDownAreaTypeCategory(ref DropDownList ddlA_areaTypeCatDropDown, int intA_areaTypeCode)
    {

        int int_status = 0;
        try
        {
            NOCAP.BLL.Master.AreaType obj_areaType = new NOCAP.BLL.Master.AreaType(intA_areaTypeCode);
            ddlA_areaTypeCatDropDown.Items.Clear();
            NOCAP.BLL.Master.AreaTypeCategory[] arr;
            arr = obj_areaType.GetAreaTypeCategoryList(NOCAP.BLL.Master.AreaType.SortingFieldForAreaTypeCategory.AreaTypeCategoryDesc);
            if (arr.Count() > 0)
            {
                ddlA_areaTypeCatDropDown.DataSource = arr;
                ddlA_areaTypeCatDropDown.DataTextField = "AreaTypeCategoryDesc";
                ddlA_areaTypeCatDropDown.DataValueField = "AreaTypeCategoryCode";
                ddlA_areaTypeCatDropDown.DataBind();
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddlA_areaTypeCatDropDown);
            return int_status;
        }
        catch
        {

            int_status = 0;
            return int_status;
        }


    }
    public static int FillDropDownUserLevel(ref DropDownList ddl_userLevel)
    {
        NOCAP.BLL.UserManagement.UserLevel obj_userLevel = new NOCAP.BLL.UserManagement.UserLevel();
        NOCAP.BLL.UserManagement.UserLevel[] arr_userLevel;

        int int_status = 0;
        try
        {
            ddl_userLevel.Items.Clear();

            if (obj_userLevel.GetAll(NOCAP.BLL.UserManagement.UserLevel.SortingField.UserLevelDesc) == 1)
            {
                arr_userLevel = obj_userLevel.UserLevelCollection;

                if (arr_userLevel.Count() > 0)
                {
                    ddl_userLevel.DataSource = arr_userLevel;
                    ddl_userLevel.DataTextField = "UserLevelDesc";
                    ddl_userLevel.DataValueField = "UserLevelCode";
                    ddl_userLevel.DataBind();
                }
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddl_userLevel);
            return int_status;
        }
        catch
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_userLevel);
            return int_status;
        }
    }
    public static int FillDropDownRegionalOffice(ref DropDownList ddl_regionalOffice)
    {
        NOCAP.BLL.Master.RegionalOffice obj_regionalOffice = new NOCAP.BLL.Master.RegionalOffice();
        NOCAP.BLL.Master.RegionalOffice[] arr_regionalOffice;

        int int_status = 0;
        try
        {
            ddl_regionalOffice.Items.Clear();

            if (obj_regionalOffice.GetAll(NOCAP.BLL.Master.RegionalOffice.SortingField.RegionalOfficeName) == 1)
            {
                arr_regionalOffice = obj_regionalOffice.RegionalOfficeCollection;

                if (arr_regionalOffice.Count() > 0)
                {
                    ddl_regionalOffice.DataSource = arr_regionalOffice;
                    ddl_regionalOffice.DataTextField = "RegionalOfficeName";
                    ddl_regionalOffice.DataValueField = "RegionalOfficeCode";
                    ddl_regionalOffice.DataBind();
                }
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddl_regionalOffice);
            return int_status;
        }
        catch
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_regionalOffice);
            return int_status;
        }
    }
    public static int FillDropDownHeadQuarter(ref DropDownList ddl_headQuarter)
    {
        NOCAP.BLL.Master.HeadQuarter obj_headQuarter = new NOCAP.BLL.Master.HeadQuarter();
        NOCAP.BLL.Master.HeadQuarter[] arr_headQuarter;

        int int_status = 0;
        try
        {
            ddl_headQuarter.Items.Clear();

            if (obj_headQuarter.GetAll(NOCAP.BLL.Master.HeadQuarter.SortingField.HQName) == 1)
            {
                arr_headQuarter = obj_headQuarter.HeadQuarterCollection;

                if (arr_headQuarter.Count() > 0)
                {
                    ddl_headQuarter.DataSource = arr_headQuarter;
                    ddl_headQuarter.DataTextField = "HeadQuarterName";
                    ddl_headQuarter.DataValueField = "HQCode";
                    ddl_headQuarter.DataBind();
                }
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddl_headQuarter);
            return int_status;
        }
        catch
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_headQuarter);
            return int_status;
        }
    }




    public static int FillDropDownApplicationStatus(ref DropDownList ddl_applicationStatusDropDown)
    {
        NOCAP.BLL.Master.ApplicationStatus obj_applicationStatus = new NOCAP.BLL.Master.ApplicationStatus();
        NOCAP.BLL.Master.ApplicationStatus[] arr;
        int int_status = 0;
        try
        {
            ddl_applicationStatusDropDown.Items.Clear();

            if (obj_applicationStatus.GetAll(NOCAP.BLL.Master.ApplicationStatus.SortingField.ApplicationStatusDescription) == 1)
            {
                arr = obj_applicationStatus.ApplicationStatusCollection;

                if (arr.Count() > 0)
                {
                    ddl_applicationStatusDropDown.DataSource = arr;
                    ddl_applicationStatusDropDown.DataTextField = "ApplicationStatusDescription";
                    ddl_applicationStatusDropDown.DataValueField = "ApplicationStatusCode";
                    ddl_applicationStatusDropDown.DataBind();
                }
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddl_applicationStatusDropDown);
            return int_status;
        }
        catch
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_applicationStatusDropDown);
            return int_status;
        }
    }


    public static string DecimalFormat(double myNumber)
    {
        var s = string.Format("{0:0,0,0.0}", myNumber);
        // var s = string.Format("{0:#,0.00}", myNumber);

        if (s.EndsWith("00"))
        {
            return ((int)myNumber).ToString();
        }
        else
        {
            return s;
        }
    }
    public static string Create16DigitString()
    {
        var builder = new StringBuilder();
        Random RNG = new Random();
        while (builder.Length < 16)
        {
            builder.Append(RNG.Next(10).ToString());
        }
        //return builder.ToString().Substring(0,16);
        return Convert.ToInt64(builder.ToString().Substring(0, 16)).ToString();

    }

    public static string GetReportFolderName()
    {
        if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ReportFolderName"]))
        {
            return null;
        }
        else
        {
            return ConfigurationManager.AppSettings["ReportFolderName"].ToString();
        }
    }
    public static int MINRenScanLetterDownloadFiles(long int_appcode)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter objMiningRenewIssusedLetter = new NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter();
            NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter objMiningRenewIssusedLetterFile = objMiningRenewIssusedLetter.DownloadScanLetterFile(int_appcode);
            if (objMiningRenewIssusedLetterFile != null)
            {
                byte[] bytes = objMiningRenewIssusedLetterFile.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = HttpUtility.HtmlEncode(objMiningRenewIssusedLetterFile.ContentType);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "MINRenScan_" + Convert.ToString(int_appcode) + objMiningRenewIssusedLetterFile.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    //public static string Decrypt(string stringToDecrypt)//Decrypt the content
    //{

    //    byte[] key;
    //    byte[] IV;

    //    byte[] inputByteArray;
    //    try
    //    {

    //        key = Convert2ByteArray(DESKey);

    //        IV = Convert2ByteArray(DESIV);

    //        int len = stringToDecrypt.Length; inputByteArray = Convert.FromBase64String(stringToDecrypt);


    //        DESCryptoServiceProvider des = new DESCryptoServiceProvider();

    //        MemoryStream ms = new MemoryStream();

    //        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
    //        cs.Write(inputByteArray, 0, inputByteArray.Length);

    //        cs.FlushFinalBlock();

    //        Encoding encoding = Encoding.UTF8; return encoding.GetString(ms.ToArray());
    //    }

    //    catch (System.Exception ex)
    //    {

    //        throw ex;
    //    }
    //}

    //public static string Encrypt(string stringToEncrypt)// Encrypt the content
    //{

    //    byte[] key;
    //    byte[] IV;

    //    byte[] inputByteArray;
    //    try
    //    {

    //        key = Convert2ByteArray(DESKey);

    //        IV = Convert2ByteArray(DESIV);

    //        inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
    //        DESCryptoServiceProvider des = new DESCryptoServiceProvider();

    //        MemoryStream ms = new MemoryStream(); CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
    //        cs.Write(inputByteArray, 0, inputByteArray.Length);

    //        cs.FlushFinalBlock();

    //        return Convert.ToBase64String(ms.ToArray());
    //    }

    //    catch (System.Exception ex)
    //    {

    //        throw ex;
    //    }

    //}




    public static string Decrypt(string stringToDecrypt)//Decrypt the content
    {
        byte[] key;
        byte[] IV;

        byte[] inputByteArray;
        try
        {

            key = Convert2ByteArray(DESKey);
            IV = Convert2ByteArray(DESIV);
            int len = stringToDecrypt.Length; inputByteArray = Convert.FromBase64String(stringToDecrypt);

            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(key, IV), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                    }
                    stringToDecrypt = Encoding.UTF8.GetString(ms.ToArray());
                }
            }

            //Encoding encoding = Encoding.UTF8; return encoding.GetString(ms.ToArray());
            return stringToDecrypt;
        }

        catch (System.Exception ex)
        {

            throw ex;
        }
    }
    public static string Encrypt(string stringToEncrypt)// Encrypt the content
    {
        byte[] key;
        byte[] IV;

        byte[] inputByteArray;
        try
        {

            key = Convert2ByteArray(DESKey);
            IV = Convert2ByteArray(DESIV);
            inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);

            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(key, IV), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                    }

                    stringToEncrypt = Convert.ToBase64String(ms.ToArray());
                }
            }
            return stringToEncrypt;
        }

        catch (System.Exception ex)
        {

            throw ex;
        }

    }






    public static byte[] Convert2ByteArray(string strInput)
    {

        int intCounter; char[] arrChar;
        arrChar = strInput.ToCharArray();

        byte[] arrByte = new byte[arrChar.Length];

        for (intCounter = 0; intCounter <= arrByte.Length - 1; intCounter++)
            arrByte[intCounter] = Convert.ToByte(arrChar[intCounter]);

        return arrByte;
    }

    //public static bool IsValidFile(HttpPostedFile FileUpload)
    //{
    //    bool bolResult = false;
    //    System.IO.Stream fs = null;
    //    try
    //    {
    //        fs = FileUpload.InputStream;
    //        System.IO.StreamReader rs = null;
    //        rs = new System.IO.StreamReader(fs, true);
    //        string firstLine = rs.ReadLine().ToString();
    //        //string lastline = rs.ReadToEnd();
    //        switch (FileUpload.ContentType.ToString().ToLower())
    //        {
    //            case "application/msword":
    //                string lastline = rs.ReadToEnd();
    //                if (lastline.IndexOf("MSWordDoc") > -1 && ((FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "docx") || (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "doc")))
    //                {
    //                    bolResult = true;
    //                }
    //                break;
    //            case "application/pdf":
    //                if (firstLine.IndexOf("%PDF") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "pdf"))
    //                {
    //                    bolResult = true;
    //                }
    //                break;
    //            case "image/jpeg":
    //                if (firstLine.IndexOf("JFIF") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "jpeg"))
    //                {
    //                    bolResult = true;
    //                }
    //                else if (firstLine.IndexOf("JFIF") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "jpg"))
    //                {
    //                    bolResult = true;
    //                }
    //                break;
    //            case "text/plain":
    //                if ((FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "txt"))
    //                {
    //                    bolResult = true;
    //                }
    //                break;

    //            //case "image/gif":
    //            //    if (firstLine.IndexOf("GIF") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "gif"))
    //            //    {
    //            //        bolResult = true;
    //            //    }
    //            //    break;
    //            //case "image/jfif":
    //            //    if (firstLine.IndexOf("JFIF") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "jfif"))
    //            //    {
    //            //        bolResult = true;
    //            //    }
    //            //    break;

    //            //case "image/jpg":
    //            //    if (firstLine.IndexOf("jpg") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "jpg"))
    //            //    {
    //            //        bolResult = true;
    //            //    }
    //            //    break;
    //        }

    //        return bolResult;
    //    }
    //    catch (Exception)
    //    {
    //        return false;
    //    }
    //    finally
    //    {
    //        fs.Position = 0;
    //    }
    //}

    public static int HideEntriesInDropDown(ref DropDownList ddl_sourceDropDown, string strValue)
    {
        int int_status = 0;
        try
        {
            string[] spString = strValue.Split(',');
            for (int SplitVal = 0; SplitVal < spString.Length; SplitVal++)
            {
                for (int ddlValue = 0; ddlValue < ddl_sourceDropDown.Items.Count; ddlValue++)
                {
                    if (ddl_sourceDropDown.Items[ddlValue].Value != "")
                    {
                        int intddlSelectValue = Convert.ToInt32(ddl_sourceDropDown.Items[ddlValue].Value);
                        int intRemoveValue = Convert.ToInt32(spString[SplitVal]);
                        if (intddlSelectValue == intRemoveValue)
                        {
                            ddl_sourceDropDown.Items.RemoveAt(ddlValue);

                        }
                    }

                }
            }
            return int_status;
        }
        catch
        {
            int_status = 0;

            return int_status;
        }
    }

    public static int INFScanLetterDownloadFiles(long int_appcode)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter objInfrastructureNewIssusedLetter = new NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter();
            NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter objInfrastructureNewIssusedLetterFile = objInfrastructureNewIssusedLetter.DownloadScanLetterFile(int_appcode);
            if (objInfrastructureNewIssusedLetterFile != null)
            {
                byte[] bytes = objInfrastructureNewIssusedLetterFile.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = objInfrastructureNewIssusedLetterFile.ContentType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "INFScan_" + Convert.ToString(int_appcode) + objInfrastructureNewIssusedLetterFile.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public static int MINScanLetterDownloadFiles(long int_appcode)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter objMiningNewIssusedLetter = new NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter();
            NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter objMiningNewIssusedLetterFile = objMiningNewIssusedLetter.DownloadScanLetterFile(int_appcode);
            if (objMiningNewIssusedLetterFile != null)
            {
                byte[] bytes = objMiningNewIssusedLetterFile.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = objMiningNewIssusedLetterFile.ContentType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "MINScan_" + Convert.ToString(int_appcode) + objMiningNewIssusedLetterFile.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public static bool IsValidFile(HttpPostedFile FileUpload)
    {
        bool bolResult = false;
        System.IO.Stream fs = null;
        try
        {
            fs = FileUpload.InputStream;
            System.IO.StreamReader rs = null;
            rs = new System.IO.StreamReader(fs, true);
            string firstLine = rs.ReadLine().ToString();

            string[] strFileExtenson = FileUpload.FileName.Split('.');

            if (strFileExtenson.Length > 2)
            {
                return false;
            }
            switch (FileUpload.ContentType.ToString().ToLower())
            {
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                    string lastlinedocx = rs.ReadToEnd();
                    if (lastlinedocx.IndexOf("docProps") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "docx"))
                    {
                        bolResult = true;
                    }
                    break;
                case "application/msword":
                    string lastline = rs.ReadToEnd();
                    if (lastline.IndexOf("MSWordDoc") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "doc"))
                    {
                        bolResult = true;
                    }
                    break;
                case "application/pdf":
                case "application/x-download":
                    if (firstLine.IndexOf("%PDF") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "pdf"))
                    {
                        bolResult = true;
                    }
                    break;
                case "image/jpeg":
                    if (firstLine.IndexOf("JFIF") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "jpeg"))
                    {
                        bolResult = true;
                    }
                    else if (firstLine.IndexOf("JFIF") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "jpg"))
                    {
                        bolResult = true;
                    }
                    break;
                case "text/plain":
                    if ((FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "txt"))
                    {
                        bolResult = true;
                    }
                    break;

                    //case "image/gif":
                    //    if (firstLine.IndexOf("GIF") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "gif"))
                    //    {
                    //        bolResult = true;
                    //    }
                    //    break;
                    //case "image/jfif":
                    //    if (firstLine.IndexOf("JFIF") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "jfif"))
                    //    {
                    //        bolResult = true;
                    //    }
                    //    break;

                    //case "image/jpg":
                    //    if (firstLine.IndexOf("jpg") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "jpg"))
                    //    {
                    //        bolResult = true;
                    //    }
                    //    break;
            }

            return bolResult;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            fs.Position = 0;
        }
    }
    public static bool IsValidFileWithExcel(HttpPostedFile FileUpload)
    {
        bool bolResult = false;
        System.IO.Stream fs = null;
        try
        {
            fs = FileUpload.InputStream;
            System.IO.StreamReader rs = null;
            rs = new System.IO.StreamReader(fs, true);
            string firstLine = rs.ReadLine().ToString();

            string[] strFileExtenson = FileUpload.FileName.Split('.');

            if (strFileExtenson.Length > 2)
            {
                return false;
            }
            switch (FileUpload.ContentType.ToString().ToLower())
            {
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                    string lastlinedocx = rs.ReadToEnd();
                    if (lastlinedocx.IndexOf("docProps") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "docx"))
                    {
                        bolResult = true;
                    }
                    break;
                case "application/msword":
                    string lastline = rs.ReadToEnd();
                    if (lastline.IndexOf("MSWordDoc") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "doc"))
                    {
                        bolResult = true;
                    }
                    break;
                case "application/pdf":
                case "application/x-download":
                    if (firstLine.IndexOf("%PDF") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "pdf"))
                    {
                        bolResult = true;
                    }
                    break;
                case "image/jpeg":
                    if (firstLine.IndexOf("JFIF") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "jpeg"))
                    {
                        bolResult = true;
                    }
                    else if (firstLine.IndexOf("JFIF") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "jpg"))
                    {
                        bolResult = true;
                    }
                    break;
                case "text/plain":
                    if ((FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "txt"))
                    {
                        bolResult = true;
                    }
                    break;
                case "application/vnd.ms-excel":
                    if ((FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "xls"))
                    {
                        bolResult = true;
                    }
                    break;
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                    if ((FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "xlsx"))
                    {
                        bolResult = true;
                    }
                    break;


                    //case "image/gif":
                    //    if (firstLine.IndexOf("GIF") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "gif"))
                    //    {
                    //        bolResult = true;
                    //    }
                    //    break;
                    //case "image/jfif":
                    //    if (firstLine.IndexOf("JFIF") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "jfif"))
                    //    {
                    //        bolResult = true;
                    //    }
                    //    break;

                    //case "image/jpg":
                    //    if (firstLine.IndexOf("jpg") > -1 && (FileUpload.FileName.Split('.')[FileUpload.FileName.Split('.').Length - 1].ToString().ToLower() == "jpg"))
                    //    {
                    //        bolResult = true;
                    //    }
                    //    break;
            }

            return bolResult;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            fs.Position = 0;
        }
    }

    public static bool IsValidDate(string Expression)
    {
        DateTime d;
        bool txtDtHearingvalid = false;
        txtDtHearingvalid = DateTime.TryParseExact(Expression, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
        return txtDtHearingvalid;
    }

    // IsNumeric Function
    public static bool IsNumeric(object Expression)
    {
        // Variable to collect the Return value of the TryParse method.
        bool isNum;

        // Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
        double retNum;

        // The TryParse method converts a string in a specified style and culture-specific format to its double-precision floating point number equivalent.
        // The TryParse method does not generate an exception if the conversion fails. If the conversion passes, True is returned. If it does not, False is returned.
        isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        return isNum;
    }




    public static int EquivalentApplicationTypeCodeOfDatabaseForApplicationType(string strA_applicationTypeDesc)
    {
        //string str_equivalentRole;
        //str_equivalentRole = "";
        int int_equivalentApplicationTypeCode;
        int_equivalentApplicationTypeCode = -1;
        try
        {
            switch (strA_applicationTypeDesc)
            {

                case "Domestic":
                    int_equivalentApplicationTypeCode = 1;
                    break;
                case "Industrial":
                    int_equivalentApplicationTypeCode = 2;
                    break;
                case "IndustrialG":
                    int_equivalentApplicationTypeCode = 2;
                    break;
                case "Infrastructure":
                    int_equivalentApplicationTypeCode = 3;
                    break;
                case "InfrastructureG":
                    int_equivalentApplicationTypeCode = 3;
                    break;
                case "Mining":
                    int_equivalentApplicationTypeCode = 4;
                    break;
                case "MiningG":
                    int_equivalentApplicationTypeCode = 4;
                    break;
                default:
                    int_equivalentApplicationTypeCode = -1;
                    break;
            }
            return int_equivalentApplicationTypeCode;
        }
        catch (Exception)
        {
            int_equivalentApplicationTypeCode = -1;
            return int_equivalentApplicationTypeCode;
        }


    }




    public static int EquivalentApplicationPurposeCodeOfDatabaseForApplicationPurpose(string strA_applicationPurposeDesc)
    {
        //string str_equivalentRole;
        //str_equivalentRole = "";
        int int_equivalentApplicationPurposeCode;
        int_equivalentApplicationPurposeCode = -1;
        try
        {
            switch (strA_applicationPurposeDesc)
            {

                case "New":
                    int_equivalentApplicationPurposeCode = 1;
                    break;
                case "Renewal":
                    int_equivalentApplicationPurposeCode = 2;
                    break;
                case "Withdrawal":
                    int_equivalentApplicationPurposeCode = 3;
                    break;
                case "Cancellation":
                    int_equivalentApplicationPurposeCode = 4;
                    break;
                default:
                    int_equivalentApplicationPurposeCode = -1;
                    break;
            }
            return int_equivalentApplicationPurposeCode;
        }
        catch (Exception)
        {
            int_equivalentApplicationPurposeCode = -1;
            return int_equivalentApplicationPurposeCode;
        }



    }

    public static int SelectForItemData(ref DropDownList ddl_DropDownList, string strValue)
    {
        int functionReturnValue = 0;
        long lNoOfItems = 0;
        int i = 0;
        lNoOfItems = ddl_DropDownList.Items.Count;
        if (lNoOfItems <= 0)
        {
            functionReturnValue = 0;
            return functionReturnValue;
        }
        lNoOfItems = lNoOfItems - 1;
        ddl_DropDownList.ClearSelection();
        for (i = 0; i <= lNoOfItems; i++)
        {
            if (ddl_DropDownList.Items[i].Value == strValue)
            {
                ddl_DropDownList.Items[i].Selected = true;
                functionReturnValue = 1;
                return functionReturnValue;
            }
        }

        functionReturnValue = 0;
        return functionReturnValue;
    }

    public static int AddFirstItemInDropDownList(ref DropDownList ddl_DropDownList)
    {
        try
        {

            ListItem ps = new ListItem();
            ps.Value = "";
            ps.Text = "--Select--";
            ps.Selected = true;
            ddl_DropDownList.Items.Insert(0, ps);
            return 1;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public static int AddFirstItemInCheckBoxList(ref CheckBoxList ddl_CheckBoxList)
    {
        try
        {

            ListItem ps = new ListItem();
            ps.Value = "";
            ps.Text = "None";
            ps.Selected = true;
            ddl_CheckBoxList.Items.Insert(0, ps);
            return 1;
        }
        catch (Exception)
        {
            return 0;
        }
    }


    public static int FillDropDownTitle(ref DropDownList ddl_title)
    {

        NOCAP.BLL.Master.Title obj_title = new NOCAP.BLL.Master.Title();
        NOCAP.BLL.Master.Title[] arr_title;

        int int_status = 0;
        try
        {
            ddl_title.Items.Clear();

            if (obj_title.GetALL(NOCAP.BLL.Master.Title.SortingField.TitleDesc) == 1)
            {
                arr_title = obj_title.TitleCollection;

                if (arr_title.Count() > 0)
                {
                    ddl_title.DataSource = arr_title;
                    ddl_title.DataTextField = "TitleDesc";
                    ddl_title.DataValueField = "TitleCode";
                    ddl_title.DataBind();
                }
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddl_title);
            return int_status;
        }
        catch (Exception)
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_title);
            return int_status;
        }
    }

    public static int FillDropDownGender(ref DropDownList ddl_genderDropDown)
    {
        NOCAP.BLL.Master.Gender obj_gender = new NOCAP.BLL.Master.Gender();
        NOCAP.BLL.Master.Gender[] arr;
        int int_status = 0;
        try
        {
            ddl_genderDropDown.Items.Clear();

            if (obj_gender.GetALL(NOCAP.BLL.Master.Gender.SortingField.GenderDesc) == 1)
            {
                arr = obj_gender.GenderCollection;

                if (arr.Count() > 0)
                {
                    ddl_genderDropDown.DataSource = arr;
                    ddl_genderDropDown.DataTextField = "GenderDesc";
                    ddl_genderDropDown.DataValueField = "GenderCode";
                    ddl_genderDropDown.DataBind();
                }
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddl_genderDropDown);
            return int_status;
        }
        catch (Exception)
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_genderDropDown);
            return int_status;
        }
    }
    public static int FillDropDownIdProofType(ref DropDownList ddl_idProofTypeDropDown)
    {
        NOCAP.BLL.Master.IDProofType obj_iDProofType = new NOCAP.BLL.Master.IDProofType();
        NOCAP.BLL.Master.IDProofType[] arr;
        int int_status = 0;
        try
        {
            ddl_idProofTypeDropDown.Items.Clear();

            if (obj_iDProofType.GetALL(NOCAP.BLL.Master.IDProofType.SortingField.IDProofTypeDesc) == 1)
            {
                arr = obj_iDProofType.IDProofTypeCollection;

                if (arr.Count() > 0)
                {
                    ddl_idProofTypeDropDown.DataSource = arr;
                    ddl_idProofTypeDropDown.DataTextField = "IDProofTypeDesc";
                    ddl_idProofTypeDropDown.DataValueField = "IDProofTypeCode";
                    ddl_idProofTypeDropDown.DataBind();
                }
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddl_idProofTypeDropDown);
            return int_status;
        }
        catch (Exception)
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_idProofTypeDropDown);
            return int_status;
        }
    }

    public static int FillDropDownApplicationTypeBaseApplicationNumbar(ref DropDownList ddlA_ApplicationNumbarDropDown, int intA_ApplicationNumbarCode, int sessionCode)
    {

        int int_status = 0;
        try
        {
            ddlA_ApplicationNumbarDropDown.Items.Clear();

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(sessionCode);
            if (intA_ApplicationNumbarCode == 2)
            {
                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication[] arrIndAppCount = obj_externalUser.GetSubmittedIndustrialNewApplicationList();

                if (arrIndAppCount.Count() > 0)
                {
                    ddlA_ApplicationNumbarDropDown.DataSource = arrIndAppCount;
                    ddlA_ApplicationNumbarDropDown.DataTextField = "IndustrialNewApplicationNumber";
                    ddlA_ApplicationNumbarDropDown.DataValueField = "IndustrialNewApplicationCode";
                    ddlA_ApplicationNumbarDropDown.DataBind();
                    int_status = 1;
                }
            }
            else if (intA_ApplicationNumbarCode == 3)
            {
                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication[] arrInfAppCount = obj_externalUser.GetSubmittedInfrastructureNewApplicationList();
                if (arrInfAppCount.Count() > 0)
                {
                    ddlA_ApplicationNumbarDropDown.DataSource = arrInfAppCount;
                    ddlA_ApplicationNumbarDropDown.DataTextField = "InfrastructureNewApplicationNumber";
                    ddlA_ApplicationNumbarDropDown.DataValueField = "InfrastructureNewApplicationCode";
                    ddlA_ApplicationNumbarDropDown.DataBind();
                    int_status = 1;
                }
            }
            else if (intA_ApplicationNumbarCode == 4)
            {
                NOCAP.BLL.Mining.New.Application.MiningNewApplication[] arrMinAppCount = obj_externalUser.GetSubmittedMiningNewApplicationList();
                if (arrMinAppCount.Count() > 0)
                {
                    ddlA_ApplicationNumbarDropDown.DataSource = arrMinAppCount;
                    ddlA_ApplicationNumbarDropDown.DataTextField = "MiningNewApplicationNumber";
                    ddlA_ApplicationNumbarDropDown.DataValueField = "ApplicationCode";
                    ddlA_ApplicationNumbarDropDown.DataBind();
                    int_status = 1;
                }
            }

            AddFirstItemInDropDownList(ref ddlA_ApplicationNumbarDropDown);
            return int_status;
        }
        catch
        {

            int_status = 0;
            return int_status;
        }


    }

    public static int FillDropDownAreaType(ref DropDownList ddl_areaTypeDropDown)
    {
        NOCAP.BLL.Master.AreaType obj_areaType = new NOCAP.BLL.Master.AreaType();
        NOCAP.BLL.Master.AreaType[] arr;
        int int_status = 0;
        try
        {
            ddl_areaTypeDropDown.Items.Clear();

            if (obj_areaType.GetAll(NOCAP.BLL.Master.AreaType.SortingField.AreaTypeDesc) == 1)
            {
                arr = obj_areaType.AreaTypeCollection;

                if (arr.Count() > 0)
                {
                    ddl_areaTypeDropDown.DataSource = arr;
                    ddl_areaTypeDropDown.DataTextField = "AreaTypeDesc";
                    ddl_areaTypeDropDown.DataValueField = "AreaTypeCode";
                    ddl_areaTypeDropDown.DataBind();
                }
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddl_areaTypeDropDown);
            return int_status;
        }
        catch
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_areaTypeDropDown);
            return int_status;
        }
    }
    public static int FillDropDownAreaTypeCategoryBasedOnAreaType(ref DropDownList ddlA_areaTypeCategoryDropDown, int intA_AreaTypeCategoryCode)
    {

        int int_status = 0;
        try
        {
            NOCAP.BLL.Master.AreaType obj_areaType = new NOCAP.BLL.Master.AreaType(intA_AreaTypeCategoryCode);
            ddlA_areaTypeCategoryDropDown.Items.Clear();
            NOCAP.BLL.Master.AreaTypeCategory[] arr;
            arr = obj_areaType.GetAreaTypeCategoryList(NOCAP.BLL.Master.AreaType.SortingFieldForAreaTypeCategory.AreaTypeCategoryDesc);
            if (arr.Count() > 0)
            {
                ddlA_areaTypeCategoryDropDown.DataSource = arr;
                ddlA_areaTypeCategoryDropDown.DataTextField = "AreaTypeCategoryDesc";
                ddlA_areaTypeCategoryDropDown.DataValueField = "AreaTypeCategoryCode";
                ddlA_areaTypeCategoryDropDown.DataBind();
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddlA_areaTypeCategoryDropDown);
            return int_status;
        }
        catch
        {

            int_status = 0;
            return int_status;
        }


    }








    public static int FillDropDownApplicationPurpose(ref DropDownList ddl_applicationPurposeDropDown)
    {
        NOCAP.BLL.Master.ApplicationPurpose obj_applicationPurpose = new NOCAP.BLL.Master.ApplicationPurpose();
        NOCAP.BLL.Master.ApplicationPurpose[] arr;
        int int_status = 0;
        try
        {
            ddl_applicationPurposeDropDown.Items.Clear();

            if (obj_applicationPurpose.GetALL(NOCAP.BLL.Master.ApplicationPurpose.SortingField.ApplicationPurposeDesc) == 1)
            {
                arr = obj_applicationPurpose.ApplicationPurposeCollection;

                if (arr.Count() > 0)
                {
                    ddl_applicationPurposeDropDown.DataSource = arr;
                    ddl_applicationPurposeDropDown.DataTextField = "ApplicationPurposeDesc";
                    ddl_applicationPurposeDropDown.DataValueField = "ApplicationPurposeCode";
                    ddl_applicationPurposeDropDown.DataBind();
                }
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddl_applicationPurposeDropDown);
            return int_status;
        }
        catch
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_applicationPurposeDropDown);
            return int_status;
        }
    }



    public static int FillDropDownApplicationTypeCategory(ref DropDownList ddlA_appTypeCategoryDropDown, int intA_applicationTypeCode)
    {

        int int_status = 0;
        try
        {
            NOCAP.BLL.Master.ApplicationType obj_applicationType = new NOCAP.BLL.Master.ApplicationType(intA_applicationTypeCode);

            ddlA_appTypeCategoryDropDown.Items.Clear();
            NOCAP.BLL.Master.ApplicationTypeCategory[] arr;
            arr = obj_applicationType.GetApplicationTypeCategoryList(NOCAP.BLL.Master.ApplicationType.SortingFieldForApplicationTypeCategory.ApplicationTypeCategoryDesc);
            arr = arr.Where(v => v.Visibility == NOCAP.BLL.Master.ApplicationTypeCategory.VisibilityYesNo.Yes).ToArray();
            if (arr.Count() > 0)
            {
                ddlA_appTypeCategoryDropDown.DataSource = arr;
                ddlA_appTypeCategoryDropDown.DataTextField = "ApplicationTypeCategoryDesc";
                ddlA_appTypeCategoryDropDown.DataValueField = "ApplicationTypeCategoryCode";
                ddlA_appTypeCategoryDropDown.DataBind();

            }
            int_status = 1;
            NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlA_appTypeCategoryDropDown);
            return int_status;
        }
        catch (Exception ex)
        {
            string dsfsd = ex.Message;
            //Response.Write(ApplicationTypeCategory.CustumMessage);
            int_status = 0;
            return int_status;
        }


    }








    //public static int FillDropDownState(ref DropDownList ddl_stateDropDown)
    //{
    //    NOCAP.BLL.Master.State obj_state = new NOCAP.BLL.Master.State();
    //    NOCAP.BLL.Master.State[] arr;
    //    int int_status = 0;
    //    try
    //    {
    //        ddl_stateDropDown.Items.Clear();

    //        if (obj_state.GetAll(NOCAP.BLL.Master.State.SortingField.StateName) == 1)
    //        {
    //            arr = obj_state.StateCollection;

    //            if (arr.Count() > 0)
    //            {
    //                ddl_stateDropDown.DataSource = arr;
    //                ddl_stateDropDown.DataTextField = "StateName";
    //                ddl_stateDropDown.DataValueField = "StateCode";
    //                ddl_stateDropDown.DataBind();
    //            }
    //            int_status = 1;
    //        }
    //        else
    //        {
    //            int_status = 1;
    //        }

    //        AddFirstItemInDropDownList(ref ddl_stateDropDown);
    //        return int_status;
    //    }
    //    catch (Exception)
    //    {
    //        int_status = 0;
    //        AddFirstItemInDropDownList(ref ddl_stateDropDown);
    //        return int_status;
    //    }

    //}
    public static int FillDropDownState(ref DropDownList ddl_stateDropDown, NOCAP.BLL.Common.CommonEnum.YesNoOption enuA_IsApplicable = NOCAP.BLL.Common.CommonEnum.YesNoOption.NotDefined)
    {
        NOCAP.BLL.Master.State obj_state = new NOCAP.BLL.Master.State();
        NOCAP.BLL.Master.State[] arr;
        int int_status = 0;
        try
        {
            ddl_stateDropDown.Items.Clear();

            obj_state.IsApplicable = enuA_IsApplicable;

            if (obj_state.GetAll(NOCAP.BLL.Master.State.SortingField.StateName) == 1)
            {
                arr = obj_state.StateCollection;

                if (arr.Count() > 0)
                {
                    ddl_stateDropDown.DataSource = arr;
                    ddl_stateDropDown.DataTextField = "StateName";
                    ddl_stateDropDown.DataValueField = "StateCode";
                    ddl_stateDropDown.DataBind();
                }
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddl_stateDropDown);
            return int_status;
        }
        catch (Exception)
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_stateDropDown);
            return int_status;
        }

    }


    public static int FillDropDownDistrict(ref DropDownList ddlA_districtDropDown, int intA_stateCode)
    {

        int int_status = 0;
        try
        {
            NOCAP.BLL.Master.State obj_State = new NOCAP.BLL.Master.State(intA_stateCode);
            ddlA_districtDropDown.Items.Clear();
            NOCAP.BLL.Master.District[] arr;
            arr = obj_State.GetDistrictList(NOCAP.BLL.Master.State.SortingFieldForDistrict.DistrictName);
            if (arr.Count() > 0)
            {
                ddlA_districtDropDown.DataSource = arr;
                ddlA_districtDropDown.DataTextField = "DistrictName";
                ddlA_districtDropDown.DataValueField = "DistrictCode";
                ddlA_districtDropDown.DataBind();
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlA_districtDropDown);
            return int_status;
        }
        catch (Exception)
        {
            //Response.Write(ApplicationTypeCategory.CustumMessage);
            int_status = 0;
            return int_status;
        }


    }

    public static int FillDropDownSubDistrict(ref DropDownList ddlB_subDistrictDropDown, int intA_districtCode, int intA_stateCode)
    {

        int int_status = 0;
        try
        {
            NOCAP.BLL.Master.District obj_SubDistrict = new NOCAP.BLL.Master.District(intA_stateCode, intA_districtCode);
            ddlB_subDistrictDropDown.Items.Clear();
            NOCAP.BLL.Master.SubDistrict[] arr;
            arr = obj_SubDistrict.GetSubDistrictList(NOCAP.BLL.Master.District.SortingFieldForSubDistrict.SubDistrictName);
            if (arr.Count() > 0)
            {
                ddlB_subDistrictDropDown.DataSource = arr;
                ddlB_subDistrictDropDown.DataTextField = "SubDistrictName";
                ddlB_subDistrictDropDown.DataValueField = "SubDistrictCode";
                ddlB_subDistrictDropDown.DataBind();
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddlB_subDistrictDropDown);
            return int_status;
        }
        catch (Exception)
        {

            int_status = 0;
            return int_status;
        }


    }
    public static int FillDropDownTownOrVillage(ref DropDownList ddl_townOrVillageDropDown)
    {
        //NOCAP.BLL.Master.State obj_state = new NOCAP.BLL.Master.State();
        //NOCAP.BLL.Master.State[] arr;
        int int_status = 0;
        try
        {
            ddl_townOrVillageDropDown.Items.Clear();

            //if (obj_state.GetAll(NOCAP.BLL.Master.State.SortingField.StateName) == 1)
            //{
            //    arr = obj_state.StateCollection;

            //    if (arr.Count() > 0)
            //    {
            //        ddl_stateDropDown.DataSource = arr;
            //        ddl_stateDropDown.DataTextField = "StateName";
            //        ddl_stateDropDown.DataValueField = "StateCode";
            //        ddl_stateDropDown.DataBind();
            //    }
            //    int_status = 1;
            //}
            //else
            //{
            //    int_status = 1;
            //}
            ListItem ps = new ListItem();
            ps.Value = "T";
            ps.Text = "Town";
            ddl_townOrVillageDropDown.Items.Insert(0, ps);
            ps = new ListItem();
            ps.Value = "V";
            ps.Text = "Village";
            ddl_townOrVillageDropDown.Items.Insert(0, ps);

            int_status = 1;
            AddFirstItemInDropDownList(ref ddl_townOrVillageDropDown);
            return int_status;
        }
        catch (Exception)
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_townOrVillageDropDown);
            return int_status;
        }

    }

    public static int FillDropDownVillage(ref DropDownList ddlAr_villageDropDown, int intA_subDistrictCode, int intA_districtCode, int intA_stateCode)
    {

        int int_status = 0;
        try
        {
            NOCAP.BLL.Master.SubDistrict obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(intA_stateCode, intA_districtCode, intA_subDistrictCode);
            ddlAr_villageDropDown.Items.Clear();
            NOCAP.BLL.Master.Village[] arr;
            arr = obj_subDistrict.GetVillageList(NOCAP.BLL.Master.SubDistrict.SortingFieldForVillage.VillageName);
            if (arr.Count() > 0)
            {
                ddlAr_villageDropDown.DataSource = arr;
                ddlAr_villageDropDown.DataTextField = "VillageName";
                ddlAr_villageDropDown.DataValueField = "VillageCode";
                ddlAr_villageDropDown.DataBind();
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddlAr_villageDropDown);
            return int_status;
        }
        catch (Exception)
        {

            int_status = 0;
            return int_status;
        }




    }
    public static int FillDropDownTown(ref DropDownList ddlAr_townDropDown, int intA_subDistrictCode, int intA_districtCode, int intA_stateCode)
    {

        int int_status = 0;
        try
        {
            NOCAP.BLL.Master.SubDistrict obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(intA_stateCode, intA_districtCode, intA_subDistrictCode);
            ddlAr_townDropDown.Items.Clear();
            NOCAP.BLL.Master.Town[] arr;
            arr = obj_subDistrict.GetTownList(NOCAP.BLL.Master.SubDistrict.SortingFieldForTown.TownName);
            if (arr.Count() > 0)
            {
                ddlAr_townDropDown.DataSource = arr;
                ddlAr_townDropDown.DataTextField = "TownName";
                ddlAr_townDropDown.DataValueField = "TownCode";
                ddlAr_townDropDown.DataBind();
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddlAr_townDropDown);
            return int_status;
        }
        catch (Exception)
        {

            int_status = 0;
            return int_status;
        }


    }
    public static int FillDropDownTypeOfStructure(ref DropDownList ddl_typeOfStructureDropDown)
    {
        NOCAP.BLL.Master.TypeOfAbstractionStructure obj_typeOfStructure = new NOCAP.BLL.Master.TypeOfAbstractionStructure();

        NOCAP.BLL.Master.TypeOfAbstractionStructure[] arr;
        int int_status = 0;
        try
        {
            ddl_typeOfStructureDropDown.Items.Clear();

            if (obj_typeOfStructure.GetALL(NOCAP.BLL.Master.TypeOfAbstractionStructure.SortingField.TypeOfAbstractionStructureDesc) == 1)
            {
                arr = obj_typeOfStructure.TypeOfAbstractionStructureCollection;

                if (arr.Count() > 0)
                {
                    ddl_typeOfStructureDropDown.DataSource = arr;
                    ddl_typeOfStructureDropDown.DataTextField = "TypeOfAbstractionStructureDesc";
                    ddl_typeOfStructureDropDown.DataValueField = "TypeOfAbstractionStructureCode";
                    ddl_typeOfStructureDropDown.DataBind();
                }
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddl_typeOfStructureDropDown);
            return int_status;
        }
        catch (Exception)
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_typeOfStructureDropDown);
            return int_status;
        }

    }
    public static int FillDropDownTypeOfARStructure(ref DropDownList ddl_typeOfStructureDropDown)
    {
        NOCAP.BLL.Master.TypeOfARStructure obj_typeOfStructure = new NOCAP.BLL.Master.TypeOfARStructure();

        NOCAP.BLL.Master.TypeOfARStructure[] arr;
        int int_status = 0;
        try
        {
            ddl_typeOfStructureDropDown.Items.Clear();

            if (obj_typeOfStructure.GetALL(NOCAP.BLL.Master.TypeOfARStructure.SortingField.TypeOfARStructureDesc) == 1)
            {
                arr = obj_typeOfStructure.TypeOfARStructureCollection;

                if (arr.Count() > 0)
                {
                    ddl_typeOfStructureDropDown.DataSource = arr;
                    ddl_typeOfStructureDropDown.DataTextField = "TypeOfARStructureDesc";
                    ddl_typeOfStructureDropDown.DataValueField = "TypeOfARStructureCode";
                    ddl_typeOfStructureDropDown.DataBind();
                }
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddl_typeOfStructureDropDown);
            return int_status;
        }
        catch (Exception)
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_typeOfStructureDropDown);
            return int_status;
        }

    }
    public static int FillCheckBoxListTypeOfARStructure(ref CheckBoxList ddl_typeOfStructureDropDown)
    {
        NOCAP.BLL.Master.TypeOfARStructure obj_typeOfStructure = new NOCAP.BLL.Master.TypeOfARStructure();

        NOCAP.BLL.Master.TypeOfARStructure[] arr;
        int int_status = 0;
        try
        {
            ddl_typeOfStructureDropDown.Items.Clear();

            if (obj_typeOfStructure.GetALL(NOCAP.BLL.Master.TypeOfARStructure.SortingField.TypeOfARStructureDesc) == 1)
            {
                arr = obj_typeOfStructure.TypeOfARStructureCollection;

                if (arr.Count() > 0)
                {
                    ddl_typeOfStructureDropDown.DataSource = arr;
                    ddl_typeOfStructureDropDown.DataTextField = "TypeOfARStructureDesc";
                    ddl_typeOfStructureDropDown.DataValueField = "TypeOfARStructureCode";
                    ddl_typeOfStructureDropDown.DataBind();
                }
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            //  AddFirstItemInCheckBoxList(ref ddl_typeOfStructureDropDown);
            return int_status;
        }
        catch (Exception)
        {
            int_status = 0;
            //  AddFirstItemInCheckBoxList(ref ddl_typeOfStructureDropDown);
            return int_status;
        }

    }

    public static int FillDropDownLiftingDevice(ref DropDownList ddl_liftingDeviceDropDown)
    {
        NOCAP.BLL.Master.LiftingDevice obj_liftingDevice = new NOCAP.BLL.Master.LiftingDevice();

        NOCAP.BLL.Master.LiftingDevice[] arr;
        int int_status = 0;
        try
        {
            ddl_liftingDeviceDropDown.Items.Clear();

            if (obj_liftingDevice.GetALL(NOCAP.BLL.Master.LiftingDevice.SortingField.LiftingDeviceDesc) == 1)
            {
                arr = obj_liftingDevice.LiftingDeviceCollection;

                if (arr.Count() > 0)
                {
                    ddl_liftingDeviceDropDown.DataSource = arr;
                    ddl_liftingDeviceDropDown.DataTextField = "LiftingDeviceDesc";
                    ddl_liftingDeviceDropDown.DataValueField = "LiftingDeviceCode";
                    ddl_liftingDeviceDropDown.DataBind();
                }
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddl_liftingDeviceDropDown);
            return int_status;
        }
        catch (Exception)
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_liftingDeviceDropDown);
            return int_status;
        }

    }

    public static int FillDropDownReferralLetterType(ref DropDownList ddl_referralLetterType)
    {

        NOCAP.BLL.Master.ReferralLetterType obj_referralLetterType = new NOCAP.BLL.Master.ReferralLetterType();
        NOCAP.BLL.Master.ReferralLetterType[] arr_referralLetterType;

        int int_status = 0;
        try
        {
            ddl_referralLetterType.Items.Clear();

            if (obj_referralLetterType.GetAll(NOCAP.BLL.Master.ReferralLetterType.SortingField.ReferralLetterTypeDesc) == 1)
            {
                arr_referralLetterType = obj_referralLetterType.ReferralLetterTypeCollection;

                if (arr_referralLetterType.Count() > 0)
                {
                    ddl_referralLetterType.DataSource = arr_referralLetterType;
                    ddl_referralLetterType.DataTextField = "ReferralLetterTypeDesc";
                    ddl_referralLetterType.DataValueField = "ReferralLetterTypeCode";
                    ddl_referralLetterType.DataBind();
                }
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }

            AddFirstItemInDropDownList(ref ddl_referralLetterType);
            return int_status;
        }
        catch (Exception)
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_referralLetterType);
            return int_status;
        }
    }

    public static int INDExpansionAppDownloadFiles(long lng_industrialNewApplicationCode, int int_attachmentCode, int int_attachmentCodeSerialNumber)
    {
        try
        {
            int intResult = 0;

            NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplicationAttachment objIndustrialExpansionApplicationAttachment = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplicationAttachment();
            //NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationAttachmentB objIndustrialNewApplicationAttachmentB = new NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationAttachmentB();
            //objIndustrialNewApplicationAttachmentB = objIndustrialNewSADApplicationAttachmentDAL.populateINDAttachmentForINDAttachmentFile(lng_industrialNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
            NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationAttachmentB objIndustrialNewApplicationAttachmentB = objIndustrialExpansionApplicationAttachment.DownloadFile(lng_industrialNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

            if (objIndustrialNewApplicationAttachmentB != null)
            {
                byte[] bytes = objIndustrialNewApplicationAttachmentB.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = objIndustrialNewApplicationAttachmentB.ContentType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "IND_" + Convert.ToString(lng_industrialNewApplicationCode) + "_" + Convert.ToString(int_attachmentCode) + "_" + Convert.ToString(int_attachmentCodeSerialNumber) + objIndustrialNewApplicationAttachmentB.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();

                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }

    }

    public static int INDSADAppDownloadFiles(long lng_industrialNewApplicationCode, int int_attachmentCode, int int_attachmentCodeSerialNumber)
    {
        try
        {
            int intResult = 0;

            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment objIndustrialNewSADApplicationAttachment = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplicationAttachment();
            //NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationAttachmentB objIndustrialNewApplicationAttachmentB = new NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationAttachmentB();
            //objIndustrialNewApplicationAttachmentB = objIndustrialNewSADApplicationAttachmentDAL.populateINDAttachmentForINDAttachmentFile(lng_industrialNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
            NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationAttachmentB objIndustrialNewApplicationAttachmentB = objIndustrialNewSADApplicationAttachment.DownloadFile(lng_industrialNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

            if (objIndustrialNewApplicationAttachmentB != null)
            {
                byte[] bytes = objIndustrialNewApplicationAttachmentB.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = objIndustrialNewApplicationAttachmentB.ContentType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "IND_" + Convert.ToString(lng_industrialNewApplicationCode) + "_" + Convert.ToString(int_attachmentCode) + "_" + Convert.ToString(int_attachmentCodeSerialNumber) + objIndustrialNewApplicationAttachmentB.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();

                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }

    }
    public static int SelfcompDownloadFiles(long lng_applicationCode, int int_attachmentCode, int int_attachmentCodeSerialNumber)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachment = new NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment();

            NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment obj_selfComplianceAttachmentB = obj_selfComplianceAttachment.DownloadFile(lng_applicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

            if (obj_selfComplianceAttachmentB != null)
            {
                byte[] bytes = obj_selfComplianceAttachmentB.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = obj_selfComplianceAttachmentB.ContentType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "Self_" + Convert.ToString(lng_applicationCode) + "_" + Convert.ToString(int_attachmentCode) + "_" + Convert.ToString(int_attachmentCodeSerialNumber) + obj_selfComplianceAttachmentB.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();

                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }

    }
    public static int SelfInsepDownloadFiles(long lng_applicationCode, int int_attachmentCode, int int_attachmentCodeSerialNumber)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment obj_selfInspectionAttachment = new NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment();

            NOCAP.BLL.Misc.SelfInspection.SelfInspectionAttachment obj_selfInspectionAttachmentB = obj_selfInspectionAttachment.DownloadFile(lng_applicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

            if (obj_selfInspectionAttachmentB != null)
            {
                byte[] bytes = obj_selfInspectionAttachmentB.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = obj_selfInspectionAttachmentB.ContentType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "Self_" + Convert.ToString(lng_applicationCode) + "_" + Convert.ToString(int_attachmentCode) + "_" + Convert.ToString(int_attachmentCodeSerialNumber) + obj_selfInspectionAttachmentB.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();

                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }

    }

    public static int INFSADAppDownloadFiles(long lng_infrastructureNewApplicationCode, int int_attachmentCode, int int_attachmentCodeSerialNumber)
    {
        try
        {
            int intResult = 0;

            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment objInfrastructureNewSADApplicationAttachment = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplicationAttachment();

            NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationAttachmentB objInfrastructureNewApplicationAttachmentB = objInfrastructureNewSADApplicationAttachment.DownloadFile(lng_infrastructureNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

            if (objInfrastructureNewApplicationAttachmentB != null)
            {
                byte[] bytes = objInfrastructureNewApplicationAttachmentB.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = objInfrastructureNewApplicationAttachmentB.ContentType;

                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "INF_" + Convert.ToString(lng_infrastructureNewApplicationCode) + "_" + Convert.ToString(int_attachmentCode) + "_" + Convert.ToString(int_attachmentCodeSerialNumber) + objInfrastructureNewApplicationAttachmentB.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();

                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }

    }

    public static byte[] StreamFile(HttpPostedFile FileUpload)
    {
        try
        {
            byte[] bytes = null;
            using (var memoryStream = new MemoryStream())
            {
                FileUpload.InputStream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }
            return bytes;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static int INDLetterAppDownloadFiles(long int_appcode)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter objIndustrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter();

            NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter objIndustrialNewIssusedLetter1 = objIndustrialNewIssusedLetter.DownloadFile(int_appcode);
            if (objIndustrialNewIssusedLetter1 != null)
            {
                byte[] bytes = objIndustrialNewIssusedLetter1.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = objIndustrialNewIssusedLetter1.ContentType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "INDLetter_" + Convert.ToString(int_appcode) + objIndustrialNewIssusedLetter1.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }

    }
    //public static byte[] INDScanLetterFiles(long int_appcode)
    //{
    //    try
    //    {
    //        byte[] bytes = null;
    //        NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter objIndustrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter();
    //        NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter objIndustrialNewIssusedLetterFile = objIndustrialNewIssusedLetter.DownloadScanLetterFile(int_appcode);
    //        if (objIndustrialNewIssusedLetterFile != null)
    //        {
    //            bytes = objIndustrialNewIssusedLetterFile.AttachmentFile;



    //        }
    //        return bytes;
    //    }
    //    catch (Exception)
    //    {
    //        return null;
    //    }
    //}
    //public static byte[] INFScanLetterFiles(long int_appcode)
    //{
    //    try
    //    {
    //        byte[] bytes = null;
    //        NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_InfrastructureNewIssusedLetter = new NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter();
    //        NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_InfrastructureNewIssusedLetterFile = obj_InfrastructureNewIssusedLetter.DownloadScanLetterFile(int_appcode);
    //        if (obj_InfrastructureNewIssusedLetterFile != null)
    //        {
    //            bytes = obj_InfrastructureNewIssusedLetterFile.AttachmentFile;
    //        }
    //        return bytes;
    //    }
    //    catch (Exception)
    //    {
    //        return null;
    //    }
    //}
    //public static byte[] MINScanLetterFiles(long int_appcode)
    //{
    //    try
    //    {
    //        byte[] bytes = null;
    //        NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter obj_MiningNewIssusedLetter = new NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter();
    //        NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter obj_MiningNewIssusedLetterFile = obj_MiningNewIssusedLetter.DownloadScanLetterFile(int_appcode);
    //        if (obj_MiningNewIssusedLetterFile != null)
    //        {
    //            bytes = obj_MiningNewIssusedLetterFile.AttachmentFile;
    //        }
    //        return bytes;
    //    }
    //    catch (Exception)
    //    {
    //        return null;
    //    }
    //}
    public static int INDScanLetterDownloadFiles(long int_appcode)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter objIndustrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter();
            NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter objIndustrialNewIssusedLetterFile = objIndustrialNewIssusedLetter.DownloadScanLetterFile(int_appcode);
            if (objIndustrialNewIssusedLetterFile != null)
            {
                byte[] bytes = objIndustrialNewIssusedLetterFile.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = HttpUtility.HtmlEncode(objIndustrialNewIssusedLetterFile.ContentType);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "INDScan_" + Convert.ToString(int_appcode) + objIndustrialNewIssusedLetterFile.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public static int INFLetterAppDownloadFiles(long int_appcode)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter objInfrastructureNewIssusedLetter = new NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter();
            NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter objInfrastructureNewIssusedLetter1 = objInfrastructureNewIssusedLetter.DownloadFile(int_appcode);
            if (objInfrastructureNewIssusedLetter1 != null)
            {
                byte[] bytes = objInfrastructureNewIssusedLetter1.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = HttpUtility.HtmlEncode(objInfrastructureNewIssusedLetter1.ContentType);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "INFLetter_" + Convert.ToString(int_appcode) + objInfrastructureNewIssusedLetter1.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public static int MINLetterAppDownloadFiles(long int_appcode)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter objMiningNewIssusedLetter = new NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter();
            NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter objMiningNewIssusedLetter1 = objMiningNewIssusedLetter.DownloadFile(int_appcode);
            if (objMiningNewIssusedLetter1 != null)
            {
                byte[] bytes = objMiningNewIssusedLetter1.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = HttpUtility.HtmlEncode(objMiningNewIssusedLetter1.ContentType);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "MINLetter_" + Convert.ToString(int_appcode) + objMiningNewIssusedLetter1.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }

    }

    public static int MINSADAppDownloadFiles(long lng_miningNewApplicationCode, int int_attachmentCode, int int_attachmentCodeSerialNumber)
    {
        try
        {
            int intResult = 0;

            NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment objMiningNewSADApplicationAttachment = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplicationAttachment();
            //NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationAttachmentB objIndustrialNewApplicationAttachmentB = new NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationAttachmentB();
            //objIndustrialNewApplicationAttachmentB = objIndustrialNewSADApplicationAttachmentDAL.populateINDAttachmentForINDAttachmentFile(lng_industrialNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
            NOCAP.BLL.Mining.New.Common.MiningNewApplicationAttachmentB objMiningNewApplicationAttachmentB = objMiningNewSADApplicationAttachment.DownloadFile(lng_miningNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

            if (objMiningNewApplicationAttachmentB != null)
            {
                byte[] bytes = objMiningNewApplicationAttachmentB.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = HttpUtility.HtmlEncode(objMiningNewApplicationAttachmentB.ContentType);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "IND_" + Convert.ToString(lng_miningNewApplicationCode) + "_" + Convert.ToString(int_attachmentCode) + "_" + Convert.ToString(int_attachmentCodeSerialNumber) + objMiningNewApplicationAttachmentB.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();

                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }

    }
    public static string GetRegionalOfficeAddress(int StateCode)
    {
        NOCAP.BLL.Master.State regstate = new NOCAP.BLL.Master.State(StateCode);
        NOCAP.BLL.Master.RegionalOffice regOffice = new NOCAP.BLL.Master.RegionalOffice();
        regOffice = regstate.GetAssociatedRegionalOffice();
        string RegionalOfficeAddress = string.Empty;
        RegionalOfficeAddress = RegionalOfficeAddress + regOffice.AuthorisedOfficer + "\n";
        if (!string.IsNullOrEmpty(regOffice.RegionalOfficeName))
        {
            RegionalOfficeAddress = RegionalOfficeAddress + regOffice.RegionalOfficeName + "\n";
        }
        if (!string.IsNullOrEmpty(regOffice.AddressLine1))
        {
            RegionalOfficeAddress = RegionalOfficeAddress + regOffice.AddressLine1 + "\n";
        }
        if (!string.IsNullOrEmpty(regOffice.AddressLine2))
        {
            RegionalOfficeAddress = RegionalOfficeAddress + regOffice.AddressLine2 + "\n";
        }
        if (!string.IsNullOrEmpty(regOffice.AddressLine3))
        {
            RegionalOfficeAddress = RegionalOfficeAddress + regOffice.AddressLine3 + "\n";
        }
        if (!string.IsNullOrEmpty(regOffice.GetAddressDistrictName()))
        {
            RegionalOfficeAddress = RegionalOfficeAddress + regOffice.GetAddressDistrictName() + "\n";
        }
        if (!string.IsNullOrEmpty(regOffice.GetAddressStateName()))
        {
            RegionalOfficeAddress = RegionalOfficeAddress + regOffice.GetAddressStateName() + "\n";
        }
        if (regOffice.PinCode != 0)
        {
            RegionalOfficeAddress = RegionalOfficeAddress + "PinCode : " + regOffice.PinCode + "\n";
        }
        return RegionalOfficeAddress;
    }

    public static int AttachmentSizeLimitofID()
    {
        try
        {
            int AttachmentSize = 1024 * 300;//300KB
            return AttachmentSize;
        }
        catch
        {
            return 0;
        }

    }

    public static int FillDropDownWaterQuality(ref DropDownList ddl_WaterQualityDropDown, NOCAP.BLL.Master.WaterQuality.VisibilityYesNo enum_Visibility = NOCAP.BLL.Master.WaterQuality.VisibilityYesNo.NotDefined)
    {
        NOCAP.BLL.Master.WaterQuality obj_WaterQuality = new NOCAP.BLL.Master.WaterQuality();
        NOCAP.BLL.Master.WaterQuality[] arr;
        int int_status = 0;
        try
        {
            ddl_WaterQualityDropDown.Items.Clear();
            arr = obj_WaterQuality.GetAll(enum_Visibility, NOCAP.BLL.Master.WaterQuality.SortingField.WaterQualityDesc);
            if (arr != null && arr.Count() > 0)
            {
                ddl_WaterQualityDropDown.DataSource = arr;
                ddl_WaterQualityDropDown.DataTextField = "WaterQualityDesc";
                ddl_WaterQualityDropDown.DataValueField = "WaterQualityCode";
                ddl_WaterQualityDropDown.DataBind();
            }
            int_status = 1;
            AddFirstItemInDropDownList(ref ddl_WaterQualityDropDown);
            return int_status;
        }
        catch (Exception)
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_WaterQualityDropDown);
            return int_status;
        }

    }

    public static int FillDropDownComplianceCondition(ref DropDownList ddlA_complianceConditionDropDown)
    {

        int int_status = 0;
        try
        {
            NOCAP.BLL.Master.ComplianceConditionNOC obj_ComplianceConditionNOC = new NOCAP.BLL.Master.ComplianceConditionNOC();
            int_status = obj_ComplianceConditionNOC.GetList(NOCAP.BLL.Master.ComplianceConditionNOC.VisibilityYesNo.Yes, NOCAP.BLL.Master.ComplianceConditionNOC.SortingField.ComplianceConditionDescription); NOCAP.BLL.Master.ComplianceConditionNOC[] arr_ComplianceConditionNOC;
            arr_ComplianceConditionNOC = obj_ComplianceConditionNOC.ComplianceConditionNOCCollection;

            if (int_status == 1 && arr_ComplianceConditionNOC.Count() > 0)
            {
                ddlA_complianceConditionDropDown.DataSource = arr_ComplianceConditionNOC;
                ddlA_complianceConditionDropDown.DataTextField = "ComplianceConditionDescription";
                ddlA_complianceConditionDropDown.DataValueField = "ComplianceConditionCode";
                ddlA_complianceConditionDropDown.DataBind();
                int_status = 1;
            }
            NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlA_complianceConditionDropDown);
            return int_status;
        }
        catch (Exception)
        {
            int_status = 0;
            return int_status;
        }
    }

    public static string GetRandomNumber()
    {
        try
        {
            string[] arrStr = "1,2,3,4,5,6,7,8,9,0".Split(",".ToCharArray());
            string strNum = string.Empty;
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                strNum += arrStr[r.Next(0, arrStr.Length - 1)];
            }
            return strNum;
        }
        catch (Exception)
        {
            return "";
        }
    }

    public static string GetRandomString()
    {
        //string[] arrStr = "A,B,C,D,1,2,3,4,5,6,7,8,9,0".Split(",".ToCharArray());
        string[] arrStr = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,1,2,3,4,5,6,7,8,9,0".Split(",".ToCharArray());
        string strDraw = string.Empty;
        Random r = new Random();
        for (int i = 0; i < 6; i++)
        {
            strDraw += arrStr[r.Next(0, arrStr.Length - 1)];
        }
        return strDraw;
    }

    public static string Number2Word(long lNumber)
    {
        string[] ones = {"One ","Two ","Three ","Four ","Five ","Six ","Seven ","Eight ","Nine ","Ten ",
                           "Eleven ","Twelve ","Thirteen ","Fourteen ","Fifteen ","Sixteen ","Seventeen ","Eighteen ","Ninteen "
                         };
        string[] tens = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninty " };

        //if 

        if (lNumber == 0)
            return ("");
        if (lNumber < 0)
        {

            lNumber *= -1;
        }
        if (lNumber < 20)
        {
            return ones[lNumber - 1];
        }
        if (lNumber <= 99)
        {
            return tens[(lNumber / 10) - 2] + Number2Word(lNumber % 10);
        }
        if (lNumber < 1000)
        {
            return Number2Word(lNumber / 100) + "Hundred " + Number2Word(lNumber % 100);
        }
        if (lNumber < 100000)
        {
            return Number2Word(lNumber / 1000) + "Thousand " + Number2Word(lNumber % 1000);
        }
        if (lNumber < 10000000)
        {
            return Number2Word(lNumber / 100000) + "Lakh " + Number2Word(lNumber % 100000);
        }
        if (lNumber < 1000000000)
        {
            return Number2Word(lNumber / 10000000) + "Crore " + Number2Word(lNumber % 10000000);
        }
        if (lNumber < 100000000000)
        {
            return Number2Word(lNumber / 1000000000) + "Arab " + Number2Word(lNumber % 1000000000);
        }
        if (lNumber < 10000000000000)
        {
            return Number2Word(lNumber / 100000000000) + "Kharab " + Number2Word(lNumber % 100000000000);
        }
        return "";
    }

    public static string ParseInput(string Numbr)
    {
        long lnumb = 0;
        string Paisa = "";
        string debit = "";
        Numbr = Numbr.Trim();
        if (Numbr != "")
        {
            if (Numbr.Substring(0, 1) == "-")
                debit = " DB";
            int decimalPlace = Numbr.IndexOf(".");
            if (decimalPlace > 0)
            {

                lnumb = long.Parse(Numbr.Substring(0, decimalPlace));
                Paisa = Numbr.Substring(decimalPlace + 1) + "00";
                Paisa = Paisa.Substring(0, 2);
                Paisa = Number2Word(long.Parse(Paisa));
                if (Paisa.Trim() != "")
                    Paisa = " And " + Paisa + "Paise ";
            }
            else
                lnumb = long.Parse(Numbr.Trim());
        }

        return Convert.ToString("Rupees ") + Convert.ToString(Number2Word(lnumb)) + Convert.ToString(Paisa) + Convert.ToString("Only") + debit;
    }

    public static string AddOrdinal(int num)
    {
        if (num <= 0) return num.ToString();

        switch (num % 100)
        {
            case 11:
            case 12:
            case 13:
                return num + "th";
        }

        switch (num % 10)
        {
            case 1:
                return num + "st";
            case 2:
                return num + "nd";
            case 3:
                return num + "rd";
            default:
                return num + "th";
        }

    }

    public static int INDRenewSADAppDownloadFiles(long lng_industrialRenewApplicationCode, int int_attachmentCode, int int_attachmentCodeSerialNumber)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment objIndustrialRenewSADApplicationAttachment = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplicationAttachment();
            NOCAP.BLL.Industrial.Renew.Common.IndustrialRenewApplicationAttachmentB objIndustrialRenewApplicationAttachmentB = objIndustrialRenewSADApplicationAttachment.DownloadFile(lng_industrialRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

            if (objIndustrialRenewApplicationAttachmentB != null)
            {
                byte[] bytes = objIndustrialRenewApplicationAttachmentB.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = HttpUtility.HtmlEncode(objIndustrialRenewApplicationAttachmentB.ContentType);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "INDRenew_" + Convert.ToString(lng_industrialRenewApplicationCode) + "_" + Convert.ToString(int_attachmentCode) + "_" + Convert.ToString(int_attachmentCodeSerialNumber) + objIndustrialRenewApplicationAttachmentB.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();

                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public static int INFRenewSADAppDownloadFiles(long lng_InfrastructureRenewApplicationCode, int int_attachmentCode, int int_attachmentCodeSerialNumber)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment objInfrastructureRenewSADApplicationAttachment = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplicationAttachment();
            NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationAttachmentB objInfrastructureRenewApplicationAttachmentB = objInfrastructureRenewSADApplicationAttachment.DownloadFile(lng_InfrastructureRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

            if (objInfrastructureRenewApplicationAttachmentB != null)
            {
                byte[] bytes = objInfrastructureRenewApplicationAttachmentB.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = HttpUtility.HtmlEncode(objInfrastructureRenewApplicationAttachmentB.ContentType);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "INFRenew_" + Convert.ToString(lng_InfrastructureRenewApplicationCode) + "_" + Convert.ToString(int_attachmentCode) + "_" + Convert.ToString(int_attachmentCodeSerialNumber) + objInfrastructureRenewApplicationAttachmentB.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();

                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public static int INDRenScanLetterDownloadFiles(long int_appcode)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter objIndustrialRenewIssusedLetter = new NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter();
            NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter objIndustrialRenewIssusedLetterFile = objIndustrialRenewIssusedLetter.DownloadScanLetterFile(int_appcode);
            if (objIndustrialRenewIssusedLetterFile != null)
            {
                byte[] bytes = objIndustrialRenewIssusedLetterFile.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = HttpUtility.HtmlEncode(objIndustrialRenewIssusedLetterFile.ContentType);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "INDRenScan_" + Convert.ToString(int_appcode) + objIndustrialRenewIssusedLetterFile.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public static int INFRenScanLetterDownloadFiles(long int_appcode)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter objInfrastructureRenewIssusedLetter = new NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter();
            NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter objInfrastructureRenewIssusedLetterFile = objInfrastructureRenewIssusedLetter.DownloadScanLetterFile(int_appcode);
            if (objInfrastructureRenewIssusedLetterFile != null)
            {
                byte[] bytes = objInfrastructureRenewIssusedLetterFile.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = HttpUtility.HtmlEncode(objInfrastructureRenewIssusedLetterFile.ContentType);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "INFRenScan_" + Convert.ToString(int_appcode) + objInfrastructureRenewIssusedLetterFile.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public static int MINRenewSADAppDownloadFiles(long lng_MiningRenewApplicationCode, int int_attachmentCode, int int_attachmentCodeSerialNumber)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment objMiningRenewSADApplicationAttachment = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplicationAttachment();
            NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationAttachmentB objMiningRenewApplicationAttachmentB = objMiningRenewSADApplicationAttachment.DownloadFile(lng_MiningRenewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

            if (objMiningRenewApplicationAttachmentB != null)
            {
                byte[] bytes = objMiningRenewApplicationAttachmentB.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = HttpUtility.HtmlEncode(objMiningRenewApplicationAttachmentB.ContentType);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "MINRenew_" + Convert.ToString(lng_MiningRenewApplicationCode) + "_" + Convert.ToString(int_attachmentCode) + "_" + Convert.ToString(int_attachmentCodeSerialNumber) + objMiningRenewApplicationAttachmentB.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();

                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }
    }


    //Created By Amardeep 2 Apr 2019
    public static int INDRenLetterAppDownloadFiles(long int_appcode)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter objIndustrialRenewIssusedLetter = new NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter();
            NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter objIndustrialRenewIssusedLetter1 = objIndustrialRenewIssusedLetter.DownloadFile(int_appcode);
            if (objIndustrialRenewIssusedLetter1 != null)
            {
                byte[] bytes = objIndustrialRenewIssusedLetter1.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = objIndustrialRenewIssusedLetter1.ContentType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "INDLetter_" + Convert.ToString(int_appcode) + objIndustrialRenewIssusedLetter1.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }

    }
    public static int INFRenLetterAppDownloadFiles(long int_appcode)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter objInfrastructureRenewIssusedLetter = new NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter();
            NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter objInfrastructureRenewIssusedLetter1 = objInfrastructureRenewIssusedLetter.DownloadFile(int_appcode);
            if (objInfrastructureRenewIssusedLetter1 != null)
            {
                byte[] bytes = objInfrastructureRenewIssusedLetter1.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = HttpUtility.HtmlEncode(objInfrastructureRenewIssusedLetter1.ContentType);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "INFLetter_" + Convert.ToString(int_appcode) + objInfrastructureRenewIssusedLetter1.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public static int MINRenLetterAppDownloadFiles(long int_appcode)
    {
        try
        {
            int intResult = 0;
            NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter objMiningRenewIssusedLetter = new NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter();
            NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter objMiningRenewIssusedLetter1 = objMiningRenewIssusedLetter.DownloadFile(int_appcode);
            if (objMiningRenewIssusedLetter1 != null)
            {
                byte[] bytes = objMiningRenewIssusedLetter1.AttachmentFile;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = HttpUtility.HtmlEncode(objMiningRenewIssusedLetter1.ContentType);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "MINLetter_" + Convert.ToString(int_appcode) + objMiningRenewIssusedLetter1.FileExtension);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                intResult = 1;
            }
            return intResult;
        }
        catch (Exception)
        {
            return 0;
        }

    }
    public static int FillDropDownApplicationTypeCategoryBasedOnApplicationType(ref DropDownList ddlA_ApplicationTypeCategoryDropDown, int intA_ApplicationTypeCategoryCode)
    {
        int int_status = 0;
        try
        {
            NOCAP.BLL.Master.ApplicationType obj_applicationType = new NOCAP.BLL.Master.ApplicationType(intA_ApplicationTypeCategoryCode);
            ddlA_ApplicationTypeCategoryDropDown.Items.Clear();
            NOCAP.BLL.Master.ApplicationTypeCategory[] arr;
            arr = obj_applicationType.GetApplicationTypeCategoryList(NOCAP.BLL.Master.ApplicationTypeCategory.SortingFieldForApplicationTypeCategory.ApplicationTypeCategoryDesc);
            if (arr.Count() > 0)
            {
                ddlA_ApplicationTypeCategoryDropDown.DataSource = arr;
                ddlA_ApplicationTypeCategoryDropDown.DataTextField = "ApplicationTypeCategoryDesc";
                ddlA_ApplicationTypeCategoryDropDown.DataValueField = "ApplicationTypeCategoryCode";
                ddlA_ApplicationTypeCategoryDropDown.DataBind();
                int_status = 1;
            }
            else
            {
                int_status = 1;
            }
            AddFirstItemInDropDownList(ref ddlA_ApplicationTypeCategoryDropDown);
            return int_status;
        }
        catch
        {
            int_status = 0;
            return int_status;
        }
    }


    public static int FillDropDownMSMEType(ref DropDownList ddl_MSMETypeDropDown)
    {

        NOCAP.BLL.Master.MSMEType obj_MSMEType = new NOCAP.BLL.Master.MSMEType();

        NOCAP.BLL.Master.MSMEType[] arr;
        int int_status = 0;
        try
        {
            ddl_MSMETypeDropDown.Items.Clear();
            obj_MSMEType.GetAll();
            arr = obj_MSMEType.MSMETypeCollection;
            if (arr != null && arr.Count() > 0)
            {
                ddl_MSMETypeDropDown.DataSource = arr;
                ddl_MSMETypeDropDown.DataTextField = "MSMETypeDesc";
                ddl_MSMETypeDropDown.DataValueField = "MSMETypeCode";
                ddl_MSMETypeDropDown.DataBind();
            }
            int_status = 1;
            AddFirstItemInDropDownList(ref ddl_MSMETypeDropDown);
            return int_status;
        }
        catch (Exception)
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl_MSMETypeDropDown);
            return int_status;
        }

    }

    public static int FillDropDownMeterType(ref DropDownList ddl)
    {

        NOCAP.BLL.Master.MeterType obj_MeterType = new NOCAP.BLL.Master.MeterType();

        NOCAP.BLL.Master.MeterType[] arr;
        int int_status = 0;
        try
        {
            ddl.Items.Clear();
            obj_MeterType.GetAll();
            arr = obj_MeterType.MeterTypeCollection;
            if (arr != null && arr.Count() > 0)
            {
                ddl.DataSource = arr;
                ddl.DataTextField = "MeterTypeDesc";
                ddl.DataValueField = "MeterTypeCode";
                ddl.DataBind();
            }
            int_status = 1;
            AddFirstItemInDropDownList(ref ddl);
            return int_status;
        }
        catch (Exception)
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl);
            return int_status;
        }

    }
    public static int FillDropDownNameOfAgency(ref DropDownList ddl)
    {

        NOCAP.BLL.Master.InspectionAgency obj_MeterType = new NOCAP.BLL.Master.InspectionAgency();

        NOCAP.BLL.Master.InspectionAgency[] arr;
        int int_status = 0;
        try
        {
            ddl.Items.Clear();
            obj_MeterType.GetAll();
            arr = obj_MeterType.InspectionAgencyCollection;
            if (arr != null && arr.Count() > 0)
            {
                ddl.DataSource = arr;
                ddl.DataTextField = "InspectionAgencyDesc";
                ddl.DataValueField = "InspectionAgencyCode";
                ddl.DataBind();
            }
            int_status = 1;
            AddFirstItemInDropDownList(ref ddl);
            return int_status;
        }
        catch (Exception)
        {
            int_status = 0;
            AddFirstItemInDropDownList(ref ddl);
            return int_status;
        }

    }






}