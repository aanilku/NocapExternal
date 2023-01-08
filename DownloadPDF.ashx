<%@ WebHandler Language="C#" Class="DownloadPDF" %>

using System;
using System.Web;

public class DownloadPDF : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            byte[] bytes = (byte[])context.Session["nocapbytes"];
            string str_ContentType = context.Session["ContentType"].ToString();
            context.Response.Buffer = true;
            context.Response.Charset = "";
            if (str_ContentType == "image/jpeg")
            {
                context.Response.AppendHeader("Content-Disposition", "inline; filename=" + "DownloadFile.jpg");
            }
            else
            {
                context.Response.AppendHeader("Content-Disposition", "inline; filename=" + "DownloadFile.pdf");
            }
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.ContentType = str_ContentType;
            context.Response.BinaryWrite(bytes);
            context.Response.Flush();
            context.Response.End();
            // context.Session["DownloadCounter"] = (int)context.Session["DownloadCounter"] - 1;
        }
        catch (System.Threading.ThreadAbortException)
        {

        }
        catch (Exception ex)
        {
            //context.Session["DownloadCounter"] = 0;
        }
        finally
        {
            //if ((int)context.Session["DownloadCounter"] == 0)
            //{
            //    context.Session.Remove("nocapbytes");
            //    context.Session.Remove("ContentType");
            //    context.Session.Remove("DownloadCounter");              
            //}
        }

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}