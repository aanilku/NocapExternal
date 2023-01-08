using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RelaxationAttachmentDAL
/// </summary>
/// 
namespace Relaxation
{
    public class RelaxationAttachmentDAL
    {
    public int AddRealaxationApplicationAttachment(Relaxation.RelaxationAttachmentBLL objAr_RelaxationAttachmentBLL)
    {
        int int_status = 0;
        string str_dbName = "";
        try
        {
            using (SqlConnection connnFile = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringFile"].ConnectionString))
            {
                str_dbName = connnFile.Database;
            }
            using (SqlConnection connn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd_addDistrict = new SqlCommand("spAddRelaxationAttachment", connn))
                {


                    SqlParameter par_addDistrict = new SqlParameter();
                    connn.Open();

                    int_status = 0;
                    cmd_addDistrict.CommandType = CommandType.StoredProcedure;
                    par_addDistrict = cmd_addDistrict.Parameters.AddWithValue("@bintAppCode", objAr_RelaxationAttachmentBLL.ApplicationCode);
                    par_addDistrict.Direction = ParameterDirection.Input;
                    par_addDistrict = cmd_addDistrict.Parameters.AddWithValue("@intAttCode", objAr_RelaxationAttachmentBLL.AttachmentCode);
                    par_addDistrict.Direction = ParameterDirection.Input;
                    par_addDistrict = cmd_addDistrict.Parameters.AddWithValue("@strAttName", objAr_RelaxationAttachmentBLL.AttachmentName);
                    par_addDistrict.Direction = ParameterDirection.Input;
                    par_addDistrict = cmd_addDistrict.Parameters.AddWithValue("@strAttPath", objAr_RelaxationAttachmentBLL.AttachmentPath);
                    par_addDistrict.Direction = ParameterDirection.Input;
                    par_addDistrict = cmd_addDistrict.Parameters.AddWithValue("@intCreatedByUC", objAr_RelaxationAttachmentBLL.CreatedByUC);
                    par_addDistrict.Direction = ParameterDirection.Input;
                    par_addDistrict = cmd_addDistrict.Parameters.AddWithValue("@bintCreatedByExUC", objAr_RelaxationAttachmentBLL.CreatedByExUC);
                    par_addDistrict.Direction = ParameterDirection.Input;


                        par_addDistrict = cmd_addDistrict.Parameters.AddWithValue("@strDatabaseName", str_dbName);
                    par_addDistrict.Direction = ParameterDirection.Input;

                    //par_addDistrict = cmd_addDistrict.Parameters.AddWithValue("@int_AttCodeSNOut", SqlDbType.Int);
                    //par_addDistrict.Direction = ParameterDirection.Output;




                    par_addDistrict = cmd_addDistrict.Parameters.AddWithValue("@byteAttachmentFile", objAr_RelaxationAttachmentBLL.AttachmentFile);
                    par_addDistrict.Direction = ParameterDirection.Input;
                    par_addDistrict = cmd_addDistrict.Parameters.AddWithValue("@strFileExtension", objAr_RelaxationAttachmentBLL.FileExtension);
                    par_addDistrict.Direction = ParameterDirection.Input;
                    par_addDistrict = cmd_addDistrict.Parameters.AddWithValue("@strContentType", objAr_RelaxationAttachmentBLL.ContentType);
                    par_addDistrict.Direction = ParameterDirection.Input;


                    par_addDistrict = cmd_addDistrict.Parameters.Add("@strCustMsg", SqlDbType.VarChar, 50);
                    par_addDistrict.Direction = ParameterDirection.Output;
                    par_addDistrict = cmd_addDistrict.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
                    par_addDistrict.Direction = ParameterDirection.ReturnValue;
                    cmd_addDistrict.ExecuteNonQuery();
                    connn.Close();
                    //int_AttCodeSN = Convert.ToInt32(cmd_addDistrict.Parameters["@int_AttCodeSNOut"].Value);
                    objAr_RelaxationAttachmentBLL.CustumMessage = cmd_addDistrict.Parameters["@strCustMsg"].Value.ToString();
                    int_status = Convert.ToInt32(cmd_addDistrict.Parameters["RETURN_VALUE"].Value.ToString());
                    return int_status;
                }
            }



        }
        catch (Exception ex)
        {
            objAr_RelaxationAttachmentBLL.CustumMessage = "Industrial New Application Attachment Problem in DAL" + ex.Message;
            return int_status;
        }
        finally
        {

        }
    }
    public int populateRelaxationAttachmentForAttachmentCode(Relaxation.RelaxationAttachmentBLL objAr_RelaxationAttachmentBLL)
    {
        SqlConnection connn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        int int_connOpenOrNot = 0;
        int int_status = 0;
        SqlCommand cmd_populateINDAttachmentForINDAttachmentCode = new SqlCommand("spPopulateRelaxationAttachmentForRelaxationAttachmentCode", connn);
        SqlParameter par_populateINDAttachmentForINDAttachmentCode = new SqlParameter();
        try
        {
            connn.Open();
            int_connOpenOrNot = 1;
            int_status = 0;
            cmd_populateINDAttachmentForINDAttachmentCode.CommandType = CommandType.StoredProcedure;
            par_populateINDAttachmentForINDAttachmentCode = cmd_populateINDAttachmentForINDAttachmentCode.Parameters.AddWithValue("@bintAppCode", objAr_RelaxationAttachmentBLL.ApplicationCode);
            par_populateINDAttachmentForINDAttachmentCode.Direction = ParameterDirection.Input;
            par_populateINDAttachmentForINDAttachmentCode = cmd_populateINDAttachmentForINDAttachmentCode.Parameters.AddWithValue("@intAttCode", objAr_RelaxationAttachmentBLL.AttachmentCode);
            par_populateINDAttachmentForINDAttachmentCode.Direction = ParameterDirection.Input;
            par_populateINDAttachmentForINDAttachmentCode = cmd_populateINDAttachmentForINDAttachmentCode.Parameters.AddWithValue("@intAttCodeSN", objAr_RelaxationAttachmentBLL.AttachmentCodeSerialNumber);
            par_populateINDAttachmentForINDAttachmentCode.Direction = ParameterDirection.Input;
            par_populateINDAttachmentForINDAttachmentCode = cmd_populateINDAttachmentForINDAttachmentCode.Parameters.Add("@strAttName", SqlDbType.NVarChar, 50);
            par_populateINDAttachmentForINDAttachmentCode.Direction = ParameterDirection.Output;
            par_populateINDAttachmentForINDAttachmentCode = cmd_populateINDAttachmentForINDAttachmentCode.Parameters.Add("@strAttPath", SqlDbType.NVarChar, 200);
            par_populateINDAttachmentForINDAttachmentCode.Direction = ParameterDirection.Output;
            par_populateINDAttachmentForINDAttachmentCode = cmd_populateINDAttachmentForINDAttachmentCode.Parameters.Add("@dtCreatedOnByUC", SqlDbType.DateTime);
            par_populateINDAttachmentForINDAttachmentCode.Direction = ParameterDirection.Output;
            par_populateINDAttachmentForINDAttachmentCode = cmd_populateINDAttachmentForINDAttachmentCode.Parameters.Add("@intCreatedByUC", SqlDbType.Int);
            par_populateINDAttachmentForINDAttachmentCode.Direction = ParameterDirection.Output;
            par_populateINDAttachmentForINDAttachmentCode = cmd_populateINDAttachmentForINDAttachmentCode.Parameters.Add("@dtCreatedOnByExUC", SqlDbType.DateTime);
            par_populateINDAttachmentForINDAttachmentCode.Direction = ParameterDirection.Output;
            par_populateINDAttachmentForINDAttachmentCode = cmd_populateINDAttachmentForINDAttachmentCode.Parameters.Add("@bintCreatedByExUC", SqlDbType.BigInt);
            par_populateINDAttachmentForINDAttachmentCode.Direction = ParameterDirection.Output;
            par_populateINDAttachmentForINDAttachmentCode = cmd_populateINDAttachmentForINDAttachmentCode.Parameters.Add("@dtModifiedOnByUC", SqlDbType.DateTime);
            par_populateINDAttachmentForINDAttachmentCode.Direction = ParameterDirection.Output;
            par_populateINDAttachmentForINDAttachmentCode = cmd_populateINDAttachmentForINDAttachmentCode.Parameters.Add("@intModifiedByUC", SqlDbType.Int);
            par_populateINDAttachmentForINDAttachmentCode.Direction = ParameterDirection.Output;
            par_populateINDAttachmentForINDAttachmentCode = cmd_populateINDAttachmentForINDAttachmentCode.Parameters.Add("@dtModifiedOnByExUC", SqlDbType.DateTime);
            par_populateINDAttachmentForINDAttachmentCode.Direction = ParameterDirection.Output;
            par_populateINDAttachmentForINDAttachmentCode = cmd_populateINDAttachmentForINDAttachmentCode.Parameters.Add("@bintModifiedByExUC", SqlDbType.BigInt);
            par_populateINDAttachmentForINDAttachmentCode.Direction = ParameterDirection.Output;
            par_populateINDAttachmentForINDAttachmentCode = cmd_populateINDAttachmentForINDAttachmentCode.Parameters.Add("@strCustMsg", SqlDbType.VarChar, 50);
            par_populateINDAttachmentForINDAttachmentCode.Direction = ParameterDirection.Output;
            par_populateINDAttachmentForINDAttachmentCode = cmd_populateINDAttachmentForINDAttachmentCode.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
            par_populateINDAttachmentForINDAttachmentCode.Direction = ParameterDirection.ReturnValue;
            cmd_populateINDAttachmentForINDAttachmentCode.ExecuteNonQuery();
            connn.Close();
            objAr_RelaxationAttachmentBLL.ApplicationCode = Convert.ToInt64(cmd_populateINDAttachmentForINDAttachmentCode.Parameters["@bintAppCode"].Value);
            objAr_RelaxationAttachmentBLL.AttachmentCode = Convert.ToInt32(cmd_populateINDAttachmentForINDAttachmentCode.Parameters["@intAttCode"].Value);
            objAr_RelaxationAttachmentBLL.AttachmentCodeSerialNumber = Convert.ToInt32(cmd_populateINDAttachmentForINDAttachmentCode.Parameters["@intAttCodeSN"].Value);

            objAr_RelaxationAttachmentBLL.AttachmentName = cmd_populateINDAttachmentForINDAttachmentCode.Parameters["@strAttName"].Value.ToString();
            objAr_RelaxationAttachmentBLL.AttachmentPath = cmd_populateINDAttachmentForINDAttachmentCode.Parameters["@strAttPath"].Value.ToString();

            if (Convert.IsDBNull(cmd_populateINDAttachmentForINDAttachmentCode.Parameters["@dtCreatedOnByUC"].Value))
            {
                objAr_RelaxationAttachmentBLL.CreatedOnByUC = null;
            }
            else
            {
                objAr_RelaxationAttachmentBLL.CreatedOnByUC = Convert.ToDateTime(cmd_populateINDAttachmentForINDAttachmentCode.Parameters["@dtCreatedOnByUC"].Value);
            }

            if (Convert.IsDBNull(cmd_populateINDAttachmentForINDAttachmentCode.Parameters["@bintCreatedByExUC"].Value))
            {
                objAr_RelaxationAttachmentBLL.CreatedByExUC = null;
            }
            else
            {
                objAr_RelaxationAttachmentBLL.CreatedByExUC = Convert.ToInt32(cmd_populateINDAttachmentForINDAttachmentCode.Parameters["@bintCreatedByExUC"].Value);
            }



           
            if (Convert.IsDBNull(cmd_populateINDAttachmentForINDAttachmentCode.Parameters["@dtModifiedOnByUC"].Value))
            {
                objAr_RelaxationAttachmentBLL.ModifiedOnByUC = null;
            }
            else
            {
                objAr_RelaxationAttachmentBLL.ModifiedOnByUC = Convert.ToDateTime(cmd_populateINDAttachmentForINDAttachmentCode.Parameters["@dtModifiedOnByUC"].Value);
            }

            if (Convert.IsDBNull(cmd_populateINDAttachmentForINDAttachmentCode.Parameters["@intModifiedByUC"].Value))
            {
                objAr_RelaxationAttachmentBLL.ModifiedByExUC = null;
            }
            else
            {
                objAr_RelaxationAttachmentBLL.ModifiedByExUC = Convert.ToInt32(cmd_populateINDAttachmentForINDAttachmentCode.Parameters["@intModifiedByUC"].Value);
            }

          
            objAr_RelaxationAttachmentBLL.CustumMessage = cmd_populateINDAttachmentForINDAttachmentCode.Parameters["@strCustMsg"].Value.ToString();
            int_status = Convert.ToInt32(cmd_populateINDAttachmentForINDAttachmentCode.Parameters["RETURN_VALUE"].Value.ToString());

            return int_status;
        }
        catch (Exception ex)
        {
            objAr_RelaxationAttachmentBLL.CustumMessage = "Industrial New Application Attachment Problem in DAL" + ex.Message;
            if (int_connOpenOrNot == 1)
            {
                connn.Close();
            }
            return int_status;
        }
        finally
        {
            if (cmd_populateINDAttachmentForINDAttachmentCode != null)
            {
                cmd_populateINDAttachmentForINDAttachmentCode.Dispose();
            }
            if (connn != null)
            {
                connn.Dispose();
            }

        }
    }

    public int DeleteRelaxationAttachment(Relaxation.RelaxationAttachmentBLL objAr_RelaxationAttachmentBLL)
    {

        int int_status = 0;
        string str_dbName = "";
        try
        {


            using (SqlConnection connn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringFile"].ConnectionString))
            {
                str_dbName = connn.Database;
            }
            using (SqlConnection connn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {



                using (SqlCommand cmd_deleteIndustrialNewApplicationAttachment = new SqlCommand("spDeleteRelaxationAttachment", connn))
                {
                    SqlParameter par_deleteIndustrialNewApplicationAttachment = new SqlParameter();

                    connn.Open();
                    cmd_deleteIndustrialNewApplicationAttachment.CommandType = CommandType.StoredProcedure;
                    par_deleteIndustrialNewApplicationAttachment = cmd_deleteIndustrialNewApplicationAttachment.Parameters.AddWithValue("@bintAppCode", objAr_RelaxationAttachmentBLL.ApplicationCode);
                    par_deleteIndustrialNewApplicationAttachment.Direction = ParameterDirection.Input;
                    par_deleteIndustrialNewApplicationAttachment = cmd_deleteIndustrialNewApplicationAttachment.Parameters.AddWithValue("@intAttCode", objAr_RelaxationAttachmentBLL.AttachmentCode);
                    par_deleteIndustrialNewApplicationAttachment.Direction = ParameterDirection.Input;
                    par_deleteIndustrialNewApplicationAttachment = cmd_deleteIndustrialNewApplicationAttachment.Parameters.AddWithValue("@intAttCodeSN", objAr_RelaxationAttachmentBLL.AttachmentCodeSerialNumber);
                    par_deleteIndustrialNewApplicationAttachment.Direction = ParameterDirection.Input;
                    par_deleteIndustrialNewApplicationAttachment = cmd_deleteIndustrialNewApplicationAttachment.Parameters.AddWithValue("@strDatabaseName", str_dbName);
                    par_deleteIndustrialNewApplicationAttachment.Direction = ParameterDirection.Input;
                    par_deleteIndustrialNewApplicationAttachment = cmd_deleteIndustrialNewApplicationAttachment.Parameters.Add("@strCustMsg", SqlDbType.VarChar, 50);
                    par_deleteIndustrialNewApplicationAttachment.Direction = ParameterDirection.Output;
                    par_deleteIndustrialNewApplicationAttachment = cmd_deleteIndustrialNewApplicationAttachment.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
                    par_deleteIndustrialNewApplicationAttachment.Direction = ParameterDirection.ReturnValue;
                    cmd_deleteIndustrialNewApplicationAttachment.ExecuteNonQuery();
                    connn.Close();
                    objAr_RelaxationAttachmentBLL.CustumMessage = cmd_deleteIndustrialNewApplicationAttachment.Parameters["@strCustMsg"].Value.ToString();
                    int_status = Convert.ToInt32(cmd_deleteIndustrialNewApplicationAttachment.Parameters["RETURN_VALUE"].Value.ToString());
                    connn.Close();
                    return int_status;


                }

            }



        }
        catch (Exception ex)
        {
            objAr_RelaxationAttachmentBLL.CustumMessage = "Industrial New Application Attachment Problem in DAL" + ex.Message;
            return int_status;
        }
    }

    public int GetAllRelaxationAttachment(Relaxation.RelaxationAttachmentBLL objAr_RelaxationAttachmentBLL, Relaxation.RelaxationAttachmentBLL.SortingFieldForAttachment enuA_SortField = Relaxation.RelaxationAttachmentBLL.SortingFieldForAttachment.NoSorting)
    {
        int int_status = 0;
        string str_custumMessage = "";
        Relaxation.RelaxationAttachmentBLL[] arr_tempIndustrialNewApplicationAttachmentListBLL = null;
        try
        {

            arr_tempIndustrialNewApplicationAttachmentListBLL = GetRelaxationAttachmentForKeys(out str_custumMessage, out int_status, enuA_SortField: enuA_SortField);
            objAr_RelaxationAttachmentBLL.RelaxationAttachmentLCollection = arr_tempIndustrialNewApplicationAttachmentListBLL;
            objAr_RelaxationAttachmentBLL.CustumMessage = str_custumMessage;
            return int_status;
        }
        catch (Exception ex)
        {
            objAr_RelaxationAttachmentBLL.CustumMessage = ex.Message;
            return int_status;

        }
        finally
        {
            if (arr_tempIndustrialNewApplicationAttachmentListBLL != null)
            {
                arr_tempIndustrialNewApplicationAttachmentListBLL = null;
            }
        }

    }


    public Relaxation.RelaxationAttachmentBLL[] GetRelaxationAttachmentForKeys(out string strA_custumMessage, out int intA_status, long? lngA_FilterINDAppCode = null, int? intA_FilterAttCode = null, int? intA_FilterAttCodeSN = null, Relaxation.RelaxationAttachmentBLL.SortingFieldForAttachment enuA_SortField = Relaxation.RelaxationAttachmentBLL.SortingFieldForAttachment.NoSorting)
    {


        List<Relaxation.RelaxationAttachmentBLL> list_tempIndustrialNewApplicationAttachmentListBLL = new List<Relaxation.RelaxationAttachmentBLL>();

        SqlConnection connn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        int int_connOpenOrNot = 0;

        SqlCommand cmd_getAllIndustrialNewApplicationList = new SqlCommand("spGetRelaxationAttachmentsList", connn);

        SqlParameter par_SortField = new SqlParameter();

        SqlDataReader dr = null;





        par_SortField.Direction = ParameterDirection.Input;

        switch (enuA_SortField)
        {
            case Relaxation.RelaxationAttachmentBLL.SortingFieldForAttachment.IndustrialNewApplicationCode:
                par_SortField = new SqlParameter("@strSortFieldName", "AppCode");
                break;
            case Relaxation.RelaxationAttachmentBLL.SortingFieldForAttachment.AttachmentCode:
                par_SortField = new SqlParameter("@strSortFieldName", "AttCode");
                break;
            case Relaxation.RelaxationAttachmentBLL.SortingFieldForAttachment.AttachmentCodeSerialNumber:
                par_SortField = new SqlParameter("@strSortFieldName", "AttCodeSN");
                break;
            case Relaxation.RelaxationAttachmentBLL.SortingFieldForAttachment.AttachmentName:
                par_SortField = new SqlParameter("@strSortFieldName", "AttName");
                break;
            case Relaxation.RelaxationAttachmentBLL.SortingFieldForAttachment.AttachmentPath:
                par_SortField = new SqlParameter("@strSortFieldName", "AttPath");
                break;
            case Relaxation.RelaxationAttachmentBLL.SortingFieldForAttachment.NoSorting:
                par_SortField = new SqlParameter("@strSortFieldName", "");
                break;
            default:
                par_SortField = new SqlParameter("@strSortFieldName", "");
                break;

        }


        par_SortField.Direction = ParameterDirection.Input;



            

        ///filter

        SqlParameter par_FilterParamForINDAppCode = new SqlParameter();

        SqlParameter par_FilterParamForAttCode = new SqlParameter();
        SqlParameter par_FilterParamForAttCodeSN = new SqlParameter();

        par_FilterParamForINDAppCode = new SqlParameter("@bintFilterINDAppCode", lngA_FilterINDAppCode);
        par_FilterParamForINDAppCode.Direction = ParameterDirection.Input;

        par_FilterParamForAttCode = new SqlParameter("@intFilterAttCode", intA_FilterAttCode);
        par_FilterParamForAttCode.Direction = ParameterDirection.Input;


        SqlParameter par_ReturnVal = new SqlParameter("RETURN_VALUE", SqlDbType.Int);
        par_ReturnVal.Direction = ParameterDirection.ReturnValue;



            

        try
        {


            connn.Open();
            int_connOpenOrNot = 1;
            cmd_getAllIndustrialNewApplicationList.CommandType = CommandType.StoredProcedure;
            cmd_getAllIndustrialNewApplicationList.Parameters.Add(par_SortField);



            if (lngA_FilterINDAppCode != null)
            {
                cmd_getAllIndustrialNewApplicationList.Parameters.Add(par_FilterParamForINDAppCode);
            }

            if (intA_FilterAttCode != null)
            {
                cmd_getAllIndustrialNewApplicationList.Parameters.Add(par_FilterParamForAttCode);
            }

            if (intA_FilterAttCodeSN != null)
            {
                cmd_getAllIndustrialNewApplicationList.Parameters.Add(par_FilterParamForAttCodeSN);
            }


            cmd_getAllIndustrialNewApplicationList.Parameters.Add(par_ReturnVal);

            //SqlDataReader dr = cmd_getAllAreaTypeList.ExecuteReader();
            dr = cmd_getAllIndustrialNewApplicationList.ExecuteReader();

            if (!(dr.HasRows))
            {
                //objAr_ApplicationTypeBLL.ApplicationTypeCollection =null;

            }

            while (dr.Read())
            {
               Relaxation.RelaxationAttachmentBLL obj_IndustrialNewApplicationAttachmentBLL = new Relaxation.RelaxationAttachmentBLL();
                obj_IndustrialNewApplicationAttachmentBLL.ApplicationCode = Convert.ToInt64(dr["AppCode"]);
                obj_IndustrialNewApplicationAttachmentBLL.AttachmentCode = Convert.ToInt32(dr["AttCode"]);
                obj_IndustrialNewApplicationAttachmentBLL.AttachmentCodeSerialNumber = Convert.ToInt32(dr["AttCodeSN"]);
                obj_IndustrialNewApplicationAttachmentBLL.AttachmentName = dr["AttName"].ToString();
                obj_IndustrialNewApplicationAttachmentBLL.AttachmentPath = dr["AttPath"].ToString();

                if (Convert.IsDBNull(dr["CreatedOnByUC"]))
                {
                    obj_IndustrialNewApplicationAttachmentBLL.CreatedOnByUC = null;
                }
                else
                {
                    obj_IndustrialNewApplicationAttachmentBLL.CreatedOnByUC = Convert.ToDateTime(dr["CreatedOnByUC"]);
                }

                if (Convert.IsDBNull(dr["CreatedByUC"]))
                {
                    obj_IndustrialNewApplicationAttachmentBLL.CreatedByExUC = null;
                }
                else
                {
                    obj_IndustrialNewApplicationAttachmentBLL.CreatedByExUC = Convert.ToInt32(dr["CreatedByUC"]);
                }

              
                if (Convert.IsDBNull(dr["CreatedByExUC"]))
                {
                    obj_IndustrialNewApplicationAttachmentBLL.CreatedByExUC = null;
                }
                else
                {
                    obj_IndustrialNewApplicationAttachmentBLL.CreatedByExUC = Convert.ToInt64(dr["CreatedByExUC"]);
                }

                if (Convert.IsDBNull(dr["ModifiedOnByUC"]))
                {
                    obj_IndustrialNewApplicationAttachmentBLL.ModifiedOnByUC = null;
                }
                else
                {
                    obj_IndustrialNewApplicationAttachmentBLL.ModifiedOnByUC = Convert.ToDateTime(dr["ModifiedOnByUC"]);
                }
                if (Convert.IsDBNull(dr["ModifiedByUC"]))
                {
                    obj_IndustrialNewApplicationAttachmentBLL.ModifiedByExUC = null;
                }
                else
                {
                    obj_IndustrialNewApplicationAttachmentBLL.ModifiedByExUC = Convert.ToInt32(dr["ModifiedByUC"]);
                }

               

                obj_IndustrialNewApplicationAttachmentBLL.CustumMessage = "";
                list_tempIndustrialNewApplicationAttachmentListBLL.Add(obj_IndustrialNewApplicationAttachmentBLL);

            }


            dr.Close();
            connn.Close();
            intA_status = Convert.ToInt32((par_ReturnVal.Value));

           Relaxation.RelaxationAttachmentBLL[] arr_tempIndustrialNewApplicationAttachmentListBLL = new Relaxation.RelaxationAttachmentBLL[list_tempIndustrialNewApplicationAttachmentListBLL.Count];


            list_tempIndustrialNewApplicationAttachmentListBLL.CopyTo(arr_tempIndustrialNewApplicationAttachmentListBLL);


            strA_custumMessage = "Successfully find the Industrial NewApplication Attachment";
            return arr_tempIndustrialNewApplicationAttachmentListBLL;


        }
        catch (Exception ex)
        {

            //objAr_IndustrialNewLandUseBLL.IndustrialNewLandUseCollection = null;
            strA_custumMessage = ex.Message;

            if (int_connOpenOrNot == 1)
            {
                connn.Close();
            }
            intA_status = 0;
            return null;


        }
        finally
        {
            if (cmd_getAllIndustrialNewApplicationList != null)
            {
                cmd_getAllIndustrialNewApplicationList.Dispose();
            }
            if (connn != null)
            {
                connn.Dispose();
            }
            if (list_tempIndustrialNewApplicationAttachmentListBLL != null)
            {
                list_tempIndustrialNewApplicationAttachmentListBLL.Clear();
            }

            if (dr != null)
            {
                dr.Dispose();
            }
        }

            

    }

    public Relaxation.RelaxationAttachmentBLL populateRelaxationAttachedForINDAttachmentFile(long lng_industrialNewApplicationCode, int int_attachmentCode, int int_attachmentCodeSerialNumbe)
        {
            SqlConnection connn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringFile"].ConnectionString);
            int int_connOpenOrNot = 0;
            //int int_status = 0;
            SqlCommand cmd_populateINDAttachmentForINDAttachmentCode = new SqlCommand("spGetRelaxationAttachmentFile", connn);
            SqlParameter par_populateINDAttachmentForINDAttachmentCode = new SqlParameter();
            SqlDataReader dr = null;
            Relaxation.RelaxationAttachmentBLL objIndustrialNewApplicationAttachmentB = null;
            try
            {
                connn.Open();
                int_connOpenOrNot = 1;
                // int_status = 0;

                cmd_populateINDAttachmentForINDAttachmentCode.CommandType = CommandType.StoredProcedure;
                cmd_populateINDAttachmentForINDAttachmentCode.Parameters.AddWithValue("@bintAppCode", lng_industrialNewApplicationCode);
                cmd_populateINDAttachmentForINDAttachmentCode.Parameters.AddWithValue("@intAttCode", int_attachmentCode);
                cmd_populateINDAttachmentForINDAttachmentCode.Parameters.AddWithValue("@intAttCodeSN", int_attachmentCodeSerialNumbe);



                dr = cmd_populateINDAttachmentForINDAttachmentCode.ExecuteReader();
                while (dr.Read())
                {
                    objIndustrialNewApplicationAttachmentB = ExtLoginHistoryFillDataRecord(dr);

                }


                return objIndustrialNewApplicationAttachmentB;
            }
            catch (Exception ex)
            {
                //objAr_IndustrialNewApplicationAttachmentBLL.CustumMessage = "Industrial New Application Attachment Problem in DAL" + ex.Message;
                if (int_connOpenOrNot == 1)
                {
                    connn.Close();
                }
                return objIndustrialNewApplicationAttachmentB;
            }
            finally
            {
                if (cmd_populateINDAttachmentForINDAttachmentCode != null)
                {
                    cmd_populateINDAttachmentForINDAttachmentCode.Dispose();
                }
                if (connn != null)
                {
                    connn.Dispose();
                }
            }
        }

    private static Relaxation.RelaxationAttachmentBLL ExtLoginHistoryFillDataRecord(IDataRecord myDataRecord)
    {
        try
        {

            Relaxation.RelaxationAttachmentBLL objIndustrialNewApplicationAttachmentB = new Relaxation.RelaxationAttachmentBLL();
            objIndustrialNewApplicationAttachmentB.AttachmentFile = (byte[])myDataRecord["AttFile"];
            objIndustrialNewApplicationAttachmentB.ContentType = myDataRecord.GetString(myDataRecord.GetOrdinal("ContentType"));
            objIndustrialNewApplicationAttachmentB.FileExtension = myDataRecord.GetString(myDataRecord.GetOrdinal("FileExtension"));
            return objIndustrialNewApplicationAttachmentB;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Relaxation.RelaxationAttachmentBLL[] GetRelaxtionApplicationAttachmentListForApplicationCode(Relaxation.RelaxationAttachmentBLL objAr_RelaxationAttachmentBLL, Relaxation.RelaxationAttachmentBLL.SortingFieldForAttachment enuA_SortField = Relaxation.RelaxationAttachmentBLL.SortingFieldForAttachment.NoSorting)
        {

            int int_status = 0;
            string str_custumMessage = "";
            Relaxation.RelaxationAttachmentBLL[] arr_tempRelaxationAttachmentApplicationAttachmentListBLL = null;
            Relaxation.RelaxationAttachmentDAL obj_RelaxationAttachmentApplicationAttachmentDAL = new Relaxation.RelaxationAttachmentDAL();

            try
            {

                arr_tempRelaxationAttachmentApplicationAttachmentListBLL = obj_RelaxationAttachmentApplicationAttachmentDAL.GetRelaxationAttachmentForKeys(out str_custumMessage, out int_status, lngA_FilterINDAppCode: objAr_RelaxationAttachmentBLL.ApplicationCode,  enuA_SortField: enuA_SortField);
                objAr_RelaxationAttachmentBLL.CustumMessage = str_custumMessage;
                return arr_tempRelaxationAttachmentApplicationAttachmentListBLL;
            }
            catch (Exception ex)
            {
                objAr_RelaxationAttachmentBLL.CustumMessage = ex.Message;
                return null;

            }
            finally
            {
                if (arr_tempRelaxationAttachmentApplicationAttachmentListBLL != null)
                {
                    arr_tempRelaxationAttachmentApplicationAttachmentListBLL = null;
                }
                if (obj_RelaxationAttachmentApplicationAttachmentDAL != null)
                {
                    obj_RelaxationAttachmentApplicationAttachmentDAL = null;
                }
            }


        }



    }


}