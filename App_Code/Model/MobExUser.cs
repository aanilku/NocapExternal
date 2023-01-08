using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MobExUser
/// </summary>
public class MobExUser
{
    private string str_exUserName;
    //private string str_password;
    private string str_exMobileNumber;
    private string str_addressLine1;
    private string str_addressLine2;
    private string str_addressLine3;
    public MobExUser()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string CustumMessage
    {
        get
        ;
        internal set
       ;
    }
    public string UserName
    {
        get
        {
            return str_exUserName;

        }
        set
        {
            if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 30) == 1)
            {
                str_exUserName = value.Trim();
            }
            else
            {
                str_exUserName = "";
                this.CustumMessage = "External User Password- External User Password not have more than 30 characters.";
                throw new ApplicationException("External User Password- External User Password have more than 30 characters.");
            }

        }
    }
    public string password {
        get; set; }
    public string MobileNumber
    {
        get
        {
            return str_exMobileNumber;
        }
        set
        {
            if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 10) == 1)
            {
                str_exMobileNumber = value.Trim();
            }
            else
            {
                str_exMobileNumber = "";
                this.CustumMessage = "External User Mobile Number- External User Mobile Number not have more than 10 characters.";
                throw new ApplicationException("External User Mobile Number- External User Mobile Number have more than 10 characters.");
            }
        }
    }
    public string AddressLine1
    {
        get
        {
            return str_addressLine1;
        }
        set
        {
            if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 100) == 1)
            {
                str_addressLine1 = value.Trim();
            }
            else
            {
                str_addressLine1 = "";
                this.CustumMessage = "External User AddressLine1- External User AddressLine1 not have more than 100 characters.";
                throw new ApplicationException("External User AddressLine1- External User AddressLine1 have more than 100 characters.");
            }
        }
    }

    public string AddressLine2
    {
        get
        {
            return str_addressLine2;
        }
        set
        {
            if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 100) == 1)
            {
                str_addressLine2 = value.Trim();
            }
            else
            {
                str_addressLine2 = "";
                this.CustumMessage = "External User AddressLine2- External User AddressLine2 not have more than 100 characters.";
                throw new ApplicationException("External User AddressLine2- External User AddressLine2 have more than 100 characters.");
            }
        }
    }

    public string AddressLine3
    {
        get
        {
            return str_addressLine3;
        }
        set
        {
            if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 100) == 1)
            {
                str_addressLine3 = value.Trim();
            }
            else
            {
                str_addressLine3 = "";
                this.CustumMessage = "External User AddressLine3- External User AddressLine3 not have more than 100 characters.";
                throw new ApplicationException("External User AddressLine3- External User AddressLine3 have more than 100 characters.");
            }
        }
    }

    public string StateName
    {
        get
        ;
        set
       ;
    }

    public string DistrictName
    {
        get
        ;
        set
        ;
    }

    public string SubDistrictName
    {
        get
        ;
        set
       ;
    }

    public int PinCode
    {
        get
       ;
        set
        ;
    }

    public string Status
    {
        get
        ;
        set
       ;
    }
    public string Message
    {
        get
        ;
        set
       ;
    }

    public string[] ApplicationNumber
    { get; set; }
  

}