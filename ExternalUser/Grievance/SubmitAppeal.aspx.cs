using NOCAP.BLL.Grievance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExternalUser_Grievance_SubmitAppeal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRF"] = hidCSRF.Value;
            if (NOCAPExternalUtility.FillDropDownState(ref ddlState) != 1)
            {
                //lblMessage.Text = "Problem in state population";
                //lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            if (PreviousPage != null)
            {
                Control placeHolder =
                    PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                if (placeHolder != null)
                {
                    Label SourceLabel =
                    (Label)placeHolder.FindControl("lblGrievanceCode");
                    if (SourceLabel != null)
                    {
                        lblGrievanceCode.Text = HttpUtility.HtmlEncode(SourceLabel.Text);  //add html encode

                    }
                    Label SourceLabelPreviousPage = (Label)placeHolder.FindControl("lblGrievanceCode");

                    if (SourceLabelPreviousPage != null)
                    {
                        lblGrievanceCode.Text = HttpUtility.HtmlEncode(SourceLabelPreviousPage.Text);  //add html encode
                        
                    }
                }

            }            

            if (NOCAPExternalUtility.FillDropDownState(ref ddlState) != 1)
            {
                //lblMessage.Text = "Problem in state population";
                //lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            BindAllControls(Convert.ToInt64(lblGrievanceCode.Text));
        }
    }
    void BindAllControls(long lngA_GrievanceCode)
    {
        GrievanceApplication obj_grievanceApplication = new GrievanceApplication(lngA_GrievanceCode);
        if(obj_grievanceApplication!=null && obj_grievanceApplication.GrievanceCode>0)
        {
            ddlState.ClearSelection();
            ddlState.Items.FindByValue(obj_grievanceApplication.StateCode.ToString()).Selected=true;
            ddlTypeofGrievance.ClearSelection();
            ddlTypeofGrievance.Items.FindByValue(obj_grievanceApplication.GrievanceType.ToString()).Selected = true;

            switch (obj_grievanceApplication.HaveyouReceivedNOC)
            {
                case NOCAP.BLL.Common.CommonEnum.FlagYesNo.Yes:
                    rbtnHaveYouReceivedNOC.SelectedValue = "Y";
                    break;
                case NOCAP.BLL.Common.CommonEnum.FlagYesNo.No:
                    rbtnHaveYouReceivedNOC.SelectedValue = "N";
                    break; 
            }
            switch (obj_grievanceApplication.SubmittedOfficeLevel)
            {
                case GrievanceApplication.SubmittedToOption.HQ:
                    rbtnSubmittedTo.SelectedValue = "H";
                    break;
                case GrievanceApplication.SubmittedToOption.Region:
                    rbtnSubmittedTo.SelectedValue = "R";
                    break;
            } 
            txtNOCNumber.Text = obj_grievanceApplication.NOCNumber;
            txtQuantum.Text =Convert.ToString(obj_grievanceApplication.Quantum);
            txtRemark.Text = obj_grievanceApplication.Remark;



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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            GrievanceApplication obj_grievanceApplication = new GrievanceApplication(Convert.ToInt64(lblGrievanceCode.Text));
            GrievanceAppeal obj_grievanceAppeal = new GrievanceAppeal(Convert.ToInt64(lblGrievanceCode.Text));            
            obj_grievanceAppeal.GrievanceCode= Convert.ToInt64(lblGrievanceCode.Text);
            switch(obj_grievanceApplication.SubmittedOfficeLevel)
            {
                case GrievanceApplication.SubmittedToOption.HQ:
                    obj_grievanceAppeal.SubmittedOfficeLevel = GrievanceAppeal.SubmittedToOption.HQ;
                    break;
                case GrievanceApplication.SubmittedToOption.Region:
                    obj_grievanceAppeal.SubmittedOfficeLevel = GrievanceAppeal.SubmittedToOption.Region;
                    break;
                case GrievanceApplication.SubmittedToOption.NotDefined:
                    obj_grievanceAppeal.SubmittedOfficeLevel = GrievanceAppeal.SubmittedToOption.NotDefined;
                    break;
            }             
              
            obj_grievanceAppeal.Remark = txtRemarkAppeal.Text;         
            obj_grievanceAppeal.CreatedByExUC = Convert.ToInt64(Session["ExternalUserCode"]);
            int int_result = obj_grievanceAppeal.AddAppeal();
            if (int_result == 1)
            {
                lblMessage.Text = obj_grievanceAppeal.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Green;
                txtRemark.Text = "";
            }
            else
            {
                lblMessage.Text = obj_grievanceAppeal.CustumMessage;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }            
        }
        catch (Exception ex)
        {

        }
    }
}