﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Configuration;
using System.Text;


public partial class ExternalUser_MiningRenew_MINRenewOnlinePayment : System.Web.UI.Page
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
                        Label SourceLabel = (Label)placeHolder.FindControl("lblMiningApplicationCodeFrom");
                        if (SourceLabel != null)
                        {
                            lblMiningApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                        }
                    }

                }
                if (NOCAPExternalUtility.FillRadioButtonListPaymentMode(ref rdBtnPayMode) != 1)
                {
                    lblMessage.Text = "Problem in Payment Mode";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                    rdBtnPayMode.SelectedIndex = 1;
                if (lblModeFrom.Text.Trim() == "Edit")
                    BindPaymentDetail(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));


            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }
    }

    #region Private
    private void BindPaymentDetail(long lngA_ApplicationCode)
    {
        DateTime dtTodayDateOnly = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewSADApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(lngA_ApplicationCode);
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplicationPrevious = new NOCAP.BLL.Mining.New.Application.MiningNewApplication();
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplicationPrevious = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication();
        NOCAP.BLL.Master.FeeRequiredPending obj_FeeRequiredPending = new NOCAP.BLL.Master.FeeRequiredPending(obj_MiningRenewSADApplication.ApplicationTypeCode, obj_MiningRenewSADApplication.ApplicationPurposeCode);
        lblFeeAmout.Text = obj_FeeRequiredPending.Amount.ToString();

        obj_MiningRenewSADApplication.GetPreviousMiningApplication(out obj_MiningNewApplicationPrevious, out obj_MiningRenewApplicationPrevious);
        if (obj_MiningNewApplicationPrevious != null && obj_MiningNewApplicationPrevious.ApplicationCode > 0)
        {
            lblValidityEndDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_MiningNewApplicationPrevious.GetIssuedLetter().ValidityEndDate).ToShortDateString());
        }
        if (obj_MiningRenewApplicationPrevious != null && obj_MiningRenewApplicationPrevious.MiningRenewApplicationCode > 0)
        {
            lblValidityEndDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_MiningRenewApplicationPrevious.GetIssuedLetter().ValidityEndDate).ToShortDateString());
        }


        if (Convert.ToDateTime(lblValidityEndDate.Text) < dtTodayDateOnly)
        {
            rowPenalty.Visible = true;
            NOCAP.BLL.Master.Penalty obj_Penalty = new NOCAP.BLL.Master.Penalty(17);
            lblPenaltyAmount.Text = obj_Penalty.Rate.ToString();
            lblPenaltyType.Text = " (" + obj_Penalty.PenaltyDesc + ")";

        }
        else
            rowPenalty.Visible = false;
        if (obj_MiningRenewSADApplication.ProFeeOrderPaymentCode != null)
        {
            NOCAP.BLL.Misc.Payment.SADOnlinePayment obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(lngA_ApplicationCode, obj_MiningRenewSADApplication.ProFeeOrderPaymentCode.ToString());
            switch (obj_SADOnlinePayment.PaymentStatus)
            {
                case NOCAP.BLL.Common.CommonEnum.PaymentStatus.SUCCESS:
                    rdBtnPayMode.Enabled = false;
                    rdBtnNEFTRTGS.Enabled = false;
                    PayBtn.Enabled = false;
                    txtGWCharge.Enabled = false;
                    txtGWChargeArear.Enabled = false;
                    rfvOnlinePayment.Enabled = false;
                    break;
                default:
                    rdBtnPayMode.Enabled = true;
                    rdBtnNEFTRTGS.Enabled = true;
                    PayBtn.Enabled = true;
                    txtGWCharge.Enabled = true;
                    txtGWChargeArear.Enabled = true;
                    rfvOnlinePayment.Enabled = true;
                    break;
            }
        }
        else
        {
            rdBtnPayMode.Enabled = true;
            rdBtnNEFTRTGS.Enabled = true;
            PayBtn.Enabled = true;
            txtGWCharge.Enabled = true;
            txtGWChargeArear.Enabled = true;
            rfvOnlinePayment.Enabled = true;
        }
    }
    private void ValidationExpInit()
    { }

    #endregion
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

    protected void PayBtn_Click(object sender, EventArgs e)
    {




        string XMLstr = "";long OrderPaymentCode = 0;
        decimal? ArearAmount = null;
        NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication obj_MiningRenewSADApplication = new NOCAP.BLL.Mining.Renew.SADApplication.MiningRenewSADApplication(Convert.ToInt64(lblMiningApplicationCodeFrom.Text));
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_MiningRenewSADApplication.FirstApplicationCode);


        NOCAP.BLL.Master.SubDistrict obj_subDistrict = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_subDistrictAreaTypeCategoryHistory = null;
        obj_subDistrictAreaTypeCategoryHistory = obj_subDistrict.GetSubDistrictAreaTypeCategoryHistory();

        Dictionary<int, decimal> dict = new Dictionary<int, decimal>();
        dict.Add(1, Convert.ToDecimal(lblFeeAmout.Text));
        if (obj_subDistrictAreaTypeCategoryHistory.AreaTypeCategoryCode == 5)
            dict.Add(2, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)) + (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)));
        else
            dict.Add(3, (txtGWCharge.Text == "" ? 0 : Convert.ToDecimal(txtGWCharge.Text)) + (txtGWChargeArear.Text == "" ? 0 : Convert.ToDecimal(txtGWChargeArear.Text)));
        if (lblPenaltyType.Text != "")
            dict.Add(4, Convert.ToDecimal(lblPenaltyAmount.Text));

        //switch (rdBtnNEFTRTGS.SelectedValue.ToString())
        //{
        //    case "0":
        //        XMLstr = NOCAPExternalUtility.CreateXML(Convert.ToInt64(Session["ExternalUserCode"]), dict, (txtGWChargeArear.Text == "" ? ArearAmount : Convert.ToDecimal(txtGWChargeArear.Text)), NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OffLine, ref OrderPaymentCode, obj_MiningRenewSADApplication: obj_MiningRenewSADApplication);
        //        break;
        //    case "1":

        //        XMLstr = NOCAPExternalUtility.CreateXML(Convert.ToInt64(Session["ExternalUserCode"]), dict, (txtGWChargeArear.Text == "" ? ArearAmount : Convert.ToDecimal(txtGWChargeArear.Text)), NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.OnLine, ref OrderPaymentCode, obj_MiningRenewSADApplication: obj_MiningRenewSADApplication);
        //        break;
        //    case "2":
        //        XMLstr = NOCAPExternalUtility.CreateXML(Convert.ToInt64(Session["ExternalUserCode"]), dict, (txtGWChargeArear.Text == "" ? ArearAmount : Convert.ToDecimal(txtGWChargeArear.Text)), NOCAP.BLL.Common.CommonEnum.PaymentMethodMode.NotDefined, ref OrderPaymentCode, obj_MiningRenewSADApplication: obj_MiningRenewSADApplication);
        //        break;
        //}



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
        NOCAP.BLL.Misc.Payment.SADOnlinePayment obj_SADOnlinePayment = new NOCAP.BLL.Misc.Payment.SADOnlinePayment(OrderPaymentCode);
        obj_SADOnlinePayment.OnlinePaymentXMLSent = xmlDoc.OuterXml;
        obj_SADOnlinePayment.SetOnlinePaymentXML();
        string URI = ConfigurationManager.AppSettings["NTRPURI"];
        string xmlDocBase64 = Convert.ToBase64String(Encoding.Default.GetBytes(xmlDoc.OuterXml));
        StringBuilder sb = new StringBuilder();
        sb.Append("<html>");
        sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
        sb.AppendFormat("<form name='form' action='{0}' method='post'>", URI);
        sb.AppendFormat("<input type='hidden' name='bharrkkosh' value='{0}'>", HttpUtility.HtmlEncode(xmlDocBase64));
        // Other params go here
        sb.Append("</form>");
        sb.Append("</body>");
        sb.Append("</html>");

        Response.Write(sb.ToString());

        Response.End();
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
                Server.Transfer("Submit.aspx");
            }
        }

    }

    protected void rdBtnPayMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(rdBtnPayMode.SelectedValue.ToString()) == 1)
        {
            rfvOnlinePayment.Enabled = false;
            tblOnlinePayment.Visible = false;

        }
        else
        {
            rfvOnlinePayment.Enabled = true;
            tblOnlinePayment.Visible = true;
        }

    }
}