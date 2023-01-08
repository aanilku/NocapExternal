using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sub_DistrictOfficeLocator_DistrictOffLocation : System.Web.UI.Page
{
    //protected void Page_PreInit(object sender, EventArgs e)
    //{

    //    try
    //    {

    //        NOCAP.BLL.UserManagement.ExternalUserLogIn obj_Login = new NOCAP.BLL.UserManagement.ExternalUserLogIn();


    //        if (obj_Login.ProccedNextToLogin(Convert.ToInt64(Session["ExternalUserCode"])) == 1)
    //        {
    //            if (obj_Login.ExternalUserCode < 1)
    //            {
    //                //MasterPageFile = "~/ExternalUser/SubMasterPage.master";
    //            }
    //            else
    //            {
    //                NOCAP.BLL.UserManagement.ExternalUser obj_exUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
    //                MasterPageFile = "~/ExternalUser/ExternalUserMaster.master";
    //                //lblLoginMsg.Text = "Logged in as: " + obj_exUser.ExternalUserName;
    //            }

    //        }
    //        else
    //        {
    //            MasterPageFile = "~/Sub/ApplicantRegistrationMaster.master";
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        Response.Redirect("~/ExternalErrorPage.aspx", false);

    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
        catch(Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }

    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {


        int StateCode = 0;
        int DistrictCode = 0;
        string CompleteDetail = "";
        txtAddress.Text = "";
        NOCAP.BLL.Master.DistrictOffice distOffice = new NOCAP.BLL.Master.DistrictOffice();
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
                if (ddlState.SelectedItem.Text != "--Select--" && ddlDistrict.SelectedItem.Text != "--Select--")
                {
                    //StateCode = Convert.ToInt32("XYZ");
                    StateCode = Convert.ToInt32(ddlState.SelectedValue);
                    DistrictCode = Convert.ToInt32(ddlDistrict.SelectedValue);
                }
                distOffice = new NOCAP.BLL.Master.DistrictOffice(StateCode, DistrictCode);


                if (Convert.ToString(distOffice.DistrictOfficeName) != String.Empty)
                {
                    CompleteDetail += " " + distOffice.DistrictOfficeName;
                    if (string.IsNullOrEmpty(distOffice.AddressLine1))
                    {
                        CompleteDetail += "\n" + " " + distOffice.AddressLine2 + ";\n" + " " + distOffice.AddressLine3;
                    }
                    else
                        if (string.IsNullOrEmpty(distOffice.AddressLine2))
                        {
                            CompleteDetail += "\n" + " " + distOffice.AddressLine1 + ";\n" + " " + distOffice.AddressLine3;
                        }
                        else
                            if (string.IsNullOrEmpty(distOffice.AddressLine3))
                            {
                                CompleteDetail += "\n" + "  " + distOffice.AddressLine1 + ";\n" + " " + distOffice.AddressLine2;
                            }
                            else
                            {
                                CompleteDetail += "\n" + " " + distOffice.AddressLine1 + ";\n" + " " + distOffice.AddressLine2 + ";\n" + " " + distOffice.AddressLine3;
                            }


                    CompleteDetail += "\n" + " " + distOffice.AddressLine1 + ";\n" + " " + distOffice.AddressLine2 + ";\n" + " " + distOffice.AddressLine3;
                    CompleteDetail += "\n" + " " + distOffice.GetAddressSubDistrictName();
                    CompleteDetail += "\n" + " " + distOffice.GetAddressDistrictName();
                    CompleteDetail += "\n" + " " + distOffice.GetAddressStateName();
                    CompleteDetail += "\n" + " " + distOffice.PinCode;
                    txtAddress.Text = HttpUtility.HtmlEncode(CompleteDetail.ToString());
                    txtDistrictCommitee.Text = HttpUtility.HtmlEncode(Convert.ToString(" " + distOffice.DistrictCommittee));
                }
                else
                {
                    txtAddress.Text = "  No Record Available";
                    txtDistrictCommitee.Text = "";
                }


            }
            catch (Exception)
            {
                //lblMessage.Text = distOffice.CustumMessage;
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }

        }



    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {


        int int_intStateCode;
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
                int_intStateCode = Convert.ToInt32(ddlState.SelectedValue);
                ddlDistrict.Items.Clear();
                if (ddlState.SelectedValue == "")
                {

                    NOCAPExternalUtility.AddFirstItemInDropDownList(ref ddlDistrict);

                }
                else
                {

                    if (NOCAPExternalUtility.FillDropDownDistrict(ref ddlDistrict, int_intStateCode) != 1)
                    {
                        Response.Write("Problem in district population");
                    }
                }

            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
                //lblMessage.Text = "Ploblem in page";
            }
        }
    }
}