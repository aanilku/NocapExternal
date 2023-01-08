using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class AscxControl_ExternalUserLeftMenu : System.Web.UI.UserControl
{
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {

            HtmlLink obj = new HtmlLink();
            obj.Href = "~/css/ExternalUserCss.css";
            obj.Attributes.Add("rel", "Stylesheet");
            obj.Attributes.Add("type", "text/css");
            Page.Header.Controls.Add(obj);
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}

//href="~/css/ExternalUserCss.css" 