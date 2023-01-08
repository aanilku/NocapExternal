using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for ValidationUtility
/// </summary>
public class ValidationUtility
{



    public static readonly string txtValForNOCNumber = "^(CGWA)/(NOC)/(IND)/(ORIG)/[0-9]{4}/[0-9]{1,4}$"; // Validation for  Numeric , Characters given Format
    public static readonly string txtValForNOCNumberMsg = "Invalid NOC Number Format."; //Validation for NOC Number  Message

    public static readonly string txtValForNOCNumberINF = "^(CGWA)/(NOC)/(INF)/(ORIG)/[0-9]{4}/[0-9]{1,4}$";

    public static readonly string txtValForNOCNumberINFMsg = "Invalid NOC Number Format."; //Validation for NOC Number  Message



    public static readonly string txtValForNOCNumberMIN = "^(CGWA)/(NOC)/(MIN)/(ORIG)/[0-9]{4}/[0-9]{1,4}$"; // Validation for  Numeric , Characters given Format
    public static readonly string txtValForNOCNumberMINMsg = "Invalid NOC Number Format."; //Validation for NOC Number  Message

    public static readonly string txtValSingleLineWithSpecialCharacters = "^([a-z]|[A-Z]|[ ]|[.]|[\\-]|[_]|[/]|[\\(]|[\\)]|[0-9])*$"; //Validation For Single Line Text Box
    public static readonly string txtValSingleLineWithSpecialCharactersMsg = "Only AlphaNumeric Value with . _ - / ( ) Characters allowed"; //Validation For Single Line Text Box Message

    public static readonly string txtValNAMultiLine = "^[^<>&'\"]*$"; //Validation Not Allowed
    public static readonly string txtValNAMultiLineMsg = "Special Charactors < > & ' \" are not Allowed."; //Validation Not Allowed Message

    public static readonly string txtValNAMultiLineWithOutEncoding = "^[^'\"]*$"; //Validation Not Allowed Without using Encoding
    public static readonly string txtValNAMultiLineWithOutEncodingMsg = "Special Charactors ' \" are not Allowed."; //Validation Not Allowed Without using Encoding Message

    public static readonly string txtValForNumeric = "^[0-9]*$"; //Validation for only Numeric Characters
    public static readonly string txtValForNumericMsg = "Only Numeric Characters allowed."; //Validation for only Numeric Characters Message

    public static readonly string txtValForProjectName = "^([a-z]|[A-Z]|[ ]|[.]|[\\-]|[_]|[/]|[\\(]|[\\)]|[0-9]|[#])*$"; //Validation for only Numeric Characters
    public static readonly string txtValForProjectNameMsg = "Only [a-z] [A-Z] . - _ / ( ) # [0-9] Characters allowed."; //Validation for only Numeric Characters Message

    public static readonly string txtValForPinCode = "^[1-9][0-9]{5}$";//Validation for PinCode
    public static readonly string txtValForPinCodeMsg = "PinCode must be of 6 numeric digits and First digit cannot be ZERO."; //Validation for PinCode Message

    public static readonly string txtValForNumericWithOutFirstCharacterZero = "^[1-9][0-9]*$"; //Validation for Numeric With Out First Character Zero
    public static readonly string txtValForNumericWithOutFirstCharacterZeroMsg = "Only Numeric Characters allowed and First character cannot be Zero."; //Validation for Numeric With Out First Character Zero Message

    public static readonly string txtValForNumericAllWithOutZero = "^[1-9]*$"; //Validation for Numeric All With Out Zero
    public static readonly string txtValForNumericAllWithOutZeroMsg = "Zero Not Allowed."; //Validation for Numeric With Out First Character Zero Message

    public static readonly string txtValForEmail = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"; //Validation for Email
    public static readonly string txtValForEmailMsg = "Invalid Email."; //Validation for Email Message

    public static readonly string txtValForDate = "^(?:(?:(?:0?[1-9]|1\\d|2[0-8])\\/(?:0?[1-9]|1[0-2]))\\/(?:(?:1[6-9]|[2-9]\\d)\\d{2}))$|^(?:(?:(?:31\\/0?[13578]|1[02])|(?:(?:29|30)\\/(?:0?[1,3-9]|1[0-2])))\\/(?:(?:1[6-9]|[2-9]\\d)\\d{2}))$|^(?:29\\/0?2\\/(?:(?:(?:1[6-9]|[2-9]\\d)(?:0[48]|[2468][048]|[13579][26]))))$"; //Validation for Date
    public static readonly string txtValForDateMsg = "Invalid Date Format."; //Validation for Date Message

    public static readonly string txtValForMobileNumber = "^[1-9][0-9]{9}$";//Validation for MobileNumber
    public static readonly string txtValForMobileNumberMsg = "Mobile Number must be of 10 numeric digits and First digit cannot be ZERO."; //Validation for MobileNumber Message

    public static readonly string txtValForPanCard = "^[A-Za-z]{5}[0-9]{4}[A-Za-z]{1}$";//Validation for PanCard
    public static readonly string txtValForPanCardMsg = "Invalid Pan Card Number."; //Validation for PanCard Message

    public static readonly string txtValForBPLCard = "^[a-zA-Z0-9/-]{5,20}[^\\@<>_!]$";//Validation for BPLCard
    public static readonly string txtValForBPLCardMsg = "Invalid BPL Card Number."; //Validation for BPLCard Message

    public static readonly string txtValForVoterID = "^[A-Za-z]{3}[0-9]{7}$";//Validation for Voter ID
    public static readonly string txtValForVoterIDMsg = "Invalid Voter ID Number."; //Validation for Voter ID Message

    public static string txtValForDecimalValue(string IntegerPart, string FractionalPart) //Validation for Decimal Value
    {
        return "^\\d{0," + IntegerPart + "}(\\.\\d{0," + FractionalPart + "})?$";
    }
    public static string txtValForDecimalValueMsg(string IntegerPart, string FractionalPart) //Validation for Decimal Value Message
    {
        string IntPart = "", FraPart = "";
        for (int i = 0; i < Convert.ToInt32(IntegerPart); i++) { IntPart = IntPart + "X"; }
        for (int i = 0; i < Convert.ToInt32(FractionalPart); i++) { FraPart = FraPart + "X"; }
        return "Invalid Value. Format should be " + IntPart + "." + FraPart + ".";
    }

    public static string txtValForMaximumCharacterAllowed(string MaximumValue) //Validation for Maximum Character Limit
    {
        return "^[\\s\\S]{0," + MaximumValue + "}$";
    }
    public static string txtValForMaximumCharacterAllowedMsg(string MaximumValue) //Validation for Maximum Character Limit Message
    {
        return "Character length should be Less Than " + MaximumValue + ".";
    }



    public static string txtValForNegativeDecimalValue(string IntegerPart, string FractionalPart) //Validation for Decimal Value
    {
        return "^[-+]?\\d{0," + IntegerPart + "}(\\.\\d{0," + FractionalPart + "})?$";
    }


    public static string txtValForNegativeDecimalValueMsg(string IntegerPart, string FractionalPart) //Validation for Decimal Value Message
    {
        string IntPart = "", FraPart = "";
        for (int i = 0; i < Convert.ToInt32(IntegerPart); i++) { IntPart = IntPart + "X"; }
        for (int i = 0; i < Convert.ToInt32(FractionalPart); i++) { FraPart = FraPart + "X"; }
        return "Invalid Value. Format should be " + IntPart + "." + FraPart + ".";
    }

    public static bool ValidateSWSId(string SWSId)
    {
        Regex objAlphaNumericPattern = new Regex("^([sw|SW]{2}[0-9]{10})");              
        return !objAlphaNumericPattern.IsMatch(SWSId); 
    }
    public static bool ValidateINDNOC(string SWSId)
    {
        Regex objAlphaNumericPattern = new Regex("^(CGWA)/(NOC)/(IND)/(ORIG)/[0-9]{4}/[0-9]{1,4}$");
        return !objAlphaNumericPattern.IsMatch(SWSId);
    }
    public static bool ValidateINDRenewNOC(string SWSId)
    {
        Regex objAlphaNumericPattern = new Regex("^(CGWA)/(NOC)/(IND)/(REN)/[0-9]{1,2}/[0-9]{4}/[0-9]{1,4}$");
        return !objAlphaNumericPattern.IsMatch(SWSId);
    }
    
    public static bool ValidateINFNOC(string SWSId)
    {
        Regex objAlphaNumericPattern = new Regex("^(CGWA)/(NOC)/(INF)/(ORIG)/[0-9]{4}/[0-9]{1,4}$");
        return !objAlphaNumericPattern.IsMatch(SWSId);
    }
    public static bool ValidateINFRenewNOC(string SWSId)
    {
        Regex objAlphaNumericPattern = new Regex("^(CGWA)/(NOC)/(INF)/(REN)/[0-9]{1,2}/[0-9]{4}/[0-9]{1,4}$");
        return !objAlphaNumericPattern.IsMatch(SWSId);
    }
    public static bool ValidateMINNOC(string SWSId)
    {
        Regex objAlphaNumericPattern = new Regex("^(CGWA)/(NOC)/(MIN)/(ORIG)/[0-9]{4}/[0-9]{1,4}$");
        return !objAlphaNumericPattern.IsMatch(SWSId);
    }
    public static bool ValidateMINRenewNOC(string SWSId)
    {
        Regex objAlphaNumericPattern = new Regex("^(CGWA)/(NOC)/(MIN)/(REN)/[0-9]{1,2}/[0-9]{4}/[0-9]{1,4}$");
        return !objAlphaNumericPattern.IsMatch(SWSId);
    }

}