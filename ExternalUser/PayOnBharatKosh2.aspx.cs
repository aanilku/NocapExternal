using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Threading;
using System.Text;
using System.Configuration;
public partial class PayOnBharatKosh2 : System.Web.UI.Page
{
    string str_NTRPURI = ConfigurationManager.AppSettings["NTRPURI"]; // "https://training.pfms.gov.in/bharatkosh/bkepay";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                
                hidCSRF.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRF"] = hidCSRF.Value;


                if (Session["hdnBase64String"] != null)
                {


                    Response.Clear();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<html>");
                    sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
                    sb.AppendFormat("<form name='form' action='{0}' method='post'>", str_NTRPURI);
                    sb.AppendFormat("<input type='hidden' name='bharrkkosh' value='{0}'>", HttpUtility.HtmlEncode(Session["hdnBase64String"].ToString()));                    
                    // Other params go here
                    sb.Append("</form>");
                    sb.Append("</body>");
                    sb.Append("</html>");

                    Response.Write(sb.ToString());

                    Response.End();
                    Session.Remove("hdnBase64String");

                }
            }

        }
        catch (ThreadAbortException ex)
        {
           // Thread.ResetAbort();
        }
        catch (Exception ex)
        {

        }
        //Thread.Sleep(10000);
    }
}