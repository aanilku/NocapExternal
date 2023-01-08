using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NOCAP.BLL;

/// <summary>
/// Summary description for NSWSUser
/// </summary>
public class NSWSUser
{

    private long lng_exUserCode;

    private int int_exTitleCode;
    private string str_exFirstName;
    private string str_exLastName;
    private string str_exUserName;
    private string str_exUserPassword;
    private string str_exEmailID;
    private string str_exAlternateEmailID;
    //private string str_exDesignation;
    private string str_exPhoneNumber;
    private string str_exPhoneNumberISD;
    private string str_exPhoneNumberSTD;
    private string str_exMobileNumber;
    private string str_exMobileNumberISD;

    private string str_custMessage;

    private int int_actionPerformByUserCode;
    private DateTime dt_exLastLogin;
    private string str_remark;
    private string str_addressLine1;
    private string str_addressLine2;
    private string str_addressLine3;
    private int int_stateCode;
    private int int_districtCode;
    private int? int_subDistrictCode;
    private int int_pinCode;
    private string dt_dateOfBirth;
    private int int_genderCode;
    //private int str_uID;
    private string str_uID;
    private int int_iDProofTypeCode;
    private string str_iDProofUniqueNo;

    private string str_IDProofAttName;
    private byte[] str_IDProofattFile;
    private string str_IDProoffileContentType;
    private string str_IDProoffileExtension;


    private DateTime? dt_createdOnByUC;
    private int? int_createdByUC;
    private DateTime? dt_createdOnExUC;
    private long? lng_createdByExUC;
    private DateTime? dt_modifiedOnByUC;
    private int? int_modifiedByUC;
    private DateTime? dt_modifiedOnByExUC;
    private long? lng_modifiedByExUC;


    private VisibilityYesNo enu_visibility;





    //private NOCAP.BLL.UserManagement.ExternalUser[] arr_externalUserCollection;


    //public NSWSUser()
    //{
    //    InvestorSWSId = null;
    //    IDProofAttName = "";
    //    ContentType = "";
    //    FileExtension = "";
    //    PwdHash = "";
    //    PwdSalt = 0;
    //    ExternalUserMobileNumber
    //}

    public string InvestorSWSId
    {
        get
       ;
        set
        ;
    }
    public enum SortingField
    {
        NoSorting = 0,
        ExternalUserCode = 1,
        ExternalUserName = 2,
    }

    public enum VisibilityYesNo
    {

        Yes = 1,
        No = 2,
    }

    public string ApprovalId { get; set; }
    public string IDProofAttName
    {
        get { return str_IDProofAttName; }
        set { str_IDProofAttName = value; }
    }

    public byte[] AttachmentFile
    {
        get
        {
            return str_IDProofattFile;
        }
        set
        {
            str_IDProofattFile = value;
        }

    }

    public string ContentType
    {
        get
        {
            return str_IDProoffileContentType;
        }
        set
        {
            str_IDProoffileContentType = value;
        }
    }

    public string FileExtension
    {
        get
        {
            return str_IDProoffileExtension;
        }
        set
        {
            str_IDProoffileExtension = value;
        }
    }


    public string PwdHash { get; set; }
    public long PwdSalt { get; set; }

    public string ExternalUserMobileNumber
    {
        get
        {
            return str_exMobileNumber;
        }
        set
        {
            str_exMobileNumber = value;

            ////if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 10) == 1)
            //{
            //    str_exMobileNumber = value.Trim();
            //}
            //else
            //{
            //    str_exMobileNumber = "";
            //    this.CustumMessage = "External User Mobile Number- External User Mobile Number not have more than 10 characters.";
            //   // throw new ApplicationException("External User Mobile Number- External User Mobile Number have more than 10 characters.");
            //}
        }
    }
    public string ExternalUserMobileNumberISD
    {
        get
        {
            return str_exMobileNumberISD;
        }
        set
        {
            str_exMobileNumberISD = value;

            //if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 2) == 1)
            //{
            //    str_exMobileNumberISD = value.Trim();
            //}
            //else
            //{
            //    str_exMobileNumberISD = "";
            //    this.CustumMessage = "External User Mobile Number- External User Mobile Number ISD not have more than 2 characters.";
            //    throw new ApplicationException("External User Mobile Number- External User Mobile Number ISD have more than 2 characters.");
            //}
        }
    }
    public string ExternalUserPhoneNumber
    {
        get
        {
            return str_exPhoneNumber;
        }
        set
        {
            str_exPhoneNumber = value;

            //if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 10) == 1)
            //{
            //    str_exPhoneNumber = value.Trim();
            //}
            //else
            //{
            //    str_exPhoneNumber = "";
            //    this.CustumMessage = "External User Phone Number- External User Phone Number  not have more than 10 characters.";
            //    throw new ApplicationException("External User Phone Number- External User Phone Number have more than 10 characters.");
            //}
        }
    }


    public string ExternalUserPhoneNumberISD
    {
        get
        {
            return str_exPhoneNumberISD;
        }
        set
        {
            str_exPhoneNumberISD = value;

            //if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 4) == 1)
            //{
            //    str_exPhoneNumberISD = value.Trim();
            //}
            //else
            //{
            //    str_exPhoneNumberISD = "";
            //    this.CustumMessage = "External User Phone Number- External User Phone Number ISD not have more than 4 characters.";
            //    throw new ApplicationException("External User Phone Number- External User Phone Number ISD have more than 4 characters.");
            //}

        }
    }


    public string ExternalUserPhoneNumberSTD
    {
        get
        {
            return str_exPhoneNumberSTD;
        }
        set
        {
            str_exPhoneNumberSTD = value;

            //if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 4) == 1)
            //{
            //    str_exPhoneNumberSTD = value.Trim();
            //}
            //else
            //{
            //    str_exPhoneNumberSTD = "";
            //    this.CustumMessage = "External User Phone Number- External User Phone Number STD not have more than 4 characters.";
            //    throw new ApplicationException("External User Phone Number- External User Phone Number STD have more than 4 characters.");
            //}
        }
    }


    public int ExternalUserTitleCode
    {
        get
        {
            return int_exTitleCode;
        }
        set
        {
            int_exTitleCode = value;
        }
    }

    public long ExternalUserCode
    {
        get
        {
            return lng_exUserCode;
        }
        internal set
        {
            lng_exUserCode = value;
        }
    }

    public string ExternalUserActive
    {
        get
        ;
        set
        ;
    }

    public string ExternalUserAlternateEmailID
    {
        get
        {
            return str_exAlternateEmailID;
        }
        set
        {
            str_exAlternateEmailID = value;

            //if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 50) == 1)
            //{
            //    str_exAlternateEmailID = value.Trim();
            //}
            //else
            //{
            //    str_exAlternateEmailID = "";
            //    this.CustumMessage = "External User Alternate Email ID- External User Alternate Email ID not have more than 50 characters.";
            //    throw new ApplicationException("External User Alternate Email ID- External User Alternate Email ID have more than 50 characters.");
            //}

        }
    }

    public string ExternalUserEmailID
    {
        get
        {
            return str_exEmailID;
        }
        set
        {
            str_exEmailID = value;

            //if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 50) == 1)
            //{
            //    str_exEmailID = value.Trim();
            //}
            //else
            //{
            //    str_exEmailID = "";
            //    this.CustumMessage = "External User Email ID- External User Email ID not have more than 50 characters.";
            //    throw new ApplicationException("External User Email ID- External User Email ID have more than 50 characters.");
            //}

        }
    }

    public string ExternalUserFirstName
    {
        get
        {
            return str_exFirstName;
        }
        set
        {
            str_exFirstName = value;

            //if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 30) == 1)
            //{
            //    str_exFirstName = value.Trim();
            //}
            //else
            //{
            //    str_exFirstName = "";
            //    this.CustumMessage = "External User First Name- External User First Name not have more than 30 characters.";
            //    throw new ApplicationException("External User First Name- External User First Name have more than 30 characters.");
            //}
        }
    }

    public string ExternalUserLastName
    {
        get
        {
            return str_exLastName;
        }
        set
        {
            str_exLastName = value;

            //if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 30) == 1)
            //{
            //    str_exLastName = value.Trim();
            //}
            //else
            //{
            //    str_exLastName = "";
            //    this.CustumMessage = "External User Last Name- External User Last Name not have more than 30 characters.";
            //    throw new ApplicationException("External User Last Name- External User Last Name have more than 30 characters.");
            //}

        }
    }

    public string ExternalUserPassword
    {
        get
        {
            return str_exUserPassword;
        }
        set
        {
            str_exUserPassword = value;

            //if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 50) == 1)
            //{
            //    str_exUserPassword = value.Trim();
            //}
            //else
            //{
            //    str_exUserPassword = "";
            //    this.CustumMessage = "External User Password- External User Password not have more than 50 characters.";
            //    throw new ApplicationException("External User Password- External User Password have more than 50 characters.");
            //}

        }
    }

    public string ExternalUserName
    {
        get
        {
            return str_exUserName;

        }
        set
        {
            str_exUserName = value;

            //if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 30) == 1)
            //{
            //    str_exUserName = value.Trim();
            //}
            //else
            //{
            //    str_exUserName = "";
            //    this.CustumMessage = "External User Password- External User Password not have more than 30 characters.";
            //    throw new ApplicationException("External User Password- External User Password have more than 30 characters.");
            //}

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

    public int ActionPerformByUserCode
    {
        internal get
        {
            return int_actionPerformByUserCode;
        }
        set
        {
            int_actionPerformByUserCode = value;
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

    public int? CreatedByUC
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

    public int? ModifiedByUC
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

    public DateTime? ModifiedOnByExUC
    {
        get
        {
            return dt_modifiedOnByExUC;
        }
        internal set
        {
            dt_modifiedOnByExUC = value;
        }
    }
    public long? ModifiedByExUC
    {
        get
        {
            return lng_modifiedByExUC;
        }
        set
        {
            lng_modifiedByExUC = value;
        }
    }


    public VisibilityYesNo Visibility
    {
        get
        {
            return enu_visibility;
        }
        set
        {
            enu_visibility = value;
        }
    }

    public DateTime ExternalUserLastLogin
    {
        get
        {
            return dt_exLastLogin;
        }
        set
        {
            dt_exLastLogin = value;
        }
    }

    public string Remark
    {
        get
        {
            return str_remark;
        }
        set
        {
            str_remark = value;

            //if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 30) == 1)
            //{
            //    str_remark = value.Trim();
            //}
            //else
            //{
            //    str_remark = "";
            //    this.CustumMessage = "External User Remark- External User Remark not have more than 30 characters.";
            //    throw new ApplicationException("External User Remark- External User Remark have more than 30 characters.");
            //}

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
            str_addressLine1 = value;

            //if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 100) == 1)
            //{
            //    str_addressLine1 = value.Trim();
            //}
            //else
            //{
            //    str_addressLine1 = "";
            //    this.CustumMessage = "External User AddressLine1- External User AddressLine1 not have more than 100 characters.";
            //    throw new ApplicationException("External User AddressLine1- External User AddressLine1 have more than 100 characters.");
            //}
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
            str_addressLine2 = value;

            //if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 100) == 1)
            //{
            //    str_addressLine2 = value.Trim();
            //}
            //else
            //{
            //    str_addressLine2 = "";
            //    this.CustumMessage = "External User AddressLine2- External User AddressLine2 not have more than 100 characters.";
            //    throw new ApplicationException("External User AddressLine2- External User AddressLine2 have more than 100 characters.");
            //}
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
            str_addressLine3 = value;

            //if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 100) == 1)
            //{
            //    str_addressLine3 = value.Trim();
            //}
            //else
            //{
            //    str_addressLine3 = "";
            //    this.CustumMessage = "External User AddressLine3- External User AddressLine3 not have more than 100 characters.";
            //    throw new ApplicationException("External User AddressLine3- External User AddressLine3 have more than 100 characters.");
            //}
        }
    }

    public int StateCode
    {
        get
        {
            return int_stateCode;
        }
        set
        {
            int_stateCode = value;
        }
    }

    public int DistrictCode
    {
        get
        {
            return int_districtCode;
        }
        set
        {
            int_districtCode = value;
        }
    }

    public int? SubDistrictCode
    {
        get
        {
            return int_subDistrictCode;
        }
        set
        {
            int_subDistrictCode = value;
        }
    }

    public int PinCode
    {
        get
        {
            return int_pinCode;
        }
        set
        {
            int_pinCode = value;
        }
    }

    public string DateOfBirth
    {
        get
        {
            return dt_dateOfBirth;
        }
        set
        {
            dt_dateOfBirth = value;
            //if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.ToString().Trim(), 10) == 1)
            //{
            //    dt_dateOfBirth = value;
            //}
            //else
            //{
            //    // dt_dateOfBirth = ;
            //    this.CustumMessage = "External User Date Of Birth- External User Date Of Birth not have more than 10 characters.";
            //    throw new ApplicationException("External User Date Of Birth- External User Date Of Birth have more than 10 characters.");
            //}
        }
    }

    public int GenderCode
    {
        get
        {
            return int_genderCode;
        }
        set
        {
            int_genderCode = value;
        }
    }

    public string UID
    {
        get
        {
            return str_uID;
        }
        set
        {
            str_uID = value;
        }
    }

    public int IDProofTypeCode
    {
        get
        {
            return int_iDProofTypeCode;
        }
        set
        {
            int_iDProofTypeCode = value;
        }
    }

    public string IDProofUniqueNo
    {
        get
        {
            return str_iDProofUniqueNo;
        }
        set
        {
            str_iDProofUniqueNo = value;

            //if (NOCAP.BLL.Utility.Utility.IsValidStringHavingLength(value.Trim(), 50) == 1)
            //{
            //    str_iDProofUniqueNo = value.Trim();
            //}
            //else
            //{
            //    str_iDProofUniqueNo = "";
            //    this.CustumMessage = "External User ID Proof Unique Number- External User ID Proof Unique Number not have more than 50 characters.";
            //    throw new ApplicationException("External User ID Proof Unique Number- External User ID Proof Unique Number have more than 50 characters.");
            //}
        }
    }




}