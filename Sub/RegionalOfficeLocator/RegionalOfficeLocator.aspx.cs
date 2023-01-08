using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sub_RegionalOfficeLocator_RegionalOfficeLocator : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;

            if (NOCAPExternalUtility.FillDropDownState(ref ddlState) != 1)
            {
                lblMessage.Text = "Error In Getting state List";
            }
        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {

        try
        {

            NOCAP.BLL.UserManagement.ExternalUserLogIn obj_Login = new NOCAP.BLL.UserManagement.ExternalUserLogIn();


            if (obj_Login.ProccedNextToLogin(Convert.ToInt64(Session["ExternalUserCode"])) == 1)
            {
                if (obj_Login.ExternalUserCode < 1)
                {
                    //MasterPageFile = "~/ExternalUser/SubMasterPage.master";
                }
                else
                {
                    NOCAP.BLL.UserManagement.ExternalUser obj_exUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                    MasterPageFile = "~/ExternalUser/ExternalUserMaster.master";
                    //lblLoginMsg.Text = "Logged in as: " + obj_exUser.ExternalUserName;
                }

            }
            else
            {
                //MasterPageFile = "~/Sub/ApplicantRegistrationMaster.master";
            }
        }
        catch (Exception)
        {
            lblMessage.Text = "Problem in page";

        }
    }



    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        int int_StateCode = 0;
        string str_completeAddress = "";
        if (Convert.ToString(Session["CSRF"]) != hidCSRF.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {

            hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
            Session["CSRF"] = hidCSRF.Value;

            if (ddlState.SelectedItem.Text != "--Select--")
            {
                int_StateCode = Convert.ToInt32(ddlState.SelectedValue);
                NOCAP.BLL.Master.State regstate = new NOCAP.BLL.Master.State(int_StateCode);

                NOCAP.BLL.Master.RegionalOffice regOffice = new NOCAP.BLL.Master.RegionalOffice();
                regOffice = regstate.GetAssociatedRegionalOffice();
                if (regOffice != null)
                {
                    str_completeAddress += regOffice.RegionalOfficeName;
                    if (string.IsNullOrEmpty(regOffice.AddressLine1))
                    {
                        str_completeAddress += "\n" + regOffice.AddressLine2 + ";\n" + regOffice.AddressLine3;
                    }
                    else
                        if (string.IsNullOrEmpty(regOffice.AddressLine2))
                        {
                            str_completeAddress += "\n" + regOffice.AddressLine1 + ";\n" + regOffice.AddressLine3;
                        }
                        else
                            if (string.IsNullOrEmpty(regOffice.AddressLine3))
                            {
                                str_completeAddress += "\n" + regOffice.AddressLine1 + ";\n" + regOffice.AddressLine2;
                            }
                            else
                            {
                                str_completeAddress += "\n" + regOffice.AddressLine1 + ";\n" + regOffice.AddressLine2 + ";\n" + regOffice.AddressLine3;
                            }


                    //str_completeAddress += "\n" + regOffice.GetAddressSubDistrictName();
                    str_completeAddress += "\n" + regOffice.GetAddressDistrictName();
                    str_completeAddress += "\n" + regOffice.GetAddressStateName();
                    str_completeAddress += "\n" + regOffice.PinCode;

                    txtAddress.Text = HttpUtility.HtmlEncode(str_completeAddress);
                }
                else
                {
                    txtAddress.Text = "Regional office does not exist in this state ";
                }
            }
            else
            {
                txtAddress.Text = "";
            }
        }
    }

}