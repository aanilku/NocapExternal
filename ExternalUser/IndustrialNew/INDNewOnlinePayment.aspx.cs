using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Xml;
public partial class ExternalUser_IndustrialNew_INDNewOnlinePayment : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ValidationExpInit();
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;

            lblMessage.Text = "";
            try
            {
                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblModeFrom");
                        if (SourceLabel != null)
                        {
                            lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                        }
                    }


                }
                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblIndustialApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                        }
                    }

                }
                //if (NOCAPExternalUtility.FillRadioButtonListPaymentMode(ref rdBtnPayMode) != 1)
                //{
                //    lblMessage.Text = "Problem in Payment Mode";
                //    lblMessage.ForeColor = System.Drawing.Color.Red;
                //}
                //else
                //    rdBtnPayMode.SelectedIndex = 1;
                tblOfflinePayment.Visible = false;
                if (lblModeFrom.Text.Trim() == "Edit")
                    BindPaymentDetail(sender, e, Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));


            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            Server.Transfer("Attachment.aspx");
        }
    }

    #region Private
    private void BindPaymentDetail(object sender, EventArgs e, long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(lngA_ApplicationCode);
            NOCAP.BLL.Master.FeeRequiredPending obj_FeeRequiredPending = new NOCAP.BLL.Master.FeeRequiredPending(obj_IndustrialNewSADApplication.ApplicationTypeCode, obj_IndustrialNewSADApplication.ApplicationPurposeCode);
            lblFeeAmout.Text = obj_FeeRequiredPending.Amount.ToString();
            lblOfflineFeeAmout.Text = obj_FeeRequiredPending.Amount.ToString();
            switch (obj_IndustrialNewSADApplication.PaymentTypeMode)
            {
                case NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined:
                    if (obj_IndustrialNewSADApplication.ProFeeOrderPaymentCode != null)
                    {
                        NOCAP.BLL.Misc.Payment.SADOnlinePayment obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(lngA_ApplicationCode, obj_IndustrialNewSADApplication.ProFeeOrderPaymentCode.ToString());
                        switch (obj_SADOnlinePayment.PaymentStatus)
                        {
                            case NOCAP.BLL.Common.CommonEnum.PaymentStatus.SUCCESS:
                                rdBtnPayMode.Enabled = false;
                                PayBtn.Enabled = false;

                                break;
                            default:
                                rdBtnPayMode.Enabled = true;
                                PayBtn.Enabled = true;

                                break;
                        }
                    }
                    else
                    {
                        rdBtnPayMode.Enabled = true;
                        PayBtn.Enabled = true;

                    }

                    rdBtnPayMode.SelectedValue = "1";
                    break;
                case NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.NotDefined:
                    rdBtnPayMode.SelectedValue = "1";
                    break;
                case NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single:
                    if (obj_IndustrialNewSADApplication.ProFeeOrderPaymentCode != null)
                    {
                        NOCAP.BLL.Misc.Payment.SADOnlinePayment obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(lngA_ApplicationCode, obj_IndustrialNewSADApplication.ProFeeOrderPaymentCode.ToString());
                        switch (obj_SADOnlinePayment.PaymentStatus)
                        {
                            case NOCAP.BLL.Common.CommonEnum.PaymentStatus.SUCCESS:
                                rdBtnPayMode.Enabled = false;
                                btnAppFee.Enabled = false;
                                break;
                            default:
                                rdBtnPayMode.Enabled = true;
                                btnAppFee.Enabled = true;
                                break;
                        }
                    }
                    else
                    {
                        rdBtnPayMode.Enabled = true;
                        btnAppFee.Enabled = true;

                    }
                    rdBtnPayMode.SelectedValue = "0";
                    break;
                default:
                    rdBtnPayMode.SelectedValue = "1";
                    break;

            }
            rdBtnPayMode_SelectedIndexChanged(sender, e);
            switch (obj_IndustrialNewSADApplication.GroundWaterUtilizationFor)
            {
                case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.GroundWaterUtilizationForOption.ExistingIndustry:
                    NOCAP.BLL.Master.Penalty obj_Penalty = new NOCAP.BLL.Master.Penalty(17);
                    switch (obj_IndustrialNewSADApplication.NOCObtainForExistIndustry.NOCObtainForExistIND)
                    {
                        case NOCAP.BLL.Common.NOCObtainForExistIndustry.NOCObtainForExistINDOption.No:
                            lblPenaltyAmount.Text = obj_Penalty.Rate.ToString();
                            lblOfflinePenaltyAmount.Text = obj_Penalty.Rate.ToString();
                            lblPenaltyType.Text = " (" + obj_Penalty.PenaltyDesc + ")";
                            lblOfflinePenaltyType.Text = " (" + obj_Penalty.PenaltyDesc + ")";
                            rowPenalty.Visible = true;
                            rowOfflinePenalty.Visible = true;
                            break;
                    }
                    break;
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    private void ValidationExpInit()
    {
        //revtxtSalientFeaturesOfInActivity.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        //revtxtSalientFeaturesOfInActivity.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;


        //revLengthtxtSalientFeaturesOfInActivity.ValidationExpression = ValidationUtility.txtValForMaximumCharacterAllowed("500");
        //revLengthtxtSalientFeaturesOfInActivity.ErrorMessage = ValidationUtility.txtValForMaximumCharacterAllowedMsg("500");


    }
    private int AreaTypeCategoryCode(NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication)
    {
        NOCAP.BLL.Master.SubDistrict obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewSADApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_subDistrictAreaTypeCategoryHistory = null;
        obj_subDistrictAreaTypeCategoryHistory = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory();
        return obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode;
    }
    private void CreateXmlParam(Dictionary<int, decimal> dict, NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode enu, decimal? ArearAmount = null)
    {
        try
        {
            string XMLstr = ""; string OrderPaymentCode = "";
            XMLstr = "";// NOCAPExternalUtility.CreateXML(Convert.ToInt64(Session["ExternalUserCode"]), dict, ArearAmount, enu, ref OrderPaymentCode, obj_IndustrialNewSADApplication: obj_IndustrialNewSADApplication);
            SendXml(XMLstr, OrderPaymentCode);


        }
        catch (Exception ex)
        { Response.Redirect("~/ExternalErrorPage.aspx", false); }
    }
    private void SendXml(string XMLstr, string OrderPaymentCode)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(XMLstr);
        xmlDoc.Save(Server.MapPath("LOBAFile.xml"));
        xmlDoc = NOCAPExternalUtility.signedFun(xmlDoc);
        foreach (XmlNode node in xmlDoc)
        {
            if (node.NodeType == XmlNodeType.XmlDeclaration)
            {
                xmlDoc.RemoveChild(node);
            }
        }
        NOCAP.BLL.Misc.Payment.SADOnlinePayment obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text), OrderPaymentCode);
        obj_SADOnlinePayment.OnlinePaymentXMLSent = xmlDoc.OuterXml;
        obj_SADOnlinePayment.SetOnlinePaymentXML();

        string URI = ConfigurationManager.AppSettings["NTRPURI"];
        xmlDoc.Save(Server.MapPath("LOBAFilewithsign.xml"));


        string xmlDocBase64 = Convert.ToBase64String(Encoding.Default.GetBytes(xmlDoc.OuterXml));

        StringBuilder sb = new StringBuilder();
        sb.Append("<html>");
        sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
        sb.AppendFormat("<form name='form' action='{0}' method='post'>", URI);
        sb.AppendFormat("<input type='hidden' name='bharrkkosh' value='{0}'>", HttpUtility.HtmlEncode(xmlDocBase64));
        sb.Append("</form>");
        sb.Append("</body>");
        sb.Append("</html>");
        Response.Write(sb.ToString());
        Response.End();
    }
    #endregion


    protected void PayBtn_Click(object sender, EventArgs e)
    {
        try
        {
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
            Dictionary<int, decimal> dict = new Dictionary<int, decimal>();
            dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.ProcFee, Convert.ToDecimal(lblFeeAmout.Text));
            if (AreaTypeCategoryCode(obj_IndustrialNewSADApplication) == 5)
                dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWRCharge, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)) + (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)));
            else
                dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWACharge, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)) + (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)));
            if (lblPenaltyType.Text != "")
                dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.Penalty, Convert.ToDecimal(lblPenaltyAmount.Text));

            CreateXmlParam(dict, obj_IndustrialNewSADApplication,NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)));

            
        }
        catch (Exception ex)
        { Response.Redirect("~/ExternalErrorPage.aspx", false); }

    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
                Server.Transfer("Submit.aspx");
            }
        }

    }

    protected void rdBtnPayMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str_msg = "";
        NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
        if (Convert.ToInt32(rdBtnPayMode.SelectedValue.ToString()) == 1)
        {
            rfvOnlinePayment.Enabled = true;
            rfvOfflinePayment.Enabled = false;
            tblOnlinePayment.Visible = true;
            tblOfflinePayment.Visible = false;
            obj_IndustrialNewSADApplication.SetPaymentTypeMode(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Combined, out str_msg);
            btnNext.ValidationGroup = "rfvOnlinePayment";


        }
        else
        {
            obj_IndustrialNewSADApplication.SetPaymentTypeMode(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text), NOCAP.BLL.Common.CommonEnum.PaymentTypeMode.Single, out str_msg);
            rfvOnlinePayment.Enabled = false;
            rfvOfflinePayment.Enabled = true;
            tblOnlinePayment.Visible = false;
            tblOfflinePayment.Visible = true;
            btnNext.ValidationGroup = "rfvOfflinePayment";

        }

    }


    protected void btnAppFee_Click(object sender, EventArgs e)
    {
        try
        {
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
            Dictionary<int, decimal> dict = new Dictionary<int, decimal>();
            dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.ProcFee, Convert.ToDecimal(lblOfflineFeeAmout.Text));
            if (rdbtnCharge.SelectedValue == "1")
                CreateXmlParam(dict, obj_IndustrialNewSADApplication, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)));
            else
                CreateXmlParam(dict, obj_IndustrialNewSADApplication, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)));
        }
        catch (Exception ex)
        { Response.Redirect("~/ExternalErrorPage.aspx", false); }
    }

    protected void btnCharge_Click(object sender, EventArgs e)
    {
        try
        {
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
            Dictionary<int, decimal> dict = new Dictionary<int, decimal>();
            if (AreaTypeCategoryCode(obj_IndustrialNewSADApplication) == 5)
                dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWRCharge, (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)) + (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)));
            else
                dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.GWACharge, (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)) + (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)));
            if (rdbtnCharge.SelectedValue == "1")
                CreateXmlParam(dict, obj_IndustrialNewSADApplication, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine,(txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)));
            else
                CreateXmlParam(dict, obj_IndustrialNewSADApplication, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)));
        }
        catch (Exception ex)
        { Response.Redirect("~/ExternalErrorPage.aspx", false); }
    }

    protected void btnPenalty_Click(object sender, EventArgs e)
    {
        try
        {
            NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication obj_IndustrialNewSADApplication = new NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
            Dictionary<int, decimal> dict = new Dictionary<int, decimal>();
            if (lblPenaltyType.Text != "")
                dict.Add((Int32)NOCAP.BLL.Common.CommonEnum.PaymentTypeDesc.Penalty, Convert.ToDecimal(lblOfflinePenaltyAmount.Text));
            if (rdbtnCharge.SelectedValue == "1")
                CreateXmlParam(dict, obj_IndustrialNewSADApplication, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine,(txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)));
            else
                CreateXmlParam(dict, obj_IndustrialNewSADApplication, NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, (txtOffLineGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtOffLineGWChargeArear.Text)));
        }
        catch (Exception ex)
        { Response.Redirect("~/ExternalErrorPage.aspx", false); }
    }
}