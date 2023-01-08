using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExternalUser_ApplicationManagement_ApplicationNameChange : System.Web.UI.Page
{

    string strStatus = "";
    string strActionName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack != true)
            {
                //int c = Convert.ToInt32(Session["ExternalUserCode"]);
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRFs"] = hidCSRFs.Value;

                if (NOCAPExternalUtility.FillDropDownApplicationType(ref ddlApplicationType) != 1)
                {
                    Response.Write("Problem in Application Type ");
                }
                if (NOCAPExternalUtility.FillDropDownCorrecationCharge(ref ddlCorrecationCharge) != 1)
                {
                    Response.Write("Problem in Application Type ");
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    private void bindGrid()
    {
        try
        {
            NOCAP.BLL.ApplicationManagement.ApplicationNameChange[] arr;
            NOCAP.BLL.ApplicationManagement.ApplicationNameChange obj_NameChangeBLL = new NOCAP.BLL.ApplicationManagement.ApplicationNameChange();
            arr = obj_NameChangeBLL.GetApplicationNameChangeList(NOCAP.BLL.ApplicationManagement.ApplicationNameChange.SortingField.NoSorting);
            // gvApplicationNameChange.DataSource = arr;
            //gvApplicationNameChange.DataBind();
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    #region SelectedIndexChanged
    protected void ddlApplicationType_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (ddlApplicationType.SelectedValue != "")
            {
                if (!NOCAPExternalUtility.IsNumeric(ddlApplicationType.SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Application Code allows only Numeric Value.');", true);
                    return;
                }
                int APPtypecode = Convert.ToInt32(ddlApplicationType.SelectedValue);
                int sessionCode = Convert.ToInt32(Session["ExternalUserCode"]);


                if (NOCAPExternalUtility.FillDropDownApplicationTypeBaseApplicationNumbar(ref ddlApplicatonNumber, APPtypecode, sessionCode) != 1)
                {
                    Response.Write("Problem in Application Type ");
                }
                ddlApplicatonNumber.Enabled = true;

            }
            else
            {
                ddlApplicatonNumber.Enabled = false;

            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Please select Application Type.";
        }


    }

    protected void ddlApplicatonNumber_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null;

            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = null;

            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = null;

            if (ddlApplicatonNumber.SelectedValue != "")
            {
                if (!NOCAPExternalUtility.IsNumeric(ddlApplicatonNumber.SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Application Code allows only Numeric Value.');", true);
                    return;
                }
                int APPtypecode = Convert.ToInt32(ddlApplicatonNumber.SelectedValue);
                int sessionCode = Convert.ToInt32(Session["ExternalUserCode"]);
                NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_InfrastructureNewApplication, out obj_MiningNewApplication, Convert.ToInt64(ddlApplicatonNumber.SelectedValue));

                if (obj_IndustrialNewApplication != null && obj_IndustrialNewApplication.CreatedByExUC > 0)
                {
                    TXTExistingName.Text = obj_IndustrialNewApplication.NameOfIndustry;
                }
                else if (obj_InfrastructureNewApplication != null && obj_InfrastructureNewApplication.CreatedByExUC > 0)
                {
                    TXTExistingName.Text = obj_InfrastructureNewApplication.NameOfInfrastructure;
                }
                else if (obj_MiningNewApplication != null && obj_MiningNewApplication.CreatedByExUC > 0)
                {
                    TXTExistingName.Text = obj_MiningNewApplication.NameOfMining;
                }

                ddlApplicatonNumber.Enabled = true;

            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = "Please select Application Type.";
        }


    }
    protected void ddlCorrecationCharge_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (ddlCorrecationCharge.SelectedValue != "")
            {
                if (!NOCAPExternalUtility.IsNumeric(ddlCorrecationCharge.SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Application Code allows only Numeric Value.');", true);
                    return;
                }
                int APPtypecode = Convert.ToInt32(ddlCorrecationCharge.SelectedValue);
                int sessionCode = Convert.ToInt32(Session["ExternalUserCode"]);

                NOCAP.BLL.Master.CorrectionCharge obj_CorrectionCharge = new NOCAP.BLL.Master.CorrectionCharge(APPtypecode);

                if (obj_CorrectionCharge != null && obj_CorrectionCharge.Rate > 0)
                {
                    txtCorrecationRate.Text = Convert.ToString(obj_CorrectionCharge.Rate);
                }
                ddlApplicatonNumber.Enabled = true;
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = "Please select Application Type.";
        }

    }
    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Session["CSRFs"] != null)
            {
                try
                {
                    Add();
                    //bindGrid();
                }
                catch (Exception ex)
                {

                }
            }

        }
        else
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    protected void gvNameChange_RowCommand(object sender, CommandEventArgs e)
    {
        if (e.CommandArgument != "ViewFile")
        {
            LinkButton btn = (LinkButton)sender;
            string[] CommandArgument = btn.CommandArgument.Split(',');
            long AppCode = Convert.ToInt32(CommandArgument[0]);
            int int_SN = Convert.ToInt32(CommandArgument[1]);


            NOCAPExternalUtility.ApplicationNameChangeDownloadFiles(AppCode, int_SN);
        }


    }
    private int Add()
    {
        try
        {
            strActionName = "Add Name Change";
            string str_fname;
            string str_ext;


            if (FileUploadRecommenedAttachment.HasFile)
            {
                str_ext = System.IO.Path.GetExtension(FileUploadRecommenedAttachment.PostedFile.FileName).ToLower();
                str_fname = FileUploadRecommenedAttachment.FileName;
                if (str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
                {
                    if (NOCAPExternalUtility.IsValidFile(FileUploadRecommenedAttachment.PostedFile))
                    {
                        if (FileUploadRecommenedAttachment.PostedFile.ContentLength <= AttachmentSizeLimit())
                        {
                            NOCAP.BLL.ApplicationManagement.ApplicationNameChange obj_NameChangeBLL = new NOCAP.BLL.ApplicationManagement.ApplicationNameChange();

                            obj_NameChangeBLL.AppCode = Convert.ToInt32(ddlApplicatonNumber.SelectedValue);

                            if (txtNewName.Text != "")
                            {
                                obj_NameChangeBLL.ProjectName = Convert.ToString(txtNewName.Text);
                            }

                            obj_NameChangeBLL.ReasonToChange = Convert.ToString(txtReasonToChange.Text);

                            obj_NameChangeBLL.AttName = FileUploadRecommenedAttachment.FileName;


                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));

                            obj_NameChangeBLL.CreatedByExUC = Convert.ToInt32(obj_externalUser.ExternalUserCode);

                            obj_NameChangeBLL.AttachmentFile = NOCAPExternalUtility.StreamFile(FileUploadRecommenedAttachment.PostedFile);

                            obj_NameChangeBLL.ContentType = FileUploadRecommenedAttachment.PostedFile.ContentType;

                            obj_NameChangeBLL.FilePath = FileUploadRecommenedAttachment.FileName;

                            obj_NameChangeBLL.FileExtension = str_ext;

                            if (obj_NameChangeBLL.Add() == 1)
                            {
                                strStatus = "Record Added Successfully";
                                lblMessage.Text = "Saved Successfully";
                                lblMessage.ForeColor = System.Drawing.Color.Green;
                                lblFileUploadMessage.Text = "";

                                return 1;
                            }
                            else
                            {
                                strStatus = "Record Adding Failed";
                                lblMessage.Text = obj_NameChangeBLL.CustumMessage;
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                return 0;
                            }
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblFileUploadMessage.Text = "File can not upload. It has more than 20 MB size";
                            lblFileUploadMessage.ForeColor = System.Drawing.Color.Red;
                            lblMessage.Text = "";
                            return 0;
                        }
                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        lblFileUploadMessage.Text = "Not a valid file!!..Select an other file!!";
                        lblFileUploadMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = "";
                        return 0;
                    }

                }
                else
                {
                    strStatus = "File Upload Failed";
                    lblFileUploadMessage.Text = "Not a valid file!!..Select an other file!!";
                    lblFileUploadMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "";
                    return 0;
                }

            }
            else
            {
                lblFileUploadMessage.Text = "Please select a file..!!";
                lblFileUploadMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }

        }
        catch
        {
            return 0;
        }


    }

    private int AttachmentSizeLimit()
    {
        try
        {
            NOCAP.BLL.Common.AttachmentLimit obj_attachmentLimit = new NOCAP.BLL.Common.AttachmentLimit();

            //  int AttachmentSize = 1048576 * (obj_attachmentLimit.SizeOfEachAttachment);
            //  return AttachmentSize;
            return 1048576 * 20;
        }
        catch
        {
            //lblMessageSitePlan.Text = "Problem in Fetch Data";
            //lblMessageSitePlan.ForeColor = System.Drawing.Color.Red;
            Response.Redirect("~/ExternalErrorPage.aspx", false);
            return 0;
        }

    }
}