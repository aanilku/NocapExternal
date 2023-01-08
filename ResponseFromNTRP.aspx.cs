using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Configuration;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_ResponseFromNTRP : System.Web.UI.Page
{
    private const string INFO_DIR = @"RequestDetails";
    public static int requestCount;

    string strPageName = "ResponseFromNTRP";
    string strActionName = "";
    string strStatus = "";
    NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication();
    NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication();
    NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication();
    NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication();
    NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication();
    NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication();



    NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_mainindustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
    NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_mainindustrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication();
    NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_maininfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
    NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_maininfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication();
    NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_mainminingNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication();
    NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_mainminingRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication();
    NOCAP.BLL.Misc.Payment.SADOnlinePayment obj_SADOnlinePayment = null;
    NOCAP.BLL.Misc.Payment.OnlinePayment obj_OnlinePayment = null;

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {

            BharatkoshResponse();



        }

    }
    private void BharatkoshResponse()
    {

        try
        {
            // Write request information to the file with HTML encoding.
            string s = Server.HtmlEncode(DateTime.Now.ToString());

            // </snippet2>
            XmlDocument xmlDoc = new XmlDocument();

            // <snippet3>
            // Iterate through the Form collection and write
            // the values to the file with HTML encoding.
            // String[] formArray = Request.Form.AllKeys;
            foreach (string ss in Request.Form.AllKeys)
            {
                s = Request["BharatkoshResponse"];
                string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(Request["BharatkoshResponse"]));
                xmlDoc.LoadXml(decodedAuthenticationToken);
                //xmlDoc.Save(Server.MapPath("BharatkoshResponse.xml"));
                //CspParameters cspParams = new CspParameters();
                //cspParams.KeyContainerName = "pkptrgapi.cga.gov.in";

                // Get the RSA key from the key container.  This key will decrypt
                // a symmetric key that was imbedded in the XML document.
                // RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider(cspParams);
                XmlNodeList elemList = NOCAPExternalUtility.GetRSAKey(xmlDoc);
                ResponseForSADOnlinePayment(elemList, xmlDoc);
                ResponseForOnlinePayment(elemList, xmlDoc);

            }
            if (obj_SADOnlinePayment.CreatedByExUC > 0)
                Session["ExternalUserCode"] = obj_SADOnlinePayment.CreatedByExUC;
            else
                Session["ExternalUserCode"] = obj_OnlinePayment.CreatedByExUC;
        }
        catch (Exception ex)
        {
            strStatus = ex.Message;
            lblMessage.Text = ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally
        {

            ActionTrail obj_ExtActionTrail = new ActionTrail();
            if (Session["ExternalUserCode"] != null)
            {
                obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
                obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
                obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
                obj_ExtActionTrail.Status = strStatus;
                if (obj_ExtActionTrail != null)
                    ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
            }

            SADOnlinePaymentActionTrail obj_SADOnlinePaymentActionTrail = new SADOnlinePaymentActionTrail();
            
            if (Session["ExternalUserCode"] != null && obj_SADOnlinePayment.CreatedByExUC > 0)
            {
                obj_SADOnlinePaymentActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
                obj_SADOnlinePaymentActionTrail.IP_Address = Request.UserHostAddress;
                obj_SADOnlinePaymentActionTrail.ActionPerformed = strPageName + "-" + strActionName;
                obj_SADOnlinePaymentActionTrail.Status = strStatus + ",OrderPaymentCode-" + Convert.ToString(obj_SADOnlinePayment.OrderPaymentCode) + ",App Code-" + Convert.ToString(obj_SADOnlinePayment.ApplicationCode);
                obj_SADOnlinePaymentActionTrail.AddSADOnlinePaymentAction(obj_SADOnlinePaymentActionTrail);
            }
            else
            {
                OnlinePaymentActionTrail obj_OnlinePaymentActionTrail = new OnlinePaymentActionTrail();
                obj_SADOnlinePaymentActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
                obj_SADOnlinePaymentActionTrail.IP_Address = Request.UserHostAddress;
                obj_SADOnlinePaymentActionTrail.ActionPerformed = strPageName + "-" + strActionName;
                obj_SADOnlinePaymentActionTrail.Status = strStatus + ",OrderPaymentCode-" + Convert.ToString(obj_OnlinePayment.OrderPaymentCode) + ",App Code-" + Convert.ToString(obj_OnlinePayment.ApplicationCode);
                obj_OnlinePaymentActionTrail.AddOnlinePaymentAction(obj_OnlinePaymentActionTrail);
            }
        }
    }
    private void ResponseForOnlinePayment(XmlNodeList elemList, XmlDocument xmlDoc)
    {
        XmlNodeList nodes = null;
        for (int i = 0; i < elemList.Count; i++)
        {
            obj_OnlinePayment = new NOCAP.BLL.Misc.Payment.OnlinePayment(elemList[i].Attributes["orderCode"].Value);
            lblOnlineOrderCode.Text = elemList[i].Attributes["orderCode"].Value;
            #region switch
            switch (elemList[i].Attributes["status"].Value.ToUpper())
            {
                #region cases
                case "SUCCESS":
                    obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.SUCCESS;
                    lblTransStatus.Text = "Success!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Green;
                    btnNext.Enabled = true;

                    // hPaymentSuccess.Visible = true;
                    break;
                case "FAIL":
                    obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.FAIL;
                    lblTransStatus.Text = "FAIL!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;

                    // hPaymentFail.Visible = true;
                    break;
                case "TERMINATE":
                    obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.TERMINATE;
                    lblTransStatus.Text = "FAIL!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;
                    break;
                case "CREATED":
                    obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.Created;
                    lblTransStatus.Text = "FAIL!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;
                    break;
                case "QUITED":
                    obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.Quited;
                    lblTransStatus.Text = "FAIL!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;
                    break;
                case "DELETED":
                    obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.Deleted;
                    lblTransStatus.Text = "FAIL!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;
                    break;
                case "HOLD":
                    obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.HOLD;
                    lblTransStatus.Text = "PENDING!/HOLD!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;
                    break;
                case "REINITIATION":
                    obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.ReInitiation;
                    lblTransStatus.Text = "PENDING!/REINITIATION!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;
                    break;
                case "STATUS UNKNOWN/ABORT":
                    obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.StatusUnknownAbort;
                    lblTransStatus.Text = "STATUS UNKNOWN/ABORT!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;

                    // hSTATUSUNKNOWN.Visible = true;
                    break;
                case "No RECORD FOUND":
                    obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.NoRecordsFound;
                    lblTransStatus.Text = "No RECORD FOUND!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;

                    break;
                case "BOOKED":
                    obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.BOOKED;
                    lblTransStatus.Text = "BOOKED!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;

                    break;
                case "PENDING":
                    obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.PENDING;
                    lblTransStatus.Text = "PENDING!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;

                    // hPENDING.Visible = true;
                    break;
                case "EXPIRED":
                    obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.EXPIRED;
                    lblTransStatus.Text = "EXPIRED!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;

                    // hEXPIRED.Visible = true;
                    break;
                case "FAILED OR REFUND IF RECEIVED":
                    obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.FailedOrRefundIfReceived;
                    lblTransStatus.Text = "FAILED OR REFUND IF RECEIVED!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;

                    break;
                case "REFUND SETTLED":
                    obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.RefundSettled;
                    lblTransStatus.Text = "REFUND SETTLED!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;

                    break;

                case "PAYMENT INITIATED":
                    obj_OnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.PaymentInitiated;
                    lblTransStatus.Text = "PAYMENT INITIATED!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;

                    break;
                case "CONFIRMED":
                    obj_OnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.Confirmed;
                    lblTransStatus.Text = "CONFIRMED!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;

                    break;
                case "INCOMPLETE":
                    obj_OnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.Incomplete;
                    lblTransStatus.Text = "INCOMPLETE!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;

                    break;
                case "SUBMITTED":
                    obj_OnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.Submitted;
                    lblTransStatus.Text = "SUBMITTED!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;

                    break;
                case "OFFDSCRD":
                    obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.OFFDSCRD;
                    lblTransStatus.Text = "Offline deposit slip created!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;

                    break;
                default:
                    obj_OnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.NotDefined;
                    obj_OnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.NotDefined;
                    lblTransStatus.Text = elemList[i].Attributes["status"].Value.ToUpper();
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;
                    break;
                    #endregion
            }
            #endregion
            obj_OnlinePayment.StatusXX = elemList[i].Attributes["status"].Value.ToUpper();
            strStatus = elemList[i].Attributes["status"].Value.ToUpper();

        }
        XmlNodeList elemList2 = xmlDoc.GetElementsByTagName("reference");
        for (int i = 0; i < elemList2.Count; i++)
        {
            lblReferNo.Text = elemList2[i].Attributes["id"].Value;
            lblTime.Text = elemList2[i].Attributes["BankTransacstionDate"].Value;
            lblAmount.Text = elemList2[i].Attributes["TotalAmount"].Value;

        }
        strActionName = "Set Online Payment Status By Response";
        obj_OnlinePayment.TransactionRefNo = Convert.ToString(elemList2[0].Attributes["id"].Value);
        obj_OnlinePayment.OnlinePaymentXMLRecieved = xmlDoc.OuterXml;
        obj_OnlinePayment.SetOnlinePaymentStatusByResponse();

        nodes = xmlDoc.ChildNodes;
        if (obj_SADOnlinePayment != null)
        {
            lblApplicationCodeFrom.Text = obj_SADOnlinePayment.ApplicationCode.ToString();
            lblModeFrom.Text = "Edit";
            NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_mainindustrialNewApplication, out obj_maininfrastructureNewApplication, out obj_mainminingNewApplication, out obj_mainindustrialRenewApplication, out obj_maininfrastructureRenewApplication, out obj_mainminingRenewApplication, obj_OnlinePayment.ApplicationCode);
            if (obj_mainindustrialNewApplication != null)
            {
                lblAppName.Text = obj_mainindustrialNewApplication.NameOfIndustry;
            }
            else if (obj_mainindustrialRenewApplication != null)
            {
                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplicationl = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_mainindustrialRenewApplication.FirstApplicationCode);
                lblAppName.Text = obj_IndustrialNewApplicationl.NameOfIndustry;
            }
            else if (obj_maininfrastructureNewApplication != null)
            {
                lblAppName.Text = obj_maininfrastructureNewApplication.NameOfInfrastructure;
            }
            else if (obj_maininfrastructureRenewApplication != null)
            {
                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplicationl = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_maininfrastructureRenewApplication.FirstApplicationCode);
                lblAppName.Text = obj_InfrastructureNewApplicationl.NameOfInfrastructure;
            }
            else if (obj_mainminingNewApplication != null)
            {
                lblAppName.Text = obj_mainminingNewApplication.NameOfMining;
            }
            else if (obj_mainminingRenewApplication != null)
            {
                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplicationl = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_mainminingRenewApplication.FirstApplicationCode);
                lblAppName.Text = obj_MiningNewApplicationl.NameOfMining;
            }
        }
    }
    private void ResponseForSADOnlinePayment(XmlNodeList elemList, XmlDocument xmlDoc)
    {
        XmlNodeList nodes = null;
        for (int i = 0; i < elemList.Count; i++)
        {
            obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(elemList[i].Attributes["orderCode"].Value);
            lblOnlineOrderCode.Text = elemList[i].Attributes["orderCode"].Value;
            #region switch
            switch (elemList[i].Attributes["status"].Value.ToUpper())
            {
                #region cases
                case "REINITIATION":
                    obj_SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.ReInitiation;
                    lblTransStatus.Text = "PENDING!/REINITIATION!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;
                    break;
                case "DELETED":
                    obj_SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.Deleted;
                    lblTransStatus.Text = "FAIL!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;
                    break;
                case "QUITED":
                    obj_SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.Quited;
                    lblTransStatus.Text = "FAIL!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;
                    break;
                case "CREATED":
                    obj_SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.Created;
                    lblTransStatus.Text = "FAIL!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;
                    break;
                case "HOLD":
                    obj_SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.HOLD;
                    lblTransStatus.Text = "PENDING!/HOLD!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;
                    break;
                case "SUBMITTED":
                    obj_SADOnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.Submitted;
                    lblTransStatus.Text = "SUBMITTED!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;
                    break;
                case "INCOMPLETE":
                    obj_SADOnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.Incomplete;
                    lblTransStatus.Text = "INCOMPLETE!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;
                    break;
                case "CONFIRMED":
                    obj_SADOnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.Confirmed;
                    lblTransStatus.Text = "CONFIRMED!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;
                    break;

                case "PAYMENT INITIATED":
                    obj_SADOnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.PaymentInitiated;
                    lblTransStatus.Text = "PAYMENT INITIATED!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;
                    break;
                case "SUCCESS":
                    obj_SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.SUCCESS;
                    lblTransStatus.Text = "Success!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Green;
                    btnNext.Enabled = true;

                    // hPaymentSuccess.Visible = true;
                    break;
                case "FAIL":
                    obj_SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.FAIL;
                    lblTransStatus.Text = "FAIL!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;
                    break;
                case "STATUS UNKNOWN/ABORT":
                    obj_SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.StatusUnknownAbort;
                    lblTransStatus.Text = "STATUS UNKNOWN/ABORT!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;
                    break;
                case "No RECORD FOUND":
                    obj_SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.NoRecordsFound;
                    lblTransStatus.Text = "No RECORD FOUND!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;

                    break;
                case "BOOKED":
                    obj_SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.BOOKED;
                    lblTransStatus.Text = "BOOKED!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;

                    break;
                case "PENDING":
                    obj_SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.PENDING;
                    lblTransStatus.Text = "PENDING!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;

                    // hPENDING.Visible = true;
                    break;
                case "EXPIRED":
                    obj_SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.EXPIRED;
                    lblTransStatus.Text = "EXPIRED!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;

                    // hEXPIRED.Visible = true;
                    break;
                case "FAILED OR REFUND IF RECEIVED":
                    obj_SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.FailedOrRefundIfReceived;
                    lblTransStatus.Text = "FAILED OR REFUND IF RECEIVED!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;

                    break;
                case "REFUND SETTLED":
                    obj_SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.RefundSettled;
                    lblTransStatus.Text = "REFUND SETTLED!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;

                    break;

                case "TERMINATE":
                    obj_SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.TERMINATE;
                    lblTransStatus.Text = "FAIL!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;
                    break;

                case "OFFDSCRD":
                    obj_SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.OFFDSCRD;
                    lblTransStatus.Text = "Offline deposit slip created!";
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;

                    break;
                default:
                    obj_SADOnlinePayment.PaymentStatus = NOCAP.BLL.Common.CommonEnum.PaymentStatus.NotDefined;
                    obj_SADOnlinePayment.ReceiptStatus = NOCAP.BLL.Common.CommonEnum.ReceiptStatus.NotDefined;
                    lblTransStatus.Text = elemList[i].Attributes["status"].Value.ToUpper();
                    lblTransStatus.ForeColor = System.Drawing.Color.Red;
                    btnNext.Enabled = false;
                    break;
                    #endregion
            }
            #endregion
            obj_SADOnlinePayment.StatusXX = elemList[i].Attributes["status"].Value.ToUpper();
            strStatus = elemList[i].Attributes["status"].Value.ToUpper();

        }
        XmlNodeList elemList2 = xmlDoc.GetElementsByTagName("reference");
        for (int i = 0; i < elemList2.Count; i++)
        {
            lblReferNo.Text = elemList2[i].Attributes["id"].Value;
            lblTime.Text = elemList2[i].Attributes["BankTransacstionDate"].Value;
            lblAmount.Text = elemList2[i].Attributes["TotalAmount"].Value;

        }
        strActionName = "Set Online Payment Status By Response";
        obj_SADOnlinePayment.TransactionRefNo = Convert.ToString(elemList2[0].Attributes["id"].Value);
        obj_SADOnlinePayment.OnlinePaymentXMLRecieved = xmlDoc.OuterXml;
        obj_SADOnlinePayment.SetOnlinePaymentStatusByResponse();

        nodes = xmlDoc.ChildNodes;
        if (obj_SADOnlinePayment != null)
        {
            lblApplicationCodeFrom.Text = obj_SADOnlinePayment.ApplicationCode.ToString();
            lblModeFrom.Text = "Edit";
            NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_InfrastructureNewApplication, out obj_MiningNewApplication, out obj_IndustrialRenewApplication, out obj_InfrastructureRenewApplication, out obj_MiningRenewApplication, obj_SADOnlinePayment.ApplicationCode);
            if (obj_IndustrialNewApplication != null)
            {
                lblAppName.Text = obj_IndustrialNewApplication.NameOfIndustry;
            }
            else if (obj_IndustrialRenewApplication != null)
            {
                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplicationl = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_IndustrialRenewApplication.FirstApplicationCode);
                lblAppName.Text = obj_IndustrialNewApplicationl.NameOfIndustry;
            }
            else if (obj_InfrastructureNewApplication != null)
            {
                lblAppName.Text = obj_InfrastructureNewApplication.NameOfInfrastructure;
            }
            else if (obj_InfrastructureRenewApplication != null)
            {
                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplicationl = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_InfrastructureRenewApplication.FirstApplicationCode);
                lblAppName.Text = obj_InfrastructureNewApplicationl.NameOfInfrastructure;
            }
            else if (obj_MiningNewApplication != null)
            {
                lblAppName.Text = obj_MiningNewApplication.NameOfMining;
            }
            else if (obj_MiningRenewApplication != null)
            {
                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplicationl = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_MiningRenewApplication.FirstApplicationCode);
                lblAppName.Text = obj_MiningNewApplicationl.NameOfMining;
            }
        }
    }
    //private XmlNodeList GetRSAKey(XmlDocument xmlDoc)
    //{
    //    try
    //    {
    //        string CertificateKey = ConfigurationManager.AppSettings["NTRPPublicKey"].ToString();
    //        X509Store certificate = new X509Store(StoreLocation.CurrentUser);


    //        certificate = new X509Store(StoreLocation.LocalMachine);
    //        certificate.Open(OpenFlags.ReadOnly);
    //        RSACryptoServiceProvider rsaKey = null;
    //        string checkCertificate = "";
    //        X509Certificate2 certx = new X509Certificate2();
    //        foreach (X509Certificate2 cert in certificate.Certificates)
    //        {
    //            if (cert.Subject.Contains(CertificateKey))
    //            {

    //                // retrieve private key                  
    //                // rsaKey = (RSACryptoServiceProvider)cert.PrivateKey;                       
    //                rsaKey = (RSACryptoServiceProvider)cert.PublicKey.Key;
    //                certx = cert;
    //                checkCertificate = GetDateTimeValue(certx.NotAfter.ToString("yyyy-MM-dd")) >= GetDateTimeValue(DateTime.Now.ToString("yyyy-MM-dd")) ? "Valid " : "Expired";
    //                break;
    //            }
    //        }
    //        if (checkCertificate == "Expired")
    //            throw new Exception("Valid certificate was not found or Expired");
    //        if (rsaKey == null && checkCertificate == "Expired")
    //            throw new Exception("Valid certificate was not found or Expired");
    //        string strResult = string.Empty;
    //        bool flag_SuccessFail = VerifyXml(xmlDoc, rsaKey, ref strResult);
    //        XmlNodeList elemList = xmlDoc.GetElementsByTagName("orderStatus");

    //        return elemList;
    //    }
    //    catch (Exception ex)
    //    {
    //        return null;
    //    }
    //}
    //private void verify()
    //{
    //    X509Store certificate = new X509Store(StoreLocation.CurrentUser);
    //    RSACryptoServiceProvider rsaKey = null;
    //    foreach (X509Certificate2 cert in certificate.Certificates)
    //    {
    //        rsaKey = (RSACryptoServiceProvider)cert.PublicKey.Key;
    //    }
    //    if (rsaKey != null)
    //    {
    //        //method to verify signed xml
    //        //string result = VerifyXml(xmlDoc, rsaKey);

    //    }
    //}
    private static DateTime GetDateTimeValue(string DateTimeValue)
    {
        string str_DateTimeValue = Convert.ToDateTime(DateTimeValue).ToString("yyyy-MM-dd h:mm tt");
        DateTime dt = DateTime.ParseExact(str_DateTimeValue, "yyyy-MM-dd h:mm tt", CultureInfo.InvariantCulture);
        return dt;
    }
    private bool VerifyXml(XmlDocument xmlDoc, RSA rsKey, ref string strResult)
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
    public void Decrypt(XmlDocument Doc)
    {
        CspParameters cspParams = new CspParameters();
        cspParams.KeyContainerName = "pkptrgapi.cga.gov.in";

        // Get the RSA key from the key container.  This key will decrypt
        // a symmetric key that was imbedded in the XML document.
        RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider(cspParams);

        try
        {
            // Check the arguments.
            if (Doc == null)
                throw new ArgumentNullException("Doc");
            if (rsaKey == null)
                throw new ArgumentNullException("Alg");
            if (cspParams.KeyContainerName == null)
                throw new ArgumentNullException("KeyName");
            // Create a new EncryptedXml object.
            EncryptedXml exml = new EncryptedXml(Doc);

            // Add a key-name mapping.
            // This method can only decrypt documents
            // that present the specified key name.
            exml.AddKeyNameMapping(cspParams.KeyContainerName, rsaKey);

            // Decrypt the element.
            exml.DecryptDocument();

            Doc.Save("test.xml");
        }
        catch (Exception e)
        {

        }
        finally
        {
            // Clear the RSA key.
            rsaKey.Clear();
        }
    }
    public string DecryptString(string encrString)
    {
        byte[] b;
        string decrypted;
        try
        {
            b = Convert.FromBase64String(encrString);
            decrypted = ASCIIEncoding.ASCII.GetString(b);
        }
        catch (FormatException fe)
        {
            decrypted = "";
        }
        return decrypted;
    }


    protected void btnNext_Click(object sender, EventArgs e)
    {

        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication();
        NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication obj_IndustrialRenewApplication = new NOCAP.BLL.Industrial.Renew.SADApplication.IndustrialRenewSADApplication();
        NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication();
        NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.SADApplication.InfrastructureRenewSADApplication();
        NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication();
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication();

        NOCAP.BLL.Utility.Utility.GetSADAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_InfrastructureNewApplication, out obj_MiningNewApplication, out obj_IndustrialRenewApplication, out obj_InfrastructureRenewApplication, out obj_MiningRenewApplication, Convert.ToInt64(lblApplicationCodeFrom.Text.Trim()));
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            Server.Transfer("~/ExternalUser/ApplicantHome.aspx");

            //if (obj_IndustrialNewApplication != null)
            //{

            //    Server.Transfer("ExternalUser/IndustrialNew/Submit.aspx");
            //}
            //else if (obj_IndustrialRenewApplication != null)
            //{
            //    Server.Transfer("ExternalUser/IndustrialRenew/Submit.aspx");
            //}
            //else if (obj_InfrastructureNewApplication != null)
            //{
            //    Server.Transfer("ExternalUser/InfrastructureNew/Submit.aspx");
            //}
            //else if (obj_InfrastructureRenewApplication != null)
            //{
            //    Server.Transfer("ExternalUser/InfrastructureRenew/Submit.aspx");
            //}
            //else if (obj_MiningNewApplication != null)
            //{
            //    Server.Transfer("ExternalUser/MiningNew/Submit.aspx");
            //}
            //else if (obj_MiningRenewApplication != null)
            //{
            //    Server.Transfer("ExternalUser/MiningRenew/Submit.aspx");
            //}
        }
    }
}