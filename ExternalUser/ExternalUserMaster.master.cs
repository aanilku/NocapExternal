using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;

public partial class ExternalUser_ExternalUserMaster : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoServerCaching();
        Response.Cache.SetNoStore();

    }
    protected void Page_Load(object sender, EventArgs e)
    {


        //if (!IsPostBack)
        //{
        //    populateMenuItem();
        //}
        //External_DynamicMenu.Items



        if (Session["ExternalUserCode"] == null)
        {
            Session["ExternalUserCode"] = null;
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");

        }


        ////if (Request.UrlReferrer == null)
        ////{
        ////    //Session["ExternalUserCode"] = null;
        ////    //Response.Redirect("http://www.google.com");
        ////    //PreviousPage.ToString();


        ////    Session["ExternalUserCode"] = null;
        ////    Session.Clear();
        ////    Session.RemoveAll();
        ////    Session.Abandon();
        ////    FormsAuthentication.SignOut();
        ////    Response.Redirect("~/Default.aspx");
        ////}


        ////if (!IsPostBack )
        ////{
        ////    NOCAP.BLL.UserManagement.ExternalUser obj_exUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
        ////    lblLoginMsg.Text = "Logged in as: " + obj_exUser.ExternalUserName;



        ////}

    }


    //private void populateMenuItem()
    //{
    //    DataTable menuData = GetMenuData();
    //    AddTopMenuItems(menuData);
    //}

    //private DataTable GetMenuData()
    //{
    //     using ( SqlConnection con= new SqlConnection("Data Source=(local);Database=CGWAMenu;Integrated Security=True;"))
    //     {
    //       using (SqlCommand cmd = new SqlCommand("Select Id, ParentId, Name From ExternalUserMenu",con))
    //       {
    //           SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //           DataTable dt = new DataTable();
    //           adp.Fill(dt);
    //           return dt;   
    //       }

    //     }

    //}


    /// Filter the data to get only the rows that have a
    /// null ParentID (This will come on the top-level menu items)

    // private void AddTopMenuItems(DataTable menuData)
    // {
    //     DataView dView = new DataView(menuData);
    //     dView.RowFilter = "ParentID IS NULL";
    //     foreach(DataRowView row in dView)
    //     {
    //         MenuItem newMenuItem = new MenuItem(row["Name"].ToString(), row["Id"].ToString());
    //         External_DynamicMenu.Items.Add(newMenuItem);
    //         AddChildMenuItems(menuData, newMenuItem);
    //     }
    // }

    // //This code is used to recursively add child menu items by filtering by ParentID

    // private void AddChildMenuItems(DataTable menuData, MenuItem parentMenuItem)
    // {
    //     DataView dView = new DataView(menuData);
    //     dView.RowFilter = "ParentID=" + parentMenuItem.Value;

    //     foreach (DataRowView row in dView)
    //     {
    //         MenuItem newMenuItem = new MenuItem(row["Name"].ToString(), row["Id"].ToString());
    //         parentMenuItem.ChildItems.Add(newMenuItem);
    //         AddChildMenuItems(menuData, newMenuItem);
    //     }
    //}
    //protected void External_DynamicMenu_MenuItemClick(object sender, MenuEventArgs e)
    //{
    //    string name = External_DynamicMenu.SelectedItem.Text;
    //    if (name == "Logout")
    //    {
    //             Session["ID"]="";
    //        Session.Clear();
    //        FormsAuthentication.SignOut();

    //        Response.Redirect("~/LandingPage/index.html");
    //    }


    //    string pageUrl = name + ".aspx";
    //    Response.Redirect(pageUrl);
    //}


    protected void ExternalMenu_MenuItemClick(object sender, MenuEventArgs e)
    {

        try
        {




            //    Response.Redirect("~/ExternalUser/MiningNew/MiningNew.aspx", false);
            NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));


            if (e.Item.Value == "Industrial")
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication[] arr = obj_ExternalUser.GetSaveAsDraftIndustrialNewApplicationList(NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.SortingField.NoSorting);
                int int_indAppCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType(e.Item.Value);
                NOCAP.BLL.Master.SADValidity objSADValidity = new NOCAP.BLL.Master.SADValidity(int_indAppCode);

                if (arr.Length >= objSADValidity.AllowNoOfMaxApplication)
                {

                    string str_IndMsg = "alert('Industrial Save As Draft Application Exceed Limit. Only " + objSADValidity.AllowNoOfMaxApplication + " Save As Draft Application Allowed at a time');";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "UserSecurity", str_IndMsg, true);
                }
                else
                {
                    if (obj_externalUser.InvestorSWSId == "" || obj_externalUser.InvestorSWSId == null)
                    {
                        // Response.Redirect("https://www.nsws.gov.in/", false);
                        Response.Redirect("~/ExternalUser/IndustrialNew/IndustrialNewKLD.aspx", false);
                    }
                    else
                        Response.Redirect("~/ExternalUser/IndustrialNew/IndustrialNewKLD.aspx", false);
                }

            }
            if (e.Item.Value == "IndustrialG")
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication[] arr = obj_ExternalUser.GetSaveAsDraftIndustrialNewApplicationList(NOCAP.BLL.Industrial.New.SADApplication.IndustrialNewSADApplication.SortingField.NoSorting);
                int int_indAppCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType(e.Item.Value);
                NOCAP.BLL.Master.SADValidity objSADValidity = new NOCAP.BLL.Master.SADValidity(int_indAppCode);

                if (arr.Length >= objSADValidity.AllowNoOfMaxApplication)
                {

                    string str_IndMsg = "alert('Industrial Save As Draft Application Exceed Limit. Only " + objSADValidity.AllowNoOfMaxApplication + " Save As Draft Application Allowed at a time');";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "UserSecurity", str_IndMsg, true);
                }
                else
                {
                    if (obj_externalUser.InvestorSWSId == "" || obj_externalUser.InvestorSWSId == null)
                    {
                        Response.Redirect("~/ExternalUser/IndustrialNew/IndustrialNew.aspx", false);
                        // Response.Redirect("https://www.nsws.gov.in/", false);
                    }
                    else Response.Redirect("~/ExternalUser/IndustrialNew/IndustrialNew.aspx", false);
                }
                  
            }
            if (e.Item.Value == "RenewIndustrial")
            {
                Response.Redirect("~/ExternalUser/IndustrialReNew/IndustrialRenewList.aspx", false);
            }
            if (e.Item.Value == "Infrastructure")
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication[] arr = obj_ExternalUser.GetSaveAsDraftInfrastructureNewApplicationList(NOCAP.BLL.Infrastructure.New.SADApplication.InfrastructureNewSADApplication.SortingField.NoSorting);
                int int_infAppCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType(e.Item.Value);
                NOCAP.BLL.Master.SADValidity objSADValidity = new NOCAP.BLL.Master.SADValidity(int_infAppCode);
                if (arr.Length >= objSADValidity.AllowNoOfMaxApplication)
                {
                    string str_InfMsg = "alert('Infrastructure Save As Draft Application Exceed Limit. Only " + objSADValidity.AllowNoOfMaxApplication + " Save As Draft Application Allowed at a time');";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "UserSecurity", str_InfMsg, true);
                }
                
                else
                {
                    if (obj_externalUser.InvestorSWSId == "" || obj_externalUser.InvestorSWSId == null)
                    {
                        //   Response.Redirect("https://www.nsws.gov.in/", false);
                        Response.Redirect("~/ExternalUser/InfrastructureNew/InfrastructureNew.aspx", false);
                    }
                    else Response.Redirect("~/ExternalUser/InfrastructureNew/InfrastructureNew.aspx", false);
                }

            }
            if (e.Item.Value == "RenewInfrastructure")
            {
                Response.Redirect("~/ExternalUser/InfrastructureRenew/InfrastructureRenewList.aspx", false);
            }

            if (e.Item.Value == "Mining")
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_ExternalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt64(Session["ExternalUserCode"]));
                NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication[] arr = obj_ExternalUser.GetSaveAsDraftMiningNewApplicationList(NOCAP.BLL.Mining.New.SADApplication.MiningNewSADApplication.SortingField.NoSorting);
                int int_minAppCode = NOCAPExternalUtility.EquivalentApplicationTypeCodeOfDatabaseForApplicationType(e.Item.Value);
                NOCAP.BLL.Master.SADValidity objSADValidity = new NOCAP.BLL.Master.SADValidity(int_minAppCode);
                if (arr.Length >= objSADValidity.AllowNoOfMaxApplication)
                {
                    string str_MinMsg = "alert('Mining Save As Draft Application Exceed Limit. Only " + objSADValidity.AllowNoOfMaxApplication + " Save As Draft Application Allowed at a time');";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "UserSecurity", str_MinMsg, true);
                }                
                else
                {
                    if (obj_externalUser.InvestorSWSId == "" || obj_externalUser.InvestorSWSId == null)
                    {
                        //   Response.Redirect("https://www.nsws.gov.in/", false);
                        Response.Redirect("~/ExternalUser/MiningNew/MiningNew.aspx", false);
                    }
                    else Response.Redirect("~/ExternalUser/MiningNew/MiningNew.aspx", false);
                }


            }

            if (e.Item.Value == "RenewMining")
            {
                Response.Redirect("~/ExternalUser/MiningRenew/MiningRenewList.aspx", false);
            }


            if (e.Item.Value == "Change Password")
            {
                Response.Redirect("~/ExternalUser/UserManagement/ChangePassword.aspx", false);

                //Response.Redirect("~/ExternalUser/UserManagement/ChangePassword.aspx");
            }
            if (e.Item.Value == "View")
            {
                Response.Redirect("~/ExternalUser/UserManagement/EditProfile.aspx", false);

                //Response.Redirect("~/ExternalUser/UserManagement/EditProfile.aspx");
            }
            if (e.Item.Value == "Enroll Old NOC")
            {
                Response.Redirect("~/ExternalUser/RequestForNOCLink/RequestForNOCLinkList.aspx", false);
            }
            if (e.Item.Value == "ApplicationUpdate")
            {
                Response.Redirect("~/ExternalUser/ApplicationManagement/ApplicationNameChange.aspx", false);
            }
            if (e.Item.Value == "PayPayment")
            {
                Response.Redirect("~/ExternalUser/PayAppSumbit.aspx", false);
            }
            if (e.Item.Value == "ViaNOCAP")
            {
                Response.Redirect("~/ExternalUser/ViewAllOnlinePayment.aspx", false);
            }


            if (e.Item.Value == "ExpansionIndustrial")
            {
                Response.Redirect("~/ExternalUser/Expansion/IND/FirstExpansionApply.aspx", false);
            }
            if (e.Item.Value == "ExpansionInfrastructure")
            {
                Response.Redirect("~/ExternalUser/Expansion/INF/INFFirstExpansionApply.aspx", false);
            }
            if (e.Item.Value == "ExpansionMining")
            {
                Response.Redirect("~/ExternalUser/Expansion/MIN/MINFirstExpansionApply.aspx", false);
            }


        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);

        }


    }

    protected void lbnLogout_Click(object sender, EventArgs e)
    {
        try
        {
            SessionIDManager Manager = new SessionIDManager();
            string NewID = Manager.CreateSessionID(Context);
            bool redirected = false;
            bool IsAdded = false;
            Manager.SaveSessionID(Context, NewID, out redirected, out IsAdded);
            Session["ExternalUserCode"] = null;
            Session.Clear();
            Session.Remove("ExternalUserCode");
            Session.RemoveAll();
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            FormsAuthentication.SignOut();

            Response.Redirect("~/Default.aspx", false);
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }




}
