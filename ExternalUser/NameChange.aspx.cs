using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExternalUser_NameChange : System.Web.UI.Page
{
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
                //if (NOCAPExternalUtility.FillDropDownApplicationType(ref ddlApplicationType) != 1)
                //{
                //    Response.Write("Problem in Application Type ");
                //}
              
            }
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    


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

                if (obj_IndustrialNewApplication!=null && obj_IndustrialNewApplication.CreatedByExUC>0)
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }

    
}