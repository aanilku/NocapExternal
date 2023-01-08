using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sub_SubMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoServerCaching();
        Response.Cache.SetNoStore();

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
