using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class ExternalUser_MiningNew_Status_MiningNewStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
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

                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplicationForMainGrid = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(Convert.ToInt32(lblAppCode.Text));
                if (obj_MiningNewApplicationForMainGrid != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ApplicationCode");
                    dt.Columns.Add("ReferBackSN");
                    dt.Columns.Add("ReferBackHeader");
                    for (int i = obj_MiningNewApplicationForMainGrid.ReferBackSN; i >= 0; i--)
                    {
                        DataRow dr = dt.NewRow();
                        dr["ApplicationCode"] = obj_MiningNewApplicationForMainGrid.ApplicationCode;
                        dr["ReferBackSN"] = i;
                        int ReferBackCountToShow = i + 1;
                        string ReferBackToShow = AddOrdinal(ReferBackCountToShow);
                        dr["ReferBackHeader"] = obj_MiningNewApplicationForMainGrid.ReferBackSN == i ? "Current Status" : "Application is Refered Back " + ReferBackToShow + " Time";
                        dt.Rows.Add(dr);
                    }
                    gvMainGrid.DataSource = dt;
                    gvMainGrid.DataBind();


                     

                }



                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(Convert.ToInt32(lblAppCode.Text));
                NOCAP.BLL.Master.ApplicationStatus obj_ApplicationStatus = new NOCAP.BLL.Master.ApplicationStatus(obj_MiningNewApplication.LatestApplicationStatusCode);


                NOCAP.BLL.Common.CurrentStatus obj_CurrentStatus = new NOCAP.BLL.Common.CurrentStatus();
                obj_CurrentStatus = obj_MiningNewApplication.GetCurrentStatus();

                lblCurrentStage.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_CurrentStatus.CurrentStage));
                 //commented for new code added below         
                //if (string.IsNullOrEmpty(obj_CurrentStatus.InternalStatus))
                //{
                //    obj_CurrentStatus.InternalStatus = "In Progress";
                //    lblCurrentStatus.Text = HttpUtility.HtmlEncode(obj_CurrentStatus.InternalStatus);
                //}
                //else
                //{
                //    lblCurrentStatus.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_CurrentStatus.InternalStatus));
                //}

                // Processing Fee 
                if (obj_MiningNewApplication.PayReq == NOCAP.BLL.Common.PaymentRequirementOption.PayReqOption.Yes)
                {

                    lblProcessingFee.Text =HttpUtility.HtmlEncode("Rs. " + obj_MiningNewApplication.PayReqAmt + "/- (" + NOCAPExternalUtility.ParseInput(Convert.ToString(obj_MiningNewApplication.PayReqAmt)) + ")");

                    //RowProcessFeeSubmitted.Visible = true;
                    switch (obj_MiningNewApplication.PayAmtRecFinally)
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


                if (obj_MiningNewApplication.WaterQualityCodeFinally != null)
                {
                    lblWaterQualityTypeApproved.Text = Convert.ToString(new NOCAP.BLL.Master.WaterQuality((int)obj_MiningNewApplication.WaterQualityCodeFinally).WaterQualityDesc);
                }


                switch (obj_MiningNewApplication.WaterChargeReqFinally)
                {
                    case NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WaterChargeReqFinallyYesNo.Yes:
                        lblWaterChargeReqFinally.Text = "Yes";

                        break;
                    case NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WaterChargeReqFinallyYesNo.No:
                        lblWaterChargeReqFinally.Text = "No";

                        break;
                    case NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WaterChargeReqFinallyYesNo.NotDefine:
                        lblWaterChargeReqFinally.Text = "Not Define";

                        break;
                    default:
                        lblWaterChargeReqFinally.Text = "Not Define";

                        break;
                }

                switch (obj_MiningNewApplication.WaterChargeReq)
                {
                    case NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WaterChargeReqYesNo.Yes:
                        lblAbsRestCharge.Text = "Rs. " + HttpUtility.HtmlEncode(obj_MiningNewApplication.GWChargeAmtFinally) + "/- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(obj_MiningNewApplication.GWChargeAmtFinally))) + ")";
                        lblAbsRestArear.Text = "Rs. " + HttpUtility.HtmlEncode(obj_MiningNewApplication.GWArearAmtFinally) + "/- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(obj_MiningNewApplication.GWArearAmtFinally))) + ")";

                        switch (obj_MiningNewApplication.WaterChargeRecFinally)
                        {
                            case NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WaterChargeRecFinallyYesNo.Yes:
                                lblAbsRestChargeSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.Yes)) + ")";
                                lblAbsRestArearSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.Yes)) + ")";

                                break;
                            case NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WaterChargeRecFinallyYesNo.No:
                                lblAbsRestChargeSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";
                                lblAbsRestArearSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";

                                break;
                            case NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WaterChargeRecFinallyYesNo.NotDefine:
                                lblAbsRestChargeSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";
                                lblAbsRestArearSubmitted.Text = "(<b>Submitted:</b> " + HttpUtility.HtmlEncode(Convert.ToString(NOCAP.BLL.Common.PaymentAmountReceiveFinallyOption.PayAmtRecFinallyOption.No)) + ")";

                                break;
                        }
                        break;
                    case NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WaterChargeReqYesNo.No:
                        lblAbsRestChargeSubmitted.Text = "No Charge";
                        lblAbsRestCharge.Text = "";
                        lblAbsRestArearSubmitted.Text = "No Arear";
                        lblAbsRestArear.Text = "";
                        break;
                    case NOCAP.BLL.Mining.New.Common.MiningNewApplicationB.WaterChargeReqYesNo.NotDefine:
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

                if (obj_MiningNewApplication.FileClose == NOCAP.BLL.Mining.New.Application.MiningNewApplication.FileCloseOption.Yes)
                {
                    RowCurrentStatus.Visible = false;
                    RowCurrentStage.Visible = false;
                    RowAddress.Visible = false;
                    RowFinalStatus.Visible = true;
                }
                else if (obj_MiningNewApplication.FileClose == NOCAP.BLL.Mining.New.Application.MiningNewApplication.FileCloseOption.No)
                {
                    RowCurrentStatus.Visible = true;
                    RowCurrentStage.Visible = true;
                    RowAddress.Visible = true;
                    RowFinalStatus.Visible = false;
                }


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
                if (obj_MiningNewApplication.PresentationCalledSerialNumber != null)
                {
                    NOCAP.BLL.Misc.Presentation.PresentationCalled obj_PresentationCalled = new NOCAP.BLL.Misc.Presentation.PresentationCalled(Convert.ToInt64(lblAppCode.Text), obj_MiningNewApplication.PresentationCalledSerialNumber);
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
    protected void grdVerificationMiningApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewVeriStageWorkFlowHistory objKeys_MiningNewVeriStageWorkFlowHistory = (NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewVeriStageWorkFlowHistory)e.Row.DataItem;
            string ApplicationCode = objKeys_MiningNewVeriStageWorkFlowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_MiningNewVeriStageWorkFlowHistory.SerialNumber.ToString();
            //string ApplicationCode = grdVerificationMiningApplicationStatus.DataKeys[e.Row.RowIndex].Values[0].ToString();
            //string SerialNumber = grdVerificationMiningApplicationStatus.DataKeys[e.Row.RowIndex].Values[1].ToString();
            NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewVeriStageWorkFlowHistory obj_MiningNewVeriStageWorkFlowHistory = new NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewVeriStageWorkFlowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
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

                if (obj_MiningNewVeriStageWorkFlowHistory.GetMiningNewApplication().NameOfMining != null && lblApplicationCode.Text != null)
                {
                 
                    lblMiningName.Text = HttpUtility.HtmlEncode(obj_MiningNewVeriStageWorkFlowHistory.GetMiningNewApplication().NameOfMining);
                }
                if (obj_MiningNewVeriStageWorkFlowHistory.GetMiningNewApplication().MiningNewApplicationNumber != null && lblApplicationCode.Text != null)
                    lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_MiningNewVeriStageWorkFlowHistory.GetMiningNewApplication().MiningNewApplicationNumber);



                if (obj_MiningNewVeriStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                { //---Added by Chirag on 29-Apr-2016 to get Office Name and user tye description--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningNewVeriStageWorkFlowHistory.FromUserCode));
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
                    //   lblFromUser.Text = HttpUtility.HtmlEncode(obj_MiningNewVeriStageWorkFlowHistory.GetFromUser().UserName);
                }
                if (obj_MiningNewVeriStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {
                    //---Added by Chirag on 29-Apr-2016 to get Office Name and user tye description--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningNewVeriStageWorkFlowHistory.ToUserCode));
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
                    //   lblToUser.Text = HttpUtility.HtmlEncode(obj_MiningNewVeriStageWorkFlowHistory.GetToUser().UserName);
                }
                if (obj_MiningNewVeriStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                { //---Added by Chirag on 29-Apr-2016 to get Office Name and user tye description--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningNewVeriStageWorkFlowHistory.ForwardedUserCode));
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
                    //   lblForwardedUser.Text = HttpUtility.HtmlEncode(obj_MiningNewVeriStageWorkFlowHistory.GetForwardedUser().UserName);
                }


                if (obj_MiningNewVeriStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_MiningNewVeriStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                obj_MiningNewVeriStageWorkFlowHistory = null;
            }
        }
    }
    protected void grdAppProcessingMiningApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewAppProcessStageWorkFlowHistory objKeys_MiningNewAppProcessStageWorkFlowHistory = (NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewAppProcessStageWorkFlowHistory)e.Row.DataItem;
            string ApplicationCode = objKeys_MiningNewAppProcessStageWorkFlowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_MiningNewAppProcessStageWorkFlowHistory.SerialNumber.ToString();
            //string ApplicationCode = grdAppProcessingMiningApplicationStatus.DataKeys[e.Row.RowIndex].Values[0].ToString();
            //string SerialNumber = grdAppProcessingMiningApplicationStatus.DataKeys[e.Row.RowIndex].Values[1].ToString();
            NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewAppProcessStageWorkFlowHistory obj_MiningNewAppStageWorkFlowHistory = new NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewAppProcessStageWorkFlowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
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


                if (obj_MiningNewAppStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {//---Added by Chirag on 29-Apr-2016 to get Office Name and user tye description--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningNewAppStageWorkFlowHistory.FromUserCode));
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
                   
                    //lblFromUser.Text = HttpUtility.HtmlEncode(obj_MiningNewAppStageWorkFlowHistory.GetFromUser().UserName);
                }
                if (obj_MiningNewAppStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                {//---Added by Chirag on 29-Apr-2016 to get Office Name and user tye description--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningNewAppStageWorkFlowHistory.ToUserCode));
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
                    //lblToUser.Text = HttpUtility.HtmlEncode(obj_MiningNewAppStageWorkFlowHistory.GetToUser().UserName);
                }
                if (obj_MiningNewAppStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                {//---Added by Chirag on 29-Apr-2016 to get Office Name and user tye description--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningNewAppStageWorkFlowHistory.ForwardedUserCode));
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
                    
                    //lblForwardedUser.Text = HttpUtility.HtmlEncode(obj_MiningNewAppStageWorkFlowHistory.GetForwardedUser().UserName);
                }
                if (obj_MiningNewAppStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_MiningNewAppStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);

                //Start Presentation
                if (obj_MiningNewAppStageWorkFlowHistory.PresentationCalledSerialNumber != null && lnkbtPresentation != null)
                {
                    NOCAP.BLL.Misc.Presentation.PresentationCalled obj_PresentationCalled = new NOCAP.BLL.Misc.Presentation.PresentationCalled(Convert.ToInt64(ApplicationCode), obj_MiningNewAppStageWorkFlowHistory.PresentationCalledSerialNumber);
                    if (obj_PresentationCalled.ApplicationCode != 0)
                    {
                        if (obj_PresentationCalled.PresentCalled == NOCAP.BLL.Common.CommonEnum.PresentationCalledOption.Yes)
                        {
                            lnkbtPresentation.Visible = true;

                            NOCAP.BLL.Misc.Presentation.PresentationDetail Obj_PresentationDetail = new NOCAP.BLL.Misc.Presentation.PresentationDetail();
                            NOCAP.BLL.Misc.Presentation.PresentationDetail[] Obj_PresentationDetailList;
                            Obj_PresentationDetailList = Obj_PresentationDetail.GetAllPresentationDetailForApplicationCode(Convert.ToInt64(ApplicationCode), Convert.ToInt32(obj_MiningNewAppStageWorkFlowHistory.PresentationCalledSerialNumber), NOCAP.BLL.Misc.Presentation.PresentationDetail.SortingField.PresentationSerialNumber);
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
                obj_MiningNewAppStageWorkFlowHistory = null;
            }
        }
    }

    protected void grdNOCProcessingMiningApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewNOCProcessStageWorkFlowHistory objKeys_MiningNewNOCProcessStageWorkFlowHistory = (NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewNOCProcessStageWorkFlowHistory)e.Row.DataItem;
            string ApplicationCode = objKeys_MiningNewNOCProcessStageWorkFlowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_MiningNewNOCProcessStageWorkFlowHistory.SerialNumber.ToString();
            //string ApplicationCode = grdNOCProcessingMiningApplicationStatus.DataKeys[e.Row.RowIndex].Values[0].ToString();
            //string SerialNumber = grdNOCProcessingMiningApplicationStatus.DataKeys[e.Row.RowIndex].Values[1].ToString();
            NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewNOCProcessStageWorkFlowHistory obj_MiningNewNOCStageWorkFlowHistory = new NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewNOCProcessStageWorkFlowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
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

                if (obj_MiningNewNOCStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                {//---Added by Chirag on 29-Apr-2016 to get Office Name and user tye description--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningNewNOCStageWorkFlowHistory.FromUserCode));
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
                    //lblFromUser.Text = HttpUtility.HtmlEncode(obj_MiningNewNOCStageWorkFlowHistory.GetFromUser().UserName);
                }
                if (obj_MiningNewNOCStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                { //---Added by Chirag on 29-Apr-2016 to get Office Name and user tye description--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningNewNOCStageWorkFlowHistory.ToUserCode));
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
                    //lblToUser.Text = HttpUtility.HtmlEncode(obj_MiningNewNOCStageWorkFlowHistory.GetToUser().UserName);
                }
                if (obj_MiningNewNOCStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                { //---Added by Chirag on 29-Apr-2016 to get Office Name and user tye description--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningNewNOCStageWorkFlowHistory.ForwardedUserCode));
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
                    //lblForwardedUser.Text = HttpUtility.HtmlEncode(obj_MiningNewNOCStageWorkFlowHistory.GetForwardedUser().UserName);
                }
                if (obj_MiningNewNOCStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_MiningNewNOCStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                obj_MiningNewNOCStageWorkFlowHistory = null;
            }
        }
    }


    protected void grdDisbursmentProcessingMiningApplicationStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewAppDisbuStageWorkflowHistory objKeys_MiningNewAppDisbuStageWorkflowHistory = (NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewAppDisbuStageWorkflowHistory)e.Row.DataItem;
            string ApplicationCode = objKeys_MiningNewAppDisbuStageWorkflowHistory.ApplicationCode.ToString();
            string SerialNumber = objKeys_MiningNewAppDisbuStageWorkflowHistory.SerialNumber.ToString();
            //string ApplicationCode = grdDisbursmentProcessingMiningApplicationStatus.DataKeys[e.Row.RowIndex].Values[0].ToString();
            //string SerialNumber = grdDisbursmentProcessingMiningApplicationStatus.DataKeys[e.Row.RowIndex].Values[1].ToString();
            NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewAppDisbuStageWorkflowHistory obj_MiningNewDisbursmentStageWorkFlowHistory = new NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewAppDisbuStageWorkflowHistory(Convert.ToInt64(ApplicationCode), Convert.ToInt32(SerialNumber));
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

                if (obj_MiningNewDisbursmentStageWorkFlowHistory.FromUserCode != null && lblFromUser != null)
                { //---Added by Chirag on 29-Apr-2016 to get Office Name--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningNewDisbursmentStageWorkFlowHistory.FromUserCode));
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
                    //lblFromUser.Text = HttpUtility.HtmlEncode(obj_MiningNewDisbursmentStageWorkFlowHistory.GetFromUser().UserName);
                }
                if (obj_MiningNewDisbursmentStageWorkFlowHistory.ToUserCode != null && lblToUser != null)
                { //---Added by Chirag on 29-Apr-2016 to get Office Name--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningNewDisbursmentStageWorkFlowHistory.ToUserCode));
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
                    //lblToUser.Text = HttpUtility.HtmlEncode(obj_MiningNewDisbursmentStageWorkFlowHistory.GetToUser().UserName);
                }
                if (obj_MiningNewDisbursmentStageWorkFlowHistory.ForwardedUserCode != null && lblForwardedUser != null)
                { //---Added by Chirag on 29-Apr-2016 to get Office Name--------------------------------------------------------
                    NOCAP.BLL.UserManagement.User obj_Login = new NOCAP.BLL.UserManagement.User(Convert.ToInt32(obj_MiningNewDisbursmentStageWorkFlowHistory.ForwardedUserCode));
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
                    //lblForwardedUser.Text = HttpUtility.HtmlEncode(obj_MiningNewDisbursmentStageWorkFlowHistory.GetForwardedUser().UserName);
                }
                if (obj_MiningNewDisbursmentStageWorkFlowHistory.ActionInternalStatusCode != null && lblActionInternalStatus != null)
                    lblActionInternalStatus.Text = HttpUtility.HtmlEncode(obj_MiningNewDisbursmentStageWorkFlowHistory.GetActionInternalStatus().InternalStatusDesc);

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                obj_MiningNewDisbursmentStageWorkFlowHistory = null;
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


                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningAppVerificationStatus = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewVeriStageWorkFlowHistory[] arr1;
                arr1 = obj_MiningAppVerificationStatus.GetMiningNewVeriStageWorkFlowHistoryListForReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.VeriStageWorkFlowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.VeriStageWorkFlowHistory.SortingField.NoSorting);
                GridView grdVerificationMiningApplicationStatus = (GridView)e.Row.FindControl("grdVerificationMiningApplicationStatus");
                if (arr1 != null && arr1.Length > 0)
                {
                    lblApplicationCode.Text = HttpUtility.HtmlEncode(arr1[0].ApplicationCode.ToString());
                    lblSerialNo.Text = HttpUtility.HtmlEncode(arr1[0].SerialNumber.ToString());
                }
                grdVerificationMiningApplicationStatus.DataSource = arr1;
                grdVerificationMiningApplicationStatus.DataBind();

                //Applicaiton Processing Status
                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningAppProcessingStatus = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewAppProcessStageWorkFlowHistory[] arr2;
                arr2 = obj_MiningAppProcessingStatus.GetMiningNewAppProcessStageWorkFlowHistoryListReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.AppProcessStageWorkFlowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.AppProcessStageWorkFlowHistory.SortingField.NoSorting);
                GridView grdAppProcessingMiningApplicationStatus = (GridView)e.Row.FindControl("grdAppProcessingMiningApplicationStatus");
                grdAppProcessingMiningApplicationStatus.DataSource = arr2;
                grdAppProcessingMiningApplicationStatus.DataBind();

                //NOC Processing Status
                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNOCProcessingStatus = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewNOCProcessStageWorkFlowHistory[] arr3;
                arr3 = obj_MiningNOCProcessingStatus.GetMiningNewNOCProcessStageWorkFlowHistoryListForReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.NOCProcessStageWorkFlowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.NOCProcessStageWorkFlowHistory.SortingField.NoSorting);
                GridView grdNOCProcessingMiningApplicationStatus = (GridView)e.Row.FindControl("grdNOCProcessingMiningApplicationStatus");
                grdNOCProcessingMiningApplicationStatus.DataSource = arr3;
                grdNOCProcessingMiningApplicationStatus.DataBind();

                //Disbursment Processing Status
                NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningDisbursmentProcessingStatus = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(Convert.ToInt32(ApplicationCode));
                NOCAP.BLL.Mining.New.WorkFlowHistory.MiningNewAppDisbuStageWorkflowHistory[] arr4;
                arr4 = obj_MiningDisbursmentProcessingStatus.GetMiningNewDisbursmentProcessStageWorkFlowHistoryListForReferBackSN(Convert.ToInt32(ReferBackSN), NOCAP.BLL.Common.AppDisbuStageWorkflowHistory.SortingField.ReceiveDate, NOCAP.BLL.Common.AppDisbuStageWorkflowHistory.SortingField.NoSorting);
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
}