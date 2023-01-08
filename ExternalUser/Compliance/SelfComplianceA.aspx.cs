using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using System.Threading;
public partial class ExternalUser_Compliance_SelfComplianceA : System.Web.UI.Page
{
    string strPageName = "SelfCompliance";
    string strActionName = "";
    string strStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                ValidationExepInit();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;

                if (PreviousPage != null)
                {
                    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");

                    if (placeHolder != null)
                    {
                        Label lblApplicationRenewCode = (Label)placeHolder.FindControl("lblApplicationRenewCode");
                        if (lblApplicationRenewCode != null && lblApplicationRenewCode.Text != "")
                            lblApplicationCodeFrom.Text = HttpUtility.HtmlEncode(lblApplicationRenewCode.Text);
                        else
                        {


                            Label SourceLabel = (Label)placeHolder.FindControl("lblApplicationCode");
                            if (SourceLabel != null && SourceLabel.Text != "")
                                lblApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);
                        }
                        Label SourceLabelPreviousPage = (Label)placeHolder.FindControl("lblApplicationCodeFrom");
                        if (SourceLabelPreviousPage != null && SourceLabelPreviousPage.Text != "")
                            lblApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabelPreviousPage.Text);
                        if (lblApplicationCodeFrom.Text.Trim() != "")
                        {
                            GetINDINFMINDetails(Convert.ToInt64(lblApplicationCodeFrom.Text));
                            PopulateData(Convert.ToInt64(lblApplicationCodeFrom.Text), sender, e);

                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblModeFrom.Text = "";
            lblPageTitleFrom.Text = "";
            lblApplicationCodeFrom.Text = "";
        }

    }
    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
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



                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                    if (obj_selfCompliance.ApplicationCode == 0)
                    {
                        if (AddData(sender, e) == 1)
                        {
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Record Saved Successfully');", true);
                        }
                    }
                    else
                    {
                        if (UpdateData(Convert.ToInt64(lblApplicationCodeFrom.Text), sender, e) == 1)
                        {

                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Record Updated Successfully');", true);
                        }
                    }
                    PopulateData(Convert.ToInt64(lblApplicationCodeFrom.Text), sender, e);
                }
                catch (Exception)
                {
                    Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
                }
            }
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
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
                    int status = 0;
                    NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(Convert.ToInt64(lblApplicationCodeFrom.Text));
                    if (obj_selfCompliance.ApplicationCode == 0)
                    {
                        status = AddData(sender, e);
                    }
                    else
                    {
                        status = UpdateData(Convert.ToInt64(lblApplicationCodeFrom.Text), sender, e);
                    }
                    if (status == 1)
                    {
                        Server.Transfer("~/ExternalUser/Compliance/SelfComplianceB.aspx");
                    }
                }
                catch (ThreadAbortException)
                {

                }
                catch (Exception ex)
                {
                    Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
                }
            }
        }
    }

    #region Private Method
    private void BindGridView(GridView gv, long lngA_ApplicationCode)
    {
        NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(lngA_ApplicationCode);
        NOCAP.BLL.Misc.Compliance.SelfComplianceAttachment[] arr_SelfComplianceAttachment = null;



        if (gv.ID == "gvNOC")
            arr_SelfComplianceAttachment = obj_selfCompliance.GetNOCAttachmentList();
        //else if (gv.ID == "gvSourceofAvailabilityofSurfaceWater")
        //    arr_industrialNewApplicationAttachmentList = obj_industrialNewApplication.GetSourceofAvailabilityofSurfaceWaterAttachmentList();

        lblNOCCount.Text = arr_SelfComplianceAttachment.Length.ToString();
        gv.DataSource = arr_SelfComplianceAttachment;
        gv.DataBind();
    }

    private void PopulateData(long lngA_ApplicationCode, object sender, EventArgs e)
    {
        try
        {
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null;
            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = null;
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = null;
            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = null;
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = null;
            NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = null;
            NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_InfrastructureNewApplication, out obj_MiningNewApplication, out obj_IndustrialRenewApplication, out obj_InfrastructureRenewApplication, out obj_MiningRenewApplication, lngA_ApplicationCode);
            if (obj_IndustrialNewApplication != null)
            {
                NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_IndustrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter(obj_IndustrialNewApplication.IndustrialNewApplicationCode);

                txtNOCNo.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialNewApplication.NOCNumber));
                txtNOCStartDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_IndustrialNewIssusedLetter.ValidityStartDate).ToString("dd/MM/yyyy"));
                txtNOCEndDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_IndustrialNewIssusedLetter.ValidityEndDate).ToString("dd/MM/yyyy"));


                txtQtyAbstractionPerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialNewIssusedLetter.GroundWaterAbsRecommToApprPerDay));


                //txtQtyDewateringPerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialNewIssusedLetter.GroundWater));
                txtQtyAbstractionPerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialNewIssusedLetter.GroundWaterAbsRecommToApprPerYear));
                // txtQtyDewateringPerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialNewIssusedLetter.GroundWaterDewYearAppr));


            }
            else if (obj_IndustrialRenewApplication != null)
            {
                
                NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter obj_IndustrialRenewIssusedLetter = new NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter(obj_IndustrialRenewApplication.IndustrialRenewApplicationCode);

                txtNOCNo.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialRenewApplication.NOCNumber));
                txtNOCStartDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_IndustrialRenewIssusedLetter.ValidityStartDate).ToString("dd/MM/yyyy"));
                txtNOCEndDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_IndustrialRenewIssusedLetter.ValidityEndDate).ToString("dd/MM/yyyy"));


                txtQtyAbstractionPerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialRenewIssusedLetter.GroundWaterAbsRecommToApprPerDay));


                //txtQtyDewateringPerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialNewIssusedLetter.GroundWater));
                txtQtyAbstractionPerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialRenewIssusedLetter.GroundWaterAbsRecommToApprPerYear));
                // txtQtyDewateringPerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialNewIssusedLetter.GroundWaterDewYearAppr));

            }
            else if (obj_InfrastructureNewApplication != null)
            {
                NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_InfrastructureNewIssusedLetter = new NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter(lngA_ApplicationCode);

                txtNOCNo.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_InfrastructureNewApplication.NOCNumber));
                txtNOCStartDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_InfrastructureNewIssusedLetter.ValidityStartDate).ToString("dd/MM/yyyy"));
                txtNOCEndDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_InfrastructureNewIssusedLetter.ValidityEndDate).ToString("dd/MM/yyyy"));


                txtQtyAbstractionPerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_InfrastructureNewIssusedLetter.GroundWaterAbsRecommToApprPerDay));


                txtQtyDewateringPerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_InfrastructureNewIssusedLetter.GroundWaterAbsRecommToApprDewaterPerDay));
                txtQtyAbstractionPerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_InfrastructureNewIssusedLetter.GroundWaterAbsRecommToApprPerYear));
                // txtQtyDewateringPerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialNewIssusedLetter.GroundWaterDewYearAppr));

            }
            else if (obj_InfrastructureRenewApplication != null)
            {
                NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter obj_InfrastructureRenewIssusedLetter = new NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter(lngA_ApplicationCode);

                txtNOCNo.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_InfrastructureRenewApplication.NOCNumber));
                txtNOCStartDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_InfrastructureRenewIssusedLetter.ValidityStartDate).ToString("dd/MM/yyyy"));
                txtNOCEndDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_InfrastructureRenewIssusedLetter.ValidityEndDate).ToString("dd/MM/yyyy"));


                txtQtyAbstractionPerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_InfrastructureRenewIssusedLetter.GroundWaterAbsRecommToApprPerDay));

               // txtQtyDewateringPerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_InfrastructureRenewIssusedLetter.GroundWaterAbsRecommToApprDewaterPerDay));
                txtQtyAbstractionPerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_InfrastructureRenewIssusedLetter.GroundWaterAbsRecommToApprPerYear));
                // txtQtyDewateringPerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialNewIssusedLetter.GroundWaterDewYearAppr));
            }
            else if (obj_MiningNewApplication != null)
            {
                NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter obj_MiningNewIssusedLetter = new NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter(lngA_ApplicationCode);

                txtNOCNo.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.NOCNumber));
                txtNOCStartDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_MiningNewIssusedLetter.ValidityStartDate).ToString("dd/MM/yyyy"));
                txtNOCEndDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_MiningNewIssusedLetter.ValidityEndDate).ToString("dd/MM/yyyy"));


                txtQtyAbstractionPerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewIssusedLetter.GroundWaterAbsRecommToApprPerDay));

                 txtQtyDewateringPerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewIssusedLetter.GroundWaterAbsRecommToApprSeepDewaterPerDay));
                txtQtyAbstractionPerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewIssusedLetter.GroundWaterAbsRecommToApprPerYear));
                 txtQtyDewateringPerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewIssusedLetter.GroundWaterSeepDewYearAppr));
            }
            else if (obj_MiningRenewApplication != null)
            {
                NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter obj_MiningRenewIssusedLetter = new NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter(lngA_ApplicationCode);

                txtNOCNo.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewApplication.NOCNumber));
                txtNOCStartDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_MiningRenewIssusedLetter.ValidityStartDate).ToString("dd/MM/yyyy"));
                txtNOCEndDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_MiningRenewIssusedLetter.ValidityEndDate).ToString("dd/MM/yyyy"));


                txtQtyAbstractionPerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewIssusedLetter.GroundWaterAbsRecommToApprPerDay));

                txtQtyDewateringPerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewIssusedLetter.GroundWaterAbsRecommToApprSeepDewaterPerDay));
                txtQtyAbstractionPerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewIssusedLetter.GroundWaterAbsRecommToApprPerYear));
                txtQtyDewateringPerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningRenewIssusedLetter.GroundWaterSeepDewYearAppr));
            }
            //NOCAP.BLL.Misc.Compliance.SelfCompliance obj_selfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(lngA_ApplicationCode);

            //if (obj_selfCompliance.ApplicationCode != 0)
            //{
            //    NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplication = null;
            //    NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = null;
            //    NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = null;
            //    NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = null;
            //    NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = null;
            //    NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = null;
            //    NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_IndustrialNewApplication, out obj_InfrastructureNewApplication, out obj_MiningNewApplication, out obj_IndustrialRenewApplication, out obj_InfrastructureRenewApplication, out obj_MiningRenewApplication, obj_selfCompliance.ApplicationCode);
            //    if(obj_IndustrialNewApplication!=null)
            //    {
            //        NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_IndustrialNewIssusedLetter = new NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter(obj_IndustrialNewApplication.IndustrialNewApplicationCode);

            //        txtNOCNo.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_selfCompliance.NOCNo));
            //        txtNOCStartDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_IndustrialNewIssusedLetter.ValidityStartDate).ToString("dd/MM/yyyy"));
            //        txtNOCEndDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(obj_IndustrialNewIssusedLetter.ValidityEndDate).ToString("dd/MM/yyyy"));


            //        txtQtyAbstractionPerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialNewIssusedLetter.GroundWaterAbsRecommToApprPerDay));


            //       //txtQtyDewateringPerDay.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialNewIssusedLetter.GroundWater));
            //        txtQtyAbstractionPerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialNewIssusedLetter.GroundWaterAbsRecommToApprPerYear));
            //       // txtQtyDewateringPerYear.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_IndustrialNewIssusedLetter.GroundWaterDewYearAppr));
            //    }


            //    // lblApplicationCode.Text = obj_selfCompliance.ApplicationCode.ToString();

            //}
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    private void ValidationExepInit()
    {

        //revtxtNocNumber.ValidationExpression = ValidationUtility.txtValForNOCNumber;
        //revtxtNocNumber.ErrorMessage = ValidationUtility.txtValForNOCNumberMsg;

        revtxtGroundWaterRequirementExist.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtGroundWaterRequirementExist.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtQtyDewateringPerDay.ValidationExpression = ValidationUtility.txtValForDecimalValue("6", "2");
        revtxtQtyDewateringPerDay.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("6", "2");
        revtxtQtyAbstractionPerYear.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtQtyAbstractionPerYear.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");
        revtxtQtyDewateringPerYear.ValidationExpression = ValidationUtility.txtValForDecimalValue("9", "2");
        revtxtQtyDewateringPerYear.ErrorMessage = ValidationUtility.txtValForDecimalValueMsg("9", "2");

    }
    private void GetINDINFMINDetails(long ApplicationCode)
    {

        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication();
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication();
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication();


        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication();
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication();
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication();



        NOCAP.BLL.Utility.Utility.GetAppplicationObjectForApplicationCode(out obj_industrialNewApplication, out obj_infrastructureNewApplication, out obj_miningNewApplication, out obj_industrialRenewApplication, out obj_infrastructureRenewApplication, out obj_miningRenewApplication, ApplicationCode);

        if (obj_industrialNewApplication != null)
        {
            GetIndustrialDetails(ApplicationCode);
            BindNewINDIssuedLetterDetails(ApplicationCode);
        }
        else if (obj_infrastructureNewApplication != null)
        {
            GetInfrastructureDetails(ApplicationCode);
            BindNewINFIssuedLetterDetails(ApplicationCode);
        }
        else if (obj_miningNewApplication != null)
        {
            GetMininingDetails(ApplicationCode);
            BindNewMINIssuedLetterDetails(ApplicationCode);
        }
        else if (obj_industrialRenewApplication != null)
        {
            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_IndustrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(ApplicationCode);
            if (obj_IndustrialRenewApplication != null)
                GetIndustrialRenewDetails(obj_IndustrialRenewApplication.IndustrialRenewApplicationCode);
            BindRenewINDIssuedLetterDetails(ApplicationCode);

        }
        else if (obj_infrastructureRenewApplication != null)
        {
            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(ApplicationCode);
            if (obj_InfrastructureRenewApplication != null)
                GetInfrastructureRenewDetails(obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode);
            BindRenewINFIssuedLetterDetails(ApplicationCode);
        }
        else if (obj_miningRenewApplication != null)
        {
            NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(ApplicationCode);
            if (obj_MiningRenewApplication != null)
                GetMininingRenewDetails(obj_MiningRenewApplication.MiningRenewApplicationCode);
            BindRenewMINIssuedLetterDetails(ApplicationCode);
        }
        else
        {
            lblMessage.Text = " Application Code does not exist.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Visible = true;
        }


    }
    private void GetIndustrialDetails(long ApplicationCode)
    {
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(ApplicationCode);
        NOCAP.BLL.Master.ApplicationTypeCategory obj_ApplicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_industrialNewApplication.ApplicationTypeCode, obj_industrialNewApplication.ApplicationTypeCategoryCode);
        NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
        NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);

        NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_IndustrialNewIssusedLetter = obj_industrialNewApplication.GetIssuedLetter();

        try
        {
           
            if (obj_industrialNewApplication != null)
            {


                lblApplicationCode.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.IndustrialNewApplicationCode);
                lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.IndustrialNewApplicationNumber);
                lblAppliedFor.Text = HttpUtility.HtmlEncode("Fresh");
                lblTypeOfProject.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.ApplicationType(obj_industrialNewApplication.ApplicationTypeCode).ApplicationTypeDescription);

                lblNameOfIndustry.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.NameOfIndustry);



            }
            
        }
        catch (Exception)
        {
            Response.Redirect("~/InternalErrorPage.aspx", false);
        }
        finally
        {
            obj_industrialNewApplication = null;
            obj_ApplicationTypeCategory = null;
            obj_District = null;
            obj_SubDistrict = null;
            obj_Town = null;
            obj_Village = null;
        }
    }
    private void GetIndustrialRenewDetails(long ApplicationCode)
    {
        NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(ApplicationCode);
        NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(obj_industrialRenewApplication.FirstApplicationCode);
        
        NOCAP.BLL.Master.ApplicationTypeCategory obj_ApplicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_industrialNewApplication.ApplicationTypeCode, obj_industrialNewApplication.ApplicationTypeCategoryCode);
        NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
        NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);

        NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter obj_IndustrialRenewIssusedLetter = obj_industrialRenewApplication.GetIssuedLetter();

        try
        {

            if (obj_industrialRenewApplication != null)
            {


                lblApplicationCode.Text = HttpUtility.HtmlEncode(obj_industrialRenewApplication.IndustrialRenewApplicationCode);
                lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.IndustrialNewApplicationNumber);
                lblAppliedFor.Text = HttpUtility.HtmlEncode("Renew (")+ HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(obj_industrialRenewApplication.LinkDepth)) + ")";


                lblTypeOfProject.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.ApplicationType(obj_industrialNewApplication.ApplicationTypeCode).ApplicationTypeDescription);

                lblNameOfIndustry.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.NameOfIndustry);



            }

        }
        catch (Exception)
        {
            Response.Redirect("~/InternalErrorPage.aspx", false);
        }
        finally
        {
            obj_industrialNewApplication = null;
            obj_ApplicationTypeCategory = null;
            obj_District = null;
            obj_SubDistrict = null;
            obj_Town = null;
            obj_Village = null;
        }
    }
    void BindRenewINDIssuedLetterDetails(long ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication obj_industrialRenewApplication = new NOCAP.BLL.Industrial.Renew.Application.IndustrialRenewApplication(ApplicationCode);
            NOCAP.BLL.Industrial.Renew.Letter.IndustrialRenewIssusedLetter obj_IndustrialRenewIssusedLetter = obj_industrialRenewApplication.GetIssuedLetter();
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_IndustrialNewApplicationFirst = obj_industrialRenewApplication.GetFirstIndustrialApplication();
            if (obj_IndustrialRenewIssusedLetter != null && obj_IndustrialRenewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey > 0)
            {
                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_IndustrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.StateCode, obj_IndustrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_IndustrialNewApplicationFirst.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_IndustrialRenewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);
                //lblCateOfBlock.Text = HttpUtility.HtmlEncode(obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc());
            }

        }
        catch (Exception)
        {
            Response.Redirect("~/InternalErrorPage.aspx", false);
        }
    }
    private void GetInfrastructureDetails(long ApplicationCode)
    {
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(ApplicationCode);
        NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_InfrastructureNewApplication.CommunicationAddress.StateCode, obj_InfrastructureNewApplication.CommunicationAddress.DistrictCode);
        NOCAP.BLL.Master.District obj_DistrictPro = new NOCAP.BLL.Master.District(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode);
        NOCAP.BLL.Master.ApplicationTypeCategory obj_ApplicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_InfrastructureNewApplication.ApplicationTypeCode, obj_InfrastructureNewApplication.ApplicationTypeCategoryCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.CommunicationAddress.StateCode, obj_InfrastructureNewApplication.CommunicationAddress.DistrictCode, obj_InfrastructureNewApplication.CommunicationAddress.SubDistrictCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrictPro = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
        NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);

        if (obj_InfrastructureNewApplication != null)
        {

            try
            {
                lblApplicationCode.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.InfrastructureNewApplicationCode);
                lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber);
                lblAppliedFor.Text = HttpUtility.HtmlEncode("Fresh");
                lblTypeOfProject.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.ApplicationType(obj_InfrastructureNewApplication.ApplicationTypeCode).ApplicationTypeDescription);

                lblNameOfIndustry.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.NameOfInfrastructure);
                //lblAddressLine1.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1);
                //lblAddressLine2.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2);
                //lblAddressLine3.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3);

                //if (obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode > 0)
                //    lblState.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.GetState().StateName));

                //if (obj_District.DistrictName != "")
                //    lblDistrict.Text = HttpUtility.HtmlEncode(obj_District.DistrictName);
                //if (obj_SubDistrict.SubDistrictName != "")
                //    lblSubDistrict.Text = HttpUtility.HtmlEncode(obj_SubDistrict.SubDistrictName);

                //if (obj_Town.TownName != "")
                //    lblVillageTown.Text = HttpUtility.HtmlEncode(obj_Town.TownName);
                //if (obj_Village.VillageName != "")
                //    lblVillageTown.Text = HttpUtility.HtmlEncode(obj_Village.VillageName);
            }
            catch (Exception)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);
            }
            finally
            {
                obj_InfrastructureNewApplication = null;
                obj_ApplicationTypeCategory = null;
                obj_District = null;
                obj_SubDistrict = null;
                obj_Town = null;
                obj_Village = null;
            }
        }
    }
    private void GetInfrastructureRenewDetails(long ApplicationCode)
    {
        NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_InfrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(ApplicationCode);
        NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(obj_InfrastructureRenewApplication.FirstApplicationCode);
        NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_InfrastructureNewApplication.CommunicationAddress.StateCode, obj_InfrastructureNewApplication.CommunicationAddress.DistrictCode);
        NOCAP.BLL.Master.District obj_DistrictPro = new NOCAP.BLL.Master.District(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode);
        NOCAP.BLL.Master.ApplicationTypeCategory obj_ApplicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_InfrastructureNewApplication.ApplicationTypeCode, obj_InfrastructureNewApplication.ApplicationTypeCategoryCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.CommunicationAddress.StateCode, obj_InfrastructureNewApplication.CommunicationAddress.DistrictCode, obj_InfrastructureNewApplication.CommunicationAddress.SubDistrictCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrictPro = new NOCAP.BLL.Master.SubDistrict(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
        NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);

        if (obj_InfrastructureRenewApplication != null)
        {

            try
            {
                lblApplicationCode.Text = HttpUtility.HtmlEncode(obj_InfrastructureRenewApplication.InfrastructureRenewApplicationCode);
                lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.InfrastructureNewApplicationNumber);
                lblAppliedFor.Text = HttpUtility.HtmlEncode("Renew (") + HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(obj_InfrastructureRenewApplication.LinkDepth))+")";
                lblTypeOfProject.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.ApplicationType(obj_InfrastructureNewApplication.ApplicationTypeCode).ApplicationTypeDescription);
                lblNameOfIndustry.Text = HttpUtility.HtmlEncode(obj_InfrastructureNewApplication.NameOfInfrastructure);
            }
            catch (Exception)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);
            }
            finally
            {
                obj_InfrastructureNewApplication = null;
                obj_ApplicationTypeCategory = null;
                obj_District = null;
                obj_SubDistrict = null;
                obj_Town = null;
                obj_Village = null;
            }
        }
    }
    private void GetMininingDetails(long ApplicationCode)
    {
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(ApplicationCode);
        NOCAP.BLL.Master.ApplicationTypeCategory obj_ApplicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_MiningNewApplication.ApplicationTypeCode, obj_MiningNewApplication.ApplicationTypeCategoryCode);
        NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_MiningNewApplication.CommunicationAddress.StateCode, obj_MiningNewApplication.CommunicationAddress.DistrictCode);
        NOCAP.BLL.Master.District obj_DistrictPro = new NOCAP.BLL.Master.District(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.CommunicationAddress.StateCode, obj_MiningNewApplication.CommunicationAddress.DistrictCode, obj_MiningNewApplication.CommunicationAddress.SubDistrictCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrictPro = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
        NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);

        if (obj_MiningNewApplication != null)
        {

            try
            {
                lblApplicationCode.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.ApplicationCode);
                lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.MiningNewApplicationNumber);
                lblAppliedFor.Text = HttpUtility.HtmlEncode("Fresh");
                lblTypeOfProject.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.ApplicationType(obj_MiningNewApplication.ApplicationTypeCode).ApplicationTypeDescription);

                lblNameOfIndustry.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.NameOfMining);
                //lblAddressLine1.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine1);
                //lblAddressLine2.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine2);
                //lblAddressLine3.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.AddressLine3);

                //if (obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode > 0)
                //    lblState.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.GetState().StateName));

                //if (obj_District.DistrictName != "")
                //    lblDistrict.Text = HttpUtility.HtmlEncode(obj_District.DistrictName);
                //if (obj_SubDistrict.SubDistrictName != "")
                //    lblSubDistrict.Text = HttpUtility.HtmlEncode(obj_SubDistrict.SubDistrictName);

                //if (obj_Town.TownName != "")
                //    lblVillageTown.Text = HttpUtility.HtmlEncode(obj_Town.TownName);
                //if (obj_Village.VillageName != "")
                //    lblVillageTown.Text = HttpUtility.HtmlEncode(obj_Village.VillageName);
            }
            catch (Exception)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);
            }
            finally
            {
                obj_MiningNewApplication = null;
                obj_ApplicationTypeCategory = null;
                obj_District = null;
                obj_SubDistrict = null;
                obj_Town = null;
                obj_Village = null;
            }
        }
    }
    private void GetMininingRenewDetails(long ApplicationCode)
    {
        NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_MiningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(ApplicationCode);
        NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(obj_MiningRenewApplication.FirstApplicationCode);
        NOCAP.BLL.Master.ApplicationTypeCategory obj_ApplicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_MiningNewApplication.ApplicationTypeCode, obj_MiningNewApplication.ApplicationTypeCategoryCode);
        NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_MiningNewApplication.CommunicationAddress.StateCode, obj_MiningNewApplication.CommunicationAddress.DistrictCode);
        NOCAP.BLL.Master.District obj_DistrictPro = new NOCAP.BLL.Master.District(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.CommunicationAddress.StateCode, obj_MiningNewApplication.CommunicationAddress.DistrictCode, obj_MiningNewApplication.CommunicationAddress.SubDistrictCode);
        NOCAP.BLL.Master.SubDistrict obj_SubDistrictPro = new NOCAP.BLL.Master.SubDistrict(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
        NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.TownCode);
        NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewApplication.ProposedLocation.ProposedLocationAddress.VillageCode);

        if (obj_MiningNewApplication != null)
        {

            try
            {
                lblApplicationCode.Text = HttpUtility.HtmlEncode(obj_MiningRenewApplication.MiningRenewApplicationCode);
                lblApplicationNo.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.MiningNewApplicationNumber);
                lblAppliedFor.Text = HttpUtility.HtmlEncode("Renew (") + HttpUtility.HtmlEncode(NOCAPExternalUtility.AddOrdinal(obj_MiningRenewApplication.LinkDepth)) + ")";
                lblTypeOfProject.Text = HttpUtility.HtmlEncode(new NOCAP.BLL.Master.ApplicationType(obj_MiningNewApplication.ApplicationTypeCode).ApplicationTypeDescription);

                lblNameOfIndustry.Text = HttpUtility.HtmlEncode(obj_MiningNewApplication.NameOfMining);
            }
            catch (Exception)
            {
                Response.Redirect("~/InternalErrorPage.aspx", false);
            }
            finally
            {
                obj_MiningNewApplication = null;
                obj_ApplicationTypeCategory = null;
                obj_District = null;
                obj_SubDistrict = null;
                obj_Town = null;
                obj_Village = null;
            }
        }
    }
    void BindNewINDIssuedLetterDetails(long ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication obj_industrialNewApplication = new NOCAP.BLL.Industrial.New.Application.IndustrialNewApplication(ApplicationCode);
            NOCAP.BLL.Industrial.New.Letter.IndustrialNewIssusedLetter obj_IndustrialNewIssusedLetter = obj_industrialNewApplication.GetIssuedLetter();
            if (obj_IndustrialNewIssusedLetter != null && obj_IndustrialNewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey > 0)
            {
                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_IndustrialNewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);
                // lblCateOfBlock.Text = HttpUtility.HtmlEncode(obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc());
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/InternalErrorPage.aspx", false);
        }
    }
    void BindNewINFIssuedLetterDetails(long ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_infrastructureNewApplication = new NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication(ApplicationCode);
            NOCAP.BLL.Infrastructure.New.Letter.InfrastructureNewIssusedLetter obj_InfrastructureNewIssusedLetter = obj_infrastructureNewApplication.GetIssuedLetter();
            if (obj_InfrastructureNewIssusedLetter != null && obj_InfrastructureNewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey > 0)
            {
                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_infrastructureNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureNewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);
                //lblCateOfBlock.Text = HttpUtility.HtmlEncode(obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc());
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/InternalErrorPage.aspx", false);
        }
    }
    void BindRenewINFIssuedLetterDetails(long ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication obj_infrastructureRenewApplication = new NOCAP.BLL.Infrastructure.Renew.Application.InfrastructureRenewApplication(ApplicationCode);

            NOCAP.BLL.Infrastructure.Renew.Letter.InfrastructureRenewIssusedLetter obj_InfrastructureRenewIssusedLetter = obj_infrastructureRenewApplication.GetIssuedLetter();
            NOCAP.BLL.Infrastructure.New.Application.InfrastructureNewApplication obj_InfrastructureNewApplicationFirst = obj_infrastructureRenewApplication.GetFirstInfrastructureApplication();
            if (obj_InfrastructureRenewIssusedLetter != null && obj_InfrastructureRenewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey > 0)
            {
                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_InfrastructureNewApplicationFirst.ProposedLocation.ProposedLocationAddress.StateCode, obj_InfrastructureNewApplicationFirst.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_InfrastructureNewApplicationFirst.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_InfrastructureRenewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);
                //lblCateOfBlock.Text = HttpUtility.HtmlEncode(obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc());
            }

        }
        catch (Exception)
        {
            Response.Redirect("~/InternalErrorPage.aspx", false);
        }
    }

    void BindNewMINIssuedLetterDetails(long ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_miningNewApplication = new NOCAP.BLL.Mining.New.Application.MiningNewApplication(ApplicationCode);
            NOCAP.BLL.Mining.New.Letter.MiningNewIssusedLetter obj_MiningNewIssusedLetter = obj_miningNewApplication.GetIssuedLetter();
            if (obj_MiningNewIssusedLetter != null && obj_MiningNewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey > 0)
            {
                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.StateCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_miningNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningNewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);
                //lblCateOfBlock.Text = HttpUtility.HtmlEncode(obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc());
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/InternalErrorPage.aspx", false);
        }
    }
    void BindRenewMINIssuedLetterDetails(long ApplicationCode)
    {
        try
        {
            NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication obj_miningRenewApplication = new NOCAP.BLL.Mining.Renew.Application.MiningRenewApplication(ApplicationCode);
            NOCAP.BLL.Mining.Renew.Letter.MiningRenewIssusedLetter obj_MiningRenewIssusedLetter = obj_miningRenewApplication.GetIssuedLetter();
            NOCAP.BLL.Mining.New.Application.MiningNewApplication obj_MiningNewApplicationFirst = obj_miningRenewApplication.GetFirstMiningApplication();

            if (obj_MiningRenewIssusedLetter != null && obj_MiningRenewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey > 0)
            {
                NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory obj_SubDistrictAreaTypeCategoryHistory = new NOCAP.BLL.Master.SubDistrictAreaTypeCategoryHistory(obj_MiningNewApplicationFirst.ProposedLocation.ProposedLocationAddress.StateCode, obj_MiningNewApplicationFirst.ProposedLocation.ProposedLocationAddress.DistrictCode, obj_MiningNewApplicationFirst.ProposedLocation.ProposedLocationAddress.SubDistrictCode, obj_MiningRenewIssusedLetter.IssueLetterTimeSubDistrictAreaCatKey);
                //lblCateOfBlock.Text = HttpUtility.HtmlEncode(obj_SubDistrictAreaTypeCategoryHistory.AreaTypeCategoryDesc());
            }

        }
        catch (Exception)
        {
            Response.Redirect("~/InternalErrorPage.aspx", false);
        }
    }


    private int AddData(object sender, EventArgs e)
    {
        try
        {
            strActionName = "AddSelfCompliance";
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance();
            obj_SelfCompliance.ApplicationCode = Convert.ToInt64(lblApplicationCodeFrom.Text.Trim());
            //if (lblNOCCount.Text == "0")
            //{
            //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('NOC should be attached');", true);
            //    return 0;
            //}
            if (SubmitData(ref obj_SelfCompliance) == 0)
                return 0;
            else
            {

                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                obj_SelfCompliance.CreatedByExUC = obj_externalUser.ExternalUserCode;

                obj_SelfCompliance.SubmittedFrom = NOCAP.BLL.Misc.Compliance.SelfCompliance.SubmittedFromOption.Web;

                if (obj_SelfCompliance.Add() == 1)
                {
                    strStatus = "Add Success";
                    lblMessage.Text = obj_SelfCompliance.CustumMessage;
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    return 1;
                }
                else
                {
                    strStatus = "Add Failed";
                    lblMessage.Text = obj_SelfCompliance.CustumMessage;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return 0;
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            return 0;
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
    private int UpdateData(long lngA_ApplicationCode, object sender, EventArgs e)
    {
        try
        {
            strActionName = "UpdateSelfCompliance";
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(lngA_ApplicationCode);
            if (lblNOCCount.Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('NOC should be attached');", true);
                return 0;
            }
            if (SubmitData(ref obj_SelfCompliance) == 0)
                return 0;
            else
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                obj_SelfCompliance.ModifiedByExUC = obj_externalUser.ExternalUserCode;

                obj_SelfCompliance.SubmittedFrom = NOCAP.BLL.Misc.Compliance.SelfCompliance.SubmittedFromOption.Web;


                if (obj_SelfCompliance.Update() == 1)
                {
                    strStatus = "Update Success";
                    lblMessage.Text = obj_SelfCompliance.CustumMessage;
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    return 1;
                }
                else
                {
                    strStatus = "Update Failed";
                    lblMessage.Text = obj_SelfCompliance.CustumMessage;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return 0;
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            return 0;
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

    private int SubmitData(ref NOCAP.BLL.Misc.Compliance.SelfCompliance refobj_SelfCompliance)
    {
        try
        {
            strActionName = "AddSelfCompliance";
            NOCAP.BLL.Misc.Compliance.SelfCompliance obj_SelfCompliance = new NOCAP.BLL.Misc.Compliance.SelfCompliance(refobj_SelfCompliance.ApplicationCode);
            if (obj_SelfCompliance.ApplicationCode == 0)
                obj_SelfCompliance.ApplicationCode = Convert.ToInt64(lblApplicationCodeFrom.Text.Trim());
            obj_SelfCompliance.NOCNo = txtNOCNo.Text.Trim();
            if (!NOCAPExternalUtility.IsValidDate(txtNOCStartDate.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Invalid Date');", true);
                return 0;
            }
            else
                obj_SelfCompliance.ValidityStartDate = Convert.ToDateTime(txtNOCStartDate.Text.Trim());

            if (!NOCAPExternalUtility.IsValidDate(txtNOCEndDate.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "alert('Invalid Date');", true);
                return 0;
            }
            else
                obj_SelfCompliance.ValidityEndDate = Convert.ToDateTime(txtNOCEndDate.Text.Trim());

            obj_SelfCompliance.ComplianceSubmitDate = DateTime.Now;
            obj_SelfCompliance.GroundWaterAbsDayAppr = Convert.ToDecimal(txtQtyAbstractionPerDay.Text.Trim());
            if (txtQtyDewateringPerDay.Text.Trim() != "")
                obj_SelfCompliance.GroundWaterDewDayAppr = Convert.ToDecimal(txtQtyDewateringPerDay.Text.Trim());
            obj_SelfCompliance.GroundWaterAbsYearAppr = Convert.ToDecimal(txtQtyAbstractionPerYear.Text.Trim());
            if (txtQtyDewateringPerYear.Text.Trim() != "")
                obj_SelfCompliance.GroundWaterDewYearAppr = Convert.ToDecimal(txtQtyDewateringPerYear.Text.Trim());
            // NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
            // obj_SelfCompliance.CreatedByExUC = obj_externalUser.ExternalUserCode;
            refobj_SelfCompliance = obj_SelfCompliance;
            return 1;
        }
        catch (Exception)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            return 0;
        }
    }
    #endregion

    
    protected void lbtnViewFile_Click(object sender, CommandEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            try
            {

                if (e.CommandArgument != null)
                {
                    LinkButton btn = (LinkButton)sender;
                    string[] CommandArgument = btn.CommandArgument.Split(',');
                    long lng_applicationCode = Convert.ToInt64(CommandArgument[0]);
                    int int_attachmentCode = Convert.ToInt32(CommandArgument[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(CommandArgument[2]);
                    NOCAPExternalUtility.SelfcompDownloadFiles(lng_applicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);
                }
            }

            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;
            }
        }
    }



}
