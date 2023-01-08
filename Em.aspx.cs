using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Em : System.Web.UI.Page
{

    string strToMailId = "";
    string strCcMailId = "";
    string strBccMailId = "";
    string strSubject = "";
    string strBody = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        SendMail();
    }

    void SendMail()
    {
        try
        {

            if (EmailUtility.IsSendEmailEnable())
            {
                string strout = "";
                bool boolResult = false;
                strToMailId = "chaurasia@nic.in";
                strSubject = "Test email";
                strBody = "custom body....";
                if (!string.IsNullOrEmpty(strToMailId))
                {
                    boolResult = EmailUtility.SendMail(out strout ,strToMailId, strCcMailId, strBccMailId, strSubject, strBody);
                    if (!boolResult)
                    {
                        Response.Write("Email not send ");

                    }
                    else
                    {
                        Response.Write("Email send Successfully.");
                    }
                }
            }
        }
        catch (Exception)
        {
            Response.Write("Exception Email not send ");

        }
    }
}