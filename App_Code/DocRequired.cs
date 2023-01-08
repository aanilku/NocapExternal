using NOCAP.BLL.DocumentRequired;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for DocumentRequired
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class DocRequired : System.Web.Services.WebService
{

    public DocRequired()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<NOCAP.BLL.DocumentRequired.DocumentRequired> bindGridDocumentRequiredINDList(string AppTypeCode, string AppPurposeCode, string WDKLDCODE)
    {
        NOCAP.BLL.DocumentRequired.DocumentRequired[] arrLatestUpdateList = null;
        var IntApplicationTypeCodes = "";
        try
        {


            NOCAP.BLL.DocumentRequired.DocumentRequired obj_DocumentRequired = new NOCAP.BLL.DocumentRequired.DocumentRequired();
            arrLatestUpdateList = obj_DocumentRequired.GetDocumentRequiredList(NOCAP.BLL.DocumentRequired.DocumentRequired.SortingField.NoSorting);

            if (arrLatestUpdateList != null)
            {
                List<DocumentRequired> listadd = new List<DocumentRequired>();
                DocumentRequired obj = null;

                for (int i = 0; i < arrLatestUpdateList.Length; i++)
                {
                    if (AppTypeCode == Convert.ToString(arrLatestUpdateList[i].ApplicationTypeCodes) && AppPurposeCode == Convert.ToString(arrLatestUpdateList[i].ApplicationPurposes))
                    {
                        obj = new NOCAP.BLL.DocumentRequired.DocumentRequired();
                        if (WDKLDCODE == "WDLessThen10KLD")
                        {
                            if (arrLatestUpdateList[i].WDLessThen10KLD == NOCAP.BLL.DocumentRequired.DocumentRequired.WDLessThen10KLDYesNo.Yes)
                            {
                                obj.DocumentText = arrLatestUpdateList[i].DocumentText;
                                obj.DateTimeFormet = Convert.ToDateTime(arrLatestUpdateList[i].CreatedOn).ToShortDateString();
                                obj.DocumentCode = arrLatestUpdateList[i].DocumentCode;

                                obj.Str_ApplicationType = Convert.ToString(arrLatestUpdateList[i].ApplicationTypeCodes);
                                obj.Str_ApplicationPurpose = Convert.ToString(arrLatestUpdateList[i].ApplicationPurposes);

                                obj.Str_WDLessThen10KLD = Convert.ToString(arrLatestUpdateList[i].WDLessThen10KLD);
                                obj.Str_WDBT10To100KLD = Convert.ToString(arrLatestUpdateList[i].WDBT10To100KLD);
                                obj.Str_WDMoreThen100KLD = Convert.ToString(arrLatestUpdateList[i].WDMoreThen100KLD);

                                listadd.Add(obj);
                            }
                        }
                        else if (WDKLDCODE == "WDBT10To100KLD")
                        {
                            if (arrLatestUpdateList[i].WDBT10To100KLD == NOCAP.BLL.DocumentRequired.DocumentRequired.WDBT10To100KLDYesNo.Yes)
                            {
                                obj.DocumentText = arrLatestUpdateList[i].DocumentText;
                                obj.DateTimeFormet = Convert.ToDateTime(arrLatestUpdateList[i].CreatedOn).ToShortDateString();
                                obj.DocumentCode = arrLatestUpdateList[i].DocumentCode;

                                obj.Str_ApplicationType = Convert.ToString(arrLatestUpdateList[i].ApplicationTypeCodes);
                                obj.Str_ApplicationPurpose = Convert.ToString(arrLatestUpdateList[i].ApplicationPurposes);

                                obj.Str_WDLessThen10KLD = Convert.ToString(arrLatestUpdateList[i].WDLessThen10KLD);
                                obj.Str_WDBT10To100KLD = Convert.ToString(arrLatestUpdateList[i].WDBT10To100KLD);
                                obj.Str_WDMoreThen100KLD = Convert.ToString(arrLatestUpdateList[i].WDMoreThen100KLD);

                                listadd.Add(obj);
                            }
                        }
                        else if (WDKLDCODE == "WDMoreThen100KLD")
                        {
                            if (arrLatestUpdateList[i].WDMoreThen100KLD == NOCAP.BLL.DocumentRequired.DocumentRequired.WDMoreThen100KLDYesNo.Yes)
                            {
                                obj.DocumentText = arrLatestUpdateList[i].DocumentText;
                                obj.DateTimeFormet = Convert.ToDateTime(arrLatestUpdateList[i].CreatedOn).ToShortDateString();
                                obj.DocumentCode = arrLatestUpdateList[i].DocumentCode;

                                obj.Str_ApplicationType = Convert.ToString(arrLatestUpdateList[i].ApplicationTypeCodes);
                                obj.Str_ApplicationPurpose = Convert.ToString(arrLatestUpdateList[i].ApplicationPurposes);

                                obj.Str_WDLessThen10KLD = Convert.ToString(arrLatestUpdateList[i].WDLessThen10KLD);
                                obj.Str_WDBT10To100KLD = Convert.ToString(arrLatestUpdateList[i].WDBT10To100KLD);
                                obj.Str_WDMoreThen100KLD = Convert.ToString(arrLatestUpdateList[i].WDMoreThen100KLD);

                                listadd.Add(obj);
                            }
                        }

                    }

                }

                return listadd;

            }
            else
            {
                return null;
            }

        }
        catch (Exception ex)
        {
            return null;
        }
    }

}
