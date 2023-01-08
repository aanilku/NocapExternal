using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class ExternalUser_IndustrialRenew_Status_IndustrialRenewStatus : System.Web.UI.Page
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
                lblApplicationCode.Text = HttpUtility.HtmlEncode(((Label)placeHolder.FindControl("lblApplicationCode")).Text);
                if (!NOCAPExternalUtility.IsNumeric(lblAppCode.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert(' Application Code allows only Numeric ');", true);
                    return;
                }

                NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplicationForMainGrid = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(Convert.ToInt32(lblAppCode.Text));

                if (obj_IndustrialRenewApplicationForMainGrid != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ApplicationCode");
                    dt.Columns.Add("ReferBackSN");
                    dt.Columns.Add("ReferBackHeader");
                    for (int i = obj_IndustrialRenewApplicationForMainGrid.ReferBackSN; i >= 0; i--)
                    {
                        DataRow dr = dt.NewRow();
                        dr["ApplicationCode"] = obj_IndustrialRenewApplicationForMainGrid.IndustrialRenewApplicationCode;
                        dr["ReferBackSN"] = i;
                        int ReferBackCountToShow = i + 1;
                        string ReferBackToShow = AddOrdinal(ReferBackCountToShow);                        
                        dr["ReferBackHeader"] = obj_IndustrialRenewApplicationForMainGrid.ReferBackSN == i ? "Current Status" : "Application is Refered Back " + ReferBackToShow + " Time";
                        dt.Rows.Add(dr);
                    }
                    gvMainGrid.DataSource = dt;
                    gvMainGrid.DataBind();
                }

                #region Commented
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //if (!(string.IsNullOrEmpty(lblApplicationRenewCode.Text) && string.IsNullOrEmpty(lblSerialNo.Text)))
                //{
                //    if (Convert.ToString(obj_IndustrialAppVerificationStatus.WorkFlowInfo.WorkFlowComplete) == "Yes")
                //    {
                //        lblCurrentStage.Text = "Completed";

                //        if (Convert.ToString(obj_IndustrialAppVerificationStatus.WorkFlowInfo.VerificationStageComplete) == "NotDefined")
                //        {
                //            lblSerialNo.Text = HttpUtility.HtmlEncode(arr1[arr1.Length - 1].SerialNumber.ToString());
                //            NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewVeriStageWorkFlowHistory obj_IndustrialNewVeriStageWorkFlowHistory = new NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewVeriStageWorkFlowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));

                //            //lblCurrentStatus.Text = obj_IndustrialNewVeriStageWorkFlowHistory.GetActionInternalStatusLatest(Convert.ToInt64(lblAppCode.Text)).InternalStatusDesc;
                //            lblCurrentStatus.Text = obj_IndustrialNewVeriStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }

                //        else if (Convert.ToString(obj_IndustrialAppVerificationStatus.WorkFlowInfo.ApplicationProcessingStageComplete) == "NotDefined")
                //        {
                //            lblSerialNo.Text = HttpUtility.HtmlEncode(arr2[arr2.Length - 1].SerialNumber.ToString());
                //            NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewAppProcessStageWorkFlowHistory obj_IndustrialNewAppProcessStageWorkFlowHistory = new NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewAppProcessStageWorkFlowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //            lblCurrentStatus.Text = obj_IndustrialNewAppProcessStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }
                //        else if (Convert.ToString(obj_IndustrialAppVerificationStatus.WorkFlowInfo.NOCProcessingStageComplete) == "NotDefined")
                //        {
                //            lblSerialNo.Text = HttpUtility.HtmlEncode(arr3[arr3.Length - 1].SerialNumber.ToString());
                //            NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewNOCProcessStageWorkFlowHistory obj_IndustrialNewNOCProcessStageWorkFlowHistory = new NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewNOCProcessStageWorkFlowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //            lblCurrentStatus.Text = obj_IndustrialNewNOCProcessStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }
                //        else if (Convert.ToString(obj_IndustrialAppVerificationStatus.WorkFlowInfo.ApplicationDisbursementStageComplete) == "NotDefined")
                //        {
                //            lblSerialNo.Text = HttpUtility.HtmlEncode(arr4[arr4.Length - 1].SerialNumber.ToString());
                //            NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewAppDisbuStageWorkflowHistory obj_IndustrialNewAppDisbuStageWorkflowHistory = new NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewAppDisbuStageWorkflowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //            lblCurrentStatus.Text = obj_IndustrialNewAppDisbuStageWorkflowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }



                //        else if (Convert.ToString(obj_IndustrialAppVerificationStatus.WorkFlowInfo.ApplicationDisbursementStageComplete) == "Yes")
                //        {
                //            lblSerialNo.Text = HttpUtility.HtmlEncode(arr4[arr4.Length - 1].SerialNumber.ToString());
                //            NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewAppDisbuStageWorkflowHistory obj_IndustrialNewAppDisbuStageWorkflowHistory = new NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewAppDisbuStageWorkflowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //            lblCurrentStatus.Text = obj_IndustrialNewAppDisbuStageWorkflowHistory.GetActionInternalStatus().InternalStatusDesc;
                //            lblReceiveDate.Text = Convert.ToDateTime(obj_IndustrialNewAppDisbuStageWorkflowHistory.ReceiveDate).ToString("dd/MM/yyyy");
                //        }


                //    }
                //    else if (Convert.ToString(obj_IndustrialAppVerificationStatus.WorkFlowInfo.VerificationStageComplete) == "NotDefined")
                //    {
                //        lblSerialNo.Text = HttpUtility.HtmlEncode(arr1[arr1.Length - 1].SerialNumber.ToString());
                //        lblCurrentStage.Text = "Verification Stage";
                //        NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewVeriStageWorkFlowHistory obj_IndustrialNewVeriStageWorkFlowHistory = new NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewVeriStageWorkFlowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //        if (obj_IndustrialNewVeriStageWorkFlowHistory.GetActionInternalStatus() != null)
                //        {
                //            lblCurrentStatus.Text = obj_IndustrialNewVeriStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }
                //        lblReceiveDate.Text = Convert.ToDateTime(obj_IndustrialNewVeriStageWorkFlowHistory.ReceiveDate).ToString("dd/MM/yyyy");


                //    }

                //    else if (Convert.ToString(obj_IndustrialAppVerificationStatus.WorkFlowInfo.ApplicationProcessingStageComplete) == "NotDefined")
                //    {
                //        lblSerialNo.Text = HttpUtility.HtmlEncode(arr2[arr2.Length - 1].SerialNumber.ToString());
                //        lblCurrentStage.Text = "Application Processing Stage";
                //        NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewAppProcessStageWorkFlowHistory obj_IndustrialNewAppProcessStageWorkFlowHistory = new NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewAppProcessStageWorkFlowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //        if (obj_IndustrialNewAppProcessStageWorkFlowHistory.GetActionInternalStatus() != null)
                //        {

                //            lblCurrentStatus.Text = obj_IndustrialNewAppProcessStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }
                //        lblReceiveDate.Text = Convert.ToDateTime(obj_IndustrialNewAppProcessStageWorkFlowHistory.ReceiveDate).ToString("dd/MM/yyyy");
                //    }
                //    else if (Convert.ToString(obj_IndustrialAppVerificationStatus.WorkFlowInfo.NOCProcessingStageComplete) == "NotDefined")
                //    {
                //        lblSerialNo.Text = HttpUtility.HtmlEncode(arr3[arr3.Length - 1].SerialNumber.ToString());
                //        lblCurrentStage.Text = "NOC Processing Stage";
                //        NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewNOCProcessStageWorkFlowHistory obj_IndustrialNewNOCProcessStageWorkFlowHistory = new NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewNOCProcessStageWorkFlowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //        if (obj_IndustrialNewNOCProcessStageWorkFlowHistory.GetActionInternalStatus() != null)
                //        {
                //            lblCurrentStatus.Text = obj_IndustrialNewNOCProcessStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }
                //        lblReceiveDate.Text = Convert.ToDateTime(obj_IndustrialNewNOCProcessStageWorkFlowHistory.ReceiveDate).ToString("dd/MM/yyyy");
                //    }
                //    else if (Convert.ToString(obj_IndustrialAppVerificationStatus.WorkFlowInfo.ApplicationDisbursementStageComplete) == "NotDefined")
                //    {
                //        lblSerialNo.Text = HttpUtility.HtmlEncode(arr4[arr4.Length - 1].SerialNumber.ToString());
                //        lblCurrentStage.Text = "Application Disbursment Stage";
                //        NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewAppDisbuStageWorkflowHistory obj_IndustrialNewAppDisbuStageWorkflowHistory = new NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewAppDisbuStageWorkflowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //        if (obj_IndustrialNewAppDisbuStageWorkflowHistory.GetActionInternalStatus() != null)
                //        {
                //            lblCurrentStatus.Text = obj_IndustrialNewAppDisbuStageWorkflowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }
                //        lblReceiveDate.Text = Convert.ToDateTime(obj_IndustrialNewAppDisbuStageWorkflowHistory.ReceiveDate).ToString("dd/MM/yyyy");
                //    }

                //}
                #endregion

                NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(Convert.ToInt32(lblAppCode.Text));
                NOCAP.BLL.Master.ApplicationStatus obj_ApplicationStatus = new NOCAP.BLL.Master.ApplicationStatus(obj_IndustrialRenewApplication.LatestApplicationStatusCode);

                // Processing Fee 

                if (obj_IndustrialRenewApplication.PayReq == NOCAP.BLL.Common.PaymentRequirementOption.PayReqOption.Yes)
                {
                    lblProcessingFee.Text =HttpUtility.HtmlEncode("Rs. " + obj_IndustrialRenewApplication.PayReqAmt + "/- (" + NOCAPExternalUtility.ParseInput(Convert.ToString(obj_IndustrialRenewApplication.PayReqAmt)) + ")");

                    //RowProcessFeeSubmitted.Visible = true;
                    switch (obj_IndustrialRenewApplication.PayAmtRecFinally)
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


                if (obj_IndustrialRenewApplication.WaterQualityCodeFinally != null)
                {
                    lblWaterQualityTypeApproved.Text = Convert.ToString(new NOCAP.BLL.Master.WaterQuality((int)obj_IndustrialRenewApplication.WaterQualityCodeFinally).WaterQualityDesc);
                }



                switch (obj_IndustrialRenewApplication.WaterChargeReqFinally)
                {
                    case NOCAP.BLL.Industrial.Renew.Common.IndustrialRenewApplicationB.WaterChargeReqFinallyYesNo.Yes:
                        lblWaterChargeReqFinally.Text = "Yes";

                        break;
                    case NOCAP.BLL.Industrial.Renew.Common.IndustrialRenewApplicationB.WaterChargeReqFinallyYesNo.No:
                        lblWaterChargeReqFinally.Text = "No";

                        break;
                    case NOCAP.BLL.Industrial.Renew.Common.IndustrialRenewApplicationB.WaterChargeReqFinallyYesNo.NotDefine:
                        lblWaterChargeReqFinally.Text = "Not Define";

                        break;
                    default:
                        lblWaterChargeReqFinally.Text = "Not Define";

                        break;
                }

                switch (obj_IndustrialRenewApplication.WaterChargeReq)
                {
                    case NOCAP.BLL.Industrial.Renew.Common.IndustrialRenewApplicationB.WaterChargeReqYesNo.Yes:
                        lblAbsRestCharge.Text = "Rs. " + HttpUtility.HtmlEncode(obj_IndustrialRenewApplication.GWChargeAmtFinally) + "/- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(obj_IndustrialRenewApplication.GWChargeAmtFinally))) + ")";
                        lblAbsRestArear.Text = "Rs. " + HttpUtility.HtmlEncode(obj_IndustrialRenewApplication.GWArearAmtFinally) + "/- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(obj_IndustrialRenewApplication.GWArearAmtFinally))) + ")";

                        switch (obj_IndustrialRenewApplication.WaterChargeRecFinally)
                        {
                            case NOCAP.BLL.Industrial.Renew.Common.IndustrialRenewApplicationB.WaterChargeRecFinallyYesNo.Yes:
                                lblAbsRestChargeSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.Yes)) + ")";
                                lblAbsRestArearSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.Yes)) + ")";

                                break;
                            case NOCAP.BLL.Industrial.Renew.Common.IndustrialRenewApplicationB.WaterChargeRecFinallyYesNo.No:
                                lblAbsRestChargeSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";
                                lblAbsRestArearSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";

                                break;
                            case NOCAP.BLL.Industrial.Renew.Common.IndustrialRenewApplicationB.WaterChargeRecFinallyYesNo.NotDefine:
                                lblAbsRestChargeSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";
                                lblAbsRestArearSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";

                                break;
                        }
                        break;
                    case NOCAP.BLL.Industrial.Renew.Common.IndustrialRenewApplicationB.WaterChargeReqYesNo.No:
                        lblAbsRestChargeSubmitted.Text = "No Charge";
                        lblAbsRestCharge.Text = "";
                        lblAbsRestArearSubmitted.Text = "No Arear";
                        lblAbsRestArear.Text = "";
                        break;
                    case NOCAP.BLL.Industrial.Renew.Common.IndustrialRenewApplicationB.WaterChargeReqYesNo.NotDefine:
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

                if (obj_IndustrialRenewApplication.FileClose == NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication.FileCloseOption.Yes)
                {
                    RowCurrentStatus.Visible = false;
                    RowCurrentStage.Visible = false;
                    RowAddress.Visible = false;
                    RowFinalStatus.Visible = true;
                }
                else if (obj_IndustrialRenewApplication.FileClose == NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication.FileCloseOption.No)
                {
                    RowCurrentStatus.Visible = true;
                    RowCurrentStage.Visible = true;
                    RowAddress.Visible = true;
                    RowFinalStatus.Visible = false;
                }

                NOCAP.BLL.Common.CurrentStatus obj_CurrentStatus = new NOCAP.BLL.Common.CurrentStatus();
                obj_CurrentStatus = obj_IndustrialRenewApplication.GetCurrentStatus();
                lblCurrentStage.Text = HttpUtility.HtmlEncode(obj_CurrentStatus.CurrentStage);


                

                NOCAP.BLL.WorkFlow.WorkFlowStage obj_workFlowStage = new NOCAP.BLL.WorkFlow.WorkFlowStage(Convert.ToInt32(obj_IndustrialRenewApplication.LatestWorkFlowStagesCode));
                lblCurrentStage.Text = HttpUtility.HtmlEncode(obj_workFlowStage.WorkFlowStageDesc);

                 
                //Start For Presentation 
                if (obj_IndustrialRenewApplication.PresentationCalledSerialNumber != null)
                {
                    NOCAP.BLL.Misc.Presentation.PresentationCalled obj_PresentationCalled = new NOCAP.BLL.Misc.Presentation.PresentationCalled(Convert.ToInt64(lblAppCode.Text), obj_IndustrialRenewApplication.PresentationCalledSerialNumber);
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
    protected void grdVerificationIndustrialApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Industrial.Renew.WorkFlowHistory.IndustrialRenewVeriStageWorkFlowHistory objKeys_IndustrialRenewVeriStageWorkFlowHistory = (NOCAP.BLL.Industrial.Renew.WorkFlowHistory.IndustrialRenewVeriStageWorkFlowHistory)e.Row.DataItem;
            
            string ApplicationCode = objKeys_IndustrialRenewVeriStageWorkFlowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_IndustrialRenewVeriStageWorkFlowHistory.SerialNumber.ToString();
            NOCAP.BLL.Industrial.Renew.WorkFlowHistory.IndustrialRenewVeriStageWorkFlowHistory obj_IndustrialRenewVeriStageWorkFlowHistory = new NOCAP.BLL.Industrial.Renew.WorkFlowHistory.IndustrialRenewVeriStageWorkFlowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
            try
            {
                Label lblForwardedUser = (Label)e.Row.FindControl("lblForwardedUser");
                Label lblToUser = (Label)e.Row.FindControl("lblToUser");
                Label lblFromUser = (Label)e.Row.FindControl("lblFromUser");
                Label lblActionInternalStatus = (Label)e.Row.FindControl("lblActionInternalStatus");
                Label lblFromUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblFromUserRegionOrDistrictOrHqName");
                Label lblToUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblToUserRegionOrDistrictOrHqName");
                Label lblForwardedUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblForwardedUserRegionOrDistrictOrHqName");

                if (obj_IndustrialRenewVeriStageWorkFlowHistory.GetIndustrialRenewApplication().NameOfIndustry != null && lblApplicationRenewCode.Text != null)
                    lblIndustoryName.Text = HttpUtility.HtmlEncode(obj_IndustrialRenewVeriStageWorkFlowHistory.GetIndustrialRenewApplication().NameOfIndustry);

                if (obj_IndustrialRenewVeriStageWorkFlowHistory.GetIndustrialRenewApplication().GetFirstIndustrialApplication().IndustrialNewApplicationNumber != null && lblApplicationRenewCode.Text != null)
                    lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_IndustrialRenewVeriStageWorkFlowHistory.GetIndustrialRenewApplication().GetFirstIndustrialApplication().IndustrialNewApplicationNumber);
                if (obj_IndustrialRenewVeriStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialRenewVeriStageWorkFlowHistory.FromUserCode));
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
                if (obj_IndustrialRenewVeriStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialRenewVeriStageWorkFlowHistory.ToUserCode));
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
                if (obj_IndustrialRenewVeriStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialRenewVeriStageWorkFlowHistory.ForwardedUserCode));
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

                if (obj_IndustrialRenewVeriStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_IndustrialRenewVeriStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                obj_IndustrialRenewVeriStageWorkFlowHistory = null;
            }
        }
    }
    protected void grdAppProcessingIndustrialApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Industrial.Renew.WorkFlowHistory.IndustrialRenewAppProcessStageWorkFlowHistory objKeys_IndustrialRenewAppProcessStageWorkFlowHistory = (NOCAP.BLL.Industrial.Renew.WorkFlowHistory.IndustrialRenewAppProcessStageWorkFlowHistory)e.Row.DataItem;
            string ApplicationCode = objKeys_IndustrialRenewAppProcessStageWorkFlowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_IndustrialRenewAppProcessStageWorkFlowHistory.SerialNumber.ToString();
            NOCAP.BLL.Industrial.Renew.WorkFlowHistory.IndustrialRenewAppProcessStageWorkFlowHistory obj_IndustrialRenewAppStageWorkFlowHistory = new NOCAP.BLL.Industrial.Renew.WorkFlowHistory.IndustrialRenewAppProcessStageWorkFlowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
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

                if (obj_IndustrialRenewAppStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialRenewAppStageWorkFlowHistory.FromUserCode));
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
                if (obj_IndustrialRenewAppStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialRenewAppStageWorkFlowHistory.ToUserCode));
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
                if (obj_IndustrialRenewAppStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialRenewAppStageWorkFlowHistory.ForwardedUserCode));
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

                if (obj_IndustrialRenewAppStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_IndustrialRenewAppStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);

                //Start Presentation
                if (obj_IndustrialRenewAppStageWorkFlowHistory.PresentationCalledSerialNumber != null && lnkbtPresentation != null)
                {
                    NOCAP.BLL.Misc.Presentation.PresentationCalled obj_PresentationCalled = new NOCAP.BLL.Misc.Presentation.PresentationCalled(Convert.ToInt64(ApplicationCode), obj_IndustrialRenewAppStageWorkFlowHistory.PresentationCalledSerialNumber);
                    if (obj_PresentationCalled.ApplicationCode != 0)
                    {
                        if (obj_PresentationCalled.PresentCalled == NOCAP.BLL.Common.CommonEnum.PresentationCalledOption.Yes)
                        {
                            lnkbtPresentation.Visible = true;

                            NOCAP.BLL.Misc.Presentation.PresentationDetail Obj_PresentationDetail = new NOCAP.BLL.Misc.Presentation.PresentationDetail();
                            NOCAP.BLL.Misc.Presentation.PresentationDetail[] Obj_PresentationDetailList;
                            Obj_PresentationDetailList = Obj_PresentationDetail.GetAllPresentationDetailForApplicationCode(Convert.ToInt64(ApplicationCode), Convert.ToInt32(obj_IndustrialRenewAppStageWorkFlowHistory.PresentationCalledSerialNumber), NOCAP.BLL.Misc.Presentation.PresentationDetail.SortingField.PresentationSerialNumber);
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
                obj_IndustrialRenewAppStageWorkFlowHistory = null;
            }
        }
    }
    protected void grdNOCProcessingIndustrialApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Industrial.Renew.WorkFlowHistory.IndustrialRenewNOCProcessStageWorkFlowHistory objKeys_IndustrialRenewNOCProcessStageWorkFlowHistory = (NOCAP.BLL.Industrial.Renew.WorkFlowHistory.IndustrialRenewNOCProcessStageWorkFlowHistory)e.Row.DataItem;
            string ApplicationCode = objKeys_IndustrialRenewNOCProcessStageWorkFlowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_IndustrialRenewNOCProcessStageWorkFlowHistory.SerialNumber.ToString();
            NOCAP.BLL.Industrial.Renew.WorkFlowHistory.IndustrialRenewNOCProcessStageWorkFlowHistory obj_IndustrialRenewNOCStageWorkFlowHistory = new NOCAP.BLL.Industrial.Renew.WorkFlowHistory.IndustrialRenewNOCProcessStageWorkFlowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
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


                if (obj_IndustrialRenewNOCStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialRenewNOCStageWorkFlowHistory.FromUserCode));
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
                if (obj_IndustrialRenewNOCStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialRenewNOCStageWorkFlowHistory.ToUserCode));
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
                if (obj_IndustrialRenewNOCStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialRenewNOCStageWorkFlowHistory.ForwardedUserCode));
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
                if (obj_IndustrialRenewNOCStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_IndustrialRenewNOCStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                obj_IndustrialRenewNOCStageWorkFlowHistory = null;
            }
        }
    }
    protected void grdDisbursmentProcessingIndustrialApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Industrial.Renew.WorkFlowHistory.IndustrialRenewAppDisbuStageWorkflowHistory objKeys_IndustrialRenewAppDisbuStageWorkflowHistory = (NOCAP.BLL.Industrial.Renew.WorkFlowHistory.IndustrialRenewAppDisbuStageWorkflowHistory)e.Row.DataItem;
            string ApplicationCode = objKeys_IndustrialRenewAppDisbuStageWorkflowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_IndustrialRenewAppDisbuStageWorkflowHistory.SerialNumber.ToString();
            
            NOCAP.BLL.Industrial.Renew.WorkFlowHistory.IndustrialRenewAppDisbuStageWorkflowHistory obj_IndustrialRenewDisbursmentStageWorkFlowHistory = new NOCAP.BLL.Industrial.Renew.WorkFlowHistory.IndustrialRenewAppDisbuStageWorkflowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
            try
            {
                Label lblForwardedUser = (Label)e.Row.FindControl("lblForwardedUser");
                Label lblToUser = (Label)e.Row.FindControl("lblToUser");
                Label lblFromUser = (Label)e.Row.FindControl("lblFromUser");
                Label lblActionInternalStatus = (Label)e.Row.FindControl("lblActionInternalStatus");


                Label lblFromUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblFromUserRegionOrDistrictOrHqName");
                Label lblToUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblToUserRegionOrDistrictOrHqName");
                Label lblForwardedUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblForwardedUserRegionOrDistrictOrHqName");


                if (obj_IndustrialRenewDisbursmentStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialRenewDisbursmentStageWorkFlowHistory.FromUserCode));
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

                if (obj_IndustrialRenewDisbursmentStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialRenewDisbursmentStageWorkFlowHistory.ToUserCode));
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
                if (obj_IndustrialRenewDisbursmentStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialRenewDisbursmentStageWorkFlowHistory.ForwardedUserCode));
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
                if (obj_IndustrialRenewDisbursmentStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_IndustrialRenewDisbursmentStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                obj_IndustrialRenewDisbursmentStageWorkFlowHistory = null;
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


                NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialAppVerificationStatus = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Industrial.Renew.WorkFlowHistory.IndustrialRenewVeriStageWorkFlowHistory[] arr1;
                arr1 = obj_IndustrialAppVerificationStatus.GetIndustrialRenewVeriStageWorkFlowHistoryListForReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.VeriStageWorkFlowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.VeriStageWorkFlowHistory.SortingField.NoSorting);
                GridView grdVerificationIndustrialApplicationStatus = (GridView)e.Row.FindControl("grdVerificationIndustrialApplicationStatus");
                if (arr1 != null && arr1.Length > 0)
                {
                    lblApplicationRenewCode.Text = HttpUtility.HtmlEncode(arr1[0].ApplicationCode.ToString());
                    lblSerialNo.Text = HttpUtility.HtmlEncode(arr1[0].SerialNumber.ToString());
                }
                grdVerificationIndustrialApplicationStatus.DataSource = arr1;
                grdVerificationIndustrialApplicationStatus.DataBind();
                
                NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialAppProcessingStatus = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Industrial.Renew.WorkFlowHistory.IndustrialRenewAppProcessStageWorkFlowHistory[] arr2;
                arr2 = obj_IndustrialAppProcessingStatus.GetIndustrialRenewAppProcessStageWorkFlowHistoryListReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.AppProcessStageWorkFlowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.AppProcessStageWorkFlowHistory.SortingField.NoSorting);
                GridView grdAppProcessingIndustrialApplicationStatus = (GridView)e.Row.FindControl("grdAppProcessingIndustrialApplicationStatus");
                
                grdAppProcessingIndustrialApplicationStatus.DataSource = arr2;
                grdAppProcessingIndustrialApplicationStatus.DataBind();
                
                //NOC Processing Status
                NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialNOCProcessingStatus = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Industrial.Renew.WorkFlowHistory.IndustrialRenewNOCProcessStageWorkFlowHistory[] arr3;
                arr3 = obj_IndustrialNOCProcessingStatus.GetIndustrialRenewNOCProcessStageWorkFlowHistoryListForReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.NOCProcessStageWorkFlowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.NOCProcessStageWorkFlowHistory.SortingField.NoSorting);
                GridView grdNOCProcessingIndustrialApplicationStatus = (GridView)e.Row.FindControl("grdNOCProcessingIndustrialApplicationStatus");
                
                grdNOCProcessingIndustrialApplicationStatus.DataSource = arr3;
                grdNOCProcessingIndustrialApplicationStatus.DataBind();
                

                //Disbursment Processing Status
                NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialDisbursmentProcessingStatus = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Industrial.Renew.WorkFlowHistory.IndustrialRenewAppDisbuStageWorkflowHistory[] arr4;
                arr4 = obj_IndustrialDisbursmentProcessingStatus.GetIndustrialRenewDisbursmentProcessStageWorkFlowHistoryListForReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.AppDisbuStageWorkflowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.AppDisbuStageWorkflowHistory.SortingField.NoSorting);
                GridView grdDisbursmentProcessingIndustrialApplicationStatus = (GridView)e.Row.FindControl("grdDisbursmentProcessingIndustrialApplicationStatus");
                
                grdDisbursmentProcessingIndustrialApplicationStatus.DataSource = arr4;
                grdDisbursmentProcessingIndustrialApplicationStatus.DataBind();
                
            }
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
    }
    protected void grdAppProcessingIndustrialApplicationStatus_RowCommand(object sender, GridViewCommandEventArgs e)
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
            Server.Transfer("~/ExternalUser/IndustrialRenew/RenewalDetail/IndustrialRenewDetail.aspx");
    }
}