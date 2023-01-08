using NOCAP.BLL.Grievance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExternalUser_Grievance_SubmitGrievance : System.Web.UI.Page
{

    string strPageName = "Close_Document";
    string strActionName = "";
    string strStatus = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRFs"] = hidCSRF.Value;

            if (NOCAPExternalUtility.FillDropDownState(ref ddlState) != 1)
            {
                //lblMessage.Text = "Problem in state population";
                //lblMessage.ForeColor = System.Drawing.Color.Red;
            }


        }

    }
    protected void ddlTypeofGrievance_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTypeofGrievance.SelectedIndex > 0 && Convert.ToInt32(ddlTypeofGrievance.SelectedValue) == 2)
        {
            rbtnHaveYouReceivedNOC.Enabled = false;
            txtNOCNumber.Enabled = false;
            txtQuantum.Enabled = false;
            txtNOCNumber.Text = "";
            txtQuantum.Text = "";
            rbtnSubmittedTo.Enabled = true;
        }
        else
        {
            rbtnHaveYouReceivedNOC.Enabled = true;
            txtNOCNumber.Enabled = true;
            txtQuantum.Enabled = true;
            rbtnSubmittedTo.Enabled = false;
        }
    }
    protected void rbtnHaveYouReceivedNOC_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnHaveYouReceivedNOC.SelectedValue == "Y")
        {
            txtNOCNumber.Enabled = true;
        }
        else
        {
            txtNOCNumber.Enabled = false;

        }

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ExternalUser/Grievance/Submitted.aspx");
    }
    void ClearControls()
    {
        ddlState.SelectedIndex = -1;
        ddlTypeofGrievance.SelectedIndex = -1;
        rbtnHaveYouReceivedNOC.SelectedIndex = -1;
        rbtnSubmittedTo.SelectedIndex = -1;
        txtNOCNumber.Text = "";
        txtQuantum.Text = "";
        txtRemark.Text = "";

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            GrievanceApplication obj_grievanceApplication = new GrievanceApplication();
           

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

                            obj_grievanceApplication.AttFile = NOCAPExternalUtility.StreamFile(FileUploadRecommenedAttachment.PostedFile);
                            obj_grievanceApplication.ContentType = FileUploadRecommenedAttachment.PostedFile.ContentType;
                            obj_grievanceApplication.AttPath = FileUploadRecommenedAttachment.FileName;
                            obj_grievanceApplication.FileExtension = str_ext;
                            obj_grievanceApplication.AttName = FileUploadRecommenedAttachment.FileName;
                            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                            obj_grievanceApplication.CreatedByUC = Convert.ToInt32(obj_externalUser.ExternalUserCode);



                            if (ddlState.SelectedIndex > 0)
                                obj_grievanceApplication.StateCode = Convert.ToInt32(ddlState.SelectedValue);
                            if (ddlTypeofGrievance.SelectedIndex > 0)
                                obj_grievanceApplication.GrievanceType = Convert.ToInt32(ddlTypeofGrievance.SelectedValue);

                            switch (rbtnHaveYouReceivedNOC.SelectedValue)
                            {
                                case "Y":
                                    obj_grievanceApplication.HaveyouReceivedNOC = NOCAP.BLL.Common.CommonEnum.FlagYesNo.Yes;
                                    break;
                                case "N":
                                    obj_grievanceApplication.HaveyouReceivedNOC = NOCAP.BLL.Common.CommonEnum.FlagYesNo.No;
                                    break;
                                default:
                                    obj_grievanceApplication.HaveyouReceivedNOC = NOCAP.BLL.Common.CommonEnum.FlagYesNo.NotDefined;
                                    break;
                            }
                            //if (rbtnHaveYouReceivedNOC.SelectedIndex > 0 && rbtnHaveYouReceivedNOC.SelectedValue == "Y")
                            //    obj_grievanceApplication.HaveyouReceivedNOC = NOCAP.BLL.Common.CommonEnum.FlagYesNo.Yes;
                            //else
                            //    obj_grievanceApplication.HaveyouReceivedNOC = NOCAP.BLL.Common.CommonEnum.FlagYesNo.No;

                            if (txtNOCNumber.Text != "")
                                obj_grievanceApplication.NOCNumber = txtNOCNumber.Text;

                            switch (rbtnSubmittedTo.SelectedValue)
                            {
                                case "HQ":
                                    obj_grievanceApplication.SubmittedOfficeLevel = GrievanceApplication.SubmittedToOption.HQ;
                                    break;
                                case "Region":
                                    obj_grievanceApplication.SubmittedOfficeLevel = GrievanceApplication.SubmittedToOption.Region;
                                    break;

                                default:
                                    if (txtQuantum.Text != "")
                                    {
                                        if (Convert.ToInt64(txtQuantum.Text) <= 100)
                                        {
                                            obj_grievanceApplication.SubmittedOfficeLevel = GrievanceApplication.SubmittedToOption.Region;
                                            obj_grievanceApplication.Quantum = Convert.ToDecimal(txtQuantum.Text);
                                            break;
                                        }
                                        else
                                        {
                                            obj_grievanceApplication.SubmittedOfficeLevel = GrievanceApplication.SubmittedToOption.HQ;
                                            obj_grievanceApplication.Quantum = Convert.ToDecimal(txtQuantum.Text);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        obj_grievanceApplication.SubmittedOfficeLevel = GrievanceApplication.SubmittedToOption.NotDefined;
                                        break;
                                    }
                            }

                            obj_grievanceApplication.Remark = txtRemark.Text;
                            obj_grievanceApplication.CreatedByExUC = Convert.ToInt64(Session["ExternalUserCode"]); ;
                            int int_result = obj_grievanceApplication.Add();
                            if (int_result == 1)
                            {
                                lblMessage.Text = obj_grievanceApplication.CustumMessage;
                                lblMessage.ForeColor = System.Drawing.Color.Green;
                                ClearControls();


                            }
                            else
                            {
                                strStatus = "Record Adding Failed";
                                lblMessage.Text = obj_grievanceApplication.CustumMessage;
                                lblMessage.ForeColor = System.Drawing.Color.Red;

                            }
                        }
                        else
                        {
                            strStatus = "File Upload Failed";
                            lblFileUploadMessage.Text = "File can not upload. It has more than 20 MB size";
                            lblFileUploadMessage.ForeColor = System.Drawing.Color.Red;
                            lblMessage.Text = "";

                        }
                    }
                    else
                    {
                        strStatus = "File Upload Failed";
                        lblFileUploadMessage.Text = "Not a valid file!!..Select an other file!!";
                        lblFileUploadMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = "";

                    }

                }
                else
                {
                    strStatus = "File Upload Failed";
                    lblFileUploadMessage.Text = "Not a valid file!!..Select an other file!!";
                    lblFileUploadMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "";
                }

            }
            else
            {
                lblFileUploadMessage.Text = "Please select a file..!!";
                lblFileUploadMessage.ForeColor = System.Drawing.Color.Red;
               
            }

        }
        catch (Exception ex)
        {

        }
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
            Response.Redirect("~/InternalErrorPage.aspx", false);
            return 0;
        }

    }








    //protected void btnUploadSitePlan_Click(object sender, EventArgs e)
    //{
    //    if (Page.IsValid)
    //    {
    //        try
    //        {
    //            NOCAP.BLL.Grievance.GrievanceAttachmentExt obj_GrievanceAttachment = new NOCAP.BLL.Grievance.GrievanceAttachmentExt(Convert.ToInt64(lblGrievanceCode.Text));

    //            string str_fname;
    //            string str_ext;
    //            // RelaxationAttachmentBLL obj_RelaxationApplicationForNoLimit = new RelaxationAttachmentBLL();
    //            byte[] buffer = new byte[1];
    //            if (FileUploadSitePlan.HasFile)
    //            {
    //                long GrievanceCode = obj_GrievanceAttachment.GrievanceCode;
    //                str_ext = System.IO.Path.GetExtension(FileUploadSitePlan.PostedFile.FileName).ToLower();
    //                str_fname = FileUploadSitePlan.FileName;
    //                if (str_ext == ".txt" || str_ext == ".doc" || str_ext == ".docx" || str_ext == ".jpg" || str_ext == ".jpeg" || str_ext == ".pdf")
    //                {
    //                    if (NOCAPExternalUtility.IsValidFile(FileUploadSitePlan.PostedFile))
    //                    {
    //                        if (FileUploadSitePlan.PostedFile.ContentLength < AttachmentSizeLimit())
    //                        {
    //                            obj_GrievanceAttachment.AttFile = NOCAPExternalUtility.StreamFile(FileUploadSitePlan.PostedFile);
    //                            obj_GrievanceAttachment.ContentType = FileUploadSitePlan.PostedFile.ContentType;
    //                            obj_GrievanceAttachment.AttPath = FileUploadSitePlan.FileName;
    //                            obj_GrievanceAttachment.FileExtension = str_ext;
    //                            obj_GrievanceAttachment.GrievanceCode = GrievanceCode;
    //                            obj_GrievanceAttachment.AttName = txtRelaxation.Text;
    //                        }
    //                        else
    //                        {
    //                            strStatus = "File Upload Failed";
    //                            lblMessage.Text = "File can not upload. It has more than 5 MB size";
    //                            lblMessage.ForeColor = System.Drawing.Color.Red;
    //                            return;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        strStatus = "File Upload Failed";
    //                        lblMessage.Text = "Not a valid file!!..Select an other file!!";
    //                        lblMessage.ForeColor = System.Drawing.Color.Red;
    //                        return;
    //                    }

    //                }
    //                else
    //                {
    //                    strStatus = "File Upload Failed";
    //                    lblMessage.Text = "Not a valid file!!..Select an other file!!";
    //                    lblMessage.ForeColor = System.Drawing.Color.Red;
    //                    return;
    //                }

    //            }
    //            else
    //            {
    //                lblMessage.Text = "Please select a file..!!";
    //                lblMessage.ForeColor = System.Drawing.Color.Red;
    //                return;
    //            }

    //            obj_GrievanceAttachment.CreatedByUC = Convert.ToInt32(Session["ExternalUserCode"]); ;


    //            if (obj_GrievanceAttachment.Add() == 1)
    //            {
    //                strStatus = "File Upload Success";
    //                lblMessage.Text = obj_GrievanceAttachment.CustumMessage;
    //                lblMessage.ForeColor = System.Drawing.Color.Green;
    //                txtRelaxation.Text = "";
    //                BindGridView(Convert.ToInt64(lblGrievanceCode.Text), obj_GrievanceAttachment.SN);
    //            }
    //            else
    //            {
    //                strStatus = "File Upload Failed";
    //                lblMessage.Text = obj_GrievanceAttachment.CustumMessage;
    //                lblMessage.ForeColor = System.Drawing.Color.Red;
    //            }

    //            lblMessage.Text = obj_GrievanceAttachment.CustumMessage;

    //        }
    //        catch (Exception Ex)
    //        {
    //            Response.Redirect("~/InternalErrorPage.aspx", false);
    //        }
    //        finally
    //        {

    //        }
    //    }
    //}


    //private void BindGridView(long lblGrievanceCode, int SN)
    //{

    //    NOCAP.BLL.Grievance.GrievanceAttachmentExt obj_GrievanceAttachment = new NOCAP.BLL.Grievance.GrievanceAttachmentExt();
    //    obj_GrievanceAttachment.GrievanceCode = lblGrievanceCode;
    //    obj_GrievanceAttachment.SN = SN;
    //    gvGrievanceAtt.DataSource = obj_GrievanceAttachment.GetAll();
    //    gvGrievanceAtt.DataBind();
    //}

    //protected void gvAttachments_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    try
    //    {
    //        strActionName = "Delete File Grievance Attachments";
    //        if (DeleteAttchment((GridView)sender, e, lblMessage, strActionName) == 1)
    //        {

    //        }

    //    }
    //    catch (Exception)
    //    {
    //        Response.Redirect("~/ExternalErrorPage.aspx", false);
    //    }

    //}


    //private int DeleteAttchment(GridView gv, GridViewDeleteEventArgs e, Label lblMessage, string str_ActionName)
    //{
    //    try
    //    {
    //        long lng_GrievanceCode = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["GrievanceCode"]);
    //        int int_SN = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["SN"]);
    //        lblMessage.ForeColor = System.Drawing.Color.Red;

    //        NOCAP.BLL.Grievance.GrievanceAttachmentExt obj_GrievanceAttachment = new NOCAP.BLL.Grievance.GrievanceAttachmentExt(lng_GrievanceCode, int_SN);
    //        if (obj_GrievanceAttachment.Delete() == 1)
    //        {
    //            lblMessage.Text = obj_GrievanceAttachment.CustumMessage;
    //            strStatus = "File Delete Success";
    //            BindGridView(Convert.ToInt64(lblGrievanceCode.Text), obj_GrievanceAttachment.SN);
    //            return 1;
    //        }
    //        else
    //        {
    //            lblMessage.Text = obj_GrievanceAttachment.CustumMessage;
    //            strStatus = "File Delete Failed";
    //            return 0;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        return 0;
    //    }
    //}
}