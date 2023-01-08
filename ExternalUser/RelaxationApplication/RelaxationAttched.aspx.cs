using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Relaxation;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Threading;

public partial class ExternalUser_RelaxationAttched : System.Web.UI.Page
{
    string strStatus = "";
    string strActionName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //int c = Convert.ToInt32(Session["ExternalUserCode"]);
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                txtBharatKoshDatedCalendarExtender.EndDate = System.DateTime.Now;
                revtxtBharatKoshDated.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");

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
                if (lblModeFrom.Text.Trim() == "Edit")
                {
                    BindCommunicationDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));
                    BindAttachment(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
                }
                
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    private void BindCommunicationDetails(long lngA_ApplicationCode)
    {
        try
        {
            RelaxationApplication obj_relaxationApplication = new RelaxationApplication(lngA_ApplicationCode);
            if (obj_relaxationApplication != null && obj_relaxationApplication.ApplicationCode > 0)
            {
                txtAmount.Text = HttpUtility.HtmlEncode(obj_relaxationApplication.PayMentAmount);
                txtBharatKoshRefferenceNo.Text = HttpUtility.HtmlEncode(obj_relaxationApplication.BharatTransReferanceNumber);

                if(obj_relaxationApplication.BharatTransDated!=null)
                        txtBharatKoshDated.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_relaxationApplication.BharatTransDated).ToShortDateString());
            }
            else
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
        catch (Exception)
        {
            //lblMessage.Text = ex.Message;
            //lblMessage.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }

    private void BindAttachment(long lngA_ApplicationCode)
    {
        try
        {
            
            BindGridView(gvSitePlan, lngA_ApplicationCode, ref lblMessageNOCCount);
          
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);

            //  lblMessageRainwaterHarvesting.Text = ex.Message;
        }
    }

    private void BindGridView(GridView gv, long lngA_ApplicationCode, ref Label AttCount)
    {
        RelaxationApplication obj_relaxationApplication = new RelaxationApplication(lngA_ApplicationCode);
        int attcount = obj_relaxationApplication.PayMentAttCode;
        Relaxation.RelaxationAttachmentBLL obj_RelaxationAttachment = new Relaxation.RelaxationAttachmentBLL(lngA_ApplicationCode, attcount);
        Relaxation.RelaxationAttachmentBLL[] arr_SRelaxationAttachment = null;

        if (gv.ID == "gvSitePlan")
        {
            //txtRelaxation.Text = obj_RelaxationAttachment.AttachmentName;
            arr_SRelaxationAttachment = obj_RelaxationAttachment.GetRelaxationAttachmentList();

        }
        
        gv.DataSource = arr_SRelaxationAttachment;
        gv.DataBind();
        AttCount.Text = arr_SRelaxationAttachment.Length.ToString();
    }

    private int AttachmentSizeLimit()
    {
        try
        {
            NOCAP.BLL.Common.AttachmentLimit obj_attachmentLimit = new NOCAP.BLL.Common.AttachmentLimit();

            int AttachmentSize = 1048576 * (obj_attachmentLimit.SizeOfEachAttachment);
            return AttachmentSize;
        }
        catch
        {
            //lblMessageSitePlan.Text = "Problem in Fetch Data";
            //lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
            return 0;
        }

    }
    protected void btnUploadSitePlan_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                RelaxationApplication obj_relaxationApplication = new RelaxationApplication(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));

                string str_fname;
                string str_ext;
                RelaxationAttachmentBLL obj_RelaxationApplicationForNoLimit = new RelaxationAttachmentBLL();
                byte[] buffer = new byte[1];
                if (FileUploadSitePlan.HasFile)
                {
                    long AppCode = obj_relaxationApplication.ApplicationCode;
                    str_ext = System.IO.Path.GetExtension(FileUploadSitePlan.PostedFile.FileName).ToLower();
                    str_fname = FileUploadSitePlan.FileName;
                    if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                    {
                        if (NOCAPExternalUtility.IsValidFile(FileUploadSitePlan.PostedFile))
                        {
                            if (FileUploadSitePlan.PostedFile.ContentLength < AttachmentSizeLimit())
                            {
                                obj_RelaxationApplicationForNoLimit.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadSitePlan.PostedFile);
                                obj_RelaxationApplicationForNoLimit.ContentType = FileUploadSitePlan.PostedFile.ContentType;
                                obj_RelaxationApplicationForNoLimit.AttachmentPath = FileUploadSitePlan.FileName;
                                obj_RelaxationApplicationForNoLimit.FileExtension = str_ext;
                                obj_RelaxationApplicationForNoLimit.AttachmentCode = obj_relaxationApplication.PayMentAttCode;
                                obj_RelaxationApplicationForNoLimit.ApplicationCode = AppCode;
                                obj_RelaxationApplicationForNoLimit.AttachmentName = txtRelaxation.Text;
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessage.Text = "File can not upload. It has more than 5 MB size";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblMessage.Text = "Not a valid file!!..Select an other file!!";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        lblMessage.Text = "Not a valid file!!..Select an other file!!";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                }
                else
                {
                    lblMessage.Text = "Please select a file..!!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }


                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                obj_RelaxationApplicationForNoLimit.CreatedByExUC = obj_externalUser.ExternalUserCode;

                if (obj_RelaxationApplicationForNoLimit.Add() == 1)
                {
                    strStatus = "File Upload Success";
                    lblMessage.Text = obj_RelaxationApplicationForNoLimit.CustumMessage;
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    txtRelaxation.Text = "";
                }
                else
                {
                    strStatus = "File Upload Failed";
                    lblMessage.Text = obj_RelaxationApplicationForNoLimit.CustumMessage;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                // BindIndustrialNewApplicationAttachmentSitePlanDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text));                                                                  
                // clearMessage();
                lblMessage.Text = obj_RelaxationApplicationForNoLimit.CustumMessage;
                BindGridView(gvSitePlan, Convert.ToInt64(lblIndustialApplicationCodeFrom.Text), ref lblMessageNOCCount);
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                //lblMessageSitePlan.Text = "";
                //lblMessageNOCCount.Text = "";
            }
        }
    }
    protected void gvSitePlan_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            try
            {
                strActionName = "Delete Self Compliance Attachment";
                long lng_ApplicationCode = Convert.ToInt32(gvSitePlan.DataKeys[e.RowIndex].Values["ApplicationCode"]);
                int int_AttachmentCode = Convert.ToInt32(gvSitePlan.DataKeys[e.RowIndex].Values["AttachmentCode"]);
                int int_AttachmentCodeSerialNumber = Convert.ToInt32(gvSitePlan.DataKeys[e.RowIndex].Values["AttachmentCodeSerialNumber"]);
                Relaxation.RelaxationAttachmentBLL obj_RelaxationAttachment = new Relaxation.RelaxationAttachmentBLL(lng_ApplicationCode, int_AttachmentCode, int_AttachmentCodeSerialNumber);


                if (obj_RelaxationAttachment.Delete() == 1)
                {
                    strStatus = "File Deleted Success";
                    lblMessage.Text = obj_RelaxationAttachment.CustumMessage;
                    lblMessage.ForeColor = System.Drawing.Color.Red;

                    BindGridView(gvSitePlan, Convert.ToInt64(lblIndustialApplicationCodeFrom.Text), ref lblMessageNOCCount);
                }
                else
                {
                    strStatus = "File Deleted Failed";
                    lblMessage.Text = obj_RelaxationAttachment.CustumMessage;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                ActionTrail obj_ExtActionTrail = new ActionTrail();
                if (Session["ExternalUserCode"] != null)
                {
                    obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
                    obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
                   // obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
                    obj_ExtActionTrail.Status = strStatus;
                    if (obj_ExtActionTrail != null)
                        ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
                }
            }
        
    }
  
    protected void ViewFile(object sender, CommandEventArgs e)
    {
            try
            {
                if (e.CommandArgument != null)
                {

                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_industrialNewApplicationCode = Convert.ToInt32(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    RelaxationAttachmentBLL.RelaxationAppDownloadFiles(lng_industrialNewApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
               Session["CSRF"] = hidCSRF.Value;
            }
       
    }

    protected void ValidateDate(object sender, ServerValidateEventArgs e)
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
    

    #region Button Click
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        //try
        //{
            //if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            //{
            //    Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            //}
            //else
            //{

            //    hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            //    Session["CSRF"] = hidCSRF.Value;

                Server.Transfer("~/ExternalUser/RelaxationApplication/CommunicationAddress.aspx");
        //    }
        //}
        //catch (ThreadAbortException)
        //{

        //}
        //catch (Exception)
        //{
        //    Response.Redirect("~/ExternalErrorPage.aspx", false);

        //}

    }

    protected void txtNext_Click(object sender, EventArgs e)
    {
            try
            {

                if (Page.IsValid)
                {
                    if (UpdateCommunicationDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text)) == 1)
                    {
                        Server.Transfer("~/ExternalUser/RelaxationApplication/RelaxationSubmit.aspx");
                    }

                }
            }
            catch (ThreadAbortException)
            {

            }

            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                // obj_ApplicationAllowOrNotForExemptionLetter.Dispose();
                //switch (str_RedirectPath)
                //{
                //    case "~/ExternalUser/IndustrialNew/SalientFeature.aspx":
                //        Server.Transfer("~/ExternalUser/IndustrialNew/SalientFeature.aspx");
                //        break;
                //    case "~/ExternalUser/IndustrialNew/LandUseDetails.aspx":
                //        Server.Transfer("~/ExternalUser/IndustrialNew/LandUseDetails.aspx");
                //        break;

                //}
            }
        //}
    }
    private int UpdateCommunicationDetails(long lngA_ApplicationCode)
    {
        if (Page.IsValid)
        {
            try
            {

                if(Convert.ToDecimal(txtAmount.Text)<10000)
                {
                    lblMessage.Text = "Amount must be 10000 Rs.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return 0;
                }
                strActionName = "Update Communication Address";
                RelaxationApplication obj_relaxationApplication = new RelaxationApplication(lngA_ApplicationCode);
                obj_relaxationApplication.PayMentAmount = Convert.ToDecimal(txtAmount.Text);
                obj_relaxationApplication.BharatTransReferanceNumber = Convert.ToInt64(txtBharatKoshRefferenceNo.Text);
                obj_relaxationApplication.BharatTransDated = Convert.ToDateTime(txtBharatKoshDated.Text);
                
               
              
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                obj_relaxationApplication.ModifiedByExUC = obj_externalUser.ExternalUserCode;

                if (obj_relaxationApplication.Update() == 1)
                {
                    strStatus = "Update Success";
                    lblMessage.Text = "Successfully Saved";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    return 1;
                }
                else
                {
                    lblMessage.Text = obj_relaxationApplication.CustumMessage;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return 0;
                }
            }
            catch (Exception)
            {
                //lblMessage.Text = ex.Message;
                //lblMessage.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
                return 0;
            }
            finally
            {
                //ActionTrail obj_ExtActionTrail = new ActionTrail();
                //if (Session["ExternalUserCode"] != null)
                //{
                //    obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
                //    obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
                //    obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
                //    obj_ExtActionTrail.Status = strStatus;
                //    if (obj_ExtActionTrail != null)
                //        ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
                //}
            }
        }
        return 0;
    }

    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {

        //if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        //{
        //    Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        //}
        //else
        //{

            //hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            //Session["CSRF"] = hidCSRF.Value;
            try
            {


                if (Page.IsValid)
                {
                    if (UpdateCommunicationDetails(Convert.ToInt32(lblIndustialApplicationCodeFrom.Text)) == 1)
                    {

                    }
                    else { }
                }

                //NOCAP.BLL.Master.ExemptionLetterConditions exem = new NOCAP.BLL.Master.ExemptionLetterConditions();
                //Response.Write(exem.AreaTypeCode);
                //Response.Write("vv"+ exem.AreaTypeCategorCode);
                //Response.Write("vv" + exem.WaterBased);
                //Response.Write("vv" + exem.GWWaterQuantityLessThan);


                //RelaxationApplication obj_relaxationApplication = new RelaxationApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));
                //int int_stateCode = obj_relaxationApplication.StateCode;
                //int int_districtCode = obj_relaxationApplication.DistrictCode;
                //int int_subDistrictCode = obj_relaxationApplication.SubDistrictCode;
                //int int_applicationTypeCode = obj_relaxationApplication.ApplicationTypeCode;
                //int int_applicationPurposeCode = NOCAPExternalUtility.EquivalentApplicationPurposeCodeOfDatabaseForApplicationPurpose("New");
                //int int_applicationTypeCategoryCode = obj_relaxationApplication.ApplicationTypeCategoryCode;
              
                
                // decimal? dec_gwRequirement = obj_relaxationApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement;

                //  NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter obj_ApplicationAllowOrNotForExemptionLetter = new NOCAP.BLL.Master.ApplicationAllowOrNotForExemptionLetter(int_applicationTypeCode, int_applicationPurposeCode, int_applicationTypeCategoryCode, int_stateCode, int_districtCode, int_subDistrictCode, dec_gwRequirement);

                //Response.Write("Category:" + HttpUtility.HtmlEncode(obj_ApplicationAllowOrNotForExemptionLetter.ProceedForExemptionLetterOnAreaTypeCategory) + " <br / > <br /> ");
                //Response.Write("Water Based:" + HttpUtility.HtmlEncode(obj_ApplicationAllowOrNotForExemptionLetter.ProceedForExemptionLetterOnWaterBased) + " <br / > <br /> ");
                //Response.Write("Water Less Than:" + HttpUtility.HtmlEncode(obj_ApplicationAllowOrNotForExemptionLetter.ProceedForExemptionLetterOnGWWaterQuantityLessThan) + " <br / > <br /> ");
                //Response.Write("Final:" + HttpUtility.HtmlEncode(obj_ApplicationAllowOrNotForExemptionLetter.ProceedForFinalExemptionLetter) + " <br / > <br /> ");
            }

            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        //}
    }
    #endregion

}