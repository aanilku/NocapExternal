using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExternalUser_Communication_CommunicationReply2 : System.Web.UI.Page
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            PopulateCommunicationReplyVeri(112);
        }
    }
    private void PopulateCommunicationReplyVeri(long AppCode)
    {
        try
        {
            NOCAP.BLL.Misc.Communication.CommunicationRequest obj_CommunicationRequest = new NOCAP.BLL.Misc.Communication.CommunicationRequest();
            obj_CommunicationRequest.GetAll();
            NOCAP.BLL.Misc.Communication.CommunicationRequest[] arr = null;
            arr = obj_CommunicationRequest.CommunicationRequestCollection;
            GridView1.DataSource = arr;
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            
        }



    }
    protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnAttachment = (HiddenField)e.Row.FindControl("hdnAttachment");
            string[] hdnAttachmentArr = hdnAttachment.Value.Split(',');
            string AppCode = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView grdviewCommReplyVeri = (GridView)e.Row.FindControl("GridView2");
            NOCAP.BLL.Misc.Communication.CommunicationReply obj_CommunicationReply = new NOCAP.BLL.Misc.Communication.CommunicationReply();
            obj_CommunicationReply.AppCode = Convert.ToInt64(AppCode);

            obj_CommunicationReply.CommunicatReqSNumber = Convert.ToInt32(hdnAttachmentArr[1].ToString());
            obj_CommunicationReply.GetAll();

            if (obj_CommunicationReply.CommunicationReplyCollection.Length > 0)
            {
                grdviewCommReplyVeri.Visible = true;
                grdviewCommReplyVeri.DataSource = obj_CommunicationReply.CommunicationReplyCollection;
                grdviewCommReplyVeri.DataBind();
            }
            else
            {
                grdviewCommReplyVeri.Visible = false;

            }
        }
    }
  
}
   