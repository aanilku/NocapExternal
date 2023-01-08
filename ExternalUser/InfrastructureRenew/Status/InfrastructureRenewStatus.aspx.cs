using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;



public partial class ExternalUser_InfrastructureRenew_Status_InfrastructureRenewStatus : System.Web.UI.Page
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

                NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplicationForMainGrid = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(Convert.ToInt32(lblAppCode.Text));

                if (obj_InfrastructureRenewApplicationForMainGrid != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ApplicationCode");
                    dt.Columns.Add("ReferBackSN");
                    dt.Columns.Add("ReferBackHeader");
                    for (int i = obj_InfrastructureRenewApplicationForMainGrid.ReferBackSN; i >= 0; i--)
                    {
                        DataRow dr = dt.NewRow();
                        dr["ApplicationCode"] = obj_InfrastructureRenewApplicationForMainGrid.InfrastructureRenewApplicationCode;
                        dr["ReferBackSN"] = i;
                        int ReferBackCountToShow = i + 1;
                        string ReferBackToShow = AddOrdinal(ReferBackCountToShow);
                        //dr["ReferBackHeader"] = obj_InfrastructureRenewApplicationForMainGrid.ReferBackSN == i ? "Refer Back " + i + " - Present" : "Refer Back " + i;
                        dr["ReferBackHeader"] = obj_InfrastructureRenewApplicationForMainGrid.ReferBackSN == i ? "Current Status" : "Application is Refered Back " + ReferBackToShow + " Time";
                        dt.Rows.Add(dr);
                    }
                    gvMainGrid.DataSource = dt;
                    gvMainGrid.DataBind();
                }

                #region Commented
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //if (!(string.IsNullOrEmpty(lblApplicationRenewCode.Text) && string.IsNullOrEmpty(lblSerialNo.Text)))
                //{
                //    if (Convert.ToString(obj_InfrastructureAppVerificationStatus.WorkFlowInfo.WorkFlowComplete) == "Yes")
                //    {
                //        lblCurrentStage.Text = "Completed";

                //        if (Convert.ToString(obj_InfrastructureAppVerificationStatus.WorkFlowInfo.VerificationStageComplete) == "NotDefined")
                //        {
                //            lblSerialNo.Text = HttpUtility.HtmlEncode(arr1[arr1.Length - 1].SerialNumber.ToString());
                //            NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewVeriStageWorkFlowHistory obj_InfrastructureNewVeriStageWorkFlowHistory = new NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewVeriStageWorkFlowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));

                //            //lblCurrentStatus.Text = obj_InfrastructureNewVeriStageWorkFlowHistory.GetActionInternalStatusLatest(Convert.ToInt64(lblAppCode.Text)).InternalStatusDesc;
                //            lblCurrentStatus.Text = obj_InfrastructureNewVeriStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }

                //        else if (Convert.ToString(obj_InfrastructureAppVerificationStatus.WorkFlowInfo.ApplicationProcessingStageComplete) == "NotDefined")
                //        {
                //            lblSerialNo.Text = HttpUtility.HtmlEncode(arr2[arr2.Length - 1].SerialNumber.ToString());
                //            NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewAppProcessStageWorkFlowHistory obj_InfrastructureNewAppProcessStageWorkFlowHistory = new NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewAppProcessStageWorkFlowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //            lblCurrentStatus.Text = obj_InfrastructureNewAppProcessStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }
                //        else if (Convert.ToString(obj_InfrastructureAppVerificationStatus.WorkFlowInfo.NOCProcessingStageComplete) == "NotDefined")
                //        {
                //            lblSerialNo.Text = HttpUtility.HtmlEncode(arr3[arr3.Length - 1].SerialNumber.ToString());
                //            NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewNOCProcessStageWorkFlowHistory obj_InfrastructureNewNOCProcessStageWorkFlowHistory = new NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewNOCProcessStageWorkFlowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //            lblCurrentStatus.Text = obj_InfrastructureNewNOCProcessStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }
                //        else if (Convert.ToString(obj_InfrastructureAppVerificationStatus.WorkFlowInfo.ApplicationDisbursementStageComplete) == "NotDefined")
                //        {
                //            lblSerialNo.Text = HttpUtility.HtmlEncode(arr4[arr4.Length - 1].SerialNumber.ToString());
                //            NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewAppDisbuStageWorkflowHistory obj_InfrastructureNewAppDisbuStageWorkflowHistory = new NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewAppDisbuStageWorkflowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //            lblCurrentStatus.Text = obj_InfrastructureNewAppDisbuStageWorkflowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }



                //        else if (Convert.ToString(obj_InfrastructureAppVerificationStatus.WorkFlowInfo.ApplicationDisbursementStageComplete) == "Yes")
                //        {
                //            lblSerialNo.Text = HttpUtility.HtmlEncode(arr4[arr4.Length - 1].SerialNumber.ToString());
                //            NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewAppDisbuStageWorkflowHistory obj_InfrastructureNewAppDisbuStageWorkflowHistory = new NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewAppDisbuStageWorkflowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //            lblCurrentStatus.Text = obj_InfrastructureNewAppDisbuStageWorkflowHistory.GetActionInternalStatus().InternalStatusDesc;
                //            lblReceiveDate.Text = Convert.ToDateTime(obj_InfrastructureNewAppDisbuStageWorkflowHistory.ReceiveDate).ToString("dd/MM/yyyy");
                //        }


                //    }
                //    else if (Convert.ToString(obj_InfrastructureAppVerificationStatus.WorkFlowInfo.VerificationStageComplete) == "NotDefined")
                //    {
                //        lblSerialNo.Text = HttpUtility.HtmlEncode(arr1[arr1.Length - 1].SerialNumber.ToString());
                //        lblCurrentStage.Text = "Verification Stage";
                //        NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewVeriStageWorkFlowHistory obj_InfrastructureNewVeriStageWorkFlowHistory = new NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewVeriStageWorkFlowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //        if (obj_InfrastructureNewVeriStageWorkFlowHistory.GetActionInternalStatus() != null)
                //        {
                //            lblCurrentStatus.Text = obj_InfrastructureNewVeriStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }
                //        lblReceiveDate.Text = Convert.ToDateTime(obj_InfrastructureNewVeriStageWorkFlowHistory.ReceiveDate).ToString("dd/MM/yyyy");


                //    }

                //    else if (Convert.ToString(obj_InfrastructureAppVerificationStatus.WorkFlowInfo.ApplicationProcessingStageComplete) == "NotDefined")
                //    {
                //        lblSerialNo.Text = HttpUtility.HtmlEncode(arr2[arr2.Length - 1].SerialNumber.ToString());
                //        lblCurrentStage.Text = "Application Processing Stage";
                //        NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewAppProcessStageWorkFlowHistory obj_InfrastructureNewAppProcessStageWorkFlowHistory = new NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewAppProcessStageWorkFlowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //        if (obj_InfrastructureNewAppProcessStageWorkFlowHistory.GetActionInternalStatus() != null)
                //        {

                //            lblCurrentStatus.Text = obj_InfrastructureNewAppProcessStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }
                //        lblReceiveDate.Text = Convert.ToDateTime(obj_InfrastructureNewAppProcessStageWorkFlowHistory.ReceiveDate).ToString("dd/MM/yyyy");
                //    }
                //    else if (Convert.ToString(obj_InfrastructureAppVerificationStatus.WorkFlowInfo.NOCProcessingStageComplete) == "NotDefined")
                //    {
                //        lblSerialNo.Text = HttpUtility.HtmlEncode(arr3[arr3.Length - 1].SerialNumber.ToString());
                //        lblCurrentStage.Text = "NOC Processing Stage";
                //        NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewNOCProcessStageWorkFlowHistory obj_InfrastructureNewNOCProcessStageWorkFlowHistory = new NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewNOCProcessStageWorkFlowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //        if (obj_InfrastructureNewNOCProcessStageWorkFlowHistory.GetActionInternalStatus() != null)
                //        {
                //            lblCurrentStatus.Text = obj_InfrastructureNewNOCProcessStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }
                //        lblReceiveDate.Text = Convert.ToDateTime(obj_InfrastructureNewNOCProcessStageWorkFlowHistory.ReceiveDate).ToString("dd/MM/yyyy");
                //    }
                //    else if (Convert.ToString(obj_InfrastructureAppVerificationStatus.WorkFlowInfo.ApplicationDisbursementStageComplete) == "NotDefined")
                //    {
                //        lblSerialNo.Text = HttpUtility.HtmlEncode(arr4[arr4.Length - 1].SerialNumber.ToString());
                //        lblCurrentStage.Text = "Application Disbursment Stage";
                //        NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewAppDisbuStageWorkflowHistory obj_InfrastructureNewAppDisbuStageWorkflowHistory = new NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewAppDisbuStageWorkflowHistory(Convert.ToInt64(lblAppCode.Text), Convert.ToInt32(lblSerialNo.Text));
                //        if (obj_InfrastructureNewAppDisbuStageWorkflowHistory.GetActionInternalStatus() != null)
                //        {
                //            lblCurrentStatus.Text = obj_InfrastructureNewAppDisbuStageWorkflowHistory.GetActionInternalStatus().InternalStatusDesc;
                //        }
                //        lblReceiveDate.Text = Convert.ToDateTime(obj_InfrastructureNewAppDisbuStageWorkflowHistory.ReceiveDate).ToString("dd/MM/yyyy");
                //    }

                //}
                #endregion

                NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(Convert.ToInt32(lblAppCode.Text));
                NOCAP.BLL.Master.ApplicationStatus obj_ApplicationStatus = new NOCAP.BLL.Master.ApplicationStatus(obj_InfrastructureRenewApplication.LatestApplicationStatusCode);

                // Processing Fee 

                if (obj_InfrastructureRenewApplication.PayReq == NOCAP.BLL.Common.PaymentRequirementOption.PayReqOption.Yes)
                {
                    lblProcessingFee.Text =HttpUtility.HtmlEncode("Rs. " + obj_InfrastructureRenewApplication.PayReqAmt + "/- (" + NOCAPExternalUtility.ParseInput(Convert.ToString(obj_InfrastructureRenewApplication.PayReqAmt)) + ")");

                    //RowProcessFeeSubmitted.Visible = true;
                    switch (obj_InfrastructureRenewApplication.PayAmtRecFinally)
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
                if (obj_InfrastructureRenewApplication.WaterQualityCodeFinally != null)
                {
                    lblWaterQualityTypeApproved.Text = Convert.ToString(new NOCAP.BLL.Master.WaterQuality((int)obj_InfrastructureRenewApplication.WaterQualityCodeFinally).WaterQualityDesc);
                }



                switch (obj_InfrastructureRenewApplication.WaterChargeReqFinally)
                {
                    case NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.WaterChargeReqFinallyYesNo.Yes:
                        lblWaterChargeReqFinally.Text = "Yes";

                        break;
                    case NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.WaterChargeReqFinallyYesNo.No:
                        lblWaterChargeReqFinally.Text = "No";

                        break;
                    case NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.WaterChargeReqFinallyYesNo.NotDefine:
                        lblWaterChargeReqFinally.Text = "Not Define";

                        break;
                    default:
                        lblWaterChargeReqFinally.Text = "Not Define";

                        break;
                }
                switch (obj_InfrastructureRenewApplication.WaterChargeReq)
                {
                    case NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.WaterChargeReqYesNo.Yes:
                        lblAbsRestCharge.Text = "Rs. " + HttpUtility.HtmlEncode(obj_InfrastructureRenewApplication.GWChargeAmtFinally) + "/- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(obj_InfrastructureRenewApplication.GWChargeAmtFinally))) + ")";
                        lblAbsRestArear.Text = "Rs. " + HttpUtility.HtmlEncode(obj_InfrastructureRenewApplication.GWArearAmtFinally) + "/- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(obj_InfrastructureRenewApplication.GWArearAmtFinally))) + ")";

                        switch (obj_InfrastructureRenewApplication.WaterChargeRecFinally)
                        {
                            case NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.WaterChargeRecFinallyYesNo.Yes:
                                lblAbsRestChargeSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.Yes)) + ")";
                                lblAbsRestArearSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.Yes)) + ")";

                                break;
                            case NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.WaterChargeRecFinallyYesNo.No:
                                lblAbsRestChargeSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";
                                lblAbsRestArearSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";

                                break;
                            case NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.WaterChargeRecFinallyYesNo.NotDefine:
                                lblAbsRestChargeSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";
                                lblAbsRestArearSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";

                                break;
                        }
                        break;
                    case NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.WaterChargeReqYesNo.No:
                        lblAbsRestChargeSubmitted.Text = "No Charge";
                        lblAbsRestCharge.Text = "";
                        lblAbsRestArearSubmitted.Text = "No Arear";
                        lblAbsRestArear.Text = "";
                        break;
                    case NOCAP.BLL.Infrastructure.Renew.Common.InfrastructureRenewApplicationB.WaterChargeReqYesNo.NotDefine:
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

                if (obj_InfrastructureRenewApplication.FileClose == NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication.FileCloseOption.Yes)
                {
                    RowCurrentStatus.Visible = false;
                    RowCurrentStage.Visible = false;
                    RowAddress.Visible = false;
                    RowFinalStatus.Visible = true;
                }
                else if (obj_InfrastructureRenewApplication.FileClose == NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication.FileCloseOption.No)
                {
                    RowCurrentStatus.Visible = true;
                    RowCurrentStage.Visible = true;
                    RowAddress.Visible = true;
                    RowFinalStatus.Visible = false;
                }

                NOCAP.BLL.Common.CurrentStatus obj_CurrentStatus = new NOCAP.BLL.Common.CurrentStatus();
                obj_CurrentStatus = obj_InfrastructureRenewApplication.GetCurrentStatus();
                lblCurrentStage.Text = HttpUtility.HtmlEncode(obj_CurrentStatus.CurrentStage);




                NOCAP.BLL.WorkFlow.WorkFlowStage obj_workFlowStage = new NOCAP.BLL.WorkFlow.WorkFlowStage(Convert.ToInt32(obj_InfrastructureRenewApplication.LatestWorkFlowStagesCode));
                lblCurrentStage.Text = HttpUtility.HtmlEncode(obj_workFlowStage.WorkFlowStageDesc);


                //Start For Presentation 
                if (obj_InfrastructureRenewApplication.PresentationCalledSerialNumber != null)
                {
                    NOCAP.BLL.Misc.Presentation.PresentationCalled obj_PresentationCalled = new NOCAP.BLL.Misc.Presentation.PresentationCalled(Convert.ToInt64(lblAppCode.Text), obj_InfrastructureRenewApplication.PresentationCalledSerialNumber);
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
    protected void grdVerificationInfrastructureApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Infrastructure.Renew.WorkFlowHistory.InfrastructureRenewVeriStageWorkFlowHistory objKeys_InfrastructureRenewVeriStageWorkFlowHistory = (NOCAP.BLL.Infrastructure.Renew.WorkFlowHistory.InfrastructureRenewVeriStageWorkFlowHistory)e.Row.DataItem;

            string ApplicationCode = objKeys_InfrastructureRenewVeriStageWorkFlowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_InfrastructureRenewVeriStageWorkFlowHistory.SerialNumber.ToString();
            NOCAP.BLL.Infrastructure.Renew.WorkFlowHistory.InfrastructureRenewVeriStageWorkFlowHistory obj_InfrastructureRenewVeriStageWorkFlowHistory = new NOCAP.BLL.Infrastructure.Renew.WorkFlowHistory.InfrastructureRenewVeriStageWorkFlowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
            try
            {
                Label lblForwardedUser = (Label)e.Row.FindControl("lblForwardedUser");
                Label lblToUser = (Label)e.Row.FindControl("lblToUser");
                Label lblFromUser = (Label)e.Row.FindControl("lblFromUser");
                Label lblActionInternalStatus = (Label)e.Row.FindControl("lblActionInternalStatus");
                Label lblFromUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblFromUserRegionOrDistrictOrHqName");
                Label lblToUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblToUserRegionOrDistrictOrHqName");
                Label lblForwardedUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblForwardedUserRegionOrDistrictOrHqName");

                if (obj_InfrastructureRenewVeriStageWorkFlowHistory.GetInfrastructureRenewApplication().NameOfInfrastructure != null && lblApplicationRenewCode.Text != null)
                    lblInfrastructureName.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewVeriStageWorkFlowHistory.GetInfrastructureRenewApplication().NameOfInfrastructure);

                if (obj_InfrastructureRenewVeriStageWorkFlowHistory.GetInfrastructureRenewApplication().GetFirstInfrastructureApplication().InfrastructureNewApplicationNumber != null && lblApplicationRenewCode.Text != null)
                    lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewVeriStageWorkFlowHistory.GetInfrastructureRenewApplication().GetFirstInfrastructureApplication().InfrastructureNewApplicationNumber);
                if (obj_InfrastructureRenewVeriStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureRenewVeriStageWorkFlowHistory.FromUserCode));
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
                if (obj_InfrastructureRenewVeriStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureRenewVeriStageWorkFlowHistory.ToUserCode));
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
                if (obj_InfrastructureRenewVeriStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureRenewVeriStageWorkFlowHistory.ForwardedUserCode));
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

                if (obj_InfrastructureRenewVeriStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewVeriStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                obj_InfrastructureRenewVeriStageWorkFlowHistory = null;
            }
        }
    }
    protected void grdAppProcessingInfrastructureApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Infrastructure.Renew.WorkFlowHistory.InfrastructureRenewAppProcessStageWorkFlowHistory objKeys_InfrastructureRenewAppProcessStageWorkFlowHistory = (NOCAP.BLL.Infrastructure.Renew.WorkFlowHistory.InfrastructureRenewAppProcessStageWorkFlowHistory)e.Row.DataItem;
            string ApplicationCode = objKeys_InfrastructureRenewAppProcessStageWorkFlowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_InfrastructureRenewAppProcessStageWorkFlowHistory.SerialNumber.ToString();
            NOCAP.BLL.Infrastructure.Renew.WorkFlowHistory.InfrastructureRenewAppProcessStageWorkFlowHistory obj_InfrastructureRenewAppStageWorkFlowHistory = new NOCAP.BLL.Infrastructure.Renew.WorkFlowHistory.InfrastructureRenewAppProcessStageWorkFlowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
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

                if (obj_InfrastructureRenewAppStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureRenewAppStageWorkFlowHistory.FromUserCode));
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
                if (obj_InfrastructureRenewAppStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureRenewAppStageWorkFlowHistory.ToUserCode));
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
                if (obj_InfrastructureRenewAppStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureRenewAppStageWorkFlowHistory.ForwardedUserCode));
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

                if (obj_InfrastructureRenewAppStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewAppStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);

                //Start Presentation
                if (obj_InfrastructureRenewAppStageWorkFlowHistory.PresentationCalledSerialNumber != null && lnkbtPresentation != null)
                {
                    NOCAP.BLL.Misc.Presentation.PresentationCalled obj_PresentationCalled = new NOCAP.BLL.Misc.Presentation.PresentationCalled(Convert.ToInt64(ApplicationCode), obj_InfrastructureRenewAppStageWorkFlowHistory.PresentationCalledSerialNumber);
                    if (obj_PresentationCalled.ApplicationCode != 0)
                    {
                        if (obj_PresentationCalled.PresentCalled == NOCAP.BLL.Common.CommonEnum.PresentationCalledOption.Yes)
                        {
                            lnkbtPresentation.Visible = true;

                            NOCAP.BLL.Misc.Presentation.PresentationDetail Obj_PresentationDetail = new NOCAP.BLL.Misc.Presentation.PresentationDetail();
                            NOCAP.BLL.Misc.Presentation.PresentationDetail[] Obj_PresentationDetailList;
                            Obj_PresentationDetailList = Obj_PresentationDetail.GetAllPresentationDetailForApplicationCode(Convert.ToInt64(ApplicationCode), Convert.ToInt32(obj_InfrastructureRenewAppStageWorkFlowHistory.PresentationCalledSerialNumber), NOCAP.BLL.Misc.Presentation.PresentationDetail.SortingField.PresentationSerialNumber);
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
                obj_InfrastructureRenewAppStageWorkFlowHistory = null;
            }
        }
    }
    protected void grdNOCProcessingInfrastructureApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Infrastructure.Renew.WorkFlowHistory.InfrastructureRenewNOCProcessStageWorkFlowHistory objKeys_InfrastructureRenewNOCProcessStageWorkFlowHistory = (NOCAP.BLL.Infrastructure.Renew.WorkFlowHistory.InfrastructureRenewNOCProcessStageWorkFlowHistory)e.Row.DataItem;
            string ApplicationCode = objKeys_InfrastructureRenewNOCProcessStageWorkFlowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_InfrastructureRenewNOCProcessStageWorkFlowHistory.SerialNumber.ToString();
            NOCAP.BLL.Infrastructure.Renew.WorkFlowHistory.InfrastructureRenewNOCProcessStageWorkFlowHistory obj_InfrastructureRenewNOCStageWorkFlowHistory = new NOCAP.BLL.Infrastructure.Renew.WorkFlowHistory.InfrastructureRenewNOCProcessStageWorkFlowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
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


                if (obj_InfrastructureRenewNOCStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureRenewNOCStageWorkFlowHistory.FromUserCode));
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
                if (obj_InfrastructureRenewNOCStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureRenewNOCStageWorkFlowHistory.ToUserCode));
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
                if (obj_InfrastructureRenewNOCStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureRenewNOCStageWorkFlowHistory.ForwardedUserCode));
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
                if (obj_InfrastructureRenewNOCStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewNOCStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                obj_InfrastructureRenewNOCStageWorkFlowHistory = null;
            }
        }
    }
    protected void grdDisbursmentProcessingInfrastructureApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Infrastructure.Renew.WorkFlowHistory.InfrastructureRenewAppDisbuStageWorkflowHistory objKeys_InfrastructureRenewAppDisbuStageWorkflowHistory = (NOCAP.BLL.Infrastructure.Renew.WorkFlowHistory.InfrastructureRenewAppDisbuStageWorkflowHistory)e.Row.DataItem;
            string ApplicationCode = objKeys_InfrastructureRenewAppDisbuStageWorkflowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_InfrastructureRenewAppDisbuStageWorkflowHistory.SerialNumber.ToString();

            NOCAP.BLL.Infrastructure.Renew.WorkFlowHistory.InfrastructureRenewAppDisbuStageWorkflowHistory obj_InfrastructureRenewDisbursmentStageWorkFlowHistory = new NOCAP.BLL.Infrastructure.Renew.WorkFlowHistory.InfrastructureRenewAppDisbuStageWorkflowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
            try
            {
                Label lblForwardedUser = (Label)e.Row.FindControl("lblForwardedUser");
                Label lblToUser = (Label)e.Row.FindControl("lblToUser");
                Label lblFromUser = (Label)e.Row.FindControl("lblFromUser");
                Label lblActionInternalStatus = (Label)e.Row.FindControl("lblActionInternalStatus");


                Label lblFromUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblFromUserRegionOrDistrictOrHqName");
                Label lblToUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblToUserRegionOrDistrictOrHqName");
                Label lblForwardedUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblForwardedUserRegionOrDistrictOrHqName");


                if (obj_InfrastructureRenewDisbursmentStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureRenewDisbursmentStageWorkFlowHistory.FromUserCode));
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

                if (obj_InfrastructureRenewDisbursmentStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureRenewDisbursmentStageWorkFlowHistory.ToUserCode));
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
                if (obj_InfrastructureRenewDisbursmentStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureRenewDisbursmentStageWorkFlowHistory.ForwardedUserCode));
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
                if (obj_InfrastructureRenewDisbursmentStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewDisbursmentStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                obj_InfrastructureRenewDisbursmentStageWorkFlowHistory = null;
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


                NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureAppVerificationStatus = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Infrastructure.Renew.WorkFlowHistory.InfrastructureRenewVeriStageWorkFlowHistory[] arr1;
                arr1 = obj_InfrastructureAppVerificationStatus.GetInfrastructureRenewVeriStageWorkFlowHistoryListForReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.VeriStageWorkFlowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.VeriStageWorkFlowHistory.SortingField.NoSorting);
                GridView grdVerificationInfrastructureApplicationStatus = (GridView)e.Row.FindControl("grdVerificationInfrastructureApplicationStatus");
                if (arr1 != null && arr1.Length > 0)
                {
                    lblApplicationRenewCode.Text = HttpUtility.HtmlEncode(arr1[0].ApplicationCode.ToString());
                    lblSerialNo.Text = HttpUtility.HtmlEncode(arr1[0].SerialNumber.ToString());
                }
                grdVerificationInfrastructureApplicationStatus.DataSource = arr1;
                grdVerificationInfrastructureApplicationStatus.DataBind();

                NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureAppProcessingStatus = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Infrastructure.Renew.WorkFlowHistory.InfrastructureRenewAppProcessStageWorkFlowHistory[] arr2;
                arr2 = obj_InfrastructureAppProcessingStatus.GetInfrastructureRenewAppProcessStageWorkFlowHistoryListReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.AppProcessStageWorkFlowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.AppProcessStageWorkFlowHistory.SortingField.NoSorting);
                GridView grdAppProcessingInfrastructureApplicationStatus = (GridView)e.Row.FindControl("grdAppProcessingInfrastructureApplicationStatus");

                grdAppProcessingInfrastructureApplicationStatus.DataSource = arr2;
                grdAppProcessingInfrastructureApplicationStatus.DataBind();

                //NOC Processing Status
                NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureNOCProcessingStatus = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Infrastructure.Renew.WorkFlowHistory.InfrastructureRenewNOCProcessStageWorkFlowHistory[] arr3;
                arr3 = obj_InfrastructureNOCProcessingStatus.GetInfrastructureRenewNOCProcessStageWorkFlowHistoryListForReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.NOCProcessStageWorkFlowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.NOCProcessStageWorkFlowHistory.SortingField.NoSorting);
                GridView grdNOCProcessingInfrastructureApplicationStatus = (GridView)e.Row.FindControl("grdNOCProcessingInfrastructureApplicationStatus");

                grdNOCProcessingInfrastructureApplicationStatus.DataSource = arr3;
                grdNOCProcessingInfrastructureApplicationStatus.DataBind();


                //Disbursment Processing Status
                NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureDisbursmentProcessingStatus = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Infrastructure.Renew.WorkFlowHistory.InfrastructureRenewAppDisbuStageWorkflowHistory[] arr4;
                arr4 = obj_InfrastructureDisbursmentProcessingStatus.GetInfrastructureRenewDisbursmentProcessStageWorkFlowHistoryListForReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.AppDisbuStageWorkflowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.AppDisbuStageWorkflowHistory.SortingField.NoSorting);
                GridView grdDisbursmentProcessingInfrastructureApplicationStatus = (GridView)e.Row.FindControl("grdDisbursmentProcessingInfrastructureApplicationStatus");

                grdDisbursmentProcessingInfrastructureApplicationStatus.DataSource = arr4;
                grdDisbursmentProcessingInfrastructureApplicationStatus.DataBind();

            }
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
    }



    protected void grdAppProcessingInfrastructureApplicationStatus_RowCommand(object sender, GridViewCommandEventArgs e)
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
            Server.Transfer("~/ExternalUser/InfrastructureRenew/RenewalDetail/InfrastructureRenewDetail.aspx");
    }
}