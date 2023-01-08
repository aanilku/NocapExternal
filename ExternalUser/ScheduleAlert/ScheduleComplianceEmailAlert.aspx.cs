using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class ExternalUser_ScheduleAlert_ScheduleComplianceEmailAlert : System.Web.UI.Page
{
    string strEmailServerName = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        //NOCAP.BLL.Template.Email.EmailTemplateWithValue obj_EmailTemplateWithValue = new NOCAP.BLL.Template.Email.EmailTemplateWithValue(98, 6);

    }
    protected void btnSendEmailAlert_Click(object sender, EventArgs e)
    {

        NOCAP.BLL.ScheduleAlert.ScheduleComplianceEmailAlert obj_scheduleComplianceEmailAlert = new NOCAP.BLL.ScheduleAlert.ScheduleComplianceEmailAlert();
        obj_scheduleComplianceEmailAlert.GetList();
        NOCAP.BLL.ScheduleAlert.ScheduleComplianceEmailAlert[] arr_ScheduleComplianceEmailAlertCollection = obj_scheduleComplianceEmailAlert.ScheduleComplianceEmailAlertCollection;

        if (arr_ScheduleComplianceEmailAlertCollection.Count() > 0)
        {
            foreach (NOCAP.BLL.ScheduleAlert.ScheduleComplianceEmailAlert item_scheduleComplianceEmailAlert in arr_ScheduleComplianceEmailAlertCollection)
            {
                obj_scheduleComplianceEmailAlert.ApplicationCode = item_scheduleComplianceEmailAlert.ApplicationCode;
                obj_scheduleComplianceEmailAlert.ComplianceAlertCode = item_scheduleComplianceEmailAlert.ComplianceAlertCode;
                NOCAP.BLL.ScheduleAlert.ComplianceAlertConfig obj_ComplianceAlertConfig = new NOCAP.BLL.ScheduleAlert.ComplianceAlertConfig(item_scheduleComplianceEmailAlert.ComplianceAlertCode);

                // get template here by EmailAlertTemplateCode
                // obj_ComplianceAlertConfig.EmailAlertTemplateCode;
              

                bool isSendEmail = false;
                //NOCAP.BLL.Template.Email.EmailTemplateWithValue obj_EmailTemplateWithValue = new NOCAP.BLL.Template.Email.EmailTemplateWithValue(item_scheduleComplianceEmailAlert.ApplicationCode, obj_ComplianceAlertConfig.EmailAlertTemplateCode);
                // send email here
                  isSendEmail= SendEMail(item_scheduleComplianceEmailAlert.ApplicationCode, obj_ComplianceAlertConfig.EmailAlertTemplateCode);

                if (isSendEmail)
                {
                    // update here
                   int intResultUpdate = obj_scheduleComplianceEmailAlert.Update();
                }
            }
        }

        ////obj_scheduleComplianceEmailAlert.ApplicationCode = 98;
        ////obj_scheduleComplianceEmailAlert.ComplianceAlertCode = 1;
        ////int intResult1 = obj_scheduleComplianceEmailAlert.Update();



        // Finaly update and delete here
         int intResult=  obj_scheduleComplianceEmailAlert.FinalUpdateAndDelete();
         

    }


    bool SendEMail(long lng_AppCode, int intEmailTemplateCode)
    {
        try
        {
            bool boolResult = false;

            if (EmailUtility.IsSendEmailEnable())
            {             
                
                    NOCAP.BLL.Template.Email.EmailTemplateWithValue obj_EmailTemplateWithValue = new NOCAP.BLL.Template.Email.EmailTemplateWithValue(lng_AppCode, intEmailTemplateCode);
                    { 
                       
                        if (!string.IsNullOrEmpty(obj_EmailTemplateWithValue.ToEmailIDValue))
                        {
                            byte[] data = obj_EmailTemplateWithValue.AttachFile;
                            if (!string.IsNullOrEmpty(obj_EmailTemplateWithValue.FileExtension))
                            {
                                MemoryStream ms = new MemoryStream(obj_EmailTemplateWithValue.AttachFile);
                            }

                            boolResult = EmailUtility.SendMailHavingFileStream(out strEmailServerName, obj_EmailTemplateWithValue.ToEmailIDValue,
                                obj_EmailTemplateWithValue.CcEmailIDValue, obj_EmailTemplateWithValue.BccEmailIDValue, StrSubject: obj_EmailTemplateWithValue.Subject,
                                StrBody: obj_EmailTemplateWithValue.MessageBodyValue, BytAttachFile: data, StrContentType: obj_EmailTemplateWithValue.ContentType, StrFileExtension: obj_EmailTemplateWithValue.FileExtension);
                            if (!boolResult)
                            {
                                //lblToMailSent.Text = "Email not send ";
                                //lblToMailSent.Visible = true;
                                //lblToMailSent.ForeColor = System.Drawing.Color.Red;

                            }
                            else
                            {
                                //lblToMailSent.Text = "Email send Successfully.";
                                //lblToMailSent.Visible = true;
                                //lblToMailSent.ForeColor = System.Drawing.Color.Green;
                                //SaveEmailAlert();

                            }

                        }

                    }              

            }
            return boolResult;
        }
        catch (Exception)
        {
            return false; 
        }
    }

}