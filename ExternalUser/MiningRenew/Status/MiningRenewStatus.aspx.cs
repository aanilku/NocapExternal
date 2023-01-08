using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class ExternalUser_MiningRenew_Status_MiningRenewStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRFs.Value;
            BindData();
        }
    }
    void BindData()
    {
        try
        {
            Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
            if (placeHolder != null)
            {
                Label lblAppCode = (Label)placeHolder.FindControl("lblApplicationRenewCode");
                lblApplicationCode.Text = HttpUtility.HtmlEncode(((Label)placeHolder.FindControl("lblApplicationNewCode")).Text);
                if (!NOCAPExternalUtility.IsNumeric(lblAppCode.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert(' Application Code allows only Numeric ');", true);
                    return;
                }

                NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplicationForMainGrid = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(Convert.ToInt32(lblAppCode.Text));

                if (obj_MiningRenewApplicationForMainGrid != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ApplicationCode");
                    dt.Columns.Add("ReferBackSN");
                    dt.Columns.Add("ReferBackHeader");
                    for (int i = obj_MiningRenewApplicationForMainGrid.ReferBackSN; i >= 0; i--)
                    {
                        DataRow dr = dt.NewRow();
                        dr["ApplicationCode"] = obj_MiningRenewApplicationForMainGrid.MiningRenewApplicationCode;
                        dr["ReferBackSN"] = i;
                        int ReferBackCountToShow = i + 1;
                        string ReferBackToShow = AddOrdinal(ReferBackCountToShow);
                        dr["ReferBackHeader"] = obj_MiningRenewApplicationForMainGrid.ReferBackSN == i ? "Current Status" : "Application is Refered Back " + ReferBackToShow + " Time";
                        dt.Rows.Add(dr);
                    }
                    gvMainGrid.DataSource = dt;
                    gvMainGrid.DataBind();
                }

                #region Commented
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //if (!(string.IsNullOrEmpty(lblApplicationRenewCode.Text) && string.IsNullOrEmpty(lblSerialNo.Text)))
                //{
                //    if (Convert.ToString(obj_MiningAppVerificationStatus.WorkFlowInfo.WorkFlowComplete) == "Yes")
                //    {
                //        lblCurrentStage.Text = "Completed";

                //        if (Convert.ToString(obj_MiningAppVerificationStatus.WorkFlowInfo.VerificationStageComplete) == "NotDefined")
                //        {
                //            lblSerialNo.Text = HttpUtility.HtmlEncode(arr1[arr1.Length - 1].SerialNumber.ToString());
                //            NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewVeriStageWorkFlowHistory obj_MiningNewVeriStageWorkFlowHistory = new NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewVeriStageWorkFlowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));

                //            //lblCurrentStatus.Text = obj_MiningNewVeriStageWorkFlowHistory.GetActionInternalStatusLatest(Convert.ToInt64(lblAppCode.Text)).InternalStatusDesc;
                //            lblCurrentStatus.Text = obj_MiningNewVeriStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }

                //        else if (Convert.ToString(obj_MiningAppVerificationStatus.WorkFlowInfo.ApplicationProcessingStageComplete) == "NotDefined")
                //        {
                //            lblSerialNo.Text = HttpUtility.HtmlEncode(arr2[arr2.Length - 1].SerialNumber.ToString());
                //            NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewAppProcessStageWorkFlowHistory obj_MiningNewAppProcessStageWorkFlowHistory = new NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewAppProcessStageWorkFlowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //            lblCurrentStatus.Text = obj_MiningNewAppProcessStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }
                //        else if (Convert.ToString(obj_MiningAppVerificationStatus.WorkFlowInfo.NOCProcessingStageComplete) == "NotDefined")
                //        {
                //            lblSerialNo.Text = HttpUtility.HtmlEncode(arr3[arr3.Length - 1].SerialNumber.ToString());
                //            NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewNOCProcessStageWorkFlowHistory obj_MiningNewNOCProcessStageWorkFlowHistory = new NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewNOCProcessStageWorkFlowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //            lblCurrentStatus.Text = obj_MiningNewNOCProcessStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }
                //        else if (Convert.ToString(obj_MiningAppVerificationStatus.WorkFlowInfo.ApplicationDisbursementStageComplete) == "NotDefined")
                //        {
                //            lblSerialNo.Text = HttpUtility.HtmlEncode(arr4[arr4.Length - 1].SerialNumber.ToString());
                //            NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewAppDisbuStageWorkflowHistory obj_MiningNewAppDisbuStageWorkflowHistory = new NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewAppDisbuStageWorkflowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //            lblCurrentStatus.Text = obj_MiningNewAppDisbuStageWorkflowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }



                //        else if (Convert.ToString(obj_MiningAppVerificationStatus.WorkFlowInfo.ApplicationDisbursementStageComplete) == "Yes")
                //        {
                //            lblSerialNo.Text = HttpUtility.HtmlEncode(arr4[arr4.Length - 1].SerialNumber.ToString());
                //            NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewAppDisbuStageWorkflowHistory obj_MiningNewAppDisbuStageWorkflowHistory = new NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewAppDisbuStageWorkflowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //            lblCurrentStatus.Text = obj_MiningNewAppDisbuStageWorkflowHistory.GetActionInternalStatus().InternalStatusDesc;
                //            lblReceiveDate.Text = Convert.ToDateTime(obj_MiningNewAppDisbuStageWorkflowHistory.ReceiveDate).ToString("dd/MM/yyyy");
                //        }


                //    }
                //    else if (Convert.ToString(obj_MiningAppVerificationStatus.WorkFlowInfo.VerificationStageComplete) == "NotDefined")
                //    {
                //        lblSerialNo.Text = HttpUtility.HtmlEncode(arr1[arr1.Length - 1].SerialNumber.ToString());
                //        lblCurrentStage.Text = "Verification Stage";
                //        NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewVeriStageWorkFlowHistory obj_MiningNewVeriStageWorkFlowHistory = new NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewVeriStageWorkFlowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //        if (obj_MiningNewVeriStageWorkFlowHistory.GetActionInternalStatus() != null)
                //        {
                //            lblCurrentStatus.Text = obj_MiningNewVeriStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }
                //        lblReceiveDate.Text = Convert.ToDateTime(obj_MiningNewVeriStageWorkFlowHistory.ReceiveDate).ToString("dd/MM/yyyy");


                //    }

                //    else if (Convert.ToString(obj_MiningAppVerificationStatus.WorkFlowInfo.ApplicationProcessingStageComplete) == "NotDefined")
                //    {
                //        lblSerialNo.Text = HttpUtility.HtmlEncode(arr2[arr2.Length - 1].SerialNumber.ToString());
                //        lblCurrentStage.Text = "Application Processing Stage";
                //        NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewAppProcessStageWorkFlowHistory obj_MiningNewAppProcessStageWorkFlowHistory = new NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewAppProcessStageWorkFlowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //        if (obj_MiningNewAppProcessStageWorkFlowHistory.GetActionInternalStatus() != null)
                //        {

                //            lblCurrentStatus.Text = obj_MiningNewAppProcessStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }
                //        lblReceiveDate.Text = Convert.ToDateTime(obj_MiningNewAppProcessStageWorkFlowHistory.ReceiveDate).ToString("dd/MM/yyyy");
                //    }
                //    else if (Convert.ToString(obj_MiningAppVerificationStatus.WorkFlowInfo.NOCProcessingStageComplete) == "NotDefined")
                //    {
                //        lblSerialNo.Text = HttpUtility.HtmlEncode(arr3[arr3.Length - 1].SerialNumber.ToString());
                //        lblCurrentStage.Text = "NOC Processing Stage";
                //        NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewNOCProcessStageWorkFlowHistory obj_MiningNewNOCProcessStageWorkFlowHistory = new NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewNOCProcessStageWorkFlowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //        if (obj_MiningNewNOCProcessStageWorkFlowHistory.GetActionInternalStatus() != null)
                //        {
                //            lblCurrentStatus.Text = obj_MiningNewNOCProcessStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }
                //        lblReceiveDate.Text = Convert.ToDateTime(obj_MiningNewNOCProcessStageWorkFlowHistory.ReceiveDate).ToString("dd/MM/yyyy");
                //    }
                //    else if (Convert.ToString(obj_MiningAppVerificationStatus.WorkFlowInfo.ApplicationDisbursementStageComplete) == "NotDefined")
                //    {
                //        lblSerialNo.Text = HttpUtility.HtmlEncode(arr4[arr4.Length - 1].SerialNumber.ToString());
                //        lblCurrentStage.Text = "Application Disbursment Stage";
                //        NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewAppDisbuStageWorkflowHistory obj_MiningNewAppDisbuStageWorkflowHistory = new NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewAppDisbuStageWorkflowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //        if (obj_MiningNewAppDisbuStageWorkflowHistory.GetActionInternalStatus() != null)
                //        {
                //            lblCurrentStatus.Text = obj_MiningNewAppDisbuStageWorkflowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }
                //        lblReceiveDate.Text = Convert.ToDateTime(obj_MiningNewAppDisbuStageWorkflowHistory.ReceiveDate).ToString("dd/MM/yyyy");
                //    }

                //}
                #endregion

                NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(Convert.ToInt32(lblAppCode.Text));
                NOCAP.BLL.Master.ApplicationStatus obj_ApplicationStatus = new NOCAP.BLL.Master.ApplicationStatus(obj_MiningRenewApplication.LatestApplicationStatusCode);

                // Processing Fee 

                if (obj_MiningRenewApplication.PayReq == NOCAP.BLL.Common.PaymentRequirementOption.PayReqOption.Yes)
                {
                    lblProcessingFee.Text =HttpUtility.HtmlEncode("Rs. " + obj_MiningRenewApplication.PayReqAmt + "/- (" + NOCAPExternalUtility.ParseInput(Convert.ToString(obj_MiningRenewApplication.PayReqAmt)) + ")");

                    //RowProcessFeeSubmitted.Visible = true;
                    switch (obj_MiningRenewApplication.PayAmtRecFinally)
                    {
                        case NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.Yes:
                            lblProcessingFeeSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.Yes)) + ")";
                            break;
                        case NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No:
                            lblProcessingFeeSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";
                            break;
                    }
                }
                else
                {
                    // RowProcessFeeSubmitted.Visible = false;
                    lblProcessingFeeSubmitted.Text = "No Fee";
                    lblProcessingFee.Text = "";
                }


                switch (obj_MiningRenewApplication.WaterChargeReq)
                {
                    case NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.WaterChargeReqYesNo.Yes:
                        lblAbsRestCharge.Text = "Rs. " + HttpUtility.HtmlEncode(obj_MiningRenewApplication.GWChargeAmtFinally) + "/- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(obj_MiningRenewApplication.GWChargeAmtFinally))) + ")";
                        lblAbsRestArear.Text = "Rs. " + HttpUtility.HtmlEncode(obj_MiningRenewApplication.GWArearAmtFinally) + "/- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(obj_MiningRenewApplication.GWArearAmtFinally))) + ")";

                        switch (obj_MiningRenewApplication.WaterChargeRecFinally)
                        {
                            case NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.WaterChargeRecFinallyYesNo.Yes:
                                lblAbsRestChargeSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.Yes)) + ")";
                                lblAbsRestArearSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.Yes)) + ")";

                                break;
                            case NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.WaterChargeRecFinallyYesNo.No:
                                lblAbsRestChargeSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";
                                lblAbsRestArearSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";

                                break;
                            case NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.WaterChargeRecFinallyYesNo.NotDefine:
                                lblAbsRestChargeSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";
                                lblAbsRestArearSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";

                                break;
                        }
                        break;
                    case NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.WaterChargeReqYesNo.No:
                        lblAbsRestChargeSubmitted.Text = "No Charge";
                        lblAbsRestCharge.Text = "";
                        lblAbsRestArearSubmitted.Text = "No Arear";
                        lblAbsRestArear.Text = "";
                        break;
                    case NOCAP.BLL.Mining.Renew.Common.MiningRenewApplicationB.WaterChargeReqYesNo.NotDefine:
                        lblAbsRestChargeSubmitted.Text = "No Charge";
                        lblAbsRestCharge.Text = "";
                        lblAbsRestArearSubmitted.Text = "No Arear";
                        lblAbsRestArear.Text = "";
                        break;

                }

                // End Processing Fee

                if (!string.IsNullOrEmpty(obj_ApplicationStatus.ApplicationStatusDescription))
                {
                    lblLatesStatus.Text = HttpUtility.HtmlEncode(obj_ApplicationStatus.ApplicationStatusDescription);
                    lblLatesStatus1.Text = HttpUtility.HtmlEncode(obj_ApplicationStatus.ApplicationStatusDescription);
                }

                if (obj_MiningRenewApplication.FileClose == NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication.FileCloseOption.Yes)
                {
                    RowCurrentStatus.Visible = false;
                    RowCurrentStage.Visible = false;
                    RowAddress.Visible = false;
                    RowFinalStatus.Visible = true;
                }
                else if (obj_MiningRenewApplication.FileClose == NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication.FileCloseOption.No)
                {
                    RowCurrentStatus.Visible = true;
                    RowCurrentStage.Visible = true;
                    RowAddress.Visible = true;
                    RowFinalStatus.Visible = false;
                }

                NOCAP.BLL.Common.CurrentStatus obj_CurrentStatus = new NOCAP.BLL.Common.CurrentStatus();
                obj_CurrentStatus = obj_MiningRenewApplication.GetCurrentStatus();
                lblCurrentStage.Text = HttpUtility.HtmlEncode(obj_CurrentStatus.CurrentStage);




                NOCAP.BLL.WorkFlow.WorkFlowStage obj_workFlowStage = new NOCAP.BLL.WorkFlow.WorkFlowStage(Convert.ToInt32(obj_MiningRenewApplication.LatestWorkFlowStagesCode));
                lblCurrentStage.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_workFlowStage.WorkFlowStageDesc));


                //Start For Presentation 
                if (obj_MiningRenewApplication.PresentationCalledSerialNumber != null)
                {
                    NOCAP.BLL.Misc.Presentation.PresentationCalled obj_PresentationCalled = new NOCAP.BLL.Misc.Presentation.PresentationCalled(Convert.ToInt64(lblAppCode.Text), obj_MiningRenewApplication.PresentationCalledSerialNumber);
                    if (obj_PresentationCalled.ApplicationCode != 0)
                    {
                        if (obj_PresentationCalled.PresentCalled == NOCAP.BLL.Common.CommonEnum.PresentationCalledOption.Yes)
                        {
                            NOCAP.BLL.Misc.Presentation.PresentationDetail Obj_PresentationDetail = new NOCAP.BLL.Misc.Presentation.PresentationDetail();
                            NOCAP.BLL.Misc.Presentation.PresentationDetail[] Obj_PresentationDetailList;
                            Obj_PresentationDetailList = Obj_PresentationDetail.GetAllPresentationDetailForApplicationCode(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(obj_PresentationCalled.PresentationCalledSerialNumber), NOCAP.BLL.Misc.Presentation.PresentationDetail.SortingField.PresentationSerialNumber);
                            if (obj_PresentationCalled.PresentationFinalize == NOCAP.BLL.Common.CommonEnum.PresentationFinalizeOption.No)
                            {

                                txtReqPresentGist.Visible = true;
                                lblPresentReq.Visible = true;
                                lblReqPresentGist.Visible = true;
                                txtReqPresentGist.Text = HttpUtility.HtmlEncode(obj_PresentationCalled.RequiredPresentationGist);
                                if (Obj_PresentationDetailList.Length != 0)
                                {
                                    lblPresentDateSche.Text = "<br />Date : " + HttpUtility.HtmlEncode((Convert.ToDateTime(Obj_PresentationDetailList[Obj_PresentationDetailList.Length - 1].PresentationDateSchedule).ToString("dd/MM/yyyy")));

                                    lblPresentDateTimeInHours.Text = "<br />Time : " + HttpUtility.HtmlEncode(Convert.ToString(Obj_PresentationDetailList[Obj_PresentationDetailList.Length - 1].PresentationDateTimeInHours)) + " Hrs";

                                    lblPresentAddress.Visible = true;
                                    txtPresentAddress.Visible = true;
                                    txtPresentAddress.Text = HttpUtility.HtmlEncode(Obj_PresentationDetailList[Obj_PresentationDetailList.Length - 1].PresentationAddress);
                                }
                            }

                        }
                    }
                }
                //End For Presentation 




                if (string.IsNullOrEmpty(Convert.ToString(obj_CurrentStatus.ReceiveDate)))
                {
                    obj_CurrentStatus.ReceiveDate = null;
                }
                else
                {
                    lblReceiveDate.Text = HttpUtility.HtmlEncode((Convert.ToDateTime(obj_CurrentStatus.ReceiveDate)).ToString("dd/MM/yyyy"));
                }


                if (!string.IsNullOrEmpty(obj_CurrentStatus.CurrentUserAddress))
                {
                    txtAddress.Text = HttpUtility.HtmlEncode(obj_CurrentStatus.CurrentUserAddress + obj_CurrentStatus.CurrentUserDistrict + obj_CurrentStatus.CurrentUserState);
                }
                else
                {
                    txtAddress.Text = "";
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
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
    protected void grdVerificationMiningApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Mining.Renew.WorkFlowHistory.MiningRenewVeriStageWorkFlowHistory objKeys_MiningRenewVeriStageWorkFlowHistory = (NOCAP.BLL.Mining.Renew.WorkFlowHistory.MiningRenewVeriStageWorkFlowHistory)e.Row.DataItem;

            string ApplicationCode = objKeys_MiningRenewVeriStageWorkFlowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_MiningRenewVeriStageWorkFlowHistory.SerialNumber.ToString();
            NOCAP.BLL.Mining.Renew.WorkFlowHistory.MiningRenewVeriStageWorkFlowHistory obj_MiningRenewVeriStageWorkFlowHistory = new NOCAP.BLL.Mining.Renew.WorkFlowHistory.MiningRenewVeriStageWorkFlowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
            try
            {
                Label lblForwardedUser = (Label)e.Row.FindControl("lblForwardedUser");
                Label lblToUser = (Label)e.Row.FindControl("lblToUser");
                Label lblFromUser = (Label)e.Row.FindControl("lblFromUser");
                Label lblActionInternalStatus = (Label)e.Row.FindControl("lblActionInternalStatus");
                Label lblFromUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblFromUserRegionOrDistrictOrHqName");
                Label lblToUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblToUserRegionOrDistrictOrHqName");
                Label lblForwardedUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblForwardedUserRegionOrDistrictOrHqName");

                if (obj_MiningRenewVeriStageWorkFlowHistory.GetMiningRenewApplication().GetFirstMiningApplication().NameOfMining != null && lblApplicationRenewCode.Text != null)
                    lblMiningName.Text = HttpUtility.HtmlEncode(obj_MiningRenewVeriStageWorkFlowHistory.GetMiningRenewApplication().GetFirstMiningApplication().NameOfMining);

                if (obj_MiningRenewVeriStageWorkFlowHistory.GetMiningRenewApplication().GetFirstMiningApplication().MiningNewApplicationNumber != null && lblApplicationRenewCode.Text != null)
                    lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_MiningRenewVeriStageWorkFlowHistory.GetMiningRenewApplication().GetFirstMiningApplication().MiningNewApplicationNumber);
                if (obj_MiningRenewVeriStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningRenewVeriStageWorkFlowHistory.FromUserCode));
                    if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 1)
                    {
                        lblFromUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetDistrictOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 2)
                    {
                        lblFromUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetRegionalOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 3)
                    {
                        lblFromUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetHeadQuarterName());
                    }
                    lblFromUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                }
                if (obj_MiningRenewVeriStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningRenewVeriStageWorkFlowHistory.ToUserCode));
                    lblToUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                    if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 1)
                    {
                        lblToUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetDistrictOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 2)
                    {
                        lblToUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetRegionalOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 3)
                    {
                        lblToUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetHeadQuarterName());
                    }
                }
                if (obj_MiningRenewVeriStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningRenewVeriStageWorkFlowHistory.ForwardedUserCode));
                    lblForwardedUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                    if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 1)
                    {
                        lblForwardedUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetDistrictOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 2)
                    {
                        lblForwardedUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetRegionalOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 3)
                    {
                        lblForwardedUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetHeadQuarterName());
                    }
                }

                if (obj_MiningRenewVeriStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_MiningRenewVeriStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                obj_MiningRenewVeriStageWorkFlowHistory = null;
            }
        }
    }
    protected void grdAppProcessingMiningApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Mining.Renew.WorkFlowHistory.MiningRenewAppProcessStageWorkFlowHistory objKeys_MiningRenewAppProcessStageWorkFlowHistory = (NOCAP.BLL.Mining.Renew.WorkFlowHistory.MiningRenewAppProcessStageWorkFlowHistory)e.Row.DataItem;
            string ApplicationCode = objKeys_MiningRenewAppProcessStageWorkFlowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_MiningRenewAppProcessStageWorkFlowHistory.SerialNumber.ToString();
            NOCAP.BLL.Mining.Renew.WorkFlowHistory.MiningRenewAppProcessStageWorkFlowHistory obj_MiningRenewAppStageWorkFlowHistory = new NOCAP.BLL.Mining.Renew.WorkFlowHistory.MiningRenewAppProcessStageWorkFlowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
            try
            {
                Label lblForwardedUser = (Label)e.Row.FindControl("lblForwardedUser");
                Label lblToUser = (Label)e.Row.FindControl("lblToUser");
                Label lblFromUser = (Label)e.Row.FindControl("lblFromUser");
                Label lblActionInternalStatus = (Label)e.Row.FindControl("lblActionInternalStatus");
                LinkButton lnkbtPresentation = (LinkButton)e.Row.FindControl("lnkbtPresentation");
                Label lblPresentPreviousDt = (Label)e.Row.FindControl("lblPresentPreviousDt");

                //---Added by Chirag on 07-Apr-2016 to get Office Name--------------------------------------------------------
                Label lblFromUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblFromUserRegionOrDistrictOrHqName");
                Label lblToUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblToUserRegionOrDistrictOrHqName");
                Label lblForwardedUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblForwardedUserRegionOrDistrictOrHqName");
                //--------------------------------------------------------------------------------------------------------------------

                if (obj_MiningRenewAppStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningRenewAppStageWorkFlowHistory.FromUserCode));
                    lblFromUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                    if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 1)
                    {
                        lblFromUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetDistrictOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 2)
                    {
                        lblFromUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetRegionalOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 3)
                    {
                        lblFromUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetHeadQuarterName());
                    }
                }
                if (obj_MiningRenewAppStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningRenewAppStageWorkFlowHistory.ToUserCode));
                    lblToUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                    if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 1)
                    {
                        lblToUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetDistrictOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 2)
                    {
                        lblToUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetRegionalOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 3)
                    {
                        lblToUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetHeadQuarterName());
                    }
                }
                if (obj_MiningRenewAppStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningRenewAppStageWorkFlowHistory.ForwardedUserCode));
                    lblForwardedUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                    if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 1)
                    {
                        lblForwardedUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetDistrictOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 2)
                    {
                        lblForwardedUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetRegionalOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 3)
                    {
                        lblForwardedUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetHeadQuarterName());
                    }
                }

                if (obj_MiningRenewAppStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_MiningRenewAppStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);

                //Start Presentation
                if (obj_MiningRenewAppStageWorkFlowHistory.PresentationCalledSerialNumber != null && lnkbtPresentation != null)
                {
                    NOCAP.BLL.Misc.Presentation.PresentationCalled obj_PresentationCalled = new NOCAP.BLL.Misc.Presentation.PresentationCalled(Convert.ToInt64(ApplicationCode), obj_MiningRenewAppStageWorkFlowHistory.PresentationCalledSerialNumber);
                    if (obj_PresentationCalled.ApplicationCode != 0)
                    {
                        if (obj_PresentationCalled.PresentCalled == NOCAP.BLL.Common.CommonEnum.PresentationCalledOption.Yes)
                        {
                            lnkbtPresentation.Visible = true;

                            NOCAP.BLL.Misc.Presentation.PresentationDetail Obj_PresentationDetail = new NOCAP.BLL.Misc.Presentation.PresentationDetail();
                            NOCAP.BLL.Misc.Presentation.PresentationDetail[] Obj_PresentationDetailList;
                            Obj_PresentationDetailList = Obj_PresentationDetail.GetAllPresentationDetailForApplicationCode(Convert.ToInt64(ApplicationCode), Convert.ToInt32(obj_MiningRenewAppStageWorkFlowHistory.PresentationCalledSerialNumber), NOCAP.BLL.Misc.Presentation.PresentationDetail.SortingField.PresentationSerialNumber);
                            StringBuilder obj_PresentPrevDt = new StringBuilder();
                            if (Obj_PresentationDetailList.Length != 0)
                            {
                                for (int i = 0; i < Obj_PresentationDetailList.Length; i++)
                                {
                                    obj_PresentPrevDt.Append(HttpUtility.HtmlEncode((Convert.ToDateTime(Obj_PresentationDetailList[i].PresentationDateSchedule).ToString("dd/MM/yyyy"))));
                                    obj_PresentPrevDt.Append("  - ");
                                    obj_PresentPrevDt.Append(Obj_PresentationDetailList[i].PresentationAttended.ToString());
                                    obj_PresentPrevDt.Append("<br/>");
                                }
                                lblPresentPreviousDt.Text =HttpUtility.HtmlEncode(obj_PresentPrevDt.ToString());
                            }
                        }
                    }
                }
                //Start Presentation


            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                obj_MiningRenewAppStageWorkFlowHistory = null;
            }
        }
    }
    protected void grdNOCProcessingMiningApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Mining.Renew.WorkFlowHistory.MiningRenewNOCProcessStageWorkFlowHistory objKeys_MiningRenewNOCProcessStageWorkFlowHistory = (NOCAP.BLL.Mining.Renew.WorkFlowHistory.MiningRenewNOCProcessStageWorkFlowHistory)e.Row.DataItem;
            string ApplicationCode = objKeys_MiningRenewNOCProcessStageWorkFlowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_MiningRenewNOCProcessStageWorkFlowHistory.SerialNumber.ToString();
            NOCAP.BLL.Mining.Renew.WorkFlowHistory.MiningRenewNOCProcessStageWorkFlowHistory obj_MiningRenewNOCStageWorkFlowHistory = new NOCAP.BLL.Mining.Renew.WorkFlowHistory.MiningRenewNOCProcessStageWorkFlowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
            try
            {
                Label lblForwardedUser = (Label)e.Row.FindControl("lblForwardedUser");
                Label lblToUser = (Label)e.Row.FindControl("lblToUser");
                Label lblFromUser = (Label)e.Row.FindControl("lblFromUser");
                Label lblActionInternalStatus = (Label)e.Row.FindControl("lblActionInternalStatus");

                //---Added by Chirag on 07-Apr-2016 to get Office Name--------------------------------------------------------
                Label lblFromUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblFromUserRegionOrDistrictOrHqName");
                Label lblToUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblToUserRegionOrDistrictOrHqName");
                Label lblForwardedUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblForwardedUserRegionOrDistrictOrHqName");
                //----------------------------------------------------------------------------------------------------------------


                if (obj_MiningRenewNOCStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningRenewNOCStageWorkFlowHistory.FromUserCode));
                    lblFromUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                    if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 1)
                    {
                        lblFromUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetDistrictOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 2)
                    {
                        lblFromUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetRegionalOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 3)
                    {
                        lblFromUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetHeadQuarterName());
                    }

                }
                if (obj_MiningRenewNOCStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningRenewNOCStageWorkFlowHistory.ToUserCode));
                    lblToUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                    if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 1)
                    {
                        lblToUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetDistrictOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 2)
                    {
                        lblToUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetRegionalOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 3)
                    {
                        lblToUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetHeadQuarterName());
                    }

                }
                if (obj_MiningRenewNOCStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningRenewNOCStageWorkFlowHistory.ForwardedUserCode));
                    lblForwardedUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                    if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 1)
                    {
                        lblForwardedUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetDistrictOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 2)
                    {
                        lblForwardedUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetRegionalOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 3)
                    {
                        lblForwardedUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetHeadQuarterName());
                    }
                }
                if (obj_MiningRenewNOCStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_MiningRenewNOCStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                obj_MiningRenewNOCStageWorkFlowHistory = null;
            }
        }
    }
    protected void grdDisbursmentProcessingMiningApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Mining.Renew.WorkFlowHistory.MiningRenewAppDisbuStageWorkflowHistory objKeys_MiningRenewAppDisbuStageWorkflowHistory = (NOCAP.BLL.Mining.Renew.WorkFlowHistory.MiningRenewAppDisbuStageWorkflowHistory)e.Row.DataItem;
            string ApplicationCode = objKeys_MiningRenewAppDisbuStageWorkflowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_MiningRenewAppDisbuStageWorkflowHistory.SerialNumber.ToString();

            NOCAP.BLL.Mining.Renew.WorkFlowHistory.MiningRenewAppDisbuStageWorkflowHistory obj_MiningRenewDisbursmentStageWorkFlowHistory = new NOCAP.BLL.Mining.Renew.WorkFlowHistory.MiningRenewAppDisbuStageWorkflowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
            try
            {
                Label lblForwardedUser = (Label)e.Row.FindControl("lblForwardedUser");
                Label lblToUser = (Label)e.Row.FindControl("lblToUser");
                Label lblFromUser = (Label)e.Row.FindControl("lblFromUser");
                Label lblActionInternalStatus = (Label)e.Row.FindControl("lblActionInternalStatus");


                Label lblFromUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblFromUserRegionOrDistrictOrHqName");
                Label lblToUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblToUserRegionOrDistrictOrHqName");
                Label lblForwardedUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblForwardedUserRegionOrDistrictOrHqName");


                if (obj_MiningRenewDisbursmentStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningRenewDisbursmentStageWorkFlowHistory.FromUserCode));
                    lblFromUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                    if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 1)
                    {
                        lblFromUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetDistrictOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 2)
                    {
                        lblFromUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetRegionalOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 3)
                    {
                        lblFromUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetHeadQuarterName());
                    }

                }

                if (obj_MiningRenewDisbursmentStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningRenewDisbursmentStageWorkFlowHistory.ToUserCode));
                    lblToUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";

                    if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 1)
                    {
                        lblToUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetDistrictOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 2)
                    {
                        lblToUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetRegionalOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 3)
                    {
                        lblToUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetHeadQuarterName());
                    }


                }
                if (obj_MiningRenewDisbursmentStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningRenewDisbursmentStageWorkFlowHistory.ForwardedUserCode));
                    lblForwardedUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";

                    if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 1)
                    {
                        lblForwardedUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetDistrictOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 2)
                    {
                        lblForwardedUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetRegionalOfficeName());
                    }
                    else if (obj_Login.GetNOCAPRole().GetUserLevel().UserLevelCode == 3)
                    {
                        lblForwardedUserRegionOrDistrictOrHqName.Text = HttpUtility.HtmlEncode(obj_Login.GetHeadQuarterName());
                    }
                }
                if (obj_MiningRenewDisbursmentStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_MiningRenewDisbursmentStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                obj_MiningRenewDisbursmentStageWorkFlowHistory = null;
            }
        }
    }

    protected void gvMainGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string ApplicationCode = gvMainGrid.DataKeys[e.Row.RowIndex].Values[0].ToString();
                HiddenField hdnReferBackSN = (HiddenField)e.Row.FindControl("hdnReferBackSN");
                string ReferBackSN = hdnReferBackSN.Value;


                //Application Verification status


                NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningAppVerificationStatus = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Mining.Renew.WorkFlowHistory.MiningRenewVeriStageWorkFlowHistory[] arr1;
                arr1 = obj_MiningAppVerificationStatus.GetMiningRenewVeriStageWorkFlowHistoryListForReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.VeriStageWorkFlowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.VeriStageWorkFlowHistory.SortingField.NoSorting);
                GridView grdVerificationMiningApplicationStatus = (GridView)e.Row.FindControl("grdVerificationMiningApplicationStatus");
                if (arr1 != null && arr1.Length > 0)
                {
                    lblApplicationRenewCode.Text = HttpUtility.HtmlEncode(arr1[0].ApplicationCode.ToString());
                    lblSerialNo.Text = HttpUtility.HtmlEncode(arr1[0].SerialNumber.ToString());
                }
                grdVerificationMiningApplicationStatus.DataSource = arr1;
                grdVerificationMiningApplicationStatus.DataBind();

                NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningAppProcessingStatus = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Mining.Renew.WorkFlowHistory.MiningRenewAppProcessStageWorkFlowHistory[] arr2;
                arr2 = obj_MiningAppProcessingStatus.GetMiningRenewAppProcessStageWorkFlowHistoryListReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.AppProcessStageWorkFlowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.AppProcessStageWorkFlowHistory.SortingField.NoSorting);
                GridView grdAppProcessingMiningApplicationStatus = (GridView)e.Row.FindControl("grdAppProcessingMiningApplicationStatus");

                grdAppProcessingMiningApplicationStatus.DataSource = arr2;
                grdAppProcessingMiningApplicationStatus.DataBind();

                //NOC Processing Status
                NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningNOCProcessingStatus = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Mining.Renew.WorkFlowHistory.MiningRenewNOCProcessStageWorkFlowHistory[] arr3;
                arr3 = obj_MiningNOCProcessingStatus.GetMiningRenewNOCProcessStageWorkFlowHistoryListForReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.NOCProcessStageWorkFlowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.NOCProcessStageWorkFlowHistory.SortingField.NoSorting);
                GridView grdNOCProcessingMiningApplicationStatus = (GridView)e.Row.FindControl("grdNOCProcessingMiningApplicationStatus");

                grdNOCProcessingMiningApplicationStatus.DataSource = arr3;
                grdNOCProcessingMiningApplicationStatus.DataBind();


                //Disbursment Processing Status
                NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningDisbursmentProcessingStatus = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Mining.Renew.WorkFlowHistory.MiningRenewAppDisbuStageWorkflowHistory[] arr4;
                arr4 = obj_MiningDisbursmentProcessingStatus.GetMiningRenewDisbursmentProcessStageWorkFlowHistoryListForReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.AppDisbuStageWorkflowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.AppDisbuStageWorkflowHistory.SortingField.NoSorting);
                GridView grdDisbursmentProcessingMiningApplicationStatus = (GridView)e.Row.FindControl("grdDisbursmentProcessingMiningApplicationStatus");

                grdDisbursmentProcessingMiningApplicationStatus.DataSource = arr4;
                grdDisbursmentProcessingMiningApplicationStatus.DataBind();

            }
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
    }

    protected void grdAppProcessingMiningApplicationStatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Presentation")
        {
            if (e.CommandArgument != null && Convert.ToString(e.CommandArgument) != "")
            {
                if (!NOCAPExternalUtility.IsNumeric(Convert.ToString(e.CommandArgument)))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert(' Presentation Call SN allows only Numeric ');", true);
                    return;
                }
                string ApplicationCode = gvMainGrid.DataKeys[0].Values[0].ToString();
                NOCAP.BLL.Misc.Presentation.PresentationCalled obj_PresentationCalled = new NOCAP.BLL.Misc.Presentation.PresentationCalled(Convert.ToInt64(ApplicationCode), Convert.ToInt32(e.CommandArgument));
                if (obj_PresentationCalled.ApplicationCode != 0)
                {
                    if (obj_PresentationCalled.PresentCalled == NOCAP.BLL.Common.CommonEnum.PresentationCalledOption.Yes)
                    {
                        txtReqPresentGistComm.Text = HttpUtility.HtmlEncode(obj_PresentationCalled.RequiredPresentationGist);
                        ModalPopupExtender.Show();
                    }
                }
            }

        }
    }
    protected void btnGoBack_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRFs"]) != hidCSRFs.Value)
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        else
            Server.Transfer("~/ExternalUser/MiningRenew/RenewalDetail/MiningRenewDetail.aspx");
    }
}