using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

using System.Net.Mail;
using System.IO;





/// <summary>
/// Summary description for EmailUtility
/// </summary>
public class EmailUtility
{

    public static bool IsSendEmailEnable()
    {
        if (ConfigurationManager.AppSettings["SendEmailEnable"] != null && ConfigurationManager.AppSettings["SendEmailEnable"] == "Yes")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool SendMail(out string EmailServerName, string StrTo = "", string StrCc = "", string StrBcc = "", string StrSubject = "", string StrBody = "", string AttachmentPath = "")
    {
        bool boolResult = false;
        try
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SMTPServerName"]) && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["FromMailAddress"]))
            {
                using (MailMessage mail = new MailMessage())
                {
                    using (SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServerName"]))
                    {
                        EmailServerName = ConfigurationManager.AppSettings["SMTPServerName"];
                        mail.From = new MailAddress(ConfigurationManager.AppSettings["FromMailAddress"]);
                        if (StrTo != "")
                            mail.To.Add(StrTo);
                        if (StrCc != "")
                            mail.CC.Add(StrCc);
                        if (StrBcc != "")
                            mail.Bcc.Add(StrBcc);
                        if (StrSubject != "")
                            mail.Subject = StrSubject;
                        if (StrBody != "")
                            mail.Body = StrBody;
                        if (AttachmentPath != "")
                        {
                            Attachment attachment = new Attachment(AttachmentPath);
                            mail.Attachments.Add(attachment);
                        }
                        mail.IsBodyHtml = true;
                        SmtpServer.Port = 25;
                        SmtpServer.Send(mail);
                        boolResult = true;
                    }
                }
            }
            EmailServerName = ConfigurationManager.AppSettings["SMTPServerName"];
            return boolResult;
        }
        catch (Exception ex)
        {
            EmailServerName = ConfigurationManager.AppSettings["SMTPServerName"];
            return false;
        }
    }

    public static bool SendMailHavingFileStream(out string EmailServerName, string StrTo = "", string StrCc = "", string StrBcc = "", string StrSubject = "", string StrBody = "", byte[] BytAttachFile = null, string StrContentType = "", string StrFileExtension = "")
    {
        bool boolResult = false;
        try
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SMTPServerName"]) && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["FromMailAddress"]))
            {
                using (MailMessage mail = new MailMessage())
                {
                    using (SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServerName"]))
                    {
                        EmailServerName = ConfigurationManager.AppSettings["SMTPServerName"];
                        mail.From = new MailAddress(ConfigurationManager.AppSettings["FromMailAddress"]);

                        if (!string.IsNullOrEmpty(StrTo))
                            mail.To.Add(StrTo);
                        if (!string.IsNullOrEmpty(StrCc))
                            mail.CC.Add(StrCc);
                        if (!string.IsNullOrEmpty(StrBcc))
                            mail.Bcc.Add(StrBcc);
                        if (!string.IsNullOrEmpty(StrSubject))
                            mail.Subject = StrSubject;
                        if (!string.IsNullOrEmpty(StrBody))
                            mail.Body = StrBody;
                        if (!string.IsNullOrEmpty(StrFileExtension))
                        {
                            MemoryStream ms = new MemoryStream(BytAttachFile);
                            Attachment attach = new Attachment(ms, StrContentType);
                            attach.ContentDisposition.FileName = "NOCAPInstructions" + StrFileExtension;
                            mail.Attachments.Add(attach);


                        }

                        mail.IsBodyHtml = true;
                        SmtpServer.Port = 25;
                        SmtpServer.Send(mail);
                        boolResult = true;
                    }
                }
            }
            EmailServerName = ConfigurationManager.AppSettings["SMTPServerName"];
            return boolResult;
        }
        catch (Exception)
        {
            EmailServerName = ConfigurationManager.AppSettings["SMTPServerName"];
            return false;
        }
    } 


}