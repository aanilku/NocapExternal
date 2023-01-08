using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AscxControl_AttachmentLimit : System.Web.UI.UserControl
{
   
    protected void Page_Load(object sender, EventArgs e)
    {               
       BindAttachmentLimitAndSize();          
    }

    public void BindAttachmentLimitAndSize()
    {
         NOCAP.BLL.Common.AttachmentLimit objAttachmentLimit = new NOCAP.BLL.Common.AttachmentLimit();
         objAttachmentLimit = new NOCAP.BLL.Common.AttachmentLimit();
        if (objAttachmentLimit!=null)
        {
            lblAttachment.Text =Convert.ToString(objAttachmentLimit.NumberOfAttachment);
            lblSize.Text =Convert.ToString(objAttachmentLimit.SizeOfEachAttachment);
        }

    }
}