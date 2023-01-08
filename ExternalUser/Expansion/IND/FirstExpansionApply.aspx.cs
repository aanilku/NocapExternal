using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Threading;

public partial class ExternalUser_Expansion_IND_FirstExpansionApply : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;

                if (NOCAPExternalUtility.FillDropDownApplicationTypeBaseApplicationNumbar(ref ddlApplicatonNumber, 2, Convert.ToInt32(Session["ExternalUserCode"])) != 1)
                {
                    Response.Write("Problem in Application Type ");
                }

                try
                {


                    if (PreviousPage != null)
                    {
                        Control placeHolder =
                            PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                        if (placeHolder != null)
                        {
                            Label SourceLabel =
                            (Label)placeHolder.FindControl("lblMode");

                            if (SourceLabel != null)
                            {
                                lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);  // add html encode

                            }

                            Label SourceLabelPreviousPage = (Label)placeHolder.FindControl("lblModeFrom");

                            if (SourceLabelPreviousPage != null)
                            {
                                lblModeFrom.Text = HttpUtility.HtmlEncode(SourceLabelPreviousPage.Text);  // add html encode

                            }
                        }
                    }

                    if (PreviousPage != null)
                    {
                        Control placeHolder =
                            PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                        if (placeHolder != null)
                        {
                            Label SourceLabel =
                            (Label)placeHolder.FindControl("lblPageTitle");
                            if (SourceLabel != null)
                            {
                                lblPageTitleFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text); // add html encode

                            }
                        }

                    }

                    if (PreviousPage != null)
                    {
                        Control placeHolder =
                            PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                        if (placeHolder != null)
                        {
                            Label SourceLabel =
                            (Label)placeHolder.FindControl("lblApplicationCode");
                            if (SourceLabel != null)
                            {
                                lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);  //add html encode

                            }
                            Label SourceLabelPreviousPage = (Label)placeHolder.FindControl("lblIndustialApplicationCodeFrom");

                            if (SourceLabelPreviousPage != null)
                            {
                                lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabelPreviousPage.Text);  //add html encode

                            }
                        }

                    }


                }
                catch (Exception ex)
                {
                    lblModeFrom.Text = "";
                    lblPageTitleFrom.Text = "";
                    lblIndustialApplicationCodeFrom.Text = "";

                }

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
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
                    lblIndustialApplicationCodeFrom.Text = Convert.ToString(obj_IndustrialNewApplication.IndustrialNewApplicationCode);
                    txtApplicationName.Text = obj_IndustrialNewApplication.NameOfIndustry;
                    txtApplicationNo.Text = Convert.ToString(obj_IndustrialNewApplication.IndustrialNewApplicationNumber);
                    txtNocNo.Text = Convert.ToString(obj_IndustrialNewApplication.NOCNumber);

                }


                //   ddlApplicatonNumber.Enabled = true;

            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = "Please select Application Type.";
        }


    }

    protected void btnShowDetails_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {
                int statusCode = 0;
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
                try
                {
                    if (lblIndustialApplicationCodeFrom.Text != "")
                    {
                        NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication Obj_INDExpansionSADApp = new NOCAP.BLL.Industrial.Expansion.IndustrialExpansionApplication();

                        NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                        Obj_INDExpansionSADApp.CreatedByExUC = obj_externalUser.ExternalUserCode;

                        if (Obj_INDExpansionSADApp.AllDataTransferINDApplicationToINDExpansionSADApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text), obj_externalUser.ExternalUserCode) == 1)
                        {

                            statusCode = 1;
                        } 
                        else
                        {
                            lblMessage.Text = "All Ready Apply You Can Move Applicant Home Edit";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }

                }              
                catch (Exception ex)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
                }



                if (statusCode ==1 )
                {

                    hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                    ViewState["CSRF"] = hidCSRF.Value;

                    if (lblIndustialApplicationCodeFrom.Text.Trim() != "")
                    {
                        Server.Transfer("~/ExternalUser/Expansion/IND/IndustrialExpansion.aspx");

                        //   Server.Transfer("~/ExternalUser/Expansion/IND/IndustrialExpansion.aspx");
                    }
                }
                
            }
        }
    }

}