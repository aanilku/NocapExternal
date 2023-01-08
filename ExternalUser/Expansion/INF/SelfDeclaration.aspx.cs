using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_InfrastructureNew_SelfDeclaration : System.Web.UI.Page
{
    //string strPageName = "INFSelfDeclaration";
    //string strActionName = "";
    //string strStatus = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            rngDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
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
                        if (SourceLabel != null) { lblModeFrom.Text =HttpUtility.HtmlEncode(SourceLabel.Text); }
                    }
                }
                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    if (placeHolder != null)
                    {
                        Label SourceLabel = (Label)placeHolder.FindControl("lblInfrastructureApplicationCodeFrom");
                        if (SourceLabel != null) { lblInfrastructureApplicationCodeFrom.Text =HttpUtility.HtmlEncode(SourceLabel.Text); }
                    }
                }
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    BindSelfDeclarationDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
            }

        }
    }

    private void BindSelfDeclarationDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            if (obj_infrastructureNewApplication.BharatTransReferanceNumber ==null)
                txtBharatKoshRefferenceNo.Text = "";
            else
                txtBharatKoshRefferenceNo.Text = obj_infrastructureNewApplication.BharatTransReferanceNumber.ToString();
            if (obj_infrastructureNewApplication.BharatTransDated == null)
                txtBharatKoshDated.Text = "";
            else
                txtBharatKoshDated.Text = Convert.ToDateTime(obj_infrastructureNewApplication.BharatTransDated).ToString("dd/MM/yyyy");

            NOCAP.BLL.Master.FeeRequiredPending objFeeRequiredPending = new NOCAP.BLL.Master.FeeRequiredPending(obj_infrastructureNewApplication.ApplicationTypeCode, obj_infrastructureNewApplication.ApplicationPurposeCode);

            if (objFeeRequiredPending != null && objFeeRequiredPending.RequiredPending == NOCAP.BLL.Master.FeeRequiredPending.RequiredOrPending.Required)
            {
                //lblFee.Text = HttpUtility.HtmlEncode("Applicant has to Submit Processing Fee of Rs " + objFeeRequiredPending.Amount + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(objFeeRequiredPending.Amount))) + ") in the form of Demand Draft drawn in Favour of PAO, CGWB and Payable at Faridabad, Haryana.)");
                // lblFee.Text = HttpUtility.HtmlEncode("Applicant has to Submit Processing Fee of Rs " + objFeeRequiredPending.Amount + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(objFeeRequiredPending.Amount))) + ") through NON TAX RECEIPT PORTAL (https://bharatkosh.gov.in). A receipt will be generated. Please fill in the Transaction Ref No. and Date from the receipt, in print out of application and attach receipt along with hard copy of application.");                          
                lblFee.Text = HttpUtility.HtmlEncode("Reciept of Processing Fee of Rs. " + objFeeRequiredPending.Amount + " /- (" + HttpUtility.HtmlEncode(NOCAPExternalUtility.ParseInput(Convert.ToString(objFeeRequiredPending.Amount))) + ") submitted  through NON TAX RECEIPT PORTAL (https://bharatkosh.gov.in) should be attached in online application at prescribed place before submission of application.");

            }
            else
            {
                lblFee.Text = "Processing Fee : Not Required.";

            }
            
                       
            //if (obj_infrastructureNewApplication.Undertaking.UndertakingAgreement == NOCAP.BLL.Common.Undertaking.UndertakingAgreementOption.Yes)
            //{
            //    chkBoxUndertaking.Checked = true;
            //}
            //else
            //{
            //    chkBoxUndertaking.Checked = false;
            //}
            NOCAP.BLL.Master.HeadQuarter obj_headQuarter = new NOCAP.BLL.Master.HeadQuarter(1);
            string HQOfficeName = obj_headQuarter.HeadQuarterName;
            string HQAddLine1 = obj_headQuarter.AddressLine1;
            string HQAddLine2 = obj_headQuarter.AddressLine2;
            string HQAddLine3 = obj_headQuarter.AddressLine3;
            string HQState = obj_headQuarter.GetAddressStateName();
            string HQDistrict = obj_headQuarter.GetAddressDistrictName();
            string HQSubDistrict = obj_headQuarter.GetAddressSubDistrictName();
            string HQPinCode = Convert.ToString(obj_headQuarter.PinCode);
            //lblHqAddress.Text = HQOfficeName + ", <br />" + HQAddLine1 + ", <br />" + HQAddLine2 + ", <br />" + HQAddLine3 + ", <br />" + HQSubDistrict + ", " + HQDistrict + ", " + HQState + " - " + HQPinCode;

            string RegionalOfficeName = obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().RegionalOfficeName;
            string RegionalAddLine1 = obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().AddressLine1;
            string RegionalAddLine2 = obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().AddressLine2;
            string RegionalAddLine3 = obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().AddressLine3;
            string RegionalSubDistrict = obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().GetAddressSubDistrictName();
            string RegionalDistrict = obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().GetAddressDistrictName();
            string RegionalState = obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().GetAddressStateName();

            string RegionalPinCode = Convert.ToString(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.GetState().GetAssociatedRegionalOffice().PinCode);
           // lblRegionalOffi.Text = RegionalOfficeName + ", <br />" + RegionalAddLine1 + ", <br />" + RegionalAddLine2 + ", <br />" + RegionalAddLine3 + ", <br />" + RegionalSubDistrict + ", " + RegionalDistrict + ", " + RegionalState + " - " + RegionalPinCode;
           // lblRegionalOffi.Text = HttpUtility.HtmlEncode(RegionalOfficeName) + ", <br />" + HttpUtility.HtmlEncode(RegionalAddLine1) + (RegionalAddLine2 == "" ? " " : ", <br />") + HttpUtility.HtmlEncode(RegionalAddLine2) + (RegionalAddLine3 == "" ? "" : ", <br />") + HttpUtility.HtmlEncode(RegionalAddLine3) + ", <br />" + HttpUtility.HtmlEncode(RegionalDistrict) + ", " + HttpUtility.HtmlEncode(RegionalState) + " - " + HttpUtility.HtmlEncode(RegionalPinCode);
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
        finally
        {
        }
    }

    private int UpdateSelfDeclarationDetails(long lngA_ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(lngA_ApplicationCode);
            obj_infrastructureNewApplication.Undertaking.UndertakingAgreement = NOCAP.BLL.Common.Undertaking.UndertakingAgreementOption.Yes;
            //if (chkBoxUndertaking.Checked)
            //{
            //    obj_infrastructureNewApplication.Undertaking.UndertakingAgreement = NOCAP.BLL.Common.Undertaking.UndertakingAgreementOption.Yes;
            //}
            //else
            //{
            //    obj_infrastructureNewApplication.Undertaking.UndertakingAgreement = NOCAP.BLL.Common.Undertaking.UndertakingAgreementOption.No;
            //}

            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            obj_infrastructureNewApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

            if (txtBharatKoshRefferenceNo.Text != "")
                obj_infrastructureNewApplication.BharatTransReferanceNumber = Convert.ToInt64(txtBharatKoshRefferenceNo.Text);
            else
                obj_infrastructureNewApplication.BharatTransReferanceNumber = null;
            if (txtBharatKoshDated.Text != "")
                obj_infrastructureNewApplication.BharatTransDated = Convert.ToDateTime(txtBharatKoshDated.Text);
            else
                obj_infrastructureNewApplication.BharatTransDated = null;


            if (obj_infrastructureNewApplication.Update() == 1)
            {
                lblMessage.Text = "Successfully Saved";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                return 1;
            }
            else
            {
                lblMessage.Text = obj_infrastructureNewApplication.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
            return 0;
        }
        finally
        {
        }
    }

    protected void btnPrev_Click(object sender, EventArgs e)
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
                Server.Transfer("OtherDetails.aspx");
            }
        }
    }
    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            UpdateSelfDeclarationDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
            //if (chkBoxUndertaking.Checked)
            //{
            //    UpdateSelfDeclarationDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
            //}
            //else
            //{
            //    lblMessage.Text = "Please accept Undertaking";
            //    lblMessage.ForeColor = System.Drawing.Color.Red;
            //    Page.SetFocus(chkBoxUndertaking);
            //}
        }
    }
    protected void txtNext_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            if (UpdateSelfDeclarationDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text)) == 1)
            {
                Server.Transfer("Attachment.aspx");
            }
            //if (chkBoxUndertaking.Checked)
            //{
            //    if (UpdateSelfDeclarationDetails(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text)) == 1)
            //    {
            //        Server.Transfer("Attachment.aspx");
            //    }
            //    else
            //    {

            //    }
            //}
            //else
            //{
            //    lblMessage.Text = "Please accept Undertaking";
            //    lblMessage.ForeColor = System.Drawing.Color.Red;
            //    Page.SetFocus(chkBoxUndertaking);
            //}
        }
    }
    protected void ValidateDate(object sender, ServerValidateEventArgs e)
    {
        try
        {
            if (NOCAPExternalUtility.IsValidDate(e.Value))
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
        }
        catch (Exception ex)
        {
            e.IsValid = false;
        }
    }
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    if (Page.IsValid)
    //    {
    //        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
    //        {
    //            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
    //        }
    //        else
    //        {
    //            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
    //            Session["CSRF"] = hidCSRF.Value;
    //            strActionName = "UpdateSelfDeclaration";
    //            long lng_submittedApplicationCode = 0;
    //            NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication(Convert.ToInt32(lblInfrastructureApplicationCodeFrom.Text));
    //            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
    //            try
    //            {
    //                string str_startPathFromConfigSource = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPDraft"].ToString();
    //                string str_infrastructureNewSADApplicationSubDirectorySource = str_startPathFromConfigSource + Convert.ToString(obj_infrastructureNewApplication.ApplicationCode);

    //                string str_startPathFromConfigTemp = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPFinal"].ToString();
    //                string str_infrastructureNewSADApplicationSubDirectoryTemp = str_startPathFromConfigTemp + Convert.ToString(obj_infrastructureNewApplication.ApplicationCode) + "-temp";

    //                if (!(Directory.Exists(str_infrastructureNewSADApplicationSubDirectorySource)))
    //                {
    //                    Response.Write(str_infrastructureNewSADApplicationSubDirectorySource + " not found");
    //                }
    //                else
    //                {
    //                    if (!(Directory.Exists(str_infrastructureNewSADApplicationSubDirectoryTemp)))
    //                    {
    //                        DirectoryInfo di = Directory.CreateDirectory(str_infrastructureNewSADApplicationSubDirectoryTemp);
    //                    }
    //                    else
    //                    {
    //                        System.IO.Directory.Delete(str_infrastructureNewSADApplicationSubDirectoryTemp, true);
    //                        DirectoryInfo di = Directory.CreateDirectory(str_infrastructureNewSADApplicationSubDirectoryTemp);
    //                    }

    //                    DirectoryCopy(str_infrastructureNewSADApplicationSubDirectorySource, str_infrastructureNewSADApplicationSubDirectoryTemp, true);
    //                    if (obj_infrastructureNewApplication.SubmitApplication(out lng_submittedApplicationCode, null, obj_externalUser.ExternalUserCode) == 1)
    //                    {
    //                        strStatus = "Update Successdully";
    //                        Response.Write(obj_infrastructureNewApplication.CustumMessage);
    //                        //lng_submittedApplicationCode = 1000;
    //                        string str_startPathFromConfigFinal = ConfigurationManager.AppSettings["NOCAPFilePath"].ToString() + ConfigurationManager.AppSettings["NOCAPFinal"].ToString();
    //                        string str_infrastructureNewSADApplicationSubDirectoryFinal = str_startPathFromConfigFinal + Convert.ToString(lng_submittedApplicationCode) + Path.DirectorySeparatorChar.ToString() + Convert.ToString(obj_infrastructureNewApplication.ApplicationCode);
    //                        if ((Directory.Exists(str_infrastructureNewSADApplicationSubDirectoryFinal)))
    //                        {

    //                            Response.Write(str_infrastructureNewSADApplicationSubDirectoryFinal + " found- replacement not allowed");
    //                            //DirectoryInfo di = Directory.CreateDirectory(str_industrialNewSADApplicationSubDirectorySource);
    //                        }
    //                        else
    //                        {
    //                            DirectoryInfo di = Directory.CreateDirectory(str_infrastructureNewSADApplicationSubDirectoryFinal);
    //                            DirectoryCopy(str_infrastructureNewSADApplicationSubDirectoryTemp, str_infrastructureNewSADApplicationSubDirectoryFinal, true);
    //                            ////after copy delete temp
    //                            System.IO.Directory.Delete(str_infrastructureNewSADApplicationSubDirectoryTemp, true);

    //                            ////after copy delete SAD
    //                            System.IO.Directory.Delete(str_infrastructureNewSADApplicationSubDirectorySource, true);
    //                        }
    //                    }
    //                    else
    //                    {
    //                        strStatus = "Update Unsuccessfull";
    //                        Response.Write(obj_infrastructureNewApplication.CustumMessage);
    //                    }
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                // error handle
    //            }
    //            finally
    //            {
    //                ActionTrail obj_ExtActionTrail = new ActionTrail();
    //                if (Session["ExternalUserCode"] != null)
    //                {
    //                    obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
    //                    obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
    //                    obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
    //                    obj_ExtActionTrail.Status = strStatus;
    //                    if (obj_ExtActionTrail != null)
    //                        ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
    //                }
    //            }
    //        }
    //    }
    //}

    //private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
    //{
    //    // Get the subdirectories for the specified directory.
    //    DirectoryInfo dir = new DirectoryInfo(sourceDirName);
    //    DirectoryInfo[] dirs = dir.GetDirectories();

    //    if (!dir.Exists)
    //    {
    //        throw new DirectoryNotFoundException(
    //            "Source directory does not exist or could not be found: "
    //            + sourceDirName);
    //    }

    //    // If the destination directory doesn't exist, create it. 
    //    if (!Directory.Exists(destDirName))
    //    {
    //        Directory.CreateDirectory(destDirName);
    //    }

    //    // Get the files in the directory and copy them to the new location.
    //    FileInfo[] files = dir.GetFiles();
    //    foreach (FileInfo file in files)
    //    {
    //        string temppath = Path.Combine(destDirName, file.Name);
    //        file.CopyTo(temppath, false);
    //    }

    //    // If copying subdirectories, copy them and their contents to new location. 
    //    if (copySubDirs)
    //    {
    //        foreach (DirectoryInfo subdir in dirs)
    //        {
    //            string temppath = Path.Combine(destDirName, subdir.Name);
    //            DirectoryCopy(subdir.FullName, temppath, copySubDirs);
    //        }
    //    }
    //}

}