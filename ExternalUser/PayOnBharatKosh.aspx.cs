using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

using System.Net.Http;
using System.Net;

public partial class PayOnBharatKosh : System.Web.UI.Page
{
    string XMLstr = "";
    NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
    NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
    NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication();



    NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication();
    NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication();
    NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication();

    protected void Page_Load(object sender, EventArgs e)
    {


        try
        {
            // lblMessage.Text = "";
            if (!IsPostBack)
            {
                lblApplicationCode.Text = Session["OnlinePayment"].ToString();
                lblApplicationCodeFrom.Text = lblApplicationCode.Text.Trim();
                if (NOCAPExternalUtility.FillCheckBoxListPaymentType(ref chblistPaymentType) != 1)
                {
                    lblMessage.Text = "Problem in Payment Type";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }

                GetINDINFMINDetails();
                Session.Remove("OnlinePayment");
                //  lblTotalAmt.Text = (Convert.ToDecimal(lblGWCharge.Text) + Convert.ToDecimal(lblGWAbsCharge.Text)).ToString();
            }
        }
        catch (Exception ex)
        {
        }
    }
    #region Button Click
    protected void PayBtn_Click(object sender, EventArgs e)
    {

        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(Convert.ToInt64(lblApplicationCode.Text.Trim()));
        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(Convert.ToInt64(lblApplicationCode.Text.Trim()));
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(Convert.ToInt64(lblApplicationCode.Text.Trim()));
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(Convert.ToInt64(lblApplicationCode.Text.Trim()));
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(Convert.ToInt64(lblApplicationCode.Text.Trim()));
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(Convert.ToInt64(lblApplicationCode.Text.Trim()));

        switch (rdBtnNEFTRTGS.SelectedValue.ToString())
        {
            case "0":
                XMLstr = CreateXML(obj_IndustrialNewApplication, obj_IndustrialRenewApplication, obj_InfrastructureNewApplication, obj_InfrastructureRenewApplication, obj_MiningNewApplication, obj_MiningRenewApplication, Convert.ToInt32(Session["ExternalUserCode"]), NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine);
                break;
            case "1":

                XMLstr = CreateXML(obj_IndustrialNewApplication, obj_IndustrialRenewApplication, obj_InfrastructureNewApplication, obj_InfrastructureRenewApplication, obj_MiningNewApplication, obj_MiningRenewApplication, Convert.ToInt32(Session["ExternalUserCode"]), NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine);
                break;
            case "2":
                XMLstr = CreateXML(obj_IndustrialNewApplication, obj_IndustrialRenewApplication, obj_InfrastructureNewApplication, obj_InfrastructureRenewApplication, obj_MiningNewApplication, obj_MiningRenewApplication, Convert.ToInt32(Session["ExternalUserCode"]), NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.NotDefined);
                break;
        }


        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(XMLstr);
        xmlDoc = NOCAPExternalUtility.signedFun(xmlDoc);
        foreach (XmlNode node in xmlDoc)
        {
            if (node.NodeType == XmlNodeType.XmlDeclaration)
            {
                xmlDoc.RemoveChild(node);
            }
        }
        xmlDoc.Save(Server.MapPath("imran.xml"));
        string xmlDocBase64 = Convert.ToBase64String(Encoding.Default.GetBytes(xmlDoc.OuterXml));//File.ReadAllBytes(Server.MapPath("imran.xml")));
        File.WriteAllText(Server.MapPath("imran.txt"), xmlDocBase64);
        Session["hdnBase64String"] = xmlDocBase64;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('PayOnBharatKosh2.aspx ','_blank')", true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region Private
    private void GetINDINFMINDetails()
    {

        NOCAP.BLL.UserManagement.User obj_internalUser = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(Session["InternalUserCode"]));




        NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_industrialNewApplication, out obj_infrastructureNewApplication, out obj_miningNewApplication, out obj_industrialRenewApplication, out obj_infrastructureRenewApplication, out obj_miningRenewApplication, Convert.ToInt64(lblApplicationCode.Text.Trim()));


        if (obj_industrialNewApplication != null)
        {

            ViewState["AppCode"] = Convert.ToInt64(lblApplicationCode.Text.Trim());
            GetIndustrialDetails(Convert.ToInt64(lblApplicationCode.Text.Trim()));



        }
        else if (obj_industrialRenewApplication != null)
        {
            // obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_industrialRenewApplication.FirstApplicationCode);

            ViewState["AppCode"] = Convert.ToInt64(lblApplicationCode.Text.Trim());
            GetIndustrialRenewDetails(Convert.ToInt64(lblApplicationCode.Text.Trim()));



        }
        else if (obj_infrastructureNewApplication != null)
        {

            ViewState["AppCode"] = Convert.ToInt64(lblApplicationCode.Text.Trim());
            GetInfrastructureDetails(Convert.ToInt64(lblApplicationCode.Text.Trim()));


        }

        else if (obj_infrastructureRenewApplication != null)
        {
            // obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_infrastructureRenewApplication.FirstApplicationCode);

            ViewState["AppCode"] = Convert.ToInt64(lblApplicationCode.Text.Trim());
            GetInfrastructureRenewDetails(Convert.ToInt64(lblApplicationCode.Text.Trim()));

        }
        else if (obj_miningNewApplication != null)
        {

            ViewState["AppCode"] = Convert.ToInt64(lblApplicationCode.Text.Trim());
            GetMiningDetails(Convert.ToInt64(lblApplicationCode.Text.Trim()));




        }
        else if (obj_miningRenewApplication != null)
        {
            // obj_miningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_miningRenewApplication.FirstApplicationCode);

            ViewState["AppCode"] = Convert.ToInt64(lblApplicationCode.Text.Trim());
            GetMiningRenewDetails(Convert.ToInt64(lblApplicationCode.Text.Trim()));


        }

        //create(obj_industrialNewApplication, obj_industrialRenewApplication, obj_infrastructureNewApplication, obj_infrastructureRenewApplication, 
        //    obj_miningNewApplication, obj_miningRenewApplication);



    }

    private void GetIndustrialDetails(long ApplicationCode)
    {
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(ApplicationCode);
        NOCAP.BLL.PenaltyImpose.PenaltyImpose obj_PenaltyImpose = new NOCAP.BLL.PenaltyImpose.PenaltyImpose(ApplicationCode, Convert.ToInt32(obj_industrialNewApplication.PenaltySN));
        try
        {
            if (obj_industrialNewApplication != null)
            {

                NOCAP.BLL.Master.ApplicationType obj_ApplicationType = new NOCAP.BLL.Master.ApplicationType(obj_industrialNewApplication.ApplicationTypeCode);

                if (obj_ApplicationType.ApplicationTypeDescription != "")
                    lblApplicationType.Text = HttpUtility.HtmlEncode(obj_ApplicationType.ApplicationTypeDescription);
                int intAppPurpose = obj_industrialNewApplication.ApplicationPurposeCode;
                lblAppPurpose.Text = (intAppPurpose == 1 ? "New" : "Renew");
                lnkApplicationNumber.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.IndustrialNewApplicationNumber);
                lblNOCNumber.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.NOCNumber);
                lblProjectName.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.NameOfIndustry);
                lblGWCharge.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.GWChargeAmtFinally);
                lblGWAbsCharge.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.GWChargeAmtFinally);

            }
        }
        catch (Exception ex)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        finally
        {
            obj_industrialNewApplication = null;
        }
    }

    private void GetIndustrialRenewDetails(long ApplicationCode)
    {
        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(ApplicationCode);
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_industrialRenewApplication.FirstApplicationCode);
        NOCAP.BLL.PenaltyImpose.PenaltyImpose obj_PenaltyImpose = new NOCAP.BLL.PenaltyImpose.PenaltyImpose(ApplicationCode, Convert.ToInt32(obj_industrialRenewApplication.PenaltySN));
        try
        {
            if (obj_industrialRenewApplication != null)
            {

                NOCAP.BLL.Master.ApplicationType obj_ApplicationType = new NOCAP.BLL.Master.ApplicationType(obj_industrialRenewApplication.ApplicationTypeCode);

                int intAppPurpose = obj_industrialRenewApplication.ApplicationPurposeCode;
                lblAppPurpose.Text = (intAppPurpose == 1 ? "New" : "Renew");

                if (obj_ApplicationType.ApplicationTypeDescription != "")
                    lblApplicationType.Text = HttpUtility.HtmlEncode(obj_ApplicationType.ApplicationTypeDescription);

                lnkApplicationNumber.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.IndustrialNewApplicationNumber);
                lblNOCNumber.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplication.NOCNumber);
                lblProjectName.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.NameOfIndustry);
                lblGWCharge.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplication.GWChargeAmtFinally);
                lblGWAbsCharge.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplication.GWChargeAmtFinally);

            }
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        finally
        {
            obj_industrialNewApplication = null;
        }
    }

    private void GetInfrastructureDetails(long ApplicationCode)
    {
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(ApplicationCode);
        NOCAP.BLL.PenaltyImpose.PenaltyImpose obj_PenaltyImpose = new NOCAP.BLL.PenaltyImpose.PenaltyImpose(ApplicationCode, Convert.ToInt32(obj_infrastructureNewApplication.PenaltySN));
        try
        {
            if (obj_infrastructureNewApplication != null)
            {

                NOCAP.BLL.Master.ApplicationType obj_ApplicationType = new NOCAP.BLL.Master.ApplicationType(obj_infrastructureNewApplication.ApplicationTypeCode);

                int intAppPurpose = obj_infrastructureNewApplication.ApplicationPurposeCode;
                lblAppPurpose.Text = (intAppPurpose == 1 ? "New" : "Renew");

                if (obj_ApplicationType.ApplicationTypeDescription != "")
                    lblApplicationType.Text = HttpUtility.HtmlEncode(obj_ApplicationType.ApplicationTypeDescription);

                lnkApplicationNumber.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.InfrastructureNewApplicationNumber);
                lblNOCNumber.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.NOCNumber);
                lblProjectName.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.NameOfInfrastructure);
                lblGWCharge.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.GWChargeAmtFinally);
                lblGWAbsCharge.Text = HttpUtility.HtmlEncode(obj_infrastructureNewApplication.GWChargeAmtFinally);



            }
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        finally
        {
            obj_infrastructureNewApplication = null;
        }

    }

    private void GetInfrastructureRenewDetails(long ApplicationCode)
    {
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(ApplicationCode);
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_InfrastructureRenewApplication.FirstApplicationCode);
        NOCAP.BLL.PenaltyImpose.PenaltyImpose obj_PenaltyImpose = new NOCAP.BLL.PenaltyImpose.PenaltyImpose(ApplicationCode, Convert.ToInt32(obj_InfrastructureRenewApplication.PenaltySN));
        try
        {
            if (obj_InfrastructureRenewApplication != null)
            {

                NOCAP.BLL.Master.ApplicationType obj_ApplicationType = new NOCAP.BLL.Master.ApplicationType(obj_InfrastructureRenewApplication.ApplicationTypeCode);

                int intAppPurpose = obj_InfrastructureRenewApplication.ApplicationPurposeCode;
                lblAppPurpose.Text = (intAppPurpose == 1 ? "New" : "Renew");

                if (obj_ApplicationType.ApplicationTypeDescription != "")
                    lblApplicationType.Text = HttpUtility.HtmlEncode(obj_ApplicationType.ApplicationTypeDescription);

                lnkApplicationNumber.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber);
                lblNOCNumber.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewApplication.NOCNumber);
                lblProjectName.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.NameOfInfrastructure);
                lblGWCharge.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewApplication.GWChargeAmtFinally);
                lblGWAbsCharge.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewApplication.GWChargeAmtFinally);


            }
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        finally
        {
            obj_InfrastructureNewApplication = null;
        }
    }

    private void GetMiningDetails(long ApplicationCode)
    {
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(ApplicationCode);

        try
        {
            if (obj_MiningNewApplication != null)
            {
                NOCAP.BLL.PenaltyImpose.PenaltyImpose obj_PenaltyImpose = new NOCAP.BLL.PenaltyImpose.PenaltyImpose(ApplicationCode, Convert.ToInt32(obj_MiningNewApplication.PenaltySN));

                NOCAP.BLL.Master.ApplicationType obj_ApplicationType = new NOCAP.BLL.Master.ApplicationType(obj_MiningNewApplication.ApplicationTypeCode);

                int intAppPurpose = obj_MiningNewApplication.ApplicationPurposeCode;
                lblAppPurpose.Text = (intAppPurpose == 1 ? "New" : "Renew");

                if (obj_ApplicationType.ApplicationTypeDescription != "")
                    lblApplicationType.Text = HttpUtility.HtmlEncode(obj_ApplicationType.ApplicationTypeDescription);
                lnkApplicationNumber.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.MiningNewApplicationNumber);
                lblNOCNumber.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.NOCNumber);
                lblProjectName.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.NameOfMining);
                lblGWCharge.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.GWChargeAmtFinally);
                lblGWAbsCharge.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.GWChargeAmtFinally);




            }
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        finally
        {
            obj_MiningNewApplication = null;
        }
    }

    private void GetMiningRenewDetails(long ApplicationCode)
    {
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(ApplicationCode);
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_MiningRenewApplication.FirstApplicationCode);
        NOCAP.BLL.PenaltyImpose.PenaltyImpose obj_PenaltyImpose = new NOCAP.BLL.PenaltyImpose.PenaltyImpose(ApplicationCode, Convert.ToInt32(obj_MiningRenewApplication.PenaltySN));

        try
        {
            if (obj_MiningRenewApplication != null)
            {

                NOCAP.BLL.Master.ApplicationType obj_ApplicationType = new NOCAP.BLL.Master.ApplicationType(obj_MiningRenewApplication.ApplicationTypeCode);

                int intAppPurpose = obj_MiningRenewApplication.ApplicationPurposeCode;
                lblAppPurpose.Text = (intAppPurpose == 1 ? "New" : "Renew");

                if (obj_ApplicationType.ApplicationTypeDescription != "")
                    lblApplicationType.Text = HttpUtility.HtmlEncode(obj_ApplicationType.ApplicationTypeDescription);
                lnkApplicationNumber.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.MiningNewApplicationNumber);
                lblNOCNumber.Text = HttpUtility.HtmlEncode(obj_MiningRenewApplication.NOCNumber);
                lblProjectName.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.NameOfMining);
                lblGWCharge.Text = HttpUtility.HtmlEncode(obj_MiningRenewApplication.GWChargeAmtFinally);
                lblGWAbsCharge.Text = HttpUtility.HtmlEncode(obj_MiningRenewApplication.GWChargeAmtFinally);





            }
        }
        catch (Exception ex)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        finally
        {
            obj_MiningNewApplication = null;
        }
    }



    private string CreateXML(NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication,
      NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication,
      NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication,
      NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication,
      NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication,
       NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication, int ExternalUserCode, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode enu)
    {
        string OrderContent = "", PaymentTypeCode = "";
        int Transaction = 0;
        long ApplicationCode = 0;
        string ShopperEmailAddress = "", Address1 = "", Address2 = "", PostalCode = "", City = "", State = "", BillMobileNumber = "";
        NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(ExternalUserCode);
        NOCAP.BLL.Misc.Payment.OnlinePayment obj_OnlinePayment = null;
        NOCAP.BLL.Master.NTRPMapping obj_NTRPMapping = new NOCAP.BLL.Master.NTRPMapping();
        obj_NTRPMapping.GetALL();
        NOCAP.BLL.Master.NTRPMapping[] arr_NTRPMapping = obj_NTRPMapping.NTRPMappingCollection;
        for (int i = 0; i < chblistPaymentType.Items.Count; i++)
        {
            if (chblistPaymentType.Items[i].Selected == true)
            {
                PaymentTypeCode += chblistPaymentType.Items[i].Value + ",";
                OrderContent += arr_NTRPMapping.Where(a => a.PaymentTypeCode == Convert.ToInt32(chblistPaymentType.Items[i].Value)).First().OrderContent.ToString() + ",";
                Transaction++;
            }
        }

        NOCAP.BLL.Misc.Payment.OnlinePaymentDetails[] arr_OnlinePaymentDetails = null;
        string strOnlonePaymentXMLDetail = null;
        try
        {

            if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.CreatedByExUC > 0)
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
            else if (obj_IndustrialRenewApplication != null && obj_IndustrialRenewApplication.CreatedByExUC > 0)
            {
                obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_IndustrialRenewApplication.FirstApplicationCode);
                ApplicationCode = obj_IndustrialRenewApplication.IndustrialRenewApplicationCode;
                ShopperEmailAddress = obj_IndustrialRenewApplication.CommunicationEmailID;
                Address1 = obj_IndustrialRenewApplication.CommunicationAddress.AddressLine1;
                Address2 = obj_IndustrialRenewApplication.CommunicationAddress.AddressLine2;
                PostalCode = ((int)obj_IndustrialRenewApplication.CommunicationAddress.PinCode).ToString();
                City = new NOCAP.BLL.Master.SubDistrict(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;
                State = new NOCAP.BLL.Master.State(obj_IndustrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
                BillMobileNumber = obj_IndustrialRenewApplication.CommunicationMobileNumber.MobileNumberRest.ToString();

            }
            else if (obj_InfrastructureNewApplication != null && obj_InfrastructureNewApplication.CreatedByExUC > 0)
            {
                ApplicationCode = obj_InfrastructureNewApplication.InfrastructureNewApplicationCode;
                ShopperEmailAddress = obj_InfrastructureNewApplication.CommunicationEmailID;
                Address1 = obj_InfrastructureNewApplication.CommunicationAddress.AddressLine1;
                Address2 = obj_InfrastructureNewApplication.CommunicationAddress.AddressLine2;
                PostalCode = ((int)obj_InfrastructureNewApplication.CommunicationAddress.PinCode).ToString();
                City = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;
                State = new NOCAP.BLL.Master.State(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
                BillMobileNumber = obj_InfrastructureNewApplication.CommunicationMobileNumber.MobileNumberRest.ToString();

            }
            else if (obj_InfrastructureRenewApplication != null && obj_InfrastructureRenewApplication.CreatedByExUC > 0)
            {
                obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_InfrastructureRenewApplication.FirstApplicationCode);
                ApplicationCode = obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode;
                ShopperEmailAddress = obj_InfrastructureRenewApplication.CommunicationEmailID;
                Address1 = obj_InfrastructureRenewApplication.CommunicationAddress.AddressLine1;
                Address2 = obj_InfrastructureRenewApplication.CommunicationAddress.AddressLine2;
                PostalCode = ((int)obj_InfrastructureRenewApplication.CommunicationAddress.PinCode).ToString();
                City = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;
                State = new NOCAP.BLL.Master.State(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
                BillMobileNumber = obj_InfrastructureRenewApplication.CommunicationMobileNumber.MobileNumberRest.ToString();
            }
            else if (obj_MiningNewApplication != null && obj_MiningNewApplication.CreatedByExUC > 0)
            {
                ApplicationCode = obj_MiningNewApplication.ApplicationCode;
                ShopperEmailAddress = obj_MiningNewApplication.CommunicationEmailID;
                Address1 = obj_MiningNewApplication.CommunicationAddress.AddressLine1;
                Address2 = obj_MiningNewApplication.CommunicationAddress.AddressLine2;
                PostalCode = ((int)obj_MiningNewApplication.CommunicationAddress.PinCode).ToString();
                City = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;
                State = new NOCAP.BLL.Master.State(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
                BillMobileNumber = obj_MiningNewApplication.CommunicationMobileNumber.MobileNumberRest.ToString();

            }
            else if (obj_MiningRenewApplication != null && obj_MiningRenewApplication.CreatedByExUC > 0)
            {
                obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_MiningRenewApplication.FirstApplicationCode);
                ApplicationCode = obj_MiningRenewApplication.MiningRenewApplicationCode;
                ShopperEmailAddress = obj_MiningRenewApplication.CommunicationEmailID;
                Address1 = obj_MiningRenewApplication.CommunicationAddress.AddressLine1;
                Address2 = obj_MiningRenewApplication.CommunicationAddress.AddressLine2;
                PostalCode = ((int)obj_MiningRenewApplication.CommunicationAddress.PinCode).ToString();
                City = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode).SubDistrictName;
                State = new NOCAP.BLL.Master.State(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode).StateName;
                BillMobileNumber = obj_MiningRenewApplication.CommunicationMobileNumber.MobileNumberRest.ToString();

            }

            obj_OnlinePayment = NOCAPExternalUtility.OnlinePayment(ApplicationCode, Convert.ToDecimal(lblTotalAmt.Text), Convert.ToInt32(Session["ExternalUserCode"]), enu, Transaction);

            if (obj_OnlinePayment != null)
            {
                //arr_OnlinePaymentDetails = NOCAPExternalUtility.OnlinePaymentDetails(obj_OnlinePayment, Convert.ToDecimal(lblGWCharge.Text),  obj_NTRPMapping.OrderContent, Convert.ToInt32(Session["ExternalUserCode"]), 1);
                arr_OnlinePaymentDetails = NOCAPExternalUtility.OnlinePaymentDetails(obj_OnlinePayment, Convert.ToDecimal(lblGWCharge.Text), OrderContent, PaymentTypeCode, Convert.ToInt32(Session["ExternalUserCode"]));
            }
            obj_NTRPMapping = new NOCAP.BLL.Master.NTRPMapping(14794,1);      
            StringBuilder builder = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(builder);            
            //Start XM DOcument
            writer.WriteStartDocument(true);
            // writer.Formatting = Formatting.Indented;
            //writer.Indentation = 2;

            #region ROOT
            //ROOT Element
            writer.WriteStartElement("BharatKoshPayment");
            writer.WriteAttributeString("DepartmentCode", obj_NTRPMapping.DepartmentCode);
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
                writer.WriteAttributeString("InstallationId", obj_NTRPMapping.InstallationId.ToString());
                writer.WriteAttributeString("OrderCode", obj_OnlinePayment.OrderPaymentCodeForNTRP);

                #region CartDetails
                //Start CartDetails
                writer.WriteStartElement("CartDetails");

                //Start Description
                writer.WriteStartElement("Description");
                // writer.WriteString(new NOCAP.BLL.Master.WaterChargeType(obj_OnlinePaymentDetails.water).WaterChargeTypeDesc);
                writer.WriteEndElement();     //Description Element


                //Start Amount
                writer.WriteStartElement("Amount");
                writer.WriteAttributeString("value", arr_OnlinePaymentDetails[i].AmountValue.ToString());
                writer.WriteAttributeString("exponent", "0");
                writer.WriteAttributeString("CurrencyCode", obj_NTRPMapping.CurrencyCode);

                writer.WriteEndElement();     //Amount Element

                //Start Amount
                writer.WriteStartElement("OrderContent");
                writer.WriteString(arr_OnlinePaymentDetails[i].OrderContent.ToString());
                writer.WriteEndElement();     //OrderContent Element

                //Start PaymentTypeId
                writer.WriteStartElement("PaymentTypeId");
                //writer.WriteString(obj_OnlinePaymentDetails.PaymentTypeCode.ToString());
                writer.WriteString("");
                writer.WriteEndElement();     //PaymentTypeId Element


                //Start PAOCode
                writer.WriteStartElement("PAOCode");
                writer.WriteString(obj_NTRPMapping.PAOCode);// obj_OnlinePaymentDetails.PAOCode);//imran
                writer.WriteEndElement();     //PAOCode Element

                //Start DDOCode
                writer.WriteStartElement("DDOCode");
                writer.WriteString(obj_NTRPMapping.DDOCode);
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


    private string VerifyXml(XmlDocument xmlDoc, RSA rsKey)
    {
        string strResult = string.Empty;
        if (xmlDoc == null)
            strResult = "Xml file is null";
        if (rsKey == null)
            strResult = "Key is null";


        XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Signature");

        if (nodeList.Count <= 0)
        {
            strResult = "Verification failed: No Signature was found in the document.";
        }
        SignedXml obj_SignedXml = new SignedXml();
        obj_SignedXml.LoadXml((XmlElement)nodeList[0]);
        if (obj_SignedXml.CheckSignature(rsKey))
        {
            strResult = "1";
        }
        else
        {
            strResult = "1";
        }
        return strResult;
    }
    #endregion




    protected void chblistPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblTotalAmt.Text = "";
        string PaymentTypeCode = "";
        for (int i = 0; i < chblistPaymentType.Items.Count; i++)
        {
            if (chblistPaymentType.Items[i].Selected == true)
            {
                PaymentTypeCode += chblistPaymentType.Items[i].Value + ",";
            }
        }
        if (PaymentTypeCode == "")
        {
            chbPaymentType.Checked = true;
            PayBtn.Enabled = false;
        }
        else
        {
            chbPaymentType.Checked = false;
            PayBtn.Enabled = true;
        }
        string[] arr_PaymentTypeCode = PaymentTypeCode.Split(',');
        if (arr_PaymentTypeCode.Contains("1"))//APPLICATION FEE  
        {
            //if (lblTotalAmt.Text != "")
            //    lblTotalAmt.Text = (Convert.ToDecimal(lblTotalAmt.Text) + Convert.ToDecimal(lblGWCharge.Text)).ToString();
            //else
            //    lblTotalAmt.Text = Convert.ToDecimal(lblGWCharge.Text).ToString();
        }
        if (arr_PaymentTypeCode.Contains("2"))//GROUND WATER RESTORATION CHARGES
        {
            if (lblTotalAmt.Text != "")
                lblTotalAmt.Text = (Convert.ToDecimal(lblTotalAmt.Text) + Convert.ToDecimal(lblGWCharge.Text)).ToString();
            else
                lblTotalAmt.Text = Convert.ToDecimal(lblGWCharge.Text).ToString();
        }
        if (arr_PaymentTypeCode.Contains("3"))//GROUND WATER ABSTRACTION CHARGES
        {
            if (lblTotalAmt.Text != "")
                lblTotalAmt.Text = (Convert.ToDecimal(lblTotalAmt.Text) + Convert.ToDecimal(lblGWAbsCharge.Text)).ToString();
            else
                lblTotalAmt.Text = Convert.ToDecimal(lblGWAbsCharge.Text).ToString();
        }

    }

    protected void chbPaymentType_CheckedChanged(object sender, EventArgs e)
    {
        if (chbPaymentType.Checked)
        {
            chblistPaymentType.ClearSelection();
            PayBtn.Enabled = false;
            lblTotalAmt.Text = "";
        }
    }
}