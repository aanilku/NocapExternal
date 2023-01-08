using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExternalUser_RequestForNOCLink_RequestForNOCLinkList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRFs"] = hidCSRFs.Value;

                BindRequestForNOCLink();
            }
        }

        catch (Exception ex)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    private void BindRequestForNOCLink()
    {
        try
        {
            if (NOCAPExternalUtility.IsNumeric(Session["ExternalUserCode"]))
            {
                NOCAP.BLL.UserManagement.ExternalUser obj_externalUser = new NOCAP.BLL.UserManagement.ExternalUser(Convert.ToInt32(Session["ExternalUserCode"]));

                List<NOCAP.BLL.Misc.RequestForNOCLink.RequestForNOCLinkExt> objTempRequestForNOCLinkExtList = new List<NOCAP.BLL.Misc.RequestForNOCLink.RequestForNOCLinkExt>();

                objTempRequestForNOCLinkExtList = obj_externalUser.GetRequestForNOCLinkListExt(enuA_SortField1: NOCAP.BLL.Misc.RequestForNOCLink.RequestForNOCLinkExt.SortingField.NoSorting, enuA_SortField2: NOCAP.BLL.Misc.RequestForNOCLink.RequestForNOCLinkExt.SortingField.NoSorting, enuA_SortField3: NOCAP.BLL.Misc.RequestForNOCLink.RequestForNOCLinkExt.SortingField.NoSorting);

                gvRequestForNOCLink.DataSource = objTempRequestForNOCLinkExtList;
                gvRequestForNOCLink.DataBind();
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/ExternalErrorPage.aspx", false);
        }
    }
    protected void lnkApplyNew_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToString(ViewState["CSRFs"]) != hidCSRFs.Value)
            {
                Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
            }
            else
            {
                hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
                ViewState["CSRFs"] = hidCSRFs.Value;
                Response.Redirect("RequestForNOCLinkApply.aspx");
            }
        }
    }
    protected void gvRequestForNOCLink_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Convert.ToString(ViewState["CSRFs"]) != hidCSRFs.Value)
        {
            Response.Redirect(NOCAPExternalUtility.ExternalErrorRedirectUrl, false);
        }
        else
        {
            hidCSRFs.Value = Convert.ToString(System.Guid.NewGuid());
            ViewState["CSRFs"] = hidCSRFs.Value;
            try
            {
                if (e.CommandName == "ViewNOCAttachment")
                {

                    string[] Values = e.CommandArgument.ToString().Split(',');

                    long lng_reqNo = Convert.ToInt64(Values[0]);
                    int int_attachmentCode = Convert.ToInt32(Values[1]);
                    int int_attachmentCodeSerialNumber = Convert.ToInt32(Values[2]);

                    NOCAP.BLL.Misc.RequestForNOCLink.RequestForNOCLinkAttachment obj_RequestForNOCLinkAttachment = new NOCAP.BLL.Misc.RequestForNOCLink.RequestForNOCLinkAttachment();
                    NOCAP.BLL.Misc.RequestForNOCLink.RequestForNOCLinkAttachment obj_RequestForNOCLinkAttachmentB = obj_RequestForNOCLinkAttachment.DownloadFile(lng_reqNo, int_attachmentCode, int_attachmentCodeSerialNumber);


                    if (obj_RequestForNOCLinkAttachmentB != null)
                    {
                        byte[] bytes = obj_RequestForNOCLinkAttachmentB.AttachmentFile;
                        HttpContext.Current.Response.Buffer = true;
                        HttpContext.Current.Response.Charset = "";
                        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        HttpContext.Current.Response.ContentType = HttpUtility.HtmlEncode(obj_RequestForNOCLinkAttachmentB.ContentType);
                        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "NOC_" + Convert.ToString(lng_reqNo) + "_" + Convert.ToString(int_attachmentCode) + "_" + Convert.ToString(int_attachmentCodeSerialNumber) + obj_RequestForNOCLinkAttachmentB.FileExtension);
                        HttpContext.Current.Response.BinaryWrite(bytes);
                        HttpContext.Current.Response.Flush();
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/ExternalErrorPage.aspx", false);
            }
            finally
            {
                Response.End();
            }
        }
    }
    protected void gvRequestForNOCLink_RowDataBound(object sender, GridViewRowEventArgs e)
    {


    }

    public bool SetLinkButtonVisibility(int AttchmentCode)
    {
        if (AttchmentCode != 0) return true;
        else return false;
    }
    public string SetLabelVillageOrTownText(string VillageName)
    {
        if (VillageName != string.Empty) return "Village:";
        else return "Town:";
    }
}