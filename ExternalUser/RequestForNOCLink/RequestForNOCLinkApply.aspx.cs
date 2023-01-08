using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;

public partial class ExternalUser_RequestForNOCLink_RequestForNOCLinkApply : System.Web.UI.Page
{
    string strPageName = "RequestForNOCLinkApply";
    string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                ValidationExpInit();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;

                if (NOCAPExternalUtility.FillDropDownState(ref ddlPresLocState) != 1)
                {
                    lblMessage.Text = "Problem in State population";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }

                if (NOCAPExternalUtility.FillDropDownState(ref ddlCommState) != 1)
                {
                    lblMessage.Text = "Problem in State population";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }

                ddlPresLocTownOrVillage_SelectedIndexChanged(sender, e);
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void ValidationExpInit()
    {
        revtxtRequestedNOCIssueName.ValidationExpression = ValidationUtility.txtValForProjectName;
        revtxtRequestedNOCIssueName.ErrorMessage = ValidationUtility.txtValForProjectNameMsg;

        revtxtRequestedDescription.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtRequestedDescription.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revtxtRequestedNOCIssueAddressTyped.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtRequestedNOCIssueAddressTyped.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revtxtCommAddress1.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtCommAddress1.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revtxtCommAddress2.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtCommAddress2.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revtxtCommAddress3.ValidationExpression = ValidationUtility.txtValNAMultiLine;
        revtxtCommAddress3.ErrorMessage = ValidationUtility.txtValNAMultiLineMsg;

        revtxtPinCode.ValidationExpression = ValidationUtility.txtValForPinCode;
        revtxtPinCode.ErrorMessage = ValidationUtility.txtValForPinCodeMsg;

        revtxtStdCode.ValidationExpression = ValidationUtility.txtValForNumericWithOutFirstCharacterZero;
        revtxtStdCode.ErrorMessage = ValidationUtility.txtValForNumericWithOutFirstCharacterZeroMsg;

        revtxtPhoneNumber.ValidationExpression = ValidationUtility.txtValForNumericWithOutFirstCharacterZero;
        revtxtPhoneNumber.ErrorMessage = ValidationUtility.txtValForNumericWithOutFirstCharacterZeroMsg;

        revtxtMobileNumber.ValidationExpression = ValidationUtility.txtValForMobileNumber;
        revtxtMobileNumber.ErrorMessage = ValidationUtility.txtValForMobileNumberMsg;

        revtxtStdCodeFax.ValidationExpression = ValidationUtility.txtValForNumericWithOutFirstCharacterZero;
        revtxtStdCodeFax.ErrorMessage = ValidationUtility.txtValForNumericWithOutFirstCharacterZeroMsg;

        revtxtFaxNumber.ValidationExpression = ValidationUtility.txtValForNumericWithOutFirstCharacterZero;
        revtxtFaxNumber.ErrorMessage = ValidationUtility.txtValForNumericWithOutFirstCharacterZeroMsg;

        revtxtEmail.ValidationExpression = ValidationUtility.txtValForEmail;
        revtxtEmail.ErrorMessage = ValidationUtility.txtValForEmailMsg;

    }

    protected void ddlPresLocState_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intStateCode;
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {

                lblMessage.Text = "";
                ddlPresLocDistrict.Items.Clear();
                ddlPresLocSubDistrict.Items.Clear();
                ddlPresLocTownOrVillage.Items.Clear();
                ddlPresLocVillage.Items.Clear();
                ddlPresLocTown.Items.Clear();

                if (ddlPresLocState.SelectedValue == "")
                {
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlPresLocDistrict);
                }
                else
                {
                    intStateCode = Convert.ToInt32(ddlPresLocState.SelectedValue);
                    if (NOCAPExternalUtility.FillDropDownDistrict(ref ddlPresLocDistrict, intStateCode) != 1)
                    {
                        lblMessage.Text = "Problem in District population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void ddlPresLocDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        int int_StateCode;
        int int_DistricCode;

        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {
                lblMessage.Text = "";
                ddlPresLocSubDistrict.Items.Clear();
                ddlPresLocTownOrVillage.Items.Clear();
                ddlPresLocVillage.Items.Clear();
                ddlPresLocTown.Items.Clear();

                if (ddlPresLocDistrict.SelectedValue == "")
                {
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlPresLocSubDistrict);
                }
                else
                {
                    int_DistricCode = Convert.ToInt32(ddlPresLocDistrict.SelectedValue);
                    int_StateCode = Convert.ToInt32(ddlPresLocState.SelectedValue);
                    if (NOCAPExternalUtility.FillDropDownSubDistrict(ref ddlPresLocSubDistrict, int_DistricCode, int_StateCode) != 1)
                    {
                        lblMessage.Text = "Problem in Sub-District population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    protected void ddlCommState_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intStateCode;
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {

                lblMessage.Text = "";

                ddlCommDistrict.Items.Clear();
                ddlCommSubDistrict.Items.Clear();

                if (ddlCommState.SelectedValue == "")
                {
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlCommDistrict);
                }
                else
                {
                    intStateCode = Convert.ToInt32(ddlCommState.SelectedValue);
                    if (NOCAPExternalUtility.FillDropDownDistrict(ref ddlCommDistrict, intStateCode) != 1)
                    {
                        lblMessage.Text = "Problem in District population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void ddlCommDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        int int_StateCode;
        int int_DistricCode;

        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {
                lblMessage.Text = "";
                ddlCommSubDistrict.Items.Clear();

                if (ddlCommDistrict.SelectedValue == "")
                {
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlCommSubDistrict);
                }
                else
                {
                    int_DistricCode = Convert.ToInt32(ddlCommDistrict.SelectedValue);
                    int_StateCode = Convert.ToInt32(ddlCommState.SelectedValue);
                    if (NOCAPExternalUtility.FillDropDownSubDistrict(ref ddlCommSubDistrict, int_DistricCode, int_StateCode) != 1)
                    {
                        lblMessage.Text = "Problem in Sub-District population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    private int AttachmentSizeLimit()
    {
        try
        {
            int AttachmentSize = 1048576;
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                string str_ext;
                string str_fname;
                strActionName = "Add Request For NOC Link";

                NOCAP.BLL.Misc.RequestForNOCLink.RequestForNOCLink obj_RequestForNOCLink = new NOCAP.BLL.Misc.RequestForNOCLink.RequestForNOCLink();
                NOCAP.BLL.Misc.RequestForNOCLink.RequestForNOCLinkAttachment obj_RequestForNOCLinkAttachment = new NOCAP.BLL.Misc.RequestForNOCLink.RequestForNOCLinkAttachment();
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));

                obj_RequestForNOCLink.RequestedNOCNo = Convert.ToString(txtRequesteNOCNo.Text);
                obj_RequestForNOCLink.RequestedNOCIssueName = Convert.ToString(txtRequestedNOCIssueName.Text);
                obj_RequestForNOCLink.RequestedNOCIssueAddressTyped = Convert.ToString(txtRequestedNOCIssueAddressTyped.Text);
                obj_RequestForNOCLink.RequestedDesc = Convert.ToString(txtRequestedDescription.Text);


                if (NOCAPExternalUtility.IsNumeric(ddlPresLocState.SelectedValue))
                {
                    obj_RequestForNOCLink.PresentLocationAddress.StateCode = Convert.ToInt32(ddlPresLocState.SelectedValue);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('State allows Numeric ');", true);
                    return;
                }

                if (NOCAPExternalUtility.IsNumeric(ddlPresLocDistrict.SelectedValue))
                {
                    obj_RequestForNOCLink.PresentLocationAddress.DistrictCode = Convert.ToInt32(ddlPresLocDistrict.SelectedValue);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('District allows Numeric ');", true);
                    return;
                }

                if (NOCAPExternalUtility.IsNumeric(ddlPresLocSubDistrict.SelectedValue))
                {
                    obj_RequestForNOCLink.PresentLocationAddress.SubDistrictCode = Convert.ToInt32(ddlPresLocSubDistrict.SelectedValue);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('SubDistrict allows Numeric ');", true);
                    return;
                }

                switch (ddlPresLocTownOrVillage.SelectedValue)
                {
                    case "V":
                        obj_RequestForNOCLink.PresentLocationAddress.VillageOrTown = NOCAP.BLL.Common.Address.VillageOrTownOption.Village;
                        obj_RequestForNOCLink.PresentLocationAddress.VillageCode = Convert.ToInt32(ddlPresLocVillage.SelectedValue);
                        break;
                    case "T":
                        obj_RequestForNOCLink.PresentLocationAddress.TownCode = Convert.ToInt32(ddlPresLocTown.SelectedValue);
                        obj_RequestForNOCLink.PresentLocationAddress.VillageOrTown = NOCAP.BLL.Common.Address.VillageOrTownOption.Town;
                        break;
                }

                obj_RequestForNOCLink.CommunicationAddress.AddressLine1 = txtCommAddress1.Text;
                obj_RequestForNOCLink.CommunicationAddress.AddressLine2 = txtCommAddress2.Text;
                obj_RequestForNOCLink.CommunicationAddress.AddressLine3 = txtCommAddress3.Text;

                if (NOCAPExternalUtility.IsNumeric(ddlCommState.SelectedValue))
                {
                    obj_RequestForNOCLink.CommunicationAddress.StateCode = Convert.ToInt32(ddlCommState.SelectedValue);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('State allows Numeric ');", true);
                    return;
                }
                if (NOCAPExternalUtility.IsNumeric(ddlCommDistrict.SelectedValue))
                {
                    obj_RequestForNOCLink.CommunicationAddress.DistrictCode = Convert.ToInt32(ddlCommDistrict.SelectedValue);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('District allows Numeric ');", true);
                    return;
                }

                if (ddlCommSubDistrict.SelectedIndex > 0 && !NOCAPExternalUtility.IsNumeric(ddlCommSubDistrict.SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Sub District allows Numeric ');", true);
                    return;

                }
                else
                {
                    if (ddlCommSubDistrict.SelectedValue == "")
                    {
                        obj_RequestForNOCLink.CommunicationAddress.SubDistrictCode = 0;
                    }
                    else
                    {
                        obj_RequestForNOCLink.CommunicationAddress.SubDistrictCode = Convert.ToInt32(ddlCommSubDistrict.SelectedValue);
                    }
                }

                obj_RequestForNOCLink.CommunicationAddress.PinCode = Convert.ToInt32(txtPinCode.Text);
                obj_RequestForNOCLink.CommunicationPhoneNumber.PhoneNumberISD = txtPhoneNumber.Text != "" ? txtCountryCode.Text : "";
                obj_RequestForNOCLink.CommunicationPhoneNumber.PhoneNumberSTD = txtStdCode.Text;
                obj_RequestForNOCLink.CommunicationPhoneNumber.PhoneNumberRest = txtPhoneNumber.Text;
                obj_RequestForNOCLink.CommunicationMobileNumber.MobileNumberISD = txtMobileNumber.Text != "" ? txtMobileCountryCode.Text : "";
                obj_RequestForNOCLink.CommunicationMobileNumber.MobileNumberRest = txtMobileNumber.Text;
                obj_RequestForNOCLink.CommunicationFaxNumber.FaxNumberISD = txtFaxNumber.Text != "" ? txtCountryCodeFax.Text : "";
                obj_RequestForNOCLink.CommunicationFaxNumber.FaxNumberSTD = txtStdCodeFax.Text;
                obj_RequestForNOCLink.CommunicationFaxNumber.FaxNumberRest = txtFaxNumber.Text;
                obj_RequestForNOCLink.CommunicationEmailID = txtEmail.Text;


                // Adding Attachment for NOC 

                if (FileUploadNOCAttachment.HasFile)
                {
                    str_ext = System.IO.Path.GetExtension(FileUploadNOCAttachment.PostedFile.FileName).ToLower();
                    str_fname = FileUploadNOCAttachment.FileName;
                    if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                    {
                        if (NOCAPExternalUtility.IsValidFile(FileUploadNOCAttachment.PostedFile))
                        {
                            if (FileUploadNOCAttachment.PostedFile.ContentLength < AttachmentSizeLimit())
                            {
                                byte[] vv = NOCAPExternalUtility.StreamFile(FileUploadNOCAttachment.PostedFile);

                                obj_RequestForNOCLink.RequestForNOCLinkAttachments.AttachmentFile = vv;
                                // obj_RequestForNOCLinkAttachment.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadNOCAttachment.PostedFile);
                                obj_RequestForNOCLink.RequestForNOCLinkAttachments.ContentType = FileUploadNOCAttachment.PostedFile.ContentType;
                                obj_RequestForNOCLink.RequestForNOCLinkAttachments.AttachmentName = FileUploadNOCAttachment.FileName;
                                obj_RequestForNOCLink.RequestForNOCLinkAttachments.FileExtension = str_ext;
                            }
                            else
                            {
                                strStatus = "File Upload Failed";
                                lblMessage.Text = "File can not upload. It has more than 1 MB size";
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

                /// End For Attachment Adding For NOC

                obj_RequestForNOCLink.CreatedByExUC = obj_externalUser.ExternalUserCode;

                if (obj_RequestForNOCLink.Add() == 1)
                {
                    strStatus = "Added Successfully";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Request For NOC Link has been Submitted Successfully !'); window.location = '" + Page.ResolveUrl("RequestForNOCLinkList.aspx") + "';", true);
                    return;
                }
                else
                {
                    strStatus = "Adding Failed";
                    lblMessage.Text =HttpUtility.HtmlEncode(obj_RequestForNOCLink.CustomMessage);
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
                return;
            }
            finally
            {
                ActionTrail obj_ExtActionTrail = new ActionTrail();
                if (Session["ExternalUserCode"] != null)
                {
                    obj_ExtActionTrail.UserCode = Convert.ToInt64(Session["ExternalUserCode"]);
                    obj_ExtActionTrail.IP_Address = Request.UserHostAddress;
                    obj_ExtActionTrail.ActionPerformed = strPageName + "-" + strActionName;
                    obj_ExtActionTrail.Status = strStatus;
                    if (obj_ExtActionTrail != null)
                        ActionTrailDAL.ExtActionSave(obj_ExtActionTrail);
                }
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;

            Response.Redirect("RequestForNOCLinkList.aspx", false);
        }
    }
    protected void ddlPresLocSubDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        int int_StateCode;
        int int_DistricCode;
        int int_SubDistrictCode;

        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {

                lblMessage.Text = "";
                ddlPresLocTownOrVillage.SelectedValue = "";
                ddlPresLocVillage.Items.Clear();
                ddlPresLocTown.Items.Clear();
                ddlPresLocTownOrVillage_SelectedIndexChanged(sender, e);

                if (ddlPresLocSubDistrict.SelectedValue == "")
                {
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlPresLocVillage);
                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlPresLocTown);
                }
                else
                {

                    int_SubDistrictCode = Convert.ToInt32(ddlPresLocSubDistrict.SelectedValue);
                    int_DistricCode = Convert.ToInt32(ddlPresLocDistrict.SelectedValue);
                    int_StateCode = Convert.ToInt32(ddlPresLocState.SelectedValue);

                    if (NOCAPExternalUtility.FillDropDownTownOrVillage(ref ddlPresLocTownOrVillage) != 1)
                    {
                        lblMessage.Text = "Problem in Villlage/Town population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    if (NOCAPExternalUtility.FillDropDownVillage(ref ddlPresLocVillage, int_SubDistrictCode, int_DistricCode, int_StateCode) != 1)
                    {
                        lblMessage.Text = "Problem in Village population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }

                    if (NOCAPExternalUtility.FillDropDownTown(ref ddlPresLocTown, int_SubDistrictCode, int_DistricCode, int_StateCode) != 1)
                    {
                        lblMessage.Text = "Problem in Town population";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }
    protected void ddlPresLocTownOrVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            try
            {
                switch (ddlPresLocTownOrVillage.SelectedValue)
                {
                    case "T":
                        ddlPresLocTown.Visible = true;
                        ddlPresLocVillage.Visible = false;
                        reqValiPresLocVillage.Enabled = false;
                        reqValiPresLocTown.Enabled = true;
                        break;
                    case "V":
                        ddlPresLocTown.Visible = false;
                        ddlPresLocVillage.Visible = true;
                        reqValiPresLocTown.Enabled = false;
                        reqValiPresLocVillage.Enabled = true;
                        break;
                    case "":
                        ddlPresLocTown.Visible = false;
                        ddlPresLocVillage.Visible = false;
                        reqValiPresLocTown.Enabled = false;
                        reqValiPresLocVillage.Enabled = false;
                        break;
                    default:
                        ddlPresLocTown.Visible = false;
                        ddlPresLocVillage.Visible = false;
                        reqValiPresLocTown.Enabled = false;
                        reqValiPresLocVillage.Enabled = false;
                        break;
                }

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

}