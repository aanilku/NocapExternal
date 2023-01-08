// Service Template For Service ID:133
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Configuration;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Design;
using System.Data.SqlClient;
using System.Data;
[WebService(Namespace = "http://schemas.xmlsoap.org/wsdl/",
Description ="Permission/Clearance for drawing ground water for industrial purpose",
Name = "Permission/Clearance for drawing ground water for industrial purpose")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]
[Microsoft.Web.Services3.Policy("ServerPolicy")]
public class npcservice : System.Web.Services.WebService
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
   // Classes objcls = new Classes();
    [WebMethod]
    public XmlDocument GetCount(int categoryid)
  {
        XmlDocument response = new XmlDocument();
        int errorCode = 0;
        try
        {
            if (categoryid < 388 || categoryid > 397)
                errorCode = -1;
            else if (RequestSoapContext.Current.IdentityToken.Identity.Name != "waterservice133")
                errorCode = -3;
        }
        catch (NullReferenceException e)
        {
            errorCode = -3;
        }
        if (errorCode < 0)
        {
            XmlElement error = response.CreateElement("error");
            XmlText text = response.CreateTextNode(errorCode.ToString());
            error.AppendChild(text);
            response.AppendChild(error);
            return response;
        }

        long record_count = 0; // This is the variable that would be send in the response

        ////TODO: The Database need to be Queried here for the Count on number of records based on the Status mentioned below
        // if (categoryid == 388)
        // {

        //     //TODO: Query number of Rows ( Under Examination (Waiting For Acceptance) )

        // }

        // if (categoryid == 389)
        // {

        //     //TODO: Query number of Rows ( Incomplete / Junk Data Proposal rejected. )

        // }

        // if (categoryid == 390)
        // {

        //     //TODO: Query number of Rows ( Application returned to applicant for clarification by Region Director, CGWB. )

        // }

        // if (categoryid == 391)
        // {

        //     //TODO: Query number of Rows ( Application at Region Office of CGWB )

        // }

        // if (categoryid == 392)
        // {

        //     //TODO: Query number of Rows ( Application at CGWA, Delhi )

        // }

        // if (categoryid == 393)
        // {

        //     //TODO: Query number of Rows ( Application returned to applicant for clarification by Member Secretary, CGWB. )

        // }

        // if (categoryid == 394)
        // {

        //     //TODO: Query number of Rows ( Application rejected by Member Secretary, CGWA. )

        // }

        // if (categoryid == 395)
        // {

        //     //TODO: Query number of Rows ( Application with Member Secretary, CGWA )

        // }

        // if (categoryid == 396)
        // {

        //     //TODO: Query number of Rows ( Application pending beyond time line )

        // }

        // if (categoryid == 397)
        // {

        //     //TODO: Query number of Rows ( Application approved by Member Secretary, CGWA )

        // }


         NOCAP.BLL.PMG.PMGService obj_PMGService = new NOCAP.BLL.PMG.PMGService();
         if (NOCAPExternalUtility.IsNumeric(categoryid))
         {
             obj_PMGService.CategoryId = categoryid;
             obj_PMGService.GetCount();
         }
         if (obj_PMGService != null)
         {
             record_count = obj_PMGService.TotalRecord;
         }
        XmlElement nps_Data = response.CreateElement("nps_Data");
        response.AppendChild(nps_Data);

        XmlElement count = response.CreateElement("count");
        nps_Data.AppendChild(count);

        XmlText Xtext = response.CreateTextNode(record_count.ToString());
        count.AppendChild(Xtext);

        return response;

  }
    [WebMethod]
    public XmlDocument GetData(int categoryid, string startrow, string numrows)
  {
        DataTable dt = new DataTable();
        XmlDocument response = new XmlDocument();
        int errorCode = 0;
        try
        {
            if (categoryid < 388 || categoryid > 397)
                errorCode = -1;
            else if (RequestSoapContext.Current.IdentityToken.Identity.Name != "waterservice133")
                errorCode = -3;
        }
        catch (NullReferenceException e)
        {
            errorCode = -3;
        }
        if (errorCode < 0)
        {
            XmlElement error = response.CreateElement("error");
            XmlText text = response.CreateTextNode(errorCode.ToString());
            error.AppendChild(text);
            response.AppendChild(error);
            return response;
        }


       ////FIELDS REQUIRED:  ApplicationNo,ApplicationType,ApplicationTypeCategory,Project Name ( Name of Industry),ProposedLocation,Application Accepted Date,ApplicationStatus,ApplicantName,MobileNo,EmailID,Address,State,District,NoOfDays
       //  if (categoryid == 388)
       //  {

       //      //TODO: Query the record from the Database with above specified fields for the mentioned status ( Under Examination (Waiting For Acceptance) )

       //  }

       //  if (categoryid == 389)
       //  {

       //      //TODO: Query the record from the Database with above specified fields for the mentioned status ( Incomplete / Junk Data Proposal rejected. )

       //  }

       //  if (categoryid == 390)
       //  {

       //      //TODO: Query the record from the Database with above specified fields for the mentioned status ( Application returned to applicant for clarification by Region Director, CGWB. )

       //  }

       //  if (categoryid == 391)
       //  {

       //      //TODO: Query the record from the Database with above specified fields for the mentioned status ( Application at Region Office of CGWB )

       //  }

       //  if (categoryid == 392)
       //  {

       //      //TODO: Query the record from the Database with above specified fields for the mentioned status ( Application at CGWA, Delhi )

       //  }

       //  if (categoryid == 393)
       //  {

       //      //TODO: Query the record from the Database with above specified fields for the mentioned status ( Application returned to applicant for clarification by Member Secretary, CGWB. )

       //  }

       //  if (categoryid == 394)
       //  {

       //      //TODO: Query the record from the Database with above specified fields for the mentioned status ( Application rejected by Member Secretary, CGWA. )

       //  }

       //  if (categoryid == 395)
       //  {

       //      //TODO: Query the record from the Database with above specified fields for the mentioned status ( Application with Member Secretary, CGWA )

       //  }

       //  if (categoryid == 396)
       //  {

       //      //TODO: Query the record from the Database with above specified fields for the mentioned status ( Application pending beyond time line )

       //  }

       //  if (categoryid == 397)
       //  {

       //      //TODO: Query the record from the Database with above specified fields for the mentioned status ( Application approved by Member Secretary, CGWA )

       //  }

         
         XmlElement nps_Data = response.CreateElement("nps_Data");
         response.AppendChild(nps_Data);


         NOCAP.BLL.PMG.PMGService obj_PMGService = new NOCAP.BLL.PMG.PMGService();
         if (NOCAPExternalUtility.IsNumeric(categoryid) && NOCAPExternalUtility.IsNumeric(startrow) && NOCAPExternalUtility.IsNumeric(numrows))
         {             
             obj_PMGService.CategoryId = categoryid;
             obj_PMGService.StartRow = Convert.ToInt64(startrow);
             obj_PMGService.NumRows = Convert.ToInt64(numrows);
             obj_PMGService.GetData();
         }
         if (obj_PMGService != null)
         {
             dt = obj_PMGService.PMGData;
         }
         if (dt != null)
         {
             if (dt.Rows.Count > 0)
             {
                 for (int i = 0; i < dt.Rows.Count; i++)
                 {
                     XmlElement row = response.CreateElement("row");
                     nps_Data.AppendChild(row);
                      
                     // Fill your fields instead of Field1,Field2......

                     XmlElement field1 = response.CreateElement("ApplicationNo");
                     XmlElement field2 = response.CreateElement("ApplicationType");
                     XmlElement field3 = response.CreateElement("ApplicationTypeCategory");
                     XmlElement field4 = response.CreateElement("ProjectName");

                     XmlElement field5 = response.CreateElement("ProposedLocation");
                     XmlElement field6 = response.CreateElement("ApplicationAcceptedDate");
                     XmlElement field7 = response.CreateElement("ApplicationStatus");
                     XmlElement field8 = response.CreateElement("ApplicantName");

                     XmlElement field9 = response.CreateElement("MobileNo");
                     XmlElement field10 = response.CreateElement("EmailID");
                     XmlElement field11 = response.CreateElement("Address");

                     XmlElement field12 = response.CreateElement("State");
                     XmlElement field13 = response.CreateElement("District");
                     XmlElement field14 = response.CreateElement("NoOfDays");

                     // Code for appending the fields in XML of Field1,Field2......
                     row.AppendChild(field1);
                     row.AppendChild(field2);
                     row.AppendChild(field3);
                     row.AppendChild(field4);
                     row.AppendChild(field5);
                     row.AppendChild(field6);
                     row.AppendChild(field7);
                     row.AppendChild(field8);
                     row.AppendChild(field9);
                     row.AppendChild(field10);
                     row.AppendChild(field11);
                     row.AppendChild(field12);
                     row.AppendChild(field13);
                     if (categoryid != 389 && categoryid != 394 && categoryid != 397)
                     {
                         row.AppendChild(field14);
                     }


                     // Code for Creating the Text of fields to be embedded in the respective field tag in XML of Field1,Field2......
                     XmlText Xtext1 = response.CreateTextNode(Convert.ToString(dt.Rows[i]["ApplicationNo"]));
                     XmlText Xtext2 = response.CreateTextNode(Convert.ToString(dt.Rows[i]["ApplicationType"]));
                     XmlText Xtext3 = response.CreateTextNode(Convert.ToString(dt.Rows[i]["ApplicationTypeCategory"]));
                     XmlText Xtext4 = response.CreateTextNode(Convert.ToString(dt.Rows[i]["ProjectName"]));
                     XmlText Xtext5 = response.CreateTextNode(Convert.ToString(dt.Rows[i]["ProposedLocation"]));
                     XmlText Xtext6 = response.CreateTextNode(Convert.ToString(dt.Rows[i]["ApplicationAcceptedDate"]));
                     XmlText Xtext7 = response.CreateTextNode(Convert.ToString(dt.Rows[i]["ApplicationStatus"]));
                     XmlText Xtext8 = response.CreateTextNode(Convert.ToString(dt.Rows[i]["ApplicantName"]));
                     XmlText Xtext9 = response.CreateTextNode(Convert.ToString(dt.Rows[i]["MobileNo"]));
                     XmlText Xtext10 = response.CreateTextNode(Convert.ToString(dt.Rows[i]["EmailID"]));
                     XmlText Xtext11 = response.CreateTextNode(Convert.ToString(dt.Rows[i]["Address"]));
                     XmlText Xtext12 = response.CreateTextNode(Convert.ToString(dt.Rows[i]["State"]));
                     XmlText Xtext13 = response.CreateTextNode(Convert.ToString(dt.Rows[i]["District"]));
                     XmlText Xtext14 = response.CreateTextNode(Convert.ToString(dt.Rows[i]["NoOfDays"]));

                     // Code for appending the text value of fields in the respective field tag in XML of Field1,Field2......
                     field1.AppendChild(Xtext1);
                     field2.AppendChild(Xtext2);
                     field3.AppendChild(Xtext3);
                     field4.AppendChild(Xtext4);
                     field5.AppendChild(Xtext5);
                     field6.AppendChild(Xtext6);
                     field7.AppendChild(Xtext7);
                     field8.AppendChild(Xtext8);
                     field9.AppendChild(Xtext9);
                     field10.AppendChild(Xtext10);
                     field11.AppendChild(Xtext11);
                     field12.AppendChild(Xtext12);
                     field13.AppendChild(Xtext13);
                     if (categoryid != 389 && categoryid != 394 && categoryid != 397)
                     {
                         field14.AppendChild(Xtext14);
                     }

                 }
             }
         }
         
         return response;
  }

}
