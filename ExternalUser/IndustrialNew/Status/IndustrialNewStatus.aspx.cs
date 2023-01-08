using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class ExternalUser_IndustrialNew_Status_IndustrialNewStatus : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
                Label lblAppCode = (Label)placeHolder.FindControl("lblApplicationCode");
                if (!NOCAPExternalUtility.IsNumeric(lblAppCode.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert(' Application Code allows only Numeric ');", true);
                    return;
                }




                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplicationForMainGrid = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(Convert.ToInt32(lblAppCode.Text));

                if (obj_IndustrialNewApplicationForMainGrid != null)
                {


                    DataTable dt = new DataTable();
                    dt.Columns.Add("ApplicationCode");
                    dt.Columns.Add("ReferBackSN");
                    dt.Columns.Add("ReferBackHeader");
                    for (int i = obj_IndustrialNewApplicationForMainGrid.ReferBackSN; i >= 0; i--)
                    {
                        DataRow dr = dt.NewRow();
                        dr["ApplicationCode"] = obj_IndustrialNewApplicationForMainGrid.IndustrialNewApplicationCode;
                        dr["ReferBackSN"] = i;
                        //dr["ReferBackHeader"] = "Refer Back " + i;
                        int ReferBackCountToShow = i + 1;
                        string ReferBackToShow = AddOrdinal(ReferBackCountToShow);
                        dr["ReferBackHeader"] = obj_IndustrialNewApplicationForMainGrid.ReferBackSN == i ? "Current Status" : "Application is Refered Back " + ReferBackToShow + " Time";
                        dt.Rows.Add(dr);
                    }

                    gvMainGrid.DataSource = dt;
                    gvMainGrid.DataBind();


                }



                #region Commented
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //if (!(string.IsNullOrEmpty(lblApplicationCode.Text) && string.IsNullOrEmpty(lblSerialNo.Text)))
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


                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(Convert.ToInt32(lblAppCode.Text));
                NOCAP.BLL.Master.ApplicationStatus obj_ApplicationStatus = new NOCAP.BLL.Master.ApplicationStatus(obj_IndustrialNewApplication.LatestApplicationStatusCode);

                // Processing Fee 



                if (obj_IndustrialNewApplication.PayReq == NOCAP.BLL.Common.PaymentRequirementOption.PayReqOption.Yes)
                {

                    lblProcessingFee.Text = "Rs. " + HttpUtility.HtmlEncode(obj_IndustrialNewApplication.PayReqAmt) + "/- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(obj_IndustrialNewApplication.PayReqAmt))) + ")";

                    //RowProcessFeeSubmitted.Visible = true;
                    switch (obj_IndustrialNewApplication.PayAmtRecFinally)
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


                if (obj_IndustrialNewApplication.WaterQualityCodeFinally != null)
                {
                    lblWaterQualityTypeApproved.Text = Convert.ToString(new NOCAP.BLL.Master.WaterQuality((int)obj_IndustrialNewApplication.WaterQualityCodeFinally).WaterQualityDesc);
                }



                switch (obj_IndustrialNewApplication.WaterChargeReqFinally)
                {
                    case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.WaterChargeReqFinallyYesNo.Yes:
                        lblWaterChargeReqFinally.Text = "Yes";

                        break;
                    case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.WaterChargeReqFinallyYesNo.No:
                        lblWaterChargeReqFinally.Text = "No";

                        break;
                    case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.WaterChargeReqFinallyYesNo.NotDefine:
                        lblWaterChargeReqFinally.Text = "Not Define";

                        break;
                    default:
                        lblWaterChargeReqFinally.Text = "Not Define";

                        break;
                }


                switch (obj_IndustrialNewApplication.WaterChargeStatus)
                {
                    case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.WaterChargeStatusYesNo.Yes:
                        lblAbsRestCharge.Text = "Rs. " + HttpUtility.HtmlEncode(obj_IndustrialNewApplication.GWChargeAmtFinally) + "/- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(obj_IndustrialNewApplication.GWChargeAmtFinally))) + ")";
                        lblAbsRestArear.Text = "Rs. " + HttpUtility.HtmlEncode(obj_IndustrialNewApplication.GWArearAmtFinally) + "/- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(obj_IndustrialNewApplication.GWArearAmtFinally))) + ")";

                        switch (obj_IndustrialNewApplication.WaterChargeRecFinally)
                        {
                            case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.WaterChargeRecFinallyYesNo.Yes:
                                lblAbsRestChargeSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.Yes)) + ")";
                                lblAbsRestArearSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.Yes)) + ")";

                                break;
                            case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.WaterChargeRecFinallyYesNo.No:
                                lblAbsRestChargeSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";
                                lblAbsRestArearSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";

                                break;
                            case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.WaterChargeRecFinallyYesNo.NotDefine:
                                lblAbsRestChargeSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";
                                lblAbsRestArearSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";

                                break;
                        }
                        break;
                    case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.WaterChargeStatusYesNo.No:
                        lblAbsRestChargeSubmitted.Text = "No Charge";
                        lblAbsRestCharge.Text = "";
                        lblAbsRestArearSubmitted.Text = "No Arear";
                        lblAbsRestArear.Text = "";
                        break;
                    case NOCAP.BLL.Industrial.New.Common.IndustrialNewApplicationB.WaterChargeStatusYesNo.NotDefine:
                        lblAbsRestChargeSubmitted.Text = "No Charge";
                        lblAbsRestCharge.Text = "";
                        lblAbsRestArearSubmitted.Text = "No Arear";
                        lblAbsRestArear.Text = "";
                        break;

                }

                if (obj_IndustrialNewApplication.ECReqFinally == NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication.ECOption.Yes)
                {
                    chkBoxECReq.Checked = true;
                    chkBoxECReq.Enabled = false;
                }
                else
                {
                    chkBoxECReq.Checked = false;
                    chkBoxECReq.Enabled = false;
                }
                if (!String.IsNullOrEmpty(Convert.ToString(obj_IndustrialNewApplication.ECGWillegalFrom)))
                {
                    lblGWWillegalFrom.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_IndustrialNewApplication.ECGWillegalFrom).ToString("dd/MM/yyyy"));
                }
                if (!String.IsNullOrEmpty(Convert.ToString(obj_IndustrialNewApplication.ECGWillegalTo)))
                {
                    lblGWWillegalTo.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_IndustrialNewApplication.ECGWillegalTo).ToString("dd/MM/yyyy"));
                }
                lblillegalQty.Text = HttpUtility.HtmlEncode(obj_IndustrialNewApplication.ECillegalQty);
                lblECAmt.Text = HttpUtility.HtmlEncode(obj_IndustrialNewApplication.ECAmout);
                if (obj_IndustrialNewApplication.ECReasonCode != null)
                {
                    lblReasonForEC.Text = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.ECReason((int)obj_IndustrialNewApplication.ECReasonCode).ECReasonDesc));
                }








                NOCAP.BLL.PenaltyImpose.PenaltyImpose obj_penaltyImpose = new NOCAP.BLL.PenaltyImpose.PenaltyImpose();
                int int_status = 0;
                string str_sortfieldName = "";
                switch (str_sortfieldName)
                {
                    case "PenaltySN":
                        int_status = obj_penaltyImpose.GetALL(NOCAP.BLL.PenaltyImpose.PenaltyImpose.SortingField.PenaltySN);
                        break;
                    case "AppCode":
                        int_status = obj_penaltyImpose.GetALL(NOCAP.BLL.PenaltyImpose.PenaltyImpose.SortingField.AppCode);
                        break;
                    default:
                        int_status = obj_penaltyImpose.GetALL(NOCAP.BLL.PenaltyImpose.PenaltyImpose.SortingField.PenaltySN);
                        break;
                }
                NOCAP.BLL.PenaltyImpose.PenaltyImpose[] arr_penaltyImpose;

                arr_penaltyImpose = obj_penaltyImpose.PenaltyImposeCollection;
                if (lblApplicationCode.Text != "")
                    arr_penaltyImpose = arr_penaltyImpose.Where(a => a.AppCode == Convert.ToInt64(lblApplicationCode.Text)).ToArray();
                else
                    arr_penaltyImpose = arr_penaltyImpose.Where(a => a.AppCode == Convert.ToInt64(lblApplicationCode.Text)).ToArray();
                if ((int_status == 1))
                {
                    if (arr_penaltyImpose.Count() > 0)
                    {
                        gvShowPenalty.DataSource = arr_penaltyImpose;
                        gvShowPenalty.DataBind();

                    }
                }
                else
                {
                    ////lblMessage.Text = HttpUtility.HtmlEncode(obj_penaltyImpose.CustMessage);
                }
                // End Processing Fee

                if (!string.IsNullOrEmpty(obj_ApplicationStatus.ApplicationStatusDescription))
                {
                    lblLatesStatus.Text = HttpUtility.HtmlEncode(obj_ApplicationStatus.ApplicationStatusDescription);
                    lblLatesStatus1.Text = HttpUtility.HtmlEncode(obj_ApplicationStatus.ApplicationStatusDescription);
                }

                if (obj_IndustrialNewApplication.FileClose == NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication.FileCloseOption.Yes)
                {
                    RowCurrentStatus.Visible = false;
                    RowCurrentStage.Visible = false;
                    RowAddress.Visible = false;
                    RowFinalStatus.Visible = true;
                }
                else if (obj_IndustrialNewApplication.FileClose == NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication.FileCloseOption.No)
                {
                    RowCurrentStatus.Visible = true;
                    RowCurrentStage.Visible = true;
                    RowAddress.Visible = true;
                    RowFinalStatus.Visible = false;
                }



                NOCAP.BLL.Common.CurrentStatus obj_CurrentStatus = new NOCAP.BLL.Common.CurrentStatus();
                obj_CurrentStatus = obj_IndustrialNewApplication.GetCurrentStatus();

                lblCurrentStage.Text = HttpUtility.HtmlEncode(obj_CurrentStatus.CurrentStage);






                // Commented For Status

                //if (string.IsNullOrEmpty(obj_CurrentStatus.InternalStatus) && (obj_CurrentStatus.WFStagesCode == 1) && (obj_CurrentStatus.ReferBackSN != 0))
                //{
                //    obj_CurrentStatus.InternalStatus = "In Progress <b>(Clarification sought from Applicant, Please Contact)</b>";
                //    lblCurrentStatus.Text = obj_CurrentStatus.InternalStatus;

                //}
                //else if (string.IsNullOrEmpty(obj_CurrentStatus.InternalStatus))
                //{
                //    obj_CurrentStatus.InternalStatus = "In Progress";
                //    lblCurrentStatus.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_CurrentStatus.InternalStatus));
                //}
                //else
                //{
                //    lblCurrentStatus.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_CurrentStatus.InternalStatus));
                //}










                //Start For Presentation 
                if (obj_IndustrialNewApplication.PresentationCalledSerialNumber != null)
                {
                    NOCAP.BLL.Misc.Presentation.PresentationCalled obj_PresentationCalled = new NOCAP.BLL.Misc.Presentation.PresentationCalled(Convert.ToInt64(lblAppCode.Text), obj_IndustrialNewApplication.PresentationCalledSerialNumber);
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

                //Commented For Status
                //if (!string.IsNullOrEmpty(obj_CurrentStatus.FinalStatus))
                //{
                //    lblFinalStatus.Text = "(" + HttpUtility.HtmlEncode(obj_CurrentStatus.FinalStatus) + ")";
                //}
                //else
                //{
                //    lblFinalStatus.Text = "";
                //}

                if (!string.IsNullOrEmpty(obj_CurrentStatus.CurrentUserAddress))
                {
                    txtAddress.Text = HttpUtility.HtmlEncode(obj_CurrentStatus.CurrentUserAddress + obj_CurrentStatus.CurrentUserDistrict + obj_CurrentStatus.CurrentUserState);
                    //txtAddress.Text = HttpUtility.HtmlEncode(obj_CurrentStatus.CurrentUserAddress + obj_CurrentStatus.CurrentUserState + obj_CurrentStatus.CurrentUserDistrict + obj_CurrentStatus.CurrentUserSubDistrict);
                    //lblAddress.Text = obj_CurrentStatus.CurrentUserAddress;
                    //.Replace("&lt;/br&gt;","</br>");
                }
                else
                {
                    //lblAddress.Text = "";
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
            NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewVeriStageWorkFlowHistory objKeys_IndustrialNewVeriStageWorkFlowHistory = (NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewVeriStageWorkFlowHistory)e.Row.DataItem;
            // string ApplicationCode = grdVerificationIndustrialApplicationStatus.DataKeys[e.Row.RowIndex].Values[0].ToString();
            // string SerialNumber = grdVerificationIndustrialApplicationStatus.DataKeys[e.Row.RowIndex].Values[1].ToString();
            string ApplicationCode = objKeys_IndustrialNewVeriStageWorkFlowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_IndustrialNewVeriStageWorkFlowHistory.SerialNumber.ToString();
            NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewVeriStageWorkFlowHistory obj_IndustrialNewVeriStageWorkFlowHistory = new NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewVeriStageWorkFlowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
            try
            {
                Label lblForwardedUser = (Label)e.Row.FindControl("lblForwardedUser");
                Label lblToUser = (Label)e.Row.FindControl("lblToUser");
                Label lblFromUser = (Label)e.Row.FindControl("lblFromUser");
                Label lblActionInternalStatus = (Label)e.Row.FindControl("lblActionInternalStatus");
                Label lblFromUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblFromUserRegionOrDistrictOrHqName");
                Label lblToUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblToUserRegionOrDistrictOrHqName");
                Label lblForwardedUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblForwardedUserRegionOrDistrictOrHqName");

                if (obj_IndustrialNewVeriStageWorkFlowHistory.GetIndustrialNewApplication().NameOfIndustry != null && lblApplicationCode.Text != null)
                    lblIndustoryName.Text = HttpUtility.HtmlEncode(obj_IndustrialNewVeriStageWorkFlowHistory.GetIndustrialNewApplication().NameOfIndustry);

                if (obj_IndustrialNewVeriStageWorkFlowHistory.GetIndustrialNewApplication().IndustrialNewApplicationNumber != null && lblApplicationCode.Text != null)
                    lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_IndustrialNewVeriStageWorkFlowHistory.GetIndustrialNewApplication().IndustrialNewApplicationNumber);
                if (obj_IndustrialNewVeriStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialNewVeriStageWorkFlowHistory.FromUserCode));
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
                    //lblFromUser.Text = HttpUtility.HtmlEncode(obj_IndustrialNewVeriStageWorkFlowHistory.GetFromUser().UserName) + " (" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")"; 
                }
                if (obj_IndustrialNewVeriStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialNewVeriStageWorkFlowHistory.ToUserCode));
                    lblToUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                    //lblToUser.Text = HttpUtility.HtmlEncode(obj_IndustrialNewVeriStageWorkFlowHistory.GetToUser().UserName) + " (" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
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
                if (obj_IndustrialNewVeriStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialNewVeriStageWorkFlowHistory.ForwardedUserCode));
                    lblForwardedUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                    //lblForwardedUser.Text = HttpUtility.HtmlEncode(obj_IndustrialNewVeriStageWorkFlowHistory.GetForwardedUser().UserName) +" (" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")"; 
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

                if (obj_IndustrialNewVeriStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_IndustrialNewVeriStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                obj_IndustrialNewVeriStageWorkFlowHistory = null;
            }
        }
    }
    protected void grdAppProcessingIndustrialApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewAppProcessStageWorkFlowHistory objKeys_IndustrialNewAppProcessStageWorkFlowHistory = (NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewAppProcessStageWorkFlowHistory)e.Row.DataItem;
            //string ApplicationCode = grdAppProcessingIndustrialApplicationStatus.DataKeys[e.Row.RowIndex].Values[0].ToString();
            //string SerialNumber = grdAppProcessingIndustrialApplicationStatus.DataKeys[e.Row.RowIndex].Values[1].ToString();
            string ApplicationCode = objKeys_IndustrialNewAppProcessStageWorkFlowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_IndustrialNewAppProcessStageWorkFlowHistory.SerialNumber.ToString();
            NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewAppProcessStageWorkFlowHistory obj_IndustrialNewAppStageWorkFlowHistory = new NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewAppProcessStageWorkFlowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
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


                if (obj_IndustrialNewAppStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialNewAppStageWorkFlowHistory.FromUserCode));
                    lblFromUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                    //lblFromUser.Text = HttpUtility.HtmlEncode(obj_IndustrialNewAppStageWorkFlowHistory.GetFromUser().UserName) + "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")"; 
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
                if (obj_IndustrialNewAppStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialNewAppStageWorkFlowHistory.ToUserCode));
                    lblToUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                    //lblToUser.Text = HttpUtility.HtmlEncode(obj_IndustrialNewAppStageWorkFlowHistory.GetToUser().UserName) + "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")"; 
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
                if (obj_IndustrialNewAppStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {


                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialNewAppStageWorkFlowHistory.ForwardedUserCode));
                    lblForwardedUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                    //lblForwardedUser.Text = HttpUtility.HtmlEncode(obj_IndustrialNewAppStageWorkFlowHistory.GetForwardedUser().UserName) + "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")"; 
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

                if (obj_IndustrialNewAppStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_IndustrialNewAppStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);

                //Start Presentation
                if (obj_IndustrialNewAppStageWorkFlowHistory.PresentationCalledSerialNumber != null && lnkbtPresentation != null)
                {
                    NOCAP.BLL.Misc.Presentation.PresentationCalled obj_PresentationCalled = new NOCAP.BLL.Misc.Presentation.PresentationCalled(Convert.ToInt64(ApplicationCode), obj_IndustrialNewAppStageWorkFlowHistory.PresentationCalledSerialNumber);
                    if (obj_PresentationCalled.ApplicationCode != 0)
                    {
                        if (obj_PresentationCalled.PresentCalled == NOCAP.BLL.Common.CommonEnum.PresentationCalledOption.Yes)
                        {
                            lnkbtPresentation.Visible = true;

                            NOCAP.BLL.Misc.Presentation.PresentationDetail Obj_PresentationDetail = new NOCAP.BLL.Misc.Presentation.PresentationDetail();
                            NOCAP.BLL.Misc.Presentation.PresentationDetail[] Obj_PresentationDetailList;
                            Obj_PresentationDetailList = Obj_PresentationDetail.GetAllPresentationDetailForApplicationCode(Convert.ToInt64(ApplicationCode), Convert.ToInt32(obj_IndustrialNewAppStageWorkFlowHistory.PresentationCalledSerialNumber), NOCAP.BLL.Misc.Presentation.PresentationDetail.SortingField.PresentationSerialNumber);
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
                                lblPresentPreviousDt.Text = HttpUtility.HtmlEncode(obj_PresentPrevDt.ToString());
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
                obj_IndustrialNewAppStageWorkFlowHistory = null;
            }
        }
    }
    protected void grdNOCProcessingIndustrialApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewNOCProcessStageWorkFlowHistory objKeys_IndustrialNewNOCProcessStageWorkFlowHistory = (NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewNOCProcessStageWorkFlowHistory)e.Row.DataItem;
            string ApplicationCode = objKeys_IndustrialNewNOCProcessStageWorkFlowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_IndustrialNewNOCProcessStageWorkFlowHistory.SerialNumber.ToString();
            // string ApplicationCode = grdNOCProcessingIndustrialApplicationStatus.DataKeys[e.Row.RowIndex].Values[0].ToString();
            // string SerialNumber = grdNOCProcessingIndustrialApplicationStatus.DataKeys[e.Row.RowIndex].Values[1].ToString();
            NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewNOCProcessStageWorkFlowHistory obj_IndustrialNewNOCStageWorkFlowHistory = new NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewNOCProcessStageWorkFlowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
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


                if (obj_IndustrialNewNOCStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialNewNOCStageWorkFlowHistory.FromUserCode));
                    lblFromUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                    //lblFromUser.Text = HttpUtility.HtmlEncode(obj_IndustrialNewNOCStageWorkFlowHistory.GetFromUser().UserName) +"(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")"; 
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
                if (obj_IndustrialNewNOCStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialNewNOCStageWorkFlowHistory.ToUserCode));
                    lblToUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                    //  lblToUser.Text = HttpUtility.HtmlEncode(obj_IndustrialNewNOCStageWorkFlowHistory.GetToUser().UserName) + "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")"; 
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
                if (obj_IndustrialNewNOCStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialNewNOCStageWorkFlowHistory.ForwardedUserCode));
                    lblForwardedUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                    //lblForwardedUser.Text = HttpUtility.HtmlEncode(obj_IndustrialNewNOCStageWorkFlowHistory.GetForwardedUser().UserName)+ "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")"; 
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
                if (obj_IndustrialNewNOCStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_IndustrialNewNOCStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                obj_IndustrialNewNOCStageWorkFlowHistory = null;
            }
        }
    }
    protected void grdDisbursmentProcessingIndustrialApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewAppDisbuStageWorkflowHistory objKeys_IndustrialNewAppDisbuStageWorkflowHistory = (NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewAppDisbuStageWorkflowHistory)e.Row.DataItem;
            string ApplicationCode = objKeys_IndustrialNewAppDisbuStageWorkflowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_IndustrialNewAppDisbuStageWorkflowHistory.SerialNumber.ToString();
            //string ApplicationCode = grdDisbursmentProcessingIndustrialApplicationStatus.DataKeys[e.Row.RowIndex].Values[0].ToString();
            //string SerialNumber = grdDisbursmentProcessingIndustrialApplicationStatus.DataKeys[e.Row.RowIndex].Values[1].ToString();
            NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewAppDisbuStageWorkflowHistory obj_IndustrialNewDisbursmentStageWorkFlowHistory = new NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewAppDisbuStageWorkflowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
            try
            {
                Label lblForwardedUser = (Label)e.Row.FindControl("lblForwardedUser");
                Label lblToUser = (Label)e.Row.FindControl("lblToUser");
                Label lblFromUser = (Label)e.Row.FindControl("lblFromUser");
                Label lblActionInternalStatus = (Label)e.Row.FindControl("lblActionInternalStatus");


                Label lblFromUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblFromUserRegionOrDistrictOrHqName");
                Label lblToUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblToUserRegionOrDistrictOrHqName");
                Label lblForwardedUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblForwardedUserRegionOrDistrictOrHqName");


                if (obj_IndustrialNewDisbursmentStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialNewDisbursmentStageWorkFlowHistory.FromUserCode));
                    lblFromUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                    //lblFromUser.Text = HttpUtility.HtmlEncode(obj_IndustrialNewDisbursmentStageWorkFlowHistory.GetFromUser().UserName) + "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")"; 
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

                if (obj_IndustrialNewDisbursmentStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialNewDisbursmentStageWorkFlowHistory.ToUserCode));
                    lblToUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                    //lblToUser.Text = HttpUtility.HtmlEncode(obj_IndustrialNewDisbursmentStageWorkFlowHistory.GetToUser().UserName) + "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")"; 
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
                if (obj_IndustrialNewDisbursmentStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {

                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_IndustrialNewDisbursmentStageWorkFlowHistory.ForwardedUserCode));
                    lblForwardedUser.Text = "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")";
                    //lblForwardedUser.Text = HttpUtility.HtmlEncode(obj_IndustrialNewDisbursmentStageWorkFlowHistory.GetForwardedUser().UserName) + "(" + HttpUtility.HtmlEncode(obj_Login.GetNOCAPRole().GetUserType().UserTypeDesc.ToString()) + ")"; 
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
                if (obj_IndustrialNewDisbursmentStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_IndustrialNewDisbursmentStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                obj_IndustrialNewDisbursmentStageWorkFlowHistory = null;
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


                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialAppVerificationStatus = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewVeriStageWorkFlowHistory[] arr1;
                arr1 = obj_IndustrialAppVerificationStatus.GetIndustrialNewVeriStageWorkFlowHistoryListForReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.VeriStageWorkFlowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.VeriStageWorkFlowHistory.SortingField.NoSorting);
                GridView grdVerificationIndustrialApplicationStatus = (GridView)e.Row.FindControl("grdVerificationIndustrialApplicationStatus");
                if (arr1 != null && arr1.Length > 0)
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(arr1[0].ApplicationCode.ToString());
                    lblSerialNo.Text = HttpUtility.HtmlEncode(arr1[0].SerialNumber.ToString());
                }
                grdVerificationIndustrialApplicationStatus.DataSource = arr1;
                grdVerificationIndustrialApplicationStatus.DataBind();
                //}
                //else
                //{
                //    grdVerificationIndustrialApplicationStatus.DataSource = null;
                //    grdVerificationIndustrialApplicationStatus.DataBind();
                //}
                //Applicaiton Processing Status
                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialAppProcessingStatus = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewAppProcessStageWorkFlowHistory[] arr2;
                arr2 = obj_IndustrialAppProcessingStatus.GetIndustrialNewAppProcessStageWorkFlowHistoryListReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.AppProcessStageWorkFlowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.AppProcessStageWorkFlowHistory.SortingField.NoSorting);
                GridView grdAppProcessingIndustrialApplicationStatus = (GridView)e.Row.FindControl("grdAppProcessingIndustrialApplicationStatus");
                //if (arr2 != null && arr2.Length > 0)
                //{
                grdAppProcessingIndustrialApplicationStatus.DataSource = arr2;
                grdAppProcessingIndustrialApplicationStatus.DataBind();
                //}
                //else
                //{
                //    grdAppProcessingIndustrialApplicationStatus.DataSource = null;
                //    grdAppProcessingIndustrialApplicationStatus.DataBind();
                //}

                //NOC Processing Status
                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNOCProcessingStatus = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewNOCProcessStageWorkFlowHistory[] arr3;
                arr3 = obj_IndustrialNOCProcessingStatus.GetIndustrialNewNOCProcessStageWorkFlowHistoryListForReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.NOCProcessStageWorkFlowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.NOCProcessStageWorkFlowHistory.SortingField.NoSorting);
                GridView grdNOCProcessingIndustrialApplicationStatus = (GridView)e.Row.FindControl("grdNOCProcessingIndustrialApplicationStatus");
                //if (arr3 != null && arr3.Length > 0)
                //{
                grdNOCProcessingIndustrialApplicationStatus.DataSource = arr3;
                grdNOCProcessingIndustrialApplicationStatus.DataBind();
                //}
                //else
                //{
                //    grdNOCProcessingIndustrialApplicationStatus.DataSource = null;
                //    grdNOCProcessingIndustrialApplicationStatus.DataBind();
                //}





                //Disbursment Processing Status
                NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialDisbursmentProcessingStatus = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Industrial.New.WorkFlowHistory.IndustrialNewAppDisbuStageWorkflowHistory[] arr4;
                arr4 = obj_IndustrialDisbursmentProcessingStatus.GetIndustrialNewDisbursmentProcessStageWorkFlowHistoryListForReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.AppDisbuStageWorkflowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.AppDisbuStageWorkflowHistory.SortingField.NoSorting);
                GridView grdDisbursmentProcessingIndustrialApplicationStatus = (GridView)e.Row.FindControl("grdDisbursmentProcessingIndustrialApplicationStatus");
                //if (arr4 != null && arr4.Length > 0)
                //{
                grdDisbursmentProcessingIndustrialApplicationStatus.DataSource = arr4;
                grdDisbursmentProcessingIndustrialApplicationStatus.DataBind();
                //}
                //else
                //{
                //    grdDisbursmentProcessingIndustrialApplicationStatus.DataSource = null;
                //    grdDisbursmentProcessingIndustrialApplicationStatus.DataBind();
                //}



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

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        int int_status = 0;
        string strA_custumMessage;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int PenaltySN = Convert.ToInt32(gvShowPenalty.DataKeys[e.Row.RowIndex].Value);
            Label AppCodePenalty = (Label)e.Row.FindControl("lblAppCode") as Label;
            long AppCode = Convert.ToInt64(AppCodePenalty.Text);
            GridView gvShowPenaltyDetail = e.Row.FindControl("gvShowPenaltyDetail") as GridView;
            NOCAP.BLL.PenaltyImpose.PenaltyImposeDetail obj_PenaltyImposeDetail = new NOCAP.BLL.PenaltyImpose.PenaltyImposeDetail();
            NOCAP.BLL.PenaltyImpose.PenaltyImposeDetail[] arr_PenaltyImposeDetail;

            arr_PenaltyImposeDetail = obj_PenaltyImposeDetail.GetALLForKeys(out strA_custumMessage, out int_status, AppCode, PenaltySN);
            gvShowPenaltyDetail.DataSource = arr_PenaltyImposeDetail;
            gvShowPenaltyDetail.DataBind();

            GridView gvShowCorrDetail = e.Row.FindControl("gvShowCorrDetail") as GridView;
            NOCAP.BLL.PenaltyImpose.PenaltyCorrImposeDetail obj_PenaltyCorrImposeDetail = new NOCAP.BLL.PenaltyImpose.PenaltyCorrImposeDetail();

            NOCAP.BLL.PenaltyImpose.PenaltyCorrImposeDetail[] arr_PenaltyCorrImposeDetail;
            arr_PenaltyCorrImposeDetail = obj_PenaltyCorrImposeDetail.GetALLForKeys(out strA_custumMessage, out int_status, AppCode, PenaltySN);
            gvShowCorrDetail.DataSource = arr_PenaltyCorrImposeDetail;
            gvShowCorrDetail.DataBind();
        }
    }

    protected void gvShowCorrDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnCorrectionChargeCode = (HiddenField)e.Row.FindControl("hdnCorrectionChargeCode");
            Label lblCorrectionChargeDesc = (Label)e.Row.FindControl("lblCorrectionChargeDesc");
            NOCAP.BLL.Master.CorrectionCharge obj_CorrectionCharge = new NOCAP.BLL.Master.CorrectionCharge(Convert.ToInt32(hdnCorrectionChargeCode.Value));
            lblCorrectionChargeDesc.Text = obj_CorrectionCharge.CorrectionChargeDesc;
        }
    }
    protected void gvShowPenaltyDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnPenaltyCode = (HiddenField)e.Row.FindControl("hdnPenaltyCode");
            Label lblPenaltyDesc = (Label)e.Row.FindControl("lblPenaltyDesc");
            NOCAP.BLL.Master.Penalty obj_Penalty = new NOCAP.BLL.Master.Penalty(Convert.ToInt32(hdnPenaltyCode.Value));
            lblPenaltyDesc.Text = obj_Penalty.PenaltyDesc;
        }

    }

}