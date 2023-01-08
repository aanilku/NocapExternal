using NOCAP.BLL.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RelaxationAttachmentBLL
/// </summary>
/// 
namespace Relaxation
{
    public class RelaxationAttachmentBLL
    {
 
        public static int RelaxationAppDownloadFiles(long lng_RelaxationApplicationCode, int int_attachmentCode, int int_attachmentCodeSerialNumber)
        {
            try
            {
                int intResult = 0;

                Relaxation.RelaxationAttachmentBLL obj_RelaxationAttachmentBLL = new Relaxation.RelaxationAttachmentBLL();

                Relaxation.RelaxationAttachmentBLL obj_RelaxationAttachmentBLLFile = obj_RelaxationAttachmentBLL.DownloadFile(lng_RelaxationApplicationCode, int_attachmentCode, int_attachmentCodeSerialNumber);

                if (obj_RelaxationAttachmentBLLFile != null)
                {
                    byte[] bytes = obj_RelaxationAttachmentBLLFile.AttachmentFile;
                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.Charset = "";
                    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    HttpContext.Current.Response.ContentType = obj_RelaxationAttachmentBLLFile.ContentType;
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "IND_" + Convert.ToString(lng_RelaxationApplicationCode) + "_" + Convert.ToString(int_attachmentCode) + "_" + Convert.ToString(int_attachmentCodeSerialNumber) + obj_RelaxationAttachmentBLLFile.FileExtension);
                    HttpContext.Current.Response.BinaryWrite(bytes);
                    HttpContext.Current.Response.Flush();

                    intResult = 1;
                }
                return intResult;
            }
            catch (Exception)
            {
                return 0;
            }

        }


        private long lng_AppCode;
        private decimal dec_PayMentAmount;
        private string str_GSTNumber;
        private int intPayMentAttCode;

        private int int_SerialNumber;

        private byte[] str_attFile;
        private string str_fileContentType;
        private string str_fileExtension;
        private string str_attPath;
        private int int_attCode;
        private string str_attName;

        private DateTime? dt_createdOnByUC;
        private long? int_createdByUC;

        private DateTime? dt_createdOnExUC;
        private long? lng_createdByExUC;


        private string str_custMessage;

        private DateTime? dt_modifiedOnByUC;
        private int? int_modifiedByUC;

        private Relaxation.RelaxationAttachmentBLL[] arr_RelaxationAttachmentLCollection;

        
        public long ApplicationCode
        {
            get
            {
                return lng_AppCode;
            }
             set
            {
                lng_AppCode = value;
            }
        }

        public int AttachmentCodeSerialNumber
        {
            get
            {
                return int_SerialNumber;
            }
            set
            {
                int_SerialNumber = value;
            }
        }

        public decimal PayMentAmount
        {
            get
            {
                return dec_PayMentAmount;
            }
            set
            {
                dec_PayMentAmount = value;
            }
        }
        public string GSTNumber
        {
            get
            {
                return str_GSTNumber;
            }
            set
            {
                str_GSTNumber = value;
            }
        }
        public int PayMentAttCode
        {
            get
            {
                return intPayMentAttCode;
            }
            set
            {
                intPayMentAttCode = value;
            }
        }

        public byte[] AttachmentFile
        {
            get
            {
                return str_attFile;
            }
            set
            {
                str_attFile = value;
            }

        }
        public string ContentType
        {
            get
            {
                return str_fileContentType;
            }
            set
            {
                str_fileContentType = value;
            }

        }
        public string FileExtension
        {

            get
            {
                return str_fileExtension;
            }
            set
            {
                str_fileExtension = value;
            }
        }
        public string AttachmentPath
        {
            get
            {
                return str_attPath;
            }
            set
            {
                try
                {
                    if (Utility.IsValidStringHavingLength(value.Trim(), 200) == 1)
                    {
                        str_attPath = value.Trim();
                    }
                    else
                    {
                        str_attPath = "";
                        //this.CustumMessage = "Land Use Type- Land Use Type descriprion have more than 50 characters.";
                        throw new ApplicationException("RelaxationApplicationAttachment- AttachmentPath  have more than 200 characters.");
                    }

                }
                catch (Exception ex)
                {
                    throw new ApplicationException(ex.Message);
                }


            }

        }

        public int AttachmentCode
        {
            get
            {
                return int_attCode;
            }
            set
            {
                int_attCode = value;
            }
        }
        public string AttachmentName
        {
            get
            {
                return str_attName;
            }
            set
            {
                try
                {
                    if (Utility.IsValidStringHavingLength(value.Trim(), 50) == 1)
                    {
                        str_attName = value.Trim();
                    }
                    else
                    {
                        str_attName = "";
                        //this.CustumMessage = "Land Use Type- Land Use Type descriprion have more than 50 characters.";
                        throw new ApplicationException("IndustrialNewApplicationAttachment- AttachmentName  have more than 50 characters.");
                    }

                }
                catch (Exception ex)
                {
                    throw new ApplicationException(ex.Message);
                }


            }

        }

        public DateTime? CreatedOnByUC
        {
            get
            {
                return dt_createdOnByUC;
            }
            internal set
            {
                dt_createdOnByUC = value;
            }
        }

        public long? CreatedByUC
        {
            get
            {
                return int_createdByUC;
            }
            set
            {
                int_createdByUC = value;
            }
        }

        public DateTime? CreatedOnByExUC
        {
            get
            {
                return dt_createdOnExUC;
            }
            internal set
            {
                dt_createdOnExUC = value;
            }
        }
        public long? CreatedByExUC
        {
            get
            {
                return lng_createdByExUC;
            }
            set
            {
                lng_createdByExUC = value;
            }
        }

        public DateTime? ModifiedOnByUC
        {
            get
            {
                return dt_modifiedOnByUC;
            }
            internal set
            {
                dt_modifiedOnByUC = value;
            }
        }

        public int? ModifiedByExUC
        {
            get
            {
                return int_modifiedByUC;
            }
            set
            {
                int_modifiedByUC = value;
            }
        }

        public string CustumMessage
        {
            get
            {
                return str_custMessage;
            }
            internal set
            {
                str_custMessage = value;
            }
        }
        public enum SortingFieldForAttachment
        {

            NoSorting = 0,
            IndustrialNewApplicationCode = 1,
            AttachmentCode = 2,
            AttachmentCodeSerialNumber = 3,
            AttachmentName = 4,
            AttachmentPath = 5
        }
        public RelaxationAttachmentBLL[] RelaxationAttachmentLCollection
        {
            get
            {
                return arr_RelaxationAttachmentLCollection;

            }
            set
            {
                arr_RelaxationAttachmentLCollection = value;
            }
        }
        public RelaxationAttachmentBLL(): base()
        {
            arr_RelaxationAttachmentLCollection = null;
        }
        public RelaxationAttachmentBLL(long lngA_ApplicationCode, int intA_AttachmentCode) : this()
        {
            int intA_AttachmentCodeSerialNumber = 0;
            populateRelaxationAttachmentForCodes(lngA_ApplicationCode, intA_AttachmentCode, intA_AttachmentCodeSerialNumber);

        }

        public RelaxationAttachmentBLL(long lngA_ApplicationCode, int intA_AttachmentCode, int intA_AttachmentCodeSerialNumber) :this()
        {
            
            populateRelaxationAttachmentForCodes(lngA_ApplicationCode, intA_AttachmentCode, intA_AttachmentCodeSerialNumber);

        }

        private int populateRelaxationAttachmentForCodes(long lngA_ApplicationCode = 0, int intA_AttachmentCode = 0, int intA_AttachmentCodeSerialNumber = 0)
        {
            //popuyate desc from dal

            if (lngA_ApplicationCode == 0)
            {
                this.CustumMessage = " Relaxation Application Attachment - IRelaxationApplicationCode required.";
                return 0;
            }

            if (intA_AttachmentCode == 0)
            {
                this.CustumMessage = "Relaxation Application Attachment - AttachmentCode required.";
                return 0;
            }
           

            this.ApplicationCode = lngA_ApplicationCode;
            this.AttachmentCode = intA_AttachmentCode;
            this.AttachmentCodeSerialNumber = intA_AttachmentCodeSerialNumber;



            Relaxation.RelaxationAttachmentDAL obj_RelaxationAttachmentDAL = new Relaxation.RelaxationAttachmentDAL();


            try
            {
                if (obj_RelaxationAttachmentDAL.populateRelaxationAttachmentForAttachmentCode(this) == 0)
                {
                    return 0;
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int Add()
        {
            Relaxation.RelaxationAttachmentDAL obj_RelaxationAttachmentDAL = new Relaxation.RelaxationAttachmentDAL();

            if (this.ApplicationCode == 0)
            {
                this.CustumMessage = "Relaxation Application Attachment - RelaxationApplicationCode should  provided ";
                return 0;

            }

            if ((this.ContentType).Trim().Length == 0)
            {
                this.CustumMessage = "Relaxation Application Attachment - ContentType required.";
                return 0;
            }
            if ((this.FileExtension).Trim().Length == 0)
            {
                this.CustumMessage = "Relaxation Application Attachment - FileExtension required.";
                return 0;
            }
            if ((this.AttachmentPath).Trim().Length == 0)
            {
                this.CustumMessage = "Relaxation Application Attachment - AttachmentPath required.";
                return 0;
            }

            if (this.AttachmentCode == 0)
            {
                this.CustumMessage = "Relaxation Application Attachment - AttachmentCode performed by user required.";
                return 0;

            }
           
            if (this.CreatedByExUC == null && this.CreatedByExUC == null)
            {
                this.CustumMessage = "Relaxation Application Attachment - CreatedByUC or  CreatedByExUC required.";
                return 0;
            }
          
            try
            {
                if (obj_RelaxationAttachmentDAL.AddRealaxationApplicationAttachment(this) == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch
            {
                this.CustumMessage = "AddIndustrial New Application Attachment Problem in BLL";
                return 0;
            }




        }



        public int Delete()
        {
            Relaxation.RelaxationAttachmentDAL obj_RelaxationAttachmentDAL = new Relaxation.RelaxationAttachmentDAL();
            if (this.ApplicationCode == 0)
            {
                this.CustumMessage = "Relaxation ApplicationAttachment - IndustrialNewApplicationCode required through constructor";
                return 0;
            }
            if (this.AttachmentCode == 0)
            {
                this.CustumMessage = "Relaxation ApplicationAttachment - AttachmentCode required through constructor";
                return 0;
            }
            if (this.AttachmentCodeSerialNumber == 0)
            {
                this.CustumMessage = "Relaxation ApplicationAttachment - AttachmentCode required through constructor";
                return 0;
            }
            try
            {

                if (obj_RelaxationAttachmentDAL.DeleteRelaxationAttachment(this) == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }

            catch
            {
                this.CustumMessage = "Application Problem in BLL";
                return 0;
            }
            finally
            {

            }
        }


        public int GetAll(Relaxation.RelaxationAttachmentBLL.SortingFieldForAttachment enuA_SortField = Relaxation.RelaxationAttachmentBLL.SortingFieldForAttachment.NoSorting)
        {
            Relaxation.RelaxationAttachmentDAL obj_RelaxationAttachmentDAL = new Relaxation.RelaxationAttachmentDAL();
            try
            {
                if (obj_RelaxationAttachmentDAL.GetAllRelaxationAttachment(this, enuA_SortField) == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                this.CustumMessage = "Application Problem in BLL";
                return 0;

            }


        }

        public Relaxation.RelaxationAttachmentBLL DownloadFile(long lngA_IndustrialNewApplicationCode = 0, int intA_AttachmentCode = 0, int intA_AttachmentCodeSerialNumber = 0)
        {
            //popuyate desc from dal

            if (lngA_IndustrialNewApplicationCode == 0)
            {
                this.CustumMessage = "Industrial New Application Attachment - IndustrialNewApplicationCode required.";
                return null;
            }

            if (intA_AttachmentCode == 0)
            {
                this.CustumMessage = "Industrial New Application Attachment - AttachmentCode required.";
                return null;
            }
            if (intA_AttachmentCodeSerialNumber == 0)
            {
                this.CustumMessage = "Industrial New Application Attachment - AttachmentCodeSerialNumber required.";
                return null;
            }

            this.ApplicationCode = lngA_IndustrialNewApplicationCode;
            this.AttachmentCode = intA_AttachmentCode;
            this.AttachmentCodeSerialNumber = intA_AttachmentCodeSerialNumber;



            Relaxation.RelaxationAttachmentDAL obj_RelaxationAttachmentDAL = new Relaxation.RelaxationAttachmentDAL();

            try
            {
                Relaxation.RelaxationAttachmentBLL objRelaxationApplicationAttachmentB = obj_RelaxationAttachmentDAL.populateRelaxationAttachedForINDAttachmentFile(lngA_IndustrialNewApplicationCode, intA_AttachmentCode, intA_AttachmentCodeSerialNumber);
                if (objRelaxationApplicationAttachmentB == null)
                {
                    return null;
                }
                return objRelaxationApplicationAttachmentB;
            }
            catch
            {
                return null;
            }
        }

        public Relaxation.RelaxationAttachmentBLL[] GetRelaxationAttachmentList(Relaxation.RelaxationAttachmentBLL.SortingFieldForAttachment enu_SortField = Relaxation.RelaxationAttachmentBLL.SortingFieldForAttachment.NoSorting)
        {
            Relaxation.RelaxationAttachmentBLL[] arr_RelaxationAttachment = null;
            Relaxation.RelaxationAttachmentDAL obj_RelaxationAttachmentDAL = new Relaxation.RelaxationAttachmentDAL();
            try
            {
                arr_RelaxationAttachment = obj_RelaxationAttachmentDAL.GetRelaxtionApplicationAttachmentListForApplicationCode(this, enu_SortField);
                return arr_RelaxationAttachment;
            }
            catch
            {
                this.CustumMessage = "Application Problem in BLL";
                return null;
            }
            finally
            {
                if (arr_RelaxationAttachment != null)
                {
                    arr_RelaxationAttachment = null;
                }
                if (obj_RelaxationAttachmentDAL != null)
                {
                    obj_RelaxationAttachmentDAL = null;
                }
            }
        }

    }

}