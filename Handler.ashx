<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Drawing;
using System.Drawing.Imaging;

public class Handler : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest (HttpContext context) {
            using (Bitmap b = new Bitmap(120, 30))
        {
            Font f = new Font("Arial", 18F, FontStyle.Bold);
            Graphics g = Graphics.FromImage(b);
            using (SolidBrush whiteBrush = new SolidBrush(Color.LightGray))
            {
                using (SolidBrush blackBrush = new SolidBrush(Color.Black))
                {
                    //SolidBrush blackBrush = new SolidBrush();
                    // RectangleF canvas = new RectangleF(0, 0, 100, 30);
                    Rectangle canvas = new Rectangle(0, 0, 120, 30);
                   
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    g.FillRectangle(whiteBrush, canvas);
                    context.Session["Captcha"] = NOCAPExternalUtility.GetRandomString();  //modified on 30.07.18
                    g.DrawString(context.Session["Captcha"].ToString(), f, blackBrush, canvas, stringFormat);
                    context.Response.ContentType = "image/gif";
                    b.Save(context.Response.OutputStream, ImageFormat.Gif);
                }
            }
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}