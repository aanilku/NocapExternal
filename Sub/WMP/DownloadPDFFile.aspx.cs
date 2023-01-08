using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sub_WMP_DownloadPDFFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            try
            {
                string embed = "";
                embed = "<object data=\"{0}\"  width=\"1300px\" height=\"1000px\">";
                embed += "</object>";
                ltEmbed.Text = string.Format(embed, ResolveUrl("DownloadPDF.ashx"));                
            }
            catch (Exception ex)
            {
            }

        }
    }
}