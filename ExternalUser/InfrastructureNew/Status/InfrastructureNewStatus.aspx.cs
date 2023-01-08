using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class ExternalUser_InfrastructureNew_Status_InfrastructureNewStatus : System.Web.UI.Page
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
                lblApplicationCode.Text = lblAppCode.Text;
                if (!NOCAPExternalUtility.IsNumeric(lblAppCode.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert(' Application Code allows only Numeric ');", true);
                    return;
                }

                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplicationForMainGrid = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(Convert.ToInt32(lblAppCode.Text));
                if (obj_InfrastructureNewApplicationForMainGrid != null)
                {

                    DataTable dt = new DataTable();
                    dt.Columns.Add("ApplicationCode");
                    dt.Columns.Add("ReferBackSN");
                    dt.Columns.Add("ReferBackHeader");
                    for (int i = obj_InfrastructureNewApplicationForMainGrid.ReferBackSN; i >= 0; i--)
                    {
                        DataRow dr = dt.NewRow();
                        dr["ApplicationCode"] = obj_InfrastructureNewApplicationForMainGrid.ApplicationCode;
                        dr["ReferBackSN"] = i;
                        int ReferBackCountToShow = i + 1;
                        string ReferBackToShow = AddOrdinal(ReferBackCountToShow);
                        dr["ReferBackHeader"] = obj_InfrastructureNewApplicationForMainGrid.ReferBackSN == i ? "Current Status" : "Application is Refered Back " + ReferBackToShow + " Time";
                        dt.Rows.Add(dr);
                    }

                    gvMainGrid.DataSource = dt;
                    gvMainGrid.DataBind();

                }


                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(Convert.ToInt32(lblAppCode.Text));
                NOCAP.BLL.Master.ApplicationStatus obj_ApplicationStatus = new NOCAP.BLL.Master.ApplicationStatus(obj_InfrastructureNewApplication.LatestApplicationStatusCode);

                    
                NOCAP.BLL.Common.CurrentStatus obj_CurrentStatus = new NOCAP.BLL.Common.CurrentStatus();
                obj_CurrentStatus = obj_InfrastructureNewApplication.GetCurrentStatus();
                lblCurrentStage.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_CurrentStatus.CurrentStage));

                



                // Processing Fee 
                if (obj_InfrastructureNewApplication.PayReq == NOCAP.BLL.Common.PaymentRequirementOption.PayReqOption.Yes)
                {

                    lblProcessingFee.Text =HttpUtility.HtmlEncode("Rs. " + obj_InfrastructureNewApplication.PayReqAmt + "/- (" + NOCAPExternalUtility.ParseInput(Convert.ToString(obj_InfrastructureNewApplication.PayReqAmt)) + ")");

                    //RowProcessFeeSubmitted.Visible = true;
                    switch (obj_InfrastructureNewApplication.PayAmtRecFinally)
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
                    lblProcessingFeeSubmitted.Text = "No Fee";
                    lblProcessingFee.Text = "";
                }


                if (obj_InfrastructureNewApplication.WaterQualityCodeFinally != null)
                {
                    lblWaterQualityTypeApproved.Text = Convert.ToString(new NOCAP.BLL.Master.WaterQuality((int)obj_InfrastructureNewApplication.WaterQualityCodeFinally).WaterQualityDesc);
                }



                switch (obj_InfrastructureNewApplication.WaterChargeReqFinally)
                {
                    case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.WaterChargeReqFinallyYesNo.Yes:
                        lblWaterChargeReqFinally.Text = "Yes";

                        break;
                    case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.WaterChargeReqFinallyYesNo.No:
                        lblWaterChargeReqFinally.Text = "No";

                        break;
                    case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.WaterChargeReqFinallyYesNo.NotDefine:
                        lblWaterChargeReqFinally.Text = "Not Define";

                        break;
                    default:
                        lblWaterChargeReqFinally.Text = "Not Define";

                        break;
                }

                switch (obj_InfrastructureNewApplication.WaterChargeReq)
                {
                    case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.WaterChargeReqYesNo.Yes:
                        lblAbsRestCharge.Text = "Rs. " + HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.GWChargeAmtFinally) + "/- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(obj_InfrastructureNewApplication.GWChargeAmtFinally))) + ")";
                        lblAbsRestArear.Text = "Rs. " + HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.GWArearAmtFinally) + "/- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(obj_InfrastructureNewApplication.GWArearAmtFinally))) + ")";

                        switch (obj_InfrastructureNewApplication.WaterChargeRecFinally)
                        {
                            case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.WaterChargeRecFinallyYesNo.Yes:
                                lblAbsRestChargeSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.Yes)) + ")";
                                lblAbsRestArearSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.Yes)) + ")";

                                break;
                            case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.WaterChargeRecFinallyYesNo.No:
                                lblAbsRestChargeSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";
                                lblAbsRestArearSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";

                                break;
                            case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.WaterChargeRecFinallyYesNo.NotDefine:
                                lblAbsRestChargeSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";
                                lblAbsRestArearSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";

                                break;
                        }
                        break;
                    case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.WaterChargeReqYesNo.No:
                        lblAbsRestChargeSubmitted.Text = "No Charge";
                        lblAbsRestCharge.Text = "";
                        lblAbsRestArearSubmitted.Text = "No Arear";
                        lblAbsRestArear.Text = "";
                        break;
                    case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.WaterChargeReqYesNo.NotDefine:
                        lblAbsRestChargeSubmitted.Text = "No Charge";
                        lblAbsRestCharge.Text = "";
                        lblAbsRestArearSubmitted.Text = "No Arear";
                        lblAbsRestArear.Text = "";
                        break;

                }

                //if (obj_IndustrialNewApplication.ECReqFinally == NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication.ECOption.Yes)
                //{
                //    chkBoxECReq.Checked = true;
                //    chkBoxECReq.Enabled = false;
                //}
                //else
                //{
                //    chkBoxECReq.Checked = false;
                //    chkBoxECReq.Enabled = false;
                //}
                switch (obj_InfrastructureNewApplication.ECRecFinally)
                {
                    case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.ECOption.Yes:
                        lblECReceived.Text = "Yes";
                        break;
                    case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.ECOption.No:
                        lblECReceived.Text = "No";
                        break;
                    case NOCAP.BLL.Infrastructure.New.Common.InfrastructureNewApplicationB.ECOption.NotDefined:
                        lblECReceived.Text = "";
                        break;

                }
                if (!String.IsNullOrEmpty(Convert.ToString(obj_InfrastructureNewApplication.ECGWillegalFrom)))
                {
                    lblGWWillegalFrom.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_InfrastructureNewApplication.ECGWillegalFrom).ToString("dd/MM/yyyy"));
                }
                if (!String.IsNullOrEmpty(Convert.ToString(obj_InfrastructureNewApplication.ECGWillegalTo)))
                {
                    lblGWWillegalTo.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_InfrastructureNewApplication.ECGWillegalTo).ToString("dd/MM/yyyy"));
                }
                lblillegalQty.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.ECillegalQty);
                lblECAmt.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.ECAmout);
                if (obj_InfrastructureNewApplication.ECReasonCode != null)
                {
                    lblReasonForEC.Text = HttpUtility.HtmlEncode(Convert.ToString(new NOCAP.BLL.Master.ECReason((int)obj_InfrastructureNewApplication.ECReasonCode).ECReasonDesc));
                }
                lblOtherReason.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.ECReasonOther);

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

                if (obj_InfrastructureNewApplication.FileClose == NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication.FileCloseOption.Yes)
                {
                    RowCurrentStatus.Visible = false;
                    RowCurrentStage.Visible = false;
                    RowAddress.Visible = false;
                    RowFinalStatus.Visible = true;
                }
                else if (obj_InfrastructureNewApplication.FileClose == NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication.FileCloseOption.No)
                {
                    RowCurrentStatus.Visible = true;
                    RowCurrentStage.Visible = true;
                    RowAddress.Visible = true;
                    RowFinalStatus.Visible = false;
                }



                    //Commented for new code added below
                //if (string.IsNullOrEmpty(obj_CurrentStatus.InternalStatus))
                //{
                //    obj_CurrentStatus.InternalStatus = "In Progress";
                //    lblCurrentStatus.Text =HttpUtility.HtmlEncode( obj_CurrentStatus.InternalStatus);
                //}
                //else
                //{
                //    lblCurrentStatus.Text =HttpUtility.HtmlEncode( Convert.ToString(obj_CurrentStatus.InternalStatus));
                //}




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
                if (obj_InfrastructureNewApplication.PresentationCalledSerialNumber != null)
                {
                    NOCAP.BLL.Misc.Presentation.PresentationCalled obj_PresentationCalled = new NOCAP.BLL.Misc.Presentation.PresentationCalled(Convert.ToInt64(lblAppCode.Text), obj_InfrastructureNewApplication.PresentationCalledSerialNumber);
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

                    lblReceiveDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_CurrentStatus.ReceiveDate).ToString("dd/MM/yyyy"));
                }

                    // Comment for status
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
                }
                else
                {
                    txtAddress.Text = "";
                }
                //if (!string.IsNullOrEmpty(obj_CurrentStatus.CurrentUserState))
                //{
                //    lblState.Text = "-" + obj_CurrentStatus.CurrentUserState;
                //}
                //else
                //{
                //    lblState.Text = "";
                //}
                //if (!string.IsNullOrEmpty(obj_CurrentStatus.CurrentUserDistrict))
                //{
                //    lblDistrict.Text = obj_CurrentStatus.CurrentUserDistrict;
                //}
                //else
                //{
                //    lblDistrict.Text = "";
                //}

                //if (!string.IsNullOrEmpty(obj_CurrentStatus.CurrentUserSubDistrict))
                //{
                //    lblSubDistrict.Text = obj_CurrentStatus.CurrentUserSubDistrict;
                //}
                //else
                //{
                //    lblSubDistrict.Text = "";
                //}

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
            NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewVeriStageWorkFlowHistory objKeys_InfrastructureNewVeriStageWorkFlowHistory = (NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewVeriStageWorkFlowHistory)e.Row.DataItem;
            string ApplicationCode = objKeys_InfrastructureNewVeriStageWorkFlowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_InfrastructureNewVeriStageWorkFlowHistory.SerialNumber.ToString();
            //string ApplicationCode = grdVerificationInfrastructureApplicationStatus.DataKeys[e.Row.RowIndex].Values[0].ToString();
            //string SerialNumber = grdVerificationInfrastructureApplicationStatus.DataKeys[e.Row.RowIndex].Values[1].ToString();
            NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewVeriStageWorkFlowHistory obj_InfrastructureNewVeriStageWorkFlowHistory = new NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewVeriStageWorkFlowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
            try
            {
                Label lblForwardedUser = (Label)e.Row.FindControl("lblForwardedUser");
                Label lblToUser = (Label)e.Row.FindControl("lblToUser");
                Label lblFromUser = (Label)e.Row.FindControl("lblFromUser");
                Label lblActionInternalStatus = (Label)e.Row.FindControl("lblActionInternalStatus");

                //---Added by Chirag on 29-Apr-2016 to get Office Name--------------------------------------------------------
                Label lblFromUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblFromUserRegionOrDistrictOrHqName");
                Label lblToUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblToUserRegionOrDistrictOrHqName");
                Label lblForwardedUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblForwardedUserRegionOrDistrictOrHqName");
                //----------------------------------------------------------------------------------------------------------------------
                if (obj_InfrastructureNewVeriStageWorkFlowHistory.GetInfrastructureNewApplication().NameOfInfrastructure != null && lblApplicationCode.Text != null)
                    lblInfrastructureName.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewVeriStageWorkFlowHistory.GetInfrastructureNewApplication().NameOfInfrastructure);

                if (obj_InfrastructureNewVeriStageWorkFlowHistory.GetInfrastructureNewApplication().InfrastructureNewApplicationNumber != null && lblApplicationCode.Text != null)
                    lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewVeriStageWorkFlowHistory.GetInfrastructureNewApplication().InfrastructureNewApplicationNumber);

                if (obj_InfrastructureNewVeriStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureNewVeriStageWorkFlowHistory.FromUserCode));
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
                if (obj_InfrastructureNewVeriStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureNewVeriStageWorkFlowHistory.ToUserCode));
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

                if (obj_InfrastructureNewVeriStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureNewVeriStageWorkFlowHistory.ForwardedUserCode));
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

                if (obj_InfrastructureNewVeriStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text =HttpUtility.HtmlEncode( obj_InfrastructureNewVeriStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                obj_InfrastructureNewVeriStageWorkFlowHistory = null;
            }
        }
    }
    protected void grdAppProcessingInfrastructureApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewAppProcessStageWorkFlowHistory objKeys_InfrastructureNewAppProcessStageWorkFlowHistory = (NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewAppProcessStageWorkFlowHistory)e.Row.DataItem;
            string ApplicationCode = objKeys_InfrastructureNewAppProcessStageWorkFlowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_InfrastructureNewAppProcessStageWorkFlowHistory.SerialNumber.ToString();
            //string ApplicationCode = grdAppProcessingInfrastructureApplicationStatus.DataKeys[e.Row.RowIndex].Values[0].ToString();
            //string SerialNumber = grdAppProcessingInfrastructureApplicationStatus.DataKeys[e.Row.RowIndex].Values[1].ToString();
            NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewAppProcessStageWorkFlowHistory obj_InfrastructureNewAppStageWorkFlowHistory = new NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewAppProcessStageWorkFlowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
            try
            {
                Label lblForwardedUser = (Label)e.Row.FindControl("lblForwardedUser");
                Label lblToUser = (Label)e.Row.FindControl("lblToUser");
                Label lblFromUser = (Label)e.Row.FindControl("lblFromUser");
                Label lblActionInternalStatus = (Label)e.Row.FindControl("lblActionInternalStatus");
                LinkButton lnkbtPresentation = (LinkButton)e.Row.FindControl("lnkbtPresentation");
                Label lblPresentPreviousDt = (Label)e.Row.FindControl("lblPresentPreviousDt");

                //---Added by Chirag on 29-Apr-2016 to get Office Name--------------------------------------------------------
                Label lblFromUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblFromUserRegionOrDistrictOrHqName");
                Label lblToUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblToUserRegionOrDistrictOrHqName");
                Label lblForwardedUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblForwardedUserRegionOrDistrictOrHqName");
                //----------------------------------------------------------------------------------------------------------------------

                if (obj_InfrastructureNewAppStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {//---Added by Chirag on 29-Apr-2016 to get Office Name and user tye description--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureNewAppStageWorkFlowHistory.FromUserCode));
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
                    //lblFromUser.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewAppStageWorkFlowHistory.GetFromUser().UserName);
                }
                if (obj_InfrastructureNewAppStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {//---Added by Chirag on 29-Apr-2016 to get Office Name and user tye description--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureNewAppStageWorkFlowHistory.ToUserCode));
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
                    //lblToUser.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewAppStageWorkFlowHistory.GetToUser().UserName);
                }
                if (obj_InfrastructureNewAppStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {//---Added by Chirag on 29-Apr-2016 to get Office Name and user tye description--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureNewAppStageWorkFlowHistory.ForwardedUserCode));
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
                    //lblForwardedUser.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewAppStageWorkFlowHistory.GetForwardedUser().UserName);
                }
                if (obj_InfrastructureNewAppStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                {
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewAppStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);
                }

                //Start Presentation
                if (obj_InfrastructureNewAppStageWorkFlowHistory.PresentationCalledSerialNumber != null && lnkbtPresentation != null)
                {
                    NOCAP.BLL.Misc.Presentation.PresentationCalled obj_PresentationCalled = new NOCAP.BLL.Misc.Presentation.PresentationCalled(Convert.ToInt64(ApplicationCode), obj_InfrastructureNewAppStageWorkFlowHistory.PresentationCalledSerialNumber);
                    if (obj_PresentationCalled.ApplicationCode != 0)
                    {
                        if (obj_PresentationCalled.PresentCalled == NOCAP.BLL.Common.CommonEnum.PresentationCalledOption.Yes)
                        {
                            lnkbtPresentation.Visible = true;

                            NOCAP.BLL.Misc.Presentation.PresentationDetail Obj_PresentationDetail = new NOCAP.BLL.Misc.Presentation.PresentationDetail();
                            NOCAP.BLL.Misc.Presentation.PresentationDetail[] Obj_PresentationDetailList;
                            Obj_PresentationDetailList = Obj_PresentationDetail.GetAllPresentationDetailForApplicationCode(Convert.ToInt64(ApplicationCode), Convert.ToInt32(obj_InfrastructureNewAppStageWorkFlowHistory.PresentationCalledSerialNumber), NOCAP.BLL.Misc.Presentation.PresentationDetail.SortingField.PresentationSerialNumber);
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
                obj_InfrastructureNewAppStageWorkFlowHistory = null;
            }
        }
    }

    protected void grdNOCProcessingInfrastructureApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewNOCProcessStageWorkFlowHistory objKeys_InfrastructureNewNOCProcessStageWorkFlowHistory = (NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewNOCProcessStageWorkFlowHistory)e.Row.DataItem;
            string ApplicationCode = objKeys_InfrastructureNewNOCProcessStageWorkFlowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_InfrastructureNewNOCProcessStageWorkFlowHistory.SerialNumber.ToString();
            //string ApplicationCode = grdNOCProcessingInfrastructureApplicationStatus.DataKeys[e.Row.RowIndex].Values[0].ToString();
            //string SerialNumber = grdNOCProcessingInfrastructureApplicationStatus.DataKeys[e.Row.RowIndex].Values[1].ToString();
            NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewNOCProcessStageWorkFlowHistory obj_InfrastructureNewNOCStageWorkFlowHistory = new NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewNOCProcessStageWorkFlowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
            try
            {
                Label lblForwardedUser = (Label)e.Row.FindControl("lblForwardedUser");
                Label lblToUser = (Label)e.Row.FindControl("lblToUser");
                Label lblFromUser = (Label)e.Row.FindControl("lblFromUser");
                Label lblActionInternalStatus = (Label)e.Row.FindControl("lblActionInternalStatus");
                //---Added by Chirag on 29-Apr-2016 to get Office Name--------------------------------------------------------
                Label lblFromUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblFromUserRegionOrDistrictOrHqName");
                Label lblToUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblToUserRegionOrDistrictOrHqName");
                Label lblForwardedUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblForwardedUserRegionOrDistrictOrHqName");
                //----------------------------------------------------------------------------------------------------------------------
                if (obj_InfrastructureNewNOCStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {//---Added by Chirag on 29-Apr-2016 to get Office Name and user tye description--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureNewNOCStageWorkFlowHistory.FromUserCode));
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
                    //lblFromUser.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewNOCStageWorkFlowHistory.GetFromUser().UserName);
                }
                if (obj_InfrastructureNewNOCStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {//---Added by Chirag on 29-Apr-2016 to get Office Name and user tye description--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureNewNOCStageWorkFlowHistory.ToUserCode));
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
                    //lblToUser.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewNOCStageWorkFlowHistory.GetToUser().UserName);
                }
                if (obj_InfrastructureNewNOCStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {//---Added by Chirag on 29-Apr-2016 to get Office Name and user tye description--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureNewNOCStageWorkFlowHistory.ForwardedUserCode));
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
                    // lblForwardedUser.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewNOCStageWorkFlowHistory.GetForwardedUser().UserName);
                }
                if (obj_InfrastructureNewNOCStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                {
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewNOCStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);
                }

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                obj_InfrastructureNewNOCStageWorkFlowHistory = null;
            }
        }
    }


    protected void grdDisbursmentProcessingInfrastructureApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewAppDisbuStageWorkflowHistory objKeys_InfrastructureNewAppDisbuStageWorkflowHistory = (NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewAppDisbuStageWorkflowHistory)e.Row.DataItem;
            string ApplicationCode = objKeys_InfrastructureNewAppDisbuStageWorkflowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_InfrastructureNewAppDisbuStageWorkflowHistory.SerialNumber.ToString();
            //string ApplicationCode = grdDisbursmentProcessingInfrastructureApplicationStatus.DataKeys[e.Row.RowIndex].Values[0].ToString();
            //string SerialNumber = grdDisbursmentProcessingInfrastructureApplicationStatus.DataKeys[e.Row.RowIndex].Values[1].ToString();
            NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewAppDisbuStageWorkflowHistory obj_InfrastructureNewDisbursmentStageWorkFlowHistory = new NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewAppDisbuStageWorkflowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
            try
            {
                Label lblForwardedUser = (Label)e.Row.FindControl("lblForwardedUser");
                Label lblToUser = (Label)e.Row.FindControl("lblToUser");
                Label lblFromUser = (Label)e.Row.FindControl("lblFromUser");
                Label lblActionInternalStatus = (Label)e.Row.FindControl("lblActionInternalStatus");
                //---Added by Chirag on 29-Apr-2016 to get Office Name--------------------------------------------------------
                Label lblFromUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblFromUserRegionOrDistrictOrHqName");
                Label lblToUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblToUserRegionOrDistrictOrHqName");
                Label lblForwardedUserRegionOrDistrictOrHqName = (Label)e.Row.FindControl("lblForwardedUserRegionOrDistrictOrHqName");
                //----------------------------------------------------------------------------------------------------------------------
                if (obj_InfrastructureNewDisbursmentStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {//---Added by Chirag on 29-Apr-2016 to get Office Name and user tye description--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureNewDisbursmentStageWorkFlowHistory.FromUserCode));
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
                    //lblFromUser.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewDisbursmentStageWorkFlowHistory.GetFromUser().UserName);
                }
                if (obj_InfrastructureNewDisbursmentStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {//---Added by Chirag on 29-Apr-2016 to get Office Name and user tye description--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureNewDisbursmentStageWorkFlowHistory.ToUserCode));
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
                    //lblToUser.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewDisbursmentStageWorkFlowHistory.GetToUser().UserName);
                }
                if (obj_InfrastructureNewDisbursmentStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {//---Added by Chirag on 29-Apr-2016 to get Office Name and user tye description--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_InfrastructureNewDisbursmentStageWorkFlowHistory.ForwardedUserCode));
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
                    //lblForwardedUser.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewDisbursmentStageWorkFlowHistory.GetForwardedUser().UserName);
                }
                if (obj_InfrastructureNewDisbursmentStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewDisbursmentStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                obj_InfrastructureNewDisbursmentStageWorkFlowHistory = null;
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


                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureAppVerificationStatus = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewVeriStageWorkFlowHistory[] arr1;
                arr1 = obj_InfrastructureAppVerificationStatus.GetInfrastructureNewVeriStageWorkFlowHistoryListForReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.VeriStageWorkFlowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.VeriStageWorkFlowHistory.SortingField.NoSorting);
                GridView grdVerificationInfrastructureApplicationStatus = (GridView)e.Row.FindControl("grdVerificationInfrastructureApplicationStatus");
                if (arr1 != null && arr1.Length > 0)
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(arr1[0].ApplicationCode.ToString());
                    lblSerialNo.Text = HttpUtility.HtmlEncode(arr1[0].SerialNumber.ToString());
                }
                grdVerificationInfrastructureApplicationStatus.DataSource = arr1;
                grdVerificationInfrastructureApplicationStatus.DataBind();
                //}
                //else
                //{
                //    grdVerificationInfrastructureApplicationStatus.DataSource = null;
                //    grdVerificationInfrastructureApplicationStatus.DataBind();
                //}




                //Applicaiton Processing Status
                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureAppProcessingStatus = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewAppProcessStageWorkFlowHistory[] arr2;
                arr2 = obj_InfrastructureAppProcessingStatus.GetInfrastructureNewAppProcessStageWorkFlowHistoryListReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.AppProcessStageWorkFlowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.AppProcessStageWorkFlowHistory.SortingField.NoSorting);
                GridView grdAppProcessingInfrastructureApplicationStatus = (GridView)e.Row.FindControl("grdAppProcessingInfrastructureApplicationStatus");
                //if (arr2 != null && arr2.Length > 0)
                //{
                grdAppProcessingInfrastructureApplicationStatus.DataSource = arr2;
                grdAppProcessingInfrastructureApplicationStatus.DataBind();
                //}
                //else
                //{
                //    grdAppProcessingInfrastructureApplicationStatus.DataSource = null;
                //    grdAppProcessingInfrastructureApplicationStatus.DataBind();
                //}

                //NOC Processing Status
                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNOCProcessingStatus = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewNOCProcessStageWorkFlowHistory[] arr3;
                arr3 = obj_InfrastructureNOCProcessingStatus.GetInfrastructureNewNOCProcessStageWorkFlowHistoryListForReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.NOCProcessStageWorkFlowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.NOCProcessStageWorkFlowHistory.SortingField.NoSorting);
                GridView grdNOCProcessingInfrastructureApplicationStatus = (GridView)e.Row.FindControl("grdNOCProcessingInfrastructureApplicationStatus");
                //if (arr3 != null && arr3.Length > 0)
                //{
                grdNOCProcessingInfrastructureApplicationStatus.DataSource = arr3;
                grdNOCProcessingInfrastructureApplicationStatus.DataBind();
                //}
                //else
                //{
                //    grdNOCProcessingInfrastructureApplicationStatus.DataSource = null;
                //    grdNOCProcessingInfrastructureApplicationStatus.DataBind();
                //}





                //Disbursment Processing Status
                NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureDisbursmentProcessingStatus = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Infrastructure.New.WorkFlowHistory.InfrastructureNewAppDisbuStageWorkflowHistory[] arr4;
                arr4 = obj_InfrastructureDisbursmentProcessingStatus.GetInfrastructureNewDisbursmentProcessStageWorkFlowHistoryListForReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.AppDisbuStageWorkflowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.AppDisbuStageWorkflowHistory.SortingField.NoSorting);
                GridView grdDisbursmentProcessingInfrastructureApplicationStatus = (GridView)e.Row.FindControl("grdDisbursmentProcessingInfrastructureApplicationStatus");
                //if (arr4 != null && arr4.Length > 0)
                //{
                grdDisbursmentProcessingInfrastructureApplicationStatus.DataSource = arr4;
                grdDisbursmentProcessingInfrastructureApplicationStatus.DataBind();
                //}
                //else
                //{
                //    grdDisbursmentProcessingInfrastructureApplicationStatus.DataSource = null;
                //    grdDisbursmentProcessingInfrastructureApplicationStatus.DataBind();
                //}



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