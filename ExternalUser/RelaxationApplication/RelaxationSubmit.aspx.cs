using NOCAP.BLL.UserManagement;
using NOCAP.DAL.UserManagement;
using Relaxation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExternalUser_RelaxationApplication_RelaxationSubmit : System.Web.UI.Page
{
    string strActionName = "";
    string strStatus = "";
    string strPageName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            try
            {
                
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
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
                        else
                        {
                            SourceLabel = (Label)placeHolder.FindControl("lblApplicationCodeFrom");
                            if (SourceLabel != null)
                            {
                                lblIndustialApplicationCodeFrom.Text = HttpUtility.HtmlEncode(SourceLabel.Text);

                            }

                        }
                    }


                }
                bindDetails();
                //BindSelfDeclarationDetails();
                //DisplayApplicationStop();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
        }
    }

    protected void bindDetails()
    {
        try
        {
            if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                Session["CSRF"] = hidCSRF.Value;
                RelaxationApplication obj_relaxationApplication = new RelaxationApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));

                if (obj_relaxationApplication != null && obj_relaxationApplication.ApplicationCode > 0)
                {
                    if (obj_relaxationApplication.Submitted == RelaxationApplication.SubmittedOption.Yes)
                    {
                        btnSubmit.Enabled = false;
                        btnPrev.Enabled = false;
                        btnPrint.Visible = true;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                        btnPrev.Enabled = true;
                        btnPrint.Visible = false;
                    }

                    // NOCAP.BLL.Master.ApplicationTypeCategory obj_ApplicationTypeCategory = new NOCAP.BLL.Master.ApplicationTypeCategory(obj_industrialNewApplication.ApplicationTypeCode, obj_industrialNewApplication.ApplicationTypeCategoryCode);
                    NOCAP.BLL.Master.State obj_State = new NOCAP.BLL.Master.State(obj_relaxationApplication.StateCode);
                    NOCAP.BLL.Master.District obj_District = new NOCAP.BLL.Master.District(obj_relaxationApplication.StateCode, obj_relaxationApplication.DistrictCode);
                    NOCAP.BLL.Master.SubDistrict obj_SubDistrict = new NOCAP.BLL.Master.SubDistrict(obj_relaxationApplication.StateCode, obj_relaxationApplication.DistrictCode, obj_relaxationApplication.SubDistrictCode);
                    NOCAP.BLL.Master.Town obj_Town = new NOCAP.BLL.Master.Town(obj_relaxationApplication.StateCode, obj_relaxationApplication.DistrictCode, obj_relaxationApplication.SubDistrictCode, obj_relaxationApplication.TownCode);
                    NOCAP.BLL.Master.Village obj_Village = new NOCAP.BLL.Master.Village(obj_relaxationApplication.StateCode, obj_relaxationApplication.DistrictCode, obj_relaxationApplication.SubDistrictCode, obj_relaxationApplication.VillageCode);


                    // lblNetGroundWaterRequirement.Text = HttpUtility.HtmlEncode(Convert.ToString(obj_relaxationApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirement + obj_relaxationApplication.IndustrialNewWaterRequirementRecycledUsesDetail.GroundWaterRequirementExist));
                    lblNameOfIndustry.Text = HttpUtility.HtmlEncode(obj_relaxationApplication.NameOfIndustry);
                    lblUIDNumber.Text = HttpUtility.HtmlEncode(obj_relaxationApplication.UIDNumber);

                    if (obj_relaxationApplication.StateCode > 0)
                        lblState.Text = HttpUtility.HtmlEncode(obj_State.StateName);
                    //lblSubDistrict.Text = Convert.ToString(obj_industrialNewApplication.ProposedLocation.ProposedLocationAddress.SubDistrictCode);
                    switch (obj_relaxationApplication.MSME)
                    {
                        case RelaxationApplication.MSMEYesNo.Yes:
                            lblMSME.Text = "Yes";
                            break;
                        case RelaxationApplication.MSMEYesNo.No:
                            lblMSME.Text = "No";
                            break;
                        case RelaxationApplication.MSMEYesNo.NotDefine:
                            lblMSME.Text = "Not Define";
                            break;
                        default:
                            break;
                    }
                    switch (obj_relaxationApplication.WetLandArea)
                    {
                        case RelaxationApplication.WetLandAreaYesNo.Yes:
                            lblWetlandarea.Text = "Yes";
                            break;
                        case RelaxationApplication.WetLandAreaYesNo.No:
                            lblWetlandarea.Text = "No";
                            break;
                        case RelaxationApplication.WetLandAreaYesNo.NotDefine:
                            lblWetlandarea.Text = "Not Define";
                            break;
                        default:
                            break;
                    }


                    lblAmount.Text =Convert.ToString(obj_relaxationApplication.PayMentAmount);
                     lblTransactionRefNo.Text= Convert.ToString(obj_relaxationApplication.BharatTransReferanceNumber);
                    lblBharatTransDated.Text = Convert.ToString(Convert.ToDateTime(obj_relaxationApplication.BharatTransDated).ToShortDateString());


                    //lblAreaTypeCatagory.Text = HttpUtility.HtmlEncode(obj_industrialNewApplication.GetApplySubDistrictAreaTypeCategoryHistory().AreaTypeCategoryDesc());
                    if (obj_District.DistrictName != "")
                        lblDistrict.Text = HttpUtility.HtmlEncode(obj_District.DistrictName);
                    if (obj_SubDistrict.SubDistrictName != "")
                        lblSubDistrict.Text = HttpUtility.HtmlEncode(obj_SubDistrict.SubDistrictName);

                    if (obj_Town.TownName != "")
                        lblVillageTown.Text = HttpUtility.HtmlEncode(obj_Town.TownName);
                    if (obj_Village.VillageName != "")
                        lblVillageTown.Text = HttpUtility.HtmlEncode(obj_Village.VillageName);
                }
                else
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
                }
            }

        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }

    protected void btnPrev_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;
            Server.Transfer("RelaxationAttched.aspx");
        }
    }


    private int SubmitApplication(long lngA_ApplicationCode)
    {
        if (Page.IsValid)
        {
            try
            {
                strActionName = "Submit Application";
                RelaxationApplication obj_relaxationApplication = new RelaxationApplication(lngA_ApplicationCode);
                obj_relaxationApplication.Submitted = RelaxationApplication.SubmittedOption.Yes;

                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));
                obj_relaxationApplication.ModifiedByExUC =obj_externalUser.ExternalUserCode;
                 
                    if (obj_relaxationApplication.Update() == 1)
                    {
                        btnSubmit.Enabled = false;
                        btnPrev.Enabled = false;
                        btnPrint.Visible = true;
                        strStatus = "Update Success";
                        lblMessage.Text = "Successfully Saved";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        return 1;
                    }
                    else
                    {
                        btnPrint.Visible = false;
                        btnSubmit.Enabled = true;
                        btnPrev.Enabled = true;
                        lblMessage.Text = obj_relaxationApplication.CustumMessage;
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
        return 0;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
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
                try
                {
                    SubmitApplication(Convert.ToInt64(lblIndustialApplicationCodeFrom.Text));

                }
                catch (Exception)
                {
                    Response.Redirect("~/ExternalErrorPage.aspx", false);
                }
            }
        }

    }
}